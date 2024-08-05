Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Documento.Mobile.ObtenerDocumento
    <Serializable()>
    Public Class Documento

        Public Property IdDocumento As String
        Public Property CodDocumento As String
        Public Property CodRuta As String
        Public Property FecRuta As Date
        Public Property FecSalidaRuta As DateTime
        Public Property DesEntidad As String
        Public Property DesCentro As String
        Public Property DesPunto As String

        <XmlArray(ElementName:="declarados")>
        <XmlArrayItem(ElementName:="declarado")>
        Public Property Declarado As List(Of Detalle)

        <XmlArray(ElementName:="bultos")>
        <XmlArrayItem(ElementName:="bulto")>
        Public Property Bulto As List(Of Bulto)

    End Class

End Namespace
