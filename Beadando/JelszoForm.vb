Public Class JelszoForm

    Dim fhszMan As New FelhasznalokManagement

    'Jelszó megváltoztatása
    Private Sub BtnCsere_Click(sender As Object, e As EventArgs) Handles BtnCsere.Click
        Try
            Dim email = TxtEmail.Text
            Dim jelszo = TxtJelenlegi.Text
            fhszMan.GetJelszo(email, jelszo)
            If TxtUj.Text = TxtMegerosites.Text Then
                fhszMan.UpdateJelszo(email, TxtUj.Text)
                TxtUj.Text = ""
                TxtMegerosites.Text = ""
                TxtEmail.Text = ""
                TxtJelenlegi.Text = ""
            Else
                MsgBox("A két jelszó nem egyezik!")
                TxtUj.Text = ""
                TxtMegerosites.Text = ""
            End If
            Me.Close()
            Login.Visible = True
        Catch ex As Exception
            MsgBox("Hibás e-mail cím!")
        End Try
    End Sub

    'Visszalépés a login felületre
    Private Sub BtnVissza_Click(sender As Object, e As EventArgs) Handles BtnVissza.Click
        Me.Close()
        Login.Visible = True
    End Sub

End Class