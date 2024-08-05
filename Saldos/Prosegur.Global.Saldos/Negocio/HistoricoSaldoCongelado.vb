Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

<Serializable()> _
Public Class HistoricoSaldoCongelado

#Region "[VARIÁVEIS]"

    Private _idSaldoCongelado As Integer
    Private _fecha As DateTime

#End Region

#Region "[PROPRIEDADES]"

    Public Property Fecha() As DateTime
        Get
            Return _fecha
        End Get
        Set(value As DateTime)
            _fecha = value
        End Set
    End Property

    Public Property IdSaldoCongelado() As Integer
        Get
            Return _idSaldoCongelado
        End Get
        Set(value As Integer)
            _idSaldoCongelado = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Obtem o oid da coluna IdSaldoCongelado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterIdSaldoCongelado() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.SSaldoCongelado.ToString()

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    ''' <summary>
    ''' Recupera o saldo congelado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/09/2010 - Criado
    ''' </history>
    Public Shared Function Selecionar() As DataTable

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SaldoCongeladoSelecionar.ToString
        comando.CommandType = CommandType.Text

        ' executar comando
        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
    End Function

    ''' <summary>
    ''' Recupera o ultimo registro inserido
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/09/2010 - Criado
    ''' </history>
    Public Shared Function SelecionarUltimaExecución() As DateTime

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SaldoCongeladoRecuperarUltimoRegistro.ToString
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return Convert.ToDateTime(dt.Rows(0)("FECHAGENERACION"))
        End If

        Return Date.MinValue
    End Function

    ''' <summary>
    ''' Insere o saldo congelado na tabela de saldos congelados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/09/2010 - Criado
    ''' </history>
    Public Shared Sub InserirSaldoCongelado(hora As DateTime)

        Dim byteSaldoCongelado() As Byte = Util.SerializarDT(Selecionar)

        If byteSaldoCongelado IsNot Nothing Then

            ' inicializar variáveis
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

            ' obter comando
            comando.CommandText = My.Resources.SaldoCongeladoInserir
            comando.CommandType = CommandType.Text

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDSALDOCONGELADO", ProsegurDbType.Identificador_Alfanumerico, ObterIdSaldoCongelado))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SALDOCONGELADO", ProsegurDbType.Binario, byteSaldoCongelado))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FECHAGENERACION", ProsegurDbType.Data, Convert.ToDateTime(DateTime.Now.Date & " " & hora)))

            'Executa o comando
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

        End If

    End Sub

#End Region

End Class
