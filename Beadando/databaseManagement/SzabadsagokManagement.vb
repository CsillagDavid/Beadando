Imports System.Data.SqlClient

Public Class SzabadsagokManagement
    Private sqlConnection As SqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New SqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Function GetAll() As List(Of Szabadsagok)
        Dim lista As New List(Of Szabadsagok)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM Szabadsagok"
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read()
            Dim szabadsag = New Szabadsagok(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetInt32(3))
            lista.Add(szabadsag)
        End While
        sqlConnection.SqlClose()
        Return lista
    End Function

    Public Function GetByEmail(Email As String) As List(Of Szabadsagok)
        Dim lista As New List(Of Szabadsagok)
        sqlConnection.SqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT SZ.id, SZ.Datum, SZ.Tipus, SZ.FelhasznaloID
                            FROM Szabadsagok SZ INNER JOIN Felhasznalok F ON SZ.FelhasznaloID = F.id
                            WHERE F.Email='" & Email & "'"
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read()
            Dim szabadsag = New Szabadsagok(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetInt32(3))
            lista.Add(szabadsag)
        End While
        sqlConnection.SqlClose()
        Return lista
    End Function



End Class
