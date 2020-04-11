USE Nyilvantartas
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'InsertOrUpdateMunkaidok' 
)
   DROP PROCEDURE [dbo].[InsertOrUpdateMunkaidok]
GO

CREATE PROCEDURE [dbo].[InsertOrUpdateMunkaidok]
	@Datum date, 
	@Kezdo_Ido decimal (18,0), 
	@Befejezo_Ido decimal (18,0), 
	@FelhasznaloID int
AS
	begin tran
		if exists (select * from [dbo].[Munkaidok] 
			where Datum = @Datum AND FelhasznaloID = @FelhasznaloID)
		begin
		   update [dbo].[Munkaidok] 
		   set 
				Datum = @Datum, 
				Kezdo_ido = @Kezdo_Ido, 
				Befejezo_ido = @Befejezo_Ido
		   where Datum = @Datum AND FelhasznaloID = @FelhasznaloID
		end
		else
		begin
		   insert into [dbo].[Munkaidok] (Datum, Kezdo_ido, Befejezo_ido, FelhasznaloID) 
		   values (@Datum, @Kezdo_Ido, @Befejezo_Ido, @FelhasznaloID)
		end
	commit tran
RETURN 0
GO