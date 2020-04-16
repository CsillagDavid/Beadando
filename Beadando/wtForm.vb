Imports System.Data.SqlClient

Public Class wtForm
    Dim authentication As New AuthenticationManagement
    Dim munkaidokManagement As New MunkaidokManagement
    Dim felhasznalokManagement As New FelhasznalokManagement
    Dim unnepnapokManagement As New UnnepnapokManagement
    Dim jogkorokManagement As New JogkorokManagement
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
    Dim felhasznalokLista As New List(Of Felhasznalok)
    Dim munkaidokLista As New List(Of Munkaidok)
    Dim unnepnapokLista As New List(Of Unnepnapok)

    Private Sub wtForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Login.ShowDialog()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
        felhasznalokManagement.getFelhasznalok(felhasznalokLista)
        unnepnapokManagement.GetUnnepnapok(unnepnapokLista)
        readYearAndMonth()
        checkAuthentication()
    End Sub

#Region "Jogosultságkezelő"
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
                    lblJogkor.Visible = True
                    lblJogkor.Enabled = True
                    btnJogkor.Visible = True
                    btnJogkor.Enabled = True
                End If
            Case "Felhasznalo"
                ltbFelhasznalok.Enabled = False
                btnFelhasznalok.Enabled = False
                setDefaultWorkingHours(user.email)
                getUserWorkingHours(user.email)
                userEmail = user.email
        End Select
    End Sub
#End Region

#Region "Inicializálások és számítási függvények, segédfüggvények"
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
    Private Function getYearAndMonth()
        Dim lista As New Dictionary(Of String, Integer)
        lista.Add(itemEv, isInteger(cmbEv.SelectedItem))
        lista.Add(itemHonap, isInteger(cmbHonap.SelectedItem))
        Return lista
    End Function

    'A napi ledolgozott órák kiszámítása, valamint a havi összesített munkaidő számolása
    Private Sub getWorkTimeofDay(tabla As DataGridView, index As Integer)
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
    Private Function initComboBox()
        Dim tavolletBox As New DataGridViewComboBoxCell
        tavolletBox.Items.Add("Szabadság")
        tavolletBox.Items.Add("Betegség")
        tavolletBox.Items.Add("Fizetetlen szabadság")
        Return tavolletBox
    End Function

    Private Function checkTable(tabla As DataGridView)
        Dim ret As Integer
        If tabla.Columns(0).Name = "id" And tabla.Columns(1).Name = "Nev" Then
            ret = 1
        ElseIf tabla.Columns(0).Name = "Datum" And tabla.Columns(1).Name = "Kezdo_ido" Then
            ret = 0
        ElseIf tabla.Columns(0).Name = "Datum" And tabla.Columns(1).Name = "Tipus" Then
            ret = 2
        ElseIf tabla.Columns(0).Name = "Nev" And tabla.Columns(1).Name = "Jogkor" Then
            ret = 3
        Else
            ret = -1
        End If
        Return ret
    End Function

    'Az összes munkaidő és a ledolgozott órák különbsége
    Private Function getDifferenceWorkingHours(index As Integer, munkanap As Integer, napiIdo As Integer, munkaIdo As Integer, aktDate As DateTime)
        Dim lista As New Dictionary(Of String, Integer)
        Dim evhonap = getYearAndMonth()
        munkaIdo = munkaIdo * munkanap
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
    Private Function getWeekdaysNumber()
        Dim evhonap As New Dictionary(Of String, Integer)
        Dim ev, honap, nap, munkanap As Integer
        Dim datum As DateTime
        evhonap = getYearAndMonth()
        ev = isInteger(evhonap.Item(itemEv))
        honap = isInteger(evhonap.Item(itemHonap))
        nap = DateTime.DaysInMonth(ev, honap)
        For index = 1 To nap
            datum = isDate(ev & ". " & honap & "." & index)
            Dim ismunkanap = isHolidayOrWeekend(datum, unnepnapokLista)
            If ismunkanap Then
                munkanap += 1
            End If
        Next
        Return munkanap
    End Function
#End Region

