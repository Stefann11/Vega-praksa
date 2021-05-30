CREATE TABLE [dbo].[Client]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(50) NOT NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [ZipCode] NCHAR(5) NOT NULL, 
    [CountryId] INT NOT NULL, 
    CONSTRAINT [FK_Client_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id])
)
