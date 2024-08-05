Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Collections.ObjectModel

Namespace Documento.AlocarPedidosExternos

    <XmlType(Namespace:="urn:AlocarPedidosExternos")> _
    <XmlRoot(Namespace:="urn:AlocarPedidosExternos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property documentos As ObservableCollection(Of Clases.Documento)

    End Class

End Namespace