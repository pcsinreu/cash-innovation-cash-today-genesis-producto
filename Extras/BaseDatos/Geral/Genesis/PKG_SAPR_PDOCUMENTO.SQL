CREATE OR REPLACE PACKAGE sapr_pdocumento_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

    const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pdocumento_###VERSION###';
    const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';

  /* Guardar documento */
  PROCEDURE sguardar_documento(par$oid_formulario            IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$adocs_oid                 IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_doc_padre       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_sustituto       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_mov_fondos      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_fyh_plncertif       IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                               par$adocs_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                               par$adocs_fyh_contable        IN gepr_pcomon_###VERSION###.tipo$fyhs_,                                                            
                               par$adocs_cod_actual_id       IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$adocs_collection_id       IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$adocs_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$adocs_oid_cuenta_ori      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_cuenta_des      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_cuentasaldo_ori IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_cuentasaldo_des IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_sector_ori      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_sector_des      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_rowver              IN gepr_pcomon_###VERSION###.tipo$nels_,
                               /* arrays de efectivo por documento */
                               par$aefdoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_divisa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_denominacion   IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_unid_medida    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_cod_niv_detalle    IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$aefdoc_cod_tp_efec_tot    IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$aefdoc_oid_calidad        IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_num_importe        IN gepr_pcomon_###VERSION###.tipo$imps_,
                               par$aefdoc_nel_cantidad       IN gepr_pcomon_###VERSION###.tipo$nels_,
                               par$ampdoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_oid_divisa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_oid_medio_pago     IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_cod_tipo_med_pago  IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$ampdoc_cod_nivel_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$ampdoc_num_importe        IN gepr_pcomon_###VERSION###.tipo$imps_,
                               par$ampdoc_nel_cantidad       IN gepr_pcomon_###VERSION###.tipo$nels_,
                               par$avtmpdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtmpdoc_oid_t_mediopago  IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtmpdoc_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$avtmpdoc_nec_indice_grp   IN gepr_pcomon_###VERSION###.tipo$ints_,
                               par$avtdoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtdoc_oid_termino        IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtdoc_des_valor          IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$updates                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$deletes                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$esnuevo                   OUT BOOLEAN);

  /* Validar documento */
  PROCEDURE svalidar_documento(par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_tipo_documento  OUT gepr_pcomon_###VERSION###.tipo$oid_,

                               /* arrays asociativos */
                               /* arrays de documentos */
                               par$adocs_oid                 IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_doc_padre       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_sustituto       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$adocs_oid_cuentasaldo_ori IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_cuentasaldo_des IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_fyh_plncertif       IN OUT gepr_pcomon_###VERSION###.tipo$fyhs_,

                               /* arrays asociativos */
                               /* arrays de efectivo por documento */
                               par$aefdoc_oid_divisa       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_denominacion IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_unid_medida  IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_cod_niv_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$aefdoc_cod_tp_efec_tot  IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$aefdoc_oid_calidad      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_num_importe      IN gepr_pcomon_###VERSION###.tipo$imps_,
                               par$aefdoc_nel_cantidad     IN gepr_pcomon_###VERSION###.tipo$nels_,

                               /* arrays de medio de pago por documento */
                               par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                               par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                               /* arrays de terminos de medio de pago por documento */
                               par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,

                               /* arrays de terminos por documento */
                               par$avtdoc_oid_termino IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtdoc_des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,

                               /* cultura/idioma */
                               par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Guardar documento - Gestion de Fondos y Cierre */
  PROCEDURE sguardar_doc_valores(par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                 par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                 par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                                 
                                 par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                 
                                 par$aefdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_oid_unid_medida   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_cod_niv_detalle   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aefdoc_cod_tp_efec_tot   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aefdoc_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                 par$aefdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                 par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,
                                 
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
                                 
                                 par$avtdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtdoc_oid_termino       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtdoc_des_valor         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$info_ejecucion           IN VARCHAR2,
                                 par$hacer_commit             IN INTEGER,
                                 par$confirmar_doc            IN INTEGER,
                                 par$doc_rc_documentos        OUT sys_refcursor,
                                 par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                 par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar valores Anteriores de los documentos de clasificacion */
  PROCEDURE sguardar_valores_anteriores(par$aefadoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
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
                                        par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validar documento de valores */
  PROCEDURE svalidar_doc_valores(par$aefdoc_oid_divisa IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampdoc_oid_divisa IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Guardar documento de Actas - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_doc_elem_actas(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,

                                    /* Informaciones formulario */
                                    par$oid_formulario    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$bol_gestion_bulto IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,

                                    /* arrays asociativos */
                                    /* arrays de documentos */
                                    par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                            
                                    par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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

                                    /* informacion de ejecucion */
                                    par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$info_ejecucion           IN VARCHAR2,
                                    par$hacer_commit             IN INTEGER,
                                    par$confirmar_doc            IN INTEGER,
                                    par$doc_rc_documentos        OUT sys_refcursor,
                                    par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                    par$ele_rc_elementos         OUT sys_refcursor,
                                    par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento de Altas - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_doc_elem_altas(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,

                                    /* Informaciones formulario */
                                    par$oid_formulario    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$bol_gestion_bulto IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,

                                    /* arrays asociativos */
                                    /* arrays de documentos */
                                    par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                             
                                    par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                    par$doc_rc_documentos        OUT sys_refcursor,
                                    par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                    par$ele_rc_elementos         OUT sys_refcursor,
                                    par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento de Reenvio/Baja - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_doc_elem_reenviobaja(par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,

                                          /* arrays asociativos */
                                          /* arrays de documentos */
                                          par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                         
                                          par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                          par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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

                                          /* Bulto - arrays */
                                          par$abuldoc_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,

                                          /* informacion de ejecucion */
                                          par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$hacer_commit             IN INTEGER,
                                          par$confirmar_doc            IN INTEGER,
                                          par$doc_rc_documentos        OUT sys_refcursor,
                                          par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                          par$ele_rc_elementos         OUT sys_refcursor,
                                          par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento de sustitucion - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_doc_elem_sustitucion(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,

                                          /* arrays asociativos */
                                          /* arrays de documentos */
                                          par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                                
                                          par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                          par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                          par$doc_rc_documentos        OUT sys_refcursor,
                                          par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                          par$ele_rc_elementos         OUT sys_refcursor,
                                          par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento de fondos - MAE */
  PROCEDURE sguardar_doc_fondos(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_accion               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_ajeno                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$adoc_fyh_contable        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$adoc_cod_actual_id       IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$adoc_cod_collection_id   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$adoc_ori_cod_client      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_canal       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_delega      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_client      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_canal       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_delega      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$aefdoc_cod_divisa        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_num_importe_total IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$aefdoc_cod_denominacion  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_cod_div_den       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$aefdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$ampdoc_cod_divisa        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$ampdoc_cod_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$avtdoc_cod_termino       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$avtdoc_des_valor         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$bol_validar_cod_externo  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$obs_,
                                par$hacer_commit             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$validaciones             OUT sys_refcursor,
                                par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validar documento de fondos - MAE */
  PROCEDURE svalidar_doc_fondos(par$cod_accion               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$adoc_ori_cod_client      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_canal       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_delega      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_client      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_canal       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_delega      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$aefdoc_cod_divisa        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_num_importe_total IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$aefdoc_cod_denominacion  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_cod_div_den       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$aefdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$ampdoc_cod_divisa        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$ampdoc_cod_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$avtdoc_cod_termino       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$avtdoc_des_valor         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$bol_validar_cod_externo  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$validaciones             OUT gepr_pcomon_###VERSION###.tipo$descs_);

  /* Cargar valores cuenta - MAE */
  PROCEDURE scargar_cuenta_fondos(par$cod_ajeno    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_tipo     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_client   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_subcli   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_ptserv   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_canal    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_subcan   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_delega   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_planta   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_sector   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_client   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_subcli   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_ptserv   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_canal    OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_subcan   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_delega   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_planta   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_sector   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_cultura  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$validaciones OUT gepr_pcomon_###VERSION###.tipo$descs_);

  /* Cargar valores terminos - MAE */
  PROCEDURE scargar_terminos_fondos(par$avtdoc_cod_termino   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$avtdoc_des_valor_ori IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$avtdoc_oid_documento OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtdoc_oid_termino   OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtdoc_des_valor     OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$validaciones         OUT gepr_pcomon_###VERSION###.tipo$descs_);

  /* Guardar documento - Gestion de Contenedores */
  PROCEDURE sguardar_doc_contenedor_alta(par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_comprobante     OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                         --documentos
                                         par$adoc_oid_documento       in gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoc_oid_documento_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoc_oid_documento_sust  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoc_oid_moviment_fondo  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoc_fyh_plan_certif     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adoc_fyh_contable        IN gepr_pcomon_###VERSION###.tipo$fyhs_,                            
                                         par$adocs_cod_actual_id      IN gepr_pcomon_###VERSION###.tipo$descs_,
                                         par$adocs_collection_id      IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                         par$cod_ejecucion  OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Guardar documento - Gestion de Contenedores */
  PROCEDURE sguardar_doc_contenedor(par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    --documentos
                                    par$adoc_oid_documento       in gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adoc_oid_documento_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adoc_oid_documento_sust  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adoc_oid_moviment_fondo  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adoc_fyh_plan_certif     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adoc_fyh_contable        IN gepr_pcomon_###VERSION###.tipo$fyhs_,                           
                                    par$adoc_cod_actual_id       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$adoc_collection_id       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$adoc_cod_comprobante     OUT gepr_pcomon_###VERSION###.tipo$codS_,
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

  /* srecuperar_documentos */
  PROCEDURE srecuperar_documentos(par$oids_documentos            IN gepr_pcomon_###VERSION###.tipo$oids_,
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
                                  par$ejecucion_interna          IN INTEGER,
                                  par$usuario                    IN VARCHAR2,
                                  par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$info_ejecucion             IN VARCHAR2,
                                  par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* srecuperar_ultimodocumentos */
  PROCEDURE srecuperar_ultimodocumentos(par$oids_documentos            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$bol_gestion_bultos         IN gepr_pcomon_###VERSION###.tipo$nbol_,
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
                                        par$ejecucion_interna          IN INTEGER,
                                        par$usuario                    IN VARCHAR2,
                                        par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion             IN VARCHAR2,
                                        par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sverificar_documento_existe */
  PROCEDURE sverificar_documento_existe(par$acod_externo               IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_client        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_subcli        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_ptserv        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_canal         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_subcan        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_delega        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_planta        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_sector        IN gepr_pcomon_###VERSION###.tipo$cods_,
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

  /* sdel_tdocumento_elemento */
  PROCEDURE sdel_tdocumento_elemento(par$oid_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$eliminar_hijos IN BOOLEAN,
                                     par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tefectivoxdocumento */
  PROCEDURE sdel_tefectivoxdocumento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_thist_mov_documento */
  PROCEDURE sdel_thist_mov_documento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tmedio_pagoxdocumento */
  PROCEDURE sdel_tmedio_pagoxdocumento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_term_med_pago */
  PROCEDURE sdel_tvalor_term_med_pago(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_terminoxdoc */
  PROCEDURE sdel_tvalor_terminoxdoc(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tdocumento_elemento */
  PROCEDURE sins_tdocumento_elemento(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_remesa           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_contenedor       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_contenedor_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_cont_mayor_nivel IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tefectivoxdocumento */
  PROCEDURE sins_tefectivoxdocumento(par$oid_documento     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                     par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                     par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_thist_mov_documento */
  PROCEDURE sins_thist_mov_documento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_estado    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tmedio_pagoxdocumento */
  PROCEDURE sins_tmedio_pagoxdocumento(par$oid_documento     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                       par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                       par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                       par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tvalor_term_med_pago */
  PROCEDURE sins_tvalor_term_med_pago(par$oid_medio_pagoxdoc     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_termino_medio_pago IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$des_valor              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$nec_indice_grupo       IN INTEGER,
                                      par$usuario                IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts                OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tantefectivoxdocumento */
  PROCEDURE sins_tantefectivoxdocumento(par$oid_documento     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                        par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                        par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tantmedio_pagoxdocumento */
  PROCEDURE sins_tantmedio_pagoxdocumento(par$oid_documento     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                          par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                          par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tantvalor_term_med_pago */
  PROCEDURE sins_tantvalor_term_med_pago(par$oid_medio_pagoxdoc     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_termino_medio_pago IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$des_valor              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                         par$nec_indice_grupo       IN INTEGER,
                                         par$usuario                IN gepr_pcomon_###VERSION###.tipo$usr_,
                                         par$inserts                OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tvalor_terminoxdoc */
  PROCEDURE sins_tvalor_terminoxdoc(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_termino   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$des_valor     IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* supd_desassociar_grp */
  PROCEDURE supd_desassociar_grp(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$updates       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* supd_estado_documento */
  PROCEDURE supd_estado_documento(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* supd_est_comp_documento */
  PROCEDURE supd_est_comp_documento(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_comprobante      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* supd_tdocumento_elemento */
  PROCEDURE supd_tdocumento_elemento(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_remesa           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_est_docxelemento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cambiar_hijos        IN BOOLEAN,
                                     par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* supd_tdocumento */
  PROCEDURE supd_tdocumento(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_formulario           IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_cuenta_origen        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_cuenta_destino       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_cuenta_saldo_origen  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_cuenta_saldo_destino IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_documento_padre      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_documento_sustituto  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_tipo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_sector_origen        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_sector_destino       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_grupo_documento      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_movimentacion_fondo  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$fyh_plan_certificacion   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            par$fyh_gestion              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            par$fyh_contable             IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            par$cod_externo              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_estado               IN gepr_pcomon_###VERSION###.tipo$est_,
                            par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$rowver                   IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$esnuevo                  OUT BOOLEAN,
                            par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_actual_id            IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_collection_id        IN gepr_pcomon_###VERSION###.tipo$desc_);

  /* scargar_tt_oids_documento_gpr */
  PROCEDURE scargar_tt_oids_documento_gpr(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$recuperar_padres    IN BOOLEAN,
                                          par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* fvalidar_terminos */
  FUNCTION fvalidar_terminos(par$cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,
                             par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  /* fexiste_documento */
  FUNCTION fexiste_documento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN;

  /* fcargar_rc_documento */
  FUNCTION fcargar_rc_documento RETURN sys_refcursor;

  /* fcargar_rc_doc_padre */
  FUNCTION fcargar_rc_doc_padre RETURN SYS_REFCURSOR;

  /* fcargar_rc_valorterminosdoc */
  FUNCTION fcargar_rc_valorterminosdoc RETURN SYS_REFCURSOR;

  /* fcargar_rc_historico */
  FUNCTION fcargar_rc_historico RETURN SYS_REFCURSOR;

  /* fcargar_rc_valores */
  FUNCTION fcargar_rc_valores RETURN SYS_REFCURSOR;

  /* srecupera_dt_deposito_ripley */
  PROCEDURE srecupera_dt_deposito_ripley(par$cod_cultura            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$numero_deposito        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$rc_DatosDepositoRipley OUT SYS_REFCURSOR);

  /* sactualiza_desc_mensaje */
  PROCEDURE sactualiza_desc_mensaje(par$cod_cultura   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$des_mensaje   IN gepr_pcomon_###VERSION###.tipo$obs_);

  /* sactualiza_desc_mensaje */
  PROCEDURE svalidar_cuentas(par$oid_formulario          IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_canal       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_canal       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_subcan      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_delega      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_delega      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_planta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_planta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_cod_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_canal       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_canal       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_subcan      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_delega      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_delega      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_planta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_planta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_cod_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_cuenta_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_sector_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_cuenta_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_cuenta_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_sector_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_cuenta_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                             par$inserts                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$updates                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$selects                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$esreenvio               OUT BOOLEAN);

  /* srecuperar_docs_certificados */
  PROCEDURE srecuperar_docs_certificados(par$cod_certificado           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_ajeno                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$bol_detallar_desgloses    IN INTEGER,
                                         par$bol_campos_adicionales    IN INTEGER,
                                         par$doc_rc_documentos         OUT sys_refcursor,
                                         par$doc_rc_cuentas            OUT sys_refcursor,
                                         par$doc_rc_valores_documentos OUT sys_refcursor,
                                         par$doc_rc_valor_terminos_doc OUT sys_refcursor,
                                         par$usuario                   IN VARCHAR2,
                                         par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion            IN VARCHAR2,
                                         par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE svalidar_doc_contenedor(par$oid_formulario        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_formulario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$aform_caracteristicas IN gepr_pcomon_###VERSION###.tipo$formularios,
                                    par$oid_grupo_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
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
                                    par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_);

  PROCEDURE svalidar_doc_contenedor_alta(par$oid_formulario        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_formulario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$aform_caracteristicas IN gepr_pcomon_###VERSION###.tipo$formularios,
                                         par$oid_grupo_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
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

                                         -- contenedores
                                         par$adoccont_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoccont_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoccont_cod_tipo_cont     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adoccont_obj_tipos_cont    in gepr_pcomon_###VERSION###.tipo$tiposContenedores,
                                         par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                         -- Precintos
                                         par$aprco_oid_contenedor       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,

                                         -- Elementos
                                         par$aelco_oid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,

                                         /* informacion de ejecucion */
                                         par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                         par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_);

  procedure svalidar_cuentas_pack_modular(par$adoc_oid_cuenta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_cliente_origen    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_subcliente_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_puntoservico_ori  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_canal_origen      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_subcanal_origen   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_delegacion_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_planta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_sector_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$cod_cultura                in gepr_pcomon_###VERSION###.tipo$cod_);

  FUNCTION fvalidar_cod_externo_duplicado(par$cod_externo    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  PROCEDURE supd_fyh_plan_certificacion(par$oid_documento          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$usuario                IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$updates                OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE srecuperar_documentos_mae(par$cod_delegacion     in gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$cod_identificacion in gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$fec_transacciones in gepr_pcomon_###VERSION###.tipo$fyh_,
                                      par$rc_documentos      out sys_refcursor);

 PROCEDURE supd_extradata(par$oid_documento          IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_termino           IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$des_valor             IN gepr_pcomon_###VERSION###.tipo$obs_,
                            par$cod_usuario           IN gepr_pcomon_###VERSION###.tipo$usr_) ; 
 PROCEDURE supd_altacashin(
    par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$acod_actual_id            IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$acod_collection_id        IN gepr_pcomon_###VERSION###.tipo$descs_
);
  
 PROCEDURE sgrabar_integracion_documento(
                            par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_documento           IN gepr_pcomon_###VERSION###.tipo$oid_,                         
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_);

 PROCEDURE sget_parametros_integracion(
                            par$enviar_datos_tipo_plani   OUT gepr_pcomon_###VERSION###.tipo$oid_,                         
                            par$enviar_datos_switch           OUT gepr_pcomon_###VERSION###.tipo$cod_);

 FUNCTION fvalidar_integracion_documento(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_documento           IN gepr_pcomon_###VERSION###.tipo$oid_) 
  RETURN gepr_pcomon_###VERSION###.tipo$bol_;

END sapr_pdocumento_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pdocumento_###VERSION### AS

  /* Guardar documento */
  PROCEDURE sguardar_documento(par$oid_formulario            IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$adocs_oid                 IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_doc_padre       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_sustituto       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_mov_fondos      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_fyh_plncertif       IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                               par$adocs_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                               par$adocs_fyh_contable        IN gepr_pcomon_###VERSION###.tipo$fyhs_,                                                            
                               par$adocs_cod_actual_id       IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$adocs_collection_id       IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$adocs_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$adocs_oid_cuenta_ori      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_cuenta_des      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_cuentasaldo_ori IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_cuentasaldo_des IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_sector_ori      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_sector_des      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_rowver              IN gepr_pcomon_###VERSION###.tipo$nels_,
                               par$aefdoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_divisa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_denominacion   IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_unid_medida    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_cod_niv_detalle    IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$aefdoc_cod_tp_efec_tot    IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$aefdoc_oid_calidad        IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_num_importe        IN gepr_pcomon_###VERSION###.tipo$imps_,
                               par$aefdoc_nel_cantidad       IN gepr_pcomon_###VERSION###.tipo$nels_,
                               par$ampdoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_oid_divisa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_oid_medio_pago     IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_cod_tipo_med_pago  IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$ampdoc_cod_nivel_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$ampdoc_num_importe        IN gepr_pcomon_###VERSION###.tipo$imps_,
                               par$ampdoc_nel_cantidad       IN gepr_pcomon_###VERSION###.tipo$nels_,
                               par$avtmpdoc_oid_documento    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtmpdoc_oid_t_mediopago  IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtmpdoc_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$avtmpdoc_nec_indice_grp   IN gepr_pcomon_###VERSION###.tipo$ints_,
                               par$avtdoc_oid_documento      IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtdoc_oid_termino        IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtdoc_des_valor          IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$updates                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$deletes                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$esnuevo                   OUT BOOLEAN) IS
  
    /*const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_documento';*/
  
    /* variables locales */
    /*var$parcial                  TIMESTAMP;
    var$info_ejecucion           gepr_pcomon_###VERSION###.tipo$obs_;
    var$cod_ejecucion            gepr_pcomon_###VERSION###.tipo$nel_;*/
    var$inserts_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int          gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$oid_tipo_documento   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$est_ := gepr_pcomon_###VERSION###.const$CodEstadoDocEnCurso;
    --var$fyh_plncertif        gepr_pcomon_###VERSION###.tipo$fyh_;
    var$adocs_fyh_plncertif gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$existe              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  
  BEGIN
  
    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;
  
    var$adocs_fyh_plncertif := par$adocs_fyh_plncertif;
  
    /* Validar documentos */
    /*var$parcial   := SYSTIMESTAMP;*/
    svalidar_documento(par$oid_formulario            => par$oid_formulario,
                       par$oid_grupo_documento       => par$oid_grupo_documento,
                       par$oid_tipo_documento        => var$oid_tipo_documento,
                       par$adocs_oid                 => par$adocs_oid,
                       par$adocs_oid_doc_padre       => par$adocs_oid_doc_padre,
                       par$adocs_oid_sustituto       => par$adocs_oid_sustituto,
                       par$adocs_cod_externo         => par$adocs_cod_externo,
                       par$adocs_oid_cuentasaldo_ori => par$adocs_oid_cuentasaldo_ori,
                       par$adocs_oid_cuentasaldo_des => par$adocs_oid_cuentasaldo_des,
                       par$adocs_fyh_plncertif       => var$adocs_fyh_plncertif,
                       par$aefdoc_oid_divisa         => par$aefdoc_oid_divisa,
                       par$aefdoc_oid_denominacion   => par$aefdoc_oid_denominacion,
                       par$aefdoc_oid_unid_medida    => par$aefdoc_oid_unid_medida,
                       par$aefdoc_cod_niv_detalle    => par$aefdoc_cod_niv_detalle,
                       par$aefdoc_cod_tp_efec_tot    => par$aefdoc_cod_tp_efec_tot,
                       par$aefdoc_oid_calidad        => par$aefdoc_oid_calidad,
                       par$aefdoc_num_importe        => par$aefdoc_num_importe,
                       par$aefdoc_nel_cantidad       => par$aefdoc_nel_cantidad,
                       par$ampdoc_oid_divisa         => par$ampdoc_oid_medio_pago,
                       par$ampdoc_oid_medio_pago     => par$ampdoc_oid_medio_pago,
                       par$ampdoc_cod_tipo_med_pago  => par$ampdoc_cod_tipo_med_pago,
                       par$ampdoc_cod_nivel_detalle  => par$ampdoc_cod_nivel_detalle,
                       par$ampdoc_num_importe        => par$ampdoc_num_importe,
                       par$ampdoc_nel_cantidad       => par$ampdoc_nel_cantidad,
                       par$avtmpdoc_oid_t_mediopago  => par$avtmpdoc_oid_t_mediopago,
                       par$avtmpdoc_des_valor        => par$avtmpdoc_des_valor,
                       par$avtmpdoc_nec_indice_grp   => par$avtmpdoc_nec_indice_grp,
                       par$avtdoc_oid_termino        => par$avtdoc_oid_termino,
                       par$avtdoc_des_valor          => par$avtdoc_des_valor,
                       par$cod_cultura               => par$cod_cultura);
    /*gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func || '.svalidar_documento', 0, 0, 0, 0, 0, 0, var$parcial, null, 'C', var$info_ejecucion, '', par$usuario, var$cod_ejecucion);*/
  
    /* Grabar documento */
    FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
    
      /*var$parcial   := SYSTIMESTAMP;*/
      supd_tdocumento(par$oid_documento            => par$adocs_oid(idx),
                      par$oid_formulario           => par$oid_formulario,
                      par$oid_cuenta_origen        => par$adocs_oid_cuenta_ori(idx),
                      par$oid_cuenta_destino       => par$adocs_oid_cuenta_des(idx),
                      par$oid_cuenta_saldo_origen  => par$adocs_oid_cuentasaldo_ori(idx),
                      par$oid_cuenta_saldo_destino => par$adocs_oid_cuentasaldo_des(idx),
                      par$oid_documento_padre      => par$adocs_oid_doc_padre(idx),
                      par$oid_documento_sustituto  => par$adocs_oid_sustituto(idx),
                      par$oid_tipo_documento       => var$oid_tipo_documento,
                      par$oid_sector_origen        => par$adocs_oid_sector_ori(idx),
                      par$oid_sector_destino       => par$adocs_oid_sector_des(idx),
                      par$oid_grupo_documento      => par$oid_grupo_documento,
                      par$oid_movimentacion_fondo  => par$adocs_oid_mov_fondos(idx),
                      par$fyh_plan_certificacion   => var$adocs_fyh_plncertif(idx),
                      par$fyh_gestion              => par$adocs_fyh_gestion(idx),
                      par$fyh_contable             => par$adocs_fyh_contable(idx),
                      par$cod_externo              => par$adocs_cod_externo(idx),
                      par$cod_estado               => var$cod_estado_documento,
                      par$usuario                  => par$usuario,
                      par$cod_cultura              => par$cod_cultura,
                      par$rowver                   => par$adocs_rowver(idx),
                      par$esnuevo                  => par$esnuevo,
                      par$inserts                  => var$inserts_int,
                      par$updates                  => var$updates_int,                      
                      par$cod_actual_id            => par$adocs_cod_actual_id(idx),
                      par$cod_collection_id        => par$adocs_collection_id(idx));
    
      /*gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func || '.supd_tdocumento', 0, 0, 0, 0, 0, 0, var$parcial, null, 'C', var$info_ejecucion, '', par$usuario, var$cod_ejecucion);*/
      par$inserts := par$inserts + var$inserts_int;
      par$updates := par$updates + var$updates_int;
    
      IF (par$adocs_cod_actual_id(idx) IS NOT NULL ) THEN
 
          sgrabar_integracion_documento(
                            par$oid_llamada             => null,
                            par$oid_documento   => par$adocs_oid(idx),                         
                            par$cod_usuario             => par$usuario);

      END IF;
      
      IF par$esnuevo THEN
        /* Grabar historico del documento */
        /*var$parcial   := SYSTIMESTAMP;*/
        sins_thist_mov_documento(par$oid_documento => par$adocs_oid(idx),
                                 par$cod_estado    => var$cod_estado_documento,
                                 par$usuario       => par$usuario,
                                 par$inserts       => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
        /*gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func || '.sins_thist_mov_documento', 0, 0, 0, 0, 0, 0, var$parcial, null, 'C', var$info_ejecucion, '', par$usuario, var$cod_ejecucion);*/
      
      ELSE
      
        /* es una modificacion, debe ser deletado todos los registros y creado nuevamente */
        /*var$parcial   := SYSTIMESTAMP;*/
        sdel_tefectivoxdocumento(par$oid_documento => par$adocs_oid(idx),
                                 par$deletes       => var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
      
        sdel_tmedio_pagoxdocumento(par$oid_documento => par$adocs_oid(idx),
                                   par$deletes       => var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
      
        sdel_tvalor_term_med_pago(par$oid_documento => par$adocs_oid(idx),
                                  par$deletes       => var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
      
        sdel_tvalor_terminoxdoc(par$oid_documento => par$adocs_oid(idx),
                                par$deletes       => var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
      
        sdel_thist_mov_documento(par$oid_documento => par$adocs_oid(idx),
                                 par$deletes       => var$deletes_int);
        par$deletes := par$deletes + var$deletes_int;
      
        /*gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func || '.DeletarRegistros', 0, 0, 0, 0, 0, 0, var$parcial, null, 'C', var$info_ejecucion, '', par$usuario, var$cod_ejecucion);*/
      END IF;
    END LOOP;
  
    /* Grabar los efectivos del documento */
    /*var$parcial   := SYSTIMESTAMP;*/
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefdoc_oid_divisa) THEN
      FOR idx IN par$aefdoc_oid_divisa.first .. par$aefdoc_oid_divisa.last LOOP
        sins_tefectivoxdocumento(par$oid_documento     => par$aefdoc_oid_documento(idx),
                                 par$oid_divisa        => par$aefdoc_oid_divisa(idx),
                                 par$oid_denominacion  => par$aefdoc_oid_denominacion(idx),
                                 par$oid_unidad_medida => par$aefdoc_oid_unid_medida(idx),
                                 par$cod_nivel_detalle => par$aefdoc_cod_niv_detalle(idx),
                                 par$cod_tipo_efec_tot => par$aefdoc_cod_tp_efec_tot(idx),
                                 par$oid_calidad       => par$aefdoc_oid_calidad(idx),
                                 par$num_importe       => par$aefdoc_num_importe(idx),
                                 par$nel_cantidad      => par$aefdoc_nel_cantidad(idx),
                                 par$usuario           => par$usuario,
                                 par$inserts           => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      END LOOP;
    END IF;
    /*gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func || '.sins_tefectivoxdocumento', 0, 0, 0, 0, 0, 0, var$parcial, null, 'C', var$info_ejecucion, '', par$usuario, var$cod_ejecucion);*/
  
    /* Grabar los medios de pago del documento */
    /*var$parcial   := SYSTIMESTAMP;*/
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampdoc_oid_divisa) THEN
      FOR idx IN par$ampdoc_oid_divisa.first .. par$ampdoc_oid_divisa.last LOOP
        sins_tmedio_pagoxdocumento(par$oid_documento     => par$ampdoc_oid_documento(idx),
                                   par$oid_divisa        => par$ampdoc_oid_divisa(idx),
                                   par$oid_medio_pago    => par$ampdoc_oid_medio_pago(idx),
                                   par$cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago(idx),
                                   par$cod_nivel_detalle => par$ampdoc_cod_nivel_detalle(idx),
                                   par$num_importe       => par$ampdoc_num_importe(idx),
                                   par$nel_cantidad      => par$ampdoc_nel_cantidad(idx),
                                   par$usuario           => par$usuario,
                                   par$inserts           => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      END LOOP;
    END IF;
    /*gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func || '.sins_tmedio_pagoxdocumento', 0, 0, 0, 0, 0, 0, var$parcial, null, 'C', var$info_ejecucion, '', par$usuario, var$cod_ejecucion);*/
  
    /* Grabar los valores de los terminos de medios de pago del documento */
    /*var$parcial   := SYSTIMESTAMP;*/
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$avtmpdoc_oid_t_mediopago) THEN
      FOR idx IN par$avtmpdoc_oid_t_mediopago.first .. par$avtmpdoc_oid_t_mediopago.last LOOP
        sins_tvalor_term_med_pago(par$oid_medio_pagoxdoc     => par$avtmpdoc_oid_documento(idx),
                                  par$oid_termino_medio_pago => par$avtmpdoc_oid_t_mediopago(idx),
                                  par$des_valor              => par$avtmpdoc_des_valor(idx),
                                  par$nec_indice_grupo       => par$avtmpdoc_nec_indice_grp(idx),
                                  par$usuario                => par$usuario,
                                  par$inserts                => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      END LOOP;
    END IF;
    /*gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func || '.sins_tvalor_term_med_pago', 0, 0, 0, 0, 0, 0, var$parcial, null, 'C', var$info_ejecucion, '', par$usuario, var$cod_ejecucion);*/
  
    /* Grabar los valores de los terminos del documento */
    /*var$parcial   := SYSTIMESTAMP;*/
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$avtdoc_oid_termino) THEN
      FOR idx IN par$avtdoc_oid_termino.first .. par$avtdoc_oid_termino.last LOOP
        sins_tvalor_terminoxdoc(par$oid_documento => par$avtdoc_oid_documento(idx),
                                par$oid_termino   => par$avtdoc_oid_termino(idx),
                                par$des_valor     => par$avtdoc_des_valor(idx),
                                par$usuario       => par$usuario,
                                par$inserts       => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      END LOOP;
    END IF;
    /*gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func || '.sins_tvalor_terminoxdoc', 0, 0, 0, 0, 0, 0, var$parcial, null, 'C', var$info_ejecucion, '', par$usuario, var$cod_ejecucion);*/
  
  END sguardar_documento;

  /* Validar documento */
  PROCEDURE svalidar_documento(par$oid_formulario            IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_grupo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_tipo_documento        OUT gepr_pcomon_###VERSION###.tipo$oid_,
                               par$adocs_oid                 IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_doc_padre       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_sustituto       IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$adocs_oid_cuentasaldo_ori IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_oid_cuentasaldo_des IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$adocs_fyh_plncertif       IN OUT gepr_pcomon_###VERSION###.tipo$fyhs_,
                               par$aefdoc_oid_divisa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_denominacion   IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_oid_unid_medida    IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_cod_niv_detalle    IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$aefdoc_cod_tp_efec_tot    IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$aefdoc_oid_calidad        IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$aefdoc_num_importe        IN gepr_pcomon_###VERSION###.tipo$imps_,
                               par$aefdoc_nel_cantidad       IN gepr_pcomon_###VERSION###.tipo$nels_,
                               par$ampdoc_oid_divisa         IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_oid_medio_pago     IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$ampdoc_cod_tipo_med_pago  IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$ampdoc_cod_nivel_detalle  IN gepr_pcomon_###VERSION###.tipo$cods_,
                               par$ampdoc_num_importe        IN gepr_pcomon_###VERSION###.tipo$imps_,
                               par$ampdoc_nel_cantidad       IN gepr_pcomon_###VERSION###.tipo$nels_,
                               par$avtmpdoc_oid_t_mediopago  IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtmpdoc_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$avtmpdoc_nec_indice_grp   IN gepr_pcomon_###VERSION###.tipo$ints_,
                               par$avtdoc_oid_termino        IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$avtdoc_des_valor          IN gepr_pcomon_###VERSION###.tipo$descs_,
                               par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_documento';
  
    var$existe               gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_cert_conflitante gepr_pcomon_###VERSION###.tipo$cod_;
    var$fyh_cert_actual      gepr_pcomon_###VERSION###.tipo$fyh_;
  
  BEGIN
  
    /* Formulario */
    IF par$oid_formulario IS NULL THEN
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'oid_formulario_invalido',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    ELSE
      /* Recuperar y validar tipo documento */
      par$oid_tipo_documento := sapr_pformulario_###VERSION###.frecuperar_tipodocumento(par$oid_formulario);
    
      IF par$oid_tipo_documento IS NULL THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'oid_tipo_documento_invalido',
                                                                    const$codFuncionalidad || '.' ||
                                                                    const$nombre_func,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      END IF;
    END IF;
  
    /* Grupo de Documentos */
    IF par$oid_grupo_documento IS NOT NULL THEN
    
      var$existe := 0;
      BEGIN
        SELECT COUNT(1)
          INTO var$existe
          FROM SAPR_TGRUPO_DOCUMENTO GD
         WHERE GD.OID_GRUPO_DOCUMENTO = par$oid_grupo_documento;
      EXCEPTION
        WHEN no_data_found THEN
          var$existe := 0;
      END;
    
      IF var$existe = 0 THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'oid_grupo_documento_invalido',
                                                                    const$codFuncionalidad || '.' ||
                                                                    const$nombre_func,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
      END IF;
    END IF;
  
    IF par$adocs_oid IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'lista_documentos_invalido',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
    ELSE
    
      /* consistencia de tamano de los arrays de documentos */
      IF par$adocs_oid.count <> par$adocs_oid_doc_padre.count OR
         par$adocs_oid.count <> par$adocs_oid_sustituto.count OR
         par$adocs_oid.count <> par$adocs_cod_externo.count THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'cantidad_datos_no_documentos',
                                                                    const$codFuncionalidad || '.' ||
                                                                    const$nombre_func,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      END IF;
    
      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
      
        /* Documento padre */
        IF par$adocs_oid_doc_padre(idx) IS NOT NULL THEN
        
          var$existe := 0;
          BEGIN
            SELECT COUNT(1)
              INTO var$existe
              FROM SAPR_TDOCUMENTO D
             WHERE D.OID_DOCUMENTO = par$adocs_oid_doc_padre(idx);
          EXCEPTION
            WHEN no_data_found THEN
              var$existe := 0;
          END;
        
          IF var$existe = 0 THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'oid_doc_padre_invalido',
                                                                        const$codFuncionalidad || '.' ||
                                                                        const$nombre_func,
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                        NULL,
                                                                        1));
          END IF;
        END IF;
      
        /* Sustituto */
        IF par$adocs_oid_sustituto(idx) IS NOT NULL THEN
        
          var$existe := 0;
          BEGIN
            SELECT COUNT(1)
              INTO var$existe
              FROM SAPR_TDOCUMENTO D
             WHERE D.OID_DOCUMENTO = par$adocs_oid_sustituto(idx);
          EXCEPTION
            WHEN no_data_found THEN
              var$existe := 0;
          END;
        
          IF var$existe = 0 THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'oid_sustituto_invalido',
                                                                        const$codFuncionalidad || '.' ||
                                                                        const$nombre_func,
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                        NULL,
                                                                        1));
          END IF;
        END IF;
      
        -- Validar Fecha Plan Certificacion
        IF par$adocs_oid_cuentasaldo_ori(idx) IS NOT NULL AND
           par$adocs_oid_cuentasaldo_des(idx) IS NOT NULL AND
           par$adocs_fyh_plncertif(idx) IS NOT NULL THEN
        
          var$cod_cert_conflitante := NULL;
          var$fyh_cert_actual      := NULL;
        
          sapr_pcertificacion_###VERSION###.SVALIDAR_FECHA_CERT(par$fyh_plncertif      => par$adocs_fyh_plncertif(idx),
                                                           par$oid_cuenta_origen  => par$adocs_oid_cuentasaldo_ori(idx),
                                                           par$oid_cuenta_destino => par$adocs_oid_cuentasaldo_des(idx));
        
        END IF;
      END LOOP;
    
    END IF;
  
    /* consistencia de tamano de los arrays de efectivo por documento */
    IF par$aefdoc_oid_divisa.count <> par$aefdoc_oid_denominacion.count OR
       par$aefdoc_oid_divisa.count <> par$aefdoc_oid_unid_medida.count OR
       par$aefdoc_oid_divisa.count <> par$aefdoc_cod_niv_detalle.count OR
       par$aefdoc_oid_divisa.count <> par$aefdoc_cod_tp_efec_tot.count OR
       par$aefdoc_oid_divisa.count <> par$aefdoc_oid_calidad.count OR
       par$aefdoc_oid_divisa.count <> par$aefdoc_num_importe.count OR
       par$aefdoc_oid_divisa.count <> par$aefdoc_nel_cantidad.count THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'la cantidad de datos no coincide con la de efectivos por documento a procesar');
      RETURN;
    END IF;
  
    /* consistencia de tamano de los arrays de medio de pago por documento */
    IF par$ampdoc_oid_divisa.count <> par$ampdoc_oid_medio_pago.count OR
       par$ampdoc_oid_divisa.count <> par$ampdoc_cod_tipo_med_pago.count OR
       par$ampdoc_oid_divisa.count <> par$ampdoc_cod_nivel_detalle.count OR
       par$ampdoc_oid_divisa.count <> par$ampdoc_num_importe.count OR
       par$ampdoc_oid_divisa.count <> par$ampdoc_nel_cantidad.count then
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'la cantidad de datos no coincide con la de medios de pago por documento a procesar');
      RETURN;
    END IF;
  
    /* consistencia de tamano de los arrays de terminos de medio de pago por documento */
    IF par$avtmpdoc_oid_t_mediopago.count <> par$avtmpdoc_des_valor.count OR
       par$avtmpdoc_oid_t_mediopago.count <>
       par$avtmpdoc_nec_indice_grp.count THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'la cantidad de datos no coincide con la de terminos de medio de pago por documento a procesar');
      RETURN;
    END IF;
  
    /* consistencia de tamano de los arrays de terminos por documento */
    IF par$avtdoc_oid_termino.count <> par$avtdoc_des_valor.count THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'la cantidad de datos no coincide con la de terminos por documento a procesar');
      RETURN;
    END IF;
  
  END svalidar_documento;

  /* Guardar documento - Gestion de Fondos y Cierre */
  PROCEDURE sguardar_doc_valores(par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                 par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                 par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                                 
                                 par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                 
                                 par$aefdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_oid_unid_medida   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_cod_niv_detalle   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aefdoc_cod_tp_efec_tot   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aefdoc_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aefdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                 par$aefdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 par$ampdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampdoc_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampdoc_oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampdoc_cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$ampdoc_cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                 par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 par$avtmpdoc_oid_documento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtmpdoc_oid_t_mediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtmpdoc_des_valor       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$avtmpdoc_nec_indice_grp  IN gepr_pcomon_###VERSION###.tipo$ints_,
                                 
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
                                 
                                 par$avtdoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtdoc_oid_termino       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$avtdoc_des_valor         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$info_ejecucion           IN VARCHAR2,
                                 par$hacer_commit             IN INTEGER,
                                 par$confirmar_doc            IN INTEGER,
                                 par$doc_rc_documentos        OUT sys_refcursor,
                                 par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                 par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_doc_valores';
  
    /* variables locales */
    var$esnuevo                  BOOLEAN;
    var$inicio                   gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros                gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                      VARCHAR2(4000);
    var$adocs_mov_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$cod_estado_documento     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante          gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver                   gepr_pcomon_###VERSION###.tipo$nel_;
    var$esreenvio                BOOLEAN;
  
  BEGIN
  
    var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;
  
    svalidar_doc_valores(par$aefdoc_oid_divisa => par$aefdoc_oid_divisa,
                         par$ampdoc_oid_divisa => par$ampdoc_oid_divisa,
                         par$cod_cultura       => par$cod_cultura);
  
    /* Recuperar cuentas de los documentos */
    FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
    
      svalidar_cuentas(par$oid_formulario          => par$oid_formulario,
                       par$mov_ori_oid_cuenta      => par$adocs_mov_ori_oid_cuenta(idx),
                       par$mov_ori_oid_client      => par$adocs_mov_ori_oid_client(idx),
                       par$mov_ori_cod_client      => par$adocs_mov_ori_cod_client(idx),
                       par$mov_ori_oid_subcli      => par$adocs_mov_ori_oid_subcli(idx),
                       par$mov_ori_cod_subcli      => par$adocs_mov_ori_cod_subcli(idx),
                       par$mov_ori_oid_ptserv      => par$adocs_mov_ori_oid_ptserv(idx),
                       par$mov_ori_cod_ptserv      => par$adocs_mov_ori_cod_ptserv(idx),
                       par$mov_ori_oid_canal       => par$adocs_mov_ori_oid_canal(idx),
                       par$mov_ori_cod_canal       => par$adocs_mov_ori_cod_canal(idx),
                       par$mov_ori_oid_subcan      => par$adocs_mov_ori_oid_subcan(idx),
                       par$mov_ori_cod_subcan      => par$adocs_mov_ori_cod_subcan(idx),
                       par$mov_ori_oid_delega      => par$adocs_mov_ori_oid_delega(idx),
                       par$mov_ori_cod_delega      => par$adocs_mov_ori_cod_delega(idx),
                       par$mov_ori_oid_planta      => par$adocs_mov_ori_oid_planta(idx),
                       par$mov_ori_cod_planta      => par$adocs_mov_ori_cod_planta(idx),
                       par$mov_ori_oid_sector      => par$adocs_mov_ori_oid_sector(idx),
                       par$mov_ori_cod_sector      => par$adocs_mov_ori_cod_sector(idx),
                       par$sal_ori_oid_cuenta      => par$adocs_sal_ori_oid_cuenta(idx),
                       par$sal_ori_oid_client      => par$adocs_sal_ori_oid_client(idx),
                       par$sal_ori_cod_client      => par$adocs_sal_ori_cod_client(idx),
                       par$sal_ori_oid_subcli      => par$adocs_sal_ori_oid_subcli(idx),
                       par$sal_ori_cod_subcli      => par$adocs_sal_ori_cod_subcli(idx),
                       par$sal_ori_oid_ptserv      => par$adocs_sal_ori_oid_ptserv(idx),
                       par$sal_ori_cod_ptserv      => par$adocs_sal_ori_cod_ptserv(idx),
                       par$mov_des_oid_cuenta      => par$adocs_mov_des_oid_cuenta(idx),
                       par$mov_des_oid_client      => par$adocs_mov_des_oid_client(idx),
                       par$mov_des_cod_client      => par$adocs_mov_des_cod_client(idx),
                       par$mov_des_oid_subcli      => par$adocs_mov_des_oid_subcli(idx),
                       par$mov_des_cod_subcli      => par$adocs_mov_des_cod_subcli(idx),
                       par$mov_des_oid_ptserv      => par$adocs_mov_des_oid_ptserv(idx),
                       par$mov_des_cod_ptserv      => par$adocs_mov_des_cod_ptserv(idx),
                       par$mov_des_oid_canal       => par$adocs_mov_des_oid_canal(idx),
                       par$mov_des_cod_canal       => par$adocs_mov_des_cod_canal(idx),
                       par$mov_des_oid_subcan      => par$adocs_mov_des_oid_subcan(idx),
                       par$mov_des_cod_subcan      => par$adocs_mov_des_cod_subcan(idx),
                       par$mov_des_oid_delega      => par$adocs_mov_des_oid_delega(idx),
                       par$mov_des_cod_delega      => par$adocs_mov_des_cod_delega(idx),
                       par$mov_des_oid_planta      => par$adocs_mov_des_oid_planta(idx),
                       par$mov_des_cod_planta      => par$adocs_mov_des_cod_planta(idx),
                       par$mov_des_oid_sector      => par$adocs_mov_des_oid_sector(idx),
                       par$mov_des_cod_sector      => par$adocs_mov_des_cod_sector(idx),
                       par$sal_des_oid_cuenta      => par$adocs_sal_des_oid_cuenta(idx),
                       par$sal_des_oid_client      => par$adocs_sal_des_oid_client(idx),
                       par$sal_des_cod_client      => par$adocs_sal_des_cod_client(idx),
                       par$sal_des_oid_subcli      => par$adocs_sal_des_oid_subcli(idx),
                       par$sal_des_cod_subcli      => par$adocs_sal_des_cod_subcli(idx),
                       par$sal_des_oid_ptserv      => par$adocs_sal_des_oid_ptserv(idx),
                       par$sal_des_cod_ptserv      => par$adocs_sal_des_cod_ptserv(idx),
                       par$mov_ori_oid_cuenta_resp => var$adocs_mov_ori_oid_cuenta(idx),
                       par$mov_ori_oid_sector_resp => var$adocs_mov_ori_oid_sector(idx),
                       par$sal_ori_oid_cuenta_resp => var$adocs_sal_ori_oid_cuenta(idx),
                       par$mov_des_oid_cuenta_resp => var$adocs_mov_des_oid_cuenta(idx),
                       par$mov_des_oid_sector_resp => var$adocs_mov_des_oid_sector(idx),
                       par$sal_des_oid_cuenta_resp => var$adocs_sal_des_oid_cuenta(idx),
                       par$cod_cultura             => par$cod_cultura,
                       par$cod_usuario             => par$usuario,
                       par$inserts                 => var$inserts_int,
                       par$updates                 => var$updates_int,
                       par$selects                 => var$selects_int,
                       par$esreenvio               => var$esreenvio);
    
      par$inserts   := par$inserts + var$inserts_int;
      par$updates   := par$updates + var$updates_int;
      par$selects   := par$selects + var$selects_int;
      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$selects_int;
    
    END LOOP;
  
    /* Guardar documento */
    sguardar_documento(par$oid_formulario            => par$oid_formulario,
                       par$oid_grupo_documento       => par$oid_grupo_documento,
                       par$adocs_oid                 => par$adocs_oid,
                       par$adocs_oid_doc_padre       => par$adocs_oid_doc_padre,
                       par$adocs_oid_sustituto       => par$adocs_oid_sustituto,
                       par$adocs_oid_mov_fondos      => par$adocs_oid_mov_fondos,
                       par$adocs_fyh_plncertif       => par$adocs_fyh_plncertif,
                       par$adocs_fyh_gestion         => par$adocs_fyh_gestion,
                       par$adocs_fyh_contable        => par$adocs_fyh_contable,
                       par$adocs_cod_actual_id       => par$adocs_cod_actual_id,
                       par$adocs_collection_id       => par$adocs_collection_id,                   
                       par$adocs_cod_externo         => par$adocs_cod_externo,
                       par$adocs_oid_cuenta_ori      => var$adocs_mov_ori_oid_cuenta,
                       par$adocs_oid_cuenta_des      => var$adocs_mov_des_oid_cuenta,
                       par$adocs_oid_cuentasaldo_ori => var$adocs_sal_ori_oid_cuenta,
                       par$adocs_oid_cuentasaldo_des => var$adocs_sal_des_oid_cuenta,
                       par$adocs_oid_sector_ori      => var$adocs_mov_ori_oid_sector,
                       par$adocs_oid_sector_des      => var$adocs_mov_des_oid_sector,
                       par$adocs_rowver              => par$adocs_rowver,
                       par$aefdoc_oid_documento      => par$aefdoc_oid_documento,
                       par$aefdoc_oid_divisa         => par$aefdoc_oid_divisa,
                       par$aefdoc_oid_denominacion   => par$aefdoc_oid_denominacion,
                       par$aefdoc_oid_unid_medida    => par$aefdoc_oid_unid_medida,
                       par$aefdoc_cod_niv_detalle    => par$aefdoc_cod_niv_detalle,
                       par$aefdoc_cod_tp_efec_tot    => par$aefdoc_cod_tp_efec_tot,
                       par$aefdoc_oid_calidad        => par$aefdoc_oid_calidad,
                       par$aefdoc_num_importe        => par$aefdoc_num_importe,
                       par$aefdoc_nel_cantidad       => par$aefdoc_nel_cantidad,
                       par$ampdoc_oid_documento      => par$ampdoc_oid_documento,
                       par$ampdoc_oid_divisa         => par$ampdoc_oid_divisa,
                       par$ampdoc_oid_medio_pago     => par$ampdoc_oid_medio_pago,
                       par$ampdoc_cod_tipo_med_pago  => par$ampdoc_cod_tipo_med_pago,
                       par$ampdoc_cod_nivel_detalle  => par$ampdoc_cod_nivel_detalle,
                       par$ampdoc_num_importe        => par$ampdoc_num_importe,
                       par$ampdoc_nel_cantidad       => par$ampdoc_nel_cantidad,
                       par$avtmpdoc_oid_documento    => par$avtmpdoc_oid_documento,
                       par$avtmpdoc_oid_t_mediopago  => par$avtmpdoc_oid_t_mediopago,
                       par$avtmpdoc_des_valor        => par$avtmpdoc_des_valor,
                       par$avtmpdoc_nec_indice_grp   => par$avtmpdoc_nec_indice_grp,
                       par$avtdoc_oid_documento      => par$avtdoc_oid_documento,
                       par$avtdoc_oid_termino        => par$avtdoc_oid_termino,
                       par$avtdoc_des_valor          => par$avtdoc_des_valor,
                       par$usuario                   => par$usuario,
                       par$cod_cultura               => par$cod_cultura,
                       par$inserts                   => var$inserts_int,
                       par$updates                   => var$updates_int,
                       par$deletes                   => var$deletes_int,
                       par$selects                   => var$selects_int,
                       par$esnuevo                   => var$esnuevo);
  
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int + var$deletes_int;
    par$inserts   := par$inserts + var$inserts_int;
    par$updates   := par$updates + var$updates_int;
    par$selects   := par$selects + var$selects_int;
    par$deletes   := par$deletes + var$deletes_int;
  
    /* Clasificacion */
    IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                          par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracClasificacion) THEN
    
      sguardar_valores_anteriores(par$aefadoc_oid_documento     => par$aefadoc_oid_documento,
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
                                  par$usuario                   => par$usuario,
                                  par$inserts                   => var$inserts_int);
      var$registros := var$registros + var$inserts_int;
      par$inserts   := par$inserts + var$inserts_int;
    
    END IF;
  
    IF par$confirmar_doc > 0 THEN
    
      /* limpiar tabla temporaria */
      delete sapr_gtt_toids;
      par$deletes   := par$deletes + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
      
        var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
        var$cod_comprobante      := '';
        var$rowver               := par$adocs_rowver(idx);
      
        sapr_ptransiciones_###VERSION###.sejecutar_doc(par$oid_llamada          => null,
                                                  par$oid_documento        => par$adocs_oid(idx),
                                                  par$cod_estado_documento => var$cod_estado_documento,
                                                  par$cod_comprobante      => var$cod_comprobante,
                                                  par$rowver               => var$rowver,
                                                  par$usuario              => par$usuario,
                                                  par$cod_cultura          => par$cod_cultura,
                                                  par$info_ejecucion       => par$info_ejecucion,
                                                  par$hacer_commit         => 0,
                                                  par$inserts              => var$inserts_int,
                                                  par$updates              => var$updates_int,
                                                  par$deletes              => var$deletes_int,
                                                  par$selects              => var$selects_int,
                                                  par$cod_ejecucion        => par$cod_ejecucion);
      
        var$registros := var$registros + var$inserts_int + var$updates_int +
                         var$selects_int + var$deletes_int;
        par$inserts   := par$inserts + var$inserts_int;
        par$updates   := par$updates + var$updates_int;
        par$selects   := par$selects + var$selects_int;
        par$deletes   := par$deletes + var$deletes_int;
      
        INSERT INTO SAPR_GTT_TOIDS
          (OID, COD_CALIFICADOR)
        VALUES
          (par$adocs_oid(idx), 'DOCUMENTO');
        par$inserts   := par$inserts + sql%ROWCOUNT;
        var$registros := var$registros + sql%ROWCOUNT;
      
      END LOOP;
    
      /* - TABLA: Documentos */
      par$doc_rc_documentos := fcargar_rc_documento;
      par$selects           := par$selects + SQL%ROWCOUNT;
      var$registros         := var$registros + SQL%ROWCOUNT;
    
      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := fcargar_rc_historico;
      par$selects                  := par$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;
    
    END IF;
  
    IF par$hacer_commit > 0 THEN
      COMMIT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
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
    
  END sguardar_doc_valores;

  /* Guardar valores Anteriores de los documentos de clasificacion */
  PROCEDURE sguardar_valores_anteriores(par$aefadoc_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
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
                                        par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  
  BEGIN
  
    /* Grabar los efectivos ANTERIORES del documento */
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefadoc_oid_divisa) THEN
      /*Exclui os registro antigos*/
      DELETE FROM SAPR_TEFECTIVO_ANTERIORXDOC
       WHERE OID_DOCUMENTO IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'DOCUMENTO');
    
      FOR idx IN par$aefadoc_oid_divisa.first .. par$aefadoc_oid_divisa.last LOOP
        sins_tantefectivoxdocumento(par$oid_documento     => par$aefadoc_oid_documento(idx),
                                    par$oid_divisa        => par$aefadoc_oid_divisa(idx),
                                    par$oid_denominacion  => par$aefadoc_oid_denominacion(idx),
                                    par$oid_unidad_medida => par$aefadoc_oid_unid_medida(idx),
                                    par$cod_nivel_detalle => par$aefadoc_cod_niv_detalle(idx),
                                    par$cod_tipo_efec_tot => par$aefadoc_cod_tp_efec_tot(idx),
                                    par$oid_calidad       => par$aefadoc_oid_calidad(idx),
                                    par$num_importe       => par$aefadoc_num_importe(idx),
                                    par$nel_cantidad      => par$aefadoc_nel_cantidad(idx),
                                    par$usuario           => par$usuario,
                                    par$inserts           => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      END LOOP;
    END IF;
  
    /* Grabar los medios de pago ANTERIORES del documento */
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampadoc_oid_divisa) THEN
      /*Exclui os registro antigos*/
      DELETE FROM SAPR_TMEDIO_PAGO_ANTERIORXDOC
       WHERE OID_DOCUMENTO IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'DOCUMENTO');
    
      FOR idx IN par$ampadoc_oid_divisa.first .. par$ampadoc_oid_divisa.last LOOP
        sins_tantmedio_pagoxdocumento(par$oid_documento     => par$ampadoc_oid_documento(idx),
                                      par$oid_divisa        => par$ampadoc_oid_divisa(idx),
                                      par$oid_medio_pago    => par$ampadoc_oid_medio_pago(idx),
                                      par$cod_tipo_med_pago => par$ampadoc_cod_tipo_med_pago(idx),
                                      par$cod_nivel_detalle => par$ampadoc_cod_nivel_detalle(idx),
                                      par$num_importe       => par$ampadoc_num_importe(idx),
                                      par$nel_cantidad      => par$ampadoc_nel_cantidad(idx),
                                      par$usuario           => par$usuario,
                                      par$inserts           => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      END LOOP;
    END IF;
  
    /* Grabar los valores de los terminos de medios de pago ANTERIORES del documento */
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$avtmpadoc_oid_t_mediopago) THEN
      FOR idx IN par$avtmpadoc_oid_t_mediopago.first .. par$avtmpadoc_oid_t_mediopago.last LOOP
        sins_tantvalor_term_med_pago(par$oid_medio_pagoxdoc     => par$avtmpadoc_oid_documento(idx),
                                     par$oid_termino_medio_pago => par$avtmpadoc_oid_t_mediopago(idx),
                                     par$des_valor              => par$avtmpadoc_des_valor(idx),
                                     par$nec_indice_grupo       => par$avtmpadoc_nec_indice_grp(idx),
                                     par$usuario                => par$usuario,
                                     par$inserts                => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      END LOOP;
    END IF;
  
  END sguardar_valores_anteriores;

  /* Validar documento de valores */
  PROCEDURE svalidar_doc_valores(par$aefdoc_oid_divisa IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$ampdoc_oid_divisa IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_doc_valores';
  
  BEGIN
  
    /* consistencia de tamano de los arrays de terminos por documento */
    IF gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefdoc_oid_divisa) AND
       gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampdoc_oid_divisa) THEN
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'No hay ningun valor para las divisas informadas.',
                                                                  const$codFuncionalidad || '.' ||
                                                                  const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$usuario',
                                                                  1));
      RETURN;
    END IF;
  
  END svalidar_doc_valores;

  /* Guardar documento de Actas - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_doc_elem_actas(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    
                                    /* Informaciones formulario */
                                    par$oid_formulario    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$bol_gestion_bulto IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    
                                    /* arrays asociativos */
                                    /* arrays de documentos */
                                    par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                            
                                    par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                    
                                    /* informacion de ejecucion */
                                    par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$info_ejecucion           IN VARCHAR2,
                                    par$hacer_commit             IN INTEGER,
                                    par$confirmar_doc            IN INTEGER,
                                    par$doc_rc_documentos        OUT sys_refcursor,
                                    par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                    par$ele_rc_elementos         OUT sys_refcursor,
                                    par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_doc_elem_actas';
  
    /* variables locales */
    var$esnuevo     BOOLEAN;
    var$inicio      gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err         VARCHAR2(4000);
  
    /* verificar formulario Actas */
    var$oid_formulario     gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_gestion_bulto  gepr_pcomon_###VERSION###.tipo$oid_;
    var$avtdoc_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
  
    var$adocs_mov_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$rowver                   gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_estado_documento     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante          gepr_pcomon_###VERSION###.tipo$cod_;
  
    /* CURSOR DE CUENTA PADRE */
    CURSOR cur$cuentas IS
      SELECT D.OID_CUENTA_ORIGEN,
             D.OID_CUENTA_DESTINO,
             D.OID_CUENTA_SALDO_ORIGEN,
             D.OID_CUENTA_SALDO_DESTINO,
             D.OID_SECTOR_ORIGEN,
             D.OID_SECTOR_DESTINO,
             D.OID_DOCUMENTO
        FROM SAPR_TDOCUMENTO D
       INNER JOIN SAPR_GTT_TOIDS T
          ON T.OID = D.OID_DOCUMENTO
       WHERE T.COD_CALIFICADOR = 'DOC_PADRE';
  
    /* CURSOR DE TERMINOS FORMULARIO */
    CURSOR cur$terminos(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TI.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TFORMULARIO F
       INNER JOIN GEPR_TTERMINO_POR_IAC TI
          ON TI.OID_IAC = F.OID_IAC_INDIVIDUAL
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       WHERE F.OID_FORMULARIO = par$oid_formulario;
  
  BEGIN
  
    var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;
  
    /* Informaciones formulario */
    var$bol_gestion_bulto := par$bol_gestion_bulto;
    var$oid_formulario    := par$oid_formulario;
  
    sapr_pformulario_###VERSION###.srecuperar_formulario_actas(par$oid_formulario    => var$oid_formulario,
                                                          par$bol_gestion_bulto => var$bol_gestion_bulto,
                                                          par$cod_integracion   => par$cod_integracion,
                                                          par$cod_cultura       => par$cod_cultura);
  
    /* Informaciones formulario - termino iac */
    IF par$avtdoc_oid_termino.COUNT > 0 THEN
      IF par$avtdoc_oid_termino(1) IS NULL THEN
        FOR rec$termino IN cur$terminos(var$oid_formulario) LOOP
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
  
    /* Recuperar cuentas de los documentos */
    FOR idx IN par$adocs_oid_doc_padre.first .. par$adocs_oid_doc_padre.last LOOP
      INSERT INTO SAPR_GTT_TOIDS
        (OID, COD_CALIFICADOR)
      VALUES
        (par$adocs_oid_doc_padre(idx), 'DOC_PADRE');
    END LOOP;
    var$adocs_mov_ori_oid_cuenta(0) := NULL;
    var$adocs_sal_ori_oid_cuenta(0) := NULL;
    var$adocs_sal_ori_oid_sector(0) := NULL;
    var$adocs_mov_des_oid_cuenta(0) := NULL;
    var$adocs_sal_des_oid_cuenta(0) := NULL;
    var$adocs_sal_des_oid_sector(0) := NULL;
  
    FOR rec$cuenta IN cur$cuentas LOOP
      FOR idx IN par$adocs_oid_doc_padre.first .. par$adocs_oid_doc_padre.last LOOP
        IF par$adocs_oid_doc_padre(idx) = rec$cuenta.Oid_Documento THEN
          var$adocs_mov_ori_oid_cuenta(idx) := rec$cuenta.OID_CUENTA_DESTINO;
          var$adocs_sal_ori_oid_cuenta(idx) := rec$cuenta.OID_CUENTA_SALDO_DESTINO;
          var$adocs_sal_ori_oid_sector(idx) := rec$cuenta.OID_SECTOR_DESTINO;
          var$adocs_mov_des_oid_cuenta(idx) := rec$cuenta.OID_CUENTA_DESTINO;
          var$adocs_sal_des_oid_cuenta(idx) := rec$cuenta.OID_CUENTA_SALDO_DESTINO;
          var$adocs_sal_des_oid_sector(idx) := rec$cuenta.OID_SECTOR_DESTINO;
          EXIT;
        END IF;
      END LOOP;
    END LOOP rec$cuenta;
  
    /* Guardar documento */
    sguardar_documento(par$oid_formulario            => var$oid_formulario,
                       par$oid_grupo_documento       => par$oid_grupo_documento,
                       par$adocs_oid                 => par$adocs_oid,
                       par$adocs_oid_doc_padre       => par$adocs_oid_doc_padre,
                       par$adocs_oid_sustituto       => par$adocs_oid_sustituto,
                       par$adocs_oid_mov_fondos      => par$adocs_oid_mov_fondos,
                       par$adocs_fyh_plncertif       => par$adocs_fyh_plncertif,
                       par$adocs_fyh_gestion         => par$adocs_fyh_gestion,
                       par$adocs_fyh_contable        => par$adocs_fyh_contable,
                       par$adocs_cod_actual_id       => par$adocs_cod_actual_id,
                       par$adocs_collection_id       => par$adocs_collection_id,  
                       par$adocs_cod_externo         => par$adocs_cod_externo,
                       par$adocs_oid_cuenta_ori      => var$adocs_mov_ori_oid_cuenta,
                       par$adocs_oid_cuenta_des      => var$adocs_mov_des_oid_cuenta,
                       par$adocs_oid_cuentasaldo_ori => var$adocs_sal_ori_oid_cuenta,
                       par$adocs_oid_cuentasaldo_des => var$adocs_sal_des_oid_cuenta,
                       par$adocs_oid_sector_ori      => var$adocs_sal_ori_oid_sector,
                       par$adocs_oid_sector_des      => var$adocs_sal_des_oid_sector,
                       par$adocs_rowver              => par$adocs_rowver,
                       par$aefdoc_oid_documento      => par$aefdoc_oid_documento,
                       par$aefdoc_oid_divisa         => par$aefdoc_oid_divisa,
                       par$aefdoc_oid_denominacion   => par$aefdoc_oid_denominacion,
                       par$aefdoc_oid_unid_medida    => par$aefdoc_oid_unid_medida,
                       par$aefdoc_cod_niv_detalle    => par$aefdoc_cod_niv_detalle,
                       par$aefdoc_cod_tp_efec_tot    => par$aefdoc_cod_tp_efec_tot,
                       par$aefdoc_oid_calidad        => par$aefdoc_oid_calidad,
                       par$aefdoc_num_importe        => par$aefdoc_num_importe,
                       par$aefdoc_nel_cantidad       => par$aefdoc_nel_cantidad,
                       par$ampdoc_oid_documento      => par$ampdoc_oid_documento,
                       par$ampdoc_oid_divisa         => par$ampdoc_oid_divisa,
                       par$ampdoc_oid_medio_pago     => par$ampdoc_oid_medio_pago,
                       par$ampdoc_cod_tipo_med_pago  => par$ampdoc_cod_tipo_med_pago,
                       par$ampdoc_cod_nivel_detalle  => par$ampdoc_cod_nivel_detalle,
                       par$ampdoc_num_importe        => par$ampdoc_num_importe,
                       par$ampdoc_nel_cantidad       => par$ampdoc_nel_cantidad,
                       par$avtmpdoc_oid_documento    => par$avtmpdoc_oid_documento,
                       par$avtmpdoc_oid_t_mediopago  => par$avtmpdoc_oid_t_mediopago,
                       par$avtmpdoc_des_valor        => par$avtmpdoc_des_valor,
                       par$avtmpdoc_nec_indice_grp   => par$avtmpdoc_nec_indice_grp,
                       par$avtdoc_oid_documento      => par$avtdoc_oid_documento,
                       par$avtdoc_oid_termino        => var$avtdoc_oid_termino,
                       par$avtdoc_des_valor          => par$avtdoc_des_valor,
                       par$usuario                   => par$usuario,
                       par$cod_cultura               => par$cod_cultura,
                       par$inserts                   => var$inserts_int,
                       par$updates                   => var$updates_int,
                       par$deletes                   => var$deletes_int,
                       par$selects                   => var$selects_int,
                       par$esnuevo                   => var$esnuevo);
  
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int + var$deletes_int;
    par$inserts   := par$inserts + var$inserts_int;
    par$updates   := par$updates + var$updates_int;
    par$selects   := par$selects + var$selects_int;
    par$deletes   := par$deletes + var$deletes_int;
  
    sapr_pelemento_###VERSION###.sguardar_elemento_acta(par$bol_gestion_bulto          => var$bol_gestion_bulto,
                                                   par$adocs_oid                  => par$adocs_oid,
                                                   par$adocs_oid_cuenta           => var$adocs_mov_des_oid_cuenta,
                                                   par$adocs_oid_cuentasaldo      => var$adocs_sal_des_oid_cuenta,
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
                                                   par$inserts                    => var$inserts_int,
                                                   par$updates                    => var$updates_int,
                                                   par$deletes                    => var$deletes_int,
                                                   par$selects                    => var$selects_int);
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int + var$deletes_int;
    par$inserts   := par$inserts + var$inserts_int;
    par$updates   := par$updates + var$updates_int;
    par$selects   := par$selects + var$selects_int;
    par$deletes   := par$deletes + var$deletes_int;
  
    IF par$confirmar_doc > 0 THEN
    
      /* limpiar tabla temporaria */
      delete sapr_gtt_toids;
      par$deletes   := par$deletes + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
      
        var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
        var$cod_comprobante      := '';
        var$rowver               := par$adocs_rowver(idx);
      
        sapr_ptransiciones_###VERSION###.sejecutar_doc( par$oid_llamada          => null,
                                                  par$oid_documento        => par$adocs_oid(idx),
                                                  par$cod_estado_documento => var$cod_estado_documento,
                                                  par$cod_comprobante      => var$cod_comprobante,
                                                  par$rowver               => var$rowver,
                                                  par$usuario              => par$usuario,
                                                  par$cod_cultura          => par$cod_cultura,
                                                  par$info_ejecucion       => par$info_ejecucion,
                                                  par$hacer_commit         => 0,
                                                  par$inserts              => var$inserts_int,
                                                  par$updates              => var$updates_int,
                                                  par$deletes              => var$deletes_int,
                                                  par$selects              => var$selects_int,
                                                  par$cod_ejecucion        => par$cod_ejecucion);
      
        var$registros := var$registros + var$inserts_int + var$updates_int +
                         var$selects_int + var$deletes_int;
        par$inserts   := par$inserts + var$inserts_int;
        par$updates   := par$updates + var$updates_int;
        par$selects   := par$selects + var$selects_int;
        par$deletes   := par$deletes + var$deletes_int;
      
        INSERT INTO SAPR_GTT_TOIDS
          (OID, COD_CALIFICADOR)
        VALUES
          (par$adocs_oid(idx), 'DOCUMENTO');
        par$inserts   := par$inserts + sql%ROWCOUNT;
        var$registros := var$registros + sql%ROWCOUNT;
      
      END LOOP;
    
      /* - TABLA: Documentos */
      par$doc_rc_documentos := fcargar_rc_documento;
      par$selects           := par$selects + SQL%ROWCOUNT;
      var$registros         := var$registros + SQL%ROWCOUNT;
    
      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := fcargar_rc_historico;
      par$selects                  := par$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;
    
      /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
      par$ele_rc_elementos := sapr_premesa_###VERSION###.fcargar_rc_elementos;
      par$selects          := par$selects + SQL % ROWCOUNT;
      var$registros        := var$registros + SQL % ROWCOUNT;
    
    END IF;
  
    IF par$hacer_commit > 0 THEN
      COMMIT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
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
    
  END sguardar_doc_elem_actas;

  /* Guardar documento de Altas - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_doc_elem_altas(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    
                                    /* Informaciones formulario */
                                    par$oid_formulario    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$bol_gestion_bulto IN gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    
                                    /* arrays asociativos */
                                    /* arrays de documentos */
                                    par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                             
                                    par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                    par$doc_rc_documentos        OUT sys_refcursor,
                                    par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                    par$ele_rc_elementos         OUT sys_refcursor,
                                    par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_doc_elem_altas';
  
    /* variables locales */
    var$esnuevo              BOOLEAN;
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante      gepr_pcomon_###VERSION###.tipo$cod_;
  
    /* verificar formulario Altas */
    var$oid_formulario           gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_gestion_bulto        gepr_pcomon_###VERSION###.tipo$oid_;
    var$avtdoc_oid_termino       gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$rowver                   gepr_pcomon_###VERSION###.tipo$nel_;
  
    /* verificar informaciones para reenvio automatico */
    var$esreenvio             BOOLEAN;
    var$r_oid_formulario      gepr_pcomon_###VERSION###.tipo$oid_;
    var$r_adocs_oid           gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_adocs_oid_sustituto gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_adocs_oid_mov_fondo gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_aefdoc_oid_doc      gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_ampdoc_oid_doc      gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtmpdoc_oid_doc    gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtdoc_oid_doc      gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtdoc_oid_termino  gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_avtdoc_des_valor    gepr_pcomon_###VERSION###.tipo$descs_;
    var$r_aremdoc_oid_doc     gepr_pcomon_###VERSION###.tipo$oids_;
    var$r_abuldoc_oid_doc     gepr_pcomon_###VERSION###.tipo$oids_;
  
    /* LOG */
    var$inicio      gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err         VARCHAR2(4000);
  
    /* CURSOR DE TERMINOS FORMULARIO */
    CURSOR cur$terminos(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TI.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TFORMULARIO F
       INNER JOIN GEPR_TTERMINO_POR_IAC TI
          ON TI.OID_IAC = F.OID_IAC_INDIVIDUAL
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       WHERE F.OID_FORMULARIO = par$oid_formulario;
  
  BEGIN
  
    var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;
  
    /* Informaciones formulario */
    var$bol_gestion_bulto := par$bol_gestion_bulto;
    var$oid_formulario    := par$oid_formulario;
  
    sapr_pformulario_###VERSION###.srecuperar_formulario_altas(par$oid_formulario    => var$oid_formulario,
                                                          par$bol_gestion_bulto => var$bol_gestion_bulto,
                                                          par$cod_integracion   => par$cod_integracion,
                                                          par$cod_cultura       => par$cod_cultura);
  
    /* Informaciones formulario - termino iac */
    IF par$avtdoc_oid_termino.COUNT > 0 THEN
      IF par$avtdoc_oid_termino(1) IS NULL THEN
        FOR rec$termino IN cur$terminos(var$oid_formulario) LOOP
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
  
    /* Recuperar cuentas de los documentos y elementos
    En un documento de Altas todas las cuentas son iguales */
    FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
    
      svalidar_cuentas(par$oid_formulario          => par$oid_formulario,
                       par$mov_ori_oid_cuenta      => par$adocs_mov_ori_oid_cuenta(idx),
                       par$mov_ori_oid_client      => par$adocs_mov_ori_oid_client(idx),
                       par$mov_ori_cod_client      => par$adocs_mov_ori_cod_client(idx),
                       par$mov_ori_oid_subcli      => par$adocs_mov_ori_oid_subcli(idx),
                       par$mov_ori_cod_subcli      => par$adocs_mov_ori_cod_subcli(idx),
                       par$mov_ori_oid_ptserv      => par$adocs_mov_ori_oid_ptserv(idx),
                       par$mov_ori_cod_ptserv      => par$adocs_mov_ori_cod_ptserv(idx),
                       par$mov_ori_oid_canal       => par$adocs_mov_ori_oid_canal(idx),
                       par$mov_ori_cod_canal       => par$adocs_mov_ori_cod_canal(idx),
                       par$mov_ori_oid_subcan      => par$adocs_mov_ori_oid_subcan(idx),
                       par$mov_ori_cod_subcan      => par$adocs_mov_ori_cod_subcan(idx),
                       par$mov_ori_oid_delega      => par$adocs_mov_ori_oid_delega(idx),
                       par$mov_ori_cod_delega      => par$adocs_mov_ori_cod_delega(idx),
                       par$mov_ori_oid_planta      => par$adocs_mov_ori_oid_planta(idx),
                       par$mov_ori_cod_planta      => par$adocs_mov_ori_cod_planta(idx),
                       par$mov_ori_oid_sector      => par$adocs_mov_ori_oid_sector(idx),
                       par$mov_ori_cod_sector      => par$adocs_mov_ori_cod_sector(idx),
                       par$sal_ori_oid_cuenta      => par$adocs_sal_ori_oid_cuenta(idx),
                       par$sal_ori_oid_client      => par$adocs_sal_ori_oid_client(idx),
                       par$sal_ori_cod_client      => par$adocs_sal_ori_cod_client(idx),
                       par$sal_ori_oid_subcli      => par$adocs_sal_ori_oid_subcli(idx),
                       par$sal_ori_cod_subcli      => par$adocs_sal_ori_cod_subcli(idx),
                       par$sal_ori_oid_ptserv      => par$adocs_sal_ori_oid_ptserv(idx),
                       par$sal_ori_cod_ptserv      => par$adocs_sal_ori_cod_ptserv(idx),
                       par$mov_des_oid_cuenta      => par$adocs_mov_des_oid_cuenta(idx),
                       par$mov_des_oid_client      => par$adocs_mov_des_oid_client(idx),
                       par$mov_des_cod_client      => par$adocs_mov_des_cod_client(idx),
                       par$mov_des_oid_subcli      => par$adocs_mov_des_oid_subcli(idx),
                       par$mov_des_cod_subcli      => par$adocs_mov_des_cod_subcli(idx),
                       par$mov_des_oid_ptserv      => par$adocs_mov_des_oid_ptserv(idx),
                       par$mov_des_cod_ptserv      => par$adocs_mov_des_cod_ptserv(idx),
                       par$mov_des_oid_canal       => par$adocs_mov_des_oid_canal(idx),
                       par$mov_des_cod_canal       => par$adocs_mov_des_cod_canal(idx),
                       par$mov_des_oid_subcan      => par$adocs_mov_des_oid_subcan(idx),
                       par$mov_des_cod_subcan      => par$adocs_mov_des_cod_subcan(idx),
                       par$mov_des_oid_delega      => par$adocs_mov_des_oid_delega(idx),
                       par$mov_des_cod_delega      => par$adocs_mov_des_cod_delega(idx),
                       par$mov_des_oid_planta      => par$adocs_mov_des_oid_planta(idx),
                       par$mov_des_cod_planta      => par$adocs_mov_des_cod_planta(idx),
                       par$mov_des_oid_sector      => par$adocs_mov_des_oid_sector(idx),
                       par$mov_des_cod_sector      => par$adocs_mov_des_cod_sector(idx),
                       par$sal_des_oid_cuenta      => par$adocs_sal_des_oid_cuenta(idx),
                       par$sal_des_oid_client      => par$adocs_sal_des_oid_client(idx),
                       par$sal_des_cod_client      => par$adocs_sal_des_cod_client(idx),
                       par$sal_des_oid_subcli      => par$adocs_sal_des_oid_subcli(idx),
                       par$sal_des_cod_subcli      => par$adocs_sal_des_cod_subcli(idx),
                       par$sal_des_oid_ptserv      => par$adocs_sal_des_oid_ptserv(idx),
                       par$sal_des_cod_ptserv      => par$adocs_sal_des_cod_ptserv(idx),
                       par$mov_ori_oid_cuenta_resp => var$adocs_mov_ori_oid_cuenta(idx),
                       par$mov_ori_oid_sector_resp => var$adocs_mov_ori_oid_sector(idx),
                       par$sal_ori_oid_cuenta_resp => var$adocs_sal_ori_oid_cuenta(idx),
                       par$mov_des_oid_cuenta_resp => var$adocs_mov_des_oid_cuenta(idx),
                       par$mov_des_oid_sector_resp => var$adocs_mov_des_oid_sector(idx),
                       par$sal_des_oid_cuenta_resp => var$adocs_sal_des_oid_cuenta(idx),
                       par$cod_cultura             => par$cod_cultura,
                       par$cod_usuario             => par$usuario,
                       par$inserts                 => var$inserts_int,
                       par$updates                 => var$updates_int,
                       par$selects                 => var$selects_int,
                       par$esreenvio               => var$esreenvio);
    
      par$inserts   := par$inserts + var$inserts_int;
      par$updates   := par$updates + var$updates_int;
      par$selects   := par$selects + var$selects_int;
      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$selects_int;
    
    END LOOP;
  
    /* Guardar documento */
    sguardar_documento(par$oid_formulario            => var$oid_formulario,
                       par$oid_grupo_documento       => par$oid_grupo_documento,
                       par$adocs_oid                 => par$adocs_oid,
                       par$adocs_oid_doc_padre       => par$adocs_oid_doc_padre,
                       par$adocs_oid_sustituto       => par$adocs_oid_sustituto,
                       par$adocs_oid_mov_fondos      => par$adocs_oid_mov_fondos,
                       par$adocs_fyh_plncertif       => par$adocs_fyh_plncertif,
                       par$adocs_fyh_gestion         => par$adocs_fyh_gestion,
                       par$adocs_fyh_contable        => par$adocs_fyh_contable,
                       par$adocs_cod_actual_id       => par$adocs_cod_actual_id,
                       par$adocs_collection_id       => par$adocs_collection_id,
                       par$adocs_cod_externo         => par$adocs_cod_externo,
                       par$adocs_oid_cuenta_ori      => var$adocs_mov_ori_oid_cuenta,
                       par$adocs_oid_cuenta_des      => var$adocs_mov_ori_oid_cuenta,
                       par$adocs_oid_cuentasaldo_ori => var$adocs_sal_ori_oid_cuenta,
                       par$adocs_oid_cuentasaldo_des => var$adocs_sal_ori_oid_cuenta,
                       par$adocs_oid_sector_ori      => var$adocs_mov_ori_oid_sector,
                       par$adocs_oid_sector_des      => var$adocs_mov_ori_oid_sector,
                       par$adocs_rowver              => par$adocs_rowver,
                       par$aefdoc_oid_documento      => par$aefdoc_oid_documento,
                       par$aefdoc_oid_divisa         => par$aefdoc_oid_divisa,
                       par$aefdoc_oid_denominacion   => par$aefdoc_oid_denominacion,
                       par$aefdoc_oid_unid_medida    => par$aefdoc_oid_unid_medida,
                       par$aefdoc_cod_niv_detalle    => par$aefdoc_cod_niv_detalle,
                       par$aefdoc_cod_tp_efec_tot    => par$aefdoc_cod_tp_efec_tot,
                       par$aefdoc_oid_calidad        => par$aefdoc_oid_calidad,
                       par$aefdoc_num_importe        => par$aefdoc_num_importe,
                       par$aefdoc_nel_cantidad       => par$aefdoc_nel_cantidad,
                       par$ampdoc_oid_documento      => par$ampdoc_oid_documento,
                       par$ampdoc_oid_divisa         => par$ampdoc_oid_divisa,
                       par$ampdoc_oid_medio_pago     => par$ampdoc_oid_medio_pago,
                       par$ampdoc_cod_tipo_med_pago  => par$ampdoc_cod_tipo_med_pago,
                       par$ampdoc_cod_nivel_detalle  => par$ampdoc_cod_nivel_detalle,
                       par$ampdoc_num_importe        => par$ampdoc_num_importe,
                       par$ampdoc_nel_cantidad       => par$ampdoc_nel_cantidad,
                       par$avtmpdoc_oid_documento    => par$avtmpdoc_oid_documento,
                       par$avtmpdoc_oid_t_mediopago  => par$avtmpdoc_oid_t_mediopago,
                       par$avtmpdoc_des_valor        => par$avtmpdoc_des_valor,
                       par$avtmpdoc_nec_indice_grp   => par$avtmpdoc_nec_indice_grp,
                       par$avtdoc_oid_documento      => par$avtdoc_oid_documento,
                       par$avtdoc_oid_termino        => var$avtdoc_oid_termino,
                       par$avtdoc_des_valor          => par$avtdoc_des_valor,
                       par$usuario                   => par$usuario,
                       par$cod_cultura               => par$cod_cultura,
                       par$inserts                   => var$inserts_int,
                       par$updates                   => var$updates_int,
                       par$deletes                   => var$deletes_int,
                       par$selects                   => var$selects_int,
                       par$esnuevo                   => var$esnuevo);
  
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int + var$deletes_int;
    par$inserts   := par$inserts + var$inserts_int;
    par$updates   := par$updates + var$updates_int;
    par$selects   := par$selects + var$selects_int;
    par$deletes   := par$deletes + var$deletes_int;
  
    sapr_pelemento_###VERSION###.sguardar_elemento(par$esnuevo                    => var$esnuevo,
                                              par$bol_sustituicion           => 0,
                                              par$bol_gestion_bulto          => var$bol_gestion_bulto,
                                              par$bol_baja                   => false,
                                              par$adocs_oid                  => par$adocs_oid,
                                              par$adocs_oid_cuenta           => var$adocs_mov_ori_oid_cuenta,
                                              par$adocs_oid_cuentasaldo      => var$adocs_sal_ori_oid_cuenta,
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
                                              par$inserts                    => var$inserts_int,
                                              par$updates                    => var$updates_int,
                                              par$deletes                    => var$deletes_int,
                                              par$selects                    => var$selects_int);
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int + var$deletes_int;
    par$inserts   := par$inserts + var$inserts_int;
    par$updates   := par$updates + var$updates_int;
    par$selects   := par$selects + var$selects_int;
    par$deletes   := par$deletes + var$deletes_int;
  
    IF par$confirmar_doc > 0 THEN
    
      /* limpiar tabla temporaria */
      delete sapr_gtt_toids;
      par$deletes   := par$deletes + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
      
        var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
        var$cod_comprobante      := '';
        var$rowver               := par$adocs_rowver(idx);
      
        sapr_ptransiciones_###VERSION###.sejecutar_doc( par$oid_llamada          => null,
                                                  par$oid_documento        => par$adocs_oid(idx),
                                                  par$cod_estado_documento => var$cod_estado_documento,
                                                  par$cod_comprobante      => var$cod_comprobante,
                                                  par$rowver               => var$rowver,
                                                  par$usuario              => par$usuario,
                                                  par$cod_cultura          => par$cod_cultura,
                                                  par$info_ejecucion       => par$info_ejecucion,
                                                  par$hacer_commit         => 0,
                                                  par$inserts              => var$inserts_int,
                                                  par$updates              => var$updates_int,
                                                  par$deletes              => var$deletes_int,
                                                  par$selects              => var$selects_int,
                                                  par$cod_ejecucion        => par$cod_ejecucion);
      
        var$registros := var$registros + var$inserts_int + var$updates_int +
                         var$selects_int + var$deletes_int;
        par$inserts   := par$inserts + var$inserts_int;
        par$updates   := par$updates + var$updates_int;
        par$selects   := par$selects + var$selects_int;
        par$deletes   := par$deletes + var$deletes_int;
      
        IF NOT var$esreenvio THEN
          INSERT INTO SAPR_GTT_TOIDS
            (OID, COD_CALIFICADOR)
          VALUES
            (par$adocs_oid(idx), 'DOCUMENTO');
          par$inserts   := par$inserts + sql%ROWCOUNT;
          var$registros := var$registros + sql%ROWCOUNT;
        ELSE
          var$r_adocs_oid(idx) := sys_guid();
          INSERT INTO SAPR_GTT_TOIDS
            (OID, COD_CALIFICADOR)
          VALUES
            (var$r_adocs_oid(idx), 'DOCUMENTO');
          par$inserts   := par$inserts + sql%ROWCOUNT;
          var$registros := var$registros + sql%ROWCOUNT;
        END IF;
      
        var$r_adocs_oid_sustituto(idx) := NULL;
        var$r_adocs_oid_mov_fondo(idx) := NULL;
      
      END LOOP;
    
      IF var$esreenvio THEN
      
        var$r_oid_formulario := sapr_pformulario_###VERSION###.frecuperar_formulario_reenvio(var$oid_formulario,
                                                                                        par$cod_cultura);
      
        /* Atualiza identificadores dos documentos de reenvio */
        FOR idx2 IN par$adocs_oid.first .. par$adocs_oid.last LOOP
        
          /* Valores Efectivos */
          IF NOT
              gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefdoc_oid_documento) THEN
            FOR idx IN par$aefdoc_oid_documento.first .. par$aefdoc_oid_documento.last LOOP
              IF par$aefdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_aefdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
              END IF;
            END LOOP;
          END IF;
        
          /* Valores Medio Pago */
          IF NOT
              gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampdoc_oid_documento) THEN
            FOR idx IN par$ampdoc_oid_documento.first .. par$ampdoc_oid_documento.last LOOP
              IF par$ampdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_ampdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
              END IF;
            END LOOP;
          END IF;
        
          /* Valores Terminos Medio Pago */
          IF NOT
              gepr_putilidades_###VERSION###.farray_vacio_oid(par$avtmpdoc_oid_documento) THEN
            FOR idx IN par$avtmpdoc_oid_documento.first .. par$avtmpdoc_oid_documento.last LOOP
              IF par$avtmpdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_avtmpdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
              END IF;
            END LOOP;
          END IF;
        
          /* Valores Terminos Documento */
          IF NOT
              gepr_putilidades_###VERSION###.farray_vacio_oid(par$avtdoc_oid_documento) THEN
            /* para todos os terminos do reenvio */
            FOR rec$termino IN cur$terminos(var$r_oid_formulario) LOOP
              /* para todos os terminos do altas */
              FOR idx IN par$avtdoc_oid_documento.first .. par$avtdoc_oid_documento.last LOOP
                IF rec$termino.oid_termino = var$avtdoc_oid_termino(idx) THEN
                  IF par$avtdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                    var$r_avtdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
                    var$r_avtdoc_oid_termino(idx) := rec$termino.oid_termino;
                    var$r_avtdoc_des_valor(idx) := par$avtdoc_des_valor(idx);
                  END IF;
                END IF;
              END LOOP;
            END LOOP rec$termino;
          END IF;
        
          /* Valores Remesas */
          IF NOT
              gepr_putilidades_###VERSION###.farray_vacio_oid(par$aremdoc_oid_documento) THEN
            FOR idx IN par$aremdoc_oid_documento.first .. par$aremdoc_oid_documento.last LOOP
              IF par$aremdoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_aremdoc_oid_doc(idx) := var$r_adocs_oid(idx2);
              END IF;
            END LOOP;
          END IF;
        
          /* Valores Bultos */
          IF NOT
              gepr_putilidades_###VERSION###.farray_vacio_oid(par$abuldoc_oid_documento) THEN
            FOR idx IN par$abuldoc_oid_documento.first .. par$abuldoc_oid_documento.last LOOP
              IF par$abuldoc_oid_documento(idx) = par$adocs_oid(idx2) THEN
                var$r_abuldoc_oid_doc(idx) := var$r_adocs_oid(idx2);
              END IF;
            END LOOP;
          END IF;
        
        END LOOP;
      
        sguardar_doc_elem_reenviobaja(par$oid_formulario       => var$r_oid_formulario,
                                      par$oid_grupo_documento  => par$oid_grupo_documento,
                                      par$adocs_oid            => var$r_adocs_oid,
                                      par$adocs_oid_doc_padre  => par$adocs_oid,
                                      par$adocs_oid_sustituto  => var$r_adocs_oid_sustituto,
                                      par$adocs_oid_mov_fondos => var$r_adocs_oid_mov_fondo,
                                      par$adocs_fyh_plncertif  => par$adocs_fyh_plncertif,
                                      par$adocs_fyh_gestion    => par$adocs_fyh_gestion,
                                      par$adocs_fyh_contable   => par$adocs_fyh_contable,
                                      par$adocs_cod_actual_id       => par$adocs_cod_actual_id,
                                      par$adocs_collection_id       => par$adocs_collection_id,  
                                      par$adocs_cod_externo    => par$adocs_cod_externo,
                                      par$adocs_rowver         => par$adocs_rowver,
                                      
                                      par$adocs_mov_ori_oid_cuenta => var$adocs_mov_ori_oid_cuenta,
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
                                      par$adocs_mov_ori_oid_sector => var$adocs_mov_ori_oid_sector,
                                      par$adocs_mov_ori_cod_sector => par$adocs_mov_ori_cod_sector,
                                      
                                      par$adocs_mov_des_oid_cuenta => var$adocs_mov_des_oid_cuenta,
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
                                      par$adocs_mov_des_oid_sector => var$adocs_mov_des_oid_sector,
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
                                      par$abuldoc_oid_bulto        => par$abuldoc_oid_bulto,
                                      par$abuldoc_oid_remesa       => par$abuldoc_oid_remesa,
                                      par$abuldoc_oid_documento    => var$r_abuldoc_oid_doc,
                                      par$usuario                  => par$usuario,
                                      par$cod_cultura              => par$cod_cultura,
                                      par$info_ejecucion           => par$info_ejecucion,
                                      par$hacer_commit             => 0,
                                      par$confirmar_doc            => 1,
                                      par$doc_rc_documentos        => par$doc_rc_documentos,
                                      par$doc_rc_historico_mov_doc => par$doc_rc_historico_mov_doc,
                                      par$ele_rc_elementos         => par$ele_rc_elementos,
                                      par$inserts                  => var$inserts_int,
                                      par$updates                  => var$updates_int,
                                      par$deletes                  => var$deletes_int,
                                      par$selects                  => var$selects_int,
                                      par$cod_ejecucion            => par$cod_ejecucion);
      
        var$registros := var$registros + var$inserts_int + var$updates_int +
                         var$selects_int + var$deletes_int;
        par$inserts   := par$inserts + var$inserts_int;
        par$updates   := par$updates + var$updates_int;
        par$selects   := par$selects + var$selects_int;
        par$deletes   := par$deletes + var$deletes_int;
      
      END IF;
    
      /* - TABLA: Documentos */
      par$doc_rc_documentos := fcargar_rc_documento;
      par$selects           := par$selects + SQL%ROWCOUNT;
      var$registros         := var$registros + SQL%ROWCOUNT;
    
      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := fcargar_rc_historico;
      par$selects                  := par$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;
    
      /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
      par$ele_rc_elementos := sapr_premesa_###VERSION###.fcargar_rc_elementos;
      par$selects          := par$selects + SQL % ROWCOUNT;
      var$registros        := var$registros + SQL % ROWCOUNT;
    
    END IF;
  
    IF par$hacer_commit > 0 THEN
      COMMIT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true);
    
  END sguardar_doc_elem_altas;

  /* Guardar documento de Reenvio/Baja - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_doc_elem_reenviobaja(par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          
                                          /* arrays asociativos */
                                          /* arrays de documentos */
                                          par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                            
                                          par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                          par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                          
                                          /* Bulto - arrays */
                                          par$abuldoc_oid_bulto     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_oid_remesa    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$abuldoc_oid_documento IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          
                                          /* informacion de ejecucion */
                                          par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$hacer_commit             IN INTEGER,
                                          par$confirmar_doc            IN INTEGER,
                                          par$doc_rc_documentos        OUT sys_refcursor,
                                          par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                          par$ele_rc_elementos         OUT sys_refcursor,
                                          par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_doc_elem_reenviobaja';
  
    /* variables locales */
    var$esnuevo                  BOOLEAN;
    var$inicio                   gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros                gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                      VARCHAR2(4000);
    var$adocs_mov_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$cod_estado_documento     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante          gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver                   gepr_pcomon_###VERSION###.tipo$nel_;
    var$esreenvio                BOOLEAN;
    var$bol_baja                 BOOLEAN;
    var$bol_gestion_bulto        gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
  
    /* Cuenta Saldos */
    var$sal_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_ori_oid_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_ori_cod_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_ori_oid_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_ori_cod_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_ori_oid_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_ori_cod_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_des_oid_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_des_cod_client gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_des_oid_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_des_cod_subcli gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_des_oid_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
    var$sal_des_cod_ptserv gepr_pcomon_###VERSION###.tipo$oids_;
  
    /* arrays de terminos por documento */
    var$avtdoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_oid_termino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_des_valor     gepr_pcomon_###VERSION###.tipo$descs_;
  
    /* cursor de terminos documentos */
    CURSOR cur$terminos(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TD.DES_VALOR, TD.OID_DOCUMENTO, T.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TVALOR_TERMINOXDOCUMENTO TD
       INNER JOIN GEPR_TTERMINO T
          ON TD.OID_TERMINO = T.OID_TERMINO
       WHERE TD.OID_DOCUMENTO = par$oid_documento;
  
  BEGIN
  
    var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;
  
    /* limpiar tabla temporaria */
    delete sapr_gtt_toids;
    par$deletes   := par$deletes + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;
  
    /* Recuperar cuentas de los documentos y elementos
    En un documento de Altas todas las cuentas son iguales */
  
    FOR idx IN par$adocs_oid_doc_padre.first .. par$adocs_oid_doc_padre.last LOOP
    
      /* Cuenta Saldo Origen */
      var$sal_ori_oid_cuenta(idx) := NULL;
      var$sal_ori_cod_client(idx) := NULL;
      var$sal_ori_cod_subcli(idx) := NULL;
      var$sal_ori_cod_ptserv(idx) := NULL;
    
      SELECT C.Oid_Cliente, c.oid_subcliente, c.oid_pto_servicio
        INTO var$sal_ori_oid_client(idx),
             var$sal_ori_oid_subcli(idx),
             var$sal_ori_oid_ptserv(idx)
        FROM SAPR_TDOCUMENTO D
       INNER JOIN SAPR_TCUENTA C
          ON C.OID_CUENTA = D.OID_CUENTA_SALDO_ORIGEN
       WHERE D.OID_DOCUMENTO = par$adocs_oid_doc_padre(idx);
    
      par$selects   := par$selects + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
      /* Cuenta Saldo Destino */
      var$sal_des_oid_cuenta(idx) := NULL;
      var$sal_des_cod_client(idx) := NULL;
      var$sal_des_cod_subcli(idx) := NULL;
      var$sal_des_cod_ptserv(idx) := NULL;
    
      SELECT C.Oid_Cliente, c.oid_subcliente, c.oid_pto_servicio
        INTO var$sal_des_oid_client(idx),
             var$sal_des_oid_subcli(idx),
             var$sal_des_oid_ptserv(idx)
        FROM SAPR_TDOCUMENTO D
       INNER JOIN SAPR_TCUENTA C
          ON C.OID_CUENTA = D.OID_CUENTA_SALDO_DESTINO
       WHERE D.OID_DOCUMENTO = par$adocs_oid_doc_padre(idx);
    
      par$selects   := par$selects + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
      /* arrays de terminos por documento */
      FOR rec$termino IN cur$terminos(par$adocs_oid_doc_padre(idx)) LOOP
        var$avtdoc_oid_documento(var$avtdoc_oid_documento.COUNT) := par$adocs_oid(idx);
        var$avtdoc_oid_termino(var$avtdoc_oid_termino.COUNT) := rec$termino.oid_termino;
        var$avtdoc_des_valor(var$avtdoc_des_valor.COUNT) := rec$termino.des_valor;
      END LOOP rec$termino;
      par$selects   := par$selects + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
    END LOOP;
  
    FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
    
      svalidar_cuentas(par$oid_formulario          => par$oid_formulario,
                       par$mov_ori_oid_cuenta      => par$adocs_mov_ori_oid_cuenta(idx),
                       par$mov_ori_oid_client      => par$adocs_mov_ori_oid_client(idx),
                       par$mov_ori_cod_client      => par$adocs_mov_ori_cod_client(idx),
                       par$mov_ori_oid_subcli      => par$adocs_mov_ori_oid_subcli(idx),
                       par$mov_ori_cod_subcli      => par$adocs_mov_ori_cod_subcli(idx),
                       par$mov_ori_oid_ptserv      => par$adocs_mov_ori_oid_ptserv(idx),
                       par$mov_ori_cod_ptserv      => par$adocs_mov_ori_cod_ptserv(idx),
                       par$mov_ori_oid_canal       => par$adocs_mov_ori_oid_canal(idx),
                       par$mov_ori_cod_canal       => par$adocs_mov_ori_cod_canal(idx),
                       par$mov_ori_oid_subcan      => par$adocs_mov_ori_oid_subcan(idx),
                       par$mov_ori_cod_subcan      => par$adocs_mov_ori_cod_subcan(idx),
                       par$mov_ori_oid_delega      => par$adocs_mov_ori_oid_delega(idx),
                       par$mov_ori_cod_delega      => par$adocs_mov_ori_cod_delega(idx),
                       par$mov_ori_oid_planta      => par$adocs_mov_ori_oid_planta(idx),
                       par$mov_ori_cod_planta      => par$adocs_mov_ori_cod_planta(idx),
                       par$mov_ori_oid_sector      => par$adocs_mov_ori_oid_sector(idx),
                       par$mov_ori_cod_sector      => par$adocs_mov_ori_cod_sector(idx),
                       par$sal_ori_oid_cuenta      => var$sal_ori_oid_cuenta(idx),
                       par$sal_ori_oid_client      => var$sal_ori_oid_client(idx),
                       par$sal_ori_cod_client      => var$sal_ori_cod_client(idx),
                       par$sal_ori_oid_subcli      => var$sal_ori_oid_subcli(idx),
                       par$sal_ori_cod_subcli      => var$sal_ori_cod_subcli(idx),
                       par$sal_ori_oid_ptserv      => var$sal_ori_oid_ptserv(idx),
                       par$sal_ori_cod_ptserv      => var$sal_ori_cod_ptserv(idx),
                       par$mov_des_oid_cuenta      => par$adocs_mov_des_oid_cuenta(idx),
                       par$mov_des_oid_client      => par$adocs_mov_des_oid_client(idx),
                       par$mov_des_cod_client      => par$adocs_mov_des_cod_client(idx),
                       par$mov_des_oid_subcli      => par$adocs_mov_des_oid_subcli(idx),
                       par$mov_des_cod_subcli      => par$adocs_mov_des_cod_subcli(idx),
                       par$mov_des_oid_ptserv      => par$adocs_mov_des_oid_ptserv(idx),
                       par$mov_des_cod_ptserv      => par$adocs_mov_des_cod_ptserv(idx),
                       par$mov_des_oid_canal       => par$adocs_mov_des_oid_canal(idx),
                       par$mov_des_cod_canal       => par$adocs_mov_des_cod_canal(idx),
                       par$mov_des_oid_subcan      => par$adocs_mov_des_oid_subcan(idx),
                       par$mov_des_cod_subcan      => par$adocs_mov_des_cod_subcan(idx),
                       par$mov_des_oid_delega      => par$adocs_mov_des_oid_delega(idx),
                       par$mov_des_cod_delega      => par$adocs_mov_des_cod_delega(idx),
                       par$mov_des_oid_planta      => par$adocs_mov_des_oid_planta(idx),
                       par$mov_des_cod_planta      => par$adocs_mov_des_cod_planta(idx),
                       par$mov_des_oid_sector      => par$adocs_mov_des_oid_sector(idx),
                       par$mov_des_cod_sector      => par$adocs_mov_des_cod_sector(idx),
                       par$sal_des_oid_cuenta      => var$sal_des_oid_cuenta(idx),
                       par$sal_des_oid_client      => var$sal_des_oid_client(idx),
                       par$sal_des_cod_client      => var$sal_des_cod_client(idx),
                       par$sal_des_oid_subcli      => var$sal_des_oid_subcli(idx),
                       par$sal_des_cod_subcli      => var$sal_des_cod_subcli(idx),
                       par$sal_des_oid_ptserv      => var$sal_des_oid_ptserv(idx),
                       par$sal_des_cod_ptserv      => var$sal_des_cod_ptserv(idx),
                       par$mov_ori_oid_cuenta_resp => var$adocs_mov_ori_oid_cuenta(idx),
                       par$mov_ori_oid_sector_resp => var$adocs_mov_ori_oid_sector(idx),
                       par$sal_ori_oid_cuenta_resp => var$adocs_sal_ori_oid_cuenta(idx),
                       par$mov_des_oid_cuenta_resp => var$adocs_mov_des_oid_cuenta(idx),
                       par$mov_des_oid_sector_resp => var$adocs_mov_des_oid_sector(idx),
                       par$sal_des_oid_cuenta_resp => var$adocs_sal_des_oid_cuenta(idx),
                       par$cod_cultura             => par$cod_cultura,
                       par$cod_usuario             => par$usuario,
                       par$inserts                 => var$inserts_int,
                       par$updates                 => var$updates_int,
                       par$selects                 => var$selects_int,
                       par$esreenvio               => var$esreenvio);
    
      par$inserts   := par$inserts + var$inserts_int;
      par$updates   := par$updates + var$updates_int;
      par$selects   := par$selects + var$selects_int;
      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$selects_int;
    
    END LOOP;
  
    /* Guardar documento */
    sguardar_documento(par$oid_formulario            => par$oid_formulario,
                       par$oid_grupo_documento       => par$oid_grupo_documento,
                       par$adocs_oid                 => par$adocs_oid,
                       par$adocs_oid_doc_padre       => par$adocs_oid_doc_padre,
                       par$adocs_oid_sustituto       => par$adocs_oid_sustituto,
                       par$adocs_oid_mov_fondos      => par$adocs_oid_mov_fondos,
                       par$adocs_fyh_plncertif       => par$adocs_fyh_plncertif,
                       par$adocs_fyh_gestion         => par$adocs_fyh_gestion,
                       par$adocs_fyh_contable        => par$adocs_fyh_contable,
                       par$adocs_cod_actual_id       => par$adocs_cod_actual_id,
                       par$adocs_collection_id       => par$adocs_collection_id,
                       par$adocs_cod_externo         => par$adocs_cod_externo,
                       par$adocs_oid_cuenta_ori      => var$adocs_mov_ori_oid_cuenta,
                       par$adocs_oid_cuenta_des      => var$adocs_mov_des_oid_cuenta,
                       par$adocs_oid_cuentasaldo_ori => var$adocs_sal_ori_oid_cuenta,
                       par$adocs_oid_cuentasaldo_des => var$adocs_sal_des_oid_cuenta,
                       par$adocs_oid_sector_ori      => var$adocs_mov_ori_oid_sector,
                       par$adocs_oid_sector_des      => var$adocs_mov_des_oid_sector,
                       par$adocs_rowver              => par$adocs_rowver,
                       par$aefdoc_oid_documento      => par$aefdoc_oid_documento,
                       par$aefdoc_oid_divisa         => par$aefdoc_oid_divisa,
                       par$aefdoc_oid_denominacion   => par$aefdoc_oid_denominacion,
                       par$aefdoc_oid_unid_medida    => par$aefdoc_oid_unid_medida,
                       par$aefdoc_cod_niv_detalle    => par$aefdoc_cod_niv_detalle,
                       par$aefdoc_cod_tp_efec_tot    => par$aefdoc_cod_tp_efec_tot,
                       par$aefdoc_oid_calidad        => par$aefdoc_oid_calidad,
                       par$aefdoc_num_importe        => par$aefdoc_num_importe,
                       par$aefdoc_nel_cantidad       => par$aefdoc_nel_cantidad,
                       par$ampdoc_oid_documento      => par$ampdoc_oid_documento,
                       par$ampdoc_oid_divisa         => par$ampdoc_oid_divisa,
                       par$ampdoc_oid_medio_pago     => par$ampdoc_oid_medio_pago,
                       par$ampdoc_cod_tipo_med_pago  => par$ampdoc_cod_tipo_med_pago,
                       par$ampdoc_cod_nivel_detalle  => par$ampdoc_cod_nivel_detalle,
                       par$ampdoc_num_importe        => par$ampdoc_num_importe,
                       par$ampdoc_nel_cantidad       => par$ampdoc_nel_cantidad,
                       par$avtmpdoc_oid_documento    => par$avtmpdoc_oid_documento,
                       par$avtmpdoc_oid_t_mediopago  => par$avtmpdoc_oid_t_mediopago,
                       par$avtmpdoc_des_valor        => par$avtmpdoc_des_valor,
                       par$avtmpdoc_nec_indice_grp   => par$avtmpdoc_nec_indice_grp,
                       par$avtdoc_oid_documento      => var$avtdoc_oid_documento,
                       par$avtdoc_oid_termino        => var$avtdoc_oid_termino,
                       par$avtdoc_des_valor          => var$avtdoc_des_valor,
                       par$usuario                   => par$usuario,
                       par$cod_cultura               => par$cod_cultura,
                       par$inserts                   => var$inserts_int,
                       par$updates                   => var$updates_int,
                       par$deletes                   => var$deletes_int,
                       par$selects                   => var$selects_int,
                       par$esnuevo                   => var$esnuevo);
  
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int + var$deletes_int;
    par$inserts   := par$inserts + var$inserts_int;
    par$updates   := par$updates + var$updates_int;
    par$selects   := par$selects + var$selects_int;
    par$deletes   := par$deletes + var$deletes_int;
  
    /*Verifica se o formulario de baja*/
    var$bol_baja := sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario,
                                                                       gepr_pcomon_###VERSION###.const$CodCaracBaja);
    if sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario,
                                                          gepr_pcomon_###VERSION###.const$CodCaracGestionBulto) then
      var$bol_gestion_bulto := 1;
    end if;
  
    /* === Cambios en los elementos === */
    SAPR_PELEMENTO_###VERSION###.stratar_reenvio_baja(par$aremdoc_oid_remesa    => par$aremdoc_oid_remesa,
                                                 par$aremdoc_oid_documento => par$aremdoc_oid_documento,
                                                 par$abuldoc_oid_bulto     => par$abuldoc_oid_bulto,
                                                 par$abuldoc_oid_remesa    => par$abuldoc_oid_remesa,
                                                 par$abuldoc_oid_documento => par$abuldoc_oid_documento,
                                                 par$usuario               => par$usuario,
                                                 par$cod_cultura           => par$cod_cultura,
                                                 par$bol_gestion_bulto     => var$bol_gestion_bulto,
                                                 par$bol_baja              => var$bol_baja,
                                                 par$inserts               => var$inserts_int,
                                                 par$updates               => var$updates_int,
                                                 par$deletes               => var$deletes_int,
                                                 par$selects               => var$selects_int);
  
    IF par$confirmar_doc > 0 THEN
    
      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
      
        var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
        var$cod_comprobante      := '';
        var$rowver               := par$adocs_rowver(idx);
      
        sapr_ptransiciones_###VERSION###.sejecutar_doc( par$oid_llamada          => null,
                                                  par$oid_documento        => par$adocs_oid(idx),
                                                  par$cod_estado_documento => var$cod_estado_documento,
                                                  par$cod_comprobante      => var$cod_comprobante,
                                                  par$rowver               => var$rowver,
                                                  par$usuario              => par$usuario,
                                                  par$cod_cultura          => par$cod_cultura,
                                                  par$info_ejecucion       => par$info_ejecucion,
                                                  par$hacer_commit         => 0,
                                                  par$inserts              => var$inserts_int,
                                                  par$updates              => var$updates_int,
                                                  par$deletes              => var$deletes_int,
                                                  par$selects              => var$selects_int,
                                                  par$cod_ejecucion        => par$cod_ejecucion);
      
        var$registros := var$registros + var$inserts_int + var$updates_int +
                         var$selects_int + var$deletes_int;
        par$inserts   := par$inserts + var$inserts_int;
        par$updates   := par$updates + var$updates_int;
        par$selects   := par$selects + var$selects_int;
        par$deletes   := par$deletes + var$deletes_int;
      
        INSERT INTO SAPR_GTT_TOIDS
          (OID, COD_CALIFICADOR)
        VALUES
          (par$adocs_oid(idx), 'DOCUMENTO');
        par$inserts   := par$inserts + sql%ROWCOUNT;
        var$registros := var$registros + sql%ROWCOUNT;
      
      END LOOP;
    
      /* - TABLA: Documentos */
      par$doc_rc_documentos := fcargar_rc_documento;
      par$selects           := par$selects + SQL%ROWCOUNT;
      var$registros         := var$registros + SQL%ROWCOUNT;
    
      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := fcargar_rc_historico;
      par$selects                  := par$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;
    
      /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
      par$ele_rc_elementos := sapr_premesa_###VERSION###.fcargar_rc_elementos;
      par$selects          := par$selects + SQL % ROWCOUNT;
      var$registros        := var$registros + SQL % ROWCOUNT;
    
    END IF;
  
    IF par$hacer_commit > 0 THEN
      COMMIT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true);
    
  END sguardar_doc_elem_reenviobaja;

  /* Guardar documento de sustitucion - Gestion de Bultos/Remesas */
  PROCEDURE sguardar_doc_elem_sustitucion(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          
                                          /* arrays asociativos */
                                          /* arrays de documentos */
                                          par$adocs_oid            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_doc_padre  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_sustituto  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_oid_mov_fondos IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adocs_fyh_plncertif  IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                          par$adocs_fyh_contable   IN gepr_pcomon_###VERSION###.tipo$fyhs_,                                
                                          par$adocs_cod_actual_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                          par$adocs_collection_id  IN gepr_pcomon_###VERSION###.tipo$descs_,
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
                                          par$doc_rc_documentos        OUT sys_refcursor,
                                          par$doc_rc_historico_mov_doc OUT sys_refcursor,
                                          par$ele_rc_elementos         OUT sys_refcursor,
                                          par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sguardar_doc_elem_sustitucion';
  
    /* variables locales */
    var$esnuevo                  BOOLEAN;
    var$inicio                   gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros                gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int              gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                      VARCHAR2(4000);
    var$adocs_mov_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_sector gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_oid_cuenta gepr_pcomon_###VERSION###.tipo$oids_;
    var$cod_estado_documento     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante          gepr_pcomon_###VERSION###.tipo$cod_;
    var$bol_gestion_bulto        gepr_pcomon_###VERSION###.tipo$oid_ := 0;
    var$rowver                   gepr_pcomon_###VERSION###.tipo$nel_;
    var$esreenvio                BOOLEAN;
  BEGIN
  
    var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;
  
    /* limpiar tabla temporaria */
    delete sapr_gtt_toids;
    par$deletes   := par$deletes + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;
  
    /* Verificar si es gestion de bulto */
    IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                          par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionBulto) THEN
      var$bol_gestion_bulto := 1;
    END IF;
  
    /* Recuperar cuentas de los documentos y elementos
    En un documento de Altas todas las cuentas son iguales */
    FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
    
      svalidar_cuentas(par$oid_formulario          => par$oid_formulario,
                       par$mov_ori_oid_cuenta      => par$adocs_mov_ori_oid_cuenta(idx),
                       par$mov_ori_oid_client      => par$adocs_mov_ori_oid_client(idx),
                       par$mov_ori_cod_client      => par$adocs_mov_ori_cod_client(idx),
                       par$mov_ori_oid_subcli      => par$adocs_mov_ori_oid_subcli(idx),
                       par$mov_ori_cod_subcli      => par$adocs_mov_ori_cod_subcli(idx),
                       par$mov_ori_oid_ptserv      => par$adocs_mov_ori_oid_ptserv(idx),
                       par$mov_ori_cod_ptserv      => par$adocs_mov_ori_cod_ptserv(idx),
                       par$mov_ori_oid_canal       => par$adocs_mov_ori_oid_canal(idx),
                       par$mov_ori_cod_canal       => par$adocs_mov_ori_cod_canal(idx),
                       par$mov_ori_oid_subcan      => par$adocs_mov_ori_oid_subcan(idx),
                       par$mov_ori_cod_subcan      => par$adocs_mov_ori_cod_subcan(idx),
                       par$mov_ori_oid_delega      => par$adocs_mov_ori_oid_delega(idx),
                       par$mov_ori_cod_delega      => par$adocs_mov_ori_cod_delega(idx),
                       par$mov_ori_oid_planta      => par$adocs_mov_ori_oid_planta(idx),
                       par$mov_ori_cod_planta      => par$adocs_mov_ori_cod_planta(idx),
                       par$mov_ori_oid_sector      => par$adocs_mov_ori_oid_sector(idx),
                       par$mov_ori_cod_sector      => par$adocs_mov_ori_cod_sector(idx),
                       par$sal_ori_oid_cuenta      => par$adocs_sal_ori_oid_cuenta(idx),
                       par$sal_ori_oid_client      => par$adocs_sal_ori_oid_client(idx),
                       par$sal_ori_cod_client      => par$adocs_sal_ori_cod_client(idx),
                       par$sal_ori_oid_subcli      => par$adocs_sal_ori_oid_subcli(idx),
                       par$sal_ori_cod_subcli      => par$adocs_sal_ori_cod_subcli(idx),
                       par$sal_ori_oid_ptserv      => par$adocs_sal_ori_oid_ptserv(idx),
                       par$sal_ori_cod_ptserv      => par$adocs_sal_ori_cod_ptserv(idx),
                       par$mov_des_oid_cuenta      => par$adocs_mov_des_oid_cuenta(idx),
                       par$mov_des_oid_client      => par$adocs_mov_des_oid_client(idx),
                       par$mov_des_cod_client      => par$adocs_mov_des_cod_client(idx),
                       par$mov_des_oid_subcli      => par$adocs_mov_des_oid_subcli(idx),
                       par$mov_des_cod_subcli      => par$adocs_mov_des_cod_subcli(idx),
                       par$mov_des_oid_ptserv      => par$adocs_mov_des_oid_ptserv(idx),
                       par$mov_des_cod_ptserv      => par$adocs_mov_des_cod_ptserv(idx),
                       par$mov_des_oid_canal       => par$adocs_mov_des_oid_canal(idx),
                       par$mov_des_cod_canal       => par$adocs_mov_des_cod_canal(idx),
                       par$mov_des_oid_subcan      => par$adocs_mov_des_oid_subcan(idx),
                       par$mov_des_cod_subcan      => par$adocs_mov_des_cod_subcan(idx),
                       par$mov_des_oid_delega      => par$adocs_mov_des_oid_delega(idx),
                       par$mov_des_cod_delega      => par$adocs_mov_des_cod_delega(idx),
                       par$mov_des_oid_planta      => par$adocs_mov_des_oid_planta(idx),
                       par$mov_des_cod_planta      => par$adocs_mov_des_cod_planta(idx),
                       par$mov_des_oid_sector      => par$adocs_mov_des_oid_sector(idx),
                       par$mov_des_cod_sector      => par$adocs_mov_des_cod_sector(idx),
                       par$sal_des_oid_cuenta      => par$adocs_sal_des_oid_cuenta(idx),
                       par$sal_des_oid_client      => par$adocs_sal_des_oid_client(idx),
                       par$sal_des_cod_client      => par$adocs_sal_des_cod_client(idx),
                       par$sal_des_oid_subcli      => par$adocs_sal_des_oid_subcli(idx),
                       par$sal_des_cod_subcli      => par$adocs_sal_des_cod_subcli(idx),
                       par$sal_des_oid_ptserv      => par$adocs_sal_des_oid_ptserv(idx),
                       par$sal_des_cod_ptserv      => par$adocs_sal_des_cod_ptserv(idx),
                       par$mov_ori_oid_cuenta_resp => var$adocs_mov_ori_oid_cuenta(idx),
                       par$mov_ori_oid_sector_resp => var$adocs_mov_ori_oid_sector(idx),
                       par$sal_ori_oid_cuenta_resp => var$adocs_sal_ori_oid_cuenta(idx),
                       par$mov_des_oid_cuenta_resp => var$adocs_mov_des_oid_cuenta(idx),
                       par$mov_des_oid_sector_resp => var$adocs_mov_des_oid_sector(idx),
                       par$sal_des_oid_cuenta_resp => var$adocs_sal_des_oid_cuenta(idx),
                       par$cod_cultura             => par$cod_cultura,
                       par$cod_usuario             => par$usuario,
                       par$inserts                 => var$inserts_int,
                       par$updates                 => var$updates_int,
                       par$selects                 => var$selects_int,
                       par$esreenvio               => var$esreenvio);
    
      par$inserts   := par$inserts + var$inserts_int;
      par$updates   := par$updates + var$updates_int;
      par$selects   := par$selects + var$selects_int;
      var$registros := var$registros + var$inserts_int + var$updates_int +
                       var$selects_int;
    
    END LOOP;
  
    /* Guardar documento */
    sguardar_documento(par$oid_formulario            => par$oid_formulario,
                       par$oid_grupo_documento       => par$oid_grupo_documento,
                       par$adocs_oid                 => par$adocs_oid,
                       par$adocs_oid_doc_padre       => par$adocs_oid_doc_padre,
                       par$adocs_oid_sustituto       => par$adocs_oid_sustituto,
                       par$adocs_oid_mov_fondos      => par$adocs_oid_mov_fondos,
                       par$adocs_fyh_plncertif       => par$adocs_fyh_plncertif,
                       par$adocs_fyh_gestion         => par$adocs_fyh_gestion,
                       par$adocs_fyh_contable        => par$adocs_fyh_contable,
                       par$adocs_cod_actual_id       => par$adocs_cod_actual_id,
                       par$adocs_collection_id       => par$adocs_collection_id, 
                       par$adocs_cod_externo         => par$adocs_cod_externo,
                       par$adocs_oid_cuenta_ori      => var$adocs_mov_ori_oid_cuenta,
                       par$adocs_oid_cuenta_des      => var$adocs_mov_ori_oid_cuenta,
                       par$adocs_oid_cuentasaldo_ori => var$adocs_sal_ori_oid_cuenta,
                       par$adocs_oid_cuentasaldo_des => var$adocs_sal_ori_oid_cuenta,
                       par$adocs_oid_sector_ori      => var$adocs_mov_ori_oid_sector,
                       par$adocs_oid_sector_des      => var$adocs_mov_ori_oid_sector,
                       par$adocs_rowver              => par$adocs_rowver,
                       par$aefdoc_oid_documento      => par$aefdoc_oid_documento,
                       par$aefdoc_oid_divisa         => par$aefdoc_oid_divisa,
                       par$aefdoc_oid_denominacion   => par$aefdoc_oid_denominacion,
                       par$aefdoc_oid_unid_medida    => par$aefdoc_oid_unid_medida,
                       par$aefdoc_cod_niv_detalle    => par$aefdoc_cod_niv_detalle,
                       par$aefdoc_cod_tp_efec_tot    => par$aefdoc_cod_tp_efec_tot,
                       par$aefdoc_oid_calidad        => par$aefdoc_oid_calidad,
                       par$aefdoc_num_importe        => par$aefdoc_num_importe,
                       par$aefdoc_nel_cantidad       => par$aefdoc_nel_cantidad,
                       par$ampdoc_oid_documento      => par$ampdoc_oid_documento,
                       par$ampdoc_oid_divisa         => par$ampdoc_oid_divisa,
                       par$ampdoc_oid_medio_pago     => par$ampdoc_oid_medio_pago,
                       par$ampdoc_cod_tipo_med_pago  => par$ampdoc_cod_tipo_med_pago,
                       par$ampdoc_cod_nivel_detalle  => par$ampdoc_cod_nivel_detalle,
                       par$ampdoc_num_importe        => par$ampdoc_num_importe,
                       par$ampdoc_nel_cantidad       => par$ampdoc_nel_cantidad,
                       par$avtmpdoc_oid_documento    => par$avtmpdoc_oid_documento,
                       par$avtmpdoc_oid_t_mediopago  => par$avtmpdoc_oid_t_mediopago,
                       par$avtmpdoc_des_valor        => par$avtmpdoc_des_valor,
                       par$avtmpdoc_nec_indice_grp   => par$avtmpdoc_nec_indice_grp,
                       par$avtdoc_oid_documento      => par$avtdoc_oid_documento,
                       par$avtdoc_oid_termino        => par$avtdoc_oid_termino,
                       par$avtdoc_des_valor          => par$avtdoc_des_valor,
                       par$usuario                   => par$usuario,
                       par$cod_cultura               => par$cod_cultura,
                       par$inserts                   => var$inserts_int,
                       par$updates                   => var$updates_int,
                       par$deletes                   => var$deletes_int,
                       par$selects                   => var$selects_int,
                       par$esnuevo                   => var$esnuevo);
  
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int + var$deletes_int;
    par$inserts   := par$inserts + var$inserts_int;
    par$updates   := par$updates + var$updates_int;
    par$selects   := par$selects + var$selects_int;
    par$deletes   := par$deletes + var$deletes_int;
  
    sapr_pelemento_###VERSION###.sguardar_elemento(par$esnuevo                    => var$esnuevo,
                                              par$bol_sustituicion           => 1,
                                              par$bol_gestion_bulto          => var$bol_gestion_bulto,
                                              par$bol_baja                   => false,
                                              par$adocs_oid                  => par$adocs_oid,
                                              par$adocs_oid_cuenta           => var$adocs_mov_ori_oid_cuenta,
                                              par$adocs_oid_cuentasaldo      => var$adocs_sal_ori_oid_cuenta,
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
                                              par$inserts                    => var$inserts_int,
                                              par$updates                    => var$updates_int,
                                              par$deletes                    => var$deletes_int,
                                              par$selects                    => var$selects_int);
    var$registros := var$registros + var$inserts_int + var$updates_int +
                     var$selects_int + var$deletes_int;
    par$inserts   := par$inserts + var$inserts_int;
    par$updates   := par$updates + var$updates_int;
    par$selects   := par$selects + var$selects_int;
    par$deletes   := par$deletes + var$deletes_int;
  
    IF par$confirmar_doc > 0 THEN
    
      FOR idx IN par$adocs_oid.first .. par$adocs_oid.last LOOP
      
        var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
        var$cod_comprobante      := '';
        var$rowver               := par$adocs_rowver(idx);
      
        sapr_ptransiciones_###VERSION###.sejecutar_doc( par$oid_llamada          => null,
                                                  par$oid_documento        => par$adocs_oid(idx),
                                                  par$cod_estado_documento => var$cod_estado_documento,
                                                  par$cod_comprobante      => var$cod_comprobante,
                                                  par$rowver               => var$rowver,
                                                  par$usuario              => par$usuario,
                                                  par$cod_cultura          => par$cod_cultura,
                                                  par$info_ejecucion       => par$info_ejecucion,
                                                  par$hacer_commit         => 0,
                                                  par$inserts              => var$inserts_int,
                                                  par$updates              => var$updates_int,
                                                  par$deletes              => var$deletes_int,
                                                  par$selects              => var$selects_int,
                                                  par$cod_ejecucion        => par$cod_ejecucion);
      
        var$registros := var$registros + var$inserts_int + var$updates_int +
                         var$selects_int + var$deletes_int;
        par$inserts   := par$inserts + var$inserts_int;
        par$updates   := par$updates + var$updates_int;
        par$selects   := par$selects + var$selects_int;
        par$deletes   := par$deletes + var$deletes_int;
      
        INSERT INTO SAPR_GTT_TOIDS
          (OID, COD_CALIFICADOR)
        VALUES
          (par$adocs_oid(idx), 'DOCUMENTO');
        par$inserts   := par$inserts + sql%ROWCOUNT;
        var$registros := var$registros + sql%ROWCOUNT;
      
      END LOOP;
    
      /* - TABLA: Documentos */
      par$doc_rc_documentos := fcargar_rc_documento;
      par$selects           := par$selects + SQL%ROWCOUNT;
      var$registros         := var$registros + SQL%ROWCOUNT;
    
      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := fcargar_rc_historico;
      par$selects                  := par$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;
    
      /* - TABLA: Principale - informaciones de Remesa/Bulto/Parcial */
      par$ele_rc_elementos := sapr_premesa_###VERSION###.fcargar_rc_elementos;
      par$selects          := par$selects + SQL % ROWCOUNT;
      var$registros        := var$registros + SQL % ROWCOUNT;
    
    END IF;
  
    IF par$hacer_commit > 0 THEN
      COMMIT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true);
    
  END sguardar_doc_elem_sustitucion;

  /* Guardar documento de fondos - MAE */
  PROCEDURE sguardar_doc_fondos(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_accion               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_ajeno                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$adoc_fyh_contable        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$adoc_cod_actual_id       IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$adoc_cod_collection_id   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$adoc_ori_cod_client      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_canal       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_delega      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_client      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_canal       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_delega      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$aefdoc_cod_divisa        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_num_importe_total IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$aefdoc_cod_denominacion  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_cod_div_den       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$aefdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$ampdoc_cod_divisa        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$ampdoc_cod_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$avtdoc_cod_termino       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$avtdoc_des_valor         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$bol_validar_cod_externo  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                par$info_ejecucion           IN gepr_pcomon_###VERSION###.tipo$obs_,
                                par$hacer_commit             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$validaciones             OUT sys_refcursor,
                                par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    const$des_origen CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sguardar_doc_fondos'; 
    const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
    /* variables locales */
    var$inicio    gepr_pcomon_###VERSION###.tipo$gmt_ := SYSTIMESTAMP;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err       VARCHAR2(4000);
  
    /* variables necesarias para llamar la procedure sguardar_doc_valores */
    var$oid_divisa                gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_formulario            gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_documento             gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_estado_documento      gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_comprobante           gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver                    gepr_pcomon_###VERSION###.tipo$nel_;
    var$adocs_oid                 gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_doc_padre       gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_sustituto       gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_mov_fondos      gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_fyh_plncertif       gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_fyh_gestion         gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_fyh_contable        gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_cod_actual_id       gepr_pcomon_###VERSION###.tipo$descs_;
    var$adocs_cod_collection_id   gepr_pcomon_###VERSION###.tipo$descs_;
    var$adocs_cod_externo         gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_rowver              gepr_pcomon_###VERSION###.tipo$nels_;
    var$adocs_mov_ori_oid_cuenta  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_oid_client  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_client  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_subcli  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_subcli  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_ptserv  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_ptserv  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_canal   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_canal   gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_subcan  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_subcan  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_delega  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_delega  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_planta  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_planta  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_ori_oid_sector  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_ori_cod_sector  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_ori_oid_cuenta  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_oid_client  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_cod_client  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_ori_oid_subcli  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_cod_subcli  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_ori_oid_ptserv  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_ori_cod_ptserv  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_cuenta  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_oid_client  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_client  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_subcli  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_subcli  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_ptserv  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_ptserv  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_canal   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_canal   gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_subcan  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_subcan  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_delega  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_delega  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_planta  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_planta  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_mov_des_oid_sector  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_mov_des_cod_sector  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_des_oid_cuenta  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_oid_client  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_cod_client  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_des_oid_subcli  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_cod_subcli  gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_sal_des_oid_ptserv  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_sal_des_cod_ptserv  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_oid_documento      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_divisa         gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_denominacion   gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_unid_medida    gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_cod_niv_detalle    gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_cod_tp_efec_tot    gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_oid_calidad        gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_num_importe        gepr_pcomon_###VERSION###.tipo$imps_;
    var$aefdoc_nel_cantidad       gepr_pcomon_###VERSION###.tipo$nels_;
    var$ampdoc_oid_documento      gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_oid_divisa         gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_oid_medio_pago     gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_cod_tipo_med_pago  gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_cod_nivel_detalle  gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_num_importe        gepr_pcomon_###VERSION###.tipo$imps_;
    var$ampdoc_nel_cantidad       gepr_pcomon_###VERSION###.tipo$nels_;
    var$avtmpdoc_oid_documento    gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_oid_t_mediopago  gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_des_valor        gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtmpdoc_nec_indice_grp   gepr_pcomon_###VERSION###.tipo$ints_;
    var$aefadoc_oid_documento     gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefadoc_oid_divisa        gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefadoc_oid_denominacion  gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefadoc_oid_unid_medida   gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefadoc_cod_niv_detalle   gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefadoc_cod_tp_efec_tot   gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefadoc_oid_calidad       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefadoc_num_importe       gepr_pcomon_###VERSION###.tipo$imps_;
    var$aefadoc_nel_cantidad      gepr_pcomon_###VERSION###.tipo$nels_;
    var$ampadoc_oid_documento     gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampadoc_oid_divisa        gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampadoc_oid_medio_pago    gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampadoc_cod_tipo_med_pago gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampadoc_cod_nivel_detalle gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampadoc_num_importe       gepr_pcomon_###VERSION###.tipo$imps_;
    var$ampadoc_nel_cantidad      gepr_pcomon_###VERSION###.tipo$nels_;
    var$avtmpadoc_oid_documento   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpadoc_oid_t_mediopago gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpadoc_des_valor       gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtmpadoc_nec_indice_grp  gepr_pcomon_###VERSION###.tipo$ints_;
    var$avtdoc_oid_documento      gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_oid_termino        gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_des_valor          gepr_pcomon_###VERSION###.tipo$descs_;
    var$doc_rc_documentos         sys_refcursor;
    var$doc_rc_historico_mov_doc  sys_refcursor;
    var$confirmar_doc             gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$inserts_int               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$cod_ejecucion             gepr_pcomon_###VERSION###.tipo$cod_;
  
    /* Validaciones */
    var$validaciones     gepr_pcomon_###VERSION###.tipo$descs_;
    var$validaciones_int gepr_pcomon_###VERSION###.tipo$descs_;
    var$tvalidaciones    varchar2_array;
  
  BEGIN
    IF par$oid_llamada IS NOT NULL THEN
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'oid_llamada: '||par$oid_llamada||', cod_accion: '||par$cod_accion||', cod_ajeno: '||par$cod_ajeno||', adoc_cod_formulario: '||par$adoc_cod_formulario||
                                                    ', adoc_cod_externo: '||par$adoc_cod_externo||', adoc_fyh_gestion: '||par$adoc_fyh_gestion||', adoc_fyh_contable: '||par$adoc_fyh_contable||
                                                    ', adoc_cod_actual_id: '||par$adoc_cod_actual_id||', adoc_cod_collection_id: '||par$adoc_cod_collection_id||', adoc_ori_cod_client: '||par$adoc_ori_cod_client||
                                                    ', adoc_ori_cod_subcli: '||par$adoc_ori_cod_subcli||', adoc_ori_cod_ptserv: '||par$adoc_ori_cod_ptserv||', adoc_ori_cod_canal: '||par$adoc_ori_cod_canal||
                                                    ', adoc_ori_cod_subcan: '||par$adoc_ori_cod_subcan||', adoc_ori_cod_delega: '||par$adoc_ori_cod_delega||', adoc_ori_cod_planta: '||par$adoc_ori_cod_planta||
                                                    ', adoc_ori_cod_sector: '||par$adoc_ori_cod_sector||', adoc_des_cod_client: '||par$adoc_des_cod_client||', adoc_des_cod_subcli: '||par$adoc_des_cod_subcli||
                                                    ', adoc_des_cod_ptserv: '||par$adoc_des_cod_ptserv||', adoc_des_cod_canal: '||par$adoc_des_cod_canal||', adoc_des_cod_subcan: '||par$adoc_des_cod_subcan||
                                                    ', adoc_des_cod_delega: '||par$adoc_des_cod_delega||', adoc_des_cod_planta: '||par$adoc_des_cod_planta||', adoc_des_cod_sector: '||par$adoc_des_cod_sector||
                                                    ', bol_validar_cod_externo: '||par$bol_validar_cod_externo||' , cod_cultura: '||par$cod_cultura||
                                                    ', cod_usuario: '||par$cod_usuario||', info_ejecucion: '||par$info_ejecucion||', hacer_commit: '||par$hacer_commit,
                              par$cod_identificador   => '');
      

      
      
      FOR idx IN  par$aefdoc_cod_divisa.first ..  par$aefdoc_cod_divisa.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$aefdoc_cod_divisa: '|| idx || ' - ' || par$aefdoc_cod_divisa(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$aefdoc_num_importe_total.first ..  par$aefdoc_num_importe_total.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$aefdoc_num_importe_total: '|| idx || ' - ' || par$aefdoc_num_importe_total(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$aefdoc_cod_denominacion.first ..  par$aefdoc_cod_denominacion.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$aefdoc_cod_denominacion: '|| idx || ' - ' || par$aefdoc_cod_denominacion(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$aefdoc_cod_div_den.first ..  par$aefdoc_cod_div_den.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$aefdoc_cod_div_den: '|| idx || ' - ' || par$aefdoc_cod_div_den(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$aefdoc_num_importe.first ..  par$aefdoc_num_importe.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$aefdoc_num_importe: '|| idx || ' - ' || par$aefdoc_num_importe(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$aefdoc_nel_cantidad.first ..  par$aefdoc_nel_cantidad.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$aefdoc_nel_cantidad: '|| idx || ' - ' || par$aefdoc_nel_cantidad(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$ampdoc_cod_divisa.first ..  par$ampdoc_cod_divisa.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$ampdoc_cod_divisa: '|| idx || ' - ' || par$ampdoc_cod_divisa(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$ampdoc_cod_medio_pago.first ..  par$ampdoc_cod_medio_pago.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$ampdoc_cod_medio_pago: '|| idx || ' - ' || par$ampdoc_cod_medio_pago(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$ampdoc_num_importe.first ..  par$ampdoc_num_importe.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$ampdoc_num_importe: '|| idx || ' - ' || par$ampdoc_num_importe(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$ampdoc_nel_cantidad.first ..  par$ampdoc_nel_cantidad.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$ampdoc_nel_cantidad: '|| idx || ' - ' || par$ampdoc_nel_cantidad(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$avtdoc_cod_termino.first ..  par$avtdoc_cod_termino.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$avtdoc_cod_termino: '|| idx || ' - ' || par$avtdoc_cod_termino(idx),
                              par$cod_identificador   => '');
      END LOOP;
      FOR idx IN  par$avtdoc_des_valor.first ..  par$avtdoc_des_valor.last LOOP 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'par$avtdoc_des_valor: '|| idx || ' - ' || par$avtdoc_des_valor(idx),
                              par$cod_identificador   => '');
      END LOOP;
    END IF;

    /* === Limpiar tabla temporaria === */
    delete sapr_gtt_toids;
    var$deletes   := var$deletes + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;
  
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$des_origen,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Comienza llamada a procedimiento svalidar_doc_fondos',
                          par$cod_identificador   => '');

    /* === Validaciones  === */
    svalidar_doc_fondos(par$cod_accion               => par$cod_accion,
                        par$adoc_cod_formulario      => par$adoc_cod_formulario,
                        par$adoc_cod_externo         => par$adoc_cod_externo,
                        par$adoc_fyh_gestion         => par$adoc_fyh_gestion,
                        par$adoc_ori_cod_client      => par$adoc_ori_cod_client,
                        par$adoc_ori_cod_subcli      => par$adoc_ori_cod_subcli,
                        par$adoc_ori_cod_ptserv      => par$adoc_ori_cod_ptserv,
                        par$adoc_ori_cod_canal       => par$adoc_ori_cod_canal,
                        par$adoc_ori_cod_subcan      => par$adoc_ori_cod_subcan,
                        par$adoc_ori_cod_delega      => par$adoc_ori_cod_delega,
                        par$adoc_ori_cod_planta      => par$adoc_ori_cod_planta,
                        par$adoc_ori_cod_sector      => par$adoc_ori_cod_sector,
                        par$adoc_des_cod_client      => par$adoc_des_cod_client,
                        par$adoc_des_cod_subcli      => par$adoc_des_cod_subcli,
                        par$adoc_des_cod_ptserv      => par$adoc_des_cod_ptserv,
                        par$adoc_des_cod_canal       => par$adoc_des_cod_canal,
                        par$adoc_des_cod_subcan      => par$adoc_des_cod_subcan,
                        par$adoc_des_cod_delega      => par$adoc_des_cod_delega,
                        par$adoc_des_cod_planta      => par$adoc_des_cod_planta,
                        par$adoc_des_cod_sector      => par$adoc_des_cod_sector,
                        par$aefdoc_cod_divisa        => par$aefdoc_cod_divisa,
                        par$aefdoc_num_importe_total => par$aefdoc_num_importe_total,
                        par$aefdoc_cod_denominacion  => par$aefdoc_cod_denominacion,
                        par$aefdoc_cod_div_den       => par$aefdoc_cod_div_den,
                        par$aefdoc_num_importe       => par$aefdoc_num_importe,
                        par$aefdoc_nel_cantidad      => par$aefdoc_nel_cantidad,
                        par$ampdoc_cod_divisa        => par$ampdoc_cod_divisa,
                        par$ampdoc_cod_medio_pago    => par$ampdoc_cod_medio_pago,
                        par$ampdoc_num_importe       => par$ampdoc_num_importe,
                        par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,
                        par$avtdoc_cod_termino       => par$avtdoc_cod_termino,
                        par$avtdoc_des_valor         => par$avtdoc_des_valor,
                        par$bol_validar_cod_externo  => par$bol_validar_cod_externo,
                        par$cod_cultura              => par$cod_cultura,
                        par$validaciones             => var$validaciones_int);
  
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Finzaliza llamada a procedimiento svalidar_doc_fondos',
                            par$cod_identificador   => '');

    IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones_int) THEN
      FOR idx IN var$validaciones_int.first .. var$validaciones_int.last LOOP
        var$validaciones(var$validaciones.COUNT) := var$validaciones_int(idx);
      END LOOP;
    END IF;
  
    IF var$validaciones IS NULL OR
       (var$validaciones IS NOT NULL AND var$validaciones.COUNT = 0) THEN
    
      /* ===  Crear documentos === */
    
      /* recuperar formulario */
      BEGIN
        SELECT F.OID_FORMULARIO
          INTO var$oid_formulario
          FROM SAPR_TFORMULARIO F
         WHERE F.COD_FORMULARIO = par$adoc_cod_formulario;
      EXCEPTION
        WHEN no_data_found THEN
          var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          'GENESIS_PRODUCTO_VAL108',
                                                                                          'validaciones_genesis_producto',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                          NULL,
                                                                                          1);
      END;
    
      /* Datos documento */
      var$oid_documento := SYS_GUID();
      var$adocs_oid(var$adocs_oid.COUNT) := var$oid_documento;
      var$adocs_oid_doc_padre(var$adocs_oid_doc_padre.COUNT) := NULL;
      var$adocs_oid_sustituto(var$adocs_oid_sustituto.COUNT) := NULL;
      var$adocs_oid_mov_fondos(var$adocs_oid_mov_fondos.COUNT) := NULL;
      var$adocs_cod_externo(var$adocs_cod_externo.COUNT) := par$adoc_cod_externo;
      var$adocs_rowver(var$adocs_rowver.COUNT) := NULL;
    
      /* Cuenta origen */
      scargar_cuenta_fondos(par$cod_ajeno    => par$cod_ajeno,
                            par$cod_tipo     => gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                    'CUENTA_ORIGEN',
                                                                                    'validaciones_genesis_producto',
                                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                    NULL,
                                                                                    0),
                            par$cod_client   => par$adoc_ori_cod_client,
                            par$cod_subcli   => par$adoc_ori_cod_subcli,
                            par$cod_ptserv   => par$adoc_ori_cod_ptserv,
                            par$cod_canal    => par$adoc_ori_cod_canal,
                            par$cod_subcan   => par$adoc_ori_cod_subcan,
                            par$cod_delega   => par$adoc_ori_cod_delega,
                            par$cod_planta   => par$adoc_ori_cod_planta,
                            par$cod_sector   => par$adoc_ori_cod_sector,
                            par$oid_client   => var$adocs_mov_ori_oid_client(var$adocs_mov_ori_oid_client.COUNT),
                            par$oid_subcli   => var$adocs_mov_ori_oid_subcli(var$adocs_mov_ori_oid_subcli.COUNT),
                            par$oid_ptserv   => var$adocs_mov_ori_oid_ptserv(var$adocs_mov_ori_oid_ptserv.COUNT),
                            par$oid_canal    => var$adocs_mov_ori_oid_canal(var$adocs_mov_ori_oid_canal.COUNT),
                            par$oid_subcan   => var$adocs_mov_ori_oid_subcan(var$adocs_mov_ori_oid_subcan.COUNT),
                            par$oid_delega   => var$adocs_mov_ori_oid_delega(var$adocs_mov_ori_oid_delega.COUNT),
                            par$oid_planta   => var$adocs_mov_ori_oid_planta(var$adocs_mov_ori_oid_planta.COUNT),
                            par$oid_sector   => var$adocs_mov_ori_oid_sector(var$adocs_mov_ori_oid_sector.COUNT),
                            par$cod_cultura  => par$cod_cultura,
                            par$validaciones => var$validaciones_int);
      var$adocs_mov_ori_oid_cuenta(var$adocs_mov_ori_oid_cuenta.COUNT) := NULL;
      var$adocs_mov_ori_cod_client(var$adocs_mov_ori_cod_client.COUNT) := NULL;
      var$adocs_mov_ori_cod_subcli(var$adocs_mov_ori_cod_subcli.COUNT) := NULL;
      var$adocs_mov_ori_cod_ptserv(var$adocs_mov_ori_cod_ptserv.COUNT) := NULL;
      var$adocs_mov_ori_cod_canal(var$adocs_mov_ori_cod_canal.COUNT) := NULL;
      var$adocs_mov_ori_cod_subcan(var$adocs_mov_ori_cod_subcan.COUNT) := NULL;
      var$adocs_mov_ori_cod_delega(var$adocs_mov_ori_cod_delega.COUNT) := NULL;
      var$adocs_mov_ori_cod_planta(var$adocs_mov_ori_cod_planta.COUNT) := NULL;
      var$adocs_mov_ori_cod_sector(var$adocs_mov_ori_cod_sector.COUNT) := NULL;
      var$adocs_sal_ori_oid_cuenta(var$adocs_sal_ori_oid_cuenta.COUNT) := NULL;
      var$adocs_sal_ori_oid_client(var$adocs_sal_ori_oid_client.COUNT) := NULL;
      var$adocs_sal_ori_cod_client(var$adocs_sal_ori_cod_client.COUNT) := NULL;
      var$adocs_sal_ori_oid_subcli(var$adocs_sal_ori_oid_subcli.COUNT) := NULL;
      var$adocs_sal_ori_cod_subcli(var$adocs_sal_ori_cod_subcli.COUNT) := NULL;
      var$adocs_sal_ori_oid_ptserv(var$adocs_sal_ori_oid_ptserv.COUNT) := NULL;
      var$adocs_sal_ori_cod_ptserv(var$adocs_sal_ori_cod_ptserv.COUNT) := NULL;
    
      IF NOT
          gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones_int) THEN
        FOR idx IN var$validaciones_int.first .. var$validaciones_int.last LOOP
          var$validaciones(var$validaciones.COUNT) := var$validaciones_int(idx);
        END LOOP;
      END IF;
    
      /* Cuenta destino */
      scargar_cuenta_fondos(par$cod_ajeno    => par$cod_ajeno,
                            par$cod_tipo     => gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                    'CUENTA_DESTINO',
                                                                                    'validaciones_genesis_producto',
                                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                    NULL,
                                                                                    0),
                            par$cod_client   => par$adoc_des_cod_client,
                            par$cod_subcli   => par$adoc_des_cod_subcli,
                            par$cod_ptserv   => par$adoc_des_cod_ptserv,
                            par$cod_canal    => par$adoc_des_cod_canal,
                            par$cod_subcan   => par$adoc_des_cod_subcan,
                            par$cod_delega   => par$adoc_des_cod_delega,
                            par$cod_planta   => par$adoc_des_cod_planta,
                            par$cod_sector   => par$adoc_des_cod_sector,
                            par$oid_client   => var$adocs_mov_des_oid_client(var$adocs_mov_des_oid_client.COUNT),
                            par$oid_subcli   => var$adocs_mov_des_oid_subcli(var$adocs_mov_des_oid_subcli.COUNT),
                            par$oid_ptserv   => var$adocs_mov_des_oid_ptserv(var$adocs_mov_des_oid_ptserv.COUNT),
                            par$oid_canal    => var$adocs_mov_des_oid_canal(var$adocs_mov_des_oid_canal.COUNT),
                            par$oid_subcan   => var$adocs_mov_des_oid_subcan(var$adocs_mov_des_oid_subcan.COUNT),
                            par$oid_delega   => var$adocs_mov_des_oid_delega(var$adocs_mov_des_oid_delega.COUNT),
                            par$oid_planta   => var$adocs_mov_des_oid_planta(var$adocs_mov_des_oid_planta.COUNT),
                            par$oid_sector   => var$adocs_mov_des_oid_sector(var$adocs_mov_des_oid_sector.COUNT),
                            par$cod_cultura  => par$cod_cultura,
                            par$validaciones => var$validaciones_int);
      var$adocs_mov_des_oid_cuenta(var$adocs_mov_des_oid_cuenta.COUNT) := NULL;
      var$adocs_mov_des_cod_client(var$adocs_mov_des_cod_client.COUNT) := NULL;
      var$adocs_mov_des_cod_subcli(var$adocs_mov_des_cod_subcli.COUNT) := NULL;
      var$adocs_mov_des_cod_ptserv(var$adocs_mov_des_cod_ptserv.COUNT) := NULL;
      var$adocs_mov_des_cod_canal(var$adocs_mov_des_cod_canal.COUNT) := NULL;
      var$adocs_mov_des_cod_subcan(var$adocs_mov_des_cod_subcan.COUNT) := NULL;
      var$adocs_mov_des_cod_delega(var$adocs_mov_des_cod_delega.COUNT) := NULL;
      var$adocs_mov_des_cod_planta(var$adocs_mov_des_cod_planta.COUNT) := NULL;
      var$adocs_mov_des_cod_sector(var$adocs_mov_des_cod_sector.COUNT) := NULL;
      var$adocs_sal_des_oid_cuenta(var$adocs_sal_des_oid_cuenta.COUNT) := NULL;
      var$adocs_sal_des_oid_client(var$adocs_sal_des_oid_client.COUNT) := NULL;
      var$adocs_sal_des_cod_client(var$adocs_sal_des_cod_client.COUNT) := NULL;
      var$adocs_sal_des_oid_subcli(var$adocs_sal_des_oid_subcli.COUNT) := NULL;
      var$adocs_sal_des_cod_subcli(var$adocs_sal_des_cod_subcli.COUNT) := NULL;
      var$adocs_sal_des_oid_ptserv(var$adocs_sal_des_oid_ptserv.COUNT) := NULL;
      var$adocs_sal_des_cod_ptserv(var$adocs_sal_des_cod_ptserv.COUNT) := NULL;
    
      IF NOT
          gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones_int) THEN
        FOR idx IN var$validaciones_int.first .. var$validaciones_int.last LOOP
          var$validaciones(var$validaciones.COUNT) := var$validaciones_int(idx);
        END LOOP;
      END IF;
    
      /* Valores efectivos */
      IF NOT
          gepr_putilidades_###VERSION###.farray_vacio_cod(par$aefdoc_cod_divisa) THEN
        FOR idx IN par$aefdoc_cod_divisa.first .. par$aefdoc_cod_divisa.last LOOP
        
          /* Valida y recuera informaciones de la divisa */
          BEGIN
            IF par$cod_ajeno IS NULL THEN
              SELECT D.OID_DIVISA
                INTO var$oid_divisa
                FROM GEPR_TDIVISA D
               WHERE D.COD_ISO_DIVISA = par$aefdoc_cod_divisa(idx);
            ELSE
              SELECT CA.OID_TABLA_GENESIS
                INTO var$oid_divisa
                FROM GEPR_TCODIGO_AJENO CA
               WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
                 AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'
                 AND CA.COD_AJENO = par$aefdoc_cod_divisa(idx);
            END IF;
          
          EXCEPTION
            WHEN no_data_found THEN
              var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'GENESIS_PRODUCTO_VAL105',
                                                                                              'validaciones_genesis_producto',
                                                                                              gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                              NULL,
                                                                                              0);
          END;
        
          /* Valida y anadir las informaciones del importe total EFECTIVO */
          IF par$aefdoc_num_importe_total(idx) IS NOT NULL THEN
            var$aefdoc_oid_documento(var$aefdoc_oid_documento.COUNT) := var$oid_documento;
            var$aefdoc_oid_divisa(var$aefdoc_oid_divisa.COUNT) := var$oid_divisa;
            var$aefdoc_oid_denominacion(var$aefdoc_oid_denominacion.COUNT) := NULL;
            var$aefdoc_oid_unid_medida(var$aefdoc_oid_unid_medida.COUNT) := NULL;
            var$aefdoc_cod_niv_detalle(var$aefdoc_cod_niv_detalle.COUNT) := 'T';
            var$aefdoc_cod_tp_efec_tot(var$aefdoc_cod_tp_efec_tot.COUNT) := 'A';
            var$aefdoc_oid_calidad(var$aefdoc_oid_calidad.COUNT) := NULL;
            var$aefdoc_num_importe(var$aefdoc_num_importe.COUNT) := par$aefdoc_num_importe_total(idx);
            var$aefdoc_nel_cantidad(var$aefdoc_nel_cantidad.COUNT) := NULL;
          END IF;
        
          /* Valida y anadir las informaciones del importe detallado */
          IF NOT
              gepr_putilidades_###VERSION###.farray_vacio_cod(par$aefdoc_cod_denominacion) THEN
            FOR idxDen IN par$aefdoc_cod_denominacion.first .. par$aefdoc_cod_denominacion.last LOOP
            
              IF par$aefdoc_cod_div_den(idxDen) =
                 par$aefdoc_cod_divisa(idx) THEN
              
                /* Valida y recuera informaciones de la denominacion */
                BEGIN
                
                  IF par$cod_ajeno IS NULL THEN
                    SELECT DEN.OID_DIVISA,
                           DEN.OID_DENOMINACION,
                           CASE
                             WHEN DEN.BOL_BILLETE = 1 THEN
                              'B'
                             ELSE
                              'M'
                           END COD_TIPO_EFECTIVO
                      INTO var$aefdoc_oid_divisa(var$aefdoc_oid_divisa.COUNT),
                           var$aefdoc_oid_denominacion(var$aefdoc_oid_denominacion.COUNT),
                           var$aefdoc_cod_tp_efec_tot(var$aefdoc_cod_tp_efec_tot.COUNT)
                      FROM GEPR_TDENOMINACION DEN
                     WHERE DEN.OID_DIVISA = var$oid_divisa
                       AND DEN.COD_DENOMINACION =
                           par$aefdoc_cod_denominacion(idxDen);
                  ELSE
                    SELECT DEN.OID_DIVISA,
                           DEN.OID_DENOMINACION,
                           CASE
                             WHEN DEN.BOL_BILLETE = 1 THEN
                              'B'
                             ELSE
                              'M'
                           END COD_TIPO_EFECTIVO
                      INTO var$aefdoc_oid_divisa(var$aefdoc_oid_divisa.COUNT),
                           var$aefdoc_oid_denominacion(var$aefdoc_oid_denominacion.COUNT),
                           var$aefdoc_cod_tp_efec_tot(var$aefdoc_cod_tp_efec_tot.COUNT)
                      FROM GEPR_TDENOMINACION DEN
                     INNER JOIN GEPR_TCODIGO_AJENO CA
                        ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION'
                       AND CA.OID_TABLA_GENESIS = DEN.OID_DENOMINACION
                     WHERE DEN.OID_DIVISA = var$oid_divisa
                       AND CA.COD_AJENO =
                           par$aefdoc_cod_denominacion(idxDen)
                       AND CA.COD_IDENTIFICADOR = par$cod_ajeno;
                  END IF;
                
                  var$aefdoc_oid_documento(var$aefdoc_oid_documento.COUNT) := var$oid_documento;
                  var$aefdoc_oid_unid_medida(var$aefdoc_oid_unid_medida.COUNT) := NULL;
                  var$aefdoc_cod_niv_detalle(var$aefdoc_cod_niv_detalle.COUNT) := 'D';
                  var$aefdoc_oid_calidad(var$aefdoc_oid_calidad.COUNT) := NULL;
                  var$aefdoc_num_importe(var$aefdoc_num_importe.COUNT) := par$aefdoc_num_importe(idxDen);
                  var$aefdoc_nel_cantidad(var$aefdoc_nel_cantidad.COUNT) := par$aefdoc_nel_cantidad(idxDen);
                
                EXCEPTION
                  WHEN no_data_found THEN
                    var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                                    'GENESIS_PRODUCTO_VAL106',
                                                                                                    'validaciones_genesis_producto',
                                                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                                    NULL,
                                                                                                    0);
                END;
              END IF;
            
            END LOOP;
          END IF;
        
        END LOOP;
      END IF;
    
      /* Valores medio pago */
      IF NOT
          gepr_putilidades_###VERSION###.farray_vacio_cod(par$ampdoc_cod_divisa) THEN
        FOR idx IN par$ampdoc_cod_divisa.first .. par$ampdoc_cod_divisa.last LOOP
          BEGIN
          
            IF par$cod_ajeno IS NULL THEN
            
              SELECT D.OID_DIVISA
                INTO var$oid_divisa
                FROM GEPR_TDIVISA D
               WHERE D.COD_ISO_DIVISA = par$aefdoc_cod_divisa(idx);
            
              SELECT MP.OID_DIVISA,
                     MP.OID_MEDIO_PAGO,
                     MP.Cod_Tipo_Medio_Pago
                INTO var$ampdoc_oid_divisa(var$ampdoc_oid_divisa.COUNT),
                     var$ampdoc_oid_medio_pago(var$ampdoc_oid_medio_pago.COUNT),
                     var$ampdoc_cod_tipo_med_pago(var$ampdoc_cod_tipo_med_pago.COUNT)
                FROM GEPR_TMEDIO_PAGO MP
               WHERE MP.OID_DIVISA = var$oid_divisa
                 AND MP.COD_MEDIO_PAGO = par$ampdoc_cod_medio_pago(idx);
            
            ELSE
            
              SELECT CA.OID_TABLA_GENESIS
                INTO var$oid_divisa
                FROM GEPR_TCODIGO_AJENO CA
               WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
                 AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'
                 AND CA.COD_AJENO = par$aefdoc_cod_divisa(idx);
            
              SELECT MP.OID_DIVISA,
                     MP.OID_MEDIO_PAGO,
                     MP.Cod_Tipo_Medio_Pago
                INTO var$ampdoc_oid_divisa(var$ampdoc_oid_divisa.COUNT),
                     var$ampdoc_oid_medio_pago(var$ampdoc_oid_medio_pago.COUNT),
                     var$ampdoc_cod_tipo_med_pago(var$ampdoc_cod_tipo_med_pago.COUNT)
                FROM GEPR_TMEDIO_PAGO MP
               INNER JOIN GEPR_TCODIGO_AJENO CA
                  ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TMEDIO_PAGO'
                 AND CA.OID_TABLA_GENESIS = MP.OID_MEDIO_PAGO
               WHERE CA.COD_AJENO = par$ampdoc_cod_medio_pago(idx)
                 AND MP.OID_DIVISA = var$oid_divisa
                 AND CA.COD_IDENTIFICADOR = par$cod_ajeno;
            END IF;
          
            var$ampdoc_cod_nivel_detalle(var$ampdoc_cod_nivel_detalle.COUNT) := 'D';
            var$ampdoc_oid_documento(var$ampdoc_oid_documento.COUNT) := var$oid_documento;
            var$ampdoc_num_importe(var$ampdoc_num_importe.COUNT) := par$ampdoc_num_importe(idx);
            var$ampdoc_nel_cantidad(var$ampdoc_nel_cantidad.COUNT) := par$ampdoc_nel_cantidad(idx);
          
          EXCEPTION
            WHEN no_data_found THEN
              var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'GENESIS_PRODUCTO_VAL107',
                                                                                              'validaciones_genesis_producto',
                                                                                              gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                              NULL,
                                                                                              0);
          END;
        
        END LOOP;
      END IF;
    
      /* Terminos */
      scargar_terminos_fondos(par$avtdoc_cod_termino   => par$avtdoc_cod_termino,
                              par$avtdoc_des_valor_ori => par$avtdoc_des_valor,
                              par$avtdoc_oid_documento => var$avtdoc_oid_documento,
                              par$avtdoc_oid_termino   => var$avtdoc_oid_termino,
                              par$avtdoc_des_valor     => var$avtdoc_des_valor,
                              par$oid_formulario       => var$oid_formulario,
                              par$oid_documento        => var$oid_documento,
                              par$cod_cultura          => par$cod_cultura,
                              par$validaciones         => var$validaciones_int);
    
      IF NOT
          gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones_int) THEN
        FOR idx IN var$validaciones_int.first .. var$validaciones_int.last LOOP
          var$validaciones(var$validaciones.COUNT) := var$validaciones_int(idx);
        END LOOP;
      END IF;
    
      /* Validar si es neceario confirmar el documento */
      IF par$cod_accion = 'Confirmado' OR par$cod_accion = 'Aceptado' THEN
        var$confirmar_doc := 1;
      END IF;
    
      /* Verifica si hasta el momento hay algun error */
      IF var$validaciones IS NULL OR
         (var$validaciones IS NOT NULL AND var$validaciones.COUNT = 0) THEN
      
        /*  Fechas  */
        var$adocs_fyh_gestion(var$adocs_fyh_gestion.COUNT) := par$adoc_fyh_gestion;
        var$adocs_fyh_plncertif(var$adocs_fyh_plncertif.COUNT) := par$adoc_fyh_gestion;
        
        var$adocs_fyh_contable(var$adocs_fyh_contable.COUNT) := par$adoc_fyh_contable;
      var$adocs_cod_collection_id(var$adocs_cod_collection_id.COUNT) := par$adoc_cod_collection_id;
      var$adocs_cod_actual_id(var$adocs_cod_actual_id.COUNT) := par$adoc_cod_actual_id;
        /* Grabar el documento */
        sguardar_doc_valores(par$oid_formulario            => var$oid_formulario,
                             par$oid_grupo_documento       => null,
                             par$adocs_oid                 => var$adocs_oid,
                             par$adocs_oid_doc_padre       => var$adocs_oid_doc_padre,
                             par$adocs_oid_sustituto       => var$adocs_oid_sustituto,
                             par$adocs_oid_mov_fondos      => var$adocs_oid_mov_fondos,
                             par$adocs_fyh_plncertif       => var$adocs_fyh_plncertif,
                             par$adocs_fyh_gestion         => var$adocs_fyh_gestion,
                             par$adocs_fyh_contable        => var$adocs_fyh_contable,                             
                             par$adocs_cod_actual_id       => var$adocs_cod_actual_id,
                             par$adocs_collection_id       => var$adocs_cod_collection_id,
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
                             par$adocs_sal_ori_oid_cuenta  => var$adocs_sal_ori_oid_cuenta,
                             par$adocs_sal_ori_oid_client  => var$adocs_sal_ori_oid_client,
                             par$adocs_sal_ori_cod_client  => var$adocs_sal_ori_cod_client,
                             par$adocs_sal_ori_oid_subcli  => var$adocs_sal_ori_oid_subcli,
                             par$adocs_sal_ori_cod_subcli  => var$adocs_sal_ori_cod_subcli,
                             par$adocs_sal_ori_oid_ptserv  => var$adocs_sal_ori_oid_ptserv,
                             par$adocs_sal_ori_cod_ptserv  => var$adocs_sal_ori_cod_ptserv,
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
                             par$adocs_sal_des_oid_cuenta  => var$adocs_sal_des_oid_cuenta,
                             par$adocs_sal_des_oid_client  => var$adocs_sal_des_oid_client,
                             par$adocs_sal_des_cod_client  => var$adocs_sal_des_cod_client,
                             par$adocs_sal_des_oid_subcli  => var$adocs_sal_des_oid_subcli,
                             par$adocs_sal_des_cod_subcli  => var$adocs_sal_des_cod_subcli,
                             par$adocs_sal_des_oid_ptserv  => var$adocs_sal_des_oid_ptserv,
                             par$adocs_sal_des_cod_ptserv  => var$adocs_sal_des_cod_ptserv,
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
                             par$aefadoc_oid_documento     => var$aefadoc_oid_documento,
                             par$aefadoc_oid_divisa        => var$aefadoc_oid_divisa,
                             par$aefadoc_oid_denominacion  => var$aefadoc_oid_denominacion,
                             par$aefadoc_oid_unid_medida   => var$aefadoc_oid_unid_medida,
                             par$aefadoc_cod_niv_detalle   => var$aefadoc_cod_niv_detalle,
                             par$aefadoc_cod_tp_efec_tot   => var$aefadoc_cod_tp_efec_tot,
                             par$aefadoc_oid_calidad       => var$aefadoc_oid_calidad,
                             par$aefadoc_num_importe       => var$aefadoc_num_importe,
                             par$aefadoc_nel_cantidad      => var$aefadoc_nel_cantidad,
                             par$ampadoc_oid_documento     => var$ampadoc_oid_documento,
                             par$ampadoc_oid_divisa        => var$ampadoc_oid_divisa,
                             par$ampadoc_oid_medio_pago    => var$ampadoc_oid_medio_pago,
                             par$ampadoc_cod_tipo_med_pago => var$ampadoc_cod_tipo_med_pago,
                             par$ampadoc_cod_nivel_detalle => var$ampadoc_cod_nivel_detalle,
                             par$ampadoc_num_importe       => var$ampadoc_num_importe,
                             par$ampadoc_nel_cantidad      => var$ampadoc_nel_cantidad,
                             par$avtmpadoc_oid_documento   => var$avtmpadoc_oid_documento,
                             par$avtmpadoc_oid_t_mediopago => var$avtmpadoc_oid_t_mediopago,
                             par$avtmpadoc_des_valor       => var$avtmpadoc_des_valor,
                             par$avtmpadoc_nec_indice_grp  => var$avtmpadoc_nec_indice_grp,
                             par$avtdoc_oid_documento      => var$avtdoc_oid_documento,
                             par$avtdoc_oid_termino        => var$avtdoc_oid_termino,
                             par$avtdoc_des_valor          => var$avtdoc_des_valor,
                             par$usuario                   => par$cod_usuario,
                             par$cod_cultura               => par$cod_cultura,
                             par$info_ejecucion            => par$info_ejecucion,
                             par$hacer_commit              => 0,
                             par$confirmar_doc             => var$confirmar_doc,
                             par$doc_rc_documentos         => var$doc_rc_documentos,
                             par$doc_rc_historico_mov_doc  => var$doc_rc_historico_mov_doc,
                             par$inserts                   => var$inserts_int,
                             par$updates                   => var$updates_int,
                             par$deletes                   => var$deletes_int,
                             par$selects                   => var$selects_int,
                             par$cod_ejecucion             => var$cod_ejecucion);
      
        var$registros := var$registros + var$inserts_int + var$updates_int +
                         var$deletes_int + var$selects_int;
        var$inserts   := var$inserts + var$inserts_int;
        var$updates   := var$updates + var$updates_int;
        var$deletes   := var$deletes + var$deletes_int;
        var$selects   := var$selects + var$selects_int;
      
        SELECT D.COD_ESTADO
          INTO var$cod_estado_documento
          FROM SAPR_TDOCUMENTO D
         WHERE D.OID_DOCUMENTO = var$oid_documento;
      
        /* === Forzar aceptar documento === */
        IF par$cod_accion = 'Aceptado' AND
           var$cod_estado_documento <>
           gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado THEN
        
          BEGIN
            SELECT D.ROWVER
              INTO var$rowver
              FROM SAPR_TDOCUMENTO D
             WHERE D.OID_DOCUMENTO = var$oid_documento;
          
            var$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado;
            sapr_ptransiciones_###VERSION###.sejecutar_doc(par$oid_llamada          => par$oid_llamada,
                                                      par$oid_documento        => var$oid_documento,
                                                      par$cod_estado_documento => var$cod_estado_documento,
                                                      par$cod_comprobante      => var$cod_comprobante,
                                                      par$rowver               => var$rowver,
                                                      par$usuario              => par$cod_usuario,
                                                      par$cod_cultura          => par$cod_cultura,
                                                      par$info_ejecucion       => par$info_ejecucion,
                                                      par$hacer_commit         => 0,
                                                      par$inserts              => var$inserts_int,
                                                      par$updates              => var$updates_int,
                                                      par$deletes              => var$deletes_int,
                                                      par$selects              => var$selects_int,
                                                      par$cod_ejecucion        => var$cod_ejecucion);
            var$inserts   := var$inserts + var$inserts_int;
            var$updates   := var$updates + var$updates_int;
            var$deletes   := var$deletes + var$deletes_int;
            var$selects   := var$selects + var$selects_int;
            var$registros := var$registros + var$selects_int +
                             var$inserts_int + var$deletes_int +
                             var$updates_int;
          EXCEPTION
            WHEN no_data_found THEN
              var$validaciones(var$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'GENESIS_PRODUCTO_VAL125',
                                                                                              'validaciones_genesis_producto',
                                                                                              gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                              NULL,
                                                                                              0);
          END;
        
        END IF;
      
        /* ===  Commit === */
        IF var$validaciones.COUNT = 0 THEN
          IF par$hacer_commit = 1 THEN
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
          END IF;
        ELSE
          ROLLBACK;
        END IF;
      END IF;
    END IF;
  
    /* Validaciones */
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN
    
      var$tvalidaciones := gepr_putilidades_###VERSION###.fconverter_array_desc_table(par$table_varchar2 => var$validaciones);
    
      open par$validaciones for
        SELECT SUBSTR(C.COLUMN_VALUE, 0, 6) CODIGO,
               SUBSTR(C.COLUMN_VALUE, 7) DESCRICION
          FROM TABLE(var$tvalidaciones) C
         WHERE C.COLUMN_VALUE IS NOT NULL;
    

        IF var$validaciones IS NOT NULL AND var$validaciones.COUNT > 0 THEN
            FOR idx IN  var$validaciones.first ..  var$validaciones.last LOOP 
               SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => idx || ' - ' || var$validaciones(idx),
                            par$cod_identificador   => '');
            END LOOP;
        END IF;

    END IF;
  
    /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
    
      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$des_origen,
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
                              'Error: ' || const$des_origen,
                              true);
    
  END sguardar_doc_fondos;

  /* Validar documento de fondos - MAE */
  PROCEDURE svalidar_doc_fondos(par$cod_accion               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$adoc_ori_cod_client      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_canal       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_delega      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_ori_cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_client      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_canal       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_delega      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$adoc_des_cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$aefdoc_cod_divisa        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_num_importe_total IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$aefdoc_cod_denominacion  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_cod_div_den       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$aefdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$aefdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$ampdoc_cod_divisa        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$ampdoc_cod_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ampdoc_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                par$ampdoc_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                par$avtdoc_cod_termino       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                par$avtdoc_des_valor         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                par$bol_validar_cod_externo  IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$validaciones             OUT gepr_pcomon_###VERSION###.tipo$descs_) IS
  
    var$existe gepr_pcomon_###VERSION###.tipo$nel_;
  
  BEGIN
  
    /* Codigo Externo */
    IF par$adoc_cod_externo IS NULL THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL104',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'codigoExterno|' ||
                                                                                      par$adoc_cod_externo,
                                                                                      0);
    
    ELSIF par$bol_validar_cod_externo = 1 THEN
    
      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_TDOCUMENTO D
       WHERE D.COD_EXTERNO = par$adoc_cod_externo;
    
      IF var$existe > 0 THEN
        par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'GENESIS_PRODUCTO_VAL121',
                                                                                        'validaciones_genesis_producto',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                        NULL,
                                                                                        0);
      END IF;
    
    END IF;
  
    /* Accion */
    IF par$cod_accion IS NULL THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL104',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'accion|' ||
                                                                                      par$cod_accion,
                                                                                      0);
    
    END IF;
  
    /* Formulario */
    IF par$adoc_cod_formulario IS NULL THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL104',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'codigoFormulario|' ||
                                                                                      par$adoc_cod_formulario,
                                                                                      0);
    
    END IF;
  
    /* Fecha gestion */
    IF par$adoc_fyh_gestion IS NULL THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL104',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'fechaHoraGestionFondos|' ||
                                                                                      par$adoc_fyh_gestion,
                                                                                      0);
    
    END IF;
  
    /* consistencia de tamano de los arrays de cuenta por documento */
    IF par$adoc_ori_cod_client IS NULL OR par$adoc_ori_cod_canal IS NULL OR
       par$adoc_ori_cod_subcan IS NULL OR par$adoc_ori_cod_delega IS NULL OR
       par$adoc_ori_cod_planta IS NULL OR par$adoc_ori_cod_sector IS NULL OR
       (par$adoc_ori_cod_subcli IS NULL AND
       par$adoc_ori_cod_ptserv IS NOT NULL) THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL122',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'Cuenta Origen',
                                                                                      0);
    
    END IF;
  
    /* consistencia de tamano de los arrays de cuenta por documento */
    IF par$adoc_des_cod_client IS NULL OR par$adoc_des_cod_canal IS NULL OR
       par$adoc_des_cod_subcan IS NULL OR par$adoc_des_cod_delega IS NULL OR
       par$adoc_des_cod_planta IS NULL OR par$adoc_des_cod_sector IS NULL OR
       (par$adoc_des_cod_subcli IS NULL AND
       par$adoc_des_cod_ptserv IS NOT NULL) THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL122',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'Cuenta Destino',
                                                                                      0);
    END IF;
  
    /* consistencia de tamano de los arrays de efectivo por documento */
    IF par$aefdoc_cod_divisa.count <> par$aefdoc_num_importe_total.count THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL122',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'Divisas',
                                                                                      0);
    
    END IF;
  
    /* consistencia de tamano de los arrays de efectivo por documento */
    IF par$aefdoc_cod_denominacion.count <> par$aefdoc_cod_div_den.count OR
       par$aefdoc_cod_denominacion.count <> par$aefdoc_num_importe.count OR
       par$aefdoc_cod_denominacion.count <> par$aefdoc_nel_cantidad.count THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL122',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'Denominaciones',
                                                                                      0);
    
    END IF;
  
    /* consistencia de tamano de los arrays de medio de pago por documento */
    IF par$ampdoc_cod_divisa.count <> par$ampdoc_cod_medio_pago.count OR
       par$ampdoc_cod_divisa.count <> par$ampdoc_num_importe.count OR
       par$ampdoc_cod_divisa.count <> par$ampdoc_nel_cantidad.count THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL122',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'Valores Medio Pago',
                                                                                      0);
    
    END IF;
  
    /* consistencia de tamano de los arrays de terminos por documento */
    IF par$avtdoc_cod_termino.count <> par$avtdoc_des_valor.count THEN
    
      par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                      'GENESIS_PRODUCTO_VAL122',
                                                                                      'validaciones_genesis_producto',
                                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                      'Terminos del Documento',
                                                                                      0);
    
    END IF;
  
  END svalidar_doc_fondos;

  /* Cargar valores cuenta - MAE */
  PROCEDURE scargar_cuenta_fondos(par$cod_ajeno    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_tipo     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_client   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_subcli   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_ptserv   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_canal    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_subcan   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_delega   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_planta   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$cod_sector   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$oid_client   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_subcli   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_ptserv   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_canal    OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_subcan   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_delega   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_planta   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_sector   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_cultura  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$validaciones OUT gepr_pcomon_###VERSION###.tipo$descs_) IS
  
    var$existe gepr_pcomon_###VERSION###.tipo$nel_;
  
  BEGIN
  
    /* Identificador cliente */
    BEGIN
      IF par$cod_ajeno IS NULL THEN
        SELECT C.OID_CLIENTE
          INTO par$oid_client
          FROM GEPR_TCLIENTE C
         WHERE C.COD_CLIENTE = par$cod_client;
      ELSE
        SELECT CA.OID_TABLA_GENESIS
          INTO par$oid_client
          FROM GEPR_TCODIGO_AJENO CA
         WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
           AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
           AND CA.COD_AJENO = par$cod_client;
      END IF;
    EXCEPTION
      WHEN no_data_found THEN
        par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'GENESIS_PRODUCTO_VAL126_CLIENTE_INVALIDA',
                                                                                        'validaciones_genesis_producto',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                        par$cod_tipo || '|' ||
                                                                                        par$cod_client,
                                                                                        0);
    END;
  
    /* Identificador subcliente */
    IF par$cod_subcli IS NOT NULL THEN
      BEGIN
        IF par$cod_ajeno IS NULL THEN
          SELECT SC.OID_SUBCLIENTE
            INTO par$oid_subcli
            FROM GEPR_TSUBCLIENTE SC
           INNER JOIN GEPR_TCLIENTE C
              ON C.OID_CLIENTE = SC.OID_CLIENTE
           WHERE C.COD_CLIENTE = par$cod_client
             AND SC.COD_SUBCLIENTE = par$cod_subcli;
        ELSE
          SELECT CA.OID_TABLA_GENESIS
            INTO par$oid_subcli
            FROM GEPR_TCODIGO_AJENO CA
           WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
             AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
             AND CA.COD_AJENO = par$cod_subcli;
        END IF;
      EXCEPTION
        WHEN no_data_found THEN
          par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          'GENESIS_PRODUCTO_VAL126_SUBCLIENTE_INVALIDA',
                                                                                          'validaciones_genesis_producto',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                          par$cod_tipo || '|' ||
                                                                                          par$cod_subcli,
                                                                                          0);
      END;
    END IF;
  
    /* Identificador punto servicio */
    IF par$cod_ptserv IS NOT NULL THEN
      BEGIN
        IF par$cod_ajeno IS NULL THEN
          SELECT PS.OID_PTO_SERVICIO
            INTO par$oid_ptserv
            FROM GEPR_TPUNTO_SERVICIO PS
           INNER JOIN GEPR_TSUBCLIENTE SC
              ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
           INNER JOIN GEPR_TCLIENTE C
              ON C.OID_CLIENTE = SC.OID_CLIENTE
           WHERE C.COD_CLIENTE = par$cod_client
             AND SC.COD_SUBCLIENTE = par$cod_subcli
             AND PS.COD_PTO_SERVICIO = par$cod_ptserv;
        ELSE
          SELECT CA.OID_TABLA_GENESIS
            INTO par$oid_ptserv
            FROM GEPR_TCODIGO_AJENO CA
           WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
             AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
             AND CA.COD_AJENO = par$cod_ptserv;
        END IF;
      EXCEPTION
        WHEN no_data_found THEN
          par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          'GENESIS_PRODUCTO_VAL126_PUNTO_SERVICIO_INVALIDA',
                                                                                          'validaciones_genesis_producto',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                          par$cod_tipo || '|' ||
                                                                                          par$cod_ptserv,
                                                                                          0);
      END;
    END IF;
  
    /* Identificador canal */
    BEGIN
      IF par$cod_ajeno IS NULL THEN
        SELECT C.OID_CANAL
          INTO par$oid_canal
          FROM GEPR_TCANAL C
         WHERE C.COD_CANAL = par$cod_canal;
      ELSE
        SELECT CA.OID_TABLA_GENESIS
          INTO par$oid_canal
          FROM GEPR_TCODIGO_AJENO CA
         WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
           AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'
           AND CA.COD_AJENO = par$cod_canal;
      END IF;
    EXCEPTION
      WHEN no_data_found THEN
        par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'GENESIS_PRODUCTO_VAL126_CANAL_INVALIDA',
                                                                                        'validaciones_genesis_producto',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                        par$cod_tipo || '|' ||
                                                                                        par$cod_canal,
                                                                                        0);
    END;
  
    /* Identificador subcanal */
    IF par$cod_subcan IS NOT NULL THEN
      BEGIN
        IF par$cod_ajeno IS NULL THEN
          SELECT SC.OID_SUBCANAL
            INTO par$oid_subcan
            FROM GEPR_TSUBCANAL SC
           INNER JOIN GEPR_TCANAL C
              ON C.OID_CANAL = SC.OID_CANAL
           WHERE C.COD_CANAL = par$cod_canal
             AND SC.COD_SUBCANAL = par$cod_subcan;
        ELSE
          SELECT CA.OID_TABLA_GENESIS
            INTO par$oid_subcan
            FROM GEPR_TCODIGO_AJENO CA
           WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
             AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'
             AND CA.COD_AJENO = par$cod_subcan;
        END IF;
      EXCEPTION
        WHEN no_data_found THEN
          par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          'GENESIS_PRODUCTO_VAL126_SUBCANAL_INVALIDA',
                                                                                          'validaciones_genesis_producto',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                          par$cod_tipo || '|' ||
                                                                                          par$cod_subcan,
                                                                                          0);
      END;
    END IF;
  
    /* Identificador delegacion */
    BEGIN
      IF par$cod_ajeno IS NULL THEN
        SELECT D.OID_DELEGACION
          INTO par$oid_delega
          FROM GEPR_TDELEGACION D
         WHERE D.COD_DELEGACION = par$cod_delega;
      ELSE
        SELECT CA.OID_TABLA_GENESIS
          INTO par$oid_delega
          FROM GEPR_TCODIGO_AJENO CA
         WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
           AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'
           AND CA.COD_AJENO = par$cod_delega;
      END IF;
    EXCEPTION
      WHEN no_data_found THEN
        par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'GENESIS_PRODUCTO_VAL126_DELEGACION_INVALIDA',
                                                                                        'validaciones_genesis_producto',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                        par$cod_tipo || '|' ||
                                                                                        par$cod_delega,
                                                                                        0);
    END;
  
    /* Identificador planta */
    IF par$cod_planta IS NOT NULL THEN
      BEGIN
        IF par$cod_ajeno IS NULL THEN
          SELECT P.OID_PLANTA
            INTO par$oid_planta
            FROM GEPR_TPLANTA P
           INNER JOIN GEPR_TDELEGACION D
              ON D.OID_DELEGACION = P.OID_DELEGACION
           WHERE D.COD_DELEGACION = par$cod_delega
             AND P.COD_PLANTA = par$cod_planta;
        ELSE
          SELECT CA.OID_TABLA_GENESIS
            INTO par$oid_planta
            FROM GEPR_TCODIGO_AJENO CA
           WHERE CA.COD_IDENTIFICADOR = par$cod_ajeno
             AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'
             AND CA.COD_AJENO = par$cod_planta;
        END IF;
      EXCEPTION
        WHEN no_data_found THEN
          par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          'GENESIS_PRODUCTO_VAL126_PLANTA_INVALIDA',
                                                                                          'validaciones_genesis_producto',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                          par$cod_tipo || '|' ||
                                                                                          par$cod_planta,
                                                                                          0);
      END;
    END IF;
  
    /* Identificador sector */
    IF par$cod_sector IS NOT NULL THEN
      BEGIN
        IF par$cod_ajeno IS NULL THEN
          SELECT S.OID_SECTOR
            INTO par$oid_sector
          FROM GEPR_TSECTOR S
            INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
            INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
            INNER JOIN SAPR_TMAQUINA MAQUI ON MAQUI.OID_SECTOR = S.OID_SECTOR AND 
                                              MAQUI.BOL_ACTIVO = 1
          WHERE D.COD_DELEGACION = par$cod_delega
            AND P.COD_PLANTA = par$cod_planta
            AND S.COD_SECTOR = par$cod_sector
            AND S.BOL_ACTIVO = 1;
        ELSE
          SELECT S.OID_SECTOR
            INTO par$oid_sector
          FROM GEPR_TSECTOR S 
            INNER JOIN GEPR_TCODIGO_AJENO CA_S ON CA_S.OID_TABLA_GENESIS =  S.OID_SECTOR AND 
                                                  CA_S.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR' AND 
                                                  CA_S.COD_IDENTIFICADOR = par$cod_ajeno AND 
                                                  CA_S.COD_AJENO = par$cod_sector
            INNER JOIN SAPR_TMAQUINA MAQUI ON MAQUI.OID_SECTOR = S.OID_SECTOR AND 
                                              MAQUI.BOL_ACTIVO = 1
            INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
            INNER JOIN GEPR_TCODIGO_AJENO CA_P ON CA_P.OID_TABLA_GENESIS =  P.OID_PLANTA AND 
                                                  CA_P.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA' AND 
                                                  CA_P.COD_IDENTIFICADOR = par$cod_ajeno AND 
                                                  CA_P.COD_AJENO = par$cod_planta
            INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
            INNER JOIN GEPR_TCODIGO_AJENO CA_D ON CA_D.OID_TABLA_GENESIS =  D.OID_DELEGACION AND 
                                                  CA_D.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION' AND 
                                                  CA_D.COD_IDENTIFICADOR = par$cod_ajeno AND 
                                                  CA_D.COD_AJENO = par$cod_delega
           WHERE S.BOL_ACTIVO = 1;
        END IF;
      EXCEPTION
        WHEN no_data_found THEN
        
          par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          'GENESIS_PRODUCTO_VAL126_SECTOR_INVALIDA',
                                                                                          'validaciones_genesis_producto',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                          par$cod_tipo || '|' ||
                                                                                          par$cod_sector,
                                                                                          0);
      END;
    END IF;
  
    /* Cuando es codigo ajeno, debe validar la integridad de los valores */
    IF (par$validaciones IS NULL OR
       (par$validaciones IS NOT NULL AND par$validaciones.COUNT = 0)) AND
       par$cod_ajeno IS NOT NULL THEN
    
      /* Identificador subcliente */
      IF par$cod_subcli IS NOT NULL THEN
        BEGIN
          SELECT COUNT(1)
            INTO var$existe
            FROM GEPR_TSUBCLIENTE SC
           INNER JOIN GEPR_TCLIENTE C
              ON C.OID_CLIENTE = SC.OID_CLIENTE
           WHERE C.OID_CLIENTE = par$oid_client
             AND SC.OID_SUBCLIENTE = par$oid_subcli;
        EXCEPTION
          WHEN no_data_found THEN
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'GENESIS_PRODUCTO_VAL126_SUBCLIENTE_INVALIDA',
                                                                                            'validaciones_genesis_producto',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                            par$cod_tipo || '|' ||
                                                                                            par$cod_subcli,
                                                                                            0);
        END;
      END IF;
    
      /* Identificador punto servicio */
      IF par$cod_ptserv IS NOT NULL THEN
        BEGIN
          SELECT COUNT(1)
            INTO var$existe
            FROM GEPR_TPUNTO_SERVICIO PS
           INNER JOIN GEPR_TSUBCLIENTE SC
              ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
           INNER JOIN GEPR_TCLIENTE C
              ON C.OID_CLIENTE = SC.OID_CLIENTE
           WHERE C.OID_CLIENTE = par$oid_client
             AND SC.OID_SUBCLIENTE = par$oid_subcli
             AND PS.OID_PTO_SERVICIO = par$oid_ptserv;
        EXCEPTION
          WHEN no_data_found THEN
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'GENESIS_PRODUCTO_VAL126_PUNTO_SERVICIO_INVALIDA',
                                                                                            'validaciones_genesis_producto',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                            par$cod_tipo || '|' ||
                                                                                            par$cod_ptserv,
                                                                                            0);
        END;
      END IF;
    
      /* Identificador subcanal */
      IF par$cod_subcan IS NOT NULL THEN
        BEGIN
          SELECT COUNT(1)
            INTO var$existe
            FROM GEPR_TSUBCANAL SC
           INNER JOIN GEPR_TCANAL C
              ON C.OID_CANAL = SC.OID_CANAL
           WHERE C.OID_CANAL = par$oid_canal
             AND SC.OID_SUBCANAL = par$oid_subcan;
        EXCEPTION
          WHEN no_data_found THEN
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'GENESIS_PRODUCTO_VAL126_subcanal_invalida',
                                                                                            'validaciones_genesis_producto',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                            par$cod_tipo || '|' ||
                                                                                            par$cod_subcan,
                                                                                            0);
        END;
      END IF;
    
      /* Identificador planta */
      IF par$cod_planta IS NOT NULL THEN
        BEGIN
          SELECT COUNT(1)
            INTO var$existe
            FROM GEPR_TPLANTA P
           INNER JOIN GEPR_TDELEGACION D
              ON D.OID_DELEGACION = P.OID_DELEGACION
           WHERE D.OID_DELEGACION = par$oid_delega
             AND P.OID_PLANTA = par$oid_planta;
        EXCEPTION
          WHEN no_data_found THEN
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'GENESIS_PRODUCTO_VAL126_PLANTA_INVALIDA',
                                                                                            'validaciones_genesis_producto',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                            par$cod_tipo || '|' ||
                                                                                            par$cod_planta,
                                                                                            0);
        END;
      END IF;
    
      /* Identificador sector */
      IF par$cod_sector IS NOT NULL THEN
        BEGIN
          SELECT COUNT(1)
            INTO var$existe
            FROM GEPR_TSECTOR S
           INNER JOIN GEPR_TPLANTA P
              ON P.OID_PLANTA = S.OID_PLANTA
           INNER JOIN GEPR_TDELEGACION D
              ON D.OID_DELEGACION = P.OID_DELEGACION
           WHERE D.OID_DELEGACION = par$oid_delega
             AND P.OID_PLANTA = par$oid_planta
             AND S.OID_SECTOR = par$oid_sector;
        EXCEPTION
          WHEN no_data_found THEN
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'GENESIS_PRODUCTO_VAL126_SECTOR_INVALIDA',
                                                                                            'validaciones_genesis_producto',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                            par$cod_tipo || '|' ||
                                                                                            par$cod_sector,
                                                                                            0);
        END;
      END IF;
    
    END IF;
  
  END scargar_cuenta_fondos;

  /* Cargar valores terminos - MAE */
  PROCEDURE scargar_terminos_fondos(par$avtdoc_cod_termino   IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$avtdoc_des_valor_ori IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$avtdoc_oid_documento OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtdoc_oid_termino   OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$avtdoc_des_valor     OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$validaciones         OUT gepr_pcomon_###VERSION###.tipo$descs_) IS
  
    /* CURSOR DE TERMINOS FORMULARIO OBLIGATORIOS */
    CURSOR cur$terminos(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT TI.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TFORMULARIO F
       INNER JOIN GEPR_TTERMINO_POR_IAC TI
          ON TI.OID_IAC = F.OID_IAC_INDIVIDUAL
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       WHERE F.OID_FORMULARIO = par$oid_formulario
         AND TI.BOL_ES_OBLIGATORIO = 1;
  
    var$termino_valido gepr_pcomon_###VERSION###.tipo$nbol_;
  
  BEGIN
  
    IF NOT
        gepr_putilidades_###VERSION###.farray_vacio_cod(par$avtdoc_cod_termino) THEN
      FOR idx IN par$avtdoc_cod_termino.first .. par$avtdoc_cod_termino.last LOOP
      
        BEGIN
          SELECT T.OID_TERMINO
            INTO par$avtdoc_oid_termino(par$avtdoc_oid_termino.COUNT)
            FROM GEPR_TTERMINO T
           INNER JOIN GEPR_TTERMINO_POR_IAC G
              ON G.OID_TERMINO = T.OID_TERMINO
           INNER JOIN SAPR_TFORMULARIO F
              ON F.OID_IAC_INDIVIDUAL = G.OID_IAC
           WHERE T.COD_TERMINO = par$avtdoc_cod_termino(idx)
             AND F.OID_FORMULARIO = par$oid_formulario;
        
          par$avtdoc_oid_documento(par$avtdoc_oid_documento.COUNT) := par$oid_documento;
          par$avtdoc_des_valor(par$avtdoc_des_valor.COUNT) := par$avtdoc_des_valor_ori(idx);
        EXCEPTION
          WHEN no_data_found THEN
            par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                            'GENESIS_PRODUCTO_VAL116',
                                                                                            'validaciones_genesis_producto',
                                                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                            par$avtdoc_cod_termino(idx),
                                                                                            0);
        END;
      
      END LOOP;
    END IF;
  
    /* Validar terminos obligatorios */
    FOR rec$termino IN cur$terminos(par$oid_formulario) LOOP
      IF NOT
          gepr_putilidades_###VERSION###.farray_vacio_cod(par$avtdoc_cod_termino) THEN
      
        var$termino_valido := 0;
        FOR idx IN par$avtdoc_cod_termino.first .. par$avtdoc_cod_termino.last LOOP
          IF rec$termino.COD_TERMINO = par$avtdoc_cod_termino(idx) THEN
            IF par$avtdoc_des_valor_ori(idx) IS NULL THEN
              par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                              'GENESIS_PRODUCTO_VAL117',
                                                                                              'validaciones_genesis_producto',
                                                                                              gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                              rec$termino.COD_TERMINO,
                                                                                              0);
            ELSE
              var$termino_valido := 1;
            END IF;
          END IF;
        END LOOP;
      
        IF var$termino_valido = 0 THEN
          par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                          'GENESIS_PRODUCTO_VAL117',
                                                                                          'validaciones_genesis_producto',
                                                                                          gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                          rec$termino.COD_TERMINO,
                                                                                          0);
        END IF;
      ELSE
        par$validaciones(par$validaciones.COUNT) := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                                        'GENESIS_PRODUCTO_VAL117',
                                                                                        'validaciones_genesis_producto',
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                                        rec$termino.COD_TERMINO,
                                                                                        0);
      END IF;
    END LOOP rec$termino;
  
  END scargar_terminos_fondos;

  /* Guardar documento - Gestion de Contenedores */
  PROCEDURE sguardar_doc_contenedor_alta(par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_comprobante     OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                         --documentos
                                         par$adoc_oid_documento       in gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoc_oid_documento_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoc_oid_documento_sust  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoc_oid_moviment_fondo  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoc_fyh_plan_certif     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                         par$adoc_fyh_contable        IN gepr_pcomon_###VERSION###.tipo$fyhs_,                            
                                         par$adocs_cod_actual_id      IN gepr_pcomon_###VERSION###.tipo$descs_,
                                         par$adocs_collection_id      IN gepr_pcomon_###VERSION###.tipo$descs_,
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
  
    /* variables locales */
    var$adoc_oid_cuenta_saldo_ori gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_cuenta_saldo_des gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_sector_origen    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_cuenta_origen    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_cuenta_destino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_sector_destino   gepr_pcomon_###VERSION###.tipo$oids_;
  
    var$cur_formulario         gepr_pcomon_###VERSION###.tipo$formularios;
    var$obj_tipos_contenedores gepr_pcomon_###VERSION###.tipo$tiposContenedores;
    var$cod_estado_doc         gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver                 gepr_pcomon_###VERSION###.tipo$nel_;
    var$inicio                 gepr_pcomon_###VERSION###.tipo$gmt_;
  
    var$registros gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_;
  
    var$es_nuevo  boolean;
    var$err       VARCHAR2(4000);
    var$esreenvio BOOLEAN;
  BEGIN
  
    var$inicio    := SYSTIMESTAMP;
    var$registros := 0;
    var$inserts   := 0;
    var$updates   := 0;
    var$selects   := 0;
    par$inserts   := 0;
    par$updates   := 0;
    par$deletes   := 0;
    par$selects   := 0;
  
    if par$cod_formulario IS NOT NULL OR par$oid_formulario IS NOT NULL THEN
    
      /* Recupera o formulario com suas caracteristicas */
      var$cur_formulario := sapr_pformulario_###VERSION###.frecuperar_formulario(par$oid_formulario => par$oid_formulario,
                                                                            par$cod_formulario => par$cod_formulario,
                                                                            par$selects        => var$selects);
    
      par$selects   := par$selects + var$selects;
      var$registros := var$registros + par$selects;
    
    END IF;
  
    if par$adoccont_cod_tipo_cont is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_cod(par$adoccont_cod_tipo_cont) then
    
      for idx in par$adoccont_cod_tipo_cont.first .. par$adoccont_cod_tipo_cont.last loop
        /* Recupera o tipo de contenedor */
        var$obj_tipos_contenedores(idx) := sapr_pcontenedor_###VERSION###.frecuperar_tipo_cont(par$cod_tipo_contenedor => par$adoccont_cod_tipo_cont(idx),
                                                                                          par$selects             => var$selects);
      
        par$selects   := par$selects + var$selects;
        var$registros := var$registros + par$selects;
      
      end loop;
    
    end if;
  
    /*   \* Validaciones del documento de Gestion de Contenedores *\ */
    svalidar_doc_contenedor_alta(par$oid_formulario        => par$oid_formulario,
                                 par$cod_formulario        => par$cod_formulario,
                                 par$aform_caracteristicas => var$cur_formulario,
                                 par$oid_grupo_documento   => par$oid_grupo_documento,
                                 --documentos
                                 par$adoc_oid_documento       => par$adoc_oid_documento,
                                 par$adoc_oid_documento_padre => par$adoc_oid_documento_padre,
                                 par$adoc_oid_documento_sust  => par$adoc_oid_documento_sust,
                                 par$adoc_oid_moviment_fondo  => par$adoc_oid_moviment_fondo,
                                 par$adoc_fyh_plan_certif     => par$adoc_fyh_plan_certif,
                                 par$adoc_fyh_gestion         => par$adoc_fyh_gestion,
                                 par$adoc_cod_externo         => par$adoc_cod_externo,
                                 par$adoc_rowver              => par$adoc_rowver,
                                 /* cuenta origen */
                                 par$adoc_oid_cuenta_origen     => par$adoc_oid_cuenta_origen,
                                 par$adoc_oid_cliente_origen    => par$adoc_oid_cliente_origen,
                                 par$adoc_oid_subcliente_origen => par$adoc_oid_subcliente_origen,
                                 par$adoc_oid_puntoservico_ori  => par$adoc_oid_puntoservico_ori,
                                 par$adoc_oid_canal_origen      => par$adoc_oid_canal_origen,
                                 par$adoc_oid_subcanal_origen   => par$adoc_oid_subcanal_origen,
                                 par$adoc_oid_delegacion_origen => par$adoc_oid_delegacion_origen,
                                 par$adoc_oid_planta_origen     => par$adoc_oid_planta_origen,
                                 par$adoc_oid_sector_origen     => par$adoc_oid_sector_origen
                                 
                                 /* cuenta destino */,
                                 par$adoc_oid_cuenta_destino   => par$adoc_oid_cuenta_destino,
                                 par$adoc_oid_cliente_destino  => par$adoc_oid_cliente_destino,
                                 par$adoc_oid_subcliente_des   => par$adoc_oid_subcliente_des,
                                 par$adoc_oid_puntoservico_des => par$adoc_oid_puntoservico_des,
                                 par$adoc_oid_canal_destino    => par$adoc_oid_canal_destino,
                                 par$adoc_oid_subcanal_destino => par$adoc_oid_subcanal_destino,
                                 par$adoc_oid_delegacion_des   => par$adoc_oid_delegacion_des,
                                 par$adoc_oid_planta_destino   => par$adoc_oid_planta_destino,
                                 par$adoc_oid_sector_destino   => par$adoc_oid_sector_destino,
                                 
                                 /* arrays asociativos */
                                 /* arrays de efectivo por documento */
                                 par$aefdoc_oid_documento    => par$aefdoc_oid_documento,
                                 par$aefdoc_oid_divisa       => par$aefdoc_oid_divisa,
                                 par$aefdoc_oid_denominacion => par$aefdoc_oid_denominacion,
                                 par$aefdoc_oid_unid_medida  => par$aefdoc_oid_unid_medida,
                                 par$aefdoc_cod_niv_detalle  => par$aefdoc_cod_niv_detalle,
                                 par$aefdoc_cod_tp_efec_tot  => par$aefdoc_cod_tp_efec_tot,
                                 par$aefdoc_oid_calidad      => par$aefdoc_oid_calidad,
                                 par$aefdoc_num_importe      => par$aefdoc_num_importe,
                                 par$aefdoc_nel_cantidad     => par$aefdoc_nel_cantidad
                                 
                                 /* arrays de medio de pago por documento */,
                                 par$ampdoc_oid_documento     => par$ampdoc_oid_documento,
                                 par$ampdoc_oid_divisa        => par$ampdoc_oid_divisa,
                                 par$ampdoc_oid_medio_pago    => par$ampdoc_oid_medio_pago,
                                 par$ampdoc_cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago,
                                 par$ampdoc_cod_nivel_detalle => par$ampdoc_cod_nivel_detalle,
                                 par$ampdoc_num_importe       => par$ampdoc_num_importe,
                                 par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,
                                 par$ampdoc_oid_unidad_medida => par$ampdoc_oid_unidad_medida,
                                 
                                 -- contenedores
                                 par$adoccont_oid_documento     => par$adoccont_oid_documento,
                                 par$adoccont_oid_contenedor    => par$adoccont_oid_contenedor,
                                 par$adoccont_cod_tipo_cont     => par$adoccont_cod_tipo_cont,
                                 par$adoccont_obj_tipos_cont    => var$obj_tipos_contenedores,
                                 par$adoccont_rowver_contenedor => par$adoccont_rowver_contenedor,
                                 
                                 par$aprco_oid_contenedor       => par$aprco_oid_contenedor,
                                 par$aprco_precintos_contenedor => par$aprco_precintos_contenedor,
                                 
                                 -- Elementos
                                 par$aelco_oid_contenedor => par$aelco_oid_contenedor,
                                 par$aelco_tipo_elemento  => par$aelco_tipo_elemento,
                                 par$aelco_precintos_ele  => par$aelco_precintos_ele,
                                 par$aelco_ids_elemento   => par$aelco_ids_elemento,
                                 
                                 /* informacion de ejecucion */
                                 par$usuario     => par$usuario,
                                 par$cod_cultura => par$cod_cultura);
  
    /* Cuenta de origen */
  
    var$adoc_oid_cuenta_origen  := par$adoc_oid_cuenta_origen;
    var$adoc_oid_cuenta_destino := par$adoc_oid_cuenta_destino;
  
    for idx in par$adoc_oid_documento.first .. par$adoc_oid_documento.last loop
    
      svalidar_cuentas(par$oid_formulario          => par$oid_formulario,
                       par$mov_ori_oid_cuenta      => par$adoc_oid_cuenta_origen(idx),
                       par$mov_ori_oid_client      => par$adoc_oid_cliente_origen(idx),
                       par$mov_ori_cod_client      => NULL,
                       par$mov_ori_oid_subcli      => par$adoc_oid_subcliente_origen(idx),
                       par$mov_ori_cod_subcli      => NULL,
                       par$mov_ori_oid_ptserv      => par$adoc_oid_puntoservico_ori(idx),
                       par$mov_ori_cod_ptserv      => NULL,
                       par$mov_ori_oid_canal       => par$adoc_oid_canal_origen(idx),
                       par$mov_ori_cod_canal       => NULL,
                       par$mov_ori_oid_subcan      => par$adoc_oid_subcanal_origen(idx),
                       par$mov_ori_cod_subcan      => NULL,
                       par$mov_ori_oid_delega      => par$adoc_oid_delegacion_origen(idx),
                       par$mov_ori_cod_delega      => NULL,
                       par$mov_ori_oid_planta      => par$adoc_oid_planta_origen(idx),
                       par$mov_ori_cod_planta      => NULL,
                       par$mov_ori_oid_sector      => par$adoc_oid_sector_origen(idx),
                       par$mov_ori_cod_sector      => NULL,
                       par$sal_ori_oid_cuenta      => NULL,
                       par$sal_ori_oid_client      => NULL,
                       par$sal_ori_cod_client      => NULL,
                       par$sal_ori_oid_subcli      => NULL,
                       par$sal_ori_cod_subcli      => NULL,
                       par$sal_ori_oid_ptserv      => NULL,
                       par$sal_ori_cod_ptserv      => NULL,
                       par$mov_des_oid_cuenta      => par$adoc_oid_cuenta_destino(idx),
                       par$mov_des_oid_client      => par$adoc_oid_cliente_destino(idx),
                       par$mov_des_cod_client      => NULL,
                       par$mov_des_oid_subcli      => par$adoc_oid_subcliente_des(idx),
                       par$mov_des_cod_subcli      => NULL,
                       par$mov_des_oid_ptserv      => par$adoc_oid_puntoservico_des(idx),
                       par$mov_des_cod_ptserv      => NULL,
                       par$mov_des_oid_canal       => par$adoc_oid_canal_destino(idx),
                       par$mov_des_cod_canal       => NULL,
                       par$mov_des_oid_subcan      => par$adoc_oid_subcanal_destino(idx),
                       par$mov_des_cod_subcan      => NULL,
                       par$mov_des_oid_delega      => par$adoc_oid_delegacion_des(idx),
                       par$mov_des_cod_delega      => NULL,
                       par$mov_des_oid_planta      => par$adoc_oid_planta_destino(idx),
                       par$mov_des_cod_planta      => NULL,
                       par$mov_des_oid_sector      => par$adoc_oid_sector_destino(idx),
                       par$mov_des_cod_sector      => NULL,
                       par$sal_des_oid_cuenta      => NULL,
                       par$sal_des_oid_client      => NULL,
                       par$sal_des_cod_client      => NULL,
                       par$sal_des_oid_subcli      => NULL,
                       par$sal_des_cod_subcli      => NULL,
                       par$sal_des_oid_ptserv      => NULL,
                       par$sal_des_cod_ptserv      => NULL,
                       par$mov_ori_oid_cuenta_resp => var$adoc_oid_cuenta_origen(idx),
                       par$mov_ori_oid_sector_resp => var$adoc_oid_sector_origen(idx),
                       par$sal_ori_oid_cuenta_resp => var$adoc_oid_cuenta_saldo_ori(idx),
                       par$mov_des_oid_cuenta_resp => var$adoc_oid_cuenta_destino(idx),
                       par$mov_des_oid_sector_resp => var$adoc_oid_sector_destino(idx),
                       par$sal_des_oid_cuenta_resp => var$adoc_oid_cuenta_saldo_des(idx),
                       par$cod_cultura             => par$cod_cultura,
                       par$cod_usuario             => par$usuario,
                       par$inserts                 => var$inserts,
                       par$updates                 => var$updates,
                       par$selects                 => var$selects,
                       par$esreenvio               => var$esreenvio);
    
      par$inserts   := par$inserts + var$inserts;
      par$updates   := par$updates + var$updates;
      par$selects   := par$selects + var$selects;
      var$registros := var$registros + var$inserts + var$updates +
                       var$selects;
    
    end loop;
  
    sguardar_documento(par$oid_formulario            => var$cur_formulario(1)
                                                        .OID_FORMULARIO,
                       par$oid_grupo_documento       => par$oid_grupo_documento,
                       par$adocs_oid                 => par$adoc_oid_documento,
                       par$adocs_oid_doc_padre       => par$adoc_oid_documento_padre,
                       par$adocs_oid_sustituto       => par$adoc_oid_documento_sust,
                       par$adocs_oid_mov_fondos      => par$adoc_oid_moviment_fondo,
                       par$adocs_fyh_plncertif       => par$adoc_fyh_plan_certif,
                       par$adocs_fyh_gestion         => par$adoc_fyh_gestion,
                       par$adocs_fyh_contable        => par$adoc_fyh_contable,
                       par$adocs_cod_actual_id       => par$adocs_cod_actual_id,
                       par$adocs_collection_id       => par$adocs_collection_id, 
                       par$adocs_cod_externo         => par$adoc_cod_externo,
                       par$adocs_oid_cuenta_ori      => var$adoc_oid_cuenta_origen,
                       par$adocs_oid_cuenta_des      => var$adoc_oid_cuenta_destino,
                       par$adocs_oid_cuentasaldo_ori => var$adoc_oid_cuenta_saldo_ori,
                       par$adocs_oid_cuentasaldo_des => var$adoc_oid_cuenta_saldo_des,
                       par$adocs_oid_sector_ori      => var$adoc_oid_sector_origen,
                       par$adocs_oid_sector_des      => var$adoc_oid_sector_destino,
                       par$adocs_rowver              => par$adoc_rowver,
                       
                       /* arrays asociativos */
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
                       
                       /* arrays de terminos por documento */
                       par$avtdoc_oid_documento => par$avtmpdoc_oid_documento,
                       par$avtdoc_oid_termino   => par$avtmpdoc_oid_t_mediopago,
                       par$avtdoc_des_valor     => par$avtmpdoc_des_valor,
                       
                       /* informacion de ejecucion */
                       par$usuario     => par$usuario,
                       par$cod_cultura => par$cod_cultura,
                       par$inserts     => var$inserts,
                       par$updates     => var$updates,
                       par$deletes     => var$deletes,
                       par$selects     => var$selects,
                       par$esnuevo     => var$es_nuevo);
  
    var$registros := var$registros + var$inserts + var$updates +
                     var$selects + var$deletes;
    par$inserts   := par$inserts + var$inserts;
    par$updates   := par$updates + var$updates;
    par$selects   := par$selects + var$selects;
    par$deletes   := par$deletes + var$deletes;
  
    if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => var$cur_formulario,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAlta) > 0 then
    
      if par$oid_grupo_documento is null then
      
        /* Grava o contenedor e seus valores*/
        sapr_pcontenedor_###VERSION###.sgrabar_contenedor_ind(par$obj_formularios            => var$cur_formulario,
                                                         par$oid_delegacion             => gepr_putilidades_###VERSION###.fobtener_primer_registro_oid(par$adoc_oid_delegacion_origen),
                                                         par$adoc_oid_documento         => par$adoc_oid_documento,
                                                         par$adoccont_oid_contenedor    => par$adoccont_oid_contenedor,
                                                         par$adoccont_obj_tipo_cont     => var$obj_tipos_contenedores,
                                                         par$adoccont_cod_tipo_servicio => par$adoccont_cod_tipo_servicio,
                                                         par$adoccont_cod_tipo_embalaje => par$adoccont_cod_tipo_embalaje,
                                                         par$adoccont_cod_puesto        => par$adoccont_cod_puesto,
                                                         par$adoccont_cod_contenedor    => par$adoccont_cod_contenedor,
                                                         par$adoccont_rowver_contenedor => par$adoccont_rowver_contenedor,
                                                         par$aprco_oid_contenedor       => par$aprco_oid_contenedor,
                                                         par$aprco_precintos_contenedor => par$aprco_precintos_contenedor,
                                                         par$aprco_bol_precinto_autom   => par$aprco_bol_precinto_autom,
                                                         par$aelco_oid_contenedor       => par$aelco_oid_contenedor,
                                                         par$aelco_tipo_elemento        => par$aelco_tipo_elemento,
                                                         par$aelco_precintos_ele        => par$aelco_precintos_ele,
                                                         par$aelco_ids_elemento         => par$aelco_ids_elemento,
                                                         par$aefco_oid_contenedor       => par$aefco_oid_contenedor,
                                                         par$aefco_oid_divisa           => par$aefco_oid_divisa,
                                                         par$aefco_oid_denom            => par$aefco_oid_denom,
                                                         par$aefco_oid_uni_med          => par$aefco_oid_uni_med,
                                                         par$aefco_cod_nivel_det        => par$aefco_cod_nivel_det,
                                                         par$aefco_cod_tipo_efec_tot    => par$aefco_cod_tipo_efec_tot,
                                                         par$aefco_oid_calidad          => par$aefco_oid_calidad,
                                                         par$aefco_num_importe          => par$aefco_num_importe,
                                                         par$aefco_nel_cantidad         => par$aefco_nel_cantidad,
                                                         par$ampco_oid_contenedor       => par$ampco_oid_contenedor,
                                                         par$ampco_oid_divisa           => par$ampco_oid_divisa,
                                                         par$ampco_oid_mp               => par$ampco_oid_mp,
                                                         par$ampco_cod_tipo_mp          => par$ampco_cod_tipo_mp,
                                                         par$ampco_cod_nivel_det        => par$ampco_cod_nivel_det,
                                                         par$ampco_num_importe          => par$ampco_num_importe,
                                                         par$ampco_nel_cantidad         => par$ampco_nel_cantidad,
                                                         par$ampco_oid_unidad_medida    => par$ampco_oid_unidad_medida,
                                                         par$avtmpco_oid_contenedor     => par$avtmpco_oid_contenedor,
                                                         par$avtmpco_oid_mp             => par$avtmpco_oid_mp,
                                                         par$avtmpco_oid_termediopago   => par$avtmpco_oid_termediopago,
                                                         par$avtmpco_des_valor          => par$avtmpco_des_valor,
                                                         par$avtmpco_nec_indicegrupo    => par$avtmpco_nec_indicegrupo,
                                                         par$usuario                    => par$usuario,
                                                         par$cod_cultura                => par$cod_cultura,
                                                         par$inserts                    => var$inserts,
                                                         par$updates                    => var$updates,
                                                         par$selects                    => var$selects);
      
      else
      
        sapr_pcontenedor_###VERSION###.sgrabar_contenedor_grp(par$obj_formularios            => var$cur_formulario,
                                                         par$oid_delegacion             => gepr_putilidades_###VERSION###.fobtener_primer_registro_oid(par$adoc_oid_delegacion_origen),
                                                         par$oid_grupo_documento        => par$oid_grupo_documento,
                                                         par$adoccont_oid_contenedor    => par$adoccont_oid_contenedor,
                                                         par$adoccont_obj_tipo_cont     => var$obj_tipos_contenedores,
                                                         par$adoccont_cod_tipo_servicio => par$adoccont_cod_tipo_servicio,
                                                         par$adoccont_cod_tipo_embalaje => par$adoccont_cod_tipo_embalaje,
                                                         par$adoccont_cod_puesto        => par$adoccont_cod_puesto,
                                                         par$adoccont_cod_contenedor    => par$adoccont_cod_contenedor,
                                                         par$adoccont_rowver_contenedor => par$adoccont_rowver_contenedor,
                                                         par$aprco_oid_contenedor       => par$aprco_oid_contenedor,
                                                         par$aprco_precintos_contenedor => par$aprco_precintos_contenedor,
                                                         par$aprco_bol_precinto_autom   => par$aprco_bol_precinto_autom,
                                                         par$aelco_oid_contenedor       => par$aelco_oid_contenedor,
                                                         par$aelco_tipo_elemento        => par$aelco_tipo_elemento,
                                                         par$aelco_precintos_ele        => par$aelco_precintos_ele,
                                                         par$aelco_ids_elemento         => par$aelco_ids_elemento,
                                                         par$aefco_oid_contenedor       => par$aefco_oid_contenedor,
                                                         par$aefco_oid_divisa           => par$aefco_oid_divisa,
                                                         par$aefco_oid_denom            => par$aefco_oid_denom,
                                                         par$aefco_oid_uni_med          => par$aefco_oid_uni_med,
                                                         par$aefco_cod_nivel_det        => par$aefco_cod_nivel_det,
                                                         par$aefco_cod_tipo_efec_tot    => par$aefco_cod_tipo_efec_tot,
                                                         par$aefco_oid_calidad          => par$aefco_oid_calidad,
                                                         par$aefco_num_importe          => par$aefco_num_importe,
                                                         par$aefco_nel_cantidad         => par$aefco_nel_cantidad,
                                                         par$ampco_oid_contenedor       => par$ampco_oid_contenedor,
                                                         par$ampco_oid_divisa           => par$ampco_oid_divisa,
                                                         par$ampco_oid_mp               => par$ampco_oid_mp,
                                                         par$ampco_cod_tipo_mp          => par$ampco_cod_tipo_mp,
                                                         par$ampco_cod_nivel_det        => par$ampco_cod_nivel_det,
                                                         par$ampco_num_importe          => par$ampco_num_importe,
                                                         par$ampco_nel_cantidad         => par$ampco_nel_cantidad,
                                                         par$ampco_oid_unidad_medida    => par$ampco_oid_unidad_medida,
                                                         par$avtmpco_oid_contenedor     => par$avtmpco_oid_contenedor,
                                                         par$avtmpco_oid_mp             => par$avtmpco_oid_mp,
                                                         par$avtmpco_oid_termediopago   => par$avtmpco_oid_termediopago,
                                                         par$avtmpco_des_valor          => par$avtmpco_des_valor,
                                                         par$avtmpco_nec_indicegrupo    => par$avtmpco_nec_indicegrupo,
                                                         par$usuario                    => par$usuario,
                                                         par$cod_cultura                => par$cod_cultura,
                                                         par$inserts                    => var$inserts,
                                                         par$updates                    => var$updates,
                                                         par$selects                    => var$selects);
      end if;
    
      var$registros := var$registros + var$inserts + var$updates +
                       var$selects;
      par$inserts   := par$inserts + var$inserts;
      par$updates   := par$updates + var$updates;
      par$selects   := par$selects + var$selects;
    
    end if;
  
    if par$confirmar_doc > 0 then
    
      var$cod_estado_doc := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
    
      for idx in par$adoc_oid_documento.first .. par$adoc_oid_documento.last loop
      
        var$rowver := par$adoc_rowver(idx);
        sapr_ptransiciones_###VERSION###.sejecutar_doc(par$oid_llamada          => NULL,
                                                  par$oid_documento        => par$adoc_oid_documento(idx),
                                                  par$cod_estado_documento => var$cod_estado_doc,
                                                  par$cod_comprobante      => par$cod_comprobante,
                                                  par$rowver               => var$rowver,
                                                  par$usuario              => par$usuario,
                                                  par$cod_cultura          => par$cod_cultura,
                                                  par$info_ejecucion       => par$info_ejecucion,
                                                  par$hacer_commit         => 0,
                                                  par$inserts              => var$inserts,
                                                  par$updates              => var$updates,
                                                  par$deletes              => var$deletes,
                                                  par$selects              => var$selects,
                                                  par$cod_ejecucion        => par$cod_ejecucion);
      
        var$registros := var$registros + var$inserts + var$updates +
                         var$selects + var$deletes;
        par$inserts   := par$inserts + var$inserts;
        par$updates   := par$updates + var$updates;
        par$selects   := par$selects + var$selects;
        par$deletes   := par$deletes + var$deletes;
      
      end loop;
    
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
    
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              SQLCODE || ' - ' || SQLERRM,
                              true);
    
  END sguardar_doc_contenedor_alta;

  /* Guardar documento - Gestion de Contenedores */
  PROCEDURE sguardar_doc_contenedor(par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    --documentos
                                    par$adoc_oid_documento       in gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adoc_oid_documento_padre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adoc_oid_documento_sust  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adoc_oid_moviment_fondo  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$adoc_fyh_plan_certif     IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adoc_fyh_gestion         IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                    par$adoc_fyh_contable        IN gepr_pcomon_###VERSION###.tipo$fyhs_,                             
                                    par$adoc_cod_actual_id       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$adoc_collection_id       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                    par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$adoc_cod_comprobante     OUT gepr_pcomon_###VERSION###.tipo$codS_,
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
  
    /* variables locales */
    var$adoc_oid_cuenta_saldo_ori gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_cuenta_saldo_des gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_sector_origen    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_cuenta_origen    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_cuenta_destino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_sector_destino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$cur_formulario            gepr_pcomon_###VERSION###.tipo$formularios;
    var$cod_estado_doc            gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowver                    gepr_pcomon_###VERSION###.tipo$nel_;
    var$inicio                    gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros                 gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts                   gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates                   gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges                    gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects                   gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes                   gepr_pcomon_###VERSION###.tipo$nel_;
    var$es_nuevo                  boolean;
    var$err                       VARCHAR2(4000);
    var$esreenvio                 BOOLEAN;
  BEGIN
  
    var$inicio    := SYSTIMESTAMP;
    var$registros := 0;
    var$inserts   := 0;
    var$updates   := 0;
    var$selects   := 0;
    par$inserts   := 0;
    par$updates   := 0;
    par$deletes   := 0;
    par$selects   := 0;
  
    if par$cod_formulario IS NOT NULL OR par$oid_formulario IS NOT NULL THEN
    
      /* Recupera o formulario com suas caracteristicas */
      var$cur_formulario := sapr_pformulario_###VERSION###.frecuperar_formulario(par$oid_formulario => par$oid_formulario,
                                                                            par$cod_formulario => par$cod_formulario,
                                                                            par$selects        => var$selects);
    
      par$selects   := par$selects + var$selects;
      var$registros := var$registros + par$selects;
    
    END IF;
  
    /*   \* Validaciones del documento de Gestion de Contenedores *\ */
    svalidar_doc_contenedor(par$oid_formulario        => par$oid_formulario,
                            par$cod_formulario        => par$cod_formulario,
                            par$aform_caracteristicas => var$cur_formulario,
                            par$oid_grupo_documento   => par$oid_grupo_documento,
                            --documentos
                            par$adoc_oid_documento       => par$adoc_oid_documento,
                            par$adoc_oid_documento_padre => par$adoc_oid_documento_padre,
                            par$adoc_oid_documento_sust  => par$adoc_oid_documento_sust,
                            par$adoc_oid_moviment_fondo  => par$adoc_oid_moviment_fondo,
                            par$adoc_fyh_plan_certif     => par$adoc_fyh_plan_certif,
                            par$adoc_fyh_gestion         => par$adoc_fyh_gestion,
                            par$adoc_cod_externo         => par$adoc_cod_externo,
                            par$adoc_rowver              => par$adoc_rowver,
                            /* cuenta origen */
                            par$adoc_oid_cuenta_origen     => par$adoc_oid_cuenta_origen,
                            par$adoc_oid_cliente_origen    => par$adoc_oid_cliente_origen,
                            par$adoc_oid_subcliente_origen => par$adoc_oid_subcliente_origen,
                            par$adoc_oid_puntoservico_ori  => par$adoc_oid_puntoservico_ori,
                            par$adoc_oid_canal_origen      => par$adoc_oid_canal_origen,
                            par$adoc_oid_subcanal_origen   => par$adoc_oid_subcanal_origen,
                            par$adoc_oid_delegacion_origen => par$adoc_oid_delegacion_origen,
                            par$adoc_oid_planta_origen     => par$adoc_oid_planta_origen,
                            par$adoc_oid_sector_origen     => par$adoc_oid_sector_origen
                            
                            /* cuenta destino */,
                            par$adoc_oid_cuenta_destino   => par$adoc_oid_cuenta_destino,
                            par$adoc_oid_cliente_destino  => par$adoc_oid_cliente_destino,
                            par$adoc_oid_subcliente_des   => par$adoc_oid_subcliente_des,
                            par$adoc_oid_puntoservico_des => par$adoc_oid_puntoservico_des,
                            par$adoc_oid_canal_destino    => par$adoc_oid_canal_destino,
                            par$adoc_oid_subcanal_destino => par$adoc_oid_subcanal_destino,
                            par$adoc_oid_delegacion_des   => par$adoc_oid_delegacion_des,
                            par$adoc_oid_planta_destino   => par$adoc_oid_planta_destino,
                            par$adoc_oid_sector_destino   => par$adoc_oid_sector_destino,
                            
                            /* arrays asociativos */
                            /* arrays de efectivo por documento */
                            par$aefdoc_oid_documento    => par$aefdoc_oid_documento,
                            par$aefdoc_oid_divisa       => par$aefdoc_oid_divisa,
                            par$aefdoc_oid_denominacion => par$aefdoc_oid_denominacion,
                            par$aefdoc_oid_unid_medida  => par$aefdoc_oid_unid_medida,
                            par$aefdoc_cod_niv_detalle  => par$aefdoc_cod_niv_detalle,
                            par$aefdoc_cod_tp_efec_tot  => par$aefdoc_cod_tp_efec_tot,
                            par$aefdoc_oid_calidad      => par$aefdoc_oid_calidad,
                            par$aefdoc_num_importe      => par$aefdoc_num_importe,
                            par$aefdoc_nel_cantidad     => par$aefdoc_nel_cantidad
                            
                            /* arrays de medio de pago por documento */,
                            par$ampdoc_oid_documento     => par$ampdoc_oid_documento,
                            par$ampdoc_oid_divisa        => par$ampdoc_oid_divisa,
                            par$ampdoc_oid_medio_pago    => par$ampdoc_oid_medio_pago,
                            par$ampdoc_cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago,
                            par$ampdoc_cod_nivel_detalle => par$ampdoc_cod_nivel_detalle,
                            par$ampdoc_num_importe       => par$ampdoc_num_importe,
                            par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,
                            par$ampdoc_oid_unidad_medida => par$ampdoc_oid_unidad_medida,
                            
                            -- contenedores
                            par$adoccont_oid_documento     => par$adoccont_oid_documento,
                            par$adoccont_oid_contenedor    => par$adoccont_oid_contenedor,
                            par$adoccont_rowver_contenedor => par$adoccont_rowver_contenedor,
                            
                            -- Elementos
                            par$aelco_oid_contenedor => par$aelco_oid_contenedor,
                            par$aelco_tipo_elemento  => par$aelco_tipo_elemento,
                            par$aelco_precintos_ele  => par$aelco_precintos_ele,
                            par$aelco_ids_elemento   => par$aelco_ids_elemento,
                            
                            /* informacion de ejecucion */
                            par$usuario     => par$usuario,
                            par$cod_cultura => par$cod_cultura);
  
    /* Cuenta de origen */
  
    var$adoc_oid_cuenta_origen  := par$adoc_oid_cuenta_origen;
    var$adoc_oid_cuenta_destino := par$adoc_oid_cuenta_destino;
  
    for idx in par$adoc_oid_documento.first .. par$adoc_oid_documento.last loop
    
      svalidar_cuentas(par$oid_formulario          => par$oid_formulario,
                       par$mov_ori_oid_cuenta      => par$adoc_oid_cuenta_origen(idx),
                       par$mov_ori_oid_client      => par$adoc_oid_cliente_origen(idx),
                       par$mov_ori_cod_client      => NULL,
                       par$mov_ori_oid_subcli      => par$adoc_oid_subcliente_origen(idx),
                       par$mov_ori_cod_subcli      => NULL,
                       par$mov_ori_oid_ptserv      => par$adoc_oid_puntoservico_ori(idx),
                       par$mov_ori_cod_ptserv      => NULL,
                       par$mov_ori_oid_canal       => par$adoc_oid_canal_origen(idx),
                       par$mov_ori_cod_canal       => NULL,
                       par$mov_ori_oid_subcan      => par$adoc_oid_subcanal_origen(idx),
                       par$mov_ori_cod_subcan      => NULL,
                       par$mov_ori_oid_delega      => par$adoc_oid_delegacion_origen(idx),
                       par$mov_ori_cod_delega      => NULL,
                       par$mov_ori_oid_planta      => par$adoc_oid_planta_origen(idx),
                       par$mov_ori_cod_planta      => NULL,
                       par$mov_ori_oid_sector      => par$adoc_oid_sector_origen(idx),
                       par$mov_ori_cod_sector      => NULL,
                       par$sal_ori_oid_cuenta      => NULL,
                       par$sal_ori_oid_client      => NULL,
                       par$sal_ori_cod_client      => NULL,
                       par$sal_ori_oid_subcli      => NULL,
                       par$sal_ori_cod_subcli      => NULL,
                       par$sal_ori_oid_ptserv      => NULL,
                       par$sal_ori_cod_ptserv      => NULL,
                       par$mov_des_oid_cuenta      => par$adoc_oid_cuenta_destino(idx),
                       par$mov_des_oid_client      => par$adoc_oid_cliente_destino(idx),
                       par$mov_des_cod_client      => NULL,
                       par$mov_des_oid_subcli      => par$adoc_oid_subcliente_des(idx),
                       par$mov_des_cod_subcli      => NULL,
                       par$mov_des_oid_ptserv      => par$adoc_oid_puntoservico_des(idx),
                       par$mov_des_cod_ptserv      => NULL,
                       par$mov_des_oid_canal       => par$adoc_oid_canal_destino(idx),
                       par$mov_des_cod_canal       => NULL,
                       par$mov_des_oid_subcan      => par$adoc_oid_subcanal_destino(idx),
                       par$mov_des_cod_subcan      => NULL,
                       par$mov_des_oid_delega      => par$adoc_oid_delegacion_des(idx),
                       par$mov_des_cod_delega      => NULL,
                       par$mov_des_oid_planta      => par$adoc_oid_planta_destino(idx),
                       par$mov_des_cod_planta      => NULL,
                       par$mov_des_oid_sector      => par$adoc_oid_sector_destino(idx),
                       par$mov_des_cod_sector      => NULL,
                       par$sal_des_oid_cuenta      => NULL,
                       par$sal_des_oid_client      => NULL,
                       par$sal_des_cod_client      => NULL,
                       par$sal_des_oid_subcli      => NULL,
                       par$sal_des_cod_subcli      => NULL,
                       par$sal_des_oid_ptserv      => NULL,
                       par$sal_des_cod_ptserv      => NULL,
                       par$mov_ori_oid_cuenta_resp => var$adoc_oid_cuenta_origen(idx),
                       par$mov_ori_oid_sector_resp => var$adoc_oid_sector_origen(idx),
                       par$sal_ori_oid_cuenta_resp => var$adoc_oid_cuenta_saldo_ori(idx),
                       par$mov_des_oid_cuenta_resp => var$adoc_oid_cuenta_destino(idx),
                       par$mov_des_oid_sector_resp => var$adoc_oid_sector_destino(idx),
                       par$sal_des_oid_cuenta_resp => var$adoc_oid_cuenta_saldo_des(idx),
                       par$cod_cultura             => par$cod_cultura,
                       par$cod_usuario             => par$usuario,
                       par$inserts                 => var$inserts,
                       par$updates                 => var$updates,
                       par$selects                 => var$selects,
                       par$esreenvio               => var$esreenvio);
    
      par$inserts   := par$inserts + var$inserts;
      par$updates   := par$updates + var$updates;
      par$selects   := par$selects + var$selects;
      var$registros := var$registros + var$inserts + var$updates +
                       var$selects;
    
    end loop;
  
    sguardar_documento(par$oid_formulario            => var$cur_formulario(1)
                                                        .OID_FORMULARIO,
                       par$oid_grupo_documento       => par$oid_grupo_documento,
                       par$adocs_oid                 => par$adoc_oid_documento,
                       par$adocs_oid_doc_padre       => par$adoc_oid_documento_padre,
                       par$adocs_oid_sustituto       => par$adoc_oid_documento_sust,
                       par$adocs_oid_mov_fondos      => par$adoc_oid_moviment_fondo,
                       par$adocs_fyh_plncertif       => par$adoc_fyh_plan_certif,
                       par$adocs_fyh_gestion         => par$adoc_fyh_gestion,
                       par$adocs_fyh_contable        => par$adoc_fyh_contable,
                       par$adocs_cod_actual_id       => par$adoc_cod_actual_id,
                       par$adocs_collection_id       => par$adoc_collection_id, 
                       par$adocs_cod_externo         => par$adoc_cod_externo,
                       par$adocs_oid_cuenta_ori      => var$adoc_oid_cuenta_origen,
                       par$adocs_oid_cuenta_des      => var$adoc_oid_cuenta_destino,
                       par$adocs_oid_cuentasaldo_ori => var$adoc_oid_cuenta_saldo_ori,
                       par$adocs_oid_cuentasaldo_des => var$adoc_oid_cuenta_saldo_des,
                       par$adocs_oid_sector_ori      => var$adoc_oid_sector_origen,
                       par$adocs_oid_sector_des      => var$adoc_oid_sector_destino,
                       par$adocs_rowver              => par$adoc_rowver,
                       
                       /* arrays asociativos */
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
                       
                       /* arrays de terminos por documento */
                       par$avtdoc_oid_documento => par$avtmpdoc_oid_documento,
                       par$avtdoc_oid_termino   => par$avtmpdoc_oid_t_mediopago,
                       par$avtdoc_des_valor     => par$avtmpdoc_des_valor,
                       
                       /* informacion de ejecucion */
                       par$usuario     => par$usuario,
                       par$cod_cultura => par$cod_cultura,
                       par$inserts     => var$inserts,
                       par$updates     => var$updates,
                       par$deletes     => var$deletes,
                       par$selects     => var$selects,
                       par$esnuevo     => var$es_nuevo);
  
    var$registros := var$registros + var$inserts + var$updates +
                     var$selects + var$deletes;
    par$inserts   := par$inserts + var$inserts;
    par$updates   := par$updates + var$updates;
    par$selects   := par$selects + var$selects;
    par$deletes   := par$deletes + var$deletes;
  
    if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => var$cur_formulario,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracBaja) > 0 then
    
      sapr_pcontenedor_###VERSION###.sbaja_contenedor_ind(par$obj_formularios            => var$cur_formulario,
                                                     par$oid_delegacion             => gepr_putilidades_###VERSION###.fobtener_primer_registro_oid(par$adoc_oid_delegacion_origen),
                                                     par$adoc_oid_documento         => par$adoc_oid_documento,
                                                     par$adoc_oid_sector_origen     => var$adoc_oid_sector_origen,
                                                     par$adoc_oid_cuenta_origen     => var$adoc_oid_cuenta_origen,
                                                     par$adoc_oid_cuenta_saldo_ori  => var$adoc_oid_cuenta_saldo_ori,
                                                     par$adoccont_oid_contenedor    => par$adoccont_oid_contenedor,
                                                     par$adoccont_rowver_contenedor => par$adoccont_rowver_contenedor,
                                                     par$aelco_oid_contenedor       => par$aelco_oid_contenedor,
                                                     par$aelco_tipo_elemento        => par$aelco_tipo_elemento,
                                                     par$aelco_precintos_ele        => par$aelco_precintos_ele,
                                                     par$aelco_ids_elemento         => par$aelco_ids_elemento,
                                                     par$usuario                    => par$usuario,
                                                     par$cod_cultura                => par$cod_cultura,
                                                     par$inserts                    => var$inserts,
                                                     par$updates                    => var$updates);
    
      var$registros := var$registros + var$inserts + var$updates;
      par$inserts   := par$inserts + var$inserts;
      par$updates   := par$updates + var$updates;
    
    elsif sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => var$cur_formulario,
                                                                par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvio) > 0 then
    
      for idx in par$adoc_oid_documento.first .. par$adoc_oid_documento.last loop
      
        sapr_pcontenedor_###VERSION###.supd_tcontenedor(par$oid_contenedor   => par$adoccont_oid_contenedor(idx),
                                                   par$oid_documento    => par$adoc_oid_documento(idx),
                                                   par$oid_cuenta_mov   => null,
                                                   par$oid_cuenta_saldo => null,
                                                   par$oid_sector       => null,
                                                   par$cod_usuario      => par$usuario,
                                                   par$cod_estado       => gepr_pcomon_###VERSION###.const$CodEstadoContEnTransito,
                                                   par$updates          => var$updates);
      
        var$registros := var$registros + var$inserts + var$updates;
        par$inserts   := par$inserts + var$inserts;
        par$updates   := par$updates + var$updates;
      
        /* Grava na tabela documento elemento */
        sins_tdocumento_elemento(par$oid_documento        => par$adoc_oid_documento(idx),
                                 par$oid_remesa           => null,
                                 par$oid_bulto            => null,
                                 par$cod_usuario          => par$usuario,
                                 par$oid_contenedor       => par$adoccont_oid_contenedor(idx),
                                 par$oid_contenedor_padre => null,
                                 par$oid_cont_mayor_nivel => null,
                                 par$inserts              => par$inserts);
      
        var$registros := var$registros + var$inserts;
        par$inserts   := par$inserts + var$inserts;
      
      end loop;
    
    end if;
  
    if par$confirmar_doc > 0 then
    
      var$cod_estado_doc := gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado;
    
      for idx in par$adoc_oid_documento.first .. par$adoc_oid_documento.last loop
      
        var$rowver := par$adoc_rowver(idx);
        sapr_ptransiciones_###VERSION###.sejecutar_doc(par$oid_llamada          => NULL,
                                                  par$oid_documento        => par$adoc_oid_documento(idx),
                                                  par$cod_estado_documento => var$cod_estado_doc,
                                                  par$cod_comprobante      => par$adoc_cod_comprobante(idx),
                                                  par$rowver               => var$rowver,
                                                  par$usuario              => par$usuario,
                                                  par$cod_cultura          => par$cod_cultura,
                                                  par$info_ejecucion       => par$info_ejecucion,
                                                  par$hacer_commit         => 0,
                                                  par$inserts              => var$inserts,
                                                  par$updates              => var$updates,
                                                  par$deletes              => var$deletes,
                                                  par$selects              => var$selects,
                                                  par$cod_ejecucion        => par$cod_ejecucion);
      
        var$registros := var$registros + var$inserts + var$updates +
                         var$selects + var$deletes;
        par$inserts   := par$inserts + var$inserts;
        par$updates   := par$updates + var$updates;
        par$selects   := par$selects + var$selects;
        par$deletes   := par$deletes + var$deletes;
      
      end loop;
    
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
    
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                           par$inserts,
                                                           par$updates,
                                                           par$deletes,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              SQLCODE || ' - ' || SQLERRM,
                              true);
    
  END sguardar_doc_contenedor;

  /* srecuperar_documentos */
  PROCEDURE srecuperar_documentos(par$oids_documentos            IN gepr_pcomon_###VERSION###.tipo$oids_,
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
                                  par$ejecucion_interna          IN INTEGER,
                                  par$usuario                    IN VARCHAR2,
                                  par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$info_ejecucion             IN VARCHAR2,
                                  par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                  par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$inicio      gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts     gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates     gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes     gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects     gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_;
    var$err         varchar2(4000);
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
    FOR idx IN par$oids_documentos.first .. par$oids_documentos.last LOOP
    
      INSERT INTO SAPR_GTT_TOIDS
        (OID, COD_CALIFICADOR)
      VALUES
        (par$oids_documentos(idx), 'DOCUMENTO');
      var$inserts   := var$inserts + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
      INSERT INTO SAPR_GTT_TOIDS
        (OID, COD_CALIFICADOR)
        SELECT OID_DOCUMENTO_PADRE, 'DOC_PADRE'
          FROM SAPR_TDOCUMENTO
         WHERE OID_DOCUMENTO = par$oids_documentos(idx)
           AND OID_DOCUMENTO_PADRE IS NOT NULL;
      var$inserts   := var$inserts + sql%ROWCOUNT;
      var$selects   := var$selects + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
    END LOOP;
  
    /* - Formularios */
    sapr_pformulario_###VERSION###.scargar_tt_oids_form_docs(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;
  
    /* - Terminos - Formularios */
    sapr_pformulario_###VERSION###.scargar_tt_oids_term_gpr;
    var$inserts   := var$inserts + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;
  
    /* - Cuentas */
    sapr_pcuentas_###VERSION###.scargar_tt_oids_cuentas_docs(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;
  
    /* - Sectores */
    sapr_psector_###VERSION###.scargar_tt_oids_sector_docs(par$selects => var$selects_int,
                                                      par$inserts => var$inserts_int);
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
  
    /* - TABLA: Documentos */
    par$doc_rc_documentos := fcargar_rc_documento;
    var$selects           := var$selects + SQL % ROWCOUNT;
    var$registros         := var$registros + SQL % ROWCOUNT;
  
    /* - TABLA: Documentos Padres */
    par$doc_rc_documentos_padre := fcargar_rc_doc_padre;
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
  
    /* - TABLA: Valor terminos documentos */
    par$doc_rc_valor_terminos_doc := fcargar_rc_valorterminosdoc;
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
  
    /* - TABLA: Historico Documento */
    par$doc_rc_historico_mov_doc := fcargar_rc_historico;
    var$selects                  := var$selects + SQL % ROWCOUNT;
    var$registros                := var$registros + SQL % ROWCOUNT;
  
    /* - TABLA: Valores de Documentos */
    par$doc_rc_valores_documentos := fcargar_rc_valores;
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
    SAPR_PELEMENTO_###VERSION###.srecuperar_elementos(par$oids_documentos           => par$oids_documentos,
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
    var$registros := var$registros + var$inserts_int + var$selects_int;
  
    IF par$ejecucion_interna = 0 THEN
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in('srecuperar_documentos',
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
    ELSE
      par$inserts := var$inserts;
      par$selects := var$selects;
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
    
      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in('srecuperar_documentos',
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
  END srecuperar_documentos;

  /* srecuperar_ultimodocumentos */
  PROCEDURE srecuperar_ultimodocumentos(par$oids_documentos            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$bol_gestion_bultos         IN gepr_pcomon_###VERSION###.tipo$nbol_,
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
                                        par$ejecucion_interna          IN INTEGER,
                                        par$usuario                    IN VARCHAR2,
                                        par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion             IN VARCHAR2,
                                        par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
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
  
    /* CURSOR DE DOCUMENTOS */
    CURSOR cur$documentos IS
      SELECT OID AS OID_DOCUMENTO
        FROM SAPR_GTT_TOIDS
       WHERE COD_CALIFICADOR = 'DOCUMENTO';
  
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
    FOR idx IN par$oids_documentos.first .. par$oids_documentos.last LOOP
    
      IF par$bol_gestion_bultos > 0 THEN
        /* Gestion de Bultos */
        INSERT INTO SAPR_GTT_TOIDS
          SELECT DISTINCT DE.OID_DOCUMENTO, 'DOCUMENTO' COD_CALIFICADOR
            FROM SAPR_TDOCUMENTOXELEMENTO DE
           INNER JOIN (SELECT B.OID_BULTO
                         FROM SAPR_TBULTO B
                        START WITH B.OID_BULTO =
                                   (SELECT DE.OID_BULTO
                                      FROM SAPR_TDOCUMENTOXELEMENTO DE
                                     WHERE DE.OID_DOCUMENTO =
                                           par$oids_documentos(idx)
                                       AND ROWNUM = 1)
                       CONNECT BY NOCYCLE
                        PRIOR B.OID_BULTO = B.OID_BULTO_PADRE) B
              ON B.OID_BULTO = DE.OID_BULTO
           WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
             AND rownum = 1;
      ELSE
        /* Gestion de remesa */
        INSERT INTO SAPR_GTT_TOIDS
          SELECT DISTINCT DE.OID_DOCUMENTO, 'DOCUMENTO' COD_CALIFICADOR
            FROM SAPR_TDOCUMENTOXELEMENTO DE
           INNER JOIN (SELECT R.OID_REMESA
                         FROM SAPR_TREMESA R
                        START WITH R.OID_REMESA =
                                   (SELECT DE.OID_REMESA
                                      FROM SAPR_TDOCUMENTOXELEMENTO DE
                                     WHERE DE.OID_DOCUMENTO =
                                           par$oids_documentos(idx)
                                       AND ROWNUM = 1)
                       CONNECT BY NOCYCLE
                        PRIOR R.OID_REMESA = R.OID_REMESA_PADRE) R
              ON R.OID_REMESA = DE.OID_REMESA
           WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
             AND rownum = 1;
      END IF;
      var$inserts   := var$inserts + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
    END LOOP;
  
    /* - Formularios */
    sapr_pformulario_###VERSION###.scargar_tt_oids_form_docs(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;
  
    /* - Terminos - Formularios */
    sapr_pformulario_###VERSION###.scargar_tt_oids_term_gpr;
    var$inserts   := var$inserts + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;
  
    /* - Cuentas */
    sapr_pcuentas_###VERSION###.scargar_tt_oids_cuentas_docs(par$inserts => var$inserts_int);
    var$inserts   := var$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;
  
    /* - Sectores */
    sapr_psector_###VERSION###.scargar_tt_oids_sector_docs(par$selects => var$selects_int,
                                                      par$inserts => var$inserts_int);
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
  
    /* - TABLA: Documentos */
    par$doc_rc_documentos := fcargar_rc_documento;
    var$selects           := var$selects + SQL % ROWCOUNT;
    var$registros         := var$registros + SQL % ROWCOUNT;
  
    /* - TABLA: Documentos Padres */
    par$doc_rc_documentos_padre := fcargar_rc_doc_padre;
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
  
    /* - TABLA: Valor terminos documentos */
    par$doc_rc_valor_terminos_doc := fcargar_rc_valorterminosdoc;
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
  
    /* - TABLA: Historico Documento */
    par$doc_rc_historico_mov_doc := fcargar_rc_historico;
    var$selects                  := var$selects + SQL % ROWCOUNT;
    var$registros                := var$registros + SQL % ROWCOUNT;
  
    /* - TABLA: Valores de Documentos */
    par$doc_rc_valores_documentos := fcargar_rc_valores;
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
    FOR rec$documento IN cur$documentos LOOP
      var$oids_documentos(var$oids_documentos.count) := rec$documento.oid_documento;
    END LOOP rec$termino;
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
  
    IF par$ejecucion_interna = 0 THEN
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in('srecuperar_ultimodocumentos',
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
    ELSE
      par$inserts := var$inserts;
      par$selects := var$selects;
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
    
      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in('srecuperar_ultimodocumentos',
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
  END srecuperar_ultimodocumentos;

  /* sverificar_documento_existe */
  PROCEDURE sverificar_documento_existe(par$acod_externo               IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_client        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_subcli        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_ptserv        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_canal         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_subcan        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_delega        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_planta        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$amov_ori_cod_sector        IN gepr_pcomon_###VERSION###.tipo$cods_,
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
                                        
                                        par$info_ejecucion IN VARCHAR2,
                                        par$cod_ejecucion  OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sverificar_documento_existe';
  
    var$inicio          gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros       gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts         gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates         gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes         gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges          gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects         gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts_int     gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int     gepr_pcomon_###VERSION###.tipo$nel_;
    var$existe          gepr_pcomon_###VERSION###.tipo$nel_;
    var$oids_documentos gepr_pcomon_###VERSION###.tipo$oids_;
    var$err             varchar2(4000);
  
    /* CURSOR DE DOCUMENTOS */
    CURSOR cur$documentos IS
      SELECT OID AS OID_DOCUMENTO
        FROM SAPR_GTT_TOIDS
       WHERE COD_CALIFICADOR = 'DOCUMENTO';
  
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
    FOR idx IN par$acod_externo.first .. par$acod_externo.last LOOP
    
      INSERT INTO SAPR_GTT_TOIDS
        SELECT DISTINCT DE.OID_DOCUMENTO, 'DOCUMENTO' COD_CALIFICADOR
          FROM SAPR_TDOCUMENTOXELEMENTO DE
         INNER JOIN (SELECT R.OID_REMESA, R.OID_CUENTA
                       FROM SAPR_TREMESA R
                      START WITH R.COD_EXTERNO = par$acod_externo(idx)
                     CONNECT BY NOCYCLE
                      PRIOR R.OID_REMESA = R.OID_REMESA_PADRE) R
            ON R.OID_REMESA = DE.OID_REMESA
         INNER JOIN SAPR_TCUENTA C
            ON C.OID_CUENTA = R.OID_CUENTA
         INNER JOIN GEPR_TSECTOR S
            ON S.OID_SECTOR = C.OID_SECTOR
         INNER JOIN GEPR_TPLANTA P
            ON P.OID_PLANTA = S.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION D
            ON D.OID_DELEGACION = P.OID_DELEGACION
         INNER JOIN GEPR_TSUBCANAL SCN
            ON SCN.OID_SUBCANAL = C.OID_SUBCANAL
         INNER JOIN GEPR_TCANAL CN
            ON CN.OID_CANAL = SCN.OID_CANAL
         INNER JOIN GEPR_TCLIENTE CL
            ON CL.OID_CLIENTE = C.OID_CLIENTE
          LEFT JOIN GEPR_TSUBCLIENTE SCL
            ON SCL.OID_SUBCLIENTE = C.OID_SUBCLIENTE
          LEFT JOIN GEPR_TPUNTO_SERVICIO PS
            ON PS.OID_PTO_SERVICIO = C.OID_PTO_SERVICIO
         WHERE DE.COD_ESTADO_DOCXELEMENTO = 'C'
           AND S.COD_SECTOR = par$amov_ori_cod_sector(idx)
           AND P.COD_PLANTA = par$amov_ori_cod_planta(idx)
           AND D.COD_DELEGACION = par$amov_ori_cod_delega(idx)
           AND CN.COD_CANAL = par$amov_ori_cod_canal(idx)
           AND SCN.COD_SUBCANAL = par$amov_ori_cod_subcan(idx)
           AND CL.COD_CLIENTE = par$amov_ori_cod_client(idx)
           AND NVL(SCL.COD_SUBCLIENTE, '###') =
               NVL(par$amov_ori_cod_subcli(idx), '###')
           AND NVL(PS.COD_PTO_SERVICIO, '###') =
               NVL(par$amov_ori_cod_ptserv(idx), '###')
           AND DE.OID_BULTO IS NULL;
      var$inserts   := var$inserts + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
    END LOOP;
  
    /* Validar si hay documento de salida recorrido */
    DELETE SAPR_GTT_TOIDS T
     WHERE T.COD_CALIFICADOR = 'DOCUMENTO'
       AND T.OID IN
           (SELECT DE.OID_DOCUMENTO
              FROM SAPR_TDOCUMENTOXELEMENTO DE
             INNER JOIN SAPR_GTT_TOIDS T
                ON T.OID = DE.OID_DOCUMENTO
             INNER JOIN SAPR_TBULTO B
                ON B.OID_BULTO = DE.OID_BULTO
             WHERE T.COD_CALIFICADOR = 'DOCUMENTO'
               AND B.COD_ESTADO = gepr_pcomon_###VERSION###.const$codEstadoBultoSalida);
  
    SELECT COUNT(1)
      INTO var$existe
      FROM SAPR_GTT_TOIDS T
     WHERE T.COD_CALIFICADOR = 'DOCUMENTO';
  
    IF var$existe > 0 THEN
    
      /* - Formularios */
      sapr_pformulario_###VERSION###.scargar_tt_oids_form_docs(par$inserts => var$inserts_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$registros := var$registros + var$inserts_int;
    
      /* - Terminos - Formularios */
      sapr_pformulario_###VERSION###.scargar_tt_oids_term_gpr;
      var$inserts   := var$inserts + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;
    
      /* - Cuentas */
      sapr_pcuentas_###VERSION###.scargar_tt_oids_cuentas_docs(par$inserts => var$inserts_int);
      var$inserts   := var$inserts + var$inserts_int;
      var$registros := var$registros + var$inserts_int;
    
      /* - Sectores */
      sapr_psector_###VERSION###.scargar_tt_oids_sector_docs(par$selects => var$selects_int,
                                                        par$inserts => var$inserts_int);
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
    
      /* - TABLA: Documentos */
      par$doc_rc_documentos := fcargar_rc_documento;
      var$selects           := var$selects + SQL % ROWCOUNT;
      var$registros         := var$registros + SQL % ROWCOUNT;
    
      /* - TABLA: Documentos Padres */
      par$doc_rc_documentos_padre := fcargar_rc_doc_padre;
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
    
      /* - TABLA: Valor terminos documentos */
      par$doc_rc_valor_terminos_doc := fcargar_rc_valorterminosdoc;
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
    
      /* - TABLA: Historico Documento */
      par$doc_rc_historico_mov_doc := fcargar_rc_historico;
      var$selects                  := var$selects + SQL % ROWCOUNT;
      var$registros                := var$registros + SQL % ROWCOUNT;
    
      /* - TABLA: Valores de Documentos */
      par$doc_rc_valores_documentos := fcargar_rc_valores;
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
      FOR rec$documento IN cur$documentos LOOP
        var$oids_documentos(var$oids_documentos.count) := rec$documento.oid_documento;
      END LOOP rec$termino;
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
                                                         par$usuario,
                                                         par$cod_ejecucion);
  
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
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$usuario,
                                                           par$cod_ejecucion);
    
      RAISE_APPLICATION_ERROR(-20101, 'Error ', true);
  END sverificar_documento_existe;

  /* sdel_tdocumento_elemento */
  PROCEDURE sdel_tdocumento_elemento(par$oid_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$eliminar_hijos IN BOOLEAN,
                                     par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    /* SAPR_TDOCUMENTOXELEMENTO = Eliminar registros en la tabla */
  
    IF par$eliminar_hijos THEN
    
      IF par$oid_bulto IS NOT NULL THEN
        DELETE SAPR_TDOCUMENTOXELEMENTO
         WHERE OID_DOCUMENTO = par$oid_documento
           AND OID_REMESA = par$oid_remesa
           AND OID_BULTO = par$oid_bulto;
        par$deletes := sql%rowcount;
      ELSIF par$oid_remesa IS NOT NULL THEN
        DELETE SAPR_TDOCUMENTOXELEMENTO
         WHERE OID_DOCUMENTO = par$oid_documento
           AND OID_REMESA = par$oid_remesa;
        par$deletes := sql%rowcount;
      ELSE
        DELETE SAPR_TDOCUMENTOXELEMENTO
         WHERE OID_DOCUMENTO = par$oid_documento;
        par$deletes := sql%rowcount;
      END IF;
    
    ELSE
      DELETE SAPR_TDOCUMENTOXELEMENTO
       WHERE OID_DOCUMENTO = par$oid_documento
         AND (par$oid_remesa IS NOT NULL AND OID_REMESA = par$oid_remesa OR
             par$oid_remesa IS NULL)
         AND (par$oid_bulto IS NOT NULL AND OID_BULTO = par$oid_bulto OR
             par$oid_bulto IS NULL);
    END IF;
  
    par$deletes := sql%rowcount;
  END sdel_tdocumento_elemento;

  /* sdel_tefectivoxdocumento */
  PROCEDURE sdel_tefectivoxdocumento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE sapr_tefectivoxdocumento
     WHERE OID_DOCUMENTO = par$oid_documento;
    par$deletes := sql%rowcount;
  END sdel_tefectivoxdocumento;

  /* sdel_thist_mov_documento */
  PROCEDURE sdel_thist_mov_documento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE sapr_thist_mov_documento
     WHERE OID_DOCUMENTO = par$oid_documento;
    par$deletes := sql%rowcount;
  END sdel_thist_mov_documento;

  /* sdel_tmedio_pagoxdocumento */
  PROCEDURE sdel_tmedio_pagoxdocumento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE sapr_tmedio_pagoxdocumento
     WHERE OID_DOCUMENTO = par$oid_documento;
    par$deletes := sql%rowcount;
  END sdel_tmedio_pagoxdocumento;

  /* sdel_tvalor_term_med_pago */
  PROCEDURE sdel_tvalor_term_med_pago(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE FROM sapr_tvalor_termino_medio_pago
     WHERE OID_MEDIO_PAGOXDOCUMENTO IN
           (SELECT OID_MEDIO_PAGOXDOCUMENTO
              FROM SAPR_TMEDIO_PAGOXDOCUMENTO
             WHERE OID_DOCUMENTO = par$oid_documento);
    par$deletes := sql%rowcount;
  END sdel_tvalor_term_med_pago;

  /* sdel_tvalor_terminoxdoc */
  PROCEDURE sdel_tvalor_terminoxdoc(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE sapr_tvalor_terminoxdocumento
     WHERE OID_DOCUMENTO = par$oid_documento;
    par$deletes := sql%rowcount;
  END sdel_tvalor_terminoxdoc;

  /* sins_tdocumento_elemento */
  PROCEDURE sins_tdocumento_elemento(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_remesa           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_contenedor       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_contenedor_padre IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_cont_mayor_nivel IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    par$inserts := 0;
  
    INSERT INTO sapr_tdocumentoxelemento
      (oid_documentoxelemento,
       oid_documento,
       oid_remesa,
       oid_bulto,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion,
       cod_estado_docxelemento,
       oid_contenedor,
       oid_contenedor_padre,
       oid_contenedor_mayor_nivel)
    VALUES
      (sys_guid(),
       par$oid_documento,
       par$oid_remesa,
       par$oid_bulto,
       var$gmt_zero,
       par$cod_usuario,
       var$gmt_zero,
       par$cod_usuario,
       gepr_pcomon_###VERSION###.const$CodEstadoDocEleTransito,
       par$oid_contenedor,
       par$oid_contenedor_padre,
       par$oid_cont_mayor_nivel);
  
    par$inserts := sql%rowcount;
  END sins_tdocumento_elemento;

  /* sins_tefectivoxdocumento */
  PROCEDURE sins_tefectivoxdocumento(par$oid_documento     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                     par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                     par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero          VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_unidad_medida gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_billete       gepr_pcomon_###VERSION###.tipo$nbol_;
  BEGIN
  
    IF par$oid_unidad_medida IS NULL AND
       par$cod_nivel_detalle <> gepr_pcomon_###VERSION###.const$cod_nivel_det_total THEN
    
      /* Este codigo ? temporario, para evitar erros em determinados cenarios que possa esta chegando o valor de unidade de medida vazio */
      BEGIN
        SELECT DE.BOL_BILLETE
          INTO var$bol_billete
          FROM GEPR_TDENOMINACION DE
         WHERE DE.OID_DENOMINACION = par$oid_denominacion;
      EXCEPTION
        WHEN no_data_found THEN
          var$bol_billete := 1;
      END;
    
      BEGIN
        IF var$bol_billete = 1 THEN
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
      

    MERGE INTO sapr_tefectivoxdocumento Q1
    USING (
   SELECT par$oid_documento oid_documento,  
   par$oid_divisa oid_divisa,
    par$oid_denominacion oid_denominacion,
    par$oid_unidad_medida oid_unidad_medida,
    par$cod_nivel_detalle cod_nivel_detalle,
    par$cod_tipo_efec_tot cod_tipo_efectivo_total,
   par$oid_calidad oid_calidad FROM DUAL
    
    ) Q2 
    ON (  Q1.oid_documento                    = Q2.oid_documento AND  
          Q1.oid_divisa                       = Q2.oid_divisa AND
          NVL(Q1.oid_denominacion, '-')        = NVL(Q2.oid_denominacion, '-') AND  
          NVL(Q1.oid_unidad_medida, '-')       = NVL(Q2.oid_unidad_medida, '-') AND
          Q1.cod_nivel_detalle                = Q2.cod_nivel_detalle AND  
          NVL(Q1.cod_tipo_efectivo_total, '-') = NVL(Q2.cod_tipo_efectivo_total, '-')  AND
          NVL(Q1.oid_calidad, '-')             = NVL(Q2.oid_calidad, '-')
      )   
     WHEN MATCHED THEN
       UPDATE
         SET Q1.nel_cantidad =  Q1.nel_cantidad + par$nel_cantidad,
             Q1.num_importe = Q1.num_importe + par$num_importe
      WHEN NOT MATCHED THEN 
        INSERT 
      (oid_efectivoxdocumento,
       oid_documento,
       oid_divisa,
       oid_denominacion,
       oid_unidad_medida,
       cod_nivel_detalle,
       cod_tipo_efectivo_total,
       oid_calidad,
       num_importe,
       nel_cantidad,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion
       )
    VALUES
      (SYS_GUID(),
       par$oid_documento,
       par$oid_divisa,
       par$oid_denominacion,
       var$oid_unidad_medida,
       par$cod_nivel_detalle,
       par$cod_tipo_efec_tot,
       par$oid_calidad,
       par$num_importe,
       par$nel_cantidad,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tefectivoxdocumento;

  /* sins_thist_mov_documento */
  PROCEDURE sins_thist_mov_documento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_estado    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_thist_mov_documento
      (oid_hist_mov_documento,
       oid_documento,
       cod_estado,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (sys_guid(),
       par$oid_documento,
       par$cod_estado,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_thist_mov_documento;

  /* sins_tmedio_pagoxdocumento */
  PROCEDURE sins_tmedio_pagoxdocumento(par$oid_documento     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                       par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                       par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                       par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_tmedio_pagoxdocumento
      (oid_medio_pagoxdocumento,
       oid_documento,
       oid_divisa,
       oid_medio_pago,
       cod_tipo_medio_pago,
       cod_nivel_detalle,
       num_importe,
       nel_cantidad,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_documento,
       par$oid_divisa,
       par$oid_medio_pago,
       par$cod_tipo_med_pago,
       par$cod_nivel_detalle,
       par$num_importe,
       par$nel_cantidad,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tmedio_pagoxdocumento;

  /* sins_tvalor_term_med_pago */
  PROCEDURE sins_tvalor_term_med_pago(par$oid_medio_pagoxdoc     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_termino_medio_pago IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$des_valor              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$nec_indice_grupo       IN INTEGER,
                                      par$usuario                IN gepr_pcomon_###VERSION###.tipo$usr_,
                                      par$inserts                OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_tvalor_termino_medio_pago
      (oid_valor_termino_medio_pago,
       oid_medio_pagoxdocumento,
       oid_termino_medio_pago,
       des_valor,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion,
       nec_indice_grupo)
    VALUES
      (SYS_GUID(),
       par$oid_medio_pagoxdoc,
       par$oid_termino_medio_pago,
       par$des_valor,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario,
       par$nec_indice_grupo);
    par$inserts := sql%rowcount;
  END sins_tvalor_term_med_pago;

  /* sins_tantefectivoxdocumento */
  PROCEDURE sins_tantefectivoxdocumento(par$oid_documento     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_denominacion  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_unidad_medida IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                        par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                        par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero          VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_unidad_medida gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_billete       gepr_pcomon_###VERSION###.tipo$nbol_;
  BEGIN
  
    IF par$oid_unidad_medida IS NULL AND
       par$cod_nivel_detalle <> gepr_pcomon_###VERSION###.const$cod_nivel_det_total THEN
    
      /* Este codigo ? temporario, para evitar erros em determinados cenarios que possa esta chegando o valor de unidade de medida vazio */
      BEGIN
        SELECT DE.BOL_BILLETE
          INTO var$bol_billete
          FROM GEPR_TDENOMINACION DE
         WHERE DE.OID_DENOMINACION = par$oid_denominacion;
      EXCEPTION
        WHEN no_data_found THEN
          var$bol_billete := 1;
      END;
    
      BEGIN
        IF var$bol_billete = 1 THEN
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
  
    INSERT INTO SAPR_TEFECTIVO_ANTERIORXDOC
      (OID_EFECTIVO_ANTERIORXDOC,
       oid_documento,
       oid_divisa,
       oid_denominacion,
       oid_unidad_medida,
       cod_nivel_detalle,
       cod_tipo_efectivo_total,
       oid_calidad,
       num_importe,
       nel_cantidad,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_documento,
       par$oid_divisa,
       par$oid_denominacion,
       var$oid_unidad_medida,
       par$cod_nivel_detalle,
       par$cod_tipo_efec_tot,
       par$oid_calidad,
       par$num_importe,
       par$nel_cantidad,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tantefectivoxdocumento;

  /* sins_tantmedio_pagoxdocumento */
  PROCEDURE sins_tantmedio_pagoxdocumento(par$oid_documento     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_medio_pago    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$cod_tipo_med_pago IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_nivel_detalle IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$num_importe       IN gepr_pcomon_###VERSION###.tipo$imp_,
                                          par$nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$cant_,
                                          par$usuario           IN gepr_pcomon_###VERSION###.tipo$usr_,
                                          par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TMEDIO_PAGO_ANTERIORXDOC
      (OID_MEDIO_PAGO_ANTERIORXDOC,
       oid_documento,
       oid_divisa,
       oid_medio_pago,
       cod_tipo_medio_pago,
       cod_nivel_detalle,
       num_importe,
       nel_cantidad,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_documento,
       par$oid_divisa,
       par$oid_medio_pago,
       par$cod_tipo_med_pago,
       par$cod_nivel_detalle,
       par$num_importe,
       par$nel_cantidad,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tantmedio_pagoxdocumento;

  /* sins_tantvalor_term_med_pago */
  PROCEDURE sins_tantvalor_term_med_pago(par$oid_medio_pagoxdoc     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_termino_medio_pago IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$des_valor              IN gepr_pcomon_###VERSION###.tipo$desc_,
                                         par$nec_indice_grupo       IN INTEGER,
                                         par$usuario                IN gepr_pcomon_###VERSION###.tipo$usr_,
                                         par$inserts                OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TVALOR_TERMINO_MEDIO_PAGO
      (OID_VALOR_TERMINO_MEDIO_PAGO,
       oid_medio_pagoxdocumento,
       oid_termino_medio_pago,
       des_valor,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion,
       nec_indice_grupo)
    VALUES
      (SYS_GUID(),
       par$oid_medio_pagoxdoc,
       par$oid_termino_medio_pago,
       par$des_valor,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario,
       par$nec_indice_grupo);
    par$inserts := sql%rowcount;
  END sins_tantvalor_term_med_pago;

  /* sins_tvalor_terminoxdoc */
  PROCEDURE sins_tvalor_terminoxdoc(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_termino   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$des_valor     IN gepr_pcomon_###VERSION###.tipo$desc_,
                                    par$usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_tvalor_terminoxdocumento
      (oid_valor_terminoxdocumento,
       oid_documento,
       oid_termino,
       des_valor,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (SYS_GUID(),
       par$oid_documento,
       par$oid_termino,
       par$des_valor,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_tvalor_terminoxdoc;

  /* supd_desassociar_grp */
  PROCEDURE supd_desassociar_grp(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$usuario       IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$updates       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    UPDATE sapr_tdocumento
       SET oid_grupo_documento      = '',
           des_usuario_modificacion = par$usuario,
           gmt_modificacion         = var$gmt_zero
     WHERE oid_documento = par$oid_documento;
    par$updates := sql%rowcount;
  
  END supd_desassociar_grp;

  /* supd_estado_documento */
  PROCEDURE supd_estado_documento(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                  par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                  par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido THEN
    
      UPDATE sapr_tdocumento
         SET cod_estado               = par$cod_estado_documento,
             des_usuario_modificacion = par$usuario,
             oid_documento_sustituto =
             (select d.oid_documento
                from sapr_tdocumento d
               where d.oid_documento_padre = par$oid_documento),
             gmt_modificacion         = var$gmt_zero
       WHERE oid_documento = par$oid_documento;
    ELSE
      UPDATE sapr_tdocumento
         SET cod_estado               = par$cod_estado_documento,
             des_usuario_modificacion = par$usuario,
             gmt_modificacion         = var$gmt_zero
       WHERE oid_documento = par$oid_documento;
    END IF;
  
    par$updates := sql%rowcount;
  END supd_estado_documento;

  /* supd_est_comp_documento */
  PROCEDURE supd_est_comp_documento(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_comprobante      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    UPDATE sapr_tdocumento
       SET cod_estado               = par$cod_estado_documento,
           cod_comprobante          = par$cod_comprobante,
           des_usuario_modificacion = par$usuario,
           gmt_modificacion         = var$gmt_zero
     WHERE oid_documento = par$oid_documento;
    par$updates := sql%rowcount;
  END supd_est_comp_documento;

  /* supd_tdocumento */
  PROCEDURE supd_tdocumento(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_formulario           IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_cuenta_origen        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_cuenta_destino       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_cuenta_saldo_origen  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_cuenta_saldo_destino IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_documento_padre      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_documento_sustituto  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_tipo_documento       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_sector_origen        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_sector_destino       IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_grupo_documento      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_movimentacion_fondo  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$fyh_plan_certificacion   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            par$fyh_gestion              IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            par$fyh_contable             IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            par$cod_externo              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_estado               IN gepr_pcomon_###VERSION###.tipo$est_,
                            par$usuario                  IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$rowver                   IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$esnuevo                  OUT BOOLEAN,
                            par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$updates                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_actual_id            IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_collection_id        IN gepr_pcomon_###VERSION###.tipo$desc_) IS
  
    var$rowveractual gepr_pcomon_###VERSION###.tipo$nel_;
    var$gmt_zero     VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$fyh_contable gepr_pcomon_###VERSION###.tipo$fyh_;
  BEGIN
  
    IF par$rowver IS NULL THEN
      /*es nuevo */
    
      /*verificar que no exista */
      IF NOT fexiste_documento(par$oid_documento) THEN
      
      
        IF par$fyh_contable IS NULL THEN
           var$fyh_contable := par$fyh_gestion;
        ELSE        
           var$fyh_contable := par$fyh_contable;
        END IF;
      
        /* insertarlo */
        INSERT INTO sapr_tdocumento
          (oid_documento,
           oid_formulario,
           oid_cuenta_origen,
           oid_cuenta_destino,
           oid_cuenta_saldo_origen,
           oid_cuenta_saldo_destino,
           oid_documento_padre,
           oid_documento_sustituto,
           oid_tipo_documento,
           oid_sector_origen,
           oid_sector_destino,
           oid_grupo_documento,
           oid_movimentacion_fondo,
           fyh_plan_certificacion,
           fyh_gestion,
           fec_contable,
           bol_certificado,
           cod_externo,
           cod_estado,
           gmt_creacion,
           des_usuario_creacion,
           gmt_modificacion,
           des_usuario_modificacion,
           cod_actual_id,
           cod_collection_id)
        VALUES
          (par$oid_documento,
           par$oid_formulario,
           par$oid_cuenta_origen,
           par$oid_cuenta_destino,
           par$oid_cuenta_saldo_origen,
           par$oid_cuenta_saldo_destino,
           par$oid_documento_padre,
           par$oid_documento_sustituto,
           par$oid_tipo_documento,
           par$oid_sector_origen,
           par$oid_sector_destino,
           par$oid_grupo_documento,
           par$oid_movimentacion_fondo,
           par$fyh_plan_certificacion,
           par$fyh_gestion,
           var$fyh_contable,
           0,
           par$cod_externo,
           par$cod_estado,
           var$gmt_zero,
           par$usuario,
           var$gmt_zero,
           par$usuario,
           par$cod_actual_id,
           par$cod_collection_id);
      
        par$inserts := sql%rowcount;
        par$esnuevo := true;
      
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
        FROM sapr_tdocumento
       WHERE oid_documento = par$oid_documento;
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
          UPDATE sapr_tdocumento
             SET oid_formulario           = par$oid_formulario,
                 oid_cuenta_origen        = par$oid_cuenta_origen,
                 oid_cuenta_destino       = par$oid_cuenta_destino,
                 oid_cuenta_saldo_origen  = par$oid_cuenta_saldo_origen,
                 oid_cuenta_saldo_destino = par$oid_cuenta_saldo_destino,
                 oid_documento_padre      = par$oid_documento_padre,
                 oid_documento_sustituto  = par$oid_documento_sustituto,
                 oid_tipo_documento       = par$oid_tipo_documento,
                 oid_sector_origen        = par$oid_sector_origen,
                 oid_sector_destino       = par$oid_sector_destino,
                 oid_grupo_documento      = par$oid_grupo_documento,
                 oid_movimentacion_fondo  = par$oid_movimentacion_fondo,
                 fyh_plan_certificacion   = par$fyh_plan_certificacion,
                 fyh_gestion              = par$fyh_gestion,
                 bol_certificado          = 0,
                 cod_externo              = par$cod_externo,
                 cod_estado               = par$cod_estado,
                 gmt_modificacion         = var$gmt_zero,
                 des_usuario_modificacion = par$usuario
           WHERE oid_documento = par$oid_documento;
        
          par$updates := sql%rowcount;
          par$esnuevo := false;
        
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
  END supd_tdocumento;

  /* supd_tdocumento_elemento */
  PROCEDURE supd_tdocumento_elemento(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_remesa           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_est_docxelemento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cambiar_hijos        IN BOOLEAN,
                                     par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    IF par$cambiar_hijos THEN
    
      IF par$oid_documento IS NOT NULL THEN
      
        IF par$oid_bulto IS NOT NULL THEN
        
          update sapr_tdocumentoxelemento de
             set de.cod_estado_docxelemento  = par$cod_est_docxelemento,
                 de.des_usuario_modificacion = par$cod_usuario,
                 de.gmt_modificacion         = var$gmt_zero
           where de.oid_documento = par$oid_documento
             and de.oid_remesa = par$oid_remesa
             and de.oid_bulto = par$oid_bulto;
        
          par$updates := sql%rowcount;
        
        ELSIF par$oid_remesa IS NOT NULL THEN
        
          update sapr_tdocumentoxelemento de
             set de.cod_estado_docxelemento  = par$cod_est_docxelemento,
                 de.des_usuario_modificacion = par$cod_usuario,
                 de.gmt_modificacion         = var$gmt_zero
           where de.oid_documento = par$oid_documento
             and de.oid_remesa = par$oid_remesa;
        
          par$updates := sql%rowcount;
        ELSE
        
          update sapr_tdocumentoxelemento de
             set de.cod_estado_docxelemento  = par$cod_est_docxelemento,
                 de.des_usuario_modificacion = par$cod_usuario,
                 de.gmt_modificacion         = var$gmt_zero
           where de.oid_documento = par$oid_documento;
        
          par$updates := sql%rowcount;
        END IF;
      
      END IF;
      /*    ELSE
                TO DO
      */
    END IF;
  END supd_tdocumento_elemento;

  /* scargar_tt_oids_documento_gpr */
  PROCEDURE scargar_tt_oids_documento_gpr(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$recuperar_padres    IN BOOLEAN,
                                          par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      SELECT OID_DOCUMENTO, 'DOCUMENTO'
        FROM SAPR_TDOCUMENTO
       WHERE OID_GRUPO_DOCUMENTO = par$oid_grupo_documento
         AND OID_DOCUMENTO IS NOT NULL;
    par$inserts := 0 + sql%ROWCOUNT;
  
    IF par$recuperar_padres THEN
    
      INSERT INTO SAPR_GTT_TOIDS
        (OID, COD_CALIFICADOR)
        SELECT OID_DOCUMENTO_PADRE, 'DOC_PADRE'
          FROM SAPR_TDOCUMENTO
         WHERE OID_GRUPO_DOCUMENTO = par$oid_grupo_documento
           AND OID_DOCUMENTO_PADRE IS NOT NULL;
      par$inserts := par$inserts + sql%ROWCOUNT;
    
    END IF;
  
  END scargar_tt_oids_documento_gpr;

  /* fvalidar_terminos */
  FUNCTION fvalidar_terminos(par$cod_termino IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_valor   IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$obligatorio IN gepr_pcomon_###VERSION###.tipo$nbols_,
                             par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'fvalidar_terminos';
  BEGIN
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_cod(par$cod_termino) THEN
      FOR idx IN par$cod_termino.first .. par$cod_termino.last LOOP
        IF par$obligatorio(idx) = 1 AND par$des_valor(idx) IS NULL THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'valor_termino_obrigatorio',
                                                                      const$codFuncionalidad || '.' ||
                                                                      const$nombre_func,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      par$cod_termino(idx),
                                                                      0));
          RETURN False;
        END IF;
      END LOOP;
    END IF;
  
    RETURN True;
  END fvalidar_terminos;

  /* fexiste_documento */
  FUNCTION fexiste_documento(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN IS
    var$cant INT;
  BEGIN
    SELECT COUNT(1)
      INTO var$cant
      FROM sapr_tdocumento
     WHERE oid_documento = par$oid_documento;
    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);
  END fexiste_documento;

  /* fcargar_rc_documento */
  FUNCTION fcargar_rc_documento RETURN SYS_REFCURSOR IS
    var$rcDocumentos SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rcDocumentos FOR
      SELECT D.OID_DOCUMENTO,
             D.OID_FORMULARIO,
             D.OID_CUENTA_ORIGEN,
             D.OID_CUENTA_DESTINO,
             D.OID_CUENTA_SALDO_ORIGEN,
             D.OID_CUENTA_SALDO_DESTINO,
             D.OID_DOCUMENTO_PADRE,
             D.OID_DOCUMENTO_SUSTITUTO,
             D.OID_TIPO_DOCUMENTO,
             D.OID_SECTOR_ORIGEN,
             D.OID_SECTOR_DESTINO,
             D.OID_MOVIMENTACION_FONDO,
             D.OID_GRUPO_DOCUMENTO,
             FN_GMT_DELEGACION_###VERSION###(P.OID_DELEGACION, D.FYH_PLAN_CERTIFICACION) AS FYH_PLAN_CERTIFICACION,
             D.FYH_PLAN_CERTIFICACION AS FYH_PLAN_CERTIFICACION_SINGMT,
             FN_GMT_DELEGACION_###VERSION###(P.OID_DELEGACION, D.FYH_GESTION) AS FYH_GESTION,
             D.BOL_CERTIFICADO,
             D.COD_EXTERNO,
             D.COD_ESTADO,
             D.COD_COMPROBANTE,
             D.COD_CERTIFICACION_CUENTAS,
             D.DES_MENSAJE_EXTERNO,
             D.ROWVER,
             D.GMT_CREACION,
             D.BOL_SALDO_SUPRIMIDO,
             D.BOL_NOTIFICADO
        FROM SAPR_GTT_TOIDS tmp
       INNER JOIN SAPR_TDOCUMENTO D
          ON tmp.OID = D.OID_DOCUMENTO
       INNER JOIN GEPR_TSECTOR S
          ON S.OID_SECTOR = D.OID_SECTOR_DESTINO
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = S.OID_PLANTA
       WHERE tmp.COD_CALIFICADOR = 'DOCUMENTO';
  
    RETURN var$rcDocumentos;
  
  END fcargar_rc_documento;

  /* fcargar_rc_doc_padre */
  FUNCTION fcargar_rc_doc_padre RETURN SYS_REFCURSOR IS
    var$rcDocumentosPadre SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rcDocumentosPadre FOR
      SELECT D.OID_DOCUMENTO,
             D.OID_FORMULARIO,
             D.OID_CUENTA_ORIGEN,
             D.OID_CUENTA_DESTINO,
             D.OID_CUENTA_SALDO_ORIGEN,
             D.OID_CUENTA_SALDO_DESTINO,
             D.OID_DOCUMENTO_PADRE,
             D.OID_DOCUMENTO_SUSTITUTO,
             D.OID_TIPO_DOCUMENTO,
             D.OID_SECTOR_ORIGEN,
             D.OID_SECTOR_DESTINO,
             D.OID_MOVIMENTACION_FONDO,
             D.OID_GRUPO_DOCUMENTO,
             FN_GMT_DELEGACION_###VERSION###(P.OID_DELEGACION, D.FYH_PLAN_CERTIFICACION) AS FYH_PLAN_CERTIFICACION,
             D.FYH_PLAN_CERTIFICACION AS FYH_PLAN_CERTIFICACION_SINGMT,
             FN_GMT_DELEGACION_###VERSION###(P.OID_DELEGACION, D.FYH_GESTION) AS FYH_GESTION,
             D.BOL_CERTIFICADO,
             D.COD_EXTERNO,
             D.COD_ESTADO,
             D.COD_COMPROBANTE,
             D.COD_CERTIFICACION_CUENTAS,
             D.ROWVER,
             D.GMT_CREACION,
             D.BOL_SALDO_SUPRIMIDO,
             D.BOL_NOTIFICADO
        FROM SAPR_GTT_TOIDS tmp
       INNER JOIN SAPR_TDOCUMENTO D
          ON tmp.OID = D.OID_DOCUMENTO
       INNER JOIN GEPR_TSECTOR S
          ON S.OID_SECTOR = D.OID_SECTOR_DESTINO
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = S.OID_PLANTA
       WHERE tmp.COD_CALIFICADOR = 'DOC_PADRE';
  
    RETURN var$rcDocumentosPadre;
  
  END fcargar_rc_doc_padre;

  /* fcargar_rc_valorterminosdoc */
  FUNCTION fcargar_rc_valorterminosdoc RETURN SYS_REFCURSOR IS
    var$rcValorTerminosDocumentos SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rcValorTerminosDocumentos FOR
      SELECT TD.DES_VALOR, TD.OID_DOCUMENTO, T.OID_TERMINO, T.COD_TERMINO
        FROM SAPR_TVALOR_TERMINOXDOCUMENTO TD
       INNER JOIN GEPR_TTERMINO T
          ON TD.OID_TERMINO = T.OID_TERMINO
       WHERE TD.OID_DOCUMENTO IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'DOCUMENTO'
                  OR COD_CALIFICADOR = 'DOC_PADRE');
  
    RETURN var$rcValorTerminosDocumentos;
  
  END fcargar_rc_valorterminosdoc;

  /* fcargar_rc_historico */
  FUNCTION fcargar_rc_historico RETURN SYS_REFCURSOR IS
    var$rcHistoricoMovDocumentos SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rcHistoricoMovDocumentos FOR
      SELECT OID_DOCUMENTO,
             COD_ESTADO,
             GMT_MODIFICACION,
             DES_USUARIO_MODIFICACION
        FROM SAPR_THIST_MOV_DOCUMENTO
       WHERE OID_DOCUMENTO IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'DOCUMENTO'
                  OR COD_CALIFICADOR = 'DOC_PADRE');
  
    RETURN var$rcHistoricoMovDocumentos;
  
  END fcargar_rc_historico;

  /* fcargar_rc_valores */
  FUNCTION fcargar_rc_valores RETURN SYS_REFCURSOR IS
    var$rcValoresDocumentos SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rcValoresDocumentos FOR
      WITH doc AS
       (SELECT DISTINCT OID OID_DOCUMENTO
          FROM SAPR_GTT_TOIDS
         WHERE COD_CALIFICADOR = 'DOCUMENTO'
            OR COD_CALIFICADOR = 'DOC_PADRE'),
      detalle as
       (SELECT e.OID_DOCUMENTO,
               OID_DIVISA,
               OID_DENOMINACION,
               NULL AS OID_MEDIO_PAGO,
               NULL AS COD_TIPO_MEDIO_PAGO,
               COD_NIVEL_DETALLE,
               OID_UNIDAD_MEDIDA,
               OID_CALIDAD,
               NUM_IMPORTE,
               NEL_CANTIDAD,
               CASE
                 WHEN NUM_IMPORTE > 0 THEN
                  1
                 ELSE
                  0
               END AS BOL_POSITIVO
          FROM doc
         INNER JOIN SAPR_TEFECTIVOXDOCUMENTO e
            ON doc.OID_DOCUMENTO = e.OID_DOCUMENTO
        UNION ALL
        SELECT m.OID_DOCUMENTO,
               OID_DIVISA,
               NULL AS OID_DENOMINACION,
               OID_MEDIO_PAGO,
               COD_TIPO_MEDIO_PAGO,
               COD_NIVEL_DETALLE,
               NULL AS OID_UNIDAD_MEDIDA,
               NULL AS OID_CALIDAD,
               NUM_IMPORTE,
               NEL_CANTIDAD,
               CASE
                 WHEN NUM_IMPORTE > 0 THEN
                  1
                 ELSE
                  0
               END AS BOL_POSITIVO
          FROM doc
         INNER JOIN SAPR_TMEDIO_PAGOXDOCUMENTO m
            ON doc.OID_DOCUMENTO = m.OID_DOCUMENTO)
      SELECT OID_DOCUMENTO,
             OID_DIVISA,
             OID_DENOMINACION,
             OID_MEDIO_PAGO,
             COD_TIPO_MEDIO_PAGO,
             COD_NIVEL_DETALLE,
             OID_UNIDAD_MEDIDA,
             OID_CALIDAD,
             0 AS BOL_DISPONIBLE,
             SUM(NUM_IMPORTE) AS IMPORTE,
             SUM(NEL_CANTIDAD) AS CANTIDAD,
             BOL_POSITIVO
        FROM detalle
       GROUP BY OID_DOCUMENTO,
                OID_DIVISA,
                OID_DENOMINACION,
                OID_MEDIO_PAGO,
                COD_TIPO_MEDIO_PAGO,
                COD_NIVEL_DETALLE,
                OID_UNIDAD_MEDIDA,
                OID_CALIDAD,
                BOL_POSITIVO;
  
    RETURN var$rcValoresDocumentos;
  
  END fcargar_rc_valores;

  /* srecupera_dt_deposito_ripley */
  PROCEDURE srecupera_dt_deposito_ripley(par$cod_cultura            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$numero_deposito        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$rc_DatosDepositoRipley OUT SYS_REFCURSOR) AS
  BEGIN
  
    /* Validacoes */
    IF par$numero_deposito IS NULL THEN
      RAISE_APPLICATION_ERROR(-20001,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MSG_VALIDAR_NUMERO_DEPOSITO',
                                                                  'sp_recuperaDatosDepositoRipley',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  0));
    END IF;
  
    /* Busca Dados */
    OPEN par$rc_DatosDepositoRipley FOR
      SELECT VALTERM.OID_DOCUMENTO,
             DE_ORI.COD_DELEGACION COD_DELEGACION_ORI,
             DE_DES.COD_DELEGACION COD_DELEGACION_DES
        FROM SAPR_TVALOR_TERMINOXDOCUMENTO VALTERM
       INNER JOIN GEPR_TTERMINO TERM
          ON VALTERM.OID_TERMINO = TERM.OID_TERMINO
       INNER JOIN SAPR_TDOCUMENTO D
          ON D.OID_DOCUMENTO = VALTERM.OID_DOCUMENTO
       INNER JOIN GEPR_TSECTOR S_ORI
          ON S_ORI.OID_SECTOR = D.OID_SECTOR_ORIGEN
       INNER JOIN GEPR_TPLANTA P_ORI
          ON P_ORI.OID_PLANTA = S_ORI.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION DE_ORI
          ON DE_ORI.OID_DELEGACION = P_ORI.OID_DELEGACION
       INNER JOIN GEPR_TSECTOR S_DES
          ON S_DES.OID_SECTOR = D.OID_SECTOR_DESTINO
       INNER JOIN GEPR_TPLANTA P_DES
          ON P_DES.OID_PLANTA = S_DES.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION DE_DES
          ON DE_DES.OID_DELEGACION = P_DES.OID_DELEGACION
       WHERE TERM.COD_TERMINO = 'NUMERO_DEPOSITO'
         AND VALTERM.DES_VALOR = par$numero_deposito;
  
  END srecupera_dt_deposito_ripley;

  /* sactualiza_desc_mensaje */
  PROCEDURE sactualiza_desc_mensaje(par$cod_cultura   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$des_mensaje   IN gepr_pcomon_###VERSION###.tipo$obs_) IS
  BEGIN
  
    /* Validacoes */
    IF par$oid_documento IS NULL THEN
      RAISE_APPLICATION_ERROR(-20001,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MSG_VALIDAR_ID_DOCUMENTO',
                                                                  'sp_actualizaDesMensaje',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  0));
    END IF;
  
    /* Actualiza tabla */
    UPDATE SAPR_TDOCUMENTO
       SET DES_MENSAJE_EXTERNO = par$des_mensaje
     WHERE OID_DOCUMENTO = par$oid_documento;
  
  END sactualiza_desc_mensaje;
  /* sactualiza_desc_mensaje */
  PROCEDURE svalidar_cuentas(par$oid_formulario          IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_canal       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_canal       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_subcan      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_delega      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_delega      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_planta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_planta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_cod_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_cod_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_canal       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_canal       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_subcan      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_subcan      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_delega      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_delega      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_planta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_planta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_cod_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_cod_client      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_cod_subcli      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_cod_ptserv      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_cuenta_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_ori_oid_sector_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_ori_oid_cuenta_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_cuenta_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$mov_des_oid_sector_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$sal_des_oid_cuenta_resp OUT gepr_pcomon_###VERSION###.tipo$oid_,
                             par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$usr_,
                             par$inserts                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$updates                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$selects                 OUT gepr_pcomon_###VERSION###.tipo$nel_,
                             par$esreenvio               OUT BOOLEAN) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_cuentas';
  
    /* variables locales */
    var$inicio        gepr_pcomon_###VERSION###.tipo$gmt_;
    var$inserts_int   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err           VARCHAR2(4000);
    var$cod_ejecucion gepr_pcomon_###VERSION###.tipo$nel_;
  
    var$cuenta_ori_mov gepr_pcomon_###VERSION###.tipo$cuenta;
    var$cuenta_ori_sal gepr_pcomon_###VERSION###.tipo$cuenta;
    var$cuenta_des_mov gepr_pcomon_###VERSION###.tipo$cuenta;
    var$cuenta_des_sal gepr_pcomon_###VERSION###.tipo$cuenta;
  
  BEGIN
  
    var$inicio := SYSTIMESTAMP;
  
    BEGIN
      /* Inicio: Cuenta de origen */
    
      /* Recuperar identificador cuenta movimiento */
      var$cuenta_ori_mov.oid_cuenta       := par$mov_ori_oid_cuenta;
      var$cuenta_ori_mov.oid_cliente      := par$mov_ori_oid_client;
      var$cuenta_ori_mov.cod_cliente      := par$mov_ori_cod_client;
      var$cuenta_ori_mov.oid_subcliente   := par$mov_ori_oid_subcli;
      var$cuenta_ori_mov.cod_subcliente   := par$mov_ori_cod_subcli;
      var$cuenta_ori_mov.oid_pto_servicio := par$mov_ori_oid_ptserv;
      var$cuenta_ori_mov.cod_pto_servicio := par$mov_ori_cod_ptserv;
      var$cuenta_ori_mov.oid_canal        := par$mov_ori_oid_canal;
      var$cuenta_ori_mov.cod_canal        := par$mov_ori_cod_canal;
      var$cuenta_ori_mov.oid_subcanal     := par$mov_ori_oid_subcan;
      var$cuenta_ori_mov.cod_subcanal     := par$mov_ori_cod_subcan;
      var$cuenta_ori_mov.oid_delegacion   := par$mov_ori_oid_delega;
      var$cuenta_ori_mov.cod_delegacion   := par$mov_ori_cod_delega;
      var$cuenta_ori_mov.oid_planta       := par$mov_ori_oid_planta;
      var$cuenta_ori_mov.cod_planta       := par$mov_ori_cod_planta;
      var$cuenta_ori_mov.oid_sector       := par$mov_ori_oid_sector;
      var$cuenta_ori_mov.cod_sector       := par$mov_ori_cod_sector;
    
      /* Recuperar identificador cuenta saldo */
      var$cuenta_ori_sal.oid_cuenta       := par$sal_ori_oid_cuenta;
      var$cuenta_ori_sal.oid_cliente      := par$sal_ori_oid_client;
      var$cuenta_ori_sal.cod_cliente      := par$sal_ori_cod_client;
      var$cuenta_ori_sal.oid_subcliente   := par$sal_ori_oid_subcli;
      var$cuenta_ori_sal.cod_subcliente   := par$sal_ori_cod_subcli;
      var$cuenta_ori_sal.oid_pto_servicio := par$sal_ori_oid_ptserv;
      var$cuenta_ori_sal.cod_pto_servicio := par$sal_ori_cod_ptserv;
      var$cuenta_ori_sal.oid_canal        := par$mov_ori_oid_canal;
      var$cuenta_ori_sal.cod_canal        := par$mov_ori_cod_canal;
      var$cuenta_ori_sal.oid_subcanal     := par$mov_ori_oid_subcan;
      var$cuenta_ori_sal.cod_subcanal     := par$mov_ori_cod_subcan;
      var$cuenta_ori_sal.oid_delegacion   := par$mov_ori_oid_delega;
      var$cuenta_ori_sal.cod_delegacion   := par$mov_ori_cod_delega;
      var$cuenta_ori_sal.oid_planta       := par$mov_ori_oid_planta;
      var$cuenta_ori_sal.cod_planta       := par$mov_ori_cod_planta;
      var$cuenta_ori_sal.oid_sector       := par$mov_ori_oid_sector;
      var$cuenta_ori_sal.cod_sector       := par$mov_ori_cod_sector;
    
      --Se noo passou a conta saldos (tem que ter passado o?par$sal_ori_oid_cuenta?ou?par$sal_ori_oid_client)
      IF (par$sal_ori_oid_cuenta is null and par$sal_ori_oid_client is null) THEN
        -- Se for cierre
        IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario,
                                                              gepr_pcomon_###VERSION###.const$CodCaracCierreCaja) THEN
          -- ent?o copiar a conta origen para a conta saldo.
          var$cuenta_ori_sal.oid_cuenta       := par$mov_ori_oid_cuenta;
          var$cuenta_ori_sal.oid_cliente      := par$mov_ori_oid_client;
          var$cuenta_ori_sal.cod_cliente      := par$mov_ori_cod_client;
          var$cuenta_ori_sal.oid_subcliente   := par$mov_ori_oid_subcli;
          var$cuenta_ori_sal.cod_subcliente   := par$mov_ori_cod_subcli;
          var$cuenta_ori_sal.oid_pto_servicio := par$mov_ori_oid_ptserv;
          var$cuenta_ori_sal.cod_pto_servicio := par$mov_ori_cod_ptserv;
        END IF;
      END IF;
    
      sapr_pcuentas_###VERSION###.srecuperar_cuenta(par$cuenta_mov     => var$cuenta_ori_mov,
                                               par$cuenta_sal     => var$cuenta_ori_sal,
                                               par$oid_formulario => par$oid_formulario,
                                               par$cod_usuario    => par$cod_usuario,
                                               par$cod_cultura    => par$cod_cultura,
                                               par$inserts        => var$inserts_int,
                                               par$updates        => var$updates_int,
                                               par$selects        => var$selects_int);
    
      par$inserts := par$inserts + var$inserts_int;
      par$updates := par$updates + var$updates_int;
      par$selects := par$selects + var$selects_int;
    
      par$mov_ori_oid_cuenta_resp := var$cuenta_ori_mov.oid_cuenta;
      par$mov_ori_oid_sector_resp := var$cuenta_ori_mov.oid_sector;
      par$sal_ori_oid_cuenta_resp := var$cuenta_ori_sal.oid_cuenta;
    
    EXCEPTION
      WHEN OTHERS THEN
      
        var$err := gepr_putilidades_###VERSION###.fformat_error;
        gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                             const$nombre_func ||
                                                             '.CuentaOrigen',
                                                             0,
                                                             0,
                                                             0,
                                                             0,
                                                             0,
                                                             0,
                                                             var$inicio,
                                                             null,
                                                             'R',
                                                             '',
                                                             var$err,
                                                             par$cod_usuario,
                                                             var$cod_ejecucion);
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                'Error(' || var$cod_ejecucion || '): ' ||
                                const$codFuncionalidad || '.' ||
                                const$nombre_func,
                                true);
    END;
    /* Fin: Cuenta de origen */
  
    BEGIN
      /* Inicio: Cuenta de destino */
    
      IF NVL(par$mov_ori_oid_cuenta, '###') <>
         NVL(par$mov_des_oid_cuenta, '###') OR
         NVL(par$mov_ori_oid_client, '###') <>
         NVL(par$mov_des_oid_client, '###') OR
         NVL(par$mov_ori_cod_client, '###') <>
         NVL(par$mov_des_cod_client, '###') OR
         NVL(par$mov_ori_oid_subcli, '###') <>
         NVL(par$mov_des_oid_subcli, '###') OR
         NVL(par$mov_ori_cod_subcli, '###') <>
         NVL(par$mov_des_cod_subcli, '###') OR
         NVL(par$mov_ori_oid_ptserv, '###') <>
         NVL(par$mov_des_oid_ptserv, '###') OR
         NVL(par$mov_ori_cod_ptserv, '###') <>
         NVL(par$mov_des_cod_ptserv, '###') OR
         NVL(par$mov_ori_oid_canal, '###') <>
         NVL(par$mov_des_oid_canal, '###') OR
         NVL(par$mov_ori_cod_canal, '###') <>
         NVL(par$mov_des_cod_canal, '###') OR
         NVL(par$mov_ori_oid_subcan, '###') <>
         NVL(par$mov_des_oid_subcan, '###') OR
         NVL(par$mov_ori_cod_subcan, '###') <>
         NVL(par$mov_des_cod_subcan, '###') OR
         NVL(par$mov_ori_oid_delega, '###') <>
         NVL(par$mov_des_oid_delega, '###') OR
         NVL(par$mov_ori_cod_delega, '###') <>
         NVL(par$mov_des_cod_delega, '###') OR
         NVL(par$mov_ori_oid_planta, '###') <>
         NVL(par$mov_des_oid_planta, '###') OR
         NVL(par$mov_ori_cod_planta, '###') <>
         NVL(par$mov_des_cod_planta, '###') OR
         NVL(par$mov_ori_oid_sector, '###') <>
         NVL(par$mov_des_oid_sector, '###') OR
         NVL(par$mov_ori_cod_sector, '###') <>
         NVL(par$mov_des_cod_sector, '###') OR
         NVL(par$sal_ori_oid_cuenta, '###') <>
         NVL(par$sal_des_oid_cuenta, '###') OR
         NVL(par$sal_ori_oid_client, '###') <>
         NVL(par$sal_des_oid_client, '###') OR
         NVL(par$sal_ori_cod_client, '###') <>
         NVL(par$sal_des_cod_client, '###') OR
         NVL(par$sal_ori_oid_subcli, '###') <>
         NVL(par$sal_des_oid_subcli, '###') OR
         NVL(par$sal_ori_cod_subcli, '###') <>
         NVL(par$sal_des_cod_subcli, '###') OR
         NVL(par$sal_ori_oid_ptserv, '###') <>
         NVL(par$sal_des_oid_ptserv, '###') OR
         NVL(par$sal_ori_cod_ptserv, '###') <>
         NVL(par$sal_des_cod_ptserv, '###') THEN
      
        /* Recuperar identificador cuenta movimiento */
        var$cuenta_des_mov.oid_cuenta       := par$mov_des_oid_cuenta;
        var$cuenta_des_mov.oid_cliente      := par$mov_des_oid_client;
        var$cuenta_des_mov.cod_cliente      := par$mov_des_cod_client;
        var$cuenta_des_mov.oid_subcliente   := par$mov_des_oid_subcli;
        var$cuenta_des_mov.cod_subcliente   := par$mov_des_cod_subcli;
        var$cuenta_des_mov.oid_pto_servicio := par$mov_des_oid_ptserv;
        var$cuenta_des_mov.cod_pto_servicio := par$mov_des_cod_ptserv;
        var$cuenta_des_mov.oid_canal        := par$mov_des_oid_canal;
        var$cuenta_des_mov.cod_canal        := par$mov_des_cod_canal;
        var$cuenta_des_mov.oid_subcanal     := par$mov_des_oid_subcan;
        var$cuenta_des_mov.cod_subcanal     := par$mov_des_cod_subcan;
        var$cuenta_des_mov.oid_delegacion   := par$mov_des_oid_delega;
        var$cuenta_des_mov.cod_delegacion   := par$mov_des_cod_delega;
        var$cuenta_des_mov.oid_planta       := par$mov_des_oid_planta;
        var$cuenta_des_mov.cod_planta       := par$mov_des_cod_planta;
        var$cuenta_des_mov.oid_sector       := par$mov_des_oid_sector;
        var$cuenta_des_mov.cod_sector       := par$mov_des_cod_sector;
      
        /* Recuperar identificador cuenta saldo */
        var$cuenta_des_sal.oid_cuenta       := par$sal_des_oid_cuenta;
        var$cuenta_des_sal.oid_cliente      := par$sal_des_oid_client;
        var$cuenta_des_sal.cod_cliente      := par$sal_des_cod_client;
        var$cuenta_des_sal.oid_subcliente   := par$sal_des_oid_subcli;
        var$cuenta_des_sal.cod_subcliente   := par$sal_des_cod_subcli;
        var$cuenta_des_sal.oid_pto_servicio := par$sal_des_oid_ptserv;
        var$cuenta_des_sal.cod_pto_servicio := par$sal_des_cod_ptserv;
        var$cuenta_des_sal.oid_canal        := par$mov_des_oid_canal;
        var$cuenta_des_sal.cod_canal        := par$mov_des_cod_canal;
        var$cuenta_des_sal.oid_subcanal     := par$mov_des_oid_subcan;
        var$cuenta_des_sal.cod_subcanal     := par$mov_des_cod_subcan;
        var$cuenta_des_sal.oid_delegacion   := par$mov_des_oid_delega;
        var$cuenta_des_sal.cod_delegacion   := par$mov_des_cod_delega;
        var$cuenta_des_sal.oid_planta       := par$mov_des_oid_planta;
        var$cuenta_des_sal.cod_planta       := par$mov_des_cod_planta;
        var$cuenta_des_sal.oid_sector       := par$mov_des_oid_sector;
        var$cuenta_des_sal.cod_sector       := par$mov_des_cod_sector;
      
        sapr_pcuentas_###VERSION###.srecuperar_cuenta(par$cuenta_mov     => var$cuenta_des_mov,
                                                 par$cuenta_sal     => var$cuenta_des_sal,
                                                 par$oid_formulario => par$oid_formulario,
                                                 par$cod_usuario    => par$cod_usuario,
                                                 par$cod_cultura    => par$cod_cultura,
                                                 par$inserts        => var$inserts_int,
                                                 par$updates        => var$updates_int,
                                                 par$selects        => var$selects_int);
      
        par$inserts := par$inserts + var$inserts_int;
        par$updates := par$updates + var$updates_int;
        par$selects := par$selects + var$selects_int;
      
        par$mov_des_oid_cuenta_resp := var$cuenta_des_mov.oid_cuenta;
        par$mov_des_oid_sector_resp := var$cuenta_des_mov.oid_sector;
        par$sal_des_oid_cuenta_resp := var$cuenta_des_sal.oid_cuenta;
      
        /* Reenvio */
        par$esreenvio := true;
      
      ELSE
      
        par$mov_des_oid_cuenta_resp := par$mov_ori_oid_cuenta_resp;
        par$mov_des_oid_sector_resp := par$mov_ori_oid_sector_resp;
        par$sal_des_oid_cuenta_resp := par$sal_ori_oid_cuenta_resp;
        par$esreenvio               := false;
      
      END IF;
    
    EXCEPTION
      WHEN OTHERS THEN
      
        var$err := gepr_putilidades_###VERSION###.fformat_error;
        gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                             const$nombre_func ||
                                                             '.CuentaDestino',
                                                             0,
                                                             0,
                                                             0,
                                                             0,
                                                             0,
                                                             0,
                                                             var$inicio,
                                                             null,
                                                             'R',
                                                             '',
                                                             var$err,
                                                             par$cod_usuario,
                                                             var$cod_ejecucion);
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                'Error(' || var$cod_ejecucion || '): ' ||
                                const$codFuncionalidad || '.' ||
                                const$nombre_func,
                                true);
    END;
    /* Fin: Cuenta de destino */
  
    /*JIRA 3530- validar se gestion de GESTION DE FUNDOS-MOVIMENTOS FUNDOS*/
    IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                          par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionFondos) AND
       sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                          par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracMovFondos) THEN
      /*ENTRE SECTORES*/
      IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                            par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvSecMismaPla) OR
         sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                            par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvSecDistPla) THEN
      
        IF (par$mov_ori_oid_sector = par$mov_des_oid_sector) or
           (var$cuenta_ori_mov.oid_sector = var$cuenta_des_mov.oid_sector) THEN
        
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'origen_destino_iguales',
                                                                      'GestionFondos.' ||
                                                                      const$nombre_func,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      null,
                                                                      1));
        END IF;
      
      elsif (sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                                par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvioClientes)) then
        /*OS CLIENTES DE ORIGEM E DESTIONO NAO PODEM SER IGUAIS*/
        IF (par$mov_ori_oid_client = par$mov_des_oid_client) or
           (var$cuenta_ori_mov.oid_cliente = var$cuenta_des_mov.oid_cliente) THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'origen_destino_iguales',
                                                                      'GestionFondos.' ||
                                                                      const$nombre_func,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      null,
                                                                      1));
        END IF;
      
      elsif (sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                                par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracEntreCanales)) then
        /*OS CANAIS DE ORIGEM E DESTIONO NAO PODEM SER IGUAIS*/
        IF (par$mov_ori_oid_canal = par$mov_des_oid_canal) or
           (var$cuenta_ori_mov.oid_canal = var$cuenta_des_mov.oid_canal) THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'origen_destino_iguales',
                                                                      'GestionFondos.' ||
                                                                      const$nombre_func,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      null,
                                                                      1));
        END IF;
      
      END IF;
    END IF;
  END svalidar_cuentas;

  /* srecuperar_docs_certificados */
  PROCEDURE srecuperar_docs_certificados(par$cod_certificado           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_ajeno                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$bol_detallar_desgloses    IN INTEGER,
                                         par$bol_campos_adicionales    IN INTEGER,
                                         par$doc_rc_documentos         OUT sys_refcursor,
                                         par$doc_rc_cuentas            OUT sys_refcursor,
                                         par$doc_rc_valores_documentos OUT sys_refcursor,
                                         par$doc_rc_valor_terminos_doc OUT sys_refcursor,
                                         par$usuario                   IN VARCHAR2,
                                         par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,

                                         par$info_ejecucion            IN VARCHAR2,
                                         par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_docs_certificados';

    var$inicio         TIMESTAMP;
    var$parcial        TIMESTAMP;
    var$registros      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges         gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err            VARCHAR2(4000);
    var$cod_ejecucion  gepr_pcomon_###VERSION###.tipo$nel_;
    var$oid_certificado gepr_pcomon_###VERSION###.tipo$oid_;
    var$count INTEGER;
  
  BEGIN

    var$inicio := SYSTIMESTAMP;
    
    /* limpiar tabla temporaria */
    delete sapr_gtt_toids;
    var$deletes   := var$deletes + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;

    /* ===== Recuperar identificador certificado ==== */
    SELECT C.OID_CERTIFICADO
      INTO var$oid_certificado
      FROM SAPR_TCERTIFICADO C
     WHERE C.COD_CERTIFICADO = par$cod_certificado;
  
  
    /* ===== Cargar tabla temporaria ==== */
    /* - Documentos */
    var$parcial := SYSTIMESTAMP;
    var$cod_ejecucion := null;
    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      SELECT TE.OID_DOCUMENTO, 'DOCUMENTO'
        FROM SAPR_TTRANSACCION_EFECTIVO TE
       WHERE TE.OID_CERTIFICADO = var$oid_certificado
      UNION ALL
      SELECT MP.OID_DOCUMENTO, 'DOCUMENTO'
        FROM SAPR_TTRANSACCION_MEDIO_PAGO MP
       WHERE MP.OID_CERTIFICADO = var$oid_certificado;
    var$inserts   := var$inserts + SQL % ROWCOUNT;
    var$registros := var$registros + SQL % ROWCOUNT;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar tabla temporaria : Documentos', '', par$usuario, var$cod_ejecucion);
    
 select COUNT(1) into var$count from SAPR_GTT_TOIDS t where COD_CALIFICADOR = 'DOCUMENTO';
 DBMS_OUTPUT.put_line(var$count || ' Documentos');

    /* - Cuentas */
    var$parcial := SYSTIMESTAMP;
    var$cod_ejecucion := null;
    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
      (SELECT D.OID_CUENTA_SALDO_ORIGEN, D.OID_CUENTA_SALDO_DESTINO, D.OID_FORMULARIO
        FROM SAPR_TDOCUMENTO D
       WHERE D.OID_DOCUMENTO IN (SELECT T.OID FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID))
       SELECT DISTINCT OID_CUENTA, 'CUENTA'
        FROM (SELECT OID_CUENTA_SALDO_ORIGEN OID_CUENTA FROM q
              UNION ALL
              SELECT OID_CUENTA_SALDO_DESTINO OID_CUENTA FROM q);
    var$inserts   := var$inserts + SQL % ROWCOUNT;
    var$registros := var$registros + SQL % ROWCOUNT;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar tabla temporaria : Cuentas', '', par$usuario, var$cod_ejecucion);

 select COUNT(1) into var$count from SAPR_GTT_TOIDS t where COD_CALIFICADOR = 'CUENTA';
 DBMS_OUTPUT.put_line(var$count || ' Cuentas');
 
    /* ===== Cargar refcursor ==== */

    /* - TABLA: Documentos */
    var$parcial := SYSTIMESTAMP;
    var$cod_ejecucion := null;
     OPEN par$doc_rc_documentos FOR
      SELECT /* certificado */ D.OID_DOCUMENTO, D.OID_CUENTA_SALDO_ORIGEN, D.OID_CUENTA_SALDO_DESTINO,
             D.FYH_PLAN_CERTIFICACION,D.FYH_GESTION AS FYH_GESTION, D.COD_EXTERNO, D.COD_COMPROBANTE,
             D.GMT_CREACION, F.OID_FORMULARIO, F.OID_IAC_INDIVIDUAL, F.COD_FORMULARIO, F.DES_FORMULARIO
        FROM SAPR_TDOCUMENTO D
       INNER JOIN SAPR_TFORMULARIO F
          ON F.OID_FORMULARIO = D.OID_FORMULARIO
       WHERE D.OID_DOCUMENTO IN (SELECT T.OID FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID);
    var$selects   := var$selects + SQL % ROWCOUNT;
    var$registros := var$registros + SQL % ROWCOUNT;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar refcursor : Documentos', '', par$usuario, var$cod_ejecucion);
    
IF par$cod_ajeno IS NOT NULL THEN

    /* - TABLA: Cuentas */
    var$parcial := SYSTIMESTAMP;
    var$cod_ejecucion := null;
    OPEN par$doc_rc_cuentas FOR
      SELECT CU.OID_CUENTA,
             NVL(CACLI.COD_AJENO, CL.COD_CLIENTE) COD_CLIENTE,
             NVL(CACLI.DES_AJENO, CL.DES_CLIENTE) DES_CLIENTE,
             NVL(CASUBCLI.COD_AJENO, SCL.COD_SUBCLIENTE) COD_SUBCLIENTE,
             NVL(CASUBCLI.DES_AJENO, SCL.DES_SUBCLIENTE) DES_SUBCLIENTE,
             NVL(CAPTO.COD_AJENO, PTO.COD_PTO_SERVICIO) COD_PTO_SERVICIO,
             NVL(CAPTO.DES_AJENO, PTO.DES_PTO_SERVICIO) DES_PTO_SERVICIO,
             NVL(CACAN.COD_AJENO, CAN.COD_CANAL) COD_CANAL,
             NVL(CACAN.DES_AJENO, CAN.DES_CANAL) DES_CANAL,
             NVL(CASBC.COD_AJENO, SBC.COD_SUBCANAL) COD_SUBCANAL,
             NVL(CASBC.DES_AJENO, SBC.DES_SUBCANAL) DES_SUBCANAL,
             NVL(CASEC.COD_AJENO, SE.COD_SECTOR) COD_SECTOR,
             NVL(CASEC.DES_AJENO, SE.DES_SECTOR) DES_SECTOR,
             D.COD_PAIS,
             NVL(CADEL.COD_AJENO, D.COD_DELEGACION) COD_DELEGACION,
             NVL(CADEL.DES_AJENO, D.DES_DELEGACION) DES_DELEGACION,
             NVL(CAPLA.COD_AJENO, P.COD_PLANTA) COD_PLANTA,
             NVL(CAPLA.DES_AJENO, P.DES_PLANTA) DES_PLANTA
        FROM SAPR_TCUENTA CU
       INNER JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CU.OID_CLIENTE

       LEFT JOIN GEPR_TCODIGO_AJENO CACLI ON CACLI.OID_TABLA_GENESIS = CU.OID_CLIENTE AND CACLI.COD_IDENTIFICADOR = par$cod_ajeno AND CACLI.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
       
       INNER JOIN GEPR_TSUBCANAL SBC
          ON SBC.OID_SUBCANAL = CU.OID_SUBCANAL

          LEFT JOIN GEPR_TCODIGO_AJENO CASBC ON CASBC.OID_TABLA_GENESIS = CU.OID_SUBCANAL AND CASBC.COD_IDENTIFICADOR = par$cod_ajeno AND CASBC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL'

       INNER JOIN GEPR_TCANAL CAN
          ON CAN.OID_CANAL = SBC.OID_CANAL

          LEFT JOIN GEPR_TCODIGO_AJENO CACAN ON CACAN.OID_TABLA_GENESIS = SBC.OID_CANAL AND CACAN.COD_IDENTIFICADOR = par$cod_ajeno AND CACAN.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL'

       INNER JOIN GEPR_TSECTOR SE
          ON SE.OID_SECTOR = CU.OID_SECTOR

          LEFT JOIN GEPR_TCODIGO_AJENO CASEC ON CASEC.OID_TABLA_GENESIS = CU.OID_SECTOR AND CASEC.COD_IDENTIFICADOR = par$cod_ajeno AND CASEC.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR'

       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = SE.OID_PLANTA

          LEFT JOIN GEPR_TCODIGO_AJENO CAPLA ON CAPLA.OID_TABLA_GENESIS = SE.OID_PLANTA AND CAPLA.COD_IDENTIFICADOR = par$cod_ajeno AND CAPLA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA'

       INNER JOIN GEPR_TDELEGACION D
          ON D.OID_DELEGACION = P.OID_DELEGACION

          LEFT JOIN GEPR_TCODIGO_AJENO CADEL ON CADEL.OID_TABLA_GENESIS = P.OID_DELEGACION AND CADEL.COD_IDENTIFICADOR = par$cod_ajeno AND CADEL.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION'

        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE

          LEFT JOIN GEPR_TCODIGO_AJENO CASUBCLI ON CASUBCLI.OID_TABLA_GENESIS = CU.OID_SUBCLIENTE AND CASUBCLI.COD_IDENTIFICADOR = par$cod_ajeno AND CASUBCLI.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'

        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
          ON PTO.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO

          LEFT JOIN GEPR_TCODIGO_AJENO CAPTO ON CAPTO.OID_TABLA_GENESIS = CU.OID_PTO_SERVICIO AND CAPTO.COD_IDENTIFICADOR = par$cod_ajeno AND CAPTO.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'

       WHERE CU.OID_CUENTA IN (SELECT T.OID FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'CUENTA' GROUP BY T.OID);
    var$selects   := var$selects + SQL % ROWCOUNT;
    var$registros := var$registros + SQL % ROWCOUNT;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar refcursor : Cuentas', '', par$usuario, var$cod_ejecucion);

ELSE

    /* - TABLA: Cuentas */
    var$parcial := SYSTIMESTAMP;
    var$cod_ejecucion := null;
    OPEN par$doc_rc_cuentas FOR
      SELECT CU.OID_CUENTA,
             CL.COD_CLIENTE,
             CL.DES_CLIENTE,
             SCL.COD_SUBCLIENTE,
             SCL.DES_SUBCLIENTE,
             PTO.COD_PTO_SERVICIO,
             PTO.DES_PTO_SERVICIO,
             CAN.COD_CANAL,
             CAN.DES_CANAL,
             SBC.COD_SUBCANAL,
             SBC.DES_SUBCANAL,
             SE.COD_SECTOR,
             SE.DES_SECTOR,
             D.COD_PAIS,
             D.COD_DELEGACION,
             D.DES_DELEGACION,
             P.COD_PLANTA,
             P.DES_PLANTA
        FROM SAPR_TCUENTA CU
       INNER JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CU.OID_CLIENTE
       INNER JOIN GEPR_TSUBCANAL SBC
          ON SBC.OID_SUBCANAL = CU.OID_SUBCANAL
       INNER JOIN GEPR_TCANAL CAN
          ON CAN.OID_CANAL = SBC.OID_CANAL
       INNER JOIN GEPR_TSECTOR SE
          ON SE.OID_SECTOR = CU.OID_SECTOR
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = SE.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION D
          ON D.OID_DELEGACION = P.OID_DELEGACION
        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
          ON PTO.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
       WHERE CU.OID_CUENTA IN (SELECT T.OID FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'CUENTA' GROUP BY T.OID);
    var$selects   := var$selects + SQL % ROWCOUNT;
    var$registros := var$registros + SQL % ROWCOUNT;

END IF;

IF par$cod_ajeno IS NOT NULL THEN

/* - TABLA: Valores de Documentos */
    IF par$bol_detallar_desgloses > 0 THEN

      /* - TABLA: Valores de Documentos - Efectivos */
      var$parcial := SYSTIMESTAMP;
      var$cod_ejecucion := null;
      OPEN par$doc_rc_valores_documentos FOR
        SELECT E.OID_DOCUMENTO,
               NVL(CADIV.COD_AJENO, DIV.COD_ISO_DIVISA) COD_ISO_DIVISA,
               NVL(CADEN.COD_AJENO, DEN.COD_DENOMINACION) COD_DENOMINACION,
               NULL AS COD_MEDIO_PAGO,
               NULL AS COD_TIPO_MEDIO_PAGO,
               NULL AS DES_MEDIO_PAGO,
               COD_NIVEL_DETALLE,
               OID_UNIDAD_MEDIDA,
               OID_CALIDAD,
               SUM(NUM_IMPORTE) AS IMPORTE,
               SUM(NEL_CANTIDAD) AS CANTIDAD
          FROM SAPR_TEFECTIVOXDOCUMENTO E
    INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = E.OID_DIVISA

    LEFT JOIN GEPR_TCODIGO_AJENO CADIV ON CADIV.OID_TABLA_GENESIS = E.OID_DIVISA AND CADIV.COD_IDENTIFICADOR = par$cod_ajeno AND CADIV.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'

    LEFT JOIN GEPR_TDENOMINACION DEN ON DEN.OID_DENOMINACION = E.OID_DENOMINACION

    LEFT JOIN GEPR_TCODIGO_AJENO CADEN ON CADEN.OID_TABLA_GENESIS = E.OID_DENOMINACION AND CADEN.COD_IDENTIFICADOR = par$cod_ajeno AND CADEN.COD_TIPO_TABLA_GENESIS = 'GEPR_TDENOMINACION'

         WHERE E.OID_DOCUMENTO IN (SELECT T.OID OID_DOCUMENTO FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID)
      GROUP BY E.OID_DOCUMENTO,
               DIV.COD_ISO_DIVISA,
               DEN.COD_DENOMINACION,
               E.COD_NIVEL_DETALLE,
               E.OID_UNIDAD_MEDIDA,
               E.OID_CALIDAD,
               CADIV.COD_AJENO,
               CADEN.COD_AJENO
      UNION ALL
        SELECT M.OID_DOCUMENTO,
               NVL(CADIV.COD_AJENO, DIV.COD_ISO_DIVISA) COD_ISO_DIVISA,
               NULL AS COD_DENOMINACION,
               NVL(CAMP.COD_AJENO, MP.COD_MEDIO_PAGO) COD_MEDIO_PAGO,
               MP.COD_TIPO_MEDIO_PAGO,
               NVL(CAMP.DES_AJENO, MP.DES_MEDIO_PAGO) DES_MEDIO_PAGO,
               M.COD_NIVEL_DETALLE,
               NULL AS OID_UNIDAD_MEDIDA,
               NULL AS OID_CALIDAD,
               SUM(NUM_IMPORTE) AS IMPORTE,
               SUM(NEL_CANTIDAD) AS CANTIDAD
          FROM SAPR_TMEDIO_PAGOXDOCUMENTO M
    INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = M.OID_DIVISA

    LEFT JOIN GEPR_TCODIGO_AJENO CADIV ON CADIV.OID_TABLA_GENESIS = M.OID_DIVISA AND CADIV.COD_IDENTIFICADOR = par$cod_ajeno AND CADIV.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'

    INNER JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = M.OID_MEDIO_PAGO

    LEFT JOIN GEPR_TCODIGO_AJENO CAMP ON CAMP.OID_TABLA_GENESIS = M.OID_MEDIO_PAGO AND CAMP.COD_IDENTIFICADOR = par$cod_ajeno AND CAMP.COD_TIPO_TABLA_GENESIS = 'GEPR_TMEDIO_PAGO'

         WHERE M.OID_DOCUMENTO IN (SELECT T.OID OID_DOCUMENTO FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID)
      GROUP BY M.OID_DOCUMENTO,
               DIV.COD_ISO_DIVISA,
               MP.COD_MEDIO_PAGO,
               MP.COD_TIPO_MEDIO_PAGO,
               MP.DES_MEDIO_PAGO,
               M.COD_NIVEL_DETALLE,
               CADIV.COD_AJENO,
               CAMP.COD_AJENO,
               CAMP.DES_AJENO;
      var$selects   := var$selects + SQL % ROWCOUNT;
      var$registros := var$registros + SQL % ROWCOUNT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar refcursor : Valores de Documentos', '', par$usuario, var$cod_ejecucion);

    ELSE
      OPEN par$doc_rc_valores_documentos FOR
                  SELECT E.OID_DOCUMENTO,
                       NVL(CADIV.COD_AJENO, DIV.COD_ISO_DIVISA) COD_ISO_DIVISA,
                       COD_NIVEL_DETALLE,
                       SUM(NUM_IMPORTE) AS IMPORTE
                  FROM SAPR_TEFECTIVOXDOCUMENTO E
            INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = E.OID_DIVISA

          LEFT JOIN GEPR_TCODIGO_AJENO CADIV ON CADIV.OID_TABLA_GENESIS = E.OID_DIVISA AND CADIV.COD_IDENTIFICADOR = par$cod_ajeno AND CADIV.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'

                 WHERE E.OID_DOCUMENTO IN (SELECT T.OID OID_DOCUMENTO FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID)
              GROUP BY E.OID_DOCUMENTO,
                       DIV.COD_ISO_DIVISA,
                       E.COD_NIVEL_DETALLE,
                 CADIV.COD_AJENO
              UNION ALL
                SELECT M.OID_DOCUMENTO,
                       NVL(CADIV.COD_AJENO, DIV.COD_ISO_DIVISA) COD_ISO_DIVISA,
                       M.COD_NIVEL_DETALLE,
                       SUM(NUM_IMPORTE) AS IMPORTE
                  FROM SAPR_TMEDIO_PAGOXDOCUMENTO M
            INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = M.OID_DIVISA

          LEFT JOIN GEPR_TCODIGO_AJENO CADIV ON CADIV.OID_TABLA_GENESIS = M.OID_DIVISA AND CADIV.COD_IDENTIFICADOR = par$cod_ajeno AND CADIV.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA'

                 WHERE M.OID_DOCUMENTO IN (SELECT T.OID OID_DOCUMENTO FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID)
              GROUP BY M.OID_DOCUMENTO,
                       DIV.COD_ISO_DIVISA,
                       M.COD_NIVEL_DETALLE,
                 CADIV.COD_AJENO;
      var$selects   := var$selects + SQL % ROWCOUNT;
      var$registros := var$registros + SQL % ROWCOUNT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar refcursor : Valores de Documentos', '', par$usuario, var$cod_ejecucion);
    END IF;

ELSE

    /* - TABLA: Valores de Documentos */
    IF par$bol_detallar_desgloses > 0 THEN
    
      /* - TABLA: Valores de Documentos - Efectivos */
      var$parcial := SYSTIMESTAMP;
      var$cod_ejecucion := null;
        OPEN par$doc_rc_valores_documentos FOR
        SELECT E.OID_DOCUMENTO,
               DIV.COD_ISO_DIVISA,
               DEN.COD_DENOMINACION,
                 NULL AS COD_MEDIO_PAGO,
                 NULL AS COD_TIPO_MEDIO_PAGO,
                 NULL AS DES_MEDIO_PAGO,
                 COD_NIVEL_DETALLE,
                 OID_UNIDAD_MEDIDA,
                 OID_CALIDAD,
                 SUM(NUM_IMPORTE) AS IMPORTE,
                 SUM(NEL_CANTIDAD) AS CANTIDAD
          FROM SAPR_TEFECTIVOXDOCUMENTO E
    INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = E.OID_DIVISA
    LEFT JOIN GEPR_TDENOMINACION DEN ON DEN.OID_DENOMINACION = E.OID_DENOMINACION
         WHERE E.OID_DOCUMENTO IN (SELECT T.OID OID_DOCUMENTO FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID)
      GROUP BY E.OID_DOCUMENTO,
               DIV.COD_ISO_DIVISA,
               DEN.COD_DENOMINACION,
               E.COD_NIVEL_DETALLE,
               E.OID_UNIDAD_MEDIDA,
               E.OID_CALIDAD
          UNION ALL
        SELECT M.OID_DOCUMENTO,
               DIV.COD_ISO_DIVISA,
                 NULL AS COD_DENOMINACION,
                 MP.COD_MEDIO_PAGO,
                 MP.COD_TIPO_MEDIO_PAGO,
                 MP.DES_MEDIO_PAGO,
               M.COD_NIVEL_DETALLE,
                 NULL AS OID_UNIDAD_MEDIDA,
                 NULL AS OID_CALIDAD,
                 SUM(NUM_IMPORTE) AS IMPORTE,
                 SUM(NEL_CANTIDAD) AS CANTIDAD
          FROM SAPR_TMEDIO_PAGOXDOCUMENTO M
    INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = M.OID_DIVISA
    INNER JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = M.OID_MEDIO_PAGO
         WHERE M.OID_DOCUMENTO IN (SELECT T.OID OID_DOCUMENTO FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID)
      GROUP BY M.OID_DOCUMENTO,
               DIV.COD_ISO_DIVISA,
               MP.COD_MEDIO_PAGO,
                    MP.COD_TIPO_MEDIO_PAGO,
                    MP.DES_MEDIO_PAGO,
               M.COD_NIVEL_DETALLE;
        var$selects   := var$selects + SQL % ROWCOUNT;
        var$registros := var$registros + SQL % ROWCOUNT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar refcursor : Valores de Documentos', '', par$usuario, var$cod_ejecucion);
        
    ELSE
        OPEN par$doc_rc_valores_documentos FOR
          SELECT E.OID_DOCUMENTO,
               DIV.COD_ISO_DIVISA,
                 COD_NIVEL_DETALLE,
                 SUM(NUM_IMPORTE) AS IMPORTE
          FROM SAPR_TEFECTIVOXDOCUMENTO E
            INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = E.OID_DIVISA
                 WHERE E.OID_DOCUMENTO IN (SELECT T.OID OID_DOCUMENTO FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID)
              GROUP BY E.OID_DOCUMENTO,
                       DIV.COD_ISO_DIVISA,
                       E.COD_NIVEL_DETALLE
                  UNION ALL
                SELECT M.OID_DOCUMENTO,
                       DIV.COD_ISO_DIVISA,
                       M.COD_NIVEL_DETALLE,
                         SUM(NUM_IMPORTE) AS IMPORTE
                  FROM SAPR_TMEDIO_PAGOXDOCUMENTO M
            INNER JOIN GEPR_TDIVISA DIV ON DIV.OID_DIVISA = M.OID_DIVISA
                 WHERE M.OID_DOCUMENTO IN (SELECT T.OID OID_DOCUMENTO FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID)
              GROUP BY M.OID_DOCUMENTO,
                       DIV.COD_ISO_DIVISA,
                       M.COD_NIVEL_DETALLE;
        var$selects   := var$selects + SQL % ROWCOUNT;
        var$registros := var$registros + SQL % ROWCOUNT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar refcursor : Valores de Documentos', '', par$usuario, var$cod_ejecucion);
    END IF;

END IF;

    /* - TABLA: Valor terminos documentos */
    var$parcial := SYSTIMESTAMP;
    var$cod_ejecucion := null;
    IF par$bol_campos_adicionales > 0 THEN
      
        OPEN par$doc_rc_valor_terminos_doc FOR
        SELECT TD.OID_DOCUMENTO,
               TD.DES_VALOR,
               T.OID_TERMINO,
               T.COD_TERMINO,
               T.DES_TERMINO
          FROM SAPR_TVALOR_TERMINOXDOCUMENTO TD
           INNER JOIN GEPR_TTERMINO T
              ON TD.OID_TERMINO = T.OID_TERMINO
         WHERE TD.OID_DOCUMENTO IN (SELECT T.OID FROM SAPR_GTT_TOIDS T WHERE T.COD_CALIFICADOR = 'DOCUMENTO' GROUP BY T.OID);
        var$selects   := var$selects + SQL % ROWCOUNT;
        var$registros := var$registros + SQL % ROWCOUNT;
        
    ELSE
        OPEN par$doc_rc_valor_terminos_doc FOR
          SELECT SYSDATE FROM DUAL;
    END IF;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func, 0, 0, 0, 0, 0, 0,
                          var$parcial, null, 'C', 'Cargar refcursor : Valor terminos documentos', '', par$usuario, var$cod_ejecucion);

  
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
                                                            '',
                                                            par$usuario,
                                                            par$cod_ejecucion);
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
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true);
  END srecuperar_docs_certificados;
  
  PROCEDURE svalidar_doc_contenedor(par$oid_formulario        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_formulario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$aform_caracteristicas IN gepr_pcomon_###VERSION###.tipo$formularios,
                                    par$oid_grupo_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
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
                                    par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_) AS
  
    var$contenedores        VARCHAR2_ARRAY;
    var$nel_cant_sectores   gepr_pcomon_###VERSION###.tipo$nel_;
    var$nel_cant_desarmados gepr_pcomon_###VERSION###.tipo$nel_;
    var$nel_cant_con_padre  gepr_pcomon_###VERSION###.tipo$nel_;
    var$str_items_error     gepr_pcomon_###VERSION###.tipo$obs_;
  
  BEGIN
  
    /*   raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
    '***AQUI***');*/
  
    if par$oid_formulario is null and par$cod_formulario is null then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'formulario',
                                                                  1));
      RETURN;
    end if;
  
    if par$aform_caracteristicas is null or
       par$aform_caracteristicas.count = 0 then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_formulario_invalido',
                                                                  gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    end if;
  
    if par$adoc_fyh_gestion is null or par$adoc_fyh_gestion.count = 0 then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'fecha gestion',
                                                                  1));
      RETURN;
    
    end if;
  
    if par$adoc_oid_documento is null or par$adoc_oid_documento.count = 0 then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'identificador documento',
                                                                  1));
      RETURN;
    
    end if;
  
    if par$adoc_oid_cuenta_origen is null or
       par$adoc_oid_cuenta_origen.count = 0 then
    
      if par$adoc_oid_cliente_origen is null or
         par$adoc_oid_cliente_origen.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador cliente origen',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_canal_origen is null or
         par$adoc_oid_canal_origen.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador canal origen',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_subcanal_origen is null or
         par$adoc_oid_subcanal_origen.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador subcanal origen',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_delegacion_origen is null or
         par$adoc_oid_delegacion_origen.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador delegaci?n origen',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_planta_origen is null or
         par$adoc_oid_planta_origen.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador planta origen',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_sector_origen is null or
         par$adoc_oid_sector_origen.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador sector origen',
                                                                    1));
        RETURN;
      
      end if;
    
    end if;
  
    if par$adoc_oid_cuenta_destino is null or
       par$adoc_oid_cuenta_destino.count = 0 then
    
      if par$adoc_oid_cliente_destino is null or
         par$adoc_oid_cliente_destino.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador cliente destino',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_canal_destino is null or
         par$adoc_oid_canal_destino.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador canal destino',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_subcanal_destino is null or
         par$adoc_oid_subcanal_destino.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador subcanal destino',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_delegacion_des is null or
         par$adoc_oid_delegacion_des.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador delegacion destino',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_planta_destino is null or
         par$adoc_oid_planta_destino.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador planta destino',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$adoc_oid_sector_destino is null or
         par$adoc_oid_sector_destino.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador sector destino',
                                                                    1));
        RETURN;
      
      end if;
    
    end if;
  
    if par$aefdoc_oid_documento is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefdoc_oid_documento) then
    
      if par$aefdoc_cod_niv_detalle is null or
         par$aefdoc_cod_niv_detalle.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'codigo nivel detalle',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$aefdoc_num_importe is null or par$aefdoc_num_importe.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'importe',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$aefdoc_oid_divisa is null or par$aefdoc_oid_divisa.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador divisa',
                                                                    1));
        RETURN;
      
      end if;
    
      if (par$aefdoc_oid_documento.count <>
         par$aefdoc_cod_niv_detalle.count) or
         (par$aefdoc_oid_documento.count <>
         par$aefdoc_cod_tp_efec_tot.count) or
         (par$aefdoc_oid_documento.count <> par$aefdoc_nel_cantidad.count) or
         (par$aefdoc_oid_documento.count <> par$aefdoc_num_importe.count) or
         (par$aefdoc_oid_documento.count <> par$aefdoc_oid_calidad.count) or
         (par$aefdoc_oid_documento.count <>
         par$aefdoc_oid_denominacion.count) or
         (par$aefdoc_oid_documento.count <> par$aefdoc_oid_divisa.count) or
         (par$aefdoc_oid_documento.count <>
         par$aefdoc_oid_unid_medida.count) then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_integridade_array',
                                                                    gepr_pcomon_###VERSION###.const$CodFunIntegridadArray,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'efetivos do documento',
                                                                    1));
        RETURN;
      
      end if;
    
    end if;
  
    if par$ampdoc_oid_documento is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampdoc_oid_documento) then
    
      if par$ampdoc_cod_nivel_detalle is null or
         par$ampdoc_cod_nivel_detalle.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'nivel detalle medio pago',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$ampdoc_cod_tipo_med_pago is null or
         par$ampdoc_cod_tipo_med_pago.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'codigo tipo medio pago',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$ampdoc_num_importe is null or par$ampdoc_num_importe.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'importe medio pago',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$ampdoc_oid_divisa is null or par$ampdoc_oid_divisa.count = 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'identificador divisa medio pago',
                                                                    1));
        RETURN;
      
      end if;
    
      if (par$ampdoc_oid_documento.count <>
         par$ampdoc_cod_nivel_detalle.count) or
         (par$ampdoc_oid_documento.count <>
         par$ampdoc_cod_tipo_med_pago.count) or
         (par$ampdoc_oid_documento.count <> par$ampdoc_nel_cantidad.count) or
         (par$ampdoc_oid_documento.count <> par$ampdoc_num_importe.count) or
         (par$ampdoc_oid_documento.count <> par$ampdoc_oid_divisa.count) or
         (par$ampdoc_oid_documento.count <> par$ampdoc_oid_medio_pago.count) or
         (par$ampdoc_oid_documento.count <>
         par$ampdoc_oid_unidad_medida.count) then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_integridade_array',
                                                                    gepr_pcomon_###VERSION###.const$CodFunIntegridadArray,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'medio pago do documento',
                                                                    1));
        RETURN;
      
      end if;
    
    end if;
  
    if par$adoccont_oid_contenedor is null or
       gepr_putilidades_###VERSION###.farray_vacio_oid(par$adoccont_oid_contenedor) then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'identificador contenedor',
                                                                  1));
      RETURN;
    
    end if;
  
    if par$adoccont_oid_documento is null or
       gepr_putilidades_###VERSION###.farray_vacio_oid(par$adoccont_oid_documento) then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'identificador documento contenedor',
                                                                  1));
      RETURN;
    
    end if;
  
    if (par$adoccont_oid_documento.count <>
       par$adoccont_oid_contenedor.count) then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_integridade_array',
                                                                  gepr_pcomon_###VERSION###.const$CodFunIntegridadArray,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'contenedores',
                                                                  1));
      RETURN;
    
    end if;
  
    if par$cod_cultura is null then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'codigo cultura',
                                                                  1));
      RETURN;
    
    end if;
  
    if par$usuario is null then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'usuario',
                                                                  1));
      RETURN;
    
    end if;
  
    if par$aelco_oid_contenedor is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aelco_oid_contenedor) then
    
      if (par$aelco_oid_contenedor.count <> par$aelco_ids_elemento.count) or
         (par$aelco_oid_contenedor.count <> par$aelco_precintos_ele.count) or
         (par$aelco_oid_contenedor.count <> par$aelco_tipo_elemento.count) then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_integridade_array',
                                                                    gepr_pcomon_###VERSION###.const$CodFunIntegridadArray,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'elementos del contenedor',
                                                                    1));
        RETURN;
      
      end if;
    
    end if;
  
    if (par$adoc_oid_documento.count <> par$adoc_cod_externo.count) or
       (par$adoc_oid_documento.count <> par$adoc_fyh_gestion.count) or
       (par$adoc_oid_documento.count <> par$adoc_fyh_plan_certif.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_canal_destino.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_canal_origen.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_cliente_destino.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_cliente_origen.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_cuenta_destino.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_cuenta_origen.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_delegacion_des.count) or
       (par$adoc_oid_documento.count <>
       par$adoc_oid_delegacion_origen.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_documento_padre.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_documento_sust.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_moviment_fondo.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_planta_destino.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_planta_origen.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_puntoservico_des.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_puntoservico_ori.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_sector_destino.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_sector_origen.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_subcanal_destino.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_subcanal_origen.count) or
       (par$adoc_oid_documento.count <> par$adoc_oid_subcliente_des.count) or
       (par$adoc_oid_documento.count <>
       par$adoc_oid_subcliente_origen.count) or
       (par$adoc_oid_documento.count <> par$adoc_rowver.count) then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_integridade_array',
                                                                  gepr_pcomon_###VERSION###.const$CodFunIntegridadArray,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'documentos',
                                                                  1));
      RETURN;
    
    end if;
  
    if sapr_pformulario_###VERSION###.fverif_carac_form_contenedor(par$obj_formularios => par$aform_caracteristicas) = 0 then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_formulario_incorreto',
                                                                  gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  par$cod_formulario,
                                                                  1));
      RETURN;
    
    end if;
  
    if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$aform_caracteristicas,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvio) > 0 or
       sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$aform_caracteristicas,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracBaja) > 0 or
       sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$aform_caracteristicas,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAlta) > 0 then
    
      var$contenedores := gepr_putilidades_###VERSION###.fconverter_array_table(par$table_varchar2 => par$adoccont_oid_contenedor);
    
      if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$aform_caracteristicas,
                                                               par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvio) > 0 or
         sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$aform_caracteristicas,
                                                               par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracBaja) > 0 then
        select COUNT(distinct c.oid_sector)
          into var$nel_cant_sectores
          from sapr_tcontenedor c
         where c.oid_contenedor in
               (select column_value
                  from table(var$contenedores)
                 where column_value is not null);
      
        if var$nel_cant_sectores > 1 then
        
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'mas_de_uno_sector',
                                                                      gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      NULL,
                                                                      1));
          RETURN;
        
        end if;
      
        select count(*)
          into var$nel_cant_desarmados
          from sapr_tcontenedor c
         where c.cod_estado <> gepr_pcomon_###VERSION###.const$CodEstadoConArmado
           and c.oid_contenedor in
               (select column_value
                  from table(var$contenedores)
                 where column_value is not null);
      
        if var$nel_cant_desarmados > 0 then
        
          select listagg(pc.cod_precinto, ',') WITHIN GROUP(ORDER BY pc.cod_precinto)
            into var$str_items_error
            from sapr_tcontenedor c
           inner join sapr_tprecintoxcontenedor pc
              on pc.oid_contenedor = c.oid_contenedor
           where c.cod_estado <> gepr_pcomon_###VERSION###.const$CodEstadoConArmado
             and c.oid_contenedor in
                 (select column_value
                    from table(var$contenedores)
                   where column_value is not null);
        
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'hay_contenedores_desarmados',
                                                                      gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      var$str_items_error,
                                                                      1));
          RETURN;
        
        end if;
      
      end if;
    
      select count(*)
        into var$nel_cant_con_padre
        from sapr_tcontenedor c
       where c.oid_contenedor_padre is not null
         and c.oid_contenedor in
             (select column_value
                from table(var$contenedores)
               where column_value is not null);
    
      if var$nel_cant_con_padre > 0 then
      
        select listagg(pc.cod_precinto, ',') WITHIN GROUP(ORDER BY pc.cod_precinto)
          into var$str_items_error
          from sapr_tcontenedor c
         inner join sapr_tprecintoxcontenedor pc
            on pc.oid_contenedor = c.oid_contenedor
         where c.oid_contenedor_padre is not null
           and c.oid_contenedor in
               (select column_value
                  from table(var$contenedores)
                 where column_value is not null);
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'hay_contenedores_con_padre',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    var$str_items_error,
                                                                    1));
        RETURN;
      
      end if;
    
      if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$aform_caracteristicas,
                                                               par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvioClientes) > 0 then
      
        /* Implementar regra contenedor com varias contas*/
        null;
      end if;
    
    end if;
  
  END svalidar_doc_contenedor;

  PROCEDURE svalidar_doc_contenedor_alta(par$oid_formulario        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_formulario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$aform_caracteristicas IN gepr_pcomon_###VERSION###.tipo$formularios,
                                         par$oid_grupo_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
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
                                         
                                         -- contenedores
                                         par$adoccont_oid_documento     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoccont_oid_contenedor    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$adoccont_cod_tipo_cont     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$adoccont_obj_tipos_cont    in gepr_pcomon_###VERSION###.tipo$tiposContenedores,
                                         par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,
                                         
                                         -- Precintos
                                         par$aprco_oid_contenedor       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         
                                         -- Elementos
                                         par$aelco_oid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         
                                         /* informacion de ejecucion */
                                         par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                         par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_) as
  
    var$apr_contenedor_consulta varchar2_array;
    var$bol_hay_precintos       gepr_pcomon_###VERSION###.tipo$nel_;
    var$acod_tipo_valores       varchar2_array;
    var$bol_hay_valores_totales gepr_pcomon_###VERSION###.tipo$nel_;
  
  begin
  
    /*   \* Validaciones del documento de Gestion de Contenedores *\ */
    svalidar_doc_contenedor(par$oid_formulario        => par$oid_formulario,
                            par$cod_formulario        => par$cod_formulario,
                            par$aform_caracteristicas => par$aform_caracteristicas,
                            par$oid_grupo_documento   => par$oid_grupo_documento,
                            --documentos
                            par$adoc_oid_documento       => par$adoc_oid_documento,
                            par$adoc_oid_documento_padre => par$adoc_oid_documento_padre,
                            par$adoc_oid_documento_sust  => par$adoc_oid_documento_sust,
                            par$adoc_oid_moviment_fondo  => par$adoc_oid_moviment_fondo,
                            par$adoc_fyh_plan_certif     => par$adoc_fyh_plan_certif,
                            par$adoc_fyh_gestion         => par$adoc_fyh_gestion,
                            par$adoc_cod_externo         => par$adoc_cod_externo,
                            par$adoc_rowver              => par$adoc_rowver,
                            /* cuenta origen */
                            par$adoc_oid_cuenta_origen     => par$adoc_oid_cuenta_origen,
                            par$adoc_oid_cliente_origen    => par$adoc_oid_cliente_origen,
                            par$adoc_oid_subcliente_origen => par$adoc_oid_subcliente_origen,
                            par$adoc_oid_puntoservico_ori  => par$adoc_oid_puntoservico_ori,
                            par$adoc_oid_canal_origen      => par$adoc_oid_canal_origen,
                            par$adoc_oid_subcanal_origen   => par$adoc_oid_subcanal_origen,
                            par$adoc_oid_delegacion_origen => par$adoc_oid_delegacion_origen,
                            par$adoc_oid_planta_origen     => par$adoc_oid_planta_origen,
                            par$adoc_oid_sector_origen     => par$adoc_oid_sector_origen
                            
                            /* cuenta destino */,
                            par$adoc_oid_cuenta_destino   => par$adoc_oid_cuenta_destino,
                            par$adoc_oid_cliente_destino  => par$adoc_oid_cliente_destino,
                            par$adoc_oid_subcliente_des   => par$adoc_oid_subcliente_des,
                            par$adoc_oid_puntoservico_des => par$adoc_oid_puntoservico_des,
                            par$adoc_oid_canal_destino    => par$adoc_oid_canal_destino,
                            par$adoc_oid_subcanal_destino => par$adoc_oid_subcanal_destino,
                            par$adoc_oid_delegacion_des   => par$adoc_oid_delegacion_des,
                            par$adoc_oid_planta_destino   => par$adoc_oid_planta_destino,
                            par$adoc_oid_sector_destino   => par$adoc_oid_sector_destino,
                            
                            /* arrays asociativos */
                            /* arrays de efectivo por documento */
                            par$aefdoc_oid_documento    => par$aefdoc_oid_documento,
                            par$aefdoc_oid_divisa       => par$aefdoc_oid_divisa,
                            par$aefdoc_oid_denominacion => par$aefdoc_oid_denominacion,
                            par$aefdoc_oid_unid_medida  => par$aefdoc_oid_unid_medida,
                            par$aefdoc_cod_niv_detalle  => par$aefdoc_cod_niv_detalle,
                            par$aefdoc_cod_tp_efec_tot  => par$aefdoc_cod_tp_efec_tot,
                            par$aefdoc_oid_calidad      => par$aefdoc_oid_calidad,
                            par$aefdoc_num_importe      => par$aefdoc_num_importe,
                            par$aefdoc_nel_cantidad     => par$aefdoc_nel_cantidad
                            
                            /* arrays de medio de pago por documento */,
                            par$ampdoc_oid_documento     => par$ampdoc_oid_documento,
                            par$ampdoc_oid_divisa        => par$ampdoc_oid_divisa,
                            par$ampdoc_oid_medio_pago    => par$ampdoc_oid_medio_pago,
                            par$ampdoc_cod_tipo_med_pago => par$ampdoc_cod_tipo_med_pago,
                            par$ampdoc_cod_nivel_detalle => par$ampdoc_cod_nivel_detalle,
                            par$ampdoc_num_importe       => par$ampdoc_num_importe,
                            par$ampdoc_nel_cantidad      => par$ampdoc_nel_cantidad,
                            par$ampdoc_oid_unidad_medida => par$ampdoc_oid_unidad_medida,
                            
                            -- contenedores
                            par$adoccont_oid_documento     => par$adoccont_oid_documento,
                            par$adoccont_oid_contenedor    => par$adoccont_oid_contenedor,
                            par$adoccont_rowver_contenedor => par$adoccont_rowver_contenedor,
                            
                            -- Elementos
                            par$aelco_oid_contenedor => par$aelco_oid_contenedor,
                            par$aelco_tipo_elemento  => par$aelco_tipo_elemento,
                            par$aelco_precintos_ele  => par$aelco_precintos_ele,
                            par$aelco_ids_elemento   => par$aelco_ids_elemento,
                            
                            /* informacion de ejecucion */
                            par$usuario     => par$usuario,
                            par$cod_cultura => par$cod_cultura);
  
    if (par$aelco_oid_contenedor is null or
       gepr_putilidades_###VERSION###.farray_vacio_oid(par$aelco_oid_contenedor)) and
       (par$aefdoc_oid_divisa is null or
       gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefdoc_oid_divisa)) and
       (par$ampdoc_oid_divisa is null or
       gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampdoc_oid_divisa)) then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_contenedores_sin_valores',
                                                                  gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;
    
    end if;
  
    if par$aprco_oid_contenedor is null or
       gepr_putilidades_###VERSION###.farray_vacio_oid(par$array => par$aprco_oid_contenedor) then
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'identificador contenedor',
                                                                  1));
      RETURN;
    
    else
    
      if par$aprco_precintos_contenedor is null or
         gepr_putilidades_###VERSION###.farray_vacio_cod(par$aprco_precintos_contenedor) then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgAtributoObrigatorio',
                                                                    gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'precintos del contenedor',
                                                                    1));
        RETURN;
      
      end if;
    
      if par$aprco_oid_contenedor.count <>
         par$aprco_precintos_contenedor.count then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_integridade_array',
                                                                    gepr_pcomon_###VERSION###.const$CodFunIntegridadArray,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    'contenedores',
                                                                    1));
        RETURN;
      
      end if;
    
      for idx in par$aprco_oid_contenedor.first .. par$aprco_oid_contenedor.last loop
      
        if par$aprco_precintos_contenedor(idx) is null then
        
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'MsgAtributoObrigatorio',
                                                                      gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                      'precinto del contenedor',
                                                                      1));
          RETURN;
        
        end if;
      
      end loop;
    
      var$apr_contenedor_consulta := gepr_putilidades_###VERSION###.fconverter_array_cod_table(par$table_varchar2 => par$aprco_precintos_contenedor);
    
      select count(*)
        into var$bol_hay_precintos
        from sapr_tcontenedor c
       inner join sapr_tprecintoxcontenedor pc
          on pc.oid_contenedor = c.oid_contenedor
       inner join table(var$apr_contenedor_consulta) tc
          on tc.column_value = pc.cod_precinto
       where c.cod_estado = gepr_pcomon_###VERSION###.const$CodEstadoConArmado;
    
      if var$bol_hay_precintos > 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_precinto_contenedor_ya_existe',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      
      end if;
    
    end if;
  
    if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$aform_caracteristicas,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracPackModular) > 0 then
    
      for idx in par$adoccont_oid_contenedor.first .. par$adoccont_oid_contenedor.last loop
      
        if par$adoccont_cod_tipo_cont(idx) =
           gepr_pcomon_###VERSION###.const$codTipoContInd then
        
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_tipo_indistinto',
                                                                      gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      NULL,
                                                                      1));
          RETURN;
        
        end if;
      
      end loop;
    
      svalidar_cuentas_pack_modular(par$adoc_oid_cuenta_origen     => par$adoc_oid_cuenta_origen,
                                    par$adoc_oid_cliente_origen    => par$adoc_oid_cliente_origen,
                                    par$adoc_oid_subcliente_origen => par$adoc_oid_subcliente_origen,
                                    par$adoc_oid_puntoservico_ori  => par$adoc_oid_puntoservico_ori,
                                    par$adoc_oid_canal_origen      => par$adoc_oid_canal_origen,
                                    par$adoc_oid_subcanal_origen   => par$adoc_oid_subcanal_origen,
                                    par$adoc_oid_delegacion_origen => par$adoc_oid_delegacion_origen,
                                    par$adoc_oid_planta_origen     => par$adoc_oid_planta_origen,
                                    par$adoc_oid_sector_origen     => par$adoc_oid_sector_origen,
                                    par$cod_cultura                => par$cod_cultura);
    
      var$acod_tipo_valores := gepr_putilidades_###VERSION###.fconverter_array_cod_table(par$table_varchar2 => par$aefdoc_cod_niv_detalle);
    
      select count(*)
        into var$bol_hay_valores_totales
        from table(var$acod_tipo_valores)
       where column_value = gepr_pcomon_###VERSION###.const$cod_nivel_det_total;
    
      if var$bol_hay_valores_totales > 0 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_valores_totales',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      
      end if;
    
      if par$ampdoc_oid_documento is not null and not
          gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampdoc_oid_documento) then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_valores_medio_pago',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      end if;
    
    end if;
  
  end svalidar_doc_contenedor_alta;

  procedure svalidar_cuentas_pack_modular(par$adoc_oid_cuenta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_cliente_origen    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_subcliente_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_puntoservico_ori  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_canal_origen      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_subcanal_origen   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_delegacion_origen IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_planta_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$adoc_oid_sector_origen     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$cod_cultura                in gepr_pcomon_###VERSION###.tipo$cod_) as
  
    var$nel_cantidad_cuentas gepr_pcomon_###VERSION###.tipo$nel_;
    var$aoid_cuentas         varchar2_array;
    var$str_chave_cuenta     varchar2(4000);
  
  begin
  
    if par$adoc_oid_cuenta_origen is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$adoc_oid_cuenta_origen) then
    
      var$aoid_cuentas := gepr_putilidades_###VERSION###.fconverter_array_table(par$adoc_oid_cuenta_origen);
    
      select count(*)
        into var$nel_cantidad_cuentas
        from table(var$aoid_cuentas)
       where column_value is not null;
    
      if var$nel_cantidad_cuentas > 1 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_hay_mas_de_una_cuenta',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      
      end if;
    
    else
    
      for idx in par$adoc_oid_cliente_origen.first .. par$adoc_oid_cliente_origen.last loop
      
        var$str_chave_cuenta := par$adoc_oid_cliente_origen(idx) ||
                                par$adoc_oid_subcliente_origen(idx) ||
                                par$adoc_oid_puntoservico_ori(idx) ||
                                par$adoc_oid_canal_origen(idx) ||
                                par$adoc_oid_subcanal_origen(idx) ||
                                par$adoc_oid_delegacion_origen(idx) ||
                                par$adoc_oid_planta_origen(idx) ||
                                par$adoc_oid_sector_origen(idx) || '|';
      
      end loop;
    
      var$aoid_cuentas := string_to_array_###VERSION### (p_string    => var$str_chave_cuenta,
                                          p_seperator => '|');
    
      select count(*)
        into var$nel_cantidad_cuentas
        from table(var$aoid_cuentas)
       where column_value is not null;
    
      if var$nel_cantidad_cuentas > 1 then
      
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msg_hay_mas_de_una_cuenta',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;
      
      end if;
    
    end if;
  
  end svalidar_cuentas_pack_modular;

  FUNCTION fvalidar_cod_externo_duplicado(par$cod_externo    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
  
    EXISTE gepr_pcomon_###VERSION###.tipo$cant_;
  
  BEGIN
  
    SELECT COUNT(CASE
                   WHEN REME.COD_ESTADO IN ('PE', 'ET') OR
                        DOCE.COD_ESTADO_DOCXELEMENTO = 'T' THEN
                    1
                   ELSE
                    0
                 END) EXISTE
      INTO EXISTE
      FROM SAPR_TDOCUMENTOXELEMENTO DOCE
     INNER JOIN SAPR_TREMESA REME
        ON REME.OID_REMESA = DOCE.OID_REMESA
     INNER JOIN SAPR_TCUENTA CUEN
        ON CUEN.OID_CUENTA = REME.OID_CUENTA
     INNER JOIN GEPR_TSECTOR SECT
        ON SECT.OID_SECTOR = CUEN.OID_SECTOR
     INNER JOIN GEPR_TPLANTA PLAT
        ON PLAT.OID_PLANTA = SECT.OID_PLANTA
     INNER JOIN GEPR_TDELEGACION DELE
        ON DELE.OID_DELEGACION = PLAT.OID_DELEGACION
     WHERE DELE.COD_DELEGACION = par$cod_delegacion
       AND REME.COD_EXTERNO = par$cod_externo;
  
    IF (EXISTE > 0) THEN
      RETURN FALSE;
    ELSE
      RETURN TRUE;
    END IF;
  
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      RETURN FALSE;
    
  END fvalidar_cod_externo_duplicado;

  PROCEDURE supd_fyh_plan_certificacion(par$oid_documento          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$usuario                IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$updates                OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    UPDATE sapr_tdocumento
       SET fyh_plan_certificacion   = par$fyh_plan_certificacion,
           des_usuario_modificacion = par$usuario,
           gmt_modificacion         = var$gmt_zero
     WHERE oid_documento = par$oid_documento;
  
    par$updates := sql%rowcount;
  END supd_fyh_plan_certificacion;

  PROCEDURE srecuperar_documentos_mae(par$cod_delegacion     in gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$cod_identificacion in gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$fec_transacciones in gepr_pcomon_###VERSION###.tipo$fyh_,
                                      par$rc_documentos      out sys_refcursor) IS
  BEGIN
    OPEN par$rc_documentos FOR
      SELECT DOCU.OID_DOCUMENTO,
             DOCU.COD_EXTERNO,
             FORM.COD_FORMULARIO,
             FORM.DES_FORMULARIO,
             fn_gmt_delegacion_###VERSION###(PLA.OID_DELEGACION,DOCU.FYH_GESTION)AS FYH_GESTION,
             PUSE.COD_PTO_SERVICIO,
             PUSE.DES_PTO_SERVICIO,
             MAQU.COD_IDENTIFICACION,
             DIVI.COD_ISO_DIVISA,
             SUM(EFDO.NUM_IMPORTE) IMPORTE
        FROM SAPR_TDOCUMENTO DOCU
       INNER JOIN SAPR_TFORMULARIO FORM
          ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO
       INNER JOIN SAPR_TCUENTA CUEN
          ON DOCU.OID_CUENTA_DESTINO = CUEN.OID_CUENTA
       INNER JOIN SAPR_TMAQUINA MAQU
          ON DOCU.OID_SECTOR_DESTINO = MAQU.OID_SECTOR
       INNER JOIN GEPR_TPUNTO_SERVICIO PUSE
          ON CUEN.OID_PTO_SERVICIO = PUSE.OID_PTO_SERVICIO
       INNER JOIN SAPR_TEFECTIVOXDOCUMENTO EFDO
          ON DOCU.OID_DOCUMENTO = EFDO.OID_DOCUMENTO
       INNER JOIN GEPR_TDIVISA DIVI
          ON EFDO.OID_DIVISA = DIVI.OID_DIVISA
       INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = MAQU.OID_SECTOR   
       INNER JOIN GEPR_TPLANTA PLA ON PLA.OID_PLANTA = SEC.OID_PLANTA
       WHERE FORM.COD_FORMULARIO IN ('MAECIC', 'MAESOC')
         AND DOCU.COD_ESTADO = 'AC'
         AND TRUNC(fn_gmt_delegacion_###VERSION###(PLA.OID_DELEGACION,DOCU.FYH_GESTION)) = TRUNC(par$fec_transacciones)
         AND MAQU.COD_IDENTIFICACION = par$cod_identificacion
       GROUP BY DOCU.OID_DOCUMENTO,
                DOCU.COD_EXTERNO,
                FORM.COD_FORMULARIO,
                FORM.DES_FORMULARIO,
                PLA.OID_DELEGACION,
                DOCU.FYH_GESTION,
                PUSE.COD_PTO_SERVICIO,
                PUSE.DES_PTO_SERVICIO,
                MAQU.COD_IDENTIFICACION,
                DIVI.COD_ISO_DIVISA
       ORDER BY DOCU.FYH_GESTION DESC;
  
  END srecuperar_documentos_mae;

  PROCEDURE supd_extradata(par$oid_documento          IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_termino           IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$des_valor             IN gepr_pcomon_###VERSION###.tipo$obs_,
                            par$cod_usuario           IN gepr_pcomon_###VERSION###.tipo$usr_) IS
  BEGIN
      MERGE INTO SAPR_TVALOR_TERMINOxDOCUMENTO
      USING DUAL ON
      (
          oid_documento      = par$oid_documento AND
          oid_termino = par$oid_termino 
      )
     WHEN MATCHED THEN
       UPDATE
         SET DES_VALOR =  NVL(par$des_valor, ''),
             DES_USUARIO_MODIFICACION = par$cod_usuario,
          GMT_MODIFICACION = FN_GMT_ZERO_###VERSION###
            WHEN NOT MATCHED THEN
              INSERT
            (OID_VALOR_TERMINOXDOCUMENTO
            ,OID_DOCUMENTO
            ,OID_TERMINO
            ,DES_VALOR
            ,GMT_CREACION
            ,DES_USUARIO_CREACION
            ,GMT_MODIFICACION
            ,DES_USUARIO_MODIFICACION
            ,COD_MIGRACION)VALUES
            (SYS_GUID()
            ,par$oid_documento
            ,par$oid_termino
            ,NVL(par$des_valor, '')
            ,FN_GMT_ZERO_###VERSION###
            ,par$cod_usuario
            ,FN_GMT_ZERO_###VERSION###
            ,par$cod_usuario
            ,NULL);
  END supd_extradata;

   PROCEDURE supd_altacashin(
      par$adoc_cod_externo         IN gepr_pcomon_###VERSION###.tipo$cods_,
      par$acod_actual_id            IN gepr_pcomon_###VERSION###.tipo$descs_,
      par$acod_collection_id        IN gepr_pcomon_###VERSION###.tipo$descs_
  ) IS 
  begin
    if par$adoc_cod_externo is not null then
      for idx in par$adoc_cod_externo.first .. par$adoc_cod_externo.last 
      loop
          if par$adoc_cod_externo(idx) is not null then
              UPDATE sapr_tdocumento
                  SET 
                    cod_actual_id = par$acod_actual_id(idx),
                    cod_collection_id = par$acod_collection_id(idx)
                WHERE cod_externo = par$adoc_cod_externo(idx);
          end if;
      end loop;
      COMMIT;
    end if;
  end supd_altacashin;
  
  
  
  PROCEDURE sgrabar_integracion_documento(
                            par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_documento           IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_) IS
                            
    const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sgrabar_integracion_documento';
    var$cod_actual_id   gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN

   IF fvalidar_integracion_documento(par$oid_llamada, par$oid_documento ) THEN

        /*Obtengo el tipo de planificacion  y el codigo de formulario*/
        SELECT DOCU.COD_ACTUAL_ID
          INTO var$cod_actual_id
        FROM SAPR_TDOCUMENTO DOCU
          INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOCU.OID_FORMULARIO
          LEFT JOIN SAPR_TPLANXMAQUINA PLANXMAQ ON PLANXMAQ.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN AND PLANXMAQ.BOL_ACTIVO = 1
          LEFT JOIN SAPR_TPLANIFICACION PLAN ON PLAN.OID_PLANIFICACION = PLANXMAQ.OID_PLANIFICACION
          LEFT JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLAN.OID_TIPO_PLANIFICACION
        WHERE DOCU.OID_DOCUMENTO = par$oid_documento;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                            par$des_origen      => const$des_origen,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Se almacena en la tabla de integración',
                                            par$cod_identificador   => '');

        GEPR_PINTEGRACION_###VERSION###.supd_integracion(par$oid_integracion         => sys_guid(),
                    par$oid_tabla_integracion   =>  var$cod_actual_id,
                    par$cod_tabla_integracion   => 'COD_ACTUAL_ID',
                    par$cod_estado              => 'AB',
                    par$cod_modulo_origen       => 'GEPR',
                    par$cod_modulo_destino      => 'SWITCH',
                    par$cod_proceso             => 'EnviarDocumentos',
                    par$nel_intento_envio       => 0,
                    par$cod_usuario             => par$cod_usuario);        


    END IF;
  END sgrabar_integracion_documento;


  PROCEDURE sget_parametros_integracion(
                            par$enviar_datos_tipo_plani   OUT gepr_pcomon_###VERSION###.tipo$oid_,                         
                            par$enviar_datos_switch           OUT gepr_pcomon_###VERSION###.tipo$cod_) IS 
      const$cod_parametro_tipo_plani CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchTipoPlanificacion';
      const$cod_parametro_sin_fvo CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchSinFechaValor';
      begin

      /* Obtener parametros */
        BEGIN 

            SELECT DES_VALOR_PARAMETRO
            INTO par$enviar_datos_tipo_plani
            FROM GEPR_TPARAMETRO
            INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
            WHERE GEPR_TPARAMETRO.cod_parametro = const$cod_parametro_tipo_plani and rownum = 1;
            EXCEPTION WHEN no_data_found THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| const$cod_parametro_tipo_plani);
            WHEN others THEN 
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'Ocurrió un error al intentar obtener el var$cod_parametro_tipo_plani para el código: '|| const$cod_parametro_tipo_plani);
        END;
        BEGIN 

            SELECT DES_VALOR_PARAMETRO
            INTO par$enviar_datos_switch
            FROM GEPR_TPARAMETRO
            INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
            WHERE GEPR_TPARAMETRO.cod_parametro = const$cod_parametro_sin_fvo and rownum = 1;
            EXCEPTION WHEN no_data_found THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'No existe un valor para el codigo de parametro '|| const$cod_parametro_sin_fvo);
            WHEN others THEN 
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'Ocurrió un error al intentar obtener el var$cod_parametro_sin_fvo para el código: '|| const$cod_parametro_sin_fvo);
        END;
        /*Fin Obtener parametros*/
    end sget_parametros_integracion;
    
    FUNCTION fvalidar_integracion_documento(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_documento           IN gepr_pcomon_###VERSION###.tipo$oid_)
        RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
        
        const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.fvalidar_integracion_documento';
        const$cod_parametro_tipo_plani CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchTipoPlanificacion';
        const$cod_parametro_sin_fvo CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchSinFechaValor';
        const$cod_parametro_documentos CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'EnviarDatosSwitchDocumento';
        
        var$enviar_datos_tipo_plani varchar2(4000);
        var$tipo_documento varchar2(4000);
        var$enviar_datos_switch varchar2(4000);
        var$tipo_planificacion varchar2(4000);
        var$tipo_formulario varchar2(4000);
        var$cod_canal gepr_pcomon_###VERSION###.tipo$cod_;
        var$cod_subcanal gepr_pcomon_###VERSION###.tipo$cod_;
        var$oid_planificacion gepr_pcomon_###VERSION###.tipo$oid_;
        var$registrar_documento gepr_pcomon_###VERSION###.tipo$bol_ := TRUE;
        var$existe 		    	gepr_pcomon_###VERSION###.tipo$nel_;
        var$existe_maquina_ps    	gepr_pcomon_###VERSION###.tipo$nel_;
        var$cod_actual_id   gepr_pcomon_###VERSION###.tipo$cod_;
      BEGIN

        /* Obtener parametros */
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Comienza obtención de valor de parámetros' ||
                ' const$cod_parametro_tipo_plani: ' || const$cod_parametro_tipo_plani ||
                ' const$cod_parametro_sin_fvo: ' || const$cod_parametro_sin_fvo ||
                ' const$cod_parametro_documentos: ' || const$cod_parametro_documentos,
                par$cod_identificador   => '');

        BEGIN
          SELECT DES_VALOR_PARAMETRO
          INTO var$enviar_datos_tipo_plani
          FROM GEPR_TPARAMETRO
          INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
          WHERE GEPR_TPARAMETRO.cod_parametro = const$cod_parametro_tipo_plani and rownum = 1;
          EXCEPTION WHEN others THEN
            var$enviar_datos_tipo_plani := null;
        END;
        BEGIN
          SELECT DES_VALOR_PARAMETRO
          INTO var$enviar_datos_switch
          FROM GEPR_TPARAMETRO
          INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
          WHERE GEPR_TPARAMETRO.cod_parametro = const$cod_parametro_sin_fvo and rownum = 1;
          EXCEPTION WHEN others THEN
            var$enviar_datos_switch := null;
        END;
        BEGIN
          SELECT DES_VALOR_PARAMETRO
          INTO var$tipo_documento
          FROM GEPR_TPARAMETRO
          INNER JOIN gepr_tparametro_valor ON gepr_tparametro_valor.oid_parametro = GEPR_TPARAMETRO.oid_parametro
          WHERE GEPR_TPARAMETRO.cod_parametro = const$cod_parametro_documentos and rownum = 1;
        EXCEPTION WHEN others THEN
            var$tipo_documento := null;
        END;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Finaliza obtención de valor de parámetros ' ||
                const$cod_parametro_tipo_plani || ': ' || var$enviar_datos_tipo_plani ||
                ', ' || const$cod_parametro_sin_fvo || ': ' || var$enviar_datos_switch ||
                ', ' || const$cod_parametro_documentos || ': ' || var$tipo_documento,
                par$cod_identificador   => '');
        /*Fin Obtener parametros*/

        /*Obtengo el tipo de planificacion  y el codigo de formulario*/
        SELECT TPLANI.COD_TIPO_PLANIFICACION, 
              FORMU.COD_FORMULARIO, 
              DOCU.COD_ACTUAL_ID,
              CANA.COD_CANAL,
              SUBC.COD_SUBCANAL,
              PLANXMAQ.OID_PLANIFICACION
          INTO var$tipo_planificacion, 
              var$tipo_formulario, 
              var$cod_actual_id,
              var$cod_canal,
              var$cod_subcanal,
              var$oid_planificacion
        FROM SAPR_TDOCUMENTO DOCU
        INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOCU.OID_FORMULARIO
        INNER JOIN SAPR_TCUENTA CUEN ON DOCU.OID_CUENTA_SALDO_ORIGEN = CUEN.OID_CUENTA
        INNER JOIN GEPR_TSUBCANAL SUBC ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL 
        INNER JOIN GEPR_TCANAL CANA ON SUBC.OID_CANAL = CANA.OID_CANAL
        LEFT JOIN SAPR_TPLANXMAQUINA PLANXMAQ ON PLANXMAQ.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN AND PLANXMAQ.BOL_ACTIVO = 1
        LEFT JOIN SAPR_TPLANIFICACION PLAN ON PLAN.OID_PLANIFICACION = PLANXMAQ.OID_PLANIFICACION AND PLAN.BOL_ACTIVO = 1
        LEFT JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLAN.OID_TIPO_PLANIFICACION
        WHERE DOCU.OID_DOCUMENTO = par$oid_documento ;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Se obtienen el tipo de planificacion, el código de formulario y actual_id ' ||
              'var$tipo_planificacion: ' || var$tipo_planificacion ||
              ', var$tipo_formulario: ' || var$tipo_formulario ||
              ', var$cod_actual_id: ' || var$cod_actual_id,
              par$cod_identificador   => '');

        /*Verifica si el actualid ya se encuentra registrado.*/
        SELECT COUNT(1)
          INTO var$existe
        FROM GEPR_TINTEGRACION
        WHERE OID_TABLA_INTEGRACION = var$cod_actual_id 
          AND COD_TABLA_INTEGRACION = 'COD_ACTUAL_ID'
          AND COD_ESTADO = 'AB'
          AND COD_MODULO_ORIGEN = 'GEPR'
          AND COD_MODULO_DESTINO = 'SWITCH'
          AND COD_PROCESO = 'EnviarDocumentos';
        IF var$existe > 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'El ActualId ' || var$cod_actual_id || ' ya se encuentra registrado',
                                        par$cod_identificador   => '');
            RETURN FALSE;
        END IF;
        /*Verifica tipo documento y tipo formulario*/
        IF var$tipo_documento IS NOT NULL AND INSTR(var$tipo_documento, var$tipo_formulario) > 0 THEN

                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Tipo de documento: ' || var$tipo_formulario || ' se encuentra dentro de los configurados por parámetro: ' || var$tipo_documento,
                                        par$cod_identificador   => '');

                /*Validamos que la maquina se encuentre asociado al punto de servicio o que el PUNTO sea de la cuenta PROSEGUR*/
                SELECT CASE WHEN PUSE.OID_MAQUINA = MAQU.OID_MAQUINA OR CODAJ_PTO.COD_AJENO = gepr_pcomon_###VERSION###.const$CodPtoServicioProsegur THEN 1 ELSE 0 END 
                INTO var$existe_maquina_ps   
                FROM SAPR_TDOCUMENTO DOCU
                INNER JOIN SAPR_TCUENTA CUEN ON DOCU.OID_CUENTA_SALDO_ORIGEN = CUEN.OID_CUENTA 
                INNER JOIN GEPR_TSECTOR SECT ON CUEN.OID_SECTOR = SECT.OID_SECTOR 
                INNER JOIN SAPR_TMAQUINA MAQU ON SECT.OID_SECTOR  = MAQU.OID_SECTOR 
                LEFT JOIN GEPR_TPUNTO_SERVICIO PUSE ON CUEN.OID_PTO_SERVICIO = PUSE.OID_PTO_SERVICIO
                LEFT JOIN GEPR_TCODIGO_AJENO CODAJ_PTO ON  CODAJ_PTO.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' AND
                                                          CODAJ_PTO.COD_IDENTIFICADOR = 'MAE' AND
                                                          CODAJ_PTO.OID_TABLA_GENESIS = PUSE.OID_PTO_SERVICIO
                WHERE DOCU.OID_DOCUMENTO = par$oid_documento;

                IF var$existe_maquina_ps  = 0 THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                    par$des_origen      => const$des_origen,
                                    par$des_version     => const$version,                            
                                    par$des_detalle     => 'El DeviceID no se encuentra asociado a un punto de servicio.',
                                    par$cod_identificador   => '');
                    var$registrar_documento := FALSE; 
                /*Verifico si la maquina es fecha valor*/
                ELSIF var$oid_planificacion IS NOT NULL THEN

                      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                    par$des_origen      => const$des_origen,
                                    par$des_version     => const$version,                            
                                    par$des_detalle     => 'La máquina está en fecha valor',
                                    par$cod_identificador   => '');

                      IF var$enviar_datos_tipo_plani IS NOT NULL AND INSTR(var$enviar_datos_tipo_plani, var$tipo_planificacion) > 0 THEN

                          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                                  par$des_origen      => const$des_origen,
                                                  par$des_version     => const$version,                            
                                                  par$des_detalle     => 'Tipo de planificación: ' || var$tipo_planificacion || ' se encuentra dentro de los configurados por parámetro: ' || var$enviar_datos_tipo_plani,
                                                  par$cod_identificador   => '');

                          /*Validamos los subcanales de la planificación coincidiendo con el del documento*/
                          SELECT COUNT(1)
                          INTO var$existe 
                          FROM SAPR_TPLANIFICACION PL
                                INNER JOIN SAPR_TPLANXCANAL PC ON PL.OID_PLANIFICACION = PC.OID_PLANIFICACION AND PC.BOL_ACTIVO = 1
                                INNER JOIN GEPR_TCANAL C ON C.OID_CANAL = PC.OID_CANAL AND C.BOL_VIGENTE = 1
                                INNER JOIN SAPR_TPLANXSUBCANAL PSC ON PSC.OID_PLANXCANAL = PC.OID_PLANXCANAL AND PSC.BOL_ACTIVO = 1
                                INNER JOIN GEPR_TSUBCANAL SUBC ON SUBC.OID_SUBCANAL = PSC.OID_SUBCANAL AND SUBC.BOL_VIGENTE = 1
                          WHERE PL.OID_PLANIFICACION = var$oid_planificacion
                          AND PL.BOL_ACTIVO = 1
                          AND  (C.COD_CANAL = var$cod_canal AND SUBC.COD_SUBCANAL = var$cod_subcanal) ;

                          IF var$existe  = 0 THEN
                            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen      => const$des_origen,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'El subcanal del documento de cod_subcanal: ' || var$cod_subcanal || ', no coincide con los configurados en la planificación.',
                                            par$cod_identificador   => '');
                            var$registrar_documento := FALSE; 

                          END IF;

                        ELSE
                          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                                  par$des_origen      => const$des_origen,
                                                  par$des_version     => const$version,                            
                                                  par$des_detalle     => 'Tipo de planificación: ' || var$tipo_planificacion || ' no se encuentra dentro de los configurados por parámetro: ' || var$enviar_datos_tipo_plani,
                                                  par$cod_identificador   => '');
                          var$registrar_documento := FALSE; 
                        END IF;

                ELSE
                      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                      par$des_origen      => const$des_origen,
                                      par$des_version     => const$version,                            
                                      par$des_detalle     => 'La máquina no está en fecha valor',
                                      par$cod_identificador   => '');
                      var$registrar_documento := FALSE;
                      IF var$enviar_datos_switch = 1 THEN

                          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                                  par$des_origen      => const$des_origen,
                                                  par$des_version     => const$version,                            
                                                  par$des_detalle     => 'Se almacena en la tabla de integración por estar habilitado el parámetro: ' || const$cod_parametro_sin_fvo,
                                                  par$cod_identificador   => '');
                          var$registrar_documento := TRUE;
                          
                      END IF;

                END IF;
        ELSE 

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Tipo de documento: ' || var$tipo_formulario || ' no se encuentra dentro de los configurados por parámetro: ' || var$tipo_documento,
                            par$cod_identificador   => '');

            var$registrar_documento := FALSE;

        END IF;

        RETURN var$registrar_documento;

  END fvalidar_integracion_documento;


 END sapr_pdocumento_###VERSION###;
/