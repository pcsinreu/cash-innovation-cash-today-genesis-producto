Imports System.Xml.Serialization
Imports System.Xml


Namespace GetIac

    <XmlType(Namespace:="urn:GetIacIntegracion")> _
    <XmlRoot(Namespace:="urn:GetIacIntegracion")> _
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
