﻿Imports System.Data.SqlClient

Public Class FelhasznalokManagement
    Private sqlConnection As sqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Function GetAllNevAndEmail(Email As String) As DataTable
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT F.Nev, F.Email FROM Felhasznalok F
                                            WHERE F.Munkaido >" & 0
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
        cmd.CommandText = "InsertOrUpdateFelhasznalok"
        cmd.Parameters.AddWithValue("@id", Cells.Item("id").Value.ToString())
        cmd.Parameters.AddWithValue("@Nev", Cells.Item("Nev").Value.ToString())
        If Cells.Item("Jelszo").Value.ToString() = "" Then
            cmd.Parameters.AddWithValue("@Jelszo", "Password1")
        Else
            cmd.Parameters.AddWithValue("@Jelszo", Cells.Item("Jelszo").Value.ToString())
        End If
        cmd.Parameters.AddWithValue("@Email", Cells.Item("Email").Value)
        cmd.Parameters.AddWithValue("@Munkaido", isInteger(Cells.Item("Munkaido").Value.ToString()))
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub

    Public Function GetAll() As DataTable
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT F.id, F.Nev, F.Jelszo, F.Email, F.Munkaido FROM Felhasznalok F
                                     WHERE F.Munkaido >" & 0
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
        Return dt
    End Function

End Class