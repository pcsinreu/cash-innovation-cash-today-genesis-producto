Imports System.Xml.Serialization
Namespace Contractos.Integracion.ModificarPeriodos.Entrada
    <XmlType(Namespace:="urn:ModificarPeriodos.Entrada")>
    <XmlRoot(Namespace:="urn:ModificarPeriodos.Entrada")>
    <Serializable()>
    Public Class Periodo

        Public Property Oid_Periodo As String
        Public Property Cod_Pais As String
        Public Property DeviceID As String
    End Class
End Namespace

