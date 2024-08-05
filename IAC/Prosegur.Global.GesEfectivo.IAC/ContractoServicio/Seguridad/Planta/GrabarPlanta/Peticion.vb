Imports System.Xml.Serialization

Namespace Seguridad.Planta.GrabarPlanta

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes]  30/08/2013 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:GrabarPlanta")> _
    <XmlRoot(Namespace:="urn:GrabarPlanta")> _
    Public Class Peticion

        Public Property OidPlanta As String
        Public Property OidDelegacion As String
        Public Property CodPlanta As String
        Public Property DesPlanta As String

    End Class

End Namespace