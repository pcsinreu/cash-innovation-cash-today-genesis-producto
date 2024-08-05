
Imports System.ComponentModel
Imports System.Xml.Serialization


Namespace Contractos.Integracion.RecuperarMovimientos
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
        Public Property CodigoCollectionId As String

        Public Property FechaGestion As DateTime
        Public Property FechaRealizacion As DateTime
        Public Property FechaAcreditacion As String
        Public Property Formulario As Comon.Entidad
        Public Property Destino As Cuenta
        Public Property Valores As List(Of Divisa)
        Public Property CamposAdicionales As List(Of CampoAdicional)

    End Class
End Namespace