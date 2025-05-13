CREATE VIEW BookingOverview AS
SELECT 
    b.BookingId,
    e.EventName,
    v.VenueName,
    b.BookingDate
FROM 
    dbo.Booking b -- Change 'dbo' to your schema if different
INNER JOIN 
    dbo.Event e ON b.EventId = e.EventId
INNER JOIN 
    dbo.Venue v ON b.VenueId = v.VenueId;