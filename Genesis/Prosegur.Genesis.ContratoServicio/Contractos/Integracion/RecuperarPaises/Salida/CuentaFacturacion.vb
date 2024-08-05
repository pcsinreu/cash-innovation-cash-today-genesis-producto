Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPaises.Salida

    <XmlType(Namespace:="urn:RecuperarPaises.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarPaises.Salida")>
    <Serializable()>
    Public Class CuentaFacturacion

        Public Property Identificador As String
        Public Property CodigoBancoCapital As String
        Public Property DescripcionBancoCapital As String
        Public Property CodigoBancoTesoreria As String
        Public Property DescripcionBancoTesoreria As String
        Public Property CodigoCuentaTesoreria As String
        Public Property DescripcionCuentaTesoreria As String
        Public Property DatosBancarios As List(Of DatoBancario)

    End Class
End Namespace