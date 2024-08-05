create or replace PACKAGE GEPR_PINTEGRACION_###VERSION### IS

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'GEPR_PINTEGRACION_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
  const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
  const$comilla_simple  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);
  const$formato_gmt CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'DD/MM/YYYY HH24:MI:SS TZH:TZM';
  
  PROCEDURE sbusqueda_estado_integracion(par$oid_llamada   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$aactual_id    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$cod_proceso  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_origen  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_destino  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                         par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$rc_estados    OUT sys_refcursor);
                                         
  PROCEDURE sconfigurar_integracion( par$oid_llamada            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_tabla_integracion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_tabla_integracion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_estado              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_estado_detalle      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_modulo_origen       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_modulo_destino      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_proceso             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$nel_incremento_intento  IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$nel_reiniciar_intento   IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$des_detalle            IN gepr_pcomon_###VERSION###.tipo$desc_,
                                  par$cod_error              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$rc_validaciones         OUT sys_refcursor  ,
                                  par$info_ejecucion             IN VARCHAR2,
                                  par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE supd_integracion(par$oid_integracion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_tabla_integracion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_tabla_integracion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_estado              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_modulo_origen       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_modulo_destino      IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_proceso             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$nel_intento_envio       IN gepr_pcomon_###VERSION###.tipo$nel_,                            
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_);
                            
  PROCEDURE sins_tintegracion_detalle(par$oid_integracion_detalle    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_integracion            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_estado                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$nel_reintentos             IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$des_comentario             IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$bol_error                  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_error                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_usuario                IN gepr_pcomon_###VERSION###.tipo$cod_);
  /* Recuperar  documentos pendientes de integración */
  PROCEDURE srecuperar_movs_pendientes(
                                    par$codigo                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$tipo_codigo             IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_device_id           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_cliente             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_subcliente          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_pto_servicio        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$aestado                 IN gepr_pcomon_###VERSION###.tipo$nels_,
                                    par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_proceso             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$msg_error               IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$rc_actualid             OUT sys_refcursor,
                                    par$rc_actual_id_det        OUT sys_refcursor,
                                    par$rc_det_integracion      OUT sys_refcursor);

  PROCEDURE sbusqueda_pend_integracion( par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_,    
                                        par$desc_parametro_reintento    IN gepr_pcomon_###VERSION###.tipo$desc_, 
                                        par$acod_estado                 IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$cod_proceso                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_origen                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_destino                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rc_pendientes               OUT sys_refcursor);

  FUNCTION fhay_error RETURN BOOLEAN;   
  
  PROCEDURE svalida_integracion(par$acod_actual_id IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                              par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$cod_,
                              par$rc_resultado    OUT sys_refcursor);


END GEPR_PINTEGRACION_###VERSION###;
/
create or replace PACKAGE BODY GEPR_PINTEGRACION_###VERSION### IS
  

PROCEDURE sbusqueda_estado_integracion(
                                        par$oid_llamada   IN gepr_pcomon_###VERSION###.tipo$oid_,    
                                        par$aactual_id    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$cod_proceso  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_origen  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_destino  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                         par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$rc_estados    OUT sys_refcursor) IS
                                         
  const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sbusqueda_estado_integracion';
      var$err       varchar2(4000);
       var$obs_detalle             gepr_pcomon_###VERSION###.tipo$obs_;
  BEGIN
      /* #### Inicializar los cursores #### */
    OPEN par$rc_estados FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
 
    DELETE SAPR_GTT_TAUXILIAR
    WHERE COD_CALIFICADOR = 'OID_TABLA_INTEGRACION';
    COMMIT;

     IF par$oid_llamada IS NOT NULL THEN
        var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_proceso: ' || const$comilla_simple || par$cod_proceso || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_origen: ' || const$comilla_simple || par$cod_origen || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_destino: ' || const$comilla_simple || par$cod_destino || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$info_ejecucion: ' || const$comilla_simple || par$info_ejecucion || const$comilla_simple || const$new_line;

        FOR idx IN par$aactual_id.first .. par$aactual_id.last LOOP
            var$obs_detalle := var$obs_detalle || 'par$aactual_id('|| idx ||'): ' || par$aactual_id(idx) || const$new_line;
        END LOOP;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$obs_detalle,
                par$cod_identificador   => '');
    END IF;

 
    IF par$aactual_id IS NOT NULL AND par$aactual_id.COUNT > 0 THEN
      FOR idx IN par$aactual_id.first .. par$aactual_id.last LOOP

         INSERT INTO SAPR_GTT_TAUXILIAR(COD_CALIFICADOR,COD_CAMPO2, OID_CAMPO1) 
         VALUES ('OID_TABLA_INTEGRACION', par$aactual_id(idx), sys_guid());

           SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Insert in SAPR_GTT_TAUXILIAR OID_TABLA_INTEGRACION '|| par$aactual_id(idx),
                par$cod_identificador   => '');

      END LOOP;
    END IF;
     
     OPEN par$rc_estados FOR
     SELECT OID_TABLA_INTEGRACION, COD_ESTADO, OID_INTEGRACION, COD_TABLA_INTEGRACION
     FROM GEPR_TINTEGRACION
     INNER JOIN SAPR_GTT_TAUXILIAR ON SAPR_GTT_TAUXILIAR.COD_CAMPO2 = GEPR_TINTEGRACION.OID_TABLA_INTEGRACION
     WHERE
      cod_proceso = par$cod_proceso
     AND cod_modulo_origen = par$cod_origen
     AND cod_modulo_destino = par$cod_destino
     AND COD_CALIFICADOR = 'OID_TABLA_INTEGRACION';

  EXCEPTION
    WHEN OTHERS THEN

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                            null,
                                                           par$cod_ejecucion);
     SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Error: '|| var$err,
                par$cod_identificador   => '');
                                                           
  END sbusqueda_estado_integracion;




 PROCEDURE sconfigurar_integracion( par$oid_llamada            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_tabla_integracion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_tabla_integracion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_estado              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_estado_detalle      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_modulo_origen       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_modulo_destino      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_proceso             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$nel_incremento_intento  IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$nel_reiniciar_intento   IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$des_detalle            IN gepr_pcomon_###VERSION###.tipo$desc_,
                                  par$cod_error              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$rc_validaciones         OUT sys_refcursor,
                                  par$info_ejecucion             IN VARCHAR2,
                                  par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

            var$oid_integracion         gepr_pcomon_###VERSION###.tipo$oid_;
            var$cod_estado              gepr_pcomon_###VERSION###.tipo$cod_;
            var$nel_intento_envio       gepr_pcomon_###VERSION###.tipo$nel_;  
            var$obs_detalle             gepr_pcomon_###VERSION###.tipo$obs_;
  const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sconfigurar_integracion';
            var$err       varchar2(4000);
            
            var$cod_tipo_error       gepr_pcomon_###VERSION###.tipo$cod_;  
            var$nel_es_error         gepr_pcomon_###VERSION###.tipo$nbol_;  
           
  BEGIN
    
      IF par$oid_llamada IS NOT NULL THEN
        var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_tabla_integracion: ' || const$comilla_simple || par$cod_tabla_integracion || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$oid_tabla_integracion: ' || const$comilla_simple || par$oid_tabla_integracion || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_estado: ' || const$comilla_simple || par$cod_estado || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_estado_detalle: ' || const$comilla_simple || par$cod_estado_detalle || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_modulo_origen: ' || const$comilla_simple || par$cod_modulo_origen || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_modulo_destino: ' || const$comilla_simple || par$cod_modulo_destino || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_proceso: ' || const$comilla_simple || par$cod_proceso || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$nel_incremento_intento: ' || const$comilla_simple || par$nel_incremento_intento || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$nel_reiniciar_intento: ' || const$comilla_simple || par$nel_reiniciar_intento || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$des_detalle: ' || const$comilla_simple || par$des_detalle || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_error: ' || const$comilla_simple || par$cod_error || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$info_ejecucion: ' || const$comilla_simple || par$info_ejecucion || const$comilla_simple || const$new_line;
     
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$obs_detalle,
                par$cod_identificador   => '');
    END IF;
    
    OPEN par$rc_validaciones        FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK
    BEGIN
        select  
                oid_integracion,            
                cod_estado,
                nel_intento_envio
        INTO 
                var$oid_integracion,
                var$cod_estado,
                var$nel_intento_envio
        FROM GEPR_TINTEGRACION  
        WHERE   cod_tabla_integracion = par$cod_tabla_integracion AND
                oid_tabla_integracion = par$oid_tabla_integracion AND
                cod_modulo_origen     = par$cod_modulo_origen AND
                cod_modulo_destino    = par$cod_modulo_destino AND
                cod_proceso           = par$cod_proceso;
      
    EXCEPTION WHEN no_data_found THEN
    
        var$oid_integracion := SYS_GUID();  
        var$cod_estado := 'AB';
        var$nel_intento_envio := 0;
    END;

    IF par$nel_incremento_intento IS NOT NULL AND par$nel_incremento_intento > 0 THEN
        var$nel_intento_envio := var$nel_intento_envio + par$nel_incremento_intento;
    END IF;

    IF par$nel_reiniciar_intento IS NOT NULL AND par$nel_reiniciar_intento > 0 THEN
        var$nel_intento_envio := 0;
    END IF;

    IF par$cod_estado IS NOT NULL THEN
        var$cod_estado := par$cod_estado;
    END IF;

  


    supd_integracion(par$oid_integracion         => var$oid_integracion,
                     par$oid_tabla_integracion   => par$oid_tabla_integracion,
                     par$cod_tabla_integracion   => par$cod_tabla_integracion,
                     par$cod_estado              => var$cod_estado,
                     par$cod_modulo_origen       => par$cod_modulo_origen,
                     par$cod_modulo_destino      => par$cod_modulo_destino,
                     par$cod_proceso             => par$cod_proceso,
                     par$nel_intento_envio       => var$nel_intento_envio,
                     par$cod_usuario             => par$cod_usuario);
      var$obs_detalle := 'Parametros llamada supd_integracion: ';
      var$obs_detalle := var$obs_detalle || 'par$oid_integracion: ' || const$comilla_simple || var$oid_integracion || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$oid_tabla_integracion: ' || const$comilla_simple || par$oid_tabla_integracion || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_tabla_integracion: ' || const$comilla_simple || par$cod_tabla_integracion || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_estado: ' || const$comilla_simple || var$cod_estado || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_modulo_origen: ' || const$comilla_simple || par$cod_modulo_origen || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_modulo_destino: ' || const$comilla_simple || par$cod_modulo_destino || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_proceso: ' || const$comilla_simple || par$cod_proceso || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$nel_intento_envio: ' || const$comilla_simple || var$nel_intento_envio || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$obs_detalle,
                par$cod_identificador   => '');

 
      if  par$des_detalle IS NOT NULL THEN
            
           
           
            var$nel_es_error := 0;
           IF  par$cod_error IS NOT NULL THEN
             var$cod_tipo_error := par$cod_error;
             var$nel_es_error := 1;
           END IF;

           IF par$cod_estado_detalle IS NOT NULL THEN
             var$cod_estado := par$cod_estado_detalle;
           END IF;

       sins_tintegracion_detalle(par$oid_integracion_detalle    => sys_guid(),
                                    par$oid_integracion         => var$oid_integracion,
                                    par$cod_estado              => var$cod_estado,
                                    par$nel_reintentos          => var$nel_intento_envio,
                                    par$des_comentario          => par$des_detalle,
                                    par$bol_error               => var$nel_es_error,
                                    par$cod_error               => var$cod_tipo_error,
                                    par$cod_usuario             => par$cod_usuario);
        var$obs_detalle := 'Parametros llamada sins_tintegracion_detalle: ';
      var$obs_detalle := var$obs_detalle || 'par$oid_integracion: ' || const$comilla_simple || var$oid_integracion || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_estado: ' || const$comilla_simple || var$cod_estado || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$nel_reintentos: ' || const$comilla_simple || var$nel_intento_envio || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$des_comentario: ' || const$comilla_simple || par$des_detalle || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$bol_error: ' || const$comilla_simple || var$nel_es_error || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_error: ' || const$comilla_simple || var$cod_tipo_error || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$obs_detalle,
                par$cod_identificador   => '');
     END IF;


    COMMIT;
    OPEN par$rc_validaciones FOR
    SELECT  '1' as CODIGO, 
            'SUCESSO' as DESCRIPCION
    FROM DUAL;
    /*
 EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                            null,
                                                           par$cod_ejecucion);*/
  END sconfigurar_integracion;




  PROCEDURE supd_integracion(par$oid_integracion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_tabla_integracion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_tabla_integracion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_estado              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_modulo_origen       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_modulo_destino      IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_proceso             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$nel_intento_envio       IN gepr_pcomon_###VERSION###.tipo$nel_,                            
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_) IS
  
     /* variables locales */
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$existe   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
  BEGIN

    /* Verifica si es Update */
    SELECT COUNT(1)
      INTO var$existe
      FROM GEPR_TINTEGRACION P
     WHERE P.OID_INTEGRACION = par$oid_integracion
       AND ROWNUM < 2;

    IF var$existe > 0 THEN
      UPDATE GEPR_TINTEGRACION
         SET oid_tabla_integracion      = par$oid_tabla_integracion,
             cod_tabla_integracion      = par$cod_tabla_integracion,
             cod_estado                 = par$cod_estado,
             cod_modulo_origen          = par$cod_modulo_origen,
             cod_modulo_destino         = par$cod_modulo_destino,
             cod_proceso                = par$cod_proceso,
             nel_intento_envio          = par$nel_intento_envio,
             gmt_modificacion           = var$gmt_zero,
             des_usuario_modificacion   = par$cod_usuario          
       WHERE oid_integracion            = par$oid_integracion;

    ELSE

      INSERT INTO GEPR_TINTEGRACION
        (oid_integracion,
         oid_tabla_integracion,
         cod_tabla_integracion,
         cod_estado,
         cod_modulo_origen,
         cod_modulo_destino,
         cod_proceso,
         nel_intento_envio,
         gmt_creacion,
         des_usuario_creacion,
         gmt_modificacion,
         des_usuario_modificacion)
      VALUES
        (
             par$oid_integracion,
             par$oid_tabla_integracion,
             par$cod_tabla_integracion,
             par$cod_estado,
             par$cod_modulo_origen,
             par$cod_modulo_destino,
             par$cod_proceso,
             par$nel_intento_envio,
             var$gmt_zero,
             par$cod_usuario,
             var$gmt_zero,
             par$cod_usuario       
       );
      
    END IF;
  END supd_integracion;





            
  PROCEDURE sins_tintegracion_detalle(par$oid_integracion_detalle    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_integracion            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_estado                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$nel_reintentos             IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$des_comentario             IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$bol_error                  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_error                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_usuario                IN gepr_pcomon_###VERSION###.tipo$cod_) IS
  
     /* variables locales */
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN   

      INSERT INTO GEPR_TINTEGRACION_DETALLE
        ( oid_integracion_detalle,
          oid_integracion,
          cod_estado,
          nel_reintentos,
          des_comentario,
          bol_error,
          tipo_error,
          fyh_creacion,
          des_usuario_creacion,
          fyh_modificacion,
          des_usuario_modificacion)
      VALUES
        (
             par$oid_integracion_detalle,
             par$oid_integracion,
             par$cod_estado,
             par$nel_reintentos,
             par$des_comentario,
             par$bol_error,
             par$cod_error,
             var$gmt_zero,
             par$cod_usuario,
             var$gmt_zero,
             par$cod_usuario       
       );
      
  END sins_tintegracion_detalle;

  /* Recuperar  documentos pendientes de integración */
  PROCEDURE srecuperar_movs_pendientes(
                                par$codigo                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$tipo_codigo             IN gepr_pcomon_###VERSION###.tipo$nel_,
                                par$cod_device_id           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_cliente             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_subcliente          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_pto_servicio        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$aestado                 IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_proceso             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$msg_error               IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$rc_actualid             OUT sys_refcursor,
                                par$rc_actual_id_det        OUT sys_refcursor,
                                par$rc_det_integracion      OUT sys_refcursor) IS
      const$nombre_func                 CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_movs_pendientes';
      const$cod_parametro_reintento     CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'ReintentosMaximosEnviosFVOaSwitch';

      const$estado_abierto              CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := 'AB';
      const$estado_cerrado              CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := 'CE';

      
      const$tipcod_actual_id            CONSTANT gepr_pcomon_###VERSION###.tipo$nel_ := 1; /* Tipo Código: ActualID */
      const$tipcod_collection_id        CONSTANT gepr_pcomon_###VERSION###.tipo$nel_ := 2; /* Tipo Código: CollectionID*/
      const$tipcod_cod_externo          CONSTANT gepr_pcomon_###VERSION###.tipo$nel_ := 3; /* Tipo Código: Código Externo */

      const$estado_envio_con_exito      CONSTANT gepr_pcomon_###VERSION###.tipo$nel_ := 1; /* Tipo Código: Envío con éxito */
      const$estado_envio_sin_exito      CONSTANT gepr_pcomon_###VERSION###.tipo$nel_ := 2; /* Tipo Código: Envío sin éxito */
      const$estado_envio_pendiente      CONSTANT gepr_pcomon_###VERSION###.tipo$nel_ := 3; /* Tipo Código: Envío pendiente */
      const$des_estado_abierto          CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.ftraduzir(
                                                par$cod_cultura,
                                                'EstadoIntegracionAbierto',
                                                'FechaValorOnline',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                '',
                                                0);


      const$des_estado_cerrado         CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.ftraduzir(
                                                par$cod_cultura,
                                                'EstadoIntegracionCerrado',
                                                'FechaValorOnline',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                '',
                                                0);

      const$new_line                    CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
      const$comilla_simple              CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);
      var$query_insert                  gepr_pcomon_###VERSION###.tipo$obs_;
      var$query_final                   gepr_pcomon_###VERSION###.tipo$obs_;
      var$query_select                  gepr_pcomon_###VERSION###.tipo$obs_;
      var$query_where                   gepr_pcomon_###VERSION###.tipo$obs_;
      var$query_groupby                 gepr_pcomon_###VERSION###.tipo$obs_;
      var$max_cant_reintento            gepr_pcomon_###VERSION###.tipo$nel_;
      var$bol_aplica_estado_abierto     gepr_pcomon_###VERSION###.tipo$nbol_;
      var$bol_aplica_estado_cerrado     gepr_pcomon_###VERSION###.tipo$nbol_;
      var$existeEstado                  gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
  BEGIN

    /* Limpio la tabla auxiliar */
    DELETE FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'ACTUAL_ID';

    /* #### Inicializar los cursores #### */
    OPEN par$rc_actualid FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_actual_id_det FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_det_integracion FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* Obtener la cantidad máxima de reintento */
    begin
        select max(val_param.DES_VALOR_PARAMETRO)
          into var$max_cant_reintento
          from GEPR_TPARAMETRO parametro
          inner join gepr_tparametro_valor val_param on val_param.oid_parametro = parametro.oid_parametro
         where parametro.cod_parametro = const$cod_parametro_reintento
         ;
      exception
        when no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| const$cod_parametro_reintento);
        when others THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'Ocurrió un error al intentar obtener el var$max_cant_reintento para el código: '|| const$cod_parametro_reintento);
    end;


    /* Query insert */
    var$query_insert := 'INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, OID_CAMPO7, COD_CALIFICADOR)' || const$new_line;


    /* Query principal */
    var$query_select := 'SELECT DOCU.COD_ACTUAL_ID, INTE.OID_INTEGRACION, ' || const$comilla_simple || 'ACTUAL_ID' || const$comilla_simple || const$new_line;
    var$query_select := var$query_select || 'FROM  SAPR_TDOCUMENTO DOCU' || const$new_line;
    var$query_select := var$query_select || 'INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO' || const$new_line;
    var$query_select := var$query_select || 'INNER JOIN SAPR_TCUENTA CUEN ON (DOCU.OID_CUENTA_SALDO_ORIGEN = CUEN.OID_CUENTA OR DOCU.OID_CUENTA_SALDO_DESTINO = CUEN.OID_CUENTA)' || const$new_line;
    var$query_select := var$query_select || 'INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = CUEN.OID_SECTOR'|| const$new_line;
    var$query_select := var$query_select || 'INNER JOIN GEPR_TINTEGRACION INTE ON   INTE.COD_TABLA_INTEGRACION = ' || const$comilla_simple || 'COD_ACTUAL_ID' || const$comilla_simple || ' AND INTE.OID_TABLA_INTEGRACION = DOCU.COD_ACTUAL_ID' || const$new_line;
    var$query_select := var$query_select || 'INNER JOIN GEPR_TINTEGRACION_DETALLE INDE ON   INTE.OID_INTEGRACION = INDE.OID_INTEGRACION ' || const$new_line;

    /* Query agrupación */
    var$query_groupby := 'GROUP BY   DOCU.COD_ACTUAL_ID, INTE.OID_INTEGRACION ';
    
    dbms_output.put_line('Imprimir var$query_select' || const$new_line);
    dbms_output.put_line(var$query_select);

    /* Query WHERE */
    var$query_where := 'WHERE 1 = 1 '; /* No finaliza con nueva línea porque puede no tener un filtro */

    IF par$codigo IS NOT NULL THEN
        dbms_output.put_line('Tiene código (' ||par$codigo||') y tipo de código(' || par$tipo_codigo||')');
        IF par$tipo_codigo = const$tipcod_actual_id THEN
            var$query_where := var$query_where || ' AND DOCU.COD_ACTUAL_ID = ' || const$comilla_simple || par$codigo || const$comilla_simple || const$new_line;
        END IF;

        IF par$tipo_codigo = const$tipcod_collection_id THEN
            var$query_where := var$query_where || ' AND DOCU.COD_COLLECTION_ID = ' || const$comilla_simple || par$codigo || const$comilla_simple || const$new_line;
        END IF;

        IF par$tipo_codigo = const$tipcod_cod_externo THEN
            var$query_where := var$query_where || ' AND DOCU.COD_EXTERNO = ' || const$comilla_simple || par$codigo || const$comilla_simple || const$new_line;
        END IF;
    END IF;

    IF par$cod_device_id IS NOT NULL THEN
        dbms_output.put_line('Tiene device_id: ' || par$cod_device_id);
        var$query_where := var$query_where || ' AND MAQU.COD_IDENTIFICACION = ' || const$comilla_simple || par$cod_device_id || const$comilla_simple || const$new_line;
    END IF;

    IF par$oid_cliente IS NOT NULL THEN
        dbms_output.put_line('Tiene oid_cliente: ' || par$oid_cliente);
        var$query_where := var$query_where || ' AND CUEN.OID_CLIENTE = ' || const$comilla_simple || par$oid_cliente || const$comilla_simple || const$new_line;
    END IF;

    IF par$oid_subcliente IS NOT NULL THEN
        dbms_output.put_line('Tiene oid_subcliente: ' || par$oid_subcliente);
        var$query_where := var$query_where || ' AND CUEN.OID_SUBCLIENTE = ' || const$comilla_simple || par$oid_subcliente || const$comilla_simple || const$new_line;
    END IF;

    IF par$oid_pto_servicio IS NOT NULL or par$oid_pto_servicio <> '' THEN
        dbms_output.put_line('Tiene oid_pto_servicio: ' || par$oid_pto_servicio);
        var$query_where := var$query_where || ' AND CUEN.OID_PTO_SERVICIO = ' || const$comilla_simple || par$oid_pto_servicio || const$comilla_simple || const$new_line;
    END IF;

    IF par$aestado.count > 1 THEN
        dbms_output.put_line('Tiene un total de ' || par$aestado.count || ' de estados');
        var$query_where := var$query_where || ' AND (';
        FOR idx in par$aestado.first .. par$aestado.last  LOOP
          IF par$aestado(idx) = const$estado_envio_con_exito THEN
            dbms_output.put_line('Uno de estos estados es ENVIO CON EXITO');
            IF var$existeEstado = 1 THEN
              var$query_where := var$query_where || ' OR ';
            END IF;
            var$query_where := var$query_where || 'INTE.COD_ESTADO = '|| const$comilla_simple || const$estado_cerrado || const$comilla_simple  || const$new_line;
            var$existeEstado := 1;
          END IF;
          IF par$aestado(idx) = const$estado_envio_sin_exito THEN
            dbms_output.put_line('Uno de estos estados es ENVIO SIN EXITO');
            IF var$existeEstado = 1 THEN
              var$query_where := var$query_where || ' OR ';
            END IF;
            var$query_where := var$query_where || '(INTE.NEL_INTENTO_ENVIO >= '|| var$max_cant_reintento || ' AND INTE.COD_ESTADO = '|| const$comilla_simple || const$estado_abierto || const$comilla_simple  || ') '|| const$new_line;
            var$existeEstado := 1;
          END IF;
          IF par$aestado(idx) = const$estado_envio_pendiente THEN
            dbms_output.put_line('Uno de estos estados es ENVIO PENDIENTE');
             IF var$existeEstado = 1 THEN
              var$query_where := var$query_where || ' OR ';
            END IF;
            var$query_where := var$query_where || '(INTE.NEL_INTENTO_ENVIO < '|| var$max_cant_reintento || ' AND INTE.COD_ESTADO = '|| const$comilla_simple || const$estado_abierto || const$comilla_simple  || ') '|| const$new_line;
            var$existeEstado := 1;
          END IF;
        END LOOP;
        var$query_where := var$query_where || ')';
    END IF;

    IF par$cod_proceso IS NOT NULL THEN
        dbms_output.put_line('Tiene cod_proceso: ' || par$cod_proceso);
        var$query_where := var$query_where || ' AND INTE.COD_PROCESO like ' || const$comilla_simple || '%' || par$cod_proceso || '%' || const$comilla_simple || const$new_line;
    END IF;

    IF par$msg_error IS NOT NULL THEN
        dbms_output.put_line('Tiene msg_error: ' || par$msg_error);
        var$query_where := var$query_where || ' AND INDE.DES_COMENTARIO like ' || const$comilla_simple || '%' || par$msg_error || '%' || const$comilla_simple || const$new_line;
    END IF;

    var$query_final := var$query_insert || var$query_select || var$query_where || var$query_groupby;
    
    dbms_output.put_line('Query final:');
    dbms_output.put_line(var$query_final);
    
    /*Ejecutamos la query final */
    EXECUTE IMMEDIATE var$query_final;

    /* Cursor de Actual IDs */
    OPEN par$rc_actualid FOR
        SELECT
          INTE.OID_TABLA_INTEGRACION COD_ACTUAL_ID,
          INTE.COD_PROCESO,
          INTE.NEL_INTENTO_ENVIO || ' / ' || var$max_cant_reintento as REINTENTOS,
          INTE.COD_ESTADO
        FROM
          GEPR_TINTEGRACION INTE
          INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO7 = INTE.OID_INTEGRACION AND INTE.COD_TABLA_INTEGRACION = 'COD_ACTUAL_ID' AND AUX.COD_CALIFICADOR = 'ACTUAL_ID'
        ;

    /* Cursor de Actual IDs Detalle */
    OPEN par$rc_actual_id_det FOR
        SELECT 
            DOCU.COD_ACTUAL_ID,
            CUEN.COD_CLIENTE,
            CUEN.DES_CLIENTE,
            CUEN.COD_SUBCLIENTE,
            CUEN.DES_SUBCLIENTE,
            CUEN.COD_PTO_SERVICIO,
            CUEN.DES_PTO_SERVICIO,
            CUEN.COD_IDENTIFICACION_MAQUINA COD_MAQUINA,
            CUEN.DES_SECTOR DES_MAQUINA,
            CUEN.COD_CANAL,
            CUEN.DES_CANAL,
            CUEN.COD_SUBCANAL,
            CUEN.DES_SUBCANAL,
            DOCU.COD_EXTERNO,
            DOCU.FYH_GESTION,
            FORM.COD_FORMULARIO,
            FORM.DES_FORMULARIO
        FROM
            SAPR_TDOCUMENTO DOCU
        INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO
        INNER JOIN SAPR_VCUENTA CUEN ON (DOCU.OID_CUENTA_SALDO_ORIGEN = CUEN.OID_CUENTA OR DOCU.OID_CUENTA_SALDO_DESTINO = CUEN.OID_CUENTA)
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = DOCU.COD_ACTUAL_ID AND AUX.COD_CALIFICADOR = 'ACTUAL_ID';

    /* Cursor de detalle integración */
    OPEN par$rc_det_integracion FOR
        SELECT        
            AUX.OID_CAMPO1 COD_ACTUAL_ID,        
            INDE.NEL_REINTENTOS NRO_REINTENTOS,        
            INDE.DES_COMENTARIO COMENTARIO,
            INDE.FYH_CREACION FECHA,
            INDE.TIPO_ERROR TIPO_ERROR
        FROM
          GEPR_TINTEGRACION_DETALLE INDE 
          INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO7 = INDE.OID_INTEGRACION 
        AND AUX.COD_CALIFICADOR = 'ACTUAL_ID';
  END srecuperar_movs_pendientes;

  PROCEDURE sbusqueda_pend_integracion( par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_,    
                                        par$desc_parametro_reintento    IN gepr_pcomon_###VERSION###.tipo$desc_, 
                                        par$acod_estado                 IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$cod_proceso                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_origen                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_destino                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rc_pendientes               OUT sys_refcursor) IS
                                         
  const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sbusqueda_pendientes_integracion';
  var$max_cant_reintento GEPR_TPARAMETRO_VALOR.DES_VALOR_PARAMETRO%TYPE;
  var$err       varchar2(4000);
  var$obs_detalle             gepr_pcomon_###VERSION###.tipo$obs_;
  BEGIN
    /* #### Inicializar los cursores #### */
    OPEN par$rc_pendientes FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    DELETE FROM SAPR_GTT_TAUXILIAR;
    COMMIT;
    BEGIN

    IF par$oid_llamada IS NOT NULL THEN
        var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$desc_parametro_reintento: ' || const$comilla_simple || par$desc_parametro_reintento || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_proceso: ' || const$comilla_simple || par$cod_proceso || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_origen: ' || const$comilla_simple || par$cod_origen || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_destino: ' || const$comilla_simple || par$cod_destino || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$info_ejecucion: ' || const$comilla_simple || par$info_ejecucion || const$comilla_simple || const$new_line;
        var$obs_detalle := var$obs_detalle || 'par$cod_pais: ' || const$comilla_simple || par$cod_pais || const$comilla_simple || const$new_line;

     
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$obs_detalle,
                par$cod_identificador   => '');
    END IF;
       
    EXCEPTION
        WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error agregar detalle');
    END;
    

    /* INICIO LOG de arrays asociativos */
    IF par$acod_estado IS NOT NULL AND par$acod_estado.COUNT > 0 THEN
        FOR idx IN  par$acod_estado.first ..  par$acod_estado.last LOOP 
           

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
            VALUES (par$acod_estado(idx), 'COD_ESTADO');
            
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Insert in SAPR_GTT_TAUXILIAR CodEstado '|| par$acod_estado(idx),
                par$cod_identificador   => '');
        END LOOP; 
    END IF;   

    IF par$cod_pais IS NULL THEN
        BEGIN
            SELECT max(DES_VALOR_PARAMETRO)
            INTO var$max_cant_reintento
            FROM GEPR_TPARAMETRO
            INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
            WHERE GEPR_TPARAMETRO.cod_parametro = par$desc_parametro_reintento;
        EXCEPTION WHEN no_data_found THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| par$desc_parametro_reintento);
             SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No existe un valor para el codigo de parametro '|| par$desc_parametro_reintento,
                par$cod_identificador   => '');
        WHEN others THEN 
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'Ocurrió un error al intentar obtener el var$max_cant_reintento para el código: '|| par$desc_parametro_reintento);
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Ocurrió un error al intentar obtener el var$max_cant_reintento para el código: '|| par$desc_parametro_reintento,
                par$cod_identificador   => '');
        END;
    ELSE
      /* Obtener la cantidad máxima de reintento */
      var$max_cant_reintento := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                              par$cod_pais       => par$cod_pais,
                              par$cod_parametro  => par$desc_parametro_reintento,
                              par$cod_aplicacion => gepr_pcomon_###VERSION###.const$codAplicacionGenesis);
    END IF;
    
    BEGIN
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se obtiene valor de parametro ' || par$desc_parametro_reintento ||
                                    ': ' || var$max_cant_reintento,
                    par$cod_identificador   => '');
    EXCEPTION
        WHEN OTHERS THEN
            DBMS_OUTPUT.PUT_LINE('Error agregar detalle');
    END;

    IF var$max_cant_reintento IS NULL THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| par$desc_parametro_reintento);
    END IF;

    OPEN par$rc_pendientes FOR
        SELECT I.OID_INTEGRACION, I.OID_TABLA_INTEGRACION, I.COD_ESTADO, I.COD_TABLA_INTEGRACION
        FROM GEPR_TINTEGRACION I
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = I.COD_ESTADO AND AUX.COD_CALIFICADOR = 'COD_ESTADO'
        WHERE
        I.COD_PROCESO = par$cod_proceso
        AND I.COD_MODULO_ORIGEN = par$cod_origen
        AND I.COD_MODULO_DESTINO = par$cod_destino
        AND I.NEL_INTENTO_ENVIO < var$max_cant_reintento;
     
  EXCEPTION
    WHEN OTHERS THEN

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                            null,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                            null,
                                                           par$cod_ejecucion);
     SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Error: '|| var$err,
                par$cod_identificador   => '');
  END sbusqueda_pend_integracion;
  
   FUNCTION fhay_error RETURN BOOLEAN IS
    var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    BEGIN

      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR A
       WHERE A.COD_CALIFICADOR = 'VALIDACIONES' and A.OID_CAMPO1 = '2040160007';

    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;

    RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);

  END fhay_error;


