Imports System.Xml.Serialization

Namespace Seguridad.Delegacion.GrabarDelegacion

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes]  30/08/2013 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:GrabarDelegacion")> _
    <XmlRoot(Namespace:="urn:GrabarDelegacion")> _
    Public Class Peticion

        Public Property OidDelegacion As String
        Public Property OidPais As String
        Public Property CodDelegacion As String
        Public Property DesDelegacion As String
        Public Property NecGmtMinutos As Short
        Public Property FyhVeranoInicio As DateTime
        Public Property FyhVeranoFin As DateTime
        Public Property NecVeranoAjuste As Short
        Public Property DesZona As String
        Public Property NecMinutoInicio As Short
        Public Property NecMetrosBase As Short
        Public Property NecMinutoSalida As Short

    End Class

End Namespace