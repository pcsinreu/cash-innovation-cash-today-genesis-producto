CREATE OR REPLACE package SAPR_PJOB_###VERSION### IS

  /*Version: ###VERSION_COMP###*/
  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PJOB_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
  const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
  const$comilla_simple  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);
  const$formato_gmt CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'DD/MM/YYYY HH24:MI:SS TZH:TZM';
  
  PROCEDURE sactualizar_fecha_calculos
  (
    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$rc_validaciones         OUT sys_refcursor
  );

  PROCEDURE sactualizar_saldos_historico (
    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
    par$rc_validaciones         OUT sys_refcursor
  );

  PROCEDURE sactualizar_saldo_acuerdo(
        par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$bol_forzar_calculo      IN gepr_pcomon_###VERSION###.tipo$nbol_,
        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_validaciones         OUT sys_refcursor);       

  PROCEDURE sactualizar_periodos (
    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
    par$rc_validaciones         OUT sys_refcursor,
    par$rc_periodos             OUT sys_refcursor
  );

  PROCEDURE sdepuracion(
        par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_validaciones         OUT sys_refcursor);

  PROCEDURE snotificar_mov_noacreditados(
        par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_validaciones         OUT sys_refcursor,
        par$rc_movimientos          OUT sys_refcursor);

  PROCEDURE sgenerar_periodos(
        par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_deviceid            IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
        par$rc_validaciones         OUT sys_refcursor
        );

