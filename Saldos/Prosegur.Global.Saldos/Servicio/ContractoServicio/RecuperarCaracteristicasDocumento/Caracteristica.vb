Namespace RecuperarCaracteristicasDocumento

    Public Class Caracteristica

#Region "[VARIÁVEIS]"

        Private _Descripcion As String
        Private _DistinguirPorNivel As Enumeradores.eDistinguirPorNivel
        Private _PermiteCreacion As Enumeradores.ePermiteCreacion
        Private _SeImprime As Boolean
        Private _EsInterplantas As Boolean
        Private _EsSustituible As Boolean
        Private _EsActaProceso As Boolean
        Private _EsConValores As Boolean
        Private _EsConBultos As Boolean
        Private _EsBasadoEnReporte As Boolean
        Private _BasadoEnSaldos As Boolean
        Private _Campos As Campos
        Private _CamposExtra As CamposExtras

#End Region

#Region "[PROPRIEDADES]"

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(Value As String)
                _Descripcion = Value
            End Set
        End Property

        Public Property DistinguirPorNivel() As Enumeradores.eDistinguirPorNivel
            Get
                Return _DistinguirPorNivel
            End Get
            Set(Value As Enumeradores.eDistinguirPorNivel)
                _DistinguirPorNivel = Value
            End Set
        End Property

        Public Property PermiteCreacion() As Enumeradores.ePermiteCreacion
            Get
                Return _DistinguirPorNivel
            End Get
            Set(Value As Enumeradores.ePermiteCreacion)
                _DistinguirPorNivel = Value
            End Set
        End Property

        Public Property SeImprime() As Boolean
            Get
                Return _SeImprime
            End Get
            Set(Value As Boolean)
                _SeImprime = Value
            End Set
        End Property

        Public Property EsInterplantas() As Boolean
            Get
                Return _EsInterplantas
            End Get
            Set(Value As Boolean)
                _EsInterplantas = Value
            End Set
        End Property

        Public Property EsSustituible() As Boolean
            Get
                Return _EsSustituible
            End Get
            Set(Value As Boolean)
                _EsSustituible = Value
            End Set
        End Property

        Public Property EsActaProceso() As Boolean
            Get
                Return _EsActaProceso
            End Get
            Set(Value As Boolean)
                _EsActaProceso = Value
            End Set
        End Property

        Public Property EsConValores() As Boolean
            Get
                Return _EsConValores
            End Get
            Set(Value As Boolean)
                _EsConValores = Value
            End Set
        End Property

        Public Property EsConBultos() As Boolean
            Get
                Return _EsConBultos
            End Get
            Set(Value As Boolean)
                _EsConBultos = Value
            End Set
        End Property

        Public Property EsBasadoEnReporte() As Boolean
            Get
                Return _EsBasadoEnReporte
            End Get
            Set(Value As Boolean)
                _EsBasadoEnReporte = Value
            End Set
        End Property

        Public Property BasadoEnSaldos() As Enumeradores.eBasadoEnSaldos
            Get
                Return _BasadoEnSaldos
            End Get
            Set(Value As Enumeradores.eBasadoEnSaldos)
                _BasadoEnSaldos = Value
            End Set
        End Property

        Public Property Campos() As Campos
            Get
                If _Campos Is Nothing Then
                    _Campos = New Campos()
                End If
                Return _Campos
            End Get
            Set(Value As Campos)
                _Campos = Value
            End Set
        End Property

        Public Property CamposExtra() As CamposExtras
            Get
                If _CamposExtra Is Nothing Then
                    _CamposExtra = New CamposExtras()
                End If
                Return _CamposExtra
            End Get
            Set(Value As CamposExtras)
                _CamposExtra = Value
            End Set
        End Property

#End Region

    End Class

End Namespace