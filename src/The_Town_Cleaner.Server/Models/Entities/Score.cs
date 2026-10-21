using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Town_Cleaner.Shared.Models.Entities {
    [Table("scores")]
    public class Score {
        public int Id { get; set; }
        public int Value { get; set; }
        public int User_Id { get; set; }
        public int Stage_Id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
