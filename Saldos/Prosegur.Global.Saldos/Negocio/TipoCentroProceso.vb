Imports Prosegur.DbHelper

<Serializable()> _
Public Class TipoCentroProceso

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _IdPS As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property IdPS() As String
        Get
            IdPS = _IdPS
        End Get
        Set(Value As String)
            _IdPS = Value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Descripcion = _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Id = _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.TipoCentroProcesoRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCentroProceso", ProsegurDbType.Identificador_Numerico, Me.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            If dt.Rows(0)("Descripcion") IsNot DBNull.Value Then
                Me.Descripcion = dt.Rows(0)("Descripcion")
            Else
                Me.Descripcion = String.Empty
            End If

            If dt.Rows(0)("IdPS") IsNot DBNull.Value Then
                Me.IdPS = dt.Rows(0)("IdPS").ToString
            Else
                Me.IdPS = String.Empty
            End If

        End If

    End Sub

    Public Function Registrar() As Short

        If Me.Id = 0 Then
            Me.Id = ObterIdTipoCentroProceso()
            TipoCentroProcesoInserir()
        Else
            TipoCentroProcesoAtualizar()
        End If

    End Function

    Private Sub TipoCentroProcesoInserir()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.TipoCentroProcesoRegistrarInsert.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idtipocentroproceso", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Private Sub TipoCentroProcesoAtualizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.TipoCentroProcesoRegistrarUpdate.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Sub Eliminar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.TipoCentroProcesoEliminar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Shared Function ObterIdTipoCentroProceso() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.STipoCentroProceso.ToString()
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

#End Region

End Class