Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ' ***********************************************************************
    '  Modulo:  TipoUnidadMedida.vb
    '  Descripción: Enum definición TipoUnidadMedida
    ' ***********************************************************************


    <Serializable()>
    Public Enum TipoUnidadMedida
        <ValorEnum("0")>
        Billete

        <ValorEnum("1")>
        Moneda

        <ValorEnum("2")>
        MedioPago

        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace
