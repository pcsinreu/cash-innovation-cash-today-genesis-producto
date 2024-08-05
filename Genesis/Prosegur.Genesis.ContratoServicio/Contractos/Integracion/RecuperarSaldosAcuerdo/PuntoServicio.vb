Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosAcuerdo
    <Serializable()>
    Public Class PuntoServicio

        <XmlAttributeAttribute()>
        Public Property SourceReferenceId As String
        Public Property ContractId As String
        Public Property ServiceOrderId As String
        Public Property ServiceOrderCode As String
        Public Property ProductCode As String
        Public Property CodigoPuntoServicio As String
        Public Property CashIns As List(Of CashIn)
        Public Property ShipOuts As List(Of ShipOut)
        Public Property Acreditaciones As List(Of Acreditacion)

        Public Sub New()
            'CashIns = New List(Of CashIn)
            'ShipOuts = New List(Of ShipOut)
            'Acreditaciones = New List(Of Acreditacion)
        End Sub
    End Class
End Namespace
