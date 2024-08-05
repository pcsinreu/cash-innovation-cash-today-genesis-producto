Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

''' <summary>
''' Classe TerminoMedioPago
''' </summary>
''' <remarks></remarks>
Public Class TerminoMedioPago

#Region "CONSULTA"

    ''' <summary>
    ''' Obtém o oid termino
    ''' </summary>
    ''' <param name="CodigoTerminoMedioPago"></param>
    ''' <param name="OidMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterOidTerminoMedioPago(CodigoTerminoMedioPago As String, OidMedioPago As String) As String

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidTerminoMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, CodigoTerminoMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, OidMedioPago))


        Dim OidRetorno As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            OidRetorno = dtQuery.Rows(0)("OID_TERMINO")
        End If

        Return OidRetorno

    End Function

    ''' <summary>
    ''' Busca Todos os Terminos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Public Shared Function BuscaTodosTerminos(OidMedioPago As String) As ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPagoColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTodosTerminos.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, OidMedioPago))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaTerminos As New ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPagoColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaTerminos.Add(PopularBuscaTerminos(dr))
            Next
        End If
        Return objRetornaTerminos
    End Function

    ''' <summary>
    ''' Popula um objeto termino através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    Private Shared Function PopularBuscaTerminos(dr As DataRow) As ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago

        ' criar objeto termino Iac
        Dim objTerminoMedioPago As New ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago

        'Termino
        Util.AtribuirValorObjeto(objTerminoMedioPago.Codigo, dr("COD_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Descripcion, dr("DES_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Observacion, dr("OBS_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.ValorInicial, dr("DES_VALOR_INICIAL"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Longitud, dr("NEC_LONGITUD"), GetType(Integer))
        Util.AtribuirValorObjeto(objTerminoMedioPago.MostrarCodigo, dr("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTerminoMedioPago.OrdenTermino, dr("NEC_ORDEN"), GetType(Integer))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        'Formato
        Util.AtribuirValorObjeto(objTerminoMedioPago.CodigoFormato, dr("COD_FORMATO"), GetType(String))

        'Algoritmo
        Util.AtribuirValorObjeto(objTerminoMedioPago.CodigoAlgoritmo, dr("COD_ALGORITMO_VALIDACION"), GetType(String))

        'Mascara
        Util.AtribuirValorObjeto(objTerminoMedioPago.CodigoMascara, dr("COD_MASCARA"), GetType(String))

        'Cria a coleção de termino para ser populada posteriormente
        objTerminoMedioPago.ValoresTermino = New ContractoServicio.MedioPago.SetMedioPago.ValorTerminoColeccion

        Return objTerminoMedioPago

    End Function

    ''' <summary>
    ''' Obtem os terminos através do oid medio pago.
    ''' </summary>
    ''' <param name="oidMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterTerminosPorMedioPado(oidMedioPago As String) As DataTable

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' montar query
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterTerminoPorOidMedioPago.ToString)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, oidMedioPago))

        'Executar query Terminos
        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function
    
    ''' <summary>
    ''' Busca o oid do Termino
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Function BuscaOidTermino(codigo As String, codMedioPago As String, codigoTipoMedioPago As String, CodigoIsoDivisa As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidTerminoMedioPago.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, codMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoIsoDivisa))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, codigoTipoMedioPago))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim oid As String = String.Empty

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            oid = dt.Rows(0)("OID_TERMINO").ToString
        End If

        Return oid

    End Function

#Region "[GETPROCESO]"


    Public Shared Function RetornarTerminosMedioPago(OidMedioPago As String, OidProcesoSubCanal As String) As GetProceso.TerminoMedioPagoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'BUSCA TERMINOS MEDIO PAGO
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaTerminoMedioPago.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, OidMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_SUBCANAL", ProsegurDbType.Objeto_Id, OidProcesoSubCanal))

        Dim dtTerminosMedioPago As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Verifica se o dtTerminosMedioPago retornou registro.
        If dtTerminosMedioPago IsNot Nothing AndAlso dtTerminosMedioPago.Rows.Count > 0 Then

            Return PopularTerminosMedioPago(dtTerminosMedioPago)

        Else

            Return Nothing

        End If

    End Function


    Private Shared Function PopularTerminosMedioPago(dt As DataTable) As GetProceso.TerminoMedioPagoColeccion

        Dim objTerminos As New GetProceso.TerminoMedioPagoColeccion

        Dim objTerminoMedioPago As GetProceso.TerminoMedioPago

        Dim oidTermino As String = String.Empty

        For Each dr In dt.Rows

            objTerminoMedioPago = New GetProceso.TerminoMedioPago()

            oidTermino = dr("OID_TERMINO")

            If dr("COD_TERMINO") IsNot DBNull.Value Then
                objTerminoMedioPago.Codigo = dr("COD_TERMINO")
            End If

            If dr("DES_TERMINO") IsNot DBNull.Value Then
                objTerminoMedioPago.Descripcion = dr("DES_TERMINO")
            End If

            If dr("OBS_TERMINO") IsNot DBNull.Value Then
                objTerminoMedioPago.Observaciones = dr("OBS_TERMINO")
            End If

            If dr("DES_VALOR_INICIAL") IsNot DBNull.Value Then
                objTerminoMedioPago.ValorInicial = dr("DES_VALOR_INICIAL")
            End If

            If dr("NEC_LONGITUD") IsNot DBNull.Value Then
                objTerminoMedioPago.Longitud = dr("NEC_LONGITUD")
            End If

            If dr("COD_FORMATO") IsNot DBNull.Value Then
                objTerminoMedioPago.CodigoFormato = dr("COD_FORMATO")
            End If

            If dr("DES_FORMATO") IsNot DBNull.Value Then
                objTerminoMedioPago.DescripcionFormato = dr("DES_FORMATO")
            End If

            If dr("COD_MASCARA") IsNot DBNull.Value Then
                objTerminoMedioPago.CodigoMascara = dr("COD_MASCARA")
            End If

            If dr("DES_MASCARA") IsNot DBNull.Value Then
                objTerminoMedioPago.DescripcionMascara = dr("DES_MASCARA")
            End If

            If dr("DES_EXP_REGULAR") IsNot DBNull.Value Then
                objTerminoMedioPago.ExpresionRegularMascara = dr("DES_EXP_REGULAR")
            End If

            If dr("COD_ALGORITMO_VALIDACION") IsNot DBNull.Value Then
                objTerminoMedioPago.CodigoAlgoritmo = dr("COD_ALGORITMO_VALIDACION")
            End If

            If dr("DES_ALGORITMO_VALIDACION") IsNot DBNull.Value Then
                objTerminoMedioPago.DescripcionAlgoritmo = dr("DES_ALGORITMO_VALIDACION")
            End If

            If dr("BOL_MOSTRAR_CODIGO") IsNot DBNull.Value Then
                objTerminoMedioPago.MostrarCodigo = dr("BOL_MOSTRAR_CODIGO")
            End If

            If dr("BOL_ES_OBLIGATORIO") IsNot DBNull.Value Then
                objTerminoMedioPago.EsObligatorio = dr("BOL_ES_OBLIGATORIO")
            End If

            If dr("NEC_ORDEN") IsNot DBNull.Value Then
                objTerminoMedioPago.Orden = dr("NEC_ORDEN")
            End If

            'Obtem os valores posiveis para o término de médio de pago
            objTerminoMedioPago.ValoresPosibles = AccesoDatos.ValorPosible.RetornarValoresPosiblesTerminoMedioPago(oidTermino)

            objTerminos.Add(objTerminoMedioPago)

        Next

        Return objTerminos

    End Function



#End Region

#Region "[GETPROCESODETAIL]"

    ''' <summary>
    ''' Busca os terminos medio pago.
    ''' </summary>
    ''' <param name="oidMedioPago"></param>
    ''' <param name="oidProcesoSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 - Criado
    ''' </history>
    Public Shared Function BuscaTerminoMedioPago(oidMedioPago As String, _
                                                 oidProcesoSubCanal As String) As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPagoColeccion
        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTerminoMedioPagoProceso.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, oidMedioPago))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, oidProcesoSubCanal))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaDivisas As New ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPagoColeccion

        'Percorre o dt e retorna uma coleção productos.
        objRetornaDivisas = RetornaColTermino(dt)

        Return objRetornaDivisas
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de terminos.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 - Criado
    ''' </history>
    Private Shared Function RetornaColTermino(dt As DataTable) As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPagoColeccion

        Dim objRetornaTermino As New ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPagoColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaTermino.Add(PopularTerminoMedioPago(dr))
            Next
        End If

        Return objRetornaTermino
    End Function

    ''' <summary>
    ''' Popula um objeto termino medio pago
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 Criado
    ''' </history>
    Private Shared Function PopularTerminoMedioPago(dr As DataRow) As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPago

        Dim objTermino As New ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPago

        Util.AtribuirValorObjeto(objTermino.Codigo, dr("COD_TERMINO"), GetType(String))

        Util.AtribuirValorObjeto(objTermino.Descripcion, dr("DES_TERMINO"), GetType(String))

        Util.AtribuirValorObjeto(objTermino.EsObligatorioTerminoMedioPago, dr("BOL_ES_OBLIGATORIO"), GetType(String))

        Return objTermino
    End Function

#End Region

#End Region

#Region "DELETE"

    ''' <summary>
    ''' Apagar todos os registros de termino para um Médio de Pago
    ''' </summary>
    ''' <param name="oidMedioPago"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Criado
    ''' </history>
    Public Shared Sub BajaTerminoMediosPagoPorMedioPago(oidMedioPago As String, _
                                                        CodUsuario As String, _
                                                        ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaTerminoMedioPagoPorMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros        
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, oidMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, False))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        ' adicionar query para transacao
        objTransacao.AdicionarItemTransacao(comando)

        'Adiciona o comando para exclusão dos valores de terminos vinculados ao medio de pago       
        ValorTerminoMedioPago.BajaValorTerminoMediosPagoPorMedioPago(oidMedioPago, CodUsuario, objTransacao)

    End Sub

    ''' <summary>
    ''' Apagar todos os registros de termino para um Médio de Pago
    ''' </summary>
    ''' <param name="objTerminoMedioPago"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Criado
    ''' </history>
    Public Shared Sub BajaTerminoMediosPago(objTerminoMedioPago As ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago, _
                                            CodUsuario As String, _
                                            oidMedioPago As String, _
                                            ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaTerminoMedioPago.ToString)
        comando.CommandType = CommandType.Text


        ' setar parametros        
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, oidMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, False))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, objTerminoMedioPago.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        ' adicionar query para transacao
        objTransacao.AdicionarItemTransacao(comando)


        '### SUBSTITUIDO PARA MAIOR PERFORMANCE ###
        'Dim oidTermino As String = ObterOidTermino(objTerminoMedioPago.Codigo, oidMedioPago)
        ''Apaga os valores de termino vinculados
        'If objTerminoMedioPago.ValoresTermino IsNot Nothing Then
        '    For Each objValorTermino In objTerminoMedioPago.ValoresTermino
        '        'Adiciona o comando para exclusão dos valores de terminos vinculados ao medio de pago       
        '        ValorTerminoMedioPago.BajaValorTerminoMediosPago(objValorTermino, CodUsuario, oidTermino, objTransacao)

        '    Next
        'End If
        '##########################################

        'Apaga todos os valores vincualdos ao medio de pago
        If objTerminoMedioPago.ValoresTermino IsNot Nothing AndAlso objTerminoMedioPago.ValoresTermino.Count > 0 Then
            ValorTerminoMedioPago.BajaValorTerminoMediosPagoPorMedioPago(oidMedioPago, CodUsuario, objTransacao)
        End If

    End Sub

    ''' <summary>
    ''' Exclui fisicamente o TerminoMedioPago
    ''' </summary>    
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Sub BajaTerminoMedioPagoPorProceso(codSubCanal As String, oidProcesoPorPServicioDesativado As String, _
                                                     ByRef objTransacion As Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajarTerminoMedioPagoPorProceso.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codSubCanal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidProcesoPorPServicioDesativado))


        '#If DEBUG Then
        '        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        '#Else
        objTransacion.AdicionarItemTransacao(comando)
        '#End If

    End Sub

    ''' <summary>
    ''' Exclui fisicamente o TerminoMedioPago
    ''' </summary>    
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Sub BajaFisicaTerminoMedioPago(oidProcesoSubCanal As String, ByRef objTransacion As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaFisicaTerminoMedioPagoPorProceso.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, oidProcesoSubCanal))


        '#If DEBUG Then
        'AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        '#Else
        objTransacion.AdicionarItemTransacao(comando)
        '#End If

    End Sub

#End Region

#Region "INSERIR"

    ''' <summary>
    ''' Responsável por inserir o Termino do Medio de Pago no DB.
    ''' </summary>
    ''' <param name="objTermino">Objeto Termino</param>
    ''' <param name="CodigoUsuario">Usuário responsável</param>
    ''' <param name="oidMedioPago">Oid do Medio de Pago a ser referenciado</param>
    ''' <param name="objtransacion">Transação corrente</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>    
    Public Shared Sub AltaTerminoMedioPago(objTermino As ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago, _
                                           CodigoUsuario As String, _
                                           oidMedioPago As String, _
                                           ByRef objtransacion As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        ' Obtêm o comando
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaTerminoMedioPago.ToString())
        comando.CommandType = CommandType.Text

        Dim strOidTermino As String = Guid.NewGuid.ToString

        'Termino
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, strOidTermino))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, oidMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, objTermino.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TERMINO", ProsegurDbType.Descricao_Longa, objTermino.Descripcion))

        If String.IsNullOrEmpty(objTermino.Observacion) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_TERMINO", ProsegurDbType.Observacao_Longa, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_TERMINO", ProsegurDbType.Observacao_Longa, objTermino.Observacion))
        End If

        If String.IsNullOrEmpty(objTermino.ValorInicial) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR_INICIAL", ProsegurDbType.Descricao_Longa, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR_INICIAL", ProsegurDbType.Descricao_Longa, objTermino.ValorInicial))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_LONGITUD", _
                                                          ProsegurDbType.Inteiro_Curto, _
                                                          IIf(objTermino.Longitud IsNot Nothing, objTermino.Longitud, DBNull.Value)))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_FORMATO", ProsegurDbType.Objeto_Id, Formato.ObterOidFormato(objTermino.CodigoFormato)))

        If String.IsNullOrEmpty(objTermino.CodigoMascara) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MASCARA", ProsegurDbType.Objeto_Id, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MASCARA", ProsegurDbType.Objeto_Id, Mascara.ObterOidMascara(objTermino.CodigoMascara)))
        End If

        If String.IsNullOrEmpty(objTermino.CodigoAlgoritmo) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_ALGORITMO_VALIDACION", ProsegurDbType.Objeto_Id, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_ALGORITMO_VALIDACION", ProsegurDbType.Objeto_Id, Algoritmo.ObterOidAlgoritmo(objTermino.CodigoAlgoritmo)))
        End If
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MOSTRAR_CODIGO", ProsegurDbType.Logico, objTermino.MostrarCodigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_ORDEN", ProsegurDbType.Inteiro_Curto, objTermino.OrdenTermino))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objTermino.Vigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objtransacion.AdicionarItemTransacao(comando)


        If objTermino.ValoresTermino IsNot Nothing Then
            For Each objValorTermino As ContractoServicio.MedioPago.SetMedioPago.ValorTermino In objTermino.ValoresTermino
                ValorTerminoMedioPago.AltaValorTerminoMedioPago(objValorTermino, CodigoUsuario, strOidTermino, objtransacion)
            Next
        End If

    End Sub

    ''' <summary>
    ''' Faz a alta de TerminoMedioPago por proceso
    ''' </summary>
    ''' <param name="codDelegacion"></param>
    ''' <param name="oidProcesoSubCanal"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objtransacion"></param>
    ''' <param name="oidTermino"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Public Shared Sub AltaTerminoMedioPagoPorProceso(codDelegacion As String, oidProcesoSubCanal As String, _
                                                     CodigoUsuario As String, ByRef objtransacion As Transacao, _
                                                     oidTermino As String, esObligatorioTerminoMedioPago As Boolean)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        ' Obtêm o comando
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaTerminoMedioPagoPorProceso.ToString())
        comando.CommandType = CommandType.Text

        Dim strOidTermino As String = Guid.NewGuid.ToString

        'Termino
        MontaParameter("OID_TERMINO_PROCESO", Guid.NewGuid.ToString, comando)
        MontaParameter("OID_TERMINO", oidTermino, comando)
        MontaParameter("COD_DELEGACION", codDelegacion, comando)
        MontaParameter("COD_USUARIO", CodigoUsuario, comando)
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
        MontaParameter("OID_PROCESO_SUBCANAL", oidProcesoSubCanal, comando)
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ES_OBLIGATORIO", ProsegurDbType.Logico, IIf(esObligatorioTerminoMedioPago, 1, 0)))

        '#If DEBUG Then
        '        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        '#Else
        objtransacion.AdicionarItemTransacao(comando)
        '#End If

    End Sub

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

