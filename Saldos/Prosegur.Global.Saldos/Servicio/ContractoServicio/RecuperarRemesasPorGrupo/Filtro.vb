Namespace RecuperarRemesasPorGrupo

    Public Class Filtro

#Region "[VARIAVEIS]"

        Private _TipoNegocio As Enumeradores.TipoNegocio
        Private _CodigosGrupo As List(Of String)
        Private _FechaHoraSaldoDesde As DateTime
        Private _FechaHoraSaldoHasta As DateTime
        Private _CodigoPlanta As String
        Private _CodigoSector As String
        Private _CodidoCliente As String
        Private _CodigoCanal As String
        Private _CodigoMoneda As String
        Private _SaldoDesglosado As Boolean
        Private _SoloSaldoDisponible As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property TipoNegocio() As Enumeradores.TipoNegocio
            Get
                Return _TipoNegocio
            End Get
            Set(value As Enumeradores.TipoNegocio)
                _TipoNegocio = value
            End Set
        End Property
        Public Property CodigosGrupo() As List(Of String)
            Get
                Return _CodigosGrupo
            End Get
            Set(value As List(Of String))
                _CodigosGrupo = value
            End Set
        End Property
        Public Property FechaHoraSaldoDesde() As DateTime
            Get
                Return _FechaHoraSaldoDesde
            End Get
            Set(value As DateTime)
                _FechaHoraSaldoDesde = value
            End Set
        End Property
        Public Property FechaHoraSaldoHasta() As DateTime
            Get
                Return _FechaHoraSaldoHasta
            End Get
            Set(value As DateTime)
                _FechaHoraSaldoHasta = value
            End Set
        End Property
        Public Property CodigoPlanta() As String
            Get
                Return _CodigoPlanta
            End Get
            Set(value As String)
                _CodigoPlanta = value
            End Set
        End Property
        Public Property CodigoSector() As String
            Get
                Return _CodigoSector
            End Get
            Set(value As String)
                _CodigoSector = value
            End Set
        End Property
        Public Property CodidoCliente() As String
            Get
                Return _CodidoCliente
            End Get
            Set(value As String)
                _CodidoCliente = value
            End Set
        End Property
        Public Property CodigoCanal() As String
            Get
                Return _CodigoCanal
            End Get
            Set(value As String)
                _CodigoCanal = value
            End Set
        End Property
        Public Property CodigoMoneda() As String
            Get
                Return _CodigoMoneda
            End Get
            Set(value As String)
                _CodigoMoneda = value
            End Set
        End Property
        Public Property SaldoDesglosado() As Boolean
            Get
                Return _SaldoDesglosado
            End Get
            Set(value As Boolean)
                _SaldoDesglosado = value
            End Set
        End Property
        Public Property SoloSaldoDisponible() As Nullable(Of Boolean)
            Get
                Return _SoloSaldoDisponible
            End Get
            Set(value As Nullable(Of Boolean))
                _SoloSaldoDisponible = value
            End Set
        End Property

#End Region

    End Class

End Namespace