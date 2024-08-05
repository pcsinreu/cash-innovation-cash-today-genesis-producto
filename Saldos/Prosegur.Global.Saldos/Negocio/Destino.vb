Imports Prosegur.DbHelper
Imports Prosegur
Imports System.Data.SqlClient

<Serializable()> _
Public Class Destino

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _IdGenesis As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

    Public Property IdGenesis() As String
        Get
            Return _IdGenesis
        End Get
        Set(value As String)
            _IdGenesis = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DestinoRealizar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDestino", ProsegurDbType.Identificador_Numerico, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")

            If dt.Rows(0)("IdGenesis") IsNot DBNull.Value Then
                Me.IdGenesis = dt.Rows(0)("IdGenesis")
            Else
                Me.IdGenesis = String.Empty
            End If

        End If

    End Sub

    Public Sub Registrar()

        If Me.Id <> 0 Then
            AlterarDestino()
        End If

    End Sub

    Private Sub AlterarDestino()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.DestinoRegistrarUpdate.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDestino", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGenesis", ProsegurDbType.Descricao_Curta, Me.IdGenesis))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

#End Region

End Class