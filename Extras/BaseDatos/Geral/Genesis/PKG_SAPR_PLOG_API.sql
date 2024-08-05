create or replace PACKAGE SAPR_PLOG_API AS

  PROCEDURE SGENERA_OID_LLAMADA(par$cod_pais          IN VARCHAR2,
                                par$des_recurso       IN VARCHAR2,
                                par$oid_llamada       IN OUT VARCHAR2);

  PROCEDURE SINICIA_LLAMADA(par$oid_llamada           IN VARCHAR2,
                            par$des_recurso            IN VARCHAR2,
                            par$des_version           IN VARCHAR2,
                            par$des_datos_entrada     IN CLOB, 
                            par$cod_pais              IN VARCHAR2,
                            par$cod_hash_entrada      IN VARCHAR2,
                            par$des_host              IN VARCHAR2,
                            par$des_host_ip           IN VARCHAR2);

  PROCEDURE SFINALIZA_LLAMADA(par$oid_llamada           IN VARCHAR2,
                              par$des_datos_salida      IN CLOB, 
                              par$cod_resultado         IN VARCHAR2,
                              par$des_resultado         IN VARCHAR2,
                              par$cod_hash_salida       IN VARCHAR2);                            

  PROCEDURE SAGREGA_DETALLE(par$oid_llamada           IN VARCHAR2,
                            par$des_origen            IN VARCHAR2,
                            par$des_version           IN VARCHAR2,                            
                            par$des_detalle           IN VARCHAR2,
                            par$cod_identificador     IN VARCHAR2);

  PROCEDURE SRECUPERA_DATOS(par$cod_pais              IN VARCHAR2,
                            par$cod_identificador     IN VARCHAR2,
                            par$oid_llamada           IN VARCHAR2,
                            par$recurso               IN VARCHAR2,
                            par$des_datos_entrada     IN VARCHAR2,
                            par$des_datos_salida      IN VARCHAR2,
                            par$cod_hash_entrada      IN VARCHAR2,
                            par$cod_hash_salida       IN VARCHAR2,
                            par$fyh_llamada_inicio    IN DATE,
                            par$fyh_llamada_fin       IN DATE,
                            par$des_host              IN VARCHAR2,
                            par$des_host_ip           IN VARCHAR2,
                            par$cur_llamadas          OUT sys_refcursor,
                            par$cur_detalles          OUT sys_refcursor);

