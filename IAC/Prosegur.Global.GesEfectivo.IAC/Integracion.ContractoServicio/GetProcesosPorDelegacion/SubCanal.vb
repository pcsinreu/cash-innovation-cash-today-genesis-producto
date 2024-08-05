Namespace GetProcesosPorDelegacion

    <Serializable()> _
    Public Class SubCanal

#Region "[VARIÁVEIS]"

        Private _codigoCanal As String
        Private _descripcionCanal As String
        Private _codigoSubCanal As String
        Private _descripcionSubCanal As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoCanal() As String
            Get
                Return _codigoCanal
            End Get
            Set(value As String)
                _codigoCanal = value
            End Set
        End Property
        Public Property DescripcionCanal() As String
            Get
                Return _descripcionCanal
            End Get
            Set(value As String)
                _descripcionCanal = value
            End Set
        End Property
        Public Property CodigoSubCanal() As String
            Get
                Return _codigoSubCanal
            End Get
            Set(value As String)
                _codigoSubCanal = value
            End Set
        End Property
        Public Property DescripcionSubCanal() As String
            Get
                Return _descripcionSubCanal
            End Get
            Set(value As String)
                _descripcionSubCanal = value
            End Set
        End Property

#End Region

    End Class

End Namespace
