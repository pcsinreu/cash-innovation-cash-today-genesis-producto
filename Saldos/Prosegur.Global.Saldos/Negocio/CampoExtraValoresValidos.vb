Imports Prosegur.DbHelper

<Serializable()> _
Public Class CampoExtraValoresValidos
    Inherits List(Of CampoExtra)

#Region "[VARIÁVEIS]"

    Private _CampoExtra As CampoExtra

#End Region

#Region "[PROPRIEDADES]"

    Public Property CampoExtra() As CampoExtra
        Get
            If _CampoExtra Is Nothing Then
                Return New CampoExtra()
            End If
            CampoExtra = _CampoExtra
        End Get
        Set(Value As CampoExtra)
            _CampoExtra = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.CampoExtraValoresValidosRealizar.ToString()
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Identificador_Numerico, Me.CampoExtra.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto usuario
            Dim objCampoExtra As CampoExtra = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto usuario
                objCampoExtra = New CampoExtra
                objCampoExtra.Id = dr("Id")
                objCampoExtra.Nombre = dr("Descripcion")

                ' adicionar para colecao
                Me.Add(objCampoExtra)

            Next

        End If

    End Sub

#End Region

End Class