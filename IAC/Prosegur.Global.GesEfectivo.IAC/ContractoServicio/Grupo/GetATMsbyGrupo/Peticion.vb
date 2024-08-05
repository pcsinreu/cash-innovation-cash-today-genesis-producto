Imports System.Xml.Serialization
Imports System.Xml

Namespace Grupo.GetATMsbyGrupo

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetATMsbyGrupo")> _
    <XmlRoot(Namespace:="urn:GetATMsbyGrupo")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _oidGrupo As String

#End Region

#Region "[Propriedades]"

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