Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.RecuperarComissionDatosBancarios

    <XmlType(Namespace:="urn:RecuperarComissionDatosBancarios")>
    <XmlRoot(Namespace:="urn:RecuperarComissionDatosBancarios")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest

        Public Property IdentificadorMaquina As List(Of String)

    End Class

End Namespace