Public Class Munkaidok
    Private valueDatum As String
    Public Property Datum() As String
        Get
            Return valueDatum
        End Get
        Set(ByVal value As String)
            valueDatum = value
        End Set
    End Property

    Private valueKezdo_ido As Decimal
    Public Property Kezdo_ido() As Decimal
        Get
            Return valueKezdo_ido
        End Get
        Set(ByVal value As Decimal)
            valueKezdo_ido = value
        End Set
    End Property

    Private valueBefejezo_ido As Decimal
    Public Property Befejezo_ido() As Decimal
        Get
            Return valueBefejezo_ido
        End Get
        Set(ByVal value As Decimal)
            valueBefejezo_ido = value
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

    Public Sub New(valueDatum As String, valueKezdo_ido As Decimal, valueBefejezo_ido As Decimal, valueFelhasznaloID As Integer)
        Me.valueDatum = valueDatum
        Me.valueKezdo_ido = valueKezdo_ido
        Me.valueBefejezo_ido = valueBefejezo_ido
        Me.valueFelhasznaloID = valueFelhasznaloID
    End Sub
End Class
