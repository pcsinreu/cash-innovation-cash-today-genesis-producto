Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoPuntosServicio.setTiposPuntosServicio

    <XmlType(Namespace:="urn:setTiposPuntosServicio")> _
    <XmlRoot(Namespace:="urn:setTiposPuntosServicio")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _CodigoTipoPuntoServicio As String
        Private _Resultado As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodigoTipoPuntoServicio() As String
            Get
                Return _CodigoTipoPuntoServicio
            End Get
            Set(value As String)
                _CodigoTipoPuntoServicio = value
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
