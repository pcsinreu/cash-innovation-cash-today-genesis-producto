create or replace PACKAGE SAPR_PDATO_BANCARIO_###VERSION### AS
  /* Version: ###VERSION_COMP### */
  const$vacio CONSTANT               gepr_pcomon_###VERSION###.tipo$desc_ := '###VACIO###';
  const$codFuncionalidad	CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PDATO_BANCARIO_###VERSION###';
  const$version             CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := '###VERSION_COMP###';

  /* Crea o actualiza los datos bancarios de las distintas entiedades (cliente, subcliente, punto) */
  PROCEDURE sconfigurar_dato_bancario(
                        par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                        par$anel_index                  IN gepr_pcomon_###VERSION###.tipo$nels_,
                        par$acod_entidad                IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_accion                 IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$aoid_dato_bancario          IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_cliente                IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_subcliente             IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_pto_servicio           IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$acod_banco                  IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_agencia                IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_tipo_cuenta_bancaria   IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_cuenta_bancaria        IN gepr_pcomon_###VERSION###.tipo$cods_,     
                        par$acod_documento              IN gepr_pcomon_###VERSION###.tipo$cods_,  
                        par$ades_titularidad            IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$acod_divisa                 IN gepr_pcomon_###VERSION###.tipo$cods_, 
                        par$ades_observaciones          IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$abol_defecto                IN gepr_pcomon_###VERSION###.tipo$nbols_,
                        par$abol_activo                  IN gepr_pcomon_###VERSION###.tipo$nbols_,
                        par$ades_campo_adicional_1      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_2      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_3      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_4      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_5      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_6      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_7      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_8      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_comentario             IN gepr_pcomon_###VERSION###.tipo$descs_, 
                        par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_usuario                IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$info_ejecucion             IN VARCHAR2,
                        par$bol_limpiar_temporal        IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$rc_validaciones            OUT sys_refcursor,
                        par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_);

 /* Utilizado por el procedure sconfigurar_dato_bancario  para validar los parametros de entrada*/
 PROCEDURE svalidar_dato_bancario(
                        par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                        par$acod_accion                 IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$anel_index                  IN gepr_pcomon_###VERSION###.tipo$nels_,
                        par$aoid_dato_bancario          IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$acod_entidad                IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$aoid_cliente                IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_subcliente             IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_pto_servicio           IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$acod_banco                  IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_agencia                IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_divisa                 IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_cuenta_bancaria        IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_tipo_cuenta_bancaria   IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$ades_titularidad            IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$oid_usuario  OUT gepr_pcomon_###VERSION###.tipo$oid_ );


   /* Se encarga de insertar o de actualizar registros en la tabla SAPR_TDATO_BANCARIO */   
   PROCEDURE svalidar_params_dats_bans(
    par$cod_cultura                    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_usuario                    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_pais                       IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_dato_bancario              IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_accion                    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_cliente                   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_subcliente                IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_pto_servicio              IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$tipo_cuenta                   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_banco                     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_agencia                   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$nro_cuenta                    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$documento                     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$titularidad                   IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$cod_divisa                    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$observacion                   IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$patron                        IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$campadic1                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic2                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic3                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic4                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic5                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic6                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic7                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic8                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$bol_activo                    IN gepr_pcomon_###VERSION###.tipo$nel_,
    /* Salida */
    par$cod_accion_out                OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_cliente_out               OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_subcli_out                OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_pto_serv_out              OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_banco_out                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$tipo_cuenta_out               OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_agencia_out               OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$nro_cuenta_out                OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$documento_out                 OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$titularidad_out               OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$oid_divisa_out                OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$observacion_out               OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$patron_out                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
    par$campadic1_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic2_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic3_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic4_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic5_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic6_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic7_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic8_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$activo_out                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
    par$acambio_campo_out             OUT gepr_pcomon_###VERSION###.tipo$cods_,
    par$acambio_valor_out             OUT gepr_pcomon_###VERSION###.tipo$descs_);

  /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
 PROCEDURE supd_dato_bancario(par$oid_dato_bancario     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_banco                   IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_cliente                 IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_subcliente              IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_pto_servicio            IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_divisa                  IN gepr_pcomon_###VERSION###.tipo$oid_,  
                        par$cod_tipo_cuenta_bancaria    IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_cuenta_bancaria         IN gepr_pcomon_###VERSION###.tipo$cod_,        
                        par$cod_documento               IN gepr_pcomon_###VERSION###.tipo$cod_,  
                        par$des_titularidad             IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_observaciones           IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$bol_defecto                 IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$bol_activo                  IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$cod_agencia                 IN gepr_pcomon_###VERSION###.tipo$cod_,  
                        par$des_campo_adicional_1       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_2       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_3       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_4       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_5       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_6       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_7       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_8       IN gepr_pcomon_###VERSION###.tipo$desc_,    
                        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_);

  FUNCTION fhay_error  RETURN BOOLEAN;

  /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
  PROCEDURE supd_dato_bancario_cambio(
                            par$oid_dato_banc_cambio IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_dato_bancario  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_campo          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$des_valor          IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$des_comentario     IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$oid_usuario        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$num_aprobaciones   IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_estado         IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$bol_activo         IN gepr_pcomon_###VERSION###.tipo$nel_);

/* Se encarga de generar registros en la tabla SAPR_TDATO_BANCARIO_APROBACION */
PROCEDURE sins_dato_banc_aprobacion(
                            par$oid_dato_banc_cambio  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_usuario        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$des_comentario     IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$bol_aprobado       IN gepr_pcomon_###VERSION###.tipo$nel_) ;
/* Devuelve la información almacenada en SAPR_TDATO_BANCARIO_CAMBIO */
PROCEDURE srecuperar_datos(
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /* ---- FILTROS ---- */
                            /*Estados*/
                            par$acod_estado             IN gepr_pcomon_###VERSION###.tipo$cods_,
                            /*Campos*/
                            par$acod_campo              IN gepr_pcomon_###VERSION###.tipo$cods_,
                            /*Clientes*/
                            par$aoid_cliente            IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_subcliente         IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_pto_servicio       IN gepr_pcomon_###VERSION###.tipo$oids_,
                            /*Usuario aprob*/
                            par$aoid_usu_aprob          IN gepr_pcomon_###VERSION###.tipo$oids_,
                            /*Usuario modif*/
                            par$aoid_usu_modif          IN gepr_pcomon_###VERSION###.tipo$oids_,
                            /*TipoFecha*/
                            par$cod_tipo_fecha          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /*Fecha desde*/
                            par$fecha_desde             IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            /*Fecha hasta*/            
                            par$fecha_hasta             IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            /* ---- FIN FILTROS ---- */
                            par$rc_cambios              OUT sys_refcursor,
                            par$rc_datos                OUT sys_refcursor,
                            par$rc_aprob                OUT sys_refcursor);

  /* saprobar_rechazar: Se encarga de aprobar o rechazar un cambio 
  Genera un registro en SAPR_TDATO_BANCARIO_APROBACION 
  Y actualiza SAPR_TDATO_BANCARIO_CAMBIO*/
  PROCEDURE saprobar_rechazar(
                            par$aoid_dato_banc_cambio  IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$cod_accion        IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$des_comentario    IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$tester_aprobacion IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_pais          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$info_ejecucion    IN VARCHAR2,
                            par$rc_validaciones   OUT sys_refcursor,
                            par$cod_ejecucion     OUT gepr_pcomon_###VERSION###.tipo$nel_);
  /* Se encarga de grabar los registros historicos de los datos bancarios.
  Inserta un nuevo registro en la tabla SAPR_THIST_DATO_BANCARIO */
  PROCEDURE sins_hist_dato_bancario(
    par$oid_dato_bancario IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$bol_hacer_commit IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$oid_hist_dato_bancario OUT gepr_pcomon_###VERSION###.tipo$oid_
  );

  PROCEDURE srecuperar_comparativo( 
                            par$oid_dato_bancario          IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$rc_datos_bancarios          OUT sys_refcursor,
                            par$rc_datos_bancarios_cambio   OUT sys_refcursor);

  FUNCTION fhay_error_aprovacion(par$oid_dato_bancario IN gepr_pcomon_###VERSION###.tipo$oid_) RETURN BOOLEAN;


  PROCEDURE svalidar_dato_banc_aprov(
                        par$cod_accion                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$aoid_dato_banc_cambio       IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$oid_usuario                 IN gepr_pcomon_###VERSION###.tipo$oid_ ) ;

  /* Inserta registros en la tabla SAPR_TDATO_BANCARIO_COMENTARIO */ 
  PROCEDURE sins_dato_banc_comentario(
    par$oid_dato_banc_cambio    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$des_tabla   IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$oid_tabla   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$des_comentario  IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$fyh_comentario IN gepr_pcomon_###VERSION###.tipo$fyh_,
    par$bol_commit  IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$oid_identificador OUT gepr_pcomon_###VERSION###.tipo$oid_);


  /* Recupera los comentarios realizados a un cambio de un dato bancario tanto de las aprobaciones rechazo, del cambio o cualquier comentario realizado al mismo */
  PROCEDURE srecuperar_comentarios (
    par$oid_dato_banc_cambio    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$rc_comentarios_aprob  OUT sys_refcursor,
    par$rc_comentarios_modif  OUT sys_refcursor);

END SAPR_PDATO_BANCARIO_###VERSION###;
/
create or replace PACKAGE BODY SAPR_PDATO_BANCARIO_###VERSION### AS
  
/* Crea o actualiza los datos bancarios de las distintas entiedades (cliente, subcliente, punto) */
  PROCEDURE sconfigurar_dato_bancario(
                        par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                        par$anel_index                  IN gepr_pcomon_###VERSION###.tipo$nels_,
                        par$acod_entidad                IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_accion                 IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$aoid_dato_bancario          IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_cliente                IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_subcliente             IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_pto_servicio           IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$acod_banco                  IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_agencia                IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_tipo_cuenta_bancaria   IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_cuenta_bancaria        IN gepr_pcomon_###VERSION###.tipo$cods_,     
                        par$acod_documento              IN gepr_pcomon_###VERSION###.tipo$cods_,  
                        par$ades_titularidad            IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$acod_divisa                 IN gepr_pcomon_###VERSION###.tipo$cods_, 
                        par$ades_observaciones          IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$abol_defecto                IN gepr_pcomon_###VERSION###.tipo$nbols_,
                        par$abol_activo                 IN gepr_pcomon_###VERSION###.tipo$nbols_,
                        par$ades_campo_adicional_1      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_2      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_3      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_4      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_5      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_6      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_7      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_campo_adicional_8      IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$ades_comentario             IN gepr_pcomon_###VERSION###.tipo$descs_,     
                        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_pais                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$info_ejecucion              IN VARCHAR2,
                        par$bol_limpiar_temporal        IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$rc_validaciones             OUT sys_refcursor,
                        par$cod_ejecucion               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$des_origen        CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.' || 'sconfigurar_dato_bancario';

    /*Variables locales*/
    var$entidad                 gepr_pcomon_###VERSION###.tipo$desc_:= 'DATOBANCARIO'; 
    var$oid_dato_bancario       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_dato_banc_cambio    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_banco               gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa              gepr_pcomon_###VERSION###.tipo$oid_;
    var$activo                  gepr_pcomon_###VERSION###.tipo$nbol_;
    var$necesita_aprobacion     gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$cantidad_aprobaciones   gepr_pcomon_###VERSION###.tipo$desc_;
    var$campos_aprobacion       gepr_pcomon_###VERSION###.tipo$desc_;
    var$cod_accion              gepr_pcomon_###VERSION###.tipo$cod_;
    var$bol_activo              gepr_pcomon_###VERSION###.tipo$nel_;

    /*salida dato bancario*/
    var$cod_accion_out          gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_cliente_out         gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_subcli_out          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_pto_serv_out        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_banco_out           gepr_pcomon_###VERSION###.tipo$oid_;
    var$tipo_cuenta_out         gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_agencia_out         gepr_pcomon_###VERSION###.tipo$cod_;
    var$nro_cuenta_out          gepr_pcomon_###VERSION###.tipo$cod_;
    var$documento_out           gepr_pcomon_###VERSION###.tipo$cod_;
    var$titularidad_out         gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_divisa_out          gepr_pcomon_###VERSION###.tipo$oid_;
    var$observacion_out         gepr_pcomon_###VERSION###.tipo$desc_;
    var$patron_out              gepr_pcomon_###VERSION###.tipo$nel_;
    var$campadic1_out           gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic2_out           gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic3_out           gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic4_out           gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic5_out           gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic6_out           gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic7_out           gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic8_out           gepr_pcomon_###VERSION###.tipo$desc_;
    var$activo_out              gepr_pcomon_###VERSION###.tipo$nel_;
    var$oid_usuario_out         gepr_pcomon_###VERSION###.tipo$oid_;
    var$acambio_campo_out       gepr_pcomon_###VERSION###.tipo$cods_;
    var$acambio_valor_out       gepr_pcomon_###VERSION###.tipo$descs_;
 BEGIN
    BEGIN

        IF par$bol_limpiar_temporal <> 0 THEN
            DELETE FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO'; 
        END IF;
        svalidar_dato_bancario(
                            par$oid_llamada                 => par$oid_llamada,
                            par$acod_accion                 => par$acod_accion,
                            par$anel_index                  => par$anel_index,
                            par$aoid_dato_bancario          => par$aoid_dato_bancario,
                            par$acod_entidad                => par$acod_entidad,
                            par$aoid_cliente                => par$aoid_cliente,
                            par$aoid_subcliente             => par$aoid_subcliente,
                            par$aoid_pto_servicio           => par$aoid_pto_servicio,
                            par$acod_banco                  => par$acod_banco,
                            par$acod_agencia                => par$acod_agencia,
                            par$acod_divisa                 => par$acod_divisa, 
                            par$acod_cuenta_bancaria        => par$acod_cuenta_bancaria,        
                            par$acod_tipo_cuenta_bancaria   => par$acod_tipo_cuenta_bancaria,
                            par$ades_titularidad            => par$ades_titularidad,  
                            par$cod_cultura                 => par$cod_cultura,
                            par$cod_usuario                 => par$cod_usuario,
                            par$oid_usuario                 => var$oid_usuario_out);

        /*2) Recorrer para realizar inserts o updates en la tabla SAPR_TDATO_BANCARIO*/
        
        IF NOT fhay_error THEN
            IF par$anel_index IS NOT NULL AND par$anel_index.COUNT > 0 THEN
                DBMS_OUTPUT.PUT_LINE('No hay error en dato bancario');
                
                FOR idx IN par$anel_index.first .. par$anel_index.last LOOP
                    var$oid_dato_bancario := NULL;
                    var$oid_banco         := NULL;
                    var$oid_divisa        := NULL;
                    var$activo            := NULL;
                    BEGIN
    
                           /*Busco el OID_BANCO en la tabla auxiliar almacenado durante la validacion */ 
                           BEGIN
                                 SELECT OID_CAMPO1
                                    INTO var$oid_banco
                                    FROM SAPR_GTT_TAUXILIAR 
                                    WHERE COD_CALIFICADOR = 'OID_BANCO' AND
                                        COD_CAMPO2 =  par$anel_index(idx) AND ROWNUM = 1;
                            EXCEPTION WHEN no_data_found THEN
                                    var$oid_banco := NULL;
                            END;
    
                            /*Busco el OID_DIVISA en la tabla auxiliar almacenado durante la validacion */ 
                           BEGIN
                                 SELECT OID_CAMPO1
                                    INTO var$oid_divisa
                                    FROM SAPR_GTT_TAUXILIAR 
                                    WHERE COD_CALIFICADOR = 'OID_DIVISA' AND
                                        COD_CAMPO2 =  par$anel_index(idx) AND ROWNUM = 1;
                            EXCEPTION WHEN no_data_found THEN
                                    var$oid_divisa := NULL;
                            END;
    
                            DBMS_OUTPUT.PUT_LINE('Por invocar svalidar_params_dats_bans');

                            

                            /* Identificamos el tipo de acción */
                            var$cod_accion := par$acod_accion(idx);
                            var$bol_activo := par$abol_activo(idx);

                            IF par$aoid_dato_bancario(idx) IS NULL THEN
                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => var$des_origen,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', no se especifica oid_dato_bancario',
                                par$cod_identificador   => '');

                                BEGIN
                                    CASE par$acod_entidad(idx)
                                        WHEN 'CLIENTE'  THEN 
                                            SELECT OID_DATO_BANCARIO
                                            INTO var$oid_dato_bancario
                                            FROM SAPR_TDATO_BANCARIO
                                            WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                            AND OID_SUBCLIENTE IS NULL 
                                            AND OID_PTO_SERVICIO IS NULL
                                            AND OID_BANCO = var$oid_banco 
                                            AND (
                                                    (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                                    OR 
                                                    (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                                )
                                            AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                            AND OID_DIVISA = var$oid_divisa
                                            AND BOL_ACTIVO = 1
                                            AND ROWNUM = 1;
                                        WHEN 'SUBCLIENTE'  THEN
                                            SELECT OID_DATO_BANCARIO
                                            INTO var$oid_dato_bancario
                                            FROM SAPR_TDATO_BANCARIO
                                            WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                            AND OID_SUBCLIENTE = par$aoid_subcliente(idx)
                                            AND OID_PTO_SERVICIO IS NULL
                                            AND OID_BANCO = var$oid_banco 
                                            AND (
                                                    (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                                    OR 
                                                    (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                                )
                                            AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                            AND OID_DIVISA = var$oid_divisa
                                            AND BOL_ACTIVO = 1
                                            AND ROWNUM = 1;
                                        WHEN 'PUNTOSERVICIO'  THEN 
                                            SELECT OID_DATO_BANCARIO
                                            INTO var$oid_dato_bancario
                                            FROM SAPR_TDATO_BANCARIO
                                            WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                            AND OID_SUBCLIENTE = par$aoid_subcliente(idx)
                                            AND OID_PTO_SERVICIO = par$aoid_pto_servicio(idx)
                                            AND OID_BANCO = var$oid_banco
                                            AND (
                                                    (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                                    OR 
                                                    (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                                ) 
                                            AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                            AND OID_DIVISA = var$oid_divisa
                                            AND BOL_ACTIVO = 1
                                            AND ROWNUM = 1;
                                   END CASE;
                                   SAPR_PLOG_API.SAGREGA_DETALLE(
                                    par$oid_llamada  => par$oid_llamada,
                                    par$des_origen      => var$des_origen,
                                    par$des_version     => const$version, 
                                    par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', se encuentra oid_dato_bancario var$oidDatoBancario con el Codigo Banco, Codigo Agencia, Numero Cuenta, Codigo Divisa informado',
                                    par$cod_identificador   => '');
                                    
                                EXCEPTION
                                WHEN others THEN
                                    var$oid_dato_bancario := NULL;
                                END;
                            ELSE
                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => var$des_origen,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', se especifica oid_dato_bancario: ' || par$aoid_dato_bancario(idx),
                                par$cod_identificador   => '');
                              BEGIN
                                select OID_DATO_BANCARIO
                                    into var$oid_dato_bancario
                                    from SAPR_TDATO_BANCARIO
                                where OID_DATO_BANCARIO = par$aoid_dato_bancario(idx);
                              EXCEPTION
                                WHEN NO_DATA_FOUND THEN
                                  SAPR_PLOG_API.SAGREGA_DETALLE(
                                    par$oid_llamada  => par$oid_llamada,
                                    par$des_origen      => var$des_origen,
                                    par$des_version     => const$version,
                                    par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', no se encuentra oid_dato_bancario para el par$aoid_dato_bancario especificado: ' || par$aoid_dato_bancario(idx),
                                    par$cod_identificador   => '');

                                  BEGIN
                                    CASE par$acod_entidad(idx)
                                      WHEN 'CLIENTE'  THEN 
                                          SELECT OID_DATO_BANCARIO
                                          INTO var$oid_dato_bancario
                                          FROM SAPR_TDATO_BANCARIO
                                          WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                          AND OID_SUBCLIENTE IS NULL 
                                          AND OID_PTO_SERVICIO IS NULL
                                          AND OID_BANCO = var$oid_banco 
                                          AND (
                                                  (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                                  OR 
                                                  (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                              )
                                          AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                          AND OID_DIVISA = var$oid_divisa
                                          AND BOL_ACTIVO = 1
                                          AND ROWNUM = 1;
                                      WHEN 'SUBCLIENTE'  THEN
                                          SELECT OID_DATO_BANCARIO
                                          INTO var$oid_dato_bancario
                                          FROM SAPR_TDATO_BANCARIO
                                          WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                          AND OID_SUBCLIENTE = par$aoid_subcliente(idx)
                                          AND OID_PTO_SERVICIO IS NULL
                                          AND OID_BANCO = var$oid_banco 
                                          AND (
                                                  (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                                  OR 
                                                  (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                              )
                                          AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                          AND OID_DIVISA = var$oid_divisa
                                          AND BOL_ACTIVO = 1
                                          AND ROWNUM = 1;
                                      WHEN 'PUNTOSERVICIO'  THEN 
                                          SELECT OID_DATO_BANCARIO
                                          INTO var$oid_dato_bancario
                                          FROM SAPR_TDATO_BANCARIO
                                          WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                          AND OID_SUBCLIENTE = par$aoid_subcliente(idx)
                                          AND OID_PTO_SERVICIO = par$aoid_pto_servicio(idx)
                                          AND OID_BANCO = var$oid_banco
                                          AND (
                                                  (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                                  OR 
                                                  (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                              ) 
                                          AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                          AND OID_DIVISA = var$oid_divisa
                                          AND BOL_ACTIVO = 1
                                          AND ROWNUM = 1;
                                    END CASE;
                                    SAPR_PLOG_API.SAGREGA_DETALLE(
                                      par$oid_llamada  => par$oid_llamada,
                                      par$des_origen      => var$des_origen,
                                      par$des_version     => const$version, 
                                      par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', se encuentra oid_dato_bancario var$oidDatoBancario con el Codigo Banco, Codigo Agencia, Numero Cuenta, Codigo Divisa informado',
                                      par$cod_identificador   => '');
                                  EXCEPTION
                                    WHEN OTHERS THEN
                                    var$oid_dato_bancario := NULL;
                                  END;
                                WHEN OTHERS THEN
                                  var$oid_dato_bancario := NULL;
                              END;
                            END IF;

                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => var$des_origen,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' var$oid_dato_bancario en sconfigurar_dato_bancario: ' || var$oid_dato_bancario || ' de indice ' || idx ,
                                par$cod_identificador   => '');


                            IF (var$cod_accion IS NULL OR var$cod_accion = 'ALTA') AND var$oid_dato_bancario IS NOT NULL THEN
                                var$cod_accion := 'MODIFICAR';
                            ELSIF var$cod_accion = 'BAJA' THEN
                                var$bol_activo := 0;
                            END IF;


                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => var$des_origen,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' var$cod_accion en sconfigurar_dato_bancario: ' || var$cod_accion || ' de indice ' || idx ,
                                par$cod_identificador   => '');


                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => var$des_origen,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' var$bol_activo en sconfigurar_dato_bancario: ' || var$bol_activo || ' de indice ' || idx ,
                                par$cod_identificador   => '');

                            SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => var$des_origen,
                                par$des_version     => const$version,
                                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' par$acod_agencia(idx) en sconfigurar_dato_bancario: ' || par$acod_agencia(idx) || ' de indice ' || idx ,
                                par$cod_identificador   => '');

                            DBMS_OUTPUT.PUT_LINE('Accion para el dato bancario: ' || var$cod_accion);
                            DBMS_OUTPUT.PUT_LINE('par$acod_agencia(idx): ' || par$acod_agencia(idx));

                            /*Para poder indicar si debe estar activo o no el dato bancario*/
                            svalidar_params_dats_bans(
                                                        par$cod_cultura                    => par$cod_cultura,
                                                        par$oid_usuario                    => var$oid_usuario_out,
                                                        par$cod_pais                       => par$cod_pais,
                                                        par$oid_dato_bancario              => var$oid_dato_bancario,
                                                        /* valores de entrada */ 
                                                        par$cod_accion                    => var$cod_accion,
                                                        par$oid_cliente                   => par$aoid_cliente(idx),
                                                        par$oid_subcliente                => par$aoid_subcliente(idx),
                                                        par$oid_pto_servicio              => par$aoid_pto_servicio(idx),
                                                        par$tipo_cuenta                   => par$acod_tipo_cuenta_bancaria(idx),
                                                        par$cod_banco                     => par$acod_banco(idx),
                                                        par$cod_agencia                   => par$acod_agencia(idx),
                                                        par$nro_cuenta                    => par$acod_cuenta_bancaria(idx),
                                                        par$documento                     => par$acod_documento(idx),
                                                        par$titularidad                   => par$ades_titularidad(idx),
                                                        par$cod_divisa                    => par$acod_divisa(idx),
                                                        par$observacion                   => par$ades_observaciones(idx),
                                                        par$patron                        => par$abol_defecto(idx),
                                                        par$campadic1                     => par$ades_campo_adicional_1(idx),
                                                        par$campadic2                     => par$ades_campo_adicional_2(idx),
                                                        par$campadic3                     => par$ades_campo_adicional_3(idx),
                                                        par$campadic4                     => par$ades_campo_adicional_4(idx),
                                                        par$campadic5                     => par$ades_campo_adicional_5(idx),
                                                        par$campadic6                     => par$ades_campo_adicional_6(idx),
                                                        par$campadic7                     => par$ades_campo_adicional_7(idx),
                                                        par$campadic8                     => par$ades_campo_adicional_8(idx),
                                                        par$bol_activo                    => var$bol_activo,
                                                        /* valores de salida */
                                                        par$cod_accion_out                => var$cod_accion_out, 
                                                        par$oid_cliente_out               => var$oid_cliente_out, 
                                                        par$oid_subcli_out                => var$oid_subcli_out, 
                                                        par$oid_pto_serv_out              => var$oid_pto_serv_out, 
                                                        par$oid_banco_out                 => var$oid_banco_out, 
                                                        par$tipo_cuenta_out               => var$tipo_cuenta_out, 
                                                        par$cod_agencia_out               => var$cod_agencia_out, 
                                                        par$nro_cuenta_out                => var$nro_cuenta_out, 
                                                        par$documento_out                 => var$documento_out, 
                                                        par$titularidad_out               => var$titularidad_out, 
                                                        par$oid_divisa_out                => var$oid_divisa_out, 
                                                        par$observacion_out               => var$observacion_out, 
                                                        par$patron_out                    => var$patron_out, 
                                                        par$campadic1_out                 => var$campadic1_out, 
                                                        par$campadic2_out                 => var$campadic2_out, 
                                                        par$campadic3_out                 => var$campadic3_out, 
                                                        par$campadic4_out                 => var$campadic4_out, 
                                                        par$campadic5_out                 => var$campadic5_out, 
                                                        par$campadic6_out                 => var$campadic6_out, 
                                                        par$campadic7_out                 => var$campadic7_out, 
                                                        par$campadic8_out                 => var$campadic8_out, 
                                                        par$activo_out                    => var$activo_out,
                                                        par$acambio_campo_out             => var$acambio_campo_out,
                                                        par$acambio_valor_out             => var$acambio_valor_out);
    
                            DBMS_OUTPUT.PUT_LINE('Luego de invocar sparams_dats_bans');
            /*3) Utilizamos los parametros de salida del procedure svalidar_params_dats_bans para realizar inserts o updates en la tabla SAPR_TDATO_BANCARIO*/
                            DBMS_OUTPUT.PUT_LINE('var$cod_agencia_out: ' || var$cod_agencia_out);
                            DBMS_OUTPUT.PUT_LINE('supd_dato_bancario');
                            
                            supd_dato_bancario(par$oid_dato_bancario    => var$oid_dato_bancario,
                                        par$oid_banco                   => var$oid_banco_out,
                                        par$oid_cliente                 => var$oid_cliente_out,
                                        par$oid_subcliente              => var$oid_subcli_out,
                                        par$oid_pto_servicio            => var$oid_pto_serv_out,
                                        par$oid_divisa                  => var$oid_divisa_out,
                                        par$cod_tipo_cuenta_bancaria    => var$tipo_cuenta_out,
                                        par$cod_cuenta_bancaria         => var$nro_cuenta_out,
                                        par$cod_documento               => var$documento_out, 
                                        par$des_titularidad             => var$titularidad_out, 
                                        par$des_observaciones           => var$observacion_out,
                                        par$bol_defecto                 => var$patron_out,
                                        par$bol_activo                  => var$activo_out,
                                        par$cod_agencia                 => var$cod_agencia_out,  
                                        par$des_campo_adicional_1       => var$campadic1_out,
                                        par$des_campo_adicional_2       => var$campadic2_out,
                                        par$des_campo_adicional_3       => var$campadic3_out,
                                        par$des_campo_adicional_4       => var$campadic4_out,
                                        par$des_campo_adicional_5       => var$campadic5_out,
                                        par$des_campo_adicional_6       => var$campadic6_out,
                                        par$des_campo_adicional_7       => var$campadic7_out,
                                        par$des_campo_adicional_8       => var$campadic8_out,
                                        par$cod_usuario                 => par$cod_usuario);
    
                            dbms_output.put_line('Recorrer salida de update_dato_bancarios_cambio');
    
                            /* Recorro el array para insertar registros en la tabla sapr_tdatos_bancarios_cambios */
                            IF  var$acambio_campo_out IS NOT NULL AND var$acambio_campo_out.COUNT >0 THEN
                                FOR indice in var$acambio_campo_out.first .. var$acambio_campo_out.last  LOOP
                                  var$oid_dato_banc_cambio := NULL;
                                  supd_dato_bancario_cambio(var$oid_dato_banc_cambio, 
                                                            var$oid_dato_bancario,
                                                            var$acambio_campo_out(indice),
                                                            var$acambio_valor_out(indice),
                                                            par$ades_comentario(idx),
                                                            var$oid_usuario_out,
                                                            0, 
                                                            'PD', 
                                                            1);
                                END LOOP;                            
                            END IF;
    
    
                     EXCEPTION
                     WHEN OTHERS THEN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3,  COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                                    VALUES (
                                        '2040080000', 
                                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080000', 
                                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente, 
                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                            NULL, 
                                            0),                                 
                                        par$acod_entidad(idx),                                
                                        par$aoid_cliente(idx),
                                        'VALIDACIONES_DATO_BANCO',
                                        par$anel_index(idx));
                    END;
                END LOOP;
        
          END IF;
        END IF;
        
        
        IF NOT fhay_error THEN
            DBMS_OUTPUT.PUT_LINE('Mensaje de Exito en DatoBancario para identificador' || var$oid_dato_bancario);
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO4, COD_CALIFICADOR)
                            VALUES (
                               '0040080000', 
                               gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040080000', 
                                      gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente, 
                                      gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                      NULL, 
                                      0), 
                                var$oid_dato_bancario,
                                'VALIDACIONES_DATO_BANCO');
        END IF;
    END;                            
    /*4) Poblar cursor de salida (validaciones)*/
    OPEN par$rc_validaciones FOR
    SELECT OID_CAMPO1 AS CODIGO,  
          COD_CAMPO2 AS DESCRIPCION,
          cod_campo3 AS ENTIDAD,
          cod_campo4 AS CODIGO_ENTIDAD,
          nel_campo5 AS IDX,
          COD_CALIFICADOR AS CALIFICADOR 
          FROM SAPR_GTT_TAUXILIAR AUX
    WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO';

 END sconfigurar_dato_bancario;

/* Utilizado por el procedure sconfigurar_dato_bancario  para validar los parametros de entrada*/
 PROCEDURE svalidar_dato_bancario(
                        par$oid_llamada                 IN gepr_pcomon_###VERSION###.tipo$oid_ := NULL,
                        par$acod_accion                 IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$anel_index                  IN gepr_pcomon_###VERSION###.tipo$nels_,
                        par$aoid_dato_bancario          IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$acod_entidad                IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$aoid_cliente                IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_subcliente             IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_pto_servicio           IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$acod_banco                  IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_agencia                IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_divisa                 IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_cuenta_bancaria        IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$acod_tipo_cuenta_bancaria   IN gepr_pcomon_###VERSION###.tipo$cods_,
                        par$ades_titularidad            IN gepr_pcomon_###VERSION###.tipo$descs_,
                        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$oid_usuario  OUT gepr_pcomon_###VERSION###.tipo$oid_ ) IS

    var$des_origen        CONSTANT  gepr_pcomon_###VERSION###.tipo$desc_ := const$codFuncionalidad || '.' || 'svalidar_dato_bancario';
    var$entidad                     gepr_pcomon_###VERSION###.tipo$desc_:= 'DATOBANCARIO';
    var$mensaje                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$mensajeEntidadTraducida     gepr_pcomon_###VERSION###.tipo$desc_;
    var$mensajeEntidadValidada      gepr_pcomon_###VERSION###.tipo$desc_;
    var$mensajeCampo                gepr_pcomon_###VERSION###.tipo$desc_;
    var$existe                      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$oidDivisaExistente          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oidBancoExistente           gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_usuario                 gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_accion                  gepr_pcomon_###VERSION###.tipo$cod_;
    var$oidDatoBancario             gepr_pcomon_###VERSION###.tipo$oid_;

 BEGIN

     /*Traducir palabra DATOBANCARIO*/
    var$mensajeEntidadTraducida := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,var$entidad,
                                   gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                   gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                   NULL,
                                   0);

    IF par$anel_index IS NOT NULL AND par$anel_index.COUNT>0 THEN
        FOR idx IN par$anel_index.first .. par$anel_index.last
        LOOP
            IF par$anel_index(idx) IS NOT NULL THEN 
                var$cod_accion := par$acod_accion(idx);

                /*CodigoBanco*/
                IF par$acod_banco(idx) IS NULL OR par$acod_banco(idx) = const$vacio THEN
                    /*Traducir palabra Codigo del banco*/
                    var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'CodigoBanco',
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

                    DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadTraducida);
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                    VALUES (
                        '2040080004',
                        var$mensaje,
                        var$entidad,
                        par$acod_banco(idx),
                        'VALIDACIONES_DATO_BANCO',
                        par$anel_index(idx));
                ELSE 
                    /*En caso de informar el código de banco validamos que exista*/
                    /*Si existe el codigo de banco, guardamos el OID_BANCO (OID_CLIENTE con TIPO_CLIENTE = 1) */
                    BEGIN
                        SELECT C.OID_CLIENTE
                        INTO var$oidBancoExistente
                        FROM GEPR_TCLIENTE C
                        INNER JOIN GEPR_TTIPO_CLIENTE TC ON C.OID_TIPO_CLIENTE = TC.OID_TIPO_CLIENTE
                        WHERE COD_CLIENTE = par$acod_banco(idx) AND TC.COD_TIPO_CLIENTE = 1 AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                        var$oidBancoExistente := NULL;
                    END;

                    /*OID_BANCO*/
                    IF var$oidBancoExistente IS NOT NULL THEN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                        VALUES (
                            var$oidBancoExistente,
                            par$anel_index(idx),
                            'OID_BANCO');
                    ELSE
                        /*Traducir palabra BANCO*/
                        var$mensajeEntidadValidada := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'BANCO',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                NULL,
                                0);

                        /*Buscamos el mensaje traducido*/
                        /*2040080005 - El {0} de codigo "{1}" no existe.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        var$mensajeEntidadValidada || '|' || par$acod_banco(idx),
                        0);
                        DBMS_OUTPUT.PUT_LINE('Error 2040080005 - El ' ||  var$mensajeEntidadValidada ||' de codigo "'|| par$acod_banco(idx) || '" no existe.');
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES (
                            '2040080005',
                            var$mensaje,
                            var$entidad,
                            par$acod_banco(idx),
                            'VALIDACIONES_DATO_BANCO',
                            par$anel_index(idx));
                    END IF;
                END IF;

                /*CodigoAgencia*/
                IF par$acod_agencia(idx) IS NULL THEN
                    /*Traducir palabra Codigo de agencia*/
                    var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'CodigoAgencia',
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

                    DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadTraducida);
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                    VALUES (
                        '2040080004',
                        var$mensaje,
                        var$entidad,
                        par$acod_agencia(idx),
                        'VALIDACIONES_DATO_BANCO',
                        par$anel_index(idx));
                END IF;
                
                /*NumeroCuenta*/
                IF par$acod_cuenta_bancaria(idx) IS NULL OR par$acod_cuenta_bancaria(idx) = const$vacio THEN
                        /*Traducir palabra Numero de cuenta*/
                        var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'NumeroCuenta',
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
                        DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadTraducida);
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES (
                            '2040080004',
                            var$mensaje,
                            var$entidad,
                            par$acod_cuenta_bancaria(idx),
                            'VALIDACIONES_DATO_BANCO',
                            par$anel_index(idx));
                END IF;

                /*Divisa*/
                IF par$acod_divisa(idx) IS NULL  OR par$acod_divisa(idx) = const$vacio  THEN
                    /*Traducir palabra Divisa*/
                    var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'DIVISA',
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

                    DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadTraducida);
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                    VALUES (
                        '2040080004',
                        var$mensaje,
                        var$entidad,
                        par$acod_divisa(idx),
                        'VALIDACIONES_DATO_BANCO',
                        par$anel_index(idx));
                ELSE 
                    /*En caso de informar el código de divisa validamos que exista*/
                    /*Si existe el codigo de divisa, guardamos el OID_DIVISA*/
                    BEGIN
                        SELECT OID_DIVISA
                        INTO var$oidDivisaExistente
                        FROM GEPR_TDIVISA
                        WHERE COD_ISO_DIVISA = par$acod_divisa(idx) AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                        var$oidDivisaExistente := NULL;
                    END;

                    /*OID_DIVISA*/
                    IF var$oidDivisaExistente IS NOT NULL THEN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                        VALUES (
                            var$oidDivisaExistente,
                            par$anel_index(idx),
                            'OID_DIVISA');
                    ELSE
                        /*Traducir palabra DIVISA*/
                        var$mensajeEntidadValidada := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'DIVISA',
                                gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                NULL,
                                0);

                        /*Buscamos el mensaje traducido*/
                        /*2040080005 - El {0} de codigo "{1}" no existe.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        var$mensajeEntidadValidada || '|' || par$acod_divisa(idx),
                        0);
                        DBMS_OUTPUT.PUT_LINE('Error 2040080005 - El ' ||  var$mensajeEntidadValidada ||' de codigo "'|| par$acod_divisa(idx) || '" no existe.');
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES (
                            '2040080005',
                            var$mensaje,
                            var$entidad,
                            par$acod_divisa(idx),
                            'VALIDACIONES_DATO_BANCO',
                            par$anel_index(idx));
                    END IF;
                END IF;

                -- /* Identificamos el tipo de acción */
                IF par$aoid_dato_bancario(idx) IS NULL THEN
                    SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => var$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', no se especifica oid_dato_bancario',
                    par$cod_identificador   => '');

                    BEGIN
                        CASE par$acod_entidad(idx)
                            WHEN 'CLIENTE'  THEN 
                                SELECT OID_DATO_BANCARIO
                                INTO var$oidDatoBancario
                                FROM SAPR_TDATO_BANCARIO
                                WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                AND OID_SUBCLIENTE IS NULL 
                                AND OID_PTO_SERVICIO IS NULL
                                AND OID_BANCO = var$oidBancoExistente 
                                AND (
                                        (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                        OR 
                                        (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                    )
                                AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                AND OID_DIVISA = var$oidDivisaExistente
                                AND BOL_ACTIVO = 1
                                AND ROWNUM = 1;
                            WHEN 'SUBCLIENTE'  THEN
                                SELECT OID_DATO_BANCARIO
                                INTO var$oidDatoBancario
                                FROM SAPR_TDATO_BANCARIO
                                WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                AND OID_SUBCLIENTE = par$aoid_subcliente(idx)
                                AND OID_PTO_SERVICIO IS NULL
                                AND OID_BANCO = var$oidBancoExistente 
                                AND (
                                        (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                        OR 
                                        (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                    )
                                AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                AND OID_DIVISA = var$oidDivisaExistente
                                AND BOL_ACTIVO = 1
                                AND ROWNUM = 1;
                            WHEN 'PUNTOSERVICIO'  THEN 
                                SELECT OID_DATO_BANCARIO
                                INTO var$oidDatoBancario
                                FROM SAPR_TDATO_BANCARIO
                                WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                AND OID_SUBCLIENTE = par$aoid_subcliente(idx)
                                AND OID_PTO_SERVICIO = par$aoid_pto_servicio(idx)
                                AND OID_BANCO = var$oidBancoExistente 
                                AND (
                                        (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                        OR 
                                        (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                    )
                                AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                AND OID_DIVISA = var$oidDivisaExistente
                                AND BOL_ACTIVO = 1
                                AND ROWNUM = 1;
                        END CASE;

                        SAPR_PLOG_API.SAGREGA_DETALLE(
                          par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => var$des_origen,
                          par$des_version     => const$version, 
                          par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', se encuentra oid_dato_bancario var$oidDatoBancario con el Codigo Banco, Codigo Agencia, Numero Cuenta, Codigo Divisa informado',
                          par$cod_identificador   => '');
                    EXCEPTION
                    WHEN others THEN
                        var$oidDatoBancario := NULL;
                    END;
                ELSE
                   SAPR_PLOG_API.SAGREGA_DETALLE(
                    par$oid_llamada  => par$oid_llamada,
                    par$des_origen      => var$des_origen,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', se especifica oid_dato_bancario: ' || par$aoid_dato_bancario(idx),
                    par$cod_identificador   => '');

                    begin
                    select OID_DATO_BANCARIO
                        into var$oidDatoBancario
                        from SAPR_TDATO_BANCARIO
                    where OID_DATO_BANCARIO = par$aoid_dato_bancario(idx);
                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                          SAPR_PLOG_API.SAGREGA_DETALLE(
                            par$oid_llamada  => par$oid_llamada,
                            par$des_origen      => var$des_origen,
                            par$des_version     => const$version,
                            par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', no se encuentra oid_dato_bancario para el par$aoid_dato_bancario especificado: ' || par$aoid_dato_bancario(idx),
                            par$cod_identificador   => '');
                          BEGIN
                            CASE par$acod_entidad(idx)
                                WHEN 'CLIENTE'  THEN 
                                    SELECT OID_DATO_BANCARIO
                                    INTO var$oidDatoBancario
                                    FROM SAPR_TDATO_BANCARIO
                                    WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                    AND OID_SUBCLIENTE IS NULL 
                                    AND OID_PTO_SERVICIO IS NULL
                                    AND OID_BANCO = var$oidBancoExistente 
                                    AND (
                                            (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                            OR 
                                            (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                        )
                                    AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                    AND OID_DIVISA = var$oidDivisaExistente
                                    AND BOL_ACTIVO = 1
                                    AND ROWNUM = 1;
                                WHEN 'SUBCLIENTE'  THEN
                                    SELECT OID_DATO_BANCARIO
                                    INTO var$oidDatoBancario
                                    FROM SAPR_TDATO_BANCARIO
                                    WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                    AND OID_SUBCLIENTE = par$aoid_subcliente(idx)
                                    AND OID_PTO_SERVICIO IS NULL
                                    AND OID_BANCO = var$oidBancoExistente 
                                    AND (
                                            (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                            OR 
                                            (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                        )
                                    AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                    AND OID_DIVISA = var$oidDivisaExistente
                                    AND BOL_ACTIVO = 1
                                    AND ROWNUM = 1;
                                WHEN 'PUNTOSERVICIO'  THEN 
                                    SELECT OID_DATO_BANCARIO
                                    INTO var$oidDatoBancario
                                    FROM SAPR_TDATO_BANCARIO
                                    WHERE OID_CLIENTE = par$aoid_cliente(idx) 
                                    AND OID_SUBCLIENTE = par$aoid_subcliente(idx)
                                    AND OID_PTO_SERVICIO = par$aoid_pto_servicio(idx)
                                    AND OID_BANCO = var$oidBancoExistente 
                                    AND (
                                            (par$acod_agencia(idx) <> const$vacio AND COD_AGENCIA = par$acod_agencia(idx))
                                            OR 
                                            (par$acod_agencia(idx) = const$vacio AND COD_AGENCIA IS NULL)
                                        )
                                    AND COD_CUENTA_BANCARIA = par$acod_cuenta_bancaria(idx)
                                    AND OID_DIVISA = var$oidDivisaExistente
                                    AND BOL_ACTIVO = 1
                                    AND ROWNUM = 1;
                              END CASE;
                              SAPR_PLOG_API.SAGREGA_DETALLE(
                                par$oid_llamada  => par$oid_llamada,
                                par$des_origen      => var$des_origen,
                                par$des_version     => const$version, 
                                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' de índice ' || par$anel_index(idx) || ', se encuentra oid_dato_bancario var$oidDatoBancario con el Codigo Banco, Codigo Agencia, Numero Cuenta, Codigo Divisa informado',
                                par$cod_identificador   => '');
                            EXCEPTION
                              WHEN OTHERS THEN
                                var$oidDatoBancario := NULL;
                            END;
                        WHEN OTHERS THEN
                        var$oidDatoBancario := NULL;
                    END;
                END IF;

                IF (var$cod_accion IS NULL OR var$cod_accion = 'ALTA') AND var$oidDatoBancario IS NOT NULL THEN
                    var$cod_accion := 'MODIFICAR';
                END IF;
                DBMS_OUTPUT.PUT_LINE('var$cod_accion en svalidar_dato_bancario: ' || var$cod_accion);
                DBMS_OUTPUT.PUT_LINE('par$aoid_cliente(idx) en svalidar_dato_bancario: ' || par$aoid_cliente(idx));
                DBMS_OUTPUT.PUT_LINE('var$oidDatoBancario en svalidar_dato_bancario: ' || var$oidDatoBancario);
                DBMS_OUTPUT.PUT_LINE('var$oidBancoExistente en svalidar_dato_bancario: ' || var$oidBancoExistente);
                DBMS_OUTPUT.PUT_LINE('var$oidDivisaExistente en svalidar_dato_bancario: ' || var$oidDivisaExistente);

                SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => var$des_origen,
                par$des_version     => const$version,
                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' var$cod_accion en svalidar_dato_bancario: ' || var$cod_accion,
                par$cod_identificador   => '');
                SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => var$des_origen,
                par$des_version     => const$version,
                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' par$aoid_cliente(idx) en svalidar_dato_bancario: ' || par$aoid_cliente(idx),
                par$cod_identificador   => '');

                SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => var$des_origen,
                par$des_version     => const$version,
                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' var$oidDatoBancario en svalidar_dato_bancario: ' || var$oidDatoBancario,
                par$cod_identificador   => '');

                SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => var$des_origen,
                par$des_version     => const$version,
                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' var$oidBancoExistente en svalidar_dato_bancario: ' || var$oidBancoExistente,
                par$cod_identificador   => '');

                SAPR_PLOG_API.SAGREGA_DETALLE(
                par$oid_llamada  => par$oid_llamada,
                par$des_origen      => var$des_origen,
                par$des_version     => const$version,
                par$des_detalle     => 'Entidad: '|| par$acod_entidad(idx) || ' var$oidDivisaExistente en svalidar_dato_bancario: ' || var$oidDivisaExistente,
                par$cod_identificador   => '');



                /*Validamos en caso de ALTA que se encuentre el Tipo, Titularidad */
                IF var$cod_accion IS NULL OR var$cod_accion = 'ALTA'  THEN
                    /*Tipo*/
                    IF par$acod_tipo_cuenta_bancaria(idx) IS NULL OR par$acod_tipo_cuenta_bancaria(idx) = const$vacio THEN
                            /*Traducir palabra Tipo de cuenta*/
                            var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'TipoCuenta',
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
                            DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadTraducida);
                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                            VALUES (
                            '2040080004',
                                var$mensaje,
                                var$entidad,
                                par$acod_tipo_cuenta_bancaria(idx),
                                'VALIDACIONES_DATO_BANCO',
                                par$anel_index(idx));
                    END IF;
                    /*Titularidad*/
                    IF par$ades_titularidad(idx) IS NULL OR par$ades_titularidad(idx) = const$vacio THEN
                            /*Traducir palabra Titularidad*/
                            var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Titularidad',
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
                            DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadTraducida);
                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                            VALUES (
                            '2040080004',
                                var$mensaje,
                                var$entidad,
                                par$ades_titularidad(idx),
                                'VALIDACIONES_DATO_BANCO',
                                par$anel_index(idx));
                    END IF;

                    /*En caso de alta siempre debemos recibir el oid_Cliente*/
                    IF par$aoid_cliente(idx) IS NULL OR par$aoid_cliente(idx) = const$vacio THEN
                            /*Traducir palabra Identificador*/
                            var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Identificador',
                                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                            NULL,
                                            0);
                            var$mensajeEntidadValidada := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'CLIENTE',
                                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                            NULL,
                                            0);
                            /*Buscamos el mensaje traducido*/
                            /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                            var$mensajeCampo  || '|' || var$mensajeEntidadValidada,
                            0);
                            DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadValidada);
                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                            VALUES (
                                '2040080004',
                                var$mensaje,
                                var$entidad,
                                par$aoid_cliente(idx),
                                'VALIDACIONES_DATO_BANCO',
                                par$anel_index(idx));
                    END IF;

                    /*En caso de entidad SUBCLIENTE validar oid_subcliente*/
                    IF par$acod_entidad(idx) = 'SUBCLIENTE' AND (par$aoid_subcliente(idx) IS NULL OR par$aoid_subcliente(idx) = const$vacio) THEN
                        /*Traducir palabra Identificador*/
                        var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Identificador',
                                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                        NULL,
                                        0);
                        var$mensajeEntidadValidada := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'SUBCLIENTE',
                                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                        NULL,
                                        0);
                        /*Buscamos el mensaje traducido*/
                        /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        var$mensajeCampo  || '|' || var$mensajeEntidadValidada,
                        0);
                        DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadValidada);
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES (
                            '2040080004',
                            var$mensaje,
                            var$entidad,
                            par$aoid_subcliente(idx),
                            'VALIDACIONES_DATO_BANCO',
                            par$anel_index(idx));
                    ELSIF par$acod_entidad(idx) = 'PUNTOSERVICIO' THEN
                        /*En caso de entidad PUNTOSERVICIO validar oid_subcliente y oid_pto_servicio*/
                        /*oid_subcliente*/
                        IF par$aoid_subcliente(idx) IS NULL  OR par$aoid_subcliente(idx) = const$vacio THEN
                        /*Traducir palabra Identificador*/
                        var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Identificador',
                                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                        NULL,
                                        0);
                        var$mensajeEntidadValidada := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'SUBCLIENTE',
                                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                        NULL,
                                        0);
                        /*Buscamos el mensaje traducido*/
                        /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                            var$mensajeCampo  || '|' || var$mensajeEntidadValidada,
                            0);
                        DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadValidada);
                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                            VALUES (
                            '2040080004',
                                var$mensaje,
                                var$entidad,
                                par$aoid_subcliente(idx),
                                'VALIDACIONES_DATO_BANCO',
                                par$anel_index(idx));
                        END IF;
                        /*oid_pto_servicio*/
                        IF par$aoid_pto_servicio(idx) IS NULL  OR par$aoid_pto_servicio(idx) = const$vacio THEN
                        /*Traducir palabra Identificador*/
                        var$mensajeCampo := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'Identificador',
                                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                        NULL,
                                        0);
                        var$mensajeEntidadValidada := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'PUNTOSERVICIO',
                                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                        NULL,
                                        0);
                        /*Buscamos el mensaje traducido*/
                        /*2040080004 - Es obligatorio informar el {0} del {1}.*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080004',
                            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                            var$mensajeCampo  || '|' || var$mensajeEntidadValidada,
                            0);
                        DBMS_OUTPUT.PUT_LINE('Error 2040080004 - Es obligatorio informar el ' ||  var$mensajeCampo ||'del '|| var$mensajeEntidadValidada);
                            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                            VALUES (
                            '2040080004',
                                var$mensaje,
                                var$entidad,
                                par$aoid_pto_servicio(idx),
                                'VALIDACIONES_DATO_BANCO',
                                par$anel_index(idx));
                        END IF;
                    END IF;
                ELSE
                    /*Validamos en caso de MODIFICACION O BAJA  que se encuentre el Identificador*/
                    /*par$aoid_dato_bancario*/
                    BEGIN
                        SELECT COUNT(1)
                        INTO var$existe
                        FROM SAPR_TDATO_BANCARIO
                        WHERE OID_DATO_BANCARIO = var$oidDatoBancario;
                    EXCEPTION WHEN no_data_found THEN
                        var$existe := 0;
                    END;

                    IF var$existe = 0 THEN
                        /*Buscamos el mensaje traducido*/
                        /*No fue posible encontrar el Dato bancario con el Codigo Banco, Codigo Agencia, Numero Cuenta, Codigo Divisa informado*/
                        var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080012',
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                        '',
                        0);
                        DBMS_OUTPUT.PUT_LINE('Error 2040080012 - No fue posible encontrar el Dato bancario con el Codigo Banco, Codigo Agencia, Numero Cuenta, Codigo Divisa informado');
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR, NEL_CAMPO5)
                        VALUES (
                            '2040080012',
                            var$mensaje,
                            var$entidad,
                            var$oidDatoBancario,
                            'VALIDACIONES_DATO_BANCO',
                            par$anel_index(idx));
                    END IF;
                END IF;
            END IF;
        END LOOP;
    END IF;




     BEGIN

        SELECT 
            OID_USUARIO
        INTO
            var$oid_usuario
        FROM ADPR_TUSUARIO
        WHERE
            upper(trim(DES_LOGIN)) = upper(trim(par$cod_usuario));

    EXCEPTION
        WHEN no_data_found THEN
            var$oid_usuario := NULL;
            /*Traducir palabra USUARIO*/
            var$mensajeEntidadValidada := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'USUARIO',
                       gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                       gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                       NULL,
                       0);

            /*Buscamos el mensaje traducido*/
            /*2040080005 - El {0} de codigo "{1}" no existe.*/
            var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
            gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
            gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
            var$mensajeEntidadValidada || '|' || par$cod_usuario,
            0);
    END;
    par$oid_usuario := var$oid_usuario;

 END svalidar_dato_bancario;

 /* 
    Este procedure: 
    -  Se encarga de validar si los parametros de entrada requieren aprobación o no. Si requiere aprobación guarda en la tabla SAPR_TDATO_BANCARIO_CAMBIO 
    invocando al SP supd_dato_bancario_cambio 
    - Devuelve la salida para persistir los datos en SAPR_TDATO_BANCARIO (llamado por el SP supd_dato_bancario)
*/
   PROCEDURE svalidar_params_dats_bans(
    par$cod_cultura                    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_usuario                    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_pais                       IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_dato_bancario              IN gepr_pcomon_###VERSION###.tipo$oid_,

    par$cod_accion                    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_cliente                   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_subcliente                IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_pto_servicio              IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$tipo_cuenta                   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_banco                     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_agencia                   IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$nro_cuenta                    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$documento                     IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$titularidad                   IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$cod_divisa                    IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$observacion                   IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$patron                        IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$campadic1                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic2                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic3                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic4                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic5                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic6                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic7                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic8                     IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$bol_activo                    IN gepr_pcomon_###VERSION###.tipo$nel_,
    /* Salida */
    par$cod_accion_out                OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$oid_cliente_out               OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_subcli_out                OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_pto_serv_out              OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$oid_banco_out                 OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$tipo_cuenta_out               OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$cod_agencia_out               OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$nro_cuenta_out                OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$documento_out                 OUT gepr_pcomon_###VERSION###.tipo$cod_,
    par$titularidad_out               OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$oid_divisa_out                OUT gepr_pcomon_###VERSION###.tipo$oid_,
    par$observacion_out               OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$patron_out                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
    par$campadic1_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic2_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic3_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic4_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic5_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic6_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic7_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$campadic8_out                 OUT gepr_pcomon_###VERSION###.tipo$desc_,
    par$activo_out                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
    par$acambio_campo_out             OUT gepr_pcomon_###VERSION###.tipo$cods_,
    par$acambio_valor_out             OUT gepr_pcomon_###VERSION###.tipo$descs_)
IS
    const$accionAlta CONSTANT          gepr_pcomon_###VERSION###.tipo$desc_ := 'ALTA';
    const$accionModificar CONSTANT     gepr_pcomon_###VERSION###.tipo$desc_ := 'MODIFICACION';
    const$accionBaja CONSTANT          gepr_pcomon_###VERSION###.tipo$desc_ := 'BAJA';
    var$identificador                  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa                     gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_banco                      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_banco_old                  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cliente_old                gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_subcliente_old             gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_pto_serv_old               gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_agencia_old                gepr_pcomon_###VERSION###.tipo$cod_;
    var$documento_old                  gepr_pcomon_###VERSION###.tipo$cod_;
    var$nro_cuenta_old                 gepr_pcomon_###VERSION###.tipo$cod_;
    var$tipo_cuenta_old                gepr_pcomon_###VERSION###.tipo$cod_;
    var$titularidad_old                gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_divisa_old                 gepr_pcomon_###VERSION###.tipo$oid_;
    var$observa_old                    gepr_pcomon_###VERSION###.tipo$cod_;
    var$patron_old                     gepr_pcomon_###VERSION###.tipo$nel_;
    var$campadic1_old                  gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic2_old                  gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic3_old                  gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic4_old                  gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic5_old                  gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic6_old                  gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic7_old                  gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic8_old                  gepr_pcomon_###VERSION###.tipo$desc_;
    var$activo_old                     gepr_pcomon_###VERSION###.tipo$nel_;

    var$cod_accion                    gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_cliente                   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_subcliente                gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_pto_servicio              gepr_pcomon_###VERSION###.tipo$oid_;
    var$tipo_cuenta                   gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_agencia                   gepr_pcomon_###VERSION###.tipo$cod_;
    var$nro_cuenta                    gepr_pcomon_###VERSION###.tipo$cod_;
    var$documento                     gepr_pcomon_###VERSION###.tipo$cod_;
    var$titularidad                   gepr_pcomon_###VERSION###.tipo$desc_;
    var$observacion                   gepr_pcomon_###VERSION###.tipo$cod_;
    var$patron                        gepr_pcomon_###VERSION###.tipo$nel_;
    var$campadic1                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic2                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic3                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic4                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic5                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic6                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic7                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$campadic8                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$activo                        gepr_pcomon_###VERSION###.tipo$nel_;
    var$necesita_aprobacion            gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$cantidad_aprobaciones          gepr_pcomon_###VERSION###.tipo$desc_;
    var$campos_aprobacion              gepr_pcomon_###VERSION###.tipo$desc_;
    var$aprobacion_requerida_alta      gepr_pcomon_###VERSION###.tipo$desc_;

BEGIN

    /* Limpio tabla auxiliar */
    DELETE FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'CAMPO_REQ_APROB';
    DBMS_OUTPUT.PUT_LINE('Busquedad de parametros');
    /*Buscar parametros CantidadAprobacionesCuentasBancarias y CamposAprobacionRequeridaCuentasBancarias*/
    /*Para poder indicar si debe estar activo o no el dato bancario*/        
    var$cantidad_aprobaciones := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                par$cod_pais       => par$cod_pais,
                                par$cod_parametro  => 'CantidadAprobacionesCuentasBancarias',
                                par$cod_aplicacion => 'Genesis');

    var$campos_aprobacion := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                par$cod_pais       => par$cod_pais,
                                par$cod_parametro  => 'CamposAprobacionRequeridaCuentasBancarias',
                                par$cod_aplicacion => 'Genesis');

    var$aprobacion_requerida_alta := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                par$cod_pais       => par$cod_pais,
                                par$cod_parametro  => 'AprobacionDatosBancariosAlta',
                                par$cod_aplicacion => 'Genesis');

    DBMS_OUTPUT.PUT_LINE('var$cantidad_aprobaciones: ' || var$cantidad_aprobaciones);
    DBMS_OUTPUT.PUT_LINE('var$campos_aprobacion: ' || var$campos_aprobacion);
    DBMS_OUTPUT.PUT_LINE('var$aprobacion_requerida_alta: ' || var$aprobacion_requerida_alta);
    /* Busco la divisa informada */
    IF par$cod_divisa IS NOT NULL AND par$cod_divisa <> const$vacio THEN
      BEGIN
        SELECT OID_DIVISA
        INTO var$oid_divisa
        FROM GEPR_TDIVISA
        WHERE COD_ISO_DIVISA = par$cod_divisa AND ROWNUM = 1;
      EXCEPTION WHEN no_data_found THEN
        var$oid_divisa := NULL;
      END;
    ELSE
      var$oid_divisa := NULL;
    END IF;


    /* Busco el banco informado */
    IF par$cod_banco IS NOT NULL AND par$cod_banco <> const$vacio  THEN
      /*Si existe el codigo de banco, guardamos el OID_BANCO (OID_CLIENTE con TIPO_CLIENTE = 1*/
      BEGIN
          SELECT C.OID_CLIENTE
          INTO var$oid_banco
          FROM GEPR_TCLIENTE C
          INNER JOIN GEPR_TTIPO_CLIENTE TC ON C.OID_TIPO_CLIENTE = TC.OID_TIPO_CLIENTE 
          WHERE COD_CLIENTE = par$cod_banco AND TC.COD_TIPO_CLIENTE = 1 AND ROWNUM = 1;
      EXCEPTION WHEN no_data_found THEN
          var$oid_banco := NULL;
      END;
    ELSE
      var$oid_banco := NULL;
    END IF;



    /*Validamos si debe pasar por el proceso de aprobación en caso de ALTA, en caso de MODIFICACION y BAJA siempre ingresa*/
    /*En caso de no estar configurado el parametro o tener valor verdadero debe pasar por aprobación*/
    DBMS_OUTPUT.PUT_LINE('par$cod_accion: ' || par$cod_accion);
    DBMS_OUTPUT.PUT_LINE('const$accionAlta: ' || const$accionAlta);
    IF (var$aprobacion_requerida_alta IS NOT NULL AND var$aprobacion_requerida_alta = 1 AND par$cod_accion = const$accionAlta) OR par$cod_accion <> const$accionAlta THEN

        dbms_output.put_line('svalidar_params_dats_bans - PARAMETRO var$cantidad_aprobaciones = ' || var$cantidad_aprobaciones);
        /* Para el caso que la cantidad de aprobaciones sea 0, es decir, que no pasa por el circuito de aprobación */ 
        IF var$cantidad_aprobaciones IS NULL or var$cantidad_aprobaciones = 0 THEN

            dbms_output.put_line('svalidar_params_dats_bans G');
            var$cod_accion := par$cod_accion;

            /* OID_BANCO */
            IF par$cod_banco IS NOT NULL AND par$cod_banco <> const$vacio THEN
                var$oid_banco := var$oid_banco;
            ELSE
                var$oid_banco := NULL;
            END IF;


            /* OID_CLIENTE */
            IF par$oid_cliente IS NOT NULL AND par$oid_cliente <> const$vacio THEN
                var$oid_cliente := par$oid_cliente;
            ELSE
                var$oid_cliente := NULL;
            END IF;

            /* OID_SUBCLIENTE */
            IF par$oid_subcliente IS NOT NULL AND par$oid_subcliente <> const$vacio THEN
                var$oid_subcliente := par$oid_subcliente;
            ELSE
                var$oid_subcliente := NULL;
            END IF;

            /* OID_PTO_SERVICIO */
            IF par$oid_pto_servicio IS NOT NULL AND par$oid_pto_servicio <> const$vacio THEN
                var$oid_pto_servicio := par$oid_pto_servicio;
            ELSE
                var$oid_pto_servicio := NULL;
            END IF;

            /* COD_TIPO_CUENTA_BANCARIA */
            IF par$tipo_cuenta IS NOT NULL AND par$tipo_cuenta <> const$vacio THEN
                var$tipo_cuenta := par$tipo_cuenta;
            ELSE
                var$tipo_cuenta := NULL;
            END IF;

            /* COD_AGENCIA */
            IF var$cod_accion = const$accionAlta AND par$cod_agencia = const$vacio THEN
                var$cod_agencia := NULL;
            ELSE
                var$cod_agencia := par$cod_agencia;
            END IF;


            /* DES_OBSERVACIONES */
            IF var$cod_accion = const$accionAlta AND par$observacion = const$vacio THEN
                var$observacion := NULL;
            ELSE
                var$observacion := par$observacion;
            END IF;

            /* DES_TITULARIDAD */
            IF par$titularidad IS NOT NULL AND par$titularidad <> const$vacio THEN
                var$titularidad := par$titularidad;
            ELSE
                var$titularidad := NULL;
            END IF;

            /* COD_CUENTA_BANCARIA */
            IF par$nro_cuenta IS NOT NULL AND par$nro_cuenta <> const$vacio THEN
                var$nro_cuenta := par$nro_cuenta;
            ELSE
                var$nro_cuenta := NULL;
            END IF;

            /* COD_DOCUMENTO */
            IF var$cod_accion = const$accionAlta AND par$documento = const$vacio THEN
                var$documento := NULL;
            ELSE
                var$documento := par$documento;
            END IF;

            /* PATRON */
            var$patron := par$patron;

            /* DES_CAMPO_ADICIONAL_1 */
            IF var$cod_accion = const$accionAlta AND par$campadic1 = const$vacio THEN
                var$campadic1 := NULL;
            ELSE
                var$campadic1 := par$campadic1;
            END IF;

            /* DES_CAMPO_ADICIONAL_2 */
            IF var$cod_accion = const$accionAlta AND par$campadic2 = const$vacio THEN
                var$campadic2 := NULL;
            ELSE
                var$campadic2 := par$campadic2;
            END IF;

            /* DES_CAMPO_ADICIONAL_3 */
            IF var$cod_accion = const$accionAlta AND par$campadic3 = const$vacio THEN
                var$campadic3 := NULL;
            ELSE
                var$campadic3 := par$campadic3;
            END IF;

            /* DES_CAMPO_ADICIONAL_4 */
            IF var$cod_accion = const$accionAlta AND par$campadic4 = const$vacio THEN
                var$campadic4 := NULL;
            ELSE
                var$campadic4 := par$campadic4;
            END IF;

            /* DES_CAMPO_ADICIONAL_5 */
            IF var$cod_accion = const$accionAlta AND par$campadic5 = const$vacio THEN
                var$campadic5 := NULL;
            ELSE
                var$campadic5 := par$campadic5;
            END IF;

            /* DES_CAMPO_ADICIONAL_6 */
            IF var$cod_accion = const$accionAlta AND par$campadic6 = const$vacio THEN
                var$campadic6 := NULL;
            ELSE
                var$campadic6 := par$campadic6;
            END IF;

            /* DES_CAMPO_ADICIONAL_7 */
            IF var$cod_accion = const$accionAlta AND par$campadic7 = const$vacio THEN
                var$campadic7 := NULL;
            ELSE
                var$campadic7 := par$campadic7;
            END IF;

            /* DES_CAMPO_ADICIONAL_8 */
            IF var$cod_accion = const$accionAlta AND par$campadic8 = const$vacio THEN
                var$campadic8 := NULL;
            ELSE
                var$campadic8 := par$campadic8;
            END IF;

            /* BOL_ACTIVO */
            IF par$bol_activo IS NOT NULL THEN
                var$activo := par$bol_activo;
            ELSE
                var$activo := NULL;
            END IF;

            /* BOL_DEFECTO */
            IF par$patron IS NOT NULL  THEN
                var$patron := par$patron;
            ELSE
                var$patron := NULL;
            END IF;        

        ELSE

            dbms_output.put_line('svalidar_params_dats_bans H - var$campos_aprobacion = ' || var$campos_aprobacion);
            /* Significa que hay campos que requieren aprobación.*/

            IF var$campos_aprobacion IS NULL OR var$campos_aprobacion = '' THEN
                /* Inserto todos los campos menos OID_DATO_BANCARIO por PK y los campos de auditoria */
                dbms_output.put_line('var$campos_aprobacion IS NULL or var$campos_aprobacion');
                INSERT INTO SAPR_GTT_TAUXILIAR
                (OID_CAMPO1, COD_CALIFICADOR)
                select UPPER(TRIM(column_name)), 'CAMPO_REQ_APROB'
                from all_tab_columns
                where UPPER(table_name) = 'SAPR_TDATO_BANCARIO'
                and UPPER(column_name) not in ('OID_DATO_BANCARIO',
                'OID_CLIENTE',
                'OID_SUBCLIENTE',
                'OID_PTO_SERVICIO',
                'GMT_CREACION',
                'DES_USUARIO_CREACION',
                'GMT_MODIFICACION',
                'DES_USUARIO_MODIFICACION');
            ELSE
                /* Poblar la tabla auxiliar con los nombre de los campos*/
                dbms_output.put_line('NOT var$campos_aprobacion IS NULL or var$campos_aprobacion');
                FOR cValue IN (
                                SELECT
                                regexp_substr(var$campos_aprobacion,'[^;]+', 1, level) CAMPO 
                                FROM DUAL
                                CONNECT BY regexp_substr(var$campos_aprobacion, '[^;]+', 1, level) IS NOT NULL
                            )
                LOOP
                INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                VALUES (TRIM(cValue.CAMPO), 'CAMPO_REQ_APROB');
                END LOOP;
            END IF;

            dbms_output.put_line('svalidar_params_dats_bans I - var$cod_accion = ' || par$cod_accion);

            IF upper(par$cod_accion) <> const$accionAlta THEN
                /* Busco los valores anteriores */
                dbms_output.put_line('svalidar_params_dats_bans J');
                BEGIN
                SELECT
                    OID_CLIENTE, OID_SUBCLIENTE, OID_PTO_SERVICIO, COD_TIPO_CUENTA_BANCARIA, COD_DOCUMENTO,
                    OID_BANCO, COD_AGENCIA, COD_CUENTA_BANCARIA, DES_TITULARIDAD,
                    OID_DIVISA, DES_OBSERVACIONES, BOL_DEFECTO, DES_CAMPO_ADICIONAL_1, DES_CAMPO_ADICIONAL_2,
                    DES_CAMPO_ADICIONAL_3, DES_CAMPO_ADICIONAL_4, DES_CAMPO_ADICIONAL_5, DES_CAMPO_ADICIONAL_6, DES_CAMPO_ADICIONAL_7,
                    DES_CAMPO_ADICIONAL_8, BOL_ACTIVO
                INTO
                    var$oid_cliente_old, var$oid_subcliente_old, var$oid_pto_serv_old, var$tipo_cuenta_old, var$documento_old,
                    var$oid_banco_old,var$cod_agencia_old, var$nro_cuenta_old, var$titularidad_old,
                    var$oid_divisa_old, var$observa_old, var$patron_old, var$campadic1_old, var$campadic2_old,
                    var$campadic3_old, var$campadic4_old, var$campadic5_old, var$campadic6_old, var$campadic7_old,
                    var$campadic8_old, var$activo_old
                FROM
                    SAPR_TDATO_BANCARIO
                WHERE
                    OID_DATO_BANCARIO = par$oid_dato_bancario;

                EXCEPTION
                WHEN OTHERS then
                    dbms_output.put_line('svalidar_params_dats_bans K');
                    var$oid_banco_old             :=NULL;
                    var$cod_agencia_old           :=NULL;
                    var$nro_cuenta_old            :=NULL;
                    var$titularidad_old           :=NULL;
                    var$oid_divisa_old            :=NULL;
                    var$observa_old               :=NULL;
                    var$patron_old                :=NULL;
                    var$campadic1_old             :=NULL;
                    var$campadic2_old             :=NULL;
                    var$campadic3_old             :=NULL;
                    var$campadic4_old             :=NULL;
                    var$campadic5_old             :=NULL;
                    var$campadic6_old             :=NULL;
                    var$campadic7_old             :=NULL;
                    var$campadic8_old             :=NULL;
                    var$activo_old                :=NULL;
                END;
            END IF;


            var$cod_accion := par$cod_accion;

            /* OID_BANCO */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'OID_BANCO' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$cod_banco IS NOT NULL AND par$cod_banco <> const$vacio THEN
                    var$oid_banco := var$oid_banco;
                ELSE
                    var$oid_banco := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */     
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$cod_banco <>  const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_BANCO';
                        par$acambio_valor_out(par$acambio_valor_out.count) := var$oid_banco;
                    END IF;

                var$oid_banco := NULL;
                ELSE
                    IF NVL(var$oid_banco, const$vacio) <>  NVL(var$oid_banco_old,  const$vacio) AND par$cod_banco <>  const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_BANCO';
                        par$acambio_valor_out(par$acambio_valor_out.count) := var$oid_banco;
                    END IF;
                    var$oid_banco := var$oid_banco_old;
                END IF;
            END IF;

            dbms_output.put_line('svalidar_params_dats_bans COD_AGENCIA');
            /* COD_AGENCIA */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'COD_AGENCIA' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            dbms_output.put_line('svalidar_params_dats_bans COD_AGENCIA - var$necesita_aprobacion = ' || var$necesita_aprobacion);
            IF var$necesita_aprobacion=0 THEN
                IF par$cod_agencia IS NOT NULL AND par$cod_agencia <> const$vacio THEN
                    var$cod_agencia := par$cod_agencia;
                ELSE
                    var$cod_agencia := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$cod_agencia <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'COD_AGENCIA';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$cod_agencia;
                    END IF;

                    var$cod_agencia := NULL;
                ELSE

                    dbms_output.put_line('svalidar_params_dats_bans COD_AGENCIA - var$cod_agencia = ' || par$cod_agencia);
                    dbms_output.put_line('svalidar_params_dats_bans COD_AGENCIA - var$cod_agencia_old = ' || var$cod_agencia_old);      
                    IF NVL(par$cod_agencia, const$vacio) <>  NVL(var$cod_agencia_old,  const$vacio) AND par$cod_agencia IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'COD_AGENCIA';
                        IF par$cod_agencia = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$cod_agencia;
                        END IF;
                    END IF;

                    var$cod_agencia := var$cod_agencia_old;
                END IF;
            END IF;

            dbms_output.put_line('Posterior cod_agencia');

            /* OID_CLIENTE */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'OID_CLIENTE' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$oid_cliente IS NOT NULL AND par$oid_cliente <> const$vacio THEN
                    var$oid_cliente := par$oid_cliente;
                ELSE
                    var$oid_cliente := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_CLIENTE';
                    par$acambio_valor_out(par$acambio_valor_out.count) := var$oid_cliente;
                    var$oid_cliente := NULL;
                ELSE
                    IF NVL(par$oid_cliente, const$vacio) <>  NVL(var$oid_cliente_old,  const$vacio) AND par$oid_cliente IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_CLIENTE';
                        IF par$oid_cliente = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$oid_cliente;
                        END IF;
                    END IF;
                    var$oid_cliente := var$oid_cliente_old;
                END IF;
            END IF;

            /* OID_SUBCLIENTE */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'OID_SUBCLIENTE' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$oid_subcliente IS NOT NULL AND par$oid_subcliente <> const$vacio THEN
                    var$oid_subcliente := par$oid_subcliente;
                ELSE
                    var$oid_subcliente := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_SUBCLIENTE';
                    par$acambio_valor_out(par$acambio_valor_out.count) := par$oid_subcliente;

                    var$oid_subcliente := NULL;
                ELSE
                    IF NVL(par$oid_subcliente, const$vacio) <>  NVL(var$oid_subcliente_old,  const$vacio) AND par$oid_subcliente IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_SUBCLIENTE';
                        IF par$oid_subcliente = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$oid_subcliente;
                        END IF;
                    END IF;
                    var$oid_subcliente := var$oid_subcliente_old;
                END IF;
            END IF;

            /* OID_PTO_SERVICIO */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'OID_PTO_SERVICIO' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$oid_pto_servicio IS NOT NULL AND par$oid_pto_servicio <> const$vacio THEN
                    var$oid_pto_servicio := par$oid_pto_servicio;
                ELSE
                    var$oid_pto_servicio := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_PTO_SERVICIO';
                    par$acambio_valor_out(par$acambio_valor_out.count) := var$oid_pto_servicio;

                    var$oid_pto_servicio := NULL;
                ELSE
                    IF NVL(par$oid_pto_servicio, const$vacio) <>  NVL(var$oid_pto_serv_old,  const$vacio) AND par$oid_pto_servicio IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_PTO_SERVICIO';
                        IF par$oid_pto_servicio = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$oid_pto_servicio;
                        END IF;
                    END IF;
                    var$oid_pto_servicio := var$oid_pto_serv_old;
                END IF;
            END IF;



            /* OID_DIVISA */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'OID_DIVISA' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$cod_divisa IS NOT NULL AND par$cod_divisa <> const$vacio THEN
                    var$oid_divisa := var$oid_divisa;
                ELSE
                    var$oid_divisa := NULL;
                END IF;
            ELSE

                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$cod_divisa <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_DIVISA';
                        par$acambio_valor_out(par$acambio_valor_out.count) := var$oid_divisa;
                    END IF;
                    var$oid_divisa := NULL;
                ELSE
                    IF NVL(var$oid_divisa, const$vacio) <>  NVL(var$oid_divisa_old,  const$vacio) AND var$oid_divisa is not null THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'OID_DIVISA';
                        IF par$cod_divisa = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := var$oid_divisa;
                        END IF;
                    END IF;
                    var$oid_divisa := var$oid_divisa_old;
                END IF;
            END IF;


            /* COD_TIPO_CUENTA_BANCARIA */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'COD_TIPO_CUENTA_BANCARIA' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$tipo_cuenta IS NOT NULL AND par$tipo_cuenta <> const$vacio THEN
                    var$tipo_cuenta := par$tipo_cuenta;
                ELSE
                    var$tipo_cuenta := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */

                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$tipo_cuenta <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'COD_TIPO_CUENTA_BANCARIA';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$tipo_cuenta;
                    END IF;
                    var$tipo_cuenta := NULL;
                ELSE
                    IF NVL(par$tipo_cuenta, const$vacio) <>  NVL(var$tipo_cuenta_old,  const$vacio) AND par$tipo_cuenta IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'COD_TIPO_CUENTA_BANCARIA';
                        IF par$tipo_cuenta = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$tipo_cuenta;
                        END IF;
                    END IF;
                    var$tipo_cuenta := var$tipo_cuenta_old;
                END IF;
            END IF;


            /* COD_CUENTA_BANCARIA */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'COD_CUENTA_BANCARIA' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$nro_cuenta IS NOT NULL AND par$nro_cuenta <> const$vacio THEN
                    var$nro_cuenta := par$nro_cuenta;
                ELSE
                    var$nro_cuenta := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$nro_cuenta <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'COD_CUENTA_BANCARIA';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$nro_cuenta;
                    END IF;
                    var$nro_cuenta := NULL;
                ELSE
                    IF NVL(par$nro_cuenta, const$vacio) <>  NVL(var$nro_cuenta_old,  const$vacio) AND par$nro_cuenta IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'COD_CUENTA_BANCARIA';
                        IF par$nro_cuenta = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$nro_cuenta;
                        END IF;
                    END IF;
                var$nro_cuenta := var$nro_cuenta_old;
                END IF;
            END IF;

            /* COD_DOCUMENTO */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'COD_DOCUMENTO' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$documento IS NOT NULL AND par$documento <> const$vacio THEN
                    var$documento := par$documento;
                ELSE
                    var$documento := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$documento <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'COD_DOCUMENTO';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$documento;
                    END IF;
                    var$documento := NULL;
                ELSE
                    IF NVL(par$documento, const$vacio) <>  NVL(var$documento_old,  const$vacio) AND par$documento IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'COD_DOCUMENTO';
                        IF par$documento = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$documento;
                        END IF;
                    END IF;
                    var$documento := var$documento_old;
                END IF;        
            END IF;

            /* DES_OBSERVACIONES */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_OBSERVACIONES' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$observacion IS NOT NULL AND par$observacion <> const$vacio THEN
                    var$observacion := par$observacion;
                ELSE
                    var$observacion := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$observacion <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_OBSERVACIONES';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$observacion;
                    END IF;          
                    var$observacion := NULL;
                ELSE
                    IF NVL(par$observacion, const$vacio) <>  NVL(var$observa_old,  const$vacio) AND par$observacion IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_OBSERVACIONES';
                        IF par$observacion = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$observacion;
                        END IF;
                    END IF;

                    var$observacion := var$observa_old;
                END IF;
            END IF;

            /* DES_TITULARIDAD */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_TITULARIDAD' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$titularidad IS NOT NULL AND par$titularidad <> const$vacio THEN
                    var$titularidad := par$titularidad;
                ELSE
                    var$titularidad := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN          
                    IF par$titularidad <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_TITULARIDAD';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$titularidad;
                    END IF;


                    var$titularidad := NULL;
                ELSE
                    IF NVL(par$titularidad, const$vacio) <>  NVL(var$titularidad_old,  const$vacio) and par$titularidad IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_TITULARIDAD';
                        IF par$titularidad = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$titularidad;
                        END IF;
                    END IF;

                    var$titularidad := var$titularidad_old;
                END IF;
            END IF;

            /* DES_CAMPO_ADICIONAL_1 */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_CAMPO_ADICIONAL_1' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$campadic1 IS NOT NULL AND par$campadic1 <> const$vacio THEN
                    var$campadic1 := par$campadic1;
                ELSE
                    var$campadic1 := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$campadic1 <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_1';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic1;
                    END IF;
                    var$campadic1 := NULL;
                ELSE
                    IF NVL(par$campadic1, const$vacio) <>  NVL(var$campadic1_old,  const$vacio) AND par$campadic1 IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_1';
                        IF par$campadic1 = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic1;
                        END IF;
                    END IF;
                    var$campadic1 := var$campadic1_old;
                END IF;
            END IF;


            /* DES_CAMPO_ADICIONAL_2 */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_CAMPO_ADICIONAL_2' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$campadic2 IS NOT NULL AND par$campadic2 <> const$vacio THEN
                    var$campadic2 := par$campadic2;
                ELSE
                    var$campadic2 := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$campadic2 <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_2';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic2;
                    END IF;

                    var$campadic2 := NULL;
                ELSE
                    IF NVL(par$campadic2, const$vacio) <>  NVL(var$campadic2_old,  const$vacio) AND par$campadic2 IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_2';
                        IF par$campadic2 = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic2;
                        END IF;
                    END IF;
                    var$campadic2 := var$campadic2_old;
                END IF;
            END IF;

            /* DES_CAMPO_ADICIONAL_3 */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_CAMPO_ADICIONAL_3' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$campadic3 IS NOT NULL AND par$campadic3 <> const$vacio THEN
                    var$campadic3 := par$campadic3;
                ELSE
                    var$campadic3 := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$campadic3 <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_3';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic3;
                    END IF;
                    var$campadic3 := NULL;
                ELSE
                    IF NVL(par$campadic3, const$vacio) <>  NVL(var$campadic3_old,  const$vacio) AND par$campadic3 IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_3';
                        IF par$campadic3 = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic3;
                        END IF;
                    END IF;
                    var$campadic3 := var$campadic3_old;
                END IF;
            END IF;

            /* DES_CAMPO_ADICIONAL_4 */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_CAMPO_ADICIONAL_4' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$campadic4 IS NOT NULL AND par$campadic4 <> const$vacio THEN
                    var$campadic4 := par$campadic4;
                ELSE
                    var$campadic4 := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$campadic4 <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_4';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic4;
                    END IF;
                    var$campadic4 := NULL;
                ELSE
                    IF NVL(par$campadic4, const$vacio) <>  NVL(var$campadic4_old,  const$vacio) AND par$campadic4 IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_4';
                        IF par$campadic4 = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic4;
                        END IF;
                    END IF;
                    var$campadic4 := var$campadic4_old;
                END IF;
            END IF;

            /* DES_CAMPO_ADICIONAL_5 */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_CAMPO_ADICIONAL_5' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$campadic5 IS NOT NULL AND par$campadic5 <> const$vacio THEN
                    var$campadic5 := par$campadic5;
                ELSE
                    var$campadic5 := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$campadic5 <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_5';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic5;
                    END IF;
                    var$campadic5 := NULL;
                ELSE
                    IF NVL(par$campadic5, const$vacio) <>  NVL(var$campadic5_old,  const$vacio) AND par$campadic5 IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_5';
                        IF par$campadic5 = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic5;
                        END IF;
                    END IF;
                    var$campadic5 := var$campadic5_old;
                END IF;
            END IF;

            dbms_output.put_line('Posterior DES_CAMPO_ADICIONAL_5');

            /* DES_CAMPO_ADICIONAL_6 */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_CAMPO_ADICIONAL_6' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$campadic6 IS NOT NULL AND par$campadic6 <> const$vacio THEN
                    var$campadic6 := par$campadic6;
                ELSE
                    var$campadic6 := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$campadic6 <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_6';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic6;
                    END IF;

                    var$campadic6 := NULL;
                ELSE
                    IF NVL(par$campadic6, const$vacio) <>  NVL(var$campadic6_old,  const$vacio) AND par$campadic6 IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_6';
                        IF par$campadic6 = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic6;
                        END IF;
                    END IF;
                    var$campadic6 := var$campadic6_old;
                END IF;
            END IF;

            /* DES_CAMPO_ADICIONAL_7 */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_CAMPO_ADICIONAL_7' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$campadic7 IS NOT NULL AND par$campadic7 <> const$vacio THEN
                    var$campadic7 := par$campadic7;
                ELSE
                    var$campadic7 := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$campadic7 <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_7';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic7;
                    END IF;
                    var$campadic7 := NULL;
                ELSE
                    IF NVL(par$campadic7, const$vacio) <>  NVL(var$campadic7_old,  const$vacio) AND par$campadic7 IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_7';
                        IF par$campadic7 = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic7;
                        END IF;
                    END IF;
                    var$campadic7 := var$campadic7_old;
                END IF;
            END IF;

            /* DES_CAMPO_ADICIONAL_8 */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'DES_CAMPO_ADICIONAL_8' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$campadic8 IS NOT NULL AND par$campadic8 <> const$vacio THEN
                    var$campadic8 := par$campadic8;
                ELSE
                    var$campadic8 := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    IF par$campadic8 <> const$vacio THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_8';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic8;
                    END IF;
                    var$campadic8 := NULL;
                ELSE
                    IF NVL(par$campadic8, const$vacio) <>  NVL(var$campadic8_old,  const$vacio) AND par$campadic8 IS NOT NULL THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'DES_CAMPO_ADICIONAL_8';
                        IF par$campadic8 = const$vacio THEN
                            par$acambio_valor_out(par$acambio_valor_out.count) := '';
                        ELSE
                            par$acambio_valor_out(par$acambio_valor_out.count) := par$campadic8;
                        END IF;
                    END IF;
                    var$campadic8 := var$campadic8_old;
                END IF;
            END IF;

            /* BOL_ACTIVO */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'BOL_ACTIVO' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$bol_activo IS NOT NULL THEN
                    var$activo := par$bol_activo;
                ELSE
                    var$activo := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    par$acambio_campo_out(par$acambio_campo_out.count) := 'BOL_ACTIVO';
                    par$acambio_valor_out(par$acambio_valor_out.count) := par$bol_activo;

                    var$activo := NULL;
                ELSE
                    IF par$bol_activo <>  var$activo_old  AND par$bol_activo is not null THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'BOL_ACTIVO';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$bol_activo;
                    END IF;
                    var$activo := var$activo_old;
                END IF;
            END IF;

            /* BOL_DEFECTO */
            BEGIN
                select count(OID_CAMPO1)
                into var$necesita_aprobacion
                from SAPR_GTT_TAUXILIAR
                where OID_CAMPO1 = 'BOL_DEFECTO' AND COD_CALIFICADOR='CAMPO_REQ_APROB';
            EXCEPTION
                WHEN OTHERS THEN
                var$necesita_aprobacion := 0;
            END;

            IF var$necesita_aprobacion=0 THEN
                IF par$patron IS NOT NULL  THEN
                    var$patron := par$patron;
                ELSE
                    var$patron := NULL;
                END IF;
            ELSE
                /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
                IF upper(par$cod_accion) = const$accionAlta THEN
                    par$acambio_campo_out(par$acambio_campo_out.count) := 'BOL_DEFECTO';
                    par$acambio_valor_out(par$acambio_valor_out.count) := par$patron;

                    var$patron := NULL;
                ELSE
                    IF par$patron <>  var$patron_old AND par$patron is not null THEN
                        par$acambio_campo_out(par$acambio_campo_out.count) := 'BOL_DEFECTO';
                        par$acambio_valor_out(par$acambio_valor_out.count) := par$patron;
                    END IF; 
                    var$patron := var$patron_old;
                END IF;
            END IF;        
        END IF;
        dbms_output.put_line('svalidar_params_dats_bans - Previo poblar outs y posterior a BOL_DEFECTO');
    ELSE
        /*Se trata de un alta que no requiere aprobación*/
        /* OID_BANCO */
        IF par$cod_banco IS NOT NULL AND par$cod_banco <> const$vacio THEN
            var$oid_banco := var$oid_banco;
        ELSE
            var$oid_banco := NULL;
        END IF;

        /* OID_CLIENTE */
        IF par$oid_cliente IS NOT NULL AND par$oid_cliente <> const$vacio THEN
            var$oid_cliente := par$oid_cliente;
        ELSE
            var$oid_cliente := NULL;
        END IF;

        /* OID_SUBCLIENTE */
        IF par$oid_subcliente IS NOT NULL AND par$oid_subcliente <> const$vacio THEN
            var$oid_subcliente := par$oid_subcliente;
        ELSE
            var$oid_subcliente := NULL;
        END IF;

        /* OID_PTO_SERVICIO */
        IF par$oid_pto_servicio IS NOT NULL AND par$oid_pto_servicio <> const$vacio THEN
            var$oid_pto_servicio := par$oid_pto_servicio;
        ELSE
            var$oid_pto_servicio := NULL;
        END IF;

        /* COD_TIPO_CUENTA_BANCARIA */
        IF par$tipo_cuenta IS NOT NULL AND par$tipo_cuenta <> const$vacio THEN
            var$tipo_cuenta := par$tipo_cuenta;
        ELSE
            var$tipo_cuenta := NULL;
        END IF;

        /* COD_AGENCIA */
        IF par$cod_agencia IS NOT NULL AND par$cod_agencia <> const$vacio THEN
            var$cod_agencia := par$cod_agencia;
        ELSE
            var$cod_agencia := NULL;
        END IF;

        /* DES_OBSERVACIONES */
        IF par$observacion IS NOT NULL AND par$observacion <> const$vacio THEN
            var$observacion := par$observacion;
        ELSE
            var$observacion := NULL;
        END IF;

        /* DES_TITULARIDAD */
        IF par$titularidad IS NOT NULL AND par$titularidad <> const$vacio THEN
            var$titularidad := par$titularidad;
        ELSE
            var$titularidad := NULL;
        END IF;

        /* COD_CUENTA_BANCARIA */
        IF par$nro_cuenta IS NOT NULL AND par$nro_cuenta <> const$vacio THEN
            var$nro_cuenta := par$nro_cuenta;
        ELSE
            var$nro_cuenta := NULL;
        END IF;

        /* COD_DOCUMENTO */
        IF par$documento IS NOT NULL AND par$documento <> const$vacio THEN
            var$documento := par$documento;
        ELSE
            var$documento := NULL;
        END IF;

        /* DES_CAMPO_ADICIONAL_1 */
        IF par$campadic1 IS NOT NULL AND par$campadic1 <> const$vacio THEN
            var$campadic1 := par$campadic1;
        ELSE
            var$campadic1 := NULL;
        END IF;

        /* DES_CAMPO_ADICIONAL_2 */
        IF par$campadic2 IS NOT NULL AND par$campadic2 <> const$vacio THEN
            var$campadic2 := par$campadic2;
        ELSE
            var$campadic2 := NULL;
        END IF;

        /* DES_CAMPO_ADICIONAL_3 */
        IF par$campadic3 IS NOT NULL AND par$campadic3 <> const$vacio THEN
            var$campadic3 := par$campadic3;
        ELSE
            var$campadic3 := NULL;
        END IF;

        /* DES_CAMPO_ADICIONAL_4 */
        IF par$campadic4 IS NOT NULL AND par$campadic4 <> const$vacio THEN
            var$campadic4 := par$campadic4;
        ELSE
            var$campadic4 := NULL;
        END IF;

        /* DES_CAMPO_ADICIONAL_5 */
        IF par$campadic5 IS NOT NULL AND par$campadic5 <> const$vacio THEN
            var$campadic5 := par$campadic5;
        ELSE
            var$campadic5 := NULL;
        END IF;

        /* DES_CAMPO_ADICIONAL_6 */
        IF par$campadic6 IS NOT NULL AND par$campadic6 <> const$vacio THEN
            var$campadic6 := par$campadic6;
        ELSE
            var$campadic6 := NULL;
        END IF;

        /* DES_CAMPO_ADICIONAL_7 */
        IF par$campadic7 IS NOT NULL AND par$campadic7 <> const$vacio THEN
            var$campadic7 := par$campadic7;
        ELSE
            var$campadic7 := NULL;
        END IF;

        /* DES_CAMPO_ADICIONAL_8 */
        IF par$campadic8 IS NOT NULL AND par$campadic8 <> const$vacio THEN
            var$campadic8 := par$campadic8;
        ELSE
            var$campadic8 := NULL;
        END IF;

        /* BOL_ACTIVO */
        IF par$bol_activo IS NOT NULL THEN
            var$activo := par$bol_activo;
        ELSE
            var$activo := NULL;
        END IF;

        /* BOL_DEFECTO - PATRON */
        var$patron := par$patron;

    END IF;

    /* Poblamos los parametros de salida */    
    par$cod_accion_out                := par$cod_accion;
    par$oid_cliente_out               := var$oid_cliente;
    par$oid_subcli_out                := var$oid_subcliente;
    par$oid_pto_serv_out              := var$oid_pto_servicio;
    par$oid_banco_out                 := var$oid_banco;
    par$tipo_cuenta_out               := var$tipo_cuenta;
    par$cod_agencia_out               := var$cod_agencia;
    par$nro_cuenta_out                := var$nro_cuenta;
    par$documento_out                 := var$documento;
    par$titularidad_out               := var$titularidad;
    par$oid_divisa_out                := var$oid_divisa;
    par$observacion_out               := var$observacion;
    par$patron_out                    := var$patron;
    par$campadic1_out                 := var$campadic1;
    par$campadic2_out                 := var$campadic2;
    par$campadic3_out                 := var$campadic3;
    par$campadic4_out                 := var$campadic4;
    par$campadic5_out                 := var$campadic5;
    par$campadic6_out                 := var$campadic6;
    par$campadic7_out                 := var$campadic7;
    par$campadic8_out                 := var$campadic8;
    par$activo_out                    := var$activo;

    dbms_output.put_line('svalidar_params_dats_bans - Posterior poblar outs');

END svalidar_params_dats_bans;


 /* Se encarga de insertar o de actualizar registros en la tabla SAPR_TDATO_BANCARIO */                       
 PROCEDURE supd_dato_bancario(par$oid_dato_bancario     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_banco                   IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_cliente                 IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_subcliente              IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_pto_servicio            IN gepr_pcomon_###VERSION###.tipo$oid_,
                        par$oid_divisa                  IN gepr_pcomon_###VERSION###.tipo$oid_,  
                        par$cod_tipo_cuenta_bancaria    IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_cuenta_bancaria         IN gepr_pcomon_###VERSION###.tipo$cod_,        
                        par$cod_documento               IN gepr_pcomon_###VERSION###.tipo$cod_,  
                        par$des_titularidad             IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_observaciones           IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$bol_defecto                 IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$bol_activo                  IN gepr_pcomon_###VERSION###.tipo$nel_,
                        par$cod_agencia                 IN gepr_pcomon_###VERSION###.tipo$cod_,  
                        par$des_campo_adicional_1       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_2       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_3       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_4       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_5       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_6       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_7       IN gepr_pcomon_###VERSION###.tipo$desc_,
                        par$des_campo_adicional_8       IN gepr_pcomon_###VERSION###.tipo$desc_,    
                        par$cod_usuario                 IN gepr_pcomon_###VERSION###.tipo$cod_)  IS
    /* variables locales */
    var$gmt_zero            gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_dato_bancario   gepr_pcomon_###VERSION###.tipo$oid_;
    var$qry_update          gepr_pcomon_###VERSION###.tipo$obs_ ;
    cont$vacio CONSTANT     gepr_pcomon_###VERSION###.tipo$desc_ := '###VACIO###';

    BEGIN
        /*Busco el oid_dato_bancario para garantizar que exista*/
        IF par$oid_dato_bancario IS NOT NULL THEN
            BEGIN
                SELECT oid_dato_bancario
                INTO var$oid_dato_bancario
                FROM SAPR_TDATO_BANCARIO
                WHERE OID_DATO_BANCARIO = par$oid_dato_bancario;
            EXCEPTION WHEN no_data_found THEN
                var$oid_dato_bancario := NULL;
            END;
        END IF;
        IF var$oid_dato_bancario IS NULL THEN
            par$oid_dato_bancario := sys_guid();

            INSERT INTO SAPR_TDATO_BANCARIO (
             OID_DATO_BANCARIO
            ,OID_BANCO
            ,OID_CLIENTE
            ,OID_SUBCLIENTE
            ,OID_PTO_SERVICIO
            ,OID_DIVISA
            ,COD_TIPO_CUENTA_BANCARIA
            ,COD_CUENTA_BANCARIA
            ,COD_DOCUMENTO
            ,DES_TITULARIDAD
            ,DES_OBSERVACIONES
            ,BOL_DEFECTO
            ,BOL_ACTIVO
            ,COD_AGENCIA 
            ,DES_CAMPO_ADICIONAL_1 
            ,DES_CAMPO_ADICIONAL_2 
            ,DES_CAMPO_ADICIONAL_3 
            ,DES_CAMPO_ADICIONAL_4 
            ,DES_CAMPO_ADICIONAL_5 
            ,DES_CAMPO_ADICIONAL_6 
            ,DES_CAMPO_ADICIONAL_7 
            ,DES_CAMPO_ADICIONAL_8
            ,GMT_CREACION
            ,DES_USUARIO_CREACION
            ,GMT_MODIFICACION
            ,DES_USUARIO_MODIFICACION)
            VALUES
            (par$oid_dato_bancario
            ,par$oid_banco
            ,par$oid_cliente
            ,par$oid_subcliente
            ,par$oid_pto_servicio
            ,par$oid_divisa
            ,par$cod_tipo_cuenta_bancaria
            ,par$cod_cuenta_bancaria
            ,par$cod_documento
            ,par$des_titularidad
            ,par$des_observaciones
            ,par$bol_defecto
            ,par$bol_activo
            ,par$cod_agencia 
            ,par$des_campo_adicional_1 
            ,par$des_campo_adicional_2
            ,par$des_campo_adicional_3
            ,par$des_campo_adicional_4
            ,par$des_campo_adicional_5 
            ,par$des_campo_adicional_6
            ,par$des_campo_adicional_7
            ,par$des_campo_adicional_8
            ,var$gmt_zero
            ,par$cod_usuario
            ,var$gmt_zero
            ,par$cod_usuario
            );
        ELSE
            IF  par$oid_banco IS NOT NULL  THEN
                var$qry_update := var$qry_update || q'[ OID_BANCO = ']' || par$oid_banco ||q'[', ]';
            END IF;

            IF  par$oid_divisa IS NOT NULL  THEN
                var$qry_update := var$qry_update || q'[ OID_DIVISA = ']' || par$oid_divisa ||q'[', ]';
            END IF;

            IF par$cod_tipo_cuenta_bancaria IS NOT NULL THEN
                var$qry_update := var$qry_update || q'[ COD_TIPO_CUENTA_BANCARIA = ']' || par$cod_tipo_cuenta_bancaria || q'[', ]';
            END IF;

            IF par$cod_cuenta_bancaria IS NOT NULL THEN
                var$qry_update := var$qry_update || q'[ COD_CUENTA_BANCARIA = ']' || par$cod_cuenta_bancaria ||q'[', ]';
            END IF;

            IF par$cod_documento IS NOT NULL THEN
                IF par$cod_documento = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ COD_DOCUMENTO = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ COD_DOCUMENTO = ']' || par$cod_documento ||q'[', ]';
                END IF;
            END IF;

            IF par$des_titularidad IS NOT NULL THEN
                var$qry_update := var$qry_update || q'[ DES_TITULARIDAD = ']' || par$des_titularidad ||q'[', ]';
            END IF;

            IF par$des_observaciones IS NOT NULL THEN
                IF par$des_observaciones = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_OBSERVACIONES = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_OBSERVACIONES = ']' || par$des_observaciones ||q'[', ]';
                END IF;
            END IF;

            IF par$bol_defecto IS NOT NULL THEN
                var$qry_update := var$qry_update || q'[ BOL_DEFECTO = ']' || par$bol_defecto ||q'[', ]';
            END IF;

            IF par$bol_activo IS NOT NULL THEN
                var$qry_update := var$qry_update || q'[ BOL_ACTIVO = ']' || par$bol_activo ||q'[', ]';
            END IF;
            dbms_output.put_line('Codigo de agencia en UPDATE' || par$cod_agencia);
            IF par$cod_agencia IS NOT NULL THEN
                IF par$cod_agencia = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ COD_AGENCIA = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ COD_AGENCIA = ']' || par$cod_agencia ||q'[', ]';
                END IF;
            END IF;

            IF par$des_campo_adicional_1 IS NOT NULL THEN
                IF par$des_campo_adicional_1 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_1 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_1 = ']' || par$des_campo_adicional_1 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_campo_adicional_2 IS NOT NULL THEN
                IF par$des_campo_adicional_2 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_2 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_2 = ']' || par$des_campo_adicional_2 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_campo_adicional_3 IS NOT NULL THEN
                 IF par$des_campo_adicional_3 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_3 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_3 = ']' || par$des_campo_adicional_3 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_campo_adicional_4 IS NOT NULL THEN
             IF par$des_campo_adicional_4 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_4 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_4 = ']' || par$des_campo_adicional_4 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_campo_adicional_5 IS NOT NULL THEN
             IF par$des_campo_adicional_5 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_5 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_5 = ']' || par$des_campo_adicional_5 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_campo_adicional_6 IS NOT NULL THEN
             IF par$des_campo_adicional_6 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_6 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_6 = ']' || par$des_campo_adicional_6 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_campo_adicional_7 IS NOT NULL THEN
             IF par$des_campo_adicional_7 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_7 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_7 = ']' || par$des_campo_adicional_7 ||q'[', ]';
                END IF;
            END IF;

            IF par$des_campo_adicional_8 IS NOT NULL THEN
             IF par$des_campo_adicional_8 = cont$vacio THEN
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_8 = '', ]';
                ELSE
                    var$qry_update := var$qry_update || q'[ DES_CAMPO_ADICIONAL_8 = ']' || par$des_campo_adicional_8 ||q'[', ]';
                END IF;
            END IF;

           IF  var$qry_update IS NOT NULL  THEN
             DBMS_OUTPUT.PUT_LINE('UPDATE SAPR_TDATO_BANCARIO SET ' ||  var$qry_update || ' GMT_MODIFICACION =' || var$gmt_zero || 'DES_USUARIO_MODIFICACION = ' || par$cod_usuario || ' WHERE OID_DATO_BANCARIO = ' ||var$oid_dato_bancario);
             dbms_output.put_line('Cod_Usuario' || par$cod_usuario);
             EXECUTE IMMEDIATE q'[ UPDATE SAPR_TDATO_BANCARIO SET ]' || var$qry_update ||q'[
             GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2
             WHERE OID_DATO_BANCARIO = :3]'
              USING  var$gmt_zero, par$cod_usuario, var$oid_dato_bancario; 
           END IF;
        END IF;
 END supd_dato_bancario;

 FUNCTION fhay_error RETURN BOOLEAN IS
  var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN
    BEGIN
      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR A
        WHERE A.COD_CALIFICADOR = 'VALIDACIONES_DATO_BANCO'
        AND OID_CAMPO1 <> '0040080000';
    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;
    DBMS_OUTPUT.PUT_LINE('Error VALIDACIONES_DATO_BANCO: ' || var$existe);
    RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);
  END fhay_error;


 /* Grabamos el cambio en la tabla SAPR_TDATO_BANCARIO_CAMBIO */
 PROCEDURE supd_dato_bancario_cambio(
                            par$oid_dato_banc_cambio IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_dato_bancario  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_campo          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$des_valor          IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$des_comentario     IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$oid_usuario        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$num_aprobaciones   IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_estado         IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$bol_activo         IN gepr_pcomon_###VERSION###.tipo$nel_) IS
                            var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
                            var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_;
                            var$des_login       gepr_pcomon_###VERSION###.tipo$cod_;
                            var$oid_usuario gepr_pcomon_###VERSION###.tipo$oid_;
                            var$oid_hist_dato_bancario gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    BEGIN
        var$oid_usuario := par$oid_usuario;
        select 
            UPPER(DES_LOGIN)
        INTO
            var$des_login
        from adpr_tusuario 
        where oid_usuario = par$oid_usuario;
    EXCEPTION
        WHEN OTHERS THEN
            dbms_output.put_line('No encontró el usuario (oid ' || par$oid_usuario || ')');
            var$des_login := upper('supd_dato_bancario_cambio');
            var$oid_usuario := null;
    END;
    DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 1');
      IF par$oid_dato_banc_cambio IS NULL THEN
          DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 2');
          par$oid_dato_banc_cambio := SYS_GUID();

          dbms_output.put_line('var$gmt_zero = ' || var$gmt_zero);
          dbms_output.put_line('var$des_login = ' || var$des_login);

          dbms_output.put_line('par$cod_campo = ' || par$cod_campo);
          dbms_output.put_line('par$oid_dato_bancario = ' || par$oid_dato_bancario);


          UPDATE 
            SAPR_TDATO_BANCARIO_CAMBIO 
         SET 
            BOL_ACTIVO = 0,
            FYH_MODIFICACION = CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE),
            DES_USUARIO_MODIFICACION = var$des_login
          WHERE  OID_DATO_BANCARIO = par$oid_dato_bancario AND trim(upper(COD_CAMPO)) =  trim(upper(par$cod_campo))
                       AND COD_ESTADO = 'PD' and bol_activo = 1;

          DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 2 A');
          /*Grabo en la tabla de historico el valor actual del dato bancario*/
          sins_hist_dato_bancario(
                            par$oid_dato_bancario => par$oid_dato_bancario,
                            par$cod_usuario => var$des_login,
                            par$bol_hacer_commit => 0, /* par$bol_hacer_commit = 0 porque el commit lo realiza este procedure */
                            par$oid_hist_dato_bancario => var$oid_hist_dato_bancario);

          INSERT INTO SAPR_TDATO_BANCARIO_CAMBIO (OID_DATO_BANCARIO_CAMBIO, OID_DATO_BANCARIO, COD_CAMPO, DES_VALOR ,
          NUM_APROBACIONES, COD_ESTADO, BOL_ACTIVO, FYH_MODIFICACION, OID_USUARIO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION,
          DES_USUARIO_MODIFICACION, OID_HIST_DATO_BANCARIO, DES_COMENTARIO) VALUES
          (par$oid_dato_banc_cambio, par$oid_dato_bancario, par$cod_campo, par$des_valor, par$num_aprobaciones, par$cod_estado, par$bol_activo,  CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), var$oid_usuario, var$gmt_zero, var$des_login, CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), var$des_login, var$oid_hist_dato_bancario,par$des_comentario);

          DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 2 B');

      ELSE
            DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 3');
            IF  par$cod_campo IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ COD_CAMPO = ']' || par$cod_campo ||q'[', ]';
            END IF;
            IF  par$des_valor IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ DES_VALOR = ']' || par$des_valor ||q'[', ]';
            END IF;
            IF  par$num_aprobaciones IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ NUM_APROBACIONES = ']' || par$num_aprobaciones ||q'[', ]';
            END IF;
            IF  par$cod_estado IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ COD_ESTADO = ']' || par$cod_estado ||q'[', ]';
            END IF;
            IF  par$bol_activo IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ BOL_ACTIVO = ']' || par$bol_activo ||q'[', ]';
            END IF;
            DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 4');
           IF  var$qry_update IS NOT NULL  THEN

           DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 4 var$qry_update: ' || var$qry_update);
           DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 4 var$oid_usuario: ' || par$oid_usuario);
           DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 4 var$oid_dato_banc_cambio: ' || par$oid_dato_banc_cambio);
             EXECUTE IMMEDIATE q'[ UPDATE SAPR_TDATO_BANCARIO_CAMBIO SET ]' || var$qry_update ||q'[
             GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2
             WHERE OID_DATO_BANCARIO_CAMBIO = :3]'
              USING CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), var$des_login, par$oid_dato_banc_cambio;
           END IF;
          DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 5');
        END IF;

        DBMS_OUTPUT.PUT_LINE('supd_dato_bancario_cambio - 6');

  END supd_dato_bancario_cambio;



/* Se encarga de generar registros en la tabla SAPR_TDATO_BANCARIO_APROBACION */ 
 PROCEDURE sins_dato_banc_aprobacion(
                            par$oid_dato_banc_cambio  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_usuario        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$des_comentario     IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$bol_aprobado       IN gepr_pcomon_###VERSION###.tipo$nel_) IS
             var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
             var$des_login       gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN
      BEGIN
            SELECT 
                UPPER(DES_LOGIN)
            INTO
                var$des_login
            FROM adpr_tusuario 
            WHERE oid_usuario = par$oid_usuario;
        EXCEPTION
            WHEN OTHERS THEN
                dbms_output.put_line('No encontró el usuario (oid ' || par$oid_usuario || ')');
                var$des_login := NULL;
        END;
     INSERT INTO SAPR_TDATO_BANCARIO_APROBACION (OID_DATO_BANCARIO_APROBACION, OID_DATO_BANCARIO_CAMBIO, USUARIO_APROBACION, FYH_APROBACION ,BOL_APROBADO, DES_COMENTARIO,
        GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION) VALUES
          (SYS_GUID(), par$oid_dato_banc_cambio, par$oid_usuario, CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), par$bol_aprobado, par$des_comentario, var$gmt_zero, var$des_login, CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), var$des_login);

  END sins_dato_banc_aprobacion;


/* Devuelve la información almacenada en SAPR_TDATO_BANCARIO_CAMBIO */
PROCEDURE srecuperar_datos(
                            par$cod_usuario             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_pais                IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /* ---- FILTROS ---- */
                            /*Estados*/
                            par$acod_estado             IN gepr_pcomon_###VERSION###.tipo$cods_,
                            /*Campos*/
                            par$acod_campo              IN gepr_pcomon_###VERSION###.tipo$cods_,
                            /*Clientes*/
                            par$aoid_cliente            IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_subcliente         IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$aoid_pto_servicio       IN gepr_pcomon_###VERSION###.tipo$oids_,
                            /*Usuario aprob*/
                            par$aoid_usu_aprob          IN gepr_pcomon_###VERSION###.tipo$oids_,
                            /*Usuario modif*/
                            par$aoid_usu_modif          IN gepr_pcomon_###VERSION###.tipo$oids_,
                            /*TipoFecha*/
                            par$cod_tipo_fecha          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            /*Fecha desde*/
                            par$fecha_desde             IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            /*Fecha hasta*/            
                            par$fecha_hasta             IN gepr_pcomon_###VERSION###.tipo$fyh_,
                            /* ---- FIN FILTROS ---- */
                            par$rc_cambios              OUT sys_refcursor,
                            par$rc_datos                OUT sys_refcursor,
                            par$rc_aprob                OUT sys_refcursor) IS
    const$new_line                    CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
    const$comilla_simple              CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);

    var$bol_filtra_cliente gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_subcliente gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_punto gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_usu_aprob gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_usu_modif gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_estado gepr_pcomon_###VERSION###.tipo$nel_;
    var$bol_filtra_campo gepr_pcomon_###VERSION###.tipo$nel_;

    var$qry_select gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_where gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_final gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_joins gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_insert gepr_pcomon_###VERSION###.tipo$obs_;
    var$qry_filtro_cliente      gepr_pcomon_###VERSION###.tipo$obs_;

    var$cantidad_aprobaciones   gepr_pcomon_###VERSION###.tipo$desc_;

  BEGIN     
    var$bol_filtra_cliente := 0;
    var$bol_filtra_subcliente := 0;
    var$bol_filtra_punto := 0;
    var$bol_filtra_usu_aprob := 0;
    var$bol_filtra_usu_modif := 0;
    var$bol_filtra_estado := 0;
    var$bol_filtra_campo := 0;

    var$qry_select := '';
    var$qry_where := '';
    var$qry_final := '';
    var$qry_joins := '';
    var$qry_insert := '';
    var$qry_filtro_cliente := '';


    /*Buscar parametros CantidadAprobacionesCuentasBancarias y CamposAprobacionRequeridaCuentasBancarias*/
    /*Para poder indicar si debe estar activo o no el dato bancario*/        
    var$cantidad_aprobaciones := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                par$cod_pais       => par$cod_pais,
                                par$cod_parametro  => 'CantidadAprobacionesCuentasBancarias',
                                par$cod_aplicacion => 'Genesis');

    DELETE  FROM SAPR_GTT_TAUXILIAR
    WHERE 
        COD_CALIFICADOR IN 
        (
            'DATO_BANCARIO_CAMBIO',
            'RP_OID_CLIENTE',
            'RP_OID_SUBCLIENTE',
            'RP_OID_PTO_SERVICIO',
            'RP_OID_USR_APRO',
            'RP_OID_USR_MODI',
            'RP_ESTADO',
            'RP_CAMPO'
        );
    DBMS_OUTPUT.PUT_LINE('Recupera Datos - A');
    /*Cargo en la tabla auxiliar los datos de filtro de clientes*/
    IF par$aoid_cliente IS NOT NULL AND par$aoid_cliente.COUNT > 0 THEN
        FOR idx in par$aoid_cliente.first .. par$aoid_cliente.last LOOP
            IF par$aoid_cliente(idx) is not null OR par$aoid_cliente(idx) <> '' THEN
                var$bol_filtra_cliente := 1;
                INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                VALUES (par$aoid_cliente(idx), 'RP_OID_CLIENTE');
            END IF;
        END LOOP;
    END IF;
    DBMS_OUTPUT.PUT_LINE('Recupera Datos - A 1');

    IF par$aoid_subcliente IS NOT NULL AND par$aoid_subcliente.COUNT > 0 THEN             
        FOR idx in par$aoid_subcliente.first .. par$aoid_subcliente.last LOOP
            IF par$aoid_subcliente(idx) is not null OR par$aoid_subcliente(idx) <> '' THEN
                    var$bol_filtra_subcliente:=1;
                    INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (par$aoid_subcliente(idx), 'RP_OID_SUBCLIENTE');
            END IF;
        END LOOP;
    END IF;            

    DBMS_OUTPUT.PUT_LINE('Recupera Datos - A 2');
    IF par$aoid_pto_servicio IS NOT NULL AND par$aoid_pto_servicio.COUNT > 0 THEN    
        FOR idx in par$aoid_pto_servicio.first .. par$aoid_pto_servicio.last LOOP
            IF par$aoid_pto_servicio(idx) is not null or par$aoid_pto_servicio(idx) <> '' THEN
                    var$bol_filtra_punto := 1;
                    INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (par$aoid_pto_servicio(idx), 'RP_OID_PTO_SERVICIO');
                END IF;
        END LOOP;
    END IF;

    DBMS_OUTPUT.PUT_LINE('Recupera Datos - B');
    IF par$aoid_usu_aprob  IS NOT NULL AND par$aoid_usu_aprob.COUNT > 0 THEN
        FOR idx IN par$aoid_usu_aprob.first .. par$aoid_usu_aprob.last  LOOP
            IF par$aoid_usu_aprob(idx) is not null OR par$aoid_usu_aprob(idx) <> '' THEN
                var$bol_filtra_usu_aprob := 1;
                INSERT INTO SAPR_GTT_TAUXILIAR
                    (OID_CAMPO1, COD_CALIFICADOR, COD_CAMPO2)
                SELECT 
                    OID_USUARIO, 'RP_OID_USR_APRO', DES_LOGIN
                  FROM
                    ADPR_TUSUARIO
                 WHERE
                    OID_USUARIO = par$aoid_usu_aprob(idx);
            END IF;          
        END LOOP;
    END IF;
    DBMS_OUTPUT.PUT_LINE('C');
    IF par$aoid_usu_modif  IS NOT NULL AND par$aoid_usu_modif.COUNT > 0 THEN
        FOR idx IN par$aoid_usu_modif.first .. par$aoid_usu_modif.last  LOOP
            IF par$aoid_usu_modif(idx) is not null OR par$aoid_usu_modif(idx) <> '' THEN
                var$bol_filtra_usu_modif := 1;
                INSERT INTO SAPR_GTT_TAUXILIAR
                    (OID_CAMPO1, COD_CALIFICADOR, COD_CAMPO2)
                SELECT 
                    OID_USUARIO, 'RP_OID_USR_MODI', DES_LOGIN
                  FROM 
                    ADPR_TUSUARIO
                 WHERE 
                    OID_USUARIO = par$aoid_usu_modif(idx);
            END IF;          
        END LOOP;
    END IF;

    IF par$acod_estado  IS NOT NULL AND par$acod_estado.COUNT > 0 THEN
        FOR idx IN par$acod_estado.first .. par$acod_estado.last  LOOP
            IF par$acod_estado(idx) is not null OR par$acod_estado(idx) <> '' THEN
                var$bol_filtra_estado := 1;
                INSERT INTO SAPR_GTT_TAUXILIAR
                    (OID_CAMPO1, COD_CALIFICADOR)
                VALUES(par$acod_estado(idx), 'RP_ESTADO');
            END IF;          
        END LOOP;
    END IF;

     IF par$acod_campo  IS NOT NULL AND par$acod_campo.COUNT > 0 THEN
        FOR idx IN par$acod_campo.first .. par$acod_campo.last  LOOP
            IF par$acod_campo(idx) is not null OR par$acod_campo(idx) <> '' THEN
                var$bol_filtra_campo := 1;
                INSERT INTO SAPR_GTT_TAUXILIAR
                    (OID_CAMPO1, COD_CALIFICADOR)
                VALUES(par$acod_campo(idx), 'RP_CAMPO');
            END IF;          
        END LOOP;
    END IF;


    /* Query INSERT */
    var$qry_insert := 'INSERT INTO SAPR_GTT_TAUXILIAR(OID_CAMPO1, OID_CAMPO7, COD_CALIFICADOR)' || const$new_line;
    DBMS_OUTPUT.PUT_LINE('Recupera Datos - D');
    /* Query principal */
    var$qry_select := 'SELECT DISTINCT' || const$new_line;
    var$qry_select := var$qry_select || 'CAMBIO.OID_DATO_BANCARIO_CAMBIO, CAMBIO.OID_DATO_BANCARIO, ' || const$comilla_simple || 'DATO_BANCARIO_CAMBIO' || const$comilla_simple || const$new_line;
    var$qry_select := var$qry_select || 'FROM' || const$new_line;
    var$qry_select := var$qry_select || '   SAPR_TDATO_BANCARIO_CAMBIO CAMBIO' || const$new_line;
    var$qry_select := var$qry_select || 'INNER JOIN SAPR_TDATO_BANCARIO DATO_BANCARIO ON DATO_BANCARIO.OID_DATO_BANCARIO = CAMBIO.OID_DATO_BANCARIO' || const$new_line;
    var$qry_select := var$qry_select || 'LEFT JOIN SAPR_TDATO_BANCARIO_APROBACION APROBACION ON CAMBIO.OID_DATO_BANCARIO_CAMBIO = APROBACION.OID_DATO_BANCARIO_CAMBIO' || const$new_line;

    /* -------- Inicio query joins -------- */
    IF var$bol_filtra_usu_aprob = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR USUAPR ON USUAPR.OID_CAMPO1 = APROBACION.USUARIO_APROBACION AND USUAPR.COD_CALIFICADOR = ' || const$comilla_simple || 'RP_OID_USR_APRO' || const$comilla_simple || const$new_line;
    END IF;

    IF var$bol_filtra_usu_modif = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR USUMOD ON USUMOD.OID_CAMPO1 = CAMBIO.OID_USUARIO AND USUMOD.COD_CALIFICADOR = ' || const$comilla_simple || 'RP_OID_USR_MODI' ||  const$comilla_simple || const$new_line;
    END IF;

    IF var$bol_filtra_estado = 1 THEN 
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR EST ON EST.OID_CAMPO1 = CAMBIO.COD_ESTADO AND EST.COD_CALIFICADOR = ' || const$comilla_simple || 'RP_ESTADO' || const$comilla_simple || const$new_line;
    END IF;

     IF var$bol_filtra_campo = 1 THEN 
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR EST ON EST.OID_CAMPO1 = CAMBIO.COD_CAMPO AND EST.COD_CALIFICADOR = ' || const$comilla_simple || 'RP_CAMPO' || const$comilla_simple || const$new_line;
    END IF;
