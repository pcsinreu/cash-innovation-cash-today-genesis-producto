'Classe visivel somente no projeto
Public Class MapeoEntidadeCodigoAjeno

    Private _CodTipoTablaGenesis As String
    Private _Entidade As String
    Private _OidTablaGenesis As String
    Private _CodTablaGenesis As String
    Private _DesTablaGenesis As String



    Public Property CodTipoTablaGenesis() As String
        Get
            Return _CodTipoTablaGenesis
        End Get
        Set(value As String)
            _CodTipoTablaGenesis = value
        End Set
    End Property


    'Nome da tabela do genesis
    Public Property Entidade() As String
        Get
            Return _Entidade
        End Get
        Set(value As String)
            _Entidade = value
        End Set
    End Property

    Public Property OidTablaGenesis() As String
        Get
            Return _OidTablaGenesis
        End Get
        Set(value As String)
            _OidTablaGenesis = value
        End Set
    End Property

    Public Property CodTablaGenesis() As String
        Get
            Return _CodTablaGenesis
        End Get
        Set(value As String)
            _CodTablaGenesis = value
        End Set
    End Property

    Public Property DesTablaGenesis() As String
        Get
            Return _DesTablaGenesis
        End Get
        Set(value As String)
            _DesTablaGenesis = value
        End Set
    End Property

End Class
