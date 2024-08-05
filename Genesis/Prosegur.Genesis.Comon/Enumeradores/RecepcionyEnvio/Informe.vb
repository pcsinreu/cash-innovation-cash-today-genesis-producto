Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores.RecepcionyEnvio

    ''' <summary>
    ''' Enumeração das telas
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Informe

        <ValorEnum("rptElementoIndividualContenedor")>
        DocumentoContenedor

        <ValorEnum("rptElementoGrupoContenedor")>
        GrupoDocumentoContenedor

        <ValorEnum("rptDocumento")>
        Documento

        <ValorEnum("rptGrupoDocumento")>
        GrupoDocumento

        <ValorEnum("rptElementoGrupoSalidasRecorrido")>
        GrupoDocumentoRemesaExterna

        <ValorEnum("RECEPCION_RUTA")>
        RecepcionRuta

        <ValorEnum("rptLiberacionSalidasRecorrido")>
        LiberacionPedidoExterno

        <ValorEnum("rptTraspaseResponsabilidad")>
        TraspaseResponsabilidad

        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace