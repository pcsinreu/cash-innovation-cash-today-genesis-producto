Namespace Contadoras.Configuracion.Interno

    Public Class ContadoraInterno

        Private _Divisas As New Dictionary(Of String, DivisaInterno)

        Public Property Divisas() As Dictionary(Of String, DivisaInterno)
            Get
                Return _Divisas
            End Get
            Set(value As Dictionary(Of String, DivisaInterno))
                _Divisas = value
            End Set
        End Property

    End Class

End Namespace