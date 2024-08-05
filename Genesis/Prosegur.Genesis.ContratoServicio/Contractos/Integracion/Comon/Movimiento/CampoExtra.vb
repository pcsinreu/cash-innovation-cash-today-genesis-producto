Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon.Movimientos
    <Serializable()>
    <XmlInclude(GetType(CampoExtra))>
    Public Class CampoExtra
        Public Property Codigo As String
        Public Property Valor As String
    End Class
End Namespace