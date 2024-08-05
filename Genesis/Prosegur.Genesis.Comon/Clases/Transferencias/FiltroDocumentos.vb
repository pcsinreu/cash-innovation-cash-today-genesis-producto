Namespace Clases.Transferencias
    ''' <summary>
    ''' Classe que representa o Filtro de Documentos.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroDocumentos
        Inherits BaseClase

        Public Property IdentificadorSector As String
        Public Property TipoSitioDocumento As Enumeradores.TipoSitio
        Public Property EstadoDocumento As List(Of Enumeradores.EstadoDocumento)
        Public Property NumeroComprovanteDesde As String
        Public Property NumeroComprovanteHasta As String
        Public Property FechaCreacionDesde As DateTime
        Public Property FechaCreacionHasta As DateTime
        Public Property PorDisponibilidad As Boolean?
        Public Property NumerosExternos As List(Of String)
        Public Property NumerosComprobantes As List(Of String)
        Public Property Sectores As List(Of Clases.Sector)
        Public Property Delegacion As Clases.Delegacion
        Public Property Clientes As List(Of Clases.Cliente)
        Public Property FechaPlanCertificacionDesde As DateTime
        Public Property FechaPlanCertificacionHasta As DateTime
        Public Property IdentificadorTipoDocumento As String
        Public Property BolIncluirDocSinFechaPlan As Boolean
        Public Property BolIncluirDocNoCertificar As Boolean
        'Public Property ConsiderarSectoresHijos As Boolean

    End Class
End Namespace