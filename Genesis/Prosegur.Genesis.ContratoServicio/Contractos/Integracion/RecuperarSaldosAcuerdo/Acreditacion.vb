Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosAcuerdo
    <Serializable()>
    Public Class Acreditacion

        Public Property FechaHoraAcreditacion As DateTime
        Public Property FechaHoraInicioVigencia As DateTime
        Public Property Divisa As String
        Public Property TotalAcreditacion As Double
        Public Property TotalTransacciones As Double
        Public Property TotalComision As Double

    End Class
End Namespace

