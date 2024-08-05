Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel

Namespace MAE.SetMAE

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:SetMAE")> _
    <XmlRoot(Namespace:="urn:SetMAE")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _OidMaquina As String
        Private _OidPlanta As String
        Private _OidPlantaAnterior As String
        Private _DeviceID As String
        Private _Descripcion As String
        Private _OidModelo As String
        Private _FechaValorInicio As DateTime
        Private _FechaValorFin As DateTime?
        Private _PuntosServicio As List(Of Comon.Clases.Cliente)
        Private _CodigosAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion
        Private _DatosBancario As List(Of Comon.Clases.DatoBancario)
        Private _PeticionDatosBancarios As Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion
        Private _OidPlanificacion As String
        Private _Vigente As Nullable(Of Boolean)
        Private _gmtCreacion As Date
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As Date
        Private _desUsuarioModificacion As String
        Private _dicionario As Prosegur.Genesis.Comon.SerializableDictionary(Of String, String)
        Private _DeviceIDAnterior As String
        Private _ConsideraRecuentos As Boolean
        Private _MultiClientes As Boolean
        Private _PorcComisionMaquina As Nullable(Of Decimal)
        Private _BancoTesoreria As Comon.Clases.SubCliente
        Private _planMAExCanalesSubCanalesPuntos As ObservableCollection(Of Comon.Clases.PlanMaqPorCanalSubCanalPunto)



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

        Public Property OidPlanta() As String
            Get
                Return _OidPlanta
            End Get
            Set(value As String)
                _OidPlanta = value
            End Set
        End Property
        Public Property OidPlantaAnterior() As String
            Get
                Return _OidPlantaAnterior
            End Get
            Set(value As String)
                _OidPlantaAnterior = value
            End Set
        End Property
        Public Property DeviceID() As String
            Get
                Return _DeviceID
            End Get
            Set(value As String)
                _DeviceID = value
            End Set
        End Property

        Public Property DeviceIDAnterior() As String
            Get
                Return _DeviceIDAnterior
            End Get
            Set(value As String)
                _DeviceIDAnterior = value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property OidModelo() As String
            Get
                Return _OidModelo
            End Get
            Set(value As String)
                _OidModelo = value
            End Set
        End Property

        Public Property FechaValorInicio() As DateTime
            Get
                Return _FechaValorInicio
            End Get
            Set(value As DateTime)
                _FechaValorInicio = value
            End Set
        End Property

        Public Property FechaValorFin() As DateTime?
            Get
                Return _FechaValorFin
            End Get
            Set(value As DateTime?)
                _FechaValorFin = value
            End Set
        End Property

        Public Property PuntosServicio() As List(Of Comon.Clases.Cliente)
            Get
                Return _PuntosServicio
            End Get
            Set(value As List(Of Comon.Clases.Cliente))
                _PuntosServicio = value
            End Set
        End Property

        Public Property CodigosAjeno() As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion
            Get
                Return _CodigosAjeno
            End Get
            Set(value As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion)
                _CodigosAjeno = value
            End Set
        End Property

        Public Property DatosBancario() As List(Of Comon.Clases.DatoBancario)
            Get
                Return _DatosBancario
            End Get
            Set(value As List(Of Comon.Clases.DatoBancario))
                _DatosBancario = value
            End Set
        End Property

        Public Property PeticionDatosBancarios() As Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion
            Get
                Return _PeticionDatosBancarios
            End Get
            Set(value As Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion)
                _PeticionDatosBancarios = value
            End Set
        End Property

        Public Property OidPlanificacion() As String
            Get
                Return _OidPlanificacion
            End Get
            Set(value As String)
                _OidPlanificacion = value
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

        Public Property ConsideraRecuentos() As Boolean
            Get
                Return _ConsideraRecuentos
            End Get
            Set(value As Boolean)
                _ConsideraRecuentos = value
            End Set
        End Property

        Public Property gmtCreacion As Date
            Get
                Return _gmtCreacion
            End Get
            Set(value As Date)
                _gmtCreacion = value
            End Set
        End Property

        Public Property desUsuarioCreacion As String
            Get
                Return _desUsuarioCreacion
            End Get
            Set(value As String)
                _desUsuarioCreacion = value
            End Set
        End Property

        Public Property gmtModificacion As Date
            Get
                Return _gmtModificacion
            End Get
            Set(value As Date)
                _gmtModificacion = value
            End Set
        End Property

        Public Property desUsuarioModificacion As String
            Get
                Return _desUsuarioModificacion
            End Get
            Set(value As String)
                _desUsuarioModificacion = value
            End Set
        End Property

        Public Property dicionario As Prosegur.Genesis.Comon.SerializableDictionary(Of String, String)
            Get
                Return _dicionario
            End Get
            Set(value As Prosegur.Genesis.Comon.SerializableDictionary(Of String, String))
                _dicionario = value
            End Set
        End Property

        Public Property MultiClientes() As Boolean
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


        Public Property BancoTesoreria() As Comon.Clases.SubCliente
            Get
                Return _BancoTesoreria
            End Get
            Set(value As Comon.Clases.SubCliente)
                _BancoTesoreria = value
            End Set
        End Property

        Public Property PlanesCanalesSubcanalesPtos() As ObservableCollection(Of Comon.Clases.PlanMaqPorCanalSubCanalPunto)
            Get
                Return _planMAExCanalesSubCanalesPuntos
            End Get
            Set(ByVal value As ObservableCollection(Of Comon.Clases.PlanMaqPorCanalSubCanalPunto))
                _planMAExCanalesSubCanalesPuntos = value
            End Set
        End Property

        Public Limites As List(Of Comon.Clases.Limite)
#End Region
    End Class
End Namespace
