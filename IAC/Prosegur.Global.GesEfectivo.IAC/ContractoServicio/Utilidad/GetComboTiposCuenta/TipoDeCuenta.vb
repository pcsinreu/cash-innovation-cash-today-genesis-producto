
Namespace Utilidad.GetComboTiposCuenta
    <Serializable()>
    Public Class TipoDeCuenta
        Private _Indice As Integer
        Private _Descripcion As String

        Public Property Indice() As Integer
            Get
                Return _Indice
            End Get
            Set(value As Integer)
                _Indice = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                If _Descripcion <> value Then
                    _Descripcion = value
                End If
            End Set
        End Property
    End Class
End Namespace
