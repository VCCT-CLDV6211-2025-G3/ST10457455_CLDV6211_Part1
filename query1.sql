-- Drop tables if they already exist (drop children before parents to avoid FK errors)
DROP TABLE IF EXISTS Booking;
DROP TABLE IF EXISTS Event;
DROP TABLE IF EXISTS Venue;

-- Create Venue table
CREATE TABLE Venue (
    VenueId INT PRIMARY KEY IDENTITY,
    VenueName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(150) NOT NULL,
    Capacity INT NOT NULL,
    ImageUrl NVARCHAR(255)
);

-- Create Event table
CREATE TABLE Event (
    EventId INT PRIMARY KEY IDENTITY,
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATE NOT NULL,
    Description NVARCHAR(MAX),
    VenueId INT NULL,
    FOREIGN KEY (VenueId) REFERENCES Venue(VenueId)
);

-- Create Booking table
CREATE TABLE Booking (
    BookingId INT PRIMARY KEY IDENTITY,
    EventId INT NOT NULL,
    VenueId INT NOT NULL,
    BookingDate DATETIME NOT NULL,
    FOREIGN KEY (EventId) REFERENCES Event(EventId),
    FOREIGN KEY (VenueId) REFERENCES Venue(VenueId)
);

-- Insert sample data into Venue
INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl)
VALUES 
('Grand Hall', 'Cape Town Convention Centre, Cape Town', 500, 'https://example.com/images/grandhall.jpg'),
('Sunset Arena', 'Durban Beachfront, Durban', 300, 'https://example.com/images/sunsetarena.jpg'),
('Skyline Ballroom', 'Sandton City, Johannesburg', 800, 'https://example.com/images/skylineballroom.jpg');

-- Insert sample data into Event
INSERT INTO Event (EventName, EventDate, Description, VenueId)
VALUES 
('Tech Conference 2025', '2025-09-15', 'A major technology conference featuring speakers from around the world.', 1),
('Summer Music Festival', '2025-12-05', 'Live performances by top local artists.', 2),
('Business Networking Gala', '2025-06-22', 'An exclusive event for professionals to connect.', 3),
('Startup Pitch Night', '2025-07-10', 'Emerging startups pitch their ideas to investors.', NULL);

-- Insert sample data into Booking
INSERT INTO Booking (EventId, VenueId, BookingDate)
VALUES 
(1, 1, '2025-07-01 10:00:00'),
(2, 2, '2025-08-15 14:30:00'),
(3, 3, '2025-05-10 09:00:00');
