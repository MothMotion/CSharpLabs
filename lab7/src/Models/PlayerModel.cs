using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("player")]
    public class Player
    {
        [Key]
        [Column("id")]
        public long Id {get; set;}
        [Column("nickname")]
        public string? Nickname {get; set;}
        [Column("money_sum")]
        public double MoneySum {get; set;}
    }
}