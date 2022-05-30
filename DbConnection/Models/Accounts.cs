using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DbConnection.Models
{
    [Table("Accounts", Schema = "dbo")]
    public class Accounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccountId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string AccountDisplayName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Username { get; set; }

        [Required]
        [JsonIgnore]
        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }

        [JsonIgnore]    
        public List<Carts> Carts { get; set; }
    }
}
