Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeradores com os codigos de processo a serem gravados
    ''' na tabela GEPR_TINTEGRACION
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Enum CodigoProcesoIntegracion

        <ValorEnum("ENVIAR_MIF_AC_RC")>
        EnviarMifAceptadoRechazado

        <ValorEnum("ENVIAR_MIF_CF")>
        EnviarMifConfirmado

        <ValorEnum("ENVIAR_REMESA_CONTEO")>
        EnviarRemesaConteo

        <ValorEnum("GRABARDOCUMENTO")>
        GrabarDocumento

        <ValorEnum("ENVIAR_DATOS_SALDOS_PROFAT_TESOURO")>
        EnviarDatosSaldosProfatTesouro

        <ValorEnum("ENVIAR_DATOS_SALDOS_PROFAT_ATESORAMENTO")>
        EnviarDatosSaldosProfatAtesoramento

        <ValorEnum("ENVIAR_DATOS_SALDOS_PROFAT_SIN_VALORES")>
        EnviarDatosSaldosProfatSinValores

        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum

End Namespace