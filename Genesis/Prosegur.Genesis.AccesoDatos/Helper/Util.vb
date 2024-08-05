Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Namespace HelperDatos

    ''' <summary>
    ''' Mantém funcionalidades a serem compartilhadas por todas as classes Helpers.
    ''' </summary>
    ''' <history>
    ''' [Thiago Dias] 04/09/2013 - Criado.
    ''' [Thiago Dias] 15/10/2013 - Modificado.
    '''</history>
    Public Class Util

        Private Shared cmdSQL As String
        Private Shared aliasTabelaA As Char
        Private Shared aliasTabelaB As Char
        Private Shared lstAlias As Char() = {"X", "Y", "Z", "W", "K", "Q"}
        Private Shared filtroTratar As String

        ''' <summary>
        ''' Enumerador que define a coluna da tabela que será utilizada como filtro.
        ''' </summary>
        Enum ColunaFiltro
            Codigo
            Descricao
            Identificador
            Vigente
        End Enum

        ''' <summary>
        ''' Implementa os filtros da consulta.
        ''' </summary>
        ''' <param name="queryDefault">Valor da query a ser alterada para inclusão do filtro.</param>
        ''' <param name="filtro">Filtro a ser incluído na consulta.</param>
        Protected Function ImplementarClausulaWhere(queryDefault As String, filtro As String) As String

            If (queryDefault.Contains(" WHERE ")) Then
                If (filtro.Contains("WHERE")) Then
                    filtro = filtro.Replace("WHERE", "AND")
                ElseIf (Not filtro.Contains(" AND ")) Then
                    filtro = filtro.Insert(0, " AND ")
                End If
            Else
                filtro = If(Not filtro.Contains(" WHERE "), filtro.Insert(0, " WHERE "), filtro)
            End If

            Return filtro

        End Function

        ''' <summary>
        ''' Implementa os filtros da consulta.
        ''' </summary>
        ''' <param name="queryDefault">Valor da query a ser alterada para inclusão do filtro.</param>
        ''' <param name="colunaFiltro">Tipo de coluna a ser utilizada no filtro.</param>
        ''' <param name="nomeColuna">Nome da coluna que o filtro deve ser atribuído.</param>
        ''' <param name="valorFiltro">Filtro a ser incluído na consulta.</param>
        ''' <param name="aliasTabela">Alias da tabela que armazena o filtro a ser incluído na consulta.</param>
        ''' <param name="usarLike">Indica se consulta será feita por like ou não.</param>
        Protected Function TratarFiltro(queryDefault As String, colunaFiltro As Util.ColunaFiltro,
                                     nomeColuna As String, valorFiltro As String, aliasTabela As Char, Optional usarLike As Boolean = True) As String

            'Tratar aspas simples
            valorFiltro = valorFiltro.Replace("'", "''")

            Select Case colunaFiltro
                Case ColunaFiltro.Codigo, ColunaFiltro.Descricao
                    If usarLike Then
                        filtroTratar = "UPPER(" + aliasTabela + "." + nomeColuna + ") LIKE '%" & valorFiltro.ToUpper() & "%'"
                    Else
                        filtroTratar = "UPPER(" + aliasTabela + "." + nomeColuna + ") = '" & valorFiltro.ToUpper() & "'"
                    End If

                Case Else
                    filtroTratar = String.Empty
            End Select

            Return ImplementarClausulaWhere(queryDefault, filtroTratar)

        End Function

        ''' <summary>
        ''' Implementa os filtros da consulta.
        ''' </summary>
        ''' <param name="queryDefault">Valor da query a ser alterada para inclusão do filtro.</param>
        ''' <param name="colunaFiltro">Tipo de coluna a ser utilizada no filtro.</param>
        ''' <param name="nomeColuna">Nome da coluna que o filtro deve ser atribuído.</param>
        ''' <param name="lstFiltro">Lista de Identificadores a serem utilizados como Filtro na consulta.</param>
        ''' <param name="aliasTabela">Alias da tabela que armazena o filtro a ser incluído na consulta.</param>
        Protected Function TratarFiltro(queryDefault As String, colunaFiltro As Util.ColunaFiltro,
                                     nomeColuna As String, lstFiltro As List(Of String), aliasTabela As Char, incluirOperadorOR As Boolean) As String

            Select Case colunaFiltro
                Case ColunaFiltro.Identificador

                    filtroTratar = If(incluirOperadorOR, "OR ", String.Empty)
                    If aliasTabela.Equals(" "c) Then
                        filtroTratar += nomeColuna + " IN ("
                    Else
                        filtroTratar += aliasTabela + "." + nomeColuna + " IN ("
                    End If

                    ' Percorre pelos identificadores selecionados.
                    For Each id In lstFiltro
                        filtroTratar += "'" & id & "',"
                    Next

                    filtroTratar = filtroTratar.Remove(filtroTratar.Length - 1)
                    filtroTratar += ")"
                Case Else
                    filtroTratar = String.Empty
            End Select

            Return ImplementarClausulaWhere(queryDefault, filtroTratar)

        End Function

        ''' <summary>        
        ''' Implementa os filtros da consulta.
        ''' </summary>
        ''' <param name="queryDefault">Valor da query a ser alterada para inclusão do filtro.</param>
        ''' <param name="filtro">Dicionário contendo nome da coluna e valor do filtro a ser incluído na consulta.</param>
        ''' <param name="aliasTabela">Alias da tabela que armazena o filtro a ser incluído na consulta.</param>        
        Protected Function TratarFiltro(queryDefault As String, filtro As KeyValuePair(Of Tabela, List(Of ArgumentosFiltro)), aliasTabela As Char) As String
            Dim filtros As String = String.Empty

            'Recupera os filtros que utilizam a codição igual
            For Each colunaFiltro In (From f In filtro.Value
                                      Select f.NomeColuna, f.TipoCondicaoFiltro).Distinct

                Dim valorFiltro = String.Join("', '", filtro.Value.Where(Function(v) v.NomeColuna = colunaFiltro.NomeColuna AndAlso v.TipoCondicaoFiltro = colunaFiltro.TipoCondicaoFiltro).Select(Function(v) v.ValorFiltro).Distinct().ToArray)
                Dim condicaoFiltroValor As String = String.Empty

                If colunaFiltro.TipoCondicaoFiltro = Helper.Enumeradores.EnumHelper.TipoCondicion.Igual Then
                    If valorFiltro.Contains(",") Then
                        condicaoFiltroValor = " IN('{0}')"
                    Else
                        condicaoFiltroValor = " ='{0}'"
                    End If

                ElseIf colunaFiltro.TipoCondicaoFiltro = Helper.Enumeradores.EnumHelper.TipoCondicion.Diferente Then
                    If valorFiltro.Contains(",") Then
                        condicaoFiltroValor = " NOT IN('{0}')"
                    Else
                        condicaoFiltroValor = " !='{0}'"
                    End If

                ElseIf colunaFiltro.TipoCondicaoFiltro = Helper.Enumeradores.EnumHelper.TipoCondicion.Avancado Then
                    condicaoFiltroValor = " {0}"
                End If

                condicaoFiltroValor = String.Format(condicaoFiltroValor, valorFiltro)

                'Se a coluna for OID, então não utlizar upper
                If colunaFiltro.NomeColuna.ToUpper().StartsWith("OID_") Then
                    filtroTratar = ImplementarClausulaWhere(queryDefault, aliasTabela + "." & colunaFiltro.NomeColuna & condicaoFiltroValor)
                Else
                    filtroTratar = ImplementarClausulaWhere(queryDefault, "UPPER(" + aliasTabela + "." & colunaFiltro.NomeColuna & ")" & condicaoFiltroValor)
                End If

                If Not (String.IsNullOrEmpty(filtros)) Then
                    If (filtros.Contains("WHERE")) Then
                        filtroTratar = filtroTratar.Replace("WHERE", " AND")
                    End If
                End If

                filtros += filtroTratar
            Next

            Return filtros

        End Function

        ''' <summary>
        ''' Gerar um novo valor de Alias para a tabela.
        ''' </summary>
        ''' <param name="tabela">Nome da Tabela de onde será obtido o Alias.</param>
        ''' <param name="indiceBuscar">Valor do índice onde será obtido o Alias.</param>
        Protected Function GerarNovoAlias(tabela As String, Optional indiceBuscar As Integer = 0) As Char

            Dim aux As Integer
            Dim aliasTabela As Char

            aux = tabela.IndexOf("_")
            indiceBuscar = indiceBuscar + 2

            Try
                If (indiceBuscar < tabela.Length) Then
                    aliasTabela = tabela.Substring((aux + indiceBuscar), 1)
                Else
                    aliasTabela = lstAlias(0)
                End If
            Catch
                aliasTabela = lstAlias(0)
            End Try

            Return aliasTabela

        End Function

        ''' <summary>
        ''' Retorna valor do Alias inserido na query.
        ''' </summary>
        ''' <param name="query">Query de pesquisa padrão.</param>
        Protected Function ObterAliasResource(query As String) As String
            'Recupera os alias da tabela ou nome da tabela
            Dim NomeTabela As String = String.Empty
            'retura os espeaços no final
            query = query.Trim

            'recupera o último espaço, e depois recupera nome da tabela ou alias
            NomeTabela = query.Substring(query.LastIndexOf(" ")).Trim()

            Return NomeTabela
        End Function

        ''' <summary>
        ''' Inclui parâmetros de ordenação da query.
        ''' </summary>
        ''' <param name="queryDefault">Query Original a ser implementada.</param>
        ''' <param name="colunaOrdenar">Nome da Coluna a ser utilizada como parâmetro da ordenação.</param>
        Protected Function IncluirOrdenacao(queryDefault As String, colunaOrdenar As Object) As String

            If (queryDefault.Contains("ORDER")) Then
                Return (", " + colunaOrdenar)
            Else
                Return (" ORDER BY " + colunaOrdenar)
            End If

        End Function

        ''' <summary>
        ''' Inclui informações do tipo (Filtro | Ordenação | Join) na query original.
        ''' </summary>
        Protected Function ImplementarComandoBusca(query As StringBuilder, peticion As PeticionHelper, nomeColCodigo As String, nomeColDescricao As String,
                                                   nomeColIdentificador As String) As StringBuilder

            Dim existeJuncao As Boolean
            Dim lstIDs As New List(Of String)
            Dim operadorOR As Boolean

            ' Alias da Tabela Principal.
            Dim aliasTabPrincipal As Char = ObterAliasResource(query.ToString())

            ' Nome da Tabela Principal da consulta.
            Dim nomeTabela As String = ObterNomeTabela(query.ToString())

            ' Valida existência da clausula Join.
            If (peticion.JuncaoSQL IsNot Nothing AndAlso peticion.JuncaoSQL.Count > 0) Then
                Dim cmdJoin As Dictionary(Of String, String) = TratarJuncaoConsulta(peticion.JuncaoSQL, nomeTabela, aliasTabPrincipal)
                query.Replace(query.ToString(), IncluirJoin(query.ToString(), cmdJoin))
                existeJuncao = True
            End If

            ' Valida existência do filtro Identificador.
            If (Not String.IsNullOrEmpty(nomeColIdentificador) AndAlso (peticion IsNot Nothing AndAlso peticion.DadosPeticao IsNot Nothing AndAlso peticion.DadosPeticao.Count > 0)) Then
                If (peticion.FiltroAvanzadoSQL IsNot Nothing AndAlso peticion.FiltroAvanzadoSQL.Count > 0) Then

                    Dim likeChar As String = String.Empty
                    Dim operador As String = " = "
                    If peticion.UsarLike Then
                        likeChar = "%"
                        operador = " LIKE "
                    End If

                    For Each item In peticion.FiltroAvanzadoSQL

                        If item.Value.ToString.Contains("&CODIGO") Then
                            query = query.Replace(item.Key, item.Value.Replace("&CODIGO", operador & "'" & likeChar & peticion.Codigo & likeChar & "'"))
                        End If
                        If item.Value.ToString.Contains("&DESCRIPCION") Then
                            query = query.Replace(item.Key, item.Value.Replace("&DESCRIPCION", operador & "'" & likeChar & peticion.Descripcion & likeChar & "'"))
                        End If
                    Next

                    For Each itemPeticion In peticion.DadosPeticao
                        lstIDs.Add(itemPeticion.Identificador)
                    Next

                    query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Identificador, nomeColIdentificador, lstIDs, " "c, operadorOR))

                Else
                    For Each itemPeticion In peticion.DadosPeticao
                        lstIDs.Add(itemPeticion.Identificador)
                    Next

                    query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Identificador, nomeColIdentificador, lstIDs, aliasTabPrincipal, operadorOR))
                End If
            Else

                If (peticion.FiltroAvanzadoSQL IsNot Nothing AndAlso peticion.FiltroAvanzadoSQL.Count > 0) Then
                    Dim likeChar As String = String.Empty
                    Dim operador As String = " = "
                    If peticion.UsarLike Then
                        likeChar = "%"
                        operador = " LIKE "
                    End If

                    'Validar campo DESCRIPCION y construir consulta Where
                    Dim paramDescripcion As String = "&DESCRIPCION"
                    Dim queryCondition As String = ""
                    If peticion.FiltroAvanzadoSQL.Any(Function(e) e.Value.Contains(paramDescripcion)) AndAlso peticion.Descripcion IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(peticion.Descripcion) Then
                        Dim param = peticion.FiltroAvanzadoSQL.Where(Function(e) e.Value.Contains(paramDescripcion)).FirstOrDefault()
                        queryCondition = UCase(param.Value.Replace(paramDescripcion, operador & "'" & likeChar & peticion.Descripcion & likeChar & "'"))
                    End If

                    For Each item In peticion.FiltroAvanzadoSQL

                        If item.Value.ToString.Contains("&CODIGO") Then
                            Dim objReplace As String = item.Value.Replace("&CODIGO", operador & "'" & likeChar & UCase(peticion.Codigo) & likeChar & "'")
                            objReplace = objReplace & queryCondition
                            query = query.Replace(item.Key, objReplace)
                        End If
                        If item.Value.ToString.Contains("&DESCRIPCION") AndAlso String.IsNullOrWhiteSpace(queryCondition) Then
                            query = query.Replace(item.Key, UCase(item.Value.Replace("&DESCRIPCION", operador & "'" & likeChar & peticion.Descripcion & likeChar & "'")))
                        End If

                    Next

                Else
                    ' Valida existência do filtro Código.
                    If (Not String.IsNullOrEmpty(nomeColCodigo) AndAlso Not String.IsNullOrEmpty(peticion.Codigo)) Then
                        query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Codigo, nomeColCodigo, peticion.Codigo, aliasTabPrincipal, peticion.UsarLike))
                        operadorOR = True
                    End If

                    ' Valida existência do filtro Descrição.
                    If (Not String.IsNullOrEmpty(nomeColDescricao) AndAlso Not String.IsNullOrEmpty(peticion.Descripcion)) Then
                        query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Descricao, nomeColDescricao, peticion.Descripcion, aliasTabPrincipal, peticion.UsarLike))
                        operadorOR = True
                    End If
                End If


            End If



            ' Valida existência de filtros no objeto Peticion.
            If (peticion.FiltroSQL IsNot Nothing AndAlso peticion.FiltroSQL.Count > 0) Then
                For Each item In peticion.FiltroSQL
                    ' Valida se o filtro pertence a Tabela Principal da consulta.
                    If (item.Key.Tabela.RecuperarValor.Equals(nomeTabela)) Then
                        query.Append(TratarFiltro(query.ToString(), item, aliasTabPrincipal))
                    Else
                        ' Valida se o filtro pertence à alguma tabela da clausula Join.
                        If (existeJuncao) Then
                            Dim auxAlias As Char
                            For Each juncao In peticion.JuncaoSQL
                                If (item.Key.Tabela = juncao.Value.TabelaEsquerda.Tabela) Then
                                    auxAlias = ObterAliasTabelaJoin(query.ToString(), juncao.Value.TabelaEsquerda.Tabela.RecuperarValor())
                                    query.Append(TratarFiltro(query.ToString(), item, auxAlias))
                                    Exit For
                                ElseIf (item.Key.Tabela = juncao.Value.TabelaDireita.Tabela) Then
                                    auxAlias = ObterAliasTabelaJoin(query.ToString(), juncao.Value.TabelaDireita.Tabela.RecuperarValor())
                                    query.Append(TratarFiltro(query.ToString(), item, auxAlias))
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Next
            End If



            ' Insere Ordenação.
            If (peticion.OrdenacaoSQL IsNot Nothing AndAlso peticion.OrdenacaoSQL.Count > 0) Then
                For Each order In peticion.OrdenacaoSQL
                    query.Append(IncluirOrdenacao(query.ToString(), order.Value.ColunaOrdenacao))
                Next
            End If

            Return query

        End Function

        ''' <summary>
        ''' Insere Junção na consulta.
        ''' </summary>    
        Protected Function IncluirJoin(queryDefault As String, cmdJoin As Dictionary(Of String, String)) As String

            Dim aux As String = String.Empty
            Dim filtroQuery As String = String.Empty
            Dim filtroJoin As String = String.Empty
            Dim posInicio As Integer = 0

            If (queryDefault.Contains("WHERE")) Then

                posInicio = queryDefault.IndexOf("WHERE")
                filtroQuery = queryDefault.Substring(posInicio)
                aux = queryDefault.Substring(0, posInicio)

                For Each item In cmdJoin
                    aux += item.Key

                    If (String.IsNullOrEmpty(item.Value)) Then
                        Continue For
                    End If

                    filtroJoin += IIf(filtroJoin.Length > 0, (" AND " + item.Value), item.Value)
                Next

                queryDefault = String.Empty
                filtroQuery = filtroQuery.Replace("WHERE", "AND")

                ' Gera nova query incluindo os joins existentes.
                queryDefault = (aux + " " + If(Not String.IsNullOrEmpty(filtroJoin), " WHERE " + filtroJoin, String.Empty) + " " + filtroQuery)
            Else
                For Each item In cmdJoin
                    aux += item.Key

                    If (String.IsNullOrEmpty(item.Value)) Then
                        Continue For
                    End If

                    filtroJoin += IIf(filtroJoin.Length > 0, (" AND " + item.Value), item.Value)
                Next

                queryDefault += (aux + " " + If(Not String.IsNullOrEmpty(filtroJoin), " WHERE " + filtroJoin, String.Empty))
            End If

            Return queryDefault

        End Function

        ''' <summary>
        ''' Implementa Cláusula Join.
        ''' </summary>
        ''' <param name="dadosJuncao">Parâmetros a serem utilizados na montagem da cláusula Join da consulta.</param>
        ''' <param name="tabelaPrincipal">Nome da tabela principal da consulta.</param>
        ''' <param name="aliasTabPrincipal">Alias atribuído à tabela principal.</param>    
        Protected Function TratarJuncaoConsulta(dadosJuncao As Dictionary(Of String, UtilHelper.JoinSQL), tabelaPrincipal As String, aliasTabPrincipal As Char) As Dictionary(Of String, String)

            Dim cont As Integer = 0
            Dim contJoin As Integer = dadosJuncao.Count
            Dim joinConsulta As New Dictionary(Of String, String)
            Dim listaDeAlias As New List(Of Tuple(Of String, String))

            listaDeAlias.Add(New Tuple(Of String, String)(tabelaPrincipal, aliasTabPrincipal))

            For i = 0 To contJoin - 1
                cmdSQL = String.Empty

                If Not String.IsNullOrEmpty(dadosJuncao.Values(i).JoinPersonalizado) Then
                    joinConsulta.Add(dadosJuncao.Values(i).JoinPersonalizado, String.Empty)
                Else
                    If Not String.IsNullOrEmpty(dadosJuncao.Keys(i)) AndAlso Not (dadosJuncao.Values(i).TabelaEsquerda.Tabela.RecuperarValor() = tabelaPrincipal) Then

                        ' Obtém alias da Tabela à esquerda da consulta na junção.
                        For index = 0 To dadosJuncao.Values(i).TabelaEsquerda.Tabela.RecuperarValor().Length - 1 Step 1
                            aliasTabelaA = GerarNovoAlias(dadosJuncao.Values(i).TabelaEsquerda.Tabela.RecuperarValor(), index)
                            'If Not (aliasTabelaA.Equals(aliasTabPrincipal)) Then Exit For

                            If Not listaDeAlias.Exists(Function(al) al.Item2 = aliasTabelaA) Then Exit For
                        Next

                        If (aliasTabelaA.ToString().Length < 1) Then
                            Do Until (Not (aliasTabelaA.Equals(aliasTabPrincipal)))
                                aliasTabelaA = lstAlias(cont)
                                cont += 1
                            Loop
                        End If

                        cmdSQL = " JOIN "
                        cmdSQL += dadosJuncao.Values(i).TabelaEsquerda.Tabela.RecuperarValor() + " " + aliasTabelaA + " ON "
                        cmdSQL += aliasTabelaA + "." + dadosJuncao.Values(i).CampoComumTabEsq + " = "

                        'Adiciona o alias na lista de alias
                        listaDeAlias.Add(New Tuple(Of String, String)(dadosJuncao.Values(i).TabelaEsquerda.Tabela.RecuperarValor(), aliasTabelaA))

                        ' Obtém alias da Tabela à direita da consulta na junção.
                        If (dadosJuncao.Values(i).TabelaDireita.Tabela.RecuperarValor() = tabelaPrincipal) Then
                            cmdSQL += aliasTabPrincipal + "." + dadosJuncao.Values(i).CampoComumTabDireita
                        Else

                            Dim _valor As Integer = i
                            Dim tabelaAlias = listaDeAlias.Find(Function(a) a.Item1 = dadosJuncao.Values(_valor).TabelaDireita.Tabela.RecuperarValor())

                            If tabelaAlias Is Nothing Then
                                For index = 0 To dadosJuncao.Values(i).TabelaDireita.Tabela.RecuperarValor().Length - 1 Step 1
                                    aliasTabelaB = GerarNovoAlias(dadosJuncao.Values(i).TabelaDireita.Tabela.RecuperarValor(), index)
                                    'If Not (aliasTabelaB.Equals(aliasTabPrincipal)) Then Exit For
                                    If Not listaDeAlias.Exists(Function(al) al.Item2 = aliasTabelaB) Then Exit For
                                Next

                                If (aliasTabelaB.ToString().Length < 1) Then
                                    Do Until (Not (aliasTabelaB.Equals(aliasTabPrincipal)))
                                        aliasTabelaB = lstAlias(cont)
                                        cont += 1
                                    Loop
                                End If
                                'Adiciona o alias na lista de alias
                                listaDeAlias.Add(New Tuple(Of String, String)(dadosJuncao.Values(i).TabelaDireita.Tabela.RecuperarValor(), aliasTabelaB))
                            Else
                                'Recupera o alias da tabela
                                aliasTabelaB = tabelaAlias.Item2
                            End If

                            cmdSQL += aliasTabelaB + "." + dadosJuncao.Values(i).CampoComumTabDireita

                        End If

                        If (Not String.IsNullOrEmpty(dadosJuncao.Values(i).ValorCampoChave)) Then
                            ' Valida existência de multiplos filtros.
                            If (dadosJuncao.Values(i).ValorCampoChave.Contains(",")) Then
                                Dim filtros As String() = dadosJuncao.Values(i).ValorCampoChave.Split(",")
                                Dim filtroTratado As String = String.Empty

                                For Each filtro As String In filtros
                                    filtroTratado += IIf(String.IsNullOrEmpty(filtroTratado), "(", ", ")
                                    filtroTratado += ("'" + filtro.Trim() + "'")
                                Next

                                filtroTratado += ")"

                                ' Dictionary(Key => Cláusula Join | Value => filtro cláusula join).
                                joinConsulta.Add(cmdSQL, (aliasTabelaA + "." + dadosJuncao.Values(i).NomeCampoChave + " IN " + filtroTratado + ""))
                            Else
                                ' Dictionary(Key => Cláusula Join | Value => filtro cláusula join).
                                joinConsulta.Add(cmdSQL, (aliasTabelaA + "." + dadosJuncao.Values(i).NomeCampoChave + " = '" + dadosJuncao.Values(i).ValorCampoChave + "'"))
                            End If
                        Else
                            joinConsulta.Add(cmdSQL, String.Empty)
                        End If
                    End If
                End If
            Next

            Return joinConsulta
        End Function

        Protected Function ImplementarComandoBuscaTipoContenedor(query As StringBuilder, peticion As PeticionHelperTipoContenedor, nomeColCodigo As String, nomeColDescricao As String,
                                                  nomeColIdentificador As String) As StringBuilder

            Dim existeJuncao As Boolean
            Dim lstIDs As New List(Of String)
            Dim operadorOR As Boolean

            ' Alias da Tabela Principal.
            Dim aliasTabPrincipal As Char = ObterAliasResource(query.ToString())

            ' Nome da Tabela Principal da consulta.
            Dim nomeTabela As String = ObterNomeTabela(query.ToString())

            ' Valida existência da clausula Join.
            If (peticion.JuncaoSQL IsNot Nothing AndAlso peticion.JuncaoSQL.Count > 0) Then
                Dim cmdJoin As Dictionary(Of String, String) = TratarJuncaoConsulta(peticion.JuncaoSQL, nomeTabela, aliasTabPrincipal)
                query.Replace(query.ToString(), IncluirJoin(query.ToString(), cmdJoin))
                existeJuncao = True
            End If

            ' Valida existência do filtro Identificador.
            If (Not String.IsNullOrEmpty(nomeColIdentificador) AndAlso (peticion IsNot Nothing AndAlso peticion.DadosPeticao IsNot Nothing AndAlso peticion.DadosPeticao.Count > 0)) Then
                For Each itemPeticion In peticion.DadosPeticao
                    lstIDs.Add(itemPeticion.Identificador)
                Next

                query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Identificador, nomeColIdentificador, lstIDs, aliasTabPrincipal, operadorOR))
            Else
                ' Valida existência do filtro Código.
                If (Not String.IsNullOrEmpty(nomeColCodigo) AndAlso Not String.IsNullOrEmpty(peticion.Codigo)) Then
                    query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Codigo, nomeColCodigo, peticion.Codigo, aliasTabPrincipal, peticion.UsarLike))
                    operadorOR = True
                End If

                ' Valida existência do filtro Descrição.
                If (Not String.IsNullOrEmpty(nomeColDescricao) AndAlso Not String.IsNullOrEmpty(peticion.Descripcion)) Then
                    query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Descricao, nomeColDescricao, peticion.Descripcion, aliasTabPrincipal, peticion.UsarLike))
                    operadorOR = True
                End If
            End If

            ' Valida existência de filtros no objeto Peticion.
            If (peticion.FiltroSQL IsNot Nothing AndAlso peticion.FiltroSQL.Count > 0) Then
                For Each item In peticion.FiltroSQL
                    ' Valida se o filtro pertence a Tabela Principal da consulta.
                    If (item.Key.Tabela.RecuperarValor.Equals(nomeTabela)) Then
                        query.Append(TratarFiltro(query.ToString(), item, aliasTabPrincipal))
                    Else
                        ' Valida se o filtro pertence à alguma tabela da clausula Join.
                        If (existeJuncao) Then
                            Dim auxAlias As Char
                            For Each juncao In peticion.JuncaoSQL
                                If (item.Key.Tabela = juncao.Value.TabelaEsquerda.Tabela) Then
                                    auxAlias = ObterAliasTabelaJoin(query.ToString(), juncao.Value.TabelaEsquerda.Tabela.RecuperarValor())
                                    query.Append(TratarFiltro(query.ToString(), item, auxAlias))
                                    Exit For
                                ElseIf (item.Key.Tabela = juncao.Value.TabelaDireita.Tabela) Then
                                    auxAlias = ObterAliasTabelaJoin(query.ToString(), juncao.Value.TabelaDireita.Tabela.RecuperarValor())
                                    query.Append(TratarFiltro(query.ToString(), item, auxAlias))
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Next
            End If

            ' Insere Ordenação.
            If (peticion.OrdenacaoSQL IsNot Nothing AndAlso peticion.OrdenacaoSQL.Count > 0) Then
                For Each order In peticion.OrdenacaoSQL
                    query.Append(IncluirOrdenacao(query.ToString(), order.Value.ColunaOrdenacao))
                Next
            End If

            Return query

        End Function

        ''' <summary>
        ''' Recupera nome da tabela principal inserida na consulta.
        ''' </summary>
        ''' <param name="queryDefault">Estrura da Query.</param>
        Protected Function ObterNomeTabela(queryDefault As String) As String

            Dim posFrom As Integer = queryDefault.Trim().IndexOf("FROM")
            Dim nomeTabela As String = String.Empty

            queryDefault = queryDefault.Substring(posFrom).Trim

            'retira o from
            queryDefault = queryDefault.Replace("FROM", String.Empty).Trim()

            Dim posEspaco As Integer = queryDefault.IndexOf(" ")

            If posEspaco > 0 Then
                nomeTabela = queryDefault.Substring(0, posEspaco).Trim
            Else
                nomeTabela = queryDefault.Substring(0).Trim
            End If

            Return nomeTabela.Replace("FROM", String.Empty).Trim()

        End Function

        ''' <summary>
        ''' Recupera o Alias da Tabela inserida na cláusula Join da consulta.
        ''' </summary>
        ''' <param name="queryDefault">Estrura da Query.</param>
        ''' <param name="tabelaObterAlias">Nome da Tabela a ser obtido o Alias.</param>        
        Protected Function ObterAliasTabelaJoin(queryDefault As String, tabelaObterAlias As String) As Char

            Dim posInicio As Integer = queryDefault.Trim().IndexOf(tabelaObterAlias)
            Dim posFim As Integer = tabelaObterAlias.Trim().Length
            Dim aliasTabela = queryDefault.Trim().Substring(posInicio, (posFim + 2))

            Return aliasTabela.Substring(aliasTabela.Length - 1)

        End Function

        ''' <summary>
        ''' Implementa comando sql para pesquisa de dados do Helper.
        ''' </summary>
        ''' <param name="query">Query Default de pesquisa da Tabela relacionada.</param>
        ''' <param name="peticion">Objeto petição Helper.</param>
        ''' <param name="nomeColCodigo">Nome da Coluna 'Código' da Tabela.</param>
        ''' <param name="nomeColDescricao">Nome da Coluna 'Descrição' da Tabela.</param>
        Public Function GenerarComandoBusca(query As StringBuilder, peticion As PeticionHelper, nomeColCodigo As String, nomeColDescricao As String) As String

            Return Me.ImplementarComandoBusca(query, peticion, nomeColCodigo, nomeColDescricao, String.Empty).ToString().Trim()

        End Function

        ''' <summary>
        ''' Implementa comando sql para pesquisa de dados do Helper.
        ''' </summary>
        ''' <param name="query">Query Default de pesquisa da Tabela relacionada.</param>
        ''' <param name="peticion">Objeto petição Helper.</param>
        ''' <param name="nomeColCodigo">Nome da Coluna 'Código' da Tabela.</param>
        ''' <param name="nomeColDescricao">Nome da Coluna 'Descrição' da Tabela.</param>
        ''' <param name="nomeColID">Nome da Coluna 'Identificador' da Tabela.</param>
        Public Function GenerarComandoBusca(query As StringBuilder, peticion As PeticionHelper, nomeColCodigo As String, nomeColDescricao As String, nomeColID As String) As String

            Return Me.ImplementarComandoBusca(query, peticion, nomeColCodigo, nomeColDescricao, nomeColID).ToString().Trim()

        End Function

        Public Function GenerarComandoBuscaPuesto(query As StringBuilder, peticion As PeticionHelperPuesto, nomeColCodigo As String, nomeColDescricao As String, nomeColID As String, nomeColunaVigente As String) As String

            Return Me.ImplementarComandoBuscaPuesto(query, peticion, nomeColCodigo, nomeColDescricao, nomeColID, nomeColunaVigente).ToString().Trim()

        End Function

        Public Function GenerarComandoBuscaTipocontenedor(query As StringBuilder, peticion As PeticionHelperTipoContenedor, nomeColCodigo As String, nomeColDescricao As String, nomeColID As String, nomeColunaVigente As String) As String

            Return Me.ImplementarComandoBuscaTipoContenedor(query, peticion, nomeColCodigo, nomeColDescricao, nomeColID).ToString().Trim()

        End Function

        ''' <summary>
        ''' Retorna valor do Enumerador da Tabela Helper.
        ''' </summary>        
        Private Function ObterNomeTabelaHelper(_tabelaHelper As TabelaHelper) As Object

            Select Case _tabelaHelper
                Case TabelaHelper.Calidad
                    Return TabelaHelper.Calidad.RecuperarValor()
                Case TabelaHelper.Canal
                    Return TabelaHelper.Canal.RecuperarValor()
                Case TabelaHelper.Cliente
                    Return TabelaHelper.Cliente.RecuperarValor()
                Case TabelaHelper.Delegacion
                    Return TabelaHelper.Delegacion.RecuperarValor()
                Case TabelaHelper.Denominacion
                    Return TabelaHelper.Denominacion.RecuperarValor()
                Case TabelaHelper.Divisa
                    Return TabelaHelper.Divisa.RecuperarValor()
                Case TabelaHelper.Formato
                    Return TabelaHelper.Formato.RecuperarValor()
                Case TabelaHelper.Formulario
                    Return TabelaHelper.Formulario.RecuperarValor()
                Case TabelaHelper.Mascara
                    Return TabelaHelper.Mascara.RecuperarValor()
                Case TabelaHelper.MedioPago
                    Return TabelaHelper.MedioPago.RecuperarValor()
                Case TabelaHelper.Pais
                    Return TabelaHelper.Pais.RecuperarValor()
                Case TabelaHelper.Planta
                    Return TabelaHelper.Planta.RecuperarValor()
                Case TabelaHelper.PuntoServicio
                    Return TabelaHelper.PuntoServicio.RecuperarValor()
                Case TabelaHelper.Sector
                    Return TabelaHelper.Sector.RecuperarValor()
                Case TabelaHelper.SubCanal
                    Return TabelaHelper.SubCanal.RecuperarValor()
                Case TabelaHelper.SubCliente
                    Return TabelaHelper.SubCliente.RecuperarValor()
                Case TabelaHelper.Termino
                    Return TabelaHelper.Termino.RecuperarValor()
                Case TabelaHelper.TipoBulto
                    Return TabelaHelper.TipoBulto.RecuperarValor()
                Case TabelaHelper.TipoSector
                    Return TabelaHelper.TipoSector.RecuperarValor()
                Case TabelaHelper.UnidadMedida
                    Return TabelaHelper.UnidadMedida.RecuperarValor()
                Case TabelaHelper.SectorXFormulario
                    Return TabelaHelper.SectorXFormulario.RecuperarValor()
                Case TabelaHelper.Parametro
                    Return TabelaHelper.Parametro.RecuperarValor()
            End Select

            Return Nothing
        End Function

        Protected Function ImplementarComandoBuscaPuesto(query As StringBuilder, peticion As PeticionHelperPuesto, nomeColCodigo As String, nomeColDescricao As String,
                                                  nomeColIdentificador As String, nomeColVigente As String) As StringBuilder

            Dim existeJuncao As Boolean
            Dim lstIDs As New List(Of String)
            Dim operadorOR As Boolean

            ' Alias da Tabela Principal.
            Dim aliasTabPrincipal As Char = ObterAliasResource(query.ToString())

            ' Nome da Tabela Principal da consulta.
            Dim nomeTabela As String = ObterNomeTabela(query.ToString())

            ' Valida existência da clausula Join.
            If (peticion.JuncaoSQL IsNot Nothing AndAlso peticion.JuncaoSQL.Count > 0) Then
                Dim cmdJoin As Dictionary(Of String, String) = TratarJuncaoConsulta(peticion.JuncaoSQL, nomeTabela, aliasTabPrincipal)
                query.Replace(query.ToString(), IncluirJoin(query.ToString(), cmdJoin))
                existeJuncao = True
            End If

            ' Valida existência do filtro Identificador.
            If (Not String.IsNullOrEmpty(nomeColIdentificador) AndAlso (peticion IsNot Nothing AndAlso peticion.DadosPeticao IsNot Nothing AndAlso peticion.DadosPeticao.Count > 0)) Then
                For Each itemPeticion In peticion.DadosPeticao
                    lstIDs.Add(itemPeticion.Identificador)
                Next

                query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Identificador, nomeColIdentificador, lstIDs, aliasTabPrincipal, operadorOR))
            Else
                ' Valida existência do filtro Código.
                If (Not String.IsNullOrEmpty(nomeColCodigo) AndAlso Not String.IsNullOrEmpty(peticion.Codigo)) Then
                    query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Codigo, nomeColCodigo, peticion.Codigo, aliasTabPrincipal, peticion.UsarLike))
                    operadorOR = True
                End If

                ' Valida existência do filtro Descrição.
                If (Not String.IsNullOrEmpty(nomeColDescricao) AndAlso Not String.IsNullOrEmpty(peticion.Descripcion)) Then
                    query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Descricao, nomeColDescricao, peticion.Descripcion, aliasTabPrincipal, peticion.UsarLike))
                    operadorOR = True
                End If
                ' Valida existência do filtro Descrição.
                If (Not String.IsNullOrEmpty(nomeColVigente) AndAlso Not String.IsNullOrEmpty(peticion.Vigente.ToString())) Then
                    query.Append(TratarFiltro(query.ToString(), Util.ColunaFiltro.Vigente, nomeColVigente, peticion.Vigente.ToString(), aliasTabPrincipal, peticion.UsarLike))
                    operadorOR = False
                End If
            End If

            ' Valida existência de filtros no objeto Peticion.
            If (peticion.FiltroSQL IsNot Nothing AndAlso peticion.FiltroSQL.Count > 0) Then
                For Each item In peticion.FiltroSQL
                    ' Valida se o filtro pertence a Tabela Principal da consulta.
                    If (item.Key.Tabela.RecuperarValor.Equals(nomeTabela)) Then
                        query.Append(TratarFiltro(query.ToString(), item, aliasTabPrincipal))
                    Else
                        ' Valida se o filtro pertence à alguma tabela da clausula Join.
                        If (existeJuncao) Then
                            Dim auxAlias As Char
                            For Each juncao In peticion.JuncaoSQL
                                If (item.Key.Tabela = juncao.Value.TabelaEsquerda.Tabela) Then
                                    auxAlias = ObterAliasTabelaJoin(query.ToString(), juncao.Value.TabelaEsquerda.Tabela.RecuperarValor())
                                    query.Append(TratarFiltro(query.ToString(), item, auxAlias))
                                    Exit For
                                ElseIf (item.Key.Tabela = juncao.Value.TabelaDireita.Tabela) Then
                                    auxAlias = ObterAliasTabelaJoin(query.ToString(), juncao.Value.TabelaDireita.Tabela.RecuperarValor())
                                    query.Append(TratarFiltro(query.ToString(), item, auxAlias))
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Next
            End If

            ' Insere Ordenação.
            If (peticion.OrdenacaoSQL IsNot Nothing AndAlso peticion.OrdenacaoSQL.Count > 0) Then
                For Each order In peticion.OrdenacaoSQL
                    query.Append(IncluirOrdenacao(query.ToString(), order.Value.ColunaOrdenacao))
                Next
            End If

            Return query

        End Function

    End Class

End Namespace
