Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoPlanificacion.GetTiposPlanificaciones
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>SS
    ''' [pgoncalves] 05/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTiposPlanificaciones")> _
    <XmlRoot(Namespace:="urn:GetTiposPlanificaciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[VARIAVEIS]"

        Private _ListaTiposPlanificiones As List(Of TipoPlanificacion)
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property TiposPlanificaciones() As List(Of TipoPlanificacion)
            Get
                Return _ListaTiposPlanificiones
            End Get
            Set(value As List(Of TipoPlanificacion))
                _ListaTiposPlanificiones = value
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

