Imports System.Xml.Serialization
Imports System.Xml

Namespace Producto.GetProductos

    <XmlType(Namespace:="urn:GetProductos")> _
    <XmlRoot(Namespace:="urn:GetProductos")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _esManual As Boolean
        Private _vigente As Boolean
        Private _codigoProducto As List(Of String)
        Private _descripcionProducto As List(Of String)
        Private _claseBillete As List(Of String)
        Private _factorCorreccion As List(Of Double)
        Private _descripcionMaquina As List(Of String)


#End Region

#Region "Propriedades"

        Public Property EsManual() As Boolean
            Get
                Return _esManual
            End Get
            Set(value As Boolean)
                _esManual = value
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

        Public Property CodigoProducto() As List(Of String)
            Get
                Return _codigoProducto
            End Get
            Set(value As List(Of String))
                _codigoProducto = value
            End Set
        End Property

        Public Property DescripcionProducto() As List(Of String)
            Get
                Return _descripcionProducto
            End Get
            Set(value As List(Of String))
                _descripcionProducto = value
            End Set
        End Property

        Public Property ClaseBillete() As List(Of String)
            Get
                Return _claseBillete
            End Get
            Set(value As List(Of String))
                _claseBillete = value
            End Set
        End Property

        Public Property FactorCorreccion() As List(Of Double)
            Get
                Return _factorCorreccion
            End Get
            Set(value As List(Of Double))
                _factorCorreccion = value
            End Set
        End Property

        Public Property DescripcionMaquinas() As List(Of String)
            Get
                Return _descripcionMaquina
            End Get
            Set(value As List(Of String))
                _descripcionMaquina = value
            End Set
        End Property

#End Region

    End Class
End Namespace
