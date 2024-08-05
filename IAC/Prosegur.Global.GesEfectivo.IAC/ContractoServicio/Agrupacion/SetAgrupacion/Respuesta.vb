Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.SetAgrupaciones

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetAgrupaciones")> _
    <XmlRoot(Namespace:="urn:SetAgrupaciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _RespuestaAgrupaciones As RespuestaAgrupacionColeccion

#End Region

#Region "[Propriedades]"

        Public Property RespuestaAgrupaciones() As RespuestaAgrupacionColeccion
            Get
                Return _RespuestaAgrupaciones
            End Get
            Set(value As RespuestaAgrupacionColeccion)
                _RespuestaAgrupaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace