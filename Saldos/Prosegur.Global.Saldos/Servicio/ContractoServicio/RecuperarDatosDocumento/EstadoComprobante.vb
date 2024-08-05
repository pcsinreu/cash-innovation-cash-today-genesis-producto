Namespace RecuperarDatosDocumento

    Public Class EstadoComprobante
      
#Region "[VARIÁVEIS]"

        Private _Id As Integer
        Private _Descripcion As String
        Private _Codigo As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Set(value As Integer)
                _Id = value
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

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

#End Region

    End Class

End Namespace
