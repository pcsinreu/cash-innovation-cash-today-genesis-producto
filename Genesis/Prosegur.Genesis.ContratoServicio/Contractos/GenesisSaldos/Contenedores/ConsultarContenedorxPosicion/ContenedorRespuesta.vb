Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedorxPosicion

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedorxPosicion")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedorxPosicion")> _
    <Serializable()> _
    Public Class ContenedorRespuesta

#Region "[PROPRIEDADES]"
        Public Property identificador As String
        Public Property codTipoContenedor As String
        Public Property desTipoContenedor As String
        Public Property fechaArmado As DateTime
        Public Property fechaArmadoMobile As String
        Public Property diasArmado As Integer
        Public Property fechaVencimiento As DateTime
        Public Property diasVencido As Integer
        Public Property precintos As List(Of Precinto)
        Public Property Sector As SectorRespuesta
        Public Property cliente As Cliente
        Public Property canal As Canal
        Public Property detEfectivo As List(Of DetalleEfectivo)

#End Region

    End Class
End Namespace