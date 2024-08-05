<Serializable()> _
Public Class Campo

#Region "[VARIÁVEIS]"

    Private _Nombre As String
    Private _IdValor As Integer
    Private _Valor As Object
    Private _Id As Integer
    Private _Clase As String
    Private _Coleccion As String
    Private _Etiqueta As String
    Private _Tipo As String
    Private _Elemento As Elemento

#End Region

#Region "[PROPRIEDADES]"

    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(Value As String)
            _Tipo = Value
        End Set
    End Property

    Public Property Etiqueta() As String
        Get
            Return _Etiqueta
        End Get
        Set(Value As String)
            _Etiqueta = Value
        End Set
    End Property

    Public Property Coleccion() As String
        Get
            Return _Coleccion
        End Get
        Set(Value As String)
            _Coleccion = Value
        End Set
    End Property

    Public Property Clase() As String
        Get
            Return _Clase
        End Get
        Set(Value As String)
            _Clase = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

    Public Property Valor() As Object
        Get
            If IsReference(_Valor) Then
                Return _Valor
            Else
                Return _Valor
            End If
        End Get
        Set(Value As Object)
            If IsReference(Value) AndAlso Not TypeOf Value Is String Then
                _Valor = Value
            Else
                _Valor = Value
            End If
        End Set
    End Property

    Public Property IdValor() As Integer
        Get
            Return _IdValor
        End Get
        Set(Value As Integer)
            _IdValor = Value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(Value As String)
            _Nombre = Value
        End Set
    End Property

    Public Property Elemento() As Elemento
        Get
            Return _Elemento
        End Get
        Set(value As Elemento)
            _Elemento = value
        End Set
    End Property

#End Region

End Class