Public Class Munkaidok

    'Munkaidők osztály, a munkaidők tábla elemeinek a fogadására

    Private valueDatum As Date
    Public Property Datum() As Date
        Get
            Return valueDatum
        End Get
        Private Set(ByVal value As Date)
            valueDatum = value
        End Set
    End Property

    Private valueKezdo_ido As DateTime
    Public Property Kezdo_ido() As DateTime
        Get
            Return valueKezdo_ido
        End Get
        Private Set(ByVal value As DateTime)
            valueKezdo_ido = value
        End Set
    End Property

    Private valueBefejezo_ido As DateTime
    Public Property Befejezo_ido() As DateTime
        Get
            Return valueBefejezo_ido
        End Get
        Private Set(ByVal value As DateTime)
            valueBefejezo_ido = value
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

    Public Sub New(datum As Date, kezdo_ido As DateTime, befejezo_ido As DateTime, felhasznaloID As Integer)
        Me.Datum = datum
        Me.Kezdo_ido = kezdo_ido
        Me.Befejezo_ido = befejezo_ido
        Me.FelhasznaloID = felhasznaloID
    End Sub

End Class
