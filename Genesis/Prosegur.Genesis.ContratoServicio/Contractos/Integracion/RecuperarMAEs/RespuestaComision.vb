Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarComissionDatosBancarios

    <XmlType(Namespace:="urn:RecuperarComissionDatosBancarios")>
    <XmlRoot(Namespace:="urn:RecuperarComissionDatosBancarios")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse
        Public Property MAEs As List(Of Maquina)

    End Class


    Public Class Maquina
        Public Property OID_MAQUINA As String
        Public Property OID_SUBCLIENTE As String
        Public Property COD_SUBCLIENTE As String
        Public Property DES_SUBCLIENTE As String
        Public Property OID_PTO_SERVICIO As String
        Public Property COD_PTO_SERVICIO As String
        Public Property DES_PTO_SERVICIO As String
        Public Property NUM_PORCENT_COMISION As Nullable(Of Decimal)

    End Class

End Namespace
