Namespace IngresoContado

    ''' <summary>
    ''' Classe Bulto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Bulto

#Region "Variáveis"

        Private _idBultoOri As String
        Private _CodigoDelegacion As String
        Private _codigoPuesto As String
        Private _codigoUbicacion As String
        Private _codigoUsuario As String
        Private _InicioConteo As DateTime
        Private _FinConteo As DateTime
        Private _CodigoPrecinto As String
        Private _CodigoPresintoOri As String
        Private _CodigoEstado As String
        Private _IntervencionesSupervisor As IntervencionSupervisorBultoColeccion
        Private _DeclaradosBulto As DeclaradoColeccion
        Private _DeclaradosBultoDet As DeclaradoDetColeccion
        Private _DeclaradosAgrupacion As DeclaradosAgrupacionColeccion
        Private _Parciales As ParcialColeccion
        Private _EsInterno As Boolean

#End Region

#Region "Propriedades"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoPrecinto() As String
            Get
                Return _CodigoPrecinto
            End Get
            Set(value As String)
                _CodigoPrecinto = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoPresintoOri() As String
            Get
                Return _CodigoPresintoOri
            End Get
            Set(value As String)
                _CodigoPresintoOri = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IdBultoOri() As String
            Get
                Return _idBultoOri
            End Get
            Set(value As String)
                _idBultoOri = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoPuesto() As String
            Get
                Return _codigoPuesto
            End Get
            Set(value As String)
                _codigoPuesto = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoUbicacion() As String
            Get
                Return _codigoUbicacion
            End Get
            Set(value As String)
                _codigoUbicacion = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property InicioConteo() As DateTime
            Get
                Return _InicioConteo
            End Get
            Set(value As DateTime)
                _InicioConteo = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FinConteo() As DateTime
            Get
                Return _FinConteo
            End Get
            Set(value As DateTime)
                _FinConteo = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoUsuario() As String
            Get
                Return _codigoUsuario
            End Get
            Set(value As String)
                _codigoUsuario = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DeclaradosBulto() As DeclaradoColeccion
            Get
                Return _DeclaradosBulto
            End Get
            Set(value As DeclaradoColeccion)
                _DeclaradosBulto = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DeclaradosBultoDet() As DeclaradoDetColeccion
            Get
                Return _DeclaradosBultoDet
            End Get
            Set(value As DeclaradoDetColeccion)
                _DeclaradosBultoDet = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DeclaradosAgrupacion() As DeclaradosAgrupacionColeccion
            Get
                Return _DeclaradosAgrupacion
            End Get
            Set(value As DeclaradosAgrupacionColeccion)
                _DeclaradosAgrupacion = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IntervencionesSupervisor() As IntervencionSupervisorBultoColeccion
            Get
                Return _IntervencionesSupervisor
            End Get
            Set(value As IntervencionSupervisorBultoColeccion)
                _IntervencionesSupervisor = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Parciales() As ParcialColeccion
            Get
                Return _Parciales
            End Get
            Set(value As ParcialColeccion)
                _Parciales = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EsInterno() As Boolean
            Get
                Return _EsInterno
            End Get
            Set(value As Boolean)
                _EsInterno = value
            End Set
        End Property

#End Region

    End Class

End Namespace