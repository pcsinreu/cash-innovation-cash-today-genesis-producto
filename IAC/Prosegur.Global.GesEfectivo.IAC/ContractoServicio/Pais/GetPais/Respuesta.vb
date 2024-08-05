Imports System.Xml.Serialization
Imports System.Xml

Namespace Pais.GetPais

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 26/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetPais")> _
    <XmlRoot(Namespace:="urn:GetPais")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Pais As PaisColeccion
        Private _Resultado As String

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

