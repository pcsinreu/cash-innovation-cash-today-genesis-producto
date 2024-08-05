Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarRemesasPorGrupo

    <XmlType(Namespace:="urn:RecuperarRemesasPorGrupo")> _
    <XmlRoot(Namespace:="urn:RecuperarRemesasPorGrupo")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Grupos As Grupos

#End Region

#Region "[PROPRIEDADES]"

        Public Property Grupos() As Grupos
            Get
                Return _Grupos
            End Get
            Set(value As Grupos)
                _Grupos = value
            End Set
        End Property

#End Region

    End Class

End Namespace