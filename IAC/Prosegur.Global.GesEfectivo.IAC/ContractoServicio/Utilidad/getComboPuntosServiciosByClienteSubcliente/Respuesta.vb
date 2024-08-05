Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.getComboPuntosServiciosByClienteSubcliente

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:getComboPuntosServiciosByClienteSubcliente")> _
    <XmlRoot(Namespace:="urn:getComboPuntosServiciosByClienteSubcliente")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _cliente As Cliente

#End Region

#Region "[PROPRIEDADES]"

        Public Property Cliente() As Cliente
            Get
                Return _cliente
            End Get
            Set(value As Cliente)
                _cliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace