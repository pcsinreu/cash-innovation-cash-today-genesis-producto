Imports Prosegur.Genesis.Comon.Atributos
Namespace Enumeradores

    <Serializable()>
    Public Enum TipoServico
        ''' <summary>
        ''' Conteo
        ''' </summary>
        ''' <remarks></remarks>
        <ValorEnum("01")>
        Procesar

        <ValorEnum("02")>
        Clasificacion

        <ValorEnum("03")>
        Preparacion

        <ValorEnum("04")>
        ATM

        ''' <summary>
        ''' Custodia
        ''' </summary>
        ''' <remarks></remarks>
        <ValorEnum("05")>
        Almacen

        <ValorEnum("06")>
        Pernoche

        <ValorEnum("ND")>
        NoDefinido

        <ValorEnum("NO-DEFINIDO")>
        NoEncontrado
    End Enum

End Namespace