/*
    IF var$bol_filtra_cliente = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR FILTRO_CLIENTE ON FILTRO_CLIENTE.OID_CAMPO1 = DATO_BANCARIO.OID_CLIENTE AND FILTRO_CLIENTE.COD_CALIFICADOR = ' || const$comilla_simple || 'RP_OID_CLIENTE' ||  const$comilla_simple || const$new_line;
    END IF;

    IF var$bol_filtra_subcliente = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR FILTRO_SUBCLI ON FILTRO_SUBCLI.OID_CAMPO1 = DATO_BANCARIO.OID_SUBCLIENTE AND FILTRO_SUBCLI.COD_CALIFICADOR = ' || const$comilla_simple || 'RP_OID_SUBCLIENTE' || const$comilla_simple || const$new_line;
    END IF; 

    IF var$bol_filtra_punto = 1 THEN
        var$qry_joins := var$qry_joins || 'INNER JOIN SAPR_GTT_TAUXILIAR FILTRO_PUNTO ON FILTRO_PUNTO.OID_CAMPO1 = DATO_BANCARIO.OID_PTO_SERVICIO AND FILTRO_PUNTO.COD_CALIFICADOR = ' || const$comilla_simple || 'RP_OID_PTO_SERVICIO' || const$comilla_simple || const$new_line;
    END IF;
*/


    /* -------- Fin query joins -------- */

    /* Query WHERE */
    var$qry_where := var$qry_where || 'WHERE' || const$new_line;
    var$qry_where := var$qry_where || ' 1 = 1' || const$new_line;
    var$qry_where := var$qry_where || ' AND CAMBIO.BOL_ACTIVO = 1';

    /*Filtros de fecha - INICIO*/
    IF par$cod_tipo_fecha IS NOT NULL THEN
        /*Tipo de filtro por Fecha de Modificación*/
        IF par$cod_tipo_fecha = 'MD' THEN
            /*Filtro fecha desde*/
            IF par$fecha_desde IS NOT NULL THEN
                var$qry_where := var$qry_where || ' AND CAMBIO.FYH_MODIFICACION >= TO_DATE('  ||  const$comilla_simple || TO_CHAR(par$fecha_desde, 'DD/MM/YYYY HH24:MI:SS') || const$comilla_simple || ', ''DD/MM/YYYY HH24:MI:SS'') ';
            END IF;
            /*Filtro fecha hasta*/
           IF par$fecha_hasta IS NOT NULL THEN
                var$qry_where := var$qry_where || ' AND CAMBIO.FYH_MODIFICACION <= TO_DATE(' || const$comilla_simple || TO_CHAR(par$fecha_hasta, 'DD/MM/YYYY HH24:MI:SS')  || const$comilla_simple || ', ''DD/MM/YYYY HH24:MI:SS'') ';
            END IF;
       /*Tipo de filtro por Fecha de Aprobación*/
       ELSIF par$cod_tipo_fecha = 'AP' THEN
            /*Filtro fecha desde*/
            IF par$fecha_desde IS NOT NULL THEN
                var$qry_where := var$qry_where || ' AND APROBACION.BOL_APROBADO = 1 AND APROBACION.FYH_APROBACION >= TO_DATE(' || const$comilla_simple || TO_CHAR(par$fecha_desde, 'DD/MM/YYYY HH24:MI:SS') || const$comilla_simple || ', ''DD/MM/YYYY HH24:MI:SS'') ';
            END IF;
            /*Filtro fecha hasta*/
           IF par$fecha_hasta IS NOT NULL THEN
                var$qry_where := var$qry_where || ' AND APROBACION.BOL_APROBADO = 1 AND APROBACION.FYH_APROBACION <= TO_DATE(' || const$comilla_simple ||  TO_CHAR(par$fecha_hasta, 'DD/MM/YYYY HH24:MI:SS') || const$comilla_simple || ', ''DD/MM/YYYY HH24:MI:SS'') ';
            END IF;
        END IF;
    END IF;
    /*Filtros de fecha - FIN*/


    IF var$bol_filtra_cliente = 1 THEN
        var$qry_filtro_cliente := var$qry_filtro_cliente || q'[ DATO_BANCARIO.OID_CLIENTE IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE  COD_CALIFICADOR = 'RP_OID_CLIENTE' ) ]' || const$new_line;
    END IF;


    IF var$bol_filtra_subcliente = 1 THEN
        IF var$qry_filtro_cliente IS NOT NULL THEN
            var$qry_filtro_cliente := var$qry_filtro_cliente || ' OR '  ;
        END IF; 

        var$qry_filtro_cliente := var$qry_filtro_cliente || q'[ DATO_BANCARIO.OID_SUBCLIENTE IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE  COD_CALIFICADOR = 'RP_OID_SUBCLIENTE' ) ]' || const$new_line;        
    END IF;


    IF var$bol_filtra_punto = 1 THEN
        IF var$qry_filtro_cliente IS NOT NULL THEN
            var$qry_filtro_cliente := var$qry_filtro_cliente || ' OR '  ;
        END IF; 

        var$qry_filtro_cliente := var$qry_filtro_cliente || q'[ DATO_BANCARIO.OID_PTO_SERVICIO IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE  COD_CALIFICADOR = 'RP_OID_PTO_SERVICIO' ) ]' || const$new_line;        
    END IF;


    IF var$qry_filtro_cliente IS NOT NULL THEN
        var$qry_filtro_cliente := ' AND ( '  || var$qry_filtro_cliente || ' ) '  ;
        var$qry_where := var$qry_where ||  var$qry_filtro_cliente;
    END IF; 

    var$qry_final := var$qry_insert || var$qry_select || var$qry_joins || var$qry_where;

    dbms_output.put_line('Recupera Datos - Imprimo query final:');
    dbms_output.put_line(var$qry_final);

    /*Ejecutamos la query final */
    EXECUTE IMMEDIATE var$qry_final;

