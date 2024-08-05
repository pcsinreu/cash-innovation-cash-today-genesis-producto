Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados SubCanal.
''' </summary>
''' <history>
''' [Thiago Dias] 13/09/2013 - Criado.
'''</history>
Public Class SubCanal

    ''' <summary>
    ''' Busca por informações de SubCanal.
    ''' </summary>
    Public Shared Function PesquisarSubCanal(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaSubCanal.ToString())

        ' Realiza Pesquisa.        
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_SUBCANAL", "DES_SUBCANAL", "OID_SUBCANAL")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

    ''' <summary>
    ''' Busca por informações de SubCanal.
    ''' </summary>
    Public Shared Function PesquisarSubCanalPorCodigo(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaSubCanal.ToString())

        ' Realiza Pesquisa.        
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_SUBCANAL", "DES_SUBCANAL", "COD_SUBCANAL")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

End Class
