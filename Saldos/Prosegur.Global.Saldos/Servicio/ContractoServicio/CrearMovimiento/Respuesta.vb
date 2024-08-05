Imports System.Xml.Serialization
Imports System.Xml

Namespace CrearMovimiento

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 28/06/2011 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:CrearMovimiento")> _
    <XmlRoot(Namespace:="urn:CrearMovimiento")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _OidTransaccion As String

#End Region

#Region "[PROPRIEDADES]"


        ''' <summary>
        ''' Identificador del documento en Saldos
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property OidTransaccion() As String
            Get
                Return _OidTransaccion
            End Get
            Set(value As String)
                _OidTransaccion = value
            End Set
        End Property

#End Region

    End Class

End Namespace