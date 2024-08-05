Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos

    <XmlType(Namespace:="urn:ObtenerCuentaPorCodigos")> _
    <XmlRoot(Namespace:="urn:ObtenerCuentaPorCodigos")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property CuentaMovimiento As Clases.Cuenta
        Public Property CuentaSaldo As Clases.Cuenta

        Sub New()
            MyBase.New()
        End Sub

        Sub New(mensaje As String)
            MyBase.New(mensaje)
        End Sub

        Sub New(exception As Exception)
            MyBase.New(exception)
        End Sub

    End Class

End Namespace