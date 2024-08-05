CREATE OR REPLACE PACKAGE SAPR_PSALDOS_###VERSION### AS
  
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PSALDOS_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
  const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
  const$comilla_simple  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);
  const$formato_gmt CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'DD/MM/YYYY HH24:MI:SS TZH:TZM';

	PROCEDURE sactualizar_saldo_acuerdo(
				par$oid_llamada							IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$bol_forzar_calculo      IN gepr_pcomon_###VERSION###.tipo$nbol_,
        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_validaciones         OUT sys_refcursor);

  --Procedure utilizado por la pantalla de Ordenes de Servicio para recalcular una los datos de una orden de servicio
  PROCEDURE srecalcular_saldo_acuerdo(par$oid_saldo_acuerdo_ref   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$rc_validaciones         OUT sys_refcursor
                                    );

  PROCEDURE snotificar_saldo_acuerdo(par$oid_saldo_acuerdo_ref   IN gepr_pcomon_###VERSION###.tipo$oid_,
  par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
  par$rc_validaciones         OUT sys_refcursor
                                    );

	PROCEDURE sborrar_saldos_hist_cli (par$oid_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_pais                IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_);

  PROCEDURE sactualizar_saldos_hist_cli (par$oid_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_pais                IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_);

  PROCEDURE sactualizar_saldos_historico(par$oid_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_maquina             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_pais                IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_tipo_actualizacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rc_validaciones         OUT sys_refcursor
  );

  PROCEDURE srecuperar_saldos_historico(
                                    par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$bol_valor_detallar        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_pais                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fecha                     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$acod_device_id            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$bol_device_id_completo    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_cliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_sub_cliente           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_punto_servicio        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$acod_canal                IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_subcanal             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_divisa               IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$info_ejecucion            IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$rc_cuentas                OUT sys_refcursor,
                                    par$rc_divisas                OUT sys_refcursor,
                                    par$rc_denominaciones         OUT sys_refcursor,
                                    par$rc_validaciones           OUT sys_refcursor
                                    );

	/*Valida los IN al procedure de recuperar saldos historico*/
	PROCEDURE svalidar_saldos_historico(/*Filtros entrada*/
                                    par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_pais                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fecha                     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$acod_device_id            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$bol_device_id_completo    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_cliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_sub_cliente           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_punto_servicio        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$acod_canal                IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_subcanal             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_divisa               IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    /*Patron*/
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_
                                    );

  /* Recuperar Saldos */
  PROCEDURE srecuperar_saldos(par$pag_indice                IN gepr_pcomon_###VERSION###.tipo$nel_,
                              par$pag_reg_pagina            IN gepr_pcomon_###VERSION###.tipo$nel_,
                              par$bol_detallar_trans        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_detallar_valores      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_detallar_bolsas       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_info_adicional        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_disponible            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_acreditado            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_notificado            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_maqu_vigente          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$fyh_gestion               IN gepr_pcomon_###VERSION###.tipo$fyh_,
                              par$fyh_creacion              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                              par$cod_cliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_sub_cliente           IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_punto_servicio        IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_delegacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_planta                IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_divisa                IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_maquinas              IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$cod_canales               IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$cod_subcanales            IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$info_ejecucion            IN gepr_pcomon_###VERSION###.tipo$desc_,
                              par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$rc_movimientos            OUT sys_refcursor,
                              par$rc_formularios            OUT sys_refcursor,
                              par$rc_cuentas                OUT sys_refcursor,
                              par$rc_terminos               OUT sys_refcursor,
                              par$rc_divisas                OUT sys_refcursor,
                              par$rc_denominaciones         OUT sys_refcursor,
                              par$rc_efectivos              OUT sys_refcursor,
                              par$rc_mediopagos             OUT sys_refcursor,
                              par$rc_saldos                 OUT sys_refcursor,
                              par$rc_bolsas                 OUT sys_refcursor,
                              par$validaciones              OUT sys_refcursor,
                              par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validar codigo Ajeno e preencher tabela temporaria */
  PROCEDURE svalidar_codigos(par$cod_cliente              IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_sub_cliente          IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_punto_servicio       IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_delegacion           IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_planta               IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_divisa               IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_maquinas             IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_canales              IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_subcanales           IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$oid_cliente             OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_sub_cliente         OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_punto_servicio      OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_delegacion          OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_planta              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_divisa              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_canales             OUT gepr_pcomon_###VERSION###.tipo$oids_,
                             par$oid_subcanales          OUT gepr_pcomon_###VERSION###.tipo$oids_,
                             par$selects                  IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_);

  /*Valida los IN al procedure de recuperar saldos periodos*/
  PROCEDURE svalidar_saldos_periodos(
                                    /*Identificador Log*/
                                    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    /*Filtros entrada*/
                                    par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_pais                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_banco                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_plan                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_periodo               IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                    par$acod_device_id            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_est_periodo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$nel_gmt_minuto            IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    /*Patron*/
                                    par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_
                                    );



  /* Recuperar valores saldos */
  PROCEDURE srecuperar_saldos_periodos(
                                        /*Identificador Log*/
                                        par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        /*Filtros entrada*/
                                        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_pais IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_banco IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_plan IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$fyh_periodo            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$acod_device_id          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$acod_est_periodo        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$nel_gmt_minuto         IN gepr_pcomon_###VERSION###.tipo$nel_,
                                        /*Cursores respuesta*/
                                        par$rc_plan           OUT sys_refcursor,
                                        par$rc_periodos           OUT sys_refcursor,
                                        par$rc_saldos           OUT sys_refcursor,
                                        par$rc_saldos_det           OUT sys_refcursor,
                                        par$rc_validaciones           OUT sys_refcursor,
                                        /*Patron*/
                                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_);

 PROCEDURE svalidar_fecha(par$nel_index               IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$oid_sector_mae          IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$nel_gmt_minuto          IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$fyh_periodo             IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$fyh_periodo_gmt0        OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_);

  /*Valida la entrada al procedure srecuperar_saldos_acuerdos*/
  PROCEDURE svalidar_saldos_acuerdos(
      /*Identificador Log*/
      par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
      /*Filtros entrada*/
      par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$des_sourcerefenceid     IN gepr_pcomon_###VERSION###.tipo$desc_,
      par$cod_fecha_saldos              IN gepr_pcomon_###VERSION###.tipo$cod_,
      /*Patron*/
      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Recupera los saldos de los acuerdos servicios */
  PROCEDURE srecuperar_saldos_acuerdos(
      /*Identificador Log*/
      par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
      /*Filtros entrada*/
      par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_fecha_saldos              IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$des_sourcerefenceid     IN gepr_pcomon_###VERSION###.tipo$desc_,
      /*Patron*/
      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
      /*Cursores respuesta*/
      par$rc_puntos               OUT sys_refcursor,
      par$rc_movimientos					OUT sys_refcursor,
      par$rc_validaciones         OUT sys_refcursor
  );

	/* Función que corrobora si hay error para el procedure de recuperar saldos acuerdos */
	FUNCTION fhay_error_rec_sal_acu RETURN BOOLEAN;
   /*Función que corrobora si hay error para el procedure de recuperar saldos periodos*/
   FUNCTION fhay_error_rec_sal_per  RETURN BOOLEAN;

  /* Recuperar Notificacion */
  PROCEDURE srecuperar_notificacion(
                              par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_pais                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$oid_integracion     IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$rc_datos                  OUT sys_refcursor);

END SAPR_PSALDOS_###VERSION###;
/
create or replace PACKAGE BODY SAPR_PSALDOS_###VERSION### AS



  PROCEDURE sactualizar_saldo_acuerdo(
				par$oid_llamada							IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$bol_forzar_calculo      IN gepr_pcomon_###VERSION###.tipo$nbol_,
        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_validaciones         OUT sys_refcursor) 
   IS
      var$cod_pais                    gepr_pcomon_###VERSION###.tipo$cod_;
      var$oid_pais                    gepr_pcomon_###VERSION###.tipo$oid_;
      var$fecha                       gepr_pcomon_###VERSION###.tipo$gmt_;
      var$mensaje                     gepr_pcomon_###VERSION###.tipo$desc_;
      const$agrupacionReferenceId constant  gepr_pcomon_###VERSION###.tipo$desc_ := 'AgrupacionReferenceId';
      const$cantidadDeDias constant  gepr_pcomon_###VERSION###.tipo$desc_ := 'cantidadDeDiasFacturacion';
      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.' || 'sactualizar_saldo_acuerdo';

      /*Parametro de la cantidad de días que se controla referencias - cantidadDeDiasFacturacion*/
      var$cantidad_de_dias_texto gepr_pcomon_###VERSION###.tipo$desc_;

      var$cantidad_de_dias gepr_pcomon_###VERSION###.tipo$nel_;
      /*Busco los documentos con punto de servicio en la tabla sapr_tacuerdo_servicio*/

      CURSOR C_REFERENCES IS (
        SELECT  DISTINCT ACSE.OID_PAIS,
          PAIS.COD_PAIS,
          ACSE.DES_PRODUCT_CODE,
          ACSE.OID_ACUERDO_SERVICIO,
          MAQU.OID_MAQUINA,
          TO_TIMESTAMP_TZ(TO_CHAR( (SYSDATE - FECHA.NEL_CAMPO5), 'DD/MM/YYYY') || ' 23:59:59 ' ||  TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN (SYSDATE - FECHA.NEL_CAMPO5) >= DELE.FYH_VERANO_INICIO AND (SYSDATE - FECHA.NEL_CAMPO5) <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' || TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN (SYSDATE - FECHA.NEL_CAMPO5) >= DELE.FYH_VERANO_INICIO AND (SYSDATE - FECHA.NEL_CAMPO5) <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') HOR_SALDO
        FROM
          SAPR_TACUERDO_SERVICIO ACSE
          INNER JOIN GEPR_TPUNTO_SERVICIO PUNTO ON PUNTO.OID_PTO_SERVICIO = ACSE.OID_PTO_SERVICIO
          INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_MAQUINA = PUNTO.OID_MAQUINA
          INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = MAQU.OID_SECTOR 
          INNER JOIN GEPR_TPLANTA PLAN ON PLAN.OID_PLANTA = SECT.OID_PLANTA
          INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
          INNER JOIN GEPR_TPAIS PAIS ON ACSE.OID_PAIS = PAIS.OID_PAIS
          INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.OID_CAMPO1 = PAIS.OID_PAIS AND AUXP.COD_CALIFICADOR = 'OID_PAIS',
          SAPR_GTT_TAUXILIAR FECHA
        WHERE
          ACSE.BOL_VIGENTE = 1
          AND FECHA.COD_CALIFICADOR = 'FECHA'
          AND  TRUNC(SYSDATE - FECHA.NEL_CAMPO5)  >= NVL(ACSE.FYH_VIGENCIA_INICIO, TRUNC(SYSDATE - FECHA.NEL_CAMPO5))
          AND  TRUNC(SYSDATE - FECHA.NEL_CAMPO5)  <= NVL(ACSE.FYH_VIGENCIA_FIN, TRUNC(SYSDATE - FECHA.NEL_CAMPO5))
          AND NOT EXISTS
          (
            SELECT 1 
            FROM SAPR_TSALDO_ACUERDO_REF SARF 
            WHERE
              SARF.OID_PAIS = ACSE.OID_PAIS AND
              SARF.DES_PRODUCT_CODE =  ACSE.DES_PRODUCT_CODE AND
              SARF.OID_ACUERDO_SERVICIO =  ACSE.OID_ACUERDO_SERVICIO AND 
              SARF.OID_MAQUINA =  MAQU.OID_MAQUINA AND 
              SARF.FEC_SALDO = TRUNC(SYSDATE - FECHA.NEL_CAMPO5) 
            ) 
      );


      CURSOR C_SALDOS IS 
         
        WITH DATOS AS (
          -- CASHINES, ACREDITACIONES, COMISIONES  
          SELECT    SARE.OID_SALDO_ACUERDO_REF,
            SARE.OID_ACUERDO_SERVICIO,
            SARE.OID_MAQUINA,
            SARE.OID_PAIS,
            SARE.FEC_SALDO,
            SARE.HOR_SALDO,
            EFDO.OID_DIVISA,
            CASE CUEN.COD_SUBCANAL 
              WHEN 'INGBIL' THEN '1'      
              WHEN 'INGMON' THEN '2'      
              WHEN 'DEB' THEN '0'      
              WHEN 'DB' THEN '0'      
              WHEN 'SF' THEN '0' END COD_TIPO_MERCANCIA,
            SUM(CASE WHEN FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID') THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_CASHIN,
            COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID') THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_CASHIN,
            0 NUM_TOTAL_SHIPOUT,
            0 NEL_CANT_SHIPOUT,
            0 NUM_TOTAL_CASHIN_ACRED,
            0 NEL_CANT_CASHIN_ACRED,
            SUM(CASE WHEN FORM.COD_FORMULARIO = 'MAEACR' THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_ACREDITACION,
            COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO = 'MAEACR' THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_ACREDITACION,    
            SUM(CASE WHEN FORM.COD_FORMULARIO = 'MAECOM' THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_COMISION,
            COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO = 'MAECOM' THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_COMISION,  
            PLMA.FYH_VIGENCIA_INICIO FYH_INICIO_VIGENCIA_PLAN
          FROM    SAPR_TSALDO_ACUERDO_REF SARE
            INNER JOIN SAPR_TACUERDO_SERVICIO ACSE ON SARE.OID_ACUERDO_SERVICIO = ACSE.OID_ACUERDO_SERVICIO AND                                              
                                                      SARE.OID_PAIS = ACSE.OID_PAIS AND                                              
                                                      ACSE.BOL_VIGENTE = '1'   
            INNER JOIN SAPR_VCUENTA CUEN ON ACSE.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO AND                                    
                                                      SARE.OID_MAQUINA = CUEN.OID_MAQUINA AND                                    
                                                      CUEN.COD_SUBCANAL IN ('INGBIL', 'INGMON', 'DEB', 'DB', 'SF')
            INNER JOIN SAPR_TDOCUMENTO DOCU ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN AND                                       
                  CUEN.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN AND                                       
                  DOCU.GMT_CREACION BETWEEN (SARE.HOR_SALDO - INTERVAL '23:59:59' HOUR TO SECOND) AND SARE.HOR_SALDO
            INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO AND                                        
                                                FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID', 'MAEACR', 'MAECOM')
            INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EFDO ON DOCU.OID_DOCUMENTO = EFDO.OID_DOCUMENTO  
            LEFT JOIN SAPR_TPLANXMAQUINA PLMA ON SARE.OID_MAQUINA = PLMA.OID_MAQUINA AND                                         
                                                PLMA.BOL_ACTIVO = '1'
          WHERE    SARE.BOL_CALCULADO = '0'  
          AND SARE.HOR_SALDO BETWEEN (var$fecha -180) AND var$fecha 
          AND SARE.FEC_SALDO BETWEEN (TRUNC(var$fecha) -180) AND TRUNC(var$fecha)
          GROUP BY    
              SARE.OID_SALDO_ACUERDO_REF,
              SARE.OID_ACUERDO_SERVICIO,
              SARE.OID_MAQUINA,
              SARE.OID_PAIS,
              SARE.FEC_SALDO,
              SARE.HOR_SALDO,
              EFDO.OID_DIVISA,
              CUEN.COD_SUBCANAL,
              PLMA.FYH_VIGENCIA_INICIO
          UNION  -- SHIPOUTES  
          SELECT    SARE.OID_SALDO_ACUERDO_REF,
            SARE.OID_ACUERDO_SERVICIO,
            SARE.OID_MAQUINA,
            SARE.OID_PAIS,
            SARE.FEC_SALDO,
            SARE.HOR_SALDO,
            EFDO.OID_DIVISA,
            CASE CUEN.COD_SUBCANAL 
              WHEN 'INGBIL' THEN '1'      
              WHEN 'INGMON' THEN '2'      
              WHEN 'DEB' THEN '0'      
              WHEN 'DB' THEN '0'    END COD_TIPO_MERCANCIA,
            0 NUM_TOTAL_CASHIN,
            0 NEL_CANT_CASHIN,
            SUM(CASE WHEN FORM.COD_FORMULARIO IN ('MAESOC', 'MAESOD') THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_SHIPOUT,
            COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO IN ('MAESOC', 'MAESOD') THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_SHIPOUT,
            0 NUM_TOTAL_CASHIN_ACRED,
            0 NEL_CANT_CASHIN_ACRED,
            0 NUM_TOTAL_ACREDITACION,
            0 NEL_CANT_ACREDITACION,
            0 NUM_TOTAL_COMISION,
            0 NEL_CANT_COMISION,  
            PLMA.FYH_VIGENCIA_INICIO FYH_INICIO_VIGENCIA_PLAN
          FROM    SAPR_TSALDO_ACUERDO_REF SARE
            INNER JOIN SAPR_TACUERDO_SERVICIO ACSE ON SARE.OID_ACUERDO_SERVICIO = ACSE.OID_ACUERDO_SERVICIO AND                                              
                                                      SARE.OID_PAIS = ACSE.OID_PAIS AND                                              
                                                      ACSE.BOL_VIGENTE = '1'    
            INNER JOIN SAPR_VCUENTA CUEN ON SARE.OID_MAQUINA = CUEN.OID_MAQUINA AND                                    
                                            CUEN.COD_SUBCANAL IN ('INGBIL', 'INGMON', 'DEB', 'DB') AND                                    
                                            CUEN.COD_CLIENTE = '999999' -- PROSEGUR    
            INNER JOIN SAPR_TDOCUMENTO DOCU ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN AND                                       
                                                CUEN.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN AND                                       
                                                DOCU.GMT_CREACION BETWEEN (SARE.HOR_SALDO - INTERVAL '23:59:59' HOUR TO SECOND) AND SARE.HOR_SALDO
            INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO AND                                        
                                                FORM.COD_FORMULARIO IN ('MAESOC', 'MAESOD')
            INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EFDO ON DOCU.OID_DOCUMENTO = EFDO.OID_DOCUMENTO  
            LEFT JOIN SAPR_TPLANXMAQUINA PLMA ON SARE.OID_MAQUINA = PLMA.OID_MAQUINA AND                                         
                                                PLMA.BOL_ACTIVO = '1'  
          WHERE    
            SARE.BOL_CALCULADO = '0'  
            AND SARE.HOR_SALDO BETWEEN (var$fecha -180) AND var$fecha 
            AND SARE.FEC_SALDO BETWEEN (TRUNC(var$fecha) -180) AND TRUNC(var$fecha)
            GROUP BY    SARE.OID_SALDO_ACUERDO_REF,
            SARE.OID_ACUERDO_SERVICIO,
            SARE.OID_MAQUINA,
            SARE.OID_PAIS,
            SARE.FEC_SALDO,
            SARE.HOR_SALDO,
            EFDO.OID_DIVISA,
            CUEN.COD_SUBCANAL,
            PLMA.FYH_VIGENCIA_INICIO
          UNION  -- CASHINES ACREDITADOS  
          SELECT    SARE.OID_SALDO_ACUERDO_REF,
            SARE.OID_ACUERDO_SERVICIO,
            SARE.OID_MAQUINA,
            SARE.OID_PAIS,
            SARE.FEC_SALDO,
            SARE.HOR_SALDO,
            EFDO.OID_DIVISA,
            CASE CUEN.COD_SUBCANAL 
              WHEN 'INGBIL' THEN '1'      
              WHEN 'INGMON' THEN '2'      
              WHEN 'DEB' THEN '0'      
              WHEN 'DB' THEN '0'    
              END COD_TIPO_MERCANCIA,
            0 NUM_TOTAL_CASHIN,
            0 NEL_CANT_CASHIN,
            0 NUM_TOTAL_SHIPOUT,
            0 NEL_CANT_SHIPOUT,
            SUM(CASE WHEN FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID') THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_CASHIN_ACRED,
            COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID') THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_CASHIN_ACRED,
            0 NUM_TOTAL_ACREDITACION,
            0 NEL_CANT_ACREDITACION,
            0 NUM_TOTAL_COMISION,
            0 NEL_CANT_COMISION,  
            PLMA.FYH_VIGENCIA_INICIO FYH_INICIO_VIGENCIA_PLAN
          FROM    SAPR_TSALDO_ACUERDO_REF SARE
            INNER JOIN SAPR_TACUERDO_SERVICIO ACSE ON SARE.OID_ACUERDO_SERVICIO = ACSE.OID_ACUERDO_SERVICIO AND                                              
                                                      SARE.OID_PAIS = ACSE.OID_PAIS AND                                              
                                                      ACSE.BOL_VIGENTE = '1'    
            INNER JOIN SAPR_VCUENTA CUEN ON ACSE.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO AND                                    
            SARE.OID_MAQUINA = CUEN.OID_MAQUINA AND                                    CUEN.COD_SUBCANAL IN ('INGBIL', 'INGMON', 'DEB', 'DB')
            INNER JOIN SAPR_TDOCUMENTO DOCU ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN AND                                       
                                              CUEN.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN AND                                       
                                              DOCU.FYH_ACREDITACION BETWEEN (SARE.HOR_SALDO - INTERVAL '23:59:59' HOUR TO SECOND) AND SARE.HOR_SALDO AND                                       
                                              DOCU.BOL_ACREDITADO = '1'    
            INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO AND                                        
                                                FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID')
            INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EFDO ON DOCU.OID_DOCUMENTO = EFDO.OID_DOCUMENTO  
            LEFT JOIN SAPR_TPLANXMAQUINA PLMA ON SARE.OID_MAQUINA = PLMA.OID_MAQUINA AND                                         
            PLMA.BOL_ACTIVO = '1'  
            WHERE    SARE.BOL_CALCULADO = '0'
            AND SARE.HOR_SALDO BETWEEN (var$fecha -180) AND var$fecha 
            AND SARE.FEC_SALDO BETWEEN (TRUNC(var$fecha) -180) AND TRUNC(var$fecha)
            GROUP BY    SARE.OID_SALDO_ACUERDO_REF,
            SARE.OID_ACUERDO_SERVICIO,
            SARE.OID_MAQUINA,
            SARE.OID_PAIS,
            SARE.FEC_SALDO,
            SARE.HOR_SALDO,
            EFDO.OID_DIVISA,
            CUEN.COD_SUBCANAL,
            PLMA.FYH_VIGENCIA_INICIO
        )
        SELECT  DATO.OID_SALDO_ACUERDO_REF,
          DATO.OID_ACUERDO_SERVICIO,
          DATO.OID_MAQUINA,
          DATO.OID_PAIS,
          DATO.FEC_SALDO,
          DATO.HOR_SALDO,
          DATO.OID_DIVISA,
          DATO.COD_TIPO_MERCANCIA, 
          SUM(DATO.NUM_TOTAL_CASHIN) AS NUM_TOTAL_CASHIN,
          SUM(DATO.NEL_CANT_CASHIN) AS NEL_CANT_CASHIN,
          SUM(DATO.NUM_TOTAL_SHIPOUT) AS NUM_TOTAL_SHIPOUT,
          SUM(DATO.NEL_CANT_SHIPOUT) AS NEL_CANT_SHIPOUT,
          SUM(DATO.NUM_TOTAL_CASHIN_ACRED) AS NUM_TOTAL_CASHIN_ACRED,
          SUM(DATO.NEL_CANT_CASHIN_ACRED) AS NEL_CANT_CASHIN_ACRED,
          SUM(DATO.NUM_TOTAL_ACREDITACION) AS NUM_TOTAL_ACREDITACION,
          SUM(DATO.NEL_CANT_ACREDITACION) AS NEL_CANT_ACREDITACION,
          SUM(DATO.NUM_TOTAL_COMISION) AS NUM_TOTAL_COMISION,
          SUM(DATO.NEL_CANT_COMISION) AS NEL_CANT_COMISION,
          DATO.FYH_INICIO_VIGENCIA_PLAN
        FROM  DATOS DATO
        GROUP BY  DATO.OID_SALDO_ACUERDO_REF,
          DATO.OID_ACUERDO_SERVICIO,
          DATO.OID_MAQUINA,
          DATO.OID_PAIS,
          DATO.FEC_SALDO,
          DATO.HOR_SALDO,
          DATO.OID_DIVISA,
          DATO.COD_TIPO_MERCANCIA,
          DATO.FYH_INICIO_VIGENCIA_PLAN;
    
  BEGIN

      /* #### Limpiamos la tabla auxiliar #### */
      DELETE FROM SAPR_GTT_TAUXILIAR;
      COMMIT;

      /* #### Inicializar los cursores #### */
      OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; 

			/* #### Grabar llamadas #### */
			IF par$oid_llamada IS NOT NULL THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
							par$oid_llamada  => par$oid_llamada,
							par$des_origen      => const$nombre_func,
							par$des_version     => const$version,
							par$des_detalle     => q'[Parametros iniciales: 
                par$oid_llamada: ]'             || par$oid_llamada              || q'[
                par$cod_pais: ]'                || par$cod_pais                 || q'[
                par$bol_forzar_calculo: ]'      || par$bol_forzar_calculo       || q'[
                par$cod_usuario: ]'             || par$cod_usuario              || q'[
                par$cod_identificador_ajeno: ]' || par$cod_identificador_ajeno  || q'[
                par$cod_cultura: ]'             || par$cod_cultura,
							par$cod_identificador   => '');
			END IF;      


			SAPR_PLOG_API.SAGREGA_DETALLE(
					par$oid_llamada  => par$oid_llamada,
					par$des_origen      => const$nombre_func,
					par$des_version     => const$version,
					par$des_detalle     => 'Previo a validar el código de país',
					par$cod_identificador   => '');

      /* Validar código de país */
      /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
      var$cod_pais := par$cod_pais;
      GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                              par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                              par$cod_pais => var$cod_pais,
                              par$oid_pais => var$oid_pais,
                              par$cod_cultura => par$cod_cultura);

      IF var$oid_pais IS NOT NULL THEN
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR) VALUES (var$oid_pais, 'OID_PAIS');
      
        /*Obtengo el parametro de la cantidad de días que se controla referencias - cantidadDeDiasFacturacion*/
        var$cantidad_de_dias_texto := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                                                                        par$cod_pais => var$cod_pais,
                                                                                        par$cod_parametro => const$cantidadDeDias,
                                                                                        par$cod_aplicacion => gepr_pcomon_###VERSION###.const$codAplicacionGenesis);

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Valido código de pais - var$oid_pais = ' || var$oid_pais,
            par$cod_identificador   => '');
          
        IF par$bol_forzar_calculo = 1 THEN
          var$fecha := SYSTIMESTAMP +1;
        ELSE
          var$fecha := SYSTIMESTAMP;
        END IF;

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'var$fecha = ' || var$fecha,
            par$cod_identificador   => '');
        

        /*Cantidad de días para atras que controla que exista */
        IF var$cantidad_de_dias_texto IS NULL OR var$cantidad_de_dias_texto = '' THEN
          var$cantidad_de_dias := 0;
        ELSE
          --Es númerico
          IF (LENGTH(TRIM(TRANSLATE(var$cantidad_de_dias_texto, '0123456789', ' '))) IS NULL) THEN
            var$cantidad_de_dias := TO_NUMBER(var$cantidad_de_dias_texto);
          ELSE
            var$cantidad_de_dias := 0;
          END IF;
        END IF;

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'var$cantidad_de_dias_texto = ' || var$cantidad_de_dias_texto,
            par$cod_identificador   => '');

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'var$cantidad_de_dias = ' || var$cantidad_de_dias,
            par$cod_identificador   => '');
        
        FOR i in 0 .. var$cantidad_de_dias LOOP
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR, NEL_CAMPO5)
          VALUES (SYSDATE - i, 'FECHA', i);

          SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Recorre var$cantidad_de_dias i = ' || i,
              par$cod_identificador   => '');
        END LOOP;

        FOR REC$REFERENCE IN C_REFERENCES LOOP  
        
          SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Inserta en tabla SAPR_TSALDO_ACUERDO_REF.',
              par$cod_identificador   => '');          
        
        INSERT INTO SAPR_TSALDO_ACUERDO_REF (OID_SALDO_ACUERDO_REF,
                          OID_PAIS,
                          OID_MAQUINA,
                          OID_ACUERDO_SERVICIO,
                          HOR_SALDO,
                          FEC_SALDO,
                          DES_PRODUCT_CODE,
                          GMT_MODIFICACION,
                          DES_USUARIO_CREACION,
                          GMT_CREACION,
                          DES_USUARIO_MODIFICACION)
                          VALUES
                          
                          (SYS_GUID(),
                          REC$REFERENCE.OID_PAIS,
                          REC$REFERENCE.OID_MAQUINA,
                          REC$REFERENCE.OID_ACUERDO_SERVICIO,
                          REC$REFERENCE.HOR_SALDO,
                          trunc(REC$REFERENCE.HOR_SALDO),
                          REC$REFERENCE.DES_PRODUCT_CODE,
                          gepr_putilidades_###VERSION###.fgmt_zero(),
                          par$cod_usuario,
                          gepr_putilidades_###VERSION###.fgmt_zero(),
                          par$cod_usuario);
                          
        END LOOP;
          
        
        
        FOR REC$SALDO IN C_SALDOS LOOP
          MERGE INTO SAPR_TSALDO_ACUERDO Q1
            USING (
              SELECT                     
                      REC$SALDO.OID_SALDO_ACUERDO_REF AS OID_SALDO_ACUERDO_REF,
                      REC$SALDO.OID_ACUERDO_SERVICIO AS OID_ACUERDO_SERVICIO,
                      REC$SALDO.OID_MAQUINA AS OID_MAQUINA,
                      REC$SALDO.OID_PAIS AS OID_PAIS,
                      REC$SALDO.FEC_SALDO AS FEC_SALDO,
                      REC$SALDO.HOR_SALDO AS HOR_SALDO,
                      --REC$SALDO.OID_PTO_SERVICIO AS OID_PTO_SERVICIO,
                      REC$SALDO.COD_TIPO_MERCANCIA AS COD_TIPO_MERCANCIA,
                      REC$SALDO.OID_DIVISA AS OID_DIVISA,
                      REC$SALDO.NUM_TOTAL_CASHIN AS NUM_TOTAL_CASHIN,
                      REC$SALDO.NEL_CANT_CASHIN AS NEL_CANT_CASHIN,
                      REC$SALDO.NUM_TOTAL_SHIPOUT AS NUM_TOTAL_SHIPOUT,
                      REC$SALDO.NEL_CANT_SHIPOUT AS NEL_CANT_SHIPOUT,
                      REC$SALDO.NUM_TOTAL_ACREDITACION AS NUM_TOTAL_ACREDITACION,
                      REC$SALDO.NEL_CANT_ACREDITACION AS NEL_CANT_ACREDITACION,
                      REC$SALDO.NUM_TOTAL_CASHIN_ACRED AS NUM_TOTAL_CASHIN_ACRED,
                      REC$SALDO.NEL_CANT_CASHIN_ACRED AS NEL_CANT_CASHIN_ACRED,
                      REC$SALDO.NUM_TOTAL_COMISION AS NUM_TOTAL_COMISION,
                      REC$SALDO.FYH_INICIO_VIGENCIA_PLAN AS FYH_INICIO_VIGENCIA_PLAN
              FROM DUAL
            ) Q2
            ON (Q1.OID_SALDO_ACUERDO_REF = Q2.OID_SALDO_ACUERDO_REF AND 
                Q1.OID_PAIS = Q2.OID_PAIS AND 
                Q1.FEC_SALDO = Q2.FEC_SALDO AND 
                Q1.COD_TIPO_MERCANCIA = Q2.COD_TIPO_MERCANCIA AND 
                Q1.OID_DIVISA = Q2.OID_DIVISA AND 
                NVL(Q1.OID_ACUERDO_SERVICIO, '-') = NVL(Q2.OID_ACUERDO_SERVICIO, '-') AND 
                NVL(Q1.OID_MAQUINA, '-') = NVL(Q2.OID_MAQUINA, '-') AND  ROWNUM <= 1)
            WHEN MATCHED THEN
              UPDATE
              SET
              Q1.NUM_TOTAL_CASHIN = Q2.NUM_TOTAL_CASHIN,
              Q1.NEL_CANT_CASHIN = Q2.NEL_CANT_CASHIN,
              Q1.NUM_TOTAL_SHIPOUT = Q2.NUM_TOTAL_SHIPOUT,
              Q1.NEL_CANT_SHIPOUT = Q2.NEL_CANT_SHIPOUT,
              Q1.NUM_TOTAL_CASHIN_ACRED = Q2.NUM_TOTAL_CASHIN_ACRED,
              Q1.NEL_CANT_CASHIN_ACRED = Q2.NEL_CANT_CASHIN_ACRED,
              Q1.NUM_TOTAL_ACREDITACION = Q2.NUM_TOTAL_ACREDITACION,
              Q1.NEL_CANT_ACREDITACION = Q2.NEL_CANT_ACREDITACION,
              Q1.NUM_TOTAL_COMISION = Q2.NUM_TOTAL_COMISION,
              Q1.FYH_INICIO_VIGENCIA_PLAN = Q2.FYH_INICIO_VIGENCIA_PLAN,
              Q1.GMT_MODIFICACION = gepr_putilidades_###VERSION###.fgmt_zero(),
              Q1.DES_USUARIO_MODIFICACION = par$cod_usuario
              
            WHEN NOT MATCHED THEN
              INSERT
                ( Q1.OID_SALDO_ACUERDO,
                  Q1.OID_SALDO_ACUERDO_REF,
                  Q1.OID_ACUERDO_SERVICIO,
                  Q1.OID_MAQUINA,
                  Q1.OID_PAIS,
                  Q1.FEC_SALDO,
                  Q1.HOR_SALDO,
                  Q1.OID_DIVISA,
                  Q1.COD_TIPO_MERCANCIA,
                  Q1.NUM_TOTAL_CASHIN,
                  Q1.NEL_CANT_CASHIN,
                  Q1.NUM_TOTAL_SHIPOUT,
                  Q1.NEL_CANT_SHIPOUT,
                  Q1.NUM_TOTAL_CASHIN_ACRED,
                  Q1.NEL_CANT_CASHIN_ACRED,
                  Q1.NUM_TOTAL_ACREDITACION,
                  Q1.NEL_CANT_ACREDITACION,
                  Q1.NUM_TOTAL_COMISION,
                  Q1.FYH_INICIO_VIGENCIA_PLAN,
                  Q1.GMT_CREACION,
                  Q1.DES_USUARIO_CREACION,
                  Q1.GMT_MODIFICACION,
                  Q1.DES_USUARIO_MODIFICACION)
                  VALUES
                  (SYS_GUID(),
                  Q2.OID_SALDO_ACUERDO_REF,
                  Q2.OID_ACUERDO_SERVICIO,
                  Q2.OID_MAQUINA,
                  Q2.OID_PAIS,
                  Q2.FEC_SALDO,
                  Q2.HOR_SALDO,
                  Q2.OID_DIVISA,
                  Q2.COD_TIPO_MERCANCIA,
                  Q2.NUM_TOTAL_CASHIN,
                  Q2.NEL_CANT_CASHIN,
                  Q2.NUM_TOTAL_SHIPOUT,
                  Q2.NEL_CANT_SHIPOUT,
                  Q2.NUM_TOTAL_CASHIN_ACRED,
                  Q2.NEL_CANT_CASHIN_ACRED,
                  Q2.NUM_TOTAL_ACREDITACION,
                  Q2.NEL_CANT_ACREDITACION,
                  Q2.NUM_TOTAL_COMISION,
                  Q2.FYH_INICIO_VIGENCIA_PLAN,
                  gepr_putilidades_###VERSION###.fgmt_zero(),
                  par$cod_usuario,
                  gepr_putilidades_###VERSION###.fgmt_zero(),
                  par$cod_usuario);
                  
        END LOOP C_SALDOS;

          SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Finaliza cursor C_SALDOS.',
              par$cod_identificador   => '');

        MERGE INTO GEPR_TINTEGRACION T1
        USING (               
              SELECT DISTINCT
              SARE.OID_SALDO_ACUERDO_REF as OID_TABLA_INTEGRACION,
              'SAPR_TSALDO_ACUERDO_REF' AS COD_TABLA_INTEGRACION,
              'AB' AS COD_ESTADO,
              'GEPR' AS COD_MODULO_ORIGEN,
              'API_GLOBAL' AS COD_MODULO_DESTINO,
              'Notificacion' AS COD_PROCESO
              
              FROM SAPR_TSALDO_ACUERDO_REF SARE
              INNER JOIN SAPR_TSALDO_ACUERDO SACU ON SACU.OID_SALDO_ACUERDO_REF = SARE.OID_SALDO_ACUERDO_REF
              WHERE SARE.BOL_CALCULADO = '0'
              AND SARE.HOR_SALDO BETWEEN (var$fecha -180) AND var$fecha 
              AND SARE.FEC_SALDO BETWEEN (TRUNC(var$fecha) -180) AND TRUNC(var$fecha)
          ) T2
          ON (T1.OID_TABLA_INTEGRACION = T2.OID_TABLA_INTEGRACION AND 
              T1.COD_TABLA_INTEGRACION = T2.COD_TABLA_INTEGRACION AND 
              T1.COD_MODULO_ORIGEN = T2.COD_MODULO_ORIGEN AND 
              T1.COD_MODULO_DESTINO = T2.COD_MODULO_DESTINO AND
              T1.COD_PROCESO = T2.COD_PROCESO AND ROWNUM <= 1) 
          WHEN MATCHED THEN
            UPDATE
            SET
                T1.COD_ESTADO = 'MD',
                T1.NEL_INTENTO_ENVIO = 0,
                T1.GMT_MODIFICACION = gepr_putilidades_###VERSION###.fgmt_zero(),
                T1.DES_USUARIO_MODIFICACION = par$cod_usuario
                WHERE T1.COD_ESTADO <> 'AB' 
              
          WHEN NOT MATCHED THEN
            INSERT 
              (
                T1.OID_INTEGRACION, 
                T1.OID_TABLA_INTEGRACION, 
                T1.COD_TABLA_INTEGRACION, 
                T1.COD_ESTADO, 
                T1.COD_MODULO_ORIGEN, 
                T1.COD_MODULO_DESTINO, 
                T1.COD_PROCESO, 
                T1.NEL_INTENTO_ENVIO, 
                T1.GMT_CREACION, 
                T1.DES_USUARIO_CREACION, 
                T1.GMT_MODIFICACION, 
                T1.DES_USUARIO_MODIFICACION
              )   
              VALUES(       
              
                sys_guid(),
                T2.OID_TABLA_INTEGRACION,
                T2.COD_TABLA_INTEGRACION,
                T2.COD_ESTADO,
                T2.COD_MODULO_ORIGEN,
                T2.COD_MODULO_DESTINO,
                T2.COD_PROCESO,
                0,
                gepr_putilidades_###VERSION###.fgmt_zero(),
                par$cod_usuario,
                gepr_putilidades_###VERSION###.fgmt_zero(),
                par$cod_usuario 
              );

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Actualiza SAPR_TSALDO_ACUERDO_REF.',
            par$cod_identificador   => '');

        UPDATE SAPR_TSALDO_ACUERDO_REF 
        SET BOL_CALCULADO = 1, 
            GMT_MODIFICACION = gepr_putilidades_###VERSION###.fgmt_zero(),
            DES_USUARIO_MODIFICACION = par$cod_usuario
        WHERE  
          HOR_SALDO BETWEEN (SYSTIMESTAMP -180) AND SYSTIMESTAMP 
          AND FEC_SALDO BETWEEN (TRUNC(SYSTIMESTAMP) -180) AND TRUNC(SYSTIMESTAMP)
          AND BOL_CALCULADO = 0;
              
        COMMIT;
      END IF; --  var$oid_pais IS NOT NULL

      SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Open par$rc_validaciones',
          par$cod_identificador   => '');

      OPEN par$rc_validaciones FOR
        SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CALIFICADOR AS CALIFICADOR
        FROM SAPR_GTT_TAUXILIAR AUX
        WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';

			SAPR_PLOG_API.SAGREGA_DETALLE(
					par$oid_llamada  => par$oid_llamada,
					par$des_origen      => const$nombre_func,
					par$des_version     => const$version,
					par$des_detalle     => 'Fin OK',
					par$cod_identificador   => '');

	EXCEPTION WHEN OTHERS THEN
    ROLLBACK;
    SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Hubo una excepción' || sqlerrm,
        par$cod_identificador   => '');
  END sactualizar_saldo_acuerdo;

