Namespace Utilidad.GetComboTiposPeriodo
    <Serializable()>
    Public Class TipoDePeriodo
        Private _Oid As String
        Private _Descripcion As String

        Public Property Oid() As String
            Get
                Return _Oid
            End Get
            Set(value As String)
                _Oid = value
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
