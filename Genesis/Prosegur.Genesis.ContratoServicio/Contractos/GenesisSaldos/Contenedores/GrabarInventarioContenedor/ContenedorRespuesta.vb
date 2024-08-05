Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.GrabarInventarioContenedor

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:GrabarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:GrabarInventarioContenedor")> _
    <Serializable()> _
    Public Class ContenedorRespuesta

#Region "[PROPRIEDADES]"

        Public Property codTipoContenedor As String
        Public Property desTipoContenedor As String
        Public Property fechaArmado As DateTime
        Public Property codPrecinto As String

        Public DetalleEfectivo As List(Of DetalleEfectivo)

        Public Property Sector As SectorRespuesta
        Public Property clientes As List(Of Cliente)
        Public Property canales As List(Of Canal)

#End Region

    End Class
End Namespace