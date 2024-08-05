Imports System.Xml.Serialization
Imports System.Xml

Namespace Producto.GetProductos

    <XmlType(Namespace:="urn:GetProductos")> _
    <XmlRoot(Namespace:="urn:GetProductos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _productos As ProductoColeccion
#End Region

#Region "Propriedades"

        Public Property Productos() As ProductoColeccion
            Get
                Return _productos
            End Get
            Set(value As ProductoColeccion)
                _productos = value
            End Set
        End Property
#End Region

    End Class
End Namespace