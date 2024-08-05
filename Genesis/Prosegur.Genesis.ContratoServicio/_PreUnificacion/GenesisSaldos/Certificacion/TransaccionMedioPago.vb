Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe TransaccionMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TransaccionMedioPago

#Region "[PROPRIEDADES]"

        Public Property CodMedioPago As String
        Public Property CodTipoMedioPago As String
        Public Property NumImporte As Decimal
        Public Property NelCantidad As Integer
        Public Property CodNivelDetalle As String
        Public Property BolDisponible As Boolean
        Public Property CodEstadoDocumento As String
        Public Property CodTipoMovimiento As String
        Public Property IdentificadorDivisa As String
        Public Property FechaHoraPlanificacionCertificacion As DateTime

        <XmlIgnore()> _
        Public Property IdentificadorTransaccionMedioPago As String
#End Region

    End Class

End Namespace