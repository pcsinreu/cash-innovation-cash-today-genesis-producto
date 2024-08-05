Namespace ReciboF22Respaldo.GetReciboF22Respaldo

    Public Class MedioPagoDeclarado

#Region " Atributos "

        Private _CodigoMedioPago As String = String.Empty
        Private _DescripcionMedioPago As String = String.Empty
        Private _ValorDeclaradoF22 As Decimal = Decimal.Zero
        Private _ValorDeclaradoSobres As Decimal = Decimal.Zero
        Private _ValorRecontadoSobres As Decimal = Decimal.Zero
        Private _DiferenciaRecontadoDeclarado As Decimal = Decimal.Zero
        Private _CodigoDivisa As String = String.Empty

#End Region

#Region " Propriedades "

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

        Public Property ValorDeclaradoF22() As Decimal
            Get
                Return _ValorDeclaradoF22
            End Get
            Set(value As Decimal)
                _ValorDeclaradoF22 = value
            End Set
        End Property

        Public Property ValorDeclaradoSobres() As Decimal
            Get
                Return _ValorDeclaradoSobres
            End Get
            Set(value As Decimal)
                _ValorDeclaradoSobres = value
            End Set
        End Property

        Public Property ValorRecontadoSobres() As Decimal
            Get
                Return _ValorRecontadoSobres
            End Get
            Set(value As Decimal)
                _ValorRecontadoSobres = value
            End Set
        End Property

        Public Property DiferenciaRecontadoDeclarado() As Decimal
            Get
                Return _DiferenciaRecontadoDeclarado
            End Get
            Set(value As Decimal)
                _DiferenciaRecontadoDeclarado = value
            End Set
        End Property

        Public Property CodigoDivisa() As String
            Get
                Return _CodigoDivisa
            End Get
            Set(value As String)
                _CodigoDivisa = value
            End Set
        End Property

#End Region

    End Class

End Namespace