end SAPR_PJOB_###VERSION###;
/
create or replace package body SAPR_PJOB_###VERSION### IS

  PROCEDURE sactualizar_fecha_calculos
    (
      par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
      par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
      par$rc_validaciones         OUT sys_refcursor
    ) IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sactualizar_saldos_historico';
    var$mensaje                     gepr_pcomon_###VERSION###.tipo$desc_;
  
  BEGIN
    
    /* Log API*/       
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => 'Se actualiza fecha de TIPO_FECHA =  de TIPO_FECHA = SYSDATE',
            par$cod_identificador   => '');
     UPDATE SAPR_TFECHA_CALCULOS
        SET FECHA = TO_DATE(SYSDATE)
        WHERE TIPO_FECHA = 'SYSDATE';

    /* Log API*/       
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => 'Se actualiza fecha de TIPO_FECHA = SYSDATE-15',
            par$cod_identificador   => '');
    UPDATE SAPR_TFECHA_CALCULOS
        SET FECHA = TO_DATE(SYSDATE-15)
        WHERE TIPO_FECHA = 'SYSDATE-15';

    /* Log API*/       
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => 'Se actualiza fecha de TIPO_FECHA = ADD_MONTHS(SYSDATE,-1)',
            par$cod_identificador   => '');
    UPDATE SAPR_TFECHA_CALCULOS
        SET FECHA = TO_DATE(ADD_MONTHS(SYSDATE,-1))
        WHERE TIPO_FECHA = 'ADD_MONTHS(SYSDATE,-1)';

    /* Log API*/       
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => 'Se actualiza fecha de TIPO_FECHA = ADD_MONTHS(SYSDATE,-12)',
            par$cod_identificador   => '');
    UPDATE SAPR_TFECHA_CALCULOS
        SET FECHA = TO_DATE(ADD_MONTHS(SYSDATE,-12))
        WHERE TIPO_FECHA = 'ADD_MONTHS(SYSDATE,-12)';
     COMMIT;
     EXCEPTION WHEN OTHERS THEN
       BEGIN
          var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'3060010003',
                    'ACTUALIZAR_FECHA_CALCULOS',
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    '',
                    0);
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CALIFICADOR)
                    VALUES (
                        '3060010003',
                        var$mensaje,
                        'VALIDACIONES_FECHA_CALC');
      END;
    OPEN par$rc_validaciones FOR
    SELECT OID_CAMPO1 AS CODIGO,  
          COD_CAMPO2 AS DESCRIPCION,
          COD_CALIFICADOR AS CALIFICADOR 
          FROM SAPR_GTT_TAUXILIAR AUX
    WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES_FECHA_CALC';
        
  END sactualizar_fecha_calculos;

  PROCEDURE sactualizar_saldos_historico (
                                          par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
                                          par$rc_validaciones         OUT sys_refcursor
                                          ) IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sactualizar_saldos_historico';
    var$oid_pais          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_pais          gepr_pcomon_###VERSION###.tipo$cod_;
    var$mensaje           gepr_pcomon_###VERSION###.tipo$desc_;
    var$rc_validaciones   sys_refcursor;

    /* CURSOR DE SAPR_TCONTROL_SALDO_HISTORICO */
    CURSOR cur$controlSaldo IS
      SELECT DISTINCT CLIE.OID_CLIENTE, MAQU.OID_MAQUINA, HIST.FEC_SALDO, HIST.HOR_SALDO
      FROM
      GEPR_TCLIENTE CLIE
      INNER JOIN SAPR_TCUENTA CUEN ON CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
      INNER JOIN GEPR_TSECTOR SECT ON CUEN.OID_SECTOR = SECT.OID_SECTOR
      INNER JOIN SAPR_TMAQUINA MAQU ON SECT.OID_SECTOR = MAQU.OID_SECTOR
      INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
      INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
      LEFT JOIN SAPR_TCONTROL_SALDO_HISTORICO HIST ON HIST.OID_CLIENTE = CLIE.OID_CLIENTE 
          AND HIST.OID_MAQUINA = MAQU.OID_MAQUINA AND HIST.OID_PAIS = DELE.OID_PAIS

      WHERE DELE.OID_PAIS = var$oid_pais AND CLIE.BOL_SALDO_HISTORICO = '1' AND CLIE.COD_FECHA_SALDO_HISTORICO IN ('CREACION','GESTION')
      AND FLOOR(SYSDATE - NVL(HIST.FEC_SALDO, SYSDATE -1)) > 0;
      /* Se debe contemplar GMT??*/


  BEGIN
    /* ##### LIMPIAR TABLAS TEMPORARIAS ##### */
    DELETE SAPR_GTT_TAUXILIAR;
    COMMIT;  
    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

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

        /* COD_IDENTIFICADOR_AJENO */
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
            par$cod_identificador   => '');
    END IF;

    BEGIN

      SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Previo a validar código de país',
          par$cod_identificador   => '');

      /* Validar código pais*/
      /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
      var$cod_pais := par$cod_pais;
      GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                              par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                              par$cod_pais => var$cod_pais,
                              par$oid_pais => var$oid_pais,
                              par$cod_cultura => par$cod_cultura);
        
      IF var$oid_pais IS NOT NULL THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'El código de país existe en la BBDD y su OID_PAIS es: ' || var$oid_pais,
          par$cod_identificador   => '');
        FOR rec$controlSaldo IN cur$controlSaldo LOOP
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Llama a SAPR_PSALDOS_###VERSION###.sactualizar_saldos_historico con: ' || const$new_line
                                    || 'par$oid_cliente = ' || rec$controlSaldo.OID_CLIENTE || const$new_line
                                    || 'par$oid_maquina = ' || rec$controlSaldo.OID_MAQUINA || const$new_line
                                    || 'par$cod_tipo_actualizacion = ANTERIOR' || const$new_line
                                    || 'par$cod_usuario = JOB_ActualizarSaldosHistorico' || const$new_line
                                    || 'par$oid_pais = ' || var$oid_pais || const$new_line
                                    ,
            par$cod_identificador   => '');
          SAPR_PSALDOS_###VERSION###.sactualizar_saldos_historico(
                                                        par$oid_cliente             => rec$controlSaldo.OID_CLIENTE,
                                                        par$oid_maquina             => rec$controlSaldo.OID_MAQUINA,
                                                        par$oid_pais                => var$oid_pais,
                                                        par$cod_tipo_actualizacion  => 'ANTERIOR',
                                                        par$cod_cultura             => par$cod_cultura,
                                                        par$cod_usuario             => 'JOB_ActualizarSaldosHistorico',
                                                        par$rc_validaciones         => var$rc_validaciones
                                                        );
        END LOOP rec$controlSaldo;
      END IF;


    EXCEPTION WHEN OTHERS THEN
        ROLLBACK;
        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'3060020003',
                  'ACTUALIZARSALDOSHISTORICO',
                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                  '',
                  0);
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CALIFICADOR)
        VALUES ('3060020003', var$mensaje, 'VALIDACIONES');
        
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => '¡Ocurrió una excepción!',
          par$cod_identificador   => '');
    END;

    OPEN par$rc_validaciones FOR
        SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CALIFICADOR AS CALIFICADOR 
        FROM SAPR_GTT_TAUXILIAR AUX
        WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';
    COMMIT;
  END sactualizar_saldos_historico;

    
  PROCEDURE sactualizar_saldo_acuerdo(
      par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
      par$bol_forzar_calculo      IN gepr_pcomon_###VERSION###.tipo$nbol_,
      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$rc_validaciones         OUT sys_refcursor)       

  IS   
      var$rc_validaciones   sys_refcursor;
      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sactualizar_saldo_acuerdo';
  BEGIN

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

          /* BOL_FORZAR_CALCULO */
          IF par$bol_forzar_calculo = 1 THEN
              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'par$bol_forzar_calculo: 1',
                  par$cod_identificador   => '');
          ELSE
              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'par$bol_forzar_calculo: 0',
                  par$cod_identificador   => '');
          END IF;

          /* COD_CULTURA */
          SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
              par$cod_identificador   => '');

          /* COD_IDENTIFICADOR_AJENO */
          SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
              par$cod_identificador   => '');

          /* COD_USUARIO */
          SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
              par$cod_identificador   => '');
      END IF;

      /* #### Inicializar los cursores #### */
      OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

      SAPR_PSALDOS_###VERSION###.sactualizar_saldo_acuerdo(
          par$oid_llamada             => par$oid_llamada,
          par$cod_pais                => par$cod_pais,
          par$bol_forzar_calculo      => par$bol_forzar_calculo,
          par$cod_usuario             => par$cod_usuario,
          par$cod_identificador_ajeno => par$cod_identificador_ajeno,
          par$cod_cultura             => par$cod_cultura,
          par$rc_validaciones         => var$rc_validaciones);

          par$rc_validaciones := var$rc_validaciones;

  END sactualizar_saldo_acuerdo;

  PROCEDURE sactualizar_periodos (
                                          par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
                                          par$rc_validaciones         OUT sys_refcursor,
                                          par$rc_periodos             OUT sys_refcursor
                                          ) IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sactualizar_periodos';
    var$oid_pais          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_pais          gepr_pcomon_###VERSION###.tipo$cod_;
    var$mensaje           gepr_pcomon_###VERSION###.tipo$desc_;
    var$max_horas         gepr_pcomon_###VERSION###.tipo$desc_;  
    var$oid_estado_periodo_NC  gepr_pcomon_###VERSION###.tipo$oid_; --NO CONFIRMADO
    var$oid_estado_periodo_NA  gepr_pcomon_###VERSION###.tipo$oid_; --NO ACREDITADO
    var$rc_validaciones   sys_refcursor;
    const$cod_parametro_max_horas  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'FechaValorConfirmacionMaxHorasConfirmacion';

    


  BEGIN
    /* ##### LIMPIAR TABLAS TEMPORARIAS ##### */
    DELETE SAPR_GTT_TAUXILIAR;
    COMMIT;  
    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_periodos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    
    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_NA FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'NA';
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Carga var$oid_estado_periodo_NA: ' || var$oid_estado_periodo_NA,
                            par$cod_identificador   => '');
    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_NC FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'NC';
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Carga var$oid_estado_periodo_NC: ' || var$oid_estado_periodo_NC,
                            par$cod_identificador   => '');
    

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

        /* COD_IDENTIFICADOR_AJENO */
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
            par$cod_identificador   => '');
    END IF;

    BEGIN

      SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Previo a validar código de país',
          par$cod_identificador   => '');

      /* Validar código pais*/
      /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
      var$cod_pais := par$cod_pais;
      GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                              par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                              par$cod_pais => var$cod_pais,
                              par$oid_pais => var$oid_pais,
                              par$cod_cultura => par$cod_cultura);
        
      IF var$oid_pais IS NOT NULL THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'El código de país existe en la BBDD y su OID_PAIS es: ' || var$oid_pais,
          par$cod_identificador   => '');

         var$max_horas := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                                                              par$cod_pais => var$cod_pais,
                                                                              par$cod_parametro => const$cod_parametro_max_horas,
                                                                              par$cod_aplicacion => 'Genesis');

          OPEN par$rc_periodos FOR
            SELECT PER.COD_PERIODO_CONFIRMACION, MAQ.COD_IDENTIFICACION, SECT.DES_SECTOR, PER.COD_MENSAJE_CONFIRMACION, PXMEN.DES_MENSAJE 
            FROM SAPR_TPERIODO PER
              INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_MAQUINA = PER.OID_MAQUINA
              INNER JOIN SAPR_TPLANXMAQUINA PXM ON PXM.OID_MAQUINA = MAQ.OID_MAQUINA AND PXM.BOL_ACTIVO = 1
              INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PXM.OID_PLANIFICACION
              LEFT JOIN SAPR_TPLANXMENSAJE PXMEN ON PXMEN.OID_PLANIFICACION = PLANI.OID_PLANIFICACION AND PXMEN.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND PER.COD_MENSAJE_CONFIRMACION = PXMEN.COD_MENSAJE
              INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = MAQ.OID_SECTOR
              INNER JOIN SAPR_TESTADO_PERIODO EST ON EST.OID_ESTADO_PERIODO = PER.OID_ESTADO_PERIODO
              INNER JOIN SAPR_TTIPO_PERIODO TPER ON TPER.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO
            WHERE EST.COD_ESTADO_PERIODO = 'CO' AND FYH_ENVIO_BANCO IS NOT NULL
              AND (extract(DAY FROM systimestamp - FYH_ENVIO_BANCO)*24)+ 
              (extract(HOUR FROM systimestamp - FYH_ENVIO_BANCO))+
              (extract(MINUTE FROM systimestamp - FYH_ENVIO_BANCO)/60)+
              (extract(SECOND FROM systimestamp - FYH_ENVIO_BANCO) /60 / 60 ) >=  var$max_horas;
        

            UPDATE SAPR_TPERIODO SET OID_ESTADO_PERIODO = var$oid_estado_periodo_NA,
            GMT_MODIFICACION = SYSTIMESTAMP, DES_USUARIO_MODIFICACION = 'JOB'
            WHERE OID_PERIODO IN (
              SELECT OID_PERIODO FROM SAPR_TPERIODO PER
              INNER JOIN SAPR_TESTADO_PERIODO EST ON EST.OID_ESTADO_PERIODO = PER.OID_ESTADO_PERIODO
              INNER JOIN SAPR_TTIPO_PERIODO TPER ON TPER.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO
              WHERE TPER.COD_TIPO_PERIODO = 'AC' AND EST.COD_ESTADO_PERIODO = 'CO' AND FYH_ENVIO_BANCO IS NOT NULL
              AND (extract(DAY FROM systimestamp - FYH_ENVIO_BANCO)*24)+ 
              (extract(HOUR FROM systimestamp - FYH_ENVIO_BANCO))+
              (extract(MINUTE FROM systimestamp - FYH_ENVIO_BANCO)/60)+
              (extract(SECOND FROM systimestamp - FYH_ENVIO_BANCO) /60 / 60 )   >=  var$max_horas 
              );
            UPDATE SAPR_TPERIODO SET OID_ESTADO_PERIODO = var$oid_estado_periodo_NC,
            GMT_MODIFICACION = SYSTIMESTAMP, DES_USUARIO_MODIFICACION = 'JOB'
            WHERE OID_PERIODO IN (
              SELECT OID_PERIODO FROM SAPR_TPERIODO PER
              INNER JOIN SAPR_TESTADO_PERIODO EST ON EST.OID_ESTADO_PERIODO = PER.OID_ESTADO_PERIODO
              INNER JOIN SAPR_TTIPO_PERIODO TPER ON TPER.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO
              WHERE TPER.COD_TIPO_PERIODO IN('BO', 'RE') AND EST.COD_ESTADO_PERIODO = 'CO' AND FYH_ENVIO_BANCO IS NOT NULL
              AND (extract(DAY FROM systimestamp - FYH_ENVIO_BANCO)*24)+ 
              (extract(HOUR FROM systimestamp - FYH_ENVIO_BANCO))+
              (extract(MINUTE FROM systimestamp - FYH_ENVIO_BANCO)/60)+
              (extract(SECOND FROM systimestamp - FYH_ENVIO_BANCO) /60 / 60 )   >=  var$max_horas 
              );
        COMMIT;
     
      END IF;


    EXCEPTION WHEN OTHERS THEN
        ROLLBACK;
        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'3060020003',
                  'ACTUALIZAR_PERIODOS',
                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                  '',
                  0);
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CALIFICADOR)
        VALUES ('3060020003', var$mensaje, 'VALIDACIONES');
        
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => '¡Ocurrió una excepción!',
          par$cod_identificador   => '');
    END;

    OPEN par$rc_validaciones FOR
        SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CALIFICADOR AS CALIFICADOR 
        FROM SAPR_GTT_TAUXILIAR AUX
        WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';
  END sactualizar_periodos;
    
  PROCEDURE sdepuracion(
      par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
      par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$rc_validaciones         OUT sys_refcursor)       
  IS   
      var$rc_validaciones   sys_refcursor;
      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sdepuracion';
  BEGIN

    /* #### Grabar llamadas #### */
    IF par$oid_llamada IS NOT NULL THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => q'[Parametros iniciales: 
              par$oid_llamada: ]'             || par$oid_llamada              || q'[
              par$cod_identificador_ajeno: ]' || par$cod_identificador_ajeno  || q'[
              par$cod_pais: ]'                || par$cod_pais                 || q'[
              par$cod_usuario: ]'             || par$cod_usuario              || q'[
              par$cod_cultura: ]'             || par$cod_cultura,
            par$cod_identificador   => '');
    END IF;  

    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    SAPR_PDEPURACION_###VERSION###.sdepurar(
        par$oid_llamada             => par$oid_llamada,
        par$cod_identificador_ajeno => par$cod_identificador_ajeno,
        par$cod_pais                => par$cod_pais,
        par$cod_usuario             => par$cod_usuario,
        par$cod_cultura             => par$cod_cultura,
        par$rc_validaciones         => var$rc_validaciones);

        par$rc_validaciones := var$rc_validaciones;

    EXCEPTION WHEN OTHERS THEN
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Se produce EXCEPTION: ' || SQLCODE || ' - ' || SQLERRM,
                                            par$cod_identificador   => '');

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                        SQLCODE || ' - ' || SQLERRM,
                        true);     
  END sdepuracion;
  
  PROCEDURE snotificar_mov_noacreditados(
        par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_validaciones         OUT sys_refcursor,
        par$rc_movimientos          OUT sys_refcursor)
  IS
  var$rc_validaciones   sys_refcursor;
  var$rc_movimientos    sys_refcursor;
  const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.srec_movimientos_noacreditados';

  BEGIN
    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    SAPR_PNOTIFICACION_SERV_###VERSION###.srec_movimientos_noacreditados(
        par$oid_llamada             => par$oid_llamada,
        par$cod_identificador_ajeno => par$cod_identificador_ajeno,
        par$cod_pais                => par$cod_pais,
        par$cod_usuario             => par$cod_usuario,
        par$cod_cultura             => par$cod_cultura,
        par$rc_validaciones         => var$rc_validaciones,
        par$rc_movimientos          => var$rc_movimientos);

        par$rc_validaciones := var$rc_validaciones;
        par$rc_movimientos  := var$rc_movimientos;

    EXCEPTION WHEN OTHERS THEN
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Se produce EXCEPTION: ' || SQLCODE || ' - ' || SQLERRM,
                                            par$cod_identificador   => '');

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                        SQLCODE || ' - ' || SQLERRM,
                        true);     
  END snotificar_mov_noacreditados;

  PROCEDURE sgenerar_periodos (
                                          par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_deviceid            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
                                          par$rc_validaciones         OUT sys_refcursor
                                          ) IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sgenerar_periodos';
    var$oid_pais          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_pais          gepr_pcomon_###VERSION###.tipo$cod_;
    var$mensaje           gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_devicedid     gepr_pcomon_###VERSION###.tipo$oid_;
    var$rc_validaciones   sys_refcursor;

    /* CURSOR DE SAPR_TMAQUINA Activo */
    CURSOR cur$maquinas IS
      SELECT MAQU.OID_MAQUINA
      FROM GEPR_TSECTOR SECT 
      INNER JOIN SAPR_TMAQUINA MAQU ON SECT.OID_SECTOR = MAQU.OID_SECTOR
      INNER JOIN GEPR_TPLANTA PLAN ON SECT.OID_PLANTA = PLAN.OID_PLANTA
      INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
      INNER JOIN SAPR_TPLANXMAQUINA PLANM ON PLANM.OID_MAQUINA = MAQU.OID_MAQUINA
      INNER JOIN SAPR_TPLANIFICACION PLANF ON PLANF.OID_PLANIFICACION = PLANM.OID_PLANIFICACION
      WHERE  DELE.OID_PAIS = var$oid_pais AND MAQU.BOL_ACTIVO = 1 AND PLANM.BOL_ACTIVO = 1 AND PLANF.BOL_ACTIVO = 1;
  
  BEGIN
    /* ##### LIMPIAR TABLAS TEMPORARIAS ##### */
    DELETE SAPR_GTT_TAUXILIAR;
    
    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* #### Grabar llamadas #### */
    IF par$oid_llamada IS NOT NULL THEN
        /* OID_LLAMADA */
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$oid_llamada: ' || par$oid_llamada,
            par$cod_identificador   => '');
        
        /* COD_IDENTIFICADOR_AJENO */
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
            par$cod_identificador   => '');

        /* COD_PAIS */
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_pais: ' || par$cod_pais,
            par$cod_identificador   => '');

        /* COD_DEVICEID */
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_deviceid: ' || par$cod_deviceid,
            par$cod_identificador   => '');

        /* COD_USUARIO */
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
            par$cod_identificador   => '');

        /* COD_CULTURA */
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
            par$cod_identificador   => '');

    END IF;
    
    SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Previo a validar código de país',
        par$cod_identificador   => '');

    /* Validar código pais*/
    /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
    var$cod_pais := par$cod_pais;
    GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                            par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                            par$cod_pais => var$cod_pais,
                            par$oid_pais => var$oid_pais,
                            par$cod_cultura => par$cod_cultura);
        
    IF var$oid_pais IS NOT NULL THEN
      SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'El código de país existe en la BBDD y su OID_PAIS es: ' || var$oid_pais,
              par$cod_identificador   => '');

      
      IF par$cod_deviceid IS NOT NULL THEN
        BEGIN
          SELECT MAQUI.OID_MAQUINA
              INTO var$oid_devicedid
          FROM SAPR_TMAQUINA MAQUI
          LEFT JOIN GEPR_TCODIGO_AJENO AJENO ON AJENO.OID_TABLA_GENESIS = MAQUI.OID_MAQUINA
          AND AJENO.COD_TIPO_TABLA_GENESIS = 'SAPR_TMAQUINA'
          AND AJENO.BOL_ACTIVO = 1
          AND AJENO.COD_IDENTIFICADOR = par$cod_identificador_ajeno
          WHERE (CASE WHEN par$cod_identificador_ajeno IS NULL THEN MAQUI.COD_IDENTIFICACION ELSE AJENO.COD_AJENO END ) = par$cod_deviceid
          AND MAQUI.BOL_ACTIVO = 1;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Carga var$oid_devicedid: ' || var$oid_devicedid,
                  par$cod_identificador   => '');

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Llama a SAPR_PPERIODO_###VERSION###.sgenerar_periodos con: ' || const$new_line
                                  || 'par$oid_llamada = ' ||  par$oid_llamada || const$new_line
                                  || 'par$oid_maquina = ' || var$oid_devicedid || const$new_line
                                  || 'par$cod_cultura = ' || par$cod_cultura || const$new_line
                                  || 'par$info_ejecucion = ' || const$new_line
                                  || 'par$COD_USUARIO = ' || par$cod_usuario || const$new_line,
            par$cod_identificador   => '');

          SAPR_PPERIODO_###VERSION###.sgenerar_periodos(
                                            par$oid_llamada               => par$oid_llamada,
                                            par$oid_maquina               => var$oid_devicedid,
                                            par$cod_cultura               => par$cod_cultura,
                                            par$info_ejecucion            => '',
                                            par$COD_USUARIO               => par$cod_usuario
                                            );
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
              var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2060060003',
              'GENERARPERIODOS',
              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
              '',
              0);

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => '2060060003 - ' || var$mensaje,
                  par$cod_identificador   => '');

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CALIFICADOR)
              VALUES ('2060060003', var$mensaje, 'VALIDACIONES');
        END;
      ELSE
        FOR rec$maquina IN cur$maquinas LOOP
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Llama a SAPR_PPERIODO_###VERSION###.sgenerar_periodos con: ' || const$new_line
                                  || 'par$oid_llamada = ' ||  par$oid_llamada || const$new_line
                                  || 'par$oid_maquina = ' || rec$maquina.OID_MAQUINA || const$new_line
                                  || 'par$cod_cultura = ' || par$cod_cultura || const$new_line
                                  || 'par$info_ejecucion = ' || const$new_line
                                  || 'par$COD_USUARIO = ' || par$cod_usuario || const$new_line,
            par$cod_identificador   => '');

          SAPR_PPERIODO_###VERSION###.sgenerar_periodos(
                                              par$oid_llamada               => par$oid_llamada,
                                              par$oid_maquina               => rec$maquina.OID_MAQUINA,
                                              par$cod_cultura               => par$cod_cultura,
                                              par$info_ejecucion            => '',
                                              par$COD_USUARIO               => par$cod_usuario
                                              );

        END LOOP rec$maquina;
      END IF;
    ELSE
      -- Buscamos las validaciones del metodo srecuperar_pais y le asignamos el código correspondiente a JOBS
      UPDATE SAPR_GTT_TAUXILIAR
        SET OID_CAMPO1 = '2060000001'
      WHERE OID_CAMPO1 = '2040010026' AND COD_CALIFICADOR = 'VALIDACIONES';

      UPDATE SAPR_GTT_TAUXILIAR
        SET OID_CAMPO1 = '2060000002'
      WHERE OID_CAMPO1 = '2040010027' AND COD_CALIFICADOR = 'VALIDACIONES';       
    END IF;

    OPEN par$rc_validaciones FOR
            SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CALIFICADOR AS CALIFICADOR 
            FROM SAPR_GTT_TAUXILIAR AUX
            WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';

  
  EXCEPTION WHEN OTHERS THEN
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,                            
                                          par$des_detalle     => 'Se produce EXCEPTION: ' || SQLCODE || ' - ' || SQLERRM,
                                          par$cod_identificador   => '');

    raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                      SQLCODE || ' - ' || SQLERRM,
                      true);  

  END sgenerar_periodos;
  
end SAPR_PJOB_###VERSION###;
/