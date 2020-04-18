﻿Imports System.Data.SqlClient
Imports System.Configuration
Public Class SqlConn

    Public con As New SqlConnection
    Public cmd As New SqlCommand

    Public Sub New()
        ReadConnectionString()
        'sqlConnect()
    End Sub

    'Kapcsolat lezárása
    Protected Overrides Sub Finalize()
        If con.State = ConnectionState.Open Then
            Try
                con.Close()
            Catch ex As Exception
            End Try
        End If
    End Sub

    'SQL kapcsolat megnyitása
    Public Sub SqlConnect()
        SqlClose()
        Try
            con.Open()
        Catch ex As Exception
            MsgBox("Az SQL kapcsolat felállítása sikertelen!")
            Application.Exit()
        End Try
    End Sub

    'SQL kapcsolat bezárása
    Public Sub SqlClose()
        If con.State = ConnectionState.Open Then
            Try
                con.Close()
            Catch ex As Exception
            End Try
        End If
    End Sub

    'A ConnectionString betöltése az App.config fájlból
    Private Sub ReadConnectionString()
        Try
            con.ConnectionString = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
        Catch ex As Exception
            MsgBox("Az sql elérési út nem található!")
            Application.Exit()
        End Try
    End Sub

End Class
