Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.crearDocumentoReenvio

    <XmlType(Namespace:="urn:crearDocumentoReenvio")> _
    <XmlRoot(Namespace:="urn:crearDocumentoReenvio")> _
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BasePeticion

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>
        <XmlElement(IsNullable:=True)>
        Public Property movimiento As Movimiento

    End Class

End Namespace
