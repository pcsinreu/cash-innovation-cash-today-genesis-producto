Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis.AccesoDatos

''' <summary>
''' Classe Genérica de Acesso a Dados.
''' </summary>
''' <history>
''' [Thiago Dias] 25/09/2013 - Criado.
'''</history>
Class HelperBuscaDatos

    ''' <summary>
    ''' Pesquisa por dados do Helper no Banco utilizando os campos Código e Descrição como filtro de dados.
    ''' </summary>
    Public Shared Function BuscaDatos(queryBusca As StringBuilder, objPeticion As PeticionHelper, paramPaginacion As ParametrosRespuestaPaginacion, _
                                       nomeColunaCodigo As String, nomeColunaDescricao As String) As DataTable

        Dim helperDatos As New HelperDatos.Util
        Dim query As New StringBuilder
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' Cria comando.
        cmd.CommandText = helperDatos.GenerarComandoBusca(queryBusca, objPeticion, nomeColunaCodigo, nomeColunaDescricao)
        cmd.CommandType = CommandType.Text

        ' Realiza Pesquisa.
        Return PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, paramPaginacion)

    End Function


    ''' <summary>
    ''' Pesquisa por dados do Helper no Banco utilizando o campo Identificador como filtro de dados.
    ''' </summary>
    Public Shared Function BuscaDatos(queryBusca As StringBuilder, objPeticion As PeticionHelper, paramPaginacion As ParametrosRespuestaPaginacion, _
                                       nomeColunaCodigo As String, nomeColunaDescricao As String, nomeColunaID As String) As DataTable

        Dim helperDatos As New HelperDatos.Util
        Dim query As New StringBuilder
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' Cria comando.
        cmd.CommandText = helperDatos.GenerarComandoBusca(queryBusca, objPeticion, nomeColunaCodigo, nomeColunaDescricao, nomeColunaID)
        cmd.CommandType = CommandType.Text

        ' Realiza Pesquisa.
        Return PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, paramPaginacion)

    End Function

    Public Shared Function BuscaDatosPuesto(queryBusca As StringBuilder, objPeticion As PeticionHelperPuesto, paramPaginacion As ParametrosRespuestaPaginacion, _
                                     nomeColunaCodigo As String, nomeColunaDescricao As String, nomeColunaID As String, nomeColunaVigente As String) As DataTable

        Dim helperDatos As New HelperDatos.Util
        Dim query As New StringBuilder
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' Cria comando.
        cmd.CommandText = helperDatos.GenerarComandoBuscaPuesto(queryBusca, objPeticion, nomeColunaCodigo, nomeColunaDescricao, nomeColunaID, nomeColunaVigente)
        cmd.CommandType = CommandType.Text

        ' Realiza Pesquisa.
        Return PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, paramPaginacion)

    End Function

    Public Shared Function BuscaDatosTipoContenedor(queryBusca As StringBuilder, objPeticion As PeticionHelperTipoContenedor, paramPaginacion As ParametrosRespuestaPaginacion, _
                                    nomeColunaCodigo As String, nomeColunaDescricao As String, nomeColunaID As String, nomeColunaVigente As String) As DataTable

        Dim helperDatos As New HelperDatos.Util
        Dim query As New StringBuilder
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' Cria comando.
        cmd.CommandText = helperDatos.GenerarComandoBuscaTipoContenedor(queryBusca, objPeticion, nomeColunaCodigo, nomeColunaDescricao, nomeColunaID, nomeColunaVigente)
        cmd.CommandType = CommandType.Text

        ' Realiza Pesquisa.
        Return PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, paramPaginacion)

    End Function

    ''' <summary>
    ''' Retorna resultado da consulta no formato de Lista de Dados.
    ''' </summary>
    ''' <remarks>
    ''' OBSERVACAO: Para o correto funcionamento da funcionalidade a query utilizada na busca deve seguir a seguinte estrutura:
    ''' 
    ''' Definir os Alias conforme a sequencia descrita abaixo:
    ''' 
    ''' ** CASO A TABELA NAO POSSUA RELACIONAMENTO **    
    '''1 - COD_IDENTIFICADOR
    '''2 - CODIGO
    '''3 - DESCRICAO
    '''4 - VIGENTE
    '''5,6, ETC - DEMAIS CAMPOS A RETORNAR.        
    '''
    '''** CASO A TABELA POSSUA RELACIONAMENTO **
    '''1 - COD_IDENTIFICADOR
    '''2 - COD_IDENTIFICADOR_PAI (CHAVE_TABELA_RELACIONAMENTO)
    '''3 - CODIGO
    '''4 - DESCRICAO
    '''5 - VIGENTE
    '''6,7, ETC - DEMAIS CAMPOS A RETORNAR.
    ''' </remarks>
    Public Shared Function ListaDatosRespuesta(dtDatos As DataTable) As List(Of Helper.Respuesta)

        Dim lstRespuesta As New List(Of Helper.Respuesta)

        ' Verifica se a consulta retornou dados e implementa lista de dados resposta.
        If (dtDatos IsNot Nothing AndAlso dtDatos.Rows.Count > 0) Then
            For Each dr As DataRow In dtDatos.Rows
                Dim respuesta As New Helper.Respuesta
                With respuesta

                    .Identificador = dr.Item(0).ToString()

                    ' Verifica existência do campo Identificador Pai.
                    If (dr.Table.Columns.Item(1).ColumnName.Contains("IDENTIFICADOR_PAI")) Then
                        .IdentificadorPai = dr.Item(1).ToString()
                        .Codigo = dr.Item(2).ToString()
                        .Descricao = dr.Item(3).ToString()
                    Else
                        .IdentificadorPai = Nothing
                        .Codigo = dr.Item(1).ToString()
                        .Descricao = dr.Item(2).ToString()
                    End If

                End With
                lstRespuesta.Add(respuesta)
            Next
        End If

        Return lstRespuesta

    End Function

    Public Shared Function ListaDatosRespuestaPuesto(dtDatos As DataTable) As List(Of Helper.RespuestaPuesto)

        Dim lstRespuesta As New List(Of Helper.RespuestaPuesto)

        ' Verifica se a consulta retornou dados e implementa lista de dados resposta.
        If (dtDatos IsNot Nothing AndAlso dtDatos.Rows.Count > 0) Then
            For Each dr As DataRow In dtDatos.Rows
                Dim respuesta As New Helper.RespuestaPuesto
                With respuesta

                    .Identificador = dr.Item(0).ToString()

                    ' Verifica existência do campo Identificador Pai.
                    If (dr.Table.Columns.Item(1).ColumnName.Contains("IDENTIFICADOR_PAI")) Then
                        .IdentificadorPai = dr.Item(1).ToString()
                        .Codigo = dr.Item(2).ToString()
                        .CodigoHost = dr.Item(3).ToString()
                        .Vigente = Int32.Parse(dr.Item(4).ToString())
                    Else
                        .IdentificadorPai = Nothing
                        .Codigo = dr.Item(1).ToString()
                        .CodigoHost = dr.Item(2).ToString()
                        .Vigente = Int32.Parse(dr.Item(3).ToString())
                    End If

                End With
                lstRespuesta.Add(respuesta)
            Next
        End If

        Return lstRespuesta

    End Function

    Public Shared Function ListaDatosRespuestaTipoContenedor(dtDatos As DataTable) As List(Of Helper.RespuestaHelperTipoContenedorDatos)

        Dim lstRespuesta As New List(Of Helper.RespuestaHelperTipoContenedorDatos)

        ' Verifica se a consulta retornou dados e implementa lista de dados resposta.
        If (dtDatos IsNot Nothing AndAlso dtDatos.Rows.Count > 0) Then
            For Each dr As DataRow In dtDatos.Rows
                Dim respuesta As New Helper.RespuestaHelperTipoContenedorDatos
                With respuesta

                    .Identificador = dr.Item(0).ToString()

                    ' Verifica existência do campo Identificador Pai.
                    If (dr.Table.Columns.Item(1).ColumnName.Contains("IDENTIFICADOR_PAI")) Then
                        .IdentificadorPai = dr.Item(1).ToString()
                        .Codigo = dr.Item(2).ToString()
                        .Descricao = dr.Item(3).ToString()
                        .NecCantidad = IIf(String.IsNullOrEmpty(dr.Item(4).ToString), 0, CInt(dr.Item(4)))
                        .ValorMaximoImporte = IIf(String.IsNullOrEmpty(dr.Item(5).ToString), "0", CInt(dr.Item(5)))
                        .CodUnidadeMedida = IIf(String.IsNullOrEmpty(dr.Item(6).ToString), String.Empty, dr.Item(6).ToString)
                        .IdentificadorUnidadeMedida = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String))
                        .DesUnidadeMedida = Util.AtribuirValorObj(dr("DES_UNIDAD_MEDIDA"), GetType(String))
                        .NumValorUnidadeMedida = Util.AtribuirValorObj(dr("NUM_VALOR_UNIDAD"), GetType(String))
                        .CodTipoUnidadeMedida = Util.AtribuirValorObj(dr("COD_TIPO_UNIDAD_MEDIDA"), GetType(String))
                        .LlevaPrecinto = Util.AtribuirValorObj(dr("BOL_LLEVA_PRECINTO"), GetType(Boolean))
                    Else
                        .IdentificadorPai = Nothing
                        .Codigo = dr.Item(1).ToString()
                        .Descricao = dr.Item(2).ToString()
                        .NecCantidad = IIf(String.IsNullOrEmpty(dr.Item(3).ToString), 0, dr.Item(3))
                        .ValorMaximoImporte = IIf(String.IsNullOrEmpty(dr.Item(4).ToString), 0, dr.Item(4))
                        .CodUnidadeMedida = IIf(String.IsNullOrEmpty(dr.Item(5).ToString), String.Empty, dr.Item(5).ToString)
                        .IdentificadorUnidadeMedida = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String))
                        .DesUnidadeMedida = Util.AtribuirValorObj(dr("DES_UNIDAD_MEDIDA"), GetType(String))
                        .NumValorUnidadeMedida = Util.AtribuirValorObj(dr("NUM_VALOR_UNIDAD"), GetType(String))
                        .CodTipoUnidadeMedida = Util.AtribuirValorObj(dr("COD_TIPO_UNIDAD_MEDIDA"), GetType(String))
                        .LlevaPrecinto = Util.AtribuirValorObj(dr("BOL_LLEVA_PRECINTO"), GetType(Boolean))
                    End If

                End With
                lstRespuesta.Add(respuesta)
            Next
        End If

        Return lstRespuesta

    End Function

End Class
