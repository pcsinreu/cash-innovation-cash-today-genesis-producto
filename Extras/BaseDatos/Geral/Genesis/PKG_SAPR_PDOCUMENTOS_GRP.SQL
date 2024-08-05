CREATE OR REPLACE PACKAGE sapr_pdocumentos_grp_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pdocumento_grp_###VERSION###';

  /* Guardar grupo documentos */
  PROCEDURE sguardar_grp_documentos(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$adocs_oid                 IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtgdoc_oid_termino       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtgdoc_des_valor         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$esnuevo                   OUT BOOLEAN,
                                    par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Validar grupo documentos */
  PROCEDURE svalidar_grp_documentos(par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_formulario_grupo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_origen    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_destino   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Guardar grupo - Gestion de Fondos y Cierre */
  PROCEDURE sguardar_grp_docs_valores(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,

                                      /* arrays asociativos */
                                      /* cuenta origen */
                                      par$adocs_mov_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,

                                      /* cuenta destino */
                                      par$adocs_mov_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,

                                      /* arrays de documentos */
                                      par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                      par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                      par$adocs_cod_externo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_rowver         IN gepr_pcomon_###VERSION###.tipo$nels_,

                                      /* arrays de efectivo por documento */
                                      par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                      par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                      /* arrays de medio de pago por documento */
                                      par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                      par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                 par$aefadoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefadoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefadoc_oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefadoc_oid_unid_medida   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefadoc_cod_niv_detalle   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aefadoc_cod_tp_efec_tot   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aefadoc_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefadoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                 par$aefadoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 par$ampadoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampadoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampadoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampadoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$ampadoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$ampadoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                 par$ampadoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 par$avtmpadoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtmpadoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtmpadoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$avtmpadoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                      /* arrays de terminos de medio de pago por documento */
                                      par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                      /* arrays de terminos por documento */
                                      par$avtdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtdoc_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtdoc_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,

                                      /* arrays de terminos del grupo de documentos */
                                      par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                      /* cultura */
                                      par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,

                                      /* informacion de ejecucion */
                                      par$info_ejecucion           IN VARCHAR2,
                                      par$hacer_commit             IN INTEGER,
                                      par$confirmar_doc            IN INTEGER,
                                      par$rc_grupo_documentos      OUT sys_refcursor,
                                      par$rc_historico_mov_grupo   OUT sys_refcursor,
                                      par$doc_rc_documentos        OUT sys_refcursor,
                                      par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                      par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validar grupo - Gestion de Fondos y Cierre */
  PROCEDURE svalidar_grp_docs_valores(par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_formulario_grupo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_sector_origen    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$rowver               IN gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Guardar grupo documento de Actas - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_grp_docs_elem_actas(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$bol_gestion_bulto         IN gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$cod_integracion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                         par$oid_sector                IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,

                                         par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtgdoc_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                         /* arrays asociativos */
                                         /* arrays de documentos */
                                         par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adocs_cod_externo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_rowver         IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de efectivo por documento */
                                         par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                         par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de medio de pago por documento */
                                         par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                         par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de terminos de medio de pago por documento */
                                         par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                         par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                         /* arrays de terminos por documento */
                                         par$avtdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtdoc_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtdoc_cod_termino   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$avtdoc_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,

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

                                         par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                         par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion           IN VARCHAR2,
                                         par$hacer_commit             IN INTEGER,
                                         par$confirmar_doc            IN INTEGER,
                                         par$rc_grupo_documentos      OUT sys_refcursor,
                                         par$rc_historico_mov_grupo   OUT sys_refcursor,
                                         par$doc_rc_documentos        OUT sys_refcursor,
                                         par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                         par$ele_rc_elementos         OUT sys_refcursor,
                                         par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento de Altas - Gestion de Bultos/Remesas/ALTAS */
  PROCEDURE sguardar_grp_docs_elem_altas(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$bol_gestion_bulto         IN gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$cod_integracion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                         par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,

                                         /* arrays asociativos */
                                         /* arrays de terminos del grupo de documentos */
                                         par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtgdoc_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                         /* arrays de documentos */
                                         par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adocs_cod_externo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_rowver         IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* cuenta origen */
                                         par$adocs_mov_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,

                                         /* cuenta destino */
                                         par$adocs_mov_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,

                                         /* arrays de efectivo por documento */
                                         par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                         par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de medio de pago por documento */
                                         par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                         par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de terminos de medio de pago por documento */
                                         par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                         par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                         /* arrays de terminos por documento */
                                         par$avtdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtdoc_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtdoc_cod_termino   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$avtdoc_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,

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
                                         par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                         par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion           IN VARCHAR2,
                                         par$hacer_commit             IN INTEGER,
                                         par$confirmar_doc            IN INTEGER,
                                         par$rc_grupo_documentos      OUT sys_refcursor,
                                         par$rc_historico_mov_grupo   OUT sys_refcursor,
                                         par$doc_rc_documentos        OUT sys_refcursor,
                                         par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                         par$ele_rc_elementos         OUT sys_refcursor,
                                         par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento de Reenvio y Baja - Gestion de Bultos/Remesas/REENVIO y BAJA */
  PROCEDURE sguardar_grp_docs_elem_re_baj(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,

                                          /* arrays asociativos */
                                          /* arrays de terminos del grupo de documentos */
                                          par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                          /* arrays de documentos */
                                          par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_cod_externo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_rowver         IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* cuenta origen */
                                          par$adocs_mov_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,

                                          /* cuenta destino */
                                          par$adocs_mov_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,

                                          /* arrays de efectivo por documento */
                                          par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                          par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* arrays de medio de pago por documento */
                                          par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                          par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* arrays de terminos de medio de pago por documento */
                                          par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                          par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                          /* Remesa - arrays */
                                          par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
										  par$aremdoc_rowver            IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* Bulto - arrays */
                                          par$abuldoc_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
										  par$abuldoc_rowver        IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* informacion de ejecucion */
                                          par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$hacer_commit             IN INTEGER,
                                          par$confirmar_doc            IN INTEGER,
                                          par$rc_grupo_documentos      OUT sys_refcursor,
                                          par$rc_historico_mov_grupo   OUT sys_refcursor,
                                          par$doc_rc_documentos        OUT sys_refcursor,
                                          par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                          par$ele_rc_elementos         OUT sys_refcursor,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento de Reenvio - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_grp_docs_elem_reenvi(par$aremdoc_oid_remesa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aremdoc_cod_remesa       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aremdoc_rowver           IN gepr_pcomon_###VERSION###.tipo$nels_,
                                          par$abuldoc_oid_bulto        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_cod_bulto        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$abuldoc_rowver           IN gepr_pcomon_###VERSION###.tipo$nels_,
                                          par$adocs_mov_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$bol_gestion_bulto        IN gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$cod_integracion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$hacer_commit             IN INTEGER,
                                          par$confirmar_doc            IN INTEGER,
                                          par$aceptar_doc              IN INTEGER,
                                          par$rc_grupo_documentos      OUT sys_refcursor,
                                          par$rc_historico_mov_grupo   OUT sys_refcursor,
                                          par$doc_rc_documentos        OUT sys_refcursor,
                                          par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                          par$ele_rc_elementos         OUT sys_refcursor,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento de Salidas Recorido - Gestion de Remesas */
  PROCEDURE sguardar_grp_docs_elem_salida(par$cod_ruta                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$fyh_plncertif            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                          par$arem_cod_remesa          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$cod_delegacion           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_planta               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_sector               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_integracion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$hacer_commit             IN INTEGER,
                                          par$confirmar_doc            IN INTEGER,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* supd_tgrupo_documento */
  PROCEDURE supd_tgrupo_documento(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_formulario            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_estado                IN gepr_pcomon_###VERSION###.tipo$est_,
                                  par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$esnuevo                   OUT BOOLEAN,
                                  par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$updates                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_thist_mov_grupo_docs */
  PROCEDURE sins_thist_mov_grupo_docs(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tvalor_termxgrupodoc */
  PROCEDURE sins_tvalor_termxgrupodoc(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_termino         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$des_valor           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  -- sdel_tvalor_termxgrupodoc
  PROCEDURE sdel_tvalor_termxgrupodoc(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* fexiste_documentos_grp */
  FUNCTION fexiste_documentos_grp(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN;

  /* Determina si hay algun cierre de caja en curso para un sector de origen */
  FUNCTION fhay_cierrecaja_encurso(par$oid_sector_origen    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_formulario_grupo IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN;

  /* srecuperar_grp_documentos */
  PROCEDURE srecuperar_grp_documentos(par$oid_grupo_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$rc_grupo_documentos        OUT sys_refcursor,
                                      par$rc_grupo_terminos_grupo    OUT sys_refcursor,
                                      par$rc_terminos_grupo          OUT sys_refcursor,
                                      par$rc_valor_terminos_grupo    OUT sys_refcursor,
                                      par$rc_historico_mov_grupo     OUT sys_refcursor,
                                      par$doc_rc_documentos          OUT sys_refcursor,
                                      par$doc_rc_documentos_padre    OUT sys_refcursor,
                                      par$doc_rc_formulario          OUT sys_refcursor,
                                      par$doc_rc_accion_contable     OUT sys_refcursor,
                                      par$doc_rc_estado_acc_contable OUT sys_refcursor,
                                      par$doc_rc_caract_formulario   OUT sys_refcursor,
                                      par$doc_rc_grp_terminos_indiv  OUT sys_refcursor,

                                      par$doc_rc_terminos_indiv      OUT sys_refcursor,
                                      par$doc_rc_valor_terminos_doc  OUT sys_refcursor,
                                      par$doc_rc_sectores            OUT sys_refcursor,
                                      par$doc_rc_tipos_sectores      OUT sys_refcursor,
                                      par$doc_rc_caract_tp_sectores  OUT sys_refcursor,
                                      par$doc_rc_cuentas             OUT sys_refcursor,
                                      par$doc_rc_historico_mov_doc   OUT sys_refcursor,
                                      par$doc_rc_valores_documentos  OUT sys_refcursor,
                                      par$doc_rc_divisas             OUT sys_refcursor,
                                      par$doc_rc_denominaciones      OUT sys_refcursor,
                                      par$doc_rc_mediospago          OUT sys_refcursor,
                                      par$doc_rc_unidades_medida     OUT sys_refcursor,
                                      par$doc_rc_calidades           OUT sys_refcursor,
                                      par$doc_rc_terminos_mediospago OUT sys_refcursor,
                                      par$doc_rc_plantas             OUT sys_refcursor,
                                      par$doc_rc_delegaciones        OUT sys_refcursor,
                                      par$doc_rc_tipos_sector_planta OUT sys_refcursor,
                                      par$ele_rc_elementos           OUT sys_refcursor,
                                      par$ele_rc_cuentas             OUT sys_refcursor,
                                      par$ele_rc_carac_tipo_sector   OUT sys_refcursor,
                                      par$ele_rc_val_det_efectivo    OUT sys_refcursor,
                                      par$ele_rc_val_det_medio_pago  OUT sys_refcursor,
                                      par$ele_rc_val_totales         OUT sys_refcursor,
                                      par$ele_rc_lista_valor         OUT sys_refcursor,
                                      par$ele_rc_iac                 OUT sys_refcursor,
                                      par$ele_rc_terminos_iac        OUT sys_refcursor,
                                      par$ele_rc_valor_termino_iac   OUT sys_refcursor,
                                      par$ele_rc_divisas             OUT sys_refcursor,
                                      par$ele_rc_denominaciones      OUT sys_refcursor,
                                      par$ele_rc_medio_pago          OUT sys_refcursor,
                                      par$ele_rc_unidad_medida       OUT sys_refcursor,
                                      par$ele_rc_calidad             OUT sys_refcursor,
                                      par$ele_rc_valor_termino       OUT sys_refcursor,
                                      par$ele_rc_cont_precintos      OUT sys_refcursor,
                                      par$usuario                    IN VARCHAR2,
                                      par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$info_ejecucion             IN VARCHAR2,
                                      par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* fcargar_rc_grupo_documento */
  FUNCTION fcargar_rc_grupo_documento(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN sys_refcursor;

  /* fcargar_rc_valorterminosgrupo */
  FUNCTION fcargar_rc_valorterminosgrupo(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  /* fcargar_rc_historico */
  FUNCTION fcargar_rc_historico(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  /* supd_est_comp_grp_documento */
  PROCEDURE supd_est_comp_grp_documento(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_estado_grp_doc  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_comprobante     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_);

  /* supd_estado_grupo_documento */
  PROCEDURE supd_estado_grupo_documento(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_estado_grp_doc  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_);

  /* sins_tgrupo_documento_cont */
  PROCEDURE sins_tgrupo_documento_cont(par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$oid_contenedor       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_contenedor_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_remesa           in gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_bulto            in gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar grupo - Gestion de Contenedores */
  PROCEDURE sguardar_grp_docs_cont(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_comprobante           OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                   /*Terminos Grupo documento*/
                                   par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                   --documentos
                                   par$adoc_oid_documento       in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_documento_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_documento_sust  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_moviment_fondo  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_fyh_plan_certif     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoc_rowver              IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   /* cuenta origen */
                                   par$adoc_oid_cuenta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_cliente_origen    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_subcliente_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_puntoservico_ori  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_canal_origen      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_subcanal_origen   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_delegacion_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_planta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_sector_origen     IN gepr_pcomon_###VERSION###.tipo$oids_

                                   /* cuenta destino */,
                                   par$adoc_oid_cuenta_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_cliente_destino  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_subcliente_des   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_puntoservico_des IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_canal_destino    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_subcanal_destino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_delegacion_des   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_planta_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_sector_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* arrays asociativos */
                                   /* arrays de efectivo por documento */
                                   par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_

                                   /* arrays de medio de pago por documento */,
                                   par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$ampdoc_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* arrays de terminos de medio de pago por documento */
                                   par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpdoc_oid_mediopago   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                   -- contenedores
                                   par$adoccont_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoccont_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   -- Elementos
                                   par$aelco_oid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* informacion de ejecucion */
                                   par$usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$info_ejecucion IN VARCHAR2,
                                   par$hacer_commit   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$confirmar_doc  in gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$inserts        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$cod_ejecucion  OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar grupo - Gestion de Contenedores */
  PROCEDURE sguardar_grp_docs_cont_alta(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_comprobante           OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                        /*Terminos Grupo documento*/
                                        par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                        --documentos
                                        par$adoc_oid_documento       in gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_documento_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_documento_sust  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_moviment_fondo  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_fyh_plan_certif     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                        par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                        par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoc_rowver              IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        /* cuenta origen */
                                        par$adoc_oid_cuenta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_cliente_origen    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_subcliente_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_puntoservico_ori  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_canal_origen      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_subcanal_origen   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_delegacion_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_planta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_sector_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,

                                        /* cuenta destino */
                                        par$adoc_oid_cuenta_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_cliente_destino  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_subcliente_des   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_puntoservico_des IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_canal_destino    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_subcanal_destino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_delegacion_des   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_planta_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_sector_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                        /* arrays asociativos */
                                        /* arrays de efectivo por documento */
                                        par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                        /* arrays de medio de pago por documento */
                                        par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        par$ampdoc_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                        /* arrays de terminos de medio de pago por documento */
                                        par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpdoc_oid_mediopago   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                        -- contenedores
                                        par$adoccont_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoccont_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoccont_cod_tipo_cont     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_cod_tipo_servicio IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_cod_tipo_embalaje IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_cod_puesto        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_cod_contenedor    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                        -- Precintos
                                        par$aprco_oid_contenedor       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aprco_bol_precinto_autom   in gepr_pcomon_###VERSION###.tipo$nbols_,

                                        -- Elementos
                                        par$aelco_oid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        /* arrays de efectivo do contenedor */
                                        par$aefco_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_oid_denom         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_oid_uni_med       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aefco_cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aefco_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$aefco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                        /* arrays de medio de pago do contenedor */
                                        par$ampco_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampco_oid_mp            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampco_cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ampco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ampco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$ampco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        par$ampco_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                        /* arrays de terminos de medio de pago do contenedor */
                                        par$avtmpco_oid_contenedor   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpco_oid_mp           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpco_oid_termediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpco_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$avtmpco_nec_indicegrupo  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                        /* informacion de ejecucion */
                                        par$usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion IN VARCHAR2,
                                        par$hacer_commit   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$confirmar_doc  in gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$inserts        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$updates        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_ejecucion  OUT gepr_pcomon_###VERSION###.tipo$nel_);

END sapr_pdocumentos_grp_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pdocumentos_grp_###VERSION### AS

  /* Guardar grupo documentos */
  PROCEDURE sguardar_grp_documentos(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$adocs_oid                 IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtgdoc_oid_termino       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtgdoc_des_valor         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$esnuevo                   OUT BOOLEAN,
                                    par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    /* const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_documentos';*/

    /* variables locales */
    var$inserts_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$est_ := gepr_pcomon_###VERSION###.const$CodEstadoDocEnCurso;
    var$cod_est_doc_anulado  gepr_pcomon_###VERSION###.tipo$est_ := gepr_pcomon_###VERSION###.const$CodEstadoDocAnulado;
    var$cod_comp_doc_anulado gepr_pcomon_###VERSION###.tipo$cod_;
    var$info_ejecucion       gepr_pcomon_###VERSION###.tipo$obs_;
    var$cod_ejecucion        gepr_pcomon_###VERSION###.tipo$nel_;
    var$rowver               gepr_pcomon_###VERSION###.tipo$nel_;

    /* CURSOR DE DOCUMENTOS DEL GRUPO */
    CURSOR cur$documentos(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT D.OID_DOCUMENTO
        FROM SAPR_TDOCUMENTO D
       WHERE D.OID_GRUPO_DOCUMENTO = par$oid_grupo_documento
         AND D.OID_DOCUMENTO NOT IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS T
               WHERE T.COD_CALIFICADOR = 'DOCUMENTOGRUPO');

  BEGIN

    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;

    /* Validar grupo */
    svalidar_grp_documentos(par$oid_grupo_documento  => par$oid_grupo_documento,
                            par$oid_formulario_grupo => par$oid_formulario_grupo,
                            par$oid_sector_origen    => par$oid_sector_origen,
                            par$oid_sector_destino   => par$oid_sector_destino,
                            par$adocs_oid            => par$adocs_oid,
                            par$usuario              => par$usuario,
                            par$cod_cultura          => par$cod_cultura);

    /* Grabar grupo */
    supd_tgrupo_documento(par$oid_grupo_documento       => par$oid_grupo_documento,
                          par$oid_grupo_documento_padre => par$oid_grupo_documento_padre,
                          par$oid_formulario            => par$oid_formulario_grupo,
                          par$oid_sector_origen         => par$oid_sector_origen,
                          par$oid_sector_destino        => par$oid_sector_destino,
                          par$cod_estado                => var$cod_estado_documento,
                          par$usuario                   => par$usuario,
                          par$cod_cultura               => par$cod_cultura,
                          par$rowver                    => par$rowver,
                          par$esnuevo                   => par$esnuevo,
                          par$inserts                   => var$inserts_int,
                          par$updates                   => var$updates_int);

    par$inserts := par$inserts + var$inserts_int;
    par$updates := par$updates + var$updates_int;

    IF NOT par$esnuevo THEN
      sdel_tvalor_termxgrupodoc(par$oid_grupo_documento => par$oid_grupo_documento,
                                par$deletes             => var$deletes_int);
      par$deletes := par$deletes + var$deletes_int;
    END IF;

    /* Grabar terminos del grupo */
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$avtgdoc_oid_termino) THEN
      FOR idx IN par$avtgdoc_oid_termino.first .. par$avtgdoc_oid_termino.last LOOP
        sins_tvalor_termxgrupodoc(par$oid_grupo_documento => par$oid_grupo_documento,
                                  par$oid_termino         => par$avtgdoc_oid_termino(idx),
                                  par$des_valor           => par$avtgdoc_des_valor(idx),
                                  par$usuario             => par$usuario,
                                  par$inserts             => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      END LOOP;
    END IF;

    /* Grabar historico del grupo */
    IF par$esnuevo THEN
      sins_thist_mov_grupo_docs(par$oid_grupo_documento => par$oid_grupo_documento,
                                par$cod_estado          => var$cod_estado_documento,
                                par$usuario             => par$usuario,
                                par$inserts             => var$inserts_int);
      par$inserts := par$inserts + var$inserts_int;

    ELSE

      /* Si estoy modificando un grupo, tengo que validar los documentos enviados con los documentos
      que ya estan en la BBDD. Caso sea necesario, deve ser anulado los documentos sacados */

      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
        INSERT INTO SAPR_GTT_TOIDS
          (OID, COD_CALIFICADOR)
        VALUES
          (par$adocs_oid(idx), 'DOCUMENTOGRUPO');
        par$inserts := par$inserts + sql%ROWCOUNT;
      END LOOP idx;

      FOR rec$documento IN cur$documentos(par$oid_grupo_documento) LOOP

        /* desassociar documento del grupo */
        sapr_pdocumento_###VERSION###.supd_desassociar_grp(par$oid_documento => rec$documento.oid_documento,
                                                      par$usuario       => par$usuario,
                                                      par$updates       => var$updates_int);
        par$updates := par$updates + var$updates_int;

        SELECT D.ROWVER
          INTO var$rowver
          FROM SAPR_TDOCUMENTO D
         WHERE D.OID_DOCUMENTO = rec$documento.oid_documento;

        /* anular documento */
        sapr_ptransiciones_###VERSION###.sejecutar_doc(par$oid_llamada     => NULL,
                                                  par$oid_documento        => rec$documento.oid_documento,
                                                  par$cod_estado_documento => var$cod_est_doc_anulado,
                                                  par$cod_comprobante      => var$cod_comp_doc_anulado,
                                                  par$rowver               => var$rowver,
                                                  par$usuario              => par$usuario,
                                                  par$cod_cultura          => par$cod_cultura,
                                                  par$info_ejecucion       => var$info_ejecucion,
                                                  par$hacer_commit         => 0,
                                                  par$inserts              => var$inserts_int,
                                                  par$updates              => var$updates_int,
                                                  par$deletes              => var$deletes_int,
                                                  par$selects              => var$selects_int,
                                                  par$cod_ejecucion        => var$cod_ejecucion);

        par$inserts := par$inserts + var$inserts_int;
        par$updates := par$updates + var$updates_int;
        par$deletes := par$deletes + var$deletes_int;
        par$selects := par$selects + var$selects_int;

      END LOOP rec$documento;
      par$selects := par$selects + 1;

    END IF;

  END sguardar_grp_documentos;

  /* Validar grupo documentos */
  PROCEDURE svalidar_grp_documentos(par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_formulario_grupo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_origen    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_destino   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_grp_documentos';
  BEGIN

    IF par$oid_grupo_documento IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'oid_grupo_documento_invalido',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    END IF;

    IF par$oid_formulario_grupo IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'oid_formulario_invalido',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    END IF;

    IF par$oid_sector_origen IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'oid_sectororigem_invalido',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    END IF;

    IF par$oid_sector_destino IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'oid_sectordestino_invalido',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    END IF;

    IF par$usuario IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'dados_usuario_invalido',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    END IF;

    IF gepr_putilidades_###VERSION###.farray_vacio_oid(par$adocs_oid) THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'sin_documentos',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    END IF;

  END svalidar_grp_documentos;

  /* Guardar grupo - Gestion de Fondos y Cierre */
  PROCEDURE sguardar_grp_docs_valores(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,

                                      /* arrays asociativos */
                                      /* cuenta origen */
                                      par$adocs_mov_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_ori_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_ori_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,

                                      /* cuenta destino */
                                      par$adocs_mov_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_mov_des_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_mov_des_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_sal_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_sal_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,

                                      /* arrays de documentos */
                                      par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                      par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                      par$adocs_cod_externo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$adocs_rowver         IN gepr_pcomon_###VERSION###.tipo$nels_,

                                      /* arrays de efectivo por documento */
                                      par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                      par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                      /* arrays de medio de pago por documento */
                                      par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                      par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                      par$aefadoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefadoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefadoc_oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefadoc_oid_unid_medida   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefadoc_cod_niv_detalle   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aefadoc_cod_tp_efec_tot   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aefadoc_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aefadoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                      par$aefadoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                      par$ampadoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampadoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampadoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$ampadoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$ampadoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$ampadoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                      par$ampadoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                      par$avtmpadoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtmpadoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtmpadoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      par$avtmpadoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                      /* arrays de terminos de medio de pago por documento */
                                      par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                      par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                      /* arrays de terminos por documento */
                                      par$avtdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtdoc_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtdoc_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,

                                      /* arrays de terminos del grupo de documentos */
                                      par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                      /* cultura */
                                      par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,

                                      /* informacion de ejecucion */
                                      par$info_ejecucion           IN VARCHAR2,
                                      par$hacer_commit             IN INTEGER,
                                      par$confirmar_doc            IN INTEGER,
                                      par$rc_grupo_documentos      OUT sys_refcursor,
                                      par$rc_historico_mov_grupo   OUT sys_refcursor,
                                      par$doc_rc_documentos        OUT sys_refcursor,
                                      par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                      par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_docs_valores';

    /* variables locales */
    var$esnuevo              BOOLEAN;
    var$inicio               gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros            gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                  VARCHAR2(4000);
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante      gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver               gepr_pcomon_###VERSION###.tipo$nel_;
    var$array_fecha_vacio    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$array_desc_vacio      gepr_pcomon_###VERSION###.tipo$descs_;
  BEGIN

    /* Valores iniciales */
    var$inicio := SYSTIMESTAMP;

    /* Validar grupo */
    svalidar_grp_docs_valores(par$oid_grupo_documento  => par$oid_grupo_documento,
                              par$oid_formulario_grupo => par$oid_formulario_grupo,
                              par$oid_sector_origen    => par$oid_sector_origen,
                              par$rowver               => par$rowver,
                              par$cod_cultura          => par$cod_cultura);

    /* Grabar las informaciones del grupo */
    sguardar_grp_documentos(par$oid_grupo_documento       => par$oid_grupo_documento,
                            par$oid_grupo_documento_padre => par$oid_grupo_documento_padre,
                            par$oid_formulario_grupo      => par$oid_formulario_grupo,
                            par$oid_sector_origen         => par$oid_sector_origen,
                            par$oid_sector_destino        => par$oid_sector_destino,
                            par$usuario                   => par$usuario,
                            par$rowver                    => par$rowver,
                            par$adocs_oid                 => par$adocs_oid,
                            par$avtgdoc_oid_termino       => par$avtgdoc_oid_termino,
                            par$avtgdoc_des_valor         => par$avtgdoc_des_valor,
                            par$esnuevo                   => var$esnuevo,
                            par$inserts                   => var$inserts_int,
                            par$updates                   => var$updates_int,
                            par$deletes                   => var$deletes_int,
                            par$selects                   => var$selects_int,
                            par$cod_cultura               => par$cod_cultura);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$deletes_int + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$selects   := var$selects + var$selects_int;

    /* Grabar los documentos del grupo */
    sapr_pdocumento_###VERSION###.sguardar_doc_valores(par$oid_formulario       => par$oid_formulario_grupo,
                                                  par$oid_grupo_documento  => par$oid_grupo_documento,
                                                  par$adocs_oid            => par$adocs_oid,
                                                  par$adocs_oid_doc_padre  => par$adocs_oid_doc_padre,
                                                  par$adocs_oid_sustituto  => par$adocs_oid_sustituto,
                                                  par$adocs_oid_mov_fondos => par$adocs_oid_mov_fondos,
                                                  par$adocs_fyh_plncertif  => par$adocs_fyh_plncertif,
                                                  par$adocs_fyh_gestion    => par$adocs_fyh_gestion,
                                                  par$adocs_fyh_contable   => var$array_fecha_vacio,                                 
                                                  par$adocs_cod_actual_id  => var$array_desc_vacio,
                                                  par$adocs_collection_id  => var$array_desc_vacio,
                                                  par$adocs_cod_externo    => par$adocs_cod_externo,
                                                  par$adocs_rowver         => par$adocs_rowver,
                                                  /* cuenta origen */
                                                  par$adocs_mov_ori_oid_cuenta => par$adocs_mov_ori_oid_cuenta,
                                                  par$adocs_mov_ori_oid_client => par$adocs_mov_ori_oid_client,
                                                  par$adocs_mov_ori_cod_client => par$adocs_mov_ori_cod_client,
                                                  par$adocs_mov_ori_oid_subcli => par$adocs_mov_ori_oid_subcli,
                                                  par$adocs_mov_ori_cod_subcli => par$adocs_mov_ori_cod_subcli,
                                                  par$adocs_mov_ori_oid_ptserv => par$adocs_mov_ori_oid_ptserv,
                                                  par$adocs_mov_ori_cod_ptserv => par$adocs_mov_ori_cod_ptserv,
                                                  par$adocs_mov_ori_oid_canal  => par$adocs_mov_ori_oid_canal,
                                                  par$adocs_mov_ori_cod_canal  => par$adocs_mov_ori_cod_canal,
                                                  par$adocs_mov_ori_oid_subcan => par$adocs_mov_ori_oid_subcan,
                                                  par$adocs_mov_ori_cod_subcan => par$adocs_mov_ori_cod_subcan,
                                                  par$adocs_mov_ori_oid_delega => par$adocs_mov_ori_oid_delega,
                                                  par$adocs_mov_ori_cod_delega => par$adocs_mov_ori_cod_delega,
                                                  par$adocs_mov_ori_oid_planta => par$adocs_mov_ori_oid_planta,
                                                  par$adocs_mov_ori_cod_planta => par$adocs_mov_ori_cod_planta,
                                                  par$adocs_mov_ori_oid_sector => par$adocs_mov_ori_oid_sector,
                                                  par$adocs_mov_ori_cod_sector => par$adocs_mov_ori_cod_sector,
                                                  par$adocs_sal_ori_oid_cuenta => par$adocs_sal_ori_oid_cuenta,
                                                  par$adocs_sal_ori_oid_client => par$adocs_sal_ori_oid_client,
                                                  par$adocs_sal_ori_cod_client => par$adocs_sal_ori_cod_client,
                                                  par$adocs_sal_ori_oid_subcli => par$adocs_sal_ori_oid_subcli,
                                                  par$adocs_sal_ori_cod_subcli => par$adocs_sal_ori_cod_subcli,
                                                  par$adocs_sal_ori_oid_ptserv => par$adocs_sal_ori_oid_ptserv,
                                                  par$adocs_sal_ori_cod_ptserv => par$adocs_sal_ori_cod_ptserv,

                                                  /* cuenta destino */
                                                  par$adocs_mov_des_oid_cuenta => par$adocs_mov_des_oid_cuenta,
                                                  par$adocs_mov_des_oid_client => par$adocs_mov_des_oid_client,
                                                  par$adocs_mov_des_cod_client => par$adocs_mov_des_cod_client,
                                                  par$adocs_mov_des_oid_subcli => par$adocs_mov_des_oid_subcli,
                                                  par$adocs_mov_des_cod_subcli => par$adocs_mov_des_cod_subcli,
                                                  par$adocs_mov_des_oid_ptserv => par$adocs_mov_des_oid_ptserv,
                                                  par$adocs_mov_des_cod_ptserv => par$adocs_mov_des_cod_ptserv,
                                                  par$adocs_mov_des_oid_canal  => par$adocs_mov_des_oid_canal,
                                                  par$adocs_mov_des_cod_canal  => par$adocs_mov_des_cod_canal,
                                                  par$adocs_mov_des_oid_subcan => par$adocs_mov_des_oid_subcan,
                                                  par$adocs_mov_des_cod_subcan => par$adocs_mov_des_cod_subcan,
                                                  par$adocs_mov_des_oid_delega => par$adocs_mov_des_oid_delega,
                                                  par$adocs_mov_des_cod_delega => par$adocs_mov_des_cod_delega,
                                                  par$adocs_mov_des_oid_planta => par$adocs_mov_des_oid_planta,
                                                  par$adocs_mov_des_cod_planta => par$adocs_mov_des_cod_planta,
                                                  par$adocs_mov_des_oid_sector => par$adocs_mov_des_oid_sector,
                                                  par$adocs_mov_des_cod_sector => par$adocs_mov_des_cod_sector,
                                                  par$adocs_sal_des_oid_cuenta => par$adocs_sal_des_oid_cuenta,
                                                  par$adocs_sal_des_oid_client => par$adocs_sal_des_oid_client,
                                                  par$adocs_sal_des_cod_client => par$adocs_sal_des_cod_client,
                                                  par$adocs_sal_des_oid_subcli => par$adocs_sal_des_oid_subcli,
                                                  par$adocs_sal_des_cod_subcli => par$adocs_sal_des_cod_subcli,
                                                  par$adocs_sal_des_oid_ptserv => par$adocs_sal_des_oid_ptserv,
                                                  par$adocs_sal_des_cod_ptserv => par$adocs_sal_des_cod_ptserv,

                                                  par$aefdoc_oid_documento     => par$aefdoc_oid_documento,
                                                  par$aefdoc_oid_divisa        => par$aefdoc_oid_divisa,
                                                  par$aefdoc_oid_denominacion  => par$aefdoc_oid_denominacion,
                                                  par$aefdoc_oid_unid_medida   => par$aefdoc_oid_unid_medida,
                                                  par$aefdoc_cod_niv_detalle   => par$aefdoc_cod_niv_detalle,
                                                  par$aefdoc_cod_tp_efec_tot   => par$aefdoc_cod_tp_efec_tot,
                                                  par$aefdoc_oid_calidad       => par$aefdoc_oid_calidad,
                                                  par$aefdoc_num_importe       => par$aefdoc_num_importe,
                                                  par$aefdoc_nel_cantidad      => par$aefdoc_nel_cantidad,
                                                  par$ampdoc_oid_documento     => par$ampdoc_oid_documento,
                                                  par$ampdoc_oid_divisa        => par$ampdoc_oid_divisa,
                                                  par$ampdoc_oid_medio_pago    => par$ampdoc_oid_medio_pago,
                                                  par$ampdoc_cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago,
                                                  par$ampdoc_cod_nivel_detalle => par$ampdoc_cod_nivel_detalle,
                                                  par$ampdoc_num_importe       => par$ampdoc_num_importe,
                                                  par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,
                                                  par$avtmpdoc_oid_documento   => par$avtmpdoc_oid_documento,
                                                  par$avtmpdoc_oid_t_mediopago => par$avtmpdoc_oid_t_mediopago,
                                                  par$avtmpdoc_des_valor       => par$avtmpdoc_des_valor,
                                                  par$avtmpdoc_nec_indice_grp  => par$avtmpdoc_nec_indice_grp,

                                                  par$aefadoc_oid_documento     => par$aefadoc_oid_documento,
                                                  par$aefadoc_oid_divisa        => par$aefadoc_oid_divisa,
                                                  par$aefadoc_oid_denominacion  => par$aefadoc_oid_denominacion,
                                                  par$aefadoc_oid_unid_medida   => par$aefadoc_oid_unid_medida,
                                                  par$aefadoc_cod_niv_detalle   => par$aefadoc_cod_niv_detalle,
                                                  par$aefadoc_cod_tp_efec_tot   => par$aefadoc_cod_tp_efec_tot,
                                                  par$aefadoc_oid_calidad       => par$aefadoc_oid_calidad,
                                                  par$aefadoc_num_importe       => par$aefadoc_num_importe,
                                                  par$aefadoc_nel_cantidad      => par$aefadoc_nel_cantidad,
                                                  par$ampadoc_oid_documento     => par$ampadoc_oid_documento,
                                                  par$ampadoc_oid_divisa        => par$ampadoc_oid_divisa,
                                                  par$ampadoc_oid_medio_pago    => par$ampadoc_oid_medio_pago,
                                                  par$ampadoc_cod_tipo_med_pago => par$ampadoc_cod_tipo_med_pago,
                                                  par$ampadoc_cod_nivel_detalle => par$ampadoc_cod_nivel_detalle,
                                                  par$ampadoc_num_importe       => par$ampadoc_num_importe,
                                                  par$ampadoc_nel_cantidad      => par$ampadoc_nel_cantidad,
                                                  par$avtmpadoc_oid_documento   => par$avtmpadoc_oid_documento,
                                                  par$avtmpadoc_oid_t_mediopago => par$avtmpadoc_oid_t_mediopago,
                                                  par$avtmpadoc_des_valor       => par$avtmpadoc_des_valor,
                                                  par$avtmpadoc_nec_indice_grp  => par$avtmpadoc_nec_indice_grp,

                                                  par$avtdoc_oid_documento     => par$avtdoc_oid_documento,
                                                  par$avtdoc_oid_termino       => par$avtdoc_oid_termino,
                                                  par$avtdoc_des_valor         => par$avtdoc_des_valor,
                                                  par$usuario                  => par$usuario,
                                                  par$cod_cultura              => par$cod_cultura,
                                                  par$info_ejecucion           => par$info_ejecucion,
                                                  par$hacer_commit             => 0,
                                                  par$confirmar_doc            => 0,
                                                  par$doc_rc_documentos        => par$doc_rc_documentos,
                                                  par$doc_rc_historico_mov_doc => par$doc_rc_historico_mov_doc,
                                                  par$inserts                  => var$inserts_int,
                                                  par$updates                  => var$updates_int,
                                                  par$deletes                  => var$deletes_int,
                                                  par$selects                  => var$selects_int,
                                                  par$cod_ejecucion            => par$cod_ejecucion);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$deletes_int + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$selects   := var$selects + var$selects_int;

    IF par$confirmar_doc > 0 THEN

      /* limpiar tabla temporaria */
      delete sapr_gtt_toids;
      var$deletes   := var$deletes + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;

      var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
      var$cod_comprobante      := '';
      var$rowver               := par$rowver;

      sapr_ptransiciones_###VERSION###.sejecutar_grp_doc(par$oid_grupo_documento  => par$oid_grupo_documento,
                                                    par$cod_estado_documento => var$cod_estado_documento,
                                                    par$cod_comprobante      => var$cod_comprobante,
                                                    par$rowver               => var$rowver,
                                                    par$usuario              => par$usuario,
                                                    par$cod_cultura          => par$cod_cultura,
                                                    par$info_ejecucion       => par$info_ejecucion,
                                                    par$cod_ejecucion        => par$cod_ejecucion,
                                                    par$hacer_commit         => 0,
                                                    par$inserts              => var$inserts_int,
                                                    par$updates              => var$updates_int,
                                                    par$deletes              => var$deletes_int,
                                                    par$selects              => var$selects_int);

      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$deletes_int + var$selects_int;
      var$inserts   := var$inserts + var$inserts_int;
      var$updates   := var$updates + var$updates_int;
      var$deletes   := var$deletes + var$deletes_int;
      var$selects   := var$selects + var$selects_int;

      /* - Documentos y Documentos Padres */
      sapr_pdocumento_###VERSION###.scargar_tt_oids_documento_gpr(par$oid_grupo_documento => par$oid_grupo_documento,
                                                             par$recuperar_padres    => true,
                                                             par$inserts             => var$inserts_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$registros := var$registros + var$inserts_int;

      /* - TABLA: Grupo de Documentos */
      par$rc_grupo_documentos := fcargar_rc_grupo_documento(par$oid_grupo_documento => par$oid_grupo_documento,
                                                            par$selects             => var$selects_int);
      var$selects             := var$selects + var$selects_int;
      var$registros           := var$registros + var$selects_int;

      /* - TABLA: Historico Grupo */
      par$rc_historico_mov_grupo := fcargar_rc_historico(par$oid_grupo_documento,
                                                         par$selects => var$selects_int);
      var$selects                := var$selects + var$selects_int;
      var$registros              := var$registros + var$selects_int;

      /* - TABLA: Documentos */
      par$doc_rc_documentos := sapr_pdocumento_###VERSION###.fcargar_rc_documento;
      var$selects           := var$selects + SQL % ROWCOUNT;
      var$registros         := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := sapr_pdocumento_###VERSION###.fcargar_rc_historico;
      var$selects                  := var$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;

    END IF;

    IF par$hacer_commit > 0 THEN
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
                                                           par$usuario,
                                                           par$cod_ejecucion);
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
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END sguardar_grp_docs_valores;

  /* Validar grupo - Gestion de Fondos y Cierre */
  PROCEDURE svalidar_grp_docs_valores(par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_formulario_grupo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_sector_origen    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$rowver               IN gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_grp_docs_valores';
  BEGIN
    IF par$rowver IS NULL THEN
      /* voy validar si existe un cierre de caja en el sector de origen
      Esta validacion hace sentido en un documento nuevo */
      IF fhay_cierrecaja_encurso(par$oid_sector_origen,
                                 par$oid_grupo_documento,
                                 par$oid_formulario_grupo) THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'hay_cierre_caja_encurso',
                                                                    const$codFuncionalidad || '.' ||
                                                                    const$nombre_func,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      END IF;
    END IF;
  END svalidar_grp_docs_valores;

  /* Guardar grupo documento de Actas - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_grp_docs_elem_actas(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$bol_gestion_bulto         IN gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$cod_integracion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                         par$oid_sector                IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,

                                         par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtgdoc_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                         /* arrays asociativos */
                                         /* arrays de documentos */
                                         par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adocs_cod_externo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_rowver         IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de efectivo por documento */
                                         par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                         par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de medio de pago por documento */
                                         par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                         par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de terminos de medio de pago por documento */
                                         par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                         par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                         /* arrays de terminos por documento */
                                         par$avtdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtdoc_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtdoc_cod_termino   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$avtdoc_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,

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

                                         par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                         par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion           IN VARCHAR2,
                                         par$hacer_commit             IN INTEGER,
                                         par$confirmar_doc            IN INTEGER,
                                         par$rc_grupo_documentos      OUT sys_refcursor,
                                         par$rc_historico_mov_grupo   OUT sys_refcursor,
                                         par$doc_rc_documentos        OUT sys_refcursor,
                                         par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                         par$ele_rc_elementos         OUT sys_refcursor,
                                         par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_docs_elem_actas';
    /* variables locales */
    var$esnuevo              BOOLEAN;
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante      gepr_pcomon_###VERSION###.tipo$cod_;

    /* verificar formulario Altas */
    var$oid_formulario_grupo gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_gestion_bulto    gepr_pcomon_###VERSION###.tipo$oid_;
    var$avtgdoc_oid_termino  gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_oid_termino   gepr_pcomon_###VERSION###.tipo$oids_;

    var$inicio      gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err         VARCHAR2(4000);
    var$rowver      gepr_pcomon_###VERSION###.tipo$nel_;
    var$array_fecha_vacio    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$array_desc_vacio      gepr_pcomon_###VERSION###.tipo$descs_;

    /* CURSOR DE TERMINOS FORMULARIO */
    CURSOR cur$terminos(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TI.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TFORMULARIO F
       INNER JOIN GEPR_TTERMINO_POR_IAC TI
          ON TI.OID_IAC = F.OID_IAC_INDIVIDUAL
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       WHERE F.OID_FORMULARIO = par$oid_formulario;

    /* CURSOR DE TERMINOS FORMULARIO GRUPO */
    CURSOR cur$terminosgrupo(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TI.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TFORMULARIO F
       INNER JOIN GEPR_TTERMINO_POR_IAC TI
          ON TI.OID_IAC = F.Oid_Iac_Grupo
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       WHERE F.OID_FORMULARIO = par$oid_formulario;

  BEGIN

    /* Valores iniciales */
    var$inicio := SYSTIMESTAMP;

    /* Informaciones formulario */
    var$bol_gestion_bulto    := par$bol_gestion_bulto;
    var$oid_formulario_grupo := par$oid_formulario_grupo;

    sapr_pformulario_###VERSION###.srecuperar_formulario_altas(par$oid_formulario    => var$oid_formulario_grupo,
                                                          par$bol_gestion_bulto => var$bol_gestion_bulto,
                                                          par$cod_integracion   => par$cod_integracion,
                                                          par$cod_cultura       => par$cod_cultura);

    /* Informaciones formulario - termino iac */
    IF par$avtdoc_oid_termino.COUNT > 0 THEN
      IF par$avtdoc_oid_termino(1) IS NULL THEN
        FOR rec$termino IN cur$terminos(var$oid_formulario_grupo) LOOP
          /* para todos os terminos do altas */
          FOR idx IN par$avtdoc_oid_documento.first .. par$avtdoc_oid_documento.last LOOP
            IF rec$termino.cod_termino = par$avtdoc_cod_termino(idx) THEN
              var$avtdoc_oid_termino(idx) := rec$termino.oid_termino;
            END IF;
          END LOOP;
        END LOOP rec$termino;
      END IF;
      IF var$avtdoc_oid_termino.COUNT = 0 THEN
        var$avtdoc_oid_termino := par$avtdoc_oid_termino;
      END IF;
    END IF;
    IF par$avtgdoc_oid_termino.COUNT > 0 THEN
      IF par$avtgdoc_oid_termino(1) IS NULL THEN
        FOR rec$termino IN cur$terminosgrupo(var$oid_formulario_grupo) LOOP
          /* para todos os terminos do altas */
          FOR idx IN par$avtgdoc_cod_termino.first .. par$avtgdoc_cod_termino.last LOOP
            IF rec$termino.cod_termino = par$avtgdoc_cod_termino(idx) THEN
              var$avtgdoc_oid_termino(idx) := rec$termino.oid_termino;
            END IF;
          END LOOP;
        END LOOP rec$termino;
      END IF;
      IF var$avtgdoc_oid_termino.COUNT = 0 THEN
        var$avtgdoc_oid_termino := par$avtgdoc_oid_termino;
      END IF;
    END IF;

    /* Grabar las informaciones del grupo */
    sguardar_grp_documentos(par$oid_grupo_documento       => par$oid_grupo_documento,
                            par$oid_grupo_documento_padre => par$oid_grupo_documento_padre,
                            par$oid_formulario_grupo      => par$oid_formulario_grupo,
                            par$oid_sector_origen         => par$oid_sector,
                            par$oid_sector_destino        => par$oid_sector,
                            par$usuario                   => par$usuario,
                            par$rowver                    => par$rowver,
                            par$adocs_oid                 => par$adocs_oid,
                            par$avtgdoc_oid_termino       => var$avtgdoc_oid_termino,
                            par$avtgdoc_des_valor         => par$avtgdoc_des_valor,
                            par$esnuevo                   => var$esnuevo,
                            par$inserts                   => var$inserts_int,
                            par$updates                   => var$updates_int,
                            par$deletes                   => var$deletes_int,
                            par$selects                   => var$selects_int,
                            par$cod_cultura               => par$cod_cultura);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$deletes_int + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$selects   := var$selects + var$selects_int;

    /* Grabar los documentos del grupo */
    sapr_pdocumento_###VERSION###.sguardar_doc_elem_actas(par$oid_grupo_documento        => par$oid_grupo_documento,
                                                     par$oid_formulario             => var$oid_formulario_grupo,
                                                     par$bol_gestion_bulto          => var$bol_gestion_bulto,
                                                     par$cod_integracion            => par$cod_integracion,
                                                     par$adocs_oid                  => par$adocs_oid,
                                                     par$adocs_oid_doc_padre        => par$adocs_oid_doc_padre,
                                                     par$adocs_oid_sustituto        => par$adocs_oid_sustituto,
                                                     par$adocs_oid_mov_fondos       => par$adocs_oid_mov_fondos,
                                                     par$adocs_fyh_plncertif        => par$adocs_fyh_plncertif,
                                                     par$adocs_fyh_gestion          => par$adocs_fyh_gestion,
                                                     par$adocs_fyh_contable         => var$array_fecha_vacio,                                 
                                                     par$adocs_cod_actual_id        => var$array_desc_vacio,
                                                     par$adocs_collection_id        => var$array_desc_vacio,
                                                     par$adocs_cod_externo          => par$adocs_cod_externo,
                                                     par$adocs_rowver               => par$adocs_rowver,
                                                     par$aefdoc_oid_documento       => par$aefdoc_oid_documento,
                                                     par$aefdoc_oid_divisa          => par$aefdoc_oid_divisa,
                                                     par$aefdoc_oid_denominacion    => par$aefdoc_oid_denominacion,
                                                     par$aefdoc_oid_unid_medida     => par$aefdoc_oid_unid_medida,
                                                     par$aefdoc_cod_niv_detalle     => par$aefdoc_cod_niv_detalle,
                                                     par$aefdoc_cod_tp_efec_tot     => par$aefdoc_cod_tp_efec_tot,
                                                     par$aefdoc_oid_calidad         => par$aefdoc_oid_calidad,
                                                     par$aefdoc_num_importe         => par$aefdoc_num_importe,
                                                     par$aefdoc_nel_cantidad        => par$aefdoc_nel_cantidad,
                                                     par$ampdoc_oid_documento       => par$ampdoc_oid_documento,
                                                     par$ampdoc_oid_divisa          => par$ampdoc_oid_divisa,
                                                     par$ampdoc_oid_medio_pago      => par$ampdoc_oid_medio_pago,
                                                     par$ampdoc_cod_tipo_med_pago   => par$ampdoc_cod_tipo_med_pago,
                                                     par$ampdoc_cod_nivel_detalle   => par$ampdoc_cod_nivel_detalle,
                                                     par$ampdoc_num_importe         => par$ampdoc_num_importe,
                                                     par$ampdoc_nel_cantidad        => par$ampdoc_nel_cantidad,
                                                     par$avtmpdoc_oid_documento     => par$avtmpdoc_oid_documento,
                                                     par$avtmpdoc_oid_t_mediopago   => par$avtmpdoc_oid_t_mediopago,
                                                     par$avtmpdoc_des_valor         => par$avtmpdoc_des_valor,
                                                     par$avtmpdoc_nec_indice_grp    => par$avtmpdoc_nec_indice_grp,
                                                     par$avtdoc_oid_documento       => par$avtdoc_oid_documento,
                                                     par$avtdoc_oid_termino         => var$avtdoc_oid_termino,
                                                     par$avtdoc_cod_termino         => par$avtdoc_cod_termino,
                                                     par$avtdoc_des_valor           => par$avtdoc_des_valor,
                                                     par$aremdoc_oid_remesa         => par$aremdoc_oid_remesa,
                                                     par$aremdoc_oid_documento      => par$aremdoc_oid_documento,
                                                     par$avtrem_oid_remesa          => par$avtrem_oid_remesa,
                                                     par$avtrem_oid_termino         => par$avtrem_oid_termino,
                                                     par$avtrem_cod_termino         => par$avtrem_cod_termino,
                                                     par$avtrem_des_valor           => par$avtrem_des_valor,
                                                     par$avtrem_obligatorio         => par$avtrem_obligatorio,
                                                     par$abuldoc_oid_bulto          => par$abuldoc_oid_bulto,
                                                     par$abuldoc_oid_remesa         => par$abuldoc_oid_remesa,
                                                     par$abuldoc_oid_documento      => par$abuldoc_oid_documento,
                                                     par$abuldoc_oid_externo        => par$abuldoc_oid_externo,
                                                     par$abuldoc_oid_iac            => par$abuldoc_oid_iac,
                                                     par$abuldoc_oid_iac_parciales  => par$abuldoc_oid_iac_parciales,
                                                     par$abuldoc_cod_precinto       => par$abuldoc_cod_precinto,
                                                     par$abuldoc_cod_bolsa          => par$abuldoc_cod_bolsa,
                                                     par$abuldoc_oid_banco_ingreso  => par$abuldoc_oid_banco_ingreso,
                                                     par$abuldoc_bol_banco_ing_mad  => par$abuldoc_bol_banco_ing_mad,
                                                     par$abuldoc_usuario_resp       => par$abuldoc_usuario_resp,
                                                     par$abuldoc_puesto_resp        => par$abuldoc_puesto_resp,
                                                     par$abuldoc_nel_cant_parciales => par$abuldoc_nel_cant_parciales,
                                                     par$abuldoc_fyh_conteo_inicio  => par$abuldoc_fyh_conteo_inicio,
                                                     par$abuldoc_fyh_conteo_fin     => par$abuldoc_fyh_conteo_fin,
                                                     par$abuldoc_fyh_proceso_leg    => par$abuldoc_fyh_proceso_leg,
                                                     par$abuldoc_oid_bulto_padre    => par$abuldoc_oid_bulto_padre,
                                                     par$abuldoc_oid_bulto_sub      => par$abuldoc_oid_bulto_sub,
                                                     par$abuldoc_cod_nivel_detalle  => par$abuldoc_cod_nivel_detalle,
                                                     par$abuldoc_bol_cuadrado       => par$abuldoc_bol_cuadrado,
                                                     par$abuldoc_oid_tipo_formato   => par$abuldoc_oid_tipo_formato,
                                                     par$abuldoc_oid_tipo_servicio  => par$abuldoc_oid_tipo_servicio,
                                                     par$avtbul_oid_bulto           => par$avtbul_oid_bulto,
                                                     par$avtbul_oid_termino         => par$avtbul_oid_termino,
                                                     par$avtbul_cod_termino         => par$avtbul_cod_termino,
                                                     par$avtbul_des_valor           => par$avtbul_des_valor,
                                                     par$avtbul_obligatorio         => par$avtbul_obligatorio,
                                                     par$apardoc_oid_remesa         => par$apardoc_oid_remesa,
                                                     par$apardoc_oid_bulto          => par$apardoc_oid_bulto,
                                                     par$apardoc_oid_parcial        => par$apardoc_oid_parcial,
                                                     par$apardoc_oid_externo        => par$apardoc_oid_externo,
                                                     par$apardoc_cod_precinto       => par$apardoc_cod_precinto,
                                                     par$apardoc_oid_iac            => par$apardoc_oid_iac,
                                                     par$apardoc_usuario_resp       => par$apardoc_usuario_resp,
                                                     par$apardoc_puesto_resp        => par$apardoc_puesto_resp,
                                                     par$apardoc_nec_secuencia      => par$apardoc_nec_secuencia,
                                                     par$apardoc_oid_parcial_padre  => par$apardoc_oid_parcial_padre,
                                                     par$apardoc_oid_parcial_sub    => par$apardoc_oid_parcial_sub,
                                                     par$apardoc_oid_tipo_formato   => par$apardoc_oid_tipo_formato,
                                                     par$avtpar_oid_parcial         => par$avtpar_oid_parcial,
                                                     par$avtpar_oid_termino         => par$avtpar_oid_termino,
                                                     par$avtpar_cod_termino         => par$avtpar_cod_termino,
                                                     par$avtpar_des_valor           => par$avtpar_des_valor,
                                                     par$avtpar_obligatorio         => par$avtpar_obligatorio,
                                                     par$aelemvalefe_tipo           => par$aelemvalefe_tipo,
                                                     par$aelemvalefe_oid_remesa     => par$aelemvalefe_oid_remesa,
                                                     par$aelemvalefe_oid_bulto      => par$aelemvalefe_oid_bulto,
                                                     par$aelemvalefe_oid_parcial    => par$aelemvalefe_oid_parcial,
                                                     par$aelemvalefe_oid_divisa     => par$aelemvalefe_oid_divisa,
                                                     par$aelemvalefe_oid_denom      => par$aelemvalefe_oid_denom,
                                                     par$aelemvalefe_bol_esbillete  => par$aelemvalefe_bol_esbillete,
                                                     par$aelemvalefe_oid_unid_med   => par$aelemvalefe_oid_unid_med,
                                                     par$aelemvalefe_cod_tipo_efec  => par$aelemvalefe_cod_tipo_efec,
                                                     par$aelemvalefe_num_importe    => par$aelemvalefe_num_importe,
                                                     par$aelemvalefe_nel_cantidad   => par$aelemvalefe_nel_cantidad,
                                                     par$aelemvalefe_cod_nvdetalle  => par$aelemvalefe_cod_nvdetalle,
                                                     par$aelemvalefe_bol_ingresado  => par$aelemvalefe_bol_ingresado,
                                                     par$aelemvalefe_oid_calidad    => par$aelemvalefe_oid_calidad,
                                                     par$aelemval_mp_tipo           => par$aelemval_mp_tipo,
                                                     par$aelemval_mp_oid_valor      => par$aelemval_mp_oid_valor,
                                                     par$aelemval_mp_oid_remesa     => par$aelemval_mp_oid_remesa,
                                                     par$aelemval_mp_oid_bulto      => par$aelemval_mp_oid_bulto,
                                                     par$aelemval_mp_oid_parcial    => par$aelemval_mp_oid_parcial,
                                                     par$aelemval_mp_oid_divisa     => par$aelemval_mp_oid_divisa,
                                                     par$aelemval_mp_oid_mediopago  => par$aelemval_mp_oid_mediopago,
                                                     par$aelemval_mp_cod_tipo_mp    => par$aelemval_mp_cod_tipo_mp,
                                                     par$aelemval_mp_num_importe    => par$aelemval_mp_num_importe,
                                                     par$aelemval_mp_nel_cantidad   => par$aelemval_mp_nel_cantidad,
                                                     par$aelemval_mp_cod_nvdetalle  => par$aelemval_mp_cod_nvdetalle,
                                                     par$aelemval_mp_bol_ingresado  => par$aelemval_mp_bol_ingresado,
                                                     par$aelemval_mp_nel_secuencia  => par$aelemval_mp_nel_secuencia,
                                                     par$aelemter_mp_tipo           => par$aelemter_mp_tipo,
                                                     par$aelemter_mp_oid_valor      => par$aelemter_mp_oid_valor,
                                                     par$aelemter_mp_oid_termino    => par$aelemter_mp_oid_termino,
                                                     par$aelemter_mp_des_valor      => par$aelemter_mp_des_valor,
                                                     par$aelemter_mp_nel_ind_grupo  => par$aelemter_mp_nel_ind_grupo,
                                                     par$usuario                    => par$usuario,
                                                     par$cod_cultura                => par$cod_cultura,
                                                     par$info_ejecucion             => par$info_ejecucion,
                                                     par$hacer_commit               => 0,
                                                     par$confirmar_doc              => 0,
                                                     par$doc_rc_documentos          => par$doc_rc_documentos,
                                                     par$doc_rc_historico_mov_doc   => par$doc_rc_historico_mov_doc,
                                                     par$ele_rc_elementos           => par$ele_rc_elementos,
                                                     par$inserts                    => var$inserts_int,
                                                     par$updates                    => var$updates_int,
                                                     par$deletes                    => var$deletes_int,
                                                     par$selects                    => var$selects_int,
                                                     par$cod_ejecucion              => par$cod_ejecucion);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$deletes_int + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$selects   := var$selects + var$selects_int;

    IF par$confirmar_doc > 0 THEN

      /* limpiar tabla temporaria */
      delete sapr_gtt_toids;
      var$deletes   := var$deletes + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;

      var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
      var$cod_comprobante      := '';
      var$rowver               := par$rowver;

      sapr_ptransiciones_###VERSION###.sejecutar_grp_doc(par$oid_grupo_documento  => par$oid_grupo_documento,
                                                    par$cod_estado_documento => var$cod_estado_documento,
                                                    par$cod_comprobante      => var$cod_comprobante,
                                                    par$rowver               => var$rowver,
                                                    par$usuario              => par$usuario,
                                                    par$cod_cultura          => par$cod_cultura,
                                                    par$info_ejecucion       => par$info_ejecucion,
                                                    par$cod_ejecucion        => par$cod_ejecucion,
                                                    par$hacer_commit         => 0,
                                                    par$inserts              => var$inserts_int,
                                                    par$updates              => var$updates_int,
                                                    par$deletes              => var$deletes_int,
                                                    par$selects              => var$selects_int);

      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$deletes_int + var$selects_int;
      var$inserts   := var$inserts + var$inserts_int;
      var$updates   := var$updates + var$updates_int;
      var$deletes   := var$deletes + var$deletes_int;
      var$selects   := var$selects + var$selects_int;

      /* - Documentos y Documentos Padres */
      sapr_pdocumento_###VERSION###.scargar_tt_oids_documento_gpr(par$oid_grupo_documento => par$oid_grupo_documento,
                                                             par$recuperar_padres    => true,
                                                             par$inserts             => var$inserts_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$registros := var$registros + var$inserts_int;

      /* - TABLA: Grupo de Documentos */
      par$rc_grupo_documentos := fcargar_rc_grupo_documento(par$oid_grupo_documento,
                                                            par$selects => var$selects_int);
      var$selects             := var$selects + var$selects_int;
      var$registros           := var$registros + var$selects_int;

      /* - TABLA: Historico Grupo */
      par$rc_historico_mov_grupo := fcargar_rc_historico(par$oid_grupo_documento,
                                                         par$selects => var$selects_int);
      var$selects                := var$selects + var$selects_int;
      var$registros              := var$registros + var$selects_int;

      /* - TABLA: Documentos */
      par$doc_rc_documentos := sapr_pdocumento_###VERSION###.fcargar_rc_documento;
      var$selects           := var$selects + SQL % ROWCOUNT;
      var$registros         := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := sapr_pdocumento_###VERSION###.fcargar_rc_historico;
      var$selects                  := var$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
      par$ele_rc_elementos := sapr_premesa_###VERSION###.fcargar_rc_elementos;
      var$selects          := var$selects + SQL % ROWCOUNT;
      var$registros        := var$registros + SQL % ROWCOUNT;

    END IF;

    IF par$hacer_commit > 0 THEN
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
                                                           par$usuario,
                                                           par$cod_ejecucion);
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
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END sguardar_grp_docs_elem_actas;

  /* Guardar documento de Altas - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_grp_docs_elem_altas(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$bol_gestion_bulto         IN gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$cod_integracion           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                         par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,

                                         /* arrays asociativos */
                                         /* arrays de terminos del grupo de documentos */
                                         par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtgdoc_cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                         /* arrays de documentos */
                                         par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adocs_cod_externo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_rowver         IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* cuenta origen */
                                         par$adocs_mov_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_ori_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_ori_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,

                                         /* cuenta destino */
                                         par$adocs_mov_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_mov_des_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_mov_des_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adocs_sal_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adocs_sal_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,

                                         /* arrays de efectivo por documento */
                                         par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                         par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de medio de pago por documento */
                                         par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                         par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         /* arrays de terminos de medio de pago por documento */
                                         par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                         par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                         /* arrays de terminos por documento */
                                         par$avtdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtdoc_oid_termino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$avtdoc_cod_termino   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$avtdoc_des_valor     IN gepr_pcomon_###VERSION###.tipo$descs_,

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
                                         par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                         par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion           IN VARCHAR2,
                                         par$hacer_commit             IN INTEGER,
                                         par$confirmar_doc            IN INTEGER,
                                         par$rc_grupo_documentos      OUT sys_refcursor,
                                         par$rc_historico_mov_grupo   OUT sys_refcursor,
                                         par$doc_rc_documentos        OUT sys_refcursor,
                                         par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                         par$ele_rc_elementos         OUT sys_refcursor,
                                         par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_docs_elem_altas';

    /* variables locales */
    var$esnuevo              BOOLEAN;
    var$oid_grupo_retorno    gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante      gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver               gepr_pcomon_###VERSION###.tipo$nel_;

    /* verificar formulario Altas */
    var$oid_formulario_grupo gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_gestion_bulto    gepr_pcomon_###VERSION###.tipo$oid_;
    var$avtgdoc_oid_termino  gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_oid_termino   gepr_pcomon_###VERSION###.tipo$oids_;

    /* verificar informaciones para reenvio automatico */
    var$esreenvio              BOOLEAN;
    var$r_oid_formulario_grupo gepr_pcomon_###VERSION###.tipo$oid_;
    var$r_oid_grupo_documento  gepr_pcomon_###VERSION###.tipo$oid_;
    var$r_adocs_oid            gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_adocs_oid_sustituto  gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_adocs_oid_mov_fondo  gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtgdoc_oid_termino  gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtgdoc_des_valor    gepr_pcomon_###VERSION###.tipo$descs_;
    var$r_aefdoc_oid_doc       gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_ampdoc_oid_doc       gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtmpdoc_oid_doc     gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtdoc_oid_doc       gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtdoc_oid_termino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtdoc_des_valor     gepr_pcomon_###VERSION###.tipo$descs_;
    var$r_aremdoc_oid_doc      gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_abuldoc_oid_doc      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aremdoc_rowver         gepr_pcomon_###VERSION###.tipo$nels_;
    var$abuldoc_rowver         gepr_pcomon_###VERSION###.tipo$nels_;
    var$idx_rowver             gepr_pcomon_###VERSION###.tipo$nel_ := 0;

    /* LOG */
    var$inicio      gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err         VARCHAR2(4000);
    var$array_fecha_vacio    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$array_desc_vacio      gepr_pcomon_###VERSION###.tipo$descs_;

    /* CURSOR DE TERMINOS FORMULARIO */
    CURSOR cur$terminos(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TI.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TFORMULARIO F
       INNER JOIN GEPR_TTERMINO_POR_IAC TI
          ON TI.OID_IAC = F.OID_IAC_INDIVIDUAL
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       WHERE F.OID_FORMULARIO = par$oid_formulario;

    /* CURSOR DE TERMINOS FORMULARIO GRUPO */
    CURSOR cur$terminosgrupo(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TI.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TFORMULARIO F
       INNER JOIN GEPR_TTERMINO_POR_IAC TI
          ON TI.OID_IAC = F.Oid_Iac_Grupo
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       WHERE F.OID_FORMULARIO = par$oid_formulario;

  BEGIN

    /* Valores iniciales */
    var$inicio := SYSTIMESTAMP;

    /* Informaciones formulario */
    var$bol_gestion_bulto    := par$bol_gestion_bulto;
    var$oid_formulario_grupo := par$oid_formulario_grupo;

    sapr_pformulario_###VERSION###.srecuperar_formulario_altas(par$oid_formulario    => var$oid_formulario_grupo,
                                                          par$bol_gestion_bulto => var$bol_gestion_bulto,
                                                          par$cod_integracion   => par$cod_integracion,
                                                          par$cod_cultura       => par$cod_cultura);

    IF par$oid_sector_origen <> par$oid_sector_destino THEN
      var$esreenvio := true;
    END IF;

    /* Informaciones formulario - termino iac */
    IF par$avtdoc_oid_termino.COUNT > 0 THEN
      IF par$avtdoc_oid_termino(1) IS NULL THEN
        FOR rec$termino IN cur$terminos(var$oid_formulario_grupo) LOOP
          /* para todos os terminos do altas */
          FOR idx IN par$avtdoc_oid_documento.first .. par$avtdoc_oid_documento.last LOOP
            IF rec$termino.cod_termino = par$avtdoc_cod_termino(idx) THEN
              var$avtdoc_oid_termino(idx) := rec$termino.oid_termino;
            END IF;
          END LOOP;
        END LOOP rec$termino;
      END IF;
      IF var$avtdoc_oid_termino.COUNT = 0 THEN
        var$avtdoc_oid_termino := par$avtdoc_oid_termino;
      END IF;
    END IF;
    IF par$avtgdoc_oid_termino.COUNT > 0 THEN
      IF par$avtgdoc_oid_termino(1) IS NULL THEN
        FOR rec$termino IN cur$terminosgrupo(var$oid_formulario_grupo) LOOP
          /* para todos os terminos do altas */
          FOR idx IN par$avtgdoc_cod_termino.first .. par$avtgdoc_cod_termino.last LOOP
            IF rec$termino.cod_termino = par$avtgdoc_cod_termino(idx) THEN
              var$avtgdoc_oid_termino(idx) := rec$termino.oid_termino;
            END IF;
          END LOOP;
        END LOOP rec$termino;
      END IF;
      IF var$avtgdoc_oid_termino.COUNT = 0 THEN
        var$avtgdoc_oid_termino := par$avtgdoc_oid_termino;
      END IF;
    END IF;

    /*atribui valor nulo para gmt da remesa e bulto*/
    IF par$adocs_oid.COUNT > 0 THEN
      var$idx_rowver := 0;
      for idx in par$adocs_oid.first .. par$adocs_oid.last loop
        var$idx_rowver := var$idx_rowver + 1;
        var$aremdoc_rowver(var$idx_rowver) := null;
        var$abuldoc_rowver(var$idx_rowver) := null;
      end loop;
    END IF;

    /* Grabar las informaciones del grupo */
    sguardar_grp_documentos(par$oid_grupo_documento       => par$oid_grupo_documento,
                            par$oid_grupo_documento_padre => par$oid_grupo_documento_padre,
                            par$oid_formulario_grupo      => var$oid_formulario_grupo,
                            par$oid_sector_origen         => par$oid_sector_origen,
                            par$oid_sector_destino        => par$oid_sector_origen,
                            par$usuario                   => par$usuario,
                            par$rowver                    => par$rowver,
                            par$adocs_oid                 => par$adocs_oid,
                            par$avtgdoc_oid_termino       => var$avtgdoc_oid_termino,
                            par$avtgdoc_des_valor         => par$avtgdoc_des_valor,
                            par$esnuevo                   => var$esnuevo,
                            par$inserts                   => var$inserts_int,
                            par$updates                   => var$updates_int,
                            par$deletes                   => var$deletes_int,
                            par$selects                   => var$selects_int,
                            par$cod_cultura               => par$cod_cultura);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$deletes_int + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$selects   := var$selects + var$selects_int;

    /* Grabar los documentos del grupo */
    sapr_pdocumento_###VERSION###.sguardar_doc_elem_altas(par$oid_grupo_documento  => par$oid_grupo_documento,
                                                     par$oid_formulario       => var$oid_formulario_grupo,
                                                     par$bol_gestion_bulto    => var$bol_gestion_bulto,
                                                     par$cod_integracion      => par$cod_integracion,
                                                     par$adocs_oid            => par$adocs_oid,
                                                     par$adocs_oid_doc_padre  => par$adocs_oid_doc_padre,
                                                     par$adocs_oid_sustituto  => par$adocs_oid_sustituto,
                                                     par$adocs_oid_mov_fondos => par$adocs_oid_mov_fondos,
                                                     par$adocs_fyh_plncertif  => par$adocs_fyh_plncertif,
                                                     par$adocs_fyh_gestion    => par$adocs_fyh_gestion,
                                                     par$adocs_fyh_contable   => var$array_fecha_vacio,                                 
                                                     par$adocs_cod_actual_id  => var$array_desc_vacio,
                                                     par$adocs_collection_id  => var$array_desc_vacio,
                                                     par$adocs_cod_externo    => par$adocs_cod_externo,
                                                     par$adocs_rowver         => par$adocs_rowver,

                                                     /* cuenta origen */
                                                     par$adocs_mov_ori_oid_cuenta => par$adocs_mov_ori_oid_cuenta,
                                                     par$adocs_mov_ori_oid_client => par$adocs_mov_ori_oid_client,
                                                     par$adocs_mov_ori_cod_client => par$adocs_mov_ori_cod_client,
                                                     par$adocs_mov_ori_oid_subcli => par$adocs_mov_ori_oid_subcli,
                                                     par$adocs_mov_ori_cod_subcli => par$adocs_mov_ori_cod_subcli,
                                                     par$adocs_mov_ori_oid_ptserv => par$adocs_mov_ori_oid_ptserv,
                                                     par$adocs_mov_ori_cod_ptserv => par$adocs_mov_ori_cod_ptserv,
                                                     par$adocs_mov_ori_oid_canal  => par$adocs_mov_ori_oid_canal,
                                                     par$adocs_mov_ori_cod_canal  => par$adocs_mov_ori_cod_canal,
                                                     par$adocs_mov_ori_oid_subcan => par$adocs_mov_ori_oid_subcan,
                                                     par$adocs_mov_ori_cod_subcan => par$adocs_mov_ori_cod_subcan,
                                                     par$adocs_mov_ori_oid_delega => par$adocs_mov_ori_oid_delega,
                                                     par$adocs_mov_ori_cod_delega => par$adocs_mov_ori_cod_delega,
                                                     par$adocs_mov_ori_oid_planta => par$adocs_mov_ori_oid_planta,
                                                     par$adocs_mov_ori_cod_planta => par$adocs_mov_ori_cod_planta,
                                                     par$adocs_mov_ori_oid_sector => par$adocs_mov_ori_oid_sector,
                                                     par$adocs_mov_ori_cod_sector => par$adocs_mov_ori_cod_sector,
                                                     par$adocs_sal_ori_oid_cuenta => par$adocs_sal_ori_oid_cuenta,
                                                     par$adocs_sal_ori_oid_client => par$adocs_sal_ori_oid_client,
                                                     par$adocs_sal_ori_cod_client => par$adocs_sal_ori_cod_client,
                                                     par$adocs_sal_ori_oid_subcli => par$adocs_sal_ori_oid_subcli,
                                                     par$adocs_sal_ori_cod_subcli => par$adocs_sal_ori_cod_subcli,
                                                     par$adocs_sal_ori_oid_ptserv => par$adocs_sal_ori_oid_ptserv,
                                                     par$adocs_sal_ori_cod_ptserv => par$adocs_sal_ori_cod_ptserv,

                                                     /* cuenta destino */
                                                     par$adocs_mov_des_oid_cuenta => par$adocs_mov_des_oid_cuenta,
                                                     par$adocs_mov_des_oid_client => par$adocs_mov_des_oid_client,
                                                     par$adocs_mov_des_cod_client => par$adocs_mov_des_cod_client,
                                                     par$adocs_mov_des_oid_subcli => par$adocs_mov_des_oid_subcli,
                                                     par$adocs_mov_des_cod_subcli => par$adocs_mov_des_cod_subcli,
                                                     par$adocs_mov_des_oid_ptserv => par$adocs_mov_des_oid_ptserv,
                                                     par$adocs_mov_des_cod_ptserv => par$adocs_mov_des_cod_ptserv,
                                                     par$adocs_mov_des_oid_canal  => par$adocs_mov_des_oid_canal,
                                                     par$adocs_mov_des_cod_canal  => par$adocs_mov_des_cod_canal,
                                                     par$adocs_mov_des_oid_subcan => par$adocs_mov_des_oid_subcan,
                                                     par$adocs_mov_des_cod_subcan => par$adocs_mov_des_cod_subcan,
                                                     par$adocs_mov_des_oid_delega => par$adocs_mov_des_oid_delega,
                                                     par$adocs_mov_des_cod_delega => par$adocs_mov_des_cod_delega,
                                                     par$adocs_mov_des_oid_planta => par$adocs_mov_des_oid_planta,
                                                     par$adocs_mov_des_cod_planta => par$adocs_mov_des_cod_planta,
                                                     par$adocs_mov_des_oid_sector => par$adocs_mov_des_oid_sector,
                                                     par$adocs_mov_des_cod_sector => par$adocs_mov_des_cod_sector,
                                                     par$adocs_sal_des_oid_cuenta => par$adocs_sal_des_oid_cuenta,
                                                     par$adocs_sal_des_oid_client => par$adocs_sal_des_oid_client,
                                                     par$adocs_sal_des_cod_client => par$adocs_sal_des_cod_client,
                                                     par$adocs_sal_des_oid_subcli => par$adocs_sal_des_oid_subcli,
                                                     par$adocs_sal_des_cod_subcli => par$adocs_sal_des_cod_subcli,
                                                     par$adocs_sal_des_oid_ptserv => par$adocs_sal_des_oid_ptserv,
                                                     par$adocs_sal_des_cod_ptserv => par$adocs_sal_des_cod_ptserv,

                                                     par$aefdoc_oid_documento       => par$aefdoc_oid_documento,
                                                     par$aefdoc_oid_divisa          => par$aefdoc_oid_divisa,
                                                     par$aefdoc_oid_denominacion    => par$aefdoc_oid_denominacion,
                                                     par$aefdoc_oid_unid_medida     => par$aefdoc_oid_unid_medida,
                                                     par$aefdoc_cod_niv_detalle     => par$aefdoc_cod_niv_detalle,
                                                     par$aefdoc_cod_tp_efec_tot     => par$aefdoc_cod_tp_efec_tot,
                                                     par$aefdoc_oid_calidad         => par$aefdoc_oid_calidad,
                                                     par$aefdoc_num_importe         => par$aefdoc_num_importe,
                                                     par$aefdoc_nel_cantidad        => par$aefdoc_nel_cantidad,
                                                     par$ampdoc_oid_documento       => par$ampdoc_oid_documento,
                                                     par$ampdoc_oid_divisa          => par$ampdoc_oid_divisa,
                                                     par$ampdoc_oid_medio_pago      => par$ampdoc_oid_medio_pago,
                                                     par$ampdoc_cod_tipo_med_pago   => par$ampdoc_cod_tipo_med_pago,
                                                     par$ampdoc_cod_nivel_detalle   => par$ampdoc_cod_nivel_detalle,
                                                     par$ampdoc_num_importe         => par$ampdoc_num_importe,
                                                     par$ampdoc_nel_cantidad        => par$ampdoc_nel_cantidad,
                                                     par$avtmpdoc_oid_documento     => par$avtmpdoc_oid_documento,
                                                     par$avtmpdoc_oid_t_mediopago   => par$avtmpdoc_oid_t_mediopago,
                                                     par$avtmpdoc_des_valor         => par$avtmpdoc_des_valor,
                                                     par$avtmpdoc_nec_indice_grp    => par$avtmpdoc_nec_indice_grp,
                                                     par$avtdoc_oid_documento       => par$avtdoc_oid_documento,
                                                     par$avtdoc_oid_termino         => var$avtdoc_oid_termino,
                                                     par$avtdoc_cod_termino         => par$avtdoc_cod_termino,
                                                     par$avtdoc_des_valor           => par$avtdoc_des_valor,
                                                     par$aremdoc_oid_remesa         => par$aremdoc_oid_remesa,
                                                     par$aremdoc_oid_documento      => par$aremdoc_oid_documento,
                                                     par$aremdoc_oid_remesa_origen  => par$aremdoc_oid_remesa_origen,
                                                     par$aremdoc_oid_externo        => par$aremdoc_oid_externo,
                                                     par$aremdoc_oid_iac            => par$aremdoc_oid_iac,
                                                     par$aremdoc_cod_recibo_salida  => par$aremdoc_cod_recibo_salida,
                                                     par$aremdoc_usuario_resp       => par$aremdoc_usuario_resp,
                                                     par$aremdoc_puesto_resp        => par$aremdoc_puesto_resp,
                                                     par$aremdoc_cod_ruta           => par$aremdoc_cod_ruta,
                                                     par$aremdoc_nel_parada         => par$aremdoc_nel_parada,
                                                     par$aremdoc_fyh_transporte     => par$aremdoc_fyh_transporte,
                                                     par$aremdoc_nel_cant_bultos    => par$aremdoc_nel_cant_bultos,
                                                     par$aremdoc_fyh_conteo_inicio  => par$aremdoc_fyh_conteo_inicio,
                                                     par$aremdoc_fyh_conteo_fin     => par$aremdoc_fyh_conteo_fin,
                                                     par$aremdoc_oid_remesa_padre   => par$aremdoc_oid_remesa_padre,
                                                     par$aremdoc_oid_remesa_sub     => par$aremdoc_oid_remesa_sub,
                                                     par$aremdoc_cod_cajero         => par$aremdoc_cod_cajero,
                                                     par$aremdoc_cod_nivel_detalle  => par$aremdoc_cod_nivel_detalle,
                                                     par$aremdoc_cod_externo        => par$aremdoc_cod_externo,
                                                     par$aremdoc_cod_estado_abono   => par$aremdoc_cod_estado_abono,
                                                     par$avtrem_oid_remesa          => par$avtrem_oid_remesa,
                                                     par$avtrem_oid_termino         => par$avtrem_oid_termino,
                                                     par$avtrem_cod_termino         => par$avtrem_cod_termino,
                                                     par$avtrem_des_valor           => par$avtrem_des_valor,
                                                     par$avtrem_obligatorio         => par$avtrem_obligatorio,
                                                     par$abuldoc_oid_bulto          => par$abuldoc_oid_bulto,
                                                     par$abuldoc_oid_remesa         => par$abuldoc_oid_remesa,
                                                     par$abuldoc_oid_documento      => par$abuldoc_oid_documento,
                                                     par$abuldoc_oid_externo        => par$abuldoc_oid_externo,
                                                     par$abuldoc_oid_iac            => par$abuldoc_oid_iac,
                                                     par$abuldoc_oid_iac_parciales  => par$abuldoc_oid_iac_parciales,
                                                     par$abuldoc_cod_precinto       => par$abuldoc_cod_precinto,
                                                     par$abuldoc_cod_bolsa          => par$abuldoc_cod_bolsa,
                                                     par$abuldoc_oid_banco_ingreso  => par$abuldoc_oid_banco_ingreso,
                                                     par$abuldoc_bol_banco_ing_mad  => par$abuldoc_bol_banco_ing_mad,
                                                     par$abuldoc_usuario_resp       => par$abuldoc_usuario_resp,
                                                     par$abuldoc_puesto_resp        => par$abuldoc_puesto_resp,
                                                     par$abuldoc_nel_cant_parciales => par$abuldoc_nel_cant_parciales,
                                                     par$abuldoc_fyh_conteo_inicio  => par$abuldoc_fyh_conteo_inicio,
                                                     par$abuldoc_fyh_conteo_fin     => par$abuldoc_fyh_conteo_fin,
                                                     par$abuldoc_fyh_proceso_leg    => par$abuldoc_fyh_proceso_leg,
                                                     par$abuldoc_oid_bulto_padre    => par$abuldoc_oid_bulto_padre,
                                                     par$abuldoc_oid_bulto_sub      => par$abuldoc_oid_bulto_sub,
                                                     par$abuldoc_cod_nivel_detalle  => par$abuldoc_cod_nivel_detalle,
                                                     par$abuldoc_bol_cuadrado       => par$abuldoc_bol_cuadrado,
                                                     par$abuldoc_oid_tipo_formato   => par$abuldoc_oid_tipo_formato,
                                                     par$abuldoc_oid_tipo_servicio  => par$abuldoc_oid_tipo_servicio,
                                                     par$avtbul_oid_bulto           => par$avtbul_oid_bulto,
                                                     par$avtbul_oid_termino         => par$avtbul_oid_termino,
                                                     par$avtbul_cod_termino         => par$avtbul_cod_termino,
                                                     par$avtbul_des_valor           => par$avtbul_des_valor,
                                                     par$avtbul_obligatorio         => par$avtbul_obligatorio,
                                                     par$apardoc_oid_remesa         => par$apardoc_oid_remesa,
                                                     par$apardoc_oid_bulto          => par$apardoc_oid_bulto,
                                                     par$apardoc_oid_parcial        => par$apardoc_oid_parcial,
                                                     par$apardoc_oid_externo        => par$apardoc_oid_externo,
                                                     par$apardoc_cod_precinto       => par$apardoc_cod_precinto,
                                                     par$apardoc_oid_iac            => par$apardoc_oid_iac,
                                                     par$apardoc_usuario_resp       => par$apardoc_usuario_resp,
                                                     par$apardoc_puesto_resp        => par$apardoc_puesto_resp,
                                                     par$apardoc_nec_secuencia      => par$apardoc_nec_secuencia,
                                                     par$apardoc_oid_parcial_padre  => par$apardoc_oid_parcial_padre,
                                                     par$apardoc_oid_parcial_sub    => par$apardoc_oid_parcial_sub,
                                                     par$apardoc_oid_tipo_formato   => par$apardoc_oid_tipo_formato,
                                                     par$avtpar_oid_parcial         => par$avtpar_oid_parcial,
                                                     par$avtpar_oid_termino         => par$avtpar_oid_termino,
                                                     par$avtpar_cod_termino         => par$avtpar_cod_termino,
                                                     par$avtpar_des_valor           => par$avtpar_des_valor,
                                                     par$avtpar_obligatorio         => par$avtpar_obligatorio,
                                                     par$aelemvalefe_tipo           => par$aelemvalefe_tipo,
                                                     par$aelemvalefe_oid_remesa     => par$aelemvalefe_oid_remesa,
                                                     par$aelemvalefe_oid_bulto      => par$aelemvalefe_oid_bulto,
                                                     par$aelemvalefe_oid_parcial    => par$aelemvalefe_oid_parcial,
                                                     par$aelemvalefe_oid_divisa     => par$aelemvalefe_oid_divisa,
                                                     par$aelemvalefe_oid_denom      => par$aelemvalefe_oid_denom,
                                                     par$aelemvalefe_bol_esbillete  => par$aelemvalefe_bol_esbillete,
                                                     par$aelemvalefe_oid_unid_med   => par$aelemvalefe_oid_unid_med,
                                                     par$aelemvalefe_cod_tipo_efec  => par$aelemvalefe_cod_tipo_efec,
                                                     par$aelemvalefe_num_importe    => par$aelemvalefe_num_importe,
                                                     par$aelemvalefe_nel_cantidad   => par$aelemvalefe_nel_cantidad,
                                                     par$aelemvalefe_cod_nvdetalle  => par$aelemvalefe_cod_nvdetalle,
                                                     par$aelemvalefe_bol_ingresado  => par$aelemvalefe_bol_ingresado,
                                                     par$aelemvalefe_oid_calidad    => par$aelemvalefe_oid_calidad,
                                                     par$aelemval_mp_tipo           => par$aelemval_mp_tipo,
                                                     par$aelemval_mp_oid_valor      => par$aelemval_mp_oid_valor,
                                                     par$aelemval_mp_oid_remesa     => par$aelemval_mp_oid_remesa,
                                                     par$aelemval_mp_oid_bulto      => par$aelemval_mp_oid_bulto,
                                                     par$aelemval_mp_oid_parcial    => par$aelemval_mp_oid_parcial,
                                                     par$aelemval_mp_oid_divisa     => par$aelemval_mp_oid_divisa,
                                                     par$aelemval_mp_oid_mediopago  => par$aelemval_mp_oid_mediopago,
                                                     par$aelemval_mp_cod_tipo_mp    => par$aelemval_mp_cod_tipo_mp,
                                                     par$aelemval_mp_num_importe    => par$aelemval_mp_num_importe,
                                                     par$aelemval_mp_nel_cantidad   => par$aelemval_mp_nel_cantidad,
                                                     par$aelemval_mp_cod_nvdetalle  => par$aelemval_mp_cod_nvdetalle,
                                                     par$aelemval_mp_bol_ingresado  => par$aelemval_mp_bol_ingresado,
                                                     par$aelemval_mp_nel_secuencia  => par$aelemval_mp_nel_secuencia,
                                                     par$aelemter_mp_tipo           => par$aelemter_mp_tipo,
                                                     par$aelemter_mp_oid_valor      => par$aelemter_mp_oid_valor,
                                                     par$aelemter_mp_oid_termino    => par$aelemter_mp_oid_termino,
                                                     par$aelemter_mp_des_valor      => par$aelemter_mp_des_valor,
                                                     par$aelemter_mp_nel_ind_grupo  => par$aelemter_mp_nel_ind_grupo,
                                                     par$usuario                    => par$usuario,
                                                     par$cod_cultura                => par$cod_cultura,
                                                     par$info_ejecucion             => par$info_ejecucion,
                                                     par$hacer_commit               => 0,
                                                     par$confirmar_doc              => 0,
                                                     par$doc_rc_documentos          => par$doc_rc_documentos,
                                                     par$doc_rc_historico_mov_doc   => par$doc_rc_historico_mov_doc,
                                                     par$ele_rc_elementos           => par$ele_rc_elementos,
                                                     par$inserts                    => var$inserts_int,
                                                     par$updates                    => var$updates_int,
                                                     par$deletes                    => var$deletes_int,
                                                     par$selects                    => var$selects_int,
                                                     par$cod_ejecucion              => par$cod_ejecucion);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$deletes_int + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$selects   := var$selects + var$selects_int;

    IF par$confirmar_doc > 0 THEN

      /* limpiar tabla temporaria */
      delete sapr_gtt_toids;
      var$deletes   := var$deletes + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;

      var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
      var$cod_comprobante      := '';
      var$rowver               := par$rowver;

      sapr_ptransiciones_###VERSION###.sejecutar_grp_doc(par$oid_grupo_documento  => par$oid_grupo_documento,
                                                    par$cod_estado_documento => var$cod_estado_documento,
                                                    par$cod_comprobante      => var$cod_comprobante,
                                                    par$rowver               => var$rowver,
                                                    par$usuario              => par$usuario,
                                                    par$cod_cultura          => par$cod_cultura,
                                                    par$info_ejecucion       => par$info_ejecucion,
                                                    par$cod_ejecucion        => par$cod_ejecucion,
                                                    par$hacer_commit         => 0,
                                                    par$inserts              => var$inserts_int,
                                                    par$updates              => var$updates_int,
                                                    par$deletes              => var$deletes_int,
                                                    par$selects              => var$selects_int);

      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$deletes_int + var$selects_int;
      var$inserts   := var$inserts + var$inserts_int;
      var$updates   := var$updates + var$updates_int;
      var$deletes   := var$deletes + var$deletes_int;
      var$selects   := var$selects + var$selects_int;

      var$oid_grupo_retorno := par$oid_grupo_documento;

      IF var$esreenvio THEN

        var$r_oid_grupo_documento := sys_guid();
        var$oid_grupo_retorno     := var$r_oid_grupo_documento;

        /* Atualiza identificadores dos documentos de reenvio */

        FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
          var$r_adocs_oid(idx) := sys_guid();
          var$r_adocs_oid_sustituto(idx) := NULL;
          var$r_adocs_oid_mov_fondo(idx) := NULL;
        END LOOP;

        /* Valores Efectivos */
        IF NOT
            gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefdoc_oid_documento) THEN
          FOR idx IN par$aefdoc_oid_documento.first .. par$aefdoc_oid_documento.last LOOP
            FOR idx2 IN par$adocs_oid.first .. par$adocs_oid.last LOOP
              IF par$aefdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_aefdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
                EXIT;
              END IF;
            END LOOP;
          END LOOP;
        END IF;

        /* Valores Medio Pago */
        IF NOT
            gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampdoc_oid_documento) THEN
          FOR idx IN par$ampdoc_oid_documento.first .. par$ampdoc_oid_documento.last LOOP
            FOR idx2 IN par$adocs_oid.first .. par$adocs_oid.last LOOP
              IF par$ampdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_ampdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
                EXIT;
              END IF;
            END LOOP;
          END LOOP;
        END IF;

        /* Valores Terminos Medio Pago */
        IF NOT
            gepr_putilidades_###VERSION###.farray_vacio_oid(par$avtmpdoc_oid_documento) THEN
          FOR idx IN par$avtmpdoc_oid_documento.first .. par$avtmpdoc_oid_documento.last LOOP
            FOR idx2 IN par$adocs_oid.first .. par$adocs_oid.last LOOP
              IF par$avtmpdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_avtmpdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
                EXIT;
              END IF;
            END LOOP;
          END LOOP;
        END IF;

        /* Valores Terminos Documento */
        IF NOT
            gepr_putilidades_###VERSION###.farray_vacio_oid(par$avtdoc_oid_documento) THEN
          /* para todos os terminos do reenvio */
          FOR rec$termino IN cur$terminos(var$r_oid_formulario_grupo) LOOP
            /* para todos os terminos do altas */
            FOR idx IN par$avtdoc_oid_documento.first .. par$avtdoc_oid_documento.last LOOP
              IF rec$termino.oid_termino = par$avtdoc_oid_termino(idx) THEN
                FOR idx2 IN par$adocs_oid.first .. par$adocs_oid.last LOOP
                  IF par$avtdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                    var$r_avtdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
                    var$r_avtdoc_oid_termino(idx) := rec$termino.oid_termino;
                    var$r_avtdoc_des_valor(idx) := par$avtdoc_des_valor(idx);
                    EXIT;
                  END IF;
                END LOOP;
              END IF;
            END LOOP;
          END LOOP rec$termino;
        END IF;

        /* Valores Terminos Grupo Documento */
        IF NOT
            gepr_putilidades_###VERSION###.farray_vacio_oid(var$avtgdoc_oid_termino) THEN
          /* para todos os terminos do reenvio */
          FOR rec$termino IN cur$terminosgrupo(var$r_oid_formulario_grupo) LOOP
            /* para todos os terminos do altas */
            FOR idx IN var$avtgdoc_oid_termino.first .. var$avtgdoc_oid_termino.last LOOP
              IF rec$termino.oid_termino = var$avtgdoc_oid_termino(idx) THEN
                var$r_avtgdoc_oid_termino(idx) := rec$termino.oid_termino;
                var$r_avtgdoc_des_valor(idx) := par$avtgdoc_des_valor(idx);
              END IF;
            END LOOP;
          END LOOP rec$termino;
        END IF;

        /* Valores Remesas */
        IF NOT
            gepr_putilidades_###VERSION###.farray_vacio_oid(par$aremdoc_oid_documento) THEN
          FOR idx IN par$aremdoc_oid_documento.first .. par$aremdoc_oid_documento.last LOOP
            FOR idx2 IN par$adocs_oid.first .. par$adocs_oid.last LOOP
              IF par$aremdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_aremdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
                EXIT;
              END IF;
            END LOOP;
          END LOOP;
        END IF;

        /* Valores Bultos */
        IF NOT
            gepr_putilidades_###VERSION###.farray_vacio_oid(par$abuldoc_oid_documento) THEN
          FOR idx IN par$abuldoc_oid_documento.first .. par$abuldoc_oid_documento.last LOOP
            FOR idx2 IN par$adocs_oid.first .. par$adocs_oid.last LOOP
              IF par$abuldoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_abuldoc_oid_doc(idx) := var$r_adocs_oid(idx2);
                EXIT;
              END IF;
            END LOOP;
          END LOOP;
        END IF;

        var$r_oid_formulario_grupo := sapr_pformulario_###VERSION###.frecuperar_formulario_reenvio(var$oid_formulario_grupo,
                                                                                              par$cod_cultura);

        sguardar_grp_docs_elem_re_baj(par$oid_grupo_documento       => var$r_oid_grupo_documento,
                                      par$oid_grupo_documento_padre => par$oid_grupo_documento,
                                      par$oid_formulario_grupo      => var$r_oid_formulario_grupo,
                                      par$oid_sector_origen         => par$oid_sector_origen,
                                      par$oid_sector_destino        => par$oid_sector_destino,
                                      par$rowver                    => par$rowver,
                                      par$avtgdoc_oid_termino       => var$r_avtgdoc_oid_termino,
                                      par$avtgdoc_des_valor         => var$r_avtgdoc_des_valor,
                                      par$adocs_oid                 => var$r_adocs_oid,
                                      par$adocs_oid_doc_padre       => par$adocs_oid,
                                      par$adocs_oid_sustituto       => var$r_adocs_oid_sustituto,
                                      par$adocs_oid_mov_fondos      => var$r_adocs_oid_mov_fondo,
                                      par$adocs_fyh_plncertif       => par$adocs_fyh_plncertif,
                                      par$adocs_fyh_gestion         => par$adocs_fyh_gestion,
                                      par$adocs_cod_externo         => par$adocs_cod_externo,
                                      par$adocs_rowver              => par$adocs_rowver,

                                      par$adocs_mov_ori_oid_cuenta => par$adocs_mov_ori_oid_cuenta,
                                      par$adocs_mov_ori_oid_client => par$adocs_mov_ori_oid_client,
                                      par$adocs_mov_ori_cod_client => par$adocs_mov_ori_cod_client,
                                      par$adocs_mov_ori_oid_subcli => par$adocs_mov_ori_oid_subcli,
                                      par$adocs_mov_ori_cod_subcli => par$adocs_mov_ori_cod_subcli,
                                      par$adocs_mov_ori_oid_ptserv => par$adocs_mov_ori_oid_ptserv,
                                      par$adocs_mov_ori_cod_ptserv => par$adocs_mov_ori_cod_ptserv,
                                      par$adocs_mov_ori_oid_canal  => par$adocs_mov_ori_oid_canal,
                                      par$adocs_mov_ori_cod_canal  => par$adocs_mov_ori_cod_canal,
                                      par$adocs_mov_ori_oid_subcan => par$adocs_mov_ori_oid_subcan,
                                      par$adocs_mov_ori_cod_subcan => par$adocs_mov_ori_cod_subcan,
                                      par$adocs_mov_ori_oid_delega => par$adocs_mov_ori_oid_delega,
                                      par$adocs_mov_ori_cod_delega => par$adocs_mov_ori_cod_delega,
                                      par$adocs_mov_ori_oid_planta => par$adocs_mov_ori_oid_planta,
                                      par$adocs_mov_ori_cod_planta => par$adocs_mov_ori_cod_planta,
                                      par$adocs_mov_ori_oid_sector => par$adocs_mov_ori_oid_sector,
                                      par$adocs_mov_ori_cod_sector => par$adocs_mov_ori_cod_sector,

                                      par$adocs_mov_des_oid_cuenta => par$adocs_mov_des_oid_cuenta,
                                      par$adocs_mov_des_oid_client => par$adocs_mov_des_oid_client,
                                      par$adocs_mov_des_cod_client => par$adocs_mov_des_cod_client,
                                      par$adocs_mov_des_oid_subcli => par$adocs_mov_des_oid_subcli,
                                      par$adocs_mov_des_cod_subcli => par$adocs_mov_des_cod_subcli,
                                      par$adocs_mov_des_oid_ptserv => par$adocs_mov_des_oid_ptserv,
                                      par$adocs_mov_des_cod_ptserv => par$adocs_mov_des_cod_ptserv,
                                      par$adocs_mov_des_oid_canal  => par$adocs_mov_des_oid_canal,
                                      par$adocs_mov_des_cod_canal  => par$adocs_mov_des_cod_canal,
                                      par$adocs_mov_des_oid_subcan => par$adocs_mov_des_oid_subcan,
                                      par$adocs_mov_des_cod_subcan => par$adocs_mov_des_cod_subcan,
                                      par$adocs_mov_des_oid_delega => par$adocs_mov_des_oid_delega,
                                      par$adocs_mov_des_cod_delega => par$adocs_mov_des_cod_delega,
                                      par$adocs_mov_des_oid_planta => par$adocs_mov_des_oid_planta,
                                      par$adocs_mov_des_cod_planta => par$adocs_mov_des_cod_planta,
                                      par$adocs_mov_des_oid_sector => par$adocs_mov_des_oid_sector,
                                      par$adocs_mov_des_cod_sector => par$adocs_mov_des_cod_sector,

                                      par$aefdoc_oid_documento     => var$r_aefdoc_oid_doc,
                                      par$aefdoc_oid_divisa        => par$aefdoc_oid_divisa,
                                      par$aefdoc_oid_denominacion  => par$aefdoc_oid_denominacion,
                                      par$aefdoc_oid_unid_medida   => par$aefdoc_oid_unid_medida,
                                      par$aefdoc_cod_niv_detalle   => par$aefdoc_cod_niv_detalle,
                                      par$aefdoc_cod_tp_efec_tot   => par$aefdoc_cod_tp_efec_tot,
                                      par$aefdoc_oid_calidad       => par$aefdoc_oid_calidad,
                                      par$aefdoc_num_importe       => par$aefdoc_num_importe,
                                      par$aefdoc_nel_cantidad      => par$aefdoc_nel_cantidad,
                                      par$ampdoc_oid_documento     => var$r_ampdoc_oid_doc,
                                      par$ampdoc_oid_divisa        => par$ampdoc_oid_divisa,
                                      par$ampdoc_oid_medio_pago    => par$ampdoc_oid_medio_pago,
                                      par$ampdoc_cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago,
                                      par$ampdoc_cod_nivel_detalle => par$ampdoc_cod_nivel_detalle,
                                      par$ampdoc_num_importe       => par$ampdoc_num_importe,
                                      par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,
                                      par$avtmpdoc_oid_documento   => var$r_avtmpdoc_oid_doc,
                                      par$avtmpdoc_oid_t_mediopago => par$avtmpdoc_oid_t_mediopago,
                                      par$avtmpdoc_des_valor       => par$avtmpdoc_des_valor,
                                      par$avtmpdoc_nec_indice_grp  => par$avtmpdoc_nec_indice_grp,
                                      par$aremdoc_oid_remesa       => par$aremdoc_oid_remesa,
                                      par$aremdoc_oid_documento    => var$r_aremdoc_oid_doc,
                                      par$aremdoc_rowver           => var$aremdoc_rowver,
                                      par$abuldoc_oid_bulto        => par$abuldoc_oid_bulto,
                                      par$abuldoc_oid_remesa       => par$abuldoc_oid_remesa,
                                      par$abuldoc_oid_documento    => var$r_abuldoc_oid_doc,
                                      par$abuldoc_rowver           => var$abuldoc_rowver,
                                      par$usuario                  => par$usuario,
                                      par$cod_cultura              => par$cod_cultura,
                                      par$info_ejecucion           => par$info_ejecucion,
                                      par$hacer_commit             => 0,
                                      par$confirmar_doc            => 1,
                                      par$rc_grupo_documentos      => par$rc_grupo_documentos,
                                      par$rc_historico_mov_grupo   => par$rc_historico_mov_grupo,
                                      par$doc_rc_documentos        => par$doc_rc_documentos,
                                      par$doc_rc_historico_mov_doc => par$doc_rc_historico_mov_doc,
                                      par$ele_rc_elementos         => par$ele_rc_elementos,
                                      par$cod_ejecucion            => par$cod_ejecucion);

      END IF;

      /* - Documentos y Documentos Padres */
      sapr_pdocumento_###VERSION###.scargar_tt_oids_documento_gpr(par$oid_grupo_documento => var$oid_grupo_retorno,
                                                             par$recuperar_padres    => true,
                                                             par$inserts             => var$inserts_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$registros := var$registros + var$inserts_int;

      /* - TABLA: Grupo de Documentos */
      par$rc_grupo_documentos := fcargar_rc_grupo_documento(var$oid_grupo_retorno,
                                                            par$selects => var$selects_int);
      var$selects             := var$selects + var$selects_int;
      var$registros           := var$registros + var$selects_int;

      /* - TABLA: Historico Grupo */
      par$rc_historico_mov_grupo := fcargar_rc_historico(var$oid_grupo_retorno,
                                                         par$selects => var$selects_int);
      var$selects                := var$selects + var$selects_int;
      var$registros              := var$registros + var$selects_int;

      /* - TABLA: Documentos */
      par$doc_rc_documentos := sapr_pdocumento_###VERSION###.fcargar_rc_documento;
      var$selects           := var$selects + SQL % ROWCOUNT;
      var$registros         := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := sapr_pdocumento_###VERSION###.fcargar_rc_historico;
      var$selects                  := var$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
      par$ele_rc_elementos := sapr_premesa_###VERSION###.fcargar_rc_elementos;
      var$selects          := var$selects + SQL % ROWCOUNT;
      var$registros        := var$registros + SQL % ROWCOUNT;

    END IF;

    IF par$hacer_commit > 0 THEN
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
                                                           par$usuario,
                                                           par$cod_ejecucion);
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
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END sguardar_grp_docs_elem_altas;

  /* Guardar documento de Reenvio y Baja - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_grp_docs_elem_re_baj(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,

                                          /* arrays asociativos */
                                          /* arrays de terminos del grupo de documentos */
                                          par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                          /* arrays de documentos */
                                          par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_cod_externo    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_rowver         IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* cuenta origen */
                                          par$adocs_mov_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,

                                          /* cuenta destino */
                                          par$adocs_mov_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,

                                          /* arrays de efectivo por documento */
                                          par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                          par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* arrays de medio de pago por documento */
                                          par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                          par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* arrays de terminos de medio de pago por documento */
                                          par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                          par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                          /* Remesa - arrays */
                                          par$aremdoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aremdoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aremdoc_rowver        IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* Bulto - arrays */
                                          par$abuldoc_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_rowver        IN gepr_pcomon_###VERSION###.tipo$nels_,

                                          /* informacion de ejecucion */
                                          par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$hacer_commit             IN INTEGER,
                                          par$confirmar_doc            IN INTEGER,
                                          par$rc_grupo_documentos      OUT sys_refcursor,
                                          par$rc_historico_mov_grupo   OUT sys_refcursor,
                                          par$doc_rc_documentos        OUT sys_refcursor,
                                          par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                          par$ele_rc_elementos         OUT sys_refcursor,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_docs_elem_re_baj';

    /* variables locales */
    var$esnuevo              BOOLEAN;
    var$inicio               gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros            gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                  VARCHAR2(4000);
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante      gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver               gepr_pcomon_###VERSION###.tipo$nel_;
    var$rowver_elemento      gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_gestion_bulto    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$array_fecha_vacio    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$array_desc_vacio      gepr_pcomon_###VERSION###.tipo$descs_;
  BEGIN

    /* Valores iniciales */
    var$inicio := SYSTIMESTAMP;

    /* Verificar si es gestion de bulto */
    IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario_grupo,
                                                          par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionBulto) THEN
      var$bol_gestion_bulto := 1;
    END IF;

    IF var$bol_gestion_bulto = 0 THEN
      /* Gestion de remesa */

      FOR idx IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP

        var$rowver_elemento := par$aremdoc_rowver(idx);
        /*atualiza o campo rowver da remesa, para que outro reenvio não seja criado*/
        sapr_premesa_###VERSION###.supd_tremesa_rowver(par$oid_remesa        => par$aremdoc_oid_remesa(idx),
                                                  par$rowver            => var$rowver_elemento,
                                                  par$cod_cultura       => par$cod_cultura,
                                                  par$cod_funcionalidad => 'reenvio');
      end loop;
    ELSE
      /* Gestion de remesa */
      FOR idx IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP

        var$rowver_elemento := par$abuldoc_rowver(idx);
        /*atualiza o campo rowver do bulto, para que outro reenvio não seja criado*/
        sapr_pbulto_###VERSION###.supd_tbulto_rowver(par$oid_bulto         => par$abuldoc_oid_bulto(idx),
                                                par$rowver            => var$rowver_elemento,
                                                par$cod_cultura       => par$cod_cultura,
                                                par$cod_funcionalidad => 'reenvio');
      end loop;
    END IF;

    /* Grabar las informaciones del grupo */
    sguardar_grp_documentos(par$oid_grupo_documento       => par$oid_grupo_documento,
                            par$oid_grupo_documento_padre => par$oid_grupo_documento_padre,
                            par$oid_formulario_grupo      => par$oid_formulario_grupo,
                            par$oid_sector_origen         => par$oid_sector_origen,
                            par$oid_sector_destino        => par$oid_sector_destino,
                            par$usuario                   => par$usuario,
                            par$rowver                    => par$rowver,
                            par$adocs_oid                 => par$adocs_oid,
                            par$avtgdoc_oid_termino       => par$avtgdoc_oid_termino,
                            par$avtgdoc_des_valor         => par$avtgdoc_des_valor,
                            par$esnuevo                   => var$esnuevo,
                            par$inserts                   => var$inserts_int,
                            par$updates                   => var$updates_int,
                            par$deletes                   => var$deletes_int,
                            par$selects                   => var$selects_int,
                            par$cod_cultura               => par$cod_cultura);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$deletes_int + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$selects   := var$selects + var$selects_int;

    /* Grabar los documentos del grupo */
    sapr_pdocumento_###VERSION###.sguardar_doc_elem_reenviobaja(par$oid_formulario      => par$oid_formulario_grupo,
                                                           par$oid_grupo_documento => par$oid_grupo_documento,

                                                           /* arrays asociativos */
                                                           /* arrays de documentos */
                                                           par$adocs_oid            => par$adocs_oid,
                                                           par$adocs_oid_doc_padre  => par$adocs_oid_doc_padre,
                                                           par$adocs_oid_sustituto  => par$adocs_oid_sustituto,
                                                           par$adocs_oid_mov_fondos => par$adocs_oid_mov_fondos,
                                                           par$adocs_fyh_plncertif  => par$adocs_fyh_plncertif,
                                                           par$adocs_fyh_gestion    => par$adocs_fyh_gestion,
                                                           par$adocs_fyh_contable   => var$array_fecha_vacio,                                 
                                                           par$adocs_cod_actual_id  => var$array_desc_vacio,
                                                           par$adocs_collection_id  => var$array_desc_vacio,
                                                           par$adocs_cod_externo    => par$adocs_cod_externo,
                                                           par$adocs_rowver         => par$adocs_rowver,

                                                           /* cuenta origen */
                                                           par$adocs_mov_ori_oid_cuenta => par$adocs_mov_ori_oid_cuenta,
                                                           par$adocs_mov_ori_oid_client => par$adocs_mov_ori_oid_client,
                                                           par$adocs_mov_ori_cod_client => par$adocs_mov_ori_cod_client,
                                                           par$adocs_mov_ori_oid_subcli => par$adocs_mov_ori_oid_subcli,
                                                           par$adocs_mov_ori_cod_subcli => par$adocs_mov_ori_cod_subcli,
                                                           par$adocs_mov_ori_oid_ptserv => par$adocs_mov_ori_oid_ptserv,
                                                           par$adocs_mov_ori_cod_ptserv => par$adocs_mov_ori_cod_ptserv,
                                                           par$adocs_mov_ori_oid_canal  => par$adocs_mov_ori_oid_canal,
                                                           par$adocs_mov_ori_cod_canal  => par$adocs_mov_ori_cod_canal,
                                                           par$adocs_mov_ori_oid_subcan => par$adocs_mov_ori_oid_subcan,
                                                           par$adocs_mov_ori_cod_subcan => par$adocs_mov_ori_cod_subcan,
                                                           par$adocs_mov_ori_oid_delega => par$adocs_mov_ori_oid_delega,
                                                           par$adocs_mov_ori_cod_delega => par$adocs_mov_ori_cod_delega,
                                                           par$adocs_mov_ori_oid_planta => par$adocs_mov_ori_oid_planta,
                                                           par$adocs_mov_ori_cod_planta => par$adocs_mov_ori_cod_planta,
                                                           par$adocs_mov_ori_oid_sector => par$adocs_mov_ori_oid_sector,
                                                           par$adocs_mov_ori_cod_sector => par$adocs_mov_ori_cod_sector,

                                                           /* cuenta destino */
                                                           par$adocs_mov_des_oid_cuenta => par$adocs_mov_des_oid_cuenta,
                                                           par$adocs_mov_des_oid_client => par$adocs_mov_des_oid_client,
                                                           par$adocs_mov_des_cod_client => par$adocs_mov_des_cod_client,
                                                           par$adocs_mov_des_oid_subcli => par$adocs_mov_des_oid_subcli,
                                                           par$adocs_mov_des_cod_subcli => par$adocs_mov_des_cod_subcli,
                                                           par$adocs_mov_des_oid_ptserv => par$adocs_mov_des_oid_ptserv,
                                                           par$adocs_mov_des_cod_ptserv => par$adocs_mov_des_cod_ptserv,
                                                           par$adocs_mov_des_oid_canal  => par$adocs_mov_des_oid_canal,
                                                           par$adocs_mov_des_cod_canal  => par$adocs_mov_des_cod_canal,
                                                           par$adocs_mov_des_oid_subcan => par$adocs_mov_des_oid_subcan,
                                                           par$adocs_mov_des_cod_subcan => par$adocs_mov_des_cod_subcan,
                                                           par$adocs_mov_des_oid_delega => par$adocs_mov_des_oid_delega,
                                                           par$adocs_mov_des_cod_delega => par$adocs_mov_des_cod_delega,
                                                           par$adocs_mov_des_oid_planta => par$adocs_mov_des_oid_planta,
                                                           par$adocs_mov_des_cod_planta => par$adocs_mov_des_cod_planta,
                                                           par$adocs_mov_des_oid_sector => par$adocs_mov_des_oid_sector,
                                                           par$adocs_mov_des_cod_sector => par$adocs_mov_des_cod_sector,

                                                           /* arrays de efectivo por documento */
                                                           par$aefdoc_oid_documento    => par$aefdoc_oid_documento,
                                                           par$aefdoc_oid_divisa       => par$aefdoc_oid_divisa,
                                                           par$aefdoc_oid_denominacion => par$aefdoc_oid_denominacion,
                                                           par$aefdoc_oid_unid_medida  => par$aefdoc_oid_unid_medida,
                                                           par$aefdoc_cod_niv_detalle  => par$aefdoc_cod_niv_detalle,
                                                           par$aefdoc_cod_tp_efec_tot  => par$aefdoc_cod_tp_efec_tot,
                                                           par$aefdoc_oid_calidad      => par$aefdoc_oid_calidad,
                                                           par$aefdoc_num_importe      => par$aefdoc_num_importe,
                                                           par$aefdoc_nel_cantidad     => par$aefdoc_nel_cantidad,

                                                           /* arrays de medio de pago por documento */
                                                           par$ampdoc_oid_documento     => par$ampdoc_oid_documento,
                                                           par$ampdoc_oid_divisa        => par$ampdoc_oid_divisa,
                                                           par$ampdoc_oid_medio_pago    => par$ampdoc_oid_medio_pago,
                                                           par$ampdoc_cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago,
                                                           par$ampdoc_cod_nivel_detalle => par$ampdoc_cod_nivel_detalle,
                                                           par$ampdoc_num_importe       => par$ampdoc_num_importe,
                                                           par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,

                                                           /* arrays de terminos de medio de pago por documento */
                                                           par$avtmpdoc_oid_documento   => par$avtmpdoc_oid_documento,
                                                           par$avtmpdoc_oid_t_mediopago => par$avtmpdoc_oid_t_mediopago,
                                                           par$avtmpdoc_des_valor       => par$avtmpdoc_des_valor,
                                                           par$avtmpdoc_nec_indice_grp  => par$avtmpdoc_nec_indice_grp,

                                                           /* Remesa - arrays */
                                                           par$aremdoc_oid_remesa    => par$aremdoc_oid_remesa,
                                                           par$aremdoc_oid_documento => par$aremdoc_oid_documento,

                                                           /* Bulto - arrays */
                                                           par$abuldoc_oid_bulto     => par$abuldoc_oid_bulto,
                                                           par$abuldoc_oid_remesa    => par$abuldoc_oid_remesa,
                                                           par$abuldoc_oid_documento => par$abuldoc_oid_documento,

                                                           par$usuario                  => par$usuario,
                                                           par$cod_cultura              => par$cod_cultura,
                                                           par$info_ejecucion           => par$info_ejecucion,
                                                           par$hacer_commit             => 0,
                                                           par$confirmar_doc            => 0,
                                                           par$doc_rc_documentos        => par$doc_rc_documentos,
                                                           par$doc_rc_historico_mov_doc => par$doc_rc_historico_mov_doc,
                                                           par$ele_rc_elementos         => par$ele_rc_elementos,
                                                           par$inserts                  => var$inserts_int,
                                                           par$updates                  => var$updates_int,
                                                           par$deletes                  => var$deletes_int,
                                                           par$selects                  => var$selects_int,
                                                           par$cod_ejecucion            => par$cod_ejecucion);

    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$deletes_int + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$updates   := var$updates + var$updates_int;
    var$deletes   := var$deletes + var$deletes_int;
    var$selects   := var$selects + var$selects_int;

    IF par$confirmar_doc > 0 THEN

      /* limpiar tabla temporaria */
      delete sapr_gtt_toids;
      var$deletes   := var$deletes + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;

      var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
      var$cod_comprobante      := '';
      var$rowver               := par$rowver;

      sapr_ptransiciones_###VERSION###.sejecutar_grp_doc(par$oid_grupo_documento  => par$oid_grupo_documento,
                                                    par$cod_estado_documento => var$cod_estado_documento,
                                                    par$cod_comprobante      => var$cod_comprobante,
                                                    par$rowver               => var$rowver,
                                                    par$usuario              => par$usuario,
                                                    par$cod_cultura          => par$cod_cultura,
                                                    par$info_ejecucion       => par$info_ejecucion,
                                                    par$cod_ejecucion        => par$cod_ejecucion,
                                                    par$hacer_commit         => 0,
                                                    par$inserts              => var$inserts_int,
                                                    par$updates              => var$updates_int,
                                                    par$deletes              => var$deletes_int,
                                                    par$selects              => var$selects_int);

      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$deletes_int + var$selects_int;
      var$inserts   := var$inserts + var$inserts_int;
      var$updates   := var$updates + var$updates_int;
      var$deletes   := var$deletes + var$deletes_int;
      var$selects   := var$selects + var$selects_int;

      /* - Documentos y Documentos Padres */
      sapr_pdocumento_###VERSION###.scargar_tt_oids_documento_gpr(par$oid_grupo_documento => par$oid_grupo_documento,
                                                             par$recuperar_padres    => true,
                                                             par$inserts             => var$inserts_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$registros := var$registros + var$inserts_int;

      /* - TABLA: Grupo de Documentos */
      par$rc_grupo_documentos := fcargar_rc_grupo_documento(par$oid_grupo_documento,
                                                            par$selects => var$selects_int);
      var$selects             := var$selects + var$selects_int;
      var$registros           := var$registros + var$selects_int;

      /* - TABLA: Historico Grupo */
      par$rc_historico_mov_grupo := fcargar_rc_historico(par$oid_grupo_documento,
                                                         par$selects => var$selects_int);
      var$selects                := var$selects + var$selects_int;
      var$registros              := var$registros + var$selects_int;

      /* - TABLA: Documentos */
      par$doc_rc_documentos := sapr_pdocumento_###VERSION###.fcargar_rc_documento;
      var$selects           := var$selects + SQL % ROWCOUNT;
      var$registros         := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := sapr_pdocumento_###VERSION###.fcargar_rc_historico;
      var$selects                  := var$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
      par$ele_rc_elementos := sapr_premesa_###VERSION###.fcargar_rc_elementos;
      var$selects          := var$selects + SQL % ROWCOUNT;
      var$registros        := var$registros + SQL % ROWCOUNT;

    END IF;

    IF par$hacer_commit > 0 THEN
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
                                                           par$usuario,
                                                           par$cod_ejecucion);
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
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END sguardar_grp_docs_elem_re_baj;

  /* Guardar documento de Reenvio - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_grp_docs_elem_reenvi(par$aremdoc_oid_remesa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aremdoc_cod_remesa       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aremdoc_rowver           IN gepr_pcomon_###VERSION###.tipo$nels_,
                                          par$abuldoc_oid_bulto        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_cod_bulto        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$abuldoc_rowver           IN gepr_pcomon_###VERSION###.tipo$nels_,
                                          par$adocs_mov_ori_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_ori_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_ori_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_cuenta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_oid_client IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_client IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_subcli IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_subcli IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_ptserv IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_ptserv IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_canal  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_canal  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_subcan IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_subcan IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_delega IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_delega IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_planta IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_planta IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$adocs_mov_des_oid_sector IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_mov_des_cod_sector IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$bol_gestion_bulto        IN gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$cod_integracion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$hacer_commit             IN INTEGER,
                                          par$confirmar_doc            IN INTEGER,
                                          par$aceptar_doc              IN INTEGER,
                                          par$rc_grupo_documentos      OUT sys_refcursor,
                                          par$rc_historico_mov_grupo   OUT sys_refcursor,
                                          par$doc_rc_documentos        OUT sys_refcursor,
                                          par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                          par$ele_rc_elementos         OUT sys_refcursor,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) AS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_docs_elem_reenvi';

    /* variables locales */
    var$inicio               gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros            gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                  VARCHAR2(4000);
    var$bol_gestion_bulto    gepr_pcomon_###VERSION###.tipo$nel_;
    var$esreenvio            BOOLEAN;
    var$sal_des_oid_client   gepr_pcomon_###VERSION###.tipo$oid_;
    var$sal_des_oid_subcli   gepr_pcomon_###VERSION###.tipo$oid_;
    var$sal_des_oid_ptserv   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_des_oid_client   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_des_cod_client   gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_des_oid_subcli   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_des_cod_subcli   gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_des_oid_ptserv   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_des_cod_ptserv   gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_des_oid_canal    gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_des_cod_canal    gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_des_oid_subcan   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_des_cod_subcan   gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_ori_oid_client   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_ori_cod_client   gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_ori_oid_subcli   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_ori_cod_subcli   gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_ori_oid_ptserv   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_ori_cod_ptserv   gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_ori_oid_canal    gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_ori_cod_canal    gepr_pcomon_###VERSION###.tipo$cod_;
    var$mov_ori_oid_subcan   gepr_pcomon_###VERSION###.tipo$oid_;
    var$mov_ori_cod_subcan   gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante      gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_remesa           gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_bulto            gepr_pcomon_###VERSION###.tipo$oid_;

    /* varibles para reenvio */
    var$oid_grupo_documento       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_grupo_documento_padre gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_formulario_grupo      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_origen         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_destino        gepr_pcomon_###VERSION###.tipo$oid_;
    var$rowver                    gepr_pcomon_###VERSION###.tipo$nel_;
    var$index                     gepr_pcomon_###VERSION###.tipo$nel_;
    var$oid_documento             gepr_pcomon_###VERSION###.tipo$oid_;

    /* arrays de terminos del grupo de documentos */
    var$avtgdoc_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtgdoc_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;

    /* arrays de documentos */
    var$adocs_oid            gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_doc_padre  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_sustituto  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_mov_fondos gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_fyh_plncertif  gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_fyh_gestion    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_cod_externo    gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_rowver         gepr_pcomon_###VERSION###.tipo$nels_;

    /* cuenta origen */
    var$adocs_mov_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_oid_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_client gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_subcli gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_ptserv gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_canal  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_canal  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_subcan gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_subcan gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_delega gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_delega gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_planta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_planta gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_sector gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;

    /* cuenta destino */
    var$adocs_mov_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_client gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_subcli gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_ptserv gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_canal  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_canal  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_subcan gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_subcan gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_delega gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_delega gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_planta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_planta gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_sector gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_oid_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_cod_client gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_des_oid_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_cod_subcli gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_des_oid_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_cod_ptserv gepr_pcomon_###VERSION###.tipo$cods_;

    /* arrays de efectivo por documento */
    var$aefdoc_oid_documento    gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_divisa       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_denominacion gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_unid_medida  gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_cod_niv_detalle  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_cod_tp_efec_tot  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_oid_calidad      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_num_importe      gepr_pcomon_###VERSION###.tipo$imps_;
    var$aefdoc_nel_cantidad     gepr_pcomon_###VERSION###.tipo$nels_;

    /* arrays de medio de pago por documento */
    var$ampdoc_oid_documento     gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_oid_divisa        gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_oid_medio_pago    gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_cod_tipo_med_pago gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_cod_nivel_detalle gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_num_importe       gepr_pcomon_###VERSION###.tipo$imps_;
    var$ampdoc_nel_cantidad      gepr_pcomon_###VERSION###.tipo$nels_;

    /* arrays de terminos de medio de pago por documento */
    var$avtmpdoc_oid_documento   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_oid_t_mediopago gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_des_valor       gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtmpdoc_nec_indice_grp  gepr_pcomon_###VERSION###.tipo$ints_;

    /* arrays de terminos por documento */
    var$avtdoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_oid_termino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_des_valor     gepr_pcomon_###VERSION###.tipo$descs_;

    /* Remesa - arrays */
    var$aremdoc_oid_remesa    gepr_pcomon_###VERSION###.tipo$oids_;
    var$aremdoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$aremdoc_rowver        gepr_pcomon_###VERSION###.tipo$nels_;
    var$aremdoc_rowver_aux    gepr_pcomon_###VERSION###.tipo$nels_;

    /* Bulto - arrays */
    var$abuldoc_oid_bulto     gepr_pcomon_###VERSION###.tipo$oids_;
    var$abuldoc_oid_remesa    gepr_pcomon_###VERSION###.tipo$oids_;
    var$abuldoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$abuldoc_rowver        gepr_pcomon_###VERSION###.tipo$nels_;
    var$abuldoc_rowver_aux    gepr_pcomon_###VERSION###.tipo$nels_;

    var$rowver_elemento       gepr_pcomon_###VERSION###.tipo$nel_;
    var$utiliza_codigo        gepr_pcomon_###VERSION###.tipo$nel_ := 0;

    /* CURSOR DE DOCUMENTOS PADRES */
    CURSOR cur$documentos_padres IS
      SELECT D.OID_DOCUMENTO,
             D.COD_EXTERNO,
             D.OID_CUENTA_DESTINO,
             D.OID_CUENTA_SALDO_DESTINO,
             D.OID_SECTOR_DESTINO,
             DE.OID_REMESA,
             R.COD_EXTERNO AS COD_EXTERNO_REMESA,
             R.ROWVER AS ROWVER_REMESA,
             DE.OID_BULTO,
             B.COD_PRECINTO,
             B.ROWVER AS ROWVER_BULTO
        FROM SAPR_TDOCUMENTOXELEMENTO DE
       INNER JOIN SAPR_GTT_TOIDS T
          ON T.OID = DE.OID_DOCUMENTOXELEMENTO
       INNER JOIN SAPR_TDOCUMENTO D
          ON D.OID_DOCUMENTO = DE.OID_DOCUMENTO
       INNER JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DE.OID_REMESA
       LEFT JOIN SAPR_TBULTO B
          ON B.OID_BULTO = DE.OID_BULTO
       WHERE T.COD_CALIFICADOR = 'DOCXELE';

    /* CURSOR DE TERMINOS DOCUMENTOS */
    CURSOR cur$terminos(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TD.DES_VALOR, TD.OID_DOCUMENTO, T.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TVALOR_TERMINOXDOCUMENTO TD
       INNER JOIN GEPR_TTERMINO T
          ON TD.OID_TERMINO = T.OID_TERMINO
       WHERE TD.OID_DOCUMENTO = par$oid_documento;

    /* CURSOR DE EFECTIVOS DOCUMENTOS */
    CURSOR cur$efectivos(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT ED.OID_DOCUMENTO,
             ED.OID_DIVISA,
             ED.OID_DENOMINACION,
             ED.COD_NIVEL_DETALLE,
             ED.OID_UNIDAD_MEDIDA,
             ED.OID_CALIDAD,
             ED.COD_TIPO_EFECTIVO_TOTAL,
             SUM(ED.NUM_IMPORTE) AS IMPORTE,
             SUM(ED.NEL_CANTIDAD) AS CANTIDAD
        FROM SAPR_TEFECTIVOXDOCUMENTO ED
       WHERE ED.OID_DOCUMENTO = par$oid_documento
       GROUP BY ED.OID_DOCUMENTO,
                ED.OID_DIVISA,
                ED.OID_DENOMINACION,
                ED.COD_NIVEL_DETALLE,
                ED.OID_UNIDAD_MEDIDA,
                ED.OID_CALIDAD,
                ED.COD_TIPO_EFECTIVO_TOTAL;

	/* CURSOR DE EFECTIVOS BULTO */
    CURSOR cur$efectivos_bulto(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT       DE.OID_DIVISA,
             DE.OID_DENOMINACION,
             DE.COD_NIVEL_DETALLE,
             DE.OID_UNIDAD_MEDIDA,
             DE.COD_TIPO_EFECTIVO_TOTAL,
             SUM(DE.NUM_IMPORTE) AS IMPORTE,
             SUM(DE.NEL_CANTIDAD) AS CANTIDAD
       FROM SAPR_TDECLARADO_EFECTIVO DE
       WHERE DE.OID_BULTO = par$oid_bulto AND DE.OID_PARCIAL IS NULL
       GROUP BY DE.OID_DOCUMENTO,
                DE.OID_DIVISA,
                DE.OID_DENOMINACION,
                DE.COD_NIVEL_DETALLE,
                DE.OID_UNIDAD_MEDIDA,
                DE.COD_TIPO_EFECTIVO_TOTAL;

    /* CURSOR DE MEDIO_PAGO DOCUMENTOS */
    CURSOR cur$mediopagos(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT MP.OID_DOCUMENTO,
             MP.OID_DIVISA,
             MP.OID_MEDIO_PAGO,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_NIVEL_DETALLE,
             SUM(MP.NUM_IMPORTE) AS IMPORTE,
             SUM(MP.NEL_CANTIDAD) AS CANTIDAD
        FROM SAPR_TMEDIO_PAGOXDOCUMENTO MP
       WHERE MP.OID_DOCUMENTO = par$oid_documento
       GROUP BY MP.OID_DOCUMENTO,
                MP.OID_DIVISA,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.OID_MEDIO_PAGO,
                MP.COD_NIVEL_DETALLE;

    /* CURSOR DE MEDIO_PAGO DOCUMENTOS */
    CURSOR cur$terminosmp(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT PD.OID_DOCUMENTO,
             OID_MEDIO_PAGO,
             TMP.DES_VALOR,
             TMP.NEC_INDICE_GRUPO
        FROM SAPR_TVALOR_TERMINO_MEDIO_PAGO TMP
       INNER JOIN SAPR_TMEDIO_PAGOXDOCUMENTO PD
          ON TMP.OID_MEDIO_PAGOXDOCUMENTO = PD.OID_MEDIO_PAGOXDOCUMENTO
       WHERE PD.OID_DOCUMENTO = PAR$OID_DOCUMENTO
       ORDER BY PD.OID_DOCUMENTO, TMP.NEC_INDICE_GRUPO;

	/* CURSOR DE BULTOS */
    CURSOR cur$bultos_por_remesa (par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT B.OID_BULTO, B.OID_REMESA, B.ROWVER
        FROM SAPR_TBULTO B
        INNER JOIN SAPR_GTT_TOIDS T
          ON T.OID = B.OID_BULTO
        WHERE B.OID_REMESA = par$oid_remesa
        AND T.COD_CALIFICADOR='OID_BULTO';
  BEGIN

    /* Valores iniciales */
    var$inicio := SYSTIMESTAMP;

    /* limpiar tabla temporaria */
    delete sapr_gtt_toids;
    var$deletes   := var$deletes + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;

    /* ===== Cargar tabla temporaria ==== */
    /* - Documentos y Documentos Padres */
    IF par$bol_gestion_bulto = 0 THEN

      /* Gestion de remesa */
      FOR idx IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP

        IF par$aremdoc_oid_remesa(idx) IS NOT NULL THEN

          var$aremdoc_rowver_aux(idx) := par$aremdoc_rowver(idx);

          /*atualiza o campo rowver da remesa, para que outro reenvio não seja criado*/
          sapr_premesa_###VERSION###.supd_tremesa_rowver(par$oid_remesa        => par$aremdoc_oid_remesa(idx),
                                                    par$rowver            => var$aremdoc_rowver_aux(idx),
                                                    par$cod_cultura       => par$cod_cultura,
                                                    par$cod_funcionalidad => 'reenvio');

          INSERT INTO SAPR_GTT_TOIDS
            SELECT DE.OID_DOCUMENTOXELEMENTO, 'DOCXELE' COD_CALIFICADOR
              FROM SAPR_TDOCUMENTOXELEMENTO DE
             INNER JOIN (SELECT R.OID_REMESA
                           FROM SAPR_TREMESA R
                          START WITH R.OID_REMESA =
                                     par$aremdoc_oid_remesa(idx)
                         CONNECT BY NOCYCLE
                          PRIOR R.OID_REMESA = R.OID_REMESA_PADRE) R
                ON R.OID_REMESA = DE.OID_REMESA
             INNER JOIN SAPR_TBULTO B
                ON B.OID_REMESA = R.OID_REMESA
             WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
               AND B.COD_ESTADO <> gepr_pcomon_###VERSION###.const$codEstadoBultoSalida
               AND DE.OID_BULTO IS NULL
               GROUP BY DE.OID_DOCUMENTOXELEMENTO;
          var$inserts   := var$inserts + sql%ROWCOUNT;
          var$registros := var$registros + sql%ROWCOUNT;

        ELSE

          BEGIN
            SELECT R.OID_REMESA, R.ROWVER
              INTO var$oid_remesa, var$rowver_elemento
              FROM SAPR_TREMESA R
             INNER JOIN SAPR_TDOCUMENTOXELEMENTO DE
                ON DE.OID_REMESA = R.OID_REMESA
             INNER JOIN SAPR_TBULTO B
                ON B.OID_REMESA = R.OID_REMESA
             WHERE R.COD_EXTERNO = par$aremdoc_cod_remesa(idx)
               AND DE.COD_ESTADO_DOCXELEMENTO = 'C'
               AND DE.OID_BULTO IS NULL
               AND B.COD_ESTADO <> gepr_pcomon_###VERSION###.const$codEstadoBultoSalida
               GROUP BY R.OID_REMESA, R.ROWVER;

          EXCEPTION
            WHEN no_data_found THEN
              raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'codigo_externo_invalido',
                                                                        const$codFuncionalidad || '.' ||
                                                                        const$nombre_func,
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                        NULL,
                                                                        1));
          END;

         var$aremdoc_rowver_aux(idx) := var$rowver_elemento;
          /*atualiza o campo rowver da remesa, para que outro reenvio não seja criado*/
          sapr_premesa_###VERSION###.supd_tremesa_rowver(par$oid_remesa        => var$oid_remesa,
                                                    par$rowver            => var$aremdoc_rowver_aux(idx),
                                                    par$cod_cultura       => par$cod_cultura,
                                                    par$cod_funcionalidad => 'reenvio');

          INSERT INTO SAPR_GTT_TOIDS
            SELECT DE.OID_DOCUMENTOXELEMENTO, 'DOCXELE' COD_CALIFICADOR
              FROM SAPR_TDOCUMENTOXELEMENTO DE
             INNER JOIN (SELECT R.OID_REMESA
                           FROM SAPR_TREMESA R
                          START WITH R.OID_REMESA =
                                     var$oid_remesa
                         CONNECT BY NOCYCLE
                          PRIOR R.OID_REMESA = R.OID_REMESA_PADRE) R
                ON R.OID_REMESA = DE.OID_REMESA
             INNER JOIN SAPR_TBULTO B
                ON B.OID_REMESA = R.OID_REMESA
             WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
               AND B.COD_ESTADO <> gepr_pcomon_###VERSION###.const$codEstadoBultoSalida
               AND DE.OID_BULTO IS NULL
               GROUP BY DE.OID_DOCUMENTOXELEMENTO;
          var$inserts   := var$inserts + sql%ROWCOUNT;
          var$registros := var$registros + sql%ROWCOUNT;

          var$utiliza_codigo := 1;

        END IF;

      END LOOP;

	  /*insere os oids dos bultos informados, para depois recupera-los atraves da remesa*/
	  FOR idx IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP

        IF par$abuldoc_oid_bulto(idx) IS NOT NULL THEN
          INSERT INTO SAPR_GTT_TOIDS(OID,COD_CALIFICADOR)
          VALUES(par$abuldoc_oid_bulto(idx),'OID_BULTO');

          var$inserts   := var$inserts + 1;
          var$registros := var$registros + 1;
        END IF;
      END LOOP;
    ELSE

      /* Gestion de Bultos */
      FOR idx IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP

        IF par$abuldoc_oid_bulto(idx) IS NOT NULL THEN

          var$abuldoc_rowver_aux(idx) := par$abuldoc_rowver(idx);
          /*atualiza o campo rowver do bulto, para que outro reenvio não seja criado*/
          sapr_pbulto_###VERSION###.supd_tbulto_rowver(par$oid_bulto         => par$abuldoc_oid_bulto(idx),
                                                  par$rowver            => var$abuldoc_rowver_aux(idx),
                                                  par$cod_cultura       => par$cod_cultura,
                                                  par$cod_funcionalidad => 'reenvio');

          INSERT INTO SAPR_GTT_TOIDS
            SELECT DE.OID_DOCUMENTOXELEMENTO, 'DOCXELE' COD_CALIFICADOR
              FROM SAPR_TDOCUMENTOXELEMENTO DE
             INNER JOIN (SELECT B.OID_BULTO, B.COD_ESTADO
                           FROM SAPR_TBULTO B
                          START WITH B.OID_BULTO =
                                     par$abuldoc_oid_bulto(idx)
                         CONNECT BY NOCYCLE
                          PRIOR B.OID_BULTO = B.OID_BULTO_PADRE) B
                ON B.OID_BULTO = DE.OID_BULTO
             WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
               AND B.COD_ESTADO <> gepr_pcomon_###VERSION###.const$codEstadoBultoSalida
               AND DE.OID_BULTO IS NOT NULL
               GROUP BY DE.OID_DOCUMENTOXELEMENTO;
          var$inserts   := var$inserts + sql%ROWCOUNT;
          var$registros := var$registros + sql%ROWCOUNT;

        ELSE

          BEGIN
            SELECT B.OID_BULTO, B.Rowver
              INTO var$oid_bulto, var$rowver_elemento
              FROM SAPR_TBULTO B
             INNER JOIN SAPR_TDOCUMENTOXELEMENTO DE
                ON DE.OID_REMESA = B.OID_BULTO
             WHERE B.COD_PRECINTO = par$abuldoc_cod_bulto(idx)
               AND DE.COD_ESTADO_DOCXELEMENTO = 'C'
               AND DE.OID_BULTO IS NOT NULL
               AND B.COD_ESTADO <> gepr_pcomon_###VERSION###.const$codEstadoBultoSalida
               GROUP BY DE.OID_BULTO;

          EXCEPTION
            WHEN no_data_found THEN
              raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'codigo_externo_invalido',
                                                                        const$codFuncionalidad || '.' ||
                                                                        const$nombre_func,
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                        NULL,
                                                                        1));
          END;

          var$abuldoc_rowver_aux(idx) := var$rowver_elemento;
          /*atualiza o campo rowver do bulto, para que outro reenvio não seja criado*/
          sapr_pbulto_###VERSION###.supd_tbulto_rowver(par$oid_bulto         => var$oid_bulto,
                                                  par$rowver            => var$abuldoc_rowver_aux(idx),
                                                  par$cod_cultura       => par$cod_cultura,
                                                  par$cod_funcionalidad => 'reenvio');

          INSERT INTO SAPR_GTT_TOIDS
            SELECT DE.OID_DOCUMENTOXELEMENTO, 'DOCXELE' COD_CALIFICADOR
              FROM SAPR_TDOCUMENTOXELEMENTO DE
             INNER JOIN (SELECT B.OID_BULTO, B.COD_ESTADO
                           FROM SAPR_TBULTO B
                          START WITH B.OID_BULTO =
                                     var$oid_bulto
                         CONNECT BY NOCYCLE
                          PRIOR B.OID_BULTO = B.OID_BULTO_PADRE) B
                ON B.OID_BULTO = DE.OID_BULTO
             WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
               AND B.COD_ESTADO <> gepr_pcomon_###VERSION###.const$codEstadoBultoSalida
               AND DE.OID_BULTO IS NOT NULL
               GROUP BY DE.OID_DOCUMENTOXELEMENTO;
          var$inserts   := var$inserts + sql%ROWCOUNT;
          var$registros := var$registros + sql%ROWCOUNT;

          var$utiliza_codigo := 1;

        END IF;

      END LOOP;

    END IF;

    /* recuperar formulario de reenvio */
    var$bol_gestion_bulto := par$bol_gestion_bulto;
    sapr_pformulario_###VERSION###.srecuperar_form_reenvio_v2(par$oid_formulario    => var$oid_formulario_grupo,
                                                         par$bol_gestion_bulto => var$bol_gestion_bulto,
                                                         par$cod_integracion   => par$cod_integracion,
                                                         par$cod_cultura       => par$cod_cultura);

    /* Cargar variables */
    FOR rec$documento_padre IN cur$documentos_padres LOOP

      var$oid_documento := sys_guid();
      /* arrays de documentos */
      var$adocs_oid(var$adocs_oid.count) := var$oid_documento;
      var$adocs_oid_doc_padre(var$adocs_oid_doc_padre.count) := rec$documento_padre.oid_documento;
      var$adocs_oid_sustituto(var$adocs_oid_sustituto.count) := NULL;
      var$adocs_oid_mov_fondos(var$adocs_oid_mov_fondos.count) := NULL;
      var$adocs_fyh_plncertif(var$adocs_fyh_plncertif.count) := SYSDATE;
      var$adocs_fyh_gestion(var$adocs_fyh_gestion.count) := SYSDATE;
      var$adocs_cod_externo(var$adocs_cod_externo.count) := rec$documento_padre.cod_externo;
      var$adocs_rowver(var$adocs_rowver.count) := NULL;

      /* cuenta origan */
      var$adocs_mov_ori_oid_client(var$adocs_mov_ori_oid_client.count) := NULL;
      var$adocs_mov_ori_cod_client(var$adocs_mov_ori_cod_client.count) := NULL;
      var$adocs_mov_ori_oid_subcli(var$adocs_mov_ori_oid_subcli.count) := NULL;
      var$adocs_mov_ori_cod_subcli(var$adocs_mov_ori_cod_subcli.count) := NULL;
      var$adocs_mov_ori_oid_ptserv(var$adocs_mov_ori_oid_ptserv.count) := NULL;
      var$adocs_mov_ori_cod_ptserv(var$adocs_mov_ori_cod_ptserv.count) := NULL;
      var$adocs_mov_ori_oid_canal(var$adocs_mov_ori_oid_canal.count) := NULL;
      var$adocs_mov_ori_cod_canal(var$adocs_mov_ori_cod_canal.count) := NULL;
      var$adocs_mov_ori_oid_subcan(var$adocs_mov_ori_oid_subcan.count) := NULL;
      var$adocs_mov_ori_cod_subcan(var$adocs_mov_ori_cod_subcan.count) := NULL;
      var$adocs_mov_ori_oid_delega(var$adocs_mov_ori_oid_delega.count) := NULL;
      var$adocs_mov_ori_cod_delega(var$adocs_mov_ori_cod_delega.count) := NULL;
      var$adocs_mov_ori_oid_planta(var$adocs_mov_ori_oid_planta.count) := NULL;
      var$adocs_mov_ori_cod_planta(var$adocs_mov_ori_cod_planta.count) := NULL;
      var$adocs_mov_ori_cod_sector(var$adocs_mov_ori_cod_sector.count) := NULL;

      /* cuenta destino */
      var$adocs_mov_des_oid_client(var$adocs_mov_des_oid_client.count) := NULL;
      var$adocs_mov_des_cod_client(var$adocs_mov_des_cod_client.count) := NULL;
      var$adocs_mov_des_oid_subcli(var$adocs_mov_des_oid_subcli.count) := NULL;
      var$adocs_mov_des_cod_subcli(var$adocs_mov_des_cod_subcli.count) := NULL;
      var$adocs_mov_des_oid_ptserv(var$adocs_mov_des_oid_ptserv.count) := NULL;
      var$adocs_mov_des_cod_ptserv(var$adocs_mov_des_cod_ptserv.count) := NULL;
      var$adocs_mov_des_oid_canal(var$adocs_mov_des_oid_canal.count) := NULL;
      var$adocs_mov_des_cod_canal(var$adocs_mov_des_cod_canal.count) := NULL;
      var$adocs_mov_des_oid_subcan(var$adocs_mov_des_oid_subcan.count) := NULL;
      var$adocs_mov_des_cod_subcan(var$adocs_mov_des_cod_subcan.count) := NULL;
      var$adocs_mov_des_oid_delega(var$adocs_mov_des_oid_delega.count) := NULL;
      var$adocs_mov_des_cod_delega(var$adocs_mov_des_cod_delega.count) := NULL;
      var$adocs_mov_des_oid_planta(var$adocs_mov_des_oid_planta.count) := NULL;
      var$adocs_mov_des_cod_planta(var$adocs_mov_des_cod_planta.count) := NULL;
      var$adocs_mov_des_cod_sector(var$adocs_mov_des_cod_sector.count) := NULL;
      var$adocs_sal_des_oid_client(var$adocs_sal_des_oid_client.count) := NULL;
      var$adocs_sal_des_cod_client(var$adocs_sal_des_cod_client.count) := NULL;
      var$adocs_sal_des_oid_subcli(var$adocs_sal_des_oid_subcli.count) := NULL;
      var$adocs_sal_des_cod_subcli(var$adocs_sal_des_cod_subcli.count) := NULL;
      var$adocs_sal_des_oid_ptserv(var$adocs_sal_des_oid_ptserv.count) := NULL;
      var$adocs_sal_des_cod_ptserv(var$adocs_sal_des_cod_ptserv.count) := NULL;

      SELECT C.OID_CLIENTE, C.OID_SUBCLIENTE, C.OID_PTO_SERVICIO
        INTO var$sal_des_oid_client,
             var$sal_des_oid_subcli,
             var$sal_des_oid_ptserv
        FROM SAPR_TCUENTA C
       WHERE C.OID_CUENTA = rec$documento_padre.oid_cuenta_saldo_destino;

      SELECT C.OID_CLIENTE,
             C.OID_SUBCLIENTE,
             C.OID_PTO_SERVICIO,
             C.OID_SUBCANAL,
             SC.OID_CANAL,
             C.OID_CLIENTE,
             C.OID_SUBCLIENTE,
             C.OID_PTO_SERVICIO,
             C.OID_SUBCANAL,
             SC.OID_CANAL
        INTO var$mov_des_oid_client,
             var$mov_des_oid_subcli,
             var$mov_des_oid_ptserv,
             var$mov_des_oid_subcan,
             var$mov_des_oid_canal,
             var$mov_ori_oid_client,
             var$mov_ori_oid_subcli,
             var$mov_ori_oid_ptserv,
             var$mov_ori_oid_subcan,
             var$mov_ori_oid_canal
        FROM SAPR_TCUENTA C
       INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
       WHERE C.OID_CUENTA = rec$documento_padre.oid_cuenta_destino;


       IF par$bol_gestion_bulto = 0 THEN

          /* Gestion de remesa */
          IF var$utiliza_codigo = 0 THEN

            FOR idxr IN par$aremdoc_oid_remesa.first .. par$aremdoc_oid_remesa.last LOOP
              IF rec$documento_padre.oid_remesa = par$aremdoc_oid_remesa(idxr) THEN
                var$index := idxr;
                EXIT;
              END IF;
            END LOOP;

          ELSE

            FOR idxr IN par$aremdoc_cod_remesa.first .. par$aremdoc_cod_remesa.last LOOP
              IF rec$documento_padre.COD_EXTERNO_REMESA = par$aremdoc_cod_remesa(idxr) THEN
                var$index := idxr;
                EXIT;
              END IF;
            END LOOP;

          END IF;


       ELSE

          /* Gestion de bulto */
          IF var$utiliza_codigo = 0 THEN

            FOR idxb IN par$abuldoc_oid_bulto.first .. par$abuldoc_oid_bulto.last LOOP
              IF rec$documento_padre.oid_bulto = par$abuldoc_oid_bulto(idxb) THEN
                var$index := idxb;
                EXIT;
              END IF;
            END LOOP;

          ELSE

            FOR idxb IN par$abuldoc_cod_bulto.first .. par$abuldoc_cod_bulto.last LOOP
              IF rec$documento_padre.cod_precinto = par$abuldoc_cod_bulto(idxb) THEN
                var$index := idxb;
                EXIT;
              END IF;
            END LOOP;

          END IF;

       END IF;

      IF par$adocs_mov_des_cod_client(var$index) IS NOT NULL OR
         par$adocs_mov_des_oid_client(var$index) IS NOT NULL THEN
        var$mov_des_oid_client := par$adocs_mov_des_oid_client(var$index);
        var$mov_des_cod_client := par$adocs_mov_des_cod_client(var$index);
      END IF;

      IF par$adocs_mov_des_cod_subcli(var$index) IS NOT NULL OR
         par$adocs_mov_des_oid_subcli(var$index) IS NOT NULL THEN
        var$mov_des_oid_subcli := par$adocs_mov_des_oid_subcli(var$index);
        var$mov_des_cod_subcli := par$adocs_mov_des_cod_subcli(var$index);
      END IF;

      IF par$adocs_mov_des_cod_ptserv(var$index) IS NOT NULL OR
         par$adocs_mov_des_oid_ptserv(var$index) IS NOT NULL THEN
        var$mov_des_oid_ptserv := par$adocs_mov_des_oid_ptserv(var$index);
        var$mov_des_cod_ptserv := par$adocs_mov_des_cod_ptserv(var$index);
      END IF;

      IF par$adocs_mov_des_oid_canal(var$index) IS NOT NULL OR
         par$adocs_mov_des_cod_canal(var$index) IS NOT NULL THEN
        var$mov_des_oid_canal := par$adocs_mov_des_oid_canal(var$index);
        var$mov_des_cod_canal := par$adocs_mov_des_cod_canal(var$index);
      END IF;

      IF par$adocs_mov_des_oid_subcan(var$index) IS NOT NULL OR
         par$adocs_mov_des_cod_subcan(var$index) IS NOT NULL THEN
        var$mov_des_oid_subcan := par$adocs_mov_des_oid_subcan(var$index);
        var$mov_des_cod_subcan := par$adocs_mov_des_cod_subcan(var$index);
      END IF;


      IF par$adocs_mov_ori_oid_client(var$index) IS NOT NULL OR
         par$adocs_mov_ori_cod_client(var$index) IS NOT NULL THEN
        var$mov_ori_oid_client := par$adocs_mov_ori_oid_client(var$index);
        var$mov_ori_cod_client := par$adocs_mov_ori_cod_client(var$index);
      END IF;

      IF par$adocs_mov_ori_oid_subcli(var$index) IS NOT NULL OR
         par$adocs_mov_ori_cod_subcli(var$index) IS NOT NULL THEN
        var$mov_ori_oid_subcli := par$adocs_mov_ori_oid_subcli(var$index);
        var$mov_ori_cod_subcli := par$adocs_mov_ori_cod_subcli(var$index);
      END IF;

      IF par$adocs_mov_ori_oid_ptserv(var$index) IS NOT NULL OR
         par$adocs_mov_ori_cod_ptserv(var$index) IS NOT NULL THEN
        var$mov_ori_oid_ptserv := par$adocs_mov_ori_oid_ptserv(var$index);
        var$mov_ori_cod_ptserv := par$adocs_mov_ori_cod_ptserv(var$index);
      END IF;

      IF par$adocs_mov_ori_oid_canal(var$index) IS NOT NULL OR
         par$adocs_mov_ori_cod_canal(var$index) IS NOT NULL THEN
        var$mov_ori_oid_canal := par$adocs_mov_ori_oid_canal(var$index);
        var$mov_ori_cod_canal := par$adocs_mov_ori_cod_canal(var$index);
      END IF;

      IF par$adocs_mov_ori_oid_subcan(var$index) IS NOT NULL OR
         par$adocs_mov_ori_cod_subcan(var$index) IS NOT NULL THEN
        var$mov_ori_oid_subcan := par$adocs_mov_ori_oid_subcan(var$index);
        var$mov_ori_cod_subcan := par$adocs_mov_ori_cod_subcan(var$index);
      END IF;

      sapr_pdocumento_###VERSION###.svalidar_cuentas(par$oid_formulario          => var$oid_formulario_grupo,
                                                par$mov_ori_oid_cuenta      => par$adocs_mov_ori_oid_cuenta(var$index),
                                                par$mov_ori_oid_client      => var$mov_ori_oid_client,
                                                par$mov_ori_cod_client      => var$mov_ori_cod_client,
                                                par$mov_ori_oid_subcli      => var$mov_ori_oid_subcli,
                                                par$mov_ori_cod_subcli      => var$mov_ori_cod_subcli,
                                                par$mov_ori_oid_ptserv      => var$mov_ori_oid_ptserv,
                                                par$mov_ori_cod_ptserv      => var$mov_ori_cod_ptserv,
                                                par$mov_ori_oid_canal       => var$mov_ori_oid_canal,
                                                par$mov_ori_cod_canal       => var$mov_ori_cod_canal,
                                                par$mov_ori_oid_subcan      => var$mov_ori_oid_subcan,
                                                par$mov_ori_cod_subcan      => var$mov_ori_cod_subcan,
                                                par$mov_ori_oid_delega      => par$adocs_mov_ori_oid_delega(var$index),
                                                par$mov_ori_cod_delega      => par$adocs_mov_ori_cod_delega(var$index),
                                                par$mov_ori_oid_planta      => par$adocs_mov_ori_oid_planta(var$index),
                                                par$mov_ori_cod_planta      => par$adocs_mov_ori_cod_planta(var$index),
                                                par$mov_ori_oid_sector      => par$adocs_mov_ori_oid_sector(var$index),
                                                par$mov_ori_cod_sector      => par$adocs_mov_ori_cod_sector(var$index),
                                                par$sal_ori_oid_cuenta      => rec$documento_padre.oid_cuenta_saldo_destino,
                                                par$sal_ori_oid_client      => NULL,
                                                par$sal_ori_cod_client      => NULL,
                                                par$sal_ori_oid_subcli      => NULL,
                                                par$sal_ori_cod_subcli      => NULL,
                                                par$sal_ori_oid_ptserv      => NULL,
                                                par$sal_ori_cod_ptserv      => NULL,
                                                par$mov_des_oid_cuenta      => par$adocs_mov_des_oid_cuenta(var$index),
                                                par$mov_des_oid_client      => var$mov_des_oid_client,
                                                par$mov_des_cod_client      => var$mov_des_cod_client,
                                                par$mov_des_oid_subcli      => var$mov_des_oid_subcli,
                                                par$mov_des_cod_subcli      => var$mov_des_cod_subcli,
                                                par$mov_des_oid_ptserv      => var$mov_des_oid_ptserv,
                                                par$mov_des_cod_ptserv      => var$mov_des_cod_ptserv,
                                                par$mov_des_oid_canal       => var$mov_des_oid_canal,
                                                par$mov_des_cod_canal       => var$mov_des_cod_canal,
                                                par$mov_des_oid_subcan      => var$mov_des_oid_subcan,
                                                par$mov_des_cod_subcan      => var$mov_des_cod_subcan,
                                                par$mov_des_oid_delega      => par$adocs_mov_des_oid_delega(var$index),
                                                par$mov_des_cod_delega      => par$adocs_mov_des_cod_delega(var$index),
                                                par$mov_des_oid_planta      => par$adocs_mov_des_oid_planta(var$index),
                                                par$mov_des_cod_planta      => par$adocs_mov_des_cod_planta(var$index),
                                                par$mov_des_oid_sector      => par$adocs_mov_des_oid_sector(var$index),
                                                par$mov_des_cod_sector      => par$adocs_mov_des_cod_sector(var$index),
                                                par$sal_des_oid_cuenta      => NULL,
                                                par$sal_des_oid_client      => var$sal_des_oid_client,
                                                par$sal_des_cod_client      => NULL,
                                                par$sal_des_oid_subcli      => var$sal_des_oid_subcli,
                                                par$sal_des_cod_subcli      => NULL,
                                                par$sal_des_oid_ptserv      => var$sal_des_oid_ptserv,
                                                par$sal_des_cod_ptserv      => NULL,
                                                par$mov_ori_oid_cuenta_resp => var$adocs_mov_ori_oid_cuenta(var$adocs_mov_ori_oid_cuenta.count),
                                                par$mov_ori_oid_sector_resp => var$adocs_mov_ori_oid_sector(var$adocs_mov_ori_oid_sector.count),
                                                par$sal_ori_oid_cuenta_resp => var$adocs_sal_ori_oid_cuenta(var$adocs_sal_ori_oid_cuenta.count),
                                                par$mov_des_oid_cuenta_resp => var$adocs_mov_des_oid_cuenta(var$adocs_mov_des_oid_cuenta.count),
                                                par$mov_des_oid_sector_resp => var$adocs_mov_des_oid_sector(var$adocs_mov_des_oid_sector.count),
                                                par$sal_des_oid_cuenta_resp => var$adocs_sal_des_oid_cuenta(var$adocs_sal_des_oid_cuenta.count),
                                                par$cod_cultura             => par$cod_cultura,
                                                par$cod_usuario             => par$usuario,
                                                par$inserts                 => var$inserts,
                                                par$updates                 => var$updates,
                                                par$selects                 => var$selects,
                                                par$esreenvio               => var$esreenvio);

     IF rec$documento_padre.oid_cuenta_destino <>
         var$adocs_mov_ori_oid_cuenta(var$adocs_mov_ori_oid_cuenta.count - 1) THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'origin_invalida',
                                                                    const$codFuncionalidad || '.' ||
                                                                    const$nombre_func,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
      END IF;

      /* arrays de efectivo por documento */
      IF par$bol_gestion_bulto = 0 THEN
		  FOR rec$efectivo IN cur$efectivos(rec$documento_padre.oid_documento) LOOP
			var$aefdoc_oid_documento(var$aefdoc_oid_documento.COUNT) := var$oid_documento;
			var$aefdoc_oid_divisa(var$aefdoc_oid_divisa.COUNT) := rec$efectivo.oid_divisa;
			var$aefdoc_oid_denominacion(var$aefdoc_oid_denominacion.COUNT) := rec$efectivo.oid_denominacion;
			var$aefdoc_oid_unid_medida(var$aefdoc_oid_unid_medida.COUNT) := rec$efectivo.oid_unidad_medida;
			var$aefdoc_cod_niv_detalle(var$aefdoc_cod_niv_detalle.COUNT) := rec$efectivo.cod_nivel_detalle;
			var$aefdoc_cod_tp_efec_tot(var$aefdoc_cod_tp_efec_tot.COUNT) := rec$efectivo.COD_TIPO_EFECTIVO_TOTAL;
			var$aefdoc_oid_calidad(var$aefdoc_oid_calidad.COUNT) := rec$efectivo.oid_calidad;
			var$aefdoc_num_importe(var$aefdoc_num_importe.COUNT) := rec$efectivo.importe;
			var$aefdoc_nel_cantidad(var$aefdoc_nel_cantidad.COUNT) := rec$efectivo.cantidad;
		  END LOOP rec$efectivo;

      ELSE
         FOR rec$efectivo_bulto IN cur$efectivos_bulto(rec$documento_padre.oid_BULTO) LOOP
            var$aefdoc_oid_documento(var$aefdoc_oid_documento.COUNT) := var$oid_documento;
            var$aefdoc_oid_divisa(var$aefdoc_oid_divisa.COUNT) := rec$efectivo_bulto.oid_divisa;
            var$aefdoc_oid_denominacion(var$aefdoc_oid_denominacion.COUNT) := rec$efectivo_bulto.oid_denominacion;
            var$aefdoc_oid_unid_medida(var$aefdoc_oid_unid_medida.COUNT) := rec$efectivo_bulto.oid_unidad_medida;
            var$aefdoc_cod_niv_detalle(var$aefdoc_cod_niv_detalle.COUNT) := rec$efectivo_bulto.cod_nivel_detalle;
            var$aefdoc_cod_tp_efec_tot(var$aefdoc_cod_tp_efec_tot.COUNT) := '';
            var$aefdoc_oid_calidad(var$aefdoc_oid_calidad.COUNT) := '';
            var$aefdoc_num_importe(var$aefdoc_num_importe.COUNT) := rec$efectivo_bulto.importe;
            var$aefdoc_nel_cantidad(var$aefdoc_nel_cantidad.COUNT) := rec$efectivo_bulto.cantidad;
         END LOOP rec$efectivo_bulto;
      END IF;

      /* arrays de medio de pago por documento */
      FOR rec$mediopago IN cur$mediopagos(rec$documento_padre.oid_documento) LOOP
        var$ampdoc_oid_documento(var$ampdoc_oid_documento.COUNT) := var$oid_documento;
        var$ampdoc_oid_divisa(var$ampdoc_oid_divisa.COUNT) := rec$mediopago.oid_divisa;
        var$ampdoc_oid_medio_pago(var$ampdoc_oid_medio_pago.COUNT) := rec$mediopago.oid_medio_pago;
        var$ampdoc_cod_tipo_med_pago(var$ampdoc_cod_tipo_med_pago.COUNT) := rec$mediopago.cod_tipo_medio_pago;
        var$ampdoc_cod_nivel_detalle(var$ampdoc_cod_nivel_detalle.COUNT) := rec$mediopago.cod_nivel_detalle;
        var$ampdoc_num_importe(var$ampdoc_num_importe.COUNT) := rec$mediopago.importe;
        var$ampdoc_nel_cantidad(var$ampdoc_nel_cantidad.COUNT) := rec$mediopago.cantidad;
      END LOOP rec$mediopago;

      /* arrays de terminos de medio de pago por documento */
      FOR rec$terminomp IN cur$terminosmp(rec$documento_padre.oid_documento) LOOP
        var$avtmpdoc_oid_documento(var$avtmpdoc_oid_documento.COUNT) := var$oid_documento;
        var$avtmpdoc_oid_t_mediopago(var$avtmpdoc_oid_t_mediopago.COUNT) := rec$terminomp.oid_medio_pago;
        var$avtmpdoc_des_valor(var$avtmpdoc_des_valor.COUNT) := rec$terminomp.des_valor;
        var$avtmpdoc_nec_indice_grp(var$avtmpdoc_nec_indice_grp.COUNT) := rec$terminomp.nec_indice_grupo;
      END LOOP rec$terminomp;

      /* arrays de terminos por documento */
      FOR rec$termino IN cur$terminos(rec$documento_padre.oid_documento) LOOP
        var$avtdoc_oid_documento(var$avtdoc_oid_documento.COUNT) := var$oid_documento;
        var$avtdoc_oid_termino(var$avtdoc_oid_termino.COUNT) := rec$termino.oid_termino;
        var$avtdoc_des_valor(var$avtdoc_des_valor.COUNT) := rec$termino.des_valor;
      END LOOP rec$termino;

		/* Remesa - arrays */
		var$aremdoc_oid_remesa(var$aremdoc_oid_remesa.COUNT) := rec$documento_padre.oid_remesa;
		var$aremdoc_oid_documento(var$aremdoc_oid_documento.COUNT) := var$oid_documento;
		var$aremdoc_rowver(var$aremdoc_rowver.COUNT) := rec$documento_padre.ROWVER_REMESA;

		/* Bulto - arrays */
		IF par$bol_gestion_bulto = 0 THEN
		/*SE FOR GESTION POR REMESA*/
			FOR rec$bulto IN cur$bultos_por_remesa(rec$documento_padre.oid_remesa) LOOP
				var$abuldoc_oid_bulto(var$abuldoc_oid_bulto.COUNT) := rec$bulto.oid_bulto;
				var$abuldoc_oid_remesa(var$abuldoc_oid_remesa.COUNT) := rec$bulto.oid_remesa;
				var$abuldoc_oid_documento(var$abuldoc_oid_documento.COUNT) := var$oid_documento;
				var$abuldoc_rowver(var$abuldoc_rowver.COUNT) := rec$bulto.rowver;
			END LOOP cur$bultos_por_remesa;
		END IF;

    END LOOP rec$documento_padre;

    /* Grupo Documentos */
    var$oid_grupo_documento       := sys_guid();
    var$oid_grupo_documento_padre := NULL;
    var$rowver                    := NULL;

    /* Sector de origen es lo mesmo del primero documento */
    var$oid_sector_origen  := var$adocs_mov_ori_oid_sector(0);
    var$oid_sector_destino := var$adocs_mov_des_oid_sector(0);

    /* Ejecutar la procedure de reenvio */
    sguardar_grp_docs_elem_re_baj(par$oid_grupo_documento       => var$oid_grupo_documento,
                                  par$oid_grupo_documento_padre => var$oid_grupo_documento_padre,
                                  par$oid_formulario_grupo      => var$oid_formulario_grupo,
                                  par$oid_sector_origen         => var$oid_sector_origen,
                                  par$oid_sector_destino        => var$oid_sector_destino,
                                  par$rowver                    => var$rowver,
                                  par$avtgdoc_oid_termino       => var$avtgdoc_oid_termino,
                                  par$avtgdoc_des_valor         => var$avtgdoc_des_valor,
                                  par$adocs_oid                 => var$adocs_oid,
                                  par$adocs_oid_doc_padre       => var$adocs_oid_doc_padre,
                                  par$adocs_oid_sustituto       => var$adocs_oid_sustituto,
                                  par$adocs_oid_mov_fondos      => var$adocs_oid_mov_fondos,
                                  par$adocs_fyh_plncertif       => var$adocs_fyh_plncertif,
                                  par$adocs_fyh_gestion         => var$adocs_fyh_gestion,
                                  par$adocs_cod_externo         => var$adocs_cod_externo,
                                  par$adocs_rowver              => var$adocs_rowver,
                                  par$adocs_mov_ori_oid_cuenta  => var$adocs_mov_ori_oid_cuenta,
                                  par$adocs_mov_ori_oid_client  => var$adocs_mov_ori_oid_client,
                                  par$adocs_mov_ori_cod_client  => var$adocs_mov_ori_cod_client,
                                  par$adocs_mov_ori_oid_subcli  => var$adocs_mov_ori_oid_subcli,
                                  par$adocs_mov_ori_cod_subcli  => var$adocs_mov_ori_cod_subcli,
                                  par$adocs_mov_ori_oid_ptserv  => var$adocs_mov_ori_oid_ptserv,
                                  par$adocs_mov_ori_cod_ptserv  => var$adocs_mov_ori_cod_ptserv,
                                  par$adocs_mov_ori_oid_canal   => var$adocs_mov_ori_oid_canal,
                                  par$adocs_mov_ori_cod_canal   => var$adocs_mov_ori_cod_canal,
                                  par$adocs_mov_ori_oid_subcan  => var$adocs_mov_ori_oid_subcan,
                                  par$adocs_mov_ori_cod_subcan  => var$adocs_mov_ori_cod_subcan,
                                  par$adocs_mov_ori_oid_delega  => var$adocs_mov_ori_oid_delega,
                                  par$adocs_mov_ori_cod_delega  => var$adocs_mov_ori_cod_delega,
                                  par$adocs_mov_ori_oid_planta  => var$adocs_mov_ori_oid_planta,
                                  par$adocs_mov_ori_cod_planta  => var$adocs_mov_ori_cod_planta,
                                  par$adocs_mov_ori_oid_sector  => var$adocs_mov_ori_oid_sector,
                                  par$adocs_mov_ori_cod_sector  => var$adocs_mov_ori_cod_sector,
                                  par$adocs_mov_des_oid_cuenta  => var$adocs_mov_des_oid_cuenta,
                                  par$adocs_mov_des_oid_client  => var$adocs_mov_des_oid_client,
                                  par$adocs_mov_des_cod_client  => var$adocs_mov_des_cod_client,
                                  par$adocs_mov_des_oid_subcli  => var$adocs_mov_des_oid_subcli,
                                  par$adocs_mov_des_cod_subcli  => var$adocs_mov_des_cod_subcli,
                                  par$adocs_mov_des_oid_ptserv  => var$adocs_mov_des_oid_ptserv,
                                  par$adocs_mov_des_cod_ptserv  => var$adocs_mov_des_cod_ptserv,
                                  par$adocs_mov_des_oid_canal   => var$adocs_mov_des_oid_canal,
                                  par$adocs_mov_des_cod_canal   => var$adocs_mov_des_cod_canal,
                                  par$adocs_mov_des_oid_subcan  => var$adocs_mov_des_oid_subcan,
                                  par$adocs_mov_des_cod_subcan  => var$adocs_mov_des_cod_subcan,
                                  par$adocs_mov_des_oid_delega  => var$adocs_mov_des_oid_delega,
                                  par$adocs_mov_des_cod_delega  => var$adocs_mov_des_cod_delega,
                                  par$adocs_mov_des_oid_planta  => var$adocs_mov_des_oid_planta,
                                  par$adocs_mov_des_cod_planta  => var$adocs_mov_des_cod_planta,
                                  par$adocs_mov_des_oid_sector  => var$adocs_mov_des_oid_sector,
                                  par$adocs_mov_des_cod_sector  => var$adocs_mov_des_cod_sector,
                                  par$aefdoc_oid_documento      => var$aefdoc_oid_documento,
                                  par$aefdoc_oid_divisa         => var$aefdoc_oid_divisa,
                                  par$aefdoc_oid_denominacion   => var$aefdoc_oid_denominacion,
                                  par$aefdoc_oid_unid_medida    => var$aefdoc_oid_unid_medida,
                                  par$aefdoc_cod_niv_detalle    => var$aefdoc_cod_niv_detalle,
                                  par$aefdoc_cod_tp_efec_tot    => var$aefdoc_cod_tp_efec_tot,
                                  par$aefdoc_oid_calidad        => var$aefdoc_oid_calidad,
                                  par$aefdoc_num_importe        => var$aefdoc_num_importe,
                                  par$aefdoc_nel_cantidad       => var$aefdoc_nel_cantidad,
                                  par$ampdoc_oid_documento      => var$ampdoc_oid_documento,
                                  par$ampdoc_oid_divisa         => var$ampdoc_oid_divisa,
                                  par$ampdoc_oid_medio_pago     => var$ampdoc_oid_medio_pago,
                                  par$ampdoc_cod_tipo_med_pago  => var$ampdoc_cod_tipo_med_pago,
                                  par$ampdoc_cod_nivel_detalle  => var$ampdoc_cod_nivel_detalle,
                                  par$ampdoc_num_importe        => var$ampdoc_num_importe,
                                  par$ampdoc_nel_cantidad       => var$ampdoc_nel_cantidad,
                                  par$avtmpdoc_oid_documento    => var$avtmpdoc_oid_documento,
                                  par$avtmpdoc_oid_t_mediopago  => var$avtmpdoc_oid_t_mediopago,
                                  par$avtmpdoc_des_valor        => var$avtmpdoc_des_valor,
                                  par$avtmpdoc_nec_indice_grp   => var$avtmpdoc_nec_indice_grp,
                                  par$aremdoc_oid_remesa        => var$aremdoc_oid_remesa,
                                  par$aremdoc_oid_documento     => var$aremdoc_oid_documento,
                                  par$aremdoc_rowver            => var$aremdoc_rowver,
                                  par$abuldoc_oid_bulto         => var$abuldoc_oid_bulto,
                                  par$abuldoc_oid_remesa        => var$abuldoc_oid_remesa,
                                  par$abuldoc_oid_documento     => var$abuldoc_oid_documento,
                                  par$abuldoc_rowver            => var$abuldoc_rowver,
                                  par$usuario                   => par$usuario,
                                  par$cod_cultura               => par$cod_cultura,
                                  par$info_ejecucion            => par$info_ejecucion,
                                  par$hacer_commit              => 0,
                                  par$confirmar_doc             => par$confirmar_doc,
                                  par$rc_grupo_documentos       => par$rc_grupo_documentos,
                                  par$rc_historico_mov_grupo    => par$rc_historico_mov_grupo,
                                  par$doc_rc_documentos         => par$doc_rc_documentos,
                                  par$doc_rc_historico_mov_doc  => par$doc_rc_historico_mov_doc,
                                  par$ele_rc_elementos          => par$ele_rc_elementos,
                                  par$cod_ejecucion             => par$cod_ejecucion);

     IF par$confirmar_doc > 0 AND par$aceptar_doc > 0 THEN

      /* limpiar tabla temporaria */
      delete sapr_gtt_toids;
      var$deletes   := var$deletes + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;

      var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado;
      SELECT GD.COD_COMPROBANTE, GD.ROWVER
        INTO var$cod_comprobante, var$rowver
        FROM SAPR_TGRUPO_DOCUMENTO GD
       WHERE GD.OID_GRUPO_DOCUMENTO = var$oid_grupo_documento;

      sapr_ptransiciones_###VERSION###.sejecutar_grp_doc(par$oid_grupo_documento  => var$oid_grupo_documento,
                                                    par$cod_estado_documento => var$cod_estado_documento,
                                                    par$cod_comprobante      => var$cod_comprobante,
                                                    par$rowver               => var$rowver,
                                                    par$usuario              => par$usuario,
                                                    par$cod_cultura          => par$cod_cultura,
                                                    par$info_ejecucion       => par$info_ejecucion,
                                                    par$cod_ejecucion        => par$cod_ejecucion,
                                                    par$hacer_commit         => 0,
                                                    par$inserts              => var$inserts_int,
                                                    par$updates              => var$updates_int,
                                                    par$deletes              => var$deletes_int,
                                                    par$selects              => var$selects_int);

      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$deletes_int + var$selects_int;
      var$inserts   := var$inserts + var$inserts_int;
      var$updates   := var$updates + var$updates_int;
      var$deletes   := var$deletes + var$deletes_int;
      var$selects   := var$selects + var$selects_int;

      /* - Documentos y Documentos Padres */
      sapr_pdocumento_###VERSION###.scargar_tt_oids_documento_gpr(par$oid_grupo_documento => var$oid_grupo_documento,
                                                             par$recuperar_padres    => true,
                                                             par$inserts             => var$inserts_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$registros := var$registros + var$inserts_int;

      /* - TABLA: Grupo de Documentos */
      par$rc_grupo_documentos := fcargar_rc_grupo_documento(var$oid_grupo_documento,
                                                            par$selects => var$selects_int);
      var$selects             := var$selects + var$selects_int;
      var$registros           := var$registros + var$selects_int;

      /* - TABLA: Historico Grupo */
      par$rc_historico_mov_grupo := fcargar_rc_historico(var$oid_grupo_documento,
                                                         par$selects => var$selects_int);
      var$selects                := var$selects + var$selects_int;
      var$registros              := var$registros + var$selects_int;

      /* - TABLA: Documentos */
      par$doc_rc_documentos := sapr_pdocumento_###VERSION###.fcargar_rc_documento;
      var$selects           := var$selects + SQL % ROWCOUNT;
      var$registros         := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := sapr_pdocumento_###VERSION###.fcargar_rc_historico;
      var$selects                  := var$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;

      /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
      par$ele_rc_elementos := sapr_premesa_###VERSION###.fcargar_rc_elementos;
      var$selects          := var$selects + SQL % ROWCOUNT;
      var$registros        := var$registros + SQL % ROWCOUNT;

    END IF;

    IF par$hacer_commit > 0 THEN
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
                                                           par$usuario,
                                                           par$cod_ejecucion);
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
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error' || var$err,
                              true);

  END sguardar_grp_docs_elem_reenvi;

  /* Guardar documento de Salidas Recorido - Gestion de Remesas */
  PROCEDURE sguardar_grp_docs_elem_salida(par$cod_ruta        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$fyh_plncertif   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                          par$arem_cod_remesa IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$cod_delegacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_integracion IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$usuario         IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion  IN VARCHAR2,
                                          par$hacer_commit    IN INTEGER,
                                          par$confirmar_doc   IN INTEGER,
                                          par$cod_ejecucion   OUT gepr_pcomon_###VERSION###.tipo$nel_) AS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_docs_elem_salida';

    /* variables locales */
    var$inicio               gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros            gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                  VARCHAR2(4000);
    var$bol_gestion_bulto    gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_delegacion       gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planta           gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sector           gepr_pcomon_###VERSION###.tipo$cod_;

    /* varibles para salidas */
    var$oid_grupo_documento       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_grupo_documento_padre gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_formulario_grupo      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_origen         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_destino        gepr_pcomon_###VERSION###.tipo$oid_;
    var$rowver                    gepr_pcomon_###VERSION###.tipo$nel_;
    var$oid_documento             gepr_pcomon_###VERSION###.tipo$oid_;

    /* arrays de terminos del grupo de documentos */
    var$avtgdoc_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtgdoc_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;

    /* arrays de documentos */
    var$adocs_oid            gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_doc_padre  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_sustituto  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_mov_fondos gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_fyh_plncertif  gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_fyh_gestion    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_cod_externo    gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_rowver         gepr_pcomon_###VERSION###.tipo$nels_;

    /* cuenta origen */
    var$adocs_mov_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_oid_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_client gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_subcli gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_ptserv gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_canal  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_canal  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_subcan gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_subcan gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_delega gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_delega gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_planta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_planta gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_sector gepr_pcomon_###VERSION###.tipo$cods_;

    /* cuenta destino */
    var$adocs_mov_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_client gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_subcli gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_ptserv gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_canal  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_canal  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_subcan gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_subcan gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_delega gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_delega gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_planta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_planta gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_sector gepr_pcomon_###VERSION###.tipo$cods_;

    /* arrays de efectivo por documento */
    var$aefdoc_oid_documento    gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_divisa       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_denominacion gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_unid_medida  gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_cod_niv_detalle  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_cod_tp_efec_tot  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_oid_calidad      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_num_importe      gepr_pcomon_###VERSION###.tipo$imps_;
    var$aefdoc_nel_cantidad     gepr_pcomon_###VERSION###.tipo$nels_;

    /* arrays de medio de pago por documento */
    var$ampdoc_oid_documento     gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_oid_divisa        gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_oid_medio_pago    gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_cod_tipo_med_pago gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_cod_nivel_detalle gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_num_importe       gepr_pcomon_###VERSION###.tipo$imps_;
    var$ampdoc_nel_cantidad      gepr_pcomon_###VERSION###.tipo$nels_;

    /* arrays de terminos de medio de pago por documento */
    var$avtmpdoc_oid_documento   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_oid_t_mediopago gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_des_valor       gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtmpdoc_nec_indice_grp  gepr_pcomon_###VERSION###.tipo$ints_;

    /* arrays de terminos por documento */
    var$avtdoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_oid_termino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_des_valor     gepr_pcomon_###VERSION###.tipo$descs_;

    /* Remesa - arrays */
    var$aremdoc_oid_remesa    gepr_pcomon_###VERSION###.tipo$oids_;
    var$aremdoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$aremdoc_rowver        gepr_pcomon_###VERSION###.tipo$nels_;

    /* Bulto - arrays */
    var$abuldoc_oid_bulto     gepr_pcomon_###VERSION###.tipo$oids_;
    var$abuldoc_oid_remesa    gepr_pcomon_###VERSION###.tipo$oids_;
    var$abuldoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$abuldoc_rowver        gepr_pcomon_###VERSION###.tipo$nels_;

    var$rc_grupo_documentos      sys_refcursor;
    var$rc_historico_mov_grupo   sys_refcursor;
    var$doc_rc_documentos        sys_refcursor;
    var$doc_rc_historico_mov_doc sys_refcursor;
    var$ele_rc_elementos         sys_refcursor;

    /* CURSOR DE DOCUMENTOS PADRES */
    CURSOR cur$documentos_padres IS
      SELECT D.OID_DOCUMENTO,
             D.COD_EXTERNO,
             D.OID_CUENTA_DESTINO,
             D.OID_CUENTA_SALDO_DESTINO,
             D.OID_SECTOR_DESTINO,
             DE.OID_REMESA,
             R.COD_EXTERNO AS COD_EXTERNO_REMESA,
             R.ROWVER AS ROWVER_REMESA,
             DE.OID_BULTO,
             B.COD_PRECINTO,
             B.ROWVER AS ROWVER_BULTO
        FROM SAPR_TDOCUMENTOXELEMENTO DE
       INNER JOIN SAPR_GTT_TOIDS T
          ON T.OID = DE.OID_DOCUMENTOXELEMENTO
       INNER JOIN SAPR_TDOCUMENTO D
          ON D.OID_DOCUMENTO = DE.OID_DOCUMENTO
       INNER JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DE.OID_REMESA
       LEFT JOIN SAPR_TBULTO B
          ON B.OID_BULTO = DE.OID_BULTO
       WHERE T.COD_CALIFICADOR = 'DOCXELE';

    /* CURSOR DE TERMINOS DOCUMENTOS */
    CURSOR cur$terminos(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TD.DES_VALOR, TD.OID_DOCUMENTO, T.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TVALOR_TERMINOXDOCUMENTO TD
       INNER JOIN GEPR_TTERMINO T
          ON TD.OID_TERMINO = T.OID_TERMINO
       WHERE TD.OID_DOCUMENTO = par$oid_documento;

    /* CURSOR DE EFECTIVOS DOCUMENTOS */
    CURSOR cur$efectivos(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT ED.OID_DOCUMENTO,
             ED.OID_DIVISA,
             ED.OID_DENOMINACION,
             ED.COD_NIVEL_DETALLE,
             ED.OID_UNIDAD_MEDIDA,
             ED.OID_CALIDAD,
             ED.COD_TIPO_EFECTIVO_TOTAL,
             SUM(ED.NUM_IMPORTE) AS IMPORTE,
             SUM(ED.NEL_CANTIDAD) AS CANTIDAD
        FROM SAPR_TEFECTIVOXDOCUMENTO ED
       WHERE ED.OID_DOCUMENTO = par$oid_documento
       GROUP BY ED.OID_DOCUMENTO,
                ED.OID_DIVISA,
                ED.OID_DENOMINACION,
                ED.COD_NIVEL_DETALLE,
                ED.OID_UNIDAD_MEDIDA,
                ED.OID_CALIDAD,
                ED.COD_TIPO_EFECTIVO_TOTAL;

    /* CURSOR DE MEDIO_PAGO DOCUMENTOS */
    CURSOR cur$mediopagos(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT MP.OID_DOCUMENTO,
             MP.OID_DIVISA,
             MP.OID_MEDIO_PAGO,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_NIVEL_DETALLE,
             SUM(MP.NUM_IMPORTE) AS IMPORTE,
             SUM(MP.NEL_CANTIDAD) AS CANTIDAD
        FROM SAPR_TMEDIO_PAGOXDOCUMENTO MP
       WHERE MP.OID_DOCUMENTO = par$oid_documento
       GROUP BY MP.OID_DOCUMENTO,
                MP.OID_DIVISA,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.OID_MEDIO_PAGO,
                MP.COD_NIVEL_DETALLE;

    /* CURSOR DE MEDIO_PAGO DOCUMENTOS */
    CURSOR cur$terminosmp(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT PD.OID_DOCUMENTO,
             OID_MEDIO_PAGO,
             TMP.DES_VALOR,
             TMP.NEC_INDICE_GRUPO
        FROM SAPR_TVALOR_TERMINO_MEDIO_PAGO TMP
       INNER JOIN SAPR_TMEDIO_PAGOXDOCUMENTO PD
          ON TMP.OID_MEDIO_PAGOXDOCUMENTO = PD.OID_MEDIO_PAGOXDOCUMENTO
       WHERE PD.OID_DOCUMENTO = PAR$OID_DOCUMENTO
       ORDER BY PD.OID_DOCUMENTO, TMP.NEC_INDICE_GRUPO;

    /* CURSOR DE REMESAS */
    CURSOR cur$remesas IS
      SELECT DE.OID_REMESA, R.ROWVER
        FROM SAPR_TDOCUMENTOXELEMENTO DE
       INNER JOIN SAPR_GTT_TOIDS T
          ON T.OID = DE.OID_DOCUMENTOXELEMENTO
       INNER JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DE.OID_REMESA
       WHERE T.COD_CALIFICADOR = 'DOCXELE'
         AND DE.COD_ESTADO_DOCXELEMENTO = 'C'
         AND DE.OID_BULTO IS NULL;

    /* CURSOR DE BULTOS */
    CURSOR cur$bultos IS
      SELECT DE.OID_BULTO, DE.OID_REMESA, B.ROWVER
        FROM SAPR_TDOCUMENTOXELEMENTO DE
       INNER JOIN SAPR_GTT_TOIDS T
          ON T.OID = DE.OID_DOCUMENTOXELEMENTO
       INNER JOIN SAPR_TBULTO B
          ON B.OID_BULTO = DE.OID_BULTO
       WHERE T.COD_CALIFICADOR = 'DOCXELE'
         AND DE.COD_ESTADO_DOCXELEMENTO = 'C'
         AND DE.OID_BULTO IS NOT NULL;

  BEGIN

    /* Valores iniciales */
    var$inicio := SYSTIMESTAMP;

    /* limpiar tabla temporaria */
    delete sapr_gtt_toids;
    var$deletes   := var$deletes + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;

    /* ===== Cargar tabla temporaria ==== */
    /* - Documentos y Documentos Padres */
    IF par$cod_ruta IS NOT NULL THEN

        INSERT INTO SAPR_GTT_TOIDS
          SELECT DE.OID_DOCUMENTOXELEMENTO, 'DOCXELE' COD_CALIFICADOR
            FROM SAPR_TDOCUMENTOXELEMENTO DE
           INNER JOIN (SELECT R.OID_REMESA
                         FROM SAPR_TREMESA R
                        START WITH R.COD_RUTA = par$cod_ruta
                       CONNECT BY NOCYCLE
                        PRIOR R.OID_REMESA = R.OID_REMESA_PADRE) R
              ON R.OID_REMESA = DE.OID_REMESA
           INNER JOIN SAPR_TBULTO B
              ON B.OID_REMESA = R.OID_REMESA
           WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
             AND B.COD_ESTADO <> gepr_pcomon_###VERSION###.const$codEstadoBultoSalida
             AND DE.OID_BULTO IS NULL
             GROUP BY DE.OID_DOCUMENTOXELEMENTO;
        var$inserts   := var$inserts + sql%ROWCOUNT;
        var$registros := var$registros + sql%ROWCOUNT;

    ELSE

      FOR idx IN par$arem_cod_remesa.first .. par$arem_cod_remesa.last LOOP

        INSERT INTO SAPR_GTT_TOIDS
          SELECT DE.OID_DOCUMENTOXELEMENTO, 'DOCXELE' COD_CALIFICADOR
            FROM SAPR_TDOCUMENTOXELEMENTO DE
           INNER JOIN (SELECT R.OID_REMESA
                         FROM SAPR_TREMESA R
                        START WITH R.COD_EXTERNO = par$arem_cod_remesa(idx)
                       CONNECT BY NOCYCLE
                        PRIOR R.OID_REMESA = R.OID_REMESA_PADRE) R
              ON R.OID_REMESA = DE.OID_REMESA
           INNER JOIN SAPR_TBULTO B
              ON B.OID_REMESA = R.OID_REMESA
           WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
             AND B.COD_ESTADO <> gepr_pcomon_###VERSION###.const$codEstadoBultoSalida
             AND DE.OID_BULTO IS NULL
             GROUP BY DE.OID_DOCUMENTOXELEMENTO;
        var$inserts   := var$inserts + sql%ROWCOUNT;
        var$registros := var$registros + sql%ROWCOUNT;

      END LOOP;

    END IF;

    /* recuperar formulario de reenvio */
    sapr_pformulario_###VERSION###.srecuperar_form_baja(par$oid_formulario    => var$oid_formulario_grupo,
                                                   par$bol_gestion_bulto => var$bol_gestion_bulto,
                                                   par$cod_integracion   => par$cod_integracion,
                                                   par$cod_cultura       => par$cod_cultura);

    /* Cargar variables */
    FOR rec$documento_padre IN cur$documentos_padres LOOP

      var$oid_documento := sys_guid();
      /* arrays de documentos */
      var$adocs_oid(var$adocs_oid.count) := var$oid_documento;
      var$adocs_oid_doc_padre(var$adocs_oid_doc_padre.count) := rec$documento_padre.oid_documento;
      var$adocs_oid_sustituto(var$adocs_oid_sustituto.count) := NULL;
      var$adocs_oid_mov_fondos(var$adocs_oid_mov_fondos.count) := NULL;
      var$adocs_fyh_plncertif(var$adocs_fyh_plncertif.count) := par$fyh_plncertif;
      var$adocs_fyh_gestion(var$adocs_fyh_gestion.count) := sys_extract_utc(current_timestamp);
      var$adocs_cod_externo(var$adocs_cod_externo.count) := rec$documento_padre.cod_externo;
      var$adocs_rowver(var$adocs_rowver.count) := NULL;

      /* cuenta origen */
      var$adocs_mov_ori_oid_cuenta(var$adocs_mov_ori_oid_cuenta.count) := rec$documento_padre.oid_cuenta_destino;
      var$adocs_mov_ori_oid_client(var$adocs_mov_ori_oid_client.count) := NULL;
      var$adocs_mov_ori_cod_client(var$adocs_mov_ori_cod_client.count) := NULL;
      var$adocs_mov_ori_oid_subcli(var$adocs_mov_ori_oid_subcli.count) := NULL;
      var$adocs_mov_ori_cod_subcli(var$adocs_mov_ori_cod_subcli.count) := NULL;
      var$adocs_mov_ori_oid_ptserv(var$adocs_mov_ori_oid_ptserv.count) := NULL;
      var$adocs_mov_ori_cod_ptserv(var$adocs_mov_ori_cod_ptserv.count) := NULL;
      var$adocs_mov_ori_oid_canal(var$adocs_mov_ori_oid_canal.count) := NULL;
      var$adocs_mov_ori_cod_canal(var$adocs_mov_ori_cod_canal.count) := NULL;
      var$adocs_mov_ori_oid_subcan(var$adocs_mov_ori_oid_subcan.count) := NULL;
      var$adocs_mov_ori_cod_subcan(var$adocs_mov_ori_cod_subcan.count) := NULL;
      var$adocs_mov_ori_oid_delega(var$adocs_mov_ori_oid_delega.count) := NULL;
      var$adocs_mov_ori_cod_delega(var$adocs_mov_ori_cod_delega.count) := NULL;
      var$adocs_mov_ori_oid_planta(var$adocs_mov_ori_oid_planta.count) := NULL;
      var$adocs_mov_ori_cod_planta(var$adocs_mov_ori_cod_planta.count) := NULL;
      var$adocs_mov_ori_oid_sector(var$adocs_mov_ori_oid_sector.count) := rec$documento_padre.oid_sector_destino;
      var$adocs_mov_ori_cod_sector(var$adocs_mov_ori_cod_sector.count) := NULL;

      /* cuenta destino */
      var$adocs_mov_des_oid_cuenta(var$adocs_mov_des_oid_cuenta.count) := rec$documento_padre.oid_cuenta_destino;
      var$adocs_mov_des_oid_client(var$adocs_mov_des_oid_client.count) := NULL;
      var$adocs_mov_des_cod_client(var$adocs_mov_des_cod_client.count) := NULL;
      var$adocs_mov_des_oid_subcli(var$adocs_mov_des_oid_subcli.count) := NULL;
      var$adocs_mov_des_cod_subcli(var$adocs_mov_des_cod_subcli.count) := NULL;
      var$adocs_mov_des_oid_ptserv(var$adocs_mov_des_oid_ptserv.count) := NULL;
      var$adocs_mov_des_cod_ptserv(var$adocs_mov_des_cod_ptserv.count) := NULL;
      var$adocs_mov_des_oid_canal(var$adocs_mov_des_oid_canal.count) := NULL;
      var$adocs_mov_des_cod_canal(var$adocs_mov_des_cod_canal.count) := NULL;
      var$adocs_mov_des_oid_subcan(var$adocs_mov_des_oid_subcan.count) := NULL;
      var$adocs_mov_des_cod_subcan(var$adocs_mov_des_cod_subcan.count) := NULL;
      var$adocs_mov_des_oid_delega(var$adocs_mov_des_oid_delega.count) := NULL;
      var$adocs_mov_des_cod_delega(var$adocs_mov_des_cod_delega.count) := NULL;
      var$adocs_mov_des_oid_planta(var$adocs_mov_des_oid_planta.count) := NULL;
      var$adocs_mov_des_cod_planta(var$adocs_mov_des_cod_planta.count) := NULL;
      var$adocs_mov_des_oid_sector(var$adocs_mov_des_oid_sector.count) := rec$documento_padre.oid_sector_destino;
      var$adocs_mov_des_cod_sector(var$adocs_mov_des_cod_sector.count) := NULL;

      SELECT D.COD_DELEGACION, P.COD_PLANTA, S.COD_SECTOR
        INTO var$cod_delegacion, var$cod_planta, var$cod_sector
        FROM SAPR_TCUENTA C
       INNER JOIN GEPR_TSECTOR S
          ON S.OID_SECTOR = C.OID_SECTOR
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = S.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION D
          ON D.OID_DELEGACION = P.OID_DELEGACION
       WHERE C.OID_CUENTA = rec$documento_padre.oid_cuenta_saldo_destino;

      IF var$cod_delegacion <> par$cod_delegacion OR
         var$cod_planta <> par$cod_planta OR
         var$cod_sector <> par$cod_sector THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'origin_invalida',
                                                                    const$codFuncionalidad || '.' ||
                                                                    const$nombre_func,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
      END IF;

      /* arrays de efectivo por documento */
      FOR rec$efectivo IN cur$efectivos(rec$documento_padre.oid_documento) LOOP
        var$aefdoc_oid_documento(var$aefdoc_oid_documento.COUNT) := var$oid_documento;
        var$aefdoc_oid_divisa(var$aefdoc_oid_divisa.COUNT) := rec$efectivo.oid_divisa;
        var$aefdoc_oid_denominacion(var$aefdoc_oid_denominacion.COUNT) := rec$efectivo.oid_denominacion;
        var$aefdoc_oid_unid_medida(var$aefdoc_oid_unid_medida.COUNT) := rec$efectivo.oid_unidad_medida;
        var$aefdoc_cod_niv_detalle(var$aefdoc_cod_niv_detalle.COUNT) := rec$efectivo.cod_nivel_detalle;
        var$aefdoc_cod_tp_efec_tot(var$aefdoc_cod_tp_efec_tot.COUNT) := rec$efectivo.COD_TIPO_EFECTIVO_TOTAL;
        var$aefdoc_oid_calidad(var$aefdoc_oid_calidad.COUNT) := rec$efectivo.oid_calidad;
        var$aefdoc_num_importe(var$aefdoc_num_importe.COUNT) := rec$efectivo.importe;
        var$aefdoc_nel_cantidad(var$aefdoc_nel_cantidad.COUNT) := rec$efectivo.cantidad;
      END LOOP rec$efectivo;

      /* arrays de medio de pago por documento */
      FOR rec$mediopago IN cur$mediopagos(rec$documento_padre.oid_documento) LOOP
        var$ampdoc_oid_documento(var$ampdoc_oid_documento.COUNT) := var$oid_documento;
        var$ampdoc_oid_divisa(var$ampdoc_oid_divisa.COUNT) := rec$mediopago.oid_divisa;
        var$ampdoc_oid_medio_pago(var$ampdoc_oid_medio_pago.COUNT) := rec$mediopago.oid_medio_pago;
        var$ampdoc_cod_tipo_med_pago(var$ampdoc_cod_tipo_med_pago.COUNT) := rec$mediopago.cod_tipo_medio_pago;
        var$ampdoc_cod_nivel_detalle(var$ampdoc_cod_nivel_detalle.COUNT) := rec$mediopago.cod_nivel_detalle;
        var$ampdoc_num_importe(var$ampdoc_num_importe.COUNT) := rec$mediopago.importe;
        var$ampdoc_nel_cantidad(var$ampdoc_nel_cantidad.COUNT) := rec$mediopago.cantidad;
      END LOOP rec$mediopago;

      /* arrays de terminos de medio de pago por documento */
      FOR rec$terminomp IN cur$terminosmp(rec$documento_padre.oid_documento) LOOP
        var$avtmpdoc_oid_documento(var$avtmpdoc_oid_documento.COUNT) := var$oid_documento;
        var$avtmpdoc_oid_t_mediopago(var$avtmpdoc_oid_t_mediopago.COUNT) := rec$terminomp.oid_medio_pago;
        var$avtmpdoc_des_valor(var$avtmpdoc_des_valor.COUNT) := rec$terminomp.des_valor;
        var$avtmpdoc_nec_indice_grp(var$avtmpdoc_nec_indice_grp.COUNT) := rec$terminomp.nec_indice_grupo;
      END LOOP rec$terminomp;

      /* arrays de terminos por documento */
      FOR rec$termino IN cur$terminos(rec$documento_padre.oid_documento) LOOP
        var$avtdoc_oid_documento(var$avtdoc_oid_documento.COUNT) := var$oid_documento;
        var$avtdoc_oid_termino(var$avtdoc_oid_termino.COUNT) := rec$termino.oid_termino;
        var$avtdoc_des_valor(var$avtdoc_des_valor.COUNT) := rec$termino.des_valor;
      END LOOP rec$termino;

      /* Remesa - arrays */
      FOR rec$remesa IN cur$remesas LOOP
        var$aremdoc_oid_remesa(var$aremdoc_oid_remesa.COUNT) := rec$remesa.oid_remesa;
        var$aremdoc_oid_documento(var$aremdoc_oid_documento.COUNT) := var$oid_documento;
        var$aremdoc_rowver(var$aremdoc_rowver.count) := rec$remesa.rowver;
      END LOOP rec$remesa;

      /* Bulto - arrays */
      FOR rec$bulto IN cur$bultos LOOP
        var$abuldoc_oid_bulto(var$abuldoc_oid_bulto.COUNT) := rec$bulto.oid_bulto;
        var$abuldoc_oid_remesa(var$abuldoc_oid_remesa.COUNT) := rec$bulto.oid_remesa;
        var$abuldoc_oid_documento(var$abuldoc_oid_documento.COUNT) := var$oid_documento;
        var$abuldoc_rowver(var$abuldoc_rowver.COUNT) := rec$bulto.rowver;
      END LOOP rec$remesa;

    END LOOP rec$documento_padre;

    /* Grupo Documentos */
    var$oid_grupo_documento       := sys_guid();
    var$oid_grupo_documento_padre := NULL;
    var$rowver                    := NULL;

    /* Sector de origen es lo mesmo del primero documento */
    var$oid_sector_origen  := var$adocs_mov_ori_oid_sector(0);
    var$oid_sector_destino := var$adocs_mov_des_oid_sector(0);

    /* Ejecutar la procedure de reenvio */
    sguardar_grp_docs_elem_re_baj(par$oid_grupo_documento       => var$oid_grupo_documento,
                                  par$oid_grupo_documento_padre => var$oid_grupo_documento_padre,
                                  par$oid_formulario_grupo      => var$oid_formulario_grupo,
                                  par$oid_sector_origen         => var$oid_sector_origen,
                                  par$oid_sector_destino        => var$oid_sector_destino,
                                  par$rowver                    => var$rowver,
                                  par$avtgdoc_oid_termino       => var$avtgdoc_oid_termino,
                                  par$avtgdoc_des_valor         => var$avtgdoc_des_valor,
                                  par$adocs_oid                 => var$adocs_oid,
                                  par$adocs_oid_doc_padre       => var$adocs_oid_doc_padre,
                                  par$adocs_oid_sustituto       => var$adocs_oid_sustituto,
                                  par$adocs_oid_mov_fondos      => var$adocs_oid_mov_fondos,
                                  par$adocs_fyh_plncertif       => var$adocs_fyh_plncertif,
                                  par$adocs_fyh_gestion         => var$adocs_fyh_gestion,
                                  par$adocs_cod_externo         => var$adocs_cod_externo,
                                  par$adocs_rowver              => var$adocs_rowver,
                                  par$adocs_mov_ori_oid_cuenta  => var$adocs_mov_ori_oid_cuenta,
                                  par$adocs_mov_ori_oid_client  => var$adocs_mov_ori_oid_client,
                                  par$adocs_mov_ori_cod_client  => var$adocs_mov_ori_cod_client,
                                  par$adocs_mov_ori_oid_subcli  => var$adocs_mov_ori_oid_subcli,
                                  par$adocs_mov_ori_cod_subcli  => var$adocs_mov_ori_cod_subcli,
                                  par$adocs_mov_ori_oid_ptserv  => var$adocs_mov_ori_oid_ptserv,
                                  par$adocs_mov_ori_cod_ptserv  => var$adocs_mov_ori_cod_ptserv,
                                  par$adocs_mov_ori_oid_canal   => var$adocs_mov_ori_oid_canal,
                                  par$adocs_mov_ori_cod_canal   => var$adocs_mov_ori_cod_canal,
                                  par$adocs_mov_ori_oid_subcan  => var$adocs_mov_ori_oid_subcan,
                                  par$adocs_mov_ori_cod_subcan  => var$adocs_mov_ori_cod_subcan,
                                  par$adocs_mov_ori_oid_delega  => var$adocs_mov_ori_oid_delega,
                                  par$adocs_mov_ori_cod_delega  => var$adocs_mov_ori_cod_delega,
                                  par$adocs_mov_ori_oid_planta  => var$adocs_mov_ori_oid_planta,
                                  par$adocs_mov_ori_cod_planta  => var$adocs_mov_ori_cod_planta,
                                  par$adocs_mov_ori_oid_sector  => var$adocs_mov_ori_oid_sector,
                                  par$adocs_mov_ori_cod_sector  => var$adocs_mov_ori_cod_sector,
                                  par$adocs_mov_des_oid_cuenta  => var$adocs_mov_des_oid_cuenta,
                                  par$adocs_mov_des_oid_client  => var$adocs_mov_des_oid_client,
                                  par$adocs_mov_des_cod_client  => var$adocs_mov_des_cod_client,
                                  par$adocs_mov_des_oid_subcli  => var$adocs_mov_des_oid_subcli,
                                  par$adocs_mov_des_cod_subcli  => var$adocs_mov_des_cod_subcli,
                                  par$adocs_mov_des_oid_ptserv  => var$adocs_mov_des_oid_ptserv,
                                  par$adocs_mov_des_cod_ptserv  => var$adocs_mov_des_cod_ptserv,
                                  par$adocs_mov_des_oid_canal   => var$adocs_mov_des_oid_canal,
                                  par$adocs_mov_des_cod_canal   => var$adocs_mov_des_cod_canal,
                                  par$adocs_mov_des_oid_subcan  => var$adocs_mov_des_oid_subcan,
                                  par$adocs_mov_des_cod_subcan  => var$adocs_mov_des_cod_subcan,
                                  par$adocs_mov_des_oid_delega  => var$adocs_mov_des_oid_delega,
                                  par$adocs_mov_des_cod_delega  => var$adocs_mov_des_cod_delega,
                                  par$adocs_mov_des_oid_planta  => var$adocs_mov_des_oid_planta,
                                  par$adocs_mov_des_cod_planta  => var$adocs_mov_des_cod_planta,
                                  par$adocs_mov_des_oid_sector  => var$adocs_mov_des_oid_sector,
                                  par$adocs_mov_des_cod_sector  => var$adocs_mov_des_cod_sector,
                                  par$aefdoc_oid_documento      => var$aefdoc_oid_documento,
                                  par$aefdoc_oid_divisa         => var$aefdoc_oid_divisa,
                                  par$aefdoc_oid_denominacion   => var$aefdoc_oid_denominacion,
                                  par$aefdoc_oid_unid_medida    => var$aefdoc_oid_unid_medida,
                                  par$aefdoc_cod_niv_detalle    => var$aefdoc_cod_niv_detalle,
                                  par$aefdoc_cod_tp_efec_tot    => var$aefdoc_cod_tp_efec_tot,
                                  par$aefdoc_oid_calidad        => var$aefdoc_oid_calidad,
                                  par$aefdoc_num_importe        => var$aefdoc_num_importe,
                                  par$aefdoc_nel_cantidad       => var$aefdoc_nel_cantidad,
                                  par$ampdoc_oid_documento      => var$ampdoc_oid_documento,
                                  par$ampdoc_oid_divisa         => var$ampdoc_oid_divisa,
                                  par$ampdoc_oid_medio_pago     => var$ampdoc_oid_medio_pago,
                                  par$ampdoc_cod_tipo_med_pago  => var$ampdoc_cod_tipo_med_pago,
                                  par$ampdoc_cod_nivel_detalle  => var$ampdoc_cod_nivel_detalle,
                                  par$ampdoc_num_importe        => var$ampdoc_num_importe,
                                  par$ampdoc_nel_cantidad       => var$ampdoc_nel_cantidad,
                                  par$avtmpdoc_oid_documento    => var$avtmpdoc_oid_documento,
                                  par$avtmpdoc_oid_t_mediopago  => var$avtmpdoc_oid_t_mediopago,
                                  par$avtmpdoc_des_valor        => var$avtmpdoc_des_valor,
                                  par$avtmpdoc_nec_indice_grp   => var$avtmpdoc_nec_indice_grp,
                                  par$aremdoc_oid_remesa        => var$aremdoc_oid_remesa,
                                  par$aremdoc_oid_documento     => var$aremdoc_oid_documento,
                                  par$aremdoc_rowver            => var$aremdoc_rowver,
                                  par$abuldoc_oid_bulto         => var$abuldoc_oid_bulto,
                                  par$abuldoc_oid_remesa        => var$abuldoc_oid_remesa,
                                  par$abuldoc_oid_documento     => var$abuldoc_oid_documento,
                                  par$abuldoc_rowver            => var$abuldoc_rowver,
                                  par$usuario                   => par$usuario,
                                  par$cod_cultura               => par$cod_cultura,
                                  par$info_ejecucion            => par$info_ejecucion,
                                  par$hacer_commit              => 0,
                                  par$confirmar_doc             => par$confirmar_doc,
                                  par$rc_grupo_documentos       => var$rc_grupo_documentos,
                                  par$rc_historico_mov_grupo    => var$rc_historico_mov_grupo,
                                  par$doc_rc_documentos         => var$doc_rc_documentos,
                                  par$doc_rc_historico_mov_doc  => var$doc_rc_historico_mov_doc,
                                  par$ele_rc_elementos          => var$ele_rc_elementos,
                                  par$cod_ejecucion             => par$cod_ejecucion);

    IF par$hacer_commit > 0 THEN
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
                                                           par$usuario,
                                                           par$cod_ejecucion);
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
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error' || var$err,
                              true);

  END sguardar_grp_docs_elem_salida;

  /* Guardar grupo - Gestion de Contenedores */
  PROCEDURE sguardar_grp_docs_cont(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_comprobante           OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                   /*Terminos Grupo documento*/
                                   par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                   --documentos
                                   par$adoc_oid_documento       in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_documento_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_documento_sust  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_moviment_fondo  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_fyh_plan_certif     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoc_rowver              IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   /* cuenta origen */
                                   par$adoc_oid_cuenta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_cliente_origen    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_subcliente_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_puntoservico_ori  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_canal_origen      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_subcanal_origen   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_delegacion_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_planta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_sector_origen     IN gepr_pcomon_###VERSION###.tipo$oids_

                                   /* cuenta destino */,
                                   par$adoc_oid_cuenta_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_cliente_destino  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_subcliente_des   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_puntoservico_des IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_canal_destino    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_subcanal_destino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_delegacion_des   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_planta_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoc_oid_sector_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* arrays asociativos */
                                   /* arrays de efectivo por documento */
                                   par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_

                                   /* arrays de medio de pago por documento */,
                                   par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$ampdoc_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* arrays de terminos de medio de pago por documento */
                                   par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpdoc_oid_mediopago   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                   -- contenedores
                                   par$adoccont_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoccont_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   -- Elementos
                                   par$aelco_oid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* informacion de ejecucion */
                                   par$usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$info_ejecucion IN VARCHAR2,
                                   par$hacer_commit   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$confirmar_doc  in gepr_pcomon_###VERSION###.tipo$nbol_,
                                   par$inserts        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$cod_ejecucion  OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /*const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_docs_contenedor';*/

    /* variables locales */
    var$esnuevo              BOOLEAN;
    var$inicio               gepr_pcomon_###VERSION###.tipo$gmt_;
    var$cur_formulario       gepr_pcomon_###VERSION###.tipo$formularios;
    var$registros            gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                  VARCHAR2(4000);
    var$adoc_cod_comprobante gepr_pcomon_###VERSION###.tipo$cods_;
    var$cod_estado_doc       gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver               gepr_pcomon_###VERSION###.tipo$nel_;
    var$array_fecha_vacio    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$array_desc_vacio      gepr_pcomon_###VERSION###.tipo$descs_;

  BEGIN

    /* Valores iniciales */
    var$inicio := SYSTIMESTAMP;

    if par$cod_formulario_grupo IS NOT NULL and
       par$oid_formulario_grupo IS NULL THEN

      /* Recupera o formulario com suas caracteristicas */
      var$cur_formulario := sapr_pformulario_###VERSION###.frecuperar_formulario(par$oid_formulario => par$oid_formulario_grupo,
                                                                            par$cod_formulario => par$cod_formulario_grupo,
                                                                            par$selects        => par$selects);

      par$selects   := par$selects + var$selects;
      var$registros := var$registros + par$selects;

    END IF;

    /* Grabar las informaciones del grupo */
    sguardar_grp_documentos(par$oid_grupo_documento       => par$oid_grupo_documento,
                            par$oid_grupo_documento_padre => par$oid_grupo_documento_padre,
                            par$oid_formulario_grupo      => var$cur_formulario(1)
                                                             .OID_FORMULARIO,
                            par$oid_sector_origen         => par$oid_sector_origen,
                            par$oid_sector_destino        => par$oid_sector_destino,
                            par$usuario                   => par$usuario,
                            par$rowver                    => par$rowver,
                            par$adocs_oid                 => par$adoc_oid_documento,
                            par$avtgdoc_oid_termino       => par$avtgdoc_oid_termino,
                            par$avtgdoc_des_valor         => par$avtgdoc_des_valor,
                            par$esnuevo                   => var$esnuevo,
                            par$inserts                   => var$inserts,
                            par$updates                   => var$updates,
                            par$deletes                   => var$deletes,
                            par$selects                   => var$selects,
                            par$cod_cultura               => par$cod_cultura);

    var$registros := var$registros + var$inserts + var$updates +
                     var$selects + var$deletes;
    par$inserts   := par$inserts + var$inserts;
    par$updates   := par$updates + var$updates;
    par$selects   := par$selects + var$selects;
    par$deletes   := par$deletes + var$deletes;

    /* Grabar los documentos del grupo */
    sapr_pdocumento_###VERSION###.sguardar_doc_contenedor(par$oid_formulario      => par$oid_formulario_grupo,
                                                     par$cod_formulario      => par$cod_formulario_grupo,
                                                     par$oid_grupo_documento => par$oid_grupo_documento,

                                                     par$adoc_oid_documento       => par$adoc_oid_documento,
                                                     par$adoc_oid_documento_padre => par$adoc_oid_documento_padre,
                                                     par$adoc_oid_documento_sust  => par$adoc_oid_documento_sust,
                                                     par$adoc_oid_moviment_fondo  => par$adoc_oid_moviment_fondo,
                                                     par$adoc_fyh_plan_certif     => par$adoc_fyh_plan_certif,
                                                     par$adoc_fyh_gestion         => par$adoc_fyh_gestion,
                                                     par$adoc_fyh_contable        => var$array_fecha_vacio,                                 
                                                     par$adoc_cod_actual_id       => var$array_desc_vacio,
                                                     par$adoc_collection_id       => var$array_desc_vacio,
                                                     par$adoc_cod_externo         => par$adoc_cod_externo,
                                                     par$adoc_cod_comprobante     => var$adoc_cod_comprobante,
                                                     par$adoc_rowver              => par$adoc_rowver,

                                                     par$adoc_oid_cuenta_origen     => par$adoc_oid_cuenta_origen,
                                                     par$adoc_oid_cliente_origen    => par$adoc_oid_cliente_origen,
                                                     par$adoc_oid_subcliente_origen => par$adoc_oid_subcliente_origen,
                                                     par$adoc_oid_puntoservico_ori  => par$adoc_oid_puntoservico_ori,
                                                     par$adoc_oid_canal_origen      => par$adoc_oid_canal_origen,
                                                     par$adoc_oid_subcanal_origen   => par$adoc_oid_subcanal_origen,
                                                     par$adoc_oid_delegacion_origen => par$adoc_oid_delegacion_origen,
                                                     par$adoc_oid_planta_origen     => par$adoc_oid_planta_origen,
                                                     par$adoc_oid_sector_origen     => par$adoc_oid_sector_origen,

                                                     par$adoc_oid_cuenta_destino   => par$adoc_oid_cuenta_destino,
                                                     par$adoc_oid_cliente_destino  => par$adoc_oid_cliente_destino,
                                                     par$adoc_oid_subcliente_des   => par$adoc_oid_subcliente_des,
                                                     par$adoc_oid_puntoservico_des => par$adoc_oid_puntoservico_des,
                                                     par$adoc_oid_canal_destino    => par$adoc_oid_canal_destino,
                                                     par$adoc_oid_subcanal_destino => par$adoc_oid_subcanal_destino,
                                                     par$adoc_oid_delegacion_des   => par$adoc_oid_delegacion_des,
                                                     par$adoc_oid_planta_destino   => par$adoc_oid_planta_destino,
                                                     par$adoc_oid_sector_destino   => par$adoc_oid_sector_destino,

                                                     par$aefdoc_oid_documento    => par$aefdoc_oid_documento,
                                                     par$aefdoc_oid_divisa       => par$aefdoc_oid_divisa,
                                                     par$aefdoc_oid_denominacion => par$aefdoc_oid_denominacion,
                                                     par$aefdoc_oid_unid_medida  => par$aefdoc_oid_unid_medida,
                                                     par$aefdoc_cod_niv_detalle  => par$aefdoc_cod_niv_detalle,
                                                     par$aefdoc_cod_tp_efec_tot  => par$aefdoc_cod_tp_efec_tot,
                                                     par$aefdoc_oid_calidad      => par$aefdoc_oid_calidad,
                                                     par$aefdoc_num_importe      => par$aefdoc_num_importe,
                                                     par$aefdoc_nel_cantidad     => par$aefdoc_nel_cantidad,

                                                     par$ampdoc_oid_documento     => par$ampdoc_oid_documento,
                                                     par$ampdoc_oid_divisa        => par$ampdoc_oid_divisa,
                                                     par$ampdoc_oid_medio_pago    => par$ampdoc_oid_medio_pago,
                                                     par$ampdoc_cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago,
                                                     par$ampdoc_cod_nivel_detalle => par$ampdoc_cod_nivel_detalle,
                                                     par$ampdoc_num_importe       => par$ampdoc_num_importe,
                                                     par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,
                                                     par$ampdoc_oid_unidad_medida => par$ampdoc_oid_unidad_medida,

                                                     par$avtmpdoc_oid_documento   => par$avtmpdoc_oid_documento,
                                                     par$avtmpdoc_oid_mediopago   => par$avtmpdoc_oid_mediopago,
                                                     par$avtmpdoc_oid_t_mediopago => par$avtmpdoc_oid_t_mediopago,
                                                     par$avtmpdoc_des_valor       => par$avtmpdoc_des_valor,
                                                     par$avtmpdoc_nec_indice_grp  => par$avtmpdoc_nec_indice_grp,

                                                     par$adoccont_oid_documento     => par$adoccont_oid_documento,
                                                     par$adoccont_oid_contenedor    => par$adoccont_oid_contenedor,
                                                     par$adoccont_rowver_contenedor => par$adoccont_rowver_contenedor,

                                                     par$aelco_oid_contenedor => par$aelco_oid_contenedor,
                                                     par$aelco_tipo_elemento  => par$aelco_tipo_elemento,
                                                     par$aelco_precintos_ele  => par$aelco_precintos_ele,
                                                     par$aelco_ids_elemento   => par$aelco_ids_elemento,
                                                     par$usuario              => par$usuario,
                                                     par$cod_cultura          => par$cod_cultura,
                                                     par$info_ejecucion       => par$info_ejecucion,
                                                     par$hacer_commit         => par$hacer_commit,
                                                     par$confirmar_doc        => 0,
                                                     par$inserts              => par$inserts,
                                                     par$updates              => par$updates,
                                                     par$deletes              => par$deletes,
                                                     par$selects              => par$selects,
                                                     par$cod_ejecucion        => par$cod_ejecucion);

    var$registros := var$registros + var$inserts + var$updates +
                     var$selects + var$deletes;
    par$inserts   := par$inserts + var$inserts;
    par$updates   := par$updates + var$updates;
    par$selects   := par$selects + var$selects;
    par$deletes   := par$deletes + var$deletes;

    if par$confirmar_doc > 0 then

      var$cod_estado_doc := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
      var$rowver         := par$rowver;

      sapr_ptransiciones_###VERSION###.sejecutar_grp_doc(par$oid_grupo_documento  => par$oid_grupo_documento,
                                                    par$cod_estado_documento => var$cod_estado_doc,
                                                    par$cod_comprobante      => par$cod_comprobante,
                                                    par$rowver               => var$rowver,
                                                    par$usuario              => par$usuario,
                                                    par$cod_cultura          => par$cod_cultura,
                                                    par$info_ejecucion       => par$info_ejecucion,
                                                    par$cod_ejecucion        => par$cod_ejecucion,
                                                    par$hacer_commit         => 0,
                                                    par$inserts              => par$inserts,
                                                    par$updates              => par$updates,
                                                    par$deletes              => par$deletes,
                                                    par$selects              => par$selects);

      var$registros := var$registros + var$inserts + var$updates +
                       var$selects + var$deletes;
      par$inserts   := par$inserts + var$inserts;
      par$updates   := par$updates + var$updates;
      par$selects   := par$selects + var$selects;
      par$deletes   := par$deletes + var$deletes;

    end if;

    IF par$hacer_commit > 0 THEN
      COMMIT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'C',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);
    END IF;

  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
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
                                                           par$usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END sguardar_grp_docs_cont;

  /* Guardar grupo - Gestion de Contenedores */
  PROCEDURE sguardar_grp_docs_cont_alta(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_formulario_grupo      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_comprobante           OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                        /*Terminos Grupo documento*/
                                        par$avtgdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtgdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                                        --documentos
                                        par$adoc_oid_documento       in gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_documento_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_documento_sust  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_moviment_fondo  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_fyh_plan_certif     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                        par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                        par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoc_rowver              IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        /* cuenta origen */
                                        par$adoc_oid_cuenta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_cliente_origen    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_subcliente_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_puntoservico_ori  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_canal_origen      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_subcanal_origen   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_delegacion_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_planta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_sector_origen     IN gepr_pcomon_###VERSION###.tipo$oids_

                                        /* cuenta destino */,
                                        par$adoc_oid_cuenta_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_cliente_destino  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_subcliente_des   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_puntoservico_des IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_canal_destino    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_subcanal_destino IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_delegacion_des   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_planta_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoc_oid_sector_destino   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                        /* arrays asociativos */
                                        /* arrays de efectivo por documento */
                                        par$aefdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_

                                        /* arrays de medio de pago por documento */,
                                        par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        par$ampdoc_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                        /* arrays de terminos de medio de pago por documento */
                                        par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpdoc_oid_mediopago   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                        -- contenedores
                                        par$adoccont_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoccont_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$adoccont_cod_tipo_cont     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_cod_tipo_servicio IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_cod_tipo_embalaje IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_cod_puesto        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_cod_contenedor    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                        -- Precintos
                                        par$aprco_oid_contenedor       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aprco_bol_precinto_autom   in gepr_pcomon_###VERSION###.tipo$nbols_,

                                        -- Elementos
                                        par$aelco_oid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        /* arrays de efectivo do contenedor */
                                        par$aefco_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_oid_denom         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_oid_uni_med       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aefco_cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$aefco_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aefco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$aefco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                        /* arrays de medio de pago do contenedor */
                                        par$ampco_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampco_oid_mp            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$ampco_cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ampco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ampco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$ampco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        par$ampco_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                        /* arrays de terminos de medio de pago do contenedor */
                                        par$avtmpco_oid_contenedor   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpco_oid_mp           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpco_oid_termediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$avtmpco_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$avtmpco_nec_indicegrupo  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                        /* informacion de ejecucion */
                                        par$usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion IN VARCHAR2,
                                        par$hacer_commit   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$confirmar_doc  in gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$inserts        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$updates        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_ejecucion  OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /*const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_grp_docs_contenedor';*/

    /* variables locales */
    var$esnuevo        BOOLEAN;
    var$inicio         gepr_pcomon_###VERSION###.tipo$gmt_;
    var$cur_formulario gepr_pcomon_###VERSION###.tipo$formularios;
    var$registros      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges         gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err            VARCHAR2(4000);
    /*var$adoc_cod_comprobante gepr_pcomon_###VERSION###.tipo$cods_;*/
    var$cod_estado_doc gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver         gepr_pcomon_###VERSION###.tipo$nel_;
    var$array_fecha_vacio    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$array_desc_vacio      gepr_pcomon_###VERSION###.tipo$descs_;
  BEGIN

    /* Valores iniciales */
    var$inicio := SYSTIMESTAMP;

    if par$cod_formulario_grupo IS NOT NULL and
       par$oid_formulario_grupo IS NULL THEN

      /* Recupera o formulario com suas caracteristicas */
      var$cur_formulario := sapr_pformulario_###VERSION###.frecuperar_formulario(par$oid_formulario => par$oid_formulario_grupo,
                                                                            par$cod_formulario => par$cod_formulario_grupo,
                                                                            par$selects        => par$selects);

    END IF;

    /* Grabar las informaciones del grupo */
    sguardar_grp_documentos(par$oid_grupo_documento       => par$oid_grupo_documento,
                            par$oid_grupo_documento_padre => par$oid_grupo_documento_padre,
                            par$oid_formulario_grupo      => var$cur_formulario(1)
                                                             .OID_FORMULARIO,
                            par$oid_sector_origen         => par$oid_sector_origen,
                            par$oid_sector_destino        => par$oid_sector_destino,
                            par$usuario                   => par$usuario,
                            par$rowver                    => par$rowver,
                            par$adocs_oid                 => par$adoc_oid_documento,
                            par$avtgdoc_oid_termino       => par$avtgdoc_oid_termino,
                            par$avtgdoc_des_valor         => par$avtgdoc_des_valor,
                            par$esnuevo                   => var$esnuevo,
                            par$inserts                   => var$inserts,
                            par$updates                   => var$updates,
                            par$deletes                   => var$deletes,
                            par$selects                   => var$selects,
                            par$cod_cultura               => par$cod_cultura);

    var$registros := var$registros + var$inserts + var$updates +
                     var$selects + var$deletes;
    par$inserts   := par$inserts + var$inserts;
    par$updates   := par$updates + var$updates;
    par$selects   := par$selects + var$selects;
    par$deletes   := par$deletes + var$deletes;

    /* Grabar los documentos del grupo */
    sapr_pdocumento_###VERSION###.sguardar_doc_contenedor_alta(par$oid_formulario      => par$oid_formulario_grupo,
                                                          par$cod_formulario      => par$cod_formulario_grupo,
                                                          par$oid_grupo_documento => par$oid_grupo_documento,
                                                          par$cod_comprobante     => par$cod_comprobante,

                                                          par$adoc_oid_documento       => par$adoc_oid_documento,
                                                          par$adoc_oid_documento_padre => par$adoc_oid_documento_padre,
                                                          par$adoc_oid_documento_sust  => par$adoc_oid_documento_sust,
                                                          par$adoc_oid_moviment_fondo  => par$adoc_oid_moviment_fondo,
                                                          par$adoc_fyh_plan_certif     => par$adoc_fyh_plan_certif,
                                                          par$adoc_fyh_gestion         => par$adoc_fyh_gestion,
                                                          par$adoc_fyh_contable        => var$array_fecha_vacio,                                 
                                                          par$adocs_cod_actual_id      => var$array_desc_vacio,
                                                          par$adocs_collection_id      => var$array_desc_vacio,
                                                          par$adoc_cod_externo         => par$adoc_cod_externo,
                                                          par$adoc_rowver              => par$adoc_rowver,

                                                          par$adoc_oid_cuenta_origen     => par$adoc_oid_cuenta_origen,
                                                          par$adoc_oid_cliente_origen    => par$adoc_oid_cliente_origen,
                                                          par$adoc_oid_subcliente_origen => par$adoc_oid_subcliente_origen,
                                                          par$adoc_oid_puntoservico_ori  => par$adoc_oid_puntoservico_ori,
                                                          par$adoc_oid_canal_origen      => par$adoc_oid_canal_origen,
                                                          par$adoc_oid_subcanal_origen   => par$adoc_oid_subcanal_origen,
                                                          par$adoc_oid_delegacion_origen => par$adoc_oid_delegacion_origen,
                                                          par$adoc_oid_planta_origen     => par$adoc_oid_planta_origen,
                                                          par$adoc_oid_sector_origen     => par$adoc_oid_sector_origen,

                                                          par$adoc_oid_cuenta_destino   => par$adoc_oid_cuenta_destino,
                                                          par$adoc_oid_cliente_destino  => par$adoc_oid_cliente_destino,
                                                          par$adoc_oid_subcliente_des   => par$adoc_oid_subcliente_des,
                                                          par$adoc_oid_puntoservico_des => par$adoc_oid_puntoservico_des,
                                                          par$adoc_oid_canal_destino    => par$adoc_oid_canal_destino,
                                                          par$adoc_oid_subcanal_destino => par$adoc_oid_subcanal_destino,
                                                          par$adoc_oid_delegacion_des   => par$adoc_oid_delegacion_des,
                                                          par$adoc_oid_planta_destino   => par$adoc_oid_planta_destino,
                                                          par$adoc_oid_sector_destino   => par$adoc_oid_sector_destino,

                                                          par$aefdoc_oid_documento    => par$aefdoc_oid_documento,
                                                          par$aefdoc_oid_divisa       => par$aefdoc_oid_divisa,
                                                          par$aefdoc_oid_denominacion => par$aefdoc_oid_denominacion,
                                                          par$aefdoc_oid_unid_medida  => par$aefdoc_oid_unid_medida,
                                                          par$aefdoc_cod_niv_detalle  => par$aefdoc_cod_niv_detalle,
                                                          par$aefdoc_cod_tp_efec_tot  => par$aefdoc_cod_tp_efec_tot,
                                                          par$aefdoc_oid_calidad      => par$aefdoc_oid_calidad,
                                                          par$aefdoc_num_importe      => par$aefdoc_num_importe,
                                                          par$aefdoc_nel_cantidad     => par$aefdoc_nel_cantidad,

                                                          par$ampdoc_oid_documento     => par$ampdoc_oid_documento,
                                                          par$ampdoc_oid_divisa        => par$ampdoc_oid_divisa,
                                                          par$ampdoc_oid_medio_pago    => par$ampdoc_oid_medio_pago,
                                                          par$ampdoc_cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago,
                                                          par$ampdoc_cod_nivel_detalle => par$ampdoc_cod_nivel_detalle,
                                                          par$ampdoc_num_importe       => par$ampdoc_num_importe,
                                                          par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,
                                                          par$ampdoc_oid_unidad_medida => par$ampdoc_oid_unidad_medida,

                                                          par$avtmpdoc_oid_documento   => par$avtmpdoc_oid_documento,
                                                          par$avtmpdoc_oid_mediopago   => par$avtmpdoc_oid_mediopago,
                                                          par$avtmpdoc_oid_t_mediopago => par$avtmpdoc_oid_t_mediopago,
                                                          par$avtmpdoc_des_valor       => par$avtmpdoc_des_valor,
                                                          par$avtmpdoc_nec_indice_grp  => par$avtmpdoc_nec_indice_grp,

                                                          par$adoccont_oid_documento     => par$adoccont_oid_documento,
                                                          par$adoccont_oid_contenedor    => par$adoccont_oid_contenedor,
                                                          par$adoccont_cod_tipo_cont     => par$adoccont_cod_tipo_cont,
                                                          par$adoccont_cod_tipo_servicio => par$adoccont_cod_tipo_servicio,
                                                          par$adoccont_cod_tipo_embalaje => par$adoccont_cod_tipo_embalaje,
                                                          par$adoccont_cod_puesto        => par$adoccont_cod_puesto,
                                                          par$adoccont_cod_contenedor    => par$adoccont_cod_contenedor,
                                                          par$adoccont_rowver_contenedor => par$adoccont_rowver_contenedor,

                                                          par$aprco_oid_contenedor       => par$aprco_oid_contenedor,
                                                          par$aprco_precintos_contenedor => par$aprco_precintos_contenedor,
                                                          par$aprco_bol_precinto_autom   => par$aprco_bol_precinto_autom,

                                                          par$aelco_oid_contenedor     => par$aelco_oid_contenedor,
                                                          par$aelco_tipo_elemento      => par$aelco_tipo_elemento,
                                                          par$aelco_precintos_ele      => par$aelco_precintos_ele,
                                                          par$aelco_ids_elemento       => par$aelco_ids_elemento,
                                                          par$aefco_oid_contenedor     => par$aefco_oid_contenedor,
                                                          par$aefco_oid_divisa         => par$aefco_oid_divisa,
                                                          par$aefco_oid_denom          => par$aefco_oid_denom,
                                                          par$aefco_oid_uni_med        => par$aefco_oid_uni_med,
                                                          par$aefco_cod_nivel_det      => par$aefco_cod_nivel_det,
                                                          par$aefco_cod_tipo_efec_tot  => par$aefco_cod_tipo_efec_tot,
                                                          par$aefco_oid_calidad        => par$aefco_oid_calidad,
                                                          par$aefco_num_importe        => par$aefco_num_importe,
                                                          par$aefco_nel_cantidad       => par$aefco_nel_cantidad,
                                                          par$ampco_oid_contenedor     => par$ampco_oid_contenedor,
                                                          par$ampco_oid_divisa         => par$ampco_oid_divisa,
                                                          par$ampco_oid_mp             => par$ampco_oid_mp,
                                                          par$ampco_cod_tipo_mp        => par$ampco_cod_tipo_mp,
                                                          par$ampco_cod_nivel_det      => par$ampco_cod_nivel_det,
                                                          par$ampco_num_importe        => par$ampco_num_importe,
                                                          par$ampco_nel_cantidad       => par$ampco_nel_cantidad,
                                                          par$ampco_oid_unidad_medida  => par$ampco_oid_unidad_medida,
                                                          par$avtmpco_oid_contenedor   => par$avtmpco_oid_contenedor,
                                                          par$avtmpco_oid_mp           => par$avtmpco_oid_mp,
                                                          par$avtmpco_oid_termediopago => par$avtmpco_oid_termediopago,
                                                          par$avtmpco_des_valor        => par$avtmpco_des_valor,
                                                          par$avtmpco_nec_indicegrupo  => par$avtmpco_nec_indicegrupo,
                                                          par$usuario                  => par$usuario,
                                                          par$cod_cultura              => par$cod_cultura,
                                                          par$info_ejecucion           => par$info_ejecucion,
                                                          par$hacer_commit             => par$hacer_commit,
                                                          par$confirmar_doc            => 0,
                                                          par$inserts                  => var$inserts,
                                                          par$updates                  => var$updates,
                                                          par$deletes                  => var$deletes,
                                                          par$selects                  => var$selects,
                                                          par$cod_ejecucion            => par$cod_ejecucion);

    var$registros := var$registros + var$inserts + var$updates +
                     var$selects + var$deletes;
    par$inserts   := par$inserts + var$inserts;
    par$updates   := par$updates + var$updates;
    par$selects   := par$selects + var$selects;
    par$deletes   := par$deletes + var$deletes;

    if par$confirmar_doc > 0 then

      var$cod_estado_doc := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
      var$rowver         := par$rowver;

      sapr_ptransiciones_###VERSION###.sejecutar_grp_doc(par$oid_grupo_documento  => par$oid_grupo_documento,
                                                    par$cod_estado_documento => var$cod_estado_doc,
                                                    par$cod_comprobante      => par$cod_comprobante,
                                                    par$rowver               => var$rowver,
                                                    par$usuario              => par$usuario,
                                                    par$cod_cultura          => par$cod_cultura,
                                                    par$info_ejecucion       => par$info_ejecucion,
                                                    par$cod_ejecucion        => par$cod_ejecucion,
                                                    par$hacer_commit         => 0,
                                                    par$inserts              => var$inserts,
                                                    par$updates              => var$updates,
                                                    par$deletes              => var$deletes,
                                                    par$selects              => var$selects);

      var$registros := var$registros + var$inserts + var$updates +
                       var$selects + var$deletes;
      par$inserts   := par$inserts + var$inserts;
      par$updates   := par$updates + var$updates;
      par$selects   := par$selects + var$selects;
      par$deletes   := par$deletes + var$deletes;

    end if;

    IF par$hacer_commit > 0 THEN
      COMMIT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'C',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);
    END IF;

  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
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
                                                           par$usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END sguardar_grp_docs_cont_alta;

  /* srecuperar_grp_documentos */
  PROCEDURE srecuperar_grp_documentos(par$oid_grupo_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$rc_grupo_documentos        OUT sys_refcursor,
                                      par$rc_grupo_terminos_grupo    OUT sys_refcursor,
                                      par$rc_terminos_grupo          OUT sys_refcursor,
                                      par$rc_valor_terminos_grupo    OUT sys_refcursor,
                                      par$rc_historico_mov_grupo     OUT sys_refcursor,
                                      par$doc_rc_documentos          OUT sys_refcursor,
                                      par$doc_rc_documentos_padre    OUT sys_refcursor,
                                      par$doc_rc_formulario          OUT sys_refcursor,
                                      par$doc_rc_accion_contable     OUT sys_refcursor,
                                      par$doc_rc_estado_acc_contable OUT sys_refcursor,
                                      par$doc_rc_caract_formulario   OUT sys_refcursor,
                                      par$doc_rc_grp_terminos_indiv  OUT sys_refcursor,
                                      par$doc_rc_terminos_indiv      OUT sys_refcursor,
                                      par$doc_rc_valor_terminos_doc  OUT sys_refcursor,
                                      par$doc_rc_sectores            OUT sys_refcursor,
                                      par$doc_rc_tipos_sectores      OUT sys_refcursor,
                                      par$doc_rc_caract_tp_sectores  OUT sys_refcursor,
                                      par$doc_rc_cuentas             OUT sys_refcursor,
                                      par$doc_rc_historico_mov_doc   OUT sys_refcursor,
                                      par$doc_rc_valores_documentos  OUT sys_refcursor,
                                      par$doc_rc_divisas             OUT sys_refcursor,
                                      par$doc_rc_denominaciones      OUT sys_refcursor,
                                      par$doc_rc_mediospago          OUT sys_refcursor,
                                      par$doc_rc_unidades_medida     OUT sys_refcursor,
                                      par$doc_rc_calidades           OUT sys_refcursor,
                                      par$doc_rc_terminos_mediospago OUT sys_refcursor,
                                      par$doc_rc_plantas             OUT sys_refcursor,
                                      par$doc_rc_delegaciones        OUT sys_refcursor,
                                      par$doc_rc_tipos_sector_planta OUT sys_refcursor,
                                      par$ele_rc_elementos           OUT sys_refcursor,
                                      par$ele_rc_cuentas             OUT sys_refcursor,
                                      par$ele_rc_carac_tipo_sector   OUT sys_refcursor,
                                      par$ele_rc_val_det_efectivo    OUT sys_refcursor,
                                      par$ele_rc_val_det_medio_pago  OUT sys_refcursor,
                                      par$ele_rc_val_totales         OUT sys_refcursor,
                                      par$ele_rc_lista_valor         OUT sys_refcursor,
                                      par$ele_rc_iac                 OUT sys_refcursor,
                                      par$ele_rc_terminos_iac        OUT sys_refcursor,
                                      par$ele_rc_valor_termino_iac   OUT sys_refcursor,
                                      par$ele_rc_divisas             OUT sys_refcursor,
                                      par$ele_rc_denominaciones      OUT sys_refcursor,
                                      par$ele_rc_medio_pago          OUT sys_refcursor,
                                      par$ele_rc_unidad_medida       OUT sys_refcursor,
                                      par$ele_rc_calidad             OUT sys_refcursor,
                                      par$ele_rc_valor_termino       OUT sys_refcursor,
                                      par$ele_rc_cont_precintos      OUT sys_refcursor,
                                      par$usuario                    IN VARCHAR2,
                                      par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$info_ejecucion             IN VARCHAR2,
                                      par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_)

   IS
    var$inicio          gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros       gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts         gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates         gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes         gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges          gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects         gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts_int     gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int     gepr_pcomon_###VERSION###.tipo$nel_;
    var$oids_documentos gepr_pcomon_###VERSION###.tipo$oids_;
    var$err             varchar2(4000);

    /* CURSOR DE ELEMENTOS */
    CURSOR cur$elementos(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT D.OID_DOCUMENTO
        FROM SAPR_TDOCUMENTO D
       WHERE D.OID_GRUPO_DOCUMENTO = par$oid_grupo_documento
       GROUP BY D.OID_DOCUMENTO;

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

    /* - Documentos y Documentos Padres */
    sapr_pdocumento_###VERSION###.scargar_tt_oids_documento_gpr(par$oid_grupo_documento => par$oid_grupo_documento,
                                                           par$recuperar_padres    => true,
                                                           par$inserts             => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Formularios */
    sapr_pformulario_###VERSION###.scargar_tt_oids_form_gpr(par$oid_grupo_documento => par$oid_grupo_documento,
                                                       par$recuperar_padres    => true,
                                                       par$inserts             => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Terminos - Formularios */
    sapr_pformulario_###VERSION###.scargar_tt_oids_term_gpr;
    var$inserts   := var$inserts + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;

    /* - Cuentas */
    sapr_pcuentas_###VERSION###.scargar_tt_oids_cuentas_gpr(par$oid_grupo_documento => par$oid_grupo_documento,
                                                       par$inserts             => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Sectores */
    sapr_psector_###VERSION###.scargar_tt_oids_sector_gpr(par$oid_grupo_documento => par$oid_grupo_documento,
                                                     par$selects             => var$selects_int,
                                                     par$inserts             => var$inserts_int);
    var$selects   := var$selects + var$selects_int;
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$selects_int + var$inserts_int;

    /* - Sectores Cuentas */
    sapr_psector_###VERSION###.scargar_tt_oids_sector_cuen(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Divisas */
    sapr_pdivisa_###VERSION###.scargar_tt_oids_divisa_gpr(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Plantas */
    sapr_pplanta_###VERSION###.scargar_tt_oids_planta_gpr(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* - Tipos Sectores */
    sapr_psector_###VERSION###.scargar_tt_oids_tipo_sec_gpr(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    /* ===== Cargar refcursor ==== */

    /* - TABLA: Grupo de Documentos */
    par$rc_grupo_documentos := fcargar_rc_grupo_documento(par$oid_grupo_documento,
                                                          par$selects => var$selects_int);
    var$selects             := var$selects + var$selects_int;
    var$registros           := var$registros + var$selects_int;

    /* - TABLA: Documentos */
    par$doc_rc_documentos := sapr_pdocumento_###VERSION###.fcargar_rc_documento;
    var$selects           := var$selects + SQL % ROWCOUNT;
    var$registros         := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Documentos Padres */
    par$doc_rc_documentos_padre := sapr_pdocumento_###VERSION###.fcargar_rc_doc_padre;
    var$selects                 := var$selects + SQL % ROWCOUNT;
    var$registros               := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Formularios */
    par$doc_rc_formulario := sapr_pformulario_###VERSION###.fcargar_rc_formulario;
    var$selects           := var$selects + SQL % ROWCOUNT;
    var$registros         := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Formularios - Accion Contable */
    par$doc_rc_accion_contable := sapr_pformulario_###VERSION###.fcargar_rc_AccionContable;
    var$selects                := var$selects + SQL % ROWCOUNT;
    var$registros              := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Formularios - Estado Accion Contable */
    par$doc_rc_estado_acc_contable := sapr_pformulario_###VERSION###.fcargar_rc_EstadoAccContable;
    var$selects                    := var$selects + SQL % ROWCOUNT;
    var$registros                  := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Formularios - Caracteristicas */
    par$doc_rc_caract_formulario := sapr_pformulario_###VERSION###.fcargar_rc_CaractFormulario;
    var$selects                  := var$selects + SQL % ROWCOUNT;
    var$registros                := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Formularios - GrupoTerminosIndividual */
    par$doc_rc_grp_terminos_indiv := sapr_pformulario_###VERSION###.fcargar_rc_GrupoTerminosIndiv;
    var$selects                   := var$selects + SQL % ROWCOUNT;
    var$registros                 := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Formularios - TerminosIndividual */
    par$doc_rc_terminos_indiv := sapr_pformulario_###VERSION###.fcargar_rc_TerminosIndiv;
    var$selects               := var$selects + SQL % ROWCOUNT;
    var$registros             := var$registros + SQL % ROWCOUNT;

    --para que no falle: CORREGIR !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    OPEN par$rc_grupo_terminos_grupo FOR
      SELECT '?' FROM dual WHERE 0 = 1;
    OPEN par$rc_terminos_grupo FOR
      SELECT '?' FROM dual WHERE 0 = 1;

    /* - TABLA: Valor terminos Grupo de Documentos */
    par$rc_valor_terminos_grupo := fcargar_rc_valorterminosgrupo(par$oid_grupo_documento,
                                                                 par$selects => var$selects_int);
    var$selects                 := var$selects + var$selects_int;
    var$registros               := var$registros + var$selects_int;

    /* - TABLA: Valor terminos documentos */
    par$doc_rc_valor_terminos_doc := sapr_pdocumento_###VERSION###.fcargar_rc_valorterminosdoc;
    var$selects                   := var$selects + SQL % ROWCOUNT;
    var$registros                 := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Sectores */
    par$doc_rc_sectores := sapr_psector_###VERSION###.fcargar_rc_sectores(par$selects => var$selects_int);
    var$selects         := var$selects + var$selects_int;
    var$registros       := var$registros + var$selects_int;

    /* - TABLA: Tipos de Sectores */
    par$doc_rc_tipos_sectores := sapr_psector_###VERSION###.fcargar_rc_tipossectores(par$selects => var$selects_int);
    var$selects               := var$selects + var$selects_int;
    var$registros             := var$registros + var$selects_int;

    /* - TABLA: Sectores */
    par$doc_rc_caract_tp_sectores := sapr_psector_###VERSION###.fcargar_rc_caractpsectores(par$selects => var$selects_int);
    var$selects                   := var$selects + var$selects_int;
    var$registros                 := var$registros + var$selects_int;

    /* - TABLA: Cuentas */
    par$doc_rc_cuentas := sapr_pcuentas_###VERSION###.fcargar_rc_cuentas(par$selects => var$selects_int);
    var$selects        := var$selects + var$selects_int;
    var$registros      := var$registros + var$selects_int;

    /* - TABLA: Historico Grupo */
    par$rc_historico_mov_grupo := fcargar_rc_historico(par$oid_grupo_documento,
                                                       par$selects => var$selects_int);
    var$selects                := var$selects + var$selects_int;
    var$registros              := var$registros + var$selects_int;

    /* - TABLA: Historico Documento */
    par$doc_rc_historico_mov_doc := sapr_pdocumento_###VERSION###.fcargar_rc_historico;
    var$selects                  := var$selects + SQL % ROWCOUNT;
    var$registros                := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Valores de Documentos */
    par$doc_rc_valores_documentos := sapr_pdocumento_###VERSION###.fcargar_rc_valores;
    var$selects                   := var$selects + SQL % ROWCOUNT;
    var$registros                 := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Divisas */
    sapr_pdivisa_###VERSION###.scargar_divisas(par$doc_rc_divisas,
                                          par$doc_rc_denominaciones,
                                          par$doc_rc_mediospago,
                                          par$doc_rc_unidades_medida,
                                          par$doc_rc_calidades,
                                          par$doc_rc_terminos_mediospago,
                                          var$selects_int);
    var$selects   := var$selects + var$selects_int;
    var$registros := var$registros + var$selects_int;

    /* - TABLA: Plantas */
    par$doc_rc_plantas := sapr_pplanta_###VERSION###.fcargar_rc_planta(par$selects => var$selects_int);
    var$selects        := var$selects + var$selects_int;
    var$registros      := var$registros + var$selects_int;

    /* - TABLA: Delegaciones */
    par$doc_rc_delegaciones := sapr_pdelegacion_###VERSION###.fcargar_rc_delegacion;
    var$selects             := var$selects + SQL % ROWCOUNT;
    var$registros           := var$registros + SQL % ROWCOUNT;

    /* - TABLA: Tipos Sector Por Planta */
    par$doc_rc_tipos_sector_planta := sapr_pplanta_###VERSION###.fcargar_rc_tp_sector(par$selects => var$selects_int);
    var$selects                    := var$selects + var$selects_int;
    var$registros                  := var$registros + var$selects_int;

    /* - TABLAS ELEMENTOS */
    FOR rec$elementos IN cur$elementos(par$oid_grupo_documento) LOOP
      var$oids_documentos(var$oids_documentos.count) := rec$elementos.oid_documento;
    END LOOP rec$elementos;

    IF var$oids_documentos.count > 0 THEN
      /* - TABLAS ELEMENTOS */
      SAPR_PELEMENTO_###VERSION###.srecuperar_elementos(par$oids_documentos           => var$oids_documentos,
                                                   par$usuario                   => par$usuario,
                                                   par$ejecucion_interna         => 1,
                                                   par$ele_rc_elementos          => par$ele_rc_elementos,
                                                   par$ele_rc_cuentas            => par$ele_rc_cuentas,
                                                   par$ele_rc_carac_tipo_sector  => par$ele_rc_carac_tipo_sector,
                                                   par$ele_rc_val_det_efectivo   => par$ele_rc_val_det_efectivo,
                                                   par$ele_rc_val_det_medio_pago => par$ele_rc_val_det_medio_pago,
                                                   par$ele_rc_val_totales        => par$ele_rc_val_totales,
                                                   par$ele_rc_lista_valor        => par$ele_rc_lista_valor,
                                                   par$ele_rc_iac                => par$ele_rc_iac,
                                                   par$ele_rc_terminos_iac       => par$ele_rc_terminos_iac,
                                                   par$ele_rc_valor_termino_iac  => par$ele_rc_valor_termino_iac,
                                                   par$ele_rc_divisas            => par$ele_rc_divisas,
                                                   par$ele_rc_denominaciones     => par$ele_rc_denominaciones,
                                                   par$ele_rc_medio_pago         => par$ele_rc_medio_pago,
                                                   par$ele_rc_unidad_medida      => par$ele_rc_unidad_medida,
                                                   par$ele_rc_calidad            => par$ele_rc_calidad,
                                                   par$ele_rc_valor_termino      => par$ele_rc_valor_termino,
                                                   par$ele_rc_cont_precintos     => par$ele_rc_cont_precintos,
                                                   par$inserts                   => var$inserts_int,
                                                   par$selects                   => var$selects_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$selects   := var$selects + var$selects_int;
      var$registros := var$registros + var$selects_int + var$inserts_int;
    END IF;

    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in('srecuperar_grp_documentos',
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
                                                         par$usuario,
                                                         par$cod_ejecucion);

  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in('srecuperar_grp_documentos',
                                                           var$inserts,
                                                           var$updates,
                                                           var$deletes,
                                                           var$merges,
                                                           var$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'E',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);

      RAISE_APPLICATION_ERROR(-20101, 'Error ', true);

  END srecuperar_grp_documentos;

  /* supd_tgrupo_documento */
  PROCEDURE supd_tgrupo_documento(par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_grupo_documento_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_formulario            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_sector_origen         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_sector_destino        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_estado                IN gepr_pcomon_###VERSION###.tipo$est_,
                                  par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$rowver                    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$esnuevo                   OUT BOOLEAN,
                                  par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$updates                   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$rowveractual NUMBER(38);
    var$gmt_zero     VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN

    IF par$rowver IS NULL THEN
      /*es nuevo */

      /*verificar que no exista */
      IF NOT fexiste_documentos_grp(par$oid_grupo_documento) THEN
        /* insertarlo */
        INSERT INTO sapr_tgrupo_documento
          (oid_grupo_documento,
           oid_grupo_documento_padre,
           oid_formulario,
           oid_sector_origen,
           oid_sector_destino,
           cod_estado,
           gmt_creacion,
           des_usuario_creacion,
           gmt_modificacion,
           des_usuario_modificacion)
        VALUES
          (par$oid_grupo_documento,
           par$oid_grupo_documento_padre,
           par$oid_formulario,
           par$oid_sector_origen,
           par$oid_sector_destino,
           par$cod_estado,
           var$gmt_zero,
           par$usuario,
           var$gmt_zero,
           par$usuario);
        par$esnuevo := true;
        par$inserts := sql%rowcount;
      ELSE
        /* ya existia */
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msgregistroyaexistente',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      END IF;

    ELSE

      /* es actualizacion */
      /* verificar concurrencia */
      SELECT NVL(rowver, -1)
        INTO var$rowveractual
        FROM sapr_tgrupo_documento
       WHERE oid_grupo_documento = par$oid_grupo_documento;

      IF var$rowveractual = -1 THEN
        /* no existia */
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msgregistroinexistente',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      ELSE
        IF var$rowveractual = par$rowver THEN

          /* actualizarlo */
          UPDATE sapr_tgrupo_documento
             SET oid_grupo_documento_padre = par$oid_grupo_documento_padre,
                 oid_formulario            = par$oid_formulario,
                 oid_sector_origen         = par$oid_sector_origen,
                 oid_sector_destino        = par$oid_sector_destino,
                 cod_estado                = par$cod_estado,
                 gmt_modificacion          = var$gmt_zero,
                 des_usuario_modificacion  = par$usuario
           WHERE oid_grupo_documento = par$oid_grupo_documento;
          par$esnuevo := false;
          par$updates := sql%rowcount;
        ELSE
          /* hubo un problema de concurrencia */
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msgconcurrenciasobreregistro',
                                                                      gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                      gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                      NULL,
                                                                      1));
          RETURN;
        END IF;
      END IF;

    END IF;
  END supd_tgrupo_documento;

  /* supd_est_comp_grp_documento */
  PROCEDURE supd_est_comp_grp_documento(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_estado_grp_doc  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_comprobante     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN

    UPDATE sapr_tgrupo_documento
       SET cod_estado               = par$cod_estado_grp_doc,
           cod_comprobante          = par$cod_comprobante,
           des_usuario_modificacion = par$usuario,
           gmt_modificacion         = var$gmt_zero
     WHERE oid_grupo_documento = par$oid_grupo_documento;

  END supd_est_comp_grp_documento;

  /* supd_estado_grupo_documento */
  PROCEDURE supd_estado_grupo_documento(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_estado_grp_doc  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN

    UPDATE sapr_tgrupo_documento
       SET cod_estado               = par$cod_estado_grp_doc,
           des_usuario_modificacion = par$usuario,
           gmt_modificacion         = var$gmt_zero
     WHERE oid_grupo_documento = par$oid_grupo_documento;

  END supd_estado_grupo_documento;

  /* sins_tdocumento_elemento */
  PROCEDURE sins_tgrupo_documento_cont(par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$oid_contenedor       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_contenedor_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_remesa           in gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_bulto            in gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_) AS

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN

    INSERT INTO sapr_tgrupodocumxcontenedor
      (OID_GRUPODOCUMENTOXCONTENEDOR,
       OID_GRUPO_DOCUMENTO,
       OID_CONTENEDOR_PADRE,
       OID_CONTENEDOR,
       OID_REMESA,
       OID_BULTO,
       COD_ESTADO_GRUPODOCXELEMENTO,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION)
    VALUES
      (sys_guid(),
       par$oid_grupo_documento,
       par$oid_contenedor_padre,
       par$oid_contenedor,
       par$oid_remesa,
       par$oid_bulto,
       gepr_pcomon_###VERSION###.const$CodEstadoDocEleTransito,
       var$gmt_zero,
       par$cod_usuario,
       var$gmt_zero,
       par$cod_usuario);

    par$inserts := sql%rowcount;
  END sins_tgrupo_documento_cont;

  /* sins_thist_mov_grupo_docs */
  PROCEDURE sins_thist_mov_grupo_docs(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_thist_mov_grupo_documento
      (oid_hist_mov_grupo_documento,
       oid_grupo_documento,
       cod_estado,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_grupo_documento,
       par$cod_estado,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_thist_mov_grupo_docs;

  /* sins_tvalor_termxgrupodoc */
  PROCEDURE sins_tvalor_termxgrupodoc(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_termino         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$des_valor           IN gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_tvalor_terminoxgrupodoc
      (oid_valor_terminoxgrupodoc,
       oid_grupo_documento,
       oid_termino,
       des_valor,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_grupo_documento,
       par$oid_termino,
       par$des_valor,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tvalor_termxgrupodoc;

  -- sdel_tvalor_termxgrupodoc
  PROCEDURE sdel_tvalor_termxgrupodoc(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$deletes             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE sapr_tvalor_terminoxgrupodoc
     WHERE oid_grupo_documento = par$oid_grupo_documento;
    par$deletes := sql%rowcount;
  END sdel_tvalor_termxgrupodoc;

  /* fexiste_documentos_grp */
  FUNCTION fexiste_documentos_grp(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN IS
    var$cant INT;
  BEGIN
    SELECT COUNT(1)
      INTO var$cant
      FROM sapr_tgrupo_documento
     WHERE oid_grupo_documento = par$oid_grupo_documento;

    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);
  END fexiste_documentos_grp;

  /* fhay_cierrecaja_encurso */
  FUNCTION fhay_cierrecaja_encurso(par$oid_sector_origen    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_formulario_grupo IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN IS
    var$cant INT := 0;
  BEGIN
    IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario_grupo,
                                                          gepr_pcomon_###VERSION###.const$CodCaracCierreCaja) THEN
      SELECT COUNT(1)
        INTO var$cant
        FROM sapr_tdocumento d
       INNER JOIN sapr_tformulario f
          ON f.oid_formulario = d.oid_formulario
       INNER JOIN sapr_tcaractformxformulario cff
          ON cff.oid_formulario = f.oid_formulario
       INNER JOIN sapr_tcaract_formulario cf
          ON cf.oid_caract_formulario = cff.oid_caract_formulario
       WHERE d.cod_estado = gepr_pcomon_###VERSION###.const$CodEstadoDocEnCurso
         AND cf.cod_caract_formulario =
             gepr_pcomon_###VERSION###.const$CodCaracCierreCaja
         AND d.oid_sector_origen = par$oid_sector_origen
         AND d.oid_grupo_documento <> par$oid_grupo_documento
         AND ROWNUM = 1;
    END IF;

    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);

  END fhay_cierrecaja_encurso;

  /* fcargar_rc_grupo_documento */
  FUNCTION fcargar_rc_grupo_documento(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_Grupo_Documentos SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_Grupo_Documentos FOR
      SELECT OID_GRUPO_DOCUMENTO,
             OID_GRUPO_DOCUMENTO_PADRE,
             OID_FORMULARIO,
             OID_SECTOR_ORIGEN,
             OID_SECTOR_DESTINO,
             COD_ESTADO,
             COD_MIGRACION,
             GMT_CREACION,
             DES_USUARIO_CREACION,
             GMT_MODIFICACION,
             COD_COMPROBANTE,
             DES_USUARIO_MODIFICACION,
             ROWVER
        FROM SAPR_TGRUPO_DOCUMENTO
       WHERE OID_GRUPO_DOCUMENTO = par$oid_grupo_documento;
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_Grupo_Documentos;

  END fcargar_rc_grupo_documento;

  /* fcargar_rc_valorterminosgrupo */
  FUNCTION fcargar_rc_valorterminosgrupo(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rcValorTerminosGrupo SYS_REFCURSOR;
  BEGIN

    OPEN var$rcValorTerminosGrupo FOR
      SELECT OID_VALOR_TERMINOXGRUPODOC,
             OID_GRUPO_DOCUMENTO,
             OID_TERMINO,
             COD_MIGRACION,
             DES_VALOR,
             GMT_CREACION,
             DES_USUARIO_CREACION,
             GMT_MODIFICACION,
             DES_USUARIO_MODIFICACION
        FROM SAPR_TVALOR_TERMINOXGRUPODOC
       WHERE OID_GRUPO_DOCUMENTO = par$oid_grupo_documento;
    par$selects := sql%ROWCOUNT;

    RETURN var$rcValorTerminosGrupo;

  END fcargar_rc_valorterminosgrupo;

  /* fcargar_rc_historico */
  FUNCTION fcargar_rc_historico(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rcHistoricoMovGrupo SYS_REFCURSOR;
  BEGIN

    OPEN var$rcHistoricoMovGrupo FOR
      SELECT OID_HIST_MOV_GRUPO_DOCUMENTO,
             OID_GRUPO_DOCUMENTO,
             COD_ESTADO,
             GMT_CREACION,
             DES_USUARIO_CREACION,
             GMT_MODIFICACION,
             DES_USUARIO_MODIFICACION
        FROM SAPR_THIST_MOV_GRUPO_DOCUMENTO
       WHERE OID_GRUPO_DOCUMENTO = par$oid_grupo_documento;
    par$selects := sql%ROWCOUNT;

    RETURN var$rcHistoricoMovGrupo;

  END fcargar_rc_historico;

END sapr_pdocumentos_grp_###VERSION###;
/