#Region "UPDATE"

    ''' <summary>
    ''' Responsável por fazer a atualização do Medio de Pago no DB.
    ''' </summary>
    ''' <param name="objTerminoMedioPago"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Public Shared Sub ActualizarTerminoMedioPago(objTerminoMedioPago As ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago, _
                                                 codigoUsuario As String, _
                                                 oidMedioPago As String, _
                                                 ByRef objtransacion As Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        query.Append("UPDATE gepr_ttermino_medio_pago SET ")

        ' adicionar campos
        query.Append(Util.AdicionarCampoQuery("des_termino = []des_termino,", "des_termino", comando, objTerminoMedioPago.Descripcion, ProsegurDbType.Descricao_Longa))
        query.Append(Util.AdicionarCampoQuery("obs_termino = []obs_termino,", "obs_termino", comando, objTerminoMedioPago.Observacion, ProsegurDbType.Observacao_Longa))
        query.Append(Util.AdicionarCampoQuery("des_valor_inicial = []des_valor_inicial,", "des_valor_inicial", comando, objTerminoMedioPago.ValorInicial, ProsegurDbType.Descricao_Longa))

        query.Append(Util.AdicionarCampoQuery("nec_longitud = []nec_longitud,", "nec_longitud", comando, _
                                              IIf(objTerminoMedioPago.Longitud IsNot Nothing, objTerminoMedioPago.Longitud, DBNull.Value), _
                                              ProsegurDbType.Inteiro_Curto))

        query.Append(Util.AdicionarCampoQuery("oid_formato = []oid_formato,", "oid_formato", comando, Formato.ObterOidFormato(objTerminoMedioPago.CodigoFormato), ProsegurDbType.Objeto_Id))
        query.Append(Util.AdicionarCampoQuery("oid_mascara = []oid_mascara,", "oid_mascara", comando, Mascara.ObterOidMascara(objTerminoMedioPago.CodigoMascara), ProsegurDbType.Objeto_Id))
        query.Append(Util.AdicionarCampoQuery("oid_algoritmo_validacion = []oid_algoritmo_validacion,", "oid_algoritmo_validacion", comando, Algoritmo.ObterOidAlgoritmo(objTerminoMedioPago.CodigoAlgoritmo), ProsegurDbType.Objeto_Id))
        query.Append(Util.AdicionarCampoQuery("bol_mostrar_codigo = []bol_mostrar_codigo,", "bol_mostrar_codigo", comando, objTerminoMedioPago.MostrarCodigo, ProsegurDbType.Logico))
        query.Append(Util.AdicionarCampoQuery("nec_orden = []nec_orden,", "nec_orden", comando, objTerminoMedioPago.OrdenTermino, ProsegurDbType.Inteiro_Curto))
        query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objTerminoMedioPago.Vigente, ProsegurDbType.Logico))

        query.Append("cod_usuario = []cod_usuario, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

        query.Append("fyh_actualizacion = []fyh_actualizacion ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

        ' criar clausula where
        query.Append("WHERE cod_termino = []cod_termino and oid_medio_pago = []oid_medio_pago")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_termino", ProsegurDbType.Identificador_Alfanumerico, objTerminoMedioPago.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_medio_pago", ProsegurDbType.Objeto_Id, oidMedioPago))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        objtransacion.AdicionarItemTransacao(comando)

    End Sub

#End Region

End Class