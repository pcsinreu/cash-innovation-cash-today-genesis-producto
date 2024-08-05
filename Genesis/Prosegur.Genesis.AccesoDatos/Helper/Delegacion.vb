Imports System.Linq
Imports System.Text
Imports System.Data.Entity
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Delegacion.
''' </summary>
''' <history>
''' [Thiago Dias] 04/09/2013 - Criado.
'''</history>
Public Class Delegacion

    ''' <summary>
    ''' Busca por informações de Delegacion.
    ''' </summary>
    Public Shared Function PesquisarDelegacion(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaDelegacion.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_DELEGACION", "DES_DELEGACION", "OID_DELEGACION")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

    Public Shared Function PesquisarDelegacionCuadre(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaDelegacion.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_DELEGACION", "DES_DELEGACION", "COD_DELEGACION")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

End Class
