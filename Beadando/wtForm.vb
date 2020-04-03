Imports System.Data.SqlClient
Imports Beadando.sqlConn

Public Class wtForm

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim sqlConnection As sqlConn
    Dim editedRows As List(Of Integer) = New List(Of Integer)
    Public Property user = New User()

    Private Sub wtForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Login.ShowDialog()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
        autentCheck()
        sqlConnect()
        chkJsg(user.role)
    End Sub

    Private Sub chkJsg(logined As String) 'A bejelentkezett felhasználó jogkörének a lekérdezése, és a program ezáltali indítása
        Select Case logined
            Case "Admin"
                getFszhLtb()
            Case "Felhasznalo"
                ltbFelhasznalok.Enabled = False
                lblFelhasznalok.Visible = False
                ltbFelhasznalok.Visible = False
                btnFelhasznalok.Enabled = False
                btnFelhasznalok.Visible = False
                btnMunkaidoleker.Enabled = False
                btnMunkaidoleker.Visible = False
                getFszhMko(user.email)
        End Select
    End Sub

    Private Sub getRltMko() 'A napi ledolgozott órák kiszámítása, valamint a havi összesített munkaidő számolása
        Try
            For index = 0 To dgvTabla.Rows.Count - 2
                dgvTabla.Item(3, index).Value = dgvTabla.Item(2, index).Value - dgvTabla.Item(1, index).Value
            Next
            Dim osszeg As Decimal
            Dim akt As Decimal
            For index = 0 To dgvTabla.Rows.Count - 2
                If (Decimal.TryParse(dgvTabla.Item(3, index).Value, akt)) Then
                    osszeg += Decimal.Parse(dgvTabla.Item(3, index).Value)
                End If
            Next
            txtMunkaidoOsszes.Text = osszeg & " óra"
        Catch ex As Exception
            Console.WriteLine("A munkaidő kiszámításában hiba lépett fel!")
            'MsgBox("A munkaidő kiszámításában hiba lépett fel!")
        End Try
    End Sub
    Private Sub getFszhMko(email As String) 'A kiválasztott felhasználó adott havi munkaidejének a lekérdezése
        dgvTabla.DataSource = sqlCmd("SELECT Datum, Kezdo_ido, Befejezo_ido FROM Munkaidok M
                            INNER JOIN Felhasznalok F
                            ON M.FelhasznaloID = F.id
                            WHERE F.Email = " & "'" & email & "'")
        dgvTabla.Columns(0).HeaderText = "Dátum"
        dgvTabla.Columns(0).ReadOnly = True
        dgvTabla.Columns(1).HeaderText = "Kezdés"
        dgvTabla.Columns(2).HeaderText = "Befejezés"
        dgvTabla.Columns.Add("Napi_ido", "Napi munkaidő")
        getRltMko()
    End Sub
    Private Sub getFszh() 'A felhasználók adatainak lekérdezése az SQL Adatbázisból
        dgvTabla.DataSource = sqlCmd("SELECT nev,email,munkaido FROM Felhasznalok")
        dgvTabla.Columns(0).HeaderText = "Név"
        dgvTabla.Columns(1).HeaderText = "E-Mail"
        dgvTabla.Columns(2).HeaderText = "Munkaidő"
    End Sub
    Private Function sqlCmd(command As String) 'A táblázat feltöltése parancs megadásával
        dgvTabla.Columns.Clear()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = command
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

    'connectionString="Data Source=GAMER-PC\SQLHOME;Initial Catalog=wtDB;Persist Security Info=True;User ID=sa;Password=2SS3BJSDbu"
    'tcp:5.187.213.233,1433\sqlhome
    Private Sub sqlConnect()
        Try
            con.ConnectionString = "Data Source=5.187.196.255,1433;Initial Catalog=wtDB;Persist Security Info=True;User ID=sa;Password=2SS3BJSDbu"
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
        Catch ex As Exception
            MsgBox("SQL Kapcsolat meghiúsult!")
        End Try
    End Sub

    Private Sub btnFelhasznalok_Click(sender As Object, e As EventArgs) Handles btnFelhasznalok.Click
        getFszh()
    End Sub

    Private Sub btnMentes_Click(sender As Object, e As EventArgs) Handles btnMentes.Click
        editedRows.ForEach(Sub(i) saveOrUpdate(dgvTabla.Rows.Item(i).Cells))
        editedRows.Clear()
    End Sub
    Private Sub saveOrUpdate(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "saveOrUpdateMunkaidok"
        cmd.Parameters.AddWithValue("date", Cells.Item(0).Value)
        cmd.Parameters.AddWithValue("beginTime", Cells.Item(1).Value)
        cmd.Parameters.AddWithValue("endTime", Cells.Item(2).Value)
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
        MsgBox("Edited: " & Cells.Item(0).Value & " " & Cells.Item(1).Value & " " & Cells.Item(2).Value & " " & Cells.Item(3).Value)
    End Sub
    Private Sub dgvTabla_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTabla.CellEndEdit
        If Not dgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value.Equals(dgvTabla.Rows.Item(e.RowIndex).Tag) Then
            If Not editedRows.Contains(e.RowIndex) Then
                editedRows.Add(e.RowIndex)
            End If
        End If
        dgvTabla.Rows.Item(e.RowIndex).Tag = ""
    End Sub

    Private Sub dgvTabla_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTabla.CellEndEdit
        getRltMko()
    End Sub

    Private Sub btnMunkaidoleker_Click(sender As Object, e As EventArgs) Handles btnMunkaidoleker.Click
        getFszhMko(ltbFelhasznalok.SelectedValue)
    Private Sub dgvTabla_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvTabla.CellBeginEdit
        dgvTabla.Rows.Item(e.RowIndex).Tag = dgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value
    End Sub
End Class