dbms_output.put_line('Recupera Datos - Ejecutó query final');

   OPEN par$rc_cambios FOR
      WITH DATOS_CAMBIO AS (
 SELECT DTCA.OID_DATO_BANCARIO_CAMBIO,
        DTCA.OID_DATO_BANCARIO,
        DTCA.COD_CAMPO,
        DTCA.DES_VALOR,
        DTCA.OID_USUARIO,
        USUA.DES_LOGIN,
        USUA.DES_NOMBRE,
        USUA.DES_APELLIDO,
        DTCA.FYH_MODIFICACION,
        DTCA.COD_ESTADO,
        DTCA.NUM_APROBACIONES,
        var$cantidad_aprobaciones APROBACIONES_NECESARIAS
        FROM SAPR_TDATO_BANCARIO_CAMBIO DTCA
        LEFT JOIN ADPR_TUSUARIO USUA ON DTCA.OID_USUARIO = USUA.OID_USUARIO
        JOIN SAPR_GTT_TAUXILIAR DATO ON DATO.OID_CAMPO1 = DTCA.OID_DATO_BANCARIO_CAMBIO
        AND DATO.OID_CAMPO7 = DTCA.OID_DATO_BANCARIO AND  DATO.COD_CALIFICADOR = 'DATO_BANCARIO_CAMBIO'),
 DIVISA As (
   SELECT DTCA.OID_DATO_BANCARIO_CAMBIO, DIVI.COD_ISO_DIVISA || ' - ' || DIVI.DES_DIVISA AS DES_VALOR FROM GEPR_TDIVISA DIVI
   INNER JOIN DATOS_CAMBIO DTCA ON DTCA.COD_CAMPO = 'OID_DIVISA' AND DTCA.DES_VALOR = DIVI.OID_DIVISA
 ),
 BANCO AS (
   SELECT DTCA.OID_DATO_BANCARIO_CAMBIO, CLIE.COD_CLIENTE || ' - ' ||  CLIE.DES_CLIENTE AS DES_VALOR
   FROM GEPR_TCLIENTE CLIE
   INNER JOIN DATOS_CAMBIO DTCA ON DTCA.COD_CAMPO = 'OID_BANCO' AND DTCA.DES_VALOR = CLIE.OID_CLIENTE
 )
 SELECT
        DTCA.OID_DATO_BANCARIO_CAMBIO,
        DTCA.OID_DATO_BANCARIO,
        DTCA.COD_CAMPO,
        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    DTCA.COD_CAMPO,
                                                                    'DATOSBANCARIOS',
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                    NULL,
                                                                    0) CAMPO_TRADUCCION,

        NVL(NVL(DIVISA.DES_VALOR, BANCO.DES_VALOR ),DTCA.DES_VALOR) DES_VALOR,
        DTCA.OID_USUARIO,
        DTCA.DES_LOGIN,
        DTCA.DES_NOMBRE,
        DTCA.DES_APELLIDO,
        DTCA.FYH_MODIFICACION,
        DTCA.COD_ESTADO,
        DTCA.NUM_APROBACIONES,
        DTCA.APROBACIONES_NECESARIAS
 FROM DATOS_CAMBIO DTCA
 LEFT JOIN DIVISA ON DTCA.OID_DATO_BANCARIO_CAMBIO = DIVISA.OID_DATO_BANCARIO_CAMBIO
 LEFT JOIN BANCO ON DTCA.OID_DATO_BANCARIO_CAMBIO = BANCO.OID_DATO_BANCARIO_CAMBIO;

       OPEN par$rc_datos FOR
        WITH DATOS AS (
       SELECT DISTINCT DTAB.OID_DATO_BANCARIO,
       BANCO.COD_CLIENTE || ' - ' ||  BANCO.DES_CLIENTE AS BANCO,
       CLIE.COD_CLIENTE || ' - ' ||  CLIE.DES_CLIENTE AS CLIENTE,
       SCLIE.COD_SUBCLIENTE || ' - ' ||  SCLIE.DES_SUBCLIENTE AS SUBCLIENTE,
       PTO.COD_PTO_SERVICIO || ' - ' ||  PTO.DES_PTO_SERVICIO AS PUNTO,
       DIVI.COD_ISO_DIVISA || ' - ' || DIVI.DES_DIVISA AS DIVISA,
       DTAB.COD_TIPO_CUENTA_BANCARIA, DTAB.COD_CUENTA_BANCARIA, DTAB.COD_DOCUMENTO, DTAB.DES_TITULARIDAD,
        DTAB.DES_OBSERVACIONES, DTAB.BOL_DEFECTO, DTAB.BOL_ACTIVO, DTAB.COD_AGENCIA, DTAB.DES_CAMPO_ADICIONAL_1,DTAB.DES_CAMPO_ADICIONAL_2,DTAB.DES_CAMPO_ADICIONAL_3,DTAB.DES_CAMPO_ADICIONAL_4,
        DTAB.DES_CAMPO_ADICIONAL_5,DTAB.DES_CAMPO_ADICIONAL_6,DTAB.DES_CAMPO_ADICIONAL_7,DTAB.DES_CAMPO_ADICIONAL_8
        FROM SAPR_THIST_DATO_BANCARIO DTAB
        LEFT JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = DTAB.OID_DIVISA
        LEFT JOIN GEPR_TCLIENTE BANCO ON BANCO.OID_CLIENTE = DTAB.OID_BANCO
        LEFT JOIN GEPR_TCLIENTE CLIE ON CLIE.OID_CLIENTE = DTAB.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SCLIE ON SCLIE.OID_SUBCLIENTE = DTAB.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_PTO_SERVICIO = DTAB.OID_PTO_SERVICIO
        --JOIN SAPR_GTT_TAUXILIAR ON  SAPR_GTT_TAUXILIAR.OID_CAMPO7 = DTAB.OID_DATO_BANCARIO
        JOIN SAPR_TDATO_BANCARIO_CAMBIO CAM ON CAM.OID_HIST_DATO_BANCARIO = DTAB.OID_HIST_DATO_BANCARIO
        JOIN SAPR_GTT_TAUXILIAR ON SAPR_GTT_TAUXILIAR.OID_CAMPO1 = CAM.OID_DATO_BANCARIO_CAMBIO  AND  SAPR_GTT_TAUXILIAR.COD_CALIFICADOR = 'DATO_BANCARIO_CAMBIO'
        ),
        /*Filtro la tabla auxiliar para traer los OID_DATO_BANCARIO que tuvieron cambios*/
        CAMBIOS AS (
        SELECT DISTINCT OID_CAMPO7 OID_DATO_BANCARIO
        FROM SAPR_GTT_TAUXILIAR 
        WHERE COD_CALIFICADOR = 'DATO_BANCARIO_CAMBIO'
        ),
        /*Busco las cantidades de APROBADOS, RECHAZADOS Y PENDIENTES para ese DATO BANCARIO
        filtrando con un JOIN de la tabla temporal CAMBIOS
        */
        CANTIDADES AS (
            SELECT  DTCA.OID_DATO_BANCARIO, DTCA.COD_ESTADO,  COUNT(1) AS CANTIDAD
            FROM SAPR_TDATO_BANCARIO_CAMBIO DTCA
            INNER JOIN CAMBIOS CAM ON CAM.OID_DATO_BANCARIO = DTCA.OID_DATO_BANCARIO
            WHERE DTCA.BOL_ACTIVO = 1
            GROUP BY DTCA.OID_DATO_BANCARIO, DTCA.COD_ESTADO
        )
         SELECT DTAB.OID_DATO_BANCARIO,
        DTAB.BANCO,
        DTAB.CLIENTE,
        DTAB.SUBCLIENTE,
        DTAB.PUNTO,
        DTAB.DIVISA,
        DTAB.COD_TIPO_CUENTA_BANCARIA, DTAB.COD_CUENTA_BANCARIA, DTAB.COD_DOCUMENTO, DTAB.DES_TITULARIDAD,
            DTAB.DES_OBSERVACIONES, DTAB.BOL_DEFECTO, DTAB.BOL_ACTIVO, DTAB.COD_AGENCIA, DTAB.DES_CAMPO_ADICIONAL_1,DTAB.DES_CAMPO_ADICIONAL_2,DTAB.DES_CAMPO_ADICIONAL_3,DTAB.DES_CAMPO_ADICIONAL_4,
            DTAB.DES_CAMPO_ADICIONAL_5,DTAB.DES_CAMPO_ADICIONAL_6,DTAB.DES_CAMPO_ADICIONAL_7,DTAB.DES_CAMPO_ADICIONAL_8,
            NVL(CANT_APROB.CANTIDAD, 0)  AS CANT_APROBADOS,
            NVL(CANT_RECHA.CANTIDAD, 0) AS CANT_RECHAZADOS,
            NVL(CANT_PENDI.CANTIDAD, 0) AS CANT_PENDIENTES
            FROM DATOS DTAB
            LEFT JOIN CANTIDADES CANT_APROB ON DTAB.OID_DATO_BANCARIO = CANT_APROB.OID_DATO_BANCARIO AND CANT_APROB.COD_ESTADO = 'AP'
            LEFT JOIN CANTIDADES CANT_RECHA ON DTAB.OID_DATO_BANCARIO = CANT_RECHA.OID_DATO_BANCARIO AND CANT_RECHA.COD_ESTADO = 'RE'
            LEFT JOIN CANTIDADES CANT_PENDI ON DTAB.OID_DATO_BANCARIO = CANT_PENDI.OID_DATO_BANCARIO AND CANT_PENDI.COD_ESTADO = 'PD'
            ORDER BY DTAB.CLIENTE, DTAB.SUBCLIENTE, DTAB.PUNTO, DTAB.DIVISA
            ;        


      OPEN par$rc_aprob FOR
      SELECT OID_DATO_BANCARIO_CAMBIO,FYH_APROBACION, USUA.DES_LOGIN AS LOGIN,
       USUA.DES_NOMBRE || ' '|| USUA.DES_APELLIDO AS USUARIO
      FROM SAPR_TDATO_BANCARIO_APROBACION DTAPROB
      LEFT JOIN ADPR_TUSUARIO USUA ON DTAPROB.USUARIO_APROBACION = USUA.OID_USUARIO
      JOIN SAPR_GTT_TAUXILIAR ON SAPR_GTT_TAUXILIAR.OID_CAMPO1 = DTAPROB.OID_DATO_BANCARIO_CAMBIO
      WHERE BOL_APROBADO = 1;

  END srecuperar_datos;

  /* saprobar_rechazar: Se encarga de aprobar o rechazar un cambio 
  Genera un registro en SAPR_TDATO_BANCARIO_APROBACION 
  Y actualiza SAPR_TDATO_BANCARIO_CAMBIO*/
   PROCEDURE saprobar_rechazar(
                            par$aoid_dato_banc_cambio  IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$cod_accion        IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$des_comentario    IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$tester_aprobacion IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_pais          IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$info_ejecucion    IN VARCHAR2,
                            par$rc_validaciones   OUT sys_refcursor,
                            par$cod_ejecucion     OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
                const$cod_parametro_aprob CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'CantidadAprobacionesCuentasBancarias';
                var$aprobaciones_necesarias gepr_pcomon_###VERSION###.tipo$nel_;
                var$aprobaciones_actuales gepr_pcomon_###VERSION###.tipo$nel_;
                var$oid_dato_bancario gepr_pcomon_###VERSION###.tipo$oid_;
                var$oid_dato_banc_cam gepr_pcomon_###VERSION###.tipo$oid_;
                var$cod_campo gepr_pcomon_###VERSION###.tipo$cod_;
                var$des_campo gepr_pcomon_###VERSION###.tipo$desc_;
                var$oid_usuario gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
   /*Limpio la tabla auxiliar*/
   DELETE SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR = 'APROBACIONES_DATO_BANCO';
   COMMIT;
   /*Inicializar cursor validaciones*/
   OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    BEGIN
        SELECT 
            OID_USUARIO
        INTO
            var$oid_usuario
        FROM adpr_tusuario 
        WHERE TRIM(UPPER(des_login)) = TRIM(UPPER(par$cod_usuario));
    EXCEPTION
        WHEN OTHERS THEN
            var$oid_usuario := null;
    END;

    IF par$tester_aprobacion IS NOT NULL AND par$tester_aprobacion = 0 THEN
        svalidar_dato_banc_aprov(
                        par$cod_accion                  => par$cod_accion,
                        par$aoid_dato_banc_cambio       => par$aoid_dato_banc_cambio,
                        par$cod_cultura                 => par$cod_cultura,
                        par$oid_usuario                 => var$oid_usuario);
    END IF;

   /*Recorro los oid_dato_bancario_cambio*/
   FOR idx IN par$aoid_dato_banc_cambio.first .. par$aoid_dato_banc_cambio.last LOOP
        begin
            var$oid_dato_banc_cam := par$aoid_dato_banc_cambio(idx);

            IF par$cod_accion = 'APROBADO' and  not fhay_error_aprovacion(var$oid_dato_banc_cam) THEN
                /* Obtengo parametro cantidad de aprobaciones necesarias */
                 

                var$aprobaciones_necesarias := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => NULL,
                                                                                    par$cod_pais => par$cod_pais,
                                                                                    par$cod_parametro => const$cod_parametro_aprob,
                                                                                    par$cod_aplicacion => 'Genesis');
                IF var$aprobaciones_necesarias IS NULL THEN
                        var$aprobaciones_necesarias := 0;
                END IF;
                

                /*Obtengo numero de aprobaciones de este campo*/
                begin
                SELECT NUM_APROBACIONES 
                INTO var$aprobaciones_actuales
                FROM SAPR_TDATO_BANCARIO_CAMBIO
                WHERE OID_DATO_BANCARIO_CAMBIO = var$oid_dato_banc_cam;
                end;
                /*Insert en dato bancario aprobacion (aprobado)*/
                sins_dato_banc_aprobacion(var$oid_dato_banc_cam, var$oid_usuario, par$des_comentario, 1);

                /*Si tengo todas las aprobaciones*/
                DBMS_OUTPUT.PUT_LINE('var$aprobaciones_necesarias: ' || var$aprobaciones_necesarias);
                 DBMS_OUTPUT.PUT_LINE('var$aprobaciones_actuales: ' || var$aprobaciones_actuales);
                 DBMS_OUTPUT.PUT_LINE('var$aprobaciones_actuales + 1: ' || (var$aprobaciones_actuales + 1));
                IF ((var$aprobaciones_actuales + 1) >= var$aprobaciones_necesarias) THEN
                  
                    /*Aumento en 1 el numero de aprobaciones y pongo el cambio como aprobado*/
                    supd_dato_bancario_cambio(var$oid_dato_banc_cam,NULL,NULL,NULL,NULL,var$oid_usuario,(var$aprobaciones_actuales + 1),'AP',NULL);

                    /*Obtengo el oid del dato bancario, el codigo de campo y su valor*/
                    SELECT OID_DATO_BANCARIO, COD_CAMPO, DES_VALOR
                    INTO var$oid_dato_bancario, var$cod_campo, var$des_campo FROM SAPR_TDATO_BANCARIO_CAMBIO
                    WHERE OID_DATO_BANCARIO_CAMBIO = var$oid_dato_banc_cam;

                    IF var$des_campo is null or var$des_campo = '' THEN
                        var$des_campo := const$vacio;
                    END IF;

                    /*Actualizo el campo correspondiente en SAPR_TDATO_BANCARIO (ver mejor forma de hacerlo y que hacer en caso de alta y baja)*/

                    IF  var$cod_campo = 'OID_BANCO'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, var$des_campo, null, null, null, null, null, null,null,null,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'OID_CLIENTE'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null , var$des_campo, null, null, null, null, null,null,null,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'OID_SUBCLIENTE'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, var$des_campo, null, null, null, null,null,null,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'OID_PTO_SERVICIO'  THEN
                        supd_dato_bancario(var$oid_dato_bancario,null, null, null, var$des_campo, null, null, null,null,null,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'OID_DIVISA'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, var$des_campo, null, null,null,null,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'COD_TIPO_CUENTA_BANCARIA'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, var$des_campo, null,null,null,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'COD_CUENTA_BANCARIA'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, var$des_campo,null,null,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'COD_DOCUMENTO'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,var$des_campo,null,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_TITULARIDAD'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,var$des_campo,null,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_OBSERVACIONES'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,var$des_campo,null,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'BOL_DEFECTO'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,var$des_campo,null,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'BOL_ACTIVO'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,var$des_campo,null,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'COD_AGENCIA'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,var$des_campo,null,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_CAMPO_ADICIONAL_1'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,null,var$des_campo,null,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_CAMPO_ADICIONAL_2'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,null,null,var$des_campo,null,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_CAMPO_ADICIONAL_3'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,null,null,null,var$des_campo,null,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_CAMPO_ADICIONAL_4'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,null,null,null,null,var$des_campo,null,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_CAMPO_ADICIONAL_5'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,null,null,null,null,null,var$des_campo,null,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_CAMPO_ADICIONAL_6'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,null,null,null,null,null,null,var$des_campo,null,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_CAMPO_ADICIONAL_7'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,null,null,null,null,null,null,null,var$des_campo,null, var$oid_usuario);
                    END IF;

                    IF  var$cod_campo = 'DES_CAMPO_ADICIONAL_8'  THEN
                        supd_dato_bancario(var$oid_dato_bancario, null, null, null, null, null, null, null,null,null,null,null,null,null,null,null,null,null,null,null,null,var$des_campo, var$oid_usuario);
                    END IF;

                ELSE
                    /*Si las aprobaciones no alcanzan, solo sumo una aprobacion en SAPR_TDATO_BANCARIO_CAMBIO*/
                    supd_dato_bancario_cambio(var$oid_dato_banc_cam,NULL,NULL,NULL,NULL,var$oid_usuario,(var$aprobaciones_actuales + 1),NULL,NULL);
                END IF;

            ELSE
                /*Caso Rechazar, Inserto en SAPR_TDATO_BANCARIO_APROBACION y paso a RECHAZADO el campo en SAPR_TDATO_BANCARIO_CAMBIO*/
                sins_dato_banc_aprobacion(var$oid_dato_banc_cam, var$oid_usuario, par$des_comentario, 0);
                supd_dato_bancario_cambio(var$oid_dato_banc_cam,NULL,NULL,NULL,NULL,var$oid_usuario,NULL,'RE',NULL);


            END IF;
            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, OID_CAMPO7,COD_CALIFICADOR)
                VALUES (
                    '0040080000', 
                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'0040080000', 
                        gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente, 
                        gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                        NULL, 
                        0),                                 
                    var$oid_dato_banc_cam,
                    'APROBACIONES_DATO_BANCO');
                    COMMIT;

          exception
            when others then
                ROLLBACK;
               INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, OID_CAMPO7,COD_CALIFICADOR)
                        VALUES (
                            'lblErrorAprobarRechazar', 
                            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'lblErrorAprobarRechazar', 
                                'APROBACION_CUENTAS_BANCARIAS', 
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                NULL, 
                                0),                                 
                            var$oid_dato_banc_cam,
                            'APROBACIONES_DATO_BANCO');
                       COMMIT;
        end;
    END LOOP;

    OPEN par$rc_validaciones FOR
    SELECT OID_CAMPO1 AS CODIGO,    
          COD_CAMPO2 AS DESCRIPCION,
          OID_CAMPO7 AS OID_DATO_BANCARIO_CAMBIO,
          COD_CALIFICADOR AS CALIFICADOR 
          FROM SAPR_GTT_TAUXILIAR AUX
    WHERE AUX.COD_CALIFICADOR = 'APROBACIONES_DATO_BANCO'
    UNION ALL 
     SELECT COD_CAMPO4 AS CODIGO,    
          COD_CAMPO3 AS DESCRIPCION,
          OID_CAMPO1 AS OID_DATO_BANCARIO_CAMBIO,
          'APROBACIONES_DATO_BANCO' AS CALIFICADOR 
          FROM SAPR_GTT_TAUXILIAR AUX
    WHERE AUX.COD_CALIFICADOR = 'VALIDACIONES_APROBACION'
    ;

  END saprobar_rechazar;


  /* Se encarga de grabar los registros historicos de los datos bancarios.
  Inserta un nuevo registro en la tabla SAPR_THIST_DATO_BANCARIO */
  PROCEDURE sins_hist_dato_bancario(
    par$oid_dato_bancario IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$bol_hacer_commit IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$oid_hist_dato_bancario OUT gepr_pcomon_###VERSION###.tipo$oid_
  )
  IS
    /* Variables locales */
    var$oid_dato_bancario   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_hist_dato_bancario gepr_pcomon_###VERSION###.tipo$oid_;
    var$gmt_zero    gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$des_login   gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN
    BEGIN
        SELECT
            OID_DATO_BANCARIO
        INTO
            var$oid_dato_bancario
        FROM
            SAPR_TDATO_BANCARIO
        WHERE
            OID_DATO_BANCARIO = par$oid_dato_bancario;
    EXCEPTION
    WHEN no_data_found THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'El OID_DATO_BANCARIO pasado por parametro en sins_hist_dato_bancario no existe');
        var$oid_dato_bancario := NULL;
    END;

        BEGIN
        SELECT
            DES_LOGIN
        INTO
            var$des_login
        FROM
            ADPR_TUSUARIO
        WHERE
            UPPER(TRIM(DES_LOGIN)) = UPPER(TRIM(par$cod_usuario));
    EXCEPTION
    WHEN no_data_found THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, 'El CODIGO_USUARIO (' || par$cod_usuario || ') informado por parametro no existe');
        var$des_login := NULL;
    END;

    var$oid_hist_dato_bancario := sys_guid();
    INSERT INTO SAPR_THIST_DATO_BANCARIO
    (
        OID_HIST_DATO_BANCARIO, OID_DATO_BANCARIO, OID_BANCO, OID_CLIENTE, OID_SUBCLIENTE, OID_PTO_SERVICIO,
        OID_DIVISA, COD_TIPO_CUENTA_BANCARIA, COD_CUENTA_BANCARIA, COD_DOCUMENTO, DES_TITULARIDAD, DES_OBSERVACIONES,
        BOL_DEFECTO, BOL_ACTIVO, COD_AGENCIA, DES_CAMPO_ADICIONAL_1, DES_CAMPO_ADICIONAL_2, DES_CAMPO_ADICIONAL_3,
        DES_CAMPO_ADICIONAL_4, DES_CAMPO_ADICIONAL_5, DES_CAMPO_ADICIONAL_6, DES_CAMPO_ADICIONAL_7, DES_CAMPO_ADICIONAL_8, GMT_CREACION,
        DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
    )
    SELECT
        var$oid_hist_dato_bancario, OID_DATO_BANCARIO, OID_BANCO, OID_CLIENTE, OID_SUBCLIENTE, OID_PTO_SERVICIO,
        OID_DIVISA, COD_TIPO_CUENTA_BANCARIA, COD_CUENTA_BANCARIA, COD_DOCUMENTO, DES_TITULARIDAD, DES_OBSERVACIONES,
        BOL_DEFECTO, BOL_ACTIVO, COD_AGENCIA, DES_CAMPO_ADICIONAL_1, DES_CAMPO_ADICIONAL_2, DES_CAMPO_ADICIONAL_3,
        DES_CAMPO_ADICIONAL_4, DES_CAMPO_ADICIONAL_5, DES_CAMPO_ADICIONAL_6, DES_CAMPO_ADICIONAL_7, DES_CAMPO_ADICIONAL_8, var$gmt_zero,
        var$des_login, var$gmt_zero, var$des_login
    FROM
        SAPR_TDATO_BANCARIO
    WHERE
        OID_DATO_BANCARIO = var$oid_dato_bancario;

    IF par$bol_hacer_commit = 1 THEN
      COMMIT;
    END IF;

    par$oid_hist_dato_bancario := var$oid_hist_dato_bancario;

  END sins_hist_dato_bancario;

    /* Se encarga de recuperar los datos para mostrar en el popup
  comparativo de datos bacnarios */
  PROCEDURE srecuperar_comparativo( 
                            par$oid_dato_bancario          IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$rc_datos_bancarios          OUT sys_refcursor,
                            par$rc_datos_bancarios_cambio   OUT sys_refcursor)
  IS
  BEGIN
  /*Inicializar cursor validaciones*/
   OPEN par$rc_datos_bancarios FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
   /*Inicializar cursor validaciones*/
   OPEN par$rc_datos_bancarios_cambio FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

    /*Poblar cursor de salida */
    OPEN par$rc_datos_bancarios FOR
        SELECT
        DATB.OID_DATO_BANCARIO,
        B.COD_CLIENTE || ' - ' ||  B.DES_CLIENTE AS DES_BANCO,
        B.COD_BANCARIO,
        DATB.COD_DOCUMENTO,
        DATB.COD_AGENCIA,
        D.COD_ISO_DIVISA || ' - ' || D.DES_DIVISA AS DES_DIVISA,
        DATB.DES_OBSERVACIONES,
        DATB.BOL_DEFECTO,
        DATB.DES_TITULARIDAD,
        DATB.COD_TIPO_CUENTA_BANCARIA,
        DATB.COD_CUENTA_BANCARIA,
        DATB.DES_CAMPO_ADICIONAL_1,
        DATB.DES_CAMPO_ADICIONAL_2,
        DATB.DES_CAMPO_ADICIONAL_3,
        DATB.DES_CAMPO_ADICIONAL_4,
        DATB.DES_CAMPO_ADICIONAL_5,
        DATB.DES_CAMPO_ADICIONAL_6,
        DATB.DES_CAMPO_ADICIONAL_7,
        DATB.DES_CAMPO_ADICIONAL_8
        FROM  SAPR_TDATO_BANCARIO DATB
        LEFT JOIN GEPR_TCLIENTE B ON B.OID_CLIENTE = DATB.OID_BANCO
        LEFT JOIN GEPR_TDIVISA D ON D.OID_DIVISA = DATB.OID_DIVISA
        WHERE DATB.OID_DATO_BANCARIO = par$oid_dato_bancario;

    /*Poblar cursor de salida */
    OPEN par$rc_datos_bancarios_cambio FOR
      WITH DATOS_CAMBIO AS (
        SELECT  DTCA.OID_DATO_BANCARIO_CAMBIO,
                DTCA.OID_DATO_BANCARIO,
                DTCA.COD_CAMPO,
                DTCA.DES_VALOR
                FROM SAPR_TDATO_BANCARIO_CAMBIO DTCA
                WHERE DTCA.COD_ESTADO='PD' AND DTCA.BOL_ACTIVO=1 AND DTCA.OID_DATO_BANCARIO = par$oid_dato_bancario),
        DIVISA As (
        SELECT DTCA.OID_DATO_BANCARIO_CAMBIO, DIVI.COD_ISO_DIVISA || ' - ' || DIVI.DES_DIVISA AS DES_VALOR FROM GEPR_TDIVISA DIVI
        INNER JOIN DATOS_CAMBIO DTCA ON DTCA.COD_CAMPO = 'OID_DIVISA' AND DTCA.DES_VALOR = DIVI.OID_DIVISA
        ),
        BANCO AS (
        SELECT DTCA.OID_DATO_BANCARIO_CAMBIO, CLIE.COD_CLIENTE || ' - ' ||  CLIE.DES_CLIENTE AS DES_VALOR,
        CLIE.COD_BANCARIO
        FROM GEPR_TCLIENTE CLIE
        INNER JOIN DATOS_CAMBIO DTCA ON DTCA.COD_CAMPO = 'OID_BANCO' AND DTCA.DES_VALOR = CLIE.OID_CLIENTE
        )

        SELECT
                DTCA.OID_DATO_BANCARIO_CAMBIO,
                DTCA.OID_DATO_BANCARIO,
                DTCA.COD_CAMPO,
                NVL(NVL(DIVISA.DES_VALOR, BANCO.DES_VALOR ),DTCA.DES_VALOR) DES_VALOR,
                BANCO.COD_BANCARIO
        FROM DATOS_CAMBIO DTCA
        LEFT JOIN DIVISA ON DTCA.OID_DATO_BANCARIO_CAMBIO = DIVISA.OID_DATO_BANCARIO_CAMBIO
        LEFT JOIN BANCO ON DTCA.OID_DATO_BANCARIO_CAMBIO = BANCO.OID_DATO_BANCARIO_CAMBIO;
    END srecuperar_comparativo;



 /* Validar si existe error con el APROVACIO*/
  FUNCTION fhay_error_aprovacion(par$oid_dato_bancario IN gepr_pcomon_###VERSION###.tipo$oid_) RETURN BOOLEAN IS
    var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    BEGIN

      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR AUX
        INNER JOIN SAPR_TDATO_BANCARIO_CAMBIO DBC ON DBC.OID_DATO_BANCARIO = AUX.COD_CAMPO2        
       WHERE COD_CALIFICADOR = 'VALIDACIONES_APROBACION'  
         AND OID_CAMPO1 = par$oid_dato_bancario;

    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;

    RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);

  END fhay_error_aprovacion;


 PROCEDURE svalidar_dato_banc_aprov(
                        par$cod_accion                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$aoid_dato_banc_cambio       IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$cod_cultura                 IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$oid_usuario                 IN gepr_pcomon_###VERSION###.tipo$oid_ ) IS

    var$existe                      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$oid_dato_bancario           gepr_pcomon_###VERSION###.tipo$oid_;
 BEGIN

  FOR idx IN par$aoid_dato_banc_cambio.first .. par$aoid_dato_banc_cambio.last LOOP
    IF par$cod_accion = 'APROBADO' and not fhay_error_aprovacion(par$aoid_dato_banc_cambio(idx)) THEN  
        BEGIN
            SELECT COUNT(1) 
                INTO var$existe
                FROM SAPR_TDATO_BANCARIO_APROBACION
                WHERE USUARIO_APROBACION = par$oid_usuario AND OID_DATO_BANCARIO_CAMBIO = par$aoid_dato_banc_cambio(idx) AND ROWNUM = 1;
        EXCEPTION WHEN no_data_found THEN
            var$existe := 0;
        END;

        IF  var$existe = 1 THEN
            select var$oid_dato_bancario INTO var$oid_dato_bancario
            FROM SAPR_TDATO_BANCARIO_CAMBIO WHERE OID_DATO_BANCARIO_CAMBIO = par$aoid_dato_banc_cambio(idx);

            INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
            VALUES (par$aoid_dato_banc_cambio(idx), var$oid_dato_bancario,

             gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'lblErrorAprobador', 
                                'APROBACION_CUENTAS_BANCARIAS', 
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                NULL, 
                                0)
                                , 'lblErrorAprobarRechazar', 'VALIDACIONES_APROBACION');
        END IF;

        BEGIN
            SELECT COUNT(1) 
                INTO var$existe
                FROM SAPR_TDATO_BANCARIO_CAMBIO
                WHERE OID_USUARIO = par$oid_usuario AND OID_DATO_BANCARIO_CAMBIO = par$aoid_dato_banc_cambio(idx);
        EXCEPTION WHEN no_data_found THEN
            var$existe := 0;
        END;

        IF  var$existe = 1 THEN
            select var$oid_dato_bancario INTO var$oid_dato_bancario
            FROM SAPR_TDATO_BANCARIO_CAMBIO WHERE OID_DATO_BANCARIO_CAMBIO = par$aoid_dato_banc_cambio(idx);

             INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CAMPO3,COD_CAMPO4, COD_CALIFICADOR)
            VALUES (par$aoid_dato_banc_cambio(idx), var$oid_dato_bancario,

               gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'lblErrorAprobador', 
                                'APROBACION_CUENTAS_BANCARIAS', 
                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis, 
                                NULL, 
                                0), 
                                'lblErrorAprobarRechazar', 
                                'VALIDACIONES_APROBACION');
        END IF;
    END IF;
  END LOOP;    
 END svalidar_dato_banc_aprov;


  /* Inserta registros en la tabla SAPR_TDATO_BANCARIO_COMENTARIO */ 
  PROCEDURE sins_dato_banc_comentario(
    par$oid_dato_banc_cambio    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_,
    par$des_tabla   IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$oid_tabla   IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$des_comentario  IN gepr_pcomon_###VERSION###.tipo$desc_,
    par$fyh_comentario IN gepr_pcomon_###VERSION###.tipo$fyh_,
    par$bol_commit  IN gepr_pcomon_###VERSION###.tipo$nel_,
    par$oid_identificador OUT gepr_pcomon_###VERSION###.tipo$oid_)
IS
    /* variables locales */
    var$gmt_zero            gepr_pcomon_###VERSION###.tipo$cod_ := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_usuario   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_identificador gepr_pcomon_###VERSION###.tipo$oid_;
BEGIN
    var$oid_identificador := '';
    BEGIN
        SELECT
            OID_USUARIO
        INTO
            var$oid_usuario
        FROM
            ADPR_TUSUARIO
        WHERE
            trim(upper(DES_LOGIN)) = trim(upper(par$cod_usuario));
    EXCEPTION
        when others then
            var$oid_usuario := '';
    END;

     IF var$oid_usuario <> '' OR var$oid_usuario is not null THEN

        var$oid_identificador := sys_guid();

        INSERT INTO SAPR_TDATO_BANCARIO_COMENTARIO
        (
            OID_DATO_BANCARIO_COMENTARIO, OID_DATO_BANCARIO_CAMBIO, DES_TABLA, 
            OID_TABLA, DES_COMENTARIO, FYH_COMENTARIO, OID_USUARIO, 
            GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION, DES_USUARIO_MODIFICACION
        )
        VALUES
        (
            var$oid_identificador, par$oid_dato_banc_cambio, par$des_tabla,
            par$oid_tabla, par$des_comentario, CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), var$oid_usuario,
            var$gmt_zero, par$cod_usuario, var$gmt_zero, par$cod_usuario
        );

        IF par$bol_commit = 1 THEN
            COMMIT;
        END IF;
    END IF;

    par$oid_identificador := var$oid_identificador;
