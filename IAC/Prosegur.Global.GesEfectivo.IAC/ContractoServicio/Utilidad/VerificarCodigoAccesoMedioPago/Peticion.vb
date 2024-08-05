Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoAccesoMedioPago

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:VerificarCodigoAccesoMedioPago")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoAccesoMedioPago")> _
    <Serializable()> _
    Public Class Peticion

        Public Property CodigoAcceso As String
        Public Property CodigoDivisa As String

    End Class

End Namespace