Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de uma Remesa
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoRemesa
        <ValorEnum("NU")>
        Nuevo
        <ValorEnum("PE")>
        Pendiente
        <ValorEnum("PR")>
        Procesada
        <ValorEnum("AN")>
        Anulado
        <ValorEnum("EE")>
        Eliminado
        <ValorEnum("AS")>
        Asignada
        <ValorEnum("EC")>
        EnCurso
        <ValorEnum("EN")>
        EnviadoLegado
        <ValorEnum("SA")>
        EnviadoSaldos
        <ValorEnum("SU")>
        Sustituido
        <ValorEnum("DV")>
        Dividido
        <ValorEnum("ET")>
        EnTransito
        <ValorEnum("MD")>
        Modificada
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace