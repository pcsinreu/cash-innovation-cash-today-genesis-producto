Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.ImprimirElemento

    <XmlType(Namespace:="urn:ImprimirElemento")> _
    <XmlRoot(Namespace:="urn:ImprimirElemento")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property IdentificadorDocumento As String
        Public Property CaracteristicasFormulario As List(Of Enumeradores.CaracteristicaFormulario)
        Public Property Usuario As String
    End Class

End Namespace