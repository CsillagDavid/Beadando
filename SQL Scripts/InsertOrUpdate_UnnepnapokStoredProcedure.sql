USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'InsertOrUpdateUnnepnapok' 
)
   DROP PROCEDURE [dbo].[InsertOrUpdateUnnepnapok]
GO

CREATE PROCEDURE [dbo].[InsertOrUpdateUnnepnapok]
	@Datum date, 
	@Tipus int
AS
	begin tran
		if exists (select * from [dbo].[InsertOrUpdateUnnepnapok] 
			where Datum=@Datum)
		begin
		   update [dbo].[InsertOrUpdateUnnepnapok] 
		   set 
				Datum=@Datum, 
				Tipus=@Tipus
		   where Datum=@Datum
		end
		else
		begin
		   insert into [dbo].[InsertOrUpdateUnnepnapok] (Datum, Tipus) 
		   values (@Datum, @Tipus)
		end
	commit tran
RETURN 0
GO