using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Town_Cleaner.Shared.Models.Entities {
    [MessagePackObject]
    public class Ranking {
        [Key(0)]
        public string name;
        [Key(1)]
        public int score;
    }
}
