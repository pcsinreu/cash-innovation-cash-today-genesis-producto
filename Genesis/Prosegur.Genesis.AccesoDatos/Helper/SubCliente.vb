Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados SubCliente.
''' </summary>
''' <history>
''' [Thiago Dias] 13/09/2013 - Criado.
''' [Thiago Dias] 27/09/2013 - Modificado.
'''</history>
Public Class SubCliente

    ''' <summary>
    ''' Pesquisa por informações de SubCliente.
    ''' </summary>
    Public Shared Function BuscarSubCliente(peticion As PeticionHelper, _
                                             ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaSubCliente.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_SUBCLIENTE", "DES_SUBCLIENTE", "COD_SUBCLIENTE")

        ' Retorna lista contendo dados de Respuesta SubCliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

End Class
