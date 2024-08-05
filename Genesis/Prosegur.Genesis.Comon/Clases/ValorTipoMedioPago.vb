Namespace Clases


    ' ***********************************************************************
    '  Modulo:  ValorMedioPago.vb
    '  Descripción: Clase definición ValorMedioPago
    ' ***********************************************************************

    <Serializable()>
    Public Class ValorTipoMedioPago
        Inherits Valor

#Region "Variaveis"

        Private _TipoMedioPago As Enumeradores.TipoMedioPago
        Private _Cantidad As Single

#End Region

#Region "Propriedades"

        Public Property TipoMedioPago As Enumeradores.TipoMedioPago
            Get
                Return _TipoMedioPago
            End Get
            Set(value As Enumeradores.TipoMedioPago)
                SetProperty(_TipoMedioPago, value, "TipoMedioPago")
            End Set
        End Property
        Public Property Cantidad As Single
            Get
                Return _Cantidad
            End Get
            Set(value As Single)
                SetProperty(_Cantidad, value, "Cantidad")
            End Set
        End Property

#End Region


    End Class

End Namespace
