Namespace RecuperarRemesasPorGrupo

    Public Class Transaccion

#Region "[VARIAVEIS]"

        Private _OidTransaccion As String
        Private _Fecha As DateTime
        Private _NumExterno As String
        Private _Planta As Planta
        Private _SectorOrigen As SectorOrigen
        Private _SectorDestino As SectorDestino
        Private _Cliente As Cliente
        Private _CanalOrigen As CanalOrigen
        Private _CanalDestino As CanalDestino
        Private _Monedas As Monedas
        Private _Documento As Documento
        Private _Disponible As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidTransaccion() As String
            Get
                Return _OidTransaccion
            End Get
            Set(value As String)
                _OidTransaccion = value
            End Set
        End Property
        Public Property Fecha() As DateTime
            Get
                Return _Fecha
            End Get
            Set(value As DateTime)
                _Fecha = value
            End Set
        End Property
        Public Property NumExterno() As String
            Get
                Return _NumExterno
            End Get
            Set(value As String)
                _NumExterno = value
            End Set
        End Property
        Public Property Planta() As Planta
            Get
                Return _Planta
            End Get
            Set(value As Planta)
                _Planta = value
            End Set
        End Property
        Public Property SectorOrigen() As SectorOrigen
            Get
                Return _SectorOrigen
            End Get
            Set(value As SectorOrigen)
                _SectorOrigen = value
            End Set
        End Property
        Public Property SectorDestino() As SectorDestino
            Get
                Return _SectorDestino
            End Get
            Set(value As SectorDestino)
                _SectorDestino = value
            End Set
        End Property
        Public Property Cliente() As Cliente
            Get
                Return _Cliente
            End Get
            Set(value As Cliente)
                _Cliente = value
            End Set
        End Property
        Public Property CanalOrigen() As CanalOrigen
            Get
                Return _CanalOrigen
            End Get
            Set(value As CanalOrigen)
                _CanalOrigen = value
            End Set
        End Property
        Public Property CanalDestino() As CanalDestino
            Get
                Return _CanalDestino
            End Get
            Set(value As CanalDestino)
                _CanalDestino = value
            End Set
        End Property
        Public Property Monedas() As Monedas
            Get
                Return _Monedas
            End Get
            Set(value As Monedas)
                _Monedas = value
            End Set
        End Property
        Public Property Documento() As Documento
            Get
                Return _Documento
            End Get
            Set(value As Documento)
                _Documento = value
            End Set
        End Property
        Public Property Disponible() As Nullable(Of Boolean)
            Get
                Return _Disponible
            End Get
            Set(value As Nullable(Of Boolean))
                _Disponible = value
            End Set
        End Property

#End Region

    End Class

End Namespace