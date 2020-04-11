USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'InsertOrUpdateFelhasznalok' 
)
   DROP PROCEDURE [dbo].[InsertOrUpdateFelhasznalok]
GO

CREATE PROCEDURE [dbo].[InsertOrUpdateFelhasznalok]
	@id int, 
	@Nev nvarchar (255), 
	@Jelszo nvarchar (255), 
	@Email nvarchar (255), 
	@Munkaido int
AS
	begin tran
		if exists (select * from [dbo].[Felhasznalok] 
			where Nev = @Nev OR Email = @Email)
		begin
		   update [dbo].[Felhasznalok] 
		   set 
				Nev = @Nev, 
				Email = @Email, 
				Munkaido = @Munkaido
		   where Nev = @Nev OR Email = @Email
		end
		else
		begin
		   insert into [dbo].[Felhasznalok](Nev, Jelszo, Email, Munkaido) 
		   values (@Nev, @Jelszo, @Email, @Munkaido)
		end
	commit tran
RETURN 0
GO