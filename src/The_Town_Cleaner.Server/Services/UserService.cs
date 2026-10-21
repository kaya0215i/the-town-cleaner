using MagicOnion;
using MagicOnion.Server;
using Microsoft.EntityFrameworkCore;
using The_Town_Cleaner.Server.Models.Contexts;
using The_Town_Cleaner.Shared.Interfaces.Services;
using The_Town_Cleaner.Shared.Models.Entities;

namespace The_Town_Cleaner.Server.Services {
    public class UserService : ServiceBase<IUserService>, IUserService{
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
        /// 接続テスト
        /// </summary>
        public async UnaryResult<bool> ConnectionTestAsync() {
            Console.WriteLine("接続テスト：Success");
            return true;
        }

        /// <summary>
        /// ユーザー登録
        /// </summary>
        public async UnaryResult<int> RegistUserAsync(string name) {
            //同時実行対策 ロック
            await _semaphore.WaitAsync();

            try {
                // 存在したら
                if (await _context.Users.AnyAsync(user=>user.Name == name)) {
                    Console.WriteLine("ユーザー名が使用されています。");
                    throw new ReturnStatusException(Grpc.Core.StatusCode.AlreadyExists, "ユーザー名が使用されています。");
                }

                // 作成
                User user = new User() {
                    Name = name,
                    Created_at = TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeZoneInfo),
                    Updated_at = TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeZoneInfo)
                };

                // 追加
                _context.Users.Add(user);

                // 保存
                await _context.SaveChangesAsync();
                return user.Id;
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
                    Created_at = TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeZoneInfo),
                    Updated_at = TimeZoneInfo.ConvertTime(DateTime.Now, tokyoTimeZoneInfo)
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
                .Select(_=> new Ranking() { name = _.name, score = _.score})
                // 配列にする
                .ToArrayAsync();
        }
    }
}
