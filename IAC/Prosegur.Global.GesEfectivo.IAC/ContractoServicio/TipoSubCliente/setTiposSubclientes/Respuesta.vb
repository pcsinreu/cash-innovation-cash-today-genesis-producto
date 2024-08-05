Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSubCliente.setTiposSubclientes

    <XmlType(Namespace:="urn:setTiposSubclientes")> _
    <XmlRoot(Namespace:="urn:setTiposSubclientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _codTipoSubcliente As String
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property codTipoSubcliente() As String
            Get
                Return _codTipoSubcliente
            End Get
            Set(value As String)
                _codTipoSubcliente = value
            End Set
        End Property

        Public Property Resultado() As String
            Get
                Return _Resultado
            End Get
            Set(value As String)
                _Resultado = value
            End Set
        End Property

#End Region

    End Class
End Namespace
