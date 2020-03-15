Public Class wtForm
    Private Sub wtForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim db As New Database
        db.sqlconnection()

    End Sub
End Class
