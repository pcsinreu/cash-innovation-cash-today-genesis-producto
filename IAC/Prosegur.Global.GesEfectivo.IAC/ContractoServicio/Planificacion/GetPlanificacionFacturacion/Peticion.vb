Imports System.Xml.Serialization
Imports System.Xml

Namespace Planificacion.GetPlanificacionFacturacion

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetPlanificacionFacturacion")>
    <XmlRoot(Namespace:="urn:GetPlanificacionFacturacion")>
    <Serializable()>
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _OidPlanificacion As String
        Private _OidDelegacion As String

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
        Public Property OidDelegacion As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
            End Set
        End Property



#End Region

    End Class
End Namespace
