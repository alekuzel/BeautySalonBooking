using System.ComponentModel.DataAnnotations;

namespace BeautySalonBooking.Models
{
    public class AboutPage
    {
        public int Id { get; set; }

       
        public string Title { get; set; }

       public string? Content { get; set; }

    }
}
