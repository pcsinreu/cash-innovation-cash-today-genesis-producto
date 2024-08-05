CREATE OR REPLACE PACKAGE sapr_pplanificacion_###VERSION### AS

  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pplanificacion_###VERSION###';
  const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
  const$comilla_simple  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';

  /* Busqueda planificacion detalle */
  PROCEDURE sbusqueda_planificacion(par$oid_planificacion       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$info_ejecucion          IN VARCHAR2,
                                    par$ele_rc_planificacion    OUT sys_refcursor,
                                    par$ele_rc_maquinas         OUT sys_refcursor,
                                    par$ele_rc_programaciones   OUT sys_refcursor,
                                    par$ele_rc_canales          OUT sys_refcursor,
                                    par$ele_rc_subcanales       OUT sys_refcursor,
                                    par$aje_rc_codigo_ajeno     OUT sys_refcursor,
                                    par$ele_rc_terminos_patron  OUT sys_refcursor,
                                    par$ele_rc_terminos_dinam   OUT sys_refcursor,
                                    par$ele_rc_mensajes         OUT sys_refcursor,
                                    par$ele_rc_procesos         OUT sys_refcursor,
                                    par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects                 OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Recuperar planificacion servicio */
  PROCEDURE srecuperar_planificacion(par$cods_planificacion       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$cod_tipo                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_banco                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_vigente              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$bol_recuperar_maquinas   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                     par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_planificacion         OUT sys_refcursor,
                                     par$rc_maquinas              OUT sys_refcursor,
                                     par$rc_programaciones        OUT sys_refcursor,
                                     par$rc_canales               OUT sys_refcursor,
                                     par$validaciones             OUT sys_refcursor,
                                     par$rc_limites               OUT sys_refcursor,
                                     par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Grabar planificacion */
     PROCEDURE sgrabar_planificacion(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                                  par$oid_planificacion       IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_planificacion       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$des_planificacion       IN gepr_pcomon_###VERSION###.tipo$desc_,
                                  par$oid_tipo_planificacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_tipo_confirmacion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_cliente             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_delegacion          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$fyh_vigencia_inicio     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$fyh_vigencia_fin        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$bol_activo              IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$aprog_num_dia_fin       IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$aprog_fyh_fechahora_fin IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                  par$amaq_oid_maquina        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$acan_oid_canal          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$ascan_oid_subcanal      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$nec_contigencia         IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$bol_cambio_horario      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_cambio_program      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$aoid_cod_ajeno           IN  gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$aoid_tabla_genesis       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$acod_identificador       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$acod_ajeno               IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$ades_ajeno               IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$abol_defecto             IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$bol_controlafacturacion IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$oid_banco_comision      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$por_comision_plan       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                  par$dia_cierre_facturacion  IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                  par$bol_divide_subcanal   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_divide_divisa   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_agrupa_subcanal   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_agrupa_puntoservicio IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_agrupa_fechacontable IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$aoid_iac_patron          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$aoid_termino_patron      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$avalor_termino_patron    IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$aoid_iac_dinam           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$aoid_termino_dinam       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$avalor_termino_dinam     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$a_lim_cod_divisa         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_lim_num                IN gepr_pcomon_###VERSION###.tipo$imps_,
                                  par$a_lim_bol_baja           IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_per_div_codigo     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_per_div_baja       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_per_mov_codigo     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$a_per_mov_baja       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_cod_mensaje            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_des_mensaje            IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$a_tipo_mensaje            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_tipop_mensaje            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$a_sin_reintentos_mensaje  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$aoid_proceso            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) ;

  /* Validar planificacion */
  PROCEDURE svalidar_planificacion(par$cod_planificacion       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$des_planificacion       IN gepr_pcomon_###VERSION###.tipo$desc_,
                                   par$oid_tipo_planificacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_cliente             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_delegacion          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$fyh_vigencia_inicio     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$aprog_num_dia_fin       IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$aprog_fyh_fechahora_fin IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$bol_activo              IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Insert/Update Planificacion */
  PROCEDURE supd_planificacion(par$oid_planificacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_planificacion      IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$des_planificacion      IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$oid_tipo_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_tipo_confirmacion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$oid_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_delegacion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$fyh_vigencia_inicio    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$fyh_vigencia_fin       IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$bol_activo             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$nec_contigencia        IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$bol_controlafacturacion IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$oid_banco_comision      IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$por_comision_plan       IN gepr_pcomon_###VERSION###.tipo$imp_,
                               par$dia_cierre_facturacion  IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$bol_divide_subcanal   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_divide_divisa   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_agrupa_subcanal    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_agrupa_puntoservicio IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_agrupa_fechacontable IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$selects                OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$inserts                OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$updates                OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Insert/Update PlanXMaquinas */
  PROCEDURE supd_planxmaquinas(par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$fyh_vigencia_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$fyh_vigencia_fin    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$amaq_oid_maquina    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$bol_cambio_horario  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_cambio_program      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                 par$fyh_vigencia_fin_old   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$deletes             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$updates             OUT gepr_pcomon_###VERSION###.tipo$nel_);

    /* Insert/Update PlanXMaquinas para fecha valor con confirmacion */
  PROCEDURE supd_planxmaquinas_fvc(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                               par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$fyh_vigencia_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$fyh_vigencia_fin    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$amaq_oid_maquina    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$bol_cambio_horario  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_cambio_program      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$fyh_vigencia_fin_old   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$deletes             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$updates             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Baja de Planificaciones */
  PROCEDURE sdel_planificacion(par$oid_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$amaq_oid_maquina  IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$info_ejecucion    IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$cod_ejecucion     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Insert/Update PlanXCanales */
  PROCEDURE supd_planxcanales(par$oid_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$apto_nel_index        IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$apto_oid_pto_servicio  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$acan_nel_index        IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$acan_oid_canal    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$selects           OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Insert/Update PlanXSubCanales */
 PROCEDURE supd_planxsubcanales( par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$apto_nel_index        IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$apto_oid_pto_servicio  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$ascan_nel_index        IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$ascan_oid_subcanal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$updates             OUT gepr_pcomon_###VERSION###.tipo$nel_);
  /* Insert Programacion */
  PROCEDURE sins_programacion(par$oid_planificacion       IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$aprog_num_dia_fin       IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aprog_fyh_fechahora_fin IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$deletes                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$inserts                 OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE supd_planxmaquina_fecha_incio(par$oid_maquina      IN gepr_pcomon_###VERSION###.tipo$oid_);

 PROCEDURE supd_maquinaxplan( par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$fyh_vigencia_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$fyh_vigencia_fin    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_);

 PROCEDURE svalidar_planxcan_maq(par$oid_planificacion       IN OUT gepr_pcomon_###VERSION###.tipo$oid_,                                 
                                  par$amaq_oid_maquina        IN gepr_pcomon_###VERSION###.tipo$oids_,                                         
                             par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$usr_);
                                                            
PROCEDURE sdel_planxcan_maqu(par$oid_plan_nueva   IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_maquina     IN gepr_pcomon_###VERSION###.tipo$oid_,                                         
                             par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$usr_) ;
                             
PROCEDURE sdel_planxcan_ptos(par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,                             
                            par$aoid_pto_servicios  IN gepr_pcomon_###VERSION###.tipo$oids_,                               
                            par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_);
                            
                            
/*SE par$oid_plan_nueva ES NULL ENTONCES ESTA QUITANDO MAQUINA DE LA PLANIFICACION*/
PROCEDURE sdel_planxptocan(par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,                             
                            par$oid_pto_servicio  IN gepr_pcomon_###VERSION###.tipo$oid_,                               
                            par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_);

/* Grabar terminos por planificación */
PROCEDURE supd_terminoxplan(par$oid_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$aoid_iac_patron IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_termino_patron IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$avalor_termino_patron IN gepr_pcomon_###VERSION###.tipo$descs_,                           
                            par$aoid_iac_dinam IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_termino_dinam IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$avalor_termino_dinam IN gepr_pcomon_###VERSION###.tipo$descs_,
                            par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$deletes             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$updates             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Recuperar MAEs planificadas */
  PROCEDURE srecuperar_maes_planificadas(
                                     par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_pais                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_planificacion        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$acod_device_id           IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$anel_index               IN gepr_pcomon_###VERSION###.tipo$nels_,
                                     par$cod_fecha                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                     par$nel_gmt_minuto           IN gepr_pcomon_###VERSION###.tipo$nel_,
                                     par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_maquinas              OUT sys_refcursor,
                                     par$rc_validaciones          OUT sys_refcursor);
  /* Validar MAEs planificadas */
  PROCEDURE svalidar_maes_plaficadas(
    par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                 IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_planificacion        IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$acod_device_id           IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$anel_index               IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$cod_fecha                IN gepr_pcomon_###VERSION###.tipo$fyh_,
    par$nel_gmt_minuto           IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
    par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_
  );
  /* Función que corrobora si hay error para el procedure de srecuperar_maes_planificadas */
	FUNCTION fhay_error_rec_maes_plani RETURN BOOLEAN;

    /* Validar Fecha */
  PROCEDURE svalidar_fecha(
    par$nel_index            IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$device_id            IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$nel_gmt_minuto       IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$fecha                IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
    par$fecha_gmt0           OUT gepr_pcomon_###VERSION###.tipo$fyh_,
    par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_
  );

  /*Insert/update de mensajes por plan*/
  PROCEDURE supd_planxmensajes(par$oid_planificacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$a_cod_mensaje               IN gepr_pcomon_###VERSION###.tipo$cods_,
                          par$a_des_mensaje               IN gepr_pcomon_###VERSION###.tipo$descs_,
                          par$a_tipo_mensaje              IN gepr_pcomon_###VERSION###.tipo$cods_,
                          par$a_tipop_mensaje              IN gepr_pcomon_###VERSION###.tipo$oids_,
                          par$a_sin_reintentos_mensaje    IN gepr_pcomon_###VERSION###.tipo$nels_,
                          par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$selects                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$inserts                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$deletes                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$updates                 OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /*Insert/update de proceso por plan*/
  PROCEDURE supd_planxproceso(par$oid_planificacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$aoid_proceso               IN gepr_pcomon_###VERSION###.tipo$oids_,
                          par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$inserts                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$deletes                 OUT gepr_pcomon_###VERSION###.tipo$nel_);
 /*Insert/update de divisas por plan*/
  PROCEDURE supd_planxdivisa(
                            par$oid_planxdivisa          IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_divisa         IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$bol_activo          IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_);
  /*Insert/update de movimientos por plan*/
  PROCEDURE supd_planxmovimiento(
                            par$oid_planxmovimiento IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$bol_corte_periodo   IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$bol_activo          IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_);

                            
  /* Verifica los datos del documento para ver si cumple con las condiciones de la planificación */
  PROCEDURE sverifica_documento(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_canal         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_subcanal      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$fyh_gestion       IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_planificacion OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_validaciones  OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$des_validaciones  OUT gepr_pcomon_###VERSION###.tipo$descs_);

END sapr_pplanificacion_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pplanificacion_###VERSION### AS

  /* Busqueda planificacion detalle */
  PROCEDURE sbusqueda_planificacion(par$oid_planificacion       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$info_ejecucion          IN VARCHAR2,
                                    par$ele_rc_planificacion    OUT sys_refcursor,
                                    par$ele_rc_maquinas         OUT sys_refcursor,
                                    par$ele_rc_programaciones   OUT sys_refcursor,
                                    par$ele_rc_canales          OUT sys_refcursor,
                                    par$ele_rc_subcanales       OUT sys_refcursor,
                                    par$aje_rc_codigo_ajeno     OUT sys_refcursor,
                                    par$ele_rc_terminos_patron  OUT sys_refcursor,
                                    par$ele_rc_terminos_dinam   OUT sys_refcursor,
                                    par$ele_rc_mensajes         OUT sys_refcursor,
                                    par$ele_rc_procesos         OUT sys_refcursor,
                                    par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects                 OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$inicio    TIMESTAMP;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_;

    var$selects_aux gepr_pcomon_###VERSION###.tipo$nel_;

    var$err varchar2(4000);

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sbusqueda_planificacion';

    var$oid_cliente gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_patron gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_tipo_planificacion gepr_pcomon_###VERSION###.tipo$cod_; 
    const$tipo_online CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := 'ONLINE';
    const$tipo_fv CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := 'FECHA_VALOR';
    const$par_online CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := 'PlanificacionFVOnlineIAC';
    const$par_fv CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := 'PlanificacionFechaValorIAC';
  BEGIN


    var$inicio    := SYSTIMESTAMP;
    var$registros := 0;
    var$inserts   := 0;
    var$updates   := 0;
    var$deletes   := 0;
    var$selects   := 0;

    BEGIN
        
        SELECT TT.COD_TIPO_PLANIFICACION
        INTO var$cod_tipo_planificacion 
        FROM
            SAPR_TPLANIFICACION PLANIF
            INNER JOIN SAPR_TTIPO_PLANIFICACION TT ON TT.OID_TIPO_PLANIFICACION  = PLANIF.OID_TIPO_PLANIFICACION
       WHERE
        PLANIF.OID_PLANIFICACION = par$oid_planificacion;
        
    END;
    
    IF var$cod_tipo_planificacion = const$tipo_online THEN
        var$cod_patron := const$par_online;
    ELSE
        IF var$cod_tipo_planificacion = const$tipo_fv THEN
            var$cod_patron := const$par_fv;
        END IF;
    END IF;

    OPEN par$ele_rc_planificacion FOR
      SELECT P.OID_PLANIFICACION,
             P.COD_PLANIFICACION,
             P.DES_PLANIFICACION,
             P.COD_TIPO_CONFIRMACION,
             CAST(fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION, P.FYH_VIGENCIA_INICIO) AS DATE) AS FYH_VIGENCIA_INICIO,
             CASE
               WHEN P.FYH_VIGENCIA_FIN IS NULL THEN
                NULL
               ELSE
                CAST(fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION, P.FYH_VIGENCIA_FIN) AS DATE)
             END AS FYH_VIGENCIA_FIN,
             P.BOL_ACTIVO,
             P.GMT_CREACION,
             P.DES_USUARIO_CREACION,
             P.GMT_MODIFICACION,
             P.DES_USUARIO_MODIFICACION,
             C.OID_CLIENTE,
             C.COD_CLIENTE,
             C.DES_CLIENTE,
             TT.OID_TIPO_PLANIFICACION,
             TT.DES_TIPO_PLANIFICACION,
             P.OID_DELEGACION,
             P.NEC_CONTINGENCIA,
             P.BOL_CONTROLA_FACTURACION,            
             B.OID_CLIENTE AS OID_CLIENTE_COMISION,
             B.COD_CLIENTE AS COD_CLIENTE_COMISION,
             B.DES_CLIENTE AS DES_CLIENTE_COMISION,             
             P.NEC_DIA_CIERRE,
             P.NUM_PORCENT_COMISION,
             P.BOL_AGR_SUBCANAL,
       P.BOL_AGR_PTO_SERVICIO,
       P.BOL_AGR_FEC_CONTABLE,
        P.BOL_PERIODO_SUBCANAL,
             P.BOL_PERIODO_DIVISA,
             P.BOL_PERIODO_PTO_SERVICIO
        FROM SAPR_TPLANIFICACION P
       INNER JOIN GEPR_TCLIENTE C
          ON C.OID_CLIENTE = P.OID_CLIENTE
       INNER JOIN SAPR_TTIPO_PLANIFICACION TT
          ON P.OID_TIPO_PLANIFICACION = TT.OID_TIPO_PLANIFICACION
       LEFT JOIN GEPR_TCLIENTE B
          ON P.OID_CLIENTE_COMISION = B.OID_CLIENTE
       WHERE P.OID_PLANIFICACION = par$oid_planificacion;

    var$selects := var$selects + sql%ROWCOUNT;

    OPEN par$ele_rc_programaciones FOR
      SELECT PG.OID_PLANXPROGRAMACION, PG.NEC_DIA_FIN, PG.FYH_HORA_FIN
        FROM SAPR_TPLANXPROGRAMACION PG
       WHERE PG.OID_PLANIFICACION = par$oid_planificacion
       ORDER BY PG.NEC_DIA_FIN, PG.FYH_HORA_FIN;

    var$selects := var$selects + sql%ROWCOUNT;

    OPEN par$ele_rc_canales FOR
      SELECT PC.OID_PLANXCANAL,
             PC.OID_PLANIFICACION,
             PC.OID_CANAL,
             CA.COD_CANAL,
             CA.DES_CANAL,
             PC.BOL_ACTIVO
        FROM SAPR_TPLANXCANAL PC
        INNER JOIN GEPR_TCANAL CA ON CA.OID_CANAL = PC.OID_CANAL
       WHERE PC.BOL_ACTIVO = 1 AND PC.OID_MAQUINA IS NULL AND PC.OID_PTO_SERVICIO IS NULL 
       AND PC.OID_PLANIFICACION = par$oid_planificacion;

    OPEN par$ele_rc_subcanales FOR
      SELECT PS.OID_PLANXCANAL,
             PC.OID_PLANIFICACION,
             PC.OID_CANAL,
             PS.OID_SUBCANAL,
             SC.COD_SUBCANAL,
             SC.DES_SUBCANAL,
             PS.BOL_ACTIVO
        FROM SAPR_TPLANXSUBCANAL PS
        INNER JOIN SAPR_TPLANXCANAL PC ON PS.OID_PLANXCANAL = PC.OID_PLANXCANAL    
        INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = PS.OID_SUBCANAL    
       WHERE PC.BOL_ACTIVO = 1 AND PS.BOL_ACTIVO = 1 AND PC.OID_MAQUINA IS NULL 
       AND PC.OID_PTO_SERVICIO IS NULL AND PC.OID_PLANIFICACION = par$oid_planificacion;
    var$selects := var$selects + sql%ROWCOUNT;

    OPEN par$ele_rc_maquinas FOR
      SELECT M.OID_MAQUINA,
             M.OID_TIPO_MAQUINA,
             M.COD_IDENTIFICACION,
             M.BOL_ACTIVO,
             M.GMT_CREACION,
             M.DES_USUARIO_CREACION,
             M.GMT_MODIFICACION,
             M.DES_USUARIO_MODIFICACION,
             M.OID_SECTOR,
             S.DES_SECTOR,
             S.OID_PLANTA,
             M.OID_MODELO, 
             NVL(CLI.OID_CLIENTE, MAX(CLIT.OID_CLIENTE)) AS OID_CLIENTE,
             NVL(CLI.COD_CLIENTE, MAX(CLIT.COD_CLIENTE)) AS COD_CLIENTE,
             NVL(CLI.DES_CLIENTE,MAX(CLIT.DES_CLIENTE)) AS DES_CLIENTE,
             NVL(SCLI.OID_SUBCLIENTE  , MAX(SCLIT.OID_SUBCLIENTE)) AS OID_SUBCLIENTE,
             NVL(SCLI.COD_SUBCLIENTE, MAX(SCLIT.COD_SUBCLIENTE)) AS COD_SUBCLIENTE,
             NVL(SCLI.DES_SUBCLIENTE,MAX(SCLIT.DES_SUBCLIENTE)) AS DES_SUBCLIENTE,
             NVL(PTO.OID_PTO_SERVICIO, MAX(PTOT.OID_PTO_SERVICIO)) AS OID_PTO_SERVICIO,
             NVL(PTO.COD_PTO_SERVICIO, MAX(PTOT.COD_PTO_SERVICIO)) AS COD_PTO_SERVICIO,
             NVL(PTO.DES_PTO_SERVICIO, MAX(PTOT.DES_PTO_SERVICIO)) AS DES_PTO_SERVICIO,
             PM.NUM_PORCENT_COMISION,   
             PM.OID_SUBCLIENTE_TESORERIA,  
             PM.OID_PTO_SERVICIO_TESORERIA           
        FROM SAPR_TPLANXMAQUINA PM
       INNER JOIN SAPR_TMAQUINA M
          ON PM.OID_MAQUINA = M.OID_MAQUINA
       INNER JOIN GEPR_TSECTOR S
          ON M.OID_SECTOR = S.OID_SECTOR
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = S.OID_PLANTA       
       LEFT JOIN GEPR_TSUBCLIENTE SCLI
          ON SCLI.OID_SUBCLIENTE = PM.OID_SUBCLIENTE_TESORERIA
       LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
          ON PTO.OID_PTO_SERVICIO = PM.OID_PTO_SERVICIO_TESORERIA
       LEFT JOIN GEPR_TCLIENTE CLI
          ON CLI.OID_CLIENTE = SCLI.OID_CLIENTE
       LEFT JOIN gepr_tdelegacionxconfig_factur DEFA
          ON DEFA.OID_DELEGACION = P.OID_DELEGACION 
        LEFT JOIN GEPR_TSUBCLIENTE SCLIT
          ON SCLIT.OID_SUBCLIENTE = DEFA.OID_SUBCLIENTE_TESORERIA
       LEFT JOIN GEPR_TPUNTO_SERVICIO PTOT
          ON PTOT.OID_PTO_SERVICIO = DEFA.OID_PTO_SERVICIO_TESORERIA
            LEFT JOIN GEPR_TCLIENTE CLIT
          ON CLIT.OID_CLIENTE = SCLIT.OID_CLIENTE
       WHERE PM.OID_PLANIFICACION = par$oid_planificacion
         AND PM.BOL_ACTIVO = 1
         GROUP BY  M.OID_MAQUINA,
             M.OID_TIPO_MAQUINA,
             M.COD_IDENTIFICACION,
             M.BOL_ACTIVO,
             M.GMT_CREACION,
             M.DES_USUARIO_CREACION,
             M.GMT_MODIFICACION,
             M.DES_USUARIO_MODIFICACION,
             M.OID_SECTOR,
             S.DES_SECTOR,
             S.OID_PLANTA,
             M.OID_MODELO, 
             CLI.OID_CLIENTE,
             CLI.COD_CLIENTE,
             CLI.DES_CLIENTE,             
             SCLI.OID_SUBCLIENTE,
             SCLI.COD_SUBCLIENTE,
             SCLI.DES_SUBCLIENTE,
             PTO.OID_PTO_SERVICIO,
             PTO.COD_PTO_SERVICIO,
             PTO.DES_PTO_SERVICIO,
             PM.NUM_PORCENT_COMISION,
             PM.OID_SUBCLIENTE_TESORERIA,  
             PM.OID_PTO_SERVICIO_TESORERIA;
          

    par$selects := var$selects + sql%ROWCOUNT;

    OPEN par$ele_rc_terminos_patron FOR
                SELECT 
             VALORES.OID_VALOR_TERMINOXDOCUMENTO,
             VALORES.OID_PLANIFICACION,
             IAC.OID_IAC,
             IAC.COD_IAC,
             IAC.DES_IAC,
             TERMINO.OID_TERMINO,
             TERMINO.COD_TERMINO,
             TERMINO.DES_TERMINO,
             VALORES.DES_VALOR
        FROM
            GEPR_TINFORM_ADICIONAL_CLIENTE IAC 
            INNER JOIN GEPR_TTERMINO_POR_IAC CRUCE ON CRUCE.OID_IAC = IAC.OID_IAC
            INNER JOIN GEPR_TTERMINO TERMINO ON TERMINO.OID_TERMINO = CRUCE.OID_TERMINO
            INNER  JOIN GEPR_TPARAMETRO_VALOR PARVAL ON PARVAL.DES_VALOR_PARAMETRO = IAC.COD_IAC /* Si no coincide serian dinamicos */
            INNER JOIN GEPR_TPARAMETRO PARAM ON PARAM.OID_PARAMETRO = PARVAL.OID_PARAMETRO
            LEFT JOIN   SAPR_TVALOR_TERMINOXPLAN  VALORES ON IAC.OID_IAC = VALORES.OID_IAC and VALORES.OID_TERMINO = TERMINO.OID_TERMINO
            AND VALORES.OID_PLANIFICACION = par$oid_planificacion 
            WHERE
                PARAM.COD_PARAMETRO = var$cod_patron;
            var$selects := var$selects + sql%ROWCOUNT;

    OPEN par$ele_rc_terminos_dinam FOR
        SELECT 
             VALORES.OID_VALOR_TERMINOXDOCUMENTO,
             VALORES.OID_PLANIFICACION,
             IAC.OID_IAC,
             IAC.COD_IAC,
             IAC.DES_IAC,
             TERMINO.OID_TERMINO,
             TERMINO.COD_TERMINO,
             TERMINO.DES_TERMINO,
             VALORES.DES_VALOR,
             var$cod_patron COD_PATRON,
             PARVAL.DES_VALOR_PARAMETRO             
        FROM
            GEPR_TINFORM_ADICIONAL_CLIENTE IAC 
            INNER JOIN GEPR_TTERMINO_POR_IAC CRUCE ON CRUCE.OID_IAC = IAC.OID_IAC
            INNER JOIN GEPR_TTERMINO TERMINO ON TERMINO.OID_TERMINO = CRUCE.OID_TERMINO
            INNER  JOIN GEPR_TPARAMETRO_VALOR PARVAL ON PARVAL.DES_VALOR_PARAMETRO <> IAC.COD_IAC  /* Si no coincide serian dinamicos */
            INNER JOIN GEPR_TPARAMETRO PARAM ON PARAM.OID_PARAMETRO = PARVAL.OID_PARAMETRO
            LEFT JOIN   SAPR_TVALOR_TERMINOXPLAN  VALORES ON IAC.OID_IAC = VALORES.OID_IAC and VALORES.OID_TERMINO = TERMINO.OID_TERMINO
            AND VALORES.OID_PLANIFICACION = par$oid_planificacion
            WHERE
                 (VALORES.DES_VALOR is not null or VALORES.DES_VALOR <> '')
                 and PARAM.COD_PARAMETRO = var$cod_patron;
        var$selects := var$selects + sql%ROWCOUNT;

    OPEN par$ele_rc_mensajes FOR
        SELECT
            PXM.COD_MENSAJE,
            PXM.DES_MENSAJE,
            PXM.COD_TIPO_MENSAJE,
            PXM.BOL_SIN_REINTENTOS,
            PXM.OID_TIPO_PERIODO,
            TPER.DES_TIPO_PERIODO
        FROM SAPR_TPLANXMENSAJE PXM
        INNER JOIN SAPR_TTIPO_PERIODO TPER ON TPER.OID_TIPO_PERIODO = PXM.OID_TIPO_PERIODO
        WHERE
            PXM.OID_PLANIFICACION = par$oid_planificacion
        ;
        var$selects := var$selects + sql%ROWCOUNT;

    OPEN par$ele_rc_procesos FOR
        SELECT
            PXP.OID_PROCESO
        FROM SAPR_TPROCESOXPLANIFICACION PXP
        WHERE
            PXP.OID_PLANIFICACION = par$oid_planificacion
        ;
        var$selects := var$selects + sql%ROWCOUNT;

    BEGIN
      SELECT P.OID_CLIENTE
        INTO var$oid_cliente
        FROM SAPR_TPLANIFICACION P
       WHERE P.OID_PLANIFICACION = par$oid_planificacion;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_cliente := NULL;
    END;

    par$selects := var$selects + sql%ROWCOUNT;

    IF var$oid_cliente  IS NOT NULL THEN
      GEPR_PCODIGO_AJENO_###VERSION###.srecuperar_codigo_ajeno(par$cod_tipo_tabla_genesis => 'GEPR_TCLIENTE',
                                                          par$oid_tabla_genesis      => var$oid_cliente,
                                                          par$cod_identificador      => par$cod_identificador_ajeno,
                                                          par$bol_activo             => null,
                                                          par$info_ejecucion         => par$info_ejecucion,
                                                          par$aje_rc_codigo_ajeno    => par$aje_rc_codigo_ajeno,
                                                          par$cod_ejecucion          => par$cod_ejecucion,
                                                          par$cod_usuario            => par$cod_usuario,
                                                          par$selects                => var$selects_aux);

      par$selects := var$selects_aux + sql%ROWCOUNT;
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

  END sbusqueda_planificacion;

    /* Recuperar planificacion servicio */
  PROCEDURE srecuperar_planificacion(par$cods_planificacion      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$cod_tipo                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_banco               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_vigente             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$bol_recuperar_maquinas  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                     par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_planificacion        OUT sys_refcursor,
                                     par$rc_maquinas             OUT sys_refcursor,
                                     par$rc_programaciones       OUT sys_refcursor,
                                     par$rc_canales              OUT sys_refcursor,
                                     par$validaciones            OUT sys_refcursor,
                                     par$rc_limites               OUT sys_refcursor,
                                     par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_planificacion';



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
    var$oid_banco     gepr_pcomon_###VERSION###.tipo$oid_;
    var$validaciones  gepr_pcomon_###VERSION###.tipo$descs_;
    var$tvalidaciones varchar2_array;
    var$count INTEGER;
  BEGIN

    OPEN par$rc_planificacion FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_maquinas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_programaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_canales FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_limites FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    IF par$cods_planificacion.COUNT = 0 AND par$cod_banco IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true);
    END IF;

    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TOIDS;
    var$deletes   := var$deletes + sql%ROWCOUNT;

    /* Validar codigo ajeno - Recuperar identificador banco */
    IF par$cod_banco IS NOT NULL THEN

      IF par$cod_identificador_ajeno IS NULL THEN

        BEGIN
          SELECT C.OID_CLIENTE
            INTO var$oid_banco
            FROM GEPR_TCLIENTE C
           WHERE C.COD_CLIENTE = par$cod_banco;
           var$selects := var$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN

            var$oid_banco := NULL;
            var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_banco',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            par$cod_banco,
                                                                                            1);
        END;

      ELSE

        BEGIN
          SELECT CA.OID_TABLA_GENESIS
            INTO var$oid_banco
            FROM GEPR_TCODIGO_AJENO CA
           WHERE CA.BOL_ACTIVO = 1
             AND CA.COD_AJENO = par$cod_banco
             AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
             AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE';
           var$selects := var$selects + sql%ROWCOUNT;

        EXCEPTION
          WHEN no_data_found THEN
            var$oid_banco := NULL;
            var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'validar_codigo_ajeno',
                                                                                            'GENERICO',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                            par$cod_banco || '|' || par$cod_identificador_ajeno || '|' ||'GEPR_TCLIENTE',
                                                                                            1);
        END;

      END IF;
    END IF;
    IF gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN
      /* Cargar tabla temporaria con OID */
      IF par$cods_planificacion.COUNT > 0 THEN
         FOR idx IN par$cods_planificacion.first .. par$cods_planificacion.last    LOOP
              IF par$cods_planificacion(idx)is not NULL THEN
                 INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
                 VALUES (par$cods_planificacion(idx), 'COD_PLANIFICACION');
                 var$inserts := var$inserts + par$cods_planificacion.COUNT;
              END IF;
           END LOOP;

             INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
              SELECT P.OID_PLANIFICACION, 'OID_PLANIFICACION'
                FROM SAPR_TPLANIFICACION P
               INNER JOIN SAPR_TTIPO_PLANIFICACION TP
                  ON P.OID_TIPO_PLANIFICACION = TP.OID_TIPO_PLANIFICACION
               INNER JOIN SAPR_GTT_TOIDS T
                  ON T.OID = P.COD_PLANIFICACION
                 AND T.COD_CALIFICADOR = 'COD_PLANIFICACION'
               WHERE (par$cod_vigente = '2' OR (P.BOL_ACTIVO = par$cod_vigente))
                 AND (var$oid_banco IS NULL OR
                     (P.OID_CLIENTE = NVL(var$oid_banco, '###')))
                 AND (par$cod_tipo IS NULL OR
                     (NVL(TP.COD_TIPO_PLANIFICACION, '###') =
                      NVL(par$cod_tipo, '###')));
             var$selects := var$selects + sql%ROWCOUNT;
             var$inserts := var$inserts + sql%ROWCOUNT;

    END IF;

    SELECT COUNT(1) into var$count from SAPR_GTT_TOIDS t where COD_CALIFICADOR = 'OID_PLANIFICACION';
    IF var$count = 0 THEN

         INSERT INTO SAPR_GTT_TOIDS(OID, COD_CALIFICADOR)
          SELECT P.OID_PLANIFICACION, 'OID_PLANIFICACION'
            FROM SAPR_TPLANIFICACION P
           INNER JOIN SAPR_TTIPO_PLANIFICACION TP
              ON P.OID_TIPO_PLANIFICACION = TP.OID_TIPO_PLANIFICACION
           WHERE  (par$cod_vigente = '2' OR (P.BOL_ACTIVO = par$cod_vigente))
             AND P.OID_CLIENTE = var$oid_banco
             AND (par$cod_tipo IS NULL OR
                 (NVL(TP.COD_TIPO_PLANIFICACION, '###') =
                  NVL(par$cod_tipo, '###')));
         var$selects := var$selects + sql%ROWCOUNT;
         var$inserts := var$inserts + sql%ROWCOUNT;

      END IF;

      /* Programaciones */
      OPEN par$rc_programaciones FOR
      SELECT PG.OID_PLANIFICACION,
               PG.NEC_DIA_INICIO,
               PG.FYH_HORA_INICIO,
               PG.NEC_DIA_FIN,
               PG.FYH_HORA_FIN
          FROM SAPR_TPLANXPROGRAMACION PG
         INNER JOIN SAPR_GTT_TOIDS T
            ON T.OID = PG.OID_PLANIFICACION
           AND T.COD_CALIFICADOR = 'OID_PLANIFICACION'
         GROUP BY PG.OID_PLANIFICACION,
                  PG.NEC_DIA_INICIO,
                  PG.FYH_HORA_INICIO,
                  PG.NEC_DIA_FIN,
                  PG.FYH_HORA_FIN
         ORDER BY PG.NEC_DIA_FIN, PG.FYH_HORA_FIN;
      var$selects := var$selects + sql%ROWCOUNT;

      IF par$cod_identificador_ajeno IS NULL THEN

          /* Planificaciones */
          OPEN par$rc_planificacion FOR
            SELECT P.OID_PLANIFICACION,
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
             INNER JOIN SAPR_GTT_TOIDS T
                ON T.OID = P.OID_PLANIFICACION
               AND T.COD_CALIFICADOR = 'OID_PLANIFICACION'
             GROUP BY P.OID_PLANIFICACION,
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
          var$selects := var$selects + sql%ROWCOUNT;

          /* Canales */
          OPEN par$rc_canales FOR
            SELECT PC.OID_PLANIFICACION,
                   C.COD_CANAL,
                   C.DES_CANAL,
                   PC.BOL_ACTIVO
              FROM SAPR_TPLANXCANAL PC
             INNER JOIN SAPR_GTT_TOIDS T
                ON T.OID = PC.OID_PLANIFICACION
               AND T.COD_CALIFICADOR = 'OID_PLANIFICACION'
             INNER JOIN GEPR_TCANAL C
                ON C.OID_CANAL = PC.OID_CANAL
             WHERE PC.BOL_ACTIVO = 1;
          var$selects := var$selects + sql%ROWCOUNT;

          /* Maquinas */
          IF par$bol_recuperar_maquinas = 1 THEN

            OPEN par$rc_maquinas FOR
                SELECT PM.OID_PLANIFICACION,
                       M.OID_MAQUINA,
                       M.COD_IDENTIFICACION,
                       M.BOL_ACTIVO,
                       CLI.COD_CLIENTE,
                       CLI.DES_CLIENTE,
                       SCLI.COD_SUBCLIENTE,
                       SCLI.DES_SUBCLIENTE,
                       PTO.COD_PTO_SERVICIO,
                       PTO.DES_PTO_SERVICIO
                  FROM SAPR_TPLANXMAQUINA PM
                 INNER JOIN SAPR_TMAQUINA M
                    ON PM.OID_MAQUINA = M.OID_MAQUINA
                 INNER JOIN GEPR_TPUNTO_SERVICIO PTO
                    ON PTO.OID_MAQUINA = M.OID_MAQUINA
                 INNER JOIN GEPR_TSUBCLIENTE SCLI
                    ON SCLI.OID_SUBCLIENTE = PTO.OID_SUBCLIENTE
                 INNER JOIN GEPR_TCLIENTE CLI
                    ON CLI.OID_CLIENTE = SCLI.OID_CLIENTE
                 INNER JOIN SAPR_GTT_TOIDS T
                    ON T.OID = PM.OID_PLANIFICACION
                   AND T.COD_CALIFICADOR = 'OID_PLANIFICACION'
                 WHERE PM.BOL_ACTIVO = 1;
              var$selects := var$selects + sql%ROWCOUNT;

          END IF;

          OPEN par$rc_limites FOR
            SELECT
              LIMITE.NUM_LIMITE VALOR,
              MAQUINA.COD_IDENTIFICACION DEVICEID,
              SECTOR.DES_SECTOR DES_SECTOR,
              DIVISA.COD_ISO_DIVISA COD_ISO_DIVISA,
              DIVISA.DES_DIVISA DES_DIVISA,
              PTOSER.COD_PTO_SERVICIO COD_PTO_SERVICIO,
              PTOSER.DES_PTO_SERVICIO DES_PTO_SERVICIO,
              LIMITE.OID_PLANIFICACION OID_PLANIFICACION
            FROM SAPR_TLIMITE LIMITE
              INNER JOIN SAPR_GTT_TOIDS PLANIF ON PLANIF.OID = LIMITE.OID_PLANIFICACION AND PLANIF.COD_CALIFICADOR = 'OID_PLANIFICACION'
              INNER JOIN GEPR_TDIVISA DIVISA ON DIVISA.OID_DIVISA = LIMITE.OID_DIVISA
              LEFT JOIN GEPR_TPUNTO_SERVICIO PTOSER ON PTOSER.OID_PTO_SERVICIO = LIMITE.OID_PTO_SERVICIO
              LEFT JOIN SAPR_TMAQUINA MAQUINA ON MAQUINA.OID_MAQUINA = LIMITE.OID_MAQUINA
              LEFT JOIN GEPR_TSECTOR SECTOR ON SECTOR.OID_SECTOR = MAQUINA.OID_SECTOR
            WHERE
              LIMITE.BOL_ACTIVO = 1;

          

      ELSE

          /* Planificaciones */
          OPEN par$rc_planificacion FOR
            SELECT P.OID_PLANIFICACION,
                   P.COD_PLANIFICACION,
                   P.DES_PLANIFICACION,
                   CAST(fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION, P.FYH_VIGENCIA_INICIO) AS DATE) AS FYH_VIGENCIA_INICIO,
                   CASE
                     WHEN P.FYH_VIGENCIA_FIN IS NULL THEN
                      NULL
                     ELSE
                      CAST(fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION, P.FYH_VIGENCIA_FIN) AS DATE)
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
             INNER JOIN GEPR_TCODIGO_AJENO CAC
                ON CAC.OID_TABLA_GENESIS = P.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
             INNER JOIN GEPR_TCODIGO_AJENO CAD
                ON CAD.OID_TABLA_GENESIS = P.OID_DELEGACION AND CAD.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAD.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
             INNER JOIN SAPR_GTT_TOIDS T
                ON T.OID = P.OID_PLANIFICACION AND T.COD_CALIFICADOR = 'OID_PLANIFICACION'
             GROUP BY P.OID_PLANIFICACION,
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
          var$selects := var$selects + sql%ROWCOUNT;

          /* Canales */
          OPEN par$rc_canales FOR
            SELECT PC.OID_PLANIFICACION,
                   CAC.COD_AJENO COD_CANAL,
                   CAC.DES_AJENO DES_CANAL
              FROM SAPR_TPLANXCANAL PC
             INNER JOIN SAPR_GTT_TOIDS T
                ON T.OID = PC.OID_PLANIFICACION
               AND T.COD_CALIFICADOR = 'OID_PLANIFICACION'
             INNER JOIN GEPR_TCODIGO_AJENO CAC
                ON CAC.OID_TABLA_GENESIS = PC.OID_CANAL AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
             WHERE PC.BOL_ACTIVO = 1;
          var$selects := var$selects + sql%ROWCOUNT;

          /* Maquinas */
          IF par$bol_recuperar_maquinas = 1 THEN

            OPEN par$rc_maquinas FOR
                SELECT PM.OID_PLANIFICACION,
                       M.OID_MAQUINA,
                       M.COD_IDENTIFICACION,
                       M.BOL_ACTIVO,
                       CAPS.COD_AJENO COD_PTO_SERVICIO,
                       CAPS.DES_AJENO DES_PTO_SERVICIO,
                       CASC.COD_AJENO COD_SUBCLIENTE,
                       CASC.DES_AJENO DES_SUBCLIENTE,
                       CAC.COD_AJENO COD_CLIENTE,
                       CAC.DES_AJENO DES_CLIENTE
                  FROM SAPR_TPLANXMAQUINA PM
                 INNER JOIN SAPR_TMAQUINA M
                    ON PM.OID_MAQUINA = M.OID_MAQUINA
                 INNER JOIN GEPR_TPUNTO_SERVICIO PTO
                    ON PTO.OID_MAQUINA = M.OID_MAQUINA
                 INNER JOIN GEPR_TSUBCLIENTE SCLI
                    ON SCLI.OID_SUBCLIENTE = PTO.OID_SUBCLIENTE
                 INNER JOIN GEPR_TCODIGO_AJENO CAPS
                    ON CAPS.OID_TABLA_GENESIS = PTO.OID_PTO_SERVICIO AND CAPS.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAPS.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                 INNER JOIN GEPR_TCODIGO_AJENO CASC
                    ON CASC.OID_TABLA_GENESIS = SCLI.OID_SUBCLIENTE AND CASC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CASC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                 INNER JOIN GEPR_TCODIGO_AJENO CAC
                    ON CAC.OID_TABLA_GENESIS = SCLI.OID_CLIENTE AND CAC.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CAC.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                 INNER JOIN SAPR_GTT_TOIDS T
                    ON T.OID = PM.OID_PLANIFICACION
                   AND T.COD_CALIFICADOR = 'OID_PLANIFICACION'
                 WHERE PM.BOL_ACTIVO = 1;
              var$selects := var$selects + sql%ROWCOUNT;

          END IF;

         OPEN par$rc_limites FOR
            SELECT
              LIMITE.NUM_LIMITE VALOR,
              CAMAQUINA.COD_AJENO DEVICEID,
              CASECTOR.DES_AJENO DES_SECTOR,
              CADIVISA.COD_AJENO COD_ISO_DIVISA,
              CADIVISA.DES_AJENO DES_DIVISA,
              PTOSER.COD_PTO_SERVICIO COD_PTO_SERVICIO,
              PTOSER.DES_PTO_SERVICIO DES_PTO_SERVICIO,
              LIMITE.OID_PLANIFICACION OID_PLANIFICACION
            FROM SAPR_TLIMITE LIMITE
              INNER JOIN SAPR_GTT_TOIDS PLANIF ON PLANIF.OID = LIMITE.OID_PLANIFICACION AND PLANIF.COD_CALIFICADOR = 'OID_PLANIFICACION'
              INNER JOIN GEPR_TDIVISA DIVISA ON DIVISA.OID_DIVISA = LIMITE.OID_DIVISA
              INNER JOIN GEPR_TCODIGO_AJENO CADIVISA ON CADIVISA.OID_TABLA_GENESIS = DIVISA.OID_DIVISA AND CADIVISA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'
              LEFT JOIN GEPR_TPUNTO_SERVICIO PTOSER ON PTOSER.OID_PTO_SERVICIO = LIMITE.OID_PTO_SERVICIO
              LEFT JOIN GEPR_TCODIGO_AJENO CAPTOSER ON CAPTOSER.OID_TABLA_GENESIS = PTOSER.OID_PTO_SERVICIO AND CAPTOSER.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
              LEFT JOIN SAPR_TMAQUINA MAQUINA ON MAQUINA.OID_MAQUINA = LIMITE.OID_MAQUINA
              LEFT JOIN GEPR_TCODIGO_AJENO CAMAQUINA ON CAMAQUINA.OID_TABLA_GENESIS = MAQUINA.OID_MAQUINA AND CAMAQUINA.COD_TIPO_TABLA_GENESIS = 'SAPR_TMAQUINA'
              LEFT JOIN GEPR_TSECTOR SECTOR ON SECTOR.OID_SECTOR = MAQUINA.OID_SECTOR
              LEFT JOIN GEPR_TCODIGO_AJENO CASECTOR ON CASECTOR.OID_TABLA_GENESIS = SECTOR.OID_SECTOR AND CASECTOR.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'
            WHERE
              LIMITE.BOL_ACTIVO = 1;


      END IF;

    END IF;

    /* Validaciones */
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN

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

  END srecuperar_planificacion;


   /* Grabar planificacion */
  PROCEDURE sgrabar_planificacion(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_planificacion       IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_planificacion       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$des_planificacion       IN gepr_pcomon_###VERSION###.tipo$desc_,
                                  par$oid_tipo_planificacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_tipo_confirmacion   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_cliente             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_delegacion          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$fyh_vigencia_inicio     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$fyh_vigencia_fin        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$bol_activo              IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$aprog_num_dia_fin       IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$aprog_fyh_fechahora_fin IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                  par$amaq_oid_maquina        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$acan_oid_canal          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$ascan_oid_subcanal      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$nec_contigencia         IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$bol_cambio_horario      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_cambio_program      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$aoid_cod_ajeno           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$aoid_tabla_genesis       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$acod_identificador       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$acod_ajeno               IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$ades_ajeno               IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$abol_defecto             IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$bol_controlafacturacion IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$oid_banco_comision      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$por_comision_plan       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                  par$dia_cierre_facturacion  IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$info_ejecucion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                  par$bol_divide_subcanal   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_divide_divisa   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_agrupa_subcanal   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_agrupa_puntoservicio IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$bol_agrupa_fechacontable IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                  par$aoid_iac_patron          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$aoid_termino_patron      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$avalor_termino_patron    IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$aoid_iac_dinam           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$aoid_termino_dinam       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$avalor_termino_dinam     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$a_lim_cod_divisa         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_lim_num                IN gepr_pcomon_###VERSION###.tipo$imps_,
                                  par$a_lim_bol_baja           IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_per_div_codigo     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_per_div_baja       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_per_mov_codigo     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$a_per_mov_baja       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$a_cod_mensaje            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_des_mensaje            IN gepr_pcomon_###VERSION###.tipo$descs_,
                                  par$a_tipo_mensaje            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$a_tipop_mensaje            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$a_sin_reintentos_mensaje  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$aoid_proceso            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$des_origen CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sgrabar_planificacion';

    /* variables locales */
    var$inicio      gepr_pcomon_###VERSION###.tipo$gmt_ := SYSTIMESTAMP;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$_fyh_ultima_planificacion gepr_pcomon_###VERSION###.tipo$fyh_;
    var$err         VARCHAR2(4000);
    var$anel_null gepr_pcomon_###VERSION###.tipo$nels_;
    var$aoid_null gepr_pcomon_###VERSION###.tipo$oids_;
    var$tmp_oid_cod_ajeno gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_limite        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planxmovimiento gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planxdivisa gepr_pcomon_###VERSION###.tipo$oid_;
    var$hayerror          gepr_pcomon_###VERSION###.tipo$nel_;
    var$es_fvc            gepr_pcomon_###VERSION###.tipo$nel_;

  BEGIN
    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TAUXILIAR;

    /*raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
    'PAROU');*/
    dbms_output.put_line('TRACE 1');
    IF par$oid_planificacion IS NULL THEN
      par$oid_planificacion := sys_guid();
    END IF;
    dbms_output.put_line(par$oid_planificacion);
    BEGIN
        SELECT  FYH_VIGENCIA_FIN
        INTO    var$_fyh_ultima_planificacion
        FROM    SAPR_TPLANIFICACION
        WHERE   OID_PLANIFICACION = par$oid_planificacion ;
      EXCEPTION
        WHEN no_data_found THEN
             var$_fyh_ultima_planificacion := null;
      END;

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Inicia procedure svalidar_planificacion',
				par$cod_identificador   => par$cod_planificacion);

    /* Validar planificacion */
    svalidar_planificacion(par$cod_planificacion       => par$cod_planificacion,
                           par$des_planificacion       => par$des_planificacion,
                           par$oid_tipo_planificacion  => par$oid_tipo_planificacion,
                           par$oid_cliente             => par$oid_cliente,
                           par$oid_delegacion          => par$oid_delegacion,
                           par$fyh_vigencia_inicio     => par$fyh_vigencia_inicio,
                           par$aprog_num_dia_fin       => par$aprog_num_dia_fin,
                           par$aprog_fyh_fechahora_fin => par$aprog_fyh_fechahora_fin,
                           par$cod_usuario             => par$cod_usuario,
                           par$bol_activo              => par$bol_activo,
                           par$cod_cultura             => par$cod_cultura);

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure svalidar_planificacion',
				par$cod_identificador   => par$cod_planificacion);


    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Inicia procedure supd_planificacion',
				par$cod_identificador   => par$cod_planificacion);
    /* Insert/Update Planificacion */
    supd_planificacion(par$oid_planificacion       => par$oid_planificacion,
                       par$cod_planificacion       => par$cod_planificacion,
                       par$des_planificacion       => par$des_planificacion,
                       par$oid_tipo_planificacion  => par$oid_tipo_planificacion,
                       par$cod_tipo_confirmacion   =>  par$cod_tipo_confirmacion,
                       par$oid_cliente             => par$oid_cliente,
                       par$oid_delegacion          => par$oid_delegacion,
                       par$fyh_vigencia_inicio     => par$fyh_vigencia_inicio,
                       par$fyh_vigencia_fin        => par$fyh_vigencia_fin,
                       par$bol_activo              => par$bol_activo,
                       par$cod_usuario             => par$cod_usuario,
                       par$nec_contigencia         => par$nec_contigencia,
                       par$selects                 => var$selects_int,
                       par$inserts                 => var$inserts_int,
                       par$updates                 => var$updates_int,
                       par$bol_controlafacturacion => par$bol_controlafacturacion,
                       par$oid_banco_comision      => par$oid_banco_comision,
                       par$por_comision_plan       => par$por_comision_plan,
                       par$dia_cierre_facturacion  => par$dia_cierre_facturacion,
                       par$bol_divide_subcanal     => par$bol_divide_subcanal,
                       par$bol_divide_divisa       => par$bol_divide_divisa,
                       par$bol_agrupa_subcanal     => par$bol_agrupa_subcanal,
                       par$bol_agrupa_puntoservicio => par$bol_agrupa_puntoservicio,
                       par$bol_agrupa_fechacontable => par$bol_agrupa_fechacontable);

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure supd_planificacion',
				par$cod_identificador   => par$cod_planificacion);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$selects   := var$selects + var$selects_int;


    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Inicia procedure sins_programacion',
				par$cod_identificador   => par$cod_planificacion);

    /* Insert Programacion */
    sins_programacion(par$oid_planificacion       => par$oid_planificacion,
                      par$aprog_num_dia_fin       => par$aprog_num_dia_fin,
                      par$aprog_fyh_fechahora_fin => par$aprog_fyh_fechahora_fin,
                      par$cod_usuario             => par$cod_usuario,
                      par$deletes                 => var$deletes_int,
                      par$inserts                 => var$inserts_int);

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure sins_programacion',
				par$cod_identificador   => par$cod_planificacion);

    var$registros := var$registros + var$inserts_int + var$deletes_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$deletes   := var$deletes + var$deletes_int;
    
     

    /*Verifico si la planificacion es de FVC*/
    SELECT COUNT(1) INTO var$es_fvc FROM SAPR_TPLANIFICACION PLANI
    INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION
    INNER JOIN SAPR_TPROCESOXPLANIFICACION PXP ON PXP.OID_PLANIFICACION = PLANI.OID_PLANIFICACION
    INNER JOIN SAPR_TPROCESO PRO ON PRO.OID_PROCESO = PXP.OID_PROCESO
    WHERE PLANI.OID_PLANIFICACION = par$oid_planificacion AND TPLANI.COD_TIPO_PLANIFICACION = 'FECHA_VALOR_CONFIR'
    AND PRO.COD_PROCESO = 'BBVA_PERU';
    
    IF var$es_fvc = 1 THEN
      SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia procedure supd_planxmaquinas_fvc',
          par$cod_identificador   => par$cod_planificacion);

       /* Insert/Update PlanXMaquinas */
      supd_planxmaquinas_fvc(par$oid_llamada => par$oid_llamada, 
                       par$oid_planificacion   => par$oid_planificacion,
                       par$fyh_vigencia_inicio => par$fyh_vigencia_inicio,
                       par$fyh_vigencia_fin    => par$fyh_vigencia_fin,
                       par$amaq_oid_maquina    => par$amaq_oid_maquina,
                       par$cod_usuario         => par$cod_usuario,
                       par$bol_cambio_horario  => par$bol_cambio_horario,
                       par$bol_cambio_program  => par$bol_cambio_program,
                       par$fyh_vigencia_fin_old => var$_fyh_ultima_planificacion,
                       par$selects             => var$selects_int,
                       par$inserts             => var$inserts_int,
                       par$deletes             => var$deletes_int,
                       par$updates             => var$updates_int);

      SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure supd_planxmaquinas_fvc',
				par$cod_identificador   => par$cod_planificacion);
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia procedure supd_planxmaquinas',
          par$cod_identificador   => par$cod_planificacion);
          
      /* Insert/Update PlanXMaquinas */
      supd_planxmaquinas(par$oid_planificacion   => par$oid_planificacion,
                       par$fyh_vigencia_inicio => par$fyh_vigencia_inicio,
                       par$fyh_vigencia_fin    => par$fyh_vigencia_fin,
                       par$amaq_oid_maquina    => par$amaq_oid_maquina,
                       par$cod_usuario         => par$cod_usuario,
                       par$bol_cambio_horario  => par$bol_cambio_horario,
                       par$bol_cambio_program  => par$bol_cambio_program,
                       par$fyh_vigencia_fin_old => var$_fyh_ultima_planificacion,
                       par$selects             => var$selects_int,
                       par$inserts             => var$inserts_int,
                       par$deletes             => var$deletes_int,
                       par$updates             => var$updates_int);

      SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure supd_planxmaquinas',
				par$cod_identificador   => par$cod_planificacion);
    END IF;
    
                       
    SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia procedure svalidar_planxcan_maq',
          par$cod_identificador   => par$cod_planificacion);

    svalidar_planxcan_maq(par$oid_planificacion   => par$oid_planificacion,
     par$amaq_oid_maquina    => par$amaq_oid_maquina,
       par$cod_usuario         => par$cod_usuario);

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure svalidar_planxcan_maq',
				par$cod_identificador   => par$cod_planificacion);
                                                
    var$registros := var$registros + var$inserts_int + var$deletes_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$updates   := var$updates + var$updates_int;

    SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia procedure supd_planxcanales',
          par$cod_identificador   => par$cod_planificacion);
    /* Insert/Update PlanXCanal */
    supd_planxcanales(par$oid_planificacion => par$oid_planificacion,
                      par$oid_maquina  => null,
                      par$apto_nel_index  => var$anel_null,
                      par$apto_oid_pto_servicio  => var$aoid_null,
                      par$acan_nel_index  => var$anel_null,
                      par$acan_oid_canal    => par$acan_oid_canal,
                      par$cod_usuario       => par$cod_usuario,
                      par$selects           => var$selects_int,
                      par$inserts           => var$inserts_int,
                      par$updates           => var$updates_int);

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure supd_planxcanales',
				par$cod_identificador   => par$cod_planificacion);
  
    SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia procedure supd_planxsubcanales',
          par$cod_identificador   => par$cod_planificacion);                      
          
    /* Insert/Update PlanXSubCanal */       
    supd_planxsubcanales(par$oid_planificacion => par$oid_planificacion,
                        par$oid_maquina        => null,                        
                        par$apto_nel_index  => var$anel_null,
                        par$apto_oid_pto_servicio  => var$aoid_null,
                        par$ascan_nel_index  => var$anel_null,
                        par$ascan_oid_subcanal => par$ascan_oid_subcanal,
                        par$cod_usuario        => par$cod_usuario,
                        par$selects            => var$selects_int,
                        par$inserts            => var$inserts_int,
                        par$updates            => var$updates_int);

    /*par$ascab_oid_subcanal*/
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$selects   := var$selects + var$selects_int;

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$des_origen,
      par$des_version     => const$version,
      par$des_detalle     => 'Finaliza procedure supd_planxsubcanales',
      par$cod_identificador   => par$cod_planificacion);  

    IF par$acan_oid_canal is not null AND par$acan_oid_canal.COUNT > 1  THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia carga de códigos ajenos',
          par$cod_identificador   => par$cod_planificacion);      

        FOR idx IN par$acod_ajeno.first .. par$acod_ajeno.last LOOP
              if  par$acod_ajeno(idx) is not null and par$ades_ajeno(idx) is not null and 
              par$aoid_tabla_genesis(idx) is not null and 
              par$acod_identificador(idx) is not null  then
              var$tmp_oid_cod_ajeno := par$aoid_cod_ajeno(idx);
              gepr_pcodigo_ajeno_###VERSION###.sgrabar_codigo_ajeno(par$oid_cod_ajeno     => var$tmp_oid_cod_ajeno,
                                                              par$oid_tabla_genesis => par$aoid_tabla_genesis(idx),
                                                              par$cod_tabla_genesis => 'GEPR_TCLIENTE',
                                                              par$cod_identificador => par$acod_identificador(idx),
                                                              par$cod_ajeno         => par$acod_ajeno(idx),
                                                              par$des_ajeno         => par$ades_ajeno(idx),
                                                              par$bol_defecto       => par$abol_defecto(idx),
                                                              par$oid_delegacion    => par$oid_delegacion,
                                                              par$cod_usuario       => par$cod_usuario,
                                                              par$inserts           => var$inserts_int,
                                                              par$updates           => var$updates_int);

              var$registros := var$registros + var$inserts_int + var$updates_int;
              var$inserts   := var$inserts + var$inserts_int;
              var$updates   := var$updates + var$updates_int;
            end if;
        END LOOP;
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Finaliza carga de códigos ajenos',
          par$cod_identificador   => par$cod_planificacion);  
    end if;

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$des_origen,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicia procedure supd_terminoxplan',
      par$cod_identificador   => par$cod_planificacion);     
    /* Insert/Update TerminoXPlan */
    supd_terminoxplan(par$oid_planificacion,
                            par$aoid_iac_patron,
                            par$aoid_termino_patron,
                            par$avalor_termino_patron,                           
                            par$aoid_iac_dinam,
                            par$aoid_termino_dinam,
                            par$avalor_termino_dinam,
                            par$cod_usuario,
                            var$selects_int,
                            var$inserts_int,
                            var$deletes_int,
                            var$updates_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$updates   := var$updates + var$updates_int;
      var$selects   := var$selects + var$selects_int;
      var$deletes   := var$deletes + var$deletes_int;

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure supd_terminoxplan',
				par$cod_identificador   => par$cod_planificacion);  

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$des_origen,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicia procedure supd_planxmensajes',
      par$cod_identificador   => par$cod_planificacion);     
    /* Insert/Update PlanXMensaje*/
    supd_planxmensajes(par$oid_planificacion,
                            par$a_cod_mensaje,
                            par$a_des_mensaje,
                            par$a_tipo_mensaje,
                            par$a_tipop_mensaje,
                            par$a_sin_reintentos_mensaje,
                            par$cod_usuario,
                            var$selects_int,
                            var$inserts_int,
                            var$deletes_int,
                            var$updates_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$updates   := var$updates + var$updates_int;
      var$selects   := var$selects + var$selects_int;
      var$deletes   := var$deletes + var$deletes_int;

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure supd_planxmensajes',
				par$cod_identificador   => par$cod_planificacion);  

    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$des_origen,
      par$des_version     => const$version,
      par$des_detalle     => 'Inicia procedure supd_planxproceso',
      par$cod_identificador   => par$cod_planificacion);    

    /* Insert/Update PlanXProceso*/
    supd_planxproceso(par$oid_planificacion,
                            par$aoid_proceso,
                            par$cod_usuario,
                            var$inserts_int,
                            var$deletes_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$deletes   := var$deletes + var$deletes_int;

    SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$des_origen,
				par$des_version     => const$version,
				par$des_detalle     => 'Finaliza procedure supd_planxproceso',
				par$cod_identificador   => par$cod_planificacion);  

    /*Proceso primero las bajas*/
    /*Limites Baja*/
    IF  par$a_lim_cod_divisa IS NOT NULL AND  par$a_lim_cod_divisa.COUNT > 0 THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia baja de límites de la planificación',
          par$cod_identificador   => par$cod_planificacion);   

        FOR idx IN par$a_lim_cod_divisa.first .. par$a_lim_cod_divisa.last LOOP
            IF par$a_lim_bol_baja(idx) = 1 THEN
            /*valido el limite*/
                SAPR_PLIMITE_###VERSION###.svalidar_limite(NULL, par$a_lim_cod_divisa(idx), NULL, par$a_lim_num(idx), par$cod_cultura);
                /*Obtengo el oid divisa del campo actual*/
                BEGIN
                SELECT OID_CAMPO1
                INTO var$oid_divisa
                FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'OID_DIVISA'
                AND COD_CAMPO2 = par$a_lim_cod_divisa(idx);
                EXCEPTION WHEN no_data_found THEN
                        var$oid_divisa := NULL;
                END;
                /*Obtengo el oid limite del campo actual*/
                BEGIN
                SELECT OID_LIMITE 
                INTO var$oid_limite
                FROM SAPR_TLIMITE 
                WHERE OID_PLANIFICACION = par$oid_planificacion AND OID_DIVISA = var$oid_divisa AND BOL_ACTIVO = 1;
                EXCEPTION WHEN no_data_found THEN
                        var$oid_limite := NULL;
                END;
                /*verifico si hubo error*/
                SELECT COUNT(1)
                INTO var$hayerror
                FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'VALIDACIONES' AND
                COD_CAMPO2 = 'LIMITE'
                AND COD_CAMPO3 IN (par$a_lim_cod_divisa(idx),  par$a_lim_num(idx));

                IF var$hayerror = 0 THEN
                    /*Si no hay error actualizo la tabla limite*/
                    SAPR_PLIMITE_###VERSION###.supd_limite(var$oid_limite, par$oid_planificacion, NULL, NULL, var$oid_divisa, par$a_lim_num(idx), 0, par$cod_usuario);
                END IF;
            END IF;

        END LOOP;
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Finaliza baja de límites de la planificación',
          par$cod_identificador   => par$cod_planificacion);   
    END IF;
     /*Divisas Baja*/
    IF  par$a_per_div_codigo IS NOT NULL AND  par$a_per_div_codigo.COUNT > 0 THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia baja de divisas de la planificación',
          par$cod_identificador   => par$cod_planificacion);   

        FOR idx IN par$a_per_div_codigo.first .. par$a_per_div_codigo.last LOOP
            IF par$a_per_div_baja(idx) = 1 THEN
            
                /*Obtengo el oid divisa del campo actual*/
                BEGIN
                SELECT OID_DIVISA
                INTO var$oid_divisa
                FROM GEPR_TDIVISA
                WHERE COD_ISO_DIVISA = par$a_per_div_codigo(idx);
                EXCEPTION WHEN no_data_found THEN
                        var$oid_divisa := NULL;
                END;
                /*Obtengo el oid planxdivisa del campo actual*/
                BEGIN
                SELECT OID_PLANXDIVISA 
                INTO var$oid_planxdivisa
                FROM SAPR_TPLANXDIVISA 
                WHERE OID_PLANIFICACION = par$oid_planificacion AND OID_DIVISA = var$oid_divisa AND BOL_ACTIVO = 1;
                EXCEPTION WHEN no_data_found THEN
                        var$oid_planxdivisa := NULL;
                END;
                
                IF var$oid_planxdivisa IS NOT NULL THEN
                    
                    supd_planxdivisa(var$oid_planxdivisa, par$oid_planificacion, var$oid_divisa, 0, par$cod_usuario);
                END IF;
            END IF;
        END LOOP;
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Finaliza baja de divisas de la planificación',
          par$cod_identificador   => par$cod_planificacion);  
    END IF;

     /*Movimientos Baja*/
    IF  par$a_per_mov_codigo IS NOT NULL AND  par$a_per_mov_codigo.COUNT > 0 THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia baja de movimientos de la planificación',
          par$cod_identificador   => par$cod_planificacion);   
        FOR idx IN par$a_per_mov_codigo.first .. par$a_per_mov_codigo.last LOOP
            IF par$a_per_mov_baja(idx) = 1 THEN
            
              
                /*Obtengo el oid planxmovimiento del campo actual*/
                BEGIN
                SELECT OID_PLANXMOVIMIENTO 
                INTO var$oid_planxmovimiento
                FROM SAPR_TPLANXMOVIMIENTO 
                WHERE OID_PLANIFICACION = par$oid_planificacion AND OID_FORMULARIO = par$a_per_mov_codigo(idx) AND BOL_ACTIVO = 1;
                EXCEPTION WHEN no_data_found THEN
                        var$oid_planxmovimiento := NULL;
                END;
                
                IF var$oid_planxmovimiento IS NOT NULL THEN
                    
                    supd_planxmovimiento(var$oid_planxmovimiento, par$oid_planificacion, par$a_per_mov_codigo(idx),0, 0, par$cod_usuario);
                END IF;
            END IF;

        END LOOP;
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Finaliza baja de movimientos de la planificación',
          par$cod_identificador   => par$cod_planificacion);   
    END IF;
    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TAUXILIAR
    WHERE COD_CALIFICADOR IN ('OID_DIVISA', 'VALIDACIONES');

    /*Limites Alta*/
    IF  par$a_lim_cod_divisa IS NOT NULL AND  par$a_lim_cod_divisa.COUNT > 0 THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inicia alta de límites de la planificación',
          par$cod_identificador   => par$cod_planificacion); 
        FOR idx IN par$a_lim_cod_divisa.first .. par$a_lim_cod_divisa.last LOOP

                IF par$a_lim_bol_baja(idx) = 0 THEN
                    /*valido el limite*/
                    SAPR_PLIMITE_###VERSION###.svalidar_limite(NULL, par$a_lim_cod_divisa(idx), NULL, par$a_lim_num(idx), par$cod_cultura);
                    /*Obtengo el oid divisa del campo actual*/
                    BEGIN
                    SELECT OID_CAMPO1
                    INTO var$oid_divisa
                    FROM SAPR_GTT_TAUXILIAR
                    WHERE COD_CALIFICADOR = 'OID_DIVISA'
                    AND COD_CAMPO2 = par$a_lim_cod_divisa(idx);
                    EXCEPTION WHEN no_data_found THEN
                            var$oid_divisa := NULL;
                    END;

                    /*Obtengo el oid limite del campo actual*/
                    BEGIN
                    SELECT OID_LIMITE 
                    INTO var$oid_limite
                    FROM SAPR_TLIMITE 
                    WHERE OID_PLANIFICACION = par$oid_planificacion AND OID_DIVISA = var$oid_divisa AND BOL_ACTIVO = 1;
                    EXCEPTION WHEN no_data_found THEN
                            var$oid_limite := NULL;
                    END;
                    /*verifico si hubo error*/
                    SELECT COUNT(1)
                    INTO var$hayerror
                    FROM SAPR_GTT_TAUXILIAR
                    WHERE COD_CALIFICADOR = 'VALIDACIONES' AND
                    COD_CAMPO2 = 'LIMITE'
                    AND COD_CAMPO3 IN (par$a_lim_cod_divisa(idx),  par$a_lim_num(idx));

                    IF var$hayerror = 0 THEN
                        /*Si no hay error actualizo la tabla limite*/
                        SAPR_PLIMITE_###VERSION###.supd_limite(var$oid_limite, par$oid_planificacion, NULL, NULL, var$oid_divisa, par$a_lim_num(idx), 1, par$cod_usuario);
                    END IF;
                END IF;
        END LOOP;
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Finaliza alta de límites de la planificación',
          par$cod_identificador   => par$cod_planificacion); 
    END IF;
     /*Divisas Alta*/
    IF  par$a_per_div_codigo IS NOT NULL AND  par$a_per_div_codigo.COUNT > 0 THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,
              par$des_detalle     => 'Inicia alta de divisas de la planificación',
              par$cod_identificador   => par$cod_planificacion);  
        FOR idx IN par$a_per_div_codigo.first .. par$a_per_div_codigo.last LOOP
            IF par$a_per_div_baja(idx) = 0 THEN
            
                /*Obtengo el oid divisa del campo actual*/
                BEGIN
                SELECT OID_DIVISA
                INTO var$oid_divisa
                FROM GEPR_TDIVISA
                WHERE COD_ISO_DIVISA = par$a_per_div_codigo(idx);
                EXCEPTION WHEN no_data_found THEN
                        var$oid_divisa := NULL;
                END;
                /*Obtengo el oid planxdivisa del campo actual*/
                BEGIN
                SELECT OID_PLANXDIVISA 
                INTO var$oid_planxdivisa
                FROM SAPR_TPLANXDIVISA 
                WHERE OID_PLANIFICACION = par$oid_planificacion AND OID_DIVISA = var$oid_divisa AND BOL_ACTIVO = 1;
                EXCEPTION WHEN no_data_found THEN
                        var$oid_planxdivisa := NULL;
                END;
                supd_planxdivisa(var$oid_planxdivisa, par$oid_planificacion, var$oid_divisa, 1, par$cod_usuario);
            END IF;

        END LOOP;
        SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,
              par$des_detalle     => 'Finaliza alta de divisas de la planificación',
              par$cod_identificador   => par$cod_planificacion);  
    END IF;
    /*Movimientos alta*/
    IF  par$a_per_mov_codigo IS NOT NULL AND  par$a_per_mov_codigo.COUNT > 0 THEN
      SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$des_origen,
            par$des_version     => const$version,
            par$des_detalle     => 'Inicia alta de movimientos de la planificación',
            par$cod_identificador   => par$cod_planificacion);   

      FOR idx IN par$a_per_mov_codigo.first .. par$a_per_mov_codigo.last LOOP
          IF par$a_per_mov_baja(idx) = 0 THEN
              /*Obtengo el oid planxmovimiento del campo actual*/
              BEGIN
              SELECT OID_PLANXMOVIMIENTO 
              INTO var$oid_planxmovimiento
              FROM SAPR_TPLANXMOVIMIENTO 
              WHERE OID_PLANIFICACION = par$oid_planificacion AND OID_FORMULARIO = par$a_per_mov_codigo(idx) AND BOL_ACTIVO = 1;
              EXCEPTION WHEN no_data_found THEN
                      var$oid_planxmovimiento := NULL;
              END;
              supd_planxmovimiento(var$oid_planxmovimiento, par$oid_planificacion, par$a_per_mov_codigo(idx),1, 1, par$cod_usuario);
          END IF;
      END LOOP;
      SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$des_origen,
            par$des_version     => const$version,
            par$des_detalle     => 'Finaliza alta de movimientos de la planificación',
            par$cod_identificador   => par$cod_planificacion); 
    END IF;

    /* ===  Commit === */
    COMMIT;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$des_origen,
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
  /*EXCEPTION
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
                              true);*/
  END sgrabar_planificacion;


  



  /*Insert/update de proceso por plan*/
  PROCEDURE supd_planxproceso(par$oid_planificacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$aoid_proceso               IN gepr_pcomon_###VERSION###.tipo$oids_,
                          par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$inserts                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$deletes                 OUT gepr_pcomon_###VERSION###.tipo$nel_)
  IS
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    /* Inicializo los parametros de salidas */
    par$inserts := 0;
    par$deletes := 0;

    /*Borramos físicamente todos los proceso de la planificación*/
    DELETE FROM SAPR_TPROCESOXPLANIFICACION WHERE OID_PLANIFICACION = par$oid_planificacion;
    par$deletes := par$deletes + sql%ROWCOUNT;

    IF par$aoid_proceso is not null THEN
      FOR idx in par$aoid_proceso.first .. par$aoid_proceso.last LOOP
        IF par$aoid_proceso(idx) IS NOT NULL THEN
          INSERT INTO SAPR_TPROCESOXPLANIFICACION
          (
            OID_PROCESOXPLANIFICACION, OID_PROCESO, OID_PLANIFICACION, 
            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
          )
          VALUES 
          (
            sys_guid(), par$aoid_proceso(idx), par$oid_planificacion,  
            var$gmt_zero, par$cod_usuario, var$gmt_zero, par$cod_usuario
          );
          par$inserts := par$inserts + sql%ROWCOUNT;
        END IF;
      END LOOP;
    END IF;


  END supd_planxproceso;  

  PROCEDURE supd_planxdivisa(
                            par$oid_planxdivisa          IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_divisa         IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$bol_activo          IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_) IS
 var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
 var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_;
BEGIN
  
  
     IF par$oid_planxdivisa IS NULL THEN
          /*Si no existe el planxdivisa hago insert*/
          par$oid_planxdivisa := SYS_GUID();

          INSERT INTO SAPR_TPLANXDIVISA (OID_PLANXDIVISA, OID_PLANIFICACION,  OID_DIVISA,  BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION,
          DES_USUARIO_MODIFICACION) VALUES
          (par$oid_planxdivisa, par$oid_planificacion, par$oid_divisa, par$bol_activo,  var$gmt_zero, par$cod_usuario, 
          CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), par$cod_usuario);
          
     ELSE
            /*Si existe el planxdivisa hago update*/
            IF  par$oid_planificacion IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_PLANIFICACION = ']' || par$oid_planificacion ||q'[', ]';
            END IF;
            IF  par$oid_divisa IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_DIVISA = ']' || par$oid_divisa ||q'[', ]';
            END IF;
            IF  par$bol_activo IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ BOL_ACTIVO = ']' || par$bol_activo ||q'[', ]';
            END IF;
           IF  var$qry_update IS NOT NULL  THEN
           
         
             EXECUTE IMMEDIATE q'[ UPDATE SAPR_TPLANXDIVISA SET ]' || var$qry_update ||q'[
             GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2
             WHERE OID_PLANXDIVISA = :3]'
              USING CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), par$cod_usuario, par$oid_planxdivisa;
           END IF;
        END IF;

