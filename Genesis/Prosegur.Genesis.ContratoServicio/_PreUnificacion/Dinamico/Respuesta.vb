Imports System.Xml.Serialization

Namespace Dinamico

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/05/2013 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:Dinamico")> _
    <XmlRoot(Namespace:="urn:Dinamico")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property Valores As List(Of Valor)

#End Region

    End Class
End Namespace
