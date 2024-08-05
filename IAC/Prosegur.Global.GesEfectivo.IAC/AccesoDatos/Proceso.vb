Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis

''' <summary>
''' Classe Proceso
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 11/03/2009 Criado
''' </history>
Public Class Proceso

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
    Private _oidIacBulto As String
    Private _oidIacRemesa As String
    Private _oidCliente As String
    Private _oidSubCliente As String
    Private _oidPuntoServicio As String
    Private _oidSubCanal As String
    Private _bolAdmiteIac As Boolean
    Private _bolMediosPago As Boolean
    Private _oidProcesoSubCanal As String

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

    Public ReadOnly Property OidIACBulto() As String
        Get
            Return _oidIacBulto
        End Get
    End Property

    Public ReadOnly Property OidIACRemesa() As String
        Get
            Return _oidIacRemesa
        End Get
    End Property

    Public ReadOnly Property OidCliente() As String
        Get
            Return _oidCliente
        End Get
    End Property

    Public ReadOnly Property OidSubCliente() As String
        Get
            Return _oidSubCliente
        End Get
    End Property

    Public ReadOnly Property OidPuntoServicio() As String
        Get
            Return _oidPuntoServicio
        End Get
    End Property

    Public ReadOnly Property OidSubCanal() As String
        Get
            Return _oidSubCanal
        End Get
    End Property

    Public ReadOnly Property OidProcesoSubCanal() As String
        Get
            Return _oidProcesoSubCanal
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
    Public Function GetProcesoIntegracion(peticion As GetProceso.Peticion) As GetProceso.Proceso

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objProceso As New GetProceso.Proceso

        comando.CommandText = My.Resources.GetProceso.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoCliente))

        If String.IsNullOrEmpty(peticion.CodigoSubcliente) Then
            ' retira parametros []COD_SUBCLIENTE da consulta            
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubcliente))
        End If

        If String.IsNullOrEmpty(peticion.CodigoPuntoServicio) Then
            ' retira parametros []COD_PTO_SERVICIO da consulta                        
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPuntoServicio))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubcanal))
        'Retorna do WebConfig o Código da Delegação Central
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DELEGACIONCENTRAL", ProsegurDbType.Identificador_Alfanumerico, AppSettings("codigo_delegacion_central")))

        If Not String.IsNullOrEmpty(peticion.IdentificadorProceso) Then
            comando.CommandText = comando.CommandText.Replace("{0}", "AND PROC.OID_PROCESO = []OID_PROCESO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, peticion.IdentificadorProceso))
        Else
            comando.CommandText = comando.CommandText.Replace("{0}", "")
        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dtProceso As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtProceso IsNot Nothing AndAlso dtProceso.Rows.Count > 0 Then

            With dtProceso.Rows(0)

                'Preenche propriedades
                Util.AtribuirValorObjeto(_oidProceso, .Item("OID_PROCESO").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidProducto, .Item("OID_PRODUCTO").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidIac, .Item("OID_IAC").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidIacBulto, .Item("OID_IAC_BULTO").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidIacRemesa, .Item("OID_IAC_REMESA").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidCliente, .Item("OID_CLIENTE").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_bolMediosPago, .Item("BOL_MEDIOS_PAGO"), GetType(Boolean))
                Util.AtribuirValorObjeto(_oidSubCanal, .Item("OID_SUBCANAL").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidProcesoSubCanal, .Item("OID_PROCESO_SUBCANAL").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidSubCliente, .Item("OID_SUBCLIENTE").ToString(), GetType(String))
                Util.AtribuirValorObjeto(_oidPuntoServicio, .Item("OID_PTO_SERVICIO").ToString(), GetType(String))

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
    Private Function PopularProceso(dr As DataRow) As GetProceso.Proceso

        Dim objproceso As New GetProceso.Proceso

        Util.AtribuirValorObjeto(objproceso.IdentificadorProceso, dr("OID_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.Cliente, dr("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.SubCliente, dr("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.PuntoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.SubCanal, dr("COD_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.DescripcionSubCanal, dr("DES_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.Delegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.DescripcionProceso, dr("DES_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.ObservacionesProceso, dr("OBS_PROCESO_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.ClienteFacturacion, dr("COD_CLIENTE_FACTURACION"), GetType(String))
        Util.AtribuirValorObjeto(objproceso.ContarChequesTotal, dr("BOL_CONTAR_CHEQUES_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproceso.ContarTicketsTotal, dr("BOL_CONTAR_TICKETS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproceso.ContarOtrosTotal, dr("BOL_CONTAR_OTROS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproceso.ContarTarjetasTotal, dr("BOL_CONTAR_TARJETAS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproceso.VigenteProceso, dr("BOL_VIGENTE"), GetType(Boolean))

        If dr("COD_PRODUCTO") IsNot DBNull.Value Then
            'Preenche os produtos do proceso
            objproceso.Producto = PopularProductoProceso(dr)
        End If

        If dr("COD_TIPO_PROCESADO") IsNot DBNull.Value Then

            'Preenche as modalidades de recuento do proceso
            objproceso.ModalidadRecuento = PopularModalidadRecuento(dr)

            'Se houver características de modalidade recuento
            If objproceso.ModalidadRecuento IsNot Nothing AndAlso _
                objproceso.ModalidadRecuento.Caracteristicas IsNot Nothing Then

                'Verifica se alguma característica é do tipo "Admite IAC"
                _bolAdmiteIac = CaracteristicaAdmiteIac(objproceso.ModalidadRecuento.Caracteristicas)

            End If

        End If

        Return objproceso

    End Function

    ''' <summary>
    ''' Dentre as características possíveis, 
    ''' verifica se existe "Admite IAC"
    ''' </summary>
    ''' <param name="Caracteristicas"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 15/05/2009 Criado
    ''' </history>
    Private Function CaracteristicaAdmiteIac(Caracteristicas As GetProceso.CaracteristicaColeccion) As Boolean

        'Procura na coleção se existe alguma característica do tipo "Admite IAC"
        Dim AdmiteIac = From Carac In Caracteristicas _
                        Where (Carac.CodigoCaracteristicaConteo = _
                               ContractoServicio.Constantes.COD_CARAC_ADMITE_IAC)

        'Retorna True ou False dependendo do item encontrado
        Return AdmiteIac.Count > 0

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
    Private Function PopularProductoProceso(dr As DataRow) As GetProceso.Producto

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
    Private Shared Function PopularModalidadRecuento(dr As DataRow) As GetProceso.ModalidadRecuento

        Dim objModalidadRecuento As New GetProceso.ModalidadRecuento

        Util.AtribuirValorObjeto(objModalidadRecuento.Codigo, dr("COD_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objModalidadRecuento.Descripcion, dr("DES_TIPO_PROCESADO"), GetType(String))

        'Prepara objeto e chama método responsável por retornar uma coleção de Caracteristicas
        objModalidadRecuento.Caracteristicas = AccesoDatos.Caracteristica.GetCaracteristicaTipoProcesado(dr("OID_TIPO_PROCESADO"))

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
    Public Shared Function GetProceso(objPeticion As ContractoServicio.Proceso.GetProceso.Peticion) As ContractoServicio.Proceso.GetProceso.ProcesoColeccion

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
    ''' Busca o processo de acordo com os parametros passados.
    ''' </summary>
    ''' <param></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [ealfaro] 15/02/2022 Criado
    ''' </history>
    Public Shared Function GetProcesoSapr() As ContractoServicio.Proceso.GetProceso.ProcesoSaprColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.GetProcesoSapr.ToString()

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaProceso As New ContractoServicio.Proceso.GetProceso.ProcesoSaprColeccion

        'Percorre o dt e retorna uma coleção productos.
        objRetornaProceso = RetornaColProcesoSapr(dt)

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
    Private Shared Function MontaClausulaProceso(objPeticion As ContractoServicio.Proceso.GetProceso.Peticion, ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New System.Text.StringBuilder

        'Monta a clausula e adiciona os parametros.
        filtros.Append(" WHERE PRSUB.BOL_VIGENTE = []BOL_VIGENTE ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))

        If objPeticion.CodigoCliente IsNot Nothing AndAlso objPeticion.CodigoCliente <> String.Empty Then

            filtros.Append(" AND CLI.COD_CLIENTE = []COD_CLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))

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
    Private Shared Function RetornaColProceso(dt As DataTable) As ContractoServicio.Proceso.GetProceso.ProcesoColeccion

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
    ''' Percorre o dt e retorna uma colecao de proceso
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaColProcesoSapr(dt As DataTable) As ContractoServicio.Proceso.GetProceso.ProcesoSaprColeccion

        Dim objRetornaProcesoSapr As New ContractoServicio.Proceso.GetProceso.ProcesoSaprColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaProcesoSapr.Add(PopularProcesoByProcesoSapr(dr))
            Next
        End If

        Return objRetornaProcesoSapr
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
    Private Shared Function PopularProcesoByProceso(dr As DataRow) As ContractoServicio.Proceso.GetProceso.Proceso

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
        Util.AtribuirValorObjeto(objProceso.DescripcionClaseBillete, dr("DES_CLASE_BILLETE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoDelegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionProceso, dr("DES_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.IdentificadorProceso, dr("OID_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Return objProceso

    End Function

    ''' <summary>
    ''' Popula o proceso sapr
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [ealfaro] 15/02/2022 Criado
    ''' </history>
    Private Shared Function PopularProcesoByProcesoSapr(dr As DataRow) As ContractoServicio.Proceso.GetProceso.ProcesoSapr

        Dim objProceso As New ContractoServicio.Proceso.GetProceso.ProcesoSapr

        Util.AtribuirValorObjeto(objProceso.OidProceso, dr("OID_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionProceso, dr("DES_PROCESO"), GetType(String))

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
    Public Shared Function GetProcesoDetail(objProceso As ContractoServicio.Proceso.GetProcesoDetail.PeticionProceso, _
                                            ByRef oidProcesoSubc As String) As ContractoServicio.Proceso.GetProcesoDetail.Proceso

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetProcesoDetail.ToString())

        ' adicionar os parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoSubcanal))

        ' adicionar filtro subcliente
        If objProceso.CodigoSubcliente IsNot Nothing AndAlso objProceso.CodigoSubcliente <> String.Empty Then
            query.Append(" AND SUBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoSubcliente))
        Else
            query.Append(" AND SUBCLI.COD_SUBCLIENTE IS NULL")
        End If

        ' adicionar filtro punto servicio
        If objProceso.CodigoPuntoServicio IsNot Nothing AndAlso objProceso.CodigoPuntoServicio <> String.Empty Then
            query.Append(" AND PTSRV.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objProceso.CodigoPuntoServicio))
        Else
            query.Append(" AND PTSRV.COD_PTO_SERVICIO IS NULL")
        End If

        ' adicionar filtro oidProceso
        If objProceso.IdentificadorProceso IsNot Nothing AndAlso objProceso.IdentificadorProceso <> String.Empty Then
            query.Append(" AND PRC.OID_PROCESO = []OID_PROCESO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, objProceso.IdentificadorProceso))
        End If

        query.Append(" ORDER BY PRSUB.BOL_VIGENTE DESC")


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
            objRetornaProceso = PopularGetProcesoDetail(dt.Rows(0), oidProcesoSubc)

        End If

        ' retornar objeto
        Return objRetornaProceso

    End Function

    ''' <summary>
    ''' Busca os processos de acordo com os parametros informados.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/02/2010  Criado
    ''' </history>
    Public Shared Function GetProcesoDetailByOid(OidProceso As String, ByRef oidProcesoSubc As String) As ContractoServicio.Proceso.GetProcesoDetail.Proceso

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetProcesoDetailByOid.ToString())

        ' adicionar filtro oidProceso
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, OidProceso))

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
            objRetornaProceso = PopularGetProcesoDetail(dt.Rows(0), oidProcesoSubc)

        End If

        ' retornar objeto
        Return objRetornaProceso

    End Function

    Public Shared Function GetRowDatosProcesoDefecto() As DataRow

        ' Data row que contera os dados do proceso e que sera retornada
        Dim RowProcesoDefecto As DataRow = Nothing

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' preparar query
        comando.CommandText = Util.PrepararQuery(My.Resources.GetDatosProcesoDefecto.ToString)
        comando.CommandType = CommandType.Text

        ' executar e obter registros
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso tenha encontrado algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ' popular o objeto processo
            RowProcesoDefecto = dt.Rows.Item(0)
        End If

        Return RowProcesoDefecto

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
    Private Shared Function PopularGetProcesoDetail(dr As DataRow, ByRef oidProcesoSubc As String) As ContractoServicio.Proceso.GetProcesoDetail.Proceso

        ' criar objeto
        Dim objProceso As New ContractoServicio.Proceso.GetProcesoDetail.Proceso

        Util.AtribuirValorObjeto(objProceso.Descripcion, dr("DES_PROCESO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoDelegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.IndicadorMediosPago, dr("BOL_MEDIOS_PAGO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.ContarChequesTotal, dr("BOL_CONTAR_CHEQUES_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.ContarTicketsTotal, dr("BOL_CONTAR_TICKETS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.ContarOtrosTotal, dr("BOL_CONTAR_OTROS_TOTAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objProceso.ContarTajetasTotal, dr("BOL_CONTAR_TARJETAS_TOTAL"), GetType(Boolean))
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
        Util.AtribuirValorObjeto(objProceso.CodigoIACBulto, dr("COD_IAC_BULTO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoIACRemesa, dr("COD_IAC_REMESA"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionIac, dr("DES_IAC"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoTipoProcesado, dr("COD_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionTipoProcesado, dr("DES_TIPO_PROCESADO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoProducto, dr("COD_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionProducto, dr("DES_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionClaseBillete, dr("DES_CLASE_BILLETE"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.CodigoClienteFacturacion, dr("COD_CLIENTE_FACTURACION"), GetType(String))
        Util.AtribuirValorObjeto(objProceso.DescripcionClienteFacturacion, dr("DES_CLIENTE_FACTURACION"), GetType(String))
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
    Public Shared Sub VerificaProcesoExiste(objInfGeneral As String, _
                                            objInfTolerancia As String, _
                                            ByRef oidProceso As String, ByRef bolProceso As Boolean)

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaProcesoExiste.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CHECKSUM_INF_GNRAL", ProsegurDbType.Identificador_Alfanumerico, objInfGeneral))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CHECKSUM_INF_TOLERANCIAS", ProsegurDbType.Identificador_Alfanumerico, objInfTolerancia))

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
    ''' <param name="codCliente"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codSubClinte"></param>
    ''' <param name="codPuntoServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Public Shared Function VerificaProcesoExisteByClienteSubClientePtoServicio(oidProceso As String, _
                                                                               codCliente As String, codDelegacion As String, _
                                                                               codSubClinte As String, codPuntoServicio As String) As String


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
    ''' <param name="oidProceso"></param>
    ''' <param name="codCliente"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codSubClinte"></param>
    ''' <param name="codPuntoServicio"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Private Shared Function MontaClausulaVerificaProcesoByClienteSubCliente(oidProceso As String, _
                                                                            codCliente As String, codDelegacion As String, _
                                                                            codSubClinte As String, codPuntoServicio As String, _
                                                                            ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New System.Text.StringBuilder

        'Monta a clausula e adiciona os parametros.
        filtros.Append(" WHERE CLI.COD_CLIENTE = []COD_CLIENTE AND PRPSRV.OID_PROCESO = []OID_PROCESO ")
        filtros.Append(" AND PRPSRV.COD_DELEGACION = []COD_DELEGACION ")

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))

        If codSubClinte IsNot Nothing AndAlso codSubClinte <> String.Empty Then

            filtros.Append(" AND SUBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, codSubClinte))

        Else
            filtros.Append(" AND SUBCLI.COD_SUBCLIENTE IS NULL ")
        End If

        If codPuntoServicio IsNot Nothing AndAlso codPuntoServicio <> String.Empty Then

            filtros.Append(" AND PTO.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, codPuntoServicio))

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
    Public Shared Function BuscaOidProceso(codCliente As String, codSubCliente As String, _
                                           codDelegacion As String, codPtoServicio As String, _
                                           codSubCanal As String) As String


        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.BuscaOidProceso.ToString()
        comando.CommandType = CommandType.Text

        Dim filtros As New StringBuilder

        If codSubCliente IsNot Nothing AndAlso codSubCliente <> String.Empty Then
            filtros.Append(" AND SBCLI.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, codSubCliente))
        Else
            filtros.Append(" AND SBCLI.COD_SUBCLIENTE IS NULL ")
        End If

        If codPtoServicio IsNot Nothing AndAlso codPtoServicio <> String.Empty Then
            filtros.Append(" AND PTSRV.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, codPtoServicio))
        Else
            filtros.Append(" AND PTSRV.COD_PTO_SERVICIO IS NULL ")
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codSubCanal))

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
    Public Shared Sub ActualizarProcesoParaVigente(oidProceso As String, ByRef objTransacion As Transacao, _
                                                   codUsuario As String)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarProcesoParaVigente.ToString())
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Identificador_Alfanumerico, oidProceso))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))


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
    Public Shared Function AltaProceso(codInfGeneral As String, _
                                 codInfTolerancias As String, _
                                 oidTipoPrcesado As String, _
                                 oidProducto As String, _
                                 peticion As ContractoServicio.Proceso.SetProceso.Peticion, ByRef objTransacion As Transacao) As String


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
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MEDIOS_PAGO", ProsegurDbType.Logico, peticion.Proceso.IndicadorMediosPago))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONTAR_CHEQUES_TOTAL", ProsegurDbType.Logico, peticion.Proceso.ContarChequesTotal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONTAR_TICKETS_TOTAL", ProsegurDbType.Logico, peticion.Proceso.ContarTicketsTotal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONTAR_OTROS_TOTAL", ProsegurDbType.Logico, peticion.Proceso.ContarOtrosTotal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONTAR_TARJETAS_TOTAL", ProsegurDbType.Logico, peticion.Proceso.ContarTarjetasTotal))
            MontaParameter("DES_PROCESO", peticion.Proceso.Descripcion, comando)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, peticion.Proceso.Vigente))
            MontaParameter("COD_USUARIO", peticion.CodigoUsuario, comando)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))

            objTransacion.AdicionarItemTransacao(comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("016_msg_erro_execucao"))
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
    Private Shared Sub MontaParameter(campo As String, objeto As String, ByRef comando As IDbCommand)

        If objeto IsNot Nothing AndAlso objeto <> String.Empty Then

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, objeto))

        Else

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))

        End If


    End Sub

#End Region

#Region "[GETPROCESOS]"

    ''' <summary>
    ''' Retorna coleção de Procesos preenchida
    ''' de acordo com os parâmetros de Peticion informados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProcesos(Peticion As GetProcesos.Peticion) As GetProcesos.ProcesoColeccion

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Caso a data final não foi informada, retorna todos os registros a partir da data inicial
        If Peticion.FechaFinal = DateTime.MinValue Then Peticion.FechaFinal = Date.MaxValue

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesos.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_INICIAL", ProsegurDbType.Data_Hora, Peticion.FechaInicial))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_FINAL", ProsegurDbType.Data_Hora, Peticion.FechaFinal))

        'Cofigura comando caso filtre por Vigente
        If Peticion.Vigente IsNot Nothing Then
            comando.CommandText += " AND PSUB.BOL_VIGENTE = []BOL_VIGENTE "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, Peticion.Vigente))
        End If

        'Preenche DataTable com retorno da consulta
        Dim DtProcesos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Se houver registros no DataTable
        If DtProcesos IsNot Nothing AndAlso DtProcesos.Rows.Count > 0 Then
            'Instancia coleção de procesos
            Return PopularObjetoGetProcesos(DtProcesos)
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Preenche a coleção de procesos e seus objetos dependentes.
    ''' </summary>
    ''' <param name="DtProcesos">Procesos recuperados na consulta</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function PopularObjetoGetProcesos(DtProcesos As DataTable) As GetProcesos.ProcesoColeccion

        Dim objModRecuento As New GetProcesos.ModalidadRecuento
        Dim objProcesos As New GetProcesos.ProcesoColeccion()
        Dim objProceso As GetProcesos.Proceso


        'Para cada linha retornada na consulta de processos
        For Each row As DataRow In DtProcesos.Rows

            'Instancia novo objeto Proceso
            objProceso = New GetProcesos.Proceso
            objModRecuento = New GetProcesos.ModalidadRecuento

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
                .Producto = AccesoDatos.Producto.PopularProductoProcesos(row("OID_PROCESO").ToString())

                With objModRecuento

                    Util.AtribuirValorObjeto(.CodigoTipoProcesado, row("COD_TIPO_PROCESADO"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionTipoProcesado, row("DES_TIPO_PROCESADO"), GetType(String))
                    Util.AtribuirValorObjeto(.VigenteModalidadRecuento, row("VIGENTE_MOD_RECUENTO"), GetType(Boolean))

                    'Adiciona as características
                    Dim objCaracteristicas As GetProceso.CaracteristicaColeccion = AccesoDatos.Caracteristica.GetCaracteristicaTipoProcesado(row("OID_TIPO_PROCESADO").ToString())
                    If objCaracteristicas.Count > 0 Then
                        .Caracteristicas = New GetProcesos.CaracteristicaColeccion()
                        For Each objCaracteristica As GetProceso.Caracteristica In objCaracteristicas
                            .Caracteristicas.Add(New GetProcesos.Caracteristica() With {.Codigo = objCaracteristica.Codigo, .CodigoConteo = objCaracteristica.CodigoCaracteristicaConteo, .Descripcion = objCaracteristica.Descripcion})
                        Next
                    End If

                End With

                'Modalidad de Recuento por Proceso
                .ModalidadRecuento = objModRecuento 'AccesoDatos.TiposProcesado.PopularModalidadRecuento(row("OID_PROCESO").ToString())                

                'Se informa medio pago
                If row("BOL_MEDIOS_PAGO").ToString() = "1" Then

                    'MedioPago por Proceso
                    .MedioPago = AccesoDatos.MedioPagoPorProceso.PopularMedioPagoProceso(row("OID_PROCESO").ToString())

                    'MedioPagoEfectivo por Proceso
                    .MedioPagoEfectivo = AccesoDatos.DivisaPorProceso.PopularMedioPagoEfectivoProceso(row("OID_PROCESO").ToString())

                Else

                    'Agrupaciones por Proceso
                    .Agrupaciones = AccesoDatos.AgrupacionPorProceso.PopularAgrupacionProceso(row("OID_PROCESO").ToString())

                End If

                'ProcesoPuntoServicio por Proceso
                .ProcesoPuntoServicio = AccesoDatos.ProcesoPorPServicio.PopularProcesoPuntoServicio(row("OID_PROCESO").ToString())

            End With

            'Adiciona objeto Proceso à coleção de procesos
            objProcesos.Add(objProceso)

            objProceso = Nothing
            objModRecuento = Nothing
        Next

        Return objProcesos

    End Function

#End Region

#Region "[GETPROCESOSPORDELEGACION]"

    ''' <summary>
    ''' Retorna coleção de Procesos preenchida
    ''' de acordo com os parâmetros de Peticion informados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProcesosPorDelegacion(Peticion As GetProcesosPorDelegacion.Peticion) As GetProcesosPorDelegacion.ProcesoColeccion

        ' cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoPorDelegacion.ToString())

        ' adiciona parâmetros obrigatórios
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "CODIGO_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoDelegacion))

        If Not String.IsNullOrEmpty(Peticion.CodigoCliente) Then
            comando.CommandText += "       AND C.COD_CLIENTE = []CODIGO_CLIENTE "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "CODIGO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoCliente))
        End If
        If Not String.IsNullOrEmpty(Peticion.DescripcionCliente) Then
            comando.CommandText += "       AND C.DES_CLIENTE LIKE []DESCRIPCION_CLIENTE "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DESCRIPCION_CLIENTE", ProsegurDbType.Descricao_Longa, Peticion.DescripcionCliente))
        End If
        If Not String.IsNullOrEmpty(Peticion.CodigoSubCliente) Then
            comando.CommandText += "       AND SC.COD_SUBCLIENTE = []CODIGO_SUBCLIENTE "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "CODIGO_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoSubCliente))
        End If
        If Not String.IsNullOrEmpty(Peticion.DescripcionSubCliente) Then
            comando.CommandText += "       AND SC.DES_SUBCLIENTE LIKE []DESCRIPCION_SUBCLIENTE "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DESCRIPCION_SUBCLIENTE", ProsegurDbType.Descricao_Longa, Peticion.DescripcionSubCliente))
        End If
        If Not String.IsNullOrEmpty(Peticion.CodigoPuntoServicio) Then
            comando.CommandText += "       AND PS.COD_PTO_SERVICIO = []CODIGO_PUNTO_SERVICIO "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "CODIGO_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoPuntoServicio))
        End If
        If Not String.IsNullOrEmpty(Peticion.DescripcionPuntoServicio) Then
            comando.CommandText += "       AND PS.DES_PTO_SERVICIO LIKE []DESCRIPCION_PUNTO_SERVICIO "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DESCRIPCION_PUNTO_SERVICIO", ProsegurDbType.Descricao_Longa, Peticion.DescripcionPuntoServicio))
        End If
        If Not String.IsNullOrEmpty(Peticion.CodigoCanal) Then
            comando.CommandText += "       AND CA.COD_CANAL = []CODIGO_CANAL "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "CODIGO_CANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoCanal))
        End If
        If Not String.IsNullOrEmpty(Peticion.DescripcionCanal) Then
            comando.CommandText += "       AND CA.DES_CANAL LIKE []DESCRIPCION_CANAL "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DESCRIPCION_CANAL", ProsegurDbType.Descricao_Longa, Peticion.DescripcionCanal))
        End If
        If Not String.IsNullOrEmpty(Peticion.CodigoSubCanal) Then
            comando.CommandText += "       AND SCA.COD_SUBCANAL = []CODIGO_SUBCANAL "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "CODIGO_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoSubCanal))
        End If
        If Not String.IsNullOrEmpty(Peticion.DescripcionSubCanal) Then
            comando.CommandText += "       AND SCA.DES_SUBCANAL = []DESCRIPCION_SUBCANAL "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DESCRIPCION_SUBCANAL", ProsegurDbType.Descricao_Longa, Peticion.DescripcionSubCanal))
        End If
        If Not String.IsNullOrEmpty(Peticion.DescripcionProceso) Then
            comando.CommandText += "       AND P.DES_PROCESO LIKE []DESCRIPCION_PROCESO "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DESCRIPCION_PROCESO", ProsegurDbType.Descricao_Longa, Peticion.DescripcionProceso))
        End If
        If Peticion.EstadoVigencia IsNot Nothing Then
            comando.CommandText += "       AND SCA.BOL_VIGENTE = []VIGENTE "
            comando.CommandText = Util.PrepararQuery(comando.CommandText)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "VIGENTE", ProsegurDbType.Logico, Peticion.EstadoVigencia))
        End If

        ' preenche datatable com retorno da consulta
        Dim dtProcesos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se houver registros no datatable
        If dtProcesos IsNot Nothing AndAlso dtProcesos.Rows.Count > 0 Then
            ' instancia coleção de procesos
            Return PopularObjetoGetProcesosPorDelegacion(dtProcesos)
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Preenche a coleção de procesos e seus objetos dependentes.
    ''' </summary>
    ''' <param name="dtProcesos">Procesos recuperados na consulta</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function PopularObjetoGetProcesosPorDelegacion(dtProcesos As DataTable) As GetProcesosPorDelegacion.ProcesoColeccion

        Dim objProcesos As New GetProcesosPorDelegacion.ProcesoColeccion
        Dim objProceso As GetProcesosPorDelegacion.Proceso
        Dim objSubCanal As GetProcesosPorDelegacion.SubCanal

        Try

            ' para cada linha retornada na consulta de processos
            For Each row As DataRow In dtProcesos.Rows
                Dim rowLocal = row
                ' tenta recuperar o projeto
                objProceso = (From p In objProcesos _
                              Where p.IdentificadorProceso = rowLocal("IDENTIFICADOR_PROCESO")).FirstOrDefault()

                ' se não existir, cria um novo
                If objProceso Is Nothing Then

                    objProceso = New GetProcesosPorDelegacion.Proceso

                    With objProceso
                        Util.AtribuirValorObjeto(.CodigoCliente, row("CODIGO_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(.CodigoDelegacion, row("CODIGO_DELEGACION"), GetType(String))
                        Util.AtribuirValorObjeto(.CodigoPuntoServicio, row("CODIGO_PUNTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(.CodigoSubCliente, row("CODIGO_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionCliente, row("DESCRIPCION_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionProceso, row("DESCRIPCION_PROCESO"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionPuntoServicio, row("DESCRIPCION_PUNTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(.DescripcionSubcliente, row("DESCRIPCION_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(.IdentificadorProceso, row("IDENTIFICADOR_PROCESO"), GetType(String))
                        Util.AtribuirValorObjeto(.Vigente, row("VIGENTE"), GetType(Boolean))
                    End With

                    objProceso.SubCanales = New GetProcesosPorDelegacion.SubCanalColeccion

                    ' adiciona objeto proceso à coleção de procesos
                    objProcesos.Add(objProceso)

                End If

                ' instancia novo objeto subcanal
                objSubCanal = New GetProcesosPorDelegacion.SubCanal

                With objSubCanal
                    Util.AtribuirValorObjeto(.CodigoCanal, row("CODIGO_CANAL"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoSubCanal, row("CODIGO_SUBCANAL"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionCanal, row("DESCRIPCION_CANAL"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionSubCanal, row("DESCRIPCION_SUBCANAL"), GetType(String))
                End With

                ' adiciona o subcanal
                objProcesos.Find(Function(p) p.IdentificadorProceso = rowLocal("IDENTIFICADOR_PROCESO")).SubCanales.Add(objSubCanal)

                objSubCanal = Nothing
                objProceso = Nothing

            Next

        Catch ex As Exception
            Throw
        End Try

        Return objProcesos

    End Function

#End Region

End Class