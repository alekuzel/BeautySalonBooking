using System.ComponentModel.DataAnnotations;

namespace BeautySalonBooking.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int DurationMinutes { get; set; } // Optional
    }
}
