create or replace PACKAGE sapr_pperiodo_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad   CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pperiodo_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';
  const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);

   /* Verificar periodo */
  PROCEDURE sverificar_periodo_ex (par$oid_llamada              IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_canal                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_subcanal             IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_divisa               IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_sector               IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_pto_servicio         IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_ajeno                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$fyh_gestion              IN     gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura              IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$info_ejecucion           IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                   par$oid_maquina                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_planificacion           OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_periodo                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_tipo_planificacion      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$validaciones                OUT SYS_REFCURSOR,
                                   par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Verificar periodo */
  PROCEDURE sverificar_periodo (par$oid_llamada              IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_canal                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_subcanal             IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_divisa               IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_sector               IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_pto_servicio         IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_ajeno                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$fyh_gestion              IN     gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$tipo_periodo             IN     gepr_pcomon_###VERSION###.tipo$cod_ := 'AC',
                                par$collection_id            IN     gepr_pcomon_###VERSION###.tipo$cod_ := '',
                                par$oid_documento            IN     gepr_pcomon_###VERSION###.tipo$oid_ := '',
                                par$bol_generar_periodos     IN     gepr_pcomon_###VERSION###.tipo$nbol_:= 1,
                                par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                par$cod_cultura              IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$info_ejecucion           IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                par$oid_maquina                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_planificacion           OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_periodo                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_tipo_planificacion      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                par$debe_crear_periodo_ac       OUT gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$debe_actualizar_col_id      OUT gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$fyh_fin_periodo             OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$cod_validaciones            OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                par$des_validaciones            OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_);

    PROCEDURE sverificar_periodo (par$oid_llamada              IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_canal                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_subcanal             IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_divisa               IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_sector               IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_pto_servicio         IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_ajeno                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$fyh_gestion              IN     gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$tipo_periodo             IN     gepr_pcomon_###VERSION###.tipo$cod_ := 'AC',
                                par$collection_id            IN     gepr_pcomon_###VERSION###.tipo$cod_ := '',
                                par$oid_documento            IN     gepr_pcomon_###VERSION###.tipo$oid_ := '',
                                par$bol_generar_periodos     IN     gepr_pcomon_###VERSION###.tipo$nbol_ := 1,
                                par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                par$cod_cultura              IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$info_ejecucion           IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                par$oid_maquina                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_planificacion           OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_periodo                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_tipo_planificacion      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_validaciones            OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                par$des_validaciones            OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Generar periodo */
  PROCEDURE sgenerar_periodo (par$cod_delegacion   IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_planta       IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_sector       IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_usuario      IN     gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura      IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$info_ejecucion   IN     gepr_pcomon_###VERSION###.tipo$desc_,
                              par$validaciones        OUT SYS_REFCURSOR,
                              par$cod_ejecucion       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Relacionar periodo */
   PROCEDURE srelacionar_periodo (par$oid_llamada      IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_documento    IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_periodo      IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$collection_id    IN     gepr_pcomon_###VERSION###.tipo$cod_ := '',
                                 par$cod_usuario      IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$cod_cultura      IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$info_ejecucion   IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                 par$hacer_commit     IN     gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$validaciones        OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$cod_ejecucion       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Relacionar periodo manual */
  PROCEDURE srelacionar_periodo_manual (par$oid_llamada             IN    gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_documento           IN    gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$bol_generar_periodos    IN    gepr_pcomon_###VERSION###.tipo$nbol_);

  PROCEDURE srelacionar_mov_shipout_per 
                                    (par$oid_llamada          IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_documento        IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_);

  /* Regresa un Valor u Otro de Acuerdo al Boolean */
  FUNCTION fiif (par$statement NUMBER,
                 par$return_1 VARCHAR2,
                 par$return_2 VARCHAR2)
  RETURN VARCHAR2;

  /* Calculo Efectivo */
  PROCEDURE scalculo_efectivo (par$oid_documento      IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_tipo_calculo   IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_tipo_periodo   IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$bol_sumar          IN gepr_pcomon_###VERSION###.tipo$nbol_);

  /* Calculo Medio Pago */
  PROCEDURE scalculo_medio_pago (par$oid_documento      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_tipo_calculo   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_tipo_periodo   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$bol_sumar          IN gepr_pcomon_###VERSION###.tipo$nbol_);

  /* Executa a acao contabil de efectivo do documento */
  FUNCTION frecuperar_tipo_calculo (par$oid_documento   IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_cultura     IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$validaciones       OUT gepr_pcomon_###VERSION###.tipo$descs_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

  PROCEDURE srelacionar_documentos_mae (par$oid_llamada          IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$fechagestion         IN gepr_pcomon_###VERSION###.tipo$fyh_,
                                        par$cod_identificacion   IN gepr_pcomon_###VERSION###.tipo$desc_);

  /* Relacionar o Quitar movimientos en un periodo */
  PROCEDURE srelacionar_mov_periodo (par$abol_baja            IN     gepr_pcomon_###VERSION###.tipo$nbols_,
                                     par$abol_codigo_simple   IN     gepr_pcomon_###VERSION###.tipo$nbols_,
                                     par$acod_movimiento      IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$rc_validaciones         OUT SYS_REFCURSOR,
                                     par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$cod_);

  /* Validar informaciones de la procedure srelacionar_mov_periodo */
  PROCEDURE svalidar_mov_periodo (par$abol_baja            IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$abol_codigo_simple   IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$acod_movimiento      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* sdel_tperiodoxdocumento */
  PROCEDURE sdel_tperiodoxdocumento (par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_, par$oid_periodo IN gepr_pcomon_###VERSION###.tipo$oid_);

  /* sins_tperiodoxdocumento */
  PROCEDURE sins_tperiodoxdocumento (par$oid_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_periodo     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$cod_);
  /* Verificar Periodos masivos */
  PROCEDURE sverificar_periodo_masivos_ex (
                                   par$acod_index                IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_canal                IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_subcanal             IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_divisa               IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_sector               IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_pto_servicio         IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_ajeno                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$afyh_gestion              IN     gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura              IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$info_ejecucion           IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                   par$validaciones                OUT SYS_REFCURSOR,
                                   par$maquinas                    OUT SYS_REFCURSOR,
                                   par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_);
  /* Procedure que se encarga de desbloquear un periodo */
  PROCEDURE sdesbloquear_periodo (
                                par$oid_periodo         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_pais            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_device_id       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_,
                                par$codigo              OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                par$descripcion         OUT gepr_pcomon_###VERSION###.tipo$desc_);

  PROCEDURE svalidar_periodo_baja (par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$abol_baja            IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$abol_codigo_simple   IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$acod_movimiento      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_);

  PROCEDURE srelacionar_mov_periodo_baja (par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$abol_baja            IN     gepr_pcomon_###VERSION###.tipo$nbols_,
                                     par$abol_codigo_simple   IN     gepr_pcomon_###VERSION###.tipo$nbols_,
                                     par$acod_movimiento      IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$rc_validaciones         OUT SYS_REFCURSOR,
                                     par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$cod_);

  /* Se encarga de modificar el estado de los periodos segun la acción indicada */
  PROCEDURE smodificar_periodos (
    par$aoid_periodo IN gepr_pcomon_###VERSION###.tipo$oids_,
    par$aindex  IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$aaccion IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$acod_pais IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$acod_device_id IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$usr_,
    par$rc_periodos                OUT SYS_REFCURSOR
  );

  /* Se encarga de crear los periodos de RECOJO Y BOVEDA en base a los movimientos del periodo enviado por parámetro */
  PROCEDURE screar_periodo (par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_tipo_periodo          IN gepr_pcomon_###VERSION###.tipo$cod_, 
                            par$cod_periodo_confirmacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_);




  /* 
    Se encarga de insertar un registro en la tabla SAPR_TPERIODO 
    En caso de enviarse un par$oid_periodo válido se trata como una actualización caso contrario se considera un insert
  */
  PROCEDURE sins_upd_tperiodo(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_periodo           IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_planificacion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_maquina               IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_estado_periodo        IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$fyh_inicio                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$fyh_fin                   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$oid_tipo_periodo          IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$collection_id            IN     gepr_pcomon_###VERSION###.tipo$cod_ := NULL,
                          par$oid_subcanal              IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                          par$oid_divisa                IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                          par$oid_pto_servicio          IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                          par$cod_periodo_confirmacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Se encarga de insertar un registro en la tabla SAPR_TPERIODO_RELACION */
  PROCEDURE sins_tperiodo_relacion(par$oid_periodo        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_periodo_relacionado     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_usuario           IN gepr_pcomon_###VERSION###.tipo$cod_);

  PROCEDURE svalidar_corte
    (
      par$oid_llamada          IN  gepr_pcomon_###VERSION###.tipo$oid_,
      par$cod_formulario       IN  gepr_pcomon_###VERSION###.tipo$cod_,
      par$oid_documento        IN  gepr_pcomon_###VERSION###.tipo$oid_,  
      par$fyh_gestion          IN gepr_pcomon_###VERSION###.tipo$fyh_, 
      par$cod_subcanal         IN gepr_pcomon_###VERSION###.tipo$cod_,
      par$oid_divisa           IN gepr_pcomon_###VERSION###.tipo$oid_, 
      par$bol_corte_periodo    OUT gepr_pcomon_###VERSION###.tipo$nel_,
      par$oid_periodo          OUT gepr_pcomon_###VERSION###.tipo$oid_,
      par$oid_tipo_periodo     OUT gepr_pcomon_###VERSION###.tipo$oid_
    );

  PROCEDURE screar_corte
  (
      par$oid_llamada              IN     gepr_pcomon_###VERSION###.tipo$oid_,
      par$oid_periodo              IN     gepr_pcomon_###VERSION###.tipo$oid_,
      par$oid_tipo_periodo         IN     gepr_pcomon_###VERSION###.tipo$oid_,
      par$oid_documento            IN     gepr_pcomon_###VERSION###.tipo$oid_,
      par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_
  );

  /* Procedure utilizado por switch una vez realizado el envío del período de acreditación al banco */
  PROCEDURE senvio_periodo (par$oid_maquina               IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_periodo_confirmacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$hacer_commit              IN gepr_pcomon_###VERSION###.tipo$bol_ := TRUE);

  /*Crea los periodos iniciales de RECOJO Y BOVEDA al dar de alta una maquina*/
  PROCEDURE screar_periodos_iniciales
                                    (par$oid_llamada          IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_maquina        IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_);

  /*Genera los periodos  de acuerdo a la Programación en la Planificación*/
  PROCEDURE sgenerar_periodos
                          (par$oid_llamada           IN     gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_maquina          IN     gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                            par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_,
                            par$hacer_commit         IN     gepr_pcomon_###VERSION###.tipo$bol_ := TRUE);

  /*Crea o actualiza periodo de recojo al llegar un cashin*/
  PROCEDURE svalidar_periodo_recojo(
        par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_maquina                 IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_planificacion           IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$usr_,
        par$oid_periodo_ac              IN gepr_pcomon_###VERSION###.tipo$oid_,    
        par$cod_subcanal                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_divisa                  IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$collection_id               IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_documento               IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_periodo_re             OUT gepr_pcomon_###VERSION###.tipo$oid_);

  /*Crea o actualiza periodo de boveda al llegar un cashin*/
  PROCEDURE svalidar_periodo_boveda(
        par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_maquina                 IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_planificacion           IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$usr_,
        par$oid_periodo_ac              IN gepr_pcomon_###VERSION###.tipo$oid_, 
        par$oid_periodo_re              IN gepr_pcomon_###VERSION###.tipo$oid_, 
        par$cod_subcanal                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_divisa                  IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$collection_id               IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_documento               IN gepr_pcomon_###VERSION###.tipo$oid_);

  /* Almacena en la tabla auxiliar los rangos de fechas de referencia para la creación de períodos */
  PROCEDURE sgenerar_fechas_periodo(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_maquina        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$fyh_inicio_periodo IN gepr_pcomon_###VERSION###.tipo$fyh_);

  /* Verifica si el documento debe ser relacioando a período en la entrada de movimientos */
  FUNCTION fregla_doc_periodo_ac(
    par$oid_llamada    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_documento  IN gepr_pcomon_###VERSION###.tipo$oid_
  ) RETURN BOOLEAN;

END sapr_pperiodo_###VERSION###;

/
create or replace PACKAGE BODY sapr_pperiodo_###VERSION### AS
  /* Verificar periodo */
  PROCEDURE sverificar_periodo_ex (par$oid_llamada              IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_canal                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_subcanal             IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_divisa               IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_sector               IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_pto_servicio         IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$cod_ajeno                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$fyh_gestion              IN     gepr_pcomon_###VERSION###.tipo$fyh_,
                                   par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura              IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$info_ejecucion           IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                   par$oid_maquina                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_planificacion           OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$oid_periodo                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$cod_tipo_planificacion      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$validaciones                OUT SYS_REFCURSOR,
                                   par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    /* Validaciones */
    var$cod_validaciones    gepr_pcomon_###VERSION###.tipo$cods_;
    var$des_validaciones    gepr_pcomon_###VERSION###.tipo$descs_;
    var$tcod_validaciones   varchar2_array;
    var$tdes_validaciones   varchar2_array;
    var$oid_pto_servicio    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa          gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
     SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => 'sverificar_periodo_ex',
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: '||par$oid_llamada||', cod_canal: '||par$cod_canal||', cod_subcanal: '||par$cod_subcanal||', cod_sector: '||par$cod_sector||' , cod_pto_servicio: '||par$cod_pto_servicio||', cod_ajeno: '||par$cod_ajeno||', fyh_gestion: '||par$fyh_gestion||', cod_usuario: '||par$cod_usuario||', cod_cultura: '||par$cod_cultura||', info_ejecucion: '||par$info_ejecucion,
                            par$cod_identificador   => '');
    BEGIN
      SELECT CAN.OID_PTO_SERVICIO
        INTO var$oid_pto_servicio
        FROM GEPR_TCODIGO_AJENO C INNER JOIN GEPR_TPUNTO_SERVICIO CAN ON CAN.OID_PTO_SERVICIO = C.OID_TABLA_GENESIS
       WHERE C.COD_IDENTIFICADOR = par$cod_ajeno AND C.COD_AJENO = par$cod_pto_servicio AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE ('No fue posible encontrar un punto servicio con el codigo Genesis: {0}');
        var$cod_validaciones (var$cod_validaciones.COUNT) := '2040010016';
        var$des_validaciones (var$des_validaciones.COUNT) :=
          gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                           '2040010016',
                                           'GENERICO',
                                           gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                           par$cod_ajeno || ' - ' || par$cod_pto_servicio,
                                           0);
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => 'sverificar_periodo_ex',
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'No fue posible encontrar un punto servicio con el codigo Genesis: ' || par$cod_ajeno || ' - ' || par$cod_pto_servicio,
                            par$cod_identificador   => '');

    END;

    -- Validación del código divisa
    IF par$cod_ajeno IS NOT NULL THEN
      BEGIN
        SELECT DIVI.OID_DIVISA
          INTO var$oid_divisa
        FROM GEPR_TCODIGO_AJENO C 
          INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = C.OID_TABLA_GENESIS
        WHERE C.COD_IDENTIFICADOR = par$cod_ajeno AND C.COD_AJENO = par$cod_divisa AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA';
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          DBMS_OUTPUT.PUT_LINE ('No fue posible encontrar una divisa con el codigo Genesis: {0}');
          var$cod_validaciones (var$cod_validaciones.COUNT) := '2040010011';
          var$des_validaciones (var$des_validaciones.COUNT) :=
            gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                            '2040010011',
                                            'GENERICO',
                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                            par$cod_ajeno || ' - ' || par$cod_divisa,
                                            0);
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => 'sverificar_periodo_ex',
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'No fue posible encontrar una divisa con el codigo Genesis: ' || par$cod_ajeno || ' - ' || par$cod_divisa,
                              par$cod_identificador   => '');

      END;
    ELSE
      BEGIN
        SELECT DIVI.OID_DIVISA
          INTO var$oid_divisa
        FROM GEPR_TDIVISA DIVI
        WHERE DIVI.COD_ISO_DIVISA = par$cod_divisa;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          DBMS_OUTPUT.PUT_LINE ('No fue posible encontrar una divisa con el codigo Genesis: {0}');
          var$cod_validaciones (var$cod_validaciones.COUNT) := '2040010011';
          var$des_validaciones (var$des_validaciones.COUNT) :=
            gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                            '2040010011',
                                            'GENERICO',
                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                            par$cod_divisa,
                                            0);
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => 'sverificar_periodo_ex',
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'No fue posible encontrar una divisa con el codigo Genesis: ' || par$cod_divisa,
                              par$cod_identificador   => '');

      END;
    END IF;

    sverificar_periodo (par$oid_llamada              => par$oid_llamada,
                        par$cod_canal                => par$cod_canal,
                        par$cod_subcanal             => par$cod_subcanal,
                        par$oid_divisa               => var$oid_divisa,
                        par$cod_sector               => par$cod_sector,
                        par$oid_pto_servicio         => var$oid_pto_servicio,
                        par$cod_ajeno                => par$cod_ajeno,
                        par$fyh_gestion              => par$fyh_gestion,
                        par$cod_usuario              => par$cod_usuario,
                        par$cod_cultura              => par$cod_cultura,
                        par$info_ejecucion           => par$info_ejecucion,
                        par$oid_maquina              => par$oid_maquina,
                        par$oid_planificacion        => par$oid_planificacion,
                        par$oid_periodo              => par$oid_periodo,
                        par$cod_tipo_planificacion   => par$cod_tipo_planificacion,
                        par$cod_validaciones         => var$cod_validaciones,
                        par$des_validaciones         => var$des_validaciones,
                        par$cod_ejecucion            => par$cod_ejecucion);

    IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc (var$des_validaciones) THEN
      var$tcod_validaciones := gepr_putilidades_###VERSION###.fconverter_array_cod_table (par$table_varchar2 => var$cod_validaciones);
      var$tdes_validaciones := gepr_putilidades_###VERSION###.fconverter_array_desc_table (par$table_varchar2 => var$des_validaciones);

      OPEN par$validaciones FOR
        WITH COD AS
               (SELECT ROWNUM IDX, C.COLUMN_VALUE
                  FROM TABLE (var$tcod_validaciones) C
                 WHERE C.COLUMN_VALUE IS NOT NULL),
             DES AS
               (SELECT ROWNUM IDX, C.COLUMN_VALUE
                  FROM TABLE (var$tdes_validaciones) C
                 WHERE C.COLUMN_VALUE IS NOT NULL)
        SELECT COD.COLUMN_VALUE CODIGO, DES.COLUMN_VALUE DESCRICION
          FROM COD INNER JOIN DES ON DES.IDX = COD.IDX;
    END IF;
  END sverificar_periodo_ex;

  /* Verificar periodo */
  PROCEDURE sverificar_periodo (par$oid_llamada              IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_canal                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_subcanal             IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_divisa               IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_sector               IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_pto_servicio         IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_ajeno                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$fyh_gestion              IN     gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$tipo_periodo             IN     gepr_pcomon_###VERSION###.tipo$cod_ := 'AC',
                                par$collection_id            IN     gepr_pcomon_###VERSION###.tipo$cod_ := '',
                                par$oid_documento            IN     gepr_pcomon_###VERSION###.tipo$oid_ := '',
                                par$bol_generar_periodos     IN     gepr_pcomon_###VERSION###.tipo$nbol_ := 1,
                                par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                par$cod_cultura              IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$info_ejecucion           IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                par$oid_maquina                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_planificacion           OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_periodo                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_tipo_planificacion      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                par$debe_crear_periodo_ac       OUT gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$debe_actualizar_col_id      OUT gepr_pcomon_###VERSION###.tipo$nbol_,
                                par$fyh_fin_periodo             OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$cod_validaciones            OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                par$des_validaciones            OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    const$nombre_func   CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sverificar_periodo';

    /* variables locales */
    var$inicio                   gepr_pcomon_###VERSION###.tipo$gmt_ := SYSTIMESTAMP;
    var$registros                gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                      VARCHAR2 (4000);

    var$cod_canal                gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sector               gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_pto_servicio         gepr_pcomon_###VERSION###.tipo$oid_;
    var$periodos_abertos         SYS_REFCURSOR;
    var$cur_oid_periodo          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cur_fyh_inicio           gepr_pcomon_###VERSION###.tipo$fyh_;
    var$cur_fyh_fin              gepr_pcomon_###VERSION###.tipo$fyh_;

    var$fyh_periodo_vigente      gepr_pcomon_###VERSION###.tipo$fyh_;
    var$oid_periodo_vigente      gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_fecha_valida         gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$oid_planxmaq             gepr_pcomon_###VERSION###.tipo$oid_;
    var$aux_oid_planificacion    gepr_pcomon_###VERSION###.tipo$oid_;
    var$col_id_periodo           gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_est_periodo          gepr_pcomon_###VERSION###.tipo$cod_;
    var$bol_periodo_subcanal     gepr_pcomon_###VERSION###.tipo$nbol_ := 0; 
    var$bol_periodo_divisa       gepr_pcomon_###VERSION###.tipo$nbol_ := 0; 
    var$fyh_fin                  gepr_pcomon_###VERSION###.tipo$fyh_;
    var$existe                   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$es_cashin                gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$aux_cod_validaciones     gepr_pcomon_###VERSION###.tipo$cods_;
    var$aux_des_validaciones     gepr_pcomon_###VERSION###.tipo$descs_;
    var$cod_pais                 gepr_pcomon_###VERSION###.tipo$cod_;
    var$validaPuntoMaquina       gepr_pcomon_###VERSION###.tipo$desc_;
  BEGIN
     /*Si tengo oid_llamada logueo los parametros del SP*/
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: '||par$oid_llamada||
                            ', cod_canal: '||par$cod_canal||
                            ', cod_subcanal: '||par$cod_subcanal||
                            ', oid_divisa: '||par$oid_divisa||
                            ', cod_sector: '||par$cod_sector||
                            ', oid_pto_servicio: '||par$oid_pto_servicio||
                            ', cod_ajeno: '||par$cod_ajeno||
                            ', fyh_gestion: '|| TO_CHAR(par$fyh_gestion, 'YYYY-MM-DD HH24:MI:SS')||
                            ', tipo_periodo: '||par$tipo_periodo||
                            ', collection_id: '||par$collection_id||
                            ', oid_documento: '||par$oid_documento||
                            ', bol_generar_periodos: '||par$bol_generar_periodos||
                            ', cod_usuario: '||par$cod_usuario||
                            ', cod_cultura: '||par$cod_cultura||
                            ', info_ejecucion: '||par$info_ejecucion,
                            par$cod_identificador   => '');

    var$fyh_periodo_vigente := SYS_EXTRACT_UTC (CURRENT_TIMESTAMP);

    delete SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR IN ('OID_PLANXCANALES');

    /* === Validaciones iniciales === */
    IF par$cod_canal IS NULL THEN
      par$cod_validaciones (par$cod_validaciones.COUNT) := '2040010020';
      par$des_validaciones (par$des_validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         '2040010020',
                                         'GENERICO',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$cod_canal',
                                         0);
      /*Si tengo oid_llamada logueo error*/
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     =>  gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         '2040010020',
                                         'GENERICO',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$cod_canal',
                                         0),
                            par$cod_identificador   => '');           
    END IF;

    IF par$cod_sector IS NULL THEN
      par$cod_validaciones (par$cod_validaciones.COUNT) := '2040010020';
      par$des_validaciones (par$des_validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         '2040010020',
                                         'GENERICO',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$cod_sector',
                                         0);
       /*Si tengo oid_llamada logueo error*/
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         '2040010020',
                                         'GENERICO',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$cod_sector',
                                         0),
                            par$cod_identificador   => '');
    END IF;


    IF par$oid_pto_servicio IS NULL THEN
      par$cod_validaciones (par$cod_validaciones.COUNT) := '2040010020';
      par$des_validaciones (par$des_validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         '2040010020',
                                         'GENERICO',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$oid_pto_servicio',
                                         0);
         /*Si tengo oid_llamada logueo error*/
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         '2040010020',
                                         'GENERICO',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$oid_pto_servicio',
                                         0),
                            par$cod_identificador   => '');
    END IF;

    IF par$fyh_gestion IS NULL THEN
      par$cod_validaciones (par$cod_validaciones.COUNT) := '2040010020';
      par$des_validaciones (par$des_validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         '2040010020',
                                         'GENERICO',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$fyh_gestion',
                                         0);
        /*Si tengo oid_llamada logueo error*/
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     =>  gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         '2040010020',
                                         'GENERICO',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$fyh_gestion',
                                         0),
                            par$cod_identificador   => '');
    END IF;


    IF gepr_putilidades_###VERSION###.farray_vacio_desc (par$des_validaciones) THEN
      var$oid_pto_servicio := par$oid_pto_servicio;

      /* Codigos Sector y Canal - GENESIS */
      IF par$cod_ajeno IS NULL THEN
        DBMS_OUTPUT.PUT_LINE ('No utiliza codigos ajenos');
        var$cod_canal := par$cod_canal;
        var$cod_sector := par$cod_sector;
      ELSE
        DBMS_OUTPUT.PUT_LINE ('Utiliza codigos ajenos');

        BEGIN
          SELECT CAN.COD_CANAL
            INTO var$cod_canal
            FROM GEPR_TCODIGO_AJENO C INNER JOIN GEPR_TCANAL CAN ON CAN.OID_CANAL = C.OID_TABLA_GENESIS
           WHERE C.COD_IDENTIFICADOR = par$cod_ajeno AND C.COD_AJENO = par$cod_canal AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL';
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            DBMS_OUTPUT.PUT_LINE ('No fue posible recuperar el canal, utilizando el codigo ajeno');
            par$cod_validaciones (par$cod_validaciones.COUNT) := '2040010010';
            par$des_validaciones (par$des_validaciones.COUNT) :=
              gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                               '2040010010',
                                               'GENERICO',
                                               gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                               par$cod_canal,
                                               0);
         /*Si tengo oid_llamada logueo excepcion*/
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'No fue posible recuperar el canal, utilizando el codigo ajeno',
                            par$cod_identificador   => '');
        END;


        BEGIN
          SELECT SEC.COD_SECTOR
            INTO var$cod_sector
            FROM GEPR_TCODIGO_AJENO C INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = C.OID_TABLA_GENESIS
           WHERE C.COD_IDENTIFICADOR = par$cod_ajeno AND C.COD_AJENO = par$cod_sector AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR';
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            DBMS_OUTPUT.PUT_LINE ('No fue posible recuperar el sector, utilizando el codigo ajeno');
            par$cod_validaciones (par$cod_validaciones.COUNT) := '2040010008';
            par$des_validaciones (par$des_validaciones.COUNT) :=
              gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                               '2040010008',
                                               'GENERICO',
                                               gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                               par$cod_sector,
                                               0);
        /*Si tengo oid_llamada logueo excepcion*/
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'No fue posible recuperar el sector, utilizando el codigo ajeno',
                            par$cod_identificador   => '');
        END;
      END IF;

      /* Verifica maquina */
      BEGIN
        SELECT M.OID_MAQUINA
          INTO par$oid_maquina
          FROM SAPR_TMAQUINA M
               INNER JOIN SAPR_TTIPO_MAQUINA TM ON TM.OID_TIPO_MAQUINA = M.OID_TIPO_MAQUINA
               INNER JOIN GEPR_TSECTOR S ON S.OID_SECTOR = M.OID_SECTOR AND S.BOL_ACTIVO = 1
         WHERE TM.COD_TIPO_MAQUINA = 'MAE' AND M.BOL_ACTIVO = 1 AND M.COD_IDENTIFICACION = var$cod_sector;


      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          DBMS_OUTPUT.PUT_LINE ('No es maquina');
          par$oid_maquina := '';
          /*Si tengo oid_llamada logueo excepcion*/
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'No es maquina',
                              par$cod_identificador   => '');
      END;

      BEGIN
        IF par$oid_maquina IS NOT NULL THEN

          -- RECUPERAMOS EL PAIS EN BASE AL SECTOR
          SELECT D.COD_PAIS
            INTO var$cod_pais
          FROM GEPR_TSECTOR S
            INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
            INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
            INNER JOIN SAPR_TMAQUINA M ON S.OID_SECTOR = M.OID_SECTOR
          WHERE M.COD_IDENTIFICACION = var$cod_sector;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,                            
                                          par$des_detalle     => 'Se recupera el país: ' || var$cod_pais || ' en base al sector del movimiento para utilizar en la búsqueda de parámetros',
                                          par$cod_identificador   => '');


          var$validaPuntoMaquina := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                                                par$cod_pais => var$cod_pais,
                                                                par$cod_parametro => 'ValidaMaquinaPunto',
                                                                par$cod_aplicacion => 'Genesis');

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Se recupera parámetro para validar la relación de la máquina con el punto de servicio, ValidaMaquinaPunto: ' || var$validaPuntoMaquina || '.',
                                            par$cod_identificador   => '');

          IF var$validaPuntoMaquina = '1' THEN 
            SELECT COUNT(1)
            INTO var$existe   
            FROM GEPR_TPUNTO_SERVICIO PTO
            INNER JOIN GEPR_TCODIGO_AJENO CODAJ_PTO ON  CODAJ_PTO.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' AND
                                                        CODAJ_PTO.COD_IDENTIFICADOR = 'MAE' AND
                                                        CODAJ_PTO.OID_TABLA_GENESIS = PTO.OID_PTO_SERVICIO AND
                                                        CODAJ_PTO.COD_AJENO = gepr_pcomon_###VERSION###.const$CodPtoServicioProsegur
            WHERE PTO.OID_PTO_SERVICIO = par$oid_pto_servicio;

            IF var$existe = 0 THEN
              SELECT COUNT(1)
                INTO var$existe   
                FROM GEPR_TPUNTO_SERVICIO PUSE 
                WHERE PUSE.OID_PTO_SERVICIO = par$oid_pto_servicio
                  AND PUSE.OID_MAQUINA = par$oid_maquina;

              IF var$existe  = 0 THEN
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,                            
                                  par$des_detalle     => 'La maquina par$oid_maquina '|| par$oid_maquina ||' no se encuentra asociado al punto de servicio ' ||par$oid_pto_servicio || '.',
                                  par$cod_identificador   => '');

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                  COD_CAMPO2,
                                                  COD_CAMPO3,
                                                  COD_CALIFICADOR)
                  VALUES ('2040100017',
                          gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                        '2040100017',
                                                        gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                        NULL,
                                                        0),
                          par$oid_pto_servicio,
                          'VALIDACIONES');

                  par$oid_maquina := NULL;
              END IF;
            ELSE
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,                            
                                par$des_detalle     => 'El punto de servicio corresponde al cliente PROSEGUR.',
                                par$cod_identificador   => '');
            END IF;
          END IF;
        END IF;

      EXCEPTION
           WHEN OTHERS THEN
           par$oid_maquina := NULL;

      END;

      IF par$oid_maquina IS NOT NULL THEN
        /* Verifica planificacion */
        sapr_pplanificacion_###VERSION###.sverifica_documento(par$oid_llamada       => par$oid_llamada,
                                                                par$oid_maquina       => par$oid_maquina,
                                                                par$cod_canal         => par$cod_canal,
                                                                par$cod_subcanal      => par$cod_subcanal,
                                                                par$fyh_gestion       => par$fyh_gestion,
                                                                par$cod_cultura       => par$cod_cultura,
                                                                par$oid_planificacion => par$oid_planificacion,
                                                                par$cod_validaciones  => var$aux_cod_validaciones,
                                                                par$des_validaciones  => var$aux_des_validaciones);

        IF par$oid_planificacion IS NOT NULL THEN
          -- Recuperamos datos de la planificación
          SELECT TP.COD_TIPO_PLANIFICACION, P.BOL_PERIODO_SUBCANAL, P.BOL_PERIODO_DIVISA
            INTO par$cod_tipo_planificacion, var$bol_periodo_subcanal, var$bol_periodo_divisa 
          FROM SAPR_TPLANIFICACION P
            INNER JOIN SAPR_TTIPO_PLANIFICACION TP ON TP.OID_TIPO_PLANIFICACION = P.OID_TIPO_PLANIFICACION
          WHERE P.OID_PLANIFICACION = par$oid_planificacion;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'bol_periodo_subcanal: ' || var$bol_periodo_subcanal || ' - var$bol_periodo_divisa: ' || var$bol_periodo_divisa ,
                      par$cod_identificador   => '');


          /* Verificar si la planificación tiene marcado el proceso BBVA_PERU */
          SELECT COUNT(1)
            INTO var$existe
          FROM SAPR_TPLANIFICACION PLANI
          INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION AND TPLANI.COD_TIPO_PLANIFICACION = 'FECHA_VALOR_CONFIR' 
          INNER JOIN SAPR_TPROCESOXPLANIFICACION TPXP ON TPXP.OID_PLANIFICACION = PLANI.OID_PLANIFICACION
          INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO AND TPRO.COD_PROCESO = 'BBVA_PERU'
          WHERE PLANI.OID_PLANIFICACION = par$oid_planificacion;

          IF var$existe > 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Planificación con proceso de BBVA_PERU' ,
                    par$cod_identificador   => '');

            -- Validamos que se trate de un Cashin
            /*Obtengo el tipo de documento*/
            BEGIN
              SELECT COUNT(1) 
                INTO var$es_cashin 
              FROM SAPR_TDOCUMENTO DOCU
                INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOCU.OID_FORMULARIO
              WHERE FORMU.COD_FORMULARIO IN ('MAECIC','MAECID') 
                AND DOCU.OID_DOCUMENTO = par$oid_documento;
            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                  var$es_cashin := 0;
            END;
            IF var$es_cashin = 1 THEN 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'El documento es un CASHIN' ,
                      par$cod_identificador   => '');
              /* 
                Verificar que exista un período de acreditación abierto con fecha y hora inicio menor o igual a la fecha del movimiento
                cashin, fecha fin mayor o igual a la fecha del movimiento cashin y con las configuraciones de la planificacion igual a las del
                movimiento cashin
              */

              BEGIN 
                SELECT P.OID_PERIODO, P.COD_COLLECTION_ID, P.FYH_FIN
                  INTO var$cur_oid_periodo, var$col_id_periodo, var$fyh_fin 
                FROM SAPR_TPERIODO P 
                  INNER JOIN SAPR_TESTADO_PERIODO EP ON EP.OID_ESTADO_PERIODO = P.OID_ESTADO_PERIODO
                  INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = par$tipo_periodo
                  LEFT JOIN GEPR_TSUBCANAL SCANAL ON SCANAL.OID_SUBCANAL = P.OID_SUBCANAL
                WHERE P.OID_PLANIFICACION = par$oid_planificacion
                  AND P.OID_MAQUINA = par$oid_maquina
                  AND EP.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') 
                  AND P.oid_acreditacion IS NULL
                  AND P.FYH_INICIO <= par$fyh_gestion 
                  AND P.FYH_FIN >= par$fyh_gestion
                  AND ((SCANAL.COD_SUBCANAL = par$cod_subcanal AND var$bol_periodo_subcanal = 1) OR (var$bol_periodo_subcanal = 0 AND P.OID_SUBCANAL IS NULL))
                  AND ((P.OID_DIVISA = par$oid_divisa AND var$bol_periodo_divisa = 1) OR (var$bol_periodo_divisa = 0 AND P.OID_DIVISA IS NULL));
              EXCEPTION
                WHEN NO_DATA_FOUND THEN
                  var$cur_oid_periodo := NULL;
                  var$col_id_periodo := NULL;
                  var$fyh_fin := NULL;
              END;
              IF var$cur_oid_periodo IS NOT NULL THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Se encuentra período abierto dentro del rango correspondiente a la fecha del documento de cur_oid_periodo: ' || var$cur_oid_periodo,
                                            par$cod_identificador   => '');
                par$oid_periodo := var$cur_oid_periodo;

                -- Verificar que el período de acreditación encontrado tiene collection ID
                IF var$col_id_periodo IS NOT NULL THEN
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'El período posee Collection ID: ' || var$col_id_periodo,
                        par$cod_identificador   => '');

                  -- Verificar que el movimiento tiene collection ID
                  IF par$collection_id IS NOT NULL THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'El documento posee Collection ID: ' || par$collection_id,
                      par$cod_identificador   => '');


                    -- Verificar que el Collection ID del período es igual al Collection ID del movimiento
                    IF var$col_id_periodo <> par$collection_id THEN
                      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'El collectionID del periodo:' || var$col_id_periodo || ' es distinto al del documento: ' || par$collection_id,
                                        par$cod_identificador   => '');
                      par$debe_crear_periodo_ac := 1;
                      par$fyh_fin_periodo := var$fyh_fin;
                    END IF;
                  ELSE
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'El documento no posee Collection ID, NO se debe actualizar Collection ID del período',
                      par$cod_identificador   => '');
                  END IF;

                ELSE
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'El período no posee Collection ID' ,
                      par$cod_identificador   => '');
                  -- Verificar que el movimiento tiene collection ID
                  IF par$collection_id IS NOT NULL THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'El documento posee Collection ID: ' || par$collection_id || '. Se debe actualizar Collection ID del período',
                      par$cod_identificador   => '');

                    par$debe_actualizar_col_id := 1;

                  ELSE
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'El documento no posee Collection ID, NO se debe actualizar Collection ID del período',
                      par$cod_identificador   => '');
                  END IF;
                END IF;
              ELSE
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'NO se encuentra período abierto dentro del rango correspondiente a la fecha del documento',
                                            par$cod_identificador   => '');

                --* Llamar al procedimiento para generar períodos
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Comienza llamada al procedimiento para generar períodos para máquina de OID_MAQUINA: ' || par$oid_maquina,
                                            par$cod_identificador   => '');
                sapr_pperiodo_###VERSION###.sgenerar_periodos(par$oid_llamada       => par$oid_llamada,
                                                              par$oid_maquina       => par$oid_maquina,
                                                              par$cod_cultura       => par$cod_cultura,
                                                              par$info_ejecucion    => par$info_ejecucion,
                                                              par$cod_usuario       => par$cod_usuario,
                                                              par$hacer_commit      => FALSE);

                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                              par$des_version     => const$version,                            
                                              par$des_detalle     => 'Finaliza llamada al procedimiento para generar períodos para máquina de OID_MAQUINA: ' || par$oid_maquina,
                                              par$cod_identificador   => '');

                /* buscar todos los periodo abiertos que la FYH_INICIO sea menor o igual que la fecha actual */
                OPEN var$periodos_abertos FOR
                    SELECT OID_PERIODO, FYH_INICIO, FYH_FIN
                      FROM SAPR_TPERIODO P 
                      INNER JOIN SAPR_TESTADO_PERIODO EP ON EP.OID_ESTADO_PERIODO = P.OID_ESTADO_PERIODO
                      INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = par$tipo_periodo
                      LEFT JOIN GEPR_TSUBCANAL SCANAL ON SCANAL.OID_SUBCANAL = P.OID_SUBCANAL
                    WHERE     P.OID_PLANIFICACION = par$oid_planificacion
                          AND P.OID_MAQUINA = par$oid_maquina
                          AND EP.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') 
                          AND P.oid_acreditacion IS NULL
                          AND P.FYH_INICIO <= var$fyh_periodo_vigente
                          AND ((SCANAL.COD_SUBCANAL = par$cod_subcanal AND var$bol_periodo_subcanal = 1) OR (var$bol_periodo_subcanal = 0 AND P.OID_SUBCANAL IS NULL))
                          AND ((P.OID_DIVISA = par$oid_divisa AND var$bol_periodo_divisa = 1) OR (var$bol_periodo_divisa = 0 AND P.OID_DIVISA IS NULL))

                  ORDER BY P.FYH_INICIO; 

                DBMS_OUTPUT.PUT_LINE ('-');

                LOOP
                  FETCH var$periodos_abertos INTO var$cur_oid_periodo, var$cur_fyh_inicio, var$cur_fyh_fin;

                  /* https://docs.oracle.com/cd/B12037_01/appdev.101/b10807/13_elems011.htm */
                  EXIT WHEN var$periodos_abertos%NOTFOUND OR var$periodos_abertos%NOTFOUND IS NULL;

                  IF    (var$cur_fyh_inicio <= par$fyh_gestion AND par$fyh_gestion <= var$cur_fyh_fin)
                    OR (par$oid_periodo IS NULL AND par$fyh_gestion < var$cur_fyh_inicio) THEN
                    par$oid_periodo := var$cur_oid_periodo;
                  END IF;

                  IF var$cur_fyh_inicio <= var$fyh_periodo_vigente AND var$fyh_periodo_vigente <= var$cur_fyh_fin THEN
                    var$oid_periodo_vigente := var$cur_oid_periodo;
                  END IF;
                END LOOP;

                CLOSE var$periodos_abertos;

                IF var$oid_periodo_vigente IS NOT NULL AND (par$oid_periodo IS NULL OR par$fyh_gestion > var$fyh_periodo_vigente) THEN
                  par$oid_periodo := var$oid_periodo_vigente;
                END IF;

              END IF;

            ELSE
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'El documento NO es un CASHIN' ,
                    par$cod_identificador   => '');
            END IF;
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'Planificación sin proceso de BBVA_PERU' ,
                      par$cod_identificador   => '');

            IF par$bol_generar_periodos = 1 THEN
              --* Llamar al procedimiento para generar períodos
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                          par$des_version     => const$version,                            
                                          par$des_detalle     => 'Comienza llamada al procedimiento para generar períodos para máquina de OID_MAQUINA: ' || par$oid_maquina,
                                          par$cod_identificador   => '');

              sapr_pperiodo_###VERSION###.sgenerar_periodos(par$oid_llamada       => par$oid_llamada,
                                                            par$oid_maquina       => par$oid_maquina,
                                                            par$cod_cultura       => par$cod_cultura,
                                                            par$info_ejecucion    => par$info_ejecucion,
                                                            par$cod_usuario       => par$cod_usuario,
                                                            par$hacer_commit      => FALSE);

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Finaliza llamada al procedimiento para generar períodos para máquina de OID_MAQUINA: ' || par$oid_maquina,
                                            par$cod_identificador   => '');
            END IF;

            /* buscar todos os periodos abertos que a data de inicio seja menor do que a data vigente */
            OPEN var$periodos_abertos FOR
                SELECT OID_PERIODO, FYH_INICIO, FYH_FIN
                FROM SAPR_TPERIODO P 
                  INNER JOIN SAPR_TESTADO_PERIODO EP ON EP.OID_ESTADO_PERIODO = P.OID_ESTADO_PERIODO
                  INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = par$tipo_periodo
                  LEFT JOIN GEPR_TSUBCANAL SCANAL ON SCANAL.OID_SUBCANAL = P.OID_SUBCANAL
                WHERE     P.OID_PLANIFICACION = par$oid_planificacion
                      AND P.OID_MAQUINA = par$oid_maquina
                      AND EP.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND P.oid_acreditacion IS NULL
                      AND P.FYH_INICIO <= var$fyh_periodo_vigente
                      AND ((SCANAL.COD_SUBCANAL = par$cod_subcanal AND var$bol_periodo_subcanal = 1) OR (var$bol_periodo_subcanal = 0 AND P.OID_SUBCANAL IS NULL))
                      AND ((P.OID_DIVISA = par$oid_divisa AND var$bol_periodo_divisa = 1) OR (var$bol_periodo_divisa = 0 AND P.OID_DIVISA IS NULL))
              ORDER BY P.FYH_INICIO;
            LOOP
              FETCH var$periodos_abertos INTO var$cur_oid_periodo, var$cur_fyh_inicio, var$cur_fyh_fin;

              /* https://docs.oracle.com/cd/B12037_01/appdev.101/b10807/13_elems011.htm */
              EXIT WHEN var$periodos_abertos%NOTFOUND OR var$periodos_abertos%NOTFOUND IS NULL;

              IF    (var$cur_fyh_inicio <= par$fyh_gestion AND par$fyh_gestion <= var$cur_fyh_fin)
                OR (par$oid_periodo IS NULL AND par$fyh_gestion < var$cur_fyh_inicio) THEN
                par$oid_periodo := var$cur_oid_periodo;
              END IF;

              IF var$cur_fyh_inicio <= var$fyh_periodo_vigente AND var$fyh_periodo_vigente <= var$cur_fyh_fin THEN
                var$oid_periodo_vigente := var$cur_oid_periodo;
              END IF;
            END LOOP;

            CLOSE var$periodos_abertos;


            IF var$oid_periodo_vigente IS NOT NULL AND (par$oid_periodo IS NULL OR par$fyh_gestion > var$fyh_periodo_vigente) THEN
              par$oid_periodo := var$oid_periodo_vigente;
            END IF;
          END IF;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                par$des_version     => const$version,                            
                                par$des_detalle     => 'El oid del periodo encontrado para relacionar es :' || par$oid_periodo,
                                par$cod_identificador   => '');
        ELSE
          -- Recorremos las validaciones en los arrays asociativos
          IF var$aux_cod_validaciones IS NOT NULL AND var$aux_cod_validaciones.COUNT > 0 THEN 
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Recorremos los errores de sapr_pplanificacion_###VERSION###.sverifica_documento',
                    par$cod_identificador   => '');

            FOR idx IN var$aux_cod_validaciones.first .. var$aux_cod_validaciones.last LOOP
              par$cod_validaciones(par$cod_validaciones.COUNT) := var$aux_cod_validaciones(idx);
              par$des_validaciones(par$des_validaciones.COUNT) := var$aux_des_validaciones(idx);
            END LOOP;
          END IF;
        END IF;
      END IF;
    END IF;
    /* === Fin de la segunda parte - Validar periodo === */
    -- Logueo de validaciones
    IF par$cod_validaciones IS NOT NULL AND par$cod_validaciones.COUNT > 0 THEN
      FOR idx IN  par$cod_validaciones.first ..  par$cod_validaciones.last LOOP 
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,                            
                          par$des_detalle     => idx || ' - ' || par$cod_validaciones(idx) || ' - ' || par$des_validaciones(idx),
                          par$cod_identificador   => '');
      END LOOP;
    END IF;
  /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      /*Si tengo oid_llamada logueo excepcion*/

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$codFuncionalidad || '.' || const$nombre_func,
                                                        var$inserts,
                                                        var$updates,
                                                        var$deletes,
                                                        var$merges,
                                                        var$selects,
                                                        var$registros,
                                                        var$inicio,
                                                        NULL,
                                                        'R',
                                                        par$info_ejecucion,
                                                        var$err,
                                                        par$cod_usuario,
                                                        par$cod_ejecucion);
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$err,
                            par$cod_identificador   => '');
      raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, 'Error: ' || const$codFuncionalidad || '.' || const$nombre_func, TRUE);
  END sverificar_periodo;

  /* Verificar periodo */
  PROCEDURE sverificar_periodo (par$oid_llamada              IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_canal                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_subcanal             IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_divisa               IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_sector               IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$oid_pto_servicio         IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_ajeno                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$fyh_gestion              IN     gepr_pcomon_###VERSION###.tipo$fyh_,
                                par$tipo_periodo             IN     gepr_pcomon_###VERSION###.tipo$cod_ := 'AC',
                                par$collection_id            IN     gepr_pcomon_###VERSION###.tipo$cod_ := '',
                                par$oid_documento            IN     gepr_pcomon_###VERSION###.tipo$oid_ := '',
                                par$bol_generar_periodos     IN     gepr_pcomon_###VERSION###.tipo$nbol_ := 1,
                                par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                par$cod_cultura              IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                par$info_ejecucion           IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                par$oid_maquina                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_planificacion           OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_periodo                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                par$cod_tipo_planificacion      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_validaciones            OUT gepr_pcomon_###VERSION###.tipo$cods_,
                                par$des_validaciones            OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
     const$nombre_func   CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sverificar_periodo';
    var$debe_crear_periodo_ac       gepr_pcomon_###VERSION###.tipo$nbol_;
    var$debe_actualizar_col_id      gepr_pcomon_###VERSION###.tipo$nbol_;
    var$fyh_fin_periodo             gepr_pcomon_###VERSION###.tipo$fyh_;

    BEGIN

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: '||par$oid_llamada||
                            ', cod_canal: '||par$cod_canal||
                            ', cod_subcanal: '||par$cod_subcanal||
                            ', cod_sector: '||par$cod_sector||
                            ', oid_pto_servicio: '||par$oid_pto_servicio||
                            ', cod_ajeno: '||par$cod_ajeno||
                            ', fyh_gestion: '||par$fyh_gestion||
                            ', bol_generar_periodos: '||par$bol_generar_periodos||
                            ', cod_usuario: '||par$cod_usuario||
                            ', cod_cultura: '||par$cod_cultura||
                            ', info_ejecucion: '||par$info_ejecucion,
                            par$cod_identificador   => '');
       sverificar_periodo (par$oid_llamada              => par$oid_llamada,
                        par$cod_canal                => par$cod_canal,
                        par$cod_subcanal             => par$cod_subcanal,
                        par$oid_divisa               => par$oid_divisa,
                        par$cod_sector               => par$cod_sector,
                        par$oid_pto_servicio         => par$oid_pto_servicio,
                        par$cod_ajeno                => par$cod_ajeno,
                        par$fyh_gestion              => par$fyh_gestion,
                        par$cod_usuario              => par$cod_usuario,
                        par$cod_cultura              => par$cod_cultura,
                        par$info_ejecucion           => par$info_ejecucion,
                        par$oid_maquina              => par$oid_maquina,
                        par$oid_planificacion        => par$oid_planificacion,
                        par$oid_periodo              => par$oid_periodo,
                        par$bol_generar_periodos     => par$bol_generar_periodos,
                        par$cod_tipo_planificacion   => par$cod_tipo_planificacion,
                        par$debe_crear_periodo_ac    => var$debe_crear_periodo_ac,
                        par$debe_actualizar_col_id   => var$debe_actualizar_col_id,   
                        par$fyh_fin_periodo          => var$fyh_fin_periodo, 
                        par$cod_validaciones         => par$cod_validaciones,
                        par$des_validaciones         => par$des_validaciones,
                        par$cod_ejecucion            => par$cod_ejecucion);
    END sverificar_periodo;


  /* Generar periodo */
  PROCEDURE sgenerar_periodo (par$cod_delegacion   IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_planta       IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_sector       IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_usuario      IN     gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura      IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$info_ejecucion   IN     gepr_pcomon_###VERSION###.tipo$desc_,
                              par$validaciones        OUT SYS_REFCURSOR,
                              par$cod_ejecucion       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    const$nombre_func   CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sgenerar_periodo';

    /* variables locales */
    var$gmt_zero                 gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$inicio                   gepr_pcomon_###VERSION###.tipo$gmt_ := SYSTIMESTAMP;
    var$registros                gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                      VARCHAR2 (4000);

    var$oid_periodo              gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_maquina              gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion        gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_maquina_mae          gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$fyh_vigencia_inicio      gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fyh_vigencia_fin         gepr_pcomon_###VERSION###.tipo$fyh_;
    var$oid_estado_periodo       gepr_pcomon_###VERSION###.tipo$cod_;

    /* Validaciones */
    var$validaciones             gepr_pcomon_###VERSION###.tipo$descs_;
    var$tvalidaciones            varchar2_array;
  BEGIN
    var$oid_periodo := NULL;

    /* === Validaciones iniciales === */
    IF par$cod_delegacion IS NULL THEN
      var$validaciones (var$validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         'GENESIS_PRODUCTO_VAL104',
                                         'validaciones_genesis_producto',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$cod_delegacion',
                                         0);
    END IF;

    IF par$cod_planta IS NULL THEN
      var$validaciones (var$validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         'GENESIS_PRODUCTO_VAL104',
                                         'validaciones_genesis_producto',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$cod_planta',
                                         0);
    END IF;

    IF par$cod_sector IS NULL THEN
      var$validaciones (var$validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         'GENESIS_PRODUCTO_VAL104',
                                         'validaciones_genesis_producto',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$cod_sector',
                                         0);
    END IF;

    IF gepr_putilidades_###VERSION###.farray_vacio_desc (var$validaciones) THEN
      BEGIN
        SELECT M.OID_MAQUINA,
               PM.OID_PLANIFICACION,
               P.FYH_VIGENCIA_INICIO,
               P.FYH_VIGENCIA_FIN
          INTO var$oid_maquina,
               var$oid_planificacion,
               var$fyh_vigencia_inicio,
               var$fyh_vigencia_fin
          FROM SAPR_TMAQUINA M
               INNER JOIN SAPR_TTIPO_MAQUINA TM ON TM.OID_TIPO_MAQUINA = M.OID_TIPO_MAQUINA
               INNER JOIN SAPR_TPLANXMAQUINA PM ON PM.OID_MAQUINA = M.OID_MAQUINA AND PM.OID_SECTOR = M.OID_SECTOR
               INNER JOIN SAPR_TPLANIFICACION P ON P.OID_PLANIFICACION = PM.OID_PLANIFICACION
               INNER JOIN GEPR_TSECTOR S ON S.OID_SECTOR = M.OID_SECTOR
         WHERE TM.COD_TIPO_MAQUINA = 'MAE' AND M.BOL_ACTIVO = 1 AND PM.BOL_ACTIVO = 1 AND S.COD_SECTOR = par$cod_sector;

        var$bol_maquina_mae := 1;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          var$oid_maquina := NULL;
          var$oid_planificacion := NULL;
          var$bol_maquina_mae := 0;
          raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, 'Error: No es maquina', TRUE);
      END;


      IF var$oid_maquina IS NOT NULL AND var$oid_planificacion IS NOT NULL THEN
        BEGIN
          /* Consulta base */
          SELECT P.OID_PERIODO
            INTO var$oid_periodo
            FROM SAPR_TPERIODO P 
            INNER JOIN SAPR_TESTADO_PERIODO EP ON EP.OID_ESTADO_PERIODO = P.OID_ESTADO_PERIODO
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
           WHERE     P.OID_PLANIFICACION = var$oid_planificacion
                 AND P.OID_MAQUINA = var$oid_maquina
                 AND EP.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND P.oid_acreditacion IS NULL
                 AND P.FYH_INICIO >= var$fyh_vigencia_inicio
                 AND (var$fyh_vigencia_fin IS NULL OR P.FYH_FIN <= var$fyh_vigencia_fin);
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            var$oid_periodo := NULL;
        END;
      END IF;

      /* === Fin de la segunda parte - Validar periodo === */


      /* === Generar perido === */
      IF var$oid_periodo IS NULL AND var$bol_maquina_mae = 1 THEN
        var$oid_periodo := SYS_GUID ();

        BEGIN
          SELECT OID_ESTADO_PERIODO
            INTO var$oid_estado_periodo
            FROM SAPR_TESTADO_PERIODO
           WHERE COD_ESTADO_PERIODO = 'AB';
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            var$oid_estado_periodo := SYS_GUID ();

            INSERT INTO SAPR_TESTADO_PERIODO (oid_estado_periodo,
                                              cod_estado_periodo,
                                              des_estado_periodo,
                                              obs_estado_periodo,
                                              gmt_creacion,
                                              des_usuario_creacion,
                                              gmt_modificacion,
                                              des_usuario_modificacion)
                 VALUES (var$oid_estado_periodo,
                         'AB',
                         'ABERTO',
                         'ABERTO',
                         var$gmt_zero,
                         par$cod_usuario,
                         var$gmt_zero,
                         par$cod_usuario);
        END;

        IF var$fyh_vigencia_fin IS NULL THEN
          var$fyh_vigencia_fin := var$fyh_vigencia_inicio + 1;
        END IF;

        INSERT INTO SAPR_TPERIODO (oid_periodo,
                                   oid_planificacion,
                                   oid_maquina,
                                   oid_acreditacion,
                                   oid_estado_periodo,
                                   fyh_inicio,
                                   fyh_fin,
                                   gmt_creacion,
                                   des_usuario_creacion,
                                   gmt_modificacion,
                                   des_usuario_modificacion)
             VALUES (var$oid_periodo,
                     var$oid_planificacion,
                     var$oid_maquina,
                     NULL,
                     var$oid_estado_periodo,
                     var$fyh_vigencia_inicio,
                     var$fyh_vigencia_fin,
                     var$gmt_zero,
                     par$cod_usuario,
                     var$gmt_zero,
                     par$cod_usuario);
      END IF;
    ELSE
      var$tvalidaciones := gepr_putilidades_###VERSION###.fconverter_array_desc_table (par$table_varchar2 => var$validaciones);

      OPEN par$validaciones FOR
        SELECT SUBSTR (C.COLUMN_VALUE, 0, 6) CODIGO, SUBSTR (C.COLUMN_VALUE, 7) DESCRICION
          FROM TABLE (var$tvalidaciones) C
         WHERE C.COLUMN_VALUE IS NOT NULL;
    END IF;

    /* ===  Commit === */
    COMMIT;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$codFuncionalidad || '.' || const$nombre_func,
                                                      var$inserts,
                                                      var$updates,
                                                      var$deletes,
                                                      var$merges,
                                                      var$selects,
                                                      var$registros,
                                                      var$inicio,
                                                      NULL,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$codFuncionalidad || '.' || const$nombre_func,
                                                        var$inserts,
                                                        var$updates,
                                                        var$deletes,
                                                        var$merges,
                                                        var$selects,
                                                        var$registros,
                                                        var$inicio,
                                                        NULL,
                                                        'R',
                                                        par$info_ejecucion,
                                                        var$err,
                                                        par$cod_usuario,
                                                        par$cod_ejecucion);

      raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, 'Error: ' || const$codFuncionalidad || '.' || const$nombre_func, TRUE);
  END sgenerar_periodo;

  /* Relacionar periodo */
  PROCEDURE srelacionar_periodo (par$oid_llamada      IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_documento    IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_periodo      IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$collection_id    IN     gepr_pcomon_###VERSION###.tipo$cod_ := '',
                                 par$cod_usuario      IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$cod_cultura      IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$info_ejecucion   IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                 par$hacer_commit     IN     gepr_pcomon_###VERSION###.tipo$nbol_,
                                 par$validaciones        OUT gepr_pcomon_###VERSION###.tipo$descs_,
                                 par$cod_ejecucion       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.srelacionar_periodo';

    /* variables locales */
    var$inicio                   gepr_pcomon_###VERSION###.tipo$gmt_ := SYSTIMESTAMP;
    var$registros                gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                      VARCHAR2 (4000);
    var$cod_estado               gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_tipo_calculo         gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_periodo         gepr_pcomon_###VERSION###.tipo$cod_;
    var$collection_id            gepr_pcomon_###VERSION###.tipo$cod_;
    /* Validaciones */
    var$validaciones_int         gepr_pcomon_###VERSION###.tipo$descs_;
    var$oid_periodo              gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada   => par$oid_llamada,

                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: '||par$oid_llamada||', oid_documento: '||par$oid_documento ||', oid_periodo: '||par$oid_periodo||
                                                    'collection_id: ' || par$collection_id ||
                                                   ', cod_usuario: '||par$cod_usuario||', cod_cultura: '||par$cod_cultura ||', info_ejecucion: '||par$info_ejecucion
                                                   ||', hacer_commit: '||par$hacer_commit,
                            par$cod_identificador   => '');
    /* === Validaciones iniciales === */
    IF par$oid_documento IS NULL THEN
      par$validaciones (par$validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         'GENESIS_PRODUCTO_VAL104',
                                         'validaciones_genesis_producto',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$oid_documento',
                                         0);
    ELSE
      BEGIN
        SELECT COD_ESTADO
          INTO var$cod_estado
          FROM SAPR_TDOCUMENTO
         WHERE OID_DOCUMENTO = par$oid_documento;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          par$validaciones (par$validaciones.COUNT) :=
            gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                             'GENESIS_PRODUCTO_VAL129',
                                             'validaciones_genesis_producto',
                                             gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                             par$oid_documento,
                                             0);
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada   => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                             'GENESIS_PRODUCTO_VAL129',
                                             'validaciones_genesis_producto',
                                             gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                             par$oid_documento,
                                             0),
                            par$cod_identificador   => '');
      END;
    END IF;

    IF par$oid_periodo IS NULL THEN
      par$validaciones (par$validaciones.COUNT) :=
        gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         'GENESIS_PRODUCTO_VAL104',
                                         'validaciones_genesis_producto',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$oid_periodo',
                                         0);
       SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada   => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     =>  gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                         'GENESIS_PRODUCTO_VAL104',
                                         'validaciones_genesis_producto',
                                         gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                         'par$oid_periodo',
                                         0),
                            par$cod_identificador   => '');
    END IF;

    IF gepr_putilidades_###VERSION###.farray_vacio_desc (par$validaciones) THEN
      /* === Inicio de la terceira parte - Relacionar periodos === */

      sins_tperiodoxdocumento (par$oid_documento => par$oid_documento, par$oid_periodo => par$oid_periodo, par$cod_usuario => par$cod_usuario);

      /* === Fin de la terceira parte - Relacionar periodos === */

      /* === Inicio de la cuarta parte - Sumar los valores relacionados al documento y grabarlos en las tablas de valores de perÃ­odos === */

      var$oid_tipo_calculo :=
        frecuperar_tipo_calculo (par$oid_documento => par$oid_documento, par$cod_cultura => par$cod_cultura, par$validaciones => var$validaciones_int);


      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                          par$des_origen      => const$des_origen,
                          par$des_version     => const$version,
                          par$des_detalle     =>  'Se recupera var$oid_tipo_calculo: ' || var$oid_tipo_calculo,
                          par$cod_identificador   => '');

      /* Busco el tipo de período y su collection id */
      BEGIN
        SELECT TIPE.COD_TIPO_PERIODO , PERI.COD_COLLECTION_ID 
        INTO var$cod_tipo_periodo, var$collection_id
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO
        WHERE PERI.OID_PERIODO = par$oid_periodo;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          var$cod_tipo_periodo := NULL;
          var$collection_id := NULL;
      END;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     =>  'Se recupera var$cod_tipo_periodo: ' || var$cod_tipo_periodo,
                    par$cod_identificador   => '');

      IF var$validaciones_int.COUNT = 0 THEN

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$des_origen,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Previo a procedure scalculo_efectivo',
                  par$cod_identificador   => '');
        scalculo_efectivo (par$oid_documento      => par$oid_documento,
                           par$oid_tipo_calculo   => var$oid_tipo_calculo,
                           par$cod_tipo_periodo   => var$cod_tipo_periodo,
                           par$cod_usuario        => par$cod_usuario,
                           par$bol_sumar          => 1);

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$des_origen,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Previo a procedure scalculo_medio_pago',
                          par$cod_identificador   => '');
        scalculo_medio_pago (par$oid_documento      => par$oid_documento,
                             par$oid_tipo_calculo   => var$oid_tipo_calculo,
                             par$cod_tipo_periodo   => var$cod_tipo_periodo,
                             par$cod_usuario        => par$cod_usuario,
                             par$bol_sumar          => 1);
        var$oid_periodo := par$oid_periodo;
        /*Aca verificar si actualizo o no el collection id del periodo*/
        IF par$collection_id IS NOT NULL AND var$collection_id IS NULL  THEN
                sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                      par$oid_periodo               => var$oid_periodo,
                      par$oid_planificacion         => NULL,
                      par$oid_maquina               => NULL,
                      par$oid_estado_periodo        => NULL,
                      par$fyh_inicio                => NULL,
                      par$fyh_fin                   => NULL,
                      par$oid_tipo_periodo          => NULL,
                      par$collection_id             => par$collection_id,
                      par$cod_periodo_confirmacion  => NULL,
                      par$cod_usuario               => par$cod_usuario);
        END IF;

      END IF;

      IF var$validaciones_int.COUNT > 0 THEN
        FOR idx IN var$validaciones_int.FIRST .. var$validaciones_int.LAST LOOP
          par$validaciones (par$validaciones.COUNT) := var$validaciones_int (idx);
        END LOOP;
      END IF;
    /* === Fin de la cuarta parte - Sumar los valores relacionados al documento y grabarlos en las tablas de valores de perÃ­odos === */

    END IF;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,
              par$des_detalle     =>  'IF par$hacer_commit > 0 THEN' || chr(13)  ||
                                      'par$hacer_commit: ' || par$hacer_commit,
              par$cod_identificador   => '');
    IF par$hacer_commit = 1 THEN
      /* ===  Commit === */
      COMMIT;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$des_origen,
                                                        var$inserts,
                                                        var$updates,
                                                        var$deletes,
                                                        var$merges,
                                                        var$selects,
                                                        var$registros,
                                                        var$inicio,
                                                        NULL,
                                                        'C',
                                                        par$info_ejecucion,
                                                        var$err,
                                                        par$cod_usuario,
                                                        par$cod_ejecucion);
    END IF;
      IF par$validaciones IS NOT NULL AND par$validaciones.COUNT > 0 THEN
        FOR idx IN  par$validaciones.first ..  par$validaciones.last LOOP 
               SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => idx || ' - ' || par$validaciones(idx),
                            par$cod_identificador   => '');
         END LOOP;
     END IF;
  /* ===  Tratar excepciones - Rollback === */
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$des_origen,
                                                        var$inserts,
                                                        var$updates,
                                                        var$deletes,
                                                        var$merges,
                                                        var$selects,
                                                        var$registros,
                                                        var$inicio,
                                                        NULL,
                                                        'R',
                                                        par$info_ejecucion,
                                                        var$err,
                                                        par$cod_usuario,
                                                        par$cod_ejecucion);

       SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada   => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'EXCEPTION srelacionar_periodo: ' || SQLERRM,
                            par$cod_identificador   => '');
      raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, 'Error: ' || const$des_origen, TRUE);
  END srelacionar_periodo;

  /* Relacionar periodo manual */
  PROCEDURE srelacionar_periodo_manual (par$oid_llamada             IN    gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_documento           IN    gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$bol_generar_periodos    IN    gepr_pcomon_###VERSION###.tipo$nbol_) AS

    var$oid_periodo              gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_maquina              gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion        gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_planificacion   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_canal_mae            gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_subcanal_mae         gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sector_mae           gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_pto_servicio         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa               gepr_pcomon_###VERSION###.tipo$oid_;
    var$fyh_gestion_mae          gepr_pcomon_###VERSION###.tipo$fyh_;
    var$cod_cultura              gepr_pcomon_###VERSION###.tipo$cod_ := 'es';
    var$usuario                  gepr_pcomon_###VERSION###.tipo$usr_ := 'RELACIONAR_PERIODO_MANUAL';
    var$info_ejecucion           gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_ejecucion            gepr_pcomon_###VERSION###.tipo$nel_;
    var$des_validaciones         gepr_pcomon_###VERSION###.tipo$descs_;
    var$cod_validaciones         gepr_pcomon_###VERSION###.tipo$cods_;
  BEGIN
  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => 'srelacionar_periodo_manual',
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: ' || par$oid_llamada || ', oid_documento: ' || par$oid_documento ||', bol_generar_periodos: '||par$bol_generar_periodos,
                            par$cod_identificador   => '');
    SELECT CA.COD_CANAL,
           SC.COD_SUBCANAL,
           D.FYH_GESTION,
           S.COD_SECTOR,
           C.OID_PTO_SERVICIO,
           EFXDOC.OID_DIVISA
      INTO var$cod_canal_mae,
           var$cod_subcanal_mae,
           var$fyh_gestion_mae,
           var$cod_sector_mae,
           var$oid_pto_servicio,
           var$oid_divisa
      FROM SAPR_TDOCUMENTO D
           INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = D.OID_CUENTA_DESTINO
           INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
           INNER JOIN GEPR_TCANAL CA ON CA.OID_CANAL = SC.OID_CANAL
           INNER JOIN GEPR_TSECTOR S ON S.OID_SECTOR = D.OID_SECTOR_DESTINO
           LEFT JOIN SAPR_TEFECTIVOXDOCUMENTO EFXDOC ON d.OID_DOCUMENTO = EFXDOC.OID_DOCUMENTO
     WHERE D.OID_DOCUMENTO = par$oid_documento AND ROWNUM = 1;

    sverificar_periodo (par$oid_llamada              => par$oid_llamada,
                        par$cod_canal                => var$cod_canal_mae,
                        par$cod_subcanal             => var$cod_subcanal_mae,
                        par$oid_divisa               => var$oid_divisa,
                        par$cod_sector               => var$cod_sector_mae,
                        par$oid_pto_servicio         => var$oid_pto_servicio,
                        par$cod_ajeno                => NULL,
                        par$fyh_gestion              => var$fyh_gestion_mae,
                        par$bol_generar_periodos     => par$bol_generar_periodos,
                        par$cod_usuario              => var$usuario,
                        par$cod_cultura              => var$cod_cultura,
                        par$info_ejecucion           => var$info_ejecucion,
                        par$oid_maquina              => var$oid_maquina,
                        par$oid_planificacion        => var$oid_planificacion,
                        par$oid_periodo              => var$oid_periodo,
                        par$cod_tipo_planificacion   => var$cod_tipo_planificacion,
                        par$cod_validaciones         => var$cod_validaciones,
                        par$des_validaciones         => var$des_validaciones,
                        par$cod_ejecucion            => var$cod_ejecucion);

    IF var$oid_periodo IS NOT NULL THEN
      srelacionar_periodo (par$oid_llamada      => par$oid_llamada,
                           par$oid_documento    => par$oid_documento,
                           par$oid_periodo      => var$oid_periodo,
                           par$cod_usuario      => var$usuario,
                           par$cod_cultura      => var$cod_cultura,
                           par$info_ejecucion   => var$info_ejecucion,
                           par$hacer_commit     => 0,
                           par$validaciones     => var$des_validaciones,
                           par$cod_ejecucion    => var$cod_ejecucion);
    END IF;
  END srelacionar_periodo_manual;


  /* Regresa un Valor u Otro de Acuerdo al Boolean */
  FUNCTION fiif
    (par$statement NUMBER, par$return_1 VARCHAR2, par$return_2 VARCHAR2)
    RETURN VARCHAR2
  IS
  BEGIN
    IF (par$statement = 1) THEN
      RETURN par$return_1;
    ELSE
      RETURN par$return_2;
    END IF;
  END fiif;

  /* Calculo Efectivo */
  PROCEDURE scalculo_efectivo (par$oid_documento      IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$oid_tipo_calculo   IN gepr_pcomon_###VERSION###.tipo$oid_,
                               par$cod_tipo_periodo   IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$bol_sumar          IN gepr_pcomon_###VERSION###.tipo$nbol_) AS
    var$gmt_zero                VARCHAR2 (50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$bol_agr_subcanal        NUMBER(1) := 0;
    var$bol_agr_pto_servicio    NUMBER(1) := 0;
    var$bol_agr_fec_contable    NUMBER(1) := 0;
    var$insert_o_update         VARCHAR2(6000) := '';
  BEGIN

    /* Recupera las Opciones de Agrupacion de Valores */
    SELECT NVL(PLAN.BOL_AGR_SUBCANAL, 0), NVL(PLAN.BOL_AGR_PTO_SERVICIO, 0), NVL(PLAN.BOL_AGR_FEC_CONTABLE, 0)
    INTO var$bol_agr_subcanal, var$bol_agr_pto_servicio, var$bol_agr_fec_contable
    FROM SAPR_TPLANIFICACION PLAN
    INNER JOIN SAPR_TPERIODO PERI ON PLAN.OID_PLANIFICACION = PERI.OID_PLANIFICACION
    INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = par$cod_tipo_periodo
    INNER JOIN SAPR_TPERIODOXDOCUMENTO PEDO ON PERI.OID_PERIODO = PEDO.OID_PERIODO
    WHERE PEDO.OID_DOCUMENTO = par$oid_documento;

    /* Arma el Insert o Update */
    var$insert_o_update := q'[
      MERGE INTO SAPR_TCALCULO_EFECTIVO Q1
           USING (SELECT A.OID_TRANSACCION_EFECTIVO,
                         A.OID_CUENTA_SALDO,
                         A.OID_DIVISA,
                         A.OID_DENOMINACION,
                         A.COD_NIVEL_DETALLE,
                         A.OID_CALIDAD,
                         A.COD_TIPO_EFECTIVO_TOTAL,
                         A.NUM_IMPORTE,
                         A.NEL_CANTIDAD,
                         A.BOL_DISPONIBLE,
                         A.OID_UNIDAD_MEDIDA,
                         A.BOL_BLOQUEADO,
                         PD.OID_PERIODO,
                         ]' || fiif(var$bol_agr_subcanal, ' C.OID_SUBCANAL, ', ' NULL OID_SUBCANAL, ') || q'[
                         ]' || fiif(var$bol_agr_pto_servicio, ' C.OID_PTO_SERVICIO, ', ' NULL OID_PTO_SERVICIO, ') || q'[
                         ]' || fiif(var$bol_agr_fec_contable, ' TRUNC(FN_GMT_DELEGACION_###VERSION###(P.OID_DELEGACION, D.FEC_CONTABLE)) FEC_CONTABLE ', ' NULL FEC_CONTABLE ') || q'[
                    FROM SAPR_TTRANSACCION_EFECTIVO A
                         INNER JOIN SAPR_TPERIODOXDOCUMENTO PD ON PD.OID_DOCUMENTO = A.OID_DOCUMENTO
                         INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PD.OID_PERIODO
                         INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = ']' || par$cod_tipo_periodo || q'['
                         INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = A.OID_CUENTA_SALDO
                         INNER JOIN GEPR_TSECTOR S ON C.OID_SECTOR = S.OID_SECTOR
                         INNER JOIN GEPR_TPLANTA P ON S.OID_PLANTA = P.OID_PLANTA
                         INNER JOIN SAPR_TDOCUMENTO D ON PD.OID_DOCUMENTO = D.OID_DOCUMENTO
                   WHERE A.OID_DOCUMENTO = ']' || par$oid_documento || q'[' AND A.NUM_IMPORTE <> 0) Q2
              ON (    Q1.OID_PERIODO = Q2.OID_PERIODO
                  AND Q1.OID_DIVISA = Q2.OID_DIVISA
                  AND NVL (Q1.OID_DENOMINACION, '-') = NVL (Q2.OID_DENOMINACION, '-')
                  AND NVL (Q1.COD_NIVEL_DETALLE, '-') = NVL (Q2.COD_NIVEL_DETALLE, '-')
                  AND Q1.BOL_BLOQUEADO = Q2.BOL_BLOQUEADO
                  AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE
                  AND NVL (Q1.OID_CALIDAD, '-') = NVL (Q2.OID_CALIDAD, '-')
                  AND NVL (Q1.COD_TIPO_EFECTIVO_TOTAL, '-') = NVL (Q2.COD_TIPO_EFECTIVO_TOTAL, '-')
                  AND NVL (Q1.OID_UNIDAD_MEDIDA, '-') = NVL (Q2.OID_UNIDAD_MEDIDA, '-')
                  AND NVL (Q1.oid_tipo_calculo, '-') = NVL (']' || par$oid_tipo_calculo || q'[', '-')
                  AND NVL (Q1.OID_SUBCANAL, '-') = NVL (Q2.OID_SUBCANAL, '-')
                  AND NVL (Q1.OID_PTO_SERVICIO, '-') = NVL (Q2.OID_PTO_SERVICIO, '-')
                  AND TRUNC(NVL (Q1.FEC_CONTABLE, TO_DATE('0001-01-01', 'YYYY-MM-DD'))) = NVL (Q2.FEC_CONTABLE, TRUNC(TO_DATE('0001-01-01', 'YYYY-MM-DD')))
                  )
      WHEN MATCHED THEN
        UPDATE SET Q1.NUM_IMPORTE = NVL (Q1.NUM_IMPORTE, 0) ]' || fiif(NVL(par$bol_sumar, 0), '+', '-') || q'[ NVL (Q2.NUM_IMPORTE, 0),
                   Q1.NEL_CANTIDAD = NVL (Q1.NEL_CANTIDAD, 0) ]' || fiif(NVL(par$bol_sumar, 0), '+', '-') || q'[ NVL (Q2.NEL_CANTIDAD, 0),
                   Q1.GMT_MODIFICACION = ']' || var$gmt_zero || q'[',
                   Q1.DES_USUARIO_MODIFICACION = ']' || par$cod_usuario || q'['
      WHEN NOT MATCHED THEN
        INSERT     (Q1.OID_CALCULO_EFECTIVO,
                    Q1.OID_PERIODO,
                    Q1.OID_DIVISA,
                    Q1.OID_DENOMINACION,
                    Q1.OID_CALIDAD,
                    Q1.OID_UNIDAD_MEDIDA,
                    Q1.OID_TIPO_CALCULO,
                    Q1.COD_NIVEL_DETALLE,
                    Q1.COD_TIPO_EFECTIVO_TOTAL,
                    Q1.BOL_DISPONIBLE,
                    Q1.BOL_BLOQUEADO,
                    Q1.NUM_IMPORTE_ACUMULADO,
                    Q1.NEL_CANTIDAD_ACUMULADO,
                    Q1.NUM_IMPORTE,
                    Q1.NEL_CANTIDAD,
                    Q1.GMT_CREACION,
                    Q1.DES_USUARIO_CREACION,
                    Q1.GMT_MODIFICACION,
                    Q1.DES_USUARIO_MODIFICACION,
                    Q1.OID_SUBCANAL,
                    Q1.OID_PTO_SERVICIO,
                    Q1.FEC_CONTABLE
                    )
            VALUES (SYS_GUID (),
                    Q2.OID_PERIODO,
                    Q2.OID_DIVISA,
                    Q2.OID_DENOMINACION,
                    Q2.OID_CALIDAD,
                    Q2.OID_UNIDAD_MEDIDA,
                    ']' || par$oid_tipo_calculo || q'[',
                    Q2.COD_NIVEL_DETALLE,
                    Q2.COD_TIPO_EFECTIVO_TOTAL,
                    Q2.BOL_DISPONIBLE,
                    Q2.BOL_BLOQUEADO,
                    0,
                    0,
                    Q2.NUM_IMPORTE,
                    Q2.NEL_CANTIDAD,
                    ']' || var$gmt_zero|| q'[',
                    ']' || par$cod_usuario|| q'[',
                    ']' || var$gmt_zero|| q'[',
                    ']' || par$cod_usuario|| q'[',
                    Q2.OID_SUBCANAL,
                    Q2.OID_PTO_SERVICIO,
                    Q2.FEC_CONTABLE
                    )]';




    /* Ejecuta el Comando */
    EXECUTE IMMEDIATE var$insert_o_update;

  END scalculo_efectivo;

 /* Calculo Medio Pago */
  PROCEDURE scalculo_medio_pago (par$oid_documento      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_tipo_calculo   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_tipo_periodo   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$usr_,
                                 par$bol_sumar          IN gepr_pcomon_###VERSION###.tipo$nbol_) AS
    var$gmt_zero   VARCHAR2 (50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$bol_agr_subcanal        NUMBER(1) := 0;
    var$bol_agr_pto_servicio    NUMBER(1) := 0;
    var$bol_agr_fec_contable    NUMBER(1) := 0;
    var$insert_o_update         VARCHAR2(6000) := '';
  BEGIN

    /* Recupera las Opciones de Agrupacion de Valores */
    SELECT NVL(PLAN.BOL_AGR_SUBCANAL, 0), NVL(PLAN.BOL_AGR_PTO_SERVICIO, 0), NVL(PLAN.BOL_AGR_FEC_CONTABLE, 0)
    INTO var$bol_agr_subcanal, var$bol_agr_pto_servicio, var$bol_agr_fec_contable
    FROM SAPR_TPLANIFICACION PLAN
    INNER JOIN SAPR_TPERIODO PERI ON PLAN.OID_PLANIFICACION = PERI.OID_PLANIFICACION
    INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = par$cod_tipo_periodo
    INNER JOIN SAPR_TPERIODOXDOCUMENTO PEDO ON PERI.OID_PERIODO = PEDO.OID_PERIODO
    WHERE PEDO.OID_DOCUMENTO = par$oid_documento;

    /* Arma el Insert o Update */
    var$insert_o_update := q'[
      MERGE INTO SAPR_TCALCULO_MEDIO_PAGO Q1
           USING (SELECT A.OID_TRANSACCION_MEDIO_PAGO,
                         A.OID_CUENTA_SALDO,
                         A.OID_MEDIO_PAGO,
                         A.OID_DIVISA,
                         A.COD_TIPO_MEDIO_PAGO,
                         A.COD_NIVEL_DETALLE,
                         A.BOL_DISPONIBLE,
                         A.NEL_CANTIDAD,
                         A.NUM_IMPORTE,
                         A.OID_UNIDAD_MEDIDA,
                         A.BOL_BLOQUEADO,
                         PD.OID_PERIODO,
                         ]' || fiif(var$bol_agr_subcanal, ' C.OID_SUBCANAL, ', ' NULL OID_SUBCANAL, ') || q'[
                         ]' || fiif(var$bol_agr_pto_servicio, ' C.OID_PTO_SERVICIO, ', ' NULL OID_PTO_SERVICIO, ') || q'[
                         ]' || fiif(var$bol_agr_fec_contable, ' TRUNC(FN_GMT_DELEGACION_###VERSION###(P.OID_DELEGACION, D.FEC_CONTABLE)) FEC_CONTABLE ', ' NULL FEC_CONTABLE ') || q'[
                    FROM SAPR_TTRANSACCION_MEDIO_PAGO A
                         INNER JOIN SAPR_TPERIODOXDOCUMENTO PD ON PD.OID_DOCUMENTO = A.OID_DOCUMENTO
                         INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PD.OID_PERIODO
                         INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = ']' || par$cod_tipo_periodo || q'['
                         INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = A.OID_CUENTA_SALDO
                         INNER JOIN GEPR_TSECTOR S ON C.OID_SECTOR = S.OID_SECTOR
                         INNER JOIN GEPR_TPLANTA P ON S.OID_PLANTA = P.OID_PLANTA
                         INNER JOIN SAPR_TDOCUMENTO D ON PD.OID_DOCUMENTO = D.OID_DOCUMENTO
                   WHERE A.OID_DOCUMENTO = ']' || par$oid_documento || q'[' AND A.NUM_IMPORTE <> 0) Q2
              ON (    Q1.OID_PERIODO = Q2.OID_PERIODO
                  AND Q1.OID_DIVISA = Q2.OID_DIVISA
                  AND Q1.OID_SUBCANAL = Q2.OID_SUBCANAL
                  AND NVL (Q1.COD_NIVEL_DETALLE, '-') = NVL (Q2.COD_NIVEL_DETALLE, '-')
                  AND Q1.BOL_BLOQUEADO = Q2.BOL_BLOQUEADO
                  AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE
                  AND NVL (Q1.COD_TIPO_MEDIO_PAGO, '-') = NVL (Q2.COD_TIPO_MEDIO_PAGO, '-')
                  AND NVL (Q1.OID_UNIDAD_MEDIDA, '-') = NVL (Q2.OID_UNIDAD_MEDIDA, '-')
                  AND NVL (Q1.oid_tipo_calculo, '-') = NVL (']' || par$oid_tipo_calculo || q'[', '-')
                  AND NVL (Q1.OID_SUBCANAL, '-') = NVL (Q2.OID_SUBCANAL, '-')
                  AND NVL (Q1.OID_PTO_SERVICIO, '-') = NVL (Q2.OID_PTO_SERVICIO, '-')
                  AND TRUNC(NVL (Q1.FEC_CONTABLE, TO_DATE('0001-01-01', 'YYYY-MM-DD'))) = NVL (Q2.FEC_CONTABLE, TRUNC(TO_DATE('0001-01-01', 'YYYY-MM-DD')))
                  )
      WHEN MATCHED THEN
        UPDATE SET Q1.NUM_IMPORTE = NVL (Q1.NUM_IMPORTE, 0) ]' || fiif(NVL(par$bol_sumar, 0), '+', '-') || q'[ NVL (Q2.NUM_IMPORTE, 0),
                   Q1.NEL_CANTIDAD = NVL (Q1.NEL_CANTIDAD, 0) ]' || fiif(NVL(par$bol_sumar, 0), '+', '-') || q'[ NVL (Q2.NEL_CANTIDAD, 0),
                   Q1.GMT_MODIFICACION = ']' || var$gmt_zero || q'[',
                   Q1.DES_USUARIO_MODIFICACION = ']' || par$cod_usuario || q'['
      WHEN NOT MATCHED THEN
        INSERT     (Q1.OID_CALCULO_MEDIO_PAGO,
                    Q1.OID_PERIODO,
                    Q1.OID_DIVISA,
                    Q1.OID_MEDIO_PAGO,
                    Q1.COD_TIPO_MEDIO_PAGO,
                    Q1.OID_UNIDAD_MEDIDA,
                    Q1.OID_TIPO_CALCULO,
                    Q1.COD_NIVEL_DETALLE,
                    Q1.BOL_DISPONIBLE,
                    Q1.BOL_BLOQUEADO,
                    Q1.NUM_IMPORTE_ACUMULADO,
                    Q1.NEL_CANTIDAD_ACUMULADO,
                    Q1.NUM_IMPORTE,
                    Q1.NEL_CANTIDAD,
                    Q1.GMT_CREACION,
                    Q1.DES_USUARIO_CREACION,
                    Q1.GMT_MODIFICACION,
                    Q1.DES_USUARIO_MODIFICACION,
                    Q1.OID_SUBCANAL,
                    Q1.OID_PTO_SERVICIO,
                    Q1.FEC_CONTABLE
                    )
            VALUES (SYS_GUID (),
                    Q2.OID_PERIODO,
                    Q2.OID_DIVISA,
                    Q2.OID_MEDIO_PAGO,
                    Q2.COD_TIPO_MEDIO_PAGO,
                    Q2.OID_UNIDAD_MEDIDA,
                    ']' || par$oid_tipo_calculo || q'[',
                    Q2.COD_NIVEL_DETALLE,
                    Q2.BOL_DISPONIBLE,
                    Q2.BOL_BLOQUEADO,
                    0,
                    0,
                    Q2.NUM_IMPORTE,
                    Q2.NEL_CANTIDAD,
                    ']' || var$gmt_zero|| q'[',
                    ']' || par$cod_usuario|| q'[',
                    ']' || var$gmt_zero|| q'[',
                    ']' || par$cod_usuario|| q'[',
                    Q2.OID_SUBCANAL,
                    Q2.OID_PTO_SERVICIO,
                    Q2.FEC_CONTABLE
                    )]';

    /* Ejecuta el Comando */
    EXECUTE IMMEDIATE var$insert_o_update;

  END scalculo_medio_pago;

  /* Executa a acao contabil de efectivo do documento */
  FUNCTION frecuperar_tipo_calculo (par$oid_documento   IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_cultura     IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$validaciones       OUT gepr_pcomon_###VERSION###.tipo$descs_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ AS
    var$oid_tipo_calculo    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_delegacion      gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_canal_destino   gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN
    BEGIN
      SELECT CAN.COD_CANAL, P.OID_DELEGACION
        INTO var$cod_canal_destino, var$oid_delegacion
        FROM SAPR_TDOCUMENTO D
             INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = D.OID_CUENTA_SALDO_DESTINO
             INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
             INNER JOIN GEPR_TCANAL CAN ON CAN.OID_CANAL = SC.OID_CANAL
             INNER JOIN GEPR_TSECTOR S ON S.OID_SECTOR = C.OID_SECTOR
             INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
       WHERE D.OID_DOCUMENTO = par$oid_documento;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        par$validaciones (par$validaciones.COUNT) := 'Documento invalido';
    END;

    IF var$cod_canal_destino <> 'SF' THEN
      BEGIN
        SELECT C.OID_TIPO_CALCULO
          INTO var$oid_tipo_calculo
          FROM SAPR_TTIPO_CALCULO C
         WHERE C.COD_TIPO_CALCULO = 'CONTABLE';
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          par$validaciones (par$validaciones.COUNT) :=
            gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                             'GENESIS_PRODUCTO_VAL133',
                                             'validaciones_genesis_producto',
                                             gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                             'CONTABLE',
                                             0);
      END;
    ELSE
      BEGIN
        SELECT C.OID_TIPO_CALCULO
          INTO var$oid_tipo_calculo
          FROM SAPR_TTIPO_CALCULO C
         WHERE C.COD_TIPO_CALCULO = 'FISICO';
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          par$validaciones (par$validaciones.COUNT) :=
            gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                             'GENESIS_PRODUCTO_VAL133',
                                             'validaciones_genesis_producto',
                                             gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                             'FISICO',
                                             0);
      END;
    END IF;

    RETURN var$oid_tipo_calculo;
  END;

 PROCEDURE srelacionar_documentos_mae (par$oid_llamada        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$fechagestion       IN gepr_pcomon_###VERSION###.tipo$fyh_, 
                                       par$cod_identificacion IN gepr_pcomon_###VERSION###.tipo$desc_) IS

    const$nombre_func      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.srelacionar_documentos_mae';
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                  par$des_origen          => const$nombre_func,
                                  par$des_version         => const$version,                            
                                  par$des_detalle         => 'oid_llamada: ' || par$oid_llamada || ', fecha de gestion: ' || TO_CHAR(par$fechagestion, 'YYYY-MM-DD HH24:MI:SS') || ', cod_identificacion: ' ||par$cod_identificacion,
                                  par$cod_identificador   => '');

          SELECT COUNT(1)
              INTO var$registros
              FROM SAPR_TDOCUMENTO DOCU
                      INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO
                      INNER JOIN SAPR_TCUENTA CUEN ON DOCU.OID_CUENTA_DESTINO = CUEN.OID_CUENTA
                      INNER JOIN SAPR_TMAQUINA MAQU ON DOCU.OID_SECTOR_DESTINO = MAQU.OID_SECTOR
                      LEFT JOIN SAPR_TPERIODOXDOCUMENTO PEDO ON DOCU.OID_DOCUMENTO = PEDO.OID_DOCUMENTO
                    WHERE DOCU.COD_ESTADO = 'AC' AND DOCU.BOL_ACREDITADO = 0
                      AND DOCU.FYH_GESTION >= par$fechagestion
                      AND MAQU.COD_IDENTIFICACION = par$cod_identificacion
                      AND PEDO.OID_PERIODO IS NULL
                    ORDER BY DOCU.FYH_GESTION;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                  par$des_origen          => const$nombre_func,
                                  par$des_version         => const$version,                            
                                  par$des_detalle         => 'Cantidad registros: ' || var$registros,
                                  par$cod_identificador   => '');

    FOR rec$doc IN (SELECT DOCU.OID_DOCUMENTO
                    FROM SAPR_TDOCUMENTO DOCU
                      INNER JOIN SAPR_TFORMULARIO FORM ON DOCU.OID_FORMULARIO = FORM.OID_FORMULARIO
                      INNER JOIN SAPR_TCUENTA CUEN ON DOCU.OID_CUENTA_DESTINO = CUEN.OID_CUENTA
                      INNER JOIN SAPR_TMAQUINA MAQU ON DOCU.OID_SECTOR_DESTINO = MAQU.OID_SECTOR
                      LEFT JOIN SAPR_TPERIODOXDOCUMENTO PEDO ON DOCU.OID_DOCUMENTO = PEDO.OID_DOCUMENTO
                    WHERE DOCU.COD_ESTADO = 'AC' AND DOCU.BOL_ACREDITADO = 0
                      AND DOCU.FYH_GESTION >= par$fechagestion
                      AND MAQU.COD_IDENTIFICACION = par$cod_identificacion
                      AND PEDO.OID_PERIODO IS NULL
                    ORDER BY DOCU.FYH_GESTION) LOOP
          srelacionar_periodo_manual (par$oid_llamada   => par$oid_llamada,
                                      par$oid_documento => rec$doc.oid_documento,
                                      par$bol_generar_periodos     => 0);
    END LOOP;
  END srelacionar_documentos_mae;

  /* Relacionar o Quitar movimientos en un periodo */
  PROCEDURE srelacionar_mov_periodo (par$abol_baja            IN     gepr_pcomon_###VERSION###.tipo$nbols_,
                                     par$abol_codigo_simple   IN     gepr_pcomon_###VERSION###.tipo$nbols_,
                                     par$acod_movimiento      IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$rc_validaciones         OUT SYS_REFCURSOR,
                                     par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$cod_) IS
    const$nombre_func   CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srelacionar_mov_periodo';

    /* LOG */
    var$inicio                   TIMESTAMP := SYSTIMESTAMP;
    var$registros                gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                      VARCHAR2 (4000);

    /* Movimientos */
    CURSOR cur$movimientos (par$cod_calificador IN gepr_pcomon_###VERSION###.tipo$cod_) IS
      SELECT OID_CAMPO1 OID_DOCUMENTO,
             COD_CAMPO2 OID_PERIODO,
             COD_CAMPO3 COD_EXTERNO,
             COD_CAMPO4 OID_TIPO_CALCULO
        FROM SAPR_GTT_TAUXILIAR D
       WHERE COD_CALIFICADOR = par$cod_calificador;
  BEGIN
    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR IN ('MOVIMIENTOS_QUITAR', 'MOVIMIENTOS_RELACIONAR', 'VALIDACIONES') ;

    var$deletes := var$deletes + SQL%ROWCOUNT;

    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR
      SELECT NULL CODIGO
        FROM DUAL
       WHERE 1 <> 1;

    /* Llamar la procedure responsable por validar las informaciones */
    svalidar_mov_periodo (par$abol_baja            => par$abol_baja,
                          par$abol_codigo_simple   => par$abol_codigo_simple,
                          par$acod_movimiento      => par$acod_movimiento,
                          par$cod_cultura          => par$cod_cultura);

    /* Movimientos para quitar */
    FOR rec$movimiento IN cur$movimientos ('MOVIMIENTOS_QUITAR') LOOP


      scalculo_efectivo (par$oid_documento      => rec$movimiento.oid_documento,
                         par$oid_tipo_calculo   => rec$movimiento.oid_tipo_calculo,
                         par$cod_tipo_periodo   => 'AC',
                         par$cod_usuario        => par$cod_usuario,
                         par$bol_sumar          => 0);

      scalculo_medio_pago (par$oid_documento      => rec$movimiento.oid_documento,
                           par$oid_tipo_calculo   => rec$movimiento.oid_tipo_calculo,
                           par$cod_tipo_periodo   => 'AC',
                           par$cod_usuario        => par$cod_usuario,
                           par$bol_sumar          => 0);

      sdel_tperiodoxdocumento (par$oid_documento => rec$movimiento.oid_documento, par$oid_periodo => rec$movimiento.oid_periodo);

      DBMS_OUTPUT.put_line ('0040100001 - El movimiento fue quitado con éxito del período. [' || rec$movimiento.cod_externo || ']');

      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                      COD_CAMPO2,
                                      COD_CAMPO3,
                                      COD_CALIFICADOR)
           VALUES ('0040100001',
                   gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                    '0040100001',
                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                    NULL,
                                                    0),
                   rec$movimiento.cod_externo,
                   'VALIDACIONES');
    END LOOP rec$movimiento;

    /* Movimientos para quitar */
    FOR rec$movimiento IN cur$movimientos ('MOVIMIENTOS_RELACIONAR') LOOP
      sins_tperiodoxdocumento (par$oid_documento   => rec$movimiento.oid_documento,
                               par$oid_periodo     => rec$movimiento.oid_periodo,
                               par$cod_usuario     => par$cod_usuario);

      scalculo_efectivo (par$oid_documento      => rec$movimiento.oid_documento,
                         par$oid_tipo_calculo   => rec$movimiento.oid_tipo_calculo,
                         par$cod_tipo_periodo   => 'AC',
                         par$cod_usuario        => par$cod_usuario,
                         par$bol_sumar          => 1);

      scalculo_medio_pago (par$oid_documento      => rec$movimiento.oid_documento,
                           par$oid_tipo_calculo   => rec$movimiento.oid_tipo_calculo,
                           par$cod_tipo_periodo   => 'AC',
                           par$cod_usuario        => par$cod_usuario,
                           par$bol_sumar          => 1);

      DBMS_OUTPUT.put_line ('0040100002 - El movimiento fue relacionado con éxito al período. [' || rec$movimiento.cod_externo || ']');

      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                      COD_CAMPO2,
                                      COD_CAMPO3,
                                      COD_CALIFICADOR)
           VALUES ('0040100002',
                   gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                    '0040100002',
                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                    NULL,
                                                    0),
                   rec$movimiento.cod_externo,
                   'VALIDACIONES');
    END LOOP rec$movimiento;

    /* Validaciones */
    OPEN par$rc_validaciones FOR
      SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CAMPO3 AS COD_MOVIMIENTO
        FROM SAPR_GTT_TAUXILIAR
       WHERE COD_CALIFICADOR = 'VALIDACIONES';

    var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$codFuncionalidad || '.' || const$nombre_func,
                                                      var$inserts,
                                                      var$updates,
                                                      var$deletes,
                                                      var$merges,
                                                      var$selects,
                                                      var$registros,
                                                      var$inicio,
                                                      NULL,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$codFuncionalidad || '.' || const$nombre_func,
                                                        var$inserts,
                                                        var$updates,
                                                        var$deletes,
                                                        var$merges,
                                                        var$selects,
                                                        var$registros,
                                                        var$inicio,
                                                        NULL,
                                                        'R',
                                                        par$info_ejecucion,
                                                        var$err,
                                                        par$cod_usuario,
                                                        par$cod_ejecucion);
  END srelacionar_mov_periodo;

  /* Validar informaciones de la procedure srelacionar_mov_periodo */
  PROCEDURE svalidar_mov_periodo (par$abol_baja            IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$abol_codigo_simple   IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$acod_movimiento      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    var$existe               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$oid_maquina          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion    gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_planificacion    gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_periodo          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_estado_periodo   gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_tipo_calculo     gepr_pcomon_###VERSION###.tipo$oid_;
    var$fyh_gestion          gepr_pcomon_###VERSION###.tipo$fyh_;

    /* Movimientos */
    CURSOR cur$movimientos  IS
      SELECT OID_CAMPO1 OID_DOCUMENTO,
             COD_CAMPO2 COD_EXTERNO,
             COD_CAMPO3 OID_SECTOR,
             COD_CAMPO4 COD_CANAL,
             COD_CALIFICADOR
        FROM SAPR_GTT_TAUXILIAR D
       WHERE COD_CALIFICADOR = 'MOVIMIENTO';
  BEGIN
    /* 2040100010 - Es obligatorio informar un movimiento. */
    IF    GEPR_PUTILIDADES_###VERSION###.farray_vacio_cod (par$acod_movimiento)
       OR par$acod_movimiento.COUNT <> par$abol_baja.COUNT
       OR par$acod_movimiento.COUNT <> par$abol_codigo_simple.COUNT THEN
      DBMS_OUTPUT.put_line ('2040100010 - Es obligatorio informar un movimiento.');

      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                      COD_CAMPO2,
                                      COD_CAMPO3,
                                      COD_CALIFICADOR)
           VALUES ('2040100010',
                   gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                    '2040100010',
                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                    NULL,
                                                    0),
                   '',
                   'VALIDACIONES');
    ELSE
      FOR idx IN par$acod_movimiento.FIRST .. par$acod_movimiento.LAST LOOP
        IF par$acod_movimiento (idx) IS NOT NULL THEN
          /* limpiar tabla temporaria */
          DELETE FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'MOVIMIENTO';

          IF par$abol_codigo_simple (idx) = 1 THEN
            /* Indica si el codigo informado esta sin el secuencial. */
            DBMS_OUTPUT.put_line ('Codigo informado esta sin el secuencial.');

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                            COD_CAMPO2,
                                            COD_CAMPO3,
                                            COD_CAMPO4,
                                            OID_CAMPO7,
                                            COD_CALIFICADOR)
              SELECT DOCU.OID_DOCUMENTO,
                     DOCU.COD_EXTERNO,
                     CUEN.OID_SECTOR,
                     CANA.COD_CANAL,
                     TO_CHAR (DOCU.FYH_GESTION, 'DD/MM/YYYY HH24:MI:SS'),
                     'MOVIMIENTO'
                FROM SAPR_TDOCUMENTO DOCU
                     INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_DESTINO
                     INNER JOIN GEPR_TSUBCANAL SUCA ON SUCA.OID_SUBCANAL = CUEN.OID_SUBCANAL
                     INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = SUCA.OID_CANAL
               WHERE DOCU.COD_EXTERNO LIKE par$acod_movimiento (idx) || '%';
          ELSE
            /* Indica si el codigo informado esta completo, incluyendo el numero secuencial. */
            DBMS_OUTPUT.put_line ('Codigo informado esta completo, incluyendo el numero secuencial.');

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                            COD_CAMPO2,
                                            COD_CAMPO3,
                                            COD_CAMPO4,
                                            OID_CAMPO7,
                                            COD_CALIFICADOR)
              SELECT DOCU.OID_DOCUMENTO,
                     DOCU.COD_EXTERNO,
                     CUEN.OID_SECTOR,
                     CANA.COD_CANAL,
                     TO_CHAR (DOCU.FYH_GESTION, 'DD/MM/YYYY HH24:MI:SS'),
                     'MOVIMIENTO'
                FROM SAPR_TDOCUMENTO DOCU
                     INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_DESTINO
                     INNER JOIN GEPR_TSUBCANAL SUCA ON SUCA.OID_SUBCANAL = CUEN.OID_SUBCANAL
                     INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = SUCA.OID_CANAL
               WHERE DOCU.COD_EXTERNO = par$acod_movimiento (idx);
          END IF;

          /* 2040100003 - El movimiento no fue encontrado en la base de datos. */
          BEGIN
            SELECT COUNT (1)
              INTO var$existe
              FROM SAPR_GTT_TAUXILIAR
             WHERE COD_CALIFICADOR = 'MOVIMIENTO' AND COD_CAMPO2 LIKE par$acod_movimiento (idx) || '%';
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$existe := 0;
          END;

          IF var$existe = 0 THEN
            DBMS_OUTPUT.put_line ('2040100003 - El movimiento NO fue encontrado en la base de datos.');

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                            COD_CAMPO2,
                                            COD_CAMPO3,
                                            COD_CALIFICADOR)
                 VALUES ('2040100003',
                         gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                          '2040100003',
                                                          gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                          NULL,
                                                          0),
                         par$acod_movimiento (idx),
                         'VALIDACIONES');
          ELSE
            DBMS_OUTPUT.put_line ('El movimiento fue encontrado en la base de datos.');

            FOR rec$movimiento IN cur$movimientos LOOP
              DBMS_OUTPUT.put_line ('Movimiento: ' || rec$movimiento.cod_externo);

              /* 2040100005 - El movimiento informado no esta relacionado a una maquina. */
              BEGIN
                SELECT MAQU.OID_MAQUINA
                  INTO var$oid_maquina
                  FROM SAPR_TMAQUINA MAQU
                 WHERE MAQU.OID_SECTOR = rec$movimiento.oid_sector;
              EXCEPTION
                WHEN NO_DATA_FOUND THEN
                  var$oid_maquina := NULL;
              END;

              IF var$oid_maquina IS NULL THEN
                DBMS_OUTPUT.put_line ('2040100005 - El movimiento informado no esta relacionado a una maquina.');

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                COD_CAMPO2,
                                                COD_CAMPO3,
                                                COD_CALIFICADOR)
                     VALUES ('2040100005',
                             gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                              '2040100005',
                                                              gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                              NULL,
                                                              0),
                             rec$movimiento.cod_externo,
                             'VALIDACIONES');
              ELSE
                /* 2040100004 - El movimiento informado no esta relacionado a una maquina planificada. */
                BEGIN
                  SELECT PLMA.OID_PLANIFICACION, PLAN.COD_PLANIFICACION
                    INTO var$oid_planificacion, var$cod_planificacion
                    FROM SAPR_TPLANXMAQUINA PLMA INNER JOIN SAPR_TPLANIFICACION PLAN ON PLAN.OID_PLANIFICACION = PLMA.OID_PLANIFICACION
                   WHERE PLMA.OID_MAQUINA = var$oid_maquina AND PLMA.BOL_ACTIVO = 1;
                EXCEPTION
                  WHEN NO_DATA_FOUND THEN
                    var$oid_planificacion := NULL;
                END;

                IF var$oid_planificacion IS NULL THEN
                  DBMS_OUTPUT.put_line ('2040100004 - El movimiento informado no esta relacionado a una maquina planificada.');

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                  COD_CAMPO2,
                                                  COD_CAMPO3,
                                                  COD_CALIFICADOR)
                       VALUES ('2040100004',
                               gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                '2040100004',
                                                                gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                NULL,
                                                                0),
                               rec$movimiento.cod_externo,
                               'VALIDACIONES');
                ELSE
                  /* Buscar periodo */
                  BEGIN
                    SELECT PEDO.OID_PERIODO, ESPE.COD_ESTADO_PERIODO
                      INTO var$oid_periodo, var$cod_estado_periodo
                      FROM SAPR_TPERIODOXDOCUMENTO PEDO
                           INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PEDO.OID_PERIODO
                           INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
                           INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO
                     WHERE PEDO.OID_DOCUMENTO = rec$movimiento.oid_documento;
                  EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                      var$oid_periodo := NULL;
                      var$cod_estado_periodo := NULL;
                  END;

                  /* Recuperar oid_tipo_calculo */
                  DBMS_OUTPUT.put_line ('COD_CANAL = ' || rec$movimiento.cod_canal);

                  IF rec$movimiento.cod_canal <> 'SF' THEN
                    BEGIN
                      SELECT C.OID_TIPO_CALCULO
                        INTO var$oid_tipo_calculo
                        FROM SAPR_TTIPO_CALCULO C
                       WHERE C.COD_TIPO_CALCULO = 'CONTABLE';
                    EXCEPTION
                      WHEN NO_DATA_FOUND THEN
                        var$oid_tipo_calculo := NULL;
                    END;
                  ELSE
                    BEGIN
                      SELECT C.OID_TIPO_CALCULO
                        INTO var$oid_tipo_calculo
                        FROM SAPR_TTIPO_CALCULO C
                       WHERE C.COD_TIPO_CALCULO = 'FISICO';
                    EXCEPTION
                      WHEN NO_DATA_FOUND THEN
                        var$oid_tipo_calculo := NULL;
                    END;
                  END IF;


                  IF par$abol_baja (idx) = 1 THEN
                    /* Indica que el movimiento deberá ser quitado del período relacionado. */
                    DBMS_OUTPUT.put_line ('Movimiento deberá ser quitado del período relacionado.');

                    /* 2040100007 - El movimiento no está relacionado a ninguno período. */
                    IF var$oid_periodo IS NULL OR var$cod_estado_periodo IS NULL THEN
                      DBMS_OUTPUT.put_line ('2040100007 - El movimiento no está relacionado a ninguno período.');

                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                      COD_CAMPO2,
                                                      COD_CAMPO3,
                                                      COD_CALIFICADOR)
                           VALUES ('2040100007',
                                   gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                    '2040100007',
                                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    NULL,
                                                                    0),
                                   rec$movimiento.cod_externo,
                                   'VALIDACIONES');
                    ELSIF var$cod_estado_periodo not in ('AB','BL','DB') THEN
                      /* 2040100006 - El movimiento informado no puede ser quitado de un período distinto de 'abierto'. */
                      DBMS_OUTPUT.put_line ('2040100006 - El movimiento informado no puede ser quitado de un período distinto de abierto.');

                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                      COD_CAMPO2,
                                                      COD_CAMPO3,
                                                      COD_CALIFICADOR)
                           VALUES ('2040100006',
                                   gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                    '2040100006',
                                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    NULL,
                                                                    0),
                                   rec$movimiento.cod_externo,
                                   'VALIDACIONES');
                    ELSE
                      DBMS_OUTPUT.put_line ('El movimiento informado es valido, puede ser quitado.');

                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                      COD_CAMPO2,
                                                      COD_CAMPO3,
                                                      COD_CAMPO4,
                                                      COD_CALIFICADOR)
                           VALUES (rec$movimiento.oid_documento,
                                   var$oid_periodo,
                                   rec$movimiento.cod_externo,
                                   var$oid_tipo_calculo,
                                   'MOVIMIENTOS_QUITAR');
                    END IF;
                  ELSE
                    /* Indica que el movimiento deberá ser relacionado a un período. */
                    DBMS_OUTPUT.put_line ('Movimiento deberá ser relacionado a un período.');

                    IF var$oid_periodo IS NOT NULL THEN
                      /* 2040100008 - El movimiento ya está relacionado a un período. */
                      DBMS_OUTPUT.put_line ('2040100008 - El movimiento ya está relacionado a un período.');

                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                      COD_CAMPO2,
                                                      COD_CAMPO3,
                                                      COD_CALIFICADOR)
                           VALUES ('2040100008',
                                   gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                    '2040100008',
                                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    NULL,
                                                                    0),
                                   rec$movimiento.cod_externo,
                                   'VALIDACIONES');
                    ELSE
                      /* 2040100009 - No fue posible relacionar el movimiento a un período. El movimiento tiene el canal "{0}" que no está relacionado a la planificación "{1}". */
                      BEGIN
                        SELECT COUNT (1)
                          INTO var$existe
                          FROM SAPR_TPLANIFICACION PLAN
                               INNER JOIN SAPR_TPLANXCANAL PLCA ON PLCA.OID_PLANIFICACION = PLAN.OID_PLANIFICACION
                               INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = PLCA.OID_CANAL
                         WHERE     PLAN.OID_PLANIFICACION = var$oid_planificacion
                               AND (CANA.COD_CANAL = rec$movimiento.cod_canal OR rec$movimiento.cod_canal = 'SF');
                      EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                          var$existe := 0;
                      END;

                      IF var$existe = 0 THEN
                        DBMS_OUTPUT.put_line (
                          '2040100009 - No fue posible relacionar el movimiento a un período. El movimiento tiene el canal "{0}" que no está relacionado a la planificación "{1}".');

                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                        COD_CAMPO2,
                                                        COD_CAMPO3,
                                                        COD_CALIFICADOR)
                             VALUES ('2040100009',
                                     gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                      '2040100009',
                                                                      gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                      rec$movimiento.cod_canal || '|' || var$cod_planificacion,
                                                                      0),
                                     rec$movimiento.cod_externo,
                                     'VALIDACIONES');
                      ELSE
                        BEGIN
                          SELECT TO_DATE (AUX.OID_CAMPO7, 'DD/MM/YYYY HH24:MI:SS')
                            INTO var$fyh_gestion
                            FROM SAPR_GTT_TAUXILIAR AUX
                           WHERE AUX.COD_CALIFICADOR = 'MOVIMIENTO';


                            SELECT OID_PERIODO
                              INTO var$oid_periodo
                              FROM SAPR_TPERIODO P 
                              INNER JOIN SAPR_TESTADO_PERIODO EP ON EP.OID_ESTADO_PERIODO = P.OID_ESTADO_PERIODO
                              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
                            WHERE     P.OID_MAQUINA = var$oid_maquina
                                   AND EP.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND P.oid_acreditacion IS NULL
                                   AND (
                                        (P.FYH_INICIO <= var$fyh_gestion AND P.FYH_FIN >= var$fyh_gestion )
                                        OR  (P.FYH_INICIO <= SYS_EXTRACT_UTC (CURRENT_TIMESTAMP) AND P.FYH_FIN >= SYS_EXTRACT_UTC (CURRENT_TIMESTAMP) )
                                   )
                            ORDER BY P.FYH_INICIO
                            FETCH FIRST ROW ONLY;


                          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                          COD_CAMPO2,
                                                          COD_CAMPO3,
                                                          COD_CAMPO4,
                                                          COD_CALIFICADOR)
                               VALUES (rec$movimiento.oid_documento,
                                       var$oid_periodo,
                                       rec$movimiento.cod_externo,
                                       var$oid_tipo_calculo,
                                       'MOVIMIENTOS_RELACIONAR');
                        EXCEPTION
                          WHEN NO_DATA_FOUND THEN
                            DBMS_OUTPUT.put_line (
                              '2040100011 - No fue posible relacionar el movimiento a un período. La Planificacion no tiene período abierto.');

                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                            COD_CAMPO2,
                                                            COD_CAMPO3,
                                                            COD_CALIFICADOR)
                                 VALUES ('2040100011',
                                         gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                          '2040100011',
                                                                          gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                          rec$movimiento.cod_canal || '|' || var$cod_planificacion,
                                                                          0),
                                         rec$movimiento.cod_externo,
                                         'VALIDACIONES');
                        END;
                      END IF;
                    END IF;
                  END IF;
                END IF;
              END IF;
            END LOOP rec$movimiento;
          END IF;

        END IF;
      END LOOP;
    END IF;
  END svalidar_mov_periodo;

  /* sdel_tperiodoxdocumento */
  PROCEDURE sdel_tperiodoxdocumento (par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_, par$oid_periodo IN gepr_pcomon_###VERSION###.tipo$oid_) AS
  BEGIN
    DELETE SAPR_TPERIODOXDOCUMENTO
     WHERE OID_DOCUMENTO = par$oid_documento AND OID_PERIODO = par$oid_periodo;
  END sdel_tperiodoxdocumento;

  /* sins_tperiodoxdocumento */
  PROCEDURE sins_tperiodoxdocumento (par$oid_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_periodo     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$cod_) AS
    var$gmt_zero   VARCHAR2 (50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO sapr_tperiodoxdocumento (oid_periodoxdocumento,
                                         oid_documento,
                                         oid_periodo,
                                         gmt_creacion,
                                         des_usuario_creacion,
                                         gmt_modificacion,
                                         des_usuario_modificacion)
         VALUES (SYS_GUID (),
                 par$oid_documento,
                 par$oid_periodo,
                 var$gmt_zero,
                 par$cod_usuario,
                 var$gmt_zero,
                 par$cod_usuario);
  END sins_tperiodoxdocumento;

 /* Verificar Periodos masivos */


PROCEDURE sverificar_periodo_masivos_ex (
                                   par$acod_index                IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_canal                IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_subcanal             IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_divisa               IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_sector               IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$acod_pto_servicio         IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                   par$cod_ajeno                IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$afyh_gestion              IN     gepr_pcomon_###VERSION###.tipo$fyhs_,
                                   par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                   par$cod_cultura              IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                   par$info_ejecucion           IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                   par$validaciones                OUT SYS_REFCURSOR,
                                   par$maquinas                    OUT SYS_REFCURSOR,
                                   par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    /* Validaciones */
    var$cod_validaciones    gepr_pcomon_###VERSION###.tipo$cods_;
    var$des_validaciones    gepr_pcomon_###VERSION###.tipo$descs_;
    var$tcod_validaciones   varchar2_array;
    var$tdes_validaciones   varchar2_array;
    var$oid_pto_servicio    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_maquina        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_periodo     gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_planificacion gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_punto_servicio  gepr_pcomon_###VERSION###.tipo$cod_;
    var$tipo_resultado gepr_pcomon_###VERSION###.tipo$cod_;
    Var$acod_pto_servicio        gepr_pcomon_###VERSION###.tipo$cods_;
          var$cod_subcanal gepr_pcomon_###VERSION###.tipo$cod_;



      CURSOR cur$datos_mae(par$cod_sector IN gepr_pcomon_###VERSION###.tipo$cod_) IS
          SELECT DISTINCT
                    PTO.oid_pto_servicio,
                    PTO.cod_pto_servicio,
                    CAPTO.cod_ajeno  cod_pto_servicio_ajeno
        FROM sapr_Tmaquina maquina
            INNER JOIN gepr_tsector sector
                ON maquina.oid_sector = sector.oid_sector
            INNER JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_MAQUINA = maquina.OID_MAQUINA
            LEFT JOIN GEPR_TCODIGO_AJENO CAPTO
                ON CAPTO.oid_Tabla_genesis = PTO.oid_pto_servicio
                AND CAPTO.cod_tipo_tabla_genesis = 'GEPR_TPUNTO_SERVICIO'
                AND CAPTO.COD_IDENTIFICADOR = par$cod_ajeno
        WHERE
            SECTOR.COD_SECTOR = par$cod_sector;
    begin


    DELETE SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'VALIDACIONES_MASIVAS';




    IF par$acod_pto_servicio IS NULL OR  not par$acod_pto_servicio.COUNT > 0 THEN

        FOR  idx IN par$acod_sector.first .. par$acod_sector.last
        LOOP

            IF par$acod_sector(idx) IS NOT NULL THEN

                FOR reg$datos_mae IN cur$datos_mae(par$acod_sector(idx)) LOOP
                    IF par$cod_ajeno IS NOT NULL THEN

                         Var$acod_pto_servicio(Var$acod_pto_servicio.COUNT) := reg$datos_mae.cod_pto_servicio_ajeno;
                    ELSE
                          Var$acod_pto_servicio(Var$acod_pto_servicio.COUNT) := reg$datos_mae.cod_pto_servicio;
                    END IF;
                END LOOP;

                  FOR  idxPT IN var$acod_pto_servicio.first .. Var$acod_pto_servicio.last
                    LOOP
                        -- Validación del código divisa
                        IF par$cod_ajeno IS NOT NULL THEN
                          BEGIN
                            SELECT DIVI.OID_DIVISA
                              INTO var$oid_divisa
                            FROM GEPR_TCODIGO_AJENO C 
                              INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = C.OID_TABLA_GENESIS
                            WHERE C.COD_IDENTIFICADOR = par$cod_ajeno AND C.COD_AJENO = par$acod_divisa(idx) AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA';
                          EXCEPTION
                            WHEN NO_DATA_FOUND THEN
                              var$oid_divisa := NULL;
                          END;
                        ELSE
                          BEGIN
                            SELECT DIVI.OID_DIVISA
                              INTO var$oid_divisa
                            FROM GEPR_TDIVISA DIVI
                            WHERE DIVI.COD_ISO_DIVISA = par$acod_divisa(idx);
                          EXCEPTION
                            WHEN NO_DATA_FOUND THEN
                              var$oid_divisa := NULL;
                          END;
                        END IF;

                        sverificar_periodo
                        (par$oid_llamada             => NULL,
                         par$cod_canal               => par$acod_canal(idx),
                        par$cod_subcanal             => par$acod_subcanal(idx),
                        par$oid_divisa               => var$oid_divisa,
                        par$cod_sector               => par$acod_sector(idx),
                        par$oid_pto_servicio         => Var$acod_pto_servicio(idxPT),
                        par$cod_ajeno                => par$cod_ajeno,
                        par$fyh_gestion              => par$afyh_gestion(idx),
                        par$cod_usuario              => par$cod_usuario,
                        par$cod_cultura              => par$cod_cultura,
                        par$info_ejecucion           => par$info_ejecucion,
                        par$oid_maquina              => var$oid_maquina,
                        par$oid_planificacion        => var$oid_planificacion,
                        par$oid_periodo              => var$oid_periodo,
                        par$cod_tipo_planificacion   => var$cod_tipo_planificacion,
                        par$cod_validaciones         => var$cod_validaciones,
                        par$des_validaciones         => var$des_validaciones,
                        par$cod_ejecucion            => par$cod_ejecucion);

                       IF (var$oid_planificacion is null) OR (var$oid_periodo is NOT null) THEN
                           var$tipo_resultado := 1;
                       ELSE
                            IF (var$oid_planificacion is NOT null )
                              AND (var$oid_maquina is NOT null )
                              AND (var$cod_tipo_planificacion is not null and  var$cod_tipo_planificacion = 'ONLINE' ) THEN
                                  var$tipo_resultado := 1;
                               ELSE
                                  var$tipo_resultado := 2;
                            END IF;
                       END IF;


                       IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc (var$des_validaciones) THEN

                           var$tipo_resultado := 2;
                          FOR idxV IN var$des_validaciones.first .. var$des_validaciones.last loop
                               INSERT INTO
                                SAPR_GTT_TAUXILIAR
                                (
                                  OID_CAMPO1,
                                  COD_CAMPO2,
                                  COD_CAMPO3,
                                  COD_CAMPO4,
                                  OID_CAMPO7,
                                  COD_CALIFICADOR
                                )
                                SELECT
                                  nvl(var$oid_maquina, ' '),
                                  nvl(var$cod_validaciones(idxV), ' '),
                                  nvl(var$des_validaciones(idxV), ' '),
                                  var$tipo_resultado,
                                  par$acod_index(idx),
                                  'VALIDACIONES_MASIVAS'
                                FROM DUAL;
                          end loop;
                       ELSE
                          INSERT INTO
                            SAPR_GTT_TAUXILIAR
                            (
                              OID_CAMPO1,
                              COD_CAMPO2,
                              COD_CAMPO3,
                              COD_CAMPO4,
                              OID_CAMPO7,
                              COD_CALIFICADOR
                            )
                            SELECT
                             nvl(var$oid_maquina, ' '),
                              ' ',
                              ' ',
                              var$tipo_resultado,
                              par$acod_index(idx),
                              'VALIDACIONES_MASIVAS'
                            FROM DUAL;

                        END IF;
                 END LOOP;
            END IF;
        END LOOP;
      END IF;



    IF par$acod_pto_servicio IS NOT NULL AND par$acod_pto_servicio.COUNT > 0 THEN
      FOR  idx IN par$acod_pto_servicio.first .. par$acod_pto_servicio.last
      LOOP
        -- Validación del código divisa
        IF par$cod_ajeno IS NOT NULL THEN
          BEGIN
            SELECT DIVI.OID_DIVISA
              INTO var$oid_divisa
            FROM GEPR_TCODIGO_AJENO C 
              INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = C.OID_TABLA_GENESIS
            WHERE C.COD_IDENTIFICADOR = par$cod_ajeno AND C.COD_AJENO = par$acod_divisa(idx) AND C.COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA';
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$oid_divisa := NULL;
          END;
        ELSE
          BEGIN
            SELECT DIVI.OID_DIVISA
              INTO var$oid_divisa
            FROM GEPR_TDIVISA DIVI
            WHERE DIVI.COD_ISO_DIVISA = par$acod_divisa(idx);
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$oid_divisa := NULL;
          END;
        END IF;

        sverificar_periodo
                        (par$oid_llamada            => NULL,
                        par$cod_canal               => par$acod_canal(idx),
                        par$cod_subcanal             => par$acod_subcanal(idx),
                        par$oid_divisa               => var$oid_divisa,
                        par$cod_sector               => par$acod_sector(idx),
                        par$oid_pto_servicio         => par$acod_pto_servicio(idx),
                        par$cod_ajeno                => par$cod_ajeno,
                        par$fyh_gestion              => par$afyh_gestion(idx),
                        par$cod_usuario              => par$cod_usuario,
                        par$cod_cultura              => par$cod_cultura,
                        par$info_ejecucion           => par$info_ejecucion,
                        par$oid_maquina              => var$oid_maquina,
                        par$oid_planificacion        => var$oid_planificacion,
                        par$oid_periodo              => var$oid_periodo,
                        par$cod_tipo_planificacion   => var$cod_tipo_planificacion,
                        par$cod_validaciones         => var$cod_validaciones,
                        par$des_validaciones         => var$des_validaciones,
                        par$cod_ejecucion            => par$cod_ejecucion);

                       IF (var$oid_planificacion is null) OR (var$oid_periodo is NOT null) THEN
                           var$tipo_resultado := 1;
                       ELSE
                            IF (var$oid_planificacion is NOT null )
                              AND (var$oid_maquina is NOT null )
                              AND (var$cod_tipo_planificacion is not null and  var$cod_tipo_planificacion = 'ONLINE' ) THEN
                                  var$tipo_resultado := 1;
                               ELSE
                                  var$tipo_resultado := 2;
                            END IF;
                       END IF;


                       IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc (var$des_validaciones) THEN

                           var$tipo_resultado := 2;
                          FOR idxV IN var$des_validaciones.first .. var$des_validaciones.last loop
                               INSERT INTO
                                SAPR_GTT_TAUXILIAR
                                (
                                  OID_CAMPO1,
                                  COD_CAMPO2,
                                  COD_CAMPO3,
                                  COD_CAMPO4,
                                  OID_CAMPO7,
                                  COD_CALIFICADOR
                                )
                                SELECT
                                  nvl(var$oid_maquina, ' '),
                                  nvl(var$cod_validaciones(idxV), ' '),
                                  nvl(var$des_validaciones(idxV), ' '),
                                  var$tipo_resultado,
                                  par$acod_index(idx),
                                  'VALIDACIONES_MASIVAS'
                                FROM DUAL;
                          end loop;
                       ELSE
                          INSERT INTO
                            SAPR_GTT_TAUXILIAR
                            (
                              OID_CAMPO1,
                              COD_CAMPO2,
                              COD_CAMPO3,
                              COD_CAMPO4,
                              OID_CAMPO7,
                              COD_CALIFICADOR
                            )
                            SELECT
                             nvl(var$oid_maquina, ' '),
                              ' ',
                              ' ',
                              var$tipo_resultado,
                              par$acod_index(idx),
                              'VALIDACIONES_MASIVAS'
                            FROM DUAL;

                        END IF;
      END LOOP;

                        END IF;

      OPEN par$validaciones FOR
        SELECT
          aux.OID_CAMPO1 OID_MAQUINA,
          aux.COD_CAMPO2 COD_VALIDACION,
          aux.COD_CAMPO3 DES_VALIDACION,
          aux.COD_CAMPO4 TIPO_RESULTADO,
          aux.OID_CAMPO7 INDICE
        FROM
          SAPR_GTT_TAUXILIAR aux
        WHERE aux.COD_CALIFICADOR = 'VALIDACIONES_MASIVAS';

          OPEN par$maquinas FOR
          SELECT

            maq.OID_MAQUINA,
            sector.COD_SECTOR,
            planta.COD_PLANTA,
            deleg.COD_DELEGACION
          FROM   SAPR_GTT_TAUXILIAR AUX
            INNER JOIN SAPR_TMAQUINA maq ON maq.OID_MAQUINA = aux.OID_CAMPO1 AND aux.COD_CALIFICADOR = 'VALIDACIONES_MASIVAS'
            INNER JOIN GEPR_TSECTOR sector ON maq.oid_sector = sector.oid_sector
            INNER JOIN GEPR_TPLANTA planta ON sector.oid_planta = planta.oid_planta
            INNER JOIN GEPR_TDELEGACION deleg ON deleg.oid_delegacion = planta.oid_delegacion
           GROUP BY  maq.OID_MAQUINA, sector.COD_SECTOR, planta.COD_PLANTA, deleg.COD_DELEGACION;


    end sverificar_periodo_masivos_ex;


  /* Procedure que se encarga de desbloquear un periodo */
PROCEDURE sdesbloquear_periodo (
    par$oid_periodo IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_pais IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_device_id IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$usr_,
    par$codigo OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$descripcion OUT gepr_pcomon_###VERSION###.tipo$desc_
) IS
    var$codigo  gepr_pcomon_###VERSION###.tipo$cod_;
    var$descripcion  gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_estado_periodo gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_estado_desbloqueado gepr_pcomon_###VERSION###.tipo$oid_;
    const$estado_bloqueado CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := 'BL';
    const$estado_desbloqueado CONSTANT gepr_pcomon_###VERSION###.tipo$cod_ := 'DB';
    var$existe gepr_pcomon_###VERSION###.tipo$nel_;
begin

  begin
    SELECT estper.cod_estado_periodo
    INTO    var$cod_estado_periodo
    FROM   SAPR_TPERIODO per INNER JOIN SAPR_TESTADO_PERIODO estper ON per.oid_estado_periodo = estper.oid_estado_periodo
    WHERE   per.OID_PERIODO = par$oid_periodo;

  exception
    when no_data_found then
        var$codigo := '2040240005';
        raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, var$codigo, TRUE);
  end;

  begin

    SELECT count(1)
    INTO    var$existe
    FROM    gepr_tsector sector
       INNER JOIN
          gepr_tplanta planta
       ON sector.oid_planta = planta.oid_planta
       INNER JOIN
        gepr_tdelegacion delegacion
       ON delegacion.oid_delegacion = planta.oid_delegacion
       INNER JOIN
        gepr_tpais pais
       ON pais.oid_pais = delegacion.oid_pais
       INNER JOIN SAPR_TMAQUINA maqui
       ON maqui.oid_sector = sector.oid_sector
       INNER JOIN SAPR_TPERIODO peri on peri.oid_maquina = maqui.oid_maquina
    WHERE
        pais.cod_pais = par$cod_pais
        AND maqui.COD_IDENTIFICACION = par$cod_device_id
        AND peri.oid_periodo = par$oid_periodo;
    exception
      when no_data_found then
         var$codigo := '2040240006';
         raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, var$codigo, TRUE);
  end;

  IF var$existe = 0 THEN
    var$codigo := '2040240006';
    raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, var$codigo, TRUE);
  END IF;

  IF var$cod_estado_periodo <> const$estado_bloqueado then
    IF var$cod_estado_periodo = const$estado_desbloqueado then
        var$codigo := '2040240003';
        raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, var$codigo, TRUE);
    else
       var$codigo := '2040240004';
       raise_application_error (gepr_pcomon_###VERSION###.const$codCustomError, var$codigo, TRUE);
    end if;
  Else

    var$codigo := '0040240000';

    select oid_estado_periodo
    into var$oid_estado_desbloqueado
    from SAPR_TESTADO_PERIODO
    where cod_estado_periodo = const$estado_desbloqueado;
    update SAPR_TPERIODO
       set oid_estado_periodo = var$oid_estado_desbloqueado,
       GMT_MODIFICACION = sysdate,
       DES_USUARIO_MODIFICACION = par$cod_usuario
     where oid_periodo = par$oid_periodo ;
    COMMIT;
  END IF;

   par$codigo := var$codigo;
   par$descripcion := gepr_putilidades_###VERSION###.ftraduzir('es',
                                                                    var$codigo,
                                                                    gepr_pcomon_###VERSION###.const$CodFunGenerico,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1);
  dbms_output.put_line( par$codigo || ' - ' || par$descripcion  );
    exception
      when others then
       if var$codigo = '' then
            var$codigo := '3040240001';
       end if;
       par$codigo := var$codigo;
       par$descripcion := gepr_putilidades_###VERSION###.ftraduzir('es',
                                                            var$codigo,
                                                            gepr_pcomon_###VERSION###.const$CodFunGenerico,
                                                            gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                            NULL,
                                                            1);
      dbms_output.put_line( par$codigo || ' - ' || par$descripcion  );

end sdesbloquear_periodo;

  PROCEDURE svalidar_periodo_baja (par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$abol_baja            IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$abol_codigo_simple   IN gepr_pcomon_###VERSION###.tipo$nbols_,
                                  par$acod_movimiento      IN gepr_pcomon_###VERSION###.tipo$cods_,
                                  par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_) IS
     --Variables de LOG
    const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.svalidar_periodo_baja';
		var$detalle           VARCHAR2(4000);

    var$existe               gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$oid_maquina          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion    gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_planificacion    gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_periodo          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_estado_periodo   gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_tipo_calculo     gepr_pcomon_###VERSION###.tipo$oid_;
    var$fyh_gestion          gepr_pcomon_###VERSION###.tipo$fyh_;
    var$cod_tipo_planificacion gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_cultura              gepr_pcomon_###VERSION###.tipo$cod_ := 'es';
    var$usuario                  gepr_pcomon_###VERSION###.tipo$usr_ := 'VALIDAR_PERIODO_BAJA';
    var$info_ejecucion           gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_ejecucion            gepr_pcomon_###VERSION###.tipo$nel_;
    var$des_validaciones         gepr_pcomon_###VERSION###.tipo$descs_;
    var$cod_validaciones         gepr_pcomon_###VERSION###.tipo$cods_;
    var$cod_tipo_plani           gepr_pcomon_###VERSION###.tipo$cod_; 
    var$cod_proceso              gepr_pcomon_###VERSION###.tipo$cod_;
    var$doc_collection_id        gepr_pcomon_###VERSION###.tipo$cod_;
    var$doc_oid_subcanal         gepr_pcomon_###VERSION###.tipo$oid_;
    var$doc_oid_divisa           gepr_pcomon_###VERSION###.tipo$oid_;
    var$cant_per_quitar          gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_per_subcanal          gepr_pcomon_###VERSION###.tipo$nel_;
     var$bol_per_divisa           gepr_pcomon_###VERSION###.tipo$nel_;       
    /* Movimientos */
    CURSOR cur$movimientos  IS
      SELECT OID_CAMPO1 OID_DOCUMENTO,
             COD_CAMPO2 COD_EXTERNO,
             COD_CAMPO3 COD_SECTOR,
             COD_CAMPO4 COD_CANAL,
             COD_CAMPO8 COD_SUBCANAL,
             COD_CAMPO9 OID_PTO_SERVICIO,
             OID_CAMPO7 FYH_GESTION,
             COD_CALIFICADOR
        FROM SAPR_GTT_TAUXILIAR D
       WHERE COD_CALIFICADOR = 'MOVIMIENTO';

    /*Periodos quitar*/
    CURSOR cur$periodos_quitar IS
    SELECT OID_CAMPO1 OID_PERIODO,
           COD_CAMPO2 COD_ESTADO_PERIODO,
           COD_CAMPO3 COD_TIPO_PERIODO
    FROM SAPR_GTT_TAUXILIAR
    WHERE COD_CALIFICADOR = 'PERIODOS_QUITAR';

    /*Periodos relacionar*/
    CURSOR cur$periodos_relacionar IS
    SELECT OID_CAMPO1 OID_PERIODO,
           COD_CAMPO2 COD_ESTADO_PERIODO,
           COD_CAMPO3 COD_TIPO_PERIODO
    FROM SAPR_GTT_TAUXILIAR
    WHERE COD_CALIFICADOR = 'PERIODOS_RELACIONAR';
  BEGIN
    IF par$oid_llamada is not null THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Parametros iniciales: ' ||
                                            ' par$oid_llamada: ' || par$oid_llamada ||
                                            ', par$cod_cultura: ' || par$cod_cultura, 
                            par$cod_identificador   => '');

        /* INICIO LOG de arrays asociativos */
        IF par$abol_baja IS NOT NULL AND par$abol_baja.COUNT > 0 THEN
          FOR idx IN  par$abol_baja.first ..  par$abol_baja.last LOOP 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'Parametros iniciales: ' || 'par$abol_baja(' || idx || '):  ' || par$abol_baja(idx),
                              par$cod_identificador   => '');
          END LOOP;
        END IF;
        IF par$abol_codigo_simple IS NOT NULL AND par$abol_codigo_simple.COUNT > 0 THEN
          FOR idx IN  par$abol_codigo_simple.first ..  par$abol_codigo_simple.last LOOP 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'Parametros iniciales: ' || 'par$abol_codigo_simple(' || idx || '):  ' || par$abol_codigo_simple(idx),
                              par$cod_identificador   => '');
          END LOOP;
        END IF;
        IF par$acod_movimiento IS NOT NULL AND par$acod_movimiento.COUNT > 0 THEN
          FOR idx IN  par$acod_movimiento.first ..  par$acod_movimiento.last LOOP 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'Parametros iniciales: ' || 'par$acod_movimiento(' || idx || '):  ' || par$acod_movimiento(idx),
                              par$cod_identificador   => '');
          END LOOP;
        END IF;

      END IF;
    /* 2040100010 - Es obligatorio informar un movimiento. */
    IF    GEPR_PUTILIDADES_###VERSION###.farray_vacio_cod (par$acod_movimiento)
       OR par$acod_movimiento.COUNT <> par$abol_baja.COUNT
       OR par$acod_movimiento.COUNT <> par$abol_codigo_simple.COUNT THEN
      DBMS_OUTPUT.put_line ('2040100010 - Es obligatorio informar un movimiento.');

      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                      COD_CAMPO2,
                                      COD_CAMPO3,
                                      COD_CALIFICADOR)
           VALUES ('2040100010',
                   gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                    '2040100010',
                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                    NULL,
                                                    0),
                   '',
                   'VALIDACIONES');
    ELSE
      FOR idx IN par$acod_movimiento.FIRST .. par$acod_movimiento.LAST LOOP
        IF par$acod_movimiento (idx) IS NOT NULL THEN
          /* limpiar tabla temporaria */
          DELETE FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'MOVIMIENTO';
          /* limpio los periodos a quitar */
          DELETE FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'PERIODOS_QUITAR';
          /* limpio los periodos a relacionar*/
          DELETE FROM SAPR_GTT_TAUXILIAR
                WHERE COD_CALIFICADOR = 'PERIODOS_RELACIONAR';

          IF par$abol_codigo_simple (idx) = 1 THEN
            /* Indica si el codigo informado esta sin el secuencial. */
            DBMS_OUTPUT.put_line ('Codigo informado esta sin el secuencial.');

           INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                            COD_CAMPO2,
                                            COD_CAMPO3,
                                            COD_CAMPO4,
                                            COD_CAMPO8,
                                            COD_CAMPO9,
                                            OID_CAMPO7,
                                            COD_CALIFICADOR)
              SELECT DOCU.OID_DOCUMENTO,
                     DOCU.COD_EXTERNO,
                     SEC.COD_SECTOR,
                     CANA.COD_CANAL,
                     SUCA.COD_SUBCANAL,
                     CUEN.OID_PTO_SERVICIO,
                     TO_CHAR (DOCU.FYH_GESTION, 'DD/MM/YYYY HH24:MI:SS'),
                     'MOVIMIENTO'
                FROM SAPR_TDOCUMENTO DOCU
                     INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_DESTINO
                     INNER JOIN GEPR_TSUBCANAL SUCA ON SUCA.OID_SUBCANAL = CUEN.OID_SUBCANAL
                     INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = SUCA.OID_CANAL
                      INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = CUEN.OID_SECTOR
               WHERE DOCU.COD_EXTERNO LIKE par$acod_movimiento (idx) || '%';
          ELSE
            /* Indica si el codigo informado esta completo, incluyendo el numero secuencial. */
            DBMS_OUTPUT.put_line ('Codigo informado esta completo, incluyendo el numero secuencial.');

         INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                            COD_CAMPO2,
                                            COD_CAMPO3,
                                            COD_CAMPO4,
                                            COD_CAMPO8,
                                            COD_CAMPO9,
                                            OID_CAMPO7,
                                            COD_CALIFICADOR)
              SELECT DOCU.OID_DOCUMENTO,
                     DOCU.COD_EXTERNO,
                     SEC.COD_SECTOR,
                     CANA.COD_CANAL,
                     SUCA.COD_SUBCANAL,
                     CUEN.OID_PTO_SERVICIO,
                     TO_CHAR (DOCU.FYH_GESTION, 'DD/MM/YYYY HH24:MI:SS'),
                     'MOVIMIENTO'
                FROM SAPR_TDOCUMENTO DOCU
                     INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_DESTINO 
                     INNER JOIN GEPR_TSUBCANAL SUCA ON SUCA.OID_SUBCANAL = CUEN.OID_SUBCANAL
                     INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = SUCA.OID_CANAL
                     INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = CUEN.OID_SECTOR
               WHERE DOCU.COD_EXTERNO = par$acod_movimiento (idx);
          END IF;

          /* 2040100003 - El movimiento no fue encontrado en la base de datos. */
          BEGIN
            SELECT COUNT (1)
              INTO var$existe
              FROM SAPR_GTT_TAUXILIAR
             WHERE COD_CALIFICADOR = 'MOVIMIENTO' AND COD_CAMPO2 LIKE par$acod_movimiento (idx) || '%';
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$existe := 0;
          END;

          IF var$existe = 0 THEN
            DBMS_OUTPUT.put_line ('2040100003 - El movimiento NO fue encontrado en la base de datos.');

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                            COD_CAMPO2,
                                            COD_CAMPO3,
                                            COD_CALIFICADOR)
                 VALUES ('2040100003',
                         gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                          '2040100003',
                                                          gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                          NULL,
                                                          0),
                         par$acod_movimiento (idx),
                         'VALIDACIONES');
          ELSE
            DBMS_OUTPUT.put_line ('El movimiento fue encontrado en la base de datos.');

            FOR rec$movimiento IN cur$movimientos LOOP
              DBMS_OUTPUT.put_line ('Movimiento: ' || rec$movimiento.cod_externo);

              /* 2040100005 - El movimiento informado no esta relacionado a una maquina. */
              BEGIN
                SELECT SECT.OID_SECTOR, MAQU.OID_MAQUINA
                  INTO var$oid_sector, var$oid_maquina
                  FROM GEPR_TSECTOR SECT
                  INNER JOIN SAPR_TMAQUINA MAQU ON SECT.OID_SECTOR = MAQU.OID_SECTOR
                WHERE MAQU.COD_IDENTIFICACION = rec$movimiento.COD_SECTOR;
              EXCEPTION
                WHEN NO_DATA_FOUND THEN
                  var$oid_maquina := NULL;
              END;

              IF var$oid_maquina IS NULL THEN
                DBMS_OUTPUT.put_line ('2040100005 - El movimiento informado no esta relacionado a una maquina.');

                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                COD_CAMPO2,
                                                COD_CAMPO3,
                                                COD_CALIFICADOR)
                     VALUES ('2040100005',
                             gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                              '2040100005',
                                                              gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                              NULL,
                                                              0),
                             rec$movimiento.cod_externo,
                             'VALIDACIONES');
              ELSE

                BEGIN
                  SELECT PLMA.OID_PLANIFICACION, PLAN.COD_PLANIFICACION, TPLANI.COD_TIPO_PLANIFICACION, BOL_PERIODO_SUBCANAL, BOL_PERIODO_DIVISA 
                    INTO var$oid_planificacion, var$cod_planificacion, var$cod_tipo_plani, var$bol_per_subcanal, var$bol_per_divisa
                    FROM SAPR_TPLANXMAQUINA PLMA 
                    INNER JOIN SAPR_TPLANIFICACION PLAN ON PLAN.OID_PLANIFICACION = PLMA.OID_PLANIFICACION
                    INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLAN.OID_TIPO_PLANIFICACION
                   WHERE PLMA.OID_MAQUINA = var$oid_maquina AND PLMA.BOL_ACTIVO = 1;
                EXCEPTION
                  WHEN NO_DATA_FOUND THEN
                    var$oid_planificacion := NULL;
                END;

                IF var$oid_planificacion IS NULL THEN
                  /* 2040100004 - El movimiento informado no esta relacionado a una maquina planificada. */
                  DBMS_OUTPUT.put_line ('2040100004 - El movimiento informado no esta relacionado a una maquina planificada.');

                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                  COD_CAMPO2,
                                                  COD_CAMPO3,
                                                  COD_CALIFICADOR)
                       VALUES ('2040100004',
                               gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                '2040100004',
                                                                gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                NULL,
                                                                0),
                               rec$movimiento.cod_externo,
                               'VALIDACIONES');
                ELSE


                    /* Buscar periodos relacionados al documento (para quitar si es el caso)*/
                    BEGIN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                        (SELECT PEDO.OID_PERIODO, ESPE.COD_ESTADO_PERIODO, TIPE.COD_TIPO_PERIODO,  'PERIODOS_QUITAR'
                        FROM SAPR_TPERIODOXDOCUMENTO PEDO
                            INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PEDO.OID_PERIODO
                            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO
                            INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO
                        WHERE PEDO.OID_DOCUMENTO = rec$movimiento.oid_documento);

                    END;



                  /*Aca me guardo un oid periodo para validaciones en quitar y relacionar*/
                  BEGIN
                    SELECT PEDO.OID_PERIODO INTO var$oid_periodo
                        FROM SAPR_TPERIODOXDOCUMENTO PEDO
                            INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PEDO.OID_PERIODO
                            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
                            INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO
                      WHERE PEDO.OID_DOCUMENTO = rec$movimiento.oid_documento AND ROWNUM = 1;
                  EXCEPTION 
                    WHEN NO_DATA_FOUND THEN
                      var$oid_periodo := null;

                  END;
                   SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'Periodo para validaciones : ' || var$oid_periodo,
                              par$cod_identificador   => '');
                  /* Recuperar oid_tipo_calculo */
                  DBMS_OUTPUT.put_line ('COD_CANAL = ' || rec$movimiento.cod_canal);

                  IF rec$movimiento.cod_canal <> 'SF' THEN
                    BEGIN
                      SELECT C.OID_TIPO_CALCULO
                        INTO var$oid_tipo_calculo
                        FROM SAPR_TTIPO_CALCULO C
                       WHERE C.COD_TIPO_CALCULO = 'CONTABLE';
                    EXCEPTION
                      WHEN NO_DATA_FOUND THEN
                        var$oid_tipo_calculo := NULL;
                    END;
                  ELSE
                    BEGIN
                      SELECT C.OID_TIPO_CALCULO
                        INTO var$oid_tipo_calculo
                        FROM SAPR_TTIPO_CALCULO C
                       WHERE C.COD_TIPO_CALCULO = 'FISICO';
                    EXCEPTION
                      WHEN NO_DATA_FOUND THEN
                        var$oid_tipo_calculo := NULL;
                    END;
                  END IF;

                        IF par$abol_baja (idx) = 1 THEN
                          begin 
                            SELECT COUNT(*) INTO  var$cant_per_quitar FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'PERIODOS_QUITAR';
                          exception
                           WHEN NO_DATA_FOUND THEN
                            var$cant_per_quitar := 0;
                          end;
                           --SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                           --     par$des_origen      => const$des_origen,
                           --     par$des_version     => const$version,                            
                            --    par$des_detalle     => 'Cantidad de periodos a quitar: ' || var$cant_per_quitar,
                           --     par$cod_identificador   => '');
                          FOR rec$periodo IN cur$periodos_quitar LOOP
                              /* Indica que el movimiento deberá ser quitado del período relacionado. */
                               SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$des_origen,
                                par$des_version     => const$version,                            
                                par$des_detalle     => 'Movimiento deberá ser quitado del período relacionado.',
                                par$cod_identificador   => '');



                                    var$oid_periodo := rec$periodo.OID_PERIODO;
                                    /* 2040100007 - El movimiento no está relacionado a ninguno período. */
                                    IF var$oid_periodo IS NULL THEN
                                         SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => '2040100007 - El movimiento no está relacionado a ninguno período.',
                                        par$cod_identificador   => '');


                                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                        COD_CAMPO2,
                                                                        COD_CAMPO3,
                                                                        COD_CALIFICADOR)
                                            VALUES ('2040100007',
                                                    gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                                    '2040100007',
                                                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                    NULL,
                                                                                    0),
                                                    rec$movimiento.cod_externo,
                                                    'VALIDACIONES');
                                    ELSIF rec$periodo.cod_tipo_periodo = 'AC' AND rec$periodo.cod_estado_periodo <> gepr_pcomon_###VERSION###.const$CodEstadoPeriodoTransito THEN /*Ver si tambien aplica para boveda y recojo*/
                                        /* 2040100006 - El movimiento informado no puede ser quitado de un período distinto de 'abierto'. */

                                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => '2040100006 - El movimiento informado no puede ser quitado de un período distinto de abierto.',
                                        par$cod_identificador   => '');

                                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                        COD_CAMPO2,
                                                                        COD_CAMPO3,
                                                                        COD_CALIFICADOR)
                                            VALUES ('2040100006',
                                                    gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                                    '2040100006',
                                                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                    NULL,
                                                                                    0),
                                                    rec$movimiento.cod_externo,
                                                    'VALIDACIONES');
                                    ELSIF rec$periodo.cod_tipo_periodo = 'RE' AND rec$periodo.cod_estado_periodo <> 'EC' THEN /*Ver si tambien aplica para boveda y recojo*/
                                        /* 2040100006 - El movimiento informado no puede ser quitado de un período distinto de 'abierto'. */
                                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => '2040100006 - El movimiento informado no puede ser quitado de un período distinto de en creacion.',
                                        par$cod_identificador   => '');

                                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                        COD_CAMPO2,
                                                                        COD_CAMPO3,
                                                                        COD_CALIFICADOR)
                                            VALUES ('2040100006',
                                                    gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                                    '2040100006',
                                                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                    NULL,
                                                                                    0),
                                                    rec$movimiento.cod_externo,
                                                    'VALIDACIONES');
                                    ELSIF rec$periodo.cod_tipo_periodo = 'BO' AND rec$periodo.cod_estado_periodo <> 'EC' THEN /*Ver si tambien aplica para boveda y recojo*/
                                        /* 2040100006 - El movimiento informado no puede ser quitado de un período distinto de 'abierto'. */
                                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => '2040100006 - El movimiento informado no puede ser quitado de un período distinto de en creacion.',
                                        par$cod_identificador   => '');

                                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                        COD_CAMPO2,
                                                                        COD_CAMPO3,
                                                                        COD_CALIFICADOR)
                                            VALUES ('2040100006',
                                                    gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                                    '2040100006',
                                                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                    NULL,
                                                                                    0),
                                                    rec$movimiento.cod_externo,
                                                    'VALIDACIONES');
                                    ELSE
                                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'El movimiento informado es valido, puede ser quitado.',
                                        par$cod_identificador   => '');


                                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                        COD_CAMPO2,
                                                                        COD_CAMPO3,
                                                                        COD_CAMPO4,
                                                                        COD_CAMPO8,
                                                                        COD_CALIFICADOR)
                                            VALUES (rec$movimiento.oid_documento,
                                                    rec$periodo.oid_periodo,
                                                    rec$movimiento.cod_externo,
                                                    var$oid_tipo_calculo,
                                                    rec$periodo.cod_tipo_periodo,
                                                    'MOVIMIENTOS_QUITAR');


                                    END IF; 
                            END LOOP;

                        ELSE
                          /* Indica que el movimiento deberá ser relacionado a un período. */
                          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Movimiento deberá ser relacionado a un período.',
                                        par$cod_identificador   => '');

                            IF var$oid_periodo IS NOT NULL THEN
                                /* 2040100008 - El movimiento ya está relacionado a un período. */
                                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => '2040100008 - El movimiento ya está relacionado a un período.',
                                        par$cod_identificador   => '');


                                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                COD_CAMPO2,
                                                                COD_CAMPO3,
                                                                COD_CALIFICADOR)
                                    VALUES ('2040100008',
                                            gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                            '2040100008',
                                                                            gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                            NULL,
                                                                            0),
                                            rec$movimiento.cod_externo,
                                            'VALIDACIONES');
                            ELSE
                                BEGIN
                                SELECT COUNT (1)
                                    INTO var$existe
                                    FROM SAPR_TPLANIFICACION PLAN
                                        INNER JOIN SAPR_TPLANXCANAL PLCA ON PLCA.OID_PLANIFICACION = PLAN.OID_PLANIFICACION
                                        INNER JOIN GEPR_TCANAL CANA ON CANA.OID_CANAL = PLCA.OID_CANAL
                                WHERE     PLAN.OID_PLANIFICACION = var$oid_planificacion
                                        AND (CANA.COD_CANAL = rec$movimiento.cod_canal OR rec$movimiento.cod_canal = 'SF');
                                EXCEPTION
                                WHEN NO_DATA_FOUND THEN
                                    var$existe := 0;
                                END;

                                IF var$existe = 0 THEN
                                  /* 2040100009 - No fue posible relacionar el movimiento a un período. El movimiento tiene el canal "{0}" que no está relacionado a la planificación "{1}". */
                                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => '2040100009 - No fue posible relacionar el movimiento a un período. El movimiento tiene el canal "{0}" que no está relacionado a la planificación "{1}".',
                                        par$cod_identificador   => '');

                                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                    COD_CAMPO2,
                                                                    COD_CAMPO3,
                                                                    COD_CALIFICADOR)
                                        VALUES ('2040100009',
                                                gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                                    '2040100009',
                                                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                    rec$movimiento.cod_canal || '|' || var$cod_planificacion,
                                                                                    0),
                                                rec$movimiento.cod_externo,
                                                'VALIDACIONES');
                                ELSE


                                    BEGIN
                                        SELECT TO_DATE (AUX.OID_CAMPO7, 'DD/MM/YYYY HH24:MI:SS')
                                        INTO var$fyh_gestion
                                        FROM SAPR_GTT_TAUXILIAR AUX
                                        WHERE AUX.COD_CALIFICADOR = 'MOVIMIENTO';

                                        IF var$cod_tipo_plani = 'FECHA_VALOR_CONFIR'  THEN
                                          BEGIN
                                            SELECT TPRO.COD_PROCESO
                                              INTO var$cod_proceso
                                            FROM SAPR_TPROCESOXPLANIFICACION TPXP
                                              INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO AND TPRO.COD_PROCESO = 'BBVA_PERU'
                                            WHERE TPXP.OID_PLANIFICACION = var$oid_planificacion;
                                          EXCEPTION
                                            WHEN NO_DATA_FOUND THEN
                                              var$cod_proceso := NULL;
                                          END;
                                          IF var$cod_proceso = 'BBVA_PERU' THEN 
                                            /*Obtengo datos adicionales del documento*/
                                            BEGIN
                                              SELECT DOCU.COD_COLLECTION_ID, CUEN.OID_SUBCANAL, MAX(EXD.OID_DIVISA)
                                              INTO var$doc_collection_id, var$doc_oid_subcanal, var$doc_oid_divisa FROM SAPR_TDOCUMENTO DOCU
                                              INNER JOIN SAPR_TCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_ORIGEN
                                              LEFT JOIN SAPR_TEFECTIVOXDOCUMENTO EXD ON EXD.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
                                              WHERE DOCU.OID_DOCUMENTO = rec$movimiento.OID_DOCUMENTO 
                                              GROUP BY DOCU.COD_COLLECTION_ID, CUEN.OID_SUBCANAL;
                                            EXCEPTION
                                            WHEN NO_DATA_FOUND THEN
                                                var$doc_collection_id := NULL;
                                                var$doc_oid_subcanal := NULL;
                                                var$doc_oid_divisa := NULL;
                                            END;
                                            /* Buscar periodos relacionados al documento (para relacionar)*/
                                            BEGIN
                                                INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                                                (SELECT P.OID_PERIODO, EP.COD_ESTADO_PERIODO, TIPE.COD_TIPO_PERIODO, 'PERIODOS_RELACIONAR'
                                                                    FROM SAPR_TPERIODO P 
                                                                    INNER JOIN SAPR_TESTADO_PERIODO EP ON EP.OID_ESTADO_PERIODO = P.OID_ESTADO_PERIODO
                                                                    INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO
                                                                WHERE     P.OID_MAQUINA = var$oid_maquina
                                                                        AND EP.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB', 'EC') AND P.oid_acreditacion IS NULL
                                                                        AND (    
                                                                            (P.FYH_INICIO <= var$fyh_gestion AND (P.FYH_FIN >= var$fyh_gestion OR P.FYH_FIN IS NULL) ) 
                                                                            OR  (P.FYH_INICIO <= SYS_EXTRACT_UTC (CURRENT_TIMESTAMP) AND (P.FYH_FIN >= SYS_EXTRACT_UTC (CURRENT_TIMESTAMP) OR P.FYH_FIN IS NULL) ) 
                                                                        )
                                                                        AND ((P.OID_SUBCANAL = var$doc_oid_subcanal AND var$bol_per_subcanal = 1)  OR (P.OID_SUBCANAL IS NULL OR var$bol_per_subcanal = 0))
                                                                        AND ((P.OID_DIVISA = var$doc_oid_divisa AND var$bol_per_divisa = 1) OR (P.OID_DIVISA IS NULL OR var$bol_per_divisa = 0)) 
                                                                        AND (P.COD_COLLECTION_ID = var$doc_collection_id OR var$doc_collection_id IS NULL OR P.COD_COLLECTION_ID IS NULL)
                                                                );

                                            END;
                                          END IF;
                                        ELSE
                                          /* Buscar periodos relacionados al documento (para relacionar)*/
                                          BEGIN
                                          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CALIFICADOR)
                                          (SELECT P.OID_PERIODO, EP.COD_ESTADO_PERIODO, TIPE.COD_TIPO_PERIODO, 'PERIODOS_RELACIONAR'
                                                                  FROM SAPR_TPERIODO P 
                                                                  INNER JOIN SAPR_TESTADO_PERIODO EP ON EP.OID_ESTADO_PERIODO = P.OID_ESTADO_PERIODO
                                                                  INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = P.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' 
                                                              WHERE     P.OID_MAQUINA = var$oid_maquina
                                                                      AND EP.COD_ESTADO_PERIODO IN ('AB', 'BL', 'DB') AND P.oid_acreditacion IS NULL
                                                                      AND (    
                                                                          (P.FYH_INICIO <= var$fyh_gestion AND P.FYH_FIN >= var$fyh_gestion ) 
                                                                          OR  (P.FYH_INICIO <= SYS_EXTRACT_UTC (CURRENT_TIMESTAMP) AND P.FYH_FIN >= SYS_EXTRACT_UTC (CURRENT_TIMESTAMP) ) 
                                                                      )
                                                                      ORDER BY P.FYH_INICIO
                                                                      FETCH FIRST ROW ONLY
                                                              );
                                          END;
                                        END IF;

                                    FOR rec$periodo IN cur$periodos_relacionar LOOP
                                      IF rec$periodo.COD_TIPO_PERIODO = 'AC' THEN
                                          sverificar_periodo (par$oid_llamada            => NULL,
                                          par$cod_canal    => rec$movimiento.cod_canal,
                                          par$cod_subcanal             => rec$movimiento.COD_SUBCANAL,
                                          par$oid_divisa               => NULL, -- Se indica NULL ya que no se encontró llamada desde la aplicación
                                          par$cod_sector               => rec$movimiento.COD_SECTOR,
                                          par$oid_pto_servicio         => rec$movimiento.OID_PTO_SERVICIO,
                                          par$cod_ajeno                => NULL,
                                          par$fyh_gestion              => TO_DATE (rec$movimiento.FYH_GESTION, 'DD/MM/YYYY HH24:MI:SS'),
                                          par$tipo_periodo             => rec$periodo.COD_TIPO_PERIODO,
                                          par$cod_usuario              => var$usuario,
                                          par$cod_cultura              => par$cod_cultura,
                                          par$info_ejecucion           => var$info_ejecucion,
                                          par$oid_maquina              => var$oid_maquina,
                                          par$oid_planificacion        => var$oid_planificacion,
                                          par$oid_periodo              => rec$periodo.oid_periodo,
                                          par$cod_tipo_planificacion   => var$cod_tipo_planificacion,
                                          par$cod_validaciones         => var$cod_validaciones,
                                          par$des_validaciones         => var$des_validaciones,
                                          par$cod_ejecucion            => var$cod_ejecucion);


                                        IF var$cod_validaciones IS NOT NULL AND var$cod_validaciones.count > 0  THEN
                                          FOR idx IN var$cod_validaciones.FIRST .. var$cod_validaciones.LAST LOOP
                                              IF var$cod_validaciones (idx) IS NOT NULL THEN
                                                  INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                                      COD_CAMPO2,
                                                                                      COD_CAMPO3,
                                                                                      COD_CALIFICADOR)
                                                          VALUES (var$cod_validaciones (idx),
                                                                  var$des_validaciones (idx),
                                                                  rec$movimiento.cod_externo,
                                                                  'VALIDACIONES');
                                              END IF;
                                          END LOOP;

                                        ELSE 


                                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                            COD_CAMPO2,
                                                                            COD_CAMPO3,
                                                                            COD_CAMPO4,
                                                                            COD_CAMPO8,
                                                                            COD_CALIFICADOR)
                                                VALUES (rec$movimiento.oid_documento,
                                                        rec$periodo.oid_periodo,
                                                        rec$movimiento.cod_externo,
                                                        var$oid_tipo_calculo,
                                                        rec$periodo.cod_tipo_periodo,
                                                        'MOVIMIENTOS_RELACIONAR');

                                        END IF;
                                      ELSE
                                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                            COD_CAMPO2,
                                                                            COD_CAMPO3,
                                                                            COD_CAMPO4,
                                                                            COD_CAMPO8,
                                                                            COD_CALIFICADOR)
                                                VALUES (rec$movimiento.oid_documento,
                                                        rec$periodo.oid_periodo,
                                                        rec$movimiento.cod_externo,
                                                        var$oid_tipo_calculo,
                                                        rec$periodo.cod_tipo_periodo,
                                                        'MOVIMIENTOS_RELACIONAR');
                                      END IF;
                                  END LOOP;
                                    EXCEPTION
                                        WHEN NO_DATA_FOUND THEN
                                        DBMS_OUTPUT.put_line (
                                            '2040100011 - No fue posible relacionar el movimiento a un período. La Planificacion no tiene período abierto.');

                                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                                                        COD_CAMPO2,
                                                                        COD_CAMPO3,
                                                                        COD_CALIFICADOR)
                                            VALUES ('2040100011',
                                                    gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                                                        '2040100011',
                                                                                        gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                                        rec$movimiento.cod_canal || '|' || var$cod_planificacion,
                                                                                        0),
                                                    rec$movimiento.cod_externo,
                                                    'VALIDACIONES');
                                    END;

                                END IF;
                            END IF;
                        END IF;

                END IF;
              END IF;
            END LOOP rec$movimiento;
          END IF;

        END IF;
      END LOOP;
    END IF;
  END svalidar_periodo_baja;

  PROCEDURE srelacionar_mov_periodo_baja (par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$abol_baja            IN     gepr_pcomon_###VERSION###.tipo$nbols_,
                                     par$abol_codigo_simple   IN     gepr_pcomon_###VERSION###.tipo$nbols_,
                                     par$acod_movimiento      IN     gepr_pcomon_###VERSION###.tipo$cods_,
                                     par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_,
                                     par$rc_validaciones         OUT SYS_REFCURSOR,
                                     par$cod_ejecucion           OUT gepr_pcomon_###VERSION###.tipo$cod_) IS
    const$nombre_func   CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srelacionar_mov_periodo_baja';
    var$des_validaciones         gepr_pcomon_###VERSION###.tipo$descs_;

     --Variables de LOG
    const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.srelacionar_mov_periodo_baja';
		var$detalle           VARCHAR2(4000);
    /* LOG */
    var$inicio                   TIMESTAMP := SYSTIMESTAMP;
    var$registros                gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges                   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects                  gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err                      VARCHAR2 (4000);

    /* Movimientos */
    CURSOR cur$movimientos (par$cod_calificador IN gepr_pcomon_###VERSION###.tipo$cod_) IS
      SELECT OID_CAMPO1 OID_DOCUMENTO,
             COD_CAMPO2 OID_PERIODO,
             COD_CAMPO3 COD_EXTERNO,
             COD_CAMPO4 OID_TIPO_CALCULO,
             COD_CAMPO8 COD_TIPO_PERIODO
        FROM SAPR_GTT_TAUXILIAR D
       WHERE COD_CALIFICADOR = par$cod_calificador;
  BEGIN
    /* limpiar tabla temporaria */
    DELETE SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR IN ('MOVIMIENTOS_QUITAR', 'MOVIMIENTOS_RELACIONAR', 'VALIDACIONES') ;

    var$deletes := var$deletes + SQL%ROWCOUNT;

    /* #### Inicializar los cursores #### */
    OPEN par$rc_validaciones FOR
      SELECT NULL CODIGO
        FROM DUAL
       WHERE 1 <> 1;

      IF par$oid_llamada is not null THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Parametros iniciales: ' ||
                                            ' par$oid_llamada: ' || par$oid_llamada ||
                                            ', par$cod_cultura: ' || par$cod_cultura ||
                                            ', par$info_ejecucion: ' || par$info_ejecucion ||
                                            ', par$cod_usuario: ' || par$cod_usuario ,
                            par$cod_identificador   => '');

        /* INICIO LOG de arrays asociativos */
        IF par$abol_baja IS NOT NULL AND par$abol_baja.COUNT > 0 THEN
          FOR idx IN  par$abol_baja.first ..  par$abol_baja.last LOOP 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'Parametros iniciales: ' || 'par$abol_baja(' || idx || '):  ' || par$abol_baja(idx),
                              par$cod_identificador   => '');
          END LOOP;
        END IF;
        IF par$abol_codigo_simple IS NOT NULL AND par$abol_codigo_simple.COUNT > 0 THEN
          FOR idx IN  par$abol_codigo_simple.first ..  par$abol_codigo_simple.last LOOP 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'Parametros iniciales: ' || 'par$abol_codigo_simple(' || idx || '):  ' || par$abol_codigo_simple(idx),
                              par$cod_identificador   => '');
          END LOOP;
        END IF;
        IF par$acod_movimiento IS NOT NULL AND par$acod_movimiento.COUNT > 0 THEN
          FOR idx IN  par$acod_movimiento.first ..  par$acod_movimiento.last LOOP 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'Parametros iniciales: ' || 'par$acod_movimiento(' || idx || '):  ' || par$acod_movimiento(idx),
                              par$cod_identificador   => '');
          END LOOP;
        END IF;

      END IF;



    /* Llamar la procedure responsable por validar las informaciones */
    svalidar_periodo_baja (par$oid_llamada         => par$oid_llamada,
                          par$abol_baja            => par$abol_baja,
                          par$abol_codigo_simple   => par$abol_codigo_simple,
                          par$acod_movimiento      => par$acod_movimiento,
                          par$cod_cultura          => par$cod_cultura);

    /* Movimientos para quitar */
    FOR rec$movimiento IN cur$movimientos ('MOVIMIENTOS_QUITAR') LOOP

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Llamada a scalculo_efectivo: ' ||
                                            ' par$oid_documento: ' || rec$movimiento.oid_documento ||
                                            ', par$oid_tipo_calculo: ' || rec$movimiento.oid_tipo_calculo ||
                                            ', par$cod_tipo_periodo: ' || rec$movimiento.COD_TIPO_PERIODO ||
                                            ', par$cod_usuario: ' || par$cod_usuario, 
                            par$cod_identificador   => '');

      scalculo_efectivo (par$oid_documento      => rec$movimiento.oid_documento,
                         par$oid_tipo_calculo   => rec$movimiento.oid_tipo_calculo,
                         par$cod_tipo_periodo   => rec$movimiento.COD_TIPO_PERIODO,
                         par$cod_usuario        => par$cod_usuario,
                         par$bol_sumar          => 0);

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Llamada a scalculo_medio_pago: ' ||
                                            ' par$oid_documento: ' || rec$movimiento.oid_documento ||
                                            ', par$oid_tipo_calculo: ' || rec$movimiento.oid_tipo_calculo ||
                                            ', par$cod_tipo_periodo: ' || rec$movimiento.COD_TIPO_PERIODO ||
                                            ', par$cod_usuario: ' || par$cod_usuario, 
                            par$cod_identificador   => '');

      scalculo_medio_pago (par$oid_documento      => rec$movimiento.oid_documento,
                           par$oid_tipo_calculo   => rec$movimiento.oid_tipo_calculo,
                           par$cod_tipo_periodo   => rec$movimiento.COD_TIPO_PERIODO,
                           par$cod_usuario        => par$cod_usuario,
                           par$bol_sumar          => 0);

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Llamada a sdel_tperiodoxdocumento: ' ||
                                            ' par$oid_documento: ' || rec$movimiento.oid_documento ||
                                            ', par$oid_periodo: ' || rec$movimiento.oid_periodo,
                            par$cod_identificador   => '');
      sdel_tperiodoxdocumento (par$oid_documento => rec$movimiento.oid_documento, par$oid_periodo => rec$movimiento.oid_periodo);

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => '0040100001 - El movimiento fue quitado con éxito del período. [' || rec$movimiento.cod_externo || ']',
                            par$cod_identificador   => '');

      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                      COD_CAMPO2,
                                      COD_CAMPO3,
                                      COD_CALIFICADOR)
           VALUES ('0040100001',
                   gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                    '0040100001',
                                                    gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                    NULL,
                                                    0),
                   rec$movimiento.cod_externo,
                   'VALIDACIONES');
    END LOOP rec$movimiento;

    /* Movimientos para relacionar */
    FOR rec$movimiento IN cur$movimientos ('MOVIMIENTOS_RELACIONAR') LOOP
        srelacionar_periodo (par$oid_llamada            => par$oid_llamada,
                            par$oid_documento  => rec$movimiento.oid_documento,
                            par$oid_periodo     => rec$movimiento.oid_periodo,
                            par$cod_usuario     => par$cod_usuario,
                            par$cod_cultura     => par$cod_cultura,
                            par$info_ejecucion  => par$info_ejecucion,
                            par$hacer_commit    => 1,
                            par$validaciones    => var$des_validaciones,
                            par$cod_ejecucion   => par$cod_ejecucion);

      DBMS_OUTPUT.put_line ('0040100002 - El movimiento fue relacionado con éxito al período. [' || rec$movimiento.cod_externo || ']');

      IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc (var$des_validaciones) THEN
        FOR idxV IN var$des_validaciones.first .. var$des_validaciones.last loop
            INSERT INTO
            SAPR_GTT_TAUXILIAR 
            (
              OID_CAMPO1,
              COD_CAMPO2,
              COD_CAMPO3,
              COD_CALIFICADOR
            )
            SELECT
              '2040100001',
              nvl(var$des_validaciones(idxV), ' '),
              rec$movimiento.cod_externo, 
              'VALIDACIONES'
            FROM DUAL;
        end loop;
      ELSE
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1,
                                        COD_CAMPO2,
                                        COD_CAMPO3,
                                        COD_CALIFICADOR)
        VALUES ('0040100002',
                gepr_putilidades_###VERSION###.ftraduzir (par$cod_cultura,
                                                  '0040100002',
                                                  gepr_pcomon_###VERSION###.const$CodFunRelacionarMovPeri,
                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                  NULL,
                                                  0),
                rec$movimiento.cod_externo,
                'VALIDACIONES');
      END IF;
    END LOOP rec$movimiento;

    /* Validaciones */
    OPEN par$rc_validaciones FOR
      SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION, COD_CAMPO3 AS COD_MOVIMIENTO
        FROM SAPR_GTT_TAUXILIAR
       WHERE COD_CALIFICADOR = 'VALIDACIONES';

    var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$codFuncionalidad || '.' || const$nombre_func,
                                                      var$inserts,
                                                      var$updates,
                                                      var$deletes,
                                                      var$merges,
                                                      var$selects,
                                                      var$registros,
                                                      var$inicio,
                                                      NULL,
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
     gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in (const$codFuncionalidad || '.' || const$nombre_func,
                                                        var$inserts,
                                                        var$updates,
                                                        var$deletes,
                                                        var$merges,
                                                        var$selects,
                                                        var$registros,
                                                        var$inicio,
                                                        NULL,
                                                        'R',
                                                        par$info_ejecucion,
                                                        var$err,
                                                        par$cod_usuario,
                                                        par$cod_ejecucion);
  END srelacionar_mov_periodo_baja;

  /* Se encarga de modificar el estado de los periodos segun la acción indicada */
  PROCEDURE smodificar_periodos
  (
    par$aoid_periodo IN gepr_pcomon_###VERSION###.tipo$oids_,
    par$aindex  IN gepr_pcomon_###VERSION###.tipo$nels_,
    par$aaccion IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$acod_pais IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$acod_device_id IN gepr_pcomon_###VERSION###.tipo$cods_,
    par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$usr_,
    par$rc_periodos                OUT SYS_REFCURSOR
  ) IS
      const$nombre_func   CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'smodificar_periodos';
    const$accion_desbloquear   CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'DESBLOQUEAR';
    var$cod_resultado gepr_pcomon_###VERSION###.tipo$cod_;
    var$des_resultado gepr_pcomon_###VERSION###.tipo$desc_;
    var$acodigo gepr_pcomon_###VERSION###.tipo$cods_;
  begin


    /* Limpiar tabla auxiliar */
    DELETE FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'MODIFICAR_PERIODOS';

    /* #### Inicializar los cursores #### */
    OPEN par$rc_periodos FOR
      SELECT NULL CODIGO
        FROM DUAL
       WHERE 1 <> 1;

    IF par$aindex IS NOT NULL OR par$aindex.count>0 THEN
      FOR idx in par$aindex.first .. par$aindex.last LOOP
        if UPPER(par$aaccion(idx)) = const$accion_desbloquear  then

          var$cod_resultado := '';
          var$des_resultado := '';
          sdesbloquear_periodo(
            par$oid_periodo => par$aoid_periodo(idx),
            par$cod_pais => par$acod_pais(idx),
            par$cod_device_id => par$acod_device_id(idx),
            par$cod_usuario => par$cod_usuario,
            par$codigo => var$cod_resultado,
            par$descripcion => var$des_resultado
          );

          INSERT INTO SAPR_GTT_TAUXILIAR
          (
            OID_CAMPO1, COD_CALIFICADOR, COD_CAMPO2,
            COD_CAMPO3, OID_CAMPO7, COD_CAMPO4,
            COD_CAMPO8
          )
          VALUES
          (
            par$aindex(idx), 'MODIFICAR_PERIODOS', var$cod_resultado,
            var$des_resultado, par$aoid_periodo(idx), par$acod_pais(idx),
            par$acod_device_id(idx)
          );

        else
          /*elseif */
          DBMS_OUTPUT.PUT_LINE('A futuro tendremos más tipos de acciones');
        end if;
      END LOOP;
    END IF;


    OPEN par$rc_periodos FOR
      SELECT 
        OID_CAMPO1 INDICE,
        OID_CAMPO7 OID_PERIODO,
        COD_CAMPO2 COD_RESPUESTA,
        COD_CAMPO3 DES_RESPUESTA,
        COD_CAMPO4 COD_PAIS,
        COD_CAMPO8 DEVICE_ID
      FROM
        SAPR_GTT_TAUXILIAR
      WHERE
        COD_CALIFICADOR = 'MODIFICAR_PERIODOS';

  exception
    when others then
      DBMS_OUTPUT.PUT_LINE('Error en: ' || const$codFuncionalidad || '.' || const$nombre_func);
  end smodificar_periodos;

  PROCEDURE screar_periodo(
    par$oid_llamada               IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_tipo_periodo          IN gepr_pcomon_###VERSION###.tipo$cod_, 
    par$cod_periodo_confirmacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
    par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_)  IS

    --Variables de LOG
    const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.screar_periodo';
		var$detalle           VARCHAR2(4000);

    -- Identificadores de estado de período
		var$oid_estado_periodo_AB   gepr_pcomon_###VERSION###.tipo$oid_; --ABIERTO
		var$oid_estado_periodo_EC   gepr_pcomon_###VERSION###.tipo$oid_; --EN CREACION

    -- Identificadores de tipo de período
		var$oid_tipo_periodo_RE     gepr_pcomon_###VERSION###.tipo$oid_; --RECOJO
		var$oid_tipo_periodo_BO     gepr_pcomon_###VERSION###.tipo$oid_; --BOVEDA

    --Variables locales
    var$existe 					gepr_pcomon_###VERSION###.tipo$nel_;
    var$oid_periodo     gepr_pcomon_###VERSION###.tipo$oid_;
    var$fecha_fin       gepr_pcomon_###VERSION###.tipo$fyh_;
    var$oid_periodo_bo  gepr_pcomon_###VERSION###.tipo$oid_;
    var$fecha_fin_bo    gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fecha_inicio_bo gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fecha_inicio_re gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fecha_fin_re    gepr_pcomon_###VERSION###.tipo$fyh_;
    var$oid_periodo_re  gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_periodo_confirmacion gepr_pcomon_###VERSION###.tipo$cod_;

    --Variables para el procedure srelacionar_periodo
    var$validaciones    gepr_pcomon_###VERSION###.tipo$descs_;
    var$cod_ejecucion   gepr_pcomon_###VERSION###.tipo$nel_;

    --Cursor utilizado para recuperar los documentos del período de acreditación, en el tratamiento de períodos de recojo
    -- Los datos de la tabla SAPR_GTT_TAUXILIAR son generados en el procedure SAPR_PSERVICIO.sconfirmar_periodos
    CURSOR cur$documentos_ac IS
      SELECT
        DOCU.OID_DOCUMENTO,
        DOCU.FYH_GESTION FYH_MOVIMIENTO,
        'CASHIN' AS FORMULARIO,
        PERI.OID_MAQUINA,
        PERI.OID_PLANIFICACION,
        PERI.OID_PERIODO
      FROM SAPR_TPERIODOXDOCUMENTO PEDI
        INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PEDI.OID_PERIODO
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = PERI.OID_PERIODO AND COD_CALIFICADOR = 'TIPO_PERIODO_AC'
        INNER JOIN SAPR_TDOCUMENTO DOCU ON PEDI.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
        INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOCU.OID_FORMULARIO 
          AND FORMU.COD_FORMULARIO IN ('MAECIC','MAECID')
      UNION
      SELECT
        MOSH.OID_DOCUMENTO,
        MOSH.FYH_GESTION FYH_MOVIMIENTO,
        'SHIPOUT' AS FORMULARIO,
        PERI.OID_MAQUINA,
        PERI.OID_PLANIFICACION,
        PERI.OID_PERIODO
      FROM SAPR_TMOVIMIENTO_SHIPOUT MOSH
        INNER JOIN SAPR_VCUENTA CUEN ON MOSH.OID_CUENTA_SALDO = CUEN.OID_CUENTA
        INNER JOIN SAPR_TPERIODO PERI ON CUEN.OID_MAQUINA = PERI.OID_MAQUINA AND
        MOSH.FYH_GESTION >= PERI.FYH_INICIO AND
        MOSH.FYH_GESTION <= PERI.FYH_FIN
        INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = PERI.OID_PERIODO AND COD_CALIFICADOR = 'TIPO_PERIODO_AC'
      ORDER BY 2 ASC;

   --Cursor utilizado para recuperar los documentos del período de acreditación relacionados al período de recojo, en el tratamiento de períodos de bóveda
    CURSOR cur$documentos_re (par$cod_per_confirmacion_ac gepr_pcomon_###VERSION###.tipo$cod_, par$oid_periodo_re gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT
        DOCU.OID_DOCUMENTO,
        DOCU.FYH_GESTION FYH_MOVIMIENTO,
        'CASHIN' AS FORMULARIO,
        PERI.OID_MAQUINA,
        PERI.OID_PLANIFICACION,
        PERI.OID_PERIODO AS OID_PERIODO_AC
      FROM SAPR_TPERIODOXDOCUMENTO PEDI
        INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PEDI.OID_PERIODO
        INNER JOIN SAPR_TDOCUMENTO DOCU ON PEDI.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
        INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOCU.OID_FORMULARIO 
          AND FORMU.COD_FORMULARIO IN ('MAECIC','MAECID')
        INNER JOIN SAPR_TPERIODOXDOCUMENTO PEDRE ON PEDRE.OID_DOCUMENTO = DOCU.OID_DOCUMENTO
            AND PEDRE.OID_PERIODO = par$oid_periodo_re --RECOJO
       WHERE PERI.COD_PERIODO_CONFIRMACION  = par$cod_per_confirmacion_ac --ACREDITACION
      UNION
      SELECT
        MOSH.OID_DOCUMENTO,
        MOSH.FYH_GESTION FYH_MOVIMIENTO,
        'SHIPOUT' AS FORMULARIO,
        PERI.OID_MAQUINA,
        PERI.OID_PLANIFICACION,
        PERI.OID_PERIODO AS OID_PERIODO_AC
      FROM SAPR_TMOVIMIENTO_SHIPOUT MOSH
        INNER JOIN SAPR_VCUENTA CUEN ON MOSH.OID_CUENTA_SALDO = CUEN.OID_CUENTA
        INNER JOIN SAPR_TPERIODO PERI ON CUEN.OID_MAQUINA = PERI.OID_MAQUINA AND
        MOSH.FYH_GESTION >= PERI.FYH_INICIO AND
        MOSH.FYH_GESTION <= PERI.FYH_FIN
        INNER JOIN SAPR_TPERIODOXDOCUMENTO PEDRE ON PEDRE.OID_DOCUMENTO = MOSH.OID_DOCUMENTO
            AND PEDRE.OID_PERIODO = par$oid_periodo_re --RECOJO
      WHERE
        PERI.COD_PERIODO_CONFIRMACION  = par$cod_per_confirmacion_ac --ACREDITACION*/
      ORDER BY 2 ASC;

  BEGIN
    -- INICIO - LOG de parametros iniciales
		IF par$oid_llamada IS NOT NULL THEN
			var$detalle := q'[Parametros iniciales: 
        par$oid_llamada: ]'               || par$oid_llamada || q'[
        par$cod_tipo_periodo: ]'          || par$cod_tipo_periodo || q'[
        par$cod_periodo_confirmacion: ]'  || par$cod_periodo_confirmacion || q'[
        par$cod_usuario: ]'               || par$cod_usuario || q'[
        par$cod_cultura: ]'               || par$cod_cultura;

			SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
    END IF;
    -- FIN - LOG de parametros iniciales

    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_AB FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'AB';
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Carga var$oid_estado_periodo_AB: ' || var$oid_estado_periodo_AB,
                            par$cod_identificador   => '');

    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_EC FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'EC';
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Carga var$oid_estado_periodo_EC: ' || var$oid_estado_periodo_EC,
                            par$cod_identificador   => '');

    SELECT OID_TIPO_PERIODO INTO var$oid_tipo_periodo_RE FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'RE';
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Carga var$oid_tipo_periodo_RE: ' || var$oid_tipo_periodo_RE,
                            par$cod_identificador   => '');

    SELECT OID_TIPO_PERIODO INTO var$oid_tipo_periodo_BO FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'BO';
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Carga var$oid_tipo_periodo_BO: ' || var$oid_tipo_periodo_BO,
                            par$cod_identificador   => '');

    IF par$cod_tipo_periodo = 'RE' THEN
      FOR rec$documentos IN cur$documentos_ac LOOP
         --Verificar que exista un periodo de recojo en estado "En creación" con fecha de inicio < a la fecha de gestón del movimiento
          BEGIN
            SELECT PERI.OID_PERIODO INTO var$oid_periodo
            FROM SAPR_TPERIODO PERI
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
              AND TIPE.COD_TIPO_PERIODO = par$cod_tipo_periodo
            WHERE PERI.OID_MAQUINA = rec$documentos.OID_MAQUINA
            AND PERI.OID_PLANIFICACION = rec$documentos.OID_PLANIFICACION
            AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_EC
            AND PERI.FYH_INICIO < rec$documentos.FYH_MOVIMIENTO
            ;

            EXCEPTION
              WHEN NO_DATA_FOUND THEN
                var$oid_periodo := null;
          END;

        --Analizar si se trata de un shipout o cashin
        IF rec$documentos.FORMULARIO = 'CASHIN' THEN
          IF var$oid_periodo IS NULL THEN
            --* NO se encuentra un periodo de Recojo en estado En Creación
            BEGIN 
              -- Verificar que exista un período de Recojo anterior en estado abierto
              SELECT PERIS.OID_PERIODO, PERIS.FYH_FIN 
              INTO var$oid_periodo_re, var$fecha_fin_re
              FROM (
              SELECT PERI.OID_PERIODO, PERI.FYH_FIN 
                    FROM SAPR_TPERIODO PERI
                    INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
                      AND TIPE.COD_TIPO_PERIODO = par$cod_tipo_periodo
                    WHERE PERI.OID_MAQUINA = rec$documentos.OID_MAQUINA
                    AND PERI.OID_PLANIFICACION = rec$documentos.OID_PLANIFICACION
                    AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_AB
                    ORDER BY PERI.FYH_FIN DESC
                    ) PERIS
              WHERE ROWNUM <= 1
              ;
            EXCEPTION
              WHEN NO_DATA_FOUND THEN
              var$oid_periodo_re := NULL;
            END;
            IF var$oid_periodo_re IS NOT NULL THEN
              /*
              Si existe período de Recojo anterior abierto, 
              Crear el período de recojo con estado igual a "En creación", con fecha de inicio igual a la fecha fin del período de bóveda obtenido 
              más un segundo después
              */
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'SI existe período de Recojo anterior abierto',
                    par$cod_identificador   => '');

              var$fecha_inicio_re := var$fecha_fin_re + (1/60/60/24);

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se definde la fecha de creación del período de recojo en base a la fecha fin del período de recojo anterior abierto con OID_PERIODO: ' ||
                    var$oid_periodo_re || ' y fecha fin: ' || TO_CHAR(var$fecha_fin_re, 'YYYY-MM-DD HH24:MI:SS') || ' resultando de la suma de un segundo la fecha: ' || TO_CHAR(var$fecha_inicio_re, 'YYYY-MM-DD HH24:MI:SS'),
                    par$cod_identificador   => '');
            ELSE
              /*
              Si No existe período de recojo anterior abierto, 
              Crear el período de recojo con estado igual a "En creación", con fecha de inicio igual a la fecha del movimiento cashin obtenida 
              */
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'NO existe período de recojo anterior abierto',
                    par$cod_identificador   => '');

              var$fecha_inicio_re := rec$documentos.FYH_MOVIMIENTO;

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Se define la fecha de creación del período de recojo en base a la fecha del documento con OID_DOCUMENTO: ' ||
                    rec$documentos.OID_DOCUMENTO || ' y FYH_GESTION: ' || TO_CHAR(rec$documentos.FYH_MOVIMIENTO, 'YYYY-MM-DD HH24:MI:SS'),
                    par$cod_identificador   => '');

            END IF;

            BEGIN
              SELECT '1' || LPAD(SAPR_QCOD_PER_CON_SEQ.NEXTVAL, 14, '0')
                INTO var$cod_periodo_confirmacion
                FROM DUAL;
            EXCEPTION
              WHEN OTHERS THEN
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$des_origen,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Error al generar secuencia para el período de recojo: ' || SQLERRM,
                  par$cod_identificador   => '');
              RAISE;
            END;

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Se genera secuencia para el período de recojo: ' || var$cod_periodo_confirmacion,
                                        par$cod_identificador   => '');

            /*  
              Se llama al procedure sins_upd_tperiodo enviando par$oid_periodo = NULL, será tratado como un alta
              y retornará en el mismo parámetro el oid_periodo registrado. 
            */ 
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$des_origen,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Previo a crear el período de recojo',
                          par$cod_identificador   => '');


            sins_upd_tperiodo(par$oid_llamada           => par$oid_llamada,
                          par$oid_periodo               => var$oid_periodo,
                          par$oid_planificacion         => rec$documentos.OID_PLANIFICACION,
                          par$oid_maquina               => rec$documentos.OID_MAQUINA,
                          par$oid_estado_periodo        => var$oid_estado_periodo_EC,
                          par$fyh_inicio                => var$fecha_inicio_re,
                          par$fyh_fin                   => NULL,
                          par$oid_tipo_periodo          => var$oid_tipo_periodo_RE,
                          par$cod_periodo_confirmacion  => var$cod_periodo_confirmacion,
                          par$cod_usuario               => par$cod_usuario);


            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$des_origen,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Se crea período de recojo con oid_periodo: ' || var$oid_periodo,
                          par$cod_identificador   => '');
          END IF;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$des_origen,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'Previo a relacionar el documento de CASHIN al período de recojo',
                        par$cod_identificador   => '');

          --Se relaciona el documento al período de recojo y se suma los valores al SAPR_TCALCULO_EFECTIVO
          srelacionar_periodo (par$oid_llamada      =>  par$oid_llamada,
                                par$oid_documento    =>  rec$documentos.OID_DOCUMENTO,
                                par$oid_periodo      =>  var$oid_periodo,
                                par$cod_usuario      =>  par$cod_usuario,
                                par$cod_cultura      =>  par$cod_cultura,
                                par$info_ejecucion   =>  '',
                                par$hacer_commit     =>  0,
                                par$validaciones     =>  var$validaciones,
                                par$cod_ejecucion    =>  var$cod_ejecucion);

          IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN
            --Con errores al relacionar el documento al período
            raise_application_error(-20001, 'Error al relacionar el documento al período de recojo');
          END IF;           


        ELSIF rec$documentos.FORMULARIO = 'SHIPOUT' THEN
          --* SI se encuentra un periodo de Recojo en estado En Creación
          IF var$oid_periodo IS NOT NULL THEN
            /* Actualizar el período de recojo existente identificando la fecha fin con la fecha del movimiento
                Shipout, actualizar el estado del periodo a "Abierto" y relacionar el movimiento Shipout con dicho
                período de Recojo
            */

            /*  
              Se llama al procedure sins_upd_tperiodo enviando el var$oid_periodo e indicando los campos a modificar
              FYH_FIN, OID_ESTADO_PERIODO
            */ 
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Previo a actualizar período de recojo, con fecha fin y estado abierto',
              par$cod_identificador   => '');

            sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                        par$oid_periodo               => var$oid_periodo,
                        par$oid_planificacion         => NULL,
                        par$oid_maquina               => NULL,
                        par$oid_estado_periodo        => var$oid_estado_periodo_AB,
                        par$fyh_inicio                => NULL,
                        par$fyh_fin                   => rec$documentos.FYH_MOVIMIENTO,
                        par$oid_tipo_periodo          => NULL,
                        par$cod_periodo_confirmacion  => NULL,
                        par$cod_usuario               => par$cod_usuario);

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Previo a relacionar el documento de SHIPOUT al período de recojo',
              par$cod_identificador   => '');
            --Se relaciona el documento al período de recojo
            sins_tperiodoxdocumento (par$oid_documento   => rec$documentos.OID_DOCUMENTO,
                                     par$oid_periodo     => var$oid_periodo,
                                     par$cod_usuario     => par$cod_usuario);

          END IF;
        END IF;

        --Analizamos si es necesario relacionar el período de acreditación al período de recojo 
        IF var$oid_periodo IS NOT NULL THEN

          SELECT COUNT(1) INTO var$existe
          FROM SAPR_TPERIODO_RELACION
          WHERE OID_PERIODO = var$oid_periodo AND OID_PERIODO_RELACIONADO = rec$documentos.OID_PERIODO;

          IF var$existe = 0 THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$des_origen,
                                  par$des_version     => const$version,                            
                                  par$des_detalle     => 'Previo a relacionar el período de recojo al período de acreditación',
                                  par$cod_identificador   => '');
            --Relacionamos el período de Recojo creado al período de acreditación 
            -- par$oid_periodo              = PERIODO DE RECOJO       = SAPR_TPERIODO_RELACION.OID_PERIODO
            -- par$oid_periodo_relacionado  = PERIODO DE ACREDITACION = SAPR_TPERIODO_RELACION.OID_PERIODO_RELACIONADO
            sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo,
                                    par$oid_periodo_relacionado   => rec$documentos.OID_PERIODO,
                                    par$cod_usuario               => par$cod_usuario);
          END IF;
        END IF;
      END LOOP;
    ELSIF par$cod_tipo_periodo = 'BO' THEN
      --Recorremos los períodos de acreditación relacionados al período de recojo que se encuentran en estado ACREDITADO
      FOR rec$periodo IN (SELECT PERE.OID_PERIODO AS OID_PERIODO_RE, PERI.COD_PERIODO_CONFIRMACION AS IDENTIFICADOR
                          FROM SAPR_TPERIODO_RELACION PERE
                            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = PERE.OID_PERIODO AND COD_CALIFICADOR = 'TIPO_PERIODO_RE' 
                            INNER JOIN SAPR_TPERIODO PERI ON PERI.OID_PERIODO = PERE.OID_PERIODO_RELACIONADO AND PERI.FYH_INICIO =  (SELECT MIN(FYH_INICIO) FROM SAPR_TPERIODO WHERE COD_PERIODO_CONFIRMACION = PERI.COD_PERIODO_CONFIRMACION)
                            INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO AND ESPE.COD_ESTADO_PERIODO = 'AC'
                          ORDER BY PERI.FYH_INICIO) LOOP

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$des_origen,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Recorremos los documentos de los períodos de acreditación de COD_PERIODO_CONFIRMACION: ' || rec$periodo.IDENTIFICADOR 
                                          || ', relacionados al período de recojo de OID_PERIODO: ' || rec$periodo.OID_PERIODO_RE || ' que se encuentran en estado ACREDITADO',
                  par$cod_identificador   => '');

        --Recorremos los documentos relacionados al período de recojo
        FOR rec$documentos IN cur$documentos_re (par$cod_per_confirmacion_ac => rec$periodo.IDENTIFICADOR, 
                                                par$oid_periodo_re => rec$periodo.OID_PERIODO_RE) LOOP
          --Verificar que exista un periodo de boveda en estado "En creación"
          BEGIN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Verificar que exista un período de bóveda en estado En creación',
              par$cod_identificador   => '');

            -- PERI.OID_PERIODO             = Período de bóveda
            -- PERE.OID_PERIODO_RELACIONADO = Período de recojo
            SELECT PERI.OID_PERIODO INTO var$oid_periodo
            FROM SAPR_TPERIODO PERI
              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
                AND TIPE.COD_TIPO_PERIODO = par$cod_tipo_periodo
              INNER JOIN SAPR_TPERIODO_RELACION PERE ON PERE.OID_PERIODO = PERI.OID_PERIODO AND PERE.OID_PERIODO_RELACIONADO = rec$periodo.OID_PERIODO_RE
            WHERE PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_EC
            ;

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'SI se encuentra un período de Bóveda en estado En Creación con OID_PERIODO: ' || var$oid_periodo,
                par$cod_identificador   => '');

          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$oid_periodo := null;
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'NO se encuentra un período de Bóveda en estado En Creación',
                par$cod_identificador   => '');
          END;

          IF rec$documentos.FORMULARIO = 'CASHIN' THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$des_origen,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Documento CASHIN de OID_DOCUMENTO: ' || rec$documentos.OID_DOCUMENTO,
                  par$cod_identificador   => '');

            --reemplazo el valor de la variable con la Fecha y Hora del movimiento CASHIN 
            var$fecha_fin := rec$documentos.FYH_MOVIMIENTO;

            IF var$oid_periodo IS NULL THEN
              --* NO se encuentra un periodo de Bóveda en estado En Creación
              BEGIN
                -- Verificar que exista un período de Bóveda anterior en estado abierto
                SELECT PERIS.OID_PERIODO, PERIS.FYH_FIN 
                INTO var$oid_periodo_bo, var$fecha_fin_bo
                FROM (
                SELECT PERI.OID_PERIODO, PERI.FYH_FIN 
                FROM SAPR_TPERIODO PERI
                  INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
                  AND TIPE.COD_TIPO_PERIODO = par$cod_tipo_periodo
                WHERE PERI.OID_MAQUINA = rec$documentos.OID_MAQUINA
                  AND PERI.OID_PLANIFICACION = rec$documentos.OID_PLANIFICACION
                  AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_AB
                ORDER BY PERI.FYH_FIN DESC 
                ) PERIS
                WHERE ROWNUM <= 1;
              EXCEPTION
                WHEN NO_DATA_FOUND THEN
                var$oid_periodo_bo := NULL;
              END;

              IF var$oid_periodo_bo IS NOT NULL THEN
                /*
                Si existe período de Bóveda anterior abierto, 
                Crear el período de bóveda con estado igual a "En creación", con fecha de inicio igual a la fecha fin del período de bóveda obtenido 
                más un segundo después
                */
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$des_origen,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'SI existe período de Bóveda anterior abierto',
                      par$cod_identificador   => '');

                var$fecha_inicio_bo := var$fecha_fin_bo + (1/60/60/24);

                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$des_origen,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'Se definde la fecha de creación del período de bóveda en base a la fecha fin del período de bóveda anterior abierto con OID_PERIODO: ' ||
                      var$oid_periodo_bo || ' y fecha fin: ' || TO_CHAR(var$fecha_fin_bo, 'YYYY-MM-DD HH24:MI:SS') || ' resultando de la suma de un segundo la fecha: ' || TO_CHAR(var$fecha_inicio_bo, 'YYYY-MM-DD HH24:MI:SS'),
                      par$cod_identificador   => '');
              ELSE
                /*
                Si No existe período de Bóveda anterior abierto, 
                Crear el período de bóveda con estado igual a "En creación", con fecha de inicio igual a la fecha del movimiento cashin obtenida 
                */
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$des_origen,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'NO existe período de Bóveda anterior abierto',
                      par$cod_identificador   => '');

                var$fecha_inicio_bo := rec$documentos.FYH_MOVIMIENTO;

                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$des_origen,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'Se definde la fecha de creación del período de bóveda en base a la fecha del documento con OID_DOCUMENTO: ' ||
                      rec$documentos.OID_DOCUMENTO || ' y FYH_GESTION: ' || TO_CHAR(rec$documentos.FYH_MOVIMIENTO, 'YYYY-MM-DD HH24:MI:SS'),
                      par$cod_identificador   => '');
              END IF;

              BEGIN
                SELECT '1' || LPAD(SAPR_QCOD_PER_CON_SEQ.NEXTVAL, 14, '0')
                  INTO var$cod_periodo_confirmacion
                  FROM DUAL;
              EXCEPTION
                WHEN OTHERS THEN
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,                            
                    par$des_detalle     => 'Error al generar secuencia para el período de bóveda: ' || SQLERRM,
                    par$cod_identificador   => '');
                RAISE;
              END;

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                          par$des_origen      => const$des_origen,
                                          par$des_version     => const$version,                            
                                          par$des_detalle     => 'Se genera secuencia para el período de bóveda: ' || var$cod_periodo_confirmacion,
                                          par$cod_identificador   => '');


              /*  
                Se llama al procedure sins_upd_tperiodo enviando par$oid_periodo = NULL, será tratado como un alta
                y retornará en el mismo parámetro el oid_periodo registrado. 
              */ 

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$des_origen,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'Previo a crear el período de bóveda',
                      par$cod_identificador   => '');

              sins_upd_tperiodo(par$oid_llamada           => par$oid_llamada,
                            par$oid_periodo               => var$oid_periodo,
                            par$oid_planificacion         => rec$documentos.OID_PLANIFICACION,
                            par$oid_maquina               => rec$documentos.OID_MAQUINA,
                            par$oid_estado_periodo        => var$oid_estado_periodo_EC,
                            par$fyh_inicio                => var$fecha_inicio_bo,
                            par$fyh_fin                   => NULL,
                            par$oid_tipo_periodo          => var$oid_tipo_periodo_BO,
                            par$cod_periodo_confirmacion  => var$cod_periodo_confirmacion,
                            par$cod_usuario               => par$cod_usuario);


              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Se crea período de bóveda con oid_periodo: ' || var$oid_periodo,
                            par$cod_identificador   => '');


              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Previo a relacionar el período de bóveda al período de recojo',
                            par$cod_identificador   => '');

              --Relacionamos el período de Bóveda creado al período de recojo 
              -- par$oid_periodo              = PERIODO DE BOVEDA      = SAPR_TPERIODO_RELACION.OID_PERIODO
              -- par$oid_periodo_relacionado  = PERIODO DE RECOJO      = SAPR_TPERIODO_RELACION.OID_PERIODO_RELACIONADO
              sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo,
                                      par$oid_periodo_relacionado   => rec$periodo.OID_PERIODO_RE,
                                      par$cod_usuario               => par$cod_usuario);

            END IF;
            --Verificamos si el periodo de acreditació del documento ya se encuentra relacionado el período de bóveda, si no lo está relacionarlo
            SELECT COUNT(1)
              INTO var$existe
            FROM SAPR_TPERIODO_RELACION
            WHERE OID_PERIODO = var$oid_periodo
            AND OID_PERIODO_RELACIONADO = rec$documentos.OID_PERIODO_AC;

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'El período de bóveda ' || CASE WHEN var$existe = 0 THEN 'NO' ELSE 'SI' END  || ' se encuentra relacionado al período de acreditación',
                            par$cod_identificador   => '');

            IF var$existe = 0 THEN
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                                par$des_origen      => const$des_origen,
                                                par$des_version     => const$version,                            
                                                par$des_detalle     => 'Previo a relacionar el período de bóveda al período de acreditación',
                                                par$cod_identificador   => '');

              --Relacionamos el período de Bóveda creado al período de acreditación 
              -- par$oid_periodo              = PERIODO DE BOVEDA       = SAPR_TPERIODO_RELACION.OID_PERIODO
              -- par$oid_periodo_relacionado  = PERIODO DE ACREDITACION = SAPR_TPERIODO_RELACION.OID_PERIODO_RELACIONADO
              sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo,
                                      par$oid_periodo_relacionado   => rec$documentos.OID_PERIODO_AC,
                                      par$cod_usuario               => par$cod_usuario);   
            END IF;

            --Relacionar el movimiento cashin con el período de bóveda
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$des_origen,
                      par$des_version     => const$version,                            
                      par$des_detalle     => 'Previo a relacionar el documento de CASHIN al período de bóveda',
                      par$cod_identificador   => '');

            --Se relaciona el documento al período de bóveda y se suma los valores al SAPR_TCALCULO_EFECTIVO
            srelacionar_periodo (par$oid_llamada      =>  par$oid_llamada,
                                  par$oid_documento    =>  rec$documentos.OID_DOCUMENTO,
                                  par$oid_periodo      =>  var$oid_periodo,
                                  par$cod_usuario      =>  par$cod_usuario,
                                  par$cod_cultura      =>  par$cod_cultura,
                                  par$info_ejecucion   =>  '',
                                  par$hacer_commit     =>  0,
                                  par$validaciones     =>  var$validaciones,
                                  par$cod_ejecucion    =>  var$cod_ejecucion);

            IF NOT gepr_putilidades_###VERSION###.farray_vacio_desc(var$validaciones) THEN
              --Con errores al relacionar el documento al período
              raise_application_error(-20001, 'Error al relacionar el documento CASHIN al período de bóveda. OID_DOCUMENTO: ' || rec$documentos.OID_DOCUMENTO);
            END IF;          

          ELSIF rec$documentos.FORMULARIO = 'SHIPOUT' THEN
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Documento SHIPOUT de OID_DOCUMENTO: ' || rec$documentos.OID_DOCUMENTO,
                par$cod_identificador   => '');

            -- Verificar que exista un período de Bóveda en estado "En Creación"
            IF var$oid_periodo IS NOT NULL THEN
              --* SI se encuentra un periodo de Bóveda en estado En Creación
              IF var$fecha_fin IS NOT NULL THEN
                /*  
                  Se llama al procedure sins_upd_tperiodo enviando el var$oid_periodo e indicando los campos a modificar
                  FYH_FIN, OID_ESTADO_PERIODO
                */ 
                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$des_origen,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Previo a actualizar período de bóveda, con fecha fin y estado abierto',
                  par$cod_identificador   => '');

                sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                            par$oid_periodo         => var$oid_periodo,
                            par$oid_planificacion         => NULL,
                            par$oid_maquina               => NULL,
                            par$oid_estado_periodo        => var$oid_estado_periodo_AB,
                            par$fyh_inicio                => NULL,
                            par$fyh_fin                   => var$fecha_fin,
                            par$oid_tipo_periodo          => NULL,
                            par$cod_periodo_confirmacion  => NULL,
                            par$cod_usuario               => par$cod_usuario);

                --Limpio la variable
                var$fecha_fin := NULL;
              END IF;
            END IF;
          END IF;
        END LOOP;
        IF var$fecha_fin IS NOT NULL THEN
          /*  
            Se llama al procedure sins_upd_tperiodo enviando el var$oid_periodo e indicando los campos a modificar
            FYH_FIN, OID_ESTADO_PERIODO
          */ 
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$des_origen,
            par$des_version     => const$version,                            
            par$des_detalle     => 'Previo a actualizar período de bóveda, con fecha fin y estado abierto',
            par$cod_identificador   => '');

          sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                      par$oid_periodo               => var$oid_periodo,
                      par$oid_planificacion         => NULL,
                      par$oid_maquina               => NULL,
                      par$oid_estado_periodo        => var$oid_estado_periodo_AB,
                      par$fyh_inicio                => NULL,
                      par$fyh_fin                   => var$fecha_fin,
                      par$oid_tipo_periodo          => NULL,
                      par$cod_periodo_confirmacion  => NULL,
                      par$cod_usuario               => par$cod_usuario);

          --Limpio la variable
          var$fecha_fin := NULL;
        END IF;
      END LOOP;
    END IF;

  EXCEPTION
    WHEN OTHERS THEN
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                        par$des_origen      => const$des_origen,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'EXCEPTION screar_periodo: ' || SQLERRM,
                        par$cod_identificador   => '');
      RAISE;

  END screar_periodo;



  PROCEDURE sins_upd_tperiodo(par$oid_llamada              IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_periodo           IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_planificacion         IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_maquina               IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_estado_periodo        IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$fyh_inicio                IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$fyh_fin                   IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$oid_tipo_periodo          IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$collection_id             IN gepr_pcomon_###VERSION###.tipo$cod_ := NULL,
                          par$oid_subcanal              IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                          par$oid_divisa                IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                          par$oid_pto_servicio          IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                          par$cod_periodo_confirmacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$cod_) AS
    var$gmt_zero   VARCHAR2 (50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$existe        gepr_pcomon_###VERSION###.tipo$nel_;
    var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_ ;
    --Variables de LOG
    const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sins_upd_tperiodo';
  BEGIN
    /* INICIO - LOG de parametros iniciales*/
    IF par$oid_llamada IS NOT NULL THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Parametros iniciales: ' ||
                                            ' par$oid_periodo: ' || par$oid_periodo ||
                                            ', par$oid_planificacion: ' || par$oid_planificacion ||
                                            ', par$oid_maquina: ' || par$oid_maquina ||
                                            ', par$oid_estado_periodo: ' || par$oid_estado_periodo ||
                                            ', par$fyh_inicio: ' || TO_CHAR(par$fyh_inicio, 'YYYY-MM-DD HH24:MI:SS') ||
                                            ', par$fyh_fin: ' || TO_CHAR(par$fyh_fin, 'YYYY-MM-DD HH24:MI:SS') ||
                                            ', par$oid_tipo_periodo: ' || par$oid_tipo_periodo ||
                                            ', par$collection_id: ' || par$collection_id ||
                                            ', par$oid_subcanal: ' || par$oid_subcanal ||
                                            ', par$oid_divisa: ' || par$oid_divisa ||
                                            ', par$oid_pto_servicio: ' || par$oid_pto_servicio ||
                                            ', par$cod_periodo_confirmacion: ' || par$cod_periodo_confirmacion ||
                                            ', par$cod_usuario: ' || par$cod_usuario,
                            par$cod_identificador   => '');

    END IF;
    /* FIN - LOG de parametros iniciales*/

    IF par$oid_periodo IS NOT NULL THEN
      BEGIN
        SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_TPERIODO
        WHERE OID_PERIODO = par$oid_periodo;
      EXCEPTION WHEN no_data_found THEN
        var$existe := 0;
      END;
    ELSE
          var$existe := 0;
          par$oid_periodo := SYS_GUID();
    END IF;

    IF var$existe = 0 THEN
      --Se trata de un ALTA
      INSERT INTO SAPR_TPERIODO(OID_PERIODO,
                            OID_PLANIFICACION,
                            OID_MAQUINA,
                            OID_ESTADO_PERIODO,
                            FYH_INICIO,
                            FYH_FIN,
                            OID_TIPO_PERIODO,
                            COD_COLLECTION_ID,
                            OID_SUBCANAL,
                            OID_DIVISA,
                            OID_PTO_SERVICIO,
                            COD_PERIODO_CONFIRMACION,
                            GMT_CREACION,
                            DES_USUARIO_CREACION,
                            GMT_MODIFICACION,
                            DES_USUARIO_MODIFICACION)
      VALUES (par$oid_periodo,
              par$oid_planificacion,
              par$oid_maquina,
              par$oid_estado_periodo,
              par$fyh_inicio,
              par$fyh_fin,
              par$oid_tipo_periodo,
              par$collection_id,
              par$oid_subcanal,
              par$oid_divisa,
              par$oid_pto_servicio,
              par$cod_periodo_confirmacion,
              var$gmt_zero,
              NVL(par$cod_usuario, 'SIN_USUARIO'),
              var$gmt_zero,
              NVL(par$cod_usuario, 'SIN_USUARIO')
              );
    ELSE
      --Se trata de una MODIFICACION
      -- Por el momento solo se indica la modificación de FYH_FIN, OID_ESTADO_PERIODO, COD_COLLECTION_ID y COD_PERIODO_CONFIRMACION
      IF  par$fyh_fin IS NOT NULL  THEN
          var$qry_update := var$qry_update || ' FYH_FIN = TO_DATE(''' || TO_CHAR(par$fyh_fin, 'YYYY-MM-DD HH24:MI:SS') || ''', ''YYYY-MM-DD HH24:MI:SS''), ';
      END IF;

      IF  par$oid_estado_periodo IS NOT NULL  THEN
          var$qry_update := var$qry_update || q'[ OID_ESTADO_PERIODO = ']' || par$oid_estado_periodo || q'[', ]';
      END IF;

      IF  par$collection_id IS NOT NULL  THEN
          var$qry_update := var$qry_update || q'[ COD_COLLECTION_ID = NVL(COD_COLLECTION_ID, ']' || par$collection_id || q'['), ]';
      END IF;

      IF  par$cod_periodo_confirmacion IS NOT NULL  THEN
          var$qry_update := var$qry_update || q'[ COD_PERIODO_CONFIRMACION = ']' || par$cod_periodo_confirmacion || q'[', ]';
      END IF;

/*Falta modificacion collection id*/
      IF  var$qry_update IS NOT NULL  THEN
        EXECUTE IMMEDIATE q'[ UPDATE SAPR_TPERIODO SET ]' || var$qry_update ||q'[
        GMT_MODIFICACION = SYSTIMESTAMP, DES_USUARIO_MODIFICACION = :1
        WHERE OID_PERIODO = :2]'
        USING  par$cod_usuario, par$oid_periodo;
      END IF;
    END IF;


  END sins_upd_tperiodo;


  PROCEDURE sins_tperiodo_relacion(par$oid_periodo        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$oid_periodo_relacionado     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_usuario           IN gepr_pcomon_###VERSION###.tipo$cod_) AS
    var$gmt_zero   VARCHAR2 (50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TPERIODO_RELACION(OID_PERIODO_RELACION,
                              OID_PERIODO,
                              OID_PERIODO_RELACIONADO,
                              GMT_CREACION,
                              DES_USUARIO_CREACION,
                              GMT_MODIFICACION,
                              DES_USUARIO_MODIFICACION)
    VALUES (SYS_GUID(),
            par$oid_periodo,
            par$oid_periodo_relacionado,
            var$gmt_zero,
            par$cod_usuario,
            var$gmt_zero,
            par$cod_usuario);

  END sins_tperiodo_relacion;

 PROCEDURE srelacionar_mov_shipout_per
                                    (par$oid_llamada          IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_documento        IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_) IS

  var$oid_documento           gepr_pcomon_###VERSION###.tipo$oid_; 
  var$oid_periodo_re          gepr_pcomon_###VERSION###.tipo$oid_; 
  var$oid_subcanal_peri       gepr_pcomon_###VERSION###.tipo$oid_; 
  var$oid_divisa_peri         gepr_pcomon_###VERSION###.tipo$oid_; 
  var$oid_pto_servicio_peri   gepr_pcomon_###VERSION###.tipo$oid_; 
  var$oid_periodo_re_new      gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_periodo_bo          gepr_pcomon_###VERSION###.tipo$oid_; 
  var$oid_periodo_bo_new      gepr_pcomon_###VERSION###.tipo$oid_;
  var$fyh_documento           gepr_pcomon_###VERSION###.tipo$fyh_;
  var$oid_estado_periodo_EC   gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_estado_periodo_AC   gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_estado_periodo_AB   gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_maquina             gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_planificacion       gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_tipo_periodo        gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_subcanal            gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_divisa              gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_pto_servicio        gepr_pcomon_###VERSION###.tipo$oid_;
  var$cod_collection_id       gepr_pcomon_###VERSION###.tipo$cod_;
  var$hay_otros_periodos      gepr_pcomon_###VERSION###.tipo$nel_;
  var$cant_no_acreditados     gepr_pcomon_###VERSION###.tipo$nel_;
  const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.srelacionar_mov_shipout_per';
  var$detalle           VARCHAR2(4000);
  var$cod_periodo_confirmacion gepr_pcomon_###VERSION###.tipo$cod_;
BEGIN

  IF par$oid_llamada IS NOT NULL THEN
    var$detalle := q'[Parametros iniciales: 
      par$oid_llamada: ]'       || par$oid_llamada || q'[
      par$oid_documento: ]'     || par$oid_documento || q'[
      par$cod_cultura: ]'       || par$cod_cultura || q'[
      par$info_ejecucion: ]'    || par$info_ejecucion || q'[
      par$cod_usuario: ]'       || par$cod_usuario;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$des_origen,
                          par$des_version     => const$version,                            
                          par$des_detalle     => var$detalle,
                          par$cod_identificador   => '');
  END IF;

  SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_EC FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'EC';
  SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_AC FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'AC';
  SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_AB FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'AB';



  /*Verifico si el documento es un shipout y es de una planificacion fv confirmacion con proceso bbva peru*/
  BEGIN 
    SELECT DOC.OID_DOCUMENTO, DOC.FYH_GESTION , MAQ.OID_MAQUINA, PLANI.OID_PLANIFICACION, DOC.COD_COLLECTION_ID, CUEN.OID_SUBCANAL, EFXDOC.OID_DIVISA, NULL
      INTO var$oid_documento, var$fyh_documento, var$oid_maquina, var$oid_planificacion, var$cod_collection_id, var$oid_subcanal, var$oid_divisa, var$oid_pto_servicio
    FROM SAPR_TDOCUMENTO DOC
      INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOC.OID_FORMULARIO
      INNER JOIN SAPR_VCUENTA CUEN ON CUEN.OID_CUENTA = DOC.OID_CUENTA_SALDO_ORIGEN
      INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_MAQUINA = CUEN.OID_MAQUINA AND MAQ.BOL_ACTIVO = 1
      INNER JOIN SAPR_TPLANXMAQUINA PXM ON PXM.OID_MAQUINA = MAQ.OID_MAQUINA AND PXM.BOL_ACTIVO = 1
      INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PXM.OID_PLANIFICACION AND PLANI.BOL_ACTIVO = 1
      INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION
      INNER JOIN SAPR_TPROCESOXPLANIFICACION TPXP ON TPXP.OID_PLANIFICACION = PLANI.OID_PLANIFICACION
      INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO
      LEFT JOIN SAPR_TEFECTIVOXDOCUMENTO EFXDOC ON DOC.OID_DOCUMENTO = EFXDOC.OID_DOCUMENTO 
    WHERE FORMU.COD_FORMULARIO IN ('MAESOC','MAESOD') 
      AND TPLANI.COD_TIPO_PLANIFICACION = 'FECHA_VALOR_CONFIR' 
      AND TPRO.COD_PROCESO = 'BBVA_PERU' 
      AND DOC.OID_DOCUMENTO = par$oid_documento
      AND ROWNUM = 1
      ;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => const$des_origen,
                    par$des_version     => const$version,                            
                        par$des_detalle     => 'Se recuperan datos del documento SHIPOUT - var$oid_documento: ' || var$oid_documento ||
                                              ', var$fyh_documento: ' || TO_CHAR(var$fyh_documento, 'YYYY-MM-DD HH24:MI:SS') ||
                                              ', var$oid_maquina: ' || var$oid_maquina ||
                                              ', var$oid_planificacion: ' || var$oid_planificacion ||
                                              ', var$cod_collection_id: ' || var$cod_collection_id ||
                                              ', var$oid_subcanal: ' || var$oid_subcanal ||
                                              ', var$oid_divisa: ' || var$oid_divisa ||
                                              ', var$oid_pto_servicio: ' || var$oid_pto_servicio
                                              ,
                    par$cod_identificador   => '');
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      var$oid_documento := NULL;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No se encontró el documento de SHIPOUT (''MAESOC'',''MAESOD'') de OID_DOCUMENTO = ' || par$oid_documento ||
                ' con COD_TIPO_PLANIFICACION = ''FECHA_VALOR_CONFIR'' y COD_PROCESO = ''BBVA_PERU'' '   
                ,
                par$cod_identificador   => '');

  END;

  IF var$oid_documento is not null THEN
    /*Comienzo Recojo*/
    BEGIN
      WITH RECOJOS AS (
        SELECT PERI.OID_PERIODO, PERI.OID_TIPO_PERIODO, PERI.COD_COLLECTION_ID, PERI.OID_SUBCANAL, PERI.OID_DIVISA, PERI.OID_PTO_SERVICIO, PERI.FYH_INICIO
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'RE'
          INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PERI.OID_PLANIFICACION 
        WHERE PERI.OID_MAQUINA = var$oid_maquina
          AND PERI.OID_PLANIFICACION = var$oid_planificacion
          AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_EC
          AND ((NVL(PERI.OID_SUBCANAL, '###VACIO###') = NVL(var$oid_subcanal, '###VACIO###') AND PLANI.BOL_PERIODO_SUBCANAL = 1) OR (PLANI.BOL_PERIODO_SUBCANAL = 0 AND PERI.OID_SUBCANAL IS NULL))
          AND ((NVL(PERI.OID_DIVISA, '###VACIO###') = NVL(var$oid_divisa, '###VACIO###') AND PLANI.BOL_PERIODO_DIVISA = 1) OR (PLANI.BOL_PERIODO_DIVISA = 0 AND PERI.OID_DIVISA IS NULL))
          AND NVL(PERI.OID_PTO_SERVICIO, '###VACIO###') = NVL(var$oid_pto_servicio, '###VACIO###')
          AND PERI.FYH_FIN IS NULL
          AND PERI.FYH_INICIO <= var$fyh_documento
        ),
        SELECCIONADO AS (
        SELECT OID_PERIODO, OID_TIPO_PERIODO, OID_SUBCANAL, OID_DIVISA, OID_PTO_SERVICIO, FYH_INICIO, 1 AS ORDEN
        FROM RECOJOS 
        WHERE COD_COLLECTION_ID = var$cod_collection_id
        UNION
        SELECT OID_PERIODO, OID_TIPO_PERIODO, OID_SUBCANAL, OID_DIVISA, OID_PTO_SERVICIO, FYH_INICIO, 2 AS ORDEN 
        FROM RECOJOS 
        WHERE COD_COLLECTION_ID IS NULL
        ORDER BY ORDEN, FYH_INICIO DESC
      )
      SELECT OID_PERIODO, OID_TIPO_PERIODO, OID_SUBCANAL, OID_DIVISA, OID_PTO_SERVICIO
        INTO var$oid_periodo_re, var$oid_tipo_periodo, var$oid_subcanal_peri, var$oid_divisa_peri, var$oid_pto_servicio_peri
      FROM SELECCIONADO
      WHERE ROWNUM <= 1;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$des_origen,
        par$des_version     => const$version,                            
                  par$des_detalle     => 'Se recuperan datos del período de recojo en creación - var$oid_periodo_re: ' || var$oid_periodo_re ||
                                          ', var$oid_tipo_periodo: ' || var$oid_tipo_periodo ||
                                          ', var$oid_subcanal_peri: ' || var$oid_subcanal_peri ||
                                          ', var$oid_divisa_peri: ' || var$oid_divisa_peri ||
                                          ', var$oid_pto_servicio_peri: ' || var$oid_pto_servicio_peri,
        par$cod_identificador   => '');

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
          var$oid_periodo_re := NULL;
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$des_origen,
            par$des_version     => const$version,                            
          par$des_detalle     => 'No hay período de recojo en creación',
            par$cod_identificador   => '');
    END;

    IF var$oid_periodo_re is not null  THEN
      BEGIN
        /*Si el periodo esta en estado "en creacion" relaciono el documento a ese periodo , actualiza la fecha fin */
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$des_origen,
        par$des_version     => const$version,                            
        par$des_detalle     => 'Previo a relacionar el documento al periodo de recojo',
        par$cod_identificador   => '');

        sins_tperiodoxdocumento (par$oid_documento   => var$oid_documento,
            par$oid_periodo     => var$oid_periodo_re,
            par$cod_usuario     => par$cod_usuario);

        /*
        Al configurar la fecha fin en el período de Recojo que hace parte del movimiento de ShipOut que se está dando de alta, 
        se deberá verificar si todos los períodos de Acreditación relacionados a este mismo período de Recojo estén en el estado "Acreditado". 
        Caso estén, entonces se deberá cambiar el estado del período de Recojo para "Abierto". 
        */
        BEGIN
          SELECT COUNT(1) AS CANTIDAD
            INTO var$cant_no_acreditados
          FROM SAPR_TPERIODO_RELACION PERE
            INNER JOIN SAPR_TPERIODO PERI ON PERE.OID_PERIODO_RELACIONADO = PERI.OID_PERIODO
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
            INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO AND ESPE.OID_ESTADO_PERIODO <> var$oid_estado_periodo_AC
          WHERE PERE.OID_PERIODO = var$oid_periodo_re
          ;
        END;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$des_origen,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Se recupera cantidad de períodos de Acreditación NO acreditados: ' ||  var$cant_no_acreditados || ' para el período de recojo: ' || var$oid_periodo_re,
                          par$cod_identificador   => '');

        IF var$cant_no_acreditados = 0 THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$des_origen,
                          par$des_version     => const$version,                            
                          par$des_detalle     => 'Previo a actualizar período de recojo, con fecha fin y estado Abierto',
                          par$cod_identificador   => '');


          sins_upd_tperiodo(par$oid_llamada  => par$oid_llamada,
            par$oid_periodo               => var$oid_periodo_re,
            par$oid_planificacion         => NULL,
            par$oid_maquina               => NULL,
            par$oid_estado_periodo        => var$oid_estado_periodo_AB,
            par$fyh_inicio                => NULL,
            par$fyh_fin                   => var$fyh_documento,
            par$oid_tipo_periodo          => NULL,
            par$collection_id             => var$cod_collection_id,
            par$cod_periodo_confirmacion  => NULL,
            par$cod_usuario               => par$cod_usuario);

        ELSE

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Previo a actualizar período de recojo, con fecha fin',
          par$cod_identificador   => '');


          sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
            par$oid_periodo         => var$oid_periodo_re,
            par$oid_planificacion         => NULL,
            par$oid_maquina               => NULL,
            par$oid_estado_periodo        => NULL,
            par$fyh_inicio                => NULL,
            par$fyh_fin                   => var$fyh_documento,
            par$oid_tipo_periodo          => NULL,
            par$collection_id             => var$cod_collection_id,
            par$cod_periodo_confirmacion  => NULL,
            par$cod_usuario               => par$cod_usuario);

        END IF;

        --buscar otros recojos para la misma maquina, subcanal, etc., que no tengan fecha fin y que este en creacion
        SELECT COUNT(1) INTO var$hay_otros_periodos
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
          INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PERI.OID_PLANIFICACION AND TIPE.COD_TIPO_PERIODO = 'RE'
        WHERE PERI.OID_MAQUINA = var$oid_maquina
          AND PERI.OID_PLANIFICACION = var$oid_planificacion
          AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_EC
          AND PERI.FYH_INICIO >= var$fyh_documento
          AND ((NVL(PERI.OID_SUBCANAL, '###VACIO###') = NVL(var$oid_subcanal, '###VACIO###') AND PLANI.BOL_PERIODO_SUBCANAL = 1) OR (PLANI.BOL_PERIODO_SUBCANAL = 0 AND PERI.OID_SUBCANAL IS NULL))
          AND ((NVL(PERI.OID_DIVISA, '###VACIO###') = NVL(var$oid_divisa, '###VACIO###') AND PLANI.BOL_PERIODO_DIVISA = 1) OR (PLANI.BOL_PERIODO_DIVISA = 0 AND PERI.OID_DIVISA IS NULL))
          AND NVL(PERI.OID_PTO_SERVICIO, '###VACIO###') = NVL(var$oid_pto_servicio, '###VACIO###')
          AND PERI.FYH_FIN IS NULL;

        --si NO encuentra, creamos un nuevo periodo de recojo
        IF var$hay_otros_periodos = 0 THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Previo a crear un nuevo periodo de recojo',
          par$cod_identificador   => '');

          /*Al crear un nuevo periodo de recojo, le asigno un sequence como cod_periodo_confirmacion*/
          SELECT '1' || LPAD(SAPR_QCOD_PER_CON_SEQ.NEXTVAL, 14, '0')
            INTO var$cod_periodo_confirmacion
          FROM DUAL;

          sins_upd_tperiodo(par$oid_llamada => par$oid_llamada,
              par$oid_periodo               => var$oid_periodo_re_new,
              par$oid_planificacion         => var$oid_planificacion,
              par$oid_maquina               => var$oid_maquina,
              par$oid_estado_periodo        => var$oid_estado_periodo_EC,
              par$fyh_inicio                => var$fyh_documento + (1/60/60/24),
              par$fyh_fin                   => NULL,
              par$oid_tipo_periodo          => var$oid_tipo_periodo,
              par$oid_subcanal              => var$oid_subcanal_peri,
              par$oid_divisa                => var$oid_divisa_peri,
              par$oid_pto_servicio          => var$oid_pto_servicio_peri,
              par$cod_periodo_confirmacion  => var$cod_periodo_confirmacion,
              par$cod_usuario               => par$cod_usuario);
        ELSE
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Ya existe un periodo de recojo en creacion sin fecha y hora fin',
          par$cod_identificador   => '');
        END IF;

      EXCEPTION
        WHEN OTHERS THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,                            
          par$des_detalle     => 'EXCEPTION srelacionar_mov_shipout_per: ' || SQLERRM,
          par$cod_identificador   => '');
          RAISE ;
      END;

    END IF;
    /*FIN RECOJO*/
    /*COMIENZO BOVEDA*/
    BEGIN
      /*Obtengo el periodo de BOVEDA en estado en creacion*/
      WITH BOVEDAS AS (
        SELECT PERI.OID_PERIODO , PERI.OID_TIPO_PERIODO, PERI.COD_COLLECTION_ID, PERI.OID_SUBCANAL, PERI.OID_DIVISA, PERI.OID_PTO_SERVICIO, PERI.FYH_INICIO
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
          INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PERI.OID_PLANIFICACION AND TIPE.COD_TIPO_PERIODO = 'BO'
        WHERE PERI.OID_MAQUINA = var$oid_maquina
          AND PERI.OID_PLANIFICACION = var$oid_planificacion
          AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_EC
          AND ((NVL(PERI.OID_SUBCANAL, '###VACIO###') = NVL(var$oid_subcanal, '###VACIO###') AND PLANI.BOL_PERIODO_SUBCANAL = 1) OR (PLANI.BOL_PERIODO_SUBCANAL = 0 AND PERI.OID_SUBCANAL IS NULL))
          AND ((NVL(PERI.OID_DIVISA, '###VACIO###') = NVL(var$oid_divisa, '###VACIO###') AND PLANI.BOL_PERIODO_DIVISA = 1) OR (PLANI.BOL_PERIODO_DIVISA = 0 AND PERI.OID_DIVISA IS NULL))
          AND NVL(PERI.OID_PTO_SERVICIO, '###VACIO###') = NVL(var$oid_pto_servicio, '###VACIO###')
          AND PERI.FYH_FIN IS NULL
          AND PERI.FYH_INICIO <= var$fyh_documento
        ),
        SELECCIONADO AS (
          SELECT OID_PERIODO, OID_TIPO_PERIODO, OID_SUBCANAL, OID_DIVISA, OID_PTO_SERVICIO, FYH_INICIO,  1 AS ORDEN
          FROM BOVEDAS 
            WHERE COD_COLLECTION_ID = var$cod_collection_id
        UNION
          SELECT OID_PERIODO, OID_TIPO_PERIODO, OID_SUBCANAL, OID_DIVISA, OID_PTO_SERVICIO, FYH_INICIO,  2 AS ORDEN
          FROM BOVEDAS 
            WHERE COD_COLLECTION_ID IS NULL
        ORDER BY ORDEN, FYH_INICIO DESC
      )
      SELECT OID_PERIODO, OID_TIPO_PERIODO, OID_SUBCANAL, OID_DIVISA, OID_PTO_SERVICIO
        INTO var$oid_periodo_bo, var$oid_tipo_periodo, var$oid_subcanal_peri, var$oid_divisa_peri, var$oid_pto_servicio_peri
      FROM SELECCIONADO
      WHERE ROWNUM <= 1;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
        par$des_origen      => const$des_origen,
        par$des_version     => const$version,                            
                          par$des_detalle     => 'Se recuperan datos del período de bóveda en creación - var$oid_periodo_bo: ' || var$oid_periodo_bo ||
                                                  ', var$oid_tipo_periodo: ' || var$oid_tipo_periodo ||
                                                  ', var$oid_subcanal_peri: ' || var$oid_subcanal_peri ||
                                                  ', var$oid_divisa_peri: ' || var$oid_divisa_peri ||
                                                  ', var$oid_pto_servicio_peri: ' || var$oid_pto_servicio_peri,
        par$cod_identificador   => '');

      EXCEPTION
          WHEN NO_DATA_FOUND THEN
            var$oid_periodo_bo := NULL;
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,                            
              par$des_detalle     => 'No hay periodo de bóveda en creación',
              par$cod_identificador   => '');
      END;
      IF var$oid_periodo_bo is not null  THEN
        BEGIN
          /*Si el periodo esta en estado "en creacion" relaciono el documento a ese periodo , lo paso a abierto y creo un nuevo periodo de boveda*/
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Previo a relacionar el documento al periodo de boveda',
          par$cod_identificador   => '');

          sins_tperiodoxdocumento (par$oid_documento   => var$oid_documento,
                    par$oid_periodo     => var$oid_periodo_bo,
                    par$cod_usuario     => par$cod_usuario);

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Previo a actualizar período de boveda, con fecha fin y cod_periodo_confirmacion',
          par$cod_identificador   => '');

          -- Al actualizar el período de bóveda, le asigno un sequence como cod_periodo_confirmacion*/
          SELECT '1' || LPAD(SAPR_QCOD_PER_CON_SEQ.NEXTVAL, 14, '0')
          INTO var$cod_periodo_confirmacion
          FROM DUAL;

          sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                par$oid_periodo         => var$oid_periodo_bo,
                par$oid_planificacion         => NULL,
                par$oid_maquina               => NULL,
                par$oid_estado_periodo        => NULL,
                par$fyh_inicio                => NULL,
                par$fyh_fin                   => var$fyh_documento,
                par$oid_tipo_periodo          => NULL,
                par$collection_id             => var$cod_collection_id,
                par$cod_periodo_confirmacion  => var$cod_periodo_confirmacion,
                par$cod_usuario               => par$cod_usuario);


        --buscar otras bovedas para la misma maquina, subcanal, etc., que no tengan fecha fin y que este en creacion
        SELECT COUNT(1) INTO var$hay_otros_periodos
        FROM SAPR_TPERIODO PERI
          INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
          INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PERI.OID_PLANIFICACION AND TIPE.COD_TIPO_PERIODO = 'BO'
        WHERE PERI.OID_MAQUINA = var$oid_maquina
          AND PERI.OID_PLANIFICACION = var$oid_planificacion
          AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_EC
          AND PERI.FYH_INICIO >= var$fyh_documento
          AND ((NVL(PERI.OID_SUBCANAL, '###VACIO###') = NVL(var$oid_subcanal, '###VACIO###') AND PLANI.BOL_PERIODO_SUBCANAL = 1) OR (PLANI.BOL_PERIODO_SUBCANAL = 0 AND PERI.OID_SUBCANAL IS NULL))
          AND ((NVL(PERI.OID_DIVISA, '###VACIO###') = NVL(var$oid_divisa, '###VACIO###') AND PLANI.BOL_PERIODO_DIVISA = 1) OR (PLANI.BOL_PERIODO_DIVISA = 0 AND PERI.OID_DIVISA IS NULL))
          AND NVL(PERI.OID_PTO_SERVICIO, '###VACIO###') = NVL(var$oid_pto_servicio, '###VACIO###')
          AND PERI.FYH_FIN IS NULL;

        IF var$hay_otros_periodos = 0 THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Previo a crear un nuevo período de bóveda',
          par$cod_identificador   => '');
          sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
              par$oid_periodo         => var$oid_periodo_bo_new,
              par$oid_planificacion         => var$oid_planificacion,
              par$oid_maquina               => var$oid_maquina,
              par$oid_estado_periodo        => var$oid_estado_periodo_EC,
              par$fyh_inicio                => var$fyh_documento + (1/60/60/24),
              par$fyh_fin                   => NULL,
              par$oid_tipo_periodo          => var$oid_tipo_periodo,
              par$oid_subcanal              => var$oid_subcanal_peri,
              par$oid_divisa                => var$oid_divisa_peri,
              par$oid_pto_servicio          => var$oid_pto_servicio_peri,
              par$cod_periodo_confirmacion  => NULL,
              par$cod_usuario               => par$cod_usuario);
        ELSE
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
          par$des_origen      => const$des_origen,
          par$des_version     => const$version,                            
          par$des_detalle     => 'Ya existe un periodo de boveda en creacion sin fecha y hora fin',
          par$cod_identificador   => '');
        END IF;

      EXCEPTION
        WHEN OTHERS THEN
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$des_origen,
            par$des_version     => const$version,                            
            par$des_detalle     => 'EXCEPTION srelacionar_mov_shipout_per: ' || SQLERRM,
            par$cod_identificador   => '');
            RAISE ;
      END;

    END IF;
    /*FIN BOVEDA*/
  END IF;

END srelacionar_mov_shipout_per;

PROCEDURE svalidar_corte
(
    par$oid_llamada          IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_formulario       IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,  
    par$fyh_gestion          IN gepr_pcomon_###VERSION###.tipo$fyh_, 
    par$cod_subcanal         IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_divisa           IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$bol_corte_periodo    OUT gepr_pcomon_###VERSION###.tipo$nel_,
    par$oid_periodo          OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_tipo_periodo     OUT gepr_pcomon_###VERSION###.tipo$oid_
) IS

  const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.svalidar_corte'; 
  var$oid_maquina           gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_planificacion     gepr_pcomon_###VERSION###.tipo$oid_;
  var$cod_collection_id     gepr_pcomon_###VERSION###.tipo$cod_;
  var$bol_periodo_subcanal  gepr_pcomon_###VERSION###.tipo$nel_;
  var$bol_periodo_divisa    gepr_pcomon_###VERSION###.tipo$nel_;
BEGIN
    /*Logueo los parametros de entrada*/
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: '||par$oid_llamada||
                            ', cod_formulario: '  || par$cod_formulario ||
                            ', oid_documento: '   || par$oid_documento ||
                            ', par$fyh_gestion: ' || TO_CHAR(par$fyh_gestion, 'YYYY-MM-DD HH24:MI:SS') ||
                            ', par$cod_subcanal: '|| par$cod_subcanal ||
                            ', par$oid_divisa: '  || par$oid_divisa
                            ,
                            par$cod_identificador   => '');
    /*Busco el oid de la maquina por el documento*/
    BEGIN 
       SELECT  MAQ.OID_MAQUINA, DOC.COD_COLLECTION_ID into var$oid_maquina, var$cod_collection_id
           FROM SAPR_TDOCUMENTO DOC
        INNER JOIN SAPR_VCUENTA CUEN ON CUEN.OID_CUENTA = DOC.OID_CUENTA_SALDO_ORIGEN
        INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_MAQUINA = CUEN.OID_MAQUINA
        INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = MAQ.OID_SECTOR
        WHERE  DOC.OID_DOCUMENTO = par$oid_documento;
    END;
    /*Busco el corte*/
    BEGIN 
        SELECT PLANI.OID_PLANIFICACION, PXM.BOL_CORTE_PERIODO, PLANI.BOL_PERIODO_SUBCANAL, PLANI.BOL_PERIODO_DIVISA 
          INTO var$oid_planificacion, par$bol_corte_periodo, var$bol_periodo_subcanal, var$bol_periodo_divisa
        FROM SAPR_TPLANXMOVIMIENTO PXM
          INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = PXM.OID_FORMULARIO
          INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PXM.OID_PLANIFICACION AND PLANI.BOL_ACTIVO = 1
          INNER JOIN SAPR_TPLANXMAQUINA PXMAQ ON PXMAQ.OID_PLANIFICACION = PLANI.OID_PLANIFICACION AND PXMAQ.BOL_ACTIVO = 1
          INNER JOIN SAPR_TMAQUINA MAQ ON MAQ.OID_MAQUINA = PXMAQ.OID_MAQUINA AND MAQ.BOL_ACTIVO = 1
          INNER JOIN GEPR_TSECTOR SECT ON SECT.OID_SECTOR = MAQ.OID_SECTOR
        WHERE PXM.BOL_ACTIVO = 1 AND FORMU.COD_FORMULARIO = par$cod_formulario 
        AND MAQ.OID_MAQUINA = var$oid_maquina;

    EXCEPTION
    WHEN no_data_found THEN
        par$bol_corte_periodo := 0;
    END;

     SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'par$bol_corte_periodo: '||par$bol_corte_periodo,
                            par$cod_identificador   => '');

    IF par$bol_corte_periodo = 1 THEN
      BEGIN
       /*Busco los datos del periodo de abono/acreditacion*/
        SELECT  PERI.OID_PERIODO,
                PERI.OID_TIPO_PERIODO 
        INTO par$oid_periodo, par$oid_tipo_periodo 
        FROM (
          /*Busco los datos del periodo de abono/acreditacion*/
          SELECT PERI.OID_PERIODO, PERI.OID_TIPO_PERIODO
          FROM SAPR_TPERIODO PERI
            INNER JOIN SAPR_TTIPO_PERIODO TPERI ON TPERI.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TPERI.COD_TIPO_PERIODO = 'AC'
            INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO AND ESPE.COD_ESTADO_PERIODO = 'AB'
            LEFT JOIN GEPR_TSUBCANAL SCANAL ON SCANAL.OID_SUBCANAL = PERI.OID_SUBCANAL 
          WHERE PERI.OID_PLANIFICACION = var$oid_planificacion 
            AND PERI.OID_MAQUINA = var$oid_maquina
            AND PERI.FYH_INICIO <= par$fyh_gestion
            AND PERI.FYH_FIN >= par$fyh_gestion
            AND NVL(NVL(PERI.COD_COLLECTION_ID, var$cod_collection_id),'###VACIO###') = NVL(NVL(var$cod_collection_id, PERI.COD_COLLECTION_ID),'###VACIO###')
            AND ((var$bol_periodo_subcanal = 1 AND SCANAL.COD_SUBCANAL = par$cod_subcanal) OR (var$bol_periodo_subcanal = 0 AND PERI.OID_SUBCANAL IS NULL))
            AND ((var$bol_periodo_divisa = 1 AND PERI.OID_DIVISA = par$oid_divisa) OR (var$bol_periodo_divisa = 0 AND PERI.OID_DIVISA IS NULL))
        ) PERI
        WHERE ROWNUM <= 1;

      EXCEPTION
      WHEN no_data_found THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'No se encuentra periodo de acreditación para realizar el corte',
                              par$cod_identificador   => '');
        par$oid_periodo := NULL;
      END;
    END IF;
END;

PROCEDURE screar_corte
  (
      par$oid_llamada              IN     gepr_pcomon_###VERSION###.tipo$oid_,
      par$oid_periodo              IN     gepr_pcomon_###VERSION###.tipo$oid_,
      par$oid_tipo_periodo         IN     gepr_pcomon_###VERSION###.tipo$oid_,
      par$oid_documento            IN     gepr_pcomon_###VERSION###.tipo$oid_,
      par$cod_usuario              IN     gepr_pcomon_###VERSION###.tipo$usr_

  ) IS

  const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.screar_corte';
  var$fyh_fin_doc         gepr_pcomon_###VERSION###.tipo$fyh_; 
  var$fyh_fin_per         gepr_pcomon_###VERSION###.tipo$fyh_;
  var$cod_collection_id   gepr_pcomon_###VERSION###.tipo$cod_;
  var$oid_periodo_creado  gepr_pcomon_###VERSION###.tipo$oid_ := NULL;
  var$oid_maquina         gepr_pcomon_###VERSION###.tipo$oid_ := null;
  var$oid_planificacion   gepr_pcomon_###VERSION###.tipo$oid_ := null;
  var$oid_estado_periodo  gepr_pcomon_###VERSION###.tipo$oid_ := null;
  var$oid_periodo         gepr_pcomon_###VERSION###.tipo$oid_ := null;
  var$oid_subcanal        gepr_pcomon_###VERSION###.tipo$oid_ := null;
  var$oid_divisa          gepr_pcomon_###VERSION###.tipo$oid_ := null;
BEGIN 

     /*Logueo los parametros de entrada*/
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: '||par$oid_llamada||', oid_periodo: '||par$oid_periodo||', oid_tipo_periodo: '||par$oid_tipo_periodo
                            ||', oid_documento: '||par$oid_documento||', cod_usuario: '||par$cod_usuario,
                            par$cod_identificador   => '');

    /*Obtengo fecha fin del documento*/
    BEGIN
        SELECT DOCU.FYH_GESTION, DOCU.COD_COLLECTION_ID INTO var$fyh_fin_doc, var$cod_collection_id  FROM SAPR_TDOCUMENTO DOCU
        WHERE DOCU.OID_DOCUMENTO = par$oid_documento;
    EXCEPTION
      WHEN no_data_found THEN
       var$fyh_fin_doc := null; 
       var$cod_collection_id := null;
    END;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'var$fyh_fin_doc: '||TO_CHAR(var$fyh_fin_doc, 'YYYY-MM-DD HH24:MI:SS') || ', var$cod_collection_id: ' || var$cod_collection_id,
                            par$cod_identificador   => '');

   IF par$oid_periodo IS NOT NULL THEN
      /*Obtengo fecha fin del periodo*/
      BEGIN
          SELECT PERI.FYH_FIN, OID_MAQUINA, PERI.OID_PLANIFICACION, PERI.OID_ESTADO_PERIODO, PERI.OID_SUBCANAL, PERI.OID_DIVISA 
            INTO var$fyh_fin_per, var$oid_maquina, var$oid_planificacion, var$oid_estado_periodo, var$oid_subcanal, var$oid_divisa 
          FROM SAPR_TPERIODO PERI
          WHERE PERI.OID_PERIODO = par$OID_PERIODO;
      EXCEPTION
        WHEN no_data_found THEN
        var$fyh_fin_per := null ;
        var$oid_maquina := null ;
        var$oid_planificacion := null;
        var$oid_estado_periodo := null;

      END;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada     => par$oid_llamada,
                                  par$des_origen      => const$des_origen,
                                  par$des_version     => const$version,                            
                                  par$des_detalle     => 'var$fyh_fin_per: '||TO_CHAR(var$fyh_fin_per, 'YYYY-MM-DD HH24:MI:SS'),
                                  par$cod_identificador   => '');
      var$oid_periodo := par$oid_periodo;
      /*Actualizo el periodo*/
      sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                      par$oid_periodo               => var$oid_periodo,
                      par$oid_planificacion         => NULL,
                      par$oid_maquina               => NULL,
                      par$oid_estado_periodo        => NULL,
                      par$fyh_inicio                => NULL,
                      par$fyh_fin                   => var$fyh_fin_doc,
                      par$oid_tipo_periodo          => NULL,
                      par$collection_id             => var$cod_collection_id,
                      par$cod_periodo_confirmacion  => NULL,
                      par$cod_usuario               => par$cod_usuario);

      /*Creo nuevo periodo*/
      sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                      par$oid_periodo               => var$oid_periodo_creado,
                      par$oid_planificacion         => var$oid_planificacion,
                      par$oid_maquina               => var$oid_maquina,
                      par$oid_estado_periodo        => var$oid_estado_periodo,
                      par$fyh_inicio                => var$fyh_fin_doc + (1/60/60/24),
                      par$fyh_fin                   => var$fyh_fin_per,
                      par$oid_tipo_periodo          => par$oid_tipo_periodo,
                      par$oid_subcanal              => var$oid_subcanal,
                      par$oid_divisa                => var$oid_divisa,
                      par$cod_periodo_confirmacion  => NULL,
                      par$cod_usuario               => par$cod_usuario);

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Se creo el periodo con oid: '||var$oid_periodo_creado,
                            par$cod_identificador   => '');
    ELSE
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$des_origen,
                              par$des_version     => const$version,                            
                              par$des_detalle     => 'No fue indicado el período de acreditación (par$oid_periodo) que se debe cortar',
                              par$cod_identificador   => '');
    END IF;
END;

PROCEDURE senvio_periodo (par$oid_maquina               IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_periodo_confirmacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$hacer_commit              IN gepr_pcomon_###VERSION###.tipo$bol_ := TRUE) AS

  -- Variables período de acreditación
  var$oid_periodo_ac      gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_planificacion   gepr_pcomon_###VERSION###.tipo$oid_;
  var$fyh_fin             gepr_pcomon_###VERSION###.tipo$fyh_;

  -- Variables período de bóveda
  var$oid_periodo_bo      gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_estado_periodo  gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_tipo_periodo    gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_periodo_creado  gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_subcanal        gepr_pcomon_###VERSION###.tipo$oid_;  
  var$oid_divisa          gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_pto_servicio    gepr_pcomon_###VERSION###.tipo$oid_;
  var$hay_otros_periodos  gepr_pcomon_###VERSION###.tipo$nel_;
  var$cod_periodo_confirmacion  gepr_pcomon_###VERSION###.tipo$cod_;
BEGIN

  BEGIN
    -- Buscar el período de acreditación con los parámetros recbidos
    SELECT PERIS.OID_PERIODO, PERIS.FYH_FIN, PERIS.OID_PLANIFICACION
      INTO var$oid_periodo_ac, var$fyh_fin, var$oid_planificacion
    FROM (
    SELECT PERI.OID_PERIODO, PERI.FYH_FIN, PERI.OID_PLANIFICACION
    FROM SAPR_TPERIODO PERI
    INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC'
    INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PERI.OID_PLANIFICACION
    INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION AND TPLANI.COD_TIPO_PLANIFICACION = 'FECHA_VALOR_CONFIR' 
    INNER JOIN SAPR_TPROCESOXPLANIFICACION TPXP ON TPXP.OID_PLANIFICACION = PLANI.OID_PLANIFICACION
    INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO AND TPRO.COD_PROCESO = 'BBVA_PERU'
    INNER JOIN SAPR_TMAQUINA MAQUI ON MAQUI.OID_MAQUINA = PERI.OID_MAQUINA
    WHERE MAQUI.OID_MAQUINA = par$oid_maquina
    AND PERI.COD_PERIODO_CONFIRMACION = par$cod_periodo_confirmacion
    ORDER BY PERI.FYH_FIN DESC 
    ) PERIS
    WHERE ROWNUM <= 1
    ;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      DBMS_OUTPUT.PUT_LINE('No encuentra período de acreditación con los parámetros recibidos');
      var$oid_periodo_ac := NULL;
  END;

  -- Validamos si encontró el período de acreditación
  IF var$oid_periodo_ac IS NOT NULL THEN
    DBMS_OUTPUT.PUT_LINE('Encuentra período de acreditación con los parámetros recibidos oid_periodo_ac: ' || var$oid_periodo_ac);
    -- Realizo la búsqueda de los períodos de bóveda relacionados al período de acreditación que esten en estado EC y sin fecha fin
    -- PERIODO DE BOVEDA        = SAPR_TPERIODO_RELACION.OID_PERIODO
    -- PERIODO DE ACREDITACION  = SAPR_TPERIODO_RELACION.OID_PERIODO_RELACIONADO
    BEGIN
      SELECT PERI.OID_PERIODO, PERI.OID_ESTADO_PERIODO, PERI.OID_TIPO_PERIODO, PERI.OID_SUBCANAL, PERI.OID_DIVISA, PERI.OID_PTO_SERVICIO
        INTO var$oid_periodo_bo, var$oid_estado_periodo, var$oid_tipo_periodo, var$oid_subcanal, var$oid_divisa, var$oid_pto_servicio 
      FROM SAPR_TPERIODO PERI
      INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'BO'
      INNER JOIN SAPR_TESTADO_PERIODO ESPE ON ESPE.OID_ESTADO_PERIODO = PERI.OID_ESTADO_PERIODO AND ESPE.COD_ESTADO_PERIODO = 'EC'
      INNER JOIN SAPR_TPERIODO_RELACION PERE ON PERE.OID_PERIODO = PERI.OID_PERIODO
      WHERE PERE.OID_PERIODO_RELACIONADO = var$oid_periodo_ac
      AND PERI.FYH_FIN IS NULL;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('No encuentra período de bóveda relacionado al período de acreditación que esten en estado EC y sin fecha fin');
        var$oid_periodo_bo := NULL;
    END;
    -- Validamos si encontró el período de bóveda
    IF var$oid_periodo_bo IS NOT NULL THEN
      DBMS_OUTPUT.PUT_LINE('Encuentra período de bóveda relacionado al período de acreditación que esten en estado EC y sin fecha fin oid_periodo_bo: ' || var$oid_periodo_bo);

      -- Al actualizar el período de bóveda, le asigno un sequence como cod_periodo_confirmacion*/
        SELECT '1' || LPAD(SAPR_QCOD_PER_CON_SEQ.NEXTVAL, 14, '0')
        INTO var$cod_periodo_confirmacion
        FROM DUAL;

      -- Actualizamos la fecha fin del período de bóveda con la fecha fin del período de acreditación
      sins_upd_tperiodo(par$oid_llamada             => NULL,
                      par$oid_periodo               => var$oid_periodo_bo,
                      par$oid_planificacion         => NULL,
                      par$oid_maquina               => NULL,
                      par$oid_estado_periodo        => NULL,
                      par$fyh_inicio                => NULL,
                      par$fyh_fin                   => var$fyh_fin,
                      par$oid_tipo_periodo          => NULL,
                      par$collection_id             => NULL,
                      par$oid_subcanal              => NULL,
                      par$oid_divisa                => NULL,
                      par$oid_pto_servicio          => NULL,
                      par$cod_periodo_confirmacion  => var$cod_periodo_confirmacion,
                      par$cod_usuario               => 'SENVIO_PERIODO');

      DBMS_OUTPUT.PUT_LINE('Actualiza el periodo de bóveda de oid_periodo: ' || var$oid_periodo_bo || ' con la fyh_fin: ' || TO_CHAR(var$fyh_fin, 'YYYY-MM-DD HH24:MI:SS'));

      -- Buscamos si ya se encuentra un período de bóveda en creacion y sin fecha fin
      SELECT COUNT(1) INTO var$hay_otros_periodos
      FROM SAPR_TPERIODO PERI
      INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
      AND TIPE.COD_TIPO_PERIODO = 'BO'
      WHERE PERI.OID_MAQUINA = par$oid_maquina
      AND PERI.OID_PLANIFICACION = var$oid_planificacion
      AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo
      AND NVL(PERI.OID_SUBCANAL, '###VACIO###') = NVL(var$oid_subcanal, '###VACIO###')
      AND NVL(PERI.OID_DIVISA, '###VACIO###') = NVL(var$oid_divisa, '###VACIO###')
      AND NVL(PERI.OID_PTO_SERVICIO, '###VACIO###') = NVL(var$oid_pto_servicio, '###VACIO###')
      AND PERI.FYH_FIN IS NULL;

      IF var$hay_otros_periodos = 0 THEN
        DBMS_OUTPUT.PUT_LINE('No se encuentra otro período de bóveda en creacion y sin fecha fin. Creamos un nuevo período de bóveda');
        -- No se encuentra otro período de bóveda en creacion y sin fecha fin
        -- Creamos un nuevo período de bóveda
        sins_upd_tperiodo(par$oid_llamada               => NULL,
                        par$oid_periodo               => var$oid_periodo_creado,
                        par$oid_planificacion         => var$oid_planificacion,
                        par$oid_maquina               => par$oid_maquina,
                        par$oid_estado_periodo        => var$oid_estado_periodo,
                        par$fyh_inicio                => var$fyh_fin + (1/60/60/24),
                        par$fyh_fin                   => NULL,
                        par$oid_tipo_periodo          => var$oid_tipo_periodo,
                        par$collection_id             => NULL,
                        par$oid_subcanal              => var$oid_subcanal,
                        par$oid_divisa                => var$oid_divisa,
                        par$oid_pto_servicio          => var$oid_pto_servicio,
                        par$cod_periodo_confirmacion  => NULL,
                        par$cod_usuario               => 'SENVIO_PERIODO');

          DBMS_OUTPUT.PUT_LINE('Nuevo período de bóveda oid_periodo: ' || var$oid_periodo_creado);
      END IF;


      IF par$hacer_commit THEN
        DBMS_OUTPUT.PUT_LINE('Realiza el COMMIT');
        COMMIT;
      END IF;
    END IF;
  END IF;
EXCEPTION
  WHEN OTHERS THEN
    IF par$hacer_commit THEN
      DBMS_OUTPUT.PUT_LINE('Realiza el ROLLBACK');
      ROLLBACK;
    END IF;
    RAISE;
END senvio_periodo;

PROCEDURE screar_periodos_iniciales
                                    (par$oid_llamada          IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$oid_maquina        IN     gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                                     par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_) IS

var$tiene_per_re            gepr_pcomon_###VERSION###.tipo$nel_; 
var$oid_periodo_re_new      gepr_pcomon_###VERSION###.tipo$oid_;
var$tiene_per_bo            gepr_pcomon_###VERSION###.tipo$nel_;
var$oid_periodo_bo_new      gepr_pcomon_###VERSION###.tipo$oid_;
var$fyh_vigencia_maq        gepr_pcomon_###VERSION###.tipo$fyh_;
var$oid_estado_periodo_EC   gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_tipo_periodo_re     gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_tipo_periodo_bo     gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_planificacion       gepr_pcomon_###VERSION###.tipo$oid_;
var$bol_periodo_subcanal    gepr_pcomon_###VERSION###.tipo$nel_;
var$bol_periodo_divisa      gepr_pcomon_###VERSION###.tipo$nel_;
const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.screar_periodos_iniciales';
var$cod_periodo_confirmacion gepr_pcomon_###VERSION###.tipo$cod_;
var$detalle           VARCHAR2(4000);
BEGIN

    IF par$oid_llamada IS NOT NULL THEN
			var$detalle := q'[Parametros iniciales: 
        par$oid_llamada: ]'               || par$oid_llamada || q'[
        par$oid_maquina: ]'          || par$oid_maquina || q'[
        par$cod_cultura: ]'  || par$cod_cultura || q'[
        par$info_ejecucion: ]'               || par$info_ejecucion || q'[
        par$cod_usuario: ]'               || par$cod_usuario;

			SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
    END IF;

    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_EC FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'EC';
    SELECT OID_TIPO_PERIODO INTO var$oid_tipo_periodo_re FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'RE';
    SELECT OID_TIPO_PERIODO INTO var$oid_tipo_periodo_bo FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'BO';
    /*Obtengo datos de la maquina para dar de alta los nuevos periodos*/
    BEGIN
      SELECT PLANI.OID_PLANIFICACION, PLANI.BOL_PERIODO_SUBCANAL, PLANI.BOL_PERIODO_DIVISA, PXM.FYH_VIGENCIA_INICIO INTO var$oid_planificacion, var$bol_periodo_subcanal, var$bol_periodo_divisa, var$fyh_vigencia_maq FROM SAPR_TMAQUINA MAQ 
        INNER JOIN SAPR_TPLANXMAQUINA PXM ON PXM.OID_MAQUINA = MAQ.OID_MAQUINA
        INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PXM.OID_PLANIFICACION
        INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION
        INNER JOIN SAPR_TPROCESOXPLANIFICACION TPXP ON TPXP.OID_PLANIFICACION = PLANI.OID_PLANIFICACION
        INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO
        WHERE TPLANI.COD_TIPO_PLANIFICACION = 'FECHA_VALOR_CONFIR' 
        AND TPRO.COD_PROCESO = 'BBVA_PERU' AND MAQ.OID_MAQUINA = par$oid_maquina AND MAQ.BOL_ACTIVO = 1 AND PLANI.BOL_ACTIVO = 1 AND PXM.BOL_ACTIVO = 1;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_planificacion := NULL;
    END;
    IF var$oid_planificacion IS NOT NULL THEN
        /*Verifico si la maquina ya tiene creados periodos de recojo y boveda*/
        BEGIN
            /*Obtengo el periodo de RECOJO de la maquina y planificacion*/
            SELECT COUNT(1) INTO var$tiene_per_re
            FROM SAPR_TPERIODO PERI
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO   AND TIPE.COD_TIPO_PERIODO = 'RE'
            WHERE PERI.OID_MAQUINA = par$oid_maquina AND PERI.OID_PLANIFICACION = var$oid_planificacion;

        END;
        BEGIN
            /*Obtengo el periodo de BOVEDA de la maquina y planificacion*/
            SELECT COUNT(1) INTO var$tiene_per_bo
            FROM SAPR_TPERIODO PERI
            INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO   AND TIPE.COD_TIPO_PERIODO = 'BO'
            WHERE PERI.OID_MAQUINA = par$oid_maquina AND PERI.OID_PLANIFICACION = var$oid_planificacion;

        END;
        BEGIN

        FOR PLANIFICACIONES IN (
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
            ),
            SUBCANALES AS (
            SELECT 
            SUBC.OID_SUBCANAL, CAN.OID_PLANIFICACION
            FROM CANALES CAN
            INNER JOIN GEPR_TSUBCANAL SUBC ON CAN.OID_CANAL = SUBC.OID_CANAL
            WHERE CAN.OID_SUBCANAL IS NULL AND CAN.CANAL_OID_PUNTO IS NULL AND CAN.CANAL_OID_MAQUINA IS NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL AND CAN.SUBCANAL_OID_MAQUINA IS NULL
            UNION ALL
            SELECT CAN.OID_SUBCANAL, CAN.OID_PLANIFICACION
            FROM CANALES CAN
            WHERE CAN.OID_SUBCANAL IS NOT NULL AND CAN.CANAL_OID_PUNTO IS NULL AND CAN.CANAL_OID_MAQUINA IS NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL AND CAN.SUBCANAL_OID_MAQUINA IS NULL),
            DIVISAS AS (
              SELECT OID_DIVISA, OID_PLANIFICACION
              FROM SAPR_TPLANXDIVISA 
              WHERE BOL_ACTIVO = 1 AND OID_PLANIFICACION = var$oid_planificacion
            )
            SELECT DISTINCT 
              CASE WHEN var$bol_periodo_subcanal = 1 THEN SC.OID_SUBCANAL ELSE NULL END AS OID_SUBCANAL, 
              CASE WHEN var$bol_periodo_divisa = 1 THEN DIVI.OID_DIVISA ELSE NULL END AS OID_DIVISA
            FROM SUBCANALES SC
            FULL JOIN DIVISAS DIVI ON SC.OID_PLANIFICACION = DIVI.OID_PLANIFICACION)
        LOOP
            var$oid_periodo_re_new := NULL;
            var$oid_periodo_bo_new := NULL;
            IF var$tiene_per_re = 0  THEN /*Si no tengo periodo de recojo creo uno nuevo */
              /*Al crear un nuevo periodo de recojo, le asigno un sequence como cod_periodo_confirmacion*/
              SELECT '1' || LPAD(SAPR_QCOD_PER_CON_SEQ.NEXTVAL, 14, '0')
              INTO var$cod_periodo_confirmacion
              FROM DUAL;

              sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                  par$oid_periodo         => var$oid_periodo_re_new,
                  par$oid_planificacion         => var$oid_planificacion,
                  par$oid_maquina               => par$oid_maquina,
                  par$oid_estado_periodo        => var$oid_estado_periodo_EC,
                  par$fyh_inicio                => var$fyh_vigencia_maq,
                  par$fyh_fin                   => NULL,
                  par$oid_subcanal              => PLANIFICACIONES.OID_SUBCANAL,
                  par$oid_divisa                => PLANIFICACIONES.OID_DIVISA,
                  par$oid_tipo_periodo          => var$oid_tipo_periodo_re,
                  par$cod_periodo_confirmacion  => var$cod_periodo_confirmacion,
                  par$cod_usuario               => par$cod_usuario);

            END IF;
            IF  var$tiene_per_bo = 0  THEN /*Si no tengo periodo de boveda creo uno nuevo */
                sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                    par$oid_periodo         => var$oid_periodo_bo_new,
                    par$oid_planificacion         => var$oid_planificacion,
                    par$oid_maquina               => par$oid_maquina,
                    par$oid_estado_periodo        => var$oid_estado_periodo_EC,
                    par$fyh_inicio                => var$fyh_vigencia_maq,
                    par$fyh_fin                   => NULL,
                    par$oid_subcanal              => PLANIFICACIONES.OID_SUBCANAL,
                    par$oid_divisa                => PLANIFICACIONES.OID_DIVISA,
                    par$oid_tipo_periodo          => var$oid_tipo_periodo_bo,
                    par$cod_periodo_confirmacion  => NULL,
                    par$cod_usuario               => par$cod_usuario);

            END IF;
        END LOOP;

        COMMIT;
        EXCEPTION
          WHEN OTHERS THEN
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,                            
              par$des_detalle     => 'EXCEPTION screar_periodos_iniciales: ' || SQLERRM,
              par$cod_identificador   => '');
              ROLLBACK ;
        END;   

    END IF;     
END screar_periodos_iniciales;

PROCEDURE sgenerar_periodos ( par$oid_llamada          IN     gepr_pcomon_###VERSION###.tipo$oid_,
                              par$oid_maquina          IN     gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_cultura          IN     gepr_pcomon_###VERSION###.tipo$cod_,
                              par$info_ejecucion       IN     gepr_pcomon_###VERSION###.tipo$desc_,
                              par$cod_usuario          IN     gepr_pcomon_###VERSION###.tipo$usr_,
                              par$hacer_commit         IN     gepr_pcomon_###VERSION###.tipo$bol_ := TRUE) IS

  var$oid_periodo_re_new      gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_periodo_bo_new      gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_periodo_ac_new      gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_planxmaquina        gepr_pcomon_###VERSION###.tipo$oid_;
  var$tiene_per_bo            gepr_pcomon_###VERSION###.tipo$nel_;
  var$tiene_per_re            gepr_pcomon_###VERSION###.tipo$nel_; 
  var$tiene_per_ac            gepr_pcomon_###VERSION###.tipo$nel_; 
  var$fyh_vigencia_maq        gepr_pcomon_###VERSION###.tipo$fyh_;
  var$fyh_ultimo_periodo_pxm  gepr_pcomon_###VERSION###.tipo$fyh_;
  var$fyh_ultimo_periodo      gepr_pcomon_###VERSION###.tipo$fyh_;
  var$fyh_ultimo_periodo_new  gepr_pcomon_###VERSION###.tipo$fyh_;
  var$fyh_periodo_inicio      gepr_pcomon_###VERSION###.tipo$fyh_;
  var$oid_estado_periodo_EC   gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_estado_periodo_AB   gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_tipo_periodo_re     gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_tipo_periodo_bo     gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_tipo_periodo_ac     gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_planificacion       gepr_pcomon_###VERSION###.tipo$oid_;
  var$bol_periodo_subcanal    gepr_pcomon_###VERSION###.tipo$nel_;
  var$bol_periodo_divisa      gepr_pcomon_###VERSION###.tipo$nel_;
  var$cod_tipo_planificacion  gepr_pcomon_###VERSION###.tipo$cod_;
  var$cod_proceso             gepr_pcomon_###VERSION###.tipo$cod_;
  var$es_fvc_conbbva          gepr_pcomon_###VERSION###.tipo$nbol_;
  var$tiene_fec_periodo       gepr_pcomon_###VERSION###.tipo$nel_;

  const$nombre_func      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sgenerar_periodos';
  var$cod_periodo_confirmacion gepr_pcomon_###VERSION###.tipo$cod_;
  var$detalle           VARCHAR2(4000);


BEGIN

    /* #### Limpiamos la tabla auxiliar #### */
    /*EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TAUXILIAR'*/

    /* #### Inicializamos variables.*/
    var$es_fvc_conbbva := 0;
    var$tiene_per_bo := 0;
    var$tiene_per_re := 0;

    /*Registrar los parametros en Log*/
    IF par$oid_llamada IS NOT NULL THEN
			var$detalle := q'[Parametros iniciales: 
        par$oid_llamada: ]'     || par$oid_llamada    || q'[
        par$oid_maquina: ]'     || par$oid_maquina    || q'[
        par$cod_cultura: ]'     || par$cod_cultura    || q'[
        par$info_ejecucion: ]'  || par$info_ejecucion || q'[
        par$cod_usuario: ]'     || par$cod_usuario    || q'[
        par$hacer_commit: ]'    || CASE par$hacer_commit WHEN TRUE THEN 'TRUE' WHEN FALSE THEN 'FALSE' ELSE NULL END;

			SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => var$detalle,
                            par$cod_identificador   => '');
    END IF;

    /*Obtengo los OID de los Estado de Periodo En Creacion y Abierto*/
    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_EC FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'EC';
    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_AB FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'AB';

    /*Obtengo los OID de los Tipos de Periodo, Recojo, Boveda y Acreditación*/
    SELECT OID_TIPO_PERIODO INTO var$oid_tipo_periodo_re FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'RE';
    SELECT OID_TIPO_PERIODO INTO var$oid_tipo_periodo_bo FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'BO';
    SELECT OID_TIPO_PERIODO INTO var$oid_tipo_periodo_ac FROM SAPR_TTIPO_PERIODO WHERE COD_TIPO_PERIODO = 'AC';

    /*Obtengo datos de la maquina para dar de alta los nuevos periodos*/
    BEGIN

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                              par$des_origen        => const$nombre_func,
                              par$des_version       => const$version,
                              par$des_detalle       => 'Obtener datos de la Maquina. ' || par$oid_maquina,
                              par$cod_identificador => '');

      SELECT PLANI.OID_PLANIFICACION, 
             PLANI.BOL_PERIODO_SUBCANAL, 
             PLANI.BOL_PERIODO_DIVISA, 
             PXM.FYH_VIGENCIA_INICIO,
             PXM.FYH_ULTIMO_PERIODO,
             TPLANI.COD_TIPO_PLANIFICACION,
             PXM.OID_PLANXMAQUINA
      INTO var$oid_planificacion, 
           var$bol_periodo_subcanal, 
           var$bol_periodo_divisa, 
           var$fyh_vigencia_maq,
           var$fyh_ultimo_periodo_pxm,
           var$cod_tipo_planificacion,
           var$oid_planxmaquina
      FROM SAPR_TMAQUINA MAQ 
        INNER JOIN SAPR_TPLANXMAQUINA PXM ON PXM.OID_MAQUINA = MAQ.OID_MAQUINA
        INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PXM.OID_PLANIFICACION
        INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION
      WHERE  MAQ.OID_MAQUINA = par$oid_maquina 
      AND MAQ.BOL_ACTIVO = 1 AND PLANI.BOL_ACTIVO = 1 AND PXM.BOL_ACTIVO = 1;

    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_planificacion := NULL;
    END;

    IF var$oid_planificacion IS NOT NULL THEN


        IF par$oid_llamada IS NOT NULL THEN
          var$detalle := q'[Datos de la Maquina: 
            var$oid_planificacion: ]'               || var$oid_planificacion || q'[
            var$bol_periodo_subcanal: ]'          || var$bol_periodo_subcanal || q'[
            var$bol_periodo_divisa: ]'  || var$bol_periodo_divisa || q'[
            var$fyh_vigencia_maq: ]'               || TO_CHAR(var$fyh_vigencia_maq, 'YYYY-MM-DD HH24:MI:SS')  || q'[
            var$fyh_ultimo_periodo_pxm: ]'               || TO_CHAR(var$fyh_ultimo_periodo_pxm, 'YYYY-MM-DD HH24:MI:SS')  || q'[
            var$cod_tipo_planificacion: ]'               || var$cod_tipo_planificacion || q'[
            var$oid_planxmaquina: ]'               || var$oid_planxmaquina;


          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => const$nombre_func,
                                par$des_version     => const$version,                            
                                par$des_detalle     => var$detalle,
                                par$cod_identificador   => '');
        END IF;

        IF var$cod_tipo_planificacion = 'FECHA_VALOR_CONFIR' THEN
            /*Verifico si la planificación tiene el proceso de BBVA_PERU*/
            SELECT COUNT(1) 
              INTO var$es_fvc_conbbva
            FROM SAPR_TPROCESOXPLANIFICACION TPXP
              INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO AND TPRO.COD_PROCESO = 'BBVA_PERU'
            WHERE TPXP.OID_PLANIFICACION = var$oid_planificacion;
        END IF;

        /*Verifico si la maquina ya tiene creados periodos de Recojo o Boveda*/

        IF var$es_fvc_conbbva = 1 THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                              par$des_origen        => const$nombre_func,
                              par$des_version       => const$version,
                              par$des_detalle       => 'La planificación es FECHA_VALOR_CONFIRM y con proceso BBVA_PERU',
                              par$cod_identificador => '');
          BEGIN
              /*Obtengo el periodo de RECOJO de la maquina y planificacion*/
              SELECT COUNT(1) INTO var$tiene_per_re
              FROM SAPR_TPERIODO PERI
              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO   AND TIPE.COD_TIPO_PERIODO = 'RE'
              WHERE PERI.OID_MAQUINA = par$oid_maquina AND PERI.OID_PLANIFICACION = var$oid_planificacion;

          END;
          BEGIN
              /*Obtengo el periodo de BOVEDA de la maquina y planificacion*/
              SELECT COUNT(1) INTO var$tiene_per_bo
              FROM SAPR_TPERIODO PERI
              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO   AND TIPE.COD_TIPO_PERIODO = 'BO'
              WHERE PERI.OID_MAQUINA = par$oid_maquina AND PERI.OID_PLANIFICACION = var$oid_planificacion;

          END;
        END IF;


        /* Buscamos el último período generado de la tabla SAPR_TPERIODO para la máquina en la planificación */
        SELECT MAX(FYH_FIN)
          INTO var$fyh_ultimo_periodo
        FROM SAPR_TPERIODO
        WHERE OID_MAQUINA = par$oid_maquina AND OID_PLANIFICACION = var$oid_planificacion;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                  par$des_origen        => const$nombre_func,
                                  par$des_version       => const$version,
                                  par$des_detalle       => 'Último período generado de la tabla SAPR_TPERIODO para la máquina en la planificación: ' || TO_CHAR(var$fyh_ultimo_periodo, 'YYYY-MM-DD HH24:MI:SS'),
                                  par$cod_identificador => '');

        IF var$fyh_ultimo_periodo IS NULL THEN 
          /* Buscamos el último período generado de la tabla SAPR_TPERIODO para la máquina independiente de la planificación */
          SELECT MAX(FYH_FIN)
            INTO var$fyh_ultimo_periodo
          FROM SAPR_TPERIODO
          WHERE OID_MAQUINA = par$oid_maquina;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                                  par$des_origen        => const$nombre_func,
                                  par$des_version       => const$version,
                                  par$des_detalle       => 'Último período generado de la tabla SAPR_TPERIODO para la máquina independiente de la planificación: ' || TO_CHAR(var$fyh_ultimo_periodo, 'YYYY-MM-DD HH24:MI:SS'),
                                  par$cod_identificador => '');
        END IF;


        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                          par$des_origen        => const$nombre_func,
                          par$des_version       => const$version,
                          par$des_detalle       => 'Ejecutar procedimiento sgenerar_fechas_periodo',
                          par$cod_identificador => '');

        var$detalle := q'[Parametros sgenerar_fechas_periodo: 
        par$oid_llamada: ]'         || par$oid_llamada || q'[
        par$oid_maquina: ]'         || par$oid_maquina || q'[
        var$fyh_ultimo_periodo_pxm: ]'  || TO_CHAR(var$fyh_ultimo_periodo_pxm, 'YYYY-MM-DD HH24:MI:SS')  || q'[
        var$fyh_ultimo_periodo: ]'  || TO_CHAR(var$fyh_ultimo_periodo, 'YYYY-MM-DD HH24:MI:SS')  || q'[
        var$fyh_vigencia_maq: ]'    || TO_CHAR(var$fyh_vigencia_maq, 'YYYY-MM-DD HH24:MI:SS')  || q'[
        NVL(var$fyh_ultimo_periodo_pxm, var$fyh_vigencia_maq): ]'   || NVL(var$fyh_ultimo_periodo_pxm, var$fyh_vigencia_maq);

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                              par$des_origen      => const$nombre_func,
                              par$des_version     => const$version,                            
                              par$des_detalle     => var$detalle,
                              par$cod_identificador   => '');


        -- 1° último período creado de pxm
        -- 2° último período creado de la tabla de período para la planificación actual
        -- 3° último período creado de la tabla de período independiente de la planificación actual
        -- 4° vigencia de la máquina 
        IF var$fyh_ultimo_periodo_pxm IS NOT NULL THEN
          var$fyh_periodo_inicio := var$fyh_ultimo_periodo_pxm + 1/60/60/24;
        ELSE
          IF var$fyh_ultimo_periodo IS NOT NULL AND var$fyh_ultimo_periodo > var$fyh_vigencia_maq THEN 
            var$fyh_periodo_inicio := var$fyh_ultimo_periodo + 1/60/60/24;
          ELSE
            var$fyh_periodo_inicio := var$fyh_vigencia_maq;
          END IF;
        END IF;


        sgenerar_fechas_periodo(par$oid_llamada, 
                                par$oid_maquina, 
                                var$fyh_periodo_inicio);

        /*Obtener la cantidad de fechas*/
        SELECT COUNT(1) INTO var$tiene_fec_periodo
        FROM SAPR_GTT_TAUXILIAR AUXI
        WHERE AUXI.COD_CALIFICADOR = 'DATOS_FECHAS_PERIODO';

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                          par$des_origen        => const$nombre_func,
                          par$des_version       => const$version,
                          par$des_detalle       => 'Total de fechas de periodos generados ' || var$tiene_fec_periodo,
                          par$cod_identificador => '');




        BEGIN

          FOR PLANIFICACIONES IN (
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
              ),
              SUBCANALES AS (
              SELECT 
              SUBC.OID_SUBCANAL, CAN.OID_PLANIFICACION
              FROM CANALES CAN
              INNER JOIN GEPR_TSUBCANAL SUBC ON CAN.OID_CANAL = SUBC.OID_CANAL
              WHERE CAN.OID_SUBCANAL IS NULL AND CAN.CANAL_OID_PUNTO IS NULL AND CAN.CANAL_OID_MAQUINA IS NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL AND CAN.SUBCANAL_OID_MAQUINA IS NULL
              UNION ALL
              SELECT CAN.OID_SUBCANAL, CAN.OID_PLANIFICACION
              FROM CANALES CAN
              WHERE CAN.OID_SUBCANAL IS NOT NULL AND CAN.CANAL_OID_PUNTO IS NULL AND CAN.CANAL_OID_MAQUINA IS NULL AND CAN.SUBCANAL_OID_PUNTO IS NULL AND CAN.SUBCANAL_OID_MAQUINA IS NULL),
              DIVISAS AS (
                SELECT OID_DIVISA, OID_PLANIFICACION
                FROM SAPR_TPLANXDIVISA 
                WHERE BOL_ACTIVO = 1 AND OID_PLANIFICACION = var$oid_planificacion
              )
              SELECT DISTINCT 
                CASE WHEN var$bol_periodo_subcanal = 1 THEN SC.OID_SUBCANAL ELSE NULL END AS OID_SUBCANAL, 
                CASE WHEN var$bol_periodo_divisa = 1 THEN DIVI.OID_DIVISA ELSE NULL END AS OID_DIVISA
              FROM SUBCANALES SC
              FULL JOIN DIVISAS DIVI ON SC.OID_PLANIFICACION = DIVI.OID_PLANIFICACION
          )
          LOOP

              var$oid_periodo_re_new := NULL;
              var$oid_periodo_bo_new := NULL;

              /*Si no tengo periodo de recojo creo uno nuevo */

              IF var$tiene_per_re = 0 AND var$es_fvc_conbbva = 1 THEN 
                /*Al crear un nuevo periodo de recojo, le asigno un sequence como cod_periodo_confirmacion*/
                SELECT '1' || LPAD(SAPR_QCOD_PER_CON_SEQ.NEXTVAL, 14, '0')
                INTO var$cod_periodo_confirmacion
                FROM DUAL;

                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                            par$des_origen        => const$nombre_func,
                            par$des_version       => const$version,
                            par$des_detalle       => 'Crear periodo de recojo',
                            par$cod_identificador => '');

                sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                    par$oid_periodo         => var$oid_periodo_re_new,
                    par$oid_planificacion         => var$oid_planificacion,
                    par$oid_maquina               => par$oid_maquina,
                    par$oid_estado_periodo        => var$oid_estado_periodo_EC,
                    par$fyh_inicio                => var$fyh_vigencia_maq,
                    par$fyh_fin                   => NULL,
                    par$oid_subcanal              => PLANIFICACIONES.OID_SUBCANAL,
                    par$oid_divisa                => PLANIFICACIONES.OID_DIVISA,
                    par$oid_tipo_periodo          => var$oid_tipo_periodo_re,
                    par$cod_periodo_confirmacion  => var$cod_periodo_confirmacion,
                    par$cod_usuario               => par$cod_usuario);

              END IF;
              /*Si no tengo periodo de boveda creo uno nuevo */
              IF  var$tiene_per_bo = 0 AND var$es_fvc_conbbva = 1 THEN 

                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                            par$des_origen        => const$nombre_func,
                            par$des_version       => const$version,
                            par$des_detalle       => 'Crear periodo de boveda',
                            par$cod_identificador => '');

                  sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                      par$oid_periodo         => var$oid_periodo_bo_new,
                      par$oid_planificacion         => var$oid_planificacion,
                      par$oid_maquina               => par$oid_maquina,
                      par$oid_estado_periodo        => var$oid_estado_periodo_EC,
                      par$fyh_inicio                => var$fyh_vigencia_maq,
                      par$fyh_fin                   => NULL,
                      par$oid_subcanal              => PLANIFICACIONES.OID_SUBCANAL,
                      par$oid_divisa                => PLANIFICACIONES.OID_DIVISA,
                      par$oid_tipo_periodo          => var$oid_tipo_periodo_bo,
                      par$cod_periodo_confirmacion  => NULL,
                      par$cod_usuario               => par$cod_usuario);

              END IF;


              /*Iniciamos la creación de los periodos de acreditación. */

              IF  var$tiene_fec_periodo > 0  THEN 

                  FOR FECHAS IN (
                    SELECT *  
                      FROM SAPR_GTT_TAUXILIAR AUXI
                      WHERE AUXI.COD_CALIFICADOR = 'DATOS_FECHAS_PERIODO'
                  )
                  LOOP

                    var$oid_periodo_ac_new := NULL;

                    BEGIN
                      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                              par$des_origen        => const$nombre_func,
                              par$des_version       => const$version,
                              par$des_detalle       => 'Crear periodo de acreditación',
                              par$cod_identificador => '');

                      sins_upd_tperiodo(par$oid_llamada => par$oid_llamada,
                        par$oid_periodo               => var$oid_periodo_ac_new,
                        par$oid_planificacion         => var$oid_planificacion,
                        par$oid_maquina               => par$oid_maquina,
                        par$oid_estado_periodo        => var$oid_estado_periodo_AB,
                        par$fyh_inicio                => TO_DATE(FECHAS.COD_CAMPO2, 'yyyy/mm/dd HH24:MI:SS'),
                        par$fyh_fin                   => TO_DATE(FECHAS.COD_CAMPO3, 'yyyy/mm/dd HH24:MI:SS'),
                        par$oid_subcanal              => PLANIFICACIONES.OID_SUBCANAL,
                        par$oid_divisa                => PLANIFICACIONES.OID_DIVISA,
                        par$oid_tipo_periodo          => var$oid_tipo_periodo_ac,
                        par$cod_periodo_confirmacion  => NULL,
                        par$cod_usuario               => par$cod_usuario);

                      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                              par$des_origen        => const$nombre_func,
                              par$des_version       => const$version,
                              par$des_detalle       => 'Periodo de acreditación creado con OID_PERIODO: ' || var$oid_periodo_ac_new,
                              par$cod_identificador => '');
                    EXCEPTION WHEN OTHERS THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada => par$oid_llamada,
                              par$des_origen        => const$nombre_func,
                              par$des_version       => const$version,
                              par$des_detalle       => 'Ocurrió una excepción al crear periodo. ' || SQLERRM ,
                              par$cod_identificador => '');

                    END;

                  END LOOP;

                  /*Actualizar FYH Ultimo Periodo en PLANXMAQUINA*/  
                  SELECT MAX(TO_DATE(COD_CAMPO3, 'yyyy/mm/dd HH24:MI:SS')) INTO var$fyh_ultimo_periodo_new
                    FROM SAPR_GTT_TAUXILIAR AUXI
                    WHERE AUXI.COD_CALIFICADOR = 'DATOS_FECHAS_PERIODO';

                  UPDATE SAPR_TPLANXMAQUINA 
                      SET FYH_ULTIMO_PERIODO = var$fyh_ultimo_periodo_new,
                      GMT_MODIFICACION = gepr_putilidades_###VERSION###.fgmt_zero,
                      DES_USUARIO_MODIFICACION = NVL(par$cod_usuario, 'SIN_USUARIO')
                  WHERE  OID_PLANXMAQUINA  = var$oid_planxmaquina;


              END IF;

          END LOOP;

        IF par$hacer_commit THEN
          COMMIT;
        END IF;

        EXCEPTION
          WHEN OTHERS THEN
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$nombre_func,
              par$des_version     => const$version,                            
              par$des_detalle     => 'EXCEPTION sgenerar_periodos: ' || SQLERRM,
              par$cod_identificador   => '');
              
              IF par$hacer_commit THEN
                ROLLBACK ;
              END IF;
        END;   

    END IF;  

END sgenerar_periodos;

PROCEDURE svalidar_periodo_recojo(
        par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_maquina                 IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_planificacion           IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$usr_,
        par$oid_periodo_ac              IN gepr_pcomon_###VERSION###.tipo$oid_,    
        par$cod_subcanal                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_divisa                  IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$collection_id               IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_documento               IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_periodo_re             OUT gepr_pcomon_###VERSION###.tipo$oid_

) IS
  const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.svalidar_periodo_recojo'; 
  var$oid_documento                gepr_pcomon_###VERSION###.tipo$oid_ := NULL;
  var$fyh_documento            gepr_pcomon_###VERSION###.tipo$fyh_;
  var$oid_estado_periodo_EC    gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_tipo_periodo         gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_periodo_re           gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_periodo_re_new       gepr_pcomon_###VERSION###.tipo$oid_;
  var$cod_collection_id_per    gepr_pcomon_###VERSION###.tipo$cod_;
  var$bol_periodo_subcanal     gepr_pcomon_###VERSION###.tipo$nbol_ := 0; 
  var$bol_periodo_divisa       gepr_pcomon_###VERSION###.tipo$nbol_ := 0; 
  var$oid_subcanal             gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_divisa               gepr_pcomon_###VERSION###.tipo$oid_;
  var$oid_pto_servicio         gepr_pcomon_###VERSION###.tipo$oid_;
  var$existe                   gepr_pcomon_###VERSION###.tipo$nel_;
  var$cod_periodo_confirmacion gepr_pcomon_###VERSION###.tipo$cod_;
  --Variables para el procedure srelacionar_periodo
  var$validaciones    gepr_pcomon_###VERSION###.tipo$descs_;
  var$cod_ejecucion   gepr_pcomon_###VERSION###.tipo$nel_;

BEGIN
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: ' || par$oid_llamada||
                            ', oid_maquina: '|| par$oid_maquina||
                            ', oid_planificacion: '|| par$oid_planificacion||
                            ', cod_usuario: '|| par$cod_usuario||
                            ', collection_id: '|| par$collection_id||
                            ', oid_documento: '|| par$oid_documento||
                            ', oid_periodo_ac: '|| par$oid_periodo_ac || 
                            ', cod_subcanal: '|| par$cod_subcanal || 
                            ', oid_divisa: ' || par$oid_divisa,
                            par$cod_identificador   => '');


    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_EC FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'EC';

       SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Se recupera var$oid_estado_periodo_EC: ' || var$oid_estado_periodo_EC,
              par$cod_identificador   => '');

    SELECT P.BOL_PERIODO_SUBCANAL, P.BOL_PERIODO_DIVISA
       INTO var$bol_periodo_subcanal, var$bol_periodo_divisa
       FROM SAPR_TPLANIFICACION P 
       WHERE P.OID_PLANIFICACION = par$oid_planificacion;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                  par$des_origen      => const$des_origen,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Se recupera var$bol_periodo_subcanal: ' || var$bol_periodo_subcanal ||
                  						 ', var$bol_periodo_divisa: ' || var$bol_periodo_divisa,
                  par$cod_identificador   => '');

    /*Obtengo el tipo de documento y la fyh_documento*/
    begin
      SELECT DOCU.OID_DOCUMENTO, DOCU.FYH_GESTION into var$oid_documento, var$fyh_documento FROM SAPR_TDOCUMENTO DOCU
      INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOCU.OID_FORMULARIO
      WHERE FORMU.COD_FORMULARIO IN ('MAECIC','MAECID') AND DOCU.OID_DOCUMENTO = par$oid_documento;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Se recupera var$fyh_documento: ' || TO_CHAR(var$fyh_documento, 'YYYY-MM-DD HH24:MI:SS'),
                par$cod_identificador   => '');

    exception
      when no_data_found then
        var$oid_documento := NULL;
    end;

    IF var$oid_documento is not null THEN

        BEGIN
            /*Obtengo el periodo de recojo en creacion*/
            SELECT PERIS.OID_PERIODO , PERIS.OID_TIPO_PERIODO, PERIS.COD_COLLECTION_ID, PERIS.OID_SUBCANAL, PERIS.OID_DIVISA, PERIS.OID_PTO_SERVICIO 
              INTO var$oid_periodo_re, var$oid_tipo_periodo, var$cod_collection_id_per, var$oid_subcanal, var$oid_divisa, var$oid_pto_servicio
            FROM (
            SELECT PERI.OID_PERIODO , PERI.OID_TIPO_PERIODO, PERI.COD_COLLECTION_ID, PERI.OID_SUBCANAL, PERI.OID_DIVISA, PERI.OID_PTO_SERVICIO 

                FROM SAPR_TPERIODO PERI
                INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
                INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PERI.OID_PLANIFICACION
                INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION AND TIPE.COD_TIPO_PERIODO = 'RE'
                INNER JOIN SAPR_TPROCESOXPLANIFICACION TPXP ON TPXP.OID_PLANIFICACION = PLANI.OID_PLANIFICACION
                INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO
                LEFT JOIN GEPR_TSUBCANAL SCANAL ON SCANAL.OID_SUBCANAL = PERI.OID_SUBCANAL
              WHERE PERI.OID_MAQUINA = par$oid_maquina
                AND TPLANI.COD_TIPO_PLANIFICACION = 'FECHA_VALOR_CONFIR' 
                AND TPRO.COD_PROCESO = 'BBVA_PERU'
                AND ((SCANAL.COD_SUBCANAL = par$cod_subcanal AND var$bol_periodo_subcanal = 1) OR (var$bol_periodo_subcanal = 0 AND PERI.OID_SUBCANAL IS NULL))
                AND ((PERI.OID_DIVISA = par$oid_divisa AND var$bol_periodo_divisa = 1) OR (var$bol_periodo_divisa = 0 AND PERI.OID_DIVISA IS NULL))
                AND PERI.OID_PLANIFICACION = par$oid_planificacion
                AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_EC
                AND PERI.FYH_INICIO <= var$fyh_documento 
                AND ((PERI.FYH_FIN IS NOT NULL AND PERI.FYH_FIN >= var$fyh_documento) OR PERI.FYH_FIN IS NULL)
            ORDER BY PERI.FYH_INICIO DESC
          ) PERIS
            WHERE ROWNUM <= 1
            ;

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
              par$des_origen      => const$des_origen,
              par$des_version     => const$version,                            
              par$des_detalle     => 'Se recupera var$oid_periodo_re: ' || var$oid_periodo_re ||
              ', var$oid_tipo_periodo: ' || var$oid_tipo_periodo ||
              ', var$cod_collection_id_per: ' || var$cod_collection_id_per ||
              ', var$oid_subcanal: ' || var$oid_subcanal ||
              ', var$oid_divisa: ' || var$oid_divisa ||
              ', var$oid_pto_servicio: ' || var$oid_pto_servicio,
              par$cod_identificador   => '');
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$oid_periodo_re := NULL;
               SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No hay período de recojo en creación',
                par$cod_identificador   => '');

        END;
        IF var$oid_periodo_re is not null  THEN
            IF var$cod_collection_id_per IS NOT NULL THEN

                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$des_origen,
                        par$des_version     => const$version,                            
                        par$des_detalle     => 'El collectionID del periodo es :' || var$cod_collection_id_per,
                        par$cod_identificador   => '');
                IF var$cod_collection_id_per <> par$collection_id THEN
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                      par$des_origen      => const$des_origen,
                                      par$des_version     => const$version,                            
                                      par$des_detalle     => 'El collectionID del periodo:' || var$cod_collection_id_per || ' es distinto al del documento: ' || par$collection_id,
                                      par$cod_identificador   => '');
                  /*Al crear un nuevo periodo de recojo, le asigno un sequence como cod_periodo_confirmacion*/
                  SELECT '1' || LPAD(SAPR_QCOD_PER_CON_SEQ.NEXTVAL, 14, '0')
                  INTO var$cod_periodo_confirmacion
                  FROM DUAL;

                  /*Crear nuevo periodo de recojo*/
                  sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                    par$oid_periodo               => var$oid_periodo_re_new,
                    par$oid_planificacion         => par$oid_planificacion,
                    par$oid_maquina               => par$oid_maquina,
                    par$oid_estado_periodo        => var$oid_estado_periodo_EC,
                    par$fyh_inicio                => var$fyh_documento,
                    par$fyh_fin                   => NULL,
                    par$oid_tipo_periodo          => var$oid_tipo_periodo,
                    par$collection_id             => par$collection_id,
                    par$oid_subcanal              => var$oid_subcanal,
                    par$oid_divisa                => var$oid_divisa,
                    par$oid_pto_servicio          => var$oid_pto_servicio,
                    par$cod_periodo_confirmacion  => var$cod_periodo_confirmacion,
                    par$cod_usuario               => par$cod_usuario);

                  /*Relacionar documento al nuevo periodo de recojo*/
                    --Se relaciona el documento al período de recojo y se suma los valores al SAPR_TCALCULO_EFECTIVO
                  srelacionar_periodo (par$oid_llamada      =>  par$oid_llamada,
                              par$oid_documento    =>  par$oid_documento,
                              par$oid_periodo      =>  var$oid_periodo_re_new,
                              par$cod_usuario      =>  par$cod_usuario,
                              par$cod_cultura      =>  par$cod_cultura,
                              par$info_ejecucion   =>  '',
                              par$hacer_commit     =>  0,
                              par$validaciones     =>  var$validaciones,
                              par$cod_ejecucion    =>  var$cod_ejecucion);

                  /*Relacionar nuevo periodo de recojo al periodo de acreditacion*/
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                      par$des_origen      => const$des_origen,
                                      par$des_version     => const$version,                            
                                      par$des_detalle     => 'Previo a relacionar el período de recojo al período de acreditación',
                                      par$cod_identificador   => '');
                  --Relacionamos el período de Recojo creado al período de acreditación 
                  -- par$oid_periodo              = PERIODO DE RECOJO      
                  -- par$oid_periodo_relacionado  = PERIODO DE ACREDITACION
                  sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_re_new,
                                          par$oid_periodo_relacionado   => par$oid_periodo_ac,
                                          par$cod_usuario               => par$cod_usuario);

                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                    par$des_origen      => const$des_origen,
                                    par$des_version     => const$version,                            
                                    par$des_detalle     => 'Previo a relacionar el período de acreditación al período de recojo',
                                    par$cod_identificador   => '');
                  --Relacionamos el período de acreditación creado al período de recojo 
                  -- par$oid_periodo              = PERIODO DE ACREDITACION      
                  -- par$oid_periodo_relacionado  = PERIODO DE RECOJO
                  sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_ac,
                                          par$oid_periodo_relacionado   => var$oid_periodo_re_new,
                                          par$cod_usuario               => par$cod_usuario);
                  par$oid_periodo_re := var$oid_periodo_re_new;
                ELSE
                  /*Relacionar documento al periodo de recojo*/
                    --Se relaciona el documento al período de recojo y se suma los valores al SAPR_TCALCULO_EFECTIVO
                  srelacionar_periodo (par$oid_llamada      =>  par$oid_llamada,
                              par$oid_documento    =>  par$oid_documento,
                              par$oid_periodo      =>  var$oid_periodo_re,
                              par$cod_usuario      =>  par$cod_usuario,
                              par$cod_cultura      =>  par$cod_cultura,
                              par$info_ejecucion   =>  '',
                              par$hacer_commit     =>  0,
                              par$validaciones     =>  var$validaciones,
                              par$cod_ejecucion    =>  var$cod_ejecucion);

                    /*Relacionar periodo de recojo al periodo de acreditacion*/
                    SELECT COUNT(1) INTO var$existe
                    FROM SAPR_TPERIODO_RELACION
                    WHERE OID_PERIODO = var$oid_periodo_re AND OID_PERIODO_RELACIONADO = par$oid_periodo_ac;

                    IF var$existe = 0 THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                            par$des_origen      => const$des_origen,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Previo a relacionar el período de recojo al período de acreditación',
                                            par$cod_identificador   => '');
                        --Relacionamos el período de Recojo creado al período de acreditación 
                        -- par$oid_periodo              = PERIODO DE RECOJO       
                        -- par$oid_periodo_relacionado  = PERIODO DE ACREDITACION 
                        sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_re,
                                                par$oid_periodo_relacionado   => par$oid_periodo_ac,
                                                par$cod_usuario               => par$cod_usuario);
                    END IF;

                     /*Relacionar periodo de acreditacion al periodo de recojo*/
                    SELECT COUNT(1) INTO var$existe
                    FROM SAPR_TPERIODO_RELACION
                    WHERE OID_PERIODO = par$oid_periodo_ac AND OID_PERIODO_RELACIONADO = var$oid_periodo_re;

                    IF var$existe = 0 THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                            par$des_origen      => const$des_origen,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Previo a relacionar el período de acreditación al período de recojo',
                                            par$cod_identificador   => '');
                        --Relacionamos el período de acreditación creado al período de recojo 
                        -- par$oid_periodo              = PERIODO DE ACREDITACION       
                        -- par$oid_periodo_relacionado  = PERIODO DE RECOJO
                        sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_ac,
                                                par$oid_periodo_relacionado   => var$oid_periodo_re,
                                                par$cod_usuario               => par$cod_usuario);
                    END IF;
                    par$oid_periodo_re := var$oid_periodo_re;
                END IF;
            ELSE
                /*Actualizar periodo actual con el collection id del documento*/
                  sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                        par$oid_periodo               => var$oid_periodo_re,
                        par$oid_planificacion         => NULL,
                        par$oid_maquina               => NULL,
                        par$oid_estado_periodo        => NULL,
                        par$fyh_inicio                => NULL,
                        par$fyh_fin                   => NULL,
                        par$oid_tipo_periodo          => NULL,
                        par$collection_id             => par$collection_id,
                        par$cod_periodo_confirmacion  => NULL,
                        par$cod_usuario               => par$cod_usuario);
                /*Relacionar documento al periodo de recojo*/
                   srelacionar_periodo (par$oid_llamada      =>  par$oid_llamada,
                                par$oid_documento    =>  par$oid_documento,
                                par$oid_periodo      =>  var$oid_periodo_re,
                                par$cod_usuario      =>  par$cod_usuario,
                                par$cod_cultura      =>  par$cod_cultura,
                                par$info_ejecucion   =>  '',
                                par$hacer_commit     =>  0,
                                par$validaciones     =>  var$validaciones,
                                par$cod_ejecucion    =>  var$cod_ejecucion);
                /*Relacionar periodo de recojo al periodo de acreditacion*/
                SELECT COUNT(1) INTO var$existe
                FROM SAPR_TPERIODO_RELACION
                WHERE OID_PERIODO = var$oid_periodo_re AND OID_PERIODO_RELACIONADO = par$oid_periodo_ac;

                IF var$existe = 0 THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de recojo al período de acreditación',
                                        par$cod_identificador   => '');
                    --Relacionamos el período de Recojo creado al período de acreditación 
                    -- par$oid_periodo              = PERIODO DE RECOJO       
                    -- par$oid_periodo_relacionado  = PERIODO DE ACREDITACION 
                    sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_re,
                                            par$oid_periodo_relacionado   => par$oid_periodo_ac,
                                            par$cod_usuario               => par$cod_usuario);
                END IF;
                 /*Relacionar periodo de acreditacion al periodo de recojo*/
                SELECT COUNT(1) INTO var$existe
                FROM SAPR_TPERIODO_RELACION
                WHERE OID_PERIODO = par$oid_periodo_ac AND OID_PERIODO_RELACIONADO = var$oid_periodo_re;

                IF var$existe = 0 THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de acreditación al período de Recojo',
                                        par$cod_identificador   => '');
                    --Relacionamos el período de acreditación creado al período de Recojo 
                    -- par$oid_periodo              = PERIODO DE ACREDITACION       
                    -- par$oid_periodo_relacionado  = PERIODO DE RECOJO 
                    sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_ac,
                                            par$oid_periodo_relacionado   => var$oid_periodo_re,
                                            par$cod_usuario               => par$cod_usuario);
                END IF;
                par$oid_periodo_re := var$oid_periodo_re;
            END IF;
        END IF;

    END IF;


END svalidar_periodo_recojo;


PROCEDURE svalidar_periodo_boveda(
        par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_maquina                 IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$oid_planificacion           IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$usr_,
        par$oid_periodo_ac              IN gepr_pcomon_###VERSION###.tipo$oid_, 
        par$oid_periodo_re              IN gepr_pcomon_###VERSION###.tipo$oid_, 
        par$cod_subcanal                IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_divisa                  IN gepr_pcomon_###VERSION###.tipo$oid_,
        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$collection_id               IN gepr_pcomon_###VERSION###.tipo$cod_,
        par$oid_documento               IN gepr_pcomon_###VERSION###.tipo$oid_

) IS
const$des_origen      CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.svalidar_periodo_boveda'; 
var$oid_documento                gepr_pcomon_###VERSION###.tipo$oid_ := NULL;
var$fyh_documento            gepr_pcomon_###VERSION###.tipo$fyh_;
var$oid_estado_periodo_EC    gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_tipo_periodo         gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_periodo_bo           gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_periodo_bo_new       gepr_pcomon_###VERSION###.tipo$oid_;
var$cod_collection_id_per    gepr_pcomon_###VERSION###.tipo$cod_;
var$bol_periodo_subcanal     gepr_pcomon_###VERSION###.tipo$nbol_ := 0; 
var$bol_periodo_divisa       gepr_pcomon_###VERSION###.tipo$nbol_ := 0; 
var$oid_subcanal             gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_divisa               gepr_pcomon_###VERSION###.tipo$oid_;
var$oid_pto_servicio         gepr_pcomon_###VERSION###.tipo$oid_;
var$existe                   gepr_pcomon_###VERSION###.tipo$nel_;
--Variables para el procedure srelacionar_periodo
    var$validaciones    gepr_pcomon_###VERSION###.tipo$descs_;
    var$cod_ejecucion   gepr_pcomon_###VERSION###.tipo$nel_;

BEGIN
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'oid_llamada: '||par$oid_llamada||
                            ', oid_maquina: '||par$oid_maquina||
                            ', oid_planificacion: '||par$oid_planificacion||
                            ', cod_usuario: '||par$cod_usuario||
                            ', collection_id: '||par$collection_id||
                            ', oid_documento: '||par$oid_documento||
                            ', oid_periodo_ac: '||par$oid_periodo_ac || 
                            ', cod_subcanal: '||par$cod_subcanal ||
                            ', oid_divisa: '||par$oid_divisa,
                            par$cod_identificador   => '');


    SELECT OID_ESTADO_PERIODO INTO var$oid_estado_periodo_EC FROM SAPR_TESTADO_PERIODO WHERE COD_ESTADO_PERIODO = 'EC';

    SELECT P.BOL_PERIODO_SUBCANAL, P.BOL_PERIODO_DIVISA
       INTO var$bol_periodo_subcanal, var$bol_periodo_divisa
       FROM SAPR_TPLANIFICACION P 
       WHERE P.OID_PLANIFICACION = par$oid_planificacion;
    /*Obtengo el tipo de documento y la fyh_documento*/
    begin
      SELECT DOCU.OID_DOCUMENTO, DOCU.FYH_GESTION into var$oid_documento, var$fyh_documento FROM SAPR_TDOCUMENTO DOCU
      INNER JOIN SAPR_TFORMULARIO FORMU ON FORMU.OID_FORMULARIO = DOCU.OID_FORMULARIO
      WHERE FORMU.COD_FORMULARIO IN ('MAECIC','MAECID') AND DOCU.OID_DOCUMENTO = par$oid_documento;
    exception
      when no_data_found then
        var$oid_documento := NULL;
    end;

    IF var$oid_documento is not null THEN

        BEGIN
            /*Obtengo el periodo de boveda en creacion*/
            SELECT PERI.OID_PERIODO , PERI.OID_TIPO_PERIODO, PERI.COD_COLLECTION_ID, PERI.OID_SUBCANAL, PERI.OID_DIVISA, PERI.OID_PTO_SERVICIO
              INTO var$oid_periodo_bo, var$oid_tipo_periodo, var$cod_collection_id_per, var$oid_subcanal, var$oid_divisa, var$oid_pto_servicio
            FROM SAPR_TPERIODO PERI
              INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PERI.OID_TIPO_PERIODO 
              INNER JOIN SAPR_TPLANIFICACION PLANI ON PLANI.OID_PLANIFICACION = PERI.OID_PLANIFICACION
              INNER JOIN SAPR_TTIPO_PLANIFICACION TPLANI ON TPLANI.OID_TIPO_PLANIFICACION = PLANI.OID_TIPO_PLANIFICACION AND TIPE.COD_TIPO_PERIODO = 'BO'
              INNER JOIN SAPR_TPROCESOXPLANIFICACION TPXP ON TPXP.OID_PLANIFICACION = PLANI.OID_PLANIFICACION
              INNER JOIN SAPR_TPROCESO TPRO ON TPRO.OID_PROCESO = TPXP.OID_PROCESO
              LEFT JOIN GEPR_TSUBCANAL SCANAL ON SCANAL.OID_SUBCANAL = PERI.OID_SUBCANAL
            WHERE PERI.OID_MAQUINA = par$oid_maquina
              AND TPLANI.COD_TIPO_PLANIFICACION = 'FECHA_VALOR_CONFIR' 
              AND TPRO.COD_PROCESO = 'BBVA_PERU'
              AND ((SCANAL.COD_SUBCANAL = par$cod_subcanal AND var$bol_periodo_subcanal = 1) OR (var$bol_periodo_subcanal = 0 AND PERI.OID_SUBCANAL IS NULL))
              AND ((PERI.OID_DIVISA = par$oid_divisa AND var$bol_periodo_divisa = 1) OR (var$bol_periodo_divisa = 0 AND PERI.OID_DIVISA IS NULL))
              AND PERI.OID_PLANIFICACION = par$oid_planificacion
              AND PERI.OID_ESTADO_PERIODO = var$oid_estado_periodo_EC
              AND PERI.FYH_FIN IS NULL
            ORDER BY PERI.FYH_INICIO
            FETCH FIRST ROW ONLY;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
              var$oid_periodo_bo := NULL;
               SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'No hay periodo de boveda en creacion',
                par$cod_identificador   => '');
        END;

         SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$des_origen,
                par$des_version     => const$version,                            
                par$des_detalle     => 'Valida var$oid_periodo_bo:' || var$oid_periodo_bo,
                par$cod_identificador   => '');

        IF var$oid_periodo_bo is not null  THEN
            IF var$cod_collection_id_per IS NOT NULL THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$des_origen,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'El collectionID del periodo es :' || var$cod_collection_id_per,
                            par$cod_identificador   => '');

                IF var$cod_collection_id_per <> par$collection_id THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'El collectionID del periodo:' || var$cod_collection_id_per || ' es distinto al del documento: ' || par$collection_id,
                                        par$cod_identificador   => '');
                    /*Crear nuevo periodo de boveda*/
                      sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                        par$oid_periodo               => var$oid_periodo_bo_new,
                        par$oid_planificacion         => par$oid_planificacion,
                        par$oid_maquina               => par$oid_maquina,
                        par$oid_estado_periodo        => var$oid_estado_periodo_EC,
                        par$fyh_inicio                => var$fyh_documento,
                        par$fyh_fin                   => NULL,
                        par$oid_tipo_periodo          => var$oid_tipo_periodo,
                        par$collection_id             => par$collection_id,
                        par$oid_subcanal              => var$oid_subcanal,
                        par$oid_divisa                => var$oid_divisa,
                        par$oid_pto_servicio          => var$oid_pto_servicio,
                        par$cod_periodo_confirmacion  => NULL,
                        par$cod_usuario               => par$cod_usuario);
                    /*Relacionar documento al nuevo periodo de boveda*/
                     --Se relaciona el documento al período de boveda y se suma los valores al SAPR_TCALCULO_EFECTIVO
                    srelacionar_periodo (par$oid_llamada      =>  par$oid_llamada,
                                par$oid_documento    =>  par$oid_documento,
                                par$oid_periodo      =>  var$oid_periodo_bo_new,
                                par$cod_usuario      =>  par$cod_usuario,
                                par$cod_cultura      =>  par$cod_cultura,
                                par$info_ejecucion   =>  '',
                                par$hacer_commit     =>  0,
                                par$validaciones     =>  var$validaciones,
                                par$cod_ejecucion    =>  var$cod_ejecucion);


                    IF par$oid_periodo_re IS NOT NULL THEN
                    /*Relacionar nuevo periodo de boveda al periodo de recojo*/
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de boveda al período de recojo',
                                        par$cod_identificador   => '');

                    --Relacionamos el período de BOVEDA creado al período de recojo 
                    -- par$oid_periodo              = PERIODO DE BOVEDA      
                    -- par$oid_periodo_relacionado  = PERIODO DE RECOJO
                    sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_bo_new,
                                            par$oid_periodo_relacionado   => par$oid_periodo_re,
                                            par$cod_usuario               => par$cod_usuario);

                     /*Relacionar nuevo periodo de recojo al periodo de boveda*/
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de recojo al período de boveda',
                                        par$cod_identificador   => '');

                    --Relacionamos el período de RECOJO creado al período de boveda 
                    -- par$oid_periodo              = PERIODO DE RECOJO      
                    -- par$oid_periodo_relacionado  = PERIODO DE BOVEDA
                    sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_re,
                                            par$oid_periodo_relacionado   => var$oid_periodo_bo_new,
                                            par$cod_usuario               => par$cod_usuario);
                    END IF;

                    /*Relacionar nuevo periodo de boveda al periodo de acreditacion*/

                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de boveda al período de acreditación',
                                        par$cod_identificador   => '');
                    --Relacionamos el período de boveda creado al período de acreditación 
                    -- par$oid_periodo              = PERIODO DE BOVEDA       
                    -- par$oid_periodo_relacionado  = PERIODO DE ACREDITACION 
                    sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_bo_new,
                                            par$oid_periodo_relacionado   => par$oid_periodo_ac,
                                            par$cod_usuario               => par$cod_usuario);

                     /*Relacionar nuevo periodo de acreditacion al periodo de boveda*/

                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de acreditacion al período de boveda',
                                        par$cod_identificador   => '');
                    --Relacionamos el período de acreditacion creado al período de boveda 
                    -- par$oid_periodo              = PERIODO DE ACREDITACION       
                    -- par$oid_periodo_relacionado  = PERIODO DE BOVEDA 
                    sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_ac,
                                            par$oid_periodo_relacionado   => var$oid_periodo_bo_new,
                                            par$cod_usuario               => par$cod_usuario);
                ELSE
                    /*Relacionar documento al periodo de boveda*/
                     --Se relaciona el documento al período de boveda y se suma los valores al SAPR_TCALCULO_EFECTIVO
                    srelacionar_periodo (par$oid_llamada      =>  par$oid_llamada,
                                par$oid_documento    =>  par$oid_documento,
                                par$oid_periodo      =>  var$oid_periodo_bo,
                                par$cod_usuario      =>  par$cod_usuario,
                                par$cod_cultura      =>  par$cod_cultura,
                                par$info_ejecucion   =>  '',
                                par$hacer_commit     =>  0,
                                par$validaciones     =>  var$validaciones,
                                par$cod_ejecucion    =>  var$cod_ejecucion);

                    IF par$oid_periodo_re IS NOT NULL THEN
                    /*Relacionar periodo de boveda al periodo de recojo*/
                    SELECT COUNT(1) INTO var$existe
                    FROM SAPR_TPERIODO_RELACION
                    WHERE OID_PERIODO = var$oid_periodo_bo AND OID_PERIODO_RELACIONADO = par$oid_periodo_re;

                    IF var$existe = 0 THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                            par$des_origen      => const$des_origen,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Previo a relacionar el período de boveda al período de recojo',
                                            par$cod_identificador   => '');
                        --Relacionamos el período de boveda creado al período de recojo 
                        -- par$oid_periodo              = PERIODO DE BOVEDA       
                        -- par$oid_periodo_relacionado  = PERIODO DE RECOJO 
                        sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_bo,
                                                par$oid_periodo_relacionado   => par$oid_periodo_re,
                                                par$cod_usuario               => par$cod_usuario);
                    END IF;
                    /*Relacionar periodo de recojo al periodo de boveda*/
                    SELECT COUNT(1) INTO var$existe
                    FROM SAPR_TPERIODO_RELACION
                    WHERE OID_PERIODO = par$oid_periodo_re AND OID_PERIODO_RELACIONADO = var$oid_periodo_bo;

                    IF var$existe = 0 THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                            par$des_origen      => const$des_origen,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Previo a relacionar el período de recojo al período de boveda',
                                            par$cod_identificador   => '');
                        --Relacionamos el período de recojo creado al período de boveda 
                        -- par$oid_periodo              = PERIODO DE RECOJO       
                        -- par$oid_periodo_relacionado  = PERIODO DE BOVEDA 
                        sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_re,
                                                par$oid_periodo_relacionado   => var$oid_periodo_bo,
                                                par$cod_usuario               => par$cod_usuario);
                    END IF;
                    END IF;

                    /*Relacionar periodo de boveda al periodo de acreditacion*/
                    SELECT COUNT(1) INTO var$existe
                    FROM SAPR_TPERIODO_RELACION
                    WHERE OID_PERIODO = var$oid_periodo_bo AND OID_PERIODO_RELACIONADO = par$oid_periodo_ac;

                    IF var$existe = 0 THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                            par$des_origen      => const$des_origen,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Previo a relacionar el período de boveda al período de acreditación',
                                            par$cod_identificador   => '');
                        --Relacionamos el período de boveda creado al período de acreditación 
                        -- par$oid_periodo              = PERIODO DE BOVEDA       
                        -- par$oid_periodo_relacionado  = PERIODO DE ACREDITACION 
                        sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_bo,
                                                par$oid_periodo_relacionado   => par$oid_periodo_ac,
                                                par$cod_usuario               => par$cod_usuario);
                    END IF;

                    /*Relacionar periodo de acreditacion al periodo de boveda*/
                    SELECT COUNT(1) INTO var$existe
                    FROM SAPR_TPERIODO_RELACION
                    WHERE OID_PERIODO = par$oid_periodo_ac AND OID_PERIODO_RELACIONADO = var$oid_periodo_bo;

                    IF var$existe = 0 THEN
                        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                            par$des_origen      => const$des_origen,
                                            par$des_version     => const$version,                            
                                            par$des_detalle     => 'Previo a relacionar el período de acreditacion al período de boveda',
                                            par$cod_identificador   => '');
                        --Relacionamos el período de acreditacion creado al período de boveda 
                        -- par$oid_periodo              = PERIODO DE ACREDITACION       
                        -- par$oid_periodo_relacionado  = PERIODO DE BOVEDA 
                        sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_ac,
                                                par$oid_periodo_relacionado   => var$oid_periodo_bo,
                                                par$cod_usuario               => par$cod_usuario);
                    END IF;
                END IF;
            ELSE
                /*Actualizar periodo actual con el collection id del documento*/
                sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                      par$oid_periodo               => var$oid_periodo_bo,
                      par$oid_planificacion         => NULL,
                      par$oid_maquina               => NULL,
                      par$oid_estado_periodo        => NULL,
                      par$fyh_inicio                => NULL,
                      par$fyh_fin                   => NULL,
                      par$oid_tipo_periodo          => NULL,
                      par$collection_id             => par$collection_id,
                      par$cod_periodo_confirmacion  => NULL,
                      par$cod_usuario               => par$cod_usuario);
                /*Relacionar documento al periodo de boveda*/
                srelacionar_periodo (par$oid_llamada      =>  par$oid_llamada,
                            par$oid_documento    =>  par$oid_documento,
                            par$oid_periodo      =>  var$oid_periodo_bo,
                            par$cod_usuario      =>  par$cod_usuario,
                            par$cod_cultura      =>  par$cod_cultura,
                            par$info_ejecucion   =>  '',
                            par$hacer_commit     =>  0,
                            par$validaciones     =>  var$validaciones,
                            par$cod_ejecucion    =>  var$cod_ejecucion);

                IF par$oid_periodo_re IS NOT NULL THEN
                /*Relacionar periodo de boveda al periodo de recojo*/
                SELECT COUNT(1) INTO var$existe
                FROM SAPR_TPERIODO_RELACION
                WHERE OID_PERIODO = var$oid_periodo_bo AND OID_PERIODO_RELACIONADO = par$oid_periodo_re;

                IF var$existe = 0 THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de boveda al período de recojo',
                                        par$cod_identificador   => '');
                    --Relacionamos el período de boveda creado al período de recojo 
                    -- par$oid_periodo              = PERIODO DE BOVEDA       
                    -- par$oid_periodo_relacionado  = PERIODO DE RECOJO 
                    sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_bo,
                                            par$oid_periodo_relacionado   => par$oid_periodo_re,
                                            par$cod_usuario               => par$cod_usuario);
                END IF;

                /*Relacionar periodo de recojo al periodo de boveda*/
                SELECT COUNT(1) INTO var$existe
                FROM SAPR_TPERIODO_RELACION
                WHERE OID_PERIODO = par$oid_periodo_re AND OID_PERIODO_RELACIONADO = var$oid_periodo_bo;

                IF var$existe = 0 THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de recojo al período de boveda',
                                        par$cod_identificador   => '');
                    --Relacionamos el período de recojo creado al período de boveda 
                    -- par$oid_periodo              = PERIODO DE RECOJO       
                    -- par$oid_periodo_relacionado  = PERIODO DE BOVEDA 
                    sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_re,
                                            par$oid_periodo_relacionado   => var$oid_periodo_bo,
                                            par$cod_usuario               => par$cod_usuario);
                END IF;
                END IF;

                /*Relacionar periodo de boveda al periodo de acreditacion*/
                SELECT COUNT(1) INTO var$existe
                FROM SAPR_TPERIODO_RELACION
                WHERE OID_PERIODO = var$oid_periodo_bo AND OID_PERIODO_RELACIONADO = par$oid_periodo_ac;

                IF var$existe = 0 THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de boveda al período de acreditación',
                                        par$cod_identificador   => '');
                    --Relacionamos el período de Recojo creado al período de acreditación 
                    -- par$oid_periodo              = PERIODO DE BOVEDA       
                    -- par$oid_periodo_relacionado  = PERIODO DE ACREDITACION 
                    sins_tperiodo_relacion(par$oid_periodo                => var$oid_periodo_bo,
                                            par$oid_periodo_relacionado   => par$oid_periodo_ac,
                                            par$cod_usuario               => par$cod_usuario);
                END IF;
                /*Relacionar periodo de acreditacion al periodo de bovedo*/
                SELECT COUNT(1) INTO var$existe
                FROM SAPR_TPERIODO_RELACION
                WHERE OID_PERIODO = par$oid_periodo_ac AND OID_PERIODO_RELACIONADO = var$oid_periodo_bo;

                IF var$existe = 0 THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                        par$des_origen      => const$des_origen,
                                        par$des_version     => const$version,                            
                                        par$des_detalle     => 'Previo a relacionar el período de acreditacion al período de boveda',
                                        par$cod_identificador   => '');
                    --Relacionamos el período de acreditacion creado al período de boveda 
                    -- par$oid_periodo              = PERIODO DE ACREDITACION       
                    -- par$oid_periodo_relacionado  = PERIODO DE BOVEDA 
                    sins_tperiodo_relacion(par$oid_periodo                => par$oid_periodo_ac,
                                            par$oid_periodo_relacionado   => var$oid_periodo_bo,
                                            par$cod_usuario               => par$cod_usuario);
                END IF;
            END IF;
        END IF;

    END IF;


END svalidar_periodo_boveda;

 /* Verifica si el documento debe ser relacioando a período en la entrada de movimientos */
  FUNCTION fregla_doc_periodo_ac(
    par$oid_llamada      IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_documento  IN gepr_pcomon_###VERSION###.tipo$oid_
  ) RETURN BOOLEAN
  AS 
    const$nombre_func CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.fverificar_documento_periodo';   
    var$existe                  gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_formulario          gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_tipo_sector_des     gepr_pcomon_###VERSION###.tipo$cod_;
    var$bol_considera_recuentos gepr_pcomon_###VERSION###.tipo$nbol_;
    var$cod_estado_doc          gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN
    /* recuperar informaciones del documento */
    SELECT
      D.COD_ESTADO,
      F.cod_formulario,
      TS.COD_TIPO_SECTOR,
      NVL(M.BOL_CONSIDERA_RECUENTOS, 1)
    INTO 
      var$cod_estado_doc,
      var$cod_formulario,
      var$cod_tipo_sector_des,
      var$bol_considera_recuentos
    FROM SAPR_TDOCUMENTO d
      INNER JOIN SAPR_TFORMULARIO F ON F.OID_FORMULARIO = D.OID_FORMULARIO
      INNER JOIN GEPR_TSECTOR S ON S.OID_SECTOR = D.oid_sector_destino
      INNER JOIN GEPR_TTIPO_SECTOR TS ON TS.OID_TIPO_SECTOR = S.OID_TIPO_SECTOR
      LEFT JOIN SAPR_TMAQUINA M ON M.OID_SECTOR = S.OID_SECTOR
    WHERE oid_documento = par$oid_documento;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$nombre_func,
                            par$des_version     => const$version,                            
                            par$des_detalle     => 'Se recuperan informaciones del documento de oid_documento : ' || par$oid_documento || const$new_line ||  
                                                  'var$cod_estado_doc: '          || var$cod_estado_doc           || const$new_line ||  
                                                  'var$cod_formulario: '          || var$cod_formulario           || const$new_line ||  
                                                  'var$cod_tipo_sector_des: '     || var$cod_tipo_sector_des      || const$new_line ||  
                                                  'var$bol_considera_recuentos: ' || var$bol_considera_recuentos,
                            par$cod_identificador   => '');


    SELECT COUNT(1)
      INTO var$existe
      FROM SAPR_TDOCUMENTO DOCU
      LEFT JOIN SAPR_TCUENTA CORI ON DOCU.OID_CUENTA_ORIGEN = CORI.OID_CUENTA
      LEFT JOIN GEPR_TSECTOR SORI ON SORI.OID_SECTOR = CORI.OID_SECTOR
      LEFT JOIN SAPR_TCUENTA CDES ON DOCU.OID_CUENTA_DESTINO = CDES.OID_CUENTA
      LEFT JOIN GEPR_TSECTOR SDES ON SDES.OID_SECTOR = CDES.OID_SECTOR
      WHERE DOCU.OID_DOCUMENTO = par$oid_documento
      AND (SDES.COD_SECTOR =  gepr_pcomon_###VERSION###.const$CodSectorBoveda OR SORI.COD_SECTOR =  gepr_pcomon_###VERSION###.const$CodSectorBoveda);

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     =>  'Valida si existe el documento y que el código de sector origen o destino sea ' || gepr_pcomon_###VERSION###.const$CodSectorBoveda || 
                              ' se regresa var$existe: ' || var$existe || ' en caso de existir no se debería relacionar',
                              par$cod_identificador   => '');

     IF (var$existe = 0 ) 
        AND var$cod_estado_doc = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado 
        AND var$cod_tipo_sector_des = gepr_pcomon_###VERSION###.const$CodTipoSectorMae 
        AND (
            var$cod_formulario = 'MAECID' 
            OR var$cod_formulario = 'MAECIC'  
            OR var$cod_formulario = 'MAEAJN' 
            OR var$cod_formulario = 'MAEAJP'  
            OR var$cod_formulario = 'MAECOC'
            OR (var$cod_formulario = 'MAEREC' AND var$bol_considera_recuentos = 1)
          ) THEN

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     =>  'Se retorna TRUE',
                      par$cod_identificador   => '');

        RETURN TRUE;
      END IF;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     =>  'Se retorna FALSE',
                    par$cod_identificador   => '');
    RETURN FALSE;

  END fregla_doc_periodo_ac;

  /* Almacena en la tabla auxiliar los rangos de fechas de referencia para la creación de períodos */
  PROCEDURE sgenerar_fechas_periodo(par$oid_llamada       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_maquina        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$fyh_inicio_periodo IN gepr_pcomon_###VERSION###.tipo$fyh_)
  IS
    const$nombre_func CONSTANT    gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.sgenerar_fechas_periodo';      

    var$oid_planificacion         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_delegacion            gepr_pcomon_###VERSION###.tipo$oid_;
    var$fyh_vigencia_inicio       gepr_pcomon_###VERSION###.tipo$fyh_;    
    var$fyh_vigencia_fin          gepr_pcomon_###VERSION###.tipo$fyh_;   
    var$fyh_inicio                gepr_pcomon_###VERSION###.tipo$fyh_;   
    var$fyh_fin                   gepr_pcomon_###VERSION###.tipo$fyh_; 
    var$fyh_fin_str               gepr_pcomon_###VERSION###.tipo$desc_; 
    var$fyh_fin_gmt               gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fecha_referencia          gepr_pcomon_###VERSION###.tipo$fyh_;
    var$fecha_referencia_gmt      gepr_pcomon_###VERSION###.tipo$fyh_;
    var$cantidad_de_dias          gepr_pcomon_###VERSION###.tipo$nel_:= 0;
    var$dia                       gepr_pcomon_###VERSION###.tipo$nel_;

    /* Programacion */
    CURSOR cur$programacion(dia gepr_pcomon_###VERSION###.tipo$nel_) IS
      SELECT 
        PM.OID_PLANIFICACION,
        P.COD_PLANIFICACION,
        PP.FYH_HORA_INICIO,
        PP.FYH_HORA_FIN,
        PP.NEC_DIA_INICIO,
        PP.NEC_DIA_FIN
      FROM SAPR_TPLANXMAQUINA PM 
        INNER JOIN SAPR_TPLANIFICACION P ON P.OID_PLANIFICACION = PM.OID_PLANIFICACION
        INNER JOIN SAPR_TPLANXPROGRAMACION PP ON PP.OID_PLANIFICACION = P.OID_PLANIFICACION
      WHERE PM.BOL_ACTIVO = 1 AND P.BOL_ACTIVO = 1 AND PM.OID_MAQUINA= par$oid_maquina
        AND PP.NEC_DIA_FIN = dia
      ORDER BY PP.FYH_HORA_FIN
        ;
    rec$programacion cur$programacion%rowtype;

  BEGIN
    DELETE FROM SAPR_GTT_TAUXILIAR
    WHERE COD_CALIFICADOR = 'DATOS_FECHAS_PERIODO';

    -- Buscamos los datos de la planificación de la máquina
    BEGIN
      SELECT 
        PM.OID_PLANIFICACION,
        PM.FYH_VIGENCIA_INICIO,
        NVL(PM.FYH_VIGENCIA_FIN,P.FYH_VIGENCIA_FIN),
        DELE.OID_DELEGACION
      INTO 
        var$oid_planificacion,   
        var$fyh_vigencia_inicio,
        var$fyh_vigencia_fin,
        var$oid_delegacion
      FROM SAPR_TPLANXMAQUINA PM 
        INNER JOIN SAPR_TPLANIFICACION P ON P.OID_PLANIFICACION = PM.OID_PLANIFICACION
        INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = P.OID_DELEGACION 
      WHERE PM.BOL_ACTIVO = 1 AND P.BOL_ACTIVO = 1 AND PM.OID_MAQUINA = par$oid_maquina;

    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_planificacion := NULL;
    END;

    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                  par$des_origen      => const$nombre_func,
                  par$des_version     => const$version,                            
                  par$des_detalle     => 'Obteniendo planificación oid_planificacion: '  || var$oid_planificacion || const$new_line || 
                                          'fyh_vigencia_inicio: '  || TO_CHAR(var$fyh_vigencia_inicio, 'DD-MM-YYYY HH24:MI:SS') || const$new_line || 
                                          'fyh_vigencia_fin: '     || TO_CHAR(var$fyh_vigencia_fin, 'DD-MM-YYYY HH24:MI:SS'),
                  par$cod_identificador   => '');

    IF var$oid_planificacion IS NOT NULL AND (var$fyh_vigencia_inicio <= par$fyh_inicio_periodo 
            AND (var$fyh_vigencia_fin IS NULL OR par$fyh_inicio_periodo <= var$fyh_vigencia_fin)
            AND par$fyh_inicio_periodo <= SYS_EXTRACT_UTC (CURRENT_TIMESTAMP)) THEN

      var$fyh_inicio := par$fyh_inicio_periodo;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                    par$des_origen          => const$nombre_func,
                                    par$des_version         => const$version,                            
                                    par$des_detalle         => 'Inicia recorrido hasta que var$fyh_fin es mayor que fecha actual.',
                                    par$cod_identificador   => '');

      var$fyh_fin_gmt := null;
      LOOP EXIT WHEN  var$fyh_fin_gmt IS NOT NULL AND (var$fyh_fin_gmt > sys_extract_utc(current_timestamp) OR (var$fyh_vigencia_fin IS NOT NULL AND var$fyh_fin_gmt = var$fyh_vigencia_fin));
        var$fecha_referencia := par$fyh_inicio_periodo + var$cantidad_de_dias;  

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                      par$des_origen          => const$nombre_func,
                                      par$des_version         => const$version,                            
                                      par$des_detalle         => 'Se establece fecha de referencia ' || TO_CHAR(var$fecha_referencia, 'YYYY-MM-DD HH24:MI:SS'),
                                      par$cod_identificador   => '');

        -- Convertimos la fecha de referencia al GMT de la delegación
        var$fecha_referencia_gmt := FN_GMT_DELEGACION_###VERSION###(var$oid_delegacion, var$fecha_referencia);


        -- Obtengo el día de la semana  
        -- 1: Lunes al 7: Domingo      
        var$dia := TRUNC(var$fecha_referencia_gmt) - TRUNC(var$fecha_referencia_gmt, 'IW') + 1;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                      par$des_origen          => const$nombre_func,
                                      par$des_version         => const$version,                            
                                      par$des_detalle         => 'Recorre cursor cur$programacion para la fecha de referencia en GMT ' || TO_CHAR(var$fecha_referencia_gmt, 'YYYY-MM-DD HH24:MI:SS') || ' de día: ' || var$dia,
                                      par$cod_identificador   => '');

        OPEN cur$programacion(var$dia);
        LOOP
            FETCH cur$programacion INTO rec$programacion;
            EXIT WHEN cur$programacion%notfound;

            -- Concatenamos la fecha de referencia en GMT, la hora de la programación (ya se encuentra en el GMT de la delegación) y el GMT de la delegación
            SELECT TO_CHAR(var$fecha_referencia_gmt, 'YYYY/MM/DD') || ' ' || TO_CHAR(rec$programacion.FYH_HORA_FIN, 'HH24:MI:SS') || NVL(CASE WHEN NVL(DELE.NEC_GMT_MINUTOS, 0) <> 0 THEN TO_CHAR(FLOOR(((CASE WHEN TRUNC(var$fecha_referencia_gmt) >= TRUNC(DELE.FYH_VERANO_INICIO) AND TRUNC(var$fecha_referencia_gmt) <= TRUNC(DELE.FYH_VERANO_FIN) THEN NVL(DELE.NEC_VERANO_AJUSTE,0) ELSE 0 END) + DELE.NEC_GMT_MINUTOS) / 60), '09') || ':' || TO_CHAR(FLOOR(MOD((CASE WHEN TRUNC(var$fecha_referencia_gmt) >= TRUNC(DELE.FYH_VERANO_INICIO) AND TRUNC(var$fecha_referencia_gmt) <= TRUNC(DELE.FYH_VERANO_FIN) THEN NVL(DELE.NEC_VERANO_AJUSTE,0) ELSE 0 END)+DELE.NEC_GMT_MINUTOS, 60)), '09') END, '') 
              INTO var$fyh_fin_str
            FROM SAPR_TPLANIFICACION PLANI
              INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = PLANI.OID_DELEGACION 
            WHERE PLANI.OID_PLANIFICACION = var$oid_planificacion
            ;

            SELECT sys_extract_utc(TO_TIMESTAMP_TZ(var$fyh_fin_str,'YYYY-MM-DD HH24:MI:SSTZH:TZM'))
              INTO var$fyh_fin_gmt
            FROM DUAL;


            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                          par$des_origen          => const$nombre_func,
                                          par$des_version         => const$version,                            
                                          par$des_detalle         => 'Las fechas generadas para su evaluación son las siguientes,'
                                                                    || ' var$fyh_inicio:' || TO_CHAR(var$fyh_inicio, 'YYYY/MM/DD HH24:MI:SS') 
                                                                    || ' var$fyh_fin_gmt: ' || TO_CHAR(var$fyh_fin_gmt, 'YYYY/MM/DD HH24:MI:SS'),
                                          par$cod_identificador   => '');

            -- Evalúo si la fecha fin del período es superior a la vigencia de la máquina
            IF (var$fyh_vigencia_fin IS NOT NULL AND var$fyh_fin_gmt > var$fyh_vigencia_fin) THEN 
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen          => const$nombre_func,
                                            par$des_version         => const$version,                            
                                            par$des_detalle         => 'La fecha fin del período: ' || TO_CHAR(var$fyh_fin_gmt, 'DD-MM-YYYY HH24:MI:SS') 
                                                                        || ' es mayor que la fecha fin de vigencia de la máquina: ' || TO_CHAR(var$fyh_vigencia_fin, 'DD-MM-YYYY HH24:MI:SS'),
                                            par$cod_identificador   => '');

              var$fyh_fin_gmt := var$fyh_vigencia_fin;

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen          => const$nombre_func,
                                            par$des_version         => const$version,                            
                                            par$des_detalle         => 'Asigno como fecha fin del período: ' || TO_CHAR(var$fyh_fin_gmt, 'DD-MM-YYYY HH24:MI:SS') 
                                                                        || ' la fecha fin de vigencia de la máquina',
                                            par$cod_identificador   => '');
            END IF;


            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                          par$des_origen          => const$nombre_func,
                                          par$des_version         => const$version,                            
                                          par$des_detalle         => 'Evalúo si la fecha fin del período: ' || TO_CHAR(var$fyh_fin_gmt, 'DD-MM-YYYY HH24:MI:SS') 
                                                                      || ' es mayor que la fecha de inicio: ' || TO_CHAR(var$fyh_inicio, 'DD-MM-YYYY HH24:MI:SS'),
                                          par$cod_identificador   => '');

            IF(var$fyh_fin_gmt > var$fyh_inicio) THEN

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                            par$des_origen          => const$nombre_func,
                                            par$des_version         => const$version,                            
                                            par$des_detalle         => 'Inserto en la tabla auxiliar las fechas del período',
                                            par$cod_identificador   => '');

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR, COD_CAMPO2, COD_CAMPO3)
                VALUES (SYS_GUID(),
                'DATOS_FECHAS_PERIODO',
                TO_CHAR(var$fyh_inicio, 'YYYY/MM/DD HH24:MI:SS'),
                TO_CHAR(var$fyh_fin_gmt, 'YYYY/MM/DD HH24:MI:SS'));

                var$fyh_inicio := var$fyh_fin_gmt + (1/60/60/24);

                SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                                              par$des_origen          => const$nombre_func,
                                              par$des_version         => const$version,                            
                                              par$des_detalle         => 'Asigno la nueva fecha de inicio sumando un segundo a la fecha fin, var$fyh_inicio: ' || TO_CHAR(var$fyh_inicio, 'DD-MM-YYYY HH24:MI:SS'),
                                              par$cod_identificador   => '');
            END IF;     

        END LOOP;
        CLOSE cur$programacion;

        var$cantidad_de_dias := var$cantidad_de_dias + 1;
      END LOOP;
    END IF;
  END sgenerar_fechas_periodo;

END sapr_pperiodo_###VERSION###;
/
