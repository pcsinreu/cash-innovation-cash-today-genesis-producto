Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosAjuste.Salida

    '<XmlType(Namespace:="urn:AltaMovimientosAcreditacion.Entrada")>
    '<XmlRoot(Namespace:="urn:AltaMovimientosAcreditacion.Entrada")>
    <Serializable()>
    Public Class Cuenta

        Public Property DeviceID As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property CodigoCanal As String
        Public Property CodigoSubcanal As String

    End Class
End Namespace