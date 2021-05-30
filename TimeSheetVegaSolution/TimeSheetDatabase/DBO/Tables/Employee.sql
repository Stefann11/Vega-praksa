CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(50) NOT NULL, 
    [Username] NCHAR(24) NOT NULL, 
    [Email] NCHAR(35) NOT NULL, 
    [Password] NCHAR(24) NOT NULL, 
    [HoursPerWeek] FLOAT NOT NULL, 
    [EmployeeStatus] INT NOT NULL, 
    [Role] INT NOT NULL
)
