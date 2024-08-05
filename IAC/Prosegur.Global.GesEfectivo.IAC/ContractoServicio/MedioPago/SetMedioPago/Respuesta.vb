Imports System.Xml.Serialization
Imports System.Xml


Namespace MedioPago.SetMedioPago
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetMedioPago")> _
    <XmlRoot(Namespace:="urn:SetMedioPago")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _RespuestaMedioPagos As RespuestaMedioPagoColeccion

#End Region

#Region "[Propriedades]"

        Public Property RespuestaMedioPagos() As RespuestaMedioPagoColeccion
            Get
                Return _RespuestaMedioPagos
            End Get
            Set(value As RespuestaMedioPagoColeccion)
                _RespuestaMedioPagos = value
            End Set
        End Property
#End Region

    End Class

End Namespace
