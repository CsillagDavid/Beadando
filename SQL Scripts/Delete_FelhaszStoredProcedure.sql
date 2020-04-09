USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'DeleteFelhasznalok' 
)
   DROP PROCEDURE [dbo].[DeleteFelhasznalok]
GO

CREATE PROCEDURE [dbo].[DeleteFelhasznalok]
	@Email nvarchar
AS
	begin tran
		if exists (select * from [dbo].[Felhasznalok] 
			where Email=@Email)
		begin
		   delete from [dbo].[Felhasznalok]
				where Email=@Email
		end
	commit tran
RETURN 0
GO