CREATE OR REPLACE PACKAGE SAPR_PELEMENTO_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pelemento_###VERSION###';

  /* srecuperar_elementos */
  PROCEDURE srecuperar_elementos(par$oids_documentos           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$ejecucion_interna         IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$ele_rc_elementos          OUT sys_refcursor,
                                 par$ele_rc_cuentas            OUT sys_refcursor,
                                 par$ele_rc_carac_tipo_sector  OUT sys_refcursor,
                                 par$ele_rc_val_det_efectivo   OUT sys_refcursor,
                                 par$ele_rc_val_det_medio_pago OUT sys_refcursor,
                                 par$ele_rc_val_totales        OUT sys_refcursor,
                                 par$ele_rc_lista_valor        OUT sys_refcursor,
                                 par$ele_rc_iac                OUT sys_refcursor,
                                 par$ele_rc_terminos_iac       OUT sys_refcursor,
                                 par$ele_rc_valor_termino_iac  OUT sys_refcursor,
                                 par$ele_rc_divisas            OUT sys_refcursor,
                                 par$ele_rc_denominaciones     OUT sys_refcursor,
                                 par$ele_rc_medio_pago         OUT sys_refcursor,
                                 par$ele_rc_unidad_medida      OUT sys_refcursor,
                                 par$ele_rc_calidad            OUT sys_refcursor,
                                 par$ele_rc_valor_termino      OUT sys_refcursor,
                                 par$ele_rc_cont_precintos     OUT sys_refcursor,
                                 par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sguardar_elemento */
  PROCEDURE sguardar_elemento(par$esnuevo           IN BOOLEAN,
                              par$bol_sustituicion  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_gestion_bulto IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_baja          IN BOOLEAN,

                              /* Documento */
                              par$adocs_oid             IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$adocs_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$adocs_oid_cuentasaldo IN gepr_pcomon_###VERSION###.tipo$oids_,

                              /* Remesa */
                              par$aremdoc_oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_remesa_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_externo       IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_iac           IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_cod_recibo_salida IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_usuario_resp      IN gepr_pcomon_###VERSION###.tipo$usrs_,
                              par$aremdoc_puesto_resp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_cod_ruta          IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_nel_parada        IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aremdoc_fyh_transporte    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$aremdoc_nel_cant_bultos   IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aremdoc_fyh_conteo_inicio IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$aremdoc_fyh_conteo_fin    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$aremdoc_oid_remesa_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_remesa_sub    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_cod_cajero        IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_cod_externo       IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_cod_estado_abono  IN gepr_pcomon_###VERSION###.tipo$cods_,

                              /* Remesa - arrays de terminos por remesa */
                              par$avtrem_oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtrem_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtrem_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$avtrem_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                              par$avtrem_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                              /* Bulto - arrays */
                              par$abuldoc_oid_bulto          IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_remesa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_externo        IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_iac            IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_iac_parciales  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_cod_precinto       IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$abuldoc_cod_bolsa          IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$abuldoc_oid_banco_ingreso  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_bol_banco_ing_mad  IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$abuldoc_usuario_resp       IN gepr_pcomon_###VERSION###.tipo$usrs_,
                              par$abuldoc_puesto_resp        IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$abuldoc_nel_cant_parciales IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$abuldoc_fyh_conteo_inicio  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$abuldoc_fyh_conteo_fin     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$abuldoc_fyh_proceso_leg    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$abuldoc_oid_bulto_padre    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_bulto_sub      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_cod_nivel_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$abuldoc_bol_cuadrado       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$abuldoc_oid_tipo_formato   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_tipo_servicio  IN gepr_pcomon_###VERSION###.tipo$oids_,

                              /* Bulto - arrays de terminos por bulto */
                              par$avtbul_oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtbul_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtbul_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$avtbul_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                              par$avtbul_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                              /* Parcial - arrays */
                              par$apardoc_oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_externo       IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_cod_precinto      IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$apardoc_oid_iac           IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_usuario_resp      IN gepr_pcomon_###VERSION###.tipo$usrs_,
                              par$apardoc_puesto_resp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$apardoc_nec_secuencia     IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$apardoc_oid_parcial_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_parcial_sub   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_tipo_formato  IN gepr_pcomon_###VERSION###.tipo$oids_,

                              /* Parcial - arrays de terminos por parcial */
                              par$avtpar_oid_parcial IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtpar_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtpar_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$avtpar_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                              par$avtpar_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                              /* Elemento - Valores Efectivo */
                              par$aelemvalefe_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemvalefe_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_oid_denom     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_bol_esbillete IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$aelemvalefe_oid_unid_med  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_cod_tipo_efec IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemvalefe_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                              par$aelemvalefe_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aelemvalefe_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemvalefe_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$aelemvalefe_oid_calidad   IN gepr_pcomon_###VERSION###.tipo$oids_,

                              /* Elemento - Valores Medio Pago */
                              par$aelemval_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemval_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_cod_tipo_mp   IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemval_mp_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                              par$aelemval_mp_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aelemval_mp_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemval_mp_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$aelemval_mp_nel_secuencia IN gepr_pcomon_###VERSION###.tipo$nels_,

                              /* Elemento - Terminos medio pago */
                              par$aelemter_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemter_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemter_mp_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemter_mp_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,
                              par$aelemter_mp_nel_ind_grupo IN gepr_pcomon_###VERSION###.tipo$nels_,

                              /* informacion de ejecucion */
                              par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$deletes     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /*Validar elemento*/
  PROCEDURE svalidar_elemento(par$aremdoc_cod_externo IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$adocs_oid_cuenta    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$bol_validar_prec    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_sustituicion    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_gestion_bulto   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_baja            IN BOOLEAN,
                              par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* sguardar_elemento_acta */
  PROCEDURE sguardar_elemento_acta(par$bol_gestion_bulto IN gepr_pcomon_###VERSION###.tipo$nel_,

                                   /* Documento */
                                   par$adocs_oid             IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adocs_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adocs_oid_cuentasaldo IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Remesa */
                                   par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Remesa - arrays de terminos por remesa */
                                   par$avtrem_oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtrem_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtrem_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$avtrem_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtrem_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                                   /* Bulto - arrays */
                                   par$abuldoc_oid_bulto          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_remesa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_externo        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_iac            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_iac_parciales  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_cod_precinto       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$abuldoc_cod_bolsa          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$abuldoc_oid_banco_ingreso  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_bol_banco_ing_mad  IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$abuldoc_usuario_resp       IN gepr_pcomon_###VERSION###.tipo$usrs_,
                                   par$abuldoc_puesto_resp        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$abuldoc_nel_cant_parciales IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$abuldoc_fyh_conteo_inicio  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$abuldoc_fyh_conteo_fin     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$abuldoc_fyh_proceso_leg    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$abuldoc_oid_bulto_padre    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_bulto_sub      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_cod_nivel_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$abuldoc_bol_cuadrado       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$abuldoc_oid_tipo_formato   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_tipo_servicio  IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Bulto - arrays de terminos por bulto */
                                   par$avtbul_oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtbul_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtbul_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$avtbul_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtbul_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                                   /* Parcial - arrays */
                                   par$apardoc_oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_externo       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_cod_precinto      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$apardoc_oid_iac           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_usuario_resp      IN gepr_pcomon_###VERSION###.tipo$usrs_,
                                   par$apardoc_puesto_resp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$apardoc_nec_secuencia     IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$apardoc_oid_parcial_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_parcial_sub   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_tipo_formato  IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Parcial - arrays de terminos por parcial */
                                   par$avtpar_oid_parcial IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtpar_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtpar_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$avtpar_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtpar_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                                   /* Elemento - Valores Efectivo */
                                   par$aelemvalefe_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemvalefe_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_oid_denom     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_bol_esbillete IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$aelemvalefe_oid_unid_med  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_cod_tipo_efec IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemvalefe_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aelemvalefe_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$aelemvalefe_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemvalefe_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$aelemvalefe_oid_calidad   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Elemento - Valores Medio Pago */
                                   par$aelemval_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemval_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_cod_tipo_mp   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemval_mp_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aelemval_mp_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$aelemval_mp_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemval_mp_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$aelemval_mp_nel_secuencia IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   /* Elemento - Terminos medio pago */
                                   par$aelemter_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemter_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemter_mp_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemter_mp_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$aelemter_mp_nel_ind_grupo IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   /* informacion de ejecucion */
                                   par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$deletes     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* stratar_reenvio_baja */
  PROCEDURE stratar_reenvio_baja(par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$abuldoc_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$abuldoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$abuldoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$cod_cultura           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$bol_gestion_bulto     in gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$bol_baja              IN BOOLEAN,
                                 par$inserts               OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates               OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$deletes               OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$selects               OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sguardar_valores_efectivo */
  PROCEDURE sguardar_valores_efectivo(par$aelemvalefe_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aelemvalefe_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_oid_denom     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_bol_esbillete IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                      par$aelemvalefe_oid_unid_med  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_cod_tipo_efec IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aelemvalefe_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                                      par$aelemvalefe_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                      par$aelemvalefe_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aelemvalefe_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                      par$aelemvalefe_oid_calidad   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sguardar_valores_mediopago */
  PROCEDURE sguardar_valores_mediopago(par$aelemval_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$aelemval_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_cod_tipo_mp   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$aelemval_mp_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                                       par$aelemval_mp_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                       par$aelemval_mp_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$aelemval_mp_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                       par$aelemval_mp_nel_secuencia IN gepr_pcomon_###VERSION###.tipo$nels_,
                                       par$aelemter_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$aelemter_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemter_mp_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemter_mp_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                       par$aelemter_mp_nel_ind_grupo IN gepr_pcomon_###VERSION###.tipo$nels_,
                                       par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                       par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar Valores Terminos*/
  PROCEDURE sguardar_valores_terminos( /* arrays de terminos por remesa */par$avtrem_oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtrem_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtrem_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      /* arrays de terminos por bulto */
                                      par$avtbul_oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtbul_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtbul_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      /* arrays de terminos por parcial */
                                      par$avtpar_oid_parcial IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtpar_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtpar_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* srecupera_iac_elementos */
  PROCEDURE srecupera_iac_elementos(par$adocs_oid        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    /* arrays de terminos por remesa */
                                    par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtrem_oid_remesa     OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtrem_oid_termino    OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtrem_cod_termino    OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$avtrem_des_valor      OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$avtrem_obligatorio    OUT gepr_pcomon_###VERSION###.tipo$nbols_,
                                    /* arrays de terminos por bulto */
                                    par$abuldoc_oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$abuldoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$abuldoc_oid_tipo_servicio IN gepr_pcomon_###VERSION###.tipo$oids_,

                                    par$avtbul_oid_bulto   OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtbul_oid_termino OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtbul_cod_termino OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$avtbul_des_valor   OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$avtbul_obligatorio OUT gepr_pcomon_###VERSION###.tipo$nbols_,
                                    /* arrays de terminos por parcial */
                                    par$apardoc_oid_bulto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtpar_oid_parcial OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtpar_oid_termino OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtpar_cod_termino OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$avtpar_des_valor   OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$avtpar_obligatorio OUT gepr_pcomon_###VERSION###.tipo$nbols_);

  /* sdel_elementos */
  PROCEDURE sdel_elementos(par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$bol_documento_altas   IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$deletes               OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tdeclarado_efectivo */
  PROCEDURE sdel_tdeclarado_efectivo(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$eliminar_hijos IN BOOLEAN,
                                     par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tdeclarado_medio_pago */
  PROCEDURE sdel_tdeclarado_medio_pago(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$eliminar_hijos IN BOOLEAN,
                                       par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tcontado_efectivo */
  PROCEDURE sdel_tcontado_efectivo(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$eliminar_hijos IN BOOLEAN,
                                   par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tcontado_medio_pago */
  PROCEDURE sdel_tcontado_medio_pago(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$eliminar_hijos IN BOOLEAN,
                                     par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tdiferencia_efectivo */
  PROCEDURE sdel_tdiferencia_efectivo(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$eliminar_hijos IN BOOLEAN,
                                      par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tdiferencia_medio_pago */
  PROCEDURE sdel_tdiferencia_medio_pago(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$eliminar_hijos IN BOOLEAN,
                                        par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tdeclarado_efectivo */
  PROCEDURE sins_tdeclarado_efectivo(par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                     par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                     par$bol_ingresado     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                     par$bol_billete       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                     par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tdeclarado_medio_pago */
  PROCEDURE sins_tdeclarado_medio_pago(par$oid_declarado     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                       par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                       par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$bol_ingresado     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                       par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                       par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tcontado_efectivo */
  PROCEDURE sins_tcontado_efectivo(par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_tipocontado   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                   par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                   par$oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$bol_billete       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tcontado_medio_pago */
  PROCEDURE sins_tcontado_medio_pago(par$oid_contato       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_tipo_contado  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                     par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                     par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$nel_secuencia     IN gepr_pcomon_###VERSION###.tipo$nel_,
                                     par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tdiferencia_efectivo */
  PROCEDURE sins_tdiferencia_efectivo(par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                      par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                      par$bol_billete       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                      par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tdiferencia_medio_pago */
  PROCEDURE sins_tdiferencia_medio_pago(par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                        par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                        par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_iac_elem */
  PROCEDURE scargar_tt_oids_iac_elem(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* fcargar_rc_lista_valor */
  FUNCTION fcargar_rc_lista_valor(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  /* fcargar_rc_elementos */
  FUNCTION fcargar_rc_elementos(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  /* fcargar_rc_valores_det_efec */
  FUNCTION fcargar_rc_valores_det_efec(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  /* fcargar_rc_valores_det_mp */
  FUNCTION fcargar_rc_valores_det_mp(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  /* fcargar_rc_valores_totales */
  FUNCTION fcargar_rc_valores_totales(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  /* fcargar_rc_iac */
  FUNCTION fcargar_rc_iac(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  /* fcargar_rc_terminos_iac */
  FUNCTION fcargar_rc_terminos_iac(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  /* fcargar_rc_valor_termino_iac */
  FUNCTION fcargar_rc_valor_termino_iac(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

END SAPR_PELEMENTO_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PELEMENTO_###VERSION### AS

  /* srecuperar_elementos */
  PROCEDURE srecuperar_elementos(par$oids_documentos           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$ejecucion_interna         IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$ele_rc_elementos          OUT sys_refcursor,
                                 par$ele_rc_cuentas            OUT sys_refcursor,
                                 par$ele_rc_carac_tipo_sector  OUT sys_refcursor,
                                 par$ele_rc_val_det_efectivo   OUT sys_refcursor,
                                 par$ele_rc_val_det_medio_pago OUT sys_refcursor,
                                 par$ele_rc_val_totales        OUT sys_refcursor,
                                 par$ele_rc_lista_valor        OUT sys_refcursor,
                                 par$ele_rc_iac                OUT sys_refcursor,
                                 par$ele_rc_terminos_iac       OUT sys_refcursor,
                                 par$ele_rc_valor_termino_iac  OUT sys_refcursor,
                                 par$ele_rc_divisas            OUT sys_refcursor,
                                 par$ele_rc_denominaciones     OUT sys_refcursor,
                                 par$ele_rc_medio_pago         OUT sys_refcursor,
                                 par$ele_rc_unidad_medida      OUT sys_refcursor,
                                 par$ele_rc_calidad            OUT sys_refcursor,
                                 par$ele_rc_valor_termino      OUT sys_refcursor,
                                 par$ele_rc_cont_precintos     OUT sys_refcursor,
                                 par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_elementos';

    var$inicio         gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros      gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts        gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates        gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes        gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges         gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects        gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int    gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts_int    gepr_pcomon_###VERSION###.tipo$nel_;
    var$info_ejecucion gepr_pcomon_###VERSION###.tipo$obs_;
    var$cod_ejecucion  gepr_pcomon_###VERSION###.tipo$nel_;
    var$err            gepr_pcomon_###VERSION###.tipo$obs_;

  BEGIN

    var$inicio    := SYSTIMESTAMP;
    var$registros := 0;
    var$inserts   := 0;
    var$updates   := 0;
    var$deletes   := 0;
    var$selects   := 0;

    /* limpiar tabla temporaria */
    delete sapr_gtt_toids;
    var$deletes   := var$deletes + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;

    /* ===== Cargar tabla temporaria ==== */

    /* - Identificadores Remesas/Documentos */
    /* La ideia de trabajar con identificadores de documentos, es porque quiero atender
    los escenarios de Gestion de Bulto, donde puendo recuperar un documento
    con una remesa, pero no tiene todos los bultos de esta remesa */
    /* Si fue informado identificadores de documentos */
    FOR idx IN par$oids_documentos.first .. par$oids_documentos.last LOOP

      INSERT INTO SAPR_GTT_TOIDS
        (OID, COD_CALIFICADOR)
      VALUES
        (par$oids_documentos(idx), 'DOCUMENTO');
      var$inserts   := var$inserts + 1;
      var$registros := var$registros + 1;

       INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR)
               SELECT DE.OID_REMESA, 'REMESA'
                 FROM SAPR_TDOCUMENTOXELEMENTO DE
                WHERE DE.OID_DOCUMENTO = par$oids_documentos(idx)
                AND DE.OID_REMESA IS NOT NULL;
        var$inserts := var$inserts + sql%ROWCOUNT;
        var$selects := var$selects + sql%ROWCOUNT;
        var$registros := var$registros + sql%ROWCOUNT;

    END LOOP;

    /* - Identificadores Bultos */
    sapr_pbulto_###VERSION###.scargar_tt_oids_bultos(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Identificadores Parciais */
    sapr_pparcial_###VERSION###.scargar_tt_oids_parciales(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Identificadores Cuentas */
    sapr_pcuentas_###VERSION###.scargar_tt_oids_cuentas_elem(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Identificadores Tipo Sector */
    sapr_psector_###VERSION###.scargar_tt_oids_tipo_sec_elem(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Identificadores IAC */
    scargar_tt_oids_iac_elem(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Identificadores Divisas/Denominaciones/MedioPago/UnidadMedida/Calidad */
    /*   - Remesas */
    sapr_pdivisa_###VERSION###.scargar_tt_oids_div_remesas(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* ===== Cargar refcursor ==== */

    /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
    par$ele_rc_elementos := fcargar_rc_elementos(par$selects => var$selects_int);
    var$selects          := var$selects + var$selects_int;
    var$registros        := var$registros + var$selects_int;

    /* - TABLA: Precintos Contenedores */
    par$ele_rc_cont_precintos := sapr_pcontenedor_###VERSION###.fcargar_rc_precintos(par$selects => var$selects_int);
    var$selects               := var$selects + var$selects_int;
    var$registros             := var$registros + var$selects_int;

    /* - TABLA: Cuentas */
    par$ele_rc_cuentas := sapr_pcuentas_###VERSION###.fcargar_rc_cuentas(par$selects => var$selects_int);
    var$selects        := var$selects + var$selects_int;
    var$registros      := var$registros + var$selects_int;

    /* - TABLA: Caracteristicas Tipo Sector */
    par$ele_rc_carac_tipo_sector := sapr_psector_###VERSION###.fcargar_rc_caractpsectores(par$selects => var$selects_int);
    var$selects                  := var$selects + var$selects_int;
    var$registros                := var$registros + var$selects_int;

    /* - TABLA: Divisas */
    sapr_pdivisa_###VERSION###.scargar_divisas(par$ele_rc_divisas,
                                          par$ele_rc_denominaciones,
                                          par$ele_rc_medio_pago,
                                          par$ele_rc_unidad_medida,
                                          par$ele_rc_calidad,
                                          par$ele_rc_valor_termino,
                                          var$selects_int);
    var$selects   := var$selects + var$selects_int;
    var$registros := var$registros + var$selects_int;

    /* - TABLA: ListaValor */
    par$ele_rc_lista_valor := fcargar_rc_lista_valor(par$selects => var$selects_int);
    var$selects            := var$selects + var$selects_int;
    var$registros          := var$registros + var$selects_int;

    /* - TABLA: Valores - Detallado Efectivos */
    par$ele_rc_val_det_efectivo := fcargar_rc_valores_det_efec(par$selects => var$selects_int);
    var$selects                 := var$selects + var$selects_int;
    var$registros               := var$registros + var$selects_int;

    /* - TABLA: Valores - Detallado Medio Pago */
    par$ele_rc_val_det_medio_pago := fcargar_rc_valores_det_mp(par$selects => var$selects_int);
    var$selects                   := var$selects + var$selects_int;
    var$registros                 := var$registros + var$selects_int;

    /* - TABLA: Valores - Totales */
    par$ele_rc_val_totales := fcargar_rc_valores_totales(par$selects => var$selects_int);
    var$selects            := var$selects + var$selects_int;
    var$registros          := var$registros + var$selects_int;

    /* - TABLA: IAC */
    par$ele_rc_iac := fcargar_rc_iac(par$selects => var$selects_int);
    var$selects    := var$selects + var$selects_int;
    var$registros  := var$registros + var$selects_int;

    /* - TABLA: IAC - Terminos */
    par$ele_rc_terminos_iac := fcargar_rc_terminos_iac(par$selects => var$selects_int);
    var$selects             := var$selects + var$selects_int;
    var$registros           := var$registros + var$selects_int;

    /* - TABLA: IAC - Valor Terminos */
    par$ele_rc_valor_termino_iac := fcargar_rc_valor_termino_iac(par$selects => var$selects_int);
    var$selects                  := var$selects + var$selects_int;
    var$registros                := var$registros + var$selects_int;

    IF par$ejecucion_interna = 0 THEN
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
                                                           var$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           var$cod_ejecucion);
    ELSE
      par$inserts := var$inserts;
      par$selects := var$selects;
    END IF;
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
                                                           'E',
                                                           var$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           var$cod_ejecucion);

      RAISE_APPLICATION_ERROR(-20101, var$err, true);

  END srecuperar_elementos;

  /* sguardar_elemento */
  PROCEDURE sguardar_elemento(par$esnuevo                    IN BOOLEAN,
                              par$bol_sustituicion           IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_gestion_bulto          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_baja                   IN BOOLEAN,
                              par$adocs_oid                  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$adocs_oid_cuenta           IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$adocs_oid_cuentasaldo      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_remesa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_remesa_origen  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_externo        IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_iac            IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_cod_recibo_salida  IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_usuario_resp       IN gepr_pcomon_###VERSION###.tipo$usrs_,
                              par$aremdoc_puesto_resp        IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_cod_ruta           IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_nel_parada         IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aremdoc_fyh_transporte     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$aremdoc_nel_cant_bultos    IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aremdoc_fyh_conteo_inicio  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$aremdoc_fyh_conteo_fin     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$aremdoc_oid_remesa_padre   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_remesa_sub     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_cod_cajero         IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_cod_nivel_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_cod_externo        IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aremdoc_cod_estado_abono   IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$avtrem_oid_remesa          IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtrem_oid_termino         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtrem_cod_termino         IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$avtrem_des_valor           IN gepr_pcomon_###VERSION###.tipo$descs_,
                              par$avtrem_obligatorio         IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$abuldoc_oid_bulto          IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_remesa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_externo        IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_iac            IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_iac_parciales  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_cod_precinto       IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$abuldoc_cod_bolsa          IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$abuldoc_oid_banco_ingreso  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_bol_banco_ing_mad  IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$abuldoc_usuario_resp       IN gepr_pcomon_###VERSION###.tipo$usrs_,
                              par$abuldoc_puesto_resp        IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$abuldoc_nel_cant_parciales IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$abuldoc_fyh_conteo_inicio  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$abuldoc_fyh_conteo_fin     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$abuldoc_fyh_proceso_leg    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                              par$abuldoc_oid_bulto_padre    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_bulto_sub      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_cod_nivel_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$abuldoc_bol_cuadrado       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$abuldoc_oid_tipo_formato   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_tipo_servicio  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtbul_oid_bulto           IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtbul_oid_termino         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtbul_cod_termino         IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$avtbul_des_valor           IN gepr_pcomon_###VERSION###.tipo$descs_,
                              par$avtbul_obligatorio         IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$apardoc_oid_remesa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_bulto          IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_parcial        IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_externo        IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_cod_precinto       IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$apardoc_oid_iac            IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_usuario_resp       IN gepr_pcomon_###VERSION###.tipo$usrs_,
                              par$apardoc_puesto_resp        IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$apardoc_nec_secuencia      IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$apardoc_oid_parcial_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_parcial_sub    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$apardoc_oid_tipo_formato   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtpar_oid_parcial         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtpar_oid_termino         IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$avtpar_cod_termino         IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$avtpar_des_valor           IN gepr_pcomon_###VERSION###.tipo$descs_,
                              par$avtpar_obligatorio         IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$aelemvalefe_tipo           IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemvalefe_oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_oid_divisa     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_oid_denom      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_bol_esbillete  IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$aelemvalefe_oid_unid_med   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemvalefe_cod_tipo_efec  IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemvalefe_num_importe    IN gepr_pcomon_###VERSION###.tipo$imps_,
                              par$aelemvalefe_nel_cantidad   IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aelemvalefe_cod_nvdetalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemvalefe_bol_ingresado  IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$aelemvalefe_oid_calidad    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_tipo           IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemval_mp_oid_valor      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_divisa     IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_oid_mediopago  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemval_mp_cod_tipo_mp    IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemval_mp_num_importe    IN gepr_pcomon_###VERSION###.tipo$imps_,
                              par$aelemval_mp_nel_cantidad   IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aelemval_mp_cod_nvdetalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemval_mp_bol_ingresado  IN gepr_pcomon_###VERSION###.tipo$nbols_,
                              par$aelemval_mp_nel_secuencia  IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$aelemter_mp_tipo           IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$aelemter_mp_oid_valor      IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemter_mp_oid_termino    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aelemter_mp_des_valor      IN gepr_pcomon_###VERSION###.tipo$descs_,
                              par$aelemter_mp_nel_ind_grupo  IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$usuario                    IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$deletes                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /*const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_elemento';*/

    /* variables locales */
    var$inserts_int      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$bol_validar_prec gepr_pcomon_###VERSION###.tipo$nbol_ := 1;

    /* cuentas por remesa */
    var$aremdoc_oid_cuenta      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aremdoc_oid_cuentasaldo gepr_pcomon_###VERSION###.tipo$oids_;
    /* cuentas por bulto */
    var$abuldoc_oid_cuenta      gepr_pcomon_###VERSION###.tipo$oids_;
    var$abuldoc_oid_cuentasaldo gepr_pcomon_###VERSION###.tipo$oids_;
    /* terminos por remesa */
    var$avtrem_oid_remesa  gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtrem_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtrem_cod_termino gepr_pcomon_###VERSION###.tipo$cods_;
    var$avtrem_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtrem_obligatorio gepr_pcomon_###VERSION###.tipo$nbols_;
    /* terminos por bulto */
    var$avtbul_oid_bulto   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtbul_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtbul_cod_termino gepr_pcomon_###VERSION###.tipo$cods_;
    var$avtbul_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtbul_obligatorio gepr_pcomon_###VERSION###.tipo$nbols_;
    /* terminos por parcial */
    var$avtpar_oid_parcial gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtpar_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtpar_cod_termino gepr_pcomon_###VERSION###.tipo$cods_;
    var$avtpar_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtpar_obligatorio gepr_pcomon_###VERSION###.tipo$nbols_;

  BEGIN

    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;

    /* Validar Remesas */
    IF par$bol_sustituicion = 1 OR NOT par$esnuevo THEN
      var$bol_validar_prec := 0;
    END IF;

    svalidar_elemento(par$aremdoc_cod_externo => par$aremdoc_cod_externo,
                      par$adocs_oid_cuenta    => par$adocs_oid_cuenta,
                      par$aremdoc_oid_remesa  => par$aremdoc_oid_remesa,
                      par$abuldoc_oid_bulto   => par$abuldoc_oid_bulto,
                      par$bol_validar_prec    => var$bol_validar_prec,
                      par$bol_sustituicion    => par$bol_sustituicion,
                      par$bol_gestion_bulto   => par$bol_gestion_bulto,
                      par$bol_baja            => par$bol_baja,
                      par$cod_cultura         => par$cod_cultura);

    IF NOT par$esnuevo THEN
      /* es una modificacion, debe ser deletado todos los registros y creado nuevamente */
      sdel_elementos(par$aremdoc_oid_remesa    => par$aremdoc_oid_remesa,
                     par$aremdoc_oid_documento => par$aremdoc_oid_documento,
                     par$bol_documento_altas   => 1,
                     par$deletes               => var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;

    END IF;

    /* Cuentas */
    FOR idx_doc IN par$adocs_oid.first .. par$adocs_oid.last LOOP

      FOR idx IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP
        IF par$adocs_oid(idx_doc) = par$aremdoc_oid_documento(idx) THEN
          var$aremdoc_oid_cuenta(idx) := par$adocs_oid_cuenta(idx_doc);
          var$aremdoc_oid_cuentasaldo(idx) := par$adocs_oid_cuentasaldo(idx_doc);
        END IF;
      END LOOP;

      FOR idx IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP
        IF par$adocs_oid(idx_doc) = par$abuldoc_oid_documento(idx) THEN
          var$abuldoc_oid_cuenta(idx) := par$adocs_oid_cuenta(idx_doc);
          var$abuldoc_oid_cuentasaldo(idx) := par$adocs_oid_cuentasaldo(idx_doc);
        END IF;
      END LOOP;

    END LOOP;

    /* Guardar Remesas */
    FOR idx IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP

      sapr_premesa_###VERSION###.sins_tremesa(par$oid_remesa            => par$aremdoc_oid_remesa(idx),
                                         par$oid_remesa_origen     => par$aremdoc_oid_remesa_origen(idx),
                                         par$oid_externo           => par$aremdoc_oid_externo(idx),
                                         par$oid_iac               => par$aremdoc_oid_iac(idx),
                                         par$oid_documento         => par$aremdoc_oid_documento(idx),
                                         par$oid_cuenta            => var$aremdoc_oid_cuenta(idx),
                                         par$cod_recibo_salida     => par$aremdoc_cod_recibo_salida(idx),
                                         par$cod_estado            => gepr_pcomon_###VERSION###.const$codEstadoRemesaTransito,
                                         par$usuario_resp          => par$aremdoc_usuario_resp(idx),
                                         par$puesto_resp           => par$aremdoc_puesto_resp(idx),
                                         par$cod_ruta              => par$aremdoc_cod_ruta(idx),
                                         par$nel_parada            => par$aremdoc_nel_parada(idx),
                                         par$fyh_transporte        => par$aremdoc_fyh_transporte(idx),
                                         par$nel_cantidad_bultos   => par$aremdoc_nel_cant_bultos(idx),
                                         par$fyh_conteo_inicio     => par$aremdoc_fyh_conteo_inicio(idx),
                                         par$fyh_conteo_fin        => par$aremdoc_fyh_conteo_fin(idx),
                                         par$oid_remesa_padre      => par$aremdoc_oid_remesa_padre(idx),
                                         par$oid_remesa_substituta => par$aremdoc_oid_remesa_sub(idx),
                                         par$cod_cajero            => par$aremdoc_cod_cajero(idx),
                                         par$cod_nivel_detalle     => par$aremdoc_cod_nivel_detalle(idx),
                                         par$cod_externo           => par$aremdoc_cod_externo(idx),
                                         par$bol_gestion_bulto     => par$bol_gestion_bulto,
                                         par$cod_estado_abono      => par$aremdoc_cod_estado_abono(idx),
                                         par$oid_cuenta_saldo      => var$aremdoc_oid_cuentasaldo(idx),
                                         par$usuario               => par$usuario,
                                         par$inserts               => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

      /* Insert en la tabla sapr_tdocumentoxelemento */
      sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$aremdoc_oid_documento(idx),
                                                        par$oid_remesa           => par$aremdoc_oid_remesa(idx),
                                                        par$oid_bulto            => null,
                                                        par$cod_usuario          => par$usuario,
                                                        par$oid_contenedor       => null,
                                                        par$oid_contenedor_padre => null,
                                                        par$oid_cont_mayor_nivel => null,
                                                        par$inserts              => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

    /* recuperar IAC - remesa */

    END LOOP;

    /* Guardar Bultos */
    FOR idx IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP

      sapr_pbulto_###VERSION###.sins_tbulto(par$oid_bulto           => par$abuldoc_oid_bulto(idx),
                                       par$oid_remesa          => par$abuldoc_oid_remesa(idx),
                                       par$oid_externo         => par$abuldoc_oid_externo(idx),
                                       par$oid_iac             => par$abuldoc_oid_iac(idx),
                                       par$oid_iac_parciales   => par$abuldoc_oid_iac_parciales(idx),
                                       par$oid_documento       => par$abuldoc_oid_documento(idx),
                                       par$oid_cuenta          => var$abuldoc_oid_cuenta(idx),
                                       par$cod_precinto        => par$abuldoc_cod_precinto(idx),
                                       par$cod_bolsa           => par$abuldoc_cod_bolsa(idx),
                                       par$oid_banco_ingreso   => par$abuldoc_oid_banco_ingreso(idx),
                                       par$bol_banco_ing_madre => par$abuldoc_bol_banco_ing_mad(idx),
                                       par$cod_estado          => gepr_pcomon_###VERSION###.const$codEstadoBultoTransito,
                                       par$usuario_resp        => par$abuldoc_usuario_resp(idx),
                                       par$puesto_resp         => par$abuldoc_puesto_resp(idx),
                                       par$nel_cant_parciales  => par$abuldoc_nel_cant_parciales(idx),
                                       par$fyh_conteo_inicio   => par$abuldoc_fyh_conteo_inicio(idx),
                                       par$fyh_conteo_fin      => par$abuldoc_fyh_conteo_fin(idx),
                                       par$fyh_proceso_leg     => par$abuldoc_fyh_proceso_leg(idx),
                                       par$oid_bulto_padre     => par$abuldoc_oid_bulto_padre(idx),
                                       par$oid_bulto_sub       => par$abuldoc_oid_bulto_sub(idx),
                                       par$cod_nivel_detalle   => par$abuldoc_cod_nivel_detalle(idx),
                                       par$bol_cuadrado        => par$abuldoc_bol_cuadrado(idx),
                                       par$oid_cuenta_saldo    => var$abuldoc_oid_cuentasaldo(idx),
                                       par$bol_gestion_bulto   => par$bol_gestion_bulto,
                                       par$usuario             => par$usuario,
                                       par$inserts             => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

      /* Insere o formato do bulto */
      sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                        par$oid_remesa      => par$abuldoc_oid_remesa(idx),
                                                        par$oid_bulto       => par$abuldoc_oid_bulto(idx),
                                                        par$oid_parcial     => null,
                                                        par$oid_lista_valor => par$abuldoc_oid_tipo_formato(idx),
                                                        par$cod_lista_valor => null,
                                                        par$cod_lista_tipo  => null,
                                                        par$cod_usuario     => par$usuario,
                                                        par$cod_cultura     => par$cod_cultura,
                                                        par$inserts         => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

      /* Insere o servicio do bulto */
      sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                        par$oid_remesa      => par$abuldoc_oid_remesa(idx),
                                                        par$oid_bulto       => par$abuldoc_oid_bulto(idx),
                                                        par$oid_parcial     => null,
                                                        par$oid_lista_valor => par$abuldoc_oid_tipo_servicio(idx),
                                                        par$cod_lista_valor => null,
                                                        par$cod_lista_tipo  => null,
                                                        par$cod_usuario     => par$usuario,
                                                        par$cod_cultura     => par$cod_cultura,
                                                        par$inserts         => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

      /* Insert en la tabla sapr_tdocumentoxelemento */
      sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$abuldoc_oid_documento(idx),
                                                        par$oid_remesa           => par$abuldoc_oid_remesa(idx),
                                                        par$oid_bulto            => par$abuldoc_oid_bulto(idx),
                                                        par$cod_usuario          => par$usuario,
                                                        par$oid_contenedor       => null,
                                                        par$oid_contenedor_padre => null,
                                                        par$oid_cont_mayor_nivel => null,
                                                        par$inserts              => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

    /* recuperar IAC - bulto */

    END LOOP;

    /* Guardar Parciales */
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$apardoc_oid_parcial) THEN
      FOR idx IN par$apardoc_oid_parcial.first .. par$apardoc_oid_parcial.last LOOP

        sapr_pparcial_###VERSION###.sins_tparcial(par$oid_parcial       => par$apardoc_oid_parcial(idx),
                                             par$oid_externo       => par$apardoc_oid_externo(idx),
                                             par$oid_bulto         => par$apardoc_oid_bulto(idx),
                                             par$cod_precinto      => par$apardoc_cod_precinto(idx),
                                             par$cod_estado        => gepr_pcomon_###VERSION###.const$codEstadoParcialPendient,
                                             par$oid_iac           => par$apardoc_oid_iac(idx),
                                             par$usuario_resp      => par$apardoc_usuario_resp(idx),
                                             par$puesto_resp       => par$apardoc_puesto_resp(idx),
                                             par$nec_secuencia     => par$apardoc_nec_secuencia(idx),
                                             par$oid_parcial_padre => par$apardoc_oid_parcial_padre(idx),
                                             par$oid_parcial_sub   => par$apardoc_oid_parcial_sub(idx),
                                             par$usuario           => par$usuario,
                                             par$inserts           => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;

        IF par$apardoc_oid_tipo_formato(idx) IS NOT NULL THEN

          /* Insere o formato do parcial */
          sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                            par$oid_remesa      => par$apardoc_oid_remesa(idx),
                                                            par$oid_bulto       => par$apardoc_oid_bulto(idx),
                                                            par$oid_parcial     => par$apardoc_oid_parcial(idx),
                                                            par$oid_lista_valor => par$apardoc_oid_tipo_formato(idx),
                                                            par$cod_lista_valor => null,
                                                            par$cod_lista_tipo  => null,
                                                            par$cod_usuario     => par$usuario,
                                                            par$cod_cultura     => par$cod_cultura,
                                                            par$inserts         => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;

        END IF;

      /* recuperar IAC - parcial */

      END LOOP;
    END IF;

    /* Guardar Valores Declarados Efectivo*/
    sguardar_valores_efectivo(par$aelemvalefe_tipo          => par$aelemvalefe_tipo,
                              par$aelemvalefe_oid_remesa    => par$aelemvalefe_oid_remesa,
                              par$aelemvalefe_oid_bulto     => par$aelemvalefe_oid_bulto,
                              par$aelemvalefe_oid_parcial   => par$aelemvalefe_oid_parcial,
                              par$aelemvalefe_oid_divisa    => par$aelemvalefe_oid_divisa,
                              par$aelemvalefe_oid_denom     => par$aelemvalefe_oid_denom,
                              par$aelemvalefe_bol_esbillete => par$aelemvalefe_bol_esbillete,
                              par$aelemvalefe_oid_unid_med  => par$aelemvalefe_oid_unid_med,
                              par$aelemvalefe_cod_tipo_efec => par$aelemvalefe_cod_tipo_efec,
                              par$aelemvalefe_num_importe   => par$aelemvalefe_num_importe,
                              par$aelemvalefe_nel_cantidad  => par$aelemvalefe_nel_cantidad,
                              par$aelemvalefe_cod_nvdetalle => par$aelemvalefe_cod_nvdetalle,
                              par$aelemvalefe_bol_ingresado => par$aelemvalefe_bol_ingresado,
                              par$aelemvalefe_oid_calidad   => par$aelemvalefe_oid_calidad,
                              par$usuario                   => par$usuario,
                              par$inserts                   => var$inserts_int);
    par$inserts := par$inserts + var$inserts_int;

    /* Guardar Valores Declarados Medio Pago*/
    sguardar_valores_mediopago(par$aelemval_mp_tipo          => par$aelemval_mp_tipo,
                               par$aelemval_mp_oid_valor     => par$aelemval_mp_oid_valor,
                               par$aelemval_mp_oid_remesa    => par$aelemval_mp_oid_remesa,
                               par$aelemval_mp_oid_bulto     => par$aelemval_mp_oid_bulto,
                               par$aelemval_mp_oid_parcial   => par$aelemval_mp_oid_parcial,
                               par$aelemval_mp_oid_divisa    => par$aelemval_mp_oid_divisa,
                               par$aelemval_mp_oid_mediopago => par$aelemval_mp_oid_mediopago,
                               par$aelemval_mp_cod_tipo_mp   => par$aelemval_mp_cod_tipo_mp,
                               par$aelemval_mp_num_importe   => par$aelemval_mp_num_importe,
                               par$aelemval_mp_nel_cantidad  => par$aelemval_mp_nel_cantidad,
                               par$aelemval_mp_cod_nvdetalle => par$aelemval_mp_cod_nvdetalle,
                               par$aelemval_mp_bol_ingresado => par$aelemval_mp_bol_ingresado,
                               par$aelemval_mp_nel_secuencia => par$aelemval_mp_nel_secuencia,
                               par$aelemter_mp_tipo          => par$aelemter_mp_tipo,
                               par$aelemter_mp_oid_valor     => par$aelemter_mp_oid_valor,
                               par$aelemter_mp_oid_termino   => par$aelemter_mp_oid_termino,
                               par$aelemter_mp_des_valor     => par$aelemter_mp_des_valor,
                               par$aelemter_mp_nel_ind_grupo => par$aelemter_mp_nel_ind_grupo,
                               par$usuario                   => par$usuario,
                               par$inserts                   => var$inserts_int);
    par$inserts := par$inserts + var$inserts_int;

    /* Recuperar IAC Elementos */
    /* terminos por remesa */
    var$avtrem_oid_remesa  := par$avtrem_oid_remesa;
    var$avtrem_oid_termino := par$avtrem_oid_termino;
    var$avtrem_cod_termino := par$avtrem_cod_termino;
    var$avtrem_des_valor   := par$avtrem_des_valor;
    var$avtrem_obligatorio := par$avtrem_obligatorio;
    /* terminos por bulto */
    var$avtbul_oid_bulto   := par$avtbul_oid_bulto;
    var$avtbul_oid_termino := par$avtbul_oid_termino;
    var$avtbul_cod_termino := par$avtbul_cod_termino;
    var$avtbul_des_valor   := par$avtbul_des_valor;
    var$avtbul_obligatorio := par$avtbul_obligatorio;
    /* terminos por parcial */
    var$avtpar_oid_parcial := par$avtpar_oid_parcial;
    var$avtpar_oid_termino := par$avtpar_oid_termino;
    var$avtpar_cod_termino := par$avtpar_cod_termino;
    var$avtpar_des_valor   := par$avtpar_des_valor;
    var$avtpar_obligatorio := par$avtpar_obligatorio;

    srecupera_iac_elementos(par$adocs_oid        => par$adocs_oid,
                            par$adocs_oid_cuenta => var$abuldoc_oid_cuenta,
                            /* arrays de terminos por remesa */
                            par$aremdoc_oid_remesa    => par$aremdoc_oid_remesa,
                            par$aremdoc_oid_documento => par$aremdoc_oid_documento,
                            par$avtrem_oid_remesa     => var$avtrem_oid_remesa,
                            par$avtrem_oid_termino    => var$avtrem_oid_termino,
                            par$avtrem_cod_termino    => var$avtrem_cod_termino,
                            par$avtrem_des_valor      => var$avtrem_des_valor,
                            par$avtrem_obligatorio    => var$avtrem_obligatorio,
                            /* arrays de terminos por bulto */
                            par$abuldoc_oid_bulto         => par$abuldoc_oid_bulto,
                            par$abuldoc_oid_documento     => par$abuldoc_oid_documento,
                            par$abuldoc_oid_tipo_servicio => par$abuldoc_oid_tipo_servicio,
                            par$avtbul_oid_bulto          => var$avtbul_oid_bulto,
                            par$avtbul_oid_termino        => var$avtbul_oid_termino,
                            par$avtbul_cod_termino        => var$avtbul_cod_termino,
                            par$avtbul_des_valor          => var$avtbul_des_valor,
                            par$avtbul_obligatorio        => var$avtbul_obligatorio,
                            /* arrays de terminos por parcial */
                            par$apardoc_oid_bulto  => par$apardoc_oid_bulto,
                            par$avtpar_oid_parcial => var$avtpar_oid_parcial,
                            par$avtpar_oid_termino => var$avtpar_oid_termino,
                            par$avtpar_cod_termino => var$avtpar_cod_termino,
                            par$avtpar_des_valor   => var$avtpar_des_valor,
                            par$avtpar_obligatorio => var$avtpar_obligatorio);

    /* Guardar Valores Terminos*/
    sguardar_valores_terminos(par$avtrem_oid_remesa  => var$avtrem_oid_remesa,
                              par$avtrem_oid_termino => var$avtrem_oid_termino,
                              par$avtrem_des_valor   => var$avtrem_des_valor,
                              par$avtbul_oid_bulto   => var$avtbul_oid_bulto,
                              par$avtbul_oid_termino => var$avtbul_oid_termino,
                              par$avtbul_des_valor   => var$avtbul_des_valor,
                              par$avtpar_oid_parcial => var$avtpar_oid_parcial,
                              par$avtpar_oid_termino => var$avtpar_oid_termino,
                              par$avtpar_des_valor   => var$avtpar_des_valor,
                              par$cod_usuario        => par$usuario,
                              par$inserts            => var$inserts_int);
    par$inserts := par$inserts + var$inserts_int;

  END sguardar_elemento;

  /*Validar elemento*/
  PROCEDURE svalidar_elemento(par$aremdoc_cod_externo IN gepr_pcomon_###VERSION###.tipo$cods_,
                              par$adocs_oid_cuenta    IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aremdoc_oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$abuldoc_oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                              par$bol_validar_prec    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_sustituicion    IN gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_gestion_bulto   in gepr_pcomon_###VERSION###.tipo$nbol_,
                              par$bol_baja            IN BOOLEAN,
                              par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_) is

    var$cods_externos       varchar2(1000) := '';
    var$oid_delegacion      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_tipo_sector     gepr_pcomon_###VERSION###.tipo$oid_;
    var$rc_elementos        sys_refcursor;
    var$oid_remesa_ori      gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_precinto_remesa gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_estado_remesa   gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_bulto_ori       gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_precinto_bulto  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_estado_bulto    gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_estado          gepr_pcomon_###VERSION###.tipo$cod_;
    var$error               gepr_pcomon_###VERSION###.tipo$obs_;
    var$countr              INTEGER;
    var$count               INTEGER;
    var$validar             INTEGER := 1;

    /* CURSOR DE BULTOS */
    CURSOR cur$bultos IS
      SELECT B.OID_BULTO
        FROM SAPR_TBULTO B
       INNER JOIN SAPR_GTT_TOIDS T
          ON T.OID = B.OID_REMESA
       WHERE T.COD_CALIFICADOR = 'COD_REMESA_VAL';

  BEGIN

    IF par$bol_validar_prec = 1 THEN

      SELECT P.OID_DELEGACION, S.OID_TIPO_SECTOR
        INTO var$oid_delegacion, var$oid_tipo_sector
        FROM SAPR_TCUENTA C
       INNER JOIN GEPR_TSECTOR S
          ON S.OID_SECTOR = C.OID_SECTOR
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = S.OID_PLANTA
       WHERE C.OID_CUENTA = par$adocs_oid_cuenta(1);

      FOR idx IN par$aremdoc_cod_externo.FIRST .. par$aremdoc_cod_externo.LAST LOOP
        INSERT INTO SAPR_GTT_TOIDS
          (OID, COD_CALIFICADOR)
        VALUES
          (par$aremdoc_cod_externo(idx), 'COD_EXTERNO');
      END LOOP;

      FOR rec IN (SELECT R.COD_EXTERNO,
                         SUM(CASE
                               WHEN R.COD_ESTADO IN ('PE', 'ET') OR
                                    DOCE.COD_ESTADO_DOCXELEMENTO = 'T' THEN
                                1
                               ELSE
                                0
                             END) EXISTE
                    FROM SAPR_TDOCUMENTOXELEMENTO DOCE
                   INNER JOIN SAPR_TREMESA R
                      ON R.OID_REMESA = DOCE.OID_REMESA
                   INNER JOIN SAPR_TCUENTA C
                      ON C.OID_CUENTA = R.OID_CUENTA
                   INNER JOIN GEPR_TSECTOR S
                      ON S.OID_SECTOR = C.OID_SECTOR
                   INNER JOIN GEPR_TPLANTA P
                      ON P.OID_PLANTA = S.OID_PLANTA
                     AND P.OID_DELEGACION = var$oid_delegacion
                   INNER JOIN SAPR_GTT_TOIDS T
                      ON T.OID = R.COD_EXTERNO
                   WHERE T.COD_CALIFICADOR = 'COD_EXTERNO'
                   GROUP BY R.COD_EXTERNO
                  HAVING(SUM(CASE
                    WHEN R.COD_ESTADO IN ('PE', 'ET') OR
                         DOCE.COD_ESTADO_DOCXELEMENTO = 'T' THEN
                     1
                    ELSE
                     0
                  END) > 0)) LOOP
        var$cods_externos := var$cods_externos || ', ' || rec.cod_externo;
      END LOOP;

      IF length(var$cods_externos) > 1 then
        /*remove a primeira virgula*/
        var$cods_externos := substr(var$cods_externos, 2);
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    '028_msg_doc_elementosremesasexisteentransicion',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDoc,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    var$cods_externos,
                                                                    1));
      END IF;

    END IF;

    IF par$bol_sustituicion = 0 THEN

      IF var$oid_tipo_sector IS NULL THEN

        FOR idx IN par$aremdoc_oid_remesa.FIRST .. par$aremdoc_oid_remesa.LAST LOOP

          IF par$aremdoc_oid_remesa(idx) IS NOT NULL THEN
            SELECT S.OID_TIPO_SECTOR
              INTO var$oid_tipo_sector
              FROM SAPR_TREMESA R
             INNER JOIN SAPR_TCUENTA C
                ON C.OID_CUENTA = R.OID_CUENTA
             INNER JOIN GEPR_TSECTOR S
                ON S.OID_SECTOR = C.OID_SECTOR
             WHERE R.OID_REMESA = par$aremdoc_oid_remesa(idx);
          END IF;

          IF var$oid_tipo_sector IS NOT NULL THEN
            EXIT;
          END IF;
        END LOOP;

      END IF;

      BEGIN
        SELECT COUNT(1)
          INTO var$count
          FROM GEPR_TTIPO_SECTOR TS
         INNER JOIN GEPR_TCARACTTIPOSECTORXTIPOSEC CT
            ON CT.OID_TIPO_SECTOR = TS.OID_TIPO_SECTOR
         INNER JOIN GEPR_TCARACT_TIPOSECTOR C
            ON C.OID_CARACT_TIPOSECTOR = CT.OID_CARACT_TIPOSECTOR
         WHERE TS.OID_TIPO_SECTOR = var$oid_tipo_sector
           AND C.COD_CARACT_TIPOSECTOR IN ('REC', 'CLAS');
      EXCEPTION
        WHEN no_data_found THEN
          var$count := 0;
      END;

    END IF;

  END;

  /* sguardar_elemento_acta */
  PROCEDURE sguardar_elemento_acta(par$bol_gestion_bulto IN gepr_pcomon_###VERSION###.tipo$nel_,

                                   /* Documento */
                                   par$adocs_oid             IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adocs_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adocs_oid_cuentasaldo IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Remesa */
                                   par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Remesa - arrays de terminos por remesa */
                                   par$avtrem_oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtrem_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtrem_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$avtrem_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtrem_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                                   /* Bulto - arrays */
                                   par$abuldoc_oid_bulto          IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_remesa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_externo        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_iac            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_iac_parciales  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_cod_precinto       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$abuldoc_cod_bolsa          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$abuldoc_oid_banco_ingreso  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_bol_banco_ing_mad  IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$abuldoc_usuario_resp       IN gepr_pcomon_###VERSION###.tipo$usrs_,
                                   par$abuldoc_puesto_resp        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$abuldoc_nel_cant_parciales IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$abuldoc_fyh_conteo_inicio  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$abuldoc_fyh_conteo_fin     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$abuldoc_fyh_proceso_leg    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$abuldoc_oid_bulto_padre    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_bulto_sub      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_cod_nivel_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$abuldoc_bol_cuadrado       IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$abuldoc_oid_tipo_formato   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$abuldoc_oid_tipo_servicio  IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Bulto - arrays de terminos por bulto */
                                   par$avtbul_oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtbul_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtbul_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$avtbul_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtbul_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                                   /* Parcial - arrays */
                                   par$apardoc_oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_externo       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_cod_precinto      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$apardoc_oid_iac           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_usuario_resp      IN gepr_pcomon_###VERSION###.tipo$usrs_,
                                   par$apardoc_puesto_resp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$apardoc_nec_secuencia     IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$apardoc_oid_parcial_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_parcial_sub   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$apardoc_oid_tipo_formato  IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Parcial - arrays de terminos por parcial */
                                   par$avtpar_oid_parcial IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtpar_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtpar_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$avtpar_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtpar_obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,

                                   /* Elemento - Valores Efectivo */
                                   par$aelemvalefe_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemvalefe_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_oid_denom     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_bol_esbillete IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$aelemvalefe_oid_unid_med  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemvalefe_cod_tipo_efec IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemvalefe_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aelemvalefe_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$aelemvalefe_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemvalefe_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$aelemvalefe_oid_calidad   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* Elemento - Valores Medio Pago */
                                   par$aelemval_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemval_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_oid_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemval_mp_cod_tipo_mp   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemval_mp_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aelemval_mp_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$aelemval_mp_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemval_mp_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                   par$aelemval_mp_nel_secuencia IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   /* Elemento - Terminos medio pago */
                                   par$aelemter_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelemter_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemter_mp_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelemter_mp_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$aelemter_mp_nel_ind_grupo IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   /* informacion de ejecucion */
                                   par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$deletes     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /*const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_elemento';*/

    /* variables locales */
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;

    /* cuentas por remesa */
    var$aremdoc_oid_cuenta      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aremdoc_oid_cuentasaldo gepr_pcomon_###VERSION###.tipo$oids_;
    /* cuentas por bulto */
    var$abuldoc_oid_cuenta      gepr_pcomon_###VERSION###.tipo$oids_;
    var$abuldoc_oid_cuentasaldo gepr_pcomon_###VERSION###.tipo$oids_;
    /* terminos por remesa */
    var$avtrem_oid_remesa  gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtrem_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtrem_cod_termino gepr_pcomon_###VERSION###.tipo$cods_;
    var$avtrem_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtrem_obligatorio gepr_pcomon_###VERSION###.tipo$nbols_;
    /* terminos por bulto */
    var$avtbul_oid_bulto   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtbul_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtbul_cod_termino gepr_pcomon_###VERSION###.tipo$cods_;
    var$avtbul_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtbul_obligatorio gepr_pcomon_###VERSION###.tipo$nbols_;
    /* terminos por parcial */
    var$avtpar_oid_parcial gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtpar_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtpar_cod_termino gepr_pcomon_###VERSION###.tipo$cods_;
    var$avtpar_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtpar_obligatorio gepr_pcomon_###VERSION###.tipo$nbols_;

  BEGIN

    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;

    /* Validar Remesas */
    /* TO DO: svalidar_elementos(); */

    /* es una modificacion, debe ser deletado todos los registros y creado nuevamente */
    sdel_elementos(par$aremdoc_oid_remesa    => par$aremdoc_oid_remesa,
                   par$aremdoc_oid_documento => par$aremdoc_oid_documento,
                   par$bol_documento_altas   => 0,
                   par$deletes               => var$deletes_int);
    par$deletes := par$deletes + var$deletes_int;

    /* Cuentas */
    FOR idx_doc IN par$adocs_oid.first .. par$adocs_oid.last LOOP

      FOR idx IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP
        IF par$adocs_oid(idx_doc) = par$aremdoc_oid_documento(idx) THEN
          var$aremdoc_oid_cuenta(idx) := par$adocs_oid_cuenta(idx_doc);
          var$aremdoc_oid_cuentasaldo(idx) := par$adocs_oid_cuentasaldo(idx_doc);
        END IF;
      END LOOP;

      FOR idx IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP
        IF par$adocs_oid(idx_doc) = par$abuldoc_oid_documento(idx) THEN
          var$abuldoc_oid_cuenta(idx) := par$adocs_oid_cuenta(idx_doc);
          var$abuldoc_oid_cuentasaldo(idx) := par$adocs_oid_cuentasaldo(idx_doc);
        END IF;
      END LOOP;

    END LOOP;

    /* Guardar Remesas */
    FOR idx IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP

      sapr_premesa_###VERSION###.supd_tremesa(par$oid_remesa       => par$aremdoc_oid_remesa(idx),
                                         par$oid_documento    => par$aremdoc_oid_documento(idx),
                                         par$oid_cuenta_mov   => var$aremdoc_oid_cuenta(idx),
                                         par$oid_cuenta_saldo => var$aremdoc_oid_cuentasaldo(idx),
                                         par$cod_usuario      => par$usuario,
                                         par$cod_estado       => gepr_pcomon_###VERSION###.const$codEstadoRemesaTransito,
                                         par$updates          => var$updates_int);
      par$updates := par$updates + var$updates_int;

      /* Insert en la tabla sapr_tdocumentoxelemento */
      sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$aremdoc_oid_documento(idx),
                                                        par$oid_remesa           => par$aremdoc_oid_remesa(idx),
                                                        par$oid_bulto            => null,
                                                        par$cod_usuario          => par$usuario,
                                                        par$oid_contenedor       => null,
                                                        par$oid_contenedor_padre => null,
                                                        par$oid_cont_mayor_nivel => null,
                                                        par$inserts              => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

    END LOOP;

    /* Guardar Bultos */
    FOR idx IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP

      IF NOT
          sapr_pbulto_###VERSION###.fregistro_existe(par$abuldoc_oid_bulto(idx)) THEN

        sapr_pbulto_###VERSION###.sins_tbulto(par$oid_bulto           => par$abuldoc_oid_bulto(idx),
                                         par$oid_remesa          => par$abuldoc_oid_remesa(idx),
                                         par$oid_externo         => par$abuldoc_oid_externo(idx),
                                         par$oid_iac             => par$abuldoc_oid_iac(idx),
                                         par$oid_iac_parciales   => par$abuldoc_oid_iac_parciales(idx),
                                         par$oid_documento       => par$abuldoc_oid_documento(idx),
                                         par$oid_cuenta          => var$abuldoc_oid_cuenta(idx),
                                         par$cod_precinto        => par$abuldoc_cod_precinto(idx),
                                         par$cod_bolsa           => par$abuldoc_cod_bolsa(idx),
                                         par$oid_banco_ingreso   => par$abuldoc_oid_banco_ingreso(idx),
                                         par$bol_banco_ing_madre => par$abuldoc_bol_banco_ing_mad(idx),
                                         par$cod_estado          => gepr_pcomon_###VERSION###.const$codEstadoBultoTransito,
                                         par$usuario_resp        => par$abuldoc_usuario_resp(idx),
                                         par$puesto_resp         => par$abuldoc_puesto_resp(idx),
                                         par$nel_cant_parciales  => par$abuldoc_nel_cant_parciales(idx),
                                         par$fyh_conteo_inicio   => par$abuldoc_fyh_conteo_inicio(idx),
                                         par$fyh_conteo_fin      => par$abuldoc_fyh_conteo_fin(idx),
                                         par$fyh_proceso_leg     => par$abuldoc_fyh_proceso_leg(idx),
                                         par$oid_bulto_padre     => par$abuldoc_oid_bulto_padre(idx),
                                         par$oid_bulto_sub       => par$abuldoc_oid_bulto_sub(idx),
                                         par$cod_nivel_detalle   => par$abuldoc_cod_nivel_detalle(idx),
                                         par$bol_cuadrado        => par$abuldoc_bol_cuadrado(idx),
                                         par$oid_cuenta_saldo    => var$abuldoc_oid_cuentasaldo(idx),
                                         par$bol_gestion_bulto   => par$bol_gestion_bulto,
                                         par$usuario             => par$usuario,
                                         par$inserts             => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;

        /* Insere o formato do bulto */
        sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                          par$oid_remesa      => par$abuldoc_oid_remesa(idx),
                                                          par$oid_bulto       => par$abuldoc_oid_bulto(idx),
                                                          par$oid_parcial     => null,
                                                          par$oid_lista_valor => par$abuldoc_oid_tipo_formato(idx),
                                                          par$cod_lista_valor => null,
                                                          par$cod_lista_tipo  => null,
                                                          par$cod_usuario     => par$usuario,
                                                          par$cod_cultura     => par$cod_cultura,
                                                          par$inserts         => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;

        /* Insere o servicio do bulto */
        sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                          par$oid_remesa      => par$abuldoc_oid_remesa(idx),
                                                          par$oid_bulto       => par$abuldoc_oid_bulto(idx),
                                                          par$oid_parcial     => null,
                                                          par$oid_lista_valor => par$abuldoc_oid_tipo_servicio(idx),
                                                          par$cod_lista_valor => null,
                                                          par$cod_lista_tipo  => null,
                                                          par$cod_usuario     => par$usuario,
                                                          par$cod_cultura     => par$cod_cultura,
                                                          par$inserts         => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;

      ELSE
        sapr_pbulto_###VERSION###.supd_tbulto(par$oid_bulto        => par$abuldoc_oid_bulto(idx),
                                         par$oid_documento    => par$abuldoc_oid_documento(idx),
                                         par$oid_cuenta_mov   => var$abuldoc_oid_cuenta(idx),
                                         par$oid_cuenta_saldo => var$abuldoc_oid_cuentasaldo(idx),
                                         par$cod_usuario      => par$usuario,
                                         par$cod_estado       => gepr_pcomon_###VERSION###.const$codEstadoBultoTransito,
                                         par$updates          => var$updates_int);
        par$updates := par$updates + var$updates_int;
      END IF;

      /* Insert en la tabla sapr_tdocumentoxelemento */
      sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$abuldoc_oid_documento(idx),
                                                        par$oid_remesa           => par$abuldoc_oid_remesa(idx),
                                                        par$oid_bulto            => par$abuldoc_oid_bulto(idx),
                                                        par$cod_usuario          => par$usuario,
                                                        par$oid_contenedor       => null,
                                                        par$oid_contenedor_padre => null,
                                                        par$oid_cont_mayor_nivel => null,
                                                        par$inserts              => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

    END LOOP;

    /* Guardar Parciales */
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$apardoc_oid_parcial) THEN
      FOR idx IN par$apardoc_oid_parcial.first .. par$apardoc_oid_parcial.last LOOP

        IF NOT
            sapr_pparcial_###VERSION###.fregistro_existe(par$apardoc_oid_parcial(idx)) THEN
          sapr_pparcial_###VERSION###.sins_tparcial(par$oid_parcial       => par$apardoc_oid_parcial(idx),
                                               par$oid_externo       => par$apardoc_oid_externo(idx),
                                               par$oid_bulto         => par$apardoc_oid_bulto(idx),
                                               par$cod_precinto      => par$apardoc_cod_precinto(idx),
                                               par$cod_estado        => gepr_pcomon_###VERSION###.const$codEstadoParcialPendient,
                                               par$oid_iac           => par$apardoc_oid_iac(idx),
                                               par$usuario_resp      => par$apardoc_usuario_resp(idx),
                                               par$puesto_resp       => par$apardoc_puesto_resp(idx),
                                               par$nec_secuencia     => par$apardoc_nec_secuencia(idx),
                                               par$oid_parcial_padre => par$apardoc_oid_parcial_padre(idx),
                                               par$oid_parcial_sub   => par$apardoc_oid_parcial_sub(idx),
                                               par$usuario           => par$usuario,
                                               par$inserts           => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;

          IF par$apardoc_oid_tipo_formato(idx) IS NOT NULL THEN

            /* Insere o formato da parcial */
            sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => null,
                                                              par$oid_remesa      => par$apardoc_oid_remesa(idx),
                                                              par$oid_bulto       => par$apardoc_oid_bulto(idx),
                                                              par$oid_parcial     => par$apardoc_oid_parcial(idx),
                                                              par$oid_lista_valor => par$apardoc_oid_tipo_formato(idx),
                                                              par$cod_lista_valor => null,
                                                              par$cod_lista_tipo  => null,
                                                              par$cod_usuario     => par$usuario,
                                                              par$cod_cultura     => par$cod_cultura,
                                                              par$inserts         => var$inserts_int);
            par$inserts := par$inserts + var$inserts_int;

          END IF;

        END IF;

      END LOOP;
    END IF;

    /* Guardar Valores Declarados Efectivo*/
    sguardar_valores_efectivo(par$aelemvalefe_tipo          => par$aelemvalefe_tipo,
                              par$aelemvalefe_oid_remesa    => par$aelemvalefe_oid_remesa,
                              par$aelemvalefe_oid_bulto     => par$aelemvalefe_oid_bulto,
                              par$aelemvalefe_oid_parcial   => par$aelemvalefe_oid_parcial,
                              par$aelemvalefe_oid_divisa    => par$aelemvalefe_oid_divisa,
                              par$aelemvalefe_oid_denom     => par$aelemvalefe_oid_denom,
                              par$aelemvalefe_bol_esbillete => par$aelemvalefe_bol_esbillete,
                              par$aelemvalefe_oid_unid_med  => par$aelemvalefe_oid_unid_med,
                              par$aelemvalefe_cod_tipo_efec => par$aelemvalefe_cod_tipo_efec,
                              par$aelemvalefe_num_importe   => par$aelemvalefe_num_importe,
                              par$aelemvalefe_nel_cantidad  => par$aelemvalefe_nel_cantidad,
                              par$aelemvalefe_cod_nvdetalle => par$aelemvalefe_cod_nvdetalle,
                              par$aelemvalefe_bol_ingresado => par$aelemvalefe_bol_ingresado,
                              par$aelemvalefe_oid_calidad   => par$aelemvalefe_oid_calidad,
                              par$usuario                   => par$usuario,
                              par$inserts                   => var$inserts_int);
    par$inserts := par$inserts + var$inserts_int;

    /* Guardar Valores Declarados Medio Pago*/
    sguardar_valores_mediopago(par$aelemval_mp_tipo          => par$aelemval_mp_tipo,
                               par$aelemval_mp_oid_valor     => par$aelemval_mp_oid_valor,
                               par$aelemval_mp_oid_remesa    => par$aelemval_mp_oid_remesa,
                               par$aelemval_mp_oid_bulto     => par$aelemval_mp_oid_bulto,
                               par$aelemval_mp_oid_parcial   => par$aelemval_mp_oid_parcial,
                               par$aelemval_mp_oid_divisa    => par$aelemval_mp_oid_divisa,
                               par$aelemval_mp_oid_mediopago => par$aelemval_mp_oid_mediopago,
                               par$aelemval_mp_cod_tipo_mp   => par$aelemval_mp_cod_tipo_mp,
                               par$aelemval_mp_num_importe   => par$aelemval_mp_num_importe,
                               par$aelemval_mp_nel_cantidad  => par$aelemval_mp_nel_cantidad,
                               par$aelemval_mp_cod_nvdetalle => par$aelemval_mp_cod_nvdetalle,
                               par$aelemval_mp_bol_ingresado => par$aelemval_mp_bol_ingresado,
                               par$aelemval_mp_nel_secuencia => par$aelemval_mp_nel_secuencia,
                               par$aelemter_mp_tipo          => par$aelemter_mp_tipo,
                               par$aelemter_mp_oid_valor     => par$aelemter_mp_oid_valor,
                               par$aelemter_mp_oid_termino   => par$aelemter_mp_oid_termino,
                               par$aelemter_mp_des_valor     => par$aelemter_mp_des_valor,
                               par$aelemter_mp_nel_ind_grupo => par$aelemter_mp_nel_ind_grupo,
                               par$usuario                   => par$usuario,
                               par$inserts                   => var$inserts_int);
    par$inserts := par$inserts + var$inserts_int;

    /* Recuperar IAC Elementos */
    BEGIN

      /* terminos por remesa */
      var$avtrem_oid_remesa  := par$avtrem_oid_remesa;
      var$avtrem_oid_termino := par$avtrem_oid_termino;
      var$avtrem_cod_termino := par$avtrem_cod_termino;
      var$avtrem_des_valor   := par$avtrem_des_valor;
      var$avtrem_obligatorio := par$avtrem_obligatorio;
      /* terminos por bulto */
      var$avtbul_oid_bulto   := par$avtbul_oid_bulto;
      var$avtbul_oid_termino := par$avtbul_oid_termino;
      var$avtbul_cod_termino := par$avtbul_cod_termino;
      var$avtbul_des_valor   := par$avtbul_des_valor;
      var$avtbul_obligatorio := par$avtbul_obligatorio;
      /* terminos por parcial */
      var$avtpar_oid_parcial := par$avtpar_oid_parcial;
      var$avtpar_oid_termino := par$avtpar_oid_termino;
      var$avtpar_cod_termino := par$avtpar_cod_termino;
      var$avtpar_des_valor   := par$avtpar_des_valor;
      var$avtpar_obligatorio := par$avtpar_obligatorio;

      srecupera_iac_elementos(par$adocs_oid        => par$adocs_oid,
                              par$adocs_oid_cuenta => var$abuldoc_oid_cuenta,
                              /* arrays de terminos por remesa */
                              par$aremdoc_oid_remesa    => par$aremdoc_oid_remesa,
                              par$aremdoc_oid_documento => par$aremdoc_oid_documento,
                              par$avtrem_oid_remesa     => var$avtrem_oid_remesa,
                              par$avtrem_oid_termino    => var$avtrem_oid_termino,
                              par$avtrem_cod_termino    => var$avtrem_cod_termino,
                              par$avtrem_des_valor      => var$avtrem_des_valor,
                              par$avtrem_obligatorio    => var$avtrem_obligatorio,
                              /* arrays de terminos por bulto */
                              par$abuldoc_oid_bulto         => par$abuldoc_oid_bulto,
                              par$abuldoc_oid_documento     => par$abuldoc_oid_documento,
                              par$abuldoc_oid_tipo_servicio => par$abuldoc_oid_tipo_servicio,
                              par$avtbul_oid_bulto          => var$avtbul_oid_bulto,
                              par$avtbul_oid_termino        => var$avtbul_oid_termino,
                              par$avtbul_cod_termino        => var$avtbul_cod_termino,
                              par$avtbul_des_valor          => var$avtbul_des_valor,
                              par$avtbul_obligatorio        => var$avtbul_obligatorio,
                              /* arrays de terminos por parcial */
                              par$apardoc_oid_bulto  => par$apardoc_oid_bulto,
                              par$avtpar_oid_parcial => var$avtpar_oid_parcial,
                              par$avtpar_oid_termino => var$avtpar_oid_termino,
                              par$avtpar_cod_termino => var$avtpar_cod_termino,
                              par$avtpar_des_valor   => var$avtpar_des_valor,
                              par$avtpar_obligatorio => var$avtpar_obligatorio);
    END;

    /* Guardar Valores Terminos*/
    sguardar_valores_terminos(par$avtrem_oid_remesa  => var$avtrem_oid_remesa,
                              par$avtrem_oid_termino => var$avtrem_oid_termino,
                              par$avtrem_des_valor   => var$avtrem_des_valor,
                              par$avtbul_oid_bulto   => var$avtbul_oid_bulto,
                              par$avtbul_oid_termino => var$avtbul_oid_termino,
                              par$avtbul_des_valor   => var$avtbul_des_valor,
                              par$avtpar_oid_parcial => var$avtpar_oid_parcial,
                              par$avtpar_oid_termino => var$avtpar_oid_termino,
                              par$avtpar_des_valor   => var$avtpar_des_valor,
                              par$cod_usuario        => par$usuario,
                              par$inserts            => var$inserts_int);
    par$inserts := par$inserts + var$inserts_int;

  END sguardar_elemento_acta;

  /* stratar_reenvio_baja */
  PROCEDURE stratar_reenvio_baja(par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$abuldoc_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$abuldoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$abuldoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$cod_cultura           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$bol_gestion_bulto     in gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$bol_baja              IN BOOLEAN,
                                 par$inserts               OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates               OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$deletes               OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$selects               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'stratar_reenvio_baja';

    var$inserts_int         gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int         gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int         gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$oid_documento       gepr_pcomon_###VERSION###.tipo$oid_;
    var$cambiar_est_remesa  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$aremdoc_cod_externo gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_oid_cuenta    gepr_pcomon_###VERSION###.tipo$oids_;

    /*CURSOR DE BULTOS DE LA REMESA */
    CURSOR cur$bultos(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT B.OID_BULTO
        FROM SAPR_TBULTO B
       WHERE B.OID_REMESA = par$oid_remesa;

    /*CURSOR DE REMESAS */
    CURSOR cur$remesas IS
      SELECT DISTINCT T.OID OID_REMESA
        FROM SAPR_GTT_TOIDS T
       WHERE T.COD_CALIFICADOR = 'REMESA';

  BEGIN

    /* Validar Remesas */
    svalidar_elemento(par$aremdoc_cod_externo => var$aremdoc_cod_externo,
                      par$adocs_oid_cuenta    => var$adocs_oid_cuenta,
                      par$aremdoc_oid_remesa  => par$aremdoc_oid_remesa,
                      par$abuldoc_oid_bulto   => par$abuldoc_oid_bulto,
                      par$bol_validar_prec    => 0,
                      par$bol_sustituicion    => 0,
                      par$bol_gestion_bulto   => par$bol_gestion_bulto,
                      par$bol_baja            => par$bol_baja,
                      par$cod_cultura         => par$cod_cultura);

    IF NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$abuldoc_oid_bulto) THEN

      /* Si fue informado los ID de los bultos:
      la package va trabajar como Gestion de BULTO */
      FOR idx IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP

        /* Guardar identificador de la remesa */
        INSERT INTO SAPR_GTT_TOIDS
          (OID, COD_CALIFICADOR)
        VALUES
          (par$abuldoc_oid_remesa(idx), 'REMESA');
        par$inserts := par$inserts + sql%ROWCOUNT;

        sapr_pdocumento_###VERSION###.sdel_tdocumento_elemento(par$abuldoc_oid_documento(idx),
                                                          par$abuldoc_oid_remesa(idx),
                                                          par$abuldoc_oid_bulto(idx),
                                                          true,
                                                          var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;

        /* BULTO - Cambiar estado para T */
        sapr_pbulto_###VERSION###.supd_tbulto(par$oid_bulto        => par$abuldoc_oid_bulto(idx),
                                         par$oid_documento    => NULL,
                                         par$oid_cuenta_mov   => NULL,
                                         par$oid_cuenta_saldo => NULL,
                                         par$cod_usuario      => par$usuario,
                                         par$cod_estado       => gepr_pcomon_###VERSION###.const$codEstadoBultoTransito,
                                         par$updates          => var$updates_int);
        par$updates := par$updates + var$updates_int;

        /* BULTO  - crear referencia en la tabla documentoelemento */
        sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$abuldoc_oid_documento(idx),
                                                          par$oid_remesa           => par$abuldoc_oid_remesa(idx),
                                                          par$oid_bulto            => par$abuldoc_oid_bulto(idx),
                                                          par$cod_usuario          => par$usuario,
                                                          par$oid_contenedor       => NULL,
                                                          par$oid_contenedor_padre => NULL,
                                                          par$oid_cont_mayor_nivel => NULL,
                                                          par$inserts              => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;

      END LOOP idx;

      FOR rec$remesa IN cur$remesas LOOP

        SELECT COUNT(1)
          INTO var$cambiar_est_remesa
          FROM SAPR_TBULTO B
         WHERE B.oid_remesa = rec$remesa.OID_REMESA
           AND B.BOL_GESTION_BULTO = 0;

        /* REMESA - Cambiar estado para T */
        IF var$cambiar_est_remesa > 0 THEN
          sapr_premesa_###VERSION###.supd_tremesa(par$oid_remesa       => rec$remesa.OID_REMESA,
                                             par$oid_documento    => NULL,
                                             par$oid_cuenta_mov   => NULL,
                                             par$oid_cuenta_saldo => NULL,
                                             par$cod_usuario      => par$usuario,
                                             par$cod_estado       => gepr_pcomon_###VERSION###.const$codEstadoRemesaTransito,
                                             par$updates          => var$updates_int);
          par$updates := par$updates + var$updates_int;
        END IF;

        /* REMESA  - crear referencia en la tabla documentoelemento */

        SELECT DE.oid_documento
          INTO var$oid_documento
          FROM sapr_tdocumentoxelemento DE
         WHERE DE.oid_remesa = rec$remesa.OID_REMESA
           AND DE.COD_ESTADO_DOCXELEMENTO =
               gepr_pcomon_###VERSION###.const$CodEstadoDocEleTransito
           AND ROWNUM = 1;
        par$selects := par$selects + sql%rowcount;

        sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => var$oid_documento,
                                                          par$oid_remesa           => rec$remesa.OID_REMESA,
                                                          par$oid_bulto            => NULL,
                                                          par$cod_usuario          => par$usuario,
                                                          par$oid_contenedor       => NULL,
                                                          par$oid_contenedor_padre => NULL,
                                                          par$oid_cont_mayor_nivel => NULL,
                                                          par$inserts              => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;

      END LOOP rec$remesa;

    ELSIF par$aremdoc_oid_remesa.count > 0 THEN

      /* Si NO fue informado los ID de los bultos, pero fue informado los ID de las remesas:
      la package va trabajar como Gestion de REMESA */
      FOR idx IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP

        sapr_pdocumento_###VERSION###.sdel_tdocumento_elemento(par$aremdoc_oid_documento(idx),
                                                          par$aremdoc_oid_remesa(idx),
                                                          NULL,
                                                          true,
                                                          var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;

        /* REMESA - Cambiar estado para T */
        sapr_premesa_###VERSION###.supd_tremesa(par$oid_remesa       => par$aremdoc_oid_remesa(idx),
                                           par$oid_documento    => NULL,
                                           par$oid_cuenta_mov   => NULL,
                                           par$oid_cuenta_saldo => NULL,
                                           par$cod_usuario      => par$usuario,
                                           par$cod_estado       => gepr_pcomon_###VERSION###.const$codEstadoRemesaTransito,
                                           par$updates          => var$updates_int);
        par$updates := par$updates + var$updates_int;

        /* REMESA  - crear referencia en la tabla documentoelemento */
        sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$aremdoc_oid_documento(idx),
                                                          par$oid_remesa           => par$aremdoc_oid_remesa(idx),
                                                          par$oid_bulto            => NULL,
                                                          par$cod_usuario          => par$usuario,
                                                          par$oid_contenedor       => NULL,
                                                          par$oid_contenedor_padre => NULL,
                                                          par$oid_cont_mayor_nivel => NULL,
                                                          par$inserts              => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;

        FOR rec$bulto IN cur$bultos(par$aremdoc_oid_remesa(idx)) LOOP

          /* BULTO - Cambiar estado para T */
          sapr_pbulto_###VERSION###.supd_tbulto(par$oid_bulto        => rec$bulto.oid_bulto,
                                           par$oid_documento    => NULL,
                                           par$oid_cuenta_mov   => NULL,
                                           par$oid_cuenta_saldo => NULL,
                                           par$cod_usuario      => par$usuario,
                                           par$cod_estado       => gepr_pcomon_###VERSION###.const$codEstadoBultoTransito,
                                           par$updates          => var$updates_int);
          par$updates := par$updates + var$updates_int;

          /* BULTO  - crear referencia en la tabla documentoelemento */
          sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$aremdoc_oid_documento(idx),
                                                            par$oid_remesa           => par$aremdoc_oid_remesa(idx),
                                                            par$oid_bulto            => rec$bulto.oid_bulto,
                                                            par$cod_usuario          => par$usuario,
                                                            par$oid_contenedor       => NULL,
                                                            par$oid_contenedor_padre => NULL,
                                                            par$oid_cont_mayor_nivel => NULL,
                                                            par$inserts              => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;

        END LOOP rec$bulto;

      END LOOP;

    ELSE

      /* Si NO fue informado ningun elemento:
      retornar un error  */

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'sin_elementos',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
    END IF;

  END stratar_reenvio_baja;

  /* sguardar_valores_efectivo */
  PROCEDURE sguardar_valores_efectivo(par$aelemvalefe_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aelemvalefe_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_oid_denom     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_bol_esbillete IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                      par$aelemvalefe_oid_unid_med  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aelemvalefe_cod_tipo_efec IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aelemvalefe_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                                      par$aelemvalefe_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                      par$aelemvalefe_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aelemvalefe_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                      par$aelemvalefe_oid_calidad   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    par$inserts := 0;

    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aelemvalefe_oid_remesa) THEN

      FOR idx IN par$aelemvalefe_oid_remesa.first .. par$aelemvalefe_oid_remesa.last LOOP

        IF par$aelemvalefe_tipo(idx) = 'Declarado' THEN

          sins_tdeclarado_efectivo(par$oid_remesa        => par$aelemvalefe_oid_remesa(idx),
                                   par$oid_bulto         => par$aelemvalefe_oid_bulto(idx),
                                   par$oid_parcial       => par$aelemvalefe_oid_parcial(idx),
                                   par$oid_divisa        => par$aelemvalefe_oid_divisa(idx),
                                   par$oid_denominacion  => par$aelemvalefe_oid_denom(idx),
                                   par$oid_unidad_medida => par$aelemvalefe_oid_unid_med(idx),
                                   par$cod_tipo_efec_tot => par$aelemvalefe_cod_tipo_efec(idx),
                                   par$cod_nivel_detalle => par$aelemvalefe_cod_nvdetalle(idx),
                                   par$num_importe       => par$aelemvalefe_num_importe(idx),
                                   par$nel_cantidad      => par$aelemvalefe_nel_cantidad(idx),
                                   par$bol_ingresado     => par$aelemvalefe_bol_ingresado(idx),
                                   par$bol_billete       => par$aelemvalefe_bol_esbillete(idx),
                                   par$usuario           => par$usuario,
                                   par$inserts           => var$inserts_int);

        ELSIF par$aelemvalefe_tipo(idx) = 'Contado' THEN

          sins_tcontado_efectivo(par$oid_remesa        => par$aelemvalefe_oid_remesa(idx),
                                 par$oid_bulto         => par$aelemvalefe_oid_bulto(idx),
                                 par$oid_parcial       => par$aelemvalefe_oid_parcial(idx),
                                 par$oid_denominacion  => par$aelemvalefe_oid_denom(idx),
                                 par$oid_unidad_medida => par$aelemvalefe_oid_unid_med(idx),
                                 par$cod_tipocontado   => par$aelemvalefe_cod_tipo_efec(idx),
                                 par$num_importe       => par$aelemvalefe_num_importe(idx),
                                 par$nel_cantidad      => par$aelemvalefe_nel_cantidad(idx),
                                 par$oid_calidad       => par$aelemvalefe_oid_calidad(idx),
                                 par$bol_billete       => par$aelemvalefe_bol_esbillete(idx),
                                 par$usuario           => par$usuario,
                                 par$inserts           => var$inserts_int);

        ELSIF par$aelemvalefe_tipo(idx) = 'Diferencia' THEN

          sins_tdiferencia_efectivo(par$oid_remesa        => par$aelemvalefe_oid_remesa(idx),
                                    par$oid_bulto         => par$aelemvalefe_oid_bulto(idx),
                                    par$oid_parcial       => par$aelemvalefe_oid_parcial(idx),
                                    par$oid_divisa        => par$aelemvalefe_oid_divisa(idx),
                                    par$oid_denominacion  => par$aelemvalefe_oid_denom(idx),
                                    par$oid_unidad_medida => par$aelemvalefe_oid_unid_med(idx),
                                    par$cod_tipo_efec_tot => par$aelemvalefe_cod_tipo_efec(idx),
                                    par$cod_nivel_detalle => par$aelemvalefe_cod_nvdetalle(idx),
                                    par$num_importe       => par$aelemvalefe_num_importe(idx),
                                    par$nel_cantidad      => par$aelemvalefe_nel_cantidad(idx),
                                    par$bol_billete       => par$aelemvalefe_bol_esbillete(idx),
                                    par$usuario           => par$usuario,
                                    par$inserts           => var$inserts_int);

        END IF;

        par$inserts := par$inserts + var$inserts_int;

      END LOOP;

    END IF;

  END sguardar_valores_efectivo;

  /* sguardar_valores_mediopago */
  PROCEDURE sguardar_valores_mediopago(par$aelemval_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$aelemval_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_parcial   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_divisa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_oid_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemval_mp_cod_tipo_mp   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$aelemval_mp_num_importe   IN gepr_pcomon_###VERSION###.tipo$imps_,
                                       par$aelemval_mp_nel_cantidad  IN gepr_pcomon_###VERSION###.tipo$nels_,
                                       par$aelemval_mp_cod_nvdetalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$aelemval_mp_bol_ingresado IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                       par$aelemval_mp_nel_secuencia IN gepr_pcomon_###VERSION###.tipo$nels_,
                                       par$aelemter_mp_tipo          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$aelemter_mp_oid_valor     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemter_mp_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$aelemter_mp_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                       par$aelemter_mp_nel_ind_grupo IN gepr_pcomon_###VERSION###.tipo$nels_,
                                       par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                       par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN
    par$inserts := 0;
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aelemval_mp_oid_valor) THEN

      FOR idx IN par$aelemval_mp_oid_valor.first .. par$aelemval_mp_oid_valor.last LOOP

        IF par$aelemval_mp_tipo(idx) = 'Declarado' THEN

          sins_tdeclarado_medio_pago(par$oid_declarado     => par$aelemval_mp_oid_valor(idx),
                                     par$oid_remesa        => par$aelemval_mp_oid_remesa(idx),
                                     par$oid_bulto         => par$aelemval_mp_oid_bulto(idx),
                                     par$oid_parcial       => par$aelemval_mp_oid_parcial(idx),
                                     par$oid_divisa        => par$aelemval_mp_oid_divisa(idx),
                                     par$oid_medio_pago    => par$aelemval_mp_oid_mediopago(idx),
                                     par$cod_tipo_mp       => par$aelemval_mp_cod_tipo_mp(idx),
                                     par$num_importe       => par$aelemval_mp_num_importe(idx),
                                     par$nel_cantidad      => par$aelemval_mp_nel_cantidad(idx),
                                     par$cod_nivel_detalle => par$aelemval_mp_cod_nvdetalle(idx),
                                     par$bol_ingresado     => par$aelemval_mp_bol_ingresado(idx),
                                     par$usuario           => par$usuario,
                                     par$inserts           => var$inserts_int);

        ELSIF par$aelemval_mp_tipo(idx) = 'Contado' THEN

          sins_tcontado_medio_pago(par$oid_contato       => par$aelemval_mp_oid_valor(idx),
                                   par$oid_remesa        => par$aelemval_mp_oid_remesa(idx),
                                   par$oid_bulto         => par$aelemval_mp_oid_bulto(idx),
                                   par$oid_parcial       => par$aelemval_mp_oid_parcial(idx),
                                   par$oid_medio_pago    => par$aelemval_mp_oid_mediopago(idx),
                                   par$cod_tipo_contado  => '0',
                                   par$num_importe       => par$aelemval_mp_num_importe(idx),
                                   par$nel_cantidad      => par$aelemval_mp_nel_cantidad(idx),
                                   par$cod_nivel_detalle => par$aelemval_mp_cod_nvdetalle(idx),
                                   par$nel_secuencia     => par$aelemval_mp_nel_secuencia(idx),
                                   par$usuario           => par$usuario,
                                   par$inserts           => var$inserts_int);

        ELSIF par$aelemval_mp_tipo(idx) = 'Diferencia' THEN

          sins_tdiferencia_medio_pago(par$oid_remesa        => par$aelemval_mp_oid_remesa(idx),
                                      par$oid_bulto         => par$aelemval_mp_oid_bulto(idx),
                                      par$oid_parcial       => par$aelemval_mp_oid_parcial(idx),
                                      par$oid_divisa        => par$aelemval_mp_oid_divisa(idx),
                                      par$oid_medio_pago    => par$aelemval_mp_oid_mediopago(idx),
                                      par$cod_tipo_mp       => par$aelemval_mp_cod_tipo_mp(idx),
                                      par$num_importe       => par$aelemval_mp_num_importe(idx),
                                      par$nel_cantidad      => par$aelemval_mp_nel_cantidad(idx),
                                      par$cod_nivel_detalle => par$aelemval_mp_cod_nvdetalle(idx),
                                      par$usuario           => par$usuario,
                                      par$inserts           => var$inserts_int);

        END IF;

        par$inserts := par$inserts + var$inserts_int;

      END LOOP;

    END IF;

    /* Insere valor terminos medio pago */
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aelemter_mp_oid_valor) THEN
      FOR idx IN par$aelemter_mp_oid_valor.first .. par$aelemter_mp_oid_valor.last LOOP

        IF par$aelemter_mp_tipo(idx) = 'Declarado' THEN

          gepr_pterminos_iac_###VERSION###.sins_iac_medio_pago(par$oid_contado_mp   => NULL,
                                                          par$oid_declarado_mp => par$aelemter_mp_oid_valor(idx),
                                                          par$oid_termino      => par$aelemter_mp_oid_termino(idx),
                                                          par$des_valor        => par$aelemter_mp_des_valor(idx),
                                                          par$nec_ind_grupo    => par$aelemter_mp_nel_ind_grupo(idx),
                                                          par$usuario          => par$usuario,
                                                          par$inserts          => var$inserts_int);

        ELSIF par$aelemter_mp_tipo(idx) = 'Contado' THEN

          gepr_pterminos_iac_###VERSION###.sins_iac_medio_pago(par$oid_contado_mp   => par$aelemter_mp_oid_valor(idx),
                                                          par$oid_declarado_mp => NULL,
                                                          par$oid_termino      => par$aelemter_mp_oid_termino(idx),
                                                          par$des_valor        => par$aelemter_mp_des_valor(idx),
                                                          par$nec_ind_grupo    => par$aelemter_mp_nel_ind_grupo(idx),
                                                          par$usuario          => par$usuario,
                                                          par$inserts          => var$inserts_int);

        END IF;
        par$inserts := par$inserts + var$inserts_int;

      END LOOP;
    END IF;

  END sguardar_valores_mediopago;

  /* Guardar Valores Terminos*/
  PROCEDURE sguardar_valores_terminos( /* arrays de terminos por remesa */par$avtrem_oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtrem_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtrem_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      /* arrays de terminos por bulto */
                                      par$avtbul_oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtbul_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtbul_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      /* arrays de terminos por parcial */
                                      par$avtpar_oid_parcial IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtpar_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtpar_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    par$inserts := 0;

    /* Remesas */
    IF par$avtrem_oid_remesa IS NOT NULL THEN
      IF par$avtrem_oid_remesa.COUNT > 0 THEN
        FOR idx IN par$avtrem_oid_remesa.first .. par$avtrem_oid_remesa.last LOOP
          GEPR_PTERMINOS_IAC_###VERSION###.sins_iac_remesa(par$oid_remesa  => par$avtrem_oid_remesa(idx),
                                                      par$oid_termino => par$avtrem_oid_termino(idx),
                                                      par$des_valor   => par$avtrem_des_valor(idx),
                                                      par$usuario     => par$cod_usuario,
                                                      par$inserts     => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;
        END LOOP;
      END IF;
    END IF;

    /* Bultos */
    IF par$avtbul_oid_bulto IS NOT NULL THEN
      IF par$avtbul_oid_bulto.COUNT > 0 THEN
        FOR idx IN par$avtbul_oid_bulto.first .. par$avtbul_oid_bulto.last LOOP
          GEPR_PTERMINOS_IAC_###VERSION###.sins_iac_bulto(par$oid_bulto   => par$avtbul_oid_bulto(idx),
                                                     par$oid_termino => par$avtbul_oid_termino(idx),
                                                     par$des_valor   => par$avtbul_des_valor(idx),
                                                     par$usuario     => par$cod_usuario,
                                                     par$inserts     => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;
        END LOOP;
      END IF;
    END IF;

    /* Parciales */
    IF par$avtpar_oid_parcial IS NOT NULL THEN
      IF par$avtpar_oid_parcial.COUNT > 0 THEN
        FOR idx IN par$avtpar_oid_parcial.first .. par$avtpar_oid_parcial.last LOOP
          GEPR_PTERMINOS_IAC_###VERSION###.sins_iac_parcial(par$oid_parcial => par$avtpar_oid_parcial(idx),
                                                       par$oid_termino => par$avtpar_oid_termino(idx),
                                                       par$des_valor   => par$avtpar_des_valor(idx),
                                                       par$usuario     => par$cod_usuario,
                                                       par$inserts     => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;
        END LOOP;
      END IF;
    END IF;

  END sguardar_valores_terminos;

  /* srecupera_iac_elementos */
  PROCEDURE srecupera_iac_elementos(par$adocs_oid        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    /* arrays de terminos por remesa */
                                    par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtrem_oid_remesa     OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtrem_oid_termino    OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtrem_cod_termino    OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$avtrem_des_valor      OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$avtrem_obligatorio    OUT gepr_pcomon_###VERSION###.tipo$nbols_,
                                    /* arrays de terminos por bulto */
                                    par$abuldoc_oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$abuldoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$abuldoc_oid_tipo_servicio IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtbul_oid_bulto          OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtbul_oid_termino        OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtbul_cod_termino        OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$avtbul_des_valor          OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$avtbul_obligatorio        OUT gepr_pcomon_###VERSION###.tipo$nbols_,
                                    /* arrays de terminos por parcial */
                                    par$apardoc_oid_bulto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtpar_oid_parcial OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtpar_oid_termino OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtpar_cod_termino OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$avtpar_des_valor   OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$avtpar_obligatorio OUT gepr_pcomon_###VERSION###.tipo$nbols_) IS

    var$cod_tipo_servicio     gepr_pcomon_###VERSION###.tipo$cod_;
    var$adocs_oid_iac_remesa  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_iac_bulto   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_iac_parcial gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtrem_oid_termino    gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtrem_des_valor      gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtbul_oid_termino    gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtbul_des_valor      gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtpar_oid_termino    gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtpar_des_valor      gepr_pcomon_###VERSION###.tipo$descs_;

    CURSOR cur$terminos(par$oid_iac IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT IAC.COD_IAC,
             T.OID_TERMINO,
             T.COD_TERMINO,
             TPIAC.BOL_ES_OBLIGATORIO
        FROM GEPR_TINFORM_ADICIONAL_CLIENTE IAC
       INNER JOIN GEPR_TTERMINO_POR_IAC TPIAC
          ON TPIAC.OID_IAC = IAC.OID_IAC
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TPIAC.OID_TERMINO
       WHERE IAC.COD_IAC = par$oid_iac;
  BEGIN

    var$avtrem_oid_termino := par$avtrem_oid_termino;
    var$avtrem_des_valor   := par$avtrem_des_valor;
    var$avtbul_oid_termino := par$avtbul_oid_termino;
    var$avtbul_des_valor   := par$avtbul_des_valor;
    var$avtpar_oid_termino := par$avtpar_oid_termino;
    var$avtpar_des_valor   := par$avtpar_des_valor;

    IF NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$adocs_oid) AND
       NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$adocs_oid_cuenta) THEN

      /* recuperar procesos */
      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
        GEPR_PPROCESO_###VERSION###.srecupera_iac_proceso(par$oid_cuenta      => par$adocs_oid_cuenta(idx),
                                                     par$oid_iac_remesa  => var$adocs_oid_iac_remesa(idx),
                                                     par$oid_iac_bulto   => var$adocs_oid_iac_bulto(idx),
                                                     par$oid_iac_parcial => var$adocs_oid_iac_parcial(idx));
      END LOOP;
    END IF;

    IF var$adocs_oid_iac_remesa IS NOT NULL OR
       var$adocs_oid_iac_bulto IS NOT NULL OR
       var$adocs_oid_iac_parcial IS NOT NULL THEN

      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP

        FOR idxE IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP
          IF par$abuldoc_oid_documento(idxE) = par$adocs_oid(idx) THEN
            SELECT LV.COD_VALOR
              INTO var$cod_tipo_servicio
              FROM GEPR_TLISTA_VALOR LV
             WHERE LV.OID_LISTA_VALOR = par$abuldoc_oid_tipo_servicio(idxE);
          END IF;
        END LOOP;

        IF var$cod_tipo_servicio = gepr_pcomon_###VERSION###.const$CodTipoServProcesar OR
           var$cod_tipo_servicio =
           gepr_pcomon_###VERSION###.const$CodTipoServClasificacion THEN

          /* terminos remesas */
          IF var$adocs_oid_iac_remesa IS NOT NULL THEN
            FOR idxE IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP
              IF par$aremdoc_oid_documento(idxE) = par$adocs_oid(idx) THEN

                FOR rec$termino IN cur$terminos(var$adocs_oid_iac_remesa(idx)) LOOP
                  par$avtrem_oid_remesa(idxE) := par$aremdoc_oid_remesa(idxE);
                  par$avtrem_oid_termino(idxE) := rec$termino.oid_termino;
                  par$avtrem_cod_termino(idxE) := rec$termino.cod_termino;
                  par$avtrem_obligatorio(idxE) := rec$termino.bol_es_obligatorio;

                  FOR idxT IN var$avtrem_oid_termino.first .. var$avtrem_oid_termino.last LOOP
                    IF var$avtrem_oid_termino(idxT) =
                       rec$termino.oid_termino THEN
                      par$avtrem_des_valor(idxE) := var$avtrem_des_valor(idxT);
                      EXIT;
                    END IF;
                  END LOOP;

                END LOOP rec$termino;

              END IF;
            END LOOP;
          END IF;

          /* terminos bultos */
          IF var$adocs_oid_iac_bulto IS NOT NULL THEN
            FOR idxE IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP
              IF par$abuldoc_oid_documento(idxE) = par$adocs_oid(idx) THEN

                FOR rec$termino IN cur$terminos(var$adocs_oid_iac_bulto(idx)) LOOP
                  par$avtbul_oid_bulto(idxE) := par$abuldoc_oid_bulto(idxE);
                  par$avtbul_oid_termino(idxE) := rec$termino.oid_termino;
                  par$avtbul_cod_termino(idxE) := rec$termino.cod_termino;
                  par$avtbul_obligatorio(idxE) := rec$termino.bol_es_obligatorio;

                  FOR idxT IN var$avtbul_oid_termino.first .. var$avtbul_oid_termino.last LOOP
                    IF var$avtbul_oid_termino(idxT) =
                       rec$termino.oid_termino THEN
                      par$avtbul_des_valor(idxE) := var$avtbul_des_valor(idxT);
                      EXIT;
                    END IF;
                  END LOOP;

                END LOOP rec$termino;

              END IF;
            END LOOP;
          END IF;

          /* terminos parciales */
          IF var$adocs_oid_iac_parcial IS NOT NULL THEN
            FOR idxE IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP
              IF par$abuldoc_oid_documento(idxE) = par$adocs_oid(idx) THEN
                FOR idxP IN par$apardoc_oid_bulto.first .. par$apardoc_oid_bulto.last LOOP
                  IF par$abuldoc_oid_bulto(idxE) =
                     par$apardoc_oid_bulto(idxP) THEN
                    FOR rec$termino IN cur$terminos(var$adocs_oid_iac_parcial(idx)) LOOP
                      par$avtpar_oid_parcial(idxP) := par$aremdoc_oid_remesa(idxE);
                      par$avtpar_oid_termino(idxP) := rec$termino.oid_termino;
                      par$avtpar_cod_termino(idxP) := rec$termino.cod_termino;
                      par$avtpar_obligatorio(idxP) := rec$termino.bol_es_obligatorio;

                      FOR idxT IN var$avtpar_oid_termino.first .. var$avtpar_oid_termino.last LOOP
                        IF var$avtpar_oid_termino(idxT) =
                           rec$termino.oid_termino THEN
                          par$avtpar_des_valor(idxP) := var$avtpar_des_valor(idxT);
                          EXIT;
                        END IF;
                      END LOOP;
                    END LOOP rec$termino;
                  END IF;
                END LOOP;
              END IF;
            END LOOP;
          END IF;
        END IF;
      END LOOP;
    END IF;

  END srecupera_iac_elementos;

  /* sdel_elementos */
  PROCEDURE sdel_elementos(par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                           par$bol_documento_altas   IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$deletes               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    /* Remesa */
    FOR idx IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP

      gepr_pterminos_iac_###VERSION###.sdel_tvalor_iac(par$aremdoc_oid_remesa(idx),
                                                  NULL,
                                                  NULL,
                                                  true,
                                                  var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
      gepr_pterminos_iac_###VERSION###.sdel_tvalor_term_mp(par$aremdoc_oid_remesa(idx),
                                                      NULL,
                                                      NULL,
                                                      true,
                                                      var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
      sdel_tdeclarado_efectivo(par$aremdoc_oid_remesa(idx),
                               NULL,
                               NULL,
                               true,
                               var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
      sdel_tdeclarado_medio_pago(par$aremdoc_oid_remesa(idx),
                                 NULL,
                                 NULL,
                                 true,
                                 var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
      sdel_tcontado_efectivo(par$aremdoc_oid_remesa(idx),
                             NULL,
                             NULL,
                             true,
                             var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
      sdel_tcontado_medio_pago(par$aremdoc_oid_remesa(idx),
                               NULL,
                               NULL,
                               true,
                               var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
      sdel_tdiferencia_efectivo(par$aremdoc_oid_remesa(idx),
                                NULL,
                                NULL,
                                true,
                                var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
      sdel_tdiferencia_medio_pago(par$aremdoc_oid_remesa(idx),
                                  NULL,
                                  NULL,
                                  true,
                                  var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
      sapr_pdocumento_###VERSION###.sdel_tdocumento_elemento(par$aremdoc_oid_documento(idx),
                                                        par$aremdoc_oid_remesa(idx),
                                                        NULL,
                                                        true,
                                                        var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;

      IF par$bol_documento_altas = 1 THEN
        sapr_plista_valor_###VERSION###.sdel_tlista_valor_elem(par$aremdoc_oid_remesa(idx),
                                                          NULL,
                                                          NULL,
                                                          NULL,
                                                          true,
                                                          var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
        sapr_pparcial_###VERSION###.sdel_tparcial(par$aremdoc_oid_remesa(idx),
                                             NULL,
                                             NULL,
                                             var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
        sapr_pbulto_###VERSION###.sdel_tbulto(par$aremdoc_oid_remesa(idx),
                                         NULL,
                                         var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
        sapr_premesa_###VERSION###.sdel_tremesa(par$aremdoc_oid_remesa(idx),
                                           var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
      END IF;

    END LOOP;

  END sdel_elementos;

  /* sdel_tdeclarado_efectivo */
  PROCEDURE sdel_tdeclarado_efectivo(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$eliminar_hijos IN BOOLEAN,
                                     par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    IF par$eliminar_hijos THEN

      IF par$oid_remesa IS NOT NULL THEN

        IF par$oid_parcial IS NOT NULL THEN
          DELETE SAPR_TDECLARADO_EFECTIVO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto
             AND OID_PARCIAL = par$oid_parcial;
          par$deletes := sql%rowcount;
        ELSIF par$oid_bulto IS NOT NULL THEN
          DELETE SAPR_TDECLARADO_EFECTIVO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto;
          par$deletes := sql%rowcount;
        ELSE
          DELETE SAPR_TDECLARADO_EFECTIVO
           WHERE OID_REMESA = par$oid_remesa;
          par$deletes := sql%rowcount;
        END IF;

      END IF;

    ELSE

      DELETE SAPR_TDECLARADO_EFECTIVO
       WHERE ((par$oid_bulto IS NOT NULL AND OID_BULTO = par$oid_bulto) OR
             (par$oid_remesa IS NOT NULL AND par$oid_bulto IS NULL AND
             OID_BULTO IS NULL) OR
             (par$oid_bulto IS NULL AND par$oid_parcial IS NOT NULL))
         AND ((par$oid_parcial IS NOT NULL AND
             OID_PARCIAL = par$oid_parcial) OR
             (par$oid_parcial IS NULL AND OID_PARCIAL IS NULL))
         AND (par$oid_remesa IS NOT NULL AND OID_REMESA = par$oid_remesa OR
             par$oid_remesa IS NULL);
      par$deletes := sql%rowcount;
    END IF;

  END sdel_tdeclarado_efectivo;

  /* sdel_tdeclarado_medio_pago */
  PROCEDURE sdel_tdeclarado_medio_pago(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$eliminar_hijos IN BOOLEAN,
                                       par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    IF par$eliminar_hijos THEN

      IF par$oid_remesa IS NOT NULL THEN

        IF par$oid_parcial IS NOT NULL THEN
          DELETE SAPR_TDECLARADO_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto
             AND OID_PARCIAL = par$oid_parcial;
          par$deletes := sql%rowcount;
        ELSIF par$oid_bulto IS NOT NULL THEN
          DELETE SAPR_TDECLARADO_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto;
          par$deletes := sql%rowcount;
        ELSE
          DELETE SAPR_TDECLARADO_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa;
          par$deletes := sql%rowcount;
        END IF;

      END IF;

    ELSE

      DELETE SAPR_TDECLARADO_MEDIO_PAGO
       WHERE ((par$oid_bulto IS NOT NULL AND OID_BULTO = par$oid_bulto) OR
             (par$oid_remesa IS NOT NULL AND par$oid_bulto IS NULL AND
             OID_BULTO IS NULL) OR
             (par$oid_bulto IS NULL AND par$oid_parcial IS NOT NULL))
         AND ((par$oid_parcial IS NOT NULL AND
             OID_PARCIAL = par$oid_parcial) OR
             (par$oid_parcial IS NULL AND OID_PARCIAL IS NULL))
         AND (par$oid_remesa IS NOT NULL AND OID_REMESA = par$oid_remesa OR
             par$oid_remesa IS NULL);
      par$deletes := sql%rowcount;

    END IF;

  END sdel_tdeclarado_medio_pago;

  /* sdel_tcontado_efectivo */
  PROCEDURE sdel_tcontado_efectivo(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$eliminar_hijos IN BOOLEAN,
                                   par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    IF par$eliminar_hijos THEN

      IF par$oid_remesa IS NOT NULL THEN

        IF par$oid_parcial IS NOT NULL THEN
          DELETE SAPR_TCONTADO_EFECTIVO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto
             AND OID_PARCIAL = par$oid_parcial;
          par$deletes := sql%rowcount;
        ELSIF par$oid_bulto IS NOT NULL THEN
          DELETE SAPR_TCONTADO_EFECTIVO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto;
          par$deletes := sql%rowcount;
        ELSE
          DELETE SAPR_TCONTADO_EFECTIVO WHERE OID_REMESA = par$oid_remesa;
          par$deletes := sql%rowcount;
        END IF;

      END IF;

    ELSE

      DELETE SAPR_TCONTADO_EFECTIVO
       WHERE ((par$oid_bulto IS NOT NULL AND OID_BULTO = par$oid_bulto) OR
             (par$oid_remesa IS NOT NULL AND par$oid_bulto IS NULL AND
             OID_BULTO IS NULL) OR
             (par$oid_bulto IS NULL AND par$oid_parcial IS NOT NULL))
         AND ((par$oid_parcial IS NOT NULL AND
             OID_PARCIAL = par$oid_parcial) OR
             (par$oid_parcial IS NULL AND OID_PARCIAL IS NULL))
         AND (par$oid_remesa IS NOT NULL AND OID_REMESA = par$oid_remesa OR
             par$oid_remesa IS NULL);
      par$deletes := sql%rowcount;

    END IF;

  END sdel_tcontado_efectivo;

  /* sdel_tcontado_medio_pago */
  PROCEDURE sdel_tcontado_medio_pago(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$eliminar_hijos IN BOOLEAN,
                                     par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    IF par$eliminar_hijos THEN

      IF par$oid_remesa IS NOT NULL THEN

        IF par$oid_parcial IS NOT NULL THEN
          DELETE SAPR_TCONTADO_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto
             AND OID_PARCIAL = par$oid_parcial;
          par$deletes := sql%rowcount;
        ELSIF par$oid_bulto IS NOT NULL THEN
          DELETE SAPR_TCONTADO_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto;
          par$deletes := sql%rowcount;
        ELSE
          DELETE SAPR_TCONTADO_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa;
          par$deletes := sql%rowcount;
        END IF;

      END IF;

    ELSE

      DELETE SAPR_TCONTADO_MEDIO_PAGO
       WHERE ((par$oid_bulto IS NOT NULL AND OID_BULTO = par$oid_bulto) OR
             (par$oid_remesa IS NOT NULL AND par$oid_bulto IS NULL AND
             OID_BULTO IS NULL) OR
             (par$oid_bulto IS NULL AND par$oid_parcial IS NOT NULL))
         AND ((par$oid_parcial IS NOT NULL AND
             OID_PARCIAL = par$oid_parcial) OR
             (par$oid_parcial IS NULL AND OID_PARCIAL IS NULL))
         AND (par$oid_remesa IS NOT NULL AND OID_REMESA = par$oid_remesa OR
             par$oid_remesa IS NULL);
      par$deletes := sql%rowcount;

    END IF;

  END sdel_tcontado_medio_pago;

  /* sdel_tdiferencia_efectivo */
  PROCEDURE sdel_tdiferencia_efectivo(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$eliminar_hijos IN BOOLEAN,
                                      par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    IF par$eliminar_hijos THEN

      IF par$oid_remesa IS NOT NULL THEN

        IF par$oid_parcial IS NOT NULL THEN
          DELETE SAPR_TDIFERENCIA_EFECTIVO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto
             AND OID_PARCIAL = par$oid_parcial;
          par$deletes := sql%rowcount;
        ELSIF par$oid_bulto IS NOT NULL THEN
          DELETE SAPR_TDIFERENCIA_EFECTIVO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto;
          par$deletes := sql%rowcount;
        ELSE
          DELETE SAPR_TDIFERENCIA_EFECTIVO
           WHERE OID_REMESA = par$oid_remesa;
          par$deletes := sql%rowcount;
        END IF;

      END IF;

    ELSE

      DELETE SAPR_TDIFERENCIA_EFECTIVO
       WHERE ((par$oid_bulto IS NOT NULL AND OID_BULTO = par$oid_bulto) OR
             (par$oid_remesa IS NOT NULL AND par$oid_bulto IS NULL AND
             OID_BULTO IS NULL) OR
             (par$oid_bulto IS NULL AND par$oid_parcial IS NOT NULL))
         AND ((par$oid_parcial IS NOT NULL AND
             OID_PARCIAL = par$oid_parcial) OR
             (par$oid_parcial IS NULL AND OID_PARCIAL IS NULL))
         AND (par$oid_remesa IS NOT NULL AND OID_REMESA = par$oid_remesa OR
             par$oid_remesa IS NULL);
      par$deletes := sql%rowcount;
    END IF;

  END sdel_tdiferencia_efectivo;

  /* sdel_tdiferencia_medio_pago */
  PROCEDURE sdel_tdiferencia_medio_pago(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$eliminar_hijos IN BOOLEAN,
                                        par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    IF par$eliminar_hijos THEN

      IF par$oid_remesa IS NOT NULL THEN

        IF par$oid_parcial IS NOT NULL THEN
          DELETE SAPR_TDIFERENCIA_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto
             AND OID_PARCIAL = par$oid_parcial;
          par$deletes := sql%rowcount;
        ELSIF par$oid_bulto IS NOT NULL THEN
          DELETE SAPR_TDIFERENCIA_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa
             AND OID_BULTO = par$oid_bulto;
          par$deletes := sql%rowcount;
        ELSE
          DELETE SAPR_TDIFERENCIA_MEDIO_PAGO
           WHERE OID_REMESA = par$oid_remesa;
          par$deletes := sql%rowcount;
        END IF;

      END IF;

    ELSE

      DELETE SAPR_TDIFERENCIA_MEDIO_PAGO
       WHERE ((par$oid_bulto IS NOT NULL AND OID_BULTO = par$oid_bulto) OR
             (par$oid_remesa IS NOT NULL AND par$oid_bulto IS NULL AND
             OID_BULTO IS NULL) OR
             (par$oid_bulto IS NULL AND par$oid_parcial IS NOT NULL))
         AND ((par$oid_parcial IS NOT NULL AND
             OID_PARCIAL = par$oid_parcial) OR
             (par$oid_parcial IS NULL AND OID_PARCIAL IS NULL))
         AND (par$oid_remesa IS NOT NULL AND OID_REMESA = par$oid_remesa OR
             par$oid_remesa IS NULL);
      par$deletes := sql%rowcount;
    END IF;

  END sdel_tdiferencia_medio_pago;

  /* sins_tdeclarado_efectivo */
  PROCEDURE sins_tdeclarado_efectivo(par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                     par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                     par$bol_ingresado     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                     par$bol_billete       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                     par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero          VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_unidad_medida gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    IF par$oid_unidad_medida IS NULL THEN

      BEGIN
        IF par$bol_billete = 1 THEN
          SELECT OID_UNIDAD_MEDIDA
            INTO var$oid_unidad_medida
            FROM GEPR_TUNIDAD_MEDIDA
           WHERE BOL_DEFECTO = 1
             AND COD_TIPO_UNIDAD_MEDIDA = 0;
        ELSE
          SELECT OID_UNIDAD_MEDIDA
            INTO var$oid_unidad_medida
            FROM GEPR_TUNIDAD_MEDIDA
           WHERE BOL_DEFECTO = 1
             AND COD_TIPO_UNIDAD_MEDIDA = 1;
        END IF;
      EXCEPTION
        WHEN no_data_found THEN
          var$oid_unidad_medida := NULL;
      END;

    ELSE
      var$oid_unidad_medida := par$oid_unidad_medida;
    END IF;

    INSERT INTO sapr_tdeclarado_efectivo
      (oid_declarado_efectivo,
       oid_remesa,
       oid_bulto,
       oid_parcial,
       oid_divisa,
       oid_denominacion,
       oid_unidad_medida,
       cod_tipo_efectivo_total,
       num_importe,
       nel_cantidad,
       cod_nivel_detalle,
       bol_ingresado,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_remesa,
       par$oid_bulto,
       par$oid_parcial,
       par$oid_divisa,
       par$oid_denominacion,
       var$oid_unidad_medida,
       par$cod_tipo_efec_tot,
       par$num_importe,
       par$nel_cantidad,
       par$cod_nivel_detalle,
       par$bol_ingresado,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tdeclarado_efectivo;

  /* sins_tdeclarado_medio_pago */
  PROCEDURE sins_tdeclarado_medio_pago(par$oid_declarado     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                       par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                       par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$bol_ingresado     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                       par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                       par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_tdeclarado_medio_pago
      (oid_declarado_medio_pago,
       oid_remesa,
       oid_bulto,
       oid_parcial,
       oid_divisa,
       oid_medio_pago,
       cod_tipo_medio_pago,
       num_importe,
       nel_cantidad,
       cod_nivel_detalle,
       bol_ingresado,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (par$oid_declarado,
       par$oid_remesa,
       par$oid_bulto,
       par$oid_parcial,
       par$oid_divisa,
       par$oid_medio_pago,
       par$cod_tipo_mp,
       par$num_importe,
       par$nel_cantidad,
       par$cod_nivel_detalle,
       par$bol_ingresado,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tdeclarado_medio_pago;

  /* sins_tcontado_efectivo */
  PROCEDURE sins_tcontado_efectivo(par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_tipocontado   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                   par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                   par$oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$bol_billete       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero          VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$cod_tipocontado   gepr_pcomon_###VERSION###.tipo$cod_ := '0';
    var$oid_unidad_medida gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    IF par$oid_unidad_medida IS NULL THEN

      BEGIN
        IF par$bol_billete = 1 THEN
          SELECT OID_UNIDAD_MEDIDA
            INTO var$oid_unidad_medida
            FROM GEPR_TUNIDAD_MEDIDA
           WHERE BOL_DEFECTO = 1
             AND COD_TIPO_UNIDAD_MEDIDA = 0;
        ELSE
          SELECT OID_UNIDAD_MEDIDA
            INTO var$oid_unidad_medida
            FROM GEPR_TUNIDAD_MEDIDA
           WHERE BOL_DEFECTO = 1
             AND COD_TIPO_UNIDAD_MEDIDA = 1;
        END IF;

      EXCEPTION
        WHEN no_data_found THEN
          var$oid_unidad_medida := NULL;
      END;
    ELSE
      var$oid_unidad_medida := par$oid_unidad_medida;
    END IF;

    IF par$cod_tipocontado IS NOT NULL THEN
      var$cod_tipocontado := par$cod_tipocontado;
    END IF;

    INSERT INTO sapr_tcontado_efectivo
      (oid_contado_efectivo,
       oid_remesa,
       oid_bulto,
       oid_parcial,
       oid_denominacion,
       oid_unidad_medida,
       cod_tipo_contado,
       num_importe,
       nel_cantidad,
       oid_calidad,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_remesa,
       par$oid_bulto,
       par$oid_parcial,
       par$oid_denominacion,
       var$oid_unidad_medida,
       var$cod_tipocontado,
       par$num_importe,
       par$nel_cantidad,
       par$oid_calidad,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tcontado_efectivo;

  /* sins_tcontado_medio_pago */
  PROCEDURE sins_tcontado_medio_pago(par$oid_contato       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_tipo_contado  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                     par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                     par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$nel_secuencia     IN gepr_pcomon_###VERSION###.tipo$nel_,
                                     par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_tcontado_medio_pago
      (oid_contado_medio_pago,
       oid_remesa,
       oid_bulto,
       oid_parcial,
       oid_medio_pago,
       cod_tipo_contado,
       num_importe,
       nel_cantidad,
       cod_nivel_detalle,
       nec_secuencia,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (par$oid_contato,
       par$oid_remesa,
       par$oid_bulto,
       par$oid_parcial,
       par$oid_medio_pago,
       par$cod_tipo_contado,
       par$num_importe,
       par$nel_cantidad,
       par$cod_nivel_detalle,
       par$nel_secuencia,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tcontado_medio_pago;

  /* sins_tdiferencia_efectivo */
  PROCEDURE sins_tdiferencia_efectivo(par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                      par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                      par$bol_billete       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                      par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero          VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_unidad_medida gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    IF par$oid_unidad_medida IS NULL THEN

      BEGIN
        IF par$bol_billete = 1 THEN
          SELECT OID_UNIDAD_MEDIDA
            INTO var$oid_unidad_medida
            FROM GEPR_TUNIDAD_MEDIDA
           WHERE BOL_DEFECTO = 1
             AND COD_TIPO_UNIDAD_MEDIDA = 0;
        ELSE
          SELECT OID_UNIDAD_MEDIDA
            INTO var$oid_unidad_medida
            FROM GEPR_TUNIDAD_MEDIDA
           WHERE BOL_DEFECTO = 1
             AND COD_TIPO_UNIDAD_MEDIDA = 1;
        END IF;

      EXCEPTION
        WHEN no_data_found THEN
          var$oid_unidad_medida := NULL;
      END;
    ELSE
      var$oid_unidad_medida := par$oid_unidad_medida;
    END IF;

    INSERT INTO sapr_tdiferencia_efectivo
      (oid_diferencia_efectivo,
       oid_remesa,
       oid_bulto,
       oid_parcial,
       oid_divisa,
       oid_denominacion,
       oid_unidad_medida,
       cod_tipo_efectivo_total,
       num_importe,
       nel_cantidad,
       cod_nivel_detalle,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_remesa,
       par$oid_bulto,
       par$oid_parcial,
       par$oid_divisa,
       par$oid_denominacion,
       var$oid_unidad_medida,
       par$cod_tipo_efec_tot,
       par$num_importe,
       par$nel_cantidad,
       par$cod_nivel_detalle,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tdiferencia_efectivo;

  /* sins_tdiferencia_medio_pago */
  PROCEDURE sins_tdiferencia_medio_pago(par$oid_remesa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_bulto         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_parcial       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                        par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                        par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TDIFERENCIA_MEDIO_PAGO
      (OID_DIFERENCIA_MEDIO_PAGO,
       OID_REMESA,
       OID_BULTO,
       OID_PARCIAL,
       OID_DIVISA,
       OID_MEDIO_PAGO,
       COD_TIPO_MEDIO_PAGO,
       NUM_IMPORTE,
       NEL_CANTIDAD,
       COD_NIVEL_DETALLE,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION)
    VALUES
      (SYS_GUID(),
       par$oid_remesa,
       par$oid_bulto,
       par$oid_parcial,
       par$oid_divisa,
       par$oid_medio_pago,
       par$cod_tipo_mp,
       par$num_importe,
       par$nel_cantidad,
       par$cod_nivel_detalle,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tdiferencia_medio_pago;

  /* scargar_tt_oids_iac_elem */
  PROCEDURE scargar_tt_oids_iac_elem(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
      /* IAC de las remesas */
       (SELECT OID_IAC, NULL AS OID_IAC_PARCIALES
          FROM SAPR_TREMESA
         WHERE OID_REMESA IN
               (SELECT DISTINCT OID OID_REMESA
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'REMESA')
        UNION
        /* cuentas de los bultos */
        SELECT OID_IAC, OID_IAC_PARCIALES
          FROM SAPR_TBULTO
         WHERE OID_BULTO IN (SELECT DISTINCT OID OID_BULTO
                               FROM SAPR_GTT_TOIDS
                              WHERE COD_CALIFICADOR = 'BULTO')
        UNION
        /* cuentas de los parcales */
        SELECT OID_IAC, NULL AS OID_IAC_PARCIALES
          FROM SAPR_TPARCIAL
         WHERE OID_PARCIAL IN
               (SELECT DISTINCT OID OID_PARCIAL
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'PARCIAL'))
      SELECT DISTINCT OID_IAC, 'IAC'
        FROM (SELECT OID_IAC
                FROM q
              UNION ALL
              SELECT OID_IAC_PARCIALES OID_IAC FROM q)
       WHERE OID_IAC IS NOT NULL;

    par$inserts := sql%ROWCOUNT;

  END scargar_tt_oids_iac_elem;

  /* fcargar_rc_lista_valor */
  FUNCTION fcargar_rc_lista_valor(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_lista_valor SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_lista_valor FOR
      SELECT LT.COD_TIPO,
             VE.OID_REMESA,
             VE.OID_BULTO,
             VE.OID_PARCIAL,
             LV.OID_LISTA_VALOR,
             LV.COD_VALOR,
             LV.DES_VALOR,
             LV.BOL_DEFECTO,
             B.OID_EXTERNO OID_BULTO_EXTERNO,
             M.OID_MODULO
        FROM GEPR_TLISTA_VALOR LV
       INNER JOIN SAPR_TLISTA_VALORXELEMENTO VE
          ON VE.OID_LISTA_VALOR = LV.OID_LISTA_VALOR
       INNER JOIN GEPR_TLISTA_TIPO LT
          ON LT.OID_LISTA_TIPO = VE.OID_LISTA_TIPO
        LEFT JOIN SAPR_TBULTO B
          ON B.OID_BULTO = VE.OID_BULTO
        LEFT JOIN GEPR_TMODULO M
          ON LV.OID_LISTA_VALOR = M.OID_LISTA_VALOR
       WHERE LT.COD_TIPO IN ('01', '05')
         AND VE.OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA');
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_lista_valor;

  END fcargar_rc_lista_valor;

  /* fcargar_rc_elementos */
  FUNCTION fcargar_rc_elementos(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_elementos SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_elementos FOR
      SELECT
      -- Contenedor
       C.OID_CONTENEDOR AS C_OID_CONTENEDOR,
       C.OID_TIPO_CONTENEDOR AS C_OID_TIPO_CONTENEDOR,
       C.OID_CONTENEDOR_PADRE AS C_OID_CONTENEDOR_PADRE,
       C.OID_SECTOR AS C_SECTOR,
       C.OID_CUENTA_MOVIMIENTO AS C_OID_CUENTA,
       C.OID_CUENTA_SALDO AS C_OID_CUENTA_SALDO,
       C.OID_GRUPO_DOCUMENTO AS C_OID_GRUPO_DOCUMENTO,
       C.COD_ESTADO AS C_COD_ESTADO,
       C.BOL_PACK_MODULAR AS C_BOL_PACK_MODULAR,
       C.FEC_VENCIMIENTO AS C_FEC_VENCIMIENTO,
       C.BOL_AGRUPA_CONTENEDOR AS C_BOL_AGRUPA_CONTENEDOR,
       C.COD_TIPO_ELEMENTO AS C_COD_TIPO_ELEMENTO,
       C.OID_CONTENEDOR_MAYOR_NIVEL AS C_OID_CONTENEDOR_MAYOR_NIVEL,
       C.COD_CONTENEDOR AS C_COD_CONTENEDOR,
       C.GMT_CREACION AS C_GMT_CREACION,
       C.GMT_MODIFICACION AS C_GMT_MODIFICACION,
       C.DES_USUARIO_CREACION AS C_DES_USUARIO_CREACION,
       C.DES_USUARIO_MODIFICACION AS C_DES_USUARIO_MODIFICACION,
       CASE
         WHEN C.OID_CONTENEDOR IS NOT NULL THEN
          DER.OID_DOCUMENTO
         ELSE
          NULL
       END AS C_OID_DOCUMENTO_DE,
       CASE
         WHEN C.OID_CONTENEDOR IS NOT NULL THEN
          DER.COD_ESTADO_DOCXELEMENTO
         ELSE
          NULL
       END AS C_COD_ESTADO_DOCXELEMENTO,

       -- Remesas
       R.OID_REMESA AS R_OID_REMESA,
       R.COD_RECIBO_SALIDA AS R_COD_RECIBO_SALIDA,
       R.COD_ESTADO AS R_COD_ESTADO,
       R.COD_USUARIO_RESPONSABLE AS R_COD_USUARIO_RESPONSABLE,
       R.COD_PUESTO_RESPONSABLE AS R_COD_PUESTO_RESPONSABLE,
       R.FYH_CONTEO_INICIO AS R_FYH_CONTEO_INICIO,
       R.FYH_CONTEO_FIN AS R_FYH_CONTEO_FIN,
       R.GMT_CREACION AS R_GMT_CREACION,
       R.DES_USUARIO_CREACION AS R_DES_USUARIO_CREACION,
       R.GMT_MODIFICACION AS R_GMT_MODIFICACION,
       R.DES_USUARIO_MODIFICACION AS R_DES_USUARIO_MODIFICACION,
       R.OID_IAC AS R_OID_IAC,
       R.COD_RUTA AS R_COD_RUTA,
       R.OID_REMESA_ORIGEN AS R_OID_REMESA_ORIGEN,
       R.NEL_CANTIDAD_BULTOS AS R_NEL_CANTIDAD_BULTOS,
       R.OID_CUENTA AS R_OID_CUENTA,
       R.COD_EXTERNO AS R_COD_EXTERNO,
       R.FYH_TRANSPORTE AS R_FYH_TRANSPORTE,
       R.NEL_PARADA AS R_NEL_PARADA,
       R.OID_DOCUMENTO AS R_OID_DOCUMENTO,
       R.OID_EXTERNO AS R_OID_EXTERNO,
       R.BOL_GESTION_BULTO AS R_BOL_GESTION_BULTO,
       R.OID_REMESA_PADRE AS R_OID_REMESA_PADRE,
       R.OID_REMESA_SUSTITUTA AS R_OID_REMESA_SUSTITUTA,
       R.COD_CAJERO AS R_COD_CAJERO,
       R.BOL_ANULADO AS R_BOL_ANULADO,
       R.BOL_NOENTREGUE AS R_BOL_NOENTREGUE,
       R.COD_NIVEL_DETALLE AS R_COD_NIVEL_DETALLE,
       R.OID_CUENTA_SALDO AS R_OID_CUENTA_SALDO,
       R.COD_ESTADO_ABONO AS R_COD_ESTADO_ABONO,
       R.OID_CONTENEDOR_PADRE AS R_OID_CONTENEDOR_PADRE,
       R.OID_CONTENEDOR_MAYOR_NIVEL AS R_OID_CONTENEDOR_MAYOR_NIVEL,
       CASE
         WHEN R.OID_REMESA IS NOT NULL THEN
          DER.OID_DOCUMENTO
         ELSE
          NULL
       END AS R_OID_DOCUMENTO_DE,
       CASE
         WHEN R.OID_REMESA IS NOT NULL THEN
          DER.COD_ESTADO_DOCXELEMENTO
         ELSE
          NULL
       END AS R_COD_ESTADO_DOCXELEMENTO,
       R.Rowver AS R_ROWVER,

       -- Bultos
       B.OID_BULTO AS B_OID_BULTO,
       B.OID_CUENTA AS B_OID_CUENTA,
       B.OID_CUENTA_SALDO AS B_OID_CUENTA_SALDO,
       B.COD_PRECINTO AS B_COD_PRECINTO,
       B.COD_ESTADO AS B_COD_ESTADO,
       B.GMT_CREACION AS B_GMT_CREACION,
       B.GMT_MODIFICACION AS B_GMT_MODIFICACION,
       B.COD_PUESTO_RESPONSABLE AS B_COD_PUESTO_RESPONSABLE,
       B.DES_USUARIO_CREACION AS B_DES_USUARIO_CREACION,
       B.DES_USUARIO_MODIFICACION AS B_DES_USUARIO_MODIFICACION,
       B.COD_USUARIO_RESPONSABLE AS B_COD_USUARIO_RESPONSABLE,
       B.OID_IAC AS B_OID_IAC,
       B.OID_IAC_PARCIALES AS B_OID_IAC_PARCIALES,
       B.COD_BOLSA AS B_COD_BOLSA,
       B.OID_CUENTA AS B_OID_CUENTA,
       B.OID_EXTERNO AS B_OID_EXTERNO,
       B.FYH_PROCESO_LEGADO AS B_FYH_PROCESO_LEGADO,
       B.NEL_CANTIDAD_PARCIALES AS B_NEL_CANTIDAD_PARCIALES,
       B.BOL_CUADRADO AS B_BOL_CUADRADO,
       B.OID_BULTO_PADRE AS B_OID_BULTO_PADRE,
       B.COD_RUTA AS B_COD_RUTA,
       B.OID_BANCO_INGRESO AS B_OID_BANCO_INGRESO,
       B.BOL_BANCO_INGRESO_ES_MADRE AS B_BOL_BANCO_INGRESO_ES_MADRE,
       B.BOL_GESTION_BULTO AS B_BOL_GESTION_BULTO,
       B.OID_BULTO_SUSTITUTO AS B_OID_BULTO_SUSTITUTO,
       B.COD_ESTADO_ABONO AS B_COD_ESTADO_ABONO,
       B.OID_CONTENEDOR_PADRE AS B_OID_CONTENEDOR_PADRE,
       B.OID_CONTENEDOR_MAYOR_NIVEL AS B_OID_CONTENEDOR_MAYOR_NIVEL,
       B.FYH_CONTEO_INICIO AS B_FYH_CONTEO_INICIO,
       B.FYH_CONTEO_FIN AS B_FYH_CONTEO_FIN,
       B.COD_NIVEL_DETALLE AS B_COD_NIVEL_DETALLE,
       (SELECT LV1.COD_VALOR
          FROM SAPR_TLISTA_VALORXELEMENTO LVB1
         INNER JOIN GEPR_TLISTA_TIPO LT1
            ON LT1.OID_LISTA_TIPO = LVB1.OID_LISTA_TIPO
           AND LT1.COD_TIPO = '04'
         INNER JOIN GEPR_TLISTA_VALOR LV1
            ON LV1.OID_LISTA_VALOR = LVB1.OID_LISTA_VALOR
         WHERE LVB1.OID_BULTO = B.OID_BULTO
           AND rownum = 1) AS B_TIPOUBICACION,
       CASE
         WHEN B.OID_BULTO IS NOT NULL THEN
          DER.OID_DOCUMENTO
         ELSE
          NULL
       END AS B_OID_DOCUMENTO_DE,
       CASE
         WHEN B.OID_BULTO IS NOT NULL THEN
          DER.COD_ESTADO_DOCXELEMENTO
         ELSE
          NULL
       END AS B_COD_ESTADO_DOCXELEMENTO,
       B.Rowver AS B_ROWVER,
       
       -- Parciales
       P.OID_PARCIAL              AS P_OID_PARCIAL,
       P.COD_PRECINTO             AS P_COD_PRECINTO,
       P.COD_ESTADO               AS P_COD_ESTADO,
       P.COD_USUARIO_RESPONSABLE  AS P_COD_USUARIO_RESPONSABLE,
       P.COD_PUESTO_RESPONSABLE   AS P_COD_PUESTO_RESPONSABLE,
       P.NEC_SECUENCIA            AS P_NEC_SECUENCIA,
       P.GMT_CREACION             AS P_GMT_CREACION,
       P.DES_USUARIO_CREACION     AS P_DES_USUARIO_CREACION,
       P.GMT_MODIFICACION         AS P_GMT_MODIFICACION,
       P.DES_USUARIO_MODIFICACION AS P_DES_USUARIO_MODIFICACION,
       P.OID_IAC                  AS P_OID_IAC,
       P.FYH_CONTEO_INICIO        AS P_FYH_CONTEO_INICIO,
       P.FYH_CONTEO_FIN           AS P_FYH_CONTEO_FIN,
       P.OID_EXTERNO              AS P_OID_EXTERNO,
       P.OID_PARCIAL_PADRE        AS P_OID_PARCIAL_PADRE,
       P.OID_PARCIAL_SUSTITUTA    AS P_OID_PARCIAL_SUSTITUTA

        FROM SAPR_TDOCUMENTOXELEMENTO DER
        LEFT JOIN SAPR_TCONTENEDOR C
          ON C.OID_CONTENEDOR = DER.OID_CONTENEDOR
        LEFT JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DER.OID_REMESA
        LEFT JOIN SAPR_TBULTO B
          ON B.OID_BULTO = DER.OID_BULTO
        LEFT JOIN SAPR_TPARCIAL P
          ON P.OID_BULTO = B.OID_BULTO
       INNER JOIN SAPR_GTT_TOIDS tmp
          ON tmp.OID = DER.OID_DOCUMENTO
       WHERE tmp.cod_calificador = 'DOCUMENTO';
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_elementos;

  END fcargar_rc_elementos;

  /* fcargar_rc_valores_det_efec */
  FUNCTION fcargar_rc_valores_det_efec(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_valores_det_efectivo SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_valores_det_efectivo FOR
      SELECT 'CONTADO_EFECTIVO' AS TIPO,
             CONEF.OID_REMESA,
             CONEF.OID_BULTO,
             CONEF.OID_PARCIAL,
             D.OID_DIVISA,
             CONEF.OID_DENOMINACION,
             SUM(CONEF.NUM_IMPORTE) AS IMPORTE,
             SUM(CONEF.NEL_CANTIDAD) AS CANTIDAD,
             CONEF.OID_CALIDAD,
             CONEF.OID_UNIDAD_MEDIDA

        FROM SAPR_TCONTADO_EFECTIVO CONEF
       INNER JOIN GEPR_TDENOMINACION D
          ON D.OID_DENOMINACION = CONEF.OID_DENOMINACION
       WHERE CONEF.OID_DENOMINACION IS NOT NULL
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY CONEF.OID_REMESA,
                CONEF.OID_BULTO,
                CONEF.OID_PARCIAL,
                D.OID_DIVISA,
                CONEF.OID_DENOMINACION,
                CONEF.OID_CALIDAD,
                CONEF.OID_UNIDAD_MEDIDA

      UNION

      SELECT 'DIFERENCIA_EFECTIVO' AS TIPO,
             DIFEF.OID_REMESA,
             DIFEF.OID_BULTO,
             DIFEF.OID_PARCIAL,
             D.OID_DIVISA,
             DIFEF.OID_DENOMINACION,
             SUM(DIFEF.NUM_IMPORTE) AS IMPORTE,
             SUM(DIFEF.NEL_CANTIDAD) AS CANTIDAD,
             NULL AS OID_CALIDAD,
             DIFEF.OID_UNIDAD_MEDIDA

        FROM SAPR_TDIFERENCIA_EFECTIVO DIFEF
       INNER JOIN GEPR_TDENOMINACION D
          ON D.OID_DENOMINACION = DIFEF.OID_DENOMINACION
       WHERE DIFEF.OID_DENOMINACION IS NOT NULL
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY DIFEF.OID_REMESA,
                DIFEF.OID_BULTO,
                DIFEF.OID_PARCIAL,
                D.OID_DIVISA,
                DIFEF.OID_DENOMINACION,
                DIFEF.OID_UNIDAD_MEDIDA

      UNION

      SELECT 'DECLARADO_EFECTIVO' AS TIPO,
             DECEF.OID_REMESA,
             DECEF.OID_BULTO,
             DECEF.OID_PARCIAL,
             D.OID_DIVISA,
             DECEF.OID_DENOMINACION,
             SUM(DECEF.NUM_IMPORTE) AS IMPORTE,
             SUM(DECEF.NEL_CANTIDAD) AS CANTIDAD,
             NULL AS OID_CALIDAD,
             DECEF.OID_UNIDAD_MEDIDA

        FROM SAPR_TDECLARADO_EFECTIVO DECEF
       INNER JOIN GEPR_TDENOMINACION D
          ON D.OID_DENOMINACION = DECEF.OID_DENOMINACION
       WHERE DECEF.OID_DENOMINACION IS NOT NULL
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY DECEF.OID_REMESA,
                DECEF.OID_BULTO,
                DECEF.OID_PARCIAL,
                D.OID_DIVISA,
                DECEF.OID_DENOMINACION,
                DECEF.OID_UNIDAD_MEDIDA;
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_valores_det_efectivo;

  END fcargar_rc_valores_det_efec;

  /* fcargar_rc_valores_det_mp */
  FUNCTION fcargar_rc_valores_det_mp(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_valores_det_medio_pago SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_valores_det_medio_pago FOR
      SELECT 'CONTADO_MEDIO_PAGO' AS TIPO,
             CONMP.OID_REMESA,
             CONMP.OID_BULTO,
             CONMP.OID_PARCIAL,
             CONMP.OID_MEDIO_PAGO,
             MP.OID_DIVISA,
             SUM(CONMP.NUM_IMPORTE) AS IMPORTE,
             SUM(CONMP.NEL_CANTIDAD) AS CANTIDAD,
             CONMP.COD_TIPO_CONTADO,
             CONMP.OID_CONTADO_MEDIO_PAGO,
             NULL AS OID_DECLARADO_MEDIO_PAGO

        FROM SAPR_TCONTADO_MEDIO_PAGO CONMP
       INNER JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = CONMP.OID_MEDIO_PAGO
       WHERE CONMP.OID_MEDIO_PAGO IS NOT NULL
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY CONMP.OID_REMESA,
                CONMP.OID_BULTO,
                CONMP.OID_PARCIAL,
                CONMP.OID_MEDIO_PAGO,
                MP.OID_DIVISA,
                CONMP.COD_TIPO_CONTADO,
                CONMP.OID_CONTADO_MEDIO_PAGO

      UNION

      SELECT 'DIFERENCIA_MEDIO_PAGO' AS TIPO,
             DIFMP.OID_REMESA,
             DIFMP.OID_BULTO,
             DIFMP.OID_PARCIAL,
             DIFMP.OID_MEDIO_PAGO,
             MP.OID_DIVISA,
             SUM(DIFMP.NUM_IMPORTE) AS IMPORTE,
             SUM(DIFMP.NEL_CANTIDAD) AS CANTIDAD,
             NULL AS COD_TIPO_CONTADO,
             NULL AS OID_CONTADO_MEDIO_PAGO,
             NULL AS OID_DECLARADO_MEDIO_PAGO

        FROM SAPR_TDIFERENCIA_MEDIO_PAGO DIFMP
       INNER JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = DIFMP.OID_MEDIO_PAGO
       WHERE DIFMP.OID_MEDIO_PAGO IS NOT NULL
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY DIFMP.OID_REMESA,
                DIFMP.OID_BULTO,
                DIFMP.OID_PARCIAL,
                DIFMP.OID_MEDIO_PAGO,
                MP.OID_DIVISA,
                DIFMP.OID_UNIDAD_MEDIDA

      UNION

      SELECT 'DECLARADO_MEDIO_PAGO' AS TIPO,
             DECMP.OID_REMESA,
             DECMP.OID_BULTO,
             DECMP.OID_PARCIAL,
             DECMP.OID_MEDIO_PAGO,
             MP.OID_DIVISA,
             SUM(DECMP.NUM_IMPORTE) AS IMPORTE,
             SUM(DECMP.NEL_CANTIDAD) AS CANTIDAD,
             NULL AS COD_TIPO_CONTADO,
             NULL AS OID_CONTADO_MEDIO_PAGO,
             DECMP.OID_DECLARADO_MEDIO_PAGO

        FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
       INNER JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
       WHERE DECMP.OID_MEDIO_PAGO IS NOT NULL
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY DECMP.OID_REMESA,
                DECMP.OID_BULTO,
                DECMP.OID_PARCIAL,
                DECMP.OID_MEDIO_PAGO,
                MP.OID_DIVISA,
                DECMP.OID_UNIDAD_MEDIDA,
                DECMP.OID_DECLARADO_MEDIO_PAGO;
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_valores_det_medio_pago;

  END fcargar_rc_valores_det_mp;

  /* fcargar_rc_valores_totales */
  FUNCTION fcargar_rc_valores_totales(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_valores_totales SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_valores_totales FOR
    -- SAPR_TDECLARADO_EFECTIVO
      SELECT 'DECLARADO_EFECTIVO' AS TIPO,
             DECEF.OID_REMESA,
             DECEF.OID_BULTO,
             DECEF.OID_PARCIAL,
             DECEF.OID_DIVISA,
             DECEF.COD_NIVEL_DETALLE,
             NULL AS OID_DEC_CONT_MEDIO_PAGO,
             NULL AS COD_TIPO_MEDIO_PAGO,
             SUM(DECEF.NUM_IMPORTE) AS IMPORTE,
             SUM(DECEF.NEL_CANTIDAD) AS CANTIDAD

        FROM SAPR_TDECLARADO_EFECTIVO DECEF
        LEFT JOIN GEPR_TDENOMINACION D
          ON D.OID_DENOMINACION = DECEF.OID_DENOMINACION
       WHERE DECEF.COD_NIVEL_DETALLE <> 'D'
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY DECEF.OID_REMESA,
                DECEF.OID_BULTO,
                DECEF.OID_PARCIAL,
                DECEF.OID_DIVISA,
                DECEF.COD_NIVEL_DETALLE

      UNION

      -- SAPR_TDIFERENCIA_EFECTIVO
      SELECT 'DIFERENCIA_EFECTIVO' AS TIPO,
             DIFEF.OID_REMESA,
             DIFEF.OID_BULTO,
             DIFEF.OID_PARCIAL,
             DIFEF.OID_DIVISA,
             DIFEF.COD_NIVEL_DETALLE,
             NULL AS OID_DEC_CONT_MEDIO_PAGO,
             NULL AS COD_TIPO_MEDIO_PAGO,
             SUM(DIFEF.NUM_IMPORTE) AS IMPORTE,
             SUM(DIFEF.NEL_CANTIDAD) AS CANTIDAD

        FROM SAPR_TDIFERENCIA_EFECTIVO DIFEF
        LEFT JOIN GEPR_TDENOMINACION D
          ON D.OID_DENOMINACION = DIFEF.OID_DENOMINACION
       WHERE DIFEF.COD_NIVEL_DETALLE <> 'D'
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY DIFEF.OID_REMESA,
                DIFEF.OID_BULTO,
                DIFEF.OID_PARCIAL,
                DIFEF.OID_DIVISA,
                DIFEF.COD_NIVEL_DETALLE

      UNION

      -- SAPR_TDECLARADO_MEDIO_PAGO
      SELECT 'DECLARADO_MEDIO_PAGO' AS TIPO,
             DECMP.OID_REMESA,
             DECMP.OID_BULTO,
             DECMP.OID_PARCIAL,
             DECMP.OID_DIVISA,
             DECMP.COD_NIVEL_DETALLE,
             DECMP.OID_DECLARADO_MEDIO_PAGO AS OID_DEC_CONT_MEDIO_PAGO,
             DECMP.COD_TIPO_MEDIO_PAGO,
             SUM(DECMP.NUM_IMPORTE) AS IMPORTE,
             SUM(DECMP.NEL_CANTIDAD) AS CANTIDAD

        FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
        LEFT JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
       WHERE DECMP.COD_NIVEL_DETALLE <> 'D'
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY DECMP.OID_REMESA,
                DECMP.OID_BULTO,
                DECMP.OID_PARCIAL,
                DECMP.OID_DIVISA,
                DECMP.COD_NIVEL_DETALLE,
                DECMP.OID_DECLARADO_MEDIO_PAGO,
                DECMP.COD_TIPO_MEDIO_PAGO

      UNION

      -- SAPR_TDIFERENCIA_MEDIO_PAGO
      SELECT 'DIFERENCIA_MEDIO_PAGO' AS TIPO,
             DIFMP.OID_REMESA,
             DIFMP.OID_BULTO,
             DIFMP.OID_PARCIAL,
             DIFMP.OID_DIVISA,
             DIFMP.COD_NIVEL_DETALLE,
             NULL AS OID_DEC_CONT_MEDIO_PAGO,
             DIFMP.COD_TIPO_MEDIO_PAGO,
             SUM(DIFMP.NUM_IMPORTE) AS IMPORTE,
             SUM(DIFMP.NEL_CANTIDAD) AS CANTIDAD
        FROM SAPR_TDIFERENCIA_MEDIO_PAGO DIFMP
        LEFT JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = DIFMP.OID_MEDIO_PAGO
       WHERE DIFMP.COD_NIVEL_DETALLE <> 'D'
         AND OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
       GROUP BY DIFMP.OID_REMESA,
                DIFMP.OID_BULTO,
                DIFMP.OID_PARCIAL,
                DIFMP.OID_DIVISA,
                DIFMP.COD_NIVEL_DETALLE,
                DIFMP.COD_TIPO_MEDIO_PAGO;
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_valores_totales;

  END fcargar_rc_valores_totales;

  /* fcargar_rc_iac */
  FUNCTION fcargar_rc_iac(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_iac SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_iac FOR
      SELECT IAC.OID_IAC,
             IAC.COD_IAC,
             IAC.DES_IAC,
             IAC.OBS_IAC,
             IAC.BOL_VIGENTE,
             IAC.BOL_INVISIBLE,
             IAC.BOL_COPIA_DECLARADOS
        FROM GEPR_TINFORM_ADICIONAL_CLIENTE IAC
       WHERE OID_IAC IN (SELECT DISTINCT tmp.OID
                           FROM SAPR_GTT_TOIDS tmp
                          WHERE tmp.cod_calificador = 'IAC');
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_iac;

  END fcargar_rc_iac;

  /* fcargar_rc_terminos_iac */
  FUNCTION fcargar_rc_terminos_iac(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_terminos_iac SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_terminos_iac FOR
      SELECT TI.OID_IAC,
             TI.OID_TERMINO,
             TI.BOL_BUSQUEDA_PARCIAL,
             TI.BOL_CAMPO_CLAVE,
             TI.NEC_ORDEN,
             TI.BOL_ES_OBLIGATORIO,
             TI.BOL_TERMINO_COPIA,
             TI.BOL_ES_PROTEGIDO

            ,
             T.COD_TERMINO,
             T.DES_TERMINO,
             T.OBS_TERMINO,
             T.NEC_LONGITUD,
             T.BOL_MOSTRAR_CODIGO,
             T.BOL_VALORES_POSIBLES,
             T.BOL_ACEPTAR_DIGITACION,
             T.BOL_VIGENTE,
             T.BOL_ESPECIFICO_DE_SALDOS

            ,
             FORM.OID_FORMATO,
             FORM.COD_FORMATO,
             FORM.DES_FORMATO

            ,
             AV.OID_ALGORITMO_VALIDACION,
             AV.COD_ALGORITMO_VALIDACION,
             AV.DES_ALGORITMO_VALIDACION,
             AV.OBS_ALGORITMO_VALIDACION,
             AV.BOL_APLICA_TERM_MEDIO_PAGO AS AV_BOL_APLICA_TERM_MEDIO_PAGO,
             AV.BOL_APLICA_TERM_IAC        AS AV_BOL_APLICA_TERM_IAC

            ,
             M.OID_MASCARA,
             M.COD_MASCARA,
             M.DES_MASCARA,
             M.DES_EXP_REGULAR,
             M.BOL_APLICA_TERM_MEDIO_PAGO AS M_BOL_APLICA_TERM_MEDIO_PAGO,
             M.BOL_APLICA_TERM_IAC        AS M_BOL_APLICA_TERM_IAC

        FROM GEPR_TTERMINO_POR_IAC TI
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       INNER JOIN GEPR_TFORMATO FORM
          ON FORM.OID_FORMATO = T.OID_FORMATO
        LEFT OUTER JOIN GEPR_TALGORITMO_VALIDACION AV
          ON AV.OID_ALGORITMO_VALIDACION = T.OID_ALGORITMO_VALIDACION
        LEFT OUTER JOIN GEPR_TMASCARA M
          ON M.OID_MASCARA = T.OID_MASCARA
       WHERE OID_IAC IN (SELECT DISTINCT tmp.OID
                           FROM SAPR_GTT_TOIDS tmp
                          WHERE tmp.cod_calificador = 'IAC');
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_terminos_iac;

  END fcargar_rc_terminos_iac;

  /* fcargar_rc_valor_termino_iac */
  FUNCTION fcargar_rc_valor_termino_iac(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_valor_termino_iac SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_valor_termino_iac FOR
      SELECT IAC.OID_REMESA    AS OID_REMESA,
             NULL              AS OID_BULTO,
             NULL              AS OID_PARCIAL,
             TIAC.OID_IAC,
             IAC.OID_TERMINO,
             IAC.DES_VALOR_IAC
        FROM SAPR_TVALOR_IACXREMESA IAC
       INNER JOIN GEPR_TTERMINO_POR_IAC TIAC
          ON TIAC.OID_TERMINO = IAC.OID_TERMINO
       WHERE OID_REMESA IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'REMESA')
      UNION
      SELECT NULL              AS OID_REMESA,
             IAC.OID_BULTO     AS OID_BULTO,
             NULL              AS OID_PARCIAL,
             TIAC.OID_IAC,
             IAC.OID_TERMINO,
             IAC.DES_VALOR_IAC
        FROM SAPR_TVALOR_IACXBULTO IAC
       INNER JOIN GEPR_TTERMINO_POR_IAC TIAC
          ON TIAC.OID_TERMINO = IAC.OID_TERMINO
       WHERE OID_BULTO IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'BULTO')
      UNION
      SELECT NULL              AS OID_REMESA,
             NULL              AS OID_BULTO,
             IAC.OID_PARCIAL   AS OID_PARCIAL,
             TIAC.OID_IAC,
             IAC.OID_TERMINO,
             IAC.DES_VALOR_IAC
        FROM SAPR_TVALOR_IACXPARCIAL IAC
       INNER JOIN GEPR_TTERMINO_POR_IAC TIAC
          ON TIAC.OID_TERMINO = IAC.OID_TERMINO
       WHERE OID_PARCIAL IN
             (SELECT DISTINCT tmp.OID
                FROM SAPR_GTT_TOIDS tmp
               WHERE tmp.cod_calificador = 'PARCIAL');
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_valor_termino_iac;

  END fcargar_rc_valor_termino_iac;

END SAPR_PELEMENTO_###VERSION###;
/