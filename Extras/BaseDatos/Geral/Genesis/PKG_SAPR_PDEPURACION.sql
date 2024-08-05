CREATE OR REPLACE package SAPR_PDEPURACION_###VERSION### IS

  /*Version: ###VERSION_COMP###*/
  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PDEPURACION_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
  
    PROCEDURE sdepurar_log (
    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_
  );

  PROCEDURE sdepurar
  (
    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
    par$rc_validaciones         OUT sys_refcursor
  );
end SAPR_PDEPURACION_###VERSION###;
/
create or replace package body SAPR_PDEPURACION_###VERSION### IS

  PROCEDURE sdepurar_log (
      par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
      par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_
  ) IS 
    const$nombre_func   CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sdepurar_log';
    const$cod_parametro CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := 'DepuracionLogsNumeroDias';
    var$cantidad_de_dias_texto    gepr_pcomon_###VERSION###.tipo$desc_;
    var$cantidad_de_dias          gepr_pcomon_###VERSION###.tipo$nel_;
    var$fecha_referencia          gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fecha_particion           gepr_pcomon_###VERSION###.tipo$fyh_;

  BEGIN

    /* #### Grabar llamadas #### */
    IF par$oid_llamada IS NOT NULL THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => q'[Parametros iniciales: 
              par$oid_llamada: ]'   || par$oid_llamada  || q'[
              par$cod_pais: ]'      || par$cod_pais,
            par$cod_identificador   => '');
    END IF;  

    SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Se busca parámetro = ' || const$cod_parametro,
            par$cod_identificador   => '');
    /*Obtengo el parametro de la cantidad de días que se deben dejar los logs */
    var$cantidad_de_dias_texto := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                                                                    par$cod_pais => par$cod_pais,
                                                                                    par$cod_parametro => const$cod_parametro,
                                                                                    par$cod_aplicacion => gepr_pcomon_###VERSION###.const$codAplicacionGenesis);
    SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'var$cantidad_de_dias_texto = ' || var$cantidad_de_dias_texto,
        par$cod_identificador   => '');

    /* Convierto el parametro obtenido en formato de texto al formato numerico */
    var$cantidad_de_dias := gepr_putilidades_###VERSION###.ftexto_a_numero(var$cantidad_de_dias_texto);

    SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'var$cantidad_de_dias = ' || var$cantidad_de_dias,
        par$cod_identificador   => '');

    IF var$cantidad_de_dias > 0 THEN
      var$fecha_referencia := TRUNC(SYS_EXTRACT_UTC(CURRENT_TIMESTAMP)) - var$cantidad_de_dias;

      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Fecha desde que se mantienen los logs - var$fecha_referencia: ' || var$fecha_referencia,
        par$cod_identificador   => '');


      /* ELIMINAMOS LOS REGISTROS DE LAS TABLAS DE LOG */
      --* SAPR_TLOG_API_DETALLE *--
      -- BUSCAMOS LAS PARTICIONES DE LA TABLA
      FOR V IN (SELECT PART.PARTITION_NAME, PART.HIGH_VALUE FROM USER_TAB_PARTITIONS PART WHERE PART.TABLE_NAME = 'SAPR_TLOG_API_DETALLE' AND PART.INTERVAL = 'YES')
      LOOP
        -- CONVERTIMOS EL CAMPO HIGH_VALUE (EN EL CUAL SE ALMACENA EL LIMITE SUPERIOR DE FECHAS ALMACENADAS EN LA PARTICION) DEL TIPO LONG A DATE 
        EXECUTE IMMEDIATE 'SELECT '|| SUBSTR(V.HIGH_VALUE, 1, 4000) || ' FROM DUAL' INTO var$fecha_particion ;

        SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'PARTICIÓN de la tabla SAPR_TLOG_API_DETALLE con PARTITION_NAME: ' || V.PARTITION_NAME || ' y HIGH_VALUE: ' || var$fecha_particion, 
                  par$cod_identificador   => '');

        IF var$fecha_particion <= var$fecha_referencia THEN

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Comienza eliminación de partición de la tabla SAPR_TLOG_API_DETALLE con PARTITION_NAME: ' || V.PARTITION_NAME || ' y HIGH_VALUE: ' || var$fecha_particion, 
            par$cod_identificador   => '');

          -- DROP PARTITION
          EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TLOG_API_DETALLE DROP PARTITION '|| V.PARTITION_NAME || ' UPDATE INDEXES';

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Finaliza eliminación de partición de la tabla SAPR_TLOG_API_DETALLE con PARTITION_NAME: ' || V.PARTITION_NAME || ' y HIGH_VALUE: ' || var$fecha_particion, 
            par$cod_identificador   => '');

        ELSE
          SAPR_PLOG_API.SAGREGA_DETALLE(
                      par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     => 'No se elimina partición. Fecha de la partición '|| var$fecha_particion || ' es mayor a la fecha de referencia ' || var$fecha_referencia, 
                      par$cod_identificador   => '');
        END IF;
      END LOOP;

      --* SAPR_TLOG_API_LLAMADA *--
      SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Comienza deshabilitación de constraints de la tabla SAPR_TLOG_API_DETALLE', 
          par$cod_identificador   => '');

      -- DESHABILITAMOS CONSTRAINT DEL TIPO R - Referential integrity DE LA TABLA HIJA SAPR_TLOG_API_DETALLE
      FOR FILA IN (SELECT CONSTRAINT_NAME FROM USER_CONSTRAINTS WHERE TABLE_NAME = 'SAPR_TLOG_API_DETALLE' AND CONSTRAINT_TYPE = 'R')
      LOOP
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'CONSTRAINT ' || FILA.CONSTRAINT_NAME, 
            par$cod_identificador   => '');

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Comienza deshabilitación constraint ' || FILA.CONSTRAINT_NAME, 
            par$cod_identificador   => '');

        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TLOG_API_DETALLE DISABLE CONSTRAINT '|| FILA.CONSTRAINT_NAME ;

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Finaliza deshabilitación constraint ' || FILA.CONSTRAINT_NAME, 
            par$cod_identificador   => '');
      END LOOP;
      
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Finaliza deshabilitación de constraints de la tabla SAPR_TLOG_API_DETALLE', 
        par$cod_identificador   => '');

      -- BUSCAMOS LAS PARTICIONES DE LA TABLA
      FOR V IN (SELECT PART.PARTITION_NAME, PART.HIGH_VALUE FROM USER_TAB_PARTITIONS PART WHERE PART.TABLE_NAME = 'SAPR_TLOG_API_LLAMADA' AND PART.INTERVAL = 'YES')
      LOOP
        -- CONVERTIMOS EL CAMPO HIGH_VALUE (EN EL CUAL SE ALMACENA EL LIMITE SUPERIOR DE FECHAS ALMACENADAS EN LA PARTICION) DEL TIPO LONG A DATE 
        EXECUTE IMMEDIATE 'SELECT '|| SUBSTR(V.HIGH_VALUE, 1, 4000) || ' FROM DUAL' INTO var$fecha_particion;
        
        SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'PARTICIÓN de la tabla SAPR_TLOG_API_LLAMADA con PARTITION_NAME: ' || V.PARTITION_NAME || ' y HIGH_VALUE: ' || var$fecha_particion, 
                  par$cod_identificador   => '');

        IF var$fecha_particion <= var$fecha_referencia THEN
          
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Comienza eliminación de partición de la tabla SAPR_TLOG_API_LLAMADA con PARTITION_NAME: ' || V.PARTITION_NAME || ' y HIGH_VALUE: ' || var$fecha_particion, 
            par$cod_identificador   => '');

          -- DROP PARTITION
          EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TLOG_API_LLAMADA DROP PARTITION '|| V.PARTITION_NAME || ' UPDATE INDEXES';

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Finaliza eliminación de partición de la tabla SAPR_TLOG_API_LLAMADA con PARTITION_NAME: ' || V.PARTITION_NAME || ' y HIGH_VALUE: ' || var$fecha_particion, 
            par$cod_identificador   => '');
        ELSE
          SAPR_PLOG_API.SAGREGA_DETALLE(
                      par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     => 'No se elimina partición. Fecha de la partición '|| var$fecha_particion || ' es mayor a la fecha de referencia ' || var$fecha_referencia, 
                      par$cod_identificador   => '');
        END IF;
      END LOOP;
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Comienza habilitación de constraints de la tabla SAPR_TLOG_API_DETALLE', 
        par$cod_identificador   => '');

      -- HABILITAMOS CONSTRAINT DEL TIPO R - Referential integrity DE LA TABLA HIJA
      FOR FILA IN (SELECT CONSTRAINT_NAME FROM USER_CONSTRAINTS WHERE TABLE_NAME = 'SAPR_TLOG_API_DETALLE' AND CONSTRAINT_TYPE = 'R')
      LOOP
        SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'CONSTRAINT ' || FILA.CONSTRAINT_NAME, 
              par$cod_identificador   => '');

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Comienza habilitación constraint ' || FILA.CONSTRAINT_NAME, 
            par$cod_identificador   => '');

        EXECUTE IMMEDIATE 'ALTER TABLE SAPR_TLOG_API_DETALLE ENABLE CONSTRAINT '|| FILA.CONSTRAINT_NAME ;

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Finaliza habilitación constraint ' || FILA.CONSTRAINT_NAME, 
            par$cod_identificador   => '');

      END LOOP;

      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Finaliza habilitación de constraints de la tabla SAPR_TLOG_API_DETALLE', 
        par$cod_identificador   => '');

    END IF;

    SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Fin OK',
        par$cod_identificador   => '');
  EXCEPTION WHEN OTHERS THEN
    -- ROLLBACK;
    
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,                            
                                          par$des_detalle     => 'Se produce EXCEPTION: ' || SQLCODE || ' - ' || SQLERRM,
                                          par$cod_identificador   => '');

    raise;

  END sdepurar_log;

  PROCEDURE sdepurar
  (
    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
    par$rc_validaciones         OUT sys_refcursor
  ) IS
    const$nombre_func CONSTANT    gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sdepurar';
    var$mensaje                   gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_pais                  gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_pais                  gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
      /* #### Limpiamos la tabla auxiliar #### */
    DELETE FROM SAPR_GTT_TAUXILIAR;

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
              par$cod_identificador_ajeno: ]' || par$cod_identificador_ajeno  || q'[
              par$cod_pais: ]'                || par$cod_pais                 || q'[
              par$cod_usuario: ]'             || par$cod_usuario              || q'[
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

      SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Valido código de pais - var$oid_pais = ' || var$oid_pais || ', var$cod_pais = ' || var$cod_pais,
            par$cod_identificador   => '');

      SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Comienza llamada al procedure sdepurar_log',
            par$cod_identificador   => '');

      sdepurar_log(   
                    par$oid_llamada             => par$oid_llamada,
                    par$cod_pais                => var$cod_pais);

      SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Finaliza llamada al procedure sdepurar_log',
            par$cod_identificador   => '');
    ELSE
      -- Buscamos las validaciones del metodo srecuperar_pais y le asignamos el código correspondiente a JOBS
      UPDATE SAPR_GTT_TAUXILIAR
         SET OID_CAMPO1 = '2060000001'
      WHERE OID_CAMPO1 = '2040010026' AND COD_CALIFICADOR = 'VALIDACIONES';

      UPDATE SAPR_GTT_TAUXILIAR
         SET OID_CAMPO1 = '2060000002'
      WHERE OID_CAMPO1 = '2040010027' AND COD_CALIFICADOR = 'VALIDACIONES';
      
    END IF;

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
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,                            
                                          par$des_detalle     => 'Se produce EXCEPTION: ' || SQLCODE || ' - ' || SQLERRM,
                                          par$cod_identificador   => '');

    raise;   
  END sdepurar;



end SAPR_PDEPURACION_###VERSION###;
/