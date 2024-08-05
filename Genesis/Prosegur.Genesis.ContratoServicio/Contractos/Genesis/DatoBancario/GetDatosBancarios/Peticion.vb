Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace DatoBancario.GetDatosBancarios

    <Serializable()> _
    <XmlType(Namespace:="urn:GetDatosBancarios")> _
    <XmlRoot(Namespace:="urn:GetDatosBancarios")> _
    Public Class Peticion
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BasePeticion

        Public Property IdentificadorCliente As String
        Public Property IdentificadorSubCliente As String
        Public Property IdentificadorPuntoServicio As String
        Public Property IdentificadorDivisa As String
        Public Property IdentificadorBanco As String
        Public Property ObtenerSubNiveis As Boolean

    End Class

End Namespace
