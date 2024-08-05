Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe TransaccionEfectivo
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TransaccionEfectivo

#Region "[PROPRIEDADES]"

        Public Property CodIsoDivisa As String
        Public Property CodDenominacion As String
        Public Property CodNivelDetalle As String
        Public Property CodTipoEfectivoTotal As String
        Public Property BolDisponible As Boolean
        Public Property NumImporte As Decimal
        Public Property NelCantidad As Integer
        Public Property CodEstadoDocumento As String
        Public Property CodTipoMovimiento As String
        Public Property OidDivisa As String
        Public Property IdentificadorCalidad As String
        Public Property IdentificadorUnidadMedida As String
        Public Property FechaHoraPlanificacionCertificacion As DateTime

        <XmlIgnore()> _
        Public Property OidTransaccionEfectivo As String
#End Region

    End Class

End Namespace