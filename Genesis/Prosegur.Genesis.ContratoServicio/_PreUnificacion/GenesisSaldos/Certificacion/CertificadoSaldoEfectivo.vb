Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe CertificadoSaldoEfectivo
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class CertificadoSaldoEfectivo

#Region "[PROPRIEDADES]"

        Public Property IdentificadorDivisa As String
        Public Property CodigoIsoDivisa As String
        Public Property DescripcionDivisa As String
        Public Property IdentificadorDenominacion As String
        Public Property CodigoDenominacion As String
        Public Property DescripcionDenominacion As String
        Public Property IdentificadorCalidad As String
        Public Property IdentificadorUnidadMedida As String
        Public Property CodigoNivelDetalle As String
        Public Property CodigoTipoEfectivo As String
        Public Property BolDisponible As Boolean
        Public Property NelCantidad As Int64
        Public Property NumImporte As Double
        Public Property NelCantidadInicial As Int64
        Public Property NumImporteInicial As Double
        Public Property NelCantidadFinal As Int64
        Public Property NumImporteFinal As Double
        Public Property IdentificadorCertificado As String
        Public Property IdentificadorCertificadoAnterior As String

        <XmlIgnore()> _
        Public Property IdentificadorSaldoEfectivo As String

#End Region

    End Class

End Namespace