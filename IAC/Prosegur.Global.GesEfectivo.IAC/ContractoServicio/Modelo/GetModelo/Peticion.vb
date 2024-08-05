Imports System.Xml.Serialization
Imports System.Xml

Namespace Modelo.GetModelo

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetModelo")> _
    <XmlRoot(Namespace:="urn:GetModelo")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _CodigoModelo As String
        Private _OidFabricante As String
        Private _Description As String
        Private _Vigente As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodModelo As String
            Get
                Return _CodigoModelo
            End Get
            Set(value As String)
                _CodigoModelo = value
            End Set
        End Property

        Public Property OidFabricante() As String
            Get
                Return _OidFabricante
            End Get
            Set(value As String)
                _OidFabricante = value
            End Set
        End Property

        Public Property DesModelo() As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property

        Public Property BolVigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

#End Region
    End Class
End Namespace
