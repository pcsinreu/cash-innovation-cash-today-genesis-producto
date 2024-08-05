Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager

<Serializable()> _
Public Class Documento

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _IdDocumentoLegado As Integer
    Private _Formulario As Formulario
    Private _Detalles As Detalles
    Private _Usuario As Usuario
    Private _Fecha As Date
    Private _NumComprobante As String
    Private _EstadoComprobante As EstadoComprobante
    Private _UsuarioResolutor As Usuario
    Private _FechaResolucion As Date
    Private _FechaGestion As Date
    Private _Grupo As Documento
    Private _Agrupado As Boolean
    Private _EsGrupo As Boolean
    Private _Bultos As Bultos
    Private _Documentos As Documentos
    Private _DocumentosDelReporte As Documentos
    Private _Origen As Documento
    Private _Reenviado As Boolean
    Private _ClaveMBC As String
    Private _UsuarioDispone As Usuario
    Private _FechaDispone As Date
    Private _Disponible As Boolean
    Private _Sobres As Sobres
    Private _Sustituido As Boolean
    Private _EsSustituto As Boolean
    Private _Sustituto As Documento
    Private _Importado As Boolean
    Private _Exportado As Boolean
    Private _Primordial As Documento
    Private _Extracto As Extracto
    Private _SaldoDisponible As Nullable(Of Boolean)
    Private _ArchivoRemesaLegado As Byte()
    Private _ReintentosConteo As Integer
    Private _Campos As Hashtable
    Private _Legado As Boolean = False
    Private _ExportadoConteo As Boolean
    Private _IdMovimentacionFondo As String
    Private _ExportadoSalidas As Boolean
    Private _GMTVeranoAjuste As Short? = Nothing
    Private Shared _Versao As String

    ' define se deve ou não gerar as mensagens de log em disco
    Private LogEnDisco As Boolean = IIf(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("GrabarLogMensajesEnDiscoDuro") IsNot Nothing AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("GrabarLogMensajesEnDiscoDuro").ToString().ToLower() = "true", True, False)

#End Region

