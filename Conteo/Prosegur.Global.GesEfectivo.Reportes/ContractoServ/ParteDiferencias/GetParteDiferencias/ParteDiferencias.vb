Namespace ParteDiferencias.GetParteDiferencias

    Public Class ParteDiferencias

#Region " Variáveis "

        Private _PrecintoRemesa As String = String.Empty
        Private _CodigoTransporte As String = String.Empty
        Private _FechaConteo As Date = DateTime.MinValue
        Private _FechaTransporte As Date = DateTime.MinValue
        Private _Cliente As String = String.Empty
        Private _SubCliente As String = String.Empty
        Private _PuntoServicio As String = String.Empty
        Private _DatosDocumentos As List(Of DatosDocumentos)

#End Region

#Region " Propriedades "

        Public Property PrecintoRemesa() As String
            Get
                Return _PrecintoRemesa
            End Get
            Set(value As String)
                _PrecintoRemesa = value
            End Set
        End Property
        Public Property CodigoTransporte() As String
            Get
                Return _CodigoTransporte
            End Get
            Set(value As String)
                _CodigoTransporte = value
            End Set
        End Property
        Public Property FechaConteo() As DateTime
            Get
                Return _FechaConteo
            End Get
            Set(value As DateTime)
                _FechaConteo = value
            End Set
        End Property
        Public Property FechaTransporte() As DateTime
            Get
                Return _FechaTransporte
            End Get
            Set(value As DateTime)
                _FechaTransporte = value
            End Set
        End Property
        Public Property Cliente() As String
            Get
                Return _Cliente
            End Get
            Set(value As String)
                _Cliente = value
            End Set
        End Property
        Public Property SubCliente() As String
            Get
                Return _SubCliente
            End Get
            Set(value As String)
                _SubCliente = value
            End Set
        End Property
        Public Property PuntoServicio() As String
            Get
                Return _PuntoServicio
            End Get
            Set(value As String)
                _PuntoServicio = value
            End Set
        End Property
        Public Property DatosDocumentos() As List(Of DatosDocumentos)
            Get
                Return _DatosDocumentos
            End Get
            Set(value As List(Of DatosDocumentos))
                _DatosDocumentos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
