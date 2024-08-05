Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresCliente

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedoresCliente")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresCliente")> _
    <Serializable()> _
    Public Class ContenedorRespuesta

#Region "[PROPRIEDADES]"

        Public Property codTipoContenedor As String
        Public Property codEstadoContenedor As String
        Public Property fechaHoraArmado As DateTime
        Public Property Precintos As System.Collections.ObjectModel.ObservableCollection(Of String)
        Public Property Sectores As List(Of Sector)
        Public Property Canais As List(Of Canal)
        Public Property DetalheEfectivo As List(Of DetalleEfectivo)
        Public Property DetalheMedioPago As List(Of DetalleMedioPago)

#End Region

    End Class
End Namespace