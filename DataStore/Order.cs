using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataStore
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        [MaxLength(300)]
        [Required]
        public string Description { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}

