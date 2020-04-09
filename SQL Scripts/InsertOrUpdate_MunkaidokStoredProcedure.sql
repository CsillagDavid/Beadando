USE wtDB
GO

CREATE PROCEDURE InsertOrUpdateMunkaidok @Datum date, @Kezdo_Ido decimal, @Befejezo_Ido decimal, @FelhasznaloID integer
AS
	begin tran
		if exists (select * from Munkaidok where Datum=@Datum AND FelhasznaloID=@FelhasznaloID)
		begin
		   update Munkaidok set Datum=@Datum, Kezdo_ido=@Kezdo_Ido, Befejezo_ido=@Befejezo_Ido
		   where Datum=@Datum AND FelhasznaloID=@FelhasznaloID
		end
		else
		begin
		   insert into Munkaidok (Datum, Kezdo_ido, Befejezo_ido, FelhasznaloID) values (@Datum, @Kezdo_Ido, @Befejezo_Ido, @FelhasznaloID)
		end
	commit tran
RETURN 0