PROCEDURE svalida_integracion(par$acod_actual_id IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                              par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$cod_,
                              par$rc_resultado    OUT sys_refcursor) IS
    var$consulta       clob := ''; 
    var$filtro         clob := ''; 
    
    const$cod_parametro_tipo_plani CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchTipoPlanificacion';
    const$cod_parametro_sin_fvo CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchSinFechaValor';
    const$cod_parametro_tipo_doc CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchDocumento';
     
    var$enviar_datos_tipo_plani varchar2(4000);
    var$enviar_datos_switch varchar2(4000);
    var$tipo_planificacion varchar2(4000);
    var$tipo_documento varchar2(4000);
      
    var$oid_maquina            gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_periodo            gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_planificacion gepr_pcomon_###VERSION###.tipo$cod_;
    
    var$cod_validaciones_int   gepr_pcomon_###VERSION###.tipo$cods_;
    var$des_validaciones_int   gepr_pcomon_###VERSION###.tipo$descs_;
        CURSOR cur$movimientos IS
              SELECT D.OID_DOCUMENTO,
                     D.FYH_GESTION,
                     D.COD_ACTUAL_ID,
                     SE.COD_SECTOR,
                     CA.COD_CANAL,
                     SC.COD_SUBCANAL,
                     PS.OID_PTO_SERVICIO,
                     MAX(EFXDOC.OID_DIVISA) AS OID_DIVISA                  
              FROM SAPR_TDOCUMENTO D
        INNER JOIN SAPR_GTT_TAUXILIAR T ON T.OID_CAMPO1 = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
        INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = D.OID_CUENTA_ORIGEN
        INNER JOIN GEPR_TPUNTO_SERVICIO PS ON PS.OID_PTO_SERVICIO = C.OID_PTO_SERVICIO
        INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
        INNER JOIN GEPR_TCANAL CA ON CA.OID_CANAL = SC.OID_CANAL
        INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = C.OID_SECTOR
        INNER JOIN SAPR_TFORMULARIO FM ON FM.OID_FORMULARIO = D.OID_FORMULARIO
        LEFT JOIN SAPR_TEFECTIVOXDOCUMENTO EFXDOC ON d.OID_DOCUMENTO = EFXDOC.OID_DOCUMENTO
        GROUP BY D.OID_DOCUMENTO, D.FYH_GESTION, D.COD_ACTUAL_ID, SE.COD_SECTOR, CA.COD_CANAL, SC.COD_SUBCANAL, PS.OID_PTO_SERVICIO;
        
  BEGIN
    
  
    DELETE SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR IN ('OID_DOCUMENTO', 'RESPUESTA');
           COMMIT;
