Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Classe Proceso Por SubCanal
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 23/03/2009 - Criado
''' </history>
Public Class ProcesoPorSubCanal

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 Criado
    ''' </history>
    Public Sub New()

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Verifica se o subcanal esta com referencia a algum proceso.
    ''' </summary>    
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Function VerificaReferenciaProcesoSubCanal(codSubcanal As String, _
                                                             oidProceso As String) As String

        Dim oidProccesoSubcanal As String = String.Empty

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.VerificaReferenciaProceso.ToString()

        Dim filtros As New StringBuilder

        'Monta clausula where
        filtros.Append(MontaClausulaVerificaReferencia(codSubcanal, oidProceso, comando))

        comando.CommandText &= filtros.ToString

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Verifica se dt retornou informações
        If dt.Rows.Count > 0 Then
            oidProccesoSubcanal = dt.Rows(0)("OID_PROCESO_SUBCANAL")
        End If

        Return oidProccesoSubcanal

    End Function

    ''' <summary>
    ''' Monta Query VerificaReferenciaProceso
    ''' </summary>
    ''' <param name="objColSubCanal"></param>
    ''' <param name="oidProceso"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Private Shared Function MontaClausulaVerificaReferencia(objColSubCanal As String, _
                                                     oidProceso As String, ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New StringBuilder
        Dim objListSubCanal As New List(Of String)

        filtros.Append(" WHERE PRSUB.OID_PROCESO_POR_PSERVICIO = []OID_PROCESO_POR_PSERVICIO ")

        filtros.Append(" AND SUB.COD_SUBCANAL = []COD_SUBCANAL")

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, objColSubCanal))

        Return filtros

    End Function


