Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Xml

Namespace Documento.CrearDocumento

    <XmlType(Namespace:="urn:CrearDocumento")> _
    <XmlRoot(Namespace:="urn:CrearDocumento")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property TipoMovimiento As Enumeradores.TipoMovimiento = Nothing
        Public Property CodigoDelegacion As String = String.Empty
        Public Property gestionaPorBulto As Boolean?

    End Class

End Namespace