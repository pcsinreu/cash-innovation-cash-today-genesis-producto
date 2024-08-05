Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarSeguimientoElemento

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <XmlRoot(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Elementos As List(Of ElementoRespuesta)

    End Class
End Namespace