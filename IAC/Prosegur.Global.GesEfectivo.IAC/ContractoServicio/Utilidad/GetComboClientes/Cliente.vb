Namespace Utilidad.GetComboClientes

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class Cliente

#Region "[Variáveis]"

        Private _OidCliente As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _CodigoAjenoCliente As String
        Private _DescripcionAjenoCliente As String
        Private _totalizadorSaldo As Boolean

#End Region

#Region "[Propriedades]"

        Public Property OidCliente() As String
            Get
                Return _OidCliente
            End Get
            Set(value As String)
                _OidCliente = value
            End Set

        End Property

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property TotalizadorSaldo() As Boolean
            Get
                Return _totalizadorSaldo
            End Get
            Set(value As Boolean)
                _totalizadorSaldo = value
            End Set
        End Property

        Public Property CodigoAjenoCliente() As String
            Get
                Return _CodigoAjenoCliente
            End Get
            Set(value As String)
                _CodigoAjenoCliente = value
            End Set
        End Property

        Public Property DescripcionAjenoCliente() As String
            Get
                Return _DescripcionAjenoCliente
            End Get
            Set(value As String)
                _DescripcionAjenoCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace