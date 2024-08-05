Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetParametroDetail

    <XmlType(Namespace:="urn:GetParametroDetail")> _
    <XmlRoot(Namespace:="urn:GetParametroDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _Parametro As Parametro
#End Region

#Region "Propriedades"
        Public Property Parametro() As Parametro
            Get
                Return _Parametro
            End Get
            Set(value As Parametro)
                _Parametro = value
            End Set
        End Property

#End Region

    End Class
End Namespace