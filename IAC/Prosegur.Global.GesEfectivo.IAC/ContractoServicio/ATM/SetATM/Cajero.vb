
Namespace ATM.SetATM

    ''' <summary>
    ''' Classe Cajero
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 07/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Cajero

#Region "[Variáveis]"

        Private _oidCajero As String
        Private _codigoCliente As String
        Private _codigoSubclietne As String
        Private _codigoCajero As String
        Private _codigoPuntoServicio As String
        Private _fyhActualizacion As DateTime

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

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property CodigoSubcliente As String
            Get
                Return _codigoSubclietne
            End Get
            Set(value As String)
                _codigoSubclietne = value
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

        Public Property FyhActualizacion() As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace