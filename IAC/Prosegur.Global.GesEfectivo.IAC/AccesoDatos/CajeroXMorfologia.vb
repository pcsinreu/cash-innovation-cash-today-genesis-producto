Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

''' <summary>
''' Classe CajeroXMorfologia
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  11/01/2011  criado
''' </history>
Public Class CajeroXMorfologia

#Region "[INSERTS]"

    ''' <summary>
    ''' Insere uma associação CajeroXMorfologia
    ''' </summary>
    ''' <param name="OidCajero"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="FyhActualizacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Public Shared Sub InsertarCajeroXMorfologia(OidCajeroXMorfologia As String, OidMorfologia As String, OidCajero As String, _
                                                FecInicio As DateTime, CodUsuario As String, FyhActualizacion As DateTime, _
                                                ByRef Transacion As Prosegur.DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder()

        query.Append(My.Resources.InsertarCajeroXMorfologia.ToString)

        ' configura parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_cajeroxmorfologia", ProsegurDbType.Objeto_Id, OidCajeroXMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_morfologia", ProsegurDbType.Objeto_Id, OidMorfologia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_cajero", ProsegurDbType.Objeto_Id, OidCajero))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fec_inicio", ProsegurDbType.Data, FecInicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data_Hora, FyhActualizacion))

        comando.CommandText = Util.PrepararQuery(query.ToString())
        comando.CommandType = CommandType.Text

        If Transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            Transacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region

#Region "[UPDATES]"

    ''' <summary>
    ''' Exclui uma associação CajeroXMorfologia
    ''' </summary>
    ''' <param name="OidCajero"></param>
    ''' <param name="Transacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/10/2011  criado
    ''' </history>
    Public Shared Sub EliminarMorfologias(OidCajero As String, ByRef Transacion As Prosegur.DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.EliminarMorfologias)
        comando.CommandType = CommandType.Text

        ' configura parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_cajero", ProsegurDbType.Objeto_Id, OidCajero))

        If Transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            Transacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region

End Class