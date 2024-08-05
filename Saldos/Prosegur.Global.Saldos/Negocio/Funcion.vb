<Serializable()> _
Public Class Funcion

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _URL As String
    Private _Codigo As Integer

#End Region

#Region "[PROPRIEDADES]"

    Public Property Descripcion() As String
        Get
            Descripcion = _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
        End Set
    End Property

    Public Property URL() As String
        Get
            URL = _URL
        End Get
        Set(Value As String)
            _URL = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Id = _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

    Public Property Codigo() As Integer
        Get
            Return _Codigo
        End Get
        Set(Value As Integer)
            _Codigo = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Function Realizar(ByRef conexion As Object) As Short

    End Function

#End Region

End Class