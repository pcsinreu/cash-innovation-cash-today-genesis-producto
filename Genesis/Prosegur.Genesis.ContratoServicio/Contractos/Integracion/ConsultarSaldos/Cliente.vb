Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <Serializable()>
    Public Class Cliente

        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Canales As List(Of Canal)

    End Class

End Namespace