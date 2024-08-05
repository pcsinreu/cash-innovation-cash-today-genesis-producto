Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.GetPlanta
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>SS
    ''' [pgoncalves] 19/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetPlanta")> _
    <XmlRoot(Namespace:="urn:GetPlanta")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _Planta As PlantaColeccion
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property Planta() As PlantaColeccion
            Get
                Return _Planta
            End Get
            Set(value As PlantaColeccion)
                _Planta = value
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


