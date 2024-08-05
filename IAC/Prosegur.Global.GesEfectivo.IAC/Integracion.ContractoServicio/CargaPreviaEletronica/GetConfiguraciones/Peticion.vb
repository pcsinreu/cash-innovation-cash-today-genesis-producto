Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguraciones

    <XmlType(Namespace:="urn:GetConfiguraciones")> _
    <XmlRoot(Namespace:="urn:GetConfiguraciones")> _
    <Serializable()>
    Public Class Peticion

        Private _codigoCliente As String
        Private _codigosubCliente As String
        Private _codigoPuntoServicio As String
        Private _codigoCanal As String
        Private _codigoSubCanal As String
        Private _codigoDelegacion As String
        Private _formatoArchivo As Nullable(Of eFormatoArchivo)
        Private _tipoArchivo As Nullable(Of eTipoArchivo)
        Private _descripcionConfiguracion As String
        Private _bol_Vigente As Nullable(Of Boolean)
        Private _codigoConfiguracion As String

        Public Property CodigoConfiguracion() As String
            Get
                Return _codigoConfiguracion
            End Get
            Set(value As String)
                _codigoConfiguracion = value
            End Set
        End Property


        Public Property TipoArchivo() As Nullable(Of eTipoArchivo)
            Get
                Return _tipoArchivo
            End Get
            Set(value As Nullable(Of eTipoArchivo))
                _tipoArchivo = value
            End Set
        End Property

        Public Property FormatoArchivo() As Nullable(Of eFormatoArchivo)
            Get
                Return _formatoArchivo
            End Get
            Set(value As Nullable(Of eFormatoArchivo))
                _formatoArchivo = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
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

        Public Property CodigoCanal() As String
            Get
                Return _codigoCanal
            End Get
            Set(value As String)
                _codigoCanal = value
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

        Public Property CodigosubCliente() As String
            Get
                Return _codigosubCliente
            End Get
            Set(value As String)
                _codigosubCliente = value
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

        Public Property DescripcionConfiguracion() As String
            Get
                Return _descripcionConfiguracion
            End Get
            Set(value As String)
                _descripcionConfiguracion = value
            End Set
        End Property

        Public Property Bol_Vigente() As Nullable(Of Boolean)
            Get
                Return _bol_Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _bol_Vigente = value
            End Set
        End Property

    End Class

End Namespace