PROCEDURE srecalcular_saldo_acuerdo(par$oid_saldo_acuerdo_ref   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$rc_validaciones         OUT sys_refcursor
                                    ) 
   IS
  BEGIN
    /* #### Limpiamos la tabla auxiliar #### */
    DELETE FROM SAPR_GTT_TAUXILIAR;
    COMMIT;

    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; 

    -- Validamos si debo calcular un par$oid_saldo_acuerdo_ref enviado por parametro
    IF par$oid_saldo_acuerdo_ref IS NOT NULL THEN
      MERGE INTO SAPR_TSALDO_ACUERDO Q1
          USING (
            WITH DATOS AS (
              -- CASHINES, ACREDITACIONES, COMISIONES  
              SELECT    SARE.OID_SALDO_ACUERDO_REF,
                SARE.OID_ACUERDO_SERVICIO,
                SARE.OID_MAQUINA,
                SARE.OID_PAIS,
                SARE.FEC_SALDO,
                SARE.HOR_SALDO,
                EFDO.OID_DIVISA,
                CASE CUEN.COD_SUBCANAL 
                  WHEN 'INGBIL' THEN '1'      
                  WHEN 'INGMON' THEN '2'      
                  WHEN 'DEB' THEN '0'      
                  WHEN 'DB' THEN '0'      
                  WHEN 'SF' THEN '0' END COD_TIPO_MERCANCIA,
                SUM(CASE WHEN FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID') THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_CASHIN,
                COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID') THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_CASHIN,
                0 NUM_TOTAL_SHIPOUT,
                0 NEL_CANT_SHIPOUT,
                0 NUM_TOTAL_CASHIN_ACRED,
                0 NEL_CANT_CASHIN_ACRED,
                SUM(CASE WHEN FORM.COD_FORMULARIO = 'MAEACR' THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_ACREDITACION,
                COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO = 'MAEACR' THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_ACREDITACION,    
                SUM(CASE WHEN FORM.COD_FORMULARIO = 'MAECOM' THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_COMISION,
                COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO = 'MAECOM' THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_COMISION,  
                PLMA.FYH_VIGENCIA_INICIO FYH_INICIO_VIGENCIA_PLAN
              FROM    SAPR_TSALDO_ACUERDO_REF SARE
                INNER JOIN SAPR_TACUERDO_SERVICIO ACSE ON SARE.OID_ACUERDO_SERVICIO = ACSE.OID_ACUERDO_SERVICIO AND                                              
                                                          SARE.OID_PAIS = ACSE.OID_PAIS AND                                              
                                                          ACSE.BOL_VIGENTE = '1'   
                INNER JOIN SAPR_VCUENTA CUEN ON ACSE.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO AND                                    
                                                          SARE.OID_MAQUINA = CUEN.OID_MAQUINA AND                                    
                                                          CUEN.COD_SUBCANAL IN ('INGBIL', 'INGMON', 'DEB', 'DB', 'SF')
                INNER JOIN SAPR_TDOCUMENTO DOCU ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN AND                                       
                      CUEN.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN AND                                       
                      DOCU.GMT_CREACION BETWEEN (SARE.HOR_SALDO - INTERVAL '23:59:59' HOUR TO SECOND) AND SARE.HOR_SALDO
                INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO AND                                        
                                                    FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID', 'MAEACR', 'MAECOM')
                INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EFDO ON DOCU.OID_DOCUMENTO = EFDO.OID_DOCUMENTO  
                LEFT JOIN SAPR_TPLANXMAQUINA PLMA ON SARE.OID_MAQUINA = PLMA.OID_MAQUINA AND                                         
                                                    PLMA.BOL_ACTIVO = '1'
              WHERE SARE.OID_SALDO_ACUERDO_REF = par$oid_saldo_acuerdo_ref
              GROUP BY    
                  SARE.OID_SALDO_ACUERDO_REF,
                  SARE.OID_ACUERDO_SERVICIO,
                  SARE.OID_MAQUINA,
                  SARE.OID_PAIS,
                  SARE.FEC_SALDO,
                  SARE.HOR_SALDO,
                  EFDO.OID_DIVISA,
                  CUEN.COD_SUBCANAL,
                  PLMA.FYH_VIGENCIA_INICIO
              UNION  -- SHIPOUTES  
              SELECT    SARE.OID_SALDO_ACUERDO_REF,
                SARE.OID_ACUERDO_SERVICIO,
                SARE.OID_MAQUINA,
                SARE.OID_PAIS,
                SARE.FEC_SALDO,
                SARE.HOR_SALDO,
                EFDO.OID_DIVISA,
                CASE CUEN.COD_SUBCANAL 
                  WHEN 'INGBIL' THEN '1'      
                  WHEN 'INGMON' THEN '2'      
                  WHEN 'DEB' THEN '0'      
                  WHEN 'DB' THEN '0'    END COD_TIPO_MERCANCIA,
                0 NUM_TOTAL_CASHIN,
                0 NEL_CANT_CASHIN,
                SUM(CASE WHEN FORM.COD_FORMULARIO IN ('MAESOC', 'MAESOD') THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_SHIPOUT,
                COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO IN ('MAESOC', 'MAESOD') THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_SHIPOUT,
                0 NUM_TOTAL_CASHIN_ACRED,
                0 NEL_CANT_CASHIN_ACRED,
                0 NUM_TOTAL_ACREDITACION,
                0 NEL_CANT_ACREDITACION,
                0 NUM_TOTAL_COMISION,
                0 NEL_CANT_COMISION,  
                PLMA.FYH_VIGENCIA_INICIO FYH_INICIO_VIGENCIA_PLAN
              FROM    SAPR_TSALDO_ACUERDO_REF SARE
                INNER JOIN SAPR_TACUERDO_SERVICIO ACSE ON SARE.OID_ACUERDO_SERVICIO = ACSE.OID_ACUERDO_SERVICIO AND                                              
                                                          SARE.OID_PAIS = ACSE.OID_PAIS AND                                              
                                                          ACSE.BOL_VIGENTE = '1'    
                INNER JOIN SAPR_VCUENTA CUEN ON SARE.OID_MAQUINA = CUEN.OID_MAQUINA AND                                    
                                                CUEN.COD_SUBCANAL IN ('INGBIL', 'INGMON', 'DEB', 'DB') AND                                    
                                                CUEN.COD_CLIENTE = '999999' -- PROSEGUR    
                INNER JOIN SAPR_TDOCUMENTO DOCU ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN AND                                       
                                                    CUEN.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN AND                                       
                                                    DOCU.GMT_CREACION BETWEEN (SARE.HOR_SALDO - INTERVAL '23:59:59' HOUR TO SECOND) AND SARE.HOR_SALDO
                INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO AND                                        
                                                    FORM.COD_FORMULARIO IN ('MAESOC', 'MAESOD')
                INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EFDO ON DOCU.OID_DOCUMENTO = EFDO.OID_DOCUMENTO  
                LEFT JOIN SAPR_TPLANXMAQUINA PLMA ON SARE.OID_MAQUINA = PLMA.OID_MAQUINA AND                                         
                                                    PLMA.BOL_ACTIVO = '1'  
              WHERE SARE.OID_SALDO_ACUERDO_REF = par$oid_saldo_acuerdo_ref
                GROUP BY    SARE.OID_SALDO_ACUERDO_REF,
                SARE.OID_ACUERDO_SERVICIO,
                SARE.OID_MAQUINA,
                SARE.OID_PAIS,
                SARE.FEC_SALDO,
                SARE.HOR_SALDO,
                EFDO.OID_DIVISA,
                CUEN.COD_SUBCANAL,
                PLMA.FYH_VIGENCIA_INICIO
              UNION  -- CASHINES ACREDITADOS  
              SELECT    SARE.OID_SALDO_ACUERDO_REF,
                SARE.OID_ACUERDO_SERVICIO,
                SARE.OID_MAQUINA,
                SARE.OID_PAIS,
                SARE.FEC_SALDO,
                SARE.HOR_SALDO,
                EFDO.OID_DIVISA,
                CASE CUEN.COD_SUBCANAL 
                  WHEN 'INGBIL' THEN '1'      
                  WHEN 'INGMON' THEN '2'      
                  WHEN 'DEB' THEN '0'      
                  WHEN 'DB' THEN '0'    
                  END COD_TIPO_MERCANCIA,
                0 NUM_TOTAL_CASHIN,
                0 NEL_CANT_CASHIN,
                0 NUM_TOTAL_SHIPOUT,
                0 NEL_CANT_SHIPOUT,
                SUM(CASE WHEN FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID') THEN EFDO.NUM_IMPORTE ELSE 0 END) NUM_TOTAL_CASHIN_ACRED,
                COUNT(DISTINCT CASE WHEN FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID') THEN DOCU.OID_DOCUMENTO ELSE NULL END) NEL_CANT_CASHIN_ACRED,
                0 NUM_TOTAL_ACREDITACION,
                0 NEL_CANT_ACREDITACION,
                0 NUM_TOTAL_COMISION,
                0 NEL_CANT_COMISION,  
                PLMA.FYH_VIGENCIA_INICIO FYH_INICIO_VIGENCIA_PLAN
              FROM    SAPR_TSALDO_ACUERDO_REF SARE
                INNER JOIN SAPR_TACUERDO_SERVICIO ACSE ON SARE.OID_ACUERDO_SERVICIO = ACSE.OID_ACUERDO_SERVICIO AND                                              
                                                          SARE.OID_PAIS = ACSE.OID_PAIS AND                                              
                                                          ACSE.BOL_VIGENTE = '1'    
                INNER JOIN SAPR_VCUENTA CUEN ON ACSE.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO AND                                    
                SARE.OID_MAQUINA = CUEN.OID_MAQUINA AND                                    CUEN.COD_SUBCANAL IN ('INGBIL', 'INGMON', 'DEB', 'DB')
                INNER JOIN SAPR_TDOCUMENTO DOCU ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN AND                                       
                                                  CUEN.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN AND                                       
                                                  DOCU.FYH_ACREDITACION BETWEEN (SARE.HOR_SALDO - INTERVAL '23:59:59' HOUR TO SECOND) AND SARE.HOR_SALDO AND                                       
                                                  DOCU.BOL_ACREDITADO = '1'    
                INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO AND                                        
                                                    FORM.COD_FORMULARIO IN ('MAECIC', 'MAECID')
                INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EFDO ON DOCU.OID_DOCUMENTO = EFDO.OID_DOCUMENTO  
                LEFT JOIN SAPR_TPLANXMAQUINA PLMA ON SARE.OID_MAQUINA = PLMA.OID_MAQUINA AND                                         
                PLMA.BOL_ACTIVO = '1'  
                WHERE SARE.OID_SALDO_ACUERDO_REF = par$oid_saldo_acuerdo_ref
                GROUP BY    SARE.OID_SALDO_ACUERDO_REF,
                SARE.OID_ACUERDO_SERVICIO,
                SARE.OID_MAQUINA,
                SARE.OID_PAIS,
                SARE.FEC_SALDO,
                SARE.HOR_SALDO,
                EFDO.OID_DIVISA,
                CUEN.COD_SUBCANAL,
                PLMA.FYH_VIGENCIA_INICIO
            )
            SELECT  DATO.OID_SALDO_ACUERDO_REF,
              DATO.OID_ACUERDO_SERVICIO,
              DATO.OID_MAQUINA,
              DATO.OID_PAIS,
              DATO.FEC_SALDO,
              DATO.HOR_SALDO,
              DATO.OID_DIVISA,
              DATO.COD_TIPO_MERCANCIA, 
              SUM(DATO.NUM_TOTAL_CASHIN) AS NUM_TOTAL_CASHIN,
              SUM(DATO.NEL_CANT_CASHIN) AS NEL_CANT_CASHIN,
              SUM(DATO.NUM_TOTAL_SHIPOUT) AS NUM_TOTAL_SHIPOUT,
              SUM(DATO.NEL_CANT_SHIPOUT) AS NEL_CANT_SHIPOUT,
              SUM(DATO.NUM_TOTAL_CASHIN_ACRED) AS NUM_TOTAL_CASHIN_ACRED,
              SUM(DATO.NEL_CANT_CASHIN_ACRED) AS NEL_CANT_CASHIN_ACRED,
              SUM(DATO.NUM_TOTAL_ACREDITACION) AS NUM_TOTAL_ACREDITACION,
              SUM(DATO.NEL_CANT_ACREDITACION) AS NEL_CANT_ACREDITACION,
              SUM(DATO.NUM_TOTAL_COMISION) AS NUM_TOTAL_COMISION,
              SUM(DATO.NEL_CANT_COMISION) AS NEL_CANT_COMISION,
              DATO.FYH_INICIO_VIGENCIA_PLAN
            FROM  DATOS DATO
            GROUP BY  DATO.OID_SALDO_ACUERDO_REF,
              DATO.OID_ACUERDO_SERVICIO,
              DATO.OID_MAQUINA,
              DATO.OID_PAIS,
              DATO.FEC_SALDO,
              DATO.HOR_SALDO,
              DATO.OID_DIVISA,
              DATO.COD_TIPO_MERCANCIA,
              DATO.FYH_INICIO_VIGENCIA_PLAN
          ) Q2
          ON (Q1.OID_SALDO_ACUERDO_REF = Q2.OID_SALDO_ACUERDO_REF AND 
              Q1.OID_PAIS = Q2.OID_PAIS AND 
              Q1.FEC_SALDO = Q2.FEC_SALDO AND 
              Q1.COD_TIPO_MERCANCIA = Q2.COD_TIPO_MERCANCIA AND 
              Q1.OID_DIVISA = Q2.OID_DIVISA AND 
              NVL(Q1.OID_ACUERDO_SERVICIO, '-') = NVL(Q2.OID_ACUERDO_SERVICIO, '-') AND 
              NVL(Q1.OID_MAQUINA, '-') = NVL(Q2.OID_MAQUINA, '-') AND  ROWNUM <= 1)
          WHEN MATCHED THEN
            UPDATE
            SET
            Q1.NUM_TOTAL_CASHIN = Q2.NUM_TOTAL_CASHIN,
            Q1.NEL_CANT_CASHIN = Q2.NEL_CANT_CASHIN,
            Q1.NUM_TOTAL_SHIPOUT = Q2.NUM_TOTAL_SHIPOUT,
            Q1.NEL_CANT_SHIPOUT = Q2.NEL_CANT_SHIPOUT,
            Q1.NUM_TOTAL_CASHIN_ACRED = Q2.NUM_TOTAL_CASHIN_ACRED,
            Q1.NEL_CANT_CASHIN_ACRED = Q2.NEL_CANT_CASHIN_ACRED,
            Q1.NUM_TOTAL_ACREDITACION = Q2.NUM_TOTAL_ACREDITACION,
            Q1.NEL_CANT_ACREDITACION = Q2.NEL_CANT_ACREDITACION,
            Q1.NUM_TOTAL_COMISION = Q2.NUM_TOTAL_COMISION,
            Q1.FYH_INICIO_VIGENCIA_PLAN = Q2.FYH_INICIO_VIGENCIA_PLAN,
            Q1.GMT_MODIFICACION = gepr_putilidades_###VERSION###.fgmt_zero(),
            Q1.DES_USUARIO_MODIFICACION = par$cod_usuario
            
          WHEN NOT MATCHED THEN
            INSERT
              ( Q1.OID_SALDO_ACUERDO,
                Q1.OID_SALDO_ACUERDO_REF,
                Q1.OID_ACUERDO_SERVICIO,
                Q1.OID_MAQUINA,
                Q1.OID_PAIS,
                Q1.FEC_SALDO,
                Q1.HOR_SALDO,
                Q1.OID_DIVISA,
                Q1.COD_TIPO_MERCANCIA,
                Q1.NUM_TOTAL_CASHIN,
                Q1.NEL_CANT_CASHIN,
                Q1.NUM_TOTAL_SHIPOUT,
                Q1.NEL_CANT_SHIPOUT,
                Q1.NUM_TOTAL_CASHIN_ACRED,
                Q1.NEL_CANT_CASHIN_ACRED,
                Q1.NUM_TOTAL_ACREDITACION,
                Q1.NEL_CANT_ACREDITACION,
                Q1.NUM_TOTAL_COMISION,
                Q1.FYH_INICIO_VIGENCIA_PLAN,
                Q1.GMT_CREACION,
                Q1.DES_USUARIO_CREACION,
                Q1.GMT_MODIFICACION,
                Q1.DES_USUARIO_MODIFICACION)
                VALUES
                (SYS_GUID(),
                Q2.OID_SALDO_ACUERDO_REF,
                Q2.OID_ACUERDO_SERVICIO,
                Q2.OID_MAQUINA,
                Q2.OID_PAIS,
                Q2.FEC_SALDO,
                Q2.HOR_SALDO,
                Q2.OID_DIVISA,
                Q2.COD_TIPO_MERCANCIA,
                Q2.NUM_TOTAL_CASHIN,
                Q2.NEL_CANT_CASHIN,
                Q2.NUM_TOTAL_SHIPOUT,
                Q2.NEL_CANT_SHIPOUT,
                Q2.NUM_TOTAL_CASHIN_ACRED,
                Q2.NEL_CANT_CASHIN_ACRED,
                Q2.NUM_TOTAL_ACREDITACION,
                Q2.NEL_CANT_ACREDITACION,
                Q2.NUM_TOTAL_COMISION,
                Q2.FYH_INICIO_VIGENCIA_PLAN,
                gepr_putilidades_###VERSION###.fgmt_zero(),
                par$cod_usuario,
                gepr_putilidades_###VERSION###.fgmt_zero(),
                par$cod_usuario);

      -- Actualizo el BOL_CALCULADO de la tabla SAPR_TSALDO_ACUERDO_REF
      UPDATE SAPR_TSALDO_ACUERDO_REF 
      SET BOL_CALCULADO = CASE WHEN TRUNC(SYSDATE) > TRUNC(FEC_SALDO) THEN 1 ELSE BOL_CALCULADO END, -- No se puede poner calculado en 1 para el día actual 
          GMT_MODIFICACION = gepr_putilidades_###VERSION###.fgmt_zero(),
          DES_USUARIO_MODIFICACION = par$cod_usuario
      WHERE OID_SALDO_ACUERDO_REF = par$oid_saldo_acuerdo_ref; 

    ELSE
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
      VALUES ('OID_SALDO_ACUERDO_REF', 'Se debe indicar un oid_saldo_acuerdo_ref', 'VALIDACIONES');
    END IF;
  
    OPEN par$rc_validaciones FOR
          SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CALIFICADOR AS CALIFICADOR
          FROM SAPR_GTT_TAUXILIAR AUX
          WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';

    COMMIT;

	EXCEPTION WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                      SQLCODE || ' - ' || SQLERRM,
                      true);
  END srecalcular_saldo_acuerdo;

  PROCEDURE snotificar_saldo_acuerdo(par$oid_saldo_acuerdo_ref   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$rc_validaciones         OUT sys_refcursor
                                    ) 
   IS
   var$existe             gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN
    /* #### Limpiamos la tabla auxiliar #### */
    DELETE FROM SAPR_GTT_TAUXILIAR;
    COMMIT;

    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; 

    -- Validamos si debo calcular un par$oid_saldo_acuerdo_ref enviado por parametro
    IF par$oid_saldo_acuerdo_ref IS NOT NULL THEN
      BEGIN
        SELECT COUNT(1)
          INTO var$existe
          FROM GEPR_TINTEGRACION
         WHERE OID_TABLA_INTEGRACION = par$oid_saldo_acuerdo_ref
         AND COD_TABLA_INTEGRACION = 'SAPR_TSALDO_ACUERDO_REF'
         AND COD_ESTADO = 'AB'
         AND COD_MODULO_ORIGEN = 'GEPR'
         AND COD_MODULO_DESTINO = 'API_GLOBAL'
         AND COD_PROCESO = 'Notificacion';
      END;

      IF var$existe = 0 THEN
          INSERT INTO GEPR_TINTEGRACION (OID_INTEGRACION, OID_TABLA_INTEGRACION, COD_TABLA_INTEGRACION, COD_ESTADO, COD_MODULO_ORIGEN, COD_MODULO_DESTINO, COD_PROCESO, NEL_INTENTO_ENVIO, 
                                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
          VALUES (sys_guid(), par$oid_saldo_acuerdo_ref, 'SAPR_TSALDO_ACUERDO_REF', 'AB', 'GEPR', 'API_GLOBAL', 'Notificacion', 0,
              gepr_putilidades_###VERSION###.fgmt_zero(), par$cod_usuario, gepr_putilidades_###VERSION###.fgmt_zero(), par$cod_usuario );
      END IF;
    ELSE
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
      VALUES ('OID_SALDO_ACUERDO_REF', 'Se debe indicar un oid_saldo_acuerdo_ref', 'VALIDACIONES');
    END IF;
  
    OPEN par$rc_validaciones FOR
          SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CALIFICADOR AS CALIFICADOR
          FROM SAPR_GTT_TAUXILIAR AUX
          WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';

    COMMIT;

	EXCEPTION WHEN OTHERS THEN
    ROLLBACK;
    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                      SQLCODE || ' - ' || SQLERRM,
                      true);
  END snotificar_saldo_acuerdo;
  
  PROCEDURE sborrar_saldos_hist_cli (par$oid_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_pais                IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_) IS


  BEGIN



    /* #### Inicializar los cursores #### */


   /* ##### LIMPIAR TABLAS TEMPORARIAS ##### */
    DELETE SAPR_GTT_TAUXILIAR;
    COMMIT;


        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,  COD_CALIFICADOR)
          SELECT
          DISTINCT CUEN.OID_CUENTA, CLIE.OID_CLIENTE, MAQU.OID_MAQUINA, DELE.OID_PAIS, 'OID_CUENTAS'
          FROM
          GEPR_TCLIENTE CLIE
          INNER JOIN SAPR_TCUENTA CUEN ON CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
          INNER JOIN GEPR_TSECTOR SECT ON CUEN.OID_SECTOR = SECT.OID_SECTOR
          INNER JOIN SAPR_TMAQUINA MAQU ON SECT.OID_SECTOR = MAQU.OID_SECTOR
          INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
          INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
          WHERE
          --DELE.OID_PAIS = '?' AND
          CLIE.OID_CLIENTE = par$oid_cliente;


          delete sapr_Tsaldo_efectivo_historico SAHI where EXISTS ( SELECT 1 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'OID_CUENTAS' AND AUX.OID_CAMPO1 = SAHI.OID_CUENTA );
          delete Sapr_tcontrol_saldo_historico HIST where EXISTS ( SELECT 1 FROM SAPR_GTT_TAUXILIAR AUX
                                                                          WHERE AUX.COD_CALIFICADOR = 'OID_CUENTAS'
                                                                                AND AUX.COD_CAMPO2 = HIST.OID_CLIENTE
                                                                                AND AUX.COD_CAMPO3 = HIST.OID_MAQUINA
                                                                                AND AUX.COD_CAMPO4 = HIST.OID_PAIS );






    COMMIT;

  END sborrar_saldos_hist_cli;


  PROCEDURE sactualizar_saldos_hist_cli (par$oid_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_pais                IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_) IS
  CURSOR C_CLIENTES IS
    SELECT
    DISTINCT CLIE.OID_CLIENTE, MAQU.OID_MAQUINA, DELE.OID_PAIS
    FROM
    GEPR_TCLIENTE CLIE
    INNER JOIN SAPR_TCUENTA CUEN ON CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
    INNER JOIN GEPR_TSECTOR SECT ON CUEN.OID_SECTOR = SECT.OID_SECTOR
    INNER JOIN SAPR_TMAQUINA MAQU ON SECT.OID_SECTOR = MAQU.OID_SECTOR
    INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
    INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
    WHERE
    --DELE.OID_PAIS = '?' AND
    CLIE.OID_CLIENTE = par$oid_cliente
    GROUP BY CLIE.OID_CLIENTE, MAQU.OID_MAQUINA, DELE.OID_PAIS;

    var$rc_validaciones   sys_refcursor;

  BEGIN





   /* ##### LIMPIAR TABLAS TEMPORARIAS ##### */
    DELETE SAPR_GTT_TAUXILIAR;
    COMMIT;
    /* #### Inicializar los cursores #### */

        FOR rec$cliente IN C_CLIENTES LOOP
          sactualizar_saldos_historico(
            par$oid_cliente             => rec$cliente.OID_CLIENTE,
            par$oid_maquina             => rec$cliente.OID_MAQUINA,
            par$oid_pais                => rec$cliente.OID_PAIS,
            par$cod_tipo_actualizacion  => 'ANTERIOR',
            par$cod_cultura             => par$cod_cultura,
            par$cod_usuario             => par$cod_usuario,
            par$rc_validaciones         => var$rc_validaciones
            );

    END LOOP C_CLIENTES;
    COMMIT;

  END sactualizar_saldos_hist_cli;



  PROCEDURE sactualizar_saldos_historico(par$oid_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_maquina             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_pais                IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_tipo_actualizacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rc_validaciones         OUT sys_refcursor
  ) IS
  var$mensaje gepr_pcomon_###VERSION###.tipo$desc_;

  CURSOR C_SALDOS IS
  WITH CUENTAS AS (
    SELECT
        DISTINCT
        CUEN.OID_CUENTA,
        MAQU.OID_MAQUINA,
        DELE.OID_PAIS,
        TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN SYSDATE >= DELE.FYH_VERANO_INICIO AND SYSDATE <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' || TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN SYSDATE >= DELE.FYH_VERANO_INICIO AND SYSDATE <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')) GMT,
        CLIE.COD_FECHA_SALDO_HISTORICO
    FROM
        GEPR_TCLIENTE CLIE
        INNER JOIN SAPR_TCUENTA CUEN ON CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
        INNER JOIN GEPR_TSECTOR SECT ON CUEN.OID_SECTOR = SECT.OID_SECTOR
        INNER JOIN SAPR_TMAQUINA MAQU ON SECT.OID_SECTOR = MAQU.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
        INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
    WHERE
        CLIE.OID_CLIENTE = par$oid_cliente AND
        MAQU.OID_MAQUINA = par$oid_maquina AND
        DELE.OID_PAIS = par$oid_pais
  ), SALDOS AS (
    SELECT
        CUEN.OID_CUENTA,
        CUEN.OID_MAQUINA,
        CUEN.OID_PAIS,
        TRUNC(SYSDATE) FEC_SALDO,
        TO_TIMESTAMP_TZ(TO_CHAR(SYSDATE, 'DD/MM/YYYY') || ' 23:59:59 ' || CUEN.GMT, 'DD/MM/YYYY HH24:MI:SS TZH:TZM') HOR_SALDO,
        TREF.OID_DENOMINACION,
        TREF.OID_DIVISA,
        TREF.COD_NIVEL_DETALLE,
        TREF.BOL_DISPONIBLE,
        NVL(TREF.NUM_IMPORTE, 0) NUM_IMPORTE,
        NVL(TREF.NEL_CANTIDAD, 0) NEL_CANTIDAD,
        CUEN.COD_FECHA_SALDO_HISTORICO,
        'D' TIPO_SALDO
    FROM
        SAPR_TTRANSACCION_EFECTIVO TREF
        INNER JOIN CUENTAS CUEN ON TREF.OID_CUENTA_SALDO = CUEN.OID_CUENTA AND
                                   CUEN.COD_FECHA_SALDO_HISTORICO = 'CREACION'
    WHERE
        TREF.GMT_CREACION >= TO_TIMESTAMP_TZ(TO_CHAR(SYSDATE, 'DD/MM/YYYY') || ' 00:00:00 ' || CUEN.GMT, 'DD/MM/YYYY HH24:MI:SS TZH:TZM') AND
        TREF.GMT_CREACION <= TO_TIMESTAMP_TZ(TO_CHAR(SYSDATE, 'DD/MM/YYYY') || ' 23:59:59 ' || CUEN.GMT, 'DD/MM/YYYY HH24:MI:SS TZH:TZM')
    UNION ALL
      SELECT
        CUEN.OID_CUENTA,
        CUEN.OID_MAQUINA,
        CUEN.OID_PAIS,
        TRUNC(SYSDATE) FEC_SALDO,
        TO_TIMESTAMP_TZ(TO_CHAR(SYSDATE, 'DD/MM/YYYY') || ' 23:59:59 ' || CUEN.GMT, 'DD/MM/YYYY HH24:MI:SS TZH:TZM') HOR_SALDO,
        TREF.OID_DENOMINACION,
        TREF.OID_DIVISA,
        TREF.COD_NIVEL_DETALLE,
        TREF.BOL_DISPONIBLE,
        NVL(TREF.NUM_IMPORTE, 0) NUM_IMPORTE,
        NVL(TREF.NEL_CANTIDAD, 0) NEL_CANTIDAD,
        CUEN.COD_FECHA_SALDO_HISTORICO,
        'D' TIPO_SALDO
      FROM
        SAPR_TTRANSACCION_EFECTIVO TREF
        INNER JOIN CUENTAS CUEN ON TREF.OID_CUENTA_SALDO = CUEN.OID_CUENTA AND
                                   CUEN.COD_FECHA_SALDO_HISTORICO = 'GESTION'
        INNER JOIN SAPR_TDOCUMENTO DOCU ON TREF.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
      WHERE
        TO_TIMESTAMP_TZ(TO_CHAR(DOCU.FYH_GESTION, 'DD/MM/YYYY HH24:MI:SS') || ' 00:00', 'DD/MM/YYYY HH24:MI:SS TZH:TZM') >= TO_TIMESTAMP_TZ(TO_CHAR(SYSDATE, 'DD/MM/YYYY') || ' 00:00:00 ' || CUEN.GMT, 'DD/MM/YYYY HH24:MI:SS TZH:TZM') AND
        TO_TIMESTAMP_TZ(TO_CHAR(DOCU.FYH_GESTION, 'DD/MM/YYYY HH24:MI:SS') || ' 00:00', 'DD/MM/YYYY HH24:MI:SS TZH:TZM') <= TO_TIMESTAMP_TZ(TO_CHAR(SYSDATE, 'DD/MM/YYYY') || ' 23:59:59 ' || CUEN.GMT, 'DD/MM/YYYY HH24:MI:SS TZH:TZM')
    UNION ALL
    SELECT
        CUEN.OID_CUENTA,
        CUEN.OID_MAQUINA,
        CUEN.OID_PAIS,
        TRUNC(SYSDATE) FEC_SALDO,
        TO_TIMESTAMP_TZ(TO_CHAR(SYSDATE, 'DD/MM/YYYY') || ' 23:59:59 ' || CUEN.GMT, 'DD/MM/YYYY HH24:MI:SS TZH:TZM') HOR_SALDO,
        SAEF.OID_DENOMINACION,
        SAEF.OID_DIVISA,
        SAEF.COD_NIVEL_DETALLE,
        SAEF.BOL_DISPONIBLE,
        NVL(SAEF.NUM_IMPORTE, 0) NUM_IMPORTE,
        NVL(SAEF.NEL_CANTIDAD, 0) NEL_CANTIDAD,
        CUEN.COD_FECHA_SALDO_HISTORICO,
        'T' TIPO_SALDO
    FROM
        SAPR_TSALDO_EFECTIVO SAEF
        INNER JOIN CUENTAS CUEN ON SAEF.OID_CUENTA_SALDO = CUEN.OID_CUENTA
  )
  SELECT
    SALD.OID_CUENTA,
    SALD.OID_MAQUINA,
    SALD.OID_PAIS,
    SALD.FEC_SALDO,
    SALD.HOR_SALDO,
    SALD.OID_DENOMINACION,
    SALD.OID_DIVISA,
    SALD.COD_NIVEL_DETALLE,
    SALD.BOL_DISPONIBLE,
    SUM(CASE WHEN SALD.TIPO_SALDO = 'D' THEN NVL(SALD.NUM_IMPORTE, 0) ELSE 0 END) NUM_IMPORTE,
    SUM(CASE WHEN SALD.TIPO_SALDO = 'D' THEN NVL(SALD.NUM_IMPORTE, 0) * -1 ELSE NVL(SALD.NUM_IMPORTE, 0) END) NUM_IMPORTE_ANTERIOR,
    SUM(CASE WHEN SALD.TIPO_SALDO = 'D' THEN NVL(SALD.NEL_CANTIDAD, 0) ELSE 0 END) NEL_CANTIDAD,
    SUM(CASE WHEN SALD.TIPO_SALDO = 'D' THEN NVL(SALD.NEL_CANTIDAD, 0) * -1 ELSE NVL(SALD.NEL_CANTIDAD, 0) END) NEL_CANTIDAD_ANTERIOR,
    SALD.COD_FECHA_SALDO_HISTORICO
  FROM
    SALDOS SALD
  GROUP BY
    SALD.OID_CUENTA,
    SALD.OID_MAQUINA,
    SALD.OID_PAIS,
    SALD.FEC_SALDO,
    SALD.HOR_SALDO,
    SALD.OID_DENOMINACION,
    SALD.OID_DIVISA,
    SALD.COD_NIVEL_DETALLE,
    SALD.BOL_DISPONIBLE,
    SALD.COD_FECHA_SALDO_HISTORICO;

  BEGIN
    BEGIN
      -- ACTUALIZACIÓN DE LA TABLA SAPR_TSALDO_EFECTIVO_HISTORICO
      FOR REC$SALDOS IN C_SALDOS LOOP
        MERGE INTO SAPR_TSALDO_EFECTIVO_HISTORICO Q1
        USING (
          SELECT REC$SALDOS.OID_CUENTA OID_CUENTA,
            REC$SALDOS.OID_MAQUINA OID_MAQUINA,
            REC$SALDOS.OID_PAIS OID_PAIS,
            REC$SALDOS.FEC_SALDO FEC_SALDO,
            REC$SALDOS.HOR_SALDO HOR_SALDO,
            REC$SALDOS.OID_DENOMINACION OID_DENOMINACION,
            REC$SALDOS.OID_DIVISA OID_DIVISA,
            REC$SALDOS.COD_NIVEL_DETALLE COD_NIVEL_DETALLE,
            REC$SALDOS.BOL_DISPONIBLE BOL_DISPONIBLE,
            REC$SALDOS.NUM_IMPORTE NUM_IMPORTE,
            REC$SALDOS.NUM_IMPORTE_ANTERIOR NUM_IMPORTE_ANTERIOR,
            REC$SALDOS.NEL_CANTIDAD NEL_CANTIDAD,
            REC$SALDOS.NEL_CANTIDAD_ANTERIOR NEL_CANTIDAD_ANTERIOR,
            REC$SALDOS.COD_FECHA_SALDO_HISTORICO COD_FECHA_SALDO_HISTORICO
          FROM DUAL
          ) Q2
          ON (Q1.OID_CUENTA = Q2.OID_CUENTA AND Q1.OID_MAQUINA = Q2.OID_MAQUINA AND Q1.OID_PAIS = Q2.OID_PAIS AND Q1.FEC_SALDO = Q2.FEC_SALDO AND Q1.OID_DIVISA = Q2.OID_DIVISA AND NVL(Q1.OID_DENOMINACION, '-') = NVL(Q2.OID_DENOMINACION, '-') AND NVL(Q1.COD_NIVEL_DETALLE, '-') = NVL(Q2.COD_NIVEL_DETALLE, '-') AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE AND ROWNUM <= 1)
          WHEN MATCHED THEN
            UPDATE
            SET
            Q1.NUM_IMPORTE = CASE WHEN Q1.BOL_SALDO_ANTERIOR_ACTUALIZADO = '0' THEN NVL(Q2.NUM_IMPORTE, 0) ELSE NVL(Q1.NUM_IMPORTE, 0) END,
            Q1.NEL_CANTIDAD = CASE WHEN Q1.BOL_SALDO_ANTERIOR_ACTUALIZADO = '0' THEN NVL(Q2.NEL_CANTIDAD, 0) ELSE NVL(Q1.NEL_CANTIDAD, 0) END,
            Q1.NUM_IMPORTE_ANTERIOR = CASE WHEN Q1.BOL_SALDO_ANTERIOR_ACTUALIZADO = '0' THEN NVL(Q2.NUM_IMPORTE_ANTERIOR, 0) ELSE NVL(Q1.NUM_IMPORTE_ANTERIOR, 0) END,
            Q1.NEL_CANTIDAD_ANTERIOR = CASE WHEN Q1.BOL_SALDO_ANTERIOR_ACTUALIZADO = '0' THEN NVL(Q2.NEL_CANTIDAD_ANTERIOR, 0) ELSE NVL(Q1.NEL_CANTIDAD_ANTERIOR, 0) END,
            Q1.GMT_MODIFICACION = gepr_putilidades_###VERSION###.fgmt_zero(),
            Q1.DES_USUARIO_MODIFICACION = par$cod_usuario,
            Q1.BOL_SALDO_ANTERIOR_ACTUALIZADO = '1'
          WHEN NOT MATCHED THEN
            INSERT
            (Q1.OID_SALDO_EFECTIVO_HISTORICO,
            Q1.OID_CUENTA,
            Q1.OID_MAQUINA,
            Q1.OID_PAIS,
            Q1.FEC_SALDO,
            Q1.HOR_SALDO,
            Q1.OID_DENOMINACION,
            Q1.OID_DIVISA,
            Q1.COD_NIVEL_DETALLE,
            Q1.BOL_DISPONIBLE,
            Q1.NUM_IMPORTE,
            Q1.NUM_IMPORTE_ANTERIOR,
            Q1.NEL_CANTIDAD,
            Q1.NEL_CANTIDAD_ANTERIOR,
            Q1.GMT_CREACION,
            Q1.DES_USUARIO_CREACION,
            Q1.GMT_MODIFICACION,
            Q1.DES_USUARIO_MODIFICACION,
            Q1.BOL_SALDO_ANTERIOR_ACTUALIZADO)
            VALUES
            (SYS_GUID(),
            Q2.OID_CUENTA,
            Q2.OID_MAQUINA,
            Q2.OID_PAIS,
            Q2.FEC_SALDO,
            Q2.HOR_SALDO,
            Q2.OID_DENOMINACION,
            Q2.OID_DIVISA,
            Q2.COD_NIVEL_DETALLE,
            Q2.BOL_DISPONIBLE,
            Q2.NUM_IMPORTE,
            Q2.NUM_IMPORTE_ANTERIOR,
            Q2.NEL_CANTIDAD,
            Q2.NEL_CANTIDAD_ANTERIOR,
            gepr_putilidades_###VERSION###.fgmt_zero(),
            par$cod_usuario,
            gepr_putilidades_###VERSION###.fgmt_zero(),
            par$cod_usuario,
            '1');
      END LOOP;
      -- ACTUALIZACIÓN DE LA TABLA SAPR_TCONTROL_SALDO_HISTORICO
      MERGE INTO SAPR_TCONTROL_SALDO_HISTORICO Q1
      USING (SELECT
        DISTINCT
        CLIE.OID_CLIENTE,
        MAQU.OID_MAQUINA,
        DELE.OID_PAIS,
        TRUNC(SYSDATE) FEC_SALDO,
        TO_TIMESTAMP_TZ(TO_CHAR(SYSDATE, 'DD/MM/YYYY') || ' 23:59:59 ' || TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN SYSDATE >= DELE.FYH_VERANO_INICIO AND SYSDATE <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' || TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN SYSDATE >= DELE.FYH_VERANO_INICIO AND SYSDATE <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') HOR_SALDO,
        CLIE.COD_FECHA_SALDO_HISTORICO
        FROM
        GEPR_TCLIENTE CLIE
        INNER JOIN SAPR_TCUENTA CUEN ON CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
        INNER JOIN GEPR_TSECTOR SECT ON CUEN.OID_SECTOR = SECT.OID_SECTOR
        INNER JOIN SAPR_TMAQUINA MAQU ON SECT.OID_SECTOR = MAQU.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
        INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
        WHERE
        CLIE.OID_CLIENTE = par$oid_cliente AND
        MAQU.OID_MAQUINA = par$oid_maquina AND
        DELE.OID_PAIS = par$oid_pais
      ) Q2
      ON (Q1.OID_CLIENTE = Q2.OID_CLIENTE AND Q1.OID_MAQUINA = Q2.OID_MAQUINA AND Q1.OID_PAIS = Q2.OID_PAIS AND ROWNUM <= 1)
      WHEN MATCHED THEN
        UPDATE
        SET
        Q1.FEC_SALDO = Q2.FEC_SALDO,
        Q1.HOR_SALDO = Q2.HOR_SALDO,
        Q1.GMT_MODIFICACION = gepr_putilidades_###VERSION###.fgmt_zero(),
        Q1.DES_USUARIO_MODIFICACION = par$cod_usuario
      WHEN NOT MATCHED THEN
        INSERT
        (Q1.OID_CONTROL_SALDO_HISTORICO,
        Q1.OID_CLIENTE,
        Q1.OID_MAQUINA,
        Q1.OID_PAIS,
        Q1.FEC_SALDO,
        Q1.HOR_SALDO,
        Q1.GMT_CREACION,
        Q1.DES_USUARIO_CREACION,
        Q1.GMT_MODIFICACION,
        Q1.DES_USUARIO_MODIFICACION)
        VALUES
        (SYS_GUID(),
        Q2.OID_CLIENTE,
        Q2.OID_MAQUINA,
        Q2.OID_PAIS,
        Q2.FEC_SALDO,
        Q2.HOR_SALDO,
        gepr_putilidades_###VERSION###.fgmt_zero(),
        par$cod_usuario,
        gepr_putilidades_###VERSION###.fgmt_zero(),
        par$cod_usuario);
      COMMIT;
    EXCEPTION WHEN OTHERS THEN
        ROLLBACK;
        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'3060020003',
                'ACTUALIZARSALDOSHISTORICO',
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                '',
                0);
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CALIFICADOR)
        VALUES ('3060020003', var$mensaje, 'VALIDACIONES');
        COMMIT;
    END;
    OPEN par$rc_validaciones FOR
        SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CALIFICADOR AS CALIFICADOR
        FROM SAPR_GTT_TAUXILIAR AUX
        WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';

  END sactualizar_saldos_historico;

	PROCEDURE srecuperar_saldos_historico(
                                    par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$bol_valor_detallar        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_pais                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fecha                     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$acod_device_id            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$bol_device_id_completo    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_cliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_sub_cliente           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_punto_servicio        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$acod_canal                IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_subcanal             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_divisa               IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$info_ejecucion            IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$rc_cuentas                OUT sys_refcursor,
                                    par$rc_divisas                OUT sys_refcursor,
                                    par$rc_denominaciones         OUT sys_refcursor,
                                    par$rc_validaciones           OUT sys_refcursor
                                    ) is

      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_saldos_historico';
      /* Variables */
      var$existe             gepr_pcomon_###VERSION###.tipo$nel_;
      var$consulta           gepr_pcomon_###VERSION###.tipo$obs_;


   CURSOR cur$datos_aux (
      par$cod_calificador IN gepr_pcomon_###VERSION###.tipo$cod_)
   IS
      SELECT oid_campo1, cod_campo2, cod_campo3, cod_calificador
        FROM SAPR_GTT_TAUXILIAR aux
       WHERE aux.cod_calificador = par$cod_calificador;

    begin

        DELETE SAPR_GTT_TAUXILIAR;
        COMMIT;
        dbms_output.put_line('teste 1 ');
        OPEN par$rc_cuentas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
        OPEN par$rc_divisas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
        OPEN par$rc_denominaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
        OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
        dbms_output.put_line('teste 2 ');

        /* #### Grabar llamadas #### */
        IF par$oid_llamada IS NOT NULL THEN
          /* OID_LLAMADA */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$oid_llamada: ' || par$oid_llamada,
            par$cod_identificador   => '');

          /* COD_PAIS */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_pais: ' || par$cod_pais,
            par$cod_identificador   => '');

          /* COD_CULTURA */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
            par$cod_identificador   => '');

          /* COD_USUARIO */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
            par$cod_identificador   => '');

          /* COD_IDENTIFICADOR_AJENO */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
            par$cod_identificador   => '');

          /*bol_valor_detallar*/
          IF par$bol_valor_detallar = 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$bol_valor_detallar: 0',
              par$cod_identificador   => '');
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$bol_valor_detallar: 1',
              par$cod_identificador   => '');
          END IF;

          /* FECHA  */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$fecha: ' || par$fecha,
            par$cod_identificador   => '');

          /*par$acod_device_id*/
          IF par$acod_device_id IS NOT NULL AND par$acod_device_id.count > 0 THEN
            FOR idx in par$acod_device_id.first .. par$acod_device_id.last LOOP
              IF par$acod_device_id(idx) IS NOT NULL THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'par$acod_device_id (' || idx || '): ' || par$acod_device_id(idx),
                  par$cod_identificador   => '');  
              END IF;
            END LOOP;
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$acod_device_id: NULL',
              par$cod_identificador   => '');
          END IF;

          /*bol_valor_detallar*/
          IF par$bol_valor_detallar = 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$bol_valor_detallar: 0',
              par$cod_identificador   => '');
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$bol_valor_detallar: 1',
              par$cod_identificador   => '');
          END IF;

          /* COD_CLIENTE  */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_cliente: ' || par$cod_cliente,
            par$cod_identificador   => '');

          /* cod_sub_cliente  */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_sub_cliente: ' || par$cod_sub_cliente,
            par$cod_identificador   => '');

          /* cod_punto_servicio  */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_punto_servicio: ' || par$cod_punto_servicio,
            par$cod_identificador   => '');

          /*par$acod_canal*/
          IF par$acod_canal IS NOT NULL AND par$acod_canal.count > 0 THEN
            FOR idx in par$acod_canal.first .. par$acod_canal.last LOOP
              IF par$acod_canal(idx) IS NOT NULL THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'par$acod_canal (' || idx || '): ' || par$acod_canal(idx),
                  par$cod_identificador   => '');  
              END IF;
            END LOOP;
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$acod_canal: NULL',
              par$cod_identificador   => '');
          END IF;

          /*par$acod_subcanal*/
          IF par$acod_subcanal IS NOT NULL AND par$acod_subcanal.count > 0 THEN
            FOR idx in par$acod_subcanal.first .. par$acod_subcanal.last LOOP
              IF par$acod_subcanal(idx) IS NOT NULL THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'par$acod_subcanal (' || idx || '): ' || par$acod_subcanal(idx),
                  par$cod_identificador   => '');  
              END IF;
            END LOOP;
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$acod_subcanal: NULL',
              par$cod_identificador   => '');
          END IF;

          /*par$acod_divisa*/
          IF par$acod_divisa IS NOT NULL AND par$acod_divisa.count > 0 THEN
            FOR idx in par$acod_divisa.first .. par$acod_divisa.last LOOP
              IF par$acod_divisa(idx) IS NOT NULL THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'par$acod_divisa (' || idx || '): ' || par$acod_divisa(idx),
                  par$cod_identificador   => '');  
              END IF;
            END LOOP;
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$acod_divisa: NULL',
              par$cod_identificador   => '');
          END IF;

          /* info_ejecucion  */
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$info_ejecucion: ' || par$info_ejecucion,
            par$cod_identificador   => '');

        END IF;

        svalidar_saldos_historico(/*Filtros entrada*/
                                        par$oid_llamada              => par$oid_llamada,
                                        par$cod_identificador_ajeno  => par$cod_identificador_ajeno,
                                        par$cod_pais                 => par$cod_pais,
                                        par$fecha                    => par$fecha,
                                        par$acod_device_id           => par$acod_device_id,
                                        par$bol_device_id_completo   => par$bol_device_id_completo,
                                        par$cod_cliente              => par$cod_cliente,
                                        par$cod_sub_cliente          => par$cod_sub_cliente,
                                        par$cod_punto_servicio       => par$cod_punto_servicio,
                                        par$acod_canal               => par$acod_canal,
                                        par$acod_subcanal            => par$acod_subcanal,
                                        par$acod_divisa              => par$acod_divisa,
                                        /*Patron*/
                                        par$cod_cultura              => par$cod_cultura
                                        );
        dbms_output.put_line('teste 3 ');
        SELECT COUNT(1)
            INTO var$existe
            FROM SAPR_GTT_TAUXILIAR
        WHERE COD_CALIFICADOR = 'VALIDACIONES';

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'var$existe VALIDACIONES: ' || var$existe,
          par$cod_identificador   => '');

        IF var$existe = 0 THEN
            var$consulta := q'[
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                            select
                            SAHI.OID_SALDO_EFECTIVO_HISTORICO, 'OID_SALDO_EFECTIVO_HISTORICO'
                            from
                            SAPR_TSALDO_EFECTIVO_HISTORICO SAHI
                            INNER JOIN SAPR_VCUENTA CUEN ON CUEN.OID_CUENTA = SAHI.OID_CUENTA
                            INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.OID_CAMPO1 = SAHI.OID_MAQUINA AND AUXM.COD_CALIFICADOR = 'OID_MAQUINA'
                            INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.OID_CAMPO1 = SAHI.OID_PAIS AND AUXP.COD_CALIFICADOR = 'OID_PAIS'

            ]';

            SELECT COUNT(1)
              INTO var$existe
            FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'OID_CLIENTE';
            
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'var$existe OID_CLIENTE: ' || var$existe,
              par$cod_identificador   => '');

            IF var$existe > 0 THEN
                var$consulta := var$consulta || q'[
                            INNER JOIN SAPR_GTT_TAUXILIAR AUXC ON AUXC.OID_CAMPO1 = CUEN.OID_CLIENTE AND AUXC.COD_CALIFICADOR = 'OID_CLIENTE' ]';
            END IF;

            SELECT COUNT(1)
              INTO var$existe
            FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE';

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'var$existe OID_SUBCLIENTE: ' || var$existe,
              par$cod_identificador   => '');

            IF var$existe > 0 THEN
                var$consulta := var$consulta || q'[
                            INNER JOIN SAPR_GTT_TAUXILIAR AUXSC ON AUXSC.OID_CAMPO1 = CUEN.OID_SUBCLIENTE AND AUXSC.COD_CALIFICADOR = 'OID_SUBCLIENTE' ]';
            END IF;


            SELECT COUNT(1)
                INTO var$existe
            FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO';

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'var$existe OID_PTO_SERVICIO: ' || var$existe,
              par$cod_identificador   => '');

            IF var$existe > 0 THEN
                var$consulta := var$consulta || q'[
                            INNER JOIN SAPR_GTT_TAUXILIAR AUXPS ON AUXPS.OID_CAMPO1 = CUEN.OID_PTO_SERVICIO AND AUXPS.COD_CALIFICADOR = 'OID_PTO_SERVICIO' ]';
            END IF;

            SELECT COUNT(1)
                INTO var$existe
            FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'OID_CANAL';

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'var$existe OID_CANAL: ' || var$existe,
              par$cod_identificador   => '');

            IF var$existe > 0 THEN
                var$consulta := var$consulta || q'[
                            INNER JOIN SAPR_GTT_TAUXILIAR AUXCA ON AUXCA.OID_CAMPO1 = CUEN.OID_CANAL AND AUXCA.COD_CALIFICADOR = 'OID_CANAL' ]';
            END IF;

            SELECT COUNT(1)
                INTO var$existe
            FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'OID_SUBCANAL';

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'var$existe OID_SUBCANAL: ' || var$existe,
              par$cod_identificador   => '');

            IF var$existe > 0 THEN
                var$consulta := var$consulta || q'[
                            INNER JOIN SAPR_GTT_TAUXILIAR AUXSCA ON AUXSCA.OID_CAMPO1 = CUEN.OID_SUBCANAL AND AUXSCA.COD_CALIFICADOR = 'OID_SUBCANAL' ]';
            END IF;

            SELECT COUNT(1)
                INTO var$existe
            FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'OID_DIVISA';

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'var$existe OID_DIVISA: ' || var$existe,
              par$cod_identificador   => '');

            IF var$existe > 0 THEN
                var$consulta := var$consulta || q'[
                            INNER JOIN SAPR_GTT_TAUXILIAR AUXDIV ON AUXDIV.OID_CAMPO1 = SAHI.OID_DIVISA AND AUXDIV.COD_CALIFICADOR = 'OID_DIVISA' ]';
            END IF;

            var$consulta := var$consulta || q'[ WHERE SAHI.FEC_SALDO =  TRUNC(TO_TIMESTAMP_TZ(']' || par$fecha || q'[', 'DD/MM/YYYY HH24:MI:SS TZH:TZM'))]';

            IF var$consulta IS NOT NULL THEN
                DBMS_OUTPUT.PUT_LINE('var$consulta: ' || var$consulta);
                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'var$consulta: ' ||  const$new_line ||var$consulta,
                  par$cod_identificador   => '');
                EXECUTE IMMEDIATE var$consulta;
            END IF;

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Imprime cursores',
              par$cod_identificador   => '');
            
            IF  par$cod_identificador_ajeno IS NULL THEN
                OPEN par$rc_cuentas FOR
                select
                    CUEN.OID_MAQUINA,
                    CUEN.COD_IDENTIFICACION_MAQUINA AS COD_MAQUINA,
                    CUEN.DES_SECTOR AS DES_MAQUINA,
                    CUEN.OID_CUENTA,
                    CUEN.OID_CLIENTE,
                    CUEN.COD_CLIENTE,
                    CUEN.DES_CLIENTE,
                    CUEN.OID_SUBCLIENTE,
                    CUEN.COD_SUBCLIENTE,
                    CUEN.DES_SUBCLIENTE,
                    CUEN.OID_PTO_SERVICIO,
                    CUEN.COD_PTO_SERVICIO,
                    CUEN.DES_PTO_SERVICIO,
                    CUEN.OID_CANAL,
                    CUEN.COD_CANAL,
                    CUEN.DES_CANAL,
                    CUEN.OID_SUBCANAL,
                    CUEN.COD_SUBCANAL,
                    CUEN.DES_SUBCANAL
                FROM SAPR_TSALDO_EFECTIVO_HISTORICO SAHI
                    INNER JOIN SAPR_GTT_TAUXILIAR AUX ON SAHI.OID_SALDO_EFECTIVO_HISTORICO = AUX.OID_CAMPO1 AND AUX.COD_CALIFICADOR = 'OID_SALDO_EFECTIVO_HISTORICO'
                    INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.OID_CAMPO1 = SAHI.OID_MAQUINA AND AUXM.COD_CALIFICADOR = 'OID_MAQUINA'
                    INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.OID_CAMPO1 = SAHI.OID_PAIS AND AUXP.COD_CALIFICADOR = 'OID_PAIS'
                    INNER JOIN SAPR_VCUENTA CUEN ON CUEN.OID_CUENTA = SAHI.OID_CUENTA
                    WHERE SAHI.FEC_SALDO = TRUNC(TO_TIMESTAMP_TZ(par$fecha, 'DD/MM/YYYY HH24:MI:SS TZH:TZM'))
                GROUP BY
                    CUEN.OID_MAQUINA,
                    CUEN.COD_IDENTIFICACION_MAQUINA,
                    CUEN.DES_SECTOR,
                    CUEN.OID_CUENTA,
                    CUEN.OID_CLIENTE,
                    CUEN.COD_CLIENTE,
                    CUEN.DES_CLIENTE,
                    CUEN.OID_SUBCLIENTE,
                    CUEN.COD_SUBCLIENTE,
                    CUEN.DES_SUBCLIENTE,
                    CUEN.OID_PTO_SERVICIO,
                    CUEN.COD_PTO_SERVICIO,
                    CUEN.DES_PTO_SERVICIO,
                    CUEN.OID_CANAL,
                    CUEN.COD_CANAL,
                    CUEN.DES_CANAL,
                    CUEN.OID_SUBCANAL,
                    CUEN.COD_SUBCANAL,
                    CUEN.DES_SUBCANAL;

                OPEN par$rc_divisas FOR
                    SELECT
                        SAHI.OID_CUENTA,
                        DIVI.OID_DIVISA,
                        DIVI.COD_ISO_DIVISA,
                        DIVI.DES_DIVISA,
                        SUM(SAHI.NUM_IMPORTE + SAHI.NUM_IMPORTE_ANTERIOR) AS NUM_IMPORTE,
                        SUM(SAHI.NUM_IMPORTE ) AS NUM_IMPORTE_DIA,
                        SUM(SAHI.NUM_IMPORTE_ANTERIOR) AS NUM_IMPORTE_ANTERIOR,
                        SAHI.BOL_DISPONIBLE
                    FROM
                        SAPR_TSALDO_EFECTIVO_HISTORICO SAHI
                        INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = SAHI.OID_DIVISA
                        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON SAHI.OID_SALDO_EFECTIVO_HISTORICO = AUX.OID_CAMPO1 AND AUX.COD_CALIFICADOR = 'OID_SALDO_EFECTIVO_HISTORICO'
                        INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.OID_CAMPO1 = SAHI.OID_MAQUINA AND AUXM.COD_CALIFICADOR = 'OID_MAQUINA'
                        INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.OID_CAMPO1 = SAHI.OID_PAIS AND AUXP.COD_CALIFICADOR = 'OID_PAIS'
                     WHERE SAHI.FEC_SALDO = TRUNC(TO_TIMESTAMP_TZ(par$fecha, 'DD/MM/YYYY HH24:MI:SS TZH:TZM'))
                    GROUP BY
                    SAHI.OID_CUENTA,
                        DIVI.OID_DIVISA,
                        DIVI.COD_ISO_DIVISA,
                        DIVI.DES_DIVISA,
                        SAHI.BOL_DISPONIBLE;

                IF par$bol_valor_detallar > 0 THEN

                    OPEN par$rc_denominaciones FOR
                        SELECT
                            SAHI.OID_CUENTA,
                            DENO.OID_DIVISA,
                            DENO.OID_DENOMINACION,
                            DENO.COD_DENOMINACION,
                            DENO.DES_DENOMINACION,
                            SUM(SAHI.NEL_CANTIDAD + SAHI.NEL_CANTIDAD_ANTERIOR) AS NEL_CANTIDAD,
                            SUM(SAHI.NUM_IMPORTE + SAHI.NUM_IMPORTE_ANTERIOR) AS NUM_IMPORTE,
                            SUM(SAHI.NUM_IMPORTE ) AS NUM_IMPORTE_DIA,
                            SUM(SAHI.NUM_IMPORTE_ANTERIOR) AS NUM_IMPORTE_ANTERIOR
                        FROM
                            SAPR_TSALDO_EFECTIVO_HISTORICO SAHI
                            INNER JOIN GEPR_TDENOMINACION DENO ON DENO.OID_DENOMINACION = SAHI.OID_DENOMINACION
                        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON SAHI.OID_SALDO_EFECTIVO_HISTORICO = AUX.OID_CAMPO1 AND AUX.COD_CALIFICADOR = 'OID_SALDO_EFECTIVO_HISTORICO'
                        INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.OID_CAMPO1 = SAHI.OID_MAQUINA AND AUXM.COD_CALIFICADOR = 'OID_MAQUINA'
                        INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.OID_CAMPO1 = SAHI.OID_PAIS AND AUXP.COD_CALIFICADOR = 'OID_PAIS'
                         WHERE SAHI.FEC_SALDO = TRUNC(TO_TIMESTAMP_TZ(par$fecha, 'DD/MM/YYYY HH24:MI:SS TZH:TZM'))
                        GROUP BY
                            SAHI.OID_CUENTA,
                            DENO.OID_DIVISA,
                            DENO.OID_DENOMINACION,
                            DENO.COD_DENOMINACION,
                            DENO.DES_DENOMINACION;
                END IF;

            ELSE
                OPEN par$rc_cuentas FOR
                select
                    CUEN.OID_MAQUINA,
                    NVL(CAS.COD_AJENO,CUEN.COD_IDENTIFICACION_MAQUINA) COD_MAQUINA,
                    NVL(CAS.DES_AJENO, CUEN.DES_SECTOR) DES_MAQUINA,
                    CUEN.OID_CUENTA,
                    CUEN.OID_CLIENTE,
                    NVL(CAC.COD_AJENO,CUEN.COD_CLIENTE) COD_CLIENTE,
                    NVL(CAC.DES_AJENO,CUEN.DES_CLIENTE) DES_CLIENTE,
                    CUEN.OID_SUBCLIENTE,
                    NVL(CASC.COD_AJENO,CUEN.COD_SUBCLIENTE) COD_SUBCLIENTE,
                    NVL(CASC.DES_AJENO,CUEN.DES_SUBCLIENTE) DES_SUBCLIENTE,
                    CUEN.OID_PTO_SERVICIO,
                    NVL(CAPS.COD_AJENO,CUEN.COD_PTO_SERVICIO) COD_PTO_SERVICIO,
                    NVL(CAPS.DES_AJENO,CUEN.DES_PTO_SERVICIO) DES_PTO_SERVICIO,
                    CUEN.OID_CANAL,
                    NVL(CACAN.COD_AJENO, CUEN.COD_CANAL) COD_CANAL,
                    NVL(CACAN.DES_AJENO, CUEN.DES_CANAL) DES_CANAL,
                    CUEN.OID_SUBCANAL,
                    NVL(CASCA.COD_AJENO, CUEN.COD_SUBCANAL) COD_SUBCANAL,
                    NVL(CASCA.DES_AJENO, CUEN.DES_SUBCANAL) DES_SUBCANAL
                FROM SAPR_TSALDO_EFECTIVO_HISTORICO SAHI
                        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON SAHI.OID_SALDO_EFECTIVO_HISTORICO = AUX.OID_CAMPO1 AND AUX.COD_CALIFICADOR = 'OID_SALDO_EFECTIVO_HISTORICO'
                                                INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.OID_CAMPO1 = SAHI.OID_MAQUINA AND AUXM.COD_CALIFICADOR = 'OID_MAQUINA'
                        INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.OID_CAMPO1 = SAHI.OID_PAIS AND AUXP.COD_CALIFICADOR = 'OID_PAIS'
                    INNER JOIN SAPR_VCUENTA CUEN ON CUEN.OID_CUENTA = SAHI.OID_CUENTA
                    LEFT JOIN GEPR_TCODIGO_AJENO CAS
                        ON CAS.OID_TABLA_GENESIS = CUEN.OID_SECTOR AND CAS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAS.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'

                    LEFT JOIN GEPR_TCODIGO_AJENO CAC
                        ON CAC.OID_TABLA_GENESIS = CUEN.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                    LEFT JOIN GEPR_TCODIGO_AJENO CASC
                    ON CASC.OID_TABLA_GENESIS = CUEN.OID_SUBCLIENTE AND CASC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                    LEFT JOIN GEPR_TCODIGO_AJENO CAPS
                    ON CAPS.OID_TABLA_GENESIS = CUEN.OID_PTO_SERVICIO AND CAPS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAPS.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                    LEFT JOIN GEPR_TCODIGO_AJENO CACAN
                    ON CACAN.OID_TABLA_GENESIS = CUEN.OID_CANAL AND CACAN.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CACAN.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
                    LEFT JOIN GEPR_TCODIGO_AJENO CASCA
                    ON CASCA.OID_TABLA_GENESIS = CUEN.OID_SUBCANAL AND CASCA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASCA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
                  WHERE SAHI.FEC_SALDO = TRUNC(TO_TIMESTAMP_TZ(par$fecha, 'DD/MM/YYYY HH24:MI:SS TZH:TZM'))
                GROUP BY
                    CUEN.OID_MAQUINA,
                    CUEN.COD_IDENTIFICACION_MAQUINA,
                    CUEN.DES_SECTOR,
                    CUEN.OID_CUENTA,
                    CUEN.OID_CLIENTE,
                    CUEN.COD_CLIENTE,
                    CUEN.DES_CLIENTE,
                    CUEN.OID_SUBCLIENTE,
                    CUEN.COD_SUBCLIENTE,
                    CUEN.DES_SUBCLIENTE,
                    CUEN.OID_PTO_SERVICIO,
                    CUEN.COD_PTO_SERVICIO,
                    CUEN.DES_PTO_SERVICIO,
                    CUEN.OID_CANAL,
                    CUEN.COD_CANAL,
                    CUEN.DES_CANAL,
                    CUEN.OID_SUBCANAL,
                    CUEN.COD_SUBCANAL,
                    CUEN.DES_SUBCANAL,
                    CAS.COD_AJENO,
                    CAS.DES_AJENO,
                    CAC.COD_AJENO,
                    CAC.DES_AJENO,
                    CASC.COD_AJENO,
                    CASC.DES_AJENO,
                    CAPS.COD_AJENO,
                    CAPS.DES_AJENO,
                    CACAN.COD_AJENO,
                    CACAN.DES_AJENO,
                    CASCA.COD_AJENO,
                    CASCA.DES_AJENO;






                OPEN par$rc_divisas FOR
                    SELECT
                        SAHI.OID_CUENTA,
                        DIVI.OID_DIVISA,
                        NVL(CADI.COD_AJENO, DIVI.COD_ISO_DIVISA) COD_ISO_DIVISA,
                        NVL(CADI.DES_AJENO, DIVI.DES_DIVISA) DES_DIVISA,
                        SUM(SAHI.NUM_IMPORTE + SAHI.NUM_IMPORTE_ANTERIOR) AS NUM_IMPORTE,
                        SUM(SAHI.NUM_IMPORTE ) AS NUM_IMPORTE_DIA,
                        SUM(SAHI.NUM_IMPORTE_ANTERIOR) AS NUM_IMPORTE_ANTERIOR,
                        SAHI.BOL_DISPONIBLE
                    FROM
                        SAPR_TSALDO_EFECTIVO_HISTORICO SAHI
                        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON SAHI.OID_SALDO_EFECTIVO_HISTORICO = AUX.OID_CAMPO1 AND AUX.COD_CALIFICADOR = 'OID_SALDO_EFECTIVO_HISTORICO'
                                                INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.OID_CAMPO1 = SAHI.OID_MAQUINA AND AUXM.COD_CALIFICADOR = 'OID_MAQUINA'
                        INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.OID_CAMPO1 = SAHI.OID_PAIS AND AUXP.COD_CALIFICADOR = 'OID_PAIS'
                        INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = SAHI.OID_DIVISA
                        LEFT JOIN GEPR_TCODIGO_AJENO CADI ON CADI.OID_TABLA_GENESIS = SAHI.OID_DIVISA
                        AND CADI.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                        AND CADI.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'
                        WHERE SAHI.FEC_SALDO = TRUNC(TO_TIMESTAMP_TZ(par$fecha, 'DD/MM/YYYY HH24:MI:SS TZH:TZM'))
                    GROUP BY
                    SAHI.OID_CUENTA,
                        DIVI.OID_DIVISA,
                        DIVI.COD_ISO_DIVISA,
                        DIVI.DES_DIVISA,
                        SAHI.BOL_DISPONIBLE,
                        CADI.COD_AJENO,
                        CADI.DES_AJENO;


                IF par$bol_valor_detallar > 0 THEN

                    OPEN par$rc_denominaciones FOR
                        SELECT
                            SAHI.OID_CUENTA,
                            DENO.OID_DIVISA,
                            DENO.OID_DENOMINACION,
                            NVL(CADE.COD_AJENO, DENO.COD_DENOMINACION) COD_DENOMINACION,
                            NVL(CADE.DES_AJENO, DENO.DES_DENOMINACION) DES_DENOMINACION,
                            SUM(SAHI.NEL_CANTIDAD + SAHI.NEL_CANTIDAD_ANTERIOR) AS NEL_CANTIDAD,
                            SUM(SAHI.NUM_IMPORTE + SAHI.NUM_IMPORTE_ANTERIOR) AS NUM_IMPORTE    ,
                            SUM(SAHI.NUM_IMPORTE ) AS NUM_IMPORTE_DIA,
                            SUM(SAHI.NUM_IMPORTE_ANTERIOR) AS NUM_IMPORTE_ANTERIOR
                        FROM
                            SAPR_TSALDO_EFECTIVO_HISTORICO SAHI
                            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON SAHI.OID_SALDO_EFECTIVO_HISTORICO = AUX.OID_CAMPO1 AND AUX.COD_CALIFICADOR = 'OID_SALDO_EFECTIVO_HISTORICO'
                                                    INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.OID_CAMPO1 = SAHI.OID_MAQUINA AND AUXM.COD_CALIFICADOR = 'OID_MAQUINA'
                        INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.OID_CAMPO1 = SAHI.OID_PAIS AND AUXP.COD_CALIFICADOR = 'OID_PAIS'
                            INNER JOIN GEPR_TDENOMINACION DENO ON DENO.OID_DENOMINACION = SAHI.OID_DENOMINACION
                            LEFT JOIN GEPR_TCODIGO_AJENO CADE ON CADE.OID_TABLA_GENESIS = SAHI.OID_DENOMINACION
                                            AND CADE.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                                            AND CADE.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION'
                        WHERE SAHI.FEC_SALDO = TRUNC(TO_TIMESTAMP_TZ(par$fecha, 'DD/MM/YYYY HH24:MI:SS TZH:TZM'))
                        GROUP BY
                            SAHI.OID_CUENTA,
                            DENO.OID_DIVISA,
                            DENO.OID_DENOMINACION,
                            DENO.COD_DENOMINACION,
                            DENO.DES_DENOMINACION,
                            CADE.COD_AJENO,
                            CADE.DES_AJENO;
                END IF;

            END IF;

        END IF;

        OPEN par$rc_validaciones FOR
            SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION
            FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'VALIDACIONES';


        COMMIT;
    end srecuperar_saldos_historico;

  /*Valida los IN al procedure de recuperar saldos historico*/
  PROCEDURE svalidar_saldos_historico(/*Filtros entrada*/
                                    par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_pais                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fecha                     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$acod_device_id            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$bol_device_id_completo    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_cliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_sub_cliente           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_punto_servicio        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$acod_canal                IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_subcanal             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_divisa               IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    /*Patron*/
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_
                                    )
  IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_saldos_historico';
    var$oid_pais            gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_pais            gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_maquina         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cliente         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sub_cliente     gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_punto_servicio  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa          gepr_pcomon_###VERSION###.tipo$oid_;
    var$consulta            gepr_pcomon_###VERSION###.tipo$obs_;

    /* CANALES */
    CURSOR cur$canales IS
      SELECT T.OID_CAMPO1 COD_CANAL, C.OID_CANAL
      FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN GEPR_TCANAL C ON C.COD_CANAL = T.OID_CAMPO1
      WHERE T.COD_CALIFICADOR = 'COD_CANAL';

    CURSOR cur$canales_ajeno IS
      SELECT T.OID_CAMPO1 COD_CANAL, CA.OID_TABLA_GENESIS OID_CANAL
      FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_AJENO = T.OID_CAMPO1
                                    AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
                                    AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                                    AND CA.BOL_ACTIVO = 1
      WHERE T.COD_CALIFICADOR = 'COD_CANAL';

    /* SUBCANALES */
    CURSOR cur$subcanales IS
      SELECT T.OID_CAMPO1 COD_SUBCANAL, SC.OID_SUBCANAL
      FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN GEPR_TSUBCANAL SC ON SC.COD_SUBCANAL = T.OID_CAMPO1
      WHERE T.COD_CALIFICADOR = 'COD_SUBCANAL';

    CURSOR cur$subcanales_ajeno IS
      SELECT T.OID_CAMPO1 COD_SUBCANAL, CA.OID_TABLA_GENESIS OID_SUBCANAL
      FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_AJENO = T.OID_CAMPO1
                                    AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
                                    AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                                    AND CA.BOL_ACTIVO = 1
      WHERE T.COD_CALIFICADOR = 'COD_SUBCANAL';

  BEGIN
    /* Link de Confluence: https://procde.prosegur.com/confluence/pages/viewpage.action?pageId=211750960 */
    /*Obligatorio
      par$cod_pais
      par$acod_device_id hay que tener al menos uno
      par$fecha
    */

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Valida par$cod_pais',
      par$cod_identificador   => '');

    /* Validar código de país */
    /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
    var$cod_pais := par$cod_pais;
    GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                            par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                            par$cod_pais => var$cod_pais,
                            par$oid_pais => var$oid_pais,
                            par$cod_cultura => par$cod_cultura);

    IF var$oid_pais IS NOT NULL THEN
      INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
      VALUES (var$oid_pais, 'OID_PAIS');

      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'var$oid_pais: ' || var$oid_pais,
        par$cod_identificador   => '');
    END IF;

    /* Validación de códigos de maquinas */
    /* Se valida si se envia el codigo completo de la maquina par$bol_device_id_completo cambia el filtro de '=' a 'LIKE' */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Validación de máquinas',
      par$cod_identificador   => '');
    IF par$acod_device_id IS NOT NULL AND par$acod_device_id.count > 1 THEN
      FOR idx IN par$acod_device_id.first .. par$acod_device_id.last LOOP
        IF par$acod_device_id(idx) IS NOT NULL THEN
          BEGIN
            IF par$cod_identificador_ajeno IS NULL THEN
              var$consulta := q'[
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                SELECT OID_MAQUINA, ']'|| par$acod_device_id(idx) || q'[', 'OID_MAQUINA'
                FROM SAPR_TMAQUINA
              ]';
              IF par$bol_device_id_completo = 1 THEN
                 var$consulta := var$consulta || q'[
                  WHERE COD_IDENTIFICACION = ']'|| par$acod_device_id(idx) || q'[']';
              ELSE
                var$consulta := var$consulta || q'[
                  WHERE COD_IDENTIFICACION LIKE '%]' || par$acod_device_id(idx) || q'[%']';
              END IF;
            ELSE
              var$consulta := q'[
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                SELECT MAQ.OID_MAQUINA,']'|| par$acod_device_id(idx) || q'[', 'OID_MAQUINA'
                FROM SAPR_TMAQUINA MAQ
                LEFT JOIN GEPR_TCODIGO_AJENO AJENO ON MAQ.OID_SECTOR = AJENO.OID_TABLA_GENESIS
                                                    AND AJENO.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
                                                    AND AJENO.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[']';

              IF par$bol_device_id_completo = 1 THEN
                 var$consulta := var$consulta || q'[
                  WHERE AJENO.COD_AJENO = ']'|| par$acod_device_id(idx) || q'[']';
              ELSE
                var$consulta := var$consulta || q'[
                  WHERE AJENO.COD_AJENO LIKE '%]' || par$acod_device_id(idx) || q'[%']';
              END IF;
            END IF;

            IF var$consulta IS NOT NULL THEN
              DBMS_OUTPUT.PUT_LINE('var$consulta: ' || var$consulta);
              SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'var$consulta: ' || var$consulta,
                par$cod_identificador   => '');              
              EXECUTE IMMEDIATE var$consulta;
            END IF;

            SELECT OID_CAMPO1
              INTO var$oid_maquina
              FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'OID_MAQUINA'
            AND COD_CAMPO2 = par$acod_device_id(idx) AND ROWNUM <=1;

          EXCEPTION
            WHEN OTHERS THEN
              var$oid_maquina := NULL;
          END;

          IF var$oid_maquina IS NULL THEN
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES
            (
              '2040310007',
              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310007', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$acod_device_id(idx), 0),
              'VALIDACIONES'
            );

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => '2040310007 - par$acod_device_id(' || idx || '): ' || par$acod_device_id(idx) ||' - var$oid_maquina: ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310007', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$acod_device_id(idx), 0),
              par$cod_identificador   => '');
          END IF;
        END IF;
      END LOOP;
    ELSE
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
      VALUES
      (
        '2040310005',
        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310005', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
        'VALIDACIONES'
      );

      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310005', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
        par$cod_identificador   => '');
    END IF;

    /*Validación de fecha */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicio de validación de fecha',
      par$cod_identificador   => '');
    IF par$fecha IS NULL  THEN
      INSERT INTO SAPR_GTT_TAUXILIAR  (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
      VALUES
      (
        '2040310006',
        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310006', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
        'VALIDACIONES'
      );

      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => '2040310006 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310006', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
        par$cod_identificador   => '');
    END IF;

    /* Validación de cliente */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicio de validación de cliente',
      par$cod_identificador   => '');
    IF par$cod_cliente IS NOT NULL THEN
      BEGIN
        IF par$cod_identificador_ajeno IS NULL THEN
          SELECT OID_CLIENTE
          INTO var$oid_cliente
          FROM GEPR_TCLIENTE
          WHERE COD_CLIENTE = par$cod_cliente;
        ELSE
          SELECT CLI.OID_CLIENTE
          INTO var$oid_cliente
          FROM GEPR_TCLIENTE CLI
          INNER JOIN GEPR_TCODIGO_AJENO AJENO ON CLI.OID_CLIENTE = AJENO.OID_TABLA_GENESIS AND AJENO.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE' AND AJENO.COD_IDENTIFICADOR = par$cod_identificador_ajeno
          WHERE AJENO.COD_AJENO = par$cod_cliente;
        END IF;

        INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
        VALUES (var$oid_cliente, 'OID_CLIENTE');

      EXCEPTION
        WHEN no_data_found THEN
          var$oid_cliente := NULL;

          INSERT INTO SAPR_GTT_TAUXILIAR  (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          VALUES
          (
            '2040310008',
            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310008', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
            'VALIDACIONES'
          );

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => '2040310008 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310008', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
            par$cod_identificador   => '');
      END;


    end if;

    /* Validación de subcliente */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicio de validación de subcliente',
      par$cod_identificador   => '');
    IF par$cod_sub_cliente IS NOT NULL THEN
      BEGIN
        IF par$cod_identificador_ajeno IS NULL THEN
          SELECT OID_SUBCLIENTE
          INTO var$oid_sub_cliente
          FROM GEPR_TSUBCLIENTE
          WHERE COD_SUBCLIENTE = par$cod_sub_cliente AND OID_CLIENTE = var$oid_cliente;
        ELSE
          SELECT SUBCLI.OID_SUBCLIENTE
          INTO var$oid_sub_cliente
          FROM GEPR_TSUBCLIENTE SUBCLI
          INNER JOIN GEPR_TCODIGO_AJENO AJENO ON SUBCLI.OID_SUBCLIENTE = AJENO.OID_TABLA_GENESIS AND AJENO.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE' AND AJENO.COD_IDENTIFICADOR = par$cod_identificador_ajeno
          WHERE AJENO.COD_AJENO = par$cod_sub_cliente AND SUBCLI.OID_CLIENTE = var$oid_cliente;
        END IF;

        INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
        VALUES (var$oid_sub_cliente, 'OID_SUBCLIENTE');

      EXCEPTION
        WHEN no_data_found THEN
          var$oid_sub_cliente := NULL;

          INSERT INTO SAPR_GTT_TAUXILIAR  (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          VALUES
          (
            '2040310008',
            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310008', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
            'VALIDACIONES'
          );
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => '2040310008 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310008', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
            par$cod_identificador   => '');
      END;
    END IF;

    /* Validación de punto de servicio */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicio de validación de punto de servicio',
      par$cod_identificador   => '');
    IF par$cod_punto_servicio IS NOT NULL THEN
      BEGIN
        IF par$cod_identificador_ajeno IS NULL THEN
          SELECT OID_PTO_SERVICIO
          INTO var$oid_punto_servicio
          FROM GEPR_TPUNTO_SERVICIO
          WHERE COD_PTO_SERVICIO = par$cod_punto_servicio AND OID_SUBCLIENTE = var$oid_sub_cliente;
        ELSE
          SELECT PUNTO.OID_PTO_SERVICIO
          INTO var$oid_punto_servicio
          FROM GEPR_TPUNTO_SERVICIO PUNTO
          INNER JOIN GEPR_TCODIGO_AJENO AJENO ON PUNTO.OID_PTO_SERVICIO = AJENO.OID_TABLA_GENESIS AND AJENO.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' AND AJENO.COD_IDENTIFICADOR = par$cod_identificador_ajeno
          WHERE AJENO.COD_AJENO = par$cod_punto_servicio AND PUNTO.OID_SUBCLIENTE = var$oid_sub_cliente;
        END IF;

        INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
        VALUES (var$oid_punto_servicio, 'OID_PTO_SERVICIO');

      EXCEPTION
        WHEN no_data_found THEN
          var$oid_punto_servicio := NULL;

          INSERT INTO SAPR_GTT_TAUXILIAR  (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          VALUES
          (
            '2040310008',
            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310008', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
            'VALIDACIONES'
          );
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => '2040310008 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310008', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
            par$cod_identificador   => '');
      END;
    END IF;

    /*Validación de divisas */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicio de validación de divisas',
      par$cod_identificador   => '');
    IF par$acod_divisa IS NOT NULL AND par$acod_divisa.count > 0 THEN
      FOR idx IN par$acod_divisa.first .. par$acod_divisa.last LOOP
        IF par$acod_divisa(idx) IS NOT NULL THEN
          BEGIN
            IF par$cod_identificador_ajeno IS NULL THEN
                SELECT OID_DIVISA
                INTO var$oid_divisa
                FROM GEPR_TDIVISA
                WHERE COD_ISO_DIVISA = par$acod_divisa(idx);
            ELSE
                SELECT DIV.OID_DIVISA
                INTO var$oid_divisa
                FROM GEPR_TDIVISA DIV
                INNER JOIN GEPR_TCODIGO_AJENO AJENO ON DIV.OID_DIVISA = AJENO.OID_TABLA_GENESIS AND AJENO.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' AND AJENO.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                WHERE AJENO.COD_AJENO = par$acod_divisa(idx);
            END IF;
          EXCEPTION
            WHEN no_data_found THEN
              var$oid_divisa := NULL;
          END;

          IF var$oid_divisa IS NOT NULL THEN
            INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
            VALUES (var$oid_divisa, 'OID_DIVISA');
          ELSE
            INSERT INTO SAPR_GTT_TAUXILIAR  (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES
            (
              '2040310009',
              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310009', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$acod_divisa(idx), 0),
              'VALIDACIONES'
            );
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => '2040310009 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310009', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$acod_divisa(idx), 0),
              par$cod_identificador   => '');
          END IF;
        END IF;
      END LOOP;
    END IF;

    /* Validación de canales */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicio de validación de canales',
      par$cod_identificador   => '');
    IF par$acod_canal IS NOT NULL AND par$acod_canal.COUNT > 0 THEN
      /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS CANALES */
      FOR idx IN par$acod_canal.first .. par$acod_canal.last LOOP
        IF par$acod_canal(idx) IS NOT NULL THEN
          INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
          VALUES (par$acod_canal(idx), 'COD_CANAL');
        END IF;
      END LOOP;

      /* CONSULTA los CODIGOS Y HACE LAS VALIDACIONES NECESARIAS - POR CODIGO AJENO*/
      IF par$cod_identificador_ajeno IS NOT NULL THEN
        FOR rec$canal IN cur$canales_ajeno LOOP
          IF rec$canal.OID_CANAL IS NOT NULL THEN
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,  COD_CALIFICADOR)
            VALUES (rec$canal.OID_CANAL,'OID_CANAL' );
          ELSE
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES ('2040310010',
                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                    '2040310010',
                    'RECUPERARSALDOSHISTORICO',
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    rec$canal.COD_CANAL || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TCANAL',
                    0),
                    'VALIDACIONES');

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => '2040310010 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310010', 'RECUPERARSALDOSHISTORICO',gepr_pcomon_###VERSION###.const$codAplicacionGenesis, rec$canal.COD_CANAL || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TCANAL', 0),
              par$cod_identificador   => '');
          END IF;
        END LOOP rec$canal;
      ELSE
        FOR rec$canal IN cur$canales LOOP
          IF rec$canal.OID_CANAL IS NOT NULL THEN
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,  COD_CALIFICADOR)
            VALUES (rec$canal.OID_CANAL,'OID_CANAL' );
          ELSE
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES ('2040310011',
                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                    '2040310011',
                    'RECUPERARSALDOSHISTORICO',
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    rec$canal.COD_CANAL,
                    0),
                    'VALIDACIONES'
                    );
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => '2040310011 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310011', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, rec$canal.COD_CANAL, 0),
              par$cod_identificador   => '');
          END IF;
        END LOOP rec$canal;
      END IF;

    END IF;

    /* Validación de subcanales */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicio de validación de subcanales',
      par$cod_identificador   => '');
    IF par$acod_subcanal IS NOT NULL AND par$acod_subcanal.COUNT > 0 THEN
      /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS SUBCANALES */
      FOR idx IN par$acod_subcanal.first .. par$acod_subcanal.last LOOP
        IF par$acod_subcanal(idx) IS NOT NULL THEN
          INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
          VALUES (par$acod_subcanal(idx), 'COD_SUBCANAL');
        END IF;
      END LOOP;

      /* CONSULTA LOS CODIGOS Y HACE LAS VALIDACIONES NECESARIAS - POR CODIGO AJENO*/
      IF par$cod_identificador_ajeno IS NOT NULL THEN
        FOR rec$subcanal IN cur$subcanales_ajeno LOOP
          IF rec$subcanal.OID_SUBCANAL IS NOT NULL THEN
            INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
            VALUES (rec$subcanal.OID_SUBCANAL, 'OID_SUBCANAL');
          ELSE
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES ('2040310010', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                    '2040310010',
                    'RECUPERARSALDOSHISTORICO',
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    rec$subcanal.COD_SUBCANAL || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TSUBCANAL',
                    0),
                    'VALIDACIONES');

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => '2040310010 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310010', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, rec$subcanal.COD_SUBCANAL || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TSUBCANAL', 0),
              par$cod_identificador   => '');
          END IF;
        END LOOP rec$canal;
      ELSE
        FOR rec$subcanal IN cur$subcanales LOOP
          IF rec$subcanal.OID_SUBCANAL IS NOT NULL THEN
            INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
            VALUES (rec$subcanal.OID_SUBCANAL, 'OID_SUBCANAL');
          ELSE
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES ('2040310012',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                    '2040310012',
                    'RECUPERARSALDOSHISTORICO',
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    rec$subcanal.COD_SUBCANAL,
                    0),
                    'VALIDACIONES'
                    );
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => '2040310012 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040310012', 'RECUPERARSALDOSHISTORICO', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, rec$subcanal.COD_SUBCANAL, 0),
              par$cod_identificador   => '');
          END IF;
        END LOOP rec$subcanal;
      END IF;
    END IF;

  END svalidar_saldos_historico;

  PROCEDURE srecuperar_saldos(par$pag_indice                IN gepr_pcomon_###VERSION###.tipo$nel_,
                              par$pag_reg_pagina            IN gepr_pcomon_###VERSION###.tipo$nel_,
                              par$bol_detallar_trans        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_detallar_valores      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_detallar_bolsas       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_info_adicional        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_disponible            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_acreditado            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_notificado            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_maqu_vigente          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$fyh_gestion               IN gepr_pcomon_###VERSION###.tipo$fyh_,
                              par$fyh_creacion              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                              par$cod_cliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_sub_cliente           IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_punto_servicio        IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_delegacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_planta                IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_divisa                IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_maquinas              IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$cod_canales               IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$cod_subcanales            IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$info_ejecucion            IN gepr_pcomon_###VERSION###.tipo$desc_,
                              par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$rc_movimientos            OUT sys_refcursor,
                              par$rc_formularios            OUT sys_refcursor,
                              par$rc_cuentas                OUT sys_refcursor,
                              par$rc_terminos               OUT sys_refcursor,
                              par$rc_divisas                OUT sys_refcursor,
                              par$rc_denominaciones         OUT sys_refcursor,
                              par$rc_efectivos              OUT sys_refcursor,
                              par$rc_mediopagos             OUT sys_refcursor,
                              par$rc_saldos                 OUT sys_refcursor,
                              par$rc_bolsas                 OUT sys_refcursor,
                              par$validaciones              OUT sys_refcursor,
                              par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_saldos';

      /* LOG */
      var$inicio    TIMESTAMP := SYSTIMESTAMP;
      var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$updates   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$merges    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$err       varchar2(4000);

      /* Variables */
      var$existe             gepr_pcomon_###VERSION###.tipo$nel_;
      var$consulta           gepr_pcomon_###VERSION###.tipo$obs_;
      var$consultaShipOut    gepr_pcomon_###VERSION###.tipo$obs_;
      var$oid_delegacion     gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_planta         gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_cliente        gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_sub_cliente    gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_punto_servicio gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_divisa         gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_canales        gepr_pcomon_###VERSION###.tipo$oids_;
      var$oid_subcanales     gepr_pcomon_###VERSION###.tipo$oids_;
      var$fyh_gestion        gepr_pcomon_###VERSION###.tipo$fyh_;
      var$fyh_creacion       gepr_pcomon_###VERSION###.tipo$fyh_;
      var$nel_gmt_minuto     gepr_pcomon_###VERSION###.tipo$nel_;
       var$pag_indice         gepr_pcomon_###VERSION###.tipo$nel_;
      var$pag_reg_pagina     gepr_pcomon_###VERSION###.tipo$nel_;

  BEGIN

      /* ##### LIMPIAR TABLAS TEMPORARIAS ##### */
      DELETE SAPR_GTT_TAUXILIAR;
      DELETE SAPR_GTT_TRECUPERARSALDOS;

      /* #### Inicializar los cursores #### */
      OPEN par$rc_movimientos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_formularios FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_cuentas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_terminos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_divisas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_denominaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_efectivos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_mediopagos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_saldos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_bolsas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

      /* Validar codigo Ajeno e preencher tabela temporaria */
      svalidar_codigos(par$cod_cliente             => par$cod_cliente,
                       par$cod_sub_cliente         => par$cod_sub_cliente,
                       par$cod_punto_servicio      => par$cod_punto_servicio,
                       par$cod_delegacion          => par$cod_delegacion,
                       par$cod_planta              => par$cod_planta,
                       par$cod_divisa              => par$cod_divisa,
                       par$cod_maquinas            => par$cod_maquinas,
                       par$cod_canales             => par$cod_canales,
                       par$cod_subcanales          => par$cod_subcanales,
                       par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                       par$oid_cliente             => var$oid_cliente,
                       par$oid_sub_cliente         => var$oid_sub_cliente,
                       par$oid_punto_servicio      => var$oid_punto_servicio,
                       par$oid_delegacion          => var$oid_delegacion,
                       par$oid_planta              => var$oid_planta,
                       par$oid_divisa              => var$oid_divisa,
                       par$oid_canales             => var$oid_canales,
                       par$oid_subcanales          => var$oid_subcanales,
                       par$selects                 => var$selects,
                       par$cod_cultura             => par$cod_cultura);

      var$fyh_gestion  := par$fyh_gestion;
      var$fyh_creacion := par$fyh_creacion;

      /* SI NO HAY ERRORES, EJECUTA LAS CONSULTAS */
      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR
      WHERE COD_CALIFICADOR = 'VALIDACIONES';

      IF var$existe = 0 THEN

        /* RECUPERAR FECHA INICIO */
        var$consultaShipOut := q'[
              SELECT CUEN.OID_SECTOR
                  , CANA.OID_CANAL
                  , COAJ.COD_AJENO
                  , TO_CHAR(MAX(MOVI.FYH_GESTION), 'DD/MM/YYYY HH24:MI:SS') FYH_GESTION
                  , 'FYH_GESTION' COD_CALIFICADOR
                FROM SAPR_GTT_TAUXILIAR AUXM
          INNER JOIN SAPR_TCUENTA CUEN ON AUXM.COD_CALIFICADOR = 'OID_SECTOR' AND AUXM.OID_CAMPO1 = CUEN.OID_SECTOR
          INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = CUEN.OID_SECTOR
          INNER JOIN GEPR_TSUBCANAL SUBC ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL
          INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = SUBC.OID_CANAL
          INNER JOIN GEPR_TCODIGO_AJENO COAJ ON COAJ.OID_TABLA_GENESIS = SUBC.OID_CANAL AND COAJ.COD_IDENTIFICADOR = 'MAE' AND COAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
          LEFT JOIN SAPR_TMOVIMIENTO_SHIPOUT MOVI ON CUEN.OID_CUENTA = MOVI.OID_CUENTA_SALDO AND MOVI.FYH_GESTION < TO_DATE(']' || TO_CHAR(var$fyh_gestion, 'DD/MM/YYYY HH24:MI:SS') || q'[', 'DD/MM/YYYY HH24:MI:SS') ]';

        /* MONTAR FILTRO PRINCIPAL */
        var$consulta := q'[
              SELECT DOCU.OID_DOCUMENTO
                    , DOCU.COD_EXTERNO
                    , DOCU.FYH_GESTION
                    , DOCU.GMT_CREACION
                    , DOCU.OID_CUENTA_SALDO_ORIGEN AS OID_CUENTA_SALDO
                    , DOCU.OID_FORMULARIO
                    , DOCU.BOL_ACREDITADO
                    , DOCU.BOL_NOTIFICADO
                    , DOCU.FYH_ACREDITACION
                    , TREF.OID_DIVISA
                    , TREF.OID_DENOMINACION
                    , TREF.COD_NIVEL_DETALLE
                    , TREF.BOL_DISPONIBLE
                    , TREF.NUM_IMPORTE
                    , TREF.NEL_CANTIDAD
                    , ]';

                    IF par$pag_reg_pagina IS NOT NULL THEN
                      var$consulta := var$consulta || q'[ rank() over(order by DOCU.GMT_CREACION, DOCU.OID_DOCUMENTO) rank_ ]';
                    ELSE
                      var$consulta := var$consulta || q'[ 1 rank_  ]';
                    END IF;

              var$consulta := var$consulta ||  q'[  FROM SAPR_TDOCUMENTO DOCU
          INNER JOIN SAPR_TTRANSACCION_EFECTIVO TREF ON TREF.OID_DOCUMENTO = DOCU.OID_DOCUMENTO AND
                                                        SYS_EXTRACT_UTC(TREF.GMT_CREACION) >= SYS_EXTRACT_UTC(DOCU.GMT_CREACION) AND
                                                        DOCU.OID_CUENTA_SALDO_DESTINO = TREF.OID_CUENTA_SALDO
          INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_DESTINO AND DOCU.OID_SECTOR_DESTINO = CUEN.OID_SECTOR
          INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = CUEN.OID_SECTOR
          INNER JOIN GEPR_TSUBCANAL SUBC ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL
          INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = DOCU.OID_FORMULARIO
          INNER JOIN SAPR_GTT_TAUXILIAR AUX ON
                  ( DOCU.FYH_GESTION >= TO_DATE(nvl(AUX.COD_CAMPO4, sysdate -180), 'DD/MM/YYYY HH24:MI:SS'))
                  AND AUX.COD_CALIFICADOR = 'FYH_GESTION' AND AUX.OID_CAMPO1 = CUEN.OID_SECTOR AND AUX.COD_CAMPO2 = SUBC.OID_CANAL ]';

          var$consultaShipOut := var$consultaShipOut || ' LEFT JOIN GEPR_TSECTOR SECTP ON SECTP.OID_SECTOR = CUEN.OID_SECTOR ';
          var$consulta := var$consulta || ' LEFT JOIN GEPR_TSECTOR SECTP ON SECTP.OID_SECTOR = CUEN.OID_SECTOR ';


        /* INNER JOIN PARA DELEGACION */
        IF var$oid_delegacion IS NOT NULL THEN
          var$consultaShipOut := var$consultaShipOut || ' INNER JOIN GEPR_TPLANTA PLANT ON PLANT.OID_PLANTA = SECTP.OID_PLANTA ';
          var$consulta := var$consulta || ' INNER JOIN GEPR_TPLANTA PLANT ON PLANT.OID_PLANTA = SECTP.OID_PLANTA ';
        END IF;

        /* INNER JOIN PARA VALORES */
        IF var$oid_divisa IS NOT NULL OR par$bol_disponible IS NOT NULL THEN
          var$consultaShipOut := var$consultaShipOut || ' LEFT JOIN SAPR_TTRANSACCION_EFECTIVO TREF ON TREF.OID_DOCUMENTO = MOVI.OID_DOCUMENTO ';
        END IF;


        /* NOTIFICADOS */
        IF par$bol_notificado IS NOT NULL THEN
          IF par$bol_notificado = 1 THEN
              var$consultaShipOut := var$consultaShipOut || ' INNER JOIN SAPR_TDOCUMENTO DOCU ON DOCU.OID_DOCUMENTO = MOVI.OID_DOCUMENTO AND DOCU.BOL_NOTIFICADO = 1 ';
          ELSIF par$bol_notificado = 0 THEN
              var$consultaShipOut := var$consultaShipOut || ' INNER JOIN SAPR_TDOCUMENTO DOCU ON DOCU.OID_DOCUMENTO = MOVI.OID_DOCUMENTO AND DOCU.BOL_NOTIFICADO = 0 ';
          END IF;
        END IF;

        var$consultaShipOut := var$consultaShipOut || ' WHERE 1 = 1 ';
        var$consulta := var$consulta || ' WHERE 1 = 1 ';

        /* FILTRAS FORMULARIOS */
        var$consulta := var$consulta || q'[ AND ((AUX.COD_CAMPO3 = 'DEB' AND FORM.COD_FORMULARIO = 'MAECID')OR (AUX.COD_CAMPO3 <> 'DEB' AND FORM.COD_FORMULARIO IN ('MAECIC', 'MAECOC')) OR (FORM.COD_FORMULARIO IN ('MAESOC', 'MAESOD'))) ]';

        /* FECHA GESTION */
        var$consulta := var$consulta || q'[ AND DOCU.FYH_GESTION <= TO_DATE(']' || TO_CHAR(var$fyh_gestion, 'DD/MM/YYYY HH24:MI:SS') || q'[', 'DD/MM/YYYY HH24:MI:SS')]';

        /* FECHA CREACION */
        IF var$fyh_creacion IS NOT NULL THEN
            var$consulta := var$consulta || q'[ AND SYS_EXTRACT_UTC(DOCU.GMT_CREACION) <= TO_TIMESTAMP(']'  || TO_CHAR(var$fyh_creacion, 'yyyy-mm-dd hh24:mi:ss') || q'[', 'yyyy-mm-dd hh24:mi:ss')]';
        END IF;

        /* COD_CLIENTE */
        IF var$oid_cliente IS NOT NULL THEN
          var$consulta := var$consulta || q'[  AND CUEN.OID_CLIENTE = ']' || var$oid_cliente || q'[' ]';
          var$consultaShipOut := var$consultaShipOut || q'[  AND CUEN.OID_CLIENTE = ']' || var$oid_cliente || q'[' ]';
        END IF;

        /* COD_SUBCLIENTE */
        IF var$oid_sub_cliente IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND CUEN.OID_SUBCLIENTE = ']' || var$oid_sub_cliente || q'[' ]';
          var$consultaShipOut := var$consultaShipOut || q'[ AND CUEN.OID_SUBCLIENTE = ']' || var$oid_sub_cliente || q'[' ]';
        END IF;

        /* COD_PTOSERVICO */
        IF var$oid_punto_servicio IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND CUEN.OID_PTO_SERVICIO = ']' || var$oid_punto_servicio || q'[' ]';
          var$consultaShipOut := var$consultaShipOut || q'[ AND CUEN.OID_PTO_SERVICIO = ']' || var$oid_punto_servicio || q'[' ]';
        END IF;

        /*MAQUINA VIGENTE*/
        IF par$bol_maqu_vigente = 1 THEN
          var$consulta := var$consulta || ' AND MAQU.BOL_ACTIVO = 1 ';
          var$consultaShipOut := var$consultaShipOut || ' AND MAQU.BOL_ACTIVO = 1 ';
        ELSIF par$bol_maqu_vigente = 0 THEN
          var$consulta := var$consulta || ' AND MAQU.BOL_ACTIVO = 0 ';
          var$consultaShipOut := var$consultaShipOut || ' AND MAQU.BOL_ACTIVO = 0 ';
        END IF;

        /* OID_PLANTA */
        IF var$oid_planta IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND SECTP.OID_PLANTA = ']' || var$oid_planta || q'[' ]';
          var$consultaShipOut := var$consultaShipOut || q'[ AND SECTP.OID_PLANTA = ']' || var$oid_planta || q'[' ]';
        END IF;

        /* OID_DELEGACION */
        IF var$oid_delegacion IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND PLANT.OID_DELEGACION = ']' || var$oid_delegacion || q'[' ]';
          var$consultaShipOut := var$consultaShipOut || q'[ AND PLANT.OID_DELEGACION = ']' || var$oid_delegacion || q'[' ]';
        END IF;

        /* OID_DIVISA */
        IF var$oid_divisa IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND TREF.OID_DIVISA = ']' || var$oid_divisa || q'[' ]';
          var$consultaShipOut := var$consultaShipOut || q'[ AND (TREF.OID_DIVISA = ']' || var$oid_divisa || q'['  OR TREF.OID_DIVISA IS NULL ) ]';
        END IF;

        /* OID_CANAL */
        IF var$oid_canales IS NOT NULL AND var$oid_canales.COUNT > 0 THEN

          var$consultaShipOut := var$consultaShipOut || ' AND CANA.OID_CANAL IN (';
          FOR idx IN var$oid_canales.first .. var$oid_canales.last LOOP
            IF idx > 0 THEN
              var$consultaShipOut := var$consultaShipOut || ', ';
            END IF;
            var$consultaShipOut := var$consultaShipOut || q'[']' || var$oid_canales(idx) || q'[']';
          END LOOP;
          var$consultaShipOut := var$consultaShipOut || ')';

        END IF;

          /* OID_SUBCANAL */
          IF var$oid_subcanales IS NOT NULL AND var$oid_subcanales.COUNT > 0 THEN

            var$consulta := var$consulta || ' AND CUEN.OID_SUBCANAL IN (';
            FOR idx IN var$oid_subcanales.first .. var$oid_subcanales.last LOOP
              IF idx > 0 THEN
                var$consulta := var$consulta || ', ';
              END IF;
              var$consulta := var$consulta || q'[']' || var$oid_subcanales(idx) || q'[']';
            END LOOP;
            var$consulta := var$consulta || ')';

            var$consultaShipOut := var$consultaShipOut || ' AND SUBC.OID_SUBCANAL IN (';
            FOR idx IN var$oid_subcanales.first .. var$oid_subcanales.last LOOP
              IF idx > 0 THEN
                var$consultaShipOut := var$consultaShipOut || ', ';
              END IF;
              var$consultaShipOut := var$consultaShipOut || q'[']' || var$oid_subcanales(idx) || q'[']';
            END LOOP;
            var$consultaShipOut := var$consultaShipOut || ')';

          END IF;

          /* VALORES DISPONIBLES */
          IF par$bol_disponible = 1 THEN
            var$consulta := var$consulta || ' AND TREF.BOL_DISPONIBLE = 1 ';
            var$consultaShipOut := var$consultaShipOut || ' AND TREF.BOL_DISPONIBLE = 1 ';
          ELSIF par$bol_disponible = 0 THEN
            var$consulta := var$consulta || ' AND TREF.BOL_DISPONIBLE = 0 ';
            var$consultaShipOut := var$consultaShipOut || ' AND TREF.BOL_DISPONIBLE = 0 ';
          END IF;

          /* NOTIFICADOS */
          IF par$bol_notificado IS NOT NULL THEN
            IF par$bol_notificado = 1 THEN
              var$consulta := var$consulta || ' AND DOCU.BOL_NOTIFICADO = 1 ';
            ELSIF par$bol_notificado = 0 THEN
              var$consulta := var$consulta || ' AND DOCU.BOL_NOTIFICADO = 0 ';
            END IF;
          END IF;

          /*  ACREDITADO */
          IF par$bol_acreditado IS NOT NULL THEN
            IF par$bol_acreditado = 1 THEN
              var$consulta := var$consulta || ' AND DOCU.BOL_ACREDITADO = 1 ';
            ELSIF par$bol_acreditado = 0 THEN
              var$consulta := var$consulta || ' AND DOCU.BOL_ACREDITADO = 0 ';
            END IF;
          END IF;


          /* MONTAR INSERT A TABLA TEMPORARIA */
          var$consultaShipOut := 'INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR) ' || var$consultaShipOut || ' GROUP BY CUEN.OID_SECTOR, CANA.OID_CANAL, COAJ.COD_AJENO ';

          var$consulta := var$consulta || ' ORDER BY DOCU.FYH_GESTION ';


        /* MONTAR PAGINACION (SI NECESARIO)*/
        IF par$pag_reg_pagina IS NOT NULL THEN
           var$pag_indice := par$pag_indice;
           IF var$pag_indice = 0 OR var$pag_indice IS NULL THEN
             var$pag_indice := 1;
           END IF;

           IF par$pag_reg_pagina > 0 THEN
              var$pag_reg_pagina := par$pag_reg_pagina;
              IF var$pag_indice > 1 THEN
                var$pag_reg_pagina := var$pag_indice * par$pag_reg_pagina;
                var$pag_indice := var$pag_reg_pagina - par$pag_reg_pagina + 1;
              END IF;
           END IF;

           var$consulta := ' SELECT * FROM ( ' || var$consulta || ' ) WHERE RANK_ BETWEEN ' || var$pag_indice || ' AND ' || var$pag_reg_pagina || ' ';
        END IF;

         var$consulta := 'INSERT INTO SAPR_GTT_TRECUPERARSALDOS(OID_DOCUMENTO,COD_EXTERNO, FYH_GESTION, GMT_CREACION, OID_CUENTA_SALDO, OID_FORMULARIO, BOL_ACREDITADO, BOL_NOTIFICADO,
                                                                FYH_ACREDITACION, OID_DIVISA, OID_DENOMINACION, COD_NIVEL_DETALLE, BOL_DISPONIBLE, NUM_IMPORTE, NEL_CANTIDAD)
                                                                WITH DOCUMENTOS AS ( ' || var$consulta ||' )
                                                                SELECT D.OID_DOCUMENTO, D.COD_EXTERNO, D.FYH_GESTION, D.GMT_CREACION, D.OID_CUENTA_SALDO
                                                                , D.OID_FORMULARIO, D.BOL_ACREDITADO, D.BOL_NOTIFICADO, D.FYH_ACREDITACION, D.OID_DIVISA
                                                                , D.OID_DENOMINACION, D.COD_NIVEL_DETALLE, D.BOL_DISPONIBLE, D.NUM_IMPORTE, D.NEL_CANTIDAD
                                                                  FROM DOCUMENTOS D
                                                                  ';

         /* EJECUTAR SHIPOUT */
         /* DBMS_OUTPUT.PUT_LINE(var$consultaShipOut); */
         EXECUTE IMMEDIATE var$consultaShipOut;

         /* EJECUTAR FILTRO PRINCIPAL */
         EXECUTE IMMEDIATE var$consulta;


      INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CAMPO2, Nel_Campo6, COD_CALIFICADOR)
         SELECT
          DISTINCT
          CUEN.OID_CUENTA,
          DIVI.OID_DIVISA,
          SAEF.BOL_DISPONIBLE AS BOL_DISPONIBLE,
          'CUENTA_DIVISA'
         FROM
             SAPR_TMAQUINA MAQU
             INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.COD_CALIFICADOR = 'OID_SECTOR' AND AUXM.OID_CAMPO1 = MAQU.OID_SECTOR
             INNER JOIN GEPR_TSECTOR SECT ON MAQU.OID_SECTOR = SECT.OID_SECTOR
             INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
             INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
             LEFT JOIN GEPR_TPUNTO_SERVICIO PUSE ON MAQU.OID_MAQUINA = PUSE.OID_MAQUINA
             LEFT JOIN GEPR_TSUBCLIENTE SUCL ON PUSE.OID_SUBCLIENTE = SUCL.OID_SUBCLIENTE
             LEFT JOIN GEPR_TCLIENTE CLIE ON SUCL.OID_CLIENTE = CLIE.OID_CLIENTE
             LEFT JOIN SAPR_TCUENTA CUEN ON SECT.OID_SECTOR = CUEN.OID_SECTOR AND
                                            PUSE.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO AND
                                            SUCL.OID_SUBCLIENTE = CUEN.OID_SUBCLIENTE AND
                                            CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
             LEFT JOIN SAPR_TSALDO_EFECTIVO SAEF ON CUEN.OID_CUENTA = SAEF.OID_CUENTA_SALDO
             INNER JOIN GEPR_TDIVISA DIVI ON SAEF.OID_DIVISA = DIVI.OID_DIVISA
             CROSS JOIN GEPR_TCANAL CANA
             INNER JOIN GEPR_TSUBCANAL SUCA ON CANA.OID_CANAL = SUCA.OID_CANAL
             INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.COD_CALIFICADOR = 'OID_CANAL' AND AUXM.OID_CAMPO1 = CANA.OID_CANAL
             INNER JOIN SAPR_GTT_TAUXILIAR AUXS ON (AUXS.COD_CALIFICADOR = 'OID_SUBCANAL' AND AUXS.OID_CAMPO1 = SUCA.OID_SUBCANAL)
       WHERE  DIVI.OID_DIVISA IS NOT NULL AND CUEN.OID_CUENTA IS NOT NULL
          ;


 IF par$cod_identificador_ajeno IS NULL THEN

              /* CARGAR DATOS DEL LAS CUENTAS */
              OPEN par$rc_cuentas FOR
                   WITH CUENTAS AS (
                          SELECT TREF.OID_CUENTA_SALDO
                            FROM SAPR_GTT_TRECUPERARSALDOS TREF
                        GROUP BY TREF.OID_CUENTA_SALDO
                   )
                     SELECT DISTINCT NVL(CUEN.OID_CUENTA, AUX.OID_CUENTA) OID_CUENTA
                           , NVL(CLIE.COD_CLIENTE,AUX.COD_CLIENTE) COD_CLIENTE
                           , NVL(CLIE.DES_CLIENTE,AUX.DES_CLIENTE) DES_CLIENTE
                           , NVL(SUBC.COD_SUBCLIENTE,AUX.COD_SUBCLIENTE) COD_SUBCLIENTE
                           , NVL(SUBC.DES_SUBCLIENTE,AUX.DES_SUBCLIENTE) DES_SUBCLIENTE
                           , NVL(PTOS.COD_PTO_SERVICIO,AUX.COD_PTO_SERVICIO) COD_PTO_SERVICIO
                           , NVL(PTOS.COD_PTO_SERVICIO,AUX.DES_PTO_SERVICIO) DES_PTO_SERVICIO
                           , NVL(DELE.COD_DELEGACION, AUX.COD_DELEGACION) COD_DELEGACION
                           , NVL(DELE.DES_DELEGACION, AUX.DES_DELEGACION) DES_DELEGACION
                           , NVL(PLANT.COD_PLANTA, AUX.COD_PLANTA) COD_PLANTA
                           , NVL(PLANT.DES_PLANTA, AUX.DES_PLANTA) DES_PLANTA
                           , NVL(SECT.COD_SECTOR, AUX.COD_SECTOR) COD_SECTOR
                           , NVL(SECT.DES_SECTOR, AUX.DES_SECTOR) DES_SECTOR
                           , NVL(CANA.COD_CANAL, AUX.COD_CANAL) COD_CANAL
                           , NVL(CANA.DES_CANAL, AUX.DES_CANAL) DES_CANAL
                           , NVL(SUBCN.COD_SUBCANAL, AUX.COD_SUBCANAL) COD_SUBCANAL
                           , NVL(SUBCN.DES_SUBCANAL, AUX.DES_SUBCANAL) DES_SUBCANAL
                           , DELE.NEC_GMT_MINUTOS
                           , DELE.FYH_VERANO_INICIO
                           , DELE.FYH_VERANO_FIN
                           , DELE.NEC_VERANO_AJUSTE
                           , NVL(MAQU.COD_IDENTIFICACION, AUX.COD_IDENTIFICACION) COD_IDENTIFICACION
                           , NVL(MAQU.BOL_ACTIVO, AUX.BOL_ACTIVO) BOL_ACTIVO
                        FROM CUENTAS
                 INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = CUENTAS.OID_CUENTA_SALDO
                 INNER JOIN GEPR_TCLIENTE CLIE ON CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
                  LEFT JOIN GEPR_TSUBCLIENTE SUBC ON SUBC.OID_SUBCLIENTE = CUEN.OID_SUBCLIENTE
                  LEFT JOIN GEPR_TPUNTO_SERVICIO PTOS ON PTOS.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO
                 INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = CUEN.OID_SECTOR
                 INNER JOIN GEPR_TPLANTA PLANT ON PLANT.OID_PLANTA = SECT.OID_PLANTA
                 INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = PLANT.OID_DELEGACION
                  LEFT JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = SECT.OID_SECTOR
                 INNER JOIN GEPR_TSUBCANAL SUBCN ON SUBCN.OID_SUBCANAL = CUEN.OID_SUBCANAL
                 INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = SUBCN.OID_CANAL
          INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.COD_CALIFICADOR = 'OID_CANAL' AND AUXM.OID_CAMPO1 = CANA.OID_Canal
                 INNER JOIN SAPR_GTT_TAUXILIAR AUXS ON (AUXS.COD_CALIFICADOR = 'OID_SUBCANAL' AND AUXS.OID_CAMPO1 = SUBCN.OID_Subcanal)

                     FULL OUTER JOIN
                 (SELECT
  DISTINCT
      CUEN.OID_CUENTA
    , SECT.COD_SECTOR COD_SECTOR
    , SECT.DES_SECTOR DES_SECTOR
    , CANA.COD_CANAL AS COD_CANAL
    , CANA.DES_CANAL AS DES_CANAL
    , SUCA.COD_SUBCANAL AS COD_SUBCANAL
    , SUCA.DES_SUBCANAL AS DES_SUBCANAL
    , CLIE.COD_CLIENTE AS COD_CLIENTE
    , CLIE.DES_CLIENTE AS DES_CLIENTE
    , SUCL.COD_SUBCLIENTE AS COD_SUBCLIENTE
    , SUCL.DES_SUBCLIENTE AS DES_SUBCLIENTE
    , PUSE.COD_PTO_SERVICIO AS COD_PTO_SERVICIO
    , PUSE.DES_PTO_SERVICIO AS DES_PTO_SERVICIO
    , PLAN.COD_PLANTA AS COD_PLANTA
    , PLAN.DES_PLANTA AS DES_PLANTA
    , DELE.COD_DELEGACION AS COD_DELEGACION
    , DELE.DES_DELEGACION AS DES_DELEGACION
    , MAQU.OID_SECTOR
    , MAQU.COD_IDENTIFICACION
    , MAQU.BOL_ACTIVO
    , DIVI.COD_ISO_DIVISA
    , DIVI.DES_DIVISA
    , 0 IMPORTE
    , SAEF.BOL_DISPONIBLE
  FROM
   SAPR_TMAQUINA MAQU
    INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.COD_CALIFICADOR = 'OID_SECTOR' AND AUXM.OID_CAMPO1 = MAQU.OID_SECTOR
   INNER JOIN GEPR_TSECTOR SECT ON MAQU.OID_SECTOR = SECT.OID_SECTOR
   INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = SECT.OID_SECTOR
   INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
   INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
   LEFT JOIN GEPR_TPUNTO_SERVICIO PUSE ON MAQU.OID_MAQUINA = PUSE.OID_MAQUINA
   LEFT JOIN GEPR_TSUBCLIENTE SUCL ON PUSE.OID_SUBCLIENTE = SUCL.OID_SUBCLIENTE
   LEFT JOIN GEPR_TCLIENTE CLIE ON SUCL.OID_CLIENTE = CLIE.OID_CLIENTE
   LEFT JOIN SAPR_TCUENTA CUEN ON SECT.OID_SECTOR = CUEN.OID_SECTOR AND
                                  PUSE.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO AND
                                  SUCL.OID_SUBCLIENTE = CUEN.OID_SUBCLIENTE AND
                                  CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
   LEFT JOIN SAPR_TSALDO_EFECTIVO SAEF ON CUEN.OID_CUENTA = SAEF.OID_CUENTA_SALDO
   LEFT JOIN GEPR_TDIVISA DIVI ON SAEF.OID_DIVISA = DIVI.OID_DIVISA
   CROSS JOIN GEPR_TCANAL CANA
   INNER JOIN GEPR_TSUBCANAL SUCA ON CANA.OID_CANAL = SUCA.OID_CANAL
  INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.COD_CALIFICADOR = 'OID_CANAL' AND AUXM.OID_CAMPO1 = CANA.OID_CANAL
  INNER JOIN SAPR_GTT_TAUXILIAR AUXS ON AUXS.COD_CALIFICADOR = 'OID_SUBCANAL' AND AUXS.OID_CAMPO1 = SUCA.OID_SUBCANAL
  WHERE  (par$bol_maqu_vigente IS NULL OR MAQU.Bol_Activo = par$bol_maqu_vigente)
   ) AUX
   ON AUX.OID_SECTOR = SECT.OID_SECTOR ;



              /* CARGAR DATOS DE LAS DIVISAS */
              OPEN par$rc_divisas FOR
                  WITH DIVISAS AS (
                          SELECT  DIV.OID_DIVISA from GEPR_TDIVISA DIV WHERE DIV.BOL_VIGENTE = 1
                  )
                    SELECT D.OID_DIVISA
                         , DIV.COD_ISO_DIVISA
                         , DIV.DES_DIVISA
                      FROM DIVISAS D
                INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = D.OID_DIVISA;

              /* CARGAR DATOS DE LAS DENOMINACIONES */
              OPEN par$rc_denominaciones FOR
                  WITH DENOMINACIONES AS (
                          SELECT TREF.OID_DIVISA
                               , TREF.OID_DENOMINACION
                            FROM SAPR_GTT_TRECUPERARSALDOS TREF
                           WHERE TREF.OID_DENOMINACION IS NOT NULL
                        GROUP BY TREF.OID_DIVISA
                               , TREF.OID_DENOMINACION
                  )
                    SELECT D.OID_DENOMINACION
                         , DEN.COD_DENOMINACION
                         , DEN.DES_DENOMINACION
                      FROM DENOMINACIONES D
                INNER JOIN GEPR_TDENOMINACION DEN ON DEN.OID_DIVISA = D.OID_DIVISA AND DEN.OID_DENOMINACION = D.OID_DENOMINACION;

         ELSE



              /* CARGAR DATOS DEL LAS CUENTAS */
              OPEN par$rc_cuentas FOR
                   WITH CUENTAS AS (
                          SELECT TREF.OID_CUENTA_SALDO
                            FROM SAPR_GTT_TRECUPERARSALDOS TREF
                        GROUP BY TREF.OID_CUENTA_SALDO
                   )
                    SELECT  DISTINCT NVL(CUEN.OID_CUENTA,AUX.OID_CUENTA) OID_CUENTA
                           , NVL(CAC.COD_AJENO,AUX.COD_CLIENTE) COD_CLIENTE
                           , NVL(CAC.DES_AJENO,AUX.DES_CLIENTE) DES_CLIENTE
                           , NVL(CASC.COD_AJENO,AUX.COD_SUBCLIENTE) COD_SUBCLIENTE
                           , NVL(CASC.DES_AJENO,AUX.DES_SUBCLIENTE) DES_SUBCLIENTE
                           , NVL(CAPS.COD_AJENO,AUX.COD_PTO_SERVICIO) COD_PTO_SERVICIO
                           , NVL(CAPS.DES_AJENO,AUX.DES_PTO_SERVICIO) DES_PTO_SERVICIO
                           , CAD.COD_AJENO COD_DELEGACION
                           , CAD.DES_AJENO DES_DELEGACION
                           , CAP.COD_AJENO COD_PLANTA
                           , CAP.DES_AJENO DES_PLANTA
                           , NVL(CAS.COD_AJENO,AUX.COD_SECTOR) COD_SECTOR
                           , NVL(CAS.DES_AJENO, AUX.DES_SECTOR) DES_SECTOR
                           , NVL(CACAN.COD_AJENO, AUX.COD_CANAL) COD_CANAL
                           , NVL(CACAN.DES_AJENO, AUX.DES_CANAL) DES_CANAL
                           , NVL(CASCA.COD_AJENO, AUX.COD_SUBCANAL) COD_SUBCANAL
                           , NVL(CASCA.DES_AJENO, AUX.DES_SUBCANAL) DES_SUBCANAL
                           , NVL(MAQU.COD_IDENTIFICACION, AUX.COD_IDENTIFICACION) COD_IDENTIFICACION
                           , NVL(MAQU.BOL_ACTIVO, AUX.BOL_ACTIVO) BOL_ACTIVO
                        FROM CUENTAS
                         INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = CUENTAS.OID_CUENTA_SALDO
                         INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = CUEN.OID_SECTOR
                         INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = SECT.OID_SECTOR
                         INNER JOIN GEPR_TPLANTA PLANT ON PLANT.OID_PLANTA = SECT.OID_PLANTA
                         INNER JOIN GEPR_TSUBCANAL SUBCN ON SUBCN.OID_SUBCANAL = CUEN.OID_SUBCANAL
                         INNER JOIN GEPR_TCODIGO_AJENO CAS
                            ON CAS.OID_TABLA_GENESIS = CUEN.OID_SECTOR AND CAS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAS.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
                         INNER JOIN GEPR_TCODIGO_AJENO CAP
                            ON CAP.OID_TABLA_GENESIS = PLANT.OID_PLANTA AND CAP.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAP.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
                         INNER JOIN GEPR_TCODIGO_AJENO CAD
                            ON CAD.OID_TABLA_GENESIS = PLANT.OID_DELEGACION AND CAD.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAD.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
                         INNER JOIN GEPR_TCODIGO_AJENO CAC
                            ON CAC.OID_TABLA_GENESIS = CUEN.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                          LEFT JOIN GEPR_TCODIGO_AJENO CASC
                            ON CASC.OID_TABLA_GENESIS = CUEN.OID_SUBCLIENTE AND CASC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                          LEFT JOIN GEPR_TCODIGO_AJENO CAPS
                            ON CAPS.OID_TABLA_GENESIS = CUEN.OID_PTO_SERVICIO AND CAPS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAPS.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                         INNER JOIN GEPR_TCODIGO_AJENO CACAN
                            ON CACAN.OID_TABLA_GENESIS = SUBCN.OID_CANAL AND CACAN.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CACAN.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
                         INNER JOIN GEPR_TCODIGO_AJENO CASCA
                            ON CASCA.OID_TABLA_GENESIS = SUBCN.OID_SUBCANAL AND CASCA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASCA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
              INNER JOIN SAPR_GTT_TAUXILIAR AUXM
                ON AUXM.COD_CALIFICADOR = 'OID_CANAL' AND AUXM.OID_CAMPO1 = CACAN.OID_TABLA_GENESIS
                         INNER JOIN GEPR_TCODIGO_AJENO CASCA
                            ON CASCA.OID_TABLA_GENESIS = SUBCN.OID_SUBCANAL AND CASCA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASCA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
                          INNER JOIN SAPR_GTT_TAUXILIAR AUXS
                ON (AUXS.COD_CALIFICADOR = 'OID_SUBCANAL' AND AUXS.OID_CAMPO1 = CASCA.OID_TABLA_GENESIS)
                            FULL OUTER JOIN
                 (SELECT
  DISTINCT
     CUEN.OID_CUENTA
    ,CAS.COD_AJENO COD_SECTOR
    , CAS.DES_AJENO DES_SECTOR
    , CACAN.COD_AJENO COD_CANAL
    , CACAN.DES_AJENO DES_CANAL
    , CASCA.COD_AJENO COD_SUBCANAL
    , CASCA.DES_AJENO DES_SUBCANAL

    , CAC.COD_AJENO COD_CLIENTE
    , CAC.DES_AJENO DES_CLIENTE

    , CASC.COD_AJENO COD_SUBCLIENTE
    , CASC.DES_AJENO DES_SUBCLIENTE

    , CAPS.COD_AJENO COD_PTO_SERVICIO
    , CAPS.DES_AJENO DES_PTO_SERVICIO
    , MAQU.OID_SECTOR
    , MAQU.COD_IDENTIFICACION
    , MAQU.BOL_ACTIVO
    , DIVI.COD_ISO_DIVISA
    , DIVI.DES_DIVISA
    , 0 IMPORTE
    , SAEF.BOL_DISPONIBLE
 FROM
  SAPR_TMAQUINA MAQU

  INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.COD_CALIFICADOR = 'OID_SECTOR' AND AUXM.OID_CAMPO1 = MAQU.OID_SECTOR
  INNER JOIN GEPR_TSECTOR SECT ON MAQU.OID_SECTOR = SECT.OID_SECTOR
  INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = SECT.OID_SECTOR
  INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
  INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
  LEFT JOIN GEPR_TPUNTO_SERVICIO PUSE ON MAQU.OID_MAQUINA = PUSE.OID_MAQUINA
  LEFT JOIN GEPR_TSUBCLIENTE SUCL ON PUSE.OID_SUBCLIENTE = SUCL.OID_SUBCLIENTE
  LEFT JOIN GEPR_TCLIENTE CLIE ON SUCL.OID_CLIENTE = CLIE.OID_CLIENTE
  LEFT JOIN SAPR_TCUENTA CUEN ON SECT.OID_SECTOR = CUEN.OID_SECTOR AND
                                 PUSE.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO AND
                                 SUCL.OID_SUBCLIENTE = CUEN.OID_SUBCLIENTE AND
                                 CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
  LEFT JOIN SAPR_TSALDO_EFECTIVO SAEF ON CUEN.OID_CUENTA = SAEF.OID_CUENTA_SALDO
  LEFT JOIN GEPR_TDIVISA DIVI ON SAEF.OID_DIVISA = DIVI.OID_DIVISA
  CROSS JOIN GEPR_TCANAL CANA
  INNER JOIN GEPR_TSUBCANAL SUCA ON CANA.OID_CANAL = SUCA.OID_CANAL
    INNER JOIN GEPR_TCODIGO_AJENO CAS
       ON CAS.OID_TABLA_GENESIS = MAQU.OID_SECTOR AND CAS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAS.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
    INNER JOIN GEPR_TCODIGO_AJENO CACAN
       ON CACAN.OID_TABLA_GENESIS = CANA.OID_CANAL AND CACAN.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CACAN.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
    INNER JOIN SAPR_GTT_TAUXILIAR AUXM
       ON AUXM.COD_CALIFICADOR = 'OID_CANAL' AND AUXM.OID_CAMPO1 = CACAN.OID_TABLA_GENESIS
    INNER JOIN GEPR_TCODIGO_AJENO CASCA
       ON CASCA.OID_TABLA_GENESIS = SUCA.OID_SUBCANAL AND CASCA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASCA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
    INNER JOIN SAPR_GTT_TAUXILIAR AUXS
       ON AUXS.COD_CALIFICADOR = 'OID_SUBCANAL' AND AUXS.OID_CAMPO1 = CASCA.OID_TABLA_GENESIS
    INNER JOIN GEPR_TCODIGO_AJENO CAC
      ON CAC.OID_TABLA_GENESIS = CLIE.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
    LEFT JOIN GEPR_TCODIGO_AJENO CASC
      ON CASC.OID_TABLA_GENESIS = SUCL.OID_SUBCLIENTE AND CASC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
    LEFT JOIN GEPR_TCODIGO_AJENO CAPS
      ON CAPS.OID_TABLA_GENESIS = PUSE.OID_PTO_SERVICIO AND CAPS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAPS.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
    WHERE  (par$bol_maqu_vigente IS NULL OR MAQU.Bol_Activo = par$bol_maqu_vigente)
   ) AUX
   ON AUX.OID_SECTOR = SECT.OID_SECTOR
   where  (par$bol_maqu_vigente IS NULL OR MAQU.Bol_Activo = par$bol_maqu_vigente);


              /* CARGAR DATOS DE LAS DIVISAS */
              OPEN par$rc_divisas FOR
                  WITH DIVISAS AS (
                        SELECT  DIV.OID_DIVISA from GEPR_TDIVISA DIV WHERE DIV.BOL_VIGENTE = 1
                  )
                    SELECT D.OID_DIVISA
                         , DIV.COD_AJENO COD_ISO_DIVISA
                         , DIV.DES_AJENO DES_DIVISA
                      FROM DIVISAS D
                INNER JOIN GEPR_TCODIGO_AJENO DIV ON DIV.OID_TABLA_GENESIS = D.OID_DIVISA
                       AND DIV.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                       AND DIV.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA';

              /* CARGAR DATOS DE LAS DENOMINACIONES */
              OPEN par$rc_denominaciones FOR
                  WITH DENOMINACIONES AS (
                          SELECT TREF.OID_DIVISA
                               , TREF.OID_DENOMINACION
                            FROM SAPR_GTT_TRECUPERARSALDOS TREF
                           WHERE TREF.OID_DENOMINACION IS NOT NULL
                        GROUP BY TREF.OID_DIVISA
                               , TREF.OID_DENOMINACION
                  )
                    SELECT D.OID_DENOMINACION
                         , DEN.COD_AJENO COD_DENOMINACION
                         , DEN.DES_AJENO DES_DENOMINACION
                      FROM DENOMINACIONES D
                INNER JOIN GEPR_TCODIGO_AJENO DEN ON DEN.OID_TABLA_GENESIS = D.OID_DENOMINACION
                       AND DEN.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                       AND DEN.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION';

         END IF;

         /* CARGAR DATOS DEL LOS SALDOS */
         OPEN par$rc_saldos FOR
             WITH EFECTIVOS AS (
                SELECT TREF.OID_CUENTA_SALDO
                     , TREF.OID_DIVISA
                     , TREF.OID_DENOMINACION
                     , TREF.COD_NIVEL_DETALLE
                     , TREF.BOL_DISPONIBLE
                     , TREF.NUM_IMPORTE
                     , TREF.NEL_CANTIDAD
                  FROM SAPR_GTT_TRECUPERARSALDOS TREF
          INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = TREF.OID_FORMULARIO AND FORM.COD_FORMULARIO NOT IN ('MAESOC', 'MAESOD')
              )
         SELECT
               NVL( SALDO.OID_CUENTA_SALDO, AUX.OID_CAMPO1) AS OID_CUENTA_SALDO
              ,NVL(SALDO.OID_DIVISA, AUX.COD_CAMPO2) AS OID_DIVISA
              ,NVL(SALDO.OID_DENOMINACION, NULL) AS OID_DENOMINACION
              ,NVL(SALDO.COD_NIVEL_DETALLE, 'T') AS COD_NIVEL_DETALLE
              ,NVL(SALDO.BOL_DISPONIBLE, AUX.Nel_Campo6) AS BOL_DISPONIBLE
              ,NVL(SALDO.NUM_IMPORTE, '0') AS NUM_IMPORTE
              ,NVL(SALDO.NEL_CANTIDAD, '0') AS NEL_CANTIDAD
              FROM SAPR_GTT_TAUXILIAR AUX
              LEFT JOIN
              (
                SELECT TREF.OID_CUENTA_SALDO
                     , TREF.OID_DIVISA
                     , NULL OID_DENOMINACION
                     , 'T' COD_NIVEL_DETALLE
                     , TREF.BOL_DISPONIBLE
                     , SUM(TREF.NUM_IMPORTE) NUM_IMPORTE
                     , NULL NEL_CANTIDAD
                  FROM EFECTIVOS TREF
                 WHERE 1 = 1
                   AND (par$bol_disponible IS NULL OR TREF.BOL_DISPONIBLE = par$bol_disponible)
              GROUP BY TREF.OID_CUENTA_SALDO
                     , TREF.OID_DIVISA
                     , TREF.BOL_DISPONIBLE
            UNION ALL
                SELECT TREF.OID_CUENTA_SALDO
                     , TREF.OID_DIVISA
                     , TREF.OID_DENOMINACION
                     , TREF.COD_NIVEL_DETALLE
                     , TREF.BOL_DISPONIBLE
                     , SUM(TREF.NUM_IMPORTE)
                     , SUM(TREF.NEL_CANTIDAD)
                  FROM EFECTIVOS TREF
                  WHERE 1 = 1
                    AND par$bol_detallar_valores = 1
                    AND (par$bol_disponible IS NULL OR TREF.BOL_DISPONIBLE = par$bol_disponible)
                    AND TREF.COD_NIVEL_DETALLE = 'D'
               GROUP BY TREF.OID_CUENTA_SALDO
                     , TREF.OID_DIVISA
                     , TREF.OID_DENOMINACION
                     , TREF.COD_NIVEL_DETALLE
                     , TREF.BOL_DISPONIBLE) SALDO
                     ON AUX.COD_CALIFICADOR = 'CUENTA_DIVISA' AND AUX.OID_CAMPO1 = SALDO.OID_CUENTA_SALDO AND AUX.COD_CAMPO2 = SALDO.OID_DIVISA AND TO_NUMBER(AUX.Nel_Campo6) = SALDO.BOL_DISPONIBLE
                     ;

         IF par$bol_detallar_bolsas = 1 THEN

             /* CARGAR DATOS DEL LOS SALDOS */
             OPEN par$rc_bolsas FOR
                WITH DOCUMENTOS AS (
                      SELECT TREF.OID_CUENTA_SALDO
                           , TREF.OID_DIVISA
                           , TREF.NUM_IMPORTE
                           , TREF.OID_DOCUMENTO
                        FROM SAPR_GTT_TRECUPERARSALDOS TREF
            INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = TREF.OID_FORMULARIO AND FORM.COD_FORMULARIO NOT IN ('MAESOC', 'MAESOD')
                    )
                  SELECT T.OID_CUENTA_SALDO
                       , T.OID_DIVISA
                       , TEDO.DES_VALOR
                       , SUM(T.NUM_IMPORTE) NUM_IMPORTE
                    FROM SAPR_TVALOR_TERMINOXDOCUMENTO TEDO
              INNER JOIN DOCUMENTOS T ON T.OID_DOCUMENTO = TEDO.OID_DOCUMENTO
              INNER JOIN GEPR_TTERMINO TERM ON TERM.OID_TERMINO = TEDO.OID_TERMINO
              WHERE UPPER(TERM.COD_TERMINO) = 'SAFEBAG'
              GROUP BY T.OID_CUENTA_SALDO
                       , T.OID_DIVISA
                       , TEDO.DES_VALOR;

         END IF;

         IF par$bol_detallar_trans = 1 THEN

            /* CARGAR DATOS DEL LOS MOVIMIENTOS */
               OPEN par$rc_movimientos FOR
                  SELECT DOCU.OID_DOCUMENTO
                       , DOCU.COD_EXTERNO
                       , to_char(DOCU.FYH_GESTION, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00'  AS FYH_GESTION
                       , to_char(DOCU.gmt_creacion, 'YYYY-MM-DD HH24:MI:SS TZR') AS GMT_CREACION
                       , DOCU.OID_CUENTA_SALDO AS OID_CUENTA_SALDO
                       , DOCU.OID_FORMULARIO
                       , DOCU.BOL_ACREDITADO
                       , DOCU.BOL_NOTIFICADO
             , CASE WHEN  DOCU.FYH_ACREDITACION is null THEN ''   ELSE to_char(DOCU.FYH_ACREDITACION, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00'  END AS FYH_ACREDITACION
                       , DOCU.BOL_DISPONIBLE
                    FROM SAPR_GTT_TRECUPERARSALDOS DOCU
                GROUP BY DOCU.OID_DOCUMENTO, DOCU.COD_EXTERNO, DOCU.FYH_GESTION, DOCU.GMT_CREACION, DOCU.OID_CUENTA_SALDO
                       , DOCU.OID_FORMULARIO, DOCU.BOL_ACREDITADO, DOCU.BOL_NOTIFICADO, DOCU.FYH_ACREDITACION, DOCU.BOL_DISPONIBLE
                ORDER BY DOCU.FYH_GESTION;

            /* CARGAR DATOS DEL LOS FORMULARIOS */
               OPEN par$rc_formularios FOR
                  WITH FORMULARIOS AS (
                        SELECT DOCU.OID_FORMULARIO
                          FROM SAPR_GTT_TRECUPERARSALDOS DOCU
                      GROUP BY DOCU.OID_FORMULARIO
                      )
                  SELECT FORM.OID_FORMULARIO
                       , FORM.COD_FORMULARIO
                       , FORM.DES_FORMULARIO
                    FROM SAPR_TFORMULARIO FORM
              INNER JOIN FORMULARIOS F ON FORM.OID_FORMULARIO = F.OID_FORMULARIO;


           /* CARGAR DATOS DEL LOS EFETIVOS */
              OPEN par$rc_efectivos FOR
               WITH EFECTIVOS AS (
                      SELECT TREF.OID_DOCUMENTO
                           , TREF.OID_DIVISA
                           , TREF.OID_DENOMINACION
                           , TREF.COD_NIVEL_DETALLE
                           , TREF.BOL_DISPONIBLE
                           , TREF.NUM_IMPORTE
                           , TREF.NEL_CANTIDAD
                        FROM SAPR_GTT_TRECUPERARSALDOS TREF
              )
                SELECT TREF.OID_DOCUMENTO
                     , TREF.OID_DIVISA
                     , NULL OID_DENOMINACION
                     , 'T' COD_NIVEL_DETALLE
                     , TREF.BOL_DISPONIBLE
                     , SUM(TREF.NUM_IMPORTE) NUM_IMPORTE
                     , NULL NEL_CANTIDAD
                  FROM EFECTIVOS TREF
                 WHERE 1 = 1
                 AND (par$bol_disponible IS NULL OR TREF.BOL_DISPONIBLE = par$bol_disponible)
              GROUP BY TREF.OID_DOCUMENTO
                     , TREF.OID_DIVISA
                     , TREF.BOL_DISPONIBLE
            UNION ALL
                SELECT TREF.OID_DOCUMENTO
                     , TREF.OID_DIVISA
                     , TREF.OID_DENOMINACION
                     , TREF.COD_NIVEL_DETALLE
                     , TREF.BOL_DISPONIBLE
                     , TREF.NUM_IMPORTE
                     , TREF.NEL_CANTIDAD
                  FROM EFECTIVOS TREF
                  WHERE 1 = 1
                    AND par$bol_detallar_valores = 1
                    AND (par$bol_disponible IS NULL OR TREF.BOL_DISPONIBLE = par$bol_disponible)
                    AND TREF.COD_NIVEL_DETALLE = 'D';


           IF par$bol_info_adicional = 1 THEN
             /* CARGAR DATOS DEL LOS TERMINOS */
             OPEN par$rc_terminos FOR
                WITH DOCUMENTOS AS (
                      SELECT DOCU.OID_DOCUMENTO
                        FROM SAPR_GTT_TRECUPERARSALDOS DOCU
                    GROUP BY DOCU.OID_DOCUMENTO
                    )
                  SELECT TEDO.OID_DOCUMENTO
                       , TERM.COD_TERMINO
                       , TEDO.DES_VALOR
                    FROM SAPR_TVALOR_TERMINOXDOCUMENTO TEDO
              INNER JOIN DOCUMENTOS D ON D.OID_DOCUMENTO = TEDO.OID_DOCUMENTO
              INNER JOIN GEPR_TTERMINO TERM ON TERM.OID_TERMINO = TEDO.OID_TERMINO;
          END IF;

         END IF;
      END IF;

      /* Validaciones */
      OPEN par$validaciones FOR
           SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION
             FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'VALIDACIONES';

      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           var$inserts,
                                                           var$updates,
                                                           var$deletes,
                                                           var$merges,
                                                           var$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'S',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);

      COMMIT;

      /* ===  Tratar excepciones - Rollback === */
      EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK;

          var$err := gepr_putilidades_###VERSION###.fformat_error;
          var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, var$err, true);
          gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                               const$nombre_func,
                                                               var$inserts,
                                                               var$updates,
                                                               var$deletes,
                                                               var$merges,
                                                               var$selects,
                                                               var$registros,
                                                               var$inicio,
                                                               null,
                                                               'R',
                                                               par$info_ejecucion,
                                                               var$err,
                                                               par$cod_usuario,
                                                               par$cod_ejecucion);

  END srecuperar_saldos;

  /* Validar codigo Ajeno e preencher tabela temporaria */
  PROCEDURE svalidar_codigos(par$cod_cliente              IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_sub_cliente          IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_punto_servicio       IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_delegacion           IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_planta               IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_divisa               IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_maquinas             IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_canales              IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_subcanales           IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$oid_cliente             OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_sub_cliente         OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_punto_servicio      OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_delegacion          OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_planta              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_divisa              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_canales             OUT gepr_pcomon_###VERSION###.tipo$oids_,
                             par$oid_subcanales          OUT gepr_pcomon_###VERSION###.tipo$oids_,
                             par$selects                  IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_) IS

      var$existe gepr_pcomon_###VERSION###.tipo$cod_;

      /* MAQUINAS */
      CURSOR cur$maquinas IS
         SELECT T.OID_CAMPO1 COD_MAQUINA
              , S.OID_SECTOR
           FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN SAPR_TMAQUINA M ON M.COD_IDENTIFICACION = T.OID_CAMPO1
      LEFT JOIN GEPR_TSECTOR S ON S.OID_SECTOR = M.OID_SECTOR
          WHERE T.COD_CALIFICADOR = 'COD_MAQUINA';

      /* CANALES */
      CURSOR cur$canales IS
         SELECT T.OID_CAMPO1 COD_CANAL
              , C.OID_CANAL
           FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN GEPR_TCANAL C ON C.COD_CANAL = T.OID_CAMPO1
          WHERE T.COD_CALIFICADOR = 'COD_CANAL';

      CURSOR cur$canales_ajeno IS
         SELECT T.OID_CAMPO1 COD_CANAL
              , CA.OID_TABLA_GENESIS OID_CANAL
           FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_AJENO = T.OID_CAMPO1
                                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
                                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                                     AND CA.BOL_ACTIVO = 1
          WHERE T.COD_CALIFICADOR = 'COD_CANAL';

      /* SUBCANALES */
      CURSOR cur$subcanales IS
         SELECT T.OID_CAMPO1 COD_SUBCANAL
              , SC.OID_SUBCANAL
           FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN GEPR_TSUBCANAL SC ON SC.COD_SUBCANAL = T.OID_CAMPO1
          WHERE T.COD_CALIFICADOR = 'COD_SUBCANAL';

      CURSOR cur$subcanales_ajeno IS
         SELECT T.OID_CAMPO1 COD_SUBCANAL
              , CA.OID_TABLA_GENESIS OID_SUBCANAL
           FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_AJENO = T.OID_CAMPO1
                                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
                                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                                     AND CA.BOL_ACTIVO = 1
          WHERE T.COD_CALIFICADOR = 'COD_SUBCANAL';


   BEGIN

      /* MAQUINAS */
      IF par$cod_maquinas IS NOT NULL AND par$cod_maquinas.COUNT > 1  THEN
         /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LAS MAQUINAS */
         FOR idx IN par$cod_maquinas.first .. par$cod_maquinas.last LOOP
           IF par$cod_maquinas(idx) IS NOT NULL THEN
               INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                VALUES (par$cod_maquinas(idx), 'COD_MAQUINA');
           END IF;
         END LOOP;

         /* CANSULTA DOS CODIGOS Y HACE LAS VALIDACIONES NECESARIAS */
         FOR rec$maquina IN cur$maquinas LOOP
            IF rec$maquina.OID_SECTOR IS NULL THEN

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060008', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060008',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                rec$maquina.cod_maquina,
                                                0), 'VALIDACIONES');
            ELSE
                INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                VALUES (rec$maquina.OID_SECTOR, 'OID_SECTOR');
            END IF;
         END LOOP rec$maquina;

      END IF;

      /* CANALES */
      IF par$cod_canales IS NOT NULL AND par$cod_canales.COUNT > 1 THEN

         /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS CANALES */
         FOR idx IN par$cod_canales.first .. par$cod_canales.last LOOP
           IF par$cod_canales(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (par$cod_canales(idx), 'COD_CANAL');
           END IF;
         END LOOP;

         /* CANSULTA DOS CODIGOS Y HACE LAS VALIDACIONES NECESARIAS - POR CODIGO AJENO*/
         IF par$cod_identificador_ajeno IS NOT NULL THEN
             FOR rec$canal IN cur$canales_ajeno LOOP
                IF rec$canal.OID_CANAL IS NULL THEN


                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060020',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                   '2040060020',
                                                   'GENERICO',
                                                   gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                   rec$canal.COD_CANAL || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TCANAL',
                                                   0), 'VALIDACIONES');

                ELSE
                   par$oid_canales(par$oid_canales.COUNT) := rec$canal.OID_CANAL;
                   INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,  COD_CALIFICADOR)
                   VALUES (rec$canal.OID_CANAL,'OID_CANAL' );
                END IF;
             END LOOP rec$canal;
         ELSE
             FOR rec$canal IN cur$canales LOOP
                IF rec$canal.OID_CANAL IS NULL THEN
                   INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060010', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060010',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                rec$canal.COD_CANAL,
                                                0), 'VALIDACIONES');
                ELSE
                   par$oid_canales(par$oid_canales.COUNT) := rec$canal.OID_CANAL;

                   INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,  COD_CALIFICADOR)
                   VALUES (rec$canal.OID_CANAL,'OID_CANAL' );
                END IF;
             END LOOP rec$canal;
         END IF;

      END IF;

      /* SUBCANALES */
      IF par$cod_subcanales IS NOT NULL AND par$cod_subcanales.COUNT > 1 THEN

         /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS SUBCANALES */
         FOR idx IN par$cod_subcanales.first .. par$cod_subcanales.last LOOP
           IF par$cod_subcanales(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (par$cod_subcanales(idx), 'COD_SUBCANAL');
           END IF;
         END LOOP;

         /* CANSULTA DOS CODIGOS Y HACE LAS VALIDACIONES NECESARIAS - POR CODIGO AJENO*/
         IF par$cod_identificador_ajeno IS NOT NULL THEN
             FOR rec$subcanal IN cur$subcanales_ajeno LOOP
                IF rec$subcanal.OID_SUBCANAL IS NULL THEN

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060020', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                 '2040060020',
                                                 'GENERICO',
                                                 gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                 rec$subcanal.COD_SUBCANAL || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TSUBCANAL',
                                                 0), 'VALIDACIONES');


                ELSE
                   par$oid_subcanales(par$oid_subcanales.COUNT) := rec$subcanal.OID_SUBCANAL;
                   INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (rec$subcanal.OID_SUBCANAL, 'OID_SUBCANAL');
                END IF;
             END LOOP rec$canal;
         ELSE
             FOR rec$subcanal IN cur$subcanales LOOP
                IF rec$subcanal.OID_SUBCANAL IS NULL THEN


                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060010',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060010',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                rec$subcanal.COD_SUBCANAL,
                                                0), 'VALIDACIONES');


                ELSE
                   par$oid_subcanales(par$oid_subcanales.COUNT) := rec$subcanal.OID_SUBCANAL;
                   INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (rec$subcanal.OID_SUBCANAL, 'OID_SUBCANAL');
                END IF;
             END LOOP rec$subcanal;
         END IF;
   ELSE
        IF par$cod_identificador_ajeno IS NOT NULL THEN

          INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
           SELECT SUCA.OID_SUBCANAL, 'OID_SUBCANAL'
        FROM
      GEPR_TSUBCANAL SUCA
      INNER JOIN  GEPR_TCODIGO_AJENO CASCA
        ON CASCA.OID_TABLA_GENESIS = SUCA.OID_SUBCANAL AND CASCA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASCA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
      INNER JOIN  GEPR_TCANAL CANA
        ON CANA.OID_CANAL = SUCA.OID_CANAL
      INNER JOIN GEPR_TCODIGO_AJENO CACAN
        ON CACAN.OID_TABLA_GENESIS = CANA.OID_CANAL AND CACAN.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CACAN.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
      INNER JOIN SAPR_GTT_TAUXILIAR AUXM
        ON AUXM.COD_CALIFICADOR = 'OID_CANAL' AND AUXM.OID_CAMPO1 = CANA.OID_CANAL;

         ELSE
          INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
          SELECT SUCA.OID_SUBCANAL, 'OID_SUBCANAL'
        FROM GEPR_TSUBCANAL SUCA
        INNER JOIN GEPR_TCANAL CANA
          ON CANA.OID_CANAL = SUCA.OID_CANAL
        INNER JOIN SAPR_GTT_TAUXILIAR AUXM
          ON AUXM.COD_CALIFICADOR = 'OID_CANAL' AND AUXM.OID_CAMPO1 = CANA.OID_CANAL;


           END IF;
      END IF;

      /* Validar codigo ajeno */
      IF par$cod_identificador_ajeno IS NOT NULL THEN

          /* DELEGACION */
          IF par$cod_delegacion IS NOT NULL THEN
            BEGIN
                  SELECT CA.OID_TABLA_GENESIS
                    INTO par$oid_delegacion
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_delegacion
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
                     AND CA.BOL_ACTIVO = 1;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_delegacion := NULL;

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060020', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                 '2040060020',
                                                 'GENERICO',
                                                 gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                 par$cod_delegacion || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TDELEGACION',
                                                 0), 'VALIDACIONES');

            END;
          END IF;

          /* PLANTA */
          IF par$cod_planta IS NOT NULL THEN
            BEGIN
                  SELECT CA.OID_TABLA_GENESIS
                    INTO par$oid_planta
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_planta
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
                     AND CA.BOL_ACTIVO = 1;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_planta := NULL;

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060020', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     '2040060020',
                                                     'GENERICO',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                     par$cod_planta || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TPLANTA',
                                                     0), 'VALIDACIONES');


            END;
          END IF;

          /* CLIENTE */
          IF par$cod_cliente IS NOT NULL THEN
            BEGIN
                  SELECT CA.OID_TABLA_GENESIS
                    INTO par$oid_cliente
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_cliente
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                     AND CA.BOL_ACTIVO = 1;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_cliente := NULL;


                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060020',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                  '2040060020',
                                                  'GENERICO',
                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                  par$cod_cliente || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TCLIENTE',
                                                  0), 'VALIDACIONES');


            END;
          END IF;

          /* SUB_CLIENTE */
          IF par$cod_sub_cliente IS NOT NULL THEN
            BEGIN
                  SELECT CA.OID_TABLA_GENESIS
                    INTO par$oid_sub_cliente
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_sub_cliente
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                     AND CA.BOL_ACTIVO = 1;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_sub_cliente := NULL;



                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060020',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                 '2040060020',
                                                 'GENERICO',
                                                 gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                 par$cod_sub_cliente || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TSUBCLIENTE',
                                                 0), 'VALIDACIONES');


            END;
          END IF;

          /* PTO_SERVICIO */
          IF par$cod_punto_servicio IS NOT NULL THEN
            BEGIN
                  SELECT CA.OID_TABLA_GENESIS
                    INTO par$oid_punto_servicio
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_punto_servicio
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                     AND CA.BOL_ACTIVO = 1;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_punto_servicio := NULL;


                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060020',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                  '2040060020',
                                                  'GENERICO',
                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                  par$cod_punto_servicio || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TPUNTO_SERVICIO',
                                                  0), 'VALIDACIONES');


            END;
          END IF;

          /* DIVISA */
          IF par$cod_divisa IS NOT NULL THEN
            BEGIN
                  SELECT CA.OID_TABLA_GENESIS
                    INTO par$oid_divisa
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_divisa
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'
                     AND CA.BOL_ACTIVO = 1;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_divisa := NULL;

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060020',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                              '2040060020',
                                              'GENERICO',
                                              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                              par$cod_divisa || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TDIVISA',
                                              0), 'VALIDACIONES');


            END;
          END IF;


      ELSE

          /* DELEGACION */
          IF par$cod_delegacion IS NOT NULL THEN
            BEGIN
                  SELECT DELE.OID_DELEGACION
                    INTO par$oid_delegacion
                    FROM GEPR_TDELEGACION DELE
                   WHERE DELE.COD_DELEGACION = par$cod_delegacion;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_delegacion := NULL;

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060012',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060012',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_delegacion,
                                                0), 'VALIDACIONES');
            END;
          END IF;

          /* PLANTA */
          IF par$cod_planta IS NOT NULL THEN
            BEGIN
                  SELECT PLANT.OID_PLANTA
                    INTO par$oid_planta
                    FROM GEPR_TPLANTA PLANT
                   WHERE PLANT.COD_PLANTA = par$cod_planta;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_planta := NULL;

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060013',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060013',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_planta,
                                                0), 'VALIDACIONES');

            END;
          END IF;

          /* CLIENTE */
          IF par$cod_cliente IS NOT NULL THEN
            BEGIN
                  SELECT CLIEN.OID_CLIENTE
                    INTO par$oid_cliente
                    FROM GEPR_TCLIENTE CLIEN
                   WHERE CLIEN.COD_CLIENTE = par$cod_cliente;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_cliente := NULL;


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060014',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060014',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_cliente,
                                                0), 'VALIDACIONES');


            END;
          END IF;

          /* SUB_CLIENTE */
          IF par$cod_sub_cliente IS NOT NULL THEN
            BEGIN
                  SELECT SCLI.OID_SUBCLIENTE
                    INTO par$oid_sub_cliente
                    FROM GEPR_TSUBCLIENTE SCLI
                   WHERE SCLI.COD_SUBCLIENTE = par$cod_sub_cliente;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_sub_cliente := NULL;


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060015',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060015',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_sub_cliente,
                                                0), 'VALIDACIONES');

            END;
          END IF;

          /* PTO_SERVICIO */
          IF par$cod_punto_servicio IS NOT NULL THEN
            BEGIN
                  SELECT PTOS.OID_PTO_SERVICIO
                    INTO par$oid_punto_servicio
                    FROM GEPR_TPUNTO_SERVICIO PTOS
                   WHERE PTOS.COD_PTO_SERVICIO = par$cod_punto_servicio;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_punto_servicio := NULL;



                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060016',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060016',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_punto_servicio,
                                                0), 'VALIDACIONES');


            END;
          END IF;

          /* DIVISA */
          IF par$cod_divisa IS NOT NULL THEN
            BEGIN
                  SELECT DIV.OID_DIVISA
                    INTO par$oid_divisa
                    FROM GEPR_TDIVISA DIV
                   WHERE DIV.COD_ISO_DIVISA = par$cod_divisa;
              par$selects := par$selects + sql%ROWCOUNT;

            EXCEPTION
              WHEN no_data_found THEN
                par$oid_divisa := NULL;


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060011',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060011',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_divisa,
                                                0), 'VALIDACIONES');


            END;
          END IF;


      END IF;

      /* Validaciones de integridad */
      /* si fue informado codigo de planta, debe ser informado codigo delegacion */
      IF par$oid_planta IS NOT NULL AND par$oid_delegacion IS NULL THEN


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060017',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                       '2040060017',
                                                       'GENERICO',
                                                       gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                       NULL,
                                                       0), 'VALIDACIONES');


      END IF;
      /* si fue informado codigo de planta y delegacion, debe validar integridad */
      IF par$oid_planta IS NOT NULL AND par$oid_delegacion IS NOT NULL THEN
          BEGIN
                SELECT P.COD_PLANTA
                  INTO var$existe
                  FROM GEPR_TPLANTA P
                 WHERE P.OID_PLANTA = par$oid_planta
                   AND P.OID_DELEGACION = par$oid_delegacion;
            par$selects := par$selects + sql%ROWCOUNT;

          EXCEPTION
            WHEN no_data_found THEN
              var$existe := NULL;


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060017',  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                               '2040060017',
                                               'GENERICO',
                                               gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                               NULL,
                                               0), 'VALIDACIONES');


          END;
      END IF;

      /* si fue informado codigo de subcliente, debe ser informado codigo cliente */
      IF par$oid_sub_cliente IS NOT NULL AND par$oid_cliente IS NULL THEN


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060018', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060018',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                NULL,
                                                0), 'VALIDACIONES');

      END IF;
      /* si fue informado codigo de subcliente y cliente, debe validar integridad */
      IF par$oid_sub_cliente IS NOT NULL AND par$oid_cliente IS NOT NULL THEN
          BEGIN
                SELECT SC.COD_SUBCLIENTE
                  INTO var$existe
                  FROM GEPR_TSUBCLIENTE SC
                 WHERE SC.OID_CLIENTE = par$oid_cliente
                   AND SC.OID_SUBCLIENTE = par$oid_sub_cliente;
            par$selects := par$selects + sql%ROWCOUNT;

          EXCEPTION
            WHEN no_data_found THEN
              var$existe := NULL;


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060018', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                 '2040060018',
                                                 'GENERICO',
                                                 gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                 NULL,
                                                 0), 'VALIDACIONES');

          END;
      END IF;

      /* si fue informado codigo de punto servicio, debe ser informado codigo subcliente */
      IF par$oid_punto_servicio IS NOT NULL AND (par$oid_sub_cliente IS NULL OR par$oid_cliente IS NULL) THEN


        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          VALUES ('2040060019', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                        '2040060019',
                                        'GENERICO',
                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                        NULL,
                                        0), 'VALIDACIONES');


      END IF;
      /* si fue informado codigo de punto servicio y subcliente, debe validar integridad */
      IF par$oid_punto_servicio IS NOT NULL AND par$oid_sub_cliente IS NOT NULL AND par$oid_cliente IS NOT NULL THEN
          BEGIN
                SELECT PS.COD_PTO_SERVICIO
                  INTO var$existe
                  FROM GEPR_TPUNTO_SERVICIO PS
            INNER JOIN GEPR_TSUBCLIENTE SC
                    ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
                 WHERE SC.OID_CLIENTE = par$oid_cliente
                   AND SC.OID_SUBCLIENTE = par$oid_sub_cliente
                   AND PS.OID_PTO_SERVICIO = par$oid_punto_servicio;
            par$selects := par$selects + sql%ROWCOUNT;

          EXCEPTION
            WHEN no_data_found THEN


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060019', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                 '2040060019',
                                                 'GENERICO',
                                                 gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                 NULL,
                                                 0), 'VALIDACIONES');


          END;
      END IF;



   END svalidar_codigos;



  /*Valida los IN al procedure de recuperar saldos periodos*/
 PROCEDURE svalidar_saldos_periodos(
                                    /*Identificador Log*/
                                    par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    /*Filtros entrada*/
                                    par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_pais                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_banco                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_plan                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_periodo               IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                    par$acod_device_id            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$acod_est_periodo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$nel_gmt_minuto            IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    /*Patron*/
                                    par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_
                                    )
  IS
    var$obs_detalle             gepr_pcomon_###VERSION###.tipo$obs_;
    var$oid_pais            gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_pais            gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_maquina         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_estado_periodo  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_plan            gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_banco           gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_identificacion  gepr_pcomon_###VERSION###.tipo$cod_;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_saldos_periodos';
    var$fyh_periodo_gmt0    gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fyh_periodo         gepr_pcomon_###VERSION###.tipo$fyh_;
    var$consulta            gepr_pcomon_###VERSION###.tipo$obs_;

    CURSOR cur$maquinas IS
      SELECT OID_CAMPO1 OID_MAQUINA, COD_CAMPO2 COD_IDENTIFICACION
      FROM SAPR_GTT_TAUXILIAR
      WHERE COD_CALIFICADOR = 'OID_MAQUINA';

  BEGIN
    /* Solo errores por:
    - País nulo o no valido
    - Al menos debe informar un código de estado periodo (y que sea válido)
    - La fecha de periodo no debe ser nula o 01/01/0001 */
    /* Link de Confluence: https://procde.prosegur.com/confluence/pages/viewpage.action?pageId=203692208 */

    /* Grabar logs de entrada */
      var$obs_detalle := '';

    IF par$oid_llamada IS NOT NULL THEN
        var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_identificador_ajeno: ' || const$comilla_simple || par$cod_identificador_ajeno || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_pais: ' || const$comilla_simple || par$cod_pais || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_banco: ' || const$comilla_simple || par$cod_banco || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_plan: ' || const$comilla_simple || par$cod_plan || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$fyh_periodo: ' || const$comilla_simple || par$fyh_periodo || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$nel_gmt_minuto: ' || const$comilla_simple || par$nel_gmt_minuto || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_cultura: ' || const$comilla_simple || par$cod_cultura || const$comilla_simple || const$new_line;

        FOR idx IN par$acod_device_id.first .. par$acod_device_id.last LOOP
            var$obs_detalle := var$obs_detalle || 'par$acod_device_id('|| idx ||'): ' || par$acod_device_id(idx) || const$new_line;
        END LOOP;

        FOR idx IN par$acod_est_periodo.first .. par$acod_est_periodo.last LOOP
            var$obs_detalle := var$obs_detalle || 'par$acod_est_periodo('|| idx ||'): ' || par$acod_est_periodo(idx) || const$new_line;
        END LOOP;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$obs_detalle,
                par$cod_identificador   => '');
    END IF;

    /* Validar código de país */
    /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
    var$cod_pais := par$cod_pais;
    GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                            par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                            par$cod_pais => var$cod_pais,
                            par$oid_pais => var$oid_pais,
                            par$cod_cultura => par$cod_cultura);

    IF var$oid_pais IS NOT NULL THEN
        INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
        VALUES (var$oid_pais, 'OID_PAIS');
    END IF;


    /*Válido fecha periodo*/
    IF par$fyh_periodo IS NULL OR par$fyh_periodo = to_date('01/01/0001 12:00:00','DD/MM/YYYY HH24:mi:ss') THEN
      INSERT INTO SAPR_GTT_TAUXILIAR  (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
      VALUES
      (
        '2040250006',
        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040250006', 'RECUPERARSALDOSPERIODOS', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
        'VALIDACIONES'
      );
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                      par$des_version     => const$version,
                                      par$des_detalle     => gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040250006', 'RECUPERARSALDOSPERIODOS', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                                      par$cod_identificador   => '');
    END IF;

    /*Validacón de códigos de estados periodos*/
    IF par$acod_est_periodo IS NOT NULL AND par$acod_est_periodo.count > 0 THEN
      FOR idx IN par$acod_est_periodo.first .. par$acod_est_periodo.last LOOP
        IF par$acod_est_periodo(idx) IS NOT NULL THEN
          BEGIN
            SELECT OID_ESTADO_PERIODO
              INTO  var$oid_estado_periodo
              FROM  SAPR_TESTADO_PERIODO
              WHERE
                cod_estado_periodo = par$acod_est_periodo(idx);
          EXCEPTION
            WHEN no_data_found THEN
              var$oid_estado_periodo := NULL;

          END;
          IF var$oid_estado_periodo IS NOT NULL THEN
            INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
            VALUES (var$oid_estado_periodo, 'OID_ESTADO_PERIODO');
          ELSE
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES
            (
              '2040250007',
              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040250007', 'RECUPERARSALDOSPERIODOS', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$acod_est_periodo(idx), 0),
              'VALIDACIONES'
            );
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                      par$des_version     => const$version,
                                      par$des_detalle     => gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040250007', 'RECUPERARSALDOSPERIODOS', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                                      par$cod_identificador   => '');
          END IF;
        END IF;
      END LOOP;
    ELSE
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
      VALUES
      (
        '2040250005',
        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040250005', 'RECUPERARSALDOSPERIODOS', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
        'VALIDACIONES'
      );
    END IF;

    /*Valido el código de banco*/
    IF par$cod_banco IS NOT NULL THEN
      IF par$cod_identificador_ajeno IS NULL THEN
        BEGIN
          SELECT OID_CLIENTE
            INTO var$oid_banco
            FROM GEPR_TCLIENTE
          WHERE COD_CLIENTE = par$cod_banco;
        EXCEPTION
          WHEN no_data_found THEN
            var$oid_banco := NULL;
        END;
      ELSE
        BEGIN
          SELECT CLI.OID_CLIENTE
            INTO var$oid_banco
            FROM GEPR_TCLIENTE CLI
            INNER JOIN GEPR_TCODIGO_AJENO COCLI ON COCLI.OID_TABLA_GENESIS = CLI.OID_CLIENTE AND COCLI.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE' AND COCLI.COD_IDENTIFICADOR = par$cod_identificador_ajeno
          WHERE COCLI.COD_AJENO = par$cod_banco;
        EXCEPTION
          WHEN no_data_found THEN
            var$oid_banco := NULL;
        END;
      END IF;
      IF var$oid_banco IS NOT NULL THEN
        INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
        VALUES (var$oid_banco, 'OID_BANCO');
      END IF;
    END IF;

    /*Valido el código de planificacion*/
    IF par$cod_plan IS NOT NULL THEN
      BEGIN
        SELECT OID_PLANIFICACION
          INTO var$oid_plan
          FROM SAPR_TPLANIFICACION
        WHERE COD_PLANIFICACION = par$cod_plan;
      EXCEPTION
        WHEN no_data_found THEN
          var$oid_plan := NULL;
      END;
      IF var$oid_plan IS NOT NULL THEN
        INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
        VALUES (var$oid_plan, 'OID_PLANIFICACION');
      END IF;
    END IF;


    /*Validación de maquinas*/
    IF par$acod_device_id IS NOT NULL AND par$acod_device_id.count > 0 THEN
      FOR idx IN par$acod_device_id.first .. par$acod_device_id.last LOOP
        IF par$acod_device_id(idx) IS NOT NULL THEN
          IF par$cod_identificador_ajeno IS NULL THEN
            BEGIN
              SELECT OID_MAQUINA, COD_IDENTIFICACION
              INTO var$oid_maquina, var$cod_identificacion
              FROM SAPR_TMAQUINA
              WHERE COD_IDENTIFICACION = par$acod_device_id(idx);
            EXCEPTION
              WHEN no_data_found THEN
                var$oid_maquina := NULL;
            END;
          ELSE
            BEGIN
              SELECT MAQ.OID_MAQUINA, MAQ.COD_IDENTIFICACION
              INTO var$oid_maquina, var$cod_identificacion
              FROM SAPR_TMAQUINA MAQ
              INNER JOIN GEPR_TCODIGO_AJENO CAMAQ ON MAQ.OID_SECTOR = CAMAQ.OID_TABLA_GENESIS AND CAMAQ.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR' AND CAMAQ.COD_IDENTIFICADOR = par$cod_identificador_ajeno
              WHERE CAMAQ.COD_AJENO = par$acod_device_id(idx);
            EXCEPTION
              WHEN no_data_found THEN
                var$oid_maquina := NULL;
            END;
          END IF;

          IF var$oid_maquina IS NOT NULL THEN
            INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES (var$oid_maquina, var$cod_identificacion, 'OID_MAQUINA');
          END IF;
        END IF;
      END LOOP;
    ELSE
      /*Recupero todas las maquinas*/
      var$consulta := q'[
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
        SELECT
          MAQ.OID_MAQUINA, MAQ.COD_IDENTIFICACION, 'OID_MAQUINA'
          FROM GEPR_TDELEGACION DELEG
            INNER JOIN GEPR_TPLANTA PLAN ON PLAN.OID_DELEGACION = DELEG.OID_DELEGACION
            INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_PLANTA = PLAN.OID_PLANTA
            INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_SECTOR = SECT.OID_SECTOR
            INNER JOIN SAPR_TPLANXMAQUINA PLANXMAQ ON PLANXMAQ.OID_MAQUINA = MAQ.OID_MAQUINA
            INNER JOIN SAPR_TPLANIFICACION PLANIF ON PLANIF.OID_PLANIFICACION = PLANXMAQ.OID_PLANIFICACION
            INNER JOIN SAPR_GTT_TAUXILIAR AUX_PAIS ON AUX_PAIS.OID_CAMPO1 = DELEG.OID_PAIS AND AUX_PAIS.COD_CALIFICADOR = 'OID_PAIS'
            ]';

      IF var$oid_banco IS NOT NULL THEN
        var$consulta := var$consulta || q'[
          INNER JOIN SAPR_GTT_TAUXILIAR AUX_BANCO ON AUX_BANCO.OID_CAMPO1 = PLANIF.OID_CLIENTE AND AUX_BANCO.COD_CALIFICADOR = 'OID_BANCO'
        ]';
      END IF;

      IF var$oid_plan IS NOT NULL THEN
        var$consulta := var$consulta || q'[
            INNER JOIN SAPR_GTT_TAUXILIAR AUX_PLANIF ON AUX_PLANIF.OID_CAMPO1 = PLANIF.OID_PLANIFICACION AND AUX_PLANIF.COD_CALIFICADOR = 'OID_PLANIFICACION'
          ]';
      END IF;

      EXECUTE IMMEDIATE var$consulta;


    END IF;
         dbms_output.put_line('cur$maquinas: ');

      FOR rec$maquina IN cur$maquinas LOOP

          dbms_output.put_line('rec$maquina.Cod_Identificacion: ' || rec$maquina.Cod_Identificacion);

        var$fyh_periodo := par$fyh_periodo;
        svalidar_fecha(par$nel_index        => 0,
                                            par$oid_sector_mae   => rec$maquina.COD_IDENTIFICACION,
                                            par$nel_gmt_minuto   => par$nel_gmt_minuto,
                                            par$fyh_periodo      => var$fyh_periodo,
                                            par$fyh_periodo_gmt0 => var$fyh_periodo_gmt0,
                                            par$cod_cultura      => par$cod_cultura);

          dbms_output.put_line('var$fyh_periodo_gmt0: ' || var$fyh_periodo_gmt0);

        UPDATE SAPR_GTT_TAUXILIAR
           SET COD_CAMPO3 = TO_CHAR(var$fyh_periodo_gmt0,'DD/MM/YYYY HH24:MI:SS')
        WHERE COD_CALIFICADOR = 'OID_MAQUINA' AND OID_CAMPO1 = rec$maquina.OID_MAQUINA;

      END LOOP rec$maquina;
  END svalidar_saldos_periodos;

 /* Recuperar valores saldos */
  PROCEDURE srecuperar_saldos_periodos(
                                        /*Identificador Log*/
                                        par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        /*Filtros entrada*/
                                        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_pais IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_banco IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_plan IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$fyh_periodo            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$acod_device_id          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$acod_est_periodo        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$nel_gmt_minuto         IN gepr_pcomon_###VERSION###.tipo$nel_,
                                        /*Cursores respuesta*/
                                        par$rc_plan           OUT sys_refcursor,
                                        par$rc_periodos           OUT sys_refcursor,
                                        par$rc_saldos           OUT sys_refcursor,
                                        par$rc_saldos_det           OUT sys_refcursor,
                                        par$rc_validaciones           OUT sys_refcursor,
                                        /*Patron*/
                                        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_)
  IS
    var$existe  gepr_pcomon_###VERSION###.tipo$nel_;
    var$consulta           gepr_pcomon_###VERSION###.tipo$obs_;
    var$obs_detalle             gepr_pcomon_###VERSION###.tipo$obs_;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_saldos_periodos';
  BEGIN

    /* #### Inicializar los cursores #### */
    OPEN par$rc_plan FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_periodos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_saldos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_saldos_det FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* #### Limpiar tabla auxiliar #### */
    DELETE FROM SAPR_GTT_TAUXILIAR;
    COMMIT;

    dbms_output.put_line('REC_saldos_per - A');

    /* Grabar logs de entrada */
    var$obs_detalle := '';

    IF par$oid_llamada IS NOT NULL THEN
        var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_identificador_ajeno: ' || const$comilla_simple || par$cod_identificador_ajeno || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_pais: ' || const$comilla_simple || par$cod_pais || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_banco: ' || const$comilla_simple || par$cod_banco || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_plan: ' || const$comilla_simple || par$cod_plan || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$fyh_periodo: ' || const$comilla_simple || par$fyh_periodo || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$nel_gmt_minuto: ' || const$comilla_simple || par$nel_gmt_minuto || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_cultura: ' || const$comilla_simple || par$cod_cultura || const$comilla_simple || const$new_line;

        FOR idx IN par$acod_device_id.first .. par$acod_device_id.last LOOP
            var$obs_detalle := var$obs_detalle || 'par$acod_device_id('|| idx ||'): ' || par$acod_device_id(idx) || const$new_line;
        END LOOP;

        FOR idx IN par$acod_est_periodo.first .. par$acod_est_periodo.last LOOP
            var$obs_detalle := var$obs_detalle || 'par$acod_est_periodo('|| idx ||'): ' || par$acod_est_periodo(idx) || const$new_line;
        END LOOP;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => var$obs_detalle,
                    par$cod_identificador   => '');
    END IF;

    svalidar_saldos_periodos
    (
      par$oid_llamada => par$oid_llamada,
      par$cod_identificador_ajeno => par$cod_identificador_ajeno,
      par$cod_pais => par$cod_pais,
      par$cod_banco => par$cod_banco,
      par$cod_plan => par$cod_plan,
      par$fyh_periodo => par$fyh_periodo,
      par$acod_device_id => par$acod_device_id,
      par$acod_est_periodo => par$acod_est_periodo,
      par$cod_usuario => par$cod_usuario,
      par$cod_cultura => par$cod_cultura,
      par$nel_gmt_minuto => par$nel_gmt_minuto
    );

    dbms_output.put_line('REC_saldos_per - B');


    IF NOT fhay_error_rec_sal_per THEN

      dbms_output.put_line('REC_saldos_per - C');

      var$consulta := q'[
        INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1,COD_CAMPO2, COD_CALIFICADOR)
        SELECT DISTINCT PER.OID_PERIODO, PER.OID_PLANIFICACION, 'OID_PERIODO'
        FROM SAPR_TPERIODO PER
        INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
        INNER JOIN SAPR_GTT_TAUXILIAR AUXM ON AUXM.OID_CAMPO1 = PER.OID_MAQUINA AND AUXM.COD_CALIFICADOR = 'OID_MAQUINA'
                                          AND TO_DATE(AUXM.COD_CAMPO3, 'DD/MM/YYYY HH24:MI:SS') BETWEEN PER.FYH_INICIO AND PER.FYH_FIN
        INNER JOIN SAPR_GTT_TAUXILIAR AUXEP ON AUXEP.OID_CAMPO1 = PER.OID_ESTADO_PERIODO AND AUXEP.COD_CALIFICADOR = 'OID_ESTADO_PERIODO'
        INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_MAQUINA = PER.OID_MAQUINA
        INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = MAQ.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLAN ON PLAN.OID_PLANTA = SEC.OID_PLANTA
        INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
        INNER JOIN SAPR_GTT_TAUXILIAR AUX_PA ON AUX_PA.OID_CAMPO1 = DELE.OID_PAIS AND AUX_PA.COD_CALIFICADOR = 'OID_PAIS'
        INNER JOIN SAPR_TPLANIFICACION PLA ON PLA.OID_PLANIFICACION = PER.OID_PLANIFICACION
         ]';

        IF par$cod_plan IS NOT NULL THEN
          var$consulta := var$consulta  || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUXPLAN ON AUXPLAN.OID_CAMPO1 = PLA.OID_PLANIFICACION AND AUXPLAN.COD_CALIFICADOR = 'OID_PLANIFICACION' ]';
        END IF;

        IF par$cod_banco IS NOT NULL THEN
          var$consulta := var$consulta  || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUXCLI ON AUXCLI.OID_CAMPO1 = PLA.OID_CLIENTE AND AUXCLI.COD_CALIFICADOR = 'OID_BANCO' ]';
        END IF;


        EXECUTE IMMEDIATE var$consulta;


        OPEN par$rc_plan FOR
           SELECT
            CASE WHEN par$cod_identificador_ajeno IS NULL THEN MAQUINA.COD_IDENTIFICACION ELSE CAMAQUINA.COD_AJENO END AS DEVICEID,
            PLAN.COD_PLANIFICACION AS COD_PLAN,
            PLAN.DES_PLANIFICACION AS DES_PLAN,
            CASE WHEN par$cod_identificador_ajeno IS NULL THEN BANCO.COD_CLIENTE ELSE CABANCO.COD_AJENO END AS COD_BANCO,
            CASE WHEN par$cod_identificador_ajeno IS NULL THEN BANCO.DES_CLIENTE ELSE CABANCO.DES_AJENO END AS DES_BANCO,
            TIPO_PLAN.COD_TIPO_PLANIFICACION AS COD_TIPO
          FROM SAPR_TPLANIFICACION PLAN
          INNER JOIN SAPR_TPLANXMAQUINA PLANXMAQ ON PLANXMAQ.OID_PLANIFICACION = PLAN.OID_PLANIFICACION
          INNER JOIN SAPR_TTIPO_PLANIFICACION TIPO_PLAN ON TIPO_PLAN.OID_TIPO_PLANIFICACION = PLAN.OID_TIPO_PLANIFICACION
          INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'OID_PERIODO' AND AUX.COD_CAMPO2 = PLAN.OID_PLANIFICACION
          INNER JOIN GEPR_TCLIENTE BANCO ON BANCO.OID_CLIENTE = PLAN.OID_CLIENTE
          INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = AUX.OID_CAMPO1
          INNER JOIN SAPR_TMAQUINA MAQUINA ON MAQUINA.OID_MAQUINA = PERI.OID_MAQUINA
          LEFT JOIN GEPR_TCODIGO_AJENO CAMAQUINA ON CAMAQUINA.OID_TABLA_GENESIS = MAQUINA.OID_SECTOR
                       AND CAMAQUINA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAMAQUINA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
          LEFT JOIN GEPR_TCODIGO_AJENO CABANCO ON CABANCO.OID_TABLA_GENESIS = BANCO.OID_CLIENTE
                       AND CABANCO.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CABANCO.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
          GROUP BY
            CASE WHEN par$cod_identificador_ajeno IS NULL THEN MAQUINA.COD_IDENTIFICACION ELSE CAMAQUINA.COD_AJENO END,
            PLAN.COD_PLANIFICACION,
            PLAN.DES_PLANIFICACION,
            CASE WHEN par$cod_identificador_ajeno IS NULL THEN BANCO.COD_CLIENTE ELSE CABANCO.COD_AJENO END,
            CASE WHEN par$cod_identificador_ajeno IS NULL THEN BANCO.DES_CLIENTE ELSE CABANCO.DES_AJENO END,
            TIPO_PLAN.COD_TIPO_PLANIFICACION;


        OPEN par$rc_periodos FOR
          SELECT DISTINCT
            CASE WHEN par$cod_identificador_ajeno IS NULL THEN MAQUINA.COD_IDENTIFICACION ELSE CAMAQUINA.COD_AJENO END AS DEVICEID,
            PLAN.COD_PLANIFICACION AS COD_PLAN,
            PERIODO.OID_PERIODO AS IDENTIFICADOR,
            ESTP.COD_ESTADO_PERIODO AS COD_ESTADO,
            PERIODO.FYH_INICIO AS FYH_INICIO,
            PERIODO.FYH_FIN AS FYH_FIN,
            CASE WHEN PERIODO.OID_ACREDITACION IS NULL THEN 0 ELSE 1 END AS BOL_ACREDITADO
          FROM SAPR_TPERIODO PERIODO
          INNER JOIN SAPR_GTT_TAUXILIAR AUXP ON AUXP.COD_CALIFICADOR = 'OID_PERIODO' AND AUXP.OID_CAMPO1 = PERIODO.OID_PERIODO
          INNER JOIN SAPR_TPLANIFICACION PLAN ON PERIODO.OID_PLANIFICACION = PLAN.OID_PLANIFICACION
          INNER JOIN SAPR_TESTADO_PERIODO ESTP ON ESTP.OID_ESTADO_PERIODO = PERIODO.OID_ESTADO_PERIODO
          INNER JOIN SAPR_TMAQUINA MAQUINA ON MAQUINA.OID_MAQUINA = PERIODO.OID_MAQUINA
          LEFT JOIN GEPR_TCODIGO_AJENO CAMAQUINA ON CAMAQUINA.OID_TABLA_GENESIS = MAQUINA.OID_SECTOR
                       AND CAMAQUINA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAMAQUINA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR';




           OPEN par$rc_saldos FOR
            SELECT DISTINCT
              PERIODO.OID_CAMPO1 AS IDENTIFICADOR,
              CASE WHEN par$cod_identificador_ajeno IS NULL THEN DIVISA.COD_ISO_DIVISA ELSE CADIVISA.COD_AJENO END AS COD_DIVISA,
              SUM(SALDO.NUM_IMPORTE) AS IMPORTE
            FROM SAPR_TCALCULO_EFECTIVO SALDO
            INNER JOIN SAPR_GTT_TAUXILIAR PERIODO ON PERIODO.OID_CAMPO1 = SALDO.OID_PERIODO AND PERIODO.COD_CALIFICADOR = 'OID_PERIODO'
            INNER JOIN GEPR_TDIVISA DIVISA ON DIVISA.OID_DIVISA = SALDO.OID_DIVISA
            LEFT JOIN GEPR_TCODIGO_AJENO CADIVISA ON CADIVISA.OID_TABLA_GENESIS = DIVISA.OID_DIVISA
                       AND CADIVISA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CADIVISA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'
            GROUP BY PERIODO.OID_CAMPO1, DIVISA.COD_ISO_DIVISA, CADIVISA.COD_AJENO;




            OPEN par$rc_saldos_det FOR
              SELECT DISTINCT
                PERIODO.OID_CAMPO1 AS IDENTIFICADOR,
                CASE WHEN par$cod_identificador_ajeno IS NULL THEN DIVISA.COD_ISO_DIVISA ELSE CADIVISA.COD_AJENO END AS COD_DIVISA,
                CASE WHEN par$cod_identificador_ajeno IS NULL THEN DENOM.COD_DENOMINACION ELSE CADENOM.COD_AJENO END AS COD_DENOMINACION,
                CASE WHEN par$cod_identificador_ajeno IS NULL THEN PTOSERV.COD_PTO_SERVICIO ELSE CAPTOSER.COD_AJENO END AS COD_PTO_SERVICIO,
                CASE WHEN par$cod_identificador_ajeno IS NULL THEN SUBCANAL.COD_SUBCANAL ELSE CASUBCANAL.COD_AJENO END AS COD_SUBCANAL,
                SALDO.FEC_CONTABLE AS FYH_CONTABLE,
                SUM(SALDO.NUM_IMPORTE) AS IMPORTE,
                SUM(SALDO.NEL_CANTIDAD) AS CANTIDAD
              FROM SAPR_TCALCULO_EFECTIVO SALDO
              INNER JOIN SAPR_GTT_TAUXILIAR PERIODO ON PERIODO.OID_CAMPO1 = SALDO.OID_PERIODO AND PERIODO.COD_CALIFICADOR = 'OID_PERIODO'
              INNER JOIN GEPR_TDIVISA DIVISA ON DIVISA.OID_DIVISA = SALDO.OID_DIVISA
              LEFT JOIN GEPR_TCODIGO_AJENO CADIVISA ON CADIVISA.OID_TABLA_GENESIS = DIVISA.OID_DIVISA AND CADIVISA.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CADIVISA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'
              LEFT JOIN GEPR_TDENOMINACION DENOM ON DENOM.OID_DENOMINACION = SALDO.OID_DENOMINACION
              LEFT JOIN GEPR_TCODIGO_AJENO CADENOM ON CADENOM.OID_TABLA_GENESIS = DENOM.OID_DENOMINACION AND CADENOM.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CADENOM.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION'
              LEFT JOIN GEPR_TPUNTO_SERVICIO PTOSERV ON PTOSERV.OID_PTO_SERVICIO = SALDO.OID_PTO_SERVICIO
              LEFT JOIN GEPR_TCODIGO_AJENO CAPTOSER ON CAPTOSER.OID_TABLA_GENESIS = PTOSERV.OID_PTO_SERVICIO AND CAPTOSER.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAPTOSER.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
              LEFT JOIN GEPR_TSUBCANAL SUBCANAL ON SUBCANAL.OID_SUBCANAL = SALDO.OID_SUBCANAL
              LEFT JOIN GEPR_TCODIGO_AJENO CASUBCANAL ON CASUBCANAL.OID_TABLA_GENESIS = SUBCANAL.OID_SUBCANAL AND CASUBCANAL.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASUBCANAL.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
              GROUP BY
                PERIODO.OID_CAMPO1,
                DIVISA.COD_ISO_DIVISA,
                CADIVISA.COD_AJENO,
                DENOM.COD_DENOMINACION,
                CADENOM.COD_AJENO,
                PTOSERV.COD_PTO_SERVICIO,
                CAPTOSER.COD_AJENO,
                SUBCANAL.COD_SUBCANAL,
                CASUBCANAL.COD_AJENO,
                SALDO.FEC_CONTABLE
              ;

          BEGIN
            SELECT COUNT(1)
              INTO var$existe
            FROM SAPR_GTT_TAUXILIAR
             WHERE COD_CALIFICADOR = 'OID_PERIODO';
          END;
          IF var$existe = 0 THEN
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES
            (
              '2040250008',
              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, '2040250008', 'RECUPERARSALDOSPERIODOS', gepr_pcomon_###VERSION###.const$codAplicacionGenesis, '', 0),
              'VALIDACIONES'
            );
          END IF;
        
    
    
    ELSE
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Encontró errores por la función fhay_error_rec_sal_per',
              par$cod_identificador   => ''
          );
    
    END IF;

      OPEN par$rc_validaciones FOR
          SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION
            FROM SAPR_GTT_TAUXILIAR
          WHERE COD_CALIFICADOR = 'VALIDACIONES';

  END srecuperar_saldos_periodos;


  /* Validar Fecha */
  PROCEDURE svalidar_fecha(par$nel_index               IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$oid_sector_mae          IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$nel_gmt_minuto          IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$fyh_periodo             IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$fyh_periodo_gmt0        OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    var$oid_delegacion gepr_pcomon_###VERSION###.tipo$oid_;
    var$nel_gmt_minuto gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN

      /* Delegacion */
      BEGIN
              SELECT PLAN.OID_DELEGACION
              INTO var$oid_delegacion
              FROM GEPR_TSECTOR SECT
        INNER JOIN GEPR_TPLANTA PLAN ON PLAN.OID_PLANTA = SECT.OID_PLANTA
        INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_SECTOR = SECT.OID_SECTOR
             WHERE MAQ.COD_IDENTIFICACION = par$oid_sector_mae;

      EXCEPTION WHEN no_data_found THEN
        var$oid_delegacion := NULL;
      END;
      DBMS_OUTPUT.PUT_LINE('var$oid_delegacion: ' || var$oid_delegacion);
      var$nel_gmt_minuto := NULL;
      /* Calcular la fecha GMT */
      gepr_putilidades_###VERSION###.scalcular_gmt(par$fyh_gestion    => par$fyh_periodo,
                                          par$oid_delegacion => var$oid_delegacion,
                                          par$nel_gmt_minuto => var$nel_gmt_minuto,
                                          par$cod_cultura    => par$cod_cultura);

      DBMS_OUTPUT.PUT_LINE('var$nel_gmt_minuto: ' || var$nel_gmt_minuto);
      IF (par$nel_gmt_minuto IS NULL) THEN
        par$fyh_periodo_gmt0 := gepr_putilidades_###VERSION###.fsomar_fecha(par$fyh_periodo, var$nel_gmt_minuto * -1);
        DBMS_OUTPUT.PUT_LINE('par$fyh_periodo_gmt0: ' || TO_CHAR(par$fyh_periodo_gmt0,'DD/MM/YYYY HH24:MI:SS'));

      ELSE
        par$fyh_periodo_gmt0 := gepr_putilidades_###VERSION###.fsomar_fecha(par$fyh_periodo, par$nel_gmt_minuto * -1);
        par$fyh_periodo := gepr_putilidades_###VERSION###.fsomar_fecha(par$fyh_periodo_gmt0, var$nel_gmt_minuto);

        DBMS_OUTPUT.PUT_LINE('par$fyh_periodo: ' || TO_CHAR(par$fyh_periodo,'DD/MM/YYYY HH24:MI:SS'));
        DBMS_OUTPUT.PUT_LINE('par$fyh_periodo_gmt0: ' || TO_CHAR(par$fyh_periodo_gmt0,'DD/MM/YYYY HH24:MI:SS'));

      END IF;

  END svalidar_fecha;


  /*Función que corrobora si hay error para el procedure de recuperar saldos periodos*/
  FUNCTION fhay_error_rec_sal_per RETURN BOOLEAN IS
    var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    BEGIN
      BEGIN
        SELECT COUNT(1)
          INTO var$existe
          FROM SAPR_GTT_TAUXILIAR A
          WHERE A.COD_CALIFICADOR = 'VALIDACIONES';
      EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
      END;
      dbms_output.put_line('Valor var$existe: ' || var$existe);
      RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);
  END fhay_error_rec_sal_per;

    /* Recupera los saldos de los acuerdos servicios */
  PROCEDURE srecuperar_saldos_acuerdos(
      /*Identificador Log*/
      par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
      /*Filtros entrada*/
      par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_fecha_saldos              IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$des_sourcerefenceid     IN gepr_pcomon_###VERSION###.tipo$desc_,
      /*Patron*/
      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
      /*Cursores respuesta*/
      par$rc_puntos               OUT sys_refcursor,
      par$rc_movimientos					OUT sys_refcursor,
      par$rc_validaciones         OUT sys_refcursor
  ) IS
      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_saldos_acuerdos';
      var$obs_detalle             gepr_pcomon_###VERSION###.tipo$obs_;
  BEGIN
      /* #### Inicializar los cursores #### */
      OPEN par$rc_puntos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_movimientos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

      /* #### Limpiar tabla auxiliar #### */
      DELETE FROM SAPR_GTT_TAUXILIAR;
      COMMIT;

      /* Grabar logs de entrada */
      var$obs_detalle := '';

      IF par$oid_llamada IS NOT NULL THEN
          var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_identificador_ajeno: ' || const$comilla_simple || par$cod_identificador_ajeno || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_pais: ' || const$comilla_simple || par$cod_pais || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_fecha_saldos: ' || const$comilla_simple || par$cod_fecha_saldos || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_cultura: ' || const$comilla_simple || par$cod_cultura || const$comilla_simple || const$new_line;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => var$obs_detalle,
                      par$cod_identificador   => '');
      END IF;

      svalidar_saldos_acuerdos(
          par$oid_llamada             => par$oid_llamada,
          par$cod_identificador_ajeno => par$cod_identificador_ajeno,
          par$cod_pais                => par$cod_pais,
          par$des_sourcerefenceid     => par$des_sourcerefenceid,
          par$cod_fecha_saldos              => par$cod_fecha_saldos,
          par$cod_usuario             => par$cod_usuario,
          par$cod_cultura             => par$cod_cultura
      );

      IF NOT fhay_error_rec_sal_acu THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'No se encuentran errores tras llamar al procedure: svalidar_saldos_acuerdos',
              par$cod_identificador   => ''
          );

          OPEN par$rc_puntos FOR
							WITH source_reference as (
								SELECT
									DISTINCT
									OID_CAMPO7 OID_SALDO_ACUERDO_REF
								FROM	SAPR_GTT_TAUXILIAR
								WHERE COD_CALIFICADOR = 'OID_SALDO_ACUERDO'
							)
              SELECT 
                  SALACUREF.OID_SALDO_ACUERDO_REF SourceReferenceId,
                  ACUSER.DES_CONTRACT_ID ContractId,
                  ACUSER.DES_SERVICE_ORDER_ID ServiceOrderId,
                  ACUSER.DES_SERVICE_ORDER_CODE ServiceOrderCode,
                  ACUSER.DES_PRODUCT_CODE ProductCode,
                  PTOSER.COD_PTO_SERVICIO CodigoPuntoServicio
              FROM
                  SAPR_TACUERDO_SERVICIO ACUSER
                  INNER JOIN GEPR_TPUNTO_SERVICIO PTOSER ON PTOSER.OID_PTO_SERVICIO = ACUSER.OID_PTO_SERVICIO
                  INNER JOIN SAPR_TSALDO_ACUERDO_REF SALACUREF ON SALACUREF.OID_ACUERDO_SERVICIO = ACUSER.OID_ACUERDO_SERVICIO
                  INNER JOIN source_reference ON source_reference.OID_SALDO_ACUERDO_REF =SALACUREF.OID_SALDO_ACUERDO_REF
              ;

          OPEN par$rc_movimientos FOR 
              WITH divisas as (
                  SELECT
                      DIVISA.OID_DIVISA IDENTIFICADOR,
                      DIVISA.COD_ISO_DIVISA COD_DIVISA,
                      DIVISA.DES_DIVISA DES_DIVISA,
                      CADIVISA.COD_AJENO COD_DIVISA_AJENO,
                      CADIVISA.DES_AJENO DES_DIVISA_AJENO
                  FROM
                      GEPR_TDIVISA DIVISA
                      LEFT JOIN GEPR_TCODIGO_AJENO CADIVISA ON DIVISA.OID_DIVISA = CADIVISA.OID_TABLA_GENESIS AND CADIVISA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' AND CADIVISA.COD_IDENTIFICADOR=par$cod_identificador_ajeno
              )
              SELECT
                  DISTINCT
                  (SALACU.HOR_SALDO - INTERVAL '23:59:59' HOUR TO SECOND) FechaHoraInicio,
                  SALACU.HOR_SALDO FechaHoraFin,
                  SALACU.COD_TIPO_MERCANCIA TipoMercancia,
                  CASE WHEN par$cod_identificador_ajeno IS NULL THEN DIVISAS.COD_DIVISA ELSE DIVISAS.COD_DIVISA_AJENO END AS COD_DIVISA,
                  CASE WHEN par$cod_identificador_ajeno IS NULL THEN DIVISAS.DES_DIVISA ELSE DIVISAS.DES_DIVISA_AJENO END AS DES_DIVISA,
                  SALACU.NEL_CANT_CASHIN CantidadTransaccionesCashIn,
                  SALACU.NUM_TOTAL_CASHIN TotalCashIn,
									SALACU.NEL_CANT_SHIPOUT CantidadTransaccionesShipOut,
                  SALACU.NUM_TOTAL_SHIPOUT TotalShipOut,
									SALACU.NUM_TOTAL_ACREDITACION TotalAcreditacionAcred,
                  SALACU.NUM_TOTAL_CASHIN_ACRED TotalTransaccionesAcred,
                  SALACU.NUM_TOTAL_COMISION TotalComisionAcred,
                  SALACU.OID_SALDO_ACUERDO_REF SourceReferenceId,
									SALACU.FYH_INICIO_VIGENCIA_PLAN FechaHoraInicioVigencia
              FROM
                  SAPR_TSALDO_ACUERDO SALACU
                  INNER JOIN DIVISAS DIVISAS ON DIVISAS.IDENTIFICADOR = SALACU.OID_DIVISA
                  INNER JOIN SAPR_GTT_TAUXILIAR FILTRO_ACUERDOS ON FILTRO_ACUERDOS.OID_CAMPO1 = SALACU.OID_SALDO_ACUERDO AND FILTRO_ACUERDOS.COD_CALIFICADOR = 'OID_SALDO_ACUERDO';
      ELSE
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Encontró errores por la función fhay_error_rec_sal_acu',
              par$cod_identificador   => ''
          );
      END IF;

      OPEN par$rc_validaciones FOR
          SELECT
              OID_CAMPO1 AS CODIGO,
              COD_CAMPO2 AS DESCRIPCION
          FROM
              SAPR_GTT_TAUXILIAR
          WHERE
              COD_CALIFICADOR = 'VALIDACIONES';

  END srecuperar_saldos_acuerdos;

  /*Valida la entrada al procedure srecuperar_saldos_acuerdos*/
  PROCEDURE svalidar_saldos_acuerdos(
      /*Identificador Log*/
      par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
      /*Filtros entrada*/
      par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$des_sourcerefenceid     IN gepr_pcomon_###VERSION###.tipo$desc_,
      par$cod_fecha_saldos              IN gepr_pcomon_###VERSION###.tipo$cod_,
      /*Patron*/
      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_)
  IS
      /*Log del reporte*/
      var$obs_detalle             gepr_pcomon_###VERSION###.tipo$obs_;
      var$nel_existe_pais         gepr_pcomon_###VERSION###.tipo$nel_;
      var$nel_existe_scr_ref_id   gepr_pcomon_###VERSION###.tipo$nel_; /* Existe source reference id */
      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_saldos_acuerdos';
      var$oid_pais                gepr_pcomon_###VERSION###.tipo$oid_;
      var$cod_pais                gepr_pcomon_###VERSION###.tipo$cod_;
      var$nel_hay_registros         gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN

      /* Grabar logs de entrada */
      var$obs_detalle := '';

      IF par$oid_llamada IS NOT NULL THEN
          var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_identificador_ajeno: ' || const$comilla_simple || par$cod_identificador_ajeno || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_pais: ' || const$comilla_simple || par$cod_pais || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_fecha_saldos: ' || const$comilla_simple || par$cod_fecha_saldos || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
          var$obs_detalle := var$obs_detalle || 'par$cod_cultura: ' || const$comilla_simple || par$cod_cultura || const$comilla_simple || const$new_line;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => var$obs_detalle,
                      par$cod_identificador   => '');
      END IF;


      /* Validar código de país */
      /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
      var$cod_pais := par$cod_pais;
      GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                              par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                              par$cod_pais => var$cod_pais,
                              par$oid_pais => var$oid_pais,
                              par$cod_cultura => par$cod_cultura);


      IF var$oid_pais IS NOT NULL THEN
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
        VALUES (var$oid_pais, 'OID_PAIS');
        
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                    par$des_version     => const$version,
                                    par$des_detalle     => 'OID_PAIS: ' || var$oid_pais,
                                    par$cod_identificador   => '');
      END IF;

      /* Validación del SOURCE_REFERENCE_ID */
      IF par$des_sourcerefenceid IS NOT NULL THEN
          BEGIN
              SELECT COUNT(1)
              INTO var$nel_existe_scr_ref_id
              FROM SAPR_TSALDO_ACUERDO
              WHERE
                  OID_SALDO_ACUERDO_REF = par$des_sourcerefenceid;
          exception
            when no_data_found then
              var$nel_existe_scr_ref_id := 0;
          END;

          IF var$nel_existe_scr_ref_id = 0 THEN
              /* 2040330005 - El source reference id informado no existe. */
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
              VALUES ('2040330005', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040330005', gepr_pcomon_###VERSION###.const$CodFunRecuperarSalAcuer, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), 'VALIDACIONES');
      
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,
                                          par$des_detalle     => 'El código de país es inexistente: 2040330005 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040330005', gepr_pcomon_###VERSION###.const$CodFunRecuperarSalAcuer, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                                          par$cod_identificador   => '');
          
          ELSE
            /* Obtengo los OID_SALDO_ACUERDO a filtrar */
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR, OID_CAMPO7)
            SELECT salacu.OID_SALDO_ACUERDO, 'OID_SALDO_ACUERDO', salacu.oid_saldo_acuerdo_ref
            FROM sapr_tsaldo_acuerdo_ref salacuref
            INNER JOIN sapr_tsaldo_acuerdo salacu ON salacu.oid_saldo_acuerdo_ref = salacuref.oid_saldo_acuerdo_ref
                    AND salacu.fec_saldo = salacuref.fec_saldo
                    AND salacuref.hor_saldo = salacu.hor_saldo
                    AND salacuref.oid_pais = salacu.oid_pais
            WHERE salacuref.oid_saldo_acuerdo_ref = par$des_sourcerefenceid
            AND  salacuref.oid_pais = var$oid_pais;

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'OID_SALDO_ACUERDO Cantidad de líneas: ' || sql%rowcount,
                        par$cod_identificador   => '');
          END IF;
      ELSIF par$cod_fecha_saldos IS NULL THEN
          /* 2040330006 - Debe informar o el campo fecha o el campo source reference id. */
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          VALUES ('2040330006', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040330006', gepr_pcomon_###VERSION###.const$CodFunRecuperarSalAcuer, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), 'VALIDACIONES');
          
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,
                                          par$des_detalle     => 'Debe informar o el campo fecha o el campo source reference id: 2040330006 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040330006', gepr_pcomon_###VERSION###.const$CodFunRecuperarSalAcuer, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                                          par$cod_identificador   => '');

      ELSE
        /* Obtengo los OID_SALDO_ACUERDO a filtrar */
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR, OID_CAMPO7)
        SELECT salacu.OID_SALDO_ACUERDO, 'OID_SALDO_ACUERDO', salacu.oid_saldo_acuerdo_ref
        FROM sapr_tsaldo_acuerdo_ref salacuref
        INNER JOIN sapr_tsaldo_acuerdo salacu ON salacu.oid_saldo_acuerdo_ref = salacuref.oid_saldo_acuerdo_ref
          AND salacu.fec_saldo = salacuref.fec_saldo
          AND salacuref.hor_saldo = salacu.hor_saldo
          AND salacuref.oid_pais = salacu.oid_pais
        WHERE TO_TIMESTAMP_TZ(par$cod_fecha_saldos, const$formato_gmt)  BETWEEN (SALACU.HOR_SALDO - INTERVAL '23:59:59' HOUR TO SECOND) AND SALACU.HOR_SALDO
        AND  salacuref.oid_pais = var$oid_pais;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'OID_SALDO_ACUERDO Cantidad de líneas: ' || sql%rowcount,
                        par$cod_identificador   => '');
      END IF;

      IF NOT fhay_error_rec_sal_acu THEN
        BEGIN
            select count(1)
              into var$nel_hay_registros
              from SAPR_GTT_TAUXILIAR
             where COD_CALIFICADOR='OID_SALDO_ACUERDO';
        exception
          when no_data_found then
            var$nel_hay_registros := 0;
        END;
        IF var$nel_hay_registros = 0 THEN
          /* 2040330007 - Con los filtros informados no se encontraron registros de saldos acuerdo. */
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          VALUES ('2040330007', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040330007', gepr_pcomon_###VERSION###.const$CodFunRecuperarSalAcuer, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), 'VALIDACIONES');
          
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,
                                          par$des_detalle     => 'Sin OID_SALDO_ACUERDO Cantidad de líneas:  2040330007 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040330007', gepr_pcomon_###VERSION###.const$CodFunRecuperarSalAcuer, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                                          par$cod_identificador   => '');

        END IF;
      END IF;
  END svalidar_saldos_acuerdos;
  /* Función que corrobora si hay error para el procedure de recuperar saldos acuerdos */
  FUNCTION fhay_error_rec_sal_acu RETURN BOOLEAN IS
      var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN
      BEGIN
      SELECT COUNT(1)
          INTO var$existe
          FROM SAPR_GTT_TAUXILIAR A
          WHERE A.COD_CALIFICADOR = 'VALIDACIONES';
      EXCEPTION WHEN no_data_found THEN
          var$existe := 0;
      END;
      dbms_output.put_line('Valor var$existe: ' || var$existe);
      RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);
  END fhay_error_rec_sal_acu;

  /* Recuperar Notificacion */
  PROCEDURE srecuperar_notificacion(
                              par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_pais                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$oid_integracion     IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$rc_datos                  OUT sys_refcursor)
  IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_notificacion';
    var$oid_pais gepr_pcomon_###VERSION###.tipo$oid_;

  BEGIN

    /* #### Inicializar los cursores #### */
    OPEN par$rc_datos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* #### Limpiar tabla auxiliar #### */
    DELETE FROM SAPR_GTT_TAUXILIAR; COMMIT;


    /*Grabar logs de parametros iniciales*/
    IF par$oid_llamada IS NOT NULL THEN
      /* OID_LLAMADA */
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$oid_llamada: ' || par$oid_llamada,
        par$cod_identificador   => '');
      
      /*CÓDIGO USUARIO*/
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
        par$cod_identificador   => '');

      /*CÓDIGO CULTURA*/
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
        par$cod_identificador   => '');

      /*CÓDIGO PAIS*/
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$cod_pais: ' || par$cod_pais,
        par$cod_identificador   => '');

      /*IDENTIFICADOR AJENO*/
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
        par$cod_identificador   => '');

      /*IDENTIFICADOR AJENO*/
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$oid_integracion: ' || par$oid_integracion,
        par$cod_identificador   => '');
    END IF;

    BEGIN
      IF par$cod_identificador_ajeno IS NOT NULL THEN
        SELECT OID_TABLA_GENESIS
        INTO  var$oid_pais
        FROM  GEPR_TCODIGO_AJENO
        WHERE
          COD_AJENO = par$cod_pais
          AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
          AND COD_TIPO_TABLA_GENESIS = 'GEPR_TPAIS';
      ELSE
        SELECT OID_PAIS
        INTO var$oid_pais
        FROM  GEPR_TPAIS
        WHERE COD_PAIS = par$cod_pais
        ;
      END IF;
    exception
      when no_data_found then
        var$oid_pais := null;
    END;


    OPEN par$rc_datos FOR
      WITH BILLING AS (
        /*billig*/
        SELECT
          GEIN.oid_integracion idTran,
          /* GEIN.cod_proceso integracion, */
          'billing' integracion,
          GEIN.gmt_creacion dateTime,
          GEIN.oid_tabla_integracion sourceId,
          CASE GEIN.COD_ESTADO
            WHEN 'AB' THEN 'A'
            WHEN 'MD' THEN 'M'
            ELSE 'NO-DEFINIDO'
          END  operation,
          ASRE.HOR_SALDO,
          PUSE.COD_PTO_SERVICIO,
          PAIS.COD_PAIS country ,
          CASE 
              WHEN PAIS.cod_pais IN ('AR' , 'BR' , 'CL' ,'CR' , 'CO' , 'EC' , 'SV' , 'GT' , 'HN' , 'MX' , 'NI' , 'PE' , 'PY' , 'UY') then 'LATAM'
              WHEN PAIS.cod_pais IN ('DE','ES',  'PT') THEN 'EMEA'
              WHEN PAIS.cod_pais IN ('AU', 'IN') THEN 'APAC'
              ELSE 'NO DEFINE'
          END region
        FROM
          GEPR_TINTEGRACION GEIN
          INNER JOIN SAPR_TSALDO_ACUERDO_REF ASRE ON GEIN.OID_TABLA_INTEGRACION = ASRE.OID_SALDO_ACUERDO_REF
          INNER JOIN SAPR_TACUERDO_SERVICIO ACSE ON ASRE.OID_ACUERDO_SERVICIO = ACSE.OID_ACUERDO_SERVICIO
          INNER JOIN GEPR_TPUNTO_SERVICIO PUSE ON ACSE.OID_PTO_SERVICIO = PUSE.OID_PTO_SERVICIO
          INNER JOIN GEPR_TPAIS PAIS ON ASRE.OID_PAIS = PAIS.OID_PAIS AND PAIS.OID_PAIS = var$oid_pais
        where
          GEIN.OID_INTEGRACION = par$oid_integracion
      )
      /*billig*/
      SELECT
        bill.idTran,
        bill.integracion,
        bill.dateTime,
        bill.sourceId,
        CASE WHEN ROWNUM <= 1 THEN 'A' ELSE 'M' END AS operation,
        bill.country,
        bill.region,
        'servicePointCode' llave,
        bill.COD_PTO_SERVICIO valor
      FROM
        BILLING bill
      UNION
        SELECT
          bill.idTran,
          bill.integracion,
          bill.dateTime,
          bill.sourceId,
          CASE WHEN ROWNUM <= 1 THEN 'A' ELSE 'M' END AS operation,
          bill.country,
          bill.region,
          'dateTimeReference' llave,
          to_CHAR(bill.hor_saldo, 'DD/MM/YYYY HH24:MI:SS') valor
        FROM
          BILLING bill
        ORDER BY dateTime;
  END srecuperar_notificacion;

END SAPR_PSALDOS_###VERSION###;
/