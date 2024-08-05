Imports Prosegur.DbHelper
Imports Prosegur

<Serializable()> _
Public Class HistoricoInventario

#Region "[VARIÁVEIS]"

    Private _IdInventario As Long
    Private _IdCentroProceso As Long
    Private _FechaEmision As Date
    Private _ArchivoPDF As Byte()
    Private _ArchivoExcel As Byte()

#End Region

#Region "[PROPRIEDADES]"

    Public Property IdInventario() As Long
        Get
            Return _IdInventario
        End Get
        Set(Value As Long)
            _IdInventario = Value
        End Set
    End Property

    Public Property IdCentroProceso() As Long
        Get
            Return _IdCentroProceso
        End Get
        Set(Value As Long)
            _IdCentroProceso = Value
        End Set
    End Property

    Public Property FechaEmision() As Date
        Get
            Return _FechaEmision
        End Get
        Set(Value As Date)
            _FechaEmision = Value
        End Set
    End Property

    Public Property ArchivoPDF() As Byte()
        Get
            Return _ArchivoPDF
        End Get
        Set(Value As Byte())
            _ArchivoPDF = Value
        End Set
    End Property

    Public Property ArchivoExcel() As Byte()
        Get
            Return _ArchivoExcel
        End Get
        Set(Value As Byte())
            _ArchivoExcel = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Registra os dados do histórico do inventário no banco
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Registrar()

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = Nothing

        ' Se o objeto não está vazio
        If (Me IsNot Nothing) Then

            ' Define a conexão com o banco de dados
            comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
            ' Define o tipo do comando
            comando.CommandType = CommandType.Text
            ' Define o script que será executado
            comando.CommandText = My.Resources.HistoricoInventarioRegistrar

            ' Define os parametros que são usados na execução do script
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Identificador_Numerico, Me.IdCentroProceso))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ArchivoPDF", ProsegurDbType.Binario, Me.ArchivoPDF))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ArchivoExcel", ProsegurDbType.Binario, Me.ArchivoExcel))

            ' Executa o comando
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

        End If

    End Sub

    Public Sub RealizarArquivoExportado(ByRef TipoArquivo As Negocio.Enumeradores.Relatorio_Exportado_Tipo)

        ' Cria a variável que vai receber o comando
        Dim comando As IDbCommand = Nothing

        ' Define a conexão com o banco de dados
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' Define o tipo do comando
        comando.CommandType = CommandType.Text

        ' Define os parametros que são usados na execução do script
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdInventario", ProsegurDbType.Identificador_Numerico, Me.IdInventario))

        ' Verifica qual o tipo do arquivo exportado
        If (TipoArquivo = Enumeradores.Relatorio_Exportado_Tipo.Excel) Then
            ' Define o script que será executado
            comando.CommandText = My.Resources.HistoricoInventarioExcel
        Else
            ' Define o script que será executado
            comando.CommandText = My.Resources.HistoricoInventarioPDF
        End If

        ' Executa o comando
        Dim dtHistoricoInventario = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' Verifica se a consulta retornou dados
        If (dtHistoricoInventario IsNot Nothing AndAlso dtHistoricoInventario.Rows.Count > 0) Then

            ' Para cada linha retornada
            For Each drHistoricoInventario As DataRow In dtHistoricoInventario.Rows

                ' Verifica qual o tipo do arquivo exportado
                If (TipoArquivo = Enumeradores.Relatorio_Exportado_Tipo.Excel) Then
                    ' Atribui o relatório do excel
                    If (drHistoricoInventario("ARCHIVOEXCEL") IsNot Nothing) Then
                        Me.ArchivoExcel = drHistoricoInventario("ARCHIVOEXCEL")
                    End If
                Else
                    ' Atribui o relatório do PDF
                    If (drHistoricoInventario("ARCHIVOPDF") IsNot Nothing) Then
                        Me.ArchivoPDF = drHistoricoInventario("ARCHIVOPDF")
                    End If
                End If
            Next

        End If

    End Sub

#End Region

End Class