Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Maquina.GetMaquina

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetMaquina")> _
    <XmlRoot(Namespace:="urn:GetMaquina")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Maquina As List(Of Maquina)
        Private _Resultado As String

#End Region

#Region "Propriedades"

        Public Property Maquinas() As List(Of Maquina)
            Get
                Return _Maquina
            End Get
            Set(value As List(Of Maquina))
                _Maquina = value
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

