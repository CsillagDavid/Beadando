Imports System.Data.SqlClient

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
        chkJsg()
        intYrMh()
    End Sub

    Private Sub chkJsg() 'A bejelentkezett felhasználó jogkörének a lekérdezése, és a program ezáltali indítása
        Select Case user.role
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

    Private Sub getRltMko(tabla As DataGridView) 'A napi ledolgozott órák kiszámítása, valamint a havi összesített munkaidő számolása
        Try
            Dim cmsCount As Integer
            Dim tryPre As Decimal
            Dim mkoSum As Decimal
            cmsCount = tabla.Columns.Count
            tabla.Columns(cmsCount - 1).ReadOnly = False
            For index = 0 To tabla.Rows.Count - 2
                tabla.Item(cmsCount - 1, index).Value = tabla.Item(cmsCount - 2, index).Value - tabla.Item(cmsCount - 3, index).Value
            Next
            For index = 0 To tabla.Rows.Count - 2
                If (Decimal.TryParse(tabla.Item(cmsCount - 1, index).Value, tryPre)) Then
                    mkoSum += Decimal.Parse(tabla.Item(cmsCount - 1, index).Value)
                End If
            Next
            txtMunkaidoOsszes.Text = mkoSum & " óra"
            tabla.Columns(cmsCount - 1).ReadOnly = True
        Catch ex As Exception
            Console.WriteLine("A munkaidő kiszámításában hiba lépett fel!")
        End Try
    End Sub

    Private Sub getFszhMko(email As String) 'A kiválasztott felhasználó adott havi munkaidejének a lekérdezése
        Dim command As String
        If chxAkt.Checked Then
            Dim yr, mh As String
            yr = DateTime.Now.ToString("yyyy/MM")
            Dim newDate As DateTime = DateTime.Now.AddMonths(1)
            mh = newDate.ToString("yyyy/MM")
            command = "SELECT M.id, Datum, Kezdo_ido, Befejezo_ido FROM Munkaidok M
                            INNER JOIN Felhasznalok F
                            ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & email & "' AND M.Datum >= '" & yr & ". 01' AND M.Datum <= '" & mh & ". 01'"
        Else
            command = "SELECT M.id, Datum, Kezdo_ido, Befejezo_ido FROM Munkaidok M
                            INNER JOIN Felhasznalok F
                            ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & email & "'"
        End If
        dgvTabla.DataSource = sqlCmd(command)
        dgvTabla.Columns(0).Visible = False
        dgvTabla.Columns(1).HeaderText = "Dátum"
        dgvTabla.Columns(2).HeaderText = "Kezdés"
        dgvTabla.Columns(3).HeaderText = "Befejezés"
        dgvTabla.Columns.Add("Napi_ido", "Napi munkaidő")
        getRltMko(dgvTabla)
        dgvTabla.Columns(1).ReadOnly = True
        dgvTabla.Columns(4).ReadOnly = True
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
        Return dt
    End Function

    Private Sub getFszhLtb() 'A felhasználók kiválasztásához szükséges ListBox feltöltése
        ltbFelhasznalok.DataSource = sqlCmd("SELECT nev,email FROM Felhasznalok")
        ltbFelhasznalok.DisplayMember = "nev"
        ltbFelhasznalok.ValueMember = "email"
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
        cmd.Parameters.AddWithValue("date", Cells.Item(1).Value)
        cmd.Parameters.AddWithValue("beginTime", Cells.Item(2).Value)
        cmd.Parameters.AddWithValue("endTime", Cells.Item(3).Value)
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
        MsgBox("Edited: " & Cells.Item(1).Value & " " & Cells.Item(2).Value & " " & Cells.Item(3).Value & " " & Cells.Item(4).Value)
    End Sub

    Private Sub dgvTabla_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTabla.CellEndEdit
        Try
            If Not dgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value.Equals(dgvTabla.Rows.Item(e.RowIndex).Tag) Then
                If Not editedRows.Contains(e.RowIndex) Then
                    editedRows.Add(e.RowIndex)
                End If
                getRltMko(dgvTabla)
            End If
            dgvTabla.Rows.Item(e.RowIndex).Tag = ""
        Catch ex As Exception
            Console.WriteLine("Hiba a cellamódosításban.")
        End Try

    End Sub

    Private Sub btnMunkaidoleker_Click(sender As Object, e As EventArgs) Handles btnMunkaidoleker.Click
        getFszhMko(ltbFelhasznalok.SelectedValue)
    End Sub

    Private Sub dgvTabla_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvTabla.CellBeginEdit
        dgvTabla.Rows.Item(e.RowIndex).Tag = dgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value
        getDate()
    End Sub

    Private Sub getDate()
        Try
            Dim cmsCount As Integer
            Dim rowCount As Integer
            cmsCount = dgvTabla.Columns.Count
            rowCount = dgvTabla.Rows.Count
            dgvTabla.Columns(cmsCount - 4).ReadOnly = False
            dgvTabla.Item(cmsCount - 4, rowCount - 1).Value = DateTime.Now.ToString("yyyy/MM/dd") & "."
            dgvTabla.Columns(cmsCount - 4).ReadOnly = True
        Catch ex As Exception

        End Try

    End Sub

    Private Sub intYrMh()
        Dim yr, mh As Integer
        yr = DateTime.Now.Year
        mh = 12
        cmbHonap.Items.Add("Összes")
        For aktYr = yr To 2000 Step -1
            cmbEv.Items.Add(aktYr)
        Next
        For aktMh = 1 To mh
            cmbHonap.Items.Add(aktMh)
        Next
        cmbEv.SelectedIndex = 0
        cmbHonap.SelectedIndex = 0
    End Sub

    Private Sub stpUjTabla(email As String)
        Select Case user.role
            Case "Admin"
                dgvUj.DataSource = sqlCmd("SELECT F.Nev, F.Email, F.Munkaido, M.Datum, M.Kezdo_ido, M.Befejezo_ido FROM Felhasznalok F
                                    INNER JOIN Munkaidok M
                                    ON F.id = M.FelhasznaloID")
            Case "Felhasznalo"
                dgvUj.DataSource = sqlCmd("SELECT F.Nev, F.Email, F.Munkaido, M.Datum, M.Kezdo_ido, M.Befejezo_ido FROM Felhasznalok F
                                    INNER JOIN Munkaidok M
                                    ON F.id = M.FelhasznaloID 
                                    WHERE F.Email = '" & email & "'")
        End Select
        dgvUj.Columns(0).HeaderText = "Név"
        dgvUj.Columns(1).HeaderText = "E-Mail"
        dgvUj.Columns(2).HeaderText = "Munkaidő"
        dgvUj.Columns(3).HeaderText = "Dátum"
        dgvUj.Columns(4).HeaderText = "Kezdő idő"
        dgvUj.Columns(5).HeaderText = "Befejező idő"
        dgvUj.Columns.Add("Napi_ido", "Napi munkaidő")
        getRltMko(dgvUj)
        txtMunkaidoOsszes.Visible = False
        lblMunkaidoOsszes.Visible = False
    End Sub

    Private Sub getMkossz()
        Dim res, selYr, selMh, cmsCount, rowCount, miSum, mkido, kul As Integer
        Dim allMh = ""
        Dim row As String()
        Dim munkanap = 22
        Dim nevLista As New List(Of String)
        Dim aktDate As DateTime
        cmsCount = dgvUj.Columns.Count
        rowCount = dgvUj.Rows.Count
        dgvTabla.Columns.Clear()
        dgvTabla.Columns.Add("nev", "Név")
        dgvTabla.Columns.Add("email", "E-Mail")
        dgvTabla.Columns.Add("eloirtora", "Előírt óraszám")
        dgvTabla.Columns.Add("teljesora", "Teljesített óraszám")
        dgvTabla.Columns.Add("kulonbseg", "Különbség")
        If Int32.TryParse(cmbEv.SelectedItem.ToString(), res) Then
            selYr = Int32.Parse(cmbEv.SelectedItem.ToString())
        Else
            selYr = DateTime.Now.Year
        End If
        If Int32.TryParse(cmbHonap.SelectedItem.ToString(), res) Then
            selMh = Int32.Parse(cmbHonap.SelectedItem.ToString())
        Else
            allMh = cmbHonap.SelectedItem.ToString()
        End If
        Dim lsindex = 0
        For index = 0 To rowCount - 1
            aktDate = Convert.ToDateTime(dgvUj.Item(cmsCount - 4, index).Value)
            If aktDate.Year = selYr Then
                If dgvTabla.Rows.Count = 1 Then
                    If Int32.TryParse((dgvUj.Item(cmsCount - 5, index).Value * munkanap), res) Then
                        mkido = Int32.Parse(dgvUj.Item(cmsCount - 5, index).Value * munkanap)
                    End If
                    If allMh = "Összes" Then
                        miSum += dgvUj.Item(cmsCount - 1, index).Value
                    End If
                    If aktDate.Month = selMh Then
                        If Int32.TryParse(dgvUj.Item(cmsCount - 1, index).Value, res) Then
                            miSum += dgvUj.Item(cmsCount - 1, index).Value
                        End If
                    Else
                        miSum = 0
                    End If
                        kul = miSum - mkido
                    nevLista.Add(dgvUj.Item(cmsCount - 7, index).Value.ToString())
                    row = New String() {
                                dgvUj.Item(cmsCount - 7, index).Value.ToString(),
                                dgvUj.Item(cmsCount - 6, index).Value.ToString(),
                                mkido,
                                miSum,
                                kul
                            }
                    dgvTabla.Rows.Add(row)
                Else
                    If nevLista.Contains(dgvUj.Item(cmsCount - 7, index).Value.ToString()) Then
                        If Int32.TryParse(dgvTabla.Item(3, lsindex).Value.ToString(), res) And Int32.TryParse(dgvUj.Item(cmsCount - 1, index).Value.ToString(), res) Then
                            dgvTabla.Item(3, lsindex).Value = Int32.Parse(dgvTabla.Item(3, lsindex).Value.ToString()) + Int32.Parse(dgvUj.Item(cmsCount - 1, index).Value.ToString())
                            If Int32.TryParse(dgvTabla.Item(4, lsindex).Value.ToString(), res) Then
                                dgvTabla.Item(4, lsindex).Value = Int32.Parse(dgvTabla.Item(3, lsindex).Value) - Int32.Parse(dgvTabla.Item(2, lsindex).Value)
                            End If

                        End If
                    Else
                        nevLista.Add(dgvUj.Item(cmsCount - 7, index).Value.ToString())
                        If allMh = "Összes" Then
                            miSum += dgvUj.Item(cmsCount - 1, index).Value
                        End If
                        If aktDate.Month = selMh Then
                            If Int32.TryParse(dgvUj.Item(cmsCount - 1, index).Value, res) Then
                                miSum += dgvUj.Item(cmsCount - 1, index).Value
                            End If
                        Else
                            miSum = 0
                        End If
                        If Int32.TryParse((dgvUj.Item(cmsCount - 5, index).Value * munkanap), res) Then
                            mkido = Int32.Parse(dgvUj.Item(cmsCount - 5, index).Value * munkanap)
                        End If
                        kul = miSum - mkido
                        row = New String() {
                                dgvUj.Item(cmsCount - 7, index).Value.ToString(),
                                dgvUj.Item(cmsCount - 6, index).Value.ToString(),
                                mkido,
                                miSum,
                                kul
                            }
                        lsindex += 1
                        dgvTabla.Rows.Add(row)
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub btnMunkaidoossz_Click(sender As Object, e As EventArgs) Handles btnMunkaidoossz.Click
        stpUjTabla(ltbFelhasznalok.SelectedValue)
        getMkossz()
    End Sub
End Class
