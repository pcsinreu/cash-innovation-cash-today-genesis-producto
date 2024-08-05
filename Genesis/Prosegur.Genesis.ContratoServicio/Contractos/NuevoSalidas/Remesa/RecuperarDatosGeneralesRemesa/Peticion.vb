Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.RecuperarDatosGeneralesRemesa

    <XmlType(Namespace:="urn:RecuperarDatosGeneralesRemesa")> _
    <XmlRoot(Namespace:="urn:RecuperarDatosGeneralesRemesa")> _
    <Serializable> _
    Public Class Peticion
        Inherits BasePeticion
        Public Property AgruparBultos As Boolean
        Public Property EsEstadoBulto As Boolean
        Public Property FechaServicioDesde As Nullable(Of DateTime)
        Public Property FechaServicioHasta As Nullable(Of DateTime)
        Public Property FechaHoraSalidaDesde As Nullable(Of DateTime)
        Public Property FechaHoraSalidaHasta As Nullable(Of DateTime)
        Public Property CodigosEstados As List(Of String)
        Public Property CodigosTiposMercancia As List(Of String)
        Public Property CodigosPuestos As List(Of String)
        Public Property CodigosTiposBulto As List(Of String)
        Public Property CodigosEmisores As List(Of String)
        Public Property CodigoRutaRemesa As String
        Public Property IdentificadorRemesa As String
        Public Property IdentificadoresRemesas As List(Of String)
        Public Property IdentificadorRemesaLegado As String
        Public Property IdentificadorBulto As String
        Public Property IdentificadorContenedor As String
        Public Property CodigoReciboRemesa As String
        Public Property ObtenerRemesaConRecibo As Boolean
        Public Property ObtenerRemesaSinRecibo As Boolean
        Public Property CodigoDelegacion As String
        Public Property CodigoPrecintoBulto As String
        Public Property CodigoClienteFacturacion As String
        Public Property CodigoClienteDestino As String
        Public Property CodigoSubClienteDestino As String
        Public Property CodigoPuntoServicioDestino As String
        Public Property CodigoATM As String
        Public Property CodigoCanal As String
        Public Property CodigoSubCanalATM As String
        Public Property CrearConfiguracionNivelSaldo As Boolean
        Public Property NumeroControleLegado As String
        Public Property BuscarRemesasConBultos As Enumeradores.RemesasConBultos
        Public Property BuscarRemesasConRuta As Enumeradores.RemesasConRuta
        Public Property EsPreparado As Nullable(Of Boolean)
        Public Property BuscarTodosBultosRemesa As Boolean
        Public Property CodigosSectores As List(Of String)
        Public Property TrabajaPorBulto As Boolean

    End Class

End Namespace
