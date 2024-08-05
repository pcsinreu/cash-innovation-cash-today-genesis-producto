Imports System.Configuration.ConfigurationManager
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.CriptoHelper
Imports System.IO
Imports System.Xml

''' <summary>
''' Utilidades
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 23/06/2008 Criado
''' </history>
Public Class Util

    '''' <summary>
    '''' Cria e retorna um parameter.
    '''' </summary>
    '''' <param name="strBaseDestino">Base destino.</param>
    '''' <param name="strCampo">Campo da query.</param>
    '''' <param name="dbtpTipo">Tipo do campo.</param>
    '''' <param name="objValue">Valor do campo.</param>
    '''' <returns>Parameter preenchido</returns>
    '''' <remarks></remarks>
    '''' <history>
    '''' [octavio.piramo] 23/06/2008	Criado
    '''' </history>
    'Public Shared Function PreencherParameter(strBaseDestino As String, _
    '                                          strCampo As String, _
    '                                          dbtpTipo As System.Data.DbType, _
    '                                          objValue As Object) As System.Data.IDataParameter

    '    ' inicializar variáveis
    '    Dim p As IDataParameter = DbHelper.AcessoDados.CriarParametroProsegurDbType(strBaseDestino)

    '    ' montar parameter
    '    p.ParameterName = RetornarPrefixoParametro() & strCampo
    '    p.Direction = ParameterDirection.Input
    '    p.DbType = dbtpTipo
    '    p.Value = objValue

    '    ' retorna o parameter preenchido
    '    Return p

    'End Function

    ''' <summary>
    ''' Retorna o prefixo do parâmetro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/06/2008	Criado
    ''' </history>
    Public Shared Function RetornarPrefixoParametro() As String

        ' verificar o banco que está sendo usado
        Select Case AcessoDados.RecuperarProvider(Constantes.CONEXAO_GE)

            Case Provider.MsOracle

                Return ":"

            Case Provider.SqlServer

                Return "@"

        End Select

        Return String.Empty

    End Function

    ''' <summary>
    ''' Substitui o caractere auxiliar [] pelo caractere do banco em uso
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/06/2008	Criado
    ''' </history>
    Public Shared Function PrepararQuery(sql As String) As String

        Dim retorno = sql.Replace("[]", RetornarPrefixoParametro)
        Return retorno.Replace("###VERSION###", Prosegur.Genesis.Comon.Util.Version)

    End Function

    ''' <summary>
    ''' Função que retorna a string com os valores de um select in()
    ''' </summary>
    ''' <param name="strLista">list of string</param>
    ''' <returns>retorna o valor formatado com aspas simples e virgula</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [mhermeto]04/07/2008 Criado
    ''' </history>
    Public Shared Function RetornaListOfStringFormatado(strLista As List(Of String)) As String

        Dim strValor As New StringBuilder
        Dim icount As Integer = 0

        For Each ite In strLista

            icount += 1
            If icount <= strLista.Count Then
                strValor.Append("'")
                strValor.Append(ite)
                strValor.Append("'")
                If (icount < strLista.Count) Then strValor.Append(",")
            End If

        Next

        Return strValor.ToString()

    End Function

    ''' <summary>
    ''' Função que retorna a string com os valores de um select in()
    ''' </summary>
    ''' <param name="strLista">list of integer</param>
    ''' <returns>retorna o valor formatado com aspas simples e virgula</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [mhermeto]07/07/2008 Criado
    ''' </history>
    Public Shared Function RetornaListOfIntegerFormatado(strLista As List(Of Integer)) As String

        Dim strValor As New StringBuilder
        Dim icount As Integer = 0

        For Each ite In strLista

            icount += 1
            If strLista.Count = icount Then
                strValor.Append(ite)
            Else
                strValor.Append(ite & ",")
            End If

        Next

        Return strValor.ToString()

    End Function

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
    Public Shared Function MontarClausulaIn(Itens As Object, _
                                            Campo As String, _
                                            ByRef Comando As IDbCommand, _
                                            Optional TipoClausula As String = "", _
                                            Optional [Alias] As String = "", _
                                            Optional Dif As String = "", _
                                            Optional EsNotIn As Boolean = False) As String

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
                    Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

                        cmd.CommandText = Util.PrepararQuery("SP_CLAUSULA_IN_###VERSION###")
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ITEMS", ProsegurDbType.Descricao_Longa, strItens))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLAUSULA_IN", ProsegurDbType.Descricao_Longa, IdentificadorIn))

                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, cmd)

                    End Using

                    'Incrementa qnts de registros inseridos
                    qtdRegistroIns += 101

                End While

                'Nome do parâmetro a ser adicionado no comando
                Dim nomeParam As String = "CLAUSULA_IN_" & [Alias].TrimEnd(".") & Campo

                If EsNotIn Then
                    ' concatenar filtro na query
                    clausulaIn.AppendLine(" " & TipoClausula & " " & [Alias] & Campo & " NOT IN (SELECT OID_ITEMS FROM GEPR_TCLAUSULA_IN WHERE OID_CLAUSULA_IN = []" & nomeParam & ")")
                Else
                    ' concatenar filtro na query
                    clausulaIn.AppendLine(" " & TipoClausula & " " & [Alias] & Campo & " IN (SELECT OID_ITEMS FROM GEPR_TCLAUSULA_IN WHERE OID_CLAUSULA_IN = []" & nomeParam & ")")
                End If

                'Insere o parâmetro no comando
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, nomeParam, ProsegurDbType.Identificador_Alfanumerico, IdentificadorIn))

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
                    clausulaIn.Append("[]" & Dif & Campo & i)

                    ' se ainda existirem codigos
                    If i <> Itens.Count - 1 Then
                        clausulaIn.Append(",")
                    End If

                    ' setar parameter
                    If TypeOf Itens(i) Is String Then
                        Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Dif & Campo & i, ProsegurDbType.Descricao_Curta, Itens(i)))
                    ElseIf TypeOf Itens(i) Is Int16 Then
                        Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Dif & Campo & i, ProsegurDbType.Inteiro_Curto, Itens(i)))
                    ElseIf TypeOf Itens(i) Is Int32 Then
                        Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Dif & Campo & i, ProsegurDbType.Inteiro_Longo, Itens(i)))
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
    ''' Monta a clausula like através de uma coleção de itens e o nome do campo
    ''' </summary>
    ''' <param name="Itens"></param>
    ''' <param name="Campo"></param>
    ''' <param name="Comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/02/2009 Created
    ''' </history>
    Public Shared Function MontarClausulaLike(Itens As Object, _
                                              Campo As String, _
                                              ByRef Comando As IDbCommand, _
                                              Optional TipoClausula As String = "", _
                                              Optional [Alias] As String = "") As String

        ' clausula in
        Dim clausulalike As New StringBuilder

        ' se alias não for vazio
        If Not [Alias].Equals(String.Empty) Then
            [Alias] &= "."
        End If

        ' se foram informados itens para pesquisa
        If Itens IsNot Nothing AndAlso Itens.Count > 0 Then

            ' criar flag
            Dim addIn As Boolean = True

            ' percorrer todos os itens
            For i As Integer = 0 To Itens.Count - 1

                If Itens(i) IsNot Nothing AndAlso Not Itens(i).Equals(String.Empty) AndAlso addIn Then

                    ' concatenar filtro na query
                    clausulalike.Append(" " & TipoClausula & " (" & [Alias] & Campo & " LIKE (")

                    ' alterar flag 
                    addIn = False

                ElseIf Not addIn Then
                    clausulalike.Append(" OR " & [Alias] & Campo & " LIKE (")
                ElseIf Itens(i) Is Nothing OrElse Itens(i).Equals(String.Empty) Then
                    Continue For
                End If

                ' concatenar parametro na query
                clausulalike.Append("[]" & Campo & i)

                ' fechar parenteses
                clausulalike.Append(")")

                ' setar parameter
                If TypeOf Itens(i) Is String Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo & i, ProsegurDbType.Descricao_Longa, "%" & Itens(i) & "%"))
                ElseIf TypeOf Itens(i) Is Int16 Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo & i, ProsegurDbType.Inteiro_Curto, "%" & Itens(i) & "%"))
                ElseIf TypeOf Itens(i) Is Int32 Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo & i, ProsegurDbType.Inteiro_Longo, "%" & Itens(i) & "%"))
                End If

            Next

            ' se adicionou IN deve fechar parenteses
            If Not addIn Then

                ' fechar comando do filtro
                clausulalike.Append(")")

            End If

        End If

        Return clausulalike.ToString

    End Function

    ''' <summary>
    ''' Monta a clausula like através de uma coleção de itens e o nome do campo
    ''' </summary>
    ''' <param name="Itens"></param>
    ''' <param name="Campo"></param>
    ''' <param name="Comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 01/10/2009 Created
    ''' </history>
    Public Shared Function MontarClausulaLikeUpper(Itens As Object, _
                                                   Campo As String, _
                                                   ByRef Comando As IDbCommand, _
                                                   Optional TipoClausula As String = "", _
                                                   Optional [Alias] As String = "") As String

        ' clausula in
        Dim clausulalike As New StringBuilder

        ' se alias não for vazio
        If Not [Alias].Equals(String.Empty) Then
            [Alias] &= "."
        End If

        ' se foram informados itens para pesquisa
        If Itens IsNot Nothing AndAlso Itens.Count > 0 Then

            ' criar flag
            Dim addIn As Boolean = True

            ' percorrer todos os itens
            For i As Integer = 0 To Itens.Count - 1

                If Itens(i) IsNot Nothing AndAlso Not Itens(i).Equals(String.Empty) AndAlso addIn Then

                    ' concatenar filtro na query
                    clausulalike.Append(" " & TipoClausula & " (" & "UPPER(" & [Alias] & Campo & ")" & " LIKE (")

                    ' alterar flag 
                    addIn = False

                ElseIf Not addIn Then
                    clausulalike.Append(" OR " & "UPPER(" & [Alias] & Campo & ")" & " LIKE (")
                ElseIf Itens(i) Is Nothing OrElse Itens(i).Equals(String.Empty) Then
                    Continue For
                End If

                ' concatenar parametro na query
                clausulalike.Append("UPPER([]" & Campo & i & ")")

                ' fechar parenteses
                clausulalike.Append(")")

                ' setar parameter
                If TypeOf Itens(i) Is String Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo & i, ProsegurDbType.Descricao_Longa, "%" & Itens(i) & "%"))
                ElseIf TypeOf Itens(i) Is Int16 Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo & i, ProsegurDbType.Inteiro_Curto, "%" & Itens(i) & "%"))
                ElseIf TypeOf Itens(i) Is Int32 Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo & i, ProsegurDbType.Inteiro_Longo, "%" & Itens(i) & "%"))
                End If

            Next

            ' se adicionou IN deve fechar parenteses
            If Not addIn Then

                ' fechar comando do filtro
                clausulalike.Append(")")

            End If

        End If

        Return clausulalike.ToString

    End Function

    ''' <summary>
    ''' Dada uma query genérica, monta a cláusula where com os pârametros informados
    ''' </summary>
    ''' <param name="Comandos">Lista de comandos - Objeto Criterio{Codicional e Clausula. Ex: Condicional-> AND e Clausula-> CampoExemplo like '%valor%'}</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <hitory>pda 12/02/09 - Created</hitory>
    Public Shared Function MontarClausulaWhere(Comandos As CriterioColecion) As String

        Dim clausula As New StringBuilder

        For Each item As Criterio In Comandos

            If Not item.Clausula.Equals(String.Empty) AndAlso clausula.Length = 0 Then
                clausula.Append(" WHERE " & item.Clausula)
            ElseIf Not item.Clausula.Equals(String.Empty) AndAlso clausula.Length > 0 Then
                clausula.Append(" " & item.Condicional & " " & item.Clausula)
            End If
        Next

        Return clausula.ToString

    End Function

    ''' <summary>
    ''' Remove itens vazios de uma lista de strings
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Public Shared Function RemoverItensVazios(Itens As List(Of String)) As List(Of String)

        Dim newDivisa As New List(Of String)
        For Each Str As String In Itens
            If Str IsNot Nothing AndAlso Not Str.Equals(String.Empty) Then
                newDivisa.Add(Str)
            End If
        Next

        Return newDivisa

    End Function

    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Campo"></param>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 06/03/2009 Criado
    ''' </history>
    Public Shared Sub AtribuirValorObjeto(ByRef Campo As Object, _
                                          Valor As Object, _
                                          TipoCampo As System.Type)

        If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
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
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(Date) Then
                Campo = Convert.ToDateTime(String.Format(Convert.ToDateTime(Valor), "dd/MM/yyyy hh:mm:ss"))
            ElseIf TipoCampo.IsEnum Then
                Campo = [Enum].Parse(TipoCampo, Valor.ToString)
            End If
        Else
            Campo = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Atribui o valor ao objeto passado caso o mesmo não seja nulo, vazio ou dbnull.value
    ''' </summary>
    ''' <param name="Campo"></param>
    ''' <param name="Valor"></param>
    ''' <param name="NullAbleOfBollean">Caso o campo seja do tipo NullAbleOfBollean passar este parâmetro como "true" </param>
    ''' <remarks></remarks>
    ''' <history>[pda] 13/02/2009</history>
    <Obsolete()> _
    Public Shared Sub AtribuirValorObjeto(ByRef Campo As Object, Valor As Object, Optional NullAbleOfBollean As Boolean = False)

        'Quando o campo é Nullable não é possivel fazer a conversão
        'A aplicação utiliza Nullable of Logico
        If NullAbleOfBollean Then
            If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
                Campo = CBool(Valor)
            End If
        Else
            If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
                Campo = Valor
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método responsável por adicionar na query campos que contêm valor.
    ''' </summary>
    ''' <param name="ComplementoQuery">Pedaço da query que será adicionada. Ex: ,COD = []COD1</param>
    ''' <param name="Campo">Campo que será utilizado no parameter. Ex: COD1</param>
    ''' <param name="Comando">Comando que será eceutado no banco. (ByRef)</param>
    ''' <param name="Valor">Valor que será validado e utilizado no parameter.</param>
    ''' <param name="Tipo">Tipo do campo que será utilizado no parameter.</param>
    ''' <returns>Retorna o complemento da query caso contenha algum valor válido na variável Valor.</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 06/03/2009 Criado
    ''' </history>
    Public Shared Function AdicionarCampoQuery(ComplementoQuery As String, _
                                               Campo As String, _
                                               ByRef Comando As IDbCommand, _
                                               Valor As Object, _
                                               Tipo As ProsegurDbType) As String

        Select Case True

            ' caso seja tipo string
            Case TypeOf Valor Is String

                ' efetuar as validações
                Dim newValor As String = DirectCast(Valor, String)
                If newValor IsNot Nothing Then
                    If Not newValor.Equals(String.Empty) Then
                        Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo, Tipo, newValor))
                        Return ComplementoQuery
                    Else
                        Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo, Tipo, DBNull.Value))
                        Return ComplementoQuery
                    End If
                End If

                ' caso seja tipo Nullable(Of Logico)
            Case TypeOf Valor Is Nullable(Of Boolean)

                Dim newValor As Nullable(Of Boolean) = DirectCast(Valor, Nullable(Of Boolean))
                If newValor IsNot Nothing Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo, Tipo, newValor))
                    Return ComplementoQuery
                End If

                ' caso seja tipo Nullable(Of Int16)
            Case TypeOf Valor Is Nullable(Of Int16)

                Dim newValor As Nullable(Of Int16) = DirectCast(Valor, Nullable(Of Int16))
                If newValor IsNot Nothing Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo, Tipo, newValor))
                    Return ComplementoQuery
                End If

                ' caso seja tipo Nullable(Of Int32)
            Case TypeOf Valor Is Nullable(Of Int32)

                Dim newValor As Nullable(Of Int32) = DirectCast(Valor, Nullable(Of Int32))
                If newValor IsNot Nothing Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo, Tipo, newValor))
                    Return ComplementoQuery
                End If

                ' caso seja tipo Nullable(Of Int64)
            Case TypeOf Valor Is Nullable(Of Int64)

                Dim newValor As Nullable(Of Int64) = DirectCast(Valor, Nullable(Of Int64))
                If newValor IsNot Nothing Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo, Tipo, newValor))
                    Return ComplementoQuery
                End If

                ' caso seja tipo Nullable(Of Decimal)
            Case TypeOf Valor Is Nullable(Of Decimal)

                Dim newValor As Nullable(Of Decimal) = DirectCast(Valor, Nullable(Of Decimal))
                If newValor IsNot Nothing Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, Campo, Tipo, newValor))
                    Return ComplementoQuery
                End If

        End Select

        Return String.Empty

    End Function

    ''' <summary>
    ''' Recebe um objInfGeneral e gera um HashCode
    ''' </summary>
    ''' <param name="objInfGeneral"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 - Criado
    ''' </history>
    Public Shared Function CheckSumInfGeneralSetProceso(objInfGeneral As ContractoServicio.Utilidad.CheckSumInfGeneralSetProceso.CheckSumInfGeneralSetProceso) As String

        Dim checkSumInf As New StringBuilder
        Dim checkSumHash As String

        checkSumInf.Append(objInfGeneral.Delegacion) _
                  .Append(objInfGeneral.DesProducto) _
                  .Append(objInfGeneral.DesModalidad) _
                  .Append(objInfGeneral.BolCuentaChequeTotal) _
                  .Append(objInfGeneral.BolCuentaTicketTotal) _
                  .Append(objInfGeneral.BolCuentaOtrosTotal) _
                  .Append(objInfGeneral.BolCuentaTarjetasTotal)

        If objInfGeneral.BolMedioPago = True Then
            checkSumInf.Append(RetornaMedioPagoInfGeneral(objInfGeneral.MediosPago)) _
                       .Append(RetornaDivisaInfGeneral(objInfGeneral.Divisas))
        Else
            checkSumInf.Append(RetornaAgrupacionInfGeneral(objInfGeneral.Agrupaciones))
        End If

        If objInfGeneral.CodigoIacParcial IsNot Nothing AndAlso Not String.IsNullOrEmpty(objInfGeneral.CodigoIacParcial) Then
            checkSumInf.Append(objInfGeneral.CodigoIacParcial & ContractoServicio.Constantes.CONST_TIPO_CONTENEDOR_PARCIAL)
        End If
        If objInfGeneral.CodigoIACBulto IsNot Nothing AndAlso Not String.IsNullOrEmpty(objInfGeneral.CodigoIACBulto) Then
            checkSumInf.Append(objInfGeneral.CodigoIACBulto & ContractoServicio.Constantes.CONST_TIPO_CONTENEDOR_BULTO)
        End If
        If objInfGeneral.CodigoIACRemesa IsNot Nothing AndAlso Not String.IsNullOrEmpty(objInfGeneral.CodigoIACRemesa) Then
            checkSumInf.Append(objInfGeneral.CodigoIACRemesa & ContractoServicio.Constantes.CONST_TIPO_CONTENEDOR_REMESA)
        End If

        'Chama o metodo GerarHash e gera um HashCode com a string informada.
        checkSumHash = (GerarHash(checkSumInf).ToString)

        ' Retorna o Hash gerado
        Return checkSumHash

    End Function

    ''' <summary>
    ''' Recebe uma string e gera um HashCode
    ''' </summary>
    ''' <param name="strHash"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 - Criado
    ''' </history>
    Private Shared Function GerarHash(strHash As StringBuilder) As String

        Dim strScript As String
        Dim objGerarHash As New Prosegur.CriptoHelper.MD5()

        strScript = (objGerarHash.GerarHash(strHash.ToString))

        Return strScript
    End Function

    ''' <summary>
    ''' Concatena uma coleção de medio pago e retorna uma string.
    ''' </summary>
    ''' <param name="objColMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaMedioPagoInfGeneral(objColMedioPago As ContractoServicio.Utilidad.CheckSumInfGeneralSetProceso.MedioPagoColeccion) As StringBuilder

        Dim strMedioPago As New StringBuilder

        For Each objMedioPago As ContractoServicio.Utilidad.CheckSumInfGeneralSetProceso.MedioPago In objColMedioPago.OrderBy(Function(mp) mp.Descripcion)

            strMedioPago.Append(objMedioPago.Descripcion)

        Next

        Return strMedioPago
    End Function

    ''' <summary>
    ''' Concatena uma coleção de divisa e retorna uma string.
    ''' </summary>
    ''' <param name="objColDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 31/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaDivisaInfGeneral(objColDivisa As ContractoServicio.Utilidad.CheckSumInfGeneralSetProceso.DivisaColeccion) As StringBuilder

        Dim strDivisa As New StringBuilder

        For Each objDivisa As ContractoServicio.Utilidad.CheckSumInfGeneralSetProceso.Divisa In objColDivisa.OrderBy(Function(d) d.Codigo)

            strDivisa.Append(objDivisa.Codigo)

        Next

        Return strDivisa
    End Function

    ''' <summary>
    ''' Concatena uma coleção de agrupações e retorna uma string.
    ''' </summary>
    ''' <param name="objColAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaAgrupacionInfGeneral(objColAgrupacion As ContractoServicio.Utilidad.CheckSumInfGeneralSetProceso.AgrupacionColeccion) As StringBuilder

        Dim strAgrupacion As New StringBuilder

        For Each objAgrupacion As ContractoServicio.Utilidad.CheckSumInfGeneralSetProceso.Agrupacion In objColAgrupacion.OrderBy(Function(a) a.Descripcion)

            strAgrupacion.Append(objAgrupacion.Descripcion)

        Next

        Return strAgrupacion
    End Function

    ''' <summary>
    ''' Recebe um objInfTolerancia e gera um HashCode
    ''' </summary>
    ''' <param name="objInfTolerancia"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 - Criado
    ''' </history>
    Public Shared Function CheckSumInfTolerancias(objInfTolerancia As ContractoServicio.Utilidad.CheckSumInfTolerancias.CheckSumInfTolerancias) As String

        Dim checkSumGen As New StringBuilder
        Dim checkSumHash As String

        If objInfTolerancia.BolMedioPago = True Then

            checkSumGen.Append(RetornaMedioPagoInfTolerancia(objInfTolerancia.MediosPago)) _
            .Append(RetornaDivisaInfTolerancia(objInfTolerancia.Divisas))

        Else

            checkSumGen.Append(RetornaAgrupacionInfTolerancia(objInfTolerancia.Agrupaciones))

        End If

        'Chama o metodo GerarHash e gera um HashCode com a string informada.
        checkSumHash = (GerarHash(checkSumGen).ToString)

        Return checkSumHash
    End Function

    ''' <summary>
    ''' Concatena uma coleção de medio pago e retorna uma string.
    ''' </summary>
    ''' <param name="objColMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaMedioPagoInfTolerancia(objColMedioPago As ContractoServicio.Utilidad.CheckSumInfTolerancias.MedioPagoColeccion) As StringBuilder

        Dim strMedioPago As New StringBuilder

        For Each objMedioPago As ContractoServicio.Utilidad.CheckSumInfTolerancias.MedioPago In objColMedioPago.OrderBy(Function(mp) mp.IndentificadorMedioPago)

            strMedioPago.Append(objMedioPago.IndentificadorMedioPago).Append(objMedioPago.ToleranciaParcialMin) _
            .Append(objMedioPago.ToleranciaParcialMax).Append(objMedioPago.ToleranciaBultoMin) _
            .Append(objMedioPago.ToleranciaBultoMax).Append(objMedioPago.ToleranciaRemesaMin).Append(objMedioPago.ToleranciaRemesaMax)

        Next

        Return strMedioPago
    End Function


    ''' <summary>
    ''' Concatena uma coleção de divisa e retorna uma string.
    ''' </summary>
    ''' <param name="objColDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 31/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaDivisaInfTolerancia(objColDivisa As ContractoServicio.Utilidad.CheckSumInfTolerancias.DivisaColeccion) As StringBuilder

        Dim strDivisa As New StringBuilder

        For Each objDivisa As ContractoServicio.Utilidad.CheckSumInfTolerancias.Divisa In objColDivisa.OrderBy(Function(d) d.Codigo)

            strDivisa.Append(objDivisa.Codigo).Append(objDivisa.ToleranciaParcialMin) _
            .Append(objDivisa.ToleranciaParcialMax).Append(objDivisa.ToleranciaBultoMin) _
            .Append(objDivisa.ToleranciaBultoMax).Append(objDivisa.ToleranciaRemesaMin).Append(objDivisa.ToleranciaRemesaMax)

        Next

        Return strDivisa
    End Function

    ''' <summary>
    ''' Concatena uma coleção de agrupações e retorna uma string.
    ''' </summary>
    ''' <param name="objColAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    Private Shared Function RetornaAgrupacionInfTolerancia(objColAgrupacion As ContractoServicio.Utilidad.CheckSumInfTolerancias.AgrupacionColeccion) As StringBuilder

        Dim strAgrupacion As New StringBuilder

        For Each objAgrupacion As ContractoServicio.Utilidad.CheckSumInfTolerancias.Agrupacion In objColAgrupacion.OrderBy(Function(a) a.Descripcion)

            strAgrupacion.Append(objAgrupacion.Descripcion).Append(objAgrupacion.ToleranciaParcialMin) _
            .Append(objAgrupacion.ToleranciaParcialMax).Append(objAgrupacion.ToleranciaBultoMin).Append(objAgrupacion.ToleranciaBultoMax) _
            .Append(objAgrupacion.ToleranciaRemesaMin).Append(objAgrupacion.ToleranciaRemesaMax)

        Next

        Return strAgrupacion
    End Function

    ''' <summary>
    ''' Retorna o nome do servidor de banco de dados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornaNomeServidorBD() As String

        ' Retorna o nome do servidor de banco de dados
        Return AcessoDados.RecuperarConexao(Constantes.CONEXAO_GE).Servidor

    End Function

    ''' <summary>
    ''' Verifica se o valor do filtro foi informado.
    ''' Se foi informado, adiciona campo na query e no command
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/01/2011 criado
    ''' </history>
    Public Shared Sub AdicionarFiltroOpcionalLike(NomeCampo As String, TipoCampo As ProsegurDbType, ValorCampo As Object, _
                                              ByRef comando As IDbCommand, ByRef Where As StringBuilder)

        Dim campos As New List(Of Object)
        Dim campoPreenchido As Boolean = False

        If ValorCampo.GetType() Is GetType(String) Then

            If Not String.IsNullOrEmpty(ValorCampo) Then
                campoPreenchido = True
            End If

        End If

        If campoPreenchido Then
            ' adiciona um filtro do tipo like
            campos.Add(ValorCampo)
            Where.Append(MontarClausulaLikeUpper(campos, NomeCampo, comando, "AND"))
        End If

    End Sub

    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/11/2009 Criado
    ''' </history>
    Public Shared Function AtribuirValorObj(Valor As Object, _
                                               TipoCampo As System.Type) As Object

        Dim Campo As New Object

        If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
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
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(DateTime) Then
                Campo = Convert.ToDateTime(Valor)
            End If
        Else

            If TipoCampo Is GetType(Decimal) OrElse TipoCampo Is GetType(Int16) OrElse TipoCampo Is GetType(Int32) OrElse TipoCampo Is GetType(Int64) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(String) Then
                Campo = String.Empty
            Else
                Campo = Nothing
            End If

        End If

        Return Campo
    End Function


    ''' <summary>
    ''' Deserializa o xml de acordo com o type
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Adans.Klevanskis] 26/03/2013 Criado
    ''' </history>
    Public Shared Function DeSerializa(xml As String, type As Type) As Object

        Dim read As New StringReader(xml)
        Dim ser As New Serialization.XmlSerializer(type)
        Dim obj = ser.Deserialize(read)
        Return obj

    End Function

    ''' <summary>
    ''' serializa o xml de acordo com o type
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Adans.Klevanskis] 26/03/2013 Criado
    ''' </history>
    Public Shared Function Serializa(obj As Object) As String

        Dim write As New StringWriter()
        Dim ser As New Serialization.XmlSerializer(obj.GetType)
        ser.Serialize(write, obj)
        Return write.ToString()

    End Function


End Class
