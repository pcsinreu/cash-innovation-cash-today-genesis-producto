Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores
    ''' <summary>
    ''' Enumeração dos possíveis estado de um bulto
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoBulto
        <ValorEnum("NU")>
        Nuevo
        <ValorEnum("CE")>
        Cerrado
        <ValorEnum("AB")>
        Aberto
        <ValorEnum("AN")>
        Anulado
        <ValorEnum("EE")>
        Eliminado
        <ValorEnum("SA")>
        Salida
        <ValorEnum("PE")>
        Pendiente
        <ValorEnum("PR")>
        Procesado
        <ValorEnum("EC")>
        EnCurso
        <ValorEnum("AS")>
        Asignado
        <ValorEnum("SU")>
        Sustituido
        <ValorEnum("ET")>
        EnTransito
        <ValorEnum("MD")>
        Modificado
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace