Imports System.Xml.Serialization
Imports System.Xml

Namespace Proceso.GetProcesoDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetProcesoDetail")> _
    <XmlRoot(Namespace:="urn:GetProcesoDetail")> _
    <Serializable()> _
Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _procesos As ProcesoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Procesos() As ProcesoColeccion
            Get
                Return _procesos
            End Get
            Set(value As ProcesoColeccion)
                _procesos = value
            End Set
        End Property

#End Region

    End Class

End Namespace