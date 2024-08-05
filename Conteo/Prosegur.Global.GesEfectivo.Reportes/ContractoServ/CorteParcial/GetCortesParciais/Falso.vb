Namespace CorteParcial.GetCortesParciais

    Public Class Falso

#Region " Variáveis "

        Private _Remesa As String = String.Empty
        Private _Tipo As String = String.Empty
        Private _Divisa As String = String.Empty
        Private _Denominacion As String = String.Empty
        Private _NumeroSerie As String = String.Empty
        Private _NumeroPlancha As String = String.Empty
        Private _Observacion As String = String.Empty
        Private _NumeroUnidades As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property Remesa() As String
            Get
                Return _Remesa
            End Get
            Set(value As String)
                _Remesa = value
            End Set
        End Property
        Public Property Tipo() As String
            Get
                Return _Tipo
            End Get
            Set(value As String)
                _Tipo = value
            End Set
        End Property
        Public Property Divisa() As String
            Get
                Return _Divisa
            End Get
            Set(value As String)
                _Divisa = value
            End Set
        End Property
        Public Property Denominacion() As String
            Get
                Return _Denominacion
            End Get
            Set(value As String)
                _Denominacion = value
            End Set
        End Property
        Public Property NumeroSerie() As String
            Get
                Return _NumeroSerie
            End Get
            Set(value As String)
                _NumeroSerie = value
            End Set
        End Property
        Public Property NumeroPlancha() As String
            Get
                Return _NumeroPlancha
            End Get
            Set(value As String)
                _NumeroPlancha = value
            End Set
        End Property
        Public Property Observacion() As String
            Get
                Return _Observacion
            End Get
            Set(value As String)
                _Observacion = value
            End Set
        End Property
        Public Property NumeroUnidades() As String
            Get
                Return _NumeroUnidades
            End Get
            Set(value As String)
                _NumeroUnidades = value
            End Set
        End Property

#End Region

    End Class

End Namespace
