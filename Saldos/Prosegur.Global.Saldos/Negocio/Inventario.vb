Imports System.Text
Imports Prosegur.DbHelper
Imports System.Collections.Generic
Imports System.Data
Imports System.Configuration.ConfigurationManager
Imports System.Data.OracleClient

<Serializable()> _
Public Class Inventario

#Region "[Variáveis]"

    Private _RS As DataTable
    Private _IdInventario As Long
    Private Shared _Versao As String

#End Region

#Region "[Propriedades]"

    Public Property RS() As DataTable
        Get
            Return _RS
        End Get
        Set(value As DataTable)
            _RS = value
        End Set
    End Property

    Public Property IdInventario() As Long
        Get
            Return _IdInventario
        End Get
        Set(value As Long)
            _IdInventario = value
        End Set
    End Property



#End Region

#Region "[Métodos]"

    ''' <summary>
    ''' Metodo que consulta os dados do inventário na tabela InvetarioDetalle e InventarioCabecera
    ''' </summary>
    ''' <param name="CentroProceso">Id do Centro de Processo para novo Inventario</param>
    ''' <param name="IdCentroProceso">Id do Centro de Processo para Historico</param>
    ''' <param name="OrdenarPor">Modo de ordenação</param>
    ''' <param name="NewNum">Se for historico, deverá ser informado o numero do inventário, senão informar -1 para novo inventário</param>
    ''' <remarks></remarks>
    Public Sub HistoricoInventarioListarV9(CentroProceso As Integer, _
                                           IdCentroProceso As Integer, _
                                           OrdenarPor As Integer, _
                                           NewNum As Integer,
                                           FeechaEmision As Date)

        Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Início Método: Inventario.HistoricoInventarioListarV9", "LOG_INVETARIO.txt")

        Dim CentroProcesoInterno As Integer
        Dim IdCentroProcesoInterno As Integer
        Dim NumeroInventario As Long

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' preparar chamada procedure
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "HistoricoInventListV9_" & Prosegur.Genesis.Comon.Util.Version

        If NewNum < 1 Then

            Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Início Chamada Método: Inventario.InventarioSeleccionarV9", "LOG_INVETARIO.txt")

            'Criando Inventario
            InventarioSeleccionarV9(CentroProceso, FeechaEmision)

            Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Fim Chamada Método: Inventario.InventarioSeleccionarV9", "LOG_INVETARIO.txt")

            NumeroInventario = Me.IdInventario
            CentroProcesoInterno = 0
            IdCentroProcesoInterno = CentroProceso

        Else

            NumeroInventario = NewNum
            CentroProcesoInterno = CentroProceso
            IdCentroProcesoInterno = IdCentroProceso

        End If

        ' adicionar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_CentroProceso", ProsegurDbType.Inteiro_Longo, CentroProcesoInterno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdCP", ProsegurDbType.Inteiro_Longo, IdCentroProcesoInterno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_Ordenarpor", ProsegurDbType.Inteiro_Longo, OrdenarPor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_Inv", ProsegurDbType.Inteiro_Longo, NumeroInventario))
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

        Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Início Execução Procedure: Inventario.HistoricoInventarioListarV9", "LOG_INVETARIO.txt")

        ' executar datatable
        Me.RS = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Fim Execução Procedure: Inventario.HistoricoInventarioListarV9", "LOG_INVETARIO.txt")

        If NumeroInventario > -1 Then
            Me.IdInventario = NumeroInventario
        End If

        Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Fim Método: Inventario.HistoricoInventarioListarV9", "LOG_INVETARIO.txt")

    End Sub

    Public Sub HistoricoInventarioDisponiblesV9(IdCentroProcesso As Long, DataIni As DateTime, DataFim As DateTime)

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = Nothing

        ' Define a conexão com o banco de dados
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' Define o tipo do comando
        comando.CommandType = CommandType.Text

        ' Define o script que será executado
        comando.CommandText = My.Resources.HistoricoInventarioDisponiblesV9

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Identificador_Numerico, IdCentroProcesso))

        ' Se a data não for informada
        If (DataIni <> DateTime.MinValue) Then
            ' Configura a data inicial que será usado na pesquisa
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaIni", ProsegurDbType.Data_Hora, DataIni))
        End If

        ' Se a data não for informada
        If (DataFim <> DateTime.MinValue) Then
            ' Configura a data final que será usado na pesquisa
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaFin", ProsegurDbType.Data_Hora, DataFim))
        End If

        ' Executa o comando
        RS = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Private Sub InventarioSeleccionarV9(CentroProceso As Integer,
                                        FechaEmision As Date)

        Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Início Método: Inventario.InventarioSeleccionarV9", "LOG_INVETARIO.txt")

        ' inicializar variaveis
        Dim Cabecera As Integer = 0
        Dim IdDocDetalles As Integer = -1

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' preparar chamada procedure
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "InventarioSelV9_" & Prosegur.Genesis.Comon.Util.Version

        ' adicionar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdCentroProceso", ProsegurDbType.Inteiro_Longo, CentroProceso))
        comando.Parameters.Add(Util.CriarParametroOracle("v_IdInventario", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Number))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FechaEmision", ProsegurDbType.Data_Hora, FechaEmision))

        Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Início Execução Procedure: Inventario.InventarioSeleccionarV9", "LOG_INVETARIO.txt")

        ' executar datatable
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

        Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Fim Execução Procedure: Inventario.InventarioSeleccionarV9", "LOG_INVETARIO.txt")

        Dim paramVal = CType(comando.Parameters("v_IdInventario"), OracleParameter).Value
        If (paramVal IsNot DBNull.Value) Then
            Me.IdInventario = paramVal
        End If
        Util.LogMensagemEmDisco("Thread " & System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() & ": Fim Método: Inventario.InventarioSeleccionarV9", "LOG_INVETARIO.txt")

    End Sub

    Private Sub InventarioCabeceraInsertarV9(IdCP As Integer, _
                                             IdInventario As Integer, _
                                             FechaEmision As DateTime, _
                                             ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.InventarioCabeceraInsertar.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, IdCP))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdInventario", ProsegurDbType.Inteiro_Longo, IdInventario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaInventario", ProsegurDbType.Data_Hora, FechaEmision))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub InventarioDetalleInsertarV9(IdCP As Integer, _
                                            IdInventario As Integer, _
                                            IdDocumento As Integer, _
                                            IdMoneda As Integer, _
                                            Importe As Decimal, _
                                            Tipo As String, _
                                            IdEstado As Integer, _
                                            IdDocDetalles As Integer, _
                                            ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.InventarioDetalleInsertar.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, IdCP))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdInventario", ProsegurDbType.Inteiro_Longo, IdInventario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Iddocumento", ProsegurDbType.Inteiro_Longo, IdDocumento))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, IdMoneda))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Importe", ProsegurDbType.Numero_Decimal, Importe))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Tipo", ProsegurDbType.Descricao_Longa, Tipo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoComprobante", ProsegurDbType.Inteiro_Longo, IdEstado))

        If IdDocDetalles = -1 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocDetalles", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocDetalles", ProsegurDbType.Inteiro_Longo, IdDocDetalles))
        End If


        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Remove o Invetario inserido
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Criado 08/09/2009
    ''' </history>
    ''' <remarks></remarks>
    Public Sub InventarioRemoverV9()

        Dim objTransacao As New DbHelper.Transacao(Constantes.CONEXAO_SALDOS)

        If Me.IdInventario <> 0 Then

            InventarioCabeceraRemoverV9(Me.IdInventario, objTransacao)

            InventarioDetalleRemoverV9(Me.IdInventario, objTransacao)

            objTransacao.RealizarTransacao()

        End If

    End Sub

    ''' <summary>
    ''' Remove o Invetario inserido
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Criado 08/09/2009
    ''' </history>
    ''' <remarks></remarks>
    Private Sub InventarioCabeceraRemoverV9(IdInventario As Integer, ByRef Transacao As DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' preparar chamada procedure
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.InventarioCabeceraRemover.ToString

        ' adicionar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idinventario", ProsegurDbType.Inteiro_Longo, IdInventario))

        Transacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Remove o Invetario inserido
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Criado 08/09/2009
    ''' </history>
    ''' <remarks></remarks>
    Private Sub InventarioDetalleRemoverV9(IdInventario As Integer, ByRef Transacao As DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' preparar chamada procedure
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.InventarioDetalleRemover.ToString

        ' adicionar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idinventario", ProsegurDbType.Inteiro_Longo, IdInventario))

        Transacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

End Class