/* Obtener parametros */
        BEGIN 
            SELECT DES_VALOR_PARAMETRO
            INTO var$enviar_datos_tipo_plani
            FROM GEPR_TPARAMETRO
            INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
            WHERE GEPR_TPARAMETRO.cod_parametro = const$cod_parametro_tipo_plani and rownum = 1;

            DBMS_OUTPUT.PUT_LINE('var$enviar_datos_tipo_plani: ' || var$enviar_datos_tipo_plani);

            EXCEPTION WHEN no_data_found THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| const$cod_parametro_tipo_plani);
            WHEN others THEN 
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'Ocurrió un error al intentar obtener el var$cod_parametro_tipo_plani para el código: '|| const$cod_parametro_tipo_plani);
        END;
        BEGIN 


            SELECT DES_VALOR_PARAMETRO
            INTO var$enviar_datos_switch
            FROM GEPR_TPARAMETRO
            INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
            WHERE GEPR_TPARAMETRO.cod_parametro = const$cod_parametro_sin_fvo and rownum = 1;
            
            DBMS_OUTPUT.PUT_LINE('var$enviar_datos_switch: ' || var$enviar_datos_switch);
            
            EXCEPTION WHEN no_data_found THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| const$cod_parametro_sin_fvo);
            WHEN others THEN 
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'Ocurrió un error al intentar obtener el var$cod_parametro_sin_fvo para el código: '|| const$cod_parametro_sin_fvo);
        END;
        
        BEGIN 


            SELECT DES_VALOR_PARAMETRO
            INTO var$tipo_documento
            FROM GEPR_TPARAMETRO
            INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
            WHERE GEPR_TPARAMETRO.cod_parametro = const$cod_parametro_tipo_doc and rownum = 1;
            
            DBMS_OUTPUT.PUT_LINE('var$tipo_documento: ' || var$tipo_documento);
                        
            EXCEPTION WHEN no_data_found THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| const$cod_parametro_tipo_doc);
            WHEN others THEN 
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'Ocurrió un error al intentar obtener el const$cod_parametro_tipo_doc para el código: '|| const$cod_parametro_tipo_doc);
        END;
        /*Fin Obtener parametros*/





  

     
       IF var$tipo_documento IS NOT NULL  THEN
         var$filtro := var$filtro || q'[ AND ']' || var$tipo_documento || q'[' LIKE '%' || FM.COD_FORMULARIO ||  '%' ]' ;
       
     END IF;
     

      IF par$acod_actual_id IS NOT NULL AND par$acod_actual_id.COUNT > 0  THEN
        
         var$filtro := var$filtro || ' AND DOCU.COD_ACTUAL_ID IN (';
         FOR idx IN par$acod_actual_id.first .. par$acod_actual_id.last LOOP
             IF par$acod_actual_id(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$acod_actual_id(idx))), 0) > 0 THEN
                IF idx > 2 THEN
                   var$filtro := var$filtro || ', ';
                END IF;
                var$filtro := var$filtro || '''' || par$acod_actual_id(idx) || '''';
             END IF;
          END LOOP;
          var$filtro := var$filtro || ') ';
          
          
       var$consulta :=q'[
       INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                     SELECT DOCU.OID_DOCUMENTO, 'OID_DOCUMENTO'
                     FROM SAPR_TDOCUMENTO DOCU 
                     INNER JOIN SAPR_TFORMULARIO FM ON FM.OID_FORMULARIO = DOCU.OID_FORMULARIO
                     WHERE 1=1 ]'  || var$filtro;
        DBMS_OUTPUT.PUT_LINE(var$consulta);
        EXECUTE IMMEDIATE     var$consulta;
     END IF;
        



    FOR rec$movimiento IN cur$movimientos LOOP


      SAPR_PPERIODO_###VERSION###.sverificar_periodo(
                                            par$oid_llamada            => NULL,
                                            par$cod_canal              => rec$movimiento.cod_canal,
                                            par$cod_subcanal           => rec$movimiento.cod_subcanal,
                                            par$oid_divisa             => rec$movimiento.oid_divisa,
                                            par$cod_sector             => rec$movimiento.cod_sector,
                                            par$oid_pto_servicio       => rec$movimiento.oid_pto_servicio,
                                            par$cod_ajeno              => NULL,
                                            par$fyh_gestion            => rec$movimiento.fyh_gestion,
                                            par$cod_usuario            => par$cod_usuario,
                                            par$cod_cultura            => 'es',
                                            par$info_ejecucion         => par$info_ejecucion,
                                            par$oid_maquina            => var$oid_maquina,
                                            par$oid_planificacion      => var$oid_planificacion,
                                            par$oid_periodo            => var$oid_periodo,
                                            par$cod_tipo_planificacion => var$cod_tipo_planificacion,
                                            par$cod_validaciones       => var$cod_validaciones_int,
                                            par$des_validaciones       => var$des_validaciones_int,
                                            par$cod_ejecucion          => par$cod_ejecucion);

      --var$oid_documento_s(var$oid_documento_s.COUNT) := rec$movimiento.oid_documento;

      DBMS_OUTPUT.PUT_LINE('var$oid_maquina: ' || var$oid_maquina);
      DBMS_OUTPUT.PUT_LINE('var$oid_planificacion: ' || var$oid_planificacion);
      DBMS_OUTPUT.PUT_LINE('var$oid_periodo: ' || var$oid_periodo);
      DBMS_OUTPUT.PUT_LINE('var$cod_tipo_planificacion: ' || var$cod_tipo_planificacion);
      
      IF  var$cod_tipo_planificacion IS NOT NULL THEN
          
          IF (var$enviar_datos_tipo_plani = '1' AND var$cod_tipo_planificacion <> 'FECHA_VALOR') 
              OR ( INSTR(var$enviar_datos_tipo_plani, var$cod_tipo_planificacion) > 0 ) THEN
            INSERT INTO SAPR_GTT_tAUXILIAR (OID_CAMPO1,COD_CAMPO2, COD_CALIFICADOR)
            SELECT  rec$movimiento.cod_actual_id, var$cod_tipo_planificacion, 'RESPUESTA' FROM DUAL;
          END IF;
      END IF;
    END LOOP rec$movimiento;
       
         /* BUSCAR TODOS MOVIMIENTOS POR ACTUAL ID */
  
  /* APLICAR FILTRO DE CODIGOS MOVIMENTOS*/
  OPEN par$rc_resultado FOR
  select OID_CAMPO1 AS COD_ACTUAL_ID, COD_CAMPO2 RESULTADO FROM SAPR_GTT_TAUXILIAR
  WHERE COD_CALIFICADOR = 'RESPUESTA'
  GROUP BY OID_CAMPO1,COD_CAMPO2
  ;
  
  COMMIT;
  END svalida_integracion;
END GEPR_PINTEGRACION_###VERSION###;
/