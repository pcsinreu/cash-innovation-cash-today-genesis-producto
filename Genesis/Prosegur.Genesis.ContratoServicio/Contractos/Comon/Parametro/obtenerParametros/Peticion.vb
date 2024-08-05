Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Parametro.obtenerParametros

    <Serializable()> _
    <XmlType(Namespace:="urn:obtenerParametros")> _
    <XmlRoot(Namespace:="urn:obtenerParametros")> _
    Public Class Peticion
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BasePeticion

        Public Property codigoPais As String
        Public Property codigoDelegacion As String
        Public Property codigoAplicacion As String
        Public Property codigoHostPuesto As String
        Public Property codigoPuesto As String
        Public Property obtenerParametrosGenesis As Boolean
        Public Property obtenerParametrosReportes As Boolean
        Public Property codigosParametro As List(Of String)

    End Class

End Namespace
