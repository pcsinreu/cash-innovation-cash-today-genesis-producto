Imports System.Xml.Serialization

Namespace Contractos.Integracion.MarcarMovimientos

    <Serializable()>
    Public Class Movimiento

        <XmlAttributeAttribute()>
        Public CodigoExterno As String
        Public Property Acreditar As Proceso
        Public Property Notificar As Proceso

    End Class

End Namespace

