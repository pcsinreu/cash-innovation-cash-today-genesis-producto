Imports System.Xml.Serialization

Namespace Clases.Abono
    <Serializable()>
    <XmlType(Namespace:="urn:Abono")> _
    <XmlRoot(Namespace:="urn:Abono")> _
    Public Class Abono
        Public Sub New()
            Me.AbonosValor = New List(Of AbonoValor)()
            Me.DatosReporte = New List(Of ReporteAbono)()
            Me.Fecha = DateTime.Now
            Me.FechaHoraCreacion = DateTime.Now
            Me.FechaHoraModificacion = DateTime.Now
            Me.TipoAbono = Enumeradores.TipoAbono.NoDefinido
            Me.TipoValor = TipoValorAbono.NoDefinido
            Me.CodigoEstado = Enumeradores.EstadoAbono.Nuevo
            Me.Bancos = New List(Of AbonoInformacion)()
            Me.SnapshotsAbonoSaldo = New List(Of AbonoValor)()
        End Sub

        Public Property Identificador As String
        Public Property Bancos As List(Of AbonoInformacion)
        Public Property Fecha As Date
        Public Property FechaFormatada As String
        Public Property TipoAbono As Enumeradores.TipoAbono
        Public Property TipoValor As TipoValorAbono
        Public Property CodigoEstado As Enumeradores.EstadoAbono
        Public Property Codigo As String
        Public Property Delegacion As Comon.Clases.Delegacion
        Public Property FechaHoraCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaHoraModificacion As DateTime
        Public Property UsuarioModificacion As String
        Public Property AbonosValor As List(Of AbonoValor)
        Public Property DatosReporte As List(Of ReporteAbono)
        Public Property CrearDocumentoPases As Boolean
        Public Property IdentificadorGrupoDocumento As String
        Public Property SnapshotsAbonoSaldo As List(Of AbonoValor)
        Public Property IdenficadorGrupoDocumento As String

    End Class
End Namespace