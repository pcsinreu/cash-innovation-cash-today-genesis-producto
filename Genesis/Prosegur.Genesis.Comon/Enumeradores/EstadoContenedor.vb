Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de um Contenedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoContenedor
        <ValorEnum("NU")>
        Nuevo
        <ValorEnum("PE")>
        Pendiente
        <ValorEnum("PR")>
        Procesado
        <ValorEnum("AN")>
        Anulado
        <ValorEnum("AR")>
        Armado
        <ValorEnum("ET")>
        EnTransito
        <ValorEnum("DR")>
        Desarmado
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido

    End Enum

End Namespace