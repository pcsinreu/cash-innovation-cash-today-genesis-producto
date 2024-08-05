Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Documento.Mobile.ObtenerDocumento

    <XmlType(Namespace:="urn:ObtenerDocumento")> _
    <XmlRoot(Namespace:="urn:ObtenerDocumento")> _
    <Serializable()>
    Public Class Peticion

        Public Property codigoDelegacion As String
        Public Property bolDetalleBulto As Boolean
        Public Property bolRutaDiaSiguiente As Boolean
        Public Property bolTodosRutas As Boolean
        Public Property codigoSector As String
        Public Property obtenerDatosRemesa As Nullable(Of Boolean)
        Public Property somenteSinRutas As Nullable(Of Boolean)

        <XmlArray(ElementName:="datosRuta")>
        <XmlArrayItem(ElementName:="Ruta")>
        Public Property datosRuta As List(Of Ruta)

    End Class

End Namespace

