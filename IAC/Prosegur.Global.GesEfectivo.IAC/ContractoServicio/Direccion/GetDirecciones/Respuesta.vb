Imports System.Xml.Serialization
Imports System.Xml

Namespace Direccion.GetDirecciones
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>SS
    ''' [pgoncalves] 24/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDirecciones")> _
    <XmlRoot(Namespace:="urn:GetDirecciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _Direccion As DireccionColeccion
        Private _Resultado As String
#End Region

#Region "[PROPRIEDADES]"

        Public Property Direccion() As DireccionColeccion
            Get
                Return _Direccion
            End Get
            Set(value As DireccionColeccion)
                _Direccion = value
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
