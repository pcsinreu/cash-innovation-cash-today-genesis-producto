Public Class Constantes

    ' nomes dos serviços genesis
    Public Const C_SERVICIO_LOGIN As String = "Login.asmx"
    Public Const C_SERVICIO_GENESIS_LOGIN As String = "GenesisLogin.asmx"
    Public Const C_SERVICIO_INTEGRACION_LOGIN_UNIFICADO As String = "Integracion.asmx"

    Public Const C_SERVICIO_CONTEO As String = "Conteo/Conteo.asmx"
    Public Const C_SERVICIO_LOGIN_SALIDAS As String = "Salidas/Login.asmx"
    Public Const C_SERVICIO_SALIDAS As String = "Salidas/Salidas.asmx"
    Public Const C_SERVICIO_RECEPCION_Y_ENVIO As String = "RecepcionyEnvio/Servicio.asmx"
    Public Const C_SERVICIO_COMON As String = "Comon.asmx"
    Public Const C_SERVICIO_GENESIS_MOVIL As String = "GenesisMovil/Servicio.asmx"

    ' nomes dos serviços IAC
    Public Const C_SERVICIO_IAC_DELEGACION As String = "IAC/Delegacion.asmx"

    'TODO: Verificar
    ' Constantes para o Event Viewer
    Public Const NOME_LOG_EVENTOS As String = "GENESIS"

    ' Códigos das Permissões do AD ATN
    Public Const C_PERMISO_ASIGNAR_FORMATO_MOD_RED As String = "Asignar_formato_modelo_red"
    Public Const C_PERMISO_FORMATO_TIRA As String = "Mantener_Formato_tira"
    Public Const C_PERMISO_REGISTRAR_TIRA As String = "Registrar_Tira"

    Public Const CODIGO_PERMISO_SUPERVISAR As String = "SUPERVISAR"

    'Tipo Certificado
    Public Const CONST_TIPO_CERTIFICADO_PROVISIONAL_CON_CIERRE As String = "PC"
    Public Const CONST_TIPO_CERTIFICADO_PROVISIONAL_SIN_CIERRE As String = "PS"
    Public Const CONST_TIPO_CERTIFICADO_DEFINITIVO As String = "DE"

    'Modo Certificado
    Public Const CONST_MODO_CERTIFICADO_CONSULTA As String = "CON"
    Public Const CONST_MODO_CERTIFICADO_GENERAR As String = "GEN"

    Public Const COD_CANAL As String = "GEPR_TCANAL"
    Public Const COD_SUBCANAL As String = "GEPR_TSUBCANAL"
    Public Const COD_CLIENTE As String = "GEPR_TCLIENTE"
    Public Const COD_SUBCLIENTE As String = "GEPR_TSUBCLIENTE"
    Public Const COD_PUNTOSERVICIO As String = "GEPR_TPUNTO_SERVICIO"
    Public Const COD_PLANTA As String = "GEPR_TPLANTA"
    Public Const COD_TIPO_SECTOR As String = "GEPR_TTIPO_SECTOR"
    Public Const COD_SECTOR As String = "GEPR_TSECTOR"

    Public Const COD_RELACION_TIPO_SECTOR_DESTINO As String = "D"
    Public Const COD_RELACION_TIPO_SECTOR_ORIGEN As String = "O"

    'Estados Bulto
    Public Const CONST_BULTO_ESTADO_TERMINADO As String = "TE"

    'Estado Resultado Certificado
    Public Const CONST_RESULTADO_CERTIFICADO_PENDIENTE As String = "PE"
    Public Const CONST_RESULTADO_CERTIFICADO_ENCURSO As String = "EC"
    Public Const CONST_RESULTADO_CERTIFICADO_PROCESADO As String = "PR"
    Public Const CONST_RESULTADO_CERTIFICADO_ERRO As String = "ER"

    'Public Const C_URL_NEUVOSALDOS As String = "IntegracionNuevoSaldosURLWeb"

    'Codigos de parâmetros do IAC
    Public Const CONST_COD_PARAMETRO_BOL_TRABALHA_BULTO As String = "BolGestionaPorBulto"

    'Codigos de parâmetros do Reportes
    Public Const CONST_COD_PARAMETRO_REPORTES_URL As String = "ReportServicesURL"
    Public Const CONST_COD_PARAMETRO_REPORTES_USER As String = "RS_A_USER"
    Public Const CONST_COD_PARAMETRO_REPORTES_PASS As String = "RS_A_PASS"
    Public Const CONST_COD_PARAMETRO_REPORTES_DOMAIN As String = "RS_A_DOMAIN"
    Public Const CONST_COD_PARAMETRO_REPORTES_CARPETA_REPORTES As String = "CarpetaReportes"
    Public Const CONST_COD_PARAMETRO_IMPRIMIR_DOC_SALDOS As String = "ImprimirSolamenteDocTransferenciaResponsabilidad"
    Public Const CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA As String = "MantenimientoClientesDivisasPorPantalla"

    Public Const VERSION_INICIO_TRABALHAR_POR_PLANTA As Int32 = 13110102

    'Codigo Tipo Documento
    Public Const CONST_COD_TIPO_DOCUMENTO_ACTA As String = "ACTAS"

    '### CONSTANTES CODIGO NIVEL DETALLE DIFERENCIAS ###'
    Public Const CONST_COD_DIFERENCIA_NIVEL_DETALLE_GERAL As String = "G"
    Public Const CONST_COD_DIFERENCIA_NIVEL_DETALLE_TOTAL As String = "T"
    Public Const CONST_COD_DIFERENCIA_NIVEL_DETALLE_DETALLADO As String = "D"

    '### ESTADOS REMESA CONTEO ###"
    Public Const CONST_REMESA_RECHAZADO As String = "RZ"
    Public Const CONST_REMESA_MODIFICADO_SUPERVISOR As String = "MS"

    'Codigos Estados de Movimentação de Fundos no Salidas
    Public Const CONST_COD_ESTADO_MOVIMENTACION_FONDO_ACEPTADO As String = "AC"
    Public Const CONST_COD_ESTADO_MOVIMENTACION_FONDO_RECEBIDO As String = "RE"
    Public Const CONST_COD_ESTADO_MOVIMENTACION_FONDO_RECHAZADO As String = "RC"

    'Codigos Copias Recibo Transporte Salidas
    Public Const CopiasReciboTransporteOriginal As String = "ORIGINAL"
    Public Const CopiasReciboTransporteDuplicado As String = "DUPLICADO"
    Public Const CopiasReciboTransporteTriplicado As String = "TRIPLICADO"
    Public Const CopiasReciboTransporteQuadruplicado As String = "QUADRUPLICADO"

    '### CONSTANTES CODIGOS TIPO MEDIO DE PAGO ###'
    Public Const COD_TIPO_MEDIO_PAGO_OTROSVALORES As String = "codtipo"
    Public Const COD_TIPO_MEDIO_PAGO_TICKET As String = "codtipoa"
    Public Const COD_TIPO_MEDIO_PAGO_CHEQUE As String = "codtipob"
    Public Const COD_TIPO_MEDIO_PAGO_TARJETAS As String = "codtipoc"

    Public Const COD_TIPO_EFECTIVO As String = "efectivo"

End Class