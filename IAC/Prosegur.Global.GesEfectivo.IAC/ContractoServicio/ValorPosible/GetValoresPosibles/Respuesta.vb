Imports System.Xml.Serialization
Imports System.Xml

Namespace ValorPosible.GetValoresPosibles

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetValoresPosibles")> _
    <XmlRoot(Namespace:="urn:GetValoresPosibles")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Terminos As TerminoColeccion

#End Region

#Region "[Propriedades]"

        Public Property Terminos() As TerminoColeccion
            Get
                Return _Terminos
            End Get
            Set(value As TerminoColeccion)
                _Terminos = value
            End Set
        End Property

#End Region

    End Class

End Namespace