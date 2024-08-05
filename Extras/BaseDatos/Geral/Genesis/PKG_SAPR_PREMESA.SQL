CREATE OR REPLACE PACKAGE SAPR_PREMESA_###VERSION###
    AS
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PREMESA_###VERSION###';

  /* sgrabar_remesa */
  PROCEDURE sgrabar_remesa(par$oid_remesa            IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_remesa_origen     IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_externo           IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_iac               IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_documento         IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_cuenta            IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$cod_recibo_salida     IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_estado            IN gepr_pcomon_###VERSION###.tipo$est_,
                           par$usuario_resp          IN gepr_pcomon_###VERSION###.tipo$usr_,
                           par$puesto_resp           IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_ruta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$nel_parada            IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$fyh_transporte        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$nel_cantidad_bultos   IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$fyh_conteo_inicio     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$fyh_conteo_fin        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$oid_remesa_padre      IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_remesa_substituta IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$cod_cajero            IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_nivel_detalle     IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_externo           IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$bol_gestion_bulto     IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$cod_estado_abono      IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$oid_cuenta_saldo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_punto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$cod_delegacion        IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                           par$cod_cultura           IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tremesa */
  PROCEDURE sins_tremesa(par$oid_remesa            IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_remesa_origen     IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_externo           IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_iac               IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_documento         IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_cuenta            IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_recibo_salida     IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_estado            IN gepr_pcomon_###VERSION###.tipo$est_,
                         par$usuario_resp          IN gepr_pcomon_###VERSION###.tipo$usr_,
                         par$puesto_resp           IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_ruta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$nel_parada            IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$fyh_transporte        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                         par$nel_cantidad_bultos   IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$fyh_conteo_inicio     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                         par$fyh_conteo_fin        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                         par$oid_remesa_padre      IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_remesa_substituta IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_cajero            IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_nivel_detalle     IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_externo           IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$bol_gestion_bulto     IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$cod_estado_abono      IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$oid_cuenta_saldo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                         par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* srecuperar_remesas */
  PROCEDURE srecuperar_remesas(par$oids_remesa                IN gepr_pcomon_###VERSION###.tipo$oids_
                              ,par$oids_documentos            IN gepr_pcomon_###VERSION###.tipo$oids_
                              ,par$ele_rc_elementos          OUT sys_refcursor
                              ,par$ele_rc_cuentas            OUT sys_refcursor
                              ,par$ele_rc_carac_tipo_sector  OUT sys_refcursor
                              ,par$ele_rc_val_det_efectivo   OUT sys_refcursor
                              ,par$ele_rc_val_det_medio_pago OUT sys_refcursor
                              ,par$ele_rc_val_totales        OUT sys_refcursor
                              ,par$ele_rc_lista_valor        OUT sys_refcursor
                              ,par$ele_rc_iac                OUT sys_refcursor
                              ,par$ele_rc_terminos_iac       OUT sys_refcursor
                              ,par$ele_rc_valor_termino_iac  OUT sys_refcursor
                              ,par$ele_rc_divisas            OUT sys_refcursor
                              ,par$ele_rc_denominaciones     OUT sys_refcursor
                              ,par$ele_rc_medio_pago         OUT sys_refcursor
                              ,par$ele_rc_unidad_medida      OUT sys_refcursor
                              ,par$ele_rc_calidad            OUT sys_refcursor
                              ,par$ele_rc_valor_termino      OUT sys_refcursor
                              ,par$ele_rc_cont_precintos     OUT sys_refcursor
                              ,par$ejecucion_interna          IN INTEGER
                              ,par$usuario                    IN gepr_pcomon_###VERSION###.tipo$usr_
                              ,par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_
                              ,par$info_ejecucion             IN VARCHAR2
                              ,par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_
                              ,par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_
                              ,par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_iac_elem */
  PROCEDURE scargar_tt_oids_iac_elem;

  /* fcargar_rc_lista_valor */
  FUNCTION fcargar_rc_lista_valor
    RETURN SYS_REFCURSOR;

  /* fcargar_rc_elementos */
  FUNCTION fcargar_rc_elementos
    RETURN SYS_REFCURSOR;

  /* fcargar_rc_valores_det_efec */
  FUNCTION fcargar_rc_valores_det_efec
    RETURN SYS_REFCURSOR;

  /* fcargar_rc_valores_det_mp */
  FUNCTION fcargar_rc_valores_det_mp
    RETURN SYS_REFCURSOR;

  /* fcargar_rc_valores_totales */
  FUNCTION fcargar_rc_valores_totales
    RETURN SYS_REFCURSOR;

  /* fcargar_rc_iac */
  FUNCTION fcargar_rc_iac
    RETURN SYS_REFCURSOR;

  /* fcargar_rc_terminos_iac */
  FUNCTION fcargar_rc_terminos_iac
    RETURN SYS_REFCURSOR;

  /* fcargar_rc_valor_termino_iac */
  FUNCTION fcargar_rc_valor_termino_iac
    RETURN SYS_REFCURSOR;

  /* fvalidar_remesa */
  FUNCTION fvalidar_remesa(par$oid_documento      IN gepr_pcomon_###VERSION###.tipo$oid_
                          ,par$cod_estado         IN gepr_pcomon_###VERSION###.tipo$est_
                          ,par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_
                          ,par$cod_cajero         IN gepr_pcomon_###VERSION###.tipo$cod_
                          ,par$oid_punto_servicio IN gepr_pcomon_###VERSION###.tipo$oid_
                          ,par$cod_delegacion     IN gepr_pcomon_###VERSION###.tipo$cod_
                          ,par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  /* Validar estado de la remesa */
  FUNCTION fvalidar_estado_remesa(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_
                                 ,par$cod_estado IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  PROCEDURE sexcluir_remesa(par$oid_documento                IN gepr_pcomon_###VERSION###.tipo$oid_
                            ,par$oid_remesa                  IN gepr_pcomon_###VERSION###.tipo$oid_
                            ,par$abul_oid_bulto              IN gepr_pcomon_###VERSION###.tipo$oids_
                            ,par$abul_oid_documento          IN gepr_pcomon_###VERSION###.tipo$oids_
                            ,par$abul_oid_tipo_formato       IN gepr_pcomon_###VERSION###.tipo$oids_
                            ,par$abul_oid_tipo_servicio      IN gepr_pcomon_###VERSION###.tipo$oids_
                            ,par$apar_oid_parcial            IN gepr_pcomon_###VERSION###.tipo$oids_
                            ,par$apar_oid_bulto              IN gepr_pcomon_###VERSION###.tipo$oids_
                            ,par$apar_oid_oid_tipo_formato   IN gepr_pcomon_###VERSION###.tipo$oids_);

  PROCEDURE sdel_tremesa(par$oid_remesa                  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$deletes       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE supd_tremesa(par$oid_remesa       in gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_documento    in gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_cuenta_mov   in gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_cuenta_saldo in gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_usuario      in gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_estado       in gepr_pcomon_###VERSION###.tipo$cod_,
                         par$updates         out gepr_pcomon_###VERSION###.tipo$nel_);

  
  procedure supd_tremesa_rowver(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$rowver   IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_cultura      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_funcionalidad in gepr_pcomon_###VERSION###.tipo$desc_);	

END SAPR_PREMESA_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PREMESA_###VERSION### AS

  /* sgrabar_remesa */
  PROCEDURE sgrabar_remesa(par$oid_remesa            IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_remesa_origen     IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_externo           IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_iac               IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_documento         IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_cuenta            IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$cod_recibo_salida     IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_estado            IN gepr_pcomon_###VERSION###.tipo$est_,
                           par$usuario_resp          IN gepr_pcomon_###VERSION###.tipo$usr_,
                           par$puesto_resp           IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_ruta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$nel_parada            IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$fyh_transporte        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$nel_cantidad_bultos   IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$fyh_conteo_inicio     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$fyh_conteo_fin        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                           par$oid_remesa_padre      IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_remesa_substituta IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$cod_cajero            IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_nivel_detalle     IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_externo           IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$bol_gestion_bulto     IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$cod_estado_abono      IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$oid_cuenta_saldo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_punto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$cod_delegacion        IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                           par$cod_cultura           IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$inserts               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    IF fvalidar_remesa(par$oid_documento,
                       par$cod_estado,
                       par$usuario,
                       par$cod_cajero,
                       par$oid_punto_servicio,
                       par$cod_delegacion,
                       par$cod_cultura) THEN
    
      sins_tremesa(par$oid_remesa,
                   par$oid_remesa_origen,
                   par$oid_externo,
                   par$oid_iac,
                   par$oid_documento,
                   par$oid_cuenta,
                   par$cod_recibo_salida,
                   par$cod_estado,
                   par$usuario_resp,
                   par$puesto_resp,
                   par$cod_ruta,
                   par$nel_parada,
                   par$fyh_transporte,
                   par$nel_cantidad_bultos,
                   par$fyh_conteo_inicio,
                   par$fyh_conteo_fin,
                   par$oid_remesa_padre,
                   par$oid_remesa_substituta,
                   par$cod_cajero,
                   par$cod_nivel_detalle,
                   par$cod_externo,
                   par$bol_gestion_bulto,
                   par$cod_estado_abono,
                   par$oid_cuenta_saldo,
                   par$usuario,
                   par$inserts);
    
    END IF;
  
  END sgrabar_remesa;

  /* sins_tremesa */
  PROCEDURE sins_tremesa(par$oid_remesa            IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_remesa_origen     IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_externo           IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_iac               IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_documento         IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_cuenta            IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_recibo_salida     IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_estado            IN gepr_pcomon_###VERSION###.tipo$est_,
                         par$usuario_resp          IN gepr_pcomon_###VERSION###.tipo$usr_,
                         par$puesto_resp           IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_ruta              IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$nel_parada            IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$fyh_transporte        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                         par$nel_cantidad_bultos   IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$fyh_conteo_inicio     IN gepr_pcomon_###VERSION###.tipo$fyh_,
                         par$fyh_conteo_fin        IN gepr_pcomon_###VERSION###.tipo$fyh_,
                         par$oid_remesa_padre      IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_remesa_substituta IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_cajero            IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_nivel_detalle     IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_externo           IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$bol_gestion_bulto     IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$cod_estado_abono      IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$oid_cuenta_saldo      IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                         par$inserts               OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TREMESA
      (OID_REMESA,
       OID_EXTERNO,
       OID_REMESA_ORIGEN,
       OID_IAC,
       OID_DOCUMENTO,
       COD_RECIBO_SALIDA,
       COD_ESTADO,
       COD_EXTERNO,
       COD_USUARIO_RESPONSABLE,
       COD_PUESTO_RESPONSABLE,
       COD_RUTA,
       NEL_PARADA,
       NEL_CANTIDAD_BULTOS,
       FYH_CONTEO_INICIO,
       FYH_CONTEO_FIN,
       FYH_TRANSPORTE,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       OID_CUENTA,
       OID_REMESA_PADRE,
       OID_REMESA_SUSTITUTA,
       DES_USUARIO_MODIFICACION,
       COD_CAJERO,
       BOL_GESTION_BULTO,
       COD_NIVEL_DETALLE,
       OID_CUENTA_SALDO,
       COD_ESTADO_ABONO)
    VALUES
      (par$oid_remesa,
       par$oid_externo,
       par$oid_remesa_origen,
       par$oid_iac,
       par$oid_documento,
       par$cod_recibo_salida,
       par$cod_estado,
       par$cod_externo,
       par$usuario_resp,
       par$puesto_resp,
       par$cod_ruta,
       par$nel_parada,
       par$nel_cantidad_bultos,
       par$fyh_conteo_inicio,
       par$fyh_conteo_fin,
       par$fyh_transporte,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$oid_cuenta,
       par$oid_remesa_padre,
       par$oid_remesa_substituta,
       par$usuario,
       par$cod_cajero,
       par$bol_gestion_bulto,
       par$cod_nivel_detalle,
       par$oid_cuenta_saldo,
       par$cod_estado_abono);
    par$inserts := sql%rowcount;
  END sins_tremesa;

  /* srecuperar_remesas */
  PROCEDURE srecuperar_remesas(par$oids_remesa               IN gepr_pcomon_###VERSION###.tipo$oids_,
                               par$oids_documentos           IN gepr_pcomon_###VERSION###.tipo$oids_,
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
                               par$ejecucion_interna         IN INTEGER,
                               par$usuario                   IN gepr_pcomon_###VERSION###.tipo$usr_,
                               par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                               par$info_ejecucion            IN VARCHAR2,
                               par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                               par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$inicio      TIMESTAMP;
    var$registros   gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts     gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates     gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes     gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges      gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects     gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts_int gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_;
    var$err         varchar2(4000);
    var$oids_documentos gepr_pcomon_###VERSION###.tipo$oids_;
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
    IF par$oids_remesa IS NOT NULL AND par$oids_remesa.count > 0 THEN
    
      /* Si fue informado identificadores de remesas */
      FOR idx IN par$oids_remesa.first .. par$oids_remesa.last LOOP
      
        SELECT DE.OID_DOCUMENTO
          INTO var$oids_documentos(var$oids_documentos.count)
          FROM SAPR_TDOCUMENTOXELEMENTO DE
         WHERE DE.OID_REMESA = par$oids_remesa(idx)
           AND DE.Cod_Estado_Docxelemento = 'C' AND ROWNUM = 1;
        var$selects   := var$selects + sql%ROWCOUNT;
        var$registros := var$registros + sql%ROWCOUNT;
      
      END LOOP;
    
    ELSE
      var$oids_documentos := par$oids_documentos;
    END IF;
  
    SAPR_PELEMENTO_###VERSION###.srecuperar_elementos(par$oids_documentos           => var$oids_documentos,
                                                 par$usuario                   => par$usuario,
                                                 par$ejecucion_interna         => par$ejecucion_interna,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in('srecuperar_remesas',
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in('srecuperar_remesas',
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
    
      RAISE_APPLICATION_ERROR(-20101, var$err, true);
    
  END srecuperar_remesas;

  /* scargar_tt_oids_iac_elem */
  PROCEDURE scargar_tt_oids_iac_elem IS
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
  
  END scargar_tt_oids_iac_elem;

  /* fcargar_rc_lista_valor */
  FUNCTION fcargar_rc_lista_valor RETURN SYS_REFCURSOR IS
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
  
    RETURN var$rc_lista_valor;
  
  END fcargar_rc_lista_valor;

  /* fcargar_rc_elementos */
  FUNCTION fcargar_rc_elementos RETURN SYS_REFCURSOR IS
    var$rc_elementos SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rc_elementos FOR
      SELECT DISTINCT -- Remesas
                       R.OID_REMESA,
                      R.OID_CUENTA                AS R_OID_CUENTA,
                      R.OID_CUENTA_SALDO          AS R_OID_CUENTA_SALDO,
                      R.COD_ESTADO                AS R_COD_ESTADO,
                      R.GMT_CREACION              AS R_GMT_CREACION,
                      R.GMT_MODIFICACION          AS R_GMT_MODIFICACION,
                      R.COD_PUESTO_RESPONSABLE    AS R_COD_PUESTO_RESPONSABLE,
                      R.DES_USUARIO_CREACION      AS R_DES_USUARIO_CREACION,
                      R.DES_USUARIO_MODIFICACION  AS R_DES_USUARIO_MODIFICACION,
                      R.COD_USUARIO_RESPONSABLE   AS R_COD_USUARIO_RESPONSABLE,
                      R.OID_IAC                   AS R_OID_IAC,
                      R.COD_RUTA                  AS R_COD_RUTA,
                      R.COD_RECIBO_SALIDA         AS R_COD_RECIBO_SALIDA,
                      R.OID_REMESA_ORIGEN         AS R_OID_REMESA_ORIGEN,
                      R.COD_EXTERNO               AS R_COD_EXTERNO,
                      DER.OID_DOCUMENTO           AS R_OID_DOCUMENTO,
                      DER.COD_ESTADO_DOCXELEMENTO AS COD_ESTADO_DOCXELEMENTO_R,
                      R.FYH_TRANSPORTE            AS R_FYH_TRANSPORTE,
                      R.NEL_PARADA                AS R_NEL_PARADA,
                      R.OID_EXTERNO               AS R_OID_EXTERNO,
                      R.COD_CAJERO                AS R_COD_CAJERO,
                      R.OID_REMESA_PADRE,
                      R.FYH_CONTEO_INICIO         R_FYH_CONTEO_INICIO,
                      R.FYH_CONTEO_FIN            R_FYH_CONTEO_FIN,
                      R.BOL_GESTION_BULTO,
                      R.BOL_NOENTREGUE,
                      R.BOL_ANULADO,
                      R.COD_NIVEL_DETALLE         R_COD_NIVEL_DETALLE,
                      R.NEL_CANTIDAD_BULTOS,
					  R.Rowver AS R_ROWVER,

                      -- Bultos
                      B.OID_BULTO,
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
                      DER.OID_DOCUMENTO AS B_OID_DOCUMENTO,
                      DER.COD_ESTADO_DOCXELEMENTO AS COD_ESTADO_DOCXELEMENTO_B,
                      B.FYH_PROCESO_LEGADO AS B_FYH_PROCESO_LEGADO,
                      B.NEL_CANTIDAD_PARCIALES AS B_NEL_CANTIDAD_PARCIALES,
                      B.BOL_CUADRADO AS BOL_CUADRADO,
                      B.OID_BULTO_PADRE,
                      B.FYH_CONTEO_INICIO B_FYH_CONTEO_INICIO,
                      B.FYH_CONTEO_FIN B_FYH_CONTEO_FIN,
                      B.COD_NIVEL_DETALLE B_COD_NIVEL_DETALLE,
                      (SELECT LV1.COD_VALOR
                         FROM SAPR_TLISTA_VALORXELEMENTO LVB1
                        INNER JOIN GEPR_TLISTA_TIPO LT1
                           ON LT1.OID_LISTA_TIPO = LVB1.OID_LISTA_TIPO
                          AND LT1.COD_TIPO = '04'
                        INNER JOIN GEPR_TLISTA_VALOR LV1
                           ON LV1.OID_LISTA_VALOR = LVB1.OID_LISTA_VALOR
                        WHERE LVB1.OID_BULTO = B.OID_BULTO
                          AND rownum = 1) AS B_TipoUbicacion,
					  B.Rowver AS B_ROWVER,
                      
                      -- Parciales
                      P.OID_PARCIAL,
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
                      P.OID_EXTERNO              AS P_OID_EXTERNO
      
        FROM SAPR_TREMESA R
        LEFT JOIN SAPR_TBULTO B
          ON B.OID_REMESA = R.OID_REMESA
        LEFT JOIN SAPR_TPARCIAL P
          ON P.OID_BULTO = B.OID_BULTO
       INNER JOIN SAPR_TDOCUMENTOXELEMENTO DER
          ON DER.OID_REMESA = R.OID_REMESA
         AND (DER.OID_BULTO = B.OID_BULTO)
       INNER JOIN SAPR_TDOCUMENTO DOCR
          ON DER.OID_DOCUMENTO = DOCR.OID_DOCUMENTO
       INNER JOIN SAPR_GTT_TOIDS tmp
          ON tmp.OID = DOCR.OID_DOCUMENTO
       WHERE tmp.cod_calificador = 'DOCUMENTO';
  
    RETURN var$rc_elementos;
  
  END fcargar_rc_elementos;

  /* fcargar_rc_valores_det_efec */
  FUNCTION fcargar_rc_valores_det_efec RETURN SYS_REFCURSOR IS
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
  
    RETURN var$rc_valores_det_efectivo;
  
  END fcargar_rc_valores_det_efec;

  /* fcargar_rc_valores_det_mp */
  FUNCTION fcargar_rc_valores_det_mp RETURN SYS_REFCURSOR IS
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
  
    RETURN var$rc_valores_det_medio_pago;
  
  END fcargar_rc_valores_det_mp;

  /* fcargar_rc_valores_totales */
  FUNCTION fcargar_rc_valores_totales RETURN SYS_REFCURSOR IS
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
  
    RETURN var$rc_valores_totales;
  
  END fcargar_rc_valores_totales;

  /* fcargar_rc_iac */
  FUNCTION fcargar_rc_iac RETURN SYS_REFCURSOR IS
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
  
    RETURN var$rc_iac;
  
  END fcargar_rc_iac;

  /* fcargar_rc_terminos_iac */
  FUNCTION fcargar_rc_terminos_iac RETURN SYS_REFCURSOR IS
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
  
    RETURN var$rc_terminos_iac;
  
  END fcargar_rc_terminos_iac;

  /* fcargar_rc_valor_termino_iac */
  FUNCTION fcargar_rc_valor_termino_iac RETURN SYS_REFCURSOR IS
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
  
    RETURN var$rc_valor_termino_iac;
  
  END fcargar_rc_valor_termino_iac;

  /* fvalidar_remesa */
  FUNCTION fvalidar_remesa(par$oid_documento      IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$cod_estado         IN gepr_pcomon_###VERSION###.tipo$est_,
                           par$usuario            IN gepr_pcomon_###VERSION###.tipo$usr_,
                           par$cod_cajero         IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$oid_punto_servicio IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$cod_delegacion     IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_cultura        IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
  BEGIN
    IF par$oid_documento IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'Documento',
                                                                  0));
      RETURN False;
    END IF;
  
    IF par$cod_estado IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'Estado',
                                                                  0));
      RETURN False;
    END IF;
  
    IF par$usuario IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  gepr_pcomon_###VERSION###.const$codExpAtributoObrig,
                                                                  gepr_pcomon_###VERSION###.const$CodFunValidarAtributo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                                  'Usuario',
                                                                  0));
      RETURN False;
    END IF;
  
    -- Validar ATM
    IF par$cod_cajero IS NOT NULL AND par$oid_punto_servicio IS NOT NULL AND
       par$cod_delegacion IS NOT NULL THEN
      IF NOT sapr_pCAJERO_###VERSION###.fvalidar_datos_atm(par$cod_cajero,
                                                      par$oid_punto_servicio,
                                                      par$cod_delegacion) THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'codigo_atm_invalido',
                                                                    const$codFuncionalidad,
                                                                    gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                    NULL,
                                                                    0));
        RETURN False;
      END IF;
    
    END IF;
  
    RETURN True;
  END fvalidar_remesa;

  /* Validar estado de la remesa */
  FUNCTION fvalidar_estado_remesa(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$cod_estado IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS
    var$retorno gepr_pcomon_###VERSION###.tipo$nbol_;
  BEGIN
  
    BEGIN
    
      SELECT 1
        INTO var$retorno
        FROM SAPR_TREMESA R
       WHERE R.OID_REMESA = par$oid_remesa
         and R.COD_ESTADO = par$cod_estado;
    
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$retorno := NULL;
    END;
  
    return var$retorno;
  end fvalidar_estado_remesa;

  PROCEDURE sexcluir_remesa(par$oid_documento             IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_remesa                IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$abul_oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$abul_oid_documento        IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$abul_oid_tipo_formato     IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$abul_oid_tipo_servicio    IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$apar_oid_parcial          IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$apar_oid_bulto            IN gepr_pcomon_###VERSION###.tipo$oids_,
                            par$apar_oid_oid_tipo_formato IN gepr_pcomon_###VERSION###.tipo$oids_) IS
    par$deletes gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN
    --Se a remesa tiver bulto, entao exclui os bultos e parciais
    IF NOT gepr_putilidades_###VERSION###.farray_vacio_oid(par$abul_oid_bulto) THEN
      SAPR_PBULTO_###VERSION###.sexcluir_bulto(par$abul_oid_bulto,
                                          par$abul_oid_documento,
                                          par$abul_oid_tipo_formato,
                                          par$abul_oid_tipo_servicio,
                                          par$apar_oid_parcial,
                                          par$apar_oid_bulto,
                                          par$apar_oid_oid_tipo_formato);
    END IF;
  
    sdel_tremesa(par$oid_remesa, par$deletes);
  END sexcluir_remesa;

  PROCEDURE sdel_tremesa(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$deletes    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE FROM SAPR_TREMESA WHERE OID_REMESA = par$oid_remesa;
    par$deletes := sql%rowcount;
  END sdel_tremesa;

  PROCEDURE supd_tremesa(par$oid_remesa       in gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_documento    in gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_cuenta_mov   in gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_cuenta_saldo in gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_usuario      in gepr_pcomon_###VERSION###.tipo$cod_,
                         par$cod_estado       in gepr_pcomon_###VERSION###.tipo$cod_,
                         par$updates          out gepr_pcomon_###VERSION###.tipo$nel_) as
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  begin
  
    UPDATE sapr_tremesa r
       SET r.cod_estado               = nvl(par$cod_estado, r.cod_estado),
           r.oid_documento            = nvl(par$oid_documento,
                                            r.oid_documento),
           r.oid_cuenta               = nvl(par$oid_cuenta_mov, r.oid_cuenta),
           r.oid_cuenta_saldo         = nvl(par$oid_cuenta_saldo,
                                            r.oid_cuenta_saldo),
           r.gmt_modificacion         = var$gmt_zero,
           r.des_usuario_modificacion = par$cod_usuario
     WHERE r.oid_remesa = par$oid_remesa;
    par$updates := sql%rowcount;
  END supd_tremesa;

  
  procedure supd_tremesa_rowver(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$rowver   IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_cultura      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_funcionalidad in gepr_pcomon_###VERSION###.tipo$desc_)as
   PRAGMA AUTONOMOUS_TRANSACTION;                                        
   var$rowver gepr_pcomon_###VERSION###.tipo$nel_ := NVL(par$rowver,0) + 1;
   var$cod_externo varchar2(100);
  begin
    
    update sapr_tremesa r
    set r.rowver = var$rowver
    where r.oid_remesa =par$oid_remesa
    and NVL(r.rowver,0) = NVL(par$rowver,0);
    
    if sql%rowcount >0 then
      par$rowver :=var$rowver;
    else
      begin
        select r.cod_externo
        into var$cod_externo
        from sapr_tremesa r where r.oid_remesa =par$oid_remesa;
        
        /*recuperar o codigo externo para exibir na mensagem*/
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                          gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                              'remesa_modificada',
                                                              'SAPR_PREMESA_###VERSION###.supd_tremesa_rowver.'||par$cod_funcionalidad,
                                                              gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                              var$cod_externo,
                                                              1));
       exception   
         when no_data_found then
           null;
       end;                                                     
    end if;  
    
    commit; 
  end supd_tremesa_rowver;                                        

END SAPR_PREMESA_###VERSION###;
/
