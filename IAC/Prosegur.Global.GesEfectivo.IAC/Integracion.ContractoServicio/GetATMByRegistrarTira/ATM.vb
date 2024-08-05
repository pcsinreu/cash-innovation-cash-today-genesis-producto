Namespace GetATMByRegistrarTira

    <Serializable()> _
    Public Class ATM
        Inherits Comum.ATM

#Region "[Variáveis]"

        Private _oidCajero As String
        Private _codigoCajero As String
        Private _codigoRed As String
        Private _codigoModeloCajero As String
        Private _oidGrupo As String
        Private _bolRegistroTira As Boolean
        Private _bolVigente As Boolean
        Private _fyhActualizacion As DateTime
        Private _cajeroXMorfologias As List(Of CajeroXMorfologia)

#End Region

#Region "[Propriedades]"

        Public Property OidCajero() As String
            Get
                Return _oidCajero
            End Get
            Set(value As String)
                _oidCajero = value
            End Set
        End Property

        Public Property CodigoCajero() As String
            Get
                Return _codigoCajero
            End Get
            Set(value As String)
                _codigoCajero = value
            End Set
        End Property

        Public Property CodigoRed() As String
            Get
                Return _codigoRed
            End Get
            Set(value As String)
                _codigoRed = value
            End Set
        End Property

        Public Property CodigoModeloCajero() As String
            Get
                Return _codigoModeloCajero
            End Get
            Set(value As String)
                _codigoModeloCajero = value
            End Set
        End Property

        Public Property OidGrupo() As String
            Get
                Return _oidGrupo
            End Get
            Set(value As String)
                _oidGrupo = value
            End Set
        End Property

        Public Property BolRegistroTira() As Boolean
            Get
                Return _bolRegistroTira
            End Get
            Set(value As Boolean)
                _bolRegistroTira = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property FyhActualizacion() As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

        Public Property CajeroXMorfologias() As List(Of CajeroXMorfologia)
            Get
                Return _cajeroXMorfologias
            End Get
            Set(value As List(Of CajeroXMorfologia))
                _cajeroXMorfologias = value
            End Set
        End Property

#End Region

    End Class

End Namespace