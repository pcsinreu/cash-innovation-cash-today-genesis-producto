Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.CuadrarBultos

    <XmlType(Namespace:="urn:CuadrarBultos")> _
    <XmlRoot(Namespace:="urn:CuadrarBultos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Property CodigoUsuario As String
        Property PrecintosBultos As List(Of String)

    End Class
End Namespace
