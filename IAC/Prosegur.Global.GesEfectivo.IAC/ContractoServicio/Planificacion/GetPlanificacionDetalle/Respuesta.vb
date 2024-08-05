Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon
Namespace Planificacion.GetPlanificacionDetalle

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetPlanificacionDetalle")> _
    <XmlRoot(Namespace:="urn:GetPlanificacionDetalle")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Planificacion As Clases.Planificacion
        ' Private _Programaciones As List(Of Programacion)
        Private _Maquinas As List(Of Maquina)
#End Region

#Region "Propriedades"

        Public Property Planificacion() As Clases.Planificacion
            Get
                Return _Planificacion
            End Get
            Set(value As Clases.Planificacion)
                _Planificacion = value
            End Set
        End Property

        'Public Property Programaciones() As List(Of Programacion)
        '    Get
        '        Return _Programaciones
        '    End Get
        '    Set(value As List(Of Programacion))
        '        _Programaciones = value
        '    End Set
        'End Property

    
        Public Property Maquinas() As List(Of Maquina)
            Get
                Return _Maquinas
            End Get
            Set(value As List(Of Maquina))
                _Maquinas = value
            End Set
        End Property
#End Region

    End Class
End Namespace

