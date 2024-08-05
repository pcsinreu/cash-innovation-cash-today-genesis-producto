Imports System.Text
Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class Motivos
    Inherits List(Of Motivo)

#Region "[VARIÁVEIS]"

    Private _CentroProceso As CentroProceso

#End Region

#Region "[PROPRIEDADES]"

    Public Property CentroProceso() As CentroProceso
        Get
            If _CentroProceso Is Nothing Then
                _CentroProceso = New CentroProceso()
            End If
            Return _CentroProceso
        End Get
        Set(Value As CentroProceso)
            _CentroProceso = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.MotivosRealizar.ToString
        comando.CommandType = CommandType.Text

        If CentroProceso.Id <> 0 Then

            comando.CommandText = My.Resources.MotivosRealizarPorCentroProceso.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Identificador_Numerico, Me.CentroProceso.Id))

        End If

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto motivo
            Dim objMotivo As Motivo = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                objMotivo = New Motivo
                objMotivo.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objMotivo.Descripcion = dr("Descripcion")
                Else
                    objMotivo.Descripcion = String.Empty
                End If

                If dr("Accion") IsNot DBNull.Value Then
                    objMotivo.Accion = dr("Accion")
                Else
                    objMotivo.Accion = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objMotivo)

            Next

        End If

    End Sub

#End Region

End Class