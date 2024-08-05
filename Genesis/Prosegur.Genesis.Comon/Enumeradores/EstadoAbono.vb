Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de uma Parcial
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoAbono
        <ValorEnum("NU")>
        Nuevo
        <ValorEnum("EN")>
        EnCurso
        <ValorEnum("PR")>
        Procesado
        <ValorEnum("AN")>
        Anulado
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace