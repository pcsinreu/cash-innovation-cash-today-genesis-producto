Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.ActualizarRemesasSalidasSaldos

    <XmlType(Namespace:="urn:ActualizarRemesasSalidasSaldos")> _
    <XmlRoot(Namespace:="urn:ActualizarRemesasSalidasSaldos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadoresRemesas As List(Of String)
        Public Property IdentificadoresRemesasSaldos As List(Of String)
        Public Property CodigoRuta As String
        Public Property CodigoSecuencia As String
        Public Property Comentario As String
        Public Property FechaSalida As DateTime
        Public Property FechaServicio As DateTime
        Public Property NelControlLegado As String
        Public Property NelPedidoLegado As Integer?
        Public Property IdentificadorOT As String
        Public Property CantidadSolicitud As Integer

    End Class

End Namespace