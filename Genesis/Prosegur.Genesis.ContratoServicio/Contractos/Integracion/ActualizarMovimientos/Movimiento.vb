Imports System.Xml.Serialization

Namespace Contractos.Integracion.ActualizarMovimientos

    <Serializable()>
    Public Class Movimiento

        <XmlAttributeAttribute()>
        Public CodigoExterno As String
        Public Property Acreditar As Proceso
        Public Property Notificar As Proceso
        Public Property Reenviar As Proceso

    End Class

End Namespace

