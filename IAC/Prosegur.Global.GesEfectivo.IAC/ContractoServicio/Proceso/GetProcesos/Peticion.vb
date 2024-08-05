Imports System.Xml.Serialization
Imports System.Xml

Namespace Proceso.GetProceso

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetProceso")> _
    <XmlRoot(Namespace:="urn:GetProceso")> _
    <Serializable()> _
Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _codigoCliente As String
        Private _codigoSubcliente As List(Of String)
        Private _codigoPuntoServicio As List(Of String)
        Private _codigoCanal As List(Of String)
        Private _codigoSubcanal As List(Of String)
        Private _codigoDelegacion As List(Of String)
        Private _codigoProducto As List(Of String)
        Private _vigente As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property CodigoSubcliente() As List(Of String)
            Get
                Return _codigoSubcliente
            End Get
            Set(value As List(Of String))
                _codigoSubcliente = value
            End Set
        End Property

        Public Property CodigoPuntoServicio() As List(Of String)
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As List(Of String))
                _codigoPuntoServicio = value
            End Set
        End Property

        Public Property CodigoCanal() As List(Of String)
            Get
                Return _codigoCanal
            End Get
            Set(value As List(Of String))
                _codigoCanal = value
            End Set
        End Property

        Public Property CodigoSubcanal() As List(Of String)
            Get
                Return _codigoSubcanal
            End Get
            Set(value As List(Of String))
                _codigoSubcanal = value
            End Set
        End Property

        Public Property CodigoDelegacion() As List(Of String)
            Get
                Return _codigoDelegacion
            End Get
            Set(value As List(Of String))
                _codigoDelegacion = value
            End Set
        End Property

        Public Property CodigoProducto() As List(Of String)
            Get
                Return _codigoProducto
            End Get
            Set(value As List(Of String))
                _codigoProducto = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

#End Region
    End Class
End Namespace