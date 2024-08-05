Imports System.Xml.Serialization
Imports System.Xml

Namespace bcp.RecuperarPedidosReportadosBCP

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/07/2012 - Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarPedidosReportadosBCP")> _
    <XmlRoot(Namespace:="urn:RecuperarPedidosReportadosBCP")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _Pedidos As PedidoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Pedidos() As PedidoColeccion
            Get
                Return _Pedidos
            End Get
            Set(value As PedidoColeccion)
                _Pedidos = value
            End Set
        End Property

#End Region

    End Class
End Namespace