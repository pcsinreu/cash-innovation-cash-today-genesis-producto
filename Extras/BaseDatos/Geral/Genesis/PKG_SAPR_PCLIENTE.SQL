/*
    Es responsable por recuperar Clientes, SubClientes, Puntos de Servicios y sus datos.
*/
CREATE OR REPLACE PACKAGE SAPR_PCLIENTE_###VERSION### AS
	/*Version: ###VERSION_COMP###*/

	const$codFuncionalidad	 CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PCLIENTE_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
  const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
  const$comilla_simple  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);

	/* Recuperar informaciones del movimientos */
	PROCEDURE srecuperar_datosentidades (par$pag_indice					 IN	  gepr_pcomon_###VERSION###.tipo$nel_,
													 par$pag_reg_pagina				 IN	  gepr_pcomon_###VERSION###.tipo$nel_,
													 par$cod_cultura					 IN	  gepr_pcomon_###VERSION###.tipo$cod_,
													 par$cod_identificador_ajeno	 IN	  gepr_pcomon_###VERSION###.tipo$cod_,
													 par$cod_clientes 				 IN	  gepr_pcomon_###VERSION###.tipo$cods_,
													 par$des_clientes 				 IN	  gepr_pcomon_###VERSION###.tipo$descs_,
													 par$cod_subclientes 			 IN	  gepr_pcomon_###VERSION###.tipo$cods_,
													 par$des_subclientes 			 IN	  gepr_pcomon_###VERSION###.tipo$descs_,
													 par$cod_ptoservs 				 IN	  gepr_pcomon_###VERSION###.tipo$cods_,
													 par$des_ptoservs 				 IN	  gepr_pcomon_###VERSION###.tipo$descs_,
													 par$nivel							 IN	  gepr_pcomon_###VERSION###.tipo$cod_,
													 par$bol_recdatbancs 			 IN	  gepr_pcomon_###VERSION###.tipo$nbol_,
													 par$bol_rec_codaje				 IN	  gepr_pcomon_###VERSION###.tipo$nbol_,
													 par$rc_clientes						 OUT SYS_REFCURSOR,
													 par$rc_subclientes					 OUT SYS_REFCURSOR,
													 par$rc_ptos_serv 					 OUT SYS_REFCURSOR,
													 par$rc_dat_banc						 OUT SYS_REFCURSOR,
													 par$rc_cod_ajenos					 OUT SYS_REFCURSOR,
													 par$rc_validaciones 				 OUT SYS_REFCURSOR);

	/* Valida los codigos de entidades */
	PROCEDURE svalidar_codigos (par$cod_cultura					 IN	  gepr_pcomon_###VERSION###.tipo$cod_,
										 par$cod_identificador_ajeno	 IN	  gepr_pcomon_###VERSION###.tipo$cod_,
										 par$cod_clientes 				 IN	  gepr_pcomon_###VERSION###.tipo$cods_,
										 par$des_clientes 				 IN	  gepr_pcomon_###VERSION###.tipo$descs_,
										 par$cod_subclientes 			 IN	  gepr_pcomon_###VERSION###.tipo$cods_,
										 par$des_subclientes 			 IN	  gepr_pcomon_###VERSION###.tipo$descs_,
										 par$cod_ptoservs 				 IN	  gepr_pcomon_###VERSION###.tipo$cods_,
										 par$des_ptoservs 				 IN	  gepr_pcomon_###VERSION###.tipo$descs_,
										 par$nivel							 IN	  gepr_pcomon_###VERSION###.tipo$cod_,
										 par$rc_validaciones 				 OUT gepr_pcomon_###VERSION###.tipo$descs_);

	PROCEDURE sconfigurar_cliente (
                       par$oid_llamada                IN gepr_pcomon_###VERSION###.tipo$oid_,
                       par$cod_identificador_ajeno	  IN		gepr_pcomon_###VERSION###.tipo$cod_,
											 /*Cliente*/
											 par$acod_accion					  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$anel_index					  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$acod_cliente 				  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$ades_descripcion			  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$acod_tipo_cliente			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$acod_codigo_bancario		  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$abol_banco_capital 		  IN		gepr_pcomon_###VERSION###.tipo$nbols_,
											 par$abol_banco_comision		  IN		gepr_pcomon_###VERSION###.tipo$nbols_,
											 par$anel_porc_comision 		  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 /*Subcliente*/
											 par$ascli_cod_accion			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$ascli_nel_index 			  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$ascli_nel_index_scli		  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$ascli_cod_subcliente		  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$ascli_des_descripcion 	  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 /*Punto Servicio*/
											 par$apser_cod_accion			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$apser_nel_index 			  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$apser_nel_index_scli		  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$apser_nel_index_pto		  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$apser_cod_pto_servicio	  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$apser_des_pto_servicio	  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 /*Datos Bancarios*/
											 par$aban_cod_accion 			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aban_cod_entidad			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aban_nel_index_entidad	  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$aban_oid_identificador	  IN		gepr_pcomon_###VERSION###.tipo$oids_,
											 par$aban_cod_codigobanco		  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aban_cod_codigoagencia	  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aban_cod_numerocuenta 	  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aban_cod_tipo_cuenta		  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aban_cod_documento 		  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aban_des_titularidad		  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_cod_divisa 			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aban_des_observaciones	  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_bol_defecto			  IN		gepr_pcomon_###VERSION###.tipo$nbols_,
											 par$aban_des_campoadicional1   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_des_campoadicional2   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_des_campoadicional3   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_des_campoadicional4   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_des_campoadicional5   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_des_campoadicional6   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_des_campoadicional7   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aban_des_campoadicional8   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 /*Codigo Ajeno*/
											 par$aajen_cod_accion			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aajen_cod_entidad			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aajen_nel_index_entidad	  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$aajen_cod_identificador	  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aajen_cod_codigo_ajeno	  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$aajen_cod_desc_ajeno		  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$aajen_bol_defecto			  IN		gepr_pcomon_###VERSION###.tipo$nbols_,
											 /*Direcciones*/
											 par$adir_cod_entidad			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$adir_nel_index_entidad	  IN		gepr_pcomon_###VERSION###.tipo$nels_,
											 par$adir_des_pais				  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_prov				  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_ciudad 			  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_num_tel			  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_email				  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_cod_fiscal 			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$adir_cod_postal 			  IN		gepr_pcomon_###VERSION###.tipo$cods_,
											 par$adir_des_dic_1				  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_dic_2				  IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_cam_adicional_1   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_cam_adicional_2   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_cam_adicional_3   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_cat_adicional_1   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_cat_adicional_2   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$adir_des_cat_adicional_3   IN		gepr_pcomon_###VERSION###.tipo$descs_,
											 par$cod_cultura					  IN		gepr_pcomon_###VERSION###.tipo$cod_,
											 par$cod_usuario					  IN		gepr_pcomon_###VERSION###.tipo$cod_,
											 par$cod_pais						  IN		gepr_pcomon_###VERSION###.tipo$cod_,
											 par$info_ejecucion				  IN		VARCHAR2,
											 par$rc_validaciones 				  OUT SYS_REFCURSOR,
											 par$cod_ejecucion					  OUT gepr_pcomon_###VERSION###.tipo$nel_);

	PROCEDURE sconfigurar_subcliente (par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_identificador_ajeno	  IN gepr_pcomon_###VERSION###.tipo$cod_,
												 /*Subcliente*/
												 par$ascli_cod_accion			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$ascli_nel_index 			  IN gepr_pcomon_###VERSION###.tipo$nels_,
												 par$ascli_nel_index_scli		  IN gepr_pcomon_###VERSION###.tipo$nels_,
												 par$ascli_oid_cliente			  IN gepr_pcomon_###VERSION###.tipo$oids_,
												 par$ascli_cod_subcliente		  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$ascli_des_descripcion 	  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 /*Datos Bancarios*/
												 par$aban_cod_accion 			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aban_cod_entidad			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aban_nel_index_entidad	  IN gepr_pcomon_###VERSION###.tipo$nels_,
												 par$aban_oid_identificador	  IN gepr_pcomon_###VERSION###.tipo$oids_,
												 par$aban_cod_codigobanco		  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aban_cod_codigoagencia	  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aban_cod_numerocuenta 	  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aban_cod_tipo_cuenta		  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aban_cod_documento 		  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aban_des_titularidad		  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_cod_divisa 			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aban_des_observaciones	  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_bol_defecto			  IN gepr_pcomon_###VERSION###.tipo$nbols_,
												 par$aban_des_campoadicional1   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_des_campoadicional2   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_des_campoadicional3   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_des_campoadicional4   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_des_campoadicional5   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_des_campoadicional6   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_des_campoadicional7   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aban_des_campoadicional8   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 /*Codigo Ajeno*/
												 par$aajen_cod_accion			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aajen_cod_entidad			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aajen_nel_index_entidad	  IN gepr_pcomon_###VERSION###.tipo$nels_,
												 par$aajen_cod_identificador	  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aajen_cod_codigo_ajeno	  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$aajen_cod_desc_ajeno		  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$aajen_bol_defecto			  IN gepr_pcomon_###VERSION###.tipo$nbols_,
												 /*Direcciones*/
												 par$adir_cod_entidad			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$adir_nel_index_entidad	  IN gepr_pcomon_###VERSION###.tipo$nels_,
												 par$adir_des_pais				  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_prov				  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_ciudad 			  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_num_tel			  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_email				  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_cod_fiscal 			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$adir_cod_postal 			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												 par$adir_des_dic_1				  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_dic_2				  IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_cam_adicional_1   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_cam_adicional_2   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_cam_adicional_3   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_cat_adicional_1   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_cat_adicional_2   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$adir_des_cat_adicional_3   IN gepr_pcomon_###VERSION###.tipo$descs_,
												 par$cod_cultura					  IN gepr_pcomon_###VERSION###.tipo$cod_,
												 par$cod_usuario					  IN gepr_pcomon_###VERSION###.tipo$cod_,
												 par$cod_pais						  IN gepr_pcomon_###VERSION###.tipo$cod_);

	PROCEDURE sconfigurar_ptoservicio (par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_identificador_ajeno 	IN gepr_pcomon_###VERSION###.tipo$cod_,
												  /*Punto Servicio*/
												  par$apser_cod_accion				IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$apser_nel_index				IN gepr_pcomon_###VERSION###.tipo$nels_,
												  par$apser_nel_index_scli 		IN gepr_pcomon_###VERSION###.tipo$nels_,
												  par$apser_nel_index_pto			IN gepr_pcomon_###VERSION###.tipo$nels_,
												  par$apser_cod_pto_servicio		IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$apser_des_pto_servicio		IN gepr_pcomon_###VERSION###.tipo$descs_,
												  /*Datos Bancarios*/
												  par$aban_cod_accion				IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aban_cod_entidad				IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aban_nel_index_entidad		IN gepr_pcomon_###VERSION###.tipo$nels_,
												  par$aban_oid_identificador		IN gepr_pcomon_###VERSION###.tipo$oids_,
												  par$aban_cod_codigobanco 		IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aban_cod_codigoagencia		IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aban_cod_numerocuenta		IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aban_cod_tipo_cuenta 		IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aban_cod_documento			IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aban_des_titularidad 		IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_cod_divisa				IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aban_des_observaciones		IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_bol_defecto				IN gepr_pcomon_###VERSION###.tipo$nbols_,
												  par$aban_des_campoadicional1	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_des_campoadicional2	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_des_campoadicional3	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_des_campoadicional4	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_des_campoadicional5	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_des_campoadicional6	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_des_campoadicional7	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aban_des_campoadicional8	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  /*Codigo Ajeno*/
												  par$aajen_cod_accion				IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aajen_cod_entidad 			IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aajen_nel_index_entidad 	IN gepr_pcomon_###VERSION###.tipo$nels_,
												  par$aajen_cod_identificador 	IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aajen_cod_codigo_ajeno		IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aajen_cod_desc_ajeno 		IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aajen_bol_defecto 			IN gepr_pcomon_###VERSION###.tipo$nbols_,
												  /*Direcciones*/
												  par$adir_cod_entidad				IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$adir_nel_index_entidad		IN gepr_pcomon_###VERSION###.tipo$nels_,
												  par$adir_des_pais					IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_prov					IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_ciudad				IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_num_tel				IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_email 				IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_cod_fiscal				IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$adir_cod_postal				IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$adir_des_dic_1 				IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_dic_2 				IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_cam_adicional_1	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_cam_adicional_2	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_cam_adicional_3	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_cat_adicional_1	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_cat_adicional_2	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$adir_des_cat_adicional_3	IN gepr_pcomon_###VERSION###.tipo$descs_,
												  par$cod_cultura 					IN gepr_pcomon_###VERSION###.tipo$cod_,
												  par$cod_usuario 					IN gepr_pcomon_###VERSION###.tipo$cod_,
												  par$cod_pais 						IN gepr_pcomon_###VERSION###.tipo$cod_);

	PROCEDURE svalidar_cliente (par$oid_llamada IN gepr_pcomon_###VERSION###.tipo$oid_,
                     par$cod_accion			IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
										 par$nel_index 			IN 	 gepr_pcomon_###VERSION###.tipo$nel_,
										 par$cod_cliente			IN 	 gepr_pcomon_###VERSION###.tipo$cod_,
										 par$des_descripcion 	IN 	 gepr_pcomon_###VERSION###.tipo$desc_,
										 par$cod_tipo_cliente	IN 	 gepr_pcomon_###VERSION###.tipo$cod_,
										 par$cod_cultura			IN 	 gepr_pcomon_###VERSION###.tipo$cod_);



	PROCEDURE svalidar_subcliente (par$oid_llamada           IN gepr_pcomon_###VERSION###.tipo$oid_,
                       par$scli_acod_accion		 IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
											 par$scli_nel_index			 IN	  gepr_pcomon_###VERSION###.tipo$nel_,
											 par$scli_nel_index_scli	 IN	  gepr_pcomon_###VERSION###.tipo$nel_,
											 par$scli_cod_subcliente	 IN	  gepr_pcomon_###VERSION###.tipo$cod_,
											 par$scli_des_descripcion	 IN	  gepr_pcomon_###VERSION###.tipo$desc_,
											 par$cod_cultura				 IN	  gepr_pcomon_###VERSION###.tipo$cod_);

	PROCEDURE svalidar_punto_servicio (
                          par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$pser_acod_accion			IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
												  par$pser_nel_index 			IN 	 gepr_pcomon_###VERSION###.tipo$nel_,
												  par$pser_nel_index_scli		IN 	 gepr_pcomon_###VERSION###.tipo$nel_,
												  par$pser_nel_index_pto		IN 	 gepr_pcomon_###VERSION###.tipo$nel_,
												  par$pser_cod_pto_servicio	IN 	 gepr_pcomon_###VERSION###.tipo$cod_,
												  par$pser_des_pto_servicio	IN 	 gepr_pcomon_###VERSION###.tipo$desc_,
												  par$cod_cultura 				IN 	 gepr_pcomon_###VERSION###.tipo$cod_);


	PROCEDURE svalidar_codigos_ajenos (par$oid_llamada  IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$aajen_cod_accion			  IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aajen_oid_entidad 		  IN gepr_pcomon_###VERSION###.tipo$oids_,
												  par$aajen_cod_entidad 		  IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aajen_nel_index_entidad   IN gepr_pcomon_###VERSION###.tipo$nels_,
												  par$aajen_nel_index_padre	  IN gepr_pcomon_###VERSION###.tipo$nels_,
												  par$aajen_cod_identificador   IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aajen_cod_codigo_ajeno	  IN gepr_pcomon_###VERSION###.tipo$cods_,
												  par$aajen_cod_desc_ajeno 	  IN OUT gepr_pcomon_###VERSION###.tipo$descs_,
												  par$aajen_bol_defecto 		  IN OUT gepr_pcomon_###VERSION###.tipo$nbols_,
												  par$cod_cultura 				  IN gepr_pcomon_###VERSION###.tipo$cod_);

	PROCEDURE supd_direccion (par$oid_direccion			 IN gepr_pcomon_###VERSION###.tipo$oid_,
									  par$oid_tabla_gen			 IN gepr_pcomon_###VERSION###.tipo$oid_,
									  par$cod_tipo_tgen			 IN gepr_pcomon_###VERSION###.tipo$cod_,
									  par$des_pais 				 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_prov 				 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_ciudad				 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_num_tel 			 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_email				 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$cod_fiscal				 IN gepr_pcomon_###VERSION###.tipo$cod_,
									  par$cod_postal				 IN gepr_pcomon_###VERSION###.tipo$cod_,
									  par$des_dic_1				 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_dic_2				 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_cam_adicional_1	 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_cam_adicional_2	 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_cam_adicional_3	 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_cat_adicional_1	 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_cat_adicional_2	 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$des_cat_adicional_3	 IN gepr_pcomon_###VERSION###.tipo$desc_,
									  par$cod_usuario 			 IN gepr_pcomon_###VERSION###.tipo$cod_);

	PROCEDURE supd_cliente (par$oid_cliente				IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
									par$cod_cliente				IN 	 gepr_pcomon_###VERSION###.tipo$cod_,
									par$des_cliente				IN 	 gepr_pcomon_###VERSION###.tipo$desc_,
									par$oid_tipo_cliente 		IN 	 gepr_pcomon_###VERSION###.tipo$oid_,
									par$cod_bancario				IN 	 gepr_pcomon_###VERSION###.tipo$cod_,
									par$bol_vigente				IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
									par$bol_totaliza_saldo		IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
									par$bol_abona_por_total 	IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
									par$bol_banco_capital		IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
									par$bol_banco_comision		IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
									par$num_percent_comision	IN 	 gepr_pcomon_###VERSION###.tipo$nel_,
									par$cod_usuario				IN 	 gepr_pcomon_###VERSION###.tipo$desc_);

	PROCEDURE supd_subcliente (par$oid_subcliente		  IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
										par$oid_cliente			  IN		gepr_pcomon_###VERSION###.tipo$oid_,
										par$cod_subcliente		  IN		gepr_pcomon_###VERSION###.tipo$cod_,
										par$des_subcliente		  IN		gepr_pcomon_###VERSION###.tipo$desc_,
										par$oid_tipo_subcliente   IN		gepr_pcomon_###VERSION###.tipo$oid_,
										par$bol_enviado_saldos	  IN		gepr_pcomon_###VERSION###.tipo$nbol_,
										par$bol_vigente			  IN		gepr_pcomon_###VERSION###.tipo$nbol_,
										par$bol_totaliza_saldo	  IN		gepr_pcomon_###VERSION###.tipo$nbol_,
										par$cod_usuario			  IN		gepr_pcomon_###VERSION###.tipo$desc_);

	PROCEDURE supd_pto_servicio (par$oid_pto_servicio			IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
										  par$oid_subcliente 			IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
										  par$cod_pto_servicio			IN 	 gepr_pcomon_###VERSION###.tipo$cod_,
										  par$des_pto_servicio			IN 	 gepr_pcomon_###VERSION###.tipo$desc_,
										  par$oid_tipo_pto_servicio	IN 	 gepr_pcomon_###VERSION###.tipo$oid_,
										  par$bol_vigente 				IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
										  par$bol_totaliza_saldo		IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
										  par$cod_usuario 				IN 	 gepr_pcomon_###VERSION###.tipo$desc_);


	PROCEDURE supd_codigo_ajeno (par$oid_pto_servicio			IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
										  par$oid_subcliente 			IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
										  par$cod_pto_servicio			IN 	 gepr_pcomon_###VERSION###.tipo$cod_,
										  par$des_pto_servicio			IN 	 gepr_pcomon_###VERSION###.tipo$desc_,
										  par$oid_tipo_pto_servicio	IN 	 gepr_pcomon_###VERSION###.tipo$oid_,
										  par$bol_vigente 				IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
										  par$bol_totaliza_saldo		IN 	 gepr_pcomon_###VERSION###.tipo$nbol_,
										  par$cod_usuario 				IN 	 gepr_pcomon_###VERSION###.tipo$desc_);

	/*Función ¿Hay error?*/
	FUNCTION fhay_error
		RETURN BOOLEAN;

	FUNCTION fhay_error_configurar_cliente (par$entidad IN gepr_pcomon_###VERSION###.tipo$desc_, par$nel_index IN gepr_pcomon_###VERSION###.tipo$nel_)
		RETURN BOOLEAN;


      /* Configurar AcuerdosServicio */
  PROCEDURE sconfigurar_AcuerdosServicio(
                                     par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$anel_index              IN gepr_pcomon_###VERSION###.tipo$nels_,
                                     par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$acod_accion             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$ades_ContractId         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_serviceOrderID     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_serviceOrderCode   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_ProductCode        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$acod_cliente            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$acod_sub_cliente        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$acod_punto_servicio     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$afyh_vigencia_inicio    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                     par$afyh_vigencia_fin       IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                     par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_validaciones         OUT sys_refcursor);

  /*Procedure de validar acuerdos servicios*/
  PROCEDURE svalidar_acuerdosservicios(par$oid_llamada           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$anel_index              IN gepr_pcomon_###VERSION###.tipo$nels_,
                                     par$acod_accion             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$ades_ContractId         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_serviceOrderID     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_serviceOrderCode   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_ProductCode        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$acod_cliente            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$acod_sub_cliente        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$acod_punto_servicio     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$afyh_vigencia_inicio    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                     par$afyh_vigencia_fin       IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                     par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_pais                OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$aoid_cliente            OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$aoid_subcliente         OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$aoid_pto_servicio       OUT gepr_pcomon_###VERSION###.tipo$oids_);

  /*Función que corrobora si hay error en un indice de AcuerdosServicios*/
  FUNCTION fhay_error_acuerdo_servicio(par$indice IN gepr_pcomon_###VERSION###.tipo$nel_) RETURN BOOLEAN;

END SAPR_PCLIENTE_###VERSION###;
/
create or replace PACKAGE BODY SAPR_PCLIENTE_###VERSION### AS

  PROCEDURE srecuperar_datosentidades
  (
    par$pag_indice              IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$pag_reg_pagina          IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_clientes            IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$des_clientes            IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$cod_subclientes         IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$des_subclientes         IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$cod_ptoservs            IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$des_ptoservs            IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$nivel                   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$bol_recdatbancs         IN gepr_pcomon_###VERSION###.tipo$nbol_,
    par$bol_rec_codaje          IN gepr_pcomon_###VERSION###.tipo$nbol_,
    par$rc_clientes             OUT sys_refcursor,
    par$rc_subclientes          OUT sys_refcursor,
    par$rc_ptos_serv            OUT sys_refcursor,
    par$rc_dat_banc             OUT sys_refcursor,
    par$rc_cod_ajenos           OUT sys_refcursor,
    par$rc_validaciones         OUT sys_refcursor
  ) IS

  var$rc_validaciones gepr_pcomon_###VERSION###.tipo$descs_;
    var$filtro gepr_pcomon_###VERSION###.tipo$obs_;
    var$filtro_dato_banc gepr_pcomon_###VERSION###.tipo$obs_;
    var$query gepr_pcomon_###VERSION###.tipo$obs_;
    var$hay_filtro gepr_pcomon_###VERSION###.tipo$nbol_;
  BEGIN
    OPEN par$rc_clientes FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_subclientes FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_ptos_serv FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_dat_banc FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_cod_ajenos  FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
    OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    DELETE SAPR_GTT_TAUXILIAR;
    DELETE SAPR_GTT_TOIDS;
    COMMIT;

    svalidar_codigos(par$cod_cultura, par$cod_identificador_ajeno, par$cod_clientes, par$des_clientes, par$cod_subclientes, par$des_subclientes,par$cod_ptoservs, par$des_ptoservs, par$nivel, var$rc_validaciones);

    --COMIENZA VALIDACION DE FILTROS EXISTENTES
    IF (par$cod_clientes is not null and par$cod_clientes.count > 1) or (par$des_clientes is not null and par$des_clientes.count > 1) then
        var$hay_filtro := 0;
      FOR idx IN par$cod_clientes.first .. par$cod_clientes.last
      LOOP
        IF par$cod_clientes(idx) IS NOT NULL THEN
          var$hay_filtro := 1;
        END IF;
      END LOOP;
      
      FOR idx IN par$des_clientes.first .. par$des_clientes.last
      LOOP
        IF par$des_clientes(idx) IS NOT NULL THEN
          var$hay_filtro := 1;
        END IF;
      END LOOP;
      IF (var$hay_filtro = 1) then
        var$filtro := var$filtro || q'[INNER JOIN SAPR_GTT_TAUXILIAR AUXCLI on AUXCLI.OID_CAMPO1 = CLI.OID_CLIENTE  AND AUXCLI.COD_CALIFICADOR = 'OID_CLIENTE' 
        ]';
        var$filtro_dato_banc := var$filtro_dato_banc || q'[INNER JOIN SAPR_GTT_TAUXILIAR AUXCLI on AUXCLI.OID_CAMPO1 = DB.OID_CLIENTE  AND AUXCLI.COD_CALIFICADOR = 'OID_CLIENTE'
        ]';
      END IF;
    END IF;
    IF (par$cod_subclientes is not null and par$cod_subclientes.count > 1) or (par$des_subclientes is not null and par$des_subclientes.count > 1) then
      var$hay_filtro := 0;
      FOR idx IN par$cod_subclientes.first .. par$cod_subclientes.last
      LOOP
        IF par$cod_subclientes(idx) IS NOT NULL THEN
          var$hay_filtro := 1;
        END IF;
      END LOOP;
      
      FOR idx IN par$des_subclientes.first .. par$des_subclientes.last
      LOOP
        IF par$des_subclientes(idx) IS NOT NULL THEN
          var$hay_filtro := 1;
        END IF;
      END LOOP;
      IF (var$hay_filtro = 1) then
        var$filtro := var$filtro || q'[INNER JOIN SAPR_GTT_TAUXILIAR AUXSUBC on AUXSUBC.OID_CAMPO1 = SUBC.OID_SUBCLIENTE  AND AUXSUBC.COD_CALIFICADOR = 'OID_SUBCLIENTE' 
        ]';
        var$filtro_dato_banc := var$filtro_dato_banc || q'[INNER JOIN SAPR_GTT_TAUXILIAR AUXSUBC on AUXSUBC.OID_CAMPO1 = DB.OID_SUBCLIENTE  AND AUXSUBC.COD_CALIFICADOR = 'OID_SUBCLIENTE'
        ]';
      END IF;
    END IF;
    IF (par$cod_ptoservs is not null and par$cod_ptoservs.count > 1) or (par$des_ptoservs is not null and par$des_ptoservs.count > 1) then
      var$hay_filtro := 0;
      FOR idx IN par$cod_ptoservs.first .. par$cod_ptoservs.last
      LOOP
        IF par$cod_ptoservs(idx) IS NOT NULL THEN
          var$hay_filtro := 1;
        END IF;
      END LOOP;
      
      FOR idx IN par$des_ptoservs.first .. par$des_ptoservs.last
      LOOP
        IF par$des_ptoservs(idx) IS NOT NULL THEN
          var$hay_filtro := 1;
        END IF;
      END LOOP;
      IF (var$hay_filtro = 1) then
          var$filtro := var$filtro || q'[INNER JOIN SAPR_GTT_TAUXILIAR AUXPTO on AUXPTO.OID_CAMPO1 = PTO.OID_PTO_SERVICIO  AND AUXPTO.COD_CALIFICADOR = 'OID_PTO_SERVICIO' 
          ]';
          var$filtro_dato_banc := var$filtro_dato_banc || q'[INNER JOIN SAPR_GTT_TAUXILIAR AUXPTO on AUXPTO.OID_CAMPO1 = DB.OID_PTO_SERVICIO  AND AUXPTO.COD_CALIFICADOR = 'OID_PTO_SERVICIO'
          ]';
      END IF;
    END IF;
    --FIN VALIDACION DE FILTROS EXISTENTES

    DBMS_OUTPUT.PUT_LINE('var$query: ' || var$query);
    IF NOT fhay_error THEN

      --QUERY DE CLIENTES
      var$query := q'[SELECT DISTINCT CLI.OID_CLIENTE  "IDENTIFICADOR",
              CLI.COD_CLIENTE "CODIGO",
              CLI.DES_CLIENTE "DESCRIPCION",
              TIPO.DES_TIPO_CLIENTE "TIPO",
              CLI.COD_BANCARIO "CODIGOBANCARIO",
              CLI.BOL_VIGENTE "VIGENTE"
        FROM GEPR_TCLIENTE CLI
        LEFT JOIN GEPR_TTIPO_CLIENTE TIPO on TIPO.OID_TIPO_CLIENTE = CLI.OID_TIPO_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SUBC ON SUBC.OID_CLIENTE = CLI.OID_CLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_SUBCLIENTE = SUBC.OID_SUBCLIENTE 
        ]' || var$filtro;

      DBMS_OUTPUT.PUT_LINE('var$query de cliente: ' || var$query);
      --CURSOR DE CLIENTES
      OPEN par$rc_clientes FOR
        var$query;

      --QUERY DE SUBCLIENTES
      var$query := q'[SELECT DISTINCT SUBC.OID_SUBCLIENTE  "IDENTIFICADOR",
              SUBC.COD_SUBCLIENTE "CODIGO",
              SUBC.DES_SUBCLIENTE "DESCRIPCION",
              SUBC.BOL_VIGENTE "VIGENTE",
              SUBC.OID_CLIENTE "OID_CLIENTE",
              CLI.COD_CLIENTE "COD_CLIENTE"
        FROM GEPR_TCLIENTE CLI
        LEFT JOIN GEPR_TTIPO_CLIENTE TIPO on TIPO.OID_TIPO_CLIENTE = CLI.OID_TIPO_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SUBC ON SUBC.OID_CLIENTE = CLI.OID_CLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_SUBCLIENTE = SUBC.OID_SUBCLIENTE 
        ]' || var$filtro;

      DBMS_OUTPUT.PUT_LINE('var$query de subcliente: ' || var$query);
      --CURSOR DE SUBCLIENTES
      OPEN par$rc_subclientes FOR
        var$query;

      --QUERY DE PUNTOS
      var$query := q'[SELECT DISTINCT PTO.OID_PTO_SERVICIO  "IDENTIFICADOR",
              PTO.COD_PTO_SERVICIO "CODIGO",
              PTO.DES_PTO_SERVICIO "DESCRIPCION",
              PTO.BOL_VIGENTE "VIGENTE",
              PTO.OID_SUBCLIENTE "OID_SUBCLIENTE",
              SUBC.COD_SUBCLIENTE "COD_SUBCLIENTE",
              CLI.OID_CLIENTE "OID_CLIENTE",
              CLI.COD_CLIENTE "COD_CLIENTE"
        FROM GEPR_TCLIENTE CLI
        LEFT JOIN GEPR_TTIPO_CLIENTE TIPO on TIPO.OID_TIPO_CLIENTE = CLI.OID_TIPO_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SUBC ON SUBC.OID_CLIENTE = CLI.OID_CLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_SUBCLIENTE = SUBC.OID_SUBCLIENTE 
        ]' || var$filtro;

      DBMS_OUTPUT.PUT_LINE('var$query de puntos: ' || var$query);
      --CURSOR DE PUNTOS
      OPEN par$rc_ptos_serv FOR
        var$query;

      IF par$bol_recdatbancs = 1 OR  par$bol_rec_codaje = 1 THEN
        --Almaceno los OID de los padres FALTANTES
        --SUBCLIENTES
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
        SELECT OID_SUBCLIENTE, 'OID_SUBCLIENTE' FROM GEPR_TPUNTO_SERVICIO 
        WHERE OID_PTO_SERVICIO IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO')
        AND OID_SUBCLIENTE NOT IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE');
        --CLIENTES
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
        SELECT OID_CLIENTE, 'OID_CLIENTE' FROM GEPR_TSUBCLIENTE 
        WHERE OID_SUBCLIENTE IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE')
        AND OID_CLIENTE NOT IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_CLIENTE');
        
        --Almaceno los OID de los hijos FALTANTES
        --SUBCLIENTES
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
        SELECT OID_SUBCLIENTE, 'OID_SUBCLIENTE' FROM GEPR_TSUBCLIENTE SUBCLI
        INNER JOIN GEPR_TCLIENTE CLI ON CLI.OID_CLIENTE = SUBCLI.OID_CLIENTE
        WHERE CLI.OID_CLIENTE IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_CLIENTE')
        AND OID_SUBCLIENTE NOT IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE');
        --PUNTOS
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
        SELECT OID_PTO_SERVICIO, 'OID_PTO_SERVICIO' FROM GEPR_TPUNTO_SERVICIO PTO
        INNER JOIN GEPR_TSUBCLIENTE SUBCLI ON SUBCLI.OID_SUBCLIENTE = PTO.OID_SUBCLIENTE
        WHERE SUBCLI.OID_SUBCLIENTE IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE')
        AND OID_PTO_SERVICIO NOT IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO');
      END IF;

      --CURSOR DE DATO BANCARIO
      IF par$bol_recdatbancs = 1 THEN
            --QUERY DE DATO BANCARIO
            var$query := q'[SELECT DISTINCT
                DB.OID_DATO_BANCARIO "IDENTIFICADOR",
                CLI.COD_CLIENTE "CODIGOBANCO",
                DB.COD_AGENCIA "CODIGOAGENCIA",
                DB.COD_CUENTA_BANCARIA "NUMEROCUENTA",
                DB.COD_TIPO_CUENTA_BANCARIA "TIPO",
                DB.COD_DOCUMENTO "NUMERODOCUMENTO",
                DB.DES_TITULARIDAD "TITULARIDAD",
                DIVISA.COD_ISO_DIVISA "CODIGODIVISA",
                DB.DES_OBSERVACIONES "OBSERVACIONES",
                DB.BOL_DEFECTO "PATRON",
                DB.BOL_ACTIVO "VIGENTE",
                DB.DES_CAMPO_ADICIONAL_1 "CAMPOADICIONAL1",
                DB.DES_CAMPO_ADICIONAL_2 "CAMPOADICIONAL2",
                DB.DES_CAMPO_ADICIONAL_3 "CAMPOADICIONAL3",
                DB.DES_CAMPO_ADICIONAL_4 "CAMPOADICIONAL4",
                DB.DES_CAMPO_ADICIONAL_5 "CAMPOADICIONAL5",
                DB.DES_CAMPO_ADICIONAL_6 "CAMPOADICIONAL6",
                DB.DES_CAMPO_ADICIONAL_7 "CAMPOADICIONAL7",
                DB.DES_CAMPO_ADICIONAL_8 "CAMPOADICIONAL8",
                      DB.OID_BANCO "OID_BANCO",
                      DB.OID_CLIENTE "OID_CLIENTE",
                DB.OID_SUBCLIENTE "OID_SUBCLIENTE",
                DB.OID_PTO_SERVICIO "OID_PTO_SERVICIO"
              FROM SAPR_TDATO_BANCARIO DB
                    INNER JOIN GEPR_TCLIENTE CLI ON CLI.OID_CLIENTE = DB.OID_BANCO
              INNER JOIN GEPR_TDIVISA DIVISA ON DIVISA.OID_DIVISA = DB.OID_DIVISA
              ]' || var$filtro_dato_banc;
            DBMS_OUTPUT.PUT_LINE('var$query de datos bancarios: ' || var$query);

          OPEN par$rc_dat_banc FOR
                var$query;
      END IF;

      IF par$bol_rec_codaje = 1 THEN
        OPEN par$rc_cod_ajenos FOR
          SELECT
            CA.OID_CODIGO_AJENO "IDENTIFICADOR",
            CA.COD_IDENTIFICADOR "CODIGOIDENTIFICADOR",
            CA.COD_AJENO "CODIGO",
            CA.DES_AJENO "DESCRIPCION",
            CA.BOL_DEFECTO "PATRON",
            CA.BOL_ACTIVO "VIGENTE",
            CA.COD_TIPO_TABLA_GENESIS "NOMBRETABLA",
            CA.OID_TABLA_GENESIS "OID_GENESIS"
          FROM    GEPR_TCODIGO_AJENO CA
          WHERE
            CA.OID_TABLA_GENESIS IN
            (
              SELECT
                OID_CAMPO1
              FROM
                SAPR_GTT_TAUXILIAR
              WHERE
                COD_CALIFICADOR IN
                  (
                    'OID_CLIENTE',
                    'OID_SUBCLIENTE',
                    'OID_PTO_SERVICIO'
                  )
            );
      END IF;

      OPEN par$rc_validaciones FOR
        SELECT
          COD_CAMPO2 "CODIGO",
          COD_CAMPO3 "DESCRIPCION",
          OID_CAMPO1 "OID_CAMPO1"
        FROM
          SAPR_GTT_TAUXILIAR
        WHERE
          COD_CALIFICADOR = 'VALIDACIONES';

    END IF;

  END srecuperar_datosentidades;

  /* Valida los Clientes */
  PROCEDURE svalidar_codigos
  (
    par$cod_cultura           IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_clientes      IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$des_clientes      IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$cod_subclientes   IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$des_subclientes   IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$cod_ptoservs      IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$des_ptoservs      IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$nivel             IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$rc_validaciones   OUT gepr_pcomon_###VERSION###.tipo$descs_
  ) IS
    var$cantidad_registro  gepr_pcomon_###VERSION###.tipo$nel_;
    var$mensaje           gepr_pcomon_###VERSION###.tipo$desc_;
  BEGIN

    /* Valida los Clientes */
    DBMS_OUTPUT.PUT_LINE('VALIDA 1');
    IF  par$nivel = 'TODOS' OR par$nivel='CLIENTE' OR par$nivel = 'SUBCLIENTE' OR par$nivel = 'PUNTO' THEN
      DBMS_OUTPUT.PUT_LINE('VALIDA 1-1') ;
      IF par$cod_clientes is not null AND par$cod_clientes.COUNT>0 THEN
        FOR idx IN par$cod_clientes.first .. par$cod_clientes.last
        LOOP
            DBMS_OUTPUT.PUT_LINE('VALIDA 1-2') ;
            IF par$cod_clientes(idx) is null AND par$des_clientes(idx) is not null THEN
              IF par$cod_identificador_ajeno IS NOT NULL THEN
                /* Busco si hay registros */
                SELECT count(*)
                INTO  var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                    UPPER (DES_AJENO) like UPPER ( '%' || par$des_clientes(idx) || '%')
                    AND COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                    AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                    AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                    )
                    ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_CLIENTE'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                    UPPER (DES_AJENO) like UPPER ( '%' || par$des_clientes(idx) || '%')
                    AND COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                    AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                    AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                    )
                    ;
              ELSE
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCLIENTE
                WHERE
                    upper(des_cliente) like upper('%' || par$des_clientes(idx) || '%')
                    AND OID_CLIENTE NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                    );
                /* Los inserto en la tabla auxiliar. */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_CLIENTE, 'OID_CLIENTE'
                FROM    GEPR_TCLIENTE
                WHERE
                    upper(des_cliente) like upper('%' || par$des_clientes(idx) || '%')
                    AND OID_CLIENTE NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                    );
              END IF;

             IF var$cantidad_registro = 0 THEN
              var$mensaje := par$des_clientes(idx);
              dbms_output.put_line('No se ha encontrado un cliente para el código - descripcion "{0} - {1}".');
              par$rc_validaciones(par$rc_validaciones.count) := '2040090005'|| var$mensaje;
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
              VALUES (var$mensaje, '2040090005',
                                                  replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090005',
                                                  gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;
            END IF;

            IF par$cod_clientes(idx) is not null AND par$des_clientes(idx) is null THEN
              IF par$cod_identificador_ajeno IS NOT NULL THEN
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND   COD_AJENO = par$cod_clientes(idx)
                  AND OID_TABLA_GENESIS NOT IN
                  (
                    SELECT OID_CAMPO1
                    FROM SAPR_GTT_TAUXILIAR
                    WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                  )
                  ;

                /* Los inserto en la tabla auxiliar. */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_CLIENTE'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND   COD_AJENO = par$cod_clientes(idx)
                  AND OID_TABLA_GENESIS NOT IN
                  (
                    SELECT OID_CAMPO1
                    FROM SAPR_GTT_TAUXILIAR
                    WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                  )
                  ;
              ELSE
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCLIENTE
                WHERE
                    cod_cliente = par$cod_clientes(idx)
                    AND OID_CLIENTE NOT IN
                      (
                         SELECT OID_CAMPO1
                         FROM SAPR_GTT_TAUXILIAR
                         WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                      )
                      ;


                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_CLIENTE, 'OID_CLIENTE'
                FROM    GEPR_TCLIENTE
                WHERE
                    cod_cliente = par$cod_clientes(idx)
                    AND OID_CLIENTE NOT IN
                      (
                         SELECT OID_CAMPO1
                         FROM SAPR_GTT_TAUXILIAR
                         WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                      )
                      ;
              END IF;

              IF var$cantidad_registro = 0 THEN
                var$mensaje := par$cod_clientes(idx);
                dbms_output.put_line('No se ha encontrado un cliente para el código - descripcion "{0} - {1}".');
                par$rc_validaciones(par$rc_validaciones.count) := '2040090005'|| var$mensaje;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (var$mensaje, '2040090005',
                                                    replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090005',
                                                    gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;
            END IF;

            IF par$cod_clientes(idx) is not null AND par$des_clientes(idx) is not null THEN
              IF par$cod_identificador_ajeno IS NOT NULL THEN
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                    COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                    AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                    AND COD_AJENO = par$cod_clientes(idx)
                    AND upper(DES_AJENO) like upper( '%' || par$des_clientes(idx) || '%')
                    AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                    )
                ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_CLIENTE'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                    COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                    AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                    AND COD_AJENO = par$cod_clientes(idx)
                    AND upper(DES_AJENO) like upper( '%' || par$des_clientes(idx) || '%')
                    AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                    )
                ;
              ELSE
                  INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                  SELECT  OID_CLIENTE, 'OID_CLIENTE'
                  FROM    GEPR_TCLIENTE
                  WHERE
                      cod_cliente = par$cod_clientes(idx)
                      AND upper(des_cliente) like upper( '%' || par$des_clientes(idx) || '%')
                      AND OID_CLIENTE NOT IN
                        (
                          SELECT OID_CAMPO1
                          FROM SAPR_GTT_TAUXILIAR
                          WHERE COD_CALIFICADOR = 'OID_CLIENTE'
                        );
              END IF;

              IF var$cantidad_registro = 0 THEN
                var$mensaje := par$cod_clientes(idx) || par$des_clientes(idx);
                dbms_output.put_line('No se ha encontrado un cliente para el código - descripcion "{0} - {1}".');
                par$rc_validaciones(par$rc_validaciones.count) := '2040090005'|| var$mensaje;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (var$mensaje, '2040090005',
                                                    replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090005',
                                                    gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;
            END IF;
        END LOOP;
      END IF;


    /* Valida los SubClientes */
      DBMS_OUTPUT.PUT_LINE('VALIDA 2-1') ;
      IF par$cod_subclientes is not null AND par$cod_subclientes.COUNT > 0 THEN
        DBMS_OUTPUT.PUT_LINE('VALIDA 2-2') ;
        FOR idx IN par$cod_subclientes.first .. par$cod_subclientes.last
        LOOP
            DBMS_OUTPUT.PUT_LINE('VALIDA 2-3 ' ||  par$cod_subclientes(idx) || ' ' || par$des_subclientes(idx) ) ;
            IF par$cod_subclientes(idx) is null AND par$des_subclientes(idx) is not null THEN
              DBMS_OUTPUT.PUT_LINE('VALIDA 2-4') ;
              IF par$cod_identificador_ajeno IS NOT NULL THEN

                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                    upper(DES_AJENO) like upper('%' || par$des_subclientes(idx) || '%')
                    AND COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                    AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                    AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    )
                    ;

                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_SUBCLIENTE'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                    upper(DES_AJENO) like upper('%' || par$des_subclientes(idx) || '%')
                    AND COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                    AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                    AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    )
                    ;
              ELSE
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TSUBCLIENTE
                WHERE
                    upper(DES_SUBCLIENTE) like upper('%' || par$des_subclientes(idx) || '%')
                    AND OID_SUBCLIENTE NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    );
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_SUBCLIENTE, 'OID_SUBCLIENTE'
                FROM    GEPR_TSUBCLIENTE
                WHERE
                    upper(DES_SUBCLIENTE) like upper('%' || par$des_subclientes(idx) || '%')
                    AND OID_SUBCLIENTE NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    );
              END IF;
              IF var$cantidad_registro = 0 THEN
                var$mensaje := par$des_subclientes(idx);
                dbms_output.put_line('No se ha encontrado un subcliente para el código - descripcion "{0} - {1}".');
                par$rc_validaciones(par$rc_validaciones.count) := '2040090006'|| var$mensaje;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (var$mensaje, '2040090006',
                                                    replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090006',
                                                    gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;
            END IF;

            IF par$cod_subclientes(idx) is not null AND par$des_subclientes(idx) is null THEN
              IF par$cod_identificador_ajeno IS NOT NULL THEN
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_AJENO = par$cod_subclientes(idx)
                  AND COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    )
                  ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_SUBCLIENTE'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_AJENO = par$cod_subclientes(idx)
                  AND COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    )
                  ;
              ELSE
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TSUBCLIENTE
                WHERE
                  COD_SUBCLIENTE = par$cod_subclientes(idx)
                  AND OID_SUBCLIENTE NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    )
                  ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_SUBCLIENTE, 'OID_SUBCLIENTE'
                FROM    GEPR_TSUBCLIENTE
                WHERE
                  COD_SUBCLIENTE = par$cod_subclientes(idx)
                  AND OID_SUBCLIENTE NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    )
                  ;
              END IF;

              IF var$cantidad_registro = 0 THEN
                var$mensaje := par$cod_subclientes(idx) ;
                dbms_output.put_line('No se ha encontrado un subcliente para el código - descripcion "{0} - {1}".');
                par$rc_validaciones(par$rc_validaciones.count) := '2040090006'|| var$mensaje;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (var$mensaje, '2040090006',
                                                    replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090006',
                                                    gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;

            END IF;

            IF par$cod_subclientes(idx) is not null AND par$des_subclientes(idx) is not null THEN
              IF par$cod_identificador_ajeno IS NOT NULL THEN
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND COD_AJENO = par$cod_subclientes(idx)
                  AND upper(DES_AJENO) like upper('%' || par$des_subclientes(idx) || '%')
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    )
                  ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_SUBCLIENTE'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND COD_AJENO = par$cod_subclientes(idx)
                  AND upper(DES_AJENO) like upper('%' || par$des_subclientes(idx) || '%')
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    )
                  ;
              ELSE
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TSUBCLIENTE
                WHERE
                    COD_SUBCLIENTE = par$cod_subclientes(idx)
                    AND upper(DES_SUBCLIENTE) like upper('%' || par$des_subclientes(idx) || '%')
                    AND OID_SUBCLIENTE NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    );
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_SUBCLIENTE, 'OID_SUBCLIENTE'
                FROM    GEPR_TSUBCLIENTE
                WHERE
                    COD_SUBCLIENTE = par$cod_subclientes(idx)
                    AND upper(DES_SUBCLIENTE) like upper('%' || par$des_subclientes(idx) || '%')
                    AND OID_SUBCLIENTE NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'
                    );
              END IF;

              IF var$cantidad_registro = 0 THEN
                var$mensaje := par$cod_subclientes(idx) || par$des_subclientes(idx);
                dbms_output.put_line('No se ha encontrado un subcliente para el código - descripcion "{0} - {1}".');
                par$rc_validaciones(par$rc_validaciones.count) := '2040090006'|| var$mensaje;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (var$mensaje, '2040090006',
                                                    replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090006',
                                                    gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;

            END IF;
        END LOOP;
      END IF;

    DBMS_OUTPUT.PUT_LINE('VALIDA 3');
    /* Valida los PuntosDeServicios */
    IF par$cod_ptoservs is not null AND par$cod_ptoservs.COUNT>0 THEN
          FOR idx IN par$cod_ptoservs.first .. par$cod_ptoservs.last
        LOOP
            IF par$cod_ptoservs(idx) is null AND par$des_ptoservs(idx) is not null THEN
              DBMS_OUTPUT.PUT_LINE('VALIDA 3 - 1') ;
              IF par$cod_identificador_ajeno IS NOT NULL THEN
                DBMS_OUTPUT.PUT_LINE('VALIDA 3 - 1 A') ;
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND upper(DES_AJENO) like upper('%' || par$des_ptoservs(idx) || '%')
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    )
                  ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_PTO_SERVICIO'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND upper(DES_AJENO) like upper('%' || par$des_ptoservs(idx) || '%')
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    )
                  ;
              ELSE
                DBMS_OUTPUT.PUT_LINE('VALIDA 3 - 1 B') ;
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TPUNTO_SERVICIO
                WHERE
                      upper(DES_PTO_SERVICIO) like upper('%' || par$des_ptoservs(idx) || '%')
                    AND OID_PTO_SERVICIO NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    );

                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_PTO_SERVICIO, 'OID_PTO_SERVICIO'
                FROM    GEPR_TPUNTO_SERVICIO
                WHERE
                      upper(DES_PTO_SERVICIO) like upper('%' || par$des_ptoservs(idx) || '%')
                    AND OID_PTO_SERVICIO NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    );

              END IF;
              IF var$cantidad_registro = 0 THEN
                var$mensaje := par$des_ptoservs(idx);
                dbms_output.put_line('No se ha encontrado un punto para el código - descripcion "{0} - {1}".');
                par$rc_validaciones(par$rc_validaciones.count) := '2040090007'|| var$mensaje;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (var$mensaje, '2040090007',
                                                    replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090007',
                                                    gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;
            END IF;

            IF par$cod_ptoservs(idx) is not null AND par$des_ptoservs(idx) is null THEN
              IF par$cod_identificador_ajeno IS NOT NULL THEN
                DBMS_OUTPUT.PUT_LINE('VALIDA 3 - 2 A') ;
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND COD_AJENO = par$cod_ptoservs(idx)
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    )
                  ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_PTO_SERVICIO'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND COD_AJENO = par$cod_ptoservs(idx)
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    )
                  ;
              ELSE
                DBMS_OUTPUT.PUT_LINE('VALIDA 3 - 2 B') ;
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TPUNTO_SERVICIO
                WHERE
                    cod_pto_servicio = par$cod_ptoservs(idx)
                    AND OID_PTO_SERVICIO NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    );
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_PTO_SERVICIO, 'OID_PTO_SERVICIO'
                FROM    GEPR_TPUNTO_SERVICIO
                WHERE
                    cod_pto_servicio = par$cod_ptoservs(idx)
                    AND OID_PTO_SERVICIO NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    );
              END IF;
              IF var$cantidad_registro = 0 THEN
                var$mensaje := par$cod_ptoservs(idx) ;
                dbms_output.put_line('No se ha encontrado un punto para el código - descripcion "{0} - {1}".');
                par$rc_validaciones(par$rc_validaciones.count) := '2040090007'|| var$mensaje;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (var$mensaje, '2040090007',
                                                    replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090007',
                                                    gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;
            END IF;

            IF par$cod_ptoservs(idx) is not null AND par$des_ptoservs(idx) is not null THEN
              IF par$cod_identificador_ajeno IS NOT NULL THEN
                DBMS_OUTPUT.PUT_LINE('VALIDA 3 - 3 A') ;
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND COD_AJENO = par$cod_ptoservs(idx)
                  AND upper(DES_AJENO) like upper('%' || par$des_ptoservs(idx) || '%')
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    )
                  ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_TABLA_GENESIS, 'OID_PTO_SERVICIO'
                FROM    GEPR_TCODIGO_AJENO
                WHERE
                  COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                  AND COD_IDENTIFICADOR = par$cod_identificador_ajeno
                  AND COD_AJENO = par$cod_ptoservs(idx)
                  AND upper(DES_AJENO) like upper('%' || par$des_ptoservs(idx) || '%')
                  AND OID_TABLA_GENESIS NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    )
                  ;
              ELSE
                DBMS_OUTPUT.PUT_LINE('VALIDA 3 - 3 B') ;
                /* Busco si hay registros */
                SELECT  count(*)
                INTO    var$cantidad_registro
                FROM    GEPR_TPUNTO_SERVICIO
                WHERE
                    cod_pto_servicio = par$cod_ptoservs(idx)
                    AND upper(par$des_ptoservs(idx)) like upper('%' || par$des_ptoservs(idx) || '%')
                    AND OID_PTO_SERVICIO NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    )
                    ;
                /* Los inserto en la tabla auxiliar */
                INSERT INTO SAPR_GTT_TAUXILIAR(oid_campo1, cod_calificador)
                SELECT  OID_PTO_SERVICIO, 'OID_PTO_SERVICIO'
                FROM    GEPR_TPUNTO_SERVICIO
                WHERE
                    cod_pto_servicio = par$cod_ptoservs(idx)
                    AND upper(par$des_ptoservs(idx)) like upper('%' || par$des_ptoservs(idx) || '%')
                    AND OID_PTO_SERVICIO NOT IN
                    (
                      SELECT OID_CAMPO1
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'
                    )
                    ;
              END IF;
              IF var$cantidad_registro = 0 THEN
                var$mensaje := par$cod_ptoservs(idx) || par$des_ptoservs(idx);
                dbms_output.put_line('No se ha encontrado un punto para el código - descripcion "{0} - {1}".');
                par$rc_validaciones(par$rc_validaciones.count) := '2040090007'|| var$mensaje;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                VALUES (var$mensaje, '2040090007',
                                                    replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090007',
                                                    gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');
              END IF;

            END IF;
        END LOOP;
    END IF;

    ELSE
      var$mensaje := par$nivel;
      dbms_output.put_line('El nivel ingresado no es valido {0}.');
      par$rc_validaciones(par$rc_validaciones.count) := '2040090003'|| var$mensaje;
      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
      VALUES (var$mensaje, '2040090003',
                                          replace( gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040090003',
                                          gepr_pcomon_###VERSION###.const$CodFunDicObtClientes,
                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0), '{0}', var$mensaje), 'VALIDACIONES');

      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,  COD_CALIFICADOR)
      VALUES ('OID_BLOQUEANTE', 'VALIDACIONES');
    END IF;



  END svalidar_codigos;

  PROCEDURE sconfigurar_cliente(
    par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
    /*Cliente*/
    par$acod_accion               IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$anel_index                IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$acod_cliente              IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$ades_descripcion          IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$acod_tipo_cliente         IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$acod_codigo_bancario      IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$abol_banco_capital        IN gepr_pcomon_###VERSION###.tipo$nbols_,
    par$abol_banco_comision       IN gepr_pcomon_###VERSION###.tipo$nbols_,
    par$anel_porc_comision        IN gepr_pcomon_###VERSION###.tipo$nels_,

    /*Subcliente*/
    par$ascli_cod_accion          IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$ascli_nel_index           IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$ascli_nel_index_scli      IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$ascli_cod_subcliente      IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$ascli_des_descripcion     IN gepr_pcomon_###VERSION###.tipo$descs_,

    /*Punto Servicio*/
    par$apser_cod_accion          IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$apser_nel_index           IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$apser_nel_index_scli      IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$apser_nel_index_pto       IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$apser_cod_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$apser_des_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$descs_,

    /*Datos Bancarios*/
    par$aban_cod_accion           IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aban_cod_entidad          IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aban_nel_index_entidad    IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$aban_oid_identificador    IN gepr_pcomon_###VERSION###.tipo$oids_,
    par$aban_cod_codigobanco      IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aban_cod_codigoagencia    IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aban_cod_numerocuenta     IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aban_cod_tipo_cuenta      IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aban_cod_documento        IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aban_des_titularidad      IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_cod_divisa           IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aban_des_observaciones    IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_bol_defecto          IN gepr_pcomon_###VERSION###.tipo$nbols_,
    par$aban_des_campoadicional1  IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_des_campoadicional2  IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_des_campoadicional3  IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_des_campoadicional4  IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_des_campoadicional5  IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_des_campoadicional6  IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_des_campoadicional7  IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aban_des_campoadicional8  IN gepr_pcomon_###VERSION###.tipo$descs_,

    /*Codigo Ajeno*/
    par$aajen_cod_accion           IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aajen_cod_entidad          IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aajen_nel_index_entidad    IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$aajen_cod_identificador    IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aajen_cod_codigo_ajeno     IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$aajen_cod_desc_ajeno       IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$aajen_bol_defecto          IN gepr_pcomon_###VERSION###.tipo$nbols_,

    /*Direcciones*/
    par$adir_cod_entidad           IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$adir_nel_index_entidad     IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$adir_des_pais              IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_prov              IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_ciudad            IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_num_tel           IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_email              IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_cod_fiscal             IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$adir_cod_postal             IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$adir_des_dic_1              IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_dic_2              IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_cam_adicional_1    IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_cam_adicional_2    IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_cam_adicional_3    IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_cat_adicional_1    IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_cat_adicional_2    IN gepr_pcomon_###VERSION###.tipo$descs_,
    par$adir_des_cat_adicional_3    IN gepr_pcomon_###VERSION###.tipo$descs_,

    par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_usuario                IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_pais                   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$info_ejecucion             IN VARCHAR2,
    par$rc_validaciones            OUT sys_refcursor,
    par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := 'sconfigurar_cliente';
    var$cod_accion              gepr_pcomon_###VERSION###.tipo$cod_;

    var$oid_cliente             gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_cliente             gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_descripcion         gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_tipo_cliente        gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_totaliza_saldo      gepr_pcomon_###VERSION###.tipo$nbol_;
    var$bol_abona_por_total     gepr_pcomon_###VERSION###.tipo$nbol_;
    var$bol_banco_capital       gepr_pcomon_###VERSION###.tipo$nbol_;
    var$bol_banco_comision      gepr_pcomon_###VERSION###.tipo$nbol_;
    var$cod_codigo_bancario     gepr_pcomon_###VERSION###.tipo$cod_;
    var$nel_porc_comision       gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_vigente             gepr_pcomon_###VERSION###.tipo$nbol_;

    var$entidad_cliente         gepr_pcomon_###VERSION###.tipo$desc_:= 'CLIENTE';
    var$entidad_subcliente      gepr_pcomon_###VERSION###.tipo$desc_:= 'SUBCLIENTE';
    var$entidad_pto_Servicio    gepr_pcomon_###VERSION###.tipo$desc_:= 'PUNTOSERVICIO';

    var$oid_subcliente          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_pto_servicio        gepr_pcomon_###VERSION###.tipo$oid_;

    var$ascli_cod_accion        gepr_pcomon_###VERSION###.tipo$cods_;
    var$ascli_oid_cliente       gepr_pcomon_###VERSION###.tipo$oids_;
    var$ascli_oid_subcliente    gepr_pcomon_###VERSION###.tipo$oids_;
    var$ascli_nel_index         gepr_pcomon_###VERSION###.tipo$nels_;
    var$ascli_nel_index_scli    gepr_pcomon_###VERSION###.tipo$nels_;
    var$ascli_cod_subcliente    gepr_pcomon_###VERSION###.tipo$cods_;
    var$ascli_des_descripcion   gepr_pcomon_###VERSION###.tipo$descs_;

    /*Punto Servicio*/
    var$apser_cod_accion           gepr_pcomon_###VERSION###.tipo$cods_;
    var$apser_nel_index            gepr_pcomon_###VERSION###.tipo$nels_;
    var$apser_nel_index_scli       gepr_pcomon_###VERSION###.tipo$nels_;
    var$apser_nel_index_pto        gepr_pcomon_###VERSION###.tipo$nels_;
    var$apser_cod_pto_servicio     gepr_pcomon_###VERSION###.tipo$cods_;
    var$apser_des_pto_servicio     gepr_pcomon_###VERSION###.tipo$descs_;
    var$apser_oid_pto_servicio     gepr_pcomon_###VERSION###.tipo$oids_;
    var$apser_oid_subcliente       gepr_pcomon_###VERSION###.tipo$oids_;

    /*Datos Bancarios*/
    var$aban_cod_accion            gepr_pcomon_###VERSION###.tipo$cods_;
    var$aban_cod_entidad           gepr_pcomon_###VERSION###.tipo$cods_;
    var$aban_nel_index_entidad     gepr_pcomon_###VERSION###.tipo$nels_;
    var$aban_oid_identificador     gepr_pcomon_###VERSION###.tipo$oids_;
    var$aban_oid_cliente       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aban_oid_subcliente      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aban_oid_pto_servicio      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aban_cod_codigobanco       gepr_pcomon_###VERSION###.tipo$cods_;
    var$aban_cod_codigoagencia     gepr_pcomon_###VERSION###.tipo$cods_;
    var$aban_cod_numerocuenta      gepr_pcomon_###VERSION###.tipo$cods_;
    var$aban_cod_tipo_cuenta       gepr_pcomon_###VERSION###.tipo$cods_;
    var$aban_cod_documento         gepr_pcomon_###VERSION###.tipo$cods_;
    var$aban_des_titularidad       gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_cod_divisa            gepr_pcomon_###VERSION###.tipo$cods_;
    var$aban_des_observaciones     gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_bol_defecto           gepr_pcomon_###VERSION###.tipo$nbols_;
    var$aban_bol_activo            gepr_pcomon_###VERSION###.tipo$nbols_;
    var$aban_des_campoadicional1   gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_des_campoadicional2   gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_des_campoadicional3   gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_des_campoadicional4   gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_des_campoadicional5   gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_des_campoadicional6   gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_des_campoadicional7   gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_des_campoadicional8   gepr_pcomon_###VERSION###.tipo$descs_;
    var$aban_des_comentario      gepr_pcomon_###VERSION###.tipo$descs_;

    /*Codigo Ajeno*/
    var$aajen_cod_accion           gepr_pcomon_###VERSION###.tipo$cods_;
    var$aajen_oid_entidad          gepr_pcomon_###VERSION###.tipo$oids_;
    var$aajen_cod_entidad          gepr_pcomon_###VERSION###.tipo$cods_;
    var$aajen_nel_index_entidad    gepr_pcomon_###VERSION###.tipo$nels_;
    var$aajen_nel_index_padre      gepr_pcomon_###VERSION###.tipo$nels_;
    var$aajen_cod_identificador    gepr_pcomon_###VERSION###.tipo$cods_;
    var$aajen_cod_codigo_ajeno     gepr_pcomon_###VERSION###.tipo$cods_;
    var$aajen_cod_desc_ajeno       gepr_pcomon_###VERSION###.tipo$descs_;
    var$aajen_bol_defecto          gepr_pcomon_###VERSION###.tipo$nbols_;

    /*Direcciones*/
    var$adir_cod_entidad            gepr_pcomon_###VERSION###.tipo$cods_;
    var$adir_nel_index_entidad      gepr_pcomon_###VERSION###.tipo$nels_;
    var$adir_des_pais               gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_prov               gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_ciudad             gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_num_tel            gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_email              gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_cod_fiscal             gepr_pcomon_###VERSION###.tipo$cods_;
    var$adir_cod_postal             gepr_pcomon_###VERSION###.tipo$cods_;
    var$adir_des_dic_1              gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_dic_2              gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_cam_adicional_1    gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_cam_adicional_2    gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_cam_adicional_3    gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_cat_adicional_1    gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_cat_adicional_2    gepr_pcomon_###VERSION###.tipo$descs_;
    var$adir_des_cat_adicional_3    gepr_pcomon_###VERSION###.tipo$descs_;

    var$oid_cod_ajeno              gepr_pcomon_###VERSION###.tipo$oid_;
    var$inserts                    gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates                    gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_pais                   gepr_pcomon_###VERSION###.tipo$cod_;
    var$existe                     gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$hayError                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$mensaje                    gepr_pcomon_###VERSION###.tipo$desc_;
    var$comentario                 gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_direccion_cli          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_usuario                gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_pais                   gepr_pcomon_###VERSION###.tipo$oid_;

BEGIN
  DELETE SAPR_GTT_TAUXILIAR;
  COMMIT;

  /* #### Grabar llamadas #### */
  IF par$oid_llamada IS NOT NULL THEN
    /* OID_LLAMADA */
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'par$oid_llamada: ' || par$oid_llamada,
      par$cod_identificador   => '');
    /*cod_identificador_ajeno*/
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'par$cod_identificador_ajeno   : ' || par$cod_identificador_ajeno,
      par$cod_identificador   => '');

    /*par$acod_accion*/
    IF par$acod_accion IS NOT NULL AND par$acod_accion.count > 0 THEN
      FOR idx in par$acod_accion.first .. par$acod_accion.last LOOP
        IF par$acod_accion(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$acod_accion (' || idx || '): ' || par$acod_accion(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$acod_accion: NULL',
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

    /*par$acod_cliente*/
    IF par$acod_cliente IS NOT NULL AND par$acod_cliente.count > 0 THEN
      FOR idx in par$acod_cliente.first .. par$acod_cliente.last LOOP
        IF par$acod_cliente(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$acod_cliente (' || idx || '): ' || par$acod_cliente(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$acod_cliente: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$ades_descripcion*/
    IF par$ades_descripcion IS NOT NULL AND par$ades_descripcion.count > 0 THEN
      FOR idx in par$ades_descripcion.first .. par$ades_descripcion.last LOOP
        IF par$ades_descripcion(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$ades_descripcion (' || idx || '): ' || par$ades_descripcion(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$ades_descripcion: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$acod_tipo_cliente*/
    IF par$acod_tipo_cliente IS NOT NULL AND par$acod_tipo_cliente.count > 0 THEN
      FOR idx in par$acod_tipo_cliente.first .. par$acod_tipo_cliente.last LOOP
        IF par$acod_tipo_cliente(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$acod_tipo_cliente (' || idx || '): ' || par$acod_tipo_cliente(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$acod_tipo_cliente: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$acod_codigo_bancario*/
    IF par$acod_codigo_bancario IS NOT NULL AND par$acod_codigo_bancario.count > 0 THEN
      FOR idx in par$acod_codigo_bancario.first .. par$acod_codigo_bancario.last LOOP
        IF par$acod_codigo_bancario(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$acod_codigo_bancario (' || idx || '): ' || par$acod_codigo_bancario(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$acod_codigo_bancario: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$abol_banco_capital*/
    IF par$abol_banco_capital IS NOT NULL AND par$abol_banco_capital.count > 0 THEN
      FOR idx in par$abol_banco_capital.first .. par$abol_banco_capital.last LOOP
        IF par$abol_banco_capital(idx) IS NOT NULL THEN
          IF par$abol_banco_capital(idx) = 1 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$abol_banco_capital (' || idx || '): 1',
              par$cod_identificador   => '');
          ELSIF par$abol_banco_capital(idx) = 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$abol_banco_capital (' || idx || '): 0',
              par$cod_identificador   => '');
          END IF;
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$abol_banco_capital: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$abol_banco_comision*/
    IF par$abol_banco_comision IS NOT NULL AND par$abol_banco_comision.count > 0 THEN
      FOR idx in par$abol_banco_comision.first .. par$abol_banco_comision.last LOOP
        IF par$abol_banco_comision(idx) IS NOT NULL THEN
          IF par$abol_banco_comision(idx) = 1 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$abol_banco_comision (' || idx || '): 1',
              par$cod_identificador   => '');
          ELSIF par$abol_banco_comision(idx) = 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$abol_banco_comision (' || idx || '): 0',
              par$cod_identificador   => '');
          END IF;
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$abol_banco_comision: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$anel_porc_comision*/
    IF par$anel_porc_comision IS NOT NULL AND par$anel_porc_comision.count > 0 THEN
      FOR idx in par$anel_porc_comision.first .. par$anel_porc_comision.last LOOP
        IF par$anel_porc_comision(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$anel_porc_comision (' || idx || '): ' || par$anel_porc_comision(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$anel_porc_comision: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$ascli_cod_accion*/
    IF par$ascli_cod_accion IS NOT NULL AND par$ascli_cod_accion.count > 0 THEN
      FOR idx in par$ascli_cod_accion.first .. par$ascli_cod_accion.last LOOP
        IF par$ascli_cod_accion(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$ascli_cod_accion (' || idx || '): ' || par$ascli_cod_accion(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$ascli_cod_accion: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$ascli_nel_index*/
    IF par$ascli_nel_index IS NOT NULL AND par$ascli_nel_index.count > 0 THEN
      FOR idx in par$ascli_nel_index.first .. par$ascli_nel_index.last LOOP
        IF par$ascli_nel_index(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$ascli_nel_index (' || idx || '): ' || par$ascli_nel_index(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$ascli_nel_index: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$ascli_nel_index_scli*/
    IF par$ascli_nel_index_scli IS NOT NULL AND par$ascli_nel_index_scli.count > 0 THEN
      FOR idx in par$ascli_nel_index_scli.first .. par$ascli_nel_index_scli.last LOOP
        IF par$ascli_nel_index_scli(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$ascli_nel_index_scli (' || idx || '): ' || par$ascli_nel_index_scli(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$ascli_nel_index_scli: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$ascli_cod_subcliente*/
    IF par$ascli_cod_subcliente IS NOT NULL AND par$ascli_cod_subcliente.count > 0 THEN
      FOR idx in par$ascli_cod_subcliente.first .. par$ascli_cod_subcliente.last LOOP
        IF par$ascli_cod_subcliente(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$ascli_cod_subcliente (' || idx || '): ' || par$ascli_cod_subcliente(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$ascli_cod_subcliente: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$ascli_des_descripcion*/
    IF par$ascli_des_descripcion IS NOT NULL AND par$ascli_des_descripcion.count > 0 THEN
      FOR idx in par$ascli_des_descripcion.first .. par$ascli_des_descripcion.last LOOP
        IF par$ascli_des_descripcion(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$ascli_des_descripcion (' || idx || '): ' || par$ascli_des_descripcion(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$ascli_des_descripcion: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$apser_cod_accion*/
    IF par$apser_cod_accion IS NOT NULL AND par$apser_cod_accion.count > 0 THEN
      FOR idx in par$apser_cod_accion.first .. par$apser_cod_accion.last LOOP
        IF par$apser_cod_accion(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$apser_cod_accion (' || idx || '): ' || par$apser_cod_accion(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$apser_cod_accion: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$apser_nel_index*/
    IF par$apser_nel_index IS NOT NULL AND par$apser_nel_index.count > 0 THEN
      FOR idx in par$apser_nel_index.first .. par$apser_nel_index.last LOOP
        IF par$apser_nel_index(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$apser_nel_index (' || idx || '): ' || par$apser_nel_index(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$apser_nel_index: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$apser_nel_index_scli*/
    IF par$apser_nel_index_scli IS NOT NULL AND par$apser_nel_index_scli.count > 0 THEN
      FOR idx in par$apser_nel_index_scli.first .. par$apser_nel_index_scli.last LOOP
        IF par$apser_nel_index_scli(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$apser_nel_index_scli (' || idx || '): ' || par$apser_nel_index_scli(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$apser_nel_index_scli: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$apser_nel_index_pto*/
    IF par$apser_nel_index_pto IS NOT NULL AND par$apser_nel_index_pto.count > 0 THEN
      FOR idx in par$apser_nel_index_pto.first .. par$apser_nel_index_pto.last LOOP
        IF par$apser_nel_index_pto(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$apser_nel_index_pto (' || idx || '): ' || par$apser_nel_index_pto(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$apser_nel_index_pto: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$apser_cod_pto_servicio*/
    IF par$apser_cod_pto_servicio IS NOT NULL AND par$apser_cod_pto_servicio.count > 0 THEN
      FOR idx in par$apser_cod_pto_servicio.first .. par$apser_cod_pto_servicio.last LOOP
        IF par$apser_cod_pto_servicio(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$apser_cod_pto_servicio (' || idx || '): ' || par$apser_cod_pto_servicio(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$apser_cod_pto_servicio: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$apser_des_pto_servicio*/
    IF par$apser_des_pto_servicio IS NOT NULL AND par$apser_des_pto_servicio.count > 0 THEN
      FOR idx in par$apser_des_pto_servicio.first .. par$apser_des_pto_servicio.last LOOP
        IF par$apser_des_pto_servicio(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$apser_des_pto_servicio (' || idx || '): ' || par$apser_des_pto_servicio(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$apser_des_pto_servicio: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_cod_accion*/
    IF par$aban_cod_accion IS NOT NULL AND par$aban_cod_accion.count > 0 THEN
      FOR idx in par$aban_cod_accion.first .. par$aban_cod_accion.last LOOP
        IF par$aban_cod_accion(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_cod_accion (' || idx || '): ' || par$aban_cod_accion(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_cod_accion: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_cod_entidad*/
    IF par$aban_cod_entidad IS NOT NULL AND par$aban_cod_entidad.count > 0 THEN
      FOR idx in par$aban_cod_entidad.first .. par$aban_cod_entidad.last LOOP
        IF par$aban_cod_entidad(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_cod_entidad (' || idx || '): ' || par$aban_cod_entidad(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_cod_entidad: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_nel_index_entidad*/
    IF par$aban_nel_index_entidad IS NOT NULL AND par$aban_nel_index_entidad.count > 0 THEN
      FOR idx in par$aban_nel_index_entidad.first .. par$aban_nel_index_entidad.last LOOP
        IF par$aban_nel_index_entidad(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_nel_index_entidad (' || idx || '): ' || par$aban_nel_index_entidad(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_nel_index_entidad: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_oid_identificador*/
    IF par$aban_oid_identificador IS NOT NULL AND par$aban_oid_identificador.count > 0 THEN
      FOR idx in par$aban_oid_identificador.first .. par$aban_oid_identificador.last LOOP
        IF par$aban_oid_identificador(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_oid_identificador (' || idx || '): ' || par$aban_oid_identificador(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_oid_identificador: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_cod_codigobanco*/
    IF par$aban_cod_codigobanco IS NOT NULL AND par$aban_cod_codigobanco.count > 0 THEN
      FOR idx in par$aban_cod_codigobanco.first .. par$aban_cod_codigobanco.last LOOP
        IF par$aban_cod_codigobanco(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_cod_codigobanco (' || idx || '): ' || par$aban_cod_codigobanco(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_cod_codigobanco: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_cod_codigoagencia*/
    IF par$aban_cod_codigoagencia IS NOT NULL AND par$aban_cod_codigoagencia.count > 0 THEN
      FOR idx in par$aban_cod_codigoagencia.first .. par$aban_cod_codigoagencia.last LOOP
        IF par$aban_cod_codigoagencia(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_cod_codigoagencia (' || idx || '): ' || par$aban_cod_codigoagencia(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_cod_codigoagencia: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_cod_numerocuenta*/
    IF par$aban_cod_numerocuenta IS NOT NULL AND par$aban_cod_numerocuenta.count > 0 THEN
      FOR idx in par$aban_cod_numerocuenta.first .. par$aban_cod_numerocuenta.last LOOP
        IF par$aban_cod_numerocuenta(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_cod_numerocuenta (' || idx || '): ' || par$aban_cod_numerocuenta(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_cod_numerocuenta: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_cod_tipo_cuenta*/
    IF par$aban_cod_tipo_cuenta IS NOT NULL AND par$aban_cod_tipo_cuenta.count > 0 THEN
      FOR idx in par$aban_cod_tipo_cuenta.first .. par$aban_cod_tipo_cuenta.last LOOP
        IF par$aban_cod_tipo_cuenta(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_cod_tipo_cuenta (' || idx || '): ' || par$aban_cod_tipo_cuenta(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_cod_tipo_cuenta: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_cod_documento*/
    IF par$aban_cod_documento IS NOT NULL AND par$aban_cod_documento.count > 0 THEN
      FOR idx in par$aban_cod_documento.first .. par$aban_cod_documento.last LOOP
        IF par$aban_cod_documento(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_cod_documento (' || idx || '): ' || par$aban_cod_documento(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_cod_documento: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_titularidad*/
    IF par$aban_des_titularidad IS NOT NULL AND par$aban_des_titularidad.count > 0 THEN
      FOR idx in par$aban_des_titularidad.first .. par$aban_des_titularidad.last LOOP
        IF par$aban_des_titularidad(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_titularidad (' || idx || '): ' || par$aban_des_titularidad(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_titularidad: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_cod_divisa*/
    IF par$aban_cod_divisa IS NOT NULL AND par$aban_cod_divisa.count > 0 THEN
      FOR idx in par$aban_cod_divisa.first .. par$aban_cod_divisa.last LOOP
        IF par$aban_cod_divisa(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_cod_divisa (' || idx || '): ' || par$aban_cod_divisa(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_cod_divisa: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_observaciones*/
    IF par$aban_des_observaciones IS NOT NULL AND par$aban_des_observaciones.count > 0 THEN
      FOR idx in par$aban_des_observaciones.first .. par$aban_des_observaciones.last LOOP
        IF par$aban_des_observaciones(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_observaciones (' || idx || '): ' || par$aban_des_observaciones(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_observaciones: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_bol_defecto*/
    IF par$aban_bol_defecto IS NOT NULL AND par$aban_bol_defecto.count > 0 THEN
      FOR idx in par$aban_bol_defecto.first .. par$aban_bol_defecto.last LOOP
        IF par$aban_bol_defecto(idx) IS NOT NULL THEN
          IF par$aban_bol_defecto(idx) = 1 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$aban_bol_defecto (' || idx || '): 1',
              par$cod_identificador   => '');
          ELSIF par$aban_bol_defecto(idx) = 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$aban_bol_defecto (' || idx || '): 0',
              par$cod_identificador   => '');
          END IF;

        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_bol_defecto: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_campoadicional1*/
    IF par$aban_des_campoadicional1 IS NOT NULL AND par$aban_des_campoadicional1.count > 0 THEN
      FOR idx in par$aban_des_campoadicional1.first .. par$aban_des_campoadicional1.last LOOP
        IF par$aban_des_campoadicional1(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_campoadicional1 (' || idx || '): ' || par$aban_des_campoadicional1(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_campoadicional1: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_campoadicional2*/
    IF par$aban_des_campoadicional2 IS NOT NULL AND par$aban_des_campoadicional2.count > 0 THEN
      FOR idx in par$aban_des_campoadicional2.first .. par$aban_des_campoadicional2.last LOOP
        IF par$aban_des_campoadicional2(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_campoadicional2 (' || idx || '): ' || par$aban_des_campoadicional2(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_campoadicional2: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_campoadicional3*/
    IF par$aban_des_campoadicional3 IS NOT NULL AND par$aban_des_campoadicional3.count > 0 THEN
      FOR idx in par$aban_des_campoadicional3.first .. par$aban_des_campoadicional3.last LOOP
        IF par$aban_des_campoadicional3(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_campoadicional3 (' || idx || '): ' || par$aban_des_campoadicional3(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_campoadicional3: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_campoadicional4*/
    IF par$aban_des_campoadicional4 IS NOT NULL AND par$aban_des_campoadicional4.count > 0 THEN
      FOR idx in par$aban_des_campoadicional4.first .. par$aban_des_campoadicional4.last LOOP
        IF par$aban_des_campoadicional4(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_campoadicional4 (' || idx || '): ' || par$aban_des_campoadicional4(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_campoadicional4: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_campoadicional5*/
    IF par$aban_des_campoadicional5 IS NOT NULL AND par$aban_des_campoadicional5.count > 0 THEN
      FOR idx in par$aban_des_campoadicional5.first .. par$aban_des_campoadicional5.last LOOP
        IF par$aban_des_campoadicional5(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_campoadicional5 (' || idx || '): ' || par$aban_des_campoadicional5(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_campoadicional5: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_campoadicional6*/
    IF par$aban_des_campoadicional6 IS NOT NULL AND par$aban_des_campoadicional6.count > 0 THEN
      FOR idx in par$aban_des_campoadicional6.first .. par$aban_des_campoadicional6.last LOOP
        IF par$aban_des_campoadicional6(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_campoadicional6 (' || idx || '): ' || par$aban_des_campoadicional6(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_campoadicional6: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_campoadicional7*/
    IF par$aban_des_campoadicional7 IS NOT NULL AND par$aban_des_campoadicional7.count > 0 THEN
      FOR idx in par$aban_des_campoadicional7.first .. par$aban_des_campoadicional7.last LOOP
        IF par$aban_des_campoadicional7(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_campoadicional7 (' || idx || '): ' || par$aban_des_campoadicional7(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_campoadicional7: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aban_des_campoadicional8*/
    IF par$aban_des_campoadicional8 IS NOT NULL AND par$aban_des_campoadicional8.count > 0 THEN
      FOR idx in par$aban_des_campoadicional8.first .. par$aban_des_campoadicional8.last LOOP
        IF par$aban_des_campoadicional8(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aban_des_campoadicional8 (' || idx || '): ' || par$aban_des_campoadicional8(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aban_des_campoadicional8: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aajen_cod_accion*/
    IF par$aajen_cod_accion IS NOT NULL AND par$aajen_cod_accion.count > 0 THEN
      FOR idx in par$aajen_cod_accion.first .. par$aajen_cod_accion.last LOOP
        IF par$aajen_cod_accion(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aajen_cod_accion (' || idx || '): ' || par$aajen_cod_accion(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aajen_cod_accion: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aajen_cod_entidad*/
    IF par$aajen_cod_entidad IS NOT NULL AND par$aajen_cod_entidad.count > 0 THEN
      FOR idx in par$aajen_cod_entidad.first .. par$aajen_cod_entidad.last LOOP
        IF par$aajen_cod_entidad(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aajen_cod_entidad (' || idx || '): ' || par$aajen_cod_entidad(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aajen_cod_entidad: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aajen_nel_index_entidad*/
    IF par$aajen_nel_index_entidad IS NOT NULL AND par$aajen_nel_index_entidad.count > 0 THEN
      FOR idx in par$aajen_nel_index_entidad.first .. par$aajen_nel_index_entidad.last LOOP
        IF par$aajen_nel_index_entidad(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aajen_nel_index_entidad (' || idx || '): ' || par$aajen_nel_index_entidad(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aajen_nel_index_entidad: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aajen_cod_identificador*/
    IF par$aajen_cod_identificador IS NOT NULL AND par$aajen_cod_identificador.count > 0 THEN
      FOR idx in par$aajen_cod_identificador.first .. par$aajen_cod_identificador.last LOOP
        IF par$aajen_cod_identificador(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aajen_cod_identificador (' || idx || '): ' || par$aajen_cod_identificador(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aajen_cod_identificador: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aajen_cod_codigo_ajeno*/
    IF par$aajen_cod_codigo_ajeno IS NOT NULL AND par$aajen_cod_codigo_ajeno.count > 0 THEN
      FOR idx in par$aajen_cod_codigo_ajeno.first .. par$aajen_cod_codigo_ajeno.last LOOP
        IF par$aajen_cod_codigo_ajeno(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aajen_cod_codigo_ajeno (' || idx || '): ' || par$aajen_cod_codigo_ajeno(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aajen_cod_codigo_ajeno: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aajen_cod_desc_ajeno*/
    IF par$aajen_cod_desc_ajeno IS NOT NULL AND par$aajen_cod_desc_ajeno.count > 0 THEN
      FOR idx in par$aajen_cod_desc_ajeno.first .. par$aajen_cod_desc_ajeno.last LOOP
        IF par$aajen_cod_desc_ajeno(idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$aajen_cod_desc_ajeno (' || idx || '): ' || par$aajen_cod_desc_ajeno(idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aajen_cod_desc_ajeno: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$aajen_bol_defecto*/
    IF par$aajen_bol_defecto IS NOT NULL AND par$aajen_bol_defecto.count > 0 THEN
      FOR idx in par$aajen_bol_defecto.first .. par$aajen_bol_defecto.last LOOP
        IF par$aajen_bol_defecto(idx) IS NOT NULL THEN
          IF par$aajen_bol_defecto(idx) = 1 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$aajen_bol_defecto (' || idx || '): 1',
              par$cod_identificador   => '');
          ELSIF par$aajen_bol_defecto(idx) = 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'par$aajen_bol_defecto (' || idx || '): 0',
              par$cod_identificador   => '');
          END IF;
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$aajen_bol_defecto: NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_cod_entidad */
    IF par$adir_cod_entidad  IS NOT NULL AND par$adir_cod_entidad .count > 0 THEN
      FOR idx in par$adir_cod_entidad .first .. par$adir_cod_entidad .last LOOP
        IF par$adir_cod_entidad (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_cod_entidad  (' || idx || '): ' || par$adir_cod_entidad (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_cod_entidad : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_nel_index_entidad */
    IF par$adir_nel_index_entidad  IS NOT NULL AND par$adir_nel_index_entidad .count > 0 THEN
      FOR idx in par$adir_nel_index_entidad .first .. par$adir_nel_index_entidad .last LOOP
        IF par$adir_nel_index_entidad (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_nel_index_entidad  (' || idx || '): ' || par$adir_nel_index_entidad (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_nel_index_entidad : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_pais */
    IF par$adir_des_pais  IS NOT NULL AND par$adir_des_pais .count > 0 THEN
      FOR idx in par$adir_des_pais .first .. par$adir_des_pais .last LOOP
        IF par$adir_des_pais (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_pais  (' || idx || '): ' || par$adir_des_pais (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_pais : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_prov */
    IF par$adir_des_prov  IS NOT NULL AND par$adir_des_prov .count > 0 THEN
      FOR idx in par$adir_des_prov .first .. par$adir_des_prov .last LOOP
        IF par$adir_des_prov (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_prov  (' || idx || '): ' || par$adir_des_prov (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_prov : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_ciudad */
    IF par$adir_des_ciudad  IS NOT NULL AND par$adir_des_ciudad .count > 0 THEN
      FOR idx in par$adir_des_ciudad .first .. par$adir_des_ciudad .last LOOP
        IF par$adir_des_ciudad (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_ciudad  (' || idx || '): ' || par$adir_des_ciudad (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_ciudad : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_num_tel */
    IF par$adir_des_num_tel  IS NOT NULL AND par$adir_des_num_tel .count > 0 THEN
      FOR idx in par$adir_des_num_tel .first .. par$adir_des_num_tel .last LOOP
        IF par$adir_des_num_tel (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_num_tel  (' || idx || '): ' || par$adir_des_num_tel (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_num_tel : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_email */
    IF par$adir_des_email  IS NOT NULL AND par$adir_des_email .count > 0 THEN
      FOR idx in par$adir_des_email.first .. par$adir_des_email.last LOOP
        IF par$adir_des_email (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_email  (' || idx || '): ' || par$adir_des_email (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_email : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_cod_fiscal */
    IF par$adir_cod_fiscal  IS NOT NULL AND par$adir_cod_fiscal .count > 0 THEN
      FOR idx in par$adir_cod_fiscal.first .. par$adir_cod_fiscal.last LOOP
        IF par$adir_cod_fiscal (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_cod_fiscal  (' || idx || '): ' || par$adir_cod_fiscal (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_cod_fiscal : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_cod_postal */
    IF par$adir_cod_postal  IS NOT NULL AND par$adir_cod_postal .count > 0 THEN
      FOR idx in par$adir_cod_postal.first .. par$adir_cod_postal.last LOOP
        IF par$adir_cod_postal (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_cod_postal  (' || idx || '): ' || par$adir_cod_postal (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_cod_postal : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_dic_1 */
    IF par$adir_des_dic_1  IS NOT NULL AND par$adir_des_dic_1 .count > 0 THEN
      FOR idx in par$adir_des_dic_1.first .. par$adir_des_dic_1.last LOOP
        IF par$adir_des_dic_1 (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_dic_1  (' || idx || '): ' || par$adir_des_dic_1 (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_dic_1 : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_dic_2 */
    IF par$adir_des_dic_2  IS NOT NULL AND par$adir_des_dic_2 .count > 0 THEN
      FOR idx in par$adir_des_dic_2.first .. par$adir_des_dic_2.last LOOP
        IF par$adir_des_dic_2 (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_dic_2  (' || idx || '): ' || par$adir_des_dic_2 (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_dic_2 : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_cam_adicional_1 */
    IF par$adir_des_cam_adicional_1  IS NOT NULL AND par$adir_des_cam_adicional_1 .count > 0 THEN
      FOR idx in par$adir_des_cam_adicional_1.first .. par$adir_des_cam_adicional_1.last LOOP
        IF par$adir_des_cam_adicional_1 (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_cam_adicional_1  (' || idx || '): ' || par$adir_des_cam_adicional_1 (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_cam_adicional_1 : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_cam_adicional_2 */
    IF par$adir_des_cam_adicional_2  IS NOT NULL AND par$adir_des_cam_adicional_2 .count > 0 THEN
      FOR idx in par$adir_des_cam_adicional_2.first .. par$adir_des_cam_adicional_2.last LOOP
        IF par$adir_des_cam_adicional_2 (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_cam_adicional_2  (' || idx || '): ' || par$adir_des_cam_adicional_2 (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_cam_adicional_2 : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_cam_adicional_3 */
    IF par$adir_des_cam_adicional_3  IS NOT NULL AND par$adir_des_cam_adicional_3 .count > 0 THEN
      FOR idx in par$adir_des_cam_adicional_3.first .. par$adir_des_cam_adicional_3.last LOOP
        IF par$adir_des_cam_adicional_3 (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_cam_adicional_3  (' || idx || '): ' || par$adir_des_cam_adicional_3 (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_cam_adicional_3 : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_cat_adicional_1 */
    IF par$adir_des_cat_adicional_1  IS NOT NULL AND par$adir_des_cat_adicional_1.count > 0 THEN
      FOR idx in par$adir_des_cat_adicional_1.first .. par$adir_des_cat_adicional_1.last LOOP
        IF par$adir_des_cat_adicional_1 (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_cat_adicional_1  (' || idx || '): ' || par$adir_des_cat_adicional_1 (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_cat_adicional_1 : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_cat_adicional_2 */
    IF par$adir_des_cat_adicional_2  IS NOT NULL AND par$adir_des_cat_adicional_2.count > 0 THEN
      FOR idx in par$adir_des_cat_adicional_2.first .. par$adir_des_cat_adicional_2.last LOOP
        IF par$adir_des_cat_adicional_2 (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_cat_adicional_2  (' || idx || '): ' || par$adir_des_cat_adicional_2 (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_cat_adicional_2 : NULL',
        par$cod_identificador   => '');
    END IF;

    /*par$adir_des_cat_adicional_3 */
    IF par$adir_des_cat_adicional_3  IS NOT NULL AND par$adir_des_cat_adicional_3.count > 0 THEN
      FOR idx in par$adir_des_cat_adicional_3.first .. par$adir_des_cat_adicional_3.last LOOP
        IF par$adir_des_cat_adicional_3 (idx) IS NOT NULL THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$adir_des_cat_adicional_3  (' || idx || '): ' || par$adir_des_cat_adicional_3 (idx),
            par$cod_identificador   => '');
        END IF;
      END LOOP;
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(
        par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
        par$des_version     => const$version,
        par$des_detalle     => 'par$adir_des_cat_adicional_3 : NULL',
        par$cod_identificador   => '');
    END IF;

    /*COD_CULTURA*/
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
      par$cod_identificador   => '');

    /*COD_USUARIO*/
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
      par$cod_identificador   => '');

    /*COD_PAIS*/
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'par$cod_pais: ' || par$cod_pais,
      par$cod_identificador   => '');

    /*INFO_EJECUCION*/
    SAPR_PLOG_API.SAGREGA_DETALLE(
      par$oid_llamada  => par$oid_llamada,
      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
      par$des_version     => const$version,
      par$des_detalle     => 'par$info_ejecucion: ' || par$info_ejecucion,
      par$cod_identificador   => '');
  END IF;

  SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Validamos código de país',
              par$cod_identificador   => '');

  /* Validar código pais*/
  /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
  var$cod_pais := par$cod_pais;
  GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                          par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                          par$cod_pais => var$cod_pais,
                          par$oid_pais => var$oid_pais,
                          par$cod_cultura => par$cod_cultura);
                          
  /* Buscamos si hay error*/
  BEGIN
    SELECT COUNT(1)
      INTO var$hayError
      FROM SAPR_GTT_TAUXILIAR A
      WHERE A.COD_CALIFICADOR = 'VALIDACIONES';
  EXCEPTION WHEN no_data_found THEN
    var$hayError := 0;
  END;

  SAPR_PLOG_API.SAGREGA_DETALLE(
    par$oid_llamada  => par$oid_llamada,
    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
    par$des_version     => const$version,
    par$des_detalle     => 'var$hayError: ' || var$hayError,
    par$cod_identificador   => '');

  /*Si no hay error continuamos la ejecución*/
  IF var$hayError = 0 THEN
    /* grabar cliente*/
    FOR idx IN par$anel_index.first .. par$anel_index.last LOOP
      IF idx > 0 and par$anel_index(idx) is not null THEN

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'INDICE: ' || idx,
          par$cod_identificador   => '');
        DBMS_OUTPUT.PUT_LINE('INDICE: ' || idx);
        BEGIN
          --INICIO VALIDACIONES
          var$cod_accion      := par$acod_accion(idx);
          var$cod_cliente     := par$acod_cliente(idx);
          var$des_descripcion := par$ades_descripcion(idx);
          var$bol_vigente     := null; /*Siempre le pasamos null. Luego, validara por el código de acción.*/

          DBMS_OUTPUT.PUT_LINE('Previo a validar cliente');

          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Previo a validar cliente',
            par$cod_identificador   => '');

          svalidar_cliente(par$oid_llamada                   => par$oid_llamada,
                          par$cod_accion                     => var$cod_accion,
                          par$nel_index                      => par$anel_index(idx),
                          par$cod_cliente                    => var$cod_cliente,
                          par$des_descripcion                => var$des_descripcion,
                          par$cod_tipo_cliente               => par$acod_tipo_cliente(idx),
                          par$cod_cultura                     => par$cod_cultura);
          DBMS_OUTPUT.PUT_LINE('Posterior a validar cliente');

          DBMS_OUTPUT.PUT_LINE('Codigo de accion para el cliente: ' || var$cod_accion);
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Posterior a validar cliente',
            par$cod_identificador   => '');

          IF NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_cliente,
                                              par$nel_index  => par$anel_index(idx)) THEN

            DBMS_OUTPUT.PUT_LINE('No hay error cliente de indice: ' || par$anel_index(idx));

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'No hay error cliente de indice: ' || par$anel_index(idx),
              par$cod_identificador   => '');

            var$oid_cliente := NULL;
            --ALTA
            IF var$cod_accion = 'ALTA' THEN
                var$bol_totaliza_saldo := 0;
                var$bol_abona_por_total := 0;

                IF par$abol_banco_capital(idx) IS NOT NULL THEN
                    var$bol_banco_capital  := par$abol_banco_capital(idx);
                ELSE
                    var$bol_banco_capital  := 0;
                END IF;

                IF par$abol_banco_comision(idx) IS NOT NULL THEN
                    var$bol_banco_comision := par$abol_banco_comision(idx);
                ELSE
                    var$bol_banco_comision  := 0;
                END IF;

            ELSE
              var$bol_totaliza_saldo := NULL;
              var$bol_abona_por_total := NULL;

              var$bol_banco_capital  := par$abol_banco_capital(idx);
              var$bol_banco_comision := par$abol_banco_comision(idx);

              BEGIN
                  SELECT OID_CAMPO1
                      INTO var$oid_cliente
                      FROM SAPR_GTT_TAUXILIAR
                      WHERE COD_CALIFICADOR = 'OID_CLIENTE'  AND
                          COD_CAMPO2 =  par$anel_index(idx) AND ROWNUM = 1;
              EXCEPTION WHEN no_data_found THEN
                  var$oid_cliente := NULL;
              END;

            END IF;

            IF  par$acod_tipo_cliente(idx) = '0' then
                var$cod_codigo_bancario := '';
                var$bol_banco_capital  := 0;
                var$bol_banco_comision := 0;
            ELSE
                var$nel_porc_comision := null;
                var$cod_codigo_bancario := par$acod_codigo_bancario(idx);
            END IF;

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Por obtener el tipo de cliente',
              par$cod_identificador   => '');

            /*Obtiene el tipo de cliente*/
            BEGIN
                SELECT OID_CAMPO1
                    INTO var$oid_tipo_cliente
                    FROM SAPR_GTT_TAUXILIAR
                    WHERE COD_CALIFICADOR = 'OID_TIPO_CLIENTE'  AND
                        COD_CAMPO2 =  par$anel_index(idx) AND ROWNUM = 1;
            EXCEPTION WHEN no_data_found THEN
                var$oid_tipo_cliente := NULL;
            END;

            dbms_output.put_line('var$cod_accion: ' || var$cod_accion);
            
            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'var$cod_accion: ' || var$cod_accion,
              par$cod_identificador   => '');

            IF var$cod_accion = 'ALTA' OR var$cod_accion = 'MODIFICAR' THEN
              dbms_output.put_line('var$bol_vigente = 1');
              SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'var$bol_vigente: 1',
                par$cod_identificador   => '');
              var$bol_vigente := 1;
              IF var$oid_cliente IS NULL AND var$cod_accion = 'ALTA' THEN
                var$oid_cliente := SYS_GUID();
              END IF;
            ELSIF  (var$cod_accion = 'BAJA') THEN
                var$bol_vigente := 0;
                dbms_output.put_line('var$bol_vigente = 0');
                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'var$bol_vigente: 1',
                  par$cod_identificador   => '');
            END IF;

            -- Validar codigos ajenos    
            var$aajen_cod_accion.delete;
            var$aajen_oid_entidad.delete;
            var$aajen_cod_entidad.delete;
            var$aajen_nel_index_entidad.delete;
            var$aajen_nel_index_padre.delete;
            var$aajen_cod_identificador.delete;
            var$aajen_cod_codigo_ajeno.delete;
            var$aajen_cod_desc_ajeno.delete;
            var$aajen_bol_defecto.delete;

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Por recorrer los códigos ajenos',
              par$cod_identificador   => '');

            IF par$aajen_nel_index_entidad IS NOT NULL AND par$aajen_nel_index_entidad.COUNT >0 THEN
                FOR idxAJ IN par$aajen_nel_index_entidad.first .. par$aajen_nel_index_entidad.last LOOP
                    IF idxAJ > 0 AND par$aajen_nel_index_entidad(idxAJ) IS NOT NULL THEN
                      IF par$aajen_cod_entidad(idxAJ) = var$entidad_cliente
                        AND par$aajen_nel_index_entidad(idxAJ) = par$anel_index(idx) THEN
                            var$aajen_cod_accion(var$aajen_cod_accion.count) :=  par$aajen_cod_accion(idxAJ);
                            var$aajen_oid_entidad(var$aajen_oid_entidad.count) := var$oid_cliente;
                            var$aajen_cod_entidad(var$aajen_cod_entidad.count) :=  par$aajen_cod_entidad(idxAJ);
                            var$aajen_nel_index_entidad(var$aajen_nel_index_entidad.count) :=  par$aajen_nel_index_entidad(idxAJ);
                            var$aajen_nel_index_padre(var$aajen_nel_index_padre.count) := NULL;
                            var$aajen_cod_identificador(var$aajen_cod_identificador.count) :=  par$aajen_cod_identificador(idxAJ);
                            var$aajen_cod_codigo_ajeno(var$aajen_cod_codigo_ajeno.count) :=  par$aajen_cod_codigo_ajeno(idxAJ);
                            var$aajen_cod_desc_ajeno(var$aajen_cod_desc_ajeno.count) :=  par$aajen_cod_desc_ajeno(idxAJ);
                            var$aajen_bol_defecto(var$aajen_bol_defecto.count) :=  par$aajen_bol_defecto(idxAJ);
                      END IF;
                    END IF;
                END LOOP;
            END IF;

            -- En caso de no tener codigos ajenos, en caso de ALTA crea el codigo ajeno MAE
            IF var$cod_accion = 'ALTA'  and var$aajen_cod_accion.count = 0 THEN
                var$aajen_cod_accion(var$aajen_cod_accion.count) :=  'ALTA';
                var$aajen_oid_entidad(var$aajen_oid_entidad.count) := var$oid_cliente;
                var$aajen_cod_entidad(var$aajen_cod_entidad.count) :=  'CLIENTE';
                var$aajen_nel_index_entidad(var$aajen_nel_index_entidad.count) :=  par$anel_index(idx);
                var$aajen_nel_index_padre(var$aajen_nel_index_padre.count) := NULL;
                var$aajen_cod_identificador(var$aajen_cod_identificador.count) :=  'MAE';
                var$aajen_cod_codigo_ajeno(var$aajen_cod_codigo_ajeno.count) := var$cod_cliente;
                var$aajen_cod_desc_ajeno(var$aajen_cod_desc_ajeno.count) := var$des_descripcion;
                var$aajen_bol_defecto(var$aajen_bol_defecto.count) :=  1;
            END IF;

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Por validar códigos ajenos',
              par$cod_identificador   => '');
            svalidar_codigos_ajenos(
                par$oid_llamada                => par$oid_llamada,
                par$aajen_cod_accion           => var$aajen_cod_accion,
                par$aajen_oid_entidad          => var$aajen_oid_entidad,
                par$aajen_cod_entidad          => var$aajen_cod_entidad,
                par$aajen_nel_index_entidad    => var$aajen_nel_index_entidad,
                par$aajen_nel_index_padre      => var$aajen_nel_index_padre,
                par$aajen_cod_identificador    => var$aajen_cod_identificador,
                par$aajen_cod_codigo_ajeno     => var$aajen_cod_codigo_ajeno,
                par$aajen_cod_desc_ajeno       => var$aajen_cod_desc_ajeno,
                par$aajen_bol_defecto          => var$aajen_bol_defecto,
                par$cod_cultura                => par$cod_cultura);

            --Validar datos bancarios
            --INICIO - Preparar datos bancarios de cliente para validar
            IF par$aban_cod_accion IS NOT NULL AND par$aban_cod_accion.COUNT > 0 THEN
                --Datos Bancarios - Limpio las variables 
                var$aban_cod_accion.delete;
                var$aban_cod_entidad.delete;
                var$aban_nel_index_entidad.delete;
                var$aban_oid_identificador.delete;
                var$aban_oid_cliente.delete;
                var$aban_oid_subcliente.delete;
                var$aban_oid_pto_servicio.delete;
                var$aban_cod_codigobanco.delete;
                var$aban_cod_codigoagencia.delete;
                var$aban_cod_numerocuenta.delete;
                var$aban_cod_tipo_cuenta.delete;
                var$aban_des_titularidad.delete;
                var$aban_cod_divisa.delete;

                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Por recorrer datos bancarios',
                  par$cod_identificador   => '');

                FOR idxDb IN par$aban_cod_accion.first .. par$aban_cod_accion.last LOOP
                  IF idxDb > 0 AND par$aban_cod_accion(idxDb) IS NOT NULL THEN
                    IF par$aban_cod_entidad(idxDb) = var$entidad_cliente
                      AND par$aban_nel_index_entidad(idxDb) = par$anel_index(idx) THEN
                        --Datos Bancarios - Preparo los datos para pasar al procedure 
                        var$aban_cod_accion(var$aban_cod_accion.count) :=  par$aban_cod_accion(idxDb);
                        var$aban_cod_entidad(var$aban_cod_entidad.count) := par$aban_cod_entidad(idxDb);
                        var$aban_nel_index_entidad(var$aban_nel_index_entidad.count) := par$aban_nel_index_entidad(idxDb);
                        var$aban_oid_identificador(var$aban_oid_identificador.count) := par$aban_oid_identificador(idxDb);
                        var$aban_oid_cliente(var$aban_oid_cliente.count) := var$oid_cliente;
                        var$aban_oid_subcliente(var$aban_oid_subcliente.count) := null;
                        var$aban_oid_pto_servicio(var$aban_oid_pto_servicio.count) := null;
                        var$aban_cod_codigobanco(var$aban_cod_codigobanco.count) := par$aban_cod_codigobanco(idxDb);
                        var$aban_cod_codigoagencia(var$aban_cod_codigoagencia.count) := par$aban_cod_codigoagencia(idxDb);
                        var$aban_cod_numerocuenta(var$aban_cod_numerocuenta.count) := par$aban_cod_numerocuenta(idxDb);
                        var$aban_cod_tipo_cuenta(var$aban_cod_tipo_cuenta.count) := par$aban_cod_tipo_cuenta(idxDb);
                        var$aban_des_titularidad(var$aban_des_titularidad.count) := par$aban_des_titularidad(idxDb);
                        var$aban_cod_divisa(var$aban_cod_divisa.count) := par$aban_cod_divisa(idxDb);
                    END IF;
                  END IF;
                END LOOP;

                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Por ingresar en: SAPR_PDATO_BANCARIO_###VERSION###.svalidar_dato_bancario',
                  par$cod_identificador   => '');

                SAPR_PDATO_BANCARIO_###VERSION###.svalidar_dato_bancario(
                        par$oid_llamada                 => par$oid_llamada,
                        par$acod_accion                 => var$aban_cod_accion,
                        par$anel_index                  => var$aban_nel_index_entidad,
                        par$aoid_dato_bancario          => var$aban_oid_identificador,
                        par$acod_entidad                => var$aban_cod_entidad,
                        par$aoid_cliente                => var$aban_oid_cliente,
                        par$aoid_subcliente             => var$aban_oid_subcliente,
                        par$aoid_pto_servicio           => var$aban_oid_pto_servicio,
                        par$acod_banco                  => var$aban_cod_codigobanco,
                        par$acod_agencia                => var$aban_cod_codigoagencia,
                        par$acod_divisa                 => var$aban_cod_divisa,
                        par$acod_cuenta_bancaria        => var$aban_cod_numerocuenta,
                        par$acod_tipo_cuenta_bancaria   => var$aban_cod_tipo_cuenta,
                        par$ades_titularidad            => var$aban_des_titularidad,
                        par$cod_cultura                 => par$cod_cultura,
                        par$cod_usuario                 => par$cod_usuario,
                        par$oid_usuario                 => var$oid_usuario);


                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                SELECT OID_CAMPO1, COD_CAMPO2, var$entidad_cliente, var$cod_cliente, 'VALIDACIONES', par$anel_index(idx), NULL 
                FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO' AND OID_CAMPO1 <> '0040080000';
                DELETE SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO';

            END IF;


            DBMS_OUTPUT.PUT_LINE('Hace COMMIT de VALIDACIONES de CLIENTE de indice:' || par$anel_index(idx));
            COMMIT;
            SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Hace COMMIT de VALIDACIONES de CLIENTE de indice:' || par$anel_index(idx),
                  par$cod_identificador   => '');

            --FIN VALIDACIONES

            --GRABAR CLIENTE
            IF NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_cliente,
                                            par$nel_index  => par$anel_index(idx)) THEN

                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Por ingresar en supd_cliente - oid_cliente: ' || var$oid_cliente,
                  par$cod_identificador   => '');
                supd_cliente(par$oid_cliente        => var$oid_cliente,
                  par$cod_cliente                 => var$cod_cliente,
                  par$des_cliente                 => var$des_descripcion,
                  par$oid_tipo_cliente            => var$oid_tipo_cliente,
                  par$cod_bancario                => var$cod_codigo_bancario,
                  par$bol_vigente                 => var$bol_vigente,
                  par$bol_totaliza_saldo          => var$bol_totaliza_saldo ,
                  par$bol_abona_por_total         => var$bol_abona_por_total ,
                  par$bol_banco_capital           => var$bol_banco_capital,
                  par$bol_banco_comision          => var$bol_banco_comision,
                  par$num_percent_comision        => var$nel_porc_comision,
                  par$cod_usuario                 => par$cod_usuario);

                  IF var$cod_accion = 'ALTA' THEN
                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                          VALUES (
                              var$oid_cliente,
                              par$anel_index(idx),
                              'OID_CLIENTE');
                  END IF;
            END IF;

            IF NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_cliente,
                                                  par$nel_index  => par$anel_index(idx)) THEN
              
              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Por grabar ajenos - par$entidad: ' || var$entidad_cliente || ' par$nel_index: ' || par$anel_index(idx) ,
                  par$cod_identificador   => '');
              --GRABAR CODIGOS AJENOS
              IF var$aajen_cod_identificador IS NOT NULL AND var$aajen_cod_identificador.COUNT >0 THEN
                  FOR idxAj IN var$aajen_cod_identificador.first .. var$aajen_cod_identificador.last LOOP

                    IF var$aajen_cod_accion(idxAj) IS NULL OR var$aajen_cod_accion(idxAj) = 'ALTA' THEN
                      gepr_pcodigo_ajeno_###VERSION###.sconfigurar_ajeno(
                                          par$oid_tabla_genesis      => var$oid_cliente,
                                          par$cod_tabla_genesis      => 'GEPR_TCLIENTE',
                                          par$cod_identificador      => var$aajen_cod_identificador(idxAj),
                                          par$cod_ajeno              => var$aajen_cod_codigo_ajeno(idxAj),
                                          par$des_ajeno              => var$aajen_cod_desc_ajeno(idxAj),
                                          par$bol_defecto            => var$aajen_bol_defecto(idxAj),
                                          par$cod_usuario            => par$cod_usuario);
                    ELSIF var$aajen_cod_accion(idxAj) = 'BAJA' THEN
                      gepr_pcodigo_ajeno_###VERSION###.seliminar_ajeno(
                              par$oid_tabla_genesis  => var$oid_cliente,
                              par$cod_tabla_genesis  => 'GEPR_TCLIENTE',
                              par$cod_identificador  => var$aajen_cod_identificador(idxAj));
                    END IF;
                  END LOOP;
              END IF;

              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Por preparar direcciones ajenos - par$entidad: ' || var$entidad_cliente || ' par$nel_index: ' || par$anel_index(idx) ,
                  par$cod_identificador   => '');
              --Preparar datos de direcciones
              IF par$adir_cod_entidad IS NOT NULL AND par$adir_cod_entidad.COUNT >0 THEN
                FOR idxDir IN par$adir_cod_entidad.first .. par$adir_cod_entidad.last LOOP
                  IF idxDir > 0 AND par$adir_nel_index_entidad(idxDir) IS NOT NULL THEN
                    IF  par$adir_cod_entidad(idxDir) = var$entidad_cliente
                        AND par$adir_nel_index_entidad(idxDir) = par$anel_index(idx) THEN

                            var$adir_cod_entidad(var$adir_cod_entidad.count) := par$adir_cod_entidad(idxDir);
                            var$adir_nel_index_entidad(var$adir_nel_index_entidad.count) := par$adir_nel_index_entidad(idxDir);
                            var$adir_des_pais(var$adir_des_pais.count) := par$adir_des_pais(idxDir);
                            var$adir_des_prov(var$adir_des_prov.count) := par$adir_des_prov(idxDir);
                            var$adir_des_ciudad(var$adir_des_ciudad.count) := par$adir_des_ciudad(idxDir);
                            var$adir_des_num_tel(var$adir_des_num_tel.count) := par$adir_des_num_tel(idxDir);
                            var$adir_des_email(var$adir_des_email.count) := par$adir_des_email(idxDir);
                            var$adir_cod_fiscal(var$adir_cod_fiscal.count) := par$adir_cod_fiscal(idxDir);
                            var$adir_cod_postal(var$adir_cod_postal.count) := par$adir_cod_postal(idxDir);
                            var$adir_des_dic_1(var$adir_des_dic_1.count) := par$adir_des_dic_1(idxDir);
                            var$adir_des_dic_2(var$adir_des_dic_2.count) := par$adir_des_dic_2(idxDir);
                            var$adir_des_cam_adicional_1(var$adir_des_cam_adicional_1.count) := par$adir_des_cam_adicional_1(idxDir);
                            var$adir_des_cam_adicional_2(var$adir_des_cam_adicional_2.count) := par$adir_des_cam_adicional_2(idxDir);
                            var$adir_des_cam_adicional_3(var$adir_des_cam_adicional_3.count) := par$adir_des_cam_adicional_3(idxDir);
                            var$adir_des_cat_adicional_1(var$adir_des_cat_adicional_1.count) := par$adir_des_cat_adicional_1(idxDir);
                            var$adir_des_cat_adicional_2(var$adir_des_cat_adicional_2.count) := par$adir_des_cat_adicional_2(idxDir);
                            var$adir_des_cat_adicional_3(var$adir_des_cat_adicional_3.count) := par$adir_des_cat_adicional_3(idxDir);
                    END IF;
                  END IF;
                END LOOP;
              END IF;

              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Por grabar direcciones - par$entidad: ' || var$entidad_cliente || ' par$nel_index: ' || par$anel_index(idx) ,
                  par$cod_identificador   => '');
              --GRABAR DIRECCIONES
              IF var$adir_cod_entidad IS NOT NULL AND var$adir_cod_entidad.COUNT >0 THEN
                  FOR idxDir IN var$adir_cod_entidad.first .. var$adir_cod_entidad.last LOOP          
                    BEGIN
                        SELECT OID_DIRECCION
                        INTO var$oid_direccion_cli
                        FROM GEPR_TDIRECCION
                        WHERE COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE' AND OID_TABLA_GENESIS = var$oid_cliente;
                    EXCEPTION WHEN no_data_found THEN
                        var$oid_direccion_cli := NULL;
                    END;

                    supd_direccion(par$oid_direccion        => var$oid_direccion_cli,
                        par$oid_tabla_gen                       => var$oid_cliente,
                        par$cod_tipo_tgen                       => 'GEPR_TCLIENTE',
                        par$des_pais                            => var$adir_des_pais(idxDir),
                        par$des_prov                            => var$adir_des_prov(idxDir),
                        par$des_ciudad                          => var$adir_des_ciudad(idxDir),
                        par$des_num_tel                         => var$adir_des_num_tel(idxDir),
                        par$des_email                           => var$adir_des_email(idxDir),
                        par$cod_fiscal                          => var$adir_cod_fiscal(idxDir),
                        par$cod_postal                          => var$adir_cod_postal(idxDir),
                        par$des_dic_1                           => var$adir_des_dic_1(idxDir),
                        par$des_dic_2                           => var$adir_des_dic_2(idxDir),
                        par$des_cam_adicional_1                 => var$adir_des_cam_adicional_1(idxDir),
                        par$des_cam_adicional_2                 => Var$adir_des_cam_adicional_2(idxDir),
                        par$des_cam_adicional_3                 => var$adir_des_cam_adicional_3(idxDir),
                        par$des_cat_adicional_1                 => var$adir_des_cat_adicional_1(idxDir),
                        par$des_cat_adicional_2                 => var$adir_des_cat_adicional_2(idxDir),
                        par$des_cat_adicional_3                 => var$adir_des_cat_adicional_3(idxDir),
                        par$cod_usuario                         => par$cod_usuario);
                  END LOOP;
              END IF;

              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Por grabar datos bancarios  - par$entidad: ' || var$entidad_cliente || ' par$nel_index: ' || par$anel_index(idx) ,
                  par$cod_identificador   => '');
              --INICIO - Grabar datos bancarios de cliente
              IF par$aban_cod_accion IS NOT NULL AND par$aban_cod_accion.COUNT > 0 THEN
                --Datos Bancarios - Limpio las variables 
                var$aban_cod_accion.delete;
                var$aban_cod_entidad.delete;
                var$aban_nel_index_entidad.delete;
                var$aban_oid_identificador.delete;
                var$aban_oid_cliente.delete;
                var$aban_oid_subcliente.delete;
                var$aban_oid_pto_servicio.delete;
                var$aban_cod_codigobanco.delete;
                var$aban_cod_codigoagencia.delete;
                var$aban_cod_numerocuenta.delete;
                var$aban_cod_tipo_cuenta.delete;
                var$aban_cod_documento.delete;
                var$aban_des_titularidad.delete;
                var$aban_cod_divisa.delete;
                var$aban_des_observaciones.delete;
                var$aban_bol_defecto.delete;
                var$aban_bol_activo.delete;
                var$aban_des_campoadicional1.delete;
                var$aban_des_campoadicional2.delete;
                var$aban_des_campoadicional3.delete;
                var$aban_des_campoadicional4.delete;
                var$aban_des_campoadicional5.delete;
                var$aban_des_campoadicional6.delete;
                var$aban_des_campoadicional7.delete;
                var$aban_des_campoadicional8.delete;
                var$aban_des_comentario.delete;

                --Buscamos el mensaje para el comentario traducido
                --msgComentario - Registro creado por el servicio de configuración de clientes 
                var$comentario := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgComentario',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                '',
                0);
                FOR idxDb IN par$aban_cod_accion.first .. par$aban_cod_accion.last LOOP
                  IF par$aban_cod_entidad(idxDb) = var$entidad_cliente
                    AND par$aban_nel_index_entidad(idxDb) = par$anel_index(idx) THEN
                    --Datos Bancarios - Preparo los datos para pasar al procedure 
                    var$aban_cod_accion(var$aban_cod_accion.count) :=  par$aban_cod_accion(idxDb);
                    var$aban_cod_entidad(var$aban_cod_entidad.count) := par$aban_cod_entidad(idxDb);
                    var$aban_nel_index_entidad(var$aban_nel_index_entidad.count) := par$aban_nel_index_entidad(idxDb);
                    var$aban_oid_identificador(var$aban_oid_identificador.count) := par$aban_oid_identificador(idxDb);
                    var$aban_oid_cliente(var$aban_oid_cliente.count) := var$oid_cliente;
                    var$aban_oid_subcliente(var$aban_oid_subcliente.count) := null;
                    var$aban_oid_pto_servicio(var$aban_oid_pto_servicio.count) := null;
                    var$aban_cod_codigobanco(var$aban_cod_codigobanco.count) := par$aban_cod_codigobanco(idxDb);
                    var$aban_cod_codigoagencia(var$aban_cod_codigoagencia.count) := par$aban_cod_codigoagencia(idxDb);
                    var$aban_cod_numerocuenta(var$aban_cod_numerocuenta.count) := par$aban_cod_numerocuenta(idxDb);
                    var$aban_cod_tipo_cuenta(var$aban_cod_tipo_cuenta.count) := par$aban_cod_tipo_cuenta(idxDb);
                    var$aban_cod_documento(var$aban_cod_documento.count) := par$aban_cod_documento(idxDb);
                    var$aban_des_titularidad(var$aban_des_titularidad.count) := par$aban_des_titularidad(idxDb);
                    var$aban_cod_divisa(var$aban_cod_divisa.count) := par$aban_cod_divisa(idxDb);
                    var$aban_des_observaciones(var$aban_des_observaciones.count) := par$aban_des_observaciones(idxDb);
                    var$aban_bol_defecto(var$aban_bol_defecto.count) := par$aban_bol_defecto(idxDb);
                    var$aban_des_campoadicional1(var$aban_des_campoadicional1.count) := par$aban_des_campoadicional1(idxDb);
                    var$aban_des_campoadicional2(var$aban_des_campoadicional2.count) := par$aban_des_campoadicional2(idxDb);
                    var$aban_des_campoadicional3(var$aban_des_campoadicional3.count) := par$aban_des_campoadicional3(idxDb);
                    var$aban_des_campoadicional4(var$aban_des_campoadicional4.count) := par$aban_des_campoadicional4(idxDb);
                    var$aban_des_campoadicional5(var$aban_des_campoadicional5.count) := par$aban_des_campoadicional5(idxDb);
                    var$aban_des_campoadicional6(var$aban_des_campoadicional6.count) := par$aban_des_campoadicional6(idxDb);
                    var$aban_des_campoadicional7(var$aban_des_campoadicional7.count) := par$aban_des_campoadicional7(idxDb);
                    var$aban_des_campoadicional8(var$aban_des_campoadicional8.count) := par$aban_des_campoadicional8(idxDb);
                    var$aban_des_comentario(var$aban_des_comentario.count) := var$comentario;
                    --En caso de accion baja, bol_activo = 0, caso contrario bol_activo = 1
                    IF par$aban_cod_accion(idxDb) = 'BAJA' THEN
                      var$aban_bol_activo(var$aban_bol_activo.count) := 0;
                    ELSE
                      var$aban_bol_activo(var$aban_bol_activo.count) := 1;
                    END IF;

                  END IF;
                END LOOP;

                -- Llamo a la procedure para grabar los datos bancarios
                SAPR_PDATO_BANCARIO_###VERSION###.sconfigurar_dato_bancario(
                  par$oid_llamada                 => par$oid_llamada,
                  par$anel_index                  => var$aban_nel_index_entidad,
                  par$acod_entidad                => var$aban_cod_entidad,
                  par$acod_accion                 => var$aban_cod_accion,
                  par$aoid_dato_bancario          => var$aban_oid_identificador,
                  par$aoid_cliente                => var$aban_oid_cliente,
                  par$aoid_subcliente             => var$aban_oid_subcliente,
                  par$aoid_pto_servicio           => var$aban_oid_pto_servicio,
                  par$acod_banco                  => var$aban_cod_codigobanco,
                  par$acod_agencia                => var$aban_cod_codigoagencia,
                  par$acod_tipo_cuenta_bancaria   => var$aban_cod_tipo_cuenta,
                  par$acod_cuenta_bancaria        => var$aban_cod_numerocuenta,
                  par$acod_documento              => var$aban_cod_documento,
                  par$ades_titularidad            => var$aban_des_titularidad,
                  par$acod_divisa                 => var$aban_cod_divisa,
                  par$ades_observaciones          => var$aban_des_observaciones,
                  par$abol_defecto                => var$aban_bol_defecto,
                  par$abol_activo                 => var$aban_bol_activo,
                  par$ades_campo_adicional_1      => var$aban_des_campoadicional1,
                  par$ades_campo_adicional_2      => var$aban_des_campoadicional2,
                  par$ades_campo_adicional_3      => var$aban_des_campoadicional3,
                  par$ades_campo_adicional_4      => var$aban_des_campoadicional4,
                  par$ades_campo_adicional_5      => var$aban_des_campoadicional5,
                  par$ades_campo_adicional_6      => var$aban_des_campoadicional6,
                  par$ades_campo_adicional_7      => var$aban_des_campoadicional7,
                  par$ades_campo_adicional_8      => var$aban_des_campoadicional8,
                  par$ades_comentario             => var$aban_des_comentario,
                  par$cod_cultura                 => par$cod_cultura,
                  par$cod_usuario                 => par$cod_usuario,
                  par$cod_pais                    => var$cod_pais,
                  par$info_ejecucion              => par$info_ejecucion,
                  par$bol_limpiar_temporal        => 0,
                  par$rc_validaciones             => par$rc_validaciones,
                  par$cod_ejecucion               => par$cod_ejecucion);

              END IF;
              --FIN - Grabar datos bancarios de cliente

            END IF;


            IF NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_cliente,
                                                par$nel_index  => par$anel_index(idx)) THEN

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR, NEL_CAMPO5)
                            VALUES (
                            '0040080000',
                            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040080000',
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0),
                                var$entidad_cliente,
                                par$acod_cliente(idx),
                                'VALIDACIONES',
                                par$anel_index(idx));
                DBMS_OUTPUT.PUT_LINE('Hace COMMIT de CLIENTE de indice:' || par$anel_index(idx));

                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Hace COMMIT de CLIENTE de indice:' || par$anel_index(idx) || const$new_line
                                            || '0040080000 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040080000',
                                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                            NULL,
                                            0),
                    par$cod_identificador   => '');
                
                COMMIT;
            ELSE
                DBMS_OUTPUT.PUT_LINE('Hace ROLLBACK de CLIENTE de indice:' || par$anel_index(idx));
                ROLLBACK;
            END IF;
          END IF;

        EXCEPTION
            WHEN OTHERS THEN
                DBMS_OUTPUT.PUT_LINE('Hace ROLLBACK por OTHERS de CLIENTE de indice:' || par$anel_index(idx));
                ROLLBACK;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3,  COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                                VALUES (
                                '2040080000',
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080000',
                                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                        NULL,
                                        0),
                                    var$entidad_cliente,
                                    par$acod_cliente(idx),
                                    'VALIDACIONES',
                                    par$anel_index(idx));


              SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                      par$des_detalle     => 'Hace ROLLBACK por OTHERS de CLIENTE de indice: ' || par$anel_index(idx) || const$new_line || SQLERRM,
                par$cod_identificador   => '');

        END;
      END IF;
    END LOOP;

    -- grabar subcliente
    var$ascli_cod_accion.delete;
    var$ascli_nel_index.delete;
    var$ascli_nel_index_scli.delete;
    var$ascli_oid_cliente.delete;
    var$ascli_cod_subcliente.delete;
    var$ascli_des_descripcion.delete;

    IF  par$ascli_nel_index IS NOT NULL AND  par$ascli_nel_index.COUNT > 0 THEN
        FOR idx IN par$ascli_nel_index.first .. par$ascli_nel_index.last LOOP
            IF idx > 0 and par$ascli_nel_index(idx) is not null THEN
                IF par$ascli_nel_index(idx) IS NOT NULL AND  NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_cliente, par$nel_index  => par$ascli_nel_index(idx))  AND
                    NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_subcliente, par$nel_index  => par$ascli_nel_index_scli(idx))   THEN
                    BEGIN
                        SELECT OID_CAMPO1
                            INTO var$oid_cliente
                        FROM SAPR_GTT_TAUXILIAR
                            WHERE COD_CALIFICADOR = 'OID_CLIENTE'  AND
                                COD_CAMPO2 =   par$ascli_nel_index(idx) AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                            var$oid_cliente := NULL;
                    END;

                    BEGIN
                        SELECT OID_CAMPO1
                            INTO var$oid_subcliente
                        FROM SAPR_GTT_TAUXILIAR
                            WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'  AND
                                COD_CAMPO2 =  par$ascli_nel_index(idx) AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                            var$oid_subcliente := NULL;
                    END;


                    var$ascli_cod_accion(var$ascli_cod_accion.count)            := par$ascli_cod_accion(idx);
                    var$ascli_oid_cliente(var$ascli_oid_cliente.count)          := var$oid_cliente;
                    var$ascli_oid_subcliente(var$ascli_oid_subcliente.count)    := var$oid_subcliente;
                    var$ascli_nel_index(var$ascli_nel_index.count)              := par$ascli_nel_index(idx);
                    var$ascli_nel_index_scli(var$ascli_nel_index_scli.count)    := par$ascli_nel_index_scli(idx);
                    var$ascli_cod_subcliente(var$ascli_cod_subcliente.count)    := par$ascli_cod_subcliente(idx);
                    var$ascli_des_descripcion(var$ascli_des_descripcion.count)  := par$ascli_des_descripcion(idx);
                ELSE
                  IF fhay_error_configurar_cliente (par$entidad    => var$entidad_cliente, par$nel_index  => par$ascli_nel_index(idx)) THEN
                      -- Ocurrió un error en la entidad padre.
                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, NEL_CAMPO5, NEL_CAMPO6, COD_CALIFICADOR)
                          VALUES (
                              '2040080006',
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080006',
                                          gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                          NULL,
                                          0),
                              var$entidad_subcliente,
                              par$ascli_cod_subcliente(idx),
                              par$ascli_nel_index_scli(idx),
                              par$ascli_nel_index(idx),
                              'VALIDACIONES');

                      SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => 'var$entidad_subcliente:  ' || var$entidad_subcliente || const$new_line
                                                  || 'par$ascli_cod_subcliente(' || idx || ') : ' || par$ascli_cod_subcliente(idx) || const$new_line
                                                  || 'par$ascli_nel_index_scli(' || idx || ') : ' || par$ascli_nel_index_scli(idx) || const$new_line
                                                  || 'par$ascli_nel_index(' || idx || ') : ' || par$ascli_nel_index(idx) || const$new_line
                                                  || '2040080006 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080006',
                                          gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                          NULL,
                                          0),
                          par$cod_identificador   => '');

                  END IF;
                  COMMIT;
                END IF;
            END IF;
        END LOOP;
    END IF;

    IF var$ascli_nel_index IS NOT NULL AND var$ascli_nel_index.COUNT > 0 THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => 'Por configurar_subcliente',
                          par$cod_identificador   => '');

        sconfigurar_subcliente(
            par$oid_llamada               => par$oid_llamada,
            par$cod_identificador_ajeno   => par$cod_identificador_ajeno,
            --Subcliente
            par$ascli_cod_accion          => var$ascli_cod_accion,
            par$ascli_nel_index           => var$ascli_nel_index,
            par$ascli_nel_index_scli      => var$ascli_nel_index_scli,
            par$ascli_oid_cliente         => var$ascli_oid_cliente,
            par$ascli_cod_subcliente      => var$ascli_cod_subcliente,
            par$ascli_des_descripcion     => var$ascli_des_descripcion,

            --Datos Bancarios
            par$aban_cod_accion            => par$aban_cod_accion,
            par$aban_cod_entidad           => par$aban_cod_entidad,
            par$aban_nel_index_entidad     => par$aban_nel_index_entidad,
            par$aban_oid_identificador     => par$aban_oid_identificador,
            par$aban_cod_codigobanco       => par$aban_cod_codigobanco,
            par$aban_cod_codigoagencia     => par$aban_cod_codigoagencia,
            par$aban_cod_numerocuenta      => par$aban_cod_numerocuenta,
            par$aban_cod_tipo_cuenta       => par$aban_cod_tipo_cuenta,
            par$aban_cod_documento         => par$aban_cod_documento,
            par$aban_des_titularidad       => par$aban_des_titularidad,
            par$aban_cod_divisa            => par$aban_cod_divisa,
            par$aban_des_observaciones     => par$aban_des_observaciones,
            par$aban_bol_defecto           => par$aban_bol_defecto,
            par$aban_des_campoadicional1   => par$aban_des_campoadicional1,
            par$aban_des_campoadicional2   => par$aban_des_campoadicional2,
            par$aban_des_campoadicional3   => par$aban_des_campoadicional3,
            par$aban_des_campoadicional4   => par$aban_des_campoadicional4,
            par$aban_des_campoadicional5   => par$aban_des_campoadicional5,
            par$aban_des_campoadicional6   => par$aban_des_campoadicional6,
            par$aban_des_campoadicional7   => par$aban_des_campoadicional7,
            par$aban_des_campoadicional8   => par$aban_des_campoadicional8,

            --Codigo Ajeno
            par$aajen_cod_accion           => par$aajen_cod_accion,
            par$aajen_cod_entidad          => par$aajen_cod_entidad,
            par$aajen_nel_index_entidad    => par$aajen_nel_index_entidad,
            par$aajen_cod_identificador    => par$aajen_cod_identificador,
            par$aajen_cod_codigo_ajeno     => par$aajen_cod_codigo_ajeno,
            par$aajen_cod_desc_ajeno       => par$aajen_cod_desc_ajeno,
            par$aajen_bol_defecto          => par$aajen_bol_defecto,

            --Direcciones
            par$adir_cod_entidad           => par$adir_cod_entidad,
            par$adir_nel_index_entidad     => par$adir_nel_index_entidad,
            par$adir_des_pais              => par$adir_des_pais,
            par$adir_des_prov              => par$adir_des_prov,
            par$adir_des_ciudad            => par$adir_des_ciudad,
            par$adir_des_num_tel           => par$adir_des_num_tel,
            par$adir_des_email             => par$adir_des_email,
            par$adir_cod_fiscal            => par$adir_cod_fiscal,
            par$adir_cod_postal            => par$adir_cod_postal,
            par$adir_des_dic_1             => par$adir_des_dic_1,
            par$adir_des_dic_2             => par$adir_des_dic_2,
            par$adir_des_cam_adicional_1   => par$adir_des_cam_adicional_1,
            par$adir_des_cam_adicional_2   => par$adir_des_cam_adicional_2,
            par$adir_des_cam_adicional_3   => par$adir_des_cam_adicional_3,
            par$adir_des_cat_adicional_1   => par$adir_des_cat_adicional_1,
            par$adir_des_cat_adicional_2   => par$adir_des_cat_adicional_2,
            par$adir_des_cat_adicional_3   => par$adir_des_cat_adicional_3,

            par$cod_cultura                => par$cod_cultura,
            par$cod_usuario                => par$cod_usuario,
            par$cod_pais                   => var$cod_pais);


        SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => 'FIN configurar_subcliente',
                          par$cod_identificador   => '');
    END IF;


    -- grabar punto servicio 
    var$apser_cod_accion.delete;
    var$apser_nel_index.delete;
    var$apser_nel_index_scli.delete;
    var$apser_nel_index_pto.delete;
    var$apser_cod_pto_servicio.delete;
    var$apser_des_pto_servicio.delete;



    IF  par$apser_nel_index IS NOT NULL AND  par$apser_nel_index.COUNT > 0 THEN
        FOR idx IN par$apser_nel_index.first .. par$apser_nel_index.last LOOP

            IF  par$apser_nel_index (idx) IS NOT NULL AND NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_cliente, par$nel_index  => par$apser_nel_index(idx))  AND
                NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_subcliente, par$nel_index  => par$apser_nel_index_scli(idx)) AND
                NOT fhay_error_configurar_cliente (par$entidad    => var$entidad_pto_servicio, par$nel_index  => par$apser_nel_index_pto(idx))   THEN

                BEGIN
                     SELECT OID_CAMPO1
                        INTO var$oid_subcliente
                        FROM SAPR_GTT_TAUXILIAR
                        WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'  AND
                            COD_CAMPO2 =  par$apser_nel_index_scli(idx) AND ROWNUM = 1;
                EXCEPTION WHEN no_data_found THEN
                        var$oid_subcliente := NULL;
                END;


                BEGIN
                     SELECT OID_CAMPO1
                        INTO var$oid_pto_servicio
                        FROM SAPR_GTT_TAUXILIAR
                        WHERE COD_CALIFICADOR = 'OID_PUNTOSERVICIO'  AND
                            COD_CAMPO2 =   par$apser_nel_index_pto(idx) AND ROWNUM = 1;
                EXCEPTION WHEN no_data_found THEN
                        var$oid_pto_servicio := NULL;
                END;

                --Punto Servicio
                var$apser_cod_accion(var$apser_cod_accion.count)                := par$apser_cod_accion(idx);
                var$apser_oid_subcliente(var$apser_oid_subcliente.count)        := var$oid_subcliente;
                var$apser_oid_pto_servicio(var$apser_oid_pto_servicio.count)    := var$oid_pto_servicio;
                var$apser_nel_index(var$apser_nel_index.count)                  := par$apser_nel_index(idx);
                var$apser_nel_index_scli(var$apser_nel_index_scli.count)        := par$apser_nel_index_scli(idx);
                var$apser_nel_index_pto(var$apser_nel_index_pto.count)          := par$apser_nel_index_pto(idx);
                var$apser_cod_pto_servicio(var$apser_cod_pto_servicio.count)    := par$apser_cod_pto_servicio(idx);
                var$apser_des_pto_servicio(var$apser_des_pto_servicio.count)    := par$apser_des_pto_servicio(idx);
            ELSE
               IF fhay_error_configurar_cliente (par$entidad    => var$entidad_subcliente, par$nel_index  => par$apser_nel_index_scli(idx)) THEN

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, NEL_CAMPO5, NEL_CAMPO6, COD_CALIFICADOR)
                                VALUES (
                                  '2040080006',
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080006',
                                          gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                          NULL,
                                          0),
                                    var$entidad_pto_Servicio,
                                    par$apser_cod_pto_servicio(idx),
                                    par$apser_nel_index_pto(idx),
                                    par$apser_nel_index_scli(idx),
                                    'VALIDACIONES');
                  SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => '2040080006 - ' || gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080006',
                                          gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                          NULL,
                                          0),
                          par$cod_identificador   => '');
               END IF;
            END IF;

        END LOOP;
    END IF;


    IF var$apser_nel_index IS NOT NULL AND var$apser_nel_index.COUNT > 0 THEN
            sconfigurar_ptoservicio(
                par$oid_llamada               => par$oid_llamada,
                par$cod_identificador_ajeno   => par$cod_identificador_ajeno,

                --Punto Servicio
                par$apser_cod_accion          => var$apser_cod_accion,
                par$apser_nel_index           => var$apser_nel_index,
                par$apser_nel_index_scli      => var$apser_nel_index_scli,
                par$apser_nel_index_pto       => var$apser_nel_index_pto,
                par$apser_cod_pto_servicio    => var$apser_cod_pto_servicio,
                par$apser_des_pto_servicio    => var$apser_des_pto_servicio,

                --Datos Bancarios
                par$aban_cod_accion            => par$aban_cod_accion,
                par$aban_cod_entidad           => par$aban_cod_entidad,
                par$aban_nel_index_entidad     => par$aban_nel_index_entidad,
                par$aban_oid_identificador     => par$aban_oid_identificador,
                par$aban_cod_codigobanco       => par$aban_cod_codigobanco,
                par$aban_cod_codigoagencia     => par$aban_cod_codigoagencia,
                par$aban_cod_numerocuenta      => par$aban_cod_numerocuenta,
                par$aban_cod_tipo_cuenta       => par$aban_cod_tipo_cuenta,
                par$aban_cod_documento         => par$aban_cod_documento,
                par$aban_des_titularidad       => par$aban_des_titularidad,
                par$aban_cod_divisa            => par$aban_cod_divisa,
                par$aban_des_observaciones     => par$aban_des_observaciones,
                par$aban_bol_defecto           => par$aban_bol_defecto,
                par$aban_des_campoadicional1   => par$aban_des_campoadicional1,
                par$aban_des_campoadicional2   => par$aban_des_campoadicional2,
                par$aban_des_campoadicional3   => par$aban_des_campoadicional3,
                par$aban_des_campoadicional4   => par$aban_des_campoadicional4,
                par$aban_des_campoadicional5   => par$aban_des_campoadicional5,
                par$aban_des_campoadicional6   => par$aban_des_campoadicional6,
                par$aban_des_campoadicional7   => par$aban_des_campoadicional7,
                par$aban_des_campoadicional8   => par$aban_des_campoadicional8,

                --Codigo Ajeno
                par$aajen_cod_accion           => par$aajen_cod_accion,
                par$aajen_cod_entidad          => par$aajen_cod_entidad,
                par$aajen_nel_index_entidad    => par$aajen_nel_index_entidad,
                par$aajen_cod_identificador    => par$aajen_cod_identificador,
                par$aajen_cod_codigo_ajeno     => par$aajen_cod_codigo_ajeno,
                par$aajen_cod_desc_ajeno       => par$aajen_cod_desc_ajeno,
                par$aajen_bol_defecto          => par$aajen_bol_defecto,

          --Direcciones
          par$adir_cod_entidad           => par$adir_cod_entidad,
          par$adir_nel_index_entidad     => par$adir_nel_index_entidad,
          par$adir_des_pais              => par$adir_des_pais,
          par$adir_des_prov              => par$adir_des_prov,
          par$adir_des_ciudad            => par$adir_des_ciudad,
          par$adir_des_num_tel           => par$adir_des_num_tel,
          par$adir_des_email             => par$adir_des_email,
          par$adir_cod_fiscal            => par$adir_cod_fiscal,
          par$adir_cod_postal            => par$adir_cod_postal,
          par$adir_des_dic_1             => par$adir_des_dic_1,
          par$adir_des_dic_2             => par$adir_des_dic_2,
          par$adir_des_cam_adicional_1   => par$adir_des_cam_adicional_1,
          par$adir_des_cam_adicional_2   => par$adir_des_cam_adicional_2,
          par$adir_des_cam_adicional_3   => par$adir_des_cam_adicional_3,
          par$adir_des_cat_adicional_1   => par$adir_des_cat_adicional_1,
          par$adir_des_cat_adicional_2   => par$adir_des_cat_adicional_2,
          par$adir_des_cat_adicional_3   => par$adir_des_cat_adicional_3,

                par$cod_cultura                => par$cod_cultura,
          par$cod_usuario                => par$cod_usuario,
          par$cod_pais           => var$cod_pais);

    END IF;

  END IF;

  OPEN par$rc_validaciones FOR
    SELECT OID_CAMPO1 AS CODIGO,
          COD_CAMPO2 AS DESCRIPCION,
          cod_campo3 AS ENTIDAD,
          cod_campo4 AS CODIGO_ENTIDAD,
          nel_campo5 AS IDX,
          nel_campo6 AS IDX_PADRE,
          COD_CALIFICADOR AS CALIFICADOR
          FROM SAPR_GTT_TAUXILIAR AUX
    WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES';

  COMMIT;

  SAPR_PLOG_API.SAGREGA_DETALLE(
    par$oid_llamada  => par$oid_llamada,
    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
    par$des_version     => const$version,
    par$des_detalle     => 'Terminó ' || const$codFuncionalidad || '.' || const$nombre_func,
    par$cod_identificador   => '');

END sconfigurar_cliente;


PROCEDURE sconfigurar_subcliente(
                par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                /*Subcliente*/
                par$ascli_cod_accion          IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$ascli_nel_index           IN gepr_pcomon_###VERSION###.tipo$nels_,
                par$ascli_nel_index_scli      IN gepr_pcomon_###VERSION###.tipo$nels_,
                par$ascli_oid_cliente         IN gepr_pcomon_###VERSION###.tipo$oids_,
                par$ascli_cod_subcliente      IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$ascli_des_descripcion     IN gepr_pcomon_###VERSION###.tipo$descs_,

                /*Datos Bancarios*/
                par$aban_cod_accion           IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aban_cod_entidad          IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aban_nel_index_entidad    IN gepr_pcomon_###VERSION###.tipo$nels_,
                par$aban_oid_identificador    IN gepr_pcomon_###VERSION###.tipo$oids_,
                par$aban_cod_codigobanco      IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aban_cod_codigoagencia    IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aban_cod_numerocuenta     IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aban_cod_tipo_cuenta      IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aban_cod_documento        IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aban_des_titularidad      IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_cod_divisa           IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aban_des_observaciones    IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_bol_defecto          IN gepr_pcomon_###VERSION###.tipo$nbols_,
                par$aban_des_campoadicional1  IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_des_campoadicional2  IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_des_campoadicional3  IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_des_campoadicional4  IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_des_campoadicional5  IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_des_campoadicional6  IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_des_campoadicional7  IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aban_des_campoadicional8  IN gepr_pcomon_###VERSION###.tipo$descs_,

                /*Codigo Ajeno*/
                par$aajen_cod_accion           IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aajen_cod_entidad          IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aajen_nel_index_entidad    IN gepr_pcomon_###VERSION###.tipo$nels_,
                par$aajen_cod_identificador    IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aajen_cod_codigo_ajeno     IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$aajen_cod_desc_ajeno       IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$aajen_bol_defecto          IN gepr_pcomon_###VERSION###.tipo$nbols_,

                /*Direcciones*/
                par$adir_cod_entidad           IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$adir_nel_index_entidad     IN gepr_pcomon_###VERSION###.tipo$nels_,
                par$adir_des_pais              IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_prov              IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_ciudad            IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_num_tel           IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_email              IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_cod_fiscal             IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$adir_cod_postal             IN gepr_pcomon_###VERSION###.tipo$cods_,
                par$adir_des_dic_1              IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_dic_2              IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_cam_adicional_1    IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_cam_adicional_2    IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_cam_adicional_3    IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_cat_adicional_1    IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_cat_adicional_2    IN gepr_pcomon_###VERSION###.tipo$descs_,
                par$adir_des_cat_adicional_3    IN gepr_pcomon_###VERSION###.tipo$descs_,

                par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                par$cod_usuario                IN gepr_pcomon_###VERSION###.tipo$cod_,
                par$cod_pais                   IN gepr_pcomon_###VERSION###.tipo$cod_) IS

                const$nombre_func CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := 'sconfigurar_subcliente';
                var$oid_subcliente          gepr_pcomon_###VERSION###.tipo$oid_;
                var$oid_tipo_subcliente     gepr_pcomon_###VERSION###.tipo$oid_;
                var$bol_enviado_saldos      gepr_pcomon_###VERSION###.tipo$nbol_;
                var$bol_totaliza_saldo      gepr_pcomon_###VERSION###.tipo$nbol_;
                var$entidad                 gepr_pcomon_###VERSION###.tipo$desc_:= 'SUBCLIENTE';

                /*Codigo Ajeno*/
                var$aajen_cod_accion           gepr_pcomon_###VERSION###.tipo$cods_;
                var$aajen_oid_entidad          gepr_pcomon_###VERSION###.tipo$oids_;
                var$aajen_cod_entidad          gepr_pcomon_###VERSION###.tipo$cods_;
                var$aajen_nel_index_entidad    gepr_pcomon_###VERSION###.tipo$nels_;
                var$aajen_nel_index_padre      gepr_pcomon_###VERSION###.tipo$nels_;
                var$aajen_cod_identificador    gepr_pcomon_###VERSION###.tipo$cods_;
                var$aajen_cod_codigo_ajeno     gepr_pcomon_###VERSION###.tipo$cods_;
                var$aajen_cod_desc_ajeno       gepr_pcomon_###VERSION###.tipo$descs_;
                var$aajen_bol_defecto          gepr_pcomon_###VERSION###.tipo$nbols_;

                 /*Direcciones*/
                var$adir_cod_entidad            gepr_pcomon_###VERSION###.tipo$cods_;
                var$adir_nel_index_entidad      gepr_pcomon_###VERSION###.tipo$nels_;
                var$adir_des_pais               gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_prov               gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_ciudad             gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_num_tel            gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_email               gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_cod_fiscal              gepr_pcomon_###VERSION###.tipo$cods_;
                var$adir_cod_postal              gepr_pcomon_###VERSION###.tipo$cods_;
                var$adir_des_dic_1               gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_dic_2               gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_cam_adicional_1     gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_cam_adicional_2     gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_cam_adicional_3     gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_cat_adicional_1     gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_cat_adicional_2     gepr_pcomon_###VERSION###.tipo$descs_;
                var$adir_des_cat_adicional_3     gepr_pcomon_###VERSION###.tipo$descs_;

                /*Datos Bancarios*/
                var$aban_nel_index_entidad     gepr_pcomon_###VERSION###.tipo$nels_;
                var$aban_cod_entidad           gepr_pcomon_###VERSION###.tipo$cods_;
                var$aban_cod_accion            gepr_pcomon_###VERSION###.tipo$cods_;
                var$aban_oid_identificador     gepr_pcomon_###VERSION###.tipo$oids_;
                var$aban_oid_cliente       gepr_pcomon_###VERSION###.tipo$oids_;
                var$aban_oid_subcliente      gepr_pcomon_###VERSION###.tipo$oids_;
                var$aban_oid_pto_servicio      gepr_pcomon_###VERSION###.tipo$oids_;
                var$aban_cod_codigobanco       gepr_pcomon_###VERSION###.tipo$cods_;
                var$aban_cod_codigoagencia     gepr_pcomon_###VERSION###.tipo$cods_;
                var$aban_cod_tipo_cuenta       gepr_pcomon_###VERSION###.tipo$cods_;
                var$aban_cod_numerocuenta      gepr_pcomon_###VERSION###.tipo$cods_;
                var$aban_cod_documento         gepr_pcomon_###VERSION###.tipo$cods_;
                var$aban_des_titularidad       gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_cod_divisa            gepr_pcomon_###VERSION###.tipo$cods_;
                var$aban_des_observaciones     gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_bol_defecto           gepr_pcomon_###VERSION###.tipo$nbols_;
                var$aban_bol_activo            gepr_pcomon_###VERSION###.tipo$nbols_;
                var$aban_des_campoadicional1   gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_des_campoadicional2   gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_des_campoadicional3   gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_des_campoadicional4   gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_des_campoadicional5   gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_des_campoadicional6   gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_des_campoadicional7   gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_des_campoadicional8   gepr_pcomon_###VERSION###.tipo$descs_;
                var$aban_des_comentario      gepr_pcomon_###VERSION###.tipo$descs_;

                var$rc_validaciones        sys_refcursor;
                var$cod_ejecucion        gepr_pcomon_###VERSION###.tipo$nel_;
                var$comentario                 gepr_pcomon_###VERSION###.tipo$desc_;
                var$oid_direccion_subcli       gepr_pcomon_###VERSION###.tipo$oid_;
                var$oid_usuario         gepr_pcomon_###VERSION###.tipo$oid_;
                var$cod_accion              gepr_pcomon_###VERSION###.tipo$cod_;
                var$bol_vigente         gepr_pcomon_###VERSION###.tipo$nbol_;
    BEGIN
        /* grabar subcliente*/
        IF  par$ascli_nel_index IS NOT NULL AND  par$ascli_nel_index.COUNT > 0 THEN
            FOR idx IN par$ascli_nel_index.first .. par$ascli_nel_index.last LOOP
                IF par$ascli_nel_index(idx) is not null THEN
                    BEGIN
                      --INICIO VALIDACIONES
                        var$cod_accion      :=  par$ascli_cod_accion(idx);
                        var$bol_vigente     := NULL;

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Por ingresar en svalidar_subcliente',
                            par$cod_identificador   => '');

                        svalidar_subcliente(
                            par$oid_llamada     => par$oid_llamada,
                            par$scli_acod_accion  =>   var$cod_accion,
                            par$scli_nel_index                 =>  par$ascli_nel_index(idx),
                            par$scli_nel_index_scli            =>  par$ascli_nel_index_scli(idx),
                            par$scli_cod_subcliente            =>  par$ascli_cod_subcliente(idx),
                            par$scli_des_descripcion           =>  par$ascli_des_descripcion(idx),
                            par$cod_cultura                     =>  par$cod_cultura);

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Posterior a svalidar_subcliente',
                            par$cod_identificador   => '');


                        IF NOT (fhay_error_configurar_cliente('CLIENTE', par$ascli_nel_index(idx)) OR fhay_error_configurar_cliente('SUBCLIENTE', par$ascli_nel_index_scli(idx) ))  THEN
                            -- Verificar si es insert o update
                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Verificar si es insert o update',
                                par$cod_identificador   => '');
                            var$oid_subcliente := NULL;
                            IF (var$cod_accion IS NULL OR var$cod_accion = 'ALTA') THEN
                                var$bol_totaliza_saldo := 0;
                                var$bol_enviado_saldos := 0;

                            ELSE
                                var$bol_totaliza_saldo := NULL;
                                var$bol_enviado_saldos := NULL;

                                SAPR_PLOG_API.SAGREGA_DETALLE(
                                    par$oid_llamada  => par$oid_llamada,
                                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                    par$des_version     => const$version,
                                    par$des_detalle     => 'Buscar oid_subcliente en la tabla auxiliar',
                                    par$cod_identificador   => '');

                                /* Buscar oid_subcliente en la tabla auxiliar */
                                BEGIN
                                    SELECT OID_CAMPO1
                                    INTO var$oid_subcliente
                                    FROM SAPR_GTT_TAUXILIAR
                                    WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'  AND
                                        COD_CAMPO2 =  par$ascli_nel_index_scli(idx) AND ROWNUM = 1;
                                EXCEPTION WHEN no_data_found THEN
                                    var$oid_subcliente := NULL;
                                END;


                            END IF;

                            /* Buscar oid_tipo_subcliente en la tabla auxiliar */
                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Buscar oid_tipo_subcliente en la tabla auxiliar',
                                par$cod_identificador   => '');
                            BEGIN
                                SELECT OID_CAMPO1
                                INTO var$oid_tipo_subcliente
                                FROM SAPR_GTT_TAUXILIAR
                                WHERE COD_CALIFICADOR = 'OID_TIPO_SUBCLIENTE'  AND
                                    COD_CAMPO2 =  par$ascli_nel_index_scli(idx) AND ROWNUM = 1;
                            EXCEPTION WHEN no_data_found THEN
                                var$oid_tipo_subcliente := NULL;
                            END;

                            /* Grabar Subcliente */
                            IF var$cod_accion = 'ALTA' OR var$cod_accion = 'MODIFICAR' THEN
                              dbms_output.put_line('var$bol_vigente = 1');
                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'var$bol_vigente = 1',
                                par$cod_identificador   => '');
                              var$bol_vigente := 1;
                              IF var$oid_subcliente IS NULL AND var$cod_accion = 'ALTA' THEN
                                var$oid_subcliente := SYS_GUID();
                              END IF;
                            ELSIF  (var$cod_accion = 'BAJA') THEN
                                var$bol_vigente := 0;
                                dbms_output.put_line('var$bol_vigente = 0');
                                SAPR_PLOG_API.SAGREGA_DETALLE(
                                  par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'var$bol_vigente = 0',
                                  par$cod_identificador   => '');
                            END IF;


                            var$aajen_cod_accion.delete;
                            var$aajen_oid_entidad.delete;
                            var$aajen_cod_entidad.delete;
                            var$aajen_nel_index_entidad.delete;
                            var$aajen_nel_index_padre.delete;
                            var$aajen_cod_identificador.delete;
                            var$aajen_cod_codigo_ajeno.delete;
                            var$aajen_cod_desc_ajeno.delete;
                            var$aajen_bol_defecto.delete;
                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Por recorrer códigos ajenos',
                                par$cod_identificador   => '');
                            IF par$aajen_nel_index_entidad IS NOT NULL AND par$aajen_nel_index_entidad.COUNT >0 THEN
                                FOR idxAJ IN par$aajen_nel_index_entidad.first .. par$aajen_nel_index_entidad.last LOOP
                                  IF idxAJ > 0 AND par$aajen_nel_index_entidad(idxAJ) IS NOT NULL THEN
                                    IF par$aajen_cod_entidad(idxAJ) = var$entidad
                                       AND par$aajen_nel_index_entidad(idxAJ) = par$ascli_nel_index_scli(idx) THEN

                                          var$aajen_cod_accion(var$aajen_cod_accion.count) :=  par$aajen_cod_accion(idxAJ);
                                          var$aajen_oid_entidad(var$aajen_oid_entidad.count) := var$oid_subcliente;
                                          var$aajen_cod_entidad(var$aajen_cod_entidad.count) :=  par$aajen_cod_entidad(idxAJ);
                                          var$aajen_nel_index_entidad(var$aajen_nel_index_entidad.count) :=  par$aajen_nel_index_entidad(idxAJ);
                                          var$aajen_nel_index_padre(var$aajen_nel_index_padre.count) := par$ascli_nel_index(idx);
                                          var$aajen_cod_identificador(var$aajen_cod_identificador.count) :=  par$aajen_cod_identificador(idxAJ);
                                          var$aajen_cod_codigo_ajeno(var$aajen_cod_codigo_ajeno.count) :=  par$aajen_cod_codigo_ajeno(idxAJ);
                                          var$aajen_cod_desc_ajeno(var$aajen_cod_desc_ajeno.count) :=  par$aajen_cod_desc_ajeno(idxAJ);
                                          var$aajen_bol_defecto(var$aajen_bol_defecto.count) :=  par$aajen_bol_defecto(idxAJ);

                                    END IF;
                                  END IF;
                                END LOOP;

                            END IF;

                            SAPR_PLOG_API.SAGREGA_DETALLE(
                              par$oid_llamada  => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     => 'Por validar si es alta y si no posee ajenos',
                              par$cod_identificador   => '');

                            IF var$cod_accion = 'ALTA'  and var$aajen_cod_accion.count = 0 THEN
                                var$aajen_cod_accion(var$aajen_cod_accion.count) :=  'ALTA';
                                var$aajen_oid_entidad(var$aajen_oid_entidad.count) := var$oid_subcliente;
                                var$aajen_cod_entidad(var$aajen_cod_entidad.count) :=  var$entidad;
                                var$aajen_nel_index_entidad(var$aajen_nel_index_entidad.count) :=  par$ascli_nel_index_scli(idx);
                                var$aajen_nel_index_padre(var$aajen_nel_index_padre.count) := par$ascli_nel_index(idx);
                                var$aajen_cod_identificador(var$aajen_cod_identificador.count) :=  'MAE';
                                var$aajen_cod_codigo_ajeno(var$aajen_cod_codigo_ajeno.count) :=   par$ascli_cod_subcliente(idx);
                                var$aajen_cod_desc_ajeno(var$aajen_cod_desc_ajeno.count) :=  par$ascli_des_descripcion(idx);
                                var$aajen_bol_defecto(var$aajen_bol_defecto.count) :=  1;
                            END IF;

                            SAPR_PLOG_API.SAGREGA_DETALLE(
                              par$oid_llamada  => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     => 'Por svalidar_codigos_ajenos',
                              par$cod_identificador   => '');

                            svalidar_codigos_ajenos(
                                        par$oid_llamada                => par$oid_llamada,
                                        par$aajen_cod_accion           => var$aajen_cod_accion,
                                        par$aajen_oid_entidad          => var$aajen_oid_entidad,
                                        par$aajen_cod_entidad          => var$aajen_cod_entidad,
                                        par$aajen_nel_index_entidad    => var$aajen_nel_index_entidad,
                                        par$aajen_nel_index_padre      => var$aajen_nel_index_padre,
                                        par$aajen_cod_identificador    => var$aajen_cod_identificador,
                                        par$aajen_cod_codigo_ajeno     => var$aajen_cod_codigo_ajeno,
                                        par$aajen_cod_desc_ajeno       => var$aajen_cod_desc_ajeno,
                                        par$aajen_bol_defecto          => var$aajen_bol_defecto,
                                        par$cod_cultura                => par$cod_cultura);

                            SAPR_PLOG_API.SAGREGA_DETALLE(
                              par$oid_llamada  => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     => 'Posterior a svalidar_codigos_ajenos',
                              par$cod_identificador   => '');


                            --Validar datos bancarios
                            --INICIO - Preparar datos bancarios de subcliente para validar
                            IF par$aban_cod_accion IS NOT NULL AND par$aban_cod_accion.COUNT > 0 THEN
                                --Datos Bancarios - Limpio las variables 
                                var$aban_cod_accion.delete;
                                var$aban_cod_entidad.delete;
                                var$aban_nel_index_entidad.delete;
                                var$aban_oid_identificador.delete;
                                var$aban_oid_cliente.delete;
                                var$aban_oid_subcliente.delete;
                                var$aban_oid_pto_servicio.delete;
                                var$aban_cod_codigobanco.delete;
                                var$aban_cod_codigoagencia.delete;
                                var$aban_cod_numerocuenta.delete;
                                var$aban_cod_tipo_cuenta.delete;
                                var$aban_des_titularidad.delete;
                                var$aban_cod_divisa.delete;

                                SAPR_PLOG_API.SAGREGA_DETALLE(
                                  par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'Por recorrer arrays asociativos de datos bancarios',
                                  par$cod_identificador   => '');

                                FOR idxDb IN par$aban_cod_accion.first .. par$aban_cod_accion.last LOOP
                                  IF idxDb > 0 AND par$aban_cod_accion(idxDb) IS NOT NULL THEN
                                    IF par$aban_cod_entidad(idxDb) = var$entidad
                                    AND par$aban_nel_index_entidad(idxDb) = par$ascli_nel_index_scli(idx) THEN
                                        --Datos Bancarios - Preparo los datos para pasar al procedure 
                                        var$aban_cod_accion(var$aban_cod_accion.count) :=  par$aban_cod_accion(idxDb);
                                        var$aban_cod_entidad(var$aban_cod_entidad.count) := par$aban_cod_entidad(idxDb);
                                        var$aban_nel_index_entidad(var$aban_nel_index_entidad.count) := par$aban_nel_index_entidad(idxDb);
                                        var$aban_oid_identificador(var$aban_oid_identificador.count) := par$aban_oid_identificador(idxDb);
                                        var$aban_oid_cliente(var$aban_oid_cliente.count) := par$ascli_oid_cliente(idx);
                                        var$aban_oid_subcliente(var$aban_oid_subcliente.count) := var$oid_subcliente;
                                        var$aban_oid_pto_servicio(var$aban_oid_pto_servicio.count) := null;
                                        var$aban_cod_codigobanco(var$aban_cod_codigobanco.count) := par$aban_cod_codigobanco(idxDb);
                                        var$aban_cod_codigoagencia(var$aban_cod_codigoagencia.count) := par$aban_cod_codigoagencia(idxDb);
                                        var$aban_cod_numerocuenta(var$aban_cod_numerocuenta.count) := par$aban_cod_numerocuenta(idxDb);
                                        var$aban_cod_tipo_cuenta(var$aban_cod_tipo_cuenta.count) := par$aban_cod_tipo_cuenta(idxDb);
                                        var$aban_des_titularidad(var$aban_des_titularidad.count) := par$aban_des_titularidad(idxDb);
                                        var$aban_cod_divisa(var$aban_cod_divisa.count) := par$aban_cod_divisa(idxDb);
                                    END IF;
                                  END IF;
                                END LOOP;

                                SAPR_PLOG_API.SAGREGA_DETALLE(
                                  par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'Por llamar a SAPR_PDATO_BANCARIO_###VERSION###.svalidar_dato_bancario',
                                  par$cod_identificador   => '');

                                SAPR_PDATO_BANCARIO_###VERSION###.svalidar_dato_bancario(
                                        par$oid_llamada                 => par$oid_llamada,
                                        par$acod_accion                 => var$aban_cod_accion,
                                        par$anel_index                  => var$aban_nel_index_entidad,
                                        par$aoid_dato_bancario          => var$aban_oid_identificador,
                                        par$acod_entidad                => var$aban_cod_entidad,
                                        par$aoid_cliente                => var$aban_oid_cliente,
                                        par$aoid_subcliente             => var$aban_oid_subcliente,
                                        par$aoid_pto_servicio           => var$aban_oid_pto_servicio,
                                        par$acod_banco                  => var$aban_cod_codigobanco,
                                        par$acod_agencia                => var$aban_cod_codigoagencia,
                                        par$acod_divisa                 => var$aban_cod_divisa,
                                        par$acod_cuenta_bancaria        => var$aban_cod_numerocuenta,
                                        par$acod_tipo_cuenta_bancaria   => var$aban_cod_tipo_cuenta,
                                        par$ades_titularidad            => var$aban_des_titularidad,
                                        par$cod_cultura                 => par$cod_cultura,
                                        par$cod_usuario                 => par$cod_usuario,
                                        par$oid_usuario                 => var$oid_usuario);


                                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                                SELECT OID_CAMPO1, COD_CAMPO2, var$entidad, par$ascli_cod_subcliente(idx), 'VALIDACIONES', par$ascli_nel_index_scli(idx), par$ascli_nel_index(idx) 
                                FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO' AND OID_CAMPO1 <> '0040080000';
                                DELETE SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO';
                                SAPR_PLOG_API.SAGREGA_DETALLE(
                                  par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'Luego de llamar a SAPR_PDATO_BANCARIO_###VERSION###.svalidar_dato_bancario',
                                  par$cod_identificador   => '');

                            END IF;


                            DBMS_OUTPUT.PUT_LINE('Hace COMMIT de VALIDACIONES de SUBCLIENTE de indice:' || par$ascli_nel_index_scli(idx));
                            COMMIT;
                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Hace COMMIT de VALIDACIONES de SUBCLIENTE de indice:' || par$ascli_nel_index_scli(idx),
                                par$cod_identificador   => '');

                           IF NOT (fhay_error_configurar_cliente('CLIENTE', par$ascli_nel_index(idx)) OR
                              fhay_error_configurar_cliente('SUBCLIENTE', par$ascli_nel_index_scli(idx) ))  THEN
                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Por ingresar en supd_subcliente - Indice :' || par$ascli_nel_index_scli(idx),
                                par$cod_identificador   => '');
                              supd_subcliente(par$oid_subcliente  => var$oid_subcliente,
                                      par$oid_cliente             => par$ascli_oid_cliente(idx),
                                      par$cod_subcliente          => par$ascli_cod_subcliente(idx),
                                      par$des_subcliente          => par$ascli_des_descripcion(idx),
                                      par$oid_tipo_subcliente     => var$oid_tipo_subcliente,
                                      par$bol_enviado_saldos      => var$bol_enviado_saldos,
                                      par$bol_vigente             => var$bol_vigente,
                                      par$bol_totaliza_saldo      => var$bol_totaliza_saldo,
                                      par$cod_usuario             => par$cod_usuario);

                              IF (var$cod_accion IS NULL OR var$cod_accion = 'ALTA') THEN

                                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                                      VALUES (
                                          var$oid_subcliente,
                                          par$ascli_nel_index_scli(idx),
                                          'OID_SUBCLIENTE');

                              END IF;
                            END IF;

                            IF NOT (fhay_error_configurar_cliente('CLIENTE', par$ascli_nel_index(idx)) OR
                              fhay_error_configurar_cliente('SUBCLIENTE', par$ascli_nel_index_scli(idx) ))  THEN
                              DBMS_OUTPUT.PUT_LINE('Graba Codigo ajeno');
                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Por Graba Codigo ajeno',
                                par$cod_identificador   => '');
                              IF var$aajen_cod_identificador IS NOT NULL AND var$aajen_cod_identificador.COUNT >0 THEN
                                    FOR idxAj IN var$aajen_cod_identificador.first .. var$aajen_cod_identificador.last LOOP
                                      IF var$aajen_cod_accion(idxAj) IS NULL OR var$aajen_cod_accion(idxAj) = 'ALTA' THEN
                                        gepr_pcodigo_ajeno_###VERSION###.sconfigurar_ajeno(
                                                par$oid_tabla_genesis      =>  var$oid_subcliente,
                                                par$cod_tabla_genesis      => 'GEPR_TSUBCLIENTE',
                                                par$cod_identificador      => var$aajen_cod_identificador(idxAj),
                                                par$cod_ajeno              => var$aajen_cod_codigo_ajeno(idxAj),
                                                par$des_ajeno              => var$aajen_cod_desc_ajeno(idxAj),
                                                par$bol_defecto            => var$aajen_bol_defecto(idxAj),
                                                par$cod_usuario            => par$cod_usuario);
                                      ELSIF var$aajen_cod_accion(idxAj) = 'BAJA' THEN
                                        gepr_pcodigo_ajeno_###VERSION###.seliminar_ajeno(
                                                par$oid_tabla_genesis  =>  var$oid_subcliente,
                                                par$cod_tabla_genesis  => 'GEPR_TSUBCLIENTE',
                                                par$cod_identificador  => var$aajen_cod_identificador(idxAj));
                                      END IF;
                                    END LOOP;
                              END IF;


                              IF par$adir_cod_entidad IS NOT NULL AND par$adir_cod_entidad.COUNT >0 THEN
                                FOR idxDir IN par$adir_cod_entidad.first .. par$adir_cod_entidad.last LOOP
                                  IF idxDir > 0 AND par$adir_nel_index_entidad(idxDir) IS NOT NULL THEN
                                    IF  par$adir_cod_entidad(idxDir) = var$entidad
                                        AND par$adir_nel_index_entidad(idxDir) = par$ascli_nel_index_scli(idx) THEN

                                            var$adir_cod_entidad(var$adir_cod_entidad.count) := par$adir_cod_entidad(idxDir);
                                            var$adir_nel_index_entidad(var$adir_nel_index_entidad.count) := par$adir_nel_index_entidad(idxDir);
                                            var$adir_des_pais(var$adir_des_pais.count) := par$adir_des_pais(idxDir);
                                            var$adir_des_prov(var$adir_des_prov.count) := par$adir_des_prov(idxDir);
                                            var$adir_des_ciudad(var$adir_des_ciudad.count) := par$adir_des_ciudad(idxDir);
                                            var$adir_des_num_tel(var$adir_des_num_tel.count) := par$adir_des_num_tel(idxDir);
                                            var$adir_des_email(var$adir_des_email.count) := par$adir_des_email(idxDir);
                                            var$adir_cod_fiscal(var$adir_cod_fiscal.count) := par$adir_cod_fiscal(idxDir);
                                            var$adir_cod_postal(var$adir_cod_postal.count) := par$adir_cod_postal(idxDir);
                                            var$adir_des_dic_1(var$adir_des_dic_1.count) := par$adir_des_dic_1(idxDir);
                                            var$adir_des_dic_2(var$adir_des_dic_2.count) := par$adir_des_dic_2(idxDir);
                                            var$adir_des_cam_adicional_1(var$adir_des_cam_adicional_1.count) := par$adir_des_cam_adicional_1(idxDir);
                                            var$adir_des_cam_adicional_2(var$adir_des_cam_adicional_2.count) := par$adir_des_cam_adicional_2(idxDir);
                                            var$adir_des_cam_adicional_3(var$adir_des_cam_adicional_3.count) := par$adir_des_cam_adicional_3(idxDir);
                                            var$adir_des_cat_adicional_1(var$adir_des_cat_adicional_1.count) := par$adir_des_cat_adicional_1(idxDir);
                                            var$adir_des_cat_adicional_2(var$adir_des_cat_adicional_2.count) := par$adir_des_cat_adicional_2(idxDir);
                                            var$adir_des_cat_adicional_3(var$adir_des_cat_adicional_3.count) := par$adir_des_cat_adicional_3(idxDir);

                                    END IF;
                                  END IF;
                                END LOOP;
                              END IF;


                              IF var$adir_cod_entidad IS NOT NULL AND var$adir_cod_entidad.COUNT >0 THEN
                                FOR idxDir IN var$adir_cod_entidad.first .. var$adir_cod_entidad.last LOOP

                                  BEGIN
                                    SELECT OID_DIRECCION
                                                INTO var$oid_direccion_subcli
                                                FROM GEPR_TDIRECCION
                                                WHERE COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE' AND OID_TABLA_GENESIS = var$oid_subcliente;
                                  EXCEPTION WHEN no_data_found THEN
                                      var$oid_direccion_subcli := NULL;
                                  END;


                                  supd_direccion(par$oid_direccion        => var$oid_direccion_subcli,
                                      par$oid_tabla_gen                       => var$oid_subcliente,
                                      par$cod_tipo_tgen                       => 'GEPR_TSUBCLIENTE',
                                      par$des_pais                            => var$adir_des_pais(idxDir),
                                      par$des_prov                            => var$adir_des_prov(idxDir),
                                      par$des_ciudad                          => var$adir_des_ciudad(idxDir),
                                      par$des_num_tel                         => var$adir_des_num_tel(idxDir),
                                      par$des_email                           => var$adir_des_email(idxDir),
                                      par$cod_fiscal                          => var$adir_cod_fiscal(idxDir),
                                      par$cod_postal                          => var$adir_cod_postal(idxDir),
                                      par$des_dic_1                           => var$adir_des_dic_1(idxDir),
                                      par$des_dic_2                           => var$adir_des_dic_2(idxDir),
                                      par$des_cam_adicional_1                 => var$adir_des_cam_adicional_1(idxDir),
                                      par$des_cam_adicional_2                 => Var$adir_des_cam_adicional_2(idxDir),
                                      par$des_cam_adicional_3                 => var$adir_des_cam_adicional_3(idxDir),
                                      par$des_cat_adicional_1                 => var$adir_des_cat_adicional_1(idxDir),
                                      par$des_cat_adicional_2                 => var$adir_des_cat_adicional_2(idxDir),
                                      par$des_cat_adicional_3                 => var$adir_des_cat_adicional_3(idxDir),
                                      par$cod_usuario                         => par$cod_usuario);
                                END LOOP;
                              END IF;

                              --INICIO - Grabar datos bancarios de subcliente
                              IF par$aban_cod_accion IS NOT NULL AND par$aban_cod_accion.COUNT > 0 THEN
                                --Datos Bancarios - Limpio las variables 
                                var$aban_cod_accion.delete;
                                var$aban_cod_entidad.delete;
                                var$aban_nel_index_entidad.delete;
                                var$aban_oid_identificador.delete;
                                var$aban_oid_cliente.delete;
                                var$aban_oid_subcliente.delete;
                                var$aban_oid_pto_servicio.delete;
                                var$aban_cod_codigobanco.delete;
                                var$aban_cod_codigoagencia.delete;
                                var$aban_cod_numerocuenta.delete;
                                var$aban_cod_tipo_cuenta.delete;
                                var$aban_cod_documento.delete;
                                var$aban_des_titularidad.delete;
                                var$aban_cod_divisa.delete;
                                var$aban_des_observaciones.delete;
                                var$aban_bol_defecto.delete;
                                var$aban_bol_activo.delete;
                                var$aban_des_campoadicional1.delete;
                                var$aban_des_campoadicional2.delete;
                                var$aban_des_campoadicional3.delete;
                                var$aban_des_campoadicional4.delete;
                                var$aban_des_campoadicional5.delete;
                                var$aban_des_campoadicional6.delete;
                                var$aban_des_campoadicional7.delete;
                                var$aban_des_campoadicional8.delete;
                                var$aban_des_comentario.delete;

                                --Buscamos el mensaje para el comentario traducido
                                --msgComentario - Registro creado por el servicio de configuración de clientes 
                                var$comentario := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgComentario',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                '',
                                0);

                                FOR idxDb IN par$aban_cod_accion.first .. par$aban_cod_accion.last LOOP
                                  IF par$aban_cod_entidad(idxDb) = var$entidad
                                    AND par$aban_nel_index_entidad(idxDb) = par$ascli_nel_index_scli(idx) THEN
                                    --Datos Bancarios - Preparo los datos para pasar al procedure 
                                    var$aban_cod_accion(var$aban_cod_accion.count) :=  par$aban_cod_accion(idxDb);
                                    var$aban_cod_entidad(var$aban_cod_entidad.count) := par$aban_cod_entidad(idxDb);
                                    var$aban_nel_index_entidad(var$aban_nel_index_entidad.count) := par$aban_nel_index_entidad(idxDb);
                                    var$aban_oid_identificador(var$aban_oid_identificador.count) := par$aban_oid_identificador(idxDb);
                                    var$aban_oid_cliente(var$aban_oid_cliente.count) := par$ascli_oid_cliente(idx);
                                    var$aban_oid_subcliente(var$aban_oid_subcliente.count) := var$oid_subcliente;
                                    var$aban_oid_pto_servicio(var$aban_oid_pto_servicio.count) := null;
                                    var$aban_cod_codigobanco(var$aban_cod_codigobanco.count) := par$aban_cod_codigobanco(idxDb);
                                    var$aban_cod_codigoagencia(var$aban_cod_codigoagencia.count) := par$aban_cod_codigoagencia(idxDb);
                                    var$aban_cod_numerocuenta(var$aban_cod_numerocuenta.count) := par$aban_cod_numerocuenta(idxDb);
                                    var$aban_cod_tipo_cuenta(var$aban_cod_tipo_cuenta.count) := par$aban_cod_tipo_cuenta(idxDb);
                                    var$aban_cod_documento(var$aban_cod_documento.count) := par$aban_cod_documento(idxDb);
                                    var$aban_des_titularidad(var$aban_des_titularidad.count) := par$aban_des_titularidad(idxDb);
                                    var$aban_cod_divisa(var$aban_cod_divisa.count) := par$aban_cod_divisa(idxDb);
                                    var$aban_des_observaciones(var$aban_des_observaciones.count) := par$aban_des_observaciones(idxDb);
                                    var$aban_bol_defecto(var$aban_bol_defecto.count) := par$aban_bol_defecto(idxDb);
                                    var$aban_des_campoadicional1(var$aban_des_campoadicional1.count) := par$aban_des_campoadicional1(idxDb);
                                    var$aban_des_campoadicional2(var$aban_des_campoadicional2.count) := par$aban_des_campoadicional2(idxDb);
                                    var$aban_des_campoadicional3(var$aban_des_campoadicional3.count) := par$aban_des_campoadicional3(idxDb);
                                    var$aban_des_campoadicional4(var$aban_des_campoadicional4.count) := par$aban_des_campoadicional4(idxDb);
                                    var$aban_des_campoadicional5(var$aban_des_campoadicional5.count) := par$aban_des_campoadicional5(idxDb);
                                    var$aban_des_campoadicional6(var$aban_des_campoadicional6.count) := par$aban_des_campoadicional6(idxDb);
                                    var$aban_des_campoadicional7(var$aban_des_campoadicional7.count) := par$aban_des_campoadicional7(idxDb);
                                    var$aban_des_campoadicional8(var$aban_des_campoadicional8.count) := par$aban_des_campoadicional8(idxDb);
                                    var$aban_des_comentario(var$aban_des_comentario.count) := var$comentario;

                                    --En caso de accion baja, bol_activo = 0, caso contrario bol_activo = 1
                                    IF par$aban_cod_accion(idxDb) = 'BAJA' THEN
                                      var$aban_bol_activo(var$aban_bol_activo.count) := 0;
                                    ELSE
                                      var$aban_bol_activo(var$aban_bol_activo.count) := 1;
                                    END IF;


                                  END IF;
                                END LOOP;


                                -- Llamo a la procedure para grabar los datos bancarios
                                SAPR_PDATO_BANCARIO_###VERSION###.sconfigurar_dato_bancario(
                                  par$oid_llamada                 => par$oid_llamada,
                                  par$anel_index                  => var$aban_nel_index_entidad,
                                  par$acod_entidad                => var$aban_cod_entidad,
                                  par$acod_accion                 => var$aban_cod_accion,
                                  par$aoid_dato_bancario          => var$aban_oid_identificador,
                                  par$aoid_cliente                => var$aban_oid_cliente,
                                  par$aoid_subcliente             => var$aban_oid_subcliente,
                                  par$aoid_pto_servicio           => var$aban_oid_pto_servicio,
                                  par$acod_banco                  => var$aban_cod_codigobanco,
                                  par$acod_agencia                => var$aban_cod_codigoagencia,
                                  par$acod_tipo_cuenta_bancaria   => var$aban_cod_tipo_cuenta,
                                  par$acod_cuenta_bancaria        => var$aban_cod_numerocuenta,
                                  par$acod_documento              => var$aban_cod_documento,
                                  par$ades_titularidad            => var$aban_des_titularidad,
                                  par$acod_divisa                 => var$aban_cod_divisa,
                                  par$ades_observaciones          => var$aban_des_observaciones,
                                  par$abol_defecto                => var$aban_bol_defecto,
                                  par$abol_activo                 => var$aban_bol_activo,
                                  par$ades_campo_adicional_1      => var$aban_des_campoadicional1,
                                  par$ades_campo_adicional_2      => var$aban_des_campoadicional2,
                                  par$ades_campo_adicional_3      => var$aban_des_campoadicional3,
                                  par$ades_campo_adicional_4      => var$aban_des_campoadicional4,
                                  par$ades_campo_adicional_5      => var$aban_des_campoadicional5,
                                  par$ades_campo_adicional_6      => var$aban_des_campoadicional6,
                                  par$ades_campo_adicional_7      => var$aban_des_campoadicional7,
                                  par$ades_campo_adicional_8      => var$aban_des_campoadicional8,
                                  par$ades_comentario             => var$aban_des_comentario,
                                  par$cod_cultura                 => par$cod_cultura,
                                  par$cod_usuario                 => par$cod_usuario,
                                  par$cod_pais                    => par$cod_pais,
                                  par$info_ejecucion              => '',
                                  par$bol_limpiar_temporal        => 0,
                                  par$rc_validaciones             => var$rc_validaciones,
                                  par$cod_ejecucion               => var$cod_ejecucion);

                              END IF;
                              --FIN - Grabar datos bancarios de subcliente

                            END IF;

                            IF NOT (fhay_error_configurar_cliente('CLIENTE', par$ascli_nel_index(idx)) OR fhay_error_configurar_cliente('SUBCLIENTE', par$ascli_nel_index_scli(idx) ))  THEN
                                DBMS_OUTPUT.PUT_LINE('Graba mensaje de Exito 0040080000 de SUBCLIENTE de Indice: ' || par$ascli_nel_index_scli(idx));
                               INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, NEL_CAMPO5, NEL_CAMPO6, COD_CALIFICADOR)
                                VALUES (
                                   '0040080000',
                                   gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040080000',
                                          gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                          NULL,
                                          0),
                                    var$entidad,
                                    par$ascli_cod_subcliente(idx),
                                    par$ascli_nel_index_scli(idx),
                                    par$ascli_nel_index(idx),
                                    'VALIDACIONES');
                                DBMS_OUTPUT.PUT_LINE('Hace COMMIT de SUBCLIENTE de indice:' || par$ascli_nel_index_scli(idx));
                                COMMIT;
                                SAPR_PLOG_API.SAGREGA_DETALLE(
                                  par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'Hace COMMIT de SUBCLIENTE de indice:' || par$ascli_nel_index_scli(idx),
                                  par$cod_identificador   => '');
                            ELSE
                                DBMS_OUTPUT.PUT_LINE('Hace ROLLBACK de SUBCLIENTE de indice:' || par$ascli_nel_index_scli(idx));
                                ROLLBACK;
                                SAPR_PLOG_API.SAGREGA_DETALLE(
                                  par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'Hace ROLLBACK de SUBCLIENTE de indice:' || par$ascli_nel_index_scli(idx),
                                  par$cod_identificador   => '');
                            END IF;
                        END IF;
                    EXCEPTION
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Hace ROLLBACK por OTHERS de SUBCLIENTE de indice:' || par$ascli_nel_index_scli(idx));
                        ROLLBACK;
                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, NEL_CAMPO5, NEL_CAMPO6, COD_CALIFICADOR)
                                        VALUES (
                                            '2040080000',
                                            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080000',
                                                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                NULL,
                                                0),
                                              var$entidad,
                                        par$ascli_cod_subcliente(idx),
                                        par$ascli_nel_index_scli(idx),
                                        par$ascli_nel_index(idx),
                                            'VALIDACIONES');

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => 'Hace ROLLBACK por OTHERS de SUBCLIENTE de indice: ' || par$ascli_nel_index_scli(idx) || const$new_line || SQLERRM,
                          par$cod_identificador   => '');

                    END;
                END IF;
            END LOOP;
        END IF;
    END sconfigurar_subcliente;


    PROCEDURE sconfigurar_ptoservicio(
            par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_,
            par$cod_identificador_ajeno     IN gepr_pcomon_###VERSION###.tipo$cod_,

            /*Punto Servicio*/
            par$apser_cod_accion            IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$apser_nel_index             IN gepr_pcomon_###VERSION###.tipo$nels_,
            par$apser_nel_index_scli        IN gepr_pcomon_###VERSION###.tipo$nels_,
            par$apser_nel_index_pto         IN gepr_pcomon_###VERSION###.tipo$nels_,
            par$apser_cod_pto_servicio      IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$apser_des_pto_servicio      IN gepr_pcomon_###VERSION###.tipo$descs_,

            /*Datos Bancarios*/
            par$aban_cod_accion             IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aban_cod_entidad            IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aban_nel_index_entidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
            par$aban_oid_identificador      IN gepr_pcomon_###VERSION###.tipo$oids_,
            par$aban_cod_codigobanco        IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aban_cod_codigoagencia      IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aban_cod_numerocuenta       IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aban_cod_tipo_cuenta        IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aban_cod_documento          IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aban_des_titularidad        IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_cod_divisa             IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aban_des_observaciones      IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_bol_defecto            IN gepr_pcomon_###VERSION###.tipo$nbols_,
            par$aban_des_campoadicional1    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_des_campoadicional2    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_des_campoadicional3    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_des_campoadicional4    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_des_campoadicional5    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_des_campoadicional6    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_des_campoadicional7    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aban_des_campoadicional8    IN gepr_pcomon_###VERSION###.tipo$descs_,

            /*Codigo Ajeno*/
            par$aajen_cod_accion            IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aajen_cod_entidad           IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aajen_nel_index_entidad     IN gepr_pcomon_###VERSION###.tipo$nels_,
            par$aajen_cod_identificador     IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aajen_cod_codigo_ajeno      IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$aajen_cod_desc_ajeno        IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$aajen_bol_defecto           IN gepr_pcomon_###VERSION###.tipo$nbols_,

            /*Direcciones*/
            par$adir_cod_entidad            IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$adir_nel_index_entidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
            par$adir_des_pais               IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_prov               IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_ciudad             IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_num_tel            IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_email              IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_cod_fiscal             IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$adir_cod_postal             IN gepr_pcomon_###VERSION###.tipo$cods_,
            par$adir_des_dic_1              IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_dic_2              IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_cam_adicional_1    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_cam_adicional_2    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_cam_adicional_3    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_cat_adicional_1    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_cat_adicional_2    IN gepr_pcomon_###VERSION###.tipo$descs_,
            par$adir_des_cat_adicional_3    IN gepr_pcomon_###VERSION###.tipo$descs_,

            par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
            par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_,
            par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_) IS
      
        const$nombre_func CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := 'sconfigurar_ptoservicio';
        var$oid_pto_servicio         gepr_pcomon_###VERSION###.tipo$oid_;
        var$oid_subcliente           gepr_pcomon_###VERSION###.tipo$oid_;
        var$oid_cliente            gepr_pcomon_###VERSION###.tipo$oid_;
        var$oid_tipo_punto_servicio  gepr_pcomon_###VERSION###.tipo$oid_;
        var$bol_totaliza_saldo       gepr_pcomon_###VERSION###.tipo$nbol_;
        var$entidad                 gepr_pcomon_###VERSION###.tipo$desc_:= 'PUNTOSERVICIO';

        /*Codigo Ajeno*/
        var$aajen_cod_accion           gepr_pcomon_###VERSION###.tipo$cods_;
        var$aajen_oid_entidad          gepr_pcomon_###VERSION###.tipo$oids_;
        var$aajen_cod_entidad          gepr_pcomon_###VERSION###.tipo$cods_;
        var$aajen_nel_index_entidad    gepr_pcomon_###VERSION###.tipo$nels_;
        var$aajen_nel_index_padre      gepr_pcomon_###VERSION###.tipo$nels_;
        var$aajen_cod_identificador    gepr_pcomon_###VERSION###.tipo$cods_;
        var$aajen_cod_codigo_ajeno     gepr_pcomon_###VERSION###.tipo$cods_;
        var$aajen_cod_desc_ajeno       gepr_pcomon_###VERSION###.tipo$descs_;
        var$aajen_bol_defecto          gepr_pcomon_###VERSION###.tipo$nbols_;

        /*Direcciones*/
        var$adir_cod_entidad            gepr_pcomon_###VERSION###.tipo$cods_;
        var$adir_nel_index_entidad      gepr_pcomon_###VERSION###.tipo$nels_;
        var$adir_des_pais               gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_prov               gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_ciudad             gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_num_tel            gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_email               gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_cod_fiscal              gepr_pcomon_###VERSION###.tipo$cods_;
        var$adir_cod_postal              gepr_pcomon_###VERSION###.tipo$cods_;
        var$adir_des_dic_1               gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_dic_2               gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_cam_adicional_1     gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_cam_adicional_2     gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_cam_adicional_3     gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_cat_adicional_1     gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_cat_adicional_2     gepr_pcomon_###VERSION###.tipo$descs_;
        var$adir_des_cat_adicional_3     gepr_pcomon_###VERSION###.tipo$descs_;

        /*Datos Bancarios*/
        var$aban_cod_accion            gepr_pcomon_###VERSION###.tipo$cods_;
        var$aban_cod_entidad           gepr_pcomon_###VERSION###.tipo$cods_;
        var$aban_nel_index_entidad     gepr_pcomon_###VERSION###.tipo$nels_;
        var$aban_oid_identificador     gepr_pcomon_###VERSION###.tipo$oids_;
        var$aban_oid_cliente       gepr_pcomon_###VERSION###.tipo$oids_;
        var$aban_oid_subcliente      gepr_pcomon_###VERSION###.tipo$oids_;
        var$aban_oid_pto_servicio      gepr_pcomon_###VERSION###.tipo$oids_;
        var$aban_cod_codigobanco       gepr_pcomon_###VERSION###.tipo$cods_;
        var$aban_cod_codigoagencia     gepr_pcomon_###VERSION###.tipo$cods_;
        var$aban_cod_numerocuenta      gepr_pcomon_###VERSION###.tipo$cods_;
        var$aban_cod_tipo_cuenta       gepr_pcomon_###VERSION###.tipo$cods_;
        var$aban_cod_documento         gepr_pcomon_###VERSION###.tipo$cods_;
        var$aban_des_titularidad       gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_cod_divisa            gepr_pcomon_###VERSION###.tipo$cods_;
        var$aban_des_observaciones     gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_bol_defecto           gepr_pcomon_###VERSION###.tipo$nbols_;
        var$aban_bol_activo            gepr_pcomon_###VERSION###.tipo$nbols_;
        var$aban_des_campoadicional1   gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_des_campoadicional2   gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_des_campoadicional3   gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_des_campoadicional4   gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_des_campoadicional5   gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_des_campoadicional6   gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_des_campoadicional7   gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_des_campoadicional8   gepr_pcomon_###VERSION###.tipo$descs_;
        var$aban_des_comentario      gepr_pcomon_###VERSION###.tipo$descs_;

        var$rc_validaciones           sys_refcursor;
        var$cod_ejecucion             gepr_pcomon_###VERSION###.tipo$nel_;
        var$comentario                gepr_pcomon_###VERSION###.tipo$desc_;
        var$oid_direccion_pto         gepr_pcomon_###VERSION###.tipo$oid_;
        var$oid_usuario               gepr_pcomon_###VERSION###.tipo$oid_;
        var$cod_accion                gepr_pcomon_###VERSION###.tipo$cod_;
        var$bol_vigente               gepr_pcomon_###VERSION###.tipo$nbol_;
    BEGIN
      IF par$apser_nel_index IS NOT NULL AND par$apser_nel_index.count > 0 THEN
        FOR idx IN par$apser_nel_index.first .. par$apser_nel_index.last LOOP
            BEGIN
                --INICIO VALIDACIONES
                var$cod_accion      :=  par$apser_cod_accion(idx);
                var$bol_vigente     :=  NULL;
                
                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'INICIO VALIDACION_PUNTO_SERVICIO',
                    par$cod_identificador   => '');

                svalidar_punto_servicio(par$oid_llamada => par$oid_llamada,
                            par$pser_acod_accion       => var$cod_accion,
                            par$pser_nel_index         => par$apser_nel_index(idx),
                            par$pser_nel_index_scli    => par$apser_nel_index_scli(idx),
                            par$pser_nel_index_pto     => par$apser_nel_index_pto(idx),
                            par$pser_cod_pto_servicio  => par$apser_cod_pto_servicio(idx),
                            par$pser_des_pto_servicio  => par$apser_des_pto_servicio(idx),
                            par$cod_cultura             => par$cod_cultura);

                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'FIN VALIDACION_PUNTO_SERVICIO',
                    par$cod_identificador   => '');
                var$oid_pto_servicio := NULL;
                IF NOT (fhay_error_configurar_cliente('CLIENTE', par$apser_nel_index(idx)) OR fhay_error_configurar_cliente('SUBCLIENTE', par$apser_nel_index_scli(idx) ) OR fhay_error_configurar_cliente('PUNTOSERVICIO', par$apser_nel_index_pto(idx)))  THEN
                    --ALTA
                    IF var$cod_accion = 'ALTA' THEN
                        var$bol_totaliza_saldo := 0;
                        
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'var$cod_accion = ALTA - var$bol_totaliza_saldo = 0',
                            par$cod_identificador   => '');
                    ELSE--MODIFICACION

                        var$bol_totaliza_saldo := NULL;

                        BEGIN
                            SELECT OID_CAMPO1
                            INTO var$oid_pto_servicio
                            FROM SAPR_GTT_TAUXILIAR
                            WHERE COD_CALIFICADOR = 'OID_PTO_SERVICIO'  AND
                                COD_CAMPO2 =  par$apser_nel_index_pto(idx) AND ROWNUM = 1;
                        EXCEPTION WHEN no_data_found THEN
                            var$oid_pto_servicio := NULL;
                        END;
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'var$cod_accion = MODIFICACION',
                            par$cod_identificador   => '');
                    END IF;

                    SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Por obtener OID_TIPO_PUNTO_SERVICIO',
                            par$cod_identificador   => '');
                    BEGIN
                        SELECT OID_CAMPO1
                        INTO var$oid_tipo_punto_servicio
                        FROM SAPR_GTT_TAUXILIAR
                        WHERE COD_CALIFICADOR = 'OID_TIPO_PUNTO_SERVICIO'  AND
                            COD_CAMPO2 =  par$apser_nel_index_pto(idx) AND ROWNUM = 1;
                        EXCEPTION WHEN no_data_found THEN
                        var$oid_tipo_punto_servicio := NULL;
                    END;

                    SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Por obtener OID_SUBCLIENTE - par$apser_nel_index_scli(' || idx ||'): ' || par$apser_nel_index_scli(idx) ,
                            par$cod_identificador   => '');
                    BEGIN
                        SELECT OID_CAMPO1
                        INTO var$oid_subcliente
                        FROM SAPR_GTT_TAUXILIAR
                        WHERE COD_CALIFICADOR = 'OID_SUBCLIENTE'  AND
                            COD_CAMPO2 =  par$apser_nel_index_scli(idx) AND ROWNUM = 1;
                        EXCEPTION WHEN no_data_found THEN
                        var$oid_subcliente := NULL;
                    END;

                    SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Por obtener OID_CLIENTE - par$apser_nel_index(' || idx ||'): ' || par$apser_nel_index(idx) ,
                            par$cod_identificador   => '');
                    BEGIN
                        SELECT OID_CAMPO1
                        INTO var$oid_cliente
                        FROM SAPR_GTT_TAUXILIAR
                        WHERE COD_CALIFICADOR = 'OID_CLIENTE'  AND
                            COD_CAMPO2 =  par$apser_nel_index(idx) AND ROWNUM = 1;
                        EXCEPTION WHEN no_data_found THEN
                        var$oid_cliente := NULL;
                    END;


                    IF var$cod_accion = 'ALTA' OR var$cod_accion = 'MODIFICAR' THEN
                        dbms_output.put_line('var$bol_vigente = 1');
                        var$bol_vigente := 1;

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'var$bol_vigente = 1 var$cod_accion es ALTA o MODIFICAR (su valor es ' || var$cod_accion || ')',
                            par$cod_identificador   => '');

                      IF var$oid_pto_servicio IS NULL AND var$cod_accion = 'ALTA' THEN
                          var$oid_pto_servicio := SYS_GUID();
                          SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Calculo un OID_PTO_SERVICIO',
                            par$cod_identificador   => '');
                      END IF;
                    ELSIF  (var$cod_accion = 'BAJA') THEN
                        var$bol_vigente := 0;
                        dbms_output.put_line('var$bol_vigente = 0');
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'var$bol_vigente = 0 por BAJA',
                            par$cod_identificador   => '');
                    END IF;


                    var$aajen_cod_accion.delete;
                    var$aajen_oid_entidad.delete;
                    var$aajen_cod_entidad.delete;
                    var$aajen_nel_index_entidad.delete;
                    var$aajen_nel_index_padre.delete;
                    var$aajen_cod_identificador.delete;
                    var$aajen_cod_codigo_ajeno.delete;
                    var$aajen_cod_desc_ajeno.delete;
                    var$aajen_bol_defecto.delete;
                    SAPR_PLOG_API.SAGREGA_DETALLE(
                        par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => 'Por recorrrer coleccion de ajenos',
                        par$cod_identificador   => '');
                    IF par$aajen_nel_index_entidad IS NOT NULL AND par$aajen_nel_index_entidad.COUNT >0 THEN
                      FOR idxAJ IN par$aajen_nel_index_entidad.first .. par$aajen_nel_index_entidad.last LOOP
                        IF idxAJ > 0 AND par$aajen_nel_index_entidad(idxAJ) IS NOT NULL THEN
                          IF par$aajen_cod_entidad(idxAJ) = var$entidad
                            AND par$aajen_nel_index_entidad(idxAJ) = par$apser_nel_index_pto(idx) THEN

                                var$aajen_cod_accion(var$aajen_cod_accion.count) :=  par$aajen_cod_accion(idxAJ);
                                var$aajen_oid_entidad(var$aajen_oid_entidad.count) := var$oid_pto_servicio;
                                var$aajen_cod_entidad(var$aajen_cod_entidad.count) :=  par$aajen_cod_entidad(idxAJ);
                                var$aajen_nel_index_entidad(var$aajen_nel_index_entidad.count) :=  par$aajen_nel_index_entidad(idxAJ);
                                var$aajen_nel_index_padre(var$aajen_nel_index_padre.count) := par$apser_nel_index_scli(idx);
                                var$aajen_cod_identificador(var$aajen_cod_identificador.count) :=  par$aajen_cod_identificador(idxAJ);
                                var$aajen_cod_codigo_ajeno(var$aajen_cod_codigo_ajeno.count) :=  par$aajen_cod_codigo_ajeno(idxAJ);
                                var$aajen_cod_desc_ajeno(var$aajen_cod_desc_ajeno.count) :=  par$aajen_cod_desc_ajeno(idxAJ);
                                var$aajen_bol_defecto(var$aajen_bol_defecto.count) :=  par$aajen_bol_defecto(idxAJ);

                          END IF;
                        END IF;
                      END LOOP;
                    END IF;

                    IF var$cod_accion = 'ALTA'  and var$aajen_cod_accion.count = 0 THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Es un alta y no tiene codigos ajenos',
                            par$cod_identificador   => '');
                        var$aajen_cod_accion(var$aajen_cod_accion.count) :=  'ALTA';
                        var$aajen_oid_entidad(var$aajen_oid_entidad.count) := var$oid_pto_servicio;
                        var$aajen_cod_entidad(var$aajen_cod_entidad.count) :=  var$entidad;
                        var$aajen_nel_index_entidad(var$aajen_nel_index_entidad.count) :=  par$apser_nel_index_pto(idx);
                        var$aajen_nel_index_padre(var$aajen_nel_index_padre.count) := par$apser_nel_index_scli(idx);
                        var$aajen_cod_identificador(var$aajen_cod_identificador.count) :=  'MAE';
                        var$aajen_cod_codigo_ajeno(var$aajen_cod_codigo_ajeno.count) :=   par$apser_cod_pto_servicio(idx);
                        var$aajen_cod_desc_ajeno(var$aajen_cod_desc_ajeno.count) :=  par$apser_des_pto_servicio(idx);
                        var$aajen_bol_defecto(var$aajen_bol_defecto.count) :=  1;
                    END IF;

                    svalidar_codigos_ajenos(
                                    par$oid_llamada                => par$oid_llamada,
                                    par$aajen_cod_accion           => var$aajen_cod_accion,
                                    par$aajen_oid_entidad          => var$aajen_oid_entidad,
                                    par$aajen_cod_entidad          => var$aajen_cod_entidad,
                                    par$aajen_nel_index_entidad    => var$aajen_nel_index_entidad,
                                    par$aajen_nel_index_padre      => var$aajen_nel_index_padre,
                                    par$aajen_cod_identificador    => var$aajen_cod_identificador,
                                    par$aajen_cod_codigo_ajeno     => var$aajen_cod_codigo_ajeno,
                                    par$aajen_cod_desc_ajeno       => var$aajen_cod_desc_ajeno,
                                    par$aajen_bol_defecto          => var$aajen_bol_defecto,
                                    par$cod_cultura                => par$cod_cultura);


                    /*Validar datos bancarios*/
                    /*INICIO - Preparar datos bancarios de punto de servicio para validar*/
                    SAPR_PLOG_API.SAGREGA_DETALLE(
                        par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => 'INICIO - Preparar datos bancarios de punto de servicio para validar',
                        par$cod_identificador   => '');
                    IF par$aban_cod_accion IS NOT NULL AND par$aban_cod_accion.COUNT > 0 THEN
                        /*Datos Bancarios - Limpio las variables */
                        var$aban_cod_accion.delete;
                        var$aban_cod_entidad.delete;
                        var$aban_nel_index_entidad.delete;
                        var$aban_oid_identificador.delete;
                        var$aban_oid_cliente.delete;
                        var$aban_oid_subcliente.delete;
                        var$aban_oid_pto_servicio.delete;
                        var$aban_cod_codigobanco.delete;
                        var$aban_cod_codigoagencia.delete;
                        var$aban_cod_numerocuenta.delete;
                        var$aban_cod_tipo_cuenta.delete;
                        var$aban_des_titularidad.delete;
                        var$aban_cod_divisa.delete;

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Por recorrer array asociativo',
                            par$cod_identificador   => '');

                        FOR idxDb IN par$aban_cod_accion.first .. par$aban_cod_accion.last LOOP
                          IF idxDb > 0 AND par$aban_cod_accion(idxDb) IS NOT NULL THEN
                            IF par$aban_cod_entidad(idxDb) = var$entidad
                            AND par$aban_nel_index_entidad(idxDb) = par$apser_nel_index_pto(idx) THEN
                                /*Datos Bancarios - Preparo los datos para pasar al procedure */
                                var$aban_cod_accion(var$aban_cod_accion.count) :=  par$aban_cod_accion(idxDb);
                                var$aban_cod_entidad(var$aban_cod_entidad.count) := par$aban_cod_entidad(idxDb);
                                var$aban_nel_index_entidad(var$aban_nel_index_entidad.count) := par$aban_nel_index_entidad(idxDb);
                                var$aban_oid_identificador(var$aban_oid_identificador.count) := par$aban_oid_identificador(idxDb);
                                var$aban_oid_cliente(var$aban_oid_cliente.count) := var$oid_cliente;
                                var$aban_oid_subcliente(var$aban_oid_subcliente.count) := var$oid_subcliente;
                                var$aban_oid_pto_servicio(var$aban_oid_pto_servicio.count) := var$oid_pto_servicio;
                                var$aban_cod_codigobanco(var$aban_cod_codigobanco.count) := par$aban_cod_codigobanco(idxDb);
                                var$aban_cod_codigoagencia(var$aban_cod_codigoagencia.count) := par$aban_cod_codigoagencia(idxDb);
                                var$aban_cod_numerocuenta(var$aban_cod_numerocuenta.count) := par$aban_cod_numerocuenta(idxDb);
                                var$aban_cod_tipo_cuenta(var$aban_cod_tipo_cuenta.count) := par$aban_cod_tipo_cuenta(idxDb);
                                var$aban_des_titularidad(var$aban_des_titularidad.count) := par$aban_des_titularidad(idxDb);
                                var$aban_cod_divisa(var$aban_cod_divisa.count) := par$aban_cod_divisa(idxDb);
                            END IF;
                          END IF;
                        END LOOP;

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Por entrar en SAPR_PDATO_BANCARIO_###VERSION###.svalidar_dato_bancario',
                            par$cod_identificador   => '');

                        SAPR_PDATO_BANCARIO_###VERSION###.svalidar_dato_bancario(
                                par$oid_llamada                 => par$oid_llamada,
                                par$acod_accion                 => var$aban_cod_accion,
                                par$anel_index                  => var$aban_nel_index_entidad,
                                par$aoid_dato_bancario          => var$aban_oid_identificador,
                                par$acod_entidad                => var$aban_cod_entidad,
                                par$aoid_cliente                => var$aban_oid_cliente,
                                par$aoid_subcliente             => var$aban_oid_subcliente,
                                par$aoid_pto_servicio           => var$aban_oid_pto_servicio,
                                par$acod_banco                  => var$aban_cod_codigobanco,
                                par$acod_agencia                => var$aban_cod_codigoagencia,
                                par$acod_divisa                 => var$aban_cod_divisa,
                                par$acod_cuenta_bancaria        => var$aban_cod_numerocuenta,
                                par$acod_tipo_cuenta_bancaria   => var$aban_cod_tipo_cuenta,
                                par$ades_titularidad            => var$aban_des_titularidad,
                                par$cod_cultura                 => par$cod_cultura,
                                par$cod_usuario                 => par$cod_usuario,
                                par$oid_usuario                 => var$oid_usuario);

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Fin de SAPR_PDATO_BANCARIO_###VERSION###.svalidar_dato_bancario',
                            par$cod_identificador   => '');


                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                        SELECT OID_CAMPO1, COD_CAMPO2, var$entidad, par$apser_cod_pto_servicio(idx), 'VALIDACIONES', par$apser_nel_index_pto(idx), par$apser_nel_index_scli(idx) 
                        FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO' AND OID_CAMPO1 <> '0040080000';
                        DELETE SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO';

                    END IF;
                    COMMIT;

                    IF NOT (fhay_error_configurar_cliente('CLIENTE', par$apser_nel_index(idx)) OR fhay_error_configurar_cliente('SUBCLIENTE', par$apser_nel_index_scli(idx) ) OR fhay_error_configurar_cliente('PUNTOSERVICIO', par$apser_nel_index_pto(idx)))  THEN

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Por ingresar en supd_pto_servicio 1/3 ' || const$new_line
                            || 'par$oid_pto_servicio: ' || var$oid_pto_servicio || const$new_line
                            || 'par$oid_subcliente: ' || var$oid_subcliente || const$new_line
                            || 'par$cod_pto_servicio('|| idx ||'): ' || par$apser_cod_pto_servicio(idx) || const$new_line
                            || 'par$des_pto_servicio('|| idx ||'): ' || par$apser_des_pto_servicio(idx) || const$new_line
                            || 'par$oid_tipo_pto_servicio: ' || var$oid_tipo_punto_servicio,
                            par$cod_identificador   => '');
                        IF var$bol_vigente = 1 THEN
                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Por ingresar en supd_pto_servicio 2/3' || const$new_line
                                || 'par$bol_vigente = 1',
                                par$cod_identificador   => '');
                        ELSE
                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Por ingresar en supd_pto_servicio 2/3' || const$new_line
                                || 'par$bol_vigente != 1',
                                par$cod_identificador   => '');
                        END IF;

                        IF var$bol_totaliza_saldo = 1 THEN
                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Por ingresar en supd_pto_servicio 3/3' || const$new_line
                                || 'par$bol_totaliza_saldo = 1',
                                par$cod_identificador   => '');
                        ELSE
                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Por ingresar en supd_pto_servicio 3/3' || const$new_line
                                || 'par$bol_totaliza_saldo != 1',
                                par$cod_identificador   => '');
                        END IF;
                        
                        supd_pto_servicio(par$oid_pto_servicio     => var$oid_pto_servicio,
                            par$oid_subcliente                => var$oid_subcliente,
                            par$cod_pto_servicio              => par$apser_cod_pto_servicio(idx),
                            par$des_pto_servicio              => par$apser_des_pto_servicio(idx),
                            par$oid_tipo_pto_servicio         => var$oid_tipo_punto_servicio,
                            par$bol_vigente                   => var$bol_vigente,
                            par$bol_totaliza_saldo            => var$bol_totaliza_saldo,
                            par$cod_usuario                   => par$cod_usuario);

                      IF var$cod_accion = 'ALTA' THEN
                          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                              VALUES (
                                  var$oid_pto_servicio,
                                  par$apser_nel_index_pto(idx),
                                  'OID_PUNTO_SERVICIO');

                      END IF;
                    END IF;

                    IF NOT (fhay_error_configurar_cliente('CLIENTE', par$apser_nel_index(idx)) OR fhay_error_configurar_cliente('SUBCLIENTE', par$apser_nel_index_scli(idx) ) OR fhay_error_configurar_cliente('PUNTOSERVICIO', par$apser_nel_index_pto(idx)))  THEN
                        IF var$aajen_cod_identificador IS NOT NULL AND var$aajen_cod_identificador.COUNT >0 THEN
                          FOR idxAj IN var$aajen_cod_identificador.first .. var$aajen_cod_identificador.last LOOP
                              IF var$aajen_cod_accion(idxAj) IS NULL OR var$aajen_cod_accion(idxAj) = 'ALTA' THEN
                                gepr_pcodigo_ajeno_###VERSION###.sconfigurar_ajeno(
                                        par$oid_tabla_genesis      => var$oid_pto_servicio,
                                        par$cod_tabla_genesis      => 'GEPR_TPUNTO_SERVICIO',
                                        par$cod_identificador      => var$aajen_cod_identificador(idxAj),
                                        par$cod_ajeno              => var$aajen_cod_codigo_ajeno(idxAj),
                                        par$des_ajeno              => var$aajen_cod_desc_ajeno(idxAj),
                                        par$bol_defecto            => var$aajen_bol_defecto(idxAj),
                                        par$cod_usuario            => par$cod_usuario);
                              ELSIF var$aajen_cod_accion(idxAj) = 'BAJA' THEN
                                gepr_pcodigo_ajeno_###VERSION###.seliminar_ajeno(
                                        par$oid_tabla_genesis  =>  var$oid_pto_servicio,
                                        par$cod_tabla_genesis  => 'GEPR_TPUNTO_SERVICIO',
                                        par$cod_identificador  => var$aajen_cod_identificador(idxAj));
                              END IF;
                          END LOOP;
                        END IF;
                        IF par$adir_cod_entidad IS NOT NULL AND par$adir_cod_entidad.COUNT >0 THEN
                            FOR idxDir IN par$adir_cod_entidad.first .. par$adir_cod_entidad.last LOOP
                              IF idxDir > 0 AND par$adir_nel_index_entidad(idxDir) IS NOT NULL THEN
                                IF  par$adir_cod_entidad(idxDir) = var$entidad
                                    AND par$adir_nel_index_entidad(idxDir) = par$apser_nel_index_pto(idx) THEN

                                        var$adir_cod_entidad(var$adir_cod_entidad.count) := par$adir_cod_entidad(idxDir);
                                        var$adir_nel_index_entidad(var$adir_nel_index_entidad.count) := par$adir_nel_index_entidad(idxDir);
                                        var$adir_des_pais(var$adir_des_pais.count) := par$adir_des_pais(idxDir);
                                        var$adir_des_prov(var$adir_des_prov.count) := par$adir_des_prov(idxDir);
                                        var$adir_des_ciudad(var$adir_des_ciudad.count) := par$adir_des_ciudad(idxDir);
                                        var$adir_des_num_tel(var$adir_des_num_tel.count) := par$adir_des_num_tel(idxDir);
                                        var$adir_des_email(var$adir_des_email.count) := par$adir_des_email(idxDir);
                                        var$adir_cod_fiscal(var$adir_cod_fiscal.count) := par$adir_cod_fiscal(idxDir);
                                        var$adir_cod_postal(var$adir_cod_postal.count) := par$adir_cod_postal(idxDir);
                                        var$adir_des_dic_1(var$adir_des_dic_1.count) := par$adir_des_dic_1(idxDir);
                                        var$adir_des_dic_2(var$adir_des_dic_2.count) := par$adir_des_dic_2(idxDir);
                                        var$adir_des_cam_adicional_1(var$adir_des_cam_adicional_1.count) := par$adir_des_cam_adicional_1(idxDir);
                                        var$adir_des_cam_adicional_2(var$adir_des_cam_adicional_2.count) := par$adir_des_cam_adicional_2(idxDir);
                                        var$adir_des_cam_adicional_3(var$adir_des_cam_adicional_3.count) := par$adir_des_cam_adicional_3(idxDir);
                                        var$adir_des_cat_adicional_1(var$adir_des_cat_adicional_1.count) := par$adir_des_cat_adicional_1(idxDir);
                                        var$adir_des_cat_adicional_2(var$adir_des_cat_adicional_2.count) := par$adir_des_cat_adicional_2(idxDir);
                                        var$adir_des_cat_adicional_3(var$adir_des_cat_adicional_3.count) := par$adir_des_cat_adicional_3(idxDir);
                                END IF;
                              END IF;
                            END LOOP;
                        END IF;
                        IF var$adir_cod_entidad IS NOT NULL AND var$adir_cod_entidad.COUNT >0 THEN
                            FOR idxDir IN var$adir_cod_entidad.first .. var$adir_cod_entidad.last LOOP

                                BEGIN
                                    SELECT OID_DIRECCION
                                        INTO var$oid_direccion_pto
                                        FROM GEPR_TDIRECCION
                                        WHERE COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' AND OID_TABLA_GENESIS = var$oid_pto_servicio;
                                EXCEPTION WHEN no_data_found THEN
                                    var$oid_direccion_pto := NULL;
                                END;

                                supd_direccion(par$oid_direccion        => var$oid_direccion_pto,
                                    par$oid_tabla_gen                       => var$oid_pto_servicio,
                                    par$cod_tipo_tgen                       => 'GEPR_TPUNTO_SERVICIO',
                                    par$des_pais                            => var$adir_des_pais(idxDir),
                                    par$des_prov                            => var$adir_des_prov(idxDir),
                                    par$des_ciudad                          => var$adir_des_ciudad(idxDir),
                                    par$des_num_tel                         => var$adir_des_num_tel(idxDir),
                                    par$des_email                           => var$adir_des_email(idxDir),
                                    par$cod_fiscal                          => var$adir_cod_fiscal(idxDir),
                                    par$cod_postal                          => var$adir_cod_postal(idxDir),
                                    par$des_dic_1                           => var$adir_des_dic_1(idxDir),
                                    par$des_dic_2                           => var$adir_des_dic_2(idxDir),
                                    par$des_cam_adicional_1                 => var$adir_des_cam_adicional_1(idxDir),
                                    par$des_cam_adicional_2                 => Var$adir_des_cam_adicional_2(idxDir),
                                    par$des_cam_adicional_3                 => var$adir_des_cam_adicional_3(idxDir),
                                    par$des_cat_adicional_1                 => var$adir_des_cat_adicional_1(idxDir),
                                    par$des_cat_adicional_2                 => var$adir_des_cat_adicional_2(idxDir),
                                    par$des_cat_adicional_3                 => var$adir_des_cat_adicional_3(idxDir),
                                    par$cod_usuario                         => par$cod_usuario);
                            END LOOP;
                          END IF;


                        /*INICIO - Grabar datos bancarios de punto de servicio*/
                        IF par$aban_cod_accion IS NOT NULL AND par$aban_cod_accion.COUNT > 0 THEN
                          /*Datos Bancarios - Limpio las variables */
                          var$aban_cod_accion.delete;
                          var$aban_cod_entidad.delete;
                          var$aban_nel_index_entidad.delete;
                          var$aban_oid_identificador.delete;
                          var$aban_oid_cliente.delete;
                          var$aban_oid_subcliente.delete;
                          var$aban_oid_pto_servicio.delete;
                          var$aban_cod_codigobanco.delete;
                          var$aban_cod_codigoagencia.delete;
                          var$aban_cod_numerocuenta.delete;
                          var$aban_cod_tipo_cuenta.delete;
                          var$aban_cod_documento.delete;
                          var$aban_des_titularidad.delete;
                          var$aban_cod_divisa.delete;
                          var$aban_des_observaciones.delete;
                          var$aban_bol_defecto.delete;
                          var$aban_bol_activo.delete;
                          var$aban_des_campoadicional1.delete;
                          var$aban_des_campoadicional2.delete;
                          var$aban_des_campoadicional3.delete;
                          var$aban_des_campoadicional4.delete;
                          var$aban_des_campoadicional5.delete;
                          var$aban_des_campoadicional6.delete;
                          var$aban_des_campoadicional7.delete;
                          var$aban_des_campoadicional8.delete;
                          var$aban_des_comentario.delete;

                          /*Buscamos el mensaje para el comentario traducido*/
                          /*msgComentario - Registro creado por el servicio de configuración de clientes */
                          var$comentario := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'msgComentario',
                          gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                          '',
                          0);

                          SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Por recorrer arrays asociativos de datos bancarios',
                                par$cod_identificador   => '');
                          FOR idxDb IN par$aban_cod_accion.first .. par$aban_cod_accion.last LOOP
                            IF par$aban_cod_entidad(idxDb) = var$entidad
                              AND par$aban_nel_index_entidad(idxDb) = par$apser_nel_index_pto(idx) THEN
                              /*Datos Bancarios - Preparo los datos para pasar al procedure */
                              var$aban_cod_accion(var$aban_cod_accion.count) :=  par$aban_cod_accion(idxDb);
                              var$aban_cod_entidad(var$aban_cod_entidad.count) := par$aban_cod_entidad(idxDb);
                              var$aban_nel_index_entidad(var$aban_nel_index_entidad.count) := par$aban_nel_index_entidad(idxDb);
                              var$aban_oid_identificador(var$aban_oid_identificador.count) := par$aban_oid_identificador(idxDb);
                              var$aban_oid_cliente(var$aban_oid_cliente.count) := var$oid_cliente;
                              var$aban_oid_subcliente(var$aban_oid_subcliente.count) := var$oid_subcliente;
                              var$aban_oid_pto_servicio(var$aban_oid_pto_servicio.count) := var$oid_pto_servicio;
                              var$aban_cod_codigobanco(var$aban_cod_codigobanco.count) := par$aban_cod_codigobanco(idxDb);
                              var$aban_cod_codigoagencia(var$aban_cod_codigoagencia.count) := par$aban_cod_codigoagencia(idxDb);
                              var$aban_cod_numerocuenta(var$aban_cod_numerocuenta.count) := par$aban_cod_numerocuenta(idxDb);
                              var$aban_cod_tipo_cuenta(var$aban_cod_tipo_cuenta.count) := par$aban_cod_tipo_cuenta(idxDb);
                              var$aban_cod_documento(var$aban_cod_documento.count) := par$aban_cod_documento(idxDb);
                              var$aban_des_titularidad(var$aban_des_titularidad.count) := par$aban_des_titularidad(idxDb);
                              var$aban_cod_divisa(var$aban_cod_divisa.count) := par$aban_cod_divisa(idxDb);
                              var$aban_des_observaciones(var$aban_des_observaciones.count) := par$aban_des_observaciones(idxDb);
                              var$aban_bol_defecto(var$aban_bol_defecto.count) := par$aban_bol_defecto(idxDb);
                              var$aban_des_campoadicional1(var$aban_des_campoadicional1.count) := par$aban_des_campoadicional1(idxDb);
                              var$aban_des_campoadicional2(var$aban_des_campoadicional2.count) := par$aban_des_campoadicional2(idxDb);
                              var$aban_des_campoadicional3(var$aban_des_campoadicional3.count) := par$aban_des_campoadicional3(idxDb);
                              var$aban_des_campoadicional4(var$aban_des_campoadicional4.count) := par$aban_des_campoadicional4(idxDb);
                              var$aban_des_campoadicional5(var$aban_des_campoadicional5.count) := par$aban_des_campoadicional5(idxDb);
                              var$aban_des_campoadicional6(var$aban_des_campoadicional6.count) := par$aban_des_campoadicional6(idxDb);
                              var$aban_des_campoadicional7(var$aban_des_campoadicional7.count) := par$aban_des_campoadicional7(idxDb);
                              var$aban_des_campoadicional8(var$aban_des_campoadicional8.count) := par$aban_des_campoadicional8(idxDb);
                                          var$aban_des_comentario(var$aban_des_comentario.count) := var$comentario;

                              /*En caso de accion baja, bol_activo = 0, caso contrario bol_activo = 1*/
                              IF par$aban_cod_accion(idxDb) = 'BAJA' THEN
                                var$aban_bol_activo(var$aban_bol_activo.count) := 0;
                              ELSE
                                var$aban_bol_activo(var$aban_bol_activo.count) := 1;
                              END IF;
                            END IF;
                          END LOOP;

                          SAPR_PLOG_API.SAGREGA_DETALLE(
                              par$oid_llamada  => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     => 'Por invocar a SAPR_PDATO_BANCARIO_###VERSION###.sconfigurar_dato_bancario',
                              par$cod_identificador   => '');

                          /* Llamo a la procedure para grabar los datos bancarios*/
                          SAPR_PDATO_BANCARIO_###VERSION###.sconfigurar_dato_bancario(
                            par$oid_llamada                 => par$oid_llamada,
                            par$anel_index                  => var$aban_nel_index_entidad,
                            par$acod_entidad                => var$aban_cod_entidad,
                            par$acod_accion                 => var$aban_cod_accion,
                            par$aoid_dato_bancario          => var$aban_oid_identificador,
                            par$aoid_cliente                => var$aban_oid_cliente,
                            par$aoid_subcliente             => var$aban_oid_subcliente,
                            par$aoid_pto_servicio           => var$aban_oid_pto_servicio,
                            par$acod_banco                  => var$aban_cod_codigobanco,
                            par$acod_agencia                => var$aban_cod_codigoagencia,
                            par$acod_tipo_cuenta_bancaria   => var$aban_cod_tipo_cuenta,
                            par$acod_cuenta_bancaria        => var$aban_cod_numerocuenta,
                            par$acod_documento              => var$aban_cod_documento,
                            par$ades_titularidad            => var$aban_des_titularidad,
                            par$acod_divisa                 => var$aban_cod_divisa,
                            par$ades_observaciones          => var$aban_des_observaciones,
                            par$abol_defecto                => var$aban_bol_defecto,
                            par$abol_activo                 => var$aban_bol_activo,
                            par$ades_campo_adicional_1      => var$aban_des_campoadicional1,
                            par$ades_campo_adicional_2      => var$aban_des_campoadicional2,
                            par$ades_campo_adicional_3      => var$aban_des_campoadicional3,
                            par$ades_campo_adicional_4      => var$aban_des_campoadicional4,
                            par$ades_campo_adicional_5      => var$aban_des_campoadicional5,
                            par$ades_campo_adicional_6      => var$aban_des_campoadicional6,
                            par$ades_campo_adicional_7      => var$aban_des_campoadicional7,
                            par$ades_campo_adicional_8      => var$aban_des_campoadicional8,
                            par$ades_comentario             => var$aban_des_comentario,
                            par$cod_cultura                 => par$cod_cultura,
                            par$cod_usuario                 => par$cod_usuario,
                            par$cod_pais                    => par$cod_pais,
                            par$info_ejecucion              => '',
                            par$bol_limpiar_temporal        => 0,
                            par$rc_validaciones             => var$rc_validaciones,
                            par$cod_ejecucion               => var$cod_ejecucion );

                        END IF;
                        /*FIN - Grabar datos bancarios de punto de servicio*/
                    END IF;
                    
                    SAPR_PLOG_API.SAGREGA_DETALLE(
                        par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => 'Previo a realizar commit del indice :' || par$apser_nel_index_pto(idx),
                        par$cod_identificador   => '');
                    IF NOT (fhay_error_configurar_cliente('CLIENTE', par$apser_nel_index(idx)) OR fhay_error_configurar_cliente('SUBCLIENTE', par$apser_nel_index_scli(idx) ) OR fhay_error_configurar_cliente('PUNTOSERVICIO', par$apser_nel_index_pto(idx)))  THEN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, NEL_CAMPO5, NEL_CAMPO6, COD_CALIFICADOR)
                                VALUES (
                                  '0040080000',
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040080000',
                                          gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                          NULL,
                                          0),
                                    var$entidad,
                                    par$apser_cod_pto_servicio(idx),
                                    par$apser_nel_index_pto(idx),
                                    par$apser_nel_index_scli(idx),
                                    'VALIDACIONES');
                        DBMS_OUTPUT.PUT_LINE('Hace COMMIT de PUNTOSERVICIO de indice:' || par$apser_nel_index_pto(idx));
                        COMMIT;
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Hace COMMIT de PUNTOSERVICIO de indice:' || par$apser_nel_index_pto(idx),
                            par$cod_identificador   => '');
                    ELSE
                        DBMS_OUTPUT.PUT_LINE('Hace ROLLBACK de PUNTOSERVICIO de indice:' || par$apser_nel_index_pto(idx));
                        ROLLBACK;
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Hace ROLLBACK de PUNTOSERVICIO de indice:' || par$apser_nel_index_pto(idx),
                            par$cod_identificador   => '');
                    END IF;
                END IF;

            EXCEPTION
                WHEN OTHERS THEN
                DBMS_OUTPUT.PUT_LINE('Hace ROLLBACK por OTHERS de PUNTOSERVICIO de indice:' || par$apser_nel_index_pto(idx));
                ROLLBACK;
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4, NEL_CAMPO5, NEL_CAMPO6, COD_CALIFICADOR)
                        VALUES (
                           '2040080000',
                            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080000',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                NULL,
                                0),
                            var$entidad,
                            par$apser_cod_pto_servicio(idx),
                            par$apser_nel_index_pto(idx),
                            par$apser_nel_index_scli(idx),
                            'VALIDACIONES');

                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Hace ROLLBACK por OTHERS de PUNTOSERVICIO de indice: ' || par$apser_nel_index_pto(idx) || const$new_line || SQLERRM,
                    par$cod_identificador   => '');

            END;
        END LOOP;
      END IF;
    END sconfigurar_ptoservicio;

  /* Validar informacion del cliente */
  PROCEDURE svalidar_cliente(  par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_accion               IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                               par$nel_index                IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$cod_cliente              IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$des_descripcion          IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$cod_tipo_cliente         IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    const$nombre_func CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_cliente';
    var$mensaje                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$mensajeEntidadTraducida     gepr_pcomon_###VERSION###.tipo$desc_;
    var$mensajeCampo                gepr_pcomon_###VERSION###.tipo$desc_;
    var$descripcionExistente        gepr_pcomon_###VERSION###.tipo$desc_;
    var$oidClienteExistente         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oidTipoClienteExistente     gepr_pcomon_###VERSION###.tipo$oid_;
    var$entidad                     gepr_pcomon_###VERSION###.tipo$desc_:= 'CLIENTE';

    var$existe        gepr_pcomon_###VERSION###.tipo$nel_ ;

    BEGIN

      /*GRABAR PARAMETROS DE ENTRADA*/
      IF par$oid_llamada IS NOT NULL THEN
        /*COD_ACCION*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_accion: ' || par$cod_accion,
            par$cod_identificador   => '');

        /*NEL_INDEX*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$nel_index: ' || par$nel_index,
            par$cod_identificador   => '');

        /*COD_CLIENTE*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_cliente: ' || par$cod_cliente,
            par$cod_identificador   => '');

        /*des_descripcion*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$des_descripcion: ' || par$des_descripcion,
            par$cod_identificador   => '');

        /*cod_tipo_cliente*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_tipo_cliente: ' || par$cod_tipo_cliente,
            par$cod_identificador   => '');

        /*cod_cultura*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
            par$cod_identificador   => '');

      END IF;
      

      SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Por traducir la palabra CLIENTE',
          par$cod_identificador   => '');

      /*Traducir palabra CLIENTE*/
      var$mensajeEntidadTraducida := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,var$entidad,
                                   gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                   gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                   NULL,
                                   0);

      SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Por validar par$cod_cliente',
          par$cod_identificador   => '');

      /*Validamos que se encuentre el codigo del cliente*/
      IF par$cod_cliente IS NULL THEN
        /*Traducir palabra Codigo*/
        var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Codigo',
                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                            NULL,
                            0);

        /*Buscamos el mensaje traducido*/
        /*2040080004 - Es obligatorio informar el {0} del {1}.*/
        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
        var$mensajeCampo  || '|' || var$mensajeEntidadTraducida,
        0);

        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
        VALUES (
            '2040080004',
            var$mensaje,
            var$entidad,
            par$cod_cliente,
            'VALIDACIONES',
            par$nel_index,
            NULL);

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => '2040080004 - ' || var$mensaje,
          par$cod_identificador   => '');
      ELSE
        IF  par$cod_accion IS NULL OR par$cod_accion = 'ALTA' THEN

            SELECT COUNT(1) INTO var$existe FROM GEPR_TCLIENTE
            WHERE COD_CLIENTE = par$cod_cliente;

            IF var$existe = 0 THEN
                par$cod_accion := 'ALTA';
            ELSE
                par$cod_accion := 'MODIFICAR';
            END IF;

            SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'var$existe: ' || var$existe || ' - par$cod_accion: ' || par$cod_accion,
                par$cod_identificador   => '');
        END IF;

        -- validar tipo accion
        /*Validamos en caso de ALTA que se encuentre el tipo de cliente, descripcion y que el codigo no exista*/
        IF par$cod_accion = 'ALTA'  THEN

            SAPR_PLOG_API.SAGREGA_DETALLE(
              par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'Validando alta',
              par$cod_identificador   => '');
            /*Tipo de cliente*/
            IF par$cod_tipo_cliente IS NULL THEN
                /*Traducir palabra Tipo de cliente*/
                var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'TipoCliente',
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0);

                /*Buscamos el mensaje traducido*/
                /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                var$mensajeCampo  || '|' || var$mensajeEntidadTraducida,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080004',
                    var$mensaje,
                    var$entidad,
                    par$cod_cliente,
                    'VALIDACIONES',
                    par$nel_index,
                    NULL);
                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => '2040080004 - ' || var$mensaje,
                  par$cod_identificador   => '');
            ELSE
                /*Si existe el codigo, guardamos el OID del Tipo de cliente*/
                BEGIN
                    SELECT OID_TIPO_CLIENTE
                    INTO var$oidTipoClienteExistente
                    FROM GEPR_TTIPO_CLIENTE
                    WHERE COD_TIPO_CLIENTE = par$cod_tipo_cliente AND ROWNUM = 1;
                EXCEPTION WHEN no_data_found THEN
                    var$oidTipoClienteExistente := NULL;
                END;
                IF var$oidTipoClienteExistente IS NOT NULL THEN
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                    VALUES (
                        var$oidTipoClienteExistente,
                        par$nel_index,
                        'OID_TIPO_CLIENTE');
                ELSE
                    /*Traducir palabra Tipo de cliente*/
                    var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'TipoCliente',
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0);

                    /*Buscamos el mensaje traducido*/
                    /*2040080005 - El {0} de codigo "{1}" no existe.*/
                    var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    var$mensajeCampo || '|' || par$cod_tipo_cliente,
                    0);

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                    VALUES (
                            '2040080005',
                            var$mensaje,
                            var$entidad,
                            par$cod_cliente,
                            'VALIDACIONES',
                            par$nel_index,
                            NULL);
                    SAPR_PLOG_API.SAGREGA_DETALLE(
                        par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => '2040080005 - ' || var$mensaje,
                        par$cod_identificador   => '');  
                END IF;
            END IF;
            /*Descripcion*/
            IF par$des_descripcion IS NULL THEN
                /*Traducir palabra Descripcion*/
                var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Descripcion',
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0);

                /*Buscamos el mensaje traducido*/
                /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                var$mensajeCampo  || '|' || var$mensajeEntidadTraducida,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080004',
                    var$mensaje,
                    var$entidad,
                    par$cod_cliente,
                    'VALIDACIONES',
                    par$nel_index,
                    NULL);
                SAPR_PLOG_API.SAGREGA_DETALLE(
                        par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => '2040080004 - ' || var$mensaje,
                        par$cod_identificador   => '');  
            END IF;

            /*Si existe el codigo, guardamos la descripcion del cliente*/
            BEGIN
                SELECT DES_CLIENTE
                INTO var$descripcionExistente
                FROM GEPR_TCLIENTE
                WHERE COD_CLIENTE = par$cod_cliente AND ROWNUM = 1;
            EXCEPTION WHEN no_data_found THEN
                var$descripcionExistente := NULL;
            END;

            IF var$descripcionExistente IS NOT NULL THEN

                /*Buscamos el mensaje traducido*/
                /*2040080003 - El {0} de código "{1}" ({2}) ya existe.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080003',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                var$mensajeEntidadTraducida || '|' || par$cod_cliente || '|' || var$descripcionExistente,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080003',
                    var$mensaje,
                    var$entidad,
                    par$cod_cliente,
                    'VALIDACIONES',
                    par$nel_index,
                    NULL);
                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => '2040080003 - ' || var$mensaje,
                    par$cod_identificador   => ''); 
            END IF;
        /*Validamos en caso de MODIFICAR o BAJA que el codigo exista*/
        ELSE
            /*Codigo de cliente*/
            /*Si existe el codigo, guardamos el OID del cliente*/
            BEGIN
                SELECT OID_CLIENTE
                INTO var$oidClienteExistente
                FROM GEPR_TCLIENTE
                WHERE COD_CLIENTE = par$cod_cliente AND ROWNUM = 1;
            EXCEPTION WHEN no_data_found THEN
                var$oidClienteExistente := NULL;
            END;

            IF var$oidClienteExistente IS NOT NULL THEN
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                VALUES (
                    var$oidClienteExistente,
                    par$nel_index,
                    'OID_CLIENTE');
            ELSE
                /*Buscamos el mensaje traducido*/
                /*2040080005 - El {0} de codigo "{1}" no existe.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                var$mensajeEntidadTraducida || '|' || par$cod_cliente,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080005',
                    var$mensaje,
                    var$entidad,
                    par$cod_cliente,
                    'VALIDACIONES',
                    par$nel_index,
                    NULL);
                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => '2040080005 - ' || var$mensaje,
                    par$cod_identificador   => ''); 
            END IF;
            /*Tipo de cliente*/
            /*Opcional para la MODIFICACION*/
            IF par$cod_tipo_cliente IS NOT NULL AND  par$cod_accion = 'MODIFICAR' THEN
                /*Si existe el codigo, guardamos el OID del Tipo de cliente*/
                BEGIN
                    SELECT OID_TIPO_CLIENTE
                    INTO var$oidTipoClienteExistente
                    FROM GEPR_TTIPO_CLIENTE
                    WHERE COD_TIPO_CLIENTE = par$cod_tipo_cliente AND ROWNUM = 1;
                EXCEPTION WHEN no_data_found THEN
                    var$oidTipoClienteExistente := NULL;
                END;

                IF var$oidTipoClienteExistente IS NOT NULL THEN
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                    VALUES (
                        var$oidTipoClienteExistente,
                        par$nel_index,
                        'OID_TIPO_CLIENTE');
                ELSE
                    /*Traducir palabra Tipo de cliente*/
                    var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'TipoCliente',
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0);

                    /*Buscamos el mensaje traducido*/
                    /*2040080005 - El {0} de codigo "{1}" no existe.*/
                    var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    var$mensajeCampo || '|' || par$cod_tipo_cliente,
                    0);

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                    VALUES (
                        '2040080005',
                        var$mensaje,
                        var$entidad,
                        par$cod_cliente,
                        'VALIDACIONES',
                        par$nel_index,
                        NULL);
                    SAPR_PLOG_API.SAGREGA_DETALLE(
                        par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => '2040080005 - ' || var$mensaje,
                        par$cod_identificador   => ''); 
                END IF;
            END IF;
        END IF;
      END IF;
  END svalidar_cliente;

  /* Validar informacion del subcliente */
  PROCEDURE svalidar_subcliente(par$oid_llamada           IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$scli_acod_accion       IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                               par$scli_nel_index           IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$scli_nel_index_scli      IN gepr_pcomon_###VERSION###.tipo$nel_,
                               par$scli_cod_subcliente      IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$scli_des_descripcion     IN gepr_pcomon_###VERSION###.tipo$desc_,
                               par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    const$nombre_func CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_subcliente';
    var$mensaje                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$mensajeEntidadTraducida     gepr_pcomon_###VERSION###.tipo$desc_;
    var$mensajeCampo                gepr_pcomon_###VERSION###.tipo$desc_;
    var$descripcionExistente        gepr_pcomon_###VERSION###.tipo$desc_;
    var$oidExistente                gepr_pcomon_###VERSION###.tipo$oid_;
    var$oidTipoSubclienteExistente  gepr_pcomon_###VERSION###.tipo$oid_;
    var$entidad                     gepr_pcomon_###VERSION###.tipo$desc_:= 'SUBCLIENTE';
    var$existe                      gepr_pcomon_###VERSION###.tipo$nel_ ;

    BEGIN

    /*Traducir palabra SUBCLIENTE*/
    var$mensajeEntidadTraducida := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,var$entidad,
                                   gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                   gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                   NULL,
                                   0);



            /*Validamos que se encuentre el codigo del Subcliente*/
            IF par$scli_cod_subcliente IS NULL THEN
                 /*Traducir palabra C¿digo*/
                var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Codigo',
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0);

                /*Buscamos el mensaje traducido*/
                /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                var$mensajeCampo  || '|' || var$mensajeEntidadTraducida,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080004',
                    var$mensaje,
                    var$entidad,
                    par$scli_cod_subcliente,
                    'VALIDACIONES',
                    par$scli_nel_index_scli,
                    par$scli_nel_index);
            ELSE
                IF  par$scli_acod_accion IS NULL OR par$scli_acod_accion = 'ALTA' THEN

                    SELECT COUNT(1) INTO var$existe 
                    FROM GEPR_TSUBCLIENTE SCLI
                    INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'OID_CLIENTE' AND aux.OID_CAMPO1 = SCLI.OID_CLIENTE AND par$scli_nel_index = aux.COD_CAMPO2
                    WHERE COD_SUBCLIENTE = par$scli_cod_subcliente;

                    IF var$existe = 0 THEN
                        par$scli_acod_accion := 'ALTA';
                    ELSE
                        par$scli_acod_accion := 'MODIFICAR';
                    END IF;
                END IF;


                /*Validamos en caso de ALTA que se encuentre la descripcion y que el codigo no exista*/
                IF par$scli_acod_accion IS NULL OR par$scli_acod_accion = 'ALTA'  THEN
                    /*Descripcion*/
                    IF par$scli_des_descripcion IS NULL THEN
                        /*Traducir palabra Descripcion*/
                        var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Descripcion',
                                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                            NULL,
                                            0);

                        /*Buscamos el mensaje traducido*/
                        /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        var$mensajeCampo  || '|' || var$mensajeEntidadTraducida,
                        0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                        VALUES (
                            '2040080004',
                            var$mensaje,
                            var$entidad,
                            par$scli_cod_subcliente,
                            'VALIDACIONES',
                            par$scli_nel_index_scli,
                            par$scli_nel_index);
                    END IF;

                    /*Si existe el codigo, guardamos la descripcion del subcliente*/
                    BEGIN
                        SELECT SCLI.DES_SUBCLIENTE
                        INTO var$descripcionExistente
                        FROM GEPR_TSUBCLIENTE SCLI
                        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'OID_CLIENTE' AND aux.OID_CAMPO1 = SCLI.OID_CLIENTE AND par$scli_nel_index = aux.COD_CAMPO2
                        WHERE SCLI.COD_SUBCLIENTE = par$scli_cod_subcliente AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                        var$descripcionExistente := NULL;
                    END;

                    IF var$descripcionExistente IS NOT NULL THEN
                        /*Buscamos el mensaje traducido*/
                        /*2040080003 - El {0} de c¿digo "{1}" ({2}) ya existe.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080003',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        var$mensajeEntidadTraducida || '|' || par$scli_cod_subcliente || '|' || var$descripcionExistente,
                        0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                        VALUES (
                            '2040080003',
                            var$mensaje,
                            var$entidad,
                            par$scli_cod_subcliente,
                            'VALIDACIONES',
                            par$scli_nel_index_scli,
                            par$scli_nel_index);
                    END IF;

                    /*Cargamos el OID_TIPO_SUBCLIENTE DEFAULT*/
                    BEGIN
                        SELECT OID_TIPO_SUBCLIENTE
                        INTO var$oidTipoSubclienteExistente
                        FROM GEPR_TTIPO_SUBCLIENTE
                        WHERE COD_TIPO_SUBCLIENTE = '0' AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                        var$oidTipoSubclienteExistente := NULL;
                    END;

                    IF var$oidTipoSubclienteExistente IS NOT NULL THEN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                        VALUES (
                            var$oidTipoSubclienteExistente,
                            par$scli_nel_index_scli,
                            'OID_TIPO_SUBCLIENTE');
                    ELSE
                        /*Buscamos el mensaje traducido*/
                        /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        'OID_TIPO_SUBCLIENTE' || '|' || var$mensajeEntidadTraducida,
                        0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                        VALUES (
                            '2040080004',
                            var$mensaje,
                            var$entidad,
                            par$scli_cod_subcliente,
                            'VALIDACIONES',
                            par$scli_nel_index_scli,
                            par$scli_nel_index);
                    END IF;

                /*Validamos en caso de MODIFICAR o BAJA que el codigo exista*/
                ELSE
                    /*Si existe el codigo, guardamos el OID del subcliente y del tipo de subcliente*/
                    BEGIN
                        SELECT OID_SUBCLIENTE, OID_TIPO_SUBCLIENTE
                        INTO var$oidExistente, var$oidTipoSubclienteExistente
                        FROM GEPR_TSUBCLIENTE SCLI
                        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'OID_CLIENTE' AND aux.OID_CAMPO1 = SCLI.OID_CLIENTE AND par$scli_nel_index = aux.COD_CAMPO2
                        WHERE COD_SUBCLIENTE = par$scli_cod_subcliente AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                        var$oidExistente := NULL;
                        var$oidTipoSubclienteExistente := NULL;
                    END;

                    /*OID_SUBCLIENTE*/
                    IF var$oidExistente IS NOT NULL THEN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                        VALUES (
                            var$oidExistente,
                            par$scli_nel_index_scli,
                            'OID_SUBCLIENTE');

                        /*OID_TIPO_SUBCLIENTE*/
                        IF var$oidTipoSubclienteExistente IS NOT NULL THEN
                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                            VALUES (
                                var$oidTipoSubclienteExistente,
                                par$scli_nel_index_scli,
                                'OID_TIPO_SUBCLIENTE');
                        ELSE
                            /*Buscamos el mensaje traducido*/
                            /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                            'OID_TIPO_SUBCLIENTE' || '|' || var$mensajeEntidadTraducida,
                            0);

                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                            VALUES (
                                '2040080004',
                                var$mensaje,
                                var$entidad,
                                par$scli_cod_subcliente,
                                'VALIDACIONES',
                                par$scli_nel_index_scli,
                                par$scli_nel_index);
                        END IF;
                    ELSE
                        /*Buscamos el mensaje traducido*/
                        /*2040080005 - El {0} de codigo "{1}" no existe.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        var$mensajeEntidadTraducida || '|' || par$scli_cod_subcliente,
                        0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                        VALUES (
                            '2040080005',
                            var$mensaje,
                            var$entidad,
                            par$scli_cod_subcliente,
                            'VALIDACIONES',
                            par$scli_nel_index_scli,
                            par$scli_nel_index);
                    END IF;
                END IF;
            END IF;
  END svalidar_subcliente;

  /* Validar informacion del punto de servicio */
  PROCEDURE svalidar_punto_servicio(
                                      par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$pser_acod_accion         IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$pser_nel_index           IN gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$pser_nel_index_scli      IN gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$pser_nel_index_pto       IN gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$pser_cod_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$pser_des_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$desc_,
                                      par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_) IS
      const$nombre_func CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_punto_servicio';
      var$mensaje                     gepr_pcomon_###VERSION###.tipo$desc_;
      var$mensajeEntidadTraducida     gepr_pcomon_###VERSION###.tipo$desc_;
      var$mensajeCampo                gepr_pcomon_###VERSION###.tipo$desc_;
      var$descripcionExistente        gepr_pcomon_###VERSION###.tipo$desc_;
      var$oidExistente                gepr_pcomon_###VERSION###.tipo$oid_;
      var$oidTipoPuntoExistente       gepr_pcomon_###VERSION###.tipo$oid_;
      var$entidad                     gepr_pcomon_###VERSION###.tipo$desc_:= 'PUNTOSERVICIO';
      var$existe                      gepr_pcomon_###VERSION###.tipo$nel_ ;

  BEGIN
    /*Limpiar tabla temporal*/
    -- DELETE SAPR_GTT_TAUXILIAR
    -- WHERE  COD_CAMPO3 =  var$entidad AND COD_CALIFICADOR = 'VALIDACIONES';

    /*Iniciar log de parametros iniciales*/
    IF par$oid_llamada is not null THEN
        /*pser_acod_accion*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$pser_acod_accion - ' || par$pser_acod_accion,
            par$cod_identificador   => '');

        /*pser_nel_index*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$pser_nel_index - ' || par$pser_nel_index,
            par$cod_identificador   => '');

        /*pser_nel_index_scli*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$pser_nel_index_scli - ' || par$pser_nel_index_scli,
            par$cod_identificador   => '');

        /*pser_nel_index_pto*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$pser_nel_index_pto - ' || par$pser_nel_index_pto,
            par$cod_identificador   => '');

        /*pser_cod_pto_servicio*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$pser_cod_pto_servicio - ' || par$pser_cod_pto_servicio,
            par$cod_identificador   => '');

        /*pser_des_pto_servicio*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$pser_des_pto_servicio - ' || par$pser_des_pto_servicio,
            par$cod_identificador   => '');

        /*cod_cultura*/
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'par$cod_cultura - ' || par$cod_cultura,
            par$cod_identificador   => '');

    END IF;

    /*Traducir palabra SUBCLIENTE*/
    var$mensajeEntidadTraducida := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,var$entidad,
                                   gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                   gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                   NULL,
                                   0);



    /*Validamos que se encuentre el codigo del Punto de servicio*/
    IF par$pser_cod_pto_servicio IS NULL THEN
          /*Traducir palabra Codigo*/
        var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Codigo',
                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                            NULL,
                            0);

        /*Buscamos el mensaje traducido*/
        /*2040080004 - Es obligatorio informar el {0} del {1}.*/
        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
        var$mensajeCampo  || '|' || var$mensajeEntidadTraducida,
        0);

        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
        VALUES (
            '2040080004',
            var$mensaje,
            var$entidad,
            par$pser_cod_pto_servicio,
            'VALIDACIONES',
            par$pser_nel_index_pto,
            par$pser_nel_index_scli);
        
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => '2040080004 - ' || var$mensaje,
            par$cod_identificador   => '');
    ELSE
        IF par$pser_acod_accion IS NULL OR par$pser_acod_accion = 'ALTA' THEN
                SELECT COUNT(1) INTO var$existe 
                FROM GEPR_TPUNTO_SERVICIO pto
                INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'OID_SUBCLIENTE' AND aux.OID_CAMPO1 = pto.OID_SUBCLIENTE AND par$pser_nel_index_scli = aux.COD_CAMPO2
                WHERE COD_PTO_SERVICIO = par$pser_cod_pto_servicio;

                IF var$existe = 0 THEN
                    par$pser_acod_accion := 'ALTA';
                ELSE
                    par$pser_acod_accion := 'MODIFICAR';
                END IF;
        END IF;

        /*Validamos en caso de ALTA que se encuentre la descripcion y que el codigo no exista*/
        IF par$pser_acod_accion IS NULL OR par$pser_acod_accion = 'ALTA'  THEN
            /*Descripcion*/
            IF par$pser_des_pto_servicio IS NULL THEN
                /*Traducir palabra Descripcion*/
                var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Descripcion',
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0);

                /*Buscamos el mensaje traducido*/
                /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                var$mensajeCampo  || '|' || var$mensajeEntidadTraducida,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080004',
                    var$mensaje,
                    var$entidad,
                    par$pser_cod_pto_servicio,
                    'VALIDACIONES',
                    par$pser_nel_index_pto,
                    par$pser_nel_index_scli);
                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => '2040080004 - ' || var$mensaje,
                    par$cod_identificador   => '');
            END IF;

            /*Si existe el codigo, guardamos la descripcion del punto de servicio*/
            BEGIN
                SELECT DES_PTO_SERVICIO
                INTO var$descripcionExistente
                FROM GEPR_TPUNTO_SERVICIO pto
                INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'OID_SUBCLIENTE' AND aux.OID_CAMPO1 = pto.OID_SUBCLIENTE AND par$pser_nel_index_scli = aux.COD_CAMPO2
                WHERE COD_PTO_SERVICIO = par$pser_cod_pto_servicio AND ROWNUM = 1;
            EXCEPTION WHEN no_data_found THEN
                var$descripcionExistente := NULL;
            END;

            IF var$descripcionExistente IS NOT NULL THEN
                /*Buscamos el mensaje traducido*/
                /*2040080003 - El {0} de codigo "{1}" ({2}) ya existe.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080003',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                var$mensajeEntidadTraducida || '|' || par$pser_cod_pto_servicio || '|' || var$descripcionExistente,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080003',
                    var$mensaje,
                    var$entidad,
                    par$pser_cod_pto_servicio,
                    'VALIDACIONES',
                    par$pser_nel_index_pto,
                    par$pser_nel_index_scli);

                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => '2040080003 - ' || var$mensaje,
                    par$cod_identificador   => '');
            END IF;

            /*Cargamos el OID_TIPO_PUNTO_SERVICIO DEFAULT*/
            BEGIN
                SELECT OID_TIPO_PUNTO_SERVICIO
                INTO var$oidTipoPuntoExistente
                FROM GEPR_TTIPO_PUNTO_SERVICIO
                WHERE COD_TIPO_PUNTO_SERVICIO in ('0','3') AND ROWNUM = 1
                order by COD_TIPO_PUNTO_SERVICIO;
            EXCEPTION WHEN no_data_found THEN
                var$oidTipoPuntoExistente := NULL;
            END;

            IF var$oidTipoPuntoExistente IS NOT NULL THEN
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                VALUES (
                    var$oidTipoPuntoExistente,
                    par$pser_nel_index_pto,
                    'OID_TIPO_PUNTO_SERVICIO');
            ELSE
                /*Buscamos el mensaje traducido*/
                /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                'OID_TIPO_PUNTO_SERVICIO' || '|' || var$mensajeEntidadTraducida,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080004',
                    var$mensaje,
                    var$entidad,
                    par$pser_cod_pto_servicio,
                    'VALIDACIONES',
                    par$pser_nel_index_pto,
                    par$pser_nel_index_scli);
                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => '2040080004 - ' || var$mensaje,
                    par$cod_identificador   => '');
            END IF;
        /*Validamos en caso de MODIFICAR o BAJA que el codigo exista*/
        ELSE
            /*Si existe el codigo, guardamos el OID del punto de servicio y del tipo de punto de servicio*/
            BEGIN
                SELECT OID_PTO_SERVICIO, OID_TIPO_PUNTO_SERVICIO
                INTO var$oidExistente, var$oidTipoPuntoExistente
                FROM GEPR_TPUNTO_SERVICIO  pto
                INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.COD_CALIFICADOR = 'OID_SUBCLIENTE' AND aux.OID_CAMPO1 = pto.OID_SUBCLIENTE AND par$pser_nel_index_scli = aux.COD_CAMPO2
                WHERE COD_PTO_SERVICIO = par$pser_cod_pto_servicio AND ROWNUM = 1;
            EXCEPTION WHEN no_data_found THEN
                var$oidExistente := NULL;
                var$oidTipoPuntoExistente := NULL;
            END;
            /*OID_PTO_SERVICIO*/
            IF var$oidExistente IS NOT NULL THEN
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                VALUES (
                    var$oidExistente,
                    par$pser_nel_index_pto,
                    'OID_PTO_SERVICIO');

                    /*OID_TIPO_PUNTO_SERVICIO*/
                    IF var$oidTipoPuntoExistente IS NOT NULL THEN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                        VALUES (
                            var$oidTipoPuntoExistente,
                            par$pser_nel_index_pto,
                            'OID_TIPO_PUNTO_SERVICIO');
                    ELSE
                        /*Buscamos el mensaje traducido*/
                        /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        'OID_TIPO_PUNTO_SERVICIO' || '|' || var$mensajeEntidadTraducida,
                        0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                        VALUES (
                            '2040080004',
                            var$mensaje,
                            var$entidad,
                            par$pser_cod_pto_servicio,
                            'VALIDACIONES',
                            par$pser_nel_index_pto,
                            par$pser_nel_index_scli);
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => '2040080004 - ' || var$mensaje,
                            par$cod_identificador   => '');
                    END IF;
            ELSE
                  /*Buscamos el mensaje traducido*/
                /*2040080005 - El {0} de codigo "{1}" no existe.*/
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                var$mensajeEntidadTraducida || '|' || par$pser_cod_pto_servicio,
                0);

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080005',
                    var$mensaje,
                    var$entidad,
                    par$pser_cod_pto_servicio,
                    'VALIDACIONES',
                    par$pser_nel_index_pto,
                    par$pser_nel_index_scli);
                SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => '2040080005 - ' || var$mensaje,
                    par$cod_identificador   => '');
            END IF;
        END IF;
    END IF;

  END svalidar_punto_servicio;

  PROCEDURE svalidar_codigos_ajenos(
      par$oid_llamada                IN gepr_pcomon_###VERSION###.tipo$oid_,
      par$aajen_cod_accion           IN gepr_pcomon_###VERSION###.tipo$cods_,
      par$aajen_oid_entidad          IN gepr_pcomon_###VERSION###.tipo$oids_,
      par$aajen_cod_entidad          IN gepr_pcomon_###VERSION###.tipo$cods_,
      par$aajen_nel_index_entidad    IN gepr_pcomon_###VERSION###.tipo$nels_,
      par$aajen_nel_index_padre      IN gepr_pcomon_###VERSION###.tipo$nels_,
      par$aajen_cod_identificador    IN gepr_pcomon_###VERSION###.tipo$cods_,
      par$aajen_cod_codigo_ajeno     IN gepr_pcomon_###VERSION###.tipo$cods_,
      par$aajen_cod_desc_ajeno       IN OUT gepr_pcomon_###VERSION###.tipo$descs_,
      par$aajen_bol_defecto          IN OUT gepr_pcomon_###VERSION###.tipo$nbols_,
      par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_) 
  IS
      /*Constantes*/
      const$nombre_func constant gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_codigos_ajenos';

      /*Variables locales*/
      var$cod_ajeno_existente         gepr_pcomon_###VERSION###.tipo$cod_;
      var$des_ajeno_existente         gepr_pcomon_###VERSION###.tipo$desc_;
      var$mensaje                     gepr_pcomon_###VERSION###.tipo$desc_;
      var$mensajeEntidadTraducida     gepr_pcomon_###VERSION###.tipo$desc_;
      var$entidad                     gepr_pcomon_###VERSION###.tipo$desc_:= 'CODIGOAJENO';
      var$mensajeEntidad              gepr_pcomon_###VERSION###.tipo$desc_;
      var$tabla_genesis               gepr_pcomon_###VERSION###.tipo$desc_;
      var$cod_entidad                 gepr_pcomon_###VERSION###.tipo$desc_;
      var$des_entidad                 gepr_pcomon_###VERSION###.tipo$desc_;
      var$existe_constraint           gepr_pcomon_###VERSION###.tipo$nel_;
      var$existe_ajeno                gepr_pcomon_###VERSION###.tipo$nel_;
      var$cod_accion                  gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN

        SAPR_PLOG_API.SAGREGA_DETALLE(
          par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     => 'Previo a validar',
          par$cod_identificador   => '');

      IF par$aajen_cod_accion IS NOT NULL AND par$aajen_cod_accion.COUNT>0 THEN
          /*Traducir palabra CODIGOAJENO*/
          var$mensajeEntidadTraducida := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,var$entidad,
                                      gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                      gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                      NULL,
                                      0);
          SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'var$mensajeEntidadTraducida: ' || var$mensajeEntidadTraducida,
            par$cod_identificador   => '');
          FOR idx IN par$aajen_cod_accion.first .. par$aajen_cod_accion.last LOOP

              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Validando código ajeno con los siguientes valores: ' || const$new_line ||
                  'par$aajen_cod_accion(idx): ' || par$aajen_cod_accion(idx) || const$new_line ||
                  'par$aajen_oid_entidad(idx): ' || par$aajen_oid_entidad(idx) || const$new_line ||
                  'par$aajen_cod_entidad(idx): ' || par$aajen_cod_entidad(idx) || const$new_line ||
                  'par$aajen_nel_index_entidad(idx): ' || par$aajen_nel_index_entidad(idx) || const$new_line ||
                  'par$aajen_nel_index_padre(idx): ' || par$aajen_nel_index_padre(idx) || const$new_line ||
                  'par$aajen_cod_identificador(idx): ' || par$aajen_cod_identificador(idx) || const$new_line ||
                  'par$aajen_cod_codigo_ajeno(idx): ' || par$aajen_cod_codigo_ajeno(idx) || const$new_line ||
                  'par$aajen_cod_desc_ajeno(idx): ' || par$aajen_cod_desc_ajeno(idx) || const$new_line ||
                  'par$aajen_bol_defecto(idx): ' || par$aajen_bol_defecto(idx) || const$new_line
                  ,
                  par$cod_identificador   => '');


              /*Validamos que se encuentre el identificador y código*/
              IF par$aajen_cod_identificador(idx) IS NULL OR par$aajen_cod_codigo_ajeno(idx) IS NULL THEN
                var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080009', gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0);
                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                VALUES (
                    '2040080009',
                    var$mensaje,
                    par$aajen_cod_entidad(idx),
                    var$cod_ajeno_existente,
                    'VALIDACIONES',
                    par$aajen_nel_index_entidad(idx),
                    par$aajen_nel_index_padre(idx));

                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => '2040080009:' || var$mensaje,
                  par$cod_identificador   => '');

              END IF;

              CASE par$aajen_cod_entidad(idx)
              WHEN 'CLIENTE' THEN
                    var$tabla_genesis := 'GEPR_TCLIENTE';
              WHEN 'SUBCLIENTE' THEN
                    var$tabla_genesis := 'GEPR_TSUBCLIENTE';
              WHEN 'PUNTOSERVICIO' THEN
                    var$tabla_genesis := 'GEPR_TPUNTO_SERVICIO';
              ELSE
                    var$tabla_genesis := NULL;
              END CASE;

              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Case par$aajen_cod_entidad(idx): ' || const$new_line ||
                  'par$aajen_cod_entidad(idx): ' || par$aajen_cod_entidad(idx) || const$new_line ||
                  'var$tabla_genesis: ' || var$tabla_genesis || const$new_line
                  ,
                  par$cod_identificador   => '');

              var$cod_accion := upper(par$aajen_cod_accion(idx));
              BEGIN
                SELECT COUNT(1)
                  INTO var$existe_ajeno
                  FROM GEPR_TCODIGO_AJENO
                WHERE COD_TIPO_TABLA_GENESIS = var$tabla_genesis 
                AND OID_TABLA_GENESIS = par$aajen_oid_entidad(idx)
                AND COD_IDENTIFICADOR = par$aajen_cod_identificador(idx);


                EXCEPTION
                  WHEN others THEN
                    var$existe_ajeno := 0;
              END;

              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Verificar si existe el código ajeno con los filtros: ' || const$new_line ||
                  'COD_TIPO_TABLA_GENESIS = ' || var$tabla_genesis || const$new_line ||
                  'OID_TABLA_GENESIS = ' || par$aajen_oid_entidad(idx) || const$new_line ||
                  'COD_IDENTIFICADOR = ' || par$aajen_cod_identificador(idx) || const$new_line ||
                  'Resultado: var$existe_ajeno = ' || var$existe_ajeno || const$new_line
                  ,
                  par$cod_identificador   => '');

              --Validamos la accion si debe ser un ALTA O MODIFICACION
              IF (var$cod_accion IS NULL OR var$cod_accion = 'ALTA') THEN
                  IF var$existe_ajeno > 0 THEN
                    var$cod_accion := 'MODIFICAR';
                    IF par$aajen_bol_defecto(idx) IS NULL THEN
                      SELECT BOL_DEFECTO
                        INTO par$aajen_bol_defecto(idx)
                        FROM GEPR_TCODIGO_AJENO
                      WHERE COD_TIPO_TABLA_GENESIS = var$tabla_genesis 
                      AND OID_TABLA_GENESIS = par$aajen_oid_entidad(idx)
                      AND COD_IDENTIFICADOR = par$aajen_cod_identificador(idx)
                      AND ROWNUM = 1;
                    END IF;
                    IF par$aajen_cod_desc_ajeno(idx) IS NULL THEN
                      SELECT DES_AJENO
                        INTO par$aajen_cod_desc_ajeno(idx)
                        FROM GEPR_TCODIGO_AJENO
                      WHERE COD_TIPO_TABLA_GENESIS = var$tabla_genesis 
                      AND OID_TABLA_GENESIS = par$aajen_oid_entidad(idx)
                      AND COD_IDENTIFICADOR = par$aajen_cod_identificador(idx)
                      AND ROWNUM = 1;
                    END IF;
                  ELSE
                    var$cod_accion := 'ALTA';
                    IF par$aajen_bol_defecto(idx) IS NULL THEN
                      par$aajen_bol_defecto(idx) := 0;
                    END IF;
                  END IF;
              END IF;

              SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'Acción del Código Ajeno: ' || const$new_line ||
                  'var$cod_accion = ' || var$cod_accion || const$new_line
                  ,
                  par$cod_identificador   => '');

              /*Para altas*/
              IF var$cod_accion IS NULL OR var$cod_accion = 'ALTA' THEN

                  dbms_output.put_line('par$aajen_cod_identificador(idx): ' || par$aajen_cod_identificador(idx));
                  dbms_output.put_line('par$aajen_cod_codigo_ajeno: ' || par$aajen_cod_codigo_ajeno(idx));
                  dbms_output.put_line('par$aajen_cod_desc_ajeno(idx): ' || par$aajen_cod_desc_ajeno(idx));
                  IF par$aajen_cod_identificador(idx) IS NULL OR par$aajen_cod_codigo_ajeno(idx) IS NULL OR par$aajen_cod_desc_ajeno(idx) IS NULL THEN
                      var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080010', gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente, gepr_pcomon_###VERSION###.const$codAplicacionGenesis, NULL, 0);
                      dbms_output.put_line('2040080010 - ' || var$mensaje);
                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                      VALUES (
                          '2040080010',
                          var$mensaje,
                          par$aajen_cod_entidad(idx),
                          var$cod_ajeno_existente,
                          'VALIDACIONES',
                          par$aajen_nel_index_entidad(idx),
                          par$aajen_nel_index_padre(idx));

                      SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => '2040080010:'  || var$mensaje || const$new_line 
                                                  || 'par$aajen_cod_entidad(' || idx || '): ' || par$aajen_cod_entidad(idx) || const$new_line
                                                  || 'par$aajen_nel_index_entidad(' || idx || '): ' || par$aajen_nel_index_entidad(idx) || const$new_line
                                                  || 'par$aajen_nel_index_padre(' || idx || '): ' || par$aajen_nel_index_padre(idx) || const$new_line
                                                  || 'var$cod_ajeno_existente: ' || var$cod_ajeno_existente || const$new_line
                                                  ,
                          par$cod_identificador   => '');
                  END IF;

                  -- Validamos CONSTRAINT OID_TABLA_GENESIS, COD_IDENTIFICADOR
                  BEGIN
                      SELECT COUNT(1)
                      INTO var$existe_constraint
                      FROM GEPR_TCODIGO_AJENO
                      WHERE OID_TABLA_GENESIS = par$aajen_oid_entidad(idx) AND COD_IDENTIFICADOR = par$aajen_cod_identificador(idx);
                  EXCEPTION  WHEN no_data_found THEN
                      var$existe_constraint := 0;
                  END;
                  IF var$existe_constraint > 0 THEN
                      /*Buscamos la traduccion de la entidad CLIENTE, SUBCLIENTE, PUNTO*/
                      var$mensajeEntidad := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,par$aajen_cod_entidad(idx),
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0);


                      /*Buscamos el mensaje traducido*/
                      /*2040080011 - Ya existe un codigo identificador {0} para la entidad {1}.*/
                      var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080011',
                      gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                      gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                      par$aajen_cod_identificador(idx) || '|' || var$mensajeEntidad,
                      0);

                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                      VALUES (
                          '2040080011',
                          var$mensaje,
                          par$aajen_cod_entidad(idx),
                          par$aajen_cod_codigo_ajeno(idx),
                          'VALIDACIONES',
                          par$aajen_nel_index_entidad(idx),
                          par$aajen_nel_index_padre(idx));

                      SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => '2040080011:' || var$mensaje || const$new_line 
                                                  || 'par$aajen_cod_entidad(' || idx || '): ' || par$aajen_cod_entidad(idx) || const$new_line
                                                  || 'par$aajen_nel_index_entidad(' || idx || '): ' || par$aajen_nel_index_entidad(idx) || const$new_line
                                                  || 'par$aajen_nel_index_padre(' || idx || '): ' || par$aajen_nel_index_padre(idx) || const$new_line
                                                  || 'var$cod_ajeno_existente: ' || var$cod_ajeno_existente || const$new_line
                                                  ,
                          par$cod_identificador   => '');
                  END IF;
      IF par$aajen_cod_entidad(idx) = 'CLIENTE' THEN
                  BEGIN
                       SELECT CA.COD_AJENO, CA.DES_AJENO, CLI.COD_CLIENTE, CLI.DES_CLIENTE
                      INTO var$cod_ajeno_existente, var$des_ajeno_existente, var$cod_entidad, var$des_entidad
                      FROM GEPR_TCODIGO_AJENO CA
                      INNER JOIN GEPR_TCLIENTE CLI ON CLI.OID_CLIENTE = CA.OID_TABLA_GENESIS
                      WHERE COD_TIPO_TABLA_GENESIS = var$tabla_genesis
                      AND BOL_ACTIVO = '1' AND COD_IDENTIFICADOR = par$aajen_cod_identificador(idx) AND COD_AJENO = par$aajen_cod_codigo_ajeno(idx) AND ROWNUM = 1;
                  EXCEPTION  WHEN no_data_found THEN
                      var$cod_ajeno_existente := NULL;
                      var$des_ajeno_existente := NULL;
                      var$cod_entidad := NULL;
                      var$des_entidad := NULL;
                  END;
      END IF;

      IF par$aajen_cod_entidad(idx) = 'SUBCLIENTE' THEN
                  BEGIN
                      SELECT CA.COD_AJENO, CA.DES_AJENO, SUBCLI.COD_SUBCLIENTE, SUBCLI.DES_SUBCLIENTE
                      INTO var$cod_ajeno_existente, var$des_ajeno_existente, var$cod_entidad, var$des_entidad
                      FROM GEPR_TCODIGO_AJENO CA
                      INNER JOIN GEPR_TSUBCLIENTE SUBCLI ON SUBCLI.OID_SUBCLIENTE = CA.OID_TABLA_GENESIS
                      WHERE COD_TIPO_TABLA_GENESIS = var$tabla_genesis
                      AND BOL_ACTIVO = '1' AND COD_IDENTIFICADOR = par$aajen_cod_identificador(idx) AND COD_AJENO = par$aajen_cod_codigo_ajeno(idx) AND ROWNUM = 1;
                  EXCEPTION  WHEN no_data_found THEN
                      var$cod_ajeno_existente := NULL;
                      var$des_ajeno_existente := NULL;
                      var$cod_entidad := NULL;
                      var$des_entidad := NULL;
                  END;
      END IF;
      IF par$aajen_cod_entidad(idx) = 'PUNTOSERVICIO' THEN
                  BEGIN
                     SELECT CA.COD_AJENO, CA.DES_AJENO, PTO.COD_PTO_SERVICIO, PTO.DES_PTO_SERVICIO
                      INTO var$cod_ajeno_existente, var$des_ajeno_existente, var$cod_entidad, var$des_entidad
                      FROM GEPR_TCODIGO_AJENO CA
                      INNER JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_PTO_SERVICIO = CA.OID_TABLA_GENESIS
                      WHERE COD_TIPO_TABLA_GENESIS = var$tabla_genesis
                      AND BOL_ACTIVO = '1' AND COD_IDENTIFICADOR = par$aajen_cod_identificador(idx) AND COD_AJENO = par$aajen_cod_codigo_ajeno(idx) AND ROWNUM = 1;
                  EXCEPTION  WHEN no_data_found THEN
                      var$cod_ajeno_existente := NULL;
                      var$des_ajeno_existente := NULL;
                      var$cod_entidad := NULL;
                      var$des_entidad := NULL;
                  END;
      END IF;
                  SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Verificamos si existe el código ajeno: ' || const$new_line ||
                    'var$cod_ajeno_existente: ' || var$cod_ajeno_existente || const$new_line ||
                    'var$des_ajeno_existente: ' || var$des_ajeno_existente || const$new_line
                  ,
                  par$cod_identificador   => '');

                  IF var$cod_ajeno_existente IS NOT NULL THEN
                      /*Buscamos el mensaje traducido*/
                      /*El identificador {0} y código externo {1} ya está configurado para el {2} de código {3} y descripción {4}.*/
                      var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080013',
                      gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                      gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                      var$cod_ajeno_existente || '|' || var$des_ajeno_existente  || '|' || par$aajen_cod_entidad(idx) || '|' ||  var$cod_entidad || '|' || var$des_entidad,
                      0);

                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                      VALUES (
                          '2040080013',
                          var$mensaje,
                          par$aajen_cod_entidad(idx),
                          var$cod_ajeno_existente,
                          'VALIDACIONES',
                          par$aajen_nel_index_entidad(idx),
                          par$aajen_nel_index_padre(idx));

                      SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => '2040080013:' || var$mensaje || const$new_line 
                                                  || 'par$aajen_cod_entidad(' || idx || '): ' || par$aajen_cod_entidad(idx) || const$new_line
                                                  || 'par$aajen_nel_index_entidad(' || idx || '): ' || par$aajen_nel_index_entidad(idx) || const$new_line
                                                  || 'par$aajen_nel_index_padre(' || idx || '): ' || par$aajen_nel_index_padre(idx) || const$new_line
                                                  || 'var$cod_ajeno_existente: ' || var$cod_ajeno_existente || const$new_line
                                                  ,
                          par$cod_identificador   => '');
                  END IF;
              ELSIF var$cod_accion = 'MODIFICAR' THEN
                /* Validamos al momento de una modificación que no exista el código ajeno en otra entidad*/
                BEGIN
                    SELECT COD_AJENO, DES_AJENO
                    INTO var$cod_ajeno_existente, var$des_ajeno_existente
                    FROM GEPR_TCODIGO_AJENO
                    WHERE COD_IDENTIFICADOR = par$aajen_cod_identificador(idx)
                    AND COD_AJENO = par$aajen_cod_codigo_ajeno(idx)
                    AND COD_TIPO_TABLA_GENESIS = var$tabla_genesis 
                    AND OID_TABLA_GENESIS <> par$aajen_oid_entidad(idx)
                    AND BOL_ACTIVO = '1'
                    AND ROWNUM = 1;
                EXCEPTION  WHEN no_data_found THEN
                    var$cod_ajeno_existente := NULL;
                    var$des_ajeno_existente := NULL;
                END;

                SAPR_PLOG_API.SAGREGA_DETALLE(
                  par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => q'[Verificamos si existe el código ajeno para otra entidad con los filtros: 
                  COD_IDENTIFICADOR =  ']' || par$aajen_cod_identificador(idx) || q'['
                  COD_AJENO = ']' || par$aajen_cod_codigo_ajeno(idx) || q'['
                  COD_TIPO_TABLA_GENESIS = ']' || var$tabla_genesis || q'['
                  OID_TABLA_GENESIS <> ']'  || par$aajen_oid_entidad(idx) || q'['
                  Resultado: ]' || q'[
                  var$cod_ajeno_existente: ]' || var$cod_ajeno_existente || q'[
                  var$des_ajeno_existente: ]' || var$des_ajeno_existente || const$new_line
                  ,
                  par$cod_identificador   => '');


                IF var$cod_ajeno_existente IS NOT NULL THEN
                  /*Buscamos el mensaje traducido*/
                  /*2040080003 - El {0} de codigo "{1}" ({2}) ya existe.*/
                  var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080003',
                  gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                  var$mensajeEntidadTraducida || '|' || var$cod_ajeno_existente || '|' || var$des_ajeno_existente,
                  0);

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5, NEL_CAMPO6)
                  VALUES (
                      '2040080003',
                      var$mensaje,
                      par$aajen_cod_entidad(idx),
                      var$cod_ajeno_existente,
                      'VALIDACIONES',
                      par$aajen_nel_index_entidad(idx),
                      par$aajen_nel_index_padre(idx));

                  SAPR_PLOG_API.SAGREGA_DETALLE(
                      par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     => '2040080003:' || var$mensaje || const$new_line 
                                              || 'par$aajen_cod_entidad(' || idx || '): ' || par$aajen_cod_entidad(idx) || const$new_line
                                              || 'par$aajen_nel_index_entidad(' || idx || '): ' || par$aajen_nel_index_entidad(idx) || const$new_line
                                              || 'par$aajen_nel_index_padre(' || idx || '): ' || par$aajen_nel_index_padre(idx) || const$new_line
                                              || 'var$cod_ajeno_existente: ' || var$cod_ajeno_existente || const$new_line
                                              ,
                      par$cod_identificador   => '');
                END IF;
              END IF;
          END LOOP;
      END IF;
  END svalidar_codigos_ajenos;

    PROCEDURE supd_direccion( par$oid_direccion          IN gepr_pcomon_###VERSION###.tipo$oid_,
                par$oid_tabla_gen          IN gepr_pcomon_###VERSION###.tipo$oid_,
                par$cod_tipo_tgen          IN gepr_pcomon_###VERSION###.tipo$cod_,
                par$des_pais               IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_prov               IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_ciudad             IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_num_tel            IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_email              IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$cod_fiscal             IN gepr_pcomon_###VERSION###.tipo$cod_,
                par$cod_postal             IN gepr_pcomon_###VERSION###.tipo$cod_,
                par$des_dic_1              IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_dic_2              IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_cam_adicional_1    IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_cam_adicional_2    IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_cam_adicional_3    IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_cat_adicional_1    IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_cat_adicional_2    IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$des_cat_adicional_3    IN gepr_pcomon_###VERSION###.tipo$desc_,
                par$cod_usuario            IN gepr_pcomon_###VERSION###.tipo$cod_) IS
 /* variables locales */
    var$gmt_zero            gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_direccion       gepr_pcomon_###VERSION###.tipo$oid_;
    var$qry_update          gepr_pcomon_###VERSION###.tipo$obs_ ;
    cont$vacio CONSTANT     gepr_pcomon_###VERSION###.tipo$desc_ := '###VACIO###';
    BEGIN
        /*Busco el oid_direccion para garantizar que exista*/
        IF par$oid_direccion IS NOT NULL THEN
            BEGIN
                SELECT OID_DIRECCION
                INTO var$oid_direccion
                FROM GEPR_TDIRECCION
                WHERE OID_DIRECCION = par$oid_direccion;
            EXCEPTION WHEN no_data_found THEN
                var$oid_direccion := NULL;
            END;
        END IF;
        IF var$oid_direccion IS NULL THEN
            var$oid_direccion := sys_guid();

            INSERT INTO GEPR_TDIRECCION (
             OID_DIRECCION
            ,OID_TABLA_GENESIS
            ,COD_TIPO_TABLA_GENESIS
            ,DES_PAIS
            ,DES_PROVINCIA
            ,DES_CIUDAD
            ,DES_NUMERO_TELEFONO
            ,DES_EMAIL
            ,COD_FISCAL
            ,COD_POSTAL
            ,DES_DIRECCION_LINEA_1
            ,DES_DIRECCION_LINEA_2
            ,DES_CAMPO_ADICIONAL_1
            ,DES_CAMPO_ADICIONAL_2
            ,DES_CAMPO_ADICIONAL_3
            ,DES_CATEGORIA_ADICIONAL_1
            ,DES_CATEGORIA_ADICIONAL_2
            ,DES_CATEGORIA_ADICIONAL_3
            ,GMT_CREACION
            ,DES_USUARIO_CREACION
            ,GMT_MODIFICACION
            ,DES_USUARIO_MODIFICACION)
            VALUES
            (var$oid_direccion
            ,par$oid_tabla_gen
            ,par$cod_tipo_tgen
            ,par$des_pais
            ,par$des_prov
            ,par$des_ciudad
            ,par$des_num_tel
            ,par$des_email
            ,par$cod_fiscal
            ,par$cod_postal
            ,par$des_dic_1
            ,par$des_dic_2
            ,par$des_cam_adicional_1
            ,par$des_cam_adicional_2
            ,par$des_cam_adicional_3
            ,par$des_cat_adicional_1
            ,par$des_cat_adicional_2
            ,par$des_cat_adicional_3
            ,var$gmt_zero
            ,par$cod_usuario
            ,var$gmt_zero
            ,par$cod_usuario
            );
        ELSE
            IF  par$oid_tabla_gen IS NOT NULL  THEN
                var$qry_update := var$qry_update || q'[ OID_TABLA_GENESIS = ']' || par$oid_tabla_gen ||q'[', ]';
            END IF;

            IF  par$cod_tipo_tgen IS NOT NULL  THEN
                var$qry_update := var$qry_update || q'[ COD_TIPO_TABLA_GENESIS = ']' || par$cod_tipo_tgen ||q'[', ]';
            END IF;

            IF par$des_pais IS NOT NULL THEN
                IF par$des_pais = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_PAIS = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_PAIS = ']' || par$des_pais ||q'[', ]';
                END IF;
            END IF;

            IF par$des_prov IS NOT NULL THEN
                IF par$des_prov = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_PROVINCIA = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_PROVINCIA = ']' || par$des_prov ||q'[', ]';
                END IF;
            END IF;

            IF par$des_ciudad IS NOT NULL THEN
                IF par$des_ciudad = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CIUDAD = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CIUDAD = ']' || par$des_ciudad ||q'[', ]';
                END IF;
            END IF;

            IF par$des_num_tel IS NOT NULL THEN
                IF par$des_num_tel = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_NUMERO_TELEFONO = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_NUMERO_TELEFONO = ']' || par$des_num_tel ||q'[', ]';
                END IF;
            END IF;

            IF par$des_email IS NOT NULL THEN
                IF par$des_email = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_EMAIL = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_EMAIL = ']' || par$des_email ||q'[', ]';
                END IF;
            END IF;

            IF par$cod_fiscal IS NOT NULL THEN
                IF par$cod_fiscal = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ COD_FISCAL = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ COD_FISCAL = ']' || par$cod_fiscal ||q'[', ]';
                END IF;
            END IF;

            IF par$cod_postal IS NOT NULL THEN
                IF par$cod_postal = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ COD_POSTAL = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ COD_POSTAL = ']' || par$cod_postal ||q'[', ]';
                END IF;
            END IF;

            IF par$des_dic_1 IS NOT NULL THEN
                IF par$des_cam_adicional_1 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_DIRECCION_LINEA_1 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_DIRECCION_LINEA_1 = ']' || par$des_dic_1 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_dic_2 IS NOT NULL THEN
                IF par$des_cam_adicional_1 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_DIRECCION_LINEA_2 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_DIRECCION_LINEA_2 = ']' || par$des_dic_2 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_cam_adicional_1 IS NOT NULL THEN
                IF par$des_cam_adicional_1 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_1 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_1 = ']' || par$des_cam_adicional_1 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_cam_adicional_2 IS NOT NULL THEN
                IF par$des_cam_adicional_2 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_2 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_2 = ']' || par$des_cam_adicional_2 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_cam_adicional_3 IS NOT NULL THEN
                IF par$des_cam_adicional_3 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_3 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_3 = ']' || par$des_cam_adicional_3 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_cat_adicional_1 IS NOT NULL THEN
                IF par$des_cat_adicional_1 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CATEGORIA_ADICIONAL_1 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CATEGORIA_ADICIONAL_1 = ']' || par$des_cat_adicional_1 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_cat_adicional_2 IS NOT NULL THEN
                IF par$des_cat_adicional_2 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CATEGORIA_ADICIONAL_2 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CATEGORIA_ADICIONAL_2 = ']' || par$des_cat_adicional_2 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_cat_adicional_3 IS NOT NULL THEN
                IF par$des_cat_adicional_3 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CATEGORIA_ADICIONAL_3 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CATEGORIA_ADICIONAL_3 = ']' || par$des_cat_adicional_3 ||q'[', ]';
                END IF;
            END IF;

           IF  var$qry_update IS NOT NULL  THEN
             DBMS_OUTPUT.PUT_LINE('UPDATE GEPR_TDIRECCION SET ' ||  var$qry_update || ' GMT_MODIFICACION =' || var$gmt_zero || 'DES_USUARIO_MODIFICACION = ' || par$cod_usuario || ' WHERE OID_DIRECCION = ' ||var$oid_direccion);
             dbms_output.put_line('Cod_Usuario' || par$cod_usuario);
             EXECUTE IMMEDIATE q'[ UPDATE GEPR_TDIRECCION SET ]' || var$qry_update ||q'[
             GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2
             WHERE OID_DIRECCION = :3]'
              USING  var$gmt_zero, par$cod_usuario, var$oid_direccion;
           END IF;
        END IF;
    END supd_direccion;


  PROCEDURE supd_cliente(par$oid_cliente               IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                       par$cod_cliente                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                       par$des_cliente                 IN gepr_pcomon_###VERSION###.tipo$desc_,
                       par$oid_tipo_cliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                       par$cod_bancario                IN gepr_pcomon_###VERSION###.tipo$cod_,
                       par$bol_vigente                 IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$bol_totaliza_saldo          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$bol_abona_por_total         IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$bol_banco_capital           IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$bol_banco_comision          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$num_percent_comision        IN gepr_pcomon_###VERSION###.tipo$nel_,
                       par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$desc_) IS

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_ ;
    var$existe        gepr_pcomon_###VERSION###.tipo$nel_ ;
  BEGIN

     BEGIN

        IF par$oid_cliente IS NOT NULL THEN
            BEGIN
                SELECT COUNT(1)
                INTO var$existe
                FROM GEPR_TCLIENTE
                WHERE OID_CLIENTE = par$oid_cliente;
            EXCEPTION WHEN no_data_found THEN
             var$existe := 0;
            END;
        ELSE
             var$existe := 0;
             par$oid_cliente  := sys_guid();
        END IF;


        IF var$existe = 0 THEN


                INSERT INTO GEPR_TCLIENTE
                            (OID_CLIENTE,
                            COD_CLIENTE,
                            DES_CLIENTE,
                            BOL_VIGENTE,
                            COD_USUARIO,
                            FYH_ACTUALIZACION,
                            OID_TIPO_CLIENTE,
                            BOL_TOTALIZADOR_SALDO,
                            BOL_ABONA_POR_TOTAL,
                            BOL_BANCO_CAPITAL,
                            BOL_BANCO_COMISION,
                            NUM_PORCENT_COMISION,
                            COD_BANCARIO)
                            VALUES
                            (par$oid_cliente,
                            par$cod_cliente,
                            par$des_cliente,
                            1,
                            par$cod_usuario,
                             sys_extract_utc(current_timestamp) ,
                            par$oid_tipo_cliente,
                            par$bol_totaliza_saldo,
                            par$bol_abona_por_total,
                            par$bol_banco_capital,
                            par$bol_banco_comision,
                            par$num_percent_comision,
                            par$cod_bancario);
        ELSE

           IF  par$cod_cliente IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ COD_CLIENTE = ']' || par$cod_cliente ||q'[', ]';
           END IF;

           IF  par$des_cliente IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ DES_CLIENTE = ']' || par$des_cliente ||q'[', ]';
           END IF;

           IF par$bol_vigente IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_VIGENTE = ']' || par$bol_vigente ||q'[', ]';
           END IF;

           IF par$oid_tipo_cliente IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ OID_TIPO_CLIENTE = ']' || par$oid_tipo_cliente ||q'[', ]';
           END IF;

           IF par$bol_totaliza_saldo IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_TOTALIZADOR_SALDO = ']' || par$bol_totaliza_saldo ||q'[', ]';
           END IF;

           IF par$bol_abona_por_total IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_ABONA_POR_TOTAL = ']' || par$bol_abona_por_total ||q'[', ]';
           END IF;

           IF par$bol_banco_capital IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_BANCO_CAPITAL = ']' || par$bol_banco_capital ||q'[', ]';
           END IF;

           IF par$bol_banco_comision IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_BANCO_COMISION = ']' || par$bol_banco_comision ||q'[', ]';
           END IF;

           IF par$num_percent_comision IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ NUM_PORCENT_COMISION = ']' || par$num_percent_comision ||q'[', ]';
           END IF;

           IF par$cod_bancario IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ COD_BANCARIO = ']' || par$cod_bancario ||q'[', ]';
           END IF;

           IF  var$qry_update IS NOT NULL  THEN
             EXECUTE IMMEDIATE q'[ UPDATE GEPR_TCLIENTE SET ]' || var$qry_update ||q'[
             FYH_ACTUALIZACION = sys_extract_utc(current_timestamp), COD_USUARIO = :1
             WHERE OID_CLIENTE = :2]'
              USING  par$cod_usuario, par$oid_cliente;
           END IF;

            IF par$bol_vigente IS NOT NULL AND par$bol_vigente = 0 THEN
                UPDATE GEPR_TCLIENTE
                SET BOL_VIGENTE = 0
                WHERE OID_CLIENTE = par$oid_cliente;

                UPDATE GEPR_TSUBCLIENTE
                SET BOL_VIGENTE = 0
                WHERE OID_CLIENTE = par$oid_cliente;


                UPDATE GEPR_TPUNTO_SERVICIO
                SET BOL_VIGENTE = 0
                WHERE OID_SUBCLIENTE IN (SELECT OID_SUBCLIENTE FROM GEPR_TSUBCLIENTE WHERE OID_CLIENTE = par$oid_cliente);

                UPDATE SAPR_TACUERDO_SERVICIO
                SET BOL_VIGENTE = 0
                WHERE OID_PTO_SERVICIO IN 
                    (SELECT OID_PTO_SERVICIO 
                        FROM GEPR_TPUNTO_SERVICIO 
                        WHERE OID_SUBCLIENTE IN 
                        (SELECT OID_SUBCLIENTE FROM 
                            GEPR_TSUBCLIENTE 
                            WHERE OID_CLIENTE = par$oid_cliente));
            END IF;

        END IF;

    END;

  END supd_cliente;


  PROCEDURE supd_subcliente(par$oid_subcliente                 IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_cliente                 IN gepr_pcomon_###VERSION###.tipo$oid_,
                       par$cod_subcliente                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                       par$des_subcliente                 IN gepr_pcomon_###VERSION###.tipo$desc_,
                       par$oid_tipo_subcliente            IN gepr_pcomon_###VERSION###.tipo$oid_,
                       par$bol_enviado_saldos             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$bol_vigente                 IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$bol_totaliza_saldo          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$desc_) IS

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_ ;
    var$existe        gepr_pcomon_###VERSION###.tipo$nel_ ;

  BEGIN

     BEGIN
        IF par$oid_subcliente IS NOT NULL THEN
            BEGIN
                SELECT COUNT(1)
                INTO var$existe
                FROM GEPR_TSUBCLIENTE
                WHERE OID_SUBCLIENTE = par$oid_subcliente;
            EXCEPTION WHEN no_data_found THEN
             var$existe := 0;
            END;
        ELSE
             var$existe := 0;
             par$oid_subcliente  := sys_guid();
        END IF;


                DBMS_OUTPUT.PUT_LINE('var$existe: ' || var$existe);
                DBMS_OUTPUT.PUT_LINE('par$oid_subcliente: ' || par$oid_subcliente);
        IF var$existe = 0 THEN

             INSERT INTO GEPR_TSUBCLIENTE
                        (OID_SUBCLIENTE,
                        OID_CLIENTE,
                        COD_SUBCLIENTE,
                        DES_SUBCLIENTE,
                        BOL_VIGENTE,
                        COD_USUARIO,
                        FYH_ACTUALIZACION,
                        OID_TIPO_SUBCLIENTE,
                        BOL_TOTALIZADOR_SALDO)
                        VALUES
                        (par$oid_subcliente,
                         par$oid_cliente,
                         par$cod_subcliente,
                         par$des_subcliente,
                         1,
                         par$cod_usuario,
                          sys_extract_utc(current_timestamp) ,
                         par$oid_tipo_subcliente,
                         par$bol_totaliza_saldo);
        ELSE


           IF  par$oid_cliente IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_CLIENTE = ']' || par$oid_cliente ||q'[', ]';
           END IF;

           IF  par$cod_subcliente IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ COD_SUBCLIENTE = ']' || par$cod_subcliente ||q'[', ]';
           END IF;

           IF  par$des_subcliente IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ DES_SUBCLIENTE = ']' || par$des_subcliente ||q'[', ]';
           END IF;

           IF par$bol_vigente IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_VIGENTE = ']' || par$bol_vigente ||q'[', ]';
           END IF;

           IF par$oid_tipo_subcliente IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ OID_TIPO_SUBCLIENTE = ']' || par$oid_tipo_subcliente ||q'[', ]';
           END IF;

           IF par$bol_totaliza_saldo IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_TOTALIZADOR_SALDO = ']' || par$bol_totaliza_saldo ||q'[', ]';
           END IF;


          IF  var$qry_update IS NOT NULL  THEN
             EXECUTE IMMEDIATE q'[ UPDATE GEPR_TSUBCLIENTE SET ]' || var$qry_update ||q'[
             FYH_ACTUALIZACION = sys_extract_utc(current_timestamp), COD_USUARIO = :1
             WHERE OID_SUBCLIENTE = :2]'
              USING  par$cod_usuario, par$oid_subcliente;
           END IF;

            IF par$bol_vigente IS NOT NULL AND par$bol_vigente = 0 THEN
                UPDATE GEPR_TSUBCLIENTE
                SET BOL_VIGENTE = 0
                WHERE OID_SUBCLIENTE = par$oid_subcliente;


                UPDATE GEPR_TPUNTO_SERVICIO
                SET BOL_VIGENTE = 0
                WHERE OID_SUBCLIENTE = par$oid_subcliente;

                UPDATE SAPR_TACUERDO_SERVICIO
                SET BOL_VIGENTE = 0
                WHERE OID_PTO_SERVICIO IN 
                    (SELECT OID_PTO_SERVICIO FROM 
                            GEPR_TPUNTO_SERVICIO 
                            WHERE OID_SUBCLIENTE = par$oid_subcliente);
            END IF;

        END IF;




       END;

  END supd_subcliente;

  PROCEDURE supd_pto_servicio(par$oid_pto_servicio                 IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_subcliente                 IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                       par$cod_pto_servicio                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                       par$des_pto_servicio                 IN gepr_pcomon_###VERSION###.tipo$desc_,
                       par$oid_tipo_pto_servicio            IN gepr_pcomon_###VERSION###.tipo$oid_,
                       par$bol_vigente                 IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$bol_totaliza_saldo          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$desc_) IS

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_ ;
    var$existe        gepr_pcomon_###VERSION###.tipo$nel_ ;

  BEGIN

     BEGIN
        IF par$oid_pto_servicio IS NOT NULL THEN
            BEGIN
                SELECT COUNT(1)
                INTO var$existe
                FROM GEPR_TPUNTO_SERVICIO
                WHERE OID_PTO_SERVICIO = par$oid_pto_servicio;
            EXCEPTION WHEN no_data_found THEN
             var$existe := 0;
            END;
        ELSE
             var$existe := 0;
             par$oid_pto_servicio  := sys_guid();
        END IF;


        IF var$existe = 0 THEN

          INSERT INTO GEPR_TPUNTO_SERVICIO
                    (OID_PTO_SERVICIO,
                    OID_SUBCLIENTE,
                    COD_PTO_SERVICIO,
                    DES_PTO_SERVICIO,
                    BOL_VIGENTE,
                    COD_USUARIO,
                    FYH_ACTUALIZACION,
                    OID_TIPO_PUNTO_SERVICIO,
                    BOL_TOTALIZADOR_SALDO)
                    VALUES
                    (par$oid_pto_servicio,
                    par$oid_subcliente,
                    par$cod_pto_servicio,
                    par$des_pto_servicio,
                    1,
                    par$cod_usuario,
                     sys_extract_utc(current_timestamp) ,
                    par$oid_tipo_pto_servicio,
                    par$bol_totaliza_saldo);
        ELSE


           IF  par$oid_subcliente IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_SUBCLIENTE = ']' || par$oid_subcliente ||q'[', ]';
           END IF;

           IF  par$cod_pto_servicio IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ COD_PTO_SERVICIO = ']' || par$cod_pto_servicio ||q'[', ]';
           END IF;

           IF  par$des_pto_servicio IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ DES_PTO_SERVICIO = ']' || par$des_pto_servicio ||q'[', ]';
           END IF;

           IF par$bol_vigente IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_VIGENTE = ']' || par$bol_vigente ||q'[', ]';
           END IF;

           IF par$oid_tipo_pto_servicio IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ OID_TIPO_PUNTO_SERVICIO = ']' || par$oid_tipo_pto_servicio ||q'[', ]';
           END IF;

           IF par$bol_totaliza_saldo IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_TOTALIZADOR_SALDO = ']' || par$bol_totaliza_saldo ||q'[', ]';
           END IF;


           IF  var$qry_update IS NOT NULL  THEN
             EXECUTE IMMEDIATE q'[ UPDATE GEPR_TPUNTO_SERVICIO SET ]' || var$qry_update ||q'[
             FYH_ACTUALIZACION = sys_extract_utc(current_timestamp), COD_USUARIO = :1
             WHERE OID_PTO_SERVICIO = :2]'
              USING  par$cod_usuario, par$oid_pto_servicio;
           END IF;

            IF par$bol_vigente IS NOT NULL AND par$bol_vigente = 0 THEN
                UPDATE SAPR_TACUERDO_SERVICIO
                SET BOL_VIGENTE = 0
                WHERE OID_PTO_SERVICIO = par$oid_pto_servicio;
            END IF;

        END IF;


       END;

  END supd_pto_servicio;


PROCEDURE supd_codigo_ajeno(par$oid_pto_servicio                 IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_subcliente                 IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                       par$cod_pto_servicio                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                       par$des_pto_servicio                 IN gepr_pcomon_###VERSION###.tipo$desc_,
                       par$oid_tipo_pto_servicio            IN gepr_pcomon_###VERSION###.tipo$oid_,
                       par$bol_vigente                 IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$bol_totaliza_saldo          IN gepr_pcomon_###VERSION###.tipo$nbol_,
                       par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$desc_) IS

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_ ;

  BEGIN

     BEGIN
        IF par$oid_pto_servicio IS NULL THEN
                par$oid_pto_servicio  := sys_guid();

          INSERT INTO GEPR_TPUNTO_SERVICIO
                    (OID_PTO_SERVICIO,
                    OID_SUBCLIENTE,
                    COD_PTO_SERVICIO,
                    DES_PTO_SERVICIO,
                    BOL_VIGENTE,
                    COD_USUARIO,
                    FYH_ACTUALIZACION,
                    OID_TIPO_PUNTO_SERVICIO,
                    BOL_TOTALIZADOR_SALDO)
                    VALUES
                    (par$oid_pto_servicio,
                    par$oid_subcliente,
                    par$cod_pto_servicio,
                    par$des_pto_servicio,
                    1,
                    par$cod_usuario,
                     sys_extract_utc(current_timestamp) ,
                    par$oid_tipo_pto_servicio,
                    par$bol_totaliza_saldo);
        ELSE


           IF  par$oid_subcliente IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_SUBCLIENTE = ']' || par$oid_subcliente ||q'[', ]';
           END IF;

           IF  par$cod_pto_servicio IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ COD_PTO_SERVICIO = ']' || par$cod_pto_servicio ||q'[', ]';
           END IF;

           IF  par$des_pto_servicio IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ DES_PTO_SERVICIO = ']' || par$des_pto_servicio ||q'[', ]';
           END IF;

           IF par$bol_vigente IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_VIGENTE = ']' || par$bol_vigente ||q'[', ]';
           END IF;

           IF par$oid_tipo_pto_servicio IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ OID_TIPO_PUNTO_SERVICIO = ']' || par$oid_tipo_pto_servicio ||q'[', ]';
           END IF;

           IF par$bol_totaliza_saldo IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_TOTALIZADOR_SALDO = ']' || par$bol_totaliza_saldo ||q'[', ]';
           END IF;


           IF  var$qry_update IS NOT NULL  THEN
             EXECUTE IMMEDIATE q'[ UPDATE GEPR_TPUNTO_SERVICIO SET ]' || var$qry_update ||q'[
             FYH_ACTUALIZACION = sys_extract_utc(current_timestamp), COD_USUARIO = :1
             WHERE OID_PTO_SERVICIO = :2]'
              USING  par$cod_usuario, par$oid_pto_servicio;
           END IF;



        END IF;


       END;

  END supd_codigo_ajeno;
  FUNCTION fhay_error RETURN BOOLEAN IS
    var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    BEGIN

      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR A
       WHERE A.COD_CALIFICADOR = 'VALIDACIONES' and A.OID_CAMPO1 = 'OID_BLOQUEANTE'  /* Tiene que ser un código bloqueante */
       ;

    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;

    RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);

  END fhay_error;

  FUNCTION fhay_error_configurar_cliente (par$entidad       IN gepr_pcomon_###VERSION###.tipo$desc_,
                                          par$nel_index     IN gepr_pcomon_###VERSION###.tipo$nel_)RETURN BOOLEAN IS
  var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN
    BEGIN
      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR A
        WHERE A.COD_CALIFICADOR = 'VALIDACIONES' AND A.COD_CAMPO3 = par$entidad AND A.NEL_CAMPO5 = par$nel_index
        AND OID_CAMPO1 <> '0040080000';
    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;
    RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);
  END fhay_error_configurar_cliente;


    PROCEDURE sconfigurar_AcuerdosServicio(
                                     par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$anel_index              IN gepr_pcomon_###VERSION###.tipo$nels_,
                                     par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$acod_accion             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$ades_ContractId         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_serviceOrderID     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_serviceOrderCode   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ades_ProductCode        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$acod_cliente            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$acod_sub_cliente        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$acod_punto_servicio     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$afyh_vigencia_inicio    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                     par$afyh_vigencia_fin       IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                     par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rc_validaciones         OUT sys_refcursor)
    IS
        var$aoid_cliente   gepr_pcomon_###VERSION###.tipo$oids_;
        var$aoid_subcliente   gepr_pcomon_###VERSION###.tipo$oids_;
        var$aoid_pto_servicio   gepr_pcomon_###VERSION###.tipo$oids_;
        var$oid_pais   gepr_pcomon_###VERSION###.tipo$oid_;
        var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
        var$existe_registro  gepr_pcomon_###VERSION###.tipo$nel_;
        const$nombre_func constant gepr_pcomon_###VERSION###.tipo$desc_ := 'sconfigurar_AcuerdosServicio';
    BEGIN
        /* #### Inicializar los cursores #### */
        OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
        DELETE FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'VALIDACIONES';
        COMMIT;

        IF par$oid_llamada IS NOT NULL THEN
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

            /* COD_USUARIO */
            SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'par$cod_usuario: ' || par$cod_usuario,
                    par$cod_identificador   => '');

            /* COD_CULTURA */
            SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'par$cod_cultura: ' || par$cod_cultura,
                    par$cod_identificador   => '');

            /* cod_identificador_ajeno */
            SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
                    par$cod_identificador   => '');

            /*anel_index*/
            IF par$anel_index IS NOT NULL AND par$anel_index.count>0 THEN
                FOR idx IN par$anel_index.first .. par$anel_index.last LOOP
                    IF par$anel_index(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$anel_index (' || idx || '): ' || par$anel_index(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*acod_accion*/
            IF par$acod_accion IS NOT NULL AND par$acod_accion.count>0 THEN
                FOR idx IN par$acod_accion.first .. par$acod_accion.last LOOP
                    IF par$acod_accion(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$acod_accion (' || idx || '): ' || par$acod_accion(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*ades_ContractId*/
            IF par$ades_ContractId IS NOT NULL AND par$ades_ContractId.count>0 THEN
                FOR idx IN par$ades_ContractId.first .. par$ades_ContractId.last LOOP
                    IF par$ades_ContractId(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$ades_ContractId (' || idx || '): ' || par$ades_ContractId(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*ades_serviceOrderID*/
            IF par$ades_serviceOrderID IS NOT NULL AND par$ades_serviceOrderID.count>0 THEN
                FOR idx IN par$ades_serviceOrderID.first .. par$ades_serviceOrderID.last LOOP
                    IF par$ades_serviceOrderID(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$ades_serviceOrderID (' || idx || '): ' || par$ades_serviceOrderID(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;


            /*ades_serviceOrderCode*/
            IF par$ades_serviceOrderCode IS NOT NULL AND par$ades_serviceOrderCode.count>0 THEN
                FOR idx IN par$ades_serviceOrderCode.first .. par$ades_serviceOrderCode.last LOOP
                    IF par$ades_serviceOrderCode(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$ades_serviceOrderCode (' || idx || '): ' || par$ades_serviceOrderCode(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*ades_ProductCode*/
            IF par$ades_ProductCode IS NOT NULL AND par$ades_ProductCode.count>0 THEN
                FOR idx IN par$ades_ProductCode.first .. par$ades_ProductCode.last LOOP
                    IF par$ades_ProductCode(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$ades_ProductCode (' || idx || '): ' || par$ades_ProductCode(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*acod_cliente*/
            IF par$acod_cliente IS NOT NULL AND par$acod_cliente.count>0 THEN
                FOR idx IN par$acod_cliente.first .. par$acod_cliente.last LOOP
                    IF par$acod_cliente(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$acod_cliente (' || idx || '): ' || par$acod_cliente(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*acod_sub_cliente*/
            IF par$acod_sub_cliente IS NOT NULL AND par$acod_sub_cliente.count>0 THEN
                FOR idx IN par$acod_sub_cliente.first .. par$acod_sub_cliente.last LOOP
                    IF par$acod_sub_cliente(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$acod_sub_cliente (' || idx || '): ' || par$acod_sub_cliente(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*acod_punto_servicio*/
            IF par$acod_punto_servicio IS NOT NULL AND par$acod_punto_servicio.count>0 THEN
                FOR idx IN par$acod_punto_servicio.first .. par$acod_punto_servicio.last LOOP
                    IF par$acod_punto_servicio(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$acod_punto_servicio (' || idx || '): ' || par$acod_punto_servicio(idx),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*afyh_vigencia_inicio*/
            IF par$afyh_vigencia_inicio IS NOT NULL AND par$afyh_vigencia_inicio.count>0 THEN
                FOR idx IN par$afyh_vigencia_inicio.first .. par$afyh_vigencia_inicio.last LOOP
                    IF par$afyh_vigencia_inicio(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$afyh_vigencia_inicio (' || idx || '): ' || TO_CHAR(par$afyh_vigencia_inicio(idx), 'DD-MM-YYYY HH24:MI:SS'),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

            /*afyh_vigencia_fin*/
            IF par$afyh_vigencia_fin IS NOT NULL AND par$afyh_vigencia_fin.count>0 THEN
                FOR idx IN par$afyh_vigencia_fin.first .. par$afyh_vigencia_fin.last LOOP
                    IF par$afyh_vigencia_fin(idx) IS NOT NULL THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => 'par$afyh_vigencia_fin (' || idx || '): ' || TO_CHAR(par$afyh_vigencia_fin(idx), 'DD-MM-YYYY HH24:MI:SS'),
                                par$cod_identificador   => '');
                    END IF;
                END LOOP;
            END IF;

        END IF;

        SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'Previo a validar acuerdosservicios',
                par$cod_identificador   => '');

        /* Llamo al procedure de validar */
        svalidar_acuerdosservicios(         par$oid_llamada             => par$oid_llamada,
                                            par$cod_identificador_ajeno         => par$cod_identificador_ajeno,
                                            par$cod_pais                 => par$cod_pais,
                                            par$anel_index               => par$anel_index,
                                            par$acod_accion              => par$acod_accion,
                                            par$ades_ContractId          => par$ades_ContractId,
                                            par$ades_serviceOrderID      => par$ades_serviceOrderID,
                                            par$ades_serviceOrderCode    => par$ades_serviceOrderCode,
                                            par$ades_ProductCode         => par$ades_ProductCode,
                                            par$acod_cliente             => par$acod_cliente,
                                            par$acod_sub_cliente         => par$acod_sub_cliente,
                                            par$acod_punto_servicio      => par$acod_punto_servicio,
                                            par$afyh_vigencia_inicio     => par$afyh_vigencia_inicio,
                                            par$afyh_vigencia_fin        => par$afyh_vigencia_fin,
                                            par$cod_cultura              => par$cod_cultura,
                                            par$oid_pais                 => var$oid_pais,
                                            par$aoid_cliente             => var$aoid_cliente,
                                            par$aoid_subcliente          => var$aoid_subcliente,
                                            par$aoid_pto_servicio        => var$aoid_pto_servicio);

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Posterior a validar acuerdosservicios',
            par$cod_identificador   => '');

        IF par$anel_index is not null and par$anel_index.count > 0 THEN
            FOR idx in par$anel_index.first .. par$anel_index.last LOOP
                IF par$anel_index(idx) is not null THEN
                    IF NOT fhay_error_acuerdo_servicio(par$indice => par$anel_index(idx)) THEN
                        IF par$acod_accion(idx) = 'ALTA' THEN
                            /* Validamos si ya existe el registro */
                              SELECT COUNT(1)
                                INTO var$existe_registro 
                              FROM SAPR_TACUERDO_SERVICIO
                              WHERE 
                                NVL(DES_CONTRACT_ID, '###NULL###') = NVL(par$ades_ContractId(idx), '###NULL###')
                                AND NVL(DES_SERVICE_ORDER_ID, '###NULL###') = NVL(par$ades_serviceOrderID(idx), '###NULL###') 
                                AND NVL(DES_SERVICE_ORDER_CODE, '###NULL###') = NVL(par$ades_serviceOrderCode(idx), '###NULL###')
                                AND NVL(DES_PRODUCT_CODE, '###NULL###') = NVL(par$ades_ProductCode(idx), '###NULL###')
                                AND OID_CLIENTE = var$aoid_cliente(par$anel_index(idx))
                                AND OID_SUBCLIENTE = var$aoid_subcliente(par$anel_index(idx))
                                AND OID_PTO_SERVICIO = var$aoid_pto_servicio(par$anel_index(idx))
                                AND OID_PAIS = var$oid_pais;
                           
                            IF var$existe_registro = 0 THEN
                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     =>  'ES ALTA' || const$new_line  || 'par$ades_ContractId(' || idx ||'): ' || par$ades_ContractId(idx) || const$new_line
                                                        || 'par$ades_serviceOrderID(' || idx ||'): ' || par$ades_serviceOrderID(idx) || const$new_line
                                                        || 'par$ades_serviceOrderCode(' || idx ||'): ' || par$ades_serviceOrderCode(idx) || const$new_line
                                                        || 'par$ades_ProductCode(' || idx ||'): ' || par$ades_ProductCode(idx) || const$new_line
                                                        || 'var$aoid_cliente(par$anel_index(' || idx ||')): '  ||var$aoid_cliente(par$anel_index(idx)) || const$new_line
                                                        || 'var$aoid_subcliente(par$anel_index(' || idx ||')): ' || var$aoid_subcliente(par$anel_index(idx)) || const$new_line
                                                        || 'var$aoid_pto_servicio(par$anel_index(' || idx ||')): ' || var$aoid_pto_servicio(par$anel_index(idx)) || const$new_line
                                                        || 'par$afyh_vigencia_inicio(' || idx ||'): ' || TO_CHAR(par$afyh_vigencia_inicio(idx), 'DD-MM-YYYY HH24:MI:SS') || const$new_line
                                                        || 'par$afyh_vigencia_fin(' || idx ||'): ' || TO_CHAR(par$afyh_vigencia_fin(idx), 'DD-MM-YYYY HH24:MI:SS'),
                                par$cod_identificador   => '');


                               INSERT INTO SAPR_TACUERDO_SERVICIO
                                (
                                    OID_ACUERDO_SERVICIO, DES_CONTRACT_ID, DES_SERVICE_ORDER_ID, DES_SERVICE_ORDER_CODE, DES_PRODUCT_CODE,
                                    OID_CLIENTE, OID_SUBCLIENTE, OID_PTO_SERVICIO, FYH_VIGENCIA_INICIO, FYH_VIGENCIA_FIN, OID_PAIS,  BOL_VIGENTE,
                                    GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
                                )
                                VALUES
                                (
                                    sys_guid(), par$ades_ContractId(idx), par$ades_serviceOrderID(idx), par$ades_serviceOrderCode(idx), par$ades_ProductCode(idx),
                                    var$aoid_cliente(par$anel_index(idx)), var$aoid_subcliente(par$anel_index(idx)), var$aoid_pto_servicio(par$anel_index(idx)), 
                                    par$afyh_vigencia_inicio(idx), par$afyh_vigencia_fin(idx), var$oid_pais, 1, /* Si es un alta, esta vigente */
                                    var$gmt_zero, par$cod_usuario, var$gmt_zero, par$cod_usuario
                                );

                                SAPR_PLOG_API.SAGREGA_DETALLE(
                                    par$oid_llamada  => par$oid_llamada,
                                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                    par$des_version     => const$version,
                                    par$des_detalle     => 'Creó un nuevo registro en la tabla SAPR_TACUERDO_SERVICIO',
                                    par$cod_identificador   => '');
                            ELSE

                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                  par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'Ya existe un registro en la tabla SAPR_TACUERDO_SERVICIO con los parametros informados',
                                  par$cod_identificador   => '');
                                      

                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     =>  'ES MODIFICACION' || const$new_line  || 'par$ades_ContractId(' || idx ||'): ' || par$ades_ContractId(idx) || const$new_line
                                                        || 'par$ades_serviceOrderID(' || idx ||'): ' || par$ades_serviceOrderID(idx) || const$new_line
                                                        || 'par$ades_serviceOrderCode(' || idx ||'): ' || par$ades_serviceOrderCode(idx) || const$new_line
                                                        || 'par$ades_ProductCode(' || idx ||'): ' || par$ades_ProductCode(idx) || const$new_line
                                                        || 'var$aoid_cliente(par$anel_index(' || idx ||')): '  ||var$aoid_cliente(par$anel_index(idx)) || const$new_line
                                                        || 'var$aoid_subcliente(par$anel_index(' || idx ||')): ' || var$aoid_subcliente(par$anel_index(idx)) || const$new_line
                                                        || 'var$aoid_pto_servicio(par$anel_index(' || idx ||')): ' || var$aoid_pto_servicio(par$anel_index(idx)) || const$new_line
                                                        || 'par$afyh_vigencia_inicio(' || idx ||'): ' || TO_CHAR(par$afyh_vigencia_inicio(idx), 'DD-MM-YYYY HH24:MI:SS') || const$new_line
                                                        || 'par$afyh_vigencia_fin(' || idx ||'): ' || TO_CHAR(par$afyh_vigencia_fin(idx), 'DD-MM-YYYY HH24:MI:SS'),
                                par$cod_identificador   => '');


                              UPDATE SAPR_TACUERDO_SERVICIO
                              SET BOL_VIGENTE = 1,
                                  FYH_VIGENCIA_INICIO = par$afyh_vigencia_inicio(idx),  
                                  FYH_VIGENCIA_FIN = par$afyh_vigencia_fin(idx),
                                  GMT_MODIFICACION = var$gmt_zero,
                                  DES_USUARIO_MODIFICACION = par$cod_usuario

                              WHERE 
                              NVL(DES_CONTRACT_ID, '###NULL###') = NVL(par$ades_ContractId(idx), '###NULL###')
                              AND NVL(DES_SERVICE_ORDER_ID, '###NULL###') = NVL(par$ades_serviceOrderID(idx), '###NULL###') 
                              AND NVL(DES_SERVICE_ORDER_CODE, '###NULL###') = NVL(par$ades_serviceOrderCode(idx), '###NULL###')
                              AND NVL(DES_PRODUCT_CODE, '###NULL###') = NVL(par$ades_ProductCode(idx), '###NULL###')
                              AND OID_CLIENTE = var$aoid_cliente(par$anel_index(idx))
                              AND OID_SUBCLIENTE = var$aoid_subcliente(par$anel_index(idx))
                              AND OID_PTO_SERVICIO = var$aoid_pto_servicio(par$anel_index(idx))
                              AND OID_PAIS = var$oid_pais;

                            END IF;
                        ELSE /* En caso de baja valido que todos los campos de la peticion coincidan */
                          dbms_output.put_line('Es baja');
                          SAPR_PLOG_API.SAGREGA_DETALLE(
                              par$oid_llamada  => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     =>  'ES BAJA' || const$new_line  || 'par$ades_ContractId(' || idx ||'): ' || par$ades_ContractId(idx) || const$new_line
                                                      || 'par$ades_serviceOrderID(' || idx ||'): ' || par$ades_serviceOrderID(idx) || const$new_line
                                                      || 'par$ades_serviceOrderCode(' || idx ||'): ' || par$ades_serviceOrderCode(idx) || const$new_line
                                                      || 'par$ades_ProductCode(' || idx ||'): ' || par$ades_ProductCode(idx) || const$new_line
                                                      || 'var$aoid_cliente(par$anel_index(' || idx ||')): '  ||var$aoid_cliente(par$anel_index(idx)) || const$new_line
                                                      || 'var$aoid_subcliente(par$anel_index(' || idx ||')): ' || var$aoid_subcliente(par$anel_index(idx)) || const$new_line
                                                      || 'var$aoid_pto_servicio(par$anel_index(' || idx ||')): ' || var$aoid_pto_servicio(par$anel_index(idx)),
                              par$cod_identificador   => '');

                          UPDATE SAPR_TACUERDO_SERVICIO 
                          SET BOL_VIGENTE = 0,
                              GMT_MODIFICACION = var$gmt_zero,
                              DES_USUARIO_MODIFICACION = par$cod_usuario
                          WHERE DES_CONTRACT_ID = par$ades_ContractId(idx) 
                          AND (DES_SERVICE_ORDER_ID = par$ades_serviceOrderID(idx) OR par$ades_serviceOrderID(idx) IS NULL) 
                          AND (DES_SERVICE_ORDER_CODE = par$ades_serviceOrderCode(idx) OR par$ades_serviceOrderCode(idx) IS NULL) 
                          AND (DES_PRODUCT_CODE = par$ades_ProductCode(idx) OR par$ades_ProductCode(idx) IS NULL) 
                          AND OID_CLIENTE = var$aoid_cliente(par$anel_index(idx)) 
                          AND OID_SUBCLIENTE = var$aoid_subcliente(par$anel_index(idx)) 
                          AND OID_PTO_SERVICIO = var$aoid_pto_servicio(par$anel_index(idx)) 
                          AND OID_PAIS = var$oid_pais;


                          dbms_output.put_line('Fin baja');
                          SAPR_PLOG_API.SAGREGA_DETALLE(
                                  par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'Se actualizó la baja SAPR_TACUERDO_SERVICIO',
                                  par$cod_identificador   => '');
                        END IF;

                        /* Notificamos que el acuerdo ha sido procesado con éxito */
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, NEL_CAMPO5, COD_CALIFICADOR)
                        VALUES ('0040320000', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040320000',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        NULL,
                        0), par$anel_index(idx), 'VALIDACIONES');
                    END IF;
                END IF;
            END LOOP;
        END IF;

        dbms_output.put_line('Devuelve cursor');
        OPEN par$rc_validaciones FOR
            SELECT
                OID_CAMPO1 CODIGO,
                COD_CAMPO2 DESCRIPCION,
                NEL_CAMPO5 NEL_INDEX
            FROM SAPR_GTT_TAUXILIAR
            where
                COD_CALIFICADOR='VALIDACIONES'
            order by NEL_CAMPO5 DESC;
        COMMIT;
    EXCEPTION
        when others then
            SAPR_PLOG_API.SAGREGA_DETALLE(
                        par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => '¡Ocurrió una excepción!' || const$new_line || SQLCODE || ' - ' || SQLERRM,
                        par$cod_identificador   => '');

            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                      SQLCODE || ' - ' || SQLERRM,
                      true);

            OPEN par$rc_validaciones FOR
            SELECT
                OID_CAMPO1 CODIGO,
                COD_CAMPO2 DESCRIPCION,
                NEL_CAMPO5 NEL_INDEX
            FROM SAPR_GTT_TAUXILIAR
            where
                COD_CALIFICADOR='VALIDACIONES'
            order by NEL_CAMPO5 ASC;
    END sconfigurar_AcuerdosServicio;

    PROCEDURE svalidar_acuerdosservicios(par$oid_llamada           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$anel_index              IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        par$acod_accion             IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ades_ContractId         IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$ades_serviceOrderID     IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$ades_serviceOrderCode   IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$ades_ProductCode        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$acod_cliente            IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$acod_sub_cliente        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$acod_punto_servicio     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$afyh_vigencia_inicio    IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                        par$afyh_vigencia_fin       IN gepr_pcomon_###VERSION###.tipo$fyhs_,
                                        par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_pais                OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$aoid_cliente            OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aoid_subcliente         OUT gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$aoid_pto_servicio       OUT gepr_pcomon_###VERSION###.tipo$oids_)
    IS
        var$oid_pais            gepr_pcomon_###VERSION###.tipo$oid_;
        var$cod_pais            gepr_pcomon_###VERSION###.tipo$cod_;
        var$aoid_cliente        gepr_pcomon_###VERSION###.tipo$oids_;
        var$aoid_subcliente     gepr_pcomon_###VERSION###.tipo$oids_;
        var$aoid_pto_servicio   gepr_pcomon_###VERSION###.tipo$oids_;
        var$oid_cliente         gepr_pcomon_###VERSION###.tipo$oid_;
        var$oid_subcliente      gepr_pcomon_###VERSION###.tipo$oid_;
        var$oid_pto_servicio    gepr_pcomon_###VERSION###.tipo$oid_;
        const$cod_alta constant gepr_pcomon_###VERSION###.tipo$cod_ := 'ALTA';
        const$cod_baja constant gepr_pcomon_###VERSION###.tipo$cod_ := 'BAJA';
        const$cod_baja_aut constant gepr_pcomon_###VERSION###.tipo$cod_ := 'BAJA-AUTO';
        var$mensaje             gepr_pcomon_###VERSION###.tipo$desc_;
        var$existe              gepr_pcomon_###VERSION###.tipo$nel_;
        const$nombre_func constant gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_acuerdosservicios';
        

    BEGIN
        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Por validar par$cod_pais: ' || par$cod_pais || ' y par$cod_identificador_ajeno: ' || par$cod_identificador_ajeno,
            par$cod_identificador   => '');

        /* Validar código de país */
        /* Al recibir el par$cod_pais de tipo OUT necesito crear una variable*/
        var$cod_pais := par$cod_pais;
        GEPR_PPAIS_###VERSION###.srecuperar_pais(par$oid_llamada => par$oid_llamada,
                                par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                                par$cod_pais => var$cod_pais,
                                par$oid_pais => var$oid_pais,
                                par$cod_cultura => par$cod_cultura);

        SAPR_PLOG_API.SAGREGA_DETALLE(
            par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'var$oid_pais: ' || var$oid_pais,
            par$cod_identificador   => '');

        IF par$anel_index IS NOT NULL AND par$anel_index.COUNT > 0 THEN
            FOR idx in par$anel_index.first .. par$anel_index.last LOOP
                IF par$anel_index(idx) is not null THEN

                    /*Validamos que sea un producto correcto*/
                    IF par$ades_ProductCode(idx) IS NOT NULL AND (UPPER(par$ades_ProductCode(idx)) <> 'PR00117' AND UPPER(par$ades_ProductCode(idx)) <> 'PR00160')  THEN
                        /*0040320001 - El código de producto "{0}" no es compatible con Génesis Producto. 
                        Los productos soportados por la plataforma son "Fecha Valor" (código "PR00117") y "Transacciones" (código "PR00160"). */
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040320001',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                par$ades_ProductCode(idx),
                                0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES ('0040320001', var$mensaje, 'VALIDACIONES', par$anel_index(idx));
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => var$mensaje,
                            par$cod_identificador   => '');
                    END IF;

                    /*Validamos que el ContractId no sea NULO*/
                    IF par$ades_ContractId(idx) IS NULL THEN
                        /*2040320008 - Es obligatorio informar el ContractId */
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040320008',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                par$acod_accion(idx),
                                0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES ('2040320008', var$mensaje, 'VALIDACIONES', par$anel_index(idx));
                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => var$mensaje,
                            par$cod_identificador   => '');
                    END IF;

                    /*Validamos que el código de acción sea correcto*/
                    IF par$acod_accion(idx) <> const$cod_alta AND par$acod_accion(idx) <> const$cod_baja AND par$acod_accion(idx) <> const$cod_baja_aut THEN
                        /*2040320004 - El código de acción {0} ingresado no es válido */
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040320004',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                par$acod_accion(idx),
                                0);
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES ('2040320004', var$mensaje, 'VALIDACIONES', par$anel_index(idx));

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => var$mensaje,
                            par$cod_identificador   => '');
                    END IF;

                    /*Validamos en caso de ALTA las fechas de vigencia*/
                    IF par$acod_accion(idx) IS NULL OR par$acod_accion(idx) = const$cod_alta THEN
                      IF par$afyh_vigencia_inicio(idx) IS NULL THEN
                        /*2040320009 - Es obligatorio informar fecha de inicio de vigencia. */
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040320009',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                NULL,
                                0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES ('2040320009', var$mensaje, 'VALIDACIONES', par$anel_index(idx));
                      END IF;
                      IF par$afyh_vigencia_fin(idx) IS NULL THEN
                        /*2040320010 - Es obligatorio informar fecha de fin de vigencia. */
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040320010',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                NULL,
                                0);

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES ('2040320010', var$mensaje, 'VALIDACIONES', par$anel_index(idx));
                      END IF;
                      
                      IF NOT fhay_error_acuerdo_servicio(par$indice => par$anel_index(idx)) THEN
                        IF par$afyh_vigencia_inicio(idx) > par$afyh_vigencia_fin(idx) THEN
                          /*2040320011 - La fecha de inicio de vigencia no puede ser mayor a la fecha de fin de vigencia */
                          var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040320011',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                NULL,
                                0);

                          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                          VALUES ('2040320011', var$mensaje, 'VALIDACIONES', par$anel_index(idx));
                        END IF;
                      END IF;

                    END IF;

                    /*Validamos que el cliente/subcliente/punto exista*/
                    BEGIN
                    IF par$cod_identificador_ajeno IS NOT NULL THEN
                        /*Acá vemos en las tablas "ajenas"*/
                        select cliente.oid_cliente, subcli.oid_subcliente, pto.oid_pto_servicio
                            into var$oid_cliente, var$oid_subcliente, var$oid_pto_servicio
                            from GEPR_TPUNTO_SERVICIO pto
                            INNER JOIN GEPR_TSUBCLIENTE SUBCLI ON PTO.OID_SUBCLIENTE = SUBCLI.OID_SUBCLIENTE
                            INNER JOIN GEPR_TCLIENTE CLIENTE ON SUBCLI.OID_CLIENTE = CLIENTE.OID_CLIENTE
                            INNER JOIN GEPR_TCODIGO_AJENO PTOAJ ON PTOAJ.OID_TABLA_GENESIS = PTO.OID_PTO_SERVICIO AND PTOAJ.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND PTOAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO'
                            LEFT JOIN GEPR_TCODIGO_AJENO CLIAJ ON CLIAJ.OID_TABLA_GENESIS = CLIENTE.OID_CLIENTE AND CLIAJ.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND CLIAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE'
                            LEFT JOIN GEPR_TCODIGO_AJENO SUBCLIAJ ON SUBCLIAJ.OID_TABLA_GENESIS = SUBCLI.OID_SUBCLIENTE AND SUBCLIAJ.COD_IDENTIFICADOR = par$cod_identificador_ajeno AND SUBCLIAJ.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE'
                        WHERE 
                            PTOAJ.COD_AJENO = par$acod_punto_servicio(idx)
                            AND (par$acod_sub_cliente(idx) is null OR SUBCLIAJ.COD_AJENO = par$acod_sub_cliente(idx)) 
                            AND (par$acod_cliente(idx) is null OR CLIAJ.COD_AJENO = par$acod_cliente(idx));

                    ELSE
                        /*Acá vemos directamente en las tablas de genesis*/

                        select cliente.oid_cliente, subcli.oid_subcliente, pto.oid_pto_servicio
                            into var$oid_cliente, var$oid_subcliente, var$oid_pto_servicio
                            from GEPR_TPUNTO_SERVICIO pto
                            INNER JOIN GEPR_TSUBCLIENTE subcli on pto.oid_subcliente = subcli.oid_subcliente
                            INNER JOIN GEPR_TCLIENTE cliente on subcli.oid_cliente = cliente.oid_cliente
                        where 
                            pto.cod_pto_servicio = par$acod_punto_servicio(idx)
                            AND (par$acod_sub_cliente(idx) is null OR subcli.cod_subcliente = par$acod_sub_cliente(idx))
                            AND (par$acod_cliente(idx) is null OR cliente.cod_cliente = par$acod_cliente(idx))
                            ;

                    END IF;
                    exception
                    when too_many_rows then
                        /*2040320005 - Más de un punto de servicio encontrado*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040320005',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                NULL,
                                0);
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES ('2040320005', var$mensaje, 'VALIDACIONES', par$anel_index(idx));
                        var$oid_cliente := NULL;
                        var$oid_subcliente := NULL;
                        var$oid_pto_servicio := NULL;

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => var$mensaje,
                            par$cod_identificador   => '');
                    when no_data_found then
                        /*2040320006 - No se encontró el cliente/subcliente/punto*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040320006',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                NULL,
                                0);
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES ('2040320006', var$mensaje, 'VALIDACIONES', par$anel_index(idx));
                        var$oid_cliente := NULL;
                        var$oid_subcliente := NULL;
                        var$oid_pto_servicio := NULL;

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => var$mensaje,
                            par$cod_identificador   => '');
                    END;

                    IF NOT fhay_error_acuerdo_servicio(par$indice => par$anel_index(idx)) AND par$acod_accion(idx) = const$cod_baja THEN
                        begin
                            select count(1)
                              into var$existe
                              from SAPR_TACUERDO_SERVICIO
                            WHERE 
                              DES_CONTRACT_ID = par$ades_ContractId(idx) 
                              AND (DES_SERVICE_ORDER_ID = par$ades_serviceOrderID(idx) OR par$ades_serviceOrderID(idx) IS NULL) 
                              AND (DES_SERVICE_ORDER_CODE = par$ades_serviceOrderCode(idx) OR par$ades_serviceOrderCode(idx) IS NULL) 
                              AND (DES_PRODUCT_CODE = par$ades_ProductCode(idx) OR par$ades_ProductCode(idx) IS NULL) 
                              AND OID_CLIENTE = var$oid_cliente 
                              AND OID_SUBCLIENTE = var$oid_subcliente 
                              AND OID_PTO_SERVICIO = var$oid_pto_servicio
                              AND OID_PAIS = var$oid_pais;
                              
                            exception
                              when others then
                                  var$existe := 0;
                        end;
                        IF var$existe = 0 THEN
                            /*2040320007 - No se encontraron registros con los parámetros ingresados*/
                            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040320007',
                                    gepr_pcomon_###VERSION###.const$CodFunConfigurarAcuerdo,
                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                    NULL,
                                    0);
                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR, NEL_CAMPO5)
                            VALUES ('2040320007', var$mensaje, 'VALIDACIONES', par$anel_index(idx));

                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => var$mensaje,
                                par$cod_identificador   => '');
                        END IF;

                    END IF;

                    SAPR_PLOG_API.SAGREGA_DETALLE(
                        par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => 'OID_CLIENTE pos ' || par$anel_index(idx) || ' valor: ' || var$oid_cliente || ' idx: ' || idx || const$new_line
                                               || 'OID_SUBCLIENTE pos ' || par$anel_index(idx) || ' valor: ' || var$oid_subcliente || ' idx: ' || idx || const$new_line
                                               || 'OID_PTO_SERVICIO pos ' || par$anel_index(idx) || ' valor: ' || var$oid_pto_servicio || ' idx: ' || idx || const$new_line,
                        par$cod_identificador   => '');

                    dbms_output.put_line('Validar: OID_CLIENTE pos ' || par$anel_index(idx) || ' valor: ' || var$oid_cliente || ' idx: ' || idx );
                    dbms_output.put_line('Validar: OID_SUBCLIENTE pos ' || par$anel_index(idx) || ' valor: ' || var$oid_subcliente );
                    dbms_output.put_line('Validar: OID_PTO_SERVICIO pos ' || par$anel_index(idx) || ' valor: ' || var$oid_pto_servicio );
                    var$aoid_cliente(par$anel_index(idx)) := var$oid_cliente;
                    var$aoid_subcliente(par$anel_index(idx)) := var$oid_subcliente;
                    var$aoid_pto_servicio(par$anel_index(idx)) := var$oid_pto_servicio;

                END IF;
            END LOOP;
        END IF;

        /* Poblamos los parametros de salida */
        par$aoid_cliente := var$aoid_cliente;
        par$aoid_subcliente := var$aoid_subcliente;
        par$aoid_pto_servicio := var$aoid_pto_servicio;
        par$oid_pais := var$oid_pais;

    exception
        when others then
            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,
                                par$des_detalle     => '¡Ocurrió una excepción!',
                                par$cod_identificador   => '');

            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                        SQLCODE || ' - ' || SQLERRM,
                        true);
    END svalidar_acuerdosservicios;

    FUNCTION fhay_error_acuerdo_servicio(par$indice IN gepr_pcomon_###VERSION###.tipo$nel_) RETURN BOOLEAN IS
        var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    BEGIN

        BEGIN
        dbms_output.put_line('HayerrorAcuerdoServicio - index:  ' || par$indice );

        /*Se trata el mensaje 0040320001 como un error para que no siga procesando el registro, 
        pero el resultado debe ser un success por eso comienza con 0*/
        SELECT COUNT(1)
            INTO var$existe
            FROM SAPR_GTT_TAUXILIAR A
            WHERE A.COD_CALIFICADOR = 'VALIDACIONES' and (A.OID_CAMPO1 LIKE '204032%' OR A.OID_CAMPO1 = '0040320001')
            AND A.NEL_CAMPO5 = par$indice
            ;
        EXCEPTION WHEN no_data_found THEN
            var$existe := 0;
        END;

        dbms_output.put_line('HayerrorAcuerdoServicio - index:  ' || par$indice || ' valor: ' || var$existe );

    RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);

    END fhay_error_acuerdo_servicio;

END SAPR_PCLIENTE_###VERSION###;
/