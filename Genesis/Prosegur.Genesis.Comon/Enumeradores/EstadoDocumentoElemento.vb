Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores
    ''' <summary>
    ''' Enumeração dos possíveis estado do documento elemento.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoDocumentoElemento
        <ValorEnum("N")>
        NaoDefinido
        <ValorEnum("H")>
        Historico
        <ValorEnum("T")>
        EnTransito
        <ValorEnum("C")>
        Concluido
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace