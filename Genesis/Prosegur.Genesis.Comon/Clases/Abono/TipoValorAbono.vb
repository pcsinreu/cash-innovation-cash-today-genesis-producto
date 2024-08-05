Imports Prosegur.Genesis.Comon.Atributos
Imports System.ComponentModel

Namespace Clases.Abono
    Public Enum TipoValorAbono
        <ValorEnum("DE")>
        <Description("Declarados")>
        Declarados
        <ValorEnum("CO")>
        <Description("Contados")>
        Contados
        <ValorEnum("NO-DEFINIDO")>
        <Description("NoDefinido")>
        NoDefinido
        '<ValorEnum("DI")>
        '<Description("Disponibles")>
        'Disponibles
        '<ValorEnum("ND")>
        '<Description("No Disponibles")>
        'NoDisponibles
        
    End Enum
End Namespace