
Imports System.Data.SqlClient

Public Class wtForm

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand

    Private Sub wtForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        sqlConnect()


    End Sub

    'connectionString="Data Source=GAMER-PC\SQLHOME;Initial Catalog=wtDB;Persist Security Info=True;User ID=sa;Password=2SS3BJSDbu"
    'tcp:5.187.213.233,1433\sqlhome

    Private Sub initltbFelhasz()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT id,nev FROM Felhasznalok"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)


        sda.Fill(dt)
    End Sub
    Private Sub sqlConnect()
        con.ConnectionString = "Data Source=tcp:5.187.201.97,1433;Initial Catalog=wtDB;Persist Security Info=True;User ID=sa;Password=2SS3BJSDbu"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
    End Sub

    Private Sub btnFelhasznalok_Click(sender As Object, e As EventArgs) Handles btnFelhasznalok.Click
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT id,nev,email,munkaido FROM Felhasznalok"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        dgvFelhasznalok.DataSource = dt
        dgvFelhasznalok.Columns(0).HeaderText = "ID"
        dgvFelhasznalok.Columns(1).HeaderText = "Név"
        dgvFelhasznalok.Columns(2).HeaderText = "E-Mail"
        dgvFelhasznalok.Columns(3).HeaderText = "Munkaidő"
    End Sub

    Private Sub btnMunkaidő_Click(sender As Object, e As EventArgs) Handles btnMunkaidő.Click

    End Sub
End Class
