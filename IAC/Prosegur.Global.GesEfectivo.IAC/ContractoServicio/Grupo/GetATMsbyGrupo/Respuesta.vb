Imports System.Xml.Serialization
Imports System.Xml

Namespace Grupo.GetATMsbyGrupo

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetATMsbyGrupo")> _
    <XmlRoot(Namespace:="urn:GetATMsbyGrupo")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _clientes As List(Of Cliente)

#End Region

#Region "[Propriedades]"

        Public Property Clientes() As List(Of Cliente)
            Get
                Return _clientes
            End Get
            Set(value As List(Of Cliente))
                _clientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace