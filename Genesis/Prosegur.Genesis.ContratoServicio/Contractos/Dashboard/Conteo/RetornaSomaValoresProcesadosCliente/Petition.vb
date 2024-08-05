Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente
    <XmlType(Namespace:="urn:RetornaSomaValoresProcesadosCliente")> _
    <XmlRoot(Namespace:="urn:RetornaSomaValoresProcesadosCliente")> _
    <Serializable()>
    Public Class Peticion

        Public Property CodigosDelegacion As List(Of String)
        Public Property CodigosSector As List(Of String)
        Public Property CodigosIsoDivisa As List(Of String)
        Public Property RangoFechaHora As RangoFecha
        Public Property Top As Integer

    End Class
End Namespace