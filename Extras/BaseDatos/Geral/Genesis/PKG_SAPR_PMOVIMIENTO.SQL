CREATE OR REPLACE PACKAGE SAPR_PMOVIMIENTO_###VERSION### AS

  /*Version: ###VERSION_COMP###*/

    const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PMOVIMIENTO_###VERSION###';
    const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
    const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
    const$comilla_simple  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);
    const$vacio CONSTANT     gepr_pcomon_###VERSION###.tipo$desc_ := '###VACIO###';

  /* Recuperar informaciones del movimientos */
  PROCEDURE srecuperar_informaciones(par$movimientos             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$tipo_movimiento         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$rc_movimientos          OUT sys_refcursor,
                                     par$rc_codigos_ajenos       OUT sys_refcursor,
                                     par$rc_periodos             OUT sys_refcursor,
                                     par$rc_validaciones         OUT sys_refcursor,
                                     par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Es responsable por marcar los movimientos como acreditados y/o notificados. */
  PROCEDURE smarcar_movimiento( 
                                par$oid_llamada            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$movimientos             IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$tipo_movimiento         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$accion                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$fecha_hora              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$sis_origen              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$sis_destino             IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$mensaje                 IN gepr_pcomon_###VERSION###.tipo$obs_,
                                par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                par$rc_validaciones         OUT sys_refcursor,
                                par$rc_codigosexternos      OUT sys_refcursor,
                                par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_);

 /* Recuperar Movimientos */
  PROCEDURE srecuperar_movimientos(par$pag_indice              IN gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$pag_reg_pagina          IN gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$filtro_val_detallar     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_disponible       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$fltro_info_adic         IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_notificado       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_acreditado       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_maqu_vigente     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_valores_inform   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_collectionid     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$fg_desde                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fg_hasta                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fc_desde                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fc_hasta                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fa_desde                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fa_hasta                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$cod_cliente             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_sub_cliente         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_punto_servicio      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_planta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_maquinas            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_canales             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_formularios         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_divisas             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                   par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$rc_movimientos          OUT sys_refcursor,
                                   par$rc_formularios          OUT sys_refcursor,
                                   par$rc_cuentas              OUT sys_refcursor,
                                   par$rc_terminos             OUT sys_refcursor,
                                   par$rc_divisas              OUT sys_refcursor,
                                   par$rc_denominaciones       OUT sys_refcursor,
                                   par$rc_efectivos            OUT sys_refcursor,
                                   par$rc_mediopagos           OUT sys_refcursor,
                                   par$validaciones            OUT sys_refcursor,
                                   par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_);


 

  /* Validar Codigos */
  PROCEDURE svalidar_codigos(par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_cliente             IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_sub_cliente         IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_punto_servicio      IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_planta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_maquinas            IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_canales             IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_formularios         IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_divisas             IN gepr_pcomon_###VERSION###.tipo$cods_,
               par$oid_sector              OUT gepr_pcomon_###VERSION###.tipo$obs_,
                             par$oid_cliente             OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_sub_cliente         OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_punto_servicio      OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_delegacion          OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_planta              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_canales             OUT gepr_pcomon_###VERSION###.tipo$oids_,
                             par$oid_formularios         OUT gepr_pcomon_###VERSION###.tipo$oids_,
                             par$selects                 IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$validaciones            OUT gepr_pcomon_###VERSION###.tipo$descs_);


  PROCEDURE srecuperar_transaciones(par$oid_delegacion_gmt       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_transaciones         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$bol_planificada          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$bol_notificado           IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$bol_acreditacion         IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$oid_delegaciones         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_canales              IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$cod_fecha                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_desde                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_hasta                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_referencia           IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                    par$bol_importeinformativo   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$oid_clientes             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_subclientes          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_maquinas             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_sectores             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_punto_servicio       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_banco_capital        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_banco_facturacion    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_banco_tesoreria      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_cuenta_tesoreria     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_tipo_planificaciones IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_planificaciones      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$rc_movimientos           OUT sys_refcursor,
                                    par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                    par$cod_termino              IN gepr_pcomon_###VERSION###.tipo$cod_,
                    par$val_termino              IN gepr_pcomon_###VERSION###.tipo$desc_);


  PROCEDURE srecuperar_detalle(par$oid_transaccion         IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_externo_base        IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$rc_detalle              OUT sys_refcursor,
                               par$rc_detalle_valores      OUT sys_refcursor,
                               par$rc_detalle_totales      OUT sys_refcursor,
                               par$rc_documentos           OUT sys_refcursor,
                               par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE svalidar_codigos_detalle(par$oid_transaccion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_externo_base  IN gepr_pcomon_###VERSION###.tipo$cod_);


 

                                
                            
  PROCEDURE svalidar_movimiento_extradata(par$cod_externo_bases  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_extr_campo        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_extr_valor        IN gepr_pcomon_###VERSION###.tipo$obs_,
                                      par$cod_cultura           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$bol_valido            OUT gepr_pcomon_###VERSION###.tipo$nbol_, 
                                      par$cod_nuevo_valor       OUT gepr_pcomon_###VERSION###.tipo$obs_, 
                                      par$oid_documento         OUT gepr_pcomon_###VERSION###.tipo$cod_, 
                                      par$oid_termino           OUT gepr_pcomon_###VERSION###.tipo$cod_);
                                      
                                                                  
  PROCEDURE smodificar_movimiento(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_externos            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$cods_extr_campo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$cods_extr_valor    IN gepr_pcomon_###VERSION###.tipo$obss_,
                                  par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$rc_validaciones    OUT sys_refcursor,
                                  par$rc_tipo_resultado OUT sys_refcursor) ;


  PROCEDURE srecuperar_doc_actual_id( par$oid_llamada            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_actual_id               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$info_ejecucion              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$rc_movimientos              OUT sys_refcursor,
                                      par$rc_limites_maq              OUT sys_refcursor,
                                      par$rc_saldos_periodo           OUT sys_refcursor,
                                      par$rc_cuentas                  OUT sys_refcursor,
                                      par$rc_valores                  OUT sys_refcursor,
                                      par$rc_terminos                 OUT sys_refcursor,
                                      par$rc_dato_bancario            OUT sys_refcursor,
                                      par$rc_direccion                OUT sys_refcursor,
                                      par$rc_planificacion_maquina    OUT sys_refcursor,
                                      par$rc_planificacion            OUT sys_refcursor,
                                      par$rc_canales                  OUT sys_refcursor,
                                      par$rc_programacion             OUT sys_refcursor,
                                      par$rc_divisas                  OUT sys_refcursor,                                    
                                      par$rc_denominaciones           OUT sys_refcursor,                                    
                                      par$rc_codigos_ajenos           OUT sys_refcursor,                                    
                                      par$rc_formularios              OUT sys_refcursor,                                  
                                      par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_);

                                      
  procedure srecuperar_actual_ids
  (
      par$oid_llamada            IN gepr_pcomon_###VERSION###.tipo$oid_,
      par$acod_externo            IN gepr_pcomon_###VERSION###.tipo$cods_,
      par$acollection             IN gepr_pcomon_###VERSION###.tipo$cods_,
      par$aactual_id              IN gepr_pcomon_###VERSION###.tipo$cods_,
      par$rc_datos                OUT sys_refcursor
  );


  /* Upsert de SAPR_TMOVIMIENTO_DETALLE */
  PROCEDURE supd_mov_detalle(par$oid_movimiento           IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$oid_movimiento_detalle      IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$fec_movimiento              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                              par$cod_denominacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$des_denominacion            IN gepr_pcomon_###VERSION###.tipo$desc_,
                              par$bol_billete                 IN gepr_pcomon_###VERSION###.tipo$nel_,  
                              par$nel_cantidad                IN gepr_pcomon_###VERSION###.tipo$nel_,
                              par$num_importe                 IN gepr_pcomon_###VERSION###.tipo$imp_);

  /* Upsert de SAPR_TMOVIMIENTO_CAMPO_EXTRA */
  PROCEDURE supd_mov_campo_extra(par$oid_movimiento           IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$oid_mov_campo_extra         IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$fec_movimiento              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                              par$cod_campo_extra             IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$des_campo_extra             IN gepr_pcomon_###VERSION###.tipo$desc_);

  /* Upsert de SAPR_TMOVIMIENTO */
  PROCEDURE supd_movimiento(par$OID_MOVIMIENTO    in out gepr_pcomon_###VERSION###.tipo$oid_,
        par$OID_DOCUMENTO     in gepr_pcomon_###VERSION###.tipo$oid_,
        par$COD_PAIS          in gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_DELEGACION_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_DELEGACION_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_DELEGACION      IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$COD_CLIENTE_OPE     IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_CLIENTE_MDM     IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_CLIENTE         IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$DES_CLIENTE_OPE     IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$COD_SUBCLIENTE_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_SUBCLIENTE_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_SUBCLIENTE      IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$DES_SUBCLIENTE_OPE  IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$COD_PTO_SERVICIO_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_PTO_SERVICIO_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_PTO_SERVICIO      IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$DES_PTO_SERVICIO_OPE  IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$COD_CANAL             IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_CANAL             IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$COD_SUBCANAL          IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_SUBCANAL          IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$COD_DEVICEID          IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_MAQUINA           IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$FEC_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$fyh_,
        par$HOR_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$gmt_,
        par$FYH_CONTABLE          IN gepr_pcomon_###VERSION###.tipo$gmt_,
        par$COD_TIPO_MOVIMIENTO   IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_TIPO_MOVIMIENTO   IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_GRUPO_MOVIMIENTO  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_COMPROBANTE       IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_ACTUAL_ID         IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_COLLECTION_ID     IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$BOL_NOTIFICADO        IN gepr_pcomon_###VERSION###.tipo$nel_,
        par$FYH_NOTIFICACION      IN gepr_pcomon_###VERSION###.tipo$gmt_,
        par$BOL_ACREDITADO        IN gepr_pcomon_###VERSION###.tipo$nel_,
        par$FYH_ACREDITACION      IN gepr_pcomon_###VERSION###.tipo$gmt_,
        par$COD_PLANIFICACION     IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_PLANIFICACION     IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$COD_PLANIF_BANCO_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$COD_PLANIF_BANCO_MDM    IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_PLANIF_BANCO      IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$FYH_PLANIF_PERIODO_INI  IN gepr_pcomon_###VERSION###.tipo$gmt_,
        par$FYH_PLANIF_PERIODO_FIN  IN gepr_pcomon_###VERSION###.tipo$gmt_,
        par$COD_TIPO_PLANIFICACION  IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_TIPO_PLANIFICACION  IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$COD_DIVISA            IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$DES_DIVISA            IN gepr_pcomon_###VERSION###.tipo$desc_,
        par$NUM_IMPORTE           IN gepr_pcomon_###VERSION###.tipo$imp_,
        par$BOL_VIRTUAL           IN gepr_pcomon_###VERSION###.tipo$nel_,
        par$GMT_CREACION          IN gepr_pcomon_###VERSION###.tipo$gmt_,
        par$COD_USUARIO           IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* SP que se encarga de grabar en las tablas desmormalizadas de MOVIMIENTOS */
  procedure sgrabar_mov_consolidado(
          par$OID_MOVIMIENTO    in gepr_pcomon_###VERSION###.tipo$oid_,
          par$OID_DOCUMENTO     in gepr_pcomon_###VERSION###.tipo$oid_,
          par$COD_PAIS          in gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_DELEGACION_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_DELEGACION_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_DELEGACION      IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$COD_CLIENTE_OPE     IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_CLIENTE_MDM     IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_CLIENTE         IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$DES_CLIENTE_OPE     IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$COD_SUBCLIENTE_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_SUBCLIENTE_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_SUBCLIENTE      IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$DES_SUBCLIENTE_OPE  IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$COD_PTO_SERVICIO_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_PTO_SERVICIO_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_PTO_SERVICIO      IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$DES_PTO_SERVICIO_OPE  IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$COD_CANAL             IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_CANAL             IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$COD_SUBCANAL          IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_SUBCANAL          IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$COD_DEVICEID          IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_MAQUINA           IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$FEC_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$fyh_,
          par$HOR_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$gmt_,
          par$FYH_CONTABLE          IN gepr_pcomon_###VERSION###.tipo$gmt_,
          par$COD_TIPO_MOVIMIENTO   IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_TIPO_MOVIMIENTO   IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_GRUPO_MOVIMIENTO  IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_COMPROBANTE       IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_ACTUAL_ID         IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_COLLECTION_ID     IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$BOL_NOTIFICADO        IN gepr_pcomon_###VERSION###.tipo$nel_,
          par$FYH_NOTIFICACION      IN gepr_pcomon_###VERSION###.tipo$gmt_,
          par$BOL_ACREDITADO        IN gepr_pcomon_###VERSION###.tipo$nel_,
          par$FYH_ACREDITACION      IN gepr_pcomon_###VERSION###.tipo$gmt_,
          par$COD_PLANIFICACION     IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_PLANIFICACION     IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$COD_PLANIF_BANCO_OPE   IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$COD_PLANIF_BANCO_MDM     IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_PLANIF_BANCO     IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$FYH_PLANIF_PERIODO_INI   IN gepr_pcomon_###VERSION###.tipo$gmt_,
          par$FYH_PLANIF_PERIODO_FIN     IN gepr_pcomon_###VERSION###.tipo$gmt_,
          par$COD_TIPO_PLANIFICACION   IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_TIPO_PLANIFICACION   IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$COD_DIVISA            IN gepr_pcomon_###VERSION###.tipo$cod_,
          par$DES_DIVISA            IN gepr_pcomon_###VERSION###.tipo$desc_,
          par$num_importe           IN gepr_pcomon_###VERSION###.tipo$imp_,

          /* MOVIMIENTO DETALLE */
          par$aoid_movimiento_detalle  IN gepr_pcomon_###VERSION###.tipo$oids_,
          par$aimp_movi_detalle         IN gepr_pcomon_###VERSION###.tipo$imps_,
          par$anel_cant_movi_detalle         IN gepr_pcomon_###VERSION###.tipo$nels_,
          par$abol_billete_movi_detalle  IN gepr_pcomon_###VERSION###.tipo$nels_,
          par$acod_denom_movi_detalle    IN gepr_pcomon_###VERSION###.tipo$cods_,
          par$ades_denom_movi_detalle    IN gepr_pcomon_###VERSION###.tipo$descs_,

          /* MOVIMIENTO CAMPOS EXTRAS */
          par$aoid_mov_campos_extras  IN gepr_pcomon_###VERSION###.tipo$oids_,
          par$acod_campo_extra         IN gepr_pcomon_###VERSION###.tipo$cods_,
          par$ades_campo_extra          IN gepr_pcomon_###VERSION###.tipo$descs_,

          par$BOL_VIRTUAL         IN gepr_pcomon_###VERSION###.tipo$nel_,
          par$GMT_CREACION            IN gepr_pcomon_###VERSION###.tipo$gmt_,
          par$cod_usuario   IN gepr_pcomon_###VERSION###.tipo$cod_
  ) ;

  /* SP que se encarga de grabar en las tablas desmormalizadas de MOVIMIENTOS por OID_DOCUMENTO */
  procedure sgrabar_movimiento_por_doc(
    par$oid_llamada      IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_documento    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_usuario      IN gepr_pcomon_###VERSION###.tipo$cod_
  );

  /* Procedimiento utilizado en trigger SAPR_TPERIODOXDOCUMENTO para actualizar los datos de la planificación y del período relacionado al documento */
  procedure supd_por_relacion_periodo(par$oid_documento    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_periodo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_usuario      IN gepr_pcomon_###VERSION###.tipo$cod_
  );
  /* Procedimiento utilizado en trigger SAPR_TPERIODOXDOCUMENTO para remover los datos de la planificación y/o del período desrelacionado al documento */
  procedure supd_por_desrelacion_periodo(par$oid_documento    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_periodo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_usuario      IN gepr_pcomon_###VERSION###.tipo$cod_
  );
  /* Procedimiento utilizado en trigger SAPR_TPLANXMAQUINA para remover los datos de la planificación una vez quitada la máquina de la planificación */
  procedure supd_por_desrelacion_maq_plani(par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$fyh_vigencia_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                          par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_
  );
                                                                         
END SAPR_PMOVIMIENTO_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PMOVIMIENTO_###VERSION### AS

  /* Recuperar informaciones del movimientos */
  PROCEDURE srecuperar_informaciones(par$movimientos             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$tipo_movimiento         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$rc_movimientos           OUT sys_refcursor,
                                     par$rc_codigos_ajenos        OUT sys_refcursor,
                                     par$rc_periodos              OUT sys_refcursor,
                                     par$rc_validaciones          OUT sys_refcursor,
                                     par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_informaciones';

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
    var$oid_validaciones       gepr_pcomon_###VERSION###.tipo$oids_;
    var$cod_validaciones       gepr_pcomon_###VERSION###.tipo$cods_;
    var$des_validaciones       gepr_pcomon_###VERSION###.tipo$descs_;
    var$cod_validaciones_int   gepr_pcomon_###VERSION###.tipo$cods_;
    var$des_validaciones_int   gepr_pcomon_###VERSION###.tipo$descs_;
    var$toid_validaciones      varchar2_array;
    var$tcod_validaciones      varchar2_array;
    var$tdes_validaciones      varchar2_array;

    var$oid_maquina            gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_periodo            gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_planificacion gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_documento_s          gepr_pcomon_###VERSION###.tipo$oids_;
    var$oid_maquina_s            gepr_pcomon_###VERSION###.tipo$oids_;
    var$oid_planificacion_s      gepr_pcomon_###VERSION###.tipo$oids_;
    var$oid_periodo_s            gepr_pcomon_###VERSION###.tipo$oids_;
    var$cod_tipo_planificacion_s gepr_pcomon_###VERSION###.tipo$cods_;

    var$oid_documento_t          varchar2_array;
    var$oid_maquina_t            varchar2_array;
    var$oid_planificacion_t      varchar2_array;
    var$oid_periodo_t            varchar2_array;
    var$cod_tipo_planificacion_t varchar2_array;

    /* Movimientos que ya están notificados */
    CURSOR cur$movimientos IS
              SELECT D.OID_DOCUMENTO,
                     D.FYH_GESTION,
                     SE.COD_SECTOR,
                     CA.COD_CANAL,
                     SC.COD_SUBCANAL,
                     PS.OID_PTO_SERVICIO,
                     MAX(EFXDOC.OID_DIVISA) AS OID_DIVISA
              FROM SAPR_TDOCUMENTO D
        INNER JOIN SAPR_GTT_TOIDS T ON T.OID = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
        INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = D.OID_CUENTA_DESTINO
        INNER JOIN GEPR_TPUNTO_SERVICIO PS ON PS.OID_PTO_SERVICIO = C.OID_PTO_SERVICIO
        INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
        INNER JOIN GEPR_TCANAL CA ON CA.OID_CANAL = SC.OID_CANAL
        INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = C.OID_SECTOR
        LEFT JOIN SAPR_TEFECTIVOXDOCUMENTO EFXDOC ON D.OID_DOCUMENTO = EFXDOC.OID_DOCUMENTO
        GROUP BY D.OID_DOCUMENTO, D.FYH_GESTION, SE.COD_SECTOR, CA.COD_CANAL, SC.COD_SUBCANAL, PS.OID_PTO_SERVICIO
        ;

  BEGIN

    /* #### Inicializar los cursores #### */
    OPEN par$rc_movimientos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_codigos_ajenos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_periodos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TOIDS;
    var$deletes   := var$deletes + sql%ROWCOUNT;

    /* Cargar tabla temporaria con OID_DOCUMENTOS */
    FOR idx IN par$movimientos.first .. par$movimientos.last LOOP

      IF ((LENGTH(par$movimientos(idx)) - LENGTH(REPLACE(par$movimientos(idx), '_', ''))) = 2)
          AND LENGTH(TRIM(TRANSLATE(SUBSTR(par$movimientos(idx), 1, 10), '0123456789', ' '))) IS NULL THEN

          INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
             SELECT D.OID_DOCUMENTO, 'OID_DOCUMENTO'
               FROM SAPR_TDOCUMENTO D
              WHERE D.COD_EXTERNO like par$movimientos(idx) || '%';
          var$inserts := var$inserts + par$movimientos.COUNT;

      ELSE

          INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
             SELECT D.OID_DOCUMENTO, 'OID_DOCUMENTO'
               FROM SAPR_TDOCUMENTO D
              WHERE D.COD_EXTERNO = par$movimientos(idx)
           GROUP BY D.OID_DOCUMENTO;
          var$inserts := var$inserts + par$movimientos.COUNT;

      END IF;

    END LOOP;

    OPEN par$rc_movimientos FOR
              SELECT D.OID_DOCUMENTO,
                     D.COD_EXTERNO,
                     D.FYH_GESTION,
                     DE.OID_DELEGACION,
                     DE.COD_DELEGACION,
                     PL.OID_PLANTA,
                     PL.COD_PLANTA,
                     SE.OID_SECTOR,
                     SE.COD_SECTOR,
                     CA.OID_CANAL,
                     CA.COD_CANAL
              FROM SAPR_TDOCUMENTO D
        INNER JOIN SAPR_GTT_TOIDS T ON T.OID = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
        INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = D.OID_CUENTA_DESTINO
        INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
        INNER JOIN GEPR_TCANAL CA ON CA.OID_CANAL = SC.OID_CANAL
        INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = C.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PL ON PL.OID_PLANTA = SE.OID_PLANTA
        INNER JOIN GEPR_TDELEGACION DE ON DE.OID_DELEGACION = PL.OID_DELEGACION;

    IF par$cod_identificador_ajeno IS NOT NULL THEN

         OPEN par$rc_codigos_ajenos FOR
              SELECT D.OID_DOCUMENTO,
                     CAD.OID_TABLA_GENESIS OID_DELEGACION,
                     CAD.COD_AJENO COD_DELEGACION,
                     CAP.OID_TABLA_GENESIS OID_PLANTA,
                     CAP.COD_AJENO COD_PLANTA,
                     CAS.OID_TABLA_GENESIS OID_SECTOR,
                     CAS.COD_AJENO COD_SECTOR,
                     CAC.OID_TABLA_GENESIS OID_CANAL,
                     CAC.COD_AJENO COD_CANAL
              FROM SAPR_TDOCUMENTO D
        INNER JOIN SAPR_GTT_TOIDS T ON T.OID = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
        INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = D.OID_CUENTA_DESTINO
        INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
        INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = C.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PL ON PL.OID_PLANTA = SE.OID_PLANTA
         INNER JOIN GEPR_TCODIGO_AJENO CAS
            ON CAS.OID_TABLA_GENESIS = SE.OID_SECTOR AND CAS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAS.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
         INNER JOIN GEPR_TCODIGO_AJENO CAP
            ON CAP.OID_TABLA_GENESIS = PL.OID_PLANTA AND CAP.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAP.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
         INNER JOIN GEPR_TCODIGO_AJENO CAD
            ON CAD.OID_TABLA_GENESIS = PL.OID_DELEGACION AND CAD.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAD.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
         INNER JOIN GEPR_TCODIGO_AJENO CAC
            ON CAC.OID_TABLA_GENESIS = SC.OID_CANAL AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL';

    END IF;

    /* Recuperar periodos */
    FOR rec$movimiento IN cur$movimientos LOOP


      SAPR_PPERIODO_###VERSION###.sverificar_periodo(par$oid_llamada   => NULL,
                                            par$cod_canal              => rec$movimiento.cod_canal,
                                            par$cod_subcanal           => rec$movimiento.cod_subcanal,
                                            par$oid_divisa             => rec$movimiento.oid_divisa,
                                            par$cod_sector             => rec$movimiento.cod_sector,
                                            par$oid_pto_servicio       => rec$movimiento.oid_pto_servicio,
                                            par$cod_ajeno              => NULL,
                                            par$fyh_gestion            => rec$movimiento.fyh_gestion,
                                            par$cod_usuario            => par$cod_usuario,
                                            par$cod_cultura            => par$cod_cultura,
                                            par$info_ejecucion         => par$info_ejecucion,
                                            par$oid_maquina            => var$oid_maquina,
                                            par$oid_planificacion      => var$oid_planificacion,
                                            par$oid_periodo            => var$oid_periodo,
                                            par$cod_tipo_planificacion => var$cod_tipo_planificacion,
                                            par$cod_validaciones       => var$cod_validaciones_int,
                                            par$des_validaciones       => var$des_validaciones_int,
                                            par$cod_ejecucion          => par$cod_ejecucion);

      var$oid_documento_s(var$oid_documento_s.COUNT) := rec$movimiento.oid_documento;

      IF var$oid_maquina IS NULL THEN
         var$oid_maquina_s(var$oid_maquina_s.COUNT) := '-';
      ELSE
         var$oid_maquina_s(var$oid_maquina_s.COUNT) := var$oid_maquina;
      END IF;

      IF var$oid_planificacion IS NULL THEN
         var$oid_planificacion_s(var$oid_planificacion_s.COUNT) := '-';
      ELSE
         var$oid_planificacion_s(var$oid_planificacion_s.COUNT) := var$oid_planificacion;
      END IF;

      IF var$oid_periodo IS NULL THEN
         var$oid_periodo_s(var$oid_periodo_s.COUNT) := '-';
      ELSE
         var$oid_periodo_s(var$oid_periodo_s.COUNT) := var$oid_periodo;
      END IF;

      IF var$cod_tipo_planificacion IS NULL THEN
         var$cod_tipo_planificacion_s(var$cod_tipo_planificacion_s.COUNT) := '-';
      ELSE
         var$cod_tipo_planificacion_s(var$cod_tipo_planificacion_s.COUNT) := var$cod_tipo_planificacion;
      END IF;

      /* Validaciones */
      IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$des_validaciones_int) THEN

        FOR idx IN var$des_validaciones_int.first .. var$des_validaciones_int.last LOOP
           var$oid_validaciones(var$oid_validaciones.COUNT) := rec$movimiento.oid_documento;
           var$cod_validaciones(var$cod_validaciones.COUNT) := var$cod_validaciones_int(idx);
           var$des_validaciones(var$des_validaciones.COUNT) := var$des_validaciones_int(idx);

        END LOOP;

      END IF;

    END LOOP rec$movimiento;
    var$selects := var$selects + 1;

    IF var$oid_documento_s.COUNT > 0 THEN

        var$oid_documento_t := gepr_putilidades_###VERSION###.fconverter_array_table(par$table_varchar2 => var$oid_documento_s);

        var$oid_maquina_t := gepr_putilidades_###VERSION###.fconverter_array_table(par$table_varchar2 => var$oid_maquina_s);
        var$oid_planificacion_t := gepr_putilidades_###VERSION###.fconverter_array_table(par$table_varchar2 => var$oid_planificacion_s);
        var$oid_periodo_t := gepr_putilidades_###VERSION###.fconverter_array_table(par$table_varchar2 => var$oid_periodo_s);
        var$cod_tipo_planificacion_t := gepr_putilidades_###VERSION###.fconverter_array_cod_table(par$table_varchar2 => var$cod_tipo_planificacion_s);

          open par$rc_periodos for
            WITH OID AS (
                 SELECT rownum IDX, C.COLUMN_VALUE
                   FROM TABLE(var$oid_documento_t) C
                  WHERE C.COLUMN_VALUE IS NOT NULL
            ),
            MAE AS (
                 SELECT rownum IDX, C.COLUMN_VALUE
                   FROM TABLE(var$oid_maquina_t) C
                  WHERE C.COLUMN_VALUE IS NOT NULL
            ),
            PLAN AS (
                 SELECT rownum IDX, C.COLUMN_VALUE
                   FROM TABLE(var$oid_planificacion_t) C
                  WHERE C.COLUMN_VALUE IS NOT NULL
            ),
            PER AS (
                 SELECT rownum IDX, C.COLUMN_VALUE
                   FROM TABLE(var$oid_periodo_t) C
                  WHERE C.COLUMN_VALUE IS NOT NULL
            ),
            COD AS (
                 SELECT rownum IDX, C.COLUMN_VALUE
                   FROM TABLE(var$cod_tipo_planificacion_t) C
                  WHERE C.COLUMN_VALUE IS NOT NULL
            )
            SELECT OID.COLUMN_VALUE OID_DOCUMENTO,
                   MAE.COLUMN_VALUE OID_MAQUINA,
                   PLAN.COLUMN_VALUE OID_PLANIFICACION,
                   PER.COLUMN_VALUE OID_PERIODO,
                   COD.COLUMN_VALUE COD_TIPO_PLANIFICACION
              FROM OID
        INNER JOIN MAE ON MAE.IDX = OID.IDX
        INNER JOIN PLAN ON PLAN.IDX = MAE.IDX
        INNER JOIN PER ON PER.IDX = PLAN.IDX
        INNER JOIN COD ON COD.IDX = PER.IDX;

    END IF;

    /* Validaciones */
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$des_validaciones) THEN

      var$toid_validaciones := gepr_putilidades_###VERSION###.fconverter_array_table(par$table_varchar2 => var$oid_validaciones);
      var$tcod_validaciones := gepr_putilidades_###VERSION###.fconverter_array_cod_table(par$table_varchar2 => var$cod_validaciones);
      var$tdes_validaciones := gepr_putilidades_###VERSION###.fconverter_array_desc_table(par$table_varchar2 => var$des_validaciones);

      open par$rc_validaciones for
            WITH OID AS (
                 SELECT rownum IDX, C.COLUMN_VALUE
                   FROM TABLE(var$toid_validaciones) C
                  WHERE C.COLUMN_VALUE IS NOT NULL
            ),
            COD AS (
                 SELECT rownum IDX, C.COLUMN_VALUE
                   FROM TABLE(var$tcod_validaciones) C
                  WHERE C.COLUMN_VALUE IS NOT NULL
            ),
            DES AS (
                 SELECT rownum IDX, C.COLUMN_VALUE
                   FROM TABLE(var$tdes_validaciones) C
                  WHERE C.COLUMN_VALUE IS NOT NULL
            )
            SELECT OID.COLUMN_VALUE OID_DOCUMENTO,
                   COD.COLUMN_VALUE CODIGO,
                   DES.COLUMN_VALUE DESCRICION
              FROM OID
        INNER JOIN COD ON COD.IDX = OID.IDX
        INNER JOIN DES ON DES.IDX = COD.IDX;

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

  END srecuperar_informaciones;

    /* Es responsable por marcar los movimientos como acreditados y/o notificados. */
  PROCEDURE smarcar_movimiento(
                                par$oid_llamada            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$movimientos             IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$tipo_movimiento         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$accion                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$fecha_hora              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$sis_origen              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$sis_destino             IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$mensaje                 IN gepr_pcomon_###VERSION###.tipo$obs_,
                                par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                par$rc_validaciones         OUT sys_refcursor,
                                par$rc_codigosexternos      OUT sys_refcursor,
                                par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

     const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'smarcar_movimiento';

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
    /* Variables */
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_documento           gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_relaciona_periodo   gepr_pcomon_###VERSION###.tipo$cod_;
    var$mensaje_cod             gepr_pcomon_###VERSION###.tipo$cod_;
    var$mensaje_des             gepr_pcomon_###VERSION###.tipo$desc_;
    var$a_oids_documentos       gepr_pcomon_###VERSION###.tipo$oids_;

    /* Necesarios para llamar a sverificar_periodo */
    var$oid_maquina            gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_periodo            gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_planificacion gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_validaciones_int   gepr_pcomon_###VERSION###.tipo$cods_;
    var$des_validaciones_int   gepr_pcomon_###VERSION###.tipo$descs_;
    var$existe                 gepr_pcomon_###VERSION###.tipo$nel_ := 0;

    /* Movimientos que ya están notificados */
    CURSOR cur$notificados IS
      SELECT D.COD_EXTERNO
      FROM SAPR_TDOCUMENTO D
        INNER JOIN SAPR_GTT_TAUXILIAR T ON T.OID_CAMPO1 = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
      WHERE D.BOL_NOTIFICADO = 1;

    /* Movimientos que no están notificados */
    CURSOR cur$nonotificados IS
      SELECT D.OID_DOCUMENTO, D.COD_EXTERNO
      FROM SAPR_TDOCUMENTO D
        INNER JOIN SAPR_GTT_TAUXILIAR T ON T.OID_CAMPO1 = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
      WHERE D.BOL_NOTIFICADO = 0;

    /* Movimientos que ya están acreditados */
    CURSOR cur$acreditados IS
      SELECT D.COD_EXTERNO
      FROM SAPR_TDOCUMENTO D
        INNER JOIN SAPR_GTT_TAUXILIAR T ON T.OID_CAMPO1 = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
      WHERE D.BOL_ACREDITADO = 1;

    /* Movimientos - Validar Periodos */
    CURSOR cur$movimientos IS
      SELECT 
          D.OID_DOCUMENTO,
          D.FYH_GESTION,
          SE.COD_SECTOR,
          CA.COD_CANAL,
          SC.COD_SUBCANAL,
          PS.OID_PTO_SERVICIO,
          D.COD_EXTERNO,
          D.BOL_NOTIFICADO,
          D.BOL_ACREDITADO,
          MAX(EFXDOC.OID_DIVISA) AS OID_DIVISA 
      FROM SAPR_TDOCUMENTO D
      INNER JOIN SAPR_GTT_TAUXILIAR T ON T.OID_CAMPO1 = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
      INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = D.OID_CUENTA_DESTINO
      INNER JOIN GEPR_TPUNTO_SERVICIO PS ON PS.OID_PTO_SERVICIO = C.OID_PTO_SERVICIO
      INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
      INNER JOIN GEPR_TCANAL CA ON CA.OID_CANAL = SC.OID_CANAL
      INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = C.OID_SECTOR
      LEFT JOIN SAPR_TEFECTIVOXDOCUMENTO EFXDOC ON d.OID_DOCUMENTO = EFXDOC.OID_DOCUMENTO
      GROUP BY D.OID_DOCUMENTO, D.FYH_GESTION, SE.COD_SECTOR, CA.COD_CANAL, SC.COD_SUBCANAL, PS.OID_PTO_SERVICIO, D.COD_EXTERNO, D.BOL_NOTIFICADO, D.BOL_ACREDITADO
      ;
        

    /* Todos los documentos */
    CURSOR cur$documentos IS
      SELECT 
          D.OID_DOCUMENTO OID_DOCUMENTO,
          D.COD_EXTERNO COD_EXTERNO,
          D.BOL_NOTIFICADO BOL_NOTIFICADO,
          D.BOL_ACREDITADO BOL_ACREDITADO
      FROM
          SAPR_GTT_TAUXILIAR AUX
          INNER JOIN SAPR_TDOCUMENTO D ON D.OID_DOCUMENTO = AUX.OID_CAMPO1 AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
        ; 

  BEGIN

    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_codigosexternos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* Limpiar tabla temporaria */
    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TAUXILIAR';

    var$deletes   := var$deletes + sql%ROWCOUNT;

    IF par$oid_llamada IS NOT NULL THEN
      var$detalle := var$detalle || 'Parametros iniciales: ' || const$new_line;
      var$detalle := var$detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$accion: ' || const$comilla_simple || par$accion || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$fecha_hora: ' || const$comilla_simple || par$fecha_hora || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$sis_origen: ' || const$comilla_simple || par$sis_origen || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$sis_destino: ' || const$comilla_simple || par$sis_destino || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$mensaje: ' || const$comilla_simple || par$mensaje || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$cod_cultura: ' || const$comilla_simple || par$cod_cultura || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$info_ejecucion: ' || const$comilla_simple || par$info_ejecucion || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,                            
                                  par$des_detalle     => var$detalle,
                                  par$cod_identificador   => '');

      IF par$movimientos IS NOT NULL AND par$movimientos.COUNT > 0 THEN
        var$detalle := '';
        FOR idx IN  par$movimientos.first ..  par$movimientos.last LOOP 
          var$detalle_actual := '';
          IF par$movimientos(idx) IS NOT NULL THEN
            var$detalle_actual := 'Parametros iniciales: par$movimientos(' || idx || '):  ' || par$movimientos(idx);
          ELSE
            var$detalle_actual := 'Parametros iniciales: par$movimientos(' || idx || '):  NULL';
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

      IF par$tipo_movimiento IS NOT NULL AND par$tipo_movimiento.COUNT > 0 THEN
        var$detalle := '';
        FOR idx IN  par$tipo_movimiento.first ..  par$tipo_movimiento.last LOOP 
          var$detalle_actual := '';
          IF par$tipo_movimiento(idx) IS NOT NULL THEN
            var$detalle_actual := 'Parametros iniciales: par$tipo_movimiento(' || idx || '):  ' || par$tipo_movimiento(idx);
          ELSE
            var$detalle_actual := 'Parametros iniciales: par$tipo_movimiento(' || idx || '):  NULL';
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
    END IF;
    
    /* Cargar tabla temporaria con OID_DOCUMENTOS */
    FOR  idx IN par$movimientos.first .. par$movimientos.last LOOP
      IF (par$tipo_movimiento(idx) IS NOT NULL) THEN
        /* Por ActualID */

        IF(upper(par$tipo_movimiento(idx)) = upper('ActualID')) THEN
          SELECT COUNT(1)
              INTO var$existe
              FROM SAPR_TDOCUMENTO
            WHERE COD_ACTUAL_ID  = par$movimientos(idx);

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Existe/n : ' || var$existe || ' documento/s con COD_ACTUAL_ID =  ' || par$movimientos(idx),
                par$cod_identificador   => '');

            IF var$existe > 0 THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                  SELECT D.OID_DOCUMENTO, 'OID_DOCUMENTO', BOL_NOTIFICADO, BOL_ACREDITADO
                  FROM SAPR_TDOCUMENTO D
                  WHERE D.COD_ACTUAL_ID  = par$movimientos(idx);
                var$inserts := var$inserts +  sql%ROWCOUNT;
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'var$inserts: ' || var$inserts,
                    par$cod_identificador   => '');
            ELSE
              -- Buscamos el mensaje traducido 
              var$mensaje_cod := '2040020024';
              var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                          var$mensaje_cod, 
                                                                          gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                          NULL, 
                                                                          0);

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
              VALUES (SYS_GUID(),
                      par$movimientos(idx), 
                      var$mensaje_cod, 
                      var$mensaje_des, 
                      'VALIDACIONES');
            END IF;
        END IF;

        /* Por CollectionID */
        IF(upper(par$tipo_movimiento(idx)) = upper('CollectionID')) THEN

          SELECT COUNT(1)
            INTO var$existe
            FROM SAPR_TDOCUMENTO
          WHERE COD_COLLECTION_ID  = par$movimientos(idx);

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Existe/n : ' || var$existe || ' documento/s con COD_COLLECTION_ID =  ' || par$movimientos(idx),
              par$cod_identificador   => '');

          IF var$existe > 0 THEN
            INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                SELECT D.OID_DOCUMENTO, 'OID_DOCUMENTO', BOL_NOTIFICADO, BOL_ACREDITADO
                FROM SAPR_TDOCUMENTO D
                WHERE D.COD_COLLECTION_ID = par$movimientos(idx);
              var$inserts := var$inserts +  sql%ROWCOUNT;
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'var$inserts: ' || var$inserts,
                  par$cod_identificador   => '');
          ELSE
            -- Buscamos el mensaje traducido 
            var$mensaje_cod := '2040020024';
            var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                        var$mensaje_cod, 
                                                                        gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                        NULL, 
                                                                        0);

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
            VALUES (SYS_GUID(),
                    par$movimientos(idx), 
                    var$mensaje_cod, 
                    var$mensaje_des, 
                    'VALIDACIONES');
          END IF;

        END IF;

        /* Por COD_EXTERNO */
        IF(upper(par$tipo_movimiento(idx)) = upper('CodigoExterno')) THEN
            SELECT COUNT(1)
              INTO var$existe
              FROM SAPR_TDOCUMENTO
            WHERE COD_EXTERNO LIKE par$movimientos(idx) || '%';

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Existe/n : ' || var$existe || ' documento/s con COD_EXTERNO LIKE ' || par$movimientos(idx) || '%',
                par$cod_identificador   => '');

            IF var$existe > 0 THEN
              INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                  SELECT D.OID_DOCUMENTO, 'OID_DOCUMENTO', BOL_NOTIFICADO, BOL_ACREDITADO
                  FROM SAPR_TDOCUMENTO D
                  WHERE D.COD_EXTERNO like par$movimientos(idx) || '%';
                var$inserts := var$inserts +  sql%ROWCOUNT;
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'var$inserts: ' || var$inserts,
                    par$cod_identificador   => '');
            ELSE
              -- Buscamos el mensaje traducido 
              var$mensaje_cod := '2040020024';
              var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                          var$mensaje_cod, 
                                                                          gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                          NULL, 
                                                                          0);

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
              VALUES (SYS_GUID(),
                      par$movimientos(idx), 
                      var$mensaje_cod, 
                      var$mensaje_des, 
                      'VALIDACIONES');
            END IF;
        END IF;
      END IF;
    END LOOP;
    --Termine de poblar la tabla intermedia
    FOR rec$movimiento IN cur$movimientos LOOP
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Ingreso al cursor de movimientos' ,
              par$cod_identificador   => '');
      SAPR_PPERIODO_###VERSION###.sverificar_periodo(par$oid_llamada            => par$oid_llamada,
                                              par$cod_canal              => rec$movimiento.cod_canal,
                                              par$cod_subcanal           => rec$movimiento.cod_subcanal,
                                              par$oid_divisa             => rec$movimiento.oid_divisa,
                                              par$cod_sector             => rec$movimiento.cod_sector,
                                              par$oid_pto_servicio       => rec$movimiento.oid_pto_servicio,
                                              par$cod_ajeno              => NULL,
                                              par$fyh_gestion            => rec$movimiento.fyh_gestion,
                                              par$cod_usuario            => par$cod_usuario,
                                              par$cod_cultura            => par$cod_cultura,
                                              par$info_ejecucion         => par$info_ejecucion,
                                              par$oid_maquina            => var$oid_maquina,
                                              par$oid_planificacion      => var$oid_planificacion,
                                              par$oid_periodo            => var$oid_periodo,
                                              par$cod_tipo_planificacion => var$cod_tipo_planificacion,
                                              par$cod_validaciones       => var$cod_validaciones_int,
                                              par$des_validaciones       => var$des_validaciones_int,
                                              par$cod_ejecucion          => par$cod_ejecucion);
      var$detalle := 'Llamada sverificar_periodo: ';
      var$detalle := var$detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$cod_canal: ' || const$comilla_simple || rec$movimiento.cod_canal || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$cod_subcanal: ' || const$comilla_simple || rec$movimiento.cod_subcanal || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$cod_sector: ' || const$comilla_simple || rec$movimiento.cod_sector || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$oid_pto_servicio: ' || const$comilla_simple || rec$movimiento.oid_pto_servicio || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$cod_ajeno: ' || const$comilla_simple || 'NULL' || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$fyh_gestion: ' || const$comilla_simple ||  rec$movimiento.fyh_gestion || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$cod_cultura: ' || const$comilla_simple || par$cod_cultura || const$comilla_simple || const$new_line;
      var$detalle := var$detalle || 'par$info_ejecucion: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;


      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => var$detalle,
            par$cod_identificador   => '');
      /* Validaciones */
      IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$des_validaciones_int) THEN
          FOR idx IN var$des_validaciones_int.first .. var$des_validaciones_int.last LOOP

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
            VALUES (SYS_GUID(),
                    rec$movimiento.cod_externo,
                    var$cod_validaciones_int(idx),
                    var$des_validaciones_int(idx), 
                    'VALIDACIONES');
          
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$cod_validaciones_int(idx) || var$des_validaciones_int(idx),
                par$cod_identificador   => '');
          END LOOP;
      END IF;

    END LOOP rec$movimiento;

    IF(par$accion = 'Notificar') THEN
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Ingreso a la accion notificar' ,
                par$cod_identificador   => '');
      /* Inserto un nuevo registro en la tabla de SAPR_TNOTIFICACION */
      INSERT INTO SAPR_TNOTIFICACION(
              OID_NOTIFICACION, 
              OID_DOCUMENTO,
              DES_SISTEMA_ORIGEN,
              DES_SISTEMA_DESTINO,
              DES_MENSAJE,
              FYH_NOTIFICACION,
              GMT_CREACION,
              DES_USUARIO_CREACION,
              GMT_MODIFICACION,
              DES_USUARIO_MODIFICACION
              )
      SELECT
          sys_guid(),
          OID_CAMPO1,
          par$sis_origen,
          par$sis_destino,
          par$mensaje,
          par$fecha_hora,
          gepr_putilidades_###VERSION###.fgmt_zero,
          par$cod_usuario,
          gepr_putilidades_###VERSION###.fgmt_zero,
          par$cod_usuario
      FROM
          SAPR_GTT_TAUXILIAR
      WHERE
        COD_CALIFICADOR = 'OID_DOCUMENTO'
        AND NEL_CAMPO5 = 0; /* En NEL_CAMPO5 tengo BOL_NOTIFICADO */
        /* AND OID_CAMPO1 IN (SELECT OID_DOCUMENTO FROM SAPR_TDOCUMENTO WHERE BOL_NOTIFICADO = 0) ; */
      
      var$selects := var$selects + sql%ROWCOUNT;
      var$inserts := var$inserts + sql%ROWCOUNT;
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'var$selects: '|| var$selects || ' var$inserts: ' || var$inserts,
              par$cod_identificador   => '');

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Hizo el insert (o no)' ,
          par$cod_identificador   => '');

      /*  Validar los documentos que ya están notificados, para reportar en la respuesta del servicio */
      FOR rec$notificado IN cur$notificados LOOP
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => 'Entre al cursor de ya notificados' ,
            par$cod_identificador   => '');

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,                            
          par$des_detalle     => 'COD_EXTERNO: ' || rec$notificado.COD_EXTERNO ,
          par$cod_identificador   => '');

        -- Buscamos el mensaje traducido 
        var$mensaje_cod := '2040020003';
        var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                    var$mensaje_cod, 
                                                                    gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                    NULL, 
                                                                    0);

        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
        VALUES (SYS_GUID(),
                rec$notificado.COD_EXTERNO, 
                var$mensaje_cod, 
                var$mensaje_des, 
                'VALIDACIONES');

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     =>  rec$notificado.COD_EXTERNO || ' - ' ||  var$mensaje_cod || ': ' || var$mensaje_des,
              par$cod_identificador   => ''); 

       END LOOP rec$notificado;


      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,                            
      par$des_detalle     => 'Sale del cursor de ya notificados' ,
      par$cod_identificador   => '');


      -- DOCUMENTOS A NOTIFICAR
      FOR rec$notificado IN cur$nonotificados LOOP

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Inicia el update para NOTIFICAR el DOCUMENTO de OID_DOCUMENTO: ' || rec$notificado.OID_DOCUMENTO || ' y COD_EXTERNO: ' || rec$notificado.COD_EXTERNO,
                    par$cod_identificador   => '');
                  /* Actualizamos la tabla de documentos que los mismos han sido notificados */

            UPDATE
                SAPR_TDOCUMENTO
            SET
                BOL_NOTIFICADO = 1,
                FYH_NOTIFICACION = par$fecha_hora
            WHERE
                OID_DOCUMENTO = rec$notificado.OID_DOCUMENTO
                AND BOL_NOTIFICADO = 0;

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Termina el update' ,
                    par$cod_identificador   => '');

            var$updates := var$updates + sql%ROWCOUNT;
            var$selects := var$selects + sql%ROWCOUNT;

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'var$updates: ' || var$updates || ' var$selects: ' || var$selects,
                  par$cod_identificador   => '');


            -- Buscamos el mensaje traducido 
            var$mensaje_cod := '0040020000';
            var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                        var$mensaje_cod, 
                                                                        gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                        NULL, 
                                                                        0);

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
            VALUES (SYS_GUID(),
                    rec$notificado.COD_EXTERNO, 
                    var$mensaje_cod, 
                    var$mensaje_des, 
                    'VALIDACIONES');

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     =>  rec$notificado.COD_EXTERNO || ' - ' ||  var$mensaje_cod || ': ' || var$mensaje_des,
                  par$cod_identificador   => ''); 
           
      END LOOP rec$notificado;


      var$selects := var$selects + 1;
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     =>  'var$selects: ' || var$selects,
            par$cod_identificador   => '');
    END IF;

    IF(par$accion = 'Acreditar') THEN

      /*  Validar los documentos que ya están acreditados, para reportar en la respuesta del servicio */
      FOR rec$acreditado IN cur$acreditados LOOP
        -- Buscamos el mensaje traducido 
        var$mensaje_cod := '2040020004';
        var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                    var$mensaje_cod, 
                                                                    gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                    NULL, 
                                                                    0);

        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
        VALUES (SYS_GUID(),
                rec$acreditado.COD_EXTERNO, 
                var$mensaje_cod, 
                var$mensaje_des, 
                'VALIDACIONES');

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     =>  rec$acreditado.COD_EXTERNO || ' - ' ||  var$mensaje_cod || ': ' || var$mensaje_des,
              par$cod_identificador   => ''); 

      END LOOP rec$acreditado;
      var$selects := var$selects + 1;
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'var$selects: ' || var$selects,
              par$cod_identificador   => '');
       /* Asociar a Periodo */
      FOR rec$documento IN cur$documentos LOOP
        BEGIN
          SELECT MAX(D.COD_EXTERNO)
            INTO var$bol_relaciona_periodo /*BOL_RELACIONADO_PERIODO*/
          FROM SAPR_TDOCUMENTO D
            INNER JOIN SAPR_TPERIODOXDOCUMENTO PD ON PD.OID_DOCUMENTO = D.OID_DOCUMENTO
            INNER JOIN SAPR_TPERIODO PER ON PER.OID_PERIODO = PD.OID_PERIODO
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
          WHERE
              D.OID_DOCUMENTO = rec$documento.OID_DOCUMENTO;
        EXCEPTION
            WHEN no_data_found THEN
                var$bol_relaciona_periodo := NULL; 
        END;

        IF var$bol_relaciona_periodo IS NULL THEN

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'No relacionado a período DOCUMENTO de OID_DOCUMENTO: ' || rec$documento.OID_DOCUMENTO || ' y COD_EXTERNO: ' || rec$documento.COD_EXTERNO,
              par$cod_identificador   => '');
          SAPR_PPERIODO_###VERSION###.srelacionar_periodo_manual(par$oid_llamada,rec$documento.OID_DOCUMENTO,1);
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'srelacionar_periodo_manual: OID_DOCUMENTO: '|| rec$documento.OID_DOCUMENTO || ', par$bol_generar_periodos: 1',
              par$cod_identificador   => '');
          BEGIN
              SELECT MAX(D.COD_EXTERNO)
              INTO var$bol_relaciona_periodo
              FROM SAPR_TDOCUMENTO D
              INNER JOIN SAPR_TPERIODOXDOCUMENTO PD ON PD.OID_DOCUMENTO = D.OID_DOCUMENTO
                INNER JOIN SAPR_TPERIODO PER ON PER.OID_PERIODO = PD.OID_PERIODO
              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
              WHERE
                  D.OID_DOCUMENTO = rec$documento.OID_DOCUMENTO;
          EXCEPTION
              WHEN no_data_found THEN
                  var$bol_relaciona_periodo := NULL;
          END;

        END IF;

          /*Si se encuentra relacion con periodo pero no esta acreditado hacer update*/
          IF var$bol_relaciona_periodo is not null and rec$documento.BOL_ACREDITADO = 0 THEN
            /* Actualizamos la tabla de documentos que los mismos han sido acreditados */
            UPDATE
                SAPR_TDOCUMENTO
            SET
                BOL_ACREDITADO = 1,
                FYH_ACREDITACION = par$fecha_hora
            WHERE
                OID_DOCUMENTO = rec$documento.oid_documento;

            -- Buscamos el mensaje traducido 
            var$mensaje_cod := '0040020000';
            var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                        var$mensaje_cod, 
                                                                        gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                        NULL, 
                                                                        0);

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
            VALUES (SYS_GUID(),
                    rec$documento.COD_EXTERNO, 
                    var$mensaje_cod, 
                    var$mensaje_des, 
                    'VALIDACIONES');

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     =>  rec$documento.COD_EXTERNO || ' - ' ||  var$mensaje_cod || ': ' || var$mensaje_des,
                  par$cod_identificador   => ''); 

            var$updates := var$updates + sql%ROWCOUNT;
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     =>'var$updates: ' || var$updates,
                par$cod_identificador   => '');
              
          ELSE
            IF rec$documento.bol_acreditado = 1 THEN
              /*verificar se esta acreditado*/             
              IF var$bol_relaciona_periodo IS NULL THEN

                -- Buscamos el mensaje traducido 
                var$mensaje_cod := '2040020004';
                var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                            var$mensaje_cod, 
                                                                            gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                            NULL, 
                                                                            0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
                VALUES (SYS_GUID(),
                        rec$documento.COD_EXTERNO, 
                        var$mensaje_cod, 
                        var$mensaje_des, 
                        'VALIDACIONES');

                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     =>  rec$documento.COD_EXTERNO || ' - ' ||  var$mensaje_cod || ': ' || var$mensaje_des,
                      par$cod_identificador   => ''); 

              END IF;                              
            ELSE
              -- Buscamos el mensaje traducido 
              var$mensaje_cod := '2040020025';
              var$mensaje_des := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 
                                                                          var$mensaje_cod, 
                                                                          gepr_pcomon_###VERSION###.const$CodFunMarcarMovimientos, 
                                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                                                          NULL, 
                                                                          0);

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
              VALUES (SYS_GUID(),
                      rec$documento.COD_EXTERNO, 
                      var$mensaje_cod, 
                      var$mensaje_des, 
                      'VALIDACIONES');

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     =>  rec$documento.COD_EXTERNO || ' - ' ||  var$mensaje_cod || ': ' || var$mensaje_des,
                    par$cod_identificador   => ''); 
                                                                          
            END IF;
        END IF;           
      END LOOP rec$docoumento;
    END IF;

    /* TODO: 
        Todavía no esta definido el comportamiento del procedure cuando llegue un tipo de acción  = Reenviar
    */
    IF(par$accion = 'Reenviar') THEN
        /* DO SOMETHING */
        dbms_output.put_line('PMOVIMIENTO.SMARCAR_MOVIMIENTO: Do something');
    END IF;


    /* Códigos Externos --> Respuesta */
    OPEN par$rc_codigosexternos FOR
        SELECT D.COD_EXTERNO
        FROM SAPR_TDOCUMENTO D
        INNER JOIN SAPR_GTT_TAUXILIAR T ON T.OID_CAMPO1 = D.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO';

    /* Validaciones */
    OPEN par$rc_validaciones FOR
        SELECT COD_CAMPO2 AS CODIGO, COD_CAMPO3 AS CODIGO_MENSAJE, COD_CAMPO4 AS MENSAJE
          FROM SAPR_GTT_TAUXILIAR
        WHERE COD_CALIFICADOR = 'VALIDACIONES';


    var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
     SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'var$registros: ' || var$registros,
                par$cod_identificador   => '');
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

    COMMIT;
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
         SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Error: '|| var$err,
                par$cod_identificador   => '');
  END smarcar_movimiento;

  /* Recuperar Movimientos */
  PROCEDURE srecuperar_movimientos(par$pag_indice              IN gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$pag_reg_pagina          IN gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$filtro_val_detallar     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_disponible       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$fltro_info_adic         IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_notificado       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_acreditado       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_maqu_vigente     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_valores_inform   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$filtro_collectionid     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$fg_desde                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fg_hasta                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fc_desde                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fc_hasta                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fa_desde                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$fa_hasta                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$cod_cliente             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_sub_cliente         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_punto_servicio      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_planta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_maquinas            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_canales             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_formularios         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_divisas             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                   par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$rc_movimientos          OUT sys_refcursor,
                                   par$rc_formularios          OUT sys_refcursor,
                                   par$rc_cuentas              OUT sys_refcursor,
                                   par$rc_terminos             OUT sys_refcursor,
                                   par$rc_divisas              OUT sys_refcursor,
                                   par$rc_denominaciones       OUT sys_refcursor,
                                   par$rc_efectivos            OUT sys_refcursor,
                                   par$rc_mediopagos           OUT sys_refcursor,
                                   par$validaciones            OUT sys_refcursor,
                                   par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_movimientos';

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
      var$consulta           gepr_pcomon_###VERSION###.tipo$obs_;
      var$oid_delegacion     gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_planta         gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_cliente        gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_sub_cliente    gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_punto_servicio gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_canales        gepr_pcomon_###VERSION###.tipo$oids_;
      var$oid_formularios    gepr_pcomon_###VERSION###.tipo$oids_;
      var$oid_sector         gepr_pcomon_###VERSION###.tipo$obs_;
      var$pag_indice         gepr_pcomon_###VERSION###.tipo$nel_;
      var$pag_reg_pagina     gepr_pcomon_###VERSION###.tipo$nel_;
      var$consulta_cuenta    clob;
      var$filtro_cuenta      clob;

    BEGIN

      /* #### Inicializar los cursores #### */
      OPEN par$rc_movimientos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_formularios FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_cuentas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_terminos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_divisas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_denominaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_efectivos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_mediopagos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

      /* Limpiar tabla temporaria */
      DELETE SAPR_GTT_TOIDS;
      var$deletes := var$deletes + sql%ROWCOUNT;

      /* Validar codigo Ajeno e preencher tabela temporaria */
      svalidar_codigos(par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                       par$cod_cliente             => par$cod_cliente,
                       par$cod_sub_cliente         => par$cod_sub_cliente,
                       par$cod_punto_servicio      => par$cod_punto_servicio,
                       par$cod_delegacion          => par$cod_delegacion,
                       par$cod_planta              => par$cod_planta,
                       par$cod_maquinas            => par$cod_maquinas,
                       par$cod_canales             => par$cod_canales,
                       par$cod_formularios         => par$cod_formularios,
                       par$cod_divisas             => par$cod_divisas,
             par$oid_sector              => var$oid_sector,
                       par$oid_cliente             => var$oid_cliente,
                       par$oid_sub_cliente         => var$oid_sub_cliente,
                       par$oid_punto_servicio      => var$oid_punto_servicio,
                       par$oid_delegacion          => var$oid_delegacion,
                       par$oid_planta              => var$oid_planta,
                       par$oid_canales             => var$oid_canales,
                       par$oid_formularios         => var$oid_formularios,
                       par$selects                 => var$selects,
                       par$cod_cultura             => par$cod_cultura,
                       par$validaciones            => var$validaciones);

      /* SI NO HAY ERRORES, EJECUTA LAS CONSULTAS */
      IF gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN

        /* MONTAR FILTRO PRINCIPAL */
        IF par$pag_reg_pagina IS NULL THEN
            var$consulta := 'SELECT DOCU.OID_DOCUMENTO
                              , rank() over(order by DOCU.GMT_CREACION, DOCU.OID_DOCUMENTO) rank_
                           FROM SAPR_TDOCUMENTO DOCU';
        ELSE
            var$consulta := 'SELECT DOCU.OID_DOCUMENTO
                              , 1 rank_
                           FROM SAPR_TDOCUMENTO DOCU';
        END IF;  

        /* INNER JOIN PARA CUENTAS */ /* INNER JOIN PARA MAQUINAS */
        IF var$oid_cliente IS NOT NULL OR
           var$oid_sub_cliente IS NOT NULL OR
           var$oid_punto_servicio IS NOT NULL OR
           var$oid_delegacion IS NOT NULL OR
           var$oid_planta IS NOT NULL OR
           (var$oid_canales IS NOT NULL AND var$oid_canales.COUNT > 0) OR
           (par$cod_maquinas IS NOT NULL AND par$cod_maquinas.COUNT > 1) THEN

           var$consulta := var$consulta || ' INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_ORIGEN ';
           var$consulta := var$consulta || ' INNER JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = CUEN.OID_SECTOR ';

           IF var$oid_canales IS NOT NULL AND var$oid_canales.COUNT > 0 THEN
              var$consulta := var$consulta || '  LEFT JOIN GEPR_TSUBCANAL SUBC ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL ';
           END IF;

           IF var$oid_delegacion IS NOT NULL OR var$oid_planta IS NOT NULL THEN
             var$consulta := var$consulta || ' INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = CUEN.OID_SECTOR ';
             var$consulta := var$consulta || ' INNER JOIN GEPR_TPLANTA PLANT ON PLANT.OID_PLANTA = SECT.OID_PLANTA ';
           END IF;

           /* INNER JOIN PARA MAQUINAS */
           IF par$cod_maquinas IS NOT NULL AND par$cod_maquinas.COUNT > 1 THEN
             var$consulta := var$consulta || q'[ INNER JOIN SAPR_GTT_TOIDS TMAQ ON TMAQ.COD_CALIFICADOR = 'OID_SECTOR' AND TMAQ.OID = CUEN.OID_SECTOR ]';
           END IF;

        END IF;

        /* INNER JOIN PARA VALORES */
        IF (par$cod_divisas IS NOT NULL AND par$cod_divisas.COUNT > 1) OR par$filtro_disponible IS NOT NULL THEN
          var$consulta := var$consulta || ' LEFT JOIN SAPR_TTRANSACCION_EFECTIVO TREF ON TREF.OID_DOCUMENTO = DOCU.OID_DOCUMENTO ';
          IF (par$cod_divisas IS NOT NULL AND par$cod_divisas.COUNT > 1) THEN
             var$consulta := var$consulta || ' INNER JOIN SAPR_TEFECTIVOXDOCUMENTO TEXD ON TEXD.OID_DOCUMENTO = DOCU.OID_DOCUMENTO ';
             var$consulta := var$consulta || q'[ INNER JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = TEXD.OID_DIVISA ]';
          END IF;
        END IF;

        var$consulta := var$consulta || ' LEFT JOIN SAPR_TPERIODOXDOCUMENTO PEDO ON PEDO.OID_DOCUMENTO = DOCU.OID_DOCUMENTO ';
        var$consulta := var$consulta || ' LEFT JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PEDO.OID_PERIODO ';
        var$consulta := var$consulta || q'[ LEFT JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' ]';
        var$consulta := var$consulta || q'[ LEFT JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO ]';
        var$consulta := var$consulta || q'[ LEFT JOIN SAPR_TACREDITACION ACRE ON ACRE.OID_ACREDITACION  = PERI.OID_ACREDITACION ]';



    var$consulta := var$consulta || ' WHERE 1 = 1  AND DOCU.GMT_CREACION > ADD_MONTHS(SYSDATE , -6)';
        var$filtro_cuenta := ' WHERE  1 = 1 ';

       /* INNER JOIN PARA ACREDITADO */
        IF par$filtro_acreditado IS NOT NULL THEN
          IF par$filtro_acreditado = 1 THEN

             var$consulta := var$consulta || q'[  AND ESPE.COD_ESTADO_PERIODO = 'AC' ]';
          ELSIF par$filtro_acreditado = 0 THEN
             var$consulta := var$consulta || q'[ AND ESPE.COD_ESTADO_PERIODO <> 'AC' ]';
          END IF;
        END IF;




       /* FECHA GESTION */
        IF par$fg_desde IS NOT NULL OR par$fg_hasta IS NOT NULL THEN
          var$consulta := var$consulta || ' AND (';
          IF par$fg_desde IS NOT NULL THEN
            var$consulta := var$consulta || q'[ DOCU.FYH_GESTION >= TO_DATE(']' || TO_CHAR(par$fg_desde, 'DD/MM/YYYY HH24:MI:SS') || q'[', 'DD/MM/YYYY HH24:MI:SS') ]';
          END IF;
          IF par$fg_desde IS NOT NULL AND par$fg_hasta IS NOT NULL THEN
             var$consulta := var$consulta || ' AND ';
          END IF;
          IF par$fg_hasta IS NOT NULL THEN
            var$consulta := var$consulta || q'[ DOCU.FYH_GESTION <= TO_DATE(']' || TO_CHAR(par$fg_hasta, 'DD/MM/YYYY HH24:MI:SS') || q'[', 'DD/MM/YYYY HH24:MI:SS') ]';
          END IF;
          var$consulta := var$consulta || ')';
        END IF;

        /* FECHA CREACION */
        IF par$fc_desde IS NOT NULL OR par$fc_hasta IS NOT NULL THEN
          var$consulta := var$consulta || ' AND (';
          IF par$fc_desde IS NOT NULL THEN
            var$consulta := var$consulta || q'[ SYS_EXTRACT_UTC(DOCU.GMT_CREACION) >= TO_TIMESTAMP(']' || TO_CHAR(par$fc_desde, 'yyyy-mm-dd hh24:mi:ss') || q'[', 'yyyy-mm-dd hh24:mi:ss') ]';
          END IF;
          IF par$fc_desde IS NOT NULL AND par$fc_hasta IS NOT NULL THEN
             var$consulta := var$consulta || ' AND ';
          END IF;
          IF par$fc_hasta IS NOT NULL THEN
            var$consulta := var$consulta || q'[ SYS_EXTRACT_UTC(DOCU.GMT_CREACION) <= TO_TIMESTAMP(']' || TO_CHAR(par$fc_hasta, 'yyyy-mm-dd hh24:mi:ss') || q'[', 'yyyy-mm-dd hh24:mi:ss') ]';
          END IF;
          var$consulta := var$consulta || ')';
        END IF;

            /* FECHA ACREDITACION */
        IF par$fa_desde IS NOT NULL OR par$fa_hasta IS NOT NULL THEN
          var$consulta := var$consulta || ' AND (';
          IF par$fa_desde IS NOT NULL THEN
            var$consulta := var$consulta || q'[ DOCU.FYH_ACREDITACION >= TO_TIMESTAMP(']' || TO_CHAR(par$fa_desde, 'yyyy-mm-dd hh24:mi:ss') || q'[', 'yyyy-mm-dd hh24:mi:ss') ]';
          END IF;
          IF par$fa_desde IS NOT NULL AND par$fa_hasta IS NOT NULL THEN
             var$consulta := var$consulta || ' AND ';
          END IF;
          IF par$fa_hasta IS NOT NULL THEN
            var$consulta := var$consulta || q'[ DOCU.FYH_ACREDITACION <= TO_TIMESTAMP(']' || TO_CHAR(par$fa_hasta, 'yyyy-mm-dd hh24:mi:ss') || q'[', 'yyyy-mm-dd hh24:mi:ss') ]';
          END IF;
          var$consulta := var$consulta || ')';
        END IF;



          /*MAQUINA VIGENTE*/
          IF par$filtro_maqu_vigente = 1 THEN
            var$consulta := var$consulta || ' AND MAQU.BOL_ACTIVO = 1 ';
            var$filtro_cuenta := var$filtro_cuenta || ' AND MAQU.BOL_ACTIVO = 1 ';
          ELSIF par$filtro_maqu_vigente = 0 THEN
            var$consulta := var$consulta || ' AND MAQU.BOL_ACTIVO = 0 ';
            var$filtro_cuenta := var$filtro_cuenta || ' AND MAQU.BOL_ACTIVO = 0 ';
          END IF;


        /* COD_CLIENTE */
        IF var$oid_cliente IS NOT NULL THEN
          var$consulta := var$consulta || q'[  AND CUEN.OID_CLIENTE = ']' || var$oid_cliente || q'[' ]';
          var$filtro_cuenta := var$filtro_cuenta || q'[ AND  CLIE.OID_CLIENTE = ']' || var$oid_cliente || q'[' ]';
        END IF;

        /* COD_SUBCLIENTE */
        IF var$oid_sub_cliente IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND CUEN.OID_SUBCLIENTE = ']' || var$oid_sub_cliente || q'[' ]';
          var$filtro_cuenta := var$filtro_cuenta || q'[ AND  SUCL.OID_SUBCLIENTE = ']' || var$oid_sub_cliente || q'[' ]';
        END IF;

        /* COD_PTOSERVICO */
        IF var$oid_punto_servicio IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND CUEN.OID_PTO_SERVICIO = ']' || var$oid_punto_servicio || q'[' ]';
          var$filtro_cuenta := var$filtro_cuenta || q'[ AND  PUSE.OID_PTO_SERVICIO = ']' || var$oid_punto_servicio || q'[' ]';
        END IF;

        /* OID_FORMULARIO */
        IF var$oid_formularios IS NOT NULL AND var$oid_formularios.COUNT > 0 THEN

          var$consulta := var$consulta || ' AND DOCU.OID_FORMULARIO IN (';
          FOR idx IN var$oid_formularios.first .. var$oid_formularios.last LOOP
            IF idx > 0 THEN
              var$consulta := var$consulta || ', ';
            END IF;
            var$consulta := var$consulta || q'[']' || var$oid_formularios(idx) || q'[']';
          END LOOP;
          var$consulta := var$consulta || ')';

        END IF;

        /* OID_CANAL */
        IF var$oid_canales IS NOT NULL AND var$oid_canales.COUNT > 0 THEN

          var$consulta := var$consulta || ' AND SUBC.OID_CANAL IN (';
          var$filtro_cuenta := var$filtro_cuenta || ' AND CANA.OID_CANAL IN (';

          FOR idx IN var$oid_canales.first .. var$oid_canales.last LOOP
            IF idx > 0 THEN
              var$consulta := var$consulta || ', ';
              var$filtro_cuenta := var$filtro_cuenta || ', ';
            END IF;
            var$consulta := var$consulta || q'[']' || var$oid_canales(idx) || q'[']';
            var$filtro_cuenta := var$filtro_cuenta || q'[']' || var$oid_canales(idx) || q'[']';
          END LOOP;
          var$consulta := var$consulta || ')';
          var$filtro_cuenta := var$filtro_cuenta || ')';
        END IF;

        /* VALORES DISPONIBLES */
        IF par$filtro_disponible = 1 THEN
          var$consulta := var$consulta || ' AND TREF.BOL_DISPONIBLE = 1 ';
        ELSIF par$filtro_disponible = 0 THEN
          var$consulta := var$consulta || ' AND TREF.BOL_DISPONIBLE = 0 ';
        END IF;

        /* OID_DELEGACION */
        IF var$oid_delegacion IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND PLANT.OID_DELEGACION = ']' || var$oid_delegacion || q'[' ]';
          var$filtro_cuenta := var$filtro_cuenta || q'[ AND DELE.OID_DELEGACION = ']' || var$oid_delegacion || q'[' ]';
        END IF;

        /* OID_PLANTA */
        IF var$oid_planta IS NOT NULL THEN
          var$consulta := var$consulta || q'[ AND PLANT.OID_PLANTA = ']' || var$oid_planta || q'[' ]';
          var$filtro_cuenta := var$filtro_cuenta || q'[ AND PLAN.OID_PLANTA = ']' || var$oid_planta || q'[' ]';
        END IF;

        /* BOL_NOTIFICADO */
        IF par$filtro_notificado IS NOT NULL THEN
          IF par$filtro_notificado = 1 THEN
             var$consulta := var$consulta || ' AND DOCU.BOL_NOTIFICADO = 1';
          ELSE
             var$consulta := var$consulta || ' AND DOCU.BOL_NOTIFICADO = 0';
          END IF;
        END IF;

        /* SI TENGO INNER JOIN CON LA TABLA SAPR_TTRANSACCION NECESITO DEL GROUP BY */
        IF (par$cod_divisas IS NOT NULL AND par$cod_divisas.COUNT > 1) OR par$filtro_disponible IS NOT NULL THEN
           var$consulta := var$consulta || ' GROUP BY DOCU.OID_DOCUMENTO, DOCU.GMT_CREACION ';
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

           var$consulta := ' SELECT * FROM ( ' || var$consulta || ' ) WHERE RANK_ BETWEEN ' || var$pag_indice || ' AND ' || var$pag_reg_pagina || ' ';
        END IF;

        /* MONTAR INSERT A TABLA TEMPORARIA */
        var$consulta := 'INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR)
                            WITH DOCUMENTOS AS ( ' || var$consulta || q'[ )
                            SELECT D.OID_DOCUMENTO, 'OID_DOCUMENTO'
                              FROM DOCUMENTOS D ]';

        /* EJECUTAR FILTRO PRINCIPAL */
        EXECUTE IMMEDIATE var$consulta;

        /* CARGAR DATOS DEL LOS MOVIMIENTOS */
        OPEN par$rc_movimientos FOR
                SELECT DOCU.OID_DOCUMENTO
                 , DOCU.COD_EXTERNO AS CODIGO
                 , to_char(DOCU.FYH_GESTION, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00'  AS FYH_GESTION
                 , to_char(DOCU.gmt_creacion, 'YYYY-MM-DD HH24:MI:SS TZR') AS FECHA_REALIZACION
                 , DOCU.OID_CUENTA_ORIGEN
                 , DOCU.OID_CUENTA_DESTINO
                 , DOCU.OID_FORMULARIO                
                 , DOCU.BOL_ACREDITADO
                 , DOCU.BOL_NOTIFICADO
                 , CASE WHEN par$filtro_collectionid = 1 THEN DOCU.COD_COLLECTION_ID ELSE '' END AS COD_COLLECTION_ID  
                 , CASE WHEN  DOCU.FYH_ACREDITACION is null THEN ''   ELSE to_char(DOCU.FYH_ACREDITACION, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00'  END AS FYH_ACREDITACION
              FROM SAPR_TDOCUMENTO DOCU
        INNER JOIN SAPR_GTT_TOIDS T ON T.OID = DOCU.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
         LEFT JOIN SAPR_TPERIODOXDOCUMENTO PEDO ON PEDO.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
        LEFT JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PEDO.OID_PERIODO
        LEFT JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
        LEFT JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO
        LEFT JOIN SAPR_TACREDITACION ACRE ON ACRE.OID_ACREDITACION = PERI.OID_ACREDITACION
    union
            SELECT  NULL, NULL, NULL, NULL , NULL, NULL, NULL, 0, 0, NULL, NULL
              FROM DUAL ;

        /* CARGAR DATOS DEL LOS FORMULARIOS */
        OPEN par$rc_formularios FOR
            SELECT FORM.OID_FORMULARIO
                 , FORM.COD_FORMULARIO
                 , FORM.DES_FORMULARIO
              FROM SAPR_TDOCUMENTO DOCU
        INNER JOIN SAPR_GTT_TOIDS T ON T.OID = DOCU.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
        INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = DOCU.OID_FORMULARIO
          GROUP BY FORM.OID_FORMULARIO
                 , FORM.COD_FORMULARIO
                 , FORM.DES_FORMULARIO;

        /* CARGAR DATOS DEL LOS TERMINOS */
        IF par$fltro_info_adic = 1 THEN
          OPEN par$rc_terminos FOR
              SELECT TEDO.OID_DOCUMENTO
                   , TERM.COD_TERMINO
                   , TEDO.DES_VALOR
                FROM SAPR_TVALOR_TERMINOXDOCUMENTO TEDO
          INNER JOIN SAPR_GTT_TOIDS T ON T.OID = TEDO.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
          INNER JOIN GEPR_TTERMINO TERM ON TERM.OID_TERMINO = TEDO.OID_TERMINO;

        ELSE
          OPEN par$rc_terminos FOR
              SELECT NULL OID_TERMINO
                FROM DUAL
                WHERE 1 <> 1;
        END IF;

        /* CARGAR DATOS DEL LOS VALORES */
        OPEN par$rc_efectivos FOR
           WITH EFECTIVOS AS (
                  SELECT TREF.OID_DOCUMENTO
                       , TREF.OID_DIVISA
                       , TREF.OID_DENOMINACION
                       , TREF.COD_NIVEL_DETALLE
                       , TREF.BOL_DISPONIBLE
                       , TREF.NUM_IMPORTE
                       , TREF.NEL_CANTIDAD
                    FROM SAPR_TTRANSACCION_EFECTIVO TREF
              INNER JOIN SAPR_GTT_TOIDS T ON T.OID = TREF.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'              
               LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = TREF.OID_DIVISA
          )          
            SELECT TREF.OID_DOCUMENTO
                 , TREF.OID_DIVISA
                 , NULL OID_DENOMINACION
                 , 'T' COD_NIVEL_DETALLE
                 , TREF.BOL_DISPONIBLE
                 , SUM(TREF.NUM_IMPORTE) NUM_IMPORTE
                 , NULL NEL_CANTIDAD
              FROM EFECTIVOS TREF
             WHERE par$filtro_disponible IS NULL OR TREF.BOL_DISPONIBLE = par$filtro_disponible
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
                AND par$filtro_val_detallar = 1
                AND TREF.COD_NIVEL_DETALLE = 'D'
                AND (par$filtro_disponible IS NULL OR TREF.BOL_DISPONIBLE = par$filtro_disponible)
         UNION ALL 
           SELECT EFDO.OID_DOCUMENTO
                    , EFDO.OID_DIVISA  
                    , EFDO.OID_DENOMINACION
                    , EFDO.COD_NIVEL_DETALLE
                    , NULL BOL_DISPONIBLE
                    , SUM(EFDO.NUM_IMPORTE) NUM_IMPORTE
                    , SUM(EFDO.NEL_CANTIDAD) NEL_CANTIDAD
                 FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
           INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = EFDO.OID_DIVISA
           INNER JOIN SAPR_GTT_TOIDS T ON T.OID = EFDO.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'    
                WHERE par$filtro_valores_inform = 1 and EFDO.OID_DOCUMENTO NOT IN (SELECT OID_DOCUMENTO FROM EFECTIVOS)
                AND EFDO.NUM_IMPORTE <> 0 and EFDO.COD_NIVEL_DETALLE = 'D'
             GROUP BY EFDO.OID_DOCUMENTO, EFDO.OID_DIVISA, EFDO.OID_DENOMINACION, EFDO.COD_NIVEL_DETALLE       
             
                UNION ALL 
           SELECT EFDO.OID_DOCUMENTO
                    , EFDO.OID_DIVISA  
                    , NULL OID_DENOMINACION
                    , 'T' COD_NIVEL_DETALLE
                    , NULL BOL_DISPONIBLE
                    , SUM(EFDO.NUM_IMPORTE) NUM_IMPORTE
                    , NULL NEL_CANTIDAD
                 FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
           INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = EFDO.OID_DIVISA
           INNER JOIN SAPR_GTT_TOIDS T ON T.OID = EFDO.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'    
                WHERE par$filtro_valores_inform = 1 and EFDO.OID_DOCUMENTO NOT IN (SELECT OID_DOCUMENTO FROM EFECTIVOS)
                AND EFDO.NUM_IMPORTE <> 0
             GROUP BY EFDO.OID_DOCUMENTO, EFDO.OID_DIVISA
                ;

        OPEN par$rc_mediopagos FOR
            SELECT NULL OID_VALOR
              FROM DUAL
              WHERE 1 <> 1;
     

        IF par$cod_identificador_ajeno IS NULL THEN

              var$consulta_cuenta := q'[
                   WITH CUENTAS AS (
                      SELECT DOCU.OID_CUENTA_ORIGEN OID_CUENTA
                        FROM SAPR_TDOCUMENTO DOCU
                  INNER JOIN SAPR_GTT_TOIDS T ON T.OID = DOCU.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       UNION
                      SELECT DOCU.OID_CUENTA_DESTINO OID_CUENTA
                        FROM SAPR_TDOCUMENTO DOCU
                  INNER JOIN SAPR_GTT_TOIDS T ON T.OID = DOCU.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                   )
                      SELECT
                        DISTINCT
                      NVL(CUEN.OID_CUENTA         , AUX.OID_CUENTA) OID_CUENTA
                           , NVL(CLIE.COD_CLIENTE, AUX.COD_CLIENTE) COD_CLIENTE
                           , NVL(CLIE.DES_CLIENTE, AUX.DES_CLIENTE) DES_CLIENTE
                           , NVL(SUBC.COD_SUBCLIENTE, AUX.COD_SUBCLIENTE) COD_SUBCLIENTE
                           , NVL(SUBC.DES_SUBCLIENTE, AUX.DES_SUBCLIENTE) DES_SUBCLIENTE
                           , NVL(PTOS.COD_PTO_SERVICIO, AUX.COD_PTO_SERVICIO) COD_PTO_SERVICIO
                           , NVL(PTOS.DES_PTO_SERVICIO, AUX.DES_PTO_SERVICIO) DES_PTO_SERVICIO
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
                           , NVL(MAQU.COD_IDENTIFICACION, AUX.COD_IDENTIFICACION) COD_IDENTIFICACION
                           , NVL(MAQU.BOL_ACTIVO, AUX.BOL_ACTIVO) BOL_ACTIVO
                        FROM CUENTAS
                 INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = CUENTAS.OID_CUENTA
                 INNER JOIN GEPR_TCLIENTE CLIE ON CLIE.OID_CLIENTE = CUEN.OID_CLIENTE
                  LEFT JOIN GEPR_TSUBCLIENTE SUBC ON SUBC.OID_SUBCLIENTE = CUEN.OID_SUBCLIENTE
                  LEFT JOIN GEPR_TPUNTO_SERVICIO PTOS ON PTOS.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO
                 INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = CUEN.OID_SECTOR
                 INNER JOIN GEPR_TPLANTA PLANT ON PLANT.OID_PLANTA = SECT.OID_PLANTA
                 INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = PLANT.OID_DELEGACION
                 INNER JOIN GEPR_TSUBCANAL SUBCN ON SUBCN.OID_SUBCANAL = CUEN.OID_SUBCANAL
                 INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = SUBCN.OID_CANAL
                  LEFT JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = SECT.OID_SECTOR
                 FULL OUTER JOIN
                 (
                        SELECT
                              NULL OID_CUENTA
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
                              LEFT JOIN GEPR_TDIVISA DIVI ON SAEF.OID_DIVISA = DIVI.OID_DIVISA
                              CROSS JOIN GEPR_TCANAL CANA
                              INNER JOIN GEPR_TSUBCANAL SUCA ON CANA.OID_CANAL = SUCA.OID_CANAL
                              ]' || var$filtro_cuenta || var$oid_sector ||
                             q'[
                              GROUP BY
                          COD_SECTOR, DES_SECTOR, COD_CANAL, DES_CANAL, COD_SUBCANAL, DES_SUBCANAL, COD_CLIENTE
                        , DES_CLIENTE, COD_SUBCLIENTE, DES_SUBCLIENTE, COD_PTO_SERVICIO, DES_PTO_SERVICIO, COD_PLANTA
                        , DES_PLANTA, COD_DELEGACION, DES_DELEGACION, MAQU.OID_SECTOR
                        , MAQU.COD_IDENTIFICACION
                        , MAQU.BOL_ACTIVO,DIVI.COD_ISO_DIVISA,DIVI.DES_DIVISA,SAEF.BOL_DISPONIBLE
                 ) AUX
                  ON AUX.OID_SECTOR = SECT.OID_SECTOR and AUX.COD_CANAL = CANA.COD_CANAL and AUX.COD_SUBCANAL = SUBCN.COD_SUBCANAL  ]';




              /* CARGAR DATOS DEL LAS CUENTAS */

              OPEN par$rc_cuentas FOR
                 var$consulta_cuenta;


              /* CARGAR DATOS DE LAS DIVISAS */
              OPEN par$rc_divisas FOR
                  WITH DIVISAS AS (
                          SELECT TREF.OID_DIVISA
                            FROM SAPR_TTRANSACCION_EFECTIVO TREF
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = TREF.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = TREF.OID_DIVISA
                        GROUP BY TREF.OID_DIVISA
                           UNION
                          SELECT TRMP.OID_DIVISA
                            FROM SAPR_TTRANSACCION_MEDIO_PAGO TRMP
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = TRMP.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = TRMP.OID_DIVISA
                       UNION 
                       SELECT EFDO.OID_DIVISA
                            FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = EFDO.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = EFDO.OID_DIVISA
                       
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
                            FROM SAPR_TTRANSACCION_EFECTIVO TREF
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = TREF.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = TREF.OID_DIVISA
                           WHERE TREF.OID_DENOMINACION IS NOT NULL
                        GROUP BY TREF.OID_DIVISA
                               , TREF.OID_DENOMINACION
                               UNION 
                               
                                 SELECT EFDO.OID_DIVISA
                               , EFDO.OID_DENOMINACION
                            FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = EFDO.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = EFDO.OID_DIVISA
                           WHERE EFDO.OID_DENOMINACION IS NOT NULL
                        GROUP BY EFDO.OID_DIVISA
                               , EFDO.OID_DENOMINACION  
                  )
                    SELECT D.OID_DENOMINACION
                         , DEN.COD_DENOMINACION
                         , DEN.DES_DENOMINACION
                      FROM DENOMINACIONES D
                INNER JOIN GEPR_TDENOMINACION DEN ON DEN.OID_DIVISA = D.OID_DIVISA AND DEN.OID_DENOMINACION = D.OID_DENOMINACION;

        ELSE
      var$consulta_cuenta := q'[ WITH CUENTAS AS (
                      SELECT DOCU.OID_CUENTA_ORIGEN OID_CUENTA
                        FROM SAPR_TDOCUMENTO DOCU
                  INNER JOIN SAPR_GTT_TOIDS T ON T.OID = DOCU.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       UNION
                      SELECT DOCU.OID_CUENTA_DESTINO OID_CUENTA
                        FROM SAPR_TDOCUMENTO DOCU
                  INNER JOIN SAPR_GTT_TOIDS T ON T.OID = DOCU.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                   )
                     SELECT
                             DISTINCT
                             NVL(CUEN.OID_CUENTA, AUX.OID_CUENTA) OID_CUENTA
                           , NVL(CAC.COD_AJENO , AUX.COD_CLIENTE) COD_CLIENTE
                           , NVL(CAC.DES_AJENO , AUX.DES_CLIENTE) DES_CLIENTE
                           , NVL(CASC.COD_AJENO , AUX.COD_SUBCLIENTE) COD_SUBCLIENTE
                           , NVL(CASC.DES_AJENO , AUX.DES_SUBCLIENTE) DES_SUBCLIENTE
                           , NVL(CAPS.COD_AJENO , AUX.COD_PTO_SERVICIO) COD_PTO_SERVICIO
                           , NVL(CAPS.DES_AJENO , AUX.DES_PTO_SERVICIO) DES_PTO_SERVICIO
                           , NVL(CAD.COD_AJENO , AUX.COD_DELEGACION) COD_DELEGACION
                           , NVL(CAD.DES_AJENO , AUX.DES_DELEGACION) DES_DELEGACION
                           , NVL(CAP.COD_AJENO , AUX.COD_PLANTA) COD_PLANTA
                           , NVL(CAP.DES_AJENO , AUX.DES_PLANTA) DES_PLANTA
                           , NVL(CAS.COD_AJENO , AUX.COD_SECTOR) COD_SECTOR
                           , NVL(CAS.DES_AJENO, AUX.DES_SECTOR) DES_SECTOR
                           , NVL(CACAN.COD_AJENO, AUX.COD_CANAL) COD_CANAL
                           , NVL(CACAN.DES_AJENO, AUX.DES_CANAL) DES_CANAL
                           , NVL(CASCA.COD_AJENO, AUX.COD_SUBCANAL) COD_SUBCANAL
                           , NVL(CASCA.DES_AJENO, AUX.DES_SUBCANAL) DES_SUBCANAL
                           , NVL(MAQU.COD_IDENTIFICACION, AUX.COD_IDENTIFICACION) COD_IDENTIFICACION
                           , NVL(MAQU.BOL_ACTIVO, AUX.BOL_ACTIVO) BOL_ACTIVO
                        FROM CUENTAS
                 INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = CUENTAS.OID_CUENTA
                 INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = CUEN.OID_SECTOR
                 INNER JOIN GEPR_TPLANTA PLANT ON PLANT.OID_PLANTA = SECT.OID_PLANTA
                 INNER JOIN GEPR_TSUBCANAL SUBCN ON SUBCN.OID_SUBCANAL = CUEN.OID_SUBCANAL
                  LEFT JOIN SAPR_TMAQUINA MAQU ON MAQU.OID_SECTOR = SECT.OID_SECTOR
                 INNER JOIN GEPR_TCODIGO_AJENO CAS
                    ON CAS.OID_TABLA_GENESIS = CUEN.OID_SECTOR AND CAS.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAS.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
                 INNER JOIN GEPR_TCODIGO_AJENO CAP
                    ON CAP.OID_TABLA_GENESIS = PLANT.OID_PLANTA AND CAP.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAP.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
                 INNER JOIN GEPR_TCODIGO_AJENO CAD
                    ON CAD.OID_TABLA_GENESIS = PLANT.OID_DELEGACION AND CAD.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAD.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'

                 INNER JOIN GEPR_TCODIGO_AJENO CAC
                    ON CAC.OID_TABLA_GENESIS = CUEN.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                  LEFT JOIN GEPR_TCODIGO_AJENO CASC
                    ON CASC.OID_TABLA_GENESIS = CUEN.OID_SUBCLIENTE AND CASC.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CASC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                  LEFT JOIN GEPR_TCODIGO_AJENO CAPS
                    ON CAPS.OID_TABLA_GENESIS = CUEN.OID_PTO_SERVICIO AND CAPS.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAPS.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'

                 INNER JOIN GEPR_TCODIGO_AJENO CACAN
                    ON CACAN.OID_TABLA_GENESIS = SUBCN.OID_CANAL AND CACAN.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CACAN.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
                 INNER JOIN GEPR_TCODIGO_AJENO CASCA
                    ON CASCA.OID_TABLA_GENESIS = SUBCN.OID_SUBCANAL AND CASCA.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CASCA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
           FULL OUTER JOIN
                 (
                        SELECT
                        NULL OID_CUENTA
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
                        , CAP.COD_AJENO AS COD_PLANTA
                        , CAP.DES_AJENO AS DES_PLANTA
                        , CAD.COD_AJENO AS COD_DELEGACION
                        , CAD.DES_AJENO AS DES_DELEGACION
                        , MAQU.OID_SECTOR
                        , MAQU.COD_IDENTIFICACION
                        , MAQU.BOL_ACTIVO
                        , DIVI.COD_ISO_DIVISA
                        , DIVI.DES_DIVISA
                        , 0 IMPORTE
                        , SAEF.BOL_DISPONIBLE
                            FROM
                              SAPR_TMAQUINA MAQU
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
                              LEFT JOIN GEPR_TDIVISA DIVI ON SAEF.OID_DIVISA = DIVI.OID_DIVISA
                              CROSS JOIN GEPR_TCANAL CANA
                              INNER JOIN GEPR_TSUBCANAL SUCA ON CANA.OID_CANAL = SUCA.OID_CANAL
                               INNER JOIN GEPR_TCODIGO_AJENO CAS
                          ON CAS.OID_TABLA_GENESIS = SECT.OID_SECTOR AND CAS.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAS.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
                       INNER JOIN GEPR_TCODIGO_AJENO CAP
                          ON CAP.OID_TABLA_GENESIS = PLAN.OID_PLANTA AND CAP.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAP.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
                       INNER JOIN GEPR_TCODIGO_AJENO CAD
                          ON CAD.OID_TABLA_GENESIS = PLAN.OID_DELEGACION AND CAD.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAD.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'

                       INNER JOIN GEPR_TCODIGO_AJENO CAC
                          ON CAC.OID_TABLA_GENESIS = CLIE.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                        LEFT JOIN GEPR_TCODIGO_AJENO CASC
                          ON CASC.OID_TABLA_GENESIS = SUCL.OID_SUBCLIENTE AND CASC.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CASC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                        LEFT JOIN GEPR_TCODIGO_AJENO CAPS
                          ON CAPS.OID_TABLA_GENESIS = PUSE.OID_PTO_SERVICIO AND CAPS.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CAPS.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'

                       INNER JOIN GEPR_TCODIGO_AJENO CACAN
                          ON CACAN.OID_TABLA_GENESIS = SUCA.OID_CANAL AND CACAN.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CACAN.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
                       INNER JOIN GEPR_TCODIGO_AJENO CASCA
                          ON CASCA.OID_TABLA_GENESIS = SUCA.OID_SUBCANAL AND CASCA.COD_IDENTIFICADOR = ']' || par$cod_identificador_ajeno || q'[' AND CASCA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'

                                ]' || var$filtro_cuenta || var$oid_sector || q'[

                          GROUP BY
                          CAS.COD_AJENO, CAS.DES_AJENO, CACAN.COD_AJENO, CACAN.DES_AJENO, CASCA.COD_AJENO, CASCA.DES_AJENO, CAC.COD_AJENO
                        , CAC.DES_AJENO, CASC.COD_AJENO, CASC.DES_AJENO, CAPS.COD_AJENO, CAPS.DES_AJENO, CAP.COD_AJENO
                        , CAP.DES_AJENO, CAD.COD_AJENO, CAD.DES_AJENO, MAQU.OID_SECTOR
                        , MAQU.COD_IDENTIFICACION
                        , MAQU.BOL_ACTIVO,DIVI.COD_ISO_DIVISA,DIVI.DES_DIVISA,SAEF.BOL_DISPONIBLE

                 ) AUX
                     ON AUX.OID_SECTOR = SECT.OID_SECTOR and AUX.COD_CANAL = CACAN.COD_AJENO and AUX.COD_SUBCANAL = CASCA.COD_AJENO ]';

              /* CARGAR DATOS DEL LAS CUENTAS */
              OPEN par$rc_cuentas FOR
               var$consulta_cuenta;


              /* CARGAR DATOS DE LAS DIVISAS */
              OPEN par$rc_divisas FOR
                  WITH DIVISAS AS (
                          SELECT TREF.OID_DIVISA
                            FROM SAPR_TTRANSACCION_EFECTIVO TREF
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = TREF.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = TREF.OID_DIVISA
                        GROUP BY TREF.OID_DIVISA
                           UNION
                          SELECT TRMP.OID_DIVISA
                            FROM SAPR_TTRANSACCION_MEDIO_PAGO TRMP
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = TRMP.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = TRMP.OID_DIVISA
                            UNION 
                       SELECT EFDO.OID_DIVISA
                            FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = EFDO.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = EFDO.OID_DIVISA
                  )
                    SELECT D.OID_DIVISA
                         , DIV.COD_AJENO COD_ISO_DIVISA
                         , DIV.DES_AJENO DES_DIVISA
                      FROM DIVISAS D
                INNER JOIN GEPR_TCODIGO_AJENO DIV ON DIV.OID_TABLA_GENESIS = D.OID_DIVISA AND DIV.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND DIV.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA';

              /* CARGAR DATOS DE LAS DENOMINACIONES */
              OPEN par$rc_denominaciones FOR
                  WITH DENOMINACIONES AS (
                          SELECT TREF.OID_DIVISA
                               , TREF.OID_DENOMINACION
                            FROM SAPR_TTRANSACCION_EFECTIVO TREF
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = TREF.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = TREF.OID_DIVISA
                           WHERE TREF.OID_DENOMINACION IS NOT NULL
                        GROUP BY TREF.OID_DIVISA
                               , TREF.OID_DENOMINACION                               
                               UNION                                
                                 SELECT EFDO.OID_DIVISA
                               , EFDO.OID_DENOMINACION
                            FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
                      INNER JOIN SAPR_GTT_TOIDS T ON T.OID = EFDO.OID_DOCUMENTO AND T.COD_CALIFICADOR = 'OID_DOCUMENTO'
                       LEFT JOIN SAPR_GTT_TOIDS TDIV ON TDIV.COD_CALIFICADOR = 'OID_DIVISA' AND TDIV.OID = EFDO.OID_DIVISA
                           WHERE EFDO.OID_DENOMINACION IS NOT NULL
                        GROUP BY EFDO.OID_DIVISA
                               , EFDO.OID_DENOMINACION  
                               
                               
                  )
                    SELECT D.OID_DENOMINACION
                         , DEN.COD_AJENO COD_DENOMINACION
                         , DEN.DES_AJENO DES_DENOMINACION
                      FROM DENOMINACIONES D
                INNER JOIN GEPR_TCODIGO_AJENO DEN ON DEN.OID_TABLA_GENESIS = D.OID_DENOMINACION AND DEN.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND DEN.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION';

        END IF;
      ELSE
        /* PARA NO RETORNAR LOS CURSORES VACIOS */
        OPEN par$rc_movimientos FOR
            SELECT NULL OID_DOCUMENTO
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_formularios FOR
            SELECT NULL OID_FORMULARIO
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_cuentas FOR
            SELECT NULL OID_CUENTA
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_terminos FOR
            SELECT NULL OID_TERMINO
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_divisas FOR
            SELECT NULL OID_DIVISA
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_denominaciones FOR
            SELECT NULL OID_DIVISA
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_efectivos FOR
            SELECT NULL OID_VALOR
              FROM DUAL
              WHERE 1 <> 1;
        OPEN par$rc_mediopagos FOR
            SELECT NULL OID_VALOR
              FROM DUAL
              WHERE 1 <> 1;
      END IF;

      /* Validaciones */
      IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN

        var$tvalidaciones := gepr_putilidades_###VERSION###.fconverter_array_desc_table(par$table_varchar2 => var$validaciones);
        open par$validaciones for
          SELECT SUBSTR(C.COLUMN_VALUE, 0, 10) CODIGO,
                 SUBSTR(C.COLUMN_VALUE, 11) DESCRICION
            FROM TABLE(var$tvalidaciones) C
           WHERE C.COLUMN_VALUE IS NOT NULL;

      END IF;

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

  END srecuperar_movimientos;

   


  /* Validar Codigos */
  PROCEDURE svalidar_codigos(par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_cliente             IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_sub_cliente         IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_punto_servicio      IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_delegacion          IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_planta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_maquinas            IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_canales             IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_formularios         IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$cod_divisas             IN gepr_pcomon_###VERSION###.tipo$cods_,
               par$oid_sector              OUT gepr_pcomon_###VERSION###.tipo$obs_,
                             par$oid_cliente             OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_sub_cliente         OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_punto_servicio      OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_delegacion          OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_planta              OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_canales             OUT gepr_pcomon_###VERSION###.tipo$oids_,
                             par$oid_formularios         OUT gepr_pcomon_###VERSION###.tipo$oids_,
                             par$selects                 IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$validaciones            OUT gepr_pcomon_###VERSION###.tipo$descs_) IS

      var$existe gepr_pcomon_###VERSION###.tipo$cod_;

      /* MAQUINAS */
      CURSOR cur$maquinas IS
         SELECT T.OID COD_MAQUINA
              , S.OID_SECTOR
           FROM SAPR_GTT_TOIDS T
      LEFT JOIN SAPR_TMAQUINA M ON M.COD_IDENTIFICACION = T.OID
      LEFT JOIN GEPR_TSECTOR S ON S.OID_SECTOR = M.OID_SECTOR
          WHERE T.COD_CALIFICADOR = 'COD_MAQUINA';

      /* FORMULARIO */
      CURSOR cur$formularios IS
         SELECT T.OID COD_FORMULARIO
              , F.OID_FORMULARIO
           FROM SAPR_GTT_TOIDS T
      LEFT JOIN SAPR_TFORMULARIO F ON F.COD_FORMULARIO = T.OID
          WHERE T.COD_CALIFICADOR = 'COD_FORMULARIO';

      /* CANALES */
      CURSOR cur$canales IS
         SELECT T.OID COD_CANAL
              , C.OID_CANAL
           FROM SAPR_GTT_TOIDS T
      LEFT JOIN GEPR_TCANAL C ON C.COD_CANAL = T.OID
          WHERE T.COD_CALIFICADOR = 'COD_CANAL';

      CURSOR cur$canales_ajeno IS
         SELECT T.OID COD_CANAL
              , CA.OID_TABLA_GENESIS OID_CANAL
           FROM SAPR_GTT_TOIDS T
      LEFT JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_AJENO = T.OID
                                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
                                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                                     AND CA.BOL_ACTIVO = 1
          WHERE T.COD_CALIFICADOR = 'COD_CANAL';

      /* DIVISAS */
      CURSOR cur$divisas IS
         SELECT T.OID COD_DIVISA
              , D.OID_DIVISA
           FROM SAPR_GTT_TOIDS T
      LEFT JOIN GEPR_TDIVISA D ON D.COD_ISO_DIVISA = T.OID
          WHERE T.COD_CALIFICADOR = 'COD_DIVISA';

      CURSOR cur$divisas_ajeno IS
         SELECT T.OID COD_DIVISA
              , CA.OID_TABLA_GENESIS OID_DIVISA
           FROM SAPR_GTT_TOIDS T
      LEFT JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_AJENO = T.OID
                                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'
                                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                                     AND CA.BOL_ACTIVO = 1
          WHERE T.COD_CALIFICADOR = 'COD_DIVISA';

  BEGIN

      /* MAQUINAS */
      IF par$cod_maquinas IS NOT NULL AND par$cod_maquinas.COUNT > 1  THEN

         /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LAS MAQUINAS */
         FOR idx IN par$cod_maquinas.first .. par$cod_maquinas.last LOOP
           IF par$cod_maquinas(idx) IS NOT NULL THEN
               INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR)
                VALUES (par$cod_maquinas(idx), 'COD_MAQUINA');
           END IF;
         END LOOP;

         /* CANSULTA DOS CODIGOS Y HACE LAS VALIDACIONES NECESARIAS */
         FOR rec$maquina IN cur$maquinas LOOP
            IF rec$maquina.OID_SECTOR IS NULL THEN
               par$validaciones(par$validaciones.COUNT) := '2040020008' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020008',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                rec$maquina.cod_maquina,
                                                0);
            ELSE
        par$oid_sector := par$oid_sector || '''' || rec$maquina.OID_SECTOR || ''',' ;
                INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
                VALUES (rec$maquina.OID_SECTOR, 'OID_SECTOR');
            END IF;
         END LOOP rec$maquina;
     par$oid_sector :=  ' AND SECT.OID_SECTOR IN ( ' || SUBSTR( par$oid_sector, 1, LENGTH( par$oid_sector)-1 ) || ')';

      END IF;

      /* FORMULARIOS */
      IF par$cod_formularios IS NOT NULL AND par$cod_formularios.COUNT > 1 THEN

         /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS FORMULARIOS */
         FOR idx IN par$cod_formularios.first .. par$cod_formularios.last LOOP
           IF par$cod_formularios(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
              VALUES (par$cod_formularios(idx), 'COD_FORMULARIO');
           END IF;
         END LOOP;

         /* CANSULTA DOS CODIGOS Y HACE LAS VALIDACIONES NECESARIAS */
         FOR rec$formulario IN cur$formularios LOOP
            IF rec$formulario.OID_FORMULARIO IS NULL THEN
               par$validaciones(par$validaciones.COUNT) := '2040020009' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020009',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                rec$formulario.COD_FORMULARIO,
                                                0);
            ELSE
               par$oid_formularios(par$oid_formularios.COUNT) := rec$formulario.OID_FORMULARIO;
            END IF;
         END LOOP rec$formulario;

      END IF;

      /* CANALES */
      IF par$cod_canales IS NOT NULL AND par$cod_canales.COUNT > 1 THEN

         /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS CANALES */
         FOR idx IN par$cod_canales.first .. par$cod_canales.last LOOP
           IF par$cod_canales(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
              VALUES (par$cod_canales(idx), 'COD_CANAL');
           END IF;
         END LOOP;

         /* CANSULTA DOS CODIGOS Y HACE LAS VALIDACIONES NECESARIAS - POR CODIGO AJENO*/
         IF par$cod_identificador_ajeno IS NOT NULL THEN
             FOR rec$canal IN cur$canales_ajeno LOOP
                IF rec$canal.OID_CANAL IS NULL THEN
                   par$validaciones(par$validaciones.COUNT) := '2040020005' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                '2040020005',
                                                                                                'MARCARMOVIMIENTOS',
                                                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                                rec$canal.COD_CANAL || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TCANAL',
                                                                                                0);
                ELSE
                   par$oid_canales(par$oid_canales.COUNT) := rec$canal.OID_CANAL;
                END IF;
             END LOOP rec$canal;
         ELSE
             FOR rec$canal IN cur$canales LOOP
                IF rec$canal.OID_CANAL IS NULL THEN
                   par$validaciones(par$validaciones.COUNT) := '2040020010' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020010',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                rec$canal.COD_CANAL,
                                                0);
                ELSE
                   par$oid_canales(par$oid_canales.COUNT) := rec$canal.OID_CANAL;
                END IF;
             END LOOP rec$canal;
         END IF;

      END IF;

      /* DIVISAS */
      IF par$cod_divisas IS NOT NULL AND par$cod_divisas.COUNT > 1 THEN

         /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS COD_DIVISA */
         FOR idx IN par$cod_divisas.first .. par$cod_divisas.last LOOP
           IF par$cod_divisas(idx) IS NOT NULL THEN
              INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
              VALUES (par$cod_divisas(idx), 'COD_DIVISA');
           END IF;
         END LOOP;

         /* CANSULTA DOS CODIGOS Y HACE LAS VALIDACIONES NECESARIAS - POR CODIGO AJENO*/
         IF par$cod_identificador_ajeno IS NOT NULL THEN
             FOR rec$divisa IN cur$divisas_ajeno LOOP
                IF rec$divisa.OID_DIVISA IS NULL THEN
                   par$validaciones(par$validaciones.COUNT) :=  '2040020005' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                '2040020005',
                                                                                                'MARCARMOVIMIENTOS',
                                                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                                rec$divisa.COD_DIVISA || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TDIVISA',
                                                                                                0);
                ELSE
                    INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
                    VALUES (rec$divisa.OID_DIVISA, 'OID_DIVISA');
                END IF;
             END LOOP rec$divisa;
         ELSE
             FOR rec$divisa IN cur$divisas LOOP
                IF rec$divisa.OID_DIVISA IS NULL THEN
                   par$validaciones(par$validaciones.COUNT) := '2040020011' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020011',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                rec$divisa.COD_DIVISA,
                                                0);
                ELSE
                    INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
                    VALUES (rec$divisa.OID_DIVISA, 'OID_DIVISA');
                END IF;
             END LOOP rec$divisa;
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
                par$validaciones(par$validaciones.COUNT) := '2040020005' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                '2040020005',
                                                                                                'MARCARMOVIMIENTOS',
                                                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                                par$cod_delegacion || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TDELEGACION',
                                                                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020005' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                '2040020005',
                                                                                                'MARCARMOVIMIENTOS',
                                                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                                par$cod_planta || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TPLANTA',
                                                                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020005' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                '2040020005',
                                                                                                'MARCARMOVIMIENTOS',
                                                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                                par$cod_cliente || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TCLIENTE',
                                                                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020005' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                '2040020005',
                                                                                                'MARCARMOVIMIENTOS',
                                                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                                par$cod_sub_cliente || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TSUBCLIENTE',
                                                                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020005' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                '2040020005',
                                                                                                'MARCARMOVIMIENTOS',
                                                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                                par$cod_punto_servicio || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TPUNTO_SERVICIO',
                                                                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020012' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020012',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_delegacion,
                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020013' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020013',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_planta,
                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020014' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020014',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_cliente,
                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020015' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020015',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_sub_cliente,
                                                0);
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
                par$validaciones(par$validaciones.COUNT) := '2040020016' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040020016',
                                                'MARCARMOVIMIENTOS',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                par$cod_punto_servicio,
                                                0);
            END;
          END IF;

      END IF;

      /* Validaciones de integridad */
      /* si fue informado codigo de planta, debe ser informado codigo delegacion */
      IF par$oid_planta IS NOT NULL AND par$oid_delegacion IS NULL THEN
          par$validaciones(par$validaciones.COUNT) := '2040020017' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          '2040020017',
                                                                                          'MARCARMOVIMIENTOS',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                          NULL,
                                                                                          0);
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
              par$validaciones(par$validaciones.COUNT) := '2040020017' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              '2040020017',
                                                                                              'MARCARMOVIMIENTOS',
                                                                                              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                              NULL,
                                                                                              0);
          END;
      END IF;

      /* si fue informado codigo de subcliente, debe ser informado codigo cliente */
      IF par$oid_sub_cliente IS NOT NULL AND par$oid_cliente IS NULL THEN
          par$validaciones(par$validaciones.COUNT) := '2040020018' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          '2040020018',
                                                                                          'MARCARMOVIMIENTOS',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                          NULL,
                                                                                          0);
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
              par$validaciones(par$validaciones.COUNT) := '2040020018' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              '2040020018',
                                                                                              'MARCARMOVIMIENTOS',
                                                                                              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                              NULL,
                                                                                              0);
          END;
      END IF;

      /* si fue informado codigo de punto servicio, debe ser informado codigo subcliente */
      IF par$oid_punto_servicio IS NOT NULL AND (par$oid_sub_cliente IS NULL OR par$oid_cliente IS NULL) THEN
          par$validaciones(par$validaciones.COUNT) := '2040020019' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          '2040020019',
                                                                                          'MARCARMOVIMIENTOS',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                          NULL,
                                                                                          0);
      END IF;
      /* si fue informado codigo de subcliente y cliente, debe validar integridad */
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
              par$validaciones(par$validaciones.COUNT) := '2040020019' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              '2040020019',
                                                                                              'MARCARMOVIMIENTOS',
                                                                                              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                              NULL,
                                                                                              0);
          END;
      END IF;

  END svalidar_codigos;
  
   PROCEDURE srecuperar_transaciones(par$oid_delegacion_gmt       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_transaciones         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$bol_planificada          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$bol_notificado           IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$bol_acreditacion         IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$oid_delegaciones         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_canales              IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$cod_fecha                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_desde                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_hasta                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_referencia           IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                    par$bol_importeinformativo   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$oid_clientes             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_subclientes          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_maquinas             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_sectores             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_punto_servicio       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_banco_capital        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_banco_facturacion    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_banco_tesoreria      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_cuenta_tesoreria     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_tipo_planificaciones IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$oid_planificaciones      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$rc_movimientos           OUT sys_refcursor,
                                    par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                    par$cod_termino              IN gepr_pcomon_###VERSION###.tipo$cod_,
                    par$val_termino              IN gepr_pcomon_###VERSION###.tipo$desc_) IS

      const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_transaciones';

      var$inicio               TIMESTAMP := SYSTIMESTAMP;
      var$err                  gepr_pcomon_###VERSION###.tipo$obs_ := '';
      var$filtro_documentos    gepr_pcomon_###VERSION###.tipo$obs_;
      var$lblSi                gepr_pcomon_###VERSION###.tipo$desc_;
      var$lblNo                gepr_pcomon_###VERSION###.tipo$desc_;
      var$lblConFechaValor     gepr_pcomon_###VERSION###.tipo$desc_;
      var$lblSinFechaValor     gepr_pcomon_###VERSION###.tipo$desc_;
      var$queryCursor          gepr_pcomon_###VERSION###.tipo$obs_ := ''; 
      var$filtros_joins        gepr_pcomon_###VERSION###.tipo$obs_ := ''; 
      var$filtros_shipout      gepr_pcomon_###VERSION###.tipo$obs_ := '';
      var$join_shipout         gepr_pcomon_###VERSION###.tipo$obs_ := '';
      var$hace_join_canal      gepr_pcomon_###VERSION###.tipo$nel_ := 0; 
      var$hace_join_cliente    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$hace_join_del        gepr_pcomon_###VERSION###.tipo$nel_ := 0; 
      var$hace_join_sector     gepr_pcomon_###VERSION###.tipo$nel_ := 0; 
      var$hace_join_maq        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$hace_join_plani      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$hace_join_pto        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$hace_join_subcliente gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$hace_join_tplani     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
      var$hace_join_trans      gepr_pcomon_###VERSION###.tipo$nel_ := 0;  
      var$withShipout          gepr_pcomon_###VERSION###.tipo$obs_ := '';
      var$cod_pais             gepr_pcomon_###VERSION###.tipo$cod_ := '';
  BEGIN

    /* Limpiar tabla temporaria */
    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TAUXILIAR';
    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TDOCUMENTOS';

    /* #### Inicializar los cursores #### */
    OPEN par$rc_movimientos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* Diccionarios */
    var$lblSi := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 'lblSi', 'SALDOS_CONSULTAR_TRANSACCIONES', gepr_pcomon_###VERSION###.const$codAplicacionSaldos, NULL, 0);
    var$lblNo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 'lblNo', 'SALDOS_CONSULTAR_TRANSACCIONES', gepr_pcomon_###VERSION###.const$codAplicacionSaldos, NULL, 0);
    var$lblConFechaValor := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 'lblConFechaValor', 'SALDOS_CONSULTAR_TRANSACCIONES', gepr_pcomon_###VERSION###.const$codAplicacionSaldos, NULL, 0);
    var$lblSinFechaValor := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura, 'lblSinFechaValor', 'SALDOS_CONSULTAR_TRANSACCIONES', gepr_pcomon_###VERSION###.const$codAplicacionSaldos, NULL, 0);

    /*Inicializar CodPais*/
    BEGIN
      SELECT COD_PAIS INTO var$cod_pais FROM GEPR_TDELEGACION WHERE OID_DELEGACION = par$oid_delegacion_gmt;
    EXCEPTION
      WHEN no_data_found THEN
      var$cod_pais := NULL;
    END;
    /*INICIO CARGA CODIGOS*/
    /* Tipo transacciones filtro */
    IF par$cod_transaciones IS NOT NULL AND par$cod_transaciones.COUNT > 1  THEN
    var$hace_join_trans := 1;
      FOR idx IN par$cod_transaciones.first .. par$cod_transaciones.last LOOP
        IF par$cod_transaciones(idx) IS NOT NULL THEN
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
               VALUES (par$cod_transaciones(idx), par$cod_transaciones(idx), 'TIPO_TRANSACIONES');
        END IF;
      END LOOP;
    END IF;

    /* Filtro: Cliente */
    IF par$oid_clientes IS NOT NULL AND par$oid_clientes.COUNT > 1 THEN
     var$hace_join_cliente := 1;
      FOR idx IN par$oid_clientes.first .. par$oid_clientes.last LOOP
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
         ( SELECT par$oid_clientes(idx), COD_CLIENTE, 'CLIENTES' FROM GEPR_TCLIENTE WHERE OID_CLIENTE = par$oid_clientes(idx));
      END LOOP;
    END IF;
    
    
    /* Filtro: Cliente Capital*/
    IF par$oid_banco_capital IS NOT NULL AND par$oid_banco_capital.COUNT > 1 THEN
      var$hace_join_cliente := 1;
      FOR idx IN par$oid_banco_capital.first .. par$oid_banco_capital.last LOOP
         INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_banco_capital(idx), COD_CLIENTE, 'CLIENTES' FROM GEPR_TCLIENTE WHERE OID_CLIENTE = par$oid_banco_capital(idx));
      END LOOP;
    END IF;
    
    /* Filtro: Banco Facturacion*/
    IF par$oid_banco_facturacion IS NOT NULL AND par$oid_banco_facturacion.COUNT > 1 THEN
        var$hace_join_cliente := 1;
      FOR idx IN par$oid_banco_facturacion.first .. par$oid_banco_facturacion.last LOOP
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_banco_facturacion(idx), COD_CLIENTE, 'CLIENTES' FROM GEPR_TCLIENTE WHERE OID_CLIENTE = par$oid_banco_facturacion(idx));
      END LOOP;
    END IF;
   
  /* oid_subclientes */
    
    IF par$oid_subclientes IS NOT NULL AND par$oid_subclientes.COUNT > 1 THEN
        var$hace_join_subcliente := 1;
      FOR idx IN par$oid_subclientes.first .. par$oid_subclientes.last LOOP
       INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_subclientes(idx), COD_SUBCLIENTE, 'SUBCLIENTES' FROM GEPR_TSUBCLIENTE WHERE OID_SUBCLIENTE = par$oid_subclientes(idx));
      END LOOP;
    END IF; 

    IF par$oid_banco_tesoreria IS NOT NULL AND par$oid_banco_tesoreria.COUNT > 1 THEN
    var$hace_join_subcliente := 1;
      FOR idx IN par$oid_banco_tesoreria.first .. par$oid_banco_tesoreria.last LOOP
       INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_banco_tesoreria(idx), COD_SUBCLIENTE, 'SUBCLIENTES' FROM GEPR_TSUBCLIENTE WHERE OID_SUBCLIENTE = par$oid_banco_tesoreria(idx));
      END LOOP;
    END IF;
  
    /* Filtro: SECTORES */
    IF par$oid_sectores IS NOT NULL AND par$oid_sectores.COUNT > 1 THEN
      var$hace_join_sector := 1;
      FOR idx IN par$oid_sectores.first .. par$oid_sectores.last LOOP
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_sectores(idx), COD_SECTOR, 'SECTORES' FROM GEPR_TSECTOR WHERE OID_SECTOR = par$oid_sectores(idx));
      END LOOP;
    END IF;
 /* Filtro: MAQUINAS */
    IF par$oid_maquinas IS NOT NULL AND par$oid_maquinas.COUNT > 1 THEN
       var$hace_join_maq := 1;
      FOR idx IN par$oid_maquinas.first .. par$oid_maquinas.last LOOP
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT  par$oid_maquinas(idx), COD_IDENTIFICACION, 'MAQUINAS' FROM SAPR_TMAQUINA WHERE OID_MAQUINA = par$oid_maquinas(idx));
      END LOOP;
    END IF;

    
    /* Filtro: Punto Servicio */
    IF par$oid_punto_servicio IS NOT NULL AND par$oid_punto_servicio.COUNT > 1 THEN
       var$hace_join_pto := 1;
      FOR idx IN par$oid_punto_servicio.first .. par$oid_punto_servicio.last LOOP
         INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_punto_servicio(idx), COD_PTO_SERVICIO, 'PTO_SERVICIOS' FROM GEPR_TPUNTO_SERVICIO WHERE OID_PTO_SERVICIO = par$oid_punto_servicio(idx));
      END LOOP;
    END IF;
  
  
/* Filtro: Cuenta Tesoreria*/
    IF par$oid_cuenta_tesoreria IS NOT NULL AND par$oid_cuenta_tesoreria.COUNT > 1 THEN
      var$hace_join_pto := 1; 
      FOR idx IN par$oid_cuenta_tesoreria.first .. par$oid_cuenta_tesoreria.last LOOP
         INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT  par$oid_cuenta_tesoreria(idx), COD_PTO_SERVICIO, 'PTO_SERVICIOS' FROM GEPR_TPUNTO_SERVICIO WHERE OID_PTO_SERVICIO = par$oid_cuenta_tesoreria(idx));
      END LOOP;
    END IF;

    /* Filtro: Delegacion */
    IF par$oid_delegaciones IS NOT NULL AND par$oid_delegaciones.COUNT > 1 THEN
      var$hace_join_del := 1;
      FOR idx IN par$oid_delegaciones.first .. par$oid_delegaciones.last LOOP
       INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_delegaciones(idx), COD_DELEGACION, 'DELEGACIONES' FROM GEPR_TDELEGACION WHERE OID_DELEGACION = par$oid_delegaciones(idx));
      END LOOP;
    END IF;

    /* Filtro: Canal */
    IF par$oid_canales IS NOT NULL AND par$oid_canales.COUNT > 1 THEN
       var$hace_join_canal := 1;
      FOR idx IN par$oid_canales.first .. par$oid_canales.last LOOP
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_canales(idx), CAJ.COD_AJENO, 'CANALES' FROM GEPR_TCANAL CAN
            INNER JOIN GEPR_TCODIGO_AJENO CAJ ON CAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'  AND COD_IDENTIFICADOR='MAE' AND CAN.OID_cANAL = OID_TABLA_GENESIS
            WHERE CAN.OID_CANAL = par$oid_canales(idx));
      END LOOP;
    END IF;


    /* Filtro: planificacion */
    IF par$oid_planificaciones IS NOT NULL AND par$oid_planificaciones.COUNT > 1 THEN
      var$hace_join_plani := 1;  
      FOR idx IN par$oid_planificaciones.first .. par$oid_planificaciones.last LOOP
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          ( SELECT par$oid_planificaciones(idx), COD_PLANIFICACION, 'PLANIFICACIONES' FROM SAPR_TPLANIFICACION WHERE OID_PLANIFICACION = par$oid_planificaciones(idx));
      END LOOP;
    END IF;

    /* Filtro: Tipo Planificacion */
    IF par$oid_tipo_planificaciones IS NOT NULL AND par$oid_tipo_planificaciones.COUNT > 1 THEN
      var$hace_join_tplani := 1;   
      FOR idx IN par$oid_tipo_planificaciones.first .. par$oid_tipo_planificaciones.last LOOP
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
         ( SELECT par$oid_tipo_planificaciones(idx), COD_TIPO_PLANIFICACION, 'TIPO_PLANIFICACIONES' FROM SAPR_TTIPO_PLANIFICACION WHERE OID_TIPO_PLANIFICACION = par$oid_tipo_planificaciones(idx));
        
      END LOOP;
    END IF;
    /*FIN CARGA CODIGOS*/
    

    /*DEFINO LOS JOINS PARA LOS FILTROS DE MOVIMIENTOS*/
    IF var$hace_join_trans = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_TRAN ON AUX_TRAN.COD_CAMPO2 = MOVI.COD_GRUPO_MOVIMIENTO AND AUX_TRAN.COD_CALIFICADOR = 'TIPO_TRANSACIONES']';
    END IF;
     IF var$hace_join_cliente = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_CLI ON AUX_CLI.COD_CAMPO2 = MOVI.COD_CLIENTE_MDM AND AUX_CLI.COD_CALIFICADOR = 'CLIENTES']';
    END IF;
    IF var$hace_join_subcliente = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_SCLI ON AUX_SCLI.COD_CAMPO2 = MOVI.COD_SUBCLIENTE_MDM AND AUX_SCLI.COD_CALIFICADOR = 'SUBCLIENTES']';
    END IF;
     IF var$hace_join_sector = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_SEC ON AUX_SEC.COD_CAMPO2 = MOVI.COD_DEVICEID AND AUX_SEC.COD_CALIFICADOR = 'SECTORES']';
    END IF;
    IF var$hace_join_maq = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_MAQ ON AUX_MAQ.COD_CAMPO2 = MOVI.COD_DEVICEID AND AUX_MAQ.COD_CALIFICADOR = 'MAQUINAS']';
    END IF;
     IF var$hace_join_pto = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_PTO ON AUX_PTO.COD_CAMPO2 = MOVI.COD_PTO_SERVICIO_MDM AND AUX_PTO.COD_CALIFICADOR = 'PTO_SERVICIOS']';
    END IF;
    IF var$hace_join_canal = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_CAN ON AUX_CAN.COD_CAMPO2 = MOVI.COD_CANAL AND AUX_CAN.COD_CALIFICADOR = 'CANALES']';
    END IF;
      IF var$hace_join_del = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_DEL ON AUX_DEL.COD_CAMPO2 = MOVI.COD_DELEGACION_MDM AND AUX_DEL.COD_CALIFICADOR = 'DELEGACIONES']';
    END IF;
     IF var$hace_join_plani = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_PLAN ON AUX_PLAN.COD_CAMPO2 = MOVI.COD_PLANIFICACION AND AUX_PLAN.COD_CALIFICADOR = 'PLANIFICACIONES']';
    END IF;
     IF var$hace_join_tplani = 1 THEN
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_TPLAN ON AUX_TPLAN.COD_CAMPO2 = MOVI.COD_TIPO_PLANIFICACION AND AUX_TPLAN.COD_CALIFICADOR = 'TIPO_PLANIFICACIONES']';
    END IF;
      IF par$val_termino IS NOT NULL  THEN 
        var$filtros_joins := var$filtros_joins || ' ' || q'[ INNER JOIN SAPR_TMOVIMIENTO_CAMPO_EXTRA EXTRA ON EXTRA.OID_MOVIMIENTO = MOVI.OID_MOVIMIENTO AND EXTRA.COD_CAMPO_EXTRA = ']' || par$cod_termino || q'[' AND EXTRA.DES_VALOR_CAMPO_EXTRA = ']' ||par$val_termino || q'[' ]';
        
    END IF;

     
    
    /*INICIO CARGA FILTROS*/
    IF var$cod_pais IS NOT NULL THEN
        var$filtro_documentos := var$filtro_documentos || q'[ AND MOVI.COD_PAIS = ']' || var$cod_pais || q'[' ]';
    END IF;

    /* Filtro: Notificacion */
    IF par$bol_notificado IS NOT NULL THEN
      IF par$bol_notificado = 1 THEN
        var$filtro_documentos := var$filtro_documentos || ' AND MOVI.BOL_NOTIFICADO = 1 ';
      ELSIF par$bol_notificado = 0 THEN
        var$filtro_documentos := var$filtro_documentos || ' AND MOVI.BOL_NOTIFICADO = 0 ';
      END IF;
    END IF;

    /* Filtro: Acreditacion */
    IF par$bol_acreditacion IS NOT NULL THEN
      IF par$bol_acreditacion = 1 THEN
        var$filtro_documentos := var$filtro_documentos || ' AND MOVI.BOL_ACREDITADO is not null ';
      ELSIF par$bol_acreditacion = 0 THEN
        var$filtro_documentos := var$filtro_documentos || ' AND MOVI.BOL_ACREDITADO is null ';
      END IF;
    END IF;

    IF par$fyh_referencia IS NULL THEN
      CASE par$cod_fecha
        WHEN 1 THEN /* FECHA GESTION*/
              var$filtro_documentos := var$filtro_documentos || q'[ AND MOVI.FEC_MOVIMIENTO BETWEEN TRUNC(TO_DATE(']' || par$fyh_desde || q'[', 'yyyy-mm-dd hh24:mi:ss'))  AND TRUNC(TO_DATE(']' || par$fyh_hasta || q'[', 'yyyy-mm-dd hh24:mi:ss')) ]';
              var$filtro_documentos := var$filtro_documentos || q'[ AND MOVI.HOR_MOVIMIENTO BETWEEN TO_DATE(']' || par$fyh_desde || q'[', 'yyyy-mm-dd hh24:mi:ss')  AND TO_DATE(']' || par$fyh_hasta || q'[', 'yyyy-mm-dd hh24:mi:ss') ]';
        WHEN 2 THEN /* FECHA CREATION*/
             var$filtro_documentos := var$filtro_documentos || q'[ AND MOVI.GMT_CREACION BETWEEN to_timestamp_Tz(']' || par$fyh_desde || q'[', 'yyyy-mm-dd hh24:mi:ss TZH:TZM')  AND to_timestamp_Tz(']' || par$fyh_hasta || q'[', 'yyyy-mm-dd hh24:mi:ss TZH:TZM') ]';
        WHEN 3 THEN /* FECHA ACREDITACION*/
             var$filtro_documentos := var$filtro_documentos || q'[ AND MOVI.FYH_ACREDITACION BETWEEN TO_DATE(']' || par$fyh_desde || q'[', 'yyyy-mm-dd hh24:mi:ss')  AND TO_DATE(']' || par$fyh_hasta || q'[', 'yyyy-mm-dd hh24:mi:ss') ]';
        WHEN 4 THEN /* FECHA NOTIFICACION*/
             var$filtro_documentos := var$filtro_documentos || q'[ AND MOVI.FYH_NOTIFICACION  BETWEEN TO_DATE(']' || par$fyh_desde || q'[', 'yyyy-mm-dd hh24:mi:ss')  AND TO_DATE(']' || par$fyh_hasta || q'[', 'yyyy-mm-dd hh24:mi:ss') ]';
      END CASE;

    ELSE

      /*DEFINO LOS JOINS PARA LOS FILTROS DE SHIPOUT*/
   
     IF var$hace_join_cliente = 1 THEN
        var$filtros_shipout := var$filtros_shipout || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_CLI ON AUX_CLI.OID_CAMPO1 = CUEN.OID_CLIENTE AND AUX_CLI.COD_CALIFICADOR = 'CLIENTES' ]';
    END IF;
    IF var$hace_join_subcliente = 1 THEN
        var$filtros_shipout := var$filtros_shipout || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_SCLI ON AUX_SCLI.OID_CAMPO1 = CUEN.OID_SUBCLIENTE AND AUX_SCLI.COD_CALIFICADOR = 'SUBCLIENTES']';
    END IF;
    IF var$hace_join_maq = 1 THEN
        var$filtros_shipout := var$filtros_shipout || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_MAQ ON AUX_MAQ.OID_CAMPO1 = CUEN.OID_MAQUINA AND AUX_MAQ.COD_CALIFICADOR = 'MAQUINAS']';
    END IF;
     IF var$hace_join_pto = 1 THEN
        var$filtros_shipout := var$filtros_shipout || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_PTO ON AUX_PTO.OID_CAMPO1 = CUEN.OID_PTO_SERVICIO AND AUX_PTO.COD_CALIFICADOR = 'PTO_SERVICIOS']';
    END IF;
    IF var$hace_join_canal = 1 THEN
        var$filtros_shipout := var$filtros_shipout || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_CAN ON AUX_CAN.OID_CAMPO1 = CUEN.OID_CANAL AND AUX_CAN.COD_CALIFICADOR = 'CANALES']';
    END IF;
      IF var$hace_join_del = 1 THEN
        var$filtros_shipout := var$filtros_shipout || ' ' || q'[ INNER JOIN SAPR_GTT_TAUXILIAR AUX_DEL ON AUX_DEL.OID_CAMPO1 = CUEN.OID_DELEGACION AND AUX_DEL.COD_CALIFICADOR = 'DELEGACIONES']';
    END IF;

      /* Probar auxiliar: Shipout */    
      var$withShipout := q'[ WITH SHIP AS (
          SELECT CUEN.COD_IDENTIFICACION_MAQUINA 
          FROM SAPR_TMOVIMIENTO_SHIPOUT MOVI_S
          INNER JOIN SAPR_VCUENTA CUEN ON CUEN.OID_CUENTA = MOVI_S.OID_CUENTA_SALDO ]'
          || var$filtros_shipout || q'[ 
          WHERE SYS_EXTRACT_UTC(MOVI_S.GMT_CREACION) > (SYSDATE-90) AND
          MOVI_S.FYH_GESTION < TO_DATE( ']' || par$fyh_referencia || q'[' , 'DD/MM/YYYY HH24:MI:SS')
          GROUP BY CUEN.COD_IDENTIFICACION_MAQUINA )]' ;
      
      var$join_shipout := q'[ INNER JOIN SHIP ON SHIP.COD_IDENTIFICACION_MAQUINA = MOVI.COD_DEVICEID ]';
   
       
    END IF;

    var$queryCursor :=  var$withShipout || q'[ SELECT MOVI.OID_MOVIMIENTO
              , MOVI.OID_DOCUMENTO
              , FN_GMT_DELEGACION( ']'|| par$oid_delegacion_gmt || q'[' , MOVI.FEC_MOVIMIENTO) AS FYH_GESTION
              , FN_GMT_DELEGACION( ']'|| par$oid_delegacion_gmt || q'[' , SYS_EXTRACT_UTC(MOVI.HOR_MOVIMIENTO)) AS HOR_GESTION
              , FN_GMT_DELEGACION( ']'|| par$oid_delegacion_gmt || q'[' , SYS_EXTRACT_UTC(MOVI.GMT_CREACION)) AS GMT_CREACION
              , case WHEN MOVI.FYH_ACREDITACION IS NULL 
                          THEN MOVI.FYH_ACREDITACION
                      ELSE
                      FN_GMT_DELEGACION( ']'|| par$oid_delegacion_gmt || q'[' , SYS_EXTRACT_UTC(MOVI.FYH_ACREDITACION)) END AS FYH_ACREDITACION
              , FN_GMT_DELEGACION( ']'|| par$oid_delegacion_gmt || q'[' , SYS_EXTRACT_UTC(MOVI.FYH_NOTIFICACION)) AS FYH_NOTIFICACION
              , CASE WHEN REGEXP_COUNT(MOVI.COD_MOVIMIENTO, '_', 1,'i')>0 AND IS_NUMBER(SUBSTR(MOVI.COD_MOVIMIENTO, INSTR(MOVI.COD_MOVIMIENTO, '_' ,1 , REGEXP_COUNT(MOVI.COD_MOVIMIENTO, '_', 1, 'i')) + 1)) = 1
                     THEN SUBSTR(MOVI.COD_MOVIMIENTO, 1, INSTR(MOVI.COD_MOVIMIENTO, '_', 1, REGEXP_COUNT(MOVI.COD_MOVIMIENTO, '_', 1, 'i')) - 1)    
                     ELSE MOVI.COD_MOVIMIENTO END AS COD_EXTERNO_BASE
              , MOVI.COD_MOVIMIENTO AS COD_EXTERNO
              , MOVI.COD_DEVICEID AS COD_MAQUINA
              , MOVI.COD_PTO_SERVICIO_OPE ||' - '|| MOVI.DES_PTO_SERVICIO AS PTO_SERVICIO
              , MOVI.COD_CLIENTE_OPE ||' - '|| MOVI.DES_CLIENTE AS CLIENTE
              , MOVI.COD_SUBCLIENTE_OPE ||' - '|| MOVI.DES_SUBCLIENTE AS SUBCLIENTE
              , MOVI.COD_DELEGACION_MDM ||' - '|| MOVI.DES_DELEGACION AS DELEGACION
              , MOVI.COD_CANAL ||' - '|| MOVI.DES_CANAL AS CANAL
              , MOVI.COD_SUBCANAL ||' - '|| MOVI.DES_SUBCANAL AS SUBCANAL
              , MOVI.COD_TIPO_MOVIMIENTO ||' - '|| MOVI.DES_TIPO_MOVIMIENTO AS FORMULARIO
              , CASE WHEN MOVI.BOL_VIRTUAL = 0 THEN ']' || var$lblNo || q'['
                 WHEN MOVI.BOL_VIRTUAL = 1 THEN ']' || var$lblSi || q'['
                 ELSE ' ' END IMPORTE_INFORMATIVO
              , CASE WHEN ]' || par$bol_importeinformativo || q'[ = 0 AND MOVI.BOL_VIRTUAL = 1 THEN NULL
                ELSE   MOVI.COD_DIVISA ||' - '|| MOVI.DES_DIVISA  END DIVISA
              , CASE WHEN ]' || par$bol_importeinformativo || q'[ = 0 AND MOVI.BOL_VIRTUAL = 1 THEN NULL
                ELSE   MOVI.NUM_IMPORTE  END IMPORTE
              , DIVI.COD_SIMBOLO AS SIMBOLO
              , TELLER.DES_VALOR_CAMPO_EXTRA AS TELLER
              , TELLERN.DES_VALOR_CAMPO_EXTRA AS TELLERNAME
              , RN.DES_VALOR_CAMPO_EXTRA AS RECEIPT_NUMBER
              , BC.DES_VALOR_CAMPO_EXTRA AS BAR_CODE
              , MOVI.COD_GRUPO_MOVIMIENTO AS TIPO_TRANSACCION
              , MOVI.COD_TIPO_MOVIMIENTO AS COD_FORMULARIO
              , CASE WHEN MOVI.COD_PLANIFICACION IS NOT NULL THEN ']' || var$lblConFechaValor || q'['
                 WHEN MOVI.COD_PLANIFICACION IS NULL THEN ']' || var$lblSinFechaValor || q'['
                 ELSE ' ' END MODALIDAD
              , CASE WHEN MOVI.BOL_NOTIFICADO = 0 THEN ']' || var$lblNo || q'['
                 WHEN MOVI.BOL_NOTIFICADO = 1 THEN ']' || var$lblSi || q'['
                 ELSE ' ' END NOTIFICACION
              , ']'|| par$cod_termino || q'[' AS COD_TERMINO
              , ']'|| par$val_termino || q'[' AS VAL_TERMINO 
              , MOVI.NEL_CANTIDAD
              , MOVI.COD_DEVICEID_BASE
           FROM SAPR_TMOVIMIENTO MOVI
           LEFT JOIN SAPR_TMOVIMIENTO_CAMPO_EXTRA TELLER ON TELLER.OID_MOVIMIENTO = MOVI.OID_MOVIMIENTO AND UPPER(TELLER.COD_CAMPO_EXTRA) = 'TELLER' AND MOVI.FEC_MOVIMIENTO = TELLER.FEC_MOVIMIENTO AND MOVI.COD_PAIS = TELLER.COD_PAIS
           LEFT JOIN SAPR_TMOVIMIENTO_CAMPO_EXTRA TELLERN ON TELLERN.OID_MOVIMIENTO = MOVI.OID_MOVIMIENTO AND UPPER(TELLERN.COD_CAMPO_EXTRA) = 'TELLERNAME'  AND MOVI.FEC_MOVIMIENTO = TELLERN.FEC_MOVIMIENTO AND MOVI.COD_PAIS = TELLERN.COD_PAIS
           LEFT JOIN SAPR_TMOVIMIENTO_CAMPO_EXTRA RN ON RN.OID_MOVIMIENTO = MOVI.OID_MOVIMIENTO AND UPPER(RN.COD_CAMPO_EXTRA) = 'RECEIPT NUMBER' AND MOVI.FEC_MOVIMIENTO = RN.FEC_MOVIMIENTO AND MOVI.COD_PAIS = RN.COD_PAIS
           LEFT JOIN SAPR_TMOVIMIENTO_CAMPO_EXTRA BC ON BC.OID_MOVIMIENTO = MOVI.OID_MOVIMIENTO AND UPPER(BC.COD_CAMPO_EXTRA) = 'IDENTIFICACION' AND MOVI.FEC_MOVIMIENTO = BC.FEC_MOVIMIENTO AND MOVI.COD_PAIS = BC.COD_PAIS
           INNER JOIN GEPR_TCODIGO_AJENO AJ ON AJ.COD_AJENO = MOVI.COD_DIVISA AND COD_IDENTIFICADOR = 'MAE' AND UPPER(COD_TIPO_TABLA_GENESIS) = 'GEPR_TDIVISA'
           LEFT JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = AJ.OID_TABLA_GENESIS ]'
           || var$filtros_joins || var$join_shipout || ' WHERE 1=1 ' || var$filtro_documentos;  

    DBMS_OUTPUT.PUT_LINE(var$queryCursor);
    /* Probar cursor */
    OPEN par$rc_movimientos FOR ( var$queryCursor );
     

    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 
              0, 0, 0, 0, 0, 0, var$inicio, null, 'C', par$info_ejecucion, NULL, par$cod_usuario, par$cod_ejecucion);

    COMMIT;

  /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func,
                0, 0, 0, 0, 0, 0, var$inicio, null, 'S', par$info_ejecucion, SUBSTR(var$queryCursor, 1, 4000) , par$cod_usuario, par$cod_ejecucion);
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 
                0, 0, 0, 0, 0, 0, var$inicio, null, 'R', par$info_ejecucion, var$err, par$cod_usuario, par$cod_ejecucion);

  END srecuperar_transaciones;

   PROCEDURE srecuperar_detalle(par$oid_transaccion    IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_externo_base   IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$info_ejecucion     IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$rc_detalle         OUT sys_refcursor,
                               par$rc_detalle_valores OUT sys_refcursor,
                               par$rc_detalle_totales OUT sys_refcursor,
                               par$rc_documentos      OUT sys_refcursor,
                               par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

     const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_detalle';

    var$inicio                 TIMESTAMP := SYSTIMESTAMP;
    var$err                    gepr_pcomon_###VERSION###.tipo$obs_ := '';
    var$cod_teller             gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_teller             gepr_pcomon_###VERSION###.tipo$cod_;
    var$fec_movimiento         gepr_pcomon_###VERSION###.tipo$fyh_;
    var$cod_pais               gepr_pcomon_###VERSION###.tipo$cod_ := '';


  BEGIN

    /* Limpiar tabla temporaria */
    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TAUXILIAR';

    /* #### Inicializar los cursores #### */
    OPEN par$rc_detalle FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_detalle_valores FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_detalle_totales FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_documentos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /* Validar codigos */
    svalidar_codigos_detalle(par$oid_transaccion, par$cod_externo_base);
   
    var$fec_movimiento := TO_DATE(SUBSTR(par$cod_externo_base,1,8), 'yyyy-mm-dd');

    BEGIN
        SELECT COD_CAMPO8 INTO var$cod_pais FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_MOVIMIENTO' AND rownum = 1;
    END;
    /* === Poblar cursor === */
    /* par$rc_detalle */
    OPEN par$rc_detalle FOR
         SELECT MOVI.COD_CLIENTE_OPE || ' - ' || MOVI.DES_CLIENTE AS CLIENTE,
       MOVI.COD_SUBCLIENTE_OPE || ' - ' || MOVI.DES_SUBCLIENTE AS SUBCLIENTE,
       MOVI.COD_PTO_SERVICIO_OPE || ' - ' || MOVI.DES_PTO_SERVICIO AS PUNTO_SERVICIO,
       MOVI.COD_DEVICEID || ' - '|| MOVI.DES_MAQUINA AS MAQUINA,
       MOVI.COD_MOVIMIENTO AS COD_EXTERNO,
       MOVI.COD_GRUPO_MOVIMIENTO AS TIPO_MOVIMIENTO,
       MOVI.HOR_MOVIMIENTO AS FYH_GESTION,
       MOVI.GMT_CREACION AS GMT_CREACION,
       TELLER.DES_VALOR_CAMPO_EXTRA ||' - ' || TELLERNAME.DES_VALOR_CAMPO_EXTRA AS TELLER,
         MOVI.COD_TIPO_MOVIMIENTO || ' - ' || MOVI.DES_TIPO_MOVIMIENTO AS FORMULARIO,
       MOVI.BOL_NOTIFICADO,
       MOVI.BOL_ACREDITADO,
       MOVI.DES_DELEGACION || ' - ' || NVL(MOVI.DES_PTO_SERVICIO, NVL(MOVI.DES_SUBCLIENTE, MOVI.DES_CLIENTE)) DESCRICAO,
       MOVI.OID_MOVIMIENTO
        FROM SAPR_TMOVIMIENTO MOVI
        INNER JOIN SAPR_GTT_TAUXILIAR A ON A.OID_CAMPO1 = MOVI.OID_MOVIMIENTO AND A.COD_CALIFICADOR = 'OID_MOVIMIENTO'  
        LEFT JOIN SAPR_TMOVIMIENTO_CAMPO_EXTRA TELLER ON TELLER.OID_MOVIMIENTO = MOVI.OID_MOVIMIENTO AND UPPER(TELLER.COD_CAMPO_EXTRA) = 'TELLER' AND MOVI.FEC_MOVIMIENTO = TELLER.FEC_MOVIMIENTO AND MOVI.COD_PAIS = TELLER.COD_PAIS
        LEFT JOIN SAPR_TMOVIMIENTO_CAMPO_EXTRA TELLERNAME ON TELLERNAME.OID_MOVIMIENTO = MOVI.OID_MOVIMIENTO AND UPPER(TELLERNAME.COD_CAMPO_EXTRA) = 'TELLERNAME' AND MOVI.FEC_MOVIMIENTO = TELLERNAME.FEC_MOVIMIENTO AND MOVI.COD_PAIS = TELLERNAME.COD_PAIS
        WHERE  MOVI.FEC_MOVIMIENTO = var$fec_movimiento 
        AND MOVI.COD_PAIS = var$cod_pais;

    /* par$rc_detalle_valores */
    OPEN par$rc_detalle_valores FOR
        
     SELECT      MOVI.DES_DIVISA
               , DET.DES_DENOMINACION
               , SUM(DET.NUM_IMPORTE) NUM_IMPORTE
               , SUM(DET.NEL_CANTIDAD) NEL_CANTIDAD
               , A.COD_CAMPO2 CANAL
               , A.COD_CAMPO3 SUBCANAL
               , A.OID_CAMPO1 OID_MOVIMIENTO
           FROM  SAPR_TMOVIMIENTO MOVI
        INNER JOIN SAPR_TMOVIMIENTO_DETALLE DET ON DET.OID_MOVIMIENTO = MOVI.OID_MOVIMIENTO  AND DET.FEC_MOVIMIENTO = MOVI.FEC_MOVIMIENTO AND DET.COD_PAIS = MOVI.COD_PAIS
        INNER JOIN SAPR_GTT_TAUXILIAR A ON A.OID_CAMPO1 = MOVI.OID_MOVIMIENTO AND A.COD_CALIFICADOR = 'OID_MOVIMIENTO'
        WHERE  MOVI.FEC_MOVIMIENTO = var$fec_movimiento 
        AND MOVI.COD_PAIS = var$cod_pais
         GROUP BY  MOVI.DES_DIVISA
               , DET.DES_DENOMINACION
               , A.COD_CAMPO2
               , A.COD_CAMPO3
               , A.OID_CAMPO1
        ORDER BY MOVI.DES_DIVISA, DET.DES_DENOMINACION, A.COD_CAMPO2, A.COD_CAMPO3;

      /* par$rc_detalle_totales */
    OPEN par$rc_detalle_totales FOR
          SELECT  
                MOVI.DES_DIVISA  
               , SUM(MOVI.NUM_IMPORTE) NUM_IMPORTE  
               , A.COD_CAMPO2 CANAL
               , A.COD_CAMPO3 SUBCANAL
               , A.OID_CAMPO1 OID_MOVIMIENTO
           FROM SAPR_TMOVIMIENTO MOVI
        INNER JOIN SAPR_GTT_TAUXILIAR A ON A.OID_CAMPO1 = MOVI.OID_MOVIMIENTO AND A.COD_CALIFICADOR = 'OID_MOVIMIENTO'   
        WHERE  MOVI.FEC_MOVIMIENTO = var$fec_movimiento
        AND MOVI.COD_PAIS = var$cod_pais
        GROUP BY MOVI.DES_DIVISA
               , A.COD_CAMPO2
               , A.COD_CAMPO3
               , A.OID_CAMPO1
               ORDER BY MOVI.DES_DIVISA ;

    /* par$rc_documentos */
    OPEN par$rc_documentos FOR
       SELECT   MOVI.OID_MOVIMIENTO,
                MOVI.COD_MOVIMIENTO              
         FROM SAPR_TMOVIMIENTO MOVI
         INNER JOIN SAPR_GTT_TAUXILIAR A
         ON A.OID_CAMPO1 = MOVI.OID_MOVIMIENTO             
        WHERE A.COD_CALIFICADOR = 'OID_MOVIMIENTO'
        AND MOVI.FEC_MOVIMIENTO = var$fec_movimiento
        AND MOVI.COD_PAIS = var$cod_pais;

  /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func,
                0, 0, 0, 0, 0, 0, var$inicio, null, 'R', par$info_ejecucion, var$err, par$cod_usuario, par$cod_ejecucion);

    

  END srecuperar_detalle;


  /* Validar Codigos */
  PROCEDURE svalidar_codigos_detalle(par$oid_transaccion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_externo_base  IN gepr_pcomon_###VERSION###.tipo$cod_) IS
  BEGIN
    
    /* par$oid_transaccion */
    IF par$oid_transaccion IS NOT NULL THEN            
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
           VALUES (par$oid_transaccion, 'OID_MOVIMIENTO');

    END IF;

    /* par$oid_transaccion */
    IF par$cod_externo_base IS NOT NULL THEN            
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CAMPO8, COD_CALIFICADOR)
           SELECT MOVI.OID_MOVIMIENTO, MOVI.COD_CANAL ||' - ' || MOVI.DES_CANAL CANAL,
                  MOVI.COD_SUBCANAL || ' - ' || MOVI.DES_SUBCANAL SUBCANAL ,MOVI.COD_MOVIMIENTO , MOVI.COD_PAIS,  'OID_MOVIMIENTO'   
             FROM SAPR_TMOVIMIENTO MOVI
            WHERE MOVI.COD_MOVIMIENTO LIKE par$cod_externo_base || '\_%' escape '\'
            AND MOVI.FEC_MOVIMIENTO = TO_DATE(SUBSTR(par$cod_externo_base,1,8), 'yyyy-mm-dd') ; 
            /*La funcion SUBSTR se utiliza para obtener la fecha y hacer uso del indice y mejorar el rendimiento.*/

    END IF;
  END svalidar_codigos_detalle;
  

PROCEDURE svalidar_movimiento_extradata(par$cod_externo_bases  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_extr_campo        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_extr_valor        IN gepr_pcomon_###VERSION###.tipo$obs_,
                                     par$cod_cultura           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$bol_valido            OUT gepr_pcomon_###VERSION###.tipo$nbol_, 
                                     par$cod_nuevo_valor       OUT gepr_pcomon_###VERSION###.tipo$obs_, 
                                     par$oid_documento         OUT gepr_pcomon_###VERSION###.tipo$cod_, 
                                     par$oid_termino           OUT gepr_pcomon_###VERSION###.tipo$cod_) IS
       var$nel_largo_campo            NUMBER;
  BEGIN


        --Por defecto siempre es valido
         par$bol_valido:=1;
        IF par$cod_externo_bases IS NULL THEN
            par$bol_valido:=0;            
            dbms_output.put_line('Es obligatorio informar el campo "Codigo Externo".');
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
            VALUES (par$cod_externo_bases, '2040170001', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040170001', gepr_pcomon_###VERSION###.const$CodFunModiMovimientos, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), 'VALIDACIONES');
        END IF;
        
        IF par$cod_extr_campo IS NULL THEN
            par$bol_valido:=0;
            
            dbms_output.put_line('Es obligatorio informar el campo "CampoExtra".');
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
            VALUES (par$cod_externo_bases, '2040170002', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040170002', gepr_pcomon_###VERSION###.const$CodFunModiMovimientos, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), 'VALIDACIONES');
         
                                                                        
        END IF;
        

          BEGIN
             SELECT OID_DOCUMENTO into par$oid_documento FROM SAPR_TDOCUMENTO WHERE COD_EXTERNO = par$cod_externo_bases;
          EXCEPTION WHEN no_data_found THEN
             par$oid_documento := NULL;
             dbms_output.put_line('No se ha encontrado un movimiento con el Codigo Externo "{0}".');
             INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
            VALUES (par$cod_externo_bases, '2040170008', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040170008', gepr_pcomon_###VERSION###.const$CodFunModiMovimientos, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), 'VALIDACIONES');

          END;
          
  
          BEGIN
             SELECT OID_DOCUMENTO into par$oid_documento FROM SAPR_TDOCUMENTO WHERE COD_EXTERNO = par$cod_externo_bases;
          EXCEPTION WHEN no_data_found THEN
              par$bol_valido:=0;          
             par$oid_documento := NULL;
             dbms_output.put_line('No se ha encontrado un movimiento con el Codigo Externo "{0}".');
             INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
            VALUES (par$cod_externo_bases, '2040170008', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040170008', gepr_pcomon_###VERSION###.const$CodFunModiMovimientos, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), 'VALIDACIONES');

          END;
          
         IF par$oid_documento IS NOT NULL THEN                               
            BEGIN
              SELECT        t.OID_TERMINO, NVL(NEC_LONGITUD,255)
              INTO        par$oid_termino, var$nel_largo_campo
              FROM GEPR_TTERMINO T
              WHERE T.COD_TERMINO = par$cod_extr_campo;
              
             EXCEPTION WHEN no_data_found THEN
               par$bol_valido:=0;          
               par$oid_termino := NULL;
               var$nel_largo_campo := NULL;
               dbms_output.put_line('1 - El campo ingresado "{0}" no cioincide con ninguno de los campos CampoExtra asignados para este tipo de movimiento.');
               
               INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
               VALUES (par$cod_externo_bases, '2040170005', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040170005', gepr_pcomon_###VERSION###.const$CodFunModiMovimientos, 
               gepr_pcomon_###VERSION###.const$codAplicacionGenesis, par$cod_extr_campo, 0), 'VALIDACIONES');                   
            END;
        END IF;  
   
            IF var$nel_largo_campo IS NOT NULL AND  length(par$cod_extr_valor) > var$nel_largo_campo  THEN
                --Si el largo del valor del extra data es mayor al máximo del termino (extra data) 
                    
                  dbms_output.put_line('El largo del campo Valor ({0}) supera el máximo permitido para este campo ({1}).');
                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                  VALUES (par$cod_externo_bases, '1040170001', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'1040170001', 
                  gepr_pcomon_###VERSION###.const$CodFunModiMovimientos, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                  par$cod_extr_valor  || '|' || var$nel_largo_campo , 0), 'VALIDACIONES');                  
               
               
                    par$cod_nuevo_valor := substr(par$cod_extr_valor, 0, var$nel_largo_campo);
                
            ELSE 
                 par$cod_nuevo_valor := substr(par$cod_extr_valor, 0, var$nel_largo_campo);              
            END IF;
            
        
        
        
        
          MERGE INTO SAPR_GTT_TAUXILIAR
        USING DUAL ON
        (
            COD_CAMPO2 = par$cod_externo_bases AND
            COD_CALIFICADOR = 'TIPO_RESULTADO'
        )
       WHEN MATCHED THEN
         UPDATE
           SET NEL_CAMPO5 =  NEL_CAMPO5 + par$bol_valido ,
               NEL_CAMPO6 = NEL_CAMPO6 + 1
              WHEN NOT MATCHED THEN
                INSERT
              (OID_CAMPO1, COD_CAMPO2, NEL_CAMPO5, NEL_CAMPO6, COD_CALIFICADOR)VALUES
              (NVL(par$oid_documento,SYS_GUID())
              ,par$cod_externo_bases
              ,par$bol_valido
              ,1
              ,'TIPO_RESULTADO');        
              
        
END svalidar_movimiento_extradata;

PROCEDURE smodificar_movimiento(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_externos            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$cods_extr_campo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$cods_extr_valor    IN gepr_pcomon_###VERSION###.tipo$obss_,
                                par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$rc_validaciones    OUT sys_refcursor,
                                par$rc_tipo_resultado OUT sys_refcursor) IS
    
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'smodificar_movimiento';

    var$detalle           VARCHAR2(4000);
    var$detalle_actual    VARCHAR2(4000);

    var$bol_valido                   gepr_pcomon_###VERSION###.tipo$nbol_;
    var$cod_nuevo_valor              gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_documento                gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_termino                  gepr_pcomon_###VERSION###.tipo$oid_;
BEGIN

    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
        DELETE SAPR_GTT_TAUXILIAR;
    

/* INICIO - LOG de parametros iniciales*/
    IF par$oid_llamada IS NOT NULL THEN
      BEGIN
        var$detalle := q'[Parametros iniciales: 
          par$oid_llamada: ]'             || par$oid_llamada || q'[
          par$cod_cultura: ]'             || par$cod_cultura || q'[
          par$cod_usuario: ]'             || par$cod_usuario;

        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$detalle,
                par$cod_identificador   => '');

        /* INICIO LOG de arrays asociativos */
        IF par$cod_externos IS NOT NULL AND par$cod_externos.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$cod_externos.first ..  par$cod_externos.last LOOP 
              var$detalle_actual := '';
              IF par$cod_externos(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$cod_externos(' || idx || '):  ' || par$cod_externos(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$cod_externos(' || idx || '):  NULL';
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
        IF par$cods_extr_campo IS NOT NULL AND par$cods_extr_campo.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$cods_extr_campo.first ..  par$cods_extr_campo.last LOOP 
              var$detalle_actual := '';
              IF par$cods_extr_campo(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$cods_extr_campo(' || idx || '):  ' || par$cods_extr_campo(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$cods_extr_campo(' || idx || '):  NULL';
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
        IF par$cods_extr_valor IS NOT NULL AND par$cods_extr_valor.COUNT > 0 THEN
          var$detalle := '';
          FOR idx IN  par$cods_extr_valor.first ..  par$cods_extr_valor.last LOOP 
              var$detalle_actual := '';
              IF par$cods_extr_valor(idx) IS NOT NULL THEN
                var$detalle_actual := 'Parametros iniciales: par$cods_extr_valor(' || idx || '):  ' || par$cods_extr_valor(idx);
              ELSE
                var$detalle_actual := 'Parametros iniciales: par$cods_extr_valor(' || idx || '):  NULL';
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

    FOR idx IN par$cod_externos.first .. par$cod_externos.last LOOP
      /* Log API*/       
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Antes de llamar procedure svalidar_movimiento_extradata',
                  par$cod_identificador   => '');

      svalidar_movimiento_extradata(par$cod_externo_bases  => par$cod_externos (idx),
                                    par$cod_extr_campo     => par$cods_extr_campo(idx),
                                    par$cod_extr_valor     => par$cods_extr_valor(idx),
                                    par$cod_nuevo_valor    => var$cod_nuevo_valor, 
                                    par$cod_cultura        => par$cod_cultura,
                                    par$bol_valido         => var$bol_valido,
                                    par$oid_documento      => var$oid_documento , 
                                    par$oid_termino        => var$oid_termino);


      IF var$bol_valido <> 0 THEN
        /* Log API*/       
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Antes de llamar procedure sapr_pdocumento_###VERSION###.supd_extradata',
                    par$cod_identificador   => '');
                    
        sapr_pdocumento_###VERSION###.supd_extradata(par$oid_documento      => var$oid_documento,
                        par$oid_termino      => var$oid_termino,
                        par$des_valor        => var$cod_nuevo_valor,
                        par$cod_usuario      => par$cod_usuario);
                          
      END IF;
                
          
     END LOOP;

    open par$rc_validaciones for
    select OID_CAMPO1 AS COD_EXTERNO,
           COD_CAMPO2 AS CODIGO,
           COD_CAMPO3 DESCRICION
    from sapr_gtt_tauxiliar WHERE COD_CALIFICADOR = 'VALIDACIONES' group by OID_CAMPO1,COD_CAMPO2,COD_CAMPO3   
    ;
    
     open par$rc_tipo_resultado for
      select OID_CAMPO1 OID_DOCUMENTO, 
      COD_CAMPO2 COD_EXTERNO, 
      CASE WHEN NEL_CAMPO6 - NEL_CAMPO5 = 0 THEN '0'
           WHEN NEL_CAMPO6 - NEL_CAMPO5 = NEL_CAMPO6 THEN '2'
           ELSE '1' END TIPO_RESULTADO
    from sapr_gtt_tauxiliar WHERE COD_CALIFICADOR = 'TIPO_RESULTADO';
  
 END smodificar_movimiento;




PROCEDURE srecuperar_doc_actual_id( par$oid_llamada            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_actual_id               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$info_ejecucion              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$rc_movimientos              OUT sys_refcursor,
                                    par$rc_limites_maq              OUT sys_refcursor,
                                    par$rc_saldos_periodo           OUT sys_refcursor,
                                    par$rc_cuentas                  OUT sys_refcursor,
                                    par$rc_valores                  OUT sys_refcursor,
                                    par$rc_terminos                 OUT sys_refcursor,
                                    par$rc_dato_bancario            OUT sys_refcursor,
                                    par$rc_direccion                OUT sys_refcursor,
                                    par$rc_planificacion_maquina    OUT sys_refcursor,
                                    par$rc_planificacion            OUT sys_refcursor,
                                    par$rc_canales                  OUT sys_refcursor,
                                    par$rc_programacion             OUT sys_refcursor,
                                    par$rc_divisas                  OUT sys_refcursor,                                    
                                    par$rc_denominaciones           OUT sys_refcursor,                                    
                                    par$rc_codigos_ajenos           OUT sys_refcursor,                                    
                                    par$rc_formularios              OUT sys_refcursor,                                  
                                    par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

  const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_doc_actual_id';
  var$inicio                    TIMESTAMP := SYSTIMESTAMP;
  var$err                       gepr_pcomon_###VERSION###.tipo$obs_ := '';
  var$cod_teller                gepr_pcomon_###VERSION###.tipo$cod_;
  var$des_teller                gepr_pcomon_###VERSION###.tipo$cod_;
  var$obs_detalle               gepr_pcomon_###VERSION###.tipo$obs_;
  var$cantidad_documentos       gepr_pcomon_###VERSION###.tipo$nel_;
  var$cantidad_cuentas          gepr_pcomon_###VERSION###.tipo$nel_;
  const$cod_parametro_tipo_doc CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchDocumento';
  var$oid_planificacion         gepr_pcomon_###VERSION###.tipo$oid_;
  var$tipo_documento            varchar2(4000);
  var$fyh_gestion              gepr_pcomon_###VERSION###.tipo$gmt_;
  var$oid_maquina              gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_periodo              gepr_pcomon_###VERSION###.tipo$oid_;
  var$cod_tipo_planificacion   gepr_pcomon_###VERSION###.tipo$cod_;
  var$cod_validaciones         gepr_pcomon_###VERSION###.tipo$cods_;
  var$des_validaciones         gepr_pcomon_###VERSION###.tipo$descs_;
  var$cod_ejecucion            gepr_pcomon_###VERSION###.tipo$nel_;
  var$cod_canal                gepr_pcomon_###VERSION###.tipo$cod_;
  var$cod_subcanal             gepr_pcomon_###VERSION###.tipo$cod_;
  var$oid_divisa               gepr_pcomon_###VERSION###.tipo$oid_;
  var$cod_sector               gepr_pcomon_###VERSION###.tipo$cod_;
  var$oid_pto_servicio         gepr_pcomon_###VERSION###.tipo$oid_;
  var$cod_cultura              gepr_pcomon_###VERSION###.tipo$cod_ := 'es';  
  var$cod_validaciones_int     gepr_pcomon_###VERSION###.tipo$cods_;
  var$des_validaciones_int    gepr_pcomon_###VERSION###.tipo$descs_;
  var$info_ejecucion           gepr_pcomon_###VERSION###.tipo$desc_;
  BEGIN
    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TAUXILIAR;

    /* #### Inicializar los cursores #### */
      
    OPEN par$rc_movimientos            FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK
    OPEN par$rc_limites_maq            FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK
    OPEN par$rc_saldos_periodo         FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK        
    OPEN par$rc_cuentas                FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK
    OPEN par$rc_valores                FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_terminos               FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK
    OPEN par$rc_direccion              FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK
    OPEN par$rc_dato_bancario          FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK    
    OPEN par$rc_planificacion          FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK    
    OPEN par$rc_canales                FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK    
    OPEN par$rc_programacion           FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_divisas                FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK
    OPEN par$rc_denominaciones         FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK    
    OPEN par$rc_formularios            FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK
    OPEN par$rc_planificacion_maquina  FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1; -- OK

    DELETE SAPR_GTT_TAUXILIAR;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Inicia llamada par$oid_llamada: ' || par$oid_llamada,
              par$cod_identificador   => ''); 

    IF par$oid_llamada IS NOT NULL THEN
      var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'par$cod_actual_id: ' || const$comilla_simple || par$cod_actual_id || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'par$info_ejecucion: ' || const$comilla_simple || par$info_ejecucion || const$comilla_simple || const$new_line;
      
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => var$obs_detalle,
              par$cod_identificador   => '');
    END IF;

    BEGIN 
      var$tipo_documento := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                                                      par$cod_pais => par$cod_pais,
                                                                      par$cod_parametro => const$cod_parametro_tipo_doc,
                                                                      par$cod_aplicacion => 'Genesis');

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Se obtuvo el valor de parametro EnviarDatosSwitchDocumento: ' || var$tipo_documento,
      par$cod_identificador   => '');

    EXCEPTION 
      WHEN no_data_found THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'No existe un valor para el codigo de parametro '|| const$cod_parametro_tipo_doc,
                par$cod_identificador   => '');
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| const$cod_parametro_tipo_doc);
            WHEN others THEN 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'Ocurrió un error al intentar obtener el const$cod_parametro_tipo_doc para el código: '|| const$cod_parametro_tipo_doc,
                par$cod_identificador   => '');
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'Ocurrió un error al intentar obtener el const$cod_parametro_tipo_doc para el código: '|| const$cod_parametro_tipo_doc);
    END;

    -- Obtengo el oid de la planificacion con el actual ID
    BEGIN
               
      SELECT PLANI.OID_PLANIFICACION into var$oid_planificacion FROM SAPR_TDOCUMENTO DOCU
      INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN
      INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_SECTOR = CUEN.OID_SECTOR AND MAQ.BOL_ACTIVO = 1
      INNER JOIN SAPR_TPLANXMAQUINA PXM ON PXM.OID_MAQUINA = MAQ.OID_MAQUINA AND PXM.BOL_ACTIVO = 1
      INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PXM.OID_PLANIFICACION AND PLANI.BOL_ACTIVO = 1
      WHERE DOCU.COD_ACTUAL_ID = par$cod_actual_id AND ROWNUM = 1;
    EXCEPTION
    WHEN  NO_DATA_FOUND THEN
       SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'No existe una planificacion para los movimientos con actual id:  '|| par$cod_actual_id,
                par$cod_identificador   => '');
      var$oid_planificacion := NULL;
    END;

    --Grabo en la tabla auxiliar todos los subcanales de la planificacion
      INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
      WITH CANALES AS (
              SELECT PC.OID_PLANIFICACION, C.OID_CANAL, C.COD_CANAL, C.DES_CANAL,
              SUBC.OID_SUBCANAL, SUBC.COD_SUBCANAL, SUBC.DES_SUBCANAL,
              PC.OID_PTO_SERVICIO CANAL_OID_PUNTO, PC.OID_MAQUINA CANAL_OID_MAQUINA, PSC.OID_PTO_SERVICIO SUBCANAL_OID_PUNTO, PSC.OID_MAQUINA SUBCANAL_OID_MAQUINA
                FROM SAPR_TPLANIFICACION PL
                INNER JOIN SAPR_TPLANXCANAL PC ON PL.OID_PLANIFICACION = PC.OID_PLANIFICACION AND PC.BOL_ACTIVO = 1
                INNER JOIN GEPR_TCANAL C ON C.OID_CANAL = PC.OID_CANAL
                LEFT JOIN SAPR_TPLANXSUBCANAL PSC ON PSC.OID_PLANXCANAL = PC.OID_PLANXCANAL AND PSC.BOL_ACTIVO = 1
                LEFT JOIN GEPR_TSUBCANAL SUBC ON SUBC.OID_SUBCANAL = PSC.OID_SUBCANAL
              WHERE PL.OID_PLANIFICACION = var$oid_planificacion
              )
              SELECT 
              SUBC.OID_SUBCANAL, 'COD_SCANAL_PLANI'
              FROM CANALES CAN
              INNER JOIN GEPR_TSUBCANAL SUBC ON CAN.OID_CANAL = SUBC.OID_CANAL
              WHERE CAN.OID_SUBCANAL IS NULL AND CAN.CANAL_OID_PUNTO IS NULL AND CAN.CANAL_OID_MAQUINA IS NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL AND CAN.SUBCANAL_OID_MAQUINA IS NULL
              UNION ALL
              SELECT CAN.OID_SUBCANAL , 'COD_SCANAL_PLANI'
              FROM CANALES CAN
              WHERE CAN.OID_SUBCANAL IS NOT NULL AND CAN.CANAL_OID_PUNTO IS NULL AND CAN.CANAL_OID_MAQUINA IS NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL AND CAN.SUBCANAL_OID_MAQUINA IS NULL;
           

           
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, OID_CAMPO7, COD_CALIFICADOR)
      SELECT DOCU.OID_DOCUMENTO, PERI.OID_PERIODO, PERI.OID_PLANIFICACION, OID_CUENTA_SALDO_ORIGEN, 'OID_DOCUMENTO'
      FROM SAPR_TDOCUMENTO DOCU
        INNER JOIN SAPR_TFORMULARIO FM ON FM.OID_FORMULARIO = DOCU.OID_FORMULARIO
        INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN
        LEFT JOIN SAPR_TPERIODOXDOCUMENTO PEDO ON PEDO.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
        LEFT JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PEDO.OID_PERIODO
        LEFT JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO
        LEFT JOIN SAPR_GTT_TAUXILIAR SUBPLANI ON SUBPLANI.OID_CAMPO1 = CUEN.OID_SUBCANAL AND SUBPLANI.COD_CALIFICADOR = 'COD_SCANAL_PLANI'
      WHERE 
        (PEDO.OID_DOCUMENTO IS NULL OR TIPE.COD_TIPO_PERIODO = 'AC') 
        AND COD_ACTUAL_ID = par$cod_actual_id
        AND var$tipo_documento LIKE '%' || FM.COD_FORMULARIO ||  '%';
      

      SELECT COUNT(1) INTO var$cantidad_documentos FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_DOCUMENTO';
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'Insert de documentos en sapr_gtt_tauxiliar: cantidad_documentos: ' ||var$cantidad_documentos|| ' registros. par$cod_actual_id: ' || par$cod_actual_id,
                par$cod_identificador   => '');
    
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,OID_CAMPO7, COD_CALIFICADOR)
        SELECT CUEN.OID_CUENTA, OID_CLIENTE, OID_SUBCLIENTE, OID_PTO_SERVICIO, OID_MAQUINA, 'CUENTAS' 
        FROM SAPR_GTT_TAUXILIAR AUX 
          INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA =  OID_CAMPO7
          INNER JOIN SAPR_TMAQUINA MAQU ON CUEN.OID_SECTOR = MAQU.OID_SECTOR
        WHERE AUX.COD_CALIFICADOR = 'OID_DOCUMENTO' ; 

      SELECT COUNT(1) INTO var$cantidad_cuentas FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'CUENTAS';
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'Insert de cuentas en sapr_gtt_tauxiliar: ' ||var$cantidad_cuentas|| ' registros.',
                par$cod_identificador   => '');

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'Inicia cursor par$rc_codigos_ajenos.',
                par$cod_identificador   => '');  

      OPEN par$rc_codigos_ajenos FOR
      SELECT CAJ.OID_TABLA_GENESIS, CAJ.COD_IDENTIFICADOR, CAJ.COD_AJENO, CAJ.DES_AJENO, COD_TIPO_TABLA_GENESIS 
      FROM SAPR_TCUENTA CUE
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'CUENTAS' AND AUX.OID_CAMPO1 = CUE.OID_CUENTA
        INNER JOIN GEPR_TCODIGO_AJENO CAJ ON CAJ.OID_TABLA_GENESIS = CUE.OID_SECTOR AND CAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
      GROUP BY CAJ.OID_TABLA_GENESIS, CAJ.COD_IDENTIFICADOR, CAJ.COD_AJENO, CAJ.DES_AJENO, COD_TIPO_TABLA_GENESIS
      UNION ALL
      SELECT CAJ.OID_TABLA_GENESIS, CAJ.COD_IDENTIFICADOR, CAJ.COD_AJENO, CAJ.DES_AJENO, COD_TIPO_TABLA_GENESIS 
      FROM SAPR_TCUENTA CUE
        INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = CUE.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLA ON PLA.OID_PLANTA = SEC.OID_PLANTA        
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'CUENTAS' AND AUX.OID_CAMPO1 = CUE.OID_CUENTA
        INNER JOIN GEPR_TCODIGO_AJENO CAJ ON CAJ.OID_TABLA_GENESIS = PLA.OID_DELEGACION AND CAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
      GROUP BY CAJ.OID_TABLA_GENESIS, CAJ.COD_IDENTIFICADOR, CAJ.COD_AJENO, CAJ.DES_AJENO, COD_TIPO_TABLA_GENESIS
      UNION ALL
      SELECT CAJ.OID_TABLA_GENESIS, CAJ.COD_IDENTIFICADOR, CAJ.COD_AJENO, CAJ.DES_AJENO, COD_TIPO_TABLA_GENESIS 
      FROM SAPR_TCUENTA CUE
        INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = CUE.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLA ON PLA.OID_PLANTA = SEC.OID_PLANTA        
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'CUENTAS' AND AUX.OID_CAMPO1 = CUE.OID_CUENTA
        INNER JOIN GEPR_TCODIGO_AJENO CAJ ON CAJ.OID_TABLA_GENESIS = PLA.OID_PLANTA AND CAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
      GROUP BY CAJ.OID_TABLA_GENESIS, CAJ.COD_IDENTIFICADOR, CAJ.COD_AJENO, CAJ.DES_AJENO, COD_TIPO_TABLA_GENESIS;


      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia cursor par$rc_cuentas.',
          par$cod_identificador   => '');  

      OPEN par$rc_cuentas FOR
        SELECT 
          CUEN.OID_CUENTA,
          CUEN.OID_DELEGACION,
          CODAJDELE.COD_AJENO as COD_DELEGACION, 
          CUEN.DES_DELEGACION, 
          CUEN.OID_PLANTA,
          CODAJPLANTA.COD_AJENO AS COD_PLANTA,
          CUEN.DES_PLANTA,
          CUEN.OID_MAQUINA,
          MAQ.BOL_ACTIVO,
          CUEN.OID_SECTOR,
          CUEN.COD_IDENTIFICACION_MAQUINA AS COD_MAQUINA,
          CUEN.DES_SECTOR AS DES_MAQUINA,
          CUEN.OID_CLIENTE,
          CODAJCLIENTE.COD_AJENO AS COD_CLIENTE,
          CUEN.DES_CLIENTE,
          CUEN.OID_SUBCLIENTE,
          CODAJSUBCLIENT.COD_AJENO AS COD_SUBCLIENTE,
          CUEN.DES_SUBCLIENTE,
          CUEN.OID_PTO_SERVICIO,
          CODAJPUNTOSERV.COD_AJENO AS COD_PTO_SERVICIO,
          CUEN.DES_PTO_SERVICIO,
          CUEN.OID_CANAL,
          CODAJCANAL.COD_AJENO AS COD_CANAL,
          CUEN.DES_CANAL,
          CUEN.OID_SUBCANAL,
          CODAJSUBCANAL.COD_AJENO AS COD_SUBCANAL,
          CUEN.DES_SUBCANAL
        FROM SAPR_TDOCUMENTO DOCU 
      INNER JOIN SAPR_VCUENTA CUEN ON DOCU.OID_CUENTA_SALDO_ORIGEN = CUEN.OID_CUENTA
      INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_MAQUINA = CUEN.OID_MAQUINA
      INNER JOIN SAPR_GTT_TAUXILIAR AUX ON DOCU.OID_DOCUMENTO = AUX.OID_CAMPO1 AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
      LEFT JOIN GEPR_TCODIGO_AJENO CODAJDELE ON CODAJDELE.OID_TABLA_GENESIS = CUEN.OID_DELEGACION 
              AND CODAJDELE.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION' 
              AND CODAJDELE.COD_IDENTIFICADOR = 'MAE' 
              AND CODAJDELE.BOL_ACTIVO = 1
      LEFT JOIN GEPR_TCODIGO_AJENO CODAJPLANTA ON CODAJPLANTA.OID_TABLA_GENESIS = CUEN.OID_PLANTA 
              AND CODAJPLANTA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA' 
              AND CODAJPLANTA.COD_IDENTIFICADOR = 'MAE' 
              AND CODAJPLANTA.BOL_ACTIVO = 1
      LEFT JOIN GEPR_TCODIGO_AJENO CODAJCLIENTE ON CODAJCLIENTE.OID_TABLA_GENESIS = CUEN.OID_CLIENTE 
              AND CODAJCLIENTE.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE' 
              AND CODAJCLIENTE.COD_IDENTIFICADOR = 'MAE' 
              AND CODAJCLIENTE.BOL_ACTIVO = 1
      LEFT JOIN GEPR_TCODIGO_AJENO CODAJSUBCLIENT ON CODAJSUBCLIENT.OID_TABLA_GENESIS = CUEN.OID_SUBCLIENTE 
              AND CODAJSUBCLIENT.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE' 
              AND CODAJSUBCLIENT.COD_IDENTIFICADOR = 'MAE' 
              AND CODAJSUBCLIENT.BOL_ACTIVO = 1
      LEFT JOIN GEPR_TCODIGO_AJENO CODAJPUNTOSERV ON CODAJPUNTOSERV.OID_TABLA_GENESIS = CUEN.OID_PTO_SERVICIO 
              AND CODAJPUNTOSERV.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' 
              AND CODAJPUNTOSERV.COD_IDENTIFICADOR = 'MAE' 
              AND CODAJPUNTOSERV.BOL_ACTIVO = 1
      LEFT JOIN GEPR_TCODIGO_AJENO CODAJCANAL ON CODAJCANAL.OID_TABLA_GENESIS = CUEN.OID_CANAL 
              AND CODAJCANAL.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL' 
              AND CODAJCANAL.COD_IDENTIFICADOR = 'MAE' 
              AND CODAJCANAL.BOL_ACTIVO = 1
      LEFT JOIN GEPR_TCODIGO_AJENO CODAJSUBCANAL ON CODAJSUBCANAL.OID_TABLA_GENESIS = CUEN.OID_SUBCANAL 
              AND CODAJSUBCANAL.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL' 
              AND CODAJSUBCANAL.COD_IDENTIFICADOR = 'MAE' 
              AND CODAJSUBCANAL.BOL_ACTIVO = 1;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Inicia cursor par$rc_direccion.',
              par$cod_identificador   => '');


      OPEN par$rc_direccion FOR 
      SELECT 
          OID_TABLA_GENESIS,
          'GEPR_TCLIENTE' AS COD_CALIFICADOR,
          DES_PAIS,
          DES_PROVINCIA,
          DES_CIUDAD,
          DES_NUMERO_TELEFONO,
          DES_EMAIL,
          COD_FISCAL,
          COD_POSTAL,
          DES_DIRECCION_LINEA_1,
          DES_DIRECCION_LINEA_2,
          DES_CAMPO_ADICIONAL_1,
          DES_CAMPO_ADICIONAL_2,
          DES_CAMPO_ADICIONAL_3,
          DES_CATEGORIA_ADICIONAL_1,
          DES_CATEGORIA_ADICIONAL_2,
          DES_CATEGORIA_ADICIONAL_3
      FROM
      SAPR_GTT_TAUXILIAR AUX 
      INNER JOIN GEPR_TDIRECCION DIREM ON DIREM.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE' 
                  AND DIREM.OID_TABLA_GENESIS = AUX.COD_CAMPO2
      WHERE AUX.COD_CALIFICADOR = 'CUENTAS'  
      UNION ALL     
      SELECT 
          OID_TABLA_GENESIS,
          'GEPR_TSUBCLIENTE' AS COD_CALIFICADOR,
          DES_PAIS,
          DES_PROVINCIA,
          DES_CIUDAD,
          DES_NUMERO_TELEFONO,
          DES_EMAIL,
          COD_FISCAL,
          COD_POSTAL,
          DES_DIRECCION_LINEA_1,
          DES_DIRECCION_LINEA_2,
          DES_CAMPO_ADICIONAL_1,
          DES_CAMPO_ADICIONAL_2,
          DES_CAMPO_ADICIONAL_3,
          DES_CATEGORIA_ADICIONAL_1,
          DES_CATEGORIA_ADICIONAL_2,
          DES_CATEGORIA_ADICIONAL_3
      FROM
      SAPR_GTT_TAUXILIAR AUX 
      INNER JOIN GEPR_TDIRECCION DIREM ON DIREM.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE' 
                  AND DIREM.OID_TABLA_GENESIS = AUX.COD_CAMPO3
      WHERE AUX.COD_CALIFICADOR = 'CUENTAS'      
      UNION ALL     
      SELECT 
          OID_TABLA_GENESIS,
          'GEPR_TPUNTO_SERVICIO' AS COD_CALIFICADOR,
          DES_PAIS,
          DES_PROVINCIA,
          DES_CIUDAD,
          DES_NUMERO_TELEFONO,
          DES_EMAIL,
          COD_FISCAL,
          COD_POSTAL,
          DES_DIRECCION_LINEA_1,
          DES_DIRECCION_LINEA_2,
          DES_CAMPO_ADICIONAL_1,
          DES_CAMPO_ADICIONAL_2,
          DES_CAMPO_ADICIONAL_3,
          DES_CATEGORIA_ADICIONAL_1,
          DES_CATEGORIA_ADICIONAL_2,
          DES_CATEGORIA_ADICIONAL_3
      FROM
      SAPR_GTT_TAUXILIAR AUX 
      INNER JOIN GEPR_TDIRECCION DIREM ON DIREM.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' 
                  AND DIREM.OID_TABLA_GENESIS = AUX.COD_CAMPO4
      WHERE AUX.COD_CALIFICADOR = 'CUENTAS';

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Inicia cursor par$rc_dato_bancario.',
              par$cod_identificador   => '');  

      --MELHORAR PARA TRAZER APENAS SUBCLIENTES E CLIENTES QUE ESTÃO NO AUXILIAR CUENTAS
      OPEN par$rc_dato_bancario FOR
      SELECT 
        DTBA.OID_CLIENTE, DTBA.OID_SUBCLIENTE, DTBA.OID_PTO_SERVICIO,
        CLI.COD_CLIENTE, CLI.DES_CLIENTE, 
        DTBA.COD_DOCUMENTO AS NUMERO_DOCUMENTO, 
        DTBA.COD_AGENCIA AS AGENCIA,
        DTBA.COD_CUENTA_BANCARIA AS NUMERO_CUENTA,
        DTBA.DES_TITULARIDAD AS TITULARIDAD,
        CODAJDIVI.COD_AJENO AS DIVISA,
        DTBA.COD_TIPO_CUENTA_BANCARIA AS TIPO,
        DTBA.DES_OBSERVACIONES AS OBSERVACIONES,
        DTBA.DES_CAMPO_ADICIONAL_1,
        DTBA.DES_CAMPO_ADICIONAL_2,
        DTBA.DES_CAMPO_ADICIONAL_3,
        DTBA.DES_CAMPO_ADICIONAL_4,
        DTBA.DES_CAMPO_ADICIONAL_5,
        DTBA.DES_CAMPO_ADICIONAL_6,
        DTBA.DES_CAMPO_ADICIONAL_7,
        DTBA.DES_CAMPO_ADICIONAL_8
      FROM SAPR_TDATO_BANCARIO DTBA
        INNER JOIN GEPR_TCLIENTE CLI ON DTBA.OID_BANCO = CLI.OID_CLIENTE
        INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = DTBA.OID_DIVISA 
        INNER JOIN GEPR_TCODIGO_AJENO CODAJDIVI ON CODAJDIVI.OID_TABLA_GENESIS = DIV.OID_DIVISA 
                    AND CODAJDIVI.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' 
                    AND CODAJDIVI.COD_IDENTIFICADOR = 'MAE' 
                    AND CODAJDIVI.BOL_ACTIVO = 1
        INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EXD ON DIV.OID_DIVISA = EXD.OID_DIVISA
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = EXD.OID_DOCUMENTO 
                    AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
      WHERE 
      DTBA.OID_CLIENTE  IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX1 WHERE COD_CALIFICADOR = 'CUENTAS' ) AND DTBA.BOL_ACTIVO = 1;
      
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia cursor par$rc_terminos.',
          par$cod_identificador   => '');


      OPEN par$rc_terminos FOR
        SELECT UPPER(TERM.COD_TERMINO) as COD_TERMINO
                , TEDO.OID_DOCUMENTO
                , TEDO.DES_VALOR
              FROM SAPR_TVALOR_TERMINOXDOCUMENTO TEDO
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON  AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'  AND  AUX.OID_CAMPO1 = TEDO.OID_DOCUMENTO
        INNER JOIN GEPR_TTERMINO TERM ON TERM.OID_TERMINO = TEDO.OID_TERMINO
      GROUP BY UPPER(TERM.COD_TERMINO), TEDO.OID_DOCUMENTO, TEDO.DES_VALOR;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'Inicia cursor par$rc_divisas.',
                par$cod_identificador   => '');

      
      /* CARGAR DATOS DE LAS DIVISAS */
      OPEN par$rc_divisas FOR
        WITH DIVISAS AS (
                SELECT TREF.OID_DIVISA
                  FROM SAPR_TTRANSACCION_EFECTIVO TREF
            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1= TREF.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
              GROUP BY TREF.OID_DIVISA
                  UNION
                SELECT TRMP.OID_DIVISA
                  FROM SAPR_TTRANSACCION_MEDIO_PAGO TRMP
            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = TRMP.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
              UNION 
              SELECT EFDO.OID_DIVISA
                  FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = EFDO.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
              
        )
      SELECT D.OID_DIVISA
        , CODAJ.COD_AJENO AS COD_ISO_DIVISA
        , DIV.DES_DIVISA
      FROM DIVISAS D
      INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = D.OID_DIVISA
      INNER JOIN GEPR_TCODIGO_AJENO CODAJ ON CODAJ.OID_TABLA_GENESIS = DIV.OID_DIVISA 
          AND CODAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' 
          AND CODAJ.COD_IDENTIFICADOR = 'MAE' 
          AND CODAJ.BOL_ACTIVO = 1;


      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia cursor par$rc_denominaciones.',
          par$cod_identificador   => '');

      /* CARGAR DATOS DE LAS DENOMINACIONES */
      OPEN par$rc_denominaciones FOR
        WITH DENOMINACIONES AS (
          SELECT TREF.OID_DIVISA, TREF.OID_DENOMINACION
          FROM SAPR_TTRANSACCION_EFECTIVO TREF
            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = TREF.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
          WHERE TREF.OID_DENOMINACION IS NOT NULL
          GROUP BY TREF.OID_DIVISA, TREF.OID_DENOMINACION                               
          UNION                                
          SELECT EFDO.OID_DIVISA, EFDO.OID_DENOMINACION
          FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = EFDO.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
          WHERE EFDO.OID_DENOMINACION IS NOT NULL
          GROUP BY EFDO.OID_DIVISA, EFDO.OID_DENOMINACION  
          )
          SELECT D.OID_DENOMINACION
              , CODAJ.COD_AJENO AS COD_DENOMINACION
              , DEN.DES_DENOMINACION
          FROM DENOMINACIONES D
          INNER JOIN GEPR_TDENOMINACION DEN ON DEN.OID_DIVISA = D.OID_DIVISA AND DEN.OID_DENOMINACION = D.OID_DENOMINACION
          INNER JOIN GEPR_TCODIGO_AJENO CODAJ ON CODAJ.OID_TABLA_GENESIS = DEN.OID_DENOMINACION 
              AND CODAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION' 
              AND CODAJ.COD_IDENTIFICADOR = 'MAE' 
              AND CODAJ.BOL_ACTIVO = 1;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Inicia cursor par$rc_formularios.',
              par$cod_identificador   => '');


      /* CARGAR DATOS DEL LOS FORMULARIOS */
      OPEN par$rc_formularios FOR
        SELECT FORM.OID_FORMULARIO, FORM.COD_FORMULARIO, FORM.DES_FORMULARIO
              FROM SAPR_TDOCUMENTO DOCU
          INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = DOCU.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
          INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = DOCU.OID_FORMULARIO
        GROUP BY FORM.OID_FORMULARIO, FORM.COD_FORMULARIO, FORM.DES_FORMULARIO;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Inicia cursor par$rc_movimientos.',
              par$cod_identificador   => '');

      /* CARGAR DATOS DEL LOS MOVIMIENTOS */
      OPEN par$rc_movimientos FOR
        SELECT DOCU.OID_DOCUMENTO
          , DOCU.COD_EXTERNO AS CODIGO 
          , to_char(DOCU.FYH_GESTION, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00'  AS FYH_GESTION
          ,TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + 
              CASE WHEN (DOCU.FYH_GESTION) >= DELE.FYH_VERANO_INICIO AND (DOCU.FYH_GESTION) <= DELE.FYH_VERANO_FIN 
                  THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) 
                  ELSE 0 END)/ 60), '09')) || ':' || 
                  TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + 
              CASE WHEN (DOCU.FYH_GESTION) >= DELE.FYH_VERANO_INICIO AND (DOCU.FYH_GESTION) <= DELE.FYH_VERANO_FIN 
              THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')) AS GMTDELE_FYH_GESTION
          , to_char(DOCU.gmt_creacion, 'YYYY-MM-DD HH24:MI:SS TZR') AS FECHA_REALIZACION
          ,TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + 
              CASE WHEN (DOCU.gmt_creacion) >= DELE.FYH_VERANO_INICIO AND (DOCU.gmt_creacion) <= DELE.FYH_VERANO_FIN 
                  THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) 
                  ELSE 0 END)/ 60), '09')) || ':' || 
                  TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + 
              CASE WHEN (DOCU.gmt_creacion) >= DELE.FYH_VERANO_INICIO AND (DOCU.gmt_creacion) <= DELE.FYH_VERANO_FIN 
              THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')) AS GMTDELE_FECHA_REALIZACION
          , CASE WHEN DOCU.FEC_CONTABLE IS NULL THEN NULL
                ELSE to_char(DOCU.FEC_CONTABLE, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00'  
                END AS FEC_CONTABLE
          ,TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + 
                  CASE WHEN (DOCU.FEC_CONTABLE) >= DELE.FYH_VERANO_INICIO AND (DOCU.FEC_CONTABLE) <= DELE.FYH_VERANO_FIN 
                      THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) 
                      ELSE 0 END)/ 60), '09')) || ':' || 
                      TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + 
                  CASE WHEN (DOCU.FEC_CONTABLE) >= DELE.FYH_VERANO_INICIO AND (DOCU.FEC_CONTABLE) <= DELE.FYH_VERANO_FIN 
                  THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')) AS GMTDELE_FEC_CONTABLE
          , DOCU.OID_CUENTA_ORIGEN
          , DOCU.OID_CUENTA_DESTINO
          , DOCU.OID_FORMULARIO
          , FORM.COD_FORMULARIO
          , FORM.DES_FORMULARIO
          , DOCU.COD_ACTUAL_ID
          , DOCU.COD_COLLECTION_ID
          , DOCU.BOL_ACREDITADO
          , DOCU.BOL_NOTIFICADO
          , '' as DISPONIBLE
          , CASE WHEN  DOCU.FYH_ACREDITACION is null THEN ''   ELSE to_char(DOCU.FYH_ACREDITACION, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00'  END AS FYH_ACREDITACION
          ,TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + 
              CASE WHEN (DOCU.FYH_ACREDITACION) >= DELE.FYH_VERANO_INICIO AND (DOCU.FYH_ACREDITACION) <= DELE.FYH_VERANO_FIN 
                  THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) 
                  ELSE 0 END)/ 60), '09')) || ':' || 
                  TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + 
              CASE WHEN (DOCU.FYH_ACREDITACION) >= DELE.FYH_VERANO_INICIO AND (DOCU.FYH_ACREDITACION) <= DELE.FYH_VERANO_FIN 
              THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')) AS GMTDELE_FYH_ACREDITACION
 
        FROM SAPR_TDOCUMENTO DOCU
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = DOCU.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
        INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = DOCU.OID_FORMULARIO
        INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = AUX.OID_CAMPO7 
        INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = CUEN.OID_SECTOR
        INNER JOIN GEPR_TPLANTA PLANT ON PLANT.OID_PLANTA = SECT.OID_PLANTA
        INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = PLANT.OID_DELEGACION;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Inicia cursor par$rc_limites_maq.',
              par$cod_identificador   => '');

      /* CARGAR DATOS DEL LOS LIMITES DE LA MAQUINA*/
      OPEN par$rc_limites_maq FOR          
        WITH LIMITES AS (
        SELECT
        '1' PRIORIDAD, -- PUNTO
        LIME.OID_DIVISA,
        LIME.NUM_LIMITE
        FROM SAPR_TLIMITE LIME
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO7 = LIME.OID_MAQUINA AND AUX.COD_CAMPO4 = LIME.OID_PTO_SERVICIO AND AUX.COD_CALIFICADOR = 'CUENTAS'
        WHERE LIME.BOL_ACTIVO = 1
        UNION
        SELECT
        '2' PRIORIDAD, -- MAQUINA
        LIME.OID_DIVISA,
        LIME.NUM_LIMITE
        FROM SAPR_TLIMITE LIME
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO7 = LIME.OID_MAQUINA AND AUX.COD_CALIFICADOR = 'CUENTAS'
        WHERE LIME.BOL_ACTIVO = 1 AND
        LIME.OID_PTO_SERVICIO IS NULL
        UNION
        SELECT
        '3' PRIORIDAD, -- PLANIFICACION
        LIME.OID_DIVISA,
        LIME.NUM_LIMITE
        FROM SAPR_TLIMITE LIME
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CAMPO3 = LIME.OID_PLANIFICACION AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
        WHERE LIME.BOL_ACTIVO = 1 AND
        LIME.OID_PTO_SERVICIO IS NULL AND
        LIME.OID_MAQUINA IS NULL
        )
        SELECT
        LIME.OID_DIVISA,
        LIME.NUM_LIMITE
        FROM LIMITES LIME
        WHERE LIME.PRIORIDAD = (SELECT MIN(LIME.PRIORIDAD) FROM LIMITES);

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Obtiene valores del documento, cuenta y efectivo.',
                par$cod_identificador   => '');
      

      SELECT CUEN.OID_MAQUINA, DOCU.FYH_GESTION, CUEN.OID_PTO_SERVICIO, CUEN.COD_CANAL, CUEN.COD_SUBCANAL, CUEN.COD_SECTOR, EFXDOC.OID_DIVISA
      INTO var$oid_maquina, var$fyh_gestion, var$oid_pto_servicio, var$cod_canal, var$cod_subcanal,  var$cod_sector, var$oid_divisa
      FROM SAPR_TDOCUMENTO DOCU 
      INNER JOIN SAPR_VCUENTA CUEN ON DOCU.OID_CUENTA_SALDO_ORIGEN = CUEN.OID_CUENTA
      LEFT JOIN SAPR_TEFECTIVOXDOCUMENTO EFXDOC ON DOCU.OID_DOCUMENTO = EFXDOC.OID_DOCUMENTO
      INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = DOCU.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
      and rownum = 1;

      var$obs_detalle := 'Obtiene valores Documento: ';
      var$obs_detalle := var$obs_detalle || 'var$oid_maquina: ' || const$comilla_simple || var$oid_maquina || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'var$fyh_gestion: ' || const$comilla_simple || var$fyh_gestion || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'var$oid_pto_servicio: ' || const$comilla_simple || var$oid_pto_servicio || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'var$cod_canal: ' || const$comilla_simple || var$cod_canal || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'var$cod_subcanal: ' || const$comilla_simple || var$cod_subcanal || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'var$cod_sector: ' || const$comilla_simple || var$cod_sector || const$comilla_simple || const$new_line;
      var$obs_detalle := var$obs_detalle || 'var$oid_divisa: ' || const$comilla_simple || var$oid_divisa || const$comilla_simple || const$new_line;
      
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,                            
      par$des_detalle     => var$obs_detalle,
      par$cod_identificador   => '');

       BEGIN
          SELECT PEDO.OID_PERIODO
              INTO var$oid_periodo 
          FROM SAPR_TPERIODOXDOCUMENTO PEDO
          INNER JOIN SAPR_TPERIODO PERI ON PEDO.OID_PERIODO = PERI.OID_PERIODO 
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON PERI.OID_TIPO_PERIODO = TIPE.OID_TIPO_PERIODO
          INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = PEDO.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO' AND ROWNUM = 1
          WHERE TIPE.COD_TIPO_PERIODO = 'AC'; 

           SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'El periodo relacionado al documento es var$oid_periodo: .' || var$oid_periodo,
                    par$cod_identificador   => '');

      EXCEPTION
          WHEN NO_DATA_FOUND THEN    
          var$oid_periodo := NULL;
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'El documento no esta relacionado a un periodo.',
                    par$cod_identificador   => '');
      END;

      IF var$oid_periodo IS NULL THEN
            SAPR_PPERIODO_###VERSION###.sverificar_periodo(par$oid_llamada            => par$oid_llamada,
                                        par$cod_canal              => var$cod_canal,
                                        par$cod_subcanal           => var$cod_subcanal,
                                        par$oid_divisa             => var$oid_divisa,
                                        par$cod_sector             => var$cod_sector,
                                        par$oid_pto_servicio       => var$oid_pto_servicio,
                                        par$cod_ajeno              => NULL,
                                        par$fyh_gestion            => var$fyh_gestion,
                                        par$cod_usuario            => par$cod_usuario,
                                        par$cod_cultura            => var$cod_cultura,
                                        par$info_ejecucion         => par$info_ejecucion,
                                        par$oid_maquina            => var$oid_maquina,
                                        par$oid_planificacion      => var$oid_planificacion,
                                        par$oid_periodo            => var$oid_periodo,
                                        par$cod_tipo_planificacion => var$cod_tipo_planificacion,
                                        par$cod_validaciones       => var$cod_validaciones_int,
                                        par$des_validaciones       => var$des_validaciones_int,
                                        par$cod_ejecucion          => par$cod_ejecucion);

            var$obs_detalle := 'Llamada sverificar_periodo: ';
            var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'par$cod_canal: ' || const$comilla_simple || var$cod_canal || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'par$cod_subcanal: ' || const$comilla_simple || var$cod_subcanal || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'par$cod_sector: ' || const$comilla_simple || var$cod_sector || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'par$oid_pto_servicio: ' || const$comilla_simple || var$oid_pto_servicio || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'par$cod_ajeno: ' || const$comilla_simple || 'NULL' || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'par$fyh_gestion: ' || const$comilla_simple ||  var$fyh_gestion || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'par$cod_usuario: ' || const$comilla_simple || par$cod_usuario || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'par$info_ejecucion: ' || const$comilla_simple || par$info_ejecucion || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'var$cod_cultura: ' || const$comilla_simple || var$cod_cultura || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'var$oid_maquina: ' || const$comilla_simple || var$oid_maquina || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'var$oid_planificacion: ' || const$comilla_simple || var$oid_planificacion || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'var$oid_periodo: ' || const$comilla_simple || var$oid_periodo || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'var$cod_tipo_planificacion: ' || const$comilla_simple || var$cod_tipo_planificacion || const$comilla_simple || const$new_line;
            var$obs_detalle := var$obs_detalle || 'var$info_ejecucion: ' || const$comilla_simple || var$info_ejecucion || const$comilla_simple || const$new_line;
            
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => var$obs_detalle,
            par$cod_identificador   => '');

      END IF;


      /* CARGAR DATOS DE SALDOS POR PERIODO*/
      OPEN par$rc_saldos_periodo FOR
        SELECT
        PE.OID_MAQUINA
        ,CALC.OID_DIVISA
        ,MAQ.COD_IDENTIFICACION AS COD_MAQUINA
        ,SUM(CALC.NUM_IMPORTE) IMPORTE      
        FROM SAPR_TPERIODO PE
        INNER JOIN SAPR_TCALCULO_EFECTIVO CALC ON PE.OID_PERIODO = CALC.OID_PERIODO
        INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_MAQUINA = PE.OID_MAQUINA
            WHERE PE.OID_PERIODO = var$oid_periodo
        GROUP BY
        PE.OID_MAQUINA
        ,CALC.OID_DIVISA
        ,MAQ.COD_IDENTIFICACION;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia cursor par$rc_valores.',
          par$cod_identificador   => '');

          /* CARGAR DATOS DEL LOS VALORES */
      OPEN par$rc_valores  FOR
        WITH EFECTIVOS AS (
              SELECT TREF.OID_DOCUMENTO
                    , TREF.OID_DIVISA
                    , TREF.OID_DENOMINACION
                    , TREF.COD_NIVEL_DETALLE
                    , TREF.BOL_DISPONIBLE
                    , TREF.NUM_IMPORTE
                    , TREF.NEL_CANTIDAD
                    , CODAJDIVI.COD_AJENO AS COD_DIVISA
                    , DIV.DES_DIVISA
                    , CODAJDENO.COD_AJENO AS COD_DENOMINACION
                    , DENO.DES_DENOMINACION
                FROM SAPR_TTRANSACCION_EFECTIVO TREF
                INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = TREF.OID_DOCUMENTO 
                            AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
                LEFT JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = TREF.OID_DIVISA
                LEFT JOIN GEPR_TCODIGO_AJENO CODAJDIVI ON CODAJDIVI.OID_TABLA_GENESIS = DIV.OID_DIVISA 
                          AND CODAJDIVI.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' 
                          AND CODAJDIVI.COD_IDENTIFICADOR = 'MAE' 
                          AND CODAJDIVI.BOL_ACTIVO = 1
                LEFT JOIN GEPR_TDENOMINACION DENO ON DENO.OID_DENOMINACION = TREF.OID_DENOMINACION
                LEFT JOIN GEPR_TCODIGO_AJENO CODAJDENO ON CODAJDENO.OID_TABLA_GENESIS = DENO.OID_DENOMINACION 
                          AND CODAJDENO.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION' 
                          AND CODAJDENO.COD_IDENTIFICADOR = 'MAE' 
                          AND CODAJDENO.BOL_ACTIVO = 1
      )          
        SELECT TREF.OID_DOCUMENTO
              , TREF.OID_DIVISA
              , NULL OID_DENOMINACION                 
              , TREF.COD_DIVISA
              , TREF.DES_DIVISA
              , NULL COD_DENOMINACION
              , NULL DES_DENOMINACION
              , 'T' COD_NIVEL_DETALLE
              , TREF.BOL_DISPONIBLE
              , SUM(TREF.NUM_IMPORTE) NUM_IMPORTE
              , NULL NEL_CANTIDAD
          FROM EFECTIVOS TREF
      --   WHERE par$filtro_disponible IS NULL OR TREF.BOL_DISPONIBLE = par$filtro_disponible
      GROUP BY TREF.OID_DOCUMENTO
              , TREF.OID_DIVISA
              , TREF.BOL_DISPONIBLE
              , TREF.COD_DIVISA
              , TREF.DES_DIVISA                                
      UNION ALL
        SELECT TREF.OID_DOCUMENTO
              , TREF.OID_DIVISA
              , TREF.OID_DENOMINACION
              , TREF.COD_DIVISA
              , TREF.DES_DIVISA
              , TREF.COD_DENOMINACION
              , TREF.DES_DENOMINACION
              , TREF.COD_NIVEL_DETALLE
              , TREF.BOL_DISPONIBLE
              , TREF.NUM_IMPORTE
              , TREF.NEL_CANTIDAD
          FROM EFECTIVOS TREF
          WHERE 1 = 1
            -- AND par$filtro_val_detallar = 1
            AND TREF.COD_NIVEL_DETALLE = 'D'
          --  AND (par$filtro_disponible IS NULL OR TREF.BOL_DISPONIBLE = par$filtro_disponible)
      UNION ALL 
      
      --Somente se for informar valores informativos
        SELECT EFDO.OID_DOCUMENTO
                , EFDO.OID_DIVISA  
                , EFDO.OID_DENOMINACION
                , CODAJDIVI.COD_AJENO AS COD_DIVISA
                , DIVI.DES_DIVISA
                , CODAJDENO.COD_AJENO AS COD_DENOMINACION
                , DENO.DES_DENOMINACION
                , EFDO.COD_NIVEL_DETALLE
                , NULL BOL_DISPONIBLE
                , SUM(EFDO.NUM_IMPORTE) NUM_IMPORTE
                , SUM(EFDO.NEL_CANTIDAD) NEL_CANTIDAD
              FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
              INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = EFDO.OID_DIVISA  
              INNER JOIN GEPR_TCODIGO_AJENO CODAJDIVI ON CODAJDIVI.OID_TABLA_GENESIS = DIVI.OID_DIVISA 
                          AND CODAJDIVI.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' 
                          AND CODAJDIVI.COD_IDENTIFICADOR = 'MAE' 
                          AND CODAJDIVI.BOL_ACTIVO = 1
              INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = EFDO.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO'
              LEFT JOIN GEPR_TDENOMINACION DENO ON DENO.OID_DENOMINACION = EFDO.OID_DENOMINACION
              LEFT JOIN GEPR_TCODIGO_AJENO CODAJDENO ON CODAJDENO.OID_TABLA_GENESIS = DENO.OID_DENOMINACION 
                          AND CODAJDENO.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION' 
                          AND CODAJDENO.COD_IDENTIFICADOR = 'MAE' 
                          AND CODAJDENO.BOL_ACTIVO = 1
            WHERE EFDO.OID_DOCUMENTO NOT IN (SELECT OID_DOCUMENTO FROM EFECTIVOS)
            AND EFDO.NUM_IMPORTE <> 0 and EFDO.COD_NIVEL_DETALLE = 'D'
          GROUP BY EFDO.OID_DOCUMENTO
                , EFDO.OID_DIVISA
                , EFDO.OID_DENOMINACION
                , EFDO.COD_NIVEL_DETALLE
                , CODAJDIVI.COD_AJENO 
                , DIVI.DES_DIVISA
                , CODAJDENO.COD_AJENO
                , DENO.DES_DENOMINACION
            UNION ALL 
        SELECT EFDO.OID_DOCUMENTO
                , EFDO.OID_DIVISA  
                , NULL OID_DENOMINACION
                , CODAJDIVI.COD_AJENO AS COD_DIVISA
                , DIVI.DES_DIVISA
                , NULL COD_DENOMINACION
                , NULL DES_DENOMINACION
                , 'T' COD_NIVEL_DETALLE
                , NULL BOL_DISPONIBLE
                , SUM(EFDO.NUM_IMPORTE) NUM_IMPORTE
                , NULL NEL_CANTIDAD
          FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
            INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = EFDO.OID_DIVISA
            INNER JOIN GEPR_TCODIGO_AJENO CODAJDIVI ON CODAJDIVI.OID_TABLA_GENESIS = DIVI.OID_DIVISA 
                    AND CODAJDIVI.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' 
                    AND CODAJDIVI.COD_IDENTIFICADOR = 'MAE' 
                    AND CODAJDIVI.BOL_ACTIVO = 1
            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = EFDO.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO' 
          WHERE  EFDO.OID_DOCUMENTO NOT IN (SELECT OID_DOCUMENTO FROM EFECTIVOS)
            AND EFDO.NUM_IMPORTE <> 0
          GROUP BY EFDO.OID_DOCUMENTO, EFDO.OID_DIVISA, CODAJDIVI.COD_AJENO, DIVI.DES_DIVISA
          --Fim valores informativos
            ;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicia cursor par$rc_planificacion_maquina.',
      par$cod_identificador   => ''); 
    

      OPEN par$rc_planificacion_maquina FOR
        SELECT MAQ.OID_MAQUINA, MAQ.COD_IDENTIFICACION, PTO.OID_PTO_SERVICIO, CODAJPUNTOSERV.COD_AJENO AS COD_PTO_SERVICIO, PTO.DES_PTO_SERVICIO
        FROM GEPR_TPUNTO_SERVICIO PTO
        INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_MAQUINA = PTO.OID_MAQUINA
        INNER JOIN SAPR_GTT_TAUXILIAR AUX_CUENTAS ON AUX_CUENTAS.OID_CAMPO7 = MAQ.OID_MAQUINA AND AUX_CUENTAS.COD_CALIFICADOR = 'CUENTAS'
        INNER JOIN SAPR_TPLANXMAQUINA PLMQ ON PLMQ.OID_MAQUINA = MAQ.OID_MAQUINA AND PLMQ.BOL_ACTIVO = 1
        LEFT JOIN GEPR_TCODIGO_AJENO CODAJPUNTOSERV ON CODAJPUNTOSERV.OID_TABLA_GENESIS = PTO.OID_PTO_SERVICIO 
            AND CODAJPUNTOSERV.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' 
            AND CODAJPUNTOSERV.COD_IDENTIFICADOR = 'MAE' 
            AND CODAJPUNTOSERV.BOL_ACTIVO = 1;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Inicia cursor par$rc_planificacion.',
            par$cod_identificador   => ''); 


      OPEN par$rc_planificacion  FOR
        SELECT P.OID_PLANIFICACION,
                    P.COD_PLANIFICACION,
                    P.DES_PLANIFICACION,
                    TO_CHAR( P.FYH_VIGENCIA_INICIO, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00' AS FYH_VIGENCIA_INICIO,
                    TRIM(TO_CHAR(ROUND((NVL(D.NEC_GMT_MINUTOS, 0) + 
                          CASE WHEN (P.FYH_VIGENCIA_INICIO) >= D.FYH_VERANO_INICIO AND (P.FYH_VIGENCIA_INICIO) <= D.FYH_VERANO_FIN 
                              THEN NVL(D.NEC_VERANO_AJUSTE, 0) 
                          ELSE 0 END)/ 60), '09')) || ':' || 
                              TRIM(TO_CHAR(MOD((NVL(D.NEC_GMT_MINUTOS, 0) + 
                          CASE WHEN (P.FYH_VIGENCIA_INICIO) >= D.FYH_VERANO_INICIO AND (P.FYH_VIGENCIA_INICIO) <= D.FYH_VERANO_FIN 
                          THEN NVL(D.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')) AS GMTDELE_FYH_VIGENCIA_INICIO,
                    CASE
                        WHEN P.FYH_VIGENCIA_FIN IS NULL THEN
                        NULL
                        ELSE
                        TO_CHAR(P.FYH_VIGENCIA_FIN, 'YYYY-MM-DD HH24:MI:SS') || ' +00:00'
                    END AS FYH_VIGENCIA_FIN,
                    CASE
                        WHEN P.FYH_VIGENCIA_FIN IS NULL THEN NULL
                        ELSE
                        TRIM(TO_CHAR(ROUND((NVL(D.NEC_GMT_MINUTOS, 0) + 
                          CASE WHEN (P.FYH_VIGENCIA_FIN) >= D.FYH_VERANO_INICIO AND (P.FYH_VIGENCIA_FIN) <= D.FYH_VERANO_FIN 
                              THEN NVL(D.NEC_VERANO_AJUSTE, 0) 
                          ELSE 0 END)/ 60), '09')) || ':' || 
                              TRIM(TO_CHAR(MOD((NVL(D.NEC_GMT_MINUTOS, 0) + 
                        CASE WHEN (P.FYH_VIGENCIA_FIN) >= D.FYH_VERANO_INICIO AND (P.FYH_VIGENCIA_FIN) <= D.FYH_VERANO_FIN 
                        THEN NVL(D.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09'))
                    END AS GMTDELE_FYH_VIGENCIA_FIN,
                    P.BOL_ACTIVO,
                    C.COD_CLIENTE,
                    C.DES_CLIENTE,
                    TP.COD_TIPO_PLANIFICACION,
                    TP.DES_TIPO_PLANIFICACION,
                    D.COD_DELEGACION,
                    D.DES_DELEGACION,
                    P.NEC_CONTINGENCIA,
                    NVL(P.BOL_AGR_SUBCANAL, 0) BOL_AGR_SUBCANAL,
                    NVL(P.BOL_AGR_PTO_SERVICIO, 0) BOL_AGR_PTO_SERVICIO,
                    NVL(P.BOL_AGR_FEC_CONTABLE, 0) BOL_AGR_FEC_CONTABLE
        FROM SAPR_GTT_TAUXILIAR AUX_CUENTAS
          INNER JOIN SAPR_TPLANXMAQUINA PLMQ ON PLMQ.OID_MAQUINA = AUX_CUENTAS.OID_CAMPO7
          INNER JOIN SAPR_TPLANIFICACION P ON P.OID_PLANIFICACION = PLMQ.OID_PLANIFICACION
          INNER JOIN GEPR_TCLIENTE C ON C.OID_CLIENTE = P.OID_CLIENTE
          INNER JOIN SAPR_TTIPO_PLANIFICACION TP ON P.OID_TIPO_PLANIFICACION = TP.OID_TIPO_PLANIFICACION
          INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
          --INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = EFDO.OID_DOCUMENTO AND AUX.COD_CALIFICADOR = 'OID_DOCUMENTO' 
          WHERE  AUX_CUENTAS.COD_CALIFICADOR = 'CUENTAS' AND PLMQ.BOL_ACTIVO = 1

                GROUP BY P.OID_PLANIFICACION,
                        P.COD_PLANIFICACION,
                        P.DES_PLANIFICACION,
                        P.OID_DELEGACION,
                        P.FYH_VIGENCIA_INICIO,
                        TRIM(TO_CHAR(ROUND((NVL(D.NEC_GMT_MINUTOS, 0) + 
                          CASE WHEN (P.FYH_VIGENCIA_INICIO) >= D.FYH_VERANO_INICIO AND (P.FYH_VIGENCIA_INICIO) <= D.FYH_VERANO_FIN 
                              THEN NVL(D.NEC_VERANO_AJUSTE, 0) 
                          ELSE 0 END)/ 60), '09')) || ':' || 
                              TRIM(TO_CHAR(MOD((NVL(D.NEC_GMT_MINUTOS, 0) + 
                          CASE WHEN (P.FYH_VIGENCIA_INICIO) >= D.FYH_VERANO_INICIO AND (P.FYH_VIGENCIA_INICIO) <= D.FYH_VERANO_FIN 
                          THEN NVL(D.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')),
                        P.FYH_VIGENCIA_FIN,
                        CASE
                        WHEN P.FYH_VIGENCIA_FIN IS NULL THEN NULL
                        ELSE
                        TRIM(TO_CHAR(ROUND((NVL(D.NEC_GMT_MINUTOS, 0) + 
                          CASE WHEN (P.FYH_VIGENCIA_FIN) >= D.FYH_VERANO_INICIO AND (P.FYH_VIGENCIA_FIN) <= D.FYH_VERANO_FIN 
                              THEN NVL(D.NEC_VERANO_AJUSTE, 0) 
                          ELSE 0 END)/ 60), '09')) || ':' || 
                              TRIM(TO_CHAR(MOD((NVL(D.NEC_GMT_MINUTOS, 0) + 
                        CASE WHEN (P.FYH_VIGENCIA_FIN) >= D.FYH_VERANO_INICIO AND (P.FYH_VIGENCIA_FIN) <= D.FYH_VERANO_FIN 
                        THEN NVL(D.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09'))
                        END,
                        P.BOL_ACTIVO,
                        C.COD_CLIENTE,
                        C.DES_CLIENTE,
                        TP.COD_TIPO_PLANIFICACION,
                        TP.DES_TIPO_PLANIFICACION,
                        D.COD_DELEGACION,
                        D.DES_DELEGACION,
                        P.NEC_CONTINGENCIA,
                        P.BOL_AGR_SUBCANAL,
                        P.BOL_AGR_PTO_SERVICIO,
                        P.BOL_AGR_FEC_CONTABLE;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicia cursor par$rc_canales.',
      par$cod_identificador   => '');

    
    /* Canales */
    OPEN par$rc_canales FOR
      WITH CANALES AS (
        SELECT PC.OID_PLANIFICACION, C.OID_CANAL, CODAJCANAL.COD_AJENO AS COD_CANAL, C.DES_CANAL,
        SUBC.OID_SUBCANAL, CODAJSUBCANAL.COD_AJENO AS COD_SUBCANAL, SUBC.DES_SUBCANAL,
        PC.OID_PTO_SERVICIO CANAL_OID_PUNTO, PC.OID_MAQUINA CANAL_OID_MAQUINA, PSC.OID_PTO_SERVICIO SUBCANAL_OID_PUNTO, PSC.OID_MAQUINA SUBCANAL_OID_MAQUINA
        FROM SAPR_GTT_TAUXILIAR AUX_CUENTAS
          INNER JOIN SAPR_TPLANXMAQUINA PLMQ ON PLMQ.OID_MAQUINA = AUX_CUENTAS.OID_CAMPO7 AND AUX_CUENTAS.COD_CALIFICADOR = 'CUENTAS' AND PLMQ.BOL_ACTIVO = 1
          INNER JOIN SAPR_TPLANXCANAL PC ON PLMQ.OID_PLANIFICACION = PC.OID_PLANIFICACION AND PC.BOL_ACTIVO = 1
          INNER JOIN GEPR_TCANAL C ON C.OID_CANAL = PC.OID_CANAL
          LEFT JOIN SAPR_TPLANXSUBCANAL PSC ON PSC.OID_PLANXCANAL = PC.OID_PLANXCANAL AND PSC.BOL_ACTIVO = 1
          LEFT JOIN GEPR_TSUBCANAL SUBC ON SUBC.OID_SUBCANAL = PSC.OID_SUBCANAL
          LEFT JOIN GEPR_TCODIGO_AJENO CODAJCANAL ON CODAJCANAL.OID_TABLA_GENESIS = C.OID_CANAL
                    AND CODAJCANAL.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL' 
                    AND CODAJCANAL.COD_IDENTIFICADOR = 'MAE' 
                    AND CODAJCANAL.BOL_ACTIVO = 1
          LEFT JOIN GEPR_TCODIGO_AJENO CODAJSUBCANAL ON CODAJSUBCANAL.OID_TABLA_GENESIS = SUBC.OID_SUBCANAL
                    AND CODAJSUBCANAL.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL' 
                    AND CODAJSUBCANAL.COD_IDENTIFICADOR = 'MAE' 
                    AND CODAJSUBCANAL.BOL_ACTIVO = 1
        )
        SELECT CAN.OID_PLANIFICACION, CAN.OID_CANAL, CAN.COD_CANAL, CAN.DES_CANAL,
          SUBC.OID_SUBCANAL, SUBC.COD_SUBCANAL, SUBC.DES_SUBCANAL,
          CAN.CANAL_OID_PUNTO , CAN.CANAL_OID_MAQUINA ,
          CAN.SUBCANAL_OID_PUNTO , CAN.SUBCANAL_OID_MAQUINA ,
          'PLANIFICACION' TIPO_CANAL
        FROM CANALES CAN
        INNER JOIN GEPR_TSUBCANAL SUBC ON CAN.OID_CANAL = SUBC.OID_CANAL
        WHERE CAN.OID_SUBCANAL IS NULL AND CAN.CANAL_OID_PUNTO IS NULL AND CAN.CANAL_OID_MAQUINA IS NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL AND CAN.SUBCANAL_OID_MAQUINA IS NULL
        UNION ALL
        SELECT CAN.OID_PLANIFICACION, CAN.OID_CANAL, CAN.COD_CANAL, CAN.DES_CANAL,
          CAN.OID_SUBCANAL, CAN.COD_SUBCANAL, CAN.DES_SUBCANAL,
          CAN.CANAL_OID_PUNTO , CAN.CANAL_OID_MAQUINA ,
          CAN.SUBCANAL_OID_PUNTO , CAN.SUBCANAL_OID_MAQUINA ,
          'PLANIFICACION' TIPO_CANAL
        FROM CANALES CAN
        WHERE CAN.OID_SUBCANAL IS NOT NULL AND CAN.CANAL_OID_PUNTO IS NULL AND CAN.CANAL_OID_MAQUINA IS NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL AND CAN.SUBCANAL_OID_MAQUINA IS NULL
        UNION ALL
        SELECT CAN.OID_PLANIFICACION, CAN.OID_CANAL, CAN.COD_CANAL, CAN.DES_CANAL,
          SUBC.OID_SUBCANAL, SUBC.COD_SUBCANAL, SUBC.DES_SUBCANAL,
          CAN.CANAL_OID_PUNTO , CAN.CANAL_OID_MAQUINA ,
          CAN.SUBCANAL_OID_PUNTO , CAN.SUBCANAL_OID_MAQUINA ,
          'MAQUINA' TIPO_CANAL
        FROM CANALES CAN
        INNER JOIN GEPR_TSUBCANAL SUBC ON CAN.OID_CANAL = SUBC.OID_CANAL
        WHERE CAN.OID_SUBCANAL IS NULL AND CAN.CANAL_OID_MAQUINA IS NOT NULL AND CAN.CANAL_OID_PUNTO IS NULL 
        UNION ALL
        SELECT CAN.OID_PLANIFICACION, CAN.OID_CANAL, CAN.COD_CANAL, CAN.DES_CANAL,
          CAN.OID_SUBCANAL, CAN.COD_SUBCANAL, CAN.DES_SUBCANAL,
          CAN.CANAL_OID_PUNTO , CAN.CANAL_OID_MAQUINA ,
          CAN.SUBCANAL_OID_PUNTO , CAN.SUBCANAL_OID_MAQUINA ,
          'MAQUINA' TIPO_CANAL
        FROM CANALES CAN
        WHERE CAN.OID_SUBCANAL IS NOT NULL AND CAN.SUBCANAL_OID_MAQUINA IS NOT NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL 
        UNION ALL
        SELECT CAN.OID_PLANIFICACION, CAN.OID_CANAL, CAN.COD_CANAL, CAN.DES_CANAL,
          SUBC.OID_SUBCANAL, SUBC.COD_SUBCANAL, SUBC.DES_SUBCANAL,
          CAN.CANAL_OID_PUNTO , CAN.CANAL_OID_MAQUINA ,
          CAN.SUBCANAL_OID_PUNTO , CAN.SUBCANAL_OID_MAQUINA ,
          'PUNTO' TIPO_CANAL
        FROM CANALES CAN
        INNER JOIN GEPR_TSUBCANAL SUBC ON CAN.OID_CANAL = SUBC.OID_CANAL
        WHERE CAN.OID_SUBCANAL IS NULL AND CANAL_OID_PUNTO IS NOT NULL
        UNION ALL
        SELECT CAN.OID_PLANIFICACION, CAN.OID_CANAL, CAN.COD_CANAL, CAN.DES_CANAL,
          CAN.OID_SUBCANAL, CAN.COD_SUBCANAL, CAN.DES_SUBCANAL,
          CAN.CANAL_OID_PUNTO , CAN.CANAL_OID_MAQUINA ,
          CAN.SUBCANAL_OID_PUNTO , CAN.SUBCANAL_OID_MAQUINA ,
          'PUNTO' TIPO_CANAL
        FROM CANALES CAN
        WHERE CAN.OID_SUBCANAL IS NOT NULL AND CAN.CANAL_OID_PUNTO IS NOT NULL AND CAN.SUBCANAL_OID_PUNTO IS NOT NULL
      ;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicia cursor par$rc_programacion.',
      par$cod_identificador   => '');

    OPEN par$rc_programacion FOR
      SELECT PG.OID_PLANIFICACION,
               PG.NEC_DIA_INICIO,
               PG.FYH_HORA_INICIO,
               PG.NEC_DIA_FIN,
               PG.FYH_HORA_FIN
      FROM SAPR_GTT_TAUXILIAR AUX_CUENTAS
                    INNER JOIN SAPR_TPLANXMAQUINA PLMQ ON PLMQ.OID_MAQUINA = AUX_CUENTAS.OID_CAMPO7
                    INNER JOIN SAPR_TPLANXPROGRAMACION PG ON PG.OID_PLANIFICACION = PLMQ.OID_PLANIFICACION
          WHERE AUX_CUENTAS.COD_CALIFICADOR = 'CUENTAS' 
         GROUP BY PG.OID_PLANIFICACION,
                  PG.NEC_DIA_INICIO,
                  PG.FYH_HORA_INICIO,
                  PG.NEC_DIA_FIN,
                  PG.FYH_HORA_FIN
         ORDER BY PG.NEC_DIA_FIN, PG.FYH_HORA_FIN;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Finaliza srecuperar_doc_actual_id.',
              par$cod_identificador   => '');

    /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
      WHEN OTHERS THEN
        ROLLBACK;

        var$err := gepr_putilidades_###VERSION###.fformat_error;
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Error '|| var$err,
                  par$cod_identificador   => '');
        gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func,
                  0, 0, 0, 0, 0, 0, var$inicio, null, 'R', par$info_ejecucion, var$err, par$cod_usuario, par$cod_ejecucion);

END srecuperar_doc_actual_id;


procedure srecuperar_actual_ids
(
    par$oid_llamada            IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$acod_externo            IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$acollection             IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aactual_id              IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$rc_datos                OUT sys_refcursor
) IS 
 const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_actual_ids';
 var$obs_detalle             gepr_pcomon_###VERSION###.tipo$obs_;
begin

    /* Limpio la tabla auxiliar */
    DELETE FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR IN( 'BUSQUEDA_COD_EXTERNO', 'BUSQUEDA_COLLECTION', 'BUSQUEDA_ACTUAL_ID');

    IF par$oid_llamada IS NOT NULL THEN
        var$obs_detalle := var$obs_detalle || 'par$oid_llamada: ' || const$comilla_simple || par$oid_llamada || const$comilla_simple || const$new_line;

        FOR idx IN par$acod_externo.first .. par$acod_externo.last LOOP
            var$obs_detalle := var$obs_detalle || 'par$acod_externo('|| idx ||'): ' || par$acod_externo(idx) || const$new_line;
        END LOOP;
        FOR idx IN par$acollection.first .. par$acollection.last LOOP
            var$obs_detalle := var$obs_detalle || 'par$acollection('|| idx ||'): ' || par$acollection(idx) || const$new_line;
        END LOOP;
         FOR idx IN par$aactual_id.first .. par$aactual_id.last LOOP
            var$obs_detalle := var$obs_detalle || 'par$aactual_id('|| idx ||'): ' || par$aactual_id(idx) || const$new_line;
        END LOOP;
     
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => var$obs_detalle,
                par$cod_identificador   => '');
    END IF;

    /* #### Inicializar los cursores #### */
    OPEN par$rc_datos   FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    
    if par$acod_externo.count>0 then
        for idx in par$acod_externo.first .. par$acod_externo.last  loop
            if par$acod_externo(idx) <> ''  or  par$acod_externo(idx) is not null then
                INSERT INTO SAPR_GTT_TAUXILIAR (COD_CAMPO2, COD_CALIFICADOR, OID_CAMPO1)
                VALUES (par$acod_externo(idx), 'BUSQUEDA_COD_EXTERNO', sys_guid() );
                
                 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'INSERT INTO SAPR_GTT_AUXILIAR: ' || par$acod_externo(idx) || ', BUSQUEDA_COD_EXTERNO' ,
                par$cod_identificador   => '');
            END IF;

        end loop;
    end if;

    if par$acollection.count>0 then
        for idx in par$acollection.first .. par$acollection.last  loop
            if par$acollection(idx) <> '' or par$acollection(idx) is not null then
                INSERT INTO SAPR_GTT_TAUXILIAR (COD_CAMPO2, COD_CALIFICADOR, OID_CAMPO1)
                VALUES (par$acollection(idx), 'BUSQUEDA_COLLECTION', sys_guid());
                   
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'INSERT INTO SAPR_GTT_AUXILIAR: ' || par$acollection(idx) || ', BUSQUEDA_COLLECTION' ,
                par$cod_identificador   => '');
            end if;

        end loop;
    end if;

    if par$aactual_id.count>0 then
        for idx in par$aactual_id.first .. par$aactual_id.last  loop
            if par$aactual_id(idx) <> '' or par$aactual_id(idx) is not null then
                INSERT INTO SAPR_GTT_TAUXILIAR (COD_CAMPO2, COD_CALIFICADOR, OID_CAMPO1)
                VALUES (par$aactual_id(idx), 'BUSQUEDA_ACTUAL_ID', sys_guid());
                    
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'INSERT INTO SAPR_GTT_AUXILIAR: ' || par$aactual_id(idx) || ', BUSQUEDA_ACTUAL_ID' ,
                par$cod_identificador   => '');
            end if;

        end loop;
    end if;

    OPEN par$rc_datos FOR
        SELECT distinct COD_ACTUAL_ID, COD_EXTERNO, COD_COLLECTION_ID
        FROM
            (
            SELECT
                COD_ACTUAL_ID, COD_EXTERNO, COD_COLLECTION_ID
            FROM    SAPR_TDOCUMENTO
            WHERE
                COD_COLLECTION_ID IN 
                (
                    SELECT COD_CAMPO2
                    FROM   SAPR_GTT_TAUXILIAR
                    where   COD_CALIFICADOR='BUSQUEDA_COLLECTION' 
                )
            UNION
            SELECT
                COD_ACTUAL_ID, COD_EXTERNO, COD_COLLECTION_ID
            FROM    SAPR_TDOCUMENTO
            WHERE
                COD_EXTERNO IN 
                (
                    SELECT COD_CAMPO2
                    FROM   SAPR_GTT_TAUXILIAR
                    where   COD_CALIFICADOR='BUSQUEDA_COD_EXTERNO' 
                )
            UNION
            SELECT
                COD_ACTUAL_ID, COD_EXTERNO, COD_COLLECTION_ID
            FROM    SAPR_TDOCUMENTO
            WHERE
                COD_ACTUAL_ID IN 
                (
                    SELECT COD_CAMPO2
                    FROM   SAPR_GTT_TAUXILIAR
                    where   COD_CALIFICADOR='BUSQUEDA_ACTUAL_ID'
                )
            ) tabla
        ;
end srecuperar_actual_ids;

/* Upsert de SAPR_TMOVIMIENTO_DETALLE */
PROCEDURE supd_mov_detalle(par$oid_movimiento           IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_movimiento_detalle      IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                        par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$fec_movimiento              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$cod_denominacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$des_denominacion            IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$bol_billete                 IN gepr_pcomon_###VERSION###.tipo$nel_,  
                        par$nel_cantidad                IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$num_importe                 IN gepr_pcomon_###VERSION###.tipo$imp_) IS
    /* variables locales */
    var$existe                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$sumCantidad gepr_pcomon_###VERSION###.tipo$imp_ := 0;
    BEGIN
        BEGIN
            SELECT COUNT(1) INTO var$existe FROM SAPR_TMOVIMIENTO_DETALLE 
            WHERE OID_MOVIMIENTO = par$oid_movimiento AND COD_PAIS = par$cod_pais AND FEC_MOVIMIENTO = par$fec_movimiento AND COD_DENOMINACION = par$cod_denominacion 
            AND BOL_BILLETE = par$bol_billete AND NEL_CANTIDAD = par$nel_cantidad AND NUM_IMPORTE = par$num_importe;
        END;
        IF var$existe = 0 THEN
            
            par$oid_movimiento_detalle := sys_guid();

            INSERT INTO SAPR_TMOVIMIENTO_DETALLE (
             OID_MOVIMIENTO
            ,OID_MOVIMIENTO_DETALLE
            ,COD_PAIS
            ,FEC_MOVIMIENTO
            ,COD_DENOMINACION
            ,DES_DENOMINACION
            ,BOL_BILLETE
            ,NEL_CANTIDAD
            ,NUM_IMPORTE)
            VALUES
            (par$oid_movimiento
            ,par$oid_movimiento_detalle
            ,par$cod_pais
            ,par$fec_movimiento
            ,par$cod_denominacion
            ,par$des_denominacion
            ,par$bol_billete
            ,par$nel_cantidad
            ,par$num_importe
            );
            --Actualizar la columna NEL_CANTIDAD (suma SAPR_TMOVIMIENTO_DETALLE.NEL_CANTIDAD) de la tabla SAPR_TMOVIMIENTO
            SELECT SUM(MD.NEL_CANTIDAD) into var$sumCantidad fROM SAPR_TMOVIMIENTO_DETALLE MD
            WHERE MD.OID_MOVIMIENTO = par$oid_movimiento;

            UPDATE SAPR_TMOVIMIENTO MD
            SET MD.NEL_CANTIDAD = var$sumCantidad
            WHERE MD.OID_MOVIMIENTO = par$oid_movimiento;
        END IF;
 END supd_mov_detalle;



/* Upsert de SAPR_TMOVIMIENTO_CAMPO_EXTRA */
PROCEDURE supd_mov_campo_extra(par$oid_movimiento           IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_mov_campo_extra         IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                        par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$fec_movimiento              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$cod_campo_extra             IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$des_campo_extra             IN gepr_pcomon_###VERSION###.tipo$desc_) IS
    /* variables locales */
    var$existe                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    BEGIN
        BEGIN
           SELECT COUNT(1) INTO var$existe FROM SAPR_TMOVIMIENTO_CAMPO_EXTRA 
            WHERE OID_MOVIMIENTO = par$oid_movimiento AND COD_PAIS = par$cod_pais AND FEC_MOVIMIENTO = par$fec_movimiento AND COD_CAMPO_EXTRA = par$cod_campo_extra 
            AND DES_VALOR_CAMPO_EXTRA = par$des_campo_extra;
        END;
        IF var$existe = 0 THEN
        
            par$oid_mov_campo_extra := sys_guid();

            INSERT INTO SAPR_TMOVIMIENTO_CAMPO_EXTRA (
             OID_MOVIMIENTO
            ,OID_MOVIMIENTO_CAMPO_EXTRA
            ,COD_PAIS
            ,FEC_MOVIMIENTO
            ,COD_CAMPO_EXTRA
            ,DES_VALOR_CAMPO_EXTRA)
            VALUES
            (par$oid_movimiento
            ,par$oid_mov_campo_extra
            ,par$cod_pais
            ,par$fec_movimiento
            ,par$cod_campo_extra
            ,par$des_campo_extra);
        END IF;
 END supd_mov_campo_extra;
 
 
/* Upsert de SAPR_TMOVIMIENTO */
PROCEDURE supd_movimiento(par$OID_MOVIMIENTO    in out gepr_pcomon_###VERSION###.tipo$oid_,
    par$OID_DOCUMENTO     in gepr_pcomon_###VERSION###.tipo$oid_,
    par$COD_PAIS          in gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_DELEGACION_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_DELEGACION_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_DELEGACION      IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_CLIENTE_OPE     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_CLIENTE_MDM     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_CLIENTE         IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$DES_CLIENTE_OPE     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_SUBCLIENTE_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_SUBCLIENTE_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_SUBCLIENTE      IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$DES_SUBCLIENTE_OPE  IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_PTO_SERVICIO_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_PTO_SERVICIO_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_PTO_SERVICIO      IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$DES_PTO_SERVICIO_OPE  IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_CANAL             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_CANAL             IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_SUBCANAL          IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_SUBCANAL          IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_DEVICEID          IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_MAQUINA           IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$FEC_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$fyh_,
    par$HOR_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$FYH_CONTABLE          IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$COD_TIPO_MOVIMIENTO   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_TIPO_MOVIMIENTO   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_GRUPO_MOVIMIENTO  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_COMPROBANTE       IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_ACTUAL_ID         IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_COLLECTION_ID     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$BOL_NOTIFICADO        IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$FYH_NOTIFICACION      IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$BOL_ACREDITADO        IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$FYH_ACREDITACION      IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$COD_PLANIFICACION     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_PLANIFICACION     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_PLANIF_BANCO_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_PLANIF_BANCO_MDM    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_PLANIF_BANCO      IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$FYH_PLANIF_PERIODO_INI  IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$FYH_PLANIF_PERIODO_FIN  IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$COD_TIPO_PLANIFICACION  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_TIPO_PLANIFICACION  IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_DIVISA            IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_DIVISA            IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$NUM_IMPORTE           IN gepr_pcomon_###VERSION###.tipo$imp_,
    par$BOL_VIRTUAL           IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$GMT_CREACION          IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$COD_USUARIO           IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    /* variables locales */
    var$oid_movimiento           gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_documento            gepr_pcomon_###VERSION###.tipo$oid_;
    var$qry_update               gepr_pcomon_###VERSION###.tipo$obs_ ;
    var$gmt_zero                 gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$sumCantidad gepr_pcomon_###VERSION###.tipo$imp_ := 0;
BEGIN
    /*Busco el oid_documento para garantizar que exista*/
    
    BEGIN
        SELECT OID_DOCUMENTO, OID_MOVIMIENTO
        INTO var$oid_documento, var$oid_movimiento
        FROM SAPR_TMOVIMIENTO
        WHERE OID_DOCUMENTO = par$oid_documento;

        SELECT SUM(MD.NEL_CANTIDAD) into var$sumCantidad 
          fROM SAPR_TMOVIMIENTO_DETALLE MD
        WHERE MD.OID_MOVIMIENTO = par$oid_movimiento;


    EXCEPTION WHEN no_data_found THEN
        var$oid_documento := NULL;
        var$oid_movimiento := NULL;
    END;
    IF var$oid_movimiento IS NULL THEN
        par$oid_movimiento := sys_guid();
    END IF;
    IF var$oid_documento IS NULL then
       
        INSERT INTO SAPR_TMOVIMIENTO (
        OID_MOVIMIENTO
        ,OID_DOCUMENTO
        ,COD_PAIS          
        ,COD_DELEGACION_OPE  
        ,COD_DELEGACION_MDM 
        ,DES_DELEGACION      
        ,COD_CLIENTE_OPE     
        ,COD_CLIENTE_MDM     
        ,DES_CLIENTE
        ,DES_CLIENTE_OPE
        ,COD_SUBCLIENTE_OPE  
        ,COD_SUBCLIENTE_MDM  
        ,DES_SUBCLIENTE
        ,DES_SUBCLIENTE_OPE
        ,COD_PTO_SERVICIO_OPE  
        ,COD_PTO_SERVICIO_MDM  
        ,DES_PTO_SERVICIO
        ,DES_PTO_SERVICIO_OPE
        ,COD_CANAL             
        ,DES_CANAL             
        ,COD_SUBCANAL          
        ,DES_SUBCANAL          
        ,COD_DEVICEID          
        ,DES_MAQUINA       
        ,FEC_MOVIMIENTO        
        ,HOR_MOVIMIENTO        
        ,FYH_CONTABLE          
        ,COD_TIPO_MOVIMIENTO   
        ,DES_TIPO_MOVIMIENTO   
        ,COD_GRUPO_MOVIMIENTO  
        ,COD_MOVIMIENTO
        ,COD_COMPROBANTE  
        ,COD_ACTUAL_ID         
        ,COD_COLLECTION_ID     
        ,BOL_NOTIFICADO        
        ,FYH_NOTIFICACION      
        ,BOL_ACREDITADO        
        ,FYH_ACREDITACION      
        ,COD_PLANIFICACION     
        ,DES_PLANIFICACION     
        ,COD_PLANIFICACION_BANCO_OPE   
        ,COD_PLANIFICACION_BANCO_MDM   
        ,DES_PLANIFICACION_BANCO     
        ,FYH_PLANIFICACION_PERIODO_INI 
        ,FYH_PLANIFICACION_PERIODO_FIN     
        ,COD_TIPO_PLANIFICACION     
        ,DES_TIPO_PLANIFICACION 
        ,COD_DIVISA
        ,DES_DIVISA
        ,NUM_IMPORTE
        ,BOL_VIRTUAL
        ,GMT_CREACION
        ,DES_USUARIO_CREACION
        ,GMT_MODIFICACION
        ,DES_USUARIO_MODIFICACION )
        VALUES
        ( par$OID_MOVIMIENTO
        ,par$OID_DOCUMENTO
        ,par$COD_PAIS          
        ,par$COD_DELEGACION_OPE  
        ,par$COD_DELEGACION_MDM 
        ,par$DES_DELEGACION      
        ,par$COD_CLIENTE_OPE     
        ,par$COD_CLIENTE_MDM     
        ,par$DES_CLIENTE
        ,par$DES_CLIENTE_OPE
        ,par$COD_SUBCLIENTE_OPE  
        ,par$COD_SUBCLIENTE_MDM  
        ,par$DES_SUBCLIENTE
        ,par$DES_SUBCLIENTE_OPE
        ,par$COD_PTO_SERVICIO_OPE  
        ,par$COD_PTO_SERVICIO_MDM  
        ,par$DES_PTO_SERVICIO
        ,par$DES_PTO_SERVICIO_OPE
        ,par$COD_CANAL             
        ,par$DES_CANAL             
        ,par$COD_SUBCANAL          
        ,par$DES_SUBCANAL          
        ,par$COD_DEVICEID          
        ,par$DES_MAQUINA       
        ,par$FEC_MOVIMIENTO        
        ,par$HOR_MOVIMIENTO        
        ,par$FYH_CONTABLE          
        ,par$COD_TIPO_MOVIMIENTO   
        ,par$DES_TIPO_MOVIMIENTO   
        ,par$COD_GRUPO_MOVIMIENTO  
        ,par$COD_MOVIMIENTO
        ,par$COD_COMPROBANTE       
        ,par$COD_ACTUAL_ID         
        ,par$COD_COLLECTION_ID     
        ,par$BOL_NOTIFICADO        
        ,par$FYH_NOTIFICACION      
        ,par$BOL_ACREDITADO        
        ,par$FYH_ACREDITACION      
        ,par$COD_PLANIFICACION     
        ,par$DES_PLANIFICACION     
        ,par$COD_PLANIF_BANCO_OPE   
        ,par$COD_PLANIF_BANCO_MDM   
        ,par$DES_PLANIF_BANCO     
        ,par$FYH_PLANIF_PERIODO_INI 
        ,par$FYH_PLANIF_PERIODO_FIN  
        ,par$COD_TIPO_PLANIFICACION     
        ,par$DES_TIPO_PLANIFICACION 
        ,par$COD_DIVISA
        ,par$DES_DIVISA
        ,par$NUM_IMPORTE
        ,par$BOL_VIRTUAL
        ,par$GMT_CREACION
        ,par$cod_usuario
        ,par$GMT_CREACION
        ,par$cod_usuario   );
    ELSE
        par$oid_movimiento := var$oid_movimiento;
        IF  par$COD_PAIS IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_PAIS = ']' || par$COD_PAIS ||q'[', ]';
        END IF;
        IF  par$COD_DELEGACION_OPE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_DELEGACION_OPE = ']' || Replace(par$COD_DELEGACION_OPE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_DELEGACION_MDM IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_DELEGACION_MDM = ']' || Replace(par$COD_DELEGACION_MDM, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_DELEGACION IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_DELEGACION = ']' || Replace(par$DES_DELEGACION, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_CLIENTE_OPE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_CLIENTE_OPE = ']' || Replace(par$COD_CLIENTE_OPE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_CLIENTE_MDM IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_CLIENTE_MDM = ']' || Replace(par$COD_CLIENTE_MDM, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_CLIENTE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_CLIENTE = ']' || Replace(par$DES_CLIENTE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_CLIENTE_OPE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_CLIENTE_OPE = ']' || Replace(par$DES_CLIENTE_OPE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_SUBCLIENTE_OPE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_SUBCLIENTE_OPE = ']' || Replace(par$COD_SUBCLIENTE_OPE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_SUBCLIENTE_MDM IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_SUBCLIENTE_MDM = ']' || Replace(par$COD_SUBCLIENTE_MDM, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_SUBCLIENTE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_SUBCLIENTE = ']' || Replace(par$DES_SUBCLIENTE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_SUBCLIENTE_OPE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_SUBCLIENTE_OPE = ']' || Replace(par$DES_SUBCLIENTE_OPE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_PTO_SERVICIO_OPE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_PTO_SERVICIO_OPE = ']' || Replace(par$COD_PTO_SERVICIO_OPE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_PTO_SERVICIO_MDM IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_PTO_SERVICIO_MDM = ']' || Replace(par$COD_PTO_SERVICIO_MDM, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_PTO_SERVICIO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_PTO_SERVICIO = ']' || Replace(par$DES_PTO_SERVICIO, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_PTO_SERVICIO_OPE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_PTO_SERVICIO_OPE = ']' || Replace(par$DES_PTO_SERVICIO_OPE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_CANAL IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_CANAL = ']' || Replace(par$COD_CANAL, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_CANAL IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_CANAL = ']' || Replace(par$DES_CANAL, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_SUBCANAL IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_SUBCANAL = ']' || Replace(par$COD_SUBCANAL, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_SUBCANAL IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_SUBCANAL = ']' || Replace(par$DES_SUBCANAL, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_DEVICEID IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_DEVICEID = ']' || Replace(par$COD_DEVICEID, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_MAQUINA IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_MAQUINA = ']' || Replace(par$DES_MAQUINA, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$FEC_MOVIMIENTO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ FEC_MOVIMIENTO = ']' || par$FEC_MOVIMIENTO ||q'[', ]';
        END IF;
        IF  par$HOR_MOVIMIENTO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ HOR_MOVIMIENTO = ']' || par$HOR_MOVIMIENTO ||q'[', ]';
        END IF;
        IF  par$FYH_CONTABLE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ FYH_CONTABLE = ']' || par$fyh_contable ||q'[', ]';
        END IF;
        IF  par$COD_TIPO_MOVIMIENTO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_TIPO_MOVIMIENTO = ']' || Replace(par$COD_TIPO_MOVIMIENTO, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_TIPO_MOVIMIENTO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_TIPO_MOVIMIENTO = ']' || Replace(par$DES_TIPO_MOVIMIENTO, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_GRUPO_MOVIMIENTO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_GRUPO_MOVIMIENTO = ']' || Replace(par$COD_GRUPO_MOVIMIENTO, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_MOVIMIENTO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_MOVIMIENTO = ']' || Replace(par$COD_MOVIMIENTO, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_COMPROBANTE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_COMPROBANTE = ']' || Replace(par$COD_COMPROBANTE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_ACTUAL_ID IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_ACTUAL_ID = ']' || Replace(par$COD_ACTUAL_ID, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$COD_COLLECTION_ID IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_COLLECTION_ID = ']' || Replace(par$COD_COLLECTION_ID, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$BOL_NOTIFICADO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ BOL_NOTIFICADO = ']' || par$BOL_NOTIFICADO ||q'[', ]';
        END IF;
        IF  par$FYH_NOTIFICACION IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ FYH_NOTIFICACION = ']' || par$fyh_notificacion ||q'[', ]';
        END IF;
        IF  par$BOL_ACREDITADO IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ BOL_ACREDITADO = ']' || par$BOL_ACREDITADO ||q'[', ]';
        END IF;
        IF  par$FYH_ACREDITACION IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ FYH_ACREDITACION = ']' || par$fyh_acreditacion ||q'[', ]';
        END IF;
        IF par$COD_PLANIFICACION IS NOT NULL THEN
          IF par$COD_PLANIFICACION = const$vacio THEN
            var$qry_update := var$qry_update || q'[ COD_PLANIFICACION = '', ]';
          ELSE
            var$qry_update := var$qry_update || q'[ COD_PLANIFICACION = ']' || Replace(par$COD_PLANIFICACION, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
          END IF;
        END IF;
        IF  par$DES_PLANIFICACION IS NOT NULL  THEN
          IF par$DES_PLANIFICACION = const$vacio THEN
            var$qry_update := var$qry_update || q'[ DES_PLANIFICACION = '', ]';
          ELSE
            var$qry_update := var$qry_update || q'[ DES_PLANIFICACION = ']' || Replace(par$DES_PLANIFICACION, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
          END IF;
        END IF;
        IF  par$COD_PLANIF_BANCO_OPE IS NOT NULL  THEN
          IF par$COD_PLANIF_BANCO_OPE = const$vacio THEN
            var$qry_update := var$qry_update || q'[ COD_PLANIFICACION_BANCO_OPE = '', ]';
          ELSE
            var$qry_update := var$qry_update || q'[ COD_PLANIFICACION_BANCO_OPE = ']' || Replace(par$COD_PLANIF_BANCO_OPE, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
          END IF;
        END IF;
        IF  par$COD_PLANIF_BANCO_MDM IS NOT NULL  THEN
          IF par$COD_PLANIF_BANCO_MDM = const$vacio THEN
            var$qry_update := var$qry_update || q'[ COD_PLANIFICACION_BANCO_MDM = '', ]';
          ELSE
            var$qry_update := var$qry_update || q'[ COD_PLANIFICACION_BANCO_MDM = ']' || Replace(par$COD_PLANIF_BANCO_MDM, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
          END IF;
        END IF;
        IF  par$DES_PLANIF_BANCO IS NOT NULL  THEN
          IF par$DES_PLANIF_BANCO = const$vacio THEN
            var$qry_update := var$qry_update || q'[ DES_PLANIFICACION_BANCO = '', ]';
          ELSE
            var$qry_update := var$qry_update || q'[ DES_PLANIFICACION_BANCO = ']' || Replace(par$DES_PLANIF_BANCO, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
          END IF;
        END IF;
        IF  par$FYH_PLANIF_PERIODO_INI IS NOT NULL  THEN
          IF par$FYH_PLANIF_PERIODO_INI = TO_DATE('0001-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS') THEN
            var$qry_update := var$qry_update || q'[ FYH_PLANIFICACION_PERIODO_INI = '', ]';
          ELSE
            var$qry_update := var$qry_update || ' FYH_PLANIFICACION_PERIODO_INI = TO_TIMESTAMP_TZ(''' || TO_CHAR(par$FYH_PLANIF_PERIODO_INI, 'YYYY-MM-DD HH24:MI:SS TZH:TZM') || ''', ''YYYY-MM-DD HH24:MI:SS TZH:TZM''), ';
          END IF;
        END IF;
        IF  par$FYH_PLANIF_PERIODO_FIN IS NOT NULL  THEN
          IF par$FYH_PLANIF_PERIODO_FIN = TO_DATE('0001-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS') THEN
              var$qry_update := var$qry_update || q'[ FYH_PLANIFICACION_PERIODO_FIN = '', ]';
          ELSE
            var$qry_update := var$qry_update || ' FYH_PLANIFICACION_PERIODO_FIN = TO_TIMESTAMP_TZ(''' || TO_CHAR(par$FYH_PLANIF_PERIODO_FIN, 'YYYY-MM-DD HH24:MI:SS TZH:TZM') || ''', ''YYYY-MM-DD HH24:MI:SS TZH:TZM''), ';
          END IF;
        END IF;
        IF  par$COD_TIPO_PLANIFICACION IS NOT NULL  THEN
          IF par$COD_TIPO_PLANIFICACION = const$vacio THEN
            var$qry_update := var$qry_update || q'[ COD_TIPO_PLANIFICACION = '', ]';
          ELSE
            var$qry_update := var$qry_update || q'[ COD_TIPO_PLANIFICACION = ']' || Replace(par$COD_TIPO_PLANIFICACION, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
          END IF;
        END IF;
        IF  par$DES_TIPO_PLANIFICACION IS NOT NULL  THEN
          IF par$DES_TIPO_PLANIFICACION = const$vacio THEN
            var$qry_update := var$qry_update || q'[ DES_TIPO_PLANIFICACION = '', ]';
          ELSE
            var$qry_update := var$qry_update || q'[ DES_TIPO_PLANIFICACION = ']' || Replace(par$DES_TIPO_PLANIFICACION, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
          END IF;
        END IF;
        IF  par$COD_DIVISA IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ COD_DIVISA = ']' || Replace(par$COD_DIVISA, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$DES_DIVISA IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ DES_DIVISA = ']' || Replace(par$DES_DIVISA, CHR(39), CHR(39)||CHR(39)) ||q'[', ]';
        END IF;
        IF  par$NUM_IMPORTE IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ NUM_IMPORTE = ']' || par$NUM_IMPORTE ||q'[', ]';
        END IF;
        IF  par$BOL_VIRTUAL IS NOT NULL  THEN
            var$qry_update := var$qry_update || q'[ BOL_VIRTUAL = ']' || par$BOL_VIRTUAL ||q'[', ]';
        END IF;

        IF  var$qry_update IS NOT NULL  THEN
            DBMS_OUTPUT.PUT_LINE('UPDATE SAPR_TMOVIMIENTO SET ' ||  var$qry_update || ' GMT_MODIFICACION = ' || var$gmt_zero || 'DES_USUARIO_MODIFICACION = ' || par$cod_usuario || ' WHERE OID_MOVIMIENTO = ' ||var$oid_movimiento);
            dbms_output.put_line('Cod_Usuario = ' || par$cod_usuario);
            EXECUTE IMMEDIATE q'[ UPDATE SAPR_TMOVIMIENTO SET ]' || var$qry_update ||q'[
            GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2, NEL_CANTIDAD = :3
            WHERE OID_MOVIMIENTO = :4]'
            USING  var$gmt_zero, par$cod_usuario, var$sumCantidad, var$oid_movimiento; 
        END IF;
    END IF;
END supd_movimiento;

/* SP que se encarga de grabar en las tablas desmormalizadas de MOVIMIENTOS */
procedure sgrabar_mov_consolidado(
    par$OID_MOVIMIENTO    in gepr_pcomon_###VERSION###.tipo$oid_,
    par$OID_DOCUMENTO     in gepr_pcomon_###VERSION###.tipo$oid_,
    par$COD_PAIS          in gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_DELEGACION_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_DELEGACION_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_DELEGACION      IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_CLIENTE_OPE     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_CLIENTE_MDM     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_CLIENTE         IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$DES_CLIENTE_OPE     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_SUBCLIENTE_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_SUBCLIENTE_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_SUBCLIENTE      IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$DES_SUBCLIENTE_OPE    IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_PTO_SERVICIO_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_PTO_SERVICIO_MDM  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_PTO_SERVICIO      IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$DES_PTO_SERVICIO_OPE  IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_CANAL             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_CANAL             IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_SUBCANAL          IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_SUBCANAL          IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_DEVICEID          IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_MAQUINA           IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$FEC_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$fyh_,
    par$HOR_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$FYH_CONTABLE          IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$COD_TIPO_MOVIMIENTO   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_TIPO_MOVIMIENTO   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_GRUPO_MOVIMIENTO  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_MOVIMIENTO        IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_COMPROBANTE       IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_ACTUAL_ID         IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_COLLECTION_ID     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$BOL_NOTIFICADO        IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$FYH_NOTIFICACION      IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$BOL_ACREDITADO        IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$FYH_ACREDITACION      IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$COD_PLANIFICACION     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_PLANIFICACION     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_PLANIF_BANCO_OPE  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$COD_PLANIF_BANCO_MDM    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_PLANIF_BANCO      IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$FYH_PLANIF_PERIODO_INI  IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$FYH_PLANIF_PERIODO_FIN  IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$COD_TIPO_PLANIFICACION  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_TIPO_PLANIFICACION  IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$COD_DIVISA            IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$DES_DIVISA            IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$num_importe           IN gepr_pcomon_###VERSION###.tipo$imp_,

    /* MOVIMIENTO DETALLE */
    par$aoid_movimiento_detalle IN gepr_pcomon_###VERSION###.tipo$oids_,
    par$aimp_movi_detalle       IN gepr_pcomon_###VERSION###.tipo$imps_,
    par$anel_cant_movi_detalle  IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$abol_billete_movi_detalle  IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$acod_denom_movi_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$ades_denom_movi_detalle IN gepr_pcomon_###VERSION###.tipo$descs_,

    /* MOVIMIENTO CAMPOS EXTRAS */
    par$aoid_mov_campos_extras  IN gepr_pcomon_###VERSION###.tipo$oids_,
    par$acod_campo_extra        IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$ades_campo_extra        IN gepr_pcomon_###VERSION###.tipo$descs_,

    par$BOL_VIRTUAL             IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$GMT_CREACION            IN gepr_pcomon_###VERSION###.tipo$gmt_,
    par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_
) IS

  /*Algunas variables*/
  var$oid_movimiento gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_mov_detalle gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_mov_campos_extras gepr_pcomon_###VERSION###.tipo$oid_;
  var$num_importe gepr_pcomon_###VERSION###.tipo$imp_;
  

BEGIN

  /*Llamar al sup que graba en la tabla principal*/
  var$oid_movimiento := par$OID_MOVIMIENTO;

  supd_movimiento(par$OID_MOVIMIENTO    => var$oid_movimiento,
    par$OID_DOCUMENTO                   => par$oid_documento,
    par$COD_PAIS                        => par$cod_pais,
    par$COD_DELEGACION_OPE              => par$COD_DELEGACION_OPE,
    par$COD_DELEGACION_MDM              => par$COD_DELEGACION_MDM,
    par$DES_DELEGACION                  => par$DES_DELEGACION,
    par$COD_CLIENTE_OPE                 => par$COD_CLIENTE_OPE,
    par$COD_CLIENTE_MDM                 => par$COD_CLIENTE_MDM,
    par$DES_CLIENTE                     => par$DES_CLIENTE,
    par$DES_CLIENTE_OPE                 => par$DES_CLIENTE_OPE,
    par$COD_SUBCLIENTE_OPE              => par$COD_SUBCLIENTE_OPE,
    par$COD_SUBCLIENTE_MDM              => par$COD_SUBCLIENTE_MDM,
    par$DES_SUBCLIENTE                  => par$DES_SUBCLIENTE,
    par$DES_SUBCLIENTE_OPE              => par$DES_SUBCLIENTE_OPE,
    par$COD_PTO_SERVICIO_OPE            => par$COD_PTO_SERVICIO_OPE,
    par$COD_PTO_SERVICIO_MDM            => par$COD_PTO_SERVICIO_MDM,
    par$DES_PTO_SERVICIO                => par$DES_PTO_SERVICIO,
    par$DES_PTO_SERVICIO_OPE            => par$DES_PTO_SERVICIO_OPE,
    par$COD_CANAL                       => par$COD_CANAL,
    par$DES_CANAL                       => par$DES_CANAL,
    par$COD_SUBCANAL                    => par$COD_SUBCANAL,
    par$DES_SUBCANAL                    => par$DES_SUBCANAL,
    par$COD_DEVICEID                    => par$COD_DEVICEID,
    par$DES_MAQUINA                     => par$DES_MAQUINA,
    par$FEC_MOVIMIENTO                  => par$FEC_MOVIMIENTO,
    par$HOR_MOVIMIENTO                  => par$HOR_MOVIMIENTO,
    par$FYH_CONTABLE                    => par$FYH_CONTABLE,
    par$COD_TIPO_MOVIMIENTO             => par$COD_TIPO_MOVIMIENTO,
    par$DES_TIPO_MOVIMIENTO             => par$DES_TIPO_MOVIMIENTO,
    par$COD_GRUPO_MOVIMIENTO            => par$COD_GRUPO_MOVIMIENTO,
    par$COD_MOVIMIENTO                  => par$COD_MOVIMIENTO,
    par$COD_COMPROBANTE                 => par$COD_COMPROBANTE,
    par$COD_ACTUAL_ID                   => par$COD_ACTUAL_ID,
    par$COD_COLLECTION_ID               => par$COD_COLLECTION_ID,
    par$BOL_NOTIFICADO                  => par$BOL_NOTIFICADO,
    par$FYH_NOTIFICACION                => par$FYH_NOTIFICACION,
    par$BOL_ACREDITADO                  => par$BOL_ACREDITADO,
    par$FYH_ACREDITACION                => par$FYH_ACREDITACION,
    par$COD_PLANIFICACION               => par$COD_PLANIFICACION,
    par$DES_PLANIFICACION               => par$DES_PLANIFICACION,
    par$COD_PLANIF_BANCO_OPE            => par$COD_PLANIF_BANCO_OPE,
    par$COD_PLANIF_BANCO_MDM            => par$COD_PLANIF_BANCO_MDM,
    par$DES_PLANIF_BANCO                => par$DES_PLANIF_BANCO,
    par$FYH_PLANIF_PERIODO_INI          => par$FYH_PLANIF_PERIODO_INI,
    par$FYH_PLANIF_PERIODO_FIN          => par$FYH_PLANIF_PERIODO_FIN,
    par$COD_TIPO_PLANIFICACION          => par$COD_TIPO_PLANIFICACION,
    par$DES_TIPO_PLANIFICACION          => par$DES_TIPO_PLANIFICACION,
    par$COD_DIVISA                      => par$COD_DIVISA,
    par$DES_DIVISA                      => par$DES_DIVISA,
    par$NUM_IMPORTE                     => par$NUM_IMPORTE,
    par$BOL_VIRTUAL                     => par$BOL_VIRTUAL,
    par$GMT_CREACION                    => par$GMT_CREACION,
    par$COD_USUARIO                     => par$cod_usuario
  );

  /*Grabar detalles*/
  IF par$acod_denom_movi_detalle IS NOT NULL AND par$acod_denom_movi_detalle.count > 0 THEN
    FOR idx IN par$acod_denom_movi_detalle.first .. par$acod_denom_movi_detalle.last LOOP

      IF par$acod_denom_movi_detalle(idx) is not null AND par$aimp_movi_detalle (idx) is not null THEN
        var$oid_mov_detalle := par$aoid_movimiento_detalle(idx);

        supd_mov_detalle(
          par$oid_movimiento            => var$oid_movimiento,
          par$oid_movimiento_detalle    => var$oid_mov_detalle,
          par$cod_pais                  => par$cod_pais,
          par$fec_movimiento            => par$fec_movimiento,
          par$cod_denominacion          => par$acod_denom_movi_detalle(idx),
          par$des_denominacion          => par$ades_denom_movi_detalle(idx),
          par$bol_billete               => par$abol_billete_movi_detalle(idx),
          par$nel_cantidad              => par$anel_cant_movi_detalle (idx),
          par$num_importe               => par$aimp_movi_detalle (idx)
        );
        
      END IF;

    END LOOP;

  END IF;

  /*Grabar campos extras*/
  IF par$acod_campo_extra IS NOT NULL AND par$acod_campo_extra.count > 0 THEN

    FOR idx IN par$acod_campo_extra.first .. par$acod_campo_extra.last LOOP
      
      IF par$acod_campo_extra (idx) IS NOT NULL then
        var$oid_mov_campos_extras := par$aoid_mov_campos_extras(idx);
        supd_mov_campo_extra(
          par$oid_movimiento            => var$oid_movimiento,
          par$oid_mov_campo_extra       => var$oid_mov_campos_extras,
          par$cod_pais                  => par$cod_pais,
          par$fec_movimiento            => par$fec_movimiento,
          par$cod_campo_extra           => par$acod_campo_extra (idx),
          par$des_campo_extra           => par$ades_campo_extra (idx)
        );
      END IF;

    END LOOP;
  
  END IF;

END sgrabar_mov_consolidado; 

   /* SP que se encarga de grabar en las tablas desmormalizadas de MOVIMIENTOS por OID_DOCUMENTO */
   procedure sgrabar_movimiento_por_doc(
      par$oid_llamada      IN gepr_pcomon_###VERSION###.tipo$oid_,
      par$oid_documento    IN gepr_pcomon_###VERSION###.tipo$oid_,
      par$cod_usuario      IN gepr_pcomon_###VERSION###.tipo$cod_
    )
  IS
     const$nombre_func CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sgrabar_movimiento_por_doc';  

    /* MOVIMIENTO */
    var$oid_planificacion       gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_planificacion       gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_planificacion       gepr_pcomon_###VERSION###.tipo$desc_;    
    var$cod_planif_banco_ope    gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planif_banco_mdm    gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_planif_banco        gepr_pcomon_###VERSION###.tipo$desc_;    
    var$fyh_planif_periodo_ini  gepr_pcomon_###VERSION###.tipo$gmt_;    
    var$fyh_planif_periodo_fin  gepr_pcomon_###VERSION###.tipo$gmt_; 
    var$cod_tipo_planificacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_tipo_planificacion  gepr_pcomon_###VERSION###.tipo$desc_;    
    var$aux_cod_validaciones    gepr_pcomon_###VERSION###.tipo$cods_;
    var$aux_des_validaciones    gepr_pcomon_###VERSION###.tipo$descs_;

    /* MOVIMIENTO DETALLE */
    var$aoid_movimiento_detalle       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aimp_movi_detalle             gepr_pcomon_###VERSION###.tipo$imps_;
    var$anel_cant_movi_detalle        gepr_pcomon_###VERSION###.tipo$nels_;
    var$abol_billete_movi_detalle     gepr_pcomon_###VERSION###.tipo$nels_;
    var$acod_denom_movi_detalle       gepr_pcomon_###VERSION###.tipo$cods_;
    var$ades_denom_movi_detalle       gepr_pcomon_###VERSION###.tipo$descs_;

    /* MOVIMIENTO CAMPOS EXTRAS */
    var$aoid_mov_campos_extras        gepr_pcomon_###VERSION###.tipo$oids_;
    var$acod_campo_extra              gepr_pcomon_###VERSION###.tipo$cods_;
    var$ades_campo_extra              gepr_pcomon_###VERSION###.tipo$descs_;

    var$existe                        gepr_pcomon_###VERSION###.tipo$nel_ := 0;

  BEGIN
    /*Limpiamos los arrays asociativos*/
    var$aoid_movimiento_detalle.delete;
    var$aimp_movi_detalle.delete;
    var$anel_cant_movi_detalle.delete;
    var$abol_billete_movi_detalle.delete;
    var$acod_denom_movi_detalle.delete;
    var$ades_denom_movi_detalle.delete;
    var$aoid_mov_campos_extras.delete;
    var$acod_campo_extra.delete;
    var$ades_campo_extra.delete;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                  par$des_origen      => const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     =>  'Comienzo a recorrer el cursor de movimiento detalle',
                  par$cod_identificador   => '');

    /*Comienzo a recorrer el cursor de movimiento detalle*/
    FOR REC$MOVI_DET IN (
      select 
        NULL OID_MOVIMIENTO_DETALLE,
        efecxdoc.oid_documento OID_DOCUMENTO,
        denominacion_aj.cod_ajeno COD_DENOMINACION,
        denominacion.DES_DENOMINACION DES_DENOMINACION,
        denominacion.BOL_BILLETE BOL_BILLETE,
        efecxdoc.nel_cantidad NEL_CANTIDAD,
        efecxdoc.num_importe
      from sapr_tefectivoxdocumento efecxdoc
      inner join gepr_tdenominacion denominacion on  denominacion.oid_denominacion = efecxdoc.oid_denominacion
      inner join GEPR_TCODIGO_AJENO denominacion_aj on denominacion_aj.oid_tabla_genesis = denominacion.OID_DENOMINACION and denominacion_aj.COD_IDENTIFICADOR = 'MAE' and denominacion_aj.cod_tipo_tabla_genesis = 'GEPR_TDENOMINACION'
      WHERE
        efecxdoc.oid_documento = par$oid_documento AND
        efecxdoc.COD_NIVEL_DETALLE = 'D') LOOP

      var$aoid_movimiento_detalle(var$aoid_movimiento_detalle.count) := REC$MOVI_DET.OID_MOVIMIENTO_DETALLE;
      var$aimp_movi_detalle(var$aimp_movi_detalle.count) := REC$MOVI_DET.NUM_IMPORTE;
      var$anel_cant_movi_detalle(var$anel_cant_movi_detalle.count) := REC$MOVI_DET.NEL_CANTIDAD;
      var$abol_billete_movi_detalle(var$abol_billete_movi_detalle.count) := REC$MOVI_DET.bol_billete;
      var$acod_denom_movi_detalle(var$acod_denom_movi_detalle.count) := REC$MOVI_DET.COD_DENOMINACION;
      var$ades_denom_movi_detalle(var$ades_denom_movi_detalle.count) := REC$MOVI_DET.DES_DENOMINACION;

    END LOOP;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     =>  'Comienzo a recorrer el cursor de movimiento campo extra',
                      par$cod_identificador   => '');

    /*Comienzo a recorrer el cursor de movimiento campo extra*/
    FOR REC$MOVI_TERMINO IN (
      select
          NULL OID_MOVIMIENTO_CAMPO_EXTRA,
          terxdoc.oid_documento OID_DOCUMENTO,
          termino.cod_termino COD_CAMPO_EXTRA,
          terxdoc.DES_VALOR DES_VALOR
      from    sapr_tvalor_terminoxdocumento terxdoc 
      inner join gepr_ttermino termino on termino.oid_termino = terxdoc.oid_termino
      WHERE terxdoc.oid_documento = par$oid_documento) LOOP
      var$aoid_mov_campos_extras(var$aoid_mov_campos_extras.count) := REC$MOVI_TERMINO.OID_MOVIMIENTO_CAMPO_EXTRA;
      var$acod_campo_extra(var$acod_campo_extra.count) := REC$MOVI_TERMINO.COD_CAMPO_EXTRA;
      var$ades_campo_extra(var$ades_campo_extra.count) := REC$MOVI_TERMINO.DES_VALOR;
    END LOOP;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     =>  'Recorremos el cursor principal de Movimiento',
                      par$cod_identificador   => '');

    /* Recorremos el cursor principal */
    FOR REC$MOVIMIENTO IN (      
      select
        NULL OID_MOVIMIENTO,
        DOCU.OID_DOCUMENTO OID_DOCUMENTO,
        DELEG.COD_PAIS COD_PAIS,
        DELEGAJ.COD_AJENO COD_DELEGACION_OPE,
        VISTA_CUENTA.COD_DELEGACION COD_DELEGACION_MDM,
        VISTA_CUENTA.DES_DELEGACION DES_DELEGACION,
        CLIENTEAJ.COD_AJENO COD_CLIENTE_OPE,
        VISTA_CUENTA.COD_CLIENTE COD_CLIENTE_MDM,
        VISTA_CUENTA.DES_CLIENTE DES_CLIENTE,
        CLIENTEAJ.DES_AJENO DES_CLIENTE_OPE,
        SUBCLIAJ.COD_AJENO COD_SUBCLIENTE_OPE,
        VISTA_CUENTA.COD_SUBCLIENTE COD_SUBCLIENTE_MDM,
        VISTA_CUENTA.DES_SUBCLIENTE DES_SUBCLIENTE,
        SUBCLIAJ.DES_AJENO DES_SUBCLIENTE_OPE,
        PTOAJ.COD_AJENO COD_PTO_SERVICIO_OPE,
        VISTA_CUENTA.COD_PTO_SERVICIO COD_PTO_SERVICIO_MDM,
        VISTA_CUENTA.DES_PTO_SERVICIO DES_PTO_SERVICIO,
        PTOAJ.DES_AJENO DES_PTO_SERVICIO_OPE,
        CANAJ.COD_AJENO COD_CANAL,
        VISTA_CUENTA.DES_CANAL DES_CANAL,
        SUBCAJ.COD_AJENO COD_SUBCANAL,
        VISTA_CUENTA.DES_SUBCANAL DES_SUBCANAL,
        VISTA_CUENTA.OID_MAQUINA,
        NVL(VISTA_CUENTA.COD_IDENTIFICACION_MAQUINA, VISTA_CUENTA.COD_SECTOR) COD_DEVICEID,
        VISTA_CUENTA.DES_SECTOR DES_MAQUINA,
        DOCU.FYH_GESTION,
        TRUNC(TO_TIMESTAMP_TZ(TO_CHAR(DOCU.FYH_GESTION + ((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_GESTION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_GESTION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
        TRIM(TO_CHAR(ROUND((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_GESTION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_GESTION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
        TRIM(TO_CHAR(MOD((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_GESTION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_GESTION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM')) FEC_MOVIMIENTO,
        TO_TIMESTAMP_TZ(TO_CHAR(DOCU.FYH_GESTION + ((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_GESTION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_GESTION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
        TRIM(TO_CHAR(ROUND((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_GESTION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_GESTION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
        TRIM(TO_CHAR(MOD((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_GESTION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_GESTION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') HOR_MOVIMIENTO,
        TO_TIMESTAMP_TZ(TO_CHAR(NVL(DOCU.FEC_CONTABLE, DOCU.FYH_GESTION)  + ((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (NVL(DOCU.FEC_CONTABLE, DOCU.FYH_GESTION) ) >= DELEG.FYH_VERANO_INICIO AND (NVL(DOCU.FEC_CONTABLE, DOCU.FYH_GESTION) ) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
        TRIM(TO_CHAR(ROUND((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (NVL(DOCU.FEC_CONTABLE, DOCU.FYH_GESTION) ) >= DELEG.FYH_VERANO_INICIO AND (NVL(DOCU.FEC_CONTABLE, DOCU.FYH_GESTION) ) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
        TRIM(TO_CHAR(MOD((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (NVL(DOCU.FEC_CONTABLE, DOCU.FYH_GESTION) ) >= DELEG.FYH_VERANO_INICIO AND (NVL(DOCU.FEC_CONTABLE, DOCU.FYH_GESTION) ) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') FYH_CONTABLE,
        FORMU.COD_FORMULARIO COD_TIPO_MOVIMIENTO,
        FORMU.DES_FORMULARIO DES_TIPO_MOVIMIENTO,
        FORMU.DES_COD_EXTERNO COD_GRUPO_MOVIMIENTO,
        DOCU.COD_EXTERNO COD_MOVIMIENTO,
        DOCU.COD_COMPROBANTE COD_COMPROBANTE,
        DOCU.COD_ACTUAL_ID COD_ACTUAL_ID,
        DOCU.COD_COLLECTION_ID COD_COLLECTION_ID,
        docu.bol_notificado BOL_NOTIFICADO,
        CASE WHEN DOCU.FYH_NOTIFICACION IS NOT NULL THEN
        TO_TIMESTAMP_TZ(TO_CHAR(DOCU.FYH_NOTIFICACION + ((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_NOTIFICACION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_NOTIFICACION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
        TRIM(TO_CHAR(ROUND((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_NOTIFICACION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_NOTIFICACION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
        TRIM(TO_CHAR(MOD((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_NOTIFICACION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_NOTIFICACION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') 
        ELSE DOCU.FYH_NOTIFICACION END FYH_NOTIFICACION,
        DOCU.BOL_ACREDITADO BOL_ACREDITADO,
        CASE WHEN DOCU.FYH_ACREDITACION IS NOT NULL THEN
        TO_TIMESTAMP_TZ(TO_CHAR(DOCU.FYH_ACREDITACION + ((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_ACREDITACION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_ACREDITACION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
        TRIM(TO_CHAR(ROUND((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_ACREDITACION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_ACREDITACION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
        TRIM(TO_CHAR(MOD((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (DOCU.FYH_ACREDITACION) >= DELEG.FYH_VERANO_INICIO AND (DOCU.FYH_ACREDITACION) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') 
        ELSE DOCU.FYH_ACREDITACION END FYH_ACREDITACION,
        PLANIFI.COD_PLANIFICACION COD_PLANIFICACION,
        PLANIFI.DES_PLANIFICACION DES_PLANIFICACION,
        BANCOAJ.COD_AJENO COD_PLANIF_BANCO_OPE,
        BANCO.COD_CLIENTE COD_PLANIF_BANCO_MDM,
        BANCO.DES_CLIENTE DES_PLANIF_BANCO,
        CASE WHEN PERI.FYH_INICIO IS NOT NULL THEN
        TO_TIMESTAMP_TZ(TO_CHAR(PERI.FYH_INICIO + ((NVL(DELEG_PLANI.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_INICIO) >= DELEG_PLANI.FYH_VERANO_INICIO AND (PERI.FYH_INICIO) <= DELEG_PLANI.FYH_VERANO_FIN THEN NVL(DELEG_PLANI.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
        TRIM(TO_CHAR(ROUND((NVL(DELEG_PLANI.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_INICIO) >= DELEG_PLANI.FYH_VERANO_INICIO AND (PERI.FYH_INICIO) <= DELEG_PLANI.FYH_VERANO_FIN THEN NVL(DELEG_PLANI.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
        TRIM(TO_CHAR(MOD((NVL(DELEG_PLANI.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_INICIO) >= DELEG_PLANI.FYH_VERANO_INICIO AND (PERI.FYH_INICIO) <= DELEG_PLANI.FYH_VERANO_FIN THEN NVL(DELEG_PLANI.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') 
        ELSE PERI.FYH_INICIO END FYH_PLANIF_PERIODO_INI,
        CASE WHEN PERI.FYH_FIN IS NOT NULL THEN
        TO_TIMESTAMP_TZ(TO_CHAR(PERI.FYH_FIN + ((NVL(DELEG_PLANI.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_FIN) >= DELEG_PLANI.FYH_VERANO_INICIO AND (PERI.FYH_FIN) <= DELEG_PLANI.FYH_VERANO_FIN THEN NVL(DELEG_PLANI.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
        TRIM(TO_CHAR(ROUND((NVL(DELEG_PLANI.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_FIN) >= DELEG_PLANI.FYH_VERANO_INICIO AND (PERI.FYH_FIN) <= DELEG_PLANI.FYH_VERANO_FIN THEN NVL(DELEG_PLANI.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
        TRIM(TO_CHAR(MOD((NVL(DELEG_PLANI.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_FIN) >= DELEG_PLANI.FYH_VERANO_INICIO AND (PERI.FYH_FIN) <= DELEG_PLANI.FYH_VERANO_FIN THEN NVL(DELEG_PLANI.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') 
        ELSE PERI.FYH_FIN END FYH_PLANIF_PERIODO_FIN,
        (   
            select DISTINCT DIVIAJ.COD_AJENO 
            FROM    SAPR_TEFECTIVOXDOCUMENTO EFECXDOCU 
            LEFT OUTER JOIN GEPR_TCODIGO_AJENO DIVIAJ ON DIVIAJ.OID_TABLA_GENESIS = EFECXDOCU.OID_DIVISA  AND DIVIAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' AND DIVIAJ.COD_IDENTIFICADOR='MAE'
            WHERE
                EFECXDOCU.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
                and rownum = 1
        ) COD_DIVISA,
        (
            select DISTINCT DIVI.DES_DIVISA 
            FROM    SAPR_TEFECTIVOXDOCUMENTO EFECXDOCU 
            LEFT OUTER JOIN GEPR_TCODIGO_AJENO DIVIAJ ON DIVIAJ.OID_TABLA_GENESIS = EFECXDOCU.OID_DIVISA  
            AND DIVIAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' AND DIVIAJ.COD_IDENTIFICADOR='MAE'
            LEFT JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = DIVIAJ.OID_TABLA_GENESIS
            WHERE
                EFECXDOCU.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
                and rownum = 1
        ) DES_DIVISA,
        (
            SELECT SUM(NVL(EFECXDOCU.NUM_IMPORTE, 0))
            FROM SAPR_TEFECTIVOXDOCUMENTO EFECXDOCU
            WHERE EFECXDOCU.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
        ) NUM_IMPORTE,
        (case ACCCONTABLE.COD_ACCION_CONTABLE 
        when 'AC_000000000001' then 1 
        else 0 end) BOL_VIRTUAL, --AC_000000000001 = No mueve valores = VIRTUAL
        TO_TIMESTAMP_TZ(TO_CHAR(SYS_EXTRACT_UTC(DOCU.GMT_CREACION) + ((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (SYS_EXTRACT_UTC(DOCU.GMT_CREACION)) >= DELEG.FYH_VERANO_INICIO AND (SYS_EXTRACT_UTC(DOCU.GMT_CREACION)) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
        TRIM(TO_CHAR(ROUND((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (SYS_EXTRACT_UTC(DOCU.GMT_CREACION)) >= DELEG.FYH_VERANO_INICIO AND (SYS_EXTRACT_UTC(DOCU.GMT_CREACION)) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
        TRIM(TO_CHAR(MOD((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (SYS_EXTRACT_UTC(DOCU.GMT_CREACION)) >= DELEG.FYH_VERANO_INICIO AND (SYS_EXTRACT_UTC(DOCU.GMT_CREACION)) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') GMT_CREACION,
        TPLANIFI.COD_TIPO_PLANIFICACION,
        TPLANIFI.DES_TIPO_PLANIFICACION
      from    sapr_Tdocumento docu
      INNER JOIN SAPR_VCUENTA VISTA_CUENTA ON VISTA_CUENTA.OID_CUENTA = docu.oid_cuenta_saldo_origen
      INNER JOIN GEPR_TDELEGACION DELEG ON DELEG.OID_DELEGACION = VISTA_CUENTA.OID_DELEGACION
      INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOCU.OID_FORMULARIO
      INNER JOIN SAPR_TACCION_CONTABLE ACCCONTABLE ON ACCCONTABLE.OID_ACCION_CONTABLE = FORMU.OID_ACCION_CONTABLE
      LEFT OUTER JOIN GEPR_TCODIGO_AJENO DELEGAJ ON DELEGAJ.OID_TABLA_GENESIS = DELEG.OID_DELEGACION AND DELEGAJ.COD_IDENTIFICADOR = 'MAE' AND DELEGAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
      LEFT OUTER JOIN GEPR_TCODIGO_AJENO CLIENTEAJ ON CLIENTEAJ.OID_TABLA_GENESIS = VISTA_CUENTA.OID_CLIENTE AND CLIENTEAJ.COD_IDENTIFICADOR = 'MAE' AND CLIENTEAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
      LEFT OUTER JOIN GEPR_TCODIGO_AJENO SUBCLIAJ ON SUBCLIAJ.OID_TABLA_GENESIS = VISTA_CUENTA.OID_SUBCLIENTE AND SUBCLIAJ.COD_IDENTIFICADOR = 'MAE' AND SUBCLIAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
      LEFT OUTER JOIN GEPR_TCODIGO_AJENO PTOAJ ON PTOAJ.OID_TABLA_GENESIS = VISTA_CUENTA.OID_PTO_SERVICIO AND PTOAJ.COD_IDENTIFICADOR = 'MAE' AND PTOAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
      LEFT OUTER JOIN GEPR_TCODIGO_AJENO CANAJ ON CANAJ.OID_TABLA_GENESIS = VISTA_CUENTA.OID_CANAL AND CANAJ.COD_IDENTIFICADOR = 'MAE' AND CANAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
      LEFT OUTER JOIN GEPR_TCODIGO_AJENO SUBCAJ ON SUBCAJ.OID_TABLA_GENESIS = VISTA_CUENTA.OID_SUBCANAL AND SUBCAJ.COD_IDENTIFICADOR = 'MAE' AND SUBCAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
      LEFT OUTER JOIN SAPR_TPERIODOXDOCUMENTO PERXDOCU ON PERXDOCU.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
      LEFT OUTER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PERXDOCU.OID_PERIODO
      LEFT OUTER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO
      LEFT OUTER JOIN SAPR_TPLANIFICACION PLANIFI ON PLANIFI.OID_PLANIFICACION = PERI.OID_PLANIFICACION
      LEFT OUTER JOIN GEPR_TDELEGACION DELEG_PLANI ON DELEG_PLANI.OID_DELEGACION = PLANIFI.OID_DELEGACION
      LEFT OUTER JOIN SAPR_TTIPO_PLANIFICACION TPLANIFI ON TPLANIFI.OID_TIPO_PLANIFICACION = PLANIFI.OID_TIPO_PLANIFICACION
      LEFT OUTER JOIN GEPR_TCLIENTE BANCO ON BANCO.OID_CLIENTE = PLANIFI.OID_CLIENTE
      LEFT OUTER JOIN GEPR_TCODIGO_AJENO BANCOAJ ON BANCOAJ.OID_TABLA_GENESIS = BANCO.OID_CLIENTE AND BANCOAJ.COD_IDENTIFICADOR = 'MAE' AND BANCOAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
      WHERE
        docu.oid_documento = par$oid_documento
        AND (TIPE.COD_TIPO_PERIODO IS NULL OR 
            TIPE.COD_TIPO_PERIODO = 'AC' OR 
            (FORMU.COD_FORMULARIO IN ('MAESOC','MAESOD') AND TIPE.COD_TIPO_PERIODO = 'RE') 
            )
        ) LOOP

        -- Limpiamos las variables
        var$cod_planificacion      := NULL;
        var$des_planificacion      := NULL;
        var$cod_planif_banco_ope   := NULL;
        var$cod_planif_banco_mdm   := NULL;
        var$des_planif_banco       := NULL;
        var$fyh_planif_periodo_ini := NULL;
        var$fyh_planif_periodo_fin := NULL;
        var$cod_tipo_planificacion := NULL;
        var$des_tipo_planificacion := NULL;

        -- Buscar datos en caso de estar el documento relacionado al período
        IF rec$movimiento.COD_PLANIFICACION IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     =>  'El documento se encuentra relacionado a un período',
                        par$cod_identificador   => '');

          var$cod_planificacion      := rec$movimiento.COD_PLANIFICACION;
          var$des_planificacion      := rec$movimiento.DES_PLANIFICACION;
          var$cod_planif_banco_ope   := rec$movimiento.COD_PLANIF_BANCO_OPE;
          var$cod_planif_banco_mdm   := rec$movimiento.COD_PLANIF_BANCO_MDM;
          var$des_planif_banco       := rec$movimiento.DES_PLANIF_BANCO;
          var$fyh_planif_periodo_ini := rec$movimiento.FYH_PLANIF_PERIODO_INI;
          var$fyh_planif_periodo_fin := rec$movimiento.FYH_PLANIF_PERIODO_FIN;
          var$cod_tipo_planificacion := rec$movimiento.COD_TIPO_PLANIFICACION;
          var$des_tipo_planificacion := rec$movimiento.DES_TIPO_PLANIFICACION;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
            par$des_origen      => const$nombre_func,
            par$des_version     => const$version,                            
            par$des_detalle     => 'Se obtienen los datos del período relacionado al documento: ' || const$new_line ||  
                                    'var$des_planificacion: '     || var$des_planificacion    || const$new_line ||  
                                    'var$cod_planif_banco_ope: '  || var$cod_planif_banco_ope || const$new_line ||  
                                    'var$cod_planif_banco_mdm: '  || var$cod_planif_banco_mdm || const$new_line ||  
                                    'var$des_planif_banco: '      || var$des_planif_banco     || const$new_line ||  
                                    'var$fyh_planif_periodo_ini: '  || TO_CHAR(var$fyh_planif_periodo_ini, 'DD-MM-YYYY HH24:MI:SS') || const$new_line || 
                                    'var$fyh_planif_periodo_fin: '  || TO_CHAR(var$fyh_planif_periodo_fin, 'DD-MM-YYYY HH24:MI:SS') || const$new_line || 
                                    'var$cod_tipo_planificacion: '  || var$cod_tipo_planificacion || const$new_line || 
                                    'var$des_tipo_planificacion: '  || var$des_tipo_planificacion,
            par$cod_identificador   => '');

        ELSE
          -- Documento no relacionado a un período
          -- Caso no encontrarse por no estar relacionado buscar en procedimeinto SAPR_PPLANIFICACION_###VERSION###.sverifica_documento
          -- Para saber si debería realacionarse a un período

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     =>  'El documento no se encuentra relacionado a un período',
                        par$cod_identificador   => '');

          IF SAPR_PPERIODO_###VERSION###.fregla_doc_periodo_ac(par$oid_llamada   => par$oid_llamada,
                                                                  par$oid_documento => par$oid_documento) THEN

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                    par$des_origen      => const$nombre_func,
                                    par$des_version     => const$version,
                                    par$des_detalle     =>  'El documento cumple las reglas para relacionar a período de acreditación',
                                    par$cod_identificador   => '');

            SAPR_PPLANIFICACION_###VERSION###.sverifica_documento(par$oid_llamada  => par$oid_llamada,
                                                          par$oid_maquina       => rec$movimiento.OID_MAQUINA,
                                                          par$cod_canal         => rec$movimiento.COD_CANAL,
                                                          par$cod_subcanal      => rec$movimiento.COD_SUBCANAL,
                                                          par$fyh_gestion       => rec$movimiento.FYH_GESTION,
                                                          par$cod_cultura       => NULL,
                                                          -- VARIABLES DE SALIDA
                                                          par$oid_planificacion => var$oid_planificacion,
                                                          par$cod_validaciones  => var$aux_cod_validaciones,
                                                          par$des_validaciones  => var$aux_des_validaciones);


            
            IF var$oid_planificacion IS NOT NULL THEN
              -- Buscar los datos de la planificación de la tabla auxiliar 
              SELECT 
                COD_CAMPO2,
                COD_CAMPO3,
                COD_CAMPO4,
                COD_CAMPO8,
                COD_CAMPO9,
                COD_CAMPO10,
                COD_CAMPO11
              INTO 
                var$cod_planificacion,
                var$des_planificacion, 
                var$cod_planif_banco_ope,
                var$cod_planif_banco_mdm,
                var$des_planif_banco,
                var$cod_tipo_planificacion,
                var$des_tipo_planificacion
              FROM SAPR_GTT_TAUXILIAR
              WHERE COD_CALIFICADOR = 'DATOS_PLANIFICACION';

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$nombre_func,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Se obtienen los datos de la planificación a la que debería estar relacionada el documento: ' || const$new_line ||  
                                        'var$cod_planificacion: '     || var$cod_planificacion    || const$new_line ||  
                                        'var$des_planificacion: '     || var$des_planificacion    || const$new_line ||  
                                        'var$cod_planif_banco_ope: '  || var$cod_planif_banco_ope || const$new_line ||  
                                        'var$cod_planif_banco_mdm: '  || var$cod_planif_banco_mdm || const$new_line ||  
                                        'var$des_planif_banco: '      || var$des_planif_banco     || const$new_line ||  
                                        'var$cod_tipo_planificacion: '  || var$cod_tipo_planificacion || const$new_line ||  
                                        'var$des_tipo_planificacion: '  || var$des_tipo_planificacion,
                par$cod_identificador   => '');
            END IF;
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                    par$des_origen      => const$nombre_func,
                                    par$des_version     => const$version,
                                    par$des_detalle     =>  'El documento no cumple las reglas para relacionar a período de acreditación',
                                    par$cod_identificador   => '');

            IF (rec$movimiento.COD_TIPO_MOVIMIENTO IN ('MAESOC','MAESOD')) THEN
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                    par$des_origen      => const$nombre_func,
                                    par$des_version     => const$version,
                                    par$des_detalle     =>  'El documento es un SHIPOUT verificamos si debe relacionarse a una planificación de Fecha Valor con Confirmación',
                                    par$cod_identificador   => '');

              SAPR_PPLANIFICACION_###VERSION###.sverifica_documento(par$oid_llamada  => par$oid_llamada,
                                                par$oid_maquina       => rec$movimiento.OID_MAQUINA,
                                                par$cod_canal         => rec$movimiento.COD_CANAL,
                                                par$cod_subcanal      => rec$movimiento.COD_SUBCANAL,
                                                par$fyh_gestion       => rec$movimiento.FYH_GESTION,
                                                par$cod_cultura       => NULL,
                                                -- VARIABLES DE SALIDA
                                                par$oid_planificacion => var$oid_planificacion,
                                                par$cod_validaciones  => var$aux_cod_validaciones,
                                                par$des_validaciones  => var$aux_des_validaciones);

              IF var$oid_planificacion IS NOT NULL THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                      par$des_origen      => const$nombre_func,
                                      par$des_version     => const$version,
                                      par$des_detalle     =>  'El documento cumple las reglas de la planificación, evaluamos si se trata de una planificación de Fecha Valor con Confirmación',
                                      par$cod_identificador   => '');

                SELECT COUNT(1)
                  INTO var$existe
                FROM SAPR_TPLANIFICACION PLANI
                  INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION
                  INNER JOIN SAPR_TPROCESOXPLANIFICACION TPXP ON TPXP.OID_PLANIFICACION = PLANI.OID_PLANIFICACION
                  INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO
                WHERE PLANI.OID_PLANIFICACION = var$oid_planificacion
                  AND TPLANI.COD_TIPO_PLANIFICACION = 'FECHA_VALOR_CONFIR' 
                  AND TPRO.COD_PROCESO = 'BBVA_PERU';

                IF var$existe > 0 THEN
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                      par$des_origen      => const$nombre_func,
                                      par$des_version     => const$version,
                                      par$des_detalle     =>  'La planificación es de Fecha Valor con Confirmación y con proceso BBVA_PERU',
                                      par$cod_identificador   => '');

                  -- Buscar los datos de la planificación de la tabla auxiliar 
                  SELECT 
                    COD_CAMPO2,
                    COD_CAMPO3,
                    COD_CAMPO4,
                    COD_CAMPO8,
                    COD_CAMPO9,
                    COD_CAMPO10,
                    COD_CAMPO11
                  INTO 
                    var$cod_planificacion,
                    var$des_planificacion, 
                    var$cod_planif_banco_ope,
                    var$cod_planif_banco_mdm,
                    var$des_planif_banco,
                    var$cod_tipo_planificacion,
                    var$des_tipo_planificacion
                  FROM SAPR_GTT_TAUXILIAR
                  WHERE COD_CALIFICADOR = 'DATOS_PLANIFICACION';

                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                  par$des_origen      => const$nombre_func,
                                  par$des_version     => const$version,                            
                                  par$des_detalle     => 'Se obtienen los datos de la planificación a la que debería estar relacionada el documento: ' || const$new_line ||  
                                                          'var$cod_planificacion: '     || var$cod_planificacion    || const$new_line ||  
                                                          'var$des_planificacion: '     || var$des_planificacion    || const$new_line ||  
                                                          'var$cod_planif_banco_ope: '  || var$cod_planif_banco_ope || const$new_line ||  
                                                          'var$cod_planif_banco_mdm: '  || var$cod_planif_banco_mdm || const$new_line ||  
                                                          'var$des_planif_banco: '      || var$des_planif_banco     || const$new_line ||  
                                                          'var$cod_tipo_planificacion: '  || var$cod_tipo_planificacion || const$new_line ||  
                                                          'var$des_tipo_planificacion: '  || var$des_tipo_planificacion,
                                  par$cod_identificador   => '');
                ELSE
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                      par$des_origen      => const$nombre_func,
                                      par$des_version     => const$version,
                                      par$des_detalle     =>  'La planificación NO es de Fecha Valor con Confirmación y con proceso BBVA_PERU',
                                      par$cod_identificador   => '');
                END IF;
              ELSE
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                        par$des_origen      => const$nombre_func,
                                        par$des_version     => const$version,
                                        par$des_detalle     =>  'El documento no cumple las reglas de la planificación',
                                        par$cod_identificador   => '');
              END IF;
            END IF;
          END IF;
        END IF;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen      => const$nombre_func,
                                            par$des_version     => const$version,
                                            par$des_detalle     =>  'Llamada a procedimiento sgrabar_mov_consolidado',
                                            par$cod_identificador   => '');

        sgrabar_mov_consolidado(
          par$OID_MOVIMIENTO            => rec$movimiento.oid_movimiento,
          par$OID_DOCUMENTO             => par$oid_documento,
          par$COD_PAIS                  => rec$movimiento.COD_PAIS,
          par$COD_DELEGACION_OPE        => rec$movimiento.COD_DELEGACION_OPE,
          par$COD_DELEGACION_MDM        => rec$movimiento.COD_DELEGACION_MDM,
          par$DES_DELEGACION            => rec$movimiento.DES_DELEGACION,
          par$COD_CLIENTE_OPE           => rec$movimiento.COD_CLIENTE_OPE,
          par$COD_CLIENTE_MDM           => rec$movimiento.COD_CLIENTE_MDM,
          par$DES_CLIENTE               => rec$movimiento.DES_CLIENTE,
          par$DES_CLIENTE_OPE           => rec$movimiento.DES_CLIENTE_OPE,
          par$COD_SUBCLIENTE_OPE        => rec$movimiento.COD_SUBCLIENTE_OPE,
          par$COD_SUBCLIENTE_MDM        => rec$movimiento.COD_SUBCLIENTE_MDM,
          par$DES_SUBCLIENTE            => rec$movimiento.DES_SUBCLIENTE,
          par$DES_SUBCLIENTE_OPE        => rec$movimiento.DES_SUBCLIENTE_OPE,
          par$COD_PTO_SERVICIO_OPE      => rec$movimiento.COD_PTO_SERVICIO_OPE,
          par$COD_PTO_SERVICIO_MDM      => rec$movimiento.COD_PTO_SERVICIO_MDM,
          par$DES_PTO_SERVICIO          => rec$movimiento.DES_PTO_SERVICIO,
          par$DES_PTO_SERVICIO_OPE      => rec$movimiento.DES_PTO_SERVICIO_OPE,
          par$COD_CANAL                 => rec$movimiento.COD_CANAL,
          par$DES_CANAL                 => rec$movimiento.DES_CANAL,
          par$COD_SUBCANAL              => rec$movimiento.COD_SUBCANAL,
          par$DES_SUBCANAL              => rec$movimiento.DES_SUBCANAL,
          par$COD_DEVICEID              => rec$movimiento.COD_DEVICEID,
          par$DES_MAQUINA               => rec$movimiento.DES_MAQUINA,
          par$FEC_MOVIMIENTO            => rec$movimiento.FEC_MOVIMIENTO,
          par$HOR_MOVIMIENTO            => rec$movimiento.HOR_MOVIMIENTO,
          par$FYH_CONTABLE              => rec$movimiento.FYH_CONTABLE,
          par$COD_TIPO_MOVIMIENTO       => rec$movimiento.COD_TIPO_MOVIMIENTO,
          par$DES_TIPO_MOVIMIENTO       => rec$movimiento.DES_TIPO_MOVIMIENTO,
          par$COD_GRUPO_MOVIMIENTO      => rec$movimiento.COD_GRUPO_MOVIMIENTO,
          par$COD_MOVIMIENTO            => rec$movimiento.COD_MOVIMIENTO,
          par$COD_COMPROBANTE           => rec$movimiento.COD_COMPROBANTE,
          par$COD_ACTUAL_ID             => rec$movimiento.COD_ACTUAL_ID,
          par$COD_COLLECTION_ID         => rec$movimiento.COD_COLLECTION_ID,
          par$BOL_NOTIFICADO            => rec$movimiento.BOL_NOTIFICADO,
          par$FYH_NOTIFICACION          => rec$movimiento.FYH_NOTIFICACION,
          par$BOL_ACREDITADO            => rec$movimiento.BOL_ACREDITADO,
          par$FYH_ACREDITACION          => rec$movimiento.FYH_ACREDITACION,
          par$COD_PLANIFICACION         => var$cod_planificacion,
          par$DES_PLANIFICACION         => var$des_planificacion,
          par$COD_PLANIF_BANCO_OPE      => var$cod_planif_banco_ope,
          par$COD_PLANIF_BANCO_MDM      => var$cod_planif_banco_mdm,
          par$DES_PLANIF_BANCO          => var$des_planif_banco,
          par$FYH_PLANIF_PERIODO_INI    => var$fyh_planif_periodo_ini,
          par$FYH_PLANIF_PERIODO_FIN    => var$fyh_planif_periodo_fin,
          par$COD_TIPO_PLANIFICACION    => var$COD_TIPO_PLANIFICACION,
          par$DES_TIPO_PLANIFICACION    => var$DES_TIPO_PLANIFICACION,
          par$COD_DIVISA                => rec$movimiento.COD_DIVISA,
          par$DES_DIVISA                => rec$movimiento.DES_DIVISA,
          par$num_importe               => rec$movimiento.NUM_IMPORTE,

          /* MOVIMIENTO DETALLE */
          par$aoid_movimiento_detalle   => var$aoid_movimiento_detalle,
          par$aimp_movi_detalle         => var$aimp_movi_detalle,
          par$anel_cant_movi_detalle    => var$anel_cant_movi_detalle,
          par$abol_billete_movi_detalle => var$abol_billete_movi_detalle,
          par$acod_denom_movi_detalle   => var$acod_denom_movi_detalle,
          par$ades_denom_movi_detalle   => var$ades_denom_movi_detalle,

          /* MOVIMIENTO CAMPOS EXTRAS */
          par$aoid_mov_campos_extras    => var$aoid_mov_campos_extras,
          par$acod_campo_extra          => var$acod_campo_extra,
          par$ades_campo_extra          => var$ades_campo_extra,

          par$BOL_VIRTUAL               => rec$movimiento.BOL_VIRTUAL,
          par$GMT_CREACION              => rec$movimiento.GMT_CREACION,
          par$cod_usuario               => par$cod_usuario
        );
    END LOOP;


  END sgrabar_movimiento_por_doc;
  
  /* Procedimiento utilizado en trigger SAPR_TPERIODOXDOCUMENTO para actualizar los datos de la planificación y del período relacionado al documento */
  procedure supd_por_relacion_periodo(par$oid_documento    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_periodo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_usuario      IN gepr_pcomon_###VERSION###.tipo$cod_
  ) IS 
    var$oid_movimiento          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_planificacion       gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_planificacion       gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_planif_banco_ope    gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planif_banco_mdm    gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_planif_banco        gepr_pcomon_###VERSION###.tipo$desc_;
    var$fyh_planif_periodo_ini  gepr_pcomon_###VERSION###.tipo$gmt_; 
    var$fyh_planif_periodo_fin  gepr_pcomon_###VERSION###.tipo$gmt_;
    var$existe                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$cod_tipo_planificacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_tipo_planificacion  gepr_pcomon_###VERSION###.tipo$desc_;
  BEGIN 

    -- Verificamos si existe previamente el documento en la tabla de movimiento
    SELECT COUNT(1)
      INTO var$existe
    FROM SAPR_TMOVIMIENTO
    WHERE OID_DOCUMENTO = par$oid_documento;

    IF var$existe > 0 THEN
      BEGIN 
        SELECT
          PLANIFI.COD_PLANIFICACION COD_PLANIFICACION,
          PLANIFI.DES_PLANIFICACION DES_PLANIFICACION,
          BANCOAJ.COD_AJENO COD_PLANIF_BANCO_OPE,
          BANCO.COD_CLIENTE COD_PLANIF_BANCO_MDM,
          BANCO.DES_CLIENTE DES_PLANIF_BANCO,
          CASE WHEN PERI.FYH_INICIO IS NOT NULL THEN
          TO_TIMESTAMP_TZ(TO_CHAR(PERI.FYH_INICIO + ((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_INICIO) >= DELEG.FYH_VERANO_INICIO AND (PERI.FYH_INICIO) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
          TRIM(TO_CHAR(ROUND((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_INICIO) >= DELEG.FYH_VERANO_INICIO AND (PERI.FYH_INICIO) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
          TRIM(TO_CHAR(MOD((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_INICIO) >= DELEG.FYH_VERANO_INICIO AND (PERI.FYH_INICIO) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') 
          ELSE PERI.FYH_INICIO END FYH_PLANIF_PERIODO_INI,
          CASE WHEN PERI.FYH_FIN IS NOT NULL THEN
          TO_TIMESTAMP_TZ(TO_CHAR(PERI.FYH_FIN + ((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_FIN) >= DELEG.FYH_VERANO_INICIO AND (PERI.FYH_FIN) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END) / 1440), 'DD/MM/YYYY HH24:MI:SS ') ||  
          TRIM(TO_CHAR(ROUND((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_FIN) >= DELEG.FYH_VERANO_INICIO AND (PERI.FYH_FIN) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' ||
          TRIM(TO_CHAR(MOD((NVL(DELEG.NEC_GMT_MINUTOS, 0) + CASE WHEN (PERI.FYH_FIN) >= DELEG.FYH_VERANO_INICIO AND (PERI.FYH_FIN) <= DELEG.FYH_VERANO_FIN THEN NVL(DELEG.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') 
          ELSE PERI.FYH_FIN END FYH_PLANIF_PERIODO_FIN,
          TPLANIFI.COD_TIPO_PLANIFICACION COD_TIPO_PLANIFICACION,
          TPLANIFI.DES_TIPO_PLANIFICACION DES_TIPO_PLANIFICACION
        INTO 
          var$cod_planificacion,
          var$des_planificacion,
          var$cod_planif_banco_ope,
          var$cod_planif_banco_mdm,
          var$des_planif_banco,
          var$fyh_planif_periodo_ini,
          var$fyh_planif_periodo_fin,
          var$cod_tipo_planificacion,
          var$des_tipo_planificacion
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND 
                                                TIPE.COD_TIPO_PERIODO = 'AC'
          INNER JOIN SAPR_TPLANIFICACION PLANIFI ON PLANIFI.OID_PLANIFICACION = PERI.OID_PLANIFICACION
          INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANIFI ON TPLANIFI.OID_TIPO_PLANIFICACION = PLANIFI.OID_TIPO_PLANIFICACION
          INNER JOIN GEPR_TDELEGACION DELEG ON DELEG.OID_DELEGACION = PLANIFI.OID_DELEGACION
          INNER JOIN GEPR_TCLIENTE BANCO ON BANCO.OID_CLIENTE = PLANIFI.OID_CLIENTE
          LEFT JOIN GEPR_TCODIGO_AJENO BANCOAJ ON BANCOAJ.OID_TABLA_GENESIS = BANCO.OID_CLIENTE AND 
                                                  BANCOAJ.COD_IDENTIFICADOR = 'MAE' AND 
                                                  BANCOAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
        WHERE
          PERI.OID_PERIODO = par$oid_periodo;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          var$cod_planificacion := NULL;
      END;

      IF var$cod_planificacion IS NOT NULL THEN
        -- Agregamos/actualizamos los datos de la planificación y del período de la tabla SAPR_TMOVIMIENTO
        supd_movimiento(par$OID_MOVIMIENTO                  => var$oid_movimiento,
                        par$OID_DOCUMENTO                   => par$oid_documento,
                        par$COD_PAIS                        => NULL,
                        par$COD_DELEGACION_OPE              => NULL,
                        par$COD_DELEGACION_MDM              => NULL,
                        par$DES_DELEGACION                  => NULL,
                        par$COD_CLIENTE_OPE                 => NULL,
                        par$COD_CLIENTE_MDM                 => NULL,
                        par$DES_CLIENTE                     => NULL,
                        par$DES_CLIENTE_OPE                 => NULL,
                        par$COD_SUBCLIENTE_OPE              => NULL,
                        par$COD_SUBCLIENTE_MDM              => NULL,
                        par$DES_SUBCLIENTE                  => NULL,
                        par$DES_SUBCLIENTE_OPE              => NULL,
                        par$COD_PTO_SERVICIO_OPE            => NULL,
                        par$COD_PTO_SERVICIO_MDM            => NULL,
                        par$DES_PTO_SERVICIO                => NULL,
                        par$DES_PTO_SERVICIO_OPE            => NULL,
                        par$COD_CANAL                       => NULL,
                        par$DES_CANAL                       => NULL,
                        par$COD_SUBCANAL                    => NULL,
                        par$DES_SUBCANAL                    => NULL,
                        par$COD_DEVICEID                    => NULL,
                        par$DES_MAQUINA                     => NULL,
                        par$FEC_MOVIMIENTO                  => NULL,
                        par$HOR_MOVIMIENTO                  => NULL,
                        par$FYH_CONTABLE                    => NULL,
                        par$COD_TIPO_MOVIMIENTO             => NULL,
                        par$DES_TIPO_MOVIMIENTO             => NULL,
                        par$COD_GRUPO_MOVIMIENTO            => NULL,
                        par$COD_MOVIMIENTO                  => NULL,
                        par$COD_COMPROBANTE                 => NULL,
                        par$COD_ACTUAL_ID                   => NULL,
                        par$COD_COLLECTION_ID               => NULL,
                        par$BOL_NOTIFICADO                  => NULL,
                        par$FYH_NOTIFICACION                => NULL,
                        par$BOL_ACREDITADO                  => NULL,
                        par$FYH_ACREDITACION                => NULL,
                        par$COD_PLANIFICACION               => var$cod_planificacion,
                        par$DES_PLANIFICACION               => var$des_planificacion,
                        par$COD_PLANIF_BANCO_OPE            => var$cod_planif_banco_ope,
                        par$COD_PLANIF_BANCO_MDM            => var$cod_planif_banco_mdm,
                        par$DES_PLANIF_BANCO                => var$des_planif_banco,
                        par$FYH_PLANIF_PERIODO_INI          => var$fyh_planif_periodo_ini,
                        par$FYH_PLANIF_PERIODO_FIN          => var$fyh_planif_periodo_fin,
                        par$COD_TIPO_PLANIFICACION          => var$cod_tipo_planificacion,
                        par$DES_TIPO_PLANIFICACION          => var$des_tipo_planificacion,
                        par$COD_DIVISA                      => NULL,
                        par$DES_DIVISA                      => NULL,
                        par$NUM_IMPORTE                     => NULL,
                        par$BOL_VIRTUAL                     => NULL,
                        par$GMT_CREACION                    => NULL,
                        par$COD_USUARIO                     => par$cod_usuario
                      );
      END IF;
    END IF;
  END supd_por_relacion_periodo;

  /* Procedimiento utilizado en trigger SAPR_TPERIODOXDOCUMENTO para remover los datos de la planificación y/o del período desrelacionado al documento */
  procedure supd_por_desrelacion_periodo(par$oid_documento    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_periodo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_usuario      IN gepr_pcomon_###VERSION###.tipo$cod_
  ) IS 
    var$oid_movimiento          gepr_pcomon_###VERSION###.tipo$oid_;
    var$existe                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN 
  -- Verificamos si existe previamente el documento en la tabla de movimiento
    SELECT COUNT(1)
      INTO var$existe
    FROM SAPR_TMOVIMIENTO
    WHERE OID_DOCUMENTO = par$oid_documento;

    IF var$existe > 0 THEN
      -- Buscamos si la máquina relacionada al documento se encuentra activa en la planificación
      SELECT COUNT(1) 
      INTO var$existe
      FROM SAPR_TDOCUMENTO DOCU
        INNER JOIN SAPR_VCUENTA VISTA_CUENTA ON VISTA_CUENTA.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN
        INNER JOIN SAPR_TPLANXMAQUINA PXM ON PXM.OID_MAQUINA = VISTA_CUENTA.OID_MAQUINA AND PXM.BOL_ACTIVO = 1
        INNER JOIN SAPR_TPLANIFICACION PLANIFI ON PLANIFI.OID_PLANIFICACION = PXM.OID_PLANIFICACION
        INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PLANIFICACION = PLANIFI.OID_PLANIFICACION AND PERI.OID_MAQUINA = VISTA_CUENTA.OID_MAQUINA
      WHERE DOCU.OID_DOCUMENTO = par$oid_documento
            AND PERI.OID_PERIODO = par$oid_periodo;
      

      IF var$existe > 0 THEN
        -- Si la máquina aún pertenece a la planificación removemos los datos del período de acreditación solamente de la tabla SAPR_TMOVIMIENTO
        -- Verificamos el caso de un periodo de acreditación
        SELECT COUNT(1) 
        INTO var$existe
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
        WHERE PERI.OID_PERIODO = par$oid_periodo;
        
        IF var$existe > 0 THEN
          -- Se trata de un período de acreditación
          supd_movimiento(par$OID_MOVIMIENTO              => var$oid_movimiento,
                      par$OID_DOCUMENTO                   => par$oid_documento,
                      par$COD_PAIS                        => NULL,
                      par$COD_DELEGACION_OPE              => NULL,
                      par$COD_DELEGACION_MDM              => NULL,
                      par$DES_DELEGACION                  => NULL,
                      par$COD_CLIENTE_OPE                 => NULL,
                      par$COD_CLIENTE_MDM                 => NULL,
                      par$DES_CLIENTE                     => NULL,
                      par$DES_CLIENTE_OPE                 => NULL,
                      par$COD_SUBCLIENTE_OPE              => NULL,
                      par$COD_SUBCLIENTE_MDM              => NULL,
                      par$DES_SUBCLIENTE                  => NULL,
                      par$DES_SUBCLIENTE_OPE              => NULL,
                      par$COD_PTO_SERVICIO_OPE            => NULL,
                      par$COD_PTO_SERVICIO_MDM            => NULL,
                      par$DES_PTO_SERVICIO                => NULL,
                      par$DES_PTO_SERVICIO_OPE            => NULL,
                      par$COD_CANAL                       => NULL,
                      par$DES_CANAL                       => NULL,
                      par$COD_SUBCANAL                    => NULL,
                      par$DES_SUBCANAL                    => NULL,
                      par$COD_DEVICEID                    => NULL,
                      par$DES_MAQUINA                     => NULL,
                      par$FEC_MOVIMIENTO                  => NULL,
                      par$HOR_MOVIMIENTO                  => NULL,
                      par$FYH_CONTABLE                    => NULL,
                      par$COD_TIPO_MOVIMIENTO             => NULL,
                      par$DES_TIPO_MOVIMIENTO             => NULL,
                      par$COD_GRUPO_MOVIMIENTO            => NULL,
                      par$COD_MOVIMIENTO                  => NULL,
                      par$COD_COMPROBANTE                 => NULL,
                      par$COD_ACTUAL_ID                   => NULL,
                      par$COD_COLLECTION_ID               => NULL,
                      par$BOL_NOTIFICADO                  => NULL,
                      par$FYH_NOTIFICACION                => NULL,
                      par$BOL_ACREDITADO                  => NULL,
                      par$FYH_ACREDITACION                => NULL,
                      par$COD_PLANIFICACION               => NULL,
                      par$DES_PLANIFICACION               => NULL,
                      par$COD_PLANIF_BANCO_OPE            => NULL,
                      par$COD_PLANIF_BANCO_MDM            => NULL,
                      par$DES_PLANIF_BANCO                => NULL,
                      par$FYH_PLANIF_PERIODO_INI          => TO_DATE('0001-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'),
                      par$FYH_PLANIF_PERIODO_FIN          => TO_DATE('0001-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'),
                      par$COD_TIPO_PLANIFICACION          => NULL,
                      par$DES_TIPO_PLANIFICACION          => NULL,
                      par$COD_DIVISA                      => NULL,
                      par$DES_DIVISA                      => NULL,
                      par$NUM_IMPORTE                     => NULL,
                      par$BOL_VIRTUAL                     => NULL,
                      par$GMT_CREACION                    => NULL,
                      par$COD_USUARIO                     => par$cod_usuario
                    );
        END IF;
      ELSE
        -- Si la máquina ya no pertenece a la planificación removemos los datos de la planificación y del período
        supd_movimiento(par$OID_MOVIMIENTO                => var$oid_movimiento,
                      par$OID_DOCUMENTO                   => par$oid_documento,
                      par$COD_PAIS                        => NULL,
                      par$COD_DELEGACION_OPE              => NULL,
                      par$COD_DELEGACION_MDM              => NULL,
                      par$DES_DELEGACION                  => NULL,
                      par$COD_CLIENTE_OPE                 => NULL,
                      par$COD_CLIENTE_MDM                 => NULL,
                      par$DES_CLIENTE                     => NULL,
                      par$DES_CLIENTE_OPE                 => NULL,
                      par$COD_SUBCLIENTE_OPE              => NULL,
                      par$COD_SUBCLIENTE_MDM              => NULL,
                      par$DES_SUBCLIENTE                  => NULL,
                      par$DES_SUBCLIENTE_OPE              => NULL,
                      par$COD_PTO_SERVICIO_OPE            => NULL,
                      par$COD_PTO_SERVICIO_MDM            => NULL,
                      par$DES_PTO_SERVICIO                => NULL,
                      par$DES_PTO_SERVICIO_OPE            => NULL,
                      par$COD_CANAL                       => NULL,
                      par$DES_CANAL                       => NULL,
                      par$COD_SUBCANAL                    => NULL,
                      par$DES_SUBCANAL                    => NULL,
                      par$COD_DEVICEID                    => NULL,
                      par$DES_MAQUINA                     => NULL,
                      par$FEC_MOVIMIENTO                  => NULL,
                      par$HOR_MOVIMIENTO                  => NULL,
                      par$FYH_CONTABLE                    => NULL,
                      par$COD_TIPO_MOVIMIENTO             => NULL,
                      par$DES_TIPO_MOVIMIENTO             => NULL,
                      par$COD_GRUPO_MOVIMIENTO            => NULL,
                      par$COD_MOVIMIENTO                  => NULL,
                      par$COD_COMPROBANTE                 => NULL,
                      par$COD_ACTUAL_ID                   => NULL,
                      par$COD_COLLECTION_ID               => NULL,
                      par$BOL_NOTIFICADO                  => NULL,
                      par$FYH_NOTIFICACION                => NULL,
                      par$BOL_ACREDITADO                  => NULL,
                      par$FYH_ACREDITACION                => NULL,
                      par$COD_PLANIFICACION               => const$vacio,
                      par$DES_PLANIFICACION               => const$vacio,
                      par$COD_PLANIF_BANCO_OPE            => const$vacio,
                      par$COD_PLANIF_BANCO_MDM            => const$vacio,
                      par$DES_PLANIF_BANCO                => const$vacio,
                      par$FYH_PLANIF_PERIODO_INI          => TO_DATE('0001-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'),
                      par$FYH_PLANIF_PERIODO_FIN          => TO_DATE('0001-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'),
                      par$COD_TIPO_PLANIFICACION          => const$vacio,
                      par$DES_TIPO_PLANIFICACION          => const$vacio,
                      par$COD_DIVISA                      => NULL,
                      par$DES_DIVISA                      => NULL,
                      par$NUM_IMPORTE                     => NULL,
                      par$BOL_VIRTUAL                     => NULL,
                      par$GMT_CREACION                    => NULL,
                      par$COD_USUARIO                     => par$cod_usuario
                    );
      END IF;
    END IF;

  END supd_por_desrelacion_periodo;

  /* Procedimiento utilizado en trigger SAPR_TPLANXMAQUINA para remover los datos de la planificación una vez quitada la máquina de la planificación */
  procedure supd_por_desrelacion_maq_plani( par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                            par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                            par$fyh_vigencia_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                            par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_
  ) IS 
    var$fyh_fin_ultimo_periodo  gepr_pcomon_###VERSION###.tipo$fyh_;

  BEGIN 
    -- Buscamos el último período acreditado sumandole un segundo para indicar a partir de que fecha/hora se deben modificar los movimientos
    BEGIN 
      SELECT MAX(PERI.FYH_FIN) + (1/60/60/24)
      INTO var$fyh_fin_ultimo_periodo
      FROM SAPR_TPERIODO PERI 
        INNER JOIN SAPR_TESTADO_PERIODO ESPE ON PERI.OID_ESTADO_PERIODO = ESPE.OID_ESTADO_PERIODO AND ESPE.COD_ESTADO_PERIODO = 'AC'
        INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
      WHERE PERI.OID_MAQUINA = par$oid_maquina 
        AND PERI.OID_PLANIFICACION = par$oid_planificacion
        AND PERI.OID_ACREDITACION IS NOT NULL;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$fyh_fin_ultimo_periodo := NULL;
    END;

    -- Buscamos los movimientos que hay que actualizar, estos movimientos no están relacionados a un período
    FOR R_MOVIMIENTO IN 
      (
        SELECT OID_MOVIMIENTO, OID_DOCUMENTO
        FROM SAPR_TMOVIMIENTO MOV 
          INNER JOIN SAPR_TMAQUINA MAQUI ON MAQUI.COD_IDENTIFICACION = MOV.COD_DEVICEID AND MAQUI.OID_MAQUINA = par$oid_maquina
          INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.COD_PLANIFICACION = MOV.COD_PLANIFICACION AND PLANI.OID_PLANIFICACION = par$oid_planificacion
        WHERE MOV.FYH_PLANIFICACION_PERIODO_INI IS NULL
          AND SYS_EXTRACT_UTC(MOV.HOR_MOVIMIENTO) >= NVL(var$fyh_fin_ultimo_periodo, par$fyh_vigencia_inicio)
      ) 
    LOOP
       -- Si la máquina ya no pertenece a la planificación removemos los datos de la planificación
        supd_movimiento(par$OID_MOVIMIENTO                => R_MOVIMIENTO.OID_MOVIMIENTO,
                      par$OID_DOCUMENTO                   => R_MOVIMIENTO.OID_DOCUMENTO,
                      par$COD_PAIS                        => NULL,
                      par$COD_DELEGACION_OPE              => NULL,
                      par$COD_DELEGACION_MDM              => NULL,
                      par$DES_DELEGACION                  => NULL,
                      par$COD_CLIENTE_OPE                 => NULL,
                      par$COD_CLIENTE_MDM                 => NULL,
                      par$DES_CLIENTE                     => NULL,
                      par$DES_CLIENTE_OPE                 => NULL,
                      par$COD_SUBCLIENTE_OPE              => NULL,
                      par$COD_SUBCLIENTE_MDM              => NULL,
                      par$DES_SUBCLIENTE                  => NULL,
                      par$DES_SUBCLIENTE_OPE              => NULL,
                      par$COD_PTO_SERVICIO_OPE            => NULL,
                      par$COD_PTO_SERVICIO_MDM            => NULL,
                      par$DES_PTO_SERVICIO                => NULL,
                      par$DES_PTO_SERVICIO_OPE            => NULL,
                      par$COD_CANAL                       => NULL,
                      par$DES_CANAL                       => NULL,
                      par$COD_SUBCANAL                    => NULL,
                      par$DES_SUBCANAL                    => NULL,
                      par$COD_DEVICEID                    => NULL,
                      par$DES_MAQUINA                     => NULL,
                      par$FEC_MOVIMIENTO                  => NULL,
                      par$HOR_MOVIMIENTO                  => NULL,
                      par$FYH_CONTABLE                    => NULL,
                      par$COD_TIPO_MOVIMIENTO             => NULL,
                      par$DES_TIPO_MOVIMIENTO             => NULL,
                      par$COD_GRUPO_MOVIMIENTO            => NULL,
                      par$COD_MOVIMIENTO                  => NULL,
                      par$COD_COMPROBANTE                 => NULL,
                      par$COD_ACTUAL_ID                   => NULL,
                      par$COD_COLLECTION_ID               => NULL,
                      par$BOL_NOTIFICADO                  => NULL,
                      par$FYH_NOTIFICACION                => NULL,
                      par$BOL_ACREDITADO                  => NULL,
                      par$FYH_ACREDITACION                => NULL,
                      par$COD_PLANIFICACION               => const$vacio,
                      par$DES_PLANIFICACION               => const$vacio,
                      par$COD_PLANIF_BANCO_OPE            => const$vacio,
                      par$COD_PLANIF_BANCO_MDM            => const$vacio,
                      par$DES_PLANIF_BANCO                => const$vacio,
                      par$FYH_PLANIF_PERIODO_INI          => NULL,
                      par$FYH_PLANIF_PERIODO_FIN          => NULL,
                      par$COD_TIPO_PLANIFICACION          => const$vacio,
                      par$DES_TIPO_PLANIFICACION          => const$vacio,
                      par$COD_DIVISA                      => NULL,
                      par$DES_DIVISA                      => NULL,
                      par$NUM_IMPORTE                     => NULL,
                      par$BOL_VIRTUAL                     => NULL,
                      par$GMT_CREACION                    => NULL,
                      par$COD_USUARIO                     => par$cod_usuario
                    );
    END LOOP;
  END supd_por_desrelacion_maq_plani;
END SAPR_PMOVIMIENTO_###VERSION###;
/