Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Parametro

Public Class ParametroValueVO

    Private _oidParametro As String
    Private _oidParametroValue As String
    Private _codigoParametro As String
    Private _tipoNivel As TipoNivel
    Private _valor As String

    Public Property OIDParametro As String
        Get
            Return _oidParametro
        End Get
        Set(value As String)
            _oidParametro = value
        End Set
    End Property

    Public Property OIDParametroValue As String
        Get
            Return _oidParametroValue
        End Get
        Set(value As String)
            _oidParametroValue = value
        End Set
    End Property

    Public Property TipoNivel As TipoNivel
        Get
            Return _tipoNivel
        End Get
        Set(value As TipoNivel)
            _tipoNivel = value
        End Set
    End Property

    Public Property CodigoParametro As String
        Get
            Return _codigoParametro
        End Get
        Set(value As String)
            _codigoParametro = value
        End Set
    End Property

    Public Property Valor As String
        Get
            Return _valor
        End Get
        Set(value As String)
            _valor = value
        End Set
    End Property

End Class