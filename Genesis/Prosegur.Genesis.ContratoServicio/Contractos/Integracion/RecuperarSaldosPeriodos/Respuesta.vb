Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosPeriodos

    <XmlType(Namespace:="urn:RecuperarSaldosPeriodos")>
    <XmlRoot(Namespace:="urn:RecuperarSaldosPeriodos")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse
        Public Property Maquinas As List(Of Maquina)
        Public Sub New()
            Maquinas = New List(Of Maquina)
        End Sub
    End Class
End Namespace

