Namespace GetATMByRegistrarTira

    <Serializable()> _
    Public Class CajeroXMorfologia

#Region "[Variáveis]"

        Private _oidMorfologia As String
        Private _fecInicio As Date
        Private _bolVigente As Boolean
        Private _necModalidadeRecogida As Integer

#End Region


#Region "[Propriedades]"

        Public Property OidMorfologia() As String
            Get
                Return _oidMorfologia
            End Get
            Set(value As String)
                _oidMorfologia = value
            End Set
        End Property

        Public Property FecInicio() As Date
            Get
                Return _fecInicio
            End Get
            Set(value As Date)
                _fecInicio = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property NecModalidadeRecogida() As Integer
            Get
                Return _necModalidadeRecogida
            End Get
            Set(value As Integer)
                _necModalidadeRecogida = value
            End Set
        End Property

#End Region

    End Class

End Namespace