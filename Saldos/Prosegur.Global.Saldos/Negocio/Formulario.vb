Imports Prosegur.DbHelper
Imports Prosegur.Global.Saldos.Negocio.Enumeradores

<Serializable()> _
Public Class Formulario

#Region "[VARIÁVEIS]"

    Private _Campos As Campos
    Private _Id As Integer
    Private _Descripcion As String
    Private _Motivo As Motivo
    Private _Copias As Copias
    Private _CamposExtra As CamposExtra
    Private _SoloIndividual As Boolean
    Private _SoloEnGrupo As Boolean
    Private _ConValores As Boolean
    Private _ConBultos As Boolean
    Private _BasadoEnReporte As Boolean
    Private _Reporte As Reporte
    Private _BasadoEnSaldos As Boolean
    Private _UsuariosExclusividad As Usuarios
    Private _SeImprime As Boolean
    Private _Interplantas As Boolean
    Private _DistinguirPorNivel As Boolean
    Private _Matrices As Boolean
    Private _EsActaProceso As Boolean
    Private _SoloSaldoDisponible As Boolean
    Private _Color As String
    Private _Sustituible As Boolean
    Private _TiposCentroProcesoDestino As TiposCentroProceso
    Private _BasadoEnExtracto As Boolean
    Private _TotalCero As Boolean
    Private _ConLector As Boolean
    Private _DebeValidarNumExternoExistente As Boolean
    Private _EsVisibleTransacionesv5 As Boolean

#End Region

#Region "[PROPRIEDADES]"

    Public Property TotalCero() As Boolean
        Get
            TotalCero = _TotalCero
        End Get
        Set(Value As Boolean)
            _TotalCero = Value
        End Set
    End Property

    Public Property TiposCentroProcesoDestino() As TiposCentroProceso
        Get
            If _TiposCentroProcesoDestino Is Nothing Then
                _TiposCentroProcesoDestino = New TiposCentroProceso()
            End If
            TiposCentroProcesoDestino = _TiposCentroProcesoDestino
        End Get
        Set(Value As TiposCentroProceso)
            _TiposCentroProcesoDestino = Value
        End Set
    End Property

    Public Property Sustituible() As Boolean
        Get
            Sustituible = _Sustituible
        End Get
        Set(Value As Boolean)
            _Sustituible = Value
        End Set
    End Property

    Public Property Color() As String
        Get
            Color = _Color
        End Get
        Set(Value As String)
            _Color = Value
        End Set
    End Property

    Public Property SoloSaldoDisponible() As Boolean
        Get
            SoloSaldoDisponible = _SoloSaldoDisponible
        End Get
        Set(Value As Boolean)
            _SoloSaldoDisponible = Value
        End Set
    End Property

    Public Property EsActaProceso() As Boolean
        Get
            EsActaProceso = _EsActaProceso
        End Get
        Set(Value As Boolean)
            _EsActaProceso = Value
        End Set
    End Property

    Public Property SoloIndividual() As Boolean
        Get
            SoloIndividual = _SoloIndividual
        End Get
        Set(Value As Boolean)
            _SoloIndividual = Value
        End Set
    End Property

    Public Property Matrices() As Boolean
        Get
            Matrices = _Matrices
        End Get
        Set(Value As Boolean)
            _Matrices = Value
        End Set
    End Property

    Public Property DistinguirPorNivel() As Boolean
        Get
            DistinguirPorNivel = _DistinguirPorNivel
        End Get
        Set(Value As Boolean)
            _DistinguirPorNivel = Value
        End Set
    End Property

    Public Property Interplantas() As Boolean
        Get
            Interplantas = _Interplantas
        End Get
        Set(Value As Boolean)
            _Interplantas = Value
        End Set
    End Property

    Public Property SeImprime() As Boolean
        Get
            SeImprime = _SeImprime
        End Get
        Set(Value As Boolean)
            _SeImprime = Value
        End Set
    End Property

    Public Property UsuariosExclusividad() As Usuarios
        Get
            If _UsuariosExclusividad Is Nothing Then
                _UsuariosExclusividad = New Usuarios()
            End If
            UsuariosExclusividad = _UsuariosExclusividad
        End Get
        Set(Value As Usuarios)
            _UsuariosExclusividad = Value
        End Set
    End Property

    Public Property Reporte() As Reporte
        Get
            If _Reporte Is Nothing Then
                _Reporte = New Reporte()
            End If
            Return _Reporte
        End Get
        Set(Value As Reporte)
            _Reporte = Value
        End Set
    End Property

    Public Property BasadoEnReporte() As Boolean
        Get
            BasadoEnReporte = _BasadoEnReporte
        End Get
        Set(Value As Boolean)
            _BasadoEnReporte = Value
        End Set
    End Property

    Public Property BasadoEnSaldos() As Boolean
        Get
            BasadoEnSaldos = _BasadoEnSaldos
        End Get
        Set(Value As Boolean)
            _BasadoEnSaldos = Value
        End Set
    End Property

    Public Property BasadoEnExtracto() As Boolean
        Get
            BasadoEnExtracto = _BasadoEnExtracto
        End Get
        Set(Value As Boolean)
            _BasadoEnExtracto = Value
        End Set
    End Property

    Public Property SoloEnGrupo() As Boolean
        Get
            SoloEnGrupo = _SoloEnGrupo
        End Get
        Set(Value As Boolean)
            _SoloEnGrupo = Value
        End Set
    End Property

    Public Property ConValores() As Boolean
        Get
            ConValores = _ConValores
        End Get
        Set(Value As Boolean)
            _ConValores = Value
        End Set
    End Property

    Public Property ConBultos() As Boolean
        Get
            ConBultos = _ConBultos
        End Get
        Set(Value As Boolean)
            _ConBultos = Value
        End Set
    End Property

    Public Property ConLector() As Boolean
        Get
            Return _ConLector
        End Get
        Set(value As Boolean)
            _ConLector = value
        End Set
    End Property

    Public Property CamposExtra() As CamposExtra
        Get
            If _CamposExtra Is Nothing Then
                _CamposExtra = New CamposExtra()
            End If
            Return _CamposExtra
        End Get
        Set(Value As CamposExtra)
            _CamposExtra = Value
        End Set
    End Property

    Public Property Motivo() As Motivo
        Get
            If _Motivo Is Nothing Then
                _Motivo = New Motivo()
            End If
            Return _Motivo
        End Get
        Set(Value As Motivo)
            _Motivo = Value
        End Set
    End Property

    Public Property Copias() As Copias
        Get
            If _Copias Is Nothing Then
                _Copias = New Copias
            End If
            Return _Copias
        End Get
        Set(Value As Copias)
            _Copias = Value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Descripcion = _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Id = _Id
        End Get
        Set(Value As Integer)
            _Id = Value
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

    Public Property DebeValidarNumExternoExistente() As Boolean
        Get
            Return _DebeValidarNumExternoExistente
        End Get
        Set(value As Boolean)
            _DebeValidarNumExternoExistente = value
        End Set
    End Property

    Public Property EsVisibleTransacionesv5() As Boolean
        Get
            Return _EsVisibleTransacionesv5
        End Get
        Set(value As Boolean)
            _EsVisibleTransacionesv5 = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar(Optional grado As Integer = 2)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reporte", ProsegurDbType.Inteiro_Longo, ReporteCondicion.RelatorioTransacoesV5))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")
            If grado > 0 Then
                If grado >= 2 Then
                    Me.Motivo.Id = dt.Rows(0)("IdMotivo")
                    Me.Motivo.Realizar()
                End If

                Me.SoloEnGrupo = dt.Rows(0)("SoloEnGrupo")
                Me.ConValores = dt.Rows(0)("ConValores")
                Me.ConBultos = dt.Rows(0)("ConBultos")
                Me.ConLector = dt.Rows(0)("ConLector")
                Me.BasadoEnReporte = dt.Rows(0)("BasadoEnReporte")
                If grado >= 2 Then
                    If dt.Rows(0)("IdReporte") Is DBNull.Value Then
                        Me.Reporte.Id = 0
                    Else
                        Me.Reporte.Id = dt.Rows(0)("IdReporte")
                    End If
                End If
                Me.BasadoEnSaldos = dt.Rows(0)("BasadoEnSaldos")
                Me.SeImprime = dt.Rows(0)("SeImprime")
                Me.Interplantas = dt.Rows(0)("Interplantas")
                Me.DistinguirPorNivel = dt.Rows(0)("DistinguirPorNivel")
                Me.Matrices = dt.Rows(0)("Matrices")
                Me.SoloIndividual = dt.Rows(0)("SoloIndividual")
                Me.EsActaProceso = dt.Rows(0)("EsActaProceso")
                Me.DebeValidarNumExternoExistente = dt.Rows(0)("BOL_VALIDAR_NUM_EXT_EXISTENTE")
                Me.EsVisibleTransacionesv5 = dt.Rows(0).IsNull("Reporte")

                If dt.Rows(0)("SoloSaldoDisponible") Is DBNull.Value Then
                    Me.SoloSaldoDisponible = False
                Else
                    Me.SoloSaldoDisponible = Convert.ToBoolean(dt.Rows(0)("SoloSaldoDisponible"))
                End If

                Me.Color = dt.Rows(0)("Color")

                If dt.Rows(0)("Sustituible") Is DBNull.Value Then
                    Me.Sustituible = False
                Else
                    Me.Sustituible = Convert.ToBoolean(dt.Rows(0)("Sustituible"))
                End If

                Me.BasadoEnExtracto = dt.Rows(0)("BasadoEnExtracto")

                Me.Campos.Formulario.Id = Me.Id
                Me.Campos.Realizar()

                If grado >= 2 Then
                    Me.UsuariosExclusividadRealizar()
                    Me.TiposCentroProcesoDestinoRealizar()

                    Me.Copias.Formulario.Id = Me.Id
                    Me.Copias.Realizar()

                    Me.CamposExtra.Formulario.Id = Me.Id
                    Me.CamposExtra.Realizar()
                End If

            End If

        End If

    End Sub

    Public Sub Registrar()

        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        If Me.Id = 0 Then
            Me.Id = ObterIdFormulario()
            RegistrarInsert(objTransacao)
        Else
            RegistrarUpdate(objTransacao)
        End If

        BorrarCondicionReporteTransicionV5(objTransacao)
        If Not Me.EsVisibleTransacionesv5 Then
            FormularioCondicionReporteTransicionV5Registrar(objTransacao)
        End If

        BorrarCampos(objTransacao)
        If Campos IsNot Nothing Then

            For Each c As Campo In Campos
                FormularioCampoRegistrar(c.Id, objTransacao)
            Next

        End If

        BorrarCopias(objTransacao)
        If Copias IsNot Nothing Then

            For Each c As Copia In Copias
                FormularioCopiaRegistrar(c.TipoCopia.Id, c.Destinatario, objTransacao)
            Next

        End If

        BorrarCamposExtra(objTransacao)
        If CamposExtra IsNot Nothing Then

            For Each ce As CampoExtra In CamposExtra
                If ce.Id = 0 Then
                    ce.Registrar()
                End If
                FormularioCampoExtraRegistrar(ce.Id, objTransacao)
            Next
        End If

        BorrarUsuariosExclusividad(objTransacao)
        If UsuariosExclusividad IsNot Nothing Then

            For Each u As Usuario In UsuariosExclusividad
                FormularioUsuarioRegistrar(u.Id, objTransacao)
            Next
        End If

        BorrarTiposCentroProcesoDestino(objTransacao)
        If TiposCentroProcesoDestino IsNot Nothing Then

            For Each tcp As TipoCentroProceso In TiposCentroProcesoDestino
                FormularioTipoCentroProcesoRegistrar(tcp.Id, objTransacao)
            Next
        End If

        objTransacao.RealizarTransacao()
    End Sub

    Private Sub RegistrarInsert(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioRegistrarInsert.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMotivo", ProsegurDbType.Inteiro_Longo, Me.Motivo.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SoloEnGrupo", ProsegurDbType.Logico, Me.SoloEnGrupo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConValores", ProsegurDbType.Logico, Me.ConValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConBultos", ProsegurDbType.Logico, Me.ConBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConLector", ProsegurDbType.Logico, Me.ConLector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "BasadoEnReporte", ProsegurDbType.Logico, Me.BasadoEnReporte))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Reporte.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "BasadoEnSaldos", ProsegurDbType.Logico, Me.BasadoEnSaldos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SeImprime", ProsegurDbType.Logico, Me.SeImprime))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Interplantas", ProsegurDbType.Logico, Me.Interplantas))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorNivel", ProsegurDbType.Logico, Me.DistinguirPorNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Matrices", ProsegurDbType.Logico, Me.Matrices))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SoloIndividual", ProsegurDbType.Logico, Me.SoloIndividual))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsActaProceso", ProsegurDbType.Logico, Me.EsActaProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SoloSaldoDisponible", ProsegurDbType.Logico, Me.SoloSaldoDisponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Color", ProsegurDbType.Descricao_Curta, Me.Color))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituible", ProsegurDbType.Logico, Me.Sustituible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "BasadoEnExtracto", ProsegurDbType.Logico, Me.BasadoEnExtracto))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub RegistrarUpdate(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioRegistrarUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMotivo", ProsegurDbType.Inteiro_Longo, Me.Motivo.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SoloEnGrupo", ProsegurDbType.Logico, Me.SoloEnGrupo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConValores", ProsegurDbType.Logico, Me.ConValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConBultos", ProsegurDbType.Logico, Me.ConBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConLector", ProsegurDbType.Logico, Me.ConLector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "BasadoEnReporte", ProsegurDbType.Logico, Me.BasadoEnReporte))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Reporte.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "BasadoEnSaldos", ProsegurDbType.Logico, Me.BasadoEnSaldos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SeImprime", ProsegurDbType.Logico, Me.SeImprime))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Interplantas", ProsegurDbType.Logico, Me.Interplantas))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorNivel", ProsegurDbType.Logico, Me.DistinguirPorNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Matrices", ProsegurDbType.Logico, Me.Matrices))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SoloIndividual", ProsegurDbType.Logico, Me.SoloIndividual))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsActaProceso", ProsegurDbType.Logico, Me.EsActaProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SoloSaldoDisponible", ProsegurDbType.Logico, Me.SoloSaldoDisponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Color", ProsegurDbType.Descricao_Curta, Me.Color))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituible", ProsegurDbType.Logico, Me.Sustituible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "BasadoEnExtracto", ProsegurDbType.Logico, Me.BasadoEnExtracto))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub FormularioCopiaRegistrar(IdTipoCopia As Integer, Destinatario As String, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioCopiaRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Destinatario", ProsegurDbType.Observacao_Curta, Destinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCopia", ProsegurDbType.Inteiro_Longo, IdTipoCopia))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub FormularioCampoRegistrar(IdCampo As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioCampoRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampo", ProsegurDbType.Inteiro_Longo, IdCampo))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub FormularioCampoExtraRegistrar(IdCampoExtra As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioCampoExtraRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Inteiro_Longo, IdCampoExtra))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub FormularioUsuarioRegistrar(IdUsuario As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioUsuarioRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, IdUsuario))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub FormularioTipoCentroProcesoRegistrar(IdTipoCentroProceso As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioTipoCentroProcesoRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCentroProceso", ProsegurDbType.Inteiro_Longo, IdTipoCentroProceso))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub FormularioCondicionReporteTransicionV5Registrar(ByRef objTransacao As Transacao)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioCondicionReporteTransicionV5Registrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reporte", ProsegurDbType.Inteiro_Longo, ReporteCondicion.RelatorioTransacoesV5))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarCampos(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioCampoBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarCamposExtra(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioCamposExtraBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarCopias(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioCopiaBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarCondicionReporteTransicionV5(ByRef objTransacao As Transacao)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioBorrarCondicionReporteTransicionV5.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reporte", ProsegurDbType.Inteiro_Longo, ReporteCondicion.RelatorioTransacoesV5))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub Eliminar()

        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        BorrarCondicionReporteTransicionV5(objTransacao)
        BorrarCampos(objTransacao)
        BorrarCamposExtra(objTransacao)
        BorrarCopias(objTransacao)
        BorrarTiposCentroProcesoDestino(objTransacao)
        BorrarUsuariosExclusividad(objTransacao)
        FormularioEliminar(objTransacao)


        objTransacao.RealizarTransacao()

    End Sub

    Private Sub FormularioEliminar(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioEliminar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarUsuariosExclusividad(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioUsuarioBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Function UsuariosExclusividadRealizar() As Short

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioUsuarioRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))

        Me.UsuariosExclusividad.Clear()

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        Dim objUsuario As Usuario = Nothing

        For Each dr As DataRow In dt.Rows

            objUsuario = New Usuario

            objUsuario.Id = dr("Id")
            objUsuario.Nombre = dr("Nombre")
            objUsuario.Clave = String.Empty
            objUsuario.ApellidoNombre = dr("ApellidoNombre")
            objUsuario.Bloqueado = dr("Bloqueado")

            Me.UsuariosExclusividad.Add(objUsuario)

        Next

    End Function

    Public Function TiposCentroProcesoDestinoRealizar() As Short

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioTipoCentroProcesoRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))

        Me.TiposCentroProcesoDestino.Clear()

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        Dim objTipoCentroProceso As TipoCentroProceso = Nothing

        For Each dr As DataRow In dt.Rows

            objTipoCentroProceso = New TipoCentroProceso

            objTipoCentroProceso.Id = dr("Id")
            objTipoCentroProceso.Descripcion = dr("Descripcion")
            objTipoCentroProceso.IdPS = dr("IdPS")

            Me.TiposCentroProcesoDestino.Add(objTipoCentroProceso)

        Next

    End Function

    Private Sub BorrarTiposCentroProcesoDestino(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.FormularioTipoCentroProcesoBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Function ObterIdFormulario() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SFormulario.ToString()
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

    End Function

#End Region

End Class