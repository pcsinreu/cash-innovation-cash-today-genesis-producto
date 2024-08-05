Imports System.Xml.Serialization
Namespace Contractos.Job.Depuracion
    <XmlType(Namespace:="urn:Depuracion.Entrada")>
    <XmlRoot(Namespace:="urn:Depuracion.Entrada")>
    <Serializable()>
    Public Class Peticion
        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
    End Class
End Namespace
