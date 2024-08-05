Imports System.Xml.Serialization
Imports System.Xml

Namespace Planificacion.GetPlanificacionDetalle

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetPlanificacionDetalle")> _
    <XmlRoot(Namespace:="urn:GetPlanificacionDetalle")> _
    <Serializable()> _
    Public Class Maquina
#Region "[VARIAVEIS]"

        Private _OidMaquina As String
        Private _DeviceID As String
        Private _Descripcion As String
        Private _DesModelo As String
        ' Private _DesPlanificacion As String
        Private _DesFabricante As String
        Private _BolActivo As Boolean
        'Private _FechaValor As Boolean
        ' Private _FechaVigenciaInicio As DateTime
        ' Private _FechaVigenciaFin As DateTime
        'Private _Cliente As String
        '' Private _SubCliente As String
        'Private _PtoServicio As String
        ' Private _CodigoCliente As String
        ' Private _CodigoSubCliente As String
        ' Private _CodigoPtoServicio As String


#End Region

#Region "[PROPRIEDADE]"

        Public Property OidMaquina As String
            Get
                Return _OidMaquina
            End Get
            Set(value As String)
                _OidMaquina = value
            End Set
        End Property

        Public Property DeviceID As String
            Get
                Return _DeviceID
            End Get
            Set(value As String)
                _DeviceID = value
            End Set
        End Property

       

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property DesFabricante As String
            Get
                Return _DesFabricante
            End Get
            Set(value As String)
                _DesFabricante = value
            End Set
        End Property

        Public Property DesModelo As String
            Get
                Return _DesModelo
            End Get
            Set(value As String)
                _DesModelo = value
            End Set
        End Property

        Public Property BolActivo As Boolean
            Get
                Return _BolActivo
            End Get
            Set(value As Boolean)
                _BolActivo = value
            End Set
        End Property



#End Region
    End Class
End Namespace