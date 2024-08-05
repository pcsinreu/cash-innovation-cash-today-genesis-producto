Namespace RecuperarDatosDocumento

    Public Class Documento

#Region "[VARIÁVEIS]"

        Private _Id As Integer
        Private _IdGrupo As Integer
        Private _IdOrigen As Integer
        Private _IdSustituto As Integer
        Private _Descripcion As String
        Private _NumeroComprobante As String
        Private _FechaGestion As DateTime
        Private _FechaUltimaActualizacion As DateTime
        Private _FechaResolucion As DateTime
        Private _FechaDispone As DateTime
        Private _EsActaProceso As Boolean
        Private _EsDisponible As Boolean
        Private _EsReenviado As Boolean
        Private _EsAgrupado As Boolean
        Private _EsGrupo As Boolean
        Private _EsImportado As Boolean
        Private _EsExportado As Boolean
        Private _EsSustituto As Boolean
        Private _EsSustituido As Boolean
        Private _Documentos As Documentos
        Private _EstadoComprobante As EstadoComprobante
        Private _Campos As Campos
        Private _CamposExtras As CamposExtras
        Private _Bultos As Bultos
        Private _Detalles As Detalles
        Private _Sobres As Sobres

#End Region

#Region "[PROPRIEDADES]"

        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Set(value As Integer)
                _Id = value
            End Set
        End Property

        Public Property IdGrupo() As Integer
            Get
                Return _IdGrupo
            End Get
            Set(value As Integer)
                _IdGrupo = value
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

        Public Property IdSustituto() As Integer
            Get
                Return _IdSustituto
            End Get
            Set(value As Integer)
                _IdSustituto = value
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

        Public Property NumeroComprobante() As String
            Get
                Return _NumeroComprobante
            End Get
            Set(value As String)
                _NumeroComprobante = value
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

        Public Property FechaUltimaActualizacion() As DateTime
            Get
                Return _FechaUltimaActualizacion
            End Get
            Set(value As DateTime)
                _FechaUltimaActualizacion = value
            End Set
        End Property

        Public Property FechaResolucion() As DateTime
            Get
                Return _FechaResolucion
            End Get
            Set(value As DateTime)
                _FechaResolucion = value
            End Set
        End Property

        Public Property FechaDispone() As DateTime
            Get
                Return _FechaDispone
            End Get
            Set(value As DateTime)
                _FechaDispone = value
            End Set
        End Property

        Public Property EsActaProceso() As Boolean
            Get
                Return _EsActaProceso
            End Get
            Set(value As Boolean)
                _EsActaProceso = value
            End Set
        End Property

        Public Property EsDisponible() As Boolean
            Get
                Return _EsDisponible
            End Get
            Set(value As Boolean)
                _EsDisponible = value
            End Set
        End Property

        Public Property EsReenviado() As Boolean
            Get
                Return _EsReenviado
            End Get
            Set(value As Boolean)
                _EsReenviado = value
            End Set
        End Property

        Public Property EsAgrupado() As Boolean
            Get
                Return _EsAgrupado
            End Get
            Set(value As Boolean)
                _EsAgrupado = value
            End Set
        End Property

        Public Property EsGrupo() As Boolean
            Get
                Return _EsGrupo
            End Get
            Set(value As Boolean)
                _EsGrupo = value
            End Set
        End Property

        Public Property EsImportado() As Boolean
            Get
                Return _EsImportado
            End Get
            Set(value As Boolean)
                _EsImportado = value
            End Set
        End Property

        Public Property EsExportado() As Boolean
            Get
                Return _EsExportado
            End Get
            Set(value As Boolean)
                _EsExportado = value
            End Set
        End Property

        Public Property EsSustituto() As Boolean
            Get
                Return _EsSustituto
            End Get
            Set(value As Boolean)
                _EsSustituto = value
            End Set
        End Property

        Public Property EsSustituido() As Boolean
            Get
                Return _EsSustituido
            End Get
            Set(value As Boolean)
                _EsSustituido = value
            End Set
        End Property

        Public Property Documentos() As Documentos
            Get
                Return _Documentos
            End Get
            Set(value As Documentos)
                _Documentos = value
            End Set
        End Property

        Public Property EstadoComprobante() As EstadoComprobante
            Get
                Return _EstadoComprobante
            End Get
            Set(value As EstadoComprobante)
                _EstadoComprobante = value
            End Set
        End Property

        Public Property Campos() As Campos
            Get
                Return _Campos
            End Get
            Set(value As Campos)
                _Campos = Campos
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

        Public Property Sobres()
            Get
                Return _Sobres
            End Get
            Set(value)
                _Sobres = value
            End Set
        End Property

#End Region

    End Class

End Namespace