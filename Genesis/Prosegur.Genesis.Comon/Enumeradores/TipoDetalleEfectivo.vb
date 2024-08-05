Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores
    Public Enum TipoDetalleEfectivo
        <ValorEnum("A")>
        Mezcla

        <ValorEnum("B")>
        Billete

        <ValorEnum("M")>
        Moneda

        'Esse valor Enum foi adicionado para resolver o item jira-2159. Não deveria existir na base de dados o valor "T".
        <ValorEnum("T")>
        Total

        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum
End Namespace
