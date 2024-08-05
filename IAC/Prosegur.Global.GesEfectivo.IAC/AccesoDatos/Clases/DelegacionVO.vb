Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Parametro

Public Class DelegacionVO

    Private _oidDelegacion As String
    Private _codigoPais As String

    Public Property OIDDelegacion As String
        Get
            Return _oidDelegacion
        End Get
        Set(value As String)
            _oidDelegacion = value
        End Set
    End Property

    Public Property CodigoPais As String
        Get
            Return _codigoPais
        End Get
        Set(value As String)
            _codigoPais = value
        End Set
    End Property
End Class