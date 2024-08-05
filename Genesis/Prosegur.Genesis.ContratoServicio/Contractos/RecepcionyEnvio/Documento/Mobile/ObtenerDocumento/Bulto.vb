Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Documento.Mobile.ObtenerDocumento

    <Serializable()>
    Public Class Bulto

        Public Property idBulto As String
        Public Property codPrecinto As String

        <XmlArray(ElementName:="declarados")>
        <XmlArrayItem(ElementName:="declarado")>
        Public Property declarado As ObservableCollection(Of Detalle)

    End Class

End Namespace
