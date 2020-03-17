Imports System.Data.SqlClient

Public Class sqlConn

    'connectionString="Data Source=GAMER-PC\SQLHOME;Initial Catalog=wtDB;Persist Security Info=True;User ID=sa;Password=2SS3BJSDbu"
    'tcp:5.187.213.233,1433\sqlhome
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand

    Public Sub sqlConnect()
        con.ConnectionString = "Data Source=GAMER-PC\SQLHOME;Initial Catalog=wtDB;Persist Security Info=True;User ID=sa;Password=2SS3BJSDbu"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
    End Sub

    Public Function getUsers()
        Dim asd As String
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM Felhasznalok"
        cmd.ExecuteNonQuery()

        Return asd
    End Function

End Class
