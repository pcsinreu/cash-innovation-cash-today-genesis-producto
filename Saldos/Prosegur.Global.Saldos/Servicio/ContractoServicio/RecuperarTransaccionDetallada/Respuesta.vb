Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarTransaccionDetallada

    ''' <summary>
    ''' Clase Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 12/07/2011 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarTransaccionDetallada")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionDetallada")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Transaccion As Transaccion

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Datos del Saldo
        ''' </summary>
        ''' <value>RecuperarTransaccionDetallada.Transacciones</value>
        ''' <returns>RecuperarTransaccionDetallada.Transacciones</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Transaccion() As Transaccion
            Get
                Return _Transaccion
            End Get
            Set(value As Transaccion)
                _Transaccion = value
            End Set
        End Property

#End Region

    End Class

End Namespace