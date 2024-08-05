Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.IniciarBulto

    <XmlType(Namespace:="urn:IniciarBulto")> _
    <XmlRoot(Namespace:="urn:IniciarBulto")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorBulto As String
        Public Property CodigoUsuario As String
        Public Property CodigoPuesto As String
        Public Property CodDelegacion As String

    End Class

End Namespace