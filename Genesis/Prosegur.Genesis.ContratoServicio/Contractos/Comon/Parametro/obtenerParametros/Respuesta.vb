Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Parametro.obtenerParametros

    <Serializable()> _
    <XmlType(Namespace:="urn:obtenerParametros")> _
    <XmlRoot(Namespace:="urn:obtenerParametros")> _
    Public Class Respuesta
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BaseRespuesta

        Public Property parametros As List(Of Clases.Parametro)

    End Class

End Namespace

