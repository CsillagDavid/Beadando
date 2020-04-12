﻿Imports System.Data.SqlClient

Public Class JogkorokManagement
    Private sqlConnection As sqlConn
    Private con As New SqlConnection
    Private cmd As New SqlCommand

    Public Sub New()
        sqlConnection = New sqlConn()
        con = sqlConnection.con
        cmd = sqlConnection.cmd
    End Sub

    Public Sub Update(Cells As DataGridViewCellCollection)
        sqlConnection.sqlConnect()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "UpdateJogkorok"
        cmd.Parameters.AddWithValue("@FelhasznaloID", Cells.Item("FelhasznaloID").Value)
        cmd.Parameters.AddWithValue("@Jogkor", Cells.Item("Jogkor").Value)
        cmd.ExecuteNonQuery()
        sqlConnection.sqlClose()
    End Sub

End Class
