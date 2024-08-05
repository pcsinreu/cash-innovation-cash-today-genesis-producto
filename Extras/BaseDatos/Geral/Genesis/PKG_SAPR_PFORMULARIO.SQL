CREATE OR REPLACE PACKAGE sapr_pformulario_###VERSION###
  AS
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pformulario_###VERSION###';

  /* determina si existen todos los formularios necesarios y que se puedan emitir del sector de origen y recibir en el de destino */
  FUNCTION fverif_formularios_sectores(par$oid_formularios    IN gepr_pcomon_###VERSION###.tipo$oids_
                                      ,par$oid_sector_origen  IN gepr_pcomon_###VERSION###.tipo$oid_
                                      ,par$oid_sector_destino IN gepr_pcomon_###VERSION###.tipo$oid_)
      RETURN BOOLEAN;

  /* verifica la existencia del formulario y que pueda emitirse desde el sector de origen y recibirse en el de destino */
  FUNCTION fverif_formulario_sectores(par$oid_formulario     IN gepr_pcomon_###VERSION###.tipo$oid_
                                     ,par$oid_sector_origen  IN gepr_pcomon_###VERSION###.tipo$oid_
                                     ,par$oid_sector_destino IN gepr_pcomon_###VERSION###.tipo$oid_)
      RETURN BOOLEAN;

  /* Verifica caracteristica de los formularios */
  FUNCTION fverif_caract_formularios(par$oid_formularios    IN gepr_pcomon_###VERSION###.tipo$oids_
                                    ,par$cod_caracteristica IN gepr_pcomon_###VERSION###.tipo$cod_)
      RETURN BOOLEAN;

  /* Verifica caracteristica del formulario */
  FUNCTION fverif_caract_formulario(par$oid_formulario     IN gepr_pcomon_###VERSION###.tipo$oid_
                                   ,par$cod_caracteristica IN gepr_pcomon_###VERSION###.tipo$cod_)
      RETURN BOOLEAN;

  /* Recupera el tipo documento */
  FUNCTION frecuperar_tipodocumento(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_)
      RETURN gepr_pcomon_###VERSION###.tipo$oid_;

  /* Verifica se formul?rio de CODIOGO: x possui determinadas caracter?sticas */
  FUNCTION fverificar_caracs_formulario(par$oid_formulario    IN gepr_pcomon_###VERSION###.tipo$oid_
                   ,par$cod_formulario    IN gepr_pcomon_###VERSION###.tipo$cod_
                   ,par$cod_caracteristicas IN VARCHAR2_ARRAY)
      RETURN gepr_pcomon_###VERSION###.tipo$oid_;
      
  /* srecuperar_form_pases_abono */
  PROCEDURE srecuperar_form_pases_abono(par$oid_sector IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$doc_rc_formulario          OUT sys_refcursor,
                                      par$doc_rc_accion_contable     OUT sys_refcursor,
                                      par$doc_rc_estado_acc_contable OUT sys_refcursor,
                                      par$doc_rc_caract_formulario   OUT sys_refcursor,
                                      par$doc_rc_grp_terminos_indiv  OUT sys_refcursor,
                                      par$doc_rc_terminos_indiv      OUT sys_refcursor,
                                      par$usuario                    IN VARCHAR2,
                                      par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$info_ejecucion             IN VARCHAR2,
                                      par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_form_gpr */
  PROCEDURE scargar_tt_oids_form_gpr(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_
                                   ,par$recuperar_padres    IN BOOLEAN
                                   ,par$inserts            OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_form_docs */
  PROCEDURE scargar_tt_oids_form_docs(par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_term_gpr */
  PROCEDURE scargar_tt_oids_term_gpr;

  /* fcargar_rc_formulario */
  FUNCTION fcargar_rc_formulario
      RETURN SYS_REFCURSOR;

  /* fcargar_rc_AccionContable */
  FUNCTION fcargar_rc_AccionContable
      RETURN SYS_REFCURSOR;

  /* fcargar_rc_EstadoAccContable */
  FUNCTION fcargar_rc_EstadoAccContable
      RETURN SYS_REFCURSOR;

  /* fcargar_rc_CaractFormulario */
  FUNCTION fcargar_rc_CaractFormulario
      RETURN SYS_REFCURSOR;

  /* fcargar_rc_GrupoTerminosIndiv */
  FUNCTION fcargar_rc_GrupoTerminosIndiv
      RETURN SYS_REFCURSOR;

  /* fcargar_rc_TerminosIndiv */
  FUNCTION fcargar_rc_TerminosIndiv
      RETURN SYS_REFCURSOR;

  /* Recupera o formulario com suas caracteristicas */
  FUNCTION frecuperar_formulario(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_formulario IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$selects        OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN gepr_pcomon_###VERSION###.tipo$formularios;

  /* Verifica se o formulario contem as caracterisitcas para gest?o de contenedores */
  FUNCTION fverif_carac_form_contenedor(par$obj_formularios IN gepr_pcomon_###VERSION###.tipo$formularios)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  /* frecuperar_formulario_reenvio */
  FUNCTION frecuperar_formulario_reenvio(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

  /* Verifica se o formulario informado contem a caracteristica */
  FUNCTION fverif_carac_form_informado(par$obj_formularios     IN gepr_pcomon_###VERSION###.tipo$formularios,
                                       par$cod_caracteristica IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  /* srecuperar_form_reenvio_v2 */
  PROCEDURE srecuperar_form_reenvio_v2(par$oid_formulario    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$bol_gestion_bulto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                       par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* srecuperar_form_baja */
  PROCEDURE srecuperar_form_baja(par$oid_formulario    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$bol_gestion_bulto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                 par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* srecuperar_formulario_altas */
  PROCEDURE srecuperar_formulario_altas(par$oid_formulario    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$bol_gestion_bulto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* srecuperar_formulario_actas */
  PROCEDURE srecuperar_formulario_actas(par$oid_formulario    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$bol_gestion_bulto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_);

END sapr_pformulario_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pformulario_###VERSION###
  AS

  /* scargar_tt_oids_form_gpr */
  PROCEDURE srecuperar_form_pases_abono(par$oid_sector IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$doc_rc_formulario          OUT sys_refcursor,
                                      par$doc_rc_accion_contable     OUT sys_refcursor,
                                      par$doc_rc_estado_acc_contable OUT sys_refcursor,
                                      par$doc_rc_caract_formulario   OUT sys_refcursor,
                                      par$doc_rc_grp_terminos_indiv  OUT sys_refcursor,
                                      par$doc_rc_terminos_indiv      OUT sys_refcursor,
                                      par$usuario                    IN VARCHAR2,
                                      par$cod_cultura                IN gepr_pcomon_###VERSION###.tipo$cod_,
                                      par$info_ejecucion             IN VARCHAR2,
                                      par$cod_ejecucion              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_form_pases_abono';

    var$inicio          gepr_pcomon_###VERSION###.tipo$gmt_;
    var$registros       gepr_pcomon_###VERSION###.tipo$nel_;
    var$inserts         gepr_pcomon_###VERSION###.tipo$nel_;
    var$updates         gepr_pcomon_###VERSION###.tipo$nel_;
    var$deletes         gepr_pcomon_###VERSION###.tipo$nel_;
    var$merges          gepr_pcomon_###VERSION###.tipo$nel_;
    var$selects         gepr_pcomon_###VERSION###.tipo$nel_;
    var$err             varchar2(4000);
    var$cod_pais        gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_delegacion  gepr_pcomon_###VERSION###.tipo$cod_;
    var$cod_formulario  gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_formulario  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_tipo_sector gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_delgacion   gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_sector      gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_formulario  gepr_pcomon_###VERSION###.tipo$desc_;

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
    
    SELECT D.COD_PAIS, D.COD_DELEGACION, S.OID_TIPO_SECTOR, D.DES_DELEGACION, S.DES_SECTOR
      INTO var$cod_pais, var$cod_delegacion, var$oid_tipo_sector, var$des_delgacion, var$des_sector
      FROM GEPR_TSECTOR S
      INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
      INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
     WHERE S.OID_SECTOR = par$oid_sector;
    
    /* Recuperar parametro IAC */
    var$cod_formulario := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => var$cod_delegacion,
                                                                         par$cod_pais       => var$cod_pais,
                                                                         par$cod_parametro  => gepr_pcomon_###VERSION###.const$CodParamIACFrmPaseAbono,
                                                                         par$cod_aplicacion => gepr_pcomon_###VERSION###.const$codAplicacionSaldos);
    
    /* Validar codigo Formulario */
    BEGIN
       SELECT F.OID_FORMULARIO, F.DES_FORMULARIO
         INTO var$oid_formulario, var$des_formulario
         FROM SAPR_TFORMULARIO F
        WHERE F.COD_FORMULARIO = var$cod_formulario;
    EXCEPTION
      WHEN no_data_found THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                            'codigo_form_pase_abono_invalido',
                                                            const$codFuncionalidad || '.' || const$nombre_func,
                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                            NULL,
                                                            1));
    END;

dbms_output.put_line(var$oid_tipo_sector);
    /* Validar tipo sector formulario */
    BEGIN
       SELECT OID_FORMULARIO
         INTO var$oid_formulario
         FROM SAPR_TFORMULARIO F
        WHERE F.OID_FORMULARIO = var$oid_formulario
          AND (SELECT COUNT(1)
            FROM GEPR_TTIPO_SECTORXFORMULARIO TSF
           WHERE TSF.OID_TIPO_SECTOR = var$oid_tipo_sector
             AND TSF.OID_FORMULARIO = F.OID_FORMULARIO) > 0;
    EXCEPTION
      WHEN no_data_found THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                            'form_invalido_config_tipo_sector',
                                                            const$codFuncionalidad || '.' || const$nombre_func,
                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                            var$des_sector || '|' || var$des_delgacion || '|' || var$des_formulario || '|' || var$cod_formulario ,
                                                            1));
    END;


    /* Validar caracteristicas formulario */
    BEGIN

          SELECT CAFO.OID_FORMULARIO
            INTO var$oid_formulario
            FROM SAPR_TCARACTFORMXFORMULARIO CAFO
      INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
      INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = CAFO.OID_FORMULARIO
        WHERE FORM.BOL_ACTIVO = 1 AND FORM.OID_FORMULARIO = var$oid_formulario
        GROUP BY CAFO.OID_FORMULARIO
  ;

    EXCEPTION
      WHEN no_data_found THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                            'form_invalido_por_caract',
                                                            const$codFuncionalidad || '.' || const$nombre_func,
                                                            gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                            NULL,
                                                            1));
    END;
    
    /* ===== Cargar tabla temporaria ==== */

    /* - Formularios */
    INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR) VALUES (var$oid_formulario, 'FORMULARIO');
    var$inserts := 0 + sql%ROWCOUNT;
    var$registros := 0 + sql%ROWCOUNT;

    /* - Terminos - Formularios */
    sapr_pformulario_###VERSION###.scargar_tt_oids_term_gpr;
    var$inserts   := var$inserts + sql%ROWCOUNT;
    var$registros := var$registros + sql%ROWCOUNT;
    
    /* ===== Cargar refcursor ==== */

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
    
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func,
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
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' || const$nombre_func,
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
      
  END srecuperar_form_pases_abono;

  /* fverif_formularios_sectores */
  FUNCTION fverif_formularios_sectores(par$oid_formularios    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       par$oid_sector_origen  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$oid_sector_destino IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN IS
    var$verificados BOOLEAN := true;
  BEGIN
    FOR idx IN par$oid_formularios.first .. par$oid_formularios.last LOOP
      IF NOT fverif_formulario_sectores(par$oid_formularios(idx),
                                        par$oid_sector_origen,
                                        par$oid_sector_destino) THEN
        var$verificados := false;
        EXIT;
      END IF;
    END LOOP;
    RETURN var$verificados;
  END fverif_formularios_sectores;

  /* fverif_formulario_sectores */
  FUNCTION fverif_formulario_sectores(par$oid_formulario     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_sector_origen  IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$oid_sector_destino IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN BOOLEAN IS
    var$cant INT := 0;
  BEGIN
    BEGIN
      SELECT s_form_sector_origen * s_form_sector_destino
        INTO var$cant
        FROM (SELECT SUM(form_sector_origen) s_form_sector_origen,
                     SUM(form_sector_destino) s_form_sector_destino
                FROM (SELECT NVL2(fso.oid_formulario, 1, 0) form_sector_origen,
                             NVL2(fsd.oid_formulario, 1, 0) form_sector_destino
                        FROM sapr_tformulario f
                        LEFT JOIN gepr_ttipo_sectorxformulario fso
                       INNER JOIN gepr_tsector so
                          ON fso.oid_tipo_sector = so.oid_tipo_sector
                         AND fso.cod_relacion_con_formulario =
                             gepr_pcomon_###VERSION###.const$CodRelConFormOri
                         AND so.oid_sector = par$oid_sector_origen ON
                       f.oid_formulario = fso.oid_formulario
                        LEFT JOIN gepr_ttipo_sectorxformulario fsd
                       INNER JOIN gepr_tsector sd
                          ON fsd.oid_tipo_sector = sd.oid_tipo_sector
                         AND fsd.cod_relacion_con_formulario =
                             gepr_pcomon_###VERSION###.const$CodRelConFormDest
                         AND sd.oid_sector = par$oid_sector_destino ON
                       f.oid_formulario = fsd.oid_formulario
                       WHERE f.oid_formulario = par$oid_formulario));

    EXCEPTION
      WHEN no_data_found THEN
        var$cant := 0;
    END;

    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);
  END fverif_formulario_sectores;

  /* fverif_caract_formularios */
  FUNCTION fverif_caract_formularios(par$oid_formularios    IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$cod_caracteristica IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN BOOLEAN IS
    var$verificados BOOLEAN := true;
  BEGIN
    FOR idx IN par$oid_formularios.first .. par$oid_formularios.last LOOP
      IF NOT fverif_caract_formulario(par$oid_formularios(idx),
                                      par$cod_caracteristica) THEN
        var$verificados := false;
        EXIT;
      END IF;
    END LOOP;
    RETURN var$verificados;
  END fverif_caract_formularios;

  /* fverif_caract_formulario */
  FUNCTION fverif_caract_formulario(par$oid_formulario     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$cod_caracteristica IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN BOOLEAN AS
    var$cant INT := 0;
  BEGIN

    BEGIN
      SELECT COUNT(1)
        INTO var$cant
        FROM sapr_tformulario f
       INNER JOIN sapr_tcaractformxformulario cff
          ON cff.oid_formulario = f.oid_formulario
       INNER JOIN sapr_tcaract_formulario cf
          ON cf.oid_caract_formulario = cff.oid_caract_formulario
       WHERE f.oid_formulario = par$oid_formulario
         AND cf.cod_caract_formulario = par$cod_caracteristica;

    EXCEPTION
      WHEN no_data_found THEN
        var$cant := 0;
    END;

    RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);

  END fverif_caract_formulario;

  /* frecuperar_tipodocumento */
  FUNCTION frecuperar_tipodocumento(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
    var$oid_tipo_documento gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    BEGIN
      SELECT f.OID_TIPO_DOCUMENTO
        INTO var$oid_tipo_documento
        FROM SAPR_TFORMULARIO f
       WHERE f.OID_FORMULARIO = par$oid_formulario;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_tipo_documento := NULL;
    END;

    RETURN var$oid_tipo_documento;
  END frecuperar_tipodocumento;

  /* fverificar_caracs_formulario */
  FUNCTION fverificar_caracs_formulario(par$oid_formulario      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$cod_formulario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$cod_caracteristicas IN VARCHAR2_ARRAY)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
    var$oid_formulario      gepr_pcomon_###VERSION###.tipo$oid_;
    var$filtro_busqueda     gepr_pcomon_###VERSION###.tipo$oid_;
    var$cantCaracteristicas gepr_pcomon_###VERSION###.tipo$cant_;

  BEGIN

    IF (par$oid_formulario IS NOT NULL) THEN
      var$filtro_busqueda := 'OID_FORMULARIO';
    ELSIF (par$cod_formulario IS NOT NULL) THEN
      var$filtro_busqueda := 'COD_FORMULARIO';
    ELSE
      DBMS_OUTPUT.put_line('SEM FORM');
      --INSERIR TRATAMENTO DE ERRO, CASO N?O TENHA INFORMADO (OID_FORMULARIO OU CODIGO FORMULARIO)
    END IF;

    IF (par$cod_caracteristicas IS NULL OR
       par$cod_caracteristicas.COUNT = 0) THEN
      DBMS_OUTPUT.put_line('SEM CARACS');
      --INSERIR TRATAMENTO DE ERRO, CASO N?O TENHA INFORMADO (OID_FORMULARIO OU CODIGO FORMULARIO)
    ELSE
      var$cantCaracteristicas := par$cod_caracteristicas.COUNT;
    END IF;

    BEGIN

      SELECT FORM.OID_FORMULARIO
        INTO var$oid_formulario
        FROM SAPR_TFORMULARIO FORM
       WHERE FORM.OID_FORMULARIO IN
             (SELECT CAFO.OID_FORMULARIO
                FROM SAPR_TCARACTFORMXFORMULARIO CAFO
               INNER JOIN SAPR_TCARACT_FORMULARIO CARA
                  ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
               WHERE 1 = 1
                 AND COD_CARACT_FORMULARIO IN
                     (SELECT COLUMN_VALUE FROM TABLE(par$cod_caracteristicas))

               GROUP BY CAFO.OID_FORMULARIO
              HAVING COUNT(CAFO.OID_CARACT_FORMULARIO) = var$cantCaracteristicas -- CANTIDAD DE CARACTERISTICAS DEL FILTRO
              )
         AND FORM.BOL_ACTIVO = 1
         AND ((var$filtro_busqueda = 'COD_FORMULARIO' AND
             FORM.COD_FORMULARIO = par$cod_formulario) OR
             (var$filtro_busqueda = 'OID_FORMULARIO' AND
             FORM.OID_FORMULARIO = par$oid_formulario));

    EXCEPTION
      WHEN no_data_found THEN
        var$oid_formulario := NULL;
    END;

    RETURN var$oid_formulario;

  END fverificar_caracs_formulario;

  /* scargar_tt_oids_form_gpr */
  PROCEDURE scargar_tt_oids_form_gpr(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$recuperar_padres    IN BOOLEAN,
                                     par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      SELECT OID_FORMULARIO, 'FORMULARIO'
        FROM SAPR_TGRUPO_DOCUMENTO
       WHERE OID_GRUPO_DOCUMENTO = par$oid_grupo_documento;
    par$inserts := 0 + sql%ROWCOUNT;

    IF par$recuperar_padres THEN

      INSERT INTO SAPR_GTT_TOIDS
        (OID, COD_CALIFICADOR)
        SELECT DISTINCT OID_FORMULARIO, 'FORMULARIO'
          FROM SAPR_TDOCUMENTO
         WHERE OID_DOCUMENTO in
               (SELECT DISTINCT OID OID_DOCUMENTO
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'DOCUMENTO'
                    OR COD_CALIFICADOR = 'DOC_PADRE')
           AND OID_FORMULARIO IS NOT NULL;
      par$inserts := par$inserts + sql%ROWCOUNT;

    END IF;

  END scargar_tt_oids_form_gpr;

  /* scargar_tt_oids_form_docs */
  PROCEDURE scargar_tt_oids_form_docs(par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      SELECT DISTINCT OID_FORMULARIO, 'FORMULARIO'
          FROM SAPR_TDOCUMENTO
         WHERE OID_DOCUMENTO in
               (SELECT DISTINCT OID OID_DOCUMENTO
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'DOCUMENTO'
                    OR COD_CALIFICADOR = 'DOC_PADRE')
           AND OID_FORMULARIO IS NOT NULL;
    par$inserts := 0 + sql%ROWCOUNT;

  END scargar_tt_oids_form_docs;

  /* scargar_tt_oids_term_gpr */
  PROCEDURE scargar_tt_oids_term_gpr IS
  BEGIN

    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
       (SELECT F.OID_IAC_INDIVIDUAL, F.OID_IAC_GRUPO
          FROM SAPR_TFORMULARIO F
         INNER JOIN SAPR_GTT_TOIDS t
            ON F.OID_FORMULARIO = t.OID
           AND t.COD_CALIFICADOR = 'FORMULARIO'),
      q1 AS
       (SELECT DISTINCT OID_IAC
          FROM (SELECT OID_IAC_INDIVIDUAL OID_IAC
                  FROM q
                UNION ALL
                SELECT OID_IAC_GRUPO OID_IAC FROM q))
      SELECT OID_IAC, 'TERMINO' FROM q1 WHERE OID_IAC IS NOT NULL;

  END scargar_tt_oids_term_gpr;

  /* fcargar_rc_formulario */
  FUNCTION fcargar_rc_formulario RETURN SYS_REFCURSOR IS
    var$rcFormulario SYS_REFCURSOR;
  BEGIN

    OPEN var$rcFormulario FOR
      SELECT F.OID_FORMULARIO,
             F.OID_ACCION_CONTABLE,
             F.OID_IAC_INDIVIDUAL,
             F.OID_IAC_GRUPO,
             F.OID_FILTRO_FORMULARIO,
             F.OID_TIPO_DOCUMENTO,
             F.COD_FORMULARIO,
             F.DES_FORMULARIO,
             F.COD_MIGRACION,
             F.COD_COLOR,
             F.BIN_ICONO_FORMULARIO,
             F.BOL_ACTIVO,
             F.GMT_CREACION,
             F.DES_USUARIO_CREACION,
             F.GMT_MODIFICACION,
             F.DES_USUARIO_MODIFICACION,
			 F.DES_COD_EXTERNO,
             AC.COD_ACCION_CONTABLE,
             AC.DES_ACCION_CONTABLE,
             AC.BOL_ACTIVO               AS AC_BOL_ACTIVO,
             AC.GMT_CREACION             AS AC_GMT_CREACION,
             AC.DES_USUARIO_CREACION     AS AC_DES_USUARIO_CREACION,
             AC.GMT_MODIFICACION         AS AC_GMT_MODIFICACION,
             AC.DES_USUARIO_MODIFICACION AS AC_DES_USUARIO_MODIFICACION,
             FF.DES_FILTRO_FORMULARIO,
             FF.BOL_SOLO_DISPONIBLE,
             FF.BOL_CON_VALOR,
             FF.BOL_CON_BULTO            AS FF_BOL_CON_BULTO,
             FF.BOL_SOLO_REENVIO,
             FF.BOL_SOLO_SUSTITUCION,
             FF.BOL_CON_FECHA_ESPECIFICA,
             FF.NEC_DIAS_BUSQUEDA_INICIO,
             FF.NEC_DIAS_BUSQUEDA_FIN,
             FF.BOL_ACTIVO               AS FF_BOL_ACTIVO,
             FF.GMT_CREACION             AS FF_GMT_CREACION,
             FF.DES_USUARIO_CREACION     AS FF_DES_USUARIO_CREACION,
             FF.GMT_MODIFICACION         AS FF_GMT_MODIFICACION,
             FF.DES_USUARIO_MODIFICACION AS FF_DES_USUARIO_MODIFICACION,
             TD.COD_TIPO_DOCUMENTO,
             TD.DES_TIPO_DOCUMENTO,
             TD.NEC_ORDEN,
             TD.BOL_ACTIVO               AS TD_BOL_ACTIVO,
             TD.BOL_CERTIFICACION,
             TD.GMT_CREACION             AS TD_GMT_CREACION,
             TD.DES_USUARIO_CREACION     AS TD_DES_USUARIO_CREACION,
             TD.GMT_MODIFICACION         AS TD_GMT_MODIFICACION,
             TD.DES_USUARIO_MODIFICACION AS TD_DES_USUARIO_MODIFICACION,
			 F.DES_COD_EXTERNO,
             CASE WHEN NVL((SELECT COUNT(1) FROM SAPR_TCOPIA COPI WHERE COPI.OID_FORMULARIO = F.OID_FORMULARIO), 0) > 1 THEN '1' ELSE '0' END BOL_IMPRIMIR
        FROM SAPR_TFORMULARIO F
       INNER JOIN SAPR_TACCION_CONTABLE AC
          ON F.OID_ACCION_CONTABLE = AC.OID_ACCION_CONTABLE
       INNER JOIN SAPR_TTIPO_DOCUMENTO TD
          ON F.OID_TIPO_DOCUMENTO = TD.OID_TIPO_DOCUMENTO
        LEFT OUTER JOIN SAPR_TFILTRO_FORMULARIO FF
          ON F.OID_FILTRO_FORMULARIO = FF.OID_FILTRO_FORMULARIO
       WHERE F.OID_FORMULARIO IN
             (SELECT OID OID_FORMULARIO
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'FORMULARIO');

    RETURN var$rcFormulario;

  END fcargar_rc_formulario;

  /* fcargar_rc_AccionContable */
  FUNCTION fcargar_rc_AccionContable RETURN SYS_REFCURSOR IS
    var$rcAccionContable SYS_REFCURSOR;
  BEGIN

    OPEN var$rcAccionContable FOR
      SELECT AC.OID_ACCION_CONTABLE,
             AC.COD_ACCION_CONTABLE,
             AC.DES_ACCION_CONTABLE,
             AC.COD_MIGRACION,
             AC.BOL_ACTIVO,
             AC.GMT_CREACION,
             AC.DES_USUARIO_CREACION,
             AC.GMT_MODIFICACION,
             AC.DES_USUARIO_MODIFICACION
        FROM SAPR_TACCION_CONTABLE AC
       INNER JOIN SAPR_TFORMULARIO F
          ON F.OID_ACCION_CONTABLE = AC.OID_ACCION_CONTABLE
       WHERE F.OID_FORMULARIO IN
             (SELECT OID OID_FORMULARIO
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'FORMULARIO');

    RETURN var$rcAccionContable;

  END fcargar_rc_AccionContable;

  /* fcargar_rc_EstadoAccContable */
  FUNCTION fcargar_rc_EstadoAccContable RETURN SYS_REFCURSOR IS
    var$rcEstadoAccContable SYS_REFCURSOR;
  BEGIN

    OPEN var$rcEstadoAccContable FOR
      SELECT EAC.OID_ESTADOXACCION_CONTABLE,
             EAC.OID_ACCION_CONTABLE,
             EAC.COD_ESTADO,
             EAC.COD_ACCION_ORIGEN_DISPONIBLE,
             EAC.COD_ACCION_ORIGEN_NODISP,
             EAC.COD_ACCION_DESTINO_DISPONIBLE,
             EAC.COD_ACCION_DESTINO_NODISP,
             EAC.GMT_CREACION,
             EAC.DES_USUARIO_CREACION,
             EAC.GMT_MODIFICACION,
             EAC.DES_USUARIO_MODIFICACION
        FROM SAPR_TESTADOXACCION_CONTABLE EAC
       INNER JOIN SAPR_TFORMULARIO F
          ON F.OID_ACCION_CONTABLE = EAC.OID_ACCION_CONTABLE
       WHERE F.OID_FORMULARIO IN
             (SELECT OID OID_FORMULARIO
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'FORMULARIO');

    RETURN var$rcEstadoAccContable;

  END fcargar_rc_EstadoAccContable;

  /* fcargar_rc_CaractFormulario */
  FUNCTION fcargar_rc_CaractFormulario RETURN SYS_REFCURSOR IS
    var$rcCaractFormulario SYS_REFCURSOR;
  BEGIN

    OPEN var$rcCaractFormulario FOR
      SELECT CFF.OID_FORMULARIO, CF.COD_CARACT_FORMULARIO
        FROM SAPR_TCARACT_FORMULARIO CF
       INNER JOIN SAPR_TCARACTFORMXFORMULARIO CFF
          ON CFF.OID_CARACT_FORMULARIO = CF.OID_CARACT_FORMULARIO
       WHERE CFF.OID_FORMULARIO IN
             (SELECT OID OID_FORMULARIO
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'FORMULARIO')
         AND CF.COD_TIPO_CARACTERISTICA = 'F';

    RETURN var$rcCaractFormulario;

  END fcargar_rc_CaractFormulario;

  /* fcargar_rc_GrupoTerminosIndiv */
  FUNCTION fcargar_rc_GrupoTerminosIndiv RETURN SYS_REFCURSOR IS
    var$rcGrupoTerminosIndiv SYS_REFCURSOR;
  BEGIN

    OPEN var$rcGrupoTerminosIndiv FOR
      SELECT IAC.OID_IAC,
             IAC.COD_IAC,
             IAC.DES_IAC,
             IAC.OBS_IAC,
             IAC.BOL_VIGENTE,
             IAC.BOL_INVISIBLE,
             IAC.COD_USUARIO,
             IAC.FYH_ACTUALIZACION,
             IAC.BOL_COPIA_DECLARADOS
        FROM GEPR_TINFORM_ADICIONAL_CLIENTE IAC
       INNER JOIN SAPR_GTT_TOIDS tmp
          ON IAC.OID_IAC = tmp.OID
         AND tmp.COD_CALIFICADOR = 'TERMINO';

    RETURN var$rcGrupoTerminosIndiv;

  END fcargar_rc_GrupoTerminosIndiv;

  /* fcargar_rc_TerminosIndiv */
  FUNCTION fcargar_rc_TerminosIndiv RETURN SYS_REFCURSOR IS
    var$rcTerminosIndiv SYS_REFCURSOR;
  BEGIN

    OPEN var$rcTerminosIndiv FOR
      SELECT TI.OID_IAC,
             TI.OID_TERMINO,
             TI.BOL_BUSQUEDA_PARCIAL,
             TI.BOL_CAMPO_CLAVE,
             TI.NEC_ORDEN,
             TI.BOL_ES_OBLIGATORIO,
             TI.COD_USUARIO,
             TI.FYH_ACTUALIZACION,
             TI.BOL_TERMINO_COPIA,
             TI.BOL_ES_PROTEGIDO,
             TI.COD_MIGRACION,
             T.COD_TERMINO,
             T.DES_TERMINO,
             T.OBS_TERMINO,
             T.NEC_LONGITUD,
             T.BOL_MOSTRAR_CODIGO,
             T.BOL_VALORES_POSIBLES,
             T.BOL_ACEPTAR_DIGITACION,
             T.BOL_VIGENTE,
             T.BOL_ESPECIFICO_DE_SALDOS,
             FORM.OID_FORMATO,
             FORM.COD_FORMATO,
             FORM.DES_FORMATO,
             FORM.COD_USUARIO              AS F_COD_USUARIO,
             FORM.FYH_ACTUALIZACION        AS F_FYH_ACTUALIZACION,
             AV.OID_ALGORITMO_VALIDACION,
             AV.COD_ALGORITMO_VALIDACION,
             AV.DES_ALGORITMO_VALIDACION,
             AV.OBS_ALGORITMO_VALIDACION,
             AV.BOL_APLICA_TERM_MEDIO_PAGO AS AV_BOL_APLICA_TERM_MEDIO_PAGO,
             AV.BOL_APLICA_TERM_IAC        AS AV_BOL_APLICA_TERM_IAC,
             AV.COD_USUARIO                AS AV_COD_USUARIO,
             AV.FYH_ACTUALIZACION          AS AV_FYH_ACTUALIZACION,
             M.OID_MASCARA,
             M.COD_MASCARA,
             M.DES_MASCARA,
             M.DES_EXP_REGULAR,
             M.BOL_APLICA_TERM_MEDIO_PAGO  AS M_BOL_APLICA_TERM_MEDIO_PAGO,
             M.BOL_APLICA_TERM_IAC         AS M_BOL_APLICA_TERM_IAC,
             M.COD_USUARIO                 AS M_COD_USUARIO,
             M.FYH_ACTUALIZACION           AS M_FYH_ACTUALIZACION
        FROM GEPR_TTERMINO_POR_IAC TI
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TI.OID_TERMINO
       INNER JOIN GEPR_TFORMATO FORM
          ON FORM.OID_FORMATO = T.OID_FORMATO
        LEFT OUTER JOIN GEPR_TALGORITMO_VALIDACION AV
          ON AV.OID_ALGORITMO_VALIDACION = T.OID_ALGORITMO_VALIDACION
        LEFT OUTER JOIN GEPR_TMASCARA M
          ON M.OID_MASCARA = T.OID_MASCARA
       INNER JOIN SAPR_GTT_TOIDS tmp
          ON TI.OID_IAC = tmp.OID
         AND tmp.COD_CALIFICADOR = 'TERMINO';

    RETURN var$rcTerminosIndiv;

  END fcargar_rc_TerminosIndiv;

  FUNCTION frecuperar_formulario(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_formulario IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$selects        OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN gepr_pcomon_###VERSION###.tipo$formularios IS
    var$obj_formularios gepr_pcomon_###VERSION###.tipo$formularios;
    var$idx             gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    par$selects := 0;

    IF par$oid_formulario IS NOT NULL THEN

      FOR F IN (SELECT F.OID_FORMULARIO,
                       F.COD_FORMULARIO,
                       F.OID_ACCION_CONTABLE,
                       F.OID_TIPO_DOCUMENTO,
                       CF.OID_CARACT_FORMULARIO,
                       CF.COD_CARACT_FORMULARIO
                  FROM SAPR_TFORMULARIO F
                 INNER JOIN SAPR_TCARACTFORMXFORMULARIO CFF
                    ON CFF.OID_FORMULARIO = F.OID_FORMULARIO
                 INNER JOIN SAPR_TCARACT_FORMULARIO CF
                    ON CF.OID_CARACT_FORMULARIO = CFF.OID_CARACT_FORMULARIO
                 WHERE F.OID_FORMULARIO = par$oid_formulario) LOOP
        var$obj_formularios(var$idx).OID_FORMULARIO := f.OID_FORMULARIO;
        var$obj_formularios(var$idx).COD_FORMULARIO := f.COD_FORMULARIO;
        var$obj_formularios(var$idx).OID_ACCION_CONTABLE := f.OID_ACCION_CONTABLE;
        var$obj_formularios(var$idx).OID_TIPO_DOCUMENTO := f.OID_TIPO_DOCUMENTO;
        var$obj_formularios(var$idx).OID_CARACT_FORMULARIO := f.OID_CARACT_FORMULARIO;
        var$obj_formularios(var$idx).COD_CARACT_FORMULARIO := f.COD_CARACT_FORMULARIO;

        var$idx := var$idx + 1;

      END LOOP;

      par$selects := par$selects + sql%ROWCOUNT;

    ELSIF par$cod_formulario IS NOT NULL THEN

      FOR F IN (SELECT F.OID_FORMULARIO,
                       F.COD_FORMULARIO,
                       F.OID_ACCION_CONTABLE,
                       F.OID_TIPO_DOCUMENTO,
                       CF.OID_CARACT_FORMULARIO,
                       CF.COD_CARACT_FORMULARIO
                  FROM SAPR_TFORMULARIO F
                 INNER JOIN SAPR_TCARACTFORMXFORMULARIO CFF
                    ON CFF.OID_FORMULARIO = F.OID_FORMULARIO
                 INNER JOIN SAPR_TCARACT_FORMULARIO CF
                    ON CF.OID_CARACT_FORMULARIO = CFF.OID_CARACT_FORMULARIO
                 WHERE F.COD_FORMULARIO = par$cod_formulario) LOOP
        var$obj_formularios(var$idx).OID_FORMULARIO := f.OID_FORMULARIO;
        var$obj_formularios(var$idx).COD_FORMULARIO := f.COD_FORMULARIO;
        var$obj_formularios(var$idx).OID_ACCION_CONTABLE := f.OID_ACCION_CONTABLE;
        var$obj_formularios(var$idx).OID_TIPO_DOCUMENTO := f.OID_TIPO_DOCUMENTO;
        var$obj_formularios(var$idx).OID_CARACT_FORMULARIO := f.OID_CARACT_FORMULARIO;
        var$obj_formularios(var$idx).COD_CARACT_FORMULARIO := f.COD_CARACT_FORMULARIO;

        var$idx := var$idx + 1;

      END LOOP;

      par$selects := par$selects + sql%ROWCOUNT;

    END IF;

    return var$obj_formularios;
  END frecuperar_formulario;


  /* frecuperar_formulario_reenvio */
  FUNCTION frecuperar_formulario_reenvio(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_)
        RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'frecuperar_formulario_reenvio';

    var$oid_formulario_reenvio gepr_pcomon_###VERSION###.tipo$oid_;

        /*CURSOR DE CARACTERISTICAS FORMULARIO*/
        CURSOR cur$caracteristicas(par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_) IS
          SELECT C.OID_CARACT_FORMULARIO
            FROM SAPR_TCARACT_FORMULARIO C
      INNER JOIN SAPR_TCARACTFORMXFORMULARIO CF ON CF.OID_CARACT_FORMULARIO = C.OID_CARACT_FORMULARIO
       WHERE CF.OID_FORMULARIO = par$oid_formulario
         AND (C.Cod_Caract_Formulario like 'CARACTERISTICA_PRINCIPAL_GESTION_%'
           OR C.Cod_Caract_Formulario like 'INTEGRACION_%');

  BEGIN

    FOR rec$caracteristica IN cur$caracteristicas(par$oid_formulario) LOOP
       INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR) VALUES (rec$caracteristica.oid_caract_formulario, 'CARACFORMULARIOREV');
    END LOOP rec$caracteristica;

    /* Caracteristicas obrigatorias de reenvio automatico */
    INSERT INTO SAPR_GTT_TOIDS
      SELECT C.OID_CARACT_FORMULARIO, 'CARACFORMULARIOREV'
        FROM SAPR_TCARACT_FORMULARIO C
       WHERE C.Cod_Caract_Formulario in (gepr_pcomon_###VERSION###.const$CodCaracReenvioAuto, gepr_pcomon_###VERSION###.const$CodCaracReenvio);

    BEGIN
            SELECT DISTINCT CAFO.OID_FORMULARIO
              INTO var$oid_formulario_reenvio
              FROM SAPR_TCARACTFORMXFORMULARIO CAFO
        INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
          WHERE CARA.OID_CARACT_FORMULARIO IN (SELECT OID FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOREV')
          GROUP BY CAFO.OID_FORMULARIO
          HAVING COUNT(CAFO.OID_CARACT_FORMULARIO) = (SELECT COUNT(1) FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOREV' GROUP BY COD_CALIFICADOR);
    EXCEPTION
      WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                        gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                              'form_reenvioautomatico_invalido',
                                                              const$codFuncionalidad || '.' || const$nombre_func,
                                                              gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                              NULL,
                                                              1));
    END;


    RETURN var$oid_formulario_reenvio;
  END;

 /* Verifica se o formulario contem as caracterisitcas para gest?o de contenedores */
  FUNCTION fverif_carac_form_contenedor(par$obj_formularios IN gepr_pcomon_###VERSION###.tipo$formularios)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS
    var$nel_retorno gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
  BEGIN

    IF par$obj_formularios IS NOT NULL and par$obj_formularios.count > 0 THEN

      /* Valida se o formulario tem a caracteristica de gest?o de contenedores
      e documento individual */
      IF fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                     par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionContd) > 0 AND
         (fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                      par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracDocIndividual) > 0 OR
          fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                      par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracDocGrupo) > 0) THEN

        /* Valida se o formulario tem a caracteristica de alta, reenvio,sustituicion ou baja */
        IF fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                       par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAlta) > 0 OR
           (fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                        par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracBaja) > 0 AND
            (fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                         par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracSalidaRecor) > 0 OR
             fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                         par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracDesarmarCont) > 0)) OR
           (fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                        par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvio) > 0 AND
            (fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                         par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvSecMismaPla) > 0 OR
             fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                         par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvSecDistPla) > 0 OR
             fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                         par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracReenvioClientes) > 0)) OR
           fverif_carac_form_informado(par$obj_formularios    => par$obj_formularios,
                                       par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracAccionSustit) > 0 THEN

          var$nel_retorno := 1;

        END IF;
      END IF;

    END IF;

    return var$nel_retorno;
  END fverif_carac_form_contenedor;

  /* Verifica se o formulario informado contem a caracteristica */
  FUNCTION fverif_carac_form_informado(par$obj_formularios     IN gepr_pcomon_###VERSION###.tipo$formularios,
                                       par$cod_caracteristica IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS
    var$nel_retorno gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
  BEGIN

    IF par$obj_formularios IS NOT NULL THEN

      FOR idx IN par$obj_formularios.first .. par$obj_formularios.last LOOP
        IF par$obj_formularios(idx)
         .COD_CARACT_FORMULARIO = par$cod_caracteristica THEN
          var$nel_retorno := 1;
        END IF;

      END LOOP;

    END IF;

    return var$nel_retorno;
  END fverif_carac_form_informado;

  /* srecuperar_form_reenvio_v2 */
  PROCEDURE srecuperar_form_reenvio_v2(par$oid_formulario    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$bol_gestion_bulto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                       par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_form_reenvio_v2';
    var$cod_caracteristicas gepr_pcomon_###VERSION###.tipo$desc_ := gepr_pcomon_###VERSION###.const$CodCaracGestionRemes;

  BEGIN

    IF par$oid_formulario IS NOT NULL THEN

        /* Verificar si es gestion de bulto */
        IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                              par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionBulto) THEN
           par$bol_gestion_bulto := 1;
        END IF;

    ELSE

        IF par$bol_gestion_bulto = 1 THEN
          var$cod_caracteristicas := gepr_pcomon_###VERSION###.const$CodCaracGestionBulto;
        END IF;

        INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR)
         SELECT CF.OID_CARACT_FORMULARIO, 'CARACFORMULARIOREENVIO'
           FROM SAPR_TCARACT_FORMULARIO CF
          WHERE CF.COD_CARACT_FORMULARIO IN (gepr_pcomon_###VERSION###.const$CodCaracReenvio, par$cod_integracion, var$cod_caracteristicas);

        BEGIN
                SELECT DISTINCT CAFO.OID_FORMULARIO
                  INTO par$oid_formulario
                  FROM SAPR_TCARACTFORMXFORMULARIO CAFO
            INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
            INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = CAFO.OID_FORMULARIO
              WHERE FORM.BOL_ACTIVO = 1 AND CARA.OID_CARACT_FORMULARIO IN (SELECT OID FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOREENVIO')
              GROUP BY CAFO.OID_FORMULARIO
              HAVING COUNT(CAFO.OID_CARACT_FORMULARIO) = (SELECT COUNT(1) FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOREENVIO' GROUP BY COD_CALIFICADOR);
        EXCEPTION
          WHEN no_data_found THEN
              raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'formulario_reenvio_invalido',
                                                                  const$codFuncionalidad || '.' || const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
        END;
    END IF;

  END srecuperar_form_reenvio_v2;

  /* srecuperar_form_baja */
  PROCEDURE srecuperar_form_baja(par$oid_formulario    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$bol_gestion_bulto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                 par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                 par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_form_baja';
    var$cod_caracteristicas gepr_pcomon_###VERSION###.tipo$desc_ := gepr_pcomon_###VERSION###.const$CodCaracGestionRemes;

  BEGIN

    IF par$oid_formulario IS NOT NULL THEN

        /* Verificar si es gestion de bulto */
        IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                              par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionBulto) THEN
           par$bol_gestion_bulto := 1;
        END IF;

    ELSE

        IF par$bol_gestion_bulto = 1 THEN
          var$cod_caracteristicas := gepr_pcomon_###VERSION###.const$CodCaracGestionBulto;
        END IF;

        INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR)
         SELECT CF.OID_CARACT_FORMULARIO, 'CARACFORMULARIOBAJA'
           FROM SAPR_TCARACT_FORMULARIO CF
          WHERE CF.COD_CARACT_FORMULARIO IN (gepr_pcomon_###VERSION###.const$CodCaracSalidaRecor, par$cod_integracion, var$cod_caracteristicas);

        BEGIN
                SELECT DISTINCT CAFO.OID_FORMULARIO
                  INTO par$oid_formulario
                  FROM SAPR_TCARACTFORMXFORMULARIO CAFO
            INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
            INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = CAFO.OID_FORMULARIO
              WHERE FORM.BOL_ACTIVO = 1 AND CARA.OID_CARACT_FORMULARIO IN (SELECT OID FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOBAJA')
              GROUP BY CAFO.OID_FORMULARIO
              HAVING COUNT(CAFO.OID_CARACT_FORMULARIO) = (SELECT COUNT(1) FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOBAJA' GROUP BY COD_CALIFICADOR);
        EXCEPTION
          WHEN no_data_found THEN
              raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'formulario_baja_invalido',
                                                                  const$codFuncionalidad || '.' || const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
        END;
    END IF;

  END srecuperar_form_baja;

  /* srecuperar_formulario_altas */
  PROCEDURE srecuperar_formulario_altas(par$oid_formulario    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$bol_gestion_bulto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_formulario_altas';
    var$cod_caracteristicas gepr_pcomon_###VERSION###.tipo$desc_ := gepr_pcomon_###VERSION###.const$CodCaracGestionRemes;

  BEGIN

    IF par$oid_formulario IS NOT NULL THEN

        /* Verificar si es gestion de bulto */
        IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                              par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionBulto) THEN
           par$bol_gestion_bulto := 1;
        END IF;

    ELSE

        IF par$bol_gestion_bulto = 1 THEN
          var$cod_caracteristicas := gepr_pcomon_###VERSION###.const$CodCaracGestionBulto;
        END IF;

        INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR)
         SELECT CF.OID_CARACT_FORMULARIO, 'CARACFORMULARIOALTA'
           FROM SAPR_TCARACT_FORMULARIO CF
          WHERE CF.COD_CARACT_FORMULARIO IN (gepr_pcomon_###VERSION###.const$CodCaracAlta, par$cod_integracion, var$cod_caracteristicas);

        BEGIN
                SELECT DISTINCT CAFO.OID_FORMULARIO
                  INTO par$oid_formulario
                  FROM SAPR_TCARACTFORMXFORMULARIO CAFO
            INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
            INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = CAFO.OID_FORMULARIO
              WHERE FORM.BOL_ACTIVO = 1 AND CARA.OID_CARACT_FORMULARIO IN (SELECT OID FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOALTA')
              GROUP BY CAFO.OID_FORMULARIO
              HAVING COUNT(CAFO.OID_CARACT_FORMULARIO) = (SELECT COUNT(1) FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOALTA' GROUP BY COD_CALIFICADOR);
        EXCEPTION
          WHEN no_data_found THEN
              raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'formulario_alta_invalido',
                                                                  const$codFuncionalidad || '.' || const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
        END;
    END IF;

  END srecuperar_formulario_altas;

  PROCEDURE srecuperar_formulario_actas(par$oid_formulario    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$bol_gestion_bulto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$cod_integracion   IN gepr_pcomon_###VERSION###.tipo$desc_,
                                        par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_formulario_actas';
    var$cod_caracteristicas gepr_pcomon_###VERSION###.tipo$desc_ := gepr_pcomon_###VERSION###.const$CodCaracGestionRemes;

  BEGIN

    IF par$oid_formulario IS NOT NULL THEN

        /* Verificar si es gestion de bulto */
        IF sapr_pformulario_###VERSION###.fverif_caract_formulario(par$oid_formulario     => par$oid_formulario,
                                                              par$cod_caracteristica => gepr_pcomon_###VERSION###.const$CodCaracGestionBulto) THEN
           par$bol_gestion_bulto := 1;
        END IF;

    ELSE

        IF par$bol_gestion_bulto = 1 THEN
          var$cod_caracteristicas := gepr_pcomon_###VERSION###.const$CodCaracGestionBulto;
        END IF;

        INSERT INTO SAPR_GTT_TOIDS (OID, COD_CALIFICADOR)
         SELECT CF.OID_CARACT_FORMULARIO, 'CARACFORMULARIOACTA'
           FROM SAPR_TCARACT_FORMULARIO CF
          WHERE CF.COD_CARACT_FORMULARIO IN (gepr_pcomon_###VERSION###.const$CodCaracActa, par$cod_integracion, var$cod_caracteristicas);

        BEGIN
                SELECT DISTINCT CAFO.OID_FORMULARIO
                  INTO par$oid_formulario
                  FROM SAPR_TCARACTFORMXFORMULARIO CAFO
					INNER JOIN SAPR_TCARACT_FORMULARIO CARA ON CAFO.OID_CARACT_FORMULARIO = CARA.OID_CARACT_FORMULARIO
					INNER JOIN SAPR_TFORMULARIO FORM ON FORM.OID_FORMULARIO = CAFO.OID_FORMULARIO
              WHERE FORM.BOL_ACTIVO = 1 AND CARA.OID_CARACT_FORMULARIO IN (SELECT OID FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOACTA')
              GROUP BY CAFO.OID_FORMULARIO
              HAVING COUNT(CAFO.OID_CARACT_FORMULARIO) = (SELECT COUNT(1) FROM SAPR_GTT_TOIDS WHERE COD_CALIFICADOR = 'CARACFORMULARIOACTA' GROUP BY COD_CALIFICADOR);
        EXCEPTION
          WHEN no_data_found THEN
              raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                            gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'formulario_acto_invalido',
                                                                  const$codFuncionalidad || '.' || const$nombre_func,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  NULL,
                                                                  1));
        END;
    END IF;

  END srecuperar_formulario_actas;

END sapr_pformulario_###VERSION###;
/
