Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Planta.
''' </summary>
''' <history>
''' [Thiago Dias] 04/09/2013 - Criado.
'''</history>
Public Class Planta

    ''' <summary>
    ''' Busca por informações de Planta.
    ''' </summary>
    Public Shared Function PesquisarPlanta(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaPlanta.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_PLANTA", "DES_PLANTA", "OID_PLANTA")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

End Class
