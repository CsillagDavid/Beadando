Public Class Szabadsagok

    Private valueId As Integer
    Public Property id() As Integer
        Get
            Return valueId
        End Get
        Set(ByVal value As Integer)
            valueId = value
        End Set
    End Property

    Private valueDatum As String
    Public Property Datum() As String
        Get
            Return valueDatum
        End Get
        Set(ByVal value As String)
            valueDatum = value
        End Set
    End Property

    Private valueTipus As Integer
    Public Property Tipus() As Integer
        Get
            Return valueTipus
        End Get
        Set(ByVal value As Integer)
            valueTipus = value
        End Set
    End Property

    Private valueFelhasznaloID As Integer
    Public Property FelhasznaloID() As Integer
        Get
            Return valueFelhasznaloID
        End Get
        Set(ByVal value As Integer)
            valueFelhasznaloID = value
        End Set
    End Property

    Public Sub New(id As Integer, datum As Date, tipus As Integer, felhasznaloId As Integer)
        Me.id = id
        Me.Datum = datum
        Me.Tipus = tipus
        Me.FelhasznaloID = felhasznaloId
    End Sub

End Class
