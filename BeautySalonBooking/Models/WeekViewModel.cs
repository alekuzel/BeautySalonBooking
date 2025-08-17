

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BeautySalonBooking.Models
{
    public class WeekViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public List<DateTime> WeekDays { get; set; }
        public List<Booking> Bookings { get; set; }

        public int WeekOffset { get; set; } = 0; // 0 = current week, +1 will be next week, -1 previous week, etc.
    }
}

