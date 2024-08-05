Imports System.Xml.Serialization
Imports System.Xml

Namespace GetProcesosPorDelegacion

    <XmlType(Namespace:="urn:GetProcesosPorDelegacion")> _
    <XmlRoot(Namespace:="urn:GetProcesosPorDelegacion")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _codigoDelegacion As String
        Private _codigoCliente As String
        Private _descripcionCliente As String
        Private _codigoSubCliente As String
        Private _descripcionSubCliente As String
        Private _codigoPuntoServicio As String
        Private _descripcionPuntoServicio As String
        Private _codigoCanal As String
        Private _descripcionCanal As String
        Private _codigoSubCanal As String
        Private _descripcionSubCanal As String
        Private _descripcionProceso As String
        Private _estadoVigencia As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
            End Set
        End Property
        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property
        Public Property DescripcionCliente() As String
            Get
                Return _descripcionCliente
            End Get
            Set(value As String)
                _descripcionCliente = value
            End Set
        End Property
        Public Property CodigoSubCliente() As String
            Get
                Return _codigoSubCliente
            End Get
            Set(value As String)
                _codigoSubCliente = value
            End Set
        End Property
        Public Property DescripcionSubCliente() As String
            Get
                Return _descripcionSubCliente
            End Get
            Set(value As String)
                _descripcionSubCliente = value
            End Set
        End Property
        Public Property CodigoPuntoServicio() As String
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As String)
                _codigoPuntoServicio = value
            End Set
        End Property
        Public Property DescripcionPuntoServicio() As String
            Get
                Return _descripcionPuntoServicio
            End Get
            Set(value As String)
                _descripcionPuntoServicio = value
            End Set
        End Property
        Public Property CodigoCanal() As String
            Get
                Return _codigoCanal
            End Get
            Set(value As String)
                _codigoCanal = value
            End Set
        End Property
        Public Property DescripcionCanal() As String
            Get
                Return _descripcionCanal
            End Get
            Set(value As String)
                _descripcionCanal = value
            End Set
        End Property
        Public Property CodigoSubCanal() As String
            Get
                Return _codigoSubCanal
            End Get
            Set(value As String)
                _codigoSubCanal = value
            End Set
        End Property
        Public Property DescripcionSubCanal() As String
            Get
                Return _descripcionSubCanal
            End Get
            Set(value As String)
                _descripcionSubCanal = value
            End Set
        End Property
        Public Property DescripcionProceso() As String
            Get
                Return _descripcionProceso
            End Get
            Set(value As String)
                _descripcionProceso = value
            End Set
        End Property
        Public Property EstadoVigencia() As Nullable(Of Boolean)
            Get
                Return _estadoVigencia
            End Get
            Set(value As Nullable(Of Boolean))
                _estadoVigencia = value
            End Set
        End Property

#End Region

    End Class

End Namespace
