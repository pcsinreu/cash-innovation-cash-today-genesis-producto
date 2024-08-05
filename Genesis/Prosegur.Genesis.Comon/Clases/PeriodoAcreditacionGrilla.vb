Namespace Clases
    Public Class PeriodoAcreditacionGrilla
        Inherits BindableBase

#Region "fields"
        Private _oidPeriodo As String
        Private _banco As String
        Private _planificacion As String
        Private _deviceId As String
        Private _fyhInicio As DateTime
        Private _fyhFin As DateTime
        Private _divisa As String
        Private _tipoLimite As String
        Private _limiteConfigurado As Decimal
        Private _valorActual As Decimal
        Private _estado As String
        Private _acreditado As Boolean
        Private _codigoPais As String


#End Region

#Region "properties"
        Public Property OidPeriodo As String
            Get
                Return _oidPeriodo
            End Get
            Set(value As String)
                SetProperty(_oidPeriodo, value, "OidPeriodo")
            End Set
        End Property
        Public Property Banco As String
            Get
                Return _banco
            End Get
            Set(value As String)
                SetProperty(_banco, value, "Banco")
            End Set
        End Property

        Public Property Planificacion As String
            Get
                Return _planificacion
            End Get
            Set(value As String)
                SetProperty(_planificacion, value, "Planificacion")
            End Set
        End Property

        Public Property DeviceId As String
            Get
                Return _deviceId
            End Get
            Set(value As String)
                SetProperty(_deviceId, value, "DeviceId")
            End Set
        End Property

        Public Property FyhInicio As DateTime
            Get
                Return _fyhInicio
            End Get
            Set(value As DateTime)
                SetProperty(_fyhInicio, value, "FyhInicio")
            End Set
        End Property

        Public Property FyhFin As DateTime
            Get
                Return _fyhFin
            End Get
            Set(value As DateTime)
                SetProperty(_fyhFin, value, "FyhFin")
            End Set
        End Property

        Public Property Divisa As String
            Get
                Return _divisa
            End Get
            Set(value As String)
                SetProperty(_divisa, value, "Divisa")
            End Set
        End Property

        Public Property TipoLimite As String
            Get
                Return _tipoLimite
            End Get
            Set(value As String)
                SetProperty(_tipoLimite, value, "TipoLimite")
            End Set
        End Property

        Public Property LimiteConfigurado As Decimal
            Get
                Return _limiteConfigurado
            End Get
            Set(value As Decimal)
                SetProperty(_limiteConfigurado, value, "LimiteConfigurado")
            End Set
        End Property

        Public Property ValorActual As Decimal
            Get
                Return _valorActual
            End Get
            Set(value As Decimal)
                SetProperty(_valorActual, value, "ValorActual")
            End Set
        End Property

        Public Property Estado As String
            Get
                Return _estado
            End Get
            Set(value As String)
                SetProperty(_estado, value, "Estado")
            End Set
        End Property

        Public Property CodigoPais As String
            Get
                Return _codigoPais
            End Get
            Set(value As String)
                SetProperty(_codigoPais, value, "CodigoPais")
            End Set
        End Property

        Public Property Acreditado As Boolean
            Get
                Return _acreditado
            End Get
            Set(value As Boolean)
                SetProperty(_acreditado, value, "Estado")
            End Set
        End Property

#End Region

    End Class


End Namespace