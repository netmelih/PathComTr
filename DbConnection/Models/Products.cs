using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbConnection.Models
{
    [Table("Products", Schema = "dbo")]
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string ProductDescription { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        public ProductStatuses ProductStatus { get; set; }
    }

    public enum ProductStatuses
    {
        Deleted = 0,
        Active = 1,
        Suspended = 2
    }
}
