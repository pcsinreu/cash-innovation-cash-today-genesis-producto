Namespace Login.ObtenerPaises

    <Serializable()>
    Public Class Pais

#Region "Variables"
        Private _oidPais As String
        Private _codigoPais As String
        Private _descripcionPais As String
        Private _esActivo As Boolean
#End Region

#Region "Propriedades"
        Public Property Identificador() As String
            Get
                Return _oidPais
            End Get
            Set(ByVal value As String)
                _oidPais = value
            End Set
        End Property
        Public Property CodigoPais() As String
            Get
                Return _codigoPais
            End Get
            Set(ByVal value As String)
                _codigoPais = value
            End Set
        End Property
        Public Property DescripcionPais() As String
            Get
                Return _descripcionPais
            End Get
            Set(ByVal value As String)
                _descripcionPais = value
            End Set
        End Property
        Public Property EsActivo() As Boolean
            Get
                Return _esActivo
            End Get
            Set(ByVal value As Boolean)
                _esActivo = value
            End Set
        End Property
#End Region


    End Class
End Namespace