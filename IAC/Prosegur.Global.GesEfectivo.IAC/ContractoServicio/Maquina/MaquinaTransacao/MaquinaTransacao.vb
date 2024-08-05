Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Maquina.GetMaquinaTransacao

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetMaquinaTransacao")>
    <XmlRoot(Namespace:="urn:GetMaquinaTransacao")>
    <Serializable()>
    Public Class MaquinaTransacao

#Region "Variáveis"

        Private _CodIndentificacion As String
        Private _DesSector As String

#End Region

#Region "Propriedades"

        Public Property CodIndentificacion As String
            Get
                Return _CodIndentificacion
            End Get
            Set(value As String)
                _CodIndentificacion = value
            End Set
        End Property

        Public Property DesSector As String
            Get
                Return _DesSector
            End Get
            Set(value As String)
                _DesSector = value
            End Set
        End Property

#End Region

    End Class
End Namespace

