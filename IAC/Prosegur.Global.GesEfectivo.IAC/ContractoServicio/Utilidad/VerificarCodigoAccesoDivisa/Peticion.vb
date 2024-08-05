Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoAccesoDivisa

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:VerificarCodigoAccesoDivisa")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoAccesoDivisa")> _
    <Serializable()> _
    Public Class Peticion

        Public Property CodigoAcceso As String

    End Class

End Namespace