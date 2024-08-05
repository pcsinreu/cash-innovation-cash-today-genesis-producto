Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPaises
    <XmlType(Namespace:="urn:RecuperarPaises.Entrada")>
    <XmlRoot(Namespace:="urn:RecuperarPaises.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property Paises As List(Of Comon.Entidad)
        Public Property Delegaciones As List(Of Comon.Entidad)
        Public Property Plantas As List(Of Comon.Entidad)
        Public Property RecuperarCodigosAjenos As String
        Public Property RecuperarHistoricoCambios As String
        Public Property RecuperarDatosFacturacion As String

    End Class

End Namespace