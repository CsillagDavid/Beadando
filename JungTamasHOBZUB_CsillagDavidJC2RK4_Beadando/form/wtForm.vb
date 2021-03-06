﻿Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class WtForm

#Region "Változók"

    'Adatbázis Management osztályok betöltése
    Dim authMan As New AuthenticationManagement
    Dim mkiMan As New MunkaidokManagement
    Dim fhszMan As New FelhasznalokManagement
    Dim unnepMan As New UnnepnapokManagement
    Dim jgkkMan As New JogkorokManagement
    Dim szabMan As New SzabadsagokManagement

    'SQL kapcsolathoz szükséges változók
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim sqlConnection As SqlConn

    'Egyéb változók
    Public Property User = New User()
    Dim editedRows As New List(Of Integer)
    Dim userEmail As String

    'Statikus változók
    ReadOnly itemEv = "Ev"
    ReadOnly itemHonap = "Honap"
    ReadOnly itemMunkaIdo = "Munkaido"
    ReadOnly itemNapiIdo = "Napiido"

    'Adatbázis objectjeit tartalmazó listák
    Dim fhszLista As New List(Of Felhasznalok)
    Dim mkiLista As New List(Of Munkaidok)
    Dim unnepLista As New List(Of Unnepnapok)
    Dim szabLista As New List(Of Szabadsagok)

#End Region

    'Load függvény
    Private Sub WtForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Login.ShowDialog()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
        fhszMan.GetFelhasznalok(fhszLista)
        unnepMan.GetUnnepnapok(unnepLista)
        ReadYearAndMonth()
        CheckAuthentication()
    End Sub

#Region "Jogosultságkezelő"

    'A bejelentkezett felhasználó jogkörének a lekérdezése, és a program ezáltali indítása
    Private Sub CheckAuthentication()
        Select Case User.Role
            Case "Admin"
                GetFszhLtb()
                LtbFelhasznalok.Enabled = True
                BtnFelhasznalok.Enabled = True
                LblUnnep.Visible = True
                LblUnnep.Enabled = True
                BtnUnnep.Visible = True
                BtnUnnep.Enabled = True
                LblJogkor.Visible = True
                LblJogkor.Enabled = True
                BtnJogkor.Visible = True
                BtnJogkor.Enabled = True
                LblGeneralas.Visible = True
                LblGeneralas.Enabled = True
                BtnGeneralas.Visible = True
                BtnGeneralas.Enabled = True
            Case "Vezeto"
                GetFszhLtb()
                SetDefaultWorkingHours(User.Email)
                GetUserWorkingHours(User.Email)
                userEmail = User.Email
                LtbFelhasznalok.Enabled = True
                BtnFelhasznalok.Enabled = True
                LblUnnep.Visible = False
                LblUnnep.Enabled = False
                BtnUnnep.Visible = False
                BtnUnnep.Enabled = False
                LblJogkor.Visible = False
                LblJogkor.Enabled = False
                BtnJogkor.Visible = False
                BtnJogkor.Enabled = False
                LblGeneralas.Visible = False
                LblGeneralas.Enabled = False
                BtnGeneralas.Visible = False
                BtnGeneralas.Enabled = False
            Case "Beosztott"
                SetDefaultWorkingHours(User.Email)
                GetUserWorkingHours(User.Email)
                userEmail = User.Email
                LtbFelhasznalok.Enabled = False
                BtnFelhasznalok.Enabled = False
                LblUnnep.Visible = False
                LblUnnep.Enabled = False
                BtnUnnep.Visible = False
                BtnUnnep.Enabled = False
                LblJogkor.Visible = False
                LblJogkor.Enabled = False
                BtnJogkor.Visible = False
                BtnJogkor.Enabled = False
                LblGeneralas.Visible = False
                LblGeneralas.Enabled = False
                BtnGeneralas.Visible = False
                BtnGeneralas.Enabled = False
        End Select
    End Sub

#End Region

