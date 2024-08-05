Imports System.Xml.Serialization

Namespace Contractos.Genesis.Pantallas.Delegaciones
    <Serializable()>
    Public Class DelegacionFacturacion

        Public Property OID_DELEGACIONXCONFIG_FACTUR As String
        Public Property OID_CLIENTE As String
        Public Property COD_CLIENTE As String
        Public Property DES_CLIENTE As String
        Public Property OID_SUBCLIENTE As String
        Public Property COD_SUBCLIENTE As String
        Public Property DES_SUBCLIENTE As String
        Public Property OID_PTO_SERVICIO As String
        Public Property COD_PTO_SERVICIO As String
        Public Property DES_PTO_SERVICIO As String

        Public ReadOnly Property BANCO_CAPITAL As String
            Get
                Return COD_CLIENTE + " - " + DES_CLIENTE
            End Get
        End Property

        Public ReadOnly Property BANCO_TESORERIA As String
            Get
                Return COD_SUBCLIENTE + " - " + DES_SUBCLIENTE
            End Get
        End Property

        Public ReadOnly Property CUENTA_TESORERIA As String
            Get
                Return COD_PTO_SERVICIO + " - " + DES_PTO_SERVICIO
            End Get
        End Property
        Public Property DATOS_BANCARIOS As String
        Public Property QUITAR As String

    End Class
End Namespace