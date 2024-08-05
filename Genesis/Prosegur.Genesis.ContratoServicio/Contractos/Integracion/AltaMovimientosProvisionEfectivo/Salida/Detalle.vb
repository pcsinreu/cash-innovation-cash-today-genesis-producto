Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosProvisionEfectivo.Salida

    <XmlType(Namespace:="urn:AltaMovimientosProvisionEfectivo.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosProvisionEfectivo.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

        Public Shared Widening Operator CType(v As String) As Detalle
            Throw New NotImplementedException()
        End Operator
    End Class

End Namespace