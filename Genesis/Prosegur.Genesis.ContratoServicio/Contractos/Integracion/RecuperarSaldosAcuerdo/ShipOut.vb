Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosAcuerdo
    <Serializable()>
    Public Class ShipOut

        Public Property FechaHoraInicio As DateTime
        Public Property FechaHoraFin As DateTime
        Public Property Total As Double
        Public Property Divisa As String
        Public Property TipoMercancia As String
        Public Property CantidadTransacciones As Integer

    End Class
End Namespace

