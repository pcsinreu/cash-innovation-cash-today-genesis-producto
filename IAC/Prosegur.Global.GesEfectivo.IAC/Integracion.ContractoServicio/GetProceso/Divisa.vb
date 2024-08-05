Namespace GetProceso

    <Serializable()> _
    Public Class Divisa

#Region "Variáveis"

        Private _codigoISO As String
        Private _descripcion As String
        Private _denominaciones As DenominacionColeccion

#End Region

#Region "Propriedades"

        Public Property CodigoISO() As String
            Get
                Return _codigoISO
            End Get
            Set(value As String)
                _codigoISO = value
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
