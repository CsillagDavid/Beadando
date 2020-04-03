Imports System.Data.SqlClient
Imports Nancy.Json
Imports System.Configuration
Public Class sqlConn
    Public con As New SqlConnection
    Public cmd As New SqlCommand

    Public Sub New()
        readConnectionString()
        sqlConnect()
    End Sub

    Protected Overrides Sub Finalize()
        If con.State = ConnectionState.Open Then
            Try
                con.Close()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub sqlConnect()
        sqlClose()
        Try
            con.Open()
        Catch ex As Exception
            MsgBox("Az SQL kapcsolat felállítása sikertelen!")
            Application.Exit()
        End Try
    End Sub

    Public Sub sqlClose()
        If con.State = ConnectionState.Open Then
            Try
                con.Close()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Function readConnectionString()
        Try
            con.ConnectionString = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
        Catch ex As Exception
            MsgBox("Az sql elérési út nem található!")
            Application.Exit()
        End Try
    End Function

End Class
