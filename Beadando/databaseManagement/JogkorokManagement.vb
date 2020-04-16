Imports System.Data.SqlClient

Public Class JogkorokManagement
    Private sqlConnection As SqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Sub UpdateJogkorok(Cells As DataGridViewCellCollection)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "UpdateJogkorok"
        cmd.Parameters.AddWithValue("@FelhasznaloID", Cells.Item("FelhasznaloID").Value)
        cmd.Parameters.AddWithValue("@Jogkor", Cells.Item("Jogkor").Value)
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    Public Sub InsertJogkorok(tabla As DataGridView)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertJogkorok"
        For index = 0 To tabla.Rows.Count - 2
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@FelhasznaloID", tabla.Item("id", index).Value)
            cmd.Parameters.AddWithValue("@Jogkor", "Beosztott")
            cmd.ExecuteNonQuery()
        Next
        sqlConnection.SqlClose()
    End Sub

    Public Sub getJogkorok(list As List(Of Jogkorok))
        sqlConnection.SqlConnect()
        Dim sdr As SqlDataReader
        Dim sda As New SqlDataAdapter
        cmd.CommandType = CommandType.Text
        Dim sqlquery As String = "SELECT J.FelhasznaloID, J.Jogkor FROM Jogkorok J"
        cmd.CommandText = sqlquery
        cmd.Connection = con
        sda.SelectCommand = cmd
        sdr = cmd.ExecuteReader()
        While sdr.Read
            Dim jogkor = New Jogkorok(
                sdr.Item("FelhasznaloID"),
                sdr.Item("Jogkor")
                )
            list.Add(jogkor)
        End While
        sqlConnection.SqlClose()
    End Sub

End Class
