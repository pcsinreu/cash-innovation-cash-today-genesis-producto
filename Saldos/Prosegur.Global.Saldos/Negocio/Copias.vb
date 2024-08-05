Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class Copias
    Inherits List(Of Copia)


#Region "[VARIÁVEIS]"

    Private _Formulario As Formulario

#End Region

#Region "[PROPRIEDADES]"

    Public Property Formulario() As Formulario
        Get
            If _Formulario Is Nothing Then
                _Formulario = New Formulario()
            End If
            Formulario = _Formulario
        End Get
        Set(Value As Formulario)
            _Formulario = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Function Realizar() As Copias

        Dim comando As IDbCommand 'criar comando

        Try
            comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

            comando.CommandType = CommandType.Text
            comando.CommandText = My.Resources.FormularioCopiaRealizar.ToString()

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Identificador_Numerico, Me.Formulario.Id))

            ' executar comando
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count Then

                Dim objCopia As Copia = Nothing

                For Each dr As DataRow In dt.Rows

                    ' criar objeto copia
                    objCopia = New Copia

                    objCopia.Id = dr("Id")
                    objCopia.Destinatario = dr("Destinatario")
                    objCopia.TipoCopia.Id = dr("IdTipoCopia")
                    objCopia.TipoCopia.Descripcion = dr("Descripcion")

                    ' adicionar para colecao
                    Me.Add(objCopia)

                Next

            End If

            Return Me

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

End Class