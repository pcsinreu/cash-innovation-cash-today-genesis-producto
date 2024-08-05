Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Classe grupo
''' </summary>
''' <remarks></remarks>
Public Class Grupo

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Recupera todos os grupos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/01/2011 Criado
    ''' </history>
    Public Shared Function GetGrupos() As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.getGrupos.ToString())

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' Recupera todos os grupos sem cajero
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/01/2011 Criado
    ''' </history>
    Public Shared Function GetGruposSemCajero() As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.getGruposSemCajero.ToString())

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' Recupera todos os ATMs de um grupos.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    Public Shared Function GetATMsByGrupo(OidGrupo As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' seta query
        comando.CommandText = Util.PrepararQuery(My.Resources.getATMsByGrupo.ToString())

        ' adiciona parâmetro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO", ProsegurDbType.Identificador_Alfanumerico, OidGrupo))

        ' executa consulta
        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' verifica se existe um grupo com o código informado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    Public Shared Function VerificarGrupo(CodGrupo As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' seta query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarGrupo.ToString())

        ' adiciona parâmetro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_GRUPO", ProsegurDbType.Identificador_Alfanumerico, CodGrupo))

        If AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere um Grupo
    ''' </summary>
    ''' <param name="CodGrupo"></param>
    ''' <param name="DesGrupo"></param>
    ''' <param name="OidGrupo"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="FyhActualizacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011  criado
    ''' </history>
    Public Shared Sub InsertarGrupo(OidGrupo As String, CodGrupo As String, DesGrupo As String, CodUsuario As String, _
                                    FyhActualizacion As DateTime)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' se query
        comando.CommandText = Util.PrepararQuery(My.Resources.InsertGrupo.ToString)
        comando.CommandType = CommandType.Text

        ' configura parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_grupo", ProsegurDbType.Objeto_Id, OidGrupo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_grupo", ProsegurDbType.Identificador_Alfanumerico, CodGrupo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_grupo", ProsegurDbType.Descricao_Longa, DesGrupo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data_Hora, FyhActualizacion))

        ' executa comando
        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

    End Sub

#End Region

#Region "[ATUALIZAR]"

#End Region

#Region "[FILTROS]"

#End Region

End Class