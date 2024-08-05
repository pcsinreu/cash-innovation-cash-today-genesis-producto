Imports System.Xml.Serialization
Imports System.Xml

Namespace CreacionMifIntersector

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [abueno] 23/07/2010 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:CreacionMifIntersector")> _
    <XmlRoot(Namespace:="urn:CreacionMifIntersector")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _OidDocumentoSaldos As String
        Private _codEstado As String
        Private _codComprobante As String

#End Region

#Region "[PROPRIEDADES]"


        ''' <summary>
        ''' Propriedad OidDocumentoSaldos
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 23/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property OidDocumentoSaldos() As String
            Get
                Return _OidDocumentoSaldos
            End Get
            Set(value As String)
                _OidDocumentoSaldos = value
            End Set
        End Property

        Public Property CodEstado() As String
            Get
                Return _codEstado
            End Get
            Set(value As String)
                _codEstado = value
            End Set
        End Property

        Public Property CodComprobante() As String
            Get
                Return _codComprobante
            End Get
            Set(value As String)
                _codComprobante = value
            End Set
        End Property

#End Region

    End Class

End Namespace