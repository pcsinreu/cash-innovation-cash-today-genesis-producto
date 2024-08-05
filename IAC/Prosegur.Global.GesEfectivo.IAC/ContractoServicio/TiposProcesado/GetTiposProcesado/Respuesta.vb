Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposProcesado.GetTiposProcesado
    <XmlType(Namespace:="urn:GetCanales")> _
    <XmlRoot(Namespace:="urn:GetCanales")> _
    <Serializable()> _
Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _tiposProcessados As TipoProcesadoColeccion

#End Region

#Region "Propriedades"
        Public Property TiposProcessados() As TipoProcesadoColeccion
            Get
                Return _tiposProcessados
            End Get
            Set(value As TipoProcesadoColeccion)
                _tiposProcessados = value
            End Set
        End Property
#End Region
    End Class

End Namespace