Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.CrearDocumento

    <XmlType(Namespace:="urn:CrearDocumento")> _
    <XmlRoot(Namespace:="urn:CrearDocumento")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorFormulario As String
        Public Property CodigoFormulario As String
        Public Property CaracteristicasFormulario As List(Of Enumeradores.CaracteristicaFormulario)

    End Class

End Namespace