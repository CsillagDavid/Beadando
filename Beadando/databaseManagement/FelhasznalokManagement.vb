Imports System.Data.SqlClient

Public Class FelhasznalokManagement
    Private sqlConnection As SqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Sub InsertOrUpdate(Cells As DataGridViewCellCollection)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateFelhasznalok"
        cmd.Parameters.AddWithValue("@Nev", Cells.Item("Nev").Value.ToString())
        If Cells.Item("Jelszo").Value = Nothing Then
            cmd.Parameters.AddWithValue("@Jelszo", "Password1")
        Else
            cmd.Parameters.AddWithValue("@Jelszo", Cells.Item("Jelszo").Value.ToString())
        End If
        cmd.Parameters.AddWithValue("@Email", Cells.Item("Email").Value)
        cmd.Parameters.AddWithValue("@Munkaido", IsInteger(Cells.Item("Munkaido").Value.ToString()))
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    Public Sub DeleteFelhasznalok(nev As String, email As String, id As Integer)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteFelhasznalok"
        cmd.Parameters.AddWithValue("@id", id)
        cmd.Parameters.AddWithValue("@Nev", nev)
        cmd.Parameters.AddWithValue("@Email", email)
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub
    Public Sub GetFelhasznalok(list As List(Of Felhasznalok))
        sqlConnection.SqlConnect()
        Dim sdr As SqlDataReader
        Dim sda As New SqlDataAdapter
        cmd.CommandType = CommandType.Text
        Dim sqlquery As String = "SELECT F.id, F.Nev, F.Jelszo, F.Email, F.Munkaido FROM Felhasznalok F
                                     WHERE F.Munkaido >" & 0
        cmd.CommandText = sqlquery
        cmd.Connection = con
        sda.SelectCommand = cmd
        sdr = cmd.ExecuteReader()
        While sdr.Read
            Dim felhasznalo = New Felhasznalok(
                sdr.Item("id"),
                sdr.Item("Nev"),
                sdr.Item("Jelszo"),
                sdr.Item("Email"),
                sdr.Item("Munkaido"))
            list.Add(felhasznalo)
        End While
        sqlConnection.SqlClose()
    End Sub

End Class
