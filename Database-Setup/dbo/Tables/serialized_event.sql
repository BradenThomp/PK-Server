CREATE TABLE [dbo].[serialized_event]
(
	[aggregate_id] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [version] INT NOT NULL, 
    [data] NVARCHAR(MAX) NOT NULL, 
    [type] NVARCHAR(MAX) NOT NULL
)
