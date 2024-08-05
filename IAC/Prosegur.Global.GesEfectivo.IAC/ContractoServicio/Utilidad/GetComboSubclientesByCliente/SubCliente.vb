Namespace Utilidad.GetComboSubclientesByCliente

    <Serializable()> _
    Public Class SubCliente

#Region "[VARIÁVEIS]"

        Private _oidSubCliente As String
        Private _codigo As String
        Private _descripcion As String
        Private _CodigoAjenoSubCliente As String
        Private _DescripcionAjenoSubCliente As String
        Private _totalizadorSaldo As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidSubCliente() As String
            Get
                Return _oidSubCliente
            End Get
            Set(value As String)
                _oidSubCliente = value
            End Set
        End Property

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
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

        Public Property TotalizadorSaldo() As Boolean
            Get
                Return _totalizadorSaldo
            End Get
            Set(value As Boolean)
                _totalizadorSaldo = value
            End Set
        End Property

        Public Property CodigoAjenoSubCliente() As String
            Get
                Return _CodigoAjenoSubCliente
            End Get
            Set(value As String)
                _CodigoAjenoSubCliente = value
            End Set
        End Property

        Public Property DescripcionAjenoSubCliente() As String
            Get
                Return _DescripcionAjenoSubCliente
            End Get
            Set(value As String)
                _DescripcionAjenoSubCliente = value
            End Set
        End Property

#End Region

    End Class
End Namespace