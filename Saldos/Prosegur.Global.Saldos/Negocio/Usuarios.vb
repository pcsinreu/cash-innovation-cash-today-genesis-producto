Imports System.Text
Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class Usuarios
    Inherits List(Of Usuario)

    Private _Nombre As String
    Public Property ApellidoNombre() As String
        Get
            Return _Nombre
        End Get
        Set(value As String)
            _Nombre = value
        End Set
    End Property

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Busca todos os usuarios sem filtros
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Alterado em 07/06/2010
    ''' </history>
    Public Sub Realizar()

        Dim comando As IDbCommand ' criar comando
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.UsuariosRealizar.ToString()
        comando.CommandType = CommandType.Text

        ' percorrer todos registros encontrados
        PercorrerTodosRegistrosEncontrados(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando))

    End Sub

    ''' <summary>
    ''' Realiza a busca dos usuarios filtrando pelo nome/login do usuario
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Criado em 07/06/2010
    ''' </history>
    Public Sub RealizarPorNombre()

        Dim comando As IDbCommand ' criar comando
        Dim comandoSql As New StringBuilder
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text

        comandoSql.Append(My.Resources.UsuariosRealizar.ToString())

        ' Se realmente for informado algum valor no nome
        If Not String.IsNullOrEmpty(Me.ApellidoNombre) Then
            comandoSql.Append(" Where ApellidoNombre Like '%" & Me.ApellidoNombre & "%'")
        End If

        comando.CommandText = comandoSql.ToString()

        ' percorrer todos registros encontrados
        PercorrerTodosRegistrosEncontrados(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando))

    End Sub

    ''' <summary>
    ''' Percorre todos os registros de um datatable
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Criado em 07/06/2010
    ''' </history>
    Private Sub PercorrerTodosRegistrosEncontrados(dt As DataTable)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto usuario
            Dim UsuarioAAdicionar As Usuario = Nothing

            For Each dr As DataRow In dt.Rows

                ' criar objeto usuario
                UsuarioAAdicionar = New Usuario
                UsuarioAAdicionar.Id = dr("Id")

                If dr("Nombre") IsNot DBNull.Value Then
                    UsuarioAAdicionar.Nombre = dr("Nombre")
                Else
                    UsuarioAAdicionar.Nombre = String.Empty
                End If

                If dr("ApellidoNombre") IsNot DBNull.Value Then
                    UsuarioAAdicionar.ApellidoNombre = dr("ApellidoNombre")
                Else
                    UsuarioAAdicionar.ApellidoNombre = String.Empty
                End If

                If dr("Bloqueado") IsNot DBNull.Value Then
                    UsuarioAAdicionar.Bloqueado = Convert.ToBoolean(dr("Bloqueado"))
                Else
                    UsuarioAAdicionar.Bloqueado = False
                End If

                ' adicionar para colecao
                Me.Add(UsuarioAAdicionar)

            Next

        End If

    End Sub

#End Region

End Class