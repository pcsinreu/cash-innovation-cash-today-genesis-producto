<Serializable()> _
Public Class Elemento

#Region "[VARIÁVEIS]"

    Private _Id As Long
    Private _IdPS As String
    Private _Descripcion As String
    Private _EsTesouro As Boolean
    Private _SiDispone As Boolean
    Private _TipoCentroProcesso As TipoCentroProceso
    Private _Planta As Planta
    Private _Matriz As CentroProceso
    Private _Motivos As Motivos

#End Region

#Region "[PROPRIEDADES]"

    Public Property Id() As Long
        Get
            Return _Id
        End Get
        Set(value As Long)
            _Id = value
        End Set
    End Property

    Public Property IdPS() As String
        Get
            Return _IdPS
        End Get
        Set(value As String)
            _IdPS = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(value As String)
            _Descripcion = value
        End Set
    End Property

    Public Property EsTesouro() As Boolean
        Get
            Return _EsTesouro
        End Get
        Set(value As Boolean)
            _EsTesouro = value
        End Set
    End Property

    Public Property SiDispone() As Boolean
        Get
            Return _SiDispone
        End Get
        Set(value As Boolean)
            _SiDispone = value
        End Set
    End Property

    Public Property TipoCentroProcesso() As TipoCentroProceso
        Get
            Return _TipoCentroProcesso
        End Get
        Set(value As TipoCentroProceso)
            _TipoCentroProcesso = value
        End Set
    End Property

    Public Property Planta() As Planta
        Get
            Return _Planta
        End Get
        Set(value As Planta)
            _Planta = value
        End Set
    End Property

    Public Property Matriz() As CentroProceso
        Get
            Return _Matriz
        End Get
        Set(value As CentroProceso)
            _Matriz = value
        End Set
    End Property

    Public Property Motivos() As Motivos
        Get
            Return _Motivos
        End Get
        Set(value As Motivos)
            _Motivos = value
        End Set
    End Property

#End Region

End Class
