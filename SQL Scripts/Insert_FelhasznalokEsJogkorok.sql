USE wtDB
GO

--Felhasználók tábla feltöltése
INSERT INTO dbo.Felhasznalok VALUES ('Rendszergazda','admin1234','admin@wtdb.com','0')
INSERT INTO dbo.Felhasznalok VALUES ('Jung Tamás','Asd123','jungt@wtdb.com','7')
INSERT INTO dbo.Felhasznalok VALUES ('Csillag Dávid','Asd456','csdavid@wtdb.com','6')
INSERT INTO dbo.Felhasznalok VALUES ('Admin Teszt','1','a','8')
INSERT INTO dbo.Felhasznalok VALUES ('User Teszt','1','u','5')

--Jogkörök beállítása
INSERT INTO dbo.Jogkorok VALUES ('1','Admin')
INSERT INTO dbo.Jogkorok VALUES ('2','Felhasznalo')
INSERT INTO dbo.Jogkorok VALUES ('3','Felhasznalo')
INSERT INTO dbo.Jogkorok VALUES ('4','Admin')
INSERT INTO dbo.Jogkorok VALUES ('5','Felhasznalo')