END supd_planxdivisa;


PROCEDURE supd_planxmovimiento(
                            par$oid_planxmovimiento IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$bol_corte_periodo   IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$bol_activo          IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_) IS
 var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
 var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_;
BEGIN
  
  
     IF par$oid_planxmovimiento IS NULL THEN
          /*Si no existe el planxmovimiento hago insert*/
          par$oid_planxmovimiento := SYS_GUID();

          INSERT INTO SAPR_TPLANXMOVIMIENTO (OID_PLANXMOVIMIENTO, OID_PLANIFICACION,  OID_FORMULARIO, BOL_CORTE_PERIODO,  BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION,
          DES_USUARIO_MODIFICACION) VALUES
          (par$oid_planxmovimiento, par$oid_planificacion, par$oid_formulario,  par$bol_corte_periodo, par$bol_activo,  var$gmt_zero, par$cod_usuario, 
          CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), par$cod_usuario);
          
     ELSE
            /*Si existe el planxmovimiento hago update*/
            IF  par$oid_planificacion IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_PLANIFICACION = ']' || par$oid_planificacion ||q'[', ]';
            END IF;
            IF  par$oid_formulario IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_FORMULARIO = ']' || par$oid_formulario ||q'[', ]';
            END IF;
              IF  par$bol_corte_periodo IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ BOL_CORTE_PERIODO = ']' || par$bol_corte_periodo ||q'[', ]';
            END IF;
            IF  par$bol_activo IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ BOL_ACTIVO = ']' || par$bol_activo ||q'[', ]';
            END IF;
           IF  var$qry_update IS NOT NULL  THEN
           
         
             EXECUTE IMMEDIATE q'[ UPDATE SAPR_TPLANXMOVIMIENTO SET ]' || var$qry_update ||q'[
             GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2
             WHERE OID_PLANXMOVIMIENTO = :3]'
              USING CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), par$cod_usuario, par$oid_planxmovimiento;
           END IF;
        END IF;

