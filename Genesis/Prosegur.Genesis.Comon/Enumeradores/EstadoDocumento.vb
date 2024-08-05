Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de um documento
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoDocumento
        <ValorEnum("NU")>
        Nuevo
        <ValorEnum("EC")>
        EnCurso
        <ValorEnum("CF")>
        Confirmado
        <ValorEnum("AN")>
        Anulado
        <ValorEnum("AC")>
        Aceptado
        <ValorEnum("RC")>
        Rechazado
        <ValorEnum("SU")>
        Sustituido
        <ValorEnum("EE")>
        Eliminado
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace