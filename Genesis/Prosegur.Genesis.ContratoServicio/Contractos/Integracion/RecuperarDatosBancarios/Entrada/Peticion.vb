Namespace Contractos.Integracion.RecuperarDatosBancarios
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

        Private _codigosEstados As List(Of String)
        Public Property CodigosEstados() As List(Of String)
            Get
                Return _codigosEstados
            End Get
            Set(ByVal value As List(Of String))
                _codigosEstados = value
            End Set
        End Property
        Private _codigosCampos As List(Of String)
        Public Property CodigosCampos() As List(Of String)
            Get
                Return _codigosCampos
            End Get
            Set(ByVal value As List(Of String))
                _codigosCampos = value
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

        Private _usuariosAprobacion As List(Of String)
        Public Property OidUsuariosAprobacion() As List(Of String)
            Get
                Return _usuariosAprobacion
            End Get
            Set(ByVal value As List(Of String))
                _usuariosAprobacion = value
            End Set
        End Property

        Private _usuariosModificacion As List(Of String)
        Public Property OidUsuariosModificacion() As List(Of String)
            Get
                Return _usuariosModificacion
            End Get
            Set(ByVal value As List(Of String))
                _usuariosModificacion = value
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

        Private _codigoTipoFecha As String
        Public Property CodigoTipoFecha() As String
            Get
                Return _codigoTipoFecha
            End Get
            Set(ByVal value As String)
                _codigoTipoFecha = value
            End Set
        End Property

        Private _fechaDesde As DateTime?
        Public Property FechaDesde() As DateTime?
            Get
                Return _fechaDesde
            End Get
            Set(ByVal value As DateTime?)
                _fechaDesde = value
            End Set
        End Property

        Private _fechaHasta As DateTime?
        Public Property FechaHasta() As DateTime?
            Get
                Return _fechaHasta
            End Get
            Set(ByVal value As DateTime?)
                _fechaHasta = value
            End Set
        End Property

        Public Sub New()
            Me._codigoUsuario = String.Empty
            Me._cultura = String.Empty
            Me._codigoPais = String.Empty

            Me._clientes = New List(Of String)
            Me._subClientes = New List(Of String)
            Me._puntosServicios = New List(Of String)

            Me._usuariosAprobacion = New List(Of String)
            Me._usuariosModificacion = New List(Of String)

            Me._codigosEstados = New List(Of String)
            Me._codigosCampos = New List(Of String)
        End Sub

    End Class
End Namespace