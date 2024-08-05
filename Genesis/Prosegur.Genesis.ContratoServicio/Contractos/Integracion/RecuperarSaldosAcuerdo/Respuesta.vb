Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosAcuerdo

    <XmlType(Namespace:="urn:RecuperarSaldosAcuerdo")>
    <XmlRoot(Namespace:="urn:RecuperarSaldosAcuerdo")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse
        Public Property CodigoPais As String
        Public Property PuntosServicio As List(Of PuntoServicio)
        Public Sub New()
            PuntosServicio = New List(Of PuntoServicio)
        End Sub
    End Class
End Namespace

