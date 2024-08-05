Imports Prosegur.DbHelper
Imports System.Collections.Generic
Imports Prosegur.Global.Saldos.Negocio.Enumeradores

<Serializable()> _
Public Class Formularios
    Inherits List(Of Formulario)

#Region "[VARIÁVEIS]"

    Private _CentroProceso As CentroProceso
    Private _UsuarioActual As Usuario

#End Region

#Region "[PROPRIEDADES]"

    Public Property UsuarioActual() As Usuario
        Get
            If _UsuarioActual Is Nothing Then
                _UsuarioActual = New Usuario()
            End If
            UsuarioActual = _UsuarioActual
        End Get
        Set(Value As Usuario)
            _UsuarioActual = Value
        End Set
    End Property

    Public Property CentroProceso() As CentroProceso
        Get
            If _CentroProceso Is Nothing Then
                _CentroProceso = New CentroProceso()
            End If
            CentroProceso = _CentroProceso
        End Get
        Set(Value As CentroProceso)
            _CentroProceso = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Popula a lista de formularios a partir do codigo do usuario.
    ''' </summary>
    ''' <param name="IdUsuario">Codigo do usuário.</param>
    ''' <remarks></remarks>
    Public Sub ReporteFormulariosUsuarioListar(IdUsuario As Integer)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.FormularioUsuarioListar.ToString()
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, IdUsuario))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objFormulario As Formulario = Nothing
            For Each dr As DataRow In dt.Rows
                objFormulario = New Formulario
                objFormulario.Id = dr("IdFormulario")
                objFormulario.Descripcion = dr("Descripcion")
                Me.Add(objFormulario)
            Next

        End If


    End Sub

    ''' <summary>
    ''' Popula a lista de formularios a partir da flag EsVisibleTransacionesv5.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReporteFormulariosVisibleTransacaoV5Listar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.FormulariosVisibleTransacaoV5Listar.ToString()
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Reporte", ProsegurDbType.Inteiro_Longo, ReporteCondicion.RelatorioTransacoesV5))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objFormulario As Formulario = Nothing
            For Each dr As DataRow In dt.Rows

                objFormulario = New Formulario
                objFormulario.Id = dr("IdFormulario")
                objFormulario.Descripcion = dr("Descripcion")
                Me.Add(objFormulario)

            Next

        End If


    End Sub

    Public Sub Realizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.FormulariosRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.UsuarioActual.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, ObterIdCentroProcessoMatriz()))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objFormulario As Formulario = Nothing

            For Each dr As DataRow In dt.Rows

                objFormulario = New Formulario
                objFormulario.Id = dr("Id")
                objFormulario.Descripcion = dr("Descripcion")
                objFormulario.SoloEnGrupo = dr("SoloEnGrupo")
                objFormulario.ConValores = dr("ConValores")
                objFormulario.ConBultos = dr("ConBultos")
                objFormulario.ConLector = dr("ConLector")
                objFormulario.BasadoEnReporte = dr("BasadoEnReporte")
                objFormulario.BasadoEnSaldos = dr("BasadoEnSaldos")
                objFormulario.DebeValidarNumExternoExistente = dr("BOL_VALIDAR_NUM_EXT_EXISTENTE")                

                If dr("SoloSaldoDisponible") Is DBNull.Value Then
                    objFormulario.SoloSaldoDisponible = False
                Else
                    objFormulario.SoloSaldoDisponible = Convert.ToBoolean(dr("SoloSaldoDisponible"))
                End If

                objFormulario.SeImprime = dr("SeImprime")
                objFormulario.Interplantas = dr("Interplantas")
                objFormulario.DistinguirPorNivel = dr("DistinguirPorNivel")
                objFormulario.Matrices = dr("Matrices")
                objFormulario.SoloIndividual = dr("SoloIndividual")
                objFormulario.EsActaProceso = dr("EsActaProceso")
                objFormulario.Color = dr("Color")
                objFormulario.BasadoEnExtracto = dr("BasadoEnExtracto")

                Me.Add(objFormulario)

            Next

        End If

    End Sub

    Public Sub RealizarTodos()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.FormularioRealizarTodos.ToString()
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objFormulario As Formulario = Nothing

            For Each dr As DataRow In dt.Rows

                objFormulario = New Formulario
                objFormulario.Id = dr("Id")
                objFormulario.Descripcion = dr("Descripcion")
                objFormulario.SoloEnGrupo = dr("SoloEnGrupo")
                objFormulario.ConValores = dr("ConValores")
                objFormulario.ConBultos = dr("ConBultos")
                objFormulario.ConLector = dr("ConLector")
                objFormulario.BasadoEnReporte = dr("BasadoEnReporte")
                objFormulario.BasadoEnSaldos = dr("BasadoEnSaldos")
                objFormulario.DebeValidarNumExternoExistente = dr("BOL_VALIDAR_NUM_EXT_EXISTENTE")

                If dr("SoloSaldoDisponible") Is DBNull.Value Then
                    objFormulario.SoloSaldoDisponible = False
                Else
                    objFormulario.SoloSaldoDisponible = Convert.ToBoolean(dr("SoloSaldoDisponible"))
                End If

                objFormulario.SeImprime = dr("SeImprime")
                objFormulario.Interplantas = dr("Interplantas")
                objFormulario.DistinguirPorNivel = dr("DistinguirPorNivel")
                objFormulario.Matrices = dr("Matrices")
                objFormulario.SoloIndividual = dr("SoloIndividual")
                objFormulario.EsActaProceso = dr("EsActaProceso")
                objFormulario.Color = dr("Color")
                objFormulario.BasadoEnExtracto = dr("BasadoEnExtracto")

                If dr("IdReporte") Is DBNull.Value Then
                    objFormulario.Reporte.Id = 0
                Else
                    objFormulario.Reporte.Id = dr("IdReporte")
                End If

                objFormulario.CamposExtra.Formulario.Id = objFormulario.Id
                objFormulario.CamposExtra.Realizar()

                objFormulario.Campos.Formulario.Id = objFormulario.Id
                objFormulario.Campos.Realizar()

                objFormulario.TiposCentroProcesoDestinoRealizar()

                Me.Add(objFormulario)

            Next

        End If

    End Sub

    Protected Function ObterIdCentroProcessoMatriz() As Integer

        ' setar retorno com id centro processo da classe
        Dim idCPMatriz As Integer = Me.CentroProceso.Id
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.FormularioRealizarCentroProcessoMatriz.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.CentroProceso.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If dt.Rows(0)("idcentroprocesomatriz") IsNot DBNull.Value Then
                idCPMatriz = CType(dt.Rows(0)("idcentroprocesomatriz"), Integer)
            End If
        End If

        Return idCPMatriz

    End Function

#End Region

End Class