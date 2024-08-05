Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido

    <XmlType(Namespace:="urn:RecuperarDocumentosSinSalidaRecorrido")>
    <XmlRoot(Namespace:="urn:RecuperarDocumentosSinSalidaRecorrido")>
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigosExternos As List(Of String)

    End Class

End Namespace