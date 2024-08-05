Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos
    <Serializable()>
    Public Class Canal
        Inherits Comon.Entidad
        Public Property SubCanales As List(Of SubCanal)
    End Class
End Namespace