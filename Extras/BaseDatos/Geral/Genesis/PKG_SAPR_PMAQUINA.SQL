CREATE OR REPLACE PACKAGE SAPR_PMAQUINA_###VERSION### AS

  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PMAQUINA_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
  const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);

  /* Recuperar Maquinas servicio */
  PROCEDURE srecuperar_maquina(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$pag_indice               IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$pag_reg_pagina           IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$device_id                IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_delegacion           IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_planta               IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_cliente              IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_sub_cliente          IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_punto_servicio       IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_modelo               IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$maquinas_vigente         IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$recuperar_cod_ajeno      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$con_planificacion        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$rc_maquinas              OUT sys_refcursor,
                               par$rc_puntos_servicio       OUT sys_refcursor,
                               par$rc_planificacion         OUT sys_refcursor,
                               par$validaciones             OUT sys_refcursor,
                               par$rc_limites               OUT sys_refcursor,
                               par$rc_codigo_ajeno          OUT sys_refcursor,
                               par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validar codigos */
  PROCEDURE svalidar_codigos(par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_delegacion          IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_planta              IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_cliente             IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_sub_cliente         IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_punto_servicio      IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$selects                 IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$validaciones            OUT gepr_pcomon_###VERSION###.tipo$descs_);

  /* Configurar maquina */
  PROCEDURE sconfigurar_maquinas(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$abol_alta                IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                 par$acod_device_id           IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$ades_descripcion         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$acod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$acod_modelo              IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$acod_fabricante          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$abol_cons_recuentos      IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                 par$abol_multiclientes       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                 par$a_plan_bol_baja          IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                 par$a_plan_cod_planificacion IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$a_plan_fyh_inicio        IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                 par$a_plan_fyh_fin           IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                 par$a_plan_nel_gmt_minuto    IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 par$a_puntos_cod_device_id   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$a_puntos_bol_baja        IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                 par$a_puntos_cod_punto       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$a_lim_cod_divisa         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$a_lim_cod_pto            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$a_lim_num                IN gepr_pcomon_###VERSION###.tipo$imps_,
                                 par$a_lim_bol_baja           IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                 par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                 par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$rc_validaciones          OUT sys_refcursor,
                                 par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$cod_);

 PROCEDURE supd_sector_maquina( var$oid_sector      in out  gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_maquina     in gepr_pcomon_###VERSION###.tipo$oid_,
                                par$des_sector      in gepr_pcomon_###VERSION###.tipo$desc_,
                                par$oid_planta      in gepr_pcomon_###VERSION###.tipo$oid_,
                                par$bol_activo      in gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$cod_usuario     in gepr_pcomon_###VERSION###.tipo$usr_);

  PROCEDURE supd_sector( par$oid_sector                  in out gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_sector_padre            in gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_tipo_sector             in gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_planta                  in gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_sector                  in gepr_pcomon_###VERSION###.tipo$cod_,
                          par$des_sector                  in gepr_pcomon_###VERSION###.tipo$desc_,
                          par$cod_migracion               in gepr_pcomon_###VERSION###.tipo$cod_,
                          par$bol_permite_disponer_valor  in gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$bol_centro_proceso          in gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$bol_activo                  in gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$cod_usuario                 in gepr_pcomon_###VERSION###.tipo$usr_,
                          par$bol_conteo                  in gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$bol_tesoro                  in gepr_pcomon_###VERSION###.tipo$nbol_);

 PROCEDURE supd_maquina(par$oid_maquina                 IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_sector                  IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_modelo                  IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_tipo_maquina            IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_identificacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$bol_activo                  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$cod_usuario    IN gepr_pcomon_###VERSION###.tipo$desc_,
                          par$bol_considera_recuentos     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$bol_multiclientes     IN gepr_pcomon_###VERSION###.tipo$nbol_);

  /* Validar informaciones configurar maquina */
  PROCEDURE svalidar_codigos_configurar(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$bol_alta                IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$cod_device_id           IN gepr_pcomon_###VERSION###.tipo$cod_,   
                                        par$des_descripcion         IN gepr_pcomon_###VERSION###.tipo$desc_,  
                                        par$cod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_fabricante          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_modelo              IN gepr_pcomon_###VERSION###.tipo$cod_,            
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_modelo              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_delegacion          OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_maquina             OUT gepr_pcomon_###VERSION###.tipo$oid_);

  /* Validar informaciones del relacionamiento maquina x planificacion */
  PROCEDURE svalidar_planxmaquina(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_device_id     IN gepr_pcomon_###VERSION###.tipo$cod_,   
                                  par$bol_baja_plan     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$cod_planificacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_delegacion    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_planificacion IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$fyh_inicio        IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$fyh_fin           IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$nel_gmt_minuto    IN OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validar informaciones de los puntos de servicios */
  PROCEDURE svalidar_pto_servicio(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$a_puntos_cod_device_id  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_puntos_bol_baja       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_puntos_cod_punto      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Validar si existe error con las informaciones de la maquina */
  FUNCTION fhay_error(par$cod_device_id IN gepr_pcomon_###VERSION###.tipo$cod_) RETURN BOOLEAN;

  PROCEDURE srel_punto_maquina(par$oid_maquina IN  gepr_pcomon_###VERSION###.tipo$oid_,                     
                               par$cod_device_id           IN  gepr_pcomon_###VERSION###.tipo$cod_,                     
                               par$cod_usuario    IN gepr_pcomon_###VERSION###.tipo$desc_);
                          
    
  /* Bajar maquina */
  PROCEDURE sbajar_maquinas(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$acod_device_id           IN gepr_pcomon_###VERSION###.tipo$cods_,
                            par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$rc_validaciones          OUT sys_refcursor,
                            par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$cod_);

  /* Validar informaciones para bajar maquina */
  PROCEDURE svalidar_bajar_maquina(par$cod_device_id IN gepr_pcomon_###VERSION###.tipo$cod_,   
                                   par$cod_cultura   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$oid_maquina   OUT gepr_pcomon_###VERSION###.tipo$oid_);

  /* sdel_tmaquina */
  PROCEDURE sdel_tmaquina(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* sdel_tsector */
  PROCEDURE sdel_tsector(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* sdel_tplanxmaquina */
  PROCEDURE sdel_tplanxmaquina(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_);
               


  /* sdel_tperiodos */
  PROCEDURE sdel_tperiodos(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_);

    PROCEDURE srecuperar_comission_dtbanca(par$oid_maquinas IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$rc_comision             OUT sys_refcursor);
  
  
  
  
   PROCEDURE srecuperar_plancanal_maquina(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$rc_canales              OUT sys_refcursor,
                               par$rc_subcanales           OUT sys_refcursor);     
  
END SAPR_PMAQUINA_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PMAQUINA_###VERSION### AS

  /* Recuperar Maquinas servicio */
  PROCEDURE srecuperar_maquina(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$pag_indice              IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$pag_reg_pagina          IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$device_id               IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_planta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_cliente             IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_sub_cliente         IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_punto_servicio      IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_modelo              IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$maquinas_vigente        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$recuperar_cod_ajeno     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$con_planificacion       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$rc_maquinas             OUT sys_refcursor,
                               par$rc_puntos_servicio      OUT sys_refcursor,
                               par$rc_planificacion        OUT sys_refcursor,
                               par$validaciones            OUT sys_refcursor,
                               par$rc_limites              OUT sys_refcursor,
                               par$rc_codigo_ajeno         OUT sys_refcursor,
                               par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.srecuperar_maquina';

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
    var$validaciones       gepr_pcomon_###VERSION###.tipo$descs_;
    var$tvalidaciones      varchar2_array;
    var$cod_delegacion     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planta         gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_cliente        gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sub_cliente    gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_punto_servicio gepr_pcomon_###VERSION###.tipo$cod_;
    var$consulta           clob;
    var$pag_indice         gepr_pcomon_###VERSION###.tipo$nel_;
    var$pag_reg_pagina     gepr_pcomon_###VERSION###.tipo$nel_;
    var$queryMaquinas      gepr_pcomon_###VERSION###.tipo$obs_ := ''; 
    var$filtros_maquinas   gepr_pcomon_###VERSION###.tipo$obs_ := '';

  BEGIN



    /* limpiar tabla temporaria */
    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TOIDS';
    var$deletes   := var$deletes + sql%ROWCOUNT;

    /* #### Inicializar los cursores #### */
    OPEN par$rc_maquinas FOR  SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_puntos_servicio FOR  SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_planificacion FOR  SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$validaciones FOR  SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_limites FOR  SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_codigo_ajeno FOR  SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* #### Grabar llamadas #### */
    IF par$oid_llamada is not null THEN

      /* OID_LLAMADA */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada     => par$oid_llamada,
				par$des_origen      => const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$oid_llamada: ' || par$oid_llamada,
				par$cod_identificador   => '');

      /* PAG_INDICE */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada     => par$oid_llamada,
				par$des_origen      => const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$pag_indice: ' || par$pag_indice,
				par$cod_identificador   => '');

      /* PAG_REG_PAGINA */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$pag_reg_pagina: ' || par$pag_reg_pagina,
				par$cod_identificador   => '');

      /* DEVICE_ID */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$device_id: ' || par$device_id,
				par$cod_identificador   => '');

      /* cod_delegacion */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_delegacion: ' || par$cod_delegacion,
				par$cod_identificador   => '');

      /* cod_planta */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_planta: ' || par$cod_planta,
				par$cod_identificador   => '');

      /* cod_cliente */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_cliente: ' || par$cod_cliente,
				par$cod_identificador   => '');

      /* cod_sub_cliente */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_sub_cliente: ' || par$cod_sub_cliente,
				par$cod_identificador   => '');

      /* cod_punto_servicio */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_punto_servicio: ' || par$cod_punto_servicio,
				par$cod_identificador   => '');

      /* cod_modelo */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_modelo: ' || par$cod_modelo,
				par$cod_identificador   => '');

      /* cod_identificador_ajeno */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
				par$cod_identificador   => '');

      /* maquinas_vigente */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$maquinas_vigente: ' || par$maquinas_vigente,
				par$cod_identificador   => '');

      /* cod_cultura */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
				par$cod_identificador   => '');

      /* cod_usuario */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
				par$cod_identificador   => '');

      /* recuperar_cod_ajeno */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$recuperar_cod_ajeno: ' || par$recuperar_cod_ajeno,
				par$cod_identificador   => '');

      /* con_planificacion  */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$con_planificacion : ' || par$con_planificacion ,
				par$cod_identificador   => '');
    END IF;

    /* Validar codigos */
    var$cod_delegacion     := par$cod_delegacion;
    var$cod_planta         := par$cod_planta;
    var$cod_cliente        := par$cod_cliente;
    var$cod_sub_cliente    := par$cod_sub_cliente;
    var$cod_punto_servicio := par$cod_punto_servicio;
    svalidar_codigos(par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                     par$cod_delegacion          => var$cod_delegacion,
                     par$cod_planta              => var$cod_planta,
                     par$cod_cliente             => var$cod_cliente,
                     par$cod_sub_cliente         => var$cod_sub_cliente,
                     par$cod_punto_servicio      => var$cod_punto_servicio,
                     par$selects                 => var$selects,
                     par$cod_cultura             => par$cod_cultura,
                     par$validaciones            => var$validaciones);

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      =>  const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'svalidar_codigos var$cod_delegacion : ' || var$cod_delegacion ,
      par$cod_identificador   => '');

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      =>  const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'svalidar_codigos var$cod_planta : ' || var$cod_planta ,
      par$cod_identificador   => '');

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      =>  const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'svalidar_codigos var$cod_cliente : ' || var$cod_cliente ,
      par$cod_identificador   => '');

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      =>  const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'svalidar_codigos var$cod_sub_cliente : ' || var$cod_sub_cliente ,
      par$cod_identificador   => '');

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      =>  const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'svalidar_codigos var$cod_punto_servicio : ' || var$cod_punto_servicio ,
      par$cod_identificador   => '');

    IF gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN

      /* Cargar tabla temporaria */
  IF par$device_id IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND M.COD_IDENTIFICACION = ']' || par$device_id || q'[' ]';
   END IF;
   IF par$cod_modelo IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND MO.COD_MODELO = ']' || par$cod_modelo || q'[' ]';
   END IF;
   IF var$cod_delegacion IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND DE.COD_DELEGACION = ']' || var$cod_delegacion || q'[' ]';
   END IF;
   IF var$cod_planta IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND PL.COD_PLANTA = ']' || var$cod_planta || q'[' ]';
   END IF;
   IF var$cod_cliente IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND C.COD_CLIENTE = ']' || var$cod_cliente || q'[' ]';
   END IF;
   IF var$cod_sub_cliente IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND SC.COD_SUBCLIENTE = ']' || var$cod_sub_cliente || q'[' ]';
   END IF;
   IF var$cod_punto_servicio IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND PS.COD_PTO_SERVICIO = ']' || var$cod_punto_servicio || q'[' ]';
   END IF;
   IF par$maquinas_vigente IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND M.Bol_Activo = ']' || par$maquinas_vigente || q'[' ]';
   END IF;
   IF par$con_planificacion IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND ( ']' || par$con_planificacion || q'[' != 1 OR  PM.OID_MAQUINA IS NOT NULL) ]';
   END IF;
   IF par$con_planificacion IS NOT NULL THEN
      var$filtros_maquinas := var$filtros_maquinas || q'[ AND ( ']' || par$con_planificacion || q'[' != 0 OR  PM.OID_MAQUINA IS NULL) ]';
   END IF;

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      =>  const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Construimos var$queryMaquinas.',
      par$cod_identificador   => '');

      /* #### Construimos Query  #### */
    var$queryMaquinas :=  q'[ 
      INSERT INTO SAPR_GTT_TOIDS
        (OID, COD_CALIFICADOR)
        SELECT M.OID_MAQUINA, 'OID_MAQUINAS'
          FROM SAPR_TMAQUINA M
         INNER JOIN SAPR_TMODELO MO
            ON M.OID_MODELO = MO.OID_MODELO
         INNER JOIN SAPR_TFABRICANTE FA
            ON MO.OID_FABRICANTE = FA.OID_FABRICANTE
         INNER JOIN GEPR_TSECTOR SE
            ON M.OID_SECTOR = SE.OID_SECTOR
         INNER JOIN GEPR_TPLANTA PL
            ON PL.OID_PLANTA = SE.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION DE
            ON DE.OID_DELEGACION = PL.OID_DELEGACION
         LEFT JOIN GEPR_TPUNTO_SERVICIO PS
            ON M.OID_MAQUINA = PS.OID_MAQUINA
         LEFT JOIN GEPR_TSUBCLIENTE SC
            ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
         LEFT JOIN GEPR_TCLIENTE C
            ON C.OID_CLIENTE = SC.OID_CLIENTE 
         LEFT JOIN SAPR_TPLANXMAQUINA PM 
            ON M.OID_MAQUINA = PM.OID_MAQUINA AND PM.BOL_ACTIVO = 1
            WHERE  1=1 ]' ||  var$filtros_maquinas || q'[ 
         GROUP BY M.OID_MAQUINA ]';

      /* Cargar tabla temporaria */
      EXECUTE IMMEDIATE (var$queryMaquinas);

			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'Valida par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
				par$cod_identificador   => '');

      IF par$cod_identificador_ajeno IS NULL THEN

        var$consulta := q'[
          SELECT M.OID_MAQUINA,
                        M.COD_IDENTIFICACION,
                        M.BOL_CONSIDERA_RECUENTOS,
                        M.BOL_ACTIVO,
                        MO.COD_MODELO,
                        MO.DES_MODELO,
                        FA.COD_FABRICANTE,
                        FA.DES_FABRICANTE,
                        SE.DES_SECTOR,
                        PL.COD_PLANTA,
                        PL.DES_PLANTA,
                        DE.COD_DELEGACION,
                        DE.DES_DELEGACION,
                        rank() over(order by M.COD_IDENTIFICACION, M.OID_MAQUINA) rank_
                    FROM SAPR_TMAQUINA M
                  INNER JOIN SAPR_GTT_TOIDS T
                      ON T.OID = M.OID_MAQUINA
                    AND T.COD_CALIFICADOR = 'OID_MAQUINAS'
                  INNER JOIN SAPR_TMODELO MO
                      ON M.OID_MODELO = MO.OID_MODELO
                  INNER JOIN SAPR_TFABRICANTE FA
                      ON MO.OID_FABRICANTE = FA.OID_FABRICANTE
                  INNER JOIN GEPR_TSECTOR SE
                      ON M.OID_SECTOR = SE.OID_SECTOR
                  INNER JOIN GEPR_TPLANTA PL
                      ON PL.OID_PLANTA = SE.OID_PLANTA
                  INNER JOIN GEPR_TDELEGACION DE
                      ON DE.OID_DELEGACION = PL.OID_DELEGACION
                  LEFT JOIN GEPR_TPUNTO_SERVICIO PS
                      ON M.OID_MAQUINA = PS.OID_MAQUINA
                  LEFT JOIN GEPR_TSUBCLIENTE SC
                      ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
                  LEFT JOIN GEPR_TCLIENTE C
                      ON C.OID_CLIENTE = SC.OID_CLIENTE
                  GROUP BY M.OID_MAQUINA,
                            M.COD_IDENTIFICACION,
                            M.BOL_CONSIDERA_RECUENTOS,
                            M.BOL_ACTIVO,
                            MO.COD_MODELO,
                            MO.DES_MODELO,
                            FA.COD_FABRICANTE,
                            FA.DES_FABRICANTE,
                            SE.DES_SECTOR,
                            PL.COD_PLANTA,
                            PL.DES_PLANTA,
                            DE.COD_DELEGACION,
                            DE.DES_DELEGACION
        ]';
        
        SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_identificador_ajeno IS NULL: Inicializa cursor par$rc_puntos_servicio. ',
				par$cod_identificador   => '');

          /* Recuperar Puntos de servicios */
          OPEN par$rc_puntos_servicio FOR
            SELECT PS.OID_MAQUINA,
                   PS.COD_PTO_SERVICIO,
                   PS.DES_PTO_SERVICIO,
                   SC.COD_SUBCLIENTE,
                   SC.DES_SUBCLIENTE,
                   C.COD_CLIENTE,
                   C.DES_CLIENTE
              FROM GEPR_TPUNTO_SERVICIO PS
             INNER JOIN GEPR_TSUBCLIENTE SC
                ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
             INNER JOIN GEPR_TCLIENTE C
                ON C.OID_CLIENTE = SC.OID_CLIENTE
             INNER JOIN SAPR_GTT_TOIDS T
                ON T.OID = PS.OID_MAQUINA
               AND T.COD_CALIFICADOR = 'OID_MAQUINAS'
             GROUP BY PS.OID_MAQUINA,
                      PS.COD_PTO_SERVICIO,
                      PS.DES_PTO_SERVICIO,
                      SC.COD_SUBCLIENTE,
                      SC.DES_SUBCLIENTE,
                      C.COD_CLIENTE,
                      C.DES_CLIENTE;

      SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_identificador_ajeno IS NULL: Inicializa cursor par$rc_planificacion. ',
				par$cod_identificador   => '');

          /* Recuperar Planificaciones */
          OPEN par$rc_planificacion FOR
          SELECT PM.OID_MAQUINA, P.OID_PLANIFICACION,
                 P.COD_PLANIFICACION,
                 P.DES_PLANIFICACION,
                 CAST(fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION,
                                        P.FYH_VIGENCIA_INICIO) AS DATE) AS FYH_VIGENCIA_INICIO,
                 CASE
                   WHEN P.FYH_VIGENCIA_FIN IS NULL THEN
                    NULL
                   ELSE
                    CAST(fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION,
                                           P.FYH_VIGENCIA_FIN) AS DATE)
                 END AS FYH_VIGENCIA_FIN,
                 P.BOL_ACTIVO,
                 C.COD_CLIENTE,
                 C.DES_CLIENTE,
                 TP.COD_TIPO_PLANIFICACION,
                 TP.DES_TIPO_PLANIFICACION,
                 D.COD_DELEGACION,
                 D.DES_DELEGACION,
                 P.NEC_CONTINGENCIA
            FROM SAPR_TPLANIFICACION P
           INNER JOIN GEPR_TCLIENTE C
              ON C.OID_CLIENTE = P.OID_CLIENTE
           INNER JOIN SAPR_TTIPO_PLANIFICACION TP
              ON P.OID_TIPO_PLANIFICACION = TP.OID_TIPO_PLANIFICACION
           INNER JOIN GEPR_TDELEGACION D
              ON D.OID_DELEGACION = P.OID_DELEGACION
           INNER JOIN SAPR_TPLANXMAQUINA PM
              ON PM.OID_PLANIFICACION = P.OID_PLANIFICACION AND PM.BOL_ACTIVO = 1
           INNER JOIN SAPR_GTT_TOIDS T
              ON T.OID = PM.OID_MAQUINA
             AND T.COD_CALIFICADOR = 'OID_MAQUINAS'
           GROUP BY PM.OID_MAQUINA, P.OID_PLANIFICACION,
                 P.COD_PLANIFICACION,
                 P.DES_PLANIFICACION,
                 P.OID_DELEGACION,
                 P.FYH_VIGENCIA_INICIO,
                 P.FYH_VIGENCIA_FIN,
                 P.BOL_ACTIVO,
                 C.COD_CLIENTE,
                 C.DES_CLIENTE,
                 TP.COD_TIPO_PLANIFICACION,
                 TP.DES_TIPO_PLANIFICACION,
                 D.COD_DELEGACION,
                 D.DES_DELEGACION,
                 P.NEC_CONTINGENCIA;

      SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      =>  const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_identificador_ajeno IS NULL: Inicializa cursor par$rc_limites. ',
				par$cod_identificador   => '');

          /* Recuperar limites */
          OPEN par$rc_limites FOR
          SELECT 
              limite.num_limite valor,
              maquina.COD_IDENTIFICACION DeviceID,
              sector.des_sector des_sector,
              divisa.COD_ISO_DIVISA,
              divisa.des_divisa,
              ptoser.cod_pto_servicio,
              ptoser.des_pto_servicio
          FROM sapr_Tlimite limite
              INNER JOIN sapr_tmaquina maquina ON limite.oid_maquina = maquina.oid_maquina
              INNER JOIN SAPR_GTT_TOIDS T ON T.OID = maquina.OID_MAQUINA AND T.COD_CALIFICADOR = 'OID_MAQUINAS'
              INNER JOIN gepr_tsector sector ON sector.oid_sector = maquina.oid_sector
              INNER JOIN gepr_tdivisa divisa ON divisa.oid_divisa = limite.oid_divisa
              LEFT JOIN gepr_tpunto_servicio ptoser ON ptoser.oid_pto_servicio = limite.oid_pto_servicio
        WHERE limite.bol_activo = 1;
      ELSE
        var$consulta := q'[
          SELECT M.OID_MAQUINA,
                        M.COD_IDENTIFICACION,
                        M.BOL_CONSIDERA_RECUENTOS,
                        M.BOL_ACTIVO,
                        MO.COD_MODELO,
                        MO.DES_MODELO,
                        FA.COD_FABRICANTE,
                        FA.DES_FABRICANTE,
                        CAS.COD_AJENO COD_SECTOR,
                        CAS.DES_AJENO DES_SECTOR,
                        CAP.COD_AJENO COD_PLANTA,
                        CAP.DES_AJENO DES_PLANTA,
                        CAD.COD_AJENO COD_DELEGACION,
                        CAD.DES_AJENO DES_DELEGACION,
                        rank() over(order by M.COD_IDENTIFICACION, M.OID_MAQUINA) rank_
                    FROM SAPR_TMAQUINA M
                  INNER JOIN SAPR_TMODELO MO
                      ON M.OID_MODELO = MO.OID_MODELO
                  INNER JOIN SAPR_TFABRICANTE FA
                      ON MO.OID_FABRICANTE = FA.OID_FABRICANTE
                  INNER JOIN GEPR_TSECTOR SE
                      ON M.OID_SECTOR = SE.OID_SECTOR
                  INNER JOIN GEPR_TPLANTA PL
                      ON PL.OID_PLANTA = SE.OID_PLANTA
                  INNER JOIN GEPR_TCODIGO_AJENO CAS
                      ON CAS.OID_TABLA_GENESIS = SE.OID_SECTOR AND CAS.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAS.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
                  INNER JOIN GEPR_TCODIGO_AJENO CAP
                      ON CAP.OID_TABLA_GENESIS = PL.OID_PLANTA AND CAP.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAP.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
                  INNER JOIN GEPR_TCODIGO_AJENO CAD
                      ON CAD.OID_TABLA_GENESIS = PL.OID_DELEGACION AND CAD.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAD.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
                  INNER JOIN SAPR_GTT_TOIDS T
                      ON T.OID = M.OID_MAQUINA
                    AND T.COD_CALIFICADOR = 'OID_MAQUINAS'
                  GROUP BY M.OID_MAQUINA,
                            M.COD_IDENTIFICACION,
                            M.BOL_CONSIDERA_RECUENTOS,
                            M.BOL_ACTIVO,
                            MO.COD_MODELO,
                            MO.DES_MODELO,
                            FA.COD_FABRICANTE,
                            FA.DES_FABRICANTE,
                            CAS.COD_AJENO,
                            CAS.DES_AJENO,
                            CAP.COD_AJENO,
                            CAP.DES_AJENO,
                            CAD.COD_AJENO,
                            CAD.DES_AJENO

        ]';

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      =>  const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'par$cod_identificador_ajeno IS NOT NULL: Inicializa cursor par$rc_puntos_servicio. ',
          par$cod_identificador   => '');

          /* Recuperar Puntos de servicios */
          OPEN par$rc_puntos_servicio FOR
            SELECT PS.OID_MAQUINA,
                   CAPS.COD_AJENO COD_PTO_SERVICIO,
                   CAPS.DES_AJENO DES_PTO_SERVICIO,
                   CASC.COD_AJENO COD_SUBCLIENTE,
                   CASC.DES_AJENO DES_SUBCLIENTE,
                   CAC.COD_AJENO COD_CLIENTE,
                   CAC.DES_AJENO DES_CLIENTE
              FROM GEPR_TPUNTO_SERVICIO PS
             INNER JOIN GEPR_TSUBCLIENTE SC
                ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
             INNER JOIN GEPR_TCODIGO_AJENO CAPS
                ON CAPS.OID_TABLA_GENESIS = PS.OID_PTO_SERVICIO AND CAPS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAPS.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
             INNER JOIN GEPR_TCODIGO_AJENO CASC
                ON CASC.OID_TABLA_GENESIS = SC.OID_SUBCLIENTE AND CASC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
             INNER JOIN GEPR_TCODIGO_AJENO CAC
                ON CAC.OID_TABLA_GENESIS = SC.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
             INNER JOIN SAPR_GTT_TOIDS T
                ON T.OID = PS.OID_MAQUINA
               AND T.COD_CALIFICADOR = 'OID_MAQUINAS'            
             GROUP BY PS.OID_MAQUINA,
                      CAPS.COD_AJENO,
                      CAPS.DES_AJENO,
                      CASC.COD_AJENO,
                      CASC.DES_AJENO,
                      CAC.COD_AJENO,
                      CAC.DES_AJENO;

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      =>  const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_identificador_ajeno IS NOT NULL: Inicializa cursor par$Planificaciones. ',
            par$cod_identificador   => '');

          /* Recuperar Planificaciones */
          OPEN par$rc_planificacion FOR
          SELECT PM.OID_MAQUINA, P.OID_PLANIFICACION,
                 P.COD_PLANIFICACION,
                 P.DES_PLANIFICACION,
                 CAST(fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION,
                                        P.FYH_VIGENCIA_INICIO) AS DATE) AS FYH_VIGENCIA_INICIO,
                 CASE
                   WHEN P.FYH_VIGENCIA_FIN IS NULL THEN
                    NULL
                   ELSE
                    CAST(fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION,
                                           P.FYH_VIGENCIA_FIN) AS DATE)
                 END AS FYH_VIGENCIA_FIN,
                 P.BOL_ACTIVO,
                 CAC.COD_AJENO COD_CLIENTE,
                 CAC.DES_AJENO DES_CLIENTE,
                 TP.COD_TIPO_PLANIFICACION,
                 TP.DES_TIPO_PLANIFICACION,
                 CAD.COD_AJENO COD_DELEGACION,
                 CAD.DES_AJENO DES_DELEGACION,
                 P.NEC_CONTINGENCIA
            FROM SAPR_TPLANIFICACION P
           INNER JOIN SAPR_TTIPO_PLANIFICACION TP
              ON P.OID_TIPO_PLANIFICACION = TP.OID_TIPO_PLANIFICACION
           INNER JOIN SAPR_TPLANXMAQUINA PM
              ON PM.OID_PLANIFICACION = P.OID_PLANIFICACION AND PM.BOL_ACTIVO = 1
           INNER JOIN GEPR_TCODIGO_AJENO CAC
              ON CAC.OID_TABLA_GENESIS = P.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
           INNER JOIN GEPR_TCODIGO_AJENO CAD
              ON CAD.OID_TABLA_GENESIS = P.OID_DELEGACION AND CAD.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAD.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
           INNER JOIN SAPR_GTT_TOIDS T
              ON T.OID = PM.OID_MAQUINA
             AND T.COD_CALIFICADOR = 'OID_MAQUINAS'
             GROUP BY PM.OID_MAQUINA, P.OID_PLANIFICACION,
                 P.COD_PLANIFICACION,
                 P.DES_PLANIFICACION,
                 P.OID_DELEGACION,
                 P.FYH_VIGENCIA_INICIO,
                 P.FYH_VIGENCIA_FIN,
                 P.BOL_ACTIVO,
                 CAC.COD_AJENO,
                 CAC.DES_AJENO,
                 TP.COD_TIPO_PLANIFICACION,
                 TP.DES_TIPO_PLANIFICACION,
                 CAD.COD_AJENO,
                 CAD.DES_AJENO,
                 P.NEC_CONTINGENCIA;

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      =>  const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'par$cod_identificador_ajeno IS NOT NULL: Inicializa cursor par$rc_limites. ',
          par$cod_identificador   => '');

        OPEN par$rc_limites FOR
          SELECT limite.num_limite valor,
                maquina.COD_IDENTIFICACION DeviceID,
                sector.des_ajeno des_sector,
                divisa.cod_ajeno COD_ISO_DIVISA,
                divisa.des_ajeno DES_DIVISA,
                ptoser.cod_ajeno COD_PTO_SERVICIO,
                ptoser.des_ajeno DES_PTO_SERVICIO
            FROM sapr_Tlimite limite
                INNER JOIN sapr_tmaquina maquina ON limite.oid_maquina = maquina.oid_maquina
                INNER JOIN SAPR_GTT_TOIDS T ON T.OID = maquina.OID_MAQUINA AND T.COD_CALIFICADOR = 'OID_MAQUINAS'
                INNER JOIN gepr_tcodigo_ajeno sector ON sector.oid_tabla_genesis = maquina.oid_sector  and sector.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR' and sector.cod_identificador = par$cod_identificador_ajeno
                INNER JOIN gepr_tcodigo_ajeno divisa ON divisa.oid_tabla_genesis = limite.oid_divisa and divisa.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' and divisa.cod_identificador = par$cod_identificador_ajeno
                LEFT JOIN  gepr_tcodigo_ajeno  ptoser ON ptoser.oid_tabla_genesis = limite.oid_pto_servicio and ptoser.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' and ptoser.cod_identificador = par$cod_identificador_ajeno
          WHERE limite.bol_activo = 1;

      END IF;

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      =>  const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Valida par$recuperar_cod_ajeno.',
          par$cod_identificador   => '');

         /*Recuperar Código Ajeno*/
           IF par$recuperar_cod_ajeno = 1 THEN
              SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      =>  const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'Inicializa cursor par$rc_codigo_ajeno. ',
                par$cod_identificador   => '');

              OPEN par$rc_codigo_ajeno FOR
                SELECT  T.OID OID_MAQUINA,
                        CA.COD_IDENTIFICADOR,
                        CA.COD_AJENO,
                        CA.DES_AJENO
                FROM   GEPR_TCODIGO_AJENO CA
                INNER JOIN SAPR_GTT_TOIDS T
                    ON T.OID = CA.OID_TABLA_GENESIS
                        AND T.cod_calificador = 'OID_MAQUINAS'
                WHERE  CA.COD_TIPO_TABLA_GENESIS = 'SAPR_TMAQUINA'
                AND CA.BOL_ACTIVO = 1 
                GROUP BY T.OID,
                        CA.COD_IDENTIFICADOR,
                        CA.COD_AJENO,
                        CA.DES_AJENO;
          END IF;

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

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      =>  const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'var$pag_indice: ' || var$pag_indice ,
            par$cod_identificador   => '');

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      =>  const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'var$pag_reg_pagina: ' || var$pag_reg_pagina ,
            par$cod_identificador   => '');

           var$consulta := ' SELECT * FROM ( ' || var$consulta || ' ) WHERE RANK_ BETWEEN ' || var$pag_indice || ' AND ' || var$pag_reg_pagina || ' ';
        END IF;

        DBMS_OUTPUT.PUT_LINE(var$consulta);

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      =>  const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Inicializa cursor par$rc_maquinas. ',
            par$cod_identificador   => '');

          /* Recuperar Maquina */
          OPEN par$rc_maquinas FOR
          var$consulta;
    ELSE
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      =>  const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Inicializa cursores par$rc_maquinas, par$rc_puntos_servicio, par$rc_planificacion, par$rc_codigo_ajeno y par$rc_limites. ',
            par$cod_identificador   => '');

        OPEN par$rc_maquinas FOR
            SELECT NULL OID_MAQUINA
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_puntos_servicio FOR
            SELECT NULL OID_MAQUINA
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_planificacion FOR
            SELECT NULL OID_MAQUINA
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_codigo_ajeno FOR
            SELECT NULL OID_MAQUINA
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_limites FOR
            SELECT NULL OID_MAQUINA
              FROM DUAL
              WHERE 1 <> 1;
    END IF;

    /* Validaciones */
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN
    SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      =>  const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Inicializa cursor par$tvalidaciones. ',
        par$cod_identificador   => '');

      var$tvalidaciones := gepr_putilidades_###VERSION###.fconverter_array_desc_table(par$table_varchar2 => var$validaciones);
      open par$validaciones for
        SELECT '2040010000' CODIGO,
               C.COLUMN_VALUE DESCRICION
          FROM TABLE(var$tvalidaciones) C
         WHERE C.COLUMN_VALUE IS NOT NULL;

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

    SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      =>  const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Finaliza srecuperar_maquina. ',
        par$cod_identificador   => '');

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

  END srecuperar_maquina;

  /* Validar codigos */
  PROCEDURE svalidar_codigos(par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_delegacion          IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_planta              IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_cliente             IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_sub_cliente         IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_punto_servicio      IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                             par$selects                 IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$validaciones            OUT gepr_pcomon_###VERSION###.tipo$descs_) IS

    var$cod_delegacion     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planta         gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_cliente        gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sub_cliente    gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_punto_servicio gepr_pcomon_###VERSION###.tipo$cod_;

  BEGIN

    /* Validar codigo ajeno */
    IF par$cod_identificador_ajeno IS NOT NULL THEN

      IF par$cod_delegacion IS NOT NULL THEN
        BEGIN
              SELECT DE.COD_DELEGACION
                INTO var$cod_delegacion
                FROM GEPR_TCODIGO_AJENO CA
          INNER JOIN GEPR_TDELEGACION DE
                  ON DE.OID_DELEGACION = CA.OID_TABLA_GENESIS
               WHERE CA.COD_AJENO = par$cod_delegacion
                 AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                 AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION';
          par$selects := par$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            var$cod_delegacion := NULL;
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_ajeno',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            par$cod_delegacion || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TDELEGACION',
                                                                                            1);
        END;
      END IF;

      IF par$cod_planta IS NOT NULL THEN
        BEGIN
              SELECT DE.COD_PLANTA
                INTO var$cod_planta
                FROM GEPR_TCODIGO_AJENO CA
          INNER JOIN GEPR_TPLANTA DE
                  ON DE.OID_PLANTA = CA.OID_TABLA_GENESIS
               WHERE CA.COD_AJENO = par$cod_planta
                 AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                 AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA';
          par$selects := par$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            var$cod_planta := NULL;
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_ajeno',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            par$cod_planta || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TPLANTA',
                                                                                            1);
        END;
      END IF;

      IF par$cod_cliente IS NOT NULL THEN
        BEGIN
              SELECT C.COD_CLIENTE
                INTO var$cod_cliente
                FROM GEPR_TCODIGO_AJENO CA
          INNER JOIN GEPR_TCLIENTE C
                  ON C.OID_CLIENTE = CA.OID_TABLA_GENESIS
               WHERE CA.COD_AJENO = par$cod_cliente
                 AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                 AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE';
          par$selects := par$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            var$cod_cliente := NULL;
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_ajeno',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            par$cod_cliente || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TCLIENTE',
                                                                                            1);
        END;
      END IF;

      IF par$cod_sub_cliente IS NOT NULL THEN
        BEGIN
              SELECT SCL.COD_SUBCLIENTE
                INTO var$cod_sub_cliente
                FROM GEPR_TCODIGO_AJENO CA
          INNER JOIN GEPR_TSUBCLIENTE SCL
                  ON SCL.OID_SUBCLIENTE = CA.OID_TABLA_GENESIS
               WHERE CA.COD_AJENO = par$cod_sub_cliente
                 AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                 AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE';
          par$selects := par$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            var$cod_sub_cliente := NULL;
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_ajeno',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            par$cod_sub_cliente || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TSUBCLIENTE',
                                                                                            1);
        END;
      END IF;

      IF par$cod_punto_servicio IS NOT NULL THEN
        BEGIN
              SELECT PS.COD_PTO_SERVICIO
                INTO var$cod_punto_servicio
                FROM GEPR_TCODIGO_AJENO CA
          INNER JOIN GEPR_TPUNTO_SERVICIO PS
                  ON PS.OID_PTO_SERVICIO = CA.OID_TABLA_GENESIS
               WHERE CA.COD_AJENO = par$cod_punto_servicio
                 AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                 AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO';
          par$selects := par$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            var$cod_punto_servicio := NULL;
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_ajeno',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            par$cod_punto_servicio || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TPUNTO_SERVICIO',
                                                                                            1);
        END;
      END IF;

      par$cod_delegacion     := var$cod_delegacion;
      par$cod_planta         := var$cod_planta;
      par$cod_cliente        := var$cod_cliente;
      par$cod_sub_cliente    := var$cod_sub_cliente;
      par$cod_punto_servicio := var$cod_punto_servicio;

    END IF;

    /* Validaciones de integridad */
    /* si fue informado codigo de planta, debe ser informado codigo delegacion */
    IF par$cod_planta IS NOT NULL AND par$cod_delegacion IS NULL THEN
        par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'integracion_invalida_del_plan',
                                                                                        'GENERICO',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                        NULL,
                                                                                        1);
    END IF;
    /* si fue informado codigo de planta y delegacion, debe validar integridad */
    IF par$cod_planta IS NOT NULL AND par$cod_delegacion IS NOT NULL THEN
        BEGIN
              SELECT P.COD_PLANTA
                INTO par$cod_planta
                FROM GEPR_TDELEGACION D
          INNER JOIN GEPR_TPLANTA P
                  ON P.OID_DELEGACION = D.OID_DELEGACION
               WHERE P.COD_PLANTA = par$cod_planta
                 AND D.COD_DELEGACION = par$cod_delegacion;
          par$selects := par$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            par$cod_planta := NULL;
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'integracion_invalida_del_plan',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            NULL,
                                                                                            1);
        END;
    END IF;

    /* si fue informado codigo de subcliente, debe ser informado codigo cliente */
    IF par$cod_sub_cliente IS NOT NULL AND par$cod_cliente IS NULL THEN
        par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'integracion_invalida_subcliente_cliente',
                                                                                        'GENERICO',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                        NULL,
                                                                                        1);
    END IF;
    /* si fue informado codigo de subcliente y cliente, debe validar integridad */
    IF par$cod_sub_cliente IS NOT NULL AND par$cod_cliente IS NOT NULL THEN
        BEGIN
              SELECT SC.COD_SUBCLIENTE
                INTO par$cod_sub_cliente
                FROM GEPR_TSUBCLIENTE SC
          INNER JOIN GEPR_TCLIENTE C
                  ON C.OID_CLIENTE = SC.OID_CLIENTE
               WHERE C.COD_CLIENTE = par$cod_cliente
                 AND SC.COD_SUBCLIENTE = par$cod_sub_cliente;
          par$selects := par$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            par$cod_sub_cliente := NULL;
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'integracion_invalida_subcliente_cliente',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            NULL,
                                                                                            1);
        END;
    END IF;

    /* si fue informado codigo de punto servicio, debe ser informado codigo subcliente */
    IF par$cod_punto_servicio IS NOT NULL AND (par$cod_sub_cliente IS NULL OR par$cod_cliente IS NULL) THEN
        par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'integracion_invalida_pto_subcliente_cliente',
                                                                                        'GENERICO',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                        NULL,
                                                                                        1);
    END IF;
    /* si fue informado codigo de subcliente y cliente, debe validar integridad */
    IF par$cod_punto_servicio IS NOT NULL AND par$cod_sub_cliente IS NOT NULL AND par$cod_cliente IS NOT NULL THEN
        BEGIN
              SELECT PS.COD_PTO_SERVICIO
                INTO par$cod_punto_servicio
                FROM GEPR_TPUNTO_SERVICIO PS
          INNER JOIN GEPR_TSUBCLIENTE SC
                  ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
          INNER JOIN GEPR_TCLIENTE C
                  ON C.OID_CLIENTE = SC.OID_CLIENTE
               WHERE C.COD_CLIENTE = par$cod_cliente
                 AND SC.COD_SUBCLIENTE = par$cod_sub_cliente
                 AND PS.COD_PTO_SERVICIO = par$cod_punto_servicio;
          par$selects := par$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            par$cod_sub_cliente := NULL;
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'integracion_invalida_pto_subcliente_cliente',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            NULL,
                                                                                            1);
        END;
    END IF;

  END svalidar_codigos;

  /* Configurar maquina */
   PROCEDURE sconfigurar_maquinas(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$abol_alta                IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$acod_device_id           IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$ades_descripcion         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$acod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$acod_modelo              IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$acod_fabricante          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$abol_cons_recuentos      IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$abol_multiclientes       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_plan_bol_baja          IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_plan_cod_planificacion IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_plan_fyh_inicio        IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                  par$a_plan_fyh_fin           IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                  par$a_plan_nel_gmt_minuto    IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$a_puntos_cod_device_id   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_puntos_bol_baja        IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_puntos_cod_punto       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_lim_cod_divisa         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_lim_cod_pto            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_lim_num                IN gepr_pcomon_###VERSION###.tipo$imps_,
                                  par$a_lim_bol_baja           IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                  par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$rc_validaciones          OUT sys_refcursor,
                                  par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$cod_) IS

     const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sconfigurar_maquinas';

    /* LOG */
    var$inicio            TIMESTAMP := SYSTIMESTAMP;
    var$registros         gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts           gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates           gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes           gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges            gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects           gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err               varchar2(4000);
    var$detalle           VARCHAR2(4000);
    var$detalle_actual    VARCHAR2(4000);

    var$oid_delegacion    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planta        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_maquina       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_modelo        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_tipo_maquina  gepr_pcomon_###VERSION###.tipo$oid_ ;
    var$nel_gmt_minuto    gepr_pcomon_###VERSION###.tipo$nel_;
    var$plan_fyh_inicio   gepr_pcomon_###VERSION###.tipo$fyh_;
    var$plan_fyh_fin      gepr_pcomon_###VERSION###.tipo$fyh_;
    var$oid_limite        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_pto_servicio  gepr_pcomon_###VERSION###.tipo$oid_;
    var$hayerror          gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN

    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TAUXILIAR;
    var$deletes   := var$deletes + sql%ROWCOUNT;
    
    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* Recuperar Tipo Maquina */
    BEGIN
      SELECT OID_TIPO_MAQUINA
        INTO var$oid_tipo_maquina
        FROM SAPR_TTIPO_MAQUINA
       WHERE COD_TIPO_MAQUINA = 'MAE';
    EXCEPTION WHEN no_data_found THEN
      var$oid_tipo_maquina := NULL;
    END;

    /* INICIO - LOG de parametros iniciales*/
    IF par$oid_llamada IS NOT NULL THEN
      BEGIN
        var$detalle := q'[Parametros iniciales: 
          par$oid_llamada: ]'             || par$oid_llamada || q'[
          par$cod_identificador_ajeno: ]' || par$cod_identificador_ajeno || q'[
          par$cod_cultura: ]'             || par$cod_cultura || q'[
          par$info_ejecucion: ]'          || par$info_ejecucion || q'[
          par$cod_usuario: ]'             || par$cod_usuario;

        /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
        /* INICIO LOG de arrays asociativos */
        IF par$abol_alta IS NOT NULL AND par$abol_alta.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$abol_alta.first ..  par$abol_alta.last LOOP 
              var$detalle_actual := '';
              IF par$abol_alta(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$abol_alta(' || idx || '):  ' || par$abol_alta(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$abol_alta(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$acod_device_id IS NOT NULL AND par$acod_device_id.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$acod_device_id.first ..  par$acod_device_id.last LOOP 
              var$detalle_actual := '';
              IF par$acod_device_id(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$acod_device_id(' || idx || '):  ' || par$acod_device_id(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$acod_device_id(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$ades_descripcion IS NOT NULL AND par$ades_descripcion.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$ades_descripcion.first ..  par$ades_descripcion.last LOOP 
              var$detalle_actual := '';
              IF par$ades_descripcion(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$ades_descripcion(' || idx || '):  ' || par$ades_descripcion(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$ades_descripcion(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$acod_delegacion IS NOT NULL AND par$acod_delegacion.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$acod_delegacion.first ..  par$acod_delegacion.last LOOP 
              var$detalle_actual := '';
              IF par$acod_delegacion(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$acod_delegacion(' || idx || '):  ' || par$acod_delegacion(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$acod_delegacion(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$acod_modelo IS NOT NULL AND par$acod_modelo.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$acod_modelo.first ..  par$acod_modelo.last LOOP 
              var$detalle_actual := '';
              IF par$acod_modelo(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$acod_modelo(' || idx || '):  ' || par$acod_modelo(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$acod_modelo(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$acod_fabricante IS NOT NULL AND par$acod_fabricante.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$acod_fabricante.first ..  par$acod_fabricante.last LOOP 
              var$detalle_actual := '';
              IF par$acod_fabricante(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$acod_fabricante(' || idx || '):  ' || par$acod_fabricante(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$acod_fabricante(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$abol_cons_recuentos IS NOT NULL AND par$abol_cons_recuentos.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$abol_cons_recuentos.first ..  par$abol_cons_recuentos.last LOOP 
              var$detalle_actual := '';
              IF par$abol_cons_recuentos(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$abol_cons_recuentos(' || idx || '):  ' || par$abol_cons_recuentos(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$abol_cons_recuentos(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$abol_multiclientes IS NOT NULL AND par$abol_multiclientes.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$abol_multiclientes.first ..  par$abol_multiclientes.last LOOP 
              var$detalle_actual := '';
              IF par$abol_multiclientes(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$abol_multiclientes(' || idx || '):  ' || par$abol_multiclientes(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$abol_multiclientes(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_plan_bol_baja IS NOT NULL AND par$a_plan_bol_baja.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_plan_bol_baja.first ..  par$a_plan_bol_baja.last LOOP 
              var$detalle_actual := '';
              IF par$a_plan_bol_baja(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_plan_bol_baja(' || idx || '):  ' || par$a_plan_bol_baja(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_plan_bol_baja(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_plan_cod_planificacion IS NOT NULL AND par$a_plan_cod_planificacion.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_plan_cod_planificacion.first ..  par$a_plan_cod_planificacion.last LOOP 
              var$detalle_actual := '';
              IF par$a_plan_cod_planificacion(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_plan_cod_planificacion(' || idx || '):  ' || par$a_plan_cod_planificacion(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_plan_cod_planificacion(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_plan_fyh_inicio IS NOT NULL AND par$a_plan_fyh_inicio.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_plan_fyh_inicio.first ..  par$a_plan_fyh_inicio.last LOOP 
              var$detalle_actual := '';
              IF par$a_plan_fyh_inicio(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_plan_fyh_inicio(' || idx || '):  ' || par$a_plan_fyh_inicio(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_plan_fyh_inicio(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_plan_fyh_fin IS NOT NULL AND par$a_plan_fyh_fin.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_plan_fyh_fin.first ..  par$a_plan_fyh_fin.last LOOP 
              var$detalle_actual := '';
              IF par$a_plan_fyh_fin(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_plan_fyh_fin(' || idx || '):  ' || par$a_plan_fyh_fin(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_plan_fyh_fin(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_plan_nel_gmt_minuto IS NOT NULL AND par$a_plan_nel_gmt_minuto.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_plan_nel_gmt_minuto.first ..  par$a_plan_nel_gmt_minuto.last LOOP 
              var$detalle_actual := '';
              IF par$a_plan_nel_gmt_minuto(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_plan_nel_gmt_minuto(' || idx || '):  ' || par$a_plan_nel_gmt_minuto(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_plan_nel_gmt_minuto(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_puntos_cod_device_id IS NOT NULL AND par$a_puntos_cod_device_id.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_puntos_cod_device_id.first ..  par$a_puntos_cod_device_id.last LOOP 
              var$detalle_actual := '';
              IF par$a_puntos_cod_device_id(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_puntos_cod_device_id(' || idx || '):  ' || par$a_puntos_cod_device_id(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_puntos_cod_device_id(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_puntos_bol_baja IS NOT NULL AND par$a_puntos_bol_baja.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_puntos_bol_baja.first ..  par$a_puntos_bol_baja.last LOOP 
              var$detalle_actual := '';
              IF par$a_puntos_bol_baja(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_puntos_bol_baja(' || idx || '):  ' || par$a_puntos_bol_baja(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_puntos_bol_baja(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_puntos_cod_punto IS NOT NULL AND par$a_puntos_cod_punto.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_puntos_cod_punto.first ..  par$a_puntos_cod_punto.last LOOP 
              var$detalle_actual := '';
              IF par$a_puntos_cod_punto(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_puntos_cod_punto(' || idx || '):  ' || par$a_puntos_cod_punto(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_puntos_cod_punto(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_lim_cod_divisa IS NOT NULL AND par$a_lim_cod_divisa.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_lim_cod_divisa.first ..  par$a_lim_cod_divisa.last LOOP 
              var$detalle_actual := '';
              IF par$a_lim_cod_divisa(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_lim_cod_divisa(' || idx || '):  ' || par$a_lim_cod_divisa(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_lim_cod_divisa(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_lim_cod_pto IS NOT NULL AND par$a_lim_cod_pto.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_lim_cod_pto.first ..  par$a_lim_cod_pto.last LOOP 
              var$detalle_actual := '';
              IF par$a_lim_cod_pto(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_lim_cod_pto(' || idx || '):  ' || par$a_lim_cod_pto(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_lim_cod_pto(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_lim_num IS NOT NULL AND par$a_lim_num.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_lim_num.first ..  par$a_lim_num.last LOOP 
              var$detalle_actual := '';
              IF par$a_lim_num(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_lim_num(' || idx || '):  ' || par$a_lim_num(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_lim_num(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;
        IF par$a_lim_bol_baja IS NOT NULL AND par$a_lim_bol_baja.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$a_lim_bol_baja.first ..  par$a_lim_bol_baja.last LOOP 
              var$detalle_actual := '';
              IF par$a_lim_bol_baja(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$a_lim_bol_baja(' || idx || '):  ' || par$a_lim_bol_baja(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$a_lim_bol_baja(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;                  
        /* FIN LOG de arrays asociativos */
      EXCEPTION
          WHEN others THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Error de logueo de parametros iniciales: SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM,
                  par$cod_identificador   => '');
      END;
    END IF;
    /* FIN - LOG de parametros iniciales*/


    /* Log API*/       
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Antes de llamar procedure svalidar_pto_servicio',
                par$cod_identificador   => '');
    /* Validar puntos de servicios */
    svalidar_pto_servicio(par$oid_llamada             => par$oid_llamada,
                          par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                          par$a_puntos_cod_device_id  => par$a_puntos_cod_device_id,
                          par$a_puntos_bol_baja       => par$a_puntos_bol_baja,
                          par$a_puntos_cod_punto      => par$a_puntos_cod_punto,
                          par$cod_cultura             => par$cod_cultura); 

    FOR idx IN par$acod_device_id.first .. par$acod_device_id.last LOOP

      /* Validaciones */
      dbms_output.put_line('Validaciones');
      /* Log API*/       
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Antes de llamar procedure svalidar_codigos_configurar',
              par$cod_identificador   => par$acod_device_id(idx));

      svalidar_codigos_configurar(par$oid_llamada             => par$oid_llamada,
                                  par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                                  par$bol_alta                => par$abol_alta(idx),
                                  par$cod_device_id           => par$acod_device_id(idx),
                                  par$des_descripcion         => par$ades_descripcion(idx),
                                  par$cod_delegacion          => par$acod_delegacion(idx),
                                  par$cod_fabricante          => par$acod_fabricante(idx),
                                  par$cod_modelo              => par$acod_modelo(idx),
                                  par$cod_cultura             => par$cod_cultura,
                                  par$oid_modelo              => var$oid_modelo,
                                  par$oid_delegacion          => var$oid_delegacion,
                                  par$oid_planta              => var$oid_planta,
                                  par$oid_sector              => var$oid_sector,           
                                  par$oid_maquina             => var$oid_maquina);
      
      var$nel_gmt_minuto  := par$a_plan_nel_gmt_minuto(idx);
      var$plan_fyh_inicio := par$a_plan_fyh_inicio(idx);
      var$plan_fyh_fin    := par$a_plan_fyh_fin(idx);

      /* Validaciones Planificacion */
      dbms_output.put_line('Validaciones Planificacion');
      /* Log API*/       
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Antes de llamar procedure svalidar_planxmaquina',
                    par$cod_identificador   => par$acod_device_id(idx));

      svalidar_planxmaquina(par$oid_llamada       => par$oid_llamada,
                            par$oid_maquina       => var$oid_maquina,
                            par$cod_device_id     => par$acod_device_id(idx),   
                            par$bol_baja_plan     => par$a_plan_bol_baja(idx),
                            par$cod_planificacion => par$a_plan_cod_planificacion(idx),
                            par$oid_delegacion    => var$oid_delegacion,
                            par$cod_cultura       => par$cod_cultura,
                            par$oid_planificacion => var$oid_planificacion,
                            par$fyh_inicio        => var$plan_fyh_inicio,
                            par$fyh_fin           => var$plan_fyh_fin,
                            par$nel_gmt_minuto    => var$nel_gmt_minuto);
        
     IF NOT fhay_error(par$acod_device_id(idx)) THEN
      IF par$abol_alta(idx) = 1 THEN
        
        dbms_output.put_line('Modo Alta');
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Alta antes de llamar procedure supd_sector_maquina',
                par$cod_identificador   => par$acod_device_id(idx));

        supd_sector_maquina( var$oid_sector   =>var$oid_sector,
                            par$oid_maquina     => par$acod_device_id(idx),
                            par$des_sector      => par$ades_descripcion(idx),
                            par$oid_planta      => var$oid_planta,
                            par$bol_activo      => 1,
                            par$cod_usuario     => par$cod_usuario);

        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Alta antes de llamar procedure supd_maquina',
                par$cod_identificador   => par$acod_device_id(idx));

        supd_maquina(par$oid_maquina => var$oid_maquina,
                          par$oid_sector => var$oid_sector,
                          par$oid_modelo => var$oid_modelo,
                          par$oid_tipo_maquina => var$oid_tipo_maquina,
                          par$cod_identificacion => par$acod_device_id(idx),
                          par$bol_activo  => 1,
                          par$cod_usuario => par$cod_usuario,
                          par$bol_considera_recuentos  => par$abol_cons_recuentos(idx),
                          par$bol_multiclientes => par$abol_multiclientes(idx));
                 
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Alta antes de llamar procedure gepr_pcodigo_ajeno_###VERSION###.sconfigurar_ajeno',
                par$cod_identificador   => par$acod_device_id(idx));    
        /* Código Ajeno*/
        gepr_pcodigo_ajeno_###VERSION###.sconfigurar_ajeno(
                                          par$oid_tabla_genesis      => var$oid_maquina,
                                          par$cod_tabla_genesis      => 'SAPR_TMAQUINA',
                                          par$cod_identificador      => 'MAE',
                                          par$cod_ajeno              => par$acod_device_id(idx),
                                          par$des_ajeno              => par$ades_descripcion(idx),
                                          par$bol_defecto            => 1,
                                          par$cod_usuario            => par$cod_usuario);

        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Alta antes de llamar procedure sapr_pplanificacion_###VERSION###.supd_maquinaxplan',
                par$cod_identificador   => par$acod_device_id(idx));
                                 
        sapr_pplanificacion_###VERSION###.supd_maquinaxplan( par$oid_maquina          => var$oid_maquina,                                                                             
                               par$oid_planificacion   => var$oid_planificacion,
                               par$fyh_vigencia_inicio => var$plan_fyh_inicio,
                               par$fyh_vigencia_fin    => var$plan_fyh_fin,
                               par$cod_usuario         => par$cod_usuario);
                                     

        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Alta antes de llamar procedure srel_punto_maquina',
                par$cod_identificador   => par$acod_device_id(idx));

        srel_punto_maquina(par$oid_maquina => var$oid_maquina, 
                          par$cod_device_id =>   par$acod_device_id(idx),                   
                          par$cod_usuario => par$cod_usuario);
         

      ELSE
       
        dbms_output.put_line('Modo Modificacion');

        /* MODIFICAR MAQUINA */

              /* Sector */
              IF var$oid_planta is not null THEN
                    /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure supd_sector_maquina',
                          par$cod_identificador   => par$acod_device_id(idx));
                  supd_sector_maquina( var$oid_sector   =>var$oid_sector,
                                  par$oid_maquina     => par$acod_device_id(idx),
                                  par$des_sector      => par$ades_descripcion(idx),
                                  par$oid_planta      => var$oid_planta,
                                  par$bol_activo      => 1,
                                  par$cod_usuario     => par$cod_usuario);

              END IF;
                    /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure supd_maquina',
                          par$cod_identificador   => par$acod_device_id(idx));
                          
              supd_maquina(par$oid_maquina => var$oid_maquina,
                   par$oid_sector => var$oid_sector,
                   par$oid_modelo => var$oid_modelo,
                   par$oid_tipo_maquina => null,
                   par$cod_identificacion => null,
                   par$bol_activo  => 1,
                   par$cod_usuario => par$cod_usuario,
                   par$bol_considera_recuentos  => par$abol_cons_recuentos(idx),
                   par$bol_multiclientes => par$abol_multiclientes(idx));


              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'Modificar antes de llamar procedure gepr_pcodigo_ajeno_###VERSION###.sconfigurar_ajeno',
                      par$cod_identificador   => par$acod_device_id(idx));    
              /* Código Ajeno*/
              gepr_pcodigo_ajeno_###VERSION###.sconfigurar_ajeno(
                                                par$oid_tabla_genesis      => var$oid_maquina,
                                                par$cod_tabla_genesis      => 'SAPR_TMAQUINA',
                                                par$cod_identificador      => 'MAE',
                                                par$cod_ajeno              => par$acod_device_id(idx),
                                                par$des_ajeno              => par$ades_descripcion(idx),
                                                par$bol_defecto            => 1,
                                                par$cod_usuario            => par$cod_usuario);

              /* Planificacion */
              IF par$a_plan_bol_baja(idx) IS NOT NULL THEN

                  /* Log API*/       
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'Modificar, parametro par$a_plan_bol_baja(' || idx || '):' || par$a_plan_bol_baja(idx),
                        par$cod_identificador   => par$acod_device_id(idx));

                  IF par$a_plan_bol_baja(idx) = 0 THEN
                          
                    /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure sdel_tplanxmaquina',
                          par$cod_identificador   => par$acod_device_id(idx));

                      sdel_tplanxmaquina(par$oid_maquina => var$oid_maquina,
                                  par$cod_usuario => par$cod_usuario);
                              
                    /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure sdel_tperiodos',
                          par$cod_identificador   => par$acod_device_id(idx));

                      sdel_tperiodos(par$oid_maquina =>var$oid_maquina  );         
                      
                    /* Relacionar maquina */
                    /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure sapr_pplanificacion_###VERSION###.supd_maquinaxplan',
                          par$cod_identificador   => par$acod_device_id(idx));

                      sapr_pplanificacion_###VERSION###.supd_maquinaxplan( par$oid_maquina          => var$oid_maquina,
                                  par$oid_planificacion   => var$oid_planificacion,
                                  par$fyh_vigencia_inicio => var$plan_fyh_inicio,
                                  par$fyh_vigencia_fin    => var$plan_fyh_fin,
                                  par$cod_usuario         => par$cod_usuario);
                                  
                    /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure sapr_pplanificacion_###VERSION###.sdel_planxcan_maqu',
                          par$cod_identificador   => par$acod_device_id(idx));   

                      SAPR_PPLANIFICACION_###VERSION###.sdel_planxcan_maqu(par$oid_plan_nueva   => var$oid_planificacion,
                                par$oid_maquina     => var$oid_maquina,
                                par$cod_usuario     => par$cod_usuario);  
                  ELSE
                    /* Log API*/
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure sapr_pplanificacion_###VERSION###.sdel_planxcan_maqu',
                          par$cod_identificador   => par$acod_device_id(idx));

                    SAPR_PPLANIFICACION_###VERSION###.sdel_planxcan_maqu(par$oid_plan_nueva   => null,
                             par$oid_maquina     => var$oid_maquina,
                             par$cod_usuario     => par$cod_usuario);  
                    
                    /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure sdel_tplanxmaquina',
                          par$cod_identificador   => par$acod_device_id(idx));

                    sdel_tplanxmaquina(par$oid_maquina => var$oid_maquina,
                               par$cod_usuario => par$cod_usuario);

                    /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Modificar antes de llamar procedure sdel_tperiodos',
                          par$cod_identificador   => par$acod_device_id(idx));

                    sdel_tperiodos(par$oid_maquina =>var$oid_maquina  ); 
                  END IF;
              END IF;
             
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Modificar antes de llamar procedure srel_punto_maquina',
                    par$cod_identificador   => par$acod_device_id(idx));          
              
              srel_punto_maquina(par$oid_maquina => var$oid_maquina, 
                          par$cod_device_id =>   par$acod_device_id(idx),                   
                          par$cod_usuario => par$cod_usuario);


                       
         END IF;
         
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR) 
             VALUES ('0040070000',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040070000', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                  par$acod_device_id(idx), 'VALIDACIONES');      

      END IF;
      
      
      
      

      /* GUARDANDO INFORMACIONES DE LAS MAQUINAS */
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR) VALUES (par$acod_device_id(idx), var$oid_maquina, par$abol_alta(idx), 'OID_MAQUINA');

        /*Proceso primero las bajas*/
        /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Proceso de Limites de MAE',
                    par$cod_identificador   => par$acod_device_id(idx)); 

        /*Limites Baja*/
        IF  par$a_lim_cod_divisa IS NOT NULL AND  par$a_lim_cod_divisa.COUNT > 0 THEN
          FOR idxDiv IN par$a_lim_cod_divisa.first .. par$a_lim_cod_divisa.last LOOP
            IF par$a_lim_bol_baja(idxDiv) = 1 THEN
              /*valido el limite*/
              /* Log API*/      
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Se valida límite con código de divisa: ' || par$a_lim_cod_divisa(idxDiv),
                  par$cod_identificador   => par$acod_device_id(idx)); 

              SAPR_PLIMITE_###VERSION###.svalidar_limite(par$cod_identificador_ajeno, par$a_lim_cod_divisa(idxDiv), par$a_lim_cod_pto(idxDiv), par$a_lim_num(idxDiv), par$cod_cultura);
              /*Obtengo el oid divisa del campo actual*/
              BEGIN
                SELECT OID_CAMPO1
                INTO var$oid_divisa
                FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'OID_DIVISA'
                AND COD_CAMPO2 = par$a_lim_cod_divisa(idxDiv) AND ROWNUM = 1;
              EXCEPTION WHEN no_data_found THEN
                        var$oid_divisa := NULL;
              END;

              /*Obtengo el oid pto servicio del campo actual*/
              BEGIN
                SELECT OID_CAMPO1
                INTO var$oid_pto_servicio
                FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                AND COD_CAMPO2 = par$a_lim_cod_pto(idxDiv) AND ROWNUM = 1;
              EXCEPTION WHEN no_data_found THEN
                        var$oid_pto_servicio := NULL;
              END;

              /*Obtengo el oid limite del campo actual*/
              BEGIN
                SELECT OID_LIMITE 
                INTO var$oid_limite
                FROM SAPR_TLIMITE 
                WHERE OID_MAQUINA = var$oid_maquina AND OID_DIVISA = var$oid_divisa AND BOL_ACTIVO = 1;
              EXCEPTION WHEN no_data_found THEN
                        var$oid_limite := NULL;
              END;
              /*verifico si hubo error*/
              SELECT COUNT(1)
              INTO var$hayerror
              FROM SAPR_GTT_TAUXILIAR
              WHERE COD_CALIFICADOR = 'VALIDACIONES' AND
              COD_CAMPO2 = 'LIMITE'
              AND COD_CAMPO3 IN (par$a_lim_cod_divisa(idxDiv),  par$a_lim_num(idxDiv));

              IF var$hayerror = 0 THEN
                /* Log API*/   
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Se actualiza Baja de límite con código de divisa: ' || par$a_lim_cod_divisa(idxDiv),
                  par$cod_identificador   => par$acod_device_id(idx)); 

                /*Si no hay error actualizo la tabla limite*/
                SAPR_PLIMITE_###VERSION###.supd_limite(var$oid_limite, NULL, var$oid_maquina, var$oid_pto_servicio, var$oid_divisa, par$a_lim_num(idx), 0, par$cod_usuario);
              END IF;
            END IF;

          END LOOP;
        END IF;
        /*Limites Alta*/
        IF  par$a_lim_cod_divisa IS NOT NULL AND  par$a_lim_cod_divisa.COUNT > 0 THEN
          FOR idxDiv IN par$a_lim_cod_divisa.first .. par$a_lim_cod_divisa.last LOOP
          
            IF par$a_lim_bol_baja(idxDiv) = 0 THEN            
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Proceso de Alta de límite con código de divisa: ' || par$a_lim_cod_divisa(idxDiv),
                    par$cod_identificador   => par$acod_device_id(idx));               

              SAPR_PLIMITE_###VERSION###.svalidar_limite(par$cod_identificador_ajeno, par$a_lim_cod_divisa(idxDiv), par$a_lim_cod_pto(idxDiv), par$a_lim_num(idxDiv), par$cod_cultura);
              /*Obtengo el oid divisa del campo actual*/
              BEGIN
                SELECT OID_CAMPO1
                INTO var$oid_divisa
                FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'OID_DIVISA'
                AND COD_CAMPO2 = par$a_lim_cod_divisa(idxDiv) AND ROWNUM = 1;
              EXCEPTION WHEN no_data_found THEN
                        var$oid_divisa := NULL;
              END;

              /*Obtengo el oid pto servicio del campo actual*/
              BEGIN
                SELECT OID_CAMPO1
                INTO var$oid_pto_servicio
                FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                AND COD_CAMPO2 = par$a_lim_cod_pto(idxDiv) AND ROWNUM = 1;
              EXCEPTION WHEN no_data_found THEN
                        var$oid_pto_servicio := NULL;
              END;

              /*Obtengo el oid limite del campo actual*/
              BEGIN
                SELECT OID_LIMITE 
                INTO var$oid_limite
                FROM SAPR_TLIMITE 
                WHERE OID_MAQUINA = var$oid_maquina AND OID_DIVISA = var$oid_divisa AND BOL_ACTIVO = 1;
              EXCEPTION WHEN no_data_found THEN
                        var$oid_limite := NULL;
              END;
              /*verifico si hubo error*/
              SELECT COUNT(1)
              INTO var$hayerror
              FROM SAPR_GTT_TAUXILIAR
              WHERE COD_CALIFICADOR = 'VALIDACIONES' AND
              COD_CAMPO2 = 'LIMITE'
              AND COD_CAMPO3 IN (par$a_lim_cod_divisa(idxDiv),  par$a_lim_num(idxDiv));

              IF var$hayerror = 0 THEN
                /* Log API*/   
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Se actualiza Alta de límite con código de divisa: ' || par$a_lim_cod_divisa(idxDiv),
                  par$cod_identificador   => par$acod_device_id(idx));

                /*Si no hay error actualizo la tabla limite*/
                SAPR_PLIMITE_###VERSION###.supd_limite(var$oid_limite, NULL, var$oid_maquina, var$oid_pto_servicio, var$oid_divisa, par$a_lim_num(idxDiv), 1, par$cod_usuario);
              END IF;
            END IF;
          END LOOP;
        END IF;
    END LOOP;
  
    

    /* Validaciones */
    OPEN par$rc_validaciones FOR
         SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CAMPO3, COD_CAMPO4 AS DEVICE_ID
           FROM SAPR_GTT_TAUXILIAR
          WHERE COD_CALIFICADOR = 'VALIDACIONES';
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
     commit;

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

  END sconfigurar_maquinas;


