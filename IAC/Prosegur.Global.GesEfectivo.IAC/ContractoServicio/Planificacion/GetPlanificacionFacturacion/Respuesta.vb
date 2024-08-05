Imports System.Xml.Serialization
Imports System.Xml

Namespace Planificacion.GetPlanificacionFacturacion

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetPlanificacionFacturacion")>
    <XmlRoot(Namespace:="urn:GetPlanificacionFacturacion")>
    <Serializable()>
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Planificacion As Planificacion
        Private _Resultado As String

#End Region

#Region "Propriedades"

        Public Property Planificacion() As Planificacion
            Get
                Return _Planificacion
            End Get
            Set(value As Planificacion)
                _Planificacion = value
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

