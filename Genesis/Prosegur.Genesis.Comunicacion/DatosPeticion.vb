Public Class DatosPeticion

    Private _IdentificadorOperacion As Metodo
    Private _IdentificadorServicio As Servicios
    Private _DatosPeticion As Object

    Public Property IdentificadorServicio() As Servicios
        Get
            Return _IdentificadorServicio
        End Get
        Set(value As Servicios)
            _IdentificadorServicio = value
        End Set
    End Property

    Public Property IdentificadorOperacion() As Metodo
        Get
            Return _IdentificadorOperacion
        End Get
        Set(value As Metodo)
            _IdentificadorOperacion = value
        End Set
    End Property

    Public Property DatosPeticion() As Object
        Get
            Return _DatosPeticion
        End Get
        Set(value As Object)
            _DatosPeticion = value
        End Set
    End Property

End Class
