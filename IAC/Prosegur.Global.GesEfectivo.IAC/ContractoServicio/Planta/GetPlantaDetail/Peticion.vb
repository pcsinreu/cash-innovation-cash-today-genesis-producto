Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.GetPlantaDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetPlantaDetail")> _
    <XmlRoot(Namespace:="urn:GetPlantaDetail")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _OidPlanta As String

#End Region

#Region "[Propriedades]"

        Public Property OidPlanta() As String
            Get
                Return _OidPlanta
            End Get
            Set(value As String)
                _OidPlanta = value
            End Set
        End Property

#End Region

    End Class

End Namespace
