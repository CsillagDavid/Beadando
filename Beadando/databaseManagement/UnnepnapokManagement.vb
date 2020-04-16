Imports System.Data.SqlClient

Public Class UnnepnapokManagement
    Private sqlConnection As sqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Sub GetUnnepnapok(list As List(Of Unnepnapok))
        sqlConnection.sqlConnect()
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
            list.Add(unnepnap)
        End While
        sqlConnection.sqlClose()
    End Sub

    Public Sub InsertOrUpdate(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateUnnepnapok"
        cmd.Parameters.AddWithValue("@Datum", isDate(Cells.Item("Datum").Value))
        cmd.Parameters.AddWithValue("@Tipus", isInteger(Cells.Item("Tipus").Value))
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub

    Public Sub DeleteUnnepnap(datum As DateTime)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteUnnepnapok"
        cmd.Parameters.AddWithValue("@Datum", datum)
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub

End Class
