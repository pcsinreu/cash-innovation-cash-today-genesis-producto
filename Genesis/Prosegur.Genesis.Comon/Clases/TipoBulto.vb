Namespace Clases

    <Serializable()>
    Public NotInheritable Class TipoBulto
        Inherits BindableBase

#Region "Construtores"

        Public Sub New()
            Me.TiposBultosDenominacion = New ObjectModel.ObservableCollection(Of TipoBultoDenominacion)()
        End Sub

#End Region

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _EstaActivo As String
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _NumImporteMaxSeguro As Decimal
        Private _EsAptoPicos As Boolean
        Private _EsAptoMezcla As Boolean
        ' Carregado para o Salidas
        Private _EsCajetin As Boolean
        Private _NelMaximoUnidades As Decimal
        Private _NecAgrupacion As Decimal
        Private _Divisa As Clases.Divisa
        Private _CodigoFormato As String
        ' Remodelagem para balanceamento (WI 2557 e 1922)
        Private _NumImporteMaxSeguroBalanceado As Decimal
        Private _NumImporteMaxSeguroBalanceadoBillete As Decimal
        Private _NumImporteMaxSeguroBalanceadoMoneda As Decimal
        Private _TiposBultosDenominacion As ObjectModel.ObservableCollection(Of TipoBultoDenominacion)
        Private _NecPrioridad As Integer
#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property EstaActivo As String
            Get
                Return _EstaActivo
            End Get
            Set(value As String)
                SetProperty(_EstaActivo, value, "EstaActivo")
            End Set
        End Property

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
            End Set
        End Property

        Public Property UsuarioCreacion As String
            Get
                Return _UsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioCreacion, value, "UsuarioCreacion")
            End Set
        End Property

        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraModificacion, value, "FechaHoraModificacion")
            End Set
        End Property

        Public Property UsuarioModificacion As String
            Get
                Return _UsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioModificacion, value, "UsuarioModificacion")
            End Set
        End Property

        Public Property CodigoFormato As String
            Get
                Return _CodigoFormato
            End Get
            Set(value As String)
                SetProperty(_CodigoFormato, value, "CodigoFormato")
            End Set
        End Property

        Public Property NumImporteMaxSeguro As Decimal
            Get
                Return _NumImporteMaxSeguro
            End Get
            Set(value As Decimal)
                SetProperty(_NumImporteMaxSeguro, value, "NumImporteMaxSeguro")
            End Set
        End Property

        Public Property EsAptoPicos As Boolean
            Get
                Return _EsAptoPicos
            End Get
            Set(value As Boolean)
                SetProperty(_EsAptoPicos, value, "EsAptoPicos")
            End Set
        End Property

        Public Property EsAptoMezcla As Boolean
            Get
                Return _EsAptoMezcla
            End Get
            Set(value As Boolean)
                SetProperty(_EsAptoMezcla, value, "EsAptoMezcla")
            End Set
        End Property

        Public Property EsCajetin As Boolean
            Get
                Return _EsCajetin
            End Get
            Set(value As Boolean)
                SetProperty(_EsCajetin, value, "EsCajetin")
            End Set
        End Property

        Public Property NumImporteMaxSeguroBalanceado As Decimal
            Get
                Return _NumImporteMaxSeguroBalanceado
            End Get
            Set(value As Decimal)
                SetProperty(_NumImporteMaxSeguroBalanceado, value, "NumImporteMaxSeguroBalanceado")
            End Set
        End Property

        Public Property NumImporteMaxSeguroBalanceadoBillete As Decimal
            Get
                Return _NumImporteMaxSeguroBalanceadoBillete
            End Get
            Set(value As Decimal)
                SetProperty(_NumImporteMaxSeguroBalanceadoBillete, value, "NumImporteMaxSeguroBalanceadoBillete")
            End Set
        End Property

        Public Property NumImporteMaxSeguroBalanceadoMoneda As Decimal
            Get
                Return _NumImporteMaxSeguroBalanceadoMoneda
            End Get
            Set(value As Decimal)
                SetProperty(_NumImporteMaxSeguroBalanceadoMoneda, value, "NumImporteMaxSeguroBalanceadoMoneda")
            End Set
        End Property

        Public Property TiposBultosDenominacion As ObjectModel.ObservableCollection(Of TipoBultoDenominacion)
            Get
                Return _TiposBultosDenominacion
            End Get
            Set(value As ObjectModel.ObservableCollection(Of TipoBultoDenominacion))
                SetProperty(_TiposBultosDenominacion, value, "TiposBultosDenominacion")
            End Set
        End Property

        Public Property NecPrioridad As Integer
            Get
                Return _NecPrioridad
            End Get
            Set(value As Integer)
                SetProperty(_NecPrioridad, value, "NecPrioridad")
            End Set
        End Property

#End Region

    End Class

    <Serializable()>
    Public NotInheritable Class TipoBultoDenominacion
        Inherits BindableBase

#Region "Contrutores"

        Public Sub New()

        End Sub

        Public Sub New(ByRef tipoBulto As Clases.TipoBulto)
            Me.TipoBulto = tipoBulto
            Me.TipoBulto.TiposBultosDenominacion.Add(Me)
        End Sub

#End Region

#Region "Variáveis"

        Private _Identificador As String
        Private _TipoBulto As Clases.TipoBulto
        Private _Denominacion As Clases.Denominacion
        Private _NecPrioridad As Integer
        Private _NelMaximoUnidades As Decimal
        Private _NecAgrupacion As Decimal
        Private _BolNecesitaPrecinto As Boolean
        Private _NumImporteMaxSeguroDenominacion As Decimal

#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnore()>
        Public Property TipoBulto As Clases.TipoBulto
            Get
                Return _TipoBulto
            End Get
            Set(value As Clases.TipoBulto)
                SetProperty(_TipoBulto, value, "TipoBulto")
            End Set
        End Property

        Public Property Denominacion As Clases.Denominacion
            Get
                Return _Denominacion
            End Get
            Set(value As Clases.Denominacion)
                SetProperty(_Denominacion, value, "Denominacion")
            End Set
        End Property

        Public Property NecPrioridad As Integer
            Get
                Return _NecPrioridad
            End Get
            Set(value As Integer)
                SetProperty(_NecPrioridad, value, "NecPrioridad")
            End Set
        End Property

        Public Property NelMaximoUnidades As Decimal
            Get
                Return _NelMaximoUnidades
            End Get
            Set(value As Decimal)
                SetProperty(_NelMaximoUnidades, value, "NelMaximoUnidades")
            End Set
        End Property

        Public Property NecAgrupacion As Decimal
            Get
                Return _NecAgrupacion
            End Get
            Set(value As Decimal)
                SetProperty(_NecAgrupacion, value, "NecAgrupacion")
            End Set
        End Property

        Public Property BolNecesitaPrecinto As Boolean
            Get
                Return _BolNecesitaPrecinto
            End Get
            Set(value As Boolean)
                SetProperty(_BolNecesitaPrecinto, value, "BolNecesitaPrecinto")
            End Set
        End Property

        Public Property NumImporteMaxSeguroDenominacion As Decimal
            Get
                Return _NumImporteMaxSeguroDenominacion
            End Get
            Set(value As Decimal)
                SetProperty(_NumImporteMaxSeguroDenominacion, value, "NumImporteMaxSeguroDenominacion")
            End Set
        End Property

#End Region

    End Class

End Namespace
