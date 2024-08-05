Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.Generic
Imports System.Data
Imports System.Text

<Serializable()> _
Public Class HistoricoSaldosCongelados
    Inherits List(Of HistoricoSaldoCongelado)

#Region "[METODOS]"

    ''' <summary>
    ''' Recupera o saldo congelado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/09/2010 - Criado
    ''' </history>
    Public Sub Realizar(fecha As DateTime)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SaldoCongeladoSelecionarByFecha.ToString
        comando.CommandType = CommandType.Text

        Dim FechaInicial As DateTime = DateAdd(DateInterval.Minute, 1, fecha.Date)
        Dim FechaFinal As DateTime = DateAdd(DateInterval.Day, 1, fecha.Date)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAGENERACIONINICIAL", ProsegurDbType.Data, FechaInicial))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAGENERACIONFINAL", ProsegurDbType.Data, FechaFinal))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objSaldo As HistoricoSaldoCongelado = Nothing

            For Each dr In dt.Rows

                objSaldo = New HistoricoSaldoCongelado

                objSaldo.IdSaldoCongelado = dr("IDSALDOCONGELADO")
                objSaldo.Fecha = dr("FECHAGENERACION")

                Me.Add(objSaldo)

            Next

        End If

    End Sub

#End Region

End Class
