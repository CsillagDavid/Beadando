
Public Class Login

    Dim sqlConnection As SqlConn
    Dim authentication As New AuthenticationManagement

    'A bejelentkez�s gomb m�k�dtet�se
    Private Sub BtnBejelentkez_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBejelentkez.Click
        Dim user = authentication.Authenticate(TxtFelhasznalonev.Text, TxtJelszo.Text)
        If (user.UserName.Length > 0) Then
            WtForm.User = user
            Me.Close()
        End If
    End Sub

    'A kil�p�s gomb m�k�dtet�se
    Private Sub BtnKilep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKilep.Click
        Me.Close()
        If WtForm.User.userName = "" Then
            Application.Exit()
            End
        End If
    End Sub

    'Alap�rtelmezett Load f�ggv�ny
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sqlConnection = New SqlConn()
    End Sub

    '�res mez�k eset�n az alkalmaz�s bez�r�sa
    Private Sub Login_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If WtForm.User.userName = "" Then
            Application.Exit()
            End
        End If
    End Sub

    'Felhaszn�l� jelsz�cser�j�hez sz�ks�ges funkci�
    Private Sub BtnJelszocsere_Click(sender As Object, e As EventArgs) Handles BtnJelszocsere.Click
        Me.Visible = False
        JelszoForm.ShowDialog()
    End Sub

End Class
