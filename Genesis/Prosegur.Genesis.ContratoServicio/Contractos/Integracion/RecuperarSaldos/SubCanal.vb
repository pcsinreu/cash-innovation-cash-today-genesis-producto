Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos

    <Serializable()>
    Public Class SubCanal
        Inherits Comon.Entidad
        Public Property Saldos As List(Of Saldo)

    End Class
End Namespace

