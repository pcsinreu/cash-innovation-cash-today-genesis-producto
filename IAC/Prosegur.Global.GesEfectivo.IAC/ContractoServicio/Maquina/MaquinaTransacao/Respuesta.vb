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
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Maquina As List(Of MaquinaTransacao)

#End Region

#Region "Propriedades"

        Public Property Maquinas() As List(Of MaquinaTransacao)
            Get
                Return _Maquina
            End Get
            Set(value As List(Of MaquinaTransacao))
                _Maquina = value
            End Set
        End Property

#End Region

    End Class
End Namespace

