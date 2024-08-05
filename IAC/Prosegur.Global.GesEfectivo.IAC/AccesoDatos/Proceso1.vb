Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario

''' <summary>
''' Classe Proceso
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 11/03/2009 Criado
''' </history>
Public Class Proceso1

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Public Sub New()

    End Sub

#End Region

#Region "[VARIAVEIS]"

    Private _oidProceso As String
    Private _oidProducto As String
    Private _oidIac As String
    Private _oidCliente As String
    Private _oidSubCanal As String
    Private _bolAdmiteIac As Boolean
    Private _bolMediosPago As Boolean

#End Region

#Region "[PROPRIEDADES]"

    Public ReadOnly Property OidProceso() As String
        Get
            Return _oidProceso
        End Get
    End Property

    Public ReadOnly Property OidProducto() As String
        Get
            Return _oidProducto
        End Get
    End Property

    Public ReadOnly Property OidIAC() As String
        Get
            Return _oidIac
        End Get
    End Property

    Public ReadOnly Property OidCliente() As String
        Get
            Return _oidCliente
        End Get
    End Property

    Public ReadOnly Property OidSubCanal() As String
        Get
            Return _oidSubCanal
        End Get
    End Property

    Public ReadOnly Property BolAdmiteIac() As Boolean
        Get
            Return _bolAdmiteIac
        End Get
    End Property

    Public ReadOnly Property BolMediosPago() As Boolean
        Get
            Return _bolMediosPago
        End Get
    End Property

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProcesoIntegracion(ByVal peticion As GetProceso.Peticion) As GetProceso.Proceso

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objProceso As New GetProceso.Proceso

        comando.CommandText = Util.PrepararQuery(My.Resources.GetProceso.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoCliente))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubcliente))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubcanal))

        Dim dtProceso As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtProceso IsNot Nothing AndAlso dtProceso.Rows.Count > 0 Then

            With dtProceso.Rows(0)

                'Preenche propriedades
                Util.AtribuirValorObjeto(_oidProceso, .Item("OID_PROCESO").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidProducto, .Item("OID_PRODUCTO").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidIac, .Item("OID_IAC").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidCliente, .Item("OID_CLIENTE").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_bolAdmiteIac, .Item("BOL_ADMITE_IAC"), GetType(Boolean))
                Util.AtribuirValorObjeto(_bolMediosPago, .Item("BOL_MEDIOS_PAGO"), GetType(Boolean))
                Util.AtribuirValorObjeto(_oidSubCanal, .Item("OID_SUBCANAL").ToString(), GetType(String))

            End With

            'POPULA PROCESO 
            Return PopularProceso(dtProceso.Rows(0))

        Else

            Return Nothing

        End If



    End Function

    ''' <summary>
    ''' Popula o objproceso
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Function PopularProceso(ByVal dr As DataRow) As GetProceso.Proceso

        Dim objproceso As New GetProceso.Proceso

        Util.AtribuirValorObjeto(objproceso.Cliente, dr("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.SubCliente, dr("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.PuntoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.SubCanal, dr("COD_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.Delegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.DescripcionProceso, dr("DES_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.ObservacionesProceso, dr("OBS_PROCESO_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.ClienteFacturacion, dr("COD_CLIENTE_FACTURACION"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.ContarChequesTotal, dr("BOL_CONTAR_CHEQUES_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproceso.ContarTicketsTotal, dr("BOL_CONTAR_TICKETS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproceso.ContarOtrosTotal, dr("BOL_CONTAR_OTROS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproceso.VigenteProceso, dr("BOL_VIGENTE"), GetType(Boolean))

        If dr("COD_PRODUCTO") IsNot DBNull.Value Then
            'Preenche os produtos do proceso
            objproceso.Producto = PopularProductoProceso(dr)
        End If

        If dr("COD_TIPO_PROCESADO") IsNot DBNull.Value Then
            'Preenche as modalidades de recuento do proceso
            objproceso.ModalidadRecuento = PopularModalidadRecuento(dr)
        End If

        Return objproceso

    End Function

    ''' <summary>
    ''' Popula o objProducto
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Function PopularProductoProceso(ByVal dr As DataRow) As GetProceso.Producto

        Dim objProducto As New GetProceso.Producto

        Util.AtribuirValorObjeto(objProducto.Codigo, dr("COD_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objProducto.Descripcion, dr("DES_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objProducto.ClaseBillete, dr("DES_CLASE_BILLETE"), GetType(String))
        Util.AtribuirValorObjeto(objProducto.FactorCorreccion, dr("NUM_FACTOR_CORRECCION"), GetType(Decimal))
        Util.AtribuirValorObjeto(objProducto.ProcesadoManual, dr("BOL_MANUAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(_oidProducto, dr("OID_PRODUCTO"), GetType(String))

        'Retorna as maquinas do producto
        objProducto.Maquinas = AccesoDatos.Maquina.RetornaMaquinas(_oidProducto)

        Return objProducto

    End Function

    ''' <summary>
    ''' Popula o objModalidadRecuento
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopularModalidadRecuento(ByVal dr As DataRow) As GetProceso.ModalidadRecuento

        Dim objModalidadRecuento As New GetProceso.ModalidadRecuento

        Util.AtribuirValorObjeto(objModalidadRecuento.Codigo, dr("COD_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objModalidadRecuento.Descripcion, dr("DES_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objModalidadRecuento.CuentaCiego, dr("BOL_CONTAR_CIEGO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objModalidadRecuento.AdmiteIAC, dr("BOL_ADMITE_IAC"), GetType(Boolean))

        Return objModalidadRecuento

    End Function

    ''' <summary>
    ''' Busca o processo de acordo com os parametros passados.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Public Shared Function GetProceso(ByVal objPeticion As ContractoServicio.Proceso.GetProceso.Peticion) As ContractoServicio.Proceso.GetProceso.ProcesoColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.GetProcesoByProceso.ToString()

        Dim filtros As New System.Text.StringBuilder

        'Monta a query de acordo com os parametros passados.
        filtros.Append(MontaClausulaProceso(objPeticion, comando))

        If (filtros.Length > 0) Then
            comando.CommandText &= filtros.ToString
        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaProceso As New ContractoServicio.Proceso.GetProceso.ProcesoColeccion

        'Percorre o dt e retorna uma coleção productos.
        objRetornaProceso = RetornaColProceso(dt)

        Return objRetornaProceso

    End Function

    ''' <summary>
    ''' Monta Clausula Where GetProceso
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Private Shared Function MontaClausulaProceso(ByVal objPeticion As ContractoServicio.Proceso.GetProceso.Peticion, ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New System.Text.StringBuilder

        'Monta a clausula e adiciona os parametros.
        filtros.Append(" WHERE PRSUB.BOL_VIGENTE = []BOL_VIGENTE ")
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))

        If objPeticion.CodigoCliente IsNot Nothing AndAlso objPeticion.CodigoCliente <> String.Empty Then

            filtros.Append(" AND CLI.COD_CLIENTE = []COD_CLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))

        End If

        filtros.Append(Util.MontarClausulaIn(objPeticion.CodigoSubcliente, "COD_SUBCLIENTE", comando, "AND", "SBCLI"))
        filtros.Append(Util.MontarClausulaIn(objPeticion.CodigoPuntoServicio, "COD_PTO_SERVICIO", comando, "AND", "PTSRV"))
        filtros.Append(Util.MontarClausulaIn(objPeticion.CodigoCanal, "COD_CANAL", comando, "AND", "CNL"))
        filtros.Append(Util.MontarClausulaIn(objPeticion.CodigoSubcanal, "COD_SUBCANAL", comando, "AND", "SUB"))
        filtros.Append(Util.MontarClausulaIn(objPeticion.CodigoDelegacion, "COD_DELEGACION", comando, "AND", "PRC"))
        filtros.Append(Util.MontarClausulaIn(objPeticion.CodigoProducto, "COD_PRODUCTO", comando, "AND", "PRD"))

        Return filtros

    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma colecao de proceso
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaColProceso(ByVal dt As DataTable) As ContractoServicio.Proceso.GetProceso.ProcesoColeccion

        Dim objRetornaProceso As New ContractoServicio.Proceso.GetProceso.ProcesoColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaProceso.Add(PopularProcesoByProceso(dr))
            Next
        End If

        Return objRetornaProceso
    End Function

    ''' <summary>
    ''' Popula o proceso
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Private Shared Function PopularProcesoByProceso(ByVal dr As DataRow) As ContractoServicio.Proceso.GetProceso.Proceso

        Dim objProceso As New ContractoServicio.Proceso.GetProceso.Proceso

        Util.AtribuirValorObjeto(objProceso.CodigoCliente, dr("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionCliente, dr("DES_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoSubcliente, dr("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionSubcliente, dr("DES_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoPuntoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionPuntoServicio, dr("DES_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoCanal, dr("COD_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionCanal, dr("DES_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoSubcanal, dr("COD_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionSubcanal, dr("DES_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoProducto, dr("COD_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionProducto, dr("DES_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoDelegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionProceso, dr("DES_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Return objProceso

    End Function

    ''' <summary>
    ''' Busca os processos de acordo com os parametros informados.
    ''' </summary>
    ''' <param name="objProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 Criado
    ''' [octavio.piramo] 25/03/2009 Alterado - Ajustado método, criação da query e popular objeto.
    ''' </history>
    Public Shared Function GetProcesoDetail(ByVal objProceso As ContractoServicio.Proceso.GetProcesoDetail.PeticionProceso, _
                                            ByRef oidProceso As String, _
                                            ByRef oidProcesoSubc As String) As ContractoServicio.Proceso.GetProcesoDetail.Proceso

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetProcesoDetail.ToString())

        ' adicionar os parametros
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoCliente))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoSubcanal))

        ' adicionar filtro subcliente
        If objProceso.CodigoSubcliente IsNot Nothing AndAlso objProceso.CodigoSubcliente <> String.Empty Then
            query.Append(" AND SUBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoSubcliente))
        End If

        ' adicionar filtro punto servicio
        If objProceso.CodigoPuntoServicio IsNot Nothing AndAlso objProceso.CodigoPuntoServicio <> String.Empty Then
            query.Append(" AND PTSRV.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoPuntoServicio))
        End If

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' criar objeto processo
        Dim objRetornaProceso As New ContractoServicio.Proceso.GetProcesoDetail.Proceso

        ' executar e obter registros
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso tenha encontrado algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' popular o objeto processo
            objRetornaProceso = PopularGetProcesoDetail(dt.Rows(0), oidProceso, oidProcesoSubc)

        End If

        ' retornar objeto
        Return objRetornaProceso

    End Function

    ''' <summary>
    ''' Popula um objeto ojtproceso
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 Criado
    ''' </history>
    Private Shared Function PopularGetProcesoDetail(ByVal dr As DataRow, ByRef oidProceso As String, ByRef oidProcesoSubc As String) As ContractoServicio.Proceso.GetProcesoDetail.Proceso

        ' criar objeto
        Dim objProceso As New ContractoServicio.Proceso.GetProcesoDetail.Proceso

        Util.AtribuirValorObjeto(objProceso.Descripcion, dr("DES_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoDelegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.IndicadorMediosPago, dr("BOL_MEDIOS_PAGO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.ContarChequesTotal, dr("BOL_CONTAR_CHEQUES_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.ContarTicketsTotal, dr("BOL_CONTAR_TICKETS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.ContarOtrosTotal, dr("BOL_CONTAR_OTROS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.Observacion, dr("OBS_PROCESO_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.CodigoCanal, dr("COD_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionCanal, dr("DES_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoSubcanal, dr("COD_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionSubcanal, dr("DES_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoCliente, dr("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionCliente, dr("DES_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoSubcliente, dr("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionSubcliente, dr("DES_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoPuntoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionPuntoServicio, dr("DES_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoIac, dr("COD_IAC"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionIac, dr("DES_IAC"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoTipoProcesado, dr("COD_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionTipoProcesado, dr("DES_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoProducto, dr("COD_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionProducto, dr("DES_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoClienteFacturacion, dr("COD_CLIENTE_FACTURACION"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionClienteFacturacion, dr("DES_CLIENTE_FACTURACION"), GetType(String))
        Util.AtribuirValorObjeto(oidProceso, dr("OID_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(oidProcesoSubc, dr("OID_PROCESO_SUBCANAL"), GetType(String))

        Return objProceso

    End Function

#Region "[SETPROCESO]"

    ''' <summary>
    ''' Metodo Verifica se o proceso ja existe
    ''' </summary>
    ''' <param name="objInfGeneral"></param>
    ''' <param name="objInfTolerancia"></param>
    ''' <param name="oidProceso"></param>
    ''' <param name="bolProceso"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Public Shared Sub VerificaProcesoExiste(ByVal objInfGeneral As String, _
                                            ByVal objInfTolerancia As String, _
                                            ByRef oidProceso As String, ByRef bolProceso As Boolean)

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaProcesoExiste.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_CHECKSUM_INF_GNRAL", ProsegurDbType.Identificador_Alfanumerico, objInfGeneral))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_CHECKSUM_INF_TOLERANCIAS", ProsegurDbType.Identificador_Alfanumerico, objInfTolerancia))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt.Rows.Count > 0 AndAlso dt.Rows IsNot Nothing Then

            oidProceso = dt.Rows(0)("OID_PROCESO").ToString
            bolProceso = dt.Rows(0)("BOL_VIGENTE")

        End If

    End Sub

    ''' <summary>
    ''' Metodo verifica se o proceso ja existe com os parametros informados.
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <param name="objCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Public Shared Function VerificaProcesoExisteByClienteSubClientePtoServicio(ByVal oidProceso As String, _
                                                                               ByVal codCliente As String, ByVal codDelegacion As String, _
                                                                               ByVal codSubClinte As String, ByVal codPuntoServicio As String) As String


        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.BuscaProcesoPorClienteSubClientePtoServicio.ToString()

        Dim filtros As New System.Text.StringBuilder

        'Monta a query de acordo com os parametros passados.
        filtros.Append(MontaClausulaVerificaProcesoByClienteSubCliente(oidProceso, codCliente, codDelegacion, _
                                                                       codSubClinte, codPuntoServicio, comando))

        If (filtros.Length > 0) Then
            comando.CommandText &= filtros.ToString
        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Monta Clausula Where GetProceso
    ''' </summary>
    ''' <param name="objCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Private Shared Function MontaClausulaVerificaProcesoByClienteSubCliente(ByVal oidProceso As String, _
                                                                            ByVal codCliente As String, ByVal codDelegacion As String, _
                                                                            ByVal codSubClinte As String, ByVal codPuntoServicio As String, _
                                                                            ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New System.Text.StringBuilder

        'Monta a clausula e adiciona os parametros.
        filtros.Append(" WHERE CLI.COD_CLIENTE = []COD_CLIENTE AND PRPSRV.OID_PROCESO = []OID_PROCESO ")
        filtros.Append(" AND PRPSRV.COD_DELEGACION = []COD_DELEGACION ")

        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))

        If codSubClinte IsNot Nothing AndAlso codSubClinte <> String.Empty Then

            filtros.Append(" AND SUBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, codSubClinte))

        Else
            filtros.Append(" AND SUBCLI.COD_SUBCLIENTE IS NULL ")
        End If

        If codPuntoServicio IsNot Nothing AndAlso codPuntoServicio <> String.Empty Then

            filtros.Append(" AND PTO.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, codPuntoServicio))

        Else
            filtros.Append(" AND PTO.COD_PTO_SERVICIO IS NULL ")
        End If

        filtros.Append(" AND ROWNUM = 1")

        Return filtros

    End Function

    ''' <summary>
    ''' Busca o oidProceso
    ''' </summary>
    ''' <param name="codCliente"></param>
    ''' <param name="codSubCliente"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codPtoServicio"></param>
    ''' <param name="codSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 25/03/2009 - Criado
    ''' </history>
    Public Shared Function BuscaOidProceso(ByVal codCliente As String, ByVal codSubCliente As String, _
                                           ByVal codDelegacion As String, ByVal codPtoServicio As String, _
                                           ByVal codSubCanal As String) As String


        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.BuscaOidProceso.ToString()
        comando.CommandType = CommandType.Text

        Dim filtros As New StringBuilder

        If codSubCliente IsNot Nothing AndAlso codSubCliente <> String.Empty Then
            filtros.Append(" AND SBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, codSubCliente))
        Else
            filtros.Append(" AND SBCLI.COD_SUBCLIENTE IS NULL ")
        End If

        If codPtoServicio IsNot Nothing AndAlso codPtoServicio <> String.Empty Then
            filtros.Append(" AND PTSRV.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, codPtoServicio))
        Else
            filtros.Append(" AND PTSRV.COD_PTO_SERVICIO IS NULL ")
        End If

        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codSubCanal))

        comando.CommandText &= filtros.ToString
        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("OID_PROCESO").ToString
        Else
            Return Nothing
        End If

    End Function

#End Region

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Atualiza o proceso para vigente
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Public Shared Sub ActualizarProcesoParaVigente(ByVal oidProceso As String, ByRef objTransacion As Transacao, _
                                                   ByVal codUsuario As String)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarProcesoParaVigente.ToString())
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

            objTransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Tradutor.Traduzir("016_msg_erro_execucao"))
        End Try

    End Sub

#End Region

#Region "[INSERT]"

    ''' <summary>
    ''' Insere o novo proceso.
    ''' </summary>
    ''' <param name="codInfGeneral"></param>
    ''' <param name="codInfTolerancias"></param>
    ''' <param name="oidTipoPrcesado"></param>
    ''' <param name="oidProducto"></param>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Public Shared Function AltaProceso(ByVal codInfGeneral As String, _
                                 ByVal codInfTolerancias As String, _
                                 ByVal oidTipoPrcesado As String, _
                                 ByVal oidProducto As String, _
                                 ByVal peticion As ContractoServicio.Proceso.SetProceso.Peticion, ByRef objTransacion As Transacao) As String


        Dim OidProceso As String = String.Empty
        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaProceso.ToString())
            comando.CommandType = CommandType.Text

            OidProceso = Guid.NewGuid.ToString
            MontaParameter("OID_PROCESO", OidProceso, comando)
            MontaParameter("COD_CHECKSUM_INF_GNRAL", codInfGeneral, comando)
            MontaParameter("COD_CHECKSUM_INF_TOLERANCIAS", codInfTolerancias, comando)
            MontaParameter("COD_DELEGACION", peticion.Proceso.CodigoDelegacion, comando)
            MontaParameter("OID_TIPO_PROCESADO", oidTipoPrcesado, comando)
            MontaParameter("OID_PRODUCTO", oidProducto, comando)
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "BOL_MEDIOS_PAGO", ProsegurDbType.Logico, peticion.Proceso.IndicadorMediosPago))
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "BOL_CONTAR_CHEQUES_TOTAL", ProsegurDbType.Logico, peticion.Proceso.ContarChequesTotal))
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "BOL_CONTAR_TICKETS_TOTAL", ProsegurDbType.Logico, peticion.Proceso.ContarTicketsTotal))
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "BOL_CONTAR_OTROS_TOTAL", ProsegurDbType.Logico, peticion.Proceso.ContarOtrosTotal))
            MontaParameter("DES_PROCESO", peticion.Proceso.Descripcion, comando)
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, peticion.Proceso.Vigente))
            MontaParameter("COD_USUARIO", peticion.CodigoUsuario, comando)
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

            objTransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Tradutor.Traduzir("016_msg_erro_execucao"))
        End Try

        Return OidProceso

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
    Private Shared Sub MontaParameter(ByVal campo As String, ByVal objeto As String, ByRef comando As IDbCommand)

        If objeto IsNot Nothing AndAlso objeto <> String.Empty Then

            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, objeto))

        Else

            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))

        End If


    End Sub

#End Region

#Region "[GETPROCESOS]"

    Public Shared Function GetProcesos(ByVal Peticion As GetProcesos.Peticion) As GetProcesos.ProcesoColeccion

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Cria novos objetos Proceso e Coleção Procesos
        Dim objProceso As GetProcesos.Proceso = Nothing
        Dim objProcesos As GetProcesos.ProcesoColeccion = Nothing

        'Caso a data final não foi informada, retorna todos os registros a partir da data inicial
        If Peticion.FechaFinal = DateTime.MinValue Then Peticion.FechaFinal = Date.MaxValue

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesos.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "FECHA_INICIAL", ProsegurDbType.Data_Hora, Peticion.FechaInicial))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "FECHA_FINAL", ProsegurDbType.Data_Hora, Peticion.FechaFinal))

        'Cofigura comando caso filtre por Vigente
        If Peticion.Vigente IsNot Nothing Then
            comando.CommandText += " AND PSUB.BOL_VIGENTE = []BOL_VIGENTE "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, Peticion.Vigente))
        End If

        'Preenche DataTable com retorno da consulta
        Dim Procesos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Se houver registros no DataTable
        If Procesos IsNot Nothing AndAlso Procesos.Rows.Count > 0 Then

            'Instancia coleção de procesos
            objProcesos = New GetProcesos.ProcesoColeccion

            'Para cada linha retornada na consulta de processos
            For Each row As DataRow In Procesos.Rows

                'Instancia novo objeto Proceso
                objProceso = New GetProcesos.Proceso

                With objProceso

                    'Preenche objeto Proceso
                    Util.AtribuirValorObjeto(.DescripcionProceso, row("DES_PROCESO"), GetType(String))
                    Util.AtribuirValorObjeto(.ObservacionesProceso, row("OBS_PROCESO_SUBCANAL"), GetType(String))
                    Util.AtribuirValorObjeto(.ContarChequesTotal, row("BOL_CONTAR_CHEQUES_TOTAL"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.ContarTicketsTotal, row("BOL_CONTAR_TICKETS_TOTAL"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.ContarOtrosTotal, row("BOL_CONTAR_OTROS_TOTAL"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.VigenteProceso, row("BOL_VIGENTE"), GetType(Boolean))

                    ' ### PREENCHE COLEÇÕES DO OBJETO PROCESO ### '

                    'Produtos e Máquinas por Proceso
                    .Producto = PopularProductoProcesos(row("OID_PROCESO").ToString())

                    'Modalidad de Recuento por Proceso
                    .ModalidadRecuento = PopularModalidadRecuento(row("OID_PROCESO").ToString())

                    'Se informa medio pago
                    If row("BOL_MEDIOS_PAGO").ToString() = "1" Then

                        'MedioPago por Proceso
                        .MedioPago = PopularMedioPagoProceso(row("OID_PROCESO").ToString())

                        'MedioPagoEfectivo por Proceso
                        .MedioPagoEfectivo = PopularMedioPagoEfectivoProceso(row("OID_PROCESO").ToString())

                    Else

                        'Agrupaciones por Proceso
                        .Agrupaciones = PopularAgrupacionProceso(row("OID_PROCESO").ToString())

                    End If

                    'ProcesoPuntoServicio por Proceso
                    .ProcesoPuntoServicio = PopularProcesoPuntoServicio(row("OID_PROCESO").ToString())

                End With

                'Adiciona objeto Proceso à coleção de procesos
                objProcesos.Add(objProceso)

            Next

        End If

        'Retorna coleção de processos
        Return objProcesos

    End Function

    Private Shared Function PopularProductoProcesos(ByVal Oid_Proceso As String) As GetProcesos.Producto

        'Cria objetos 
        Dim objProducto As GetProcesos.Producto = Nothing
        Dim objMaquina As GetProcesos.Maquina = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProductoPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim Productos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If Productos IsNot Nothing AndAlso Productos.Rows.Count > 0 Then

            'Instancia objeto Producto
            objProducto = New GetProcesos.Producto
            Dim Maquina As String = Nothing

            'Preenche Producto
            With objProducto

                Util.AtribuirValorObjeto(.Codigo, Productos.Rows(0)("COD_PRODUCTO"), GetType(String))
                Util.AtribuirValorObjeto(.Descripcion, Productos.Rows(0)("DES_PRODUCTO"), GetType(String))
                Util.AtribuirValorObjeto(.ClaseBillete, Productos.Rows(0)("DES_CLASE_BILLETE"), GetType(String))
                Util.AtribuirValorObjeto(.FactorCorreccion, Productos.Rows(0)("NUM_FACTOR_CORRECCION"), GetType(Decimal))
                Util.AtribuirValorObjeto(.ProcesadoManual, Productos.Rows(0)("BOL_MANUAL"), GetType(Boolean))

                'Cria nova coleção de máquinas
                .Maquinas = New GetProcesos.MaquinaColeccion

            End With

            'Preenche Maquinas se houver
            For Each row As DataRow In Productos.Rows

                Util.AtribuirValorObjeto(Maquina, row("DES_MAQUINA"), GetType(String))

                If Maquina IsNot Nothing Then

                    'Instancia objeto Maquina
                    objMaquina = New GetProcesos.Maquina

                    'Preenche Maquina
                    objMaquina.Descripcion = Maquina

                    'Adiciona Maquina à coleção
                    objProducto.Maquinas.Add(objMaquina)

                End If

            Next

        End If

        Return objProducto

    End Function

    Private Shared Function PopularModalidadRecuento(ByVal Oid_Proceso As String) As GetProcesos.ModalidadRecuento

        'Cria objetos 
        Dim objModalidadRecuento As GetProcesos.ModalidadRecuento = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetTipoProcesadoPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim ModRecuento As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If ModRecuento IsNot Nothing AndAlso ModRecuento.Rows.Count > 0 Then

            'Instancia objeto Producto
            objModalidadRecuento = New GetProcesos.ModalidadRecuento

            'Preenche Producto
            With objModalidadRecuento

                Util.AtribuirValorObjeto(.CodigoTipoProcesado, ModRecuento.Rows(0)("COD_TIPO_PROCESADO"), GetType(String))
                Util.AtribuirValorObjeto(.DescripcionTipoProcesado, ModRecuento.Rows(0)("DES_TIPO_PROCESADO"), GetType(String))
                Util.AtribuirValorObjeto(.CuentaCiego, ModRecuento.Rows(0)("BOL_CONTAR_CIEGO"), GetType(Boolean))
                Util.AtribuirValorObjeto(.AdmiteIAC, ModRecuento.Rows(0)("BOL_ADMITE_IAC"), GetType(Boolean))
                Util.AtribuirValorObjeto(.VigenteModalidadRecuento, ModRecuento.Rows(0)("BOL_VIGENTE"), GetType(Boolean))

            End With

        End If

        Return objModalidadRecuento

    End Function

    Private Shared Function PopularAgrupacionProceso(ByVal Oid_Proceso As String) As GetProcesos.AgrupacionColeccion

        'Cria objetos 
        Dim objAgrupacion As GetProcesos.Agrupacion = Nothing
        Dim objAgrupColeccion As GetProcesos.AgrupacionColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaAgrupacionesPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim Agrupaciones As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If Agrupaciones IsNot Nothing AndAlso Agrupaciones.Rows.Count > 0 Then

            'Instancia coleção de agrupacion
            objAgrupColeccion = New GetProcesos.AgrupacionColeccion

            'Para cada registro do DataTable
            For Each row As DataRow In Agrupaciones.Rows

                'Instancia objeto Agrupacion
                objAgrupacion = New GetProcesos.Agrupacion

                'Preenche Agrupacion
                With objAgrupacion

                    Util.AtribuirValorObjeto(.Codigo, row("COD_AGRUPACION"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_AGRUPACION"), GetType(String))
                    Util.AtribuirValorObjeto(.Observacion, row("OBS_AGRUPACION"), GetType(String))
                    Util.AtribuirValorObjeto(.ToleranciaParcialMin, row("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaParcialMax, row("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaBultoMin, row("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaBultoMax, row("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaRemesaMin, row("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaRemesaMax, row("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                    'Preenche coleção de divisas por agrupacion
                    .Divisas = PopularDivisaAgrupacion(row("OID_AGRUPACION").ToString())

                    'Preenche coleção de medio pago por agrupacion
                    .MedioPago = PopularMedioPagoAgrupacion(row("OID_AGRUPACION").ToString())

                End With

                'Adciona objeto agrupacion à coleção de agrupaciones
                objAgrupColeccion.Add(objAgrupacion)

            Next

        End If

        Return objAgrupColeccion

    End Function

    Private Shared Function PopularMedioPagoAgrupacion(ByVal Oid_Agrupacion As String) As GetProcesos.MedioPagoColeccion

        'Cria objetos 
        Dim objMedioPago As GetProcesos.MedioPago = Nothing
        Dim objMedioPagoColeccion As GetProcesos.MedioPagoColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetMedioPagoPorAgrupacion.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, Oid_Agrupacion))

        'Preenche DataTable com o resultado da consulta
        Dim MedioPagos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If MedioPagos IsNot Nothing AndAlso MedioPagos.Rows.Count > 0 Then

            'Instancia coleção de agrupacion
            objMedioPagoColeccion = New GetProcesos.MedioPagoColeccion

            'Para cada registro do DataTable
            For Each row As DataRow In MedioPagos.Rows

                'Instancia objeto Agrupacion
                objMedioPago = New GetProcesos.MedioPago

                'Preenche Agrupacion
                With objMedioPago

                    Util.AtribuirValorObjeto(.Codigo, row("COD_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.Observacion, row("OBS_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoTipoMedioPago, row("COD_TIPO_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionTipoMedioPago, row("DES_TIPO_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoIsoDivisa, row("COD_ISO_DIVISA"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionDivisa, row("DES_DIVISA"), GetType(String))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                'Adciona objeto agrupacion à coleção de agrupaciones
                objMedioPagoColeccion.Add(objMedioPago)

            Next

        End If

        Return objMedioPagoColeccion

    End Function

    Private Shared Function PopularMedioPagoProceso(ByVal Oid_Proceso As String) As GetProcesos.MedioPagoColeccion

        'Cria objetos 
        Dim objMedioPago As GetProcesos.MedioPago = Nothing
        Dim objMedioPagoColeccion As GetProcesos.MedioPagoColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaMedioPagoPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim MedioPagos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If MedioPagos IsNot Nothing AndAlso MedioPagos.Rows.Count > 0 Then

            'Instancia coleção de agrupacion
            objMedioPagoColeccion = New GetProcesos.MedioPagoColeccion

            'Para cada registro do DataTable
            For Each row As DataRow In MedioPagos.Rows

                'Instancia objeto Agrupacion
                objMedioPago = New GetProcesos.MedioPago

                'Preenche Agrupacion
                With objMedioPago

                    Util.AtribuirValorObjeto(.Codigo, row("COD_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.Observacion, row("OBS_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoTipoMedioPago, row("COD_TIPO_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionTipoMedioPago, row("DES_TIPO_MEDIO_PAGO"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoIsoDivisa, row("COD_ISO_DIVISA"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionDivisa, row("DES_DIVISA"), GetType(String))
                    Util.AtribuirValorObjeto(.ToleranciaParcialMin, row("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaParcialMax, row("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaBultoMin, row("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaBultoMax, row("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaRemesaMin, row("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.ToleranciaRemesaMax, row("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                'Adciona objeto agrupacion à coleção de agrupaciones
                objMedioPagoColeccion.Add(objMedioPago)

            Next

        End If

        Return objMedioPagoColeccion

    End Function

    Private Shared Function PopularDivisaAgrupacion(ByVal Oid_Agrupacion As String) As GetProcesos.DivisaColeccion

        'Cria objetos 
        Dim objDivisa As GetProcesos.Divisa = Nothing
        Dim objDivisaColeccion As GetProcesos.DivisaColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetDivisaPorAgrupacion.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, Oid_Agrupacion))

        'Preenche DataTable com o resultado da consulta
        Dim Divisas As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If Divisas IsNot Nothing AndAlso Divisas.Rows.Count > 0 Then

            'Instancia coleção de agrupacion
            objDivisaColeccion = New GetProcesos.DivisaColeccion
            Dim CodIsoDivisa As String = String.Empty
            Dim objDenominacion As GetProcesos.Denominacion = Nothing

            'Para cada registro do DataTable
            For Each row As DataRow In Divisas.Rows

                If CodIsoDivisa <> row("COD_ISO_DIVISA").ToString() Then

                    'Instancia objeto Agrupacion
                    objDivisa = New GetProcesos.Divisa

                    'Preenche Agrupacion
                    With objDivisa

                        Util.AtribuirValorObjeto(.CodigoIso, row("COD_ISO_DIVISA"), GetType(String))
                        Util.AtribuirValorObjeto(.Descripcion, row("DES_DIVISA"), GetType(String))
                        Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                        'Cria nova instância para denominaciones
                        .Denominaciones = New GetProcesos.DenominacionColeccion

                    End With

                    'Adciona objeto divisa à coleção de divisas
                    objDivisaColeccion.Add(objDivisa)                    

                End If

                'Instancia objeto Denominacion
                objDenominacion = New GetProcesos.Denominacion

                With objDenominacion

                    Util.AtribuirValorObjeto(.Codigo, row("COD_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(.EsBillete, row("BOL_BILLETE"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.Valor, row("NUM_VALOR"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Peso, row("NUM_PESO"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                objDivisa.Denominaciones.Add(objDenominacion)
                CodIsoDivisa = row("COD_ISO_DIVISA").ToString()

            Next

        End If

        Return objDivisaColeccion

    End Function

    Private Shared Function PopularMedioPagoEfectivoProceso(ByVal Oid_Proceso As String) As GetProcesos.DivisaProcesoColeccion

        'Cria objetos 
        Dim objDivProceso As GetProcesos.DivisaProceso = Nothing
        Dim objDivProcesoColeccion As GetProcesos.DivisaProcesoColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaMedioPagoEfectivoPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim MPEfectivo As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If MPEfectivo IsNot Nothing AndAlso MPEfectivo.Rows.Count > 0 Then

            'Instancia coleção de agrupacion
            objDivProcesoColeccion = New GetProcesos.DivisaProcesoColeccion
            Dim CodIsoDivisa As String = String.Empty
            Dim objDenominacion As GetProcesos.Denominacion = Nothing

            'Para cada registro do DataTable
            For Each row As DataRow In MPEfectivo.Rows

                If CodIsoDivisa <> row("COD_ISO_DIVISA").ToString() Then

                    'Instancia objeto Agrupacion
                    objDivProceso = New GetProcesos.DivisaProceso

                    'Preenche Agrupacion
                    With objDivProceso

                        Util.AtribuirValorObjeto(.CodigoISO, row("COD_ISO_DIVISA"), GetType(String))
                        Util.AtribuirValorObjeto(.Descripcion, row("DES_DIVISA"), GetType(String))
                        Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.ToleranciaParcialMin, row("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaParcialMax, row("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaBultoMin, row("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaBultoMax, row("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaRemesaMin, row("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))
                        Util.AtribuirValorObjeto(.ToleranciaRemesaMax, row("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))

                        'Cria nova instância para denominaciones
                        .Denominaciones = New GetProcesos.DenominacionColeccion

                    End With

                    'Adciona objeto divisa à coleção de divisas
                    objDivProcesoColeccion.Add(objDivProceso)

                End If

                'Instancia objeto Denominacion
                objDenominacion = New GetProcesos.Denominacion

                With objDenominacion

                    Util.AtribuirValorObjeto(.Codigo, row("COD_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_DENOMINACION"), GetType(String))
                    Util.AtribuirValorObjeto(.EsBillete, row("BOL_BILLETE"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.Valor, row("NUM_VALOR"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Peso, row("NUM_PESO"), GetType(Decimal))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                objDivProceso.Denominaciones.Add(objDenominacion)
                CodIsoDivisa = row("COD_ISO_DIVISA").ToString()

            Next

        End If

        Return objDivProcesoColeccion

    End Function

    Private Shared Function PopularProcesoPuntoServicio(ByVal Oid_Proceso As String) As GetProcesos.ProcesoPuntoServicioColeccion

        'Cria objetos 
        Dim objIac As GetProcesos.Iac = Nothing
        Dim objProcesoPS As GetProcesos.ProcesoPuntoServicio = Nothing
        Dim objProcesoPSColeccion As GetProcesos.ProcesoPuntoServicioColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaProcesoPuntoServicioPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

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
                            .Terminos = PopularTerminoIac(row("OID_IAC").ToString(), row("OID_CLIENTE").ToString())

                        End If

                    End With

                    'Adiciona objeto Iac preenchido à propriedade
                    .IAC = objIac
                    .Subcanales = PopularSubcanal(row("OID_PROCESO_POR_PSERVICIO").ToString())
                    .MedioPago = PopularMedioPagoProcesoServicio(row("OID_PROCESO_POR_PSERVICIO").ToString())

                End With

                'Adiciona objeto à coleção
                objProcesoPSColeccion.Add(objProcesoPS)

            Next

        End If

        Return objProcesoPSColeccion

    End Function

    Private Shared Function PopularTerminoIac(ByVal Oid_Iac As String, ByVal Oid_Cliente As String) As GetProcesos.TerminoIacColeccion

        'Cria objetos         
        Dim objTerminoIac As GetProcesos.TerminoIac = Nothing
        Dim objTerminoIacColeccion As GetProcesos.TerminoIacColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscarTerminoPorIac.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_IAC", ProsegurDbType.Objeto_Id, Oid_Iac))
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, Oid_Cliente))

        'Preenche DataTable com o resultado da consulta
        Dim ProcesoPS As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If ProcesoPS IsNot Nothing AndAlso ProcesoPS.Rows.Count > 0 Then

            'Instancia coleção de agrupacion
            objTerminoIacColeccion = New GetProcesos.TerminoIacColeccion
            Dim CodTermino As String = String.Empty
            Dim objValPosibles As GetProcesos.ValorPosible = Nothing

            'Para cada registro do DataTable
            For Each row As DataRow In ProcesoPS.Rows

                If CodTermino <> row("COD_TERMINO").ToString() Then

                    'Instancia objeto Agrupacion
                    objTerminoIac = New GetProcesos.TerminoIac

                    'Preenche Agrupacion
                    With objTerminoIac

                        Util.AtribuirValorObjeto(.Codigo, row("COD_TERMINO"), GetType(String))
                        Util.AtribuirValorObjeto(.Descripcion, row("DES_TERMINO"), GetType(String))
                        Util.AtribuirValorObjeto(.Observacion, row("OBS_TERMINO"), GetType(String))
                        Util.AtribuirValorObjeto(.CodigoFormato, row("COD_FORMATO"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionFormato, row("DES_FORMATO"), GetType(String))
                        Util.AtribuirValorObjeto(.Longitud, row("NEC_LONGITUD"), GetType(Integer))
                        Util.AtribuirValorObjeto(.CodigoMascara, row("COD_MASCARA"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionMascara, row("DES_MASCARA"), GetType(String))
                        Util.AtribuirValorObjeto(.CodigoAlgValidacion, row("COD_ALGORITMO_VALIDACION"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionAlgValidacion, row("DES_ALGORITMO_VALIDACION"), GetType(String))
                        Util.AtribuirValorObjeto(.MostrarCodigo, row("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.BusquedaParcial, row("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.CampoClave, row("BOL_CAMPO_CLAVE"), GetType(Boolean))
                        Util.AtribuirValorObjeto(.Orden, row("NEC_ORDEN"), GetType(Integer))
                        Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                        'Cria nova instância para denominaciones
                        .ValoresPosibles = New GetProcesos.ValorPosibleColeccion

                    End With

                    'Adciona objeto divisa à coleção de divisas
                    objTerminoIacColeccion.Add(objTerminoIac)

                End If

                'Instancia objeto Denominacion
                objValPosibles = New GetProcesos.ValorPosible

                With objValPosibles

                    Util.AtribuirValorObjeto(.Codigo, row("COD_VALOR"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_VALOR"), GetType(String))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                If objValPosibles.Codigo IsNot Nothing Then
                    objTerminoIac.ValoresPosibles.Add(objValPosibles)
                End If

                CodTermino = row("COD_TERMINO").ToString()

            Next

        End If

        Return objTerminoIacColeccion

    End Function

    Private Shared Function PopularSubcanal(ByVal Oid_Proceso_Servicio As String) As GetProcesos.SubcanalColeccion

        'Cria objetos 
        Dim objSubcanal As GetProcesos.Subcanal = Nothing
        Dim objSubcanalColeccion As GetProcesos.SubcanalColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetSubcanalesPorProcesoServicio.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametro(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Objeto_Id, Oid_Proceso_Servicio))

        'Preenche DataTable com o resultado da consulta
        Dim Subcanales As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If Subcanales IsNot Nothing AndAlso Subcanales.Rows.Count > 0 Then

            'Instancia coleção de agrupacion
            objSubcanalColeccion = New GetProcesos.SubcanalColeccion

            'Para cada registro do DataTable
            For Each row As DataRow In Subcanales.Rows

                'Instancia objeto Agrupacion
                objSubcanal = New GetProcesos.Subcanal

                'Preenche Agrupacion
                With objSubcanal

                    Util.AtribuirValorObjeto(.Subcanal, row("COD_SUBCANAL"), GetType(String))

                End With

                'Adciona objeto agrupacion à coleção de agrupaciones
                objSubcanalColeccion.Add(objSubcanal)

            Next

        End If

        Return objSubcanalColeccion

    End Function

    Private Shared Function PopularMedioPagoProcesoServicio(ByVal Oid_Proceso_Servicio As String) As GetProcesos.MedioPagoProceso
        Return Nothing
    End Function

#End Region

End Class