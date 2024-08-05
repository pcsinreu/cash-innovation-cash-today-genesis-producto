Namespace Agrupacion.GetAgrupacionesDetail

    ''' <summary>
    ''' Classe Divisa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class Divisa

        Private _CodigoIso As String
        Private _Descripcion As String
        Private _TieneEfectivo As Boolean
        Private _TiposMedioPago As TipoMedioPagoColeccion

        Public Property CodigoIso() As String
            Get
                Return _CodigoIso
            End Get
            Set(value As String)
                _CodigoIso = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property TieneEfectivo() As Boolean
            Get
                Return _TieneEfectivo
            End Get
            Set(value As Boolean)
                _TieneEfectivo = value
            End Set
        End Property

        Public Property TiposMedioPago() As TipoMedioPagoColeccion
            Get
                Return _TiposMedioPago
            End Get
            Set(value As TipoMedioPagoColeccion)
                _TiposMedioPago = value
            End Set
        End Property

    End Class

End Namespace