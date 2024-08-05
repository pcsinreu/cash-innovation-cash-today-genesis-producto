Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarSaldosPorSector

    <XmlType(Namespace:="urn:RecuperarSaldosPorSector")> _
    <XmlRoot(Namespace:="urn:RecuperarSaldosPorSector")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Saldos As Saldos

#End Region

#Region "[PROPRIEDADES]"

        Public Property Saldos() As Saldos
            Get
                Return _Saldos
            End Get
            Set(value As Saldos)
                _Saldos = value
            End Set
        End Property

#End Region

    End Class

End Namespace