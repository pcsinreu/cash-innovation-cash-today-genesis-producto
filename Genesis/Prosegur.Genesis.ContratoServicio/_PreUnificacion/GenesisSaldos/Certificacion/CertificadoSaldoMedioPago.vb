Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe CertificadoSaldoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class CertificadoSaldoMedioPago

#Region "[PROPRIEDADES]"

        Public Property DescripcionMedioPago As String
        Public Property DescripcionDivisa As String
        Public Property IdentificadorDivisa As String
        Public Property CodigoIsoDivisa As String
        Public Property IdentificadorUnidadMedida As String
        Public Property IdentificadorMedioPago As String
        Public Property CodigoMedioPago As String
        Public Property CodigoTipoMedioPago As String
        Public Property CodigoNivelDetalle As String
        Public Property BolDisponible As Boolean
        Public Property NelCantidad As Int64
        Public Property NumImporte As Decimal
        Public Property NelCantidadInicial As Int64
        Public Property NumImporteInicial As Decimal
        Public Property NelCantidadFinal As Int64
        Public Property NumImporteFinal As Decimal
        Public Property IdentificadorCertificado As String
        Public Property IdentificadorCertificadoAnterior As String

        <XmlIgnore()> _
        Public Property IdentificadorSaldoMedioPago As String
#End Region

    End Class

End Namespace