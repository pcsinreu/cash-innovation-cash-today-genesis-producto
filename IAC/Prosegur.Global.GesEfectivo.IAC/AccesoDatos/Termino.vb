Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis


Public Class Termino

#Region "[CONSULTAR]"

#Region "GETTERMINOS"

    ''' <summary>
    ''' Obtém terminos através dos filtros informados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    Public Shared Function GetTerminos(objPeticion As ContractoServicio.TerminoIac.GetTerminoIac.Peticion) As ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion

        ' criar objeto agrupaciones
        Dim objTerminos As New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Obter query genérica
        Dim query As New StringBuilder
        query.Append(My.Resources.GetTermino.ToString)

        'Adiciona Where caso algum parâmetro seja passado
        Dim clausulaWhere As New CriterioColecion

        ' Validação Campo Vigente
        If objPeticion.VigenteTermino IsNot Nothing Then
            clausulaWhere.addCriterio("AND", " TRM.BOL_VIGENTE = []BOL_VIGENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.VigenteTermino))
        End If

        ' Validação Campo Mostra Código
        If objPeticion.MostrarCodigo IsNot Nothing Then
            clausulaWhere.addCriterio("AND", " TRM.BOL_MOSTRAR_CODIGO = []BOL_MOSTRAR_CODIGO ")
            ' adicionar parametro do comando acima
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MOSTRAR_CODIGO", ProsegurDbType.Logico, objPeticion.MostrarCodigo))
        End If

        ' Validação Campo CodigoTermino
        If objPeticion.CodigoTermino IsNot Nothing AndAlso objPeticion.CodigoTermino.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaLikeUpper(objPeticion.CodigoTermino, "COD_TERMINO", comando, String.Empty, "TRM"))
        End If


        ' Validação Campo DescricaoFormato
        If objPeticion.DescripcionFormato IsNot Nothing AndAlso objPeticion.DescripcionFormato.Count > 0 Then
            clausulaWhere.addCriterio("AND", PrepararFiltroFormato(objPeticion.DescripcionFormato, comando))
        End If

        'Adiciona a clausula Where
        If clausulaWhere.Count > 0 Then
            query.Append(Util.MontarClausulaWhere(clausulaWhere))

            ' Validação Campo DescricaoTermino
            If objPeticion.DescripcionTermino IsNot Nothing AndAlso objPeticion.DescripcionTermino.Count > 0 Then
                query.Append(Util.MontarClausulaLikeUpper(objPeticion.DescripcionTermino, "DES_TERMINO", comando, "AND", "TRM"))
            End If
        Else
            ' Validação Campo DescricaoTermino
            If objPeticion.DescripcionTermino IsNot Nothing AndAlso objPeticion.DescripcionTermino.Count > 0 Then
                query.Append(Util.MontarClausulaLikeUpper(objPeticion.DescripcionTermino, "DES_TERMINO", comando, "WHERE", "TRM"))
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

                objTerminos.Add(PopularGetTerminos(dr))

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
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    Private Shared Function PrepararFiltroFormato(DescricaoFormato As List(Of String), _
                                                  ByRef comando As IDbCommand) As String

        ' criar filtro
        Dim filtro As New StringBuilder

        ' remover valores vazios
        DescricaoFormato = Util.RemoverItensVazios(DescricaoFormato)

        ' criar filtro por codigo divisa
        If DescricaoFormato IsNot Nothing _
            AndAlso DescricaoFormato.Count > 0 Then

            filtro.Append(" TRM.OID_FORMATO IN")
            filtro.Append(" (SELECT TFR.OID_FORMATO")
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
    Private Shared Function PopularGetTerminos(dr As DataRow) As ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        ' criar objeto termino Iac
        Dim objTerminoIac As New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        Util.AtribuirValorObjeto(objTerminoIac.Codigo, dr("COD_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoIac.Descripcion, dr("DES_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoIac.Observacion, dr("OBS_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoIac.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Return objTerminoIac

    End Function

#End Region

#Region "GETTERMINODETAIL"

    ''' <summary>
    ''' Obtém o detalhe dos terminos através dos filtros informados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    Public Shared Function GetTerminoDetail(objPeticion As ContractoServicio.TerminoIac.GetTerminoDetailIac.Peticion) As ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIacColeccion

        ' criar objeto agrupaciones
        Dim objTerminos As New ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIacColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Obter query genérica
        Dim query As New StringBuilder
        query.Append(My.Resources.GetTerminoDetail.ToString)

        'Adiciona Where caso algum parâmetro seja passado
        Dim clausulaWhere As New CriterioColecion

        ' Validação Campo CodigoTermino
        If objPeticion.CodigoTermino IsNot Nothing AndAlso objPeticion.CodigoTermino.Count > 0 Then
            clausulaWhere.addCriterio("AND", Util.MontarClausulaIn(objPeticion.CodigoTermino, "COD_TERMINO", comando, String.Empty, "TER"))
        End If

        'Adiciona a clausula Where
        If clausulaWhere.Count > 0 Then
            query.Append(Util.MontarClausulaWhere(clausulaWhere))
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

                objTerminos.Add(PopularGetTerminoDetail(dr))

            Next

        End If

        ' retornar objeto preenchido
        Return objTerminos

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
    Private Shared Function PopularGetTerminoDetail(dr As DataRow) As ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIac

        ' criar objeto termino Iac
        Dim objTerminoDetailIac As New ContractoServicio.TerminoIac.GetTerminoDetailIac.TerminoDetailIac

        'Termino
        Util.AtribuirValorObjeto(objTerminoDetailIac.Codigo, dr("COD_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoDetailIac.Descripcion, dr("DES_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoDetailIac.Observacion, dr("OBS_TERMINO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoDetailIac.Longitud, dr("NEC_LONGITUD"), GetType(Integer))
        Util.AtribuirValorObjeto(objTerminoDetailIac.MostrarCodigo, dr("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTerminoDetailIac.ValoresPosibles, dr("BOL_VALORES_POSIBLES"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTerminoDetailIac.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTerminoDetailIac.AceptarDigitiacion, dr("BOL_ACEPTAR_DIGITACION"), GetType(Boolean))

        'Formato
        Util.AtribuirValorObjeto(objTerminoDetailIac.CodigoFormato, dr("COD_FORMATO"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoDetailIac.DescripcionFormato, dr("DES_FORMATO"), GetType(String))

        'Algoritmo
        Util.AtribuirValorObjeto(objTerminoDetailIac.CodigoAlgoritmo, dr("COD_ALGORITMO_VALIDACION"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoDetailIac.DescripcionAlgoritmo, dr("DES_ALGORITMO_VALIDACION"), GetType(String))

        'Mascara
        Util.AtribuirValorObjeto(objTerminoDetailIac.CodigoMascara, dr("COD_MASCARA"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoDetailIac.DescripcionMascara, dr("DES_MASCARA"), GetType(String))
        Util.AtribuirValorObjeto(objTerminoDetailIac.ExpRegularMascara, dr("DES_EXP_REGULAR"), GetType(String))

        Return objTerminoDetailIac

    End Function

#End Region

#Region "[DEMAIS MÉTODOS]"

    ''' <summary>
    ''' Verifica se alguma entidade utiliza o termino em questão
    ''' </summary>
    ''' <param name="CodigoTermino"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarEntidadesVigentesComTermino(CodigoTermino As String) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarEntidadesVigentesComTermino.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, CodigoTermino))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Verifica se o código do termino já existe
    ''' </summary>
    ''' <param name="PeticionVerificaCodigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarCodigoTermino(PeticionVerificaCodigo As ContractoServicio.TerminoIac.VerificarCodigoTermino.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoTermino.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, PeticionVerificaCodigo.Codigo))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Verifica se a descrição do termino já existe
    ''' </summary>
    ''' <param name="PeticionVerificaDescricao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarDescricaoTermino(PeticionVerificaDescricao As ContractoServicio.TerminoIac.VerificarDescripcionTermino.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionTermino.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TERMINO", ProsegurDbType.Descricao_Longa, PeticionVerificaDescricao.Descripcion))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

#End Region

#End Region

#Region "[DELETE]"

    ''' <summary>
    ''' Deleta um termino existente
    ''' </summary>
    ''' <param name="objPeticaoTermino"></param>
    ''' <remarks></remarks>
    Public Shared Sub BajaTermino(objPeticaoTermino As ContractoServicio.TerminoIac.SetTerminoIac.Peticion)

        Dim objTransacao As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaTermino.ToString())
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticaoTermino.Vigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, objPeticaoTermino.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, objPeticaoTermino.CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objTransacao.AdicionarItemTransacao(comando)
        objTransacao.RealizarTransacao()

    End Sub

#End Region

#Region "[INSERT]"

    ''' <summary>
    ''' 'Inseri um novo término
    ''' </summary>
    ''' <param name="objPeticaoTermino"></param>
    ''' <remarks></remarks>
    Public Shared Sub AltaTermino(objPeticaoTermino As ContractoServicio.TerminoIac.SetTerminoIac.Peticion)

        Try

            Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaTermino.ToString())
            comando.CommandType = CommandType.Text

            Dim oidTermino As String = Guid.NewGuid.ToString
            'Termino
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, oidTermino))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, objPeticaoTermino.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TERMINO", ProsegurDbType.Descricao_Longa, objPeticaoTermino.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_TERMINO", ProsegurDbType.Observacao_Longa, objPeticaoTermino.Observacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_LONGITUD", ProsegurDbType.Inteiro_Curto, objPeticaoTermino.Longitud))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MOSTRAR_CODIGO", ProsegurDbType.Logico, objPeticaoTermino.MostrarCodigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VALORES_POSIBLES", ProsegurDbType.Logico, objPeticaoTermino.AdmiteValoresPosibles))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticaoTermino.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACEPTAR_DIGITACION", ProsegurDbType.Logico, objPeticaoTermino.AceptarDigitacion))

            'Formato
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_FORMATO", ProsegurDbType.Objeto_Id, Formato.ObterOidFormato(objPeticaoTermino.CodigoFormato)))
            'Algortimo
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_ALGORITMO_VALIDACION", ProsegurDbType.Objeto_Id, Algoritmo.ObterOidAlgoritmo(objPeticaoTermino.CodigoAlgoritmo)))
            'Mascara
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MASCARA", ProsegurDbType.Objeto_Id, Mascara.ObterOidMascara(objPeticaoTermino.CodigoMascara)))

            'Informações do usuário que fez a inclusão
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, objPeticaoTermino.CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            objtransacion.AdicionarItemTransacao(comando)


            objtransacion.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("010_msg_Erro_UKTermino"))
        End Try


    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Atualiza um termino
    ''' </summary>
    ''' <param name="objPeticaoTermino"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/03/2009 Alterado
    ''' </history>
    Public Shared Sub ActualizarTermino(objPeticaoTermino As ContractoServicio.TerminoIac.SetTerminoIac.Peticion)
        Try

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' preparar query
            Dim query As New StringBuilder
            query.Append("UPDATE GEPR_TTERMINO SET ")

            query.Append(Util.AdicionarCampoQuery("des_termino = []des_termino,", "des_termino", comando, objPeticaoTermino.Descripcion, ProsegurDbType.Descricao_Longa))
            query.Append(Util.AdicionarCampoQuery("obs_termino = []obs_termino,", "obs_termino", comando, objPeticaoTermino.Observacion, ProsegurDbType.Observacao_Longa))
            query.Append(Util.AdicionarCampoQuery("nec_longitud	= []nec_longitud,", "nec_longitud", comando, objPeticaoTermino.Longitud, ProsegurDbType.Inteiro_Curto))
            query.Append(Util.AdicionarCampoQuery("oid_formato = []oid_formato,", "oid_formato", comando, Formato.ObterOidFormato(objPeticaoTermino.CodigoFormato), ProsegurDbType.Objeto_Id))
            query.Append(Util.AdicionarCampoQuery("oid_algoritmo_validacion	= []oid_algoritmo_validacion,", "oid_algoritmo_validacion", comando, Algoritmo.ObterOidAlgoritmo(objPeticaoTermino.CodigoAlgoritmo), ProsegurDbType.Objeto_Id))
            query.Append(Util.AdicionarCampoQuery("oid_mascara = []oid_mascara,", "oid_mascara", comando, Mascara.ObterOidMascara(objPeticaoTermino.CodigoMascara), ProsegurDbType.Objeto_Id))
            query.Append(Util.AdicionarCampoQuery("bol_mostrar_codigo = []bol_mostrar_codigo,", "bol_mostrar_codigo", comando, objPeticaoTermino.MostrarCodigo, ProsegurDbType.Logico))
            query.Append(Util.AdicionarCampoQuery("bol_valores_posibles = []bol_valores_posibles,", "bol_valores_posibles", comando, objPeticaoTermino.AdmiteValoresPosibles, ProsegurDbType.Logico))
            query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objPeticaoTermino.Vigente, ProsegurDbType.Logico))
            query.Append(Util.AdicionarCampoQuery("bol_aceptar_digitacion = []bol_aceptar_digitacion,", "bol_aceptar_digitacion", comando, objPeticaoTermino.AceptarDigitacion, ProsegurDbType.Logico))

            query.Append("cod_usuario = []cod_usuario, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, objPeticaoTermino.CodigoUsuario))

            query.Append("fyh_actualizacion	= []fyh_actualizacion ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

            query.Append("WHERE cod_termino = []cod_termino ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, objPeticaoTermino.Codigo))

            comando.CommandText = Util.PrepararQuery(query.ToString)
            comando.CommandType = CommandType.Text

            ' executar o comando
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("010_msg_Erro_UKTermino"))
        End Try

    End Sub

#End Region

End Class