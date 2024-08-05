Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposMedioPago

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTiposMedioPago")> _
    <XmlRoot(Namespace:="urn:GetComboTiposMedioPago")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _TiposMedioPago As TipoMedioPagoColeccion

#End Region

#Region "[Propriedades]"

        Public Property TiposMedioPago() As TipoMedioPagoColeccion
            Get
                Return _TiposMedioPago
            End Get
            Set(value As TipoMedioPagoColeccion)
                _TiposMedioPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace