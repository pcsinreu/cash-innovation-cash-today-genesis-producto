Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.RecuperarPlanificaciones

    <XmlType(Namespace:="urn:RecuperarPlanificaciones")>
    <XmlRoot(Namespace:="urn:RecuperarPlanificaciones")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest

        Public Property Codigos As List(Of String)

        <DefaultValue("")>
        Public Property CodigoTipo As String

        <DefaultValue("")>
        Public Property CodigoBanco As String

        <DefaultValue(2)>
        Public Property Vigente As Prosegur.Genesis.Comon.Enumeradores.TipoVigente

        <DefaultValue(False)>
        Public Property RecuperarMaquinas As Boolean

    End Class

End Namespace