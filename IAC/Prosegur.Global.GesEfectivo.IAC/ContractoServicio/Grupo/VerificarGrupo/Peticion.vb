Imports System.Xml.Serialization
Imports System.Xml

Namespace Grupo.VerificarGrupo

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarGrupo")> _
    <XmlRoot(Namespace:="urn:VerificarGrupo")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _codigoGrupo As String

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

#End Region

    End Class

End Namespace