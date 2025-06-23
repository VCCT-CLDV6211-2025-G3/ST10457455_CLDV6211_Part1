--7. Create new consolidated BookingOverview view with flitering fields
CREATE VIEW BookingOverview AS 
SELECT 
b.BookingId,
e.EventName,
e.EventDate,
e.StartTime,
e.EndTime,
e.EventTypeId,
et.TypeName AS EventType, 
v.VenueName,
v.IsAvailable AS IsVenueAvailable,
b.BookingDate
FROM 
Booking b 
INNER JOIN 
Event e ON b.EventId = e.EventId
INNER JOIN 
Venue v ON b.VenueId = v.VenueId
LEFT JOIN 
EventType et ON e.EventTypeId = et.EventTypeId;
