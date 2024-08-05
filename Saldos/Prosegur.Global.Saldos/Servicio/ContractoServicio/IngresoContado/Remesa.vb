Namespace IngresoContado

    ''' <summary>
    ''' Classe Remesa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama]  03/08/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class Remesa

#Region "[VARIÁVEIS]"

        Private _IdRemesaOri As String
        Private _CodigoDelegacion As String
        Private _CodigoPuesto As String
        Private _CodigoUbicacion As String
        Private _CodigoUsuario As String
        Private _CodigoEstado As String
        Private _EsInterno As Boolean
        Private _IdentificadorDocumento As String
        Private _IntervencionesSupervisor As IntervencionSupervisorRemesaColeccion
        Private _DeclaradosRemesa As DeclaradoColeccion
        Private _DeclaradosRemesaDet As DeclaradoDetColeccion
        Private _DeclaradosAgrupacion As DeclaradosAgrupacionColeccion
        Private _Bultos As BultoColeccion

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IdentificadorDocumento() As String
            Get
                Return _IdentificadorDocumento
            End Get
            Set(value As String)
                _IdentificadorDocumento = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IdRemesaOri() As String
            Get
                Return _IdRemesaOri
            End Get
            Set(value As String)
                _IdRemesaOri = value
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
                Return _CodigoPuesto
            End Get
            Set(value As String)
                _CodigoPuesto = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
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
        Public Property CodigoUbicacion() As String
            Get
                Return _CodigoUbicacion
            End Get
            Set(value As String)
                _CodigoUbicacion = value
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
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoEstado() As String
            Get
                Return _CodigoEstado
            End Get
            Set(value As String)
                _CodigoEstado = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EsInterno() As String
            Get
                Return _EsInterno
            End Get
            Set(value As String)
                _EsInterno = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DeclaradosRemesa() As DeclaradoColeccion
            Get
                Return _DeclaradosRemesa
            End Get
            Set(value As DeclaradoColeccion)
                _DeclaradosRemesa = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DeclaradosRemesaDet() As DeclaradoDetColeccion
            Get
                Return _DeclaradosRemesaDet
            End Get
            Set(value As DeclaradoDetColeccion)
                _DeclaradosRemesaDet = value
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
        Public Property Bultos() As BultoColeccion
            Get
                Return _Bultos
            End Get
            Set(value As BultoColeccion)
                _Bultos = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IntervencionesSupervisor() As IntervencionSupervisorRemesaColeccion
            Get
                Return _IntervencionesSupervisor
            End Get
            Set(value As IntervencionSupervisorRemesaColeccion)
                _IntervencionesSupervisor = value
            End Set
        End Property

#End Region

    End Class

End Namespace