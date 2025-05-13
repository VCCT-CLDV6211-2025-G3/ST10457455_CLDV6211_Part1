using System;

namespace EventEaseBookingSystem.Models
{
    public class BookingView
    {
        public int BookingId { get; set; }
        public int VenueId { get; set; }
        public required string VenueName { get; set; }
        public required string VenueLocation { get; set; }
        public int EventId { get; set; }
        public required string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}