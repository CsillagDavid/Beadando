﻿Imports System.Data.SqlClient

Public Class FelhasznalokManagement

    Private sqlConnection As SqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    'Felhasználó adatainak frissítése vagy új felhasználó beszúrása az adatbázisban
    Public Sub InsertOrUpdate(cella As DataGridViewCellCollection)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateFelhasznalok"
        cmd.Parameters.AddWithValue("@Nev", cella.Item("Nev").Value.ToString())
        If cella.Item("Jelszo").Value = Nothing Then
            cmd.Parameters.AddWithValue("@Jelszo", "Password1")
        Else
            cmd.Parameters.AddWithValue("@Jelszo", cella.Item("Jelszo").Value.ToString())
        End If
        cmd.Parameters.AddWithValue("@Email", cella.Item("Email").Value)
        cmd.Parameters.AddWithValue("@Munkaido", IsInteger(cella.Item("Munkaido").Value.ToString()))
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    'Felhasználó törlése az adatbázisból paraméterek alapján
    Public Sub Delete(nev As String, email As String, id As Integer)
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

    'Felhasználók lekérése az adatbázisból, tárolása
    Public Sub GetFelhasznalok(felhasznaloklista As List(Of Felhasznalok))
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
            felhasznaloklista.Add(felhasznalo)
        End While
        sqlConnection.SqlClose()
    End Sub

    'Jelszócseréhez szükséges adatok betöltése az adatbázisból
    Public Sub GetJelszo(email As String, jelszo As String)
        sqlConnection.SqlConnect()
        Dim sdr As SqlDataReader
        Dim sda As New SqlDataAdapter
        cmd.CommandType = CommandType.Text
        Dim sqlquery As String = "SELECT F.Jelszo, F.Email FROM Felhasznalok F
                                     WHERE F.Email = '" & email & "' AND F.Jelszo = '" & jelszo & "'"
        cmd.CommandText = sqlquery
        cmd.Connection = con
        sda.SelectCommand = cmd
        sdr = cmd.ExecuteReader()
        While sdr.Read
            email = sdr.Item("Email")
            jelszo = sdr.Item("Jelszo")
        End While
        sqlConnection.SqlClose()
    End Sub

    'Új jelszó beszúrása
    Public Sub UpdateJelszo(email As String, jelszo As String)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "UpdateJelszo"
        cmd.Parameters.AddWithValue("@Email", email)
        cmd.Parameters.AddWithValue("@Jelszo", jelszo)
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

End Class
