Imports System.Xml.Serialization
Imports System.Xml

Namespace Grupo.SetGrupo

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetGrupo")> _
    <XmlRoot(Namespace:="urn:SetGrupo")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _codigoGrupo As String
        Private _descripcionGrupo As String
        Private _codUsuario As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoGrupo() As String
            Get
                Return _codigoGrupo
            End Get
            Set(value As String)
                _codigoGrupo = value
            End Set
        End Property

        Public Property DescripcionGrupo() As String
            Get
                Return _descripcionGrupo
            End Get
            Set(value As String)
                _descripcionGrupo = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace