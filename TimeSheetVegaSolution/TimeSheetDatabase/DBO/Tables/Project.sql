CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(50) NOT NULL, 
    [ProjectStatus] INT NOT NULL, 
    [EmployeeId] INT NULL, 
    [ClientId] INT NULL, 
    [RowId] ROWVERSION NULL, 
    [IsRemoved] BIT NULL, 
    CONSTRAINT [FK_Project_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]), 
    CONSTRAINT [FK_Project_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id])
)
