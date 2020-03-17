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
	id int IDENTITY(1,1) PRIMARY KEY,
	Datum date NOT NULL, 
	Kezdo_ido decimal(18, 0) NOT NULL,
	Befejezo_ido decimal(18, 0) NOT NULL,
	FelhasznaloID int NOT NULL
)
GO
