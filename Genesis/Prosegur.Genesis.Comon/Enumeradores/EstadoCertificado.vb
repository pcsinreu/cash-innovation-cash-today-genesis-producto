Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de um Contenedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoCertificado
        <ValorEnum("CO")>
        Consulta
        <ValorEnum("PC")>
        ProvisionalConCierre
        <ValorEnum("PS")>
        ProvisionalSinCierre
        <ValorEnum("DE")>
        Definitivo
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace