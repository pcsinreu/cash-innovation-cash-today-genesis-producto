Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.SetAgrupaciones

    ''' <summary>
    ''' Classe RespuestaAgrupacionColeccion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetAgrupaciones")> _
    <XmlRoot(Namespace:="urn:SetAgrupaciones")> _
    <Serializable()> _
    Public Class RespuestaAgrupacionColeccion
        Inherits List(Of RespuestaAgrupacion)

    End Class

End Namespace