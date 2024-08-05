Imports System.Xml.Serialization
Imports System.Xml

Namespace Configuracion.General

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:ConfiguracionGeneral")> _
    <XmlRoot(Namespace:="urn:ConfiguracionGeneral")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico
        Public ConfiguracionGeneralColeccion As List(Of ConfiguracionGeneral)
        Public ConfiguracionGeneral As ConfiguracionGeneral
    End Class

End Namespace