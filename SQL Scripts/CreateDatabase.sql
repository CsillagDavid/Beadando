USE master
GO

--Adatb�zis l�trehoz�sa, ha m�r l�tezik t�rl�se
IF  EXISTS (
	SELECT name 
		FROM sys.databases 
		WHERE name = N'Nyilvantartas'
)
DROP DATABASE Nyilvantartas
GO

CREATE DATABASE Nyilvantartas
GO

USE Nyilvantartas
GO

--T�bl�k eldob�sa, ha m�r l�teznek
IF OBJECT_ID('[dbo].[Jogkorok]', 'U') IS NOT NULL
  DROP TABLE [dbo].[Jogkorok]
GO

IF OBJECT_ID('[dbo].[Munkaidok]', 'U') IS NOT NULL
  DROP TABLE [dbo].[Munkaidok]
GO

IF OBJECT_ID('[dbo].[Szabadsagok]', 'U') IS NOT NULL
  DROP TABLE [dbo].[Szabadsagok]
GO

IF OBJECT_ID('[dbo].[Felhasznalok]', 'U') IS NOT NULL
  DROP TABLE [dbo].[Felhasznalok]
GO

IF OBJECT_ID('[dbo].[Unnepnapok]', 'U') IS NOT NULL
  DROP TABLE [dbo].[Unnepnapok]
GO

--Felhaszn�l�k t�bla l�trehoz�sa
CREATE TABLE [dbo].[Felhasznalok] (
    [id]       INT            IDENTITY (1, 1) NOT NULL,
    [Nev]      NVARCHAR (255) NOT NULL,
    [Jelszo]   NVARCHAR (255) NOT NULL,
    [Email]    NVARCHAR (255) NOT NULL,
    [Munkaido] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

GO

--Munkaid�k t�bla l�trehoz�sa
CREATE TABLE [dbo].[Munkaidok] (
    [id]            INT          IDENTITY (1, 1) NOT NULL,
    [Datum]         DATE         NOT NULL,
    [Kezdo_ido]     DECIMAL (18) NOT NULL,
    [Befejezo_ido]  DECIMAL (18) NOT NULL,
    [FelhasznaloID] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [fk_munkaidok_felhasznalok] FOREIGN KEY ([FelhasznaloID]) REFERENCES [dbo].[Felhasznalok] ([id])
);

GO

--Jogk�r�k t�bla l�trehoz�sa
CREATE TABLE [dbo].[Jogkorok] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [FelhasznaloID] INT           NOT NULL,
    [Jogkor]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_jogkorok_Felhasznalok] FOREIGN KEY ([FelhasznaloID]) REFERENCES [dbo].[Felhasznalok] ([id])
);

GO

--Szabads�g t�bla l�trehoz�sa
CREATE TABLE [dbo].[Szabadsagok](
    [id]            INT          IDENTITY (1, 1) NOT NULL,
    [Datum]         DATE         NOT NULL,
	[Tipus]			INT			NOT NULL,
    [FelhasznaloID] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [fk_szabadsagok_felhasznalok] FOREIGN KEY ([FelhasznaloID]) REFERENCES [dbo].[Felhasznalok] ([id])
);

GO

--�nnepnapok t�bla l�trehoz�sa
CREATE TABLE [dbo].[Unnepnapok]
(
	[Id]	INT		IDENTITY (1, 1) NOT NULL,
	[Datum]	DATE	NOT NULL,
    [Tipus] int		NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
);
GO

--Felhaszn�l�k t�bla felt�lt�se
INSERT INTO [dbo].[Felhasznalok] VALUES ('Rendszergazda','admin1234','admin@nyilvantartas.com','0')
INSERT INTO [dbo].[Felhasznalok] VALUES ('Jung Tam�s','Asd123','jungtamas@nyilvantartas.com','7')
INSERT INTO [dbo].[Felhasznalok] VALUES ('Csillag D�vid','Asd456','csillagdavid@nyilvantartas.com','6')
INSERT INTO [dbo].[Felhasznalok] VALUES ('Munkahelyi Vezet�','1','a','8')
INSERT INTO [dbo].[Felhasznalok] VALUES ('User Teszt','1','u','5')

--Jogk�r�k t�bla felt�lt�se (Jogkorok be�ll�t�sa)
INSERT INTO [dbo].[Jogkorok] VALUES ('1','Admin')
INSERT INTO [dbo].[Jogkorok] VALUES ('2','Felhasznalo')
INSERT INTO [dbo].[Jogkorok] VALUES ('3','Felhasznalo')
INSERT INTO [dbo].[Jogkorok] VALUES ('4','Admin')
INSERT INTO [dbo].[Jogkorok] VALUES ('5','Felhasznalo')

--�nnepnapok t�bla felt�lt�se
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 01. 01','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 03. 15','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 04. 10','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 04. 13','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 05. 01','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 06. 01','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 08. 20','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 08. 21','1')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 08. 29','2')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 10. 23','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 11. 01','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 12. 12','2')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 12. 24','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 12. 25','0')
INSERT INTO [dbo].[Unnepnapok] VALUES ('2020. 12. 26','0')