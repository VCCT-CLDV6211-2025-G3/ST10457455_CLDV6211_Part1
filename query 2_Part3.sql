--6. Drop existing view if it exists 
IF OBJECT_ID('dbo.BookingOverview', 'V') IS NOT NULL
DROP VIEW dbo.BookingOverview;
