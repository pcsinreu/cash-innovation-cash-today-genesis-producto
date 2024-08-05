Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarInventarioContenedor

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:ConsultarInventarioContenedor")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico
        Public Property inventarios As List(Of InventarioRespuesta)
        Public Property contenedores As List(Of ContenedorRespuesta)

    End Class
End Namespace