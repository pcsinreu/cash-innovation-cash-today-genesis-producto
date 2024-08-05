Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.RecuperarDocumentosPorElemento

    <XmlType(Namespace:="urn:RecuperarDocumentosPorElemento")> _
    <XmlRoot(Namespace:="urn:RecuperarDocumentosPorElemento")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property EstadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento
        Public Property Elemento As Clases.Elemento

    End Class

End Namespace