END supd_planxmovimiento;

  /* Validar planificacion */
  PROCEDURE svalidar_planificacion(par$cod_planificacion       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$des_planificacion       IN gepr_pcomon_###VERSION###.tipo$desc_,
                                   par$oid_tipo_planificacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_cliente             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_delegacion          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$fyh_vigencia_inicio     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$aprog_num_dia_fin       IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$aprog_fyh_fechahora_fin IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$bol_activo              IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    var$identificador gepr_pcomon_###VERSION###.tipo$oid_;

  BEGIN

    IF par$cod_usuario IS NULL THEN

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'GENESIS_PRODUCTO_VAL104',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$cod_usuario',
                                                                  0));
    END IF;

    IF par$cod_planificacion IS NULL THEN

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'GENESIS_PRODUCTO_VAL104',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$cod_planificacion',
                                                                  0));
    END IF;

    IF par$des_planificacion IS NULL THEN

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'GENESIS_PRODUCTO_VAL104',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$des_planificacion',
                                                                  0));

    END IF;

    IF par$bol_activo = 1 THEN
      IF par$fyh_vigencia_inicio IS NULL THEN

        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'GENESIS_PRODUCTO_VAL104',
                                                                    'validaciones_genesis_producto',
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    'par$fyh_vigencia_inicio',
                                                                    0));

      END IF;
    END IF;
    IF par$oid_tipo_planificacion IS NULL THEN

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'GENESIS_PRODUCTO_VAL104',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$oid_tipo_planificacion',
                                                                  0));
    ELSE

      BEGIN
        SELECT OID_TIPO_PLANIFICACION
          INTO var$identificador
          FROM SAPR_TTIPO_PLANIFICACION
         WHERE OID_TIPO_PLANIFICACION = par$oid_tipo_planificacion;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'GENESIS_PRODUCTO_VAL104',
                                                                      'validaciones_genesis_producto',
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      'par$oid_tipo_planificacion',
                                                                      0));
      END;

    END IF;

    IF par$oid_cliente IS NULL THEN

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'GENESIS_PRODUCTO_VAL104',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$oid_cliente',
                                                                  0));
    ELSE

      BEGIN
        SELECT OID_CLIENTE
          INTO var$identificador
          FROM GEPR_TCLIENTE
         WHERE OID_CLIENTE = par$oid_cliente;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'GENESIS_PRODUCTO_VAL104',
                                                                      'validaciones_genesis_producto',
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      'par$oid_cliente',
                                                                      0));
      END;

    END IF;

    IF par$oid_delegacion IS NULL THEN

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'GENESIS_PRODUCTO_VAL104',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$oid_delegacion',
                                                                  0));
    ELSE

      BEGIN
        SELECT OID_DELEGACION
          INTO var$identificador
          FROM GEPR_TDELEGACION
         WHERE OID_DELEGACION = par$oid_delegacion;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'GENESIS_PRODUCTO_VAL104',
                                                                      'validaciones_genesis_producto',
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      'par$oid_delegacion',
                                                                      0));
      END;

    END IF;

    IF par$aprog_num_dia_fin IS NULL OR par$aprog_num_dia_fin.COUNT = 0 THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'GENESIS_PRODUCTO_VAL104',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$aprog_num_dia_fin',
                                                                  0));
    END IF;

    IF par$aprog_fyh_fechahora_fin IS NULL OR
       par$aprog_fyh_fechahora_fin.COUNT = 0 THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'GENESIS_PRODUCTO_VAL104',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$aprog_fyh_fechahora_fin',
                                                                  0));
    END IF;

    IF par$aprog_fyh_fechahora_fin.COUNT <> par$aprog_num_dia_fin.COUNT THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'cantidad_datos_no_documentos',
                                                                  'validaciones_genesis_producto',
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
    END IF;

  END svalidar_planificacion;

  /* Insert/Update Planificacion */
  PROCEDURE supd_planificacion(par$oid_planificacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_planificacion      IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$des_planificacion      IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$oid_tipo_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_tipo_confirmacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$oid_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_delegacion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$fyh_vigencia_inicio    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$fyh_vigencia_fin       IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$bol_activo             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$nec_contigencia        IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$bol_controlafacturacion IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$oid_banco_comision      IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$por_comision_plan       IN gepr_pcomon_###VERSION###.tipo$imp_,
                               par$dia_cierre_facturacion  IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$bol_divide_subcanal   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_divide_divisa   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_agrupa_subcanal     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_agrupa_puntoservicio IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_agrupa_fechacontable IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$selects                OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$inserts                OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$updates                OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /* variables locales */
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$existe   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
  BEGIN

    /* Verifica si es Update */
    SELECT COUNT(1)
      INTO var$existe
      FROM SAPR_TPLANIFICACION P
     WHERE P.OID_PLANIFICACION = par$oid_planificacion
       AND ROWNUM < 2;
    par$selects := sql%rowcount;

    IF var$existe > 0 THEN

      UPDATE sapr_tplanificacion
         SET oid_tipo_planificacion   = par$oid_tipo_planificacion,
             oid_cliente              = par$oid_cliente,
             oid_delegacion           = par$oid_delegacion,
             cod_planificacion        = par$cod_planificacion,
             des_planificacion        = par$des_planificacion,
             cod_tipo_confirmacion    = par$cod_tipo_confirmacion,
             fyh_vigencia_inicio      = par$fyh_vigencia_inicio,
             fyh_vigencia_fin         = par$fyh_vigencia_fin,
             bol_activo               = par$bol_activo,
             gmt_modificacion         = var$gmt_zero,
             des_usuario_modificacion = par$cod_usuario,
             nec_contingencia         = par$nec_contigencia,
             bol_controla_facturacion = par$bol_controlafacturacion,
             oid_cliente_comision        = par$oid_banco_comision,
             nec_dia_cierre           = par$dia_cierre_facturacion,
             num_porcent_comision        = par$por_comision_plan,
             bol_periodo_subcanal   = par$bol_divide_subcanal,
             BOL_PERIODO_DIVISA     = par$bol_divide_divisa,
             bol_agr_subcanal       = par$bol_agrupa_subcanal,
             bol_agr_pto_servicio   = par$bol_agrupa_puntoservicio,
             bol_agr_fec_contable   = par$bol_agrupa_fechacontable
       WHERE oid_planificacion = par$oid_planificacion;
      par$updates := sql%rowcount;

    ELSE

      INSERT INTO sapr_tplanificacion
        (oid_planificacion,
         oid_tipo_planificacion,
         oid_cliente,
         cod_planificacion,
         des_planificacion,
         cod_tipo_confirmacion,
         fyh_vigencia_inicio,
         fyh_vigencia_fin,
         bol_activo,
         gmt_creacion,
         des_usuario_creacion,
         gmt_modificacion,
         des_usuario_modificacion,
         nec_contingencia,
         oid_delegacion,
         bol_controla_facturacion,
         oid_cliente_comision,
         nec_dia_cierre,
         num_porcent_comision,
         bol_periodo_subcanal,
         bol_periodo_divisa,
         bol_agr_subcanal,
     bol_agr_pto_servicio,
     bol_agr_fec_contable)
      VALUES
        (par$oid_planificacion,
         par$oid_tipo_planificacion,
         par$oid_cliente,
         par$cod_planificacion,
         par$des_planificacion,
         par$cod_tipo_confirmacion,
         par$fyh_vigencia_inicio,
         par$fyh_vigencia_fin,
         par$bol_activo,
         var$gmt_zero,
         par$cod_usuario,
         var$gmt_zero,
         par$cod_usuario,
         par$nec_contigencia,
         par$oid_delegacion,
         par$bol_controlafacturacion,
         par$oid_banco_comision,
         par$dia_cierre_facturacion,
         par$por_comision_plan,
         par$bol_divide_subcanal,
         par$bol_divide_divisa,
         par$bol_agrupa_subcanal,
     par$bol_agrupa_puntoservicio,
     par$bol_agrupa_fechacontable);
      par$inserts := sql%rowcount;
      
    END IF;

  END supd_planificacion;

  /* Insert/Update PlanXMaquinas */
  PROCEDURE supd_planxmaquinas(par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$fyh_vigencia_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$fyh_vigencia_fin    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$amaq_oid_maquina    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$bol_cambio_horario  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_cambio_program      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$fyh_vigencia_fin_old   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$deletes             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$updates             OUT gepr_pcomon_###VERSION###.tipo$nel_)IS

    /* variables locales */
    var$gmt_zero                  gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$_fyh_ultimo_periodo       gepr_pcomon_###VERSION###.tipo$fyh_;
    var$_fyh_vigencia_inicio_maq  gepr_pcomon_###VERSION###.tipo$fyh_;
    var$_fyh_vigencia_fin_maq     gepr_pcomon_###VERSION###.tipo$fyh_;
    var$_fyh_cambio               gepr_pcomon_###VERSION###.tipo$fyh_ := SYS_EXTRACT_UTC (CURRENT_TIMESTAMP);
    var$existe                    gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$existe2                   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;

  BEGIN

    par$inserts      := 0;
    par$selects      := 0;
    par$updates      := 0;
    par$deletes      := 0;

    DELETE SAPR_GTT_TAUXILIAR;

    IF par$amaq_oid_maquina IS NOT NULL AND par$amaq_oid_maquina.COUNT > 0  THEN
      FOR idxx IN par$amaq_oid_maquina.first .. par$amaq_oid_maquina.last LOOP
        IF par$amaq_oid_maquina(idxx) IS NOT NULL THEN

          SELECT COUNT(1)
            INTO var$existe
          FROM SAPR_TPLANXMAQUINA PM
          WHERE PM.OID_PLANIFICACION = par$oid_planificacion
            AND PM.OID_MAQUINA = par$amaq_oid_maquina(idxx)
            AND PM.BOL_ACTIVO = 1
            AND ROWNUM < 2;

          /* Insere por meio do cursor par$amaq_oid_maquina, quais maquinas ja existem na planificação */
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR, COD_CAMPO2)
          VALUES (par$amaq_oid_maquina(idxx), 'OID_MAQUINA', var$existe);
        END IF;
      END LOOP;
    END IF;

    /* Borrar todos los registros (logico) */
    -- BORRA LOGICAMENTE LAS MAQUINAS QUE PERTENECIAN A LA PLANIFICACION Y YA NO SON PARTE DE LA MISMA
    UPDATE SAPR_TPLANXMAQUINA
      SET BOL_ACTIVO               = 0,
          GMT_MODIFICACION         = var$gmt_zero,
          DES_USUARIO_MODIFICACION = par$cod_usuario
      WHERE OID_PLANIFICACION = par$oid_planificacion
        AND BOL_ACTIVO = 1
        AND OID_MAQUINA NOT IN (SELECT OID_CAMPO1 FROM  SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_MAQUINA' );
    par$updates := par$updates + sql%rowcount;

    -- BORRA LOGICAMENTE LAS MAQUINAS QUE PERTENECIAN A OTRA PLANIFICACION, Y AHORA SON PARTE DE LA ENVIADA POR PARAMETRO
    UPDATE SAPR_TPLANXMAQUINA
       SET bol_activo               = 0,
           gmt_modificacion         = var$gmt_zero,
           des_usuario_modificacion = par$cod_usuario
     WHERE oid_planificacion <> par$oid_planificacion
           AND bol_activo = 1
           AND oid_maquina in (select OID_CAMPO1 from  SAPR_GTT_TAUXILIAR where COD_CALIFICADOR = 'OID_MAQUINA' );
               par$updates := par$updates + sql%rowcount;


    IF par$amaq_oid_maquina is not null and not gepr_putilidades_###VERSION###.farray_vacio_oid(par$array => par$amaq_oid_maquina) then

      /* Relacionar */
      FOR idx IN par$amaq_oid_maquina.first .. par$amaq_oid_maquina.last LOOP

        SELECT count(1)
          INTO var$existe2
          FROM SAPR_GTT_TAUXILIAR AX
        WHERE AX.OID_CAMPO1 =  par$amaq_oid_maquina(idx)
          AND AX.COD_CALIFICADOR = 'OID_MAQUINA' 
          AND AX.COD_CAMPO2  = 1
          AND ROWNUM < 2;

        /*Maquina Nova*/
        IF var$existe2 = 0 THEN

          -- Si la fecha de inicio de la planificación es a futuro, indicamos la fecha a futuro, caso contrario aplicamos la fecha/hora actual (fyh_cambio) 
          INSERT INTO SAPR_TPLANXMAQUINA
            (OID_PLANXMAQUINA,
            OID_PLANIFICACION,
            OID_MAQUINA,
            OID_SECTOR,
            FYH_VIGENCIA_INICIO,
            FYH_VIGENCIA_FIN,
            FYH_ULTIMO_PERIODO,
            BOL_ACTIVO,
            GMT_CREACION,
            DES_USUARIO_CREACION,
            GMT_MODIFICACION,
            DES_USUARIO_MODIFICACION)
          VALUES
            (sys_guid(),
            par$oid_planificacion,
            par$amaq_oid_maquina(idx),
            (SELECT OID_SECTOR
                FROM SAPR_TMAQUINA
              WHERE OID_MAQUINA = par$amaq_oid_maquina(idx)),
            CASE WHEN par$fyh_vigencia_inicio > var$_fyh_cambio THEN par$fyh_vigencia_inicio ELSE var$_fyh_cambio END,
            NULL,
            NULL,
            1,
            var$gmt_zero,
            par$cod_usuario,
            var$gmt_zero,
            par$cod_usuario);
            par$inserts := par$inserts + sql%rowcount;

        ELSE /*maquina ja existente na planificação*/
          -- SI CAMBIA EL HORARIO DE LA PLANIFICACION Y/O SE CAMBIA EL HORARIO DE LA PROGRAMACION (CORTES)
          -- SE BORRAN LOS PERIODOS DE ACREDITACION ABIERTOS FUTUROS (LUEGO SERAN CREADOS POR SWITCH)
          IF par$bol_cambio_program = 1 OR par$bol_cambio_horario = 1 THEN
            /* INSERT  GTTAUXILIAR COD CALIFICADOR DEL_PERIODO*/     
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR )                    
            SELECT PERI.OID_MAQUINA, PERI.OID_PERIODO, TO_CHAR(PERI.FYH_INICIO, 'DD/MM/YYYY HH24:MI:SS'), 'DEL_PERIODO'
            FROM SAPR_TPERIODO PERI 
              INNER JOIN SAPR_TESTADO_PERIODO EST_PER ON PERI.OID_ESTADO_PERIODO = EST_PER.OID_ESTADO_PERIODO
              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
                AND TIPE.COD_TIPO_PERIODO = 'AC'
            WHERE PERI.OID_MAQUINA = par$amaq_oid_maquina(idx) 
              AND PERI.OID_PLANIFICACION    = par$oid_planificacion
              AND PERI.FYH_INICIO > var$_fyh_cambio
              AND EST_PER.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND PERI.oid_acreditacion IS NULL;
            par$inserts := par$inserts + sql%rowcount;

          END IF;

          -- SI CAMBIA EL HORARIO DE LA PROGRAMACION (CORTES)
          IF par$bol_cambio_program = 1 THEN
            /* MODIFICAMOS LA FECHA FIN DEL PERIODO DE ACREDITACION VIGENTE AL MOMENTO DEL CAMBIO DE HORARIO */
            UPDATE SAPR_TPERIODO
            SET FYH_FIN = var$_fyh_cambio,
              GMT_MODIFICACION = var$gmt_zero,
              DES_USUARIO_MODIFICACION = par$cod_usuario
            WHERE OID_MAQUINA = par$amaq_oid_maquina(idx) 
            AND OID_PLANIFICACION = par$oid_planificacion
            AND var$_fyh_cambio BETWEEN FYH_INICIO AND FYH_FIN
            AND OID_ACREDITACION IS NULL
            AND OID_ESTADO_PERIODO IN (SELECT OID_ESTADO_PERIODO FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB'))
            AND OID_TIPO_PERIODO = (SELECT OID_TIPO_PERIODO FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'AC')
            ;

          END IF;

          -- SI CAMBIA EL HORARIO DE LA PLANIFICACION SE GUARDA EL HISTORICO
          IF par$bol_cambio_horario = 1 THEN   

            -- Recuperamos los valores actuales de la PLANXMAQUINA
            SELECT FYH_VIGENCIA_INICIO, FYH_VIGENCIA_FIN
              INTO var$_fyh_vigencia_inicio_maq, var$_fyh_vigencia_fin_maq
            FROM SAPR_TPLANXMAQUINA
            WHERE OID_PLANIFICACION = par$oid_planificacion
              AND OID_MAQUINA = par$amaq_oid_maquina(idx)
              AND BOL_ACTIVO = 1;

            UPDATE SAPR_TPLANXMAQUINA
            SET BOL_ACTIVO = 0,                      
                GMT_MODIFICACION         = var$gmt_zero,
                DES_USUARIO_MODIFICACION = par$cod_usuario
            WHERE
              OID_PLANIFICACION = par$oid_planificacion
              AND OID_MAQUINA = par$amaq_oid_maquina(idx)
              AND BOL_ACTIVO = 1;
              par$updates := par$updates + sql%rowcount;
                
            -- VALIDAR FECHAS INICIO Y VIGENCIA
            -- SI LA FYH_INICIO_PLANIFICACION > FYH_INICIO_MAQUINA ENTONCES FYH_INICIO_PLANIFICACION SINO FYH_INICIO_MAQUINA
            -- SI FYH_FIN_MAQUINA ES NULA ENTONCES SIGUE NULA, LA FYH_FIN_PLANIFICACION < FYH_FIN_MAQUINA ENTONCES FYH_FIN_PLANIFICACION SINO FYH_FIN_MAQUINA
            INSERT INTO SAPR_TPLANXMAQUINA
                (OID_PLANXMAQUINA,
                  OID_PLANIFICACION,
                  OID_MAQUINA,
                  OID_SECTOR,
                  FYH_VIGENCIA_INICIO,
                  FYH_VIGENCIA_FIN,
                  FYH_ULTIMO_PERIODO,
                  BOL_ACTIVO,
                  GMT_CREACION,
                  DES_USUARIO_CREACION,
                  GMT_MODIFICACION,
                  DES_USUARIO_MODIFICACION)
            VALUES
                (sys_guid(),
                  par$oid_planificacion,
                  par$amaq_oid_maquina(idx),
                  (SELECT OID_SECTOR
                    FROM SAPR_TMAQUINA
                    WHERE OID_MAQUINA = par$amaq_oid_maquina(idx)),
                  CASE WHEN par$fyh_vigencia_inicio > var$_fyh_vigencia_inicio_maq THEN  par$fyh_vigencia_inicio ELSE var$_fyh_vigencia_inicio_maq END,
                  CASE WHEN var$_fyh_vigencia_fin_maq IS NOT NULL AND var$_fyh_vigencia_fin_maq >  par$fyh_vigencia_fin THEN par$fyh_vigencia_fin
                    ELSE var$_fyh_vigencia_fin_maq END,
                  NULL,
                  1,
                  var$gmt_zero,
                  par$cod_usuario,
                  var$gmt_zero,
                  par$cod_usuario);
            par$inserts := par$inserts + sql%rowcount;
              
          END IF;
        END IF;
      END LOOP;
    END IF;
    /*  Inserta en tabla auxiliar los períodos correspondientes a las máquinas
          de la planificación que ya no están en la misma (borradas)
    */
    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR )
        SELECT PERI.OID_MAQUINA, PERI.OID_PERIODO, TO_CHAR(PERI.FYH_INICIO, 'DD/MM/YYYY HH24:MI:SS'), 'DEL_PERIODO'
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TESTADO_PERIODO E ON PERI.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
          INNER JOIN SAPR_TPLANXMAQUINA PM ON PM.OID_PLANIFICACION = PERI.OID_PLANIFICACION AND PM.OID_MAQUINA = PERI.OID_MAQUINA
        WHERE PERI.OID_PLANIFICACION = par$oid_planificacion
          AND E.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND PERI.oid_acreditacion IS NULL
          AND PM.BOL_ACTIVO = 0
          AND PERI.OID_MAQUINA not in (select OID_CAMPO1 from  SAPR_GTT_TAUXILIAR where COD_CALIFICADOR = 'OID_MAQUINA' );

    /*  Inserta en tabla auxiliar los períodos correspondientes a las máquinas
        Que no existen en la planificación actual (nuevas) pero 
        podrían estar en otra planificación
    */
    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR )
        SELECT PERI.OID_MAQUINA, PERI.OID_PERIODO , TO_CHAR(PERI.FYH_INICIO, 'DD/MM/YYYY HH24:MI:SS'), 'DEL_PERIODO'
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TESTADO_PERIODO TEPE ON PERI.OID_ESTADO_PERIODO = TEPE.OID_ESTADO_PERIODO
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'OID_MAQUINA' AND AUX.OID_CAMPO1 = PERI.OID_MAQUINA
        WHERE AUX.COD_CAMPO2 = 0
          AND TEPE.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND PERI.oid_acreditacion IS NULL
          AND PERI.OID_PLANIFICACION <> par$oid_planificacion;

    /*Borrar periodos abiertos */
    DELETE FROM SAPR_TPERIODOXDOCUMENTO PD
      WHERE PD.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes := sql%rowcount;

    DELETE FROM SAPR_TCALCULO_EFECTIVO CE
      WHERE CE.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes := par$deletes + sql%rowcount;

    DELETE FROM SAPR_TCALCULO_MEDIO_PAGO CMP
      WHERE CMP.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes :=  par$deletes +sql%rowcount;

    DELETE FROM SAPR_TPERIODO PR
      WHERE PR.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes :=  par$deletes + sql%rowcount;
    /*Fin borrar periodos */

    -- ACTUALIZAR LA FECHA DEL ÚLTIMO PERIODO
    -- SI MAX(FYH_ULTIMO_PERIODO DE LA MAQUINA EN ESTA PLANIFICACION) > FYH_VIGENCIA_INCIO_MAQ = MAX(FYH_ULTIMO_PERIODO DE LA MAQUINA EN ESTA PLANIFICACION)
    -- SI NO FYH_VIGENCIA_MAQ
    IF par$amaq_oid_maquina is not null and not gepr_putilidades_###VERSION###.farray_vacio_oid(par$array => par$amaq_oid_maquina) then
      FOR idx IN par$amaq_oid_maquina.first .. par$amaq_oid_maquina.last LOOP

        BEGIN
          SELECT FYH_VIGENCIA_INICIO
            INTO var$_fyh_vigencia_inicio_maq
            FROM SAPR_TPLANXMAQUINA
            WHERE OID_MAQUINA = par$amaq_oid_maquina(idx)
              AND OID_PLANIFICACION = par$oid_planificacion
              AND BOL_ACTIVO = 1
            ;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$_fyh_vigencia_inicio_maq := NULL;
        END;
        
         DBMS_OUTPUT.PUT_LINE('Maquina de OID_MAQUINA: ' || par$amaq_oid_maquina(idx) || ', FYH_VIGENCIA_INICIO: ' || TO_CHAR(var$_fyh_vigencia_inicio_maq, 'DD-MM-YYYY HH24:MI:SS'));

        IF var$_fyh_vigencia_inicio_maq IS NOT NULL THEN
          SELECT MAX(FYH_FIN)
          INTO var$_fyh_ultimo_periodo
          FROM SAPR_TPERIODO P
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO 
                                                  AND TIPE.COD_TIPO_PERIODO = 'AC'
            INNER JOIN SAPR_TESTADO_PERIODO E ON P.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
          WHERE P.OID_MAQUINA = par$amaq_oid_maquina(idx)
            AND P.OID_PLANIFICACION = par$oid_planificacion;
          
          DBMS_OUTPUT.PUT_LINE('Maquina de OID_MAQUINA: ' || par$amaq_oid_maquina(idx) || ', FYH_ULTIMO_PERIODO: ' || TO_CHAR(var$_fyh_ultimo_periodo, 'DD-MM-YYYY HH24:MI:SS'));

          UPDATE SAPR_TPLANXMAQUINA
              SET FYH_ULTIMO_PERIODO = CASE WHEN var$_fyh_ultimo_periodo IS NOT NULL AND var$_fyh_ultimo_periodo > var$_fyh_vigencia_inicio_maq  THEN var$_fyh_ultimo_periodo ELSE  NULL END,                      
              GMT_MODIFICACION         = var$gmt_zero,
              DES_USUARIO_MODIFICACION = par$cod_usuario
          WHERE
              OID_PLANIFICACION = par$oid_planificacion
              AND OID_MAQUINA = par$amaq_oid_maquina(idx)
              AND BOL_ACTIVO = 1; 
        END IF;
      END LOOP;
    END IF;

  END supd_planxmaquinas;

  /* Insert/Update PlanXMaquinas para fecha valor con confirmacion*/
  PROCEDURE supd_planxmaquinas_fvc(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                               par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$fyh_vigencia_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$fyh_vigencia_fin    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$amaq_oid_maquina    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$bol_cambio_horario  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$bol_cambio_program      IN gepr_pcomon_###VERSION###.tipo$nbol_,
                               par$fyh_vigencia_fin_old   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$deletes             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$updates             OUT gepr_pcomon_###VERSION###.tipo$nel_)IS

    /* variables locales */
    var$gmt_zero                  gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$_fyh_ultimo_periodo       gepr_pcomon_###VERSION###.tipo$fyh_;
    var$_fyh_vigencia_inicio_maq  gepr_pcomon_###VERSION###.tipo$fyh_;
    var$_fyh_vigencia_fin_maq     gepr_pcomon_###VERSION###.tipo$fyh_;
    var$_fyh_cambio               gepr_pcomon_###VERSION###.tipo$fyh_ := SYS_EXTRACT_UTC (CURRENT_TIMESTAMP);
    var$existe                    gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$existe2                   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$cod_divisa_defecto        gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_divisa_defecto        gepr_pcomon_###VERSION###.tipo$oid_;
    const$cod_parametro_divisa_def CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'CodigoIsoDivisaDefecto';
    var$cod_pais                  gepr_pcomon_###VERSION###.tipo$cod_;
    const$des_origen CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.supd_planxmaquinas_fvc';
  BEGIN

    par$inserts      := 0;
    par$selects      := 0;
    par$updates      := 0;
    par$deletes      := 0;

    DELETE SAPR_GTT_TAUXILIAR;
    SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Recorrer lista de oid maquina ' || par$amaq_oid_maquina.COUNT,
          par$cod_identificador   => '');

    IF par$amaq_oid_maquina IS NOT NULL AND par$amaq_oid_maquina.COUNT > 0  THEN
      FOR idxx IN par$amaq_oid_maquina.first .. par$amaq_oid_maquina.last LOOP
        IF par$amaq_oid_maquina(idxx) IS NOT NULL THEN

          SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Validar si existe PLANxMAQUINA para par$oid_planificacion: ' || par$oid_planificacion || ', par$amaq_oid_maquina(' || idxx || '):' || par$amaq_oid_maquina(idxx),
                    par$cod_identificador   => '');

          SELECT COUNT(1)
            INTO var$existe
          FROM SAPR_TPLANXMAQUINA PM
          WHERE PM.OID_PLANIFICACION = par$oid_planificacion
            AND PM.OID_MAQUINA = par$amaq_oid_maquina(idxx)
            AND PM.BOL_ACTIVO = 1
            AND ROWNUM < 2;

          SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Validar si existe PLANxMAQUINA, resultado: var$existe ->  ' || var$existe,
                    par$cod_identificador   => '');

          /* Insere por meio do cursor par$amaq_oid_maquina, quais maquinas ja existem na planificação */
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR, COD_CAMPO2)
          VALUES (par$amaq_oid_maquina(idxx), 'OID_MAQUINA', var$existe);
        END IF;

        IF var$existe = 1 THEN
        /* Obtiene el cod_pais */
          SELECT P.COD_PAIS 
            INTO var$cod_pais
          FROM SAPR_TPLANIFICACION PL
            INNER JOIN GEPR_TDELEGACION DL ON PL.OID_DELEGACION=DL.OID_DELEGACION AND DL.BOL_VIGENTE='1'
            INNER JOIN GEPR_TPAIS P ON DL.OID_PAIS = P.OID_PAIS AND P.BOL_ACTIVO='1'
            WHERE PL.OID_PLANIFICACION=par$oid_planificacion
            AND PL.BOL_ACTIVO='1';

          SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Obtiene el cod_pais var$cod_pais ->  ' || var$cod_pais,
                    par$cod_identificador   => '');

        /* Obtiene valor de parametro CodigoIsoDivisaDefecto */
            SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Obtiene valor de parametro CodigoIsoDivisaDefecto  con el valor const$cod_parametro_divisa_def->  ' || const$cod_parametro_divisa_def,
                    par$cod_identificador   => '');

            var$cod_divisa_defecto := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                                                      par$cod_pais => var$cod_pais,
                                                                      par$cod_parametro => const$cod_parametro_divisa_def,
                                                                      par$cod_aplicacion => 'GenesisSaldos');
             SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Obtiene valor de parametro CodigoIsoDivisaDefecto var$cod_divisa_defecto ->  ' || var$cod_divisa_defecto,
                    par$cod_identificador   => '');

            if var$cod_divisa_defecto is not null then
            /* Valida si valor de CodigoIsoDivisaDefecto existe */
             SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Valida si valor de CodigoIsoDivisaDefecto existe',
                    par$cod_identificador   => '');
              BEGIN
                SELECT OID_DIVISA
                INTO var$oid_divisa_defecto
                FROM GEPR_TDIVISA
                WHERE COD_ISO_DIVISA = var$cod_divisa_defecto AND BOL_VIGENTE = 1;
              EXCEPTION WHEN no_data_found THEN
                    var$oid_divisa_defecto := NULL;
              END;
              SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'var$oid_divisa_defecto: ' || var$oid_divisa_defecto,
                    par$cod_identificador   => '');

              IF var$oid_divisa_defecto IS not NULL THEN
              /* Actualiza OID_DIVISA en periodos */
                UPDATE SAPR_TPERIODO
                SET OID_DIVISA = var$oid_divisa_defecto,
                  GMT_MODIFICACION = var$gmt_zero,
                  DES_USUARIO_MODIFICACION = par$cod_usuario
                WHERE OID_MAQUINA = par$amaq_oid_maquina(idxx) 
                AND OID_PLANIFICACION IN (SELECT OID_PLANIFICACION FROM SAPR_TPLANIFICACION WHERE OID_PLANIFICACION = par$oid_planificacion  AND BOL_PERIODO_DIVISA ='1')
                AND OID_DIVISA IS NULL;
              END IF;

            END IF;
        END IF;
      END LOOP;
    END IF;

    /* Borrar todos los registros (logico) */
     SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'BORRA LOGICAMENTE LAS MAQUINAS QUE PERTENECIAN A LA PLANIFICACION Y YA NO SON PARTE DE LA MISMA',
          par$cod_identificador   => '');
    -- BORRA LOGICAMENTE LAS MAQUINAS QUE PERTENECIAN A LA PLANIFICACION Y YA NO SON PARTE DE LA MISMA
    UPDATE SAPR_TPLANXMAQUINA
      SET BOL_ACTIVO               = 0,
          GMT_MODIFICACION         = var$gmt_zero,
          DES_USUARIO_MODIFICACION = par$cod_usuario
      WHERE OID_PLANIFICACION = par$oid_planificacion
        AND BOL_ACTIVO = 1
        AND OID_MAQUINA NOT IN (SELECT OID_CAMPO1 FROM  SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_MAQUINA' );
    par$updates := par$updates + sql%rowcount;

    -- BORRA LOGICAMENTE LAS MAQUINAS QUE PERTENECIAN A OTRA PLANIFICACION, Y AHORA SON PARTE DE LA ENVIADA POR PARAMETRO
    SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'BORRA LOGICAMENTE LAS MAQUINAS QUE PERTENECIAN A OTRA PLANIFICACION, Y AHORA SON PARTE DE LA ENVIADA POR PARAMETRO',
          par$cod_identificador   => '');

    UPDATE SAPR_TPLANXMAQUINA
       SET bol_activo               = 0,
           gmt_modificacion         = var$gmt_zero,
           des_usuario_modificacion = par$cod_usuario
     WHERE oid_planificacion <> par$oid_planificacion
           AND bol_activo = 1
           AND oid_maquina in (select OID_CAMPO1 from  SAPR_GTT_TAUXILIAR where COD_CALIFICADOR = 'OID_MAQUINA' );
               par$updates := par$updates + sql%rowcount;


    IF par$amaq_oid_maquina is not null and not gepr_putilidades_###VERSION###.farray_vacio_oid(par$array => par$amaq_oid_maquina) then

      /* Relacionar */
      FOR idx IN par$amaq_oid_maquina.first .. par$amaq_oid_maquina.last LOOP

        SELECT count(1)
          INTO var$existe2
          FROM SAPR_GTT_TAUXILIAR AX
        WHERE AX.OID_CAMPO1 =  par$amaq_oid_maquina(idx)
          AND AX.COD_CALIFICADOR = 'OID_MAQUINA' 
          AND AX.COD_CAMPO2  = 1
          AND ROWNUM < 2;

        /*Maquina Nova*/
        IF var$existe2 = 0 THEN

          -- Si la fecha de inicio de la planificación es a futuro, indicamos la fecha a futuro, caso contrario aplicamos la fecha/hora actual (fyh_cambio) 
          INSERT INTO SAPR_TPLANXMAQUINA
            (OID_PLANXMAQUINA,
            OID_PLANIFICACION,
            OID_MAQUINA,
            OID_SECTOR,
            FYH_VIGENCIA_INICIO,
            FYH_VIGENCIA_FIN,
            FYH_ULTIMO_PERIODO,
            BOL_ACTIVO,
            GMT_CREACION,
            DES_USUARIO_CREACION,
            GMT_MODIFICACION,
            DES_USUARIO_MODIFICACION)
          VALUES
            (sys_guid(),
            par$oid_planificacion,
            par$amaq_oid_maquina(idx),
            (SELECT OID_SECTOR
                FROM SAPR_TMAQUINA
              WHERE OID_MAQUINA = par$amaq_oid_maquina(idx)),
            CASE WHEN par$fyh_vigencia_inicio > var$_fyh_cambio THEN par$fyh_vigencia_inicio ELSE var$_fyh_cambio END,
            NULL,
            NULL,
            1,
            var$gmt_zero,
            par$cod_usuario,
            var$gmt_zero,
            par$cod_usuario);
            par$inserts := par$inserts + sql%rowcount;

        ELSE /*maquina ja existente na planificação*/
          -- SI CAMBIA EL HORARIO DE LA PLANIFICACION Y/O SE CAMBIA EL HORARIO DE LA PROGRAMACION (CORTES)
          -- SE BORRAN LOS PERIODOS DE ACREDITACION ABIERTOS FUTUROS (LUEGO SERAN CREADOS POR SWITCH)
          IF par$bol_cambio_program = 1 OR par$bol_cambio_horario = 1 THEN
            /* INSERT  GTTAUXILIAR COD CALIFICADOR DEL_PERIODO*/     
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR )                    
            SELECT PERI.OID_MAQUINA, PERI.OID_PERIODO, TO_CHAR(PERI.FYH_INICIO, 'DD/MM/YYYY HH24:MI:SS'), 'DEL_PERIODO'
            FROM SAPR_TPERIODO PERI 
              INNER JOIN SAPR_TESTADO_PERIODO EST_PER ON PERI.OID_ESTADO_PERIODO = EST_PER.OID_ESTADO_PERIODO
              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
                AND TIPE.COD_TIPO_PERIODO = 'AC'
            WHERE PERI.OID_MAQUINA = par$amaq_oid_maquina(idx) 
              AND PERI.OID_PLANIFICACION    = par$oid_planificacion
              AND PERI.FYH_INICIO > var$_fyh_cambio
              AND EST_PER.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND PERI.oid_acreditacion IS NULL;
            par$inserts := par$inserts + sql%rowcount;

          END IF;

          -- SI CAMBIA EL HORARIO DE LA PROGRAMACION (CORTES)
          IF par$bol_cambio_program = 1 THEN
            /* MODIFICAMOS LA FECHA FIN DEL PERIODO DE ACREDITACION VIGENTE AL MOMENTO DEL CAMBIO DE HORARIO */
            UPDATE SAPR_TPERIODO
            SET FYH_FIN = var$_fyh_cambio,
              GMT_MODIFICACION = var$gmt_zero,
              DES_USUARIO_MODIFICACION = par$cod_usuario
            WHERE OID_MAQUINA = par$amaq_oid_maquina(idx) 
            AND OID_PLANIFICACION = par$oid_planificacion
            AND var$_fyh_cambio BETWEEN FYH_INICIO AND FYH_FIN
            AND OID_ACREDITACION IS NULL
            AND OID_ESTADO_PERIODO IN (SELECT OID_ESTADO_PERIODO FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB'))
            AND OID_TIPO_PERIODO = (SELECT OID_TIPO_PERIODO FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'AC')
            ;

          END IF;

          -- SI CAMBIA EL HORARIO DE LA PLANIFICACION SE GUARDA EL HISTORICO
          IF par$bol_cambio_horario = 1 THEN   

            -- Recuperamos los valores actuales de la PLANXMAQUINA
            SELECT FYH_VIGENCIA_INICIO, FYH_VIGENCIA_FIN
              INTO var$_fyh_vigencia_inicio_maq, var$_fyh_vigencia_fin_maq
            FROM SAPR_TPLANXMAQUINA
            WHERE OID_PLANIFICACION = par$oid_planificacion
              AND OID_MAQUINA = par$amaq_oid_maquina(idx)
              AND BOL_ACTIVO = 1;

            UPDATE SAPR_TPLANXMAQUINA
            SET BOL_ACTIVO = 0,                      
                GMT_MODIFICACION         = var$gmt_zero,
                DES_USUARIO_MODIFICACION = par$cod_usuario
            WHERE
              OID_PLANIFICACION = par$oid_planificacion
              AND OID_MAQUINA = par$amaq_oid_maquina(idx)
              AND BOL_ACTIVO = 1;
              par$updates := par$updates + sql%rowcount;
                
            -- VALIDAR FECHAS INICIO Y VIGENCIA
            -- SI LA FYH_INICIO_PLANIFICACION > FYH_INICIO_MAQUINA ENTONCES FYH_INICIO_PLANIFICACION SINO FYH_INICIO_MAQUINA
            -- SI FYH_FIN_MAQUINA ES NULA ENTONCES SIGUE NULA, LA FYH_FIN_PLANIFICACION < FYH_FIN_MAQUINA ENTONCES FYH_FIN_PLANIFICACION SINO FYH_FIN_MAQUINA
            INSERT INTO SAPR_TPLANXMAQUINA
                (OID_PLANXMAQUINA,
                  OID_PLANIFICACION,
                  OID_MAQUINA,
                  OID_SECTOR,
                  FYH_VIGENCIA_INICIO,
                  FYH_VIGENCIA_FIN,
                  FYH_ULTIMO_PERIODO,
                  BOL_ACTIVO,
                  GMT_CREACION,
                  DES_USUARIO_CREACION,
                  GMT_MODIFICACION,
                  DES_USUARIO_MODIFICACION)
            VALUES
                (sys_guid(),
                  par$oid_planificacion,
                  par$amaq_oid_maquina(idx),
                  (SELECT OID_SECTOR
                    FROM SAPR_TMAQUINA
                    WHERE OID_MAQUINA = par$amaq_oid_maquina(idx)),
                  CASE WHEN par$fyh_vigencia_inicio > var$_fyh_vigencia_inicio_maq THEN  par$fyh_vigencia_inicio ELSE var$_fyh_vigencia_inicio_maq END,
                  CASE WHEN var$_fyh_vigencia_fin_maq IS NOT NULL AND var$_fyh_vigencia_fin_maq >  par$fyh_vigencia_fin THEN par$fyh_vigencia_fin
                    ELSE var$_fyh_vigencia_fin_maq END,
                  NULL,
                  1,
                  var$gmt_zero,
                  par$cod_usuario,
                  var$gmt_zero,
                  par$cod_usuario);
            par$inserts := par$inserts + sql%rowcount;
              
          END IF;
        END IF;
      END LOOP;
    END IF;
    /*  Inserta en tabla auxiliar los períodos correspondientes a las máquinas
          de la planificación que ya no están en la misma (borradas)
    */
     SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inserta en tabla auxiliar los períodos correspondientes a las máquinas
          de la planificación que ya no están en la misma (borradas)',
          par$cod_identificador   => '');

    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR )
            SELECT P.OID_MAQUINA, P.OID_PERIODO, TO_CHAR (P.FYH_INICIO, 'DD/MM/YYYY HH24:MI:SS'), 'DEL_PERIODO'
                FROM SAPR_TPERIODO P
            INNER JOIN SAPR_TESTADO_PERIODO E
            ON P.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO 
            INNER JOIN SAPR_TPLANXMAQUINA PM ON PM.OID_PLANIFICACION = P.OID_PLANIFICACION
            AND PM.OID_MAQUINA = P.OID_MAQUINA
            WHERE P.OID_PLANIFICACION = par$oid_planificacion
            AND E.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB', 'EC')
            AND PM.BOL_ACTIVO = 0
            AND P.OID_MAQUINA not in (select OID_CAMPO1 from  SAPR_GTT_TAUXILIAR where COD_CALIFICADOR = 'OID_MAQUINA' );

    /*  Inserta en tabla auxiliar los períodos correspondientes a las máquinas
        Que no existen en la planificación actual (nuevas) pero 
        podrían estar en otra planificación
    */
    SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Inserta en tabla auxiliar los períodos correspondientes a las máquinas Que no existen en la planificación actual (nuevas) pero  podrían estar en otra planificación',
          par$cod_identificador   => '');
    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR )
        Select PERI.OID_MAQUINA, PERI.OID_PERIODO , TO_CHAR (PERI.FYH_INICIO, 'DD/MM/YYYY HH24:MI:SS'), 'DEL_PERIODO'
        from SAPR_TPERIODO PERI
            INNER JOIN SAPR_TESTADO_PERIODO TEPE
              ON PERI.OID_ESTADO_PERIODO = TEPE.OID_ESTADO_PERIODO
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
            INNER JOIN SAPR_GTT_TAUXILIAR AUX
            ON AUX.COD_CALIFICADOR = 'OID_MAQUINA' AND AUX.OID_CAMPO1 = PERI.OID_MAQUINA
            WHERE
            AUX.COD_CAMPO2 = 0
            AND TEPE.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB', 'EC') 
            AND PERI.OID_PLANIFICACION <> par$oid_planificacion;

    /*Borrar periodos abiertos */
     SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'Borrar periodos abiertos',
          par$cod_identificador   => '');
    DELETE FROM SAPR_TPERIODOXDOCUMENTO PD
      WHERE PD.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes := sql%rowcount;

    DELETE FROM SAPR_TCALCULO_EFECTIVO CE
      WHERE CE.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes := par$deletes + sql%rowcount;

    DELETE FROM SAPR_TCALCULO_MEDIO_PAGO CMP
      WHERE CMP.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes :=  par$deletes +sql%rowcount;
      
    DELETE FROM SAPR_TPERIODO_RELACION RELA
      WHERE RELA.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes :=  par$deletes +sql%rowcount;

    DELETE FROM SAPR_TPERIODO_RELACION RELA
      WHERE RELA.OID_PERIODO_RELACIONADO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes :=  par$deletes +sql%rowcount;

    DELETE FROM SAPR_TPERIODO PR
      WHERE PR.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
      par$deletes :=  par$deletes + sql%rowcount;
    /*Fin borrar periodos */

    -- ACTUALIZAR LA FECHA DEL ÚLTIMO PERIODO
    -- SI MAX(FYH_ULTIMO_PERIODO DE LA MAQUINA EN ESTA PLANIFICACION) > FYH_VIGENCIA_INCIO_MAQ = MAX(FYH_ULTIMO_PERIODO DE LA MAQUINA EN ESTA PLANIFICACION)
    -- SI NO FYH_VIGENCIA_MAQ
     SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,
          par$des_detalle     => 'ACTUALIZAR LA FECHA DEL ÚLTIMO PERIODO ',
          par$cod_identificador   => '');

    IF par$amaq_oid_maquina is not null and not gepr_putilidades_###VERSION###.farray_vacio_oid(par$array => par$amaq_oid_maquina) then
      FOR idx IN par$amaq_oid_maquina.first .. par$amaq_oid_maquina.last LOOP

        BEGIN
          SELECT FYH_VIGENCIA_INICIO
            INTO var$_fyh_vigencia_inicio_maq
            FROM SAPR_TPLANXMAQUINA
            WHERE OID_MAQUINA = par$amaq_oid_maquina(idx)
              AND OID_PLANIFICACION = par$oid_planificacion
              AND BOL_ACTIVO = 1
            ;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$_fyh_vigencia_inicio_maq := NULL;
        END;
        
         DBMS_OUTPUT.PUT_LINE('Maquina de OID_MAQUINA: ' || par$amaq_oid_maquina(idx) || ', FYH_VIGENCIA_INICIO: ' || TO_CHAR(var$_fyh_vigencia_inicio_maq, 'DD-MM-YYYY HH24:MI:SS'));

        IF var$_fyh_vigencia_inicio_maq IS NOT NULL THEN
          SELECT MAX(FYH_FIN)
          INTO var$_fyh_ultimo_periodo
          FROM SAPR_TPERIODO P
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO 
                                                  AND TIPE.COD_TIPO_PERIODO = 'AC'
            INNER JOIN SAPR_TESTADO_PERIODO E ON P.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
          WHERE P.OID_MAQUINA = par$amaq_oid_maquina(idx)
            AND P.OID_PLANIFICACION = par$oid_planificacion;
          
          DBMS_OUTPUT.PUT_LINE('Maquina de OID_MAQUINA: ' || par$amaq_oid_maquina(idx) || ', FYH_ULTIMO_PERIODO: ' || TO_CHAR(var$_fyh_ultimo_periodo, 'DD-MM-YYYY HH24:MI:SS'));

          UPDATE SAPR_TPLANXMAQUINA
              SET FYH_ULTIMO_PERIODO = CASE WHEN var$_fyh_ultimo_periodo IS NOT NULL AND var$_fyh_ultimo_periodo > var$_fyh_vigencia_inicio_maq  THEN var$_fyh_ultimo_periodo ELSE  NULL END,                      
              GMT_MODIFICACION         = var$gmt_zero,
              DES_USUARIO_MODIFICACION = par$cod_usuario
          WHERE
              OID_PLANIFICACION = par$oid_planificacion
              AND OID_MAQUINA = par$amaq_oid_maquina(idx)
              AND BOL_ACTIVO = 1; 
        END IF;
      END LOOP;
    END IF;

  END supd_planxmaquinas_fvc;

  /* Insert/Update PlanXMaquinas */
  PROCEDURE sdel_planificacion(par$oid_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$amaq_oid_maquina  IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$info_ejecucion    IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$cod_ejecucion     OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /* variables locales */
    var$inicio       TIMESTAMP;
    var$updates      gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes      gepr_pcomon_###VERSION###.tipo$nel_;
    var$registros    gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts      gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges       gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects      gepr_pcomon_###VERSION###.tipo$nel_;
    var$err          varchar2(4000);
    var$fim_vigencia gepr_pcomon_###VERSION###.tipo$fyh_;

    const$nombre_func    CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sdel_planificacion';
    const$estado_periodo CONSTANT gepr_pcomon_###VERSION###.tipo$est_ := 'AB';

  BEGIN

    var$inicio    := SYSTIMESTAMP;
    var$updates   := 0;
    var$deletes   := 0;
    var$registros := 0;
    var$inserts   := 0;
    var$selects   := 0;

    var$fim_vigencia := CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE);
    var$err          := par$oid_planificacion;

    /* Borrar todos los registros (logico) SAPR_TPLANIFICACION*/
    UPDATE SAPR_TPLANIFICACION
       SET bol_activo = 0
     WHERE oid_planificacion = par$oid_planificacion;
    var$updates := var$updates + sql%rowcount;

    /* Borrar todos los registros (logico) SAPR_TPLANXMAQUINA */

    UPDATE SAPR_TPLANXMAQUINA
       SET bol_activo = 0, fyh_vigencia_fin = var$fim_vigencia
     WHERE oid_planificacion = par$oid_planificacion
       and bol_activo = 1;
    var$updates := var$updates + sql%rowcount;

    /* Para cada máquina deletar periodos abertos */
    FOR idx IN par$amaq_oid_maquina.first .. par$amaq_oid_maquina.last LOOP


