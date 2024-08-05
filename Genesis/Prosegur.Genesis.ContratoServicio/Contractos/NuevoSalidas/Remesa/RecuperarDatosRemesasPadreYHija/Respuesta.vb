Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.RecuperarDatosRemesasPadreYHija

    <XmlType(Namespace:="urn:RecuperarDatosRemesasPadreYHija")> _
    <XmlRoot(Namespace:="urn:RecuperarDatosRemesasPadreYHija")> _
    <Serializable> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Remesas As ObservableCollection(Of Clases.Remesa)

    End Class

End Namespace