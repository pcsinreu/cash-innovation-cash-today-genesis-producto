Namespace ManipularDocumentos

    ''' <summary>
    ''' Documento
    ''' </summary>
    <Serializable()> _
    Public Class Documento

#Region "[VARIÁVEIS]"

        Private _Accion As Enumeradores.eAccion
        Private _IdDocumento As String
        Private _IdFormulario As String
        Private _NumExterno As String
        Private _IdCentroProcesoOrigen As String
        Private _IdClienteOrigen As String
        Private _IdCanalOrigen As String
        Private _IdCentroProcesoDestino As String
        Private _IdClienteDestino As String
        Private _IdCanalDestino As String
        Private _CodigoCliente As String
        Private _CodigoSubCliente As String
        Private _CamposExtras As CamposExtras
        Private _Bultos As Bultos
        Private _Detalles As Detalles
        Private _Parciales As Parciales

#End Region

#Region "[PROPRIEDADES]"

        Public Property Accion() As Enumeradores.eAccion
            Get
                Return _Accion
            End Get
            Set(value As Enumeradores.eAccion)
                _Accion = value
            End Set
        End Property

        Public Property IdDocumento() As String
            Get
                Return _IdDocumento
            End Get
            Set(value As String)
                _IdDocumento = value
            End Set
        End Property

        Public Property IdFormulario() As String
            Get
                Return _IdFormulario
            End Get
            Set(value As String)
                _IdFormulario = value
            End Set
        End Property

        Public Property NumExterno() As String
            Get
                Return _NumExterno
            End Get
            Set(value As String)
                _NumExterno = value
            End Set
        End Property

        Public Property IdCentroProcesoOrigen() As String
            Get
                Return _IdCentroProcesoOrigen
            End Get
            Set(value As String)
                _IdCentroProcesoOrigen = value
            End Set
        End Property

        Public Property IdClienteOrigen() As String
            Get
                Return _IdClienteOrigen
            End Get
            Set(value As String)
                _IdClienteOrigen = value
            End Set
        End Property

        Public Property IdCanalOrigen() As String
            Get
                Return _IdCanalOrigen
            End Get
            Set(value As String)
                _IdCanalOrigen = value
            End Set
        End Property

        Public Property IdCentroProcesoDestino() As String
            Get
                Return _IdCentroProcesoDestino
            End Get
            Set(value As String)
                _IdCentroProcesoDestino = value
            End Set
        End Property

        Public Property IdClienteDestino() As String
            Get
                Return _IdClienteDestino
            End Get
            Set(value As String)
                _IdClienteDestino = value
            End Set
        End Property

        Public Property IdCanalDestino() As String
            Get
                Return _IdCanalDestino
            End Get
            Set(value As String)
                _IdCanalDestino = value
            End Set
        End Property

        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        Public Property CodigoSubCliente() As String
            Get
                Return _CodigoSubCliente
            End Get
            Set(value As String)
                _CodigoSubCliente = value
            End Set
        End Property

        Public Property CamposExtras() As CamposExtras
            Get
                Return _CamposExtras
            End Get
            Set(value As CamposExtras)
                _CamposExtras = value
            End Set
        End Property

        Public Property Bultos() As Bultos
            Get
                Return _Bultos
            End Get
            Set(value As Bultos)
                _Bultos = value
            End Set
        End Property

        Public Property Detalles() As Detalles
            Get
                Return _Detalles
            End Get
            Set(value As Detalles)
                _Detalles = value
            End Set
        End Property

        Public Property Parciales() As Parciales
            Get
                Return _Parciales
            End Get
            Set(value As Parciales)
                _Parciales = value
            End Set
        End Property

#End Region

    End Class

End Namespace