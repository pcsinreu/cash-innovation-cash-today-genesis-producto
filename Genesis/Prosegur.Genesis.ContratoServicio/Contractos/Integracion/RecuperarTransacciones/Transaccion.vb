
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransacciones

    <XmlType(Namespace:="urn:RecuperarTransacciones")>
    <XmlRoot(Namespace:="urn:RecuperarTransacciones")>
    <Serializable()>
    Public Class Transaccion

        Public Property ColumnPivot As String
        Public Property Oid_documento As String
        Public Property FechaGestion As String
        Public Property FechaCreacion As String
        Public Property FechaAcreditacion As String
        Public Property FechaNotificacion As String
        Public Property OidMovimiento As String

        'Public Property FechaGestion As DateTime
        'Public Property FechaCreacion As DateTime
        'Public Property FechaAcreditacion As DateTime
        'Public Property FechaNotificacion As DateTime
        'Public Property FechaGestionTxt As String
        'Public Property FechaCreacionTxt As String
        'Public Property FechaAcreditacionTxt As String
        'Public Property FechaNotificacionTxt As String

        Public Property HoraGestion As String
        Public Property HoraCreacion As String
        Public Property HoraAcreditacion As String
        Public Property HoraNotificacion As String


        Public Property CodExternoBase As String
        Public Property CodExterno As String
        Public Property Maquina As String
        Public Property PuntoServicio As String
        Public Property Cliente As String
        Public Property SubCliente As String
        Public Property Delegacion As String
        Public Property Canal As String
        Public Property SubCanal As String
        Public Property TipoTransaccion As String
        Public Property Formulario As String

        Public Property Divisa As String
        Public Property ImporteContado As Double
        Public Property Importe As String
        Public Property ImporteDeclarado As Double
        Public Property ContadoDeclarado As String
        Public Property Simbolo As String
        Public Property CodResponsable As String
        Public Property NombreResponsable As String
        Public Property ReceiptNumber As String
        Public Property Barcode As String
        Public Property Modalidad As String
        Public Property Notificacion As String
        Public Property ImporteInformativo As String
        Public Property CampoExtra As String
        Public Property CampoExtraValor As String
        Public Property Cantidad As Integer
        Public Property BaseDeviceId As String
    End Class

End Namespace
