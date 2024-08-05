Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposMedioPagoByDivisa

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTiposMedioPagoByDivisa")> _
    <XmlRoot(Namespace:="urn:GetComboTiposMedioPagoByDivisa")> _
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