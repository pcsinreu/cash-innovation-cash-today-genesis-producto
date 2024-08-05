Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Cuenta.ObtenerCuentas

    <XmlType(Namespace:="urn:ObtenerCuentas")> _
    <XmlRoot(Namespace:="urn:ObtenerCuentas")> _
    <Serializable()>
    Public Class Peticion
        Inherits Contractos.Integracion.Comon.BasePeticion

        Public Property CuentaMovimiento As DatosCuenta

        Public Property CuentaSaldo As DatosCuenta

        Public Property ObtenerVersionSimplificada As Boolean

        Public Property CrearConfiguiracionNivelSaldo As Boolean

        Public Property PermitirCualquierTotalizadorSaldoServicio As Boolean
        Public Property EsDocumentoDeValor As Boolean
    End Class

End Namespace