Imports System.Xml.Serialization
Imports System.Xml

<Serializable()> _
Public Class Aplicacion

    Private _codigoAplicacion As String
    Private _descripcionAplicacion As String
    Private _codigoPermiso As String

    Public Property CodigoAplicacion As String
        Get
            Return _codigoAplicacion
        End Get
        Set(value As String)
            _codigoAplicacion = value
        End Set
    End Property

    Public Property DescripcionAplicacion As String
        Get
            Return _descripcionAplicacion
        End Get
        Set(value As String)
            _descripcionAplicacion = value
        End Set
    End Property

    Public Property CodigoPermiso As String
        Get
            Return _codigoPermiso
        End Get
        Set(value As String)
            _codigoPermiso = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(codigoAplicacion As String, descripcionAplicacion As String, codigoPermiso As String)
        _codigoAplicacion = codigoAplicacion
        _descripcionAplicacion = descripcionAplicacion
        _codigoPermiso = codigoPermiso
    End Sub

End Class
