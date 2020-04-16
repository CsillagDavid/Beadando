
Public Class Login

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Dim sqlConnection As SqlConn
    Dim authentication As New AuthenticationManagement

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim user = authentication.Authenticate(UsernameTextBox.Text, PasswordTextBox.Text)
        If (user.UserName.Length > 0) Then
            WtForm.User = user
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
        If WtForm.User.userName = "" Then
            Application.Exit()
            End
        End If
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sqlConnection = New SqlConn()
    End Sub

    Private Sub PasswordLabel_Click(sender As Object, e As EventArgs) Handles PasswordLabel.Click

    End Sub

    Private Sub Login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If WtForm.User.userName = "" Then
            Application.Exit()
            End
        End If
    End Sub
End Class
