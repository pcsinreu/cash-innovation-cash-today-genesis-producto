Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos
    <Serializable()>
    Public Class Movimiento
        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property Notificado As Boolean

        <XmlAttributeAttribute()>
        Public Property Acreditado As Boolean

        <XmlAttributeAttribute()>
        Public Property Disponible As Boolean

        <XmlAttributeAttribute()>
        Public Property CreadoHaPasado As Boolean

        Public Property FechaGestion As DateTime

        Public Property FechaRealizacion As DateTime

        Public Property FechaAcreditacion As String

        Public Property Formulario As Comon.Entidad

        Public Property Valores As List(Of Contractos.Integracion.RecuperarSaldos.Divisa)

        Public Property CamposAdicionales As List(Of Contractos.Integracion.RecuperarSaldos.CampoAdicional)

    End Class
End Namespace