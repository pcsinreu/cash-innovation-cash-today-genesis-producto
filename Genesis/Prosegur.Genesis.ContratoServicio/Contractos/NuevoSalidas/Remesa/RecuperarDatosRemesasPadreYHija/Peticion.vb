Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.RecuperarDatosRemesasPadreYHija

    <XmlType(Namespace:="urn:RecuperarDatosRemesasPadreYHija")> _
    <XmlRoot(Namespace:="urn:RecuperarDatosRemesasPadreYHija")> _
    <Serializable> _
    Public Class Peticion
        Inherits BasePeticion

        Public Property AgruparBultos As Boolean
        Public Property IdentificadoresRemesas As List(Of String)
        Public Property CodigoSubCanalATM As String
        Public Property CrearConfiguracionNivelSaldo As Boolean

    End Class

End Namespace
