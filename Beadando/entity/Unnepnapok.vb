Public Class Unnepnapok

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

    Public Sub New(datum As String, tipus As Integer)
        Me.Datum = datum
        Me.Tipus = tipus
    End Sub

End Class
