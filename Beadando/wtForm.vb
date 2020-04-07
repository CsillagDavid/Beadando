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
            Dim result As Decimal
            Dim mkoSum As Decimal
            Dim rowCount = tabla.Rows.Count
            tabla.Columns("napi_ido").ReadOnly = False
            For index = 0 To rowCount - 2
                tabla.Item("napi_ido", index).Value = tabla.Item("befejezo_ido", index).Value - tabla.Item("kezdo_ido", index).Value
            Next
            For index = 0 To tabla.Rows.Count - 2
                If (Decimal.TryParse(tabla.Item("napi_ido", index).Value, result)) Then
                    mkoSum += Decimal.Parse(tabla.Item("napi_ido", index).Value)
                End If
            Next
            txtMunkaidoOsszes.Visible = True
            lblMunkaidoOsszes.Visible = True
            txtMunkaidoOsszes.Text = mkoSum & " óra"
            tabla.Columns("napi_ido").ReadOnly = True
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
        dgvTabla.Columns(1).Name = "datum"
        dgvTabla.Columns(2).HeaderText = "Kezdés"
        dgvTabla.Columns(2).Name = "kezdo_ido"
        dgvTabla.Columns(3).HeaderText = "Befejezés"
        dgvTabla.Columns(3).Name = "befejezo_ido"
        dgvTabla.Columns.Add("napi_ido", "Napi munkaidő")
        getRltMko(dgvTabla)
        dgvTabla.Columns(1).ReadOnly = True
        dgvTabla.Columns(4).ReadOnly = True
    End Sub

    Private Sub getFszh() 'A felhasználók adatainak lekérdezése az SQL Adatbázisból
        dgvTabla.DataSource = sqlCmd("SELECT nev,email,munkaido FROM Felhasznalok
                                     WHERE munkaido >" & 0)
        dgvTabla.Columns(0).HeaderText = "Név"
        dgvTabla.Columns(0).Name = "nev"
        dgvTabla.Columns(1).HeaderText = "E-Mail"
        dgvTabla.Columns(1).Name = "email"
        dgvTabla.Columns(2).HeaderText = "Munkaidő"
        dgvTabla.Columns(2).Name = "munkaido"
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
        ltbFelhasznalok.DataSource = sqlCmd("SELECT nev,email FROM Felhasznalok
                                            WHERE Munkaido >" & 0)
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
            Dim rowCount = dgvTabla.Rows.Count
            dgvTabla.Columns("datum").ReadOnly = False
            dgvTabla.Item("datum", rowCount - 1).Value = DateTime.Now.ToString("yyyy/MM/dd") & "."
            dgvTabla.Columns("datum").ReadOnly = True
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
        Dim command = ""
        Select Case user.role
            Case "Admin"
                command = "SELECT F.Nev, F.Email, F.Munkaido, M.Datum, M.Kezdo_ido, M.Befejezo_ido FROM Felhasznalok F
                                    INNER JOIN Munkaidok M
                                    ON F.id = M.FelhasznaloID"
            Case "Felhasznalo"
                command = "SELECT F.Nev, F.Email, F.Munkaido, M.Datum, M.Kezdo_ido, M.Befejezo_ido FROM Felhasznalok F
                                    INNER JOIN Munkaidok M
                                    ON F.id = M.FelhasznaloID 
                                    WHERE F.Email = '" & email & "'"
        End Select
        dgvUj.DataSource = sqlCmd(command)
        dgvUj.Columns(0).HeaderText = "Név"
        dgvUj.Columns(0).Name = "nev"
        dgvUj.Columns(1).HeaderText = "E-Mail"
        dgvUj.Columns(1).Name = "email"
        dgvUj.Columns(2).HeaderText = "Munkaidő"
        dgvUj.Columns(2).Name = "munkaido"
        dgvUj.Columns(3).HeaderText = "Dátum"
        dgvUj.Columns(3).Name = "datum"
        dgvUj.Columns(4).HeaderText = "Kezdő idő"
        dgvUj.Columns(4).Name = "kezdo_ido"
        dgvUj.Columns(5).HeaderText = "Befejező idő"
        dgvUj.Columns(5).Name = "befejezo_ido"
        dgvUj.Columns.Add("napi_ido", "Napi munkaidő")
        getRltMko(dgvUj)
        dgvUj.ReadOnly = True
        txtMunkaidoOsszes.Visible = False
        lblMunkaidoOsszes.Visible = False
    End Sub

    Private Function getMkIdomiSum(index As Integer)
        Dim selMh, selYr, result, munkaIdo, napiIdo, munkanap As Integer
        Dim allMh As String
        Dim dateRes As Date
        Dim miSumIdo() = {0, 0}
        Dim aktDate As DateTime
        munkanap = 22
        allMh = ""
        If Int32.TryParse(dgvUj.Item("napi_ido", index).Value, result) Then
            napiIdo = result
        End If
        If Int32.TryParse((dgvUj.Item("munkaido", index).Value), result) Then
            munkaIdo = munkanap * result
        End If
        If Date.TryParse(dgvUj.Item("datum", index).Value, dateRes) Then
            aktDate = Convert.ToDateTime(dateRes)
        Else
            aktDate = DateTime.Now
        End If
        If Int32.TryParse(cmbEv.SelectedItem.ToString(), result) Then
            selYr = result
        Else
            selYr = DateTime.Now.Year
        End If
        If aktDate.Year = selYr Then
            If Int32.TryParse(cmbHonap.SelectedItem.ToString(), result) Then
                selMh = result
            Else
                allMh = cmbHonap.SelectedItem.ToString()
            End If

            If allMh = "Összes" Then
                miSumIdo(0) += napiIdo
            ElseIf aktDate.Month = selMh Then
                miSumIdo(0) += napiIdo
            Else
                miSumIdo(0) = 0
            End If
            miSumIdo(1) = munkaIdo
        Else
            miSumIdo(0) = 0
            miSumIdo(1) = 0
        End If
        Return miSumIdo
    End Function

    Private Sub getMkossz()
        Dim miSumIdo() = {0, 0}
        Dim result, rowCount, kulonbseg, teljesora, eloirtora As Integer
        Dim nev, email As String
        Dim row As String()
        Dim nevLista As New List(Of String)
        Dim lsindex = 0
        rowCount = dgvUj.Rows.Count
        dgvTabla.DataSource = Nothing
        dgvTabla.Columns.Add("nev", "Név")
        dgvTabla.Columns.Add("email", "E-Mail")
        dgvTabla.Columns.Add("eloirtora", "Előírt óraszám")
        dgvTabla.Columns.Add("teljesora", "Teljesített óraszám")
        dgvTabla.Columns.Add("kulonbseg", "Különbség")
        For index = 0 To rowCount - 2
            nev = dgvUj.Item("nev", index).Value
            email = dgvUj.Item("email", index).Value
            If dgvTabla.Rows.Count = 1 Then
                miSumIdo = getMkIdomiSum(index)
                kulonbseg = miSumIdo(0) - miSumIdo(1)
                nevLista.Add(nev)
                row = New String() {
                            nev,
                            email,
                            miSumIdo(1),
                            miSumIdo(0),
                            kulonbseg
                        }
                dgvTabla.Rows.Add(row)
            Else
                If nevLista.Contains(nev) Then
                    If Int32.TryParse(dgvTabla.Item("eloirtora", lsindex).Value, result) Then
                        eloirtora = result
                    Else
                        eloirtora = 0
                    End If
                    If Int32.TryParse(dgvTabla.Item("teljesora", lsindex).Value, result) Then
                        teljesora = result
                    Else
                        teljesora = 0
                    End If
                    If Int32.TryParse(dgvTabla.Item("kulonbseg", lsindex).Value, result) Then
                        kulonbseg = result
                    Else
                        kulonbseg = 0
                    End If
                    miSumIdo = getMkIdomiSum(index)
                    dgvTabla.Item("teljesora", lsindex).Value = teljesora + miSumIdo(0)
                    dgvTabla.Item("kulonbseg", lsindex).Value = (teljesora + miSumIdo(0)) - eloirtora
                Else
                    miSumIdo = getMkIdomiSum(index)
                    nevLista.Add(nev)
                    kulonbseg = miSumIdo(0) - miSumIdo(1)
                    row = New String() {
                            nev,
                            email,
                            miSumIdo(1),
                            miSumIdo(0),
                            kulonbseg
                        }
                    lsindex += 1
                    dgvTabla.Rows.Add(row)
                End If
            End If
        Next
    End Sub
    Private Sub btnMunkaidoossz_Click(sender As Object, e As EventArgs) Handles btnMunkaidoossz.Click
        stpUjTabla(ltbFelhasznalok.SelectedValue)
        getMkossz()
    End Sub
End Class
