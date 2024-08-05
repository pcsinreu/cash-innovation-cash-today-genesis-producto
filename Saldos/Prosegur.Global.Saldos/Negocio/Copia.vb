<Serializable()> _
Public Class Copia

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Destinatario As String
    Private _TipoCopia As TipoCopia

#End Region

#Region "[PROPRIEDADES]"

    Public Property TipoCopia() As TipoCopia
        Get
            If _TipoCopia Is Nothing Then
                _TipoCopia = New TipoCopia()
            End If

            TipoCopia = _TipoCopia
        End Get
        Set(Value As TipoCopia)
            _TipoCopia = Value
        End Set
    End Property

    Public Property Destinatario() As String
        Get
            Destinatario = _Destinatario
        End Get
        Set(Value As String)
            _Destinatario = Value
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