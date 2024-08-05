create or replace PACKAGE gepr_pcodigo_ajeno_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pcodigo_ajeno_###VERSION###';

  /* Recuperar codigo ajeno */
  PROCEDURE srecuperar_codigo_ajeno_mae(par$cod_delegacion    IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_planta        IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_sector        IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_identificador IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion    IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$validaciones     OUT sys_refcursor,
                                        par$cod_ejecucion    OUT gepr_pcomon_###VERSION###.tipo$nel_);

 PROCEDURE sgrabar_codigo_ajeno(par$oid_cod_ajeno         in out gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_tabla_genesis     in gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_tabla_genesis     in gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_identificador     in gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_ajeno             in gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$des_ajeno             in gepr_pcomon_###VERSION###.tipo$desc_,
                                 par$bol_defecto           in gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$oid_delegacion        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_usuario           in gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$inserts               OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates               OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Recuperar codigo ajeno */
  PROCEDURE srecuperar_codigo_ajeno(par$cod_tipo_tabla_genesis IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_identificador      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$bol_activo             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$info_ejecucion         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$aje_rc_codigo_ajeno    OUT sys_refcursor,
                                    par$cod_ejecucion          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$selects                OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Configurar codigo ajeno */
  PROCEDURE sconfigurar_ajeno(par$oid_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_identificador      IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_ajeno              IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$des_ajeno              IN gepr_pcomon_###VERSION###.tipo$desc_,
                              par$bol_defecto            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_);

  /* Elimina fisicamente un codigo ajeno */
  PROCEDURE seliminar_ajeno(par$oid_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_identificador      IN gepr_pcomon_###VERSION###.tipo$cod_);

END gepr_pcodigo_ajeno_###VERSION###;
/
create or replace PACKAGE BODY gepr_pcodigo_ajeno_###VERSION### AS

  /* Recuperar codigo ajeno */
  PROCEDURE srecuperar_codigo_ajeno_mae(par$cod_delegacion    IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_planta        IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_sector        IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_identificador IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion    IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$validaciones     OUT sys_refcursor,
                                        par$cod_ejecucion    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_codigo_ajeno';

    /* variables locales */
    var$inicio    gepr_pcomon_###VERSION###.tipo$gmt_ := SYSTIMESTAMP;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err       VARCHAR2(4000);

    var$cod_delegacion gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planta     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sector     gepr_pcomon_###VERSION###.tipo$cod_;
                                        
    /* Validaciones */
    var$validaciones  gepr_pcomon_###VERSION###.tipo$descs_;
    var$tvalidaciones varchar2_array;

  BEGIN

    BEGIN
          
          SELECT D.COD_DELEGACION
            INTO var$cod_delegacion
            FROM GEPR_TCODIGO_AJENO C
      INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = C.OID_TABLA_GENESIS
           WHERE C.BOL_ACTIVO = 1
             AND C.COD_IDENTIFICADOR = par$cod_identificador
             AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
             AND C.COD_AJENO = par$cod_delegacion;

       par$cod_delegacion:= var$cod_delegacion;

    EXCEPTION
      WHEN no_data_found THEN
        var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'GENESIS_PRODUCTO_VAL130',
                                                                                        'validaciones_genesis_producto',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                        par$cod_delegacion,
                                                                                        1);
    END;
      
    BEGIN
          
          SELECT P.COD_PLANTA
            INTO var$cod_planta
            FROM GEPR_TCODIGO_AJENO C
      INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = C.OID_TABLA_GENESIS
           WHERE C.BOL_ACTIVO = 1
             AND C.COD_IDENTIFICADOR = par$cod_identificador
             AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
             AND C.COD_AJENO = par$cod_planta;

       par$cod_planta:= var$cod_planta;

    EXCEPTION
      WHEN no_data_found THEN
        var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'GENESIS_PRODUCTO_VAL131',
                                                                                        'validaciones_genesis_producto',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                        par$cod_planta,
                                                                                        1);
    END;
        
    BEGIN
          
          SELECT S.COD_SECTOR
            INTO var$cod_sector
            FROM GEPR_TCODIGO_AJENO C
      INNER JOIN GEPR_TSECTOR S ON S.OID_SECTOR = C.OID_TABLA_GENESIS
           WHERE C.BOL_ACTIVO = 1
             AND C.COD_IDENTIFICADOR = par$cod_identificador
             AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
             AND C.COD_AJENO = par$cod_sector;

       par$cod_sector:= var$cod_sector;

    EXCEPTION
      WHEN no_data_found THEN
        var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'GENESIS_PRODUCTO_VAL132',
                                                                                        'validaciones_genesis_producto',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                        par$cod_sector,
                                                                                        1);
    END;

    IF var$validaciones.COUNT > 0 THEN
      
      var$tvalidaciones := gepr_putilidades_###VERSION###.fconverter_array_desc_table(par$table_varchar2 => var$validaciones);

      open par$validaciones for
        SELECT SUBSTR(C.COLUMN_VALUE, 0, 6) CODIGO,
               SUBSTR(C.COLUMN_VALUE, 7) DESCRICION
          FROM TABLE(var$tvalidaciones) C
         WHERE C.COLUMN_VALUE IS NOT NULL;

    END IF;
    
  /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
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

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true);
                              
  END srecuperar_codigo_ajeno_mae;

   PROCEDURE sgrabar_codigo_ajeno(par$oid_cod_ajeno         in out gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_tabla_genesis     in gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_tabla_genesis     in gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_identificador     in gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_ajeno             in gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$des_ajeno             in gepr_pcomon_###VERSION###.tipo$desc_,
                                 par$bol_defecto           in gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$oid_delegacion        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_usuario           in gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$inserts               OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

  var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    par$inserts := 0;
    par$updates := 0;
  
    if par$oid_cod_ajeno is null then
    
      par$oid_cod_ajeno := sys_guid();
    
      INSERT INTO GEPR_TCODIGO_AJENO
        (OID_CODIGO_AJENO,
         OID_TABLA_GENESIS,
         COD_TIPO_TABLA_GENESIS,
         COD_IDENTIFICADOR,
         COD_AJENO,
         DES_AJENO,
         BOL_DEFECTO,
         BOL_ACTIVO,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION)
      VALUES
        (par$oid_cod_ajeno,
         par$oid_tabla_genesis,
         par$cod_tabla_genesis,
         par$cod_identificador,
         par$cod_ajeno,
         par$des_ajeno,
         par$bol_defecto,
         1,
         var$gmt_zero,
         par$cod_usuario,
         var$gmt_zero,
         par$cod_usuario);
   
   
      par$inserts := par$inserts + sql%rowcount;
    
    else
    
      UPDATE GEPR_TCODIGO_AJENO
         SET OID_TABLA_GENESIS        = par$oid_tabla_genesis,
             COD_TIPO_TABLA_GENESIS   = par$cod_tabla_genesis,
             COD_IDENTIFICADOR        = par$cod_identificador,
             COD_AJENO                = par$cod_ajeno,
             DES_AJENO                = par$des_ajeno,
             BOL_ACTIVO               = 1,
             BOL_DEFECTO              = par$bol_defecto,
             GMT_CREACION             = var$gmt_zero,
             DES_USUARIO_CREACION     = par$cod_usuario,
             GMT_MODIFICACION         = var$gmt_zero,
             DES_USUARIO_MODIFICACION = par$cod_usuario
       WHERE OID_CODIGO_AJENO = par$oid_cod_ajeno;
     
     
      par$updates := par$updates + sql%rowcount;
    
    end if;
  
  END sgrabar_codigo_ajeno;

  PROCEDURE srecuperar_codigo_ajeno(par$cod_tipo_tabla_genesis IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_identificador      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$bol_activo             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$info_ejecucion         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$aje_rc_codigo_ajeno    OUT sys_refcursor,
                                    par$cod_ejecucion          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$selects                OUT gepr_pcomon_###VERSION###.tipo$nel_) is
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_codigo_ajeno';
  
    /* variables locales */
    var$inicio gepr_pcomon_###VERSION###.tipo$gmt_ := SYSTIMESTAMP;
    var$err    VARCHAR2(4000);
  
  begin
  
    par$selects := 0;
  
    OPEN par$aje_rc_codigo_ajeno FOR
      SELECT CA.OID_TABLA_GENESIS,
             CA.COD_TIPO_TABLA_GENESIS,
             CA.OID_CODIGO_AJENO,
             CA.COD_IDENTIFICADOR,
             CA.COD_AJENO,
             CA.DES_AJENO,
             CA.BOL_DEFECTO,
             CA.BOL_MIGRADO,
             CA.BOL_ACTIVO,
             CA.GMT_CREACION,
             CA.DES_USUARIO_CREACION,
             CA.GMT_MODIFICACION,
             CA.DES_USUARIO_MODIFICACION
        FROM GEPR_TCODIGO_AJENO CA
       WHERE CA.COD_TIPO_TABLA_GENESIS = par$cod_tipo_tabla_genesis
         AND CA.OID_TABLA_GENESIS =
             NVL(par$oid_tabla_genesis, CA.OID_TABLA_GENESIS)
         AND CA.COD_IDENTIFICADOR =
             NVL(par$cod_identificador, CA.COD_IDENTIFICADOR)
         AND CA.BOL_ACTIVO = NVL(par$bol_activo, CA.BOL_ACTIVO)
       ORDER BY CA.COD_TIPO_TABLA_GENESIS,
                CA.OID_TABLA_GENESIS,
                CA.COD_IDENTIFICADOR;
  
    par$selects := sql%ROWCOUNT;
  
    /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           0,
                                                           0,
                                                           0,
                                                           0,
                                                           0,
                                                           0,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true);
  end srecuperar_codigo_ajeno;

  /* Configurar codigo ajeno */
  PROCEDURE sconfigurar_ajeno(par$oid_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_identificador      IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_ajeno              IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$des_ajeno              IN gepr_pcomon_###VERSION###.tipo$desc_,
                              par$bol_defecto            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_) IS

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;

  BEGIN

    MERGE INTO GEPR_TCODIGO_AJENO
      USING DUAL ON
      (
          OID_TABLA_GENESIS      = par$oid_tabla_genesis AND
          COD_TIPO_TABLA_GENESIS = par$cod_tabla_genesis AND
          COD_IDENTIFICADOR      = par$cod_identificador
      )
      WHEN MATCHED THEN
        UPDATE
           SET COD_AJENO                = par$cod_ajeno,
               DES_AJENO                = par$des_ajeno,
               BOL_ACTIVO               = 1,
               BOL_DEFECTO              = par$bol_defecto,
               GMT_MODIFICACION         = var$gmt_zero,
               DES_USUARIO_MODIFICACION = par$cod_usuario
      WHEN NOT MATCHED THEN
        INSERT 
              (OID_CODIGO_AJENO,
               OID_TABLA_GENESIS,
               COD_TIPO_TABLA_GENESIS,
               COD_IDENTIFICADOR,
               COD_AJENO,
               DES_AJENO,
               BOL_DEFECTO,
               BOL_ACTIVO,
               GMT_CREACION,
               DES_USUARIO_CREACION,
               GMT_MODIFICACION,
               DES_USUARIO_MODIFICACION)
            VALUES
              (SYS_GUID(),
               par$oid_tabla_genesis,
               par$cod_tabla_genesis,
               par$cod_identificador,
               par$cod_ajeno,
               par$des_ajeno,
               par$bol_defecto,
               1,
               var$gmt_zero,
               par$cod_usuario,
               var$gmt_zero,
               par$cod_usuario);
      
      /* Solo permite un �nico codigo ajeno con bol_defecto = 1*/
      IF par$bol_defecto = 1 THEN
        UPDATE GEPR_TCODIGO_AJENO
          SET BOL_DEFECTO = 0
        WHERE OID_TABLA_GENESIS = par$oid_tabla_genesis
        AND COD_TIPO_TABLA_GENESIS = par$cod_tabla_genesis
        AND COD_IDENTIFICADOR <> par$cod_identificador;
      END IF;

  END sconfigurar_ajeno;
  
  /* Elimina fisicamente un codigo ajeno */
  PROCEDURE seliminar_ajeno(par$oid_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_tabla_genesis      IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_identificador      IN gepr_pcomon_###VERSION###.tipo$cod_) IS
  BEGIN
    DELETE FROM GEPR_TCODIGO_AJENO
    WHERE OID_TABLA_GENESIS = par$oid_tabla_genesis
    AND COD_TIPO_TABLA_GENESIS = par$cod_tabla_genesis
    AND COD_IDENTIFICADOR = par$cod_identificador;    
  END;
END gepr_pcodigo_ajeno_###VERSION###;
/