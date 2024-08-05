Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Planificacion.GetPlanificacionProgramacion

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetPlanificacionProgramacion")> _
    <XmlRoot(Namespace:="urn:GetPlanificacionProgramacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Planificacion As List(Of Clases.Planificacion)
        Private _Resultado As String

#End Region

#Region "Propriedades"

        Public Property Planificacion() As List(Of Clases.Planificacion)
            Get
                Return _Planificacion
            End Get
            Set(value As List(Of Clases.Planificacion))
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

