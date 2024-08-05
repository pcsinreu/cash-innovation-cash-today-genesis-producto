Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.ActualizarCodigoBolsa

    <XmlType(Namespace:="urn:ActualizarCodigoBolsa")> _
    <XmlRoot(Namespace:="urn:ActualizarCodigoBolsa")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Property IdentificadorBulto As String
        Property CodigoBolsa As String

    End Class

End Namespace