CREATE OR REPLACE PACKAGE SAPR_PCONTENEDOR_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  const$CodTipoDocReenvioCliente CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'REENVIO_CLIENTE';
  const$CodTipoDocReenvioSector  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'REENVIO_SECTOR';
  const$CodTipoDocDesarmar       CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'DESARMAR';

  PROCEDURE sdesarmar_contenedor(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                 par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                 par$oid_delegacion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_planta             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_sector             IN gepr_pcomon_###VERSION###.tipo$oid_
                                 /* arrays asociativos contenedor */,
                                 par$acosoid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$acoscod_precinto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$codcultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$oid_documento      out gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$info_ejecucion     IN VARCHAR2,
                                 par$hacer_commit       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_);

PROCEDURE s_upd_tposicion_sector(par$codIdioma                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$codDelegacion              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$codPlanta                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$codSector                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$acont_cod_precinto         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acont_cod_posicion         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acont_cod_posicion_destino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$UsuarioCreacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$hacer_Commit               IN integer,
                                   par$info_ejecucion             IN VARCHAR2,
                                   par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE svalidar_desarmar_contenedor(par$cod_formulario IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$oid_delegacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_planta     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_sector     IN gepr_pcomon_###VERSION###.tipo$oid_
                                         /* arrays asociativos contenedor */,
                                         par$codcultura IN gepr_pcomon_###VERSION###.tipo$cod_);

  PROCEDURE srecuperar_contenedores(par$oids_contenedores  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$cods_precintos     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$rc_contenedores    OUT gepr_pcomon_###VERSION###.tipo$contenedores,
                                    par$rc_detalle_efec_mp OUT gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores,
                                    par$rc_precintos       OUT gepr_pcomon_###VERSION###.tipo$precintos_contenedores,
                                    par$rc_lst_val_cont    OUT gepr_pcomon_###VERSION###.tipo$lst_val_contenedores,
                                    par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  FUNCTION fverificar_documento_agrupador(par$rc_contenedores IN gepr_pcomon_###VERSION###.tipo$contenedores)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  PROCEDURE screar_parametros_grupo_doc(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$oid_delegacion_origen  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_origen      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_origen      IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$oid_delegacion_destino IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_destino     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_destino     IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$oid_cliente_destino       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_subcliente_destino    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_puntoservicio_destino in gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$codcultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_tipo_doc              in gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$rc_contenedores           IN gepr_pcomon_###VERSION###.tipo$contenedores,
                                        par$rc_detalle_efec_mp        IN gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores,
                                        par$rc_precintos              IN gepr_pcomon_###VERSION###.tipo$precintos_contenedores,
                                        par$rc_lst_val_cont           IN gepr_pcomon_###VERSION###.tipo$lst_val_contenedores,
                                        par$cod_comprobante           OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$updates                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$deletes                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE screar_parametros_documento(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_,

                                        par$oid_delegacion_origen IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_origen     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_origen     IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$oid_delegacion_destino IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_destino     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_destino     IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$oid_cliente_destino       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_subcliente_destino    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_puntoservicio_destino in gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$codcultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_tipo_doc       in gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$rc_contenedores    IN gepr_pcomon_###VERSION###.tipo$contenedores,
                                        par$rc_detalle_efec_mp IN gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores,
                                        par$rc_precintos       IN gepr_pcomon_###VERSION###.tipo$precintos_contenedores,
                                        par$rc_lst_val_cont    IN gepr_pcomon_###VERSION###.tipo$lst_val_contenedores,
                                        par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_documento      out gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE s_ins_talerta_venc_contenedor(par$codPrecinto     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$fechaAlerta     IN DATE,
                                          par$codTipoAlerta   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$diasVencer      IN INTEGER,
                                          par$Emails          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$UsuarioCreacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$hacer_Commit    IN integer);

  FUNCTION fvalidar_estado_contenedor(par$oid_contenedor IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado     IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  FUNCTION frecuperar_oid_tipo_cont(par$cod_tipo_contenedor gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

  PROCEDURE s_buscarcontenedorxposicion(par$codDelegacion      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codPlanta          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codSector          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$packModular        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rc_contenedores    OUT SYS_REFCURSOR,
                                        par$rc_detalle_efec_mp OUT SYS_REFCURSOR,
                                        par$cod_usuario        in gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion     IN VARCHAR2,
                                        par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_);

   PROCEDURE srecuperar_contenedores_fifo(par$cod_tipo_contenedor IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$oid_delegacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_planta          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_sector          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_cliente         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_subcliente      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_canal           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_subcanal        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_divisa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_denominacion    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$importe             IN gepr_pcomon_###VERSION###.tipo$imp_,
                                         par$codIdioma           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_canal           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_subcanal        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$rc_contenedores     OUT SYS_REFCURSOR,
                                         par$rc_precintos        OUT SYS_REFCURSOR,
                                         par$cod_usuario         in gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion      IN VARCHAR2,
                                         par$cod_ejecucion       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE sgenerar_oids_contenedor_fifo(par$cod_tipo_contenedor IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$oid_delegacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_planta          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_sector          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_cliente         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_subcliente      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_canal           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_subcanal        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_divisa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_denominacion    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$importe             IN gepr_pcomon_###VERSION###.tipo$imp_,
                                          par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE sconsultar_contenedor_cli(par$codIdioma          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$codTipoContenedor  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$codEstadoConenedor IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$fyhArmadoDesde     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                      par$fyhArmadoHasta     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                      par$bolPackModular     IN INTEGER,
                                      par$codCliente         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codSubCliente      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codPtoServicio     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codDelegacion      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codPlanta          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codSector          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codCanal           IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codSubcanal        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$rcContenedores     OUT SYS_REFCURSOR,
                                      par$rcClientes         OUT SYS_REFCURSOR,
                                      par$rcPrecintos        OUT SYS_REFCURSOR,
                                      par$rcDetalleEfecMP    OUT SYS_REFCURSOR,
                                      par$cod_usuario        in gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$info_ejecucion     IN VARCHAR2,
                                      par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE s_ins_tinventario_contenedor(par$codIdioma            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codInventario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$regresarDetalles     IN INTEGER,
                                         par$codDelegacion        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codPlanta            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codSector            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codCliente           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codSubcliente        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codPuntoServicio     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cods_precintos      IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$UsuarioCreacion      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$hacer_Commit         IN INTEGER,
                                         par$rc_Inventario        OUT SYS_REFCURSOR,
                                         par$rc_detalle_efec_mp   OUT SYS_REFCURSOR);

  PROCEDURE s_consultar_invent_contenedor(par$codIdioma                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codInventario            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$fechaHoraInventarioDesde IN DATE,
                                          par$fechaHoraInventarioHasta IN DATE,
                                          par$codDelegacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codPlanta                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codSector                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codCliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codSubcliente            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codPuntoServicio         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$rc_Inventario            OUT SYS_REFCURSOR,
                                          par$rc_detalle_efec_mp       OUT SYS_REFCURSOR,
                                          par$cod_usuario              in gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_);

  procedure sins_tprecintoxcontenedor(par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aprco_oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aprco_bol_precinto_autom   in gepr_pcomon_###VERSION###.tipo$nbols_,
                                      par$cod_usuario                in gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  -- faz o calculo da data devencimento do contenedor
  function fcalcular_fec_vencimento_cont(par$aefco_oid_divisa in gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefco_oid_denom  in gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$bol_acepta_pico  in gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$cod_cultura      in gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$selects          out gepr_pcomon_###VERSION###.tipo$nel_)
    return gepr_pcomon_###VERSION###.tipo$fyh_;

  function frecuperar_tipo_cont(par$cod_tipo_contenedor in gepr_pcomon_###VERSION###.tipo$cod_,
                                par$selects out gepr_pcomon_###VERSION###.tipo$nel_)
    return gepr_pcomon_###VERSION###.tipo$tipocontenedor;

  PROCEDURE s_ins_tconsultarelemento(par$codIdioma       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$codTipoElemento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oidElemento     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$codPrecinto     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rcElementos     OUT SYS_REFCURSOR,
                                     par$rcDocumentos    OUT SYS_REFCURSOR,
                                     par$rc_precintos    OUT SYS_REFCURSOR,
                                     par$rcCuentas       OUT SYS_REFCURSOR,
                                     par$rcAcciones      OUT SYS_REFCURSOR);

  procedure sins_tcontenedor(par$oid_documento         in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_contenedor        in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_tipo_contenedor   in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_contenedor_padre  in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_sector            in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_cuenta_movimiento in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_cuenta_saldo      in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_grupo_documento   in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$bol_pack_modular      in gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$fec_vencimento        in gepr_pcomon_###VERSION###.tipo$fyh_,
                             par$bol_agrupa_cont       in gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$cod_puesto            in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_usuario           in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$oid_cont_mayor_nivel  in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$cod_contenedor        in gepr_pcomon_###VERSION###.tipo$cod_,
               par$inserts               OUT gepr_pcomon_###VERSION###.tipo$nel_);

  procedure sins_tefectivoxcontenedor(par$oid_contenedor    gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_divisa        gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_denominacion  gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_unidad_medida gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_calidad       gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_nivel_detalle gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$bol_disponible    gepr_pcomon_###VERSION###.tipo$nbol_,
                                      par$bol_bloqueado     gepr_pcomon_###VERSION###.tipo$nbol_,
                                      par$cod_tipo_contado  gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$num_importe       gepr_pcomon_###VERSION###.tipo$imp_,
                                      par$nel_cantidad      gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$cod_usuario       gepr_pcomon_###VERSION###.tipo$cod_,
                    par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_);

  procedure sins_tmedio_pagoxcontenedor(par$oid_contenedor      gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_divisa          gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_medio_pago      gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_unidad_medida   gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_nivel_detalle   gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$bol_disponible      gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$bol_bloqueado       gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$num_importe         gepr_pcomon_###VERSION###.tipo$imp_,
                                        par$nel_cantidad        gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_usuario         gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_tipo_medio_pago gepr_pcomon_###VERSION###.tipo$cod_,
                    par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  procedure svincular_elemento_contenedor(par$aelco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$oid_delegacion       in gepr_pcomon_###VERSION###.tipo$oid_,
                      par$selects              out gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$updates              out gepr_pcomon_###VERSION###.tipo$nel_);

 procedure sgrabar_contenedor_ind(par$obj_formularios in gepr_pcomon_###VERSION###.tipo$formularios,
                                   par$oid_delegacion  in gepr_pcomon_###VERSION###.tipo$oid_,
                                   --documentos
                                   par$adoc_oid_documento in gepr_pcomon_###VERSION###.tipo$oids_,
                                   -- contenedores
                                   par$adoccont_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoccont_obj_tipo_cont     in gepr_pcomon_###VERSION###.tipo$tiposContenedores,
                                   par$adoccont_cod_tipo_servicio IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_tipo_embalaje IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_puesto        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_contenedor    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   -- Precintos
                                   par$aprco_oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aprco_bol_precinto_autom   in gepr_pcomon_###VERSION###.tipo$nbols_,
                                   -- Elementos
                                   par$aelco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   /* arrays de efectivo do contenedor */
                                   par$aefco_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_denom         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_uni_med       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefco_cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefco_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aefco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   /* arrays de medio de pago do contenedor */
                                   par$ampco_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_oid_mp            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$ampco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$ampco_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,
                                   /* arrays de terminos de medio de pago do contenedor */
                                   par$avtmpco_oid_contenedor   in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_oid_mp           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_oid_termediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtmpco_nec_indicegrupo  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                   /* informacion de ejecucion */
                                   par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_);


   procedure sbaja_contenedor_ind(par$obj_formularios in gepr_pcomon_###VERSION###.tipo$formularios,
                                 par$oid_delegacion  in gepr_pcomon_###VERSION###.tipo$oid_,
                                 --documentos
                                 par$adoc_oid_documento        in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adoc_oid_sector_origen    in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adoc_oid_cuenta_origen    in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adoc_oid_cuenta_saldo_ori in gepr_pcomon_###VERSION###.tipo$oids_,
                                 -- contenedores
                                 par$adoccont_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 -- Elementos
                                 par$aelco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 /* informacion de ejecucion */
                                 par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  procedure supd_tcontenedor(par$oid_contenedor   in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_documento    in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_cuenta_mov   in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_cuenta_saldo in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_sector       in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$cod_usuario      in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_estado       in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$updates          OUT gepr_pcomon_###VERSION###.tipo$nel_);

  procedure sgrabar_contenedor_grp(par$obj_formularios     in gepr_pcomon_###VERSION###.tipo$formularios,
                                   par$oid_delegacion      in gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_grupo_documento in gepr_pcomon_###VERSION###.tipo$oid_,
                                   -- contenedores
                                   par$adoccont_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoccont_obj_tipo_cont     in gepr_pcomon_###VERSION###.tipo$tiposContenedores,
                                   par$adoccont_cod_tipo_servicio IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_tipo_embalaje IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_puesto        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_contenedor    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   -- Precintos
                                   par$aprco_oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aprco_bol_precinto_autom   in gepr_pcomon_###VERSION###.tipo$nbols_,
                                   -- Elementos
                                   par$aelco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   /* arrays de efectivo do contenedor */
                                   par$aefco_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_denom         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_uni_med       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefco_cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefco_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aefco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   /* arrays de medio de pago do contenedor */
                                   par$ampco_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_oid_mp            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$ampco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$ampco_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* arrays de terminos de medio de pago do contenedor */
                                   par$avtmpco_oid_contenedor   in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_oid_mp           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_oid_termediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtmpco_nec_indicegrupo  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                   /* informacion de ejecucion */
                                   par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE sreenvio_entre_sectores(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                    par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_
                                    /* origem */,
                                    par$oid_delegacion_ori IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_planta_ori     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_ori     IN gepr_pcomon_###VERSION###.tipo$oid_
                                    /* destino */,
                                    par$oid_delegacion_des IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_planta_des     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_des     IN gepr_pcomon_###VERSION###.tipo$oid_
                                    /* arrays asociativos contenedor */,
                                    par$acosoid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$acoscod_precinto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$acosrowver         IN gepr_pcomon_###VERSION###.tipo$nels_,
                                    par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                  par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_documento      out gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$info_ejecucion     IN VARCHAR2,
                                    par$hacer_commit       in gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE sreenvio_entre_clientes(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                    par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_
                                    /* origem */,
                                    par$oid_delegacion_ori IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_planta_ori     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_ori     IN gepr_pcomon_###VERSION###.tipo$oid_
                                    /* destino */,
                                    par$oid_cliente_des      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_subcliente_des   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_pto_servicio_des IN gepr_pcomon_###VERSION###.tipo$oid_
                                    /* arrays asociativos contenedor */,
                                    par$acosoid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$acoscod_precinto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$acosrowver         IN gepr_pcomon_###VERSION###.tipo$nels_,
                                    par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_documento      out gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$info_ejecucion     IN VARCHAR2,
                                    par$hacer_commit       in gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE svalidar_datos_contenedores(par$cod_formulario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_delegacion_ori   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_ori       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_ori       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_delegacion_des   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_des       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_des       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_cliente_des      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_subcliente_des   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_pto_servicio_des IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_servicio         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$codcultura           IN gepr_pcomon_###VERSION###.tipo$cod_);

  PROCEDURE sconsultar_contenedor_sector(par$codIdioma                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codTipoContenedor         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codEstadoConenedor        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$fyhArmadoDesde            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                         par$fyhArmadoHasta            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                         par$bolmayorNivel             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$bolPackModular            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$bolretornarElementosHijos IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$aPrecintos                IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$codCliente                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codSubCliente             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codPtoServicio            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codDelegacion             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codPlanta                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$acodSector                IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$codCanal                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codSubcanal               IN gepr_pcomon_###VERSION###.tipo$cod_,
										 par$bolRecSoloContSinPosicion     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$rcContenedores            OUT SYS_REFCURSOR,
                                         par$rcEfectivos               OUT SYS_REFCURSOR,
                                         par$rcMediosPago              OUT SYS_REFCURSOR,
                                         par$cod_usuario               in gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion            IN VARCHAR2,
                                         par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

PROCEDURE SP_CONSULTARDOCGESTIONCONT(par$codIdioma                IN VARCHAR2,
                                       par$fechaDocumentoDesde      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                       par$fechaDocumentoHasta      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                       par$codEstadoDocumento       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codSector                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codDelegacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codPlanta                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codCliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codSubCliente            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codPtoServicio           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codCanal                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codSubcanal              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codPrecinto              IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$codTipoContenedor        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codOrigenBusqueda        in gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$rcDocumentos             OUT SYS_REFCURSOR,
                                       par$rcPrecintos              OUT SYS_REFCURSOR,
                                       par$rcDetalleEfecMPDocumento OUT SYS_REFCURSOR,
                                       par$cod_usuario              in gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$info_ejecucion           IN VARCHAR2,
                                       par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_);

PROCEDURE SP_INSVALORESCONTENEDOR(par$bolPai       IN gepr_pcomon_###VERSION###.tipo$bol_,
                                      par$existeCuenta IN INTEGER);

PROCEDURE SP_CONSULTARCONTPACKMODULAR(par$codIdioma        IN VARCHAR2,
                                        par$vencidos         IN INTEGER,
                                        par$fechaVencimiento IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$codTipoSector    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$codDelegacion    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$codPlanta        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$codSector        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$codCliente       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codSubCliente    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codPtoServicio   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codCanal         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codSubcanal      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rcContenedores   OUT SYS_REFCURSOR,
                                        par$rcAlertas        OUT SYS_REFCURSOR,
                                        par$rcClientes       OUT SYS_REFCURSOR,
                                        par$rcPrecintos      OUT SYS_REFCURSOR,
                                        par$rcDetalleEfec    OUT SYS_REFCURSOR,
                                        par$cod_usuario      in gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion   IN VARCHAR2,
                                        par$cod_ejecucion    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$inserts          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$deletes          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects          OUT gepr_pcomon_###VERSION###.tipo$nel_);

 procedure svalidar_tipo_valor(par$oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oid_,
                                par$aefco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                par$aefco_oid_denom      in gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ampco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ampco_oid_mp         in gepr_pcomon_###VERSION###.tipo$oids_,
                                par$obj_tipo_cont        in gepr_pcomon_###VERSION###.tipo$tipoContenedor,
                                par$cod_cultura          in gepr_pcomon_###VERSION###.tipo$cod_,
                                par$selects              out gepr_pcomon_###VERSION###.tipo$nel_);

  procedure svalidar_calidad(par$oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$aefco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                             par$aefco_oid_calidad    IN gepr_pcomon_###VERSION###.tipo$oids_,
                             par$obj_tipo_cont        in gepr_pcomon_###VERSION###.tipo$tipoContenedor,
                             par$cod_cultura          in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$selects              out gepr_pcomon_###VERSION###.tipo$nel_);

  procedure svalidar_cantidad(par$oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oid_,
                              par$aefco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aefco_nel_cantidad   IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$ampco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                              par$ampco_nel_cantidad   IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$obj_tipo_cont        in gepr_pcomon_###VERSION###.tipo$tipoContenedor,
                              par$cod_cultura          in gepr_pcomon_###VERSION###.tipo$cod_);

  /* fcargar_rc_precintos */
  FUNCTION fcargar_rc_precintos(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

END SAPR_PCONTENEDOR_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PCONTENEDOR_###VERSION### AS

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pcontenedor_###VERSION###';

  PROCEDURE sdesarmar_contenedor(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                 par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                 par$oid_delegacion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_planta             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_sector             IN gepr_pcomon_###VERSION###.tipo$oid_
                                 /* arrays asociativos contenedor */,
                                 par$acosoid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$acoscod_precinto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$codcultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$oid_documento      out gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$info_ejecucion     IN VARCHAR2,
                                 par$hacer_commit       IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err         VARCHAR2(4000);
    var$inicio      TIMESTAMP;

    var$contenedores_cursor    gepr_pcomon_###VERSION###.tipo$contenedores;
    var$efec_mp_cont_cursor    gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores;
    var$precintos_contenedores gepr_pcomon_###VERSION###.tipo$precintos_contenedores;
    var$lst_val_cnt            gepr_pcomon_###VERSION###.tipo$lst_val_contenedores;

    var$es_doc_agrupador gepr_pcomon_###VERSION###.tipo$nbol_;

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sdesarmar_contenedor';

  BEGIN

    var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
    par$deletes := 0;
    par$selects := 0;

    /*  svalidar_desarmar_contenedor(par$cod_formulario => par$cod_formulario,
    par$oid_delegacion => par$oid_delegacion,
    par$oid_planta => par$oid_planta,
    par$oid_sector => par$oid_sector);*/

    -- buscar contenedores
    srecuperar_contenedores(par$oids_contenedores  => par$acosoid_contenedor,
                            par$cods_precintos     => par$acoscod_precinto,
                            par$rc_contenedores    => var$contenedores_cursor,
                            par$rc_detalle_efec_mp => var$efec_mp_cont_cursor,
                            par$rc_precintos       => var$precintos_contenedores,
                            par$rc_lst_val_cont    => var$lst_val_cnt,
                            par$inserts            => var$inserts_int,
                            par$selects            => var$selects_int);

    par$inserts := par$inserts + var$inserts_int;
    par$selects := par$selects + var$selects_int;

    var$es_doc_agrupador := fverificar_documento_agrupador(par$rc_contenedores => var$contenedores_cursor);

    IF (var$es_doc_agrupador = 0) THEN

      screar_parametros_documento(par$cod_formulario         => par$cod_formulario,
                                  par$fyh_plan_certificacion => par$fyh_plan_certificacion,
                                  par$fyh_gestion            => par$fyh_gestion,

                                  par$oid_delegacion_origen     => par$oid_delegacion,
                                  par$oid_planta_origen         => par$oid_planta,
                                  par$oid_sector_origen         => par$oid_sector,
                                  par$oid_delegacion_destino    => null,
                                  par$oid_planta_destino        => null,
                                  par$oid_sector_destino        => null,
                                  par$oid_cliente_destino       => null,
                                  par$oid_subcliente_destino    => null,
                                  par$oid_puntoservicio_destino => null,
                                  par$usuario                   => par$usuario,
                                  par$codcultura                => par$codcultura,
                                  par$cod_tipo_doc              => const$CodTipoDocDesarmar,
                                  par$rc_contenedores           => var$contenedores_cursor,
                                  par$rc_detalle_efec_mp        => var$efec_mp_cont_cursor,
                                  par$rc_precintos              => var$precintos_contenedores,
                                  par$rc_lst_val_cont           => var$lst_val_cnt,
                                  par$cod_comprobante           => par$cod_comprobante,
                                  par$oid_documento             => par$oid_documento,
                                  par$inserts                   => var$inserts_int,
                                  par$updates                   => var$updates_int,
                                  par$deletes                   => var$deletes_int,
                                  par$selects                   => var$selects_int);

      par$inserts := par$inserts + var$inserts_int;
      par$updates := par$updates + var$updates_int;
      par$deletes := par$deletes + var$deletes_int;
      par$selects := par$selects + var$selects_int;

    ELSE

      screar_parametros_grupo_doc(par$cod_formulario            => par$cod_formulario,
                                  par$fyh_plan_certificacion    => par$fyh_plan_certificacion,
                                  par$fyh_gestion               => par$fyh_gestion,
                                  par$oid_delegacion_origen     => par$oid_delegacion,
                                  par$oid_planta_origen         => par$oid_planta,
                                  par$oid_sector_origen         => par$oid_sector,
                                  par$oid_delegacion_destino    => par$oid_delegacion,
                                  par$oid_planta_destino        => par$oid_planta,
                                  par$oid_sector_destino        => par$oid_sector,
                                  par$oid_cliente_destino       => null,
                                  par$oid_subcliente_destino    => null,
                                  par$oid_puntoservicio_destino => null,
                                  par$usuario                   => par$usuario,
                                  par$codcultura                => par$codcultura,
                                  par$cod_tipo_doc              => const$CodTipoDocDesarmar,
                                  par$rc_contenedores           => var$contenedores_cursor,
                                  par$rc_detalle_efec_mp        => var$efec_mp_cont_cursor,
                                  par$rc_precintos              => var$precintos_contenedores,
                                  par$rc_lst_val_cont           => var$lst_val_cnt,
                                  par$cod_comprobante           => par$cod_comprobante,
                                  par$inserts                   => var$inserts_int,
                                  par$updates                   => var$updates_int,
                                  par$deletes                   => var$deletes_int,
                                  par$selects                   => var$selects_int);

      par$inserts := par$inserts + var$inserts_int;
      par$updates := par$updates + var$updates_int;
      par$deletes := par$deletes + var$deletes_int;
      par$selects := par$selects + var$selects_int;

    END IF;

    var$registros := par$inserts + par$updates + par$deletes + par$selects;

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

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$registros := par$inserts + par$updates + par$deletes +
                       par$selects;

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

  END sdesarmar_contenedor;

  FUNCTION fverificar_documento_agrupador(par$rc_contenedores IN gepr_pcomon_###VERSION###.tipo$contenedores)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS

    var$contador      gepr_pcomon_###VERSION###.tipo$cant_;
    var$rowContenedor gepr_pcomon_###VERSION###.tipo$contenedor;

  BEGIN

    var$contador := 0;

    IF par$rc_contenedores IS NOT NULL and par$rc_contenedores.count > 1 THEN

      var$contador := 1;

    END IF;

    return var$contador;
  END fverificar_documento_agrupador;

  PROCEDURE screar_parametros_grupo_doc(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$oid_delegacion_origen  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_origen      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_origen      IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$oid_delegacion_destino IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_destino     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_destino     IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$oid_cliente_destino       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_subcliente_destino    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_puntoservicio_destino in gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$codcultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_tipo_doc       in gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$rc_contenedores    IN gepr_pcomon_###VERSION###.tipo$contenedores,
                                        par$rc_detalle_efec_mp IN gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores,
                                        par$rc_precintos       IN gepr_pcomon_###VERSION###.tipo$precintos_contenedores,
                                        par$rc_lst_val_cont    IN gepr_pcomon_###VERSION###.tipo$lst_val_contenedores,
                                        par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$oid_grupo_documento       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_grupo_documento_padre gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_formulario_grupo      gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_formulario_grupo      gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_sector_origen         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_destino        gepr_pcomon_###VERSION###.tipo$oid_;
    var$usuario                   gepr_pcomon_###VERSION###.tipo$usr_;
    var$rowver                    gepr_pcomon_###VERSION###.tipo$nel_;

    var$inserts       gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates       gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes       gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects       gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$cod_ejecucion gepr_pcomon_###VERSION###.tipo$nel_;

    -- arrays asociativos
    -- arrays de documentos
    var$adocs_oid            gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_doc_padre  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_sustituto  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_mov_fondos gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_fyh_plncertif  gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_fyh_gestion    gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adocs_cod_externo    gepr_pcomon_###VERSION###.tipo$cods_;
    var$adocs_oid_documento  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_rowver         gepr_pcomon_###VERSION###.tipo$nels_;

    -- cuenta origen
    var$adocs_oid_cuenta_ori     gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_cliente_ori    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_subcliente_ori gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_ptservicio_ori gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_canal_ori      gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_subcanal_ori   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_delegacion_ori gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_planta_ori     gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_sector_ori     gepr_pcomon_###VERSION###.tipo$oids_;

    -- cuenta destino
    var$adocs_oid_cuenta_des     gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_cliente_des    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_subcliente_des gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_ptservicio_des gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_canal_des      gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_subcanal_des   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_delegacion_des gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_planta_des     gepr_pcomon_###VERSION###.tipo$oids_;
    var$adocs_oid_sector_des     gepr_pcomon_###VERSION###.tipo$oids_;

    -- arrays de efectivo por documento
    var$aefdoc_oid_documento    gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_divisa       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_denominacion gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_oid_unid_medida  gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_cod_niv_detalle  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_cod_tp_efec_tot  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefdoc_oid_calidad      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefdoc_num_importe      gepr_pcomon_###VERSION###.tipo$imps_;
    var$aefdoc_nel_cantidad     gepr_pcomon_###VERSION###.tipo$nels_;

    -- arrays de medio de pago por documento
    var$ampdoc_oid_documento     gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_oid_divisa        gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_oid_medio_pago    gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_cod_tipo_med_pago gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_cod_nivel_detalle gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_num_importe       gepr_pcomon_###VERSION###.tipo$imps_;
    var$ampdoc_nel_cantidad      gepr_pcomon_###VERSION###.tipo$nels_;
    var$ampdoc_oid_unidad_medida gepr_pcomon_###VERSION###.tipo$oids_;

    -- arrays de terminos de medio de pago por documento
    var$avtmpdoc_oid_documento   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_oid_mediopago   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_oid_t_mediopago gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_des_valor       gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtmpdoc_nec_indice_grp  gepr_pcomon_###VERSION###.tipo$ints_;

    -- arrays de terminos por documento
    var$avtdoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_oid_termino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_des_valor     gepr_pcomon_###VERSION###.tipo$descs_;

    -- contenedores
    var$aconts_oid_documento       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aconts_oid_contenedor      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aconts_oid_tipo_contenedor gepr_pcomon_###VERSION###.tipo$oids_;
    var$aconts_cod_tipo_servicio   gepr_pcomon_###VERSION###.tipo$cods_;
    var$aconts_cod_tipo_embalaje   gepr_pcomon_###VERSION###.tipo$cods_;
    var$aconts_cod_puesto          gepr_pcomon_###VERSION###.tipo$cods_;
    var$aconts_rowver_contenedor   gepr_pcomon_###VERSION###.tipo$nels_;

    -- Precintos do contenedor
    var$aprco_oid_contenedor       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aprco_precintos_contenedor gepr_pcomon_###VERSION###.tipo$cods_;

    var$aelco_oid_contenedor gepr_pcomon_###VERSION###.tipo$oids_;
    var$aelco_tipo_elemento  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aelco_precintos_ele  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aelco_ids_elemento   gepr_pcomon_###VERSION###.tipo$oids_;

    -- arrays de efectivo por contenedor
    var$aefcnt_oid_contenedor   gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefcnt_oid_divisa       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefcnt_oid_denominacion gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefcnt_oid_unid_medida  gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefcnt_cod_niv_detalle  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefcnt_cod_tp_efec_tot  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefcnt_oid_calidad      gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefcnt_num_importe      gepr_pcomon_###VERSION###.tipo$imps_;
    var$aefcnt_nel_cantidad     gepr_pcomon_###VERSION###.tipo$nels_;

    -- arrays de medio de pago por contenedor
    var$ampcnt_oid_contenedor    gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampcnt_oid_divisa        gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampcnt_oid_medio_pago    gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampcnt_cod_tipo_med_pago gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampcnt_cod_nivel_detalle gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampcnt_num_importe       gepr_pcomon_###VERSION###.tipo$imps_;
    var$ampcnt_nel_cantidad      gepr_pcomon_###VERSION###.tipo$nels_;

    -- arrays de terminos de medio de pago por contenedor
    var$avtmpcnt_oid_contenedor  gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpcnt_oid_t_mediopago gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpcnt_des_valor       gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtmpcnt_nec_indice_grp  gepr_pcomon_###VERSION###.tipo$ints_;

    -- arrays de terminos por contenedor
    var$avtcnt_oid_contenedor gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtcnt_oid_termino    gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtcnt_des_valor      gepr_pcomon_###VERSION###.tipo$descs_;

    -- arrays de terminos del grupo de documentos
    var$avtgdoc_oid_termino gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtgdoc_des_valor   gepr_pcomon_###VERSION###.tipo$descs_;

    -- informacion de ejecucion
    var$info_ejecucion VARCHAR2(4000);

    -- id documento referencia para os arrays
    var$oid_documento gepr_pcomon_###VERSION###.tipo$oid_;

    -- tipos contenedor e valores
    var$row_contenedor         gepr_pcomon_###VERSION###.tipo$contenedor;
    var$row_efec_mp_contenedor gepr_pcomon_###VERSION###.tipo$efectivo_mp_contenedor;
    var$row_precinto           gepr_pcomon_###VERSION###.tipo$precinto_contenedor;
    var$row_lista_valor        gepr_pcomon_###VERSION###.tipo$lst_valor_contenedor;

    -- indices para contenedores e arrays usados no loop
    var$idx_efec gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$idx_mp   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$idx_prc  gepr_pcomon_###VERSION###.tipo$nel_ := 0;

  BEGIN

    var$oid_grupo_documento       := SYS_GUID();
    var$oid_grupo_documento_padre := NULL;
    var$oid_formulario_grupo      := NULL;
    var$cod_formulario_grupo      := par$cod_formulario;
    var$oid_sector_origen         := par$oid_sector_origen;
    var$oid_sector_destino        := par$oid_sector_destino;
    var$usuario                   := par$usuario;
    var$rowver                    := 0;

    FOR idx_cont IN par$rc_contenedores.FIRST .. par$rc_contenedores.LAST LOOP

      var$row_contenedor := par$rc_contenedores(idx_cont);
      var$oid_documento  := SYS_GUID();

      var$adocs_oid(idx_cont) := var$oid_documento;
      var$adocs_oid_doc_padre(idx_cont) := NULL;
      var$adocs_oid_sustituto(idx_cont) := NULL;
      var$adocs_oid_mov_fondos(idx_cont) := NULL;
      var$adocs_fyh_plncertif(idx_cont) := par$fyh_plan_certificacion;
      var$adocs_fyh_gestion(idx_cont) := par$fyh_gestion;
      var$adocs_cod_externo(idx_cont) := NULL;
      var$adocs_oid_documento(idx_cont) := var$oid_documento;
      var$adocs_rowver(idx_cont) := null;

      -- cuenta origen
      var$adocs_oid_cuenta_ori(idx_cont) := var$row_contenedor.OID_CUENTA_MOVIMIENTO;
      var$adocs_oid_cliente_ori(idx_cont) := var$row_contenedor.OID_CLIENTE;
      var$adocs_oid_subcliente_ori(idx_cont) := var$row_contenedor.OID_SUBCLIENTE;
      var$adocs_oid_ptservicio_ori(idx_cont) := var$row_contenedor.OID_PTO_SERVICIO;
      var$adocs_oid_canal_ori(idx_cont) := var$row_contenedor.OID_CANAL;
      var$adocs_oid_subcanal_ori(idx_cont) := var$row_contenedor.OID_SUBCANAL;
      var$adocs_oid_delegacion_ori(idx_cont) := var$row_contenedor.OID_DELEGACION;
      var$adocs_oid_planta_ori(idx_cont) := var$row_contenedor.OID_PLANTA;
      var$adocs_oid_sector_ori(idx_cont) := var$row_contenedor.OID_SECTOR;

      -- cuenta destino
      var$adocs_oid_cuenta_des(idx_cont) := var$row_contenedor.OID_CUENTA_MOVIMIENTO;
      var$adocs_oid_cliente_des(idx_cont) := var$row_contenedor.OID_CLIENTE;
      var$adocs_oid_subcliente_des(idx_cont) := var$row_contenedor.OID_SUBCLIENTE;
      var$adocs_oid_ptservicio_des(idx_cont) := var$row_contenedor.OID_PTO_SERVICIO;
      var$adocs_oid_canal_des(idx_cont) := var$row_contenedor.OID_CANAL;
      var$adocs_oid_subcanal_des(idx_cont) := var$row_contenedor.OID_SUBCANAL;
      var$adocs_oid_delegacion_des(idx_cont) := var$row_contenedor.OID_DELEGACION;
      var$adocs_oid_planta_des(idx_cont) := var$row_contenedor.OID_PLANTA;
      var$adocs_oid_sector_des(idx_cont) := var$row_contenedor.OID_SECTOR;

      if par$cod_tipo_doc = const$CodTipoDocDesarmar then

        /* cuenta destino */
        var$adocs_oid_cuenta_des(idx_cont) := var$row_contenedor.OID_CUENTA_MOVIMIENTO;
        var$adocs_oid_cliente_des(idx_cont) := var$row_contenedor.OID_CLIENTE;
        var$adocs_oid_subcliente_des(idx_cont) := var$row_contenedor.OID_SUBCLIENTE;
        var$adocs_oid_ptservicio_des(idx_cont) := var$row_contenedor.OID_PTO_SERVICIO;
        var$adocs_oid_canal_des(idx_cont) := var$row_contenedor.OID_CANAL;
        var$adocs_oid_subcanal_des(idx_cont) := var$row_contenedor.OID_SUBCANAL;
        var$adocs_oid_delegacion_des(idx_cont) := var$row_contenedor.OID_DELEGACION;
        var$adocs_oid_planta_des(idx_cont) := var$row_contenedor.OID_PLANTA;
        var$adocs_oid_sector_des(idx_cont) := var$row_contenedor.OID_SECTOR;

      elsif par$cod_tipo_doc = const$CodTipoDocReenvioSector then

        /* cuenta destino */
        var$adocs_oid_cuenta_des(idx_cont) := null;
        var$adocs_oid_cliente_des(idx_cont) := var$row_contenedor.OID_CLIENTE;
        var$adocs_oid_subcliente_des(idx_cont) := var$row_contenedor.OID_SUBCLIENTE;
        var$adocs_oid_ptservicio_des(idx_cont) := var$row_contenedor.OID_PTO_SERVICIO;
        var$adocs_oid_canal_des(idx_cont) := var$row_contenedor.OID_CANAL;
        var$adocs_oid_subcanal_des(idx_cont) := var$row_contenedor.OID_SUBCANAL;
        var$adocs_oid_delegacion_des(idx_cont) := par$oid_delegacion_destino;
        var$adocs_oid_planta_des(idx_cont) := par$oid_planta_destino;
        var$adocs_oid_sector_des(idx_cont) := par$oid_sector_destino;

        /* Removendo contenedor da posição */
        update sapr_tposicion_sector ps
           set ps.oid_contenedor = null
         where ps.oid_contenedor = var$row_contenedor.OID_CONTENEDOR;

      elsif par$cod_tipo_doc = const$CodTipoDocReenvioCliente then

        /* cuenta destino */
        var$adocs_oid_cuenta_des(idx_cont) := null;
        var$adocs_oid_cliente_des(idx_cont) := par$oid_cliente_destino;
        var$adocs_oid_subcliente_des(idx_cont) := par$oid_subcliente_destino;
        var$adocs_oid_ptservicio_des(idx_cont) := par$oid_puntoservicio_destino;
        var$adocs_oid_canal_des(idx_cont) := var$row_contenedor.OID_CANAL;
        var$adocs_oid_subcanal_des(idx_cont) := var$row_contenedor.OID_SUBCANAL;
        var$adocs_oid_delegacion_des(idx_cont) := var$row_contenedor.OID_DELEGACION;
        var$adocs_oid_planta_des(idx_cont) := var$row_contenedor.OID_PLANTA;
        var$adocs_oid_sector_des(idx_cont) := var$row_contenedor.OID_SECTOR;

      end if;

      -- contenedores
      var$aconts_oid_documento(idx_cont) := var$oid_documento;
      var$aconts_oid_contenedor(idx_cont) := var$row_contenedor.OID_CONTENEDOR;
      var$aconts_oid_tipo_contenedor(idx_cont) := var$row_contenedor.OID_TIPO_CONTENEDOR;
      var$aconts_cod_puesto(idx_cont) := var$row_contenedor.COD_PUESTO;

      IF (par$rc_lst_val_cont IS NOT NULL AND par$rc_lst_val_cont.COUNT > 0) THEN

        FOR idxListaVal IN par$rc_lst_val_cont.FIRST .. par$rc_lst_val_cont.LAST LOOP

          if par$rc_lst_val_cont(idxListaVal)
           .OID_CONTENEDOR = var$row_contenedor.OID_CONTENEDOR THEN

            var$row_lista_valor := par$rc_lst_val_cont(idxListaVal);
            IF (var$row_lista_valor.COD_TIPO =
               gepr_pcomon_###VERSION###.const$codListaTipoTrabajo) THEN
              var$aconts_cod_tipo_servicio(idx_cont) := var$row_lista_valor.COD_VALOR;

            ELSIF (var$row_lista_valor.COD_TIPO =
                  gepr_pcomon_###VERSION###.const$codListaTipoFormato) THEN
              var$aconts_cod_tipo_embalaje(idx_cont) := var$row_lista_valor.COD_VALOR;

            END IF;

          else
            var$aconts_cod_tipo_servicio(idx_cont) := null;
            var$aconts_cod_tipo_embalaje(idx_cont) := null;
          END IF;

        END LOOP;

      else
        var$aconts_cod_tipo_servicio(idx_cont) := null;
        var$aconts_cod_tipo_embalaje(idx_cont) := null;
      END IF;
      -- inicia indice da lista de precintos
      --var$idx_prc := 0;

      FOR idxPrecinto IN par$rc_precintos.FIRST .. par$rc_precintos.LAST LOOP

        var$row_precinto := par$rc_precintos(idxPrecinto);

        IF (var$row_precinto.OID_CONTENEDOR =
           var$row_contenedor.OID_CONTENEDOR) THEN

          var$aprco_oid_contenedor(var$idx_prc) := var$row_precinto.OID_CONTENEDOR;
          var$aprco_precintos_contenedor(var$idx_prc) := var$row_precinto.COD_PRECINTO;

          var$idx_prc := var$idx_prc + 1;

        END IF;

      END LOOP;

      --var$idx_efec := 0;
      --var$idx_mp := 0;

      if par$rc_detalle_efec_mp is not null and
         par$rc_detalle_efec_mp.count > 0 then

        FOR idx_efe_mp IN par$rc_detalle_efec_mp.FIRST .. par$rc_detalle_efec_mp.LAST LOOP

          var$row_efec_mp_contenedor := par$rc_detalle_efec_mp(idx_efe_mp);

          IF (var$row_efec_mp_contenedor.OID_CONTENEDOR =
             var$row_contenedor.OID_CONTENEDOR) THEN

            -- arrays de efectivo do contenedor
            IF (var$row_efec_mp_contenedor.COD_TIPO_MEDIO_PAGO IS NULL) THEN

              var$aefcnt_oid_contenedor(var$idx_efec) := var$row_contenedor.OID_CONTENEDOR;
              var$aefcnt_oid_divisa(var$idx_efec) := var$row_efec_mp_contenedor.OID_DIVISA;
              var$aefcnt_oid_denominacion(var$idx_efec) := var$row_efec_mp_contenedor.OID_DENOMINACION;
              var$aefcnt_oid_unid_medida(var$idx_efec) := var$row_efec_mp_contenedor.OID_UNIDAD_MEDIDA;
              var$aefcnt_cod_niv_detalle(var$idx_efec) := var$row_efec_mp_contenedor.COD_NIVEL_DETALLE;
              var$aefcnt_cod_tp_efec_tot(var$idx_efec) := NULL;
              var$aefcnt_oid_calidad(var$idx_efec) := var$row_efec_mp_contenedor.OID_CALIDAD;
              var$aefcnt_num_importe(var$idx_efec) := var$row_efec_mp_contenedor.NUM_IMPORTE;
              var$aefcnt_nel_cantidad(var$idx_efec) := var$row_efec_mp_contenedor.NEL_CANTIDAD;

              var$aefdoc_oid_documento(var$idx_efec) := var$oid_documento;
              var$aefdoc_oid_divisa(var$idx_efec) := var$row_efec_mp_contenedor.OID_DIVISA;
              var$aefdoc_oid_denominacion(var$idx_efec) := var$row_efec_mp_contenedor.OID_DENOMINACION;
              var$aefdoc_oid_unid_medida(var$idx_efec) := var$row_efec_mp_contenedor.OID_UNIDAD_MEDIDA;
              var$aefdoc_cod_niv_detalle(var$idx_efec) := var$row_efec_mp_contenedor.COD_NIVEL_DETALLE;
              var$aefdoc_cod_tp_efec_tot(var$idx_efec) := NULL;
              var$aefdoc_oid_calidad(var$idx_efec) := var$row_efec_mp_contenedor.OID_CALIDAD;
              var$aefdoc_num_importe(var$idx_efec) := var$row_efec_mp_contenedor.NUM_IMPORTE;
              var$aefdoc_nel_cantidad(var$idx_efec) := var$row_efec_mp_contenedor.NEL_CANTIDAD;

              var$idx_efec := var$idx_efec + 1;

            ELSE
              -- arrays de medio de pago por documento

              var$ampcnt_oid_contenedor(var$idx_mp) := var$row_contenedor.OID_CONTENEDOR;
              var$ampcnt_oid_divisa(var$idx_mp) := var$row_efec_mp_contenedor.OID_DIVISA;
              var$ampcnt_oid_medio_pago(var$idx_mp) := var$row_efec_mp_contenedor.OID_MEDIO_PAGO;
              var$ampcnt_cod_tipo_med_pago(var$idx_mp) := var$row_efec_mp_contenedor.COD_TIPO_MEDIO_PAGO;
              var$ampcnt_cod_nivel_detalle(var$idx_mp) := var$row_efec_mp_contenedor.COD_NIVEL_DETALLE;
              var$ampcnt_num_importe(var$idx_mp) := var$row_efec_mp_contenedor.NUM_IMPORTE;
              var$ampcnt_nel_cantidad(var$idx_mp) := var$row_efec_mp_contenedor.NEL_CANTIDAD;

              var$ampdoc_oid_documento(var$idx_mp) := var$oid_documento;
              var$ampdoc_oid_divisa(var$idx_mp) := var$row_efec_mp_contenedor.OID_DIVISA;
              var$ampdoc_oid_medio_pago(var$idx_mp) := var$row_efec_mp_contenedor.OID_MEDIO_PAGO;
              var$ampdoc_cod_tipo_med_pago(var$idx_mp) := var$row_efec_mp_contenedor.COD_TIPO_MEDIO_PAGO;
              var$ampdoc_cod_nivel_detalle(var$idx_mp) := var$row_efec_mp_contenedor.COD_NIVEL_DETALLE;
              var$ampdoc_num_importe(var$idx_mp) := var$row_efec_mp_contenedor.NUM_IMPORTE;
              var$ampdoc_nel_cantidad(var$idx_mp) := var$row_efec_mp_contenedor.NEL_CANTIDAD;

              var$idx_mp := var$idx_mp + 1;

            END IF;

          END IF;

        END LOOP;

      end if;

    END LOOP;

    -- Call the procedure
    sapr_pdocumentos_grp_###VERSION###.sguardar_grp_docs_cont(par$oid_grupo_documento       => var$oid_grupo_documento,
                                                         par$oid_grupo_documento_padre => var$oid_grupo_documento_padre,
                                                         par$oid_formulario_grupo      => null,
                                                         par$cod_formulario_grupo      => par$cod_formulario,
                                                         par$oid_sector_origen         => par$oid_sector_origen,
                                                         par$oid_sector_destino        => par$oid_sector_destino,
                                                         par$cod_comprobante           => par$cod_comprobante,
                                                         par$rowver                    => null,
                                                         par$avtgdoc_oid_termino       => var$avtgdoc_oid_termino,
                                                         par$avtgdoc_des_valor         => var$avtgdoc_des_valor,

                                                         par$adoc_oid_documento       => var$adocs_oid_documento,
                                                         par$adoc_oid_documento_padre => var$adocs_oid_doc_padre,
                                                         par$adoc_oid_documento_sust  => var$adocs_oid_sustituto,
                                                         par$adoc_oid_moviment_fondo  => var$adocs_oid_mov_fondos,
                                                         par$adoc_fyh_plan_certif     => var$adocs_fyh_plncertif,
                                                         par$adoc_fyh_gestion         => var$adocs_fyh_gestion,
                                                         par$adoc_cod_externo         => var$adocs_cod_externo,
                                                         par$adoc_rowver              => var$adocs_rowver,

                                                         par$adoc_oid_cuenta_origen     => var$adocs_oid_cuenta_ori,
                                                         par$adoc_oid_cliente_origen    => var$adocs_oid_cliente_ori,
                                                         par$adoc_oid_subcliente_origen => var$adocs_oid_subcliente_ori,
                                                         par$adoc_oid_puntoservico_ori  => var$adocs_oid_ptservicio_ori,
                                                         par$adoc_oid_canal_origen      => var$adocs_oid_canal_ori,
                                                         par$adoc_oid_subcanal_origen   => var$adocs_oid_subcanal_ori,
                                                         par$adoc_oid_delegacion_origen => var$adocs_oid_delegacion_ori,
                                                         par$adoc_oid_planta_origen     => var$adocs_oid_planta_ori,
                                                         par$adoc_oid_sector_origen     => var$adocs_oid_sector_ori,

                                                         par$adoc_oid_cuenta_destino   => var$adocs_oid_cuenta_des,
                                                         par$adoc_oid_cliente_destino  => var$adocs_oid_cliente_des,
                                                         par$adoc_oid_subcliente_des   => var$adocs_oid_subcliente_des,
                                                         par$adoc_oid_puntoservico_des => var$adocs_oid_ptservicio_des,
                                                         par$adoc_oid_canal_destino    => var$adocs_oid_canal_des,
                                                         par$adoc_oid_subcanal_destino => var$adocs_oid_subcanal_des,
                                                         par$adoc_oid_delegacion_des   => var$adocs_oid_delegacion_des,
                                                         par$adoc_oid_planta_destino   => var$adocs_oid_planta_des,
                                                         par$adoc_oid_sector_destino   => var$adocs_oid_sector_des,

                                                         par$aefdoc_oid_documento    => var$aefdoc_oid_documento,
                                                         par$aefdoc_oid_divisa       => var$aefdoc_oid_divisa,
                                                         par$aefdoc_oid_denominacion => var$aefdoc_oid_denominacion,
                                                         par$aefdoc_oid_unid_medida  => var$aefdoc_oid_unid_medida,
                                                         par$aefdoc_cod_niv_detalle  => var$aefdoc_cod_niv_detalle,
                                                         par$aefdoc_cod_tp_efec_tot  => var$aefdoc_cod_tp_efec_tot,
                                                         par$aefdoc_oid_calidad      => var$aefdoc_oid_calidad,
                                                         par$aefdoc_num_importe      => var$aefdoc_num_importe,
                                                         par$aefdoc_nel_cantidad     => var$aefdoc_nel_cantidad,

                                                         par$ampdoc_oid_documento     => var$ampdoc_oid_documento,
                                                         par$ampdoc_oid_divisa        => var$ampdoc_oid_divisa,
                                                         par$ampdoc_oid_medio_pago    => var$ampdoc_oid_medio_pago,
                                                         par$ampdoc_cod_tipo_med_pago => var$ampdoc_cod_tipo_med_pago,
                                                         par$ampdoc_cod_nivel_detalle => var$ampdoc_cod_nivel_detalle,
                                                         par$ampdoc_num_importe       => var$ampdoc_num_importe,
                                                         par$ampdoc_nel_cantidad      => var$ampdoc_nel_cantidad,
                                                         par$ampdoc_oid_unidad_medida => var$ampdoc_oid_unidad_medida,

                                                         par$avtmpdoc_oid_documento   => var$avtmpdoc_oid_documento,
                                                         par$avtmpdoc_oid_mediopago   => var$avtmpdoc_oid_mediopago,
                                                         par$avtmpdoc_oid_t_mediopago => var$avtmpdoc_oid_t_mediopago,
                                                         par$avtmpdoc_des_valor       => var$avtmpdoc_des_valor,
                                                         par$avtmpdoc_nec_indice_grp  => var$avtmpdoc_nec_indice_grp,

                                                         par$adoccont_oid_documento     => var$aconts_oid_documento,
                                                         par$adoccont_oid_contenedor    => var$aconts_oid_contenedor,
                                                         par$adoccont_rowver_contenedor => var$aconts_rowver_contenedor,

                                                         par$aelco_oid_contenedor => var$aelco_oid_contenedor,
                                                         par$aelco_tipo_elemento  => var$aelco_tipo_elemento,
                                                         par$aelco_precintos_ele  => var$aelco_precintos_ele,
                                                         par$aelco_ids_elemento   => var$aelco_ids_elemento,

                                                         par$usuario        => par$usuario,
                                                         par$cod_cultura    => par$codcultura,
                                                         par$info_ejecucion => var$info_ejecucion,
                                                         par$hacer_commit   => 1,
                                                         par$confirmar_doc  => 1,
                                                         par$inserts        => var$inserts,
                                                         par$updates        => var$updates,
                                                         par$deletes        => var$deletes,
                                                         par$selects        => var$selects,
                                                         par$cod_ejecucion  => var$cod_ejecucion);

    par$inserts := par$inserts + var$inserts;
    par$updates := par$updates + var$updates;
    par$deletes := par$deletes + var$deletes;
    par$selects := par$selects + var$selects;

  END screar_parametros_grupo_doc;


  PROCEDURE screar_parametros_documento(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_,

                                        par$oid_delegacion_origen IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_origen     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_origen     IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$oid_delegacion_destino IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_destino     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_destino     IN gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$oid_cliente_destino       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_subcliente_destino    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_puntoservicio_destino in gepr_pcomon_###VERSION###.tipo$oid_,

                                        par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                        par$codcultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_tipo_doc       in gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$rc_contenedores    IN gepr_pcomon_###VERSION###.tipo$contenedores,
                                        par$rc_detalle_efec_mp IN gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores,
                                        par$rc_precintos       IN gepr_pcomon_###VERSION###.tipo$precintos_contenedores,
                                        par$rc_lst_val_cont    IN gepr_pcomon_###VERSION###.tipo$lst_val_contenedores,
                                        par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_documento      OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$adoc_oid_documento       gepr_pcomon_###VERSION###.tipo$oids_;
    var$oid_formulario           gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_formulario           gepr_pcomon_###VERSION###.tipo$cod_;
    var$adoc_oid_documento_padre gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_documento_sust  gepr_pcomon_###VERSION###.tipo$oids_;
    var$oid_grupo_documento      gepr_pcomon_###VERSION###.tipo$oid_;
    var$adoc_oid_mov_fondo       gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_fyh_plan_certif     gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adoc_fyh_gestion         gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adoc_cod_externo         gepr_pcomon_###VERSION###.tipo$cods_;
    var$adoc_cod_comprobante     gepr_pcomon_###VERSION###.tipo$cods_;
    var$adoc_rowver              gepr_pcomon_###VERSION###.tipo$nels_;

    /* cuenta origen */
    var$adoc_oid_cuenta_origen     gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_cliente_origen    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_subcliente_origen gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_puntoservico_ori  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_canal_origen      gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_subcanal_origen   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_delegacion_origen gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_planta_origen     gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_sector_origen     gepr_pcomon_###VERSION###.tipo$oids_;

    /* cuenta destino */
    var$adoc_oid_cuenta_destino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_cliente_destino  gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_subcliente_des   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_puntoservico_des gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_canal_destino    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_subcanal_destino gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_delegacion_des   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_planta_destino   gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoc_oid_sector_destino   gepr_pcomon_###VERSION###.tipo$oids_;

    /* arrays asociativos */
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
    var$ampdoc_oid_uni_med       gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampdoc_cod_tipo_med_pago gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_cod_nivel_detalle gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampdoc_num_importe       gepr_pcomon_###VERSION###.tipo$imps_;
    var$ampdoc_nel_cantidad      gepr_pcomon_###VERSION###.tipo$nels_;

    /* arrays de terminos de medio de pago por documento */
    var$avtmpdoc_oid_medio_pago  gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_oid_t_mediopago gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpdoc_des_valor       gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtmpdoc_nec_indice_grp  gepr_pcomon_###VERSION###.tipo$ints_;

    /* arrays de terminos por documento */
    var$avtmpdoc_oid_documento gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_oid_termino     gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtdoc_des_valor       gepr_pcomon_###VERSION###.tipo$descs_;

    -- contenedores
    var$adoccont_oid_documento     gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoccont_oid_contenedor    gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoccont_oid_tipo_cont     gepr_pcomon_###VERSION###.tipo$oids_;
    var$adoccont_cod_tipo_cont     gepr_pcomon_###VERSION###.tipo$cods_;
    var$adoccont_cod_tipo_servicio gepr_pcomon_###VERSION###.tipo$cods_;
    var$adoccont_cod_tipo_embalaje gepr_pcomon_###VERSION###.tipo$cods_;
    var$adoccont_cod_puesto        gepr_pcomon_###VERSION###.tipo$cods_;
    var$adoccont_rowver_contenedor gepr_pcomon_###VERSION###.tipo$nels_;
    -- Precintos
    var$aprco_oid_contenedor       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aprco_precintos_contenedor gepr_pcomon_###VERSION###.tipo$cods_;
    -- Elementos
    var$aelco_oid_contenedor gepr_pcomon_###VERSION###.tipo$oids_;
    var$aelco_tipo_elemento  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aelco_precintos_ele  gepr_pcomon_###VERSION###.tipo$cods_;
    var$aelco_ids_elemento   gepr_pcomon_###VERSION###.tipo$oids_;
    /* arrays de efectivo do contenedor */
    var$aefco_oid_divisa        gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefco_oid_denom         gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefco_oid_uni_med       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefco_cod_nivel_det     gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefco_cod_tipo_efec_tot gepr_pcomon_###VERSION###.tipo$cods_;
    var$aefco_oid_calidad       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefco_num_importe       gepr_pcomon_###VERSION###.tipo$imps_;
    var$aefco_nel_cantidad      gepr_pcomon_###VERSION###.tipo$nels_;

    /* arrays de medio de pago do contenedor */
    var$ampco_oid_divisa    gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampco_oid_mp        gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampco_oid_uni_med   gepr_pcomon_###VERSION###.tipo$oids_;
    var$ampco_cod_tipo_mp   gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampco_cod_nivel_det gepr_pcomon_###VERSION###.tipo$cods_;
    var$ampco_num_importe   gepr_pcomon_###VERSION###.tipo$imps_;
    var$ampco_nel_cantidad  gepr_pcomon_###VERSION###.tipo$nels_;

    /* arrays de terminos de medio de pago do contenedor */
    var$avtmpco_oid_val_ter_mp   gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpco_oid_mpxdoc       gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpco_oid_termediopago gepr_pcomon_###VERSION###.tipo$oids_;
    var$avtmpco_des_valor        gepr_pcomon_###VERSION###.tipo$descs_;
    var$avtmpco_nec_indicegrupo  gepr_pcomon_###VERSION###.tipo$ints_;

    /* informacion de ejecucion */
    var$usuario        gepr_pcomon_###VERSION###.tipo$usr_;
    var$cod_cultura    gepr_pcomon_###VERSION###.tipo$cod_;
    var$info_ejecucion VARCHAR2(32767);
    var$hacer_commit   gepr_pcomon_###VERSION###.tipo$nbol_;
    var$inserts        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$cod_ejecucion  gepr_pcomon_###VERSION###.tipo$nel_;

    -- tipos RECORD para contenedor e valores - usados nos loop's
    var$row_contenedor         gepr_pcomon_###VERSION###.tipo$contenedor;
    var$row_efec_mp_contenedor gepr_pcomon_###VERSION###.tipo$efectivo_mp_contenedor;
    var$row_precinto           gepr_pcomon_###VERSION###.tipo$precinto_contenedor;
    var$row_lista_valor        gepr_pcomon_###VERSION###.tipo$lst_valor_contenedor;

    -- indice para os loop's de Efetivo e Meio de Pago
    var$idx_efec gepr_pcomon_###VERSION###.tipo$nel_;
    var$idx_mp   gepr_pcomon_###VERSION###.tipo$nel_;
    var$idx_prc  gepr_pcomon_###VERSION###.tipo$nel_;

    var$adoc_fyh_contable       gepr_pcomon_###VERSION###.tipo$fyhs_;
    var$adoc_cod_actual_id      gepr_pcomon_###VERSION###.tipo$descs_;
    var$adoc_collection_id      gepr_pcomon_###VERSION###.tipo$descs_;


  BEGIN

    /* Informa??es cabe?alho do documento */
    var$adoc_oid_documento(0) := SYS_GUID();
    par$oid_documento := var$adoc_oid_documento(0);
    var$oid_formulario := NULL;
    var$cod_formulario := par$cod_formulario;
    var$adoc_oid_documento_padre(0) := NULL;
    var$adoc_oid_documento_sust(0) := NULL;
    var$oid_grupo_documento := NULL;
    var$adoc_oid_mov_fondo(0) := NULL;
    var$adoc_fyh_plan_certif(0) := par$fyh_plan_certificacion;
    var$adoc_fyh_gestion(0) := par$fyh_gestion;
    var$adoc_cod_externo(0) := NULL;
    var$adoc_rowver(0) := null;
    var$usuario := par$usuario;
    var$cod_cultura := par$codcultura;

    FOR idxContenedor IN par$rc_contenedores.FIRST .. par$rc_contenedores.LAST LOOP

      var$row_contenedor := par$rc_contenedores(idxContenedor);

      /* cuenta origen */
      var$adoc_oid_cuenta_origen(0) := var$row_contenedor.OID_CUENTA_MOVIMIENTO;
      var$adoc_oid_cliente_origen(0) := var$row_contenedor.OID_CLIENTE;
      var$adoc_oid_subcliente_origen(0) := var$row_contenedor.OID_SUBCLIENTE;
      var$adoc_oid_puntoservico_ori(0) := var$row_contenedor.OID_PTO_SERVICIO;
      var$adoc_oid_canal_origen(0) := var$row_contenedor.OID_CANAL;
      var$adoc_oid_subcanal_origen(0) := var$row_contenedor.OID_SUBCANAL;
      var$adoc_oid_delegacion_origen(0) := var$row_contenedor.OID_DELEGACION;
      var$adoc_oid_planta_origen(0) := var$row_contenedor.OID_PLANTA;
      var$adoc_oid_sector_origen(0) := var$row_contenedor.OID_SECTOR;

      if par$cod_tipo_doc = const$CodTipoDocDesarmar then

        /* cuenta destino */
        var$adoc_oid_cuenta_destino(0) := var$row_contenedor.OID_CUENTA_MOVIMIENTO;
        var$adoc_oid_cliente_destino(0) := var$row_contenedor.OID_CLIENTE;
        var$adoc_oid_subcliente_des(0) := var$row_contenedor.OID_SUBCLIENTE;
        var$adoc_oid_puntoservico_des(0) := var$row_contenedor.OID_PTO_SERVICIO;
        var$adoc_oid_canal_destino(0) := var$row_contenedor.OID_CANAL;
        var$adoc_oid_subcanal_destino(0) := var$row_contenedor.OID_SUBCANAL;
        var$adoc_oid_delegacion_des(0) := var$row_contenedor.OID_DELEGACION;
        var$adoc_oid_planta_destino(0) := var$row_contenedor.OID_PLANTA;
        var$adoc_oid_sector_destino(0) := var$row_contenedor.OID_SECTOR;

      elsif par$cod_tipo_doc = const$CodTipoDocReenvioSector then

        /* cuenta destino */
        var$adoc_oid_cuenta_destino(0) := null;
        var$adoc_oid_cliente_destino(0) := var$row_contenedor.OID_CLIENTE;
        var$adoc_oid_subcliente_des(0) := var$row_contenedor.OID_SUBCLIENTE;
        var$adoc_oid_puntoservico_des(0) := var$row_contenedor.OID_PTO_SERVICIO;
        var$adoc_oid_canal_destino(0) := var$row_contenedor.OID_CANAL;
        var$adoc_oid_subcanal_destino(0) := var$row_contenedor.OID_SUBCANAL;
        var$adoc_oid_delegacion_des(0) := par$oid_delegacion_destino;
        var$adoc_oid_planta_destino(0) := par$oid_planta_destino;
        var$adoc_oid_sector_destino(0) := par$oid_sector_destino;

    /* Removendo contenedor da posição */
        update sapr_tposicion_sector ps
           set ps.oid_contenedor = null
         where ps.oid_contenedor = var$row_contenedor.OID_CONTENEDOR;

      elsif par$cod_tipo_doc = const$CodTipoDocReenvioCliente then

        var$adoc_oid_cuenta_destino(0) := null;
        var$adoc_oid_cliente_destino(0) := par$oid_cliente_destino;
        var$adoc_oid_subcliente_des(0) := par$oid_subcliente_destino;
        var$adoc_oid_puntoservico_des(0) := par$oid_puntoservicio_destino;
        var$adoc_oid_canal_destino(0) := var$row_contenedor.OID_CANAL;
        var$adoc_oid_subcanal_destino(0) := var$row_contenedor.OID_SUBCANAL;
        var$adoc_oid_delegacion_des(0) := var$row_contenedor.OID_DELEGACION;
        var$adoc_oid_planta_destino(0) := var$row_contenedor.OID_PLANTA;
        var$adoc_oid_sector_destino(0) := var$row_contenedor.OID_SECTOR;

      end if;

      IF (par$rc_lst_val_cont IS NOT NULL AND par$rc_lst_val_cont.COUNT > 0) THEN
        FOR idxListaVal IN par$rc_lst_val_cont.FIRST .. par$rc_lst_val_cont.LAST LOOP

          var$row_lista_valor := par$rc_lst_val_cont(idxListaVal);
          IF (var$row_lista_valor.COD_TIPO =
             gepr_pcomon_###VERSION###.const$codListaTipoTrabajo) THEN
            var$adoccont_cod_tipo_servicio(0) := var$row_lista_valor.COD_VALOR;

          ELSIF (var$row_lista_valor.COD_TIPO =
                gepr_pcomon_###VERSION###.const$codListaTipoFormato) THEN
            var$adoccont_cod_tipo_embalaje(0) := var$row_lista_valor.COD_VALOR;

          END IF;

        END LOOP;
      END IF;
      -- contenedores
      --var$adoccont_tipo_contenedor := var$row_contenedor.OID_TIPO_CONTENEDOR;
      var$adoccont_cod_tipo_cont(0) := var$row_contenedor.COD_TIPO_CONTENEDOR;
      var$adoccont_cod_puesto(0) := var$row_contenedor.COD_PUESTO;
      var$adoccont_oid_documento(0) := var$adoc_oid_documento(0);
      var$adoccont_oid_contenedor(0) := var$row_contenedor.OID_CONTENEDOR;
      var$adoccont_rowver_contenedor(0) := null;

      -- inicia indice da lista de precintos
      var$idx_prc := 0;

      FOR idxPrecinto IN par$rc_precintos.FIRST .. par$rc_precintos.LAST LOOP

        var$row_precinto := par$rc_precintos(idxPrecinto);
        var$aprco_oid_contenedor(var$idx_prc) := var$row_contenedor.OID_CONTENEDOR;
        var$aprco_precintos_contenedor(var$idx_prc) := var$row_precinto.COD_PRECINTO;
        var$idx_prc := var$idx_prc + 1;

      END LOOP;

      var$idx_efec := 0;
      var$idx_mp   := 0;

      FOR idxValores IN par$rc_detalle_efec_mp.FIRST .. par$rc_detalle_efec_mp.LAST LOOP
        var$row_efec_mp_contenedor := par$rc_detalle_efec_mp(idxValores);

        /* arrays de efectivo do contenedor */
        IF (var$row_efec_mp_contenedor.COD_TIPO_MEDIO_PAGO IS NULL) THEN

          var$aefdoc_oid_documento(var$idx_efec) := var$adoc_oid_documento(0);
          var$aefco_oid_divisa(var$idx_efec) := var$row_efec_mp_contenedor.OID_DIVISA;
          var$aefco_oid_denom(var$idx_efec) := var$row_efec_mp_contenedor.OID_DENOMINACION;
          var$aefco_oid_uni_med(var$idx_efec) := var$row_efec_mp_contenedor.OID_UNIDAD_MEDIDA;
          var$aefco_cod_nivel_det(var$idx_efec) := var$row_efec_mp_contenedor.COD_NIVEL_DETALLE;
          var$aefco_cod_tipo_efec_tot(var$idx_efec) := NULL;
          var$aefco_oid_calidad(var$idx_efec) := var$row_efec_mp_contenedor.OID_CALIDAD;
          var$aefco_num_importe(var$idx_efec) := var$row_efec_mp_contenedor.NUM_IMPORTE;
          var$aefco_nel_cantidad(var$idx_efec) := var$row_efec_mp_contenedor.NEL_CANTIDAD;

          var$idx_efec := var$idx_efec + 1;

        ELSE

          /* arrays de medio de pago do contenedor */
          var$ampdoc_oid_documento(var$idx_mp) := var$adoc_oid_documento(0);
          var$ampco_oid_divisa(var$idx_mp) := var$row_efec_mp_contenedor.OID_DIVISA;
          var$ampco_oid_uni_med(var$idx_mp) := var$row_efec_mp_contenedor.OID_UNIDAD_MEDIDA;
          var$ampco_oid_mp(var$idx_mp) := var$row_efec_mp_contenedor.OID_MEDIO_PAGO;
          var$ampco_cod_tipo_mp(var$idx_mp) := var$row_efec_mp_contenedor.COD_TIPO_MEDIO_PAGO;
          var$ampco_cod_nivel_det(var$idx_mp) := var$row_efec_mp_contenedor.COD_NIVEL_DETALLE;
          var$ampco_num_importe(var$idx_mp) := var$row_efec_mp_contenedor.NUM_IMPORTE;
          var$ampco_nel_cantidad(var$idx_mp) := var$row_efec_mp_contenedor.NEL_CANTIDAD;

          var$idx_mp := var$idx_mp + 1;

        END IF;

      END LOOP;

    END LOOP;

    var$aefdoc_oid_divisa       := var$aefco_oid_divisa;
    var$aefdoc_oid_denominacion := var$aefco_oid_denom;
    var$aefdoc_oid_unid_medida  := var$aefco_oid_uni_med;
    var$aefdoc_cod_niv_detalle  := var$aefco_cod_nivel_det;
    var$aefdoc_cod_tp_efec_tot  := var$aefco_cod_tipo_efec_tot;
    var$aefdoc_oid_calidad      := var$aefco_oid_calidad;
    var$aefdoc_num_importe      := var$aefco_num_importe;
    var$aefdoc_nel_cantidad     := var$aefco_nel_cantidad;

    /* arrays de medio de pago por documento */
    var$ampdoc_oid_divisa        := var$ampco_oid_divisa;
    var$ampdoc_oid_medio_pago    := var$ampco_oid_mp;
    var$ampdoc_oid_uni_med       := var$ampco_oid_uni_med;
    var$ampdoc_cod_tipo_med_pago := var$ampco_cod_tipo_mp;
    var$ampdoc_cod_nivel_detalle := var$ampco_cod_nivel_det;
    var$ampdoc_num_importe       := var$ampco_num_importe;
    var$ampdoc_nel_cantidad      := var$ampco_nel_cantidad;

    sapr_pdocumento_###VERSION###.sguardar_doc_contenedor(par$oid_formulario           => var$oid_formulario,
                                                     par$cod_formulario           => var$cod_formulario,
                                                     par$oid_grupo_documento      => null,
                                                     par$adoc_oid_documento       => var$adoc_oid_documento,
                                                     par$adoc_oid_documento_padre => var$adoc_oid_documento_padre,
                                                     par$adoc_oid_documento_sust  => var$adoc_oid_documento_sust,
                                                     par$adoc_oid_moviment_fondo  => var$adoc_oid_mov_fondo,
                                                     par$adoc_fyh_plan_certif     => var$adoc_fyh_plan_certif,
                                                     par$adoc_fyh_gestion         => var$adoc_fyh_gestion,

                                                     par$adoc_fyh_contable       => var$adoc_fyh_contable, 
                                                     par$adoc_cod_actual_id     => var$adoc_cod_actual_id, 
                                                     par$adoc_collection_id     => var$adoc_collection_id,

                                                     par$adoc_cod_externo         => var$adoc_cod_externo,
                           par$adoc_cod_comprobante     => var$adoc_cod_comprobante,
                                                     par$adoc_rowver              => var$adoc_rowver,

                                                     /* cuenta origen */
                                                     par$adoc_oid_cuenta_origen     => var$adoc_oid_cuenta_origen,
                                                     par$adoc_oid_cliente_origen    => var$adoc_oid_cliente_origen,
                                                     par$adoc_oid_subcliente_origen => var$adoc_oid_subcliente_origen,
                                                     par$adoc_oid_puntoservico_ori  => var$adoc_oid_puntoservico_ori,
                                                     par$adoc_oid_canal_origen      => var$adoc_oid_canal_origen,
                                                     par$adoc_oid_subcanal_origen   => var$adoc_oid_subcanal_origen,
                                                     par$adoc_oid_delegacion_origen => var$adoc_oid_delegacion_origen,
                                                     par$adoc_oid_planta_origen     => var$adoc_oid_planta_origen,
                                                     par$adoc_oid_sector_origen     => var$adoc_oid_sector_origen,

                                                     /* cuenta destino */
                                                     par$adoc_oid_cuenta_destino   => var$adoc_oid_cuenta_destino,
                                                     par$adoc_oid_cliente_destino  => var$adoc_oid_cliente_destino,
                                                     par$adoc_oid_subcliente_des   => var$adoc_oid_subcliente_des,
                                                     par$adoc_oid_puntoservico_des => var$adoc_oid_puntoservico_des,
                                                     par$adoc_oid_canal_destino    => var$adoc_oid_canal_destino,
                                                     par$adoc_oid_subcanal_destino => var$adoc_oid_subcanal_destino,
                                                     par$adoc_oid_delegacion_des   => var$adoc_oid_delegacion_des,
                                                     par$adoc_oid_planta_destino   => var$adoc_oid_planta_destino,
                                                     par$adoc_oid_sector_destino   => var$adoc_oid_sector_destino,

                                                     /* arrays asociativos */
                                                     /* arrays de efectivo por documento */
                                                     par$aefdoc_oid_documento    => var$aefdoc_oid_documento,
                                                     par$aefdoc_oid_divisa       => var$aefdoc_oid_divisa,
                                                     par$aefdoc_oid_denominacion => var$aefdoc_oid_denominacion,
                                                     par$aefdoc_oid_unid_medida  => var$aefdoc_oid_unid_medida,
                                                     par$aefdoc_cod_niv_detalle  => var$aefdoc_cod_niv_detalle,
                                                     par$aefdoc_cod_tp_efec_tot  => var$aefdoc_cod_tp_efec_tot,
                                                     par$aefdoc_oid_calidad      => var$aefdoc_oid_calidad,
                                                     par$aefdoc_num_importe      => var$aefdoc_num_importe,
                                                     par$aefdoc_nel_cantidad     => var$aefdoc_nel_cantidad,

                                                     /* arrays de medio de pago por documento */
                                                     par$ampdoc_oid_documento     => var$ampdoc_oid_documento,
                                                     par$ampdoc_oid_divisa        => var$ampdoc_oid_divisa,
                                                     par$ampdoc_oid_medio_pago    => var$ampdoc_oid_medio_pago,
                                                     par$ampdoc_cod_tipo_med_pago => var$ampdoc_cod_tipo_med_pago,
                                                     par$ampdoc_cod_nivel_detalle => var$ampdoc_cod_nivel_detalle,
                                                     par$ampdoc_num_importe       => var$ampdoc_num_importe,
                                                     par$ampdoc_nel_cantidad      => var$ampdoc_nel_cantidad,
                                                     par$ampdoc_oid_unidad_medida => var$ampdoc_oid_medio_pago,

                                                     /* arrays de terminos de medio de pago por documento */
                                                     par$avtmpdoc_oid_documento   => var$avtmpdoc_oid_documento,
                                                     par$avtmpdoc_oid_mediopago   => var$avtmpdoc_oid_medio_pago,
                                                     par$avtmpdoc_oid_t_mediopago => var$avtmpdoc_oid_t_mediopago,
                                                     par$avtmpdoc_des_valor       => var$avtmpdoc_des_valor,
                                                     par$avtmpdoc_nec_indice_grp  => var$avtmpdoc_nec_indice_grp,

                                                     -- contenedores
                                                     par$adoccont_oid_documento     => var$adoccont_oid_documento,
                                                     par$adoccont_oid_contenedor    => var$adoccont_oid_contenedor,
                                                     par$adoccont_rowver_contenedor => var$adoccont_rowver_contenedor,

                                                     -- Elementos
                                                     par$aelco_oid_contenedor => var$aelco_oid_contenedor,
                                                     par$aelco_tipo_elemento  => var$aelco_tipo_elemento,
                                                     par$aelco_precintos_ele  => var$aelco_precintos_ele,
                                                     par$aelco_ids_elemento   => var$aelco_ids_elemento,

                                                     /* informacion de ejecucion */
                                                     par$usuario        => var$usuario,
                                                     par$cod_cultura    => var$cod_cultura,
                                                     par$info_ejecucion => var$info_ejecucion,
                                                     par$hacer_commit   => var$hacer_commit,
                                                     par$confirmar_doc  => 1,
                                                     par$inserts        => var$inserts,
                                                     par$updates        => var$updates,
                                                     par$deletes        => var$deletes,
                                                     par$selects        => var$selects,
                                                     par$cod_ejecucion  => var$cod_ejecucion);

    par$inserts := par$inserts + var$inserts;
    par$updates := par$updates + var$updates;
    par$deletes := par$deletes + var$deletes;
    par$selects := par$selects + var$selects;

    if var$adoc_cod_comprobante is not null and
       var$adoc_cod_comprobante.count > 0 then
      par$cod_comprobante := gepr_putilidades_###VERSION###.fobtener_primer_registro_cod(var$adoc_cod_comprobante);
    end if;

  END screar_parametros_documento;

  PROCEDURE svalidar_desarmar_contenedor(par$cod_formulario IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$oid_delegacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_planta     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_sector     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$codcultura     IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    -- armazena as mensagens de validacao
    var$mensajes_validacion gepr_pcomon_###VERSION###.tipo$obs_;
  BEGIN

    var$mensajes_validacion := '#';

    /* IF (par$cod_formulario IS NULL) THEN
                var$mensajes_validacion := var$mensajes_validacion || GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'CodigoFormulario', 0) || chr(10);
    END IF;
        IF (par$oid_delegacion IS NULL) THEN
        var$mensajes_validacion := var$mensajes_validacion || GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Delegaci?n', 0) || chr(10);
    END IF;
      IF (par$oid_planta IS NULL) THEN
        var$mensajes_validacion := var$mensajes_validacion || GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Planta', 0) || chr(10);
        END IF;
    IF (par$oid_sector IS NULL) THEN
        var$mensajes_validacion := var$mensajes_validacion || GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Sector', 0) || chr(10);
      END IF;*/

    var$mensajes_validacion := var$mensajes_validacion || '#';

    -- se houver alguma mensagem na vari?vel, dispara o RAISE
    IF (length(var$mensajes_validacion) > 2) THEN
      RAISE_APPLICATION_ERROR(-20001, var$mensajes_validacion);
    END IF;

  END;

   PROCEDURE srecuperar_contenedores(par$oids_contenedores  IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$cods_precintos     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$rc_contenedores    OUT gepr_pcomon_###VERSION###.tipo$contenedores,
                                    par$rc_detalle_efec_mp OUT gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores,
                                    par$rc_precintos       OUT gepr_pcomon_###VERSION###.tipo$precintos_contenedores,
                                    par$rc_lst_val_cont    OUT gepr_pcomon_###VERSION###.tipo$lst_val_contenedores,
                                    par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$oids_contenedores VARCHAR2_ARRAY;
    var$nel_indice        gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    par$inserts := 0;
    par$selects := 0;

    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TOIDS';

    IF par$oids_contenedores IS NOT NULL AND
       par$oids_contenedores.COUNT > 0 THEN

      var$oids_contenedores := gepr_putilidades_###VERSION###.fconverter_array_table(par$oids_contenedores);

      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT 'OID_CONTENEDORES', COLUMN_VALUE
          FROM TABLE(var$oids_contenedores)
         WHERE COLUMN_VALUE IS NOT NULL;

      par$inserts := sql%rowcount;

    END IF;

    IF par$cods_precintos IS NOT NULL AND par$cods_precintos.COUNT > 0 THEN

      FOR idx IN par$cods_precintos.FIRST .. par$cods_precintos.LAST

       LOOP

        IF par$cods_precintos(idx) IS NOT NULL THEN

          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'OID_CONTENEDORES', PC.OID_CONTENEDOR
              FROM SAPR_TPRECINTOXCONTENEDOR PC
             WHERE PC.COD_PRECINTO = par$cods_precintos(idx);

          par$inserts := par$inserts + sql%rowcount;

        END IF;

      END LOOP;

    END IF;

    for c in (SELECT CONT.OID_CONTENEDOR,
                     CONT.OID_CONTENEDOR_PADRE,
                     CONT.OID_CONTENEDOR_MAYOR_NIVEL,
                     CONT.OID_TIPO_CONTENEDOR,
                     CONT.OID_SECTOR,
                     CONT.OID_CUENTA_MOVIMIENTO,
                     CUEN.OID_CLIENTE,
                     CUEN.OID_SUBCLIENTE,
                     CUEN.OID_PTO_SERVICIO,
                     CANA.OID_CANAL,
                     CUEN.OID_SUBCANAL,
                     CONT.OID_DOCUMENTO,
                     CONT.COD_ESTADO,
                     CONT.BOL_PACK_MODULAR,
                     CONT.FEC_VENCIMIENTO,
                     CONT.COD_PUESTO,
                     CONT.COD_TIPO_ELEMENTO,
                     PLAN.OID_PLANTA,
                     DELE.OID_DELEGACION,
                     NULL                            OID_CUENTA_SALDO,
                     NULL                            OID_GRUPO_DOCUMENTO,
                     TCON.COD_TIPO_CONTENEDOR
                FROM SAPR_TCONTENEDOR CONT
                LEFT JOIN SAPR_TCUENTA CUEN
                  ON CUEN.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
                LEFT JOIN GEPR_TSUBCANAL SUBC
                  ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL
                LEFT JOIN GEPR_TCANAL CANA
                  ON CANA.OID_CANAL = SUBC.OID_CANAL
               INNER JOIN GEPR_TSECTOR SECT
                  ON SECT.OID_SECTOR = CONT.OID_SECTOR
               INNER JOIN GEPR_TPLANTA PLAN
                  ON PLAN.OID_PLANTA = SECT.OID_PLANTA
               INNER JOIN GEPR_TDELEGACION DELE
                  ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
               INNER JOIN SAPR_GTT_TOIDS GTID
                  ON GTID.OID = CONT.OID_CONTENEDOR
                 AND GTID.COD_CALIFICADOR = 'OID_CONTENEDORES'
               INNER JOIN SAPR_TTIPO_CONTENEDOR TCON
                  ON TCON.OID_TIPO_CONTENEDOR = CONT.OID_TIPO_CONTENEDOR) loop

      par$rc_contenedores(var$nel_indice).OID_CONTENEDOR := c.OID_CONTENEDOR;
      par$rc_contenedores(var$nel_indice).OID_CONTENEDOR_PADRE := c.OID_CONTENEDOR_PADRE;
      par$rc_contenedores(var$nel_indice).OID_CONTENEDOR_MAYOR_NIVEL := c.OID_CONTENEDOR_MAYOR_NIVEL;
      par$rc_contenedores(var$nel_indice).OID_TIPO_CONTENEDOR := c.OID_TIPO_CONTENEDOR;
      par$rc_contenedores(var$nel_indice).OID_SECTOR := c.OID_SECTOR;
      par$rc_contenedores(var$nel_indice).OID_CUENTA_MOVIMIENTO := c.OID_CUENTA_MOVIMIENTO;
      par$rc_contenedores(var$nel_indice).OID_CLIENTE := c.OID_CLIENTE;
      par$rc_contenedores(var$nel_indice).OID_SUBCLIENTE := c.OID_SUBCLIENTE;
      par$rc_contenedores(var$nel_indice).OID_PTO_SERVICIO := c.OID_PTO_SERVICIO;
      par$rc_contenedores(var$nel_indice).OID_CANAL := c.OID_CANAL;
      par$rc_contenedores(var$nel_indice).OID_SUBCANAL := c.OID_SUBCANAL;
      par$rc_contenedores(var$nel_indice).OID_DOCUMENTO := c.OID_DOCUMENTO;
      par$rc_contenedores(var$nel_indice).COD_ESTADO := c.COD_ESTADO;
      par$rc_contenedores(var$nel_indice).BOL_PACK_MODULAR := c.BOL_PACK_MODULAR;
      par$rc_contenedores(var$nel_indice).FEC_VENCIMIENTO := c.FEC_VENCIMIENTO;
      par$rc_contenedores(var$nel_indice).COD_PUESTO := c.COD_PUESTO;
      par$rc_contenedores(var$nel_indice).COD_TIPO_ELEMENTO := c.COD_TIPO_ELEMENTO;
      par$rc_contenedores(var$nel_indice).OID_PLANTA := c.OID_PLANTA;
      par$rc_contenedores(var$nel_indice).OID_DELEGACION := c.OID_DELEGACION;
      par$rc_contenedores(var$nel_indice).OID_CUENTA_SALDO := c.OID_CUENTA_SALDO;
      par$rc_contenedores(var$nel_indice).OID_GRUPO_DOCUMENTO := c.OID_GRUPO_DOCUMENTO;
      par$rc_contenedores(var$nel_indice).COD_TIPO_CONTENEDOR := c.COD_TIPO_CONTENEDOR;

      var$nel_indice := var$nel_indice + 1;

      par$selects := par$selects + 1;

    end loop;

    var$nel_indice := 0;

    for p in (SELECT PRC.OID_CONTENEDOR, PRC.COD_PRECINTO
                FROM SAPR_TPRECINTOXCONTENEDOR PRC
               INNER JOIN SAPR_GTT_TOIDS GTID
                  ON GTID.OID = PRC.OID_CONTENEDOR
                 AND GTID.COD_CALIFICADOR = 'OID_CONTENEDORES') loop

      par$rc_precintos(var$nel_indice).OID_CONTENEDOR := p.OID_CONTENEDOR;
      par$rc_precintos(var$nel_indice).COD_PRECINTO := p.COD_PRECINTO;

      var$nel_indice := var$nel_indice + 1;

      par$selects := par$selects + 1;

    end loop;

    var$nel_indice := 0;

    for lvc in (SELECT LVE.OID_CONTENEDOR,
                       LST.COD_TIPO,
                       LSV.COD_VALOR,
                       LSV.DES_VALOR
                  FROM SAPR_TLISTA_VALORXELEMENTO LVE
                 INNER JOIN GEPR_TLISTA_TIPO LST
                    ON LST.OID_LISTA_TIPO = LVE.OID_LISTA_TIPO
                 INNER JOIN GEPR_TLISTA_VALOR LSV
                    ON LSV.OID_LISTA_VALOR = LVE.OID_LISTA_VALOR
                 INNER JOIN SAPR_GTT_TOIDS GTID
                    ON GTID.OID = LVE.OID_CONTENEDOR
                   AND GTID.COD_CALIFICADOR = 'OID_CONTENEDORES') loop

      par$rc_lst_val_cont(var$nel_indice).OID_CONTENEDOR := lvc.OID_CONTENEDOR;
      par$rc_lst_val_cont(var$nel_indice).COD_TIPO := lvc.COD_TIPO;
      par$rc_lst_val_cont(var$nel_indice).COD_VALOR := lvc.COD_VALOR;
      par$rc_lst_val_cont(var$nel_indice).DES_VALOR := lvc.DES_VALOR;

      var$nel_indice := var$nel_indice + 1;

      par$selects := par$selects + 1;

    end loop;

    var$nel_indice := 0;

    for demp in (SELECT OID_CONTENEDOR,
                        OID_DIVISA,
                        OID_DENOMINACION,
                        OID_CALIDAD,
                        OID_UNIDAD_MEDIDA,
                        COD_NIVEL_DETALLE,
                        COD_TIPO_MEDIO_PAGO,
                        OID_MEDIO_PAGO,
                        DES_MEDIO_PAGO,
                        NEL_CANTIDAD,
                        NUM_IMPORTE
                  FROM (SELECT EFEC.OID_CONTENEDOR,
                                DIV.OID_DIVISA,
                                DEN.OID_DENOMINACION,
                                CAL.OID_CALIDAD,
                                UNI.OID_UNIDAD_MEDIDA,
                                EFEC.COD_NIVEL_DETALLE,
                                NULL COD_TIPO_MEDIO_PAGO,
                                NULL OID_MEDIO_PAGO,
                                NULL DES_MEDIO_PAGO,
                                SUM(EFEC.NEL_CANTIDAD) NEL_CANTIDAD,
                                SUM(EFEC.NUM_IMPORTE) NUM_IMPORTE
                           FROM SAPR_TEFECTIVOXCONTENEDOR EFEC
                          INNER JOIN SAPR_TCONTENEDOR CONT
                             ON CONT.OID_CONTENEDOR = EFEC.OID_CONTENEDOR
                          INNER JOIN GEPR_TDIVISA DIV
                             ON DIV.OID_DIVISA = EFEC.OID_DIVISA
                           LEFT JOIN GEPR_TDENOMINACION DEN
                             ON DEN.OID_DENOMINACION = EFEC.OID_DENOMINACION
                            AND DEN.OID_DIVISA = DIV.OID_DIVISA
                           LEFT JOIN GEPR_TCALIDAD CAL
                             ON CAL.OID_CALIDAD = EFEC.OID_CALIDAD
                           LEFT JOIN GEPR_TUNIDAD_MEDIDA UNI
                             ON UNI.OID_UNIDAD_MEDIDA =
                                EFEC.OID_UNIDAD_MEDIDA
                          INNER JOIN SAPR_GTT_TOIDS GTID
                             ON GTID.OID = EFEC.OID_CONTENEDOR
                            AND GTID.COD_CALIFICADOR = 'OID_CONTENEDORES'
                          GROUP BY EFEC.OID_CONTENEDOR,
                                   DIV.OID_DIVISA,
                                   DEN.OID_DENOMINACION,
                                   CAL.OID_CALIDAD,
                                   UNI.OID_UNIDAD_MEDIDA,
                                   EFEC.COD_NIVEL_DETALLE
                         UNION ALL

                         SELECT MPG.OID_CONTENEDOR,
                                DIV.OID_DIVISA,
                                NULL OID_DENOMINACION,
                                NULL OID_CALIDAD,
                                NULL OID_UNIDAD_MEDIDA,
                                MPG.COD_NIVEL_DETALLE,
                                MP.COD_TIPO_MEDIO_PAGO,
                                MP.OID_MEDIO_PAGO,
                                MP.DES_MEDIO_PAGO,
                                SUM(MPG.NEL_CANTIDAD) NEL_CANTIDAD,
                                SUM(MPG.NUM_IMPORTE) NUM_IMPORTE
                           FROM SAPR_TMEDIO_PAGOXCONTENEDOR MPG
                          INNER JOIN SAPR_TCONTENEDOR CONT
                             ON CONT.OID_CONTENEDOR = MPG.OID_CONTENEDOR
                          INNER JOIN GEPR_TDIVISA DIV
                             ON DIV.OID_DIVISA = MPG.OID_DIVISA
                           LEFT JOIN GEPR_TMEDIO_PAGO MP
                             ON MP.OID_MEDIO_PAGO = MPG.OID_MEDIO_PAGO
                            AND MP.OID_DIVISA = DIV.OID_DIVISA
                          INNER JOIN SAPR_GTT_TOIDS GTID
                             ON GTID.OID = MPG.OID_CONTENEDOR
                            AND GTID.COD_CALIFICADOR = 'OID_CONTENEDORES'
                          GROUP BY MPG.OID_CONTENEDOR,
                                   DIV.OID_DIVISA,
                                   MP.COD_TIPO_MEDIO_PAGO,
                                   MP.OID_MEDIO_PAGO,
                                   MP.DES_MEDIO_PAGO,
                                   MPG.COD_NIVEL_DETALLE)) loop

      par$rc_detalle_efec_mp(var$nel_indice).OID_CONTENEDOR := demp.OID_CONTENEDOR;
      par$rc_detalle_efec_mp(var$nel_indice).OID_DIVISA := demp.OID_DIVISA;
      par$rc_detalle_efec_mp(var$nel_indice).OID_DENOMINACION := demp.OID_DENOMINACION;
      par$rc_detalle_efec_mp(var$nel_indice).OID_CALIDAD := demp.OID_CALIDAD;
      par$rc_detalle_efec_mp(var$nel_indice).OID_UNIDAD_MEDIDA := demp.OID_UNIDAD_MEDIDA;
      par$rc_detalle_efec_mp(var$nel_indice).COD_NIVEL_DETALLE := demp.COD_NIVEL_DETALLE;
      par$rc_detalle_efec_mp(var$nel_indice).COD_TIPO_MEDIO_PAGO := demp.COD_TIPO_MEDIO_PAGO;
      par$rc_detalle_efec_mp(var$nel_indice).OID_MEDIO_PAGO := demp.OID_MEDIO_PAGO;
      par$rc_detalle_efec_mp(var$nel_indice).DES_MEDIO_PAGO := demp.DES_MEDIO_PAGO;
      par$rc_detalle_efec_mp(var$nel_indice).NEL_CANTIDAD := demp.NEL_CANTIDAD;
      par$rc_detalle_efec_mp(var$nel_indice).NUM_IMPORTE := demp.NUM_IMPORTE;

      var$nel_indice := var$nel_indice + 1;

      par$selects := par$selects + 1;

    end loop;

  END srecuperar_contenedores;

PROCEDURE s_upd_tposicion_sector(par$codIdioma                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$codDelegacion              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$codPlanta                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$codSector                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$acont_cod_precinto         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acont_cod_posicion         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acont_cod_posicion_destino IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$UsuarioCreacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$hacer_Commit               IN integer,
                                   par$info_ejecucion             IN VARCHAR2,
                                   par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$PosicionOcupada            gepr_pcomon_###VERSION###.tipo$nel_;
    var$oid_contenedor             gepr_pcomon_###VERSION###.tipo$oid_;
    var$codSector                  gepr_pcomon_###VERSION###.tipo$cod_;
    var$posicionExisteBanco        gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_contenedor_posicionado gepr_pcomon_###VERSION###.tipo$nbol_;

    var$inicio TIMESTAMP;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 's_upd_tposicion_sector';
    var$err VARCHAR2(4000);

    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;

  BEGIN

  var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
     par$selects := 0;

    /*Busca oidSector */
    SELECT SEC.OID_SECTOR
      INTO var$codSector
      FROM GEPR_TSECTOR SEC
     INNER JOIN GEPR_TPLANTA PLA
        ON PLA.OID_PLANTA = SEC.OID_PLANTA
       AND PLA.COD_PLANTA = par$codPlanta
     INNER JOIN GEPR_TDELEGACION DEL
        ON DEL.OID_DELEGACION = PLA.OID_DELEGACION
       AND DEL.COD_DELEGACION = par$codDelegacion
     WHERE SEC.COD_SECTOR = par$codSector;

   par$selects := par$selects + sql%rowcount;

    for idx in par$acont_cod_precinto.first .. par$acont_cod_precinto.last loop

      begin

        /*BUSCA OID_CONTENEDOR PELO PRECINTO*/
        SELECT OID_CONTENEDOR
          INTO var$oid_contenedor
          FROM Sapr_Tprecintoxcontenedor
         WHERE COD_PRECINTO = par$acont_cod_precinto(idx);

      par$selects := par$selects + sql%rowcount;

      exception
        when no_data_found then
          var$oid_contenedor := null;
      end;

      /*verifica se posicao origem existe senao cria ja informando contendor*/
      IF par$acont_cod_posicion(idx) is null AND
         par$acont_cod_posicion_destino(idx) IS not NULL THEN

        SELECT COUNT(0)
          INTO var$posicionExisteBanco
          FROM SAPR_TPOSICION_SECTOR
         WHERE COD_POSICION = par$acont_cod_posicion_destino(idx)
           AND OID_SECTOR = var$codSector;

      par$selects := par$selects + sql%rowcount;

        IF var$posicionExisteBanco = 0 then
          INSERT INTO SAPR_TPOSICION_SECTOR
            (OID_POSICION_SECTOR,
             OID_SECTOR,
             COD_POSICION,
             OID_CONTENEDOR,
             BOL_ACTIVO,
             GMT_CREACION,
             DES_USUARIO_CREACION,
             GMT_MODIFICACION,
             DES_USUARIO_MODIFICACION)
          VALUES
            (sys_guid(),
             var$codSector,
             par$acont_cod_posicion_destino(idx),
             var$oid_contenedor,
             1,
             sysdate,
             par$UsuarioCreacion,
             sysdate,
             par$UsuarioCreacion);
        ELSE
          var$PosicionOcupada := 0;
          SELECT COUNT(0)
            INTO var$PosicionOcupada
            FROM SAPR_TPOSICION_SECTOR
           WHERE (COD_POSICION = par$acont_cod_posicion_destino(idx) AND
                 OID_SECTOR = var$codSector)
             AND (OID_CONTENEDOR is not null AND
                 OID_CONTENEDOR <> var$oid_contenedor);

        par$selects := par$selects + sql%rowcount;

          /*SE POSICAOO VEIO PREENCHIDA N?O POSSO INSERIR REGISTRO NESTA POSICAOO ENTAO EXIBE MSG DE ERRO*/
          IF var$PosicionOcupada > 0 THEN
            RAISE_APPLICATION_ERROR(-20001,
                                    GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                        'MSG_POSICION_OCUPADA',
                                                                        gepr_pcomon_###VERSION###.const$CodFunDefCambPosicCont,
                                                                        'GenesisSaldos',
                                                                        NULL,
                                                                        1));
          END IF;

          /*se posi??o nao esta ocupada entao atualizo informando contenedor que ir? ocupar
          a posi??o*/
          UPDATE SAPR_TPOSICION_SECTOR
             SET OID_CONTENEDOR           = var$oid_contenedor,
                 BOL_ACTIVO               = 1,
                 GMT_MODIFICACION         = sysdate,
                 DES_USUARIO_MODIFICACION = par$UsuarioCreacion
           WHERE OID_SECTOR = var$codSector
             AND COD_POSICION = par$acont_cod_posicion_destino(idx);

             par$updates := par$updates + sql%rowcount;

        END IF;

      ELSIF par$acont_cod_posicion(idx) IS NOT NULL AND
            par$acont_cod_posicion_destino(idx) IS NOT NULL THEN

        select count(*)
          into var$bol_contenedor_posicionado
          from sapr_tposicion_sector ps
         inner join gepr_tsector s
            on s.oid_sector = ps.oid_sector
         where ps.oid_contenedor = var$oid_contenedor
           and s.cod_sector = par$codSector
           and ps.cod_posicion = par$acont_cod_posicion(idx);

      par$selects := par$selects + sql%rowcount;

        /*SE POSICAOO VEIO PREENCHIDA N?O POSSO INSERIR REGISTRO NESTA POSICAOO ENTAO EXIBE MSG DE ERRO*/
        IF var$bol_contenedor_posicionado = 0 THEN

          RAISE_APPLICATION_ERROR(-20001,
                                  GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                      'msg_contenedor_sector_distinto',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDefCambPosicCont,
                                                                      'GenesisSaldos',
                                                                      NULL,
                                                                      1));
        END IF;

        SELECT COUNT(0)
          INTO var$posicionExisteBanco
          FROM SAPR_TPOSICION_SECTOR
         WHERE COD_POSICION = par$acont_cod_posicion_destino(idx)
           AND OID_SECTOR = var$codSector;

      par$selects := par$selects + sql%rowcount;

        /*CRIA POSI??O DESTINO PREENCHIDA E ATUALIZA A POSICAO INICIAL PARA VAZIO*/
        IF var$posicionExisteBanco = 0 then
          INSERT INTO SAPR_TPOSICION_SECTOR
            (OID_POSICION_SECTOR,
             OID_SECTOR,
             COD_POSICION,
             OID_CONTENEDOR,
             BOL_ACTIVO,
             GMT_CREACION,
             DES_USUARIO_CREACION,
             GMT_MODIFICACION,
             DES_USUARIO_MODIFICACION)
          VALUES
            (sys_guid(),
             var$codSector,
             par$acont_cod_posicion_destino(idx),
             var$oid_contenedor,
             1,
             sysdate,
             par$UsuarioCreacion,
             sysdate,
             par$UsuarioCreacion);

             par$inserts := par$inserts + sql%rowcount;

			 /*  limpa a posicao anterior */
             UPDATE SAPR_TPOSICION_SECTOR PS
               SET OID_CONTENEDOR           = null,
                 BOL_ACTIVO               = 1,
                 GMT_MODIFICACION         = sysdate,
                 DES_USUARIO_MODIFICACION = par$UsuarioCreacion
                 WHERE OID_SECTOR = var$codSector
                 AND COD_POSICION = par$acont_cod_posicion(idx);
              
             par$updates := par$updates + sql%rowcount;

        ELSE
          /*VALIDA SE POSI??O QUE QUERO ENVIAR CONTENEDOR ESTA OCUPADA*/
          var$PosicionOcupada := 0;
          IF par$acont_cod_posicion_destino(idx) IS NOT NULL THEN
            SELECT COUNT(0)
              INTO var$PosicionOcupada
              FROM SAPR_TPOSICION_SECTOR
           WHERE (COD_POSICION = par$acont_cod_posicion_destino(idx) AND
                 OID_SECTOR = var$codSector)
             AND (OID_CONTENEDOR is not null AND
                 OID_CONTENEDOR <> var$oid_contenedor);

               par$selects := par$selects + sql%rowcount;

          END IF;
          /*SE POSICAOO VEIO PREENCHIDA N?O POSSO INSERIR REGISTRO NESTA POSICAOO ENTAO EXIBE MSG DE ERRO*/
          IF var$PosicionOcupada > 0 THEN
            RAISE_APPLICATION_ERROR(-20001,
                                    GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                        'MSG_POSICION_OCUPADA',
                                                                        gepr_pcomon_###VERSION###.const$CodFunDefCambPosicCont,
                                                                        'GenesisSaldos',
                                                                        NULL,
                                                                        1));
          END IF;

      /*mudando posi??o inicial para vazio*/
        UPDATE SAPR_TPOSICION_SECTOR
           SET OID_CONTENEDOR           = null,
               BOL_ACTIVO               = 0,
               GMT_MODIFICACION         = sysdate,
               DES_USUARIO_MODIFICACION = par$UsuarioCreacion
         WHERE OID_SECTOR = var$codSector
           AND COD_POSICION = par$acont_cod_posicion(idx);

          /*Definindo posi??o destino*/
          UPDATE SAPR_TPOSICION_SECTOR
             SET OID_CONTENEDOR           = var$oid_contenedor,
                 BOL_ACTIVO               = 1,
                 GMT_MODIFICACION         = sysdate,
                 DES_USUARIO_MODIFICACION = par$UsuarioCreacion
           WHERE OID_SECTOR = var$codSector
             AND COD_POSICION = par$acont_cod_posicion_destino(idx);

             par$updates := par$updates + sql%rowcount;

        END IF;

      par$updates := par$updates + sql%rowcount;

      elsIF par$acont_cod_posicion(idx) is not null AND
            par$acont_cod_posicion_destino(idx) IS NULL THEN

        select count(*)
          into var$bol_contenedor_posicionado
          from sapr_tposicion_sector ps
         inner join gepr_tsector s
            on s.oid_sector = ps.oid_sector
         where ps.oid_contenedor = var$oid_contenedor
           and s.cod_sector = par$codSector
           and ps.cod_posicion = par$acont_cod_posicion(idx);

      par$selects := par$selects + sql%rowcount;

        /*SE POSICAOO VEIO PREENCHIDA N?O POSSO INSERIR REGISTRO NESTA POSICAOO ENTAO EXIBE MSG DE ERRO*/
        IF var$bol_contenedor_posicionado = 0 THEN

          RAISE_APPLICATION_ERROR(-20001,
                                  GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                      'msg_contenedor_sector_distinto',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDefCambPosicCont,
                                                                      'GenesisSaldos',
                                                                      NULL,
                                                                      1));
        END IF;

        update sapr_tposicion_sector ps
           set ps.oid_contenedor = null
         where ps.oid_contenedor = var$oid_contenedor;

      par$updates := par$updates + sql%rowcount;

      END IF;

    end loop;

    IF par$hacer_Commit = 1 THEN
      COMMIT;
    END IF;

  var$registros := par$inserts + par$updates + par$selects;

    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                         const$nombre_func,
                                                         par$inserts,
                                                         par$updates,
                                                         0,
                                                         0,
                                                         par$selects,
                                                         var$registros,
                                                         var$inicio,
                                                         null,
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$UsuarioCreacion,
                                                         par$cod_ejecucion);

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$registros := par$inserts + par$updates +
                       par$selects;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           par$updates,
                                                           0,
                                                           0,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$UsuarioCreacion,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END s_upd_tposicion_sector;



  PROCEDURE s_ins_talerta_venc_contenedor(par$codPrecinto     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$fechaAlerta     IN DATE,
                                          par$codTipoAlerta   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$diasVencer      IN INTEGER,
                                          par$Emails          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$UsuarioCreacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$hacer_Commit    IN INTEGER) IS

    var$oidcontenedor gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN

    /*
    Recuperar las informaciones del contenedor:
    SAPR_TALERTA_VENC_CONTENEDOR.OID_CONTENEDOR = OID del contenedor desarmado
    recibido por el par?metro Contenedor.codPrecinto
    */
    SELECT OID_CONTENEDOR
      INTO var$oidcontenedor
      FROM SAPR_TPRECINTOXCONTENEDOR
     WHERE COD_PRECINTO = par$codPrecinto;

    IF var$oidcontenedor IS NOT NULL THEN
      INSERT INTO SAPR_TALERTA_VENC_CONTENEDOR
        (OID_ALERTA_VENC_CONTENEDOR,
         OID_CONTENEDOR,
         COD_TIPO_ALERTA,
         NEC_DIAS_VENCER,
         DES_EMAILS,
         GMT_CREACION,
         DES_USUARIO_CREACION)
      VALUES
        (sys_guid(),
         var$oidcontenedor,
         par$codTipoAlerta,
         par$diasVencer,
         par$Emails,
         sysdate,
         par$UsuarioCreacion);
    END IF;

    IF par$hacer_Commit = 1 THEN
      COMMIT;
    END IF;

  END s_ins_talerta_venc_contenedor;

  /* Valida o estado do contenedor */
  FUNCTION fvalidar_estado_contenedor(par$oid_contenedor IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado     IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS
    var$retorno gepr_pcomon_###VERSION###.tipo$nbol_;
  BEGIN

    BEGIN

      SELECT 1
        INTO var$retorno
        FROM SAPR_TCONTENEDOR C
       WHERE C.OID_CONTENEDOR = par$oid_contenedor
         and C.COD_ESTADO = par$cod_estado;

    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$retorno := NULL;
    END;

    return var$retorno;
  end fvalidar_estado_contenedor;

  function frecuperar_oid_tipo_cont(par$cod_tipo_contenedor gepr_pcomon_###VERSION###.tipo$cod_)
    return gepr_pcomon_###VERSION###.tipo$oid_ is
    var$oid_tipo_contenedor gepr_pcomon_###VERSION###.tipo$oid_;
  begin

    if par$cod_tipo_contenedor is not null then

      begin

        select tc.oid_tipo_contenedor
          into var$oid_tipo_contenedor
          from sapr_ttipo_contenedor tc
         where tc.cod_tipo_contenedor = par$cod_tipo_contenedor;

      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          var$oid_tipo_contenedor := NULL;
      END;

    end if;

    return var$oid_tipo_contenedor;
  end frecuperar_oid_tipo_cont;

  PROCEDURE s_buscarcontenedorxposicion(par$codDelegacion      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codPlanta          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codSector          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$packModular        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rc_contenedores    OUT SYS_REFCURSOR,
                                        par$rc_detalle_efec_mp OUT SYS_REFCURSOR,
                                        par$cod_usuario        in gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion     IN VARCHAR2,
                                        par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$codSector        gepr_pcomon_###VERSION###.tipo$cod_;
    var$CondicaoConsulta gepr_pcomon_###VERSION###.tipo$cod_;
    var$inicio           TIMESTAMP;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 's_buscarcontenedorxposicion';
    var$err         VARCHAR2(4000);
  BEGIN
    var$inicio  := SYSTIMESTAMP;
    par$selects := 0;

    /*
    SAPR_TCONTENEDOR.COD_ESTADO = AR ? Armado
    SAPR_TCONTENEDOR.OID_SECTOR = OID del sector
    packModular
    Define si deben ser regresados los contenedores del tipo pack modular, no pack modular o ambos:
    Nulo: Ser?n regresados ambos contenedores: pack modular y no pack modular
    0: Ser?n regresado solamente contenedores q no son pack modular
    1: Ser?n regresado solamente contenedores q son pack modular
    */

    /*Busca oidSector */
    SELECT SEC.OID_SECTOR
      INTO var$codSector
      FROM GEPR_TSECTOR SEC
     INNER JOIN GEPR_TPLANTA PLA
        ON PLA.OID_PLANTA = SEC.OID_PLANTA
       AND PLA.COD_PLANTA = par$codPlanta
     INNER JOIN GEPR_TDELEGACION DEL
        ON DEL.OID_DELEGACION = PLA.OID_DELEGACION
       AND DEL.COD_DELEGACION = par$codDelegacion
     WHERE SEC.COD_SECTOR = par$codSector;

    par$selects := par$selects + sql%rowcount;

    IF par$packModular IS NOT NULL THEN
      var$CondicaoConsulta := ' AND CONT.BOL_PACK_MODULAR =' ||
                              par$packModular;
    END IF;

    OPEN par$rc_contenedores FOR
      SELECT DISTINCT CONT.OID_CONTENEDOR,
                      TPCONT.COD_TIPO_CONTENEDOR,
                      TPCONT.DES_TIPO_CONTENEDOR,
                      CONT.GMT_CREACION AS fechaArmado,
                      PRECXCONT.COD_PRECINTO,
            PRECXCONT.BOL_PRECINTO_AUTOMATICO,
                      CUEN.OID_DELEGACION,
                      CUEN.COD_DELEGACION,
                      CUEN.DES_DELEGACION,
                      CUEN.OID_PLANTA,
                      CUEN.COD_PLANTA,
                      CUEN.DES_PLANTA,
                      CUEN.OID_SECTOR,
                      CUEN.COD_SECTOR,
                      CUEN.DES_SECTOR,
                      CONTXPOSI.COD_POSICION,
                      CUEN.OID_CLIENTE,
                      CUEN.OID_SUBCLIENTE,
                      CUEN.OID_PTO_SERVICIO,
                      CUEN.OID_CANAL,
                      CUEN.COD_CANAL,
                      CUEN.DES_CANAL,
                      CUEN.OID_SUBCANAL,
                      CUEN.COD_SUBCANAL,
                      CUEN.DES_SUBCANAL,
                      CUEN.COD_CLIENTE,
                      CUEN.DES_CLIENTE,
                      CUEN.COD_SUBCLIENTE,
                      CUEN.DES_SUBCLIENTE
        FROM SAPR_TCONTENEDOR CONT
       INNER JOIN SAPR_TTIPO_CONTENEDOR TPCONT
          ON TPCONT.OID_TIPO_CONTENEDOR = CONT.OID_TIPO_CONTENEDOR
       INNER JOIN SAPR_TPRECINTOXCONTENEDOR PRECXCONT
          ON PRECXCONT.OID_CONTENEDOR = CONT.OID_CONTENEDOR
        left JOIN SAPR_TPOSICION_SECTOR CONTXPOSI
          ON CONTXPOSI.OID_CONTENEDOR = CONT.OID_CONTENEDOR
        LEFT JOIN SAPR_VCUENTA CUEN
          ON CUEN.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
       WHERE CUEN.OID_SECTOR = var$codSector
         AND CONT.COD_ESTADO = 'AR'
       ORDER BY CONT.GMT_CREACION;

       par$selects := par$selects + sql%rowcount;

    OPEN par$rc_detalle_efec_mp FOR
      SELECT EFEC.OID_CONTENEDOR,
             DIV.OID_DIVISA,
             DIV.COD_ISO_DIVISA,
             DIV.DES_DIVISA,
             DIV.COD_SIMBOLO,
             DEN.OID_DENOMINACION,
             DEN.COD_DENOMINACION,
             DEN.DES_DENOMINACION,
             CAL.OID_CALIDAD,
             CAL.COD_CALIDAD,
             UNI.OID_UNIDAD_MEDIDA,
             EFEC.COD_NIVEL_DETALLE,
             EFEC.BOL_DISPONIBLE,
             EFEC.BOL_BLOQUEADO,
             NULL COD_TIPO_MEDIO_PAGO,
             NULL COD_MEDIO_PAGO,
             NULL DES_MEDIO_PAGO,
             SUM(EFEC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(EFEC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TEFECTIVOXCONTENEDOR EFEC
       INNER JOIN SAPR_TCONTENEDOR CONT
          ON CONT.OID_CONTENEDOR = EFEC.OID_CONTENEDOR
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = EFEC.OID_DIVISA
        LEFT JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = EFEC.OID_DENOMINACION
         AND DEN.OID_DIVISA = DIV.OID_DIVISA
        LEFT JOIN GEPR_TCALIDAD CAL
          ON CAL.OID_CALIDAD = EFEC.OID_CALIDAD
       LEFT JOIN GEPR_TUNIDAD_MEDIDA UNI
          ON UNI.OID_UNIDAD_MEDIDA = EFEC.OID_UNIDAD_MEDIDA
       GROUP BY EFEC.OID_CONTENEDOR,
                DIV.OID_DIVISA,
                DIV.COD_ISO_DIVISA,
                DIV.DES_DIVISA,
                DIV.COD_SIMBOLO,
                DEN.OID_DENOMINACION,
                DEN.COD_DENOMINACION,
                DEN.DES_DENOMINACION,
                CAL.OID_CALIDAD,
                CAL.COD_CALIDAD,
                UNI.OID_UNIDAD_MEDIDA,
                EFEC.COD_NIVEL_DETALLE,
                EFEC.BOL_DISPONIBLE,
                EFEC.BOL_BLOQUEADO
      UNION ALL

      SELECT MPG.OID_CONTENEDOR,
             DIV.OID_DIVISA,
             DIV.COD_ISO_DIVISA,
             DIV.DES_DIVISA,
             DIV.COD_SIMBOLO,
             NULL OID_DENOMINACION,
             NULL COD_DENOMINACION,
             NULL DES_DENOMINACION,
             NULL OID_CALIDAD,
             NULL COD_CALIDAD,
             NULL OID_UNIDAD_MEDIDA,
             NULL COD_NIVEL_DETALLE,
             NULL BOL_DISPONIBLE,
             NULL BOL_BLOQUEADO,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.OID_MEDIO_PAGO,
             MP.DES_MEDIO_PAGO,
             SUM(MPG.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(MPG.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TMEDIO_PAGOXCONTENEDOR MPG
       INNER JOIN SAPR_TCONTENEDOR CONT
          ON CONT.OID_CONTENEDOR = MPG.OID_CONTENEDOR
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = MPG.OID_DIVISA
        LEFT JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = MPG.OID_MEDIO_PAGO
         AND MP.OID_DIVISA = DIV.OID_DIVISA
       GROUP BY MPG.OID_CONTENEDOR,
                DIV.OID_DIVISA,
                DIV.COD_ISO_DIVISA,
                DIV.DES_DIVISA,
                DIV.COD_SIMBOLO,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.OID_MEDIO_PAGO,
                MP.DES_MEDIO_PAGO;

  par$selects := par$selects + sql%rowcount;

    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                         const$nombre_func,
                                                         0,
                                                         0,
                                                         0,
                                                         0,
                                                         par$selects,
                                                         par$selects,
                                                         var$inicio,
                                                         null,
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$cod_usuario,
                                                         par$cod_ejecucion);

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;


      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           0,
                                                           0,
                                                           0,
                                                           0,
                                                           par$selects,
                                                           par$selects,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END s_buscarcontenedorxposicion;

  PROCEDURE srecuperar_contenedores_fifo(par$cod_tipo_contenedor IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$oid_delegacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_planta          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_sector          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_cliente         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_subcliente      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_canal           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_subcanal        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_divisa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$oid_denominacion    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$importe             IN gepr_pcomon_###VERSION###.tipo$imp_,
                                         par$codIdioma           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_canal           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_subcanal        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$rc_contenedores     OUT SYS_REFCURSOR,
                                         par$rc_precintos        OUT SYS_REFCURSOR,
                                         par$cod_usuario         in gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion      IN VARCHAR2,
                                         par$cod_ejecucion       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    -- total somado dos efetivos dos contenedores encontrados. Usado para validar se importe informado na entrada existe na tabela
    var$total_sumado gepr_pcomon_###VERSION###.tipo$imp_;
    -- contador de loop para controlar variavel de OID_ANTERIOR
    var$contador gepr_pcomon_###VERSION###.tipo$nel_;
    -- armazena o OID_ANTERIOR do contenedor no loop
    var$oid_anterior gepr_pcomon_###VERSION###.tipo$oid_;
    -- flag para controlar se ser? necessario continuar gravando registros na tabela temporaria depois de alcan?ar o valor pedido por parametro de entrada.
    -- OBS: Pode ser que durante o loop, chegue-se ao valor pedido, mas o loop deve continuar, porque ainda tem registros do mesmo contenedor e a logica devolve o contenedor completo.
    -- PORQUE NO FINAL DO LOOP, A RESPOSTA DO SERVI?O SER? TUDO QUE ESTIVER NA TABELA TEMPORARIA
    var$flag_limite gepr_pcomon_###VERSION###.tipo$nel_;
    -- armazena as mensagens de validacao
    var$mensajes_validacion gepr_pcomon_###VERSION###.tipo$obs_;
    var$oidCanal            gepr_pcomon_###VERSION###.tipo$cod_;
    var$oidSubCanal         gepr_pcomon_###VERSION###.tipo$cod_;

    var$inicio TIMESTAMP;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_contenedores_fifo';
    var$err VARCHAR2(4000);

    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;

  BEGIN

    var$inicio  := SYSTIMESTAMP;
    par$selects := 0;
    par$inserts := 0;

    var$mensajes_validacion := '#';

    IF (par$oid_cliente IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Cliente',
                                                                     0) ||
                                 chr(10);
      --RAISE_APPLICATION_ERROR(-20001,GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Cliente', 1));
    END IF;

    IF (par$oid_canal IS NULL AND par$cod_canal IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Canal',
                                                                     0) ||
                                 chr(10);
      --RAISE_APPLICATION_ERROR(-20001,GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Canal', 1));
    END IF;

    IF (par$oid_subcanal IS NULL AND par$cod_Subcanal IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'SubCanal',
                                                                     0) ||
                                 chr(10);
      --RAISE_APPLICATION_ERROR(-20001,GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'SubCanal', 1));
    END IF;

    IF (par$oid_delegacion IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Delegaci?n',
                                                                     0) ||
                                 chr(10);
      --RAISE_APPLICATION_ERROR(-20001,GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Delegaci?n', 1));
    END IF;

    IF (par$oid_planta IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Planta',
                                                                     0) ||
                                 chr(10);
      --RAISE_APPLICATION_ERROR(-20001,GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Planta', 1));
    END IF;

    IF (par$oid_sector IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Sector',
                                                                     0) ||
                                 chr(10);
      --RAISE_APPLICATION_ERROR(-20001,GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Sector', 1));
    END IF;

    IF (par$oid_divisa IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Divisa',
                                                                     0) ||
                                 chr(10);
      --RAISE_APPLICATION_ERROR(-20001,GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Divisa', 1));
    END IF;

    IF (par$importe IS NULL OR par$importe <= 0) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Importe',
                                                                     0) ||
                                 chr(10);
      --RAISE_APPLICATION_ERROR(-20001,GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Importe', 1));
    END IF;

    var$mensajes_validacion := var$mensajes_validacion || '#';

    -- se houver alguma mensagem na vari?vel, dispara o RAISE
    IF (length(var$mensajes_validacion) > 2) THEN
      RAISE_APPLICATION_ERROR(-20001, var$mensajes_validacion);
    END IF;

    --Recupera oid do Canal e do subCanal pelo codigo
    IF par$oid_canal IS NULL THEN
      SELECT OID_CANAL
        INTO var$oidCanal
        FROM GEPR_TCANAL
       WHERE COD_CANAL = par$cod_canal;

      par$selects := par$selects + sql%rowcount;

    ELSE
      var$oidCanal := par$oid_canal;
    end if;

    IF par$oid_Subcanal IS NULL THEN
      SELECT OID_SUBCANAL
        INTO var$oidSubCanal
        FROM GEPR_TSUBCANAL
       WHERE COD_SUBCANAL = par$cod_Subcanal;

      par$selects := par$selects + sql%rowcount;

    else
      var$oidSubCanal := par$oid_Subcanal;
    end if;

    -- chamada procedure que ir? gerar na tabela temporaria SAPR_GTT_TOIDS, os OIDS dos contenedores encontrados de acordo com os filtros passados.
    sgenerar_oids_contenedor_fifo(par$cod_tipo_contenedor,
                                  par$oid_delegacion,
                                  par$oid_planta,
                                  par$oid_sector,
                                  par$oid_cliente,
                                  par$oid_subcliente,
                                  par$oid_pto_servicio,
                                  var$oidCanal,
                                  var$oidSubCanal,
                                  par$oid_divisa,
                                  par$oid_denominacion,
                                  par$importe,
                                  par$inserts,
                                  par$selects);

    par$inserts := par$inserts + var$inserts;
    par$selects := par$selects + var$selects;

    -- limpa a tabela tempor?ria SAPR_GTT_TEFECXCONTENEDOR_FIFO, para evitar duplicidade de dados entre as sess?es
    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TEFECXCONTENEDOR_FIFO';

    -- inicia as variaveis de controle
    var$total_sumado := 0;
    var$contador     := 0;
    var$flag_limite  := 0;

    -- loop no resultado da consulta dos valores (efetivos) encontrados para os contenedores que foram gravados na tabela temporaria anteriormente para fazer
    -- as valida??es dos valores. (Pedido x encontrados)
    -- OBS: Busca todos os dados no select para n?o precisar fazer outra consulta depois, uma vez que a resposta do servi?o necessita deles.
    FOR efetivoContenedor IN (SELECT CONT.OID_CONTENEDOR,
                                     CONT.GMT_CREACION,
                                     CONT.OID_TIPO_CONTENEDOR,
                                     TCON.COD_TIPO_CONTENEDOR,
                                     TCON.DES_TIPO_CONTENEDOR,
                                     PSECT.COD_POSICION,
                                     EFEC.OID_DIVISA,
                                     EFEC.OID_DENOMINACION,
                                     EFEC.OID_CALIDAD,
                                     EFEC.BOL_DISPONIBLE,
                                     EFEC.BOL_BLOQUEADO,
                                     EFEC.NEL_CANTIDAD,
                                     EFEC.NUM_IMPORTE
                                FROM SAPR_GTT_TOIDS GTTO
                               INNER JOIN SAPR_TCONTENEDOR CONT
                                  ON CONT.OID_CONTENEDOR = GTTO.OID
                                 AND GTTO.COD_CALIFICADOR =
                                     'OID_CONTENEDORES_FIFO'
                               INNER JOIN SAPR_TTIPO_CONTENEDOR TCON
                                  ON TCON.OID_TIPO_CONTENEDOR =
                                     CONT.OID_TIPO_CONTENEDOR
                               INNER JOIN SAPR_TPOSICION_SECTOR PSECT
                                  ON PSECT.OID_CONTENEDOR =
                                     CONT.OID_CONTENEDOR
                               INNER JOIN SAPR_TEFECTIVOXCONTENEDOR EFEC
                                  ON EFEC.OID_CONTENEDOR =
                                     CONT.OID_CONTENEDOR
                               WHERE EFEC.OID_DIVISA = par$oid_divisa
                               ORDER BY CONT.FEC_VENCIMIENTO,
                                        EFEC.OID_CONTENEDOR,
                                        EFEC.NUM_IMPORTE)

     LOOP
      -- se foi informado DENOMINA??O de entrada, ? obrigat?rio no LOOP validar a soma dos valores somente se a denomina??o for igual a do parametro de entrada.
      -- D?vida que possa surgir: PORQUE ent?o n?o se colocou na consulta de uma vez??? Para n?o precisar fazer outra consulta depois, uma vez que o retorno da procedure
      --               precisa devolver o contenedor completo, ou seja, n?o somente com denomina??o passada na entrada, mas todas denomina??es do contenedor
      IF (par$oid_denominacion IS NOT NULL) THEN

        -- Ent?o, se denomina??o passada for diferente da do Loop, ent?o passa pro pr?ximo indice
        IF (par$oid_denominacion <>
           NVL(efetivoContenedor.OID_DENOMINACION, '-')) THEN
          CONTINUE;
        END IF;

      END IF;

      -- soma acumulada dos valores
      var$total_sumado := var$total_sumado + efetivoContenedor.NUM_IMPORTE;

      -- se for primeira itera??o do Loop, armazena o OID_CONTENEDOR
      IF (var$contador = 0) THEN
        var$oid_anterior := efetivoContenedor.OID_CONTENEDOR;
      END IF;

      -- se soma acumulada atingiu ou passou do valor pedido
      IF (var$total_sumado >= par$importe) THEN

        -- se FLAG de soma acumulada atingida for 0, ? porque foi a primeira vez que entrou no IF de soma acumulada atingida. Ent?o altera o FLAG para 1.
        -- OBS: Pode ser que ele entre no limite atingido, mas n?o poder? ainda SAIR do loop, porque pode haver outros registros do mesmo contenedor que precisam
        --    ser inseridos ainda na tabela temporaria.
        -- PORQUE NO FINAL DO LOOP, A RESPOSTA DO SERVI?O SER? TUDO QUE ESTIVER NA TABELA TEMPORARIA
        IF (var$flag_limite = 0) THEN

          var$flag_limite := 1;

          -- grava o item do loop na tabela temporaria SAPR_GTT_TEFECXCONTENEDOR_FIFO
          INSERT INTO SAPR_GTT_TEFECXCONTENEDOR_FIFO (oid_contenedor, gmt_creacion, oid_tipo_contenedor, cod_tipo_contenedor, des_tipo_contenedor, cod_posicion, oid_divisa, oid_denominacion, oid_calidad, bol_disponible, bol_bloqueado, nel_cantidad, num_importe)
          VALUES
            (efetivoContenedor.OID_CONTENEDOR,
             efetivoContenedor.GMT_CREACION,
             efetivoContenedor.OID_TIPO_CONTENEDOR,
             efetivoContenedor.COD_TIPO_CONTENEDOR,
			 efetivoContenedor.DES_TIPO_CONTENEDOR,
			 efetivoContenedor.COD_POSICION,
             efetivoContenedor.OID_DIVISA,
             efetivoContenedor.OID_DENOMINACION,
             efetivoContenedor.OID_CALIDAD,
             efetivoContenedor.BOL_DISPONIBLE,
             efetivoContenedor.BOL_BLOQUEADO,
             efetivoContenedor.NEL_CANTIDAD,
             efetivoContenedor.NUM_IMPORTE);

           par$inserts := par$inserts + sql%rowcount;

        ELSE
          -- se soma acumulada atingiu o limte e o FLAG j? foi alterado, ou seja, j? passou nesse IF a primeira vez?
          -- verifica se o OID_ANTERIOR ? diferente do OID_CONTENEDOR da itera??o atual. Se for diferente, n?o tem nada mais a ser gravado, ent?o SAIR DO LOOP
          IF (var$oid_anterior <> efetivoContenedor.OID_CONTENEDOR) THEN
            EXIT;
          ELSE

            -- grava o item do loop na tabela temporaria SAPR_GTT_TEFECXCONTENEDOR_FIFO
            INSERT INTO SAPR_GTT_TEFECXCONTENEDOR_FIFO (oid_contenedor, gmt_creacion, oid_tipo_contenedor, cod_tipo_contenedor, des_tipo_contenedor, cod_posicion, oid_divisa, oid_denominacion, oid_calidad, bol_disponible, bol_bloqueado, nel_cantidad, num_importe)
          VALUES
            (efetivoContenedor.OID_CONTENEDOR,
             efetivoContenedor.GMT_CREACION,
             efetivoContenedor.OID_TIPO_CONTENEDOR,
             efetivoContenedor.COD_TIPO_CONTENEDOR,
			 efetivoContenedor.DES_TIPO_CONTENEDOR,
			 efetivoContenedor.COD_POSICION,
             efetivoContenedor.OID_DIVISA,
             efetivoContenedor.OID_DENOMINACION,
             efetivoContenedor.OID_CALIDAD,
             efetivoContenedor.BOL_DISPONIBLE,
             efetivoContenedor.BOL_BLOQUEADO,
             efetivoContenedor.NEL_CANTIDAD,
             efetivoContenedor.NUM_IMPORTE);

          par$inserts := par$inserts + sql%rowcount;

          END IF;

        END IF;

      ELSE
        -- grava o item do loop na tabela temporaria SAPR_GTT_TEFECXCONTENEDOR_FIFO

        INSERT INTO SAPR_GTT_TEFECXCONTENEDOR_FIFO (oid_contenedor, gmt_creacion, oid_tipo_contenedor, cod_tipo_contenedor, des_tipo_contenedor, cod_posicion, oid_divisa, oid_denominacion, oid_calidad, bol_disponible, bol_bloqueado, nel_cantidad, num_importe)
          VALUES
            (efetivoContenedor.OID_CONTENEDOR,
             efetivoContenedor.GMT_CREACION,
             efetivoContenedor.OID_TIPO_CONTENEDOR,
             efetivoContenedor.COD_TIPO_CONTENEDOR,
			 efetivoContenedor.DES_TIPO_CONTENEDOR,
			 efetivoContenedor.COD_POSICION,
             efetivoContenedor.OID_DIVISA,
             efetivoContenedor.OID_DENOMINACION,
             efetivoContenedor.OID_CALIDAD,
             efetivoContenedor.BOL_DISPONIBLE,
             efetivoContenedor.BOL_BLOQUEADO,
             efetivoContenedor.NEL_CANTIDAD,
             efetivoContenedor.NUM_IMPORTE);

      par$inserts := par$inserts + sql%rowcount;

      END IF;

      -- se itera??o for a segunda ou superior, armazena o OID_CONTENEDOR
      IF (var$contador > 0) THEN
        var$oid_anterior := efetivoContenedor.OID_CONTENEDOR;
      END IF;

      -- incrementa o contador
      var$contador := var$contador + 1;

    END LOOP;

	-- GENPLATINT-2406, Argentina - CUSTODIA PRECINTADA - Extracción de Monto determinado
	-- Se terminou o loop a primeira validação não foi satisfeita, deverá se considerar todos os precintos que determinado cliente possui segundo filtro informado.
   IF (var$flag_limite = 0 AND var$contador > 0) THEN
       var$flag_limite := 1;
   END IF;
	
    -- se terminou o LOOP e o FLAG n?o foi alterado. ? porque a soma acumulada n?o atingiu o limite necess?rio. Ent?o a procedure n?o devolve valores.
    -- Sen?o, busca os dados gravados que ser?o retornados na procedure na tabela tempor?ria
    IF (var$flag_limite = 1) THEN

      OPEN par$rc_contenedores FOR
        SELECT EFEC.OID_CONTENEDOR,
               EFEC.GMT_CREACION,
               EFEC.OID_TIPO_CONTENEDOR,
               EFEC.COD_TIPO_CONTENEDOR,
               EFEC.DES_TIPO_CONTENEDOR,
               EFEC.COD_POSICION,
               EFEC.OID_DIVISA,
               EFEC.OID_DENOMINACION,
               EFEC.OID_CALIDAD,
               EFEC.BOL_DISPONIBLE,
               EFEC.BOL_BLOQUEADO,
               EFEC.NEL_CANTIDAD,
               EFEC.NUM_IMPORTE
          FROM SAPR_GTT_TEFECXCONTENEDOR_FIFO EFEC;

    par$selects   := par$selects + sql%rowcount;

      -- Recuperar a lista de precintos dos contenedores gravados na tabela temporaria, para carregar o objeto de contenedores no retorno da procedure
      OPEN par$rc_precintos FOR
        SELECT PREC.OID_CONTENEDOR, PREC.COD_PRECINTO
          FROM SAPR_GTT_TOIDS GTTO
         INNER JOIN SAPR_TPRECINTOXCONTENEDOR PREC
            ON PREC.OID_CONTENEDOR = GTTO.OID
           AND GTTO.COD_CALIFICADOR = 'OID_CONTENEDORES_FIFO'
         ORDER BY PREC.OID_CONTENEDOR, PREC.COD_PRECINTO;

   par$selects   := par$selects + sql%rowcount;

    END IF;

   par$selects   := par$selects + sql%rowcount;
    var$registros :=  par$selects + par$inserts;


    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                         const$nombre_func,
                                                         0,
                                                         0,
                                                         0,
                                                         0,
                                                         par$selects,
                                                         var$registros,
                                                         var$inicio,
                                                         null,
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$cod_usuario,
                                                         par$cod_ejecucion);

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           0,
                                                           0,
                                                           0,
                                                           0,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END srecuperar_contenedores_fifo;

  PROCEDURE sgenerar_oids_contenedor_fifo(par$cod_tipo_contenedor IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$oid_delegacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_planta          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_sector          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_cliente         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_subcliente      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_canal           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_subcanal        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_divisa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$oid_denominacion    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$importe             IN gepr_pcomon_###VERSION###.tipo$imp_,
                                          par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

  BEGIN

    par$inserts := 0;
    par$selects := 0;

    -- limpa a tabela tempor?ria SAPR_GTT_TEFECXCONTENEDOR_FIFO, para evitar duplicidade de dados entre as sess?es
    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TOIDS';

    -- se COD_TIPO_CONTENEDOR preenchido E OID_DENOMINACION preenchido
    IF (par$cod_tipo_contenedor IS NOT NULL) AND
       (par$oid_denominacion IS NOT NULL) THEN

      -- grava resultado da consulta na tabela tempor?ria SAPR_GTT_TOIDS
      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT DISTINCT 'OID_CONTENEDORES_FIFO', CONT.OID_CONTENEDOR
          FROM SAPR_TCONTENEDOR CONT
         INNER JOIN SAPR_TTIPO_CONTENEDOR TCON
            ON TCON.OID_TIPO_CONTENEDOR = CONT.OID_TIPO_CONTENEDOR
         INNER JOIN SAPR_TCUENTA CUEN
            ON CUEN.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
         INNER JOIN GEPR_TCLIENTE CLI
            ON CLI.OID_CLIENTE = CUEN.OID_CLIENTE
          LEFT JOIN GEPR_TSUBCLIENTE SCLI
            ON SCLI.OID_CLIENTE = CUEN.OID_SUBCLIENTE
          LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
            ON PTO.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO
         INNER JOIN GEPR_TSUBCANAL SUBC
            ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL
         INNER JOIN GEPR_TCANAL CANA
            ON CANA.OID_CANAL = SUBC.OID_CANAL
         INNER JOIN GEPR_TSECTOR SECT
            ON SECT.OID_SECTOR = CUEN.OID_SECTOR
         INNER JOIN GEPR_TPLANTA PLAN
            ON PLAN.OID_PLANTA = SECT.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION DELE
            ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
         INNER JOIN SAPR_TEFECTIVOXCONTENEDOR EFEC
            ON EFEC.OID_CONTENEDOR = CONT.OID_CONTENEDOR
         WHERE CONT.COD_ESTADO = 'AR'
           AND TCON.COD_TIPO_CONTENEDOR = par$cod_tipo_contenedor
           AND CONT.BOL_PACK_MODULAR = 1
           AND DELE.OID_DELEGACION = par$oid_delegacion
           AND PLAN.OID_PLANTA = par$oid_planta
           AND SECT.OID_SECTOR = par$oid_sector
           AND CLI.OID_CLIENTE = par$oid_cliente
           AND NVL(CUEN.OID_SUBCLIENTE, '-') = NVL(par$oid_subcliente, '-')
           AND NVL(CUEN.OID_PTO_SERVICIO, '-') =
               NVL(par$oid_pto_servicio, '-')
           AND CANA.OID_CANAL = par$oid_canal
           AND SUBC.OID_SUBCANAL = par$oid_subcanal
           AND EFEC.OID_DIVISA = par$oid_divisa
           AND EFEC.OID_DENOMINACION = par$oid_denominacion;

      par$inserts := par$inserts + sql%rowcount;
      par$selects := par$selects + sql%rowcount;

      -- se COD_TIPO_CONTENEDOR preenchido E OID_DENOMINACION vazio
    ELSIF (par$cod_tipo_contenedor IS NOT NULL) AND
          (par$oid_denominacion IS NULL) THEN

      -- grava resultado da consulta na tabela tempor?ria SAPR_GTT_TOIDS
      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT DISTINCT 'OID_CONTENEDORES_FIFO', CONT.OID_CONTENEDOR
          FROM SAPR_TCONTENEDOR CONT
         INNER JOIN SAPR_TTIPO_CONTENEDOR TCON
            ON TCON.OID_TIPO_CONTENEDOR = CONT.OID_TIPO_CONTENEDOR
         INNER JOIN SAPR_TCUENTA CUEN
            ON CUEN.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
         INNER JOIN GEPR_TCLIENTE CLI
            ON CLI.OID_CLIENTE = CUEN.OID_CLIENTE
          LEFT JOIN GEPR_TSUBCLIENTE SCLI
            ON SCLI.OID_CLIENTE = CUEN.OID_SUBCLIENTE
          LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
            ON PTO.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO
         INNER JOIN GEPR_TSUBCANAL SUBC
            ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL
         INNER JOIN GEPR_TCANAL CANA
            ON CANA.OID_CANAL = SUBC.OID_CANAL
         INNER JOIN GEPR_TSECTOR SECT
            ON SECT.OID_SECTOR = CUEN.OID_SECTOR
         INNER JOIN GEPR_TPLANTA PLAN
            ON PLAN.OID_PLANTA = SECT.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION DELE
            ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
         INNER JOIN SAPR_TEFECTIVOXCONTENEDOR EFEC
            ON EFEC.OID_CONTENEDOR = CONT.OID_CONTENEDOR
         WHERE CONT.COD_ESTADO = 'AR'
           AND TCON.COD_TIPO_CONTENEDOR = par$cod_tipo_contenedor
           AND CONT.BOL_PACK_MODULAR = 1
           AND DELE.OID_DELEGACION = par$oid_delegacion
           AND PLAN.OID_PLANTA = par$oid_planta
           AND SECT.OID_SECTOR = par$oid_sector
           AND CLI.OID_CLIENTE = par$oid_cliente
           AND NVL(CUEN.OID_SUBCLIENTE, '-') = NVL(par$oid_subcliente, '-')
           AND NVL(CUEN.OID_PTO_SERVICIO, '-') =
               NVL(par$oid_pto_servicio, '-')
           AND CANA.OID_CANAL = par$oid_canal
           AND SUBC.OID_SUBCANAL = par$oid_subcanal
           AND EFEC.OID_DIVISA = par$oid_divisa;

      par$inserts := par$inserts + sql%rowcount;
      par$selects := par$selects + sql%rowcount;

      -- se COD_TIPO_CONTENEDOR vazio E OID_DENOMINACION preenchido
    ELSIF (par$cod_tipo_contenedor IS NULL) AND
          (par$oid_denominacion IS NOT NULL) THEN

      -- grava resultado da consulta na tabela tempor?ria SAPR_GTT_TOIDS
      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT DISTINCT 'OID_CONTENEDORES_FIFO', CONT.OID_CONTENEDOR
          FROM SAPR_TCONTENEDOR CONT
         INNER JOIN SAPR_TCUENTA CUEN
            ON CUEN.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
         INNER JOIN GEPR_TCLIENTE CLI
            ON CLI.OID_CLIENTE = CUEN.OID_CLIENTE
          LEFT JOIN GEPR_TSUBCLIENTE SCLI
            ON SCLI.OID_CLIENTE = CUEN.OID_SUBCLIENTE
          LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
            ON PTO.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO
         INNER JOIN GEPR_TSUBCANAL SUBC
            ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL
         INNER JOIN GEPR_TCANAL CANA
            ON CANA.OID_CANAL = SUBC.OID_CANAL
         INNER JOIN GEPR_TSECTOR SECT
            ON SECT.OID_SECTOR = CUEN.OID_SECTOR
         INNER JOIN GEPR_TPLANTA PLAN
            ON PLAN.OID_PLANTA = SECT.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION DELE
            ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
         INNER JOIN SAPR_TEFECTIVOXCONTENEDOR EFEC
            ON EFEC.OID_CONTENEDOR = CONT.OID_CONTENEDOR
         WHERE CONT.COD_ESTADO = 'AR'
           AND CONT.BOL_PACK_MODULAR = 1
           AND DELE.OID_DELEGACION = par$oid_delegacion
           AND PLAN.OID_PLANTA = par$oid_planta
           AND SECT.OID_SECTOR = par$oid_sector
           AND CLI.OID_CLIENTE = par$oid_cliente
           AND NVL(CUEN.OID_SUBCLIENTE, '-') = NVL(par$oid_subcliente, '-')
           AND NVL(CUEN.OID_PTO_SERVICIO, '-') =
               NVL(par$oid_pto_servicio, '-')
           AND CANA.OID_CANAL = par$oid_canal
           AND SUBC.OID_SUBCANAL = par$oid_subcanal
           AND EFEC.OID_DIVISA = par$oid_divisa
           AND EFEC.OID_DENOMINACION = par$oid_denominacion;

      par$inserts := par$inserts + sql%rowcount;
      par$selects := par$selects + sql%rowcount;

      -- se COD_TIPO_CONTENEDOR vazio E OID_DENOMINACION vazio
    ELSIF (par$cod_tipo_contenedor IS NULL) AND
          (par$oid_denominacion IS NULL) THEN

      -- grava resultado da consulta na tabela tempor?ria SAPR_GTT_TOIDS
      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT DISTINCT 'OID_CONTENEDORES_FIFO', CONT.OID_CONTENEDOR
          FROM SAPR_TCONTENEDOR CONT
         INNER JOIN SAPR_TTIPO_CONTENEDOR TCON
            ON TCON.OID_TIPO_CONTENEDOR = CONT.OID_TIPO_CONTENEDOR
         INNER JOIN SAPR_TCUENTA CUEN
            ON CUEN.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
         INNER JOIN GEPR_TCLIENTE CLI
            ON CLI.OID_CLIENTE = CUEN.OID_CLIENTE
          LEFT JOIN GEPR_TSUBCLIENTE SCLI
            ON SCLI.OID_CLIENTE = CUEN.OID_SUBCLIENTE
          LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
            ON PTO.OID_PTO_SERVICIO = CUEN.OID_PTO_SERVICIO
         INNER JOIN GEPR_TSUBCANAL SUBC
            ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL
         INNER JOIN GEPR_TCANAL CANA
            ON CANA.OID_CANAL = SUBC.OID_CANAL
         INNER JOIN GEPR_TSECTOR SECT
            ON SECT.OID_SECTOR = CUEN.OID_SECTOR
         INNER JOIN GEPR_TPLANTA PLAN
            ON PLAN.OID_PLANTA = SECT.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION DELE
            ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
         INNER JOIN SAPR_TEFECTIVOXCONTENEDOR EFEC
            ON EFEC.OID_CONTENEDOR = CONT.OID_CONTENEDOR
         WHERE CONT.COD_ESTADO = 'AR'
           AND CONT.BOL_PACK_MODULAR = 1
           AND DELE.OID_DELEGACION = par$oid_delegacion
           AND PLAN.OID_PLANTA = par$oid_planta
           AND SECT.OID_SECTOR = par$oid_sector
           AND CLI.OID_CLIENTE = par$oid_cliente
           AND NVL(CUEN.OID_SUBCLIENTE, '-') = NVL(par$oid_subcliente, '-')
           AND NVL(CUEN.OID_PTO_SERVICIO, '-') =
               NVL(par$oid_pto_servicio, '-')
           AND CANA.OID_CANAL = par$oid_canal
           AND SUBC.OID_SUBCANAL = par$oid_subcanal
           AND EFEC.OID_DIVISA = par$oid_divisa;

      par$inserts := par$inserts + sql%rowcount;
      par$selects := par$selects + sql%rowcount;

    END IF;

  END sgenerar_oids_contenedor_fifo;


  PROCEDURE sconsultar_contenedor_cli(par$codIdioma          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$codTipoContenedor  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$codEstadoConenedor IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$fyhArmadoDesde     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                      par$fyhArmadoHasta     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                      par$bolPackModular     IN INTEGER,
                                      par$codCliente         IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codSubCliente      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codPtoServicio     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codDelegacion      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codPlanta          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codSector          IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codCanal           IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$codSubcanal        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$rcContenedores     OUT SYS_REFCURSOR,
                                      par$rcClientes         OUT SYS_REFCURSOR,
                                      par$rcPrecintos        OUT SYS_REFCURSOR,
                                      par$rcDetalleEfecMP    OUT SYS_REFCURSOR,
                                      par$cod_usuario        in gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$info_ejecucion     IN VARCHAR2,
                                      par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$existeDelegacion INTEGER;
    var$existePlanta     INTEGER;
    var$existeSector     INTEGER;
    var$existeCanal      INTEGER;

    var$inicio TIMESTAMP;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sconsultar_contenedor_cli';
    var$err VARCHAR2(4000);

    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;

  begin

    var$inicio  := SYSTIMESTAMP;
    par$selects := 0;
    par$deletes := 0;
    par$inserts := 0;

    DELETE FROM SAPR_GTT_TOIDS;

    var$deletes := var$deletes + sql%rowcount;

    var$existeDelegacion := 0;
    --insere codDelegacion
    if par$codDelegacion is not null AND par$codDelegacion.COUNT > 0 Then
      for idx in par$codDelegacion.FIRST .. par$codDelegacion.LAST LOOP
        if par$codDelegacion(idx) is not null Then
          var$existeDelegacion := 1;
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'DELEGACIONES', D.OID_DELEGACION
              FROM GEPR_TDELEGACION D
             WHERE D.COD_DELEGACION = par$codDelegacion(idx)
               AND D.OID_DELEGACION NOT IN
                   (SELECT OID
                      FROM SAPR_GTT_TOIDS gti
                     WHERE gti.COD_CALIFICADOR = 'DELEGACIONES');

          var$inserts := var$inserts + sql%rowcount;
          var$selects := var$selects + sql%rowcount;
        end if;
      END LOOP;
    end if;

    var$existePlanta := 0;
    --insere codPlanta
    if par$codPlanta is not null AND par$codPlanta.COUNT > 0 Then
      for idx in par$codPlanta.FIRST .. par$codPlanta.LAST LOOP
        if par$codPlanta(idx) is not null Then
          var$existePlanta := 1;
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'PLANTAS', P.OID_PLANTA
              FROM GEPR_TPLANTA P
             WHERE P.COD_PLANTA = par$codPlanta(idx)
               AND P.OID_PLANTA NOT IN
                   (SELECT OID
                      FROM SAPR_GTT_TOIDS gti
                     WHERE gti.COD_CALIFICADOR = 'PLANTAS')
               AND (var$existeDelegacion = 1 AND
                   P.OID_DELEGACION IN
                   (SELECT OID
                       FROM SAPR_GTT_TOIDS
                      WHERE COD_CALIFICADOR = 'DELEGACIONES') OR
                   var$existeDelegacion = 0);

          var$inserts := var$inserts + sql%rowcount;
          var$selects := var$selects + sql%rowcount;

        end if;
      END LOOP;
    end if;

    var$existeSector := 0;
    --insere codSector
    if par$codSector is not null AND par$codSector.COUNT > 0 THEN
      for idx in par$codSector.FIRST .. par$codSector.LAST LOOP
        if par$codSector(idx) is not null Then
          var$existeSector := 1;
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'SECTORES', S.OID_SECTOR
              FROM GEPR_TSECTOR S
             INNER JOIN GEPR_TPLANTA P
                ON P.OID_PLANTA = S.OID_PLANTA
             WHERE S.COD_SECTOR = par$codSector(idx)
               AND S.OID_SECTOR NOT IN
                   (SELECT OID
                      FROM SAPR_GTT_TOIDS gti
                     WHERE gti.COD_CALIFICADOR = 'SECTORES')
               AND (var$existePlanta = 1 AND
                   S.OID_PLANTA IN
                   (SELECT OID
                       FROM SAPR_GTT_TOIDS
                      WHERE COD_CALIFICADOR = 'PLANTAS') OR
                   var$existePlanta = 0)
               AND (var$existeDelegacion = 1 AND
                   P.OID_DELEGACION IN
                   (SELECT OID
                       FROM SAPR_GTT_TOIDS
                      WHERE COD_CALIFICADOR = 'DELEGACIONES'));

          var$inserts := var$inserts + sql%rowcount;
          var$selects := var$selects + sql%rowcount;

        end if;
      END LOOP;
    end if;

    --insere oidsubcanal
    var$existeCanal := 0;
    if (par$codCanal is not null AND par$codCanal.COUNT > 0) Then
      for idx in par$codCanal.FIRST .. par$codCanal.LAST LOOP
        if par$codCanal(idx) is not null OR
           par$codSubCanal(idx) is not null Then
          var$existeCanal := 1;
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT DISTINCT 'SUBCANAIS', SC.OID_SUBCANAL
              FROM GEPR_TCANAL CA
              LEFT JOIN GEPR_TSUBCANAL SC
                ON SC.OID_CANAL = CA.OID_CANAL
             WHERE (par$codCanal(idx) IS NOT NULL AND
                   CA.COD_CANAL = par$codCanal(idx) OR
                   par$codCanal(idx) IS NULL)
               AND (par$codSubCanal(idx) IS NOT NULL AND
                   SC.COD_SUBCANAL = par$codSubCanal(idx) OR
                   par$codSubCanal(idx) IS NULL);

          var$inserts := var$inserts + sql%rowcount;
          var$selects := var$selects + sql%rowcount;

        end if;
      END LOOP;
    end if;

    --insere clientes
    if par$codCliente is not null AND par$codCliente.COUNT > 0 Then
      for idx in par$codCliente.FIRST .. par$codCliente.LAST LOOP
        INSERT INTO SAPR_GTT_TOIDS
          (COD_CALIFICADOR, OID)
          SELECT 'CUENTAS', CU.OID_CUENTA
            FROM SAPR_TCONTENEDOR C
           INNER JOIN SAPR_VCUENTA CU
              ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
           WHERE CU.COD_CLIENTE = par$codCliente(idx)
             AND (par$codSubCliente(idx) IS NOT NULL AND
                 CU.COD_SUBCLIENTE = par$codSubCliente(idx) OR
                 par$codSubCliente(idx) IS NULL OR
                 CU.COD_SUBCLIENTE IS NULL)
             AND (par$codPtoServicio(idx) IS NOT NULL AND
                 CU.COD_PTO_SERVICIO = par$codPtoServicio(idx) OR
                 par$codPtoServicio(idx) IS NULL OR
                 CU.COD_PTO_SERVICIO IS NULL)
             AND (var$existeSector = 1 AND
                 CU.OID_SECTOR IN
                 (SELECT OID
                     FROM SAPR_GTT_TOIDS
                    WHERE COD_CALIFICADOR = 'SECTORES') OR
                 var$existeSector = 0)
             AND (var$existeCanal = 1 AND
                 CU.OID_SUBCANAL IN
                 (SELECT OID
                     FROM SAPR_GTT_TOIDS
                    WHERE COD_CALIFICADOR = 'SUBCANAIS') OR
                 var$existeCanal = 0)
          UNION

          SELECT 'CUENTAS', CU.OID_CUENTA
            FROM SAPR_TEFECTIVOXCONTENEDOR C
           INNER JOIN SAPR_VCUENTA CU
              ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
           WHERE CU.COD_CLIENTE = par$codCliente(idx)
             AND (par$codSubCliente(idx) IS NOT NULL AND
                 CU.COD_SUBCLIENTE = par$codSubCliente(idx) OR
                 par$codSubCliente(idx) IS NULL OR
                 CU.COD_SUBCLIENTE IS NULL)
             AND (par$codPtoServicio(idx) IS NOT NULL AND
                 CU.COD_PTO_SERVICIO = par$codPtoServicio(idx) OR
                 par$codPtoServicio(idx) IS NULL OR
                 CU.COD_PTO_SERVICIO IS NULL)
             AND (var$existeSector = 1 AND
                 CU.OID_SECTOR IN
                 (SELECT OID
                     FROM SAPR_GTT_TOIDS
                    WHERE COD_CALIFICADOR = 'SECTORES') OR
                 var$existeSector = 0)
             AND (var$existeCanal = 1 AND
                 CU.OID_SUBCANAL IN
                 (SELECT OID
                     FROM SAPR_GTT_TOIDS
                    WHERE COD_CALIFICADOR = 'SUBCANAIS') OR
                 var$existeCanal = 0)
          UNION

          SELECT 'CUENTAS', CU.OID_CUENTA
            FROM SAPR_TMEDIO_PAGOXCONTENEDOR C
           INNER JOIN SAPR_VCUENTA CU
              ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
           WHERE CU.COD_CLIENTE = par$codCliente(idx)
             AND (par$codSubCliente(idx) IS NOT NULL AND
                 CU.COD_SUBCLIENTE = par$codSubCliente(idx) OR
                 par$codSubCliente(idx) IS NULL OR
                 CU.COD_SUBCLIENTE IS NULL)
             AND (par$codPtoServicio(idx) IS NOT NULL AND
                 CU.COD_PTO_SERVICIO = par$codPtoServicio(idx) OR
                 par$codPtoServicio(idx) IS NULL OR
                 CU.COD_PTO_SERVICIO IS NULL)
             AND (var$existeSector = 1 AND
                 CU.OID_SECTOR IN
                 (SELECT OID
                     FROM SAPR_GTT_TOIDS
                    WHERE COD_CALIFICADOR = 'SECTORES') OR
                 var$existeSector = 0)
             AND (var$existeCanal = 1 AND
                 CU.OID_SUBCANAL IN
                 (SELECT OID
                     FROM SAPR_GTT_TOIDS
                    WHERE COD_CALIFICADOR = 'SUBCANAIS') OR
                 var$existeCanal = 0);

        var$inserts := var$inserts + sql%rowcount;
        var$selects := var$selects + sql%rowcount;
      END LOOP;
    end if;

    --insere oid contenedores
    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)
      SELECT 'CONTENEDORES', C.OID_CONTENEDOR
        FROM SAPR_TCONTENEDOR C
       INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON C.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
        LEFT JOIN SAPR_TEFECTIVOXCONTENEDOR EXC
          ON EXC.OID_CONTENEDOR = C.OID_CONTENEDOR
        LEFT JOIN SAPR_TMEDIO_PAGOXCONTENEDOR MPXC
          ON MPXC.OID_CONTENEDOR = C.OID_CONTENEDOR
       WHERE (par$codTipoContenedor IS NOT NULL AND
             TC.COD_TIPO_CONTENEDOR = par$codTipoContenedor OR
             par$codTipoContenedor IS NULL)
         AND (par$codEstadoConenedor IS NOT NULL AND
             C.COD_ESTADO = par$codEstadoConenedor OR
             par$codEstadoConenedor IS NULL)
         AND (par$bolPackModular IS NOT NULL AND
             C.BOL_PACK_MODULAR = par$bolPackModular OR
             par$bolPackModular IS NULL)
         AND (par$fyhArmadoDesde IS NOT NULL AND
             C.GMT_CREACION >= par$fyhArmadoDesde OR
             par$fyhArmadoDesde IS NULL)
         AND (par$fyhArmadoHasta IS NOT NULL AND
             C.GMT_CREACION <= par$fyhArmadoHasta OR
             par$fyhArmadoHasta IS NULL)
         AND (var$existeSector = 1 AND
             C.OID_SECTOR IN
             (SELECT OID
                 FROM SAPR_GTT_TOIDS
                WHERE COD_CALIFICADOR = 'SECTORES') OR
             var$existeSector = 0)
         AND (C.OID_CUENTA_MOVIMIENTO IN
             (SELECT OID
                 FROM SAPR_GTT_TOIDS
                WHERE COD_CALIFICADOR = 'CUENTAS') OR
             EXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT OID
                 FROM SAPR_GTT_TOIDS
                WHERE COD_CALIFICADOR = 'CUENTAS') OR
             MPXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT OID
                 FROM SAPR_GTT_TOIDS
                WHERE COD_CALIFICADOR = 'CUENTAS'));

    var$inserts := var$inserts + sql%rowcount;
    var$selects := var$selects + sql%rowcount;
    COMMIT;

    --busca dados de contenedores
    OPEN par$rcContenedores FOR
      SELECT C.OID_CONTENEDOR,
             TC.COD_TIPO_CONTENEDOR,
             C.COD_ESTADO,
             C.GMT_CREACION,
             S.COD_SECTOR,
             P.COD_PLANTA,
             D.COD_DELEGACION
        FROM SAPR_TCONTENEDOR C
       INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON C.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
       INNER JOIN GEPR_TSECTOR S
          ON C.OID_SECTOR = S.OID_SECTOR
       INNER JOIN GEPR_TPLANTA P
          ON S.OID_PLANTA = P.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION D
          ON P.OID_DELEGACION = D.OID_DELEGACION
       WHERE C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES');

    var$selects := var$selects + sql%rowcount;

    --busca dados dos precintos
    OPEN par$rcPrecintos FOR
      SELECT PXC.OID_CONTENEDOR,
             PXC.OID_PRECINTOXCONTENEDOR,
             PXC.COD_PRECINTO
        FROM SAPR_TPRECINTOXCONTENEDOR PXC
       WHERE PXC.OID_CONTENEDOR IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'CONTENEDORES');

    var$selects := var$selects + sql%rowcount;

    --busca dados dos clientes
    OPEN par$rcClientes FOR
      SELECT C.OID_CONTENEDOR,
             CUMOV.COD_CLIENTE,
             CUMOV.COD_SUBCLIENTE,
             CUMOV.COD_PTO_SERVICIO,
             CUMOV.COD_CANAL,
             CUMOV.COD_SUBCANAL,
             CUMOV.COD_DELEGACION,
             CUMOV.COD_PLANTA,
             CUMOV.COD_SECTOR
        FROM SAPR_TCONTENEDOR C
       INNER JOIN SAPR_VCUENTA CUMOV
          ON C.OID_CUENTA_MOVIMIENTO = CUMOV.OID_CUENTA
       WHERE C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
         AND C.OID_CUENTA_MOVIMIENTO IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
      UNION ALL
      SELECT EXC.OID_CONTENEDOR,
             CUEFEC.COD_CLIENTE,
             CUEFEC.COD_SUBCLIENTE,
             CUEFEC.COD_PTO_SERVICIO,
             CUEFEC.COD_CANAL,
             CUEFEC.COD_SUBCANAL,
             CUEFEC.COD_DELEGACION,
             CUEFEC.COD_PLANTA,
             CUEFEC.COD_SECTOR
        FROM SAPR_TEFECTIVOXCONTENEDOR EXC
       INNER JOIN SAPR_VCUENTA CUEFEC
          ON EXC.OID_CUENTA_MOVIMIENTO = CUEFEC.OID_CUENTA
       WHERE EXC.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
         AND EXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
      UNION ALL
      SELECT MPXC.OID_CONTENEDOR,
             CUMP.COD_CLIENTE,
             CUMP.COD_SUBCLIENTE,
             CUMP.COD_PTO_SERVICIO,
             CUMP.COD_CANAL,
             CUMP.COD_SUBCANAL,
             CUMP.COD_DELEGACION,
             CUMP.COD_PLANTA,
             CUMP.COD_SECTOR
        FROM SAPR_TMEDIO_PAGOXCONTENEDOR MPXC
       INNER JOIN SAPR_VCUENTA CUMP
          ON MPXC.OID_CUENTA_MOVIMIENTO = CUMP.OID_CUENTA
       WHERE MPXC.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
         AND MPXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CUENTAS');

    var$selects := var$selects + sql%rowcount;

    --busca dados de efetivo e MP
    OPEN par$rcDetalleEfecMP FOR
      SELECT EXC.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             DEN.COD_DENOMINACION,
             CAL.COD_CALIDAD,
             NULL COD_TIPO_MEDIO_PAGO,
             NULL COD_MEDIO_PAGO,
             EXC.BOL_DISPONIBLE,
             EXC.BOL_BLOQUEADO,
             SUM(EXC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(EXC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TEFECTIVOXCONTENEDOR EXC
       INNER JOIN SAPR_TCUENTA CUEFEC
          ON EXC.OID_CUENTA_MOVIMIENTO = CUEFEC.OID_CUENTA
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = EXC.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = EXC.OID_DENOMINACION
       INNER JOIN GEPR_TCALIDAD CAL
          ON CAL.OID_CALIDAD = EXC.OID_CALIDAD
       WHERE EXC.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
         AND EXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
       GROUP BY EXC.OID_CONTENEDOR,
                DIV.COD_ISO_DIVISA,
                DEN.COD_DENOMINACION,
                CAL.COD_CALIDAD,
                EXC.BOL_DISPONIBLE,
                EXC.BOL_BLOQUEADO
      UNION ALL
      SELECT MPXC.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             NULL COD_DENOMINACION,
             NULL COD_CALIDAD,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_MEDIO_PAGO,
             MPXC.BOL_DISPONIBLE,
             MPXC.BOL_BLOQUEADO,
             SUM(MPXC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(MPXC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TMEDIO_PAGOXCONTENEDOR MPXC
       INNER JOIN SAPR_TCUENTA CUMP
          ON MPXC.OID_CUENTA_MOVIMIENTO = CUMP.OID_CUENTA
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = MPXC.OID_DIVISA
       INNER JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = MPXC.OID_MEDIO_PAGO
       WHERE MPXC.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
         AND MPXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
       GROUP BY MPXC.OID_CONTENEDOR,
                DIV.COD_ISO_DIVISA,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.COD_MEDIO_PAGO,
                MPXC.BOL_DISPONIBLE,
                MPXC.BOL_BLOQUEADO
      UNION ALL
      SELECT C.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             DEN.COD_DENOMINACION,
             NULL COD_CALIDAD,
             NULL COD_TIPO_MEDIO_PAGO,
             NULL COD_MEDIO_PAGO,
             0 BOL_DISPONIBLE,
             0 BOL_BLOQUEADO,
             SUM(DECEFEC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(DECEFEC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TDECLARADO_EFECTIVO DECEFEC
       INNER JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DECEFEC.OID_REMESA
         AND DECEFEC.OID_BULTO IS NULL
       INNER JOIN SAPR_TCONTENEDOR C
          ON C.OID_CONTENEDOR = R.OID_CONTENEDOR_PADRE
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = DECEFEC.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = DECEFEC.OID_DENOMINACION
       WHERE DECEFEC.COD_NIVEL_DETALLE = 'D'
         AND C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
       GROUP BY C.OID_CONTENEDOR, DIV.COD_ISO_DIVISA, DEN.COD_DENOMINACION
      UNION ALL
      SELECT C.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             DEN.COD_DENOMINACION,
             NULL COD_CALIDAD,
             NULL COD_TIPO_MEDIO_PAGO,
             NULL COD_MEDIO_PAGO,
             0 BOL_DISPONIBLE,
             0 BOL_BLOQUEADO,
             SUM(DECEFEC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(DECEFEC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TDECLARADO_EFECTIVO DECEFEC
       INNER JOIN SAPR_TBULTO B
          ON B.OID_REMESA = DECEFEC.OID_REMESA
         AND B.OID_BULTO = DECEFEC.OID_BULTO
       INNER JOIN SAPR_TCONTENEDOR C
          ON C.OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = DECEFEC.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = DECEFEC.OID_DENOMINACION
       WHERE DECEFEC.COD_NIVEL_DETALLE = 'D'
         AND C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
       GROUP BY C.OID_CONTENEDOR, DIV.COD_ISO_DIVISA, DEN.COD_DENOMINACION
      UNION ALL
      SELECT C.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             NULL COD_DENOMINACION,
             NULL COD_CALIDAD,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_MEDIO_PAGO,
             0 BOL_DISPONIBLE,
             0 BOL_BLOQUEADO,
             SUM(DECMP.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(DECMP.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
       INNER JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DECMP.OID_REMESA
         AND DECMP.OID_BULTO IS NULL
       INNER JOIN SAPR_TCONTENEDOR C
          ON C.OID_CONTENEDOR = R.OID_CONTENEDOR_PADRE
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = DECMP.OID_DIVISA
       INNER JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
       WHERE DECMP.COD_NIVEL_DETALLE = 'D'
         AND C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
       GROUP BY C.OID_CONTENEDOR,
                DIV.COD_ISO_DIVISA,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.COD_MEDIO_PAGO
      UNION ALL
      SELECT C.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             NULL COD_DENOMINACION,
             NULL COD_CALIDAD,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_MEDIO_PAGO,
             0 BOL_DISPONIBLE,
             0 BOL_BLOQUEADO,
             SUM(DECMP.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(DECMP.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
       INNER JOIN SAPR_TBULTO B
          ON B.OID_REMESA = DECMP.OID_REMESA
         AND B.OID_BULTO = DECMP.OID_BULTO
       INNER JOIN SAPR_TCONTENEDOR C
          ON C.OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = DECMP.OID_DIVISA
       INNER JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
       WHERE DECMP.COD_NIVEL_DETALLE = 'D'
         AND C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
       GROUP BY C.OID_CONTENEDOR,
                DIV.COD_ISO_DIVISA,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.COD_MEDIO_PAGO;

    var$selects   := var$selects + sql%rowcount;
    var$registros := var$deletes + var$selects + var$inserts;

    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                         const$nombre_func,
                                                         0,
                                                         0,
                                                         0,
                                                         0,
                                                         par$selects,
                                                         var$registros,
                                                         var$inicio,
                                                         null,
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$cod_usuario,
                                                         par$cod_ejecucion);

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           0,
                                                           0,
                                                           0,
                                                           0,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END sconsultar_contenedor_cli;


PROCEDURE s_ins_tinventario_contenedor(par$codIdioma          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codInventario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$regresarDetalles   IN INTEGER,
                                         par$codDelegacion      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codPlanta          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codSector          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codCliente         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codSubcliente      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codPuntoServicio   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cods_precintos     IN gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$UsuarioCreacion    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$hacer_Commit       IN INTEGER,
                                         par$rc_Inventario      OUT SYS_REFCURSOR,
                                         par$rc_detalle_efec_mp OUT SYS_REFCURSOR) IS

    var$rc_contenedores      gepr_pcomon_###VERSION###.tipo$contenedores;
    var$rc_contenedor        gepr_pcomon_###VERSION###.tipo$contenedor;
    var$row_contenedor       gepr_pcomon_###VERSION###.tipo$contenedor;
    var$codInventario        gepr_pcomon_###VERSION###.tipo$cod_;
    var$codSector            gepr_pcomon_###VERSION###.tipo$cod_;
    var$codEstadoInventario  gepr_pcomon_###VERSION###.tipo$cod_;
    var$desSector            gepr_pcomon_###VERSION###.tipo$cod_;
    var$codCliente           gepr_pcomon_###VERSION###.tipo$cod_;
    var$codSubcliente        gepr_pcomon_###VERSION###.tipo$cod_;
    var$codPuntoServicio     gepr_pcomon_###VERSION###.tipo$cod_;
    var$oidInventario        gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_cuenta_origen    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_contenedor       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_documento        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_grupo_documento  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_movimento gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_saldo     gepr_pcomon_###VERSION###.tipo$oid_;
    var$cantidadLogica       gepr_pcomon_###VERSION###.tipo$cod_;
    var$cantidadInventariada gepr_pcomon_###VERSION###.tipo$cod_;
    var$cods_precintos       varchar2_array;
    var$nel_indice           gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$msgDescuadre         gepr_pcomon_###VERSION###.tipo$obs_;
    var$oidNotificacion      gepr_pcomon_###VERSION###.tipo$oid_;
    var$codTipoDesNotif      gepr_pcomon_###VERSION###.tipo$obss_;
    var$oidTipoDesNotif      gepr_pcomon_###VERSION###.tipo$obss_;
	var$gmt_zero VARCHAR2(50) := GEPR_PUTILIDADES_###VERSION###.fgmt_zero;

  BEGIN
    var$codInventario := par$codInventario;

    IF par$codSector IS NOT NULL AND par$codPlanta IS NOT NULL THEN

      SELECT OID_SECTOR
        INTO var$codSector
        FROM GEPR_TSECTOR
       WHERE COD_SECTOR IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codSector, ',')))
         AND OID_PLANTA = (SELECT OID_PLANTA
                             FROM GEPR_TPLANTA
                            WHERE COD_PLANTA = par$codPlanta);
      SELECT DES_SECTOR
        INTO var$desSector
        FROM GEPR_TSECTOR
       WHERE COD_SECTOR IN
             (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codSector, ',')))
         AND OID_PLANTA = (SELECT OID_PLANTA
                             FROM GEPR_TPLANTA
                            WHERE COD_PLANTA = par$codPlanta);

      IF var$codSector IS NULL THEN
        RAISE_APPLICATION_ERROR(-20001,
                                GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                    'Sector_Nao_Encontrado',
                                                                    gepr_pcomon_###VERSION###.const$CodFunGravarInventario,
                                                                    'GenesisSaldos',
                                                                    NULL,
                                                                    1));

      END IF;

    END IF;

    IF par$codcliente IS NOT NULL THEN
      SELECT OID_CLIENTE
        INTO var$codCliente
        FROM GEPR_TCLIENTE
       WHERE COD_CLIENTE = par$codCliente;
      IF var$codCliente IS NULL THEN
        RAISE_APPLICATION_ERROR(-20001,
                                GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                    'Cliente_Nao_Encontrado',
                                                                    gepr_pcomon_###VERSION###.const$CodFunGravarInventario,
                                                                    'GenesisSaldos',
                                                                    NULL,
                                                                    1));
      END IF;
    END IF;

    IF par$codSubcliente IS NOT NULL THEN
      SELECT OID_SUBCLIENTE
        INTO var$codSubcliente
        FROM GEPR_TSUBCLIENTE
       WHERE COD_SUBCLIENTE = par$codSubcliente;
      IF var$codSubcliente IS NULL THEN
        RAISE_APPLICATION_ERROR(-20001,
                                GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                    'SubCliente_Nao_Encontrado',
                                                                    gepr_pcomon_###VERSION###.const$CodFunGravarInventario,
                                                                    'GenesisSaldos',
                                                                    NULL,
                                                                    1));
      END IF;
    END IF;

    IF par$codPuntoServicio IS NOT NULL THEN
      SELECT OID_PTO_SERVICIO
        INTO var$codPuntoServicio
        FROM GEPR_TPUNTO_SERVICIO
       WHERE COD_PTO_SERVICIO = par$codPuntoServicio;
      IF var$codPuntoServicio IS NULL THEN
        RAISE_APPLICATION_ERROR(-20001,
                                GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                    'PtoServ_Nao_Encontrado',
                                                                    gepr_pcomon_###VERSION###.const$CodFunGravarInventario,
                                                                    'GenesisSaldos',
                                                                    NULL,
                                                                    1));
      END IF;
    END IF;

    SELECT COUNT(DISTINCT(C.OID_CONTENEDOR))
      INTO var$cantidadLogica
      FROM SAPR_TCONTENEDOR C
     INNER JOIN SAPR_VCUENTA CUENTA
        ON C.OID_CUENTA_SALDO = CUENTA.OID_CUENTA
     WHERE (var$codSector IS NULL OR CUENTA.OID_SECTOR = var$codSector)
       AND (var$codCliente IS NULL OR CUENTA.OID_CLIENTE = var$codCliente)
     AND C.COD_ESTADO = 'AR';

    var$oidInventario := sys_guid();
    /*Almacenar las informaciones de la cabecera del inventario*/
    INSERT INTO SAPR_TINVENTARIO
      (OID_INVENTARIO,
       OID_SECTOR,
       COD_INVENTARIO,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       OID_CLIENTE,
       OID_SUBCLIENTE,
       OID_PTO_SERVICIO,
       COD_TIPO_ELEMENTO,
       NEC_CANTIDAD_LOGICA,
       NEC_CANTIDAD_INVENTARIADA,
       BOL_AUTOMATICO)
    VALUES
      (var$oidInventario,
       var$codSector,
       var$codInventario,
       var$gmt_zero,
       par$UsuarioCreacion,
       var$codCliente,
       var$codSubcliente,
       var$codPuntoServicio,
       'C',
       var$cantidadLogica,
       0,
       0);

    /*FIM Almacenar las informaciones de la cabecera del inventario*/

    /*Almacenar las informaciones de los detalles del inventario (contenedores inventariados)*/

    /*
    Si el parametro Inventario.Contenedor est? rellenado
    o  SAPR_TINVENTARIOXCONTENEDOR.OID_CONTENEDOR = OID del contenedor inventariado recibido por el par?metro Contenedor.codPrecinto
    Caso contrario:
    Buscar los contenedores de mayor nivel y con estado = AR (Armado) pertenecientes al:
    Sector, si solamente par?metro Inventario.Sector rellenado;
    Sector y Cliente, si par?metros Inventario.Sector e Inventario.Cliente rellenados
    */

    IF par$cods_precintos IS NOT NULL AND
       NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$cods_precintos) THEN

      var$cods_precintos := gepr_putilidades_###VERSION###.fconverter_array_table(par$cods_precintos);

      for c in (SELECT DISTINCT CONT.OID_CONTENEDOR,
                                OID_CONTENEDOR_PADRE,
                                OID_CONTENEDOR_MAYOR_NIVEL,
                                OID_TIPO_CONTENEDOR,
                                null                       OID_SECTOR,
                                OID_CUENTA_MOVIMIENTO,
                                null                       OID_CLIENTE,
                                null                       OID_SUBCLIENTE,
                                null                       OID_PTO_SERVICIO,
                                null                       OID_CANAL,
                                null                       OID_SUBCANAL,
                                OID_DOCUMENTO,
                                COD_ESTADO,
                                BOL_PACK_MODULAR,
                                FEC_VENCIMIENTO,
                                COD_PUESTO,
                                COD_TIPO_ELEMENTO,
                                OID_PLANTA,
                                null                       OID_DELEGACION,
                                OID_CUENTA_SALDO,
                                OID_GRUPO_DOCUMENTO,
                                null                       COD_TIPO_CONTENEDOR
                  FROM SAPR_TCONTENEDOR CONT
                  LEFT JOIN GEPR_TSECTOR SECT
                    ON SECT.OID_SECTOR = CONT.OID_SECTOR
                 INNER JOIN SAPR_TCUENTA CUE
                    ON CUE.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
                  LEFT JOIN GEPR_TCLIENTE CLI
                    ON CLI.OID_CLIENTE = CUE.OID_CLIENTE
                 INNER JOIN SAPR_TPRECINTOXCONTENEDOR PC
                    ON PC.OID_CONTENEDOR = CONT.OID_CONTENEDOR
                 WHERE CONT.OID_CONTENEDOR_MAYOR_NIVEL IS NULL
                   AND CONT.COD_ESTADO = 'AR'
                   AND (par$codSector IS NULL OR
                       SECT.COD_SECTOR = par$codSector)
                   AND (par$codCliente IS NULL OR
                       CLI.COD_CLIENTE = par$codCliente)
                   AND PC.COD_PRECINTO IN
                       (SELECT COLUMN_VALUE FROM TABLE(var$cods_precintos))) loop

        var$rc_contenedores(var$nel_indice).OID_CONTENEDOR := c.OID_CONTENEDOR;
        var$rc_contenedores(var$nel_indice).OID_CONTENEDOR_PADRE := c.OID_CONTENEDOR_PADRE;
        var$rc_contenedores(var$nel_indice).OID_CONTENEDOR_MAYOR_NIVEL := c.OID_CONTENEDOR_MAYOR_NIVEL;
        var$rc_contenedores(var$nel_indice).OID_TIPO_CONTENEDOR := c.OID_TIPO_CONTENEDOR;
        var$rc_contenedores(var$nel_indice).OID_SECTOR := c.OID_SECTOR;
        var$rc_contenedores(var$nel_indice).OID_CUENTA_MOVIMIENTO := c.OID_CUENTA_MOVIMIENTO;
        var$rc_contenedores(var$nel_indice).OID_CLIENTE := c.OID_CLIENTE;
        var$rc_contenedores(var$nel_indice).OID_SUBCLIENTE := c.OID_SUBCLIENTE;
        var$rc_contenedores(var$nel_indice).OID_PTO_SERVICIO := c.OID_PTO_SERVICIO;
        var$rc_contenedores(var$nel_indice).OID_CANAL := c.OID_CANAL;
        var$rc_contenedores(var$nel_indice).OID_SUBCANAL := c.OID_SUBCANAL;
        var$rc_contenedores(var$nel_indice).OID_DOCUMENTO := c.OID_DOCUMENTO;
        var$rc_contenedores(var$nel_indice).COD_ESTADO := c.COD_ESTADO;
        var$rc_contenedores(var$nel_indice).BOL_PACK_MODULAR := c.BOL_PACK_MODULAR;
        var$rc_contenedores(var$nel_indice).FEC_VENCIMIENTO := c.FEC_VENCIMIENTO;
        var$rc_contenedores(var$nel_indice).COD_PUESTO := c.COD_PUESTO;
        var$rc_contenedores(var$nel_indice).COD_TIPO_ELEMENTO := c.COD_TIPO_ELEMENTO;
        var$rc_contenedores(var$nel_indice).OID_PLANTA := c.OID_PLANTA;
        var$rc_contenedores(var$nel_indice).OID_DELEGACION := c.OID_DELEGACION;
        var$rc_contenedores(var$nel_indice).OID_CUENTA_SALDO := c.OID_CUENTA_SALDO;
        var$rc_contenedores(var$nel_indice).OID_GRUPO_DOCUMENTO := c.OID_GRUPO_DOCUMENTO;
        var$rc_contenedores(var$nel_indice).COD_TIPO_CONTENEDOR := c.COD_TIPO_CONTENEDOR;

        var$nel_indice := var$nel_indice + 1;
      end loop;

      /*Para cada contenedor obtenido con la busqueda GRAVAR
      SAPR_TINVENTARIOXCONTENEDOR.OID_CONTENEDOR = OID del contenedor
      SAPR_TINVENTARIOXCONTENEDOR.COD_ESTADO = E - Encontrado
      SAPR_TINVENTARIOXCONTENEDOR.OID_CUENTA_MOVIMIENTO = OID de la cuenta movimiento vinculada al contenedor
      SAPR_TINVENTARIOXCONTENEDOR.OID_CUENTA_SALDO = OID de la cuenta saldo vinculada al contenedor

      Verificar cual es el tipo de documento vinculado al contenedor (SAPR_TCONTENEDOR.OID_DOCUMENTO o SAPR_TCONTENEDOR.OID_GRUPO_DOCUMENTO):
      Si Documento Individual: SAPR_TINVENTARIOXCONTENEDOR.OID_DOCUMENTO = OID del documento vinculado al contenedor
      Si Documento Agrupador: SAPR_TINVENTARIOXCONTENEDOR.OID_GRUPO_DOCUMENTO = OID del documento agrupador vinculado al contenedor
      */
      IF var$rc_contenedores.COUNT > 0 THEN

        var$cantidadInventariada := var$rc_contenedores.COUNT;
        UPDATE SAPR_TINVENTARIO I
           SET I.NEC_CANTIDAD_INVENTARIADA = var$cantidadInventariada
         WHERE I.OID_INVENTARIO = var$oidInventario;

        FOR idxContenedor IN var$rc_contenedores.FIRST .. var$rc_contenedores.LAST LOOP

          var$row_contenedor := var$rc_contenedores(idxContenedor);

          var$oid_contenedor       := var$row_contenedor.oid_contenedor;
          var$oid_cuenta_movimento := var$row_contenedor.oid_cuenta_movimiento;
          var$oid_cuenta_saldo     := var$row_contenedor.oid_cuenta_saldo;

          /*Verificar cual es el tipo de documento vinculado al contenedor (SAPR_TCONTENEDOR.OID_DOCUMENTO o SAPR_TCONTENEDOR.OID_GRUPO_DOCUMENTO):
          Si Documento Individual: SAPR_TINVENTARIOXCONTENEDOR.OID_DOCUMENTO = OID del documento vinculado al contenedor
          Si Documento Agrupador: SAPR_TINVENTARIOXCONTENEDOR.OID_GRUPO_DOCUMENTO = OID del documento agrupador vinculado al contenedor
          */

          IF var$row_contenedor.OID_DOCUMENTO IS NOT NULL THEN
            var$oid_documento := var$row_contenedor.oid_documento;
          ELSIF var$row_contenedor.OID_GRUPO_DOCUMENTO IS NOT NULL THEN
            var$oid_grupo_documento := var$row_contenedor.oid_grupo_documento;
          END IF;

          INSERT INTO SAPR_TINVENTARIOXCONTENEDOR
            (OID_INVENTARIOXCONTENEDOR,
             OID_INVENTARIO,
             OID_CONTENEDOR,
             OID_CUENTA_MOVIMIENTO,
             OID_CUENTA_SALDO,
             OID_DOCUMENTO,
             OID_GRUPO_DOCUMENTO,
             GMT_CREACION,
             DES_USUARIO_CREACION,
             COD_ESTADO)
          VALUES
            (sys_guid(),
             var$oidInventario,
             var$oid_contenedor,
             var$oid_cuenta_movimento,
             var$oid_cuenta_saldo,
             var$oid_documento,
             var$oid_grupo_documento,
             sysdate,
             par$UsuarioCreacion,
             'E');
        END LOOP;

        IF var$cantidadLogica > var$cantidadInventariada THEN
          var$codEstadoInventario := 'F';
        ELSE
          var$codEstadoInventario := 'S';
        END IF;

       INSERT INTO SAPR_TINVENTARIOXCONTENEDOR
            (OID_INVENTARIOXCONTENEDOR,
             OID_INVENTARIO,
             OID_CONTENEDOR,
             OID_CUENTA_MOVIMIENTO,
             OID_CUENTA_SALDO,
             OID_DOCUMENTO,
             OID_GRUPO_DOCUMENTO,
             GMT_CREACION,
             DES_USUARIO_CREACION,
             COD_ESTADO)
       SELECT DISTINCT sys_guid(),
             var$oidInventario,
             C.OID_CONTENEDOR,
             C.OID_CUENTA_MOVIMIENTO,
             C.OID_CUENTA_SALDO,
             C.OID_DOCUMENTO,
             C.OID_GRUPO_DOCUMENTO,
             sysdate,
             par$UsuarioCreacion,
             var$codEstadoInventario
          FROM SAPR_TCONTENEDOR C
         INNER JOIN SAPR_TPRECINTOXCONTENEDOR PREC
            ON PREC.OID_CONTENEDOR = C.OID_CONTENEDOR
         INNER JOIN SAPR_VCUENTA CUENTA
            ON C.OID_CUENTA_SALDO = CUENTA.OID_CUENTA
         WHERE (var$codSector IS NULL OR CUENTA.OID_SECTOR = var$codSector)
           AND (var$codCliente IS NULL OR CUENTA.OID_CLIENTE = var$codCliente)
         AND C.COD_ESTADO = 'AR' AND NOT EXISTS
         (SELECT COLUMN_VALUE FROM TABLE(var$cods_precintos) WHERE PREC.COD_PRECINTO = COLUMN_VALUE);

      END IF;

    ELSE
      FOR C IN (SELECT CONT.OID_CONTENEDOR,
                       OID_CONTENEDOR_PADRE,
                       OID_CONTENEDOR_MAYOR_NIVEL,
                       OID_TIPO_CONTENEDOR,
                       null                       OID_SECTOR,
                       OID_CUENTA_MOVIMIENTO,
                       null                       OID_CLIENTE,
                       null                       OID_SUBCLIENTE,
                       null                       OID_PTO_SERVICIO,
                       null                       OID_CANAL,
                       null                       OID_SUBCANAL,
                       OID_DOCUMENTO,
                       COD_ESTADO,
                       BOL_PACK_MODULAR,
                       FEC_VENCIMIENTO,
                       COD_PUESTO,
                       COD_TIPO_ELEMENTO,
                       OID_PLANTA,
                       null                       OID_DELEGACION,
                       OID_CUENTA_SALDO,
                       OID_GRUPO_DOCUMENTO,
                       null                       COD_TIPO_CONTENEDOR
                  FROM SAPR_TCONTENEDOR CONT
                  LEFT JOIN GEPR_TSECTOR SECT
                    ON SECT.OID_SECTOR = CONT.OID_SECTOR
                 INNER JOIN SAPR_TCUENTA CUE
                    ON CUE.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
                  LEFT JOIN GEPR_TCLIENTE CLI
                    ON CLI.OID_CLIENTE = CUE.OID_CLIENTE
                 WHERE CONT.OID_CONTENEDOR_MAYOR_NIVEL IS NULL
                   AND CONT.COD_ESTADO = 'AR'
                   AND (par$codSector IS NULL OR
                       SECT.COD_SECTOR = par$codSector)
                   AND (par$codCliente IS NULL OR
                       CLI.COD_CLIENTE = par$codCliente)) loop

        var$rc_contenedores(var$nel_indice).OID_CONTENEDOR := C.OID_CONTENEDOR;
        var$rc_contenedores(var$nel_indice).OID_CONTENEDOR_PADRE := C.OID_CONTENEDOR_PADRE;
        var$rc_contenedores(var$nel_indice).OID_CONTENEDOR_MAYOR_NIVEL := C.OID_CONTENEDOR_MAYOR_NIVEL;
        var$rc_contenedores(var$nel_indice).OID_TIPO_CONTENEDOR := C.OID_TIPO_CONTENEDOR;
        var$rc_contenedores(var$nel_indice).OID_SECTOR := C.OID_SECTOR;
        var$rc_contenedores(var$nel_indice).OID_CUENTA_MOVIMIENTO := C.OID_CUENTA_MOVIMIENTO;
        var$rc_contenedores(var$nel_indice).OID_CLIENTE := C.OID_CLIENTE;
        var$rc_contenedores(var$nel_indice).OID_SUBCLIENTE := C.OID_SUBCLIENTE;
        var$rc_contenedores(var$nel_indice).OID_PTO_SERVICIO := C.OID_PTO_SERVICIO;
        var$rc_contenedores(var$nel_indice).OID_CANAL := C.OID_CANAL;
        var$rc_contenedores(var$nel_indice).OID_SUBCANAL := C.OID_SUBCANAL;
        var$rc_contenedores(var$nel_indice).OID_DOCUMENTO := C.OID_DOCUMENTO;
        var$rc_contenedores(var$nel_indice).COD_ESTADO := C.COD_ESTADO;
        var$rc_contenedores(var$nel_indice).BOL_PACK_MODULAR := C.BOL_PACK_MODULAR;
        var$rc_contenedores(var$nel_indice).FEC_VENCIMIENTO := C.FEC_VENCIMIENTO;
        var$rc_contenedores(var$nel_indice).COD_PUESTO := C.COD_PUESTO;
        var$rc_contenedores(var$nel_indice).COD_TIPO_ELEMENTO := C.COD_TIPO_ELEMENTO;
        var$rc_contenedores(var$nel_indice).OID_PLANTA := C.OID_PLANTA;
        var$rc_contenedores(var$nel_indice).OID_DELEGACION := C.OID_DELEGACION;
        var$rc_contenedores(var$nel_indice).OID_CUENTA_SALDO := C.OID_CUENTA_SALDO;
        var$rc_contenedores(var$nel_indice).OID_GRUPO_DOCUMENTO := C.OID_GRUPO_DOCUMENTO;
        var$rc_contenedores(var$nel_indice).COD_TIPO_CONTENEDOR := C.COD_TIPO_CONTENEDOR;

        var$nel_indice := var$nel_indice + 1;

      end loop;

      /*Para cada contenedor obtenido con la busqueda GRAVAR
      SAPR_TINVENTARIOXCONTENEDOR.OID_CONTENEDOR = OID del contenedor
      SAPR_TINVENTARIOXCONTENEDOR.COD_ESTADO = E - Encontrado
      SAPR_TINVENTARIOXCONTENEDOR.OID_CUENTA_MOVIMIENTO = OID de la cuenta movimiento vinculada al contenedor
      SAPR_TINVENTARIOXCONTENEDOR.OID_CUENTA_SALDO = OID de la cuenta saldo vinculada al contenedor

      Verificar cual es el tipo de documento vinculado al contenedor (SAPR_TCONTENEDOR.OID_DOCUMENTO o SAPR_TCONTENEDOR.OID_GRUPO_DOCUMENTO):
      Si Documento Individual: SAPR_TINVENTARIOXCONTENEDOR.OID_DOCUMENTO = OID del documento vinculado al contenedor
      Si Documento Agrupador: SAPR_TINVENTARIOXCONTENEDOR.OID_GRUPO_DOCUMENTO = OID del documento agrupador vinculado al contenedor
      */
      IF var$rc_contenedores.COUNT > 0 THEN

        var$cantidadInventariada := var$rc_contenedores.COUNT;
        UPDATE SAPR_TINVENTARIO I
           SET I.NEC_CANTIDAD_INVENTARIADA = var$cantidadInventariada
         WHERE I.OID_INVENTARIO = var$oidInventario;

        FOR idxContenedor IN var$rc_contenedores.FIRST .. var$rc_contenedores.LAST LOOP

          var$row_contenedor := var$rc_contenedores(idxContenedor);

          var$oid_contenedor       := var$row_contenedor.oid_contenedor;
          var$oid_cuenta_movimento := var$row_contenedor.oid_cuenta_movimiento;
          var$oid_cuenta_saldo     := var$row_contenedor.oid_cuenta_saldo;

          /*Verificar cual es el tipo de documento vinculado al contenedor (SAPR_TCONTENEDOR.OID_DOCUMENTO o SAPR_TCONTENEDOR.OID_GRUPO_DOCUMENTO):
          Si Documento Individual: SAPR_TINVENTARIOXCONTENEDOR.OID_DOCUMENTO = OID del documento vinculado al contenedor
          Si Documento Agrupador: SAPR_TINVENTARIOXCONTENEDOR.OID_GRUPO_DOCUMENTO = OID del documento agrupador vinculado al contenedor
          */

          IF var$row_contenedor.OID_DOCUMENTO IS NOT NULL THEN
            var$oid_documento := var$row_contenedor.oid_documento;
          ELSIF var$row_contenedor.OID_GRUPO_DOCUMENTO IS NOT NULL THEN
            var$oid_grupo_documento := var$row_contenedor.oid_grupo_documento;
          END IF;

          INSERT INTO SAPR_TINVENTARIOXCONTENEDOR
            (OID_INVENTARIOXCONTENEDOR,
             OID_INVENTARIO,
             OID_CONTENEDOR,
             OID_CUENTA_MOVIMIENTO,
             OID_CUENTA_SALDO,
             OID_DOCUMENTO,
             OID_GRUPO_DOCUMENTO,
             GMT_CREACION,
             DES_USUARIO_CREACION,
             COD_ESTADO)
          VALUES
            (sys_guid(),
             var$oidInventario,
             var$oid_contenedor,
             var$oid_cuenta_movimento,
             var$oid_cuenta_saldo,
             var$oid_documento,
             var$oid_grupo_documento,
             sysdate,
             par$UsuarioCreacion,
             'E');
        END LOOP;
      END IF;
    END IF;

    IF par$hacer_Commit = 1 THEN
      COMMIT;
    END IF;

    IF par$regresarDetalles = 1 THEN
      OPEN par$rc_Inventario FOR
        SELECT INV.COD_INVENTARIO,
               INV.GMT_CREACION              AS FECHAHORAINVENTARIO,
               INV.DES_USUARIO_CREACION      AS DESUSUARIO,
               INV.NEC_CANTIDAD_LOGICA       AS CANTIDADLOGICA,
               INV.NEC_CANTIDAD_INVENTARIADA AS CANTIDADINVENTARIADA,
               INVC.COD_ESTADO               AS CODESTADOCONTINVENTARIO,
               CONT.OID_CONTENEDOR,
               TPCONT.COD_TIPO_CONTENEDOR,
               TPCONT.DES_TIPO_CONTENEDOR,
               CONT.GMT_CREACION             AS fechaArmado,
               PRECXCONT.COD_PRECINTO,
               DELE.OID_DELEGACION,
               DELE.COD_DELEGACION,
               PLAN.OID_PLANTA,
               PLAN.COD_PLANTA,
               SECT.OID_SECTOR,
               SECT.COD_SECTOR,
               CONTXPOSI.COD_POSICION,
               CUEN.OID_CLIENTE,
               CUEN.OID_SUBCLIENTE,
               CUEN.OID_PTO_SERVICIO,
               CANA.OID_CANAL,
               CANA.COD_CANAL,
               SUBC.OID_SUBCANAL,
               SUBC.COD_SUBCANAL
          FROM SAPR_TCONTENEDOR CONT
         INNER JOIN SAPR_TTIPO_CONTENEDOR TPCONT
            ON TPCONT.OID_TIPO_CONTENEDOR = CONT.OID_TIPO_CONTENEDOR
         INNER JOIN SAPR_TPRECINTOXCONTENEDOR PRECXCONT
            ON PRECXCONT.OID_CONTENEDOR = CONT.OID_CONTENEDOR
          LEFT JOIN SAPR_TPOSICION_SECTOR CONTXPOSI
            ON CONTXPOSI.OID_CONTENEDOR = CONT.OID_CONTENEDOR
          LEFT JOIN SAPR_TCUENTA CUEN
            ON CUEN.OID_CUENTA = CONT.OID_CUENTA_MOVIMIENTO
          LEFT JOIN GEPR_TSUBCANAL SUBC
            ON SUBC.OID_SUBCANAL = CUEN.OID_SUBCANAL
          LEFT JOIN GEPR_TCANAL CANA
            ON CANA.OID_CANAL = SUBC.OID_CANAL
         INNER JOIN GEPR_TSECTOR SECT
            ON SECT.OID_SECTOR = CONT.OID_SECTOR
         INNER JOIN GEPR_TPLANTA PLAN
            ON PLAN.OID_PLANTA = SECT.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION DELE
            ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
         INNER JOIN SAPR_TINVENTARIOXCONTENEDOR INVC
            ON INVC.OID_CONTENEDOR = CONT.OID_CONTENEDOR
         INNER JOIN SAPR_TINVENTARIO INV
            ON INV.OID_INVENTARIO = INVC.OID_INVENTARIO
         WHERE INV.OID_INVENTARIO = var$oidInventario;

      OPEN par$rc_detalle_efec_mp FOR
        SELECT EFEC.OID_CONTENEDOR,
               DIV.OID_DIVISA,
               DIV.COD_ISO_DIVISA,
               DIV.DES_DIVISA,
               DIV.COD_SIMBOLO,
               DEN.OID_DENOMINACION,
               DEN.COD_DENOMINACION,
               CAL.OID_CALIDAD,
               CAL.COD_CALIDAD,
               UNI.OID_UNIDAD_MEDIDA,
               EFEC.COD_NIVEL_DETALLE,
               EFEC.BOL_DISPONIBLE,
               EFEC.BOL_BLOQUEADO,
               NULL COD_TIPO_MEDIO_PAGO,
               NULL COD_MEDIO_PAGO,
               NULL DES_MEDIO_PAGO,
               SUM(EFEC.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(EFEC.NUM_IMPORTE) NUM_IMPORTE
          FROM SAPR_TEFECTIVOXCONTENEDOR EFEC
         INNER JOIN SAPR_TCONTENEDOR CONT
            ON CONT.OID_CONTENEDOR = EFEC.OID_CONTENEDOR
         INNER JOIN GEPR_TDIVISA DIV
            ON DIV.OID_DIVISA = EFEC.OID_DIVISA
          LEFT JOIN GEPR_TDENOMINACION DEN
            ON DEN.OID_DENOMINACION = EFEC.OID_DENOMINACION
           AND DEN.OID_DIVISA = DIV.OID_DIVISA
          LEFT JOIN GEPR_TCALIDAD CAL
            ON CAL.OID_CALIDAD = EFEC.OID_CALIDAD
         LEFT JOIN GEPR_TUNIDAD_MEDIDA UNI
            ON UNI.OID_UNIDAD_MEDIDA = EFEC.OID_UNIDAD_MEDIDA
         GROUP BY EFEC.OID_CONTENEDOR,
                  DIV.OID_DIVISA,
                  DIV.COD_ISO_DIVISA,
                  DIV.DES_DIVISA,
                  DIV.COD_SIMBOLO,
                  DEN.OID_DENOMINACION,
                  DEN.COD_DENOMINACION,
                  CAL.OID_CALIDAD,
                  CAL.COD_CALIDAD,
                  UNI.OID_UNIDAD_MEDIDA,
                  EFEC.COD_NIVEL_DETALLE,
                  EFEC.BOL_DISPONIBLE,
                  EFEC.BOL_BLOQUEADO
        UNION ALL
        SELECT MPG.OID_CONTENEDOR,
               DIV.OID_DIVISA,
               DIV.COD_ISO_DIVISA,
               DIV.DES_DIVISA,
               DIV.COD_SIMBOLO,
               NULL OID_DENOMINACION,
               NULL COD_DENOMINACION,
               NULL OID_CALIDAD,
               NULL COD_CALIDAD,
               NULL OID_UNIDAD_MEDIDA,
               NULL COD_NIVEL_DETALLE,
               NULL BOL_DISPONIBLE,
               NULL BOL_BLOQUEADO,
               MP.COD_TIPO_MEDIO_PAGO,
               MP.OID_MEDIO_PAGO,
               MP.DES_MEDIO_PAGO,
               SUM(MPG.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(MPG.NUM_IMPORTE) NUM_IMPORTE
          FROM SAPR_TMEDIO_PAGOXCONTENEDOR MPG
         INNER JOIN SAPR_TCONTENEDOR CONT
            ON CONT.OID_CONTENEDOR = MPG.OID_CONTENEDOR
         INNER JOIN GEPR_TDIVISA DIV
            ON DIV.OID_DIVISA = MPG.OID_DIVISA
          LEFT JOIN GEPR_TMEDIO_PAGO MP
            ON MP.OID_MEDIO_PAGO = MPG.OID_MEDIO_PAGO
           AND MP.OID_DIVISA = DIV.OID_DIVISA
         GROUP BY MPG.OID_CONTENEDOR,
                  DIV.OID_DIVISA,
                  DIV.COD_ISO_DIVISA,
                  DIV.DES_DIVISA,
                  DIV.COD_SIMBOLO,
                  MP.COD_TIPO_MEDIO_PAGO,
                  MP.OID_MEDIO_PAGO,
                  MP.DES_MEDIO_PAGO;
    END IF;

    IF var$cantidadLogica <> var$cantidadInventariada THEN

      SELECT VALOR
      INTO var$msgDescuadre
      FROM
      (SELECT VALOR
      FROM GEPR_TDICCIONARIO D
      WHERE D.COD_IDIOMA = par$codIdioma
      AND D.COD_FUNCIONALIDAD = 'NOTIFICACION_DESCUADRE_INVENTARIO'
      AND D.COD_EXPRESION = 'MSG_DESCUADRE'
      UNION
      SELECT VALOR
      FROM GEPR_TDICCIONARIO D
      WHERE D.BOL_DEFECTO = 1
      AND D.COD_FUNCIONALIDAD = 'NOTIFICACION_DESCUADRE_INVENTARIO'
      AND D.COD_EXPRESION = 'MSG_DESCUADRE') WHERE ROWNUM = 1;

    var$msgDescuadre := REPLACE(SRCSTR => var$msgDescuadre,
      OLDSUB => '{COD_INVENTARIO}',NEWSUB => var$codInventario);

      var$msgDescuadre := REPLACE(SRCSTR => var$msgDescuadre,
      OLDSUB => '{DES_SECTOR}',NEWSUB => var$desSector);


      var$codTipoDesNotif(0) := 'SECTOR';

      var$oidTipoDesNotif(0) := var$codSector;

      SAPR_PNOTIFICACION_###VERSION###.sins_notificacion(par$codIdioma,
                                                    'DESCUADREINVENT',
                                                    var$msgDescuadre,
                                                    NULL,
                                                    var$codTipoDesNotif,
                                                    var$oidTipoDesNotif,
                                                    'AUTOMATICO',
                                                    var$oidNotificacion);
    END IF;

  END s_ins_tinventario_contenedor;

 PROCEDURE s_consultar_invent_contenedor(par$codIdioma                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codInventario            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$fechaHoraInventarioDesde IN DATE,
                                          par$fechaHoraInventarioHasta IN DATE,
                                          par$codDelegacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codPlanta                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codSector                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codCliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codSubcliente            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$codPuntoServicio         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$rc_Inventario            OUT SYS_REFCURSOR,
                                          par$rc_detalle_efec_mp       OUT SYS_REFCURSOR,
                                          par$cod_usuario              in gepr_pcomon_###VERSION###.tipo$cod_,
                                          par$info_ejecucion           IN VARCHAR2,
                                          par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$rc_contenedores      gepr_pcomon_###VERSION###.tipo$contenedores;
    var$rc_contenedor        gepr_pcomon_###VERSION###.tipo$contenedor;
    var$row_contenedor       gepr_pcomon_###VERSION###.tipo$contenedor;
    var$codInventario        gepr_pcomon_###VERSION###.tipo$cod_;
    var$codSector            gepr_pcomon_###VERSION###.tipo$cod_;
    var$codCliente           gepr_pcomon_###VERSION###.tipo$cod_;
    var$codSubcliente        gepr_pcomon_###VERSION###.tipo$cod_;
    var$codPuntoServicio     gepr_pcomon_###VERSION###.tipo$cod_;
    var$oidInventario        gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_cuenta_origen    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_contenedor       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_documento        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_grupo_documento  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_movimento gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_saldo     gepr_pcomon_###VERSION###.tipo$oid_;

     var$inicio TIMESTAMP;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 's_consultar_invent_contenedor';
    var$err VARCHAR2(4000);

  BEGIN

   var$inicio  := SYSTIMESTAMP;
    par$selects := 0;

    OPEN par$rc_Inventario FOR
      SELECT
      INV.OID_INVENTARIO,
      INV.COD_INVENTARIO,
      fn_gmt_delegacion_###VERSION###(DELE.OID_DELEGACION,INV.GMT_CREACION) AS FECHAHORAINVENTARIO,
      INV.DES_USUARIO_CREACION AS DESUSUARIO,
      INV.NEC_CANTIDAD_LOGICA AS CANTIDADLOGICA,
      INV.NEC_CANTIDAD_INVENTARIADA AS CANTIDADINVENTARIADA,
      DELE.OID_DELEGACION,
      DELE.COD_DELEGACION,
      DELE.DES_DELEGACION,
      PLAN.OID_PLANTA,
      PLAN.COD_PLANTA,
      PLAN.DES_PLANTA,
      SEC.OID_SECTOR,
      SEC.COD_SECTOR,
      SEC.DES_SECTOR,
      CLI.OID_CLIENTE,
      CLI.COD_CLIENTE,
      CLI.DES_CLIENTE,
      SBC.OID_SUBCLIENTE,
      SBC.COD_SUBCLIENTE,
      SBC.DES_SUBCLIENTE,
      PTO.OID_PTO_SERVICIO,
      PTO.COD_PTO_SERVICIO,
      PTO.DES_PTO_SERVICIO,
      NULL CODESTADOCONTINVENTARIO,
      NULL OID_CONTENEDOR,
      NULL fechaArmado,
      NULL COD_PRECINTO,
      NULL OID_CANAL,
      NULL COD_CANAL,
      NULL DES_CANAL,
      NULL OID_SUBCANAL,
      NULL COD_SUBCANAL,
      NULL DES_SUBCANAL
      FROM SAPR_TINVENTARIO INV
           INNER JOIN GEPR_TSECTOR SEC
                 ON SEC.OID_SECTOR = INV.OID_SECTOR
           INNER JOIN GEPR_TPLANTA PLAN
                 ON PLAN.OID_PLANTA = SEC.OID_PLANTA
           INNER JOIN GEPR_TDELEGACION DELE
                 ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
           LEFT JOIN GEPR_TCLIENTE CLI
                ON CLI.OID_CLIENTE = INV.OID_CLIENTE
           LEFT JOIN GEPR_TSUBCLIENTE SBC
                ON SBC.OID_SUBCLIENTE = INV.OID_SUBCLIENTE
           LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
                ON PTO.OID_PTO_SERVICIO = INV.OID_PTO_SERVICIO
      WHERE INV.COD_TIPO_ELEMENTO = 'C'
              AND (par$codInventario IS NULL OR INV.COD_INVENTARIO = par$codInventario)
              AND (par$codSector IS NULL OR SEC.OID_SECTOR IN (SELECT OID_SECTOR
                                                                      FROM GEPR_TSECTOR
                                                                      WHERE COD_SECTOR IN (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codSector, ',')))
                                                                      AND OID_PLANTA = (SELECT OID_PLANTA
                                                                                         FROM GEPR_TPLANTA
                                                                                        WHERE COD_PLANTA = par$codPlanta)))
              AND (par$codCliente IS NULL OR CLI.OID_CLIENTE IN (SELECT OID_CLIENTE
                                                                      FROM GEPR_TCLIENTE
                                                                      WHERE COD_CLIENTE IN (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codCliente, ',')))))
              AND (par$codSubcliente IS NULL OR SBC.OID_SUBCLIENTE IN (SELECT OID_SUBCLIENTE
                                                                      FROM GEPR_TSUBCLIENTE
                                                                      WHERE COD_SUBCLIENTE IN (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codSubcliente, ',')))))
              AND (par$codPuntoServicio IS NULL OR PTO.OID_PTO_SERVICIO IN (SELECT OID_PTO_SERVICIO
                                                                      FROM GEPR_TPUNTO_SERVICIO
                                                                      WHERE COD_PTO_SERVICIO IN (SELECT * FROM TABLE(STRING_TO_ARRAY_###VERSION### (par$codPuntoServicio, ',')))))
              AND INV.GMT_CREACION BETWEEN par$fechaHoraInventarioDesde AND par$fechaHoraInventarioHasta
      ORDER BY INV.GMT_CREACION DESC;

     par$selects := par$selects + sql%rowcount;

    OPEN par$rc_detalle_efec_mp FOR
      SELECT EFEC.OID_CONTENEDOR,
             DIV.OID_DIVISA,
             DIV.COD_ISO_DIVISA,
             DIV.DES_DIVISA,
             DIV.COD_SIMBOLO,
             DEN.OID_DENOMINACION,
             DEN.COD_DENOMINACION,
             CAL.OID_CALIDAD,
             CAL.COD_CALIDAD,
             UNI.OID_UNIDAD_MEDIDA,
             EFEC.COD_NIVEL_DETALLE,
             EFEC.BOL_DISPONIBLE,
             EFEC.BOL_BLOQUEADO,
             NULL COD_TIPO_MEDIO_PAGO,
             NULL COD_MEDIO_PAGO,
             NULL DES_MEDIO_PAGO,
             SUM(EFEC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(EFEC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TEFECTIVOXCONTENEDOR EFEC
       INNER JOIN SAPR_TCONTENEDOR CONT
          ON CONT.OID_CONTENEDOR = EFEC.OID_CONTENEDOR
       INNER JOIN SAPR_TINVENTARIOXCONTENEDOR INVC
          ON INVC.OID_CONTENEDOR = CONT.OID_CONTENEDOR
       INNER JOIN SAPR_TINVENTARIO INV
          ON INV.OID_INVENTARIO = INVC.OID_INVENTARIO
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = EFEC.OID_DIVISA
        LEFT JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = EFEC.OID_DENOMINACION
         AND DEN.OID_DIVISA = DIV.OID_DIVISA
        LEFT JOIN GEPR_TCALIDAD CAL
          ON CAL.OID_CALIDAD = EFEC.OID_CALIDAD
       LEFT JOIN GEPR_TUNIDAD_MEDIDA UNI
          ON UNI.OID_UNIDAD_MEDIDA = EFEC.OID_UNIDAD_MEDIDA
       GROUP BY EFEC.OID_CONTENEDOR,
                DIV.OID_DIVISA,
                DIV.COD_ISO_DIVISA,
                DIV.DES_DIVISA,
                DIV.COD_SIMBOLO,
                DEN.OID_DENOMINACION,
                DEN.COD_DENOMINACION,
                CAL.OID_CALIDAD,
                CAL.COD_CALIDAD,
                UNI.OID_UNIDAD_MEDIDA,
                EFEC.COD_NIVEL_DETALLE,
                EFEC.BOL_DISPONIBLE,
                EFEC.BOL_BLOQUEADO
      UNION ALL
      SELECT MPG.OID_CONTENEDOR,
             DIV.OID_DIVISA,
             DIV.COD_ISO_DIVISA,
             DIV.DES_DIVISA,
             DIV.COD_SIMBOLO,
             NULL OID_DENOMINACION,
             NULL COD_DENOMINACION,
             NULL OID_CALIDAD,
             NULL COD_CALIDAD,
             NULL OID_UNIDAD_MEDIDA,
             NULL COD_NIVEL_DETALLE,
             NULL BOL_DISPONIBLE,
             NULL BOL_BLOQUEADO,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.OID_MEDIO_PAGO,
             MP.DES_MEDIO_PAGO,
             SUM(MPG.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(MPG.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TMEDIO_PAGOXCONTENEDOR MPG
       INNER JOIN SAPR_TCONTENEDOR CONT
          ON CONT.OID_CONTENEDOR = MPG.OID_CONTENEDOR
       INNER JOIN SAPR_TINVENTARIOXCONTENEDOR INVC
          ON INVC.OID_CONTENEDOR = CONT.OID_CONTENEDOR
       INNER JOIN SAPR_TINVENTARIO INV
          ON INV.OID_INVENTARIO = INVC.OID_INVENTARIO
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = MPG.OID_DIVISA
        LEFT JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = MPG.OID_MEDIO_PAGO
         AND MP.OID_DIVISA = DIV.OID_DIVISA
       GROUP BY MPG.OID_CONTENEDOR,
                DIV.OID_DIVISA,
                DIV.COD_ISO_DIVISA,
                DIV.DES_DIVISA,
                DIV.COD_SIMBOLO,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.OID_MEDIO_PAGO,
                MP.DES_MEDIO_PAGO;

   par$selects := par$selects + sql%rowcount;

   gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                         const$nombre_func,
                                                         0,
                                                         0,
                                                         0,
                                                         0,
                                                         par$selects,
                                                         par$selects,
                                                         var$inicio,
                                                         null,
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$cod_usuario,
                                                         par$cod_ejecucion);

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           0,
                                                           0,
                                                           0,
                                                           0,
                                                           par$selects,
                                                           par$selects,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);
  END s_consultar_invent_contenedor;


procedure sins_tprecintoxcontenedor(par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                      par$aprco_oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oids_,
                                      par$aprco_bol_precinto_autom   in gepr_pcomon_###VERSION###.tipo$nbols_,
                                      par$cod_usuario                in gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_) as
  begin

    par$inserts := 0;

    if par$aprco_precintos_contenedor is not null and
       par$aprco_precintos_contenedor.count > 0 then

      for idx in par$aprco_precintos_contenedor.first .. par$aprco_precintos_contenedor.last loop

        insert into sapr_tprecintoxcontenedor
          (oid_precintoxcontenedor,
           oid_contenedor,
           cod_precinto,
           gmt_creacion,
           des_usuario_creacion,
           gmt_modificacion,
           des_usuario_modificacion,
           bol_precinto_automatico)
        values
          (sys_guid(),
           par$aprco_oid_contenedor(idx),
           par$aprco_precintos_contenedor(idx),
           sysdate,
           par$cod_usuario,
           sysdate,
           par$cod_usuario,
           par$aprco_bol_precinto_autom(idx));

        par$inserts := par$inserts + sql%rowcount;

      end loop;

    end if;

  end sins_tprecintoxcontenedor;


  -- faz o calculo da data devencimento do contenedor
  function fcalcular_fec_vencimento_cont(par$aefco_oid_divisa in gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$aefco_oid_denom  in gepr_pcomon_###VERSION###.tipo$oids_,
                                         par$bol_acepta_pico  in gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$cod_cultura      in gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$selects          out gepr_pcomon_###VERSION###.tipo$nel_)
    return gepr_pcomon_###VERSION###.tipo$fyh_ is
    var$neldiasvencimento   gepr_pcomon_###VERSION###.tipo$nel_;
    var$oids_divisas        gepr_pcomon_###VERSION###.tipo$oids_;
    var$oids_denominaciones gepr_pcomon_###VERSION###.tipo$oids_;
    var$fyhvencimento       gepr_pcomon_###VERSION###.tipo$fyh_;
  begin

    par$selects := 0;
    -- se o contenedor for do tipo pack modular

    if par$aefco_oid_divisa is not null then

      -- faz o calculo da data de vencimento.
      if par$bol_acepta_pico > 0 then

        -- da um distinct na cole??o de divisas
        var$oids_divisas := gepr_putilidades_###VERSION###.fdistinct_oids(par$array_Items => par$aefco_oid_divisa);

        if var$oids_divisas is not null and var$oids_divisas.count > 1 then

          -- retorna erro, pois pack modular so pode ter uma divisa.
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$codcultura        => par$cod_cultura,
                                                                      par$codchave          => 'MsgMasDeUnaDivisa',
                                                                      par$codfuncionalidad  => gepr_pcomon_###VERSION###.const$codfuncdicgrabardoccont,
                                                                      par$codaplicacion     => gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                      par$colparametros     => null,
                                                                      par$bolmarcarinitexto => 1));

        else

          begin

            select div.nec_dias_vencimiento
              into var$neldiasvencimento
              from gepr_tdivisa div
             where div.oid_divisa = var$oids_divisas(1);

          exception
            when no_data_found then
              var$neldiasvencimento := null;
          end;

          par$selects := +sql%ROWCOUNT;

          if var$neldiasvencimento is not null then
            var$fyhvencimento := sysdate + var$neldiasvencimento;
          end if;

        end if;

      else

        -- da um distinct na cole??o de denomina??es
        var$oids_denominaciones := gepr_putilidades_###VERSION###.fdistinct_oids(par$aefco_oid_denom);

        if var$oids_denominaciones is not null and
           var$oids_denominaciones.count > 1 then

          -- retorna erro, pois pack modular so pode ter uma divisa.
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$codcultura        => par$cod_cultura,
                                                                      par$codchave          => 'MsgMasDeUnaDenominacion',
                                                                      par$codfuncionalidad  => gepr_pcomon_###VERSION###.const$codfuncdicgrabardoccont,
                                                                      par$codaplicacion     => gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                      par$colparametros     => null,
                                                                      par$bolmarcarinitexto => 1));

        else

          begin

            select den.nec_dias_vencimiento
              into var$neldiasvencimento
              from gepr_tdenominacion den
             where den.oid_denominacion = var$oids_denominaciones(1);

          exception
            when no_data_found then
              var$neldiasvencimento := null;
          end;

          par$selects := +sql%ROWCOUNT;

          if var$neldiasvencimento is not null then
            var$fyhvencimento := sysdate + var$neldiasvencimento;
          end if;

        end if;

      end if;

    end if;

    return var$fyhvencimento;
  end fcalcular_fec_vencimento_cont;

  function frecuperar_tipo_cont(par$cod_tipo_contenedor in gepr_pcomon_###VERSION###.tipo$cod_,
                                par$selects             out gepr_pcomon_###VERSION###.tipo$nel_)
    return gepr_pcomon_###VERSION###.tipo$tipocontenedor is
    var$objtipocontenedor      gepr_pcomon_###VERSION###.tipo$tipocontenedor;
    var$oid_tipo_contenedor    gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_contenedor    gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_tipo_contenedor    gepr_pcomon_###VERSION###.tipo$desc_;
    var$nec_cantidad           gepr_pcomon_###VERSION###.tipo$nel_;
    var$oid_unidad_medida      gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_unidad_medida      gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_tipo_unidad_medida gepr_pcomon_###VERSION###.tipo$cod_;
    var$num_valor_unidad       gepr_pcomon_###VERSION###.tipo$imp_;
    var$oid_calidad            gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_calidad            gepr_pcomon_###VERSION###.tipo$cod_;
    var$bol_pico               gepr_pcomon_###VERSION###.tipo$nbol_;
  begin

    par$selects := 0;

    select tp.oid_tipo_contenedor,
           tp.cod_tipo_contenedor,
           tp.des_tipo_contenedor,
           tp.nec_cantidad,
           um.oid_unidad_medida,
           um.cod_unidad_medida,
           um.cod_tipo_unidad_medida,
           um.num_valor_unidad,
           ca.oid_calidad,
           ca.cod_calidad,
           (case
             when tp.nec_cantidad is null then
              1
             else
              0
           end) bol_pico
      into var$oid_tipo_contenedor,
           var$cod_tipo_contenedor,
           var$des_tipo_contenedor,
           var$nec_cantidad,
           var$oid_unidad_medida,
           var$cod_unidad_medida,
           var$cod_tipo_unidad_medida,
           var$num_valor_unidad,
           var$oid_calidad,
           var$cod_calidad,
           var$bol_pico
      from sapr_ttipo_contenedor tp
      left join gepr_tunidad_medida um
        on um.oid_unidad_medida = tp.oid_unidad_medida
      left join gepr_tcalidad ca
        on ca.oid_calidad = tp.oid_calidad
     where tp.cod_tipo_contenedor = par$cod_tipo_contenedor;

    par$selects := par$selects + sql%ROWCOUNT;

    var$objtipocontenedor.oid_tipo_contenedor    := var$oid_tipo_contenedor;
    var$objtipocontenedor.cod_tipo_contenedor    := var$cod_tipo_contenedor;
    var$objtipocontenedor.des_tipo_contenedor    := var$des_tipo_contenedor;
    var$objtipocontenedor.nec_cantidad           := var$nec_cantidad;
    var$objtipocontenedor.oid_unidad_medida      := var$oid_unidad_medida;
    var$objtipocontenedor.cod_unidad_medida      := var$cod_unidad_medida;
    var$objtipocontenedor.cod_tipo_unidad_medida := var$cod_tipo_unidad_medida;
    var$objtipocontenedor.num_valor_unidad       := var$num_valor_unidad;
    var$objtipocontenedor.oid_calidad            := var$oid_calidad;
    var$objtipocontenedor.cod_calidad            := var$cod_calidad;
    var$objtipocontenedor.bol_pico               := var$bol_pico;

    return var$objtipocontenedor;
  end frecuperar_tipo_cont;

  PROCEDURE s_ins_tconsultarelemento(par$codIdioma       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$codTipoElemento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oidElemento     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$codPrecinto     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$rcElementos     OUT SYS_REFCURSOR,
                                     par$rcDocumentos    OUT SYS_REFCURSOR,
                                     par$rc_precintos    OUT SYS_REFCURSOR,
                                     par$rcCuentas       OUT SYS_REFCURSOR,
                                     par$rcAcciones      OUT SYS_REFCURSOR) IS

    var$oidDocumentos     VARCHAR2_ARRAY;
    var$oidCuentasOrigem  VARCHAR2_ARRAY;
    var$oidCuentasDestino VARCHAR2_ARRAY;
    var$oidsFormulario    VARCHAR2_ARRAY;
    var$oids              gepr_pcomon_###VERSION###.tipo$OIDS_;
    var$oidsCOrigem       gepr_pcomon_###VERSION###.tipo$OIDS_;
    var$oidsCDestino      gepr_pcomon_###VERSION###.tipo$OIDS_;
    var$oidsForm          gepr_pcomon_###VERSION###.tipo$OIDS_;

  BEGIN

    /* Recuperar los movimientos de los elementos:
    Recuperar los documentos (movimientos) de los elementos:
    Recuperar las cuentas de los documentos*/
    /*Buscar Elementos da delega??o solicitada que tenh OID ou precinto igual filtrado*/

    IF par$codTipoElemento = 'C' THEN
      OPEN par$rcElementos FOR
        SELECT 'C' AS CODTIPOELEMENTO,
               DOCELE.OID_DOCUMENTO,
               DIC.VALOR AS CODESTADOELEMENTO,
               CONT.OID_CONTENEDOR,
               TCONT.COD_TIPO_CONTENEDOR,
               TCONT.DES_TIPO_CONTENEDOR,
               CONT.BOL_PACK_MODULAR,
               fn_gmt_delegacion_###VERSION###(P.OID_DELEGACION, CONT.GMT_CREACION) AS FEC_ARMADO
          FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
         INNER JOIN SAPR_TCONTENEDOR CONT
            ON CONT.OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
		 INNER JOIN GEPR_TSECTOR S 
               ON S.OID_SECTOR = CONT.OID_SECTOR
         INNER JOIN GEPR_TPLANTA P 
               ON P.OID_PLANTA = S.OID_PLANTA
         INNER JOIN SAPR_TTIPO_CONTENEDOR TCONT
            ON TCONT.OID_TIPO_CONTENEDOR = CONT.OID_TIPO_CONTENEDOR
         LEFT JOIN SAPR_TPRECINTOXCONTENEDOR PREC
            ON PREC.OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
         LEFT JOIN GEPR_TDICCIONARIO DIC
            ON CONT.COD_ESTADO = DIC.COD_EXPRESION AND DIC.COD_FUNCIONALIDAD = 'Geral-EstadoContenedor' AND DIC.COD_IDIOMA = par$codIdioma
         WHERE PREC.COD_PRECINTO LIKE '%' || par$codPrecinto || '%';

      /*Recupera os OIDS do contenedor*/
      -- limpa a tabela tempor?ria
      EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TOIDS';

      -- grava resultado da consulta na tabela tempor?ria SAPR_GTT_TOIDS
      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT DISTINCT 'OID_CONSULTAR_ELEMENTO', CONT.OID_CONTENEDOR
          FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
         INNER JOIN SAPR_TCONTENEDOR CONT
            ON CONT.OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
         INNER JOIN SAPR_TTIPO_CONTENEDOR TCONT
            ON TCONT.OID_TIPO_CONTENEDOR = CONT.OID_TIPO_CONTENEDOR
         LEFT JOIN SAPR_TPRECINTOXCONTENEDOR PREC ON PREC.OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
         WHERE PREC.COD_PRECINTO LIKE '%' || par$codPrecinto || '%';

    ELSIF par$codTipoElemento = 'B' THEN
      OPEN par$rcElementos FOR
        SELECT DISTINCT 'B' AS CODTIPOELEMENTO,
               DOCELE.OID_DOCUMENTO,
               DIC.VALOR AS CODESTADOELEMENTO,
               REM.OID_REMESA,
               REM.COD_RUTA,
               REM.COD_EXTERNO,
               REM.FYH_TRANSPORTE,
               BUL.COD_PRECINTO,
               BUL.COD_BOLSA,
               BUL.OID_BULTO,
               REM.GMT_CREACION,
               LT.DES_TIPO AS TIPO_SERVICIO
          FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
         INNER JOIN SAPR_TBULTO BUL
            ON BUL.OID_BULTO = DOCELE.OID_BULTO
         INNER JOIN SAPR_TREMESA REM
            ON REM.OID_REMESA = BUL.OID_REMESA
         LEFT JOIN SAPR_TLISTA_VALORXELEMENTO VEL
            ON BUL.OID_BULTO = VEL.OID_BULTO
         LEFT JOIN GEPR_TLISTA_TIPO LT
            ON VEL.OID_LISTA_TIPO = LT.OID_LISTA_TIPO
         LEFT JOIN GEPR_TDICCIONARIO DIC
            ON BUL.COD_ESTADO = DIC.COD_EXPRESION AND DIC.COD_FUNCIONALIDAD = 'Geral-EstadoBulto' AND DIC.COD_IDIOMA = par$codIdioma
         WHERE BUL.COD_PRECINTO LIKE '%' || par$codPrecinto || '%';

      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT DISTINCT 'OID_CONSULTAR_ELEMENTO', DOCELE.OID_DOCUMENTO
          FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
         INNER JOIN SAPR_TBULTO BUL
            ON BUL.OID_BULTO = DOCELE.OID_BULTO
         INNER JOIN SAPR_TREMESA REM
            ON REM.OID_REMESA = BUL.OID_REMESA
         WHERE BUL.COD_PRECINTO LIKE '%' || par$codPrecinto || '%';

    ELSIF par$codTipoElemento = 'R' THEN
      OPEN par$rcElementos FOR
        SELECT DISTINCT'R' AS CODTIPOELEMENTO,
               DOCELE.OID_DOCUMENTO,
               DIC.VALOR AS CODESTADOELEMENTO,
               REM.OID_REMESA,
               REM.COD_RUTA,
               REM.COD_EXTERNO,
               REM.FYH_TRANSPORTE,
               BUL.COD_PRECINTO,
               BUL.COD_BOLSA,
               BUL.OID_BULTO,
               REM.GMT_CREACION
          FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
         INNER JOIN SAPR_TREMESA REM
            ON REM.OID_REMESA = DOCELE.OID_REMESA
         INNER JOIN SAPR_TBULTO BUL
            ON BUL.OID_REMESA = REM.OID_REMESA
         LEFT JOIN GEPR_TDICCIONARIO DIC
            ON REM.COD_ESTADO = DIC.COD_EXPRESION AND DIC.COD_FUNCIONALIDAD = 'Geral-EstadoRemesa' AND DIC.COD_IDIOMA = par$codIdioma
         WHERE REM.COD_EXTERNO = par$codPrecinto;

      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT DISTINCT 'OID_CONSULTAR_ELEMENTO', DOCELE.OID_DOCUMENTO
          FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
         INNER JOIN SAPR_TREMESA REM
            ON REM.OID_REMESA = DOCELE.OID_REMESA
         INNER JOIN SAPR_TBULTO BUL
            ON BUL.OID_REMESA = REM.OID_REMESA
         WHERE REM.COD_EXTERNO = par$codPrecinto;
    END IF;

    /*preenchendo os valores dos precintos*/
    OPEN par$rc_precintos FOR
      SELECT PREC.OID_CONTENEDOR, PREC.COD_PRECINTO
        FROM SAPR_GTT_TOIDS GTTO
       INNER JOIN SAPR_TPRECINTOXCONTENEDOR PREC
          ON PREC.OID_CONTENEDOR = GTTO.OID
         AND GTTO.COD_CALIFICADOR = 'OID_CONSULTAR_ELEMENTO'
       ORDER BY PREC.OID_CONTENEDOR, PREC.COD_PRECINTO;
    /*fim preenchimento precintos*/

    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)
      SELECT DISTINCT 'OID_CONSULTAR_ELEMENTO', DOCELE.OID_DOCUMENTO
        FROM SAPR_TDOCUMENTOXELEMENTO DOCELE
       INNER JOIN SAPR_TCONTENEDOR CONT
          ON CONT.OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
       LEFT JOIN SAPR_TPRECINTOXCONTENEDOR PREC
         ON PREC.OID_CONTENEDOR = DOCELE.OID_CONTENEDOR
       WHERE PREC.COD_PRECINTO LIKE '%' || par$codPrecinto || '%';

    /*Preenche variavel com todos oids de documento encontrado*/
    SELECT DOC.OID_DOCUMENTO BULK COLLECT
      INTO var$oids
      FROM SAPR_GTT_TOIDS GTTO
     INNER JOIN SAPR_TDOCUMENTOXELEMENTO DOC
        ON DOC.OID_DOCUMENTO = GTTO.OID
       AND GTTO.COD_CALIFICADOR = 'OID_CONSULTAR_ELEMENTO'
     ORDER BY DOC.OID_DOCUMENTO;

    if var$oids.count > 0 then
      var$oidDocumentos := gepr_putilidades_###VERSION###.fconverter_array_table(var$oids);
    end if;

    /*Buscar Documentos dos elementos encontrados*/

    IF par$codTipoElemento = 'C' THEN
      OPEN par$rcDocumentos FOR
        SELECT NVL(GDOC.OID_GRUPODOCUMENTOXCONTENEDOR, 0) AS ESAGRUPADOR,
               FORM.COD_FORMULARIO,
               FORM.DES_FORMULARIO,
               HDOC.COD_ESTADO,
               HDOC.GMT_MODIFICACION AS FECHAMODIFICACION,
               HDOC.DES_USUARIO_MODIFICACION,
               HDOC.OID_DOCUMENTO,
               DOC.OID_CUENTA_ORIGEN,
               DOC.OID_CUENTA_DESTINO
          FROM SAPR_THIST_MOV_DOCUMENTO HDOC
         INNER JOIN SAPR_TDOCUMENTO DOC
            ON DOC.OID_DOCUMENTO = HDOC.OID_DOCUMENTO
          LEFT JOIN SAPR_TGRUPODOCUMXCONTENEDOR GDOC
            ON GDOC.OID_GRUPO_DOCUMENTO = DOC.OID_GRUPO_DOCUMENTO
         INNER JOIN SAPR_TFORMULARIO FORM
            ON FORM.OID_FORMULARIO = DOC.OID_FORMULARIO
         WHERE HDOC.OID_DOCUMENTO IN
               (SELECT COLUMN_VALUE FROM TABLE(var$oidDocumentos));

    ELSE
      OPEN par$rcDocumentos FOR
        SELECT 0                             AS ESAGRUPADOR,
               FORM.COD_FORMULARIO,
               FORM.DES_FORMULARIO,
               HDOC.COD_ESTADO,
               HDOC.GMT_MODIFICACION         AS FECHAMODIFICACION,
               HDOC.DES_USUARIO_MODIFICACION,
               HDOC.OID_DOCUMENTO,
               DOC.OID_CUENTA_ORIGEN,
               DOC.OID_CUENTA_DESTINO
          FROM SAPR_THIST_MOV_DOCUMENTO HDOC
         INNER JOIN SAPR_TDOCUMENTO DOC
            ON DOC.OID_DOCUMENTO = HDOC.OID_DOCUMENTO
          LEFT JOIN SAPR_TGRUPO_DOCUMENTO GDOC
            ON GDOC.OID_GRUPO_DOCUMENTO = DOC.OID_GRUPO_DOCUMENTO
         INNER JOIN SAPR_TFORMULARIO FORM
            ON FORM.OID_FORMULARIO = DOC.OID_FORMULARIO
         WHERE HDOC.OID_DOCUMENTO IN
               (SELECT COLUMN_VALUE FROM TABLE(var$oidDocumentos));

    END IF;

    --insere registros dos oids formularios para buscar acciones (caracteristicas)
    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)
      SELECT DISTINCT 'OID_FORMULARIO_ELEMENTO', DOC.OID_FORMULARIO
        FROM SAPR_TDOCUMENTO DOC
       INNER JOIN SAPR_TFORMULARIO FORM
          ON FORM.OID_FORMULARIO = DOC.OID_FORMULARIO
       WHERE DOC.OID_DOCUMENTO IN
             (SELECT COLUMN_VALUE FROM TABLE(var$oidDocumentos));
    --insere registros dos oids de cuenta encontrado
    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)
      SELECT DISTINCT 'OID_CUENTA_ORIGEM_ELEMENTO', DOC.OID_CUENTA_ORIGEN
        FROM SAPR_TDOCUMENTO DOC
       WHERE DOC.OID_DOCUMENTO IN
             (SELECT COLUMN_VALUE FROM TABLE(var$oidDocumentos));
    --insere registros dos oids de cuenta destino encontrado
    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)
      SELECT DISTINCT 'OID_CUENTA_DESTINO_ELEMENTO', DOC.OID_CUENTA_DESTINO
        FROM SAPR_TDOCUMENTO DOC
       WHERE DOC.OID_DOCUMENTO IN
             (SELECT COLUMN_VALUE FROM TABLE(var$oidDocumentos));

    /*BUSCAR CARACTERISTICAS DOS FORMULARIOS*/
    SELECT DISTINCT DOC.OID_FORMULARIO BULK COLLECT
      INTO var$oidsForm
      FROM SAPR_GTT_TOIDS GTTO
     INNER JOIN SAPR_TDOCUMENTO DOC
        ON DOC.OID_FORMULARIO = GTTO.OID
       AND GTTO.COD_CALIFICADOR = 'OID_FORMULARIO_ELEMENTO'
     ORDER BY DOC.OID_FORMULARIO;

    if var$oidsForm.count > 0 then
      var$oidsFormulario := gepr_putilidades_###VERSION###.fconverter_array_table(var$oidsForm);
    end if;

    /*Fim busca caracteristica formulario*/

    /*Buscar Cuentas dos documentos*/
    /*Preenche variavel com todos oids de cuentas encontrado*/
    --CUENTA ORIGEM
    SELECT DISTINCT DOC.OID_CUENTA_ORIGEN BULK COLLECT
      INTO var$oidsCOrigem
      FROM SAPR_GTT_TOIDS GTTO
     INNER JOIN SAPR_TDOCUMENTO DOC
        ON DOC.OID_CUENTA_ORIGEN = GTTO.OID
       AND GTTO.COD_CALIFICADOR = 'OID_CUENTA_ORIGEM_ELEMENTO'
     ORDER BY DOC.OID_CUENTA_ORIGEN;

    if var$oidsCOrigem.count > 0 then
      var$oidCuentasOrigem := gepr_putilidades_###VERSION###.fconverter_array_table(var$oidsCOrigem);
    end if;

    --CUENTA DESTINO
    SELECT DISTINCT DOC.OID_CUENTA_DESTINO BULK COLLECT
      INTO var$oidsCDestino
      FROM SAPR_GTT_TOIDS GTTO
     INNER JOIN SAPR_TDOCUMENTO DOC
        ON DOC.OID_CUENTA_DESTINO = GTTO.OID
       AND GTTO.COD_CALIFICADOR = 'OID_CUENTA_DESTINO_ELEMENTO'
     ORDER BY DOC.OID_CUENTA_DESTINO;

    if var$oidsCDestino.count > 0 then
      var$oidCuentasDestino := gepr_putilidades_###VERSION###.fconverter_array_table(var$oidsCDestino);
    end if;

OPEN par$rcCuentas FOR
      SELECT OID_CUENTA,
             COD_CLIENTE,
             COD_SECTOR,
             COD_CANAL,
             COD_SUBCLIENTE,
             COD_PTO_SERVICIO,
             COD_SUBCANAL,
             COD_PLANTA,
             COD_DELEGACION,
       DES_DELEGACION,
       DES_PLANTA,
       DES_SECTOR,
       DES_CLIENTE,
       DES_SUBCLIENTE,
       DES_PTO_SERVICIO,
       DES_CANAL,
       DES_SUBCANAL,
             'ORIGEM' AS TIPOCUENTA
        FROM SAPR_VCUENTA
       WHERE OID_CUENTA IN
             (SELECT COLUMN_VALUE FROM TABLE(var$oidCuentasOrigem))
      UNION
      SELECT OID_CUENTA,
             COD_CLIENTE,
             COD_SECTOR,
             COD_CANAL,
             COD_SUBCLIENTE,
             COD_PTO_SERVICIO,
             COD_SUBCANAL,
             COD_PLANTA,
             COD_DELEGACION,
       DES_DELEGACION,
       DES_PLANTA,
       DES_SECTOR,
       DES_CLIENTE,
       DES_SUBCLIENTE,
       DES_PTO_SERVICIO,
       DES_CANAL,
       DES_SUBCANAL,
       'DESTINO' AS TIPOCUENTA
        FROM SAPR_VCUENTA
       WHERE OID_CUENTA IN
             (SELECT COLUMN_VALUE FROM TABLE(var$oidCuentasDestino));

    OPEN par$rcAcciones FOR
      SELECT CF.COD_CARACT_FORMULARIO, DOC.OID_DOCUMENTO
        FROM SAPR_TCARACT_FORMULARIO CF
       INNER JOIN SAPR_TCARACTFORMXFORMULARIO CFF
          ON CFF.OID_CARACT_FORMULARIO = CF.OID_CARACT_FORMULARIO
       INNER JOIN SAPR_TFORMULARIO F
          ON F.OID_FORMULARIO = CFF.OID_FORMULARIO
       INNER JOIN SAPR_TDOCUMENTO DOC
          ON DOC.OID_FORMULARIO = F.OID_FORMULARIO
       WHERE CFF.OID_FORMULARIO IN
             (SELECT COLUMN_VALUE FROM TABLE(var$oidsFormulario))
         AND DOC.OID_DOCUMENTO IN
             (SELECT COLUMN_VALUE FROM TABLE(var$oidDocumentos));

  END s_ins_tconsultarelemento;

  procedure sins_tcontenedor(par$oid_documento         in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_contenedor        in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_tipo_contenedor   in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_contenedor_padre  in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_sector            in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_cuenta_movimiento in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_cuenta_saldo      in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_grupo_documento   in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$bol_pack_modular      in gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$fec_vencimento        in gepr_pcomon_###VERSION###.tipo$fyh_,
                             par$bol_agrupa_cont       in gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$cod_puesto            in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_usuario           in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$oid_cont_mayor_nivel  in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$cod_contenedor        in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$inserts               OUT gepr_pcomon_###VERSION###.tipo$nel_) as

    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;

  begin

    par$inserts := 0;

    insert into sapr_tcontenedor
      (oid_contenedor,
       oid_tipo_contenedor,
       oid_contenedor_padre,
       oid_sector,
       oid_cuenta_movimiento,
       oid_cuenta_saldo,
       oid_grupo_documento,
       oid_documento,
       cod_estado,
       bol_pack_modular,
       fec_vencimiento,
       bol_agrupa_contenedor,
       cod_puesto,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion,
       cod_tipo_elemento,
       oid_contenedor_mayor_nivel,
       cod_contenedor)
    values
      (par$oid_contenedor,
       par$oid_tipo_contenedor,
       par$oid_contenedor_padre,
       par$oid_sector,
       par$oid_cuenta_movimiento,
       par$oid_cuenta_saldo,
       par$oid_grupo_documento,
       par$oid_documento,
       gepr_pcomon_###VERSION###.const$CodEstadoContEnTransito,
       par$bol_pack_modular,
       par$fec_vencimento,
       par$bol_agrupa_cont,
       par$cod_puesto,
       var$gmt_zero,
       par$cod_usuario,
       var$gmt_zero,
       par$cod_usuario,
       gepr_pcomon_###VERSION###.const$codTipoEleContenedor,
       par$oid_cont_mayor_nivel,
       par$cod_contenedor);

    par$inserts := sql%ROWCOUNT;

  end sins_tcontenedor;

 procedure sins_tefectivoxcontenedor(par$oid_contenedor    gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_divisa        gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_denominacion  gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_unidad_medida gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_calidad       gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_nivel_detalle gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$bol_disponible    gepr_pcomon_###VERSION###.tipo$nbol_,
                                      par$bol_bloqueado     gepr_pcomon_###VERSION###.tipo$nbol_,
                                      par$cod_tipo_contado  gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$num_importe       gepr_pcomon_###VERSION###.tipo$imp_,
                                      par$nel_cantidad      gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$cod_usuario       gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts           OUT gepr_pcomon_###VERSION###.tipo$nel_) as
  begin

    insert into sapr_tefectivoxcontenedor
      (oid_efectivoxcontenedor,
       oid_contenedor,
       oid_divisa,
       oid_denominacion,
       oid_unidad_medida,
       oid_calidad,
       cod_nivel_detalle,
       bol_disponible,
       bol_bloqueado,
       cod_tipo_contado,
       num_importe,
       nel_cantidad,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    values
      (sys_guid(),
       par$oid_contenedor,
       par$oid_divisa,
       par$oid_denominacion,
       par$oid_unidad_medida,
       par$oid_calidad,
       par$cod_nivel_detalle,
       par$bol_disponible,
       par$bol_bloqueado,
       par$cod_tipo_contado,
       par$num_importe,
       par$nel_cantidad,
       sysdate,
       par$cod_usuario,
       sysdate,
       par$cod_usuario);

    par$inserts := sql%rowcount;

  end sins_tefectivoxcontenedor;

 procedure sins_tmedio_pagoxcontenedor(par$oid_contenedor      gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_divisa          gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_medio_pago      gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_unidad_medida   gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_nivel_detalle   gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$bol_disponible      gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$bol_bloqueado       gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$num_importe         gepr_pcomon_###VERSION###.tipo$imp_,
                                        par$nel_cantidad        gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_usuario         gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_tipo_medio_pago gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) as

  begin

    insert into sapr_tmedio_pagoxcontenedor
      (oid_medio_pagoxcontenedor,
       oid_contenedor,
       oid_cuenta_movimiento,
       oid_cuenta_saldo,
       oid_divisa,
       cod_tipo_medio_pago,
       oid_medio_pago,
       oid_unidad_medida,
       cod_nivel_detalle,
       bol_disponible,
       bol_bloqueado,
       num_importe,
       nel_cantidad,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    values
      (sys_guid(),
       par$oid_contenedor,
       (select c.oid_cuenta_movimiento
          from sapr_tcontenedor c
         where c.oid_contenedor = par$oid_contenedor),
       (select c.oid_cuenta_saldo
          from sapr_tcontenedor c
         where c.oid_contenedor = par$oid_contenedor),
       par$oid_divisa,
       par$cod_tipo_medio_pago,
       par$oid_medio_pago,
       par$oid_unidad_medida,
       par$cod_nivel_detalle,
       par$bol_disponible,
       par$bol_bloqueado,
       par$num_importe,
       par$nel_cantidad,
       sysdate,
       par$cod_usuario,
       sysdate,
       par$cod_usuario);

    par$inserts := sql%rowcount;

  end sins_tmedio_pagoxcontenedor;

   procedure svincular_elemento_contenedor(par$aelco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                          par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                          par$oid_delegacion       in gepr_pcomon_###VERSION###.tipo$oid_,
                                          par$selects              out gepr_pcomon_###VERSION###.tipo$nel_,
                                          par$updates              out gepr_pcomon_###VERSION###.tipo$nel_) as
    var$oid_elemento gepr_pcomon_###VERSION###.tipo$oid_;
  begin

    par$selects := 0;
    par$updates := 0;

    if par$aelco_oid_contenedor is not null and
       par$aelco_oid_contenedor.count > 0 then

      for idx in par$aelco_oid_contenedor.first .. par$aelco_oid_contenedor.last loop

        if par$aelco_tipo_elemento(idx) =
           gepr_pcomon_###VERSION###.const$codTipoEleContenedor then

          if par$aelco_ids_elemento(idx) is not null then

            begin
              select c.oid_contenedor
                into var$oid_elemento
                from sapr_tcontenedor c
               inner join sapr_vcuenta cu
                  on cu.OID_CUENTA = c.oid_cuenta_movimiento
                 and cu.OID_DELEGACION = par$oid_delegacion
               where c.oid_contenedor = par$aelco_ids_elemento(idx)
                 and c.cod_estado = gepr_pcomon_###VERSION###.const$CodEstadoConArmado
                 and c.oid_contenedor_padre is null;

              par$selects := sql%rowcount;

            exception
              when no_data_found then
                var$oid_elemento := null;
            end;

          else

            begin
              select c.oid_contenedor
                into var$oid_elemento
                from sapr_tcontenedor c
               inner join sapr_tprecintoxcontenedor pc
                  on pc.oid_contenedor = c.oid_contenedor
               inner join sapr_vcuenta cu
                  on cu.OID_CUENTA = c.oid_cuenta_movimiento
                 and cu.OID_DELEGACION = par$oid_delegacion
               where pc.cod_precinto = par$aelco_precintos_ele(idx)
                 and c.cod_estado = gepr_pcomon_###VERSION###.const$CodEstadoConArmado
                 and c.oid_contenedor_padre is null;

              par$selects := sql%rowcount;

            exception
              when no_data_found then
                var$oid_elemento := null;
            end;

          end if;

          if var$oid_elemento is not null then

            update sapr_tcontenedor c
               set c.oid_contenedor_padre       = par$aelco_oid_contenedor(idx),
                   c.oid_contenedor_mayor_nivel = par$aelco_oid_contenedor(idx)
             where c.oid_contenedor = var$oid_elemento;

            par$updates := sql%rowcount;

          else

            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    'tipo contenedor parcial');

          end if;

        elsif par$aelco_tipo_elemento(idx) =
              gepr_pcomon_###VERSION###.const$codTipoEleRemesa then

          if par$aelco_ids_elemento(idx) is not null then

            begin
              select r.oid_remesa
                into var$oid_elemento
                from sapr_tremesa r
               inner join sapr_vcuenta cu
                  on cu.OID_CUENTA = r.oid_cuenta
                 and cu.OID_DELEGACION = par$oid_delegacion
               where r.oid_remesa = par$aelco_ids_elemento(idx)
                 and r.cod_estado =
                     gepr_pcomon_###VERSION###.const$codEstadoRemesaPendiente
                 and r.oid_contenedor_padre is null
                 and r.bol_gestion_bulto = 0;

              par$selects := sql%rowcount;

            exception
              when no_data_found then
                var$oid_elemento := null;
            end;

          else

            begin
              select r.oid_remesa
                into var$oid_elemento
                from sapr_tremesa r
               inner join sapr_vcuenta cu
                  on cu.OID_CUENTA = r.oid_cuenta
                 and cu.OID_DELEGACION = par$oid_delegacion
               where r.cod_externo = par$aelco_precintos_ele(idx)
                 and r.cod_estado =
                     gepr_pcomon_###VERSION###.const$codEstadoRemesaPendiente
                 and r.oid_contenedor_padre is null
                 and r.bol_gestion_bulto = 0;

              par$selects := sql%rowcount;

            exception
              when no_data_found then
                var$oid_elemento := null;
            end;

          end if;

          if var$oid_elemento is not null then

            update sapr_tremesa r
               set r.oid_contenedor_padre       = par$aelco_oid_contenedor(idx),
                   r.oid_contenedor_mayor_nivel = par$aelco_oid_contenedor(idx)
             where r.oid_remesa = var$oid_elemento;

            par$updates := sql%rowcount;

          else

            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    'tipo contenedor parcial');

          end if;

        elsif par$aelco_tipo_elemento(idx) =
              gepr_pcomon_###VERSION###.const$codTipoEleBulto then

          if par$aelco_ids_elemento(idx) is not null then

            begin
              select b.oid_bulto
                into var$oid_elemento
                from sapr_tbulto b
               inner join sapr_vcuenta cu
                  on cu.OID_CUENTA = b.oid_cuenta
                 and cu.OID_DELEGACION = par$oid_delegacion
               where b.oid_bulto = par$aelco_ids_elemento(idx)
                 and b.cod_estado = gepr_pcomon_###VERSION###.const$codEstadoBultoCerrado
                 and b.oid_contenedor_padre is null
                 and b.bol_gestion_bulto = 1;

              par$selects := sql%rowcount;

            exception
              when no_data_found then
                var$oid_elemento := null;
            end;

          else

            begin
              select b.oid_bulto
                into var$oid_elemento
                from sapr_tbulto b
               inner join sapr_vcuenta cu
                  on cu.OID_CUENTA = b.oid_cuenta
                 and cu.OID_DELEGACION = par$oid_delegacion
               where b.cod_precinto = par$aelco_precintos_ele(idx)
                 and b.cod_estado = gepr_pcomon_###VERSION###.const$codEstadoBultoCerrado
                 and b.oid_contenedor_padre is null
                 and b.bol_gestion_bulto = 1;

              par$selects := sql%rowcount;

            exception
              when no_data_found then
                var$oid_elemento := null;
            end;

          end if;

          if var$oid_elemento is not null then

            update sapr_tbulto b
               set b.oid_contenedor_padre       = par$aelco_oid_contenedor(idx),
                   b.oid_contenedor_mayor_nivel = par$aelco_oid_contenedor(idx)
             where b.oid_bulto = var$oid_elemento;

            par$updates := sql%rowcount;

          else

            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    'tipo contenedor parcial');

          end if;

        elsif par$aelco_tipo_elemento(idx) =
              gepr_pcomon_###VERSION###.const$codTipoEleParcial then

          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  'tipo contenedor parcial');

        end if;

      end loop;
    end if;

  end svincular_elemento_contenedor;


  procedure sgrabar_contenedor_ind(par$obj_formularios in gepr_pcomon_###VERSION###.tipo$formularios,
                                   par$oid_delegacion  in gepr_pcomon_###VERSION###.tipo$oid_,
                                   --documentos
                                   par$adoc_oid_documento in gepr_pcomon_###VERSION###.tipo$oids_,
                                   -- contenedores
                                   par$adoccont_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoccont_obj_tipo_cont     in gepr_pcomon_###VERSION###.tipo$tiposContenedores,
                                   par$adoccont_cod_tipo_servicio IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_tipo_embalaje IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_puesto        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_contenedor    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   -- Precintos
                                   par$aprco_oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aprco_bol_precinto_autom   in gepr_pcomon_###VERSION###.tipo$nbols_,
                                   -- Elementos
                                   par$aelco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   /* arrays de efectivo do contenedor */
                                   par$aefco_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_denom         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_uni_med       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefco_cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefco_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aefco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   /* arrays de medio de pago do contenedor */
                                   par$ampco_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_oid_mp            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$ampco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$ampco_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* arrays de terminos de medio de pago do contenedor */
                                   par$avtmpco_oid_contenedor   in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_oid_mp           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_oid_termediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtmpco_nec_indicegrupo  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                   /* informacion de ejecucion */
                                   par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_) as

    var$bol_agrupa_contenedor    gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$bol_pack_modular         gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$fyh_vencimento           gepr_pcomon_###VERSION###.tipo$fyh_;
    var$oid_contenedor           gepr_pcomon_###VERSION###.tipo$oid_;
    var$aefco_oid_divisa         gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefco_oid_denom          gepr_pcomon_###VERSION###.tipo$oids_;
    var$oid_cuenta_origen        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_saldo_origen  gepr_pcomon_###VERSION###.tipo$oid_;
    var$ampcont_oid_mp           gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefcont_oid_denominacion gepr_pcomon_###VERSION###.tipo$oids_;
    var$inserts                  gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates                  gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects                  gepr_pcomon_###VERSION###.tipo$nel_;
  begin

    par$inserts := 0;
    par$updates := 0;
    par$selects := 0;
    var$inserts := 0;
    var$updates := 0;
    var$selects := 0;

    for idx in par$adoccont_oid_contenedor.first .. par$adoccont_oid_contenedor.last loop

      var$oid_contenedor := par$adoccont_oid_contenedor(idx);

      if par$aelco_tipo_elemento is not null and
         par$aelco_tipo_elemento.count > 0 then
        var$bol_agrupa_contenedor := 1;
      end if;

      /* Verifica se ? pack modular */
      if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                                               par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracPackModular) > 0 then
        var$bol_pack_modular := 1;
      end if;

      /* Valida os tipos de valores */
      svalidar_tipo_valor(par$oid_contenedor       => par$adoccont_oid_contenedor(idx),
                          par$aefco_oid_contenedor => par$aefco_oid_contenedor,
                          par$aefco_oid_denom      => par$aefco_oid_denom,
                          par$ampco_oid_contenedor => par$ampco_oid_contenedor,
                          par$ampco_oid_mp         => par$ampco_oid_mp,
                          par$obj_tipo_cont        => par$adoccont_obj_tipo_cont(idx),
                          par$cod_cultura          => par$cod_cultura,
                          par$selects              => var$selects);

      par$selects := par$selects + var$selects;

      /* Validação da calidad */
      svalidar_calidad(par$oid_contenedor       => par$adoccont_oid_contenedor(idx),
                       par$aefco_oid_contenedor => par$aefco_oid_contenedor,
                       par$aefco_oid_calidad    => par$aefco_oid_calidad,
                       par$obj_tipo_cont        => par$adoccont_obj_tipo_cont(idx),
                       par$cod_cultura          => par$cod_cultura,
                       par$selects              => var$selects);

      par$selects := par$selects + var$selects;

      /* validação de cantidad*/
      svalidar_cantidad(par$oid_contenedor       => par$adoccont_oid_contenedor(idx),
                        par$aefco_oid_contenedor => par$aefco_oid_contenedor,
                        par$aefco_nel_cantidad   => par$aefco_nel_cantidad,
                        par$ampco_oid_contenedor => par$ampco_oid_contenedor,
                        par$ampco_nel_cantidad   => par$ampco_nel_cantidad,
                        par$obj_tipo_cont        => par$adoccont_obj_tipo_cont(idx),
                        par$cod_cultura          => par$cod_cultura);

      var$aefco_oid_divisa := gepr_putilidades_###VERSION###.ffiltrar_array_por_oid_padre(par$array_oids_padres => par$aefco_oid_contenedor,
                                                                                     par$array_itens       => par$aefco_oid_divisa,
                                                                                     par$oid_corrente      => par$adoccont_oid_contenedor(idx));

      var$aefco_oid_denom := gepr_putilidades_###VERSION###.ffiltrar_array_por_oid_padre(par$array_oids_padres => par$aefco_oid_contenedor,
                                                                                    par$array_itens       => par$aefco_oid_denom,
                                                                                    par$oid_corrente      => par$adoccont_oid_contenedor(idx));

      var$fyh_vencimento := fcalcular_fec_vencimento_cont(par$aefco_oid_divisa => var$aefco_oid_divisa,
                                                          par$aefco_oid_denom  => var$aefco_oid_denom,
                                                          par$bol_acepta_pico  => par$adoccont_obj_tipo_cont(idx)
                                                                                  .bol_pico,
                                                          par$cod_cultura      => par$cod_cultura,
                                                          par$selects          => var$selects);

      par$selects := par$selects + var$selects;

      if var$fyh_vencimento is null then

        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgFechaVencimento',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;

      end if;

      /* Grava o contenedor */
      sins_tcontenedor(par$oid_documento         => null,
                       par$oid_contenedor        => var$oid_contenedor,
                       par$oid_tipo_contenedor   => par$adoccont_obj_tipo_cont(idx)
                                                    .OID_TIPO_CONTENEDOR,
                       par$oid_contenedor_padre  => null,
                       par$oid_sector            => null,
                       par$oid_cuenta_movimiento => null,
                       par$oid_cuenta_saldo      => null,
                       par$oid_grupo_documento   => null,
                       par$bol_pack_modular      => var$bol_pack_modular,
                       par$fec_vencimento        => var$fyh_vencimento,
                       par$bol_agrupa_cont       => var$bol_agrupa_contenedor,
                       par$cod_puesto            => par$adoccont_cod_puesto(idx),
                       par$cod_usuario           => par$usuario,
                       par$oid_cont_mayor_nivel  => null,
                       par$cod_contenedor        => par$adoccont_cod_contenedor(idx),
                       par$inserts               => var$inserts);

      par$inserts := par$inserts + var$inserts;

      /* Grava na tabela documento elemento */
      sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$adoc_oid_documento(idx),
                                                        par$oid_remesa           => null,
                                                        par$oid_bulto            => null,
                                                        par$cod_usuario          => par$usuario,
                                                        par$oid_contenedor       => var$oid_contenedor,
                                                        par$oid_contenedor_padre => null,
                                                        par$oid_cont_mayor_nivel => null,
                                                        par$inserts              => par$inserts);

      par$inserts := par$inserts + var$inserts;

      /* Insere o tipo de servi?o */
      sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => var$oid_contenedor,
                                                        par$oid_remesa      => null,
                                                        par$oid_bulto       => null,
                                                        par$oid_parcial     => null,
                                                        par$oid_lista_valor => null,
                                                        par$cod_lista_valor => par$adoccont_cod_tipo_servicio(idx),
                                                        par$cod_lista_tipo  => gepr_pcomon_###VERSION###.const$codListaTipoTrabajo,
                                                        par$cod_usuario     => par$usuario,
                                                        par$cod_cultura     => par$cod_cultura,
                                                        par$inserts         => var$inserts);

      par$inserts := par$inserts + var$inserts;

      /* Insere o tipo de embalagem */

      sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => var$oid_contenedor,
                                                        par$oid_remesa      => null,
                                                        par$oid_bulto       => null,
                                                        par$oid_parcial     => null,
                                                        par$oid_lista_valor => null,
                                                        par$cod_lista_valor => par$adoccont_cod_tipo_embalaje(idx),
                                                        par$cod_lista_tipo  => gepr_pcomon_###VERSION###.const$codListaTipoFormato,
                                                        par$cod_usuario     => par$usuario,
                                                        par$cod_cultura     => par$cod_cultura,
                                                        par$inserts         => var$inserts);

      par$inserts := par$inserts + var$inserts;

    end loop;

    /* Insere os precintos do contenedor */
    sins_tprecintoxcontenedor(par$aprco_precintos_contenedor => par$aprco_precintos_contenedor,
                              par$aprco_oid_contenedor       => par$aprco_oid_contenedor,
                              par$aprco_bol_precinto_autom   => par$aprco_bol_precinto_autom,
                              par$cod_usuario                => par$usuario,
                              par$inserts                    => var$inserts);

    par$inserts := par$inserts + var$inserts;

    if par$aefco_oid_contenedor is not null and
       par$aefco_oid_contenedor.count > 0 then

      for idx in par$aefco_oid_contenedor.first .. par$aefco_oid_contenedor.last loop

        sins_tefectivoxcontenedor(par$oid_contenedor    => par$aefco_oid_contenedor(idx),
                                  par$oid_divisa        => par$aefco_oid_divisa(idx),
                                  par$oid_denominacion  => par$aefco_oid_denom(idx),
                                  par$oid_unidad_medida => par$aefco_oid_uni_med(idx),
                                  par$oid_calidad       => par$aefco_oid_calidad(idx),
                                  par$cod_nivel_detalle => par$aefco_cod_nivel_det(idx),
                                  par$bol_disponible    => 1,
                                  par$bol_bloqueado     => 1,
                                  par$cod_tipo_contado  => '0',
                                  par$num_importe       => par$aefco_num_importe(idx),
                                  par$nel_cantidad      => par$aefco_nel_cantidad(idx),
                                  par$cod_usuario       => par$usuario,
                                  par$inserts           => var$inserts);

        par$inserts := par$inserts + var$inserts;
      end loop;

    end if;

    if par$ampco_oid_divisa is not null and
       NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampco_oid_divisa) then

      for idx in par$ampco_oid_divisa.first .. par$ampco_oid_divisa.last loop

        sins_tmedio_pagoxcontenedor(par$oid_contenedor      => var$oid_contenedor,
                                    par$oid_divisa          => par$ampco_oid_divisa(idx),
                                    par$oid_medio_pago      => par$ampco_oid_mp(idx),
                                    par$oid_unidad_medida   => par$ampco_oid_unidad_medida(idx),
                                    par$cod_nivel_detalle   => par$ampco_cod_nivel_det(idx),
                                    par$bol_disponible      => 1,
                                    par$bol_bloqueado       => 1,
                                    par$num_importe         => par$ampco_num_importe(idx),
                                    par$nel_cantidad        => par$ampco_nel_cantidad(idx),
                                    par$cod_usuario         => par$usuario,
                                    par$cod_tipo_medio_pago => par$ampco_cod_tipo_mp(idx),
                                    par$inserts             => var$inserts);

        par$inserts := par$inserts + var$inserts;

      end loop;

    end if;

    /* Vincula o contenedor a seu elementos*/
    if par$aelco_oid_contenedor is not null and NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aelco_oid_contenedor) then

      svincular_elemento_contenedor(par$aelco_oid_contenedor => par$aelco_oid_contenedor,
                                    par$aelco_tipo_elemento  => par$aelco_tipo_elemento,
                                    par$aelco_precintos_ele  => par$aelco_precintos_ele,
                                    par$aelco_ids_elemento   => par$aelco_ids_elemento,
                                    par$oid_delegacion       => par$oid_delegacion,
                                    par$selects              => var$selects,
                                    par$updates              => var$updates);

      par$selects := par$selects + var$selects;
      par$updates := par$updates + var$updates;

    end if;

  end sgrabar_contenedor_ind;

procedure sbaja_contenedor_ind(par$obj_formularios in gepr_pcomon_###VERSION###.tipo$formularios,
                                 par$oid_delegacion  in gepr_pcomon_###VERSION###.tipo$oid_,
                                 --documentos
                                 par$adoc_oid_documento        in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adoc_oid_sector_origen    in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adoc_oid_cuenta_origen    in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adoc_oid_cuenta_saldo_ori in gepr_pcomon_###VERSION###.tipo$oids_,
                                 -- contenedores
                                 par$adoccont_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,
                                 -- Elementos
                                 par$aelco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                 par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                 par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                 /* informacion de ejecucion */
                                 par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_) as

    var$oid_contenedor gepr_pcomon_###VERSION###.tipo$oid_;
    var$updates        gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts        gepr_pcomon_###VERSION###.tipo$nel_;
  begin

    for idx in par$adoc_oid_documento.first .. par$adoc_oid_documento.last loop

      var$oid_contenedor := par$adoccont_oid_contenedor(idx);

      supd_tcontenedor(par$oid_contenedor   => par$adoccont_oid_contenedor(idx),
                       par$oid_documento    => par$adoc_oid_documento(idx),
                       par$oid_cuenta_mov   => null,
                       par$oid_cuenta_saldo => null,
                       par$oid_sector       => null,
                       par$cod_usuario      => par$usuario,
                       par$cod_estado       => gepr_pcomon_###VERSION###.const$CodEstadoContEnTransito,
                       par$updates          => var$updates);

      par$updates := par$updates + var$updates;

      /* Grava na tabela documento elemento */
      sapr_pdocumento_###VERSION###.sins_tdocumento_elemento(par$oid_documento        => par$adoc_oid_documento(idx),
                                                        par$oid_remesa           => null,
                                                        par$oid_bulto            => null,
                                                        par$cod_usuario          => par$usuario,
                                                        par$oid_contenedor       => var$oid_contenedor,
                                                        par$oid_contenedor_padre => null,
                                                        par$oid_cont_mayor_nivel => null,
                                                        par$inserts              => par$inserts);

      par$inserts := par$inserts + var$inserts;

    end loop;

    if par$aelco_oid_contenedor is not null and
       gepr_putilidades_###VERSION###.farray_vacio_oid(par$aelco_oid_contenedor) then

      /* Verifica se e uma baja de salidas recorrido */
      if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                                               par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracSalidaRecor) > 0 then

        for idx in par$aelco_oid_contenedor.first .. par$aelco_oid_contenedor.last loop

          if par$aelco_tipo_elemento(idx) =
             gepr_pcomon_###VERSION###.const$codTipoEleContenedor then

            supd_tcontenedor(par$oid_contenedor   => par$aelco_ids_elemento(idx),
                             par$oid_documento    => null,
                             par$oid_cuenta_mov   => null,
                             par$oid_cuenta_saldo => null,
                             par$oid_sector       => null,
                             par$cod_usuario      => par$usuario,
                             par$cod_estado       => gepr_pcomon_###VERSION###.const$CodEstadoContEnTransito,
                             par$updates          => var$updates);

            par$updates := par$updates + var$updates;

          elsif par$aelco_tipo_elemento(idx) =
                gepr_pcomon_###VERSION###.const$codTipoEleRemesa then

            sapr_premesa_###VERSION###.supd_tremesa(par$oid_remesa       => par$aelco_ids_elemento(idx),
                                               par$oid_documento    => null,
                                               par$oid_cuenta_mov   => null,
                                               par$oid_cuenta_saldo => null,
                                               par$cod_usuario      => par$usuario,
                                               par$cod_estado       => gepr_pcomon_###VERSION###.const$codEstadoRemesaTransito,
                                               par$updates          => var$updates);

            par$updates := par$updates + var$updates;

          elsif par$aelco_tipo_elemento(idx) =
                gepr_pcomon_###VERSION###.const$codTipoEleBulto then

            sapr_pbulto_###VERSION###.supd_tbulto(par$oid_bulto        => par$aelco_ids_elemento(idx),
                                             par$oid_documento    => null,
                                             par$oid_cuenta_mov   => null,
                                             par$oid_cuenta_saldo => null,
                                             par$cod_usuario      => par$usuario,
                                             par$cod_estado       => gepr_pcomon_###VERSION###.const$codEstadoBultoTransito,
                                             par$updates          => var$updates);

            par$updates := par$updates + var$updates;

          elsif par$aelco_tipo_elemento(idx) =
                gepr_pcomon_###VERSION###.const$codTipoEleParcial then

            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    'N?o ? possivel dessarmar um parcial');

          end if;

        end loop;

      end if;

    end if;
  end sbaja_contenedor_ind;


  procedure supd_tcontenedor(par$oid_contenedor   in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_documento    in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_cuenta_mov   in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_cuenta_saldo in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_sector       in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$cod_usuario      in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_estado       in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$updates          OUT gepr_pcomon_###VERSION###.tipo$nel_) as
  begin

    update sapr_tcontenedor c
       set c.cod_estado               = nvl(par$cod_estado, c.cod_estado),
           c.oid_documento            = nvl(par$oid_documento,
                                            c.oid_documento),
           c.oid_sector               = nvl(par$oid_sector, c.oid_sector),
           c.oid_cuenta_movimiento    = nvl(par$oid_cuenta_mov,
                                            c.oid_cuenta_movimiento),
           c.oid_cuenta_saldo         = nvl(par$oid_cuenta_saldo,
                                            c.oid_cuenta_saldo),
           c.gmt_modificacion         = sysdate,
           c.des_usuario_modificacion = par$cod_usuario
     where c.oid_contenedor = par$oid_contenedor;

    par$updates := sql%rowcount;

  end supd_tcontenedor;

  procedure sgrabar_contenedor_grp(par$obj_formularios     in gepr_pcomon_###VERSION###.tipo$formularios,
                                   par$oid_delegacion      in gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_grupo_documento in gepr_pcomon_###VERSION###.tipo$oid_,
                                   --documentos
                                   -- contenedores
                                   par$adoccont_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$adoccont_obj_tipo_cont     in gepr_pcomon_###VERSION###.tipo$tiposContenedores,
                                   par$adoccont_cod_tipo_servicio IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_tipo_embalaje IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_puesto        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_cod_contenedor    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$adoccont_rowver_contenedor IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   -- Precintos
                                   par$aprco_oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aprco_precintos_contenedor IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aprco_bol_precinto_autom   in gepr_pcomon_###VERSION###.tipo$nbols_,
                                   -- Elementos
                                   par$aelco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aelco_tipo_elemento  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_precintos_ele  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aelco_ids_elemento   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   /* arrays de efectivo do contenedor */
                                   par$aefco_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_denom         IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_oid_uni_med       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefco_cod_tipo_efec_tot IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$aefco_oid_calidad       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$aefco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$aefco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,

                                   /* arrays de medio de pago do contenedor */
                                   par$ampco_oid_contenedor    in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_oid_divisa        IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_oid_mp            IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$ampco_cod_tipo_mp       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampco_cod_nivel_det     IN gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$ampco_num_importe       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                   par$ampco_nel_cantidad      IN gepr_pcomon_###VERSION###.tipo$nels_,
                                   par$ampco_oid_unidad_medida in gepr_pcomon_###VERSION###.tipo$oids_,

                                   /* arrays de terminos de medio de pago do contenedor */
                                   par$avtmpco_oid_contenedor   in gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_oid_mp           IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_oid_termediopago IN gepr_pcomon_###VERSION###.tipo$oids_,
                                   par$avtmpco_des_valor        IN gepr_pcomon_###VERSION###.tipo$descs_,
                                   par$avtmpco_nec_indicegrupo  IN gepr_pcomon_###VERSION###.tipo$ints_,

                                   /* informacion de ejecucion */
                                   par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                   par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_) as

    var$bol_agrupa_contenedor   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$bol_pack_modular        gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$fyh_vencimento          gepr_pcomon_###VERSION###.tipo$fyh_;
    var$oid_contenedor          gepr_pcomon_###VERSION###.tipo$oid_;
    var$aefco_oid_divisa        gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefco_oid_denom         gepr_pcomon_###VERSION###.tipo$oids_;
    var$oid_cuenta_origen       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_saldo_origen gepr_pcomon_###VERSION###.tipo$oid_;
    var$selects                 gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts                 gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates                 gepr_pcomon_###VERSION###.tipo$nel_;

  begin

    par$inserts := 0;
    par$updates := 0;
    par$selects := 0;
    var$inserts := 0;
    var$updates := 0;
    var$selects := 0;

    for idx in par$adoccont_oid_contenedor.first .. par$adoccont_oid_contenedor.last loop

      var$oid_contenedor := par$adoccont_oid_contenedor(idx);

      if par$aelco_tipo_elemento is not null and
         par$aelco_tipo_elemento.count > 0 then
        var$bol_agrupa_contenedor := 1;
      end if;

      /* Verifica se ? pack modular */
      if sapr_pformulario_###VERSION###.fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                                               par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracPackModular) > 0 then
        var$bol_pack_modular := 1;
      end if;

      /* Valida os tipos de valores */
      svalidar_tipo_valor(par$oid_contenedor       => par$adoccont_oid_contenedor(idx),
                          par$aefco_oid_contenedor => par$aefco_oid_contenedor,
                          par$aefco_oid_denom      => par$aefco_oid_denom,
                          par$ampco_oid_contenedor => par$ampco_oid_contenedor,
                          par$ampco_oid_mp         => par$ampco_oid_mp,
                          par$obj_tipo_cont        => par$adoccont_obj_tipo_cont(idx),
                          par$cod_cultura          => par$cod_cultura,
                          par$selects              => var$selects);

      par$selects := par$selects + var$selects;

      /* Validação da calidad */
      svalidar_calidad(par$oid_contenedor       => par$adoccont_oid_contenedor(idx),
                       par$aefco_oid_contenedor => par$aefco_oid_contenedor,
                       par$aefco_oid_calidad    => par$aefco_oid_calidad,
                       par$obj_tipo_cont        => par$adoccont_obj_tipo_cont(idx),
                       par$cod_cultura          => par$cod_cultura,
                       par$selects              => var$selects);

      par$selects := par$selects + var$selects;

      /* validação de cantidad*/
      svalidar_cantidad(par$oid_contenedor       => par$adoccont_oid_contenedor(idx),
                        par$aefco_oid_contenedor => par$aefco_oid_contenedor,
                        par$aefco_nel_cantidad   => par$aefco_nel_cantidad,
                        par$ampco_oid_contenedor => par$ampco_oid_contenedor,
                        par$ampco_nel_cantidad   => par$ampco_nel_cantidad,
                        par$obj_tipo_cont        => par$adoccont_obj_tipo_cont(idx),
                        par$cod_cultura          => par$cod_cultura);

      var$aefco_oid_divisa := gepr_putilidades_###VERSION###.ffiltrar_array_por_oid_padre(par$array_oids_padres => par$aefco_oid_contenedor,
                                                                                     par$array_itens       => par$aefco_oid_divisa,
                                                                                     par$oid_corrente      => par$adoccont_oid_contenedor(idx));

      var$aefco_oid_denom := gepr_putilidades_###VERSION###.ffiltrar_array_por_oid_padre(par$array_oids_padres => par$aefco_oid_contenedor,
                                                                                    par$array_itens       => par$aefco_oid_denom,
                                                                                    par$oid_corrente      => par$adoccont_oid_contenedor(idx));

      var$fyh_vencimento := fcalcular_fec_vencimento_cont(par$aefco_oid_divisa => var$aefco_oid_divisa,
                                                          par$aefco_oid_denom  => var$aefco_oid_denom,
                                                          par$bol_acepta_pico  => par$adoccont_obj_tipo_cont(idx)
                                                                                  .bol_pico,
                                                          par$cod_cultura      => par$cod_cultura,
                                                          par$selects          => par$selects);

      /* Grava o contenedor */
      sins_tcontenedor(par$oid_documento         => null,
                       par$oid_contenedor        => var$oid_contenedor,
                       par$oid_tipo_contenedor   => par$adoccont_obj_tipo_cont(idx)
                                                    .OID_TIPO_CONTENEDOR,
                       par$oid_contenedor_padre  => null,
                       par$oid_sector            => null,
                       par$oid_cuenta_movimiento => null,
                       par$oid_cuenta_saldo      => null,
                       par$oid_grupo_documento   => null,
                       par$bol_pack_modular      => var$bol_pack_modular,
                       par$fec_vencimento        => var$fyh_vencimento,
                       par$bol_agrupa_cont       => var$bol_agrupa_contenedor,
                       par$cod_puesto            => par$adoccont_cod_puesto(idx),
                       par$cod_usuario           => par$usuario,
                       par$oid_cont_mayor_nivel  => null,
                       par$cod_contenedor        => par$adoccont_cod_contenedor(idx),
                       par$inserts               => var$inserts);

      par$inserts := par$inserts + var$inserts;

      /* Grava na tabela grupo documento contenedor */
      sapr_pdocumentos_grp_###VERSION###.sins_tgrupo_documento_cont(par$oid_grupo_documento  => par$oid_grupo_documento,
                                                               par$cod_usuario          => par$usuario,
                                                               par$oid_contenedor       => var$oid_contenedor,
                                                               par$oid_contenedor_padre => var$oid_contenedor,
                                                               par$oid_remesa           => null,
                                                               par$oid_bulto            => null,
                                                               par$inserts              => par$inserts);

      par$inserts := par$inserts + var$inserts;

      /* Insere o tipo de servi?o */
      sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => var$oid_contenedor,
                                                        par$oid_remesa      => null,
                                                        par$oid_bulto       => null,
                                                        par$oid_parcial     => null,
                                                        par$oid_lista_valor => null,
                                                        par$cod_lista_valor => par$adoccont_cod_tipo_servicio(idx),
                                                        par$cod_lista_tipo  => gepr_pcomon_###VERSION###.const$codListaTipoTrabajo,
                                                        par$cod_usuario     => par$usuario,
                                                        par$cod_cultura     => par$cod_cultura,
                                                        par$inserts         => var$inserts);

      par$inserts := par$inserts + var$inserts;

      /* Insere o tipo de embalagem */
      sapr_plista_valor_###VERSION###.sins_tlista_valor_elem(par$oid_contenedor  => var$oid_contenedor,
                                                        par$oid_remesa      => null,
                                                        par$oid_bulto       => null,
                                                        par$oid_parcial     => null,
                                                        par$oid_lista_valor => null,
                                                        par$cod_lista_valor => par$adoccont_cod_tipo_embalaje(idx),
                                                        par$cod_lista_tipo  => gepr_pcomon_###VERSION###.const$codListaTipoFormato,
                                                        par$cod_usuario     => par$usuario,
                                                        par$cod_cultura     => par$cod_cultura,
                                                        par$inserts         => var$inserts);

      par$inserts := par$inserts + var$inserts;

    end loop;

    /* Insere os precintos do contenedor */
    sins_tprecintoxcontenedor(par$aprco_precintos_contenedor => par$aprco_precintos_contenedor,
                              par$aprco_oid_contenedor       => par$aprco_oid_contenedor,
                              par$aprco_bol_precinto_autom => par$aprco_bol_precinto_autom,
                              par$cod_usuario                => par$usuario,
                              par$inserts                    => var$inserts);

    par$inserts := par$inserts + var$inserts;

    if par$aefco_oid_contenedor is not null and
       par$aefco_oid_contenedor.count > 0 then

      for idx in par$aefco_oid_contenedor.first .. par$aefco_oid_contenedor.last loop

        sins_tefectivoxcontenedor(par$oid_contenedor    => par$aefco_oid_contenedor(idx),
                                  par$oid_divisa        => par$aefco_oid_divisa(idx),
                                  par$oid_denominacion  => par$aefco_oid_denom(idx),
                                  par$oid_unidad_medida => par$aefco_oid_uni_med(idx),
                                  par$oid_calidad       => par$aefco_oid_calidad(idx),
                                  par$cod_nivel_detalle => par$aefco_cod_nivel_det(idx),
                                  par$bol_disponible    => 1,
                                  par$bol_bloqueado     => 1,
                                  par$cod_tipo_contado  => '0',
                                  par$num_importe       => par$aefco_num_importe(idx),
                                  par$nel_cantidad      => par$aefco_nel_cantidad(idx),
                                  par$cod_usuario       => par$usuario,
                                  par$inserts           => var$inserts);

        par$inserts := par$inserts + var$inserts;
      end loop;

    end if;

    if par$ampco_oid_divisa is not null and
       NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampco_oid_divisa) then

      for idx in par$ampco_oid_divisa.first .. par$ampco_oid_divisa.last loop

        sins_tmedio_pagoxcontenedor(par$oid_contenedor      => par$ampco_oid_contenedor(idx),
                                    par$oid_divisa          => par$ampco_oid_divisa(idx),
                                    par$oid_medio_pago      => par$ampco_oid_mp(idx),
                                    par$oid_unidad_medida   => par$ampco_oid_unidad_medida(idx),
                                    par$cod_nivel_detalle   => par$ampco_cod_nivel_det(idx),
                                    par$bol_disponible      => 1,
                                    par$bol_bloqueado       => 1,
                                    par$num_importe         => par$ampco_num_importe(idx),
                                    par$nel_cantidad        => par$ampco_nel_cantidad(idx),
                                    par$cod_usuario         => par$usuario,
                                    par$cod_tipo_medio_pago => par$ampco_cod_tipo_mp(idx),
                                    par$inserts             => var$inserts);

        par$inserts := par$inserts + var$inserts;
      end loop;

    end if;

    /* Vincula o contenedor a seu elementos*/
    if par$aelco_oid_contenedor is not null and NOT
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aelco_oid_contenedor) then

      svincular_elemento_contenedor(par$aelco_oid_contenedor => par$aelco_oid_contenedor,
                                    par$aelco_tipo_elemento  => par$aelco_tipo_elemento,
                                    par$aelco_precintos_ele  => par$aelco_precintos_ele,
                                    par$aelco_ids_elemento   => par$aelco_ids_elemento,
                                    par$oid_delegacion       => par$oid_delegacion,
                                    par$selects              => var$selects,
                                    par$updates              => var$updates);

      par$selects := par$selects + var$selects;
      par$updates := par$updates + var$updates;
    end if;

  end sgrabar_contenedor_grp;



  PROCEDURE sreenvio_entre_sectores(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                    par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_
                                    /* origem */,
                                    par$oid_delegacion_ori IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_planta_ori     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_ori     IN gepr_pcomon_###VERSION###.tipo$oid_
                                    /* destino */,
                                    par$oid_delegacion_des IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_planta_des     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_des     IN gepr_pcomon_###VERSION###.tipo$oid_
                                    /* arrays asociativos contenedor */,
                                    par$acosoid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$acoscod_precinto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$acosrowver         IN gepr_pcomon_###VERSION###.tipo$nels_,
                                    par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_documento      OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$info_ejecucion     IN VARCHAR2,
                                    par$hacer_commit       in gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$contenedores_cursor    gepr_pcomon_###VERSION###.tipo$contenedores;
    var$efec_mp_cont_cursor    gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores;
    var$precintos_contenedores gepr_pcomon_###VERSION###.tipo$precintos_contenedores;
    var$lst_val_cnt            gepr_pcomon_###VERSION###.tipo$lst_val_contenedores;
    var$es_doc_agrupador       gepr_pcomon_###VERSION###.tipo$nbol_;
    var$cod_formulario         gepr_pcomon_###VERSION###.tipo$cod_;

    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err         VARCHAR2(4000);
    var$inicio      TIMESTAMP;

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sreenvio_entre_sectores';

  BEGIN

    var$inicio := SYSTIMESTAMP;
    /*                svalidar_datos_contenedores(par$cod_formulario => par$cod_formulario
                                                ,par$oid_delegacion_ori => par$oid_delegacion_ori
                                                ,par$oid_planta_ori => par$oid_planta_ori
                                                ,par$oid_sector_ori => par$oid_sector_ori
                                                ,par$oid_delegacion_des => par$oid_delegacion_des
                                                ,par$oid_planta_des => par$oid_planta_des
                                                ,par$oid_sector_des => par$oid_sector_des
                                                ,par$cod_cliente_des => null
                                                ,par$cod_subcliente_des => null
                                                ,par$cod_pto_servicio_des => null
                                                ,par$cod_servicio => gepr_pcomon_###VERSION###.const$servReenvioSectores
                                                ,par$codIdioma => par$codIdioma);
    */
    -- buscar contenedores

    srecuperar_contenedores(par$oids_contenedores  => par$acosoid_contenedor,
                            par$cods_precintos     => par$acoscod_precinto,
                            par$rc_contenedores    => var$contenedores_cursor,
                            par$rc_detalle_efec_mp => var$efec_mp_cont_cursor,
                            par$rc_precintos       => var$precintos_contenedores,
                            par$rc_lst_val_cont    => var$lst_val_cnt,
                            par$inserts            => var$inserts_int,
                            par$selects            => var$selects_int);

    par$inserts := par$inserts + var$inserts_int;
    par$selects := par$selects + var$selects_int;

    var$es_doc_agrupador := fverificar_documento_agrupador(par$rc_contenedores => var$contenedores_cursor);

    if par$cod_formulario is null then

      begin

        select pv.des_valor_parametro
          into var$cod_formulario
          from gepr_tparametro p
         inner join gepr_tparametro_valor pv
            on pv.oid_parametro = p.oid_parametro
         where p.cod_parametro = gepr_pcomon_###VERSION###.const$cod_form_move_cont_sect;

      exception
        when no_data_found then
          var$cod_formulario := null;
      end;
    else
      var$cod_formulario := par$cod_formulario;
    end if;

    IF (var$es_doc_agrupador = 0) THEN

      screar_parametros_documento(par$cod_formulario            => var$cod_formulario,
                                  par$fyh_plan_certificacion    => par$fyh_plan_certificacion,
                                  par$fyh_gestion               => par$fyh_gestion,
                                  par$oid_delegacion_origen     => par$oid_delegacion_ori,
                                  par$oid_planta_origen         => par$oid_planta_ori,
                                  par$oid_sector_origen         => par$oid_sector_ori,
                                  par$oid_delegacion_destino    => par$oid_delegacion_des,
                                  par$oid_planta_destino        => par$oid_planta_des,
                                  par$oid_sector_destino        => par$oid_sector_des,
                                  par$oid_cliente_destino       => null,
                                  par$oid_subcliente_destino    => null,
                                  par$oid_puntoservicio_destino => null,
                                  par$usuario                   => par$usuario,
                                  par$codcultura                => par$cod_cultura,
                                  par$cod_tipo_doc              => const$CodTipoDocReenvioSector,
                                  par$rc_contenedores           => var$contenedores_cursor,
                                  par$rc_detalle_efec_mp        => var$efec_mp_cont_cursor,
                                  par$rc_precintos              => var$precintos_contenedores,
                                  par$rc_lst_val_cont           => var$lst_val_cnt,
                                  par$cod_comprobante           => par$cod_comprobante,
                                  par$oid_documento             => par$oid_documento,
                                  par$inserts                   => var$inserts_int,
                                  par$updates                   => var$updates_int,
                                  par$deletes                   => var$deletes_int,
                                  par$selects                   => var$selects_int);

      par$inserts := par$inserts + var$inserts_int;
      par$updates := par$updates + var$updates_int;
      par$deletes := par$deletes + var$deletes_int;
      par$selects := par$selects + var$selects_int;

    ELSE

      screar_parametros_grupo_doc(par$cod_formulario            => var$cod_formulario,
                                  par$fyh_plan_certificacion    => par$fyh_plan_certificacion,
                                  par$fyh_gestion               => par$fyh_gestion,
                                  par$oid_delegacion_origen     => par$oid_delegacion_ori,
                                  par$oid_planta_origen         => par$oid_planta_ori,
                                  par$oid_sector_origen         => par$oid_sector_ori,
                                  par$oid_delegacion_destino    => par$oid_delegacion_des,
                                  par$oid_planta_destino        => par$oid_planta_des,
                                  par$oid_sector_destino        => par$oid_sector_des,
                                  par$oid_cliente_destino       => null,
                                  par$oid_subcliente_destino    => null,
                                  par$oid_puntoservicio_destino => null,
                                  par$usuario                   => par$usuario,
                                  par$codcultura                => par$cod_cultura,
                                  par$cod_tipo_doc              => const$CodTipoDocReenvioSector,
                                  par$rc_contenedores           => var$contenedores_cursor,
                                  par$rc_detalle_efec_mp        => var$efec_mp_cont_cursor,
                                  par$rc_precintos              => var$precintos_contenedores,
                                  par$rc_lst_val_cont           => var$lst_val_cnt,
                                  par$cod_comprobante           => par$cod_comprobante,
                                  par$inserts                   => var$inserts_int,
                                  par$updates                   => var$updates_int,
                                  par$deletes                   => var$deletes_int,
                                  par$selects                   => var$selects_int);

      par$inserts := par$inserts + var$inserts_int;
      par$updates := par$updates + var$updates_int;
      par$deletes := par$deletes + var$deletes_int;
      par$selects := par$selects + var$selects_int;

    END IF;

    var$registros := par$inserts + par$updates + par$deletes + par$selects;

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

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$registros := par$inserts + par$updates + par$deletes +
                       par$selects;

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

  END sreenvio_entre_sectores;

  PROCEDURE sreenvio_entre_clientes(par$cod_formulario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$fyh_plan_certificacion IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                    par$fyh_gestion            IN gepr_pcomon_###VERSION###.tipo$fyh_
                                    /* origem */,
                                    par$oid_delegacion_ori IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_planta_ori     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_sector_ori     IN gepr_pcomon_###VERSION###.tipo$oid_
                                    /* destino */,
                                    par$oid_cliente_des      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_subcliente_des   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_pto_servicio_des IN gepr_pcomon_###VERSION###.tipo$oid_
                                    /* arrays asociativos contenedor */,
                                    par$acosoid_contenedor IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$acoscod_precinto   IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$acosrowver         IN gepr_pcomon_###VERSION###.tipo$nels_,
                                    par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                                    par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_comprobante    OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$oid_documento      out gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$info_ejecucion     IN VARCHAR2,
                                    par$hacer_commit       in gepr_pcomon_###VERSION###.tipo$nbol_,
                                    par$cod_ejecucion      OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$updates            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$deletes            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                    par$selects            OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$contenedores_cursor    gepr_pcomon_###VERSION###.tipo$contenedores;
    var$efec_mp_cont_cursor    gepr_pcomon_###VERSION###.tipo$efec_mp_contenedores;
    var$precintos_contenedores gepr_pcomon_###VERSION###.tipo$precintos_contenedores;
    var$lst_val_cnt            gepr_pcomon_###VERSION###.tipo$lst_val_contenedores;
    var$es_doc_agrupador       gepr_pcomon_###VERSION###.tipo$nbol_;

    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err         VARCHAR2(4000);
    var$inicio      TIMESTAMP;

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sreenvio_entre_clientes';

  BEGIN

    var$inicio := SYSTIMESTAMP;
    /*svalidar_datos_contenedores(par$cod_formulario => par$cod_formulario
    ,par$oid_delegacion_ori => par$oid_delegacion_ori
    ,par$oid_planta_ori => par$oid_planta_ori
    ,par$oid_sector_ori => par$oid_sector_ori
    ,par$oid_delegacion_des => null
    ,par$oid_planta_des => null
    ,par$oid_sector_des => null
    ,par$cod_cliente_des => par$cod_cliente_des
    ,par$cod_subcliente_des => par$cod_subcliente_des
    ,par$cod_pto_servicio_des => par$cod_pto_servicio_des
    ,par$cod_servicio => gepr_pcomon_###VERSION###.const$servReenvioClientes
    ,par$codIdioma => par$codIdioma);*/

    -- buscar contenedores
    srecuperar_contenedores(par$oids_contenedores  => par$acosoid_contenedor,
                            par$cods_precintos     => par$acoscod_precinto,
                            par$rc_contenedores    => var$contenedores_cursor,
                            par$rc_detalle_efec_mp => var$efec_mp_cont_cursor,
                            par$rc_precintos       => var$precintos_contenedores,
                            par$rc_lst_val_cont    => var$lst_val_cnt,
                            par$inserts            => var$inserts_int,
                            par$selects            => var$selects_int);

    par$inserts := par$inserts + var$inserts_int;
    par$selects := par$selects + var$selects_int;

    var$es_doc_agrupador := fverificar_documento_agrupador(par$rc_contenedores => var$contenedores_cursor);

    IF (var$es_doc_agrupador = 0) THEN

      screar_parametros_documento(par$cod_formulario            => par$cod_formulario,
                                  par$fyh_plan_certificacion    => par$fyh_plan_certificacion,
                                  par$fyh_gestion               => par$fyh_gestion,
                                  par$oid_delegacion_origen     => par$oid_delegacion_ori,
                                  par$oid_planta_origen         => par$oid_planta_ori,
                                  par$oid_sector_origen         => par$oid_sector_ori,
                                  par$oid_delegacion_destino    => par$oid_delegacion_ori,
                                  par$oid_planta_destino        => par$oid_planta_ori,
                                  par$oid_sector_destino        => par$oid_sector_ori,
                                  par$oid_cliente_destino       => par$oid_cliente_des,
                                  par$oid_subcliente_destino    => par$oid_subcliente_des,
                                  par$oid_puntoservicio_destino => par$oid_pto_servicio_des,
                                  par$usuario                   => par$usuario,
                                  par$codcultura                => par$cod_cultura,
                                  par$cod_tipo_doc              => const$CodTipoDocReenvioCliente,
                                  par$rc_contenedores           => var$contenedores_cursor,
                                  par$rc_detalle_efec_mp        => var$efec_mp_cont_cursor,
                                  par$rc_precintos              => var$precintos_contenedores,
                                  par$rc_lst_val_cont           => var$lst_val_cnt,
                                  par$cod_comprobante           => par$cod_comprobante,
                                  par$oid_documento             => par$oid_documento,
                                  par$inserts                   => var$inserts_int,
                                  par$updates                   => var$updates_int,
                                  par$deletes                   => var$deletes_int,
                                  par$selects                   => var$selects_int);

      par$inserts := par$inserts + var$inserts_int;
      par$updates := par$updates + var$updates_int;
      par$deletes := par$deletes + var$deletes_int;
      par$selects := par$selects + var$selects_int;

    ELSE

      screar_parametros_grupo_doc(par$cod_formulario            => par$cod_formulario,
                                  par$fyh_plan_certificacion    => par$fyh_plan_certificacion,
                                  par$fyh_gestion               => par$fyh_gestion,
                                  par$oid_delegacion_origen     => par$oid_delegacion_ori,
                                  par$oid_planta_origen         => par$oid_planta_ori,
                                  par$oid_sector_origen         => par$oid_sector_ori,
                                  par$oid_delegacion_destino    => par$oid_delegacion_ori,
                                  par$oid_planta_destino        => par$oid_planta_ori,
                                  par$oid_sector_destino        => par$oid_sector_ori,
                                  par$oid_cliente_destino       => par$oid_cliente_des,
                                  par$oid_subcliente_destino    => par$oid_subcliente_des,
                                  par$oid_puntoservicio_destino => par$oid_pto_servicio_des,
                                  par$usuario                   => par$usuario,
                                  par$codcultura                => par$cod_cultura,
                                  par$cod_tipo_doc              => const$CodTipoDocReenvioCliente,
                                  par$rc_contenedores           => var$contenedores_cursor,
                                  par$rc_detalle_efec_mp        => var$efec_mp_cont_cursor,
                                  par$rc_precintos              => var$precintos_contenedores,
                                  par$rc_lst_val_cont           => var$lst_val_cnt,
                                  par$cod_comprobante           => par$cod_comprobante,
                                  par$inserts                   => var$inserts_int,
                                  par$updates                   => var$updates_int,
                                  par$deletes                   => var$deletes_int,
                                  par$selects                   => var$selects_int);

      par$inserts := par$inserts + var$inserts_int;
      par$updates := par$updates + var$updates_int;
      par$deletes := par$deletes + var$deletes_int;
      par$selects := par$selects + var$selects_int;

    END IF;

    var$registros := par$inserts + par$updates + par$deletes + par$selects;

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

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$registros := par$inserts + par$updates + par$deletes +
                       par$selects;

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

  END sreenvio_entre_clientes;

  PROCEDURE svalidar_datos_contenedores(par$cod_formulario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_delegacion_ori   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_ori       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_ori       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_delegacion_des   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_planta_des       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_sector_des       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_cliente_des      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_subcliente_des   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_pto_servicio_des IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_servicio         IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$codcultura           IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    -- armazena as mensagens de validacao
    var$mensajes_validacion gepr_pcomon_###VERSION###.tipo$obs_;
  BEGIN

    var$mensajes_validacion := '#';

    IF (par$cod_formulario IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codcultura,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'CodigoFormulario',
                                                                     0) ||
                                 chr(10);
    END IF;

    IF (par$oid_delegacion_ori IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codcultura,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Delegacion Origen',
                                                                     0) ||
                                 chr(10);
    END IF;

    IF (par$oid_planta_ori IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codcultura,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Planta Origen',
                                                                     0) ||
                                 chr(10);
    END IF;

    IF (par$oid_sector_ori IS NULL) THEN
      var$mensajes_validacion := var$mensajes_validacion ||
                                 GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codcultura,
                                                                     gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                     gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                     gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                     'Sector Origen',
                                                                     0) ||
                                 chr(10);
    END IF;

    /*IF par$cod_servicio = gepr_pcomon_###VERSION###.const$servReenvioSectores THEN

            IF (par$oid_delegacion_des IS NULL) THEN
               var$mensajes_validacion := var$mensajes_validacion || GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codcultura, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Delegacion Destino', 0) || chr(10);
            END IF;

            IF (par$oid_planta_des IS NULL) THEN
               var$mensajes_validacion := var$mensajes_validacion || GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codcultura, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Planta Destino', 0) || chr(10);
            END IF;

            IF (par$oid_sector_des IS NULL) THEN
               var$mensajes_validacion := var$mensajes_validacion || GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codcultura, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Sector Destino', 0) || chr(10);
            END IF;

     END IF;

    IF par$cod_servicio = gepr_pcomon_###VERSION###.const$servReenvioClientes THEN

            IF (par$cod_cliente_des IS NULL) THEN
               var$mensajes_validacion := var$mensajes_validacion || GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codcultura, gepr_pcomon_###VERSION###.const$codExpAtributoObrig, gepr_pcomon_###VERSION###.const$CodFuncDicComon, gepr_pcomon_###VERSION###.const$codAplicacionSaldos, 'Cliente Destino', 0) || chr(10);
            END IF;

     END IF;*/

    var$mensajes_validacion := var$mensajes_validacion || '#';

    -- se houver alguma mensagem na vari?vel, dispara o RAISE
    IF (length(var$mensajes_validacion) > 2) THEN
      RAISE_APPLICATION_ERROR(-20001, var$mensajes_validacion);
    END IF;

  END svalidar_datos_contenedores;

 PROCEDURE sconsultar_contenedor_sector(par$codIdioma                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codTipoContenedor         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codEstadoConenedor        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$fyhArmadoDesde            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                         par$fyhArmadoHasta            IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                         par$bolmayorNivel             IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$bolPackModular            IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$bolretornarElementosHijos IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$aPrecintos                IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$codCliente                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codSubCliente             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codPtoServicio            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codDelegacion             IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codPlanta                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$acodSector                IN gepr_pcomon_###VERSION###.tipo$cods_,
                                         par$codCanal                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$codSubcanal               IN gepr_pcomon_###VERSION###.tipo$cod_,
										 par$bolRecSoloContSinPosicion     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                         par$rcContenedores            OUT SYS_REFCURSOR,
                                         par$rcEfectivos               OUT SYS_REFCURSOR,
                                         par$rcMediosPago              OUT SYS_REFCURSOR,
                                         par$cod_usuario               in gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$info_ejecucion            IN VARCHAR2,
                                         par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$existeDelegacion INTEGER;
    var$existePlanta     INTEGER;
    var$existeSector     INTEGER;
    var$existeCanal      INTEGER;
    var$sql              CLOB;
    var$where            CLOB;
    var$itens            CLOB;

    var$inicio TIMESTAMP;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sconsultar_contenedor_sector';
    var$err VARCHAR2(4000);

  begin

    var$inicio  := SYSTIMESTAMP;
    par$selects := 0;

    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TCONTENEDORXSECTOR';

    var$sql := 'SELECT C.OID_CONTENEDOR,
       TC.COD_TIPO_CONTENEDOR,
       C.COD_ESTADO,
       C.GMT_CREACION,
       VC.COD_SECTOR,
       VC.OID_SECTOR,
       VC.COD_PLANTA,
       VC.OID_PLANTA,
       VC.COD_DELEGACION,
       VC.OID_DELEGACION,
       PC.COD_PRECINTO,
       VC.COD_CLIENTE,
       VC.COD_SUBCLIENTE,
       VC.COD_PTO_SERVICIO,
       VC.COD_CANAL,
       VC.COD_SUBCANAL,
       PS.COD_POSICION,
       VC.OID_CUENTA,
     TC.NEC_CANTIDAD,
     VC.DES_CLIENTE,
     PC.BOL_PRECINTO_AUTOMATICO,
     TC.DES_TIPO_CONTENEDOR,
     VC.DES_SUBCLIENTE,
     VC.DES_PTO_SERVICIO,
     VC.DES_CANAL,
     VC.DES_SUBCANAL,
     C.FEC_VENCIMIENTO,
     C.DES_USUARIO_CREACION,
     C.COD_PUESTO
 FROM SAPR_TCONTENEDOR C
 INNER JOIN SAPR_TTIPO_CONTENEDOR TC
    ON C.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
 INNER JOIN SAPR_VCUENTA VC ON VC.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
 INNER JOIN SAPR_TPRECINTOXCONTENEDOR PC ON PC.OID_CONTENEDOR = C.OID_CONTENEDOR
 LEFT JOIN SAPR_TPOSICION_SECTOR PS ON PS.OID_CONTENEDOR = C.OID_CONTENEDOR
 WHERE ';

    if par$bolPackModular is not null then
      var$where := var$where || ' C.BOL_PACK_MODULAR = ' ||
                   par$bolPackModular;
    end if;

    if par$codCliente is not null then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' VC.COD_CLIENTE = ' || chr(39) ||
                   par$codCliente || chr(39);

      if par$codSubCliente is not null then

        if var$where is not null then
          var$where := var$where || ' AND ';
        end if;

        var$where := var$where || ' VC.COD_SUBCLIENTE = ' || chr(39) ||
                     par$codSubCliente || chr(39);

        if par$codPtoServicio is not null then

          if var$where is not null then
            var$where := var$where || ' AND ';
          end if;

          var$where := var$where || ' VC.COD_SUBCLIENTE = ' || chr(39) ||
                       par$codPtoServicio || chr(39);

        end if;

      end if;

    end if;

    if par$codTipoContenedor is not null then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' TC.COD_TIPO_CONTENEDOR = ' || chr(39) ||
                   par$codTipoContenedor || chr(39);

    end if;

    if par$codEstadoConenedor is not null then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' C.COD_ESTADO = ' || chr(39) ||
                   par$codEstadoConenedor || chr(39);

    end if;

    if par$fyhArmadoDesde is not null then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' C.GMT_CREACION >= ' || CHR(39) ||
                   TO_TIMESTAMP_TZ(par$fyhArmadoDesde || '+00:00',
                                   'DD/MM/YYYY HH24:MI:SS TZH:TZM') ||
                   CHR(39);

    end if;

    if par$fyhArmadoHasta is not null then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' C.GMT_CREACION <= ' || CHR(39) ||
                   TO_TIMESTAMP_TZ(par$fyhArmadoHasta || '+00:00',
                                   'DD/MM/YYYY HH24:MI:SS TZH:TZM') ||
                   CHR(39);

    end if;

    if par$codCanal is not null then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' VC.COD_CANAL = ' || chr(39) ||
                   par$codCanal || chr(39);

      if par$codSubcanal is not null then

        if var$where is not null then
          var$where := var$where || ' AND ';
        end if;

        var$where := var$where || ' VC.COD_SUBCANAL = ' || chr(39) ||
                     par$codSubcanal || chr(39);

      end if;

    end if;

	 if par$bolRecSoloContSinPosicion > 0 then
      
       if var$where is not null then
          var$where := var$where || ' AND ';
        end if;
        
      var$where := var$where || ' NOT EXISTS(SELECT * 
                                             FROM SAPR_TPOSICION_SECTOR PS 
                                             WHERE PS.OID_CONTENEDOR = C.OID_CONTENEDOR) ';
    end if;

    if par$codDelegacion is not null then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' VC.COD_DELEGACION = ' || chr(39) ||
                   par$codDelegacion || chr(39);

    end if;

    if par$codPlanta is not null then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' VC.COD_PLANTA = ' || chr(39) ||
                   par$codPlanta || chr(39);

    end if;

    if par$aPrecintos is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_cod(par$array => par$aPrecintos) then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' PC.COD_PRECINTO IN ( ';
      var$itens := null;

      for idx in par$aPrecintos.first .. par$aPrecintos.last loop

        if var$itens is not null then
          var$itens := var$itens || ',';
        end if;

        var$itens := var$itens || chr(39) || par$aPrecintos(idx) || chr(39);

      end loop;

      var$where := var$where || var$itens || ')';

    end if;

    if par$acodSector is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_cod(par$array => par$acodSector) then

      if var$where is not null then
        var$where := var$where || ' AND ';
      end if;

      var$where := var$where || ' VC.COD_SECTOR IN ( ';
      var$itens := null;

      for idx in par$acodSector.first .. par$acodSector.last loop

        if var$itens is not null then
          var$itens := var$itens || ',';
        end if;

        var$itens := var$itens || chr(39) || par$acodSector(idx) || chr(39);

      end loop;

      var$where := var$where || var$itens || ')';

    end if;

    var$sql := var$sql || var$where;

    EXECUTE IMMEDIATE 'INSERT INTO SAPR_GTT_TCONTENEDORXSECTOR ' || var$sql;

    par$selects := par$selects + sql%rowcount;

    open par$rcContenedores for
      select * from SAPR_GTT_TCONTENEDORXSECTOR;

    par$selects := par$selects + sql%rowcount;

    open par$rcEfectivos for
      SELECT VCU.OID_CUENTA,
             CS.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             DEN.COD_DENOMINACION,
             DEN.NUM_VALOR,
             CAL.COD_CALIDAD,
             EC.BOL_DISPONIBLE,
             EC.BOL_BLOQUEADO,
             EC.NEL_CANTIDAD,
             EC.NUM_IMPORTE
        FROM SAPR_VCUENTA VCU
       INNER JOIN SAPR_GTT_TCONTENEDORXSECTOR CS
          ON CS.OID_CUENTA = VCU.OID_CUENTA
       INNER JOIN SAPR_TEFECTIVOXCONTENEDOR EC
          ON EC.OID_CONTENEDOR = CS.OID_CONTENEDOR
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = EC.OID_DIVISA
        LEFT JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = EC.OID_DENOMINACION
        LEFT JOIN GEPR_TCALIDAD CAL
          ON CAL.OID_CALIDAD = EC.OID_CALIDAD;

    par$selects := par$selects + sql%rowcount;

    open par$rcMediosPago for
      SELECT VCU.OID_CUENTA,
             CS.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             MP.COD_MEDIO_PAGO,
             MPC.COD_TIPO_MEDIO_PAGO,
             MPC.BOL_DISPONIBLE,
             MPC.BOL_BLOQUEADO,
             MPC.NEL_CANTIDAD,
             MPC.NUM_IMPORTE
        FROM SAPR_VCUENTA VCU
       INNER JOIN SAPR_GTT_TCONTENEDORXSECTOR CS
          ON CS.OID_CUENTA = VCU.OID_CUENTA
       INNER JOIN SAPR_TMEDIO_PAGOXCONTENEDOR MPC
          ON MPC.OID_CONTENEDOR = CS.OID_CONTENEDOR
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = MPC.OID_DIVISA
        LEFT JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = MPC.OID_MEDIO_PAGO;

    par$selects := par$selects + sql%rowcount;

   gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                         const$nombre_func,
                                                         0,
                                                         0,
                                                         0,
                                                         0,
                                                         par$selects,
                                                         par$selects,
                                                         var$inicio,
                                                         null,
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$cod_usuario,
                                                         par$cod_ejecucion);

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           0,
                                                           0,
                                                           0,
                                                           0,
                                                           par$selects,
                                                           par$selects,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);
  END sconsultar_contenedor_sector;


PROCEDURE SP_CONSULTARDOCGESTIONCONT(par$codIdioma                IN VARCHAR2,
                                       par$fechaDocumentoDesde      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                       par$fechaDocumentoHasta      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                       par$codEstadoDocumento       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codSector                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codDelegacion            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codPlanta                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codCliente               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codSubCliente            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codPtoServicio           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codCanal                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codSubcanal              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codPrecinto              IN gepr_pcomon_###VERSION###.tipo$cods_,
                                       par$codTipoContenedor        IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$codOrigenBusqueda        in gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$rcDocumentos             OUT SYS_REFCURSOR,
                                       par$rcPrecintos              OUT SYS_REFCURSOR,
                                       par$rcDetalleEfecMPDocumento OUT SYS_REFCURSOR,
                                       par$cod_usuario              in gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$info_ejecucion           IN VARCHAR2,
                                       par$cod_ejecucion            OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$deletes                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$existePrecinto  INTEGER;
    var$existeContPai   INTEGER;
    var$existeContFilho INTEGER;
    var$existeCuenta    INTEGER;

    var$inicio TIMESTAMP;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SP_CONSULTARDOCGESTIONCONT';
    var$err VARCHAR2(4000);

    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$precintos varchar2_array;

  BEGIN

    var$inicio  := SYSTIMESTAMP;
    par$selects := 0;
    par$deletes := 0;
    par$inserts := 0;

    --Validacoes
    IF par$fechaDocumentoDesde IS NULL AND par$fechaDocumentoHasta IS NULL THEN
      RAISE_APPLICATION_ERROR(-20001,
                              gepr_putilidades_###VERSION###.FTRADUZIR(par$codIdioma,
                                                                  'MSG_VALIDAR_COD_FECHA_DOC',
                                                                  'SP_CONSULTARDOCGESTIONCONT',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  1));
    END IF;

    IF par$codEstadoDocumento IS NULL THEN
      RAISE_APPLICATION_ERROR(-20002,
                              gepr_putilidades_###VERSION###.FTRADUZIR(par$codIdioma,
                                                                  'MSG_VALIDAR_COD_ESTADO_DOC',
                                                                  'SP_CONSULTARDOCGESTIONCONT',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  1));
    END IF;

    IF par$codSector IS NULL OR par$codDelegacion IS NULL OR
       par$codPlanta IS NULL THEN
      RAISE_APPLICATION_ERROR(-20003,
                              gepr_putilidades_###VERSION###.FTRADUZIR(par$codIdioma,
                                                                  'MSG_VALIDAR_COD_SECTOR',
                                                                  'SP_CONSULTARDOCGESTIONCONT',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  1));
    END IF;

    IF par$codCliente IS NULL AND
       (par$codSubCliente IS NOT NULL OR par$codPtoServicio IS NOT NULL) THEN
      RAISE_APPLICATION_ERROR(-20004,
                              gepr_putilidades_###VERSION###.FTRADUZIR(par$codIdioma,
                                                                  'MSG_VALIDAR_CLIENTE',
                                                                  'SP_CONSULTARCONTPACKMODULAR',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  1));
    END IF;

    IF (par$codCanal IS NULL AND par$codSubcanal IS NOT NULL) OR
       (par$codCanal IS NOT NULL AND par$codSubcanal IS NULL) THEN
      RAISE_APPLICATION_ERROR(-20005,
                              gepr_putilidades_###VERSION###.FTRADUZIR(par$codIdioma,
                                                                  'MSG_VALIDAR_CANAL',
                                                                  'SP_CONSULTARCONTPACKMODULAR',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  1));
    END IF;

    DELETE FROM SAPR_GTT_TOIDS;
    par$deletes := par$deletes + sql%rowcount;

    var$existePrecinto := 0;
    --insere Precinto
    if par$codPrecinto is not null AND par$codPrecinto.COUNT > 0 Then

      var$precintos := gepr_putilidades_###VERSION###.fconverter_array_cod_table(par$table_varchar2 => par$codPrecinto);

      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT 'PRECINTOS', PC.OID_CONTENEDOR
          FROM SAPR_TPRECINTOXCONTENEDOR PC
         INNER JOIN TABLE(var$precintos) P
            ON P.COLUMN_VALUE = PC.COD_PRECINTO
         WHERE P.COLUMN_VALUE IS NOT NULL;

      par$inserts := par$inserts + sql%rowcount;
      par$selects := par$selects + sql%rowcount;

    end if;

    --insere oid documentos
    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)

      WITH documentos AS
       (SELECT D.OID_DOCUMENTO, D.OID_SECTOR_ORIGEN, D.OID_SECTOR_DESTINO
          FROM SAPR_TDOCUMENTO D
         INNER JOIN SAPR_TCONTENEDOR C
            ON C.OID_DOCUMENTO = D.OID_DOCUMENTO
         INNER JOIN SAPR_TTIPO_CONTENEDOR TPCONT
            ON TPCONT.OID_TIPO_CONTENEDOR = C.OID_TIPO_CONTENEDOR
         WHERE (par$fechaDocumentoDesde IS NOT NULL AND
               D.GMT_CREACION >= par$fechaDocumentoDesde OR
               par$fechaDocumentoDesde IS NULL)
           AND (par$fechaDocumentoHasta IS NOT NULL AND
               D.GMT_CREACION <= par$fechaDocumentoHasta OR
               par$fechaDocumentoHasta IS NULL)
           AND (par$codEstadoDocumento IS NOT NULL AND
               D.COD_ESTADO = par$codEstadoDocumento OR
               par$codEstadoDocumento IS NULL)
           AND (par$codTipoContenedor IS NOT NULL AND
               TPCONT.COD_TIPO_CONTENEDOR = par$codTipoContenedor OR
               par$codTipoContenedor IS NULL)
           AND (var$existePrecinto = 1 AND C.OID_CONTENEDOR_PADRE IS NULL AND
               C.OID_CONTENEDOR IN
               (SELECT OID
                   FROM SAPR_GTT_TOIDS
                  WHERE COD_CALIFICADOR = 'PRECINTOS') OR
               var$existePrecinto = 0))

      SELECT 'DOCUMENTOS', DOC.OID_DOCUMENTO
        FROM documentos DOC
       INNER JOIN GEPR_TSECTOR SE_O
          ON SE_O.OID_SECTOR = DOC.OID_SECTOR_ORIGEN
       INNER JOIN GEPR_TPLANTA PL_O
          ON PL_O.OID_PLANTA = SE_O.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION DE_O
          ON DE_O.OID_DELEGACION = PL_O.OID_DELEGACION
       INNER JOIN GEPR_TSECTOR SE_D
          ON SE_D.OID_SECTOR = DOC.OID_SECTOR_DESTINO
       INNER JOIN GEPR_TPLANTA PL_D
          ON PL_D.OID_PLANTA = SE_D.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION DE_D
          ON DE_D.OID_DELEGACION = PL_D.OID_DELEGACION

       WHERE ((par$codOrigenBusqueda = gepr_pcomon_###VERSION###.const$codOriBusquedaEnviado or
               par$codOrigenBusqueda = gepr_pcomon_###VERSION###.const$codOriBusquedaAmbos) and ((par$codSector IS NOT NULL AND
             SE_O.COD_SECTOR = par$codSector) OR par$codSector IS NULL) AND
             ((par$codDelegacion IS NOT NULL AND
             DE_O.COD_DELEGACION = par$codDelegacion) OR
             par$codDelegacion IS NULL) AND
             ((par$codPlanta IS NOT NULL AND
             PL_O.COD_PLANTA = par$codPlanta) OR par$codPlanta IS NULL))
          OR ((par$codOrigenBusqueda = gepr_pcomon_###VERSION###.const$codOriBusquedaRecebidos or
               par$codOrigenBusqueda = gepr_pcomon_###VERSION###.const$codOriBusquedaAmbos) and ((par$codSector IS NOT NULL AND
             SE_D.COD_SECTOR = par$codSector) OR par$codSector IS NULL) AND
             ((par$codDelegacion IS NOT NULL AND
             DE_D.COD_DELEGACION = par$codDelegacion) OR
             par$codDelegacion IS NULL) AND
             ((par$codPlanta IS NOT NULL AND
             PL_D.COD_PLANTA = par$codPlanta) OR par$codPlanta IS NULL));

    par$inserts := par$inserts + sql%rowcount;
    par$selects := par$selects + sql%rowcount;

    --insere oid contenedores maior nivel
    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)
      SELECT 'CONTENEDORESPAI', C.OID_CONTENEDOR
        FROM SAPR_TCONTENEDOR C
       WHERE C.OID_CONTENEDOR_MAYOR_NIVEL IS NULL
         AND C.OID_DOCUMENTO IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'DOCUMENTOS');

    par$inserts := par$inserts + sql%rowcount;
    par$selects := par$selects + sql%rowcount;

    /*    --insere oid contenedores que n?o s?o de maior n?vel
    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)
      SELECT 'CONTENEDORESFILHO', C.OID_CONTENEDOR
        FROM SAPR_TCONTENEDOR C
       INNER JOIN SAPR_TDOCUMENTO D
          ON D.OID_DOCUMENTO = C.OID_DOCUMENTO
       WHERE C.OID_CONTENEDOR_MAYOR_NIVEL IS NOT NULL
         AND D.OID_DOCUMENTO IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'DOCUMENTOS');

    par$inserts := par$inserts + sql%rowcount;
    par$selects := par$selects + sql%rowcount;*/

    /*SELECT COUNT(0)
      INTO var$existeContPai
      FROM SAPR_GTT_TOIDS
     WHERE COD_CALIFICADOR = 'CONTENEDORESPAI';

    par$selects := par$selects + sql%rowcount;

    SELECT COUNT(0)
      INTO var$existeContFilho
      FROM SAPR_GTT_TOIDS
     WHERE COD_CALIFICADOR = 'CONTENEDORESFILHO';

    par$selects := par$selects + sql%rowcount;*/

    var$existeCuenta := 0;

    /* IF var$existeContPai > 0 OR var$existeContFilho > 0 THEN

        IF par$codCliente IS NOT NULL OR par$codCanal IS NOT NULL OR
           par$codSubcanal IS NOT NULL THEN
          var$existeCuenta := 1;
          --insere clientes
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'CUENTAS', CU.OID_CUENTA
              FROM SAPR_TCONTENEDOR C
             INNER JOIN SAPR_VCUENTA CU
                ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
             INNER JOIN GEPR_TSECTOR SEC
                ON SEC.OID_SECTOR = CU.OID_SECTOR
             WHERE (par$codCliente IS NOT NULL AND
                   CU.COD_CLIENTE = par$codCliente OR par$codCliente IS NULL)
               AND (par$codSubCliente IS NOT NULL AND
                   CU.COD_SUBCLIENTE = par$codSubCliente OR
                   par$codSubCliente IS NULL OR CU.COD_SUBCLIENTE IS NULL)
               AND (par$codPtoServicio IS NOT NULL AND
                   CU.COD_PTO_SERVICIO = par$codPtoServicio OR
                   par$codPtoServicio IS NULL OR CU.COD_PTO_SERVICIO IS NULL)
               AND (par$codCanal IS NOT NULL AND CU.COD_CANAL = par$codCanal OR
                   par$codCanal IS NULL)
               AND (par$codSubcanal IS NOT NULL AND
                   CU.COD_SUBCANAL = par$codSubcanal OR
                   par$codSubcanal IS NULL)
            UNION

            SELECT 'CUENTAS', CU.OID_CUENTA
              FROM SAPR_TEFECTIVOXCONTENEDOR C
             INNER JOIN SAPR_VCUENTA CU
                ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
             INNER JOIN GEPR_TSECTOR SEC
                ON SEC.OID_SECTOR = CU.OID_SECTOR
             WHERE (par$codCliente IS NOT NULL AND
                   CU.COD_CLIENTE = par$codCliente OR par$codCliente IS NULL)
               AND (par$codSubCliente IS NOT NULL AND
                   CU.COD_SUBCLIENTE = par$codSubCliente OR
                   par$codSubCliente IS NULL OR CU.COD_SUBCLIENTE IS NULL)
               AND (par$codPtoServicio IS NOT NULL AND
                   CU.COD_PTO_SERVICIO = par$codPtoServicio OR
                   par$codPtoServicio IS NULL OR CU.COD_PTO_SERVICIO IS NULL)
               AND (par$codCanal IS NOT NULL AND CU.COD_CANAL = par$codCanal OR
                   par$codCanal IS NULL)
               AND (par$codSubcanal IS NOT NULL AND
                   CU.COD_SUBCANAL = par$codSubcanal OR
                   par$codSubcanal IS NULL)
            UNION

            SELECT 'CUENTAS', CU.OID_CUENTA
              FROM SAPR_TMEDIO_PAGOXCONTENEDOR C
             INNER JOIN SAPR_VCUENTA CU
                ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
             INNER JOIN GEPR_TSECTOR SEC
                ON SEC.OID_SECTOR = CU.OID_SECTOR
             WHERE (par$codCliente IS NOT NULL AND
                   CU.COD_CLIENTE = par$codCliente OR par$codCliente IS NULL)
               AND (par$codSubCliente IS NOT NULL AND
                   CU.COD_SUBCLIENTE = par$codSubCliente OR
                   par$codSubCliente IS NULL OR CU.COD_SUBCLIENTE IS NULL)
               AND (par$codPtoServicio IS NOT NULL AND
                   CU.COD_PTO_SERVICIO = par$codPtoServicio OR
                   par$codPtoServicio IS NULL OR CU.COD_PTO_SERVICIO IS NULL)
               AND (par$codCanal IS NOT NULL AND CU.COD_CANAL = par$codCanal OR
                   par$codCanal IS NULL)
               AND (par$codSubcanal IS NOT NULL AND
                   CU.COD_SUBCANAL = par$codSubcanal OR
                   par$codSubcanal IS NULL);

          par$inserts := par$inserts + sql%rowcount;
          par$selects := par$selects + sql%rowcount;

        END IF;

      END IF;

      IF var$existeContPai > 0 THEN
        SP_INSVALORESCONTENEDOR(TRUE, var$existeCuenta);

        par$inserts := par$inserts + sql%rowcount;
        par$selects := par$selects + sql%rowcount;

      END IF;

      IF var$existeContFilho > 0 THEN
        SP_INSVALORESCONTENEDOR(FALSE, var$existeCuenta);

        par$inserts := par$inserts + sql%rowcount;
        par$selects := par$selects + sql%rowcount;

      END IF;
    */
    COMMIT;

    OPEN par$rcDocumentos FOR
      SELECT DISTINCT CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.COD_COMPROBANTE
                        ELSE
                         GD.COD_COMPROBANTE
                      END COD_COMPROBANTE,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.OID_DOCUMENTO
                        ELSE
                         GD.OID_GRUPO_DOCUMENTO
                      END OID_DOCUMENTO,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.COD_ESTADO
                        ELSE
                         GD.COD_ESTADO
                      END COD_ESTADO,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.OID_SECTOR_ORIGEN
                        ELSE
                         GD.OID_SECTOR_ORIGEN
                      END OID_SECTOR_ORIGEN,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.OID_SECTOR_DESTINO
                        ELSE
                         GD.OID_SECTOR_DESTINO
                      END OID_SECTOR_DESTINO,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.COD_EXTERNO
                        ELSE
                         NULL
                      END COD_EXTERNO,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.GMT_CREACION
                        ELSE
                         GD.GMT_CREACION
                      END GMT_CREACION,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.GMT_MODIFICACION
                        ELSE
                         GD.GMT_MODIFICACION
                      END GMT_MODIFICACION,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         D.ROWVER
                        ELSE
                         GD.ROWVER
                      END ROWVER,
                      C.COD_ESTADO AS COD_ESTADO_CONTENEDOR,
                      C.OID_TIPO_CONTENEDOR,
                      C.OID_CONTENEDOR,
                      CUO.COD_CLIENTE COD_CLIENTE,
                      CUO.DES_CLIENTE DES_CLIENTE,
                      CUO.COD_SUBCLIENTE COD_SUBCLIENTE,
                      CUO.DES_SUBCLIENTE DES_SUBCLIENTE,
                      CUO.COD_PTO_SERVICIO COD_PTO_SERVICIO,
                      CUO.DES_PTO_SERVICIO DES_PTO_SERVICIO,
                      CUO.COD_CANAL COD_CANAL,
                      CUO.DES_CANAL DES_CANAL,
                      CUO.COD_SUBCANAL COD_SUBCANAL,
                      CUO.DES_SUBCANAL DES_SUBCANAL,
                      CUO.COD_SECTOR AS COD_SECTOR_ORIGEN,
                      CUO.DES_SECTOR AS DES_SECTOR_ORIGEN,
                      CUO.COD_PLANTA AS COD_PLANTA_ORIGEN,
                      CUO.COD_DELEGACION AS COD_DELEGACION_ORIGEN,
                      CUD.COD_SECTOR AS COD_SECTOR_DESTINO,
                      CUD.DES_SECTOR AS DES_SECTOR_DESTINO,
                      CUD.COD_PLANTA AS COD_PLANTA_DESTINO,
                      CUD.COD_DELEGACION AS COD_DELEGACION_DESTINO,
                      TPCONT.COD_TIPO_CONTENEDOR,
                      CASE
                        WHEN D.OID_GRUPO_DOCUMENTO IS NULL THEN
                         0
                        ELSE
                         1
                      END ES_GRUPO
        FROM SAPR_TDOCUMENTO D
       INNER JOIN SAPR_TCONTENEDOR C
          ON C.OID_DOCUMENTO = D.OID_DOCUMENTO
       INNER JOIN SAPR_VCUENTA CUO
          ON CUO.OID_CUENTA = D.OID_CUENTA_ORIGEN
       INNER JOIN SAPR_VCUENTA CUD
          ON CUD.OID_CUENTA = D.OID_CUENTA_DESTINO
       INNER JOIN SAPR_TTIPO_CONTENEDOR TPCONT
          ON TPCONT.OID_TIPO_CONTENEDOR = C.OID_TIPO_CONTENEDOR
       INNER JOIN (SELECT OID
                     FROM SAPR_GTT_TOIDS gti
                    WHERE gti.COD_CALIFICADOR = 'DOCUMENTOS') DT
          ON DT.OID = D.OID_DOCUMENTO
        LEFT JOIN SAPR_TGRUPO_DOCUMENTO GD
          ON GD.OID_GRUPO_DOCUMENTO = D.OID_GRUPO_DOCUMENTO;

    par$selects := par$selects + sql%rowcount;

    --busca dados dos precintos
    OPEN par$rcPrecintos FOR
      SELECT PXC.OID_CONTENEDOR,
             PXC.OID_PRECINTOXCONTENEDOR,
             PXC.COD_PRECINTO
        FROM SAPR_TPRECINTOXCONTENEDOR PXC
       WHERE PXC.OID_CONTENEDOR IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR IN
                     ('CONTENEDORESPAI', 'CONTENEDORESFILHO'));

    par$selects := par$selects + sql%rowcount;

    OPEN par$rcDetalleEfecMPDocumento FOR
    --Documentos individuais [Efectivos]
      SELECT EFEC.OID_DOCUMENTO,
             DIV.COD_ISO_DIVISA,
             DEN.COD_DENOMINACION,
             CAL.COD_CALIDAD,
             NULL COD_TIPO_MEDIO_PAGO,
             NULL COD_MEDIO_PAGO,
             NULL DES_MEDIO_PAGO,
             SUM(EFEC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(EFEC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TEFECTIVOXDOCUMENTO EFEC
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = EFEC.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = EFEC.OID_DENOMINACION
         AND DEN.OID_DIVISA = DIV.OID_DIVISA
       INNER JOIN (SELECT GTI.OID
                     FROM SAPR_GTT_TOIDS GTI
                    WHERE GTI.COD_CALIFICADOR = 'DOCUMENTOS') DT
          ON DT.OID = EFEC.OID_DOCUMENTO
        LEFT JOIN GEPR_TCALIDAD CAL
          ON CAL.OID_CALIDAD = EFEC.OID_CALIDAD
       GROUP BY EFEC.OID_DOCUMENTO,
                DIV.COD_ISO_DIVISA,
                DEN.COD_DENOMINACION,
                CAL.COD_CALIDAD
      UNION ALL
      --Documentos individuais [Medios Pagos]
      SELECT MPD.OID_DOCUMENTO,
             DIV.COD_ISO_DIVISA,
             NULL COD_DENOMINACION,
             NULL COD_CALIDAD,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_MEDIO_PAGO,
             MP.DES_MEDIO_PAGO,
             SUM(MPD.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(MPD.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TMEDIO_PAGOXDOCUMENTO MPD
       INNER JOIN (SELECT GTI.OID
                     FROM SAPR_GTT_TOIDS GTI
                    WHERE GTI.COD_CALIFICADOR = 'DOCUMENTOS') DT
          ON DT.OID = MPD.OID_DOCUMENTO
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = MPD.OID_DIVISA
        LEFT JOIN GEPR_TMEDIO_PAGO MP
          ON MP.OID_MEDIO_PAGO = MPD.OID_MEDIO_PAGO
         AND MP.OID_DIVISA = DIV.OID_DIVISA
       GROUP BY MPD.OID_DOCUMENTO,
                DIV.COD_ISO_DIVISA,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.COD_MEDIO_PAGO,
                MP.DES_MEDIO_PAGO;

    par$selects   := par$selects + sql%rowcount;
    var$registros := par$deletes + par$selects + par$inserts;

    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                         const$nombre_func,
                                                         par$inserts,
                                                         0,
                                                         par$deletes,
                                                         0,
                                                         par$selects,
                                                         var$registros,
                                                         var$inicio,
                                                         null,
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$cod_usuario,
                                                         par$cod_ejecucion);

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           0,
                                                           par$deletes,
                                                           0,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'C',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END SP_CONSULTARDOCGESTIONCONT;


PROCEDURE SP_INSVALORESCONTENEDOR(par$bolPai IN gepr_pcomon_###VERSION###.tipo$bol_, par$existeCuenta IN INTEGER) IS
    BEGIN
        IF par$bolPai THEN
            INSERT INTO SAPR_GTT_TEFECMPXCONTENEDOR
                (OID_CONTENEDOR,
                  COD_ISO_DIVISA,
                  COD_DENOMINACION,
                  COD_CALIDAD,
                  COD_TIPO_MEDIO_PAGO,
                  COD_MEDIO_PAGO,
                  DES_MEDIO_PAGO,
                  BOL_DISPONIBLE,
                  BOL_BLOQUEADO,
                  NEL_CANTIDAD,
                  NUM_IMPORTE,
                  COD_CLIENTE,
                  DES_CLIENTE,
                  COD_SUBCLIENTE,
                  DES_SUBCLIENTE,
                  COD_PTO_SERVICIO,
                  DES_PTO_SERVICIO,
                  COD_CANAL,
                  DES_CANAL,
                  COD_SUBCANAL,
                  DES_SUBCANAL
                )
WITH contenedores AS (SELECT OID
            FROM SAPR_GTT_TOIDS
            WHERE COD_CALIFICADOR = 'CONTENEDORESPAI'),
    efectivoMP AS (SELECT C1.OID,
                          DIV.COD_ISO_DIVISA,
                          DEN.COD_DENOMINACION,
                          CAL.COD_CALIDAD,
                          NULL COD_TIPO_MEDIO_PAGO,
                          NULL COD_MEDIO_PAGO,
                          NULL DES_MEDIO_PAGO,
                          EXC.BOL_DISPONIBLE,
                          EXC.BOL_BLOQUEADO,
                          SUM(EXC.NEL_CANTIDAD) NEL_CANTIDAD,
                          SUM(EXC.NUM_IMPORTE) NUM_IMPORTE,
                          CUEFEC.COD_CLIENTE,
                          CUEFEC.DES_CLIENTE,
                          CUEFEC.COD_SUBCLIENTE,
                          CUEFEC.DES_SUBCLIENTE,
                          CUEFEC.COD_PTO_SERVICIO,
                          CUEFEC.DES_PTO_SERVICIO,
                          CUEFEC.COD_CANAL,
                          CUEFEC.DES_CANAL,
                          CUEFEC.COD_SUBCANAL,
                          CUEFEC.DES_SUBCANAL
            FROM SAPR_TEFECTIVOXCONTENEDOR EXC
                INNER JOIN SAPR_TCONTENEDOR C
                    ON C.OID_CONTENEDOR = EXC.OID_CONTENEDOR
                    INNER JOIN SAPR_VCUENTA CUEFEC
                        ON EXC.OID_CUENTA_MOVIMIENTO = CUEFEC.OID_CUENTA
                        INNER JOIN GEPR_TDIVISA DIV
                            ON DIV.OID_DIVISA = EXC.OID_DIVISA
                            INNER JOIN GEPR_TDENOMINACION DEN
                                ON DEN.OID_DENOMINACION = EXC.OID_DENOMINACION
                                INNER JOIN GEPR_TCALIDAD CAL
                                    ON CAL.OID_CALIDAD = EXC.OID_CALIDAD
                                    INNER JOIN contenedores C1
                                        ON C1.OID = C.OID_CONTENEDOR
                                        OR C1.OID = C.OID_CONTENEDOR_MAYOR_NIVEL
            WHERE EXC.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND EXC.OID_CUENTA_MOVIMIENTO IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR NOT par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     DEN.COD_DENOMINACION,
                     CAL.COD_CALIDAD,
                     EXC.BOL_DISPONIBLE,
                     EXC.BOL_BLOQUEADO,
                     CUEFEC.COD_CLIENTE,
                    CUEFEC.DES_CLIENTE,
                    CUEFEC.COD_SUBCLIENTE,
                    CUEFEC.DES_SUBCLIENTE,
                    CUEFEC.COD_PTO_SERVICIO,
                    CUEFEC.DES_PTO_SERVICIO,
                    CUEFEC.COD_CANAL,
                    CUEFEC.DES_CANAL,
                    CUEFEC.COD_SUBCANAL,
                    CUEFEC.DES_SUBCANAL
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               NULL COD_DENOMINACION,
               NULL COD_CALIDAD,
               MP.COD_TIPO_MEDIO_PAGO,
               MP.COD_MEDIO_PAGO,
               MP.DES_MEDIO_PAGO,
               MPXC.BOL_DISPONIBLE,
               MPXC.BOL_BLOQUEADO,
               SUM(MPXC.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(MPXC.NUM_IMPORTE) NUM_IMPORTE,
               CUMP.COD_CLIENTE,
               CUMP.DES_CLIENTE,
               CUMP.COD_SUBCLIENTE,
               CUMP.DES_SUBCLIENTE,
               CUMP.COD_PTO_SERVICIO,
               CUMP.DES_PTO_SERVICIO,
               CUMP.COD_CANAL,
               CUMP.DES_CANAL,
               CUMP.COD_SUBCANAL,
               CUMP.DES_SUBCANAL
            FROM SAPR_TMEDIO_PAGOXCONTENEDOR MPXC
                INNER JOIN SAPR_TCONTENEDOR C
                    ON C.OID_CONTENEDOR = MPXC.OID_CONTENEDOR
                    INNER JOIN SAPR_VCUENTA CUMP
                        ON MPXC.OID_CUENTA_MOVIMIENTO = CUMP.OID_CUENTA
                        INNER JOIN GEPR_TDIVISA DIV
                            ON DIV.OID_DIVISA = MPXC.OID_DIVISA
                            INNER JOIN GEPR_TMEDIO_PAGO MP
                                ON MP.OID_MEDIO_PAGO = MPXC.OID_MEDIO_PAGO
                                INNER JOIN contenedores C1
                                    ON C1.OID = C.OID_CONTENEDOR
                                    OR C1.OID = C.OID_CONTENEDOR_MAYOR_NIVEL
            WHERE MPXC.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND MPXC.OID_CUENTA_MOVIMIENTO IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     MP.COD_TIPO_MEDIO_PAGO,
                     MP.COD_MEDIO_PAGO,
                     MP.DES_MEDIO_PAGO,
                     MPXC.BOL_DISPONIBLE,
                     MPXC.BOL_BLOQUEADO,
           CUMP.COD_CLIENTE,
           CUMP.DES_CLIENTE,
           CUMP.COD_SUBCLIENTE,
           CUMP.DES_SUBCLIENTE,
           CUMP.COD_PTO_SERVICIO,
           CUMP.DES_PTO_SERVICIO,
           CUMP.COD_CANAL,
           CUMP.DES_CANAL,
           CUMP.COD_SUBCANAL,
           CUMP.DES_SUBCANAL
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               DEN.COD_DENOMINACION,
               NULL COD_CALIDAD,
               NULL COD_TIPO_MEDIO_PAGO,
               NULL COD_MEDIO_PAGO,
               NULL DES_MEDIO_PAGO,
               0 BOL_DISPONIBLE,
               0 BOL_BLOQUEADO,
               SUM(DECEFEC.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(DECEFEC.NUM_IMPORTE) NUM_IMPORTE,
               CU.COD_CLIENTE,
               CU.DES_CLIENTE,
               CU.COD_SUBCLIENTE,
               CU.DES_SUBCLIENTE,
               CU.COD_PTO_SERVICIO,
               CU.DES_PTO_SERVICIO,
               CU.COD_CANAL,
               CU.DES_CANAL,
               CU.COD_SUBCANAL,
               CU.DES_SUBCANAL
            FROM SAPR_TDECLARADO_EFECTIVO DECEFEC
                INNER JOIN SAPR_TREMESA R
                    ON R.OID_REMESA = DECEFEC.OID_REMESA
                    AND DECEFEC.OID_BULTO IS NULL
                    INNER JOIN SAPR_VCUENTA CU
                        ON R.OID_CUENTA = CU.OID_CUENTA
                        INNER JOIN SAPR_TCONTENEDOR C
                            ON C.OID_CONTENEDOR = R.OID_CONTENEDOR_PADRE
                            INNER JOIN GEPR_TDIVISA DIV
                                ON DIV.OID_DIVISA = DECEFEC.OID_DIVISA
                                INNER JOIN GEPR_TDENOMINACION DEN
                                    ON DEN.OID_DENOMINACION = DECEFEC.OID_DENOMINACION
                                    INNER JOIN contenedores C1
                                        ON C1.OID = R.OID_CONTENEDOR_MAYOR_NIVEL
            WHERE DECEFEC.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND R.OID_CUENTA IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     DEN.COD_DENOMINACION,
           CU.COD_CLIENTE,
           CU.DES_CLIENTE,
           CU.COD_SUBCLIENTE,
           CU.DES_SUBCLIENTE,
           CU.COD_PTO_SERVICIO,
           CU.DES_PTO_SERVICIO,
           CU.COD_CANAL,
           CU.DES_CANAL,
           CU.COD_SUBCANAL,
           CU.DES_SUBCANAL
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               DEN.COD_DENOMINACION,
               NULL COD_CALIDAD,
               NULL COD_TIPO_MEDIO_PAGO,
               NULL COD_MEDIO_PAGO,
               NULL DES_MEDIO_PAGO,
               0 BOL_DISPONIBLE,
               0 BOL_BLOQUEADO,
               SUM(DECEFEC.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(DECEFEC.NUM_IMPORTE) NUM_IMPORTE,
               CU.COD_CLIENTE,
               CU.DES_CLIENTE,
               CU.COD_SUBCLIENTE,
               CU.DES_SUBCLIENTE,
               CU.COD_PTO_SERVICIO,
               CU.DES_PTO_SERVICIO,
               CU.COD_CANAL,
               CU.DES_CANAL,
               CU.COD_SUBCANAL,
               CU.DES_SUBCANAL
            FROM SAPR_TDECLARADO_EFECTIVO DECEFEC
                INNER JOIN SAPR_TBULTO B
                    ON B.OID_REMESA = DECEFEC.OID_REMESA
                    AND B.OID_BULTO = DECEFEC.OID_BULTO
                    INNER JOIN SAPR_VCUENTA CU
                        ON B.OID_CUENTA = CU.OID_CUENTA
                        INNER JOIN SAPR_TCONTENEDOR C
                            ON C.OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
                            INNER JOIN GEPR_TDIVISA DIV
                                ON DIV.OID_DIVISA = DECEFEC.OID_DIVISA
                                INNER JOIN GEPR_TDENOMINACION DEN
                                    ON DEN.OID_DENOMINACION = DECEFEC.OID_DENOMINACION
                                    INNER JOIN contenedores C1
                                        ON C1.OID = B.OID_CONTENEDOR_MAYOR_NIVEL
            WHERE DECEFEC.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND B.OID_CUENTA IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     DEN.COD_DENOMINACION,
           CU.COD_CLIENTE,
           CU.DES_CLIENTE,
           CU.COD_SUBCLIENTE,
           CU.DES_SUBCLIENTE,
           CU.COD_PTO_SERVICIO,
           CU.DES_PTO_SERVICIO,
           CU.COD_CANAL,
           CU.DES_CANAL,
           CU.COD_SUBCANAL,
           CU.DES_SUBCANAL
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               NULL COD_DENOMINACION,
               NULL COD_CALIDAD,
               MP.COD_TIPO_MEDIO_PAGO,
               MP.COD_MEDIO_PAGO,
               MP.DES_MEDIO_PAGO,
               0 BOL_DISPONIBLE,
               0 BOL_BLOQUEADO,
               SUM(DECMP.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(DECMP.NUM_IMPORTE) NUM_IMPORTE,
               CU.COD_CLIENTE,
               CU.DES_CLIENTE,
               CU.COD_SUBCLIENTE,
               CU.DES_SUBCLIENTE,
               CU.COD_PTO_SERVICIO,
               CU.DES_PTO_SERVICIO,
               CU.COD_CANAL,
               CU.DES_CANAL,
               CU.COD_SUBCANAL,
               CU.DES_SUBCANAL
            FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
                INNER JOIN SAPR_TREMESA R
                    ON R.OID_REMESA = DECMP.OID_REMESA
                    AND DECMP.OID_BULTO IS NULL
                    INNER JOIN SAPR_VCUENTA CU
                        ON R.OID_CUENTA = CU.OID_CUENTA
                        INNER JOIN SAPR_TCONTENEDOR C
                            ON C.OID_CONTENEDOR = R.OID_CONTENEDOR_PADRE
                            INNER JOIN GEPR_TDIVISA DIV
                                ON DIV.OID_DIVISA = DECMP.OID_DIVISA
                                INNER JOIN GEPR_TMEDIO_PAGO MP
                                    ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
                                    INNER JOIN contenedores C1
                                        ON C1.OID = R.OID_CONTENEDOR_MAYOR_NIVEL
            WHERE DECMP.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND R.OID_CUENTA IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     MP.COD_TIPO_MEDIO_PAGO,
                     MP.COD_MEDIO_PAGO,
                     MP.DES_MEDIO_PAGO,
             CU.COD_CLIENTE,
             CU.DES_CLIENTE,
             CU.COD_SUBCLIENTE,
             CU.DES_SUBCLIENTE,
             CU.COD_PTO_SERVICIO,
             CU.DES_PTO_SERVICIO,
             CU.COD_CANAL,
             CU.DES_CANAL,
             CU.COD_SUBCANAL,
             CU.DES_SUBCANAL
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               NULL COD_DENOMINACION,
               NULL COD_CALIDAD,
               MP.COD_TIPO_MEDIO_PAGO,
               MP.COD_MEDIO_PAGO,
               MP.DES_MEDIO_PAGO,
               0 BOL_DISPONIBLE,
               0 BOL_BLOQUEADO,
               SUM(DECMP.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(DECMP.NUM_IMPORTE) NUM_IMPORTE,
        CU.COD_CLIENTE,
        CU.DES_CLIENTE,
        CU.COD_SUBCLIENTE,
        CU.DES_SUBCLIENTE,
        CU.COD_PTO_SERVICIO,
        CU.DES_PTO_SERVICIO,
        CU.COD_CANAL,
        CU.DES_CANAL,
        CU.COD_SUBCANAL,
        CU.DES_SUBCANAL
            FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
                INNER JOIN SAPR_TBULTO B
                    ON B.OID_REMESA = DECMP.OID_REMESA
                    AND B.OID_BULTO = DECMP.OID_BULTO
                    INNER JOIN SAPR_VCUENTA CU
                        ON B.OID_CUENTA = CU.OID_CUENTA
                        INNER JOIN SAPR_TCONTENEDOR C
                            ON C.OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
                            INNER JOIN GEPR_TDIVISA DIV
                                ON DIV.OID_DIVISA = DECMP.OID_DIVISA
                                INNER JOIN GEPR_TMEDIO_PAGO MP
                                    ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
                                    INNER JOIN contenedores C1
                                        ON C1.OID = B.OID_CONTENEDOR_MAYOR_NIVEL
            WHERE DECMP.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND B.OID_CUENTA IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     MP.COD_TIPO_MEDIO_PAGO,
                     MP.COD_MEDIO_PAGO,
                     MP.DES_MEDIO_PAGO,
          CU.COD_CLIENTE,
          CU.DES_CLIENTE,
          CU.COD_SUBCLIENTE,
          CU.DES_SUBCLIENTE,
          CU.COD_PTO_SERVICIO,
          CU.DES_PTO_SERVICIO,
          CU.COD_CANAL,
          CU.DES_CANAL,
          CU.COD_SUBCANAL,
          CU.DES_SUBCANAL)
SELECT E.OID,
       E.COD_ISO_DIVISA,
       E.COD_DENOMINACION,
       E.COD_CALIDAD,
       E.COD_TIPO_MEDIO_PAGO,
       E.COD_MEDIO_PAGO,
       E.DES_MEDIO_PAGO,
       E.BOL_DISPONIBLE,
       E.BOL_BLOQUEADO,
       E.NEL_CANTIDAD,
       E.NUM_IMPORTE,
    E.COD_CLIENTE,
    E.DES_CLIENTE,
    E.COD_SUBCLIENTE,
    E.DES_SUBCLIENTE,
    E.COD_PTO_SERVICIO,
    E.DES_PTO_SERVICIO,
    E.COD_CANAL,
    E.DES_CANAL,
    E.COD_SUBCANAL,
    E.DES_SUBCANAL
    FROM efectivoMP E;
  ELSE


        INSERT INTO SAPR_GTT_TEFECMPXCONTENEDOR
                (OID_CONTENEDOR,
                  COD_ISO_DIVISA,
                  COD_DENOMINACION,
                  COD_CALIDAD,
                  COD_TIPO_MEDIO_PAGO,
                  COD_MEDIO_PAGO,
                  DES_MEDIO_PAGO,
                  BOL_DISPONIBLE,
                  BOL_BLOQUEADO,
                  NEL_CANTIDAD,
                  NUM_IMPORTE,
           COD_CLIENTE,
           DES_CLIENTE,
           COD_SUBCLIENTE,
           DES_SUBCLIENTE,
           COD_PTO_SERVICIO,
           DES_PTO_SERVICIO,
           COD_CANAL,
           DES_CANAL,
           COD_SUBCANAL,
           DES_SUBCANAL
                )
WITH contenedores AS (SELECT OID
            FROM SAPR_GTT_TOIDS
            WHERE COD_CALIFICADOR = 'CONTENEDORESFILHO'),
    efectivoMP AS (SELECT C1.OID,
                          DIV.COD_ISO_DIVISA,
                          DEN.COD_DENOMINACION,
                          CAL.COD_CALIDAD,
                          NULL COD_TIPO_MEDIO_PAGO,
                          NULL COD_MEDIO_PAGO,
                          NULL DES_MEDIO_PAGO,
                          EXC.BOL_DISPONIBLE,
                          EXC.BOL_BLOQUEADO,
                          SUM(EXC.NEL_CANTIDAD) NEL_CANTIDAD,
                          SUM(EXC.NUM_IMPORTE) NUM_IMPORTE,
              CUEFEC.COD_CLIENTE,
              CUEFEC.DES_CLIENTE,
              CUEFEC.COD_SUBCLIENTE,
              CUEFEC.DES_SUBCLIENTE,
              CUEFEC.COD_PTO_SERVICIO,
              CUEFEC.DES_PTO_SERVICIO,
              CUEFEC.COD_CANAL,
              CUEFEC.DES_CANAL,
              CUEFEC.COD_SUBCANAL,
              CUEFEC.DES_SUBCANAL
            FROM SAPR_TEFECTIVOXCONTENEDOR EXC
                INNER JOIN SAPR_TCONTENEDOR C
                    ON C.OID_CONTENEDOR = EXC.OID_CONTENEDOR
                    INNER JOIN SAPR_VCUENTA CUEFEC
                        ON EXC.OID_CUENTA_MOVIMIENTO = CUEFEC.OID_CUENTA
                        INNER JOIN GEPR_TDIVISA DIV
                            ON DIV.OID_DIVISA = EXC.OID_DIVISA
                            INNER JOIN GEPR_TDENOMINACION DEN
                                ON DEN.OID_DENOMINACION = EXC.OID_DENOMINACION
                                INNER JOIN GEPR_TCALIDAD CAL
                                    ON CAL.OID_CALIDAD = EXC.OID_CALIDAD
                                    INNER JOIN contenedores C1
                                        ON C1.OID = C.OID_CONTENEDOR
            WHERE EXC.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND EXC.OID_CUENTA_MOVIMIENTO IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     DEN.COD_DENOMINACION,
                     CAL.COD_CALIDAD,
                     EXC.BOL_DISPONIBLE,
                     EXC.BOL_BLOQUEADO,
          CUEFEC.COD_CLIENTE,
          CUEFEC.DES_CLIENTE,
          CUEFEC.COD_SUBCLIENTE,
          CUEFEC.DES_SUBCLIENTE,
          CUEFEC.COD_PTO_SERVICIO,
          CUEFEC.DES_PTO_SERVICIO,
          CUEFEC.COD_CANAL,
          CUEFEC.DES_CANAL,
          CUEFEC.COD_SUBCANAL,
          CUEFEC.DES_SUBCANAL
        START WITH C.OID_CONTENEDOR = C1.OID
        CONNECT BY NOCYCLE PRIOR C.OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE

            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               NULL COD_DENOMINACION,
               NULL COD_CALIDAD,
               MP.COD_TIPO_MEDIO_PAGO,
               MP.COD_MEDIO_PAGO,
               MP.DES_MEDIO_PAGO,
               MPXC.BOL_DISPONIBLE,
               MPXC.BOL_BLOQUEADO,
               SUM(MPXC.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(MPXC.NUM_IMPORTE) NUM_IMPORTE,
        CUMP.COD_CLIENTE,
        CUMP.DES_CLIENTE,
        CUMP.COD_SUBCLIENTE,
        CUMP.DES_SUBCLIENTE,
        CUMP.COD_PTO_SERVICIO,
        CUMP.DES_PTO_SERVICIO,
        CUMP.COD_CANAL,
        CUMP.DES_CANAL,
        CUMP.COD_SUBCANAL,
        CUMP.DES_SUBCANAL
            FROM SAPR_TMEDIO_PAGOXCONTENEDOR MPXC
                INNER JOIN SAPR_TCONTENEDOR C
                    ON C.OID_CONTENEDOR = MPXC.OID_CONTENEDOR
                    INNER JOIN SAPR_VCUENTA CUMP
                        ON MPXC.OID_CUENTA_MOVIMIENTO = CUMP.OID_CUENTA
                        INNER JOIN GEPR_TDIVISA DIV
                            ON DIV.OID_DIVISA = MPXC.OID_DIVISA
                            INNER JOIN GEPR_TMEDIO_PAGO MP
                                ON MP.OID_MEDIO_PAGO = MPXC.OID_MEDIO_PAGO
                                INNER JOIN contenedores C1
                                    ON C1.OID = C.OID_CONTENEDOR
            WHERE MPXC.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND MPXC.OID_CUENTA_MOVIMIENTO IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     MP.COD_TIPO_MEDIO_PAGO,
                     MP.COD_MEDIO_PAGO,
                     MP.DES_MEDIO_PAGO,
                     MPXC.BOL_DISPONIBLE,
                     MPXC.BOL_BLOQUEADO,
          CUMP.COD_CLIENTE,
          CUMP.DES_CLIENTE,
          CUMP.COD_SUBCLIENTE,
          CUMP.DES_SUBCLIENTE,
          CUMP.COD_PTO_SERVICIO,
          CUMP.DES_PTO_SERVICIO,
          CUMP.COD_CANAL,
          CUMP.DES_CANAL,
          CUMP.COD_SUBCANAL,
          CUMP.DES_SUBCANAL
        START WITH C.OID_CONTENEDOR = C1.OID
        CONNECT BY NOCYCLE PRIOR C.OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               DEN.COD_DENOMINACION,
               NULL COD_CALIDAD,
               NULL COD_TIPO_MEDIO_PAGO,
               NULL COD_MEDIO_PAGO,
               NULL DES_MEDIO_PAGO,
               0 BOL_DISPONIBLE,
               0 BOL_BLOQUEADO,
               SUM(DECEFEC.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(DECEFEC.NUM_IMPORTE) NUM_IMPORTE,
        CU.COD_CLIENTE,
        CU.DES_CLIENTE,
        CU.COD_SUBCLIENTE,
        CU.DES_SUBCLIENTE,
        CU.COD_PTO_SERVICIO,
        CU.DES_PTO_SERVICIO,
        CU.COD_CANAL,
        CU.DES_CANAL,
        CU.COD_SUBCANAL,
        CU.DES_SUBCANAL
            FROM SAPR_TDECLARADO_EFECTIVO DECEFEC
                INNER JOIN SAPR_TREMESA R
                    ON R.OID_REMESA = DECEFEC.OID_REMESA
                    AND DECEFEC.OID_BULTO IS NULL
                    INNER JOIN SAPR_VCUENTA CU
                        ON R.OID_CUENTA = CU.OID_CUENTA
                        INNER JOIN SAPR_TCONTENEDOR C
                            ON C.OID_CONTENEDOR = R.OID_CONTENEDOR_PADRE
                            INNER JOIN GEPR_TDIVISA DIV
                                ON DIV.OID_DIVISA = DECEFEC.OID_DIVISA
                                INNER JOIN GEPR_TDENOMINACION DEN
                                    ON DEN.OID_DENOMINACION = DECEFEC.OID_DENOMINACION
                                    INNER JOIN contenedores C1
                                        ON C1.OID = C.OID_CONTENEDOR
            WHERE DECEFEC.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND R.OID_CUENTA IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     DEN.COD_DENOMINACION,
          CU.COD_CLIENTE,
          CU.DES_CLIENTE,
          CU.COD_SUBCLIENTE,
          CU.DES_SUBCLIENTE,
          CU.COD_PTO_SERVICIO,
          CU.DES_PTO_SERVICIO,
          CU.COD_CANAL,
          CU.DES_CANAL,
          CU.COD_SUBCANAL,
          CU.DES_SUBCANAL
        START WITH C.OID_CONTENEDOR = C1.OID
        CONNECT BY NOCYCLE PRIOR C.OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               DEN.COD_DENOMINACION,
               NULL COD_CALIDAD,
               NULL COD_TIPO_MEDIO_PAGO,
               NULL COD_MEDIO_PAGO,
               NULL DES_MEDIO_PAGO,
               0 BOL_DISPONIBLE,
               0 BOL_BLOQUEADO,
               SUM(DECEFEC.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(DECEFEC.NUM_IMPORTE) NUM_IMPORTE,
        CU.COD_CLIENTE,
        CU.DES_CLIENTE,
        CU.COD_SUBCLIENTE,
        CU.DES_SUBCLIENTE,
        CU.COD_PTO_SERVICIO,
        CU.DES_PTO_SERVICIO,
        CU.COD_CANAL,
        CU.DES_CANAL,
        CU.COD_SUBCANAL,
        CU.DES_SUBCANAL
            FROM SAPR_TDECLARADO_EFECTIVO DECEFEC
                INNER JOIN SAPR_TBULTO B
                    ON B.OID_REMESA = DECEFEC.OID_REMESA
                    AND B.OID_BULTO = DECEFEC.OID_BULTO
                    INNER JOIN SAPR_VCUENTA CU
                        ON B.OID_CUENTA = CU.OID_CUENTA
                        INNER JOIN SAPR_TCONTENEDOR C
                            ON C.OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
                            INNER JOIN GEPR_TDIVISA DIV
                                ON DIV.OID_DIVISA = DECEFEC.OID_DIVISA
                                INNER JOIN GEPR_TDENOMINACION DEN
                                    ON DEN.OID_DENOMINACION = DECEFEC.OID_DENOMINACION
                                    INNER JOIN contenedores C1
                                        ON C1.OID = C.OID_CONTENEDOR
            WHERE DECEFEC.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND B.OID_CUENTA IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     DEN.COD_DENOMINACION,
          CU.COD_CLIENTE,
          CU.DES_CLIENTE,
          CU.COD_SUBCLIENTE,
          CU.DES_SUBCLIENTE,
          CU.COD_PTO_SERVICIO,
          CU.DES_PTO_SERVICIO,
          CU.COD_CANAL,
          CU.DES_CANAL,
          CU.COD_SUBCANAL,
          CU.DES_SUBCANAL
        START WITH C.OID_CONTENEDOR = C1.OID
        CONNECT BY NOCYCLE PRIOR C.OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               NULL COD_DENOMINACION,
               NULL COD_CALIDAD,
               MP.COD_TIPO_MEDIO_PAGO,
               MP.COD_MEDIO_PAGO,
               MP.DES_MEDIO_PAGO,
               0 BOL_DISPONIBLE,
               0 BOL_BLOQUEADO,
               SUM(DECMP.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(DECMP.NUM_IMPORTE) NUM_IMPORTE,
        CU.COD_CLIENTE,
        CU.DES_CLIENTE,
        CU.COD_SUBCLIENTE,
        CU.DES_SUBCLIENTE,
        CU.COD_PTO_SERVICIO,
        CU.DES_PTO_SERVICIO,
        CU.COD_CANAL,
        CU.DES_CANAL,
        CU.COD_SUBCANAL,
        CU.DES_SUBCANAL
            FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
                INNER JOIN SAPR_TREMESA R
                    ON R.OID_REMESA = DECMP.OID_REMESA
                    AND DECMP.OID_BULTO IS NULL
                    INNER JOIN SAPR_VCUENTA CU
                        ON R.OID_CUENTA = CU.OID_CUENTA
                        INNER JOIN SAPR_TCONTENEDOR C
                            ON C.OID_CONTENEDOR = R.OID_CONTENEDOR_PADRE
                            INNER JOIN GEPR_TDIVISA DIV
                                ON DIV.OID_DIVISA = DECMP.OID_DIVISA
                                INNER JOIN GEPR_TMEDIO_PAGO MP
                                    ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
                                    INNER JOIN contenedores C1
                                        ON C1.OID = C.OID_CONTENEDOR
            WHERE DECMP.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND R.OID_CUENTA IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     MP.COD_TIPO_MEDIO_PAGO,
                     MP.COD_MEDIO_PAGO,
                     MP.DES_MEDIO_PAGO,
          CU.COD_CLIENTE,
          CU.DES_CLIENTE,
          CU.COD_SUBCLIENTE,
          CU.DES_SUBCLIENTE,
          CU.COD_PTO_SERVICIO,
          CU.DES_PTO_SERVICIO,
          CU.COD_CANAL,
          CU.DES_CANAL,
          CU.COD_SUBCANAL,
          CU.DES_SUBCANAL
        START WITH C.OID_CONTENEDOR = C1.OID
        CONNECT BY NOCYCLE PRIOR C.OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE
            UNION ALL
        SELECT C1.OID,
               DIV.COD_ISO_DIVISA,
               NULL COD_DENOMINACION,
               NULL COD_CALIDAD,
               MP.COD_TIPO_MEDIO_PAGO,
               MP.COD_MEDIO_PAGO,
               MP.DES_MEDIO_PAGO,
               0 BOL_DISPONIBLE,
               0 BOL_BLOQUEADO,
               SUM(DECMP.NEL_CANTIDAD) NEL_CANTIDAD,
               SUM(DECMP.NUM_IMPORTE) NUM_IMPORTE,
        CU.COD_CLIENTE,
        CU.DES_CLIENTE,
        CU.COD_SUBCLIENTE,
        CU.DES_SUBCLIENTE,
        CU.COD_PTO_SERVICIO,
        CU.DES_PTO_SERVICIO,
        CU.COD_CANAL,
        CU.DES_CANAL,
        CU.COD_SUBCANAL,
        CU.DES_SUBCANAL
            FROM SAPR_TDECLARADO_MEDIO_PAGO DECMP
                INNER JOIN SAPR_TBULTO B
                    ON B.OID_REMESA = DECMP.OID_REMESA
                    AND B.OID_BULTO = DECMP.OID_BULTO
                    INNER JOIN SAPR_VCUENTA CU
                        ON B.OID_CUENTA = CU.OID_CUENTA
                        INNER JOIN SAPR_TCONTENEDOR C
                            ON C.OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
                            INNER JOIN GEPR_TDIVISA DIV
                                ON DIV.OID_DIVISA = DECMP.OID_DIVISA
                                INNER JOIN GEPR_TMEDIO_PAGO MP
                                    ON MP.OID_MEDIO_PAGO = DECMP.OID_MEDIO_PAGO
                                    INNER JOIN contenedores C1
                                        ON C1.OID = C.OID_CONTENEDOR
            WHERE DECMP.COD_NIVEL_DETALLE = 'D'
                AND (par$existeCuenta = 1
                AND B.OID_CUENTA IN (SELECT CLAI.OID
                        FROM SAPR_GTT_TOIDS CLAI
                        WHERE CLAI.COD_CALIFICADOR = 'CUENTAS')
                OR par$existeCuenta = 0)
            GROUP BY C1.OID,
                     DIV.COD_ISO_DIVISA,
                     MP.COD_TIPO_MEDIO_PAGO,
                     MP.COD_MEDIO_PAGO,
                     MP.DES_MEDIO_PAGO,
          CU.COD_CLIENTE,
          CU.DES_CLIENTE,
          CU.COD_SUBCLIENTE,
          CU.DES_SUBCLIENTE,
          CU.COD_PTO_SERVICIO,
          CU.DES_PTO_SERVICIO,
          CU.COD_CANAL,
          CU.DES_CANAL,
          CU.COD_SUBCANAL,
          CU.DES_SUBCANAL
        START WITH C.OID_CONTENEDOR = C1.OID
        CONNECT BY NOCYCLE PRIOR C.OID_CONTENEDOR = C.OID_CONTENEDOR_PADRE)
SELECT E.OID,
       E.COD_ISO_DIVISA,
       E.COD_DENOMINACION,
       E.COD_CALIDAD,
       E.COD_TIPO_MEDIO_PAGO,
       E.COD_MEDIO_PAGO,
       E.DES_MEDIO_PAGO,
       E.BOL_DISPONIBLE,
       E.BOL_BLOQUEADO,
       E.NEL_CANTIDAD,
       E.NUM_IMPORTE,
    E.COD_CLIENTE,
    E.DES_CLIENTE,
    E.COD_SUBCLIENTE,
    E.DES_SUBCLIENTE,
    E.COD_PTO_SERVICIO,
    E.DES_PTO_SERVICIO,
    E.COD_CANAL,
    E.DES_CANAL,
    E.COD_SUBCANAL,
    E.DES_SUBCANAL
    FROM efectivoMP E;

END IF;

END SP_INSVALORESCONTENEDOR;

PROCEDURE SP_CONSULTARCONTPACKMODULAR(par$codIdioma        IN VARCHAR2,
                                        par$vencidos         IN INTEGER,
                                        par$fechaVencimiento IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$codTipoSector    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$codDelegacion    IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$codPlanta        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$codSector        IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$codCliente       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codSubCliente    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codPtoServicio   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codCanal         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$codSubcanal      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$rcContenedores   OUT SYS_REFCURSOR,
                                        par$rcAlertas        OUT SYS_REFCURSOR,
                                        par$rcClientes       OUT SYS_REFCURSOR,
                                        par$rcPrecintos      OUT SYS_REFCURSOR,
                                        par$rcDetalleEfec    OUT SYS_REFCURSOR,
                                        par$cod_usuario      in gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$info_ejecucion   IN VARCHAR2,
                                        par$cod_ejecucion    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$inserts          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$deletes          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects          OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$existetipoSector INTEGER;
    var$existeDelegacion INTEGER;
    var$existePlanta     INTEGER;
    var$existeSector     INTEGER;
    var$existeCanal      INTEGER;
    var$existeCuenta     INTEGER;

    var$inicio TIMESTAMP;
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SP_CONSULTARCONTPACKMODULAR';
    var$err VARCHAR2(4000);

    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;

  begin

    var$inicio  := SYSTIMESTAMP;
    par$selects := 0;
    par$deletes := 0;
    par$inserts := 0;

    IF par$vencidos IS NULL AND par$fechaVencimiento IS NULL THEN
      RAISE_APPLICATION_ERROR(-20001,
                              GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                  'MSG_VALIDAR_FYH_VENC',
                                                                  'SP_CONSULTARCONTPACKMODULAR',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  1));
    END IF;

    IF par$codCliente IS NULL AND
       (par$codSubCliente IS NOT NULL OR par$codPtoServicio IS NOT NULL) THEN
      RAISE_APPLICATION_ERROR(-20002,
                              GEPR_PUTILIDADES_###VERSION###.ftraduzir(par$codIdioma,
                                                                  'MSG_VALIDAR_CLIENTE',
                                                                  'SP_CONSULTARCONTPACKMODULAR',
                                                                  'GenesisSaldos',
                                                                  NULL,
                                                                  1));
    END IF;

    DELETE FROM SAPR_GTT_TOIDS;
    par$deletes := par$deletes + sql%rowcount;

    var$existetipoSector := 0;
    --insere par$codTipoSector
    if par$codTipoSector is not null AND par$codTipoSector.COUNT > 0 Then
      for idx in par$codTipoSector.FIRST .. par$codTipoSector.LAST LOOP
        if par$codTipoSector(idx) is not null Then
          var$existetipoSector := 1;
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'TIPOSECTOR', TS.OID_TIPO_SECTOR
              FROM GEPR_TTIPO_SECTOR TS
             INNER JOIN GEPR_TCARACTTIPOSECTORXTIPOSEC CTSXTS ON CTSXTS.OID_TIPO_SECTOR = TS.OID_TIPO_SECTOR
              INNER JOIN GEPR_TCARACT_TIPOSECTOR CTS ON CTSXTS.OID_CARACT_TIPOSECTOR = CTS.OID_CARACT_TIPOSECTOR
             WHERE CTS.COD_CARACT_TIPOSECTOR = par$codTipoSector(idx)
               AND TS.OID_TIPO_SECTOR NOT IN
                   (SELECT OID
                      FROM SAPR_GTT_TOIDS gti
                     WHERE gti.COD_CALIFICADOR = 'TIPOSECTOR');

          par$inserts := par$inserts + sql%rowcount;
          par$selects := par$selects + sql%rowcount;

        end if;
      END LOOP;
    end if;

    var$existeDelegacion := 0;
    --insere codDelegacion
    if par$codDelegacion is not null AND par$codDelegacion.COUNT > 0 Then
      for idx in par$codDelegacion.FIRST .. par$codDelegacion.LAST LOOP
        if par$codDelegacion(idx) is not null Then
          var$existeDelegacion := 1;
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'DELEGACIONES', D.OID_DELEGACION
              FROM GEPR_TDELEGACION D
             WHERE D.COD_DELEGACION = par$codDelegacion(idx)
               AND D.OID_DELEGACION NOT IN
                   (SELECT OID
                      FROM SAPR_GTT_TOIDS gti
                     WHERE gti.COD_CALIFICADOR = 'DELEGACIONES');

          par$inserts := par$inserts + sql%rowcount;
          par$selects := par$selects + sql%rowcount;

        end if;
      END LOOP;
    end if;

    var$existePlanta := 0;
    --insere codPlanta
    if par$codPlanta is not null AND par$codPlanta.COUNT > 0 Then
      for idx in par$codPlanta.FIRST .. par$codPlanta.LAST LOOP
        if par$codPlanta(idx) is not null Then
          var$existePlanta := 1;
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'PLANTAS', P.OID_PLANTA
              FROM GEPR_TPLANTA P
             WHERE P.COD_PLANTA = par$codPlanta(idx)
               AND P.OID_PLANTA NOT IN
                   (SELECT OID
                      FROM SAPR_GTT_TOIDS gti
                     WHERE gti.COD_CALIFICADOR = 'PLANTAS')
               AND (var$existeDelegacion = 1 AND
                   P.OID_DELEGACION IN
                   (SELECT OID
                       FROM SAPR_GTT_TOIDS
                      WHERE COD_CALIFICADOR = 'DELEGACIONES') OR
                   var$existeDelegacion = 0);

          par$inserts := par$inserts + sql%rowcount;
          par$selects := par$selects + sql%rowcount;

        end if;
      END LOOP;
    end if;

    var$existeSector := 0;
    --insere codSector
    if par$codSector is not null AND par$codSector.COUNT > 0 THEN
      for idx in par$codSector.FIRST .. par$codSector.LAST LOOP
        if par$codSector(idx) is not null Then
          var$existeSector := 1;
          INSERT INTO SAPR_GTT_TOIDS
            (COD_CALIFICADOR, OID)
            SELECT 'SECTORES', S.OID_SECTOR
              FROM GEPR_TSECTOR S
             INNER JOIN GEPR_TPLANTA P
                ON P.OID_PLANTA = S.OID_PLANTA
             WHERE S.COD_SECTOR = par$codSector(idx)
               AND S.OID_SECTOR NOT IN
                   (SELECT OID
                      FROM SAPR_GTT_TOIDS gti
                     WHERE gti.COD_CALIFICADOR = 'SECTORES')
               AND (var$existePlanta = 1 AND
                   S.OID_PLANTA IN
                   (SELECT OID
                       FROM SAPR_GTT_TOIDS
                      WHERE COD_CALIFICADOR = 'PLANTAS') OR
                   var$existePlanta = 0)
               AND (var$existeDelegacion = 1 AND
                   P.OID_DELEGACION IN
                   (SELECT OID
                       FROM SAPR_GTT_TOIDS
                      WHERE COD_CALIFICADOR = 'DELEGACIONES'));

          par$inserts := par$inserts + sql%rowcount;
          par$selects := par$selects + sql%rowcount;

        end if;
      END LOOP;
    end if;

    var$existeCuenta := 0;
    IF var$existeSector = 1 OR var$existetipoSector = 1 OR
       par$codCliente IS NOT NULL OR par$codCanal IS NOT NULL OR
       par$codSubcanal IS NOT NULL THEN
      var$existeCuenta := 1;
      --insere clientes
      INSERT INTO SAPR_GTT_TOIDS
        (COD_CALIFICADOR, OID)
        SELECT 'CUENTAS', CU.OID_CUENTA
          FROM SAPR_TCONTENEDOR C
         INNER JOIN SAPR_VCUENTA CU
            ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
         INNER JOIN GEPR_TSECTOR SEC
            ON SEC.OID_SECTOR = CU.OID_SECTOR
         WHERE (par$codCliente IS NOT NULL AND
               CU.COD_CLIENTE = par$codCliente OR par$codCliente IS NULL)
           AND (par$codSubCliente IS NOT NULL AND
               CU.COD_SUBCLIENTE = par$codSubCliente OR
               par$codSubCliente IS NULL OR CU.COD_SUBCLIENTE IS NULL)
           AND (par$codPtoServicio IS NOT NULL AND
               CU.COD_PTO_SERVICIO = par$codPtoServicio OR
               par$codPtoServicio IS NULL OR CU.COD_PTO_SERVICIO IS NULL)
           AND (var$existeSector = 1 AND
               CU.OID_SECTOR IN
               (SELECT OID
                   FROM SAPR_GTT_TOIDS
                  WHERE COD_CALIFICADOR = 'SECTORES') OR
               var$existeSector = 0)
           AND (par$codCanal IS NOT NULL AND CU.COD_CANAL = par$codCanal OR
               par$codCanal IS NULL)
           AND (par$codSubcanal IS NOT NULL AND
               CU.COD_SUBCANAL = par$codSubcanal OR
               par$codSubcanal IS NULL)
           AND (var$existetipoSector = 1 AND
               SEC.OID_TIPO_SECTOR IN
               (SELECT OID
                   FROM SAPR_GTT_TOIDS
                  WHERE COD_CALIFICADOR = 'TIPOSECTOR') OR
               var$existetipoSector = 0)
        UNION

        SELECT 'CUENTAS', CU.OID_CUENTA
          FROM SAPR_TEFECTIVOXCONTENEDOR C
         INNER JOIN SAPR_VCUENTA CU
            ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
         INNER JOIN GEPR_TSECTOR SEC
            ON SEC.OID_SECTOR = CU.OID_SECTOR
         WHERE (par$codCliente IS NOT NULL AND
               CU.COD_CLIENTE = par$codCliente OR par$codCliente IS NULL)
           AND (par$codSubCliente IS NOT NULL AND
               CU.COD_SUBCLIENTE = par$codSubCliente OR
               par$codSubCliente IS NULL OR CU.COD_SUBCLIENTE IS NULL)
           AND (par$codPtoServicio IS NOT NULL AND
               CU.COD_PTO_SERVICIO = par$codPtoServicio OR
               par$codPtoServicio IS NULL OR CU.COD_PTO_SERVICIO IS NULL)
           AND (var$existeSector = 1 AND
               CU.OID_SECTOR IN
               (SELECT OID
                   FROM SAPR_GTT_TOIDS
                  WHERE COD_CALIFICADOR = 'SECTORES') OR
               var$existeSector = 0)
           AND (par$codCanal IS NOT NULL AND CU.COD_CANAL = par$codCanal OR
               par$codCanal IS NULL)
           AND (par$codSubcanal IS NOT NULL AND
               CU.COD_SUBCANAL = par$codSubcanal OR
               par$codSubcanal IS NULL)
           AND (var$existetipoSector = 1 AND
               SEC.OID_TIPO_SECTOR IN
               (SELECT OID
                   FROM SAPR_GTT_TOIDS
                  WHERE COD_CALIFICADOR = 'TIPOSECTOR') OR
               var$existetipoSector = 0)
        UNION

        SELECT 'CUENTAS', CU.OID_CUENTA
          FROM SAPR_TMEDIO_PAGOXCONTENEDOR C
         INNER JOIN SAPR_VCUENTA CU
            ON CU.OID_CUENTA = C.OID_CUENTA_MOVIMIENTO
         INNER JOIN GEPR_TSECTOR SEC
            ON SEC.OID_SECTOR = CU.OID_SECTOR
         WHERE (par$codCliente IS NOT NULL AND
               CU.COD_CLIENTE = par$codCliente OR par$codCliente IS NULL)
           AND (par$codSubCliente IS NOT NULL AND
               CU.COD_SUBCLIENTE = par$codSubCliente OR
               par$codSubCliente IS NULL OR CU.COD_SUBCLIENTE IS NULL)
           AND (par$codPtoServicio IS NOT NULL AND
               CU.COD_PTO_SERVICIO = par$codPtoServicio OR
               par$codPtoServicio IS NULL OR CU.COD_PTO_SERVICIO IS NULL)
           AND (var$existetipoSector = 1 AND
               SEC.OID_TIPO_SECTOR IN
               (SELECT OID
                   FROM SAPR_GTT_TOIDS
                  WHERE COD_CALIFICADOR = 'TIPOSECTOR') OR
               var$existetipoSector = 0)
           AND (par$codCanal IS NOT NULL AND CU.COD_CANAL = par$codCanal OR
               par$codCanal IS NULL)
           AND (par$codSubcanal IS NOT NULL AND
               CU.COD_SUBCANAL = par$codSubcanal OR
               par$codSubcanal IS NULL);

      par$inserts := par$inserts + sql%rowcount;
      par$selects := par$selects + sql%rowcount;

    END IF;

    --insere oid contenedores
    INSERT INTO SAPR_GTT_TOIDS
      (COD_CALIFICADOR, OID)
      SELECT 'CONTENEDORES', C.OID_CONTENEDOR
        FROM SAPR_TCONTENEDOR C
       INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON C.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
        LEFT JOIN SAPR_TEFECTIVOXCONTENEDOR EXC
          ON EXC.OID_CONTENEDOR = C.OID_CONTENEDOR
        LEFT JOIN SAPR_TMEDIO_PAGOXCONTENEDOR MPXC
          ON MPXC.OID_CONTENEDOR = C.OID_CONTENEDOR
       WHERE C.BOL_PACK_MODULAR = 1
         AND C.COD_ESTADO = 'AR'
         AND (par$vencidos = 1 AND C.FEC_VENCIMIENTO <= SYSDATE() OR
             par$vencidos = 0 AND C.FEC_VENCIMIENTO > SYSDATE() OR
             par$vencidos IS NULL AND
             C.FEC_VENCIMIENTO = par$fechaVencimiento)
         AND ((var$existeCuenta = 1 AND
             C.OID_CUENTA_MOVIMIENTO IN
             (SELECT OID
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'CUENTAS') OR
             EXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT OID
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'CUENTAS') OR
             MPXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT OID
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'CUENTAS')) OR
             var$existeCuenta = 0);

    par$inserts := par$inserts + sql%rowcount;
    par$selects := par$selects + sql%rowcount;

    COMMIT;

    --busca dados de contenedores
    OPEN par$rcContenedores FOR
      SELECT C.OID_CONTENEDOR,
             TC.COD_TIPO_CONTENEDOR,
             TC.DES_TIPO_CONTENEDOR,
             C.GMT_CREACION,
             trunc(sysdate) - TRUNC(C.GMT_CREACION) AS DIAS_ARMADO,
             C.FEC_VENCIMIENTO,
             trunc(sysdate) - TRUNC(C.FEC_VENCIMIENTO) AS DIAS_VENCIDO
        FROM SAPR_TCONTENEDOR C
       INNER JOIN SAPR_TTIPO_CONTENEDOR TC
          ON C.OID_TIPO_CONTENEDOR = TC.OID_TIPO_CONTENEDOR
       WHERE C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES');

    par$selects := par$selects + sql%rowcount;

    --busca dados dos precintos
    OPEN par$rcPrecintos FOR
      SELECT PXC.OID_CONTENEDOR,
             PXC.OID_PRECINTOXCONTENEDOR,
             PXC.COD_PRECINTO
        FROM SAPR_TPRECINTOXCONTENEDOR PXC
       WHERE PXC.OID_CONTENEDOR IN
             (SELECT OID
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'CONTENEDORES');

    par$selects := par$selects + sql%rowcount;

    --busca dados dos clientes
    OPEN par$rcClientes FOR
      SELECT C.OID_CONTENEDOR,
             CUMOV.COD_CLIENTE,
             CUMOV.DES_CLIENTE,
             CUMOV.COD_SUBCLIENTE,
             CUMOV.COD_PTO_SERVICIO,
             CUMOV.COD_CANAL,
             CUMOV.DES_CANAL,
             CUMOV.COD_SUBCANAL,
             CUMOV.DES_SUBCANAL,
             CUMOV.COD_DELEGACION,
             CUMOV.COD_PLANTA,
             CUMOV.OID_SECTOR,
             CUMOV.COD_SECTOR,
             CUMOV.DES_SECTOR,
             POS.COD_POSICION
        FROM SAPR_TCONTENEDOR C
       INNER JOIN SAPR_VCUENTA CUMOV
          ON C.OID_CUENTA_MOVIMIENTO = CUMOV.OID_CUENTA
        LEFT JOIN SAPR_TPOSICION_SECTOR POS
          ON POS.OID_SECTOR = CUMOV.OID_SECTOR
       WHERE C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
         AND (var$existeCuenta = 1 AND
             C.OID_CUENTA_MOVIMIENTO IN
             (SELECT CLAI.OID
                 FROM SAPR_GTT_TOIDS CLAI
                WHERE CLAI.COD_CALIFICADOR = 'CUENTAS') OR
             var$existeCuenta = 0)
      UNION ALL
      SELECT EXC.OID_CONTENEDOR,
             CUEFEC.COD_CLIENTE,
             CUEFEC.DES_CLIENTE,
             CUEFEC.COD_SUBCLIENTE,
             CUEFEC.COD_PTO_SERVICIO,
             CUEFEC.COD_CANAL,
             CUEFEC.DES_CANAL,
             CUEFEC.COD_SUBCANAL,
             CUEFEC.DES_SUBCANAL,
             CUEFEC.COD_DELEGACION,
             CUEFEC.COD_PLANTA,
             CUEFEC.OID_SECTOR,
             CUEFEC.COD_SECTOR,
             CUEFEC.DES_SECTOR,
             POS.COD_POSICION
        FROM SAPR_TEFECTIVOXCONTENEDOR EXC
       INNER JOIN SAPR_VCUENTA CUEFEC
          ON EXC.OID_CUENTA_MOVIMIENTO = CUEFEC.OID_CUENTA
        LEFT JOIN SAPR_TPOSICION_SECTOR POS
          ON POS.OID_SECTOR = CUEFEC.OID_SECTOR
       WHERE EXC.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
         AND (var$existeCuenta = 1 AND
             EXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT CLAI.OID
                 FROM SAPR_GTT_TOIDS CLAI
                WHERE CLAI.COD_CALIFICADOR = 'CUENTAS') OR
             var$existeCuenta = 0);

    par$selects := par$selects + sql%rowcount;

    --busca dados de efetivo e MP
    OPEN par$rcDetalleEfec FOR
      SELECT EXC.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             DIV.DES_DIVISA,
             DIV.COD_SIMBOLO,
             DEN.COD_DENOMINACION,
             CAL.COD_CALIDAD,
             EXC.BOL_DISPONIBLE,
             EXC.BOL_BLOQUEADO,
             SUM(EXC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(EXC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TEFECTIVOXCONTENEDOR EXC
       INNER JOIN SAPR_TCUENTA CUEFEC
          ON EXC.OID_CUENTA_MOVIMIENTO = CUEFEC.OID_CUENTA
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = EXC.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = EXC.OID_DENOMINACION
       LEFT JOIN GEPR_TCALIDAD CAL
          ON CAL.OID_CALIDAD = EXC.OID_CALIDAD
       WHERE EXC.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
         AND (var$existeCuenta = 1 AND
             EXC.OID_CUENTA_MOVIMIENTO IN
             (SELECT CLAI.OID
                 FROM SAPR_GTT_TOIDS CLAI
                WHERE CLAI.COD_CALIFICADOR = 'CUENTAS') OR
             var$existeCuenta = 0)
       GROUP BY EXC.OID_CONTENEDOR,
                DIV.COD_ISO_DIVISA,
                DIV.DES_DIVISA,
                DIV.COD_SIMBOLO,
                DEN.COD_DENOMINACION,
                CAL.COD_CALIDAD,
                EXC.BOL_DISPONIBLE,
                EXC.BOL_BLOQUEADO
      UNION ALL
      SELECT C.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             DIV.DES_DIVISA,
             DIV.COD_SIMBOLO,
             DEN.COD_DENOMINACION,
             NULL COD_CALIDAD,
             0 BOL_DISPONIBLE,
             0 BOL_BLOQUEADO,
             SUM(DECEFEC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(DECEFEC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TDECLARADO_EFECTIVO DECEFEC
       INNER JOIN SAPR_TREMESA R
          ON R.OID_REMESA = DECEFEC.OID_REMESA
         AND DECEFEC.OID_BULTO IS NULL
       INNER JOIN SAPR_TCONTENEDOR C
          ON C.OID_CONTENEDOR = R.OID_CONTENEDOR_PADRE
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = DECEFEC.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = DECEFEC.OID_DENOMINACION
       WHERE DECEFEC.COD_NIVEL_DETALLE = 'D'
         AND C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
       GROUP BY C.OID_CONTENEDOR,
                DIV.COD_ISO_DIVISA,
                DIV.DES_DIVISA,
                DIV.COD_SIMBOLO,
                DEN.COD_DENOMINACION
      UNION ALL
      SELECT C.OID_CONTENEDOR,
             DIV.COD_ISO_DIVISA,
             DIV.DES_DIVISA,
             DIV.COD_SIMBOLO,
             DEN.COD_DENOMINACION,
             NULL COD_CALIDAD,
             0 BOL_DISPONIBLE,
             0 BOL_BLOQUEADO,
             SUM(DECEFEC.NEL_CANTIDAD) NEL_CANTIDAD,
             SUM(DECEFEC.NUM_IMPORTE) NUM_IMPORTE
        FROM SAPR_TDECLARADO_EFECTIVO DECEFEC
       INNER JOIN SAPR_TBULTO B
          ON B.OID_REMESA = DECEFEC.OID_REMESA
         AND B.OID_BULTO = DECEFEC.OID_BULTO
       INNER JOIN SAPR_TCONTENEDOR C
          ON C.OID_CONTENEDOR = B.OID_CONTENEDOR_PADRE
       INNER JOIN GEPR_TDIVISA DIV
          ON DIV.OID_DIVISA = DECEFEC.OID_DIVISA
       INNER JOIN GEPR_TDENOMINACION DEN
          ON DEN.OID_DENOMINACION = DECEFEC.OID_DENOMINACION
       WHERE DECEFEC.COD_NIVEL_DETALLE = 'D'
         AND C.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES')
       GROUP BY C.OID_CONTENEDOR,
                DIV.COD_ISO_DIVISA,
                DIV.DES_DIVISA,
                DIV.COD_SIMBOLO,
                DEN.COD_DENOMINACION;

    par$selects := par$selects + sql%rowcount;

    OPEN par$rcAlertas FOR
      SELECT AL.OID_CONTENEDOR,
             AL.GMT_CREACION,
             AL.COD_TIPO_ALERTA,
             trunc(sysdate) - TRUNC(AL.GMT_CREACION) AS DIAS_ALERTA,
             AL.NEC_DIAS_VENCER,
             AL.DES_EMAILS
        FROM SAPR_TALERTA_VENC_CONTENEDOR AL
       WHERE AL.OID_CONTENEDOR IN
             (SELECT CLAI.OID
                FROM SAPR_GTT_TOIDS CLAI
               WHERE CLAI.COD_CALIFICADOR = 'CONTENEDORES');

    par$selects := par$selects + sql%rowcount;

    var$registros := par$deletes + par$selects + par$inserts;

    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                         const$nombre_func,
                                                         par$inserts,
                                                         0,
                                                         par$deletes,
                                                         0,
                                                         par$selects,
                                                         var$registros,
                                                         var$inicio,
                                                         null,
                                                         'C',
                                                         par$info_ejecucion,
                                                         var$err,
                                                         par$cod_usuario,
                                                         par$cod_ejecucion);

  EXCEPTION

    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           0,
                                                           par$deletes,
                                                           0,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'C',
                                                           par$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           par$cod_ejecucion);

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error',
                              true);

  END SP_CONSULTARCONTPACKMODULAR;



  procedure svalidar_tipo_valor(par$oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oid_,
                                par$aefco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                par$aefco_oid_denom      in gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ampco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                                par$ampco_oid_mp         in gepr_pcomon_###VERSION###.tipo$oids_,
                                par$obj_tipo_cont        in gepr_pcomon_###VERSION###.tipo$tipoContenedor,
                                par$cod_cultura          in gepr_pcomon_###VERSION###.tipo$cod_,
                                par$selects              out gepr_pcomon_###VERSION###.tipo$nel_) as

    var$ampcont_oid_mp        gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefco_oid_denom       gepr_pcomon_###VERSION###.tipo$oids_;
    var$aefco_oid_denom_table varchar2_array;
    var$bol_hay_billetes      gepr_pcomon_###VERSION###.tipo$nel_;
  begin

    par$selects := 0;

    if par$aefco_oid_contenedor is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefco_oid_contenedor) and
       par$aefco_oid_denom is not null and
       not gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefco_oid_denom) then

      var$aefco_oid_denom := gepr_putilidades_###VERSION###.ffiltrar_array_por_oid_padre(par$array_oids_padres => par$aefco_oid_contenedor,
                                                                                    par$array_itens       => par$aefco_oid_denom,
                                                                                    par$oid_corrente      => par$oid_contenedor);

    end if;

    if par$ampco_oid_contenedor is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampco_oid_contenedor) and
       par$ampco_oid_mp is not null and
       not gepr_putilidades_###VERSION###.farray_vacio_oid(par$ampco_oid_mp) then

      var$ampcont_oid_mp := gepr_putilidades_###VERSION###.ffiltrar_array_por_oid_padre(par$array_oids_padres => par$ampco_oid_contenedor,
                                                                                   par$array_itens       => par$ampco_oid_mp,
                                                                                   par$oid_corrente      => par$oid_contenedor);

    end if;

    if var$aefco_oid_denom is not null and
       not gepr_putilidades_###VERSION###.farray_vacio_oid(var$aefco_oid_denom) then

      var$aefco_oid_denom_table := gepr_putilidades_###VERSION###.fconverter_array_table(par$table_varchar2 => var$aefco_oid_denom);

    end if;
  if par$obj_tipo_cont.BOL_PICO = 0 then
    if par$obj_tipo_cont.cod_tipo_unidad_medida =
       gepr_pcomon_###VERSION###.const$codTipoUniMedBillete then

      if var$ampcont_oid_mp is not null and not
          gepr_putilidades_###VERSION###.farray_vacio_oid(par$array => var$ampcont_oid_mp) then

        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgTipoContenedorBillete',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;

      end if;

      if var$aefco_oid_denom_table is not null and
         var$aefco_oid_denom_table.count > 0 then

        select count(*)
          into var$bol_hay_billetes
          from gepr_tdenominacion d
         inner join table(var$aefco_oid_denom_table) t
            on t.column_value = d.oid_denominacion
         where d.bol_billete = 0;

        par$selects := +sql%ROWCOUNT;

        if var$bol_hay_billetes > 0 then

          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'MsgTipoContenedorBillete',
                                                                      gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      NULL,
                                                                      1));
          RETURN;

        end if;

      end if;

    elsif par$obj_tipo_cont.cod_tipo_unidad_medida =
          gepr_pcomon_###VERSION###.const$codTipoUniMedMoneda then

      if var$ampcont_oid_mp is not null and not
          gepr_putilidades_###VERSION###.farray_vacio_oid(par$array => var$ampcont_oid_mp) then

        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgTipoContenedorMoneda',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;

      end if;

      if var$aefco_oid_denom_table is not null and
         var$aefco_oid_denom_table.count > 0 then

        select count(*)
          into var$bol_hay_billetes
          from gepr_tdenominacion d
         inner join table(var$aefco_oid_denom_table) t
            on t.column_value = d.oid_denominacion
         where d.bol_billete = 1;

        par$selects := +sql%ROWCOUNT;

        if var$bol_hay_billetes > 0 then

          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'MsgTipoContenedorMoneda',
                                                                      gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      NULL,
                                                                      1));
          RETURN;

        end if;

      end if;

    elsif par$obj_tipo_cont.cod_tipo_unidad_medida =
          gepr_pcomon_###VERSION###.const$codTipoUniMedMedioPago then

      if var$aefco_oid_denom is not null and not
          gepr_putilidades_###VERSION###.farray_vacio_oid(par$array => var$aefco_oid_denom) then

        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'MsgTipoContenedorMedioPago',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    1));
        RETURN;

      end if;

    end if;

  end if;

  end svalidar_tipo_valor;

  procedure svalidar_calidad(par$oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oid_,
                             par$aefco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                             par$aefco_oid_calidad    IN gepr_pcomon_###VERSION###.tipo$oids_,
                             par$obj_tipo_cont        in gepr_pcomon_###VERSION###.tipo$tipoContenedor,
                             par$cod_cultura          in gepr_pcomon_###VERSION###.tipo$cod_,
                             par$selects              out gepr_pcomon_###VERSION###.tipo$nel_) as

    var$aefco_oid_calidad_table varchar2_array;
    var$aefco_oid_calidad       gepr_pcomon_###VERSION###.tipo$oids_;
    var$bol_emite_error         gepr_pcomon_###VERSION###.tipo$nbol_;
    var$oid_calidad_det         gepr_pcomon_###VERSION###.tipo$oid_;

  begin

    par$selects := 0;

    if par$aefco_oid_contenedor is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefco_oid_contenedor) and
       par$aefco_oid_calidad is not null and
       not gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefco_oid_calidad) then

      var$aefco_oid_calidad := gepr_putilidades_###VERSION###.ffiltrar_array_por_oid_padre(par$array_oids_padres => par$aefco_oid_contenedor,
                                                                                      par$array_itens       => par$aefco_oid_calidad,
                                                                                      par$oid_corrente      => par$oid_contenedor);

      var$aefco_oid_calidad_table := gepr_putilidades_###VERSION###.fconverter_array_table(par$table_varchar2 => var$aefco_oid_calidad);

    end if;

    if var$aefco_oid_calidad_table is not null and
       var$aefco_oid_calidad_table.count > 0 then

      if par$obj_tipo_cont.OID_CALIDAD is not null then

        begin

          SELECT 1
            INTO var$bol_emite_error
            from table(var$aefco_oid_calidad_table) t
           where t.column_value <> par$obj_tipo_cont.OID_CALIDAD;
        exception
          when no_data_found then
            var$bol_emite_error := 0;
        end;

        par$selects := +sql%ROWCOUNT;

      else

        begin

          select c.oid_calidad
            into var$oid_calidad_det
            from gepr_tcalidad c
           where c.cod_calidad = gepr_pcomon_###VERSION###.const$CodCalidadDeteriorado;

        exception
          when no_data_found then
            var$oid_calidad_det := null;

        end;

        par$selects := +sql%ROWCOUNT;

        begin

          SELECT 1
            INTO var$bol_emite_error
            from table(var$aefco_oid_calidad_table) t
           where t.column_value = var$oid_calidad_det;

        exception
          when no_data_found then
            var$bol_emite_error := 0;
        end;

      end if;

    end if;

    if var$bol_emite_error > 0 then

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgCalidadeIncorreta',
                                                                  gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;

    end if;

  end svalidar_calidad;

  procedure svalidar_cantidad(par$oid_contenedor       in gepr_pcomon_###VERSION###.tipo$oid_,
                              par$aefco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                              par$aefco_nel_cantidad   IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$ampco_oid_contenedor in gepr_pcomon_###VERSION###.tipo$oids_,
                              par$ampco_nel_cantidad   IN gepr_pcomon_###VERSION###.tipo$nels_,
                              par$obj_tipo_cont        in gepr_pcomon_###VERSION###.tipo$tipoContenedor,
                              par$cod_cultura          in gepr_pcomon_###VERSION###.tipo$cod_) as

    var$aefco_nel_cantidad       gepr_pcomon_###VERSION###.tipo$nels_;
    var$aefco_nel_cantidad_table varchar2_array;

    var$ampco_nel_cantidad       gepr_pcomon_###VERSION###.tipo$nels_;
    var$ampco_nel_cantidad_table varchar2_array;

    var$bol_emite_error      gepr_pcomon_###VERSION###.tipo$nbol_;
    var$bol_validar_efectivo gepr_pcomon_###VERSION###.tipo$nbol_;

    var$nel_cantidad_tp_cont gepr_pcomon_###VERSION###.tipo$nel_;
    var$nel_cantidad         gepr_pcomon_###VERSION###.tipo$nel_;
  begin

    if par$aefco_oid_contenedor is not null and not
        gepr_putilidades_###VERSION###.farray_vacio_oid(par$aefco_oid_contenedor) then

      var$aefco_nel_cantidad := gepr_putilidades_###VERSION###.ffiltrar_array_por_nel_padre(par$array_oids_padres => par$aefco_oid_contenedor,
                                                                                       par$array_itens       => par$aefco_nel_cantidad,
                                                                                       par$oid_corrente      => par$oid_contenedor);

      var$aefco_nel_cantidad_table := gepr_putilidades_###VERSION###.fconverter_array_nel_table(par$table_varchar2 => var$aefco_nel_cantidad);

      var$bol_validar_efectivo := 1;

    else

      var$ampco_nel_cantidad := gepr_putilidades_###VERSION###.ffiltrar_array_por_nel_padre(par$array_oids_padres => par$ampco_oid_contenedor,
                                                                                       par$array_itens       => par$ampco_nel_cantidad,
                                                                                       par$oid_corrente      => par$oid_contenedor);

      var$ampco_nel_cantidad_table := gepr_putilidades_###VERSION###.fconverter_array_nel_table(par$table_varchar2 => var$ampco_nel_cantidad);

    end if;

    if par$obj_tipo_cont.NEC_CANTIDAD is not null and
       par$obj_tipo_cont.NUM_VALOR_UNIDAD is not null then

      var$nel_cantidad_tp_cont := par$obj_tipo_cont.NUM_VALOR_UNIDAD *
                                  par$obj_tipo_cont.NEC_CANTIDAD;

      if var$bol_validar_efectivo > 0 then

        if var$aefco_nel_cantidad_table is not null and
           var$aefco_nel_cantidad_table.count > 0 then

          select sum(column_value)
            into var$nel_cantidad
            from table(var$aefco_nel_cantidad_table);

        end if;

      else

        if var$ampco_nel_cantidad_table is not null and
           var$ampco_nel_cantidad_table.count > 0 then

          select sum(column_value)
            into var$nel_cantidad
            from table(var$ampco_nel_cantidad_table);

        end if;

      end if;

      if var$nel_cantidad_tp_cont <> var$nel_cantidad then
        var$bol_emite_error := 1;
      end if;

    end if;

    if var$bol_emite_error > 0 then

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgCantidadIncorreta',
                                                                  gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDocCont,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
      RETURN;

    end if;

  end svalidar_cantidad;

  /* fcargar_rc_precintos */
  FUNCTION fcargar_rc_precintos(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rcPrecintos SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rcPrecintos FOR
      SELECT PC.OID_PRECINTOXCONTENEDOR,
             PC.OID_CONTENEDOR,
             PC.COD_PRECINTO,
             PC.GMT_CREACION,
             PC.DES_USUARIO_CREACION,
             PC.GMT_MODIFICACION,
             PC.DES_USUARIO_MODIFICACION,
             PC.BOL_PRECINTO_AUTOMATICO
        FROM SAPR_TPRECINTOXCONTENEDOR PC
  INNER JOIN SAPR_TDOCUMENTOXELEMENTO DE ON DE.OID_CONTENEDOR = PC.OID_CONTENEDOR
       WHERE DE.OID_CONTENEDOR IS NOT NULL
         AND DE.OID_DOCUMENTO IN 
             (SELECT OID OID_DOCUMENTO
                FROM  SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'DOCUMENTO');
    par$selects := sql%ROWCOUNT;
  
    RETURN var$rcPrecintos;
  
  END fcargar_rc_precintos;

END SAPR_PCONTENEDOR_###VERSION###;
/
