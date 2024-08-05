Imports System.Xml
Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos.Contenedores.ArmarContenedores

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ArmarContenedores")> _
    <XmlRoot(Namespace:="urn:ArmarContenedores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property CodigoComprobante As String
        Public Property IdentificadorDocumento As String
        Public Property FechaCreacionContenedor As DateTime

    End Class
End Namespace