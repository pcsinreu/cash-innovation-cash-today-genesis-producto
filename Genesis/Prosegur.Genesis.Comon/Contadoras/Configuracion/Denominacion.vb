
Namespace Contadoras.Configuracion

    <Serializable()> _
    Public Class Denominacion

        Private _Origen As String
        Private _Destino As String

        Public Property Origen() As String
            Get
                Return _Origen
            End Get
            Set(value As String)
                _Origen = value
            End Set
        End Property

        Public Property Destino() As String
            Get
                Return _Destino
            End Get
            Set(value As String)
                _Destino = value
            End Set
        End Property

    End Class

End Namespace