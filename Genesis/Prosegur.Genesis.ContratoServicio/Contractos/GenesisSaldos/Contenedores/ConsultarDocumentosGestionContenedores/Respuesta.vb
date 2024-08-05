Imports System.Xml
Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores

Namespace GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarDocumentosGestionContenedores")> _
    <XmlRoot(Namespace:="urn:ConsultarDocumentosGestionContenedores")> _
    <Serializable()>
    Public Class Respuesta
        Inherits Prosegur.Genesis.Comon.BaseRespuesta

#Region "[PROPRIEDADES]"

        Public Property documentos As List(Of Comon.Documento)

#End Region

    End Class
End Namespace