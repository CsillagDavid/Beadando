Imports System.Data.SqlClient

Public Class Database


    Public Function sqlconnection()
        ' Open a database connection.
        Dim strConnection As String =
           "Server = tcp:5.187.213.233,1433; Database = wtDB; User Id = sa; Password = 2SS3BJSDbu"
        Dim conn As SqlConnection = New SqlConnection(strConnection)
        conn.Open()
        MsgBox("Test")

        conn.Close()

    End Function
End Class
