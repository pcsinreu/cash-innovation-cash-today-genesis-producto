Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Canal.
''' </summary>
''' <history>
''' [Thiago Dias] 13/09/2013 - Criado.
'''</history>
Public Class Canal

    ''' <summary>
    ''' Busca por informações de Canal.
    ''' </summary>
    Public Shared Function PesquisarCanal(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.        
        query.Append(My.Resources.BuscaCanal.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_CANAL", "DES_CANAL", "OID_CANAL")

        ' Retorna lista contendo dados de Respuesta Canal.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

    Public Shared Function PesquisarCanalCodigo(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.        
        query.Append(My.Resources.BuscaCanal.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_CANAL", "DES_CANAL", "COD_CANAL")

        ' Retorna lista contendo dados de Respuesta Canal.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

End Class
