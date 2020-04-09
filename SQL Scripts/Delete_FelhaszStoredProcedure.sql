USE wtDB
GO

CREATE PROCEDURE DeleteFelhasznalok @Email nvarchar
AS
	begin tran
		if exists (select * from Felhasznalok where Email=@Email)
		begin
		   delete from Felhasznalok
		   where Email=@Email
		end
	commit tran
RETURN 0