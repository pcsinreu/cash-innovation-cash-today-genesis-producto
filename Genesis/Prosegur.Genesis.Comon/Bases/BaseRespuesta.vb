Imports System.Runtime.Serialization

<Serializable()>
Public MustInherit Class BaseRespuesta

    Private _mensajes As New List(Of String)
    Private _excepciones As New List(Of String)

    Public ReadOnly Property Mensajes As List(Of String)
        Get
            Return _mensajes
        End Get
    End Property

    Public ReadOnly Property Excepciones As List(Of String)
        Get
            Return _excepciones
        End Get
    End Property

    Public ReadOnly Property HayMensajes As Boolean
        Get
            Return (Mensajes IsNot Nothing AndAlso Mensajes.Count > 0) OrElse (Excepciones IsNot Nothing AndAlso Excepciones.Count > 0)
        End Get
    End Property

    Public ReadOnly Property TodasMensajes As String
        Get
            If Mensajes IsNot Nothing Then
                Return String.Join(vbCrLf, Mensajes.ToArray()).Trim
            End If
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property TodasExcepciones As String
        Get
            If Excepciones IsNot Nothing Then
                Return String.Join(vbCrLf, Excepciones.ToArray()).Trim
            End If
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property TodasMensajesYExcepciones As String
        Get
            If Mensajes IsNot Nothing AndAlso Excepciones IsNot Nothing Then
                Return String.Join(vbCrLf, New String() {String.Join(vbCrLf, Mensajes.ToArray()), String.Join(vbCrLf, Excepciones.ToArray())}).Trim
            End If
            Return String.Empty
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(mensaje As String)
        _mensajes.Add(mensaje)
    End Sub

    Public Sub New(excepcion As Exception)
        _mensajes.Add(excepcion.Message)
        _excepciones.Add(excepcion.ToString)
    End Sub

    ''' <summary>
    ''' Estructura donde vendrá informado el movimiento a crear
    ''' </summary>
    Public Property TiempoDeEjecucion As String

End Class
