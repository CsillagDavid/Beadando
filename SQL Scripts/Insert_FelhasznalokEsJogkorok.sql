USE wtDB
GO

--Felhaszn�l�k t�bla felt�lt�se
INSERT INTO dbo.Felhasznalok VALUES ('Rendszergazda','admin1234','admin@wtdb.com','0')
INSERT INTO dbo.Felhasznalok VALUES ('Jung Tam�s','Asd123','jungt@wtdb.com','7')
INSERT INTO dbo.Felhasznalok VALUES ('Csillag D�vid','Asd456','csdavid@wtdb.com','6')
INSERT INTO dbo.Felhasznalok VALUES ('Admin Teszt','1','a','8')
INSERT INTO dbo.Felhasznalok VALUES ('User Teszt','1','u','5')

--Jogk�r�k be�ll�t�sa
INSERT INTO dbo.Jogkorok VALUES ('1','Admin')
INSERT INTO dbo.Jogkorok VALUES ('2','Felhasznalo')
INSERT INTO dbo.Jogkorok VALUES ('3','Felhasznalo')
INSERT INTO dbo.Jogkorok VALUES ('4','Admin')
INSERT INTO dbo.Jogkorok VALUES ('5','Felhasznalo')