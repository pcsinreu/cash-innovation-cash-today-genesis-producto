Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarSeguimientoElemento

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <XmlRoot(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <Serializable()> _
    Public Class ElementoRespuesta

#Region "[PROPRIEDADES]"

        Public Property oidElemento As String
        Public Property codTipoElemento As String
        Public Property codPrecinto As String

        Public Property codEstado As String

        Public Property Contenedor As ContenedorRespuesta
        Public Property Remesa As Remesa
        Public Property Documentos As List(Of Documento)


#End Region

    End Class
End Namespace