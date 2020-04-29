Public Class Jogkorok

    'Jogkörök osztály, a jogkörök tábla elemeinek a fogadására

    Private valueFelhasznaloID As Integer
    Public Property FelhasznaloID() As Integer
        Get
            Return valueFelhasznaloID
        End Get
        Private Set(ByVal value As Integer)
            valueFelhasznaloID = value
        End Set
    End Property

    Private valueJogkor As String
    Public Property Jogkor() As String
        Get
            Return valueJogkor
        End Get
        Private Set(ByVal value As String)
            valueJogkor = value
        End Set
    End Property

    Public Sub New(felhasznaloID As Integer, jogkor As String)
        Me.FelhasznaloID = felhasznaloID
        Me.Jogkor = jogkor
    End Sub

End Class
