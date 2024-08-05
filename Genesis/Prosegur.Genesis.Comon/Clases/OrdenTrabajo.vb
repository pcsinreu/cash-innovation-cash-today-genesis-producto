Imports System.Collections.ObjectModel

Namespace Clases

    <Serializable()>
    Public NotInheritable Class OrdenTrabajo
        Inherits BaseClase

        Public Property OidOT As String
        Public Property BolSinMovimiento As Boolean
        Public Property CodigoSecuencia As Integer
        Public Property CodigoServicio As String
        Public Property CodigoTipoOT As String
        Public Property DescripcionTipoOT As String
        Public Property DescripcionTipoOTCorta As String
        Public Property CodigoTipoServicioOT As String
        Public Property CodigoClienteFacturado As String
        Public Property CodigoClienteSaldo As String
        Public Property DescripcionClienteSaldo As String
        Public Property DescripcionClienteFacturado As String
        Public Property CodigoClienteLV As String
        Public Property DescripcionClienteLV As String
        Public Property InicioProgramado As DateTime
        Public Property FinProgramado As DateTime
        Public Property PuntoServicioOrigen As PuntoServicioOT
        Public Property PuntoServicioDestino As PuntoServicioOT
        Public Property SituacionOT As Enumeradores.TipoSituacionOT
        Public Property Remesas As Prosegur.Global.Saldos.ContractoServicio.IngresoRemesasNuevo.Remesas
        Public Property Documentos As ObservableCollection(Of Documento)
        Public Property BolAnulada As Boolean
        Public Property BolEnvioProcesoGE As Boolean
        Public Property CodigoCanal As String
        Public Property DescripcionCanal As String
        Public Property CodigoSubCanal As String
        Public Property DescripcionSubCanal As String
        Public Property BolDeshalibitado As Boolean
        Public Property CodigoProducto As String
        Public Property DescripcionProducto As String
        Public Property CodigoClienteSaldoSeleccionado As String
        Public Property DescripcionClienteSaldoSeleccionado As String
        Public Property CodigoSubClienteSaldoSeleccionado As String
        Public Property DescripcionSubClienteSaldoSeleccionado As String
        Public Property CodigoPtoServicioSaldoSeleccionado As String
        Public Property DescripcionPtoServicioSaldoSeleccionado As String
        Public Property DocumentosSOL As List(Of SOL.DocumentoSOL)

        Public Property CodigoSituacionOT As String
        Public Property DescripcionSituacionOT As String
        Public Property EsTransito As Boolean
    End Class

End Namespace