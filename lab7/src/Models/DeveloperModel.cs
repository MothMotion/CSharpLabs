using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("developer")]
    public class Developer
    {
        [Key]
        [Column("id")]
        public long Id {get; private set;}
        [Column("name")]
        public string Name {get; set;}
        [Column("rating")]
        public double Rating {get; set;}
        [Column("workers_amount")]
        public short WAmount {get; set;}

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}