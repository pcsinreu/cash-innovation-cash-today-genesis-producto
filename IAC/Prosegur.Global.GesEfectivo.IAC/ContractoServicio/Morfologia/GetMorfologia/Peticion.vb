Imports System.Xml.Serialization
Imports System.Xml

Namespace Morfologia.GetMorfologia

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetMorfologia")> _
    <XmlRoot(Namespace:="urn:GetMorfologia")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _codMorfologia As String
        Private _desMorfologia As String
        Private _bolVigente As Boolean

#End Region

#Region "[Propriedades]"

        Public Property CodMorfologia() As String
            Get
                Return _codMorfologia
            End Get
            Set(value As String)
                _codMorfologia = value
            End Set
        End Property

        Public Property DesMorfologia() As String
            Get
                Return _desMorfologia
            End Get
            Set(value As String)
                _desMorfologia = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace