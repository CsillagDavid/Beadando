USE wtDB
GO

CREATE PROCEDURE InsertOrUpdateFelhasznalok @id int, @Nev nvarchar, @Jelszo nvarchar, @Email nvarchar, @Munkaido int
AS
	begin tran
		if exists (select * from Felhasznalok where Email=@Email)
		begin
		   update Felhasznalok set Nev=@Nev, Email=@Email, Munkaido=@Munkaido
		   where id=@id
		end
		else
		begin
		   insert into Felhasznalok(Nev, Jelszo, Email, Munkaido) values (@Nev, @Jelszo, @Email, @Munkaido)
		end
	commit tran
RETURN 0