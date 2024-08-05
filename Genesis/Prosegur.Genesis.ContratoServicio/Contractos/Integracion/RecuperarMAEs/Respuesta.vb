Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMAEs

    <XmlType(Namespace:="urn:RecuperarMAEs")>
    <XmlRoot(Namespace:="urn:RecuperarMAEs")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse

        Public Property Paginacion As Comon.Paginacion
        Public Property MAEs As List(Of Maquina)

    End Class

End Namespace
