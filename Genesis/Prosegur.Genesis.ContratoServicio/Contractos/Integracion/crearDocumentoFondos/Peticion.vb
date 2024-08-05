Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

Namespace Contractos.Integracion.crearDocumentoFondos

    <XmlType(Namespace:="urn:crearDocumentoFondos")> _
    <XmlRoot(Namespace:="urn:crearDocumentoFondos")> _
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BasePeticion

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>
        <XmlElement(IsNullable:=True)>
        Public Property movimiento As Comon.Movimiento

        Public Function ToXML() As String
            Dim sw1 = New StringWriter()
            Dim xs1 As New XmlSerializer(GetType(Peticion))
            xs1.Serialize(New XmlTextWriter(sw1), Me)
            Return sw1.ToString
        End Function

    End Class

End Namespace
