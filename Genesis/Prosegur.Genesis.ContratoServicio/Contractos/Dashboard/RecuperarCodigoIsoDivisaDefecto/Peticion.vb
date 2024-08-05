Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.Dashboard.RecuperarCodigoIsoDivisaDefecto

    <XmlType(Namespace:="urn:RecuperarCodigoIsoDivisaDefecto")> _
    <XmlRoot(Namespace:="urn:RecuperarCodigoIsoDivisaDefecto")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property CodigoDelegacion As String
        Public Property CodigoAplicacion As String

#End Region

    End Class

End Namespace