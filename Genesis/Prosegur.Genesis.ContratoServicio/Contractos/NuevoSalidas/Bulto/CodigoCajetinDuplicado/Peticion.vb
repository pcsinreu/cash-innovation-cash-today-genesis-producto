Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.CodigoCajetinDuplicado

    <XmlType(Namespace:="urn:CodigoCajetinDuplicado")> _
    <XmlRoot(Namespace:="urn:CodigoCajetinDuplicado")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion
        Public Property IdentificadorRemesa As String
        Public Property IdentificadorBulto As String
        Public Property CodigoCajetin As String

    End Class

End Namespace