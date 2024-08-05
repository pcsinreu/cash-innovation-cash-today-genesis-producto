Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboSubclientesByCliente

    ''' <summary>
    ''' Classe Resposta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboSubclientesByCliente")> _
    <XmlRoot(Namespace:="urn:GetComboSubclientesByCliente")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _clientes As ClienteColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Clientes() As ClienteColeccion
            Get
                Return _clientes
            End Get
            Set(value As ClienteColeccion)
                _clientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace