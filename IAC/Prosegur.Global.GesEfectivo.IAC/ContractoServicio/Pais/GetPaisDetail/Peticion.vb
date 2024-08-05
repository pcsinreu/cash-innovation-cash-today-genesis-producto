Imports System.Xml.Serialization
Imports System.Xml

Namespace Pais.GetPaisDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 26/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetPaisDetail")> _
    <XmlRoot(Namespace:="urn:GetPaisDetail")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoPais As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoPais() As String
            Get
                Return _CodigoPais
            End Get
            Set(value As String)
                _CodigoPais = value
            End Set
        End Property

#End Region
    End Class

End Namespace
