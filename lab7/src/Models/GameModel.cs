using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;

namespace Models
{
    [Table("game")]
    public class Game
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Column("name")]
        public string? Name {get; set;}
        [Column("cost", TypeName = "money")]
        public decimal Cost {get; set;}
        [Column("release")]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ReleaseDate {get; set;}
        [ForeignKey("series")]
        [Column("series")]
        public Series? Series {get; set;}
        [ForeignKey("developer_id")]
        [Column("developer_id")]
        public Developer Developer {get; set;}
        [Column("description")]
        public string? Description {get; set;} 

        public Game() {}

        public Game(string _name, decimal _cost, string? _description) {
            Name = _name;
            Cost = _cost;
            Description = _description;
        }
    }
}