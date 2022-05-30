using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbConnection.Models
{
    [Table("Carts", Schema = "dbo")]
    public class Carts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CartId { get; set; }

        public Accounts Account { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalPrice { get; set; }

        public CartStatuses CartStatus { get; set; }

        public List<CartItems> CartItems { get; set; }
    }

    public enum CartStatuses
    {
        Deleted = 0,
        Active = 1,
        Ordered = 2,
        Completed = 2
    }
}
