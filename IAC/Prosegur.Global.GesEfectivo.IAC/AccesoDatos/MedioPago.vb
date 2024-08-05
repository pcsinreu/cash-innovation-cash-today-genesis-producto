Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

''' <summary>
''' Classe MedioPago
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 03/02/2009 Criado
''' [pda] 19/02/2009 Modificado
''' </history>
Public Class MedioPago

#Region "[CONSULTAS]"

#Region "GETMEDIOPAGOS"

    ''' <summary>
    ''' Obtém Medio de Pagos através dos filtros informados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 19/02/2009 Criado
    ''' </history>
    Public Shared Function GetMedioPagos(objPeticion As ContractoServicio.MedioPago.GetMedioPagos.Peticion) As ContractoServicio.MedioPago.GetMedioPagos.MedioPagoColeccion

        ' criar objeto Medio Pagos
        Dim objMedioPagos As New ContractoServicio.MedioPago.GetMedioPagos.MedioPagoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Obter query genérica
        Dim query As New StringBuilder
        query.Append(My.Resources.GetMedioPago.ToString)

        'Adiciona Where caso algum parâmetro seja passado
        Dim clausulaWhere As New CriterioColecion

        ' Validação Campo Mercancia
        clausulaWhere.addCriterio("AND", " MPG.BOL_MERCANCIA = []BOL_MERCANCIA ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MERCANCIA", ProsegurDbType.Logico, objPeticion.EsMercancia))

        ' Validação Campo Vigente
        If objPeticion.Vigente IsNot Nothing Then
            clausulaWhere.addCriterio("AND", " MPG.BOL_VIGENTE = []BOL_VIGENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))
        End If

        ' Validação Campo Codigo
        If objPeticion.Codigo IsNot Nothing AndAlso objPeticion.Codigo.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaLikeUpper(objPeticion.Codigo, "COD_MEDIO_PAGO", comando, String.Empty, "MPG"))
        End If

        ' Validação Campo Codigo Tipo Medio Pago
        If objPeticion.CodigoTipoMedioPago IsNot Nothing AndAlso objPeticion.CodigoTipoMedioPago.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaIn(objPeticion.CodigoTipoMedioPago, "COD_TIPO_MEDIO_PAGO", comando, String.Empty, "MPG"))
        End If

        ' Validação Campo Codigo Divisa
        If objPeticion.CodigoDivisa IsNot Nothing AndAlso objPeticion.CodigoDivisa.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaIn(objPeticion.CodigoDivisa, "COD_ISO_DIVISA", comando, String.Empty, "DIV"))
        End If

        ' Validação Campo Descricao Divisa
        If objPeticion.DescripcionDivisa IsNot Nothing AndAlso objPeticion.DescripcionDivisa.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaIn(objPeticion.DescripcionDivisa, "DES_DIVISA", comando, String.Empty, "DIV"))
        End If


        'Adiciona a clausula Where
        If clausulaWhere.Count > 0 Then
            query.Append(Util.MontarClausulaWhere(clausulaWhere))

            ' Validação Campo Descricao
            If objPeticion.Descripcion IsNot Nothing AndAlso objPeticion.Descripcion.Count > 0 Then
                query.Append(Util.MontarClausulaLikeUpper(objPeticion.Descripcion, "DES_MEDIO_PAGO", comando, "AND", "MPG"))
            End If
        Else
            ' Validação Campo Descricao
            If objPeticion.Descripcion IsNot Nothing AndAlso objPeticion.Descripcion.Count > 0 Then
                query.Append(Util.MontarClausulaLikeUpper(objPeticion.Descripcion, "DES_MEDIO_PAGO", comando, "WHERE", "MPG"))
            End If
        End If


        ' preparar a query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            For Each dr As DataRow In dtQuery.Rows

                objMedioPagos.Add(PopularGetMedioPago(dr))

            Next

        End If

        ' retornar objeto preenchido
        Return objMedioPagos

    End Function

    ''' <summary>
    ''' Popula um objeto Medio Pago através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 19/02/2009 Criado
    ''' </history>
    Private Shared Function PopularGetMedioPago(dr As DataRow) As ContractoServicio.MedioPago.GetMedioPagos.MedioPago

        ' criar objeto termino Iac
        Dim objMedioPago As New ContractoServicio.MedioPago.GetMedioPagos.MedioPago

        'Medio Pago
        Util.AtribuirValorObjeto(objMedioPago.Codigo, dr("COD_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Descripcion, dr("DES_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Observaciones, dr("OBS_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.CodigoAccesoMedioPago, dr("COD_ACCESO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.EsMercancia, dr("BOL_MERCANCIA"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMedioPago.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        'Tipo Medio Pago
        Util.AtribuirValorObjeto(objMedioPago.CodigoTipoMedioPago, dr("COD_TIPO_MEDIO_PAGO"), GetType(String))

        If dr("COD_TIPO_MEDIO_PAGO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("COD_TIPO_MEDIO_PAGO")) Then
            objMedioPago.DescripcionTipoMedioPago = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("COD_TIPO_MEDIO_PAGO"))
        End If

        'Divisa
        Util.AtribuirValorObjeto(objMedioPago.CodigoDivisa, dr("COD_ISO_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.DescripcionDivisa, dr("DES_DIVISA"), GetType(String))

        Return objMedioPago

    End Function

    ''' <summary>
    ''' Obtém os medios pago através de um código tipo medio pago e código divisa
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Shared Function GetComboMediosPagoByTipoAndDivisa(objPeticion As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Peticion) As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.MedioPagoColeccion

        Dim query As New StringBuilder

        ' criar objeto medio pago coleccion
        Dim objMedioPago As New ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.MedioPagoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        query.Append(My.Resources.getComboMediosPagoByTipoAndDivisa.ToString)

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoTipoMedioPago))

        ' caso o codigo da divisa não seja vazio
        If Not String.IsNullOrEmpty(objPeticion.CodigoIsoDivisa) Then

            query.Append(" AND DIV.COD_ISO_DIVISA = []COD_ISO_DIVISA")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoIsoDivisa))

        End If

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows

                ' adicionar para coleção
                objMedioPago.Add(PopularComboMediosPago(dr))

            Next

        End If

        ' retornar coleção de medio pago
        Return objMedioPago

    End Function

    ''' <summary>
    ''' Popula o objeto através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Private Shared Function PopularComboMediosPago(dr As DataRow) As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.MedioPago

        ' criar objeto tipos medio pago
        Dim objMedioPago As New ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.MedioPago

        Util.AtribuirValorObjeto(objMedioPago.Codigo, dr("cod_medio_pago"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Descripcion, dr("des_medio_pago"), GetType(String))

        ' retorna objeto preenchido
        Return objMedioPago

    End Function

#End Region

#Region "GETMEDIOPAGODETAIL"

    ''' <summary>
    ''' Obtém o Termino de um Médio Pago através dos filtros informados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' [blcosta] 28/06/2010 modificado
    ''' </history>
    Public Shared Function GetMedioPagoDetail(objPeticion As ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion) As ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPagoColeccion

        ' criar objeto medio pago col
        Dim objMedioPagoCol As New ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPagoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Obter query genérica
        Dim query As New StringBuilder
        query.Append(My.Resources.GetMedioPagoDetailObterMedioPago.ToString)

        'Adiciona Where caso algum parâmetro seja passado
        Dim clausulaWhere As New CriterioColecion

        ' Validação Campo CodigoMedioPago
        If objPeticion.CodigoMedioPago IsNot Nothing AndAlso objPeticion.CodigoMedioPago.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaIn(objPeticion.CodigoMedioPago, "COD_MEDIO_PAGO", comando, String.Empty, "MPG"))
        End If

        ' Validação Campo CodigoDivisa
        If objPeticion.CodigoIsoDivisa IsNot Nothing AndAlso objPeticion.CodigoIsoDivisa.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaIn(objPeticion.CodigoIsoDivisa, "COD_ISO_DIVISA", comando, String.Empty, "DIV"))
        End If

        ' Validação Campo CodigoTipoMedioPago
        If objPeticion.CodigoMedioPago IsNot Nothing AndAlso objPeticion.CodigoMedioPago.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaIn(objPeticion.CodigoTipoMedioPago, "COD_TIPO_MEDIO_PAGO", comando, String.Empty, "MPG"))
        End If

        'Adiciona a clausula Where
        If clausulaWhere.Count > 0 Then
            query.Append(Util.MontarClausulaWhere(clausulaWhere))
        End If

        comando.CommandType = CommandType.Text

        '################## RETORNA OS MEDIO PAGOS ######################
        ' preparar a query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        ' executar query Medio Pago
        Dim dtQueryMedioPago As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        '#############################################################


        ' se encontrou algum registro
        If dtQueryMedioPago IsNot Nothing _
            AndAlso dtQueryMedioPago.Rows.Count > 0 Then

            Dim strOidMedioPago As String = String.Empty
            For Each dr As DataRow In dtQueryMedioPago.Rows
                'Preenche o objeto medio pago
                strOidMedioPago = String.Empty
                Dim objMedioPago As ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPago
                objMedioPago = PopularMedioPagoDetailObterMedioPago(dr, strOidMedioPago)

                '################## RETORNA OS TERMINOS ######################
                Dim dtQueryTerminos As DataTable = TerminoMedioPago.ObterTerminosPorMedioPado(strOidMedioPago)
                '#############################################################


                If dtQueryTerminos IsNot Nothing _
                AndAlso dtQueryTerminos.Rows.Count > 0 Then

                    'Preenche o objeto Termino
                    Dim strOidTerminoMedioPago As String = String.Empty
                    For Each drTerminos As DataRow In dtQueryTerminos.Rows
                        'Preenche o objeto Termino pago
                        strOidTerminoMedioPago = String.Empty
                        Dim objTerminoMedioPago As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
                        objTerminoMedioPago = PopularMedioPagoObterTermino(drTerminos, strOidTerminoMedioPago)

                        '################## RETORNA OS VALORES DE TERMINOS ######################
                        Dim dtQueryValorTerminos As DataTable = ValorTerminoMedioPago.ObterValorTerminoPorOidTermino(strOidTerminoMedioPago)
                        '#############################################################

                        If dtQueryValorTerminos IsNot Nothing _
                        AndAlso dtQueryValorTerminos.Rows.Count > 0 Then

                            For Each drValorTermino As DataRow In dtQueryValorTerminos.Rows
                                'Preenche o objeto Valor Termino pago
                                Dim objValorTerminoMedioPago As ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino
                                objValorTerminoMedioPago = PopularMedioPagoObterValor(drValorTermino)

                                'Adiciona o valor de termino na coleção de valores do objeto termino
                                objTerminoMedioPago.ValoresTermino.Add(objValorTerminoMedioPago)
                            Next

                        End If
                        'Adiciona o termino na coleção de terminos Medio Pago
                        objMedioPago.TerminosMedioPago.Add(objTerminoMedioPago)
                    Next
                End If

                'Adiciona o medio pago na coleção de medios de pago
                objMedioPagoCol.Add(objMedioPago)

            Next

        End If


        ' retornar objeto preenchido
        Return objMedioPagoCol

    End Function

    ''' <summary>
    ''' Popula um objeto termino iac através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    Private Shared Function PopularMedioPagoDetailObterMedioPago(dr As DataRow, ByRef OidMedioPago As String) As ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPago

        ' criar objeto Medio Pago
        Dim objMedioPago As New ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPago

        'Retorna o valor do OidMedioPago         
        Util.AtribuirValorObjeto(OidMedioPago, dr("OID_MEDIO_PAGO"), GetType(String))

        'Termino
        Util.AtribuirValorObjeto(objMedioPago.Codigo, dr("COD_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Descripcion, dr("DES_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Observaciones, dr("OBS_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.EsMercancia, dr("BOL_MERCANCIA"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMedioPago.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMedioPago.CodigoAccesoMedioPago, dr("COD_ACCESO"), GetType(String))

        'Tipo Medio Pago
        Util.AtribuirValorObjeto(objMedioPago.CodigoTipoMedioPago, dr("COD_TIPO_MEDIO_PAGO"), GetType(String))

        If dr("COD_TIPO_MEDIO_PAGO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("COD_TIPO_MEDIO_PAGO")) Then
            objMedioPago.DescripcionTipoMedioPago = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("COD_TIPO_MEDIO_PAGO"))
        End If

        'Divisa
        Util.AtribuirValorObjeto(objMedioPago.CodigoDivisa, dr("COD_ISO_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.DescripcionDivisa, dr("DES_DIVISA"), GetType(String))

        'Cria a coleção de termino para ser populada posteriormente
        objMedioPago.TerminosMedioPago = New ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion

        Return objMedioPago

    End Function

    ''' <summary>
    ''' Popula um objeto termino através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    Private Shared Function PopularMedioPagoObterTermino(dr As DataRow, ByRef OidTerminoMedioPago As String) As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago

        ' criar objeto termino Iac
        Dim objTerminoMedioPago As New ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago

        'Retorna o valor do OidTermino         
        Util.AtribuirValorObjeto(OidTerminoMedioPago, dr("OID_TERMINO"), GetType(String))

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
        Util.AtribuirValorObjeto(objTerminoMedioPago.DescripcionFormato, dr("DES_FORMATO"), GetType(String))

        'Algoritmo
        Util.AtribuirValorObjeto(objTerminoMedioPago.CodigoAlgoritmo, dr("COD_ALGORITMO_VALIDACION"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.DescripcionAlgoritmo, dr("DES_ALGORITMO_VALIDACION"), GetType(String))

        'Mascara
        Util.AtribuirValorObjeto(objTerminoMedioPago.CodigoMascara, dr("COD_MASCARA"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.DescripcionMascara, dr("DES_MASCARA"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.ExpRegularMascara, dr("DES_EXP_REGULAR"), GetType(String))

        'Cria a coleção de termino para ser populada posteriormente
        objTerminoMedioPago.ValoresTermino = New ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion

        Return objTerminoMedioPago

    End Function

    ''' <summary>
    ''' Popula um valor de termino através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    Private Shared Function PopularMedioPagoObterValor(dr As DataRow) As ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino

        ' criar objeto valor termino Iac
        Dim objValorTerminoMedioPago As New ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino

        'Valor de Termino
        Util.AtribuirValorObjeto(objValorTerminoMedioPago.Codigo, dr("COD_VALOR"), GetType(String))
        Util.AtribuirValorObjeto(objValorTerminoMedioPago.Descripcion, dr("DES_VALOR"), GetType(String))
        Util.AtribuirValorObjeto(objValorTerminoMedioPago.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Return objValorTerminoMedioPago

    End Function

#End Region

#Region "[GETMEDIOSPAGOINTEGRACION]"

    ''' <summary>
    ''' Função Selecionar, faz a pesquisa de medios pago e preenche do datatable e retorna uma coleção. 
    ''' </summary>
    ''' <param name="objpeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Public Shared Function GetMediosPagoIntegracion(objPeticion As GetMediosPago.Peticion) As GetMediosPago.MedioPagoColeccion

        Dim objMedioPagoCol As New GetMediosPago.MedioPagoColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.GetMediosPagoIntegracion.ToString()

        Dim filtros As New System.Text.StringBuilder

        If objPeticion.Vigente IsNot Nothing Then

            If filtros.Length > 0 Then
                filtros.Append(" AND MPG.BOL_VIGENTE = []BOL_VIGENTE")
            Else
                filtros.Append(" MPG.BOL_VIGENTE = []BOL_VIGENTE")
            End If

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))
        End If

        If objPeticion.FechaInical <> Nothing Then

            If filtros.Length > 0 Then
                filtros.Append(" AND MPG.FYH_ACTUALIZACION >= []DATAINI")
            Else
                filtros.Append(" MPG.FYH_ACTUALIZACION >= []DATAINI")
            End If

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DATAINI", ProsegurDbType.Data, objPeticion.FechaInical))

        End If

        If objPeticion.FechaFinal <> Nothing Then

            If filtros.Length > 0 Then
                filtros.Append(" AND MPG.FYH_ACTUALIZACION <= []DATAFIM")
            Else
                filtros.Append(" MPG.FYH_ACTUALIZACION <= []DATAFIM")
            End If

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DATAFIM", ProsegurDbType.Data, objPeticion.FechaFinal & " 23:59"))

        End If


        If (filtros.Length > 0) Then
            comando.CommandText &= " WHERE " & filtros.ToString
        End If

        '################## RETORNA OS MEDIO PAGOS ######################
        ' preparar a query
        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        ' executar query Medio Pago
        Dim dtQueryMedioPago As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        '#############################################################


        ' se encontrou algum registro
        If dtQueryMedioPago IsNot Nothing _
            AndAlso dtQueryMedioPago.Rows.Count > 0 Then

            Dim strOidMedioPago As String = String.Empty
            For Each dr As DataRow In dtQueryMedioPago.Rows
                'Preenche o objeto medio pago
                strOidMedioPago = String.Empty
                Dim objMedioPago As GetMediosPago.MedioPago
                objMedioPago = PopularMedioPagoIntegracion(dr, strOidMedioPago)
                objMedioPago.TerminosMedioPago = New GetMediosPago.TerminoMedioPagoColeccion
                '################## RETORNA OS TERMINOS ######################
                filtros = New StringBuilder
                filtros.Append(My.Resources.BuscaTerminoMedioPagoIntegracion.ToString)
                comando.CommandText = Util.PrepararQuery(filtros.ToString)
                'Limpa os parametros anteriores e adiciona o novo parâmetro
                comando.Parameters.Clear()
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, strOidMedioPago))
                'Executar query Terminos
                Dim dtQueryTerminos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
                '#############################################################


                If dtQueryTerminos IsNot Nothing _
                AndAlso dtQueryTerminos.Rows.Count > 0 Then

                    'Preenche o objeto Termino
                    Dim strOidTerminoMedioPago As String = String.Empty
                    For Each drTerminos As DataRow In dtQueryTerminos.Rows
                        'Preenche o objeto Termino pago
                        strOidTerminoMedioPago = String.Empty
                        Dim objTerminoMedioPago As GetMediosPago.TerminoMedioPago
                        objTerminoMedioPago = PopularTerminosMedioPagoIntegracion(drTerminos, strOidTerminoMedioPago)

                        '################## RETORNA OS VALORES DE TERMINOS ######################
                        filtros = New StringBuilder
                        filtros.Append(My.Resources.BuscaValorTerminoIntegracion.ToString)
                        comando.CommandText = Util.PrepararQuery(filtros.ToString)
                        'Limpa os parametros anteriores e adiciona o novo parâmetro
                        comando.Parameters.Clear()
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, strOidTerminoMedioPago))
                        ' executar query Terminos
                        Dim dtQueryValorTerminos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
                        '#############################################################

                        If dtQueryValorTerminos IsNot Nothing _
                        AndAlso dtQueryValorTerminos.Rows.Count > 0 Then
                            objTerminoMedioPago.ValoresTermino = New GetMediosPago.ValorTerminoColeccion
                            For Each drValorTermino As DataRow In dtQueryValorTerminos.Rows
                                'Preenche o objeto Valor Termino pago
                                Dim objValorTerminoMedioPago As GetMediosPago.ValorTermino
                                objValorTerminoMedioPago = PopularValoresTerminosIntegracion(drValorTermino)

                                'Adiciona o valor de termino na coleção de valores do objeto termino
                                objTerminoMedioPago.ValoresTermino.Add(objValorTerminoMedioPago)
                            Next

                        End If
                        'Adiciona o termino na coleção de terminos Medio Pago
                        objMedioPago.TerminosMedioPago.Add(objTerminoMedioPago)
                    Next
                End If

                'Adiciona o medio pago na coleção de medios de pago
                objMedioPagoCol.Add(objMedioPago)

            Next

        End If

        ' retornar objeto
        Return objMedioPagoCol
    End Function

    ''' <summary>
    ''' Função PopularCanal cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Private Shared Function PopularMedioPagoIntegracion(dr As DataRow, ByRef oidmedioPago As String) As GetMediosPago.MedioPago

        Dim objMedioPago As New GetMediosPago.MedioPago

        Util.AtribuirValorObjeto(oidmedioPago, dr("OID_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Codigo, dr("COD_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Descripcion, dr("DES_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.CodigoTipoMedioPago, dr("COD_TIPO_MEDIO_PAGO"), GetType(String))

        If dr("COD_TIPO_MEDIO_PAGO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("COD_TIPO_MEDIO_PAGO")) Then
            objMedioPago.DescripcionTipoMedioPago = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("COD_TIPO_MEDIO_PAGO"))
        End If

        Util.AtribuirValorObjeto(objMedioPago.CodigoDivisa, dr("COD_ISO_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.DescripcionDivisa, dr("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Observaciones, dr("OBS_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        ' retornar objeto
        Return objMedioPago

    End Function

    ''' <summary>
    ''' Função PopularCanal cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Private Shared Function PopularTerminosMedioPagoIntegracion(dr As DataRow, ByRef strOidTerminoMedioPago As String) As GetMediosPago.TerminoMedioPago

        Dim objTerminoMedioPago As New GetMediosPago.TerminoMedioPago

        Util.AtribuirValorObjeto(strOidTerminoMedioPago, dr("OID_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Codigo, dr("COD_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Descripcion, dr("DES_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Observaciones, dr("OBS_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.ValorInicial, dr("DES_VALOR_INICIAL"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Longitud, dr("NEC_LONGITUD"), GetType(Integer))
        Util.AtribuirValorObjeto(objTerminoMedioPago.CodigoFormato, dr("COD_FORMATO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.DescripcionFormato, dr("DES_FORMATO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.CodigoMascara, dr("COD_MASCARA"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.DescripcionMascara, dr("DES_MASCARA"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.ExpRegularMascara, dr("DES_EXP_REGULAR"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.CodigoAlgoritmo, dr("COD_ALGORITMO_VALIDACION"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.DescripcionAlgoritmo, dr("DES_ALGORITMO_VALIDACION"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoMedioPago.MostarCodigo, dr("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTerminoMedioPago.OrdenTermino, dr("NEC_ORDEN"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTerminoMedioPago.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        ' retornar objeto
        Return objTerminoMedioPago

    End Function

    ''' <summary>
    ''' Função PopularCanal cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Private Shared Function PopularValoresTerminosIntegracion(dr As DataRow) As GetMediosPago.ValorTermino

        Dim objValorTermino As New GetMediosPago.ValorTermino

        Util.AtribuirValorObjeto(objValorTermino.Codigo, dr("COD_VALOR"), GetType(String))
        Util.AtribuirValorObjeto(objValorTermino.Descripcion, dr("DES_VALOR"), GetType(String))
        Util.AtribuirValorObjeto(objValorTermino.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        ' retornar objeto
        Return objValorTermino

    End Function

#End Region

#Region "[GETPROCESO]"

    ''' <summary>
    ''' Retorna uma coleção de medios pago.
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <param name="oidSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] Criado 11/03/2009
    ''' </history>
    Public Shared Function RetornaMediosPago(oidProceso As String, oidSubCanal As String, oidProcesoSubCanal As String) As GetProceso.MedioPagoColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objColMedioPago As New GetProceso.MedioPagoColeccion

        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaMedioPago.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, oidProceso))

        Dim dtMedioPago As New DataTable

        dtMedioPago = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)


        'Verifica se o dtMedioPago contem registro.
        If dtMedioPago IsNot Nothing AndAlso dtMedioPago.Rows.Count > 0 Then

            Return PopularMediosPago(dtMedioPago, oidSubCanal, oidProcesoSubCanal)

        Else

            Return Nothing

        End If

    End Function

    Private Shared Function PopularMediosPago(dt As DataTable, oid_subCanal As String, oid_ProcesoSubCanal As String) As GetProceso.MedioPagoColeccion

        Dim objMediosPago As New GetProceso.MedioPagoColeccion()
        Dim objMedioPago As GetProceso.MedioPago
        Dim oidMedioPago As String = String.Empty

        For Each dr In dt.Rows

            objMedioPago = New GetProceso.MedioPago()

            Util.AtribuirValorObjeto(oidMedioPago, dr("OID_MEDIO_PAGO"), GetType(String))
            objMedioPago.Identificador = oidMedioPago
            Util.AtribuirValorObjeto(objMedioPago.Codigo, dr("COD_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.Descripcion, dr("DES_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.Observaciones, dr("OBS_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.EsMercancia, dr("BOL_MERCANCIA"), GetType(Boolean))
            Util.AtribuirValorObjeto(objMedioPago.CodigoTipo, dr("COD_TIPO_MEDIO_PAGO"), GetType(String))
            Util.AtribuirValorObjeto(objMedioPago.CodigoAccesoMedioPago, dr("COD_ACCESO"), GetType(String))

            If dr("COD_TIPO_MEDIO_PAGO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("COD_TIPO_MEDIO_PAGO")) Then
                objMedioPago.DescripcionTipo = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("COD_TIPO_MEDIO_PAGO"))
            End If

            Util.AtribuirValorObjeto(objMedioPago.ToleranciaParcialMin, dr("NUM_TOLERANCIA_PARCIAL_MIN"), GetType(Decimal))
            Util.AtribuirValorObjeto(objMedioPago.TolerenciaParcialMax, dr("NUM_TOLERANCIA_PARCIAL_MAX"), GetType(Decimal))
            Util.AtribuirValorObjeto(objMedioPago.ToleranciaBultoMin, dr("NUM_TOLERANCIA_BULTO_MIN"), GetType(Decimal))
            Util.AtribuirValorObjeto(objMedioPago.ToleranciaBultoMax, dr("NUM_TOLERANCIA_BULTO_MAX"), GetType(Decimal))
            Util.AtribuirValorObjeto(objMedioPago.ToleranciaRemesaMin, dr("NUM_TOLERANCIA_REMESA_MIN"), GetType(Decimal))
            Util.AtribuirValorObjeto(objMedioPago.ToleranciaRemesaMax, dr("NUM_TOLERANCIA_REMESA_MAX"), GetType(Decimal))

            If dr("COD_ISO_DIVISA") IsNot DBNull.Value AndAlso dr("DES_DIVISA") IsNot DBNull.Value Then
                objMedioPago.Divisa = New GetProceso.Divisa() With {.CodigoISO = dr("COD_ISO_DIVISA"), .Descripcion = dr("DES_DIVISA")}
            End If

            'Obtem os términos de médio de pago
            objMedioPago.TerminosMedioPago = AccesoDatos.TerminoMedioPago.RetornarTerminosMedioPago(oidMedioPago, oid_ProcesoSubCanal)

            objMediosPago.Add(objMedioPago)

            objMedioPago = Nothing

        Next

        Return objMediosPago

    End Function

#End Region

#End Region

#Region "DEMAIS METODOS"

    ''' <summary>
    ''' Verifica se o codigo de acesso existe.
    ''' </summary>
    ''' <param name="CodigoAcesso"></param>
    ''' <param name="IdentificadorDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarCodAccesoMedioPagoExiste(CodigoAcesso As String, CodigoMedioPago As String, _
                                                             IdentificadorDivisa As String) As Boolean

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.VerificarCodigoAccesoMedioPagoExiste
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ACCESO", ProsegurDbType.Identificador_Alfanumerico, CodigoAcesso))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))

        If Not String.IsNullOrEmpty(CodigoMedioPago) Then

            cmd.CommandText &= " AND COD_MEDIO_PAGO <> []COD_MEDIO_PAGO "
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, CodigoMedioPago))

        End If

        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, cmd)
    End Function

    ''' <summary>
    ''' Verifica se o código do Medio de Pago já existe
    ''' </summary>
    ''' <param name="PeticionVerificaCodigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    Public Shared Function VerificarCodigoMedioPago(PeticionVerificaCodigo As ContractoServicio.MedioPago.VerificarCodigoMedioPago.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, PeticionVerificaCodigo.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, PeticionVerificaCodigo.Divisa))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, PeticionVerificaCodigo.Tipo))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Verifica se existe algum medio pago com o codigo informado
    ''' </summary>
    ''' <param name="CodigoMedioPago">Codigo do medio pago</param>
    ''' <returns>True ou False</returns>
    ''' <history>
    ''' [vinicius.gama] Criado em 13/08/2010
    ''' </history>
    Public Shared Function VerificarSeHayMediosPagosConElCodigo(CodigoMedioPago As String)
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarSeHayMediosPagosConElCodigo.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, CodigoMedioPago))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Verifica se o código do Termino do Medio de Pago já existe
    ''' </summary>
    ''' <param name="PeticionVerificaCodigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    Public Shared Function VerificarCodigoTerminoMedioPago(PeticionVerificaCodigo As ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoTerminoMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, PeticionVerificaCodigo.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, PeticionVerificaCodigo.CodigoMedioPago))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Recupera os tipos medio pago e os medio pago de uma agrupação e uma divisa
    ''' </summary>
    ''' <param name="CodigoAgrupacion"></param>
    ''' <param name="CodigoDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Function ObterTipoMedioPagoEMedioPago(CodigoAgrupacion As String, _
                                                        CodigoDivisa As String) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.TipoMedioPagoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.getAgrupacionesDetailObterTipoMPeMP.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AGRUPACION", ProsegurDbType.Identificador_Alfanumerico, CodigoAgrupacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoDivisa))

        ' criar objeto denominacion coleccion
        Dim objTiposMedioPago As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.TipoMedioPagoColeccion

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            Dim codTipoMedioPagoAtual As String = String.Empty
            Dim objTipoMedioPago As ContractoServicio.Agrupacion.GetAgrupacionesDetail.TipoMedioPago = Nothing

            ' percorrer os registros encontrados
            For Each dr As DataRow In dtQuery.Rows

                If dr("COD_TIPO_MEDIO_PAGO") IsNot DBNull.Value Then

                    If codTipoMedioPagoAtual <> dr("COD_TIPO_MEDIO_PAGO") Then

                        If objTipoMedioPago IsNot Nothing Then
                            objTiposMedioPago.Add(objTipoMedioPago)
                        End If

                        ' popular o objeto
                        objTipoMedioPago = PopularTipoMedioPago(dr)

                        ' atribuir codigo atual
                        codTipoMedioPagoAtual = dr("COD_TIPO_MEDIO_PAGO")

                    End If


                    ' adicionar para coleção o objeto medio pago
                    objTipoMedioPago.MediosPago.Add(PopularMedioPago(dr))

                End If

            Next

            ' adiciona para coleção
            objTiposMedioPago.Add(objTipoMedioPago)

        End If

        ' retornar coleção de divisas
        Return objTiposMedioPago

    End Function

    ''' <summary>
    ''' Popula o objeto TipoMedioPago através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Private Shared Function PopularTipoMedioPago(dr As DataRow) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.TipoMedioPago

        Dim objTipoMedioPago As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.TipoMedioPago

        Util.AtribuirValorObjeto(objTipoMedioPago.Codigo, dr("COD_TIPO_MEDIO_PAGO"), GetType(String))

        If dr("COD_TIPO_MEDIO_PAGO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("COD_TIPO_MEDIO_PAGO")) Then
            objTipoMedioPago.Descripcion = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("COD_TIPO_MEDIO_PAGO"))
        End If

        objTipoMedioPago.MediosPago = New ContractoServicio.Agrupacion.GetAgrupacionesDetail.MedioPagoColeccion

        Return objTipoMedioPago

    End Function

    ''' <summary>
    ''' Popula o objeto MedioPago através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Private Shared Function PopularMedioPago(dr As DataRow) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.MedioPago

        Dim objMedioPago As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.MedioPago

        Util.AtribuirValorObjeto(objMedioPago.Codigo, dr("COD_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objMedioPago.Descripcion, dr("DES_MEDIO_PAGO"), GetType(String))

        Return objMedioPago

    End Function

    ''' <summary>
    ''' Obtém o oidmedio pago através do codigo da divisa, codigo do tipo medio pago e do codigo medio pago
    ''' </summary>
    ''' <param name="CodigoIsoDivisa"></param>
    ''' <param name="CodMedioPago"></param>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 04/02/2009 Criado
    ''' </history>
    Public Shared Function ObterOidMedioPago(CodigoIsoDivisa As String, _
                                             CodMedioPago As String, _
                                             CodTipoMedioPago As String) As String

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, CodMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, CodTipoMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoIsoDivisa))

        Dim OidMedioPago As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            OidMedioPago = dtQuery.Rows(0)("OID_MEDIO_PAGO")
        End If

        Return OidMedioPago

    End Function

    ''' <summary>
    ''' Verifica se alguma entidade utiliza o Medio de Pago em questão
    ''' </summary>
    ''' <param name="OidMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarEntidadesVigentesComMedioPago(OidMedioPago As String) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarEntidadesVigentesComMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, OidMedioPago))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta um Médio de Pago Existente
    ''' </summary>
    ''' <param name="objPeticaoMedioPago"></param>
    ''' <remarks></remarks>
    Public Shared Sub BajaMedioPago(objPeticaoMedioPago As ContractoServicio.MedioPago.SetMedioPago.MedioPago, codigoUsuario As String, OidMedioPago As String)

        Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaMedioPago.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros para exclusão do medio de pago
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticaoMedioPago.Vigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, OidMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objTransacao.AdicionarItemTransacao(comando)

        'Adiciona o comando para exclusão dos terminos        
        TerminoMedioPago.BajaTerminoMediosPagoPorMedioPago(OidMedioPago, codigoUsuario, objTransacao)

        objTransacao.RealizarTransacao()

    End Sub

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o Médio de Pago no DB.
    ''' </summary>
    ''' <param name="objMedioPago">Objeto Médio de Pago</param>
    ''' <param name="codigoUsuario">Usuário Responsável</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Public Shared Sub AltaMedioPago(objMedioPago As ContractoServicio.MedioPago.SetMedioPago.MedioPago, codigoUsuario As String)

        Try

            Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaMedioPago.ToString())
            comando.CommandType = CommandType.Text

            'Medio de Pago
            Dim oidMedioPago As String = Guid.NewGuid.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, oidMedioPago))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, objMedioPago.CodigoTipoMedioPago))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, Divisa.ObterOidDivisa(objMedioPago.CodigoDivisa)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, objMedioPago.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_MEDIO_PAGO", ProsegurDbType.Descricao_Longa, objMedioPago.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ACCESO", ProsegurDbType.Identificador_Alfanumerico, objMedioPago.CodigoAccesoMedioPago))

            If String.IsNullOrEmpty(objMedioPago.Observaciones) Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_MEDIO_PAGO", ProsegurDbType.Observacao_Longa, DBNull.Value))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_MEDIO_PAGO", ProsegurDbType.Observacao_Longa, objMedioPago.Observaciones))
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MERCANCIA", ProsegurDbType.Logico, objMedioPago.EsMercancia))

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objMedioPago.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            objtransacion.AdicionarItemTransacao(comando)

            'Adiciona os terminos de Pago e Valores de Termino
            If objMedioPago.TerminosMedioPago IsNot Nothing AndAlso objMedioPago.TerminosMedioPago.Count > 0 Then
                For Each objTermino As ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago In objMedioPago.TerminosMedioPago
                    TerminoMedioPago.AltaTerminoMedioPago(objTermino, codigoUsuario, oidMedioPago, objtransacion)
                Next
            End If

            objtransacion.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("014_msg_Erro_UKMedioPago"))
        End Try

    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização do Medio de Pago no DB.
    ''' </summary>
    ''' <param name="objMedioPago"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' [blcosta] 28/06/2010  modificado
    ''' </history>
    Public Shared Sub ActualizarMedioPago(objMedioPago As ContractoServicio.MedioPago.SetMedioPago.MedioPago, _
                                          codigoUsuario As String, _
                                          oidMedioPago As String, _
                                          ByRef objtransacion As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        query.Append("UPDATE gepr_tmedio_pago SET ")

        ' adicionar campos
        query.Append(Util.AdicionarCampoQuery("cod_tipo_medio_pago = []cod_tipo_medio_pago,", "cod_tipo_medio_pago", comando, objMedioPago.CodigoTipoMedioPago, ProsegurDbType.Identificador_Alfanumerico))
        query.Append(Util.AdicionarCampoQuery("oid_divisa = []oid_divisa,", "oid_divisa", comando, Divisa.ObterOidDivisa(objMedioPago.CodigoDivisa), ProsegurDbType.Objeto_Id))
        query.Append(Util.AdicionarCampoQuery("des_medio_pago = []des_medio_pago,", "des_medio_pago", comando, objMedioPago.Descripcion, ProsegurDbType.Descricao_Longa))
        query.Append(Util.AdicionarCampoQuery("obs_medio_pago = []obs_medio_pago,", "obs_medio_pago", comando, objMedioPago.Observaciones, ProsegurDbType.Observacao_Longa))
        query.Append(Util.AdicionarCampoQuery("bol_mercancia = []bol_mercancia,", "bol_mercancia", comando, objMedioPago.EsMercancia, ProsegurDbType.Logico))
        query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objMedioPago.Vigente, ProsegurDbType.Logico))
        query.Append(Util.AdicionarCampoQuery("cod_acceso = []cod_acceso,", "cod_acceso", comando, objMedioPago.CodigoAccesoMedioPago, ProsegurDbType.Identificador_Alfanumerico))

        query.Append("cod_usuario = []cod_usuario, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

        query.Append("fyh_actualizacion = []fyh_actualizacion ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

        ' adicionar clausula where
        query.Append("WHERE oid_medio_pago = []oid_medio_pago")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_medio_pago", ProsegurDbType.Objeto_Id, oidMedioPago))

        comando.CommandText = Util.PrepararQuery(query.ToString())
        comando.CommandType = CommandType.Text

        objtransacion.AdicionarItemTransacao(comando)

    End Sub

#End Region

End Class