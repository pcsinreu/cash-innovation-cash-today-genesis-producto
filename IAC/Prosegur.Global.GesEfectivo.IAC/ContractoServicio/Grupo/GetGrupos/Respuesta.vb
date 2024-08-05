Imports System.Xml.Serialization
Imports System.Xml

Namespace Grupo.GetGrupos

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetGrupos")> _
    <XmlRoot(Namespace:="urn:GetGrupos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _grupos As List(Of Grupo)

#End Region

#Region "[Propriedades]"

        Public Property Grupos() As List(Of Grupo)
            Get
                Return _grupos
            End Get
            Set(value As List(Of Grupo))
                _grupos = value
            End Set
        End Property

#End Region

    End Class

End Namespace