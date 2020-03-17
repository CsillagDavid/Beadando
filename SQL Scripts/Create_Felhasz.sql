-- =========================================
-- Create table template
-- =========================================
USE wtDB
GO

IF OBJECT_ID('dbo.Felhasznalok', 'U') IS NOT NULL
  DROP TABLE dbo.Felhasznalok
GO

CREATE TABLE dbo.Felhasznalok
(
	id int IDENTITY(1,1) PRIMARY KEY,
	Nev nvarchar(255) NOT NULL, 
	Jelszo nvarchar(255) NOT NULL,
	Email nvarchar(255) NOT NULL,
	Munkaido int NOT NULL
)
GO
