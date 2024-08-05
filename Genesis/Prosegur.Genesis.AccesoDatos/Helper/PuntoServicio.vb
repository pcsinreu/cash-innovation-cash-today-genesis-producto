Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Punto Servicio.
''' </summary>
''' <history>
''' [Thiago Dias] 13/09/2013 - Criado.
''' [Thiago Dias] 27/09/2013 - Modificado.
'''</history>
Public Class PuntoServicio

    ''' <summary>
    ''' Pesquisa por informações de Punto de Servício.
    ''' </summary>
    Public Shared Function PesquisarPuntoServicio(peticion As PeticionHelper, _
                                             ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaPuntoServicio.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_PTO_SERVICIO", "DES_PTO_SERVICIO", "COD_PTO_SERVICIO")

        ' Retorna lista contendo dados de Respuesta PuntoServicio.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function


    Public Shared Function PesquisarPuntoServicio2(peticion As PeticionHelper,
                                             ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaPuntoServicio2.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_PTO_SERVICIO", "DES_PTO_SERVICIO", "COD_PTO_SERVICIO")

        ' Retorna lista contendo dados de Respuesta PuntoServicio.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function
    ''' <summary>
    ''' Pesquisa por informações de Punto de Servício.
    ''' </summary>
    Public Shared Function PesquisarPuntoServicioMaquina(peticion As PeticionHelper,
                                             ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaPuntoServicioMaquina.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_PTO_SERVICIO", "DES_PTO_SERVICIO", "COD_PTO_SERVICIO")

        ' Retorna lista contendo dados de Respuesta PuntoServicio.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function


End Class
