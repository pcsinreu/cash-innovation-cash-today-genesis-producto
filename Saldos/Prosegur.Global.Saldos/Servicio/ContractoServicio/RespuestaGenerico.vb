Imports System.Xml.Serialization
Imports System.Xml

<Serializable()> _
Public Class RespuestaGenerico

#Region " Variáveis "

    Private _CodigoError As String
    Private _MensajeError As String
    Private _MensajeErrorDescriptiva As String = String.Empty
    Private _Tiempo As String

#End Region

#Region " Propriedades "

    Public Property CodigoError() As Integer
        Get
            Return _CodigoError
        End Get
        Set(value As Integer)
            _CodigoError = value
        End Set
    End Property

    Public Property MensajeError() As String
        Get
            Return _MensajeError
        End Get
        Set(value As String)
            _MensajeError = value
        End Set
    End Property

    Public Property Tiempo() As String
        Get
            Return _Tiempo
        End Get
        Set(value As String)
            _Tiempo = value
        End Set
    End Property

    Public Property MensajeErrorDescriptiva() As String
        Get
            Return _MensajeErrorDescriptiva
        End Get
        Set(value As String)
            _MensajeErrorDescriptiva = value
        End Set
    End Property

#End Region

End Class
