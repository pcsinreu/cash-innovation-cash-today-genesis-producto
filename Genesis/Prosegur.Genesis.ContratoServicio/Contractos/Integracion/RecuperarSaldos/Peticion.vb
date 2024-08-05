Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos
    <XmlType(Namespace:="urn:Recuperarsaldos")>
    <XmlRoot(Namespace:="urn:Recuperarsaldos")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest
        Public Property Paginacion As Comon.Paginacion
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPtoServicio As String
        Public Property CodigoDivisa As String
        Public Property FechaGestion As DateTime?
        Public Property FechaCreacion As DateTime?
        Public Property CodigoMaquinas As List(Of String)
        Public Property CodigoCanales As List(Of String)
        Public Property CodigoSubCanales As List(Of String)
        Public Property Opciones As Opciones

    End Class

End Namespace