Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Documento.AperturarElemento

    <XmlType(Namespace:="urn:AperturarElemento")> _
    <XmlRoot(Namespace:="urn:AperturarElemento")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property IdentificadorDocumento As String
        Public Property CaracteristicasFormulario As List(Of Enumeradores.CaracteristicaFormulario)
        Public Property Usuario As String
    End Class

End Namespace