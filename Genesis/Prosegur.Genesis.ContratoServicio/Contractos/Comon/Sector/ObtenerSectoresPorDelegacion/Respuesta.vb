Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Sector.ObtenerSectoresPorDelegacion

    <XmlType(Namespace:="urn:ObtenerSectoresPorDelegacion")> _
    <XmlRoot(Namespace:="urn:ObtenerSectoresPorDelegacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property Sectores As ObservableCollection(Of Clases.Sector)

#End Region

    End Class

End Namespace