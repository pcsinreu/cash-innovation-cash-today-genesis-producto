Imports System.Xml.Serialization
Imports System.Xml

Namespace GetATMs

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 07/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetATMs")> _
    <XmlRoot(Namespace:="urn:GetATMs")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _atms As List(Of ATM)

#End Region

#Region "[Propriedades]"

        Public Property ATMs() As List(Of ATM)
            Get
                Return _atms
            End Get
            Set(value As List(Of ATM))
                _atms = value
            End Set
        End Property

#End Region

    End Class

End Namespace