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
        UnaryResult<string> ConnectionTestAsync();
    }
}
