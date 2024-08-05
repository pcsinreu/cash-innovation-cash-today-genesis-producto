Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.RecuperarBultosRemesa

    <XmlType(Namespace:="urn:RecuperarBultosRemesa")> _
    <XmlRoot(Namespace:="urn:RecuperarBultosRemesa")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Bultos As ObservableCollection(Of Clases.Bulto)

    End Class
End Namespace

