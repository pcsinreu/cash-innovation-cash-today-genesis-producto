Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeradores com os codigos das aplicações a serem gravados
    ''' na tabela GEPR_TINTEGRACION
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Enum Aplicacion

        <ValorEnum("GenesisConteo")>
        GenesisConteo

        <ValorEnum("GenesisSalidas")>
        GenesisSalidas

        <ValorEnum("GenesisNuevoSaldos")>
        GenesisNuevoSaldos

        <ValorEnum("GenesisIAC")>
        GenesisIAC

        <ValorEnum("GenesisReportes")>
        GenesisReportes

        <ValorEnum("GenesisATM")>
        GenesisATM

        <ValorEnum("GenesisSaldos")>
        GenesisSaldos

        <ValorEnum("GenesisCargaPrevia")>
        GenesisCargaPrevia

        <ValorEnum("GenesisSupervisor")>
        GenesisSupervisor

        <ValorEnum("GenesisEmulador")>
        GenesisEmulador

        <ValorEnum("GenesisRecepcionEnvio")>
        GenesisRecepcionEnvio

        <ValorEnum("SOL")>
        SOL

        <ValorEnum("PROFAT")>
       PROFAT

        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace