Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.DividirEnBultos

    <XmlType(Namespace:="urn:DividirEnBultos")> _
    <XmlRoot(Namespace:="urn:DividirEnBultos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Remesa As Clases.Remesa

        Public Property MezclarDenominacion As Boolean
        Public Property MezclarBilleteMoneda As Boolean
        Public Property CodigoConfiguracionAutoDesglose As String
    End Class

End Namespace