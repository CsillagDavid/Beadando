Public Class Felhasznalok
    Private valueId As Integer
    Public Property id() As Integer
        Get
            Return valueId
        End Get
        Set(ByVal value As Integer)
            valueId = value
        End Set
    End Property

    Private valueNev As String
    Public Property Nev() As String
        Get
            Return valueNev
        End Get
        Set(ByVal value As String)
            valueNev = value
        End Set
    End Property

    Private valueJelszo As String
    Public Property Jelszo() As String
        Get
            Return valueJelszo
        End Get
        Set(ByVal value As String)
            valueJelszo = value
        End Set
    End Property

    Private valueEmail As String
    Public Property Email() As String
        Get
            Return valueEmail
        End Get
        Set(ByVal value As String)
            valueEmail = value
        End Set
    End Property

    Private valueMunkaido As Integer
    Public Property Munkaido() As Integer
        Get
            Return valueMunkaido
        End Get
        Set(ByVal value As Integer)
            valueMunkaido = value
        End Set
    End Property

    Public Sub New(id As Integer, nev As String, jelszo As String, email As String, munkaido As Integer)
        Me.valueId = id
        Me.valueNev = nev
        Me.valueJelszo = jelszo
        Me.valueEmail = email
        Me.valueMunkaido = munkaido
    End Sub

End Class
