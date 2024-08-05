Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos
Imports System.Collections.ObjectModel

Namespace Maquina.GetMaquinaDetalle

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetMaquinaDetalle")> _
    <XmlRoot(Namespace:="urn:GetMaquinaDetalle")> _
    <Serializable()> _
    Public Class Maquina

#Region "[VARIAVEIS]"

        Private _OidMaquina As String
        Private _OidDelegacion As String
        Private _OidPlanta As String
        Private _DeviceID As String
        Private _Descripcion As String
        Private _PuntosServicio As List(Of Clases.Cliente)
        Private _OidFabricante As String
        Private _OidModelo As String
        Private _Planificacion As Clases.Planificacion
        Private _FechaValor As Boolean
        Private _FechaValorInicio As DateTime
        Private _FechaValorFin As DateTime
        Private _OidSector As String
        Private _ConsideraRecuentos As Boolean
        Private _BolActivo As Boolean
        Private _DesUsuarioCreacion As String
        Private _FechaHoraCreacion As DateTime
        Private _DesUsuarioModificacion As String
        Private _FechaHoraModificacion As DateTime
        Private _MultiClientes As Boolean
        Private _PorcComisionMaquina As Nullable(Of Decimal)
        Private _BancoTesoreriaPlanxMaquina As Prosegur.Genesis.Comon.Clases.SubCliente
        Private _CuentaTesoreriaPlanxMaquina As Prosegur.Genesis.Comon.Clases.PuntoServicio
        Private _CodigosAjenos As CodigoAjeno.CodigoAjenoColeccionBase
        Private _planes As ObservableCollection(Of Clases.PlanMaqPorCanalSubCanalPunto)
        Private _CumpleNombrePatron As Boolean
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

        Public Property OidDelegacion As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
            End Set
        End Property

        Public Property OidPlanta As String
            Get
                Return _OidPlanta
            End Get
            Set(value As String)
                _OidPlanta = value
            End Set
        End Property

        Public Property CumpleNombrePatron() As Boolean
            Get
                Return _CumpleNombrePatron
            End Get
            Set(ByVal value As Boolean)
                _CumpleNombrePatron = value
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

        Public Property PuntosServicio As List(Of Clases.Cliente)
            Get
                Return _PuntosServicio
            End Get
            Set(value As List(Of Clases.Cliente))
                _PuntosServicio = value
            End Set
        End Property

        Public Property OidFabricante As String
            Get
                Return _OidFabricante
            End Get
            Set(value As String)
                _OidFabricante = value
            End Set
        End Property

        Public Property OidModelo As String
            Get
                Return _OidModelo
            End Get
            Set(value As String)
                _OidModelo = value
            End Set
        End Property

        Public Property Planificacion As Clases.Planificacion
            Get
                Return _Planificacion
            End Get
            Set(value As Clases.Planificacion)
                _Planificacion = value
            End Set
        End Property

        Public Property FechaValor As Boolean
            Get
                Return _FechaValor
            End Get
            Set(value As Boolean)
                _FechaValor = value
            End Set
        End Property

        Public Property FechaValorInicio As DateTime
            Get
                Return _FechaValorInicio
            End Get
            Set(value As DateTime)
                _FechaValorInicio = value
            End Set
        End Property

        Public Property FechaValorFin As DateTime
            Get
                Return _FechaValorFin
            End Get
            Set(value As DateTime)
                _FechaValorFin = value
            End Set
        End Property

        Public Property OidSector As String
            Get
                Return _OidSector
            End Get
            Set(value As String)
                _OidSector = value
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

        Public Property DesUsuarioCreacion As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                _DesUsuarioCreacion = value
            End Set
        End Property

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                _FechaHoraCreacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion As String
            Get
                Return _DesUsuarioModificacion
            End Get
            Set(value As String)
                _DesUsuarioModificacion = value
            End Set
        End Property

        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                _FechaHoraModificacion = value
            End Set
        End Property

        Public Property ConsideraRecuentos() As Boolean
            Get
                Return _ConsideraRecuentos
            End Get
            Set(value As Boolean)
                _ConsideraRecuentos = value
            End Set
        End Property

        Public Property MultiClientes As Boolean
            Get
                Return _MultiClientes
            End Get
            Set(value As Boolean)
                _MultiClientes = value
            End Set
        End Property

        Public Property PorcComisionMaquina() As Nullable(Of Decimal)
            Get
                Return _PorcComisionMaquina
            End Get
            Set(value As Nullable(Of Decimal))
                _PorcComisionMaquina = value
            End Set
        End Property

        Public Property BancoTesoreriaPlanxMaquina() As Prosegur.Genesis.Comon.Clases.SubCliente
            Get
                Return _BancoTesoreriaPlanxMaquina
            End Get
            Set(value As Prosegur.Genesis.Comon.Clases.SubCliente)
                _BancoTesoreriaPlanxMaquina = value
            End Set
        End Property
        Public Property CuentaTesoreriaPlanxMaquina() As Prosegur.Genesis.Comon.Clases.PuntoServicio
            Get
                Return _CuentaTesoreriaPlanxMaquina
            End Get
            Set(value As Prosegur.Genesis.Comon.Clases.PuntoServicio)
                _CuentaTesoreriaPlanxMaquina = value
            End Set
        End Property

        Public Property Planes() As ObservableCollection(Of Clases.PlanMaqPorCanalSubCanalPunto)
            Get
                Return _planes
            End Get
            Set(ByVal value As ObservableCollection(Of Clases.PlanMaqPorCanalSubCanalPunto))
                _planes = value
            End Set
        End Property
        Public Property CodigosAjenos() As CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _CodigosAjenos
            End Get
            Set(value As CodigoAjeno.CodigoAjenoColeccionBase)
                _CodigosAjenos = value
            End Set
        End Property
#End Region

    End Class
End Namespace
