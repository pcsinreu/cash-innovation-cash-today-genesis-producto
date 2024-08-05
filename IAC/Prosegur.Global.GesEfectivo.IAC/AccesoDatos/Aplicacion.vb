Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text

Public Class Aplicacion

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Obtém os Niveis
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/08/2011 Criado
    ''' </history>
    Public Shared Function GetComboAplicaciones() As ContractoServicio.Utilidad.getComboAplicaciones.AplicacionColeccion

        ' criar objeto Aplicação coleccion
        Dim objAplicaciones As New ContractoServicio.Utilidad.getComboAplicaciones.AplicacionColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetComboAplicaciones.ToString)

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows
                ' adicionar para coleção
                objAplicaciones.Add(PopularComboAplicaciones(dr))
            Next
        End If
        ' retornar coleção de aplicacion
        Return objAplicaciones
    End Function

    Private Shared Function PopularComboAplicaciones(dr As DataRow) As ContractoServicio.Utilidad.getComboAplicaciones.Aplicacion
        ' criar objeto aplicacion
        Dim objAplication As New ContractoServicio.Utilidad.getComboAplicaciones.Aplicacion
        Util.AtribuirValorObjeto(objAplication.CodigoAplicacion, dr("COD_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objAplication.DescripcionAplicacion, dr("DES_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objAplication.CodigoPermiso, dr("COD_PERMISO"), GetType(String))
        Return objAplication
    End Function

    Public Shared Function ObterOIDAplicacion(codigoAplicacion As String) As String
        

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.ObterOIDAplicacion.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.CommandText = Util.PrepararQuery(query.ToString)

        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result
    End Function

#End Region

End Class