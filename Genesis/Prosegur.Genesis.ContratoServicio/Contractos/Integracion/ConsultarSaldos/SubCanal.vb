Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <Serializable()>
    Public Class SubCanal

        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Divisas As List(Of Divisa)

    End Class

End Namespace