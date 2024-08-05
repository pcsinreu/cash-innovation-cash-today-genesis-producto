Namespace IngresoContado

    <Serializable()> _
    Public Class ConteoEfectivo

#Region "[VARIÁVEIS]"

        Private _CodigoIsoDivisa As String
        Private _Unidades As String
        Private _Denominacion As String
        Private _Calidad As String
        Private _Importe As Decimal
        Private _Manual As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoIsoDivisa() As String
            Get
                Return _CodigoIsoDivisa
            End Get
            Set(value As String)
                _CodigoIsoDivisa = value
            End Set
        End Property

        Public Property Unidades() As String
            Get
                Return _Unidades
            End Get
            Set(value As String)
                _Unidades = value
            End Set
        End Property

        Public Property Denominacion() As String
            Get
                Return _Denominacion
            End Get
            Set(value As String)
                _Denominacion = value
            End Set
        End Property

        Public Property Calidad() As String
            Get
                Return _Calidad
            End Get
            Set(value As String)
                _Calidad = value
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

        Public Property Manual() As String
            Get
                Return _Manual
            End Get
            Set(value As String)
                _Manual = value
            End Set
        End Property

#End Region

    End Class
End Namespace