
Namespace GetATMs

    ''' <summary>
    ''' Classe ATM
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 07/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class ATM

#Region "[Variáveis]"

        Private _oidCajero As String
        Private _codigoCajero As String
        Private _codigoRed As String
        Private _descripcionRed As String
        Private _codigoModeloCajero As String
        Private _descripcionModeloCajero As String
        Private _oidGrupo As String
        Private _codigoGrupo As String
        Private _descripcionGrupo As String
        Private _bolRegistroTira As Boolean
        Private _codigoCliente As String
        Private _descripcionCliente As String
        Private _codigoSubcliente As String
        Private _descripcionSubcliente As String
        Private _codigoPuntoServicio As String
        Private _descripcionPuntoServicio As String
        Private _bolVigente As Boolean
        Private _fyhActualizacion As DateTime
        Private _morfologia As Morfologia

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

        Public Property DescripcionRed() As String
            Get
                Return _descripcionRed
            End Get
            Set(value As String)
                _descripcionRed = value
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

        Public Property DescripcionModeloCajero() As String
            Get
                Return _descripcionModeloCajero
            End Get
            Set(value As String)
                _descripcionModeloCajero = value
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

        Public Property CodigoGrupo() As String
            Get
                Return _codigoGrupo
            End Get
            Set(value As String)
                _codigoGrupo = value
            End Set
        End Property

        Public Property DescripcionGrupo() As String
            Get
                Return _descripcionGrupo
            End Get
            Set(value As String)
                _descripcionGrupo = value
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

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property DescripcionCliente() As String
            Get
                Return _descripcionCliente
            End Get
            Set(value As String)
                _descripcionCliente = value
            End Set
        End Property

        Public Property CodigoSubcliente() As String
            Get
                Return _codigoSubcliente
            End Get
            Set(value As String)
                _codigoSubcliente = value
            End Set
        End Property

        Public Property DescripcionSubcliente() As String
            Get
                Return _descripcionSubcliente
            End Get
            Set(value As String)
                _descripcionSubcliente = value
            End Set
        End Property

        Public Property CodigoPuntoServicio() As String
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As String)
                _codigoPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionPuntoServicio() As String
            Get
                Return _descripcionPuntoServicio
            End Get
            Set(value As String)
                _descripcionPuntoServicio = value
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

        Public Property Morfologia() As Morfologia
            Get
                Return _morfologia
            End Get
            Set(value As Morfologia)
                _morfologia = value
            End Set
        End Property

#End Region

    End Class

End Namespace