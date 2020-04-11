Imports System.Data.SqlClient

Public Class MunkaidokManagement
    Private sqlConnection As sqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Function FindMunkaidoByDate(Email As String, TolDatum As String, IgDatum As String) As DataTable
        sqlConnection.sqlConnect()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT M.Datum, M.Kezdo_ido, M.Befejezo_ido, M.FelhasznaloID FROM Munkaidok M
                            INNER JOIN Felhasznalok F
                            ON M.FelhasznaloID = F.id
                            WHERE F.Email = '" & Email & "' AND M.Datum >= '" & TolDatum &
                            "And M.Datum < '" & IgDatum
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(cmd)
        sda.Fill(dt)
        sqlConnection.sqlClose()
        Return dt
    End Function

End Class
