Imports System.Data.SqlClient

Public Class wtForm
    Dim authentication As New AuthenticationManagement
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim sqlConnection As sqlConn
    Dim editedRows As List(Of Integer) = New List(Of Integer)
    Public Property user = New User()
    Dim userEmail As String
    Dim itemEv = "ev"
    Dim itemHonap = "honap"
    Dim itemMunkaIdo = "munkaido"
    Dim itemNapiIdo = "napiido"
    Dim munkanap As Integer
    Private Sub wtForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Login.ShowDialog()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
        readYearAndMonth()
        checkAuthentication()
        getWeekdaysNumber()
    End Sub
    'Jogosultságkezelő
    Private Sub checkAuthentication() 'A bejelentkezett felhasználó jogkörének a lekérdezése, és a program ezáltali indítása
        Select Case user.role
            Case "Admin"
                getFszhLtb()
                If Not user.userName = "Rendszergazda" Then
                    setDefaultWorkingHours(user.email)
                Else
                    sqlConnection.sqlConnect()
                    lblUnnep.Visible = True
                    lblUnnep.Enabled = True
                    btnUnnep.Visible = True
                    btnUnnep.Enabled = True
                End If
            Case "Felhasznalo"
                ltbFelhasznalok.Enabled = False
                btnFelhasznalok.Enabled = False
                setDefaultWorkingHours(user.email)
                getUserWorkingHours(user.email)
                userEmail = user.email
        End Select
    End Sub

    'Inicializálások és számítási függvények, segédfüggvények
    Private Sub readYearAndMonth() 'Az év és hónap kiválasztó feltöltése értékekkel
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

    Private Sub getWorkTimeofDay(tabla As DataGridView, index As Integer) 'A napi ledolgozott órák kiszámítása, valamint a havi összesített munkaidő számolása
        Try
            Dim kezdo, befejezo, mkoSum As Decimal
            tabla.Columns("napi_ido").ReadOnly = False
            befejezo = isDecimal(tabla.Item("befejezo_ido", index).Value)
            kezdo = isDecimal(tabla.Item("kezdo_ido", index).Value)
            mkoSum = isDecimal(txtMunkaidoOsszes.Text)
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
            lblOra.Visible = True
            tabla.Columns("napi_ido").ReadOnly = True
        Catch ex As Exception
            Console.WriteLine("A munkaidő kiszámításában hiba lépett fel!")
        End Try
    End Sub

    Private Function getYearAndMonth()
        Dim lista As New Dictionary(Of String, Integer)
        lista.Add(itemEv, isInteger(cmbEv.SelectedItem))
        lista.Add(itemHonap, isInteger(cmbHonap.SelectedItem))
        Return lista
    End Function

    Private Function getCommand(value As String, email As String)
        Dim evhonap = getYearAndMonth()
        Select Case value
            Case "Munkaido"
                Return "SELECT M.Datum, M.Kezdo_ido, M.Befejezo_ido, M.FelhasznaloID FROM Munkaidok M
                            INNER JOIN Felhasznalok F
                            ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & email & "' AND M.Datum >= '" & evhonap.Item(itemEv) & ". " & evhonap.Item(itemHonap) & ". 01' 
                            AND M.Datum < '" & evhonap.Item(itemEv) & ". " & (evhonap.Item(itemHonap) + 1) & ". 01'"
            Case "MunkaidokAll"
                Return "SELECT M.Datum, F.Munkaido, M.Kezdo_ido, M.Befejezo_ido, F.id FROM Munkaidok M
                   INNER JOIN Felhasznalok F
                   ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & email & "' AND M.Datum >= '" & evhonap.Item(itemEv) & ". " & evhonap.Item(itemHonap) & ". 01' 
                            AND M.Datum < '" & evhonap.Item(itemEv) & ". " & (evhonap.Item(itemHonap) + 1) & ". 01'"
            Case "IDMunkaido"
                Return "SELECT F.id, F.Munkaido FROM Felhasznalok F
                            WHERE F.Email = '" & email & "'"
            Case "Felhasznalo"
                Return "SELECT F.id, F.Nev, F.Jelszo, F.Email, F.Munkaido FROM Felhasznalok F
                                     WHERE F.Munkaido >" & 0
            Case "FelhasznaloLista"
                Return "SELECT F.Nev, F.Email FROM Felhasznalok F
                                            WHERE F.Munkaido >" & 0
            Case "OsszesitoAdmin"
                Return "SELECT F.Nev, F.Email, F.Munkaido, M.Datum, M.Kezdo_ido, M.Befejezo_ido FROM Felhasznalok F
                                    INNER JOIN Munkaidok M
                                    ON F.id = M.FelhasznaloID
                                    WHERE M.Datum >= '" & evhonap.Item(itemEv) & ". " & evhonap.Item(itemHonap) & ". 01' 
                                    AND M.Datum < '" & evhonap.Item(itemEv) & ". " & (evhonap.Item(itemHonap) + 1) & ". 01'"
            Case "OsszesitoFelhasznalo"
                Return "SELECT F.Nev, F.Email, F.Munkaido, M.Datum, M.Kezdo_ido, M.Befejezo_ido FROM Felhasznalok F
                                    INNER JOIN Munkaidok M
                                    ON F.id = M.FelhasznaloID 
                                    WHERE F.Email = '" & email & "' AND M.Datum >= '" & evhonap.Item(itemEv) & ". " & evhonap.Item(itemHonap) & ". 01' 
                                    AND M.Datum < '" & evhonap.Item(itemEv) & ". " & (evhonap.Item(itemHonap) + 1) & ". 01'"
            Case "Unnepnap"
                Return "SELECT U.Datum, U.Tipus FROM Unnepnapok U"
        End Select
        Return ""
    End Function
    Private Sub InsertMunkaidok(tabla As DataGridView)
        Try
            Dim rowCount = tabla.Rows.Count
            cmd = con.CreateCommand()
            cmd.CommandText = "InsertOrUpdateMunkaidok"
            cmd.CommandType = CommandType.StoredProcedure
            sqlConnection.sqlConnect()
            For index = 0 To rowCount - 2
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@Datum", isDate(dgvTabla.Item("datum", index).Value))
                cmd.Parameters.AddWithValue("@Kezdo_ido", isDecimal(dgvTabla.Item("kezdo_ido", index).Value))
                cmd.Parameters.AddWithValue("@Befejezo_ido", isDecimal(dgvTabla.Item("befejezo_ido", index).Value))
                cmd.Parameters.AddWithValue("@FelhasznaloID", isInteger(dgvTabla.Item("felhasznaloid", index).Value))
                cmd.ExecuteNonQuery()
            Next
            sqlConnection.sqlClose()
        Catch ex As Exception
            Console.WriteLine("Hiba az adatbázis frissítése közben")
        End Try
    End Sub
    Private Function setSqlCommand(command As String) 'A táblázat feltöltése parancs megadásával
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
    Private Function initComboBox()
        Dim tavolletBox As New DataGridViewComboBoxCell
        tavolletBox.Items.Add("Szabadság")
        tavolletBox.Items.Add("Betegség")
        tavolletBox.Items.Add("Fizetetlen szabadság")
        Return tavolletBox
    End Function
    Private Function checkTable(tabla As DataGridView)
        If tabla.Columns(0).Name = "id" And tabla.Columns(1).Name = "Nev" Then
            If tabla.Columns(3).Name = "Jelszo" And tabla.Columns(4).Name = "Email" And tabla.Columns(3).Name = "Munkaido" Then
                Return 1
            End If
        ElseIf tabla.Columns(0).Name = "Datum" And tabla.Columns(1).Name = "Kezdo_ido" Then
            If tabla.Columns(2).Name = "Befejezo_ido" And tabla.Columns(3).Name = "FelhasznaloID" Then
                Return 0
            End If
        ElseIf tabla.Columns(0).Name = "Datum" And tabla.Columns(1).Name = "Tipus" Then
            Return 2
        Else
            Return -1
        End If
        Return -1
    End Function
    Private Function getDifferenceWorkingHours(index As Integer) 'Az összes munkaidő és a ledolgozott órák különbsége
        Dim munkaIdo, napiIdo As Integer
        Dim lista As New Dictionary(Of String, Integer)
        Dim aktDate As DateTime
        Dim evhonap = getYearAndMonth()
        napiIdo = isInteger(dgvUj.Item("napi_ido", index).Value)
        munkaIdo = (isInteger(dgvUj.Item("munkaido", index).Value)) * munkanap
        aktDate = isDate(dgvUj.Item("datum", index).Value)
        If aktDate.Year = evhonap.Item(itemEv) Then
            If aktDate.Month = evhonap.Item(itemHonap) Then
                lista.Add(itemMunkaIdo, munkaIdo)
                lista.Add(itemNapiIdo, napiIdo)
            Else
                napiIdo = 0
                munkaIdo = 0
            End If
        End If
        Return lista
    End Function
    Private Sub getWeekdaysNumber()
        Dim evhonap As New Dictionary(Of String, Integer)
        Dim ev, honap, nap As Integer
        Dim datum As DateTime
        Dim holidays = getHolidays()
        munkanap = 0
        evhonap = getYearAndMonth()
        ev = isInteger(evhonap.Item(itemEv))
        honap = isInteger(evhonap.Item(itemHonap))
        nap = DateTime.DaysInMonth(ev, honap)
        For index = 1 To nap
            datum = isDate(ev & ". " & honap & "." & index)
            Dim ismunkanap = isHolidayOrWeekend(datum, holidays)
            If ismunkanap Then
                munkanap += 1
            End If
        Next
    End Sub
    Private Sub UpdateMunkaidok(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateMunkaidok"
        cmd.Parameters.AddWithValue("@Datum", isDate(Cells.Item("Datum").Value))
        cmd.Parameters.AddWithValue("@Kezdo_ido", isDecimal(Cells.Item("Kezdo_ido").Value))
        cmd.Parameters.AddWithValue("@Befejezo_ido", isDecimal(Cells.Item("Befejezo_ido").Value))
        cmd.Parameters.AddWithValue("@FelhasznaloID", isInteger(Cells.Item("FelhasznaloID").Value))
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub
    Private Sub UpdateFelhasznalok(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateFelhasznalok"
        cmd.Parameters.AddWithValue("@id", Cells.Item("id").Value)
        cmd.Parameters.AddWithValue("@Nev", Cells.Item("Nev").Value)
        cmd.Parameters.AddWithValue("@Jelszo", Cells.Item("Jelszo").Value)
        cmd.Parameters.AddWithValue("@Email", Cells.Item("Email").Value)
        cmd.Parameters.AddWithValue("@Munkaido", isInteger(Cells.Item("Munkaido").Value))
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub
    Private Sub UpdateUnnenpnapok(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateUnnepnapok"
        cmd.Parameters.AddWithValue("@Datum", isDate(Cells.Item("Datum").Value))
        cmd.Parameters.AddWithValue("@Tipus", isInteger(Cells.Item("Tipus").Value))
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub
    Private Sub DeleteFelhasznalok(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteFelhasznalok"
        cmd.Parameters.AddWithValue("@Email", Cells.Item("Email").Value)
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub
    Private Function getHolidays()
        clearDataGridView(dgvUj)
        dgvUj.DataSource = setSqlCommand(getCommand("Unnepnap", ""))
        Dim lista As New Dictionary(Of Date, Integer)
        For index = 0 To dgvUj.Rows.Count - 2
            Dim datum = isDate(dgvUj.Item("Datum", index).Value)
            Dim tipus = isInteger(dgvUj.Item("Tipus", index).Value)
            lista.Add(datum, tipus)
        Next
        clearDataGridView(dgvUj)
        Return lista
    End Function

    'Dátum, Integer, Decimal ellenőrzés, és táblaresetelés
    Private Function isDate(parameter As Object)
        Dim result As Date
        If Date.TryParse(parameter, result) Then
            Return result
        Else
            Return DateTime.Now
        End If
    End Function
    Private Function isInteger(parameter As Object)
        Dim result As Integer
        If Integer.TryParse(parameter, result) Then
            Return result
        Else
            Return 0
        End If
    End Function
    Private Function isDecimal(parameter As Object)
        Dim result As Decimal
        If Decimal.TryParse(parameter, result) Then
            Return result
        Else
            Return 0
        End If
    End Function
    Private Sub clearDataGridView(tabla As DataGridView)
        tabla.ReadOnly = False
        tabla.DataSource = Nothing
        tabla.Columns.Clear()
        tabla.Rows.Clear()
    End Sub
    Private Function isHolidayOrWeekend(datum As Date, holidays As Dictionary(Of Date, Integer))
        If datum.DayOfWeek = DayOfWeek.Sunday Then
            Return False
        ElseIf datum.DayOfWeek() = DayOfWeek.Saturday Then
            Return False
        End If
        For Each item In holidays
            If item.Key = datum Then
                If item.Value = 0 Then
                    Return False
                ElseIf item.Value = 1 Then
                    Return False
                Else
                    Return True
                End If
            End If
        Next
        Return True
    End Function

    'Funkciót ellátó függvények
    Private Sub getUserWorkingHours(email As String) 'A kiválasztott felhasználó adott havi munkaidejének a lekérdezése
        clearDataGridView(dgvTabla)
        dgvTabla.DataSource = setSqlCommand(getCommand("Munkaido", email))
        dgvTabla.Columns("Datum").HeaderText = "Dátum"
        dgvTabla.Columns("Datum").ValueType = GetType(Date)
        dgvTabla.Columns("Kezdo_ido").HeaderText = "Kezdés"
        dgvTabla.Columns("Kezdo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("Befejezo_ido").HeaderText = "Befejezés"
        dgvTabla.Columns("Befejezo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("FelhasznaloID").Visible = False
        dgvTabla.Columns.Add("napi_ido", "Napi munkaidő")
        dgvTabla.Columns("napi_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns.Add("tavollet", "Távollét")
        dgvTabla.Columns("tavollet").ValueType = GetType(DataGridViewComboBoxCell)
        txtMunkaidoOsszes.Text = 0
        For index = 0 To dgvTabla.Rows.Count - 2
            dgvTabla.Item("tavollet", index) = initComboBox()
            getWorkTimeofDay(dgvTabla, index)
        Next
        dgvTabla.Columns("Datum").ReadOnly = True
        dgvTabla.Columns("napi_ido").ReadOnly = True
        btnMentes.Enabled = True
        btnTorles.Enabled = False
    End Sub
    Private Sub setDefaultWorkingHours(email As String)
        Dim datum, napStr, honapStr As String
        Dim napok, munkaido, felhaszid, ev, honap, rowCount As Integer
        Dim kido, bido As Decimal
        Dim row As String()
        Dim dateResult As DateTime
        Dim holidays = getHolidays()
        Dim evhonap = getYearAndMonth()
        clearDataGridView(dgvUj)
        dgvUj.DataSource = setSqlCommand(getCommand("MunkaidokAll", email))
        rowCount = dgvUj.Rows.Count
        If rowCount = 1 Then
            clearDataGridView(dgvUj)
            dgvUj.DataSource = setSqlCommand(getCommand("IDMunkaido", email))
        End If
        munkaido = isInteger(dgvUj.Item("munkaido", 0).Value)
        felhaszid = isInteger(dgvUj.Item("id", 0).Value)
        clearDataGridView(dgvTabla)
        dgvTabla.Columns.Add("Datum", "Dátum")
        dgvTabla.Columns.Add("Kezdo_ido", "Kezdő idő")
        dgvTabla.Columns.Add("Befejezo_ido", "Befejező idő")
        dgvTabla.Columns.Add("FelhasznaloID", "Felhasználó ID")
        dgvTabla.Columns("Datum").ValueType = GetType(Date)
        dgvTabla.Columns("Kezdo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("Befejezo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("FelhasznaloID").ValueType = GetType(Integer)
        ev = evhonap.Item(itemEv).ToString()
        honap = evhonap.Item(itemHonap).ToString()
        napok = DateTime.DaysInMonth(evhonap.Item(itemEv), evhonap.Item(itemHonap))
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
                Dim ismunkanap = isHolidayOrWeekend(datum, holidays)
                If ismunkanap Then
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
            End If
        Next
        InsertMunkaidok(dgvTabla)
        clearDataGridView(dgvTabla)
    End Sub
    Private Sub getUserData() 'A felhasználók adatainak lekérdezése az SQL Adatbázisból
        clearDataGridView(dgvTabla)
        dgvTabla.DataSource = setSqlCommand(getCommand("Felhasznalo", ""))
        dgvTabla.Columns("id").Visible = False
        dgvTabla.Columns("nev").HeaderText = "Név"
        dgvTabla.Columns("email").HeaderText = "E-Mail"
        dgvTabla.Columns("munkaido").HeaderText = "Munkaidő"
        dgvTabla.Columns("jelszo").Visible = False
        dgvTabla.Columns("jelszo").ReadOnly = True
        dgvTabla.Columns("id").ValueType = GetType(Integer)
        dgvTabla.Columns("id").ValueType = GetType(String)
        dgvTabla.Columns("id").ValueType = GetType(String)
        dgvTabla.Columns("id").ValueType = GetType(Integer)
        btnMentes.Enabled = True
        btnTorles.Enabled = True
        txtMunkaidoOsszes.Visible = False
        lblMunkaidoOsszes.Visible = False
        lblOra.Visible = False
    End Sub
    Private Sub getFszhLtb() 'A felhasználók kiválasztásához szükséges ListBox feltöltése
        ltbFelhasznalok.DataSource = setSqlCommand(getCommand("FelhasznaloLista", ""))
        ltbFelhasznalok.DisplayMember = "Nev"
        ltbFelhasznalok.ValueMember = "Email"
    End Sub
    Private Sub getSummary()
        clearDataGridView(dgvUj)
        Select Case user.role
            Case "Admin"
                dgvUj.DataSource = setSqlCommand(getCommand("OsszesitoAdmin", ""))
            Case "Felhasznalo"
                dgvUj.DataSource = setSqlCommand(getCommand("OsszesitoFelhasznalo", userEmail))
        End Select
        dgvUj.Columns.Add("napi_ido", "Napi munkaidő")
        dgvUj.Columns.Add("tavollet", "Távollét")
        For index = 0 To dgvUj.Rows.Count - 2
            dgvUj.Item("tavollet", index) = initComboBox()
            getWorkTimeofDay(dgvUj, index)
        Next
        dgvUj.ReadOnly = True
        txtMunkaidoOsszes.Visible = False
        lblOra.Visible = False
        lblMunkaidoOsszes.Visible = False
        btnMentes.Enabled = False
        btnTorles.Enabled = False
        getWorkingHoursSummary()
    End Sub
    Private Sub setHoliday()
        clearDataGridView(dgvTabla)
        dgvTabla.DataSource = setSqlCommand(getCommand("Unnepnap", ""))
        btnMentes.Enabled = True
        btnTorles.Enabled = True
    End Sub
    Private Sub getWorkingHoursSummary() 'Az összesített munkaidők megjelenítése egy táblázatban
        Dim kulonbseg, teljesora, eloirtora As Decimal
        Dim nev, email As String
        Dim row As String()
        Dim nevLista As New List(Of String)
        Dim lsindex = 0
        Dim rowCount As Integer
        clearDataGridView(dgvTabla)
        dgvTabla.Columns.Add("Nev", "Név")
        dgvTabla.Columns("Nev").ValueType = GetType(String)
        dgvTabla.Columns.Add("Email", "E-Mail")
        dgvTabla.Columns("Nev").ValueType = GetType(String)
        dgvTabla.Columns.Add("Eloirtora", "Előírt óraszám")
        dgvTabla.Columns("Nev").ValueType = GetType(Decimal)
        dgvTabla.Columns.Add("Teljesora", "Teljesített óraszám")
        dgvTabla.Columns("Nev").ValueType = GetType(Decimal)
        dgvTabla.Columns.Add("Kulonbseg", "Különbség")
        dgvTabla.Columns("Nev").ValueType = GetType(Decimal)
        rowCount = dgvUj.Rows.Count
        For index = 0 To rowCount - 2
            nev = dgvUj.Item("Nev", index).Value
            email = dgvUj.Item("email", index).Value
            Dim diffworkhours = getDifferenceWorkingHours(index)
            Dim napiIdo = diffworkhours.Item(itemNapiIdo)
            Dim munkaIdo = diffworkhours.Item(itemMunkaIdo)
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
                    eloirtora = isDecimal(dgvTabla.Item("eloirtora", lsindex).Value)
                    teljesora = isDecimal(dgvTabla.Item("teljesora", lsindex).Value)
                    kulonbseg = isDecimal(dgvTabla.Item("kulonbseg", lsindex).Value)
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

    'Funkciógombok működtetése
    Private Sub btnFelhasznalok_Click(sender As Object, e As EventArgs) Handles btnFelhasznalok.Click
        getUserData()
        If user.userName = "Rendszergazda" Then
            dgvTabla.Columns("jelszo").Visible = True
            dgvTabla.Columns("jelszo").ReadOnly = False
        End If
    End Sub
    Private Sub btnMentes_Click(sender As Object, e As EventArgs) Handles btnMentes.Click
        If checkTable(dgvTabla) = 1 Then
            editedRows.ForEach(Sub(i) UpdateFelhasznalok(dgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
        ElseIf checkTable(dgvTabla) = 0 Then
            editedRows.ForEach(Sub(i) UpdateMunkaidok(dgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
        ElseIf checkTable(dgvTabla) = 2 Then
            editedRows.ForEach(Sub(i) UpdateUnnenpnapok(dgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
        End If

    End Sub
    Private Sub btnMunkaidoleker_Click(sender As Object, e As EventArgs) Handles btnMunkaidoleker.Click
        Select Case user.role
            Case "Admin"
                getUserWorkingHours(ltbFelhasznalok.SelectedValue)
            Case "Felhasznalo"
                getUserWorkingHours(userEmail)
        End Select
    End Sub
    Private Sub btnMunkaidoossz_Click(sender As Object, e As EventArgs) Handles btnMunkaidoossz.Click
        getWeekdaysNumber()
        getSummary()
    End Sub
    Private Sub tstButton_Click(sender As Object, e As EventArgs) Handles tstButton.Click
        Select Case user.role
            Case "Admin"
                setDefaultWorkingHours(ltbFelhasznalok.SelectedValue)
            Case "Felhasznalo"
                setDefaultWorkingHours(userEmail)
        End Select
    End Sub
    Private Sub btnTorles_Click(sender As Object, e As EventArgs) Handles btnTorles.Click
        If checkTable(dgvTabla) = 1 Then
            editedRows.ForEach(Sub(i) DeleteFelhasznalok(dgvTabla.SelectedCells.Item(i).Value))
            editedRows.Clear()
            'ElseIf checkTable(dgvTabla) = 0 Then
            '    editedRows.ForEach(Sub(i) UpdateMunkaidok(dgvTabla.Rows.Item(i).Cells))
            '    editedRows.Clear()
        End If
    End Sub
    Private Sub btnUnnep_Click(sender As Object, e As EventArgs) Handles btnUnnep.Click
        setHoliday()
    End Sub

    'DataGridView automatikus függvényei
    Private Sub dgvTabla_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvTabla.CellBeginEdit
        dgvTabla.Rows.Item(e.RowIndex).Tag = dgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value
    End Sub
    Private Sub dgvTabla_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTabla.CellEndEdit
        Try
            If Not dgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value.Equals(dgvTabla.Rows.Item(e.RowIndex).Tag) Then
                If Not editedRows.Contains(e.RowIndex) Then
                    editedRows.Add(e.RowIndex)
                End If
                If checkTable(dgvTabla) = 0 Then
                    Dim rowCount = dgvTabla.Rows.Count
                    txtMunkaidoOsszes.Text = 0
                    For index = 0 To rowCount - 2
                        getWorkTimeofDay(dgvTabla, index)
                    Next
                End If
            End If
            dgvTabla.Rows.Item(e.RowIndex).Tag = ""
        Catch ex As Exception
            Console.WriteLine("Hiba a cellamódosításban.")
        End Try
    End Sub
    Private Sub dgvTabla_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvTabla.RowPostPaint
        Dim dg As DataGridView = DirectCast(sender, DataGridView)
        Dim rowNumber As String = (e.RowIndex + 1).ToString()
        While rowNumber.Length < dg.RowCount.ToString().Length
            rowNumber = "0" & rowNumber
        End While
        Dim size As SizeF = e.Graphics.MeasureString(rowNumber, Me.Font)
        If dg.RowHeadersWidth < CInt(size.Width + 20) Then
            dg.RowHeadersWidth = CInt(size.Width + 20)
        End If
        Dim b As Brush = SystemBrushes.ControlText
        e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))
    End Sub
    Private Sub dgvTabla_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvTabla.DataError
        MessageBox.Show("Error:  " & e.Context.ToString())
        If (e.Context = DataGridViewDataErrorContexts.Commit) Then
            MessageBox.Show("Commit error")
        End If
        If (e.Context = DataGridViewDataErrorContexts.CurrentCellChange) Then
            MessageBox.Show("Cell change")
        End If
        If (e.Context = DataGridViewDataErrorContexts.Parsing) Then
            MessageBox.Show("parsing error")
        End If
        If (e.Context = DataGridViewDataErrorContexts.LeaveControl) Then
            MessageBox.Show("leave control error")
        End If

        If (TypeOf (e.Exception) Is ConstraintException) Then
            Dim view As DataGridView = CType(sender, DataGridView)
            view.Rows(e.RowIndex).ErrorText = "an error"
            view.Rows(e.RowIndex).Cells(e.ColumnIndex) _
                .ErrorText = "an error"
            MsgBox("error")
            e.ThrowException = False
        End If
    End Sub

End Class
