Imports System.Xml.Serialization
Imports System.Xml

Namespace Canal.SetCanal
    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pagoncalves] 13/05/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetCanal")> _
    <XmlRoot(Namespace:="urn:SetCanal")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"
        Private _canales As CanalColeccion
        Private _codUsuario As String
#End Region

#Region "Propriedades"

        Public Property Canales() As CanalColeccion
            Get
                Return _canales
            End Get
            Set(value As CanalColeccion)
                _canales = value
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