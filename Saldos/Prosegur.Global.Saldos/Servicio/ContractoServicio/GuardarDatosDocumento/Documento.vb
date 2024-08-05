Namespace GuardarDatosDocumento

    Public Class Documento

        Private _IdDocumento As Integer
        Private _IdCaracteristica As Integer
        Private _IdOrigen As Integer
        Private _IdGrupo As String
        Private _IdMovimentacionFondo As String
        Private _FechaGestion As DateTime
        Private _Campos As New Campos
        Private _CamposExtras As New CamposExtras
        Private _Bultos As New Bultos
        Private _Detalles As New Detalles
        Private _Sobres As New Sobres
        Private _DocumentosAgrupados As New DocumentosAgrupados
        Private _Legado As Boolean

        Public Property IdMovimentacionFondo() As String
            Get
                Return _IdMovimentacionFondo
            End Get
            Set(value As String)
                _IdMovimentacionFondo = value
            End Set
        End Property

        Public Property IdDocumento() As Integer
            Get
                Return _IdDocumento
            End Get
            Set(value As Integer)
                _IdDocumento = value
            End Set
        End Property

        Public Property IdCaracteristica() As Integer
            Get
                Return _IdCaracteristica
            End Get
            Set(value As Integer)
                _IdCaracteristica = value
            End Set
        End Property

        Public Property IdOrigen() As Integer
            Get
                Return _IdOrigen
            End Get
            Set(value As Integer)
                _IdOrigen = value
            End Set
        End Property

        Public Property IdGrupo() As String
            Get
                Return _IdGrupo
            End Get
            Set(value As String)
                _IdGrupo = value
            End Set
        End Property

        Public Property FechaGestion() As DateTime
            Get
                Return _FechaGestion
            End Get
            Set(value As DateTime)
                _FechaGestion = value
            End Set
        End Property

        Public Property Campos() As Campos
            Get
                Return _Campos
            End Get
            Set(value As Campos)
                _Campos = value
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

        Public Property Sobres() As Sobres
            Get
                Return _Sobres
            End Get
            Set(value As Sobres)
                _Sobres = value
            End Set
        End Property

        Public Property DocumentosAgrupados() As DocumentosAgrupados
            Get
                Return _DocumentosAgrupados
            End Get
            Set(value As DocumentosAgrupados)
                _DocumentosAgrupados = value
            End Set
        End Property

        Public Property Legado() As Boolean
            Get
                Return _Legado
            End Get
            Set(value As Boolean)
                _Legado = value
            End Set
        End Property

    End Class

End Namespace