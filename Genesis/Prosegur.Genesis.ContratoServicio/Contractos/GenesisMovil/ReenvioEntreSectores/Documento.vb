Namespace Contractos.GenesisMovil.ReenvioEntreSectores


    <Serializable()> _
    Public Class Documento

        Public Property CodigoFormulario As String
        Public Property FechaHoraGestion As String
        Public Property SectorOrigen As Sector
        Public Property SectorDestino As Sector
        Public Property PrecintosContenedores As List(Of String)
        Public Property CodigoUsuario As String
    End Class
End Namespace