#Region "Inicializálások és számítási függvények, segédfüggvények"

    'Az év és hónap comboboxok inicializálása
    Private Sub ReadYearAndMonth()
        Dim yr, mh As Integer
        yr = DateTime.Now.Year
        mh = DateTime.Now.Month
        For aktYr = yr To 2000 Step -1
            CmbEv.Items.Add(aktYr)
        Next
        For aktMh = 1 To 12
            CmbHonap.Items.Add(aktMh)
        Next
        CmbEv.SelectedIndex = 0
        CmbHonap.SelectedIndex = (mh - 1)
    End Sub

    'Az új, felvitt dátum ellenőrzése, hogy szerepel-e már az adatbázisban
    Private Sub CheckDate(tabla As DataGridView)
        Try
            Dim ujDatum = tabla.Item("Datum", tabla.Rows.Count - 2).Value
            For index = 0 To tabla.Rows.Count - 3
                If ujDatum = tabla.Item("Datum", index).Value Then
                    MsgBox("Ez a dátum már szerepel a rendszerben, válasszon másikat vagy módosítsa a már meglévő dátum jelenlétét!")
                    tabla.Rows.RemoveAt(tabla.Rows.Count - 2)
                    Exit For
                Else
                    tabla.Item("FelhasznaloID", tabla.Rows.Count - 2).Value = tabla.Item("FelhasznaloID", tabla.Rows.Count - 3).Value
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("Checkdate hiba")
        End Try

    End Sub

    'A kiválasztott év és hónap lekérése a comboboxokból
    Private Function GetYearAndMonth()
        Dim lista As New Dictionary(Of String, Integer)
        lista.Add(itemEv, IsInteger(CmbEv.SelectedItem))
        lista.Add(itemHonap, IsInteger(CmbHonap.SelectedItem))
        Return lista
    End Function

    'A naponként ledolgozott órák kiszámolása a táblázatba, valamint a textbox mezőbe az adott havi összesítés megjelenítése
    Private Sub GetWorkTimeofDay(tabla As DataGridView, index As Integer)
        Try
            Dim kezdo, befejezo As DateTime
            Dim mkoSum As Decimal
            Dim asa = kezdo.GetDateTimeFormats()
            Dim ora, perc As Integer
            tabla.Columns("Napi_ido").ReadOnly = False
            befejezo = tabla.Item("Befejezo_ido", index).Value
            kezdo = tabla.Item("Kezdo_ido", index).Value
            mkoSum = TxtMunkaidoOsszes.Text
            ora = befejezo.Hour - kezdo.Hour
            perc = befejezo.Minute - kezdo.Minute
            If perc < 0 Then
                ora -= 1
                perc *= (-1)
                perc = (60 - perc)
            End If
            tabla.Item("Napi_ido", index).Value = IsDate(ora & ":" & perc)
            mkoSum += ora + IsDecimal(0 & "," & perc)
            TxtMunkaidoOsszes.Visible = True
            LblMunkaidoOsszes.Visible = True
            TxtMunkaidoOsszes.Text = mkoSum
            LblOra.Visible = True
            tabla.Columns("Napi_ido").ReadOnly = True
        Catch ex As Exception
            Console.WriteLine("A munkaidő kiszámításában hiba lépett fel!")
        End Try
    End Sub

    'A generált táblázatok ellenőrzése, ezáltal értékadása a mentés és törlés funkciókhoz
    Private Function CheckTable(tabla As DataGridView)
        Dim ret As Integer
        If tabla.Columns(0).Name = "id" And tabla.Columns(1).Name = "Nev" Then
            ret = 1
        ElseIf tabla.Columns(0).Name = "Datum" And tabla.Columns(1).Name = "Kezdo_ido" Then
            ret = 0
        ElseIf tabla.Columns(0).Name = "Datum" And tabla.Columns(1).Name = "Tipus" Then
            ret = 2
        ElseIf tabla.Columns(0).Name = "Nev" And tabla.Columns(1).Name = "Jogkor" Then
            ret = 3
        ElseIf tabla.Columns(0).Name = "Datum" And tabla.Columns(1).Name = "Tavollet" Then
            ret = 4
        Else
            ret = -1
        End If
        Return ret
    End Function

    'Egy felhasználó összes előírt órájának kiszámítása
    Private Function GetDifferenceWorkingHours(munkanap As Integer, napiIdo As DateTime, munkaIdo As Integer, aktDate As DateTime)
        Dim lista As New Dictionary(Of String, Decimal)
        Dim ora, perc As Integer
        ora = napiIdo.Hour
        perc = napiIdo.Minute
        If perc < 0 Then
            ora -= 1
            perc *= (-1)
        End If
        Dim aktIdo = IsDecimal(ora & "," & perc)
        Dim evhonap = GetYearAndMonth()
        munkaIdo *= munkanap
        If aktDate.Year = evhonap.Item(itemEv) Then
            If aktDate.Month = evhonap.Item(itemHonap) Then
                lista.Add(itemMunkaIdo, munkaIdo)
                lista.Add(itemNapiIdo, aktIdo)
            End If
        End If
        Return lista
    End Function

    'A munkanapok számának megállapítására szolgáló függvény
    Private Function GetWeekdaysNumber()
        Dim ev, honap, nap, munkanap As Integer
        Dim datum As DateTime
        Dim evhonap = GetYearAndMonth()
        ev = IsInteger(evhonap.Item(itemEv))
        honap = IsInteger(evhonap.Item(itemHonap))
        nap = DateTime.DaysInMonth(ev, honap)
        For index = 1 To nap
            datum = IsDate(ev & ". " & honap & "." & index)
            Dim ismunkanap = IsHolidayOrWeekend(datum, unnepLista)
            If ismunkanap Then
                munkanap += 1
            End If
        Next
        Return munkanap
    End Function

    'DataGridView táblák "resetelése", adatok törlése a táblából, nullázás
    Private Sub ClearDataGridView(table As DataGridView)
        table.ReadOnly = False
        table.DataSource = Nothing
        table.Columns.Clear()
        table.Rows.Clear()
    End Sub

    'Felhasználó ID beszúrása az új sorokba
    Public Sub SaveSzabadsag()
        Dim email As String
        If User.Role = "Beosztott" Then
            email = userEmail
        Else
            email = LtbFelhasznalok.SelectedValue
        End If
        Dim felhaszid As Integer
        For Each item In fhszLista
            If item.Email = email Then
                felhaszid = item.Id
                Exit For
            End If
        Next
        For index = 0 To DgvTabla.Rows.Count - 2
            DgvTabla.Item("FelhasznaloID", index).Value = felhaszid
        Next
    End Sub

    'Szabadságok számának megállapítása
    Public Function GetSzabadsagCount(email As String)
        Dim lista As New List(Of Szabadsagok)
        Dim evhonap = GetYearAndMonth()
        szabMan.GetSzabadsagok(lista, email, evhonap.item(itemEv), evhonap.item(itemHonap))
        Dim count = lista.Count
        For Each item In lista
            If Not IsHolidayOrWeekend(item.Datum, unnepLista) Then
                count -= 1
            End If
        Next
        Return count
    End Function

#End Region

#Region "Funkciót ellátó függvények"

    'A kiválasztott vagy bejelentkezett felhasználó adott havi munkaidejének a lekérdezése
    Private Sub GetUserWorkingHours(email As String)

        ClearDataGridView(DgvTabla)
        Dim evhonap = GetYearAndMonth()
        mkiLista.Clear()
        mkiMan.GetMunkaidok(mkiLista, email, evhonap.item(itemEv), evhonap.item(itemHonap))

        DgvTabla.Columns.Add("Datum", "Dátum")
        DgvTabla.Columns.Add("Kezdo_ido", "Kezdés")
        DgvTabla.Columns.Add("Befejezo_ido", "Befejezés")
        DgvTabla.Columns.Add("Napi_ido", "Napi munkaidő")
        DgvTabla.Columns.Add("FelhasznaloID", "Felhasználó ID")

        DgvTabla.Columns("Datum").ValueType = GetType(Date)
        DgvTabla.Columns("Kezdo_ido").ValueType = GetType(DateTime)
        DgvTabla.Columns("Kezdo_ido").DefaultCellStyle.Format = "HH:mm"
        DgvTabla.Columns("Befejezo_ido").ValueType = GetType(DateTime)
        DgvTabla.Columns("Befejezo_ido").DefaultCellStyle.Format = "HH:mm"
        DgvTabla.Columns("Napi_ido").ValueType = GetType(DateTime)
        DgvTabla.Columns("Napi_ido").DefaultCellStyle.Format = "HH:mm"
        DgvTabla.Columns("FelhasznaloID").ValueType = GetType(Integer)

        TxtMunkaidoOsszes.Text = 0

        For index = 0 To mkiLista.Count - 1
            DgvTabla.Rows.Add()
            DgvTabla.Item("Datum", index).Value = mkiLista(index).Datum
            DgvTabla.Item("Kezdo_ido", index).Value = mkiLista(index).Kezdo_ido
            DgvTabla.Item("Befejezo_ido", index).Value = mkiLista(index).Befejezo_ido
            DgvTabla.Item("FelhasznaloID", index).Value = mkiLista(index).FelhasznaloID
            GetWorkTimeofDay(DgvTabla, index)
        Next

        DgvTabla.Columns("Datum").ReadOnly = True
        DgvTabla.Columns("Napi_ido").ReadOnly = True
        DgvTabla.Columns("FelhasznaloID").ReadOnly = True
        DgvTabla.Columns("FelhasznaloID").Visible = False

        DgvTabla.Sort(DgvTabla.Columns("Datum"), ListSortDirection.Ascending)

        BtnMentes.Enabled = True

        If User.Role = "Beosztott" Then
            DgvTabla.AllowUserToAddRows = False
            BtnTorles.Enabled = False
        Else
            DgvTabla.AllowUserToAddRows = True
            BtnTorles.Enabled = True
            DgvTabla.Item("Datum", DgvTabla.Rows.Count - 1).ReadOnly = False
        End If

    End Sub

    'Az első bejelentkezéskor legenerált alapértelmezett munkaidők generálása a felhasználó munkaideje alapján
    Private Sub SetDefaultWorkingHours(email As String)

        Dim datum, napStr, honapStr As String
        Dim napok, munkaido, felhaszid, ev, honap As Integer
        Dim kido, bido As DateTime
        Dim row As String()
        Dim dateResult As DateTime
        Dim evhonap = GetYearAndMonth()
        Dim szabadsagdatum As New List(Of Date)

        mkiLista.Clear()
        mkiMan.GetMunkaidok(mkiLista, email, evhonap.item(itemEv), evhonap.item(itemHonap))
        szabLista.Clear()
        szabMan.GetSzabadsagok(szabLista, email, evhonap.item(itemEv), evhonap.item(itemHonap))

        For Each felhasznalo In fhszLista
            If felhasznalo.Email = email Then
                munkaido = IsInteger(felhasznalo.Munkaido)
                felhaszid = IsInteger(felhasznalo.Id)
                Exit For
            End If
        Next

        For Each item In szabLista
            szabadsagdatum.Add(item.Datum)
        Next

        ClearDataGridView(DgvTabla)

        DgvTabla.Columns.Add("Datum", "Dátum")
        DgvTabla.Columns.Add("Kezdo_ido", "Kezdés")
        DgvTabla.Columns.Add("Befejezo_ido", "Befejezés")
        DgvTabla.Columns.Add("FelhasznaloID", "Felhasználó ID")

        DgvTabla.Columns("Datum").ValueType = GetType(Date)
        DgvTabla.Columns("Kezdo_ido").ValueType = GetType(DateTime)
        DgvTabla.Columns("Befejezo_ido").ValueType = GetType(DateTime)
        DgvTabla.Columns("FelhasznaloID").ValueType = GetType(Integer)

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
                Dim ismunkanap = IsHolidayOrWeekend(dateResult, unnepLista)
                If ismunkanap Then
                    If Not szabadsagdatum.Contains(dateResult) Then
                        kido = dateResult.AddHours(8.0)
                        bido = dateResult.AddHours(kido.Hour() + munkaido)
                        If mkiLista.Count > 1 Then
                            'bido.Hour & ":" & kido.Minute,
                            If Not mkiLista(datumindex).Datum = dateResult Then
                                row = {
                                        dateResult,
                                        kido.ToString("MM/dd/yyyy HH:mm:ss tt"),
                                        bido.ToString("MM/dd/yyyy HH:mm:ss tt"),
                                        felhaszid
                                     }
                            Else
                                dateResult = mkiLista(datumindex).Datum
                                kido = mkiLista(datumindex).Kezdo_ido
                                bido = mkiLista(datumindex).Befejezo_ido
                                felhaszid = mkiLista(datumindex).FelhasznaloID
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
                                    kido.ToString("MM/dd/yyyy HH:mm:ss tt"),
                                    bido.ToString("MM/dd/yyyy HH:mm:ss tt"),
                                    felhaszid
                                }
                        End If
                        DgvTabla.Rows.Add(row)
                    End If
                End If
            End If
        Next

        mkiMan.InsertOrUpdate(DgvTabla)
        ClearDataGridView(DgvTabla)

    End Sub

    'A felhasználók adatainak lekérdezése az SQL Adatbázisból
    Private Sub GetUserData()

        ClearDataGridView(DgvTabla)

        DgvTabla.Columns.Add("id", "id")
        DgvTabla.Columns.Add("Nev", "Név")
        DgvTabla.Columns.Add("Jelszo", "Jelszó")
        DgvTabla.Columns.Add("Email", "E-mail")
        DgvTabla.Columns.Add("Munkaido", "Munkaidő")

        DgvTabla.Columns("id").ValueType = GetType(Integer)
        DgvTabla.Columns("Nev").ValueType = GetType(String)
        DgvTabla.Columns("Jelszo").ValueType = GetType(String)
        DgvTabla.Columns("Email").ValueType = GetType(String)
        DgvTabla.Columns("Munkaido").ValueType = GetType(Decimal)

        For index = 0 To fhszLista.Count - 1
            DgvTabla.Rows.Add()
            DgvTabla.Item("id", index).Value = fhszLista(index).Id
            DgvTabla.Item("Nev", index).Value = fhszLista(index).Nev
            DgvTabla.Item("Jelszo", index).Value = fhszLista(index).Jelszo
            DgvTabla.Item("Email", index).Value = fhszLista(index).Email
            DgvTabla.Item("Munkaido", index).Value = fhszLista(index).Munkaido
        Next

        DgvTabla.Columns("id").Visible = False
        DgvTabla.Columns("id").ReadOnly = True

        If User.role = "Admin" Then
            DgvTabla.Columns("Jelszo").Visible = True
            DgvTabla.Columns("Jelszo").ReadOnly = False
        Else
            DgvTabla.Columns("Jelszo").Visible = False
            DgvTabla.Columns("Jelszo").ReadOnly = True
        End If

        DgvTabla.Sort(DgvTabla.Columns("Nev"), ListSortDirection.Ascending)
        DgvTabla.AllowUserToAddRows = True

        BtnMentes.Enabled = True
        BtnTorles.Enabled = True
        TxtMunkaidoOsszes.Visible = False
        LblMunkaidoOsszes.Visible = False
        LblOra.Visible = False

    End Sub

    'A felhasználók kiválasztásához szükséges ListBox feltöltése
    Private Sub GetFszhLtb()

        Dim szures As New Dictionary(Of String, String)

        For Each felhasz In fhszLista
            szures.Add(felhasz.Nev, felhasz.Email)
        Next

        Dim pbs As New BindingSource
        pbs.DataSource = szures
        LtbFelhasznalok.DataSource = pbs
        LtbFelhasznalok.DisplayMember = "Key"
        LtbFelhasznalok.ValueMember = "Value"

    End Sub

    'A munkaidő összesítéshez szükséges segédtábla készítése
    Private Function GetSummaryTable()

        ClearDataGridView(DgvUj)
        ClearDataGridView(DgvTabla)

        Dim evhonap = GetYearAndMonth()
        Dim felhaszMunkaido As New List(Of List(Of Munkaidok))

        If User.Role = "Beosztott" Then
            Dim ujmunkaido As New List(Of Munkaidok)
            mkiMan.GetMunkaidok(ujmunkaido, userEmail, evhonap.item(itemEv), evhonap.item(itemHonap))
            felhaszMunkaido.Add(ujmunkaido)
        Else
            For index = 0 To fhszLista.Count - 1
                Dim ujmunkaido As New List(Of Munkaidok)
                mkiMan.GetMunkaidok(ujmunkaido, fhszLista(index).Email, evhonap.item(itemEv), evhonap.item(itemHonap))
                felhaszMunkaido.Add(ujmunkaido)
            Next
        End If

        DgvUj.Columns.Add("Nev", "Név")
        DgvUj.Columns.Add("Email", "E-mail")
        DgvUj.Columns.Add("Munkaido", "Munkaidő")
        DgvUj.Columns.Add("Datum", "Dátum")
        DgvUj.Columns.Add("Kezdo_ido", "Kezdés")
        DgvUj.Columns.Add("Befejezo_ido", "Befejezés")
        DgvUj.Columns.Add("Napi_ido", "Napi munkaidő")

        Dim rowindex = 0
        For Each szemelyes In felhaszMunkaido
            For Index = 0 To szemelyes.Count - 1
                DgvUj.Rows.Add()
                For findex = 0 To fhszLista.Count - 1
                    If szemelyes(Index).FelhasznaloID = fhszLista(findex).Id Then
                        DgvUj.Item("Nev", rowindex).Value = fhszLista(findex).Nev
                        DgvUj.Item("Email", rowindex).Value = fhszLista(findex).Email
                        DgvUj.Item("Munkaido", rowindex).Value = fhszLista(findex).Munkaido
                        Exit For
                    End If
                Next
                DgvUj.Item("Datum", rowindex).Value = szemelyes(Index).Datum
                DgvUj.Item("Kezdo_ido", rowindex).Value = szemelyes(Index).Kezdo_ido
                DgvUj.Item("Befejezo_ido", rowindex).Value = szemelyes(Index).Befejezo_ido
                GetWorkTimeofDay(DgvUj, rowindex)
                rowindex += 1
            Next
        Next

        DgvUj.AllowUserToAddRows = False
        DgvUj.ReadOnly = True
        TxtMunkaidoOsszes.Visible = False
        LblOra.Visible = False
        LblMunkaidoOsszes.Visible = False
        BtnMentes.Enabled = False
        BtnTorles.Enabled = False

        Return DgvUj
    End Function

    'Új ünnepnapok vagy mostaniak törlésére szolgáló függvény Admin jogként
    Private Sub SetHoliday()

        ClearDataGridView(DgvTabla)

        DgvTabla.Columns.Add("Datum", "Dátum")
        DgvTabla.Columns.Add("Tipus", "Típus")

        DgvTabla.Columns("Datum").ValueType = GetType(Date)
        DgvTabla.Columns("Tipus").ValueType = GetType(Integer)

        For index = 0 To unnepLista.Count - 1
            DgvTabla.Rows.Add()
            DgvTabla.Item("Datum", index).Value = unnepLista(index).Datum
            DgvTabla.Item("Tipus", index).Value = unnepLista(index).Tipus
        Next

        BtnMentes.Enabled = True
        BtnTorles.Enabled = True
    End Sub

    'A felhasználók munkaidejének összesítése az adott hónapra
    Private Sub GetWorkingHoursSummary()

        ClearDataGridView(DgvTabla)

        Dim kulonbseg As Decimal
        Dim row As String()
        Dim nevLista As New List(Of String)
        Dim rowCount As Integer
        Dim tabla = GetSummaryTable()

        DgvTabla.Columns.Add("Nev", "Név")
        DgvTabla.Columns.Add("Email", "E-Mail")
        DgvTabla.Columns.Add("Eloirtora", "Előírt óraszám")
        DgvTabla.Columns.Add("Teljesora", "Teljesített óraszám")
        DgvTabla.Columns.Add("Kulonbseg", "Különbség")

        DgvTabla.Columns("Nev").ValueType = GetType(String)
        DgvTabla.Columns("Email").ValueType = GetType(String)
        DgvTabla.Columns("Eloirtora").ValueType = GetType(Decimal)
        DgvTabla.Columns("Teljesora").ValueType = GetType(Decimal)
        DgvTabla.Columns("Kulonbseg").ValueType = GetType(Decimal)

        rowCount = tabla.Rows.Count - 1

        For index = 0 To rowCount
            Dim nev = tabla.Item("Nev", index).Value
            Dim email = tabla.Item("email", index).Value
            Dim munkanap = GetWeekdaysNumber() - GetSzabadsagCount(email)
            Dim diffworkhours = GetDifferenceWorkingHours(munkanap, tabla.Item("napi_ido", index).Value, IsInteger(tabla.Item("munkaido", index).Value), IsDate(tabla.Item("datum", index).Value))
            Dim napiIdo = diffworkhours.Item(itemNapiIdo)
            Dim munkaIdo = diffworkhours.Item(itemMunkaIdo)
            Dim nevIndex = nevLista.IndexOf(nev)

            If nevIndex = -1 Then
                nevLista.Add(nev)
                kulonbseg = napiIdo - munkaIdo
                row = New String() {
                                nev,
                                    email,
                                    munkaIdo,
                                    napiIdo,
                                    kulonbseg
                                }
                DgvTabla.Rows.Add(row)
            Else
                Dim eloirtora = IsDecimal(DgvTabla.Item("eloirtora", nevIndex).Value)
                Dim teljesora = IsDecimal(DgvTabla.Item("teljesora", nevIndex).Value)
                DgvTabla.Item("teljesora", nevIndex).Value = teljesora + napiIdo
                DgvTabla.Item("kulonbseg", nevIndex).Value = (teljesora + napiIdo) - eloirtora
            End If
        Next

        DgvTabla.Sort(DgvTabla.Columns("Nev"), ListSortDirection.Ascending)

        DgvTabla.AllowUserToAddRows = False
        DgvTabla.ReadOnly = True

    End Sub

    'Szabadságok bekérése az adatbázisból, táblázat készítése
    Private Sub GetUserHoliday(email As String)

        ClearDataGridView(DgvTabla)

        Dim evhonap = GetYearAndMonth()

        szabLista.Clear()
        szabMan.GetSzabadsagok(szabLista, email, evhonap.item(itemEv), evhonap.item(itemHonap))

        DgvTabla.Columns.Add("Datum", "Dátum")
        DgvTabla.Columns.Add("Tavollet", "Távollét")
        DgvTabla.Columns.Add("FelhasznaloID", "Felhasználó ID")

        DgvTabla.Columns("Datum").ValueType = GetType(Date)
        DgvTabla.Columns("Tavollet").ValueType = GetType(String)
        DgvTabla.Columns("FelhasznaloID").ValueType = GetType(Integer)

        For index = 0 To szabLista.Count - 1
            DgvTabla.Rows.Add()
            DgvTabla.Item("Datum", index).Value = szabLista(index).Datum
            DgvTabla.Item("Tavollet", index).Value = szabLista(index).Tavollet
            DgvTabla.Item("FelhasznaloID", index).Value = szabLista(index).FelhasznaloID
        Next

        DgvTabla.Columns("FelhasznaloID").ReadOnly = True
        DgvTabla.Columns("FelhasznaloID").Visible = False
        DgvTabla.Sort(DgvTabla.Columns("Datum"), ListSortDirection.Ascending)

        BtnMentes.Enabled = True
        BtnTorles.Enabled = True
        TxtMunkaidoOsszes.Visible = False
        LblOra.Visible = False
        LblMunkaidoOsszes.Visible = False
        DgvTabla.AllowUserToAddRows = True

    End Sub

    'Jogkorok bekérése módosításhoz, Admin jogként
    Private Sub GetJogkorok()

        ClearDataGridView(DgvTabla)

        Dim jogkorok As New List(Of Jogkorok)
        jgkkMan.GetJogkorok(jogkorok)

        DgvTabla.Columns.Add("Nev", "Név")
        DgvTabla.Columns.Add("Jogkor", "Jogkör")
        DgvTabla.Columns.Add("FelhasznaloID", "Felhasználó ID")


        DgvTabla.Columns("Nev").ValueType = GetType(String)
        DgvTabla.Columns("Jogkor").ValueType = GetType(String)
        DgvTabla.Columns("FelhasznaloID").ValueType = GetType(Integer)

        For index = 0 To fhszLista.Count - 1
            DgvTabla.Rows.Add()
            DgvTabla.Item("Nev", index).Value = fhszLista(index).Nev
            For jkindex = 0 To jogkorok.Count - 1
                If fhszLista(index).Id = jogkorok(jkindex).FelhasznaloID Then
                    DgvTabla.Item("Jogkor", index).Value = jogkorok(jkindex).Jogkor
                    DgvTabla.Item("FelhasznaloID", index).Value = jogkorok(jkindex).FelhasznaloID
                End If
            Next
        Next

        DgvTabla.Columns("Nev").ReadOnly = True
        DgvTabla.Columns("FelhasznaloID").ReadOnly = True
        DgvTabla.Columns("FelhasznaloID").Visible = False

        BtnMentes.Enabled = True
        BtnTorles.Enabled = False

    End Sub

    'Új felhasználóknak beállítja az alapértelmezett Beosztott jogkört
    Private Sub NewFhszsetJogkor()

        ClearDataGridView(DgvUj)

        DgvUj.Columns.Add("id", "Id")
        DgvUj.Columns("id").ValueType = GetType(Integer)

        For index = 0 To fhszLista.Count - 1
            DgvUj.Rows.Add()
            DgvUj.Item("id", index).Value = fhszLista(index).Id
        Next

        jgkkMan.InsertJogkorok(DgvUj)
        ClearDataGridView(DgvUj)

    End Sub

#End Region

#Region "Funkciógombok működtetése"

    'Felhasználó adatainak lekérdezése
    Private Sub BtnFelhasznalok_Click(sender As Object, e As EventArgs) Handles BtnFelhasznalok.Click
        GetUserData()
    End Sub

    'Mentés gomb működtetése
    Private Sub BtnMentes_Click(sender As Object, e As EventArgs) Handles BtnMentes.Click
        If CheckTable(DgvTabla) = 1 Then
            editedRows.ForEach(Sub(i) fhszMan.InsertOrUpdate(DgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
            fhszLista.Clear()
            fhszMan.GetFelhasznalok(fhszLista)
            GetFszhLtb()
            NewFhszsetJogkor()
        ElseIf CheckTable(DgvTabla) = 0 Then
            editedRows.ForEach(Sub(i) mkiMan.InsertOrUpdate(DgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
        ElseIf CheckTable(DgvTabla) = 2 Then
            editedRows.ForEach(Sub(i) unnepMan.InsertOrUpdate(DgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
            unnepLista.Clear()
            unnepMan.GetUnnepnapok(unnepLista)
            SetHoliday()
        ElseIf CheckTable(DgvTabla) = 3 Then
            editedRows.ForEach(Sub(i) jgkkMan.UpdateJogkorok(DgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
            GetJogkorok()
        ElseIf CheckTable(DgvTabla) = 4 Then
            editedRows.ForEach(Sub(i) szabMan.InsertOrUpdate(DgvTabla.Rows.Item(i).Cells))
            editedRows.Clear()
        End If
    End Sub

    'Felhasználó munkaidejének lekérdezése
    Private Sub BtnMunkaidoleker_Click(sender As Object, e As EventArgs) Handles BtnMunkaidoleker.Click
        If User.Role = "Beosztott" Then
            GetUserWorkingHours(userEmail)
        Else
            GetUserWorkingHours(LtbFelhasznalok.SelectedValue)
        End If
    End Sub

    'Munkaidő összesítés gomb
    Private Sub BtnMunkaidoossz_Click(sender As Object, e As EventArgs) Handles BtnMunkaidoossz.Click
        GetWorkingHoursSummary()
    End Sub

    'Teszt gomb, később törlésre kerül
    Private Sub BtnGeneralas_Click(sender As Object, e As EventArgs) Handles BtnGeneralas.Click
        SetDefaultWorkingHours(LtbFelhasznalok.SelectedValue)
    End Sub

    'Törlés gomb működtetése
    Private Sub BtnTorles_Click(sender As Object, e As EventArgs) Handles BtnTorles.Click
        If CheckTable(DgvTabla) = 1 Then
            Dim email, nev As String
            Dim id As Integer
            If DgvTabla.SelectedRows.Count > 0 Then
                email = DgvTabla.SelectedRows(0).Cells("Email").Value
                id = DgvTabla.SelectedRows(0).Cells("id").Value
                nev = DgvTabla.SelectedRows(0).Cells("nev").Value
                DgvTabla.Rows.Remove(DgvTabla.SelectedRows(0))
                fhszMan.Delete(nev, email, id)
                fhszLista.Clear()
                fhszMan.GetFelhasznalok(fhszLista)
                GetFszhLtb()
            Else
                MessageBox.Show("Jelölj ki egy sort, mielőtt törölni szeretnéd.")
            End If
        ElseIf CheckTable(DgvTabla) = 2 Then
            If DgvTabla.SelectedRows.Count > 0 Then
                Dim datum = DgvTabla.SelectedRows(0).Cells("Datum").Value
                DgvTabla.Rows.Remove(DgvTabla.SelectedRows(0))
                unnepMan.DeleteUnnepnap(datum)
                unnepLista.Clear()
                unnepMan.GetUnnepnapok(unnepLista)
                SetHoliday()
            Else
                MessageBox.Show("Jelölj ki egy sort, mielőtt törölni szeretnéd.")
            End If
        ElseIf CheckTable(DgvTabla) = 4 Then
            If DgvTabla.SelectedRows.Count > 0 Then
                Dim datum = DgvTabla.SelectedRows(0).Cells("Datum").Value
                Dim felhasznaloid = DgvTabla.SelectedRows(0).Cells("FelhasznaloID").Value
                DgvTabla.Rows.Remove(DgvTabla.SelectedRows(0))
                szabMan.Delete(datum, felhasznaloid)
            Else
                MessageBox.Show("Jelölj ki egy sort, mielőtt törölni szeretnéd.")
            End If
        ElseIf CheckTable(DgvTabla) = 0 Then
            If DgvTabla.SelectedRows.Count > 0 Then
                Dim datum = DgvTabla.SelectedRows(0).Cells("Datum").Value
                Dim felhasznaloid = DgvTabla.SelectedRows(0).Cells("FelhasznaloID").Value
                DgvTabla.Rows.Remove(DgvTabla.SelectedRows(0))
                mkiMan.Delete(datum, felhasznaloid)
            Else
                MessageBox.Show("Jelölj ki egy sort, mielőtt törölni szeretnéd.")
            End If
        End If
    End Sub

    'Ünnepnapok lekérdezése és szerkesztése
    Private Sub BtnUnnep_Click(sender As Object, e As EventArgs) Handles BtnUnnep.Click
        SetHoliday()
    End Sub

    'Jogkörök lekérdezése és szerkesztése
    Private Sub BtnJogkor_Click(sender As Object, e As EventArgs) Handles BtnJogkor.Click
        GetJogkorok()
    End Sub

    'Szabadságok lekérdezése és szerkesztése
    Private Sub BtnSzabadsagleker_Click(sender As Object, e As EventArgs) Handles BtnSzabadsagleker.Click
        If User.Role = "Beosztott" Then
            GetUserHoliday(userEmail)
        Else
            GetUserHoliday(LtbFelhasznalok.SelectedValue)
        End If
    End Sub

    'Kijelentkezés gomb
    Private Sub BtnKijelentkez_Click(sender As Object, e As EventArgs) Handles BtnKijelentkez.Click
        Me.User = New User
        fhszLista = New List(Of Felhasznalok)
        mkiLista = New List(Of Munkaidok)
        unnepLista = New List(Of Unnepnapok)
        szabLista = New List(Of Szabadsagok)
        Me.Visible = False
        Me.WtForm_Load(sender, e)
        Me.Visible = True
    End Sub

#End Region

#Region "DataGridView automatikus függvényei"

    'Cellamódosítás kezdésének érzékelése
    Private Sub DgvTabla_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DgvTabla.CellBeginEdit
        Try
            If DgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value <> Nothing Then
                DgvTabla.Rows.Item(e.RowIndex).Tag = DgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Cellamódosítás befejézésnek érzékelése
    Private Sub DgvTabla_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvTabla.CellEndEdit
        Try
            If DgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value <> Nothing Then
                If Not DgvTabla.Rows.Item(e.RowIndex).Cells.Item(e.ColumnIndex).Value.Equals(DgvTabla.Rows.Item(e.RowIndex).Tag) Then
                    If Not editedRows.Contains(e.RowIndex) Then
                        editedRows.Add(e.RowIndex)
                    End If
                    If CheckTable(DgvTabla) = 0 Then
                        Dim rowCount As New Integer
                        If User.Role = "Beosztott" Then
                            rowCount = DgvTabla.Rows.Count - 1
                        Else
                            CheckDate(DgvTabla)
                            rowCount = DgvTabla.Rows.Count - 2
                        End If
                        TxtMunkaidoOsszes.Text = 0
                        For index = 0 To rowCount
                            GetWorkTimeofDay(DgvTabla, index)
                        Next
                    ElseIf CheckTable(DgvTabla) = 4 Then
                        SaveSzabadsag()
                    End If
                End If
            End If
            DgvTabla.Rows.Item(e.RowIndex).Tag = ""
        Catch ex As Exception
            Console.WriteLine("Hiba a cellamódosításban.")
        End Try
    End Sub

    'Táblázat sorszámozására szolgál
    Private Sub DgvTabla_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DgvTabla.RowPostPaint
        Try
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
        Catch ex As Exception

        End Try
    End Sub

    'Error függvény, még nincs kifejtve!
    Private Sub DgvTabla_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DgvTabla.DataError
        Select Case e.Context
            Case DataGridViewDataErrorContexts.Commit
                MessageBox.Show("Commit error")
            Case DataGridViewDataErrorContexts.CurrentCellChange
                MessageBox.Show("Cell change")
            Case DataGridViewDataErrorContexts.Parsing
                MessageBox.Show("Parsing error")
            Case DataGridViewDataErrorContexts.LeaveControl
                MessageBox.Show("Leave control error")
            Case DataGridViewDataErrorContexts.Formatting
                MessageBox.Show("Format error")
            Case DataGridViewDataErrorContexts.Display
                MessageBox.Show("Display error")
            Case 4864
                MessageBox.Show("Hibás cella formátum!")
            Case Else
                MessageBox.Show("Error:  " & e.Context.ToString())
        End Select
        'If (TypeOf (e.Exception) Is ConstraintException) Then
        '    Dim view As DataGridView = CType(sender, DataGridView)
        '    view.Rows(e.RowIndex).ErrorText = "an error"
        '    view.Rows(e.RowIndex).Cells(e.ColumnIndex) _
        '        .ErrorText = "an error"
        '    MsgBox("error")
        '    e.ThrowException = False
        'End If
    End Sub

#End Region

End Class
