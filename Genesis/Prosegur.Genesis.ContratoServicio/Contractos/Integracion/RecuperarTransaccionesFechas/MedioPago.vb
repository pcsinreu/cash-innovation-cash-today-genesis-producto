Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransaccionesFechas

    <Serializable()>
    Public Class MedioPago

        Public Property CodigoIsoDivisa As String
        Public Property CodigoTipoMedioPago As String
        Public Property CodigoMedioPago As String
        Public Property DescripcionMedioPago As String
        Public Property Unidades As Integer
        Public Property Importe As Decimal

    End Class

End Namespace