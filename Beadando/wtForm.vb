﻿Imports System.Data.SqlClient

Public Class wtForm

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim sqlConnection As sqlConn
    Public Property user = New User()

    Private Sub wtForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Login.ShowDialog()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
        autentCheck()
    End Sub
    Private Sub autentCheck()
        Select Case user.role
            Case "Admin"

            Case "Felhasznalo"
                ltbFelhasznalok.Enabled = False
                lblFelhasznalok.Visible = False
                ltbFelhasznalok.Visible = False
                btnFelhasznalok.Enabled = False
                btnFelhasznalok.Visible = False
                btnMunkaidoleker.Enabled = False
                btnMunkaidoleker.Visible = False
                getMunkaido()
                szamolNapi()
                szamolHavi()
        End Select
    End Sub
    Private Sub szamolNapi()
        Try
            For index = 0 To dgvTabla.Rows.Count - 2
                dgvTabla.Item(3, index).Value = dgvTabla.Item(2, index).Value - dgvTabla.Item(1, index).Value
            Next
        Catch ex As Exception
            MsgBox("Napi számláló megdöglött")
        End Try

    End Sub

    Private Sub szamolHavi()
        Try
            Dim osszeg As Decimal
            Dim akt As Decimal
            For index = 0 To dgvTabla.Rows.Count - 2
                If (Decimal.TryParse(dgvTabla.Item(3, index).Value, akt)) Then
                    osszeg += Decimal.Parse(dgvTabla.Item(3, index).Value)
                End If
            Next
            txtMunkaidoOsszes.Text = osszeg & " óra"
        Catch ex As Exception
            MsgBox("Havi számláló megdöglött")
        End Try
    End Sub
    Private Sub getMunkaido()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT Datum, Kezdo_ido, Befejezo_ido FROM Munkaidok M
                            INNER JOIN Felhasznalok F
                            ON M.FelhasznaloID = F.id
                            WHERE F.Email = " & "'" & email & "'"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        dgvTabla.DataSource = dt
        dgvTabla.Columns(0).HeaderText = "Dátum"
        dgvTabla.Columns(1).HeaderText = "Kezdés"
        dgvTabla.Columns(2).HeaderText = "Befejezés"
        dgvTabla.Columns.Add("Napi_ido", "Napi munkaidő")
        con.Close()
    End Sub
    Private Sub loadFelhasznalok()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT nev,email,munkaido FROM Felhasznalok"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        dgvTabla.DataSource = dt
        dgvTabla.Columns(0).HeaderText = "Név"
        dgvTabla.Columns(1).HeaderText = "E-Mail"
        dgvTabla.Columns(2).HeaderText = "Munkaidő"
        con.Close()
    End Sub

    Private Sub btnFelhasznalok_Click(sender As Object, e As EventArgs) Handles btnFelhasznalok.Click
        loadFelhasznalok()
    End Sub

    Private Sub btnMentes_Click(sender As Object, e As EventArgs) Handles btnMentes.Click

    End Sub

    Private Sub dgvTabla_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTabla.CellEndEdit
        MsgBox("Edited!")
    End Sub
End Class
