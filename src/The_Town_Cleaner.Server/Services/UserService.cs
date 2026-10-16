using MagicOnion;
using MagicOnion.Server;
using The_Town_Cleaner.Server.Models.Contexts;
using The_Town_Cleaner.Shared.Interfaces.Services;

namespace The_Town_Cleaner.Server.Services {
    public class UserService : ServiceBase<IUserService>, IUserService{
        private readonly GameDbContext _context;

        // 排他制御用
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        // DI
        public UserService(GameDbContext context) {
            _context = context;
        }

        /// <summary>
        /// 接続テスト
        /// </summary>
        public async UnaryResult<string> ConnectionTestAsync() {
            Console.WriteLine("接続テスト");
            return "Success";
        }
    }
}
