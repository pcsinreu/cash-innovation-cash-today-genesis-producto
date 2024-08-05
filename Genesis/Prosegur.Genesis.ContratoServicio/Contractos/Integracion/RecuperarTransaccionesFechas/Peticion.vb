Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransaccionesFechas

    <XmlType(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BasePeticion

        Public Property FechaGestion As Comon.Fecha
        Public Property FechaCreacion As Comon.Fecha
        Public Property FechaAcreditacion As Comon.Fecha
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPtoServicio As String
        Public Property CodigoTipoPlanificacion As String
        Public Property Canales As List(Of Canal)
        Public Property Sectores As List(Of Sector)
        Public Property Divisas As List(Of Divisa)
        Public Property Filtros As Filtro
        Public Property Formulario As List(Of Formulario)

    End Class

End Namespace
