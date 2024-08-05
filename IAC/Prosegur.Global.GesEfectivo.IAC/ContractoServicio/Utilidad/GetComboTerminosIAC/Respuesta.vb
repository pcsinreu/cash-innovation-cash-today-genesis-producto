Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTerminosIAC

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTerminosIAC")> _
    <XmlRoot(Namespace:="urn:GetComboTerminosIAC")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _Terminos As TerminoColeccion

        Public Property Terminos() As TerminoColeccion
            Get
                Return _Terminos
            End Get
            Set(value As TerminoColeccion)
                _Terminos = value
            End Set
        End Property

    End Class

End Namespace