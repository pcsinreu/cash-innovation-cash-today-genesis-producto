Imports System.Xml.Serialization
Imports System.Xml

Namespace Proceso.GetProceso

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetProceso")> _
    <XmlRoot(Namespace:="urn:GetProceso")> _
    <Serializable()> _
Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIÁVEIS]"

        Private _procesos As ProcesoColeccion
        Private _procesosSapr As ProcesoSaprColeccion
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

        Public Property ProcesosSapr() As ProcesoSaprColeccion
            Get
                Return _procesosSapr
            End Get
            Set(value As ProcesoSaprColeccion)
                _procesosSapr = value
            End Set
        End Property

#End Region
    End Class
End Namespace
