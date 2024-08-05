Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Collections.ObjectModel

Namespace Documento.GrabaryReenviarGrupoDocumentos

    <XmlType(Namespace:="urn:GrabaryReenviarGrupoDocumentos")> _
    <XmlRoot(Namespace:="urn:GrabaryReenviarGrupoDocumentos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty
        Public Property TipoMovimiento As Enumeradores.TipoMovimiento = Nothing
        Public Property Documentos As ObservableCollection(Of Clases.Documento) = Nothing
        Public Property SectorDestino As Clases.Sector = Nothing
        Public Property CodigoUsuario As String = String.Empty
        Public Property GrabarSaldosI As Boolean = False
        Public Property UtilizarReglaAutomata As Boolean = False
        Public Property gestionaPorBulto As Boolean?
        Public Property configNivelDetalle As String = String.Empty
        Public Property codigoComprovanteBasadoEnReglas As Boolean?

    End Class

End Namespace