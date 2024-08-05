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
    Public Class ContenedorRespuesta

#Region "[PROPRIEDADES]"

        Public Property oidContenedor As String
        Public Property codTipoContenedor As String
        Public Property desTipoContenedor As String
        Public Property packModular As Boolean

        Public Property fechaHoraArmado As DateTime
        Public Property agrupaElementos As Boolean

        Public Property Precintos As System.Collections.ObjectModel.ObservableCollection(Of String)

        'Public Clientes As List(Of Cliente)

        'Public Canal As Canal

        'Public DetalleEfectivo As DetalleEfectivo

        ' Public Property Precintos As System.Collections.ObjectModel.ObservableCollection(Of String)

#End Region

    End Class
End Namespace