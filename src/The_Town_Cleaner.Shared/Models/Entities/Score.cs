using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Town_Cleaner.Shared.Models.Entities {
    [Table("scores")]
    [MessagePackObject]
    public class Score {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public int Value { get; set; }
        [Key(2)]
        public int User_Id { get; set; }
        [Key(3)]
        public int Stage_Id { get; set; }
    }
}
