Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarDatosDocumento

    <XmlType(Namespace:="urn:RecuperarDatosDocumento")> _
    <XmlRoot(Namespace:="urn:RecuperarDatosDocumento")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Documentos As Documentos

#End Region

#Region "[PROPRIEDADES]"

        Public Property Documentos() As Documentos
            Get
                If _Documentos Is Nothing Then
                    _Documentos = New Documentos
                End If
                Return _Documentos
            End Get
            Set(value As Documentos)
                _Documentos = value
            End Set
        End Property

#End Region

    End Class

End Namespace