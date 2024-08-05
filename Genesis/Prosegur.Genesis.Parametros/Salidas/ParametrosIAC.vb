Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ValorPosible

Namespace Salidas

    Public Class ParametrosIAC

        Public Property CodigoIsoDivisaLocal As String
        Public Property EstadoSistemaVideo2 As Boolean
        Public Property PortaSocketDVR As String
        Public Property VideoHost As String
        Public Property VideoIP As String
        Public Property VideoIP2 As String
        Public Property VideoModelo As List(Of ValorPosible)
        Public Property VideoModelo2 As List(Of ValorPosible)
        Public Property VideoNumeroCamara As String
        Public Property VideoNumeroCamara2 As String
        Public Property VideoPassword As String
        Public Property VideoPassword2 As String
        Public Property VideoUsuario As String
        Public Property VideoUsuario2 As String
        Public Property ControlaNumerarioPorBulto As Boolean
        Public Property PrepararYArmar As Boolean
        Public Property GestionaSaldoPorPuesto As Boolean
        Public Property TipoEtiquetaBulto As List(Of ValorPosible)
        Public Property TipoReciboRemesa As List(Of ValorPosible)
        Public Property GenerarNumeroReciboTransporte As Boolean
        Public Property ReciboTransporteSector As String
        Public Property ReciboTransporteTipo As String
        Public Property NombreImpEtiqueta As String
        Public Property MezclarDenominacion As Boolean
        Public Property MezclarBilleteMoneda As Boolean
        Public Property CodigoConfiguracionAutoDesglose As String
        Public Property CopiasReciboTransporte As List(Of ValorPosible)
        Public Property RazonSocialEmpresaF22 As String
        Public Property RazonSocialClienteServicioF22 As String
        Public Property LocalidadRemitoF22 As String
        Public Property PlantaConfeccionRemitoF22 As String
        Public Property CodSectorSalidas As String
        Public Property CondicionesTransporte As String
        Public Property DireccionDelegacion As String
        Public Property CodigoClienteProsegur As String
        Public Property CodigoSectorTesoro As String
        Public Property CodigoCanalSaldos As String
        Public Property CodigoSubCanalSaldos As String
        Public Property ObligarEntregarValoresPuesto As Boolean
        Public Property ContadoraBilleteModelo As List(Of ValorPosible)
        Public Property ContadoraMonedaModelo As List(Of ValorPosible)
        Public Property ContadoraBilleteLink As String
        Public Property ContadoraBilleteHost As String
        Public Property ContadoraMonedaLink As String
        Public Property ContadoraMonedaHost As String
        Public Property PortaSocketContadora As String
        Public Property GenerarDiferenciasEnLaCuentaProsegur As Boolean
        Public Property CuentaProsegurCodigoCliente As String
        Public Property CuentaProsegurCodigoSubCliente As String
        Public Property CuentaProsegurCodigoPuntoServicio As String
        Public Property CantRemesasCuadrar As Integer
        Public Property CantBultosCuadrar As Integer
        Public Property CuadrarPuesto As Boolean
        Public Property FiltrarDatosPorSectorPadre As Boolean
        Public Property TrabajaPorCuentaCero As Boolean
        Public Property CodigoClienteCuentaCero As String
        Public Property CodigoCanalCuentaCero As String
        Public Property CodigoSubCanalCuentaCero As String
        Public Property SaldoInicialSumaTodosPuestos As Boolean
        Public Property UrlServicioIntegracionSol As String
        Public Property TrabajarConCodigoBolsa As Boolean
        Public Property CrearPrecintoModulo As Boolean
        Public Property InformarCodigoCajetinATM As Boolean
        Public Property CodigoSubCanalDefecto As String
        Public Property CrearConfiguiracionNivelSaldo As String
        Public Property AlgoritmoDivisionBultos As List(Of ValorPosible)
        Public Property ConfirmarSolicitudFondosAutomatica As Boolean
        Public Property ReenvioCambioPrecintoLegado As Boolean
        Public Property ReenvioCambioPrecintoSol As Boolean
        Public Property AgruparBultos As Boolean
        Public Property ModelosImpresoras As List(Of ValorPosible)
        Public Property CaminoRedImpresora As String
    End Class

End Namespace