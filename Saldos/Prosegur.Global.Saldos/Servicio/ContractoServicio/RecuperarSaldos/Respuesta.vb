Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarSaldos

    ''' <summary>
    ''' Clase Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarSaldos")> _
    <XmlRoot(Namespace:="urn:RecuperarSaldos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Saldo As Saldo

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Datos del Saldo
        ''' </summary>
        ''' <value>RecuperarSaldos.Saldo</value>
        ''' <returns>RecuperarSaldos.Saldo</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Saldo() As Saldo
            Get
                Return _Saldo
            End Get
            Set(value As Saldo)
                _Saldo = value
            End Set
        End Property

#End Region

    End Class

End Namespace