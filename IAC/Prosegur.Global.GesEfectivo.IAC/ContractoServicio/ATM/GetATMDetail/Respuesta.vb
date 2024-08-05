Imports System.Xml.Serialization
Imports System.Xml

Namespace ATM.GetATMDetail

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 07/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetATMDetail")> _
    <XmlRoot(Namespace:="urn:GetATMDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _atm As ATM

#End Region

#Region "[Propriedades]"

        Public Property ATM() As ATM
            Get
                Return _atm
            End Get
            Set(value As ATM)
                _atm = value
            End Set
        End Property

#End Region

    End Class

End Namespace