Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboFormatos

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboFormatos")> _
    <XmlRoot(Namespace:="urn:GetComboFormatos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _Formatos As FormatoColeccion

        Public Property Formatos() As FormatoColeccion
            Get
                Return _Formatos
            End Get
            Set(value As FormatoColeccion)
                _Formatos = value
            End Set
        End Property

    End Class

End Namespace