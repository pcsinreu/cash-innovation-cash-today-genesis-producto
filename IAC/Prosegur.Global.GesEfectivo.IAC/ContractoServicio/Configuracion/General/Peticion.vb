Imports System.Xml.Serialization
Imports System.Xml

Namespace Configuracion.General

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 04/07/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:ConfiguracionGeneral")> _
    <XmlRoot(Namespace:="urn:ConfiguracionGeneral")> _
    <Serializable()> _
    Public Class Peticion
        Public Property ConfiguracionGeneral As ConfiguracionGeneral
        Public ListaOIDConfiguracionGeneral As List(Of String)
    End Class

End Namespace
