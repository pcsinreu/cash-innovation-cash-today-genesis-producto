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
    Public Class Contenedor

#Region "[PROPRIEDADES]"
        'Public Property codTipoContenedor As String
        'Public Property desTipoContenedor As String
        'Public Property fechaArmado As DateTime
        'Public Property diasArmado As Integer
        'Public Property fechaVencimiento As DateTime
        'Public Property diasVencido As Integer
        '' Public Property alertasVencimento As List(Of AlertaVencimento)
        'Public Property precintos As System.Collections.ObjectModel.ObservableCollection(Of String)
        Public Property sector As Sector
        'Public Property clientes As List(Of Cliente)
        'Public Property canales As List(Of Canal)
        ' Public Property detEfectivo As List(Of DetalleEfectivo)
#End Region

    End Class
End Namespace