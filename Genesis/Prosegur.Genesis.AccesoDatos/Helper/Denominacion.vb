Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Denominacion.
''' </summary>
Public Class Denominacion

    ''' <summary>
    ''' Busca por informações de Denominacion.
    ''' </summary>
    Public Shared Function PesquisarDenominacion(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.        
        query.Append(My.Resources.BuscaDenominacion.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_DENOMINACION", "DES_DENOMINACION", "OID_DENOMINACION")

        ' Retorna lista contendo dados de Respuesta Denominacion.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

End Class
