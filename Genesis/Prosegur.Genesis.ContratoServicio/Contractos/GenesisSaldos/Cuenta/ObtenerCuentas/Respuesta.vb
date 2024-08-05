Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Cuenta.ObtenerCuentas

    <XmlType(Namespace:="urn:ObtenerCuentas")> _
    <XmlRoot(Namespace:="urn:ObtenerCuentas")> _
    <Serializable()>
    Public Class Respuesta
        Inherits Contractos.Integracion.Comon.BaseRespuesta

        Public Property CuentaMovimiento As Clases.Cuenta
        Public Property CuentaSaldo As Clases.Cuenta

        Sub New()
            MyBase.New()
        End Sub

    End Class

End Namespace