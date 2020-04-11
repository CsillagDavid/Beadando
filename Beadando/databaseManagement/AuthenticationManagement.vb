Imports System.Data.SqlClient

Public Class AuthenticationManagement
    Private sqlConnection As sqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Function authenticate(UserName As String, Password As String) As User
        Dim user = New User()
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT f.Nev, f.Email, j.Jogkor FROM Felhasznalok f INNER JOIN Jogkorok j ON j.FelhasznaloID = f.id WHERE (f.Email = '" & UserName & "' AND f.Jelszo = '" + Password & "')"
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            user.userName = reader.Item(0)
            user.email = reader.Item(1)
            user.role = reader.Item(2)
        Else
            MsgBox("Hibás felhasználónév vagy jelszó!", , "Hiba!")
        End If
        reader.Close()
        sqlConnection.sqlClose()
        Return user
    End Function
End Class