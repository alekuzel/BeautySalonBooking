using System.ComponentModel.DataAnnotations;

namespace BeautySalonBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required, EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        public int ServiceId { get; set; }

        public Service? Service { get; set; }
    }
}
