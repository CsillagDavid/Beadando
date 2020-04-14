Public Class Felhasznalok
    Private Id As Integer
    Public Property propId() As String
        Get
            Return Id
        End Get
        Set(ByVal value As String)
            Id = value
        End Set
    End Property

    Private Nev As String
    Public Property propNev() As String
        Get
            Return Nev
        End Get
        Set(ByVal value As String)
            Nev = value
        End Set
    End Property

    Private Jelszo As String
    Public Property propJelszo() As String
        Get
            Return Jelszo
        End Get
        Set(ByVal value As String)
            Jelszo = value
        End Set
    End Property

    Private Email As String
    Public Property propEmail() As String
        Get
            Return Email
        End Get
        Set(ByVal value As String)
            Email = value
        End Set
    End Property

    Private Munkaido As Integer
    Public Property propMunkaido() As String
        Get
            Return Munkaido
        End Get
        Set(ByVal value As String)
            Munkaido = value
        End Set
    End Property

    Public Sub New(id As Integer, nev As String, jelszo As String, email As String, munkaido As Integer)
        Me.id = id
        Me.Nev = nev
        Me.Jelszo = jelszo
        Me.Email = email
        Me.Munkaido = munkaido
    End Sub

End Class
