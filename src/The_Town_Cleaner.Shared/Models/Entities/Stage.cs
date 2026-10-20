using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Town_Cleaner.Shared.Models.Entities {
    [Table("stages")]
    [MessagePackObject]
    public class Stage {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string Name { get; set; }
    }
}
