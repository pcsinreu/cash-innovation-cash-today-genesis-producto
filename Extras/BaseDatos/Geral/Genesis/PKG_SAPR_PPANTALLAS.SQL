CREATE OR REPLACE PACKAGE SAPR_PPANTALLAS_###VERSION### AS

  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PPANTALLAS_###VERSION###';

/*IAC*/

  /* Recuperar informaciones de la pantalla ABM DELEGACIONES */
  PROCEDURE srecuperar_info_delegaciones(par$oid_delegacion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$rc_resultado           OUT sys_refcursor,
                                          par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_, 
                                          par$info_ejecucion         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$cod_ejecucion          OUT gepr_pcomon_###VERSION###.tipo$nel_);
     
  
  /*NUEVO SALDOS*/                                     
  /* Recuperar informaciones de la pantalla transacciones */
  PROCEDURE srecuperar_info_transacciones(par$info_ejecucion         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$rc_delegaciones       OUT sys_refcursor,
                                          par$rc_canales            OUT sys_refcursor,
                                          par$rc_tipo_planificacion OUT sys_refcursor,
                                          par$rc_tipo_transaccion   OUT sys_refcursor,
                                          par$cod_ejecucion         OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Recuperar informaciones dinamicas de la pantalla transacciones */
  PROCEDURE srecuperar_info_dinamico(par$oids_delegaciones      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_canales           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_clientes          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_bancos            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_maquinas          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_tipo_plan         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$info_ejecucion         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$cod_cultura            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_maquinas           OUT sys_refcursor,
                                     par$rc_ptoservicios       OUT sys_refcursor,
                                     par$rc_planificacion      OUT sys_refcursor,
                                     par$cod_ejecucion         OUT gepr_pcomon_###VERSION###.tipo$nel_);

END SAPR_PPANTALLAS_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PPANTALLAS_###VERSION### AS


  /* Recuperar informaciones de la pantalla ABM DELEGACIONES */
  PROCEDURE srecuperar_info_delegaciones(par$oid_delegacion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$rc_resultado           OUT sys_refcursor,
                                          par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_, 
                                          par$info_ejecucion         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$cod_ejecucion          OUT gepr_pcomon_###VERSION###.tipo$nel_)IS
                                          
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_info_transacciones';

    /* LOG */
    var$inicio    TIMESTAMP := SYSTIMESTAMP;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err       varchar2(4000);
  BEGIN                                     
                    
   OPEN par$rc_resultado FOR
        SELECT 
          D.OID_DELEGACIONXCONFIG_FACTUR,
          C.OID_CLIENTE,
          C.COD_CLIENTE,
          C.DES_CLIENTE,
          S.OID_SUBCLIENTE,
          S.COD_SUBCLIENTE,
          S.DES_SUBCLIENTE,
          P.OID_PTO_SERVICIO,
          P.COD_PTO_SERVICIO,
          P.DES_PTO_SERVICIO
          FROM GEPR_TDELEGACIONXCONFIG_FACTUR D
          INNER JOIN GEPR_TCLIENTE C ON C.OID_CLIENTE = D.OID_CLIENTE_CAPITAL
          INNER JOIN GEPR_TSUBCLIENTE S ON S.OID_SUBCLIENTE = D.OID_SUBCLIENTE_TESORERIA
          INNER JOIN GEPR_TPUNTO_SERVICIO P ON P.OID_PTO_SERVICIO = D.OID_PTO_SERVICIO_TESORERIA
          WHERE  D.OID_DELEGACION = par$oid_delegacion;

       /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
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
  END srecuperar_info_delegaciones;                                        
                                          
  /* Recuperar informaciones de la pantalla transacciones */
  PROCEDURE srecuperar_info_transacciones(par$info_ejecucion         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$rc_delegaciones       OUT sys_refcursor,
                                          par$rc_canales            OUT sys_refcursor,
                                          par$rc_tipo_planificacion OUT sys_refcursor,
                                          par$rc_tipo_transaccion   OUT sys_refcursor,
                                          par$cod_ejecucion         OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_info_transacciones';

    /* LOG */
    var$inicio    TIMESTAMP := SYSTIMESTAMP;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err       varchar2(4000);
    var$dic_todos gepr_pcomon_###VERSION###.tipo$desc_;

  BEGIN

    /* Buscar diccionarios */
    var$dic_todos := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     'lbTodos',
                                                     'srecuperar_info_transacciones',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                     NULL,
                                                     0);
                                                     
                 
         DBMS_OUTPUT.PUT_LINE(par$cod_cultura);   
         DBMS_OUTPUT.PUT_LINE(gepr_pcomon_###VERSION###.const$codAplicacionGenesis);                                    
    /* Delegaciones */
    OPEN par$rc_delegaciones FOR
        WITH DELEGACIONES AS (
            SELECT D.OID_DELEGACION
                 , D.COD_DELEGACION || ' - ' || D.DES_DELEGACION AS DES_DELEGACION
              FROM GEPR_TDELEGACION D
             WHERE D.BOL_VIGENTE = 1
          ORDER BY D.COD_DELEGACION, D.DES_DELEGACION)
      SELECT 'todos' AS OID_DELEGACION, var$dic_todos AS DES_DELEGACION FROM DUAL UNION ALL
      SELECT * FROM DELEGACIONES;

    /* Canales */
    OPEN par$rc_canales FOR
        WITH CANALES AS (
              SELECT C.OID_CANAL
                   , C.COD_CANAL || ' - ' || C.DES_CANAL AS DES_CANAL
                FROM GEPR_TCANAL C
               WHERE C.BOL_VIGENTE = 1
            ORDER BY C.COD_CANAL, C.DES_CANAL)
      SELECT 'todos' AS OID_CANAL, var$dic_todos AS DES_CANAL FROM DUAL UNION ALL
      SELECT * FROM CANALES;

    /* Tipos de planificacion */
    OPEN par$rc_tipo_planificacion FOR
        WITH TIPOS AS (
              SELECT T.OID_TIPO_PLANIFICACION
                   , T.DES_TIPO_PLANIFICACION
                FROM SAPR_TTIPO_PLANIFICACION T
               WHERE T.BOL_ACTIVO = 1
             ORDER BY T.DES_TIPO_PLANIFICACION)
      SELECT 'todos' AS OID_TIPO_PLANIFICACION, var$dic_todos AS DES_TIPO_PLANIFICACION FROM DUAL UNION ALL
      SELECT * FROM TIPOS;
      
    /* Tipos de transaccion */
    OPEN par$rc_tipo_transaccion FOR
        WITH TIPOS AS (
              SELECT FORM.DES_COD_EXTERNO AS OID_TIPO_TRANSACCION
                   , FORM.DES_COD_EXTERNO AS DES_TIPO_TRANSACCION
                FROM SAPR_TFORMULARIO FORM
               WHERE FORM.BOL_ACTIVO = 1 AND FORM.DES_COD_EXTERNO IS NOT NULL
            GROUP BY FORM.DES_COD_EXTERNO
            ORDER BY FORM.DES_COD_EXTERNO)
      SELECT 'todos' AS OID_TIPO_TRANSACCION, var$dic_todos AS DES_TIPO_TRANSACCION FROM DUAL UNION ALL
      SELECT * FROM TIPOS;

    var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
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
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$cod_usuario,
                                                         par$cod_ejecucion);

  /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
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

  END srecuperar_info_transacciones;

  /* Recuperar informaciones dinamicas de la pantalla transacciones */
  PROCEDURE srecuperar_info_dinamico(par$oids_delegaciones      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_canales           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_clientes          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_bancos            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_maquinas          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$oids_tipo_plan         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$info_ejecucion         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$cod_cultura            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_maquinas           OUT sys_refcursor,
                                     par$rc_ptoservicios       OUT sys_refcursor,
                                     par$rc_planificacion      OUT sys_refcursor,
                                     par$cod_ejecucion         OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_info_dinamico';

    /* LOG */
    var$inicio    TIMESTAMP := SYSTIMESTAMP;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err       varchar2(4000);
    var$dic_todos gepr_pcomon_###VERSION###.tipo$desc_;

  BEGIN

      /* ##### LIMPIAR TABLAS TEMPORARIAS ##### */
      DELETE SAPR_GTT_TAUXILIAR;

      /* #### Inicializar los cursores #### */
      OPEN par$rc_maquinas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_ptoservicios FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_planificacion FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

      /* Buscar diccionarios */
      var$dic_todos := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                 'lbTodos',
                                                 'srecuperar_info_transacciones',
                                                 gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                 NULL,
                                                 0);

      /* par$oids_delegaciones*/
      IF par$oids_delegaciones IS NOT NULL AND par$oids_delegaciones.COUNT > 0  THEN
         FOR idx IN par$oids_delegaciones.first .. par$oids_delegaciones.last LOOP
           IF par$oids_delegaciones(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (par$oids_delegaciones(idx), 'OID_DELEGACIONES');
           END IF;
         END LOOP;
      END IF;

      /* par$oids_canales     */
      IF par$oids_canales IS NOT NULL AND par$oids_canales.COUNT > 0  THEN
         FOR idx IN par$oids_canales.first .. par$oids_canales.last LOOP
           IF par$oids_canales(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (par$oids_canales(idx), 'OID_CANALES');
           END IF;
         END LOOP;
      END IF;

      /* par$oids_clientes    */
      IF par$oids_clientes IS NOT NULL AND par$oids_clientes.COUNT > 0  THEN
         FOR idx IN par$oids_clientes.first .. par$oids_clientes.last LOOP
           IF par$oids_clientes(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (par$oids_clientes(idx), 'OID_CLIENTES');
           END IF;
         END LOOP;
      END IF;

      /* par$oids_bancos      */
      IF par$oids_bancos IS NOT NULL AND par$oids_bancos.COUNT > 0  THEN
         FOR idx IN par$oids_bancos.first .. par$oids_bancos.last LOOP
           IF par$oids_bancos(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (par$oids_bancos(idx), 'OID_BANCOS');
           END IF;
         END LOOP;
      END IF;

      /* par$oids_maquinas    */
      IF par$oids_maquinas IS NOT NULL AND par$oids_maquinas.COUNT > 0  THEN
         FOR idx IN par$oids_maquinas.first .. par$oids_maquinas.last LOOP
           IF par$oids_maquinas(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (par$oids_maquinas(idx), 'OID_MAQUINA');
           END IF;
         END LOOP;
      END IF;

      /* par$oids_tipo_plan   */
      IF par$oids_tipo_plan IS NOT NULL AND par$oids_tipo_plan.COUNT > 0  THEN
         FOR idx IN par$oids_tipo_plan.first .. par$oids_tipo_plan.last LOOP
           IF par$oids_tipo_plan(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
              VALUES (par$oids_tipo_plan(idx), 'OID_TIPO_PLAN');
           END IF;
         END LOOP;
      END IF;

      IF par$oids_clientes IS NOT NULL AND par$oids_clientes.COUNT > 0  THEN

          OPEN par$rc_maquinas FOR
            WITH MAQUINAS AS (
                  SELECT MAQU.OID_MAQUINA
                       , MAQU.COD_IDENTIFICACION AS COD_MAQUINA
                    FROM SAPR_TMAQUINA MAQU
              INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = MAQU.OID_SECTOR
              INNER JOIN GEPR_TPLANTA PLAN ON PLAN.OID_PLANTA = SECT.OID_PLANTA
              INNER JOIN GEPR_TPUNTO_SERVICIO PUSE ON PUSE.OID_MAQUINA = MAQU.OID_MAQUINA
              INNER JOIN GEPR_TSUBCLIENTE SUCL ON SUCL.OID_SUBCLIENTE = PUSE.OID_SUBCLIENTE
              INNER JOIN SAPR_GTT_TAUXILIAR ACLI ON ACLI.COD_CALIFICADOR = 'OID_CLIENTES' AND ACLI.OID_CAMPO1 = SUCL.OID_CLIENTE
               LEFT JOIN SAPR_GTT_TAUXILIAR ADEL ON ADEL.COD_CALIFICADOR = 'OID_DELEGACIONES' AND ADEL.OID_CAMPO1 = PLAN.OID_DELEGACION
                   WHERE MAQU.BOL_ACTIVO = 1
                ORDER BY MAQU.OID_MAQUINA, MAQU.COD_IDENTIFICACION)
          SELECT 'todos' AS OID_MAQUINA, var$dic_todos AS COD_MAQUINA FROM DUAL UNION ALL
          SELECT * FROM MAQUINAS;

          OPEN par$rc_ptoservicios FOR
            WITH PTO_SERVICIO AS (
                  SELECT PUSE.OID_PTO_SERVICIO
                       , PUSE.COD_PTO_SERVICIO || ' - ' || PUSE.DES_PTO_SERVICIO AS DES_PTO_SERVICIO, PUSE.OID_MAQUINA
                    FROM GEPR_TPUNTO_SERVICIO PUSE
              INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_MAQUINA = PUSE.OID_MAQUINA
              INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = MAQU.OID_SECTOR
              INNER JOIN GEPR_TPLANTA PLAN ON PLAN.OID_PLANTA = SECT.OID_PLANTA
              INNER JOIN GEPR_TSUBCLIENTE SUCL ON SUCL.OID_SUBCLIENTE = PUSE.OID_SUBCLIENTE
              INNER JOIN SAPR_GTT_TAUXILIAR ACLI ON ACLI.COD_CALIFICADOR = 'OID_CLIENTES' AND ACLI.OID_CAMPO1 = SUCL.OID_CLIENTE
               LEFT JOIN SAPR_GTT_TAUXILIAR ADEL ON ADEL.COD_CALIFICADOR = 'OID_DELEGACIONES' AND ADEL.OID_CAMPO1 = PLAN.OID_DELEGACION
                   WHERE PUSE.BOL_VIGENTE = 1
                ORDER BY PUSE.COD_PTO_SERVICIO, PUSE.DES_PTO_SERVICIO)
          SELECT 'todos' AS OID_PTO_SERVICIO, var$dic_todos AS DES_PTO_SERVICIO, '-' as OID_MAQUINA  FROM DUAL UNION ALL
          SELECT * FROM PTO_SERVICIO;

      END IF;

      IF par$oids_bancos IS NOT NULL AND par$oids_bancos.COUNT > 0  THEN

          OPEN par$rc_planificacion FOR
            WITH PLANIFICACIONES AS (
                SELECT PLAN.OID_PLANIFICACION
                     , PLAN.DES_PLANIFICACION
                  FROM SAPR_TPLANIFICACION PLAN
            INNER JOIN SAPR_TPLANXCANAL PLCA ON PLCA.OID_PLANIFICACION = PLAN.OID_PLANIFICACION
            INNER JOIN SAPR_GTT_TAUXILIAR ABAN ON ABAN.COD_CALIFICADOR = 'OID_BANCOS' AND ABAN.OID_CAMPO1 = PLAN.OID_CLIENTE
             LEFT JOIN SAPR_GTT_TAUXILIAR ADEL ON ADEL.COD_CALIFICADOR = 'OID_DELEGACIONES' AND ADEL.OID_CAMPO1 = PLAN.OID_DELEGACION
             LEFT JOIN SAPR_GTT_TAUXILIAR ACAN ON ACAN.COD_CALIFICADOR = 'OID_CANALES' AND ACAN.OID_CAMPO1 = PLCA.OID_CANAL
             INNER JOIN SAPR_GTT_TAUXILIAR ATIP ON ATIP.COD_CALIFICADOR = 'OID_TIPO_PLAN' AND ATIP.OID_CAMPO1 = PLAN.OID_TIPO_PLANIFICACION
                 WHERE PLAN.BOL_ACTIVO = 1
              ORDER BY PLAN.DES_PLANIFICACION)
          SELECT 'todos' AS OID_PLANIFICACION, var$dic_todos AS DES_PLANIFICACION FROM DUAL UNION ALL
          SELECT * FROM PLANIFICACIONES;

      END IF;

    var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
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
                                                     'C',
                                                     par$info_ejecucion,
                                                     var$err,
                                                     par$cod_usuario,
                                                     par$cod_ejecucion);

  /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, var$err, true);
      var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
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

  END srecuperar_info_dinamico;

END SAPR_PPANTALLAS_###VERSION###;
/
