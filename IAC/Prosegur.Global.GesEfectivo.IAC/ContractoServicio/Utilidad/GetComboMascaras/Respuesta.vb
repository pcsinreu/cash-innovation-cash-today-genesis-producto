Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboMascaras

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboMascaras")> _
    <XmlRoot(Namespace:="urn:GetComboMascaras")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _Mascaras As MascaraColeccion

        Public Property Mascaras() As MascaraColeccion
            Get
                Return _Mascaras
            End Get
            Set(value As MascaraColeccion)
                _Mascaras = value
            End Set
        End Property

    End Class

End Namespace