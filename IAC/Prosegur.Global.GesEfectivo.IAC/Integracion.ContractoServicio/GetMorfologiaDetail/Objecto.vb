
Namespace GetMorfologiaDetail

    ''' <summary>
    ''' Classe Componente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    <Serializable()> _
    Public Class Objecto

#Region "[Variáveis]"

        Private _oidComponenteObjeto As String
        Private _codIsoDivisa As String
        Private _oidDivisa As String
        Private _desDivisa As String
        Private _bolEfectivo As Boolean
        Private _tiposMedioPago As List(Of TipoMedioPago)
        Private _denominacion As Denominacion
        Private _necOrdenDivisa As Integer
        Private _necOrdenTipoMedPago As Integer

#End Region

#Region "[Propriedades]"

        Public Property Denominacion As Denominacion
            Get
                Return _denominacion
            End Get
            Set(value As Denominacion)
                _denominacion = value
            End Set
        End Property

        Public Property OidComponenteObjeto() As String
            Get
                Return _oidComponenteObjeto
            End Get
            Set(value As String)
                _oidComponenteObjeto = value
            End Set
        End Property

        Public Property TiposMedioPago() As List(Of TipoMedioPago)
            Get
                If _tiposMedioPago Is Nothing Then
                    _tiposMedioPago = New List(Of TipoMedioPago)
                End If
                Return _tiposMedioPago
            End Get
            Set(value As List(Of TipoMedioPago))
                _tiposMedioPago = value
            End Set
        End Property

        Public Property CodIsoDivisa() As String
            Get
                Return _codIsoDivisa
            End Get
            Set(value As String)
                _codIsoDivisa = value
            End Set
        End Property

        Public Property OidDivisa As String
            Get
                Return _oidDivisa
            End Get
            Set(value As String)
                _oidDivisa = value
            End Set
        End Property

        Public Property DesDivisa() As String
            Get
                Return _desDivisa
            End Get
            Set(value As String)
                _desDivisa = value
            End Set
        End Property

        Public Property BolEfectivo() As Boolean
            Get
                Return _bolEfectivo
            End Get
            Set(value As Boolean)
                _bolEfectivo = value
            End Set
        End Property

        Public Property NecOrdenDivisa As Integer
            Get
                Return _necOrdenDivisa
            End Get
            Set(value As Integer)
                _necOrdenDivisa = value
            End Set
        End Property

        Public Property NecOrdenTipoMedPago As Integer
            Get
                Return _necOrdenTipoMedPago
            End Get
            Set(value As Integer)
                _necOrdenTipoMedPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace