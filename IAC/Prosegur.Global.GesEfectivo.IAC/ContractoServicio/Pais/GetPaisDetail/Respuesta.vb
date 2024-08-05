Imports System.Xml.Serialization
Imports System.Xml

Namespace Pais.GetPaisDetail

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 26/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetPaisDetail")> _
    <XmlRoot(Namespace:="urn:GetPaisDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Pais As PaisColeccion
#End Region

#Region "Propriedades"

        Public Property Pais() As PaisColeccion
            Get
                Return _Pais
            End Get
            Set(value As PaisColeccion)
                _Pais = value
            End Set
        End Property

#End Region

    End Class

End Namespace
