Imports System.Xml.Serialization
Imports System.Xml

Namespace Grupo.GetGrupos

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetGrupos")> _
    <XmlRoot(Namespace:="urn:GetGrupos")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _bolObtenerTodosGrupos As Boolean

#End Region

#Region "[Propriedades]"

        Public Property BolObtenerTodosGrupos() As Boolean
            Get
                Return _bolObtenerTodosGrupos
            End Get
            Set(value As Boolean)
                _bolObtenerTodosGrupos = value
            End Set
        End Property

#End Region

    End Class

End Namespace