Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.VolverBultoEstadoAsignado

    <XmlType(Namespace:="urn:VolverBultoEstadoAsignado")> _
    <XmlRoot(Namespace:="urn:VolverBultoEstadoAsignado")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorBulto As String
        Public Property CodigoUsuario As String
        Public Property CodigoPuesto As String
        Public Property FyhActualizacion As DateTime


    End Class

End Namespace