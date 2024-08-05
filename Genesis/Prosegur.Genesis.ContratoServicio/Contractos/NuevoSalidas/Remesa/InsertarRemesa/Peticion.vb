Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Remesa.DividirServicios

    <XmlType(Namespace:="urn:DividirServicios")> _
    <XmlRoot(Namespace:="urn:DividirServicios")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorRemesaPadre As String
        Public Property Remesas As List(Of Clases.Remesa)
        Public Property CodigoPuesto As String
        Public Property CodigoDelegacion As String
        Public Property Modo As Enumeradores.Modo

    End Class

End Namespace