using MagicOnion;
using MagicOnion.Server;
using Microsoft.EntityFrameworkCore;
using The_Town_Cleaner.Server.Models.Contexts;
using The_Town_Cleaner.Shared.Interfaces.Services;
using The_Town_Cleaner.Shared.Models.Entities;

namespace The_Town_Cleaner.Server.Services {
    public class UserService : ServiceBase<IUserService>, IUserService {
        private readonly GameDbContext _context;

        // 排他制御用
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        // DI
        public UserService(GameDbContext context) {
            _context = context;
        }

        // タイムゾーン設定
        private TimeZoneInfo tokyoTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        /// <summary>
        /// 時間取得
        /// </summary>
        private DateTime GetTimeNow() {
            return TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeZoneInfo);
        }

        /// <summary>
        /// ユーザー登録
        /// </summary>
        public async UnaryResult<User> RegistUserAsync() {
            //同時実行対策 ロック
            await _semaphore.WaitAsync();

            try {
                // 名前作成
                string userName = string.Empty;

                if (await _context.Users.CountAsync() <= 0) {
                    userName = "Player1001";
                }
                else {
                    userName = "Player" + ((await _context.Users.OrderBy(user => user.Id).LastAsync()).Id + 1001).ToString();
                }

                // 作成
                User user = new User() {
                    Name = userName,
                    Created_at = GetTimeNow(),
                    Updated_at = GetTimeNow()
                };

                // 追加
                _context.Users.Add(user);

                // 保存
                await _context.SaveChangesAsync();

                return user;
            }
            finally {
                // 解除
                _semaphore.Release();
            }
        }

        /// <summary>
        /// 名前変更
        /// </summary>
        public async UnaryResult ChangeNameAsync(int userId, string changedName) {
            //同時実行対策 ロック
            await _semaphore.WaitAsync();

            try {
                // ユーザーが存在するか
                User? user = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);

                if (user == null) {
                    Console.WriteLine("存在しないユーザーidです。");
                    throw new ReturnStatusException(Grpc.Core.StatusCode.NotFound, "存在しないユーザーidです。");
                }

                // その名前が使われているか
                if (await _context.Users.AnyAsync(user => user.Name == changedName)) {
                    Console.WriteLine("ユーザー名が使用されています。");
                    throw new ReturnStatusException(Grpc.Core.StatusCode.AlreadyExists, "ユーザー名が使用されています。");
                }

                // 適応
                user.Name = changedName;
                user.Updated_at = GetTimeNow();

                // 保存
                await _context.SaveChangesAsync();
            }
            finally {
                // 解除
                _semaphore.Release();
            }
        }

        /// <summary>
        /// スコア送信
        /// </summary>
        public async UnaryResult SendScoreAsync(int userId, int stageId, int score) {
            // 存在したら
            if (await _context.Scores.AnyAsync(s => s.User_Id == userId && s.Stage_Id == stageId)) {
                Score myScore = await _context.Scores.FirstAsync(s => s.User_Id == userId && s.Stage_Id == stageId);
                // データベース上のスコアより大きかったら更新
                myScore.Value = myScore.Value < score ? score : myScore.Value;
                myScore.Updated_at = TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeZoneInfo);
            }
            // 存在しなかったら
            else {
                // 作成
                Score myScore = new Score() {
                    Value = score,
                    User_Id = userId,
                    Stage_Id = stageId,
                    Created_at = GetTimeNow(),
                    Updated_at = GetTimeNow()
                };

                // 登録
                _context.Scores.Add(myScore);
            }

            // 保存
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// スコアランキング取得
        /// </summary>
        public async UnaryResult<Ranking[]> GetScoreRankingAsync(int stageId) {
            // 存在しなかったら
            if (!await _context.Scores.AnyAsync(s => s.Stage_Id == stageId)) {
                throw new ReturnStatusException(Grpc.Core.StatusCode.NotFound, "ステージにスコアがありません。");
            }

            return await _context.Scores.Where(_ => _.Stage_Id == stageId) // ステージIdで絞り込み
                                                                           // userテーブルと結合
                .Join(_context.Users, myScore => myScore.User_Id, user => user.Id, (myScore, user) =>
                // 新しいテーブル作成
                new {
                    name = user.Name,
                    score = myScore.Value
                })
                // 降順
                .OrderByDescending(_ => _.score)
                // Ranking型に
                .Select(_ => new Ranking() { name = _.name, score = _.score })
                // 配列にする
                .ToArrayAsync();
        }
    }
}
