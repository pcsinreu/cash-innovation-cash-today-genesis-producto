Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Remesa.IniciarRemesa

    <XmlType(Namespace:="urn:IniciarRemesa")> _
    <XmlRoot(Namespace:="urn:IniciarRemesa")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Remesa As Clases.Remesa
        Public Property CodigoUsuario As String
        Public Property CodigoPuesto As String
        Public Property CodigoDelegacion As String
        Public Property FyhActualizacion As DateTime


    End Class

End Namespace