Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <Serializable()>
    Public Class SectorSaldo

        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Clientes As List(Of Cliente)

    End Class

End Namespace