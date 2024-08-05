Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de um objeto
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoObjeto
        <ValorEnum("PE")>
        Pendiente
        <ValorEnum("PR")>
        Procesado
        <ValorEnum("PP")>
        Preparado
        <ValorEnum("NP")>
        NoPreparado
        <ValorEnum("IN")>
        Indiferente
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace