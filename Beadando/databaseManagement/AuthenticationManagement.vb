Imports System.Data.SqlClient

Public Class AuthenticationManagement
    Private sqlConnection As SqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Function Authenticate(UserName As String, Password As String) As User
        Dim user = New User()
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT f.Nev, f.Email, j.Jogkor FROM Felhasznalok f INNER JOIN Jogkorok j ON j.FelhasznaloID = f.id WHERE (f.Email = '" & UserName & "' AND f.Jelszo = '" + Password & "')"
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            user.UserName = reader.Item(0)
            user.Email = reader.Item(1)
            user.Role = reader.Item(2)
        Else
            MsgBox("Hibás felhasználónév vagy jelszó!", , "Hiba!")
        End If
        reader.Close()
        sqlConnection.SqlClose()
        Return user
    End Function
End Class