#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Atualiza o proceso por punto servicio
    ''' </summary>    
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Sub ActualizaObservacionProcesoSubCanal(OidProcesoSubcanal As String, _
                                                         obsProceso As String, ByRef objTransacion As Transacao)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            comando.CommandText = My.Resources.ActualizaReferenciaProceso.ToString()
            comando.CommandType = CommandType.Text

            Dim filtros As New StringBuilder

            filtros.Append(",FYH_ACTUALIZACION = ")
            filtros.Append("    CASE WHEN OBS_PROCESO_SUBCANAL IS NULL AND LENGTH([]OBS_PROCESO_SUBCANAL) > 0 THEN ")
            filtros.Append("        []FYH_ACTUALIZACION ")
            filtros.Append("    WHEN OBS_PROCESO_SUBCANAL IS NOT NULL AND OBS_PROCESO_SUBCANAL <> []OBS_PROCESO_SUBCANAL THEN ")
            filtros.Append("        []FYH_ACTUALIZACION ")
            filtros.Append("    WHEN BOL_VIGENTE = 0 THEN ")
            filtros.Append("        []FYH_ACTUALIZACION ")
            filtros.Append("    ELSE ")
            filtros.Append("        FYH_ACTUALIZACION ")
            filtros.Append("    END ")
            filtros.Append(",OBS_PROCESO_SUBCANAL = []OBS_PROCESO_SUBCANAL")
            filtros.Append(" WHERE(OID_PROCESO_SUBCANAL = []OID_PROCESO_SUBCANAL)")

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_PROCESO_SUBCANAL", ProsegurDbType.Descricao_Longa, obsProceso))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, OidProcesoSubcanal))

            comando.CommandText &= filtros.ToString
            comando.CommandText = Util.PrepararQuery(comando.CommandText)


            '#If DEBUG Then
            'AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            '#Else
            objTransacion.AdicionarItemTransacao(comando)
            '#End If

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("016_msg_erro_execucao"))
        End Try

    End Sub

#End Region

#Region "[DELETE]"

    Public Shared Sub BajaProcesoSubCanal(codSubCanal As String, oidListProcesoPorPServicio As String, _
                                          ByRef objTransacion As Transacao, codUsuario As String)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaProcesoSubCanal.ToString())

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidListProcesoPorPServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codSubCanal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))


        '#If DEBUG Then
        '        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        '#Else
        objTransacion.AdicionarItemTransacao(comando)
        '#End If

    End Sub

    ''' <summary>
    ''' Faz a baja logica do proceso por subcnal
    ''' </summary>
    ''' <param name="oidProcesoPorPServicio"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 24/03/2009 - Criado
    ''' </history>
    Public Shared Sub BajaProcesoPorSubCanal(oidProcesoPorPServicio As String, _
                                      CodUsuario As String, ByRef objTransacion As Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaProcesoPorSubCanal.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidProcesoPorPServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objTransacion.AdicionarItemTransacao(comando)

    End Sub
#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Dá a alta de proceso por subcanal
    ''' </summary>        
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Function AltaProcesoSubCanal(oidProcesoPServicio As String, _
                                                obsProcesoSubCanal As String, _
                                                CodigoUsuario As String, _
                                                ByRef objtransacion As Transacao, _
                                                codSubCanal As String) As String

        Dim oidProcesoSubcanal As String = Guid.NewGuid.ToString

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.AltaProcesoSubCanal.ToString())
        comando.CommandType = CommandType.Text

        MontaParameter("OID_PROCESO_SUBCANAL", oidProcesoSubcanal, comando)
        MontaParameter("OID_PROCESO_POR_PSERVICIO", oidProcesoPServicio, comando)
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_PROCESO_SUBCANAL", ProsegurDbType.Observacao_Longa, obsProcesoSubCanal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codSubCanal))


        '#If DEBUG Then
        '        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        '#Else
        objtransacion.AdicionarItemTransacao(comando)
        '#End If

        Return oidProcesoSubcanal

    End Function

    ''' <summary>
    ''' Monda o parameter
    ''' </summary>
    ''' <param name="campo"></param>
    ''' <param name="objeto"></param>
    ''' <param name="comando"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Private Shared Sub MontaParameter(campo As String, objeto As String, ByRef comando As IDbCommand)

        If objeto IsNot Nothing AndAlso objeto <> String.Empty Then

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, objeto))

        Else

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))

        End If


    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' IActualiza o proceso
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Sub ActualizarProcessoSubCanal(ByRef oidProceso As String, codUsuario As String, _
                                                codCliente As String, codSubcanal As String, _
                                                ByRef objTransacion As Transacao, Optional objColSubCliente As ContractoServicio.Proceso.SetProceso.SubClienteColeccion = Nothing)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = My.Resources.Actualizar_ProcessoPorSubCanal.ToString()
            comando.CommandType = CommandType.Text

            Dim filtros As New StringBuilder
            filtros.Append(MontaQueryActualizarProcessoSubCanal(codCliente, codUsuario, oidProceso, comando, codSubcanal, objColSubCliente))

            comando.CommandText &= filtros.ToString

            comando.CommandText = Util.PrepararQuery(comando.CommandText)

            objTransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("016_msg_erro_execucao"))
        End Try

    End Sub

    Private Shared Function MontaQueryActualizarProcessoSubCanal(codCliente As String, codUsuario As String, _
                                                                 oidProceso As String, ByRef comando As IDbCommand, _
                                                                 codSubCanal As String, _
                                                                 Optional objColSubCliente As ContractoServicio.Proceso.SetProceso.SubClienteColeccion = Nothing) As StringBuilder

        Dim filtros As New StringBuilder

        filtros.Append(" WHERE CLI.COD_CLIENTE = []COD_CLIENTE AND PRPSRV.OID_PROCESO = []OID_PROCESO ")

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))


        If codSubCanal IsNot Nothing AndAlso codSubCanal <> String.Empty Then

            filtros.Append(" AND SUB.COD_SUBCANAL = []COD_SUBCANAL ")

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codSubCanal))

        End If

        If objColSubCliente IsNot Nothing AndAlso objColSubCliente.Count > 0 Then

            Dim codListSubClientes As New List(Of String)
            Dim codLisPServicio As New List(Of String)

            For Each objSubCliente As ContractoServicio.Proceso.SetProceso.SubCliente In objColSubCliente

                codListSubClientes.Add(objSubCliente.Codigo)

                If objSubCliente.PuntosServicio IsNot Nothing AndAlso objSubCliente.PuntosServicio.Count > 0 Then

                    For Each objPuntoServicio As ContractoServicio.Proceso.SetProceso.PuntoServicio In objSubCliente.PuntosServicio

                        codLisPServicio.Add(objPuntoServicio.Codigo)

                    Next

                End If

            Next

            If codLisPServicio Is Nothing OrElse codLisPServicio.Count = 0 Then
                filtros.Append(" AND PTO.COD_PTO_SERVICIO IS NULL ")
            Else
                filtros.Append(Util.MontarClausulaIn(codLisPServicio, "COD_PTO_SERVICIO", comando, "AND", "PTO"))
            End If

            filtros.Append(Util.MontarClausulaIn(codListSubClientes, "COD_SUBCLIENTE", comando, "AND", "SUBCLI"))

        Else

            filtros.Append(" AND SUBCLI.COD_SUBCLIENTE IS NULL ")
            filtros.Append(" AND PTO.COD_PTO_SERVICIO IS NULL ")

        End If

        filtros.Append(" ) PROCESO_SUBCANAL  SET PROCESO_SUBCANAL.BOL_VIGENTE = 0, ")
        filtros.Append("PROCESO_SUBCANAL.COD_USUARIO = []COD_USUARIO, ")
        filtros.Append("PROCESO_SUBCANAL.FYH_ACTUALIZACION = []FYH_ACTUALIZACION ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

        Return filtros
    End Function

#End Region
End Class
