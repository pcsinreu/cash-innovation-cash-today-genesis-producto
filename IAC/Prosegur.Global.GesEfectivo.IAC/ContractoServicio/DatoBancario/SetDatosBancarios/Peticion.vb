Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace DatoBancario.SetDatosBancarios

    <Serializable()> _
    <XmlType(Namespace:="urn:SetDatosBancarios")> _
    <XmlRoot(Namespace:="urn:SetDatosBancarios")> _
    Public Class Peticion
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BasePeticion
        Public Property CodigoUsuario As String
        Public Property IdentificadorCliente As String
        Public Property IdentificadorSubCliente As String
        Public Property IdentificadorPuntoServicio As String
        Public Property DatosBancarios As List(Of Clases.DatoBancario)

    End Class

End Namespace
