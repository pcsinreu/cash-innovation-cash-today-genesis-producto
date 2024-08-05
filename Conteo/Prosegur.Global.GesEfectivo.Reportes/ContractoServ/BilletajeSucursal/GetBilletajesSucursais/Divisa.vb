Namespace BilletajeSucursal.GetBilletajesSucursais

    Public Class Divisa

#Region " Variáveis "

        Private _CodigoDivisa As String = String.Empty
        Private _DeclaradoEfectivo As Decimal = 0
        Private _DeclaradoCheque As Decimal = 0
        Private _DeclaradoTicket As Decimal = 0
        Private _DeclaradoOtroValor As Decimal = 0
        Private _DividasDetalles As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaDetalheColeccion

#End Region

#Region " Propriedades "

        Public Property CodigoDivisa() As String
            Get
                Return _CodigoDivisa
            End Get
            Set(value As String)
                _CodigoDivisa = value
            End Set
        End Property

        Public Property DeclaradoEfectivo() As Decimal
            Get
                Return _DeclaradoEfectivo
            End Get
            Set(value As Decimal)
                _DeclaradoEfectivo = value
            End Set
        End Property

        Public Property DeclaradoCheque() As Decimal
            Get
                Return _DeclaradoCheque
            End Get
            Set(value As Decimal)
                _DeclaradoCheque = value
            End Set
        End Property

        Public Property DeclaradoTicket() As Decimal
            Get
                Return _DeclaradoTicket
            End Get
            Set(value As Decimal)
                _DeclaradoTicket = value
            End Set
        End Property

        Public Property DeclaradoOtroValor() As Decimal
            Get
                Return _DeclaradoOtroValor
            End Get
            Set(value As Decimal)
                _DeclaradoOtroValor = value
            End Set
        End Property

        Public Property DividasDetalles() As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaDetalheColeccion
            Get
                Return _DividasDetalles
            End Get
            Set(value As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaDetalheColeccion)
                _DividasDetalles = value
            End Set
        End Property

#End Region

    End Class

End Namespace
