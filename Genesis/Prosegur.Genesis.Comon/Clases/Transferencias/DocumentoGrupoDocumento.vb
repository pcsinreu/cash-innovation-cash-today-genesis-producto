Namespace Clases.Transferencias

    <Serializable()>
    Public NotInheritable Class DocumentoGrupoDocumento
        Inherits BaseClase

        Public Property Identificador As String
        Public Property TipoSitio As Enumeradores.TipoSitio
        Public Property Tipo As String
        Public Property DescripcionSubCanal As String
        Public Property CorFormulario As Drawing.Color
        Public Property DescripcionFormulario As String
        Public Property DescripcionSector As String
        Public Property IdentificadorSector As String
        Public Property DescripcionSectorOrigenDestino As String
        Public Property IdentificadorSectorOrigenDestino As String
        Public Property CodigoComprobante As String
        Public Property NumeroExterno As String
        Public Property FechaHoraCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaHoraModificacion As DateTime
        Public Property UsuarioModificacion As String
        Public Property FechaPlanCertificacion As DateTime
        Public Property NoCertificar As Boolean
        Public Property CuentaSaldoOrigen As Cuenta
        Public Property CuentaSaldoDestino As Cuenta
        Public Property EsGeneracionF22 As Boolean

    End Class

End Namespace