END sins_dato_banc_comentario;



 /* Recupera los comentarios realizados a un cambio de un dato bancario tanto de las aprobaciones rechazo, del cambio o cualquier comentario realizado al mismo */
 PROCEDURE srecuperar_comentarios
(
    par$oid_dato_banc_cambio    IN gepr_pcomon_###VERSION###.tipo$oid_,
    par$rc_comentarios_aprob  OUT sys_refcursor,
    par$rc_comentarios_modif  OUT sys_refcursor
)
IS

BEGIN

  /*Inicializar cursor validaciones*/
  OPEN par$rc_comentarios_aprob FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
  OPEN par$rc_comentarios_modif FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

  OPEN par$rc_comentarios_aprob FOR
  WITH aprobados
        AS (SELECT usu.des_login DES_LOGIN,
                   usu.des_apellido DES_APELLIDO,
                   usu.des_nombre DES_NOMBRE,
                   apr.fyh_aprobacion FYH_APROBACION,
                   apr.bol_aprobado BOL_APROBADO,
                   apr.des_comentario DES_COMENTARIO
              FROM    sapr_tdato_bancario_aprobacion apr
                   INNER JOIN
                      adpr_tusuario usu
                   ON usu.oid_usuario = apr.usuario_aprobacion
             WHERE apr.oid_dato_bancario_cambio = par$oid_dato_banc_cambio
            UNION
            SELECT usu.des_login DES_LOGIN,
                   usu.des_apellido DES_APELLIDO,
                   usu.des_nombre DES_NOMBRE,
                   comentario.fyh_comentario FYH_APROBACION,
                   2 BOL_APROBADO,
                   comentario.des_comentario DES_COMENTARIO
              FROM    sapr_Tdato_bancario_comentario comentario
                   INNER JOIN
                      adpr_tusuario usu
                   ON usu.oid_usuario = comentario.oid_usuario
             WHERE comentario.oid_dato_bancario_cambio = par$oid_dato_banc_cambio)
  SELECT apr.des_login,
         apr.des_apellido,
         apr.des_nombre,
         apr.fyh_aprobacion,
         apr.bol_aprobado,
         apr.des_comentario
    FROM aprobados apr
  ORDER BY apr.FYH_APROBACION DESC;

  OPEN par$rc_comentarios_modif FOR
  WITH modificacion
        AS (SELECT usu.des_login,
                   usu.des_apellido,
                   usu.des_nombre,
                   cambio.fyh_modificacion,
                   cambio.des_comentario
              FROM    sapr_tdato_bancario_cambio cambio
                   INNER JOIN
                      adpr_tusuario usu
                   ON usu.oid_usuario = cambio.oid_usuario
             WHERE cambio.oid_dato_bancario_cambio = par$oid_dato_banc_cambio)
  SELECT modif.des_login,
        modif.des_apellido,
        modif.des_nombre,
        modif.fyh_modificacion,
        modif.des_comentario
    FROM modificacion modif
  ORDER BY MODIF.FYH_MODIFICACION DESC;

END srecuperar_comentarios;

END SAPR_PDATO_BANCARIO_###VERSION###;
/