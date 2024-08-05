Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de um documento
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum OrigenBusquedaDocumento
        <ValorEnum("A")>
        Ambos
        <ValorEnum("E")>
        Enviados
        <ValorEnum("R")>
        Recebidos
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace