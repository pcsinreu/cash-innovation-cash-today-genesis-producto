Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager

''' <summary>
''' Classe TerminosIac
''' </summary>
''' <remarks></remarks>
Public Class TerminosIac

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Obtém terminos através dos filtros informados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Public Shared Function getTerminos(ByVal objPeticion As ContractoServicio.TerminosIac.GetTerminosIac.Peticion) As ContractoServicio.TerminosIac.GetTerminosIac.TerminoIacColeccion

        ' criar objeto agrupaciones
        Dim objTerminos As New ContractoServicio.TerminosIac.GetTerminosIac.TerminoIacColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.getTerminos.ToString)
        ' adicionar parametro do comando acima
        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "BOL_VIGENTE", DbType.Boolean, objPeticion.VigenteTermino))

        query.Append(" AND TRM.BOL_MOSTRAR_CODIGO = []BOL_MOSTRAR_CODIGO ")
        ' adicionar parametro do comando acima
        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "BOL_MOSTRAR_CODIGO", DbType.Boolean, objPeticion.MostrarCodigo))

        ' montar clausula in Código Termino
        query.Append(Util.MontarClausulaIn(objPeticion.CodigoTermino, "COD_TERMINO", comando, "AND", "TRM"))

        ' montar clausula IN Descrição Formato
        query.Append(Util.MontarClausulaIn(objPeticion.DescripcionTermino, "DES_TERMINO", comando, "AND", "TRM"))

        ' montar clausula IN Formato
        query.Append(PrepararFiltroFormato(objPeticion.DescripcionFormato, comando))

        ' preparar a query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            For Each dr As DataRow In dtQuery.Rows

                objTerminos.Add(PopularGetFormatos(dr))

            Next

        End If

        ' retornar objeto preenchido
        Return objTerminos

    End Function

    ''' <summary>
    ''' Prepara o filtro formato
    ''' </summary>
    ''' <param name="DescricaoFormato"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 12/02/2009 Criado
    ''' </history>
    Private Shared Function PrepararFiltroFormato(ByVal DescricaoFormato As List(Of String), _
                                                  ByRef comando As IDbCommand) As String

        ' criar filtro
        Dim filtro As New StringBuilder

        ' remover valores vazios
        DescricaoFormato = Util.RemoverItensVazios(DescricaoFormato)

        ' criar filtro por codigo divisa
        If DescricaoFormato IsNot Nothing _
            AndAlso DescricaoFormato.Count > 0 Then

            filtro.Append(" AND TRM.OID_FORMATO IN")
            filtro.Append(" (SELECT OID_FORMATO")
            filtro.Append(" FROM GEPR_TFORMATO TFR")
            filtro.Append(" INNER JOIN GEPR_TTERMINO TRN ON TFR.OID_FORMATO = TRN.OID_FORMATO")
            filtro.Append(Util.MontarClausulaIn(DescricaoFormato, "DES_FORMATO", comando, "WHERE", "TFR", "FORMATO"))
            filtro.Append(")")

        End If

        ' retornar filtro
        Return filtro.ToString

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
    Private Shared Function PopularGetFormatos(ByVal dr As DataRow) As ContractoServicio.TerminosIac.GetTerminosIac.TerminoIac

        ' criar objeto termino Iac
        Dim objTerminoIac As New ContractoServicio.TerminosIac.GetTerminosIac.TerminoIac

        If Not String.IsNullOrEmpty(dr("COD_TERMINO")) Then
            objTerminoIac.Codigo = dr("COD_TERMINO")
        End If

        If Not String.IsNullOrEmpty(dr("DES_TERMINO")) Then
            objTerminoIac.Descripcion = dr("DES_TERMINO")
        End If

        If Not String.IsNullOrEmpty(dr("OBS_TERMINO")) Then
            objTerminoIac.Observacion = dr("OBS_TERMINO")
        End If

        If Not String.IsNullOrEmpty(dr("BOL_VIGENTE")) Then
            objTerminoIac.Vigente = CBool(dr("BOL_VIGENTE"))
        End If

        Return objTerminoIac

    End Function

#End Region

#Region "[OUTROS]"

    '    ''' <summary>
    '    ''' Verifica se o código da agrupacion já existe na base de dados
    '    ''' </summary>
    '    ''' <param name="CodigoTermino"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 02/02/2009 Criado
    '    ''' </history>
    '    Public Shared Function VerificarCodigoTermino(ByVal CodigoTermino As String) As Boolean

    '        ' criar comando
    '        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '        ' obter query
    '        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoAgrupacion.ToString)
    '        comando.CommandType = CommandType.Text

    '        ' setar parametros
    '        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_AGRUPACION", DbType.String, CodigoAgrupacion))

    '        ' executar query e retornar resultado
    '        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    '    End Function

    '    ''' <summary>
    '    ''' Verifica se a descrição da agrupacion já existe na base de dados
    '    ''' </summary>
    '    ''' <param name="DescripcionAgrupacion"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 02/02/2009 Criado
    '    ''' </history>
    '    Public Shared Function VerificarDescripcionAgrupacion(ByVal DescripcionAgrupacion As String) As Boolean

    '        ' criar comando
    '        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '        ' obter query
    '        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionAgrupacion.ToString)
    '        comando.CommandType = CommandType.Text

    '        ' setar parametros
    '        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "DES_AGRUPACION", DbType.String, DescripcionAgrupacion))

    '        ' executar query e retornar resultado
    '        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    '    End Function

    '    ''' <summary>
    '    ''' Obtém a agrupaciones
    '    ''' </summary>
    '    ''' <param name="CodigosAgrupacion"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 03/02/2009 Criado
    '    ''' </history>
    '    Public Shared Function ObterAgrupaciones(ByVal CodigosAgrupacion As List(Of String)) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.AgrupacionColeccion

    '        ' criar objeto agrupaciones
    '        Dim objAgrupaciones As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.AgrupacionColeccion

    '        ' criar comando
    '        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '        ' obter query
    '        Dim query As New StringBuilder
    '        query.Append(My.Resources.getAgrupacionesDetailObterAgrupaciones.ToString())

    '        ' criar filtro IN
    '        query.Append(Util.MontarClausulaIn(CodigosAgrupacion, "COD_AGRUPACION", comando, "WHERE"))

    '        ' preparar a query
    '        comando.CommandText = Util.PrepararQuery(query.ToString)
    '        comando.CommandType = CommandType.Text

    '        ' executar query
    '        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    '        ' se encontrou algum registro
    '        If dtQuery IsNot Nothing _
    '            AndAlso dtQuery.Rows.Count > 0 Then

    '            For Each dr As DataRow In dtQuery.Rows

    '                objAgrupaciones.Add(PopularObterAgrupaciones(dr))

    '            Next

    '        End If

    '        ' retornar objeto preenchido
    '        Return objAgrupaciones

    '    End Function

    '    ''' <summary>
    '    ''' Popula um objeto agrupacion através de um datarow
    '    ''' </summary>
    '    ''' <param name="dr"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 03/02/2009 Criado
    '    ''' </history>
    '    Private Shared Function PopularObterAgrupaciones(ByVal dr As DataRow) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Agrupacion

    '        ' criar objeto agrupacion
    '        Dim objAgrupacion As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Agrupacion

    '        If Not String.IsNullOrEmpty(dr("cod_agrupacion")) Then
    '            objAgrupacion.Codigo = dr("cod_agrupacion")
    '        End If

    '        If Not String.IsNullOrEmpty(dr("des_agrupacion")) Then
    '            objAgrupacion.Descripcion = dr("des_agrupacion")
    '        End If

    '        If Not String.IsNullOrEmpty(dr("obs_agrupacion")) Then
    '            objAgrupacion.Observacion = dr("obs_agrupacion")
    '        End If

    '        If Not String.IsNullOrEmpty(dr("bol_vigente")) Then
    '            objAgrupacion.Vigente = Convert.ToBoolean(dr("bol_vigente"))
    '        End If

    '        Return objAgrupacion

    '    End Function

    '    ''' <summary>
    '    ''' Obtém OidAgrupacion através de um código agrupacion
    '    ''' </summary>
    '    ''' <param name="CodAgrupacion"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 05/02/2009 Criado
    '    ''' </history>
    '    Private Shared Function ObterOidAgrupacion(ByVal CodAgrupacion As String) As String

    '        ' criar comando
    '        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '        ' obter comando sql
    '        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidAgrupacion.ToString)
    '        comando.CommandType = CommandType.Text

    '        ' criar parameter
    '        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_AGRUPACION", DbType.String, CodAgrupacion))

    '        Dim OidAgrupacion As String = String.Empty

    '        ' executar query
    '        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    '        If dtQuery.Rows.Count > 0 Then
    '            OidAgrupacion = dtQuery.Rows(0)("OID_AGRUPACION")
    '        End If

    '        Return OidAgrupacion

    '    End Function

    '    ''' <summary>
    '    ''' Verifica se Processo esta vigente utilizando a agrupacion informada
    '    ''' Retorna True caso encontre alguma entidade vigente que usa a agrupacion
    '    ''' </summary>
    '    ''' <param name="CodigoAgrupacion"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 05/02/2009 Criado
    '    ''' </history>
    '    Public Shared Function VerificarEntidadesVigentesComDivisa(ByVal CodigoAgrupacion As String) As Boolean

    '        ' criar comando
    '        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '        ' obter query
    '        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarEntidadesVigentesComAgrupacion.ToString)
    '        comando.CommandType = CommandType.Text

    '        ' setar parametros
    '        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_AGRUPACION", DbType.String, CodigoAgrupacion))

    '        ' executar query e retornar resultado
    '        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    '    End Function

    '#End Region

    '#Region "[INSERIR]"

    '    ''' <summary>
    '    ''' Insere Agrupacion e os relacionamentos AGRUPACION X DIVISA e AGRUPACION X MEDIO PAGO
    '    ''' </summary>
    '    ''' <param name="objAgrupacion"></param>
    '    ''' <param name="CodigoUsuario"></param>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 05/02/2009 Criado
    '    ''' </history>
    '    Public Shared Sub AltaAgrupacion(ByVal objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion, _
    '                                     ByVal CodigoUsuario As String)

    '        Try

    '            ' criar transacao
    '            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

    '            ' criar comando
    '            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '            ' obter query
    '            comando.CommandText = Util.PrepararQuery(My.Resources.AltaAgrupacion.ToString)
    '            comando.CommandType = CommandType.Text

    '            ' gerar guid
    '            Dim OidAgrupacion As String = Guid.NewGuid().ToString

    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "OID_AGRUPACION", DbType.String, OidAgrupacion))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_AGRUPACION", DbType.String, objAgrupacion.Codigo))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "DES_AGRUPACION", DbType.String, objAgrupacion.Descripcion))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "OBS_AGRUPACION", DbType.AnsiString, objAgrupacion.Observacion))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "BOL_VIGENTE", DbType.Boolean, objAgrupacion.Vigente))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_USUARIO", DbType.String, CodigoUsuario))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", DbType.DateTime, DateTime.Now))

    '            ' adicionar comando para transação
    '            objTransacao.AdicionarItemTransacao(comando)

    '            ' obter todas as divisas
    '            Dim dtdivisas As DataTable = AccesoDatos.Divisa.ObterDivisas(True)

    '            ' caso existam Efetivos para agrupacao
    '            If objAgrupacion.Efectivos IsNot Nothing _
    '                AndAlso objAgrupacion.Efectivos.Count > 0 Then

    '                ' para cada efetivo da agrupacao
    '                For Each objEfetivo As ContractoServicio.Agrupacion.SetAgrupaciones.Efectivo In objAgrupacion.Efectivos

    '                    ' obter oid da divisa
    '                    Dim dr() As DataRow = dtdivisas.Select(String.Format("COD_ISO_DIVISA = '{0}'", objEfetivo.CodigoIsoDivisa))

    '                    ' se encontrou algum registro
    '                    If dr.Count > 0 Then

    '                        ' adicionar relacionamento DIVISA X AGRUPACION
    '                        AccesoDatos.DivisaPorAgrupacion.AltaDivisaPorAgrupacion(dr(0)("OID_DIVISA"), OidAgrupacion, CodigoUsuario, objTransacao)

    '                    End If

    '                Next

    '            End If

    '            ' caso existam Resto Medio Pago
    '            If objAgrupacion.MediosPago IsNot Nothing _
    '                AndAlso objAgrupacion.MediosPago.Count > 0 Then

    '                ' para cada medio pago
    '                For Each objMedioPago In objAgrupacion.MediosPago

    '                    ' obter oid medio pago
    '                    Dim OidMedioPago As String = AccesoDatos.MedioPago.ObterOidMedioPago(objMedioPago.CodigoIsoDivisa, _
    '                                                                                         objMedioPago.CodigoMedioPago, _
    '                                                                                         objMedioPago.CodigoTipoMedioPago)

    '                    ' se o oid não for vazio
    '                    If Not OidMedioPago.Equals(String.Empty) Then

    '                        ' adicionar relacionamento MEDIO PAGO X AGRUPACION
    '                        AccesoDatos.MedioPagoPorAgrupacion.AltaMedioPagoPorAgrupacion(OidMedioPago, OidAgrupacion, CodigoUsuario, objTransacao)

    '                    End If

    '                Next

    '            End If

    '            ' realizar a transação
    '            objTransacao.RealizarTransacao()

    '        Catch ex As Exception

    '            Excepcion.Util.Tratar(ex, Traduzir("003_msg_Erro_UKAgrupacion"))

    '        End Try

    '    End Sub

    '#End Region

    '#Region "[ATUALIZAR]"

    '    ''' <summary>
    '    ''' Atualiza agrupacion e refaz os relacionamento AGRUPACION X DIVISA e AGRUPACION X MEDIO PAGO
    '    ''' </summary>
    '    ''' <param name="objAgrupacion"></param>
    '    ''' <param name="CodigoUsuario"></param>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 05/02/2009 Criado
    '    ''' </history>
    '    Public Shared Sub ActualizarAgrupacion(ByVal objAgrupacion As ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion, _
    '                                           ByVal CodigoUsuario As String)

    '        Try

    '            ' criar transacao
    '            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

    '            ' criar comando
    '            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '            ' obter query
    '            comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarAgrupacion.ToString)
    '            comando.CommandType = CommandType.Text

    '            ' obter oidAgrupacion
    '            Dim OidAgrupacion As String = ObterOidAgrupacion(objAgrupacion.Codigo)

    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_AGRUPACION", DbType.String, objAgrupacion.Codigo))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "DES_AGRUPACION", DbType.String, objAgrupacion.Descripcion))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "OBS_AGRUPACION", DbType.AnsiString, objAgrupacion.Observacion))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "BOL_VIGENTE", DbType.Boolean, objAgrupacion.Vigente))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_USUARIO", DbType.String, CodigoUsuario))
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", DbType.DateTime, DateTime.Now))

    '            ' adicionar comando para transação
    '            objTransacao.AdicionarItemTransacao(comando)

    '            ' remover todos os relacionamentos AGRUPACION X DIVISA
    '            DivisaPorAgrupacion.BorrarDivisasPorAgrupacion(OidAgrupacion, objTransacao)

    '            ' remover todos os relacionamentos AGRUPACION X MEDIO PAGO
    '            MedioPagoPorAgrupacion.BorrarMediosPagoPorAgrupacion(OidAgrupacion, objTransacao)

    '            ' obter todas as divisas
    '            Dim dtdivisas As DataTable = AccesoDatos.Divisa.ObterDivisas(True)

    '            ' caso existam Efetivos para agrupacao
    '            If objAgrupacion.Efectivos IsNot Nothing _
    '                AndAlso objAgrupacion.Efectivos.Count > 0 Then

    '                ' para cada efetivo da agrupacao
    '                For Each objEfetivo As ContractoServicio.Agrupacion.SetAgrupaciones.Efectivo In objAgrupacion.Efectivos

    '                    ' obter oid da divisa
    '                    Dim dr() As DataRow = dtdivisas.Select(String.Format("COD_ISO_DIVISA = '{0}'", objEfetivo.CodigoIsoDivisa))

    '                    ' se encontrou algum registro
    '                    If dr.Count > 0 Then

    '                        ' adicionar relacionamento DIVISA X AGRUPACION
    '                        AccesoDatos.DivisaPorAgrupacion.AltaDivisaPorAgrupacion(dr(0)("OID_DIVISA"), OidAgrupacion, CodigoUsuario, objTransacao)

    '                    End If

    '                Next

    '            End If

    '            ' caso existam Resto Medio Pago
    '            If objAgrupacion.MediosPago IsNot Nothing _
    '                AndAlso objAgrupacion.MediosPago.Count > 0 Then

    '                ' para cada medio pago
    '                For Each objMedioPago In objAgrupacion.MediosPago

    '                    ' obter oid medio pago
    '                    Dim OidMedioPago As String = AccesoDatos.MedioPago.ObterOidMedioPago(objMedioPago.CodigoIsoDivisa, _
    '                                                                                         objMedioPago.CodigoMedioPago, _
    '                                                                                         objMedioPago.CodigoTipoMedioPago)

    '                    ' se o oid não for vazio
    '                    If Not OidMedioPago.Equals(String.Empty) Then

    '                        ' adicionar relacionamento MEDIO PAGO X AGRUPACION
    '                        AccesoDatos.MedioPagoPorAgrupacion.AltaMedioPagoPorAgrupacion(OidMedioPago, OidAgrupacion, CodigoUsuario, objTransacao)

    '                    End If

    '                Next

    '            End If

    '            ' realizar a transação
    '            objTransacao.RealizarTransacao()

    '        Catch ex As Exception

    '            Excepcion.Util.Tratar(ex, Traduzir("003_msg_Erro_UKAgrupacion"))

    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Efetua a alta ou a baixa de uma agrupacion
    '    ''' </summary>
    '    ''' <param name="CodigoAgrupacion"></param>
    '    ''' <param name="CodigoUsuario"></param>
    '    ''' <param name="EsAlta"></param>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 05/02/2009 Criado
    '    ''' </history>
    '    Public Shared Sub AltaBajaAgrupacion(ByVal CodigoAgrupacion As String, _
    '                                         ByVal CodigoUsuario As String, _
    '                                         ByVal EsAlta As Boolean)

    '        ' criar comando
    '        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '        ' obter query
    '        comando.CommandText = Util.PrepararQuery(My.Resources.AltaBajaAgrupacion.ToString)
    '        comando.CommandType = CommandType.Text

    '        ' setar parameters
    '        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "BOL_VIGENTE", DbType.Boolean, EsAlta))
    '        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_USUARIO", DbType.String, CodigoUsuario))
    '        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", DbType.DateTime, DateTime.Now))
    '        comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, "COD_AGRUPACION", DbType.String, CodigoAgrupacion))

    '        ' executar comando
    '        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    '    End Sub

    '#End Region

    '#Region "[FILTROS]"

    '    ''' <summary>
    '    ''' Prepara o filtro ticket através dos codigos ou divisas
    '    ''' </summary>
    '    ''' <param name="CodigoDivisa"></param>
    '    ''' <param name="comando"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 12/02/2009 Criado
    '    ''' </history>
    '    Private Shared Function PrepararFiltroEfetivo(ByVal CodigoDivisa As List(Of String), _
    '                                                  ByRef comando As IDbCommand) As String

    '        ' criar filtro
    '        Dim filtro As New StringBuilder

    '        ' remover valores vazios
    '        CodigoDivisa = Util.RemoverItensVazios(CodigoDivisa)

    '        ' criar filtro por codigo divisa
    '        If CodigoDivisa IsNot Nothing _
    '            AndAlso CodigoDivisa.Count > 0 Then

    '            filtro.Append(" AND AGR.OID_AGRUPACION IN")
    '            filtro.Append(" (SELECT OID_AGRUPACION")
    '            filtro.Append(" FROM GEPR_TDIVISA_POR_AGRUPACION DIVAGR")
    '            filtro.Append(" INNER JOIN GEPR_TDIVISA DIV ON DIVAGR.OID_DIVISA = DIV.OID_DIVISA")
    '            filtro.Append(Util.MontarClausulaIn(CodigoDivisa, "COD_ISO_DIVISA", comando, "WHERE", "DIV", "EFETIVO"))
    '            filtro.Append(")")

    '        End If

    '        ' retornar filtro
    '        Return filtro.ToString

    '    End Function

    '    ''' <summary>
    '    ''' Prepara o filtro ticket através dos codigos ou divisas
    '    ''' </summary>
    '    ''' <param name="CodigoTicket"></param>
    '    ''' <param name="DivisaTicket"></param>
    '    ''' <param name="comando"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 30/01/2009 Criado
    '    ''' </history>
    '    Private Shared Function PrepararFiltroTicketOtroValorCheque(ByVal Codigo As List(Of String), _
    '                                                                ByVal Divisa As List(Of String), _
    '                                                                ByVal TpFiltro As TipoFiltro, _
    '                                                                ByRef comando As IDbCommand) As String

    '        ' criar filtro
    '        Dim filtro As New StringBuilder
    '        Dim CodTipoMedioPago As String = String.Empty
    '        Dim TipoBusca As String = String.Empty

    '        ' obter o tipo da consulta
    '        If TpFiltro = Agrupacion.TipoFiltro.Ticket Then
    '            CodTipoMedioPago = AppSettings("get_agrupacion_ticket")
    '            TipoBusca = "T"
    '        ElseIf TpFiltro = Agrupacion.TipoFiltro.OtrosValores Then
    '            CodTipoMedioPago = AppSettings("get_agrupacion_otrosvalores")
    '            TipoBusca = "O"
    '        ElseIf TpFiltro = Agrupacion.TipoFiltro.Cheque Then
    '            CodTipoMedioPago = AppSettings("get_agrupacion_cheque")
    '            TipoBusca = "C"
    '        End If

    '        ' remover valores vazios
    '        Codigo = Util.RemoverItensVazios(Codigo)
    '        Divisa = Util.RemoverItensVazios(Divisa)

    '        ' criar filtro por codigo
    '        If Codigo IsNot Nothing _
    '            AndAlso Codigo.Count > 0 Then

    '            filtro.Append(" AND AGR.OID_AGRUPACION IN")
    '            filtro.Append(" (SELECT OID_AGRUPACION")
    '            filtro.Append(" FROM GEPR_TMEDIO_PAGO_POR_AGRUPACIO MPAGR")
    '            filtro.Append(" INNER JOIN GEPR_TMEDIO_PAGO MP ON MPAGR.OID_MEDIO_PAGO = MP.OID_MEDIO_PAGO")
    '            filtro.Append(" INNER JOIN GEPR_TTIPO_MEDIO_PAGO TMP ON MP.OID_TIPO_MEDIO_PAGO = TMP.OID_TIPO_MEDIO_PAGO")
    '            filtro.Append(" WHERE TMP.COD_TIPO_MEDIO_PAGO = []" & TipoBusca & "COD_TIPO_MEDIO_PAGO ")
    '            filtro.Append(Util.MontarClausulaIn(Codigo, "COD_MEDIO_PAGO", comando, "AND", "MP", TipoBusca))
    '            filtro.Append(")")
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, TipoBusca & "COD_TIPO_MEDIO_PAGO", DbType.String, CodTipoMedioPago))

    '        End If

    '        ' criar filtro por divisa
    '        If Divisa IsNot Nothing _
    '            AndAlso Divisa.Count > 0 Then

    '            filtro.Append(" AND AGR.OID_AGRUPACION IN")
    '            filtro.Append(" (SELECT OID_AGRUPACION")
    '            filtro.Append(" FROM GEPR_TMEDIO_PAGO_POR_AGRUPACIO MPAGR")
    '            filtro.Append(" INNER JOIN GEPR_TMEDIO_PAGO MP ON MPAGR.OID_MEDIO_PAGO = MP.OID_MEDIO_PAGO")
    '            filtro.Append(" INNER JOIN GEPR_TTIPO_MEDIO_PAGO TMP ON MP.OID_TIPO_MEDIO_PAGO = TMP.OID_TIPO_MEDIO_PAGO")
    '            filtro.Append(" INNER JOIN GEPR_TDIVISA DIV ON MP.OID_DIVISA = DIV.OID_DIVISA")
    '            filtro.Append(" WHERE TMP.COD_TIPO_MEDIO_PAGO = []" & TipoBusca & "DIVCOD_TIPO_MEDIO_PAGO ")
    '            filtro.Append(Util.MontarClausulaIn(Divisa, "COD_ISO_DIVISA", comando, "AND", "DIV", TipoBusca))
    '            filtro.Append(")")
    '            comando.Parameters.Add(Util.PreencherParameter(Constantes.CONEXAO_GE, TipoBusca & "DIVCOD_TIPO_MEDIO_PAGO", DbType.String, CodTipoMedioPago))

    '        End If

    '        ' retornar filtro
    '        Return filtro.ToString

    '    End Function

#End Region

End Class