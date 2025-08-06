using System;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }


        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        // Navigation property (valfri)
        public Service Service { get; set; }
    }
}
