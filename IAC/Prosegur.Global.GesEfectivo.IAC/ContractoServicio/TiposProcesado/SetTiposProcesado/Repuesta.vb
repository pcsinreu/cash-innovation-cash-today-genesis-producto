Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposProcesado.SetTiposProcesado


    <XmlType(Namespace:="urn:SetTiposProcesado")> _
    <XmlRoot(Namespace:="urn:SetTiposProcesado")> _
    <Serializable()> _
    Public Class Repuesta
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
