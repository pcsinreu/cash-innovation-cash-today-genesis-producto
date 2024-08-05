Imports Prosegur.DbHelper
Imports System.Collections.Generic
Imports System.Text

<Serializable()> _
Public Class Automatas
    Inherits List(Of Automata)

    Public Sub Realizar()

        Dim filtro As New StringBuilder
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        filtro.Append(" ORDER BY ordenproceso, idautomata")

        ' obter query
        comando.CommandText = My.Resources.AutomatasRealizar.ToString() & filtro.ToString
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto automata
            Dim objAutomata As Automata = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto automata
                objAutomata = New Automata

                objAutomata.Id = dr("Id")
                objAutomata.Formulario.Id = dr("IdFormulario")

                If dr("RutaDeTrabajo") IsNot DBNull.Value Then
                    objAutomata.RutaDeTrabajo = dr("RutaDeTrabajo")
                End If

                objAutomata.Usuario.Id = dr("IdUsuario")
                objAutomata.ArchivosPorTurno = dr("ArchivosPorTurno")
                objAutomata.Exportador = dr("Exportador")
                objAutomata.Descripcion = dr("Descripcion")

                If dr("IdEstadoAExportar") IsNot DBNull.Value Then
                    objAutomata.EstadoAExportar.Id = dr("IdEstadoAExportar")
                End If

                If dr("RutaDeCadena") IsNot DBNull.Value Then
                    objAutomata.RutaDeCadena = dr("RutaDeCadena")
                End If

                If dr("FormatoExportacion") IsNot DBNull.Value Then
                    objAutomata.FormatoExportacion = dr("FormatoExportacion")
                End If

                If dr("DiasAProcesar") Is DBNull.Value Then
                    objAutomata.DiasAProcesar = 1
                Else
                    objAutomata.DiasAProcesar = dr("DiasAProcesar")
                End If

                ' adicionar para colecao
                Me.Add(objAutomata)

            Next

        End If

    End Sub

End Class