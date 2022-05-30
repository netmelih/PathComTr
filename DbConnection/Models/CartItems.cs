using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DbConnection.Models
{
    [Table("CartItems", Schema = "dbo")]
    public class CartItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CartItemId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Carts Cart { get; set; }

        [Required]
        public int Amount { get; set; }

        [ForeignKey("Accounts")]
        [Required]
        [JsonIgnore]
        public Guid ProductId { get; set; }

        public Products Product { get; set; }

    }
}
