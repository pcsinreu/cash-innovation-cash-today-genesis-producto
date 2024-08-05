Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores
    ''' <summary>
    ''' Enumerador que corresponde a coluna EstadoLV do legado
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SituacionOT
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
        <ValorEnum("0")>
        Anulado
        <ValorEnum("1")>
        SinMovimiento
        <ValorEnum("2")>
        EnRuta
        <ValorEnum("3")>
        Concluido
    End Enum

End Namespace