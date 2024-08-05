Public Class Constantes

    Public Const CodigoTerminoTipoFormato As String = "TF05TipoFormato"

    'Constante com o nome do arquivo de log que é gerado
    Public Const NombreArchivoLog As String = "Log.txt"

    'Constante com o nome do arquivo de log que é gerado para gravar remessas
    Public Const Documento_GrabarRemesas_NombreArchivoLog As String = "Documento_GrabarRemesas_Log.txt"

    'CODIGOS DE PRODUCTOS DE PROSEGUR
    Public Const CODIGO_PRODUCTO_FECHA_VALOR As String = "PR00117"
    Public Const CODIGO_PRODUCTO_TRANSACCION As String = "PR00160"

    Public Const TipoMovimientoIngreso As String = "+"
    Public Const TipoMovimientoEgreso As String = "-"
    Public Const TipoMovimientoSinMovimiento As String = "0"

    'TIPOS DE DOCUMENTOS
    'REGISTRACION DE PROCESOS
    Public Const CODIGO_TIPO_DOCUMENTO_REGPROC As String = "REGPROC"
    'ACTAS DE PROCESO
    Public Const CODIGO_TIPO_DOCUMENTO_ACTA As String = "ACTAS"

    Public Const CodigoSectorLegado As String = "CODSECTORLEGADO"

    Public Const OPERACION_PRUEBA_DVR As String = "OPERACION_PRUEBA_DVR"

    Public Const GENESIS_SALDOS_URL_DOCUMENTO_IMPRESION As String = "~/Pantallas/DocumentoImpresion.aspx"

    Public Const CODIGO_LISTA_TIPO_TRABAJO As String = "01"
    Public Const CODIGO_LISTA_TIPO_FORMATO As String = "05"
    Public Const CODIGO_LISTA_TIPO_SERVICIO As String = "05"

    Public Const CODIGO_PARAMETRO_URL_SERVICIO_INTEGRACION_SOL As String = "UrlServicioIntegracionSol"

    'Parametros Nuevo Saldos
    Public Const CODIGO_PARAMETRO_FORMULARIO_PASES_PROCESO_ABONO As String = "FormularioPasesProcesoAbono"
    Public Const COD_TERMINO_ABONO_CODIGO_PROCESO As String = "ABONO_CODIGO_PROCESO"
    Public Const COD_TERMINO_ABONO_TIPO_CUENTA As String = "ABONO_TIPO_CUENTA"
    Public Const COD_TERMINO_ABONO_NUMERO_CUENTA As String = "ABONO_NUMERO_CUENTA"
    Public Const COD_TERMINO_ABONO_DOCUMENTO As String = "ABONO_DOCUMENTO"
    Public Const COD_TERMINO_ABONO_TITULARIDAD As String = "ABONO_TITULARIDAD"
    Public Const COD_TERMINO_ABONO_OBSERVACIONES As String = "ABONO_OBSERVACIONES"
    Public Const COD_CODIGO_ISO_DIVISA_DEFECTO As String = "CodigoIsoDivisaDefecto"
    Public Const COD_MAE_LIMITE_DIAS_ANTERIORES_FECHA_INICIO_VIGENCIA As String = "MAELimiteDiasAnterioresFechaInicioVigencia"

    'Parametros Salidas
    Public Const CODIGO_PARAMETRO_SALIDAS_AGRUPAR_BULTOS As String = "AgruparBultos"

    Public Const DigitoVerificadorBrasil As String = "DigitoVerificadorBrasil"

    Public Const CODIGO_CONFIGURACION_NIVEL_DETALLE As String = "D"
    Public Const CODIGO_CONFIGURACION_NIVEL_TOTAL As String = "T"
    Public Const CODIGO_CONFIGURACION_NIVEL_AMBOS As String = ""


    Public Const CODIGO_APLICACION_GENESIS As String = "Genesis"
    Public Const CODIGO_APLICACION_GENESIS_CONTEO As String = "GenesisConteo"
    Public Const CODIGO_APLICACION_GENESIS_EMULADOR As String = "Emulador"
    Public Const CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO As String = "RecepcionyEnvio"
    Public Const CODIGO_APLICACION_GENESIS_PACK_MODULAR As String = "PackModular"
    Public Const CODIGO_APLICACION_GENESIS_REPORTES As String = "Reportes"
    Public Const CODIGO_APLICACION_GENESIS_SALDOS As String = "GenesisSaldos"
    Public Const CODIGO_APLICACION_GENESIS_SALIDAS As String = "GenesisSalidas"
    Public Const CODIGO_APLICACION_GENESIS_SALIDAS_ANTIGUO As String = "Salidas"
    Public Const CODIGO_APLICACION_GENESIS_IAC As String = "IAC"
    Public Const CODIGO_APLICACION_LEGADO As String = "Legado"
    Public Const CODIGO_APLICACION_CONTEO As String = "Conteo"

    Public Const CODIGO_PARAMETRO_IAC_PAIS_PERMITIR_CUALQUIER_TOT_SALDOS As String = "PermitirCualquierTotalizadorSaldoServicio"
    Public Const CODIGO_PARAMETRO_IAC_PAIS_CREAR_CONFIGURACION_NIVEL_SALDO As String = "CrearConfiguiracionNivelSaldo"
    Public Const CODIGO_PARAMETRO_IAC_DELEGACION_CODIGOFORMULARIOINGRESO As String = "CodigoFormularioIngreso"
    Public Const CODIGO_PARAMETRO_IAC_PAIS_TRABAJA_POR_BULTO As String = "BolGestionaPorBulto"
    Public Const CODIGO_PARAMETRO_IAC_DELEGACION_CODIGO_EMISOR_DOCUMENTO = "CodigoEmisorPorDefecto"
    Public Const CODIGO_PARAMETRO_IAC_PAIS_CODIGO_COMPROBANTE_BASADO_REGLAS = "CodigoComprovanteBasadoEnReglas"
    Public Const CODIGO_PARAMETRO_IAC_PAIS_CODIGO_SUBCANAL_DEFECTO = "CodigoSubCanalDefecto"
    Public Const CODIGO_PARAMETRO_IAC_PAIS_CODIGO_FORMATO_BOLSON_AGRUPADOR = "CodigoFormatoBolsonAgrupador"
    Public Const CODIGO_PARAMETRO_IAC_CREAR_PRECINTO_MODULO = "CrearPrecintoModulo"
    Public Const CODIGO_PARAMETRO_IAC_PAIS_CONFIG_NIVEL_DETALLE = "ConfigNivelDetalle"
    Public Const CODIGO_PARAMETRO_IAC_DELEGACION_INTEGRACION_SALDOS_URL = "IntegracionNuevoSaldosURLWeb"
    Public Const CODIGO_PARAMETRO_IAC_DELEGACION_DIRECCION_REPORTES_GENERADOS = "DireccionReportesGenerados"

    Public Const CODIGO_PARAMETRO_IAC_PAIS_CANTIDAD_HORAS_INICIO_VIGENCIA = "CantidadHorasInicioVigencia"
    Public Const CODIGO_PARAMETRO_IAC_PAIS_CANTIDAD_HORAS_PERIODO_FUTUROS = "CantidadHorasPeriodosFuturos"
    Public Const CODIGO_PARAMETRO_IAC_PAIS_CANTIDAD_APROBACIONES_CUENTAS_BANCARIAS = "CantidadAprobacionesCuentasBancarias"


    Public Const CODIGO_PARAMETRO_SALDOS_PAIS_URL_SERVICIO_PERIODO = "URLServicioCrearPeriodos"

    Public Const CODIGO_PARAMETRO_RS_REPORT_SEVICE_URL = "ReportServicesURL"
    Public Const CODIGO_PARAMETRO_RS_A_USER = "RS_A_USER"
    Public Const CODIGO_PARAMETRO_RS_A_PASS = "RS_A_PASS"
    Public Const CODIGO_PARAMETRO_RS_A_DOMAIN = "RS_A_DOMAIN"
    Public Const CODIGO_PARAMETRO_RS_CARPETA_REPORTES As String = "CarpetaReportes"

    Public Const CODIGO_PARAMETRO_URL_REINTENTOS_ENVIOS_FVO = "URLReintentosEnviosFVOaSwitch"
    Public Const CODIGO_PARAMETRO_CANTIDAD_APROVACIONES = "CantidadAprobacionesCuentasBancarias"
    Public Const CODIGO_PARAMETRO_CAMPOS_APROVACIONES = "CamposAprobacionRequeridaCuentasBancarias"
    Public Const CODIGO_PARAMETRO_APROBACION_ALTA = "AprobacionDatosBancariosAlta"

    Public Const CODIGO_PARAMETRO_URL_NOTIFICACION_API_GLOBAL = "APIGlobalURLNotificacion"
    Public Const CODIGO_PARAMETRO_URL_AUTENTICACION_NILO = "NiloURLAutenticacion"
    Public Const CODIGO_PARAMETRO_CREDENCIAL_NILO = "NiloCredencial"
    Public Const CODIGO_PARAMETRO_URL_ACUERDO_SERVICIO_NILO = "NiloURLConsultaAcuerdosServicio"
    Public Const CODIGO_PARAMETRO_URL_DELIVERED_MESSAGES_NILO = "NiloURLMensajesEntregados"

    Public Const CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL = "APIGlobalURLAutenticacion"
    Public Const CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_SCOPE = "APIGlobalURLAutenticacionScope"
    Public Const CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_CS = "APIGlobalURLAutenticacionClientSecret"
    Public Const CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_CI = "APIGlobalURLAutenticacionClientId"


    Public Const CODIGO_PARAMETRO_CONTEO_DELEGACION_ENVIARDATOSALCERRARREMESA = "EnviarDatosAlCerrarRemesa"

    'PARAMETROS REPORTE
    Public Const CODIGO_PARAMETRO_REPORTE_CODIGO_PROCESO_ABONO As String = "COD_ABONO"
    Public Const CODIGO_PARAMETRO_REPORTE_CODIGO_DELEGACION As String = "CODIGO_DELEGACION"
    Public Const CODIGO_PARAMETRO_REPORTE_FECHA_HORA As String = "FECHA_HORA"
    Public Const CODIGO_PARAMETRO_REPORTE_TIPO_ABONO As String = "TIPO_ABONO"
    Public Const CODIGO_PARAMETRO_REPORTE_TIPO_VALORES As String = "TIPO_VALORES"
    Public Const CODIGO_PARAMETRO_REPORTE_CODIGO_BANCO As String = "CODIGO_BANCO"
    Public Const CODIGO_PARAMETRO_REPORTE_DESCRIPCION_BANCO As String = "DESCRIPCION_BANCO"
    Public Const CODIGO_PARAMETRO_REPORTE_DOCUMENTO_PASES As String = "DOCUMENTO_PASES"
    Public Const CODIGO_PARAMETRO_REPORTE_CODIGO_DIVISA As String = "COD_ISO_DIVISA"
    Public Const CODIGO_PARAMETRO_REPORTE_TIPO_REPORTE As String = "TIPO_REPORTE"
    Public Const CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_ABONO As String = "OID_ABONO"
    Public Const CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CONFIG_REPORTE As String = "IDENTIFICADOR_CONFIG_REPORTE"
    Public Const CODIGO_PARAMETRO_REPORTE_FORMULA_NOMBRE As String = "FORMULA_NOMBRE"
    Public Const CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_CERTIFICADO As String = "OID_CERTIFICADO"
    Public Const CODIGO_PARAMETRO_REPORTE_CODIGO_CERTIFICADO As String = "CODIGO_CERTIFICADO"
    Public Const CODIGO_PARAMETRO_REPORTE_IDENTIFICADOR_SUBCANAL As String = "OID_SUBCANAL"
    Public Const CODIGO_PARAMETRO_REPORTE_CODIGO_ESTADO_CERTIFICADO As String = "CODIGO_ESTADO_CERTIFICADO"
    Public Const CODIGO_PARAMETRO_REPORTE_CODIGO_SUBCANAL As String = "CODIGO_SUBCANAL "

    'PARAMETROS ABONO
    Public Const CODIGO_PARAMETRO_ABONO_CODIGO_DELEGACION As String = "CODIGO_DELEGACION"
    Public Const CODIGO_PARAMETRO_ABONO_FECHA_HORA As String = "FECHA_HORA"
    Public Const CODIGO_PARAMETRO_ABONO_TIPO_ABONO As String = "TIPO_ABONO"
    Public Const CODIGO_PARAMETRO_ABONO_TIPO_VALORES As String = "TIPO_VALORES"
    Public Const CODIGO_PARAMETRO_ABONO_CODIGO_BANCO As String = "CODIGO_BANCO"
    Public Const CODIGO_PARAMETRO_ABONO_DESCRIPCION_BANCO As String = "DESCRIPCION_BANCO"

    'PARAMETROS PROCESSO PACKMODULAR
    Public Const CODIGO_PARAMETRO_PACKMODULAR_CANT_DIAS_ENVIO_EMAIL_VENC_CONTENEDOR As String = "CantDiasEnvioEmailVencContenedor"
    Public Const CODIGO_PARAMETRO_PACKMODULAR_EMAIL_VENCIMENTO_CONTENEDOR As String = "EmailVencimientoContenedor"

    'TIPO CLIENTE
    Public Const CODIGO_TIPO_CLIENTE_BANCO As String = "1"

    Public Const CODIGO_PARAMETRO_SALIDAS_MESCLAR_BILLETE_MONEDA As String = "MezclarBilleteMoneda"
    Public Const CODIGO_PARAMETRO_SALIDAS_MESCLAR_DENOMINACION As String = "MezclarDenominacion"
    Public Const CODIGO_PARAMETRO_SALIDAS_ALGORITMO_DIVISION_BULTOS As String = "AlgoritmoDivisionBultos"
    Public Const CODIGO_PARAMETRO_SALIDAS_CODIGO_CONFIG_AUTO_DESGLOSE As String = "CodigoConfiguracionAutoDesglose"

    ''' <summary>
    ''' Al rellenar ese campo con Génesis, SOL regresa en los códigos terceros, el código utilizado por Génesis.
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CODIGO_SISTEMA_TERCERO_GENESIS As String = "GENESIS"

    Public Const CODIGO_PARAMETRO_SALIDAS_CONFIRMAR_SOLICITUD_FONDOS_AUTO As String = "ConfirmarSolicitudFondosAutomatica"

    Public Const DUO_CODIGO_ESTADO_PROCESO_PENDENTE As Integer = 0
    Public Const DUO_CODIGO_ESTADO_PROCESO_EJECUCION As Integer = 1
    Public Const DUO_CODIGO_ESTADO_PROCESO_CONCLUIDOSUCESO As Integer = 2
    Public Const DUO_CODIGO_ESTADO_PROCESO_ERROR As Integer = 3

    Public Const DUO_CODIGO_TIPO_LOG_ERROR As Integer = 1

    'pack
    Public Const CONST_IDENTIFICADOR_PACK_MODULAR As String = "pack"

    'PREFERENCIAS - FUNCIONALIDADES
    Public Const CODIGO_PREFERENCIAS_FUNCIONALIDAD_CENTRAL_NOTIFICACION As String = "CENTRAL_NOTIFICACION"

    'PREFERENCIAS - PROPRIEDADES
    Public Const CODIGO_PREFERENCIAS_PROPRIEDAD_ACTUALIZACION_AUTOMATICA As String = "ACTUALIZACION_AUTOMATICA"

    Public Const CODIGO_PARAMETRO_RYE_CODIGODIVISAPORDEFECTO = "CodigoDivisaPorDefecto"

    'Tipo notificações
    Public Const CODIGO_TIPO_NOTIFICACION_SOBRE_RIPLEY As String = "SobreRipley"
    Public Const CODIGO_TIPO_NOTIFICACION_DEPOSITO_RIPLEY As String = "DepositoRipley"
    Public Const CODIGO_TIPO_NOTIFICACION_ENV_REMESA_LV As String = "Salidas_Enviar_Remesa_LV"


    Public Shared ReadOnly CONST_CERTIFICADO_USUARIO As String = "SERVICIO_CERTIFICACION"

    'Parametros tipos de planificación
    Public Const CODIGO_PARAMETRO_TIPO_PLANIFICACION_FECHA_VALOR As String = "PlanificacionFechaValorIAC"
    Public Const CODIGO_PARAMETRO_TIPO_PLANIFIACION_FV_ONLINE As String = "PlanificacionFVOnlineIAC"


    ' Planificaciones
    Public Const LUNES As Integer = 1
    Public Const MARTES As Integer = 2
    Public Const MIERCOLES As Integer = 3
    Public Const JUEVES As Integer = 4
    Public Const VIERNES As Integer = 5
    Public Const SABADO As Integer = 6
    Public Const DOMINGO As Integer = 7
    Public Const TOTAL_LINEAS As Integer = 4

#Region "Tabelas nomeadas para retornos das procedures - REFCURSORS"

    Public Const CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_CONTENEDORES As String = "CONTENEDORES"
    Public Const CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_PRECINTOS As String = "PRECINTOS"

#End Region

    Public Const CONST_VACIO_PARA_BBDD As String = "###VACIO###"

    'Rest params
    Public Const CONST_CLIENT_ID As String = "client_id"
    Public Const CONST_CLIENT_SECRET As String = "client_secret"
    Public Const CONST_SCOPE As String = "scope"
    Public Const CONST_GRANT_TYPE As String = "grant_type"
    Public Const CONST_CLIENT_CREDENTIALS As String = "client_credentials"
    Public Const CONST_URL_AUTENTICACION As String = "url_autenticacion"
End Class
