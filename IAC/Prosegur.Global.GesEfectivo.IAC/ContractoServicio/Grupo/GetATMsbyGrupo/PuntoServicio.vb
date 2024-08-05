
Namespace Grupo.GetATMsbyGrupo

    ''' <summary>
    ''' Classe PuntoServicio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 13/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class PuntoServicio

#Region "[Variáveis]"

        Private _oidPuntoServicio As String
        Private _codigoPuntoServicio As String
        Private _descripcionPuntoServicio As String
        Private _oidCajero As String
        Private _codigoCajero As String
        Private _fyhActualizacion As DateTime

#End Region

#Region "[Propriedades]"

        Public Property OidPuntoServicio() As String
            Get
                Return _oidPuntoServicio
            End Get
            Set(value As String)
                _oidPuntoServicio = value
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

        Public Property FyhActualizacion As DateTime
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