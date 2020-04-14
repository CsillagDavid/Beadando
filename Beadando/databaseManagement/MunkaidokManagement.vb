Imports System.Data.SqlClient

Public Class MunkaidokManagement
    Private sqlConnection As sqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand
    Dim itemEv = "ev"
    Dim itemHonap = "honap"
    Dim itemMunkaIdo = "munkaido"
    Dim itemNapiIdo = "napiido"

    Public Sub New()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Function GetIdAndMunkaidoId(Email As String) As DataTable
        sqlConnection.sqlConnect()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT F.id, F.Munkaido FROM Felhasznalok F
                            WHERE F.Email = '" & Email & "'"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
        Return dt
    End Function

    Public Function FindByDate(Email As String, evhonap As Dictionary(Of String, Integer)) As DataTable
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT M.Datum, M.Kezdo_ido, M.Befejezo_ido, M.FelhasznaloID FROM Munkaidok M
                   INNER JOIN Felhasznalok F
                   ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & Email & "' AND M.Datum >= '" & evhonap.Item(itemEv) & ". " & evhonap.Item(itemHonap) & ". 01' 
                            AND M.Datum < '" & evhonap.Item(itemEv) & ". " & (evhonap.Item(itemHonap) + 1) & ". 01'"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
        Return dt
    End Function

    Public Sub InsertOrUpdate(tabla As DataGridView, dgvTabla As DataGridView)
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
    End Sub

    Public Sub InsertOrUpdate(Cells As DataGridViewCellCollection)
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

    Public Function FindByEmailAndDate(Email As String, evhonap As Dictionary(Of String, Integer)) As DataTable
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT M.Datum, F.Munkaido, M.Kezdo_ido, M.Befejezo_ido, F.id FROM Munkaidok M
                   INNER JOIN Felhasznalok F
                   ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & Email & "' AND M.Datum >= '" & evhonap.Item(itemEv) & ". " & evhonap.Item(itemHonap) & ". 01' 
                            AND M.Datum < '" & evhonap.Item(itemEv) & ". " & (evhonap.Item(itemHonap) + 1) & ". 01'"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
        Return dt
    End Function

    Public Sub getMunkaidok(list As List(Of Munkaidok), Email As String, KezdoDatum As String, BefejezoDatum As String)
        sqlConnection.sqlConnect()
        Dim sdr As SqlDataReader
        Dim sda As New SqlDataAdapter
        cmd.CommandType = CommandType.Text
        Dim sqlquery As String = "SELECT M.Datum, M.Kezdo_ido, M.Befejezo_ido, M.FelhasznaloID FROM Munkaidok M                   
                                INNER JOIN Felhasznalok F
                                ON M.FelhasznaloID = F.id
                                WHERE F.Email = '" & Email & "' AND M.Datum >= '" & KezdoDatum & ". " & BefejezoDatum & ". 01' 
                                AND M.Datum < '" & KezdoDatum & ". " & (BefejezoDatum + 1) & ". 01'"
        cmd.CommandText = sqlquery
        cmd.Connection = con
        sda.SelectCommand = cmd
        sdr = cmd.ExecuteReader()
        While sdr.Read
            Dim munkaido = New Munkaidok(
                sdr.Item("Datum"),
                sdr.Item("Kezdo_ido"),
                sdr.Item("Befejezo_ido"),
                sdr.Item("FelhasznaloID")
                )
            list.Add(munkaido)
        End While
        sqlConnection.sqlClose()
    End Sub

End Class
