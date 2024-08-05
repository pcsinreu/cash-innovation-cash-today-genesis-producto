Namespace IngresoContado

    ''' <summary>
    ''' Classe MedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class MedioPago

        Private _TipoCodigo As String
        Private _TipoDescripcion As String
        Private _DivisaCodigoISO As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Importe As Decimal
        Private _Unidades As Integer
        Private _Secuencia As Integer
        Private _Terminos As TerminoMedioPagoColeccion

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
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

        Public Property DivisaCodigoISO() As String
            Get
                Return _DivisaCodigoISO
            End Get
            Set(value As String)
                _DivisaCodigoISO = value
            End Set
        End Property

        Public Property Importe() As Decimal
            Get
                Return _Importe
            End Get
            Set(value As Decimal)
                _Importe = value
            End Set
        End Property

        Public Property Secuencia() As Integer
            Get
                Return _Secuencia
            End Get
            Set(value As Integer)
                _Secuencia = value
            End Set
        End Property

        Public Property Terminos() As TerminoMedioPagoColeccion
            Get
                Return _Terminos
            End Get
            Set(value As TerminoMedioPagoColeccion)
                _Terminos = value
            End Set
        End Property

        Public Property TipoCodigo() As String
            Get
                Return _TipoCodigo
            End Get
            Set(value As String)
                _TipoCodigo = value
            End Set
        End Property

        Public Property TipoDescripcion() As String
            Get
                Return _TipoDescripcion
            End Get
            Set(value As String)
                _TipoDescripcion = value
            End Set
        End Property

        Public Property Unidades() As Integer
            Get
                Return _Unidades
            End Get
            Set(value As Integer)
                _Unidades = value
            End Set
        End Property

    End Class

End Namespace