Namespace NuevoSalidas.Recibo.TransportePeru
    Public Class EfectivoBultoDetalle
        Public Property BolBillete As Boolean
        Public Property ValorFacial As Decimal
        Public Property DescripcionDenominacion As String
        Public Property Cantidad As Int32
        Public ReadOnly Property Total As Decimal
            Get
                If Me.Cantidad > 0 Then
                    Return Me.Cantidad * Me.ValorFacial
                End If

                Return 0
            End Get
        End Property

    End Class

End Namespace

