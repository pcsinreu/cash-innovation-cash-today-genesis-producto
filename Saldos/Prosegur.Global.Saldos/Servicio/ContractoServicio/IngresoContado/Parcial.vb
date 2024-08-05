Namespace IngresoContado

    ''' <summary>
    ''' Classe Parcial
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Parcial

#Region "Variáveis"

        Private _IdParcial As String
        Private _CodigoPuesto As String
        Private _CodigoUbicacion As String
        Private _CodigoUsuario As String
        Private _FinConteo As DateTime
        Private _IntervencionesSupervisor As IntervencionSupervisorParcialColeccion
        Private _DeclaradosParcial As DeclaradoColeccion
        Private _DeclaradosParcialDet As DeclaradoDetColeccion
        Private _DeclaradosAgrupacion As DeclaradosAgrupacionColeccion
        Private _Efectivos As EfectivoColeccion
        Private _MediosPago As MedioPagoColeccion
        Private _BilletesFalso As BilleteFalsoColeccion
        Private _MonedasFalsa As MonedaFalsaColeccion
        Private _InfosAdicionalCliente As InfoAdicionalClienteColeccion

#End Region

#Region "Propriedades"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IdParcial() As String
            Get
                Return _IdParcial
            End Get
            Set(value As String)
                _IdParcial = value
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
        Public Property DeclaradosParcial() As DeclaradoColeccion
            Get
                Return _DeclaradosParcial
            End Get
            Set(value As DeclaradoColeccion)
                _DeclaradosParcial = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DeclaradosParcialDet() As DeclaradoDetColeccion
            Get
                Return _DeclaradosParcialDet
            End Get
            Set(value As DeclaradoDetColeccion)
                _DeclaradosParcialDet = value
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
        Public Property Efectivos() As EfectivoColeccion
            Get
                Return _Efectivos
            End Get
            Set(value As EfectivoColeccion)
                _Efectivos = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BilletesFalso() As BilleteFalsoColeccion
            Get
                Return _BilletesFalso
            End Get
            Set(value As BilleteFalsoColeccion)
                _BilletesFalso = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IntervencionesSupervisor() As IntervencionSupervisorParcialColeccion
            Get
                Return _IntervencionesSupervisor
            End Get
            Set(value As IntervencionSupervisorParcialColeccion)
                _IntervencionesSupervisor = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property MediosPago() As MedioPagoColeccion
            Get
                Return _MediosPago
            End Get
            Set(value As MedioPagoColeccion)
                _MediosPago = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property MonedasFalsa() As MonedaFalsaColeccion
            Get
                Return _MonedasFalsa
            End Get
            Set(value As MonedaFalsaColeccion)
                _MonedasFalsa = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property InfosAdicionalCliente() As InfoAdicionalClienteColeccion
            Get
                Return _InfosAdicionalCliente
            End Get
            Set(value As InfoAdicionalClienteColeccion)
                _InfosAdicionalCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace