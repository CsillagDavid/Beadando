Imports System.Data.SqlClient

Public Class wtForm

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim sqlConnection As sqlConn
    Dim editedRows As List(Of Integer) = New List(Of Integer)
    Public Property user = New User()
    Dim userEmail As String

    Private Sub wtForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Login.ShowDialog()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
        intEvHonap()
        chkJsg()
    End Sub

    Private Sub chkJsg() 'A bejelentkezett felhasználó jogkörének a lekérdezése, és a program ezáltali indítása
        Select Case user.role
            Case "Admin"
                getFszhLtb()
                If Not user.userName = "Rendszergazda" Then
                    getAlapMk(user.email)
                End If
            Case "Felhasznalo"
                ltbFelhasznalok.Enabled = False
                btnFelhasznalok.Enabled = False
                getFszhMko(user.email)
                getAlapMk(user.email)
                userEmail = user.email
        End Select
    End Sub

    Private Sub getRltMko(tabla As DataGridView, index As Integer) 'A napi ledolgozott órák kiszámítása, valamint a havi összesített munkaidő számolása
        Try

            Dim result, kezdo, befejezo, mkoSum As Decimal
            tabla.Columns("napi_ido").ReadOnly = False
            If Decimal.TryParse(tabla.Item("befejezo_ido", index).Value, result) Then
                befejezo = result
            End If
            If Decimal.TryParse(tabla.Item("kezdo_ido", index).Value, result) Then
                kezdo = result
            End If
            If Decimal.TryParse(txtMunkaidoOsszes.Text, result) Then
                mkoSum = result
            End If
            If tabla.Item("tavollet", index).Value = "Szabadság" Then
                tabla.Item("napi_ido", index).Value = 0
                tabla.Item("befejezo_ido", index).Value = 0
                tabla.Item("kezdo_ido", index).Value = 0
            Else
                tabla.Item("napi_ido", index).Value = befejezo - kezdo
            End If
            mkoSum += (befejezo - kezdo)
            txtMunkaidoOsszes.Visible = True
            lblMunkaidoOsszes.Visible = True
            txtMunkaidoOsszes.Text = mkoSum
            tabla.Columns("napi_ido").ReadOnly = True
        Catch ex As Exception
            Console.WriteLine("A munkaidő kiszámításában hiba lépett fel!")
        End Try
    End Sub

    Private Function intComboBox()
        Dim tavolletBox As New DataGridViewComboBoxCell
        tavolletBox.Items.Add("Szabadság")
        tavolletBox.Items.Add("Betegség")
        tavolletBox.Items.Add("Fizetetlen szabadság")
        Return tavolletBox
    End Function

    Private Function getEvHonap()
        Dim result As Integer
        Dim lista As New Dictionary(Of String, Integer)
        If Int32.TryParse(cmbEv.SelectedItem.ToString(), result) Then
            lista.Add("ev", result)
        Else
            lista.Add("ev", DateTime.Now.Year)
        End If
        If Int32.TryParse(cmbHonap.SelectedItem.ToString(), result) Then
            lista.Add("honap", result)
        Else
            lista.Add("honap", 0)
        End If
        Return lista
    End Function

    Private Sub getFszhMko(email As String) 'A kiválasztott felhasználó adott havi munkaidejének a lekérdezése
        Dim evhonap = getEvHonap()
        Dim command = "SELECT Datum, Kezdo_ido, Befejezo_ido, FelhasznaloID FROM Munkaidok M
                            INNER JOIN Felhasznalok F
                            ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & email & "' AND M.Datum >= '" & evhonap.Item("ev") & ". " & evhonap.Item("honap") & ". 01' 
                            AND M.Datum < '" & evhonap.Item("ev") & ". " & (evhonap.Item("honap") + 1) & ". 01'"
        dgvTabla.DataSource = sqlCmd(command)
        dgvTabla.Columns("Datum").HeaderText = "Dátum"
        dgvTabla.Columns("Datum").ValueType = GetType(Date)
        dgvTabla.Columns("Kezdo_ido").HeaderText = "Kezdés"
        dgvTabla.Columns("Kezdo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("Befejezo_ido").HeaderText = "Befejezés"
        dgvTabla.Columns("Befejezo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns.Add("napi_ido", "Napi munkaidő")
        dgvTabla.Columns("napi_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("FelhasznaloID").Visible = False
        dgvTabla.Columns("FelhasznaloID").ReadOnly = True
        dgvTabla.Columns.Add("tavollet", "Távollét")
        txtMunkaidoOsszes.Text = 0
        For index = 0 To dgvTabla.Rows.Count - 2
            dgvTabla.Item("tavollet", index) = intComboBox()
            getRltMko(dgvTabla, index)
        Next
        dgvTabla.Columns("Datum").ReadOnly = True
        dgvTabla.Columns("napi_ido").ReadOnly = True
        btnMentes.Enabled = True
        btnTorles.Enabled = True
    End Sub

    Private Sub getAlapMk(email As String)
        Dim datum, napStr, honapStr As String
        Dim intResult, napok, munkaido, felhaszid, ev, honap, rowCount As Integer
        Dim kido, bido, decResult As Decimal
        Dim row As String()
        Dim dateResult As DateTime
        Dim evhonap = getEvHonap()
        dgvUj.DataSource = Nothing
        dgvUj.Columns.Clear()
        dgvUj.Rows.Clear()
        Dim command = "SELECT M.Datum, F.Munkaido, M.Kezdo_ido, M.Befejezo_ido, F.id FROM Munkaidok M
                   INNER JOIN Felhasznalok F
                   ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & email & "' AND M.Datum >= '" & evhonap.Item("ev") & ". " & evhonap.Item("honap") & ". 01' 
                            AND M.Datum < '" & evhonap.Item("ev") & ". " & (evhonap.Item("honap") + 1) & ". 01'"
        dgvUj.DataSource = sqlCmd(command)
        rowCount = dgvUj.Rows.Count
        If rowCount = 1 Then
            command = "SELECT F.id, F.Munkaido FROM Felhasznalok F
                            WHERE F.Email = '" & email & "'"
            dgvUj.DataSource = Nothing
            dgvUj.Columns.Clear()
            dgvUj.Rows.Clear()
            dgvUj.DataSource = sqlCmd(command)
        End If
        If Int32.TryParse(dgvUj.Item("munkaido", 0).Value, intResult) Then
            munkaido = intResult
        Else
            munkaido = 0
        End If
        If Int32.TryParse(dgvUj.Item("id", 0).Value, intResult) Then
            felhaszid = intResult
        Else
            felhaszid = 0
        End If
        dgvTabla.DataSource = Nothing
        dgvTabla.Columns.Clear()
        dgvTabla.Rows.Clear()
        dgvTabla.Columns.Add("Datum", "Dátum")
        dgvTabla.Columns.Add("Kezdo_ido", "Kezdő idő")
        dgvTabla.Columns.Add("Befejezo_ido", "Befejező idő")
        dgvTabla.Columns.Add("FelhasznaloID", "Felhasználó ID")
        dgvTabla.Columns("Datum").ValueType = GetType(Date)
        dgvTabla.Columns("Kezdo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("Befejezo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("FelhasznaloID").ValueType = GetType(Integer)
        ev = evhonap.Item("ev").ToString()
        honap = evhonap.Item("honap").ToString()
        napok = DateTime.DaysInMonth(evhonap.Item("ev"), evhonap.Item("honap"))
        Dim datumindex = 0
        For index = 1 To napok
            If honap < 10 Then
                honapStr = "0" & honap
            Else
                honapStr = honap
            End If
            If index < 10 Then
                napStr = "0" & index
            Else
                napStr = index
            End If
            datum = ev & ". " & honapStr & ". " & napStr
            If Date.TryParse(datum, dateResult) Then
                kido = 8
                bido = kido + munkaido
                If rowCount > 1 Then
                    If Not dgvUj.Item("datum", datumindex).Value = dateResult Then
                        row = {
                                dateResult,
                                kido,
                                bido,
                                felhaszid
                             }
                    Else
                        dateResult = dgvUj.Item("datum", datumindex).Value
                        kido = dgvUj.Item("kezdo_ido", datumindex).Value
                        bido = dgvUj.Item("befejezo_ido", datumindex).Value
                        felhaszid = dgvUj.Item("id", datumindex).Value
                        row = {
                                dateResult,
                                kido,
                                bido,
                                felhaszid
                            }
                        datumindex += 1
                    End If
                Else
                    row = {
                            dateResult,
                            kido,
                            bido,
                            felhaszid
                        }
                End If
                dgvTabla.Rows.Add(row)
            End If
        Next
        rowCount = dgvTabla.Rows.Count
        cmd = con.CreateCommand()
        cmd.CommandText = "InsetIntoMunkaidok"
        cmd.CommandType = CommandType.StoredProcedure
        sqlConnection.sqlConnect()
        For index = 0 To rowCount - 2
            cmd.Parameters.Clear()
            If Date.TryParse(dgvTabla.Item("datum", index).Value, dateResult) Then
                cmd.Parameters.AddWithValue("@Datum", dateResult)
            End If
            If Decimal.TryParse(dgvTabla.Item("kezdo_ido", index).Value, decResult) Then
                cmd.Parameters.AddWithValue("@Kezdo_ido", decResult)
            End If
            If Decimal.TryParse(dgvTabla.Item("befejezo_ido", index).Value, decResult) Then
                cmd.Parameters.AddWithValue("@Befejezo_ido", decResult)
            End If
            If Integer.TryParse(dgvTabla.Item("felhasznaloid", index).Value, intResult) Then
                cmd.Parameters.AddWithValue("@FelhasznaloID", intResult)
            End If
            cmd.ExecuteNonQuery()
        Next
        sqlConnection.sqlClose()
        'dgvTabla.DataSource = Nothing
        'dgvTabla.Columns.Clear()
        'dgvTabla.Rows.Clear()
    End Sub

    Private Sub getFszh() 'A felhasználók adatainak lekérdezése az SQL Adatbázisból
        dgvTabla.DataSource = sqlCmd("SELECT nev,email,munkaido FROM Felhasznalok
                                     WHERE munkaido >" & 0)
        dgvTabla.Columns("nev").HeaderText = "Név"
        dgvTabla.Columns("email").HeaderText = "E-Mail"
        dgvTabla.Columns("munkaido").HeaderText = "Munkaidő"
        btnMentes.Enabled = True
        btnTorles.Enabled = True
        txtMunkaidoOsszes.Visible = False
        lblMunkaidoOsszes.Visible = False
        lblOra.Visible = False
    End Sub

    Private Function sqlCmd(command As String) 'A táblázat feltöltése parancs megadásával
        dgvTabla.Columns.Clear()
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = command
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
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
        Dim dateResult As DateTime
        Dim decResult As Decimal
        Dim intResult As Integer
        If Date.TryParse(Cells.Item("datum").Value, dateResult) Then
            cmd.Parameters.AddWithValue("@Datum", dateResult)
        End If
        If Decimal.TryParse(Cells.Item("kezdo_ido").Value, decResult) Then
            cmd.Parameters.AddWithValue("@Kezdo_ido", decResult)
        End If
        If Decimal.TryParse(Cells.Item("befejezo_ido").Value, decResult) Then
            cmd.Parameters.AddWithValue("@Befejezo_ido", decResult)
        End If
        If Integer.TryParse(Cells.Item("felhasznaloid").Value, intResult) Then
            cmd.Parameters.AddWithValue("@FelhasznaloID", intResult)
        End If
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
        MsgBox("Edited: " & Cells.Item("datum").Value & " " & Cells.Item("kezdo_ido").Value & " " & Cells.Item("befejezo_ido").Value & " " & Cells.Item("napi_ido").Value)
    End Sub

    Private Sub dgvTabla_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTabla.CellEndEdit
        Try
            If Not dgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value.Equals(dgvTabla.Rows.Item(e.RowIndex).Tag) Then
                If Not editedRows.Contains(e.RowIndex) Then
                    editedRows.Add(e.RowIndex)
                End If
                Dim rowCount = dgvTabla.Rows.Count
                txtMunkaidoOsszes.Text = 0
                For index = 0 To rowCount - 2
                    getRltMko(dgvTabla, index)
                Next
            End If
            dgvTabla.Rows.Item(e.RowIndex).Tag = ""
        Catch ex As Exception
            Console.WriteLine("Hiba a cellamódosításban.")
        End Try

    End Sub

    Private Sub btnMunkaidoleker_Click(sender As Object, e As EventArgs) Handles btnMunkaidoleker.Click
        Select Case user.role
            Case "Admin"
                getFszhMko(ltbFelhasznalok.SelectedValue)
            Case "Felhasznalo"
                getFszhMko(userEmail)
        End Select
    End Sub

    Private Sub dgvTabla_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvTabla.CellBeginEdit
        dgvTabla.Rows.Item(e.RowIndex).Tag = dgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value
        'getDate()
    End Sub

    Private Sub getDate()
        Try
            Dim rowCount = dgvTabla.Rows.Count
            dgvTabla.Columns("datum").ReadOnly = False
            dgvTabla.Item("datum", rowCount - 1).Value = DateTime.Now.ToString("yyyy/MM/dd") & "."
            dgvTabla.Columns("datum").ReadOnly = True
            dgvTabla.Item("tavollet", rowCount - 1) = intComboBox()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub intEvHonap() 'Az év és hónap kiválasztó feltöltése értékekkel
        Dim yr, mh As Integer
        yr = DateTime.Now.Year
        mh = DateTime.Now.Month
        For aktYr = yr To 2000 Step -1
            cmbEv.Items.Add(aktYr)
        Next
        For aktMh = 1 To 12
            cmbHonap.Items.Add(aktMh)
        Next
        cmbEv.SelectedIndex = 0
        cmbHonap.SelectedIndex = (mh - 1)
    End Sub

    Private Sub stpUjTabla()
        Dim command = ""
        Dim evhonap = getEvHonap()
        dgvUj.DataSource = Nothing
        dgvUj.Columns.Clear()
        dgvUj.Rows.Clear()
        Select Case user.role
            Case "Admin"
                command = "SELECT F.Nev, F.Email, F.Munkaido, M.Datum, M.Kezdo_ido, M.Befejezo_ido FROM Felhasznalok F
                                    INNER JOIN Munkaidok M
                                    ON F.id = M.FelhasznaloID
                                    WHERE M.Datum >= '" & evhonap.Item("ev") & ". " & evhonap.Item("honap") & ". 01' 
                                    AND M.Datum <= '" & evhonap.Item("ev") & ". " & (evhonap.Item("honap") + 1) & ". 01'"
            Case "Felhasznalo"
                command = "SELECT F.Nev, F.Email, F.Munkaido, M.Datum, M.Kezdo_ido, M.Befejezo_ido FROM Felhasznalok F
                                    INNER JOIN Munkaidok M
                                    ON F.id = M.FelhasznaloID 
                                    WHERE F.Email = '" & userEmail & "' AND M.Datum >= '" & evhonap.Item("ev") & ". " & evhonap.Item("honap") & ". 01' 
                                    AND M.Datum <= '" & evhonap.Item("ev") & ". " & (evhonap.Item("honap") + 1) & ". 01'"
        End Select
        dgvUj.DataSource = sqlCmd(command)
        dgvUj.Columns.Add("napi_ido", "Napi munkaidő")
        dgvUj.Columns.Add("tavollet", "Távollét")
        For index = 0 To dgvUj.Rows.Count - 2
            dgvUj.Item("tavollet", index) = intComboBox()
            getRltMko(dgvUj, index)
        Next
        dgvUj.ReadOnly = True
        txtMunkaidoOsszes.Visible = False
        lblOra.Visible = False
        lblMunkaidoOsszes.Visible = False
    End Sub

    Private Function getMunkaidoNapiido(index As Integer) 'Az összes munkaidő és a különbség kiszámítása
        Dim result, munkaIdo, napiIdo, munkanap As Integer
        Dim dateRes As Date
        Dim lista As New Dictionary(Of String, Integer)
        Dim aktDate As DateTime
        Dim evhonap = getEvHonap()
        munkanap = 22

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

        If aktDate.Year = evhonap.Item("ev") Then
            If aktDate.Month = evhonap.Item("honap") Then
                lista.Add("mkido", munkaIdo)
                lista.Add("npido", napiIdo)
            Else
                napiIdo = 0
                munkaIdo = 0
            End If
        End If
        Return lista
    End Function

    Private Sub getMkossz() 'Az összesített munkaidők megjelenítése egy táblázatban
        Dim result, kulonbseg, teljesora, eloirtora As Integer
        Dim nev, email As String
        Dim row As String()
        Dim nevLista As New List(Of String)
        Dim lsindex = 0
        dgvTabla.ReadOnly = False
        dgvTabla.DataSource = Nothing
        dgvTabla.Columns.Clear()
        dgvTabla.Rows.Clear()
        dgvTabla.Columns.Add("nev", "Név")
        dgvTabla.Columns.Add("email", "E-Mail")
        dgvTabla.Columns.Add("eloirtora", "Előírt óraszám")
        dgvTabla.Columns.Add("teljesora", "Teljesített óraszám")
        dgvTabla.Columns.Add("kulonbseg", "Különbség")
        For index = 0 To dgvUj.Rows.Count - 2
            nev = dgvUj.Item("nev", index).Value
            email = dgvUj.Item("email", index).Value
            Dim evhonap = getMunkaidoNapiido(index)
            Dim napiIdo = evhonap.Item("npido")
            Dim munkaIdo = evhonap.Item("mkido")
            If dgvTabla.Rows.Count = 1 Then
                kulonbseg = napiIdo - munkaIdo
                nevLista.Add(nev)
                row = New String() {
                            nev,
                            email,
                            munkaIdo,
                            napiIdo,
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
                    dgvTabla.Item("teljesora", lsindex).Value = teljesora + napiIdo
                    dgvTabla.Item("kulonbseg", lsindex).Value = (teljesora + napiIdo) - eloirtora
                Else
                    nevLista.Add(nev)
                    kulonbseg = napiIdo - munkaIdo
                    row = New String() {
                            nev,
                            email,
                            munkaIdo,
                            napiIdo,
                            kulonbseg
                        }
                    lsindex += 1
                    dgvTabla.Rows.Add(row)
                End If
            End If
        Next
        dgvTabla.ReadOnly = True
    End Sub

    Private Sub btnMunkaidoossz_Click(sender As Object, e As EventArgs) Handles btnMunkaidoossz.Click
        stpUjTabla()
        getMkossz()
        btnMentes.Enabled = False
        btnTorles.Enabled = False
    End Sub

    Private Sub tstButton_Click(sender As Object, e As EventArgs) Handles tstButton.Click
        Select Case user.role
            Case "Admin"
                getAlapMk(ltbFelhasznalok.SelectedValue)
            Case "Felhasznalo"
                getAlapMk(userEmail)
        End Select
    End Sub
End Class
