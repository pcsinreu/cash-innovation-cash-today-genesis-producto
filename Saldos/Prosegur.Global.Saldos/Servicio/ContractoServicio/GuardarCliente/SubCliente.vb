Namespace GuardarCliente

    ''' <summary>
    ''' Classe SubCliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' [vinicius.gama] 22/10/2009 Alterado - Adicionado propriedade Oid, identificador unico
    ''' </history>
    <Serializable()> _
    Public Class SubCliente

#Region "[VARIAVEIS]"

        Private _OidSubCliente As String
        Private _CodSubCliente As String
        Private _DescripcionSubCliente As String
        Private _PuntoServicios As PuntoServicioColeccion
        Private _Enviado As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidSubCliente() As String
            Get
                Return _OidSubCliente
            End Get
            Set(value As String)
                _OidSubCliente = value
            End Set
        End Property

        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

        Public Property DescripcionSubCliente() As String
            Get
                Return _DescripcionSubCliente
            End Get
            Set(value As String)
                _DescripcionSubCliente = value
            End Set
        End Property

        Public Property PuntoServicios() As PuntoServicioColeccion
            Get
                Return _PuntoServicios
            End Get
            Set(value As PuntoServicioColeccion)
                _PuntoServicios = value
            End Set
        End Property

        Public Property Enviado() As Boolean
            Get
                Return _Enviado
            End Get
            Set(value As Boolean)
                _Enviado = value
            End Set
        End Property

#End Region

    End Class

End Namespace
