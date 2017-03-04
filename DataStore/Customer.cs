using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DataStore
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(200)]
        [Required]
        public string Address { get; set; }

        public bool IsVIP { get; set; }

        public ObservableCollection<Order> Orders { get; set; }
    }
}
