Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Modelo.GetModelo

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetModelo")> _
    <XmlRoot(Namespace:="urn:GetModelo")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Modelo As List(Of Clases.Modelo)
        Private _Resultado As String

#End Region

#Region "Propriedades"

        Public Property Modelo() As List(Of Clases.Modelo)
            Get
                Return _Modelo
            End Get
            Set(value As List(Of Clases.Modelo))
                _Modelo = value
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

