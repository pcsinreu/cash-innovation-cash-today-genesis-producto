Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores

    ''' <summary>
    ''' Enumeração dos possíveis estados a serem gravados
    ''' na tabela GEPR_TINTEGRACION
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoIntegracion

        <ValorEnum("EV")>
        Enviado

        <ValorEnum("NE")>
        NoEnviado

        <ValorEnum("OK")>
        OK
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido

        <ValorEnum("AB")>
        Abierto

        <ValorEnum("PD")>
        Pendiente

        <ValorEnum("CE")>
        Cerrado

        <ValorEnum("MD")>
        Modificado

    End Enum
    ''' <summary>
    ''' Enumeração dos possíveis estados a serem gravados
    ''' na tabela GEPR_TINTEGRACION_DETALLE
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EstadoIntegracionDetalle

        <ValorEnum("")>
        Vacio

        <ValorEnum("REENVIO_AUTO")>
        ReenvioAuto

        <ValorEnum("REENVIO_MANUAL")>
        ReenvioManual

        <ValorEnum("ENV_EXITO")>
        EnvioExito

        <ValorEnum("ENV_FALLO")>
        EnvioFallo

    End Enum

End Namespace