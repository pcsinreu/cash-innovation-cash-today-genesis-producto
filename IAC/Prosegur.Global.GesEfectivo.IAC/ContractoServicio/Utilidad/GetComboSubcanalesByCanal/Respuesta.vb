Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboSubcanalesByCanal

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboSubcanalesByCanal")> _
    <XmlRoot(Namespace:="urn:GetComboSubcanalesByCanal")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _canales As CanalColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Canales() As CanalColeccion
            Get
                Return _canales
            End Get
            Set(value As CanalColeccion)
                _canales = value
            End Set
        End Property

#End Region

    End Class

End Namespace