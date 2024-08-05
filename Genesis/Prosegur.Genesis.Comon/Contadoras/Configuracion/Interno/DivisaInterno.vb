Namespace Contadoras.Configuracion.Interno

    Public Class DivisaInterno

        Private _Denominaciones As New Dictionary(Of String, String)

        Public Property Denominaciones() As Dictionary(Of String, String)
            Get
                Return _Denominaciones
            End Get
            Set(value As Dictionary(Of String, String))
                _Denominaciones = value
            End Set
        End Property

    End Class

End Namespace