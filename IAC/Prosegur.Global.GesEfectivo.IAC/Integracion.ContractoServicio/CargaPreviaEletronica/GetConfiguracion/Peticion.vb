Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguracion

    <XmlType(Namespace:="urn:GetConfiguracion")> _
    <XmlRoot(Namespace:="urn:GetConfiguracion")> _
    <Serializable()>
    Public Class Peticion

        Public Property IdentificadorConfiguracion() As String
        Public Property CodigoConfiguracion() As String


    End Class

End Namespace