Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.InsertarBulto

    <XmlType(Namespace:="urn:InsertarBulto")> _
    <XmlRoot(Namespace:="urn:InsertarBulto")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoUsuario As String
        Public Property IdentificadorRemesa As String
        Public Property CodDelegacion As String
        Public Property Bulto As Clases.Bulto

    End Class

End Namespace