CREATE TABLE [dbo].[Voti]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdAllievo] INT NULL, 
    [IdMateria] INT NULL, 
    [Voto] INT NULL, 
    [DataVoto] DATETIME NULL, 
    [DataInserimento] DATETIME NULL, 
    [DataModifica] DATETIME NULL
)
