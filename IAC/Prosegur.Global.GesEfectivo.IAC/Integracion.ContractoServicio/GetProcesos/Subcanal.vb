Namespace GetProcesos

    <Serializable()> _
    Public Class Subcanal

#Region "[VARIÁVEIS]"

        Private _subcanal As String
        Private _medioPago As MedioPagoProcesoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Subcanal() As String
            Get
                Return _subcanal
            End Get
            Set(value As String)
                _subcanal = value
            End Set
        End Property

        Public Property MedioPago() As MedioPagoProcesoColeccion
            Get
                Return _medioPago
            End Get
            Set(value As MedioPagoProcesoColeccion)
                _medioPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace
