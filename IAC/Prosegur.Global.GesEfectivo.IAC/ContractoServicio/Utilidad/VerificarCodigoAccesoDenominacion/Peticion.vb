Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoAccesoDenominacion

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:VerificarCodigoAccesoDenominacion")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoAccesoDenominacion")> _
    <Serializable()> _
    Public Class Peticion

        Public Property CodigoAcceso As String
        Public Property CodigoDivisa As String

    End Class

End Namespace