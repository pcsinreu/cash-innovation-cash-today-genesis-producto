Imports Prosegur.DbHelper
Imports System.Text
Imports System.Collections.Generic

<Serializable()> _
Public Class Campos
    Inherits List(Of Campo)

#Region "[VARIÁVEIS]"

    Private _Formulario As Formulario

#End Region

#Region "[PROPRIEDADES]"

    Public Property Formulario() As Formulario
        Get
            If _Formulario Is Nothing Then
                _Formulario = New Formulario()
            End If
            Return _Formulario
        End Get
        Set(Value As Formulario)
            _Formulario = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim filtros As New StringBuilder
        Dim comando As IDbCommand

        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.FormularioCampoRealizar.ToString()

        If Me.Formulario.Id > 0 Then
            comando.CommandText += filtros.Append(" WHERE IdFormulario = :IdFormulario ORDER BY C.Orden").ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Identificador_Numerico, Me.Formulario.Id))
        End If

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        Me.Clear()

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            Dim objCampo As Campo = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto campo
                objCampo = New Campo

                ' o valor do campo é vazio
                objCampo.Valor = String.Empty

                If dr("Clase") IsNot DBNull.Value Then
                    objCampo.Clase = dr("Clase")
                Else
                    objCampo.Clase = String.Empty
                End If

                If dr("Coleccion") IsNot DBNull.Value Then
                    objCampo.Coleccion = dr("Coleccion")
                Else
                    objCampo.Coleccion = String.Empty
                End If

                objCampo.Id = dr("Id")

                If dr("Nombre") IsNot DBNull.Value Then
                    objCampo.Nombre = dr("Nombre")
                Else
                    objCampo.Nombre = String.Empty
                End If

                If dr("Etiqueta") IsNot DBNull.Value Then
                    objCampo.Etiqueta = dr("Etiqueta")
                Else
                    objCampo.Etiqueta = String.Empty
                End If

                If dr("Tipo") IsNot DBNull.Value Then
                    objCampo.Tipo = dr("Tipo")
                Else
                    objCampo.Tipo = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objCampo)

            Next

        End If

    End Sub

#End Region

End Class