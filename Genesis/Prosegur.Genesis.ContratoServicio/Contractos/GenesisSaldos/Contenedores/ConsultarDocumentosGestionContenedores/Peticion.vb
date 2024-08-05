Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarDocumentosGestionContenedores")> _
    <XmlRoot(Namespace:="urn:ConsultarDocumentosGestionContenedores")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property Documento As FiltroDocumento
        Public Property CodigoUsuario As String

#End Region

    End Class

End Namespace