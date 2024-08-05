Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.getComboPuntosServiciosByClientesSubclientes

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 09/11/2012
    ''' </history>
    <XmlType(Namespace:="urn:getComboPuntosServiciosByClientesSubclientes")> _
    <XmlRoot(Namespace:="urn:getComboPuntosServiciosByClientesSubclientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _clientes As ClienteColeccion

        Public Property Clientes As ClienteColeccion
            Get
                Return _clientes
            End Get
            Set(value As ClienteColeccion)
                _clientes = value
            End Set
        End Property

    End Class

End Namespace

