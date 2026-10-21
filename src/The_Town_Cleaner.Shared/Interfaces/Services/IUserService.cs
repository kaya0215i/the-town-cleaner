using MagicOnion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Town_Cleaner.Shared.Interfaces.Services {
    public interface IUserService : IService<IUserService>{
        /// <summary>
        /// 接続テスト
        /// </summary>
        UnaryResult<bool> ConnectionTestAsync();

        /// <summary>
        /// ユーザー登録
        /// </summary>
        UnaryResult<int> RegistUserAsync(string name);

        /// <summary>
        /// スコア送信
        /// </summary>
        UnaryResult SendScoreAsync(int userId, int stageId, int score);

        /// <summary>
        /// スコア取得
        /// </summary>
        UnaryResult<(string, int)[]> GetScoreRankingAsync(int stageId);
    }
}
