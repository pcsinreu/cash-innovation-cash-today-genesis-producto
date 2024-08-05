Imports System.Xml.Serialization
Imports System.Xml

Namespace GrupoCliente.SetGruposCliente

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetGruposCliente")> _
    <XmlRoot(Namespace:="urn:SetGruposCliente")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _CodGrupoCliente As String
        Private _OidGrupoCliente As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property oidGrupoCliente() As String
            Get
                Return _OidGrupoCliente
            End Get
            Set(value As String)
                _OidGrupoCliente = value
            End Set
        End Property

        Public Property CodGrupoCliente As String
            Get
                Return _CodGrupoCliente
            End Get
            Set(value As String)
                _CodGrupoCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace
