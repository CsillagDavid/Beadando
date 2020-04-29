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

    'A bejelentkezésnél az email és a jelszó ellenőrzése és a bejelentkezés engedélyezése
    Public Function Authenticate(felhasznalonev As String, jelszo As String) As User
        Dim user = New User()
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT F.Nev, F.Email, J.Jogkor FROM Felhasznalok F 
                            INNER JOIN Jogkorok J 
                            ON J.FelhasznaloID = F.id 
                            WHERE (F.Email = '" & felhasznalonev & "' AND F.Jelszo = '" + jelszo & "')"
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