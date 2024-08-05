Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.CrearModificarElementos

    <XmlType(Namespace:="urn:CrearModificarElementos")> _
    <XmlRoot(Namespace:="urn:CrearModificarElementos")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property Documento As Clases.Documento

    End Class

End Namespace