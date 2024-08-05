CREATE OR REPLACE package SAPR_PNOTIFICACION_SERV_###VERSION### IS

  /*Version: ###VERSION_COMP###*/
  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PNOTIFICACION_SERV_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
  
  PROCEDURE srec_movimientos_noacreditados
  (
    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
    par$rc_validaciones         OUT sys_refcursor,
    par$rc_movimientos          OUT sys_refcursor
  );
end SAPR_PNOTIFICACION_SERV_###VERSION###;
/
CREATE OR REPLACE package body SAPR_PNOTIFICACION_SERV_###VERSION### IS

PROCEDURE srec_movimientos_noacreditados
  (
    par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_, 
    par$rc_validaciones         OUT sys_refcursor,
    par$rc_movimientos          OUT sys_refcursor
  )
  IS
  const$nombre_func CONSTANT    gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.srec_movimientos_noacreditados';
  const$cod_parametro CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := 'NotificacionListaCorreosMovimientosNoAcreditados';
  var$lista_correos             gepr_pcomon_###VERSION###.tipo$desc_;
  var$cod_pais                  gepr_pcomon_###VERSION###.tipo$cod_;
  var$oid_pais                  gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

  /* #### Base #### */

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


  /* #### Limpiamos la tabla auxiliar #### */
  EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TAUXILIAR';

  /* #### Obtener lista de correos a notificar #### */
  SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Se busca parámetro = ' || const$cod_parametro,
            par$cod_identificador   => '');

  var$lista_correos := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                                                            par$cod_pais => par$cod_pais,
                                                                            par$cod_parametro => const$cod_parametro,
                                                                            par$cod_aplicacion => gepr_pcomon_###VERSION###.const$codAplicacionGenesis);
  SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'var$lista_correos = ' || var$lista_correos,
        par$cod_identificador   => '');

  var$cod_pais := par$cod_pais;
    GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                            par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                            par$cod_pais => var$cod_pais,
                            par$oid_pais => var$oid_pais,
                            par$cod_cultura => par$cod_cultura);

  /* #### Inicializar los cursores #### */

  OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; 
  OPEN par$rc_movimientos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

  IF var$oid_pais IS NOT NULL THEN
  /* #### Insertar Movimientos #### */
   OPEN par$rc_movimientos FOR
    SELECT 
        MOV.COD_TIPO_PLANIFICACION,
        MOV.DES_PLANIFICACION,
        MOV.COD_PLANIFICACION_BANCO_OPE,
        MOV.DES_PLANIFICACION_BANCO,
        MOV.COD_DEVICEID,
        MOV.COD_CLIENTE_OPE,
        MOV.DES_CLIENTE,
        MOV.COD_PTO_SERVICIO_OPE,
        MOV.DES_PTO_SERVICIO,
        MOV.HOR_MOVIMIENTO,
        MOV.COD_GRUPO_MOVIMIENTO,
        MOV.COD_MOVIMIENTO,
        EXTRACT( DAY FROM (SYSTIMESTAMP - MOV.HOR_MOVIMIENTO)) * 24 + EXTRACT( HOUR FROM (SYSTIMESTAMP - MOV.HOR_MOVIMIENTO)) HORAS_TRANSCURRIDAS
    FROM SAPR_TMOVIMIENTO MOV
    WHERE 
        FYH_PLANIFICACION_PERIODO_INI IS NULL 
        AND FYH_PLANIFICACION_PERIODO_FIN IS NULL
        AND COD_PLANIFICACION IS NOT NULL;
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
    -- ROLLBACK;
    
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,                            
                                          par$des_detalle     => 'Se produce EXCEPTION: ' || SQLCODE || ' - ' || SQLERRM,
                                          par$cod_identificador   => '');

    RAISE;

  END srec_movimientos_noacreditados;

end SAPR_PNOTIFICACION_SERV_###VERSION###;
/