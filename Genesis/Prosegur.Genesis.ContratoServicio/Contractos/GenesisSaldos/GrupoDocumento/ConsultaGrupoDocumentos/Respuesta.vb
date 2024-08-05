Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.GrupoDocumento.ConsultaGrupoDocumentos

    <XmlType(Namespace:="urn:ConsultaGrupoDocumentos")> _
    <XmlRoot(Namespace:="urn:ConsultaGrupoDocumentos")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property GrupoDocumentos As ObservableCollection(Of Clases.GrupoDocumentos)

    End Class

End Namespace
