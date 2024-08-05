Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Fabricante.GetFabricante

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetFabricante")> _
    <XmlRoot(Namespace:="urn:GetFabricante")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Fabricante As List(Of Clases.Fabricante)
        Private _Resultado As String

#End Region

#Region "Propriedades"

        Public Property Fabricante() As List(Of Clases.Fabricante)
            Get
                Return _Fabricante
            End Get
            Set(value As List(Of Clases.Fabricante))
                _Fabricante = value
            End Set
        End Property

        Public Property Resultado() As String
            Get
                Return _Resultado
            End Get
            Set(value As String)
                _Resultado = value
            End Set
        End Property
#End Region

    End Class
End Namespace

