Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Remesa.VolverRemesaEstadoAsignado

    <XmlType(Namespace:="urn:VolverRemesaEstadoAsignado")> _
    <XmlRoot(Namespace:="urn:VolverRemesaEstadoAsignado")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorRemesa As String
        Public Property CodigoUsuario As String
        Public Property CodigoPuesto As String
        Public Property CodigoDelegacion As String
        Public Property FyhActualizacion As DateTime


    End Class

End Namespace