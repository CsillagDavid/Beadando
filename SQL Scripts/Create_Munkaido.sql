-- =========================================
-- Create table template
-- =========================================
USE wtDB
GO

IF OBJECT_ID('dbo.Munkaidok', 'U') IS NOT NULL
  DROP TABLE dbo.Munkaidok
GO

CREATE TABLE [dbo].[Munkaidok] (
    [id]            INT          IDENTITY (1, 1) NOT NULL,
    [Datum]         DATE         NOT NULL,
    [Kezdo_ido]     DECIMAL (18) NOT NULL,
    [Befejezo_ido]  DECIMAL (18) NOT NULL,
    [FelhasznaloID] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC), 
    CONSTRAINT fk_munkaidok_felhasznalok FOREIGN KEY (FelhasznaloID) REFERENCES Felhasznalok(id)
);

GO
