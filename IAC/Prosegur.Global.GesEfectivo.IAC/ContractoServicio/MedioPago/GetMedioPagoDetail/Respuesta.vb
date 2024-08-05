Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.GetMedioPagoDetail
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetMedioPagoDetail")> _
    <XmlRoot(Namespace:="urn:GetMedioPagoDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _MedioPagoCol As MedioPagoColeccion
#End Region

#Region "Propriedades"

        Public Property MedioPagos() As MedioPagoColeccion
            Get
                Return _MedioPagoCol
            End Get
            Set(value As MedioPagoColeccion)
                _MedioPagoCol = value
            End Set
        End Property

#End Region

    End Class

End Namespace
