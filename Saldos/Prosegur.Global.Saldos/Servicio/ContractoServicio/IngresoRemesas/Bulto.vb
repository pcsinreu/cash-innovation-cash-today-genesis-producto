Imports System.Xml.Serialization

Namespace IngresoRemesas

    ''' <summary>
    ''' Classe bulto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 27/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:IngresoRemesas")> _
    <XmlRoot(Namespace:="urn:IngresoRemesas")> _
    <Serializable()> _
    Public Class Bulto

#Region "[VARIÁVEIS]"

        Private _IdentificadorSOL As String
        Private _IdBultoOrigen As String
        Private _CodigoPrecinto As String
        Private _CodigoEstado As String
        Private _CodigoCliente As String
        Private _CodigoTipoTrabajo As String
        Private _DescripcionCliente As String
        Private _CodigoSubCliente As String
        Private _DescripcionSubCliente As String
        Private _CodigoPuntoServicio As String
        Private _DescripcionPuntoServicio As String
        Private _CodigoCanal As String
        Private _DescripcionCanal As String
        Private _CodigoSubcanal As String
        Private _CodigoRuta As String
        Private _FechaProceso As Nullable(Of Date)
        Private _NumeroParciales As Nullable(Of Integer)
        Private _CodigoTransporte As String
        Private _FechaTransporte As Nullable(Of Date)
        Private _CodigoBancoIngreso As String
        Private _BolBancoMadre As Boolean
        Private _CodigoUbicacion As String
        Private _CodigoFormato As String
        Private _DescripcionSubCanal As String
        Private _FechaConfeccion As Nullable(Of DateTime)
        Private _DeclaradosTotalBulto As DeclaradosTotalBulto
        Private _DeclaradosDetalleBulto As DeclaradosDetalleBulto
        Private _DeclaradosAgrupacionBulto As DeclaradosAgrupacionBulto
        Private _Parciales As Parciales
        Private _ValoresBulto As ValoresBulto
        Private _CodigoClienteSaldo As String
        Private _DescripcionClienteSaldo As String
        Private _CodigoSubClienteSaldo As String
        Private _DescripcionSubClienteSaldo As String
        Private _CodigoPuntoServicioSaldo As String
        Private _DescripcionPuntoServicioSaldo As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoTipoTrabajo() As String
            Get
                Return _CodigoTipoTrabajo
            End Get
            Set(value As String)
                _CodigoTipoTrabajo = value
            End Set
        End Property

        Public Property DescripcionSubCanal() As String
            Get
                Return _DescripcionSubCanal
            End Get
            Set(value As String)
                _DescripcionSubCanal = value
            End Set
        End Property

        Public Property IdBultoOrigen() As String
            Get
                Return _IdBultoOrigen
            End Get
            Set(value As String)
                _IdBultoOrigen = value
            End Set
        End Property

        Public Property IdentificadorSOL() As String
            Get
                Return _IdentificadorSOL
            End Get
            Set(value As String)
                _IdentificadorSOL = value
            End Set
        End Property

        Public Property CodigoPrecinto() As String
            Get
                Return _CodigoPrecinto
            End Get
            Set(value As String)
                _CodigoPrecinto = value
            End Set
        End Property

        Public Property CodigoEstado() As String
            Get
                Return _CodigoEstado
            End Get
            Set(value As String)
                _CodigoEstado = value
            End Set
        End Property

        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        Public Property DescripcionCliente() As String
            Get
                Return _DescripcionCliente
            End Get
            Set(value As String)
                _DescripcionCliente = value
            End Set
        End Property

        Public Property CodigoSubCliente() As String
            Get
                Return _CodigoSubCliente
            End Get
            Set(value As String)
                _CodigoSubCliente = value
            End Set
        End Property

        Public Property DescripcionSubCliente() As String
            Get
                Return _DescripcionSubCliente
            End Get
            Set(value As String)
                _DescripcionSubCliente = value
            End Set
        End Property

        Public Property CodigoPuntoServicio() As String
            Get
                Return _CodigoPuntoServicio
            End Get
            Set(value As String)
                _CodigoPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionPuntoServicio() As String
            Get
                Return _DescripcionPuntoServicio
            End Get
            Set(value As String)
                _DescripcionPuntoServicio = value
            End Set
        End Property

        Public Property CodigoCanal() As String
            Get
                Return _CodigoCanal
            End Get
            Set(value As String)
                _CodigoCanal = value
            End Set
        End Property

        Public Property DescripcionCanal() As String
            Get
                Return _DescripcionCanal
            End Get
            Set(value As String)
                _DescripcionCanal = value
            End Set
        End Property

        Public Property CodigoSubCanal() As String
            Get
                Return _CodigoSubcanal
            End Get
            Set(value As String)
                _CodigoSubcanal = value
            End Set
        End Property

        Public Property CodigoRuta() As String
            Get
                Return _CodigoRuta
            End Get
            Set(value As String)
                _CodigoRuta = value
            End Set
        End Property

        Public Property FechaProceso() As Nullable(Of Date)
            Get
                Return _FechaProceso
            End Get
            Set(value As Nullable(Of Date))
                _FechaProceso = value
            End Set
        End Property

        Public Property NumeroParciales() As Nullable(Of Integer)
            Get
                Return _NumeroParciales
            End Get
            Set(value As Nullable(Of Integer))
                _NumeroParciales = value
            End Set
        End Property

        Public Property CodigoTransporte() As String
            Get
                Return _CodigoTransporte
            End Get
            Set(value As String)
                _CodigoTransporte = value
            End Set
        End Property

        Public Property FechaTransporte() As Nullable(Of Date)
            Get
                Return _FechaTransporte
            End Get
            Set(value As Nullable(Of Date))
                _FechaTransporte = value
            End Set
        End Property

        Public Property CodigoBancoIngreso() As String
            Get
                Return _CodigoBancoIngreso
            End Get
            Set(value As String)
                _CodigoBancoIngreso = value
            End Set
        End Property

        Public Property BolBancoMadre() As Boolean
            Get
                Return _BolBancoMadre
            End Get
            Set(value As Boolean)
                _BolBancoMadre = value
            End Set
        End Property

        Public Property CodigoUbicacion() As String
            Get
                Return _CodigoUbicacion
            End Get
            Set(value As String)
                _CodigoUbicacion = value
            End Set
        End Property

        Public Property CodigoFormato() As String
            Get
                Return _CodigoFormato
            End Get
            Set(value As String)
                _CodigoFormato = value
            End Set
        End Property

        Public Property FechaConfeccion() As Nullable(Of DateTime)
            Get
                Return _FechaConfeccion
            End Get
            Set(value As Nullable(Of DateTime))
                _FechaConfeccion = value
            End Set
        End Property

        Public Property DeclaradosTotalBulto() As DeclaradosTotalBulto
            Get
                Return _DeclaradosTotalBulto
            End Get
            Set(value As DeclaradosTotalBulto)
                _DeclaradosTotalBulto = value
            End Set
        End Property

        Public Property DeclaradosDetBulto() As DeclaradosDetalleBulto
            Get
                Return _DeclaradosDetalleBulto
            End Get
            Set(value As DeclaradosDetalleBulto)
                _DeclaradosDetalleBulto = value
            End Set
        End Property

        Public Property DeclaradosAgrupacionBulto() As DeclaradosAgrupacionBulto
            Get
                Return _DeclaradosAgrupacionBulto
            End Get
            Set(value As DeclaradosAgrupacionBulto)
                _DeclaradosAgrupacionBulto = value
            End Set
        End Property

        Public Property Parciales() As Parciales
            Get
                Return _Parciales
            End Get
            Set(value As Parciales)
                _Parciales = value
            End Set
        End Property

        Public Property ValoresBulto() As ValoresBulto
            Get
                Return _ValoresBulto
            End Get
            Set(value As ValoresBulto)
                _ValoresBulto = value
            End Set
        End Property

        Public Property CodigoClienteSaldo() As String
            Get
                Return _CodigoClienteSaldo
            End Get
            Set(value As String)
                _CodigoClienteSaldo = value
            End Set
        End Property

        Public Property DescripcionClienteSaldo() As String
            Get
                Return _DescripcionClienteSaldo
            End Get
            Set(value As String)
                _DescripcionClienteSaldo = value
            End Set
        End Property

        Public Property CodigoSubClienteSaldo() As String
            Get
                Return _CodigoSubClienteSaldo
            End Get
            Set(value As String)
                _CodigoSubClienteSaldo = value
            End Set
        End Property

        Public Property DescripcionSubClienteSaldo() As String
            Get
                Return _DescripcionSubClienteSaldo
            End Get
            Set(value As String)
                _DescripcionSubClienteSaldo = value
            End Set
        End Property

        Public Property CodigoPuntoServicioSaldo() As String
            Get
                Return _CodigoPuntoServicioSaldo
            End Get
            Set(value As String)
                _CodigoPuntoServicioSaldo = value
            End Set
        End Property

        Public Property DescripcionPuntoServicioSaldo() As String
            Get
                Return _DescripcionPuntoServicioSaldo
            End Get
            Set(value As String)
                _DescripcionPuntoServicioSaldo = value
            End Set
        End Property

#End Region

    End Class

End Namespace