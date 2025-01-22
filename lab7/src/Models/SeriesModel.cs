using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("game_series")]
    public class Series
    {
        [Key]
        [Column("id")]
        public long Id {get; set;}
        [Column("name")]
        public string? Name {get; set;}

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}