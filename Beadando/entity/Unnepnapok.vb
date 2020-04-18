Public Class Unnepnapok

    'Ünnepnapok osztály, az ünnepnapok tábla értékeinek fogadására

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

    Public Sub New(datum As String, tipus As Integer)
        Me.Datum = datum
        Me.Tipus = tipus
    End Sub

End Class
