Imports System.Data.SqlClient

Public Class Login

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Dim sqlConnection As sqlConn

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        sqlConnection.cmd = sqlConnection.con.CreateCommand()
        sqlConnection.cmd.CommandType = CommandType.Text
        'cmd.CommandText = "SELECT f.Nev, j.Jogkor FROM Felhasznalok f INNER JOIN Jogkorok j ON j.FelhasznaloID = f.id WHERE f.Jelszo = '" + PasswordTextBox.Text & "'"
        sqlConnection.cmd.CommandText = "SELECT f.Nev, f.Email, j.Jogkor FROM Felhasznalok f INNER JOIN Jogkorok j ON j.FelhasznaloID = f.id WHERE (f.Email = '" & UsernameTextBox.Text & "' AND f.Jelszo = '" + PasswordTextBox.Text & "')"
        Dim reader As SqlDataReader
        reader = sqlConnection.cmd.ExecuteReader()
        If reader.Read() Then
            wtForm.user.userName = reader.Item(0)
            wtForm.user.email = reader.Item(1)
            wtForm.user.role = reader.Item(2)
            Me.Close()
        End If
        MsgBox("Hib�s felhaszn�l�n�v vagy jelsz�!", , "Hiba!")
        reader.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
        If wtForm.user.userName = "" Then
            Application.Exit()
            End
        End If
    End Sub

    Private Sub sqlConnect()
        sqlConnection.con.ConnectionString = "Data Source=tcp:5.187.201.97,1433;Initial Catalog=wtDB;Persist Security Info=True;User ID=sa;Password=2SS3BJSDbu"
        If sqlConnection.con.State = ConnectionState.Open Then
            sqlConnection.con.Close()
        End If
        sqlConnection.con.Open()
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sqlConnection = New sqlConn()
    End Sub

    Private Sub PasswordLabel_Click(sender As Object, e As EventArgs) Handles PasswordLabel.Click

    End Sub
End Class
