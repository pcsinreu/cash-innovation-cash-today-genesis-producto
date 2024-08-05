Namespace IngresoContado

    ''' <summary>
    ''' Classe IntervencionSupervisorGenerico
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class IntervencionSupervisorGenerico

#Region "Variáveis"

        Private _CodSupervisor As String
        Private _CodContador As String
        Private _DesComentario As String
        Private _FyhInicioIntervencion As DateTime
        Private _FyhFinIntervencion As DateTime
        Private _CodMotivo As String
        Private _DesMotivo As String

#End Region

#Region "Propriedades"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodContador() As String
            Get
                Return _CodContador
            End Get
            Set(value As String)
                _CodContador = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodMotivo() As String
            Get
                Return _CodMotivo
            End Get
            Set(value As String)
                _CodMotivo = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodSupervisor() As String
            Get
                Return _CodSupervisor
            End Get
            Set(value As String)
                _CodSupervisor = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DesComentario() As String
            Get
                Return _DesComentario
            End Get
            Set(value As String)
                _DesComentario = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DesMotivo() As String
            Get
                Return _DesMotivo
            End Get
            Set(value As String)
                _DesMotivo = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FyhFinIntervencion() As DateTime
            Get
                Return _FyhFinIntervencion
            End Get
            Set(value As DateTime)
                _FyhFinIntervencion = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FyhInicioIntervencion() As DateTime
            Get
                Return _FyhInicioIntervencion
            End Get
            Set(value As DateTime)
                _FyhInicioIntervencion = value
            End Set
        End Property

#End Region

    End Class

End Namespace