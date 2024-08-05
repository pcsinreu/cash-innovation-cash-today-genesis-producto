Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.RecuperarMAEs

    <XmlType(Namespace:="urn:RecuperarMAEs")>
    <XmlRoot(Namespace:="urn:RecuperarMAEs")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest

        Public Property Paginacion As Comon.Paginacion

        <DefaultValue("")>
        Public Property DeviceID As String

        <DefaultValue("")>
        Public Property CodigoDelegacion As String

        <DefaultValue("")>
        Public Property CodigoPlanta As String

        <DefaultValue("")>
        Public Property CodigoCliente As String

        <DefaultValue("")>
        Public Property CodigoSubCliente As String

        <DefaultValue("")>
        Public Property CodigoPuntoServicio As String

        <DefaultValue("")>
        Public Property CodigoModelo As String

        <DefaultValue("")>
        Public Property MaquinasVigente As String

        <DefaultValue("false")>
        Public Property RecuperarCodigosAjenos As String
        <DefaultValue("")>
        Public Property ConPlanificacion As String
    End Class

End Namespace