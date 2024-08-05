Imports Prosegur.DbHelper
Imports System.Text

<Serializable()> _
Public Class Reporte

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _DistinguirPorVistaDestinatario As Boolean
    Private _EstadosComprobanteEmitido As EstadosComprobante
    Private _EstadosComprobanteRecibido As EstadosComprobante
    Private _VistaDestinatario As Boolean
    Private _FechaDesdeHoyDiasD As Integer
    Private _FechaHastaHoyDiasD As Integer
    Private _DistinguirPorFecha As Boolean
    Private _DistinguirPorDisponibilidad As Boolean
    Private _Disponible As Boolean
    Private _DistinguirPorReenvio As Boolean
    Private _Reenviado As Boolean
    Private _DistinguirPorBultos As Boolean
    Private _ConBultos As Boolean
    Private _DistinguirPorValores As Boolean
    Private _ConValores As Boolean
    Private _ConTomados As Boolean
    Private _DistinguirPorSustitucion As Boolean
    Private _Sustituido As Boolean
    Private _Descripcion As String
    Private _Documentos As Documentos
    Private _SeleccionDocumentos As SelDocumentos
    Private Realizado As Boolean
    Private _FormulariosRestriccion As Formularios
    Private _FechaDesde As Date
    Private _FechaHasta As Date
    Private _NumComprobanteDesde As String
    Private _NumComprobanteHasta As String
    Private _CentroProceso As CentroProceso
    Private _TipoCodigo As String
    Private _ListaCodigos As String
    Private _NumPagina As Integer
    Private _Paginado As Boolean
    Private _NumPaginas As Integer
    Private _NumDocsPorPagina As Integer
    Private _NumDocumentos As Integer
    Private _NumDocIni As Integer
    Private _NumDocFin As Integer
    Private _UsuarioActual As Usuario

#End Region

