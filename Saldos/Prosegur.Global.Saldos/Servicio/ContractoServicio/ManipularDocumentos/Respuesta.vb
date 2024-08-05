Imports System.Xml.Serialization
Imports System.Xml

Namespace ManipularDocumentos

    ''' <summary>
    ''' ManipularDocumentos Respuesta
    ''' </summary>
    <Serializable()> _
    <XmlType(Namespace:="urn:ManipularDocumentos")> _
    <XmlRoot(Namespace:="urn:ManipularDocumentos")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Resultados As Resultados

#End Region

#Region "[PROPRIEDADES]"

        Public Property Resultados() As Resultados
            Get
                Return _Resultados
            End Get
            Set(value As Resultados)
                _Resultados = value
            End Set
        End Property

#End Region

    End Class

End Namespace