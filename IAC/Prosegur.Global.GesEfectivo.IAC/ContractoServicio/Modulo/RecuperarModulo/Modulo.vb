Namespace Modulo

    ''' <summary>
    ''' Classe Modulo
    ''' </summary>
    ''' <remarks></remarks>

    <Serializable()> _
    Public Class Modulo

#Region "[Variáveis]"

        Private _oidModulo As String
        Private _codModulo As String
        Private _desModulo As String
        Private _codCliente As String
        Private _bolActivo As Nullable(Of Boolean)

        Public Property OidModulo() As String
            Get
                Return _oidModulo
            End Get
            Set(value As String)
                _oidModulo = value
            End Set
        End Property
        Public Property CodModulo() As String
            Get
                Return _codModulo
            End Get
            Set(value As String)
                _codModulo = value
            End Set
        End Property

        Public Property DesModulo() As String
            Get
                Return _desModulo
            End Get
            Set(value As String)
                _desModulo = value
            End Set
        End Property

        Public Property CodCliente() As String
            Get
                Return _codCliente
            End Get
            Set(value As String)
                _codCliente = value
            End Set
        End Property

        Public Property BolActivo() As Nullable(Of Boolean)
            Get
                Return _bolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _bolActivo = value
            End Set
        End Property


#End Region

    End Class

End Namespace