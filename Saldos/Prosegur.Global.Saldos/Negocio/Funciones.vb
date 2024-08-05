Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class Funciones
    Inherits List(Of Funcion)

#Region "[VARIÁVEIS]"

    Private _Usuario As Usuario

#End Region

#Region "[PROPRIEDADES]"

    Public Property Usuario() As Usuario
        Get
            If _Usuario Is Nothing Then
                _Usuario = New Usuario()
            End If
            Usuario = _Usuario
        End Get
        Set(Value As Usuario)
            _Usuario = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém todas as funções ou apenas de um determinado usuário.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/05/2009 Criado
    ''' </history>
    Public Sub Realizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.FuncionesRealizar.ToString
        comando.CommandType = CommandType.Text

        If Me.Usuario.Id <> 0 Then

            comando.CommandText &= " INNER JOIN PD_FuncionUsuario FU ON F.IdFuncion = FU.IdFuncion WHERE FU.IdUsuario = :IdUsuario "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Usuario.Id))

        End If

        ' adicionar ordenação
        comando.CommandText &= " ORDER BY CODIGO, DESCRIPCION "

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objFucion As Funcion = Nothing

            For Each dr As DataRow In dt.Rows

                objFucion = New Funcion
                objFucion.Id = dr("Id")
                objFucion.Descripcion = dr("Descripcion")
                objFucion.URL = dr("URL")

                If dr("URL") IsNot DBNull.Value Then
                    objFucion.Codigo = dr("Codigo")
                Else
                    objFucion.Codigo = 0
                End If

                Me.Add(objFucion)

            Next

        End If

    End Sub

#End Region

End Class