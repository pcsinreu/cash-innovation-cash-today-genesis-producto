Namespace PantallaProceso

    ''' <summary>
    ''' Classe utilizada para médio pagos de um processo.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 23/03/2009 Criado
    ''' </history>    
    <Serializable()> _
    Public Class MedioPago

        Private _CodigoDivisa As String
        Private _DescripcionDivisa As String
        Private _CodigoTipoMedioPago As String
        Private _DescripcionTipoMedioPago As String
        Private _CodigoMedioPago As String
        Private _DescripcionMedioPago As String
        Private _TerminoMedioPago As TerminoMedioPagoColeccion

        Public Property CodigoDivisa() As String
            Get
                Return _CodigoDivisa
            End Get
            Set(value As String)
                _CodigoDivisa = value
            End Set
        End Property

        Public Property DescripcionDivisa() As String
            Get
                Return _DescripcionDivisa
            End Get
            Set(value As String)
                _DescripcionDivisa = value
            End Set
        End Property

        Public Property CodigoTipoMedioPago() As String
            Get
                Return _CodigoTipoMedioPago
            End Get
            Set(value As String)
                _CodigoTipoMedioPago = value
            End Set
        End Property

        Public Property DescripcionTipoMedioPago() As String
            Get
                Return _DescripcionTipoMedioPago
            End Get
            Set(value As String)
                _DescripcionTipoMedioPago = value
            End Set
        End Property

        Public Property CodigoMedioPago() As String
            Get
                Return _CodigoMedioPago
            End Get
            Set(value As String)
                _CodigoMedioPago = value
            End Set
        End Property

        Public Property DescripcionMedioPago() As String
            Get
                Return _DescripcionMedioPago
            End Get
            Set(value As String)
                _DescripcionMedioPago = value
            End Set
        End Property

        Public Property TerminosMedioPago() As TerminoMedioPagoColeccion
            Get
                Return _TerminoMedioPago
            End Get
            Set(value As TerminoMedioPagoColeccion)
                _TerminoMedioPago = value
            End Set
        End Property


    End Class

End Namespace