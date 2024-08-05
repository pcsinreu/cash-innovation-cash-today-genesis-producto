<Serializable()> _
Public Class Esel

#Region "[VARIAVEIS]"

    Private _Id As Integer
    Private _Descripcion As String

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

    Public Property Id() As Integer
        Get
            Id = _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property
#End Region

End Class



