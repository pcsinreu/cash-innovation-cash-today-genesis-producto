Imports Prosegur.DbHelper
Imports Prosegur

<Serializable()> _
Public Class Bulto

#Region "[VARIÁVEIS]"

    Private _NumPrecinto As String
    Private _CodBolsa As String
    Private _Destino As Destino

#End Region

#Region "[PROPRIEDADES]"

    Public Property Destino() As Destino
        Get
            If _Destino Is Nothing Then
                _Destino = New Destino()
            End If
            Return _Destino
        End Get
        Set(Value As Destino)
            _Destino = Value
        End Set
    End Property

    Public Property CodBolsa() As String
        Get
            Return _CodBolsa
        End Get
        Set(Value As String)
            _CodBolsa = Value
        End Set
    End Property

    Public Property NumPrecinto() As String
        Get
            Return _NumPrecinto
        End Get
        Set(Value As String)
            _NumPrecinto = Value
        End Set
    End Property

#End Region

End Class