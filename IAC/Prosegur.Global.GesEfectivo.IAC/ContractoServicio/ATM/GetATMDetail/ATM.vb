
Namespace ATM.GetATMDetail

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

        Private _codCajero As String
        Private _oidRede As String
        Private _codigoRed As String
        Private _descripcionRed As String
        Private _oidModeloCajero As String
        Private _codigoModeloCajero As String
        Private _descripcionModeloCajero As String
        Private _oidGrupo As String
        Private _codigoGrupo As String
        Private _descripcionGrupo As String
        Private _bolRegistroTira As Boolean
        Private _fyhActualizacion As DateTime
        Private _morfologias As List(Of Morfologia)
        Private _proceso As List(Of Proceso)

#End Region

#Region "[Propriedades]"

        Public Property CodCajero As String
            Get
                Return _codCajero
            End Get
            Set(value As String)
                _codCajero = value
            End Set
        End Property

        Public Property OidRede As String
            Get
                Return _oidRede
            End Get
            Set(value As String)
                _oidRede = value
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

        Public Property OidModeloCajero As String
            Get
                Return _oidModeloCajero
            End Get
            Set(value As String)
                _oidModeloCajero = value
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

        Public Property OidGrupo As String
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

        Public Property FyhActualizacion() As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

        Public Property Morfologias() As List(Of Morfologia)
            Get
                Return _morfologias
            End Get
            Set(value As List(Of Morfologia))
                _morfologias = value
            End Set
        End Property

        Public Property Procesos() As List(Of Proceso)
            Get
                Return _proceso
            End Get
            Set(value As List(Of Proceso))
                _proceso = value
            End Set
        End Property

#End Region

    End Class

End Namespace