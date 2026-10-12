--use the database
USE [Cars4U]
GO
--select all records from the Cars table
SELECT * FROM [Cars]
GO
--selecting all the records from the ApplicationUsers table and the town of the users
SELECT
	[AspNetUsers].[Id],
	[AspNetUsers].[FirstName],
	[AspNetUsers].[LastName],
	[AspNetUsers].[UserName],
	[AspNetUsers].[PhoneNumber],
	[AspNetUsers].[Email],
	[AspNetUsers].[IsAdmin],
	[Towns].[Name] AS [Town]
FROM [AspNetUsers]
JOIN [Towns]
ON [AspNetUsers].[TownId] = [Towns].[Id];