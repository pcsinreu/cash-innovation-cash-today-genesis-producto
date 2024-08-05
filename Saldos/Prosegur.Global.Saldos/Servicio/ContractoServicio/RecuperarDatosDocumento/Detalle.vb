Namespace RecuperarDatosDocumento

    Public Class Detalle

#Region "[VARIÁVEIS]"

        Private _Cantidad As Integer
        Private _Importe As Decimal
        Private _Especie As Especie

#End Region

#Region "[PROPRIEDADES]"

        Public Property Cantidad() As Integer
            Get
                Cantidad = _Cantidad
            End Get
            Set(Value As Integer)
                _Cantidad = Value
            End Set
        End Property

        Public Property Importe() As Decimal
            Get
                Importe = _Importe
            End Get
            Set(Value As Decimal)
                _Importe = Value
            End Set
        End Property

        Public Property Especie() As Especie
            Get
                If _Especie Is Nothing Then
                    _Especie = New Especie()
                End If
                Especie = _Especie
            End Get
            Set(Value As Especie)
                _Especie = Value
            End Set
        End Property

#End Region

    End Class

End Namespace