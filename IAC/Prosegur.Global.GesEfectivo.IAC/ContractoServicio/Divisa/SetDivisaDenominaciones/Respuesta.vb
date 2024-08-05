Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.SetDivisasDenominaciones

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetDivisasDenominaciones")> _
    <XmlRoot(Namespace:="urn:SetDivisasDenominaciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _RespuestaDivisas As RespuestaDivisaColeccion

#End Region

#Region "[Propriedades]"

        Public Property RespuestaDivisas() As RespuestaDivisaColeccion
            Get
                Return _RespuestaDivisas
            End Get
            Set(value As RespuestaDivisaColeccion)
                _RespuestaDivisas = value
            End Set
        End Property

#End Region

    End Class

End Namespace