#Region "SQL tárolt eljárások"
    Private Sub newFhszsetJogkor() 'Új felhasználóknak beállítja az alapértelmezett Felhasználó jogkört

        clearDataGridView(dgvUj)

        dgvUj.Columns.Add("id", "Id")
        dgvUj.Columns("id").ValueType = GetType(Integer)

        For index = 0 To felhasznalokLista.Count - 1
            dgvUj.Rows.Add()
            dgvUj.Item("id", index).Value = felhasznalokLista(index).Id
        Next

        jogkorokManagement.InsertJogkorok(dgvUj)
        clearDataGridView(dgvUj)

    End Sub
    Private Sub InsertMunkaidok(tabla As DataGridView)
        Try
            munkaidokManagement.InsertOrUpdate(tabla)
        Catch ex As Exception
            Console.WriteLine("Hiba az adatbázis frissítése közben")
        End Try
    End Sub
#End Region
    'Dátum, Integer, Decimal ellenőrzés, és táblaresetelés
    Private Sub clearDataGridView(table As DataGridView)
        table.ReadOnly = False
        table.DataSource = Nothing
        table.Columns.Clear()
        table.Rows.Clear()
    End Sub

#Region "Funkciót ellátó függvények"
    Private Sub getUserWorkingHours(email As String) 'A kiválasztott felhasználó adott havi munkaidejének a lekérdezése

        clearDataGridView(dgvTabla)
        Dim evhonap = getYearAndMonth()
        munkaidokLista.Clear()
        munkaidokManagement.getMunkaidok(munkaidokLista, email, evhonap.item(itemEv), evhonap.item(itemHonap))

        dgvTabla.Columns.Add("Datum", "Dátum")
        dgvTabla.Columns.Add("Kezdo_ido", "Kezdés")
        dgvTabla.Columns.Add("Befejezo_ido", "Befejezés")
        dgvTabla.Columns.Add("FelhasznaloID", "Felhasználó ID")
        dgvTabla.Columns.Add("Napi_ido", "Napi munkaidő")
        dgvTabla.Columns.Add("Tavollet", "Távollét")

        dgvTabla.Columns("Datum").ValueType = GetType(Date)
        dgvTabla.Columns("Kezdo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("Befejezo_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("FelhasznaloID").ValueType = GetType(Integer)
        dgvTabla.Columns("Napi_ido").ValueType = GetType(Decimal)
        dgvTabla.Columns("Tavollet").ValueType = GetType(DataGridViewComboBoxCell)

        dgvTabla.Columns("FelhasznaloID").Visible = False

        For index = 0 To munkaidokLista.Count - 1
            dgvTabla.Rows.Add()
            dgvTabla.Item("Datum", index).Value = munkaidokLista(index).Datum
            dgvTabla.Item("Kezdo_ido", index).Value = munkaidokLista(index).Kezdo_ido
            dgvTabla.Item("Befejezo_ido", index).Value = munkaidokLista(index).Befejezo_ido
            dgvTabla.Item("FelhasznaloID", index).Value = munkaidokLista(index).FelhasznaloID
        Next

        txtMunkaidoOsszes.Text = 0
        For index = 0 To dgvTabla.Rows.Count - 1
            dgvTabla.Item("Tavollet", index) = initComboBox()
            getWorkTimeofDay(dgvTabla, index)
        Next

        dgvTabla.Columns("Datum").ReadOnly = True
        dgvTabla.Columns("Napi_ido").ReadOnly = True
        btnMentes.Enabled = True
        btnTorles.Enabled = False
        Select Case user.role
            Case "Admin"
                dgvTabla.AllowUserToAddRows = True
            Case "Felhasznalo"
                dgvTabla.AllowUserToAddRows = False
        End Select

    End Sub
    Private Sub setDefaultWorkingHours(email As String)

        Dim datum, napStr, honapStr As String
        Dim napok, munkaido, felhaszid, ev, honap As Integer
        Dim kido, bido As Decimal
        Dim row As String()
        Dim dateResult As DateTime
        Dim evhonap = getYearAndMonth()

        munkaidokLista.Clear()
        munkaidokManagement.getMunkaidok(munkaidokLista, email, evhonap.item(itemEv), evhonap.item(itemHonap))

        For Each felhasznalo In felhasznalokLista
            If felhasznalo.Email = email Then
                munkaido = isInteger(felhasznalo.Munkaido)
                felhaszid = isInteger(felhasznalo.Id)
                Exit For
            End If
        Next

        clearDataGridView(dgvTabla)

        dgvTabla.Columns.Add("Datum", "Dátum")
        dgvTabla.Columns.Add("Kezdo_ido", "Kezdés")
        dgvTabla.Columns.Add("Befejezo_ido", "Befejezés")
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
                Dim ismunkanap = isHolidayOrWeekend(datum, unnepnapokLista)
                If ismunkanap Then
                    kido = 8
                    bido = kido + munkaido
                    If munkaidokLista.Count > 1 Then
                        If Not munkaidokLista(datumindex).Datum = dateResult Then
                            row = {
                                    dateResult,
                                    kido,
                                    bido,
                                    felhaszid
                                 }
                        Else
                            dateResult = munkaidokLista(datumindex).Datum
                            kido = munkaidokLista(datumindex).Kezdo_ido
                            bido = munkaidokLista(datumindex).Befejezo_ido
                            felhaszid = munkaidokLista(datumindex).FelhasznaloID
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

        dgvTabla.Columns.Add("id", "id")
        dgvTabla.Columns.Add("Nev", "Név")
        dgvTabla.Columns.Add("Jelszo", "Jelszó")
        dgvTabla.Columns.Add("Email", "E-mail")
        dgvTabla.Columns.Add("Munkaido", "Munkaidő")

        dgvTabla.Columns("id").ValueType = GetType(Integer)
        dgvTabla.Columns("Nev").ValueType = GetType(String)
        dgvTabla.Columns("Jelszo").ValueType = GetType(String)
        dgvTabla.Columns("Email").ValueType = GetType(String)
        dgvTabla.Columns("Munkaido").ValueType = GetType(Decimal)


        For index = 0 To felhasznalokLista.Count - 1
            dgvTabla.Rows.Add()
            dgvTabla.Item("id", index).Value = felhasznalokLista(index).Id
            dgvTabla.Item("Nev", index).Value = felhasznalokLista(index).Nev
            dgvTabla.Item("Jelszo", index).Value = felhasznalokLista(index).Jelszo
            dgvTabla.Item("Email", index).Value = felhasznalokLista(index).Email
            dgvTabla.Item("Munkaido", index).Value = felhasznalokLista(index).Munkaido
        Next

        dgvTabla.Columns("id").Visible = False
        dgvTabla.Columns("id").ReadOnly = True

        If user.userName = "Rendszergazda" Then
            dgvTabla.Columns("jelszo").Visible = True
            dgvTabla.Columns("jelszo").ReadOnly = False
        Else
            dgvTabla.Columns("Jelszo").Visible = False
            dgvTabla.Columns("Jelszo").ReadOnly = True
        End If

        btnMentes.Enabled = True
        btnTorles.Enabled = True
        txtMunkaidoOsszes.Visible = False
        lblMunkaidoOsszes.Visible = False
        lblOra.Visible = False

    End Sub
    Private Sub getFszhLtb() 'A felhasználók kiválasztásához szükséges ListBox feltöltése

        Dim szures As New Dictionary(Of String, String)

        For Each felhasz In felhasznalokLista
            szures.Add(felhasz.Nev, felhasz.Email)
        Next

        Dim pbs As New BindingSource
        pbs.DataSource = szures
        ltbFelhasznalok.DataSource = pbs
        ltbFelhasznalok.DisplayMember = "Key"
        ltbFelhasznalok.ValueMember = "Value"

    End Sub
    Private Function getSummary()

        clearDataGridView(dgvUj)

        Dim evhonap = getYearAndMonth()
        Dim command = ""
        Dim felhaszMunkaido As New List(Of List(Of Munkaidok))

        Select Case user.role
            Case "Admin"
                For index = 0 To felhasznalokLista.Count - 1
                    Dim ujmunkaido As New List(Of Munkaidok)
                    munkaidokManagement.getMunkaidok(ujmunkaido, felhasznalokLista(index).Email, evhonap.item(itemEv), evhonap.item(itemHonap))
                    felhaszMunkaido.Add(ujmunkaido)
                Next
            Case "Felhasznalo"
                Dim ujmunkaido As New List(Of Munkaidok)
                munkaidokManagement.getMunkaidok(ujmunkaido, userEmail, evhonap.item(itemEv), evhonap.item(itemHonap))
                felhaszMunkaido.Add(ujmunkaido)
        End Select

        dgvUj.Columns.Add("Nev", "Név")
        dgvUj.Columns.Add("Email", "E-mail")
        dgvUj.Columns.Add("Munkaido", "Munkaidő")
        dgvUj.Columns.Add("Datum", "Dátum")
        dgvUj.Columns.Add("Kezdo_ido", "Kezdés")
        dgvUj.Columns.Add("Befejezo_ido", "Befejezés")
        dgvUj.Columns.Add("Napi_ido", "Napi munkaidő")
        dgvUj.Columns.Add("Tavollet", "Távollét")

        Dim rowindex = 0
        For Each szemelyes In felhaszMunkaido
            For Index = 0 To szemelyes.Count - 1
                dgvUj.Rows.Add()
                For findex = 0 To felhasznalokLista.Count - 1
                    If szemelyes(Index).FelhasznaloID = felhasznalokLista(findex).Id Then
                        dgvUj.Item("Nev", rowindex).Value = felhasznalokLista(findex).Nev
                        dgvUj.Item("Email", rowindex).Value = felhasznalokLista(findex).Email
                        dgvUj.Item("Munkaido", rowindex).Value = felhasznalokLista(findex).Munkaido
                        Exit For
                    End If
                Next
                dgvUj.Item("Datum", rowindex).Value = szemelyes(Index).Datum
                dgvUj.Item("Kezdo_ido", rowindex).Value = szemelyes(Index).Kezdo_ido
                dgvUj.Item("Befejezo_ido", rowindex).Value = szemelyes(Index).Befejezo_ido
                rowindex += 1
            Next
        Next

        For index = 0 To dgvUj.Rows.Count - 2
            dgvUj.Item("Tavollet", index) = initComboBox()
            getWorkTimeofDay(dgvUj, index)
        Next

        dgvUj.ReadOnly = True
        dgvUj.Visible = False
        txtMunkaidoOsszes.Visible = False
        lblOra.Visible = False
        lblMunkaidoOsszes.Visible = False
        btnMentes.Enabled = False
        btnTorles.Enabled = False

        Return dgvUj
    End Function
    Private Sub setHoliday()
        clearDataGridView(dgvTabla)

        dgvTabla.Columns.Add("Datum", "Dátum")
        dgvTabla.Columns.Add("Tipus", "Típus")

        dgvTabla.Columns("Datum").ValueType = GetType(Date)
        dgvTabla.Columns("Tipus").ValueType = GetType(Integer)

        For index = 0 To unnepnapokLista.Count - 2
            dgvTabla.Rows.Add()
            dgvTabla.Item("Datum", index).Value = unnepnapokLista(index).Datum
            dgvTabla.Item("Tipus", index).Value = unnepnapokLista(index).Tipus
        Next

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
        Dim munkanap = getWeekdaysNumber()
        Dim tabla = getSummary()

        clearDataGridView(dgvTabla)

        dgvTabla.Columns.Add("Nev", "Név")
        dgvTabla.Columns.Add("Email", "E-Mail")
        dgvTabla.Columns.Add("Eloirtora", "Előírt óraszám")
        dgvTabla.Columns.Add("Teljesora", "Teljesített óraszám")
        dgvTabla.Columns.Add("Kulonbseg", "Különbség")

        dgvTabla.Columns("Nev").ValueType = GetType(String)
        dgvTabla.Columns("Email").ValueType = GetType(String)
        dgvTabla.Columns("Eloirtora").ValueType = GetType(Integer)
        dgvTabla.Columns("Teljesora").ValueType = GetType(Decimal)
        dgvTabla.Columns("Kulonbseg").ValueType = GetType(Decimal)

        rowCount = tabla.Rows.Count

        For index = 0 To rowCount - 2

            nev = tabla.Item("Nev", index).Value
            email = tabla.Item("email", index).Value

            Dim diffworkhours = getDifferenceWorkingHours(index, munkanap, isInteger(tabla.Item("napi_ido", index).Value), isInteger(tabla.Item("munkaido", index).Value), isDate(tabla.Item("datum", index).Value))
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

        dgvTabla.AllowUserToAddRows = False
        dgvTabla.ReadOnly = True

    End Sub
    Private Sub getJogkorok()
        clearDataGridView(dgvTabla)

        Dim jogkorok As New List(Of Jogkorok)
        jogkorokManagement.getJogkorok(jogkorok)

        dgvTabla.Columns.Add("Nev", "Név")
        dgvTabla.Columns.Add("Jogkor", "Jogkör")

        dgvTabla.Columns("Nev").ValueType = GetType(String)
        dgvTabla.Columns("Jogkor").ValueType = GetType(String)

        For index = 0 To felhasznalokLista.Count - 1
            dgvTabla.Rows.Add()
            dgvTabla.Item("Nev", index).Value = felhasznalokLista(index).Nev
            For jkindex = 0 To jogkorok.Count - 1
                If felhasznalokLista(index).Id = jogkorok(jkindex).FelhasznaloID Then
                    dgvTabla.Item("Jogkor", index).Value = jogkorok(jkindex).Jogkor
                End If
            Next
        Next

        dgvTabla.Columns("Nev").ReadOnly = True
        btnMentes.Enabled = True

    End Sub

#End Region
#Region "Funkciógombok működtetése"
    Private Sub btnFelhasznalok_Click(sender As Object, e As EventArgs) Handles btnFelhasznalok.Click
        getUserData()
    End Sub
    Private Sub btnMentes_Click(sender As Object, e As EventArgs) Handles btnMentes.Click
        If checkTable(dgvTabla) = 1 Then
            editedRows.ForEach(Sub(i) felhasznalokManagement.InsertOrUpdate(dgvTabla.Rows.Item(i).Cells))
            felhasznalokLista.Clear()
            felhasznalokManagement.getFelhasznalok(felhasznalokLista)
            getFszhLtb()
            newFhszsetJogkor()
            editedRows.Clear()
        ElseIf checkTable(dgvTabla) = 0 Then
            editedRows.ForEach(Sub(i) munkaidokManagement.InsertOrUpdate(dgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
        ElseIf checkTable(dgvTabla) = 2 Then
            editedRows.ForEach(Sub(i) unnepnapokManagement.InsertOrUpdate(dgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
        ElseIf checkTable(dgvTabla) = 3 Then
            editedRows.ForEach(Sub(i) jogkorokManagement.UpdateJogkorok(dgvTabla.Rows.Item(i).Cells))
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
        getWorkingHoursSummary()
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
            Dim email, nev As String
            Dim id As Integer
            If dgvTabla.SelectedRows.Count > 0 Then
                email = dgvTabla.SelectedRows(0).Cells("Email").Value
                id = dgvTabla.SelectedRows(0).Cells("id").Value
                nev = dgvTabla.SelectedRows(0).Cells("nev").Value
                dgvTabla.Rows.Remove(dgvTabla.SelectedRows(0))
                felhasznalokManagement.DeleteFelhasznalok(nev, email, id)
                felhasznalokLista.Clear()
                felhasznalokManagement.getFelhasznalok(felhasznalokLista)
                getFszhLtb()
            Else
                MessageBox.Show("Jelölj ki egy sort, mielőtt törölni szeretnéd.")
            End If
        End If
    End Sub
    Private Sub btnUnnep_Click(sender As Object, e As EventArgs) Handles btnUnnep.Click
        setHoliday()
    End Sub
    Private Sub btnJogkor_Click(sender As Object, e As EventArgs) Handles btnJogkor.Click
        getJogkorok()
    End Sub
#End Region

#Region "DataGridView automatikus függvényei"
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
#End Region

End Class
