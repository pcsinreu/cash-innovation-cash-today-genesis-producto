CREATE OR REPLACE PACKAGE sapr_ptransiciones_###VERSION### AS

  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_ptransiciones_###VERSION###';

  /* sejecutar_grp_doc */
  PROCEDURE sejecutar_grp_doc(par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_estado_documento IN OUT gepr_pcomon_###VERSION###.tipo$est_,
                              par$cod_comprobante      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                              par$rowver               IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$info_ejecucion       IN VARCHAR2,
                              par$cod_ejecucion        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$hacer_commit         IN INTEGER,
                              par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$deletes              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sejecutar_doc */
  PROCEDURE sejecutar_doc(par$oid_llamada        IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_estado_documento IN OUT gepr_pcomon_###VERSION###.tipo$est_,
                          par$cod_comprobante      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                          par$rowver               IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$info_ejecucion       IN VARCHAR2,
                          par$hacer_commit         IN INTEGER,
                          par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$deletes              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$cod_ejecucion        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* fverificar_estado_grp */
  FUNCTION fverificar_estado_grp(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$estado              IN gepr_pcomon_###VERSION###.tipo$est_)
    RETURN BOOLEAN;

  /* fverificar_estado_doc */
  FUNCTION fverificar_estado_doc(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$estado        IN gepr_pcomon_###VERSION###.tipo$est_)
    RETURN BOOLEAN;

  /* fverificar_estado */
  FUNCTION fverificar_estado(par$estado_actual IN gepr_pcomon_###VERSION###.tipo$est_,
                             par$estado        IN gepr_pcomon_###VERSION###.tipo$est_)
    RETURN BOOLEAN;

  /* svalidar_accion_rechazar */
  FUNCTION fvalidar_accion_rechazar(par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$est_)
    RETURN BOOLEAN;

  /* fgenerar_comprobante_grp */
  PROCEDURE fgenerar_comprobante_grp(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_comprobante     OUT gepr_pcomon_###VERSION###.tipo$cod_);

  /* fgenerar_comprobante_doc */
  PROCEDURE fgenerar_comprobante_doc(par$oid_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_cultura     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_comprobante OUT gepr_pcomon_###VERSION###.tipo$cod_);

  /* fgenerar_comprobante */
  PROCEDURE fgenerar_comprobante(par$cod_delegacion  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_planta      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_sector      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_formulario  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$oid_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_formulario  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_cultura     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_comprobante OUT gepr_pcomon_###VERSION###.tipo$cod_);

  /* fform_implementado */
  FUNCTION fform_implementado(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN INTEGER;

  /* Reverte a accio contabil del documento */
  PROCEDURE saccion_contable_revertir(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_documento_padre  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$usuario              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Reverte a transacao de efectivo do documento */
  PROCEDURE stransacion_efectivo_reverti(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_estado_documento     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$tipo_sitio               IN CHAR,
                                         par$oid_transaccion_efectivo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$permite_saldo_negativo   IN BOOLEAN,
                                         par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Reverte a transacao de medio pago do documento */
  PROCEDURE stransacion_medio_pg_reverti(par$oid_documento              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_estado_documento       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$usuario                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$tipo_sitio                 IN CHAR,
                                         par$oid_transaccion_medio_pago IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$permite_saldo_negativo     IN BOOLEAN,
                                         par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Executa a acao contabil de efectivo do documento */
  PROCEDURE ssaldo_efectivo_actualizar(par$oid_documento              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_transaccion_efectivo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$permite_saldo_negativo   IN BOOLEAN,
                                       par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Executa a acao contabil de medio pago do documento */
  PROCEDURE ssaldo_medio_pago_actualizar(par$oid_transaccion_medio_pago IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$usuario                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$permite_saldo_negativo     IN BOOLEAN,
                                         par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_);

   PROCEDURE ssaldo_efectivo_hist_actu(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_transaccion_efectivo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$permite_saldo_negativo   IN BOOLEAN,
                                       par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Executa a acao contabil do documento */
  PROCEDURE saccion_contable_ejecutar(par$oid_documento             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_doc_formulario_accion IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado_documento      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$usuario                   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* actualizar estado de los elementos del documento */
  PROCEDURE selemento_actualizar_estado(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$bol_sustituicion     IN BOOLEAN,
                                        par$oid_documento_padre  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$usuario              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sactualizar_fecha_cert_f22 */
  PROCEDURE sactualizar_fecha_cert_f22(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$usuario       IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* saccion_contable_classificac */
  PROCEDURE saccion_contable_classificac(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$usuario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_cultura   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects       OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE ssaldo_efec_actualizar_clas(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_transaccion_efectivo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_nivel_detalle        in varchar2,
                                        par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$permite_saldo_negativo   IN BOOLEAN,
                                        par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* fverificar_estado_bultos */
  FUNCTION fverificar_estado_bultos(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN;

END sapr_ptransiciones_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_ptransiciones_###VERSION### AS

  /* sejecutar_grp_doc */
  PROCEDURE sejecutar_grp_doc(par$oid_grupo_documento  IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_estado_documento IN OUT gepr_pcomon_###VERSION###.tipo$est_,
                              par$cod_comprobante      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                              par$rowver               IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                              par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$info_ejecucion       IN VARCHAR2,
                              par$cod_ejecucion        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$hacer_commit         IN INTEGER,
                              par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$deletes              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sejecutar_grp_doc';

    var$inicio               gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros            gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges               gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts_int          gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int          gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates_int          gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes_int          gepr_pcomon_###VERSION###.tipo$nel_;
    var$err                  varchar2(4000);
    var$oid_formulario       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_origen    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_destino   gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_comprobante      gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$cod_;
    var$rowveractual         gepr_pcomon_###VERSION###.tipo$nel_;

    /*CURSOR DE DOCUMENTOS DEL GRUPO*/
    CURSOR cur$documentos(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT D.OID_DOCUMENTO, D.ROWVER
        FROM SAPR_TDOCUMENTO D
       WHERE D.OID_GRUPO_DOCUMENTO = par$oid_grupo_documento;

  BEGIN

    var$inicio    := SYSTIMESTAMP;
    var$registros := 0;
    par$inserts   := 0;
    par$updates   := 0;
    par$deletes   := 0;
    par$selects   := 0;

    /* recuperar formulario */
    SELECT oid_formulario,
           oid_sector_origen,
           oid_sector_destino,
           NVL(rowver, -1)
      INTO var$oid_formulario,
           var$oid_sector_origen,
           var$oid_sector_destino,
           var$rowveractual
      FROM SAPR_TGRUPO_DOCUMENTO
     WHERE oid_grupo_documento = par$oid_grupo_documento;
    par$selects   := par$selects + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;

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
      IF var$rowveractual <> par$rowver THEN
        /* hubo un problema de concurrencia */
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msgconcurrenciasobreregistro',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1));
      END IF;
    END IF;

    /* - Documentos */
    FOR rec$documento IN cur$documentos(par$oid_grupo_documento) LOOP

      var$cod_estado_documento := par$cod_estado_documento;
      sejecutar_doc(par$oid_llamada          => NULL,
                    par$oid_documento        => rec$documento.oid_documento,
                    par$cod_estado_documento => var$cod_estado_documento,
                    par$cod_comprobante      => var$cod_comprobante,
                    par$rowver               => rec$documento.rowver,
                    par$usuario              => par$usuario,
                    par$cod_cultura          => par$cod_cultura,
                    par$info_ejecucion       => par$info_ejecucion,
                    par$hacer_commit         => 0,
                    par$inserts              => var$inserts_int,
                    par$updates              => var$updates_int,
                    par$deletes              => var$deletes_int,
                    par$selects              => var$selects_int,
                    par$cod_ejecucion        => par$cod_ejecucion);

      par$inserts   := par$inserts + var$inserts_int;
      par$updates   := par$updates + var$updates_int;
      par$deletes   := par$deletes + var$deletes_int;
      par$selects   := par$selects + var$selects_int;
      var$registros := var$registros + var$selects_int + var$inserts_int +
                       var$deletes_int + var$updates_int;

    END LOOP rec$documento;
    par$selects := par$selects + 1;

    /* - Insere historico */
    sapr_pdocumentos_grp_###VERSION###.sins_thist_mov_grupo_docs(par$oid_grupo_documento,
                                                            par$cod_estado_documento,
                                                            par$usuario,
                                                            var$inserts_int);
    par$inserts   := par$inserts + var$inserts_int;
    var$registros := var$registros + var$inserts_int;

    IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado THEN

      /* - generar codigo comprovante del grupo */
      fgenerar_comprobante_grp(par$oid_grupo_documento => par$oid_grupo_documento,
                               par$cod_cultura         => par$cod_cultura,
                               par$cod_comprobante     => par$cod_comprobante);

      par$selects   := par$selects + 3;
      var$registros := var$registros + 3;

      /* - Si el setor de destino es lo mismo del origen o si el formulario esta configurado
      como MOVIMIENTO_ACEPTACION_AUTOMATICA  */
      IF var$oid_sector_origen = var$oid_sector_destino OR
         sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                            par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic) THEN

        par$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado;

        sapr_pdocumentos_grp_###VERSION###.sins_thist_mov_grupo_docs(par$oid_grupo_documento,
                                                                par$cod_estado_documento,
                                                                par$usuario,
                                                                var$inserts_int);
        par$inserts   := par$inserts + var$inserts_int;
        var$registros := var$registros + var$inserts_int;
      END IF;

      /* - Atualiza o grupo de documentos */
      sapr_pdocumentos_grp_###VERSION###.supd_est_comp_grp_documento(par$oid_grupo_documento => par$oid_grupo_documento,
                                                                par$cod_estado_grp_doc  => par$cod_estado_documento,
                                                                par$cod_comprobante     => par$cod_comprobante,
                                                                par$usuario             => par$usuario);
      par$updates   := par$updates + sql%rowcount;
      var$registros := var$registros + sql%rowcount;

    ELSE

      /* - Atualiza o grupo de documentos */
      sapr_pdocumentos_grp_###VERSION###.supd_estado_grupo_documento(par$oid_grupo_documento => par$oid_grupo_documento,
                                                                par$cod_estado_grp_doc  => par$cod_estado_documento,
                                                                par$usuario             => par$usuario);
      par$updates   := par$updates + sql%rowcount;
      var$registros := var$registros + sql%rowcount;
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

  END sejecutar_grp_doc;

  
  /* sejecutar_doc */
  PROCEDURE sejecutar_doc(par$oid_llamada          IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$cod_estado_documento IN OUT gepr_pcomon_###VERSION###.tipo$est_,
                          par$cod_comprobante      OUT gepr_pcomon_###VERSION###.tipo$cod_,
                          par$rowver               IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$usuario              IN gepr_pcomon_###VERSION###.tipo$usr_,
                          par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                          par$info_ejecucion       IN VARCHAR2,
                          par$hacer_commit         IN INTEGER,
                          par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$deletes              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$cod_ejecucion        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sejecutar_doc';
    const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';



    var$oid_sector_origen   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_destino  gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_sector_des gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_documento_padre gepr_pcomon_###VERSION###.tipo$oid_;
    var$etransacioncert     NUMBER;

    /* variables locales */
    var$inicio                  gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros               gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges                  gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts_int             gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates_int             gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes_int             gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int             gepr_pcomon_###VERSION###.tipo$nel_;
    var$err                     VARCHAR2(4000);
    var$oid_formulario          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_comprobante         gepr_pcomon_###VERSION###.tipo$cod_;
    var$bol_sustituicion        BOOLEAN := false;
    var$bol_gestion_elem        BOOLEAN := false;
    var$bol_gestion_contenedor  BOOLEAN := false;
    var$rowveractual            gepr_pcomon_###VERSION###.tipo$nel_;
    var$doc_no_sustituto        varchar2(36);
    var$bolPermitirUsrAceptaDoc gepr_pcomon_###VERSION###.tipo$nbol_;
    var$cod_delegacion          gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_pais                gepr_pcomon_###VERSION###.tipo$cod_;
    var$usuario                 gepr_pcomon_###VERSION###.tipo$usr_;
    var$bol_considera_recuentos gepr_pcomon_###VERSION###.tipo$nbol_ := 1;
    var$oid_cuentasaldo_ori     gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuentasaldo_des     gepr_pcomon_###VERSION###.tipo$oid_;
    var$fyh_plncertif           gepr_pcomon_###VERSION###.tipo$fyh_;
    var$cod_validaciones        gepr_pcomon_###VERSION###.tipo$cods_;
    var$des_validaciones        gepr_pcomon_###VERSION###.tipo$descs_;
    
    var$oid_maquina             gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_planificacion       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_periodo             gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_tipo_planificacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_pto_servicio        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_pto_servicio_mae    gepr_pcomon_###VERSION###.tipo$oid_;
    var$existe                  gepr_pcomon_###VERSION###.tipo$nel_;

    var$cod_canal_mae           gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_subcanal_mae        gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_subcanal_mae        gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_sector_mae          gepr_pcomon_###VERSION###.tipo$cod_;
    var$fyh_gestion_mae         gepr_pcomon_###VERSION###.tipo$fyh_;

    var$cod_estado_doc_inicial  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_formulario          gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_documento           gepr_pcomon_###VERSION###.tipo$desc_;
    var$collection_id           gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_divisa              gepr_pcomon_###VERSION###.tipo$oid_;
    var$debe_crear_periodo_ac   gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$debe_actualizar_col_id  gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$fyh_fin_periodo         gepr_pcomon_###VERSION###.tipo$fyh_;
    var$col_id_periodo          gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_periodo_ac          gepr_pcomon_###VERSION###.tipo$oid_ := NULL;
    var$oid_estado_periodo      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_tipo_periodo        gepr_pcomon_###VERSION###.tipo$oid_;
    var$bol_corte_periodo       gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$oid_periodo_re          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_subcanal_peri       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_divisa_peri         gepr_pcomon_###VERSION###.tipo$oid_;

  BEGIN

    var$inicio    := SYSTIMESTAMP;
    var$registros := 0;
    par$inserts   := 0;
    par$updates   := 0;
    par$deletes   := 0;
    par$selects   := 0;

    begin
      select COD_EXTERNO
        into var$cod_documento
        from SAPR_TDOCUMENTO
       where oid_documento = par$oid_documento;

    exception
      when no_data_found then
        var$cod_documento := '';
    end;

    /* Guardo los parametros */
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => 'oid_llamada: '||par$oid_llamada||
                          ', oid_documento: '|| par$oid_documento||
                          ', cod_estado_documento : '||par$cod_estado_documento||
                          ', rowver: '||par$rowver||
                          ', usuario: '||par$usuario||
                          ', cod_cultura: '||par$cod_cultura||
                          ', info_ejecucion: '||par$info_ejecucion||
                          ', hacer_commit: '||par$hacer_commit,
                          par$cod_identificador   => var$cod_documento);

    /* recuperar informaciones del documento */
    SELECT d.oid_formulario,
           f.cod_formulario,
           d.oid_sector_origen,
           d.oid_sector_destino,
           NVL(d.rowver, -1),
           d.des_usuario_modificacion,
           TS.COD_TIPO_SECTOR,
           d.oid_cuenta_saldo_origen,
           d.oid_cuenta_saldo_destino,
           d.fyh_plan_certificacion,
           NVL(M.BOL_CONSIDERA_RECUENTOS, 1),
           d.COD_COLLECTION_ID,
           d.fyh_gestion,
           SC.COD_SUBCANAL,
           EFXDOC.OID_DIVISA
      INTO var$oid_formulario,
           var$cod_formulario,
           var$oid_sector_origen,
           var$oid_sector_destino,
           var$rowveractual,
           var$usuario,
           var$cod_tipo_sector_des,
           var$oid_cuentasaldo_ori,
           var$oid_cuentasaldo_des,
           var$fyh_plncertif,
           var$bol_considera_recuentos,
           var$collection_id,
           var$fyh_gestion_mae,
           var$cod_subcanal_mae,
          var$oid_divisa
      FROM SAPR_TDOCUMENTO d
      INNER JOIN SAPR_TFORMULARIO F ON F.OID_FORMULARIO = D.OID_FORMULARIO
      INNER JOIN GEPR_TSECTOR S ON S.OID_SECTOR = D.oid_sector_destino
      INNER JOIN GEPR_TTIPO_SECTOR TS ON TS.OID_TIPO_SECTOR = S.OID_TIPO_SECTOR
	    INNER JOIN SAPR_TCUENTA C ON C.OID_CUENTA = D.OID_CUENTA_DESTINO
      INNER JOIN GEPR_TSUBCANAL SC ON SC.OID_SUBCANAL = C.OID_SUBCANAL
      LEFT JOIN SAPR_TMAQUINA M ON M.OID_SECTOR = S.OID_SECTOR
      LEFT JOIN SAPR_TEFECTIVOXDOCUMENTO EFXDOC ON d.OID_DOCUMENTO = EFXDOC.OID_DOCUMENTO
    WHERE d.oid_documento = par$oid_documento AND ROWNUM = 1;
    par$selects   := par$selects + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;

    IF var$rowveractual = -1 THEN

      /* Guardo no existe */
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                            par$des_version     => const$version,
                            par$des_detalle     => 'No existe registro (var$rowveractual = -1)',
                            par$cod_identificador   => var$cod_documento);

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
      IF var$rowveractual <> par$rowver THEN

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     => 'var$rowveractual(' || var$rowveractual || ') <> par$rowver(' || par$rowver || ')',
                              par$cod_identificador   => var$cod_documento);
        /* hubo un problema de concurrencia */
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'msgconcurrenciasobreregistro',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1));
      END IF;
    END IF;

    /* GENPLATINT-2364 - Brasil - Saldos - No Permitir Usuario Creacion del documento Aceptar o Rechazar */
    IF (par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado OR
       par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocRechazado) AND
       var$usuario = par$usuario AND
       var$oid_sector_origen <> var$oid_sector_destino AND NOT
        sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                                                                               par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic) THEN

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     => '1er if (no permitir usuario creacion del docu acep o rechaz)' || 
                                                      ', var$usuario: ' || var$usuario || 
                                                      ', var$oid_sector_origen: ' || var$oid_sector_origen ||
                                                      ', var$oid_sector_destino: ' || var$oid_sector_destino ||
                                                      ', var$oid_formulario: ' || var$oid_formulario ||
                                                      ', gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic: ' || gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic ||
                                                      ', sapr_pformulario_###VERSION###.fverif_caract_formulario: ' || 
                                                        case 
                                                          when sapr_pformulario_###VERSION###.fverif_caract_formulario(var$oid_formulario, gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic) then 
                                                            'true'
                                                          else 
                                                            'false'
                                                        end
                                                      ,
                              par$cod_identificador   => var$cod_documento);

      SELECT D.COD_DELEGACION, D.COD_PAIS
        INTO var$cod_delegacion, var$cod_pais
        FROM GEPR_TSECTOR S
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = S.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION D
          ON D.OID_DELEGACION = P.OID_DELEGACION
       WHERE S.OID_SECTOR = var$oid_sector_destino;

      /* Recupera o parametro PermitirUsuarioCreacionAcepteRechazeDoc */
      var$bolPermitirUsrAceptaDoc := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => var$cod_delegacion,
                                                                                    par$cod_pais       => var$cod_pais,
                                                                                    par$cod_parametro  => gepr_pcomon_###VERSION###.const$CodParamIACUsrAceptaDoc,
                                                                                    par$cod_aplicacion => gepr_pcomon_###VERSION###.const$codAplicacionSaldos);

      IF var$bolPermitirUsrAceptaDoc <> 1 THEN
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                              par$des_version     => const$version,
                              par$des_detalle     => 'var$bolPermitirUsrAceptaDoc = ' || var$bolPermitirUsrAceptaDoc || '<> 1' ||
                                                      ' - El usuario responsable de aceptar o rechazar un documento, debe ser diferente del usuario que creo el documento.',
                              par$cod_identificador   => var$cod_documento);

        /* El usuario responsable de aceptar o rechazar un documento, debe ser diferente del usuario que creo el documento. */
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    'noPermiteUsuarioCreacionAceptarDoc',
                                                                    'sapr_ptransiciones.' ||
                                                                    const$nombre_func,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1));
      END IF;
    END IF;

    var$cod_estado_doc_inicial := par$cod_estado_documento;

    /* verifica estato */
    IF fverificar_estado_doc(par$oid_documento => par$oid_documento,
                             par$estado        => par$cod_estado_documento) THEN

      par$selects   := par$selects + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     => 'par$oid_docuemento: ' || par$oid_documento ||
                                               'par$cod_estado_documento: ' || par$cod_estado_documento ||
                                               ' - verifica_estado_doc = TRUE',
                        par$cod_identificador   => var$cod_documento);

      /* generar codigo comprobante (caso sea confirmar) */
      IF par$cod_estado_documento =
         gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado THEN

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'par$cod_estado_documento: ' || par$cod_estado_documento ||
                                          'gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado ||
                                          ' - Son iguales (cod_estado_documento = const$CodEstadoDocConfirmado)',
                  par$cod_identificador   => var$cod_documento);
        fgenerar_comprobante_doc(par$oid_documento   => par$oid_documento,
                                 par$cod_cultura     => par$cod_cultura,
                                 par$cod_comprobante => par$cod_comprobante);
        par$selects   := par$selects + 3;
        var$registros := var$registros + 3;

        /* actualizar estato y comprobante */
        sapr_pdocumento_###VERSION###.supd_est_comp_documento(par$oid_documento        => par$oid_documento,
                                                         par$cod_estado_documento => par$cod_estado_documento,
                                                         par$cod_comprobante      => par$cod_comprobante,
                                                         par$usuario              => par$usuario,
                                                         par$updates              => var$updates_int);
        par$updates   := par$updates + var$updates_int;
        var$registros := var$registros + var$updates_int;

      ELSIF par$cod_estado_documento =
            gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado THEN
                  SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     => 'par$cod_estado_documento: ' || par$cod_estado_documento ||
                                          'gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado ||
                                          ' - Son iguales (cod_estado_documento = const$CodEstadoDocAceptado)',
                  par$cod_identificador   => var$cod_documento);
        -- Validar Fecha Plan Certificacion
        IF var$oid_cuentasaldo_ori IS NOT NULL AND
           var$oid_cuentasaldo_des IS NOT NULL AND
           var$fyh_plncertif IS NOT NULL THEN

          sapr_pcertificacion_###VERSION###.SVALIDAR_FECHA_CERT(par$fyh_plncertif      => var$fyh_plncertif,
                                                           par$oid_cuenta_origen  => var$oid_cuentasaldo_ori,
                                                           par$oid_cuenta_destino => var$oid_cuentasaldo_des);

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     => 'var$oid_cuentasaldo_ori: ' || var$oid_cuentasaldo_ori ||
                                        'var$oid_cuentasaldo_des: ' || var$oid_cuentasaldo_des ||
                                        'var$fyh_plncertif: ' || var$fyh_plncertif ||
                                        ' - los tres parametros no son nulos',
                par$cod_identificador   => var$cod_documento);

        END IF;


        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'Llama: sapr_pdocumento_###VERSION###.supd_estado_documento' ||
                                      'par$oid_documento: ' || par$oid_documento ||
                                      'par$cod_estado_documento: ' || par$cod_estado_documento ||
                                      'par$usuario: ' || par$usuario ||
                                      'var$updates_int: ' || var$updates_int,
              par$cod_identificador   => var$cod_documento);

        /* actualizar estado */
        sapr_pdocumento_###VERSION###.supd_estado_documento(par$oid_documento        => par$oid_documento,
                                                       par$cod_estado_documento => par$cod_estado_documento,
                                                       par$usuario              => par$usuario,
                                                       par$updates              => var$updates_int);
        par$updates   := par$updates + var$updates_int;
        var$registros := var$registros + var$updates_int;

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'Llama: sapr_pdocumento_###VERSION###.supd_fyh_plan_certificacion' ||
                                      'par$oid_documento: ' || par$oid_documento ||
                                      'var$fyh_plncertif: ' || var$fyh_plncertif ||
                                      'par$usuario: ' || par$usuario ||
                                      'var$updates_int: ' || var$updates_int,
              par$cod_identificador   => var$cod_documento);
        sapr_pdocumento_###VERSION###.supd_fyh_plan_certificacion(par$oid_documento          => par$oid_documento,
                                                             par$fyh_plan_certificacion => var$fyh_plncertif,
                                                             par$usuario                => par$usuario,
                                                             par$updates                => var$updates_int);
        par$updates   := par$updates + var$updates_int;
        var$registros := var$registros + var$updates_int;

      ELSE

        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'Llama: sapr_pdocumento_###VERSION###.supd_estado_documento' ||
                                      'par$oid_documento: ' || par$oid_documento ||
                                      'par$cod_estado_documento: ' || par$cod_estado_documento ||
                                      'par$usuario: ' || par$usuario ||
                                      'var$updates_int: ' || var$updates_int,
              par$cod_identificador   => var$cod_documento);

        /* actualizar estato */
        sapr_pdocumento_###VERSION###.supd_estado_documento(par$oid_documento        => par$oid_documento,
                                                       par$cod_estado_documento => par$cod_estado_documento,
                                                       par$usuario              => par$usuario,
                                                       par$updates              => var$updates_int);
        par$updates   := par$updates + var$updates_int;
        var$registros := var$registros + var$updates_int;

      END IF;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     =>  'Llama: sapr_pdocumento_###VERSION###.sins_thist_mov_documento' ||
                                    'par$oid_documento: ' || par$oid_documento ||
                                    'par$cod_estado_documento: ' || par$cod_estado_documento ||
                                    'par$usuario: ' || par$usuario ||
                                    'var$inserts_int: ' || var$inserts_int,
            par$cod_identificador   => var$cod_documento);
      /* historico del documento */
      sapr_pdocumento_###VERSION###.sins_thist_mov_documento(par$oid_documento,
                                                        par$cod_estado_documento,
                                                        par$usuario,
                                                        var$inserts_int);

      par$inserts   := par$inserts + var$inserts_int;
      var$registros := var$registros + var$inserts_int;

      /* obtener identificador documento padre */
      SELECT d.oid_documento_padre
        INTO var$oid_documento_padre
        FROM sapr_tdocumento d
       WHERE d.oid_documento = par$oid_documento;
      par$selects   := par$selects + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;

      /* tratar gestion de elementos (Remesa y Bulto) */
      var$bol_gestion_elem := sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                                                 par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionRemes) OR
                              sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                                                 par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionBulto);

      IF var$bol_gestion_elem THEN
        /* tratar sustituiciones */
        var$bol_sustituicion := sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                                                   par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAccionSustit);
      
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     =>  'IF var$bol_gestion_elem THEN' ||
                                                  'var$bol_gestion_elem: ' || 
                                                  case 
                                                    when var$bol_gestion_elem  then 'true'
                                                    else 'false'
                                                  end ||
                                                  'var$bol_sustituicion: ' || 
                                                  case 
                                                    when var$bol_sustituicion then 'true'
                                                    else 'false'
                                                  end,
                          par$cod_identificador   => var$cod_documento);
      END IF;

      par$selects   := par$selects + sql%ROWCOUNT;
      var$registros := var$registros + sql%ROWCOUNT;

      var$bol_gestion_contenedor := sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                                                       par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionContd);
      par$selects                := par$selects + sql%ROWCOUNT;
      var$registros              := var$registros + sql%ROWCOUNT;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     =>  'Se asigna valor a var$bol_gestion_contenedor' ||
                                          ' - var$bol_gestion_contenedor: ' || 
                                          case 
                                            when var$bol_gestion_contenedor then 
                                              'true'
                                            else 
                                              'false'
                                          end,
                  par$cod_identificador   => var$cod_documento);

      IF var$bol_sustituicion THEN
        begin
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     =>  'IF var$bol_sustituicion THEN' ||
                                    ' - var$bol_sustituicion: ' || 
                                    case 
                                      when var$bol_sustituicion then 'true'
                                      else 'false'
                                    end,
            par$cod_identificador   => var$cod_documento);
        end;
        begin
          WITH DOCS AS
           (SELECT D.OID_DOCUMENTO,
                   (SELECT COUNT(*)
                      FROM SAPR_TCARACTFORMXFORMULARIO CF
                     INNER JOIN SAPR_TCARACT_FORMULARIO C
                        ON C.OID_CARACT_FORMULARIO = CF.OID_CARACT_FORMULARIO
                     WHERE C.cod_caract_formulario =
                           gepr_pcomon_###VERSION###.const$CodCaracAccionSustit
                       AND CF.OID_FORMULARIO = D.OID_FORMULARIO) ES_SUSTITUTO
              FROM SAPR_TDOCUMENTO D
             START WITH D.OID_DOCUMENTO = par$oid_documento
            CONNECT BY NOCYCLE
             PRIOR D.OID_DOCUMENTO = D.OID_DOCUMENTO_SUSTITUTO)
          SELECT OID_DOCUMENTO
            INTO var$doc_no_sustituto
            FROM DOCS
           WHERE ES_SUSTITUTO = 0;
        exception
          when others then
            var$doc_no_sustituto := null;
        end;
      else
        var$doc_no_sustituto := par$oid_documento;
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     =>  'IF NOT var$bol_sustituicion THEN' ||
                                    ' - var$doc_no_sustituto: ' || var$doc_no_sustituto ||
                                    ' - var$bol_sustituicion: ' || 
                                    case 
                                      when var$bol_sustituicion then 'true'
                                      else 'false'
                                    end,
            par$cod_identificador   => var$cod_documento);
      end if;

      IF var$bol_sustituicion THEN

        IF par$cod_estado_documento =
           gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado THEN

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     =>  'IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado THEN' ||
                                              ' - par$cod_estado_documento: ' || par$cod_estado_documento ||
                                              ' - gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado,
                      par$cod_identificador   => var$cod_documento);

          /* verifica si el documento tiene alguna transicion con certificado definitivo */
          SELECT COUNT(1)
            INTO var$etransacioncert
            FROM sapr_tdocumento doc
            LEFT JOIN sapr_ttransaccion_efectivo tf
              ON tf.oid_documento = doc.oid_documento
            LEFT JOIN sapr_ttransaccion_medio_pago tmp
              ON tmp.oid_documento = doc.oid_documento
           INNER JOIN sapr_tcertificado cer
              ON cer.oid_certificado IN
                 (tf.oid_certificado, tmp.oid_certificado)
           WHERE cer.cod_estado = 'DE'
             AND doc.bol_certificado = 1
             AND doc.oid_documento = var$oid_documento_padre;
          par$selects   := par$selects + sql%ROWCOUNT;
          var$registros := var$registros + sql%ROWCOUNT;


          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     =>  'Cargo la variable var$etransacioncert' ||
                                              ' - var$etransacioncert: ' || var$etransacioncert,
                      par$cod_identificador   => var$cod_documento);

          IF var$etransacioncert > 0 THEN

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     =>  'var$etransacioncert > 0' ||
                                                ' - var$etransacioncert: ' || var$etransacioncert ||
                                                ' - llama saccion_contable_revertir ' ||
                                                ' - par$oid_documento: ' || par$oid_documento ||
                                                ' - var$oid_documento_padre: ' || var$oid_documento_padre ||
                                                ' - par$cod_estado_documento: ' || par$cod_estado_documento ||
                                                ' - par$usuario: ' || par$usuario ||
                                                ' - par$cod_cultura: ' || par$cod_cultura ||
                                                ' - var$inserts_int: ' || var$inserts_int ||
                                                ' - var$selects_int: ' || var$selects_int ,
                        par$cod_identificador   => var$cod_documento);

            saccion_contable_revertir(par$oid_documento        => par$oid_documento,
                                      par$oid_documento_padre  => var$oid_documento_padre,
                                      par$cod_estado_documento => par$cod_estado_documento,
                                      par$usuario              => par$usuario,
                                      par$cod_cultura          => par$cod_cultura,
                                      par$inserts              => var$inserts_int,
                                      par$selects              => var$selects_int);
            par$inserts   := par$inserts + var$inserts_int;
            par$selects   := par$selects + var$selects_int;
            var$registros := var$registros + var$selects_int +
                             var$inserts_int;

          ELSE

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     =>  'var$etransacioncert <= 0' ||
                                                ' - var$etransacioncert: ' || var$etransacioncert ||
                                                ' - llama saccion_contable_revertir ' ||
                                                ' - par$oid_documento: ' || par$oid_documento ||
                                                ' - var$oid_documento_padre: ' || var$oid_documento_padre ||
                                                ' - par$cod_estado_documento: ' || par$cod_estado_documento ||
                                                ' - par$usuario: ' || par$usuario ||
                                                ' - par$cod_cultura: ' || par$cod_cultura ||
                                                ' - var$inserts_int: ' || var$inserts_int ||
                                                ' - var$selects_int: ' || var$selects_int ,
                        par$cod_identificador   => var$cod_documento);

            saccion_contable_revertir(par$oid_documento        => var$oid_documento_padre,
                                      par$oid_documento_padre  => null,
                                      par$cod_estado_documento => par$cod_estado_documento,
                                      par$usuario              => par$usuario,
                                      par$cod_cultura          => par$cod_cultura,
                                      par$inserts              => var$inserts_int,
                                      par$selects              => var$selects_int);
            par$inserts   := par$inserts + var$inserts_int;
            par$selects   := par$selects + var$selects_int;
            var$registros := var$registros + var$selects_int +
                             var$inserts_int;

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     =>  'var$etransacioncert <= 0' ||
                                                ' - var$etransacioncert: ' || var$etransacioncert ||
                                                ' - llama saccion_contable_ejecutar ' ||
                                                ' - par$oid_documento: ' || par$oid_documento ||
                                                ' - par$cod_estado_documento (gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido): ' || gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido ||
                                                ' - par$usuario: ' || par$usuario ||
                                                ' - par$cod_cultura: ' || par$cod_cultura ||
                                                ' - var$inserts_int: ' || var$inserts_int ||
                                                ' - var$selects_int: ' || var$selects_int ,
                        par$cod_identificador   => var$cod_documento);

            saccion_contable_ejecutar(par$oid_documento             => par$oid_documento,
                                      par$oid_doc_formulario_accion => var$doc_no_sustituto,
                                      par$cod_estado_documento      => gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido,
                                      par$usuario                   => par$usuario,
                                      par$cod_cultura               => par$cod_cultura,
                                      par$inserts                   => var$inserts_int,
                                      par$selects                   => var$selects_int);
            par$inserts   := par$inserts + var$inserts_int;
            par$selects   := par$selects + var$selects_int;
            var$registros := var$registros + var$selects_int +
                             var$inserts_int;
          END IF;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     =>  'Llama sapr_pdocumento_###VERSION###.supd_estado_documento' ||
                                    ' - par$oid_documento: ' || par$oid_documento ||
                                    ' - par$cod_estado_documento (gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido): ' || gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido ||
                                    ' - par$usuario: ' || par$usuario ||
                                    ' - var$updates_int: ' || var$updates_int,
            par$cod_identificador   => var$cod_documento);

          /* actualizar estado documento padre para sustituido*/
          sapr_pdocumento_###VERSION###.supd_estado_documento(par$oid_documento        => var$oid_documento_padre,
                                                         par$cod_estado_documento => gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido,
                                                         par$usuario              => par$usuario,
                                                         par$updates              => var$updates_int);
          par$updates   := par$updates + var$updates_int;
          var$registros := var$registros + var$updates_int;

        ELSIF par$cod_estado_documento =
              gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado THEN

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     =>  'ELSEIF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado THEN' ||
                                    '- Llama sapr_pdocumento_###VERSION###.saccion_contable_ejecutar' ||
                                    ' - par$oid_documento: ' || par$oid_documento ||
                                    ' - var$doc_no_sustituto: ' || var$doc_no_sustituto ||
                                    ' - par$cod_estado_documento(gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido): ' || gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido ||
                                    ' - par$cod_cultura: ' || par$cod_cultura ||
                                    ' - par$usuario: ' || par$usuario ||
                                    ' - var$inserts_int: ' || var$inserts_int ||
                                    ' - var$selects_int: ' || var$selects_int,
            par$cod_identificador   => var$cod_documento);

          saccion_contable_ejecutar(par$oid_documento             => par$oid_documento,
                                    par$oid_doc_formulario_accion => var$doc_no_sustituto,
                                    par$cod_estado_documento      => gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido,
                                    par$usuario                   => par$usuario,
                                    par$cod_cultura               => par$cod_cultura,
                                    par$inserts                   => var$inserts_int,
                                    par$selects                   => var$selects_int);

          par$inserts   := par$inserts + var$inserts_int;
          par$selects   := par$selects + var$selects_int;
          var$registros := var$registros + var$selects_int +
                           var$inserts_int;

          par$selects   := par$selects + 1;
          var$registros := var$registros + 1;
        END IF;

      END IF;
      par$selects   := par$selects + 1;
      var$registros := var$registros + 1;

      /* ejecutar accion contable */
        IF par$cod_estado_documento =
           gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido THEN

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido THEN' ||
                                      ' - Llama sapr_pdocumento_###VERSION###.saccion_contable_revertir' ||
                                      ' - par$oid_documento: ' || var$oid_documento_padre ||
                                      ' - par$oid_documento_padre: ' || null ||
                                      ' - par$cod_estado_documento: ' || par$cod_estado_documento ||
                                      ' - par$usuario: ' || par$usuario ||
                                      ' - par$cod_cultura: ' || par$cod_cultura ||
                                      ' - var$inserts_int: ' || var$inserts_int ||
                                      ' - var$selects_int: ' || var$selects_int,
              par$cod_identificador   => var$cod_documento);

          /* SUSTITUICION */
          saccion_contable_revertir(par$oid_documento        => var$oid_documento_padre,
                                    par$oid_documento_padre  => null,
                                    par$cod_estado_documento => par$cod_estado_documento,
                                    par$usuario              => par$usuario,
                                    par$cod_cultura          => par$cod_cultura,
                                    par$inserts              => var$inserts_int,
                                    par$selects              => var$selects_int);
          par$inserts   := par$inserts + var$inserts_int;
          par$selects   := par$selects + var$selects_int;
          var$registros := var$registros + var$selects_int + var$inserts_int;

        ELSIF fvalidar_accion_rechazar(par$oid_formulario       => var$oid_formulario,
                                       par$cod_estado_documento => par$cod_estado_documento) THEN

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'ELSE IF fvalidar_accion_rechazar' ||
                                      ' - par$oid_formulario: ' || var$oid_formulario ||
                                      ' - par$cod_estado_documento: ' || par$cod_estado_documento ||
                                      ' - Luego llama: ' || chr(10) || chr(13) ||
                                      'sapr_pdocumento_###VERSION###.saccion_contable_revertir' ||
                                      ' - par$oid_documento: ' || par$oid_documento ||
                                      ' - par$oid_documento_padre: ' || null ||
                                      ' - par$cod_estado_documento: ' || par$cod_estado_documento ||
                                      ' - par$usuario: ' || par$usuario ||
                                      ' - par$cod_cultura: ' || par$cod_cultura ||
                                      ' - var$inserts_int: ' || var$inserts_int ||
                                      ' - var$selects_int: ' || var$selects_int,
              par$cod_identificador   => var$cod_documento);

          /* RECHAZADO - Cuando no tiene accion contable configurada es hecho un revertir */
          saccion_contable_revertir(par$oid_documento        => par$oid_documento,
                                    par$oid_documento_padre  => null,
                                    par$cod_estado_documento => par$cod_estado_documento,
                                    par$usuario              => par$usuario,
                                    par$cod_cultura          => par$cod_cultura,
                                    par$inserts              => var$inserts_int,
                                    par$selects              => var$selects_int);
          par$inserts   := par$inserts + var$inserts_int;
          par$selects   := par$selects + var$selects_int;
          var$registros := var$registros + var$selects_int + var$inserts_int;

        ELSE

          IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                                par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracActa) AND
             par$cod_estado_documento =
             gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado THEN


            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     =>  'sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario => var$oid_formulario, par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracActa) AND par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado' || chr(13) ||
                                        'par$oid_formulario: ' || var$oid_formulario ||
                                        ' - par$cod_carasteristica: ' || gepr_pcomon_###VERSION###.const$CodCaracActa ||
                                        ' - par$cod_estado_documento: ' || par$cod_estado_documento || 
                                        ' - gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado || chr(13) ||
                                        'sapr_pdocumento_###VERSION###.saccion_contable_revertir' ||
                                        ' - par$oid_documento: ' || var$oid_documento_padre ||
                                        ' - par$oid_documento_padre: ' || null ||
                                        ' - par$cod_estado_documento: ' || par$cod_estado_documento ||
                                        ' - par$usuario: ' || par$usuario ||
                                        ' - par$cod_cultura: ' || par$cod_cultura ||
                                        ' - var$inserts_int: ' || var$inserts_int ||
                                        ' - var$selects_int: ' || var$selects_int,
                par$cod_identificador   => var$cod_documento);

            /* Si es un documento de Acta y el estado es AC, es necesario hacer un contra-movimiento
            del documento padre */
            saccion_contable_revertir(par$oid_documento        => var$oid_documento_padre,
                                      par$oid_documento_padre  => null,
                                      par$cod_estado_documento => par$cod_estado_documento,
                                      par$usuario              => par$usuario,
                                      par$cod_cultura          => par$cod_cultura,
                                      par$inserts              => var$inserts_int,
                                      par$selects              => var$selects_int);
            par$inserts   := par$inserts + var$inserts_int;
            par$selects   := par$selects + var$selects_int;
            var$registros := var$registros + var$selects_int +
                             var$inserts_int;

          END IF;

          IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                                par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracClasificacion) AND
             par$cod_estado_documento =
             gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado THEN

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     =>  'sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario => var$oid_formulario, par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracClasificacion) AND par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado' || chr(13) ||
                                        'par$oid_formulario: ' || var$oid_formulario ||
                                        ' - par$cod_carasteristica: ' || gepr_pcomon_###VERSION###.const$CodCaracClasificacion ||
                                        ' - par$cod_estado_documento: ' || par$cod_estado_documento || 
                                        ' - gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado || chr(13) ||
                                        'sapr_pdocumento_###VERSION###.saccion_contable_classificac' ||
                                        ' - par$oid_documento: ' || var$oid_documento_padre ||
                                        ' - par$usuario: ' || par$usuario ||
                                        ' - par$cod_cultura: ' || par$cod_cultura ||
                                        ' - var$inserts_int: ' || var$inserts_int ||
                                        ' - var$selects_int: ' || var$selects_int,
                par$cod_identificador   => var$cod_documento);


            /* Por uma particularidade do destino, as a??es contaveis dos documentos de Classifica??o est?o fixos no codigo
            Os documentos de classifica??o devem fazer um contra movimento dos valores anteriores e acrescentar os novos valores */

            saccion_contable_classificac(par$oid_documento => par$oid_documento,
                                         par$usuario       => par$usuario,
                                         par$cod_cultura   => par$cod_cultura,
                                         par$inserts       => var$inserts_int,
                                         par$selects       => var$selects_int);
          ELSE
            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     =>  'NOT sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario => var$oid_formulario, par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracClasificacion) AND par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado' || chr(13) ||
                                        'par$oid_formulario: ' || var$oid_formulario ||
                                        ' - par$cod_carasteristica: ' || gepr_pcomon_###VERSION###.const$CodCaracClasificacion ||
                                        ' - par$cod_estado_documento: ' || par$cod_estado_documento || 
                                        ' - gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado || chr(13) ||
                                        'sapr_pdocumento_###VERSION###.saccion_contable_ejecutar' ||
                                        ' - par$oid_documento: ' || var$oid_documento_padre ||
                                        ' - par$oid_doc_formulario_accion: ' || var$doc_no_sustituto || 
                                        ' - par$cod_estado_documento: ' || par$cod_estado_documento || 
                                        ' - par$usuario: ' || par$usuario ||
                                        ' - par$cod_cultura: ' || par$cod_cultura ||
                                        ' - var$inserts_int: ' || var$inserts_int ||
                                        ' - var$selects_int: ' || var$selects_int,
                par$cod_identificador   => var$cod_documento);

            saccion_contable_ejecutar(par$oid_documento             => par$oid_documento,
                                      par$oid_doc_formulario_accion => var$doc_no_sustituto,
                                      par$cod_estado_documento      => par$cod_estado_documento,
                                      par$usuario                   => par$usuario,
                                      par$cod_cultura               => par$cod_cultura,
                                      par$inserts                   => var$inserts_int,
                                      par$selects                   => var$selects_int);

          END IF;

          par$inserts   := par$inserts + var$inserts_int;
          par$selects   := par$selects + var$selects_int;
          var$registros := var$registros + var$selects_int + var$inserts_int;
        END IF;

      
      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     =>  'IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado THEN' || chr(13) ||
                                        ', par$cod_estado_documento: ' || par$cod_estado_documento || 
                                        ', gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado || chr(13) ||
                                        'ELSEIF var$bol_gestion_elem or var$bol_gestion_contenedor THEN' || chr(13) ||
                                        'var$bol_gestion_elem: ' || 
                                        case 
                                          when var$bol_gestion_elem then 'true'
                                          else 'false'
                                        end ||
                                        ' - var$bol_gestion_contenedor: ' || 
                                        case 
                                          when var$bol_gestion_contenedor then 'true'
                                          else 'false'
                                        end,
                par$cod_identificador   => var$cod_documento);
      /* ejecutar aceptar automatico */
      IF par$cod_estado_documento =
         gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado THEN

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     =>  'IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado THEN' || chr(13) ||
                                        ', par$cod_estado_documento: ' || par$cod_estado_documento || 
                                        ', gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado || chr(13) ||
                                        'var$bol_considera_recuentos: ' || var$bol_considera_recuentos,
                par$cod_identificador   => var$cod_documento);


        IF var$bol_considera_recuentos = 1 THEN
           update sapr_tdocumento
              set bol_saldo_suprimido = 0
            where oid_documento = par$oid_documento;
        END IF;
        
        SELECT d.oid_sector_origen, d.oid_sector_destino, d.rowver
          INTO var$oid_sector_origen, var$oid_sector_destino, par$rowver
          FROM sapr_tdocumento d
         WHERE d.oid_documento = par$oid_documento;

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     =>  'IF var$oid_sector_origen = var$oid_sector_destino OR sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario => var$oid_formulario, par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic) THEN' || chr(13) ||
                                        ' - var$oid_sector_origen: ' || var$oid_sector_origen || 
                                        ' - var$oid_sector_destino: ' || var$oid_sector_destino || 
                                        ' - par$oid_formulario: ' || var$oid_formulario || 
                                        ' - par$cod_caracteristica: ' || gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic || 
                                        ' - gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado,
                par$cod_identificador   => var$cod_documento);

        IF var$oid_sector_origen = var$oid_sector_destino OR
           sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                              par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic) THEN

          par$cod_estado_documento := gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'TRUE IF var$oid_sector_origen = var$oid_sector_destino OR sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario => var$oid_formulario, par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic) THEN' || chr(13) ||
                                      ' - var$oid_sector_origen: ' || var$oid_sector_origen || 
                                      ' - var$oid_sector_destino: ' || var$oid_sector_destino || 
                                      ' - par$oid_formulario: ' || var$oid_formulario || 
                                      ' - par$cod_caracteristica: ' || gepr_pcomon_###VERSION###.const$CodCaracAceptAutomatic || 
                                      ' - gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado: ' || gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado || chr(13) ||
                                      'sejecutar_doc(' ||
                                      'par$oid_llamada: ' || par$oid_llamada ||
                                      ', par$oid_documento: ' || par$oid_documento ||
                                      ', par$cod_estado_documento' || par$cod_estado_documento ||
                                      ', par$cod_comprobante: ' || var$cod_comprobante ||
                                      ', par$rowver: ' || par$rowver ||
                                      ', par$usuario: ' || par$usuario ||
                                      ', par$cod_cultura: ' || par$cod_cultura  ||
                                      ', par$info_ejecucion: ' || par$info_ejecucion ||
                                      ', par$hacer_commit: ' || par$hacer_commit ||
                                      ', par$inserts: ' || var$inserts_int ||
                                      ', par$updates: ' || var$updates_int ||
                                      ', par$deletes: ' || var$deletes_int ||
                                      ', par$selects: ' || var$selects_int ||
                                      ', par$cod_ejecucion: ' || par$cod_ejecucion ||  ')',
              par$cod_identificador   => var$cod_documento);

          sejecutar_doc(par$oid_llamada          => par$oid_llamada,
                        par$oid_documento        => par$oid_documento,
                        par$cod_estado_documento => par$cod_estado_documento,
                        par$cod_comprobante      => var$cod_comprobante,
                        par$rowver               => par$rowver,
                        par$usuario              => par$usuario,
                        par$cod_cultura          => par$cod_cultura,
                        par$info_ejecucion       => par$info_ejecucion,
                        par$hacer_commit         => par$hacer_commit,
                        par$inserts              => var$inserts_int,
                        par$updates              => var$updates_int,
                        par$deletes              => var$deletes_int,
                        par$selects              => var$selects_int,
                        par$cod_ejecucion        => par$cod_ejecucion);

          par$inserts   := par$inserts + var$inserts_int;
          par$updates   := par$updates + var$updates_int;
          par$deletes   := par$deletes + var$deletes_int;
          par$selects   := par$selects + var$selects_int;
          var$registros := var$registros + var$selects_int +
                           var$inserts_int + var$deletes_int +
                           var$updates_int;

        END IF;

      ELSIF var$bol_gestion_elem or var$bol_gestion_contenedor THEN


        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
          par$des_version     => const$version,
          par$des_detalle     =>  'ELSEIF var$bol_gestion_elem or var$bol_gestion_contenedor THEN' || chr(13) ||
                                  'var$bol_gestion_elem: ' ||
                                  case 
                                    when var$bol_gestion_elem then 'true'
                                    else 'false'
                                  end ||
                                  ' - var$bol_gestion_contenedor: ' ||
                                  case 
                                    when var$bol_gestion_contenedor then 'true'
                                    else 'false'
                                  end || chr(13) ||
                                  'selemento_actualizar_estado(' ||
                                  'par$oid_documento: ' || par$oid_documento || 
                                  ', par$cod_estado_documento: ' || par$cod_estado_documento ||
                                  ', par$oid_formulario: '  || var$oid_formulario ||
                                  ', par$bol_sustituicion: ' || 
                                  case 
                                    when var$bol_sustituicion then 'true'
                                    else 'false'
                                  end ||
                                  ', par$oid_documento_padre: ' ||  var$oid_documento_padre ||
                                  ', par$usuario: ' || par$usuario ||
                                  ', par$updates: ' || var$updates_int ||
                                  ', par$selects: ' || var$selects_int || ')',
          par$cod_identificador   => var$cod_documento);
        /* actualizar estado de los elementos del documento */
        selemento_actualizar_estado(par$oid_documento        => par$oid_documento,
                                    par$cod_estado_documento => par$cod_estado_documento,
                                    par$oid_formulario       => var$oid_formulario,
                                    par$bol_sustituicion     => var$bol_sustituicion,
                                    par$oid_documento_padre  => var$oid_documento_padre,
                                    par$usuario              => par$usuario,
                                    par$updates              => var$updates_int,
                                    par$selects              => var$selects_int);
        par$inserts := par$inserts + var$inserts_int;
        par$selects := par$selects + var$selects_int;

      END IF;

      /* Relacionar periodo */
      IF (var$cod_estado_doc_inicial = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado) THEN

        IF SAPR_PPERIODO_###VERSION###.fregla_doc_periodo_ac(par$oid_llamada   => par$oid_llamada,
                                                                    par$oid_documento => par$oid_documento) THEN

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     =>  'Cumple las reglas para relacionar a período',
                      par$cod_identificador   => var$cod_documento);

      
          SELECT COD_SECTOR
            INTO var$cod_sector_mae
            FROM GEPR_TSECTOR S
            WHERE S.OID_SECTOR = var$oid_sector_destino;

          SELECT CA.COD_CANAL, SC.COD_SUBCANAL, SC.OID_SUBCANAL, D.FYH_GESTION, PTO.OID_PTO_SERVICIO
            INTO var$cod_canal_mae, var$cod_subcanal_mae, var$oid_subcanal_mae, var$fyh_gestion_mae, var$oid_pto_servicio
            FROM SAPR_TDOCUMENTO D
            INNER JOIN SAPR_TCUENTA C
              ON C.OID_CUENTA = D.OID_CUENTA_DESTINO
            INNER JOIN GEPR_TSUBCANAL SC
              ON SC.OID_SUBCANAL = C.OID_SUBCANAL
            INNER JOIN GEPR_TCANAL CA
              ON CA.OID_CANAL = SC.OID_CANAL
            LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
              ON PTO.OID_PTO_SERVICIO = C.OID_PTO_SERVICIO
            WHERE D.OID_DOCUMENTO = par$oid_documento;
    
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'Llamada a procedure SAPR_PPERIODO_###VERSION###.sverificar_periodo',
              par$cod_identificador   => var$cod_documento);

          SAPR_PPERIODO_###VERSION###.sverificar_periodo(par$oid_llamada            => par$oid_llamada,
                                                par$cod_canal              => var$cod_canal_mae,
                                                par$cod_subcanal           => var$cod_subcanal_mae,
                                                par$oid_divisa             => var$oid_divisa,
                                                par$cod_sector             => var$cod_sector_mae,
                                                par$oid_pto_servicio       => var$oid_pto_servicio,
                                                par$cod_ajeno              => NULL,
                                                par$fyh_gestion            => var$fyh_gestion_mae,
                                                par$collection_id          => var$collection_id,
                                                par$oid_documento          => par$oid_documento,
                                                par$cod_usuario            => par$usuario,
                                                par$cod_cultura            => par$cod_cultura,
                                                par$info_ejecucion         => par$info_ejecucion,
                                                par$oid_maquina            => var$oid_maquina,
                                                par$oid_planificacion      => var$oid_planificacion,
                                                par$oid_periodo            => var$oid_periodo,
                                                par$cod_tipo_planificacion => var$cod_tipo_planificacion,
                                                par$debe_crear_periodo_ac  => var$debe_crear_periodo_ac,
                                                par$debe_actualizar_col_id => var$debe_actualizar_col_id,
                                                par$fyh_fin_periodo        => var$fyh_fin_periodo,
                                                par$cod_validaciones       => var$cod_validaciones,
                                                par$des_validaciones       => var$des_validaciones,                                                    
                                                par$cod_ejecucion          => par$cod_ejecucion);

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                        par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                        par$des_version     => const$version,
                        par$des_detalle     =>  'Posterior a llamada a procedure SAPR_PPERIODO_###VERSION###.sverificar_periodo. Variables de salida: ' || 
                                                ', par$oid_maquina: '             || var$oid_maquina ||
                                                ', par$oid_planificacion: '       || var$oid_planificacion ||
                                                ', par$oid_periodo: '             || var$oid_periodo ||
                                                ', par$cod_tipo_planificacion: '  || var$cod_tipo_planificacion ||
                                                ', par$debe_crear_periodo_ac: '   || var$debe_crear_periodo_ac ||
                                                ', par$debe_actualizar_col_id: '  || var$debe_actualizar_col_id ||
                                                ', par$fyh_fin_periodo: '         || var$fyh_fin_periodo,
                        par$cod_identificador   => var$cod_documento);

          IF var$debe_crear_periodo_ac = 1 THEN
              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'debe_crear_periodo_ac',
                    par$cod_identificador   => var$cod_documento);


              /*Obtengo los datos de periodo original para crear un nuevo periodo*/
              BEGIN 
                SELECT PERI.OID_ESTADO_PERIODO, PERI.OID_TIPO_PERIODO, PERI.OID_SUBCANAL, PERI.OID_DIVISA 
                  INTO var$oid_estado_periodo, var$oid_tipo_periodo, var$oid_subcanal_peri, var$oid_divisa_peri
                FROM SAPR_TPERIODO PERI
                WHERE PERI.OID_PERIODO = var$oid_periodo;
              EXCEPTION
                WHEN no_data_found THEN
                  var$oid_estado_periodo := NULL;
                  var$oid_tipo_periodo := NULL;
              END;

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Se crea nuevo período',
                    par$cod_identificador   => var$cod_documento);

              SAPR_PPERIODO_###VERSION###.sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                        par$oid_periodo               => var$oid_periodo_ac,
                        par$oid_planificacion         => var$oid_planificacion,
                        par$oid_maquina               => var$oid_maquina,
                        par$oid_estado_periodo        => var$oid_estado_periodo,
                        par$fyh_inicio                => var$fyh_gestion_mae,
                        par$fyh_fin                   => var$fyh_fin_periodo,
                        par$oid_tipo_periodo          => var$oid_tipo_periodo,
                        par$collection_id             => var$collection_id,
                        par$oid_subcanal              => var$oid_subcanal_peri,
                        par$oid_divisa                => var$oid_divisa_peri,
                        par$cod_periodo_confirmacion  => NULL,
                        par$cod_usuario               => par$usuario);

              SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     => 'Se actualiza fecha fin del período anterior restando un segundo a la fecha de inicio del período creado',
                    par$cod_identificador   => var$cod_documento);

              SAPR_PPERIODO_###VERSION###.sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                        par$oid_periodo               => var$oid_periodo,
                        par$oid_planificacion         => NULL,
                        par$oid_maquina               => NULL,
                        par$oid_estado_periodo        => NULL,
                        par$fyh_inicio                => NULL,
                        par$fyh_fin                   => var$fyh_gestion_mae - (1/60/60/24),
                        par$oid_tipo_periodo          => NULL,
                        par$collection_id             => NULL,
                        par$oid_subcanal              => NULL,
                        par$oid_divisa                => NULL,
                        par$cod_periodo_confirmacion  => NULL,
                        par$cod_usuario               => par$usuario);
          END IF;
          IF var$debe_actualizar_col_id = 1 THEN

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     => 'debe_actualizar_col_id',
              par$cod_identificador   => var$cod_documento);
            
                SAPR_PPERIODO_###VERSION###.sins_upd_tperiodo(par$oid_llamada         => par$oid_llamada,
                        par$oid_periodo               => var$oid_periodo,
                        par$oid_planificacion         => NULL,
                        par$oid_maquina               => NULL,
                        par$oid_estado_periodo        => NULL,
                        par$fyh_inicio                => NULL,
                        par$fyh_fin                   => NULL,
                        par$oid_tipo_periodo          => NULL,
                        par$collection_id             => var$collection_id,
                        par$cod_periodo_confirmacion  => NULL,
                        par$cod_usuario               => par$usuario);
          END IF;

          IF var$oid_maquina IS NOT NULL AND var$oid_planificacion IS NOT NULL AND var$oid_periodo IS NOT NULL 
            AND var$cod_tipo_planificacion <> 'ONLINE' THEN

            SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     =>  'TRUE IF var$oid_maquina IS NOT NULL AND var$oid_planificacion IS NOT NULL AND var$oid_periodo IS NOT NULL' ||
                                        '   AND var$cod_tipo_planificacion <> ' || chr(39) || 'ONLINE' || chr(39) || ' THEN' || chr(13) ||
                                        'SAPR_PPERIODO_###VERSION###.srelacionar_periodo(' ||
                                        'par$oid_llamada: ' || par$oid_llamada ||
                                        ', par$oid_documento: ' || par$oid_documento ||
                                        ', par$cod_usuario: ' || par$usuario ||
                                        ', par$cod_cultura: ' || par$cod_cultura ||
                                        ', par$info_ejecucion: ' || par$info_ejecucion ||
                                        ', par$hacer_commit: 0' ||
                                        ', par$validaciones: is out'  ||
                                        ', par$cod_ejecucion: is out' || ')',
                par$cod_identificador   => var$cod_documento);

            IF var$oid_periodo_ac is not null THEN 
                /* Relacionar periodo acreditacion recientemente creado*/
                SAPR_PPERIODO_###VERSION###.srelacionar_periodo(par$oid_llamada => par$oid_llamada,
                                                        par$oid_documento  => par$oid_documento,
                                                        par$oid_periodo    => var$oid_periodo_ac,
                                                        par$cod_usuario    => par$usuario,
                                                        par$cod_cultura    => par$cod_cultura,
                                                        par$info_ejecucion => par$info_ejecucion,
                                                        par$hacer_commit   => 0,
                                                        par$validaciones   => var$des_validaciones,
                                                        par$cod_ejecucion  => par$cod_ejecucion);

                SAPR_PPERIODO_###VERSION###.svalidar_periodo_recojo(
                        par$oid_llamada                => par$oid_llamada,
                        par$oid_maquina                => var$oid_maquina,
                        par$oid_planificacion          => var$oid_planificacion,
                        par$cod_usuario                => par$usuario,
                        par$oid_periodo_ac             => var$oid_periodo_ac,   
                        par$cod_subcanal               => var$cod_subcanal_mae,
                        par$oid_divisa                 => var$oid_divisa,
                        par$cod_cultura                => par$cod_cultura,
                        par$collection_id              => var$collection_id,
                        par$oid_documento              => par$oid_documento,
                        par$oid_periodo_re             => var$oid_periodo_re);

                SAPR_PPERIODO_###VERSION###.svalidar_periodo_boveda(
                        par$oid_llamada                => par$oid_llamada,
                        par$oid_maquina                => var$oid_maquina,
                        par$oid_planificacion          => var$oid_planificacion,
                        par$cod_usuario                => par$usuario,
                        par$oid_periodo_ac             => var$oid_periodo_ac,   
                        par$oid_periodo_re             => var$oid_periodo_re,  
                        par$cod_subcanal               => var$cod_subcanal_mae,
                        par$oid_divisa                 => var$oid_divisa,
                        par$cod_cultura                => par$cod_cultura,
                        par$collection_id              => var$collection_id,
                        par$oid_documento              => par$oid_documento);
            ELSE
                /* Relacionar periodo */
                SAPR_PPERIODO_###VERSION###.srelacionar_periodo(par$oid_llamada => par$oid_llamada,
                                                        par$oid_documento  => par$oid_documento,
                                                        par$oid_periodo    => var$oid_periodo,
                                                        par$cod_usuario    => par$usuario,
                                                        par$cod_cultura    => par$cod_cultura,
                                                        par$info_ejecucion => par$info_ejecucion,
                                                        par$hacer_commit   => 0,
                                                        par$validaciones   => var$des_validaciones,
                                                        par$cod_ejecucion  => par$cod_ejecucion);

                  SAPR_PPERIODO_###VERSION###.svalidar_periodo_recojo(
                        par$oid_llamada                => par$oid_llamada,
                        par$oid_maquina                => var$oid_maquina,
                        par$oid_planificacion          => var$oid_planificacion,
                        par$cod_usuario                => par$usuario,
                        par$oid_periodo_ac             => var$oid_periodo,   
                        par$cod_subcanal               => var$cod_subcanal_mae,
                        par$oid_divisa                 => var$oid_divisa,
                        par$cod_cultura                => par$cod_cultura,
                        par$collection_id              => var$collection_id,
                        par$oid_documento              => par$oid_documento,
                        par$oid_periodo_re             => var$oid_periodo_re);

                SAPR_PPERIODO_###VERSION###.svalidar_periodo_boveda(
                        par$oid_llamada                => par$oid_llamada,
                        par$oid_maquina                => var$oid_maquina,
                        par$oid_planificacion          => var$oid_planificacion,
                        par$cod_usuario                => par$usuario,
                        par$oid_periodo_ac             => var$oid_periodo,   
                        par$oid_periodo_re             => var$oid_periodo_re,  
                        par$cod_subcanal               => var$cod_subcanal_mae,
                        par$oid_divisa                 => var$oid_divisa,
                        par$cod_cultura                => par$cod_cultura,
                        par$collection_id              => var$collection_id,
                        par$oid_documento              => par$oid_documento);
            END IF;
          END IF;

        END IF;

        IF var$cod_formulario = 'MAESOC' OR var$cod_formulario = 'MAESOD' THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                  par$des_version     => const$version,
                  par$des_detalle     =>  'Llamada al procedure srelacionar_mov_shipout_per',
                  par$cod_identificador   => var$cod_documento);

          SAPR_PPERIODO_###VERSION###.srelacionar_mov_shipout_per
                                      (par$oid_llamada          =>     par$oid_llamada,
                                      par$oid_documento        =>     par$oid_documento,
                                      par$cod_cultura          =>     par$cod_cultura,
                                      par$info_ejecucion       =>     par$info_ejecucion,
                                      par$cod_usuario          =>     par$usuario);
        
        END IF;
      
      
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                    par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                    par$des_version     => const$version,
                    par$des_detalle     =>  'Llamada al procedure svalidar_corte',
                    par$cod_identificador   => var$cod_documento);

        SAPR_PPERIODO_###VERSION###.svalidar_corte
                                      (par$oid_llamada          => par$oid_llamada,
                                      par$cod_formulario       => var$cod_formulario,
                                      par$oid_documento        => par$oid_documento,
                                      par$fyh_gestion          => var$fyh_gestion_mae,
                                      par$cod_subcanal         => var$cod_subcanal_mae,
                                      par$oid_divisa           => var$oid_divisa,
                                      par$bol_corte_periodo    => var$bol_corte_periodo,
                                      par$oid_periodo          => var$oid_periodo,
                                      par$oid_tipo_periodo     => var$oid_tipo_periodo);
        
        IF (var$bol_corte_periodo = 1) THEN
          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     =>  'Llamada al procedure screar_corte',
                      par$cod_identificador   => var$cod_documento);

          SAPR_PPERIODO_###VERSION###.screar_corte
                                      (
                                        par$oid_llamada    => par$oid_llamada,
                                        par$oid_periodo    => var$oid_periodo,
                                        par$oid_tipo_periodo   => var$oid_tipo_periodo,
                                        par$oid_documento  => par$oid_documento,
                                        par$cod_usuario    => par$usuario);
        END IF;

        /*Grabamos en las tablas SAPR_TMOVIMIENTO*/
        SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
                par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                par$des_version     => const$version,
                par$des_detalle     =>  'Grabamos en las tablas SAPR_TMOVIMIENTO',
                par$cod_identificador   => var$cod_documento);

        SAPR_PMOVIMIENTO_###VERSION###.sgrabar_movimiento_por_doc(
          par$oid_llamada   => par$oid_llamada,
          par$oid_documento => par$oid_documento,
          par$cod_usuario => par$usuario
        );

      END IF;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'IF par$hacer_commit > 0 THEN' || chr(13)  ||
                                      'par$hacer_commit: ' || par$hacer_commit,
              par$cod_identificador   => var$cod_documento);
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

    ELSE
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
                                                           'S',
                                                           par$info_ejecucion,
                                                           'Formulario aun no es soportado.',
                                                           par$usuario,
                                                           par$cod_ejecucion);
    END IF;

  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;

      SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada         => par$oid_llamada,
              par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
              par$des_version     => const$version,
              par$des_detalle     =>  'Hizo rollback: ' || SQLERRM,
              par$cod_identificador   => var$cod_documento);

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

  end sejecutar_doc;


  /* fverificar_estado_grp */
  FUNCTION fverificar_estado_grp(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$estado              IN gepr_pcomon_###VERSION###.tipo$est_)
    RETURN BOOLEAN AS
    var$estado_actual gepr_pcomon_###VERSION###.tipo$est_;
  begin

    SELECT GD.COD_ESTADO
      INTO var$estado_actual
      FROM SAPR_TGRUPO_DOCUMENTO GD
     WHERE GD.OID_GRUPO_DOCUMENTO = par$oid_grupo_documento;

    RETURN fverificar_estado(var$estado_actual, par$estado);

  END fverificar_estado_grp;

  /* fverificar_estado_doc */
  FUNCTION fverificar_estado_doc(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$estado        IN gepr_pcomon_###VERSION###.tipo$est_)
    RETURN BOOLEAN AS
    var$estado_actual gepr_pcomon_###VERSION###.tipo$est_;
  begin

    SELECT D.COD_ESTADO
      INTO var$estado_actual
      FROM SAPR_TDOCUMENTO D
     WHERE D.OID_DOCUMENTO = par$oid_documento;

    RETURN fverificar_estado(var$estado_actual, par$estado);

  END fverificar_estado_doc;

  /* fverificar_estado */
  FUNCTION fverificar_estado(par$estado_actual IN gepr_pcomon_###VERSION###.tipo$est_,
                             par$estado        IN gepr_pcomon_###VERSION###.tipo$est_)
    RETURN BOOLEAN AS
    var$cant INT;
  begin

    IF par$estado_actual = 'NU' AND
       par$estado = gepr_pcomon_###VERSION###.const$CodEstadoDocEnCurso THEN
      var$cant := 1;
    ELSIF par$estado_actual = gepr_pcomon_###VERSION###.const$CodEstadoDocEnCurso AND
          par$estado IN
          (gepr_pcomon_###VERSION###.const$CodEstadoDocEnCurso,
           gepr_pcomon_###VERSION###.const$CodEstadoDocAnulado,
           gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado) THEN
      var$cant := 1;
    ELSIF par$estado_actual = gepr_pcomon_###VERSION###.const$CodEstadoDocConfirmado AND
          par$estado IN
          (gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado,
           gepr_pcomon_###VERSION###.const$CodEstadoDocRechazado) THEN
      var$cant := 1;
    ELSIF par$estado_actual = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado AND
          par$estado = gepr_pcomon_###VERSION###.const$CodEstadoDocSustituido THEN
      var$cant := 1;
    END IF;

    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);

  END fverificar_estado;

  /* svalidar_accion_rechazar */
  FUNCTION fvalidar_accion_rechazar(par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$est_)
    RETURN BOOLEAN AS
    var$cant INT;
  BEGIN

    IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocRechazado THEN

      SELECT COUNT(1)
        INTO var$cant
        FROM SAPR_TFORMULARIO F
       INNER JOIN SAPR_TACCION_CONTABLE AC
          ON AC.OID_ACCION_CONTABLE = F.OID_ACCION_CONTABLE
       INNER JOIN SAPR_TESTADOXACCION_CONTABLE EAC
          ON EAC.OID_ACCION_CONTABLE = AC.OID_ACCION_CONTABLE
       WHERE F.OID_FORMULARIO = par$oid_formulario
         AND AC.BOL_ACTIVO = 1
         AND EAC.COD_ESTADO = gepr_pcomon_###VERSION###.const$CodEstadoDocRechazado;

      IF NVL(var$cant, 0) = 0 THEN
        RETURN true;
      END IF;
    END IF;

    RETURN false;
  END fvalidar_accion_rechazar;

  /* fgenerar_comprobante_grp */
  PROCEDURE fgenerar_comprobante_grp(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_comprobante     OUT gepr_pcomon_###VERSION###.tipo$cod_) AS
    var$cod_delegacion gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planta     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sector     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_formulario gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_sector     gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_formulario gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    /* Obtener Codigo Delegacion, Planta, Sector Y Formulario */
    SELECT S.COD_SECTOR,
           P.COD_PLANTA,
           DE.COD_DELEGACION,
           F.COD_FORMULARIO,
           D.OID_SECTOR_ORIGEN,
           D.OID_FORMULARIO
      INTO var$cod_sector,
           var$cod_planta,
           var$cod_delegacion,
           var$cod_formulario,
           var$oid_sector,
           var$oid_formulario
      FROM SAPR_TGRUPO_DOCUMENTO D
     INNER JOIN GEPR_TSECTOR S
        ON S.OID_SECTOR = D.OID_SECTOR_ORIGEN
     INNER JOIN GEPR_TPLANTA P
        ON P.OID_PLANTA = S.OID_PLANTA
     INNER JOIN GEPR_TDELEGACION DE
        ON DE.OID_DELEGACION = P.OID_DELEGACION
     INNER JOIN SAPR_TFORMULARIO F
        ON F.OID_FORMULARIO = D.OID_FORMULARIO
     WHERE D.OID_GRUPO_DOCUMENTO = par$oid_grupo_documento;

    fgenerar_comprobante(par$cod_delegacion  => var$cod_delegacion,
                         par$cod_planta      => var$cod_planta,
                         par$cod_sector      => var$cod_sector,
                         par$cod_formulario  => var$cod_formulario,
                         par$oid_sector      => var$oid_sector,
                         par$oid_formulario  => var$oid_formulario,
                         par$cod_cultura     => par$cod_cultura,
                         par$cod_comprobante => par$cod_comprobante);

  END fgenerar_comprobante_grp;

  /* fgenerar_comprobante_doc */
  PROCEDURE fgenerar_comprobante_doc(par$oid_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$cod_cultura     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_comprobante OUT gepr_pcomon_###VERSION###.tipo$cod_) AS
    var$cod_delegacion gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planta     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sector     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_formulario gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_sector     gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_formulario gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    /* Obtener Codigo Delegacion, Planta, Sector Y Formulario */
    SELECT S.COD_SECTOR,
           P.COD_PLANTA,
           DE.COD_DELEGACION,
           F.COD_FORMULARIO,
           D.OID_SECTOR_ORIGEN,
           D.OID_FORMULARIO
      INTO var$cod_sector,
           var$cod_planta,
           var$cod_delegacion,
           var$cod_formulario,
           var$oid_sector,
           var$oid_formulario
      FROM SAPR_TDOCUMENTO D
     INNER JOIN GEPR_TSECTOR S
        ON S.OID_SECTOR = D.OID_SECTOR_ORIGEN
     INNER JOIN GEPR_TPLANTA P
        ON P.OID_PLANTA = S.OID_PLANTA
     INNER JOIN GEPR_TDELEGACION DE
        ON DE.OID_DELEGACION = P.OID_DELEGACION
     INNER JOIN SAPR_TFORMULARIO F
        ON F.OID_FORMULARIO = D.OID_FORMULARIO
     WHERE D.OID_DOCUMENTO = par$oid_documento;

    fgenerar_comprobante(par$cod_delegacion  => var$cod_delegacion,
                         par$cod_planta      => var$cod_planta,
                         par$cod_sector      => var$cod_sector,
                         par$cod_formulario  => var$cod_formulario,
                         par$oid_sector      => var$oid_sector,
                         par$oid_formulario  => var$oid_formulario,
                         par$cod_cultura     => par$cod_cultura,
                         par$cod_comprobante => par$cod_comprobante);

  END fgenerar_comprobante_doc;

  /* fgenerar_comprobante */
  PROCEDURE fgenerar_comprobante(par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_planta     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_sector     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$cod_formulario IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$oid_sector     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_,

                                 par$cod_comprobante OUT gepr_pcomon_###VERSION###.tipo$cod_) AS
    var$cod_delegacion gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_planta     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_sector     gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_formulario gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_secuencial gepr_pcomon_###VERSION###.tipo$cod_;
    var$basadoEnReglas NUMBER(1) := 0;
  BEGIN

   /* Obtener Codigo Secuencia */
   var$cod_secuencial := FN_SEC_COMPROBANTE_###VERSION###(par$oid_sector,par$oid_formulario);


    /* Obtener Parametro IAC */
    SELECT PV.DES_VALOR_PARAMETRO
      INTO var$basadoEnReglas
      FROM GEPR_TPARAMETRO P
     INNER JOIN GEPR_TPARAMETRO_VALOR PV
        ON PV.OID_PARAMETRO = P.OID_PARAMETRO
     INNER JOIN GEPR_TAPLICACION AP
        ON AP.OID_APLICACION = P.OID_APLICACION
     WHERE P.COD_PARAMETRO = gepr_pcomon_###VERSION###.const$CodParamIACCompEnReglas
       AND AP.COD_APLICACION = gepr_pcomon_###VERSION###.const$codAplicacionSaldos
       AND PV.COD_IDENTIFICADOR_NIVEL =
           (SELECT D.COD_PAIS
              FROM GEPR_TDELEGACION D
             WHERE D.COD_DELEGACION = par$cod_delegacion);

    /* Configurar Codigo Comprobante */
    IF var$basadoEnReglas = 0 THEN

      SELECT SAPR_QDOC_COD_COMPROBANTE.NEXTVAL
        into var$cod_secuencial
        FROM DUAL;

      IF var$cod_secuencial IS NULL THEN
        var$cod_secuencial := '';
      END IF;

      IF length(var$cod_secuencial) > 18 THEN
        var$cod_secuencial := SUBSTR(var$cod_secuencial, -18, 18);
      END IF;

      par$cod_comprobante := lpad(var$cod_secuencial, 18, '0');

    ELSE

      SELECT SC.NEL_SECUENCIA
        INTO var$cod_secuencial
        FROM SAPR_TSECUENCIA_COMPROBANTE SC
       WHERE SC.OID_SECTOR = par$oid_sector
         AND SC.OID_FORMULARIO = par$oid_formulario;

      IF length(par$cod_sector) > 3 THEN
        var$cod_sector := SUBSTR(par$cod_sector, -3, 3);
      END IF;
      var$cod_sector := lpad(var$cod_sector, 3, '0');

      IF length(par$cod_planta) > 3 THEN
        var$cod_planta := SUBSTR(par$cod_planta, -3, 3);
      END IF;
      var$cod_planta := lpad(var$cod_planta, 3, '0');

      IF length(par$cod_delegacion) > 3 THEN
        var$cod_delegacion := SUBSTR(par$cod_delegacion, -3, 3);
      END IF;
      var$cod_delegacion := lpad(var$cod_delegacion, 3, '0');

      IF length(par$cod_formulario) > 2 THEN
        var$cod_formulario := SUBSTR(par$cod_formulario, -2, 2);
      END IF;
      var$cod_formulario := lpad(var$cod_formulario, 2, '0');

      IF length(var$cod_secuencial) > 7 THEN
        var$cod_secuencial := SUBSTR(var$cod_secuencial, -7, 7);
      END IF;
      var$cod_secuencial := lpad(var$cod_secuencial, 7, '0');

      par$cod_comprobante := var$cod_delegacion || var$cod_planta ||
                             var$cod_sector || var$cod_formulario ||
                             var$cod_secuencial;

    END IF;

  END fgenerar_comprobante;

  /* fform_implementado */
  FUNCTION fform_implementado(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN INTEGER IS
    var$cant INT := 0;
  BEGIN

    BEGIN

      SELECT COUNT(1)
        INTO var$cant
        FROM sapr_tformulario f
       INNER JOIN sapr_tcaractformxformulario c
          ON f.oid_formulario = c.oid_formulario
       INNER JOIN sapr_tcaract_formulario cf
          ON c.oid_caract_formulario = cf.oid_caract_formulario
       WHERE f.oid_formulario = par$oid_formulario
         AND cf.cod_caract_formulario in
             (gepr_pcomon_###VERSION###.const$CodCaracCierreCaja,
              gepr_pcomon_###VERSION###.const$CodCaracCierretesoro,
              gepr_pcomon_###VERSION###.const$CodCaracGestionRemes,
              gepr_pcomon_###VERSION###.const$CodCaracGestionBulto);

    EXCEPTION
      WHEN no_data_found THEN
        var$cant := 0;
    END;

    RETURN var$cant;

  END fform_implementado;

  /* Reverte a accio contabil del documento */
  PROCEDURE saccion_contable_revertir(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_documento_padre  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$usuario              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$permite_saldo_negativo    BOOLEAN := false;
    var$existe_transacion_destino BOOLEAN := false;
    var$oid_sector_destino        gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_documento_gravar      gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_documento_consultar   gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_formulario            gepr_pcomon_###VERSION###.tipo$oid_;
    var$inserts_int               gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int               gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN
    /* SE O DOCUMENTO PADRE FOI INFORMADO, E PORQUE ESSE DOCUMENTO PADRE ESTA CERTIFICADO
       ENTAO DEVE FAZER O ESTORNO DAS TRANSACOES DO DOCUMENTO PADRE, POREM AS TRANSACOES
       SERAO APLICADAS NO DOCUMENTO ATUAL
    */
    IF par$oid_documento_padre IS NOT NULL AND
       par$oid_documento IS NOT NULL THEN
      var$oid_documento_consultar := par$oid_documento_padre;
      var$oid_documento_gravar    := par$oid_documento;
    ELSE
      var$oid_documento_consultar := par$oid_documento;
      var$oid_documento_gravar    := par$oid_documento;
    END IF;

    /* RECUPERA O SECTOR DE DESTINO DO DOCUMENTO E SE O FORMULARIO PERMITE SALDO NEGATIVO */
    SELECT OID_SECTOR_DESTINO, oid_formulario
      INTO var$oid_sector_destino, var$oid_formulario
      FROM SAPR_TDOCUMENTO
     WHERE OID_DOCUMENTO = var$oid_documento_gravar;
    par$selects                := 0 + sql%ROWCOUNT;
    var$permite_saldo_negativo := sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario,
                                                                                     par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracPerSaldoNeg);
    par$selects                := par$selects + sql%ROWCOUNT;

    /* ==================== EFECTIVO ==========================================*/
    /*RECUPERA AS TRANSACOES QUE AINDA NAO FORAM REVERTIDAS PARA O DESTINO*/
    FOR rec$transacion_efectivo in (SELECT OID_TRANSACCION_EFECTIVO
                                      FROM SAPR_TTRANSACCION_EFECTIVO
                                     WHERE BOL_CONTRA_MOVIMIENTO = 0 /* solamente las transaciones que no fueran anuladas*/
                                       AND OID_DOCUMENTO =
                                           var$oid_documento_consultar
                                       AND COD_ESTADO_DOCUMENTO =
                                           gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado
                                       AND COD_TIPO_SITIO = 'D' /*DESTINO*/
                                     ORDER BY GMT_CREACION) LOOP

      /* se existem transaciones para o destino, as mesmas terao de ser desfeitas (contra-movimientos) */
      stransacion_efectivo_reverti(par$oid_documento            => var$oid_documento_gravar,
                                   par$cod_estado_documento     => par$cod_estado_documento,
                                   par$usuario                  => par$usuario,
                                   par$tipo_sitio               => 'D',
                                   par$oid_transaccion_efectivo => rec$transacion_efectivo.oid_transaccion_efectivo,
                                   par$permite_saldo_negativo   => var$permite_saldo_negativo,
                                   par$cod_cultura              => par$cod_cultura,
                                   par$inserts                  => var$inserts_int,
                                   par$selects                  => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;

      /* SE PASSOU AQUI, ENTAO EXISTE TRANSACION DE DESTION */
      var$existe_transacion_destino := true;
    END LOOP rec$transacion_efectivo;
    par$selects := par$selects + 1;

    /* se no existem transacoes no destino, significa que devem existir trancoes para a origem
    sendo a origem igual ao parametro sector de origem (tendo o mesmo), elas deverao ser desfeitas */
    IF NOT var$existe_transacion_destino THEN
      FOR rec$transacion_efectivo IN (SELECT TE.OID_TRANSACCION_EFECTIVO,
                                             TE.COD_TIPO_SITIO
                                        FROM SAPR_TTRANSACCION_EFECTIVO TE
                                       INNER JOIN SAPR_TCUENTA C
                                          ON C.OID_CUENTA = TE.OID_CUENTA
                                       WHERE TE.BOL_CONTRA_MOVIMIENTO = 0 /* somente transacoes que nao foram anuladas */
                                         AND TE.OID_DOCUMENTO =
                                             var$oid_documento_consultar
                                         AND TE.COD_ESTADO_DOCUMENTO =
                                             gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado /* SOMENTE TRANSACIONES ACEITAS*/
                                         AND C.OID_SECTOR =
                                             var$oid_sector_destino
                                       ORDER BY TE.GMT_CREACION) LOOP
        /* se existem transacoes para o destino, as mesmas terao de ser desfeitas (contra-movimientos) */
        stransacion_efectivo_reverti(par$oid_documento            => var$oid_documento_gravar,
                                     par$cod_estado_documento     => par$cod_estado_documento,
                                     par$usuario                  => par$usuario,
                                     par$tipo_sitio               => rec$transacion_efectivo.cod_tipo_sitio,
                                     par$oid_transaccion_efectivo => rec$transacion_efectivo.oid_transaccion_efectivo,
                                     par$permite_saldo_negativo   => var$permite_saldo_negativo,
                                     par$cod_cultura              => par$cod_cultura,
                                     par$inserts                  => var$inserts_int,
                                     par$selects                  => var$selects_int);
        par$inserts := par$inserts + var$inserts_int;
        par$selects := par$selects + var$selects_int;

      END LOOP rec$transacion_efectivo;
      par$selects := par$selects + 1;
    END IF;

    /* ==================== MEDIO PAGO ==========================================*/
    /*RECUPERA AS TRANSACOES QUE AINDA NAO FORAM REVERTIDAS PARA O DESTINO*/
    var$existe_transacion_destino := false;
    FOR rec$transacion_medio_pago in (SELECT OID_TRANSACCION_MEDIO_PAGO
                                        FROM SAPR_TTRANSACCION_MEDIO_PAGO
                                       WHERE BOL_CONTRA_MOVIMIENTO = 0 /* solamente las transaciones que no fueran anuladas*/
                                         AND OID_DOCUMENTO =
                                             var$oid_documento_consultar
                                         AND COD_ESTADO_DOCUMENTO =
                                             gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado
                                         AND COD_TIPO_SITIO = 'D' /*DESTINO*/
                                       ORDER BY GMT_CREACION) LOOP

      /* se existem transaciones para o destino, as mesmas terao de ser desfeitas (contra-movimientos) */
      stransacion_medio_pg_reverti(par$oid_documento              => var$oid_documento_gravar,
                                   par$cod_estado_documento       => par$cod_estado_documento,
                                   par$usuario                    => par$usuario,
                                   par$tipo_sitio                 => 'D',
                                   par$oid_transaccion_medio_pago => rec$transacion_medio_pago.oid_transaccion_medio_pago,
                                   par$permite_saldo_negativo     => var$permite_saldo_negativo,
                                   par$cod_cultura                => par$cod_cultura,
                                   par$inserts                    => var$inserts_int,
                                   par$selects                    => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;

      /* SE PASSOU AQUI, ENTAO EXISTE TRANSACION DE DESTION */
      var$existe_transacion_destino := true;
    END LOOP rec$transacion_medio_pago;
    par$selects := par$selects + 1;

    /* se no existem transacoes no destino, significa que devem existir trancoes para a origem
    sendo a origem igual ao parametro sector de origem (tendo o mesmo), elas deverao ser desfeitas */
    IF NOT var$existe_transacion_destino THEN
      FOR rec$transacion_medio_pago IN (SELECT TMP.OID_TRANSACCION_MEDIO_PAGO,
                                               TMP.COD_TIPO_SITIO
                                          FROM SAPR_TTRANSACCION_MEDIO_PAGO TMP
                                         INNER JOIN SAPR_TCUENTA C
                                            ON C.OID_CUENTA = TMP.OID_CUENTA
                                         WHERE TMP.BOL_CONTRA_MOVIMIENTO = 0 /* somente transacoes que nao foram anuladas */
                                           AND TMP.OID_DOCUMENTO =
                                               var$oid_documento_consultar
                                           AND TMP.COD_ESTADO_DOCUMENTO =
                                               gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado /* SOMENTE TRANSACIONES ACEITAS*/
                                           AND C.OID_SECTOR =
                                               var$oid_sector_destino
                                         ORDER BY TMP.GMT_CREACION) LOOP
        /* se existem transacoes para o destino, as mesmas terao de ser desfeitas (contra-movimientos) */
        stransacion_medio_pg_reverti(par$oid_documento              => var$oid_documento_gravar,
                                     par$cod_estado_documento       => par$cod_estado_documento,
                                     par$usuario                    => par$usuario,
                                     par$tipo_sitio                 => rec$transacion_medio_pago.cod_tipo_sitio,
                                     par$oid_transaccion_medio_pago => rec$transacion_medio_pago.oid_transaccion_medio_pago,
                                     par$permite_saldo_negativo     => var$permite_saldo_negativo,
                                     par$cod_cultura                => par$cod_cultura,
                                     par$inserts                    => var$inserts_int,
                                     par$selects                    => var$selects_int);
        par$inserts := par$inserts + var$inserts_int;
        par$selects := par$selects + var$selects_int;

      END LOOP rec$transacion_medio_pago;
      par$selects := par$selects + 1;
    END IF;

  END saccion_contable_revertir;

  /* Reverte a transacao de efectivo do documento */
  PROCEDURE stransacion_efectivo_reverti(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_estado_documento     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$tipo_sitio               IN CHAR,
                                         par$oid_transaccion_efectivo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$permite_saldo_negativo   IN BOOLEAN,
                                         par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$oid_transaccion_efectivo gepr_pcomon_###VERSION###.tipo$oid_ := SYS_GUID;
    var$inserts_int              gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int              gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN

    INSERT INTO SAPR_TTRANSACCION_EFECTIVO
      (OID_TRANSACCION_EFECTIVO,
       OID_DOCUMENTO,
       OID_CUENTA,
       OID_CUENTA_SALDO,
       OID_DIVISA,
       OID_DENOMINACION,
       OID_CERTIFICADO,
       OID_UNIDAD_MEDIDA,
       COD_NIVEL_DETALLE,
       COD_TIPO_EFECTIVO_TOTAL,
       OID_CALIDAD,
       BOL_DISPONIBLE,
       NUM_IMPORTE,
       NEL_CANTIDAD,
       COD_ESTADO_DOCUMENTO,
       COD_TIPO_SITIO,
       COD_TIPO_MOVIMIENTO,
       OBS_TRANSACCION,
       COD_MIGRACION,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       BOL_CONTRA_MOVIMIENTO,
       DES_USUARIO_MODIFICACION,
       BOL_BLOQUEADO)
      SELECT var$oid_transaccion_efectivo,
             par$oid_documento,
             OID_CUENTA,
             OID_CUENTA_SALDO,
             OID_DIVISA,
             OID_DENOMINACION,
             NULL AS OID_CERTIFICADO,
             OID_UNIDAD_MEDIDA,
             COD_NIVEL_DETALLE,
             COD_TIPO_EFECTIVO_TOTAL,
             OID_CALIDAD,
             BOL_DISPONIBLE,
             NUM_IMPORTE * (-1), /* Inverte o valor */
             NEL_CANTIDAD * (-1), /* Inverte o valor */
             par$cod_estado_documento,
             par$tipo_sitio,
             COD_TIPO_MOVIMIENTO,
             OBS_TRANSACCION,
             COD_MIGRACION,
             FN_GMT_ZERO_###VERSION###,
             par$usuario,
             FN_GMT_ZERO_###VERSION###,
             1, /* Anulado */
             par$usuario,
             BOL_BLOQUEADO
        FROM SAPR_TTRANSACCION_EFECTIVO
       WHERE OID_TRANSACCION_EFECTIVO = par$oid_transaccion_efectivo;

    par$inserts := 0 + sql%ROWCOUNT;

    /* DEPOIS DE INSERIR A TRANSACAO DEVE ATUALIZAR O SALDO */
    ssaldo_efectivo_actualizar(par$oid_documento            => par$oid_documento,
                               par$oid_transaccion_efectivo => var$oid_transaccion_efectivo,
                               par$usuario                  => par$usuario,
                               par$permite_saldo_negativo   => par$permite_saldo_negativo,
                               par$cod_cultura              => par$cod_cultura,
                               par$inserts                  => var$inserts_int,
                               par$selects                  => var$selects_int);

    par$inserts := par$inserts + var$inserts_int;
    par$selects := par$selects + var$selects_int;

  END stransacion_efectivo_reverti;

  /* Reverte a transacao de medio pago do documento */
  PROCEDURE stransacion_medio_pg_reverti(par$oid_documento              IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_estado_documento       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$usuario                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$tipo_sitio                 IN CHAR,
                                         par$oid_transaccion_medio_pago IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$permite_saldo_negativo     IN BOOLEAN,
                                         par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$oid_transaccion_medio_pago gepr_pcomon_###VERSION###.tipo$oid_ := sys_guid;
    var$inserts_int                gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int                gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN

    INSERT INTO SAPR_TTRANSACCION_MEDIO_PAGO
      (OID_TRANSACCION_MEDIO_PAGO,
       OID_DOCUMENTO,
       OID_CUENTA,
       OID_CUENTA_SALDO,
       OID_DIVISA,
       OID_MEDIO_PAGO,
       OID_CERTIFICADO,
       COD_TIPO_MEDIO_PAGO,
       COD_NIVEL_DETALLE,
       BOL_DISPONIBLE,
       NUM_IMPORTE,
       NEL_CANTIDAD,
       COD_ESTADO_DOCUMENTO,
       COD_TIPO_SITIO,
       COD_TIPO_MOVIMIENTO,
       OBS_TRANSACCION,
       COD_MIGRACION,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       BOL_CONTRA_MOVIMIENTO,
       DES_USUARIO_MODIFICACION,
       BOL_BLOQUEADO)
      SELECT var$oid_transaccion_medio_pago,
             par$oid_documento,
             OID_CUENTA,
             OID_CUENTA_SALDO,
             OID_DIVISA,
             OID_MEDIO_PAGO,
             OID_CERTIFICADO,
             COD_TIPO_MEDIO_PAGO,
             COD_NIVEL_DETALLE,
             BOL_DISPONIBLE,
             NUM_IMPORTE * (-1), /* Inverte o valor */
             NEL_CANTIDAD * (-1), /* Inverte o valor */
             par$cod_estado_documento,
             par$tipo_sitio,
             COD_TIPO_MOVIMIENTO,
             OBS_TRANSACCION,
             COD_MIGRACION,
             FN_GMT_ZERO_###VERSION###,
             par$usuario,
             FN_GMT_ZERO_###VERSION###,
             1, /* ANULADO */
             par$usuario,
             BOL_BLOQUEADO
        FROM SAPR_TTRANSACCION_MEDIO_PAGO
       WHERE OID_TRANSACCION_MEDIO_PAGO = par$oid_transaccion_medio_pago;

    par$inserts := 0 + sql%ROWCOUNT;

    /* DEPOIS DE INSERIR A TRANSACAO DEVE ATUALIZAR O SALDO */
    ssaldo_medio_pago_actualizar(par$oid_transaccion_medio_pago => var$oid_transaccion_medio_pago,
                                 par$usuario                    => par$usuario,
                                 par$permite_saldo_negativo     => par$permite_saldo_negativo,
                                 par$cod_cultura                => par$cod_cultura,
                                 par$inserts                    => var$inserts_int,
                                 par$selects                    => var$selects_int);

    par$inserts := par$inserts + var$inserts_int;
    par$selects := par$selects + var$selects_int;

  end stransacion_medio_pg_reverti;

  /* Executa a acao contabil de efectivo do documento */
  PROCEDURE ssaldo_efectivo_actualizar(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_transaccion_efectivo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$permite_saldo_negativo   IN BOOLEAN,
                                       par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$gerou_saldo_negativo NUMBER(20, 2);
  BEGIN

    /* verifica se existe um saldo para a transacao,
       se existir atualiza o saldo
       senao existir entao insere
    */
    MERGE INTO SAPR_TSALDO_EFECTIVO Q1
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
                  A.BOL_BLOQUEADO
             FROM SAPR_TTRANSACCION_EFECTIVO A
            WHERE A.OID_TRANSACCION_EFECTIVO = par$oid_transaccion_efectivo) Q2
    ON (Q1.OID_CUENTA_SALDO = Q2.OID_CUENTA_SALDO AND Q1.OID_DIVISA = Q2.OID_DIVISA AND NVL(Q1.OID_DENOMINACION, '-') = NVL(Q2.OID_DENOMINACION, '-') AND NVL(Q1.COD_NIVEL_DETALLE, '-') = NVL(Q2.COD_NIVEL_DETALLE, '-') AND Q1.BOL_BLOQUEADO = Q2.BOL_BLOQUEADO AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE AND NVL(Q1.OID_CALIDAD, '-') = NVL(Q2.OID_CALIDAD, '-') AND NVL(Q1.COD_TIPO_EFECTIVO_TOTAL, '-') = NVL(Q2.COD_TIPO_EFECTIVO_TOTAL, '-') AND NVL(Q1.OID_UNIDAD_MEDIDA, '-') = NVL(Q2.OID_UNIDAD_MEDIDA, '-') AND ROWNUM <= 1)
    WHEN MATCHED THEN
      UPDATE
         SET Q1.NUM_IMPORTE              = NVL(Q1.NUM_IMPORTE, 0) +
                                           NVL(Q2.NUM_IMPORTE, 0),
             Q1.NEL_CANTIDAD             = NVL(Q1.NEL_CANTIDAD, 0) +
                                           NVL(Q2.NEL_CANTIDAD, 0),
             Q1.GMT_MODIFICACION         = FN_GMT_ZERO_###VERSION###,
             Q1.DES_USUARIO_MODIFICACION = par$usuario,
             Q1.OID_ULTIMA_TRANSACCION   = Q2.OID_TRANSACCION_EFECTIVO
    WHEN NOT MATCHED THEN
      INSERT
        (Q1.OID_SALDO_EFECTIVO,
         Q1.OID_CUENTA_SALDO,
         Q1.OID_ULTIMA_TRANSACCION,
         Q1.OID_DENOMINACION,
         Q1.OID_DIVISA,
         Q1.COD_NIVEL_DETALLE,
         Q1.COD_TIPO_EFECTIVO_TOTAL,
         Q1.OID_CALIDAD,
         Q1.BOL_DISPONIBLE,
         Q1.OID_UNIDAD_MEDIDA,
         Q1.NUM_IMPORTE,
         Q1.NEL_CANTIDAD,
         Q1.GMT_CREACION,
         Q1.DES_USUARIO_CREACION,
         Q1.GMT_MODIFICACION,
         Q1.DES_USUARIO_MODIFICACION,
         Q1.BOL_BLOQUEADO)
      VALUES
        (SYS_GUID(),
         Q2.OID_CUENTA_SALDO,
         par$oid_transaccion_efectivo,
         Q2.OID_DENOMINACION,
         Q2.OID_DIVISA,
         Q2.COD_NIVEL_DETALLE,
         Q2.COD_TIPO_EFECTIVO_TOTAL,
         Q2.OID_CALIDAD,
         Q2.BOL_DISPONIBLE,
         Q2.OID_UNIDAD_MEDIDA,
         Q2.NUM_IMPORTE,
         Q2.NEL_CANTIDAD,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         Q2.BOL_BLOQUEADO);

    par$inserts := 0 + sql%ROWCOUNT;

    /* DEPOIS DE ATUALIZAR O SALDO, DEVE VERIFICAR SE GEROU UM SALDO NEGATIVO E
       SE O FORMULARIO PERMITE SALDO NEGATIVO
    */

    /* SE O FORMULARIO NAO PERMITE SALDO NEGATIVO*/
    IF NOT par$permite_saldo_negativo then
      SELECT NUM_IMPORTE
        INTO var$gerou_saldo_negativo
        FROM SAPR_TSALDO_EFECTIVO
       WHERE OID_ULTIMA_TRANSACCION = par$oid_transaccion_efectivo;

      par$selects := 0 + sql%ROWCOUNT;

      /* SE GEROU UM SALDO NEGATIVO ENTAO DEVE ABORTAR O PROCESSO*/
      if var$gerou_saldo_negativo < 0 then
        /* GERA A EXCESAO QUE SERA TRATADA NA APLICACAO */
        raise_application_error(-20001,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    '028_msg_el_movimiento_produce_saldo_negativo',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDoc,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1));
      end if;
    end if;


    ssaldo_efectivo_hist_actu(par$oid_documento   =>par$oid_documento,
                                       par$oid_transaccion_efectivo => par$oid_transaccion_efectivo,
                                       par$usuario                  => par$usuario,
                                       par$permite_saldo_negativo   => par$permite_saldo_negativo,
                                       par$cod_cultura              => par$cod_cultura);

  end ssaldo_efectivo_actualizar;


   PROCEDURE ssaldo_efectivo_hist_actu(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_transaccion_efectivo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                       par$permite_saldo_negativo   IN BOOLEAN,
                                       par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_) AS
    var$gerou_saldo_negativo NUMBER(20, 2);

    var$fec_dia DATE;
    var$fec_dia_fecha DATE;

    var$fc_dia DATE;
    var$fc_dia_fecha DATE;
    var$fg_dia DATE;
    var$fg_dia_fecha DATE;
    var$bol_saldo_historico NUMBER(1);
    var$cod_fecha_saldo_historico VARCHAR2(10) ;

    var$oid_maquina  VARCHAR2(36) ;
    var$oid_pais     VARCHAR2(36) ;

 CURSOR  cur$fecha_saldo (par$fecha_inicio IN DATE)is 
with DIAS AS (
  select  (e.x + level -1)  dia,
   TO_DATE(to_char((e.x + level -1),'YYYY-MM-DD') || ' 23:59:59','YYYY-MM-DD HH24:MI:SS')  DIA_FECHA
          from (select
          trunc(par$fecha_inicio)   x, 
          TO_DATE(to_char(SYSDATE,'YYYY-MM-DD') || ' 00:00:00','YYYY-MM-DD HH24:MI:SS')  y from DUAL a ) e
          connect by level <= ceil(y-x)+1) 
          SELECT DIAS.DIA, DIAS.DIA_FECHA
          FROM DIAS 
          WHERE DIAS.DIA >  trunc(par$fecha_inicio)
          AND  DIAS.DIA <>  trunc(par$fecha_inicio);

  BEGIN




 SELECT 
    TO_DATE(to_char( FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.GMT_CREACION) ,'YYYY-MM-DD') || '00:00:00','YYYY-MM-DD HH24:MI:SS') AS FC_DIA,
    TO_TIMESTAMP_TZ(TO_CHAR(FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.GMT_CREACION), 'DD/MM/YYYY') || ' 23:59:59 ' || TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.GMT_CREACION) >= DELE.FYH_VERANO_INICIO AND FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.GMT_CREACION) <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' || TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.GMT_CREACION) >= DELE.FYH_VERANO_INICIO AND SYSDATE <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') FC_DIA_FECHA,
    TO_DATE(to_char(FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.FYH_GESTION),'YYYY-MM-DD') || '00:00:00','YYYY-MM-DD HH24:MI:SS') AS FG_DIA,
    TO_TIMESTAMP_TZ(TO_CHAR(FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.FYH_GESTION), 'DD/MM/YYYY') || ' 23:59:59 ' || TRIM(TO_CHAR(ROUND((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.FYH_GESTION) >= DELE.FYH_VERANO_INICIO AND FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.FYH_GESTION) <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END)/ 60), '09')) || ':' || TRIM(TO_CHAR(MOD((NVL(DELE.NEC_GMT_MINUTOS, 0) + CASE WHEN FN_GMT_DELEGACION_###VERSION###(CUEN.OID_DELEGACION, DOCU.FYH_GESTION) >= DELE.FYH_VERANO_INICIO AND SYSDATE <= DELE.FYH_VERANO_FIN THEN NVL(DELE.NEC_VERANO_AJUSTE, 0) ELSE 0 END), 60), '09')), 'DD/MM/YYYY HH24:MI:SS TZH:TZM') FG_DIA_FECHA,
    CLIE.BOL_SALDO_HISTORICO,
    CLIE.COD_FECHA_SALDO_HISTORICO,
    cuen.OID_MAQUINA,
    DELE.OID_PAIS
    into 

    var$fc_dia,
    var$fc_dia_fecha,
    var$fg_dia,
    var$fg_dia_fecha,
    var$bol_saldo_historico,
    var$cod_fecha_saldo_historico,
    var$oid_maquina,
    var$oid_pais
    FROM SAPR_TDOCUMENTO DOCU
    INNER JOIN SAPR_VCUENTA CUEN ON CUEN.OID_CUENTA = DOCU.OID_CUENTA_SALDO_ORIGEN
    INNER JOIN GEPR_TCLIENTE CLIE ON CUEN.OID_CLIENTE = CLIE.OID_CLIENTE
    INNER JOIN GEPR_TDELEGACION DELE ON DELE.OID_DELEGACION = CUEN.OID_DELEGACION
    WHERE OID_DOCUMENTO = par$oid_documento;

    /* verifica se existe um saldo para a transacao,
       se existir atualiza o saldo
       senao existir entao insere
    */
    if var$cod_fecha_saldo_historico = 'GESTION' THEN
        var$fec_dia := var$fg_dia;
        var$fec_dia_fecha := var$fg_dia_fecha;
    ELSE    
        var$fec_dia := var$fc_dia;
        var$fec_dia_fecha := var$fc_dia_fecha;
    END IF;

    IF var$bol_saldo_historico > 0 THEN

        MERGE INTO SAPR_TSALDO_EFECTIVO_HISTORICO Q1
        USING (SELECT A.OID_TRANSACCION_EFECTIVO,
                    A.OID_CUENTA_SALDO,
                    A.OID_DIVISA,
                    A.OID_DENOMINACION,
                    A.COD_NIVEL_DETALLE,
                    A.COD_TIPO_EFECTIVO_TOTAL,
                    A.NUM_IMPORTE,
                    A.NEL_CANTIDAD,
                    A.BOL_DISPONIBLE
                FROM SAPR_TTRANSACCION_EFECTIVO A
                WHERE A.OID_TRANSACCION_EFECTIVO = par$oid_transaccion_efectivo) Q2
        ON (var$fec_dia = Q1.fec_saldo AND Q1.OID_CUENTA = Q2.OID_CUENTA_SALDO AND Q1.OID_DIVISA = Q2.OID_DIVISA AND NVL(Q1.OID_DENOMINACION, '-') = NVL(Q2.OID_DENOMINACION, '-') AND NVL(Q1.COD_NIVEL_DETALLE, '-') = NVL(Q2.COD_NIVEL_DETALLE, '-') AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE AND ROWNUM <= 1)
        WHEN MATCHED THEN
        UPDATE
            SET Q1.NUM_IMPORTE              = NVL(Q1.NUM_IMPORTE, 0) +
                                            NVL(Q2.NUM_IMPORTE, 0),
                Q1.NEL_CANTIDAD             = NVL(Q1.NEL_CANTIDAD, 0) +
                                            NVL(Q2.NEL_CANTIDAD, 0),
                Q1.GMT_MODIFICACION         = FN_GMT_ZERO_###VERSION###,
                Q1.DES_USUARIO_MODIFICACION = par$usuario
        WHEN NOT MATCHED THEN
        INSERT
            (Q1.OID_SALDO_EFECTIVO_HISTORICO,
            Q1.OID_CUENTA,
            Q1.OID_MAQUINA,
            Q1.OID_PAIS,
            Q1.FEC_SALDO,
            Q1.HOR_SALDO,
            Q1.OID_DENOMINACION,
            Q1.OID_DIVISA,
            Q1.COD_NIVEL_DETALLE,
            Q1.BOL_DISPONIBLE,
            Q1.NUM_IMPORTE,
            Q1.NUM_IMPORTE_ANTERIOR,
            Q1.NEL_CANTIDAD,
            Q1.NEL_CANTIDAD_ANTERIOR,
            Q1.GMT_CREACION,
            Q1.DES_USUARIO_CREACION,
            Q1.GMT_MODIFICACION,
            Q1.DES_USUARIO_MODIFICACION)
        VALUES
            (SYS_GUID(),
            Q2.OID_CUENTA_SALDO,
            var$oid_maquina,
            var$oid_pais,            
            var$fec_dia,
            var$fec_dia_fecha,
            Q2.OID_DENOMINACION,
            Q2.OID_DIVISA,
            Q2.COD_NIVEL_DETALLE,
            Q2.BOL_DISPONIBLE,
            Q2.NUM_IMPORTE,
            0,
            Q2.NEL_CANTIDAD,
            0,
            FN_GMT_ZERO_###VERSION###,
            par$usuario,
            FN_GMT_ZERO_###VERSION###,
            par$usuario);






            FOR rec$fecha IN cur$fecha_saldo(var$fec_dia) LOOP
              --IF rec$canal.dia IS NOT NULL THEN
                  MERGE INTO SAPR_TSALDO_EFECTIVO_HISTORICO Q1
                  USING (SELECT A.OID_TRANSACCION_EFECTIVO,
                              A.OID_CUENTA_SALDO,
                              A.OID_DIVISA,
                              A.OID_DENOMINACION,
                              A.COD_NIVEL_DETALLE,
                              A.COD_TIPO_EFECTIVO_TOTAL,
                              A.NUM_IMPORTE,
                              A.NEL_CANTIDAD,
                              A.BOL_DISPONIBLE
                          FROM SAPR_TTRANSACCION_EFECTIVO A
                          WHERE A.OID_TRANSACCION_EFECTIVO = par$oid_transaccion_efectivo) Q2
                  ON (rec$fecha.dia = Q1.fec_saldo AND Q1.OID_CUENTA = Q2.OID_CUENTA_SALDO AND Q1.OID_DIVISA = Q2.OID_DIVISA AND NVL(Q1.OID_DENOMINACION, '-') = NVL(Q2.OID_DENOMINACION, '-') AND NVL(Q1.COD_NIVEL_DETALLE, '-') = NVL(Q2.COD_NIVEL_DETALLE, '-') AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE AND ROWNUM <= 1)
                  WHEN MATCHED THEN
                  UPDATE
                      SET Q1.NUM_IMPORTE_ANTERIOR     = NVL(Q1.NUM_IMPORTE_ANTERIOR, 0) +
                                                      NVL(Q2.NUM_IMPORTE, 0),
                          Q1.NEL_CANTIDAD_ANTERIOR    = NVL(Q1.NEL_CANTIDAD_ANTERIOR, 0) +
                                                      NVL(Q2.NEL_CANTIDAD, 0),
                          Q1.GMT_MODIFICACION         = FN_GMT_ZERO_###VERSION###,
                          Q1.DES_USUARIO_MODIFICACION = par$usuario
                  WHEN NOT MATCHED THEN
                  INSERT
                      (Q1.OID_SALDO_EFECTIVO_HISTORICO,
                      Q1.OID_CUENTA,
                      Q1.OID_MAQUINA,
                      Q1.OID_PAIS,
                      Q1.FEC_SALDO,
                      Q1.HOR_SALDO,
                      Q1.OID_DENOMINACION,
                      Q1.OID_DIVISA,
                      Q1.COD_NIVEL_DETALLE,
                      Q1.BOL_DISPONIBLE,
                      Q1.NUM_IMPORTE,
                      Q1.NUM_IMPORTE_ANTERIOR,
                      Q1.NEL_CANTIDAD,
                      Q1.NEL_CANTIDAD_ANTERIOR,
                      Q1.GMT_CREACION,
                      Q1.DES_USUARIO_CREACION,
                      Q1.GMT_MODIFICACION,
                      Q1.DES_USUARIO_MODIFICACION)
                  VALUES
                      (SYS_GUID(),
                      Q2.OID_CUENTA_SALDO,
                      var$oid_maquina,
                      var$oid_pais,            
                      rec$fecha.dia,
                      rec$fecha.dia_fecha,
                      Q2.OID_DENOMINACION,
                      Q2.OID_DIVISA,
                      Q2.COD_NIVEL_DETALLE,
                      Q2.BOL_DISPONIBLE,
                      0,
                      Q2.NUM_IMPORTE,
                      0,
                      Q2.NEL_CANTIDAD,
                      FN_GMT_ZERO_###VERSION###,
                      par$usuario,
                      FN_GMT_ZERO_###VERSION###,
                      par$usuario);


            END LOOP rec$canal;


    END IF;
  end ssaldo_efectivo_hist_actu;


  /* Executa a acao contabil de medio pago do documento */
  PROCEDURE ssaldo_medio_pago_actualizar(par$oid_transaccion_medio_pago IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$usuario                    IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$permite_saldo_negativo     IN BOOLEAN,
                                         par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$inserts                    OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects                    OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$gerou_saldo_negativo number(20, 2);
  BEGIN
    /* verifica se existe um saldo para a transacao,
       se existir atualiza o saldo
       senao existir entao insere
    */
    MERGE INTO SAPR_TSALDO_MEDIO_PAGO Q1
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
                  A.BOL_BLOQUEADO
             FROM SAPR_TTRANSACCION_MEDIO_PAGO A
            WHERE A.OID_TRANSACCION_MEDIO_PAGO =
                  par$oid_transaccion_medio_pago) Q2
    ON (Q1.OID_CUENTA_SALDO = Q2.OID_CUENTA_SALDO AND Q1.COD_NIVEL_DETALLE = Q2.COD_NIVEL_DETALLE AND Q1.BOL_BLOQUEADO = Q2.BOL_BLOQUEADO AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE AND Q1.OID_DIVISA = Q2.OID_DIVISA AND Q1.COD_TIPO_MEDIO_PAGO = Q2.COD_TIPO_MEDIO_PAGO AND NVL(Q1.OID_MEDIO_PAGO, '-') = NVL(Q2.OID_MEDIO_PAGO, '-') AND NVL(Q1.OID_UNIDAD_MEDIDA, '-') = NVL(Q2.OID_UNIDAD_MEDIDA, '-') AND ROWNUM <= 1)
    WHEN MATCHED THEN
      update
         set q1.num_importe              = nvl(q1.num_importe, 0) +
                                           nvl(q2.num_importe, 0),
             q1.nel_cantidad             = nvl(q1.nel_cantidad, 0) +
                                           nvl(q2.nel_cantidad, 0),
             q1.GMT_MODIFICACION         = FN_GMT_ZERO_###VERSION###,
             q1.DES_USUARIO_MODIFICACION = par$usuario,
             q1.OID_ULTIMA_TRANSACCION   = q2.oid_transaccion_medio_pago
    WHEN NOT MATCHED THEN
      INSERT
        (Q1.OID_SALDO_MEDIO_PAGO,
         Q1.OID_CUENTA_SALDO,
         Q1.OID_MEDIO_PAGO,
         Q1.OID_DIVISA,
         Q1.OID_ULTIMA_TRANSACCION,
         Q1.COD_TIPO_MEDIO_PAGO,
         Q1.COD_NIVEL_DETALLE,
         Q1.OID_UNIDAD_MEDIDA,
         Q1.BOL_DISPONIBLE,
         Q1.NUM_IMPORTE,
         Q1.NEL_CANTIDAD,
         Q1.GMT_CREACION,
         Q1.DES_USUARIO_CREACION,
         Q1.GMT_MODIFICACION,
         Q1.DES_USUARIO_MODIFICACION,
         Q1.BOL_BLOQUEADO)

      VALUES
        (SYS_GUID(),
         Q2.OID_CUENTA_SALDO,
         Q2.OID_MEDIO_PAGO,
         Q2.OID_DIVISA,
         par$oid_transaccion_medio_pago,
         Q2.COD_TIPO_MEDIO_PAGO,
         Q2.COD_NIVEL_DETALLE,
         Q2.OID_UNIDAD_MEDIDA,
         Q2.BOL_DISPONIBLE,
         Q2.NUM_IMPORTE,
         Q2.NEL_CANTIDAD,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         Q2.BOL_BLOQUEADO);

    par$inserts := 0 + sql%ROWCOUNT;
    /* DEPOIS DE ATUALIZAR O SALDO, DEVE VERIFICAR SE GEROU UM SALDO NEGATIVO E
       SE O FORMULARIO PERMITE SALDO NEGATIVO
    */

    /* SE O FORMULARIO NAO PERMITE SALDO NEGATIVO*/
    IF NOT par$permite_saldo_negativo then
      SELECT NUM_IMPORTE
        into var$gerou_saldo_negativo
        FROM SAPR_TSALDO_MEDIO_PAGO
       WHERE OID_ULTIMA_TRANSACCION = par$oid_transaccion_medio_pago;

      par$selects := 0 + sql%ROWCOUNT;

      /* SE GEROU UM SALDO NEGATIVO ENTAO DEVE ABORTAR O PROCESSO*/
      if var$gerou_saldo_negativo < 0 then
        /* GERA A EXCESAO QUE SERA TRATADA NA APLICACAO */
        raise_application_error(-20001,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    '028_msg_el_movimiento_produce_saldo_negativo',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDoc,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1));
      end if;
    end if;

  end ssaldo_medio_pago_actualizar;

  /* Executa a acao contabil do documento */
  PROCEDURE saccion_contable_ejecutar(par$oid_documento             IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_doc_formulario_accion IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado_documento      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$usuario                   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts                   OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$selects                   OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$tipo_movimiento gepr_pcomon_###VERSION###.tipo$est_;
    var$tipo_sitio      gepr_pcomon_###VERSION###.tipo$est_;
    var$bol_disponible  NUMBER(1);
    var$bol_bloqueado   gepr_pcomon_###VERSION###.tipo$nbol_;
    var$inserts_int     gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int     gepr_pcomon_###VERSION###.tipo$nel_;

    /*CURSOR DE ACCIONES CONTABLES DEL DOCUMENTO*/
    CURSOR cur$accion(par$oid_doc_formulario_accion IN gepr_pcomon_###VERSION###.tipo$oid_,
                      par$cod_estado_documento      IN gepr_pcomon_###VERSION###.tipo$cod_) IS
      SELECT EAC.COD_ESTADO,
             EAC.COD_ACCION_ORIGEN_DISPONIBLE,
             EAC.COD_ACCION_ORIGEN_NODISP,
             EAC.COD_ACCION_DESTINO_DISPONIBLE,
             EAC.COD_ACCION_DESTINO_NODISP,
             EAC.COD_ACCION_ORIGEN_DISPBLOQ,
             EAC.COD_ACCION_DESTINO_DISPBLOQ,
             D.OID_CUENTA_ORIGEN,
             D.OID_CUENTA_DESTINO,
             D.OID_CUENTA_SALDO_ORIGEN,
             D.OID_CUENTA_SALDO_DESTINO,
             par$cod_estado_documento          AS COD_ESTADO_DOCUMENTO,
             F.OID_FORMULARIO                  AS OID_FORMULARIO
        FROM SAPR_TFORMULARIO F
       INNER JOIN SAPR_TACCION_CONTABLE AC
          ON AC.OID_ACCION_CONTABLE = F.OID_ACCION_CONTABLE
       INNER JOIN SAPR_TESTADOXACCION_CONTABLE EAC
          ON EAC.OID_ACCION_CONTABLE = AC.OID_ACCION_CONTABLE
       INNER JOIN SAPR_TDOCUMENTO D
          ON D.OID_FORMULARIO = F.OID_FORMULARIO
       WHERE D.OID_DOCUMENTO = par$oid_doc_formulario_accion
         AND AC.BOL_ACTIVO = 1
         AND EAC.COD_ESTADO = par$cod_estado_documento
         AND D.COD_ESTADO = DECODE(par$oid_doc_formulario_accion,
                                   par$oid_documento,
                                   par$cod_estado_documento,
                                   'SU');

    /*PARA CADA DENOMINACION DO EFECTIVO*/
    CURSOR cur$efectivo(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT ED.OID_DIVISA,
             ED.OID_DENOMINACION,
             ED.COD_NIVEL_DETALLE,
             ED.OID_UNIDAD_MEDIDA,
             ED.OID_CALIDAD,
             CASE
               WHEN ED.COD_NIVEL_DETALLE = 'T' THEN
                'A'
               ELSE
                NULL
             END COD_TIPO_EFECTIVO_TOTAL,
             SUM(ED.NUM_IMPORTE) AS NUM_IMPORTE,
             SUM(ED.NEL_CANTIDAD) AS NEL_CANTIDAD
        FROM SAPR_TEFECTIVOXDOCUMENTO ED
       WHERE OID_DOCUMENTO = par$oid_documento
       GROUP BY ED.OID_DOCUMENTO,
                ED.OID_DIVISA,
                ED.OID_DENOMINACION,
                ED.COD_NIVEL_DETALLE,
                ED.OID_UNIDAD_MEDIDA,
                ED.OID_CALIDAD
       ORDER BY ED.OID_DIVISA,
                ED.OID_DENOMINACION;

    /*PARA CADA MEDIO DO DOCUMENTO*/
    CURSOR cur$medio_pago(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT MP.OID_DIVISA,
             MP.OID_MEDIO_PAGO,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_NIVEL_DETALLE,
             SUM(MP.NUM_IMPORTE) AS NUM_IMPORTE,
             SUM(MP.NEL_CANTIDAD) AS NEL_CANTIDAD
        FROM SAPR_TMEDIO_PAGOXDOCUMENTO MP
       WHERE OID_DOCUMENTO = par$oid_documento
       GROUP BY MP.OID_DIVISA,
                MP.OID_MEDIO_PAGO,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.COD_NIVEL_DETALLE
       ORDER BY DECODE(MP.COD_NIVEL_DETALLE, 'D', 1, 'T', 2, 'G', 3);

    PROCEDURE strans_efectivo_inserir(par$oid_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$usuario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$tipo_movimiento IN gepr_pcomon_###VERSION###.tipo$est_,
                                      par$tipo_sitio      IN gepr_pcomon_###VERSION###.tipo$est_,
                                      par$bol_disponible  IN NUMBER,
                                      par$bol_bloqueado   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                      par$accion          IN cur$accion%rowtype,
                                      par$efectivo        IN cur$efectivo%rowtype,
                                      par$cod_cultura     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts         OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$selects         OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
      var$oid_cuenta               gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_cuenta_saldo         gepr_pcomon_###VERSION###.tipo$oid_;
      var$tipo_movimiento          gepr_pcomon_###VERSION###.tipo$est_;
      var$num_importe              NUMBER(20, 2);
      var$nel_cantidad             NUMBER(16);
      var$oid_transaccion_efectivo gepr_pcomon_###VERSION###.tipo$oid_;
      var$inserts_int              gepr_pcomon_###VERSION###.tipo$nel_;
      var$selects_int              gepr_pcomon_###VERSION###.tipo$nel_;
      var$fyh_plncertif            gepr_pcomon_###VERSION###.tipo$fyh_;
    BEGIN

      /* gera o OID que sera inserido */
      var$oid_transaccion_efectivo := sys_guid;

      IF par$tipo_movimiento = '+' THEN
        /*INGRESO*/
        var$tipo_movimiento := 'I';
        var$num_importe     := par$efectivo.num_importe;
        var$nel_cantidad    := par$efectivo.nel_cantidad;
      ELSE
        /*EGRESO*/
        var$tipo_movimiento := 'E';
        var$num_importe     := par$efectivo.num_importe * (-1);
        var$nel_cantidad    := par$efectivo.nel_cantidad * (-1);
      END IF;

      IF par$tipo_sitio = 'O' THEN
        var$oid_cuenta       := par$accion.oid_cuenta_origen;
        var$oid_cuenta_saldo := par$accion.oid_cuenta_saldo_origen;
      ELSE
        var$oid_cuenta       := par$accion.oid_cuenta_destino;
        var$oid_cuenta_saldo := par$accion.oid_cuenta_saldo_destino;
      END IF;

      BEGIN
        SELECT D.FYH_PLAN_CERTIFICACION
          INTO var$fyh_plncertif
          FROM SAPR_TDOCUMENTO D
         WHERE D.OID_DOCUMENTO = par$oid_documento;
      EXCEPTION
        WHEN no_data_found THEN
          var$fyh_plncertif := sysdate();
      END;

      INSERT INTO SAPR_TTRANSACCION_EFECTIVO
        (OID_TRANSACCION_EFECTIVO,
         OID_DOCUMENTO,
         OID_CUENTA,
         OID_CUENTA_SALDO,
         OID_DIVISA,
         OID_DENOMINACION,
         OID_CERTIFICADO,
         COD_NIVEL_DETALLE,
         COD_TIPO_EFECTIVO_TOTAL,
         BOL_DISPONIBLE,
         NUM_IMPORTE,
         NEL_CANTIDAD,
         COD_ESTADO_DOCUMENTO,
         COD_TIPO_MOVIMIENTO,
         OBS_TRANSACCION,
         COD_MIGRACION,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION,
         COD_TIPO_SITIO,
         OID_UNIDAD_MEDIDA,
         OID_CALIDAD,
         BOL_CONTRA_MOVIMIENTO,
         FYH_PLAN_CERTIFICACION,
         BOL_CERTIFICADO,
         BOL_BLOQUEADO)
      VALUES
        (var$oid_transaccion_efectivo,
         par$oid_documento,
         var$oid_cuenta,
         var$oid_cuenta_saldo,
         par$efectivo.oid_divisa,
         par$efectivo.oid_denominacion,
         null,
         par$efectivo.cod_nivel_detalle,
         par$efectivo.cod_tipo_efectivo_total,
         par$bol_disponible,
         var$num_importe,
         var$nel_cantidad,
         par$accion.cod_estado_documento,
         var$tipo_movimiento,
         null,
         null,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         par$tipo_sitio,
         par$efectivo.Oid_Unidad_Medida,
         par$efectivo.oid_calidad,
         0,
         var$fyh_plncertif,
         0,
         par$bol_bloqueado);

      par$inserts := par$inserts + sql%rowcount;

      /* DEPOIS DE INSERIR A TRANSACAO DEVE ATUALIZAR O SALDO */
      ssaldo_efectivo_actualizar(par$oid_documento            => par$oid_documento,
                                 par$oid_transaccion_efectivo => var$oid_transaccion_efectivo,
                                 par$usuario                  => par$usuario,
                                 par$permite_saldo_negativo   => sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$accion.oid_formulario,
                                                                                                                    par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracPerSaldoNeg),
                                 par$cod_cultura              => par$cod_cultura,
                                 par$inserts                  => var$inserts_int,
                                 par$selects                  => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;

    END strans_efectivo_inserir;

    PROCEDURE strans_medio_pago_inserir(par$oid_documento   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$usuario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$tipo_movimiento IN gepr_pcomon_###VERSION###.tipo$est_,
                                        par$tipo_sitio      IN gepr_pcomon_###VERSION###.tipo$est_,
                                        par$bol_disponible  IN NUMBER,
                                        par$bol_bloqueado   IN gepr_pcomon_###VERSION###.tipo$nbol_,
                                        par$accion          IN cur$accion%rowtype,
                                        par$medio_pago      IN cur$medio_pago%rowtype,
                                        par$cod_cultura     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$inserts         OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects         OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
      var$oid_cuenta                 gepr_pcomon_###VERSION###.tipo$oid_;
      var$oid_cuenta_saldo           gepr_pcomon_###VERSION###.tipo$oid_;
      var$tipo_movimiento            gepr_pcomon_###VERSION###.tipo$est_;
      var$num_importe                NUMBER(20, 2);
      var$nel_cantidad               NUMBER(16);
      var$oid_transaccion_medio_pago gepr_pcomon_###VERSION###.tipo$oid_;
      var$inserts_int                gepr_pcomon_###VERSION###.tipo$nel_;
      var$selects_int                gepr_pcomon_###VERSION###.tipo$nel_;
      var$fyh_plncertif              gepr_pcomon_###VERSION###.tipo$fyh_;
    BEGIN

      /* gera o OID que sera inserido */
      var$oid_transaccion_medio_pago := sys_guid;

      IF par$tipo_movimiento = '+' THEN
        /*INGRESO*/
        var$tipo_movimiento := 'I';
        var$num_importe     := par$medio_pago.num_importe;
        var$nel_cantidad    := par$medio_pago.nel_cantidad;
      ELSE
        /*EGRESO*/
        var$tipo_movimiento := 'E';
        var$num_importe     := par$medio_pago.num_importe * (-1);
        var$nel_cantidad    := par$medio_pago.nel_cantidad * (-1);
      END IF;

      IF par$tipo_sitio = 'O' THEN
        var$oid_cuenta       := par$accion.oid_cuenta_origen;
        var$oid_cuenta_saldo := par$accion.oid_cuenta_saldo_origen;
      ELSE
        var$oid_cuenta       := par$accion.oid_cuenta_destino;
        var$oid_cuenta_saldo := par$accion.oid_cuenta_saldo_destino;
      END IF;

      BEGIN
        SELECT D.FYH_PLAN_CERTIFICACION
          INTO var$fyh_plncertif
          FROM SAPR_TDOCUMENTO D
         WHERE D.OID_DOCUMENTO = par$oid_documento;
      EXCEPTION
        WHEN no_data_found THEN
          var$fyh_plncertif := sysdate();
      END;

      INSERT INTO SAPR_TTRANSACCION_MEDIO_PAGO
        (OID_TRANSACCION_MEDIO_PAGO,
         OID_DOCUMENTO,
         OID_CUENTA,
         OID_CUENTA_SALDO,
         OID_MEDIO_PAGO,
         OID_CERTIFICADO,
         COD_TIPO_MEDIO_PAGO,
         COD_NIVEL_DETALLE,
         BOL_DISPONIBLE,
         NUM_IMPORTE,
         NEL_CANTIDAD,
         COD_ESTADO_DOCUMENTO,
         COD_TIPO_MOVIMIENTO,
         OBS_TRANSACCION,
         COD_MIGRACION,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION,
         OID_DIVISA,
         COD_TIPO_SITIO,
         BOL_CONTRA_MOVIMIENTO,
         OID_UNIDAD_MEDIDA,
         FYH_PLAN_CERTIFICACION,
         BOL_CERTIFICADO,
         BOL_BLOQUEADO)
      VALUES
        (var$oid_transaccion_medio_pago,
         par$oid_documento,
         var$oid_cuenta,
         var$oid_cuenta_saldo,
         par$medio_pago.oid_medio_pago,
         null,
         par$medio_pago.cod_tipo_medio_pago,
         par$medio_pago.cod_nivel_detalle,
         par$bol_disponible,
         var$num_importe,
         var$nel_cantidad,
         par$accion.cod_estado_documento,
         var$tipo_movimiento,
         null,
         null,
         fn_gmt_zero_###VERSION###,
         par$usuario,
         fn_gmt_zero_###VERSION###,
         par$usuario,
         par$medio_pago.oid_divisa,
         var$tipo_sitio,
         0,
         null,
         var$fyh_plncertif,
         0,
         par$bol_bloqueado);

      par$inserts := par$inserts + sql%rowcount;

      /* DEPOIS DE INSERIR A TRANSACAO DEVE ATUALIZAR O SALDO */
      ssaldo_medio_pago_actualizar(par$oid_transaccion_medio_pago => var$oid_transaccion_medio_pago,
                                   par$usuario                    => par$usuario,
                                   par$permite_saldo_negativo     => sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$accion.oid_formulario,
                                                                                                                        par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracPerSaldoNeg),
                                   par$cod_cultura                => par$cod_cultura,
                                   par$inserts                    => var$inserts_int,
                                   par$selects                    => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;

    END strans_medio_pago_inserir;

  BEGIN
    par$inserts := 0;
    par$selects := 0;

    FOR rec$accion IN cur$accion(par$oid_doc_formulario_accion,
                                 par$cod_estado_documento) LOOP

      /* TRANSACION EFECTIVO */
      FOR rec$efectivo IN cur$efectivo(par$oid_documento) LOOP

        /* PARA CADA A ACCION CONTABLE */
        /* SE O TIPO MOVIMENTO FOR ZERO(0) ENTAO NAO FAZ NADA */

        /* ORIGEN DISPONIBLE */
        IF rec$accion.cod_accion_origen_disponible <> '0' THEN

          var$tipo_movimiento := rec$accion.cod_accion_origen_disponible;

          /* ORIGEN */
          var$tipo_sitio := 'O';

          /* DISPONIBLE */
          var$bol_disponible := 1;

          /* BLOQUEADO */
          var$bol_bloqueado := 0;

          /* INSERE O EFECTIVO */
          strans_efectivo_inserir(par$oid_documento   => par$oid_documento,
                                  par$usuario         => par$usuario,
                                  par$tipo_movimiento => var$tipo_movimiento,
                                  par$tipo_sitio      => var$tipo_sitio,
                                  par$bol_disponible  => var$bol_disponible,
                                  par$bol_bloqueado   => var$bol_bloqueado,
                                  par$accion          => rec$accion,
                                  par$efectivo        => rec$efectivo,
                                  par$cod_cultura     => par$cod_cultura,
                                  par$inserts         => var$inserts_int,
                                  par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* ORIGEN DISPONIBLE BLOQUEADO */
        IF rec$accion.cod_accion_origen_dispbloq <> '0' THEN

          var$tipo_movimiento := rec$accion.cod_accion_origen_dispbloq;

          /* ORIGEN */
          var$tipo_sitio := 'O';

          /* DISPONIBLE */
          var$bol_disponible := 1;

          /* BLOQUEADO */
          var$bol_bloqueado := 1;

          /* INSERE O EFECTIVO */
          strans_efectivo_inserir(par$oid_documento   => par$oid_documento,
                                  par$usuario         => par$usuario,
                                  par$tipo_movimiento => var$tipo_movimiento,
                                  par$tipo_sitio      => var$tipo_sitio,
                                  par$bol_disponible  => var$bol_disponible,
                                  par$bol_bloqueado   => var$bol_bloqueado,
                                  par$accion          => rec$accion,
                                  par$efectivo        => rec$efectivo,
                                  par$cod_cultura     => par$cod_cultura,
                                  par$inserts         => var$inserts_int,
                                  par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* ORIGEN NO DISPONIBLE */
        IF rec$accion.cod_accion_origen_nodisp <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_origen_nodisp;

          /* ORIGEN */
          var$tipo_sitio := 'O';

          /* NO DISPONIBLE */
          var$bol_disponible := 0;

          /* BLOQUEADO */
          var$bol_bloqueado := 0;

          /* INSERE O EFECTIVO */
          strans_efectivo_inserir(par$oid_documento   => par$oid_documento,
                                  par$usuario         => par$usuario,
                                  par$tipo_movimiento => var$tipo_movimiento,
                                  par$tipo_sitio      => var$tipo_sitio,
                                  par$bol_disponible  => var$bol_disponible,
                                  par$bol_bloqueado   => var$bol_bloqueado,
                                  par$accion          => rec$accion,
                                  par$efectivo        => rec$efectivo,
                                  par$cod_cultura     => par$cod_cultura,
                                  par$inserts         => var$inserts_int,
                                  par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* DESTINO DISPONIBLE */
        IF rec$accion.cod_accion_destino_disponible <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_destino_disponible;

          /* DESTINO */
          var$tipo_sitio := 'D';

          /* DISPONIBLE */
          var$bol_disponible := 1;

          /* BLOQUEADO */
          var$bol_bloqueado := 0;

          /* INSERE O EFECTIVO */
          strans_efectivo_inserir(par$oid_documento   => par$oid_documento,
                                  par$usuario         => par$usuario,
                                  par$tipo_movimiento => var$tipo_movimiento,
                                  par$tipo_sitio      => var$tipo_sitio,
                                  par$bol_disponible  => var$bol_disponible,
                                  par$bol_bloqueado   => var$bol_bloqueado,
                                  par$accion          => rec$accion,
                                  par$efectivo        => rec$efectivo,
                                  par$cod_cultura     => par$cod_cultura,
                                  par$inserts         => var$inserts_int,
                                  par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* DESTINO DISPONIBLE BLOQUEADO */
        IF rec$accion.cod_accion_destino_dispbloq <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_destino_dispbloq;

          /* DESTINO */
          var$tipo_sitio := 'D';

          /* DISPONIBLE */
          var$bol_disponible := 1;

          /* BLOQUEADO */
          var$bol_bloqueado := 1;

          /* INSERE O EFECTIVO */
          strans_efectivo_inserir(par$oid_documento   => par$oid_documento,
                                  par$usuario         => par$usuario,
                                  par$tipo_movimiento => var$tipo_movimiento,
                                  par$tipo_sitio      => var$tipo_sitio,
                                  par$bol_disponible  => var$bol_disponible,
                                  par$bol_bloqueado   => var$bol_bloqueado,
                                  par$accion          => rec$accion,
                                  par$efectivo        => rec$efectivo,
                                  par$cod_cultura     => par$cod_cultura,
                                  par$inserts         => var$inserts_int,
                                  par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* DESTINO NO DISPONIBLE */
        IF rec$accion.cod_accion_destino_nodisp <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_destino_nodisp;

          /* DESTINO */
          var$tipo_sitio := 'D';

          /* NO DISPONIBLE */
          var$bol_disponible := 0;

          /* BLOQUEADO */
          var$bol_bloqueado := 0;

          /* INSERE O EFECTIVO */
          strans_efectivo_inserir(par$oid_documento   => par$oid_documento,
                                  par$usuario         => par$usuario,
                                  par$tipo_movimiento => var$tipo_movimiento,
                                  par$tipo_sitio      => var$tipo_sitio,
                                  par$bol_disponible  => var$bol_disponible,
                                  par$bol_bloqueado   => var$bol_bloqueado,
                                  par$accion          => rec$accion,
                                  par$efectivo        => rec$efectivo,
                                  par$cod_cultura     => par$cod_cultura,
                                  par$inserts         => var$inserts_int,
                                  par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;
      END LOOP rec$efectivo;

      par$selects := par$selects + 1;

      /* TRANSACION MEDIO PAGO */
      FOR rec$medio_pago IN cur$medio_pago(par$oid_documento) LOOP
        /* PARA CADA A ACCION CONTABLE */
        /* SE O TIPO MOVIMENTO FOR ZERO(0) ENTAO NAO FAZ NADA */

        /* ORIGEN DISPONIBLE */
        IF rec$accion.cod_accion_origen_disponible <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_origen_disponible;

          -- ORIGEN */
          var$tipo_sitio := 'O';

          -- DISPONIBLE */
          var$bol_disponible := 1;

          /* BLOQUEADO */
          var$bol_bloqueado := 0;

          -- INSERE O MEDIO PAGO */
          strans_medio_pago_inserir(par$oid_documento   => par$oid_documento,
                                    par$usuario         => par$usuario,
                                    par$tipo_movimiento => var$tipo_movimiento,
                                    par$tipo_sitio      => var$tipo_sitio,
                                    par$bol_disponible  => var$bol_disponible,
                                    par$bol_bloqueado   => var$bol_bloqueado,
                                    par$accion          => rec$accion,
                                    par$medio_pago      => rec$medio_pago,
                                    par$cod_cultura     => par$cod_cultura,
                                    par$inserts         => var$inserts_int,
                                    par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* ORIGEN DISPONIBLE BLOQUEADO */
        IF rec$accion.cod_accion_origen_dispbloq <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_origen_dispbloq;

          -- ORIGEN */
          var$tipo_sitio := 'O';

          -- DISPONIBLE */
          var$bol_disponible := 1;

          /* BLOQUEADO */
          var$bol_bloqueado := 1;

          -- INSERE O MEDIO PAGO */
          strans_medio_pago_inserir(par$oid_documento   => par$oid_documento,
                                    par$usuario         => par$usuario,
                                    par$tipo_movimiento => var$tipo_movimiento,
                                    par$tipo_sitio      => var$tipo_sitio,
                                    par$bol_disponible  => var$bol_disponible,
                                    par$bol_bloqueado   => var$bol_bloqueado,
                                    par$accion          => rec$accion,
                                    par$medio_pago      => rec$medio_pago,
                                    par$cod_cultura     => par$cod_cultura,
                                    par$inserts         => var$inserts_int,
                                    par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* ORIGEN NO DISPONIBLE */
        IF rec$accion.cod_accion_origen_nodisp <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_origen_nodisp;

          /* ORIGEN */
          var$tipo_sitio := 'O';

          /* NO DISPONIBLE */
          var$bol_disponible := 0;

          /* BLOQUEADO */
          var$bol_bloqueado := 0;

          /* INSERE O MEDIO PAGO */
          strans_medio_pago_inserir(par$oid_documento   => par$oid_documento,
                                    par$usuario         => par$usuario,
                                    par$tipo_movimiento => var$tipo_movimiento,
                                    par$tipo_sitio      => var$tipo_sitio,
                                    par$bol_disponible  => var$bol_disponible,
                                    par$bol_bloqueado   => var$bol_bloqueado,
                                    par$accion          => rec$accion,
                                    par$medio_pago      => rec$medio_pago,
                                    par$cod_cultura     => par$cod_cultura,
                                    par$inserts         => var$inserts_int,
                                    par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* DESTINO DISPONIBLE */
        IF rec$accion.cod_accion_destino_disponible <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_destino_disponible;

          /* DESTINO */
          var$tipo_sitio := 'D';

          /* DISPONIBLE */
          var$bol_disponible := 1;

          /* BLOQUEADO */
          var$bol_bloqueado := 0;

          /* INSERE O MEDIO PAGO */
          strans_medio_pago_inserir(par$oid_documento   => par$oid_documento,
                                    par$usuario         => par$usuario,
                                    par$tipo_movimiento => var$tipo_movimiento,
                                    par$tipo_sitio      => var$tipo_sitio,
                                    par$bol_disponible  => var$bol_disponible,
                                    par$bol_bloqueado   => var$bol_bloqueado,
                                    par$accion          => rec$accion,
                                    par$medio_pago      => rec$medio_pago,
                                    par$cod_cultura     => par$cod_cultura,
                                    par$inserts         => var$inserts_int,
                                    par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* DESTINO DISPONIBLE BLOQUEADO */
        IF rec$accion.cod_accion_destino_dispbloq <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_destino_dispbloq;

          /* DESTINO */
          var$tipo_sitio := 'D';

          /* DISPONIBLE */
          var$bol_disponible := 1;

          /* BLOQUEADO */
          var$bol_bloqueado := 1;

          /* INSERE O MEDIO PAGO */
          strans_medio_pago_inserir(par$oid_documento   => par$oid_documento,
                                    par$usuario         => par$usuario,
                                    par$tipo_movimiento => var$tipo_movimiento,
                                    par$tipo_sitio      => var$tipo_sitio,
                                    par$bol_disponible  => var$bol_disponible,
                                    par$bol_bloqueado   => var$bol_bloqueado,
                                    par$accion          => rec$accion,
                                    par$medio_pago      => rec$medio_pago,
                                    par$cod_cultura     => par$cod_cultura,
                                    par$inserts         => var$inserts_int,
                                    par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;

        /* DESTINO NO DISPONIBLE */
        IF rec$accion.cod_accion_destino_nodisp <> '0' THEN
          var$tipo_movimiento := rec$accion.cod_accion_destino_nodisp;

          /* DESTINO */
          var$tipo_sitio := 'D';

          /* NO DISPONIBLE */
          var$bol_disponible := 0;

          /* BLOQUEADO */
          var$bol_bloqueado := 0;

          /* INSERE O MEDIO PAGO */
          strans_medio_pago_inserir(par$oid_documento   => par$oid_documento,
                                    par$usuario         => par$usuario,
                                    par$tipo_movimiento => var$tipo_movimiento,
                                    par$tipo_sitio      => var$tipo_sitio,
                                    par$bol_disponible  => var$bol_disponible,
                                    par$bol_bloqueado   => var$bol_bloqueado,
                                    par$accion          => rec$accion,
                                    par$medio_pago      => rec$medio_pago,
                                    par$cod_cultura     => par$cod_cultura,
                                    par$inserts         => var$inserts_int,
                                    par$selects         => var$selects_int);
          par$inserts := par$inserts + var$inserts_int;
          par$selects := par$selects + var$selects_int;
        END IF;
      END LOOP rec$medio_pago;
      par$selects := par$selects + 1;

    END LOOP rec$accion;
    par$selects := par$selects + 1;

  END saccion_contable_ejecutar;

  /* actualizar estado de los elementos del documento */
  PROCEDURE selemento_actualizar_estado(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$bol_sustituicion     IN BOOLEAN,
                                        par$oid_documento_padre  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$usuario              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$gmt_zero             VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$oid_formulario_padre gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_remesa           gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_contenedor       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_mov       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_sal       gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector           gepr_pcomon_###VERSION###.tipo$oid_;

    /* VALORES PATRON */
    var$cod_estado_doc_elemento gepr_pcomon_###VERSION###.tipo$est_ := gepr_pcomon_###VERSION###.const$CodEstadoDocEleHistorico; /* H */
    var$cod_estado_remesa       gepr_pcomon_###VERSION###.tipo$est_ := gepr_pcomon_###VERSION###.const$codEstadoRemesaPendiente; /* PE */
    var$cod_estado_bulto        gepr_pcomon_###VERSION###.tipo$est_ := gepr_pcomon_###VERSION###.const$codEstadoBultoCerrado; /* CE */
    var$cod_estado_parcial      gepr_pcomon_###VERSION###.tipo$est_ := gepr_pcomon_###VERSION###.const$codEstadoParcialPendient; /* PE */
    var$cod_estado_contenedor   gepr_pcomon_###VERSION###.tipo$est_ := gepr_pcomon_###VERSION###.const$CodEstadoConArmado; /* AR */

    /* CURSOR DE BULTOS DO DOCUMENTO */
    CURSOR cur$bultos(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT DISTINCT DE.OID_BULTO, DE.OID_REMESA
        FROM SAPR_TDOCUMENTOXELEMENTO DE
       WHERE DE.OID_DOCUMENTO = par$oid_documento
         AND DE.OID_BULTO IS NOT NULL;

  BEGIN

    par$updates := 0;
    par$selects := 0;
    /* === SE ESTIVER ANULANDO O DOCUMENTO ===*/
    IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocAnulado THEN

      par$selects := par$selects + 3;

      /*SE FOR UM FORMULARIO DE ALTAS ENTAO ANULA OS ELEMENTOS, SENAO UTILIZA OS VALORES PADROES*/
      IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                            par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAlta) THEN
        var$cod_estado_remesa     := gepr_pcomon_###VERSION###.const$codEstadoRemesaAnulado; /* AN */
        var$cod_estado_bulto      := gepr_pcomon_###VERSION###.const$codEstadoBultoAnulado; /* AN */
        var$cod_estado_parcial    := gepr_pcomon_###VERSION###.const$codEstadoParcialAnulado; /* AN */
        var$cod_estado_contenedor := gepr_pcomon_###VERSION###.const$CodEstadoContDesarmado; /*DR*/
      END IF;

      /*=== ESTADO ELEMENTOS ===*/
    ELSIF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracSalidaRecor) THEN
      par$selects := par$selects + 1;

      /* SALIDA RECORRIDO */
      var$cod_estado_remesa     := gepr_pcomon_###VERSION###.const$codEstadoRemesaProcesado; /* PR */
      var$cod_estado_bulto      := gepr_pcomon_###VERSION###.const$codEstadoBultoSalida; /* SA */
      var$cod_estado_parcial    := gepr_pcomon_###VERSION###.const$codEstadoParcialSalida; /* SA */
      var$cod_estado_contenedor := gepr_pcomon_###VERSION###.const$CodEstadoContDesarmado; /*DR*/

      /* Actualizar fecha plan certificacion de F22 */
      sactualizar_fecha_cert_f22(par$oid_documento => par$oid_documento,
                                 par$usuario       => par$usuario);
      par$selects := par$selects + 3;
      par$updates := par$updates + 3;

    ELSIF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracBaja) THEN
      par$selects := par$selects + 2;

      /* BAJA */
      var$cod_estado_remesa     := gepr_pcomon_###VERSION###.const$codEstadoRemesaAnulado; /* AN */
      var$cod_estado_bulto      := gepr_pcomon_###VERSION###.const$codEstadoBultoAnulado; /* AN */
      var$cod_estado_parcial    := gepr_pcomon_###VERSION###.const$codEstadoParcialAnulado; /* AN */
      var$cod_estado_contenedor := gepr_pcomon_###VERSION###.const$CodEstadoContDesarmado; /*DR*/

    ELSIF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                             par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracActa) THEN
      par$selects := par$selects + 3;

      /* ACTA */
      var$cod_estado_remesa  := gepr_pcomon_###VERSION###.const$codEstadoRemesaProcesado; /* PR */
      var$cod_estado_bulto   := gepr_pcomon_###VERSION###.const$codEstadoBultoAberto; /* AB */
      var$cod_estado_parcial := gepr_pcomon_###VERSION###.const$codEstadoParcialProcesad; /* PR */

    ELSIF par$bol_sustituicion THEN

      /* SI ES SUSTITUICION, DEBE VERIFICAR SI EL PADRE ES UNA ACTA */
      SELECT D.OID_FORMULARIO
        INTO var$oid_formulario_padre
        FROM SAPR_TDOCUMENTO D
       where (SELECT COUNT(*)
                FROM SAPR_TCARACTFORMXFORMULARIO CF
               INNER JOIN SAPR_TCARACT_FORMULARIO C
                  ON C.OID_CARACT_FORMULARIO = CF.OID_CARACT_FORMULARIO
               WHERE C.cod_caract_formulario = 'ACCION_SUSTITUCION'
                 AND CF.OID_FORMULARIO = D.OID_FORMULARIO) = 0
       START WITH D.OID_DOCUMENTO = par$oid_documento_padre
      CONNECT BY NOCYCLE PRIOR D.OID_DOCUMENTO = D.OID_DOCUMENTO_SUSTITUTO;
      par$selects := par$selects + 4;

      IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => var$oid_formulario_padre,
                                                            par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracActa) THEN
        /* ACTA */
        var$cod_estado_remesa  := gepr_pcomon_###VERSION###.const$codEstadoRemesaProcesado; /* PR */
        var$cod_estado_bulto   := gepr_pcomon_###VERSION###.const$codEstadoBultoAberto; /* AB */
        var$cod_estado_parcial := gepr_pcomon_###VERSION###.const$codEstadoParcialProcesad; /* PR */
      END IF;
      par$selects := par$selects + 1;

    ELSE
      par$selects := par$selects + 3;
    END IF;

    /*=== ESTADO DOCUMENTO ELEMENTO ===*/
    IF par$cod_estado_documento = gepr_pcomon_###VERSION###.const$CodEstadoDocAceptado THEN
      var$cod_estado_doc_elemento := gepr_pcomon_###VERSION###.const$CodEstadoDocEleConcluido; /* C */
    END IF;

    IF par$cod_estado_documento <> gepr_pcomon_###VERSION###.const$CodEstadoDocRechazado THEN

      SELECT d.oid_cuenta_destino,
             d.oid_cuenta_saldo_destino,
             d.oid_sector_destino
        INTO var$oid_cuenta_mov, var$oid_cuenta_sal, var$oid_sector
        FROM sapr_tdocumento d
       WHERE d.oid_documento = par$oid_documento;

    ELSE

      SELECT d.Oid_Cuenta_Origen,
             d.Oid_Cuenta_Saldo_Origen,
             d.oid_sector_origen
        INTO var$oid_cuenta_mov, var$oid_cuenta_sal, var$oid_sector
        FROM sapr_tdocumento d
       WHERE d.oid_documento = par$oid_documento;

    END IF;

    begin

      select distinct de.oid_contenedor
        into var$oid_contenedor
        from sapr_tdocumentoxelemento de
       where de.oid_documento = par$oid_documento;

    exception
      when no_data_found then
        var$oid_contenedor := null;
    end;

    /*=== UPDATES ===*/
    FOR rec$bulto IN cur$bultos(par$oid_documento) LOOP

      var$oid_remesa := rec$bulto.oid_remesa;

      /* ESTADO DOCUMENTO/ELEMENTO BULTO */
      UPDATE sapr_tdocumentoxelemento
         SET gmt_modificacion         = var$gmt_zero,
             des_usuario_modificacion = par$usuario,
             cod_estado_docxelemento  = var$cod_estado_doc_elemento
       WHERE oid_documento = par$oid_documento
         AND oid_bulto = rec$bulto.oid_bulto;
      par$updates := par$updates + sql%rowcount;

      /*SE DOCUMENTO ESTA SENDO ANULADO OU RECHAZADO*/
      IF par$cod_estado_documento IN
         (gepr_pcomon_###VERSION###.const$CodEstadoDocAnulado,
          gepr_pcomon_###VERSION###.const$CodEstadoDocRechazado) THEN
        /* ESTADO BULTO */
        UPDATE sapr_tbulto
           SET cod_estado               = var$cod_estado_bulto,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$usuario
         WHERE oid_bulto = rec$bulto.oid_bulto
           AND cod_estado <> var$cod_estado_bulto;
      else
        /* ESTADO BULTO */
        UPDATE sapr_tbulto
           SET cod_estado               = var$cod_estado_bulto,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$usuario,
               oid_cuenta               = var$oid_cuenta_mov,
               oid_cuenta_saldo         = var$oid_cuenta_sal,
               oid_documento            = par$oid_documento
         WHERE oid_bulto = rec$bulto.oid_bulto
           AND cod_estado <> var$cod_estado_bulto;

      end if;

      par$updates := par$updates + sql%rowcount;

      /* ESTADO PARCIAL */
      UPDATE sapr_tparcial
         SET cod_estado               = var$cod_estado_parcial,
             gmt_modificacion         = var$gmt_zero,
             des_usuario_modificacion = par$usuario
       WHERE oid_bulto = rec$bulto.oid_bulto
         AND cod_estado <> var$cod_estado_parcial;
      par$updates := par$updates + sql%rowcount;

      /*=== DOCUMENTO PADRE ===*/
      IF par$oid_documento_padre IS NOT NULL AND
         par$cod_estado_documento NOT IN
         (gepr_pcomon_###VERSION###.const$CodEstadoDocAnulado,
          gepr_pcomon_###VERSION###.const$CodEstadoDocRechazado) THEN

        IF NOT par$bol_sustituicion THEN
          /* ESTADO DOCUMENTO/ELEMENTO DEL PADRE */
          UPDATE sapr_tdocumentoxelemento
             SET gmt_modificacion         = var$gmt_zero,
                 des_usuario_modificacion = par$usuario,
                 cod_estado_docxelemento  = gepr_pcomon_###VERSION###.const$CodEstadoDocEleHistorico
           WHERE oid_documento = par$oid_documento_padre
             AND oid_bulto = rec$bulto.oid_bulto;
          par$updates := par$updates + sql%rowcount;
        ELSE
          /* ESTADO DOCUMENTO/ELEMENTO DEL PADRE */
          UPDATE sapr_tdocumentoxelemento
             SET gmt_modificacion         = var$gmt_zero,
                 des_usuario_modificacion = par$usuario,
                 cod_estado_docxelemento  = gepr_pcomon_###VERSION###.const$CodEstadoDocEleHistorico
           WHERE oid_documento = par$oid_documento_padre
             AND oid_bulto =
                 (SELECT B.OID_BULTO_PADRE
                    FROM SAPR_TBULTO B
                   WHERE B.OID_BULTO = rec$bulto.oid_bulto);
          par$updates := par$updates + sql%rowcount;
        END IF;

      END IF;

    END LOOP rec$bulto;
    par$selects := par$selects + 1;

    if var$oid_remesa is not null then

      /* ESTADO DOCUMENTO/ELEMENTO REMESA */
      UPDATE sapr_tdocumentoxelemento
         SET gmt_modificacion         = var$gmt_zero,
             des_usuario_modificacion = par$usuario,
             cod_estado_docxelemento  = var$cod_estado_doc_elemento
       WHERE oid_documento = par$oid_documento
         AND oid_remesa = var$oid_remesa
         AND oid_bulto IS NULL;
      par$updates := par$updates + sql%rowcount;

      /* Se tentar alterar o estado da remesa para PR porem nem todos os bultos estao fechados entao volta para o estado anterior */
      IF var$cod_estado_remesa = gepr_pcomon_###VERSION###.const$codEstadoRemesaProcesado AND
         NOT fverificar_estado_bultos(var$oid_remesa) THEN
        var$cod_estado_remesa := gepr_pcomon_###VERSION###.const$codEstadoRemesaPendiente; /* PE */
      END IF;

      /*SE DOCUMENTO ESTA SENDO ANULADO OU RECHAZADO*/
      IF par$cod_estado_documento IN
         (gepr_pcomon_###VERSION###.const$CodEstadoDocAnulado,
          gepr_pcomon_###VERSION###.const$CodEstadoDocRechazado) THEN
        /* ESTADO REMESA */
        UPDATE sapr_tremesa
           SET cod_estado               = var$cod_estado_remesa,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$usuario
         WHERE oid_remesa = var$oid_remesa
           AND cod_estado <> var$cod_estado_remesa;
      else
        /* ESTADO REMESA */
        UPDATE sapr_tremesa
           SET cod_estado               = var$cod_estado_remesa,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$usuario,
               oid_cuenta               = var$oid_cuenta_mov,
               oid_cuenta_saldo         = var$oid_cuenta_sal,
               oid_documento            = par$oid_documento
         WHERE oid_remesa = var$oid_remesa
           AND cod_estado <> var$cod_estado_remesa;
      end if;

      par$updates := par$updates + sql%rowcount;

    end if;

    if var$oid_contenedor is not null then

      /* ESTADO DOCUMENTO/ELEMENTO CONTENEDOR */
      UPDATE sapr_tdocumentoxelemento
         SET gmt_modificacion         = var$gmt_zero,
             des_usuario_modificacion = par$usuario,
             cod_estado_docxelemento  = var$cod_estado_doc_elemento
       WHERE oid_documento = par$oid_documento
         AND oid_contenedor = var$oid_contenedor;

      par$updates := par$updates + sql%rowcount;

      /* ESTADO CONTENEDOR */
      UPDATE sapr_tcontenedor c
         SET c.cod_estado               = var$cod_estado_contenedor,
             c.gmt_modificacion         = var$gmt_zero,
             c.des_usuario_modificacion = par$usuario,
             c.oid_cuenta_movimiento    = var$oid_cuenta_mov,
             c.oid_cuenta_saldo         = var$oid_cuenta_sal,
             c.oid_documento            = par$oid_documento,
             c.oid_sector               = var$oid_sector
       WHERE c.oid_contenedor = var$oid_contenedor
         AND c.cod_estado <> var$cod_estado_contenedor;

      par$updates := par$updates + sql%rowcount;

      update sapr_tefectivoxcontenedor ec
         set ec.oid_cuenta_movimiento = var$oid_cuenta_mov,
             ec.oid_cuenta_saldo      = var$oid_cuenta_sal
       where ec.oid_contenedor = var$oid_contenedor;

      par$updates := par$updates + sql%rowcount;

    end if;

    /*=== DOCUMENTO PADRE ===*/
    IF par$oid_documento_padre IS NOT NULL AND
       par$cod_estado_documento <> gepr_pcomon_###VERSION###.const$CodEstadoDocAnulado THEN

      IF NOT par$bol_sustituicion THEN
        /* ESTADO DOCUMENTO/ELEMENTO DEL PADRE */
        UPDATE sapr_tdocumentoxelemento
           SET gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$usuario,
               cod_estado_docxelemento  = gepr_pcomon_###VERSION###.const$CodEstadoDocEleHistorico
         WHERE oid_documento = par$oid_documento_padre
           AND oid_remesa = var$oid_remesa
           AND oid_bulto IS NULL;
        par$updates := par$updates + sql%rowcount;
      ELSE
        /* ESTADO DOCUMENTO/ELEMENTO DEL PADRE */
        UPDATE sapr_tdocumentoxelemento
           SET gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$usuario,
               cod_estado_docxelemento  = gepr_pcomon_###VERSION###.const$CodEstadoDocEleHistorico
         WHERE oid_documento = par$oid_documento_padre
           AND oid_remesa =
               (SELECT R.OID_REMESA_PADRE
                  FROM SAPR_TREMESA R
                 WHERE R.OID_REMESA = var$oid_remesa)
           AND oid_bulto IS NULL;
        par$updates := par$updates + sql%rowcount;
      END IF;
    END IF;

    /*=== SUSTITUTO ===*/
    IF par$bol_sustituicion THEN

      FOR rec$bulto IN cur$bultos(par$oid_documento_padre) LOOP

        var$oid_remesa := rec$bulto.oid_remesa;

        /* ESTADO BULTO */
        UPDATE sapr_tbulto
           SET cod_estado               = gepr_pcomon_###VERSION###.const$codEstadoBultoSustituido,
               gmt_modificacion         = var$gmt_zero,
               oid_bulto_sustituto     =
               (select b.oid_bulto
                  from sapr_tbulto b
                 where b.oid_bulto_padre = rec$bulto.oid_bulto),
               des_usuario_modificacion = par$usuario
         WHERE oid_bulto = rec$bulto.oid_bulto
           AND cod_estado <> gepr_pcomon_###VERSION###.const$codEstadoBultoSustituido;
        par$updates := par$updates + sql%rowcount;

        /* ESTADO PARCIAL */
        UPDATE sapr_tparcial
           SET cod_estado               = gepr_pcomon_###VERSION###.const$codEstadoParcialSustitui,
               gmt_modificacion         = var$gmt_zero,
               des_usuario_modificacion = par$usuario
         WHERE oid_bulto = rec$bulto.oid_bulto
           AND cod_estado <> gepr_pcomon_###VERSION###.const$codEstadoParcialSustitui;
        par$updates := par$updates + sql%rowcount;

      END LOOP rec$bulto;
      par$selects := par$selects + 1;

      /* ESTADO REMESA */
      UPDATE sapr_tremesa
         SET cod_estado               = gepr_pcomon_###VERSION###.const$codEstadoRemesaSustituid,
             gmt_modificacion         = var$gmt_zero,
             oid_remesa_sustituta    =
             (select r.oid_remesa
                from sapr_tremesa r
               where r.oid_remesa_padre = var$oid_remesa),
             des_usuario_modificacion = par$usuario
       WHERE oid_remesa = var$oid_remesa
         AND cod_estado <> gepr_pcomon_###VERSION###.const$codEstadoRemesaSustituid;
      par$updates := par$updates + sql%rowcount;

    END IF;

  END selemento_actualizar_estado;

  /* sactualizar_fecha_cert_f22 */
  PROCEDURE sactualizar_fecha_cert_f22(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$usuario       IN gepr_pcomon_###VERSION###.tipo$cod_) IS
    var$oid_documento_f22 gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_remesa        gepr_pcomon_###VERSION###.tipo$oid_;
    var$gmt_zero          VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$fecha_plan_cert   gepr_pcomon_###VERSION###.tipo$fyh_;
  BEGIN

    /* Recuperar identificador de la remesa */
    SELECT dce.oid_remesa
      INTO var$oid_remesa
      FROM sapr_tdocumentoxelemento dce
     WHERE dce.oid_documento = par$oid_documento
     GROUP BY dce.oid_remesa;

    /* Recuperar identificador del documento f22 */
    BEGIN
      SELECT doc.oid_documento
        INTO var$oid_documento_f22
        FROM sapr_tdocumento doc
       INNER JOIN sapr_tdocumentoxelemento dce
          ON dce.oid_documento = doc.oid_documento
       INNER JOIN (SELECT cafo.oid_formulario
                     FROM sapr_tcaractformxformulario cafo
                    INNER JOIN sapr_tcaract_formulario cara
                       ON cafo.oid_caract_formulario =
                          cara.oid_caract_formulario
                    WHERE cara.cod_caract_formulario IN
                          (gepr_pcomon_###VERSION###.const$CodCaracGestionRemes,
                           gepr_pcomon_###VERSION###.const$CodCaracGestionBulto,
                           gepr_pcomon_###VERSION###.const$CodCaracAlta,
                           gepr_pcomon_###VERSION###.const$CodCaracIntegrSalid)
                    GROUP BY cafo.oid_formulario
                   HAVING COUNT(cafo.oid_caract_formulario) = 3) gen_f22
          ON gen_f22.oid_formulario = doc.oid_formulario
       WHERE doc.cod_estado = 'AC'
         AND dce.oid_remesa = var$oid_remesa
         AND ROWNUM = 1;
    EXCEPTION
      WHEN no_data_found THEN
        var$oid_documento_f22 := NULL;
    END;

    IF var$oid_documento_f22 IS NOT NULL THEN

      /* Recuperar fecha */
      SELECT fyh_plan_certificacion
        INTO var$fecha_plan_cert
        FROM sapr_tdocumento doc
       WHERE doc.oid_documento = par$oid_documento;

      /* actualizar tablas */
      UPDATE sapr_tdocumento
         SET fyh_plan_certificacion   = var$fecha_plan_cert,
             des_usuario_modificacion = par$usuario,
             gmt_modificacion         = var$gmt_zero
       WHERE oid_documento = var$oid_documento_f22;

      UPDATE sapr_ttransaccion_efectivo
         SET fyh_plan_certificacion   = var$fecha_plan_cert,
             des_usuario_modificacion = par$usuario,
             gmt_modificacion         = var$gmt_zero
       WHERE oid_documento = var$oid_documento_f22;

      UPDATE sapr_ttransaccion_medio_pago
         SET fyh_plan_certificacion   = var$fecha_plan_cert,
             des_usuario_modificacion = par$usuario,
             gmt_modificacion         = var$gmt_zero
       WHERE oid_documento = var$oid_documento_f22;

    END IF;

  END sactualizar_fecha_cert_f22;

  /* saccion_contable_classificac */
  PROCEDURE saccion_contable_classificac(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$usuario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$cod_cultura   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                         par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                         par$selects       OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    var$oid_cuenta           gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_cuenta_saldo     gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_formulario       gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_estado_documento gepr_pcomon_###VERSION###.tipo$cod_;
    var$tipo_movimiento      gepr_pcomon_###VERSION###.tipo$cod_;
    var$tipo_sitio           gepr_pcomon_###VERSION###.tipo$cod_;
    var$bol_disponible       NUMBER(1);
    var$inserts_int          gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects_int          gepr_pcomon_###VERSION###.tipo$nel_;

    /*ANTERIOR PARA CADA DENOMINACION DO EFECTIVO*/
    CURSOR cur$efectivoAnterior(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT ED.OID_DIVISA,
             ED.OID_DENOMINACION,
             ED.COD_NIVEL_DETALLE,
             ED.OID_UNIDAD_MEDIDA,
             ED.OID_CALIDAD,
             CASE
               WHEN ED.COD_NIVEL_DETALLE = 'T' THEN
                'A'
               ELSE
                NULL
             END COD_TIPO_EFECTIVO_TOTAL,
             SUM(ED.NUM_IMPORTE) AS NUM_IMPORTE,
             SUM(ED.NEL_CANTIDAD) AS NEL_CANTIDAD
        FROM SAPR_TEFECTIVO_ANTERIORXDOC ED
       WHERE OID_DOCUMENTO = par$oid_documento
       GROUP BY ED.OID_DOCUMENTO,
                ED.OID_DIVISA,
                ED.OID_DENOMINACION,
                ED.COD_NIVEL_DETALLE,
                ED.OID_UNIDAD_MEDIDA,
                ED.OID_CALIDAD
       ORDER BY OID_DOCUMENTO,
                DECODE(ED.COD_NIVEL_DETALLE, 'D', 1, 'T', 2, 'G', 3);

    /*ANTERIOR PARA CADA MEDIO DO DOCUMENTO*/
    CURSOR cur$medio_pagoAnterior(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT MP.OID_DIVISA,
             MP.OID_MEDIO_PAGO,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_NIVEL_DETALLE,
             SUM(MP.NUM_IMPORTE) AS NUM_IMPORTE,
             SUM(MP.NEL_CANTIDAD) AS NEL_CANTIDAD
        FROM SAPR_TMEDIO_PAGO_ANTERIORXDOC MP
       WHERE OID_DOCUMENTO = par$oid_documento
       GROUP BY MP.OID_DIVISA,
                MP.OID_MEDIO_PAGO,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.COD_NIVEL_DETALLE
       ORDER BY DECODE(MP.COD_NIVEL_DETALLE, 'D', 1, 'T', 2, 'G', 3);

    /*PARA CADA DENOMINACION DO EFECTIVO*/
    CURSOR cur$efectivo(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT ED.OID_DIVISA,
             ED.OID_DENOMINACION,
             ED.COD_NIVEL_DETALLE,
             ED.OID_UNIDAD_MEDIDA,
             ED.OID_CALIDAD,
             CASE
               WHEN ED.COD_NIVEL_DETALLE = 'T' THEN
                'A'
               ELSE
                NULL
             END COD_TIPO_EFECTIVO_TOTAL,
             SUM(ED.NUM_IMPORTE) AS NUM_IMPORTE,
             SUM(ED.NEL_CANTIDAD) AS NEL_CANTIDAD
        FROM SAPR_TEFECTIVOXDOCUMENTO ED
       WHERE OID_DOCUMENTO = par$oid_documento
       GROUP BY ED.OID_DOCUMENTO,
                ED.OID_DIVISA,
                ED.OID_DENOMINACION,
                ED.COD_NIVEL_DETALLE,
                ED.OID_UNIDAD_MEDIDA,
                ED.OID_CALIDAD
       ORDER BY OID_DOCUMENTO,
                DECODE(ED.COD_NIVEL_DETALLE, 'D', 1, 'T', 2, 'G', 3);

    /*PARA CADA MEDIO DO DOCUMENTO*/
    CURSOR cur$medio_pago(par$oid_documento IN gepr_pcomon_###VERSION###.tipo$oid_) IS
      SELECT MP.OID_DIVISA,
             MP.OID_MEDIO_PAGO,
             MP.COD_TIPO_MEDIO_PAGO,
             MP.COD_NIVEL_DETALLE,
             SUM(MP.NUM_IMPORTE) AS NUM_IMPORTE,
             SUM(MP.NEL_CANTIDAD) AS NEL_CANTIDAD
        FROM SAPR_TMEDIO_PAGOXDOCUMENTO MP
       WHERE OID_DOCUMENTO = par$oid_documento
       GROUP BY MP.OID_DIVISA,
                MP.OID_MEDIO_PAGO,
                MP.COD_TIPO_MEDIO_PAGO,
                MP.COD_NIVEL_DETALLE
       ORDER BY DECODE(MP.COD_NIVEL_DETALLE, 'D', 1, 'T', 2, 'G', 3);

    /* strans_efectivo_inserir */
    PROCEDURE strans_efectivo_inserir(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$usuario              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$tipo_movimiento      IN gepr_pcomon_###VERSION###.tipo$est_,
                                      par$tipo_sitio           IN gepr_pcomon_###VERSION###.tipo$est_,
                                      par$bol_disponible       IN NUMBER,
                                      par$oid_cuenta           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_cuenta_saldo     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$efectivo             IN cur$efectivo%rowtype,
                                      par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                      par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

      var$num_importe              NUMBER(20, 2);
      var$nel_cantidad             NUMBER(16);
      var$oid_transaccion_efectivo gepr_pcomon_###VERSION###.tipo$oid_;
      var$inserts_int              gepr_pcomon_###VERSION###.tipo$nel_;
      var$selects_int              gepr_pcomon_###VERSION###.tipo$nel_;
    BEGIN

      /* gera o OID que sera inserido */
      var$oid_transaccion_efectivo := sys_guid;

      IF par$tipo_movimiento = 'I' THEN
        /*INGRESO*/
        var$num_importe  := par$efectivo.num_importe;
        var$nel_cantidad := par$efectivo.nel_cantidad;
      ELSE
        /*EGRESO*/
        var$num_importe  := par$efectivo.num_importe * (-1);
        var$nel_cantidad := par$efectivo.nel_cantidad * (-1);
      END IF;

      INSERT INTO SAPR_TTRANSACCION_EFECTIVO
        (OID_TRANSACCION_EFECTIVO,
         OID_DOCUMENTO,
         OID_CUENTA,
         OID_CUENTA_SALDO,
         OID_DIVISA,
         OID_DENOMINACION,
         OID_CERTIFICADO,
         COD_NIVEL_DETALLE,
         COD_TIPO_EFECTIVO_TOTAL,
         BOL_DISPONIBLE,
         NUM_IMPORTE,
         NEL_CANTIDAD,
         COD_ESTADO_DOCUMENTO,
         COD_TIPO_MOVIMIENTO,
         OBS_TRANSACCION,
         COD_MIGRACION,
         GMT_CREACION,
         DES_USUARIO_CREACION,
         GMT_MODIFICACION,
         DES_USUARIO_MODIFICACION,
         COD_TIPO_SITIO,
         OID_UNIDAD_MEDIDA,
         OID_CALIDAD,
         BOL_CONTRA_MOVIMIENTO)
      VALUES
        (var$oid_transaccion_efectivo,
         par$oid_documento,
         par$oid_cuenta,
         par$oid_cuenta_saldo,
         par$efectivo.oid_divisa,
         par$efectivo.oid_denominacion,
         null,
         par$efectivo.cod_nivel_detalle,
         par$efectivo.cod_tipo_efectivo_total,
         par$bol_disponible,
         var$num_importe,
         var$nel_cantidad,
         par$cod_estado_documento,
         par$tipo_movimiento,
         null,
         null,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         FN_GMT_ZERO_###VERSION###,
         par$usuario,
         par$tipo_sitio,
         par$efectivo.Oid_Unidad_Medida,
         par$efectivo.oid_calidad,
         0);

      par$inserts := par$inserts + sql%rowcount;

      /* DEPOIS DE INSERIR A TRANSACAO DEVE ATUALIZAR O SALDO */
      ssaldo_efec_actualizar_clas(par$oid_documento            => par$oid_documento,
                                  par$oid_transaccion_efectivo => var$oid_transaccion_efectivo,                             
                                  par$cod_nivel_detalle        => par$efectivo.cod_nivel_detalle,
                                  par$usuario                  => par$usuario,
                                  par$permite_saldo_negativo   => sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                                                                                     par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracPerSaldoNeg),
                                  par$cod_cultura              => par$cod_cultura,
                                  par$inserts                  => var$inserts_int,
                                  par$selects                  => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;

    END strans_efectivo_inserir;

    /* strans_medio_pago_inserir */
    PROCEDURE strans_medio_pago_inserir(par$oid_documento        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$usuario              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$tipo_movimiento      IN gepr_pcomon_###VERSION###.tipo$est_,
                                        par$tipo_sitio           IN gepr_pcomon_###VERSION###.tipo$est_,
                                        par$bol_disponible       IN NUMBER,
                                        par$oid_cuenta           IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_cuenta_saldo     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_estado_documento IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$oid_formulario       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$medio_pago           IN cur$medio_pago%rowtype,
                                        par$cod_cultura          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
      var$num_importe                NUMBER(20, 2);
      var$nel_cantidad               NUMBER(16);
      var$oid_transaccion_medio_pago gepr_pcomon_###VERSION###.tipo$oid_;
      var$inserts_int                gepr_pcomon_###VERSION###.tipo$nel_;
      var$selects_int                gepr_pcomon_###VERSION###.tipo$nel_;
    BEGIN

      /* gera o OID que sera inserido */
      var$oid_transaccion_medio_pago := sys_guid;

      IF par$tipo_movimiento = 'I' THEN
        /*INGRESO*/
        var$num_importe  := par$medio_pago.num_importe;
        var$nel_cantidad := par$medio_pago.nel_cantidad;
      ELSE
        /*EGRESO*/
        var$num_importe  := par$medio_pago.num_importe * (-1);
        var$nel_cantidad := par$medio_pago.nel_cantidad * (-1);
      END IF;

      INSERT INTO sapr_ttransaccion_medio_pago
        (oid_transaccion_medio_pago,
         oid_documento,
         oid_cuenta,
         oid_cuenta_saldo,
         oid_divisa,
         oid_medio_pago,
         oid_certificado,
         cod_tipo_medio_pago,
         cod_nivel_detalle,
         bol_disponible,
         num_importe,
         nel_cantidad,
         cod_estado_documento,
         cod_tipo_sitio,
         cod_tipo_movimiento,
         obs_transaccion,
         cod_migracion,
         gmt_creacion,
         des_usuario_creacion,
         gmt_modificacion,
         bol_contra_movimiento,
         des_usuario_modificacion)
      values
        (var$oid_transaccion_medio_pago,
         par$oid_documento,
         par$oid_cuenta,
         par$oid_cuenta_saldo,
         par$medio_pago.oid_divisa,
         par$medio_pago.oid_medio_pago,
         null,
         par$medio_pago.cod_tipo_medio_pago,
         par$medio_pago.cod_nivel_detalle,
         par$bol_disponible,
         var$num_importe,
         var$nel_cantidad,
         par$cod_estado_documento,
         par$tipo_sitio,
         par$tipo_movimiento,
         null,
         null,
         fn_gmt_zero_###VERSION###,
         par$usuario,
         fn_gmt_zero_###VERSION###,
         0,
         par$usuario);

      par$inserts := par$inserts + sql%rowcount;

      /* DEPOIS DE INSERIR A TRANSACAO DEVE ATUALIZAR O SALDO */
      ssaldo_medio_pago_actualizar(par$oid_transaccion_medio_pago => var$oid_transaccion_medio_pago,
                                   par$usuario                    => par$usuario,
                                   par$permite_saldo_negativo     => sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                                                                                        par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracPerSaldoNeg),
                                   par$cod_cultura                => par$cod_cultura,
                                   par$inserts                    => var$inserts_int,
                                   par$selects                    => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;

    END strans_medio_pago_inserir;

  BEGIN

    /* Recuperar fecha */
    SELECT doc.oid_cuenta_destino,
           doc.oid_cuenta_saldo_destino,
           doc.oid_formulario
      INTO var$oid_cuenta, var$oid_cuenta_saldo, var$oid_formulario
      FROM sapr_tdocumento doc
     WHERE doc.oid_documento = par$oid_documento;

    /* TRANSACION EFECTIVO ANTERIORES */
    /* TIPO MOVIMIENTO */
    var$cod_estado_documento := 'AC';
    /* TIPO MOVIMIENTO */
    var$tipo_movimiento := 'E';
    /* DESTINO */
    var$tipo_sitio := 'D';
    /* DISPONIBLE */
    var$bol_disponible := 1;

    /* INSERE EFECTIVO ANTERIOR */
    FOR rec$efectivo IN cur$efectivoAnterior(par$oid_documento) LOOP
      strans_efectivo_inserir(par$oid_documento        => par$oid_documento,
                              par$usuario              => par$usuario,
                              par$tipo_movimiento      => var$tipo_movimiento,
                              par$tipo_sitio           => var$tipo_sitio,
                              par$bol_disponible       => var$bol_disponible,
                              par$oid_cuenta           => var$oid_cuenta,
                              par$oid_cuenta_saldo     => var$oid_cuenta_saldo,
                              par$cod_estado_documento => var$cod_estado_documento,
                              par$oid_formulario       => var$oid_formulario,
                              par$efectivo             => rec$efectivo,
                              par$cod_cultura          => par$cod_cultura,
                              par$inserts              => var$inserts_int,
                              par$selects              => var$selects_int);

      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;
    END LOOP rec$efectivo;
    par$selects := par$selects + 1;

    /* INSERE MEDIO PAGO ANTERIOR */
    FOR rec$mediopago IN cur$medio_pagoAnterior(par$oid_documento) LOOP
      strans_medio_pago_inserir(par$oid_documento        => par$oid_documento,
                                par$usuario              => par$usuario,
                                par$tipo_movimiento      => var$tipo_movimiento,
                                par$tipo_sitio           => var$tipo_sitio,
                                par$bol_disponible       => var$bol_disponible,
                                par$oid_cuenta           => var$oid_cuenta,
                                par$oid_cuenta_saldo     => var$oid_cuenta_saldo,
                                par$cod_estado_documento => var$cod_estado_documento,
                                par$oid_formulario       => var$oid_formulario,
                                par$medio_pago           => rec$mediopago,
                                par$cod_cultura          => par$cod_cultura,
                                par$inserts              => var$inserts_int,
                                par$selects              => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;
    END LOOP rec$mediopago;
    par$selects := par$selects + 1;

    /* TRANSACION EFECTIVO ACTUAL */
    /* TIPO MOVIMIENTO */
    var$cod_estado_documento := 'AC';
    /* TIPO MOVIMIENTO */
    var$tipo_movimiento := 'I';
    /* DESTINO */
    var$tipo_sitio := 'D';
    /* DISPONIBLE */
    var$bol_disponible := 1;
    /* INSERE O EFECTIVO */
    FOR rec$efectivo IN cur$efectivo(par$oid_documento) LOOP
      strans_efectivo_inserir(par$oid_documento        => par$oid_documento,
                              par$usuario              => par$usuario,
                              par$tipo_movimiento      => var$tipo_movimiento,
                              par$tipo_sitio           => var$tipo_sitio,
                              par$bol_disponible       => var$bol_disponible,
                              par$oid_cuenta           => var$oid_cuenta,
                              par$oid_cuenta_saldo     => var$oid_cuenta_saldo,
                              par$cod_estado_documento => var$cod_estado_documento,
                              par$oid_formulario       => var$oid_formulario,
                              par$efectivo             => rec$efectivo,
                              par$cod_cultura          => par$cod_cultura,
                              par$inserts              => var$inserts_int,
                              par$selects              => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;
    END LOOP rec$efectivo;
    par$selects := par$selects + 1;

    /* INSERE MEDIO PAGO */
    FOR rec$mediopago IN cur$medio_pago(par$oid_documento) LOOP
      strans_medio_pago_inserir(par$oid_documento        => par$oid_documento,
                                par$usuario              => par$usuario,
                                par$tipo_movimiento      => var$tipo_movimiento,
                                par$tipo_sitio           => var$tipo_sitio,
                                par$bol_disponible       => var$bol_disponible,
                                par$oid_cuenta           => var$oid_cuenta,
                                par$oid_cuenta_saldo     => var$oid_cuenta_saldo,
                                par$cod_estado_documento => var$cod_estado_documento,
                                par$oid_formulario       => var$oid_formulario,
                                par$medio_pago           => rec$mediopago,
                                par$cod_cultura          => par$cod_cultura,
                                par$inserts              => var$inserts_int,
                                par$selects              => var$selects_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;
    END LOOP rec$mediopago;
    par$selects := par$selects + 1;

  END saccion_contable_classificac;

  /* Executa a acao contabil de efectivo do documento */
  PROCEDURE ssaldo_efec_actualizar_clas(par$oid_documento            IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$oid_transaccion_efectivo IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_nivel_detalle        in varchar2,
                                        par$usuario                  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$permite_saldo_negativo   IN BOOLEAN,
                                        par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$inserts                  OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$selects                  OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$gerou_saldo_negativo NUMBER(20, 2);
  BEGIN

    /* verifica se existe um saldo para a transacao,
       se existir atualiza o saldo
       senao existir entao insere
    */
    IF par$cod_nivel_detalle = 'T' THEN
      /*
      se for total
      entao recupera o total para ser atualizado no novo valor total('A')
      entao atualiza o total T para zero, e o novo saldo estara em TOTAL('A')

      */
      FOR reg$saldo in (WITH Q1 AS
                           (SELECT A.OID_CUENTA_SALDO,
                                  A.OID_DIVISA,
                                  A.COD_NIVEL_DETALLE,
                                  A.BOL_DISPONIBLE,
                                  A.BOL_BLOQUEADO,
                                  A.NUM_IMPORTE AS NUM_IMPORTE_TRANS
                             FROM SAPR_TTRANSACCION_EFECTIVO A
                            WHERE A.COD_NIVEL_DETALLE = 'T'
                              AND A.OID_TRANSACCION_EFECTIVO =
                                  par$oid_transaccion_efectivo)
                          SELECT NVL(SUM(Q2.NUM_IMPORTE), 0) AS NUM_IMPORTE,
                                 NVL(SUM(Q2.NEL_CANTIDAD), 0) AS NEL_CANTIDAD,
                                 Q1.OID_CUENTA_SALDO,
                                 Q1.OID_DIVISA,
                                 Q1.COD_NIVEL_DETALLE,
                                 Q1.BOL_DISPONIBLE,
                                 Q1.BOL_BLOQUEADO,
                                 Q1.NUM_IMPORTE_TRANS
                            FROM SAPR_TSALDO_EFECTIVO Q2
                           INNER JOIN Q1
                              ON Q1.OID_CUENTA_SALDO = Q2.OID_CUENTA_SALDO
                             AND Q1.OID_DIVISA = Q2.OID_DIVISA
                             AND Q1.COD_NIVEL_DETALLE = Q2.COD_NIVEL_DETALLE
                             AND Q1.BOL_BLOQUEADO = Q2.BOL_BLOQUEADO
                             AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE
                           GROUP BY Q1.OID_CUENTA_SALDO,
                                    Q1.OID_DIVISA,
                                    Q1.COD_NIVEL_DETALLE,
                                    Q1.BOL_DISPONIBLE,
                                    Q1.BOL_BLOQUEADO,
                                    Q1.NUM_IMPORTE_TRANS) loop
        UPDATE SAPR_TSALDO_EFECTIVO SE
           SET SE.NUM_IMPORTE            = 0,
               SE.NEL_CANTIDAD           = 0,
               SE.OID_ULTIMA_TRANSACCION = par$oid_transaccion_efectivo
         WHERE SE.OID_CUENTA_SALDO = reg$saldo.OID_CUENTA_SALDO
           AND SE.OID_DIVISA = reg$saldo.OID_DIVISA
           AND SE.COD_NIVEL_DETALLE = reg$saldo.COD_NIVEL_DETALLE
           AND SE.BOL_BLOQUEADO = reg$saldo.BOL_BLOQUEADO
           AND SE.BOL_DISPONIBLE = reg$saldo.BOL_DISPONIBLE
           AND SE.COD_TIPO_EFECTIVO_TOTAL = 'T';

        MERGE INTO SAPR_TSALDO_EFECTIVO Q1
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
                      A.BOL_BLOQUEADO
                 FROM SAPR_TTRANSACCION_EFECTIVO A
                WHERE A.OID_TRANSACCION_EFECTIVO =
                      par$oid_transaccion_efectivo) Q2
        ON (Q1.OID_CUENTA_SALDO = Q2.OID_CUENTA_SALDO AND Q1.OID_DIVISA = Q2.OID_DIVISA AND NVL(Q1.OID_DENOMINACION, '-') = NVL(Q2.OID_DENOMINACION, '-') AND NVL(Q1.COD_NIVEL_DETALLE, '-') = NVL(Q2.COD_NIVEL_DETALLE, '-') AND Q1.BOL_BLOQUEADO = Q2.BOL_BLOQUEADO AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE AND NVL(Q1.OID_CALIDAD, '-') = NVL(Q2.OID_CALIDAD, '-') AND Q1.COD_TIPO_EFECTIVO_TOTAL = 'A' AND NVL(Q1.OID_UNIDAD_MEDIDA, '-') = NVL(Q2.OID_UNIDAD_MEDIDA, '-') AND ROWNUM <= 1)
        WHEN MATCHED THEN
          UPDATE
             SET Q1.NUM_IMPORTE              = reg$saldo.num_importe +
                                               NVL(Q2.NUM_IMPORTE, 0),
                 Q1.NEL_CANTIDAD             = reg$saldo.nel_cantidad +
                                               NVL(Q2.NEL_CANTIDAD, 0),
                 Q1.GMT_MODIFICACION         = FN_GMT_ZERO_###VERSION###,
                 Q1.DES_USUARIO_MODIFICACION = par$usuario,
                 Q1.OID_ULTIMA_TRANSACCION   = Q2.OID_TRANSACCION_EFECTIVO
        WHEN NOT MATCHED THEN
          INSERT
            (Q1.OID_SALDO_EFECTIVO,
             Q1.OID_CUENTA_SALDO,
             Q1.OID_ULTIMA_TRANSACCION,
             Q1.OID_DENOMINACION,
             Q1.OID_DIVISA,
             Q1.COD_NIVEL_DETALLE,
             Q1.COD_TIPO_EFECTIVO_TOTAL,
             Q1.OID_CALIDAD,
             Q1.BOL_DISPONIBLE,
             Q1.OID_UNIDAD_MEDIDA,
             Q1.NUM_IMPORTE,
             Q1.NEL_CANTIDAD,
             Q1.GMT_CREACION,
             Q1.DES_USUARIO_CREACION,
             Q1.GMT_MODIFICACION,
             Q1.DES_USUARIO_MODIFICACION,
             Q1.BOL_BLOQUEADO)
          VALUES
            (SYS_GUID(),
             Q2.OID_CUENTA_SALDO,
             par$oid_transaccion_efectivo,
             Q2.OID_DENOMINACION,
             Q2.OID_DIVISA,
             Q2.COD_NIVEL_DETALLE,
             Q2.COD_TIPO_EFECTIVO_TOTAL,
             Q2.OID_CALIDAD,
             Q2.BOL_DISPONIBLE,
             Q2.OID_UNIDAD_MEDIDA,
             Q2.NUM_IMPORTE,
             Q2.NEL_CANTIDAD,
             FN_GMT_ZERO_###VERSION###,
             par$usuario,
             FN_GMT_ZERO_###VERSION###,
             par$usuario,
             Q2.BOL_BLOQUEADO);
      end loop;
    ELSE
      MERGE INTO SAPR_TSALDO_EFECTIVO Q1
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
                    A.BOL_BLOQUEADO
               FROM SAPR_TTRANSACCION_EFECTIVO A
              WHERE A.OID_TRANSACCION_EFECTIVO =
                    par$oid_transaccion_efectivo) Q2
      ON (Q1.OID_CUENTA_SALDO = Q2.OID_CUENTA_SALDO AND Q1.OID_DIVISA = Q2.OID_DIVISA AND NVL(Q1.OID_DENOMINACION, '-') = NVL(Q2.OID_DENOMINACION, '-') AND NVL(Q1.COD_NIVEL_DETALLE, '-') = NVL(Q2.COD_NIVEL_DETALLE, '-') AND Q1.BOL_BLOQUEADO = Q2.BOL_BLOQUEADO AND Q1.BOL_DISPONIBLE = Q2.BOL_DISPONIBLE AND NVL(Q1.OID_CALIDAD, '-') = NVL(Q2.OID_CALIDAD, '-') AND NVL(Q1.COD_TIPO_EFECTIVO_TOTAL, '-') = NVL(Q2.COD_TIPO_EFECTIVO_TOTAL, '-') AND NVL(Q1.OID_UNIDAD_MEDIDA, '-') = NVL(Q2.OID_UNIDAD_MEDIDA, '-') AND ROWNUM <= 1)
      WHEN MATCHED THEN
        UPDATE
           SET Q1.NUM_IMPORTE              = NVL(Q1.NUM_IMPORTE, 0) +
                                             NVL(Q2.NUM_IMPORTE, 0),
               Q1.NEL_CANTIDAD             = NVL(Q1.NEL_CANTIDAD, 0) +
                                             NVL(Q2.NEL_CANTIDAD, 0),
               Q1.GMT_MODIFICACION         = FN_GMT_ZERO_###VERSION###,
               Q1.DES_USUARIO_MODIFICACION = par$usuario,
               Q1.OID_ULTIMA_TRANSACCION   = Q2.OID_TRANSACCION_EFECTIVO
      WHEN NOT MATCHED THEN
        INSERT
          (Q1.OID_SALDO_EFECTIVO,
           Q1.OID_CUENTA_SALDO,
           Q1.OID_ULTIMA_TRANSACCION,
           Q1.OID_DENOMINACION,
           Q1.OID_DIVISA,
           Q1.COD_NIVEL_DETALLE,
           Q1.COD_TIPO_EFECTIVO_TOTAL,
           Q1.OID_CALIDAD,
           Q1.BOL_DISPONIBLE,
           Q1.OID_UNIDAD_MEDIDA,
           Q1.NUM_IMPORTE,
           Q1.NEL_CANTIDAD,
           Q1.GMT_CREACION,
           Q1.DES_USUARIO_CREACION,
           Q1.GMT_MODIFICACION,
           Q1.DES_USUARIO_MODIFICACION,
           Q1.BOL_BLOQUEADO)
        VALUES
          (SYS_GUID(),
           Q2.OID_CUENTA_SALDO,
           par$oid_transaccion_efectivo,
           Q2.OID_DENOMINACION,
           Q2.OID_DIVISA,
           Q2.COD_NIVEL_DETALLE,
           Q2.COD_TIPO_EFECTIVO_TOTAL,
           Q2.OID_CALIDAD,
           Q2.BOL_DISPONIBLE,
           Q2.OID_UNIDAD_MEDIDA,
           Q2.NUM_IMPORTE,
           Q2.NEL_CANTIDAD,
           FN_GMT_ZERO_###VERSION###,
           par$usuario,
           FN_GMT_ZERO_###VERSION###,
           par$usuario,
           Q2.BOL_BLOQUEADO);
    END IF;

    par$inserts := 0 + sql%ROWCOUNT;

    /* DEPOIS DE ATUALIZAR O SALDO, DEVE VERIFICAR SE GEROU UM SALDO NEGATIVO E
       SE O FORMULARIO PERMITE SALDO NEGATIVO
    */

    /* SE O FORMULARIO NAO PERMITE SALDO NEGATIVO*/
    IF NOT par$permite_saldo_negativo then
      SELECT NUM_IMPORTE
        INTO var$gerou_saldo_negativo
        FROM SAPR_TSALDO_EFECTIVO
       WHERE OID_ULTIMA_TRANSACCION = par$oid_transaccion_efectivo;

      par$selects := 0 + sql%ROWCOUNT;

      /* SE GEROU UM SALDO NEGATIVO ENTAO DEVE ABORTAR O PROCESSO*/
      if var$gerou_saldo_negativo < 0 then
        /* GERA A EXCESAO QUE SERA TRATADA NA APLICACAO */
        raise_application_error(-20001,
                                gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                    '028_msg_el_movimiento_produce_saldo_negativo',
                                                                    gepr_pcomon_###VERSION###.const$CodFuncDicGrabarDoc,
                                                                    gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                                                    NULL,
                                                                    1));
      end if;
    end if;

  end ssaldo_efec_actualizar_clas;

  /* fverificar_estado_bultos */
  FUNCTION fverificar_estado_bultos(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN AS
    var$count gepr_pcomon_###VERSION###.tipo$cant_;
  begin
    var$count := 0;

    IF par$oid_remesa IS NOT NULL THEN

      BEGIN
        SELECT COUNT(B.OID_BULTO)
          INTO var$count
          FROM sapr_tremesa R
         INNER JOIN SAPR_TBULTO B
            ON B.OID_REMESA = R.OID_REMESA
         WHERE R.OID_REMESA = par$oid_remesa HAVING
         COUNT(B.OID_BULTO) IN
               (SELECT COUNT(B.COD_ESTADO)
                  FROM SAPR_TBULTO B
                 WHERE B.OID_REMESA = R.OID_REMESA
                 GROUP BY B.COD_ESTADO)
         GROUP BY R.OID_REMESA;
      EXCEPTION
        WHEN no_data_found THEN
          var$count := 0;
      END;

    END IF;

    RETURN(var$count > 0);
  END fverificar_estado_bultos;

END sapr_ptransiciones_###VERSION###;
/