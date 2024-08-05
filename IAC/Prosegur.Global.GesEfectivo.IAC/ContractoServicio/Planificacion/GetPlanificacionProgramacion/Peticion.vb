Imports System.Xml.Serialization
Imports System.Xml

Namespace Planificacion.GetPlanificacionProgramacion

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetPlanificacionProgramacion")> _
    <XmlRoot(Namespace:="urn:GetPlanificacionProgramacion")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _OidPlanificacion As String
        Private _OidBanco As String
        Private _DesPlanificacion As String
        Private _Vigente As Nullable(Of Boolean)
        Private _OidTipoPlanificacion As String
#End Region

#Region "[PROPRIEDADE]"

        Public Property OidPlanificacion As String
            Get
                Return _OidPlanificacion
            End Get
            Set(value As String)
                _OidPlanificacion = value
            End Set
        End Property

        Public Property OidBanco As String
            Get
                Return _OidBanco
            End Get
            Set(value As String)
                _OidBanco = value
            End Set
        End Property

        Public Property DesPlanificacion() As String
            Get
                Return _DesPlanificacion
            End Get
            Set(value As String)
                _DesPlanificacion = value
            End Set
        End Property

        Public Property OidTipoPlanificacion As String
            Get
                Return _OidTipoPlanificacion
            End Get
            Set(value As String)
                _OidTipoPlanificacion = value
            End Set
        End Property

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

#End Region

    End Class
End Namespace
