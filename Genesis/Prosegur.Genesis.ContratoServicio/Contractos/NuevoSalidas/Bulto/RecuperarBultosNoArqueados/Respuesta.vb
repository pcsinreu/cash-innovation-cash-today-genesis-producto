Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.RecuperarBultosNoArqueados

    <XmlType(Namespace:="urn:RecuperarBultosNoArqueados")> _
    <XmlRoot(Namespace:="urn:RecuperarBultosNoArqueados")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Remesas As ObservableCollection(Of Clases.Remesa)

    End Class

End Namespace