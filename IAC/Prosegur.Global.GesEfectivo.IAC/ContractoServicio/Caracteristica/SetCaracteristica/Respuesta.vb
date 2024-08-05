Imports System.Xml.Serialization
Imports System.Xml

Namespace Caracteristica.SetCaracteristica

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 18/05/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetCaracteristica")> _
    <XmlRoot(Namespace:="urn:SetCaracteristica")> _
    <Serializable()> _
Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _caracteristicas As CaracteristicaRespuestaColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Caracteristicas() As CaracteristicaRespuestaColeccion
            Get
                Return _caracteristicas
            End Get
            Set(value As CaracteristicaRespuestaColeccion)
                _caracteristicas = value
            End Set
        End Property

#End Region

    End Class
End Namespace