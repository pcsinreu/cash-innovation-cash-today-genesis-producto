Imports System.Xml.Serialization

Namespace Contractos.Job.NotificarMovimientosNoAcreditados
    <XmlType(Namespace:="urn:NotificarMovimientosNoAcreditados.Entrada")>
    <XmlRoot(Namespace:="urn:NotificarMovimientosNoAcreditados.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String

    End Class
End Namespace
