Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboInformacionAdicionalConFiltros

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 27/05/2014 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboInformacionAdicionalConFiltros")> _
    <XmlRoot(Namespace:="urn:GetComboInformacionAdicionalConFiltros")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _iacs As IacColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Iacs() As IacColeccion
            Get
                Return _iacs
            End Get
            Set(value As IacColeccion)
                _iacs = value
            End Set
        End Property

#End Region

    End Class
End Namespace