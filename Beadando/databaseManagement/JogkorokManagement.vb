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

    Public Sub UpdateJogkorok(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "UpdateJogkorok"
        cmd.Parameters.AddWithValue("@FelhasznaloID", Cells.Item("FelhasznaloID").Value)
        cmd.Parameters.AddWithValue("@Jogkor", Cells.Item("Jogkor").Value)
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub

    Public Sub InsertJogkorok(tabla As DataGridView)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertJogkorok"
        For index = 0 To tabla.Rows.Count - 2
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@FelhasznaloID", tabla.Item("id", index).Value)
            cmd.Parameters.AddWithValue("@Jogkor", "Felhasznalo")
            cmd.ExecuteNonQuery()
        Next
        sqlConnection.sqlClose()
    End Sub

    Public Function getJogrok() As DataTable
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT F.Nev, J.FelhasznaloID, J.Jogkor FROM Jogkorok J
                        INNER JOIN Felhasznalok F
                        ON F.id = J.FelhasznaloID
                        WHERE F.Munkaido > " & 0
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
        Return dt
    End Function

End Class
