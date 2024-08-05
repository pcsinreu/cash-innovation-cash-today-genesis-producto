Imports System.Xml.Serialization
Imports System.Xml

Namespace GetProceso

    <XmlType(Namespace:="urn:GetProceso")> _
    <XmlRoot(Namespace:="urn:GetProceso")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _codigoCliente As String
        Private _codigoSubcliente As String
        Private _codigoPuntoServicio As String
        Private _codigoSubcanal As String
        Private _codigoDelegacion As String
        Private _identificadorProceso As String

#End Region

#Region "Propriedades"

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property CodigoSubcliente() As String
            Get
                Return _codigoSubcliente
            End Get
            Set(value As String)
                _codigoSubcliente = value
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

        Public Property CodigoSubcanal() As String
            Get
                Return _codigoSubcanal
            End Get
            Set(value As String)
                _codigoSubcanal = value
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

        Public Property IdentificadorProceso() As String
            Get
                Return _identificadorProceso
            End Get
            Set(value As String)
                _identificadorProceso = value
            End Set
        End Property

#End Region
    End Class
End Namespace