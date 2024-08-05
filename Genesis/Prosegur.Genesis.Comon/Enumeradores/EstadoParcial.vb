Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de uma Parcial
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoParcial
        <ValorEnum("NU")>
        Nuevo
        <ValorEnum("PE")>
        Pendiente
        <ValorEnum("PR")>
        Procesado
        <ValorEnum("AN")>
        Anulado
        <ValorEnum("EE")>
        Eliminado
        <ValorEnum("SA")>
        Salida
        <ValorEnum("SU")>
        Sustituido
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace