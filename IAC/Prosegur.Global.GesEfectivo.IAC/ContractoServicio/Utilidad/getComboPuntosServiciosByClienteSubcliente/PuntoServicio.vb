Namespace Utilidad.getComboPuntosServiciosByClienteSubcliente

    <Serializable()> _
    Public Class PuntoServicio

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _oidPuntoServicio As String
        Private _CodigoAjenoPuntoServicio As String
        Private _DescripcionAjenoPuntoServicio As String
        Private _totalizadorSaldo As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property OidPuntoServicio() As String
            Get
                Return _oidPuntoServicio
            End Get
            Set(value As String)
                _oidPuntoServicio = value
            End Set
        End Property

        Public Property TotalizadorSaldo() As Boolean
            Get
                Return _totalizadorSaldo
            End Get
            Set(value As Boolean)
                _totalizadorSaldo = value
            End Set
        End Property

        Public Property CodigoAjenoPuntoServicio() As String
            Get
                Return _CodigoAjenoPuntoServicio
            End Get
            Set(value As String)
                _CodigoAjenoPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionAjenoPuntoServicio() As String
            Get
                Return _DescripcionAjenoPuntoServicio
            End Get
            Set(value As String)
                _DescripcionAjenoPuntoServicio = value
            End Set
        End Property

#End Region

    End Class
End Namespace