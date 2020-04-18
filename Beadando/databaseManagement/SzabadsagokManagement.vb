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

    'A szabadságok lekérése az adatbázisból és ebből lista készítése
    Public Function GetSzabadsagok() As List(Of Szabadsagok)
        Dim lista As New List(Of Szabadsagok)
        Dim sdr As SqlDataReader
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT Sz.Datum, Sz.Tipus, Sz.FelhasznaloID FROM Szabadsagok Sz"
        sdr = cmd.ExecuteReader()
        While sdr.Read()
            Dim szabadsag = New Szabadsagok(
                sdr.Item("Datum"),
                sdr.Item("Tipus"),
                sdr.Item("FelhasznaloID"))
            lista.Add(szabadsag)
        End While
        sqlConnection.SqlClose()
        Return lista
    End Function

End Class
