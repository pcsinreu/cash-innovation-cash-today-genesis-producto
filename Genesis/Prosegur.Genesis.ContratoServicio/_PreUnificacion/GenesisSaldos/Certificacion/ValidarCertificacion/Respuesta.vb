Imports System.Xml.Serialization
Imports System.Xml

Namespace GenesisSaldos.Certificacion.ValidarCertificacion

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 27/05/2013 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:ValidarCertificacion")> _
    <XmlRoot(Namespace:="urn:ValidarCertificacion")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _BolError As Boolean
        Private _CodigosUltimosCertificados As List(Of String)

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodigosUltimosCertificados As List(Of String)
            Get
                Return _CodigosUltimosCertificados
            End Get
            Set(value As List(Of String))
                _CodigosUltimosCertificados = value
            End Set
        End Property

        Public Property BolError() As Boolean
            Get
                Return _BolError
            End Get
            Set(value As Boolean)
                _BolError = value
            End Set
        End Property

#End Region
    End Class

End Namespace

