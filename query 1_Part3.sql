--1. Create EventType Lookup Table
CREATE TABLE EventType (
EventTypeId INT PRIMARY KEY IDENTITY (1,1),
TypeName NVARCHAR(100) NOT NULL 
);

--2. Insert Sample Event Types
INSERT INTO EventType (TypeName)
VALUES ('Conference'), ('Wedding'), ('Workshop'), ('Concert');

--3. Alter Event table to include EventTypeId
ALTER TABLE Event
ADD EventTypeId INT;

--4. Add Foreign Key Constraints from Event to EventType
ALTER TABLE Event 
ADD CONSTRAINTS FK_Event_EventType
FOREIGN KEY (EventTypeId) REFERENCES EventType(EventTypeId);

--5. Update Venue Table to include IsAvailable column
ALTER TABLE Venue
ADD IsAvailable BIT NOT NULL DEFAULT 1;


