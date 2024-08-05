Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico

    <XmlType(Namespace:="urn:RecuperarSaldosHistorico")>
    <XmlRoot(Namespace:="urn:RecuperarSaldosHistorico")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse
        Public Property Maquinas As List(Of Maquina)
        Public Sub New()
            Maquinas = New List(Of Maquina)
        End Sub
    End Class
End Namespace