sdel_planxcan_maqu(par$oid_plan_nueva   => null,
                             par$oid_maquina     => par$amaq_oid_maquina(idx),
                             par$cod_usuario     => par$cod_usuario); 
                             
      /*Verificar si hay periodos abiertos, borrar todos los registros*/

      DELETE FROM SAPR_TPERIODO PR
       WHERE PR.OID_PERIODO IN
             (SELECT P.OID_PERIODO
                FROM SAPR_TPERIODO P
                  INNER JOIN SAPR_TESTADO_PERIODO E
                    ON P.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
                  INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO 
                      AND TIPE.COD_TIPO_PERIODO = 'AC'
               WHERE P.OID_MAQUINA = par$amaq_oid_maquina(idx)
                 AND E.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND P.oid_acreditacion IS NULL
                 AND P.FYH_INICIO >= var$fim_vigencia);

      var$deletes := sql%rowcount;

    END LOOP;

    COMMIT;
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

  END sdel_planificacion;

  /* Insert/Update PlanXCanales */
  PROCEDURE supd_planxcanales(par$oid_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$apto_nel_index        IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$apto_oid_pto_servicio  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$acan_nel_index        IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$acan_oid_canal    IN gepr_pcomon_###VERSION###.tipo$oids_,      
                              par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$selects           OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /* variables locales */
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$existe   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$nel_index   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    par$inserts := 0;
    par$selects := 0;
    par$updates := 0;
    IF ( par$oid_maquina IS NULL) THEN
      
      /* Borrar todos los registros (logico) */
      UPDATE SAPR_TPLANXCANAL
         SET bol_activo               = 0,
             gmt_modificacion         = var$gmt_zero,
             des_usuario_modificacion = par$cod_usuario
      WHERE oid_planificacion = par$oid_planificacion 
             AND oid_maquina IS NULL 
             AND oid_pto_servicio IS NULL;
      par$updates := par$updates + sql%rowcount;

      /* Relacionar */
      FOR idx IN par$acan_oid_canal.first .. par$acan_oid_canal.last LOOP

        /* Verifica si es Update */
        SELECT COUNT(1)
          INTO var$existe
          FROM SAPR_TPLANXCANAL PM
         WHERE PM.OID_PLANIFICACION = par$oid_planificacion
           AND PM.OID_CANAL = par$acan_oid_canal(idx)
           AND oid_maquina IS NULL 
           AND oid_pto_servicio IS NULL
           AND ROWNUM < 2;
        par$selects := par$selects + sql%rowcount;

        IF var$existe > 0 THEN

          UPDATE SAPR_TPLANXCANAL PM
             SET bol_activo               = 1,
                 gmt_modificacion         = var$gmt_zero,
                 des_usuario_modificacion = par$cod_usuario
           WHERE PM.OID_PLANIFICACION = par$oid_planificacion
             AND oid_maquina IS NULL 
             AND oid_pto_servicio IS NULL
             AND PM.OID_CANAL = par$acan_oid_canal(idx);
          par$updates := par$updates + sql%rowcount;

        ELSE

          INSERT INTO SAPR_TPLANXCANAL
            (oid_planxcanal,
             oid_planificacion,
             oid_canal,
             bol_activo,
             gmt_creacion,
             des_usuario_creacion,
             gmt_modificacion,
             des_usuario_modificacion)
          VALUES
            (sys_guid(),
             par$oid_planificacion,
             par$acan_oid_canal(idx),
             1,
             var$gmt_zero,
             par$cod_usuario,
             var$gmt_zero,
             par$cod_usuario);
          par$inserts := par$inserts + sql%rowcount;
        END IF;

      END LOOP;
    ELSE
    
      
      /* Borrar todos los registros (logico) */
      UPDATE SAPR_TPLANXCANAL
         SET bol_activo               = 0,
             COD_AGRUPADOR = null,
             gmt_modificacion         = var$gmt_zero,
             des_usuario_modificacion = par$cod_usuario
       WHERE oid_planificacion = par$oid_planificacion
             AND oid_maquina = par$oid_maquina
             AND bol_activo = 1;
      par$updates := par$updates + sql%rowcount;
     
        IF  par$apto_nel_index IS NOT NULL AND  par$apto_nel_index.COUNT > 0 THEN
            FOR idx_pto IN par$apto_nel_index.first .. par$apto_nel_index.last LOOP



            /* Relacionar */
                FOR idx IN par$acan_nel_index.first .. par$acan_nel_index.last LOOP

                    IF par$apto_nel_index(idx_pto) = par$acan_nel_index(idx) THEN 
                    
                            /* Verifica si es Update */
                            SELECT COUNT(1)
                            INTO var$existe
                            FROM SAPR_TPLANXCANAL PM
                            WHERE PM.OID_PLANIFICACION = par$oid_planificacion
                            AND PM.OID_CANAL = par$acan_oid_canal(idx)
                            AND par$oid_maquina = PM.oid_maquina
                              AND (par$apto_oid_pto_servicio(idx_pto) = oid_pto_servicio 
                                OR (par$apto_oid_pto_servicio(idx_pto) IS NULL AND  oid_pto_servicio IS NULL))
                            AND ROWNUM < 2;
                            par$selects := par$selects + sql%rowcount;

                            IF var$existe > 0 THEN
                            dbms_output.put_line('1');
                            dbms_output.put_line('par$apto_nel_index(idx_pto):' || par$apto_nel_index(idx_pto));
                            UPDATE SAPR_TPLANXCANAL PM
                                SET bol_activo               = 1,
                                    COD_AGRUPADOR = par$apto_nel_index(idx_pto),
                                    gmt_modificacion         = var$gmt_zero,
                                    des_usuario_modificacion = par$cod_usuario
                            WHERE PM.OID_PLANIFICACION = par$oid_planificacion
                                AND PM.OID_CANAL = par$acan_oid_canal(idx)
                                AND par$oid_maquina = par$oid_maquina
                                AND (par$apto_oid_pto_servicio(idx_pto) = oid_pto_servicio 
                                OR (par$apto_oid_pto_servicio(idx_pto) IS NULL AND  oid_pto_servicio IS NULL));
                            par$updates := par$updates + sql%rowcount;

                            ELSE
                            dbms_output.put_line('2');
                            INSERT INTO SAPR_TPLANXCANAL
                                (oid_planxcanal,
                                oid_planificacion,
                                oid_canal,
                                oid_maquina,
                                oid_pto_servicio,
                                COD_AGRUPADOR,
                                bol_activo,
                                gmt_creacion,
                                des_usuario_creacion,
                                gmt_modificacion,
                                des_usuario_modificacion)
                            VALUES
                                (sys_guid(),
                                par$oid_planificacion,
                                par$acan_oid_canal(idx),             
                                par$oid_maquina,
                                par$apto_oid_pto_servicio(idx_pto),
                                par$apto_nel_index(idx_pto),
                                1,
                                var$gmt_zero,
                                par$cod_usuario,
                                var$gmt_zero,
                                par$cod_usuario);
                            par$inserts := par$inserts + sql%rowcount;
                            END IF;
                    END IF;
                END LOOP;

            END LOOP;
        END IF;
            
    END IF;  
  END supd_planxcanales;
  
  /* Insert/Update PlanXCanales */
  PROCEDURE supd_planxsubcanales( par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$apto_nel_index        IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$apto_oid_pto_servicio  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$ascan_nel_index        IN gepr_pcomon_###VERSION###.tipo$nels_,
                                  par$ascan_oid_subcanal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                  par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$updates             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /* variables locales */
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$existe   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$oid_planxcanal   gepr_pcomon_###VERSION###.tipo$oid_  ;

  BEGIN

    par$inserts := 0;
    par$selects := 0;
    par$updates := 0;

    IF par$oid_maquina is null then
          /* Borrar todos los registros (logico) */
          UPDATE SAPR_TPLANXSUBCANAL
             SET bol_activo               = 0,
                 gmt_modificacion         = var$gmt_zero,
                 des_usuario_modificacion = par$cod_usuario
           WHERE BOL_ACTIVO = 1 AND
           OID_PLANXCANAL IN (SELECT OID_PLANXCANAL FROM SAPR_TPLANXCANAL CAN 
           WHERE CAN.OID_PLANIFICACION = par$oid_planificacion) and oid_maquina is null and oid_pto_servicio is null;
          par$updates := par$updates + sql%rowcount;



       IF par$ascan_oid_subcanal IS NOT NULL AND par$ascan_oid_subcanal.COUNT > 0  THEN
          /* LOOP EN TODOS par$ascan_oid_subcanal VERIFICAR SE EXISTE, SENÃO INSERIR*/
          FOR idx IN par$ascan_oid_subcanal.first .. par$ascan_oid_subcanal.last LOOP
         DBMS_OUTPUT.put_line('1');
              IF (par$ascan_oid_subcanal(idx) IS NOT null ) THEN
                   DBMS_OUTPUT.put_line('2');
                 SELECT COUNT(1)
                  INTO var$existe
                  FROM SAPR_TPLANXSUBCANAL PS
                  INNER JOIN SAPR_TPLANXCANAL PC ON PS.OID_PLANXCANAL = PC.OID_PLANXCANAL        
                 WHERE PC.OID_PLANIFICACION = par$oid_planificacion
                   AND PS.OID_SUBCANAL = par$ascan_oid_subcanal(idx)
                   and PS.oid_maquina is null and PS.oid_pto_servicio is null
                   AND ROWNUM < 2;
                   
                   SELECT MAX(PC.OID_PLANXCANAL)                        
                          INTO var$oid_planxcanal
                   FROM SAPR_TPLANXCANAL PC 
                   INNER JOIN GEPR_TSUBCANAL SC ON PC.OID_CANAL = SC.OID_CANAL
                   WHERE PC.OID_PLANIFICACION = par$oid_planificacion           
                         AND SC.OID_SUBCANAL = par$ascan_oid_subcanal(idx)
                         and oid_maquina is null and oid_pto_servicio is null
                         AND ROWNUM < 2;
                         
                         
                         dbms_output.put_line('par$oid_planificacion ' || par$oid_planificacion);
                         dbms_output.put_line(' par$ascan_oid_subcanal(idx) ' ||  par$ascan_oid_subcanal(idx));
                  IF var$existe > 0 THEN
                       DBMS_OUTPUT.put_line('UPDATE');
                   /*BUSCAR var$oid_planxcanal */
                
                  
                    UPDATE SAPR_TPLANXSUBCANAL PS
                     SET bol_activo               = 1,
                         gmt_modificacion         = var$gmt_zero,
                         des_usuario_modificacion = par$cod_usuario
                   WHERE PS.OID_PLANXCANAL = var$oid_planxcanal
                     AND PS.OID_SUBCANAL = par$ascan_oid_subcanal(idx)
                     and oid_maquina is null and oid_pto_servicio is null;
                 
                  ELSE        
                            DBMS_OUTPUT.put_line('INSERT');
                  
                   INSERT INTO SAPR_TPLANXSUBCANAL
                    (oid_planxsubcanal,
                     oid_planxcanal,
                     oid_subcanal,
                     bol_activo,
                     gmt_creacion,
                     des_usuario_creacion,
                     gmt_modificacion,
                     des_usuario_modificacion
                     )
                  VALUES
                    (sys_guid(),
                     var$oid_planxcanal,
                     par$ascan_oid_subcanal(idx),
                     1,
                     var$gmt_zero,
                     par$cod_usuario,
                     var$gmt_zero,
                     par$cod_usuario);
                par$inserts := par$inserts + sql%rowcount;
                
                    
                 END IF;
               
               END IF;
          END LOOP;
          
          
          
          END IF;











    ELSE
          
           IF  par$apto_nel_index IS NOT NULL AND  par$apto_nel_index.COUNT > 0 THEN
            FOR idx_pto IN par$apto_nel_index.first .. par$apto_nel_index.last LOOP 

                /* Borrar todos los registros (logico) */
                UPDATE SAPR_TPLANXSUBCANAL
                  SET bol_activo               = 0,
                      gmt_modificacion         = var$gmt_zero,
                       des_usuario_modificacion = par$cod_usuario
                  WHERE BOL_ACTIVO = 1 AND
                        OID_PLANXCANAL IN (SELECT OID_PLANXCANAL FROM SAPR_TPLANXCANAL CAN 
                                          WHERE CAN.OID_PLANIFICACION = par$oid_planificacion)
                                                and oid_maquina = par$oid_maquina 
                                                and (par$apto_oid_pto_servicio(idx_pto) = oid_pto_servicio or oid_pto_servicio is null) ;
                par$updates := par$updates + sql%rowcount;
            END LOOP;
        END IF;


        IF  par$apto_nel_index IS NOT NULL AND  par$apto_nel_index.COUNT > 0 THEN
            FOR idx_pto IN par$apto_nel_index.first .. par$apto_nel_index.last LOOP 
                IF par$ascan_oid_subcanal IS NOT NULL AND par$ascan_oid_subcanal.COUNT > 0  THEN
                    /* LOOP EN TODOS par$ascan_oid_subcanal VERIFICAR SE EXISTE, SENÃO INSERIR*/
                    FOR idx IN par$ascan_oid_subcanal.first .. par$ascan_oid_subcanal.last LOOP
                        IF par$apto_nel_index(idx_pto) = par$ascan_nel_index(idx) THEN

                            IF (par$ascan_oid_subcanal(idx) IS NOT null ) THEN
                                SELECT COUNT(1)
                                    INTO var$existe
                                FROM SAPR_TPLANXSUBCANAL PS
                                INNER JOIN SAPR_TPLANXCANAL PC ON PS.OID_PLANXCANAL = PC.OID_PLANXCANAL        
                                WHERE PC.OID_PLANIFICACION = par$oid_planificacion
                                    AND PS.OID_SUBCANAL = par$ascan_oid_subcanal(idx)
                                    and PS.oid_maquina = par$oid_maquina 
                                    and (par$apto_oid_pto_servicio(idx_pto) = PS.oid_pto_servicio or 
                                        (par$apto_oid_pto_servicio(idx_pto) is null and PS.oid_pto_servicio is null))  
                                    AND ROWNUM < 2;
                                    
                                SELECT MAX(PC.OID_PLANXCANAL)                        
                                    INTO var$oid_planxcanal
                                FROM SAPR_TPLANXCANAL PC 
                                INNER JOIN GEPR_TSUBCANAL SC ON PC.OID_CANAL = SC.OID_CANAL
                                WHERE PC.OID_PLANIFICACION = par$oid_planificacion           
                                    AND SC.OID_SUBCANAL = par$ascan_oid_subcanal(idx)                         
                                    and oid_maquina = par$oid_maquina 
                                    and (par$apto_oid_pto_servicio(idx_pto) = oid_pto_servicio or 
                                        (par$apto_oid_pto_servicio(idx_pto) is null and oid_pto_servicio is null))  
                                    AND ROWNUM < 2;                         
                                            
                                IF var$existe > 0 THEN                                    
                                    UPDATE SAPR_TPLANXSUBCANAL PS
                                        SET bol_activo               = 1,
                                            gmt_modificacion         = var$gmt_zero,
                                            des_usuario_modificacion = par$cod_usuario
                                    WHERE PS.OID_PLANXCANAL = var$oid_planxcanal                   
                                            and oid_maquina = par$oid_maquina and (par$apto_oid_pto_servicio(idx_pto) = oid_pto_servicio or 
                                            (par$apto_oid_pto_servicio(idx_pto) is null and oid_pto_servicio is null))  
                                            AND PS.OID_SUBCANAL = par$ascan_oid_subcanal(idx);
                                    
                                ELSE
                                    INSERT INTO SAPR_TPLANXSUBCANAL
                                        (oid_planxsubcanal,
                                        oid_planxcanal,
                                        oid_subcanal,
                                        oid_maquina,
                                        oid_pto_servicio,
                                        bol_activo,
                                        gmt_creacion,
                                        des_usuario_creacion,
                                        gmt_modificacion,
                                        des_usuario_modificacion)
                                    VALUES
                                        (sys_guid(),
                                        var$oid_planxcanal,
                                        par$ascan_oid_subcanal(idx),
                                        par$oid_maquina,
                                        par$apto_oid_pto_servicio(idx_pto),
                                        1,
                                        var$gmt_zero,
                                        par$cod_usuario,
                                        var$gmt_zero,
                                        par$cod_usuario);
                                    par$inserts := par$inserts + sql%rowcount;                                   
                                        
                                END IF;                                
                            END IF;
                        END IF;
                    END LOOP;                    
                END IF;
            END LOOP;
        END IF;





    END IF;

  END supd_planxsubcanales;

  /* Insert Programacion */
  PROCEDURE sins_programacion(par$oid_planificacion       IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$aprog_num_dia_fin       IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aprog_fyh_fechahora_fin IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$deletes                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$inserts                 OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /* variables locales */
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;

    var$aprog_num_dia_inicio      gepr_pcomon_###VERSION###.tipo$nels_;
    var$aprog_fyh_fechahorainicio gepr_pcomon_###VERSION###.tipo$fyhs_;

  BEGIN

    par$inserts := 0;

    /* Borrar todos los registros */
    DELETE FROM SAPR_TPLANXPROGRAMACION PP
     WHERE PP.OID_PLANIFICACION = par$oid_planificacion;
    par$deletes := sql%rowcount;

    var$aprog_num_dia_inicio(0) := null;
    var$aprog_fyh_fechahorainicio(0) := null;

    /* Calcular fecha inicio */
    var$aprog_num_dia_inicio(var$aprog_num_dia_inicio.COUNT) := par$aprog_num_dia_fin(par$aprog_num_dia_fin.COUNT);
    var$aprog_fyh_fechahorainicio(var$aprog_fyh_fechahorainicio.COUNT) := par$aprog_fyh_fechahora_fin(par$aprog_fyh_fechahora_fin.COUNT) + 1 /
                                                                          86400;

    FOR idx IN par$aprog_num_dia_fin.first + 1 .. par$aprog_num_dia_fin.last LOOP
      var$aprog_num_dia_inicio(var$aprog_num_dia_inicio.COUNT) := par$aprog_num_dia_fin(idx - 1);
      var$aprog_fyh_fechahorainicio(var$aprog_fyh_fechahorainicio.COUNT) := par$aprog_fyh_fechahora_fin(idx - 1) + 1 /
                                                                            86400;
    END LOOP;

    /* Grabar */
    FOR idx IN par$aprog_num_dia_fin.first .. par$aprog_num_dia_fin.last LOOP

      INSERT INTO SAPR_TPLANXPROGRAMACION
        (oid_planxprogramacion,
         oid_planificacion,
         nec_dia_inicio,
         fyh_hora_inicio,
         nec_dia_fin,
         fyh_hora_fin,
         gmt_creacion,
         des_usuario_creacion,
         gmt_modificacion,
         des_usuario_modificacion)
      VALUES
        (sys_guid(),
         par$oid_planificacion,
         var$aprog_num_dia_inicio(idx),
         var$aprog_fyh_fechahorainicio(idx),
         par$aprog_num_dia_fin(idx),
         par$aprog_fyh_fechahora_fin(idx),
         var$gmt_zero,
         par$cod_usuario,
         var$gmt_zero,
         par$cod_usuario);
      par$inserts := par$inserts + sql%rowcount;

    END LOOP;

  END sins_programacion;

  PROCEDURE supd_planxmaquina_fecha_incio(par$oid_maquina IN gepr_pcomon_###VERSION###.tipo$oid_) IS
  BEGIN
    UPDATE SAPR_TPLANXMAQUINA
       SET FYH_VIGENCIA_INICIO = sys_extract_utc(current_timestamp)
     WHERE OID_MAQUINA = par$oid_maquina
     AND BOL_ACTIVO = 1;
  END supd_planxmaquina_fecha_incio;



 PROCEDURE supd_maquinaxplan( par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$fyh_vigencia_inicio IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$fyh_vigencia_fin    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                               par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_) IS

    /* variables locales */
    var$gmt_zero             gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$_fyh_ultimo_periodo  gepr_pcomon_###VERSION###.tipo$fyh_;
    var$existe               gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    const$estado_periodo CONSTANT gepr_pcomon_###VERSION###.tipo$est_ := 'AB';
    var$oid_planificacion_old gepr_pcomon_###VERSION###.tipo$oid_ ;
    var$aux gepr_pcomon_###VERSION###.tipo$oid_ ;
  BEGIN
     
  IF par$oid_maquina IS NOT NULL AND par$oid_planificacion IS NOT NULL AND par$fyh_vigencia_inicio IS NOT NULL THEN
     SELECT count(AUX.COD_CAMPO2) into var$aux FROM SAPR_GTT_TAUXILIAR AUX WHERE  AUX.COD_CALIFICADOR = 'PTO_SERVICIO';          
      dbms_output.put_line('Metodo maqxpan 01: ' || var$aux);     
                                 
      SELECT COUNT(1)
          INTO var$existe
            FROM SAPR_TPLANXMAQUINA PM
         WHERE PM.OID_MAQUINA = par$oid_maquina
           AND  PM.BOL_ACTIVO = 1
                  AND ROWNUM < 2;
                   
     SELECT count(AUX.COD_CAMPO2) into var$aux FROM SAPR_GTT_TAUXILIAR AUX WHERE  AUX.COD_CALIFICADOR = 'PTO_SERVICIO';          
      dbms_output.put_line('Metodo maqxplan 01: ' || var$aux);   
                  
       IF var$existe = 1 THEN
          SELECT PM.OID_PLANIFICACION
              INTO var$oid_planificacion_old
                FROM SAPR_TPLANXMAQUINA PM
             WHERE PM.OID_MAQUINA = par$oid_maquina
               AND PM.BOL_ACTIVO = 1
               AND ROWNUM < 2;    
       END IF;
                                    dbms_output.put_line('a3');   
      dbms_output.put_line('plan: ');    
      dbms_output.put_line(var$oid_planificacion_old);    
      dbms_output.put_line(par$oid_planificacion); 
         
       IF(var$existe = 0  OR var$oid_planificacion_old <> par$oid_planificacion) THEN
       /*NOVO CODIGO DE PLANIFICAÇÃO*/
          IF  var$existe = 1 THEN
                                     dbms_output.put_line('a4');
           UPDATE SAPR_TPLANXMAQUINA
           SET bol_activo               = 0,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$cod_usuario
               WHERE oid_planificacion <> par$oid_planificacion
                   AND bol_activo = 1
                   AND oid_maquina = par$oid_maquina;
                                        dbms_output.put_line('a5');                     
             INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR )
                   Select P.OID_MAQUINA, P.OID_PERIODO , 'DEL_PERIODO'
                   FROM SAPR_TPERIODO P
              INNER JOIN SAPR_TESTADO_PERIODO E
                ON P.OID_ESTADO_PERIODO = E.OID_ESTADO_PERIODO
              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO 
                    AND TIPE.COD_TIPO_PERIODO = 'AC'
              INNER JOIN SAPR_TPLANXMAQUINA PM
                ON PM.OID_PLANIFICACION = P.OID_PLANIFICACION
               AND PM.OID_MAQUINA = P.OID_MAQUINA
             WHERE P.OID_PLANIFICACION = par$oid_planificacion
               AND E.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND P.oid_acreditacion IS NULL
               AND PM.BOL_ACTIVO = 0
               AND P.OID_MAQUINA = par$oid_maquina;
               
                DELETE FROM SAPR_TPERIODOXDOCUMENTO PD
                 WHERE PD.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
                DELETE FROM SAPR_TCALCULO_EFECTIVO CE
                 WHERE CE.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
                DELETE FROM SAPR_TCALCULO_MEDIO_PAGO CMP
                 WHERE CMP.OID_PERIODO IN (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');
                DELETE FROM SAPR_TPERIODO PR
                 WHERE PR.OID_PERIODO IN
                       (SELECT COD_CAMPO2 FROM SAPR_GTT_TAUXILIAR AUX WHERE AUX.COD_CALIFICADOR = 'DEL_PERIODO');            
             END IF;
       /*Inserir registro novo*/
       
       
         INSERT INTO sapr_tplanxmaquina
          (oid_planxmaquina,
           oid_planificacion,
           oid_maquina,
           oid_sector,
           fyh_vigencia_inicio,
           fyh_vigencia_fin,
           bol_activo,
           gmt_creacion,
           des_usuario_creacion,
           gmt_modificacion,
           des_usuario_modificacion)
        VALUES
          (sys_guid(),
           par$oid_planificacion,
           par$oid_maquina,
           (SELECT OID_SECTOR
              FROM SAPR_TMAQUINA
             WHERE OID_MAQUINA = par$oid_maquina),
           par$fyh_vigencia_inicio,
           par$fyh_vigencia_fin,
           1,
           var$gmt_zero,
           par$cod_usuario,
           var$gmt_zero,
           par$cod_usuario);
        
       
      ELSE
        UPDATE sapr_tplanxmaquina 
        SET fyh_vigencia_inicio = par$fyh_vigencia_inicio,
            fyh_vigencia_fin = par$fyh_vigencia_fin,
            gmt_modificacion = var$gmt_zero,
            des_usuario_modificacion = par$cod_usuario
        WHERE BOL_ACTIVO = 1 AND OID_MAQUINA = par$oid_maquina  
        AND OID_PLANIFICACION = par$oid_planificacion;
      
       END IF;    
     END IF;      

  END supd_maquinaxplan;




 PROCEDURE svalidar_planxcan_maq(par$oid_planificacion       IN OUT gepr_pcomon_###VERSION###.tipo$oid_,                                 
                                  par$amaq_oid_maquina        IN gepr_pcomon_###VERSION###.tipo$oids_,                                         
                             par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$usr_) IS
                                  
                                  
         CURSOR cur$maquinas IS
         SELECT T.OID_CAMPO1 OID_MAQUINA
              , T.COD_CAMPO2 OID_PLANIFICACION
           FROM SAPR_GTT_TAUXILIAR T
          WHERE T.COD_CALIFICADOR = 'MAQXPLAN';
  BEGIN
     
       IF par$amaq_oid_maquina.count > 0 THEN
         FOR idx IN par$amaq_oid_maquina.first .. par$amaq_oid_maquina.last    LOOP             
            IF par$amaq_oid_maquina(idx) IS NOT NULL THEN
               insert into SAPR_GTT_TAUXILIAR (oid_campo1, cod_campo2, cod_calificador)
                values (par$amaq_oid_maquina(idx),par$oid_planificacion, 'MAQXPLAN'); 
            END IF;
         END LOOP;
       END IF;

         
         /*MAQUINAS QUITADAS DESTA PLANIFICACION*/
         INSERT INTO SAPR_GTT_TAUXILIAR (oid_campo1, cod_campo2, cod_calificador)
         SELECT PC.OID_MAQUINA, NULL,'MAQXPLAN'
           FROM SAPR_TPLANXCANAL PC
           LEFT JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'MAQXPLAN' AND AUX.OID_CAMPO1 = PC.OID_MAQUINA
           WHERE PC.BOL_ACTIVO = 1 AND 
                 PC.OID_PLANIFICACION = par$oid_planificacion AND 
                 PC.OID_MAQUINA IS NOT NULL and 
                 AUX.OID_CAMPO1 IS NULL;
                 

         FOR rec$maquina IN cur$maquinas LOOP
                           
                 sdel_planxcan_maqu(par$oid_plan_nueva   => rec$maquina.OID_PLANIFICACION,
                             par$oid_maquina     => rec$maquina.OID_MAQUINA,
                             par$cod_usuario     => par$cod_usuario);                             
         
         END LOOP rec$maquina;         
                 
                                                     
  END svalidar_planxcan_maq;   
 
/*SE par$oid_plan_nueva ES NULL ENTONCES ESTA QUITANDO MAQUINA DE LA PLANIFICACION*/
PROCEDURE sdel_planxcan_maqu(par$oid_plan_nueva   IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_maquina     IN gepr_pcomon_###VERSION###.tipo$oid_,                                         
                             par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$usr_) IS

    var$gmt_zero             gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
BEGIN
           
             IF par$oid_plan_nueva IS NULL THEN
                /* QUITANDO planxcanal y planxsubcanal de MAQUINA quitada DA PLANIFICACION*/
               update sapr_TplanXcanal 
                SET bol_activo               = 0,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$cod_usuario
               where 
                 bol_activo               = 1            
                and oid_maquina = par$oid_maquina; 
                
                
               update sapr_TplanXSUBcanal 
                SET bol_activo               = 0,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$cod_usuario
               where 
                 bol_activo               = 1            
                and oid_maquina = par$oid_maquina; 
             
             ELSE


              /*QUITANDO PLAXCANAL Y PLANXSUBCANAL DE MAQUINA CAMBIADA DE PLANIFICACION*/
               
             update sapr_Tplanxsubcanal 
              SET bol_activo               = 0,
             gmt_modificacion         = var$gmt_zero,
             des_usuario_modificacion = par$cod_usuario
             where oid_planxcanal not in (select oid_planxcanal 
                                           FROM SAPR_TPLANXCANAL 
                                           where oid_planificacion = par$oid_plan_nueva
                                           and bol_activo  = 1             
                                            and oid_maquina = par$oid_maquina) 
              and bol_activo  = 1             
              and oid_maquina = par$oid_maquina;        
              
               update sapr_Tplanxcanal  
                SET bol_activo               = 0,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$cod_usuario
               where oid_planificacion <> par$oid_plan_nueva 
                and bol_activo = 1     
                and oid_maquina = par$oid_maquina;
                
             END IF;
        
        END sdel_planxcan_maqu;     




PROCEDURE sdel_planxcan_ptos(par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,                             
                            par$aoid_pto_servicios  IN gepr_pcomon_###VERSION###.tipo$oids_,                               
                            par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_) is
                            
                            
   
    
  CURSOR cur$pto_servicio IS
         SELECT T.OID_CAMPO1 OID_PTO_SERVICIO
              , T.COD_CAMPO2 OID_MAQUINA
           FROM SAPR_GTT_TAUXILIAR T
          WHERE T.COD_CALIFICADOR = 'MAQXPTO';
  BEGIN
     
       
         FOR idx IN par$aoid_pto_servicios.first .. par$aoid_pto_servicios.last    LOOP    
           
               insert into SAPR_GTT_TAUXILIAR (oid_campo1, cod_campo2, cod_calificador)
                values (par$aoid_pto_servicios(idx),par$oid_maquina, 'MAQXPTO'); 
             
         END LOOP;
          insert into SAPR_GTT_TAUXILIAR (oid_campo1, cod_campo2, cod_calificador)
          
                SELECT OID_PTO_SERVICIO, NULL, 'MAQXPTO' FROM SAPR_TPLANXCANAL WHERE OID_MAQUINA = par$oid_maquina 
                AND OID_PTO_SERVICIO NOT IN (
                       SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'MAQXPTO');
               
                
                
                
         FOR rec$pto_servicio IN cur$pto_servicio LOOP
                           
                 sdel_planxptocan(par$oid_maquina   => rec$pto_servicio.OID_MAQUINA,
                             par$oid_pto_servicio     => rec$pto_servicio.OID_PTO_SERVICIO,
                             par$cod_usuario     => par$cod_usuario);                             
         
         END LOOP cur$pto_servicio;
  
/*se oidmaquina for diferente no ponto de serviço então deve quitar
se oidpunto + maquina nãão estiver no array também deve quitar

*/


end sdel_planxcan_ptos;
                            
 /*
 par$oid_maquina NULL => DEL
 par$oid_maquina <> OID_MAQUINA ATUAL => DEL
 
 */                           
PROCEDURE sdel_planxptocan(par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,                             
                            par$oid_pto_servicio  IN gepr_pcomon_###VERSION###.tipo$oid_,                               
                            par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$usr_) IS

    var$gmt_zero             gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;    
    var$existe   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
BEGIN
       
    IF par$oid_maquina IS NULL THEN
  

           UPDATE SAPR_TPLANXSUBCANAL
             SET bol_activo               = 0,
                 gmt_modificacion         = var$gmt_zero,
                 des_usuario_modificacion = par$cod_usuario
           WHERE OID_PTO_SERVICIO = par$oid_pto_servicio
             AND bol_activo = 1;
               
           UPDATE SAPR_TPLANXCANAL
                 SET bol_activo               = 0,
                     COD_AGRUPADOR = null,
                     gmt_modificacion         = var$gmt_zero,
                     des_usuario_modificacion = par$cod_usuario
                 WHERE OID_PTO_SERVICIO = par$oid_pto_servicio
             AND bol_activo = 1;
    ELSE  
      SELECT COUNT(1)
              INTO var$existe
                FROM SAPR_TPLANXCANAL PC
             WHERE PC.OID_MAQUINA <> par$oid_maquina
             AND PC.OID_PTO_SERVICIO = par$oid_pto_servicio
               AND  PC.BOL_ACTIVO = 1
                      AND ROWNUM < 2;
                    
                 IF var$existe = 1 THEN
                  
             
                  UPDATE SAPR_TPLANXSUBCANAL
                   SET bol_activo               = 0,
                       gmt_modificacion         = var$gmt_zero,
                       des_usuario_modificacion = par$cod_usuario
                 WHERE OID_PTO_SERVICIO = par$oid_pto_servicio
                   AND bol_activo = 1;
                     
                 UPDATE SAPR_TPLANXCANAL
                       SET bol_activo               = 0,
                           COD_AGRUPADOR = null,
                           gmt_modificacion         = var$gmt_zero,
                           des_usuario_modificacion = par$cod_usuario
                       WHERE OID_PTO_SERVICIO = par$oid_pto_servicio
                   AND bol_activo = 1;
               END IF;
    END IF;
  

 END sdel_planxptocan;

  /* Grabar terminos por planificación */
 PROCEDURE supd_terminoxplan(par$oid_planificacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$aoid_iac_patron IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_termino_patron IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$avalor_termino_patron IN gepr_pcomon_###VERSION###.tipo$descs_,                           
                            par$aoid_iac_dinam IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_termino_dinam IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$avalor_termino_dinam IN gepr_pcomon_###VERSION###.tipo$descs_,
                            par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$deletes             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$updates             OUT gepr_pcomon_###VERSION###.tipo$nel_
                            ) IS
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;

  BEGIN
    /* variables locales */
    par$inserts      := 0;
    par$selects      := 0;
    par$updates     := 0;
    par$deletes      := 0;
 
    DELETE FROM SAPR_TVALOR_TERMINOXPLAN 
    WHERE OID_PLANIFICACION = par$oid_planificacion;
    par$deletes := par$deletes + sql%rowcount;
    
    IF par$aoid_iac_patron IS NOT NULL AND par$aoid_iac_patron.COUNT > 0  THEN
        FOR idx IN par$aoid_iac_patron.first .. par$aoid_iac_patron.last LOOP
            IF par$aoid_iac_patron(idx) IS NOT NULL THEN
                insert into SAPR_TVALOR_TERMINOXPLAN (OID_VALOR_TERMINOXDOCUMENTO, OID_PLANIFICACION, OID_IAC, OID_TERMINO, DES_VALOR, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                values (sys_guid(), par$oid_planificacion, par$aoid_iac_patron(idx), par$aoid_termino_patron(idx), par$avalor_termino_patron(idx), var$gmt_zero, par$cod_usuario, var$gmt_zero,par$cod_usuario);
                par$inserts := par$inserts + sql%rowcount;
            END IF;
        END LOOP;
    END IF;
    
    IF par$aoid_iac_dinam IS NOT NULL AND par$aoid_iac_dinam.COUNT > 0 THEN
        FOR idx IN par$aoid_iac_dinam.first .. par$aoid_iac_dinam.last LOOP
            IF par$aoid_iac_dinam(idx) IS NOT NULL THEN
                insert into SAPR_TVALOR_TERMINOXPLAN (OID_VALOR_TERMINOXDOCUMENTO, OID_PLANIFICACION, OID_IAC, OID_TERMINO, DES_VALOR, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION)
                values (sys_guid(), par$oid_planificacion, par$aoid_iac_dinam(idx), par$aoid_termino_dinam(idx), par$avalor_termino_dinam(idx), var$gmt_zero, par$cod_usuario, var$gmt_zero,par$cod_usuario);
                par$inserts := par$inserts + sql%rowcount;
            END IF;
        END LOOP;
    END IF;
 
  END supd_terminoxplan;

  /* Recuperar MAEs planificadas */
  PROCEDURE srecuperar_maes_planificadas(
                                     par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_pais                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_planificacion        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$acod_device_id           IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$anel_index               IN gepr_pcomon_###VERSION###.tipo$nels_,
                                     par$cod_fecha                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                     par$nel_gmt_minuto           IN gepr_pcomon_###VERSION###.tipo$nel_,
                                     par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_maquinas                 OUT sys_refcursor,
                                     par$rc_validaciones             OUT sys_refcursor)
  IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_maes_planificadas';

    const$res_0_no_planif CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'MSG_RES_0',
                gepr_pcomon_###VERSION###.const$CodFunRecMaesPlanificad,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                '',
                0);
    const$res_1_planif CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'MSG_RES_1',
                gepr_pcomon_###VERSION###.const$CodFunRecMaesPlanificad,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                '',
                0);
    const$res_2_no_existe CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'MSG_RES_2',
                gepr_pcomon_###VERSION###.const$CodFunRecMaesPlanificad,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                '',
                0);

    
  BEGIN

    /* #### Limpiamos tabla auxiliar #### */
    DELETE FROM SAPR_GTT_TAUXILIAR;
    COMMIT;

    /* #### Inicializar los cursores #### */
    OPEN par$rc_maquinas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    /* #### Grabar llamadas #### */
    IF par$oid_llamada is not null THEN

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

      /* COD_PLANIFICACION */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_planificacion: ' || par$cod_planificacion,
				par$cod_identificador   => '');

      /* cod_identificador_ajeno */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
				par$cod_identificador   => '');

      /* cod_fecha */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_fecha: ' || par$cod_fecha,
				par$cod_identificador   => '');
      
      /* nel_gmt_minuto */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$nel_gmt_minuto: ' || par$nel_gmt_minuto,
				par$cod_identificador   => '');

      /* cod_usuario */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
				par$cod_identificador   => '');

      /* cod_cultura */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
				par$cod_identificador   => '');

      /*par$acod_device_id*/
      IF par$acod_device_id IS NOT NULL AND par$acod_device_id.count > 0 THEN
        FOR idx in par$acod_device_id.first .. par$acod_device_id.last LOOP
          IF par$acod_device_id(idx) IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$acod_device_id (' || idx || '): ' || par$acod_device_id(idx),
              par$cod_identificador   => '');  
          END IF;
        END LOOP;
      ELSE
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'par$acod_device_id: NULL',
          par$cod_identificador   => '');
      END IF;


      /*par$anel_index*/
      IF par$anel_index IS NOT NULL AND par$anel_index.count > 0 THEN
        FOR idx in par$anel_index.first .. par$anel_index.last LOOP
          IF par$anel_index(idx) IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$anel_index (' || idx || '): ' || par$anel_index(idx),
              par$cod_identificador   => '');  
          END IF;
        END LOOP;
      ELSE
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'par$anel_index: NULL',
          par$cod_identificador   => '');
      END IF;

    END IF;
    
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Previo a svalidar_maes_planificadas',
      par$cod_identificador   => '');

    svalidar_maes_plaficadas( 
      par$oid_llamada               => par$oid_llamada,
      par$cod_identificador_ajeno   => par$cod_identificador_ajeno,
      par$cod_pais                  => par$cod_pais,
      par$cod_planificacion         => par$cod_planificacion,
      par$acod_device_id            => par$acod_device_id,
      par$anel_index                => par$anel_index,
      par$cod_fecha                 => par$cod_fecha,
      par$nel_gmt_minuto            => par$nel_gmt_minuto,
      par$cod_usuario               => par$cod_usuario,
      par$cod_cultura               => par$cod_cultura
    );

    DBMS_OUTPUT.put_line('Previo a preguntar si hay error (func: fhay_error_rec_maes_plani() )');

    
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'Previo a preguntar si hay error (func: fhay_error_rec_maes_plani() )',
      par$cod_identificador   => '');

    IF NOT fhay_error_rec_maes_plani() THEN
      DBMS_OUTPUT.put_line('No devuelve error en la función fhay_error_rec_maes_plani');
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => '¡fhay_error_rec_maes_plani() = NO HAY ERROR!',
        par$cod_identificador   => '');
      
      /*Grabamos en la tabla auxiliar los valores*/
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, NEL_CAMPO5, COD_CALIFICADOR)
      SELECT
        DISTINCT
        MAQUINAS.OID_CAMPO1 COD_DEVICE_ID,
        CASE 
          WHEN MAQUINAS.OID_CAMPO7 IS NULL THEN 2
          ELSE
          (
            SELECT DISTINCT 

              NVL(MAX(CASE 
                WHEN PLANXMAQ.BOL_ACTIVO = 1 AND (TO_DATE(MAQUINAS.COD_CAMPO3, 'DD/MM/YYYY HH24:MI:SS') BETWEEN PLANXMAQ.FYH_VIGENCIA_INICIO AND PLANXMAQ.FYH_VIGENCIA_FIN OR TO_DATE(MAQUINAS.COD_CAMPO3, 'DD/MM/YYYY HH24:MI:SS') >= PLANXMAQ.FYH_VIGENCIA_INICIO AND PLANXMAQ.FYH_VIGENCIA_FIN IS NULL) THEN 1
                WHEN PLANXMAQ.BOL_ACTIVO = 0 AND PLANXMAQ.FYH_VIGENCIA_FIN IS NULL AND TO_DATE(MAQUINAS.COD_CAMPO3, 'DD/MM/YYYY HH24:MI:SS') BETWEEN PLANXMAQ.FYH_VIGENCIA_INICIO AND PLANXMAQ.GMT_MODIFICACION THEN 1 
                WHEN PLANXMAQ.BOL_ACTIVO = 0 AND TO_DATE(MAQUINAS.COD_CAMPO3, 'DD/MM/YYYY HH24:MI:SS') BETWEEN PLANXMAQ.FYH_VIGENCIA_INICIO AND PLANXMAQ.FYH_VIGENCIA_FIN THEN 1
                ELSE 0
              END ), 0)
            FROM 
              SAPR_TPLANXMAQUINA PLANXMAQ WHERE PLANXMAQ.OID_MAQUINA = MAQUINAS.OID_CAMPO7
          )
        END CODIGO_RESULTADO,
        'CURSOR_MAQUINAS'
      FROM
        SAPR_GTT_TAUXILIAR MAQUINAS
      WHERE
        MAQUINAS.COD_CALIFICADOR = 'OID_MAQUINA';

      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Devolvemos cursor de máquinas',
        par$cod_identificador   => '');

      OPEN par$rc_maquinas FOR
      SELECT 
        MAQUINAS.OID_CAMPO1 COD_DEVICE_ID,
        MAQUINAS.NEL_CAMPO5 CODIGO_RESULTADO,
        CASE
          when MAQUINAS.NEL_CAMPO5 = 0 then const$res_0_no_planif
          when MAQUINAS.NEL_CAMPO5 = 1 then const$res_1_planif
          when MAQUINAS.NEL_CAMPO5 = 2 then const$res_2_no_existe
        END DESCRIPCION_RESULTADO
      FROM SAPR_GTT_TAUXILIAR MAQUINAS
      WHERE MAQUINAS.COD_CALIFICADOR = 'CURSOR_MAQUINAS'
      ;


    END IF;
    SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Devolvemos cursor de validaciones',
        par$cod_identificador   => '');


    OPEN par$rc_validaciones FOR
      SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CALIFICADOR AS CALIFICADOR
      FROM SAPR_GTT_TAUXILIAR AUX
      WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';
  EXCEPTION
      when others then
      ROLLBACK;
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'Ocurrió un error inesperado',
        par$cod_identificador   => '');
  END srecuperar_maes_planificadas;

  /* Validar MAEs planificadas */
  PROCEDURE svalidar_maes_plaficadas(
    par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                 IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_planificacion        IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$acod_device_id           IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$anel_index               IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$cod_fecha                IN gepr_pcomon_###VERSION###.tipo$fyh_,
    par$nel_gmt_minuto           IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
    par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_
  )
  IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_maes_plaficadas';
    var$oid_pais  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_maquina  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$mensaje gepr_pcomon_###VERSION###.tipo$desc_;
    var$fecha gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fecha_gmt0 gepr_pcomon_###VERSION###.tipo$fyh_;

    CURSOR cur$maquinas IS
      SELECT OID_CAMPO7 OID_MAQUINA, OID_CAMPO1 COD_IDENTIFICACION
      FROM SAPR_GTT_TAUXILIAR
      WHERE COD_CALIFICADOR = 'OID_MAQUINA';

  BEGIN
    /* #### Grabar llamadas #### */
    IF par$oid_llamada is not null THEN

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

      /* COD_PLANIFICACION */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_planificacion: ' || par$cod_planificacion,
				par$cod_identificador   => '');

      /* cod_identificador_ajeno */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
				par$cod_identificador   => '');

      /* cod_fecha */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_fecha: ' || par$cod_fecha,
				par$cod_identificador   => '');

      /* nel_gmt_minuto */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$nel_gmt_minuto: ' || par$nel_gmt_minuto,
				par$cod_identificador   => '');

      /* cod_usuario */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
				par$cod_identificador   => '');

      /* cod_cultura */
			SAPR_PLOG_API.SAGREGA_DETALLE(
			  par$oid_llamada  => par$oid_llamada,
				par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
				par$des_version     => const$version,
				par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
				par$cod_identificador   => '');

      /*par$acod_device_id*/
      IF par$acod_device_id IS NOT NULL AND par$acod_device_id.count > 0 THEN
        FOR idx in par$acod_device_id.first .. par$acod_device_id.last LOOP
          IF par$acod_device_id(idx) IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$acod_device_id (' || idx || '): ' || par$acod_device_id(idx),
              par$cod_identificador   => '');  
          END IF;
        END LOOP;
      ELSE
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'par$acod_device_id: NULL',
          par$cod_identificador   => '');
      END IF;

      /*par$anel_index*/
      IF par$anel_index IS NOT NULL AND par$anel_index.count > 0 THEN
        FOR idx in par$anel_index.first .. par$anel_index.last LOOP
          IF par$anel_index(idx) IS NOT NULL THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$anel_index (' || idx || '): ' || par$anel_index(idx),
              par$cod_identificador   => '');  
          END IF;
        END LOOP;
      ELSE
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'par$anel_index: NULL',
          par$cod_identificador   => '');
      END IF;

    END IF;

    /*Valido el codigo de pais y obtengo su OID*/
    BEGIN
      IF par$cod_identificador_ajeno IS NOT NULL THEN
          select OID_TABLA_GENESIS
          into var$oid_pais
          from GEPR_TCODIGO_AJENO
          where COD_AJENO = par$cod_pais 
          and COD_TIPO_TABLA_GENESIS = 'GEPR_TPAIS'
          and COD_IDENTIFICADOR = par$cod_identificador_ajeno;
      ELSE
          select OID_PAIS
          into var$oid_pais
          from GEPR_TPAIS
          where cod_pais = par$cod_pais;
      END IF;
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR) VALUES (var$oid_pais, 'OID_PAIS');
    EXCEPTION
      when others then
        /*2040300005 - El código de país {0} ingresado no existe */
        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040300005',
                gepr_pcomon_###VERSION###.const$CodFunRecMaesPlanificad,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                par$cod_pais,
                0);
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
        VALUES ('2040300005', var$mensaje, 'VALIDACIONES');

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'El código de país no existe. ' || var$mensaje,
          par$cod_identificador   => '');
    END;

    /* Valido las MAEs */
    BEGIN
      IF par$acod_device_id IS NOT NULL THEN
        /*Recorro la colección de DeviceIDs*/
        DBMS_OUTPUT.put_line('Previo a recorrer el array asociativo');
        FOR idx in par$acod_device_id.first .. par$acod_device_id.last LOOP
          IF par$acod_device_id(idx) is not null THEN
            BEGIN
              SELECT OID_MAQUINA
              INTO var$oid_maquina
              FROM SAPR_TMAQUINA
              WHERE cod_identificacion = par$acod_device_id(idx);
            EXCEPTION
              WHEN NO_DATA_FOUND THEN
                var$oid_maquina := NULL;
            END;

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, OID_CAMPO7, NEL_CAMPO5, COD_CALIFICADOR) VALUES (par$acod_device_id(idx), var$oid_maquina,  par$anel_index(idx), 'OID_MAQUINA');

          END IF;
        END LOOP;

        DBMS_OUTPUT.put_line('Previo a recorrer el cursor');
        FOR rec$maquina IN cur$maquinas LOOP

          dbms_output.put_line('rec$maquina.Cod_Identificacion: ' || rec$maquina.Cod_Identificacion);
          dbms_output.put_line('rec$maquina.OID_MAQUINA: ' || rec$maquina.oid_maquina);

          var$fecha := par$cod_fecha;

          /*Solo valido fecha para máquinas que existan*/
          IF rec$maquina.OID_MAQUINA IS NOT NULL THEN
            svalidar_fecha(
              par$nel_index        => 0,
              par$device_id        => rec$maquina.COD_IDENTIFICACION,
              par$nel_gmt_minuto   => par$nel_gmt_minuto,
              par$fecha            => var$fecha,
              par$fecha_gmt0       => var$fecha_gmt0,
              par$cod_cultura      => par$cod_cultura
            );

            dbms_output.put_line('var$fecha_gmt0: ' || var$fecha_gmt0);

            UPDATE SAPR_GTT_TAUXILIAR
              SET COD_CAMPO3 = TO_CHAR(var$fecha_gmt0,'DD/MM/YYYY HH24:MI:SS')
            WHERE
              COD_CALIFICADOR = 'OID_MAQUINA' 
              AND Oid_Campo1 = rec$maquina.COD_IDENTIFICACION
              AND OID_CAMPO7 = rec$maquina.oid_maquina;

          END IF;
        END LOOP rec$maquina;
        COMMIT;

      ELSE
        /*2040300007 - NO INFORMA MAQUINA ALGUNA */
        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040300007',
          gepr_pcomon_###VERSION###.const$CodFunRecMaesPlanificad,
          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,'',
          0);
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
        VALUES ('2040300007', var$mensaje, 'VALIDACIONES');

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => var$mensaje,
          par$cod_identificador   => '');

      END IF;
    EXCEPTION
      when others then
        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Ocurrió un error inesperado',
          par$cod_identificador   => '');
    END;

    /*Valido que haya informado fecha */
    IF par$cod_fecha is null THEN
      /*2040300006 - Es necesario informar una fecha */
      var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040300006',
              gepr_pcomon_###VERSION###.const$CodFunRecMaesPlanificad,
              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
              '',
              0);
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
      VALUES ('2040300006', var$mensaje, 'VALIDACIONES');

      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => var$mensaje,
        par$cod_identificador   => '');
        
    END IF;

    /*Valido la planificacion informada*/
    IF par$cod_planificacion is not null then
      BEGIN
        SELECT OID_PLANIFICACION
        INTO var$oid_planificacion
        FROM SAPR_TPLANIFICACION
        WHERE COD_PLANIFICACION = par$cod_planificacion;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          var$oid_planificacion := NULL;
      END;

      IF var$oid_planificacion IS NOT NULL THEN
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR) VALUES (var$oid_planificacion, 'OID_PLANIFICACION');
      ELSE
        /*2040300003 - El código de planificado {0} ingresado no existe */
        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040300003',
                gepr_pcomon_###VERSION###.const$CodFunRecMaesPlanificad,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                par$cod_planificacion,
                0);        
      END IF;
    END IF;

  END svalidar_maes_plaficadas;

  /* Función que corrobora si hay error para el procedure de srecuperar_maes_planificadas */
  FUNCTION fhay_error_rec_maes_plani RETURN BOOLEAN IS
    var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN
    BEGIN
      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR A
       WHERE A.COD_CALIFICADOR = 'VALIDACIONES' and A.OID_CAMPO1 like '20403%';
    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;
    RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);
  END fhay_error_rec_maes_plani;

  /* Validar Fecha */
  PROCEDURE svalidar_fecha(par$nel_index            IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$device_id          IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$nel_gmt_minuto       IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$fecha                IN OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$fecha_gmt0           OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    var$oid_delegacion gepr_pcomon_###VERSION###.tipo$oid_;
    var$nel_gmt_minuto gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN

      /* Delegacion */
      BEGIN
              SELECT PLAN.OID_DELEGACION
              INTO var$oid_delegacion
              FROM GEPR_TSECTOR SECT
        INNER JOIN GEPR_TPLANTA PLAN ON PLAN.OID_PLANTA = SECT.OID_PLANTA
        INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_SECTOR = SECT.OID_SECTOR
             WHERE MAQ.COD_IDENTIFICACION = par$device_id;

      EXCEPTION WHEN no_data_found THEN
        var$oid_delegacion := NULL;
      END;
      DBMS_OUTPUT.PUT_LINE('var$oid_delegacion: ' || var$oid_delegacion);
      var$nel_gmt_minuto := NULL;
      /* Calcular la fecha GMT */
      gepr_putilidades_###VERSION###.scalcular_gmt(par$fyh_gestion    => par$fecha,
                                          par$oid_delegacion => var$oid_delegacion,
                                          par$nel_gmt_minuto => var$nel_gmt_minuto,
                                          par$cod_cultura    => par$cod_cultura);

      DBMS_OUTPUT.PUT_LINE('var$nel_gmt_minuto: ' || var$nel_gmt_minuto);
      IF (par$nel_gmt_minuto IS NULL) THEN
        par$fecha_gmt0 := gepr_putilidades_###VERSION###.fsomar_fecha(par$fecha, var$nel_gmt_minuto * -1);
        DBMS_OUTPUT.PUT_LINE('par$fecha_gmt0: ' || TO_CHAR(par$fecha_gmt0,'DD/MM/YYYY HH24:MI:SS'));

      ELSE
        par$fecha_gmt0 := gepr_putilidades_###VERSION###.fsomar_fecha(par$fecha, par$nel_gmt_minuto * -1);
        par$fecha := gepr_putilidades_###VERSION###.fsomar_fecha(par$fecha_gmt0, var$nel_gmt_minuto);

        DBMS_OUTPUT.PUT_LINE('par$fecha: ' || TO_CHAR(par$fecha,'DD/MM/YYYY HH24:MI:SS'));
        DBMS_OUTPUT.PUT_LINE('par$fecha_gmt0: ' || TO_CHAR(par$fecha_gmt0,'DD/MM/YYYY HH24:MI:SS'));

      END IF;

  END svalidar_fecha;

  /*Insert/update de mensajes por plan*/
  PROCEDURE supd_planxmensajes(par$oid_planificacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$a_cod_mensaje               IN gepr_pcomon_###VERSION###.tipo$cods_,
                          par$a_des_mensaje               IN gepr_pcomon_###VERSION###.tipo$descs_,
                          par$a_tipo_mensaje              IN gepr_pcomon_###VERSION###.tipo$cods_,
                          par$a_tipop_mensaje              IN gepr_pcomon_###VERSION###.tipo$oids_,
                          par$a_sin_reintentos_mensaje    IN gepr_pcomon_###VERSION###.tipo$nels_,
                          par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$selects                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$inserts                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$deletes                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$updates                 OUT gepr_pcomon_###VERSION###.tipo$nel_)
  IS
    var$gmt_zero gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    /* Inicializo los parametros de salidas */
    par$selects := 0;
    par$inserts := 0;
    par$deletes := 0;
    par$updates := 0;


    /*Borramos físicamente todos los mensajes de la planificación*/
    DELETE FROM SAPR_TPLANXMENSAJE WHERE OID_PLANIFICACION = par$oid_planificacion;
    par$deletes := par$deletes + sql%ROWCOUNT;

    IF par$a_cod_mensaje is not null THEN
      FOR idx in par$a_cod_mensaje.first .. par$a_cod_mensaje.last LOOP
        IF par$a_cod_mensaje(idx) IS NOT NULL THEN
          INSERT INTO SAPR_TPLANXMENSAJE
          (
            OID_PLANXMENSAJE, OID_PLANIFICACION, COD_MENSAJE, DES_MENSAJE, COD_TIPO_MENSAJE, BOL_SIN_REINTENTOS, 
            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION, OID_TIPO_PERIODO
          )
          VALUES 
          (
            sys_guid(), par$oid_planificacion, par$a_cod_mensaje(idx), par$a_des_mensaje(idx), par$a_tipo_mensaje(idx),  par$a_sin_reintentos_mensaje(idx), 
            var$gmt_zero, par$cod_usuario, var$gmt_zero, par$cod_usuario, par$a_tipop_mensaje(idx)
          );
          par$inserts := par$inserts + sql%ROWCOUNT;
        END IF;
      END LOOP;
    END IF;


  END supd_planxmensajes;  
  
  /* Verifica los datos del documento para ver si cumple con las condiciones de la planificación */
  PROCEDURE sverifica_documento(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_maquina       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_canal         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_subcanal      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$fyh_gestion       IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                  par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_planificacion OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_validaciones  OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$des_validaciones  OUT gepr_pcomon_###VERSION###.tipo$descs_)
  IS
    const$nombre_func CONSTANT    gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sverifica_documento';      

    var$cod_planificacion         gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_planificacion         gepr_pcomon_###VERSION###.tipo$desc_;    
    var$fyh_inicio_planificacion  gepr_pcomon_###VERSION###.tipo$fyh_;    
    var$fyh_fin_planificacion     gepr_pcomon_###VERSION###.tipo$fyh_;    
    var$fyh_inicio_maquina        gepr_pcomon_###VERSION###.tipo$fyh_;    
    var$fyh_fin_maquina           gepr_pcomon_###VERSION###.tipo$fyh_;    
    var$cod_planif_banco_ope      gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planif_banco_mdm      gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_planif_banco          gepr_pcomon_###VERSION###.tipo$desc_;
    var$existe                    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$cod_tipo_planificacion    gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_tipo_planificacion    gepr_pcomon_###VERSION###.tipo$desc_; 

  BEGIN

    DELETE FROM SAPR_GTT_TAUXILIAR
    WHERE COD_CALIFICADOR = 'DATOS_PLANIFICACION';

    -- Buscamos los datos de la planificación de la máquina
    BEGIN
      SELECT 
        PM.OID_PLANIFICACION,
        P.COD_PLANIFICACION,
        P.DES_PLANIFICACION,
        P.FYH_VIGENCIA_INICIO FYH_INICIO_PLANIFICACION,
        P.FYH_VIGENCIA_FIN FYH_FIN_PLANIFICACION,
        PM.FYH_VIGENCIA_INICIO FYH_INICIO_MAQUINA,
        PM.FYH_VIGENCIA_FIN FYH_FIN_MAQUINA,
        BANCOAJ.COD_AJENO COD_PLANIF_BANCO_OPE,
        BANCO.COD_CLIENTE COD_PLANIF_BANCO_MDM,
        BANCO.DES_CLIENTE DES_PLANIF_BANCO,
        TP.COD_TIPO_PLANIFICACION,
        TP.DES_TIPO_PLANIFICACION
      INTO 
        par$oid_planificacion, 
        var$cod_planificacion,   
        var$des_planificacion, 
        var$fyh_inicio_planificacion,
        var$fyh_fin_planificacion,
        var$fyh_inicio_maquina,
        var$fyh_fin_maquina,
        var$cod_planif_banco_ope,
        var$cod_planif_banco_mdm,
        var$des_planif_banco,
        var$cod_tipo_planificacion,
        var$des_tipo_planificacion
      FROM SAPR_TPLANXMAQUINA PM 
        INNER JOIN SAPR_TPLANIFICACION P ON P.OID_PLANIFICACION = PM.OID_PLANIFICACION
        INNER JOIN SAPR_TTIPO_PLANIFICACION TP ON TP.OID_TIPO_PLANIFICACION = P.OID_TIPO_PLANIFICACION
        INNER JOIN GEPR_TCLIENTE BANCO ON BANCO.OID_CLIENTE = P.OID_CLIENTE
        LEFT JOIN GEPR_TCODIGO_AJENO BANCOAJ ON BANCOAJ.OID_TABLA_GENESIS = BANCO.OID_CLIENTE AND BANCOAJ.COD_IDENTIFICADOR = 'MAE' AND BANCOAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
      WHERE PM.BOL_ACTIVO = 1 AND P.BOL_ACTIVO = 1 AND TP.BOL_ACTIVO = 1 AND PM.OID_MAQUINA = par$oid_maquina;

    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        par$oid_planificacion := NULL;
    END;

    IF par$oid_planificacion IS NOT NULL THEN
      -- Buscamos los subcanales de la planificación coincidiendo con el del documento, a excepcion de SALDO FISICO
      SELECT COUNT(1)
      INTO var$existe 
      FROM SAPR_TPLANIFICACION PL
            INNER JOIN SAPR_TPLANXCANAL PC ON PL.OID_PLANIFICACION = PC.OID_PLANIFICACION AND PC.BOL_ACTIVO = 1
            INNER JOIN GEPR_TCANAL C ON C.OID_CANAL = PC.OID_CANAL AND C.BOL_VIGENTE = 1
            INNER JOIN SAPR_TPLANXSUBCANAL PSC ON PSC.OID_PLANXCANAL = PC.OID_PLANXCANAL AND PSC.BOL_ACTIVO = 1
            INNER JOIN GEPR_TSUBCANAL SUBC ON SUBC.OID_SUBCANAL = PSC.OID_SUBCANAL AND SUBC.BOL_VIGENTE = 1
      WHERE PL.OID_PLANIFICACION = par$oid_planificacion
      AND (
            (C.COD_CANAL = par$cod_canal AND SUBC.COD_SUBCANAL = par$cod_subcanal) 
            OR par$cod_canal = 'SF'
            ) 
      ;

      IF var$existe  > 0 THEN
        /* Valida si la fecha de gestion esta en el rango de fecha de la planificacion */

        IF (var$fyh_inicio_maquina <= par$fyh_gestion 
            AND (var$fyh_fin_maquina IS NULL OR par$fyh_gestion <= var$fyh_fin_maquina) 
            AND (var$fyh_fin_planificacion IS NULL OR par$fyh_gestion <= var$fyh_fin_planificacion)) THEN
          -- Fecha de gestion esta en rango de fecha de la planificacion debería relacionarse.

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$nombre_func,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'Fecha de gestión: ' || TO_CHAR(par$fyh_gestion, 'DD-MM-YYYY HH24:MI:SS') || ' esta en el rango de fecha de la planificación debería relacionarse: ' || const$new_line ||  
                                                'fyh_inicio_planificacion: '  || TO_CHAR(var$fyh_inicio_planificacion, 'DD-MM-YYYY HH24:MI:SS') || const$new_line || 
                                                'fyh_fin_planificacion: '     || TO_CHAR(var$fyh_fin_planificacion, 'DD-MM-YYYY HH24:MI:SS')    || const$new_line || 
                                                'fyh_inicio_maquina: '        || TO_CHAR(var$fyh_inicio_maquina, 'DD-MM-YYYY HH24:MI:SS')       || const$new_line || 
                                                'fyh_fin_maquina: '           || TO_CHAR(var$fyh_fin_maquina, 'DD-MM-YYYY HH24:MI:SS')          || const$new_line || 
                                                'cod_tipo_planificacion: '    || var$cod_tipo_planificacion                                     || const$new_line || 
                                                'des_tipo_planificacion: '    || var$des_tipo_planificacion,
                        par$cod_identificador   => '');


          -- En caso de pasar todas las validaciones almacenamos los datos en la tabla auxiliar
          INSERT INTO SAPR_GTT_TAUXILIAR (
            OID_CAMPO1,
            COD_CAMPO2,
            COD_CAMPO3,
            COD_CAMPO4,
            COD_CAMPO8,
            COD_CAMPO9,
            COD_CALIFICADOR,
            COD_CAMPO10,
            COD_CAMPO11
            )
          VALUES (
            par$oid_planificacion,
            var$cod_planificacion,
            var$des_planificacion, 
            var$cod_planif_banco_ope,
            var$cod_planif_banco_mdm,
            var$des_planif_banco,
            'DATOS_PLANIFICACION',
            var$cod_tipo_planificacion,
            var$des_tipo_planificacion
          );

        ELSE
          -- Fecha de gestion esta fuera del rango de fecha de la planificacion.
          par$oid_planificacion    := NULL;

          par$cod_validaciones (par$cod_validaciones.COUNT) := '2040010021';
          par$des_validaciones (par$des_validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                              '2040010021',
                                                              'GENERICO',
                                                              gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                              NULL,
                                                              0);

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$nombre_func,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'Fecha de gestión: ' || TO_CHAR(par$fyh_gestion, 'DD-MM-YYYY HH24:MI:SS') || ' esta fuera del rango de fecha de la planificación: ' || const$new_line ||  
                                                'fyh_inicio_planificacion: '  || TO_CHAR(var$fyh_inicio_planificacion, 'DD-MM-YYYY HH24:MI:SS') || const$new_line || 
                                                'fyh_fin_planificacion: '     || TO_CHAR(var$fyh_fin_planificacion, 'DD-MM-YYYY HH24:MI:SS')    || const$new_line || 
                                                'fyh_inicio_maquina: '        || TO_CHAR(var$fyh_inicio_maquina, 'DD-MM-YYYY HH24:MI:SS')       || const$new_line || 
                                                'fyh_fin_maquina: '           || TO_CHAR(var$fyh_fin_maquina, 'DD-MM-YYYY HH24:MI:SS')          || const$new_line || 
                                                'cod_tipo_planificacion: '    || var$cod_tipo_planificacion                                     || const$new_line || 
                                                'des_tipo_planificacion: '    || var$des_tipo_planificacion,
                        par$cod_identificador   => '');
        END IF;
      ELSE
        -- El subcanal del documento no coincide con los configurados en la planificación
        par$oid_planificacion    := NULL;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$nombre_func,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'El subcanal del documento de cod_subcanal: ' || par$cod_subcanal || ', no coincide con los configurados en la planificación.',
                        par$cod_identificador   => '');

      END IF;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$nombre_func,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'No se encuentran datos de la planificación activa para la máquina de oid_maquina: ' || par$oid_maquina,
                        par$cod_identificador   => '');
    END IF;

  END sverifica_documento;
END sapr_pplanificacion_###VERSION###;
/