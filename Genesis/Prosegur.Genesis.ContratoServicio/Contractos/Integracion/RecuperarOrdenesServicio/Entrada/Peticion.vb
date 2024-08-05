Namespace Contractos.Integracion.RecuperarOrdenesServicio
    Public Class Peticion
        Private _codigoUsuario As String
        Public Property CodigoUsuario() As String
            Get
                Return _codigoUsuario
            End Get
            Set(ByVal value As String)
                _codigoUsuario = value
            End Set
        End Property
        Private _codigoPais As String
        Public Property CodigoPais() As String
            Get
                Return _codigoPais
            End Get
            Set(ByVal value As String)
                _codigoPais = value
            End Set
        End Property

        Private _codigosProductos As List(Of String)
        Public Property CodigosProductos() As List(Of String)
            Get
                Return _codigosProductos
            End Get
            Set(ByVal value As List(Of String))
                _codigosProductos = value
            End Set
        End Property
        Private _clientes As List(Of String)
        Public Property OidClientes() As List(Of String)
            Get
                Return _clientes
            End Get
            Set(ByVal value As List(Of String))
                _clientes = value
            End Set
        End Property

        Private _subClientes As List(Of String)
        Public Property OidSubClientes() As List(Of String)
            Get
                Return _subClientes
            End Get
            Set(ByVal value As List(Of String))
                _subClientes = value
            End Set
        End Property

        Private _puntosServicios As List(Of String)
        Public Property OidPuntosServicios() As List(Of String)
            Get
                Return _puntosServicios
            End Get
            Set(ByVal value As List(Of String))
                _puntosServicios = value
            End Set
        End Property

        Private _cultura As String
        Public Property Cultura() As String
            Get
                Return _cultura
            End Get
            Set(ByVal value As String)
                _cultura = value
            End Set
        End Property

        Private _contrato As String
        Public Property Contrato() As String
            Get
                Return _contrato
            End Get
            Set(ByVal value As String)
                _contrato = value
            End Set
        End Property

        Private _ordenServicio As String
        Public Property OrdenServicio() As String
            Get
                Return _ordenServicio
            End Get
            Set(ByVal value As String)
                _ordenServicio = value
            End Set
        End Property

        Private _fechaInicio As DateTime?
        Public Property FechaInicio() As DateTime?
            Get
                Return _fechaInicio
            End Get
            Set(ByVal value As DateTime?)
                _fechaInicio = value
            End Set
        End Property

        Private _fechaFin As DateTime?
        Public Property FechaFin() As DateTime?
            Get
                Return _fechaFin
            End Get
            Set(ByVal value As DateTime?)
                _fechaFin = value
            End Set
        End Property

        Private _estado As Boolean
        Public Property Estado() As Boolean
            Get
                Return _estado
            End Get
            Set(ByVal value As Boolean)
                _estado = value
            End Set
        End Property

        Public Sub New()
            Me._codigoUsuario = String.Empty
            Me._cultura = String.Empty
            Me._codigoPais = String.Empty

            Me._clientes = New List(Of String)
            Me._subClientes = New List(Of String)
            Me._puntosServicios = New List(Of String)

            Me._contrato = String.Empty
            Me._ordenServicio = String.Empty

            Me._codigosProductos = New List(Of String)
            Me._estado = vbNull
        End Sub

    End Class
End Namespace
