Imports System.Data.SqlClient

Public Class SzabadsagokManagement

    Private sqlConnection As SqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    'Szabadság frissítése vagy új felvétele cella megadásával
    Public Sub InsertOrUpdate(Cells As DataGridViewCellCollection)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateSzabadsagok"
        cmd.Parameters.AddWithValue("@Datum", IsDate(Cells.Item("Datum").Value))
        cmd.Parameters.AddWithValue("@Tavollet", Cells.Item("Tavollet").Value)
        cmd.Parameters.AddWithValue("@FelhasznaloID", IsInteger(Cells.Item("FelhasznaloID").Value))
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    'Szabadság törlése az adatbázisból
    Public Sub Delete(datum As Date, felhasznaloid As String)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteSzabadsagok"
        cmd.Parameters.AddWithValue("@Datum", datum)
        cmd.Parameters.AddWithValue("@FelhasznaloID", felhasznaloid)
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    'A szabadságok lekérése az adatbázisból és ebből lista készítése
    Public Sub GetSzabadsagok(lista As List(Of Szabadsagok), Email As String, KezdoDatum As String, BefejezoDatum As String)
        Dim sdr As SqlDataReader
        Dim sda As New SqlDataAdapter
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        Dim sqlquery As String = "SELECT Sz.Datum, Sz.Tavollet, Sz.FelhasznaloID FROM Szabadsagok Sz                   
                                INNER JOIN Felhasznalok F
                                ON Sz.FelhasznaloID = F.id
                                WHERE F.Email = '" & Email & "' AND Sz.Datum >= '" & KezdoDatum & ". " & BefejezoDatum & ". 01' 
                                AND Sz.Datum < '" & KezdoDatum & ". " & (BefejezoDatum + 1) & ". 01'"
        cmd.CommandText = sqlquery
        sda.SelectCommand = cmd
        sdr = cmd.ExecuteReader()
        While sdr.Read()
            Dim szabadsag = New Szabadsagok(
                sdr.Item("Datum"),
                sdr.Item("Tavollet"),
                sdr.Item("FelhasznaloID"))
            lista.Add(szabadsag)
        End While
        sqlConnection.SqlClose()
    End Sub

End Class
