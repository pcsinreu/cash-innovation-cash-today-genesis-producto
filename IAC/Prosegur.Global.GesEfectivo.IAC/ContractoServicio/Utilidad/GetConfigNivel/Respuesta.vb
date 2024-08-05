Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetConfigNivel

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 20/05/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetConfigNivel")> _
    <XmlRoot(Namespace:="urn:GetConfigNivel")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

        Public Property ConfigNivelMovs As ConfigNivelMovColeccion

    End Class

End Namespace