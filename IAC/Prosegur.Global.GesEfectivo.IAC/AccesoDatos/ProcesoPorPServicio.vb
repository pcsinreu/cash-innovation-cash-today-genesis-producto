Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Classe Proceso Por Punto Servicio
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 20/03/2009 - Criado
''' </history>
Public Class ProcesoPorPServicio

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 Criado
    ''' </history>
    Public Sub New()

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    '''Busca Proceso Por Punto Servicio
    ''' </summary>        
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Function RetornaOidProcesoPorPServicioInativo(codCliente As String, _
                                                                codSubCliente As String, _
                                                                codPtoServicio As String, _
                                                                codSubcanal As String, _
                                                                codDelegacion As String, _
                                                                oidProcesoPorServicioModificado As String) As List(Of String)
        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.BuscaListaA.ToString()

        Dim filtros As New Text.StringBuilder

        'Monta Clausua 
        filtros.Append(MontaClausulaPuntoServicioProceso(codCliente, codSubCliente, codPtoServicio, codDelegacion, codSubcanal, oidProcesoPorServicioModificado, comando))

        comando.CommandText &= filtros.ToString

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim ListaOidProcesoInativos As New List(Of String)

        If dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                ListaOidProcesoInativos.Add(dr("OID_PROCESO_POR_PSERVICIO"))
            Next

            Return ListaOidProcesoInativos
        Else
            Return Nothing
        End If

    End Function

    'Private Shared Sub RetornaOidProcesoPServicio(dt As DataTable, ByRef oidListProceso As List(Of String), _
    '                                              ByRef oidListProcesoPservicio As List(Of String))

    '    Dim objRetornaDivisa As New ContractoServicio.Proceso.GetProcesoDetail.DivisaProcesoColeccion

    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

    '        For Each dr As DataRow In dt.Rows
    '            ' adicionar para objeto
    '            oidListProceso.Add(dr("OID_PROCESO"))
    '            oidListProcesoPservicio.Add(dr("OID_PROCESO_POR_PSERVICIO"))
    '        Next

    '    End If

    'End Sub

    ''' <summary>
    ''' Monta Query Punto Servico
    ''' </summary> 
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 Criado
    ''' </history>
    Private Shared Function MontaClausulaPuntoServicioProceso(codCliente As String, _
                                                              codSubcliente As String, _
                                                              codPtoServicio As String, _
                                                              codDelegacion As String, _
                                                              codSubcanal As String, _
                                                              oidProcesoPorPServicioModificado As String, ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New System.Text.StringBuilder

        'Monta a clausula e adiciona os parametros.
        filtros.Append(" where PRPSRV.oid_cliente = (select oid_cliente from GEPR_TCLIENTE where cod_cliente = []cod_cliente)  and ")

        filtros.Append(" PRPSRV.BOL_VIGENTE = 1 AND PRPSRV.COD_DELEGACION = []COD_DELEGACION AND ")

        If codSubcliente <> String.Empty Then
            filtros.Append(" PRPSRV.oid_subcliente = (select tsc.oid_subcliente from GEPR_TSUBCLIENTE tsc inner join GEPR_TCLIENTE tc on tsc.oid_cliente = tc.oid_cliente where tsc.cod_subcliente = []cod_subcliente and tc.cod_cliente = []cod_cliente)  and ")

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_subcliente", ProsegurDbType.Identificador_Alfanumerico, codSubcliente))

        Else
            filtros.Append(" PRPSRV.OID_SUBCLIENTE IS NULL AND ")
        End If

        If codPtoServicio <> String.Empty Then
            filtros.Append(" PRPSRV.oid_pto_servicio = ( Select oid_pto_servicio FROM  GEPR_TPUNTO_SERVICIO tps INNER JOIN GEPR_TSUBCLIENTE tsc on  tps.oid_subcliente = tsc.oid_subcliente INNER JOIN GEPR_TCLIENTE tc on  tsc.oid_cliente = tc.oid_cliente WHERE tsc.cod_subcliente = []cod_subcliente and tc.cod_cliente = []cod_cliente and cod_pto_servicio = []cod_pto_servicio ) and ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, codPtoServicio))

        Else
            filtros.Append(" PRPSRV.OID_PTO_SERVICIO IS NULL AND ")
        End If

        filtros.Append(" PRPSRV.oid_proceso_por_pservicio <> []oid_proceso_por_pservicio AND ")

        filtros.Append("  SUBC.OID_SUBCANAL = (SELECT OID_SUBCANAL FROM GEPR_TSUBCANAL WHERE COD_SUBCANAL = []COD_SUBCANAL) ")

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codSubcanal))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_proceso_por_pservicio", ProsegurDbType.Identificador_Alfanumerico, oidProcesoPorPServicioModificado))

        Return filtros

    End Function

    ''' <summary>
    ''' Retorna os codigos da coleção de punto se servicio.
    ''' </summary>
    ''' <param name="objColPuntoServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Private Shared Function RetornaCodigoPuntoServicio(objColPuntoServicio As ContractoServicio.Proceso.SetProceso.PuntoServicioColeccion) As List(Of String)

        Dim codPuntoServicio As New List(Of String)

        For Each objPuntoServicio As ContractoServicio.Proceso.SetProceso.PuntoServicio In objColPuntoServicio

            codPuntoServicio.Add(objPuntoServicio.Codigo)

        Next

        Return codPuntoServicio
    End Function

    ''' <summary>
    '''Busca Proceso Por Punto Servicio
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Function BuscaOidProcesoPServicio(oidProceso As String, codCliente As String, _
                                                    codSubCliente As String, codPtoServicio As String) As String

        Dim oidProcesoPServicio As String = String.Empty
        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidProcesoPServicio.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))

        If Not String.IsNullOrEmpty(codSubCliente) Then
            query.Append(" AND SUBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, codSubCliente))
        Else
            query.Append(" AND SUBCLI.COD_SUBCLIENTE IS NULL ")
        End If

        If Not String.IsNullOrEmpty(codPtoServicio) Then
            query.Append(" AND PTOSERV.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, codPtoServicio))
        Else
            query.Append(" AND PTOSERV.COD_PTO_SERVICIO IS NULL ")
        End If

        comando.CommandText &= query.ToString

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Retorna os oids proceso e procesoPuntoServicio.
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            oidProcesoPServicio = dt.Rows(0)("OID_PROCESO_POR_PSERVICIO")
        End If

        Return oidProcesoPServicio
    End Function

    ''' <summary>
    ''' Função retorna LISTB (oids proceso punto servicio)
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 25/03/2009 - Criado
    ''' </history>
    Public Shared Function BuscaOidPtoServicio(oidProceso As String) As List(Of String)

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidTProcesoPServicio.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Retorna os oids proceso e procesoPuntoServicio.
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return PopulaOidProcessoPServicio(dt)
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Popula lista de string
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 25/03/2009 - Criado
    ''' </history>
    Private Shared Function PopulaOidProcessoPServicio(dt As DataTable) As List(Of String)

        Dim oidListProcesoPServicio As New List(Of String)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                oidListProcesoPServicio.Add(dr("OID_PROCESO_POR_PSERVICIO"))
            Next

        End If

        Return oidListProcesoPServicio
    End Function

    ''' <summary>
    ''' Preencher Proceso por Punto Servicio - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Proceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 02/04/2009 Criado
    ''' </history>
    Public Shared Function PopularProcesoPuntoServicio(Oid_Proceso As String) As GetProcesos.ProcesoPuntoServicioColeccion

        'Cria objetos 
        Dim objIac As GetProcesos.Iac = Nothing
        Dim objProcesoPS As GetProcesos.ProcesoPuntoServicio = Nothing
        Dim objProcesoPSColeccion As GetProcesos.ProcesoPuntoServicioColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaProcesoPuntoServicioPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim ProcesoPS As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If ProcesoPS IsNot Nothing AndAlso ProcesoPS.Rows.Count > 0 Then

            'Instancia coleção de proceso punto servicio
            objProcesoPSColeccion = New GetProcesos.ProcesoPuntoServicioColeccion

            For Each row As DataRow In ProcesoPS.Rows

                'Instancia objetos
                objProcesoPS = New GetProcesos.ProcesoPuntoServicio
                objIac = New GetProcesos.Iac

                With objProcesoPS

                    'Preenche propriedades do objeto Proceso
                    Util.AtribuirValorObjeto(.Cliente, row("COD_CLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(.SubCliente, row("COD_SUBCLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(.PuntoServicio, row("COD_PTO_SERVICIO"), GetType(String))
                    Util.AtribuirValorObjeto(.Delegacion, row("COD_DELEGACION"), GetType(String))
                    Util.AtribuirValorObjeto(.ClienteFacturacion, row("COD_CLIENTE_FACTURACION"), GetType(String))

                    With objIac

                        'Preenche propriedades do objeto Iac
                        Util.AtribuirValorObjeto(.Codigo, row("COD_IAC"), GetType(String))
                        Util.AtribuirValorObjeto(.Descripcion, row("DES_IAC"), GetType(String))
                        Util.AtribuirValorObjeto(.Observacion, row("OBS_IAC"), GetType(String))
                        Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                        'Se houver IAC, preenche seus términos
                        If objIac.Codigo IsNot Nothing Then

                            'Preenche coleção de terminos por Iac
                            .Terminos = AccesoDatos.TerminoIac.PopularTerminoIac(row("OID_IAC").ToString(), row("OID_CLIENTE").ToString())

                        End If

                    End With

                    'Adiciona objeto Iac preenchido à propriedade
                    .IAC = objIac

                    'Preenche coleçõe Subcanales e MedioPago
                    .Subcanales = AccesoDatos.SubCanal.PopularSubCanal(row("OID_PROCESO_POR_PSERVICIO").ToString())

                End With

                'Adiciona objeto à coleção
                objProcesoPSColeccion.Add(objProcesoPS)

            Next

        End If

        Return objProcesoPSColeccion

    End Function

    ''' <summary>
    ''' Preencher MedioPago por Proceso Servicio - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Proceso_Servicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 02/04/2009 Criado
    ''' </history>
    Public Shared Function PopularMedioPagoProcesoServicio(Oid_Proceso_Servicio As String, Oid_Subcanal As String) As GetProcesos.MedioPagoProcesoColeccion

        'Cria objetos         
        Dim objMedPagProceso As GetProcesos.MedioPagoProceso = Nothing
        Dim objMedPagProcesoCol As GetProcesos.MedioPagoProcesoColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaMedioPagoProcesoServicio.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Objeto_Id, Oid_Proceso_Servicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, Oid_Subcanal))

        'Preenche DataTable com o resultado da consulta
        Dim MedPagProceso As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If MedPagProceso IsNot Nothing AndAlso MedPagProceso.Rows.Count > 0 Then

            'Instancia objeto MedioPagoProcesoColeccion
            objMedPagProcesoCol = New GetProcesos.MedioPagoProcesoColeccion

            'Cria variável para comparação de CodMedioPago
            Dim CodMedPag As String = String.Empty

            'Cria objeto do tipo TerminoIac
            Dim objTerminoIac As GetProcesos.TerminoIac = Nothing

            'Para cada registro do DataTable
            For Each row As DataRow In MedPagProceso.Rows

                If CodMedPag <> row("COD_MEDIO_PAGO").ToString() Then

                    'Instancia objeto MedioPagoProceso
                    objMedPagProceso = New GetProcesos.MedioPagoProceso

                    'Preenche propriedades MedioPagoProceso
                    With objMedPagProceso

                        Util.AtribuirValorObjeto(.Codigo, row("COD_MEDIO_PAGO"), GetType(String))

                        'Cria nova instância para TerminoIacColeccion
                        .Terminos = New GetProcesos.TerminoIacColeccion

                    End With

                    'Adciona objeto MedioPagoProceso à coleção de MedioPagoProceso
                    objMedPagProcesoCol.Add(objMedPagProceso)

                End If

                'Instancia objeto TerminoIac
                objTerminoIac = New GetProcesos.TerminoIac

                With objTerminoIac

                    Util.AtribuirValorObjeto(.Codigo, row("COD_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(.Observacion, row("OBS_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(.ValorInicial, row("DES_VALOR_INICIAL"), GetType(String))
                    Util.AtribuirValorObjeto(.Longitud, row("NEC_LONGITUD"), GetType(Integer))
                    Util.AtribuirValorObjeto(.CodigoFormato, row("COD_FORMATO"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionFormato, row("DES_FORMATO"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoMascara, row("COD_MASCARA"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionMascara, row("DES_MASCARA"), GetType(String))
                    Util.AtribuirValorObjeto(.ExpRegularMascaraTerminoIAC, row("DES_EXP_REGULAR"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoAlgValidacion, row("COD_ALGORITMO_VALIDACION"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionAlgValidacion, row("DES_ALGORITMO_VALIDACION"), GetType(String))
                    Util.AtribuirValorObjeto(.MostrarCodigo, row("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.EsObligatorioTermino, row("BOL_ES_OBLIGATORIO"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.esProtegidoTermino, row("BOL_ES_PROTEGIDO"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.Orden, row("NEC_ORDEN"), GetType(Integer))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                    'Preenche coleção de valores porssíveis
                    .ValoresPosibles = AccesoDatos.ValorPosible.PopularValorPosible(row("OID_TERMINO").ToString())

                End With

                objMedPagProceso.Terminos.Add(objTerminoIac)
                CodMedPag = row("COD_MEDIO_PAGO").ToString()

            Next

        End If

        Return objMedPagProcesoCol

    End Function

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Atualiza o proceso por punto servicio
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Public Shared Sub ActualizarProcesoPorPServicio(oidProceso As String, _
                                                    objProceso As ContractoServicio.Proceso.SetProceso.Proceso, _
                                                    ByRef objTransacion As Transacao)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizaProcesoPorPServicio.ToString())
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

            If objProceso.CodigoIac IsNot Nothing AndAlso objProceso.CodigoIac <> String.Empty Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoIac))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IAC", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))
            End If
            If objProceso.CodigoClienteFacturacion IsNot Nothing AndAlso objProceso.CodigoClienteFacturacion <> String.Empty Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoClienteFacturacion))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))
            End If

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

#Region "[INSERT]"

    ''' <summary>
    ''' Insere um novo processo por punto servicio
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Public Shared Function AltaProcesoPorPServicio(oidProceso As String, _
                                              oidCliente As String, _
                                              oidSubCliente As String, _
                                              oidPtoServicio As String, _
                                              oidIac As String, _
                                              oidIacBulto As String, _
                                              oidIacRemesa As String, _
                                              oidClienteFac As String, _
                                              codDelegacion As String, _
                                              codUsuario As String, _
                                              vigente As Boolean, ByRef objTransacion As Transacao) As String


        Dim oidProcesoPServicio As String = String.Empty

        Try

            oidProcesoPServicio = Guid.NewGuid.ToString

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaProcesoPorPServicio.ToString())
            comando.CommandType = CommandType.Text

            MontaParameter("OID_PROCESO_POR_PSERVICIO", oidProcesoPServicio, comando)
            MontaParameter("OID_PROCESO", oidProceso, comando)
            MontaParameter("OID_CLIENTE", oidCliente, comando)
            MontaParameter("OID_SUBCLIENTE", oidSubCliente, comando)
            MontaParameter("OID_PTO_SERVICIO", oidPtoServicio, comando)
            MontaParameter("COD_DELEGACION", codDelegacion, comando)
            MontaParameter("OID_CLIENTE_FACTURACION", oidClienteFac, comando)
            MontaParameter("OID_IAC", oidIac, comando)
            MontaParameter("OID_IAC_BULTO", oidIacBulto, comando)
            MontaParameter("OID_IAC_REMESA", oidIacRemesa, comando)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, vigente))
            MontaParameter("COD_USUARIO", codUsuario, comando)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

            '#If DEBUG Then
            '            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            '#Else
            objTransacion.AdicionarItemTransacao(comando)
            '#End If




        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("016_msg_erro_execucao"))
        End Try

        Return oidProcesoPServicio

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

