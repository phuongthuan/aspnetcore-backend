using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
    [Table("Customer")]
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "First name cannot be longer than 255 characters.")]
        public string FirstName { get; set; }

        [StringLength(255, ErrorMessage = "Last name cannot be longer than 255 characters.")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "Email cannot be longer than 50 characters.")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Status cannot be longer than 20 characters.")]
        public string Status { get; set; }

        // public ICollection<Order> Orders { get; set; }
    }
}