Imports System.Data.SqlClient

Public Class SzabadsagManagement
    Private sqlConnection As sqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Function GetAll() As List(Of Szabadsag)
        Dim lista As New List(Of Szabadsag)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT * FROM Szabadsag"
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read()
            Dim szabadsag = New Szabadsag(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetInt32(3))
            lista.Add(szabadsag)
        End While
        sqlConnection.sqlClose()
        Return lista
    End Function

    Public Function GetByEmail(Email As String) As List(Of Szabadsag)
        Dim lista As New List(Of Szabadsag)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT sz.id, sz.Datum, sz.Tipus, sz.FelhasznaloID
                            FROM Szabadsag sz INNER JOIN Felhasznalok f ON sz.FelhasznaloID = f.id
                            WHERE f.Email='" & Email & "'"
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read()
            Dim szabadsag = New Szabadsag(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetInt32(3))
            lista.Add(szabadsag)
        End While
        sqlConnection.sqlClose()
        Return lista
    End Function



End Class
