Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

''' <summary>
''' Classe Agrupacion
''' </summary>
''' <remarks></remarks>
Public Class Agrupacion

#Region "[ENUMERADORES]"

    Private Enum TipoFiltro As Integer
        Ticket
        Cheque
        OtrosValores
    End Enum

#End Region

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Obtém agrupaciones através dos filtros informados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Public Shared Function getAgrupaciones(objPeticion As ContractoServicio.Agrupacion.GetAgrupaciones.Peticion) As ContractoServicio.Agrupacion.GetAgrupaciones.AgrupacionColeccion

        ' criar objeto agrupaciones
        Dim objAgrupaciones As New ContractoServicio.Agrupacion.GetAgrupaciones.AgrupacionColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.getAgrupacion.ToString)

        ' adicionar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.VigenteAgrupacion))

        ' montar clausula IN AGRUPACION
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.CodigoAgrupacion, "COD_AGRUPACION", comando, "AND", "AGR"))
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.DescripcionAgrupacion, "DES_AGRUPACION", comando, "AND", "AGR"))

        ' montar clausula IN DIVISA
        query.Append(PrepararFiltroEfetivo(objPeticion.DivisaEfectivo, comando))

        ' preparar o filtro de ticket
        query.Append(PrepararFiltroTicketOtroValorCheque(objPeticion.CodigoTicket, objPeticion.DivisaTicket, TipoFiltro.Ticket, comando))

        ' preparar o filtro de otros valores
        query.Append(PrepararFiltroTicketOtroValorCheque(objPeticion.CodigoOtroValor, objPeticion.DivisaOtroValor, TipoFiltro.OtrosValores, comando))

        ' preparar o filtro de cheques
        query.Append(PrepararFiltroTicketOtroValorCheque(objPeticion.CodigoCheque, objPeticion.DivisaCheque, TipoFiltro.Cheque, comando))

        ' preparar a query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            For Each dr As DataRow In dtQuery.Rows

                objAgrupaciones.Add(PopularGetAgrupaciones(dr))

            Next

        End If

        ' retornar objeto preenchido
        Return objAgrupaciones

    End Function

    ''' <summary>
    ''' Popula um objeto agrupacion através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Private Shared Function PopularGetAgrupaciones(dr As DataRow) As ContractoServicio.Agrupacion.GetAgrupaciones.Agrupacion

        ' criar objeto agrupacion
        Dim objAgrupacion As New ContractoServicio.Agrupacion.GetAgrupaciones.Agrupacion

        Util.AtribuirValorObjeto(objAgrupacion.Codigo, dr("COD_AGRUPACION"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.Descripcion, dr("DES_AGRUPACION"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.Observacion, dr("OBS_AGRUPACION"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Return objAgrupacion

    End Function

    ''' <summary>
    ''' Popula o objAgrupacion
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopularAgrupacionesGetProceso(dt As DataTable) As GetProceso.AgrupacionColeccion

        Dim objAgrupacionColeccion As New GetProceso.AgrupacionColeccion
        Dim oidAgrupacion As String = String.Empty

        For Each dr In dt.Rows

            Dim objAgrupacion As New GetProceso.Agrupacion

            Util.AtribuirValorObjeto(oidAgrupacion, dr("OID_AGRUPACION"), GetType(String))
            Util.AtribuirValorObjeto(objAgrupacion.Codigo, dr("COD_AGRUPACION"), GetType(String))
            Util.AtribuirValorObjeto(objAgrupacion.Descripcion, dr("DES_AGRUPACION"), GetType(String))
            Util.AtribuirValorObjeto(objAgrupacion.Observacion, dr("OBS_AGRUPACION"), GetType(String))
            Util.AtribuirValorObjeto(objAgrupacion.ToleranciaParcialMin, dr("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))
            Util.AtribuirValorObjeto(objAgrupacion.TolerenciaParcialMax, dr("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))
            Util.AtribuirValorObjeto(objAgrupacion.ToleranciaBultoMin, dr("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))
            Util.AtribuirValorObjeto(objAgrupacion.ToleranciaBultoMax, dr("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))
            Util.AtribuirValorObjeto(objAgrupacion.ToleranciaRemesaMin, dr("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))
            Util.AtribuirValorObjeto(objAgrupacion.ToleranciaRemesaMax, dr("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))

            'Retorna as divisas e denominações da agrapação.
            objAgrupacion.Divisas = AccesoDatos.Divisa.RetornaDivisasAgrupacion(oidAgrupacion)

            'Retorna os médios de pago da agrupação.
            objAgrupacion.MediosDePago = AccesoDatos.MedioPagoPorAgrupacion.RetornarMediosPagoAgrupacion(oidAgrupacion)

            objAgrupacionColeccion.Add(objAgrupacion)

            objAgrupacion = Nothing

        Next

        Return objAgrupacionColeccion

    End Function

    ''' <summary>
    ''' Verifica se o código da agrupacion já existe na base de dados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Shared Function VerificarCodigoAgrupacion(objPeticion As ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AGRUPACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoAgrupacion))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Verifica se a descrição da agrupacion já existe na base de dados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Shared Function VerificarDescripcionAgrupacion(objPeticion As ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_AGRUPACION", ProsegurDbType.Descricao_Longa, objPeticion.DescripcionAgrupacion))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Obtém a agrupaciones
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Function ObterAgrupaciones(objPeticion As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.AgrupacionColeccion

        ' criar objeto agrupaciones
        Dim objAgrupaciones As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.AgrupacionColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.getAgrupacionesDetailObterAgrupaciones.ToString())

        ' criar filtro IN
        query.Append(Util.MontarClausulaIn(objPeticion.CodigoAgrupacion, "COD_AGRUPACION", comando, "WHERE"))

        ' preparar a query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            For Each dr As DataRow In dtQuery.Rows

                objAgrupaciones.Add(PopularObterAgrupaciones(dr))

            Next

        End If

        ' retornar objeto preenchido
        Return objAgrupaciones

    End Function

    ''' <summary>
    ''' Popula um objeto agrupacion através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Private Shared Function PopularObterAgrupaciones(dr As DataRow) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Agrupacion

        ' criar objeto agrupacion
        Dim objAgrupacion As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Agrupacion

        Util.AtribuirValorObjeto(objAgrupacion.Codigo, dr("cod_agrupacion"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.Descripcion, dr("des_agrupacion"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.Observacion, dr("obs_agrupacion"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.Vigente, dr("bol_vigente"), GetType(Boolean))

        Return objAgrupacion

    End Function

    ''' <summary>
    ''' Obtém OidAgrupacion através de um código agrupacion
    ''' </summary>
    ''' <param name="CodAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Public Shared Function ObterOidAgrupacion(CodAgrupacion As String) As String

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AGRUPACION", ProsegurDbType.Identificador_Alfanumerico, CodAgrupacion))

        Dim OidAgrupacion As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            OidAgrupacion = dtQuery.Rows(0)("OID_AGRUPACION")
        End If

        Return OidAgrupacion

    End Function

    ''' <summary>
    ''' Verifica se Processo esta vigente utilizando a agrupacion informada
    ''' Retorna True caso encontre alguma entidade vigente que usa a agrupacion
    ''' </summary>
    ''' <param name="CodigoAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Public Shared Function VerificarEntidadesVigentesComDivisa(CodigoAgrupacion As String) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarEntidadesVigentesComAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AGRUPACION", ProsegurDbType.Identificador_Alfanumerico, CodigoAgrupacion))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Retorna uma coleção de agrupaciones
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] Criado 11/03/2009
    ''' </history>
    Public Shared Function RetornaAgrupaciones(oidProceso As String) As GetProceso.AgrupacionColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objColAgrupaciones As New GetProceso.AgrupacionColeccion

        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaAgrupaciones.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, oidProceso))

        Dim dtAgrupaciones As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'RETORNA AGRUPACIONES
        'Verifica se o dtAgrupaciones retornou registro.
        If dtAgrupaciones IsNot Nothing AndAlso dtAgrupaciones.Rows.Count > 0 Then

            Return PopularAgrupacionesGetProceso(dtAgrupaciones)

        Else

            Return Nothing

        End If

    End Function

#Region "[GETLISTAAGRUPACIONES]"

    ''' <summary>
    ''' Busca as agrupações vigentes e retorna uma coleção de agrupações.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Public Shared Function GetListaAgrupaciones() As GetListaAgrupaciones.AgrupacionColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetListaAgrupaciones.ToString())

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaAgrupacion As New GetListaAgrupaciones.AgrupacionColeccion

        'Percorre o dt e retorna uma coleção de agrupações.
        objRetornaAgrupacion = RetornaColecaoAgrupacion(dt)

        ' retornar objeto
        Return objRetornaAgrupacion

    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de iac.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoAgrupacion(dt As DataTable) As GetListaAgrupaciones.AgrupacionColeccion

        Dim objRetornaAgrupacion As New GetListaAgrupaciones.AgrupacionColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaAgrupacion.Add(PopularGetAgrupacion(dr))
            Next

        End If

        Return objRetornaAgrupacion
    End Function

    ''' <summary>
    ''' Função PopularGetAgrupacion cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2008 Criado
    ''' </history>
    Private Shared Function PopularGetAgrupacion(dr As DataRow) As GetListaAgrupaciones.Agrupacion

        Dim objAgrupacion As New GetListaAgrupaciones.Agrupacion

        Util.AtribuirValorObjeto(objAgrupacion.Codigo, dr("COD_AGRUPACION"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.Descripcion, dr("DES_AGRUPACION"), GetType(String))

        ' retornar objeto
        Return objAgrupacion
    End Function
#End Region

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere Agrupacion e os relacionamentos AGRUPACION X DIVISA e AGRUPACION X MEDIO PAGO
    ''' </summary>
    ''' <param name="objAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Public Shared Sub AltaAgrupacion(objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion, _
                                     CodigoUsuario As String)

        Try

            ' criar transacao
            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            ' efetuar a inserção de uma agrupação
            Dim oidAgrupacion As String = AltaAgrupacion(objAgrupacion, CodigoUsuario, objTransacao)

            ' efetuar inserção relacionamento Agrupacion X Divisa
            AltaAgrupacionDivisa(objAgrupacion, CodigoUsuario, oidAgrupacion, objTransacao)

            ' efetuar inserção relacionamento Agrupacion X Medio Pago
            AltaAgrupacionMedioPago(objAgrupacion, CodigoUsuario, oidAgrupacion, objTransacao)

            ' realizar a transação
            objTransacao.RealizarTransacao()

        Catch ex As Exception

            Excepcion.Util.Tratar(ex, Traduzir("003_msg_Erro_UKAgrupacion"))

        End Try

    End Sub

    ''' <summary>
    ''' Efetua a inserção de uma agrupação.
    ''' </summary>
    ''' <param name="objAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Private Shared Function AltaAgrupacion(objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion, _
                                           CodigoUsuario As String, _
                                           ByRef objTransacao As Transacao) As String

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' gerar guid
        Dim OidAgrupacion As String = Guid.NewGuid().ToString

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, OidAgrupacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AGRUPACION", ProsegurDbType.Identificador_Alfanumerico, objAgrupacion.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_AGRUPACION", ProsegurDbType.Descricao_Longa, objAgrupacion.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_AGRUPACION", ProsegurDbType.Observacao_Longa, objAgrupacion.Observacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objAgrupacion.Vigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        ' adicionar comando para transação
        objTransacao.AdicionarItemTransacao(comando)

        Return OidAgrupacion

    End Function

    ''' <summary>
    ''' Efetua a inserção do relacionamento AGRUPACION X DIVISA
    ''' </summary>
    ''' <param name="objAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="OidAgrupacion"></param>
    ''' <param name="ObjTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Private Shared Sub AltaAgrupacionDivisa(objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion, _
                                            CodigoUsuario As String, _
                                            OidAgrupacion As String, _
                                            ByRef ObjTransacao As Transacao)

        ' obter todas as divisas
        Dim dtdivisas As DataTable = AccesoDatos.Divisa.ObterDivisas(True)

        ' caso existam Efetivos para agrupacao
        If objAgrupacion.Efectivos IsNot Nothing _
            AndAlso objAgrupacion.Efectivos.Count > 0 Then

            ' para cada efetivo da agrupacao
            For Each objEfetivo As ContractoServicio.Agrupacion.SetAgrupaciones.Efectivo In objAgrupacion.Efectivos

                ' obter oid da divisa
                Dim dr() As DataRow = dtdivisas.Select(String.Format("COD_ISO_DIVISA = '{0}'", objEfetivo.CodigoIsoDivisa))

                ' se encontrou algum registro
                If dr.Count > 0 Then

                    ' adicionar relacionamento DIVISA X AGRUPACION
                    AccesoDatos.DivisaPorAgrupacion.AltaDivisaPorAgrupacion(dr(0)("OID_DIVISA"), OidAgrupacion, CodigoUsuario, ObjTransacao)

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Efetua a inserção do relacionamento AGRUPACION X MEDIO PAGO
    ''' </summary>
    ''' <param name="objAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="OidAgrupacion"></param>
    ''' <param name="ObjTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Private Shared Sub AltaAgrupacionMedioPago(objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion, _
                                               CodigoUsuario As String, _
                                               OidAgrupacion As String, _
                                               ByRef ObjTransacao As Transacao)

        ' caso existam Resto Medio Pago
        If objAgrupacion.MediosPago IsNot Nothing _
            AndAlso objAgrupacion.MediosPago.Count > 0 Then

            ' para cada medio pago
            For Each objMedioPago In objAgrupacion.MediosPago

                ' obter oid medio pago
                Dim OidMedioPago As String = AccesoDatos.MedioPago.ObterOidMedioPago(objMedioPago.CodigoIsoDivisa, _
                                                                                     objMedioPago.CodigoMedioPago, _
                                                                                     objMedioPago.CodigoTipoMedioPago)

                ' se o oid não for vazio
                If Not OidMedioPago.Equals(String.Empty) Then

                    ' adicionar relacionamento MEDIO PAGO X AGRUPACION
                    AccesoDatos.MedioPagoPorAgrupacion.AltaMedioPagoPorAgrupacion(OidMedioPago, OidAgrupacion, CodigoUsuario, ObjTransacao)

                End If

            Next

        End If

    End Sub

#End Region

#Region "[ATUALIZAR]"

    ''' <summary>
    ''' Atualiza agrupacion e refaz os relacionamento AGRUPACION X DIVISA e AGRUPACION X MEDIO PAGO
    ''' </summary>
    ''' <param name="objAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Public Shared Sub ActualizarAgrupacion(objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion, _
                                           CodigoUsuario As String, _
                                           OidAgrupacion As String)

        Try

            ' criar transacao
            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            ' efetuar alteração nos dados da agrupação
            ModificarAgrupacion(objAgrupacion, CodigoUsuario, OidAgrupacion, objTransacao)

            ' remover todos os relacionamentos AGRUPACION X DIVISA
            DivisaPorAgrupacion.BorrarDivisasPorAgrupacion(OidAgrupacion, objTransacao)

            ' remover todos os relacionamentos AGRUPACION X MEDIO PAGO
            MedioPagoPorAgrupacion.BorrarMediosPagoPorAgrupacion(OidAgrupacion, objTransacao)

            ' efetuar a inserção do relacionamento AGRUPACION X DIVISA
            AltaAgrupacionDivisa(objAgrupacion, CodigoUsuario, OidAgrupacion, objTransacao)

            ' efetuar a inserção do relacionamento AGRUPACION X MEDIO PAGO
            AltaAgrupacionMedioPago(objAgrupacion, CodigoUsuario, OidAgrupacion, objTransacao)

            ' realizar a transação
            objTransacao.RealizarTransacao()

        Catch ex As Exception

            Excepcion.Util.Tratar(ex, Traduzir("003_msg_Erro_UKAgrupacion"))

        End Try

    End Sub

    ''' <summary>
    ''' Efetuar alteração dos dados de uma agrupação.
    ''' </summary>
    ''' <param name="objAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="OidAgrupacion"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Private Shared Sub ModificarAgrupacion(objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion, _
                                           CodigoUsuario As String, _
                                           OidAgrupacion As String, _
                                           ByRef objTransacao As Transacao)


        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append("UPDATE gepr_tagrupacion SET ")

        ' adicionar campos caso eles estejam preenchidos.
        query.Append(Util.AdicionarCampoQuery("des_agrupacion = []des_agrupacion,", "des_agrupacion", comando, objAgrupacion.Descripcion, ProsegurDbType.Descricao_Longa))
        query.Append(Util.AdicionarCampoQuery("obs_agrupacion = []obs_agrupacion,", "obs_agrupacion", comando, objAgrupacion.Observacion, ProsegurDbType.Observacao_Longa))
        query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objAgrupacion.Vigente, ProsegurDbType.Logico))

        ' adicionar cod_usuario
        query.Append("cod_usuario = []cod_usuario, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))

        ' adicionar fyh_actualizacion
        query.Append("fyh_actualizacion = []fyh_actualizacion ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

        ' adicionar clausula where
        query.Append("WHERE cod_agrupacion = []cod_agrupacion ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_agrupacion", ProsegurDbType.Identificador_Alfanumerico, objAgrupacion.Codigo))

        ' preparar comando
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' adicionar comando para transação
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Efetua a alta ou a baixa de uma agrupacion
    ''' </summary>
    ''' <param name="CodigoAgrupacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    Public Shared Sub BajaAgrupacion(CodigoAgrupacion As String, _
                                     CodigoUsuario As String)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaBajaAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' setar parameters
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, False))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AGRUPACION", ProsegurDbType.Identificador_Alfanumerico, CodigoAgrupacion))

        ' executar comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    End Sub

#End Region

#Region "[FILTROS]"

    ''' <summary>
    ''' Prepara o filtro ticket através dos codigos ou divisas
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 12/02/2009 Criado
    ''' </history>
    Private Shared Function PrepararFiltroEfetivo(CodigoDivisa As List(Of String), _
                                                  ByRef comando As IDbCommand) As String

        ' criar filtro
        Dim filtro As New StringBuilder

        ' remover valores vazios
        CodigoDivisa = Util.RemoverItensVazios(CodigoDivisa)

        ' criar filtro por codigo divisa
        If CodigoDivisa IsNot Nothing _
            AndAlso CodigoDivisa.Count > 0 Then

            filtro.Append(" AND AGR.OID_AGRUPACION IN")
            filtro.Append(" (SELECT OID_AGRUPACION")
            filtro.Append(" FROM GEPR_TDIVISA_POR_AGRUPACION DIVAGR")
            filtro.Append(" INNER JOIN GEPR_TDIVISA DIV ON DIVAGR.OID_DIVISA = DIV.OID_DIVISA")
            filtro.Append(Util.MontarClausulaIn(CodigoDivisa, "COD_ISO_DIVISA", comando, "WHERE", "DIV", "EFETIVO"))
            filtro.Append(")")

        End If

        ' retornar filtro
        Return filtro.ToString

    End Function

    ''' <summary>
    ''' Prepara o filtro ticket através dos codigos ou divisas
    ''' </summary>
    ''' <param name="Codigo"></param>
    ''' <param name="Divisa"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Private Shared Function PrepararFiltroTicketOtroValorCheque(Codigo As List(Of String), _
                                                                Divisa As List(Of String), _
                                                                TpFiltro As TipoFiltro, _
                                                                ByRef comando As IDbCommand) As String

        ' criar filtro
        Dim filtro As New StringBuilder
        Dim CodTipoMedioPago As String = String.Empty
        Dim TipoBusca As String = String.Empty

        ' obter o tipo da consulta
        If TpFiltro = Agrupacion.TipoFiltro.Ticket Then
            CodTipoMedioPago = AppSettings("Codigo_Tipo_Medio_Pago_Ticket")
            TipoBusca = "T"
        ElseIf TpFiltro = Agrupacion.TipoFiltro.OtrosValores Then
            CodTipoMedioPago = AppSettings("Codigo_Tipo_Medio_Pago_OtrosValores")
            TipoBusca = "O"
        ElseIf TpFiltro = Agrupacion.TipoFiltro.Cheque Then
            CodTipoMedioPago = AppSettings("Codigo_Tipo_Medio_Pago_Cheque")
            TipoBusca = "C"
        End If

        ' remover valores vazios
        Codigo = Util.RemoverItensVazios(Codigo)
        Divisa = Util.RemoverItensVazios(Divisa)

        ' criar filtro por codigo
        If Codigo IsNot Nothing _
            AndAlso Codigo.Count > 0 Then

            filtro.Append(" AND AGR.OID_AGRUPACION IN")
            filtro.Append(" (SELECT OID_AGRUPACION")
            filtro.Append(" FROM GEPR_TMEDIO_PAGO_POR_AGRUPACIO MPAGR")
            filtro.Append(" INNER JOIN GEPR_TMEDIO_PAGO MP ON MPAGR.OID_MEDIO_PAGO = MP.OID_MEDIO_PAGO")
            filtro.Append(" WHERE MP.COD_TIPO_MEDIO_PAGO = []" & TipoBusca & "COD_TIPO_MEDIO_PAGO ")
            filtro.Append(Util.MontarClausulaIn(Codigo, "COD_MEDIO_PAGO", comando, "AND", "MP", TipoBusca))
            filtro.Append(")")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, TipoBusca & "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, CodTipoMedioPago))

        End If

        ' criar filtro por divisa
        If Divisa IsNot Nothing _
            AndAlso Divisa.Count > 0 Then

            filtro.Append(" AND AGR.OID_AGRUPACION IN")
            filtro.Append(" (SELECT OID_AGRUPACION")
            filtro.Append(" FROM GEPR_TMEDIO_PAGO_POR_AGRUPACIO MPAGR")
            filtro.Append(" INNER JOIN GEPR_TMEDIO_PAGO MP ON MPAGR.OID_MEDIO_PAGO = MP.OID_MEDIO_PAGO")
            filtro.Append(" INNER JOIN GEPR_TDIVISA DIV ON MP.OID_DIVISA = DIV.OID_DIVISA")
            filtro.Append(" WHERE MP.COD_TIPO_MEDIO_PAGO = []" & TipoBusca & "DIVCOD_TIPO_MEDIO_PAGO ")
            filtro.Append(Util.MontarClausulaIn(Divisa, "COD_ISO_DIVISA", comando, "AND", "DIV", TipoBusca))
            filtro.Append(")")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, TipoBusca & "DIVCOD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, CodTipoMedioPago))

        End If

        ' retornar filtro
        Return filtro.ToString

    End Function

#End Region

End Class