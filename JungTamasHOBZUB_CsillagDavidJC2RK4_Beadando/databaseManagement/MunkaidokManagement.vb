Imports System.Data.SqlClient

Public Class MunkaidokManagement

    Private sqlConnection As SqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    'Munkaidők frissítése vagy új felvétele tábla megadásával
    Public Sub InsertOrUpdate(tablazat As DataGridView)
        Dim rowCount = tablazat.Rows.Count
        cmd = con.CreateCommand()
        cmd.CommandText = "InsertOrUpdateMunkaidok"
        cmd.CommandType = CommandType.StoredProcedure
        sqlConnection.SqlConnect()
        For index = 0 To rowCount - 2
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@Datum", IsDate(tablazat.Item("Datum", index).Value))
            cmd.Parameters.AddWithValue("@Kezdo_ido", IsDate(tablazat.Item("Kezdo_ido", index).Value))
            cmd.Parameters.AddWithValue("@Befejezo_ido", IsDate(tablazat.Item("Befejezo_ido", index).Value))
            cmd.Parameters.AddWithValue("@FelhasznaloID", IsInteger(tablazat.Item("FelhasznaloID", index).Value))
            cmd.ExecuteNonQuery()
        Next
        sqlConnection.SqlClose()
    End Sub

    'Munkaidők frissítése vagy új felvétele cella megadásával
    Public Sub InsertOrUpdate(cella As DataGridViewCellCollection)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "InsertOrUpdateMunkaidok"
        cmd.Parameters.AddWithValue("@Datum", IsDate(cella.Item("Datum").Value))
        cmd.Parameters.AddWithValue("@Kezdo_ido", IsDate(cella.Item("Kezdo_ido").Value))
        cmd.Parameters.AddWithValue("@Befejezo_ido", IsDate(cella.Item("Befejezo_ido").Value))
        cmd.Parameters.AddWithValue("@FelhasznaloID", IsInteger(cella.Item("FelhasznaloID").Value))
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    'Munkaidő törlése az adatbázisból
    Public Sub Delete(datum As Date, felhasznaloid As Integer)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "DeleteMunkaidok"
        cmd.Parameters.AddWithValue("@Datum", datum)
        cmd.Parameters.AddWithValue("@FelhasznaloID", felhasznaloid)
        cmd.ExecuteNonQuery()
        sqlConnection.SqlClose()
    End Sub

    'A munkaidők lekérése az adatbázisból és ezekből lista generálása
    Public Sub GetMunkaidok(munkaidoklista As List(Of Munkaidok), email As String, ev As String, honap As String)
        sqlConnection.SqlConnect()
        Dim sdr As SqlDataReader
        Dim sda As New SqlDataAdapter
        cmd.CommandType = CommandType.Text
        Dim sqlquery As String = "SELECT M.Datum, M.Kezdo_ido, M.Befejezo_ido, M.FelhasznaloID FROM Munkaidok M                   
                                INNER JOIN Felhasznalok F
                                ON M.FelhasznaloID = F.id
                                WHERE F.Email = '" & email & "' AND M.Datum >= '" & ev & ". " & honap & ". 01' 
                                AND M.Datum < '" & ev & ". " & (honap + 1) & ". 01'"
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
            munkaidoklista.Add(munkaido)
        End While
        sqlConnection.SqlClose()
    End Sub

End Class
