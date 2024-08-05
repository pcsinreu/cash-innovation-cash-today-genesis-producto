Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Conteo.Remesa.RetornaCantidadBilletesUltimaHora

    <XmlType(Namespace:="urn:RetornaCantidadBilletesUltimaHora")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadBilletesUltimaHora")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta
        Public Property CantidadBilletesUltimaHora As Integer
        Public Property HoraInicial As DateTime
        Public Property HoraFinal As DateTime
        Public Property MaxRangoVerde
        Public Property MaxRangoAmarillo
        Public Property MaxRangoRojo
    End Class

End Namespace