Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

Public Class Maquina

    ''' <summary>
    ''' Retorna as maquinas do produto
    ''' </summary>
    ''' <param name="oidProducto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] Criado 11/03/2009
    ''' </history>
    Public Shared Function RetornaMaquinas(oidProducto As String) As GetProceso.MaquinaColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaMaquinasProducto)
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PRODUCTO", ProsegurDbType.Objeto_Id, oidProducto))

        Dim dtMaquinas As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Verifica se o dtMaquinas retornou algum registro
        If dtMaquinas IsNot Nothing AndAlso dtMaquinas.Rows.Count > 0 Then

            Return RetornarColeccionMaquinas(dtMaquinas)

        Else

            Return Nothing

        End If

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dtMaquinas"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function RetornarColeccionMaquinas(dtMaquinas As DataTable) As GetProceso.MaquinaColeccion

        Dim objColMaquinas As New GetProceso.MaquinaColeccion

        'Percorre o dtMaquinas
        For Each drMaquinas As DataRow In dtMaquinas.Rows

            objColMaquinas.Add(PopularMaquina(drMaquinas))

        Next

        Return objColMaquinas

    End Function

    ''' <summary>
    ''' Popula o objMaquina
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopularMaquina(dr As DataRow) As GetProceso.Maquina

        Dim objMaquina As New GetProceso.Maquina

        Util.AtribuirValorObjeto(objMaquina.Descripcion, dr("DES_MAQUINA"), GetType(String))

        Return objMaquina

    End Function

    ''' <summary>
    ''' Faz a pesquisa e preenche do datatable, retornando uma coleção de maquinas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' [octavio.piramo] 27/03/2009 Alterado - Adicionado DISTINCT na query getComboMaquinas.
    ''' </history>
    Public Shared Function GetComboMaquinas() As List(Of String)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.getComboMaquinas.ToString())
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaUtilidad As New List(Of String)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If dr("DES_MAQUINA") IsNot DBNull.Value Then
                    ' adicionar para objeto
                    objRetornaUtilidad.Add(dr("DES_MAQUINA").ToString)
                End If
            Next
        End If

        ' retornar objeto
        Return objRetornaUtilidad

    End Function

End Class