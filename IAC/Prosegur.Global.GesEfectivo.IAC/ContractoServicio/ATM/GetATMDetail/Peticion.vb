Imports System.Xml.Serialization
Imports System.Xml

Namespace ATM.GetATMDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 07/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetATMDetail")> _
    <XmlRoot(Namespace:="urn:GetATMDetail")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _oidCajero As String
        Private _oidGrupo As String

#End Region

#Region "[Propriedades]"

        Public Property OidCajero() As String
            Get
                Return _oidCajero
            End Get
            Set(value As String)
                _oidCajero = value
            End Set
        End Property

        Public Property OidGrupo() As String
            Get
                Return _oidGrupo
            End Get
            Set(value As String)
                _oidGrupo = value
            End Set
        End Property

#End Region

    End Class

End Namespace