Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Documento.Mobile.ObtenerDocumento

    <Serializable()>
    Public Class Detalle

        Public Property DesDivisa As String
        Public Property DesTipoMercancia As String
        Public Property Cantidad As Integer
        Public Property Importe As Decimal 'Es el tipo correcto?

        <XmlArray(ElementName:="Desglose")>
        <XmlArrayItem(ElementName:="Desglose")>
        Public Property Desglose As List(Of Desglose)

    End Class

End Namespace