#Region "[DELETE]"
    ''' <summary>
    ''' Verifica na tabela por ProcesoPorPservicio se existe proceso vigente para o OID informado
    ''' </summary>
    ''' <param name="oidProcesoPorPServicio"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Sub BajaProcesoPservicioPorSubCanalVigente(oidProcesoPorPServicio As String, _
                                                             ByRef objTransacion As Transacao, codUsuario As String)

        Dim Resultado As Integer = 0

        Using objCmdSel As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            objCmdSel.CommandText = Util.PrepararQuery(My.Resources.BuscaProcesoSubCanalPorProcesoPServicio)
            objCmdSel.CommandType = CommandType.Text
            objCmdSel.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidProcesoPorPServicio))

            Resultado = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, objCmdSel)

            If Resultado = 0 Then

                Using objCmdUpd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

                    objCmdUpd.CommandText = Util.PrepararQuery(My.Resources.BajaProcesoPservicioPorSubCanalVigente)
                    objCmdUpd.CommandType = CommandType.Text

                    objCmdUpd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidProcesoPorPServicio))
                    objCmdUpd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))
                    objCmdUpd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

                    objTransacion.AdicionarItemTransacao(objCmdUpd)

                End Using

            End If

        End Using

    End Sub

    ''' <summary>
    ''' Atualiza o proceso por punto servicio
    ''' </summary>
    ''' <param name="oidProcesoPServicio"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Public Shared Sub BajaProcesoPorPuntoServicio(oidProcesoPServicio As String, codUsuario As String, ByRef objTransacion As Transacao)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarProcesoPorPuntoServicio.ToString())
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidProcesoPServicio))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))

            '#If DEBUG Then
            '            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            '#Else
            objTransacion.AdicionarItemTransacao(comando)
            '#End If

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("016_msg_erro_execucao"))
        End Try

    End Sub

#End Region

End Class