/*

 PROCEDURE srelacionar_maquina( par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$plan_fyh_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$plan_fyh_fin IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$usr_) IS


   var$existe               gepr_pcomon_###VERSION###.tipo$cod_;

   begin

       IF par$oid_planificacion IS NOT NULL  THEN 


           sapr_pplanificacion_###VERSION###.supd_maquinaxplan( par$oid_maquina          => par$oid_maquina,
                               par$oid_planificacion   => par$oid_planificacion,
                               par$fyh_vigencia_inicio => par$plan_fyh_inicio,
                               par$fyh_vigencia_fin    => par$plan_fyh_fin,
                               par$cod_usuario         => par$cod_usuario);
      ELSE
                         dbms_output.put_line('Erro ao validar petição');
      END IF;
end srelacionar_maquina;
*/

 PROCEDURE supd_sector_maquina( var$oid_sector      in out  gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_maquina     in gepr_pcomon_###VERSION###.tipo$oid_,
                                par$des_sector      in gepr_pcomon_###VERSION###.tipo$desc_,
                                par$oid_planta      in gepr_pcomon_###VERSION###.tipo$oid_,
                                par$bol_activo      in gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$cod_usuario     in gepr_pcomon_###VERSION###.tipo$usr_) IS

    var$oid_tipo_sector gepr_pcomon_###VERSION###.tipo$oid_;

  BEGIN

    SELECT TSETOR.OID_TIPO_SECTOR into var$oid_tipo_sector
      FROM GEPR_TTIPO_SECTOR TSETOR
      WHERE TSETOR.COD_TIPO_SECTOR = gepr_pcomon_###VERSION###.const$CodTipoSectorMae
      AND  TSETOR.BOL_ACTIVO = 1;


    supd_sector(par$oid_sector                  => var$oid_sector,
                par$oid_sector_padre            => null,
                par$oid_tipo_sector             => var$oid_tipo_sector ,
                par$oid_planta                  => par$oid_planta,
                par$cod_sector                  => par$oid_maquina,
                par$des_sector                  => par$des_sector,
                par$cod_migracion               => null,
                par$bol_permite_disponer_valor  => 0,
                par$bol_centro_proceso          => 0,
                par$bol_activo                  => par$bol_activo,
                par$cod_usuario                 => par$cod_usuario,
                par$bol_conteo                  => 0,
                par$bol_tesoro                  => 0);

   
    gepr_pcodigo_ajeno_###VERSION###.sconfigurar_ajeno(par$oid_tabla_genesis => var$oid_sector,
                                              par$cod_tabla_genesis => gepr_pcomon_###VERSION###.const$nomeTableSector,
                                              par$cod_identificador => gepr_pcomon_###VERSION###.const$identificadorAjenoMAE,
                                              par$cod_ajeno => par$oid_maquina,
                                              par$des_ajeno => par$des_sector,
                                              par$bol_defecto => 0,
                                              par$cod_usuario => par$cod_usuario);
                                                          



  END supd_sector_maquina;


  PROCEDURE supd_sector( par$oid_sector                  in out gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_sector_padre            in gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_tipo_sector             in gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_planta                  in gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_sector                  in gepr_pcomon_###VERSION###.tipo$cod_,
                          par$des_sector                  in gepr_pcomon_###VERSION###.tipo$desc_,
                          par$cod_migracion               in gepr_pcomon_###VERSION###.tipo$cod_,
                          par$bol_permite_disponer_valor  in gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$bol_centro_proceso          in gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$bol_activo                  in gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$cod_usuario                 in gepr_pcomon_###VERSION###.tipo$usr_,
                          par$bol_conteo                  in gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$bol_tesoro                  in gepr_pcomon_###VERSION###.tipo$nbol_) IS

   var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_ ;
  BEGIN


     BEGIN
       
                 dbms_output.put_line('par$oid_sector' );
        IF par$oid_sector IS NULL THEN


        par$oid_sector  := sys_guid();
        INSERT INTO GEPR_TSECTOR (
                    OID_SECTOR,
                    OID_SECTOR_PADRE,
                    OID_TIPO_SECTOR,
                    OID_PLANTA,
                    COD_SECTOR,
                    DES_SECTOR,
                    COD_MIGRACION,
                    BOL_PERMITE_DISPONER_VALOR,
                    BOL_CENTRO_PROCESO,
                    BOL_ACTIVO,
                    GMT_CREACION,
                    DES_USUARIO_CREACION,
                    GMT_MODIFICACION,
                    DES_USUARIO_MODIFICACION,
                    BOL_CONTEO,
                    BOL_TESORO
                  )
                  VALUES
                  ( par$oid_sector,
                    par$oid_sector_padre,
                    par$oid_tipo_sector,
                    par$oid_planta,
                    par$cod_sector,
                    par$des_sector,
                    par$cod_migracion,
                    par$bol_permite_disponer_valor,
                    par$bol_centro_proceso,
                    par$bol_activo,
                    CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE),
                    par$cod_usuario,
                    CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE),
                    par$cod_usuario,
                    par$bol_conteo,
                    par$bol_tesoro);
        ELSE



           IF  par$des_sector IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ DES_SECTOR = ']' || par$des_sector ||q'[', ]';
           END IF;

           IF  par$oid_planta IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_PLANTA = ']' || par$oid_planta ||q'[', ]';
           END IF;

           IF par$bol_activo IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_ACTIVO = ']' || par$bol_activo ||q'[', ]';
           END IF;

           IF  var$qry_update IS NOT NULL  THEN
             var$qry_update := q'[ UPDATE GEPR_TSECTOR SET ]' || var$qry_update ||q'[
             GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2
             WHERE OID_SECTOR = :3]';
             
             
             EXECUTE IMMEDIATE var$qry_update
              USING GEPR_PUTILIDADES_###VERSION###.fgmt_zero, par$cod_usuario, par$oid_sector;
           END IF;
        END IF;


        EXCEPTION
          WHEN no_data_found THEN

            dbms_output.put_line('1');
           /* par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_ajeno',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            '',
                                                                                            1);  */
       END;

  END supd_sector;
  

  PROCEDURE supd_maquina(par$oid_maquina                 IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_sector                  IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_modelo                  IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_tipo_maquina            IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_identificacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$bol_activo                  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$cod_usuario    IN gepr_pcomon_###VERSION###.tipo$desc_,
                          par$bol_considera_recuentos     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                          par$bol_multiclientes     IN gepr_pcomon_###VERSION###.tipo$nbol_) IS

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_ ;

  BEGIN

     BEGIN
        IF par$oid_maquina IS NULL THEN
                 par$oid_maquina  := sys_guid();
                 dbms_output.put_line('teste');
                 dbms_output.put_line('par$oid_maquina' || par$oid_maquina);
                 dbms_output.put_line('par$oid_sector' || par$oid_sector);
                 dbms_output.put_line('par$oid_modelo' || par$oid_modelo);
                 dbms_output.put_line('par$oid_tipo_maquina' || par$oid_tipo_maquina);
                 dbms_output.put_line('par$cod_identificacion' || par$cod_identificacion);
                 dbms_output.put_line('par$bol_activo' || par$bol_activo);
                 dbms_output.put_line('par$cod_usuario' || par$cod_usuario);
                 dbms_output.put_line('var$gmt_zero' || var$gmt_zero);
                 dbms_output.put_line('par$bol_considera_recuentos' || par$bol_considera_recuentos);
                 
                 
              INSERT INTO SAPR_TMAQUINA
                    (OID_MAQUINA,
                     OID_SECTOR,
                     OID_MODELO,
                     OID_TIPO_MAQUINA,
                     COD_IDENTIFICACION,
                     BOL_ACTIVO,
                     GMT_CREACION,
                     DES_USUARIO_CREACION,
                     GMT_MODIFICACION,
                     DES_USUARIO_MODIFICACION,
                     BOL_CONSIDERA_RECUENTOS,
                     BOL_MULTICLIENTE)
                    VALUES
                    (par$oid_maquina,
                     par$oid_sector,
                     par$oid_modelo,
                     par$oid_tipo_maquina,
                     par$cod_identificacion,
                     par$bol_activo,
                     var$gmt_zero,
                     par$cod_usuario,
                     CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE),
                     par$cod_usuario,
                     par$bol_considera_recuentos,
                     par$bol_multiclientes);
        ELSE


           IF  par$oid_modelo IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_MODELO = ']' || par$oid_modelo ||q'[', ]';
           END IF;

           IF par$bol_considera_recuentos IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_CONSIDERA_RECUENTOS = ']' || par$bol_considera_recuentos ||q'[', ]';
           END IF;

           IF par$bol_multiclientes IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_MULTICLIENTE = ']' || par$bol_multiclientes ||q'[', ]';
           END IF;

           IF par$bol_activo IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_ACTIVO = ']' || par$bol_activo ||q'[', ]';
           END IF;



           IF  var$qry_update IS NOT NULL  THEN
             EXECUTE IMMEDIATE q'[ UPDATE SAPR_TMAQUINA SET ]' || var$qry_update ||q'[
             GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2
             WHERE OID_MAQUINA = :3]'
              USING var$gmt_zero, par$cod_usuario, par$oid_maquina;
           END IF;



        END IF;

        EXCEPTION
          WHEN no_data_found THEN
                dbms_output.put_line('1');
           /* par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_ajeno',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            '',
                                                                                            1);    */
       END;

  END supd_maquina;



  /* Validar informaciones configurar maquina */
  PROCEDURE svalidar_codigos_configurar(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$bol_alta                IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$cod_device_id           IN gepr_pcomon_###VERSION###.tipo$cod_,   
                                        par$des_descripcion         IN gepr_pcomon_###VERSION###.tipo$desc_,  
                                        par$cod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_fabricante          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_modelo              IN gepr_pcomon_###VERSION###.tipo$cod_,            
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_modelo              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_delegacion          OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_maquina             OUT gepr_pcomon_###VERSION###.tipo$oid_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_codigos_configurar';
    var$cod_delegacion     gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_fabricante     gepr_pcomon_###VERSION###.tipo$oid_;

  BEGIN

    IF par$cod_device_id IS NULL THEN
      /* Log API*/       
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'El cod_device_id es nulo',
                    par$cod_identificador   => '');

      /* 2040070001 - Es obligatorio informar el DeviceID. */
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
        VALUES ('2040070001',
                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070001', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                par$cod_device_id, 'VALIDACIONES');

    ELSE

      
      /* OBS.: recupera el identificador del sector */
      BEGIN
        SELECT OID_MAQUINA, OID_SECTOR
          INTO par$oid_maquina, par$oid_sector
          FROM SAPR_TMAQUINA M
         WHERE COD_IDENTIFICACION = par$cod_device_id;
      EXCEPTION WHEN no_data_found THEN
        par$oid_maquina := NULL;
        par$oid_sector := NULL;
      END;
      IF par$oid_maquina IS NOT NULL AND par$bol_alta = 1 THEN
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,                            
          par$des_detalle     => 'La máquina no fue creada, ya existe una máquina con el DeviceID: ' || par$cod_device_id,
          par$cod_identificador   => par$cod_device_id);

        /* 2040070002 - La maquina no fue creada, pues ya existe una maquina con el DeviceID informado. */
        dbms_output.put_line('La maquina no fue creada, pues ya existe una maquina con el DeviceID informado.');
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
          VALUES ('2040070002',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070002', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                  par$cod_device_id, 'VALIDACIONES');              
      END IF;

      /* 2040070018 - No existe una maquina con el DeviceID [{0}] configurada en Génesis Producto. */
      IF par$oid_maquina IS NULL AND par$bol_alta = 0 THEN
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => 'No se puede modificar/dar de baja la máquina, no existe una máquina con el DeviceID: ' || par$cod_device_id,
            par$cod_identificador   => par$cod_device_id);

        dbms_output.put_line('No existe una maquina con el DeviceID [{0}] configurada en Génesis Producto.');
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
          VALUES ('2040070018',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070018', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_device_id, 0),
                  par$cod_device_id, 'VALIDACIONES');              
      END IF;
      
      /* 2040070003 - Es obligatorio informar una descripcion. */
      IF  par$des_descripcion IS NULL AND par$bol_alta = 1 THEN
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Es obligatorio informar una descripción',
          par$cod_identificador   => par$cod_device_id);

        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
          VALUES ('2040070003',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070003', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                  par$cod_device_id, 'VALIDACIONES');  
      END IF;

      /* Delegacion */
      IF  par$cod_delegacion IS NULL AND par$bol_alta = 1 THEN       
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Es obligatorio informar una delegación',
          par$cod_identificador   => par$cod_device_id);

        /* 2040070004 - Es obligatorio informar una delegacion. */
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
          VALUES ('2040070004',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070004', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                  par$cod_device_id, 'VALIDACIONES');

      ELSIF par$cod_delegacion IS NOT NULL THEN

        IF par$cod_identificador_ajeno IS NULL THEN
          BEGIN
             SELECT DELE.OID_DELEGACION, DELE.COD_DELEGACION
              INTO par$oid_delegacion, var$cod_delegacion
              FROM GEPR_TDELEGACION DELE
             WHERE DELE.COD_DELEGACION = par$cod_delegacion
               AND DELE.BOL_VIGENTE = 1;
          EXCEPTION WHEN no_data_found THEN
            par$oid_delegacion := NULL;
          END;
          IF par$oid_delegacion IS NULL THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No existe la delegacion '|| par$cod_delegacion ||' configurada en Genesis Producto.',
                par$cod_identificador   => par$cod_device_id);

            /* 2040070005 - No existe la delegacion [{0}] configurada en Genesis Producto. */
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
              VALUES ('2040070005',
                      gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070005', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_delegacion, 0),
                      par$cod_device_id, 'VALIDACIONES');              
          END IF;

        ELSE

          
          BEGIN
            SELECT CA.OID_TABLA_GENESIS, DELE.COD_DELEGACION
              INTO par$oid_delegacion, var$cod_delegacion
              FROM GEPR_TCODIGO_AJENO CA
              INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = CA.OID_TABLA_GENESIS AND DELE.BOL_VIGENTE = 1
             WHERE CA.COD_AJENO = par$cod_delegacion
               AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
               AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
               AND CA.BOL_ACTIVO = 1;
          EXCEPTION WHEN no_data_found THEN
            par$oid_delegacion := NULL;
          END;
          IF par$oid_delegacion IS NULL THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No fue posible encontrar una delegación, utilizando el codigo ' || par$cod_delegacion || ' y el codigo ajeno '|| par$cod_identificador_ajeno,
                par$cod_identificador   => par$cod_device_id);

            /* 2040070006 - No fue posible identificar una delegación en Genesis Producto, utilizando el identificador [{0}] y el codigo ajeno [{0}]. */
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
              VALUES ('2040070006',
                      gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070006', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_identificador_ajeno || '|' || par$cod_delegacion, 0),
                      par$cod_device_id, 'VALIDACIONES');              
          END IF;

        END IF;
        
        /* Planta */
        IF par$oid_delegacion IS NOT NULL THEN
          BEGIN
            SELECT PLAN.OID_PLANTA
              INTO par$oid_planta
            FROM GEPR_TPLANTA PLAN
            LEFT JOIN GEPR_TCODIGO_AJENO AJENO ON AJENO.OID_TABLA_GENESIS = PLAN.OID_PLANTA 
                                                AND AJENO.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA' 
                                                AND AJENO.BOL_ACTIVO = 1
                                                AND AJENO.COD_IDENTIFICADOR = par$cod_identificador_ajeno
            WHERE PLAN.OID_DELEGACION = par$oid_delegacion
              AND (CASE WHEN par$cod_identificador_ajeno IS NULL THEN PLAN.COD_PLANTA ELSE AJENO.COD_AJENO END ) = par$cod_delegacion
              AND PLAN.BOL_ACTIVO = 1;
          EXCEPTION WHEN no_data_found THEN
            par$oid_planta := NULL;
          END;
          IF par$oid_planta IS NULL THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No existe la planta '|| par$cod_delegacion || ' configurada en Génesis Producto',
                par$cod_identificador   => par$cod_device_id);
            /* 2040070007 - No existe la planta [{0}] configurada en Génesis Producto. */
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
              VALUES ('2040070007',
                      gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070007', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_delegacion, 0),
                      par$cod_device_id, 'VALIDACIONES');              
          END IF;
          
          IF par$oid_planta IS NOT NULL THEN
            /* Recuperar identificador Sector - en el escenario de ALTA */
            BEGIN
              SELECT OID_SECTOR
                INTO par$oid_sector
                FROM GEPR_TSECTOR
               WHERE COD_SECTOR = par$cod_device_id
                 AND OID_PLANTA = par$oid_planta
                 --AND BOL_ACTIVO = 1
                ;
            EXCEPTION WHEN no_data_found THEN
              par$oid_sector := NULL;
            END;

            
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Se recupera identificador Sector: ' || par$oid_sector,
                  par$cod_identificador   => par$cod_device_id);
          END IF;
        END IF;
      END IF;
      
      /* Si la accion es MODIFICAR y estoy cambinado la descripcion. Tengo que recupear el identificador de la planta
         mismo que no fue informado un codigo de delegación */
      IF par$oid_planta IS NULL AND par$bol_alta = 0 AND par$des_descripcion IS NOT NULL AND par$oid_maquina IS NOT NULL THEN
           
          BEGIN
            SELECT SECT.OID_PLANTA, SECT.OID_SECTOR
              INTO par$oid_planta, par$oid_sector
              FROM GEPR_TSECTOR SECT
              INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = SECT.OID_SECTOR
             WHERE MAQU.OID_MAQUINA = par$oid_maquina;
          EXCEPTION WHEN no_data_found THEN
            par$oid_planta := NULL;
          END;
            
          /* Log API*/       
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Si la acción es MODIFICAR, se recupera identificador de sector: ' || par$oid_sector || ' y planta: ' || par$oid_planta,
                par$cod_identificador   => par$cod_device_id);
      END IF;

      /* Fabricantes */
      IF  par$cod_fabricante IS NULL AND par$bol_alta = 1 THEN       
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Es obligatorio informar un código de fabricante',
                par$cod_identificador   => par$cod_device_id);

        /* 2040070008 - Es obligatorio informar un fabricante. */
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
          VALUES ('2040070008',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070008', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                  par$cod_device_id, 'VALIDACIONES');

      ELSIF  par$cod_fabricante IS NOT NULL THEN
        BEGIN
           SELECT OID_FABRICANTE
             INTO var$oid_fabricante
             FROM SAPR_TFABRICANTE
            WHERE COD_FABRICANTE = par$cod_fabricante
              AND BOL_ACTIVO = 1;
        EXCEPTION
          WHEN no_data_found THEN
            var$oid_fabricante := NULL;
        END;
        IF var$oid_fabricante IS NULL THEN
          /* Log API*/       
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No existe el fabricante de código: ' || par$cod_fabricante,
                par$cod_identificador   => par$cod_device_id);

          /* 2040070009 - No existe el fabricante [{0}] configurado en Genesis Producto. */
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
            VALUES ('2040070009',
                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070009', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_fabricante, 0),
                    par$cod_device_id, 'VALIDACIONES');
        ELSE

          /* Modelo */
          IF par$cod_modelo IS NULL AND par$bol_alta = 1 THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Es obligatorio informar un modelo',
                par$cod_identificador   => par$cod_device_id);

            /* 2040070010 - Es obligatorio informar un modelo. */
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
              VALUES ('2040070010',
                      gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070010', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                      par$cod_device_id, 'VALIDACIONES');

          ELSIF par$cod_modelo IS NOT NULL THEN
            BEGIN
               SELECT OID_MODELO
                 INTO par$oid_modelo
                 FROM SAPR_TMODELO
                WHERE OID_FABRICANTE = var$oid_fabricante 
                  AND COD_MODELO = par$cod_modelo
                  AND BOL_ACTIVO = 1;
            EXCEPTION
              WHEN no_data_found THEN
                par$oid_modelo := NULL;
            END;
            IF par$oid_modelo IS NULL THEN
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No existe el modelo de código: ' || par$cod_modelo,
                par$cod_identificador   => par$cod_device_id);

              /* 2040070011 - No existe el modelo [{0}] configurado en Genesis Producto. */
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES ('2040070011',
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070011', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_modelo, 0),
                        par$cod_device_id, 'VALIDACIONES');
            END IF;
          END IF;
          
        END IF;
      END IF;

    END IF;

  END svalidar_codigos_configurar;

  /* Validar informaciones del relacionamiento maquina x planificacion */
  PROCEDURE svalidar_planxmaquina(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_device_id     IN gepr_pcomon_###VERSION###.tipo$cod_,   
                                  par$bol_baja_plan     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$cod_planificacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_delegacion    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_planificacion IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$fyh_inicio        IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$fyh_fin           IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$nel_gmt_minuto    IN OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_planxmaquina';
    var$fyh_inicio_plan  gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fyh_fin_plan     gepr_pcomon_###VERSION###.tipo$fyh_;
    var$bol_fecha_valida gepr_pcomon_###VERSION###.tipo$nbol_;
    var$existe           gepr_pcomon_###VERSION###.tipo$nel_;

  BEGIN

    IF NOT fhay_error(par$cod_device_id => par$cod_device_id) AND par$cod_planificacion IS NOT NULL THEN


      /* Validar GMT */
      IF par$fyh_inicio IS NOT NULL OR par$fyh_fin IS NOT NULL THEN
        
        IF par$nel_gmt_minuto IS NOT NULL THEN
          /* Si veo la informacion en la fecha (peticion) */
          IF par$fyh_inicio IS NOT NULL THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se inicia el cálculo GMT de fecha inicio: ' || par$fyh_inicio || ' con parámetro par$nel_gmt_minuto : ' || par$nel_gmt_minuto,
                    par$cod_identificador   => par$cod_device_id);
            
            par$fyh_inicio  := gepr_putilidades_###VERSION###.fsomar_fecha(par$fyh_inicio, par$nel_gmt_minuto * -1);
            
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se finaliza cálculo fecha inicio en GMT con resultado: ' || par$fyh_inicio || ' con parámetro par$nel_gmt_minuto : ' || par$nel_gmt_minuto,
                    par$cod_identificador   => par$cod_device_id);
          END IF;   
         
          IF par$fyh_fin IS NOT NULL THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se inicia el cálculo GMT de fecha fin: ' || par$fyh_fin || ' con parámetro par$nel_gmt_minuto : ' || par$nel_gmt_minuto,
                    par$cod_identificador   => par$cod_device_id);

            par$fyh_fin := gepr_putilidades_###VERSION###.fsomar_fecha(par$fyh_fin, par$nel_gmt_minuto * -1);

            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se finaliza cálculo fecha fin en GMT con resultado: ' || par$fyh_fin || ' con parámetro par$nel_gmt_minuto : ' || par$nel_gmt_minuto,
                    par$cod_identificador   => par$cod_device_id);
            
          END IF;
        ELSE
          /* Si no veo el GMT en la peticion, pero necesito de la delegacion para calcular la fecha */
          IF par$oid_delegacion IS NOT NULL THEN
            IF par$fyh_inicio IS NOT NULL THEN
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se inicia el cálculo GMT de fecha inicio: ' || par$fyh_inicio || ' de la delegación con oid: ' || par$oid_delegacion,
                    par$cod_identificador   => par$cod_device_id);

              par$fyh_inicio  := fn_gmt_delegacion_###VERSION###(par$oid_delegacion => par$oid_delegacion,
                                                        par$fyh_gestion => par$fyh_inicio);

              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se finaliza cálculo fecha inicio en GMT con resultado: ' || par$fyh_inicio || ' de la delegación con oid: ' || par$oid_delegacion,
                    par$cod_identificador   => par$cod_device_id);
            END IF;   
           
            IF par$fyh_fin IS NOT NULL THEN
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se inicia el cálculo GMT de fecha fin: ' || par$fyh_fin || ' de la delegación con oid: ' || par$oid_delegacion,
                    par$cod_identificador   => par$cod_device_id);

              par$fyh_fin  := fn_gmt_delegacion_###VERSION###(par$oid_delegacion => par$oid_delegacion,
                                                        par$fyh_gestion => par$fyh_fin);

              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se finaliza cálculo fecha fin en GMT con resultado: ' || par$fyh_fin || ' de la delegación con oid: ' || par$oid_delegacion,
                    par$cod_identificador   => par$cod_device_id);
            END IF;
          
          ELSE
            
            IF par$fyh_inicio IS NOT NULL THEN
              /* 2040070023 - No fue posible calcular el GMT de la fecha "{0}". Por favor, indique el GMT o el código de la delegación. */
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'No fue posible calcular el GMT de la fecha inicio ' || par$fyh_inicio || ' se debe indicar el GMT o el código de la delegación',
                    par$cod_identificador   => par$cod_device_id);

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES ('2040070023',
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070023', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$fyh_inicio, 0),
                        par$cod_device_id, 'VALIDACIONES');
            END IF;   
           
            IF par$fyh_fin IS NOT NULL THEN
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'No fue posible calcular el GMT de la fecha fin ' || par$fyh_fin || ' se debe indicar el GMT o el código de la delegación',
                    par$cod_identificador   => par$cod_device_id);

              /* 2040070023 - No fue posible calcular el GMT de la fecha "{0}". Por favor, indique el GMT o el código de la delegación. */
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES ('2040070023',
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070023', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$fyh_fin, 0),
                        par$cod_device_id, 'VALIDACIONES');
            END IF;
          END IF;
        END IF;
      END IF;        
  
      /* 2040070012 - No existe la planificación [{0}] configurada en Génesis Producto. */
      BEGIN
        
        SELECT OID_PLANIFICACION, fyh_vigencia_inicio, fyh_vigencia_fin
          INTO par$oid_planificacion, var$fyh_inicio_plan, var$fyh_fin_plan
          FROM SAPR_TPLANIFICACION
         WHERE COD_PLANIFICACION = par$cod_planificacion 
           AND BOL_ACTIVO = 1;
         
      EXCEPTION WHEN no_data_found THEN
        par$oid_planificacion := NULL;
        var$fyh_inicio_plan := NULL;
        var$fyh_fin_plan := NULL;
      END;
      IF par$oid_planificacion IS NULL THEN
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'No existe la planificación con código: ' || par$cod_planificacion ,
              par$cod_identificador   => par$cod_device_id);

        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
          VALUES ('2040070012',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070012', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_planificacion, 0),
                  par$cod_device_id, 'VALIDACIONES');
                  
      ELSIF par$bol_baja_plan = 0 THEN
        /* No es necesario validar fechas si estoy haciendo una baja */
        IF par$fyh_inicio IS NULL THEN
          /* Log API*/       
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Es obligatorio informar la fecha y hora del inicio en la planificacion' ,
              par$cod_identificador   => par$cod_device_id);

          /* 2040070013 - Es obligatorio informar la fecha y hora del inicio en la planificacion. */
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
            VALUES ('2040070013',
                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070013', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                    par$cod_device_id, 'VALIDACIONES');
        ELSE

          var$bol_fecha_valida := 1;
          
          /* Valida si la fecha de inicio es inferior a la fecha fin */
          IF par$fyh_fin IS NOT NULL THEN
            IF par$fyh_inicio > par$fyh_fin THEN
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'La fecha/hora de inicio es superior a la fecha/hora de fin' ,
                    par$cod_identificador   => par$cod_device_id);

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES ('2040070014',
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070014', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                        par$cod_device_id, 'VALIDACIONES');
               var$bol_fecha_valida := 0;
            END IF;
          END IF;
          
          IF var$bol_fecha_valida = 1 THEN

            IF par$fyh_inicio < var$fyh_inicio_plan THEN
              var$bol_fecha_valida := 0;
            END IF;

            IF var$fyh_fin_plan IS NOT NULL THEN
              
              IF par$fyh_fin > var$fyh_fin_plan THEN
                var$bol_fecha_valida := 0;
              END IF;
            
            END IF;
              
            IF  var$bol_fecha_valida = 0 THEN
              /* Log API*/       
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'La fecha/hora informado no está dentro del rango de fechas planificado. Fecha y hora da planificación: Inicio = ' || var$fyh_inicio_plan || ' Fin = ' || var$fyh_fin_plan ,
                    par$cod_identificador   => par$cod_device_id);
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES ('2040070015',
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070015', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, var$fyh_inicio_plan || '|' || var$fyh_fin_plan, 0),
                        par$cod_device_id, 'VALIDACIONES');
            END IF;
            
          END IF;
          
        END IF;

      END IF;
      
      IF NOT fhay_error(par$cod_device_id => par$cod_device_id) AND par$bol_baja_plan = 0 AND par$oid_maquina IS NOT NULL AND par$oid_planificacion IS NOT NULL THEN
        /* Verifica si estan cambiando las fechas */
        BEGIN
          
          SELECT COUNT(1)
            INTO var$existe
            FROM SAPR_TPLANXMAQUINA PLMA
           WHERE PLMA.OID_PLANIFICACION = par$oid_planificacion
             AND PLMA.OID_MAQUINA = par$oid_maquina
             AND PLMA.FYH_VIGENCIA_INICIO = par$fyh_inicio
             AND PLMA.FYH_VIGENCIA_FIN = par$fyh_fin
             AND PLMA.BOL_ACTIVO = 1;
           
        EXCEPTION WHEN no_data_found THEN
          var$existe := 0;
        END;
        
        /* Si existe una configuracion con la misma fecha de inicio y fin, 
           entonces no es necesario hacer cambios. */
        IF var$existe > 0 THEN
          par$oid_planificacion := NULL;
        END IF;
        
      END IF;
      
     END IF;
    
  END svalidar_planxmaquina;

  /* Validar informaciones de los puntos de servicios */
  PROCEDURE svalidar_pto_servicio(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$a_puntos_cod_device_id  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_puntos_bol_baja       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_puntos_cod_punto      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_pto_servicio';
    var$oid_pto_servicio gepr_pcomon_###VERSION###.tipo$oid_;
    var$puntos_bol_baja  gepr_pcomon_###VERSION###.tipo$cod_;

  BEGIN

    IF par$a_puntos_cod_device_id IS NOT NULL AND par$a_puntos_cod_device_id.COUNT > 0 THEN

      FOR idx IN par$a_puntos_cod_device_id.first .. par$a_puntos_cod_device_id.last LOOP      
    
        IF par$a_puntos_cod_punto(idx) IS NOT NULL THEN  
          
          var$oid_pto_servicio := NULL;
          
          IF par$cod_identificador_ajeno IS NULL THEN
            /* No utiliza codigo ajeno */
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Validación de punto de servicio de código: '|| par$a_puntos_cod_punto(idx) || ' sin código ajeno',
                  par$cod_identificador   => '');

            BEGIN
             SELECT PTO.OID_PTO_SERVICIO
               INTO var$oid_pto_servicio
               FROM GEPR_TPUNTO_SERVICIO PTO
              WHERE PTO.COD_PTO_SERVICIO = par$a_puntos_cod_punto(idx)
                AND PTO.BOL_VIGENTE = 1;
            EXCEPTION WHEN no_data_found THEN
              var$oid_pto_servicio := NULL;
            END;
            /* 2040070020 - No existe el punto de servicio [{0}] configurado en Genesis Producto. */
            IF var$oid_pto_servicio IS NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES ('2040070020',
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070020', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$a_puntos_cod_punto(idx), 0),
                        par$a_puntos_cod_device_id(idx), 'VALIDACIONES');              
            END IF;

          ELSE
            /* Utiliza codigo ajeno */
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Validación de punto de servicio de código: '|| par$a_puntos_cod_punto(idx) || ' con código ajeno' || par$cod_identificador_ajeno,
                  par$cod_identificador   => '');
            BEGIN
                  SELECT CA.OID_TABLA_GENESIS
                    INTO var$oid_pto_servicio
                    FROM GEPR_TCODIGO_AJENO CA
              INNER JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.Oid_Pto_Servicio = CA.OID_TABLA_GENESIS AND PTO.BOL_VIGENTE = 1
                   WHERE CA.COD_AJENO =  par$a_puntos_cod_punto(idx)
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                     AND CA.BOL_ACTIVO = 1;
            EXCEPTION WHEN no_data_found THEN
              var$oid_pto_servicio := NULL;
            END;
            /* 2040070021 - No fue posible identificar una Punto Servicio en Genesis Producto, utilizando el identificador [{0}] y el codigo ajeno [{0}]. */
            IF var$oid_pto_servicio IS NULL THEN
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES ('2040070021',
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070021', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_identificador_ajeno || '|' || par$a_puntos_cod_device_id(idx), 0),
                        par$a_puntos_cod_device_id(idx), 'VALIDACIONES');              
            END IF;

          END IF;
          
          /* Inserir en la tabla temporaria el punto de servicio que no ocurrio error */
          IF var$oid_pto_servicio IS NOT NULL THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Punto de servicio encontrado durante la validacion con OID: '|| var$oid_pto_servicio,
                par$cod_identificador   => '');

            IF par$a_puntos_bol_baja(idx) IS NULL THEN
              var$puntos_bol_baja :=  0;
            ELSE
              var$puntos_bol_baja := par$a_puntos_bol_baja(idx);
            END IF; 

             INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (par$a_puntos_cod_device_id(idx) , var$oid_pto_servicio, var$puntos_bol_baja,'PTO_SERVICIO');

          END IF;

        END IF;
      END LOOP;
    END IF;

  END svalidar_pto_servicio;
  
  /* Validar si existe error con las informaciones de la maquina */
  FUNCTION fhay_error(par$cod_device_id IN gepr_pcomon_###VERSION###.tipo$cod_) RETURN BOOLEAN IS
    var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    BEGIN

      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR
       WHERE COD_CALIFICADOR = 'VALIDACIONES'
         AND COD_CAMPO3 = par$cod_device_id
         AND OID_CAMPO1 <> '0040070000';

    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;

    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);
  END fhay_error;

  /* Alterar puntos de servicios em la mae */
  PROCEDURE srel_punto_maquina(par$oid_maquina IN  gepr_pcomon_###VERSION###.tipo$oid_,                     
                               par$cod_device_id           IN  gepr_pcomon_###VERSION###.tipo$cod_,                     
                               par$cod_usuario    IN gepr_pcomon_###VERSION###.tipo$desc_) IS
                               
        CURSOR cur$pto_servicio IS
          SELECT AUX.COD_CAMPO2 OID_PTO_SERVICIO,
          AUX.COD_CAMPO3 AS QUITAR
          FROM SAPR_GTT_TAUXILIAR AUX 
          WHERE  AUX.COD_CALIFICADOR = 'PTO_SERVICIO'
          and aux.oid_campo1 = par$cod_device_id ;                       
  BEGIN
    
          FOR rec$pto_servicio IN cur$pto_servicio LOOP
              IF  rec$pto_servicio.QUITAR = 1          THEN
                  SAPR_PPLANIFICACION_###VERSION###.sdel_planxptocan(par$oid_maquina   => NULL,
                             par$oid_pto_servicio     => rec$pto_servicio.OID_PTO_SERVICIO,
                             par$cod_usuario     => par$cod_usuario);
               ELSE
                  SAPR_PPLANIFICACION_###VERSION###.sdel_planxptocan(par$oid_maquina   => par$oid_maquina,
                             par$oid_pto_servicio     => rec$pto_servicio.OID_PTO_SERVICIO,
                             par$cod_usuario     => par$cod_usuario);
                 END IF;
         
         END LOOP cur$pto_servicio;
                            
                            
             UPDATE GEPR_TPUNTO_SERVICIO 
              SET      OID_MAQUINA = par$oid_maquina,
                     COD_USUARIO             = par$cod_usuario,
                     FYH_ACTUALIZACION       = sys_extract_utc(current_timestamp)
              WHERE OID_PTO_SERVICIO IN (SELECT AUX.COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE  AUX.COD_CALIFICADOR = 'PTO_SERVICIO' AND AUX.COD_CAMPO3 = 0 and aux.oid_campo1 = par$cod_device_id  );
              
              
               UPDATE GEPR_TPUNTO_SERVICIO 
              SET      OID_MAQUINA = NULL,
                     COD_USUARIO             = par$cod_usuario,
                     FYH_ACTUALIZACION       = sys_extract_utc(current_timestamp)
              WHERE OID_PTO_SERVICIO IN (SELECT AUX.COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE  AUX.COD_CALIFICADOR = 'PTO_SERVICIO' AND AUX.COD_CAMPO3 = 1 and aux.oid_campo1 = par$cod_device_id );
  END srel_punto_maquina;

  /* Bajar maquina */
  PROCEDURE sbajar_maquinas(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$acod_device_id           IN gepr_pcomon_###VERSION###.tipo$cods_,
                            par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$rc_validaciones          OUT sys_refcursor,
                            par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$cod_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sbajar_maquinas';

    /* LOG */
    var$inicio    TIMESTAMP := SYSTIMESTAMP;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err       varchar2(4000);
    var$detalle           VARCHAR2(4000);
    var$detalle_actual    VARCHAR2(4000);
    
    var$oid_maquina gepr_pcomon_###VERSION###.tipo$oid_;

  BEGIN

    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TAUXILIAR;
    var$deletes   := var$deletes + sql%ROWCOUNT;

    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

/* INICIO - LOG de parametros iniciales*/
    IF par$oid_llamada IS NOT NULL THEN
      BEGIN
        var$detalle := q'[Parametros iniciales: 
          par$oid_llamada: ]'             || par$oid_llamada || q'[
          par$cod_cultura: ]'             || par$cod_cultura || q'[
          par$info_ejecucion: ]'          || par$info_ejecucion || q'[
          par$cod_usuario: ]'             || par$cod_usuario;

        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$detalle,
                par$cod_identificador   => '');
        /* INICIO LOG de arrays asociativos */
        IF par$acod_device_id IS NOT NULL AND par$acod_device_id.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$acod_device_id.first ..  par$acod_device_id.last LOOP 
              var$detalle_actual := '';
              IF par$acod_device_id(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$acod_device_id(' || idx || '):  ' || par$acod_device_id(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$acod_device_id(' || idx || '):  NULL';
              END IF;
              IF (LENGTH(var$detalle) + LENGTH(const$new_line) + LENGTH(var$detalle_actual)) > 4000 THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');
                var$detalle := var$detalle_actual;
              ELSE
                var$detalle := var$detalle || const$new_line || var$detalle_actual;
              END IF;
          END LOOP;
          IF var$detalle IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
          END IF;
        END IF;                  
        /* FIN LOG de arrays asociativos */
      EXCEPTION
          WHEN others THEN
            /* Log API*/       
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Error de logueo de parametros iniciales: SQLCODE: ' || SQLCODE || ' SQLERRM: ' || SQLERRM,
                  par$cod_identificador   => '');
      END;
    END IF;
    /* FIN - LOG de parametros iniciales*/

    /* Loop Maquinas */
    FOR idx IN par$acod_device_id.first .. par$acod_device_id.last LOOP

      /* Validaciones */
      dbms_output.put_line('Validaciones');
      /* Log API*/       
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Antes de llamar procedure svalidar_bajar_maquina',
                  par$cod_identificador   => '');

      svalidar_bajar_maquina(par$cod_device_id => par$acod_device_id(idx),
                             par$cod_cultura   => par$cod_cultura,
                             par$oid_maquina   => var$oid_maquina);

      IF NOT fhay_error(par$acod_device_id(idx)) THEN
        
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Antes de llamar procedure sdel_tmaquina',
                  par$cod_identificador   => '');
        /* Bajar maquina */
        sdel_tmaquina(par$oid_maquina => var$oid_maquina,
                      par$cod_usuario => par$cod_usuario);

        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                par$des_detalle     => 'Antes de llamar procedure gepr_pcodigo_ajeno_###VERSION###.seliminar_ajeno SAPR_TMAQUINA',
                par$cod_identificador   => par$acod_device_id(idx));    
        /* Código Ajeno*/
        gepr_pcodigo_ajeno_###VERSION###.seliminar_ajeno(par$oid_tabla_genesis => var$oid_maquina,
                                                         par$cod_tabla_genesis    => 'SAPR_TMAQUINA',
                                                         par$cod_identificador    => 'MAE');

        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Antes de llamar procedure sdel_tsector',
                  par$cod_identificador   => '');
        /* Bajar sector */
        sdel_tsector(par$oid_maquina => var$oid_maquina,
                     par$cod_usuario => par$cod_usuario);

        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Antes de llamar procedure sdel_tplanxmaquina',
                  par$cod_identificador   => '');
        /* Desvinculando maquina con la planificacion */
        sdel_tplanxmaquina(par$oid_maquina => var$oid_maquina,
                           par$cod_usuario => par$cod_usuario);
                           
      
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Antes de llamar procedure SAPR_PPLANIFICACION_###VERSION###.sdel_planxcan_maqu',
                  par$cod_identificador   => '');
        SAPR_PPLANIFICACION_###VERSION###.sdel_planxcan_maqu(par$oid_plan_nueva   => NULL,
                             par$oid_maquina     => var$oid_maquina,
                             par$cod_usuario     => par$cod_usuario);  
      
        /* Validar periodos abiertos*/
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Antes de llamar procedure sdel_tperiodos',
                  par$cod_identificador   => '');
        sdel_tperiodos(par$oid_maquina =>var$oid_maquina  );   
        
        /* Inserindo mensaje de exito para el device id */
         INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR) 
         VALUES ('0040070000',
              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040070000', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
              par$acod_device_id(idx), 'VALIDACIONES');

      END IF;

    END LOOP;

    /* Validaciones */
    OPEN par$rc_validaciones FOR
         SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CAMPO3 AS DEVICE_ID
           FROM SAPR_GTT_TAUXILIAR
          WHERE COD_CALIFICADOR = 'VALIDACIONES';

    COMMIT;
    
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

  END sbajar_maquinas;

  /* Validar informaciones para bajar maquina */
  PROCEDURE svalidar_bajar_maquina(par$cod_device_id IN gepr_pcomon_###VERSION###.tipo$cod_,   
                                   par$cod_cultura   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$oid_maquina   OUT gepr_pcomon_###VERSION###.tipo$oid_) IS

    var$bol_activo gepr_pcomon_###VERSION###.tipo$nbol_;
    
  BEGIN

    IF par$cod_device_id IS NULL THEN

      /* 2040070001 - Es obligatorio informar el DeviceID. */
      dbms_output.put_line('Es obligatorio informar el DeviceID.');
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
        VALUES ('2040070001',
                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070001', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0),
                par$cod_device_id, 'VALIDACIONES');

    ELSE

      BEGIN
        SELECT OID_MAQUINA, BOL_ACTIVO
          INTO par$oid_maquina, var$bol_activo
          FROM SAPR_TMAQUINA M
         WHERE COD_IDENTIFICACION = par$cod_device_id;
      EXCEPTION WHEN no_data_found THEN
        par$oid_maquina := NULL;
      END;
      IF par$oid_maquina IS NULL THEN
        /* 2040070018 - No existe una maquina con el DeviceID [{0}] configurada en Génesis Producto. */
        dbms_output.put_line('No existe una maquina con el DeviceID [{0}] configurada en Génesis Producto.');
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
          VALUES ('2040070018',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070018', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_device_id, 0),
                  par$cod_device_id, 'VALIDACIONES');
      ELSIF par$oid_maquina IS NOT NULL AND var$bol_activo = 0 THEN
        /* 2040070022 - La maquina no está activa. */
        dbms_output.put_line('La maquina no está activa.');
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
          VALUES ('2040070022',
                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040070022', gepr_pcomon_###VERSION###.const$CodFunConfigurarMaquina, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_device_id, 0),
                  par$cod_device_id, 'VALIDACIONES');        
      END IF;

    END IF;

  END svalidar_bajar_maquina;

  /* sdel_tmaquina */
  PROCEDURE sdel_tmaquina(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN

    UPDATE SAPR_TMAQUINA SET
        BOL_ACTIVO = 0,
        GMT_MODIFICACION = var$gmt_zero,
        DES_USUARIO_MODIFICACION = par$cod_usuario
    WHERE OID_MAQUINA = par$oid_maquina;

  END sdel_tmaquina;

  /* sdel_tsector */
  PROCEDURE sdel_tsector(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_sector gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    BEGIN
        SELECT MAQU.OID_SECTOR
          INTO var$oid_sector
          FROM SAPR_TMAQUINA MAQU
         WHERE MAQU.OID_MAQUINA = par$oid_maquina;
    EXCEPTION WHEN no_data_found THEN
      var$oid_sector := NULL;
    END;

    UPDATE GEPR_TSECTOR SET
        BOL_ACTIVO = 0,
        GMT_MODIFICACION = var$gmt_zero,
        DES_USUARIO_MODIFICACION = par$cod_usuario
    WHERE OID_SECTOR = var$oid_sector;

    /* Código Ajeno*/
    gepr_pcodigo_ajeno_###VERSION###.seliminar_ajeno(par$oid_tabla_genesis => var$oid_sector,
                                                      par$cod_tabla_genesis    => 'GEPR_TSECTOR',
                                                      par$cod_identificador    => 'MAE');


  END sdel_tsector;
  
  /* sdel_tplanxmaquina */
  PROCEDURE sdel_tplanxmaquina(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN

    UPDATE SAPR_TPLANXMAQUINA 
       SET BOL_ACTIVO = 0
         , FYH_VIGENCIA_FIN = sys_extract_utc(current_timestamp)
         , GMT_MODIFICACION = var$gmt_zero
         , DES_USUARIO_MODIFICACION = par$cod_usuario
     WHERE OID_MAQUINA = par$oid_maquina
       AND BOL_ACTIVO = 1;

  END sdel_tplanxmaquina;

  /* sdel_tperiodos */
  PROCEDURE sdel_tperiodos(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_) IS
  BEGIN

      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR )
             SELECT P.OID_MAQUINA, P.OID_PERIODO, 'DEL_PERIODO'
                   FROM SAPR_TPERIODO P
             INNER JOIN SAPR_TESTADO_PERIODO E
                ON P.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
              INNER JOIN SAPR_TTIPO_PERIODO TIPE 
                ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
             INNER JOIN SAPR_TPLANXMAQUINA PM
                ON PM.OID_PLANIFICACION = P.OID_PLANIFICACION
               AND PM.OID_MAQUINA = P.OID_MAQUINA
             WHERE E.COD_ESTADO_PERIODO = gepr_pcomon_###VERSION###.const$CodEstadoPeriodoTransito
               AND PM.BOL_ACTIVO = 0
               AND P.OID_MAQUINA = par$oid_maquina;

    /* Borrar los periodos de las maquinas no activa */
    DELETE FROM SAPR_TPERIODOXDOCUMENTO PD
     WHERE PD.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');

    DELETE FROM SAPR_TCALCULO_EFECTIVO CE
     WHERE CE.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');

    DELETE FROM SAPR_TCALCULO_MEDIO_PAGO CMP
     WHERE CMP.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');

    DELETE FROM SAPR_TPERIODO PR
     WHERE PR.OID_PERIODO IN
           (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');

  END sdel_tperiodos;

   PROCEDURE srecuperar_comission_dtbanca(par$oid_maquinas IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$rc_comision             OUT sys_refcursor) IS
  BEGIN
    
        IF par$oid_maquinas IS NOT NULL AND par$oid_maquinas.COUNT > 0  THEN
         /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LAS MAQUINAS */
           FOR idx IN par$oid_maquinas.first .. par$oid_maquinas.last LOOP
             IF par$oid_maquinas(idx) IS NOT NULL THEN
                 INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                  VALUES (par$oid_maquinas(idx), 'OID_MAQUINA');
             END IF;
           END LOOP;
         END IF;
         
   OPEN par$rc_comision FOR
SELECT M.OID_MAQUINA,
        PM.NUM_PORCENT_COMISION,
             NVL(SCLI.OID_SUBCLIENTE  , MAX(SCLIT.OID_SUBCLIENTE)) AS OID_SUBCLIENTE,
             NVL(SCLI.COD_SUBCLIENTE, MAX(SCLIT.COD_SUBCLIENTE)) AS COD_SUBCLIENTE,
             NVL(SCLI.DES_SUBCLIENTE,MAX(SCLIT.DES_SUBCLIENTE)) AS DES_SUBCLIENTE,
             NVL(PTO.OID_PTO_SERVICIO, MAX(PTOT.OID_PTO_SERVICIO)) AS OID_PTO_SERVICIO,
             NVL(PTO.COD_PTO_SERVICIO, MAX(PTOT.COD_PTO_SERVICIO)) AS COD_PTO_SERVICIO,
             NVL(PTO.DES_PTO_SERVICIO, MAX(PTOT.DES_PTO_SERVICIO)) AS DES_PTO_SERVICIO           
        FROM  SAPR_TMAQUINA M
       INNER JOIN GEPR_TSECTOR S
          ON M.OID_SECTOR = S.OID_SECTOR
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = S.OID_PLANTA  
            INNER JOIN SAPR_GTT_TAUXILIAR AUX1
        ON AUX1.OID_CAMPO1 = M.OID_MAQUINA                 
       LEFT JOIN (SELECT PMQ.OID_MAQUINA,PMQ.OID_PLANIFICACION, PMQ.NUM_PORCENT_COMISION, PMQ.OID_SUBCLIENTE_TESORERIA,PMQ.OID_PTO_SERVICIO_TESORERIA
       FROM SAPR_TPLANXMAQUINA PMQ 
       INNER JOIN SAPR_TPLANIFICACION PLA ON PLA.OID_PLANIFICACION = PMQ.OID_PLANIFICACION
       INNER JOIN  SAPR_GTT_TAUXILIAR AUX2 ON AUX2.OID_CAMPO1 = PMQ.OID_MAQUINA where PMQ.BOL_ACTIVO = 1
       )  PM
          ON PM.OID_MAQUINA = M.OID_MAQUINA       
       LEFT JOIN GEPR_TSUBCLIENTE SCLI
          ON SCLI.OID_SUBCLIENTE = PM.OID_SUBCLIENTE_TESORERIA
       LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
          ON PTO.OID_PTO_SERVICIO = PM.OID_PTO_SERVICIO_TESORERIA
       LEFT JOIN SAPR_TPLANIFICACION PLA 
          ON PM.OID_PLANIFICACION = PLA.OID_PLANIFICACION
       LEFT JOIN gepr_tdelegacionxconfig_factur DEFA
          ON DEFA.OID_DELEGACION = P.OID_DELEGACION AND DEFA.OID_CLIENTE_CAPITAL = PLA.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SCLIT
          ON SCLIT.OID_SUBCLIENTE = DEFA.OID_SUBCLIENTE_TESORERIA
       LEFT JOIN GEPR_TPUNTO_SERVICIO PTOT
          ON PTOT.OID_PTO_SERVICIO = DEFA.OID_PTO_SERVICIO_TESORERIA
         
         GROUP BY M.OID_MAQUINA,
             PM.NUM_PORCENT_COMISION, 
             SCLI.OID_SUBCLIENTE,
             SCLI.COD_SUBCLIENTE,
             SCLI.DES_SUBCLIENTE,
             PTO.OID_PTO_SERVICIO,
             PTO.COD_PTO_SERVICIO,
             PTO.DES_PTO_SERVICIO;
         
         
  end srecuperar_comission_dtbanca;     
  
  
  
  
  
   PROCEDURE srecuperar_plancanal_maquina(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$rc_canales              OUT sys_refcursor,
                               par$rc_subcanales           OUT sys_refcursor) IS
                                 
    var$consulta_maquina             clob := ''; 
    var$filtro_maquina              clob := '';  
  BEGIN           
    
    
    
  
               
    OPEN par$rc_canales FOR 
      SELECT  COD_AGRUPADOR, PS.OID_PTO_SERVICIO, PS.COD_PTO_SERVICIO, PS.DES_PTO_SERVICIO,
      CAN.OID_CANAL, CAN.COD_CANAL, CAN.DES_CANAL
      FROM SAPR_TPLANXCANAL PC
      LEFT JOIN GEPR_TCANAL CAN ON PC.OID_CANAL = CAN.OID_CANAL
      LEFT JOIN GEPR_TPUNTO_SERVICIO PS ON PS.OID_PTO_SERVICIO = PC.OID_PTO_SERVICIO
      WHERE PC.BOL_ACTIVO = 1 AND PC.OID_MAQUINA  = par$oid_maquina;


      open par$rc_subcanales for
      SELECT  COD_AGRUPADOR, PSC.OID_PTO_SERVICIO,
      SC.OID_CANAL,SC.OID_SUBCANAL, SC.COD_SUBCANAL, SC.DES_SUBCANAL
      FROM SAPR_TPLANXSUBCANAL PSC      
      INNER JOIN SAPR_TPLANXCANAL PC ON PC.OID_PLANXCANAL = PSC.OID_PLANXCANAL
      LEFT JOIN GEPR_TSUBCANAL SC ON PSC.OID_SUBCANAL = SC.OID_SUBCANAL
      WHERE PC.BOL_ACTIVO = 1 AND PSC.BOL_ACTIVO = 1 AND PSC.OID_MAQUINA  = par$oid_maquina;

         
         
  end srecuperar_plancanal_maquina;     
  
  
   
END SAPR_PMAQUINA_###VERSION###;
/