Imports System.Data.SqlClient
Imports System.Configuration
Public Class SqlConn

    Public con As New SqlConnection
    Public cmd As New SqlCommand
    'ReadOnly connectionString = "Data Source=tcp:5.187.197.206,1433;Initial Catalog=Nyilvantartas;Persist Security Info=True;User ID=nyilvantartasdb;Password=Nyilvan1234"
    ReadOnly connectionString = "Data Source=gamer-pc\sqlhome;Initial Catalog=Nyilvantartas;Persist Security Info=True;User ID=nyilvantartasdb;Password=Nyilvan1234"

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
            con.ConnectionString = connectionString
        Catch ex As Exception
            MsgBox("Az sql elérési út nem található!")
            Application.Exit()
        End Try
    End Sub

End Class
