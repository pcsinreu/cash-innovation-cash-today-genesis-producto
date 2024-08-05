Namespace Clases.Abono
    <Serializable()>
    Public Class ReporteAbono
        Inherits BindableBase

        Public Property Identificador As String
        Public Property IdentificadorAbono As String
        Public Property CodigoSituacion As String
        Public Property Tipo As Genesis.Comon.Enumeradores.TipoReporte
        Public Property DesErrorEjecucion As String
        Public Property NombreArchivo As String

    End Class
End Namespace