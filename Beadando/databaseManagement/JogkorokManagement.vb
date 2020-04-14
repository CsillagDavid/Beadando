Imports System.Data.SqlClient

Public Class JogkorokManagement
    Private sqlConnection As sqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Sub Update(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "UpdateJogkorok"
        cmd.Parameters.AddWithValue("@FelhasznaloID", Cells.Item("FelhasznaloID").Value)
        cmd.Parameters.AddWithValue("@Jogkor", Cells.Item("Jogkor").Value)
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub

    Public Function GetIds() As DataTable
        sqlConnection.sqlConnect()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT F.id FROM Felhasznalok F
                        WHERE F.Munkaido > " & 0
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
        Return dt
    End Function

    Public Sub InsertJogkorok(dgvUj As DataGridView)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertJogkorok"
        For index = 0 To dgvUj.Rows.Count - 2
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@FelhasznaloID", dgvUj.Item("id", index).Value)
            cmd.Parameters.AddWithValue("@Jogkor", "Felhasznalo")
            cmd.ExecuteNonQuery()
        Next
        sqlConnection.sqlClose()
    End Sub
End Class
