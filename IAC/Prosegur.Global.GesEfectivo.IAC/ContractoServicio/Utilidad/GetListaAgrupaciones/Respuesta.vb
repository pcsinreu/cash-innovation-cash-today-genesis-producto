Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetListaAgrupaciones

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetListaAgrupaciones")> _
    <XmlRoot(Namespace:="urn:GetListaAgrupaciones")> _
    <Serializable()> _
Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _agrupaciones As AgrupacionColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Agrupaciones() As AgrupacionColeccion
            Get
                Return _agrupaciones
            End Get
            Set(value As AgrupacionColeccion)
                _agrupaciones = value
            End Set
        End Property

#End Region

    End Class
End Namespace