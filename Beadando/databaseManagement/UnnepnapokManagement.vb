Imports System.Data.SqlClient

Public Class UnnepnapokManagement

    Private sqlConnection As SqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    'Új ünnepnap felvétele az adatbázisba, vagy egy meglévő módosítása
    Public Sub InsertOrUpdate(Cells As DataGridViewCellCollection)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateUnnepnapok"
        cmd.Parameters.AddWithValue("@Datum", IsDate(Cells.Item("Datum").Value))
        cmd.Parameters.AddWithValue("@Tipus", IsInteger(Cells.Item("Tipus").Value))
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    'Ünnepnapok törlésére szolgáló függvény
    Public Sub DeleteUnnepnap(datum As DateTime)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteUnnepnapok"
        cmd.Parameters.AddWithValue("@Datum", datum)
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    'Az ünnepnapok elemeinek lekérdezése az adatbázisból, majd ezeket egy ünnepnapok típusú listában tárolása
    Public Sub GetUnnepnapok(lista As List(Of Unnepnapok))
        sqlConnection.SqlConnect()
        Dim sdr As SqlDataReader
        Dim sda As New SqlDataAdapter
        cmd.CommandType = CommandType.Text
        Dim sqlquery As String = "SELECT U.Datum, U.Tipus FROM Unnepnapok U"
        cmd.CommandText = sqlquery
        cmd.Connection = con
        sda.SelectCommand = cmd
        sdr = cmd.ExecuteReader()
        While sdr.Read
            Dim unnepnap = New Unnepnapok(
                sdr.Item("Datum"),
                sdr.Item("Tipus")
                )
            lista.Add(unnepnap)
        End While
        sqlConnection.SqlClose()
    End Sub

End Class
