CREATE OR REPLACE PACKAGE SAPR_PCERTIFICACION_###VERSION### IS
  PRAGMA SERIALLY_REUSABLE;

    /*Version: ###VERSION_COMP###*/

  VAR$OID_LOG       VARCHAR2(50);
  VAR$CULTURA       VARCHAR2(50);
  VAR$OID_EJECUCION VARCHAR2(36);

  PROCEDURE SRECUPERAR_CERTIFICADO(P_COD_CERTIFICADO IN VARCHAR2,
                                   P_CERTIFICADO     OUT SYS_REFCURSOR,
                                   P_ERROR           OUT VARCHAR2);

  PROCEDURE SEJECUTAR(P_IDENTIFICADOR              IN OUT VARCHAR2,
                      P_CODIGO                     IN OUT VARCHAR2,
                      P_CODIGO_CLIENTE             IN VARCHAR2,
                      P_CODIGO_EXTERNO             IN VARCHAR2,
                      P_FYH_CERTIFICADO            IN TIMESTAMP,
                      P_TODOS_SECTORES             IN INT,
                      P_TODOS_SUBCANALES           IN INT,
                      P_DESCRIPCION_USUARIO        IN VARCHAR2,
                      P_OID_SECTOR_CLAUSULA_IN     IN VARCHAR2,
                      P_OID_SUBCANAL_CLAUSULA_IN   IN VARCHAR2,
                      P_OID_DELEGACION_CLAUSULA_IN IN VARCHAR2,
                      P_COD_ESTADO                 IN VARCHAR2,
                      P_CULTURA                    IN VARCHAR2,
                      P_ERROR                      OUT VARCHAR2);

  PROCEDURE SGENERAR_CERTIFICADO(P_IDENTIFICADOR                IN OUT VARCHAR2,
                                 P_CODIGO                       IN VARCHAR2,
                                 P_IDENTIFICADOR_CONFIG_NIV_SAL IN VARCHAR2,
                                 P_CODIGO_EXTERNO               IN VARCHAR2,
                                 P_FYH_CERTIFICADO              IN TIMESTAMP,
                                 P_TODOS_SECTORES               IN INT,
                                 P_TODOS_SUBCANALES             IN INT,
                                 P_DESCRIPCION_USUARIO          IN VARCHAR2,
                                 P_CULTURA                      IN VARCHAR2,
                                 P_ERROR                        OUT VARCHAR2);

  PROCEDURE SANADIR_SECTOR(P_IDENTIFICADOR              IN OUT VARCHAR2,
                           P_CODIGO                     IN OUT VARCHAR2,
                           P_DESCRIPCION_USUARIO        IN VARCHAR2,
                           P_CULTURA                    IN VARCHAR2,
                           P_TODOS_SECTORES             IN INT,
                           P_OID_SECTOR_CLAUSULA_IN     IN VARCHAR2,
                           P_OID_DELEGACION_CLAUSULA_IN IN VARCHAR2,
                           P_ERROR                      OUT VARCHAR2);

  PROCEDURE SANADIR_DELEGACION(P_IDENTIFICADOR              IN OUT VARCHAR2,
                               P_CODIGO                     IN OUT VARCHAR2,
                               P_OID_DELEGACION_CLAUSULA_IN IN VARCHAR2,
                               P_DESCRIPCION_USUARIO        IN VARCHAR2,
                               P_CULTURA                    IN VARCHAR2,
                               P_ERROR                      OUT VARCHAR2);

  PROCEDURE SANADIR_SUBCANAL(P_IDENTIFICADOR            IN OUT VARCHAR2,
                             P_CODIGO                   IN OUT VARCHAR2,
                             P_DESCRIPCION_USUARIO      IN VARCHAR2,
                             P_CULTURA                  IN VARCHAR2,
                             P_TODOS_SUBCANALES         IN INT,
                             P_OID_SUBCANAL_CLAUSULA_IN IN VARCHAR2,
                             P_ERROR                    OUT VARCHAR2);

  PROCEDURE SBORRAR_CERTIFICADO(P_IDENTIFICADOR       IN OUT VARCHAR2,
                                P_CODIGO              IN OUT VARCHAR2,
                                P_DESCRIPCION_USUARIO IN VARCHAR2,
                                P_CULTURA             IN VARCHAR2,
                                P_ERROR               OUT VARCHAR2);

  PROCEDURE SCONVERTIR(P_IDENTIFICADOR            IN OUT VARCHAR2,
                       P_CODIGO                   IN OUT VARCHAR2,
                       P_CODIGO_ESTADO_CONVERSION IN VARCHAR2,
                       P_DESCRIPCION_USUARIO      IN VARCHAR2,
                       P_NUEVO_CODIGO             IN VARCHAR2,
                       P_CULTURA                  IN VARCHAR2,
                       P_BORRAR_DATOS_CUENTA      IN NUMBER DEFAULT 1,
                       P_ERROR                    OUT VARCHAR2);

  PROCEDURE SRETORNAR_CONFLITANTES(P_IDENTIFICADOR               IN VARCHAR2,
                                   P_BUSCAR_TIPO_CONSULTA        IN INT,
                                   P_BUSCAR_TIPO_PROV_SIN_CIERRE IN INT,
                                   P_BUSCAR_TIPO_PROV_CON_CIERRE IN INT,
                                   P_BUSCAR_TIPO_DEFINITIVO      IN INT,
                                   P_BUSCAR_TIPO_TEMPORARIO      IN INT,
                                   P_CERTIFICADOS_CONFLITANTES   OUT SYS_REFCURSOR,
                                   P_CULTURA                     IN VARCHAR2,
                                   P_ERROR                       OUT VARCHAR2);

  PROCEDURE SMARCAR_TRANSACCIONES(P_IDENTIFICADOR       IN VARCHAR2,
                                  P_DESCRIPCION_USUARIO IN VARCHAR2,
                                  P_ERROR               OUT VARCHAR2);

  PROCEDURE SMARCAR_TRANSAC_EFECTIVO(P_IDENTIFICADOR       IN VARCHAR2,
                                     P_DESCRIPCION_USUARIO IN VARCHAR2,
                                     P_ERROR               OUT VARCHAR2);

  PROCEDURE SMARCAR_TRANSAC_MEDIO_PAGO(P_IDENTIFICADOR       IN VARCHAR2,
                                       P_DESCRIPCION_USUARIO IN VARCHAR2,
                                       P_ERROR               OUT VARCHAR2);

  PROCEDURE SDESMARCAR_TRANSACCIONES(P_IDENTIFICADOR       IN VARCHAR2,
                                     P_DESCRIPCION_USUARIO IN VARCHAR2,
                                     P_ERROR               OUT VARCHAR2);

  PROCEDURE SRECUPERAR_FILTROS_COD_CERT(P_IDENTIFICADOR IN VARCHAR2,
                                        P_CULTURA       IN VARCHAR2,
                                        P_CERTIFICADO   OUT SYS_REFCURSOR,
                                        P_SECTORES      OUT SYS_REFCURSOR,
                                        P_SUBCANALES    OUT SYS_REFCURSOR,
                                        P_DELEGACIONES  OUT SYS_REFCURSOR,
                                        P_ERROR         OUT VARCHAR2);

  PROCEDURE SRECUPERAR_TRANSACCIONES(P_IDENTIFICADOR             IN OUT VARCHAR2,
                                     P_CODIGO                    IN OUT VARCHAR2,
                                     P_CULTURA                   IN VARCHAR2,
                                     P_TRANSACCIONES_EFECTIVO    OUT SYS_REFCURSOR,
                                     P_TRANSACCIONES_MEDIOS_PAGO OUT SYS_REFCURSOR,
                                     P_ERROR                     OUT VARCHAR2);

  PROCEDURE SRECUPERAR_SALDOS(P_IDENTIFICADOR      IN OUT VARCHAR2,
                              P_CODIGO             IN OUT VARCHAR2,
                              P_CULTURA            IN VARCHAR2,
                              P_SALDOS_EFECTIVO    OUT SYS_REFCURSOR,
                              P_SALDOS_MEDIOS_PAGO OUT SYS_REFCURSOR,
                              P_ERROR              OUT VARCHAR2);

  PROCEDURE SDEBUG(P_IDENTIFICADOR IN VARCHAR2, P_MENSAJE IN CLOB);

  PROCEDURE VALIDAR_GENERACION_REPORTE_URU(NOMBRE_PROCEDURE OUT VARCHAR2,
                                           P_ERROR          OUT VARCHAR2);

  PROCEDURE SRECUPERAR_CUENTAS(P_IDENTIFICADOR IN VARCHAR2,
                               P_ERROR         OUT VARCHAR2);

  PROCEDURE SVALIDAR_FECHA_CERT(par$fyh_plncertif      IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$oid_cuenta_origen  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_cuenta_destino IN gepr_pcomon_###VERSION###.tipo$oid_);

  PROCEDURE SANULAR_CERTIFICADO(P_IDENTIFICADOR IN VARCHAR2,
                                P_ERROR         OUT VARCHAR2);

END SAPR_PCERTIFICACION_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PCERTIFICACION_###VERSION### IS
  PRAGMA SERIALLY_REUSABLE;

  PROCEDURE SRECUPERAR_CERTIFICADO(P_COD_CERTIFICADO IN VARCHAR2,
                                   P_CERTIFICADO     OUT SYS_REFCURSOR,
                                   P_ERROR           OUT VARCHAR2)
  
   AS
  
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
  
  BEGIN
    INSERT INTO SAPR_GTT_TCERT_EJECUCION
      (OID_CERTIFICADO,
       COD_CERTIFICADO,
       COD_ESTADO,
       COD_EXTERNO,
       FYH_CERTIFICADO,
       OID_EJECUCION)
      SELECT DISTINCT CT.OID_CERTIFICADO,
                      CT.COD_CERTIFICADO,
                      CT.COD_ESTADO,
                      CT.COD_EXTERNO,
                      CT.FYH_CERTIFICADO,
                      VAR$OID_EJECUCION AS OID_EJECUCION
        FROM SAPR_TCERTIFICADO CT
       WHERE CT.COD_CERTIFICADO = P_COD_CERTIFICADO;
  
    OPEN P_CERTIFICADO FOR
      SELECT *
        FROM SAPR_GTT_TCERT_EJECUCION
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SRECUPERAR_CERTIFICADO:' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SRECUPERAR_CERTIFICADO;

  /* IN OUT SYS_REFCURSOR */
  PROCEDURE SEJECUTAR(P_IDENTIFICADOR              IN OUT VARCHAR2,
                      P_CODIGO                     IN OUT VARCHAR2,
                      P_CODIGO_CLIENTE             IN VARCHAR2,
                      P_CODIGO_EXTERNO             IN VARCHAR2,
                      P_FYH_CERTIFICADO            IN TIMESTAMP,
                      P_TODOS_SECTORES             IN INT,
                      P_TODOS_SUBCANALES           IN INT,
                      P_DESCRIPCION_USUARIO        IN VARCHAR2,
                      P_OID_SECTOR_CLAUSULA_IN     IN VARCHAR2,
                      P_OID_SUBCANAL_CLAUSULA_IN   IN VARCHAR2,
                      P_OID_DELEGACION_CLAUSULA_IN IN VARCHAR2,
                      P_COD_ESTADO                 IN VARCHAR2,
                      P_CULTURA                    IN VARCHAR2,
                      P_ERROR                      OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE       NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE    VARCHAR2(32767) := '';
    V_EXISTE                NUMERIC := 0;
    CURSOR_CERT_CONFLITANTE SYS_REFCURSOR;
    V_OID_CERTIFICADO       VARCHAR2(50);
    V_OID_NIVEL_SALDO       VARCHAR2(50);
    V_COD_CLIENTE           VARCHAR2(50);
    V_DES_CLIENTE           VARCHAR2(200);
  
  BEGIN
    VAR$CULTURA       := P_CULTURA;
    VAR$OID_EJECUCION := SYS_GUID();
  
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;
  
    DELETE FROM SAPR_GTT_TCERT_EJECUCION
     WHERE OID_EJECUCION = VAR$OID_EJECUCION;
    DELETE FROM SAPR_GTT_TCUENTA_CERTIFICADO
     WHERE OID_EJECUCION = VAR$OID_EJECUCION;
    COMMIT;
  
    /* SE FOR CERTIFICADO DEFINITIVO, SIMPLISMENTE VAMOS FAZER A CONVERSAO DO  CERTIFICADO */
    IF P_COD_ESTADO = 'DE' THEN
    
      /* RECUPERA O CERTIFICADO PROVISIONAL CON CIERRE */
      SRECUPERAR_CERTIFICADO(P_CODIGO_EXTERNO,
                             CURSOR_CERT_CONFLITANTE,
                             P_ERROR);
    
      /* VALIDA SE EXISTE CERTIFICADO PROVISIONAL CON CIERRE */
      SELECT COUNT(*)
        INTO V_EXISTE
        FROM SAPR_GTT_TCERT_EJECUCION
       WHERE COD_ESTADO = 'PC'
         AND OID_EJECUCION = VAR$OID_EJECUCION;
    
      IF V_EXISTE = 1 THEN
      
        SELECT OID_CERTIFICADO
          INTO V_OID_CERTIFICADO
          FROM SAPR_GTT_TCERT_EJECUCION
         WHERE COD_ESTADO = 'PC'
           AND OID_EJECUCION = VAR$OID_EJECUCION;
      
        /* CONVERTE O CERTIFICADO PROVISIONAL CON CIERRE PARA DEFINITIVO */
        SCONVERTIR(V_OID_CERTIFICADO,
                   P_CODIGO,
                   P_COD_ESTADO,
                   P_DESCRIPCION_USUARIO,
                   P_CODIGO,
                   P_CULTURA,
                   0,
                   P_ERROR);
      
      ELSE
      
        /* SE NAO ENCONTROU CERTIFICADO PROVISIONAL CON CIERRE, RETORNA O ERRO QUE NAO FOI POSSIVEL GERAR O CERTIFICADO */
        CUSTOM_ERROR_CODE    := -20001;
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_CON_CIERRE_NO_ENCONTRADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            NULL,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
    
    ELSE
    
      SELECT COUNT(*)
        INTO V_EXISTE
        FROM SAPR_TCONFIG_NIVEL_SALDO CNS
       INNER JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CNS.OID_CLIENTE
         AND CNS.OID_SUBCLIENTE IS NULL
         AND CNS.OID_PTO_SERVICIO IS NULL
       WHERE CL.COD_CLIENTE = P_CODIGO_CLIENTE
         AND ROWNUM = 1;
    
      IF V_EXISTE > 0 THEN
      
        SELECT DISTINCT CNS.OID_CONFIG_NIVEL_SALDO
          INTO V_OID_NIVEL_SALDO
          FROM SAPR_TCONFIG_NIVEL_SALDO CNS
         INNER JOIN GEPR_TCLIENTE CL
            ON CL.OID_CLIENTE = CNS.OID_CLIENTE
           AND CNS.OID_SUBCLIENTE IS NULL
           AND CNS.OID_PTO_SERVICIO IS NULL
         WHERE CL.COD_CLIENTE = P_CODIGO_CLIENTE
           AND ROWNUM = 1;
      
      ELSE
        SDEBUG(SYS_GUID(), P_CODIGO_CLIENTE);
        CUSTOM_ERROR_CODE    := -20001;
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CONFIG_NIVEL_SALDO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            NULL,
                                            1);
        RAISE CUSTOM_ERROR;
      
      END IF;
    
      /* GERA O CERTIFICADO TEMPORARIO */
      SGENERAR_CERTIFICADO(P_IDENTIFICADOR,
                           P_CODIGO,
                           V_OID_NIVEL_SALDO,
                           P_CODIGO,
                           P_FYH_CERTIFICADO,
                           P_TODOS_SECTORES,
                           P_TODOS_SUBCANALES,
                           P_DESCRIPCION_USUARIO,
                           P_CULTURA,
                           P_ERROR);
    
      /* ADICIONA OS SETORES AO CERTIFICADO TEMPORARIO */
      SANADIR_SECTOR(P_IDENTIFICADOR,
                     P_CODIGO,
                     P_DESCRIPCION_USUARIO,
                     P_CULTURA,
                     P_TODOS_SECTORES,
                     P_OID_SECTOR_CLAUSULA_IN,
                     P_OID_DELEGACION_CLAUSULA_IN,
                     P_ERROR);
    
      SANADIR_SUBCANAL(P_IDENTIFICADOR,
                       P_CODIGO,
                       P_DESCRIPCION_USUARIO,
                       P_CULTURA,
                       P_TODOS_SUBCANALES,
                       P_OID_SUBCANAL_CLAUSULA_IN,
                       P_ERROR);
    
      SANADIR_DELEGACION(P_IDENTIFICADOR,
                         P_CODIGO,
                         P_OID_DELEGACION_CLAUSULA_IN,
                         P_DESCRIPCION_USUARIO,
                         P_CULTURA,
                         P_ERROR);
    
      COMMIT;
    
      /* INICIO GRAVAR FILTRO */
      /* P_FYH_CERTIFICADO */
      INSERT INTO SAPR_THIST_CERTIFICADO
        (OID_CERTIFICADO,
         COD_CERTIFICADO,
         COD_FILTRO,
         VALOR_FILTRO,
         GMT_CREACION)
      VALUES
        (P_IDENTIFICADOR,
         P_CODIGO,
         'P_FYH_CERTIFICADO',
         P_FYH_CERTIFICADO,
         SYSTIMESTAMP);
    
      /* OID_DELEGACION */
      INSERT INTO SAPR_THIST_CERTIFICADO
        (OID_CERTIFICADO,
         COD_CERTIFICADO,
         COD_FILTRO,
         VALOR_FILTRO,
         GMT_CREACION)
        SELECT P_IDENTIFICADOR,
               P_CODIGO,
               'OID_DELEGACION',
               OID_DELEGACION,
               SYSTIMESTAMP
          FROM SAPR_TCERTIFICADOXDELEGACION
         WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* OID_SUBCANAL */
      INSERT INTO SAPR_THIST_CERTIFICADO
        (OID_CERTIFICADO,
         COD_CERTIFICADO,
         COD_FILTRO,
         VALOR_FILTRO,
         GMT_CREACION)
        SELECT P_IDENTIFICADOR,
               P_CODIGO,
               'OID_SUBCANAL',
               OID_SUBCANAL,
               SYSTIMESTAMP
          FROM SAPR_TCERTIFICADOXSUBCANAL
         WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* OID_SECTOR */
      INSERT INTO SAPR_THIST_CERTIFICADO
        (OID_CERTIFICADO,
         COD_CERTIFICADO,
         COD_FILTRO,
         VALOR_FILTRO,
         GMT_CREACION)
        SELECT P_IDENTIFICADOR,
               P_CODIGO,
               'OID_SECTOR',
               OID_SECTOR,
               SYSTIMESTAMP
          FROM SAPR_TCERTIFICADOXSECTOR
         WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
      /* FIN GRAVAR FILTRO */
      COMMIT;
    
      IF P_COD_ESTADO = 'PC' OR P_COD_ESTADO = 'PS' THEN
      
        /* RECUPERA OS CERTIFICADOS CONFLITANTES */
        SRETORNAR_CONFLITANTES(P_IDENTIFICADOR,
                               0,
                               1,
                               1,
                               1,
                               1,
                               CURSOR_CERT_CONFLITANTE,
                               P_CULTURA,
                               P_ERROR);
      
      ELSE
      
        /* RECUPERA OS CERTIFICADOS CONFLITANTES */
        SRETORNAR_CONFLITANTES(P_IDENTIFICADOR,
                               1,
                               0,
                               0,
                               1,
                               1,
                               CURSOR_CERT_CONFLITANTE,
                               P_CULTURA,
                               P_ERROR);
      
      END IF;
    
      /* VALIDA SE EXISTE CERTIFICADO TEMPORARIO */
      SELECT COUNT(*)
        INTO V_EXISTE
        FROM SAPR_GTT_TCERT_EJECUCION CT
       WHERE CT.COD_ESTADO = 'TP'
         AND CT.OID_EJECUCION = VAR$OID_EJECUCION;
    
      /* SE EXISTIR INFORMA QUE JÁ ESTÁ GERANDO UM CERTIFICADO TEMPORÁRIO COM OS MESMOS FILTROS */
      IF V_EXISTE > 0 THEN
      
        BEGIN
          /* RECUPERA O CLIENTE */
          SELECT CL.COD_CLIENTE, CL.DES_CLIENTE
            INTO V_COD_CLIENTE, V_DES_CLIENTE
            FROM SAPR_GTT_TCERT_EJECUCION CT
           INNER JOIN SAPR_TCERTIFICADO C
              ON C.OID_CERTIFICADO = CT.OID_CERTIFICADO
           INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
              ON CNS.OID_CONFIG_NIVEL_SALDO = C.OID_CONFIG_NIVEL_SALDO
           INNER JOIN GEPR_TCLIENTE CL
              ON CL.OID_CLIENTE = CNS.OID_CLIENTE
           WHERE CT.COD_ESTADO = 'TP'
             AND CT.OID_EJECUCION = VAR$OID_EJECUCION;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            V_COD_CLIENTE := NULL;
            V_DES_CLIENTE := NULL;
        END;
      
        CUSTOM_ERROR_CODE    := -20001;
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_EJECUCION',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            V_COD_CLIENTE || '|' ||
                                            V_DES_CLIENTE,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
    
      /* VALIDA SE EXISTE CERTIFICADO DEFINITIVO */
      SELECT COUNT(*)
        INTO V_EXISTE
        FROM SAPR_GTT_TCERT_EJECUCION CT
       WHERE CT.COD_ESTADO = 'DE'
         AND CT.OID_EJECUCION = VAR$OID_EJECUCION
         AND CT.FYH_CERTIFICADO >= P_FYH_CERTIFICADO;
    
      IF V_EXISTE > 0 THEN
      
        /* APAGA O CERTIFICADO TEMPORARIO POIS JA EXISTE UM DEFINITIVO */
        SBORRAR_CERTIFICADO(P_IDENTIFICADOR,
                            P_CODIGO,
                            P_DESCRIPCION_USUARIO,
                            P_CULTURA,
                            P_ERROR);
      
        /* SE NAO ENCONTROU CERTIFICADO PROVISIONAL CON CIERRE, RETORNA O ERRO QUE NAO FOI POSSIVEL GERAR O CERTIFICADO */
        CUSTOM_ERROR_CODE    := -20001;
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_PERIODO_CERTIFICADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            NULL,
                                            1);
        RAISE CUSTOM_ERROR;
      
      ELSE
      
        BEGIN
        
          FOR CC IN (SELECT OID_CERTIFICADO, COD_CERTIFICADO, COD_ESTADO
                       FROM SAPR_GTT_TCERT_EJECUCION
                      WHERE OID_EJECUCION = VAR$OID_EJECUCION) LOOP
          
            IF CC.COD_ESTADO <> 'DE' THEN
              /* APAGA OS CERTIFICADOS CONFLITANTES */
              SBORRAR_CERTIFICADO(CC.OID_CERTIFICADO,
                                  CC.COD_CERTIFICADO,
                                  P_DESCRIPCION_USUARIO,
                                  P_CULTURA,
                                  P_ERROR);
            
            END IF;
          
          END LOOP;
        
        EXCEPTION
          WHEN OTHERS THEN
            ROLLBACK;
          
            /* APAGA O CERTIFICADO TEMPORARIO */
            SELECT COUNT(0)
              INTO V_EXISTE
              FROM SAPR_TCERTIFICADO C
             WHERE C.OID_CERTIFICADO = P_IDENTIFICADOR
               AND C.COD_ESTADO = 'TP';
            IF V_EXISTE > 0 THEN
              SBORRAR_CERTIFICADO(P_IDENTIFICADOR,
                                  P_CODIGO,
                                  P_DESCRIPCION_USUARIO,
                                  P_CULTURA,
                                  P_ERROR);
              COMMIT;
            END IF;
          
            RAISE;
        END;
      
        /* CONVERTE O CERTIFICADO */
        SCONVERTIR(P_IDENTIFICADOR,
                   P_CODIGO,
                   P_COD_ESTADO,
                   P_DESCRIPCION_USUARIO,
                   P_CODIGO,
                   P_CULTURA,
                   0,
                   P_ERROR);
      END IF;
    
    END IF;

    DELETE FROM SAPR_GTT_TCERT_EJECUCION
     WHERE OID_EJECUCION = VAR$OID_EJECUCION;
    DELETE FROM SAPR_GTT_TCUENTA_CERTIFICADO
     WHERE OID_EJECUCION = VAR$OID_EJECUCION;
    COMMIT;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
    
      SANULAR_CERTIFICADO(P_IDENTIFICADOR => P_IDENTIFICADOR,
                          P_ERROR         => P_ERROR);
    
      P_ERROR := CUSTOM_ERROR_MESSAGE;
    
      ROLLBACK;
      DELETE FROM SAPR_GTT_TCERT_EJECUCION
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      DELETE FROM SAPR_GTT_TCUENTA_CERTIFICADO
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      COMMIT;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, P_ERROR);
    WHEN OTHERS THEN
    
      SANULAR_CERTIFICADO(P_IDENTIFICADOR => P_IDENTIFICADOR,
                          P_ERROR         => P_ERROR);
    
      P_ERROR := 'ERROR: SEJECUTAR:' || SQLCODE || ' - ' || SQLERRM;
      ROLLBACK;
      DELETE FROM SAPR_GTT_TCERT_EJECUCION
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      DELETE FROM SAPR_GTT_TCUENTA_CERTIFICADO
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      COMMIT;
    
      SDEBUG(SYS_GUID, P_ERROR);
      IF SQLCODE <> -20001 THEN
        RAISE_APPLICATION_ERROR(-20001, P_ERROR);
      END IF;
  END SEJECUTAR;

  PROCEDURE SGENERAR_CERTIFICADO(P_IDENTIFICADOR                IN OUT VARCHAR2,
                                 P_CODIGO                       IN VARCHAR2,
                                 P_IDENTIFICADOR_CONFIG_NIV_SAL IN VARCHAR2,
                                 P_CODIGO_EXTERNO               IN VARCHAR2,
                                 P_FYH_CERTIFICADO              IN TIMESTAMP,
                                 P_TODOS_SECTORES               IN INT,
                                 P_TODOS_SUBCANALES             IN INT,
                                 P_DESCRIPCION_USUARIO          IN VARCHAR2,
                                 P_CULTURA                      IN VARCHAR2,
                                 P_ERROR                        OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_EXISTE             NUMERIC := 0;
  BEGIN
  
    /* VERIFICA SI FUE INFORMADO UNO IDENTIFICADOR PARA EL CERTIFICADO */
    /* SI NO LO FUE, SE GENERA UNO */
    IF P_IDENTIFICADOR IS NULL THEN
      SELECT SYS_GUID() INTO P_IDENTIFICADOR FROM DUAL;
    END IF;
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;

  
    /* VERIFICA SI YA EXISTE UNO CERTIFICADO CON EL MISMO CODIGO INFORMADO */
    SELECT COUNT(1)
      INTO V_EXISTE
      FROM SAPR_TCERTIFICADO
     WHERE COD_CERTIFICADO = P_CODIGO;
    IF V_EXISTE > 0 THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'NO FUE POSIBLE GENERAR EL CERTIFICADO. YA EXISTE EN LA BASE DE DATOS UN CERTIFICADO CON EL CODIGO "' || P_CODIGO || '"'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_CERTIFICADO_EXISTE',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          P_CODIGO,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* VERIFICA SI YA EXISTE UNO CERTIFICADO CON EL MISMO CODIGO EXTERNO INFORMADO */
    SELECT COUNT(1)
      INTO V_EXISTE
      FROM SAPR_TCERTIFICADO
     WHERE COD_EXTERNO = P_CODIGO_EXTERNO;
    IF V_EXISTE > 0 THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'NO FUE POSIBLE GENERAR EL CERTIFICADO. YA EXISTE EN LA BASE DE DATOS UN CERTIFICADO CON EL CODIGO EXTERNO "' || P_CODIGO_EXTERNO || '"'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_COD_EXTERNO_EXISTE',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          P_CODIGO_EXTERNO,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* VERIFICA SI LA CONFIGURACION DE NIVEL DE SALDO INFORMADA ES VALIDA (SOLAMENTE EN NIVEL DEL CLIENTE) */
    SELECT COUNT(1)
      INTO V_EXISTE
      FROM SAPR_TCONFIG_NIVEL_SALDO
     WHERE OID_CONFIG_NIVEL_SALDO = P_IDENTIFICADOR_CONFIG_NIV_SAL
       AND OID_SUBCLIENTE IS NULL
       AND OID_PTO_SERVICIO IS NULL;
    IF V_EXISTE = 0 THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'NO FUE POSIBLE GENERAR EL CERTIFICADO. LA CONFIGURACION DE NIVEL DE SALDO UTILIZADA DEBE SER SOLAMENTE EN EL NIVEL DEL CLIENTE'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_CONFIGURACION_NIVEL_SALDO',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          NULL,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* GRABA EL CERTIFICADO CON EL ESTADO 'TP' (TEMPORARIO)
    PARA QUE SE PUEDA GRABAR LOS DEMAS DATOS ANTES DE SEGUIR
    CON EL FLUJO DE CERTIFICACION */
    INSERT INTO SAPR_TCERTIFICADO
      (OID_CERTIFICADO,
       OID_CONFIG_NIVEL_SALDO,
       COD_CERTIFICADO,
       COD_ESTADO,
       COD_EXTERNO,
       FYH_CERTIFICADO,
       BOL_TODOS_SECTORES,
       BOL_TODOS_CANALES,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION)
    VALUES
      (P_IDENTIFICADOR,
       P_IDENTIFICADOR_CONFIG_NIV_SAL,
       P_CODIGO,
       'TP',
       P_CODIGO_EXTERNO,
       P_FYH_CERTIFICADO,
       P_TODOS_SECTORES,
       P_TODOS_SUBCANALES,
       SYSDATE,
       P_DESCRIPCION_USUARIO,
       SYSDATE,
       P_DESCRIPCION_USUARIO);
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
    
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SGENERAR_CERTIFICADO:' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SGENERAR_CERTIFICADO;

  PROCEDURE SANADIR_SECTOR(P_IDENTIFICADOR              IN OUT VARCHAR2,
                           P_CODIGO                     IN OUT VARCHAR2,
                           P_DESCRIPCION_USUARIO        IN VARCHAR2,
                           P_CULTURA                    IN VARCHAR2,
                           P_TODOS_SECTORES             IN INT,
                           P_OID_SECTOR_CLAUSULA_IN     IN VARCHAR2,
                           P_OID_DELEGACION_CLAUSULA_IN IN VARCHAR2,
                           P_ERROR                      OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_EXISTE             NUMERIC := 0;
    V_COD_ESTADO         VARCHAR2(2) := '';
  BEGIN
  
    IF P_IDENTIFICADOR IS NULL THEN
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE COD_CERTIFICADO = P_CODIGO;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_CODIGO,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.OID_CERTIFICADO, CERT.COD_ESTADO
        INTO P_IDENTIFICADOR, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.COD_CERTIFICADO = P_CODIGO;
    ELSE
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_IDENTIFICADOR || '" EN EL PARAMETRO "P_IDENTIFICADOR"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO_IDENTIFICADOR',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_IDENTIFICADOR,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.COD_CERTIFICADO, CERT.COD_ESTADO
        INTO P_CODIGO, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
    END IF;
  
    /* VERIFICA SI SE PUEDE ANADIR EL SECTOR */
    IF V_COD_ESTADO <> 'TP' THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'NO SE PUEDE CAMBIAR LOS SECTORES DE UNO CERTIFICADO QUE NO SEA TEMPORARIO'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_CAMBIAR_SECTORES_CERT_TEMP',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          NULL,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* EXCLUI OS SECTORES DO CERTIFICADO */
    DELETE FROM SAPR_TCERTIFICADOXSECTOR
     WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
  
    IF P_TODOS_SECTORES = 1 THEN
      INSERT INTO SAPR_TCERTIFICADOXSECTOR
        (OID_CERTIFICADOXSECTOR,
         OID_CERTIFICADO,
         OID_SECTOR,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION)
        SELECT SYS_GUID(),
               P_IDENTIFICADOR,
               SEC.OID_SECTOR,
               SYSDATE,
               P_DESCRIPCION_USUARIO,
               SYSDATE,
               P_DESCRIPCION_USUARIO
          FROM GEPR_TSECTOR SEC
         INNER JOIN GEPR_TPLANTA PLA
            ON PLA.OID_PLANTA = SEC.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION DEL
            ON DEL.OID_DELEGACION = PLA.OID_DELEGACION
         WHERE SEC.BOL_ACTIVO = 1
           AND PLA.BOL_ACTIVO = 1
           AND DEL.COD_DELEGACION IN
               (SELECT C.OID_ITEMS
                  FROM GEPR_TCLAUSULA_IN C
                 WHERE C.OID_CLAUSULA_IN = P_OID_DELEGACION_CLAUSULA_IN);
    ELSE
      INSERT INTO SAPR_TCERTIFICADOXSECTOR
        (OID_CERTIFICADOXSECTOR,
         OID_CERTIFICADO,
         OID_SECTOR,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION)
        SELECT SYS_GUID(),
               P_IDENTIFICADOR,
               SEC.OID_SECTOR,
               SYSDATE,
               P_DESCRIPCION_USUARIO,
               SYSDATE,
               P_DESCRIPCION_USUARIO
          FROM GEPR_TSECTOR SEC
         WHERE SEC.BOL_ACTIVO = 1
           AND SEC.COD_SECTOR IN
               (SELECT C.OID_ITEMS
                  FROM GEPR_TCLAUSULA_IN C
                 WHERE C.OID_CLAUSULA_IN = P_OID_SECTOR_CLAUSULA_IN);
    END IF;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SANADIR_SECTOR: ' || SQLCODE || ' - ' || SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SANADIR_SECTOR;

  PROCEDURE SANADIR_DELEGACION(P_IDENTIFICADOR              IN OUT VARCHAR2,
                               P_CODIGO                     IN OUT VARCHAR2,
                               P_OID_DELEGACION_CLAUSULA_IN IN VARCHAR2,
                               P_DESCRIPCION_USUARIO        IN VARCHAR2,
                               P_CULTURA                    IN VARCHAR2,
                               P_ERROR                      OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_EXISTE             NUMERIC := 0;
    V_COD_ESTADO         VARCHAR2(2) := '';
  BEGIN
  
    IF P_IDENTIFICADOR IS NULL THEN
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE COD_CERTIFICADO = P_CODIGO;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_CODIGO,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.OID_CERTIFICADO, CERT.COD_ESTADO
        INTO P_IDENTIFICADOR, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.COD_CERTIFICADO = P_CODIGO;
    ELSE
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_IDENTIFICADOR || '" EN EL PARAMETRO "P_IDENTIFICADOR"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO_IDENTIFICADOR',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_IDENTIFICADOR,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.COD_CERTIFICADO, CERT.COD_ESTADO
        INTO P_CODIGO, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
    END IF;
  
    /* VERIFICA SI SE PUEDE ANADIR EL SECTOR */
    IF V_COD_ESTADO <> 'TP' THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'NO SE PUEDE CAMBIAR LOS SECTORES DE UNO CERTIFICADO QUE NO SEA TEMPORARIO'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_CAMBIAR_SECTORES_CERT_TEMP',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          NULL,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* EXCLUI AS DELEGACIONES DO CERTIFICADO */
    DELETE FROM SAPR_TCERTIFICADOXDELEGACION CS
     WHERE CS.OID_CERTIFICADO = P_IDENTIFICADOR;
  
    INSERT INTO SAPR_TCERTIFICADOXDELEGACION
      (OID_CERTIFICADOXDELEGACION,
       OID_CERTIFICADO,
       OID_DELEGACION,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION)
      SELECT SYS_GUID(),
             P_IDENTIFICADOR,
             D.OID_DELEGACION,
             SYSDATE,
             P_DESCRIPCION_USUARIO,
             SYSDATE,
             P_DESCRIPCION_USUARIO
        FROM GEPR_TDELEGACION D
       WHERE D.COD_DELEGACION IN
             (SELECT C.OID_ITEMS
                FROM GEPR_TCLAUSULA_IN C
               WHERE C.OID_CLAUSULA_IN = P_OID_DELEGACION_CLAUSULA_IN);
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SANADIR_DELEGACION: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SANADIR_DELEGACION;

  PROCEDURE SANADIR_SUBCANAL(P_IDENTIFICADOR            IN OUT VARCHAR2,
                             P_CODIGO                   IN OUT VARCHAR2,
                             P_DESCRIPCION_USUARIO      IN VARCHAR2,
                             P_CULTURA                  IN VARCHAR2,
                             P_TODOS_SUBCANALES         IN INT,
                             P_OID_SUBCANAL_CLAUSULA_IN IN VARCHAR2,
                             P_ERROR                    OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_EXISTE             NUMERIC := 0;
    V_COD_ESTADO         VARCHAR2(2) := '';
  BEGIN
  
    IF P_IDENTIFICADOR IS NULL THEN
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE COD_CERTIFICADO = P_CODIGO;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_CODIGO,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.OID_CERTIFICADO, CERT.COD_ESTADO
        INTO P_IDENTIFICADOR, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.COD_CERTIFICADO = P_CODIGO;
    ELSE
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_IDENTIFICADOR || '" EN EL PARAMETRO "P_IDENTIFICADOR"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO_IDENTIFICADOR',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_IDENTIFICADOR,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.COD_CERTIFICADO, CERT.COD_ESTADO
        INTO P_CODIGO, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
    END IF;
  
    /* VERIFICA SI SE PUEDE ANADIR EL SUBCANAL */
    IF V_COD_ESTADO <> 'TP' THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'NO SE PUEDE CAMBIAR LOS SECTORES DE UNO CERTIFICADO QUE NO SEA TEMPORARIO'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_CAMBIAR_SECTORES_CERT_TEMP',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          NULL,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* EXCLUI OS SUBCANAIS DO CERTIFICADO */
    DELETE FROM SAPR_TCERTIFICADOXSUBCANAL CS
     WHERE CS.OID_CERTIFICADO = P_IDENTIFICADOR;
  
    IF P_TODOS_SUBCANALES = 1 THEN
      INSERT INTO SAPR_TCERTIFICADOXSUBCANAL
        (OID_CERTIFICADOXSUBCANAL,
         OID_CERTIFICADO,
         OID_SUBCANAL,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION)
        SELECT SYS_GUID(),
               P_IDENTIFICADOR,
               SB.OID_SUBCANAL,
               SYSDATE,
               P_DESCRIPCION_USUARIO,
               SYSDATE,
               P_DESCRIPCION_USUARIO
          FROM GEPR_TSUBCANAL SB
         WHERE SB.BOL_VIGENTE = 1;
    ELSE
      INSERT INTO SAPR_TCERTIFICADOXSUBCANAL
        (OID_CERTIFICADOXSUBCANAL,
         OID_CERTIFICADO,
         OID_SUBCANAL,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION)
        SELECT SYS_GUID(),
               P_IDENTIFICADOR,
               SB.OID_SUBCANAL,
               SYSDATE,
               P_DESCRIPCION_USUARIO,
               SYSDATE,
               P_DESCRIPCION_USUARIO
          FROM GEPR_TSUBCANAL SB
         WHERE SB.BOL_VIGENTE = 1
           AND SB.COD_SUBCANAL IN
               (SELECT C.OID_ITEMS
                  FROM GEPR_TCLAUSULA_IN C
                 WHERE C.OID_CLAUSULA_IN = P_OID_SUBCANAL_CLAUSULA_IN);
    END IF;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SANADIR_SUBCANAL: ' || SQLCODE || ' - ' || SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SANADIR_SUBCANAL;

  PROCEDURE SBORRAR_CERTIFICADO(P_IDENTIFICADOR       IN OUT VARCHAR2,
                                P_CODIGO              IN OUT VARCHAR2,
                                P_DESCRIPCION_USUARIO IN VARCHAR2,
                                P_CULTURA             IN VARCHAR2,
                                P_ERROR               OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_EXISTE             NUMERIC := 0;
    V_COD_ESTADO         VARCHAR2(2) := '';
  BEGIN
  
    IF P_IDENTIFICADOR IS NULL THEN
    
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE COD_CERTIFICADO = P_CODIGO;
    
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_CODIGO,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
    
      /* DETALLES */
      SELECT CERT.OID_CERTIFICADO, CERT.COD_ESTADO
        INTO P_IDENTIFICADOR, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.COD_CERTIFICADO = P_CODIGO;
    
    ELSE
    
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_IDENTIFICADOR || '" EN EL PARAMETRO "P_IDENTIFICADOR"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO_IDENTIFICADOR',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_IDENTIFICADOR,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
    
      /* DETALLES */
      SELECT CERT.COD_CERTIFICADO, CERT.COD_ESTADO
        INTO P_CODIGO, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
    
    END IF;
  
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;

  
    /* VERIFICA SI EL CERTIFICANO ES DEFINITIVO */
    IF V_COD_ESTADO = 'DE' THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'NO SE PUEDE BORRAR UNO CERTIFICADO CON EL ESTADO DEFINITIVO'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_BORRAR_CERTIFICADO_DEFINITIVO',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          NULL,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* DESMARCAR LAS POSIBLES TRANSACCIONES */
    SDESMARCAR_TRANSACCIONES(P_IDENTIFICADOR,
                             P_DESCRIPCION_USUARIO,
                             P_ERROR);
  
    /* BORRA LOS FILTROS */
    DELETE FROM SAPR_TCERTIFICADOXSECTOR
     WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    DELETE FROM SAPR_TCERTIFICADOXSUBCANAL
     WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    DELETE FROM SAPR_TCERTIFICADOXDELEGACION
     WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    DELETE FROM SAPR_TCERTIFICADO WHERE OID_CERTIFICADO = P_IDENTIFICADOR;

  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SBORRAR_CERTIFICADO: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SBORRAR_CERTIFICADO;

  PROCEDURE SCONVERTIR(P_IDENTIFICADOR            IN OUT VARCHAR2,
                       P_CODIGO                   IN OUT VARCHAR2,
                       P_CODIGO_ESTADO_CONVERSION IN VARCHAR2,
                       P_DESCRIPCION_USUARIO      IN VARCHAR2,
                       P_NUEVO_CODIGO             IN VARCHAR2,
                       P_CULTURA                  IN VARCHAR2,
                       P_BORRAR_DATOS_CUENTA      IN NUMBER DEFAULT 1,
                       P_ERROR                    OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE       NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE    VARCHAR2(32767) := '';
    V_EXISTE                NUMERIC := 0;
    V_COD_ESTADO            VARCHAR2(2) := '';
    V_COD_EXTERNO           VARCHAR2(500) := '';
    V_COD_CERTIFICADO       VARCHAR2(500) := '';
    V_NOMBRE_PROCEDURE      VARCHAR2(200) := '';
    V_SQL                   VARCHAR2(4000) := '';
    CURSOR_CERT_CONFLITANTE SYS_REFCURSOR;
    V_COD_CLIENTE           VARCHAR2(50);
    V_DES_CLIENTE           VARCHAR2(200);
  
    CURSOR CURSOR_SUBCANALES_DELEGACIONES IS
      SELECT DISTINCT C.COD_DELEGACION,
                      C.COD_SUBCANAL,
                      C.COD_CLIENTE,
                      CC.FYH_CERTIFICADO
        FROM SAPR_GTT_TCUENTA_CERTIFICADO CC
       INNER JOIN SAPR_VCUENTA C
          ON C.OID_CUENTA = CC.OID_CUENTA_SALDO
       WHERE CC.OID_EJECUCION = VAR$OID_EJECUCION;
  
  BEGIN
    VAR$CULTURA := P_CULTURA;
  
    IF P_BORRAR_DATOS_CUENTA = 1 THEN
      VAR$OID_EJECUCION := SYS_GUID();
    
      DELETE FROM SAPR_GTT_TCUENTA_CERTIFICADO
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
    END IF;
  
    VALIDAR_GENERACION_REPORTE_URU(V_NOMBRE_PROCEDURE, P_ERROR);
  
    IF P_IDENTIFICADOR IS NULL THEN
    
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE COD_CERTIFICADO = P_CODIGO;
    
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_CODIGO,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
    
      /* DETALLES */
      SELECT CERT.OID_CERTIFICADO,
             CERT.COD_ESTADO,
             CERT.COD_EXTERNO,
             CERT.COD_CERTIFICADO
        INTO P_IDENTIFICADOR,
             V_COD_ESTADO,
             V_COD_EXTERNO,
             V_COD_CERTIFICADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.COD_CERTIFICADO = P_CODIGO;
    
    ELSE
    
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_IDENTIFICADOR || '" EN EL PARAMETRO "P_IDENTIFICADOR"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO_IDENTIFICADOR',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_IDENTIFICADOR,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
    
      /* DETALLES */
      SELECT CERT.COD_CERTIFICADO,
             CERT.COD_ESTADO,
             CERT.COD_EXTERNO,
             CERT.COD_CERTIFICADO
        INTO P_CODIGO, V_COD_ESTADO, V_COD_EXTERNO, V_COD_CERTIFICADO
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
    
    END IF;
  
    IF P_BORRAR_DATOS_CUENTA = 1 THEN
    
      /* RECUPERA OS CERTIFICADOS CONFLITANTES */
      SRETORNAR_CONFLITANTES(P_IDENTIFICADOR,
                             0,
                             0,
                             0,
                             0,
                             1,
                             CURSOR_CERT_CONFLITANTE,
                             P_CULTURA,
                             P_ERROR);
    
      /* VALIDA SE EXISTE CERTIFICADO TEMPORARIO */
      SELECT COUNT(*)
        INTO V_EXISTE
        FROM SAPR_GTT_TCERT_EJECUCION CT
       WHERE CT.COD_ESTADO = 'TP'
         AND CT.OID_EJECUCION = VAR$OID_EJECUCION;
    
      /* SE EXISTIR INFORMA QUE JÁ ESTÁ GERANDO UM CERTIFICADO TEMPORÁRIO COM OS MESMOS FILTROS */
      IF V_EXISTE > 0 THEN
        BEGIN
          /* RECUPERA O CLIENTE */
          SELECT CL.COD_CLIENTE, CL.DES_CLIENTE
            INTO V_COD_CLIENTE, V_DES_CLIENTE
            FROM SAPR_GTT_TCERT_EJECUCION CT
           INNER JOIN SAPR_TCERTIFICADO C
              ON C.OID_CERTIFICADO = CT.OID_CERTIFICADO
           INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
              ON CNS.OID_CONFIG_NIVEL_SALDO = C.OID_CONFIG_NIVEL_SALDO
           INNER JOIN GEPR_TCLIENTE CL
              ON CL.OID_CLIENTE = CNS.OID_CLIENTE
           WHERE CT.COD_ESTADO = 'TP'
             AND OID_EJECUCION = VAR$OID_EJECUCION;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            V_COD_CLIENTE := NULL;
            V_DES_CLIENTE := NULL;
        END;
      
        CUSTOM_ERROR_CODE    := -20001;
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_EJECUCION',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            V_COD_CLIENTE || '|' ||
                                            V_DES_CLIENTE,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
    
    END IF;
  
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;

  
    /* EXCLUI OS REGISTROS DE ARQUIVOS DE RELATORIOS GERADO */
    DELETE FROM SAPR_TRESULTADO_RPXPARAMETRO RP
     WHERE RP.OID_RESULTADO_REPORTE IN
           (SELECT RR.OID_RESULTADO_REPORTE
              FROM SAPR_TRESULTADO_REPORTE RR
             WHERE RR.OID_ENTIDAD_REPORTE = P_IDENTIFICADOR);
  
    DELETE SAPR_TRESULTADO_REPORTE RR
     WHERE RR.OID_ENTIDAD_REPORTE = P_IDENTIFICADOR;
  
    SRECUPERAR_CUENTAS(P_IDENTIFICADOR, P_ERROR);
  
    /* CONVERSION DE TEMPORARIO PARA CONSULTA */
    IF V_COD_ESTADO = 'TP' AND P_CODIGO_ESTADO_CONVERSION = 'CO' THEN
    
      /* LA CONVERSION DESDE UN CERTIFICADO TEMPORARIO HASTA UNO DE CONSULTA
      SE PASA CUANDO LA GENERACION DE LOS DATOS DE FILTROS DEL CERTIFICADO
      DE CONSULTA, SE HAN CERRADO. ES DECIR, ANTES DE TENER EL CERTIFICADO
      LISTO PARA UTILIZACION, EL ESTADO SE QUEDA FIJO COMO TEMPORARIO
      HASTA QUE TODOS LOS FILTROS (SECTORES, SUBCANALES, ETC) ESTEN
      CORRECTAMENTE GRABADOS EN LA BASE DE DATOS PARA QUE DESPUES
      SEA ACTUALIZADO COMO CONSULTA */
    
      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO = P_CODIGO_ESTADO_CONVERSION
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* CONVERSION DE CONSULTA PARA PROVISIONAL SIN CIERRE */
    ELSIF (V_COD_ESTADO = 'CO' OR V_COD_ESTADO = 'TP') AND
          P_CODIGO_ESTADO_CONVERSION = 'PS' THEN
    
      /* LA CONVERSION DESDE UN CERTIFICADO DE CONSULTA HASTA UNO PROVISIONAL
      SIN CIERRE DE PERIODO SE PASA CUANDO DESPUES DE EVALUAR LOS DATOS
      REGRESADOS INICIALMENTE POR EL CERTIFICADO DE CONSULTA, EL USUARIO
      DEFINE QUE SE DEBEN MARCAR TODAS LAS TRANSACCIONES EVALUADAS PREVIAMENTE.
      ES DECIR, AL EJECUTAR UNA EVALUACION PREVIA EN EL CERTIFICADO DE CONSULTA,
      EL USUARIO DEFINE OFICIALIZAR EL CERTIFICADO, DESTO MODO, SE CAMBIA PARA UNO
      CERTIFICADO SIN CIERRE DE PERIODO (PERMITINDO GRABACION DE NUEVOS MOVIMIENTOS
      PARA LAS COMBINATORIAS DE CUENTAS Y VALORES ENVOLUCRADOS). */
    
      /* MARCA LAS TRANSACCIONES Y CALCULA LOS SALDOS */
      SMARCAR_TRANSACCIONES(P_IDENTIFICADOR,
                            P_DESCRIPCION_USUARIO,
                            P_ERROR);
    
      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO      = P_CODIGO_ESTADO_CONVERSION,
             COD_CERTIFICADO = P_NUEVO_CODIGO,
             COD_EXTERNO     = P_NUEVO_CODIGO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* CONVERSION DE CONSULTA PARA PROVISIONAL CON CIERRE */
    ELSIF (V_COD_ESTADO = 'CO' OR V_COD_ESTADO = 'TP') AND
          P_CODIGO_ESTADO_CONVERSION = 'PC' THEN
    
      /* LA CONVERSION DESDE UN CERTIFICADO DE CONSULTA HASTA UNO PROVISIONAL
      CON CIERRE DE PERIODO SE PASA CUANDO DESPUES DE EVALUAR LOS DATOS
      REGRESADOS INICIALMENTE POR EL CERTIFICADO DE CONSULTA, EL USUARIO
      DEFINE QUE SE DEBEN MARCAR TODAS LAS TRANSACCIONES EVALUADAS PREVIAMENTE.
      ES DECIR, AL EJECUTAR UNA EVALUACION PREVIA EN EL CERTIFICADO DE CONSULTA,
      EL USUARIO DEFINE OFICIALIZAR EL CERTIFICADO, DESTO MODO, SE CAMBIA PARA UNO
      CERTIFICADO CON CIERRE DE PERIODO (NO PERMITINDO GRABACION DE NUEVOS MOVIMIENTOS
      PARA LAS COMBINATORIAS DE CUENTAS Y VALORES ENVOLUCRADOS). */
    
      /* MARCA LAS TRANSACCIONES Y CALCULA LOS SALDOS */
      SMARCAR_TRANSACCIONES(P_IDENTIFICADOR,
                            P_DESCRIPCION_USUARIO,
                            P_ERROR);

      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO      = P_CODIGO_ESTADO_CONVERSION,
             COD_CERTIFICADO = P_NUEVO_CODIGO,
             COD_EXTERNO     = P_NUEVO_CODIGO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
    ELSIF V_COD_ESTADO = 'PS' AND P_CODIGO_ESTADO_CONVERSION = 'PC' THEN
    
      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO = 'TP'
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      COMMIT;
    
      /* DESMARCA LAS TRANSACCIONES Y BORRA LOS SALDOS */
      SDESMARCAR_TRANSACCIONES(P_IDENTIFICADOR,
                               P_DESCRIPCION_USUARIO,
                               P_ERROR);
    
      /* MARCA LAS TRANSACCIONES Y CALCULA LOS SALDOS */
      SMARCAR_TRANSACCIONES(P_IDENTIFICADOR,
                            P_DESCRIPCION_USUARIO,
                            P_ERROR);

      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO      = P_CODIGO_ESTADO_CONVERSION,
             COD_CERTIFICADO = P_NUEVO_CODIGO,
             COD_EXTERNO     = P_NUEVO_CODIGO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* CONVERSION DE PROVISIONAL CON CIERRE PARA DEFINITIVO */
    ELSIF V_COD_ESTADO = 'PC' AND P_CODIGO_ESTADO_CONVERSION = 'DE' THEN
    
      /* LA CONVERSION DESDE UN CERTIFICADO DE PROVISIONAL CON CIERRE DE PERIODO
      HASTA UNO DEFINITIVO SE PASA CUANDO EL USUARIO DECIDE QUE LOS DATOS CERTIFICADO
      DEBERAN QUEDAR DEFINITIVOS (NO MAS PODRAN SUFRIR CAMBIOS) */
    
      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO = 'TP'
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      COMMIT;
    
      /* RECUPERA OS DOCUMENTOS QUE SERAO MARCADOS COMO CERTIFICADOS
      MARCA LOS DOCUMENTOS COMO CERTIFICADOS */
      FOR CDOC IN (WITH CONTAS AS
                      (SELECT CUEN.OID_CUENTA, CERT.OID_CERTIFICADO
                        FROM SAPR_TCERTIFICADO CERT
                       INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
                          ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
                       INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
                          ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
                       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
                          ON CERT.OID_CONFIG_NIVEL_SALDO =
                             CONS.OID_CONFIG_NIVEL_SALDO
                       INNER JOIN SAPR_TCUENTA CUEN
                          ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
                         AND CESE.OID_SECTOR = CUEN.OID_SECTOR
                         AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
                       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR
                         AND CERT.COD_ESTADO = 'DE'
                       GROUP BY CUEN.OID_CUENTA, CERT.OID_CERTIFICADO),
                     TRANSACOES AS
                      (SELECT TE.OID_DOCUMENTO, TE.OID_CUENTA_SALDO
                        FROM SAPR_TTRANSACCION_EFECTIVO TE
                       WHERE TE.OID_CERTIFICADO = P_IDENTIFICADOR
                      UNION
                      SELECT MP.OID_DOCUMENTO, MP.OID_CUENTA_SALDO
                        FROM SAPR_TTRANSACCION_MEDIO_PAGO MP
                       WHERE MP.OID_CERTIFICADO = P_IDENTIFICADOR)
                     SELECT DOC.OID_DOCUMENTO,
                            (SELECT COUNT(1)
                               FROM CONTAS
                              WHERE OID_CUENTA = DOC.OID_CUENTA_SALDO_ORIGEN) AS ORIGEN,
                            (SELECT COUNT(1)
                               FROM CONTAS
                              WHERE OID_CUENTA = DOC.OID_CUENTA_SALDO_DESTINO) AS DESTINO
                       FROM SAPR_TDOCUMENTO DOC
                      INNER JOIN TRANSACOES T
                         ON T.OID_DOCUMENTO = DOC.OID_DOCUMENTO) LOOP
        IF (CDOC.ORIGEN = 1 AND CDOC.DESTINO = 1) THEN
          UPDATE SAPR_TDOCUMENTO
             SET BOL_CERTIFICADO = 1, COD_CERTIFICACION_CUENTAS = 'A' /* AMBAS- CERTIFICADO NA ORIGEN E DESTINO */
           WHERE COD_ESTADO = 'AC' /* SOMENTE DOCUMENTOS QUE ESTAO ACEITOS */
             AND OID_DOCUMENTO = CDOC.OID_DOCUMENTO;
        
          UPDATE SAPR_TTRANSACCION_EFECTIVO T
             SET T.BOL_CERTIFICADO = 1
           WHERE T.OID_DOCUMENTO = CDOC.OID_DOCUMENTO
             AND T.OID_CERTIFICADO = P_IDENTIFICADOR
             AND T.BOL_CERTIFICADO = 0;
        
          UPDATE SAPR_TTRANSACCION_MEDIO_PAGO T
             SET T.BOL_CERTIFICADO = 1
           WHERE T.OID_DOCUMENTO = CDOC.OID_DOCUMENTO
             AND T.OID_CERTIFICADO = P_IDENTIFICADOR
             AND T.BOL_CERTIFICADO = 0;
        
        ELSIF (CDOC.ORIGEN = 1 AND CDOC.DESTINO = 0) THEN
          /* CERTIFICANDO NA ORIGEN
          SE O DOCUMENTO JA ESTAVA CERTIFICADO NO DESTINO ENTAO ATUALIZA PARA AMBOS
          E ATUALIZA O DOCUMENTO PARA BOL_CERTIFICADO=1 */
        
          UPDATE SAPR_TDOCUMENTO
             SET BOL_CERTIFICADO           = DECODE(COD_CERTIFICACION_CUENTAS,
                                                    'N',
                                                    0,
                                                    1),
                 COD_CERTIFICACION_CUENTAS = DECODE(COD_CERTIFICACION_CUENTAS,
                                                    'N',
                                                    'O',
                                                    'D',
                                                    'A',
                                                    COD_CERTIFICACION_CUENTAS)
           WHERE COD_ESTADO = 'AC' /* SOMENTE DOCUMENTOS QUE ESTAO ACEITOS */
             AND OID_DOCUMENTO = CDOC.OID_DOCUMENTO;
        
          UPDATE SAPR_TTRANSACCION_EFECTIVO T
             SET T.BOL_CERTIFICADO = 1
           WHERE T.OID_DOCUMENTO = CDOC.OID_DOCUMENTO
             AND T.OID_CERTIFICADO = P_IDENTIFICADOR
             AND T.COD_TIPO_SITIO = 'O'
             AND T.BOL_CERTIFICADO = 0;
        
          UPDATE SAPR_TTRANSACCION_MEDIO_PAGO T
             SET T.BOL_CERTIFICADO = 1
           WHERE T.OID_DOCUMENTO = CDOC.OID_DOCUMENTO
             AND T.OID_CERTIFICADO = P_IDENTIFICADOR
             AND T.COD_TIPO_SITIO = 'O'
             AND T.BOL_CERTIFICADO = 0;
        
        ELSIF (CDOC.ORIGEN = 0 AND CDOC.DESTINO = 1) THEN
          /* CERTIFICANDO NO DESTINO
          SE O DOCUMENTO JA ESTAVA CERTIFICADO NA ORIGEN ENTAO ATUALIZA PARA AMBOS
          E ATUALIZA O DOCUMENTO PARA BOL_CERTIFICADO=1 */
          UPDATE SAPR_TDOCUMENTO
             SET BOL_CERTIFICADO           = DECODE(COD_CERTIFICACION_CUENTAS,
                                                    'N',
                                                    0,
                                                    1),
                 COD_CERTIFICACION_CUENTAS = DECODE(COD_CERTIFICACION_CUENTAS,
                                                    'N',
                                                    'D',
                                                    'O',
                                                    'A',
                                                    COD_CERTIFICACION_CUENTAS)
           WHERE COD_ESTADO = 'AC' /* SOMENTE DOCUMENTOS QUE ESTAO ACEITOS */
             AND OID_DOCUMENTO = CDOC.OID_DOCUMENTO;
        
          UPDATE SAPR_TTRANSACCION_EFECTIVO T
             SET T.BOL_CERTIFICADO = 1
           WHERE T.OID_DOCUMENTO = CDOC.OID_DOCUMENTO
             AND T.OID_CERTIFICADO = P_IDENTIFICADOR
             AND T.COD_TIPO_SITIO = 'D'
             AND T.BOL_CERTIFICADO = 0;
        
          UPDATE SAPR_TTRANSACCION_MEDIO_PAGO T
             SET T.BOL_CERTIFICADO = 1
           WHERE T.OID_DOCUMENTO = CDOC.OID_DOCUMENTO
             AND T.OID_CERTIFICADO = P_IDENTIFICADOR
             AND T.COD_TIPO_SITIO = 'D'
             AND T.BOL_CERTIFICADO = 0;
        
        END IF;
      END LOOP;

      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO      = P_CODIGO_ESTADO_CONVERSION,
             COD_CERTIFICADO = P_NUEVO_CODIGO,
             COD_EXTERNO     = P_NUEVO_CODIGO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* ACTUALIZACION DE TADOS DEL TIPO PROVISIONAL SIN CIERRE DE PERIODO
      O DEL TIPO PROVISIONAL CON CIERRE DE PERIODO */
    ELSIF (V_COD_ESTADO = 'PS' AND P_CODIGO_ESTADO_CONVERSION = 'PS') OR
          (V_COD_ESTADO = 'PC' AND P_CODIGO_ESTADO_CONVERSION = 'PC') THEN
    
      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO = 'TP'
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
      COMMIT;
    
      /* DESMARCA LAS TRANSACCIONES Y BORRA LOS SALDOS */
      SDESMARCAR_TRANSACCIONES(P_IDENTIFICADOR,
                               P_DESCRIPCION_USUARIO,
                               P_ERROR);
    
      /* MARCA LAS TRANSACCIONES Y CALCULA LOS SALDOS */
      SMARCAR_TRANSACCIONES(P_IDENTIFICADOR,
                            P_DESCRIPCION_USUARIO,
                            P_ERROR);
    
      /* ACTUALIZA EL ESTADO DEL CERTIFICADO */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO      = P_CODIGO_ESTADO_CONVERSION,
             COD_CERTIFICADO = P_NUEVO_CODIGO,
             COD_EXTERNO     = P_NUEVO_CODIGO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    
    ELSE
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'NO ES POSIBLE CONVERTIR UN CERTIFICADO DESDE EL ESTADO "' || V_COD_ESTADO || '" HASTA EL ESTADO "' || P_CODIGO_ESTADO_CONVERSION || '"'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_NO_FUE_POSIBLE_CONVERTIR',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          V_COD_ESTADO || '|' ||
                                          P_CODIGO_ESTADO_CONVERSION,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* EJECUTA O CERTIFICADO_URUGUAY PARA OS CANAIS E DELEGACIONES DO CERTIFICADO */
    IF V_NOMBRE_PROCEDURE IS NOT NULL THEN
      FOR SUBCANAL_DELEGACION IN CURSOR_SUBCANALES_DELEGACIONES LOOP
      
        V_SQL := 'begin ' || V_NOMBRE_PROCEDURE ||
                 '(:COD_CLIENTE, :FECHA_CERTIFICADO, :COD_DELEGACION, :COD_SUBCANAL, :OID_CERTIFICADO); end;';
        execute immediate V_SQL
          using in SUBCANAL_DELEGACION.COD_CLIENTE, in SUBCANAL_DELEGACION.FYH_CERTIFICADO, in SUBCANAL_DELEGACION.COD_DELEGACION, in SUBCANAL_DELEGACION.COD_SUBCANAL, in P_IDENTIFICADOR;
      
      END LOOP;
    END IF;
  
  
    IF P_BORRAR_DATOS_CUENTA = 1 THEN
      DELETE FROM SAPR_GTT_TCERT_EJECUCION
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      DELETE FROM SAPR_GTT_TCUENTA_CERTIFICADO
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      COMMIT;
    END IF;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      ROLLBACK;
      /* VOLTA O CERTIFICADO COM OS MESMOS VALORES ANTERIORES */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO      = V_COD_ESTADO,
             COD_CERTIFICADO = V_COD_CERTIFICADO,
             COD_EXTERNO     = V_COD_EXTERNO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
      COMMIT;
    
      SANULAR_CERTIFICADO(P_IDENTIFICADOR => P_IDENTIFICADOR,
                          P_ERROR         => P_ERROR);
    
      DELETE FROM SAPR_GTT_TCERT_EJECUCION
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      DELETE FROM SAPR_GTT_TCUENTA_CERTIFICADO
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      COMMIT;
    
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      ROLLBACK;
      /* VOLTA O CERTIFICADO COM OS MESMOS VALORES ANTERIORES */
      UPDATE SAPR_TCERTIFICADO
         SET COD_ESTADO      = V_COD_ESTADO,
             COD_CERTIFICADO = V_COD_CERTIFICADO,
             COD_EXTERNO     = V_COD_EXTERNO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
      COMMIT;
    
      SANULAR_CERTIFICADO(P_IDENTIFICADOR => P_IDENTIFICADOR,
                          P_ERROR         => P_ERROR);
    
      DELETE FROM SAPR_GTT_TCERT_EJECUCION
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      DELETE FROM SAPR_GTT_TCUENTA_CERTIFICADO
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;
      COMMIT;
    
      P_ERROR := 'ERROR: SCONVERTIR: ' || SQLCODE || ' - ' || SQLERRM;
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SCONVERTIR;

  PROCEDURE SRETORNAR_CONFLITANTES(P_IDENTIFICADOR               IN VARCHAR2,
                                   P_BUSCAR_TIPO_CONSULTA        IN INT,
                                   P_BUSCAR_TIPO_PROV_SIN_CIERRE IN INT,
                                   P_BUSCAR_TIPO_PROV_CON_CIERRE IN INT,
                                   P_BUSCAR_TIPO_DEFINITIVO      IN INT,
                                   P_BUSCAR_TIPO_TEMPORARIO      IN INT,
                                   P_CERTIFICADOS_CONFLITANTES   OUT SYS_REFCURSOR,
                                   P_CULTURA                     IN VARCHAR2,
                                   P_ERROR                       OUT VARCHAR2) AS
  
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_TIPOS_CERTIFICADOS VARCHAR2(30) := '';
  BEGIN
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;
  
    V_TIPOS_CERTIFICADOS := 'AN,';
  
    /* VERIFICA SE DEBERA COMPARAR LOS DATOS DEL CERTIFICADO (P_IDENTIFICADOR)
    CON OTROS CERTIFICADOS DEL TIPO CONSULTA */
    IF P_BUSCAR_TIPO_CONSULTA = 1 THEN
      V_TIPOS_CERTIFICADOS := 'CO,';
    END IF;
  
    /* VERIFICA SE DEBERA COMPARAR LOS DATOS DEL CERTIFICADO (P_IDENTIFICADOR)
    CON OTROS CERTIFICADOS DEL TIPO PROVISIONAL SIN CIERRE */
    IF P_BUSCAR_TIPO_PROV_SIN_CIERRE = 1 THEN
      V_TIPOS_CERTIFICADOS := V_TIPOS_CERTIFICADOS || 'PS,';
    END IF;
  
    /* VERIFICA SE DEBERA COMPARAR LOS DATOS DEL CERTIFICADO (P_IDENTIFICADOR)
    CON OTROS CERTIFICADOS DEL TIPO PROVISIONAL CON CIERRE */
    IF P_BUSCAR_TIPO_PROV_CON_CIERRE = 1 THEN
      V_TIPOS_CERTIFICADOS := V_TIPOS_CERTIFICADOS || 'PC,';
    END IF;
  
    /* VERIFICA SE DEBERA COMPARAR LOS DATOS DEL CERTIFICADO (P_IDENTIFICADOR)
    CON OTROS CERTIFICADOS DEL TIPO DEFINITIVO */
    IF P_BUSCAR_TIPO_DEFINITIVO = 1 THEN
      V_TIPOS_CERTIFICADOS := V_TIPOS_CERTIFICADOS || 'DE,';
    END IF;
  
    /* VERIFICA SE DEBERA COMPARAR LOS DATOS DEL CERTIFICADO (P_IDENTIFICADOR)
    CON OTROS CERTIFICADOS DEL TIPO TEMPORARIO */
    IF P_BUSCAR_TIPO_TEMPORARIO = 1 THEN
      V_TIPOS_CERTIFICADOS := V_TIPOS_CERTIFICADOS || 'TP,';
    END IF;
  
    IF LENGTH(V_TIPOS_CERTIFICADOS) IS NULL THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'POR LO MENOS UNO DE LOS PARAMETROS "P_BUSCAR_TIPO_CONSULTA", "P_BUSCAR_TIPO_PROV_SIN_CIERRE", "P_BUSCAR_TIPO_PROV_CON_CIERRE" O "P_BUSCAR_TIPO_DEFINITIVO" DEBERA SER INFORMADO'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_PARAMETROS_NO_INFORMADOS',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          NULL,
                                          1);
      RAISE CUSTOM_ERROR;
    ELSE
      V_TIPOS_CERTIFICADOS := SUBSTR(V_TIPOS_CERTIFICADOS,
                                     1,
                                     (LENGTH(V_TIPOS_CERTIFICADOS) - 1));
    END IF;
  
    /* EJECTUA LA CONSULTA BUSCANDO DATOS DE SECTORES UTILIZADOS IGUALMENTE EN UNO
    CERTIFICADO PARA EL MISMO CLIENTE Y DESPUES EJECUTA LA MISMA BUSCA PERO
    VERIFICANDO LOS SUBCANALES ENVOLUCRADOS, EN CASO DE ENCONTRAR DATOS,
    LOS REGRESARA POR EL CURSOR DE MODO A SER POSIBLE IDENTIFICAR EN CUAL OTRO
    CERTIFICADO EL ACTUAL (P_IDENTIFICADOR) TIENE UN CONFLITO (MISMA COMBINACION DE
    FILTROS - ENTERA O PARCIAL - UTILIZADA EN LA GENERACION) */
    INSERT INTO SAPR_GTT_TCERT_EJECUCION
      (OID_CERTIFICADO,
       COD_CERTIFICADO,
       COD_ESTADO,
       COD_EXTERNO,
       FYH_CERTIFICADO,
       OID_EJECUCION)
      WITH CERTIFICADOS_CONFLITANTES AS
       (SELECT DISTINCT CT.OID_CERTIFICADO,
                        CT.COD_CERTIFICADO,
                        CT.COD_ESTADO,
                        CT.COD_EXTERNO,
                        CT.FYH_CERTIFICADO,
                        CT.GMT_CREACION,
                        VAR$OID_EJECUCION AS OID_EJECUCION
          FROM SAPR_TCERTIFICADO CT
         INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
            ON CT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
         INNER JOIN SAPR_TCERTIFICADOXDELEGACION DEL
            ON DEL.OID_CERTIFICADO = CT.OID_CERTIFICADO
         INNER JOIN SAPR_TCERTIFICADOXSECTOR CTS
            ON CTS.OID_CERTIFICADO = CT.OID_CERTIFICADO
         WHERE CT.OID_CERTIFICADO <> P_IDENTIFICADOR
           AND CT.COD_ESTADO IN
               (SELECT *
                  FROM TABLE(STRING_TO_ARRAY_###VERSION### (V_TIPOS_CERTIFICADOS, ',')))
           AND CT.OID_CONFIG_NIVEL_SALDO IN
               (SELECT OID_CONFIG_NIVEL_SALDO
                  FROM SAPR_TCERTIFICADO
                 WHERE OID_CERTIFICADO = P_IDENTIFICADOR)
           AND CESC.OID_SUBCANAL IN
               (SELECT OID_SUBCANAL
                  FROM SAPR_TCERTIFICADOXSUBCANAL
                 WHERE OID_CERTIFICADO = P_IDENTIFICADOR)
           AND DEL.OID_DELEGACION IN
               (SELECT OID_DELEGACION
                  FROM SAPR_TCERTIFICADOXDELEGACION
                 WHERE OID_CERTIFICADO = P_IDENTIFICADOR)
           AND CTS.OID_SECTOR IN
               (SELECT OID_SECTOR
                  FROM SAPR_TCERTIFICADOXSECTOR
                 WHERE OID_CERTIFICADO = P_IDENTIFICADOR)),
      CERTIFICADOS_NO_ANULADOS AS
       (SELECT CNA.OID_CERTIFICADO,
               CNA.COD_CERTIFICADO,
               CNA.COD_ESTADO,
               CNA.COD_EXTERNO,
               CNA.FYH_CERTIFICADO,
               VAR$OID_EJECUCION AS OID_EJECUCION
          FROM CERTIFICADOS_CONFLITANTES CNA
         WHERE CNA.COD_ESTADO <> 'AN'),
      CERTIFICADOS_ANULADOS AS
       (SELECT CA.OID_CERTIFICADO,
               CA.COD_CERTIFICADO,
               CA.COD_ESTADO,
               CA.COD_EXTERNO,
               CA.FYH_CERTIFICADO,
               VAR$OID_EJECUCION AS OID_EJECUCION
          FROM CERTIFICADOS_CONFLITANTES CA
         WHERE CA.COD_ESTADO = 'AN'
           AND CA.GMT_CREACION <= (SYSDATE - INTERVAL '1' HOUR))
      SELECT CTNA.OID_CERTIFICADO,
             CTNA.COD_CERTIFICADO,
             CTNA.COD_ESTADO,
             CTNA.COD_EXTERNO,
             CTNA.FYH_CERTIFICADO,
             VAR$OID_EJECUCION AS OID_EJECUCION
        FROM CERTIFICADOS_NO_ANULADOS CTNA
      UNION
      SELECT CTA.OID_CERTIFICADO,
             CTA.COD_CERTIFICADO,
             CTA.COD_ESTADO,
             CTA.COD_EXTERNO,
             CTA.FYH_CERTIFICADO,
             VAR$OID_EJECUCION AS OID_EJECUCION
        FROM CERTIFICADOS_ANULADOS CTA;
  
    OPEN P_CERTIFICADOS_CONFLITANTES FOR
      SELECT *
        FROM SAPR_GTT_TCERT_EJECUCION
       WHERE OID_EJECUCION = VAR$OID_EJECUCION;

  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SRETORNAR_CONFLITANTES: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SRETORNAR_CONFLITANTES;

  PROCEDURE SMARCAR_TRANSACCIONES(P_IDENTIFICADOR       IN VARCHAR2,
                                  P_DESCRIPCION_USUARIO IN VARCHAR2,
                                  P_ERROR               OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_COD_ESTADO         VARCHAR2(2) := '';
  
  BEGIN
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;

  
    /* DETALLES */
    SELECT CERT.COD_ESTADO
      INTO V_COD_ESTADO
      FROM SAPR_TCERTIFICADO CERT
     WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
  
    /* VERIFICA SI EL CERTIFICADO ES DEL TIPO PROVISIONAL SIN CIERRE O PROVISIONAL CON CIERRE
    LOS CERTIFICADOS DEL TIPO CONSULTA NO MARCAN TRANSACCIONES Y LOS CERTIFICADOS DEL TIPO
    DEFINITIVO, YA TIENEN LAS TRANSACCIONES MARCADAS Y NO SE PUEDE REMARCALAS */
    IF V_COD_ESTADO = 'PS' OR V_COD_ESTADO = 'PC' OR V_COD_ESTADO = 'TP' THEN
    
      SMARCAR_TRANSAC_EFECTIVO(P_IDENTIFICADOR,
                               P_DESCRIPCION_USUARIO,
                               P_ERROR);
    
      SMARCAR_TRANSAC_MEDIO_PAGO(P_IDENTIFICADOR,
                                 P_DESCRIPCION_USUARIO,
                                 P_ERROR);
    
      INSERT INTO SAPR_TCERT_SALDO_EFECTIVO
        (OID_CERT_SALDO_EFECTIVO,
         OID_CUENTA_SALDO,
         OID_CERTIFICADO,
         OID_DENOMINACION,
         OID_DIVISA,
         COD_NIVEL_DETALLE,
         COD_TIPO_EFECTIVO_TOTAL,
         BOL_DISPONIBLE,
         NUM_IMPORTE,
         NUM_IMPORTE_ACUMULADO,
         NEL_CANTIDAD,
         NEL_CANTIDAD_ACUMULADO,
         OID_UNIDAD_MEDIDA,
         OID_CALIDAD,
         OID_CERTIFICADO_ANTERIOR,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION)
        WITH Q AS
         (SELECT SAEF.OID_CERTIFICADO,
                 SAEF.OID_CUENTA_SALDO,
                 SAEF.OID_DIVISA,
                 SAEF.OID_DENOMINACION,
                 SAEF.COD_NIVEL_DETALLE,
                 SAEF.COD_TIPO_EFECTIVO_TOTAL,
                 SAEF.BOL_DISPONIBLE,
                 0 AS NUM_IMPORTE,
                 (NVL(SAEF.NUM_IMPORTE, 0) +
                 NVL(SAEF.NUM_IMPORTE_ACUMULADO, 0)) AS NUM_IMPORTE_ACUMULADO,
                 0 AS NEL_CANTIDAD,
                 (NVL(SAEF.NEL_CANTIDAD, 0) +
                 NVL(SAEF.NEL_CANTIDAD_ACUMULADO, 0)) AS NEL_CANTIDAD_ACUMULADO,
                 SAEF.OID_UNIDAD_MEDIDA,
                 SAEF.OID_CALIDAD
            FROM SAPR_TCERT_SALDO_EFECTIVO SAEF
           INNER JOIN SAPR_GTT_TCUENTA_CERTIFICADO CONTA
              ON CONTA.OID_CUENTA_SALDO = SAEF.OID_CUENTA_SALDO
             AND CONTA.OID_DIVISA = SAEF.OID_DIVISA
           WHERE SAEF.OID_CERTIFICADO = CONTA.OID_CERTIFICADO_ANT
             AND CONTA.OID_EJECUCION = VAR$OID_EJECUCION
          UNION ALL
          SELECT NULL OID_CERTIFICADO,
                 TREF.OID_CUENTA_SALDO,
                 TREF.OID_DIVISA,
                 TREF.OID_DENOMINACION,
                 TREF.COD_NIVEL_DETALLE,
                 TREF.COD_TIPO_EFECTIVO_TOTAL,
                 TREF.BOL_DISPONIBLE,
                 SUM(TREF.NUM_IMPORTE) AS NUM_IMPORTE,
                 0 AS NUM_IMPORTE_ACUMULADO,
                 SUM(TREF.NEL_CANTIDAD) AS NEL_CANTIDAD,
                 0 AS NEL_CANTIDAD_ACUMULADO,
                 TREF.OID_UNIDAD_MEDIDA,
                 TREF.OID_CALIDAD
            FROM SAPR_TTRANSACCION_EFECTIVO TREF
           INNER JOIN SAPR_TDOCUMENTO DOCU
              ON TREF.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
             AND DOCU.COD_ESTADO IN ('AC', 'RC', 'SU')
           WHERE TREF.OID_CERTIFICADO = P_IDENTIFICADOR
             AND TREF.COD_ESTADO_DOCUMENTO IN ('AC', 'RC', 'CF', 'SU')
           GROUP BY TREF.OID_CUENTA_SALDO,
                    TREF.OID_DIVISA,
                    TREF.OID_DENOMINACION,
                    TREF.COD_NIVEL_DETALLE,
                    TREF.COD_TIPO_EFECTIVO_TOTAL,
                    TREF.BOL_DISPONIBLE,
                    TREF.OID_UNIDAD_MEDIDA,
                    TREF.OID_CALIDAD)
        SELECT SYS_GUID() AS OID_CERT_SALDO_EFECTIVO,
               Q.OID_CUENTA_SALDO,
               P_IDENTIFICADOR AS OID_CERTIFICADO,
               Q.OID_DENOMINACION,
               Q.OID_DIVISA,
               Q.COD_NIVEL_DETALLE,
               Q.COD_TIPO_EFECTIVO_TOTAL,
               Q.BOL_DISPONIBLE,
               SUM(Q.NUM_IMPORTE) AS NUM_IMPORTE,
               SUM(Q.NUM_IMPORTE_ACUMULADO) AS NUM_IMPORTE_ACUMULADO,
               SUM(Q.NEL_CANTIDAD) AS NEL_CANTIDAD,
               SUM(Q.NEL_CANTIDAD_ACUMULADO) AS NEL_CANTIDAD_ACUMULADO,
               Q.OID_UNIDAD_MEDIDA,
               Q.OID_CALIDAD,
               MAX(Q.OID_CERTIFICADO) OID_CERTIFICADO_ANTERIOR,
               SYSDATE AS GMT_CREACION,
               P_DESCRIPCION_USUARIO AS DES_USUARIO_CREACION,
               SYSDATE AS GMT_MODIFICACION,
               P_DESCRIPCION_USUARIO AS DES_USUARIO_MODIFICACION
          FROM Q
         GROUP BY Q.OID_CUENTA_SALDO,
                  Q.OID_DENOMINACION,
                  Q.OID_DIVISA,
                  Q.COD_NIVEL_DETALLE,
                  Q.COD_TIPO_EFECTIVO_TOTAL,
                  Q.BOL_DISPONIBLE,
                  Q.OID_UNIDAD_MEDIDA,
                  Q.OID_CALIDAD;
    
      INSERT INTO SAPR_TCERT_SALDO_MEDIO_PAGO
        (OID_CERT_SALDO_MEDIO_PAGO,
         OID_CUENTA_SALDO,
         OID_CERTIFICADO,
         OID_MEDIO_PAGO,
         COD_TIPO_MEDIO_PAGO,
         COD_NIVEL_DETALLE,
         BOL_DISPONIBLE,
         NUM_IMPORTE,
         NUM_IMPORTE_ACUMULADO,
         NEL_CANTIDAD,
         NEL_CANTIDAD_ACUMULADO,
         OID_DIVISA,
         OID_UNIDAD_MEDIDA,
         OID_CERTIFICADO_ANTERIOR,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION)
        WITH Q AS
         (SELECT CSMP.OID_CERTIFICADO,
                 CSMP.OID_CUENTA_SALDO,
                 CSMP.OID_DIVISA,
                 CSMP.OID_MEDIO_PAGO,
                 CSMP.COD_TIPO_MEDIO_PAGO,
                 CSMP.COD_NIVEL_DETALLE,
                 CSMP.BOL_DISPONIBLE,
                 0 AS NUM_IMPORTE,
                 (NVL(CSMP.NUM_IMPORTE, 0) +
                 NVL(CSMP.NUM_IMPORTE_ACUMULADO, 0)) AS NUM_IMPORTE_ACUMULADO,
                 0 AS NEL_CANTIDAD,
                 (NVL(CSMP.NEL_CANTIDAD, 0) +
                 NVL(CSMP.NEL_CANTIDAD_ACUMULADO, 0)) AS NEL_CANTIDAD_ACUMULADO,
                 CSMP.OID_UNIDAD_MEDIDA
            FROM SAPR_TCERT_SALDO_MEDIO_PAGO CSMP
           INNER JOIN SAPR_GTT_TCUENTA_CERTIFICADO CONTA
              ON CONTA.OID_CUENTA_SALDO = CSMP.OID_CUENTA_SALDO
             AND CONTA.OID_DIVISA = CSMP.OID_DIVISA
           WHERE CSMP.OID_CERTIFICADO = CONTA.OID_CERTIFICADO_ANT
             AND CONTA.OID_EJECUCION = VAR$OID_EJECUCION
          UNION ALL
          SELECT NULL OID_CERTIFICADO,
                 TRMP.OID_CUENTA_SALDO,
                 TRMP.OID_DIVISA,
                 TRMP.Oid_Medio_Pago,
                 TRMP.Cod_Tipo_Medio_Pago,
                 TRMP.COD_NIVEL_DETALLE,
                 TRMP.BOL_DISPONIBLE,
                 SUM(TRMP.NUM_IMPORTE) AS NUM_IMPORTE,
                 0 AS NUM_IMPORTE_ACUMULADO,
                 SUM(TRMP.NEL_CANTIDAD) AS NEL_CANTIDAD,
                 0 AS NEL_CANTIDAD_ACUMULADO,
                 TRMP.OID_UNIDAD_MEDIDA
            FROM SAPR_TTRANSACCION_MEDIO_PAGO TRMP
           INNER JOIN SAPR_TDOCUMENTO DOCU
              ON TRMP.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
             AND DOCU.COD_ESTADO IN ('AC', 'RC', 'SU')
           WHERE TRMP.OID_CERTIFICADO = P_IDENTIFICADOR
             AND TRMP.COD_ESTADO_DOCUMENTO IN ('AC', 'RC', 'CF', 'SU')
           GROUP BY TRMP.OID_CUENTA_SALDO,
                    TRMP.OID_DIVISA,
                    TRMP.Oid_Medio_Pago,
                    TRMP.Cod_Tipo_Medio_Pago,
                    TRMP.COD_NIVEL_DETALLE,
                    TRMP.BOL_DISPONIBLE,
                    TRMP.OID_UNIDAD_MEDIDA)
        SELECT SYS_GUID() AS OID_CERT_SALDO_MEDIO_PAGO,
               Q.OID_CUENTA_SALDO,
               P_IDENTIFICADOR AS OID_CERTIFICADO,
               Q.OID_MEDIO_PAGO,
               Q.COD_TIPO_MEDIO_PAGO,
               Q.COD_NIVEL_DETALLE,
               Q.BOL_DISPONIBLE,
               SUM(Q.NUM_IMPORTE) AS NUM_IMPORTE,
               SUM(Q.NUM_IMPORTE_ACUMULADO) AS NUM_IMPORTE_ACUMULADO,
               SUM(Q.NEL_CANTIDAD) AS NEL_CANTIDAD,
               SUM(Q.NEL_CANTIDAD_ACUMULADO) AS NEL_CANTIDAD_ACUMULADO,
               Q.OID_DIVISA,
               Q.OID_UNIDAD_MEDIDA,
               MAX(Q.OID_CERTIFICADO) OID_CERTIFICADO_ANTERIOR,
               SYSDATE AS GMT_CREACION,
               P_DESCRIPCION_USUARIO AS DES_USUARIO_CREACION,
               SYSDATE AS GMT_MODIFICACION,
               P_DESCRIPCION_USUARIO AS DES_USUARIO_MODIFICACION
          FROM Q
         GROUP BY Q.OID_CUENTA_SALDO,
                  Q.OID_DIVISA,
                  Q.OID_MEDIO_PAGO,
                  Q.COD_TIPO_MEDIO_PAGO,
                  Q.COD_NIVEL_DETALLE,
                  Q.BOL_DISPONIBLE,
                  Q.OID_UNIDAD_MEDIDA;
    
    END IF;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SMARCAR_TRANSACCIONES: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SMARCAR_TRANSACCIONES;

  PROCEDURE SMARCAR_TRANSAC_EFECTIVO(P_IDENTIFICADOR       IN VARCHAR2,
                                     P_DESCRIPCION_USUARIO IN VARCHAR2,
                                     P_ERROR               OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE     NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE  VARCHAR2(32767) := '';
    V_CANTIDAD_REG_COMMIT NUMBER := 0;
  
    CURSOR CURSOR_TRANSACCIONES IS(
      SELECT DISTINCT TREF.OID_TRANSACCION_EFECTIVO,
                      CASE
                        WHEN UPPER(TIDO.COD_TIPO_DOCUMENTO) = 'PASES' THEN
                         1
                        ELSE
                         0
                      END ES_DOCUMENTO_PASES,
                      (SELECT COUNT(*)
                         FROM DUAL
                        WHERE EXISTS (SELECT COUNT(*)
                                 FROM SAPR_TCARACT_FORMULARIO CF
                                WHERE CF.OID_CARACT_FORMULARIO =
                                      CFXF.OID_CARACT_FORMULARIO
                                  AND CF.COD_CARACT_FORMULARIO =
                                      'ENTRE_CANALES')) ES_ENTRE_CANALES,
                      UPPER(CORI.COD_CANAL) COD_CANAL_ORI,
                      UPPER(CDES.COD_CANAL) COD_CANAL_DES,
                      CASE
                        WHEN TCL.COD_TIPO_CLIENTE = '1' AND
                             TSCL.COD_TIPO_SUBCLIENTE = '1' AND
                             TPTO.COD_TIPO_PUNTO_SERVICIO = '4' THEN
                         1
                        ELSE
                         0
                      END ES_REGLA_CLIENTE_BANCO
        FROM SAPR_GTT_TCUENTA_CERTIFICADO CC
       INNER JOIN SAPR_TCUENTA CUEN
          ON CUEN.OID_CUENTA = CC.OID_CUENTA_SALDO
       INNER JOIN SAPR_TTRANSACCION_EFECTIVO TREF
          ON CUEN.OID_CUENTA = TREF.OID_CUENTA_SALDO
         AND TREF.FYH_PLAN_CERTIFICACION BETWEEN
             CC.FYH_CERTIFICADO_ANTERIOR AND CC.FYH_CERTIFICADO
         AND TREF.COD_ESTADO_DOCUMENTO IN ('AC', 'RC', 'CF', 'SU')
         AND TREF.BOL_CERTIFICADO = 0
       INNER JOIN SAPR_TDOCUMENTO DOCU
          ON DOCU.FYH_PLAN_CERTIFICACION BETWEEN
             CC.FYH_CERTIFICADO_ANTERIOR AND CC.FYH_CERTIFICADO
         AND DOCU.COD_ESTADO IN ('AC', 'RC', 'SU')
         AND DOCU.BOL_NO_CERTIFICAR = 0
         AND TREF.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
         AND DOCU.BOL_CERTIFICADO = 0
       INNER JOIN SAPR_TCUENTA CUENORI
          ON CUENORI.OID_CUENTA = DOCU.OID_CUENTA_ORIGEN
       INNER JOIN SAPR_TCUENTA CUENDES
          ON CUENDES.OID_CUENTA = DOCU.OID_CUENTA_DESTINO
       INNER JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CUENORI.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CUENORI.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
          ON PTO.OID_PTO_SERVICIO = CUENORI.OID_PTO_SERVICIO
        LEFT JOIN GEPR_TTIPO_CLIENTE TCL
          ON TCL.OID_TIPO_CLIENTE = CL.OID_TIPO_CLIENTE
        LEFT JOIN GEPR_TTIPO_SUBCLIENTE TSCL
          ON TSCL.OID_TIPO_SUBCLIENTE = SCL.OID_TIPO_SUBCLIENTE
        LEFT JOIN GEPR_TTIPO_PUNTO_SERVICIO TPTO
          ON TPTO.OID_TIPO_PUNTO_SERVICIO = PTO.OID_TIPO_PUNTO_SERVICIO
       INNER JOIN GEPR_TSUBCANAL SCORI
          ON SCORI.OID_SUBCANAL = CUENORI.OID_SUBCANAL
       INNER JOIN GEPR_TCANAL CORI
          ON CORI.OID_CANAL = SCORI.OID_CANAL
       INNER JOIN GEPR_TSUBCANAL SCDES
          ON SCDES.OID_SUBCANAL = CUENDES.OID_SUBCANAL
       INNER JOIN GEPR_TCANAL CDES
          ON CDES.OID_CANAL = SCDES.OID_CANAL
       INNER JOIN SAPR_TTIPO_DOCUMENTO TIDO
          ON DOCU.OID_TIPO_DOCUMENTO = TIDO.OID_TIPO_DOCUMENTO
         AND TIDO.BOL_CERTIFICACION = 1
       INNER JOIN SAPR_TFORMULARIO F
          ON F.OID_FORMULARIO = DOCU.OID_FORMULARIO
       INNER JOIN SAPR_TCARACTFORMXFORMULARIO CFXF
          ON CFXF.OID_FORMULARIO = F.OID_FORMULARIO
       WHERE CC.OID_EJECUCION = VAR$OID_EJECUCION);
  BEGIN
  
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;
  
    FOR TE IN CURSOR_TRANSACCIONES LOOP
    
      UPDATE SAPR_TTRANSACCION_EFECTIVO
         SET OID_CERTIFICADO          = P_IDENTIFICADOR,
             GMT_MODIFICACION         = SYSDATE,
             DES_USUARIO_MODIFICACION = P_DESCRIPCION_USUARIO
       WHERE OID_TRANSACCION_EFECTIVO = TE.OID_TRANSACCION_EFECTIVO
         AND BOL_CERTIFICADO = 0;
    
      V_CANTIDAD_REG_COMMIT := V_CANTIDAD_REG_COMMIT + 1;
    
      IF V_CANTIDAD_REG_COMMIT >= 5000 THEN
        COMMIT;
        V_CANTIDAD_REG_COMMIT := 0;
      END IF;
    
    END LOOP;
  
    COMMIT;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SMARCAR_TRANSAC_EFECTIVO: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SMARCAR_TRANSAC_EFECTIVO;

  PROCEDURE SMARCAR_TRANSAC_MEDIO_PAGO(P_IDENTIFICADOR       IN VARCHAR2,
                                       P_DESCRIPCION_USUARIO IN VARCHAR2,
                                       P_ERROR               OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE     NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE  VARCHAR2(32767) := '';
    V_CANTIDAD_REG_COMMIT NUMBER := 0;
  
    CURSOR CURSOR_TRANSACCIONES IS(
      SELECT DISTINCT TRMP.OID_TRANSACCION_MEDIO_PAGO,
                      CASE
                        WHEN UPPER(TIDO.COD_TIPO_DOCUMENTO) = 'PASES' THEN
                         1
                        ELSE
                         0
                      END ES_DOCUMENTO_PASES,
                      (SELECT COUNT(*)
                         FROM DUAL
                        WHERE EXISTS (SELECT COUNT(*)
                                 FROM SAPR_TCARACT_FORMULARIO CF
                                WHERE CF.OID_CARACT_FORMULARIO =
                                      CFXF.OID_CARACT_FORMULARIO
                                  AND CF.COD_CARACT_FORMULARIO =
                                      'ENTRE_CANALES')) ES_ENTRE_CANALES,
                      UPPER(CORI.COD_CANAL) COD_CANAL_ORI,
                      UPPER(CDES.COD_CANAL) COD_CANAL_DES,
                      CASE
                        WHEN TCL.COD_TIPO_CLIENTE = '1' AND
                             TSCL.COD_TIPO_SUBCLIENTE = '1' AND
                             TPTO.COD_TIPO_PUNTO_SERVICIO = '4' THEN
                         1
                        ELSE
                         0
                      END ES_REGLA_CLIENTE_BANCO
        FROM SAPR_GTT_TCUENTA_CERTIFICADO CC
       INNER JOIN SAPR_TCUENTA CUEN
          ON CUEN.OID_CUENTA = CC.OID_CUENTA_SALDO
       INNER JOIN SAPR_TTRANSACCION_MEDIO_PAGO TRMP
          ON CUEN.OID_CUENTA = TRMP.OID_CUENTA_SALDO
         AND TRMP.FYH_PLAN_CERTIFICACION BETWEEN
             CC.FYH_CERTIFICADO_ANTERIOR AND CC.FYH_CERTIFICADO
         AND TRMP.COD_ESTADO_DOCUMENTO IN ('AC', 'RC', 'CF', 'SU')
         AND TRMP.BOL_CERTIFICADO = 0
       INNER JOIN SAPR_TDOCUMENTO DOCU
          ON DOCU.FYH_PLAN_CERTIFICACION BETWEEN
             CC.FYH_CERTIFICADO_ANTERIOR AND CC.FYH_CERTIFICADO
         AND DOCU.COD_ESTADO IN ('AC', 'RC', 'SU')
         AND DOCU.BOL_NO_CERTIFICAR = 0
         AND TRMP.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
         AND DOCU.BOL_CERTIFICADO = 0
       INNER JOIN SAPR_TCUENTA CUENORI
          ON CUENORI.OID_CUENTA = DOCU.OID_CUENTA_ORIGEN
       INNER JOIN SAPR_TCUENTA CUENDES
          ON CUENDES.OID_CUENTA = DOCU.OID_CUENTA_DESTINO
       INNER JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CUENORI.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CUENORI.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
          ON PTO.OID_PTO_SERVICIO = CUENORI.OID_PTO_SERVICIO
        LEFT JOIN GEPR_TTIPO_CLIENTE TCL
          ON TCL.OID_TIPO_CLIENTE = CL.OID_TIPO_CLIENTE
        LEFT JOIN GEPR_TTIPO_SUBCLIENTE TSCL
          ON TSCL.OID_TIPO_SUBCLIENTE = SCL.OID_TIPO_SUBCLIENTE
        LEFT JOIN GEPR_TTIPO_PUNTO_SERVICIO TPTO
          ON TPTO.OID_TIPO_PUNTO_SERVICIO = PTO.OID_TIPO_PUNTO_SERVICIO
       INNER JOIN GEPR_TSUBCANAL SCORI
          ON SCORI.OID_SUBCANAL = CUENORI.OID_SUBCANAL
       INNER JOIN GEPR_TCANAL CORI
          ON CORI.OID_CANAL = SCORI.OID_CANAL
       INNER JOIN GEPR_TSUBCANAL SCDES
          ON SCDES.OID_SUBCANAL = CUENDES.OID_SUBCANAL
       INNER JOIN GEPR_TCANAL CDES
          ON CDES.OID_CANAL = SCDES.OID_CANAL
       INNER JOIN SAPR_TTIPO_DOCUMENTO TIDO
          ON DOCU.OID_TIPO_DOCUMENTO = TIDO.OID_TIPO_DOCUMENTO
         AND TIDO.BOL_CERTIFICACION = 1
       INNER JOIN SAPR_TFORMULARIO F
          ON F.OID_FORMULARIO = DOCU.OID_FORMULARIO
       INNER JOIN SAPR_TCARACTFORMXFORMULARIO CFXF
          ON CFXF.OID_FORMULARIO = F.OID_FORMULARIO
       WHERE CC.OID_EJECUCION = VAR$OID_EJECUCION);
  BEGIN
  
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;
  
    FOR TMP IN CURSOR_TRANSACCIONES LOOP
    
      UPDATE SAPR_TTRANSACCION_MEDIO_PAGO
         SET OID_CERTIFICADO          = P_IDENTIFICADOR,
             GMT_MODIFICACION         = SYSDATE,
             DES_USUARIO_MODIFICACION = P_DESCRIPCION_USUARIO
       WHERE OID_TRANSACCION_MEDIO_PAGO = TMP.OID_TRANSACCION_MEDIO_PAGO
         AND BOL_CERTIFICADO = 0;
    
      V_CANTIDAD_REG_COMMIT := V_CANTIDAD_REG_COMMIT + 1;
    
      IF V_CANTIDAD_REG_COMMIT >= 5000 THEN
        COMMIT;
        V_CANTIDAD_REG_COMMIT := 0;
      END IF;
    
    END LOOP;
  
    COMMIT;

  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SMARCAR_TRANSAC_MEDIO_PAGO: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SMARCAR_TRANSAC_MEDIO_PAGO;

  PROCEDURE SDESMARCAR_TRANSACCIONES(P_IDENTIFICADOR       IN VARCHAR2,
                                     P_DESCRIPCION_USUARIO IN VARCHAR2,
                                     P_ERROR               OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE     NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE  VARCHAR2(32767) := '';
    V_CANTIDAD_REG_COMMIT NUMBER := 0;
  
    CURSOR CURSOR_TRANSACCIONES_EFE IS(
      SELECT TE.OID_TRANSACCION_EFECTIVO
        FROM SAPR_TTRANSACCION_EFECTIVO TE
       WHERE TE.OID_CERTIFICADO = P_IDENTIFICADOR);
  
    CURSOR CURSOR_TRANSACCIONES_MP IS(
      SELECT TE.OID_TRANSACCION_MEDIO_PAGO
        FROM SAPR_TTRANSACCION_MEDIO_PAGO TE
       WHERE TE.OID_CERTIFICADO = P_IDENTIFICADOR);
  BEGIN
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;

  
    FOR TE IN CURSOR_TRANSACCIONES_EFE LOOP
    
      /* DESMARCA TODAS LAS TRANSACCIONES DE EFECTIVOS Y DE MEDIOS DE PAGO */
      UPDATE SAPR_TTRANSACCION_EFECTIVO TEFE
         SET OID_CERTIFICADO          = NULL,
             DES_USUARIO_MODIFICACION = P_DESCRIPCION_USUARIO,
             GMT_MODIFICACION         = SYSDATE
       WHERE TEFE.OID_TRANSACCION_EFECTIVO = TE.OID_TRANSACCION_EFECTIVO
         AND BOL_CERTIFICADO = 0;
    
      V_CANTIDAD_REG_COMMIT := V_CANTIDAD_REG_COMMIT + 1;
    
      IF V_CANTIDAD_REG_COMMIT >= 5000 THEN
        COMMIT;
        V_CANTIDAD_REG_COMMIT := 0;
      END IF;
    
    END LOOP;
  
    COMMIT;
  
    FOR TMP IN CURSOR_TRANSACCIONES_MP LOOP
    
      /* DESMARCA TODAS LAS TRANSACCIONES DE EFECTIVOS Y DE MEDIOS DE PAGO */
      UPDATE SAPR_TTRANSACCION_MEDIO_PAGO TMPG
         SET OID_CERTIFICADO          = NULL,
             DES_USUARIO_MODIFICACION = P_DESCRIPCION_USUARIO,
             GMT_MODIFICACION         = SYSDATE
       WHERE TMPG.OID_TRANSACCION_MEDIO_PAGO =
             TMP.OID_TRANSACCION_MEDIO_PAGO;
    
      V_CANTIDAD_REG_COMMIT := V_CANTIDAD_REG_COMMIT + 1;
    
      IF V_CANTIDAD_REG_COMMIT >= 5000 THEN
        COMMIT;
        V_CANTIDAD_REG_COMMIT := 0;
      END IF;
    
    END LOOP;
  
    COMMIT;
  
    /* BORRA TODOS LOS SALDOS CALCULADOS PARA EL CERTIFICADO */
    DELETE SAPR_TCERT_SALDO_EFECTIVO
     WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
    DELETE SAPR_TCERT_SALDO_MEDIO_PAGO
     WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SDESMARCAR_TRANSACCIONES: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SDESMARCAR_TRANSACCIONES;

  PROCEDURE SRECUPERAR_FILTROS_COD_CERT(P_IDENTIFICADOR IN VARCHAR2,
                                        P_CULTURA       IN VARCHAR2,
                                        P_CERTIFICADO   OUT SYS_REFCURSOR,
                                        P_SECTORES      OUT SYS_REFCURSOR,
                                        P_SUBCANALES    OUT SYS_REFCURSOR,
                                        P_DELEGACIONES  OUT SYS_REFCURSOR,
                                        P_ERROR         OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_EXISTE             NUMERIC := 0;
  BEGIN
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;
  
    SELECT COUNT(1)
      INTO V_EXISTE
      FROM SAPR_TCERTIFICADO
     WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
  
    IF V_EXISTE = 0 THEN
      CUSTOM_ERROR_CODE := -20001;
      /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_IDENTIFICADOR || '" EN EL PARAMETRO "P_IDENTIFICADOR"'; */
      CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                          'MSG_CERTIFICADO_NO_ENCONTRADO_IDENTIFICADOR',
                                          'CERTIFICACION',
                                          'GenesisSaldos',
                                          P_IDENTIFICADOR,
                                          1);
      RAISE CUSTOM_ERROR;
    END IF;
  
    /* DETALLES DE LOS FILTROS */
    OPEN P_CERTIFICADO FOR
      SELECT CERT.COD_CERTIFICADO,
             CERT.COD_EXTERNO,
             CERT.COD_ESTADO,
             CERT.FYH_CERTIFICADO,
             CLIE.OID_CLIENTE,
             CLIE.COD_CLIENTE,
             CLIE.DES_CLIENTE,
             CERT.BOL_TODOS_SECTORES,
             CERT.BOL_TODOS_CANALES
        FROM SAPR_TCERTIFICADO CERT
       INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
          ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLIE
          ON CONS.OID_CLIENTE = CLIE.OID_CLIENTE
       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
  
    /* SECTORES */
    OPEN P_SECTORES FOR
      SELECT PLANT.OID_PLANTA,
             PLANT.COD_PLANTA,
             PLANT.DES_PLANTA,
             SECT.OID_SECTOR,
             SECT.COD_SECTOR,
             SECT.DES_SECTOR
        FROM SAPR_TCERTIFICADOXSECTOR CESE
       INNER JOIN GEPR_TSECTOR SECT
          ON CESE.OID_SECTOR = SECT.OID_SECTOR
       INNER JOIN GEPR_TPLANTA PLANT
          ON SECT.OID_PLANTA = PLANT.OID_PLANTA
       WHERE CESE.OID_CERTIFICADO = P_IDENTIFICADOR
       ORDER BY PLANT.COD_PLANTA, SECT.DES_SECTOR;
  
    /* DELEGACIONES */
    OPEN P_DELEGACIONES FOR
      SELECT DE.OID_DELEGACION, DE.COD_DELEGACION, DE.DES_DELEGACION
        FROM SAPR_TCERTIFICADOXDELEGACION CEDE
       INNER JOIN GEPR_TDELEGACION DE
          ON DE.OID_DELEGACION = CEDE.OID_DELEGACION
       WHERE CEDE.OID_CERTIFICADO = P_IDENTIFICADOR
       ORDER BY DE.COD_DELEGACION, DE.DES_DELEGACION;
  
    /* SUBCANALES */
    OPEN P_SUBCANALES FOR
      SELECT SUCA.OID_SUBCANAL, SUCA.COD_SUBCANAL, SUCA.DES_SUBCANAL
        FROM SAPR_TCERTIFICADOXSUBCANAL CESC
       INNER JOIN GEPR_TSUBCANAL SUCA
          ON CESC.OID_SUBCANAL = SUCA.OID_SUBCANAL
       WHERE CESC.OID_CERTIFICADO = P_IDENTIFICADOR
       ORDER BY SUCA.DES_SUBCANAL;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SRECUPERAR_FILTROS_COD_CERT: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SRECUPERAR_FILTROS_COD_CERT;

  PROCEDURE SRECUPERAR_TRANSACCIONES(P_IDENTIFICADOR             IN OUT VARCHAR2,
                                     P_CODIGO                    IN OUT VARCHAR2,
                                     P_CULTURA                   IN VARCHAR2,
                                     P_TRANSACCIONES_EFECTIVO    OUT SYS_REFCURSOR,
                                     P_TRANSACCIONES_MEDIOS_PAGO OUT SYS_REFCURSOR,
                                     P_ERROR                     OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_EXISTE             NUMERIC := 0;
    V_COD_ESTADO         VARCHAR2(2) := '';
  BEGIN
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;
  
    IF P_IDENTIFICADOR IS NULL THEN
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE COD_CERTIFICADO = P_CODIGO;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_CODIGO,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.OID_CERTIFICADO, CERT.COD_ESTADO
        INTO P_IDENTIFICADOR, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       WHERE CERT.COD_CERTIFICADO = P_CODIGO;
    ELSE
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_IDENTIFICADOR || '" EN EL PARAMETRO "P_IDENTIFICADOR"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO_IDENTIFICADOR',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_IDENTIFICADOR,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.COD_CERTIFICADO, CERT.COD_ESTADO
        INTO P_CODIGO, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
    END IF;
  
    /* VERIFICA SI EL CERTIFICADO ES DE CONSULTA */
    IF V_COD_ESTADO = 'CO' THEN
    
      /* TRANSACCIONES EN EFECTIVO */
      OPEN P_TRANSACCIONES_EFECTIVO FOR
        SELECT TREF.OID_TRANSACCION_EFECTIVO,
               TREF.OID_DOCUMENTO,
               TREF.COD_ESTADO_DOCUMENTO,
               TREF.OID_CUENTA,
               TREF.OID_CUENTA_SALDO,
               TREF.OID_DIVISA,
               DIVI.COD_ISO_DIVISA,
               DIVI.DES_DIVISA,
               TREF.OID_DENOMINACION,
               DENO.COD_DENOMINACION,
               DENO.DES_DENOMINACION,
               TREF.COD_NIVEL_DETALLE,
               CASE TREF.COD_NIVEL_DETALLE
                 WHEN 'D' THEN
                  'DETALLADO'
                 WHEN 'T' THEN
                  'TOTAL EFECTIVO'
                 WHEN 'G' THEN
                  'TOTAL NO DEFINIDO'
                 ELSE
                  'ERROR'
               END DES_NIVEL_DETALLE,
               TREF.COD_TIPO_EFECTIVO_TOTAL,
               CASE TREF.COD_TIPO_EFECTIVO_TOTAL
                 WHEN 'A' THEN
                  'BILLETE Y MONEDA'
                 WHEN 'B' THEN
                  'BILLETE'
                 WHEN 'M' THEN
                  'MONEDA'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_EFECTIVO_TOTAL,
               TREF.BOL_DISPONIBLE,
               TREF.NUM_IMPORTE,
               TREF.NEL_CANTIDAD,
               TREF.COD_TIPO_MOVIMIENTO,
               CASE TREF.COD_TIPO_MOVIMIENTO
                 WHEN 'I' THEN
                  'INGRESO'
                 WHEN 'E' THEN
                  'EGRESO'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_MOVIMIENTO,
               TREF.COD_TIPO_SITIO,
               CASE TREF.COD_TIPO_SITIO
                 WHEN 'O' THEN
                  'ORIGEN'
                 WHEN 'D' THEN
                  'DESTINO'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_SITIO,
               TREF.OID_UNIDAD_MEDIDA,
               TREF.OID_CALIDAD,
               TREF.BOL_CONTRA_MOVIMIENTO
          FROM SAPR_TCERTIFICADO CERT
         INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
            ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
         INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
            ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
         INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
            ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
         INNER JOIN SAPR_TCUENTA CUEN
            ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
           AND CESE.OID_SECTOR = CUEN.OID_SECTOR
           AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
         INNER JOIN SAPR_TTRANSACCION_EFECTIVO TREF
            ON CUEN.OID_CUENTA = TREF.OID_CUENTA_SALDO
           AND TREF.COD_ESTADO_DOCUMENTO = 'AC'
         INNER JOIN SAPR_TDOCUMENTO DOCU
            ON CERT.FYH_CERTIFICADO >= DOCU.FYH_PLAN_CERTIFICACION
           AND TREF.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
           AND DOCU.BOL_CERTIFICADO = 0
         INNER JOIN SAPR_TTIPO_DOCUMENTO TIDO
            ON DOCU.OID_TIPO_DOCUMENTO = TIDO.OID_TIPO_DOCUMENTO
           AND TIDO.BOL_CERTIFICACION = 1
         INNER JOIN GEPR_TDIVISA DIVI
            ON TREF.OID_DIVISA = DIVI.OID_DIVISA
          LEFT OUTER JOIN GEPR_TDENOMINACION DENO
            ON DIVI.OID_DIVISA = DENO.OID_DIVISA
           AND TREF.OID_DENOMINACION = DENO.OID_DENOMINACION
         WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR
           AND CESE.OID_CERTIFICADO = P_IDENTIFICADOR
           AND CESC.OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* TRANSACCIONES EN MEDIOS DE PAGO */
      OPEN P_TRANSACCIONES_MEDIOS_PAGO FOR
        SELECT TRMP.OID_TRANSACCION_MEDIO_PAGO,
               TRMP.OID_DOCUMENTO,
               TRMP.COD_ESTADO_DOCUMENTO,
               TRMP.OID_CUENTA,
               TRMP.OID_CUENTA_SALDO,
               TRMP.OID_DIVISA,
               DIVI.COD_ISO_DIVISA,
               DIVI.DES_DIVISA,
               TRMP.OID_MEDIO_PAGO,
               MEPA.COD_MEDIO_PAGO,
               MEPA.DES_MEDIO_PAGO,
               TRMP.COD_TIPO_MEDIO_PAGO,
               CASE UPPER(TRMP.COD_TIPO_MEDIO_PAGO)
                 WHEN 'CODTIPO' THEN
                  'OTROS VALORES'
                 WHEN 'CODTIPOA' THEN
                  'TICKET'
                 WHEN 'CODTIPOB' THEN
                  'CHEQUE'
                 WHEN 'CODTIPOC' THEN
                  'TARJETA'
                 ELSE
                  'ERROR'
               END DES_TIPO_MEDIO_PAGO,
               TRMP.COD_NIVEL_DETALLE,
               CASE UPPER(TRMP.COD_NIVEL_DETALLE)
                 WHEN 'D' THEN
                  'DETALLADO'
                 WHEN 'T' THEN
                  CASE UPPER(TRMP.COD_TIPO_MEDIO_PAGO)
                    WHEN 'CODTIPO' THEN
                     'TOTAL OTROS VALORES'
                    WHEN 'CODTIPOA' THEN
                     'TOTAL TICKET'
                    WHEN 'CODTIPOB' THEN
                     'TOTAL CHEQUE'
                    WHEN 'CODTIPOC' THEN
                     'TOTAL TARJETA'
                    ELSE
                     'ERROR'
                  END
                 WHEN 'G' THEN
                  'TOTAL NO DEFINIDO'
                 ELSE
                  'ERROR'
               END DES_NIVEL_DETALLE,
               TRMP.BOL_DISPONIBLE,
               TRMP.NUM_IMPORTE,
               TRMP.NEL_CANTIDAD,
               TRMP.COD_TIPO_MOVIMIENTO,
               CASE TRMP.COD_TIPO_MOVIMIENTO
                 WHEN 'I' THEN
                  'INGRESO'
                 WHEN 'E' THEN
                  'EGRESO'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_MOVIMIENTO,
               TRMP.COD_TIPO_SITIO,
               CASE TRMP.COD_TIPO_SITIO
                 WHEN 'O' THEN
                  'ORIGEN'
                 WHEN 'D' THEN
                  'DESTINO'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_SITIO,
               TRMP.OID_UNIDAD_MEDIDA,
               TRMP.BOL_CONTRA_MOVIMIENTO
          FROM SAPR_TCERTIFICADO CERT
         INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
            ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
         INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
            ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
         INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
            ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
         INNER JOIN SAPR_TCUENTA CUEN
            ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
           AND CESE.OID_SECTOR = CUEN.OID_SECTOR
           AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
         INNER JOIN SAPR_TTRANSACCION_MEDIO_PAGO TRMP
            ON CUEN.OID_CUENTA = TRMP.OID_CUENTA_SALDO
           AND TRMP.COD_ESTADO_DOCUMENTO = 'AC'
         INNER JOIN SAPR_TDOCUMENTO DOCU
            ON CERT.FYH_CERTIFICADO >= DOCU.FYH_PLAN_CERTIFICACION
           AND TRMP.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
           AND DOCU.BOL_CERTIFICADO = 0
         INNER JOIN SAPR_TTIPO_DOCUMENTO TIDO
            ON DOCU.OID_TIPO_DOCUMENTO = TIDO.OID_TIPO_DOCUMENTO
           AND TIDO.BOL_CERTIFICACION = 1
         INNER JOIN GEPR_TDIVISA DIVI
            ON TRMP.OID_DIVISA = DIVI.OID_DIVISA
          LEFT OUTER JOIN GEPR_TMEDIO_PAGO MEPA
            ON DIVI.OID_DIVISA = MEPA.OID_DIVISA
           AND TRMP.OID_MEDIO_PAGO = MEPA.OID_MEDIO_PAGO
         WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR
           AND CESE.OID_CERTIFICADO = P_IDENTIFICADOR
           AND CESC.OID_CERTIFICADO = P_IDENTIFICADOR;
    
    ELSIF V_COD_ESTADO = 'DE' OR V_COD_ESTADO = 'PC' OR V_COD_ESTADO = 'PS' THEN
    
      /* CERTIFICADOS DEFINITIVOS Y PROVISIONALES */
    
      /* TRANSACCIONES EN EFECTIVO */
      OPEN P_TRANSACCIONES_EFECTIVO FOR
        SELECT TREF.OID_TRANSACCION_EFECTIVO,
               TREF.OID_DOCUMENTO,
               TREF.COD_ESTADO_DOCUMENTO,
               TREF.OID_CUENTA,
               TREF.OID_CUENTA_SALDO,
               TREF.OID_DIVISA,
               DIVI.COD_ISO_DIVISA,
               DIVI.DES_DIVISA,
               TREF.OID_DENOMINACION,
               DENO.COD_DENOMINACION,
               DENO.DES_DENOMINACION,
               TREF.COD_NIVEL_DETALLE,
               CASE TREF.COD_NIVEL_DETALLE
                 WHEN 'D' THEN
                  'DETALLADO'
                 WHEN 'T' THEN
                  'TOTAL EFECTIVO'
                 WHEN 'G' THEN
                  'TOTAL NO DEFINIDO'
                 ELSE
                  'ERROR'
               END DES_NIVEL_DETALLE,
               TREF.COD_TIPO_EFECTIVO_TOTAL,
               CASE TREF.COD_TIPO_EFECTIVO_TOTAL
                 WHEN 'A' THEN
                  'BILLETE Y MONEDA'
                 WHEN 'B' THEN
                  'BILLETE'
                 WHEN 'M' THEN
                  'MONEDA'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_EFECTIVO_TOTAL,
               TREF.BOL_DISPONIBLE,
               TREF.NUM_IMPORTE,
               TREF.NEL_CANTIDAD,
               TREF.COD_TIPO_MOVIMIENTO,
               CASE TREF.COD_TIPO_MOVIMIENTO
                 WHEN 'I' THEN
                  'INGRESO'
                 WHEN 'E' THEN
                  'EGRESO'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_MOVIMIENTO,
               TREF.COD_TIPO_SITIO,
               CASE TREF.COD_TIPO_SITIO
                 WHEN 'O' THEN
                  'ORIGEN'
                 WHEN 'D' THEN
                  'DESTINO'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_SITIO,
               TREF.OID_UNIDAD_MEDIDA,
               TREF.OID_CALIDAD,
               TREF.BOL_CONTRA_MOVIMIENTO
          FROM SAPR_TTRANSACCION_EFECTIVO TREF
         INNER JOIN GEPR_TDIVISA DIVI
            ON TREF.OID_DIVISA = DIVI.OID_DIVISA
          LEFT OUTER JOIN GEPR_TDENOMINACION DENO
            ON DIVI.OID_DIVISA = DENO.OID_DIVISA
           AND TREF.OID_DENOMINACION = DENO.OID_DENOMINACION
         WHERE TREF.OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* TRANSACCIONES EN MEDIOS DE PAGO */
      OPEN P_TRANSACCIONES_MEDIOS_PAGO FOR
        SELECT TRMP.OID_TRANSACCION_MEDIO_PAGO,
               TRMP.OID_DOCUMENTO,
               TRMP.COD_ESTADO_DOCUMENTO,
               TRMP.OID_CUENTA,
               TRMP.OID_CUENTA_SALDO,
               TRMP.OID_DIVISA,
               DIVI.COD_ISO_DIVISA,
               DIVI.DES_DIVISA,
               TRMP.OID_MEDIO_PAGO,
               MEPA.COD_MEDIO_PAGO,
               MEPA.DES_MEDIO_PAGO,
               TRMP.COD_TIPO_MEDIO_PAGO,
               CASE UPPER(TRMP.COD_TIPO_MEDIO_PAGO)
                 WHEN 'CODTIPO' THEN
                  'OTROS VALORES'
                 WHEN 'CODTIPOA' THEN
                  'TICKET'
                 WHEN 'CODTIPOB' THEN
                  'CHEQUE'
                 WHEN 'CODTIPOC' THEN
                  'TARJETA'
                 ELSE
                  'ERROR'
               END DES_TIPO_MEDIO_PAGO,
               TRMP.COD_NIVEL_DETALLE,
               CASE UPPER(TRMP.COD_NIVEL_DETALLE)
                 WHEN 'D' THEN
                  'DETALLADO'
                 WHEN 'T' THEN
                  CASE UPPER(TRMP.COD_TIPO_MEDIO_PAGO)
                    WHEN 'CODTIPO' THEN
                     'TOTAL OTROS VALORES'
                    WHEN 'CODTIPOA' THEN
                     'TOTAL TICKET'
                    WHEN 'CODTIPOB' THEN
                     'TOTAL CHEQUE'
                    WHEN 'CODTIPOC' THEN
                     'TOTAL TARJETA'
                    ELSE
                     'ERROR'
                  END
                 WHEN 'G' THEN
                  'TOTAL NO DEFINIDO'
                 ELSE
                  'ERROR'
               END DES_NIVEL_DETALLE,
               TRMP.BOL_DISPONIBLE,
               TRMP.NUM_IMPORTE,
               TRMP.NEL_CANTIDAD,
               TRMP.COD_TIPO_MOVIMIENTO,
               CASE TRMP.COD_TIPO_MOVIMIENTO
                 WHEN 'I' THEN
                  'INGRESO'
                 WHEN 'E' THEN
                  'EGRESO'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_MOVIMIENTO,
               TRMP.COD_TIPO_SITIO,
               CASE TRMP.COD_TIPO_SITIO
                 WHEN 'O' THEN
                  'ORIGEN'
                 WHEN 'D' THEN
                  'DESTINO'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_SITIO,
               TRMP.OID_UNIDAD_MEDIDA,
               TRMP.BOL_CONTRA_MOVIMIENTO
          FROM SAPR_TTRANSACCION_MEDIO_PAGO TRMP
         INNER JOIN GEPR_TDIVISA DIVI
            ON TRMP.OID_DIVISA = DIVI.OID_DIVISA
          LEFT OUTER JOIN GEPR_TMEDIO_PAGO MEPA
            ON DIVI.OID_DIVISA = MEPA.OID_DIVISA
           AND TRMP.OID_MEDIO_PAGO = MEPA.OID_MEDIO_PAGO
         WHERE TRMP.OID_CERTIFICADO = P_IDENTIFICADOR;
    
    END IF;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SRECUPERAR_TRANSACCIONES: ' || SQLCODE || ' - ' ||
                 SQLERRM;
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SRECUPERAR_TRANSACCIONES;

  PROCEDURE SRECUPERAR_SALDOS(P_IDENTIFICADOR      IN OUT VARCHAR2,
                              P_CODIGO             IN OUT VARCHAR2,
                              P_CULTURA            IN VARCHAR2,
                              P_SALDOS_EFECTIVO    OUT SYS_REFCURSOR,
                              P_SALDOS_MEDIOS_PAGO OUT SYS_REFCURSOR,
                              P_ERROR              OUT VARCHAR2) AS
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
    V_EXISTE             NUMERIC := 0;
    V_COD_ESTADO         VARCHAR2(2) := '';
  BEGIN
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;
  
    IF P_IDENTIFICADOR IS NULL THEN
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE COD_CERTIFICADO = P_CODIGO;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_CODIGO,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.OID_CERTIFICADO, CERT.COD_ESTADO
        INTO P_IDENTIFICADOR, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       WHERE CERT.COD_CERTIFICADO = P_CODIGO;
    ELSE
      SELECT COUNT(1)
        INTO V_EXISTE
        FROM SAPR_TCERTIFICADO
       WHERE OID_CERTIFICADO = P_IDENTIFICADOR;
      IF V_EXISTE = 0 THEN
        CUSTOM_ERROR_CODE := -20001;
        /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_IDENTIFICADOR || '" EN EL PARAMETRO "P_IDENTIFICADOR"'; */
        CUSTOM_ERROR_MESSAGE := FN_TRADUZIR_###VERSION###(P_CULTURA,
                                            'MSG_CERTIFICADO_NO_ENCONTRADO_IDENTIFICADOR',
                                            'CERTIFICACION',
                                            'GenesisSaldos',
                                            P_IDENTIFICADOR,
                                            1);
        RAISE CUSTOM_ERROR;
      END IF;
      /* DETALLES */
      SELECT CERT.COD_CERTIFICADO, CERT.COD_ESTADO
        INTO P_CODIGO, V_COD_ESTADO
        FROM SAPR_TCERTIFICADO CERT
       WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR;
    END IF;
  
    /* VERIFICA SI EL CERTIFICADO ES DE CONSULTA */
    IF V_COD_ESTADO = 'CO' THEN
    
      /* SALDOS EN EFECTIVO */
      OPEN P_SALDOS_EFECTIVO FOR
        WITH SALDO_ANTERIOR_LLAVE AS
         (SELECT MAX(CSEF.GMT_CREACION) GMT_CREACION,
                 CSEF.OID_CUENTA_SALDO,
                 CSEF.OID_DIVISA,
                 CSEF.OID_DENOMINACION,
                 CSEF.COD_NIVEL_DETALLE,
                 CSEF.COD_TIPO_EFECTIVO_TOTAL,
                 CSEF.BOL_DISPONIBLE,
                 CSEF.OID_UNIDAD_MEDIDA,
                 CSEF.OID_CALIDAD
            FROM SAPR_TCERTIFICADO CERT
           INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
              ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
           INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
              ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
           INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
              ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
           INNER JOIN SAPR_TCUENTA CUEN
              ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
             AND CESE.OID_SECTOR = CUEN.OID_SECTOR
             AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
           INNER JOIN SAPR_TCERT_SALDO_EFECTIVO CSEF
              ON CUEN.OID_CUENTA = CSEF.OID_CUENTA_SALDO
           INNER JOIN SAPR_TCERTIFICADO CEAN
              ON CSEF.OID_CERTIFICADO = CEAN.OID_CERTIFICADO
             AND CEAN.COD_ESTADO = 'DE'
           WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR
             AND CESE.OID_CERTIFICADO = P_IDENTIFICADOR
             AND CESC.OID_CERTIFICADO = P_IDENTIFICADOR
           GROUP BY CSEF.OID_CUENTA_SALDO,
                    CSEF.OID_DIVISA,
                    CSEF.OID_DENOMINACION,
                    CSEF.COD_NIVEL_DETALLE,
                    CSEF.COD_TIPO_EFECTIVO_TOTAL,
                    CSEF.BOL_DISPONIBLE,
                    CSEF.OID_UNIDAD_MEDIDA,
                    CSEF.OID_CALIDAD),
        SALDO_ANTERIOR AS
         (SELECT SAEF.OID_CERTIFICADO,
                 SAEF.OID_CUENTA_SALDO,
                 SAEF.OID_DIVISA,
                 SAEF.OID_DENOMINACION,
                 SAEF.COD_NIVEL_DETALLE,
                 SAEF.COD_TIPO_EFECTIVO_TOTAL,
                 SAEF.BOL_DISPONIBLE,
                 0 AS NUM_IMPORTE,
                 (NVL(SAEF.NUM_IMPORTE, 0) +
                 NVL(SAEF.NUM_IMPORTE_ACUMULADO, 0)) AS NUM_IMPORTE_ACUMULADO,
                 0 AS NEL_CANTIDAD,
                 (NVL(SAEF.NEL_CANTIDAD, 0) +
                 NVL(SAEF.NEL_CANTIDAD_ACUMULADO, 0)) AS NEL_CANTIDAD_ACUMULADO,
                 SAEF.OID_UNIDAD_MEDIDA,
                 SAEF.OID_CALIDAD
            FROM SAPR_TCERT_SALDO_EFECTIVO SAEF
           INNER JOIN SALDO_ANTERIOR_LLAVE TEMP
              ON SAEF.GMT_CREACION = TEMP.GMT_CREACION
             AND SAEF.OID_CUENTA_SALDO = TEMP.OID_CUENTA_SALDO
             AND NVL(SAEF.OID_DENOMINACION, '-') =
                 NVL(TEMP.OID_DENOMINACION, '-')
             AND SAEF.OID_DIVISA = TEMP.OID_DIVISA
             AND SAEF.COD_NIVEL_DETALLE = TEMP.COD_NIVEL_DETALLE
             AND NVL(SAEF.COD_TIPO_EFECTIVO_TOTAL, '-') =
                 NVL(TEMP.COD_TIPO_EFECTIVO_TOTAL, '-')
             AND SAEF.BOL_DISPONIBLE = TEMP.BOL_DISPONIBLE
             AND SAEF.OID_UNIDAD_MEDIDA = TEMP.OID_UNIDAD_MEDIDA
             AND NVL(SAEF.OID_CALIDAD, '-') = NVL(TEMP.OID_CALIDAD, '-')),
        SALDO_ACTUAL AS
         (SELECT NULL                         OID_CERTIFICADO,
                 TREF.OID_CUENTA_SALDO,
                 TREF.OID_DIVISA,
                 TREF.OID_DENOMINACION,
                 TREF.COD_NIVEL_DETALLE,
                 TREF.COD_TIPO_EFECTIVO_TOTAL,
                 TREF.BOL_DISPONIBLE,
                 TREF.NUM_IMPORTE,
                 0                            AS NUM_IMPORTE_ACUMULADO,
                 TREF.NEL_CANTIDAD,
                 0                            AS NEL_CANTIDAD_ACUMULADO,
                 TREF.OID_UNIDAD_MEDIDA,
                 TREF.OID_CALIDAD
            FROM SAPR_TCERTIFICADO CERT
           INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
              ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
           INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
              ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
           INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
              ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
           INNER JOIN SAPR_TCUENTA CUEN
              ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
             AND CESE.OID_SECTOR = CUEN.OID_SECTOR
             AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
           INNER JOIN SAPR_TTRANSACCION_EFECTIVO TREF
              ON CUEN.OID_CUENTA = TREF.OID_CUENTA_SALDO
             AND TREF.COD_ESTADO_DOCUMENTO = 'AC'
           INNER JOIN SAPR_TDOCUMENTO DOCU
              ON TREF.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
             AND CERT.FYH_CERTIFICADO >= DOCU.FYH_PLAN_CERTIFICACION
             AND DOCU.BOL_CERTIFICADO = 0
           INNER JOIN SAPR_TTIPO_DOCUMENTO TIDO
              ON DOCU.OID_TIPO_DOCUMENTO = TIDO.OID_TIPO_DOCUMENTO
             AND TIDO.BOL_CERTIFICACION = 1
           WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR
             AND CESE.OID_CERTIFICADO = P_IDENTIFICADOR
             AND CESC.OID_CERTIFICADO = P_IDENTIFICADOR)
        SELECT MAX(SAEF.OID_CERTIFICADO) OID_CERTIFICADO_ANTERIOR,
               SAEF.OID_CUENTA_SALDO,
               SAEF.OID_DIVISA,
               DIVI.COD_ISO_DIVISA,
               DIVI.DES_DIVISA,
               SAEF.OID_DENOMINACION,
               DENO.COD_DENOMINACION,
               DENO.DES_DENOMINACION,
               SAEF.COD_NIVEL_DETALLE,
               CASE SAEF.COD_NIVEL_DETALLE
                 WHEN 'D' THEN
                  'DETALLADO'
                 WHEN 'T' THEN
                  'TOTAL EFECTIVO'
                 WHEN 'G' THEN
                  'TOTAL NO DEFINIDO'
                 ELSE
                  'ERROR'
               END DES_NIVEL_DETALLE,
               SAEF.COD_TIPO_EFECTIVO_TOTAL,
               CASE SAEF.COD_TIPO_EFECTIVO_TOTAL
                 WHEN 'A' THEN
                  'BILLETE Y MONEDA'
                 WHEN 'B' THEN
                  'BILLETE'
                 WHEN 'M' THEN
                  'MONEDA'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_EFECTIVO_TOTAL,
               SAEF.BOL_DISPONIBLE,
               SUM(SAEF.NUM_IMPORTE) NUM_IMPORTE,
               SUM(SAEF.NUM_IMPORTE_ACUMULADO) NUM_IMPORTE_ACUMULADO,
               SUM(SAEF.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(SAEF.NEL_CANTIDAD_ACUMULADO) NEL_CANTIDAD_ACUMULADO,
               SAEF.OID_UNIDAD_MEDIDA,
               SAEF.OID_CALIDAD
          FROM (SELECT *
                  FROM SALDO_ANTERIOR
                UNION ALL
                SELECT * FROM SALDO_ACTUAL) SAEF
         INNER JOIN GEPR_TDIVISA DIVI
            ON SAEF.OID_DIVISA = DIVI.OID_DIVISA
          LEFT OUTER JOIN GEPR_TDENOMINACION DENO
            ON DIVI.OID_DIVISA = DENO.OID_DIVISA
           AND SAEF.OID_DENOMINACION = DENO.OID_DENOMINACION
         GROUP BY SAEF.OID_CUENTA_SALDO,
                  SAEF.OID_DIVISA,
                  DIVI.COD_ISO_DIVISA,
                  DIVI.DES_DIVISA,
                  SAEF.OID_DENOMINACION,
                  DENO.COD_DENOMINACION,
                  DENO.DES_DENOMINACION,
                  SAEF.COD_NIVEL_DETALLE,
                  CASE SAEF.COD_NIVEL_DETALLE
                    WHEN 'D' THEN
                     'DETALLADO'
                    WHEN 'T' THEN
                     'TOTAL EFECTIVO'
                    WHEN 'G' THEN
                     'TOTAL NO DEFINIDO'
                    ELSE
                     'ERROR'
                  END,
                  SAEF.COD_TIPO_EFECTIVO_TOTAL,
                  CASE SAEF.COD_TIPO_EFECTIVO_TOTAL
                    WHEN 'A' THEN
                     'BILLETE Y MONEDA'
                    WHEN 'B' THEN
                     'BILLETE'
                    WHEN 'M' THEN
                     'MONEDA'
                    ELSE
                     'NO DEFINIDO'
                  END,
                  SAEF.BOL_DISPONIBLE,
                  SAEF.OID_UNIDAD_MEDIDA,
                  SAEF.OID_CALIDAD;
    
      /* SALDOS EN MEDIOS DE PAGO */
      OPEN P_SALDOS_MEDIOS_PAGO FOR
        WITH SALDO_ANTERIOR_LLAVE AS
         (SELECT MAX(CSMP.GMT_CREACION) GMT_CREACION,
                 CSMP.OID_CUENTA_SALDO,
                 CSMP.OID_DIVISA,
                 CSMP.OID_MEDIO_PAGO,
                 CSMP.COD_TIPO_MEDIO_PAGO,
                 CSMP.COD_NIVEL_DETALLE,
                 CSMP.BOL_DISPONIBLE,
                 CSMP.OID_UNIDAD_MEDIDA
            FROM SAPR_TCERTIFICADO CERT
           INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
              ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
           INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
              ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
           INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
              ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
           INNER JOIN SAPR_TCUENTA CUEN
              ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
             AND CESE.OID_SECTOR = CUEN.OID_SECTOR
             AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
           INNER JOIN SAPR_TCERT_SALDO_MEDIO_PAGO CSMP
              ON CUEN.OID_CUENTA = CSMP.OID_CUENTA_SALDO
           INNER JOIN SAPR_TCERTIFICADO CEAN
              ON CSMP.OID_CERTIFICADO = CEAN.OID_CERTIFICADO
             AND CEAN.COD_ESTADO = 'DE'
           WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR
             AND CESE.OID_CERTIFICADO = P_IDENTIFICADOR
             AND CESC.OID_CERTIFICADO = P_IDENTIFICADOR
           GROUP BY CSMP.OID_CUENTA_SALDO,
                    CSMP.OID_DIVISA,
                    CSMP.OID_MEDIO_PAGO,
                    CSMP.COD_TIPO_MEDIO_PAGO,
                    CSMP.COD_NIVEL_DETALLE,
                    CSMP.BOL_DISPONIBLE,
                    CSMP.OID_UNIDAD_MEDIDA),
        SALDO_ANTERIOR AS
         (SELECT SAMP.OID_CERTIFICADO,
                 SAMP.OID_CUENTA_SALDO,
                 SAMP.OID_DIVISA,
                 SAMP.OID_MEDIO_PAGO,
                 SAMP.COD_TIPO_MEDIO_PAGO,
                 SAMP.COD_NIVEL_DETALLE,
                 SAMP.BOL_DISPONIBLE,
                 0 AS NUM_IMPORTE,
                 (NVL(SAMP.NUM_IMPORTE, 0) +
                 NVL(SAMP.NUM_IMPORTE_ACUMULADO, 0)) AS NUM_IMPORTE_ACUMULADO,
                 0 AS NEL_CANTIDAD,
                 (NVL(SAMP.NEL_CANTIDAD, 0) +
                 NVL(SAMP.NEL_CANTIDAD_ACUMULADO, 0)) AS NEL_CANTIDAD_ACUMULADO,
                 SAMP.OID_UNIDAD_MEDIDA
            FROM SAPR_TCERT_SALDO_MEDIO_PAGO SAMP
           INNER JOIN SALDO_ANTERIOR_LLAVE TEMP
              ON SAMP.GMT_CREACION = TEMP.GMT_CREACION
             AND SAMP.OID_CUENTA_SALDO = TEMP.OID_CUENTA_SALDO
             AND NVL(SAMP.OID_MEDIO_PAGO, '-') =
                 NVL(TEMP.OID_MEDIO_PAGO, '-')
             AND SAMP.OID_DIVISA = TEMP.OID_DIVISA
             AND SAMP.COD_TIPO_MEDIO_PAGO = TEMP.COD_TIPO_MEDIO_PAGO
             AND SAMP.COD_NIVEL_DETALLE = TEMP.COD_NIVEL_DETALLE
             AND SAMP.BOL_DISPONIBLE = TEMP.BOL_DISPONIBLE
             AND NVL(SAMP.OID_UNIDAD_MEDIDA, '-') =
                 NVL(TEMP.OID_UNIDAD_MEDIDA, '-')),
        SALDO_ACTUAL AS
         (SELECT NULL                     OID_CERTIFICADO,
                 TRMP.OID_CUENTA_SALDO,
                 TRMP.OID_DIVISA,
                 TRMP.OID_MEDIO_PAGO,
                 TRMP.COD_TIPO_MEDIO_PAGO,
                 TRMP.COD_NIVEL_DETALLE,
                 TRMP.BOL_DISPONIBLE,
                 TRMP.NUM_IMPORTE,
                 0                        AS NUM_IMPORTE_ACUMULADO,
                 TRMP.NEL_CANTIDAD,
                 0                        AS NEL_CANTIDAD_ACUMULADO,
                 TRMP.OID_UNIDAD_MEDIDA
            FROM SAPR_TCERTIFICADO CERT
           INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
              ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
           INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
              ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
           INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
              ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
           INNER JOIN SAPR_TCUENTA CUEN
              ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
             AND CESE.OID_SECTOR = CUEN.OID_SECTOR
             AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
           INNER JOIN SAPR_TTRANSACCION_MEDIO_PAGO TRMP
              ON CUEN.OID_CUENTA = TRMP.OID_CUENTA_SALDO
             AND TRMP.COD_ESTADO_DOCUMENTO = 'AC'
           INNER JOIN SAPR_TDOCUMENTO DOCU
              ON CERT.FYH_CERTIFICADO >= DOCU.FYH_PLAN_CERTIFICACION
             AND TRMP.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
             AND DOCU.BOL_CERTIFICADO = 0
           INNER JOIN SAPR_TTIPO_DOCUMENTO TIDO
              ON DOCU.OID_TIPO_DOCUMENTO = TIDO.OID_TIPO_DOCUMENTO
             AND TIDO.BOL_CERTIFICACION = 1
           WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR
             AND CESE.OID_CERTIFICADO = P_IDENTIFICADOR
             AND CESC.OID_CERTIFICADO = P_IDENTIFICADOR)
        SELECT MAX(SAMP.OID_CERTIFICADO) OID_CERTIFICADO_ANTERIOR,
               SAMP.OID_CUENTA_SALDO,
               SAMP.OID_DIVISA,
               DIVI.COD_ISO_DIVISA,
               DIVI.DES_DIVISA,
               SAMP.OID_MEDIO_PAGO,
               MEPA.COD_MEDIO_PAGO,
               MEPA.DES_MEDIO_PAGO,
               SAMP.COD_TIPO_MEDIO_PAGO,
               CASE UPPER(SAMP.COD_TIPO_MEDIO_PAGO)
                 WHEN 'CODTIPO' THEN
                  'OTROS VALORES'
                 WHEN 'CODTIPOA' THEN
                  'TICKET'
                 WHEN 'CODTIPOB' THEN
                  'CHEQUE'
                 WHEN 'CODTIPOC' THEN
                  'TARJETA'
                 ELSE
                  'ERROR'
               END,
               SAMP.COD_NIVEL_DETALLE,
               CASE UPPER(SAMP.COD_NIVEL_DETALLE)
                 WHEN 'D' THEN
                  'DETALLADO'
                 WHEN 'T' THEN
                  CASE UPPER(SAMP.COD_TIPO_MEDIO_PAGO)
                    WHEN 'CODTIPO' THEN
                     'TOTAL OTROS VALORES'
                    WHEN 'CODTIPOA' THEN
                     'TOTAL TICKET'
                    WHEN 'CODTIPOB' THEN
                     'TOTAL CHEQUE'
                    WHEN 'CODTIPOC' THEN
                     'TOTAL TARJETA'
                    ELSE
                     'ERROR'
                  END
                 WHEN 'G' THEN
                  'TOTAL NO DEFINIDO'
                 ELSE
                  'ERROR'
               END,
               SAMP.BOL_DISPONIBLE,
               SUM(SAMP.NUM_IMPORTE) NUM_IMPORTE,
               SUM(SAMP.NUM_IMPORTE_ACUMULADO) NUM_IMPORTE_ACUMULADO,
               SUM(SAMP.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(SAMP.NEL_CANTIDAD_ACUMULADO) NEL_CANTIDAD_ACUMULADO,
               SAMP.OID_UNIDAD_MEDIDA
          FROM (SELECT *
                  FROM SALDO_ANTERIOR
                UNION ALL
                SELECT * FROM SALDO_ACTUAL) SAMP
         INNER JOIN GEPR_TDIVISA DIVI
            ON SAMP.OID_DIVISA = DIVI.OID_DIVISA
          LEFT OUTER JOIN GEPR_TMEDIO_PAGO MEPA
            ON DIVI.OID_DIVISA = MEPA.OID_DIVISA
           AND SAMP.OID_MEDIO_PAGO = MEPA.OID_MEDIO_PAGO
         GROUP BY SAMP.OID_CUENTA_SALDO,
                  SAMP.OID_DIVISA,
                  DIVI.COD_ISO_DIVISA,
                  DIVI.DES_DIVISA,
                  SAMP.OID_MEDIO_PAGO,
                  MEPA.COD_MEDIO_PAGO,
                  MEPA.DES_MEDIO_PAGO,
                  SAMP.COD_TIPO_MEDIO_PAGO,
                  CASE UPPER(SAMP.COD_TIPO_MEDIO_PAGO)
                    WHEN 'CODTIPO' THEN
                     'OTROS VALORES'
                    WHEN 'CODTIPOA' THEN
                     'TICKET'
                    WHEN 'CODTIPOB' THEN
                     'CHEQUE'
                    WHEN 'CODTIPOC' THEN
                     'TARJETA'
                    ELSE
                     'ERROR'
                  END,
                  SAMP.COD_NIVEL_DETALLE,
                  CASE UPPER(SAMP.COD_NIVEL_DETALLE)
                    WHEN 'D' THEN
                     'DETALLADO'
                    WHEN 'T' THEN
                     CASE UPPER(SAMP.COD_TIPO_MEDIO_PAGO)
                       WHEN 'CODTIPO' THEN
                        'TOTAL OTROS VALORES'
                       WHEN 'CODTIPOA' THEN
                        'TOTAL TICKET'
                       WHEN 'CODTIPOB' THEN
                        'TOTAL CHEQUE'
                       WHEN 'CODTIPOC' THEN
                        'TOTAL TARJETA'
                       ELSE
                        'ERROR'
                     END
                    WHEN 'G' THEN
                     'TOTAL NO DEFINIDO'
                    ELSE
                     'ERROR'
                  END,
                  SAMP.BOL_DISPONIBLE,
                  SAMP.OID_UNIDAD_MEDIDA;
    
    ELSIF V_COD_ESTADO = 'DE' OR V_COD_ESTADO = 'PC' OR V_COD_ESTADO = 'PS' THEN
    
      /* CERTIFICADOS DEFINITIVOS Y PROVISIONALES */
    
      /* SALDOS EN EFECTIVO */
      OPEN P_SALDOS_EFECTIVO FOR
        SELECT SAEF.OID_CERTIFICADO_ANTERIOR,
               SAEF.OID_CUENTA_SALDO,
               SAEF.OID_DIVISA,
               DIVI.COD_ISO_DIVISA,
               DIVI.DES_DIVISA,
               SAEF.OID_DENOMINACION,
               DENO.COD_DENOMINACION,
               DENO.DES_DENOMINACION,
               SAEF.COD_NIVEL_DETALLE,
               CASE SAEF.COD_NIVEL_DETALLE
                 WHEN 'D' THEN
                  'DETALLADO'
                 WHEN 'T' THEN
                  'TOTAL EFECTIVO'
                 WHEN 'G' THEN
                  'TOTAL NO DEFINIDO'
                 ELSE
                  'ERROR'
               END DES_NIVEL_DETALLE,
               SAEF.COD_TIPO_EFECTIVO_TOTAL,
               CASE SAEF.COD_TIPO_EFECTIVO_TOTAL
                 WHEN 'A' THEN
                  'BILLETE Y MONEDA'
                 WHEN 'B' THEN
                  'BILLETE'
                 WHEN 'M' THEN
                  'MONEDA'
                 ELSE
                  'NO DEFINIDO'
               END DES_TIPO_EFECTIVO_TOTAL,
               SAEF.BOL_DISPONIBLE,
               SAEF.NUM_IMPORTE,
               SAEF.NUM_IMPORTE_ACUMULADO,
               SAEF.NEL_CANTIDAD,
               SAEF.NEL_CANTIDAD_ACUMULADO,
               SAEF.OID_UNIDAD_MEDIDA,
               SAEF.OID_CALIDAD
          FROM SAPR_TCERT_SALDO_EFECTIVO SAEF
         INNER JOIN GEPR_TDIVISA DIVI
            ON SAEF.OID_DIVISA = DIVI.OID_DIVISA
          LEFT OUTER JOIN GEPR_TDENOMINACION DENO
            ON DIVI.OID_DIVISA = DENO.OID_DIVISA
           AND SAEF.OID_DENOMINACION = DENO.OID_DENOMINACION
         WHERE SAEF.OID_CERTIFICADO = P_IDENTIFICADOR;
    
      /* SALDOS EN MEDIOS DE PAGO */
      OPEN P_SALDOS_MEDIOS_PAGO FOR
        SELECT SAMP.OID_CERTIFICADO_ANTERIOR,
               SAMP.OID_CUENTA_SALDO,
               SAMP.OID_DIVISA,
               DIVI.COD_ISO_DIVISA,
               DIVI.DES_DIVISA,
               SAMP.OID_MEDIO_PAGO,
               MEPA.COD_MEDIO_PAGO,
               MEPA.DES_MEDIO_PAGO,
               SAMP.COD_TIPO_MEDIO_PAGO,
               CASE UPPER(SAMP.COD_TIPO_MEDIO_PAGO)
                 WHEN 'CODTIPO' THEN
                  'OTROS VALORES'
                 WHEN 'CODTIPOA' THEN
                  'TICKET'
                 WHEN 'CODTIPOB' THEN
                  'CHEQUE'
                 WHEN 'CODTIPOC' THEN
                  'TARJETA'
                 ELSE
                  'ERROR'
               END DES_TIPO_MEDIO_PAGO,
               SAMP.COD_NIVEL_DETALLE,
               CASE UPPER(SAMP.COD_NIVEL_DETALLE)
                 WHEN 'D' THEN
                  'DETALLADO'
                 WHEN 'T' THEN
                  CASE UPPER(SAMP.COD_TIPO_MEDIO_PAGO)
                    WHEN 'CODTIPO' THEN
                     'TOTAL OTROS VALORES'
                    WHEN 'CODTIPOA' THEN
                     'TOTAL TICKET'
                    WHEN 'CODTIPOB' THEN
                     'TOTAL CHEQUE'
                    WHEN 'CODTIPOC' THEN
                     'TOTAL TARJETA'
                    ELSE
                     'ERROR'
                  END
                 WHEN 'G' THEN
                  'TOTAL NO DEFINIDO'
                 ELSE
                  'ERROR'
               END DES_NIVEL_DETALLE,
               SAMP.BOL_DISPONIBLE,
               SAMP.NUM_IMPORTE,
               SAMP.NUM_IMPORTE_ACUMULADO,
               SAMP.NEL_CANTIDAD,
               SAMP.NEL_CANTIDAD_ACUMULADO,
               SAMP.OID_UNIDAD_MEDIDA
          FROM SAPR_TCERT_SALDO_MEDIO_PAGO SAMP
         INNER JOIN GEPR_TDIVISA DIVI
            ON SAMP.OID_DIVISA = DIVI.OID_DIVISA
          LEFT OUTER JOIN GEPR_TMEDIO_PAGO MEPA
            ON DIVI.OID_DIVISA = MEPA.OID_DIVISA
           AND SAMP.OID_MEDIO_PAGO = MEPA.OID_MEDIO_PAGO
         WHERE SAMP.OID_CERTIFICADO = P_IDENTIFICADOR;
    
    END IF;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SRECUPERAR_SALDOS: ' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SRECUPERAR_SALDOS;

  PROCEDURE SDEBUG(P_IDENTIFICADOR IN VARCHAR2, P_MENSAJE IN CLOB) AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
  BEGIN
  
    /* SCRIPT DE GENERACION DE LA TABLA DE DEBUG (LOG): */
    /* CREATE TABLE SAPR_TDEBUG (IDENTIFICADOR VARCHAR2(36) DEFAULT SYS_GUID() NOT NULL, FECHA_HORA TIMESTAMP DEFAULT (SYSDATE) NOT NULL, MENSAJE CLOB NOT NULL) */
  
    IF P_IDENTIFICADOR IS NOT NULL THEN
      INSERT INTO SAPR_TDEBUG
        (IDENTIFICADOR, MENSAJE)
      VALUES
        (P_IDENTIFICADOR, P_MENSAJE);
      COMMIT;
    END IF;
  
    /* ERRO DE DEADLOCK */
    IF INSTR(P_MENSAJE, 'ORA-00060') > 0 THEN
      CUSTOM_ERROR_MESSAGE := gepr_putilidades_###VERSION###.ftraduzir(VAR$CULTURA,
                                                                  'CERTIFICACION_INTERBLOQUEO',
                                                                  'CERTIFICACION',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  1);
      RAISE_APPLICATION_ERROR(-20001, CUSTOM_ERROR_MESSAGE);
    END IF;
  
  END SDEBUG;

  PROCEDURE VALIDAR_GENERACION_REPORTE_URU(NOMBRE_PROCEDURE OUT VARCHAR2,
                                           P_ERROR          OUT VARCHAR2) AS
  
    V_EXISTE                     NUMERIC := 0;
    V_NOMBRE_PROCEDURE_PARAMETRO VARCHAR2(200) := '';
    V_NOMBRE_PACKAGE             VARCHAR2(200) := '';
    V_NOMBRE_PROCEDURE           VARCHAR2(200) := '';
  
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
  
  BEGIN
  
    SELECT COUNT(*)
      INTO V_EXISTE
      FROM GEPR_TPARAMETRO P
     INNER JOIN GEPR_TPARAMETRO_VALOR PV
        ON PV.OID_PARAMETRO = P.OID_PARAMETRO
     INNER JOIN GEPR_TAPLICACION A
        ON A.OID_APLICACION = P.OID_APLICACION
     WHERE A.COD_APLICACION = 'GenesisSaldos'
       AND P.COD_PARAMETRO = 'ProcGenerarDatosAdicionalesCertificacion';
  
    /* VERIFICA SE O PARAMETRO EXISTE */
    IF V_EXISTE > 0 THEN
    
      SELECT CASE
               WHEN PV.DES_VALOR_PARAMETRO IS NULL THEN
                ''
               ELSE
                PV.DES_VALOR_PARAMETRO
             END
        INTO V_NOMBRE_PROCEDURE_PARAMETRO
        FROM GEPR_TPARAMETRO P
       INNER JOIN GEPR_TPARAMETRO_VALOR PV
          ON PV.OID_PARAMETRO = P.OID_PARAMETRO
       INNER JOIN GEPR_TAPLICACION A
          ON A.OID_APLICACION = P.OID_APLICACION
       WHERE A.COD_APLICACION = 'GenesisSaldos'
         AND P.COD_PARAMETRO = 'ProcGenerarDatosAdicionalesCertificacion'
         AND ROWNUM = 1;
    
      /* VERIFICA SE O PARAMETRO ESTA PREENCHIDO */
      IF V_NOMBRE_PROCEDURE_PARAMETRO IS NOT NULL THEN
      
        /* RECUPERA O NOME DA PACKAGE OU PROCEDURE */
        SELECT REGEXP_SUBSTR(V_NOMBRE_PROCEDURE_PARAMETRO, '[^.]+', 1, 1)
          INTO V_NOMBRE_PACKAGE
          FROM DUAL;
        /* RECUPERA O NOME DA PROCEDURE */
        SELECT REGEXP_SUBSTR(V_NOMBRE_PROCEDURE_PARAMETRO, '[^.]+', 1, 2)
          INTO V_NOMBRE_PROCEDURE
          FROM DUAL;
        /* SE HÁ PACKAGE E PRCEDURE */
        IF V_NOMBRE_PACKAGE IS NOT NULL AND V_NOMBRE_PROCEDURE IS NOT NULL THEN
        
          SELECT COUNT(*)
            INTO V_EXISTE
            FROM ALL_SYNONYMS S
           WHERE S.SYNONYM_NAME = V_NOMBRE_PACKAGE;
          /* VALIDA SE O SINONIMO EXISTE, O GRANT É DADO PARA A PACKAGE E NÃO PARA A PROCEDURE */
          IF V_EXISTE = 0 THEN
            CUSTOM_ERROR_CODE := -20001;
            /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
            CUSTOM_ERROR_MESSAGE := 'Sinónimo: ' || V_NOMBRE_PACKAGE ||
                                    ' no existe';
            RAISE CUSTOM_ERROR;
          END IF;
        
          SELECT COUNT(*)
            INTO V_EXISTE
            FROM ALL_PROCEDURES P
           WHERE P.PROCEDURE_NAME = V_NOMBRE_PROCEDURE;
          /* VALIDA SE A PROCEDURE EXISTE */
          IF V_EXISTE = 0 THEN
            CUSTOM_ERROR_CODE := -20001;
            /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
            CUSTOM_ERROR_MESSAGE := 'Procedure: ' || V_NOMBRE_PROCEDURE ||
                                    ' no existe';
            RAISE CUSTOM_ERROR;
          END IF;
        
          NOMBRE_PROCEDURE := V_NOMBRE_PROCEDURE_PARAMETRO;
        
        ELSE
          IF V_NOMBRE_PACKAGE IS NOT NULL THEN
            SELECT COUNT(*)
              INTO V_EXISTE
              FROM ALL_SYNONYMS S
             WHERE S.SYNONYM_NAME = V_NOMBRE_PACKAGE;
            /* VALIDA SE O SINONIMO EXISTE
            QUANDO A APENAS A PROCEDURE, SEM PACKAGE, O SINONIMO JA E A PROCEDURE */
            IF V_EXISTE = 0 THEN
              CUSTOM_ERROR_CODE := -20001;
              /* CUSTOM_ERROR_MESSAGE := 'CERTIFICADO NO ENCONTRADO CON EL VALOR "' || P_CODIGO || '" EN EL PARAMETRO "P_CODIGO"'; */
              CUSTOM_ERROR_MESSAGE := 'Sinónimo: ' || V_NOMBRE_PACKAGE ||
                                      ' no existe';
              RAISE CUSTOM_ERROR;
            END IF;
            NOMBRE_PROCEDURE := V_NOMBRE_PROCEDURE_PARAMETRO;
          END IF;
        END IF;
      END IF;
    END IF;
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SGENERAR_CERTIFICADO:' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
    
  END;

  PROCEDURE SRECUPERAR_CUENTAS(P_IDENTIFICADOR IN VARCHAR2,
                               P_ERROR         OUT VARCHAR2) AS
  
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
  
  BEGIN
    IF VAR$OID_LOG IS NULL THEN
      VAR$OID_LOG := SYS_GUID();
    END IF;
  
    INSERT INTO SAPR_GTT_TCUENTA_CERTIFICADO
      (FYH_CERTIFICADO_ANTERIOR,
       OID_CERTIFICADO_ANT,
       OID_CUENTA_SALDO,
       OID_DIVISA,
       FYH_CERTIFICADO,
       OID_EJECUCION)
      WITH CUENTA AS
       (SELECT CUEN.OID_CUENTA,
               CERT.FYH_CERTIFICADO,
               CONS.OID_CONFIG_NIVEL_SALDO
          FROM SAPR_TCERTIFICADO CERT
         INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
            ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
         INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
            ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
         INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
            ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
         INNER JOIN SAPR_TCUENTA CUEN
            ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
           AND CESE.OID_SECTOR = CUEN.OID_SECTOR
           AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
         WHERE CERT.OID_CERTIFICADO = P_IDENTIFICADOR
           AND CESE.OID_CERTIFICADO = P_IDENTIFICADOR
           AND CESC.OID_CERTIFICADO = P_IDENTIFICADOR),
      CERTIFICADO_ANTERIOR AS
       (SELECT MAX(CEAN.FYH_CERTIFICADO) FYH_CERTIFICADO_ANTERIOR,
               CSEF.OID_CUENTA_SALDO,
               CSEF.OID_DIVISA,
               CU.FYH_CERTIFICADO,
               VAR$OID_EJECUCION AS OID_EJECUCION
          FROM CUENTA CU
         INNER JOIN SAPR_TCERT_SALDO_EFECTIVO CSEF
            ON CU.OID_CUENTA = CSEF.OID_CUENTA_SALDO
         INNER JOIN SAPR_TCERTIFICADO CEAN
            ON CSEF.OID_CERTIFICADO = CEAN.OID_CERTIFICADO
           AND CEAN.COD_ESTADO = 'DE'
         GROUP BY CSEF.OID_CUENTA_SALDO, CSEF.OID_DIVISA, CU.FYH_CERTIFICADO
        UNION
        SELECT MAX(CEAN.FYH_CERTIFICADO) FYH_CERTIFICADO_ANTERIOR,
               CSMP.OID_CUENTA_SALDO,
               CSMP.OID_DIVISA,
               CU.FYH_CERTIFICADO,
               VAR$OID_EJECUCION AS OID_EJECUCION
          FROM CUENTA CU
         INNER JOIN SAPR_TCERT_SALDO_MEDIO_PAGO CSMP
            ON CU.OID_CUENTA = CSMP.OID_CUENTA_SALDO
         INNER JOIN SAPR_TCERTIFICADO CEAN
            ON CSMP.OID_CERTIFICADO = CEAN.OID_CERTIFICADO
           AND CEAN.COD_ESTADO = 'DE'
         GROUP BY CSMP.OID_CUENTA_SALDO, CSMP.OID_DIVISA, CU.FYH_CERTIFICADO),
      CERTIFICADO_ANTERIOR_AUX AS
       (SELECT CERT.FYH_CERTIFICADO FYH_CERTIFICADO_ANTERIOR,
               CERT.OID_CERTIFICADO OID_CERTIFICADO_ANT,
               CA.OID_CUENTA_SALDO,
               CA.OID_DIVISA,
               CA.FYH_CERTIFICADO,
               VAR$OID_EJECUCION    AS OID_EJECUCION
          FROM SAPR_TCERTIFICADO CERT
         INNER JOIN CUENTA CU
            ON CU.OID_CONFIG_NIVEL_SALDO = CERT.OID_CONFIG_NIVEL_SALDO
         INNER JOIN CERTIFICADO_ANTERIOR CA
            ON CA.FYH_CERTIFICADO_ANTERIOR = CERT.FYH_CERTIFICADO
           AND CU.OID_CUENTA = CA.OID_CUENTA_SALDO)
      SELECT (SELECT MIN(CA.FYH_CERTIFICADO_ANTERIOR)
                FROM CERTIFICADO_ANTERIOR CA) FYH_CERTIFICADO_ANTERIOR,
             OID_CERTIFICADO_ANT,
             OID_CUENTA_SALDO,
             OID_DIVISA,
             FYH_CERTIFICADO,
             VAR$OID_EJECUCION AS OID_EJECUCION
        FROM CERTIFICADO_ANTERIOR_AUX
      UNION
      SELECT (SYSDATE - 3650) FYH_CERTIFICADO_ANTERIOR,
             NULL OID_CERTIFICADO_ANT,
             C.OID_CUENTA OID_CUENTA_SALDO,
             NULL OID_DIVISA,
             C.FYH_CERTIFICADO,
             VAR$OID_EJECUCION AS OID_EJECUCION
        FROM CUENTA C
       WHERE NOT EXISTS (SELECT OID_CUENTA_SALDO
                FROM CERTIFICADO_ANTERIOR CA
               WHERE CA.OID_CUENTA_SALDO = C.OID_CUENTA);
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SGENERAR_CERTIFICADO:' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
    
  END;

  /* Validar fecha plancertificado */
  PROCEDURE SVALIDAR_FECHA_CERT(par$fyh_plncertif      IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$oid_cuenta_origen  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_cuenta_destino IN gepr_pcomon_###VERSION###.tipo$oid_) IS
    var$fyh_ultimo_cert gepr_pcomon_###VERSION###.tipo$fyh_;
  BEGIN
  
    IF par$fyh_plncertif IS NOT NULL THEN
      BEGIN
        SELECT MAX(CERT.FYH_CERTIFICADO)
          INTO var$fyh_ultimo_cert
          FROM SAPR_TCERTIFICADO CERT
         INNER JOIN SAPR_TCERTIFICADOXSECTOR CESE
            ON CERT.OID_CERTIFICADO = CESE.OID_CERTIFICADO
         INNER JOIN SAPR_TCERTIFICADOXSUBCANAL CESC
            ON CERT.OID_CERTIFICADO = CESC.OID_CERTIFICADO
         INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS
            ON CERT.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO
         INNER JOIN SAPR_TCUENTA CUEN
            ON CONS.OID_CLIENTE = CUEN.OID_CLIENTE
           AND CESE.OID_SECTOR = CUEN.OID_SECTOR
           AND CESC.OID_SUBCANAL = CUEN.OID_SUBCANAL
         WHERE CERT.COD_ESTADO IN ('PC', 'DE')
           AND (CUEN.OID_CUENTA = par$oid_cuenta_origen OR
               CUEN.OID_CUENTA = par$oid_cuenta_destino);
      EXCEPTION
        WHEN no_data_found THEN
          var$fyh_ultimo_cert := NULL;
      END;
    
      IF var$fyh_ultimo_cert IS NOT NULL AND
         var$fyh_ultimo_cert >= par$fyh_plncertif THEN
        /* Si a fecha informada es inferior a fecha del banco, anadir un segundo */
        par$fyh_plncertif := var$fyh_ultimo_cert + 1 / 86400;
      END IF;
    END IF;
  
  END svalidar_fecha_cert;

  PROCEDURE SANULAR_CERTIFICADO(P_IDENTIFICADOR IN VARCHAR2,
                                P_ERROR         OUT VARCHAR2) AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    CUSTOM_ERROR EXCEPTION;
    CUSTOM_ERROR_CODE    NUMERIC := -20000;
    CUSTOM_ERROR_MESSAGE VARCHAR2(32767) := '';
  
  BEGIN
  
    UPDATE SAPR_TCERTIFICADO C
       SET C.COD_ESTADO = 'AN'
     WHERE C.OID_CERTIFICADO = P_IDENTIFICADOR;
  
    COMMIT;
  
  EXCEPTION
    WHEN CUSTOM_ERROR THEN
      P_ERROR := 'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
                 CUSTOM_ERROR_MESSAGE;
    
      SDEBUG(SYS_GUID,
             'ERROR: ' || CUSTOM_ERROR_CODE || ' - ' ||
             CUSTOM_ERROR_MESSAGE);
      RAISE_APPLICATION_ERROR(CUSTOM_ERROR_CODE, CUSTOM_ERROR_MESSAGE);
    WHEN OTHERS THEN
      P_ERROR := 'ERROR: SGENERAR_CERTIFICADO:' || SQLCODE || ' - ' ||
                 SQLERRM;
    
      SDEBUG(SYS_GUID, P_ERROR);
      RAISE_APPLICATION_ERROR(-20001, P_ERROR);
  END SANULAR_CERTIFICADO;

END SAPR_PCERTIFICACION_###VERSION###;
/