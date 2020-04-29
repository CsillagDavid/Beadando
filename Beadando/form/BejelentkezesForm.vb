
Public Class Login

    Dim sqlConnection As SqlConn
    Dim authentication As New AuthenticationManagement

    'A bejelentkezés gomb mûködtetése
    Private Sub BtnBejelentkez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBejelentkez.Click
        Dim user = authentication.Authenticate(TxtFelhasznalonev.Text, TxtJelszo.Text)
        If (user.UserName.Length > 0) Then
            WtForm.User = user
            Me.Close()
        End If
        TxtFelhasznalonev.Text = ""
        TxtJelszo.Text = ""
    End Sub

    'A kilépés gomb mûködtetése
    Private Sub BtnKilep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKilep.Click
        Me.Close()
        If WtForm.User.userName = "" Then
            Application.Exit()
            End
        End If
    End Sub

    'Alapértelmezett Load függvény
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sqlConnection = New SqlConn()
    End Sub

    'Üres mezõk esetén az alkalmazás bezárása
    Private Sub Login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If WtForm.User.userName = "" Then
            Application.Exit()
            End
        End If
    End Sub

    'Felhasználó jelszócseréjéhez szükséges funkció
    Private Sub BtnJelszocsere_Click(sender As Object, e As EventArgs) Handles BtnJelszocsere.Click
        Me.Visible = False
        JelszoForm.ShowDialog()
    End Sub

End Class
