Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboInformacionAdicional

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboInformacionAdicional")> _
    <XmlRoot(Namespace:="urn:GetComboInformacionAdicional")> _
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