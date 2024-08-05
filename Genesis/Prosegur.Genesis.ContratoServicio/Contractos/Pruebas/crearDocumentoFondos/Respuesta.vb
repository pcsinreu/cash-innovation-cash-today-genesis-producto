Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Pruebas.crearDocumentoFondos

    <XmlType(Namespace:="urn:pruebasCrearDocumentoFondos")> _
    <XmlRoot(Namespace:="urn:pruebasCrearDocumentoFondos")> _
    <Serializable()>
    Public Class Respuesta
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BaseRespuesta

        Public Property peticiones As List(Of Contractos.Integracion.crearDocumentoFondos.Peticion)

        Public Property respuestas As List(Of Contractos.Integracion.crearDocumentoFondos.Respuesta)

    End Class

End Namespace
