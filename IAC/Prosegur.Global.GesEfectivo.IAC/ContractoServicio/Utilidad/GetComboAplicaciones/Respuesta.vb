Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.getComboAplicaciones

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:getComboAplicaciones")> _
    <XmlRoot(Namespace:="urn:getComboAplicaciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _Aplicaciones As AplicacionColeccion

#End Region

#Region "[PROPRIEDADE]"
        Public Property Aplicaciones() As AplicacionColeccion
            Get
                Return _Aplicaciones
            End Get
            Set(value As AplicacionColeccion)
                _Aplicaciones = value
            End Set
        End Property
#End Region

    End Class
End Namespace