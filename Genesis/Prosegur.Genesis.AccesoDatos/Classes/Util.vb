Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Data.OracleClient
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario

Public Class Util

    Public Shared Function PrepararQuery(IdConexao As String, sql As String) As String
        Dim retorno = sql.Replace("[]", RetornarPrefixoParametro(IdConexao))
        Return retorno.Replace("###VERSION###", Prosegur.Genesis.Comon.Util.Version)
    End Function

    Public Shared Function RecuperarMensagemTratada(ex As Exception) As String

        Dim MsgErro As String() = Nothing

        If ex.Message.Contains("ORA-20001") Then

            MsgErro = ex.Message.Split("#")

            If MsgErro.Count > 1 AndAlso Not String.IsNullOrEmpty(MsgErro(1)) Then
                Return MsgErro(1)
            End If

        ElseIf ex.Message.Contains("ORA-06508") Then
            MsgErro = ex.Message.Split(Chr(34))
            If MsgErro.Count > 1 AndAlso Not String.IsNullOrEmpty(MsgErro(1)) Then
                ' MSG provisoria
                Return "Error al intentar ejecutar la package: " & MsgErro(1) & "."
            End If
        End If

        If ex.InnerException IsNot Nothing Then
            Return RecuperarMensagemTratada(ex.InnerException)
        End If


        Return String.Empty
    End Function

    Public Shared Function RetornarPrefixoParametro(IdConexao As String) As String

        ' verificar o banco que está sendo usado
        Select Case AcessoDados.RecuperarProvider(IdConexao)

            Case Provider.MsOracle

                Return ":"

            Case Provider.SqlServer

                Return "@"

        End Select

        Return String.Empty

    End Function


    ''' <summary>
    ''' Implementa os filtros da consulta.
    ''' </summary>
    ''' <param name="queryDefault">Valor da query a ser alterada para inclusão do filtro.</param>
    ''' <param name="filtro">Filtro a ser incluído na consulta.</param>
    Public Shared Function ImplementarClausulaWhere(queryDefault As String, filtro As String) As String

        If (queryDefault.Contains("WHERE")) Then
            If (filtro.Contains("WHERE")) Then
                filtro = filtro.Replace("WHERE", "AND")
            ElseIf Not (filtro.Contains("AND")) Then
                filtro = filtro.Insert(0, " AND ")
            End If
        Else
            filtro = If(Not filtro.Contains("WHERE"), filtro.Insert(0, " WHERE "), filtro)
        End If

        Return filtro

    End Function

    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra] 04/01/2013 Criado
    ''' </history>
    Public Shared Function AtribuirValorObj(Valor As Object,
                                               TipoCampo As System.Type) As Object

        Dim Campo As New Object

        If TipoCampo Is GetType(Byte()) AndAlso Valor IsNot DBNull.Value Then
            Campo = Valor
        ElseIf Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
            If TipoCampo Is Nothing Then
                Campo = Valor
            ElseIf TipoCampo Is GetType(String) Then
                Campo = Convert.ToString(Valor)
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = Convert.ToInt16(Valor)
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = Convert.ToInt32(Valor)
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Int64?) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(DateTime) Then
                Campo = Convert.ToDateTime(Valor)
            ElseIf TipoCampo Is GetType(Drawing.Color) Then
                Campo = Drawing.ColorTranslator.FromHtml("#" & Valor)
            End If
        Else

            If TipoCampo Is GetType(Decimal) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = 0
            Else
                Campo = Nothing
            End If

        End If

        Return Campo
    End Function

    ''' <summary>
    ''' Sobrecarga do método AtribuirValorObj para passar o tipo como um generics.
    ''' </summary>
    ''' <typeparam name="T">Tipo do campo (Generics)</typeparam>
    ''' <param name="Valor">Valor do campo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AtribuirValorObj(Of T)(Valor As Object) As T
        Return AtribuirValorObj(Valor, GetType(T))
    End Function

    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Campo"></param>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [marcel.espiritosanto] 22/08/2013 Criado
    ''' </history>
    Public Shared Sub AtribuirValorObjeto(ByRef Campo As Object,
                                          Valor As Object,
                                          TipoCampo As System.Type)

        If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
            If TipoCampo Is Nothing Then
                Campo = Valor
            ElseIf TipoCampo Is GetType(String) Then
                Campo = Convert.ToString(Valor)
            ElseIf TipoCampo Is GetType(Char) Then
                Campo = Convert.ToChar(Valor)
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = Convert.ToInt16(Valor)
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = Convert.ToInt32(Valor)
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(Date) Then
                Campo = Convert.ToDateTime(Valor)
            ElseIf TipoCampo.IsEnum Then
                Campo = [Enum].Parse(TipoCampo, Valor.ToString)
            ElseIf TipoCampo Is GetType(Drawing.Color) Then
                Campo = Drawing.ColorTranslator.FromHtml("#" & Valor)
            End If
        Else
            Campo = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Sobrecarga do método AtribuirValorObjeto para utilizar generics para passar o tipo.
    ''' </summary>
    ''' <param name="Campo">Campo a ser atribuido</param>
    ''' <param name="Valor">Valor que será atribuido</param>
    ''' <typeparam name="T">Tipo do campo.</typeparam>
    ''' <remarks></remarks>
    ''' <history>
    ''' [henrique.ribeiro] 17/10/2013 Criado
    ''' </history>
    Public Shared Sub AtribuirValorObjeto(Of T)(ByRef Campo As Object,
                                          Valor As Object)
        AtribuirValorObjeto(Campo, Valor, GetType(T))
    End Sub

    ''' <summary>
    ''' Monta a clausula in através de uma coleção de itens e o nome do campo
    ''' </summary>
    ''' <param name="Itens"></param>
    ''' <param name="Campo"></param>
    ''' <param name="Comando"></param>
    ''' <param name="TipoClausula">Utilizado para informar se a clausula é WHERE, AND ou OR.</param>
    ''' <param name="Alias">Insere um alias para o campo de consulta.</param>
    ''' <param name="Dif">Utilizado para diferenciar o campo que será adicionado no parameter. Isto evita problemas com campos repetidos no parameter.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Function MontarClausulaIn(IdConexao As String,
                                            Itens As Object,
                                            Campo As String,
                                            ByRef Comando As IDbCommand,
                                            Optional TipoClausula As String = "",
                                            Optional [Alias] As String = "",
                                            Optional Dif As String = "",
                                            Optional EsNotIn As Boolean = False,
                                            Optional PrefixParam As String = "") As String

        ' clausula in
        Dim clausulaIn As New StringBuilder

        ' se alias não for vazio
        If Not [Alias].Equals(String.Empty) Then
            [Alias] &= "."
        End If

        ' se foram informados itens para pesquisa
        If Itens IsNot Nothing AndAlso Itens.Count > 0 Then

            ' criar flag
            Dim addIn As Boolean = True

            If Itens.Count > 900 Then

                'Id que será inserido na tabela temporária para identificar os itens referentes ao parâmetro da cláusula IN
                Dim IdentificadorIn As String = Guid.NewGuid().ToString()
                'Qtd de itens inseridos
                Dim qtdRegistroIns As Integer = 0

                'Insere os itens de 100 em 100
                While qtdRegistroIns <= Itens.Count

                    'Insere na váriavel lstItens 100 itens e os concatena numa string
                    Dim lstItens As New List(Of String)
                    For i As Integer = qtdRegistroIns To qtdRegistroIns + 100
                        If i < Itens.Count Then
                            lstItens.Add(Itens(i))
                        End If
                    Next
                    Dim strItens As String = String.Join("|", lstItens.ToArray())

                    'Chama a proc pra inserir os 100 itens na tabela temporária
                    Using cmd As IDbCommand = AcessoDados.CriarComando(IdConexao)

                        cmd.CommandText = Util.PrepararQuery(IdConexao, "SP_CLAUSULA_IN_###VERSION###")
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, "ITEMS", ProsegurDbType.Descricao_Longa, strItens))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, "OID_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, IdentificadorIn))

                        AcessoDados.ExecutarNonQuery(IdConexao, cmd)

                    End Using

                    'Incrementa qnts de registros inseridos
                    qtdRegistroIns += 101

                End While

                'Nome do parâmetro a ser adicionado no comando
                Dim nomeParam As String = "CLAUSULA_IN_" & [Alias].TrimEnd(".") & Campo
                If nomeParam.Length > 30 Then
                    nomeParam = nomeParam.Substring(0, 30)
                End If

                If EsNotIn Then
                    ' concatenar filtro na query
                    clausulaIn.AppendLine(" " & TipoClausula & " " & [Alias] & Campo & " NOT IN (SELECT OID_ITEMS FROM GEPR_TCLAUSULA_IN WHERE OID_CLAUSULA_IN = []" & nomeParam & ")")
                Else
                    ' concatenar filtro na query
                    clausulaIn.AppendLine(" " & TipoClausula & " " & [Alias] & Campo & " IN (SELECT OID_ITEMS FROM GEPR_TCLAUSULA_IN WHERE OID_CLAUSULA_IN = []" & nomeParam & ")")
                End If

                'Insere o parâmetro no comando
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, nomeParam, ProsegurDbType.Identificador_Alfanumerico, IdentificadorIn))

            Else
                ' percorrer todos os itens
                For i As Integer = 0 To Itens.Count - 1

                    If Itens(i) IsNot Nothing AndAlso Not Itens(i).Equals(String.Empty) AndAlso addIn Then

                        If EsNotIn Then
                            ' concatenar filtro na query
                            clausulaIn.Append(" " & TipoClausula & " " & [Alias] & Campo & " NOT IN (")
                        Else
                            ' concatenar filtro na query
                            clausulaIn.Append(" " & TipoClausula & " " & [Alias] & Campo & " IN (")
                        End If

                        ' alterar flag 
                        addIn = False

                    ElseIf Itens(i) Is Nothing OrElse Itens(i).Equals(String.Empty) Then
                        Continue For
                    End If

                    ' concatenar parametro na query
                    clausulaIn.Append("[]" & PrefixParam & Dif & Campo & i)

                    ' se ainda existirem codigos
                    If i <> Itens.Count - 1 Then
                        clausulaIn.Append(",")
                    End If

                    ' setar parameter
                    If TypeOf Itens(i) Is String Then
                        Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, PrefixParam & Dif & Campo & i, ProsegurDbType.Descricao_Curta, Itens(i)))
                    ElseIf TypeOf Itens(i) Is Int16 Then
                        Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, PrefixParam & Dif & Campo & i, ProsegurDbType.Inteiro_Curto, Itens(i)))
                    ElseIf TypeOf Itens(i) Is Int32 Then
                        Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, PrefixParam & Dif & Campo & i, ProsegurDbType.Inteiro_Longo, Itens(i)))
                    End If

                Next
            End If

            ' se adicionou IN deve fechar parenteses
            If Not addIn Then

                ' fechar comando do filtro
                clausulaIn.Append(")")

            End If

        End If

        Return clausulaIn.ToString

    End Function


    Public Shared Function MontarClausulaIn(IdConexao As String,
                                            Itens As Object,
                                            Campo As String,
                                            ByRef Wrapper As DataBaseHelper.SPWrapper,
                                            ByRef Transaccion As DataBaseHelper.Transaccion,
                                            Optional TipoClausula As String = "",
                                            Optional [Alias] As String = "",
                                            Optional Dif As String = "",
                                            Optional EsNotIn As Boolean = False,
                                            Optional PrefixParam As String = "") As String

        ' clausula in
        Dim clausulaIn As New StringBuilder

        ' se alias não for vazio
        If Not [Alias].Equals(String.Empty) Then
            [Alias] &= "."
        End If

        ' se foram informados itens para pesquisa
        If Itens IsNot Nothing AndAlso Itens.Count > 0 Then

            ' criar flag
            Dim addIn As Boolean = True

            If Itens.Count > 900 Then

                'Id que será inserido na tabela temporária para identificar os itens referentes ao parâmetro da cláusula IN
                Dim IdentificadorIn As String = Guid.NewGuid().ToString()
                'Qtd de itens inseridos
                Dim qtdRegistroIns As Integer = 0

                'Insere os itens de 100 em 100
                While qtdRegistroIns <= Itens.Count

                    'Insere na váriavel lstItens 100 itens e os concatena numa string
                    Dim lstItens As New List(Of String)
                    For i As Integer = qtdRegistroIns To qtdRegistroIns + 100
                        If i < Itens.Count Then
                            lstItens.Add(Itens(i))
                        End If
                    Next
                    Dim strItens As String = String.Join("|", lstItens.ToArray())

                    'Chama a proc pra inserir os 100 itens na tabela temporária

                    Dim wrapperTemporario As New DataBaseHelper.SPWrapper(Util.PrepararQuery(IdConexao, "SP_CLAUSULA_IN_###VERSION###"), True)

                    wrapperTemporario.AgregarParam("ITEMS", ProsegurDbType.Descricao_Longa, strItens)
                    wrapperTemporario.AgregarParam("OID_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, IdentificadorIn)

                    DataBaseHelper.AccesoDB.EjecutarSP(Wrapper, IdConexao, False, Transaccion)

                    'Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    '    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, "SP_CLAUSULA_IN_###VERSION###")
                    '    cmd.CommandType = CommandType.StoredProcedure

                    '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "ITEMS", ProsegurDbType.Descricao_Longa, strItens))
                    '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, IdentificadorIn))

                    '    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

                    'End Using

                    'Incrementa qnts de registros inseridos
                    qtdRegistroIns += 101

                End While

                'Nome do parâmetro a ser adicionado no comando
                Dim nomeParam As String = "CLAUSULA_IN_" & [Alias].TrimEnd(".") & Campo
                If nomeParam.Length > 30 Then
                    nomeParam = nomeParam.Substring(0, 30)
                End If

                If EsNotIn Then
                    ' concatenar filtro na query
                    clausulaIn.AppendLine(" " & TipoClausula & " " & [Alias] & Campo & " NOT IN (SELECT OID_ITEMS FROM GEPR_TCLAUSULA_IN WHERE OID_CLAUSULA_IN = []" & nomeParam & ")")
                Else
                    ' concatenar filtro na query
                    clausulaIn.AppendLine(" " & TipoClausula & " " & [Alias] & Campo & " IN (SELECT OID_ITEMS FROM GEPR_TCLAUSULA_IN WHERE OID_CLAUSULA_IN = []" & nomeParam & ")")
                End If

                'Insere o parâmetro no comando
                Wrapper.AgregarParam(nomeParam, ProsegurDbType.Identificador_Alfanumerico, IdentificadorIn)

            Else
                ' percorrer todos os itens
                For i As Integer = 0 To Itens.Count - 1

                    If Itens(i) IsNot Nothing AndAlso Not Itens(i).Equals(String.Empty) AndAlso addIn Then

                        If EsNotIn Then
                            ' concatenar filtro na query
                            clausulaIn.Append(" " & TipoClausula & " " & [Alias] & Campo & " NOT IN (")
                        Else
                            ' concatenar filtro na query
                            clausulaIn.Append(" " & TipoClausula & " " & [Alias] & Campo & " IN (")
                        End If

                        ' alterar flag 
                        addIn = False

                    ElseIf Itens(i) Is Nothing OrElse Itens(i).Equals(String.Empty) Then
                        Continue For
                    End If

                    ' concatenar parametro na query
                    clausulaIn.Append("[]" & PrefixParam & Dif & Campo & i)

                    ' se ainda existirem codigos
                    If i <> Itens.Count - 1 Then
                        clausulaIn.Append(",")
                    End If

                    ' setar parameter
                    If TypeOf Itens(i) Is String Then
                        Wrapper.AgregarParam(PrefixParam & Dif & Campo & i, ProsegurDbType.Descricao_Curta, Itens(i))
                    ElseIf TypeOf Itens(i) Is Int16 Then
                        Wrapper.AgregarParam(PrefixParam & Dif & Campo & i, ProsegurDbType.Inteiro_Curto, Itens(i))
                    ElseIf TypeOf Itens(i) Is Int32 Then
                        Wrapper.AgregarParam(PrefixParam & Dif & Campo & i, ProsegurDbType.Inteiro_Longo, Itens(i))
                    End If

                Next
            End If

            ' se adicionou IN deve fechar parenteses
            If Not addIn Then

                ' fechar comando do filtro
                clausulaIn.Append(")")

            End If

        End If

        Return clausulaIn.ToString

    End Function

    ''' <summary>
    ''' Monta a clausula in através de uma coleção de itens e o nome do campo
    ''' </summary>
    ''' <param name="Itens"></param>
    ''' <param name="Campo"></param>
    ''' <param name="Comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 20/06/2013 Criado
    ''' </history>
    Public Shared Function MontarParametroIn(IdConexao As String,
                                            Itens As Object,
                                            Campo As String,
                                            ByRef Comando As IDbCommand) As String

        ' clausula in
        Dim clausulaIn As New StringBuilder

        ' se foram informados itens para pesquisa
        If Itens IsNot Nothing AndAlso Itens.Count > 0 Then

            ' percorrer todos os itens
            For i As Integer = 0 To Itens.Count - 1
                If Itens(i) Is Nothing OrElse Itens(i).Equals(String.Empty) Then
                    Continue For
                End If

                ' concatenar parametro na query
                clausulaIn.Append(",[]" & Campo & i)

                ' setar parameter
                If TypeOf Itens(i) Is String Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, Campo & i, ProsegurDbType.Descricao_Curta, Itens(i)))
                ElseIf TypeOf Itens(i) Is Int16 Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, Campo & i, ProsegurDbType.Inteiro_Curto, Itens(i)))
                ElseIf TypeOf Itens(i) Is Int32 Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(IdConexao, Campo & i, ProsegurDbType.Inteiro_Longo, Itens(i)))
                End If

            Next

        End If

        If clausulaIn.Length > 0 Then
            'Retira a primeira virgula
            Return clausulaIn.ToString(1, clausulaIn.Length - 1)
        Else
            Return String.Empty
        End If

    End Function

    Public Shared Function RetornaValorOuDbNull(Valor As Object) As Object

        If Valor Is Nothing OrElse
            (Valor Is GetType(String) AndAlso String.IsNullOrEmpty(Valor)) Then

            Return DBNull.Value

        End If

        Return Valor

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="coluna"></param>
    ''' <param name="direcao"></param>
    ''' <param name="valor"></param>
    ''' <param name="tipo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CriarParametroOracle(coluna As String,
                                                direcao As ParameterDirection,
                                                ByRef valor As Object,
                                                tipo As OracleType,
                                                Optional Tamanho As Integer = -1) As OracleParameter

        ' inicializar variáveis

        Dim p As OracleParameter = New OracleClient.OracleParameter

        ' montar parameter
        p.ParameterName = coluna
        p.Direction = direcao
        p.Value = valor
        p.OracleType = tipo

        If Tamanho <> -1 Then
            p.Size = Tamanho
        End If

        ' retorna o parameter preenchido
        Return p

    End Function

    ''' <summary>
    ''' Metodo utilizado para recuperar dados na tela de deste com o select fixo.
    ''' </summary>
    ''' <param name="SQL">Select a ser executado</param>
    ''' <returns>Retorna o datatable</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecutaSQL(SQL) As DataTable
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        cmd.CommandText = SQL
        cmd.CommandType = CommandType.Text

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
    End Function

    ''' <summary>
    ''' Verifica se o registro existe na tabela.
    ''' </summary>
    ''' <param name="tabela"></param>
    ''' <param name="coluna"></param>
    ''' <param name="valor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RegistroExiste(tabela As Enumeradores.Tabela, coluna As String, valor As String) As Boolean
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        Dim sql As String = String.Format("SELECT COUNT(1) FROM {0} WHERE {1}=[]VALOR_COLUNA", tabela.RecuperarValor, coluna)
        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sql)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "VALOR_COLUNA", ProsegurDbType.Objeto_Id, valor))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
    End Function

    ''' <summary>
    ''' Verifica se o registro existe na tabela.
    ''' </summary>
    ''' <param name="tabela">Nome da tabela</param>
    ''' <param name="coluna">Nome da coluna</param>
    ''' <param name="valor">Valor a ser procurado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RegistroExisteSalidas(tabela As String, coluna As String, valor As String) As Boolean
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
        Dim sql As String = String.Format("SELECT COUNT(1) FROM {0} WHERE {1}=[]VALOR_COLUNA", tabela, coluna)
        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, sql)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "VALOR_COLUNA", ProsegurDbType.Objeto_Id, valor))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALIDAS, cmd)
    End Function

    Public Shared Sub PreencherQueryCuenta(Filtro As Clases.Transferencias.Filtro, ByRef query As StringBuilder, ByRef cmd As IDbCommand)

        If Filtro IsNot Nothing Then

            If Filtro.ExcluirSectoresHijos AndAlso Filtro.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(Filtro.Sector.Identificador) Then
                query.Append(IIf(query.ToString <> " WHERE ", " AND SE.OID_SECTOR = []OID_SECTOR ", " SE.OID_SECTOR = []OID_SECTOR "))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, Filtro.Sector.Identificador))
            End If

            If Filtro.Cliente IsNot Nothing Then

                If Not String.IsNullOrEmpty(Filtro.Cliente.Codigo) Then
                    query.Append(IIf(query.ToString <> " WHERE ", " AND CL.COD_CLIENTE = []COD_CLIENTE ", " CL.COD_CLIENTE = []COD_CLIENTE "))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Objeto_Id, Filtro.Cliente.Codigo))
                End If

                If Filtro.Cliente.SubClientes IsNot Nothing AndAlso Filtro.Cliente.SubClientes.Count > 0 Then

                    Dim CodigosSubCliente As List(Of String) = Nothing
                    Dim CodigosPuntoServicio As List(Of String) = Nothing

                    'Recupera os codigos dos subclientes
                    CodigosSubCliente = (From subCliente In Filtro.Cliente.SubClientes Select subCliente.Codigo).ToList

                    'Monta a query com os codigos dos subclientes
                    If CodigosSubCliente IsNot Nothing AndAlso CodigosSubCliente.Count > 0 Then
                        query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosSubCliente, "COD_SUBCLIENTE", cmd, "AND", "SCL"))
                    End If

                    'Recupera os codigos dos pontos de serviço
                    CodigosPuntoServicio = (From SubCliente In Filtro.Cliente.SubClientes.FindAll(Function(sc) sc.PuntosServicio IsNot Nothing AndAlso sc.PuntosServicio.Count > 0),
                                            Pto In SubCliente.PuntosServicio Select Pto.Codigo).ToList

                    'Monta a query com os codigos dos pontos de serviço
                    If CodigosPuntoServicio IsNot Nothing AndAlso CodigosPuntoServicio.Count > 0 Then
                        query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosPuntoServicio, "COD_PTO_SERVICIO", cmd, "AND", "PTO"))
                    End If

                End If

            End If

            If Filtro.Canais IsNot Nothing AndAlso Filtro.Canais.Count > 0 Then

                'Recupera os codigos dos canais
                Dim CodigosCanais As List(Of String) = (From canal In Filtro.Canais Select canal.Codigo).ToList()

                Dim CodigosSubCanais As List(Of String) = Nothing

                'Monta a query com os codigos dos canais
                If CodigosCanais IsNot Nothing AndAlso CodigosCanais.Count > 0 Then
                    query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosCanais, "COD_CANAL", cmd, "AND", "CAN"))
                End If

                For Each Canal In Filtro.Canais

                    If Canal.SubCanales IsNot Nothing AndAlso Canal.SubCanales.Count > 0 Then

                        If CodigosSubCanais Is Nothing Then CodigosSubCanais = New List(Of String)

                        'Recupera os codigos dos subcanais
                        CodigosSubCanais.AddRange((From subcanal In Canal.SubCanales Select subcanal.Codigo).ToList())

                    End If

                Next

                'Monta a query com os codigos dos subcanais
                If CodigosSubCanais IsNot Nothing AndAlso CodigosSubCanais.Count > 0 Then
                    query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosSubCanais, "COD_SUBCANAL", cmd, "AND", "SBC"))
                End If

            End If
        End If

    End Sub
    Public Shared Sub PreencherQueryCuentaSaldo(Filtro As Clases.Transferencias.FiltroConsultaValoresAbono,
                                                ByRef query As StringBuilder, ByRef cmd As IDbCommand)

        If Filtro IsNot Nothing Then

            If Not Filtro.ConsiderarTodosLosNiveles AndAlso Filtro.Sectores IsNot Nothing AndAlso Filtro.Sectores.Count > 0 Then

                If Filtro.Sectores.Count = 1 Then

                    query.Append(IIf(query.ToString <> " WHERE ", " AND C.OID_SECTOR = []OID_SECTOR ", " C.OID_SECTOR = []OID_SECTOR "))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, Filtro.Sectores(0).Identificador))

                Else
                    query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.Sectores.Select(Function(r) r.Identificador).ToList, "OID_SECTOR",
                                                   cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "C"))
                End If

            End If

            If Filtro.Clientes IsNot Nothing AndAlso Filtro.Clientes.Count > 0 Then

                If Filtro.Clientes.Count = 1 Then
                    query.Append(IIf(query.ToString <> " WHERE ", " AND C.OID_CLIENTE = []OID_CLIENTE ", " C.OID_CLIENTE = []OID_CLIENTE "))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, Filtro.Clientes(0).Identificador))
                Else
                    query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.Clientes.Select(Function(r) r.Identificador).ToList, "OID_CLIENTE", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "C"))
                End If

                If Filtro.SubClientes IsNot Nothing AndAlso Filtro.SubClientes.Count > 0 Then

                    If Filtro.SubClientes.Count = 1 Then
                        query.Append(IIf(query.ToString <> " WHERE ", " AND C.OID_SUBCLIENTE = []OID_SUBCLIENTE ", " C.OID_SUBCLIENTE = []OID_SUBCLIENTE "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, Filtro.SubClientes(0).Identificador))
                    Else
                        query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.SubClientes.Select(Function(r) r.Identificador).ToList, "OID_SUBCLIENTE", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "C"))
                    End If

                    'Monta a query com os codigos dos pontos de serviço
                    If Filtro.PuntosServicio IsNot Nothing AndAlso Filtro.PuntosServicio.Count > 0 Then

                        If Filtro.PuntosServicio.Count = 1 Then
                            query.Append(IIf(query.ToString <> " WHERE ", " AND C.OID_PTO_SERVICIO = []OID_PTO_SERVICIO ", " C.OID_PTO_SERVICIO = []OID_PTO_SERVICIO "))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, Filtro.PuntosServicio(0).Identificador))
                        Else
                            query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.PuntosServicio.Select(Function(r) r.Identificador).ToList, "OID_PTO_SERVICIO", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "C"))
                        End If

                    End If

                End If

            End If

            If Filtro.Canales IsNot Nothing AndAlso Filtro.Canales.Count > 0 Then

                If Filtro.Canales.Count = 1 Then
                    query.Append(IIf(query.ToString <> " WHERE ", " AND C.OID_CANAL = []OID_CANAL ", " C.OID_CANAL = []OID_CANAL "))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", ProsegurDbType.Objeto_Id, Filtro.Canales(0).Identificador))
                Else
                    query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.Canales.Select(Function(r) r.Identificador).ToList, "OID_CANAL", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "C"))
                End If

                'Monta a query com os codigos dos subcanais
                If Filtro.SubCanales IsNot Nothing AndAlso Filtro.SubCanales.Count > 0 Then

                    If Filtro.SubCanales.Count = 1 Then
                        query.Append(IIf(query.ToString <> " WHERE ", " AND C.OID_SUBCANAL = []OID_SUBCANAL ", " C.OID_SUBCANAL = []OID_SUBCANAL "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, Filtro.SubCanales(0).Identificador))
                    Else
                        query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.SubCanales.Select(Function(r) r.Identificador).ToList, "OID_SUBCANAL", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "C"))
                    End If

                End If

            End If
        End If

    End Sub
    Public Shared Sub PreencherQueryCuentaElemento(Filtro As Clases.Transferencias.FiltroConsultaValoresAbono, abono As Clases.Abono.Abono, ByRef query As StringBuilder, ByRef cmd As IDbCommand)

        If Filtro IsNot Nothing Then

            If Not Filtro.ConsiderarTodosLosNiveles AndAlso Filtro.Sectores IsNot Nothing AndAlso Filtro.Sectores.Count > 0 Then

                If Filtro.Sectores.Count = 1 Then

                    query.Append(IIf(query.ToString <> " WHERE ", " AND SE.OID_SECTOR = []OID_SECTOR ", " SE.OID_SECTOR = []OID_SECTOR "))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, Filtro.Sectores(0).Identificador))

                Else
                    query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.Sectores.Select(Function(r) r.Identificador).ToList, "OID_SECTOR",
                                                   cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "SE"))
                End If

            End If

            If abono.Bancos IsNot Nothing AndAlso abono.Bancos.Count > 0 Then

                If abono.Bancos.Count = 1 Then
                    query.Append(IIf(query.ToString <> " WHERE ", " AND CUS.OID_CLIENTE = []OID_BANCO ", " CUS.OID_CLIENTE = []OID_BANCO "))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BANCO", ProsegurDbType.Objeto_Id, abono.Bancos(0).Identificador))
                Else
                    'To do
                    'query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.Clientes.Select(Function(r) r.Identificador).ToList, "OID_CLIENTE", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "CUS"))
                End If

            End If

            If Filtro.Clientes IsNot Nothing AndAlso Filtro.Clientes.Count > 0 Then

                If Filtro.Clientes.Count = 1 Then
                    query.Append(IIf(query.ToString <> " WHERE ", " AND CU.OID_CLIENTE = []OID_CLIENTE ", " CU.OID_CLIENTE = []OID_CLIENTE "))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, Filtro.Clientes(0).Identificador))
                Else
                    query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.Clientes.Select(Function(r) r.Identificador).ToList, "OID_CLIENTE", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "CU"))
                End If

                If Filtro.SubClientes IsNot Nothing AndAlso Filtro.SubClientes.Count > 0 Then

                    If Filtro.SubClientes.Count = 1 Then
                        query.Append(IIf(query.ToString <> " WHERE ", " AND CU.OID_SUBCLIENTE = []OID_SUBCLIENTE ", " CU.OID_SUBCLIENTE = []OID_SUBCLIENTE "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, Filtro.SubClientes(0).Identificador))
                    Else
                        query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.SubClientes.Select(Function(r) r.Identificador).ToList, "OID_SUBCLIENTE", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "CU"))
                    End If

                    'Monta a query com os codigos dos pontos de serviço
                    If Filtro.PuntosServicio IsNot Nothing AndAlso Filtro.PuntosServicio.Count > 0 Then

                        If Filtro.PuntosServicio.Count = 1 Then
                            query.Append(IIf(query.ToString <> " WHERE ", " AND CU.OID_PTO_SERVICIO = []OID_PTO_SERVICIO ", " CU.OID_PTO_SERVICIO = []OID_PTO_SERVICIO "))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, Filtro.PuntosServicio(0).Identificador))
                        Else
                            query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.PuntosServicio.Select(Function(r) r.Identificador).ToList, "OID_PTO_SERVICIO", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "CU"))
                        End If

                    End If

                End If

            End If

            If Filtro.Canales IsNot Nothing AndAlso Filtro.Canales.Count > 0 Then

                If Filtro.Canales.Count = 1 Then
                    query.Append(IIf(query.ToString <> " WHERE ", " AND CAN.OID_CANAL = []OID_CANAL ", " CAN.OID_CANAL = []OID_CANAL "))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", ProsegurDbType.Objeto_Id, Filtro.Canales(0).Identificador))
                Else
                    query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.Canales.Select(Function(r) r.Identificador).ToList, "OID_CANAL", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "CAN"))
                End If

                'Monta a query com os codigos dos subcanais
                If Filtro.SubCanales IsNot Nothing AndAlso Filtro.SubCanales.Count > 0 Then

                    If Filtro.SubCanales.Count = 1 Then
                        query.Append(IIf(query.ToString <> " WHERE ", " AND SBC.OID_SUBCANAL = []OID_SUBCANAL ", " SBC.OID_SUBCANAL = []OID_SUBCANAL "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, Filtro.SubCanales(0).Identificador))
                    Else
                        query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Filtro.SubCanales.Select(Function(r) r.Identificador).ToList, "OID_SUBCANAL", cmd, IIf(query.ToString <> " WHERE ", "AND", ""), "SBC"))
                    End If

                End If

            End If
        End If

    End Sub

    Public Shared Function ValidarValor(Item As Object) As Object
        If (Item Is Nothing) OrElse (Item Is DBNull.Value) Then
            Return String.Empty
        Else
            Return Item
        End If
    End Function

    Public Shared Function NewGUID() As String
        Return Guid.NewGuid.ToString
    End Function

    ''' <summary>
    ''' Recebi una data y retorna la data que debe ser grabada en la BBDD
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="delegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DataHoraGMT_GrabarEnLaBase(data As DateTime, ByRef delegacion As Clases.Delegacion) As DateTime
        Return DataHoraGMTDelegacion(data, delegacion, True)
    End Function

    Public Shared Function DataHoraGMT_RecuperardeLaBase(data As DateTime, ByRef delegacion As Clases.Delegacion) As DateTime
        Return DataHoraGMTDelegacion(data, delegacion, False)
    End Function

    Private Shared Function DataHoraGMTDelegacion(data As DateTime, ByRef delegacion As Clases.Delegacion, esGMTZero As Boolean) As DateTime
        If data <> DateTime.MinValue Then

            'Verifica se a delegação foi informada
            If delegacion Is Nothing OrElse (String.IsNullOrEmpty(delegacion.Identificador) AndAlso String.IsNullOrEmpty(delegacion.Codigo)) Then
                Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("066_NoFueInformadoDelegacion"))
            End If

            If delegacion.HusoHorarioEnMinutos = 0 Then
                If Not String.IsNullOrEmpty(delegacion.Identificador) Then
                    Dim objDelegacion = AccesoDatos.Genesis.Delegacion.ObtenerPorOid(delegacion.Identificador)
                    If objDelegacion Is Nothing Then
                        Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("066_Identificador_Delegacion_Invalido"))
                    End If
                    delegacion = objDelegacion
                Else

                    Dim codDelegacion As New List(Of String)
                    codDelegacion.Add(delegacion.Codigo)
                    Dim objDelegaciones = AccesoDatos.Genesis.Delegacion.ObtenerPorCodigos(codDelegacion)
                    If objDelegaciones Is Nothing OrElse objDelegaciones.Count = 0 Then
                        Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("066_Codigo_Delegacion_Invalido"))
                    End If
                    delegacion = objDelegaciones.FirstOrDefault
                End If
            End If

            If Not String.IsNullOrEmpty(delegacion.Identificador) Then
                data = DataHoraGMT(data, delegacion, esGMTZero)
            End If
        End If

        Return data
    End Function


    Public Shared Sub resultado(ByRef Codigo As String,
                                ByRef Descripcion As String,
                       Optional Tipo As Enumeradores.Mensajes.Tipo = Enumeradores.Mensajes.Tipo.Exito,
                       Optional Contexto As Enumeradores.Mensajes.Contexto = Enumeradores.Mensajes.Contexto.Genesis,
                       Optional Funcionalidad As Enumeradores.Mensajes.Funcionalidad = Enumeradores.Mensajes.Funcionalidad.General,
                       Optional Mensaje As String = "0000",
                       Optional Detalle As String = "",
                       Optional RecuperarMensaje As Boolean = False)

        Codigo = Tipo.RecuperarValor() & Contexto.RecuperarValor() & Funcionalidad.RecuperarValor() & Mensaje
        Descripcion = Detalle

        If RecuperarMensaje Then
            Descripcion = RecuperarMensajes(Codigo, Funcionalidad.ToString.ToUpper(), Detalle)

        End If

    End Sub

    Public Shared Sub resultadoGenesis(ByRef Codigo As String,
                                ByRef Descripcion As String,
                       Optional Tipo As Enumeradores.Mensajes.Tipo = Enumeradores.Mensajes.Tipo.Exito,
                       Optional Contexto As Enumeradores.Mensajes.Contexto = Enumeradores.Mensajes.Contexto.Genesis,
                       Optional Funcionalidad As Enumeradores.Mensajes.FuncionalidadGenesis = Enumeradores.Mensajes.FuncionalidadGenesis.General,
                       Optional Mensaje As String = "0000",
                       Optional Detalle As String = "",
                       Optional RecuperarMensaje As Boolean = False)

        Codigo = Tipo.RecuperarValor() & Contexto.RecuperarValor() & Funcionalidad.RecuperarValor() & Mensaje
        Descripcion = Detalle

        If RecuperarMensaje Then
            Descripcion = RecuperarMensajes(Codigo, Funcionalidad.ToString.ToUpper(), Detalle)

        End If

    End Sub

    Private Shared Property dicionario() As Prosegur.Genesis.Comon.SerializableDictionary(Of String, Prosegur.Genesis.Comon.SerializableDictionary(Of String, String))

    Public Shared Function RecuperarMensajes(Codigo As String, Funcionalidad As String, Detalle As String) As String

        Dim mensaje As String = String.Empty

        Try

            Dim codigoCultura As String = GetCultureUser()

            If dicionario IsNot Nothing AndAlso dicionario.Count > 0 AndAlso dicionario.ContainsKey(codigoCultura) Then

                Dim chavesDic = dicionario(codigoCultura)
                If chavesDic IsNot Nothing AndAlso chavesDic.ContainsKey(Codigo) Then
                    mensaje = chavesDic(Codigo)
                Else
                    mensaje = RecuperarDiccionario(Codigo, Funcionalidad, codigoCultura)

                End If

            Else
                mensaje = RecuperarDiccionario(Codigo, Funcionalidad, codigoCultura)

            End If

            If Not String.IsNullOrEmpty(mensaje) Then
                mensaje = String.Format(mensaje, Detalle)
                mensaje = mensaje.Replace(Chr(34), "'")
            End If

        Catch ex As Exception
            '
        End Try

        Return mensaje

    End Function

    Public Shared Function RecuperarDiccionario(Codigo As String, Funcionalidad As String, codigoCultura As String) As String

        Dim mensaje As String = String.Empty
        'mensaje = Tradutor.Traduzir(Codigo)

        If mensaje.Equals("[" & Codigo & "]") Then
            mensaje = String.Empty
        End If

        If String.IsNullOrEmpty(mensaje) Then
            mensaje = AccesoDatos.Genesis.Diccionario.ObtenerValorDicionarioSimples(codigoCultura, Funcionalidad, Codigo)
        End If

        If Not String.IsNullOrEmpty(mensaje) Then

            If dicionario Is Nothing Then
                dicionario = New SerializableDictionary(Of String, SerializableDictionary(Of String, String))
            End If
            If dicionario.Count = 0 OrElse Not dicionario.ContainsKey(codigoCultura) Then
                dicionario.Add(codigoCultura, New Prosegur.Genesis.Comon.SerializableDictionary(Of String, String))
            End If

            dicionario(codigoCultura).Add(Codigo, mensaje)

        End If

        Return mensaje

    End Function


    ''' <summary>
    ''' GetCultureUser() reemplaza a 
    ''' If(Tradutor.CulturaSistema IsNot Nothing AndAlso
    '''    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
    '''         Tradutor.CulturaSistema.Name,
    '''         If (Tradutor.CulturaPadrao IsNot Nothing, 
    '''             Tradutor.CulturaPadrao.Name, 
    '''             String.Empty)
    ''' </summary>
    ''' <returns>La cultura del usuario</returns>
    Public Shared Function GetCultureUser() As String

        If Tradutor.CulturaSistema IsNot Nothing AndAlso Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name) Then
            Return Tradutor.CulturaSistema.Name
        Else
            If Tradutor.CulturaPadrao IsNot Nothing Then
                Return Tradutor.CulturaPadrao.Name
            Else
                Return String.Empty
            End If
        End If

    End Function

    Public Shared Function ToXML(objObject As Object) As String
        Dim sw1 = New StringWriter()
        Dim xs1 As New XmlSerializer(objObject.GetType())
        xs1.Serialize(New XmlTextWriter(sw1), objObject)

        Return sw1.ToString
    End Function
    
    ''' <summary>
    ''' Permite retornar una fecha y hora en una zona horaria especificada
    ''' </summary>
    ''' <param name="pDateTime">Fecha y hora a convertir</param>
    ''' <param name="pTimeZone">Zona horaria deseada ej: "-03:00"</param>
    ''' <returns></returns>
    Public Shared Function ConvertToDateTimeOffset(pDateTime As Date, pTimeZone As String) As DateTimeOffset
        Try
            Dim utcDate = pDateTime.ToUniversalTime
            Dim TimeZone = Strings.Split(pTimeZone, ":")
            Dim offset As DateTimeOffset

            Dim liHour As Integer = CInt(TimeZone(0))
            Dim liMinute As Integer = CInt(TimeZone(1))

            Dim customTZ = TimeZoneInfo.CreateCustomTimeZone("TZ", New TimeSpan(liHour, liMinute, 0), "TimeZone", "TimeZone")

            With utcDate
                offset = New DateTimeOffset(.Year, .Month, .Day, .Hour, .Minute, .Second, New TimeSpan(0, 0, 0))
            End With

            Dim conversion = TimeZoneInfo.ConvertTime(offset, customTZ)

            Return conversion

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
