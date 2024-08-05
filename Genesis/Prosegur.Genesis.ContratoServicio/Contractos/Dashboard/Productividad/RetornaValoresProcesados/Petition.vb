Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.Dashboard.Productividad.RetornaValoresProcesados
    <XmlType(Namespace:="urn:RetornaValoresProcesados")> _
    <XmlRoot(Namespace:="urn:RetornaValoresProcesados")> _
    <Serializable()>
    Public Class Peticion

        Public Property CodigosDelegacion As List(Of String)
        Public Property CodigosSector As List(Of String)
        Public Property CodigosIsoDivisa As List(Of String)
        Public Property RangoFechaHora As RangoFecha
        Public Property Top As Integer

    End Class
End Namespace