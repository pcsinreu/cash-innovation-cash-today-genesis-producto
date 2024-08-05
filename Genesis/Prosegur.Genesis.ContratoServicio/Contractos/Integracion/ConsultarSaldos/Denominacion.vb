Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <Serializable()>
    Public Class Denominacion

        Public Property Codigo As String
        Public Property DescripcionDenominacion As String
        Public Property Cantidad As Long
        Public Property Importe As Decimal

    End Class

End Namespace