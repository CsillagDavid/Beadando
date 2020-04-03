Imports System.Data.SqlClient
Imports Nancy.Json
Imports System.Configuration
Public Class sqlConn

    'connectionString="Data Source=GAMER-PC\SQLHOME;Initial Catalog=wtDB;Persist Security Info=True;User ID=sa;Password=2SS3BJSDbu"
    'tcp:5.187.213.233,1433\sqlhome
    Public con As New SqlConnection
    Public cmd As New SqlCommand

    Public Sub New()
        readConnectionString()
        sqlConnect()
    End Sub

    Protected Overrides Sub Finalize()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Public Sub sqlConnect()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
    End Sub

    Private Function readConnectionString()
        Try
            con.ConnectionString = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
        Catch ex As Exception

        End Try
    End Function

End Class
