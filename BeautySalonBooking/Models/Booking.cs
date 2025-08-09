using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BeautySalonBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int ServiceId { get; set; }  // FK property

        // Navigation property
        [ValidateNever]
        public Service Service { get; set; }

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
        [MaxLength(500)]
         public string? Comment { get; set; }

    }
    
}
