Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.GrabarInventarioContenedor

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:GrabarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:GrabarInventarioContenedor")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico
        Public Property codInventario As String
        Public Property contenedores As List(Of ContenedorRespuesta)
        Public Property codTipoContenedor As String
        Public Property desTipoContenedor As String
        Public Property fechaArmado As DateTime
        Public Property codPrecinto As List(Of String)
        Public Property Sectores As Sector


    End Class
End Namespace