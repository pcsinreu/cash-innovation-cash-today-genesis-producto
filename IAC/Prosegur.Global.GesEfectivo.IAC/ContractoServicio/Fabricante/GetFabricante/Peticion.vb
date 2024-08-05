Imports System.Xml.Serialization
Imports System.Xml

Namespace Fabricante.GetFabricante

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetFabricante")> _
    <XmlRoot(Namespace:="urn:GetFabricante")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _CodigoFabricante As String
        Private _Description As String
        Private _Vigente As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodFabricante As String
            Get
                Return _CodigoFabricante
            End Get
            Set(value As String)
                _CodigoFabricante = value
            End Set
        End Property

        Public Property DesFabricante() As String
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
