Imports System.Xml.Serialization
Imports System.Xml

Namespace Iac.GetIacDetail

    <XmlType(Namespace:="urn:GetIacDetail")> _
    <XmlRoot(Namespace:="urn:GetIacDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _iacs As IacColeccion

#End Region

#Region "Propriedades"

        Public Property Iacs() As IacColeccion
            Get
                Return _iacs
            End Get
            Set(value As IacColeccion)
                _iacs = value
            End Set
        End Property

#End Region
    End Class
End Namespace