#Region "[PROPRIEDADES]"

    Public Property ExportadoSalidas() As Boolean
        Get
            Return _ExportadoSalidas
        End Get
        Set(value As Boolean)
            _ExportadoSalidas = value
        End Set
    End Property

    Public Property IdMovimentacionFondo() As String
        Get
            Return _IdMovimentacionFondo
        End Get
        Set(value As String)
            _IdMovimentacionFondo = value
        End Set
    End Property

    Public Property IdDocumentoLegado() As Integer
        Get
            Return _IdDocumentoLegado
        End Get
        Set(value As Integer)
            _IdDocumentoLegado = value
        End Set
    End Property

    Public Property Extracto() As Extracto
        Get
            If _Extracto Is Nothing Then
                _Extracto = New Extracto()
            End If
            Return _Extracto
        End Get
        Set(Value As Extracto)
            _Extracto = Value
        End Set
    End Property


    Public Property SaldoDisponible() As Nullable(Of Boolean)
        Get
            Return _SaldoDisponible
        End Get
        Set(Value As Nullable(Of Boolean))
            _SaldoDisponible = Value
        End Set
    End Property

    Public Property Exportado() As Boolean
        Get
            Return _Exportado
        End Get
        Set(Value As Boolean)
            _Exportado = Value
        End Set
    End Property

    Public Property Importado() As Boolean
        Get
            Return _Importado
        End Get
        Set(Value As Boolean)
            _Importado = Value
        End Set
    End Property

    Public Property Sustituto() As Documento
        Get
            If _Sustituto Is Nothing Then
                _Sustituto = New Documento()
            End If
            Return _Sustituto
        End Get
        Set(Value As Documento)
            _Sustituto = Value
        End Set
    End Property

    Public Property EsSustituto() As Boolean
        Get
            Return _EsSustituto
        End Get
        Set(Value As Boolean)
            _EsSustituto = Value
        End Set
    End Property

    Public Property Sustituido() As Boolean
        Get
            Return _Sustituido
        End Get
        Set(Value As Boolean)
            _Sustituido = Value
        End Set
    End Property

    Public Property Sobres() As Sobres
        Get
            If _Sobres Is Nothing Then
                _Sobres = New Sobres()
            End If
            Return _Sobres
        End Get
        Set(Value As Sobres)
            _Sobres = Value
        End Set
    End Property

    Public Property Disponible() As Boolean
        Get
            Return _Disponible
        End Get
        Set(Value As Boolean)
            _Disponible = Value
        End Set
    End Property

    Public Property FechaDispone() As Date
        Get
            Return _FechaDispone
        End Get
        Set(Value As Date)
            _FechaDispone = Value
        End Set
    End Property

    Public Property UsuarioDispone() As Usuario
        Get
            If _UsuarioDispone Is Nothing Then
                _UsuarioDispone = New Usuario()
            End If
            Return _UsuarioDispone
        End Get
        Set(Value As Usuario)
            _UsuarioDispone = Value
        End Set
    End Property

    Public Property claveMBC() As String
        Get
            If _ClaveMBC = "" Then ClaveMBCGenerar()
            Return _ClaveMBC
        End Get
        Set(Value As String)
            _ClaveMBC = Value
        End Set
    End Property

    Public Property DocumentosDelReporte() As Documentos
        Get
            If _DocumentosDelReporte Is Nothing Then
                _DocumentosDelReporte = New Documentos()
            End If
            Return _DocumentosDelReporte
        End Get
        Set(Value As Documentos)
            _DocumentosDelReporte = Value
        End Set
    End Property

    Public Property Documentos() As Documentos
        Get
            If _Documentos Is Nothing Then
                _Documentos = New Documentos()
            End If
            Return _Documentos
        End Get
        Set(Value As Documentos)
            _Documentos = Value
        End Set
    End Property

    Public Property Bultos() As Bultos
        Get
            If _Bultos Is Nothing Then
                _Bultos = New Bultos()
            End If
            Return _Bultos
        End Get
        Set(Value As Bultos)
            _Bultos = Value
        End Set
    End Property

    Public Property EsGrupo() As Boolean
        Get
            Return _EsGrupo
        End Get
        Set(Value As Boolean)
            _EsGrupo = Value
        End Set
    End Property

    Public Property Agrupado() As Boolean
        Get
            Return _Agrupado
        End Get
        Set(Value As Boolean)
            _Agrupado = Value
        End Set
    End Property

    Public Property EstadoComprobante() As EstadoComprobante
        Get
            If _EstadoComprobante Is Nothing Then
                _EstadoComprobante = New EstadoComprobante()
            End If
            Return _EstadoComprobante
        End Get
        Set(Value As EstadoComprobante)
            _EstadoComprobante = Value
        End Set
    End Property

    Public Property Usuario() As Usuario
        Get
            If _Usuario Is Nothing Then
                _Usuario = New Usuario()
            End If
            Return _Usuario
        End Get
        Set(Value As Usuario)
            _Usuario = Value
        End Set
    End Property

    Public Property UsuarioResolutor() As Usuario
        Get
            If _UsuarioResolutor Is Nothing Then
                _UsuarioResolutor = New Usuario()
            End If
            Return _UsuarioResolutor
        End Get
        Set(Value As Usuario)
            _UsuarioResolutor = Value
        End Set
    End Property

    Public Property Detalles() As Detalles
        Get
            If _Detalles Is Nothing Then
                _Detalles = New Detalles()
            End If
            Return _Detalles
        End Get
        Set(Value As Detalles)
            _Detalles = Value
        End Set
    End Property

    Public Property Formulario() As Formulario
        Get
            If _Formulario Is Nothing Then
                _Formulario = New Formulario()
            End If
            Return _Formulario
        End Get
        Set(Value As Formulario)
            _Formulario = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

    Public Property FechaResolucion() As Date
        Get
            Return _FechaResolucion
        End Get
        Set(Value As Date)
            _FechaResolucion = Value
        End Set
    End Property

    Public Property FechaGestion() As Date
        Get
            Return _FechaGestion
        End Get
        Set(Value As Date)
            _FechaGestion = Value
        End Set
    End Property

    Public Property Fecha() As Date
        Get
            Return _Fecha
        End Get
        Set(Value As Date)
            _Fecha = Value
        End Set
    End Property

    Public Property NumComprobante() As String
        Get
            Return _NumComprobante
        End Get
        Set(Value As String)
            _NumComprobante = Value
        End Set
    End Property

    Public Property Grupo() As Documento
        Get
            If _Grupo Is Nothing Then
                _Grupo = New Documento()
            End If
            Return _Grupo
        End Get
        Set(Value As Documento)
            _Grupo = Value
        End Set
    End Property

    Public Property Primordial() As Documento
        Get
            If _Primordial Is Nothing Then
                _Primordial = New Documento()
            End If
            Return _Primordial
        End Get
        Set(Value As Documento)
            _Primordial = Value
        End Set
    End Property

    Public Property Origen() As Documento
        Get
            If _Origen Is Nothing Then
                _Origen = New Documento()
            End If
            Return _Origen
        End Get
        Set(Value As Documento)
            _Origen = Value
        End Set
    End Property

    Public Property Reenviado() As Boolean
        Get
            Return _Reenviado
        End Get
        Set(Value As Boolean)
            _Reenviado = Value
        End Set
    End Property

    Public Property ArchivoRemesaLegado() As Byte()
        Get
            Return _ArchivoRemesaLegado
        End Get
        Set(value As Byte())
            _ArchivoRemesaLegado = value
        End Set
    End Property

    Public Property ReintentosConteo() As Integer
        Get
            Return _ReintentosConteo
        End Get
        Set(value As Integer)
            _ReintentosConteo = value
        End Set
    End Property

    Public Property Campos() As Hashtable
        Get
            Return _Campos
        End Get
        Set(value As Hashtable)
            _Campos = value
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

    Public Property ExportadoConteo() As Boolean
        Get
            Return _ExportadoConteo
        End Get
        Set(value As Boolean)
            _ExportadoConteo = value
        End Set
    End Property

    ''' <summary>
    ''' quantidade de minutos somados (gmt + VeranoAjuste) a serem ajustados na hora utc
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GMTVeranoAjuste() As Short?
        Get
            Return _GMTVeranoAjuste
        End Get
        Set(value As Short?)
            _GMTVeranoAjuste = value
        End Set
    End Property

    Public Function GetDateTime() As DateTime
        'If (_GMTVeranoAjuste Is Nothing) Then
        '    Throw New Exception("erro no getDateTime.")
        'End If
        Dim dt As DateTime
        dt = If(_GMTVeranoAjuste IsNot Nothing, DateTime.UtcNow.AddMinutes(_GMTVeranoAjuste), DateTime.Now)
        Return dt

    End Function

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar(Optional ByRef UsuarioCodigo As Integer = 0)

        Dim campo As Campo = Nothing
        Dim Campos As Campos = Nothing
        Dim IdDocDetalles As Long = Nothing
        Dim IdDocBultos As Long = Nothing
        Dim IdDocCamposExtra As Long = Nothing

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoCabeceraRealizar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Usuario.Id = dt.Rows(0)("IdUsuario")
            Me.Fecha = dt.Rows(0)("Fecha")

            If dt.Rows(0)("NumComprobante") Is DBNull.Value Then
                Me.NumComprobante = ""
            Else
                Me.NumComprobante = dt.Rows(0)("NumComprobante")
            End If

            If dt.Rows(0)("IdMovimentacionFondo") Is DBNull.Value Then
                Me.IdMovimentacionFondo = 0
            Else
                Me.IdMovimentacionFondo = dt.Rows(0)("IdMovimentacionFondo")
            End If

            If dt.Rows(0)("IdUsuarioResolutor") Is DBNull.Value Then
                Me.UsuarioResolutor.Id = 0
            Else
                Me.UsuarioResolutor.Id = dt.Rows(0)("IdUsuarioResolutor")
            End If

            If dt.Rows(0)("FechaResolucion") Is DBNull.Value Then
                Me.FechaResolucion = Date.MinValue
            Else
                Me.FechaResolucion = dt.Rows(0)("FechaResolucion")
            End If

            If dt.Rows(0)("IdUsuarioDispone") Is DBNull.Value Then
                Me.UsuarioDispone.Id = 0
            Else
                Me.UsuarioDispone.Id = dt.Rows(0)("IdUsuarioDispone")
            End If

            If dt.Rows(0)("FechaDispone") Is DBNull.Value Then
                Me.FechaDispone = Date.MinValue
            Else
                Me.FechaDispone = dt.Rows(0)("FechaDispone")
            End If

            'realiza formulario
            Me.Formulario.Id = dt.Rows(0)("IdFormulario")
            Me.Formulario.Realizar()

            'realiza estadoComprobante
            Me.EstadoComprobante.Id = dt.Rows(0)("IdEstadoComprobante")
            Me.EstadoComprobante.Realizar()

            If dt.Rows(0)("FechaGestion") Is DBNull.Value Then
                Me.FechaGestion = Date.MinValue
            Else
                Me.FechaGestion = dt.Rows(0)("FechaGestion")
            End If

            If dt.Rows(0)("Agrupado") Is DBNull.Value Then
                Me.Agrupado = False
            Else
                Me.Agrupado = CType(dt.Rows(0)("Agrupado"), Boolean)
            End If

            If dt.Rows(0)("EsGrupo") Is DBNull.Value Then
                Me.EsGrupo = False
            Else
                Me.EsGrupo = CType(dt.Rows(0)("EsGrupo"), Boolean)
            End If

            If dt.Rows(0)("IdGrupo") Is DBNull.Value Then
                Me.Grupo.Id = 0
            Else
                Me.Grupo.Id = dt.Rows(0)("IdGrupo")
            End If

            If dt.Rows(0)("IdOrigen") Is DBNull.Value Then
                Me.Origen.Id = 0
            Else
                Me.Origen.Id = dt.Rows(0)("IdOrigen")
            End If

            If dt.Rows(0)("Reenviado") Is DBNull.Value Then
                Me.Reenviado = False
            Else
                Me.Reenviado = CType(dt.Rows(0)("Reenviado"), Boolean)
            End If

            If dt.Rows(0)("Disponible") Is DBNull.Value Then
                Me.Disponible = False
            Else
                Me.Disponible = CType(dt.Rows(0)("Disponible"), Boolean)
            End If

            If dt.Rows(0)("Sustituido") Is DBNull.Value Then
                Me.Sustituido = False
            Else
                Me.Sustituido = CType(dt.Rows(0)("Sustituido"), Boolean)
            End If

            If dt.Rows(0)("EsSustituto") Is DBNull.Value Then
                Me.EsSustituto = False
            Else
                Me.EsSustituto = CType(dt.Rows(0)("EsSustituto"), Boolean)
            End If

            If dt.Rows(0)("IdSustituto") Is DBNull.Value Then
                Me.Sustituto.Id = 0
            Else
                Me.Sustituto.Id = dt.Rows(0)("IdSustituto")
            End If

            If dt.Rows(0)("IdPrimordial") Is DBNull.Value Then
                Me.Primordial.Id = 0
            Else
                Me.Primordial.Id = dt.Rows(0)("IdPrimordial")
            End If

            If dt.Rows(0)("Importado") Is DBNull.Value Then
                Me.Importado = False
            Else
                Me.Importado = CType(dt.Rows(0)("Importado"), Boolean)
            End If

            If dt.Rows(0)("Exportado") Is DBNull.Value Then
                Me.Exportado = False
            Else
                Me.Exportado = CType(dt.Rows(0)("Exportado"), Boolean)
            End If

            Campos = Me.Formulario.Campos

            If Campos IsNot Nothing Then

                For Each c As Campo In Campos

                    Select Case c.Tipo

                        Case "I"

                            If dt.Rows(0)("Id" & c.Nombre) Is DBNull.Value Then
                                c.IdValor = 0
                            Else
                                c.IdValor = Convert.ToInt32(dt.Rows(0)("Id" & c.Nombre))
                            End If

                            If dt.Rows(0)(c.Nombre) IsNot DBNull.Value Then
                                c.Valor = dt.Rows(0)(c.Nombre)
                            Else
                                c.Valor = String.Empty
                            End If

                        Case "A"

                            If dt.Rows(0)(c.Nombre) Is DBNull.Value Then
                                c.Valor = String.Empty
                            Else
                                c.Valor = dt.Rows(0)(c.Nombre)
                            End If

                        Case "F"

                            If dt.Rows(0)(c.Nombre) Is DBNull.Value Then
                                c.Valor = String.Empty
                            Else
                                c.Valor = dt.Rows(0)(c.Nombre)
                            End If

                    End Select

                Next

            End If

            If Me.Formulario.ConValores Then
                IdDocDetalles = dt.Rows(0)("IdDocDetalles")
                If IdDocDetalles > 0 Then
                    Me.Detalles.Documento.Id = IdDocDetalles
                Else
                    Me.Detalles.Documento.Id = Me.Id
                End If
            End If

            IdDocCamposExtra = dt.Rows(0)("IdDocCamposExtra")

            If IdDocCamposExtra > 0 Then
                Me.Formulario.CamposExtra.Documento.Id = IdDocCamposExtra
            Else
                Me.Formulario.CamposExtra.Documento.Id = Me.Id
                Me.Formulario.CamposExtra.Realizar()
            End If

            IdDocBultos = dt.Rows(0)("IdDocBultos")
            If Me.Formulario.ConBultos Then
                If IdDocBultos > 0 Then
                    Me.Bultos.Documento.Id = IdDocBultos
                Else
                    Me.Bultos.Documento.Id = Me.Id
                End If
            End If

            If Me.Formulario.EsActaProceso Then
                If Me.Origen.Id > 0 Then Me.Origen.Realizar()
                Me.Sobres.Documento.Id = Me.Id
                Me.Sobres.Realizar()
            End If

            ReintentosConteo = dt.Rows(0)("reintentos_conteo")

            Me.Legado = dt.Rows(0)("Legado")

        End If

    End Sub

    ''' <summary>
    ''' Verifica si ya existe un documento con el identificador de la transacción
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RealizarIdMovimentacionFondo()

        Dim campo As Campo = Nothing
        Dim Campos As Campos = Nothing
        Dim IdDocDetalles As Long = Nothing
        Dim IdDocBultos As Long = Nothing
        Dim IdDocCamposExtra As Long = Nothing

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoCabeceraRealizarCrearMif.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMovimentacionFondo", ProsegurDbType.Identificador_Alfanumerico, Me.IdMovimentacionFondo))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Usuario.Id = dt.Rows(0)("IdUsuario")
            Me.Fecha = dt.Rows(0)("Fecha")

            If dt.Rows(0)("IdDocumento") Is DBNull.Value Then
                Me.Id = 0
            Else
                Me.Id = dt.Rows(0)("IdDocumento")
            End If

            If dt.Rows(0)("NumComprobante") Is DBNull.Value Then
                Me.NumComprobante = ""
            Else
                Me.NumComprobante = dt.Rows(0)("NumComprobante")
            End If

            If dt.Rows(0)("IdUsuarioResolutor") Is DBNull.Value Then
                Me.UsuarioResolutor.Id = 0
            Else
                Me.UsuarioResolutor.Id = dt.Rows(0)("IdUsuarioResolutor")
            End If

            If dt.Rows(0)("FechaResolucion") Is DBNull.Value Then
                Me.FechaResolucion = Date.MinValue
            Else
                Me.FechaResolucion = dt.Rows(0)("FechaResolucion")
            End If

            If dt.Rows(0)("IdUsuarioDispone") Is DBNull.Value Then
                Me.UsuarioDispone.Id = 0
            Else
                Me.UsuarioDispone.Id = dt.Rows(0)("IdUsuarioDispone")
            End If

            If dt.Rows(0)("FechaDispone") Is DBNull.Value Then
                Me.FechaDispone = Date.MinValue
            Else
                Me.FechaDispone = dt.Rows(0)("FechaDispone")
            End If

            'realiza formulario
            Me.Formulario.Id = dt.Rows(0)("IdFormulario")
            Me.Formulario.Realizar()

            'realiza estadoComprobante
            Me.EstadoComprobante.Id = dt.Rows(0)("IdEstadoComprobante")
            Me.EstadoComprobante.Realizar()

            If dt.Rows(0)("FechaGestion") Is DBNull.Value Then
                Me.FechaGestion = Date.MinValue
            Else
                Me.FechaGestion = dt.Rows(0)("FechaGestion")
            End If

            If dt.Rows(0)("Agrupado") Is DBNull.Value Then
                Me.Agrupado = False
            Else
                Me.Agrupado = CType(dt.Rows(0)("Agrupado"), Boolean)
            End If

            If dt.Rows(0)("EsGrupo") Is DBNull.Value Then
                Me.EsGrupo = False
            Else
                Me.EsGrupo = CType(dt.Rows(0)("EsGrupo"), Boolean)
            End If

            If dt.Rows(0)("IdGrupo") Is DBNull.Value Then
                Me.Grupo.Id = 0
            Else
                Me.Grupo.Id = dt.Rows(0)("IdGrupo")
            End If

            If dt.Rows(0)("IdOrigen") Is DBNull.Value Then
                Me.Origen.Id = 0
            Else
                Me.Origen.Id = dt.Rows(0)("IdOrigen")
            End If

            If dt.Rows(0)("Reenviado") Is DBNull.Value Then
                Me.Reenviado = False
            Else
                Me.Reenviado = CType(dt.Rows(0)("Reenviado"), Boolean)
            End If

            If dt.Rows(0)("Disponible") Is DBNull.Value Then
                Me.Disponible = False
            Else
                Me.Disponible = CType(dt.Rows(0)("Disponible"), Boolean)
            End If

            If dt.Rows(0)("Sustituido") Is DBNull.Value Then
                Me.Sustituido = False
            Else
                Me.Sustituido = CType(dt.Rows(0)("Sustituido"), Boolean)
            End If

            If dt.Rows(0)("EsSustituto") Is DBNull.Value Then
                Me.EsSustituto = False
            Else
                Me.EsSustituto = CType(dt.Rows(0)("EsSustituto"), Boolean)
            End If

            If dt.Rows(0)("IdSustituto") Is DBNull.Value Then
                Me.Sustituto.Id = 0
            Else
                Me.Sustituto.Id = dt.Rows(0)("IdSustituto")
            End If

            If dt.Rows(0)("IdPrimordial") Is DBNull.Value Then
                Me.Primordial.Id = 0
            Else
                Me.Primordial.Id = dt.Rows(0)("IdPrimordial")
            End If

            If dt.Rows(0)("Importado") Is DBNull.Value Then
                Me.Importado = False
            Else
                Me.Importado = CType(dt.Rows(0)("Importado"), Boolean)
            End If

            If dt.Rows(0)("Exportado") Is DBNull.Value Then
                Me.Exportado = False
            Else
                Me.Exportado = CType(dt.Rows(0)("Exportado"), Boolean)
            End If

            Campos = Me.Formulario.Campos

            If Campos IsNot Nothing Then

                For Each c As Campo In Campos

                    Select Case c.Tipo

                        Case "I"

                            If dt.Rows(0)("Id" & c.Nombre) Is DBNull.Value Then
                                c.IdValor = 0
                            Else
                                c.IdValor = Convert.ToInt32(dt.Rows(0)("Id" & c.Nombre))
                            End If

                            If dt.Rows(0)(c.Nombre) IsNot DBNull.Value Then
                                c.Valor = dt.Rows(0)(c.Nombre)
                            Else
                                c.Valor = String.Empty
                            End If

                        Case "A"

                            If dt.Rows(0)(c.Nombre) Is DBNull.Value Then
                                c.Valor = String.Empty
                            Else
                                c.Valor = dt.Rows(0)(c.Nombre)
                            End If

                        Case "F"

                            If dt.Rows(0)(c.Nombre) Is DBNull.Value Then
                                c.Valor = String.Empty
                            Else
                                c.Valor = dt.Rows(0)(c.Nombre)
                            End If

                    End Select

                Next

            End If

            If Me.Formulario.ConValores Then
                IdDocDetalles = dt.Rows(0)("IdDocDetalles")
                If IdDocDetalles > 0 Then
                    Me.Detalles.Documento.Id = IdDocDetalles
                Else
                    Me.Detalles.Documento.Id = Me.Id
                End If

                Me.Detalles.Realizar()

            End If

            IdDocCamposExtra = dt.Rows(0)("IdDocCamposExtra")

            If IdDocCamposExtra > 0 Then
                Me.Formulario.CamposExtra.Documento.Id = IdDocCamposExtra
            Else
                Me.Formulario.CamposExtra.Documento.Id = Me.Id
                Me.Formulario.CamposExtra.Realizar()
            End If

            IdDocBultos = dt.Rows(0)("IdDocBultos")
            If Me.Formulario.ConBultos Then
                If IdDocBultos > 0 Then
                    Me.Bultos.Documento.Id = IdDocBultos
                Else
                    Me.Bultos.Documento.Id = Me.Id
                End If
            End If

            If Me.Formulario.EsActaProceso Then
                If Me.Origen.Id > 0 Then Me.Origen.Realizar()
                Me.Sobres.Documento.Id = Me.Id
                Me.Sobres.Realizar()
            End If

            ReintentosConteo = dt.Rows(0)("reintentos_conteo")

            Me.Legado = dt.Rows(0)("Legado")

        End If

    End Sub

    Public Function Registrar(substituir As Boolean) As List(Of String)

        Dim IdDocDetalles As Long = 0
        Dim IdDocBultos As Long = 0
        Dim IdDocCamposExtra As Long = 0
        Dim RegistrarCamposExtra As Integer = 0
        Dim Validar As New List(Of String)
        Dim codFormularioIngresso As String = String.Empty

        If Not String.IsNullOrEmpty(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("codFormularioIngresso")) Then
            codFormularioIngresso = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("codFormularioIngresso").ToString()
        End If

        IdDocDetalles = 0
        If Not Me.Detalles Is Nothing Then
            If Me.Detalles.Documento.Id <> Me.Id Then
                IdDocDetalles = Me.Detalles.Documento.Id
            End If
        End If

        If Not Me.Bultos Is Nothing Then
            If Me.Bultos.Documento.Id <> Me.Id Then
                IdDocBultos = Me.Bultos.Documento.Id
            End If
        End If

        If Not Me.Formulario.CamposExtra Is Nothing Then
            If Me.Formulario.CamposExtra.Documento.Id <> Me.Id Then
                IdDocCamposExtra = Me.Formulario.CamposExtra.Documento.Id
            End If
        End If

        ' verificar se possui campo NumExterno
        Dim result = From objCampos In Me.Formulario.Campos _
                     Where objCampos.Tipo = "A" AndAlso objCampos.Nombre = "NumExterno"

        ' caso exista
        If result IsNot Nothing AndAlso result.Count > 0 AndAlso Me.Formulario.DebeValidarNumExternoExistente AndAlso Not Me.Legado Then

            If Not substituir AndAlso codFormularioIngresso.Contains(Me._Formulario.Id) Then
                ' Verificar se o ultimo documento é um documento de saída (e030 ou e120)
                If Not DocumentoVerificaNumExternoTipoFormulario(result(0).Valor, Me.Id, Me.Origen.Id) Then
                    Validar.Add(Traduzir("023_msg_valida_TipoDoc_NumExternoExistente"))
                End If
            Else
                ' verificar se já existe o numero externo
                If Me.DocumentoVerificaNumExterno(result(0).Valor, Me.Id, Me.Origen.Id) Then
                    ' verificar se o numero externo está associado a um documento de rechazo
                    If Me.DocumentoVerificarNumExternoAssociadoRechazo(result(0).Valor, Me.Id) Then
                        Validar.Add(Traduzir("023_msg_valida_numexterno"))
                    End If

                End If
            End If

        End If

        ' se não encontrou erros
        If Validar.Count = 0 Then
            If Me.Id = 0 Then
                Me.Id = Documento.ObterIdDocumento()
                RegistrarInsert(IdDocDetalles, IdDocBultos, IdDocCamposExtra, RegistrarCamposExtra)
            Else
                RegistrarUpdate(IdDocDetalles, IdDocBultos, IdDocCamposExtra, RegistrarCamposExtra)
            End If

        End If

        Return Validar

    End Function

    Protected Function RegistrarInsert(IdDocDetalles As Integer, IdDocBultos As Integer, IdDocCamposExtra As Integer, ByRef RegistrarCamposExtra As Integer) As List(Of String)

        Dim Mensagens As New List(Of String)
        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.DocumentoCabeceraRegistrarInsert.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Fecha", ProsegurDbType.Data_Hora, Me.Fecha))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CentroProcesoOrigen", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CentroProcesoDestino", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ClienteOrigen", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ClienteDestino", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Banco", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Usuario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Formulario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "BancoDeposito", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMovimentacionFondo", ProsegurDbType.Identificador_Alfanumerico, Me.IdMovimentacionFondo))

        If Me.FechaGestion = Date.MinValue Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaGestion", ProsegurDbType.Data_Hora, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaGestion", ProsegurDbType.Data_Hora, Me.FechaGestion))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumExterno", ProsegurDbType.Descricao_Longa, String.Empty))

        If Me.Grupo.Id <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGrupo", ProsegurDbType.Identificador_Numerico, Me.Grupo.Id))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGrupo", ProsegurDbType.Identificador_Numerico, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Agrupado", ProsegurDbType.Logico, Me.Agrupado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsGrupo", ProsegurDbType.Logico, Me.EsGrupo))

        If Me.Origen.Id <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdOrigen", ProsegurDbType.Inteiro_Longo, Me.Origen.Id))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdOrigen", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reenviado", ProsegurDbType.Logico, Me.Reenviado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Disponible", ProsegurDbType.Logico, Me.Disponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituido", ProsegurDbType.Logico, Me.Sustituido))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsSustituto", ProsegurDbType.Logico, Me.EsSustituto))

        If IdDocDetalles <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocDetalles", ProsegurDbType.Inteiro_Longo, IdDocDetalles))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocDetalles", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        If IdDocBultos <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocBultos", ProsegurDbType.Inteiro_Longo, IdDocBultos))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocBultos", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        If IdDocCamposExtra <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocCamposExtra", ProsegurDbType.Inteiro_Longo, IdDocCamposExtra))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocCamposExtra", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        If Me.Primordial.Id <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPrimordial", ProsegurDbType.Inteiro_Longo, Me.Primordial.Id))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPrimordial", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Legado", ProsegurDbType.Logico, Me.Legado))

        ' tratar campos do formulario
        TratarCamposFormulario(comando)

        ' adicionar comando para transação
        objTransacao.AdicionarItemTransacao(comando)

        ' registrar detalles
        RegistrarDetalles(IdDocDetalles, objTransacao)

        ' registrar campos extra
        Mensagens.AddRange(RegistrarCampoExtra(IdDocCamposExtra, objTransacao))

        ' Registro de los Bultos
        RegistrarBultos(IdDocBultos, objTransacao)

        ' Registro de los sobres
        RegistrarSobres(objTransacao)

        'si el doc es un grupo hay que setear el valor de los campos fijos igual a este para todos los que cuelgan
        If Me.EsGrupo Then
            GrupoDocumentosSeanCamposFijos(objTransacao)
        End If

        ' realizar a transação
        objTransacao.RealizarTransacao()

        'si el doc esta basado en un reporte de docs hay que registrar todos los que cuelgan
        If Me.EsGrupo AndAlso (Me.Formulario.BasadoEnReporte OrElse Me.Formulario.BasadoEnSaldos) Then
            DocumentosReporteRegistrar()
        End If

        Return Mensagens

    End Function

    Protected Function RegistrarUpdate(IdDocDetalles As Integer, IdDocBultos As Integer, IdDocCamposExtra As Integer, ByRef RegistrarCamposExtra As Integer) As List(Of String)

        Dim IdDocumentoEnviadoLegado As Integer = 0

        ' Se o documento foi criado pelo legado
        If Me.Legado Then
            IdDocumentoEnviadoLegado = RecuperarDocumentoEnviadoConteo()
        End If

        Dim Mensagens As New List(Of String)
        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.DocumentoCabeceraRegistrarUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Fecha", ProsegurDbType.Data_Hora, Me.Fecha))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CentroProcesoOrigen", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CentroProcesoDestino", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ClienteOrigen", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ClienteDestino", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Banco", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Usuario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Formulario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "BancoDeposito", ProsegurDbType.Inteiro_Longo, DBNull.Value))

        If Me.FechaGestion = Date.MinValue Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaGestion", ProsegurDbType.Data_Hora, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaGestion", ProsegurDbType.Data_Hora, Me.FechaGestion))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumExterno", ProsegurDbType.Descricao_Longa, String.Empty))

        If Me.Grupo.Id <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGrupo", ProsegurDbType.Identificador_Numerico, Me.Grupo.Id))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGrupo", ProsegurDbType.Identificador_Numerico, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Agrupado", ProsegurDbType.Logico, Me.Agrupado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsGrupo", ProsegurDbType.Logico, Me.EsGrupo))

        If Me.Origen.Id <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdOrigen", ProsegurDbType.Inteiro_Longo, Me.Origen.Id))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdOrigen", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reenviado", ProsegurDbType.Logico, Me.Reenviado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Disponible", ProsegurDbType.Logico, Me.Disponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituido", ProsegurDbType.Logico, Me.Sustituido))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsSustituto", ProsegurDbType.Logico, Me.EsSustituto))

        If IdDocDetalles <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocDetalles", ProsegurDbType.Inteiro_Longo, IdDocDetalles))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocDetalles", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        If IdDocBultos <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocBultos", ProsegurDbType.Inteiro_Longo, IdDocBultos))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocBultos", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        If IdDocCamposExtra <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocCamposExtra", ProsegurDbType.Inteiro_Longo, IdDocCamposExtra))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocCamposExtra", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        If Me.Primordial.Id <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPrimordial", ProsegurDbType.Inteiro_Longo, Me.Primordial.Id))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPrimordial", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        End If

        ' tratar campos do formulario
        TratarCamposFormulario(comando)

        ' Insere os dados do documento
        objTransacao.AdicionarItemTransacao(comando)

        ' registrar detalles
        RegistrarDetalles(IdDocDetalles, objTransacao)

        ' registrar campos extra
        Mensagens.AddRange(RegistrarCampoExtra(IdDocCamposExtra, objTransacao))

        ' Registro de los Bultos
        RegistrarBultos(IdDocBultos, objTransacao)

        ' Registro de los sobres
        RegistrarSobres(objTransacao)

        'si el doc es un grupo hay que setear el valor de los campos fijos igual a este para todos los que cuelgan
        If Me.EsGrupo Then
            GrupoDocumentosSeanCamposFijos(objTransacao)
        End If

        If Me.Legado AndAlso IdDocumentoEnviadoLegado > 0 Then

            AtualizarDocumentoExportadoLegado(IdDocumentoEnviadoLegado, objTransacao)

        End If

        ' realizar a transação
        objTransacao.RealizarTransacao()

        'si el doc esta basado en un reporte de docs hay que registrar todos los que cuelgan
        If Me.EsGrupo AndAlso (Me.Formulario.BasadoEnReporte OrElse Me.Formulario.BasadoEnSaldos) Then
            DocumentosReporteRegistrar()
        End If

        Return Mensagens

    End Function

    ''' <summary>
    ''' Trata os valores dos campos do formulário
    ''' </summary>
    ''' <param name="Comando"></param>
    ''' <remarks></remarks>
    Private Sub TratarCamposFormulario(ByRef Comando As IDbCommand)

        ' percorre os campos do formulario associoado ao doc
        ' atualizando os parametros referentes aos campos encontrados
        For Each campo In Me.Formulario.Campos

            Select Case campo.Tipo

                Case "I"

                    If (IsNumeric(campo.IdValor) AndAlso campo.IdValor <> 0) Then

                        CType(Comando.Parameters(campo.Nombre), IDbDataParameter).Value = campo.IdValor

                        If campo.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN Then
                            CType(Comando.Parameters(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESODESTINO), IDbDataParameter).Value = campo.IdValor
                        End If

                        If campo.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEORIGEN Then
                            CType(Comando.Parameters(Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEDESTINO), IDbDataParameter).Value = campo.IdValor
                        End If

                        If campo.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCO Then
                            CType(Comando.Parameters(Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCODEPOSITO), IDbDataParameter).Value = campo.IdValor
                        End If

                    Else
                        CType(Comando.Parameters(campo.Nombre), IDbDataParameter).Value = DBNull.Value
                    End If

                Case "A", "F"
                    CType(Comando.Parameters(campo.Nombre), IDbDataParameter).Value = campo.Valor

            End Select

        Next

    End Sub

    ''' <summary>
    ''' Apaga e insere todos os sobres de um documento
    ''' </summary>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Private Sub RegistrarSobres(ByRef objTransacao As Transacao)

        If Me.Formulario.EsActaProceso Then

            If Not Me.Sobres Is Nothing Then

                ' Exclui os sobres antigos
                Me.Sobres.BorrarSobres(Me.Id, objTransacao)

                ' Grava os novos sobres
                Me.Sobres.SobresRegistrar(Me.Id, objTransacao)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Apaga e insere todos os bultos de um documento
    ''' </summary>
    ''' <param name="IdDocBultos"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Private Sub RegistrarBultos(IdDocBultos As Integer, ByRef objTransacao As Transacao)

        If Me.Formulario.ConBultos AndAlso Not Me.EsGrupo Then

            If Not Me.Bultos Is Nothing Then

                If IdDocBultos = 0 Then

                    ' Exclui os bultos antigos
                    Me.Bultos.BorrarBultos(Me.Id, objTransacao)

                    ' Grava os novos bultos
                    Me.Bultos.BultosRegistrar(Me.Id, objTransacao)

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Apaga e insere todos os detalles de um documento
    ''' </summary>
    ''' <param name="IdDocDetalles"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Private Sub RegistrarDetalles(IdDocDetalles As Integer, ByRef objTransacao As Transacao)

        ' Registro de los Detalles
        If Me.Formulario.ConValores AndAlso Not Me.EsGrupo Then

            If Not Me.Detalles Is Nothing Then

                If IdDocDetalles = 0 Then

                    ' apagar detalles
                    Me.Detalles.BorrarDetalles(Me.Id, objTransacao)

                    ' gravar novos detalles
                    Me.Detalles.GravarDetalles(Me.Id, objTransacao)

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Apaga e insere todos os campos extras de um documento
    ''' </summary>
    ''' <param name="IdDocCamposExtra"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Private Function RegistrarCampoExtra(IdDocCamposExtra As Integer, _
                                         ByRef objTransacao As Transacao) As List(Of String)

        ' controla as validações da função
        Dim Validacoes As New List(Of String)

        ' Registro de los campos extras
        If Not Me.Formulario.CamposExtra Is Nothing Then

            If IdDocCamposExtra = 0 Then

                ' apagar campos extra
                Me.Formulario.CamposExtra.BorrarCamposExtra(Me.Id, objTransacao)

                For Each objCampoExtra In Me.Formulario.CamposExtra

                    If objCampoExtra.SeValida Then

                        'valida campo
                        If objCampoExtra.Nombre = "UNICODE" Then

                            ' chama a procedure unicode
                            If Util.ValidarUNICODE(objCampoExtra.Valor) = 1 Then
                                'Validacoes.Add(String.Format(Traduzir("023_msg_valor_campo_extra_invalido"), objCampoExtra.Nombre))
                            End If

                        End If

                    End If

                    ' insere no banco
                    objCampoExtra.CampoExtraValorRegistrar(Me.Id, objTransacao)

                Next

            End If

        End If

        Return Validacoes

    End Function

    Private Function DocumentoVerificaNumExterno(numExterno As String, IdDocumento As Integer, IdOrigen As Integer) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoVerificaNumExterno.ToString()

        ' parametros de verificação
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumExterno", ProsegurDbType.Descricao_Longa, numExterno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdOrigen", ProsegurDbType.Identificador_Numerico, IdOrigen))

        Dim objRetorno As Int32 = AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

        If objRetorno > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se o tipo de documento é de saída a partir do número externo.
    ''' </summary>
    ''' <param name="numExterno"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra] 13/08/2012 - Criado
    ''' </history>
    Private Function DocumentoVerificaNumExternoTipoFormulario(numExterno As String, IdDocumento As Integer, IdOrigen As Integer) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        Dim codFormularioSalida As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("codFormularioSalida").ToString()

        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoVerificaNumExternoTipoFormulario.ToString()

        ' parametros de verificação
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumExterno", ProsegurDbType.Descricao_Longa, numExterno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdOrigen", ProsegurDbType.Identificador_Numerico, IdOrigen))

        Dim objRetorno As Int32 = AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

        Return codFormularioSalida.Contains(objRetorno)

    End Function

    ''' <summary>
    ''' Atualiza os clientes do documento.
    ''' </summary>
    ''' <param name="IdDocumento"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/11/2010 - Criado
    ''' </history>
    Public Shared Sub ActualizarClientesDocumento(IdDocumento As Integer)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.DocumentoAtualizarClientes.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDORIGEN", ProsegurDbType.Identificador_Numerico, IdDocumento))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    ''' <summary>
    ''' Verificar se o numero externo está associado a um documento do tipo rechazo
    ''' caso esteja associado é possível adicionar novo documento com um numero externo já existente
    ''' caso contrario o sistema deve avisar que o numero externo já existe
    ''' </summary>
    ''' <param name="numExterno"></param>
    ''' <param name="IdDocumento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DocumentoVerificarNumExternoAssociadoRechazo(numExterno As String, IdDocumento As Integer) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoVerificarNumExternoAssociadoRechazo.ToString()

        ' parametros de verificação
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumExterno", ProsegurDbType.Descricao_Longa, numExterno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Identificador_Numerico, Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("IdentificadorActaRechazo")))

        Dim objRetorno As Int32 = AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

        If objRetorno > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Sub GrupoDocumentosSeanCamposFijos(ByRef objTransacao As Transacao)

        Dim dtCentrosProceso As DataTable = BuscaCentroProcesoOrigemDestinoPorIdGrupo()

        ' verificar se foram retornados registros
        If dtCentrosProceso IsNot Nothing AndAlso dtCentrosProceso.Rows.Count > 0 Then

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
            comando.CommandType = CommandType.Text
            comando.CommandText = My.Resources.GrupoDocumentosSeanCamposFijosUpdate.ToString()

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoOrigen", ProsegurDbType.Identificador_Numerico, dtCentrosProceso.Rows(0)("IdCentroProcesoOrigen")))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoDestino", ProsegurDbType.Identificador_Numerico, dtCentrosProceso.Rows(0)("IdCentroProcesoDestino")))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGrupo", ProsegurDbType.Identificador_Numerico, Me.Id))

            objTransacao.AdicionarItemTransacao(comando)

        End If

    End Sub

    Public Function BuscaCentroProcesoOrigemDestinoPorIdGrupo() As DataTable


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoCentroProcesoOrigemDestino.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGrupo", ProsegurDbType.Identificador_Numerico, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
        Dim retorno As DataTable = Nothing

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            retorno = dt
        End If

        Return retorno

    End Function

    ''' <summary>
    ''' Busca o Id do documento de acordo com o número externo.
    ''' </summary>
    ''' <param name="numExterno">Número externo do documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - Criação
    ''' </history>
    Public Shared Function BuscaDocumentoPorNumExterno(numExterno As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoBuscaPorNumExterno.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NUMEXTERNO", ProsegurDbType.Identificador_Alfanumerico, numExterno))

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
    End Function
    Public Shared Function BuscaUltimoDocumentoReenvio(numExterno As String, codigoDelegacion As String, identificadoresFormulario As String()) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoBuscaUltimoDocumentoRemessa.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NUM_EXTERNO", ProsegurDbType.Identificador_Alfanumerico, numExterno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

        If identificadoresFormulario IsNot Nothing AndAlso identificadoresFormulario.Count > 0 Then
            comando.CommandText = String.Format(comando.CommandText, " and d.idformulario in (" & String.Join(", ", identificadoresFormulario) & ")")
        Else
            comando.CommandText = String.Format(comando.CommandText, "")
        End If

        Dim dtDocumento As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
        If dtDocumento IsNot Nothing AndAlso dtDocumento.Rows.Count > 0 Then
            Return dtDocumento.Rows(0)("IdDocumento").ToString()
        Else
            Return String.Empty
        End If
    End Function

    Private Sub DocumentosReporteRegistrar()

        Dim DocEnReporte As Documento
        Dim DocCreado As Documento
        Dim campo As Campo
        Dim CampoExtra As CampoExtra

        For Each DocEnReporte In Me.DocumentosDelReporte

            DocCreado = New Documento

            With DocCreado

                .Id = DocEnReporte.Id
                .Formulario.Id = Me.Formulario.Id
                .Formulario.Realizar()

                For Each campo In .Formulario.Campos

                    Dim PesqCampo = From ECampos In Me.Formulario.Campos _
                                    Where ECampos.Nombre = campo.Nombre

                    If PesqCampo IsNot Nothing AndAlso PesqCampo.Count > 0 Then

                        campo.IdValor = PesqCampo(0).IdValor
                        campo.Valor = PesqCampo(0).Valor

                    End If

                    Dim PesqCampoEnReporte = From objCampo In DocEnReporte.Formulario.Campos _
                                             Where objCampo.Nombre = campo.Nombre

                    Dim CampoLocalizado As Boolean = False
                    If PesqCampoEnReporte IsNot Nothing AndAlso PesqCampoEnReporte.Count > 0 Then
                        CampoLocalizado = True
                    End If

                    If campo.Valor IsNot DBNull.Value _
                        AndAlso campo.Valor = String.Empty _
                        AndAlso campo.IdValor = 0 _
                        AndAlso CampoLocalizado Then
                        campo.IdValor = PesqCampoEnReporte(0).IdValor
                        campo.Valor = PesqCampoEnReporte(0).Valor
                    End If

                Next

                Dim LocalizaCampoCentroProcesoDestino = From objCampo In .Formulario.Campos _
                                                        Where objCampo.Nombre = "CentroProcesoDestino"

                If LocalizaCampoCentroProcesoDestino.Count > 0 Then
                    LocalizaCampoCentroProcesoDestino(0).IdValor = LocalizaCampoCentroProcesoDestino(0).IdValor
                End If

                .Formulario.Campos = ValidarCamposFijos(.Formulario.Campos)

                For Each CampoExtra In DocEnReporte.Formulario.CamposExtra

                    Dim pesqCampoExtra = From objCampoExtra In .Formulario.CamposExtra _
                                         Where objCampoExtra.Id = CampoExtra.Id

                    If pesqCampoExtra IsNot Nothing AndAlso pesqCampoExtra.Count > 0 Then
                        pesqCampoExtra(0).Valor = CampoExtra.Valor
                    End If

                Next
                Dim cp = New Negocio.CentroProceso
                Dim idCP As Negocio.Campo = .Formulario.Campos.FirstOrDefault(Function(f) f.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN)
                cp.Id = If(idCP IsNot Nothing, idCP.IdValor, 0)
                cp.Realizar()
                cp.Planta.Realizar()



                .Formulario.CamposExtra.Documento.Id = DocEnReporte.Formulario.CamposExtra.Documento.Id
                .Agrupado = True
                .Bultos.Documento.Id = DocEnReporte.Bultos.Documento.Id
                .Detalles = DocEnReporte.Detalles
                .Detalles.Documento.Id = DocEnReporte.Detalles.Documento.Id
                .EsGrupo = False
                .Fecha = Util.GetDateTime(cp.Planta.CodDelegacionGenesis) 'Me.GetDateTime()
                .FechaGestion = DocEnReporte.FechaGestion
                .Grupo.Id = Me.Id
                .Usuario = Me.Usuario
                .Origen.Id = DocEnReporte.Origen.Id
                .Reenviado = False
                .Disponible = DocEnReporte.Disponible

                If DocEnReporte.Primordial.Id = 0 Then
                    .Primordial.Id = DocEnReporte.Origen.Id
                Else
                    .Primordial.Id = DocEnReporte.Primordial.Id
                End If

            End With

            DocCreado.Registrar(False)

        Next

    End Sub

    Private Function DocumentosReporteExiste(ByRef claveMBC As String) As Boolean

        Dim Documento_Renamed As Documento
        Return False
        For Each Documento_Renamed In Me.DocumentosDelReporte
            Return (Documento_Renamed.claveMBC = claveMBC)
            If DocumentosReporteExiste Then Exit For
        Next Documento_Renamed

    End Function

    Private Function DocumentosReporte(ByRef claveMBC As String) As Documento

        Dim Documento_Renamed As Documento
        For Each Documento_Renamed In Me.DocumentosDelReporte
            If Documento_Renamed.claveMBC = claveMBC Then
                Return Documento_Renamed
                Exit For
            End If
        Next Documento_Renamed

        Return Nothing

    End Function

    Public Shared Function ObterIdDocumento() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.SDocumento.ToString()

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    Public Sub CambiarEstado(ByRef IdEstado As Integer)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoCambiarEstado.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Id", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Fecha", ProsegurDbType.Data_Hora, Me.Fecha))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstado", ProsegurDbType.Inteiro_Longo, IdEstado))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

        If (IdEstado = 3) Then

            GenerarTransaccion(1)

        End If

        If (IdEstado = 4) Then

            GenerarTransaccion(0)

        End If

    End Sub

    Protected Sub GenerarTransaccion(salida As Integer)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "PD_GenerarTransaccion"

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Id", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Salida", ProsegurDbType.Data_Hora, salida))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Signo", ProsegurDbType.Data_Hora, 0))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Function Imprimir() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "PD_DocumentoImprimir_" & Prosegur.Genesis.Comon.Util.Version
        comando.Parameters.Add(AcessoDados.CriarParametroProcedure(Constantes.CONEXAO_SALDOS, "v_IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id, ParameterDirection.Input))
        comando.Parameters.Add(AcessoDados.CriarParametroProcedure(Constantes.CONEXAO_SALDOS, "v_IdUsuario", ProsegurDbType.Identificador_Numerico, Me.Usuario.Id, ParameterDirection.Input))
        comando.Parameters.Add(AcessoDados.CriarParametroProcedure(Constantes.CONEXAO_SALDOS, "v_Fecha", ProsegurDbType.Data_Hora, Me.GetDateTime, ParameterDirection.Input))
        comando.Parameters.Add(AcessoDados.CriarParametroProcedure(Constantes.CONEXAO_SALDOS, "v_FechaResuelve", ProsegurDbType.Data_Hora, Me.GetDateTime, ParameterDirection.Input))
        comando.Parameters.Add(AcessoDados.CriarParametroProcedure(Constantes.CONEXAO_SALDOS, "Result", ProsegurDbType.Inteiro_Curto, DBNull.Value, ParameterDirection.ReturnValue))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

        Return CType(comando.Parameters("Result"), IDbDataParameter).Value

    End Function

    Public Sub Aceptar()

        Dim CentroProcesoDestinoId As Integer = 0
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "PD_DocumentoAceptar_" & Prosegur.Genesis.Comon.Util.Version

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdUsuarioResuelve", ProsegurDbType.Identificador_Numerico, Me.UsuarioResolutor.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ExportadoSalidas", ProsegurDbType.Logico, Me.ExportadoSalidas))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FechaResuelve", ProsegurDbType.Data_Hora, Me.GetDateTime))

        Dim LocalizaCampoCentroProcesoDestino = From objCampo In Me.Formulario.Campos _
                                                Where objCampo.Nombre = "CentroProcesoDestino"

        If LocalizaCampoCentroProcesoDestino.Count > 0 Then
            CentroProcesoDestinoId = LocalizaCampoCentroProcesoDestino(0).IdValor
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdCentroProcesoDestino", ProsegurDbType.Inteiro_Longo, CentroProcesoDestinoId))
        comando.Parameters.Add(AcessoDados.CriarParametroProcedure(Constantes.CONEXAO_SALDOS, "Result", ProsegurDbType.Inteiro_Curto, DBNull.Value, ParameterDirection.ReturnValue))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Sub Rechazar()

        Dim CentroProcesoDestinoId As Integer = 0
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "PD_DocumentoRechazar_" & Prosegur.Genesis.Comon.Util.Version

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdUsuarioResuelve", ProsegurDbType.Identificador_Numerico, Me.UsuarioResolutor.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FechaResuelve", ProsegurDbType.Data_Hora, Me.GetDateTime))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ExportadoSalidas", ProsegurDbType.Logico, Me.ExportadoSalidas))

        Dim LocalizaCampoCentroProcesoDestino = From objCampo In Me.Formulario.Campos _
                                                Where objCampo.Nombre = "CentroProcesoDestino"


        If LocalizaCampoCentroProcesoDestino IsNot Nothing AndAlso LocalizaCampoCentroProcesoDestino.Count > 0 Then
            CentroProcesoDestinoId = LocalizaCampoCentroProcesoDestino(0).IdValor
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdCentroProcesoDestino", ProsegurDbType.Inteiro_Longo, CentroProcesoDestinoId))
        comando.Parameters.Add(AcessoDados.CriarParametroProcedure(Constantes.CONEXAO_SALDOS, "Result", ProsegurDbType.Inteiro_Curto, DBNull.Value, ParameterDirection.ReturnValue))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    ''' <summary>
    ''' Elimina documentos utilizando transação.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EliminarPorTransacao()

        ' criar tranzação
        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        ' chamar regra para eliminar documento
        Eliminar(objTransacao)

        ' realizar transação
        objTransacao.RealizarTransacao()

    End Sub

    Public Sub Eliminar(ByRef objTransacao As Transacao)

        Dim DocAgrupado As Documento = Nothing
        Dim Extracto As Extracto = Nothing

        Me.Realizar()
        If Me.EsGrupo Then

            If Me.Documentos.Count = 0 Then
                Me.Documentos.IdGrupo = Me.Id
                Me.Documentos.Realizar()
            End If

            For Each DocAgrupado In Me.Documentos
                DocAgrupado.Usuario.Id = Me.Usuario.Id
                DocAgrupado.Eliminar(objTransacao)
            Next

        End If

        If Me.Formulario.BasadoEnExtracto Then
            Extracto = New Extracto
            Extracto.Documento.Id = Me.Id
            Extracto.Eliminar(objTransacao)
        End If

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "PD_DOCUMENTOELIMINAR_" & Prosegur.Genesis.Comon.Util.Version
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Function ValidarCamposFijos(ByRef Campos As Campos) As Campos

        For Each campo As Campo In Campos

            If campo.Valor = "" AndAlso campo.IdValor = 0 Then

                Select Case campo.Nombre
                    Case "ClienteDestino"

                        Dim _Campo = From objCampo In Campos Where objCampo.Nombre = "ClienteOrigen"

                        If Not _Campo Is Nothing AndAlso _Campo.Count > 0 Then
                            campo.IdValor = _Campo(0).IdValor
                        End If

                    Case "BancoDeposito"

                        Dim _Campo = From objCampo In Campos Where objCampo.Nombre = "Banco"

                        If Not _Campo Is Nothing AndAlso _Campo.Count > 0 Then
                            campo.IdValor = _Campo(0).IdValor
                        End If

                End Select

            End If

        Next

        Return Campos

    End Function

    Public Sub DocumentosReporteGenerar(TipoReporte As String, Parametros As Object)

        Dim newDoc As Documento = Nothing
        Dim Existe As Boolean = False
        Dim Saldos As Saldos = Nothing
        Dim Saldo As Saldo = Nothing
        Dim SaldoClaveMBC As String = String.Empty
        Dim Especie As Especie = Nothing
        Dim ListaIdCentroProceso As String = String.Empty
        Dim ListaIdMoneda As String = String.Empty
        Dim ListaClaveMBC As String = String.Empty
        Dim strSoloSaldoDisponible As String = String.Empty
        Dim detalleDoc As Detalle = Nothing
        Dim totalDetalleDoc As Total = Nothing

        Select Case TipoReporte

            Case "Carga"

                Me.GrupoDocumentosRealizar()

                Dim ListaParametros() As String = Split(Parametros, ",")

                For Each DocId In ListaParametros

                    Existe = False

                    For Each auxDoc In Me.Documentos
                        If auxDoc.Origen.Id = DocId Then
                            Existe = True
                            Exit For
                        End If
                    Next

                    If Not Existe Then

                        newDoc = New Documento

                        newDoc.Grupo = Nothing
                        newDoc.NumComprobante = String.Empty
                        newDoc.Fecha = Date.MinValue
                        newDoc.FechaResolucion = Date.MinValue
                        newDoc.FechaGestion = Date.MinValue
                        newDoc.Id = DocId
                        newDoc.Agrupado = True
                        newDoc.EsGrupo = False
                        newDoc.Reenviado = False
                        newDoc.Disponible = False
                        newDoc.FechaDispone = Date.MinValue

                        newDoc.Realizar()
                        newDoc.Origen.Id = newDoc.Id
                        newDoc.Id = 0

                        Me.DocumentosDelReporte.Add(newDoc)

                    End If

                Next

            Case "Saldos"

                'Antes de Generar los documentos en Me.DocumentosDelReporte
                'se cargan los docs ya generados realizando el grupo y copiando a Me.DocumentosDelReporte
                'despues el metodo ".Existe" impide generar docs duplicados utilizando la clave (IdMoneda,IdBanco,Idcliente)

                Dim ListaParametros() As String = Split(Parametros, "+")

                ListaIdCentroProceso = ListaParametros(0)
                ListaIdMoneda = ListaParametros(1)
                ListaClaveMBC = ListaParametros(2)
                strSoloSaldoDisponible = ListaParametros(3)

                If Me.Id > 0 Then

                    ' limpar lista de documentos
                    Me.Documentos.Clear()
                    ' obter documentos do banco
                    Me.GrupoDocumentosRealizar()
                    ' setar documento encontrados para lista de reportes
                    Me.DocumentosDelReporte = Me.Documentos

                    Dim NovaColecaoDocumentosDelReporte As New Documentos
                    For Each auxDoc In Me.DocumentosDelReporte

                        ' verifica se a chave do doc existe na lista, se existe adiciona para nova coleção.
                        ' se não existe remove do banco
                        If InStr(ListaClaveMBC, auxDoc.claveMBC) > 0 Then
                            ' adicionar para nova coleção
                            NovaColecaoDocumentosDelReporte.Add(auxDoc)
                        Else
                            ' remover documento do banco
                            auxDoc.EliminarPorTransacao()
                        End If

                    Next

                    ' adiconar documentos da nova coleção para documentos do reporte
                    Me.DocumentosDelReporte = NovaColecaoDocumentosDelReporte

                End If

                ' realiza saldos
                Saldos = New Negocio.Saldos()
                With Saldos
                    .ListaIdCentroProceso = "|" & ListaIdCentroProceso & "|"
                    .ListaIdMoneda = "|" & ListaIdMoneda & "|"
                    .DiscriminarEspecies = True
                    .IntegrarCentrosProceso = True
                    .TodosBancos = True
                    .TodosClientes = True
                    .Actual = True
                    .SoloSaldoDisponible = (strSoloSaldoDisponible = "SOLODISPONIBLE")
                    .Realizar()
                End With

                If Saldos.SumasSobreCPs.Count > 0 Then

                    'Grupo = New Negocio.Documento()
                    'Grupo.Id = Me.Id

                    'Usuario = New Negocio.Usuario()
                    'Usuario.Id = Me.Usuario.Id

                    Dim docReporte As Documento = Nothing
                    For Each Saldo In Saldos.SumasSobreCPs

                        SaldoClaveMBC = "M" & Saldo.IdMoneda.ToString _
                                        & "B" & Saldo.IdBanco.ToString _
                                        & "C" & Saldo.IdCliente.ToString

                        If InStr(ListaClaveMBC, SaldoClaveMBC) > 0 Then 'solo lo que elije el usuario

                            ' se existir algum doc com a claveMBC igual a SaldoClaveMBC
                            ' docReportes = Nothing
                            Dim docReportes = From objDocReporte In Me.DocumentosDelReporte _
                                              Where objDocReporte.claveMBC = SaldoClaveMBC

                            If docReportes IsNot Nothing AndAlso docReportes.Count > 0 Then
                                docReporte = docReportes(0)
                            Else
                                docReporte = DocumentosReporteAdd(Grupo, Usuario, Nothing, Saldo.IdBanco, Saldo.IdCliente)
                            End If

                            For Each Fajo In Saldo.Fajos

                                Especie = New Negocio.Especie()
                                Especie.Id = Fajo.IdEspecie

                                Dim detallesDoc = From objDetalle In docReporte.Detalles _
                                                  Where objDetalle.Especie.Id = Especie.Id

                                If detallesDoc IsNot Nothing _
                                    AndAlso detallesDoc.Count > 0 Then

                                    ' obter detalle
                                    detalleDoc = detallesDoc(0)

                                Else
                                    ' criar novo detalle
                                    detalleDoc = New Detalle
                                    detalleDoc.Especie = Especie
                                    detalleDoc.Cantidad = 0
                                    detalleDoc.Importe = 0
                                    ' adicionar para coleção de detalles
                                    docReporte.Detalles.Add(detalleDoc)
                                End If

                                detalleDoc.Cantidad = Fajo.Cantidad
                                detalleDoc.Importe = Fajo.Importe
                                detalleDoc.Especie.Moneda.Id = Saldo.IdMoneda

                            Next

                            Dim totalesDetalleDoc = From objTotale In docReporte.Detalles.Totales _
                                                    Where objTotale.Moneda.Id = Saldo.IdMoneda

                            If totalesDetalleDoc IsNot Nothing _
                                AndAlso totalesDetalleDoc.Count > 0 Then

                                totalDetalleDoc = totalesDetalleDoc(0)

                            Else

                                totalDetalleDoc = New Total
                                totalDetalleDoc.Importe = 0
                                totalDetalleDoc.Moneda = detalleDoc.Especie.Moneda
                                totalDetalleDoc.HayUniformes = True
                                totalDetalleDoc.HayNoUniformes = False

                                docReporte.Detalles.Totales.Add(totalDetalleDoc)

                            End If

                            totalDetalleDoc.Importe += Saldo.Importe

                        End If

                    Next

                End If

        End Select

    End Sub

    Public Sub GrupoDocumentosRealizar()

        Dim idDocDetalhe As Integer = Nothing
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.GrupoDocumentosRealizar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGrupo", ProsegurDbType.Identificador_Numerico, Me.Id))

        Dim dtDocumento As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dtDocumento IsNot Nothing AndAlso dtDocumento.Rows.Count > 0 Then

            Dim objDocumento As Documento = Nothing

            For Each dr As DataRow In dtDocumento.Rows

                objDocumento = New Documento

                If dr("IdDocumento") Is DBNull.Value Then
                    objDocumento.Id = 0
                Else
                    objDocumento.Id = dr("IdDocumento")
                End If

                If dr("IdGrupo") Is DBNull.Value Then
                    objDocumento.Grupo.Id = 0
                Else
                    objDocumento.Grupo.Id = dr("IdGrupo")
                End If

                If dr("IdOrigen") Is DBNull.Value Then
                    objDocumento.Origen.Id = 0
                Else
                    objDocumento.Origen.Id = dr("IdOrigen")
                End If

                If dr("IdEstadoComprobante") Is DBNull.Value Then
                    objDocumento.EstadoComprobante.Id = 0
                Else
                    objDocumento.EstadoComprobante.Id = dr("IdEstadoComprobante")
                End If

                If dr("IdUsuario") Is DBNull.Value Then
                    objDocumento.Usuario.Id = 0
                Else
                    objDocumento.Usuario.Id = dr("IdUsuario")
                End If

                If dr("IdUsuarioResuelve") Is DBNull.Value Then
                    objDocumento.UsuarioResolutor.Id = 0
                Else
                    objDocumento.UsuarioResolutor.Id = dr("IdUsuarioResuelve")
                End If

                If dr("FechaResuelve") Is DBNull.Value Then
                    objDocumento.FechaResolucion = Date.MinValue
                Else
                    objDocumento.FechaResolucion = dr("FechaResuelve")
                End If

                If dr("FechaGestion") Is DBNull.Value Then
                    objDocumento.FechaGestion = Date.MinValue
                Else
                    objDocumento.FechaGestion = dr("FechaGestion")
                End If

                If dr("IdUsuarioDispone") Is DBNull.Value Then
                    objDocumento.UsuarioDispone.Id = 0
                Else
                    objDocumento.UsuarioDispone.Id = dr("IdUsuarioDispone")
                End If

                If dr("FechaDispone") Is DBNull.Value Then
                    objDocumento.FechaDispone = Date.MinValue
                Else
                    objDocumento.FechaDispone = dr("FechaDispone")
                End If

                objDocumento.Formulario = New Formulario
                objDocumento.Formulario.Id = dr("IdFormulario")
                objDocumento.Formulario.Realizar(1)

                ' percorre os campos do formulario
                For Each campo In objDocumento.Formulario.Campos

                    Select Case campo.Tipo
                        Case "I"

                            If dr("Id" & campo.Nombre) Is DBNull.Value Then
                                campo.IdValor = 0
                            Else
                                campo.IdValor = dr("Id" & campo.Nombre)
                            End If

                            If dr(campo.Nombre & "Des") Is DBNull.Value Then
                                campo.Valor = String.Empty
                            Else
                                campo.Valor = dr(campo.Nombre & "Des")
                            End If

                        Case "A"

                            If dr(campo.Nombre) Is DBNull.Value Then
                                campo.Valor = String.Empty
                            Else
                                campo.Valor = dr(campo.Nombre)
                            End If

                        Case "F"

                            If dr(campo.Nombre) Is DBNull.Value Then
                                campo.Valor = Date.MinValue
                            Else
                                campo.Valor = dr(campo.Nombre)
                            End If

                    End Select

                Next

                If dr("IdDocDetalles") Is DBNull.Value Then
                    idDocDetalhe = 0
                Else
                    idDocDetalhe = dr("IdDocDetalles")
                End If

                ' se documento for com valores
                If objDocumento.Formulario.ConValores Then

                    If dr("IdDocDetalles") IsNot Nothing _
                        AndAlso Convert.ToInt32(dr("IdDocDetalles")) = 0 Then
                        objDocumento.Detalles.Documento.Id = objDocumento.Id
                    Else
                        objDocumento.Detalles.Documento.Id = Convert.ToInt32(dr("IdDocDetalles"))
                    End If

                    objDocumento.Detalles.Realizar()

                    ' para cada registro
                    For Each DocAgregadoTotal In objDocumento.Detalles.Totales

                        ' declarar objeto total
                        Dim objTotal As Total = Nothing

                        ' verificar se existe na coleção de detalhes
                        Dim resultDocTotal = From objTotales In Me.Detalles.Totales _
                                             Where objTotales.Moneda.Id = DocAgregadoTotal.Moneda.Id

                        ' se encontrou algum doc total
                        If resultDocTotal IsNot Nothing AndAlso resultDocTotal.Count > 0 Then
                            ' setar para objeto total
                            objTotal = resultDocTotal(0)
                        Else
                            ' criar novo objeto
                            objTotal = New Total
                            objTotal.Moneda = DocAgregadoTotal.Moneda
                            objTotal.Importe = 0
                            objTotal.HayNoUniformes = False
                            objTotal.HayUniformes = False

                            ' adicionar para coleção
                            Me.Detalles.Totales.Add(objTotal)
                        End If

                        ' incrementar objeto novo/encontrado com valores do objeto do loop
                        objTotal.Importe += DocAgregadoTotal.Importe
                        objTotal.HayNoUniformes = DocAgregadoTotal.HayNoUniformes
                        objTotal.HayUniformes = DocAgregadoTotal.HayUniformes

                    Next

                End If

                ' se documento for com bultos
                If objDocumento.Formulario.ConBultos Then

                    If dr("IdDocDetalles") IsNot Nothing _
                        AndAlso Convert.ToInt32(dr("IdDocDetalles")) = 0 Then
                        objDocumento.Bultos.Documento.Id = objDocumento.Id
                    Else
                        objDocumento.Bultos.Documento.Id = Convert.ToInt32(dr("IdDocDetalles"))
                    End If

                    ' obter bultos
                    objDocumento.Bultos.Realizar()

                    For Each Bulto In objDocumento.Bultos
                        Me.Bultos.Add(Bulto)
                    Next

                End If

                ' adicionar para coleção
                Me.Documentos.Add(objDocumento)

            Next

        End If

    End Sub

    Private Sub ClaveMBCGenerar()

        Dim ClaveM As String = String.Empty
        Dim ClaveB As String = String.Empty
        Dim ClaveC As String = String.Empty

        If Me.Detalles.Totales.Count = 1 Then

            ClaveM = "M" & Me.Detalles.Totales(0).Moneda.Id.ToString

            Dim cBanco = From objCampo In Me.Formulario.Campos _
                         Where objCampo.Nombre = "Banco"

            If cBanco IsNot Nothing _
                AndAlso cBanco.Count > 0 Then

                ClaveB = "B" & cBanco(0).IdValor.ToString

                Dim cClienteOrigen = From objCampo In Me.Formulario.Campos _
                                     Where objCampo.Nombre = "ClienteOrigen"

                If cClienteOrigen IsNot Nothing _
                    AndAlso cClienteOrigen.Count > 0 Then

                    ClaveC = "C" & cClienteOrigen(0).IdValor.ToString
                    Me.claveMBC = ClaveM + ClaveB + ClaveC

                End If

            Else
                Me.claveMBC = "MBC"
            End If

        Else
            Me.claveMBC = "MBC"
        End If

    End Sub

    Public Function Disponer() As Integer

        ' criar objeto documento
        Dim DocAgrupado As Documento

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "PD_DocumentoDisponer_" & Prosegur.Genesis.Comon.Util.Version

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuarioDispone", ProsegurDbType.Identificador_Numerico, Me.UsuarioDispone.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_Fechadispone", ProsegurDbType.Data_Hora, Me.GetDateTime))

        Dim retornoProcedure As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

        If retornoProcedure = 0 Then

            Me.Realizar()

            If Me.EsGrupo Then

                Me.Documentos.IdGrupo = Me.Id
                Me.Documentos.Realizar()

                For Each DocAgrupado In Me.Documentos

                    DocAgrupado.Usuario.Id = Me.Usuario.Id
                    Return DocAgrupado.Disponer()

                Next

            End If

        End If

    End Function


    Public Function Sustituir(Optional ByRef GrupoId As Integer = 0) As List(Of String)

        Dim Validacoes As New List(Of String)
        Dim DocAgrupado As Documento
        Dim SustituidoId As Integer = Me.Id

        ' Se o documento não veio do Legado.

        Me.Realizar()
        Me.Origen.Id = Me.Id
        Me.EsSustituto = True
        Me.Id = 0

        If Me.Agrupado Then
            Me.Grupo.Id = GrupoId
        End If

        With Me.Bultos
            .Realizar()
            .Documento.Id = 0
        End With

        With Me.Detalles
            .Realizar()
            .Documento.Id = 0
        End With

        With Me.Formulario.CamposExtra
            .Realizar()
            .Documento.Id = 0
        End With

        ' obtem as validações ao registrar documento
        Validacoes.AddRange(Me.Registrar(True))

        'si es un grupo de documentos hay que sustituir todos los que cuelgan
        If Me.EsGrupo AndAlso Validacoes.Count = 0 Then

            Me.Documentos.IdGrupo = SustituidoId

            ' se ainda não obteve os documentos
            If Me.Documentos.Count = 0 Then
                Me.Documentos.Realizar()
            End If

            For Each DocAgrupado In Me.Documentos
                DocAgrupado.Sustituir(Me.Id)
            Next

        End If

        Return Validacoes

    End Function

    Public Function CrearActa() As List(Of String)

        Me.Origen.Realizar()

        ConsolidarCampo("Banco", "BancoDeposito", "Banco")
        ConsolidarCampo("ClienteOrigen", "ClienteDestino", "ClienteOrigen")
        ConsolidarCampo("NumExterno", "NumExterno", "")

        If Me.Origen.Bultos.Documento.Id > 0 Then
            Me.Bultos.Documento.Id = Me.Origen.Bultos.Documento.Id
        Else
            Me.Bultos.Documento.Id = Me.Origen.Id
        End If

        If Me.Origen.Primordial.Id = 0 Then
            Me.Primordial.Id = Me.Origen.Id
        Else
            Me.Primordial.Id = Me.Origen.Primordial.Id
        End If

        Return Me.Registrar(False)

    End Function

    Private Sub ConsolidarCampo(Nombre As String, NombreOrigen As String, NombrePorFalta As String)

        Dim OrigenCampos As Campos = Me.Origen.Formulario.Campos
        Dim MeCampos As Campos = Me.Formulario.Campos
        Dim OrigenCampo As Campo = Nothing
        Dim MeCampo As Campo = Nothing

        Dim _OrigemCampo = From objOrigemCampo In OrigenCampos Where objOrigemCampo.Nombre = NombreOrigen
        If Not _OrigemCampo Is Nothing AndAlso _OrigemCampo.Count > 0 Then
            OrigenCampo = _OrigemCampo(0)
        Else

            Dim OrigemCampo = From objOrigemCampo In OrigenCampos Where objOrigemCampo.Nombre = NombrePorFalta
            If Not OrigemCampo Is Nothing AndAlso OrigemCampo.Count > 0 Then

                OrigenCampo = OrigemCampo(0)
            End If

        End If

        Dim OrigemCampoFormulario = From objOrigemCampo In MeCampos Where objOrigemCampo.Nombre = Nombre
        If OrigemCampoFormulario IsNot Nothing _
            AndAlso OrigemCampoFormulario.Count > 0 _
            AndAlso OrigenCampo IsNot Nothing Then

            MeCampo = OrigemCampoFormulario(0)

            MeCampo.Valor = OrigenCampo.Valor
            MeCampo.IdValor = OrigenCampo.IdValor

        End If

    End Sub

    Public Sub SeaExportado()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoSeaExportadoUpdate.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Private Function DocumentosReporteAdd(Grupo As Documento, Usuario As Usuario, Detalles As Detalles, IdBanco As Long, IdCliente As Long) As Documento

        Dim Formulario As Formulario

        Formulario = New Formulario
        Formulario.Id = Me.Formulario.Id
        Formulario.Realizar()

        Dim resultBanco = From objCampos In Formulario.Campos Where objCampos.Nombre = "Banco"

        If resultBanco IsNot Nothing AndAlso resultBanco.Count > 0 Then
            resultBanco(0).IdValor = IdBanco
        End If

        Dim resultClienteOrigen = From objCampos In Formulario.Campos Where objCampos.Nombre = "ClienteOrigen"

        If resultClienteOrigen IsNot Nothing AndAlso resultClienteOrigen.Count > 0 Then
            resultClienteOrigen(0).IdValor = IdCliente
        End If

        Dim resultBancoDeposito = From objCampos In Formulario.Campos Where objCampos.Nombre = "BancoDeposito"

        If resultBancoDeposito IsNot Nothing AndAlso resultBancoDeposito.Count > 0 Then
            resultBancoDeposito(0).IdValor = IdBanco
        End If

        Dim resultClienteDestino = From objCampos In Formulario.Campos Where objCampos.Nombre = "ClienteDestino"

        If resultClienteDestino IsNot Nothing AndAlso resultClienteDestino.Count > 0 Then
            resultClienteDestino(0).IdValor = IdCliente
        End If

        Dim cp = New Negocio.CentroProceso
        Dim idCP As Negocio.Campo = Formulario.Campos.FirstOrDefault(Function(f) f.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN)
        cp.Id = If(idCP IsNot Nothing, idCP.IdValor, 0)
        cp.Realizar()
        cp.Planta.Realizar()


        Dim newDoc = New Documento
        newDoc.Grupo = Grupo
        newDoc.NumComprobante = String.Empty
        newDoc.Fecha = Util.GetDateTime(cp.Planta.CodDelegacionGenesis)
        newDoc.FechaGestion = Util.GetDateTime(cp.Planta.CodDelegacionGenesis)
        'newDoc.Fecha = Date.Now
        'newDoc.FechaGestion = Date.Now
        newDoc.FechaResolucion = Date.MinValue
        newDoc.Id = 0
        newDoc.Formulario = Formulario
        newDoc.Detalles = Detalles
        newDoc.UsuarioResolutor = Nothing
        newDoc.Usuario = Usuario
        newDoc.EstadoComprobante = Nothing
        newDoc.Agrupado = True
        newDoc.EsGrupo = False
        newDoc.Origen = Nothing
        newDoc.Reenviado = False
        newDoc.Disponible = False
        newDoc.UsuarioDispone = Nothing
        newDoc.FechaDispone = Date.MinValue

        ' adicionar para coleção
        Me.DocumentosDelReporte.Add(newDoc)

        ' retorna novo documento
        Return newDoc

    End Function

    ''' <summary>
    ''' Metodo que grava o registros das remessas inseridas atraves do webservice
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] - 27/07/09 Criado
    ''' </history>
    Public Shared Sub RegistrarRemesa(IdDocumento As Integer, _
                                      IdRemesaOriginal As String, _
                                      IdRemesaGenesis As String, _
                                      ArchivoRemesaLegado As Byte())

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o tipo do comando
        comando.CommandType = CommandType.Text
        ' Define o script que será executado
        comando.CommandText = My.Resources.DocumentoRegistrarRemesa.ToString

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Alfanumerico, IdDocumento))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdRemesaOriginal", ProsegurDbType.Identificador_Alfanumerico, IdRemesaOriginal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdRemesaGenesis", ProsegurDbType.Identificador_Alfanumerico, IdRemesaGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ArchivoRemesaLegado", ProsegurDbType.Binario, ArchivoRemesaLegado))

        ' Executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Shared Sub AtualizarRemesa(IdDocumento As Integer, IdRemesaGenesis As String)

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o tipo do comando
        comando.CommandType = CommandType.Text
        ' Define o script que será executado
        comando.CommandText = My.Resources.DocumentoModificarRemesa.ToString

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Inteiro_Longo, IdDocumento))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdRemesaGenesis", ProsegurDbType.Identificador_Alfanumerico, IdRemesaGenesis))

        ' Executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Shared Sub ActualizarDescripcionError(IdDocumento As Integer, DescripcionError As String)

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' Define o tipo do comando
        comando.CommandType = CommandType.Text

        ' Define o script que será executado
        comando.CommandText = My.Resources.DocumentoActualizarDesError.ToString

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DES_ERR_ENVIO", ProsegurDbType.Observacao_Longa, DescripcionError))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDDOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, IdDocumento))

        ' Executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Shared Sub AtualizarDocumentoExportadoLegado(IdDocumento As Integer, ByRef objTransacao As Transacao)

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o tipo do comando
        comando.CommandType = CommandType.Text
        ' Define o script que será executado
        comando.CommandText = My.Resources.DocumentoAtualizarExportadoConteo.ToString

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))

        ' Executa o comando
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Shared Sub AtualizarDocumentoExportado(IdDocumento As Integer)

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o tipo do comando
        comando.CommandType = CommandType.Text
        ' Define o script que será executado
        comando.CommandText = My.Resources.DocumentoAtualizarExportado.ToString

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Alfanumerico, IdDocumento))

        ' Executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Shared Sub AtualizarDocumentoExportadoSalidas(IdDocumento As Integer)

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o tipo do comando
        comando.CommandType = CommandType.Text
        ' Define o script que será executado
        comando.CommandText = My.Resources.DocumentoActualizarExportadoSalidas.ToString

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDDOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, IdDocumento))

        ' Executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    ''' <summary>
    ''' Atualiza os estado de envio a Salidas dos MIFs aceitos/recusados no Saldos
    ''' </summary>
    ''' <param name="strDocumentos"></param>
    ''' <remarks></remarks>
    Public Shared Sub AtualizarMIFEnviado(strDocumentos As String)

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o tipo do comando
        comando.CommandType = CommandType.Text
        ' Define o script que será executado
        comando.CommandText = String.Format(My.Resources.DocumentoActualizarMIFEnviado.ToString(), strDocumentos)

        ' Executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    ''' <summary>
    ''' Verifica se o documento existe em um centro de processo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarExistenciaDocumentoIdPs(IdDocumento As Integer, _
                                                        IdPsCentroProceso As String) As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoValidarExistenciaDocumentoIdPsCentroProceso

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idpscentroprocesoorigen", ProsegurDbType.Identificador_Alfanumerico, IdPsCentroProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "iddocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))

        Return Convert.ToInt32(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando))

    End Function

    ''' <summary>
    ''' Verifica se o documento existe em um centro de processo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarExistenciaDocumento(IdDocumento As Integer, _
                                                        IdCentroProceso As Integer) As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoValidarExistenciaDocumentoNoCP

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idcentroprocesoorigen", ProsegurDbType.Identificador_Numerico, IdCentroProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "iddocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))

        Return Convert.ToInt32(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando))

    End Function

    Public Shared Function VerificarExistenciaDocumento(IdDocumento As String) As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoValidarExistenciaDocumento

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "iddocumento", ProsegurDbType.Identificador_Numerico, IdDocumento))

        Return Convert.ToInt32(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando))

    End Function

    ''' <summary>
    ''' Verifica se o documento existe em um centro de processo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperarDocumentoEnviadoConteo() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoRecuperarEnviadoConteo

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "iddocumento", ProsegurDbType.Identificador_Numerico, Me.Id))

        Return Convert.ToInt32(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando))

    End Function

    ''' <summary>
    ''' Verifica se o documento existe em um centro de processo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperarRemesaContadaConteo() As Byte()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DocumentoRecuperarRemesaContadaConteo

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "iddocumento", ProsegurDbType.Identificador_Numerico, Me.Id))

        Return DirectCast(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Byte())

    End Function

    ''' <summary>
    ''' Atualiza o campo reintentos_conteo, incrementando a quantidade de tentativas de envio da remessa para o conteo
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Criado em 25/01/10
    ''' </history>
    Public Sub IncrementaCantidadReintentosConteo()

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o tipo do comando
        comando.CommandType = CommandType.Text
        ' Define o script que será executado
        comando.CommandText = My.Resources.DocumentoIncrementaCantidadReintentos.ToString

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Alfanumerico, Me.Id))

        ' Executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub IncrementaCantidadDocumentoMIF()

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o tipo do comando
        comando.CommandType = CommandType.Text
        ' Define o script que será executado
        comando.CommandText = My.Resources.DocumentoMIFIncrementaCantidadReintentos.ToString()

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Alfanumerico, Me.Id))

        ' Executa o comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

#End Region

End Class