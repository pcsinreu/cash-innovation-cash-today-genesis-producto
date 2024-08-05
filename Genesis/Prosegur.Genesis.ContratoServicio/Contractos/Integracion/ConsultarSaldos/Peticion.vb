Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <XmlType(Namespace:="urn:ConsultarSaldos")> _
    <XmlRoot(Namespace:="urn:ConsultarSaldos")> _
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BasePeticion

        Public Property FechaHoraSaldo As DateTime
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoIsoDivisa As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPtoServicio As String
        Public Property Sectores As List(Of Sector)
        Public Property Canales As List(Of CanalPeticion)
        Public Property Filtros As Filtro

    End Class

End Namespace
