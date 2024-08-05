Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMovimientos

    <XmlType(Namespace:="urn:RecuperarMovimientos")>
    <XmlRoot(Namespace:="urn:RecuperarMovimientos")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>

        Public Property Paginacion As Comon.Paginacion
        Public Property FiltrosAdicionales As Filtro
        Public Property FechaGestion As Fecha
        Public Property FechaCreacion As Fecha
        Public Property FechaAcreditacion As Fecha

        <DefaultValue("")>
        Public Property CodigoCliente As String

        <DefaultValue("")>
        Public Property CodigoSubCliente As String

        <DefaultValue("")>
        Public Property CodigoPuntoServicio As String

        <DefaultValue("")>
        Public Property CodigoDelegacion As String

        <DefaultValue("")>
        Public Property CodigoPlanta As String

        Public Property CodigosMaquinas As List(Of String)

        Public Property CodigosCanales As List(Of String)

        Public Property CodigosFormularios As List(Of String)

        Public Property CodigosDivisas As List(Of String)

    End Class

End Namespace
