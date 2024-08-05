Namespace Parametro.GetParametroDetail
    <Serializable()> _
    Public Class Parametro

        Public Sub New()
            _Aplicacion = New Aplicacion
            _Agrupacion = New Agrupacion
            _Nivel = New Nivel
        End Sub

#Region "Variáveis"
        Private _CodParametro As String
        Private _DesCortaParametro As String
        Private _DesLargaParametro As String
        Private _NecOrden As Integer
        Private _BolObligatorio As Boolean
        Private _NecTipoComponente As TipoComponente
        Private _NecTipoDato As TipoDato
        Private _Aplicacion As Aplicacion
        Private _Agrupacion As Agrupacion
        Private _Nivel As Nivel
#End Region

#Region "Propriedades"
        Public Property CodParametro() As String
            Get
                Return _CodParametro
            End Get
            Set(value As String)
                _CodParametro = value
            End Set
        End Property

        Public Property DesCortaParametro() As String
            Get
                Return _DesCortaParametro
            End Get
            Set(value As String)
                _DesCortaParametro = value
            End Set
        End Property

        Public Property DesLargaParametro() As String
            Get
                Return _DesLargaParametro
            End Get
            Set(value As String)
                _DesLargaParametro = value
            End Set
        End Property

        Public Property NecOrden() As Integer
            Get
                Return _NecOrden
            End Get
            Set(value As Integer)
                _NecOrden = value
            End Set
        End Property

        Public Property BolObligatorio() As Boolean
            Get
                Return _BolObligatorio
            End Get
            Set(value As Boolean)
                _BolObligatorio = value
            End Set
        End Property

        Public Property NecTipoComponente() As TipoComponente
            Get
                Return _NecTipoComponente
            End Get
            Set(value As TipoComponente)
                _NecTipoComponente = value
            End Set
        End Property

        Public Property NecTipoDato() As TipoDato
            Get
                Return _NecTipoDato
            End Get
            Set(value As TipoDato)
                _NecTipoDato = value
            End Set
        End Property

        Public Property Aplicacion() As Aplicacion
            Get
                Return _Aplicacion
            End Get
            Set(value As Aplicacion)
                _Aplicacion = value
            End Set
        End Property

        Public Property Agrupacion() As Agrupacion
            Get
                Return _Agrupacion
            End Get
            Set(value As Agrupacion)
                _Agrupacion = value
            End Set
        End Property

        Public Property Nivel() As Nivel
            Get
                Return _Nivel
            End Get
            Set(value As Nivel)
                _Nivel = value
            End Set
        End Property


#End Region
    End Class
End Namespace