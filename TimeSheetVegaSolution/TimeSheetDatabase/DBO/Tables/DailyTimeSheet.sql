CREATE TABLE [dbo].[DailyTimeSheet]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME2 NULL,
    [Description] VARCHAR(50) NULL, 
    [Time] FLOAT NULL, 
    [Overtime] FLOAT NULL, 
    [ProjectId] INT NULL, 
    [CategoryId] INT NULL, 
    [EmployeeId] INT NULL, 
    [IsRemoved] BIT NULL, 
    CONSTRAINT [FK_DailyTimeSheet_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]), 
    CONSTRAINT [FK_DailyTimeSheet_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id]), 
    CONSTRAINT [FK_DailyTimeSheet_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id])
)
