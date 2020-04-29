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
    Public Sub InsertOrUpdate(cella As DataGridViewCellCollection)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateSzabadsagok"
        cmd.Parameters.AddWithValue("@Datum", IsDate(cella.Item("Datum").Value))
        cmd.Parameters.AddWithValue("@Tavollet", cella.Item("Tavollet").Value)
        cmd.Parameters.AddWithValue("@FelhasznaloID", IsInteger(cella.Item("FelhasznaloID").Value))
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
    Public Sub GetSzabadsagok(szabadsagoklista As List(Of Szabadsagok), email As String, ev As String, honap As String)
        Dim sdr As SqlDataReader
        Dim sda As New SqlDataAdapter
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        Dim sqlquery As String = "SELECT Sz.Datum, Sz.Tavollet, Sz.FelhasznaloID FROM Szabadsagok Sz                   
                                INNER JOIN Felhasznalok F
                                ON Sz.FelhasznaloID = F.id
                                WHERE F.Email = '" & email & "' AND Sz.Datum >= '" & ev & ". " & honap & ". 01' 
                                AND Sz.Datum < '" & ev & ". " & (honap + 1) & ". 01'"
        cmd.CommandText = sqlquery
        sda.SelectCommand = cmd
        sdr = cmd.ExecuteReader()
        While sdr.Read()
            Dim szabadsag = New Szabadsagok(
                sdr.Item("Datum"),
                sdr.Item("Tavollet"),
                sdr.Item("FelhasznaloID"))
            szabadsagoklista.Add(szabadsag)
        End While
        sqlConnection.SqlClose()
    End Sub

End Class
