Imports System.Xml.Serialization
Imports System.Xml

Namespace GenesisSaldos.Certificacion.ObtenerCertificado

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:ObtenerCertificado")> _
    <XmlRoot(Namespace:="urn:ObtenerCertificado")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _resultado As String
        Private _Certificado As ObtenerCertificado.CertificadoColeccion
#End Region

#Region "[PROPRIEDADE]"

        Public Property Certificado() As ObtenerCertificado.CertificadoColeccion
            Get
                Return _Certificado
            End Get
            Set(value As ObtenerCertificado.CertificadoColeccion)
                _Certificado = value
            End Set
        End Property

        Public Property resultado() As String
            Get
                Return _resultado
            End Get
            Set(value As String)
                _resultado = value
            End Set
        End Property

#End Region

    End Class
End Namespace

