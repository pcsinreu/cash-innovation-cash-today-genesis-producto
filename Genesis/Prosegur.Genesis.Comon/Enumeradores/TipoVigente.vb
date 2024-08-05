Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ' ***********************************************************************
    '  Modulo:  TipoUnidadMedida.vb
    '  Descripción: Enum definición TipoUnidadMedida
    ' ***********************************************************************


    <Serializable()>
    Public Enum TipoVigente
        <ValorEnum("0")>
        NoVigente

        <ValorEnum("1")>
        Vigente

        <ValorEnum("2")>
        Ambos

        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace
