Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Cuenta.RecuperarCuenta

    <XmlType(Namespace:="urn:RecuperarCuenta")> _
    <XmlRoot(Namespace:="urn:RecuperarCuenta")> _
    <Serializable()>
    Public Class FiltroParametros

        Public Property Cliente As FiltrosParametros
        Public Property SubCliente As FiltrosParametros
        Public Property PuntoServicio As FiltrosParametros
        Public Property Canal As FiltrosParametros
        Public Property SubCanal As FiltrosParametros
        Public Property Delegacion As FiltrosParametros
        Public Property Planta As FiltrosParametros
        Public Property Sector As FiltrosParametros

    End Class

End Namespace