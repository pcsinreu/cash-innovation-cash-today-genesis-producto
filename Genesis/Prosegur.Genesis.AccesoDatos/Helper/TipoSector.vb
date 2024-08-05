Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Sector.
''' </summary>
''' <history>
''' [Thiago Dias] 04/09/2013 - Criado.
'''</history>
Public Class TipoSector

    ''' <summary>
    ''' Busca por informações de Sector.
    ''' </summary>
    Public Shared Function PesquisarTipoSector(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaTipoSector.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_TIPO_SECTOR", "DES_TIPO_SECTOR", "COD_TIPO_SECTOR")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

End Class
