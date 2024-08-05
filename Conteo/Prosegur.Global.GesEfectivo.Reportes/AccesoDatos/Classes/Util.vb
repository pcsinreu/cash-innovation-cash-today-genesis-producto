Imports System.Configuration.ConfigurationManager
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.CriptoHelper
Imports System.Data.OracleClient

''' <summary>
''' Utilidades
''' </summary>
''' <remarks></remarks>
''' <history>
''' [magnum.oliveira] 17/07/2009 Criado
''' </history>
Public Class Util

    ''' <summary>
    ''' Retorna o prefixo do parâmetro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009	Criado
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
    ''' [magnum.oliveira] 16/07/2009	Criado
    ''' </history>
    Public Shared Function PrepararQuery(sql As String) As String

        Return sql.Replace("[]", RetornarPrefixoParametro)

    End Function

    Public Shared Function VerificarDBNull(Valor As Object) As Object

        If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
            Return Valor
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Campo"></param>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009	Criado
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
                Campo = Convert.ToDateTime(Valor.ToString.Trim).ToString("dd/MM/yyyy")
            ElseIf TipoCampo Is GetType(DictionaryEntry) Then
                Campo.Add(Valor, Nothing)
            End If
        Else
            Campo = Nothing
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
    ''' 
    ''' </summary>
    ''' <param name="coluna"></param>
    ''' <param name="direcao"></param>
    ''' <param name="valor"></param>
    ''' <param name="tipo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CriarParametroOracle(coluna As String, _
                                                direcao As ParameterDirection, _
                                                valor As Object, _
                                                tipo As OracleType) As OracleParameter

        ' inicializar variáveis

        Dim p As OracleParameter = New OracleClient.OracleParameter

        ' montar parameter
        p.ParameterName = coluna
        p.Direction = direcao
        p.Value = valor
        p.OracleType = tipo

        ' retorna o parameter preenchido

        Return p

    End Function

    ''' <summary>
    ''' Retorna uma string com todos os valores passados, separados por aspas simples e vírgula.
    ''' </summary>
    ''' <param name="listaValores"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornaStringListaValores(listaValores As List(Of String)) As String

        Dim valores As String = ""

        If listaValores IsNot Nothing AndAlso Not listaValores.Count = 0 Then
            For Each valor In listaValores
                valores += "'" + valor + "',"
            Next
            valores = valores.Substring(0, (valores.Length - 1))
        End If

        Return valores

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

End Class
