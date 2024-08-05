Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Controle Helper.
''' </summary>
''' <history>
''' [Thiago Dias] 04/09/2013 - Criado.
'''</history>
Public Class HelperGenerico

    ''' <summary>
    ''' Buscador Genérico do Controle Helper.
    ''' </summary>
    ''' <remarks>
    ''' Esta funcionalidade é exclusiva para tratamento de objetos do tipo QueryHelperControl.
    ''' </remarks>
    Public Shared Function PesquisarDadosHelper(peticion As PeticionHelper, _
                                             ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of KeyValuePair(Of String, String))

        ''Dim query As New StringBuilder
        ''Dim dtResultado As DataTable
        ''Dim lstResultado As New List(Of KeyValuePair(Of String, String))
        ''Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' '' Cria comando.
        ''cmd.CommandText = UtilHelper.TratarQuery(peticion)
        ''cmd.CommandType = CommandType.Text

        ' '' Pesquisa dados.
        ''dtResultado = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, peticion.ParametrosPaginacion, paramRespuestaPaginacion)

        ' '' Verifica se a consulta retornou dados.
        ''If (dtResultado IsNot Nothing AndAlso dtResultado.Rows.Count > 0) Then
        ''    For Each dr As DataRow In dtResultado.Rows
        ''        lstResultado.Add(New KeyValuePair(Of String, String)(dr.ItemArray(0).ToString(), dr.ItemArray(1).ToString()))
        ''    Next
        ''End If

        Return Nothing

    End Function

End Class
