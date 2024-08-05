Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Puesto.SolicitarFondosSaldos

    <XmlType(Namespace:="urn:SolicitarFondosSaldos")> _
    <XmlRoot(Namespace:="urn:SolicitarFondosSaldos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Documento As Clases.Documento
    End Class

End Namespace