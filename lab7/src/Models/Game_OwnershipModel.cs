using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("game_ownership")]
    public class GameOwnership
    {
        [Key]
        [Column("id")]
        public int Id {get; set;}
        
        [ForeignKey("player_id")]
        [Column("player_id")]
        public Player Player {get; set;}
        
        [ForeignKey("game_id")]
        [Column("game_id")]
        public Game Game {get; set;}
        [Column("is_gift")]
        public bool IsGift {get; set;}
        [Column("date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Date {get; set;}
    }
}