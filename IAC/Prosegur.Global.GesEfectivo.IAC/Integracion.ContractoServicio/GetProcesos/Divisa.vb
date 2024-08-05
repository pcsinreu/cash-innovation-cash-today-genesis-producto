Namespace GetProcesos

    <Serializable()> _
    Public Class Divisa

#Region "[VARIÁVEIS]"

        Private _codigoIso As String
        Private _descripcion As String
        Private _vigente As Boolean
        Private _denominaciones As DenominacionColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoIso() As String
            Get
                Return _codigoIso
            End Get
            Set(value As String)
                _codigoIso = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property Denominaciones() As DenominacionColeccion
            Get
                Return _denominaciones
            End Get
            Set(value As DenominacionColeccion)
                _denominaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace
