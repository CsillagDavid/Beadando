USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'DeleteUnnepnapok' 
)
   DROP PROCEDURE [dbo].[DeleteUnnepnapok]
GO

CREATE PROCEDURE [dbo].[DeleteUnnepnapok]
	@Datum date
AS
	begin tran
		if exists (select * from [dbo].[Unnepnapok] 
			where Datum = @Datum)
		begin
			delete from [dbo].[Unnepnapok]
			where Datum = @Datum
		end	
	commit tran
RETURN 0
GO