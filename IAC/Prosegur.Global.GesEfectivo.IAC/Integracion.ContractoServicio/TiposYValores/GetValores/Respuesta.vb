Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposYValores.GetValores

    <XmlType(Namespace:="urn:GetValores")> _
    <XmlRoot(Namespace:="urn:GetValores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _valores As List(Of Valor)
#End Region

#Region "Propriedades"

        Public Property Valores() As List(Of Valor)
            Get
                Return _valores
            End Get
            Set(value As List(Of Valor))
                _valores = value
            End Set
        End Property
#End Region

    End Class
End Namespace