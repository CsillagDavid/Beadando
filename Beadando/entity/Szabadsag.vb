Public Class Szabadsag
    Public Id As Integer
    Public Datum As Date
    Public Tipus As Integer
    Public FelhasznaloId As Integer

    Public Sub New(id As Integer, datum As Date, tipus As Integer, felhasznaloId As Integer)
        Me.Id = id
        Me.Datum = datum
        Me.Tipus = tipus
        Me.FelhasznaloId = felhasznaloId
    End Sub
End Class
