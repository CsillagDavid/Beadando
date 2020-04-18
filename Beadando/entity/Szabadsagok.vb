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

    Private valueTipus As Integer
    Public Property Tipus() As Integer
        Get
            Return valueTipus
        End Get
        Private Set(ByVal value As Integer)
            valueTipus = value
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

    Public Sub New(datum As Date, tipus As Integer, felhasznaloId As Integer)
        Me.Datum = datum
        Me.Tipus = tipus
        Me.FelhasznaloID = felhasznaloId
    End Sub

End Class
