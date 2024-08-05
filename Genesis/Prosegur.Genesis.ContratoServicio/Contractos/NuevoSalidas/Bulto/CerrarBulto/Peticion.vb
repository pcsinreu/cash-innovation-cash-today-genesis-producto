Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.CerrarBulto

    <XmlType(Namespace:="urn:CerrarBulto")> _
    <XmlRoot(Namespace:="urn:CerrarBulto")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Remesa As Clases.Remesa
        Public Property EsCuentaCero As Boolean
        Public Property CodigoPuesto As String = String.Empty
        Public Property CodigoPlanta As String = String.Empty
        Public Property CodigoDelegacion As String = String.Empty
    End Class

End Namespace