Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class HistoricoInventarios
    Inherits List(Of HistoricoInventario)

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Recupera os historicos do inventario
    ''' </summary>
    ''' <param name="IdCentroProcesso">Código do centro de processo</param>
    ''' <param name="DataIni">Data Inicial</param>
    ''' <param name="DataFim">DataFinal</param>
    ''' <remarks></remarks>
    Public Sub RecuperarHistoricoInventario(IdCentroProcesso As Long, DataIni As DateTime, DataFim As DateTime)

        ' Declara a data inicial que será usada na pesquisa
        Dim dataInicial As DateTime = DateTime.MinValue

        ' Se a data não for informada
        If (DataIni <> DateTime.MinValue) Then
            ' Configura a data inicial que será usado na pesquisa
            dataInicial = New DateTime(DataIni.Year, DataIni.Month, DataIni.Day, Date.MinValue.Hour, Date.MinValue.Minute, Date.MinValue.Second)
        End If

        ' Declara a data final que será usada na pesquisa
        Dim dataFinal As DateTime = DateTime.MinValue

        ' Se a data não for informada
        If (DataFim <> DateTime.MinValue) Then
            ' Configura a data final que será usado na pesquisa
            dataFinal = New DateTime(DataFim.Year, DataFim.Month, DataFim.Day, Date.MaxValue.Hour, Date.MaxValue.Minute, Date.MaxValue.Second)
        End If

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = Nothing

        ' Define a conexão com o banco de dados
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' Define o tipo do comando
        comando.CommandType = CommandType.Text

        ' Define o script que será executado
        comando.CommandText = My.Resources.HistoricoInventario

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Identificador_Numerico, IdCentroProcesso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DataIni", ProsegurDbType.Data_Hora, IIf(dataInicial = DateTime.MinValue, DBNull.Value, dataInicial)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DataFim", ProsegurDbType.Data_Hora, IIf(dataFinal = DateTime.MinValue, DBNull.Value, dataFinal)))

        ' Executa o comando
        Dim dtHistoricoInventario = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' Verifica se a consulta retornou dados
        If (dtHistoricoInventario IsNot Nothing AndAlso dtHistoricoInventario.Rows.Count > 0) Then

            Dim historicoInventario As HistoricoInventario

            ' Para cada linha retornada
            For Each drHistoricoInventario As DataRow In dtHistoricoInventario.Rows

                ' Instância o historico de inventário
                historicoInventario = New HistoricoInventario

                ' Adiciona o código do inventário
                If (drHistoricoInventario("IDINVENTARIO") IsNot Nothing) Then
                    historicoInventario.IdInventario = drHistoricoInventario("IDINVENTARIO")
                End If

                ' Adiciona o data de emissão
                If (drHistoricoInventario("FECHAEMISION") IsNot Nothing) Then
                    historicoInventario.FechaEmision = drHistoricoInventario("FECHAEMISION")
                End If

                ' Adiciona o historico na listagem
                Me.Add(historicoInventario)

            Next

        End If

    End Sub

#End Region

End Class