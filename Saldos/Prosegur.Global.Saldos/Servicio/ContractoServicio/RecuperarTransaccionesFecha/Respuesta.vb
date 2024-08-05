Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarTransaccionesFechas

    ''' <summary>
    ''' Clase Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 12/07/2011 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Transacciones As Transacciones

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Datos del Saldo
        ''' </summary>
        ''' <value>RecuperarSaldos.Transacciones</value>
        ''' <returns>RecuperarSaldos.Transacciones</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Transacciones() As Transacciones
            Get
                Return _Transacciones
            End Get
            Set(value As Transacciones)
                _Transacciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace