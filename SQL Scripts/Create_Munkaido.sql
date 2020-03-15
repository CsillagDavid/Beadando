-- =========================================
-- Create table template
-- =========================================
USE wtDB
GO

IF OBJECT_ID('dbo.Munkaidok', 'U') IS NOT NULL
  DROP TABLE dbo.Munkaidok
GO

CREATE TABLE dbo.Munkaidok
(
	id int NOT NULL, 
	Datum date NOT NULL, 
	Kezdo_ido datetime NOT NULL,
	Befejezo_ido datetime NOT NULL,
	FelhasznaloID int NOT NULL,
	PRIMARY KEY (id)
)
GO
