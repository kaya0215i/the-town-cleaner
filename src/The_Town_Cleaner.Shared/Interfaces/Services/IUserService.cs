using MagicOnion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Town_Cleaner.Shared.Models.Entities;

namespace The_Town_Cleaner.Shared.Interfaces.Services {
    public interface IUserService : IService<IUserService>{
        /// <summary>
        /// ユーザー登録
        /// </summary>
        UnaryResult<User> RegistUserAsync();

        /// <summary>
        /// スコア送信
        /// </summary>
        UnaryResult SendScoreAsync(int userId, int stageId, int score);

        /// <summary>
        /// スコア取得
        /// </summary>
        UnaryResult<Ranking[]> GetScoreRankingAsync(int stageId);
    }
}
