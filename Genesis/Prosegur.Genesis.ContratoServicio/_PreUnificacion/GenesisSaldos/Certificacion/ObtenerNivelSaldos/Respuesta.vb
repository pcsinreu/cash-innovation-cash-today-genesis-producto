Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion.ObtenerNivelSaldos

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 31/05/2013 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:ObtenerNivelSaldos")> _
    <XmlRoot(Namespace:="urn:ObtenerNivelSaldos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property NivelSaldos As NivelSaldos

    End Class

End Namespace