#Region "[PROPRIEDADES]"

    Public Property CentroProceso() As CentroProceso
        Get
            If _CentroProceso Is Nothing Then
                _CentroProceso = New CentroProceso()
            End If
            Return _CentroProceso
        End Get
        Set(Value As CentroProceso)
            _CentroProceso = Value
        End Set
    End Property

    Public Property TipoCodigo() As String
        Get
            Return _TipoCodigo
        End Get
        Set(Value As String)
            _TipoCodigo = Value
        End Set
    End Property

    Public Property ListaCodigos() As String
        Get
            Return _ListaCodigos
        End Get
        Set(Value As String)
            _ListaCodigos = Value
        End Set
    End Property

    Public Property NumComprobanteHasta() As String
        Get
            Return _NumComprobanteHasta
        End Get
        Set(Value As String)
            _NumComprobanteHasta = Value
        End Set
    End Property

    Public Property NumComprobanteDesde() As String
        Get
            Return _NumComprobanteDesde
        End Get
        Set(Value As String)
            _NumComprobanteDesde = Value
        End Set
    End Property

    Public Property FechaHasta() As Date
        Get
            Return _FechaHasta
        End Get
        Set(Value As Date)
            _FechaHasta = Value
        End Set
    End Property

    Public Property FechaDesde() As Date
        Get
            Return _FechaDesde
        End Get
        Set(Value As Date)
            _FechaDesde = Value
        End Set
    End Property

    Public Property UsuarioActual() As Usuario
        Get
            If _UsuarioActual Is Nothing Then
                _UsuarioActual = New Usuario()
            End If
            Return _UsuarioActual
        End Get
        Set(Value As Usuario)
            _UsuarioActual = Value
        End Set
    End Property

    Public Property NumDocFin() As Integer
        Get
            Return _NumDocFin
        End Get
        Set(Value As Integer)
            _NumDocFin = Value
        End Set
    End Property

    Public Property NumDocIni() As Integer
        Get
            Return _NumDocIni
        End Get
        Set(Value As Integer)
            _NumDocIni = Value
        End Set
    End Property

    Public Property NumDocumentos() As Integer
        Get
            Return _NumDocumentos
        End Get
        Set(Value As Integer)
            _NumDocumentos = Value
        End Set
    End Property

    Public Property NumDocsPorPagina() As Integer
        Get
            Return _NumDocsPorPagina
        End Get
        Set(Value As Integer)
            _NumDocsPorPagina = Value
        End Set
    End Property

    Public Property NumPaginas() As Integer
        Get
            Return _NumPaginas
        End Get
        Set(Value As Integer)
            _NumPaginas = Value
        End Set
    End Property

    Public Property Paginado() As Boolean
        Get
            Return _Paginado
        End Get
        Set(Value As Boolean)
            _Paginado = Value
        End Set
    End Property

    Public Property NumPagina() As Integer
        Get
            Return _NumPagina
        End Get
        Set(Value As Integer)
            _NumPagina = Value
        End Set
    End Property

    Public Property FormulariosRestriccion() As Formularios
        Get
            If _FormulariosRestriccion Is Nothing Then
                _FormulariosRestriccion = New Formularios()
            End If
            Return _FormulariosRestriccion
        End Get
        Set(Value As Formularios)
            _FormulariosRestriccion = Value
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

    Public Property SeleccionDocumentos() As SelDocumentos
        Get
            If _SeleccionDocumentos Is Nothing Then
                _SeleccionDocumentos = New SelDocumentos
            End If
            Return _SeleccionDocumentos
        End Get
        Set(Value As SelDocumentos)
            _SeleccionDocumentos = Value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
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

    Public Property DistinguirPorReenvio() As Boolean
        Get
            Return _DistinguirPorReenvio
        End Get
        Set(Value As Boolean)
            _DistinguirPorReenvio = Value
        End Set
    End Property

    Public Property ConBultos() As Boolean
        Get
            Return _ConBultos
        End Get
        Set(Value As Boolean)
            _ConBultos = Value
        End Set
    End Property

    Public Property DistinguirPorBultos() As Boolean
        Get
            Return _DistinguirPorBultos
        End Get
        Set(Value As Boolean)
            _DistinguirPorBultos = Value
        End Set
    End Property

    Public Property ConTomados() As Boolean
        Get
            Return _ConTomados
        End Get
        Set(Value As Boolean)
            _ConTomados = Value
        End Set
    End Property

    Public Property ConValores() As Boolean
        Get
            Return _ConValores
        End Get
        Set(Value As Boolean)
            _ConValores = Value
        End Set
    End Property

    Public Property DistinguirPorValores() As Boolean
        Get
            Return _DistinguirPorValores
        End Get
        Set(Value As Boolean)
            _DistinguirPorValores = Value
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

    Public Property DistinguirPorSustitucion() As Boolean
        Get
            Return _DistinguirPorSustitucion
        End Get
        Set(Value As Boolean)
            _DistinguirPorSustitucion = Value
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

    Public Property DistinguirPorDisponibilidad() As Boolean
        Get
            Return _DistinguirPorDisponibilidad
        End Get
        Set(Value As Boolean)
            _DistinguirPorDisponibilidad = Value
        End Set
    End Property

    Public Property DistinguirPorFecha() As Boolean
        Get
            Return _DistinguirPorFecha
        End Get
        Set(Value As Boolean)
            _DistinguirPorFecha = Value
        End Set
    End Property

    Public Property FechaHastaHoyDiasD() As Integer
        Get
            Return _FechaHastaHoyDiasD
        End Get
        Set(Value As Integer)
            _FechaHastaHoyDiasD = Value
        End Set
    End Property

    Public Property FechaDesdeHoyDiasD() As Integer
        Get
            Return _FechaDesdeHoyDiasD
        End Get
        Set(Value As Integer)
            _FechaDesdeHoyDiasD = Value
        End Set
    End Property

    Public Property VistaDestinatario() As Boolean
        Get
            Return _VistaDestinatario
        End Get
        Set(Value As Boolean)
            _VistaDestinatario = Value
        End Set
    End Property

    Public Property EstadosComprobanteRecibido() As EstadosComprobante
        Get
            If _EstadosComprobanteRecibido Is Nothing Then
                _EstadosComprobanteRecibido = New EstadosComprobante()
            End If
            Return _EstadosComprobanteRecibido
        End Get
        Set(Value As EstadosComprobante)
            _EstadosComprobanteRecibido = Value
        End Set
    End Property

    Public Property EstadosComprobanteEmitido() As EstadosComprobante
        Get
            If _EstadosComprobanteEmitido Is Nothing Then
                _EstadosComprobanteEmitido = New EstadosComprobante()
            End If
            Return _EstadosComprobanteEmitido
        End Get
        Set(Value As EstadosComprobante)
            _EstadosComprobanteEmitido = Value
        End Set
    End Property

    Public Property DistinguirPorVistaDestinatario() As Boolean
        Get
            Return _DistinguirPorVistaDestinatario
        End Get
        Set(Value As Boolean)
            _DistinguirPorVistaDestinatario = Value
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

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.DistinguirPorVistaDestinatario = dt.Rows(0)("DistinguirPorVistaDestinatario")
            Me.DistinguirPorFecha = dt.Rows(0)("DistinguirPorFecha")
            Me.FechaDesdeHoyDiasD = dt.Rows(0)("FechaDesdeHoyDiasD")
            Me.FechaHastaHoyDiasD = dt.Rows(0)("FechaHastaHoyDiasD")
            Me.DistinguirPorDisponibilidad = dt.Rows(0)("DistinguirPorDisponibilidad")
            Me.Disponible = dt.Rows(0)("Disponible")
            Me.DistinguirPorReenvio = dt.Rows(0)("DistinguirPorReenvio")
            Me.Reenviado = dt.Rows(0)("Reenviado")
            Me.VistaDestinatario = dt.Rows(0)("VistaDestinatario")
            Me.DistinguirPorBultos = dt.Rows(0)("DistinguirPorBultos")
            Me.ConBultos = dt.Rows(0)("ConBultos")
            Me.DistinguirPorValores = dt.Rows(0)("DistinguirPorValores")
            Me.ConValores = dt.Rows(0)("ConValores")
            Me.ConTomados = dt.Rows(0)("ConTomados")
            Me.DistinguirPorSustitucion = dt.Rows(0)("DistinguirPorSustitucion")
            Me.Sustituido = dt.Rows(0)("Sustituido")

            FormulariosRestriccionRealizar()
            EstadosComprobanteEmitidoRealizar()
            EstadosComprobanteRecibidoRealizar()

            EstablecerPropiedades()

        End If

    End Sub

    Protected Sub FormulariosRestriccionRealizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteFormularioRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objFormulario As Formulario = Nothing

            For Each dr As DataRow In dt.Rows

                objFormulario = New Formulario
                objFormulario.Id = dr("Id")
                objFormulario.Descripcion = dr("Descripcion")

                Me.FormulariosRestriccion.Add(objFormulario)

            Next

        End If

    End Sub

    Protected Function EstadosComprobanteEmitidoRealizar() As Short

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteEstadoComprobanteRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, False))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objEstadoComprobante As EstadoComprobante = Nothing

            For Each dr As DataRow In dt.Rows

                objEstadoComprobante = New EstadoComprobante
                objEstadoComprobante.Id = dr("IdEstadoComprobante")
                objEstadoComprobante.Descripcion = dr("Descripcion")
                objEstadoComprobante.Codigo = dr("Codigo")

                Me.EstadosComprobanteEmitido.Add(objEstadoComprobante)

            Next

        End If

    End Function

    Protected Function EstadosComprobanteRecibidoRealizar() As Short

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteEstadoComprobanteRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, True))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objEstadoComprobante As EstadoComprobante = Nothing

            For Each dr As DataRow In dt.Rows

                objEstadoComprobante = New EstadoComprobante
                objEstadoComprobante.Id = dr("IdEstadoComprobante")
                objEstadoComprobante.Descripcion = dr("Descripcion")
                objEstadoComprobante.Codigo = dr("Codigo")

                Me.EstadosComprobanteRecibido.Add(objEstadoComprobante)

            Next

        End If

    End Function

    Public Sub Registrar()

        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        If Me.Id = 0 Then
            Me.Id = ObterIdReporte()
            RegistrarInsert(objTransacao)
        Else
            RegistrarUpdate(objTransacao)
        End If


        ReporteFormularioBorrar(objTransacao)
        If Me.FormulariosRestriccion IsNot Nothing Then

            For Each objFormulario As Formulario In Me.FormulariosRestriccion
                FormulariosRestriccionRegistrar(objFormulario.Id, objTransacao)
            Next

        End If

        ReporteEstadoComprobanteBorrar(False, objTransacao)
        If Me.EstadosComprobanteEmitido IsNot Nothing Then

            For Each objEstadoEmitido As EstadoComprobante In Me.EstadosComprobanteEmitido
                EstadosComprobanteEmitidoRegistrar(objEstadoEmitido.Id, objTransacao)
            Next

        End If

        ReporteEstadoComprobanteBorrar(True, objTransacao)
        If Me.EstadosComprobanteRecibido IsNot Nothing Then

            For Each objEstadoRecibido As EstadoComprobante In Me.EstadosComprobanteRecibido
                EstadosComprobanteRecibidoRegistrar(objEstadoRecibido.Id, objTransacao)
            Next

        End If

        objTransacao.RealizarTransacao()

    End Sub

    Protected Sub RegistrarInsert(ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteRegistrarInsert.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorVistaDestinatario", ProsegurDbType.Logico, Me.DistinguirPorVistaDestinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, Me.VistaDestinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorFecha", ProsegurDbType.Logico, Me.DistinguirPorFecha))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesdeHoyDiasD", ProsegurDbType.Inteiro_Longo, Me.FechaDesdeHoyDiasD))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHastaHoyDiasD", ProsegurDbType.Inteiro_Longo, Me.FechaHastaHoyDiasD))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorDisponibilidad", ProsegurDbType.Logico, Me.DistinguirPorDisponibilidad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Disponible", ProsegurDbType.Logico, Me.Disponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorReenvio", ProsegurDbType.Logico, Me.DistinguirPorReenvio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reenviado", ProsegurDbType.Logico, Me.Reenviado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorBultos", ProsegurDbType.Logico, Me.DistinguirPorBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConBultos", ProsegurDbType.Logico, Me.ConBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorValores", ProsegurDbType.Logico, Me.DistinguirPorValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConValores", ProsegurDbType.Logico, Me.ConValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConTomados", ProsegurDbType.Logico, Me.ConTomados))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorSustitucion", ProsegurDbType.Logico, Me.DistinguirPorSustitucion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituido", ProsegurDbType.Logico, Me.Sustituido))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Protected Sub RegistrarUpdate(ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteRegistrarUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorVistaDestinatario", ProsegurDbType.Logico, Me.DistinguirPorVistaDestinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, Me.VistaDestinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorFecha", ProsegurDbType.Logico, Me.DistinguirPorFecha))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesdeHoyDiasD", ProsegurDbType.Inteiro_Longo, Me.FechaDesdeHoyDiasD))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHastaHoyDiasD", ProsegurDbType.Inteiro_Longo, Me.FechaHastaHoyDiasD))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorDisponibilidad", ProsegurDbType.Logico, Me.DistinguirPorDisponibilidad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Disponible", ProsegurDbType.Logico, Me.Disponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorReenvio", ProsegurDbType.Logico, Me.DistinguirPorReenvio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reenviado", ProsegurDbType.Logico, Me.Reenviado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorBultos", ProsegurDbType.Logico, Me.DistinguirPorBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConBultos", ProsegurDbType.Logico, Me.ConBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorValores", ProsegurDbType.Logico, Me.DistinguirPorValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConValores", ProsegurDbType.Logico, Me.ConValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConTomados", ProsegurDbType.Logico, Me.ConTomados))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorSustitucion", ProsegurDbType.Logico, Me.DistinguirPorSustitucion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituido", ProsegurDbType.Logico, Me.Sustituido))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Protected Sub FormulariosRestriccionRegistrar(IdFormulario As Integer, ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteFormularioRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, IdFormulario))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Protected Sub ReporteFormularioBorrar(ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteFormularioBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Protected Sub EstadosComprobanteEmitidoRegistrar(IdEstadoComprobante As Integer, ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteEstadoComprobanteRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoComprobante", ProsegurDbType.Inteiro_Longo, IdEstadoComprobante))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, False))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Protected Sub EstadosComprobanteRecibidoRegistrar(IdEstadoComprobante As Integer, ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteEstadoComprobanteRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoComprobante", ProsegurDbType.Inteiro_Longo, IdEstadoComprobante))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, True))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Protected Sub ReporteEstadoComprobanteBorrar(VistaDestinatario As Boolean, ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteEstadoComprobanteBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, VistaDestinatario))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub Eliminar()

        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        'Emitido
        ReporteEstadoComprobanteBorrar(False, objTransacao)

        'Recibido
        ReporteEstadoComprobanteBorrar(True, objTransacao)

        ReporteFormularioBorrar(objTransacao)
        ReporteEliminar(objTransacao)

        objTransacao.RealizarTransacao()

    End Sub

    Private Sub ReporteEliminar(ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReporteEliminar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdReporte", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub DocumentosRealizar(Optional ByRef Grado As Integer = 2)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        'variavies para concatenar sequencia de ids separados por '|'
        Dim IdsEstadosComprobanteEmitido, IdsEstadosComprobanteRecibido, IdsFormulariosRestriccion As New StringBuilder

        ' obter query
        comando.CommandText = My.Resources.ReporteDocumentosRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.CentroProceso.Id))

        If Me.EstadosComprobanteEmitido.Count > 0 Then IdsEstadosComprobanteEmitido.Append("|")
        For Each ECE In Me.EstadosComprobanteEmitido
            IdsEstadosComprobanteEmitido.Append(ECE.Id)
            IdsEstadosComprobanteEmitido.Append("|")
        Next

        If Me.EstadosComprobanteRecibido.Count > 0 Then IdsEstadosComprobanteRecibido.Append("|")
        For Each ECR In Me.EstadosComprobanteRecibido
            IdsEstadosComprobanteRecibido.Append(ECR.Id)
            IdsEstadosComprobanteRecibido.Append("|")
        Next

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsEstadosComprobanteEmitido", ProsegurDbType.Descricao_Longa, IdsEstadosComprobanteEmitido.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsEstadosComprobanteRecibido", ProsegurDbType.Descricao_Longa, IdsEstadosComprobanteRecibido.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorVistaDestinatario", ProsegurDbType.Logico, Me.DistinguirPorVistaDestinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, Me.VistaDestinatario))

        If Me.FechaDesde <> DateTime.MinValue Then

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data_Hora, Me.FechaDesde))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data_Hora, DBNull.Value))
        End If

        If Me.FechaHasta <> DateTime.MinValue Then

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data_Hora, Me.FechaHasta))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data_Hora, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteDesde", ProsegurDbType.Descricao_Curta, Me.NumComprobanteDesde))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteHasta", ProsegurDbType.Descricao_Curta, Me.NumComprobanteHasta))

        If Me.FormulariosRestriccion.Count Then IdsFormulariosRestriccion.Append("|")
        For Each FR In Me.FormulariosRestriccion
            IdsFormulariosRestriccion.Append(FR.Id)
            IdsFormulariosRestriccion.Append("|")
        Next

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsFormulariosRestriccion", ProsegurDbType.Descricao_Longa, IdsFormulariosRestriccion.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorDisponibilidad", ProsegurDbType.Logico, Me.DistinguirPorDisponibilidad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Disponible", ProsegurDbType.Logico, Me.Disponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorReenvio", ProsegurDbType.Logico, Me.DistinguirPorReenvio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reenviado", ProsegurDbType.Logico, Me.Reenviado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.UsuarioActual.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorBultos", ProsegurDbType.Logico, Me.DistinguirPorBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConBultos", ProsegurDbType.Logico, Me.ConBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorValores", ProsegurDbType.Logico, Me.DistinguirPorValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConValores", ProsegurDbType.Logico, Me.ConValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConTomados", ProsegurDbType.Logico, Me.ConTomados))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorSustitucion", ProsegurDbType.Logico, Me.DistinguirPorSustitucion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituido", ProsegurDbType.Logico, Me.Sustituido))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "TipoCodigo", ProsegurDbType.Descricao_Curta, Me.TipoCodigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ListaCodigos", ProsegurDbType.Descricao_Longa, Me.ListaCodigos))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objDocumento As Documento = Nothing

            For Each dr As DataRow In dt.Rows

                ' criar novo documento
                objDocumento = New Documento

                objDocumento.Id = dr("IdDocumento")

                If Grado >= 2 Then

                    If dr("IdGrupo") IsNot DBNull.Value Then
                        objDocumento.Grupo.Id = dr("IdGrupo")
                    End If

                    If dr("IdOrigen") IsNot DBNull.Value Then
                        objDocumento.Origen.Id = dr("IdOrigen")
                    End If

                End If

                objDocumento.Formulario.Id = dr("IdFormulario")
                objDocumento.Formulario.Realizar(1)

                For Each objCampo In objDocumento.Formulario.Campos
                    Select Case objCampo.Tipo
                        Case "I"
                            If dr("Id" & objCampo.Nombre) Is DBNull.Value Then
                                objCampo.IdValor = 0
                            Else
                                objCampo.IdValor = dr("Id" & objCampo.Nombre)
                            End If

                            If dr(objCampo.Nombre & "Desc") Is DBNull.Value Then
                                objCampo.Valor = String.Empty
                            Else
                                objCampo.Valor = dr(objCampo.Nombre & "Desc")
                            End If

                        Case "A"

                            If dr(objCampo.Nombre) Is DBNull.Value Then
                                objCampo.Valor = String.Empty
                            Else
                                objCampo.Valor = dr(objCampo.Nombre)
                            End If

                    End Select
                Next

                If Grado >= 2 Then

                    objDocumento.EstadoComprobante.Id = dr("IdEstadoComprobante")
                    objDocumento.Usuario.Id = dr("IdUsuario")
                    objDocumento.Usuario.Realizar()

                    If dr("IdUsuarioResuelve") IsNot DBNull.Value Then
                        objDocumento.UsuarioResolutor.Id = dr("IdUsuarioResuelve")
                        objDocumento.UsuarioResolutor.Realizar()
                    End If

                    If dr("IdUsuariodispone") IsNot DBNull.Value Then
                        objDocumento.UsuarioDispone.Id = dr("IdUsuariodispone")
                        objDocumento.UsuarioDispone.Realizar()
                    End If

                    If dr("FechaResuelve") IsNot DBNull.Value Then
                        objDocumento.FechaResolucion = dr("FechaResuelve")
                    End If

                    If dr("FechaGestion") IsNot DBNull.Value Then
                        objDocumento.FechaGestion = dr("FechaGestion")
                    End If

                    If dr("FechaDispone") IsNot DBNull.Value Then
                        objDocumento.FechaDispone = dr("Fechadispone")
                    End If

                End If

                If dr("NumComprobante") IsNot DBNull.Value Then
                    objDocumento.NumComprobante = dr("NumComprobante").ToString
                End If

                If Grado >= 1 Then

                    If objDocumento.Formulario.ConValores Then

                        If dr("IdDocDetalles") Is DBNull.Value OrElse dr("IdDocDetalles") = 0 Then
                            objDocumento.Detalles.Documento.Id = objDocumento.Id
                        Else
                            objDocumento.Detalles.Documento.Id = dr("IdDocDetalles")
                        End If

                        objDocumento.Detalles.Realizar()

                        Dim objTotal As Total = Nothing
                        Dim add As Boolean = False

                        For Each DocAgregadoTotal As Total In objDocumento.Detalles.Totales

                            Dim IdMoneda As Integer = DocAgregadoTotal.Moneda.Id
                            Dim resultTotal = From PesTotales In Me.Documentos.Detalles.Totales _
                                     Where PesTotales.Moneda.Id = IdMoneda

                            If resultTotal IsNot Nothing AndAlso resultTotal.Count > 0 Then
                                objTotal = resultTotal(0)
                            Else
                                add = True
                                objTotal = New Total
                                objTotal.Importe = 0
                                objTotal.Moneda = DocAgregadoTotal.Moneda
                                objTotal.HayUniformes = False
                                objTotal.HayNoUniformes = False
                            End If

                            objTotal.Importe = objTotal.Importe + DocAgregadoTotal.Importe
                            If DocAgregadoTotal.HayUniformes Then objTotal.HayUniformes = True
                            If DocAgregadoTotal.HayNoUniformes Then objTotal.HayNoUniformes = True

                            If add Then
                                Me.Documentos.Detalles.Totales.Add(objTotal)
                                add = False
                            End If

                        Next
                    End If

                    If objDocumento.Formulario.ConBultos Then

                        If dr("IdDocDetalles") Is DBNull.Value OrElse dr("IdDocDetalles") = 0 Then
                            objDocumento.Bultos.Documento.Id = objDocumento.Id
                        Else
                            objDocumento.Bultos.Documento.Id = dr("IdDocDetalles")
                        End If

                        objDocumento.Bultos.Realizar()

                    End If

                End If

                Me.Documentos.Add(objDocumento)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Busca os documentos que atendem aos filtros definido pelo Reporte, e o popula de forma simplificada por questoes de performance
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DocumentosRealizarSimplificado()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        'variavies para concatenar sequencia de ids separados por '|'
        Dim IdsEstadosComprobanteEmitido, IdsEstadosComprobanteRecibido, IdsFormulariosRestriccion As New StringBuilder

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.CentroProceso.Id))

        If Me.EstadosComprobanteEmitido.Count > 0 Then IdsEstadosComprobanteEmitido.Append("|")
        For Each ECE In Me.EstadosComprobanteEmitido
            IdsEstadosComprobanteEmitido.Append(ECE.Id)
            IdsEstadosComprobanteEmitido.Append("|")
        Next

        If Me.EstadosComprobanteRecibido.Count > 0 Then IdsEstadosComprobanteRecibido.Append("|")
        For Each ECR In Me.EstadosComprobanteRecibido
            IdsEstadosComprobanteRecibido.Append(ECR.Id)
            IdsEstadosComprobanteRecibido.Append("|")
        Next

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsEstadosComprobanteEmitido", ProsegurDbType.Descricao_Longa, IdsEstadosComprobanteEmitido.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsEstadosComprobanteRecibido", ProsegurDbType.Descricao_Longa, IdsEstadosComprobanteRecibido.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorVistaDestinatario", ProsegurDbType.Logico, Me.DistinguirPorVistaDestinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, Me.VistaDestinatario))

        Dim FiltroFechaDesde As String = String.Empty
        Dim FiltroFechaHasta As String = String.Empty
        If Me.FechaDesde <> DateTime.MinValue Then
            FiltroFechaDesde = "AND (dc.fecha >= :fechadesde) "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data_Hora, Me.FechaDesde))
        End If

        If Me.FechaHasta <> DateTime.MinValue Then
            FiltroFechaHasta = "AND (dc.fecha < :fechahasta) "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data_Hora, DateTime.Parse(Me.FechaHasta)))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteDesde", ProsegurDbType.Descricao_Curta, Me.NumComprobanteDesde))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "NumComprobanteHasta", ProsegurDbType.Descricao_Curta, Me.NumComprobanteHasta))

        If Me.FormulariosRestriccion.Count Then IdsFormulariosRestriccion.Append("|")
        For Each FR In Me.FormulariosRestriccion
            IdsFormulariosRestriccion.Append(FR.Id)
            IdsFormulariosRestriccion.Append("|")
        Next

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsFormulariosRestriccion", ProsegurDbType.Descricao_Longa, IdsFormulariosRestriccion.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorDisponibilidad", ProsegurDbType.Logico, Me.DistinguirPorDisponibilidad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Disponible", ProsegurDbType.Logico, Me.Disponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorReenvio", ProsegurDbType.Logico, Me.DistinguirPorReenvio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reenviado", ProsegurDbType.Logico, Me.Reenviado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.UsuarioActual.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorBultos", ProsegurDbType.Logico, Me.DistinguirPorBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConBultos", ProsegurDbType.Logico, Me.ConBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorValores", ProsegurDbType.Logico, Me.DistinguirPorValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConValores", ProsegurDbType.Logico, Me.ConValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConTomados", ProsegurDbType.Logico, Me.ConTomados))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorSustitucion", ProsegurDbType.Logico, Me.DistinguirPorSustitucion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituido", ProsegurDbType.Logico, Me.Sustituido))

        Dim FiltroTipoCodigo As String = String.Empty
        If Not String.IsNullOrEmpty(Me.TipoCodigo) Then

            Select Case Me.TipoCodigo

                Case "NC"
                    FiltroTipoCodigo = "AND (INSTR(:ListaCodigos, '|' || rtrim(total.numcomprobante) || '|') > 0) "
                Case "NE"
                    FiltroTipoCodigo = "AND (INSTR(:ListaCodigos, '|' || rtrim(total.numexterno) || '|') > 0) "
                Case "CB"
                    FiltroTipoCodigo = "AND (INSTR(:ListaCodigos, '|' || rtrim(total.codbolsa) || '|') > 0) "
                Case "NP"
                    FiltroTipoCodigo = "AND (INSTR(:ListaCodigos, '|' || rtrim(total.numprecinto) || '|') > 0) "

            End Select

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ListaCodigos", ProsegurDbType.Descricao_Longa, Me.ListaCodigos))

        End If

        comando.CommandText = String.Format(My.Resources.ReporteDocumentosRealizarSimplificado.ToString(), _
                                            FiltroFechaDesde, _
                                            FiltroFechaHasta, _
                                            FiltroTipoCodigo)
        
        comando.CommandType = CommandType.Text

        ' executar comando
        PopularColecao(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando))

    End Sub

    Public Sub DocumentosRealizarPorPrecinto()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        'variavies para concatenar sequencia de ids separados por '|'
        Dim IdsEstadosComprobanteEmitido, IdsEstadosComprobanteRecibido, IdsFormulariosRestriccion As New StringBuilder

        comando.CommandText = My.Resources.ReporteDocumentosRealizarPorPrecinto.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.CentroProceso.Id))

        If Me.EstadosComprobanteEmitido.Count > 0 Then IdsEstadosComprobanteEmitido.Append("|")
        For Each ECE In Me.EstadosComprobanteEmitido
            IdsEstadosComprobanteEmitido.Append(ECE.Id)
            IdsEstadosComprobanteEmitido.Append("|")
        Next

        If Me.EstadosComprobanteRecibido.Count > 0 Then IdsEstadosComprobanteRecibido.Append("|")
        For Each ECR In Me.EstadosComprobanteRecibido
            IdsEstadosComprobanteRecibido.Append(ECR.Id)
            IdsEstadosComprobanteRecibido.Append("|")
        Next

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsEstadosComprobanteEmitido", ProsegurDbType.Descricao_Longa, IdsEstadosComprobanteEmitido.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsEstadosComprobanteRecibido", ProsegurDbType.Descricao_Longa, IdsEstadosComprobanteRecibido.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorVistaDestinatario", ProsegurDbType.Logico, Me.DistinguirPorVistaDestinatario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaDestinatario", ProsegurDbType.Logico, Me.VistaDestinatario))

        If Me.FormulariosRestriccion.Count Then IdsFormulariosRestriccion.Append("|")
        For Each FR In Me.FormulariosRestriccion
            IdsFormulariosRestriccion.Append(FR.Id)
            IdsFormulariosRestriccion.Append("|")
        Next

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdsFormulariosRestriccion", ProsegurDbType.Descricao_Longa, IdsFormulariosRestriccion.ToString))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorDisponibilidad", ProsegurDbType.Logico, Me.DistinguirPorDisponibilidad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Disponible", ProsegurDbType.Logico, Me.Disponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorReenvio", ProsegurDbType.Logico, Me.DistinguirPorReenvio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reenviado", ProsegurDbType.Logico, Me.Reenviado))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.UsuarioActual.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorBultos", ProsegurDbType.Logico, Me.DistinguirPorBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConBultos", ProsegurDbType.Logico, Me.ConBultos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorValores", ProsegurDbType.Logico, Me.DistinguirPorValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConValores", ProsegurDbType.Logico, Me.ConValores))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConTomados", ProsegurDbType.Logico, Me.ConTomados))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorSustitucion", ProsegurDbType.Logico, Me.DistinguirPorSustitucion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Sustituido", ProsegurDbType.Logico, Me.Sustituido))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ListaCodigos", ProsegurDbType.Descricao_Longa, Me.ListaCodigos))

        ' executar comando
        PopularColecao(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando), False)

    End Sub

    Private Sub PopularColecao(dt As DataTable, Optional Simples As Boolean = True)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Documentos = New Negocio.Documentos
            Dim objDocumento As Documento = Nothing

            For Each dr As DataRow In dt.Rows

                ' criar novo documento
                objDocumento = New Documento

                objDocumento.Id = dr("IdDocumento")

                If dr("NumComprobante") IsNot DBNull.Value Then
                    objDocumento.NumComprobante = dr("NumComprobante").ToString
                End If

                objDocumento.Formulario.Id = dr("IdFormulario")
                objDocumento.Formulario.Descripcion = dr("Descripcion")
                objDocumento.Formulario.ConBultos = dr("ConBultos")
                objDocumento.Formulario.ConValores = dr("ConValores")

                If objDocumento.Formulario.ConValores Then

                    If dr("IdDocDetalles") Is DBNull.Value OrElse dr("IdDocDetalles") = 0 Then
                        objDocumento.Detalles.Documento.Id = objDocumento.Id
                    Else
                        objDocumento.Detalles.Documento.Id = dr("IdDocDetalles")
                    End If

                    ' Se nao for consulta simplificada preenche os detalles
                    If Not Simples Then
                        objDocumento.Detalles.RealizarSimplificado()
                    End If

                End If

                If objDocumento.Formulario.ConBultos Then

                    If dr("IdDocDetalles") Is DBNull.Value OrElse dr("IdDocDetalles") = 0 Then
                        objDocumento.Bultos.Documento.Id = objDocumento.Id
                    Else
                        objDocumento.Bultos.Documento.Id = dr("IdDocDetalles")
                    End If

                    ' Se nao for consulta simplificada preenche os bultos
                    If Not Simples Then
                        objDocumento.Bultos.Realizar(1)
                    End If

                End If

                objDocumento.Formulario.Campos.Formulario.Id = dr("IdFormulario")

                ' Se for consulta simplificada
                If Simples Then

                    objDocumento.Campos = New Hashtable

                    If (dr("idcentroprocesoorigen") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("IdCentroProcesoOrigen", dr("idcentroprocesoorigen"))
                    End If

                    If (dr("centroprocesoorigendesc") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("CentroProcesoOrigenDesc", dr("centroprocesoorigendesc"))
                    End If

                    If (dr("idcentroprocesodestino") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("IdCentroProcesoDestino", dr("idcentroprocesodestino"))
                    End If

                    If (dr("centroprocesodestinodesc") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("CentroProcesoDestinoDesc", dr("centroprocesodestinodesc"))
                    End If

                    If (dr("idclienteorigen") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("IdClienteOrigen", dr("idclienteorigen"))
                    End If

                    If (dr("clienteorigendesc") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("ClienteOrigenDesc", dr("clienteorigendesc"))
                    End If

                    If (dr("idclientedestino") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("IdClienteDestino", dr("idclientedestino"))
                    End If

                    If (dr("clientedestinodesc") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("ClienteDestinoDesc", dr("clientedestinodesc"))
                    End If

                    If (dr("idbanco") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("IdBanco", dr("idbanco"))
                    End If

                    If (dr("bancodesc") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("BancoDesc", dr("bancodesc"))
                    End If

                    If (dr("idbancodeposito") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("IdBancoDeposito", dr("idbancodeposito"))
                    End If

                    If (dr("bancodepositodesc") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("BancoDepositoDesc", dr("bancodepositodesc"))
                    End If

                    If (dr("numexterno") IsNot DBNull.Value) Then
                        objDocumento.Campos.Add("NumExterno", dr("numexterno"))
                    End If

                Else 'Se nao for consulta simplificada

                    objDocumento.Formulario.Campos.Realizar()

                    For Each objCampo In objDocumento.Formulario.Campos
                        Select Case objCampo.Tipo
                            Case "I"
                                If dr("Id" & objCampo.Nombre) Is DBNull.Value Then
                                    objCampo.IdValor = 0
                                Else
                                    objCampo.IdValor = dr("Id" & objCampo.Nombre)
                                End If

                                If dr(objCampo.Nombre & "Desc") Is DBNull.Value Then
                                    objCampo.Valor = String.Empty
                                Else
                                    objCampo.Valor = dr(objCampo.Nombre & "Desc")
                                End If

                            Case "A"

                                If dr(objCampo.Nombre) Is DBNull.Value Then
                                    objCampo.Valor = String.Empty
                                Else
                                    objCampo.Valor = dr(objCampo.Nombre)
                                End If

                        End Select

                    Next

                End If

                Me.Documentos.Add(objDocumento)

            Next

        End If

    End Sub

    Public Function SeleccionDocumentosRealizar(ByRef conexion As Object, Optional ByRef grado As Short = 2) As Short

    End Function

    Private Sub EstablecerPropiedades()

        If Me.DistinguirPorFecha Then
            Me.FechaDesde = System.DateTime.FromOADate(Today.ToOADate + Me.FechaDesdeHoyDiasD)
            Me.FechaHasta = System.DateTime.FromOADate(Today.ToOADate + Me.FechaHastaHoyDiasD + (#11:59:59 AM#).ToOADate)
        Else
            Me.FechaDesde = System.DateTime.FromOADate(0)
            Me.FechaHasta = System.DateTime.FromOADate(0)
        End If

    End Sub

    Private Function ObterIdReporte() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SReporte.ToString()
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

    End Function

#End Region

End Class