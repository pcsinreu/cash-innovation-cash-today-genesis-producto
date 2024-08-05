Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.ObtenerFormularioFondos

    <XmlType(Namespace:="urn:ObtenerFormularioFondos")> _
    <XmlRoot(Namespace:="urn:ObtenerFormularioFondos")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property codFormulario As String
    End Class

End Namespace
