Imports System.Xml.Serialization

Namespace Login.EjecutarLoginAplicacion


    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 27/06/2013 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:Login.EjecutarLoginAplicacion")> _
    <XmlRoot(Namespace:="urn:Login.EjecutarLoginAplicacion")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property InformacionUsuario As New InformacionUsuario
        Public Property ResultadoOperacion As ResultadoOperacionLoginLocal


    End Class

End Namespace