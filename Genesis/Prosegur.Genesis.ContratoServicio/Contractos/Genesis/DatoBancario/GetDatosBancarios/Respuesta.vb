Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace DatoBancario.GetDatosBancarios

    <Serializable()> _
    <XmlType(Namespace:="urn:GetDatosBancarios")> _
    <XmlRoot(Namespace:="urn:GetDatosBancarios")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property DatosBancarios As List(Of Clases.DatoBancario)

    End Class

End Namespace

