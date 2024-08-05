Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.RecuperarDatosGeneralesRemesa

    <XmlType(Namespace:="urn:RecuperarDatosGeneralesRemesa")> _
    <XmlRoot(Namespace:="urn:RecuperarDatosGeneralesRemesa")> _
    <Serializable> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Remesas As ObservableCollection(Of Clases.Remesa)

    End Class

End Namespace