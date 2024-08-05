Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.GetPlantaDetail

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetPlantaDetail")> _
    <XmlRoot(Namespace:="urn:GetPlantaDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Planta As Planta
#End Region

#Region "Propriedades"

        Public Property Planta() As Planta
            Get
                Return _Planta
            End Get
            Set(value As Planta)
                _Planta = value
            End Set
        End Property

#End Region

    End Class
End Namespace

