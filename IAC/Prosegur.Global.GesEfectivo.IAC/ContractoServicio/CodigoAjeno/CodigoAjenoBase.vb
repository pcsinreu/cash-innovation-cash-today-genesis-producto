Namespace CodigoAjeno

    <Serializable()> _
    Public Class CodigoAjenoBase

#Region "[VARIAVEIS]"

        Private _oidCodigoAjeno As String
        Private _codIdentificador As String
        Private _codAjeno As String
        Private _desAjeno As String
        Private _bolDefecto As Boolean
        Private _bolActivo As Boolean
        Private _bolMigrado As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidCodigoAjeno() As String
            Get
                Return _oidCodigoAjeno
            End Get
            Set(value As String)
                _oidCodigoAjeno = value
            End Set
        End Property

        Public Property CodIdentificador() As String
            Get
                Return _codIdentificador
            End Get
            Set(value As String)
                _codIdentificador = value
            End Set
        End Property

        Public Property CodAjeno() As String
            Get
                Return _codAjeno
            End Get
            Set(value As String)
                _codAjeno = value
            End Set
        End Property

        Public Property DesAjeno() As String
            Get
                Return _desAjeno
            End Get
            Set(value As String)
                _desAjeno = value
            End Set
        End Property

        Public Property BolDefecto() As Boolean
            Get
                Return _bolDefecto
            End Get
            Set(value As Boolean)
                _bolDefecto = value
            End Set
        End Property

        Public Property BolActivo() As Boolean
            Get
                Return _bolActivo
            End Get
            Set(value As Boolean)
                _bolActivo = value
            End Set
        End Property

        Public Property BolMigrado() As Boolean
            Get
                Return _bolMigrado
            End Get
            Set(value As Boolean)
                _bolMigrado = value
            End Set
        End Property

#End Region

    End Class

End Namespace
