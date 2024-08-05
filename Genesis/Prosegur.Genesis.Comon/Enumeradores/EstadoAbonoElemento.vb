Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estado de uma Parcial
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoAbonoElemento
        <ValorEnum("A")>
        Abonado
        <ValorEnum("NA")>
        NoAbonado
        <ValorEnum("AD")>
        AbonadoConDiferencias
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
        <ValorEnum("PA")>
        ParcialmenteAbonado
    End Enum

End Namespace

