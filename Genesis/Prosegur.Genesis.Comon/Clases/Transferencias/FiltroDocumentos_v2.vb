Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Documentos.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroDocumentos_v2

        Public Property CodigoDelegacion As String
        Public Property IdentificadorDelegacion As String

        ' Documento
        Public Property IdentificadorSector As String
        Public Property CodigoEmisor As String
        Public Property CodigoEstadoDocumentoElemento As String
        Public Property CodigoEstadoDocumento As String
        Public Property CodigoEstadoDocumentoIgual As Boolean
        Public Property CodigoComprobante As String
        Public Property FechaCreacion As Date
        Public Property CodigoSector As String
        Public Property CodigoPlanta As String

        ' Remesa
        Public Property CodigoExterno As String
        Public Property CodigoRuta As String
        Public Property CodigoEstadoRemesa As Comon.Enumeradores.EstadoRemesa
        Public Property IdentificadorExternoRemesa As String

        ' Bulto
        Public Property CodigoPrecintoBulto As String
        Public Property FechaTransporteDesde As Date
        Public Property FechaTransporteHasta As Date
        Public Property CodigoEstadoBulto As Comon.Enumeradores.EstadoBulto

        ' Parcial
    End Class

End Namespace
