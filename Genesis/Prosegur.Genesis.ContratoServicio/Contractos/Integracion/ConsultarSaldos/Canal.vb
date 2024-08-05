Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <Serializable()>
    Public Class Canal

        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Subcanales As List(Of SubCanal)

    End Class

End Namespace