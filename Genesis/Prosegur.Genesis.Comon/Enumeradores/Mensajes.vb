Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores.Mensajes

    <Serializable()>
    Public Enum Tipo
        <ValorEnum("0")>
        Exito
        <ValorEnum("1")>
        Alerta
        <ValorEnum("2")>
        Error_Negocio
        <ValorEnum("3")>
        Error_Aplicacion
    End Enum

    Public Enum Contexto
        <ValorEnum("00")>
        Genesis
        <ValorEnum("01")>
        Saldos
        <ValorEnum("02")>
        IAC
        <ValorEnum("03")>
        Reportes
        <ValorEnum("04")>
        Integraciones
        <ValorEnum("05")>
        Infraestructura
        <ValorEnum("06")>
        Job
    End Enum

    Public Enum Funcionalidad
        'Inicio de Enumeradores de integracion
        <ValorEnum("001")>
        General
        <ValorEnum("002")>
        MarcarMovimientos
        <ValorEnum("003")>
        RecuperarPlanificaciones
        <ValorEnum("004")>
        RecuperarMAEs
        <ValorEnum("005")>
        RecuperarMovimientos
        <ValorEnum("006")>
        RecuperarSaldos
        <ValorEnum("007")>
        ConfigurarMaquinas
        <ValorEnum("008")>
        ConfigurarClientes
        <ValorEnum("009")>
        RecuperarClientes
        <ValorEnum("010")>
        RelacionarMovimientosPeriodos
        <ValorEnum("011")>
        AltaMovimientosProvisionEfectivo
        <ValorEnum("012")>
        AltaMovimientosAcreditaciones
        <ValorEnum("013")>
        AltaMovimientosShipOut
        <ValorEnum("014")>
        AltaMovimientosRecuento
        <ValorEnum("015")>
        AltaMovimientosCierreFacturacion
        <ValorEnum("016")>
        RecuperarPaises
        <ValorEnum("017")>
        ModificarMovimientos
        <ValorEnum("018")>
        EnviarDocumentos
        <ValorEnum("019")>
        AltaMovimientosCashIn
        <ValorEnum("020")>
        AltaMovimientosCashOut
        <ValorEnum("021")>
        AltaMovimientosAjuste
        <ValorEnum("022")>
        ModificarPeriodos
        <ValorEnum("025")>
        RecuperarSaldosPeriodos
        <ValorEnum("026")>
        AltaMovimientosBalance
        <ValorEnum("027")>
        AltaMovimientosShipIn
        <ValorEnum("028")>
        AltaMovimientosMoveOut
        <ValorEnum("029")>
        AltaMovimientosMoveIn
        <ValorEnum("030")>
        RecuperarMAEsPlanificadas
        <ValorEnum("031")>
        RecuperarSaldosHistorico
        <ValorEnum("032")>
        ConfigurarAcuerdosServicio
        <ValorEnum("033")>
        RecuperarSaldosAcuerdo
        <ValorEnum("034")>
        ConfirmarPeriodos
        <ValorEnum("035")>
        ReconfirmarPeriodos
        'Fin de enumeradores de integración

        '-------------------------------------------

        'Enumeradores de infraestructura
        <ValorEnum("001")>
        RecuperarInformacionesVersion
        <ValorEnum("002")>
        RecuperaDatosEntradaMovimientos

        'Enumeradores de Job
        <ValorEnum("001")>
        ActualizarFechaCalculos
        <ValorEnum("002")>
        ActualizarSaldosHistorico
        <ValorEnum("003")>
        ActualizarSaldosAcuerdo
        <ValorEnum("004")>
        EnviarNotificaciones
        <ValorEnum("005")>
        ActualizarPeriodos
        <ValorEnum("007")>
        Depuracion
        <ValorEnum("008")>
        NotificarMovimientosNoAcreditados
        <ValorEnum("006")>
        GenerarPeriodos

    End Enum

    Public Enum FuncionalidadGenesis
        <ValorEnum("001")>
        General
        <ValorEnum("002")>
        GenericoCuentas
        <ValorEnum("003")>
        GenericoDivisa
        <ValorEnum("004")>
        GenericoTermino
        <ValorEnum("005")>
        GenericoFechaHora
    End Enum

End Namespace
