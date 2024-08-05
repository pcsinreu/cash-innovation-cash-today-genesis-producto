Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados TipoContenedor.
''' </summary>
Public Class TipoContenedor

    ''' <summary>
    ''' Busca por informações de TipoContenedor.
    ''' </summary>
    Public Shared Function PesquisarTipoContenedor(peticion As PeticionHelperTipoContenedor, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.RespuestaHelperTipoContenedorDatos)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.        
        query.Append(My.Resources.BuscaTipoContenedor.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatosTipoContenedor(query, peticion, paramRespuestaPaginacion, "COD_TIPO_CONTENEDOR", "DES_TIPO_CONTENEDOR", "OID_TIPO_CONTENEDOR", True)

        ' Retorna lista contendo dados de Respuesta Tipocontenedor.
        Return HelperBuscaDatos.ListaDatosRespuestaTipoContenedor(dtResultado)

    End Function

End Class
