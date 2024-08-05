Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ' ***********************************************************************
    '  Modulo:  TipoMedioPago.vb
    '  Descripción: Enum definición TipoMedioPago
    ' ***********************************************************************


    <Serializable()>
    Public Enum TipoMedioPago
        <ValorEnum("codtipob")>
        Cheque
        <ValorEnum("codtipo")>
        OtroValor
        <ValorEnum("codtipoc")>
        Tarjeta
        <ValorEnum("codtipoa")>
        Ticket
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace
