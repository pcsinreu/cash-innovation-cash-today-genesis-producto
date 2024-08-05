Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Remesa.VolverRemesasBultosEstadoAsignado

    <XmlType(Namespace:="urn:VolverRemesasBultosEstadoAsignado")> _
    <XmlRoot(Namespace:="urn:VolverRemesasBultosEstadoAsignado")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadoresRemesa As List(Of Clases.Conversion)
        Public Property IdentificadoresBulto As List(Of Clases.Conversion)
        Public Property ValidarRemesas As Boolean
        Public Property CodigoUsuario As String
        Public Property CodigoPuesto As String
        Public Property CodigoDelegacion As String

    End Class

End Namespace