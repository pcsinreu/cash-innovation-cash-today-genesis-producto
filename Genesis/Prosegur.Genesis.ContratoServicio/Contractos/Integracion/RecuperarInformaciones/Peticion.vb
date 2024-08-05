Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarInformaciones

    <XmlType(Namespace:="urn:RecuperarInformaciones")>
    <XmlRoot(Namespace:="urn:RecuperarInformaciones")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion

        Public Property Movimientos As List(Of ActualizarMovimientos.MovimientoEntrada)

    End Class

End Namespace