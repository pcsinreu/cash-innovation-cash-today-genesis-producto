Imports System.Xml.Serialization
Imports System.Xml

Namespace Planificacion.GetPlanificaciones

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetPlanificaciones")> _
    <XmlRoot(Namespace:="urn:GetPlanificaciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Planificacion As List(Of PlanXProgramacion)
        Private _Resultado As String

#End Region

#Region "Propriedades"

        Public Property Planificacion() As List(Of PlanXProgramacion)
            Get
                Return _Planificacion
            End Get
            Set(value As List(Of PlanXProgramacion))
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

