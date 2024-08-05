Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.DesarmarContenedores

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:DesarmarContenedores")> _
    <XmlRoot(Namespace:="urn:DesarmarContenedores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property IdentificadorDocumento As String
        Public Property CodigoCombrobante As String

    End Class
End Namespace