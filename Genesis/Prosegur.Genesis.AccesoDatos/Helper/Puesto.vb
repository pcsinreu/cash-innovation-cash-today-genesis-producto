Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Puesto.
''' </summary>
''' <history>
''' [Claudioniz] 19/12/2014 - Criado.
'''</history>
Public Class Puesto

    ''' <summary>
    ''' Busca por informações de Puesto.
    ''' </summary>
    Public Shared Function PesquisarPuesto(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.        
        query.Append(My.Resources.BuscaPuesto.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_PUESTO", "COD_PUESTO", "OID_PUESTO")

        ' Retorna lista contendo dados de Respuesta Canal.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function
    Public Shared Function PesquisarPuesto2(peticion As PeticionHelperPuesto, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.RespuestaPuesto)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.        
        query.Append(My.Resources.BuscaPuesto2.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatosPuesto(query, peticion, paramRespuestaPaginacion, "COD_PUESTO", "COD_HOST_PUESTO", "OID_PUESTO", "BOL_VIGENTE")

        ' Retorna lista contendo dados de Respuesta Canal.
        Return HelperBuscaDatos.ListaDatosRespuestaPuesto(dtResultado)

    End Function

End Class
