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

    Public Function GetUnnepnapok() As DataTable
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT U.Datum, U.Tipus FROM Unnepnapok U"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
        Return dt
    End Function

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

End Class
