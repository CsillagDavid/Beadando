Public Class Szabadsagok

    'Szabadságok osztály, a szabadság elemeinek fogadására

    Private valueDatum As Date
    Public Property Datum() As Date
        Get
            Return valueDatum
        End Get
        Private Set(ByVal value As Date)
            valueDatum = value
        End Set
    End Property

    Private valueTavollet As String
    Public Property Tavollet() As String
        Get
            Return valueTavollet
        End Get
        Private Set(ByVal value As String)
            valueTavollet = value
        End Set
    End Property

    Private valueFelhasznaloID As Integer
    Public Property FelhasznaloID() As Integer
        Get
            Return valueFelhasznaloID
        End Get
        Private Set(ByVal value As Integer)
            valueFelhasznaloID = value
        End Set
    End Property

    Public Sub New(datum As Date, tavollet As String, felhasznaloId As Integer)
        Me.Datum = datum
        Me.Tavollet = tavollet
        Me.FelhasznaloID = felhasznaloId
    End Sub

End Class