END SAPR_PLOG_API;
/
create or replace PACKAGE BODY SAPR_PLOG_API AS

 PROCEDURE SGENERA_OID_LLAMADA(par$cod_pais              IN VARCHAR2,
                                par$des_recurso          IN VARCHAR2,
                                par$oid_llamada          IN OUT VARCHAR2) IS
    PRAGMA AUTONOMOUS_TRANSACTION;
    var$oid_apliacion             VARCHAR2(36);
    var$oid_nivel_parametro       VARCHAR2(36);
    var$cod_parametro             VARCHAR2(50); 
    var$valor_parametro           VARCHAR2(4000);
    var$error                     BOOLEAN := false;
    var$error_message             VARCHAR2(512);
    par$prefijo                   VARCHAR2(20);
  BEGIN
    SELECT 
         CASE
              WHEN INSTR(UPPER(par$des_recurso), 'IAC') = 1 THEN 'Log'
              WHEN INSTR(UPPER(par$des_recurso), 'SALDOS') = 1 THEN 'Log'
              WHEN INSTR(UPPER(par$des_recurso), 'REPORTE') = 1 THEN 'Log'
              ELSE 'LogServicio'
         END INTO par$prefijo
    FROM DUAL;
    var$cod_parametro := par$prefijo || par$des_recurso;
    BEGIN
      -- Busco el oid_aplicacion de codigo Genesis
      BEGIN
        SELECT OID_APLICACION
        INTO var$oid_apliacion
        FROM GEPR_TAPLICACION
        WHERE COD_APLICACION = 'Genesis';
      EXCEPTION WHEN no_data_found THEN
        var$error := TRUE;
        INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
        VALUES (SYS_GUID(), 'No existe un valor en GEPR_TAPLICACION para el COD_APLICACION Genesis', 'SGENERA_OID_LLAMADA', 'LOG_MOVIMIENTO', SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
      WHEN others THEN 
        var$error := TRUE;
        var$error_message := SQLERRM;
        INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
        VALUES (SYS_GUID(), 'Ocurrió un error al intentar obtener el OID_APLICACION con el código: Genesis ERROR: ' || var$error_message, 'SGENERA_OID_LLAMADA', 'LOG_MOVIMIENTO', SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
      END;

      -- Busco el oid_nivel_parametro de tipo Pais
      BEGIN
        SELECT OID_NIVEL_PARAMETRO 
        INTO var$oid_nivel_parametro
        FROM GEPR_TNIVEL_PARAMETRO
        WHERE COD_NIVEL_PARAMETRO = 1;
      EXCEPTION WHEN no_data_found THEN
        var$error := TRUE;
        INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
        VALUES (SYS_GUID(), 'No existe un valor en GEPR_TNIVEL_PARAMETRO para el COD_NIVEL_PARAMETRO '|| 1, 'SGENERA_OID_LLAMADA', 'LOG_MOVIMIENTO', SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
      WHEN others THEN
        var$error := TRUE;
        var$error_message := SQLERRM;
        INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
        VALUES (SYS_GUID(), 'Ocurrió un error al intentar obtener el OID_NIVEL_PARAMETRO con el código: 1 ERROR: ' || var$error_message, 'SGENERA_OID_LLAMADA', 'LOG_MOVIMIENTO', SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
      END;

      -- Busco el valor del parametro
      BEGIN
        SELECT PV.DES_VALOR_PARAMETRO
        INTO var$valor_parametro
        FROM GEPR_TPARAMETRO P
        INNER JOIN GEPR_TPARAMETRO_VALOR PV ON P.OID_PARAMETRO = PV.OID_PARAMETRO
        WHERE P.OID_NIVEL_PARAMETRO = var$oid_nivel_parametro 
        AND  P.OID_APLICACION = var$oid_apliacion 
        AND P.COD_PARAMETRO = var$cod_parametro
        AND PV.COD_IDENTIFICADOR_NIVEL = par$cod_pais;
      EXCEPTION WHEN no_data_found THEN
        var$error := TRUE;
        INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
        VALUES (SYS_GUID(), 'No existe un valor para el parametro de código "'|| var$cod_parametro || '" con el código de país "' || par$cod_pais || '"','SGENERA_OID_LLAMADA', 'LOG_MOVIMIENTO',SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
      WHEN others THEN 
        var$error := TRUE;
        var$error_message := SQLERRM;
        INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
        VALUES (SYS_GUID(), 'Ocurrió un error al intentar obtener el DES_VALOR_PARAMETRO para el código: '|| var$cod_parametro || ' ERROR: ' || var$error_message,'SGENERA_OID_LLAMADA', 'LOG_MOVIMIENTO',SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
      END;

      IF var$valor_parametro = '1' AND NOT var$error THEN
        IF par$oid_llamada IS NULL THEN
          par$oid_llamada := SYS_GUID();
        END IF;
      ELSE
          par$oid_llamada := NULL;
      END IF;
      COMMIT;
    EXCEPTION WHEN others THEN
      var$error_message := SQLERRM;
      DBMS_OUTPUT.PUT_LINE('Ocurrió una Excepción' || var$error_message);
    END;
  END SGENERA_OID_LLAMADA;

  PROCEDURE SINICIA_LLAMADA(par$oid_llamada           IN VARCHAR2,
                            par$des_recurso            IN VARCHAR2,
                            par$des_version           IN VARCHAR2,
                            par$des_datos_entrada     IN CLOB, 
                            par$cod_pais              IN VARCHAR2,
                            par$cod_hash_entrada      IN VARCHAR2,
                            par$des_host              IN VARCHAR2,
                            par$des_host_ip           IN VARCHAR2) IS
    PRAGMA AUTONOMOUS_TRANSACTION;
    var$error_message             VARCHAR2(512);
  BEGIN
    IF par$oid_llamada IS NOT NULL THEN
      BEGIN
        INSERT INTO SAPR_TLOG_API_LLAMADA (OID_LOG_API_LLAMADA, COD_PAIS, DES_RECURSO, DES_VERSION, DES_DATOS_ENTRADA, 
        COD_HASH_ENTRADA, FEC_LOG, FYH_LLAMADA_INICIO, DES_HOST, DES_HOST_IP)
        values (par$oid_llamada, par$cod_pais, par$des_recurso, par$des_version, par$des_datos_entrada, 
        par$cod_hash_entrada, TRUNC(SYS_EXTRACT_UTC(CURRENT_TIMESTAMP)), SYSTIMESTAMP, par$des_host, par$des_host_ip);
      EXCEPTION WHEN OTHERS THEN
        var$error_message := SQLERRM;
        BEGIN
          INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
          VALUES (SYS_GUID(), 'Ocurrió un error al intentar  realizar insert en SAPR_TLOG_API_LLAMADA con COD_PAIS: '|| par$cod_pais || ' y DES_RECURSO: ' || par$des_recurso || ' ERROR: ' || var$error_message,'SINICIA_LLAMADA','LOG_API',SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
        EXCEPTION WHEN OTHERS THEN
          var$error_message := SQLERRM;
          DBMS_OUTPUT.PUT_LINE('Ocurrió una Excepción' || var$error_message);
        END;
      END;
      COMMIT;
    END IF;
  END SINICIA_LLAMADA;

  PROCEDURE SFINALIZA_LLAMADA(par$oid_llamada           IN VARCHAR2,
                              par$des_datos_salida      IN CLOB, 
                              par$cod_resultado         IN VARCHAR2,
                              par$des_resultado         IN VARCHAR2,
                              par$cod_hash_salida       IN VARCHAR2) IS
    PRAGMA AUTONOMOUS_TRANSACTION;
    var$error_message             VARCHAR2(512);
  BEGIN
    IF par$oid_llamada IS NOT NULL THEN
      BEGIN
        UPDATE SAPR_TLOG_API_LLAMADA 
        SET DES_DATOS_SALIDA = par$des_datos_salida, FYH_LLAMADA_FIN = SYSTIMESTAMP, COD_RESULTADO = par$cod_resultado, DES_RESULTADO = par$des_resultado, COD_HASH_SALIDA = par$cod_hash_salida
        WHERE OID_LOG_API_LLAMADA = par$oid_llamada;
      EXCEPTION WHEN OTHERS THEN
        var$error_message := SQLERRM;
        BEGIN
          INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
          VALUES (SYS_GUID(), 'Ocurrió un error al intentar actualizar SAPR_TLOG_API_LLAMADA de OID_LOG_API_LLAMADA: ' || par$oid_llamada || ' ERROR: ' || var$error_message,'SFINALIZA_LLAMADA','LOG_API',SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
        EXCEPTION WHEN OTHERS THEN
          var$error_message := SQLERRM;
          DBMS_OUTPUT.PUT_LINE('Ocurrió una Excepción' || var$error_message);
        END;
      END;
      COMMIT;
    END IF;
  END SFINALIZA_LLAMADA; 

  PROCEDURE SAGREGA_DETALLE(par$oid_llamada           IN VARCHAR2,
                            par$des_origen            IN VARCHAR2,
                            par$des_version           IN VARCHAR2,                            
                            par$des_detalle           IN VARCHAR2,
                            par$cod_identificador     IN VARCHAR2) IS
    PRAGMA AUTONOMOUS_TRANSACTION;
    var$error_message             VARCHAR2(512);
    var$fec_log                   DATE;
  BEGIN
    IF par$oid_llamada IS NOT NULL THEN
      BEGIN
        SELECT FEC_LOG
          INTO var$fec_log
        FROM SAPR_TLOG_API_LLAMADA
        WHERE OID_LOG_API_LLAMADA = par$oid_llamada;

        INSERT INTO SAPR_TLOG_API_DETALLE (OID_LOG_API_DETALLE, OID_LOG_API_LLAMADA, DES_ORIGEN, DES_VERSION, DES_DETALLE, FEC_LOG, FYH_DETALLE, COD_IDENTIFICADOR)
        VALUES (SYS_GUID(), par$oid_llamada, par$des_origen, par$des_version, par$des_detalle, var$fec_log, SYSTIMESTAMP, par$cod_identificador);

      EXCEPTION
        WHEN OTHERS THEN
          var$error_message := SQLERRM;
          BEGIN
              INSERT INTO GEPR_TLOG_ERROR (OID_LOG_ERROR, DES_ERROR, DES_OTRO, COD_USUARIO, FYH_ERROR)
              VALUES (SYS_GUID(), 'Ocurrió un error al intentar insertar SAPR_TLOG_API_DETALLE de OID_LOG_API_LLAMADA:' || par$oid_llamada || ' ERROR: ' || var$error_message,'SAGREGA_DETALLE','LOG_API',SYS_EXTRACT_UTC(CURRENT_TIMESTAMP));
          EXCEPTION WHEN OTHERS THEN
            var$error_message := SQLERRM;
            DBMS_OUTPUT.PUT_LINE('Ocurrió una Excepción' || var$error_message);
          END;
      END;
      COMMIT;
    END IF;
  END SAGREGA_DETALLE;

  PROCEDURE SRECUPERA_DATOS(par$cod_pais              IN VARCHAR2,
                            par$cod_identificador     IN VARCHAR2,
                            par$oid_llamada           IN VARCHAR2,
                            par$recurso               IN VARCHAR2,
                            par$des_datos_entrada     IN VARCHAR2,
                            par$des_datos_salida      IN VARCHAR2,
                            par$cod_hash_entrada      IN VARCHAR2,
                            par$cod_hash_salida       IN VARCHAR2,
                            par$fyh_llamada_inicio    IN DATE,
                            par$fyh_llamada_fin       IN DATE,
                            par$des_host              IN VARCHAR2,
                            par$des_host_ip           IN VARCHAR2,
                            par$cur_llamadas          OUT sys_refcursor,
                            par$cur_detalles          OUT sys_refcursor) IS
  BEGIN
    OPEN par$cur_llamadas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$cur_detalles FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    IF par$cod_pais IS NOT NULL THEN
      OPEN par$cur_llamadas FOR
        SELECT
              M.OID_LOG_API_LLAMADA ,
              M.DES_RECURSO   ,
              M.DES_VERSION ,
              M.DES_DATOS_ENTRADA ,
              M.DES_DATOS_SALIDA ,
              M.COD_HASH_ENTRADA,
              M.COD_HASH_SALIDA,
              M.FYH_LLAMADA_INICIO ,
              M.FYH_LLAMADA_FIN ,
              M.COD_RESULTADO ,
              M.DES_RESULTADO,
              M.DES_HOST,
              M.DES_HOST_IP
            FROM
          SAPR_TLOG_API_LLAMADA  M
          WHERE M.COD_PAIS = par$cod_pais  
          AND (M.OID_LOG_API_LLAMADA = par$oid_llamada OR par$oid_llamada IS NULL)
          AND (M.DES_RECURSO = par$recurso OR par$recurso IS NULL)
          AND (M.COD_HASH_ENTRADA = par$cod_hash_entrada OR par$cod_hash_entrada IS NULL)
          AND (M.COD_HASH_SALIDA = par$cod_hash_salida OR par$cod_hash_salida IS NULL)
          AND (M.DES_DATOS_ENTRADA LIKE '%'|| par$des_datos_entrada || '%' OR par$des_datos_entrada IS NULL)
          AND (M.DES_DATOS_SALIDA LIKE '%'||  par$des_datos_salida  || '%' OR par$des_datos_salida IS NULL)
          AND (M.FYH_LLAMADA_INICIO >= par$fyh_llamada_inicio OR par$fyh_llamada_inicio IS NULL)
          AND (M.FYH_LLAMADA_FIN <= par$fyh_llamada_fin OR par$fyh_llamada_fin IS NULL)
          AND (M.DES_HOST = par$des_host OR par$des_host IS NULL)
          AND (M.DES_HOST_IP = par$des_host_ip OR par$des_host_ip IS NULL);

      OPEN par$cur_detalles FOR
        SELECT DISTINCT
              MD.OID_LOG_API_DETALLE ,
              MD.OID_LOG_API_LLAMADA ,
              MD.DES_ORIGEN ,
              MD.DES_VERSION ,
              MD.DES_DETALLE ,
              MD.FYH_DETALLE ,
              MD.COD_IDENTIFICADOR 
          FROM
          SAPR_TLOG_API_LLAMADA  M
          LEFT JOIN SAPR_TLOG_API_DETALLE MD ON M.OID_LOG_API_LLAMADA = MD.OID_LOG_API_LLAMADA 
          WHERE M.COD_PAIS = par$cod_pais 
          AND (M.OID_LOG_API_LLAMADA = par$oid_llamada OR par$oid_llamada IS NULL)
          AND (MD.COD_IDENTIFICADOR = par$cod_identificador OR par$cod_identificador IS NULL)
          AND (M.FYH_LLAMADA_INICIO >= par$fyh_llamada_inicio OR par$fyh_llamada_inicio IS NULL)
          AND (M.FYH_LLAMADA_FIN <= par$fyh_llamada_fin OR par$fyh_llamada_fin IS NULL);
    END IF;

  END SRECUPERA_DATOS;

END SAPR_PLOG_API;
/