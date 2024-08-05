CREATE OR REPLACE PACKAGE sapr_pdivisa_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  PROCEDURE scargar_tt_oids_divisa_gpr(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_tt_oids_div_remesas(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_tt_oids_div_bultos(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_tt_oids_div_parciales(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_divisas(par$rc_divisas              out SYS_REFCURSOR,
                            par$rc_denominaciones       out SYS_REFCURSOR,
                            par$rc_mediospago           out SYS_REFCURSOR,
                            par$rc_unidades_medida      out SYS_REFCURSOR,
                            par$rc_calidades            out SYS_REFCURSOR,
                            par$rc_terminos_medios_pago OUT SYS_REFCURSOR,
                            par$selects                 OUT gepr_pcomon_###VERSION###.tipo$nel_);

  FUNCTION fcargar_rc_divisa(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  FUNCTION fcargar_rc_denominacion(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  FUNCTION fcargar_rc_mediopago(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  FUNCTION fcargar_rc_unidadmedida(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  FUNCTION fcargar_rc_calidades(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  FUNCTION fcargar_rc_terminos_mp(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

END sapr_pdivisa_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pdivisa_###VERSION### AS

  PROCEDURE scargar_tt_oids_divisa_gpr(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
       (SELECT OID OID_DOCUMENTO
          FROM SAPR_GTT_TOIDS
         WHERE COD_CALIFICADOR = 'DOCUMENTO'
            OR COD_CALIFICADOR = 'DOC_PADRE'),
      q1 AS
       (SELECT DISTINCT OID_DIVISA,
                        OID_DENOMINACION,
                        OID_MEDIO_PAGO,
                        OID_UNIDAD_MEDIDA,
                        OID_CALIDAD
          FROM (SELECT OID_DIVISA,
                       OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       OID_UNIDAD_MEDIDA,
                       OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TEFECTIVOXDOCUMENTO e
                    ON q.OID_DOCUMENTO = e.OID_DOCUMENTO
                UNION ALL
                SELECT OID_DIVISA,
                       NULL           AS OID_DENOMINACION,
                       OID_MEDIO_PAGO,
                       NULL           AS OID_UNIDAD_MEDIDA,
                       NULL           AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TMEDIO_PAGOXDOCUMENTO m
                    ON q.OID_DOCUMENTO = m.OID_DOCUMENTO))
      SELECT DISTINCT OID_DIVISA, 'DIVISA'
        FROM q1
       WHERE OID_DIVISA IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_DENOMINACION, 'DENOMINAC'
        FROM q1
       WHERE OID_DENOMINACION IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_MEDIO_PAGO, 'MEDIO_PAGO'
        FROM q1
       WHERE OID_MEDIO_PAGO IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_UNIDAD_MEDIDA, 'U_MEDIDA'
        FROM q1
       WHERE OID_UNIDAD_MEDIDA IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_CALIDAD, 'CALIDAD'
        FROM q1
       WHERE OID_CALIDAD IS NOT NULL;
    par$inserts := 0 + sql%ROWCOUNT;

  END scargar_tt_oids_divisa_gpr;

  PROCEDURE scargar_tt_oids_div_remesas(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
       (SELECT OID OID_REMESA
          FROM SAPR_GTT_TOIDS
         WHERE COD_CALIFICADOR = 'REMESA'),
      q1 AS
       (SELECT DISTINCT OID_DIVISA,
                        OID_DENOMINACION,
                        OID_MEDIO_PAGO,
                        OID_UNIDAD_MEDIDA,
                        OID_CALIDAD
          FROM (SELECT OID_DIVISA,
                       OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDECLARADO_EFECTIVO DE
                    ON q.OID_REMESA = DE.OID_REMESA
                UNION ALL
                SELECT OID_DIVISA,
                       OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDIFERENCIA_EFECTIVO DI
                    ON q.OID_REMESA = DI.OID_REMESA
                UNION ALL
                SELECT D.OID_DIVISA,
                       C.OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       C.OID_UNIDAD_MEDIDA,
                       OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TCONTADO_EFECTIVO C
                    ON q.OID_REMESA = C.OID_REMESA
                 INNER JOIN GEPR_TDENOMINACION D
                    ON D.OID_DENOMINACION = C.OID_DENOMINACION
                UNION ALL
                SELECT OID_DIVISA,
                       NULL           AS OID_DENOMINACION,
                       OID_MEDIO_PAGO,
                       NULL           AS OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDECLARADO_MEDIO_PAGO DMP
                    ON q.OID_REMESA = DMP.OID_REMESA
                UNION ALL
                SELECT OID_DIVISA,
                       NULL           AS OID_DENOMINACION,
                       OID_MEDIO_PAGO,
                       NULL           AS OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDIFERENCIA_MEDIO_PAGO DIMP
                    ON q.OID_REMESA = DIMP.OID_REMESA
                UNION ALL
                SELECT MP.OID_DIVISA,
                       NULL               AS OID_DENOMINACION,
                       CMP.OID_MEDIO_PAGO,
                       NULL               AS OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TCONTADO_MEDIO_PAGO CMP
                    ON q.OID_REMESA = CMP.OID_REMESA
                 INNER JOIN GEPR_TMEDIO_PAGO MP
                    ON MP.OID_MEDIO_PAGO = CMP.OID_MEDIO_PAGO))
      SELECT DISTINCT OID_DIVISA, 'DIVISA'
        FROM q1
       WHERE OID_DIVISA IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_DENOMINACION, 'DENOMINAC'
        FROM q1
       WHERE OID_DENOMINACION IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_MEDIO_PAGO, 'MEDIO_PAGO'
        FROM q1
       WHERE OID_MEDIO_PAGO IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_UNIDAD_MEDIDA, 'U_MEDIDA'
        FROM q1
       WHERE OID_UNIDAD_MEDIDA IS NOT NULL
       UNION ALL
      SELECT DISTINCT OID_CALIDAD, 'CALIDAD'
        FROM q1
       WHERE OID_CALIDAD IS NOT NULL;
    par$inserts := 0 + sql%ROWCOUNT;

  END scargar_tt_oids_div_remesas;

  PROCEDURE scargar_tt_oids_div_bultos(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
       (SELECT OID OID_BULTO
          FROM SAPR_GTT_TOIDS
         WHERE COD_CALIFICADOR = 'BULTO'),
      q1 AS
       (SELECT DISTINCT OID_DIVISA,
                        OID_DENOMINACION,
                        OID_MEDIO_PAGO,
                        OID_UNIDAD_MEDIDA,
                        OID_CALIDAD
          FROM (SELECT OID_DIVISA,
                       OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDECLARADO_EFECTIVO DE
                    ON q.OID_BULTO = DE.OID_BULTO
                UNION ALL
                SELECT OID_DIVISA,
                       OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDIFERENCIA_EFECTIVO DI
                    ON q.OID_BULTO = DI.OID_BULTO
                UNION ALL
                SELECT D.OID_DIVISA,
                       C.OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       C.OID_UNIDAD_MEDIDA,
                       C.OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TCONTADO_EFECTIVO C
                    ON q.OID_BULTO = C.OID_BULTO
                 INNER JOIN GEPR_TDENOMINACION D
                    ON D.OID_DENOMINACION = C.OID_DENOMINACION
                UNION ALL
                SELECT OID_DIVISA,
                       NULL           AS OID_DENOMINACION,
                       OID_MEDIO_PAGO,
                       NULL           AS OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDECLARADO_MEDIO_PAGO DMP
                    ON q.OID_BULTO = DMP.OID_BULTO
                UNION ALL
                SELECT OID_DIVISA,
                       NULL           AS OID_DENOMINACION,
                       OID_MEDIO_PAGO,
                       NULL           AS OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD                       
                  FROM q
                 INNER JOIN SAPR_TDIFERENCIA_MEDIO_PAGO DIMP
                    ON q.OID_BULTO = DIMP.OID_BULTO
                UNION ALL
                SELECT MP.OID_DIVISA,
                       NULL               AS OID_DENOMINACION,
                       CMP.OID_MEDIO_PAGO,
                       NULL               AS OID_UNIDAD_MEDIDA,
                       NULL AS OID_CALIDAD                       
                  FROM q
                 INNER JOIN SAPR_TCONTADO_MEDIO_PAGO CMP
                    ON q.OID_BULTO = CMP.OID_BULTO
                 INNER JOIN GEPR_TMEDIO_PAGO MP
                    ON MP.OID_MEDIO_PAGO = CMP.OID_MEDIO_PAGO))
      SELECT DISTINCT OID_DIVISA, 'DIVISA'
        FROM q1
       WHERE OID_DIVISA IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_DENOMINACION, 'DENOMINAC'
        FROM q1
       WHERE OID_DENOMINACION IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_MEDIO_PAGO, 'MEDIO_PAGO'
        FROM q1
       WHERE OID_MEDIO_PAGO IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_UNIDAD_MEDIDA, 'U_MEDIDA'
        FROM q1
       WHERE OID_UNIDAD_MEDIDA IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_CALIDAD, 'CALIDAD'
        FROM q1
       WHERE OID_CALIDAD IS NOT NULL;

    par$inserts := 0 + sql%ROWCOUNT;

  END scargar_tt_oids_div_bultos;

  PROCEDURE scargar_tt_oids_div_parciales(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN

    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
       (SELECT OID OID_PARCIAL
          FROM SAPR_GTT_TOIDS
         WHERE COD_CALIFICADOR = 'PARCIAL'),
      q1 AS
       (SELECT DISTINCT OID_DIVISA,
                        OID_DENOMINACION,
                        OID_MEDIO_PAGO,
                        OID_UNIDAD_MEDIDA,
            OID_CALIDAD
          FROM (SELECT OID_DIVISA,
                       OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       OID_UNIDAD_MEDIDA,
             NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDECLARADO_EFECTIVO DE
                    ON q.OID_PARCIAL = DE.OID_PARCIAL
                UNION ALL
                SELECT OID_DIVISA,
                       OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       OID_UNIDAD_MEDIDA,
             NULL AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDIFERENCIA_EFECTIVO DI
                    ON q.OID_PARCIAL = DI.OID_PARCIAL
                UNION ALL
                SELECT D.OID_DIVISA,
                       C.OID_DENOMINACION,
                       NULL AS OID_MEDIO_PAGO,
                       C.OID_UNIDAD_MEDIDA,
             C.OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TCONTADO_EFECTIVO C
                    ON q.OID_PARCIAL = C.OID_PARCIAL
                 INNER JOIN GEPR_TDENOMINACION D
                    ON D.OID_DENOMINACION = C.OID_DENOMINACION
                UNION ALL
                SELECT OID_DIVISA,
                       NULL           AS OID_DENOMINACION,
                       OID_MEDIO_PAGO,
                       NULL           AS OID_UNIDAD_MEDIDA,
             NULL           AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDECLARADO_MEDIO_PAGO DMP
                    ON q.OID_PARCIAL = DMP.OID_PARCIAL
                UNION ALL
                SELECT OID_DIVISA,
                       NULL           AS OID_DENOMINACION,
                       OID_MEDIO_PAGO,
                       NULL           AS OID_UNIDAD_MEDIDA,
             NULL           AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TDIFERENCIA_MEDIO_PAGO DIMP
                    ON q.OID_PARCIAL = DIMP.OID_PARCIAL
                UNION ALL
                SELECT MP.OID_DIVISA,
                       NULL               AS OID_DENOMINACION,
                       CMP.OID_MEDIO_PAGO,
                       NULL               AS OID_UNIDAD_MEDIDA,
             NULL           AS OID_CALIDAD
                  FROM q
                 INNER JOIN SAPR_TCONTADO_MEDIO_PAGO CMP
                    ON q.OID_PARCIAL = CMP.OID_PARCIAL
                 INNER JOIN GEPR_TMEDIO_PAGO MP
                    ON MP.OID_MEDIO_PAGO = CMP.OID_MEDIO_PAGO))
      SELECT DISTINCT OID_DIVISA, 'DIVISA'
        FROM q1
       WHERE OID_DIVISA IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_DENOMINACION, 'DENOMINAC'
        FROM q1
       WHERE OID_DENOMINACION IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_MEDIO_PAGO, 'MEDIO_PAGO'
        FROM q1
       WHERE OID_MEDIO_PAGO IS NOT NULL
      UNION ALL
      SELECT DISTINCT OID_UNIDAD_MEDIDA, 'U_MEDIDA'
        FROM q1
       WHERE OID_UNIDAD_MEDIDA IS NOT NULL
    UNION ALL
      SELECT DISTINCT OID_CALIDAD, 'CALIDAD'
        FROM q1
      WHERE OID_CALIDAD IS NOT NULL;

    par$inserts := 0 + sql%ROWCOUNT;

  END scargar_tt_oids_div_parciales;

  PROCEDURE scargar_divisas(par$rc_divisas              out SYS_REFCURSOR,
                            par$rc_denominaciones       out SYS_REFCURSOR,
                            par$rc_mediospago           out SYS_REFCURSOR,
                            par$rc_unidades_medida      out SYS_REFCURSOR,
                            par$rc_calidades            out SYS_REFCURSOR,
                            par$rc_terminos_medios_pago OUT SYS_REFCURSOR,
                            par$selects                 OUT gepr_pcomon_###VERSION###.tipo$nel_)

   IS
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;

  BEGIN

    /* - TABLA: Divisas */
    par$rc_Divisas := fcargar_rc_divisa(par$selects => var$selects_int);
    par$selects    := 0 + var$selects_int;

    /* - TABLA: Denominaciones */
    par$rc_Denominaciones := fcargar_rc_denominacion(par$selects => var$selects_int);
    par$selects           := par$selects + var$selects_int;

    /* - TABLA: Medio Pagos */
    par$rc_MediosPago := fcargar_rc_mediopago(par$selects => var$selects_int);
    par$selects       := par$selects + var$selects_int;

    /* - TABLA: Unidades de medida */
    par$rc_Unidades_Medida := fcargar_rc_unidadmedida(par$selects => var$selects_int);
    par$selects            := par$selects + var$selects_int;

    /* - TABLA: Calidades */
    par$rc_Calidades := fcargar_rc_calidades(par$selects => var$selects_int);
    par$selects      := par$selects + var$selects_int;

    /* - TABLA: Terminos de medios de pago */
    par$rc_Terminos_Medios_Pago := fcargar_rc_terminos_mp(par$selects => var$selects_int);
    par$selects                 := par$selects + var$selects_int;

  END scargar_divisas;

  FUNCTION fcargar_rc_divisa(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_divisas SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_divisas FOR
      SELECT d.oid_divisa,
             d.cod_iso_divisa,
             d.des_divisa,
             d.cod_simbolo,
             d.bol_vigente,
             d.cod_usuario,
             d.fyh_actualizacion,
             d.cod_acceso,
             d.cod_color
        FROM sapr_gtt_toids tmp
       INNER JOIN gepr_tdivisa d
          ON tmp.oid = d.oid_divisa
       WHERE tmp.cod_calificador = 'DIVISA';

    RETURN var$rc_divisas;
    par$selects := sql%ROWCOUNT;

  END fcargar_rc_divisa;

  FUNCTION fcargar_rc_denominacion(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_denominaciones SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_denominaciones FOR
      SELECT d.cod_denominacion,
             d.cod_usuario,
             d.des_denominacion,
             d.bol_billete,
             d.bol_vigente,
             d.fyh_actualizacion,
             d.oid_denominacion,
             d.num_valor,
             d.oid_divisa,
             div.cod_iso_divisa
        FROM gepr_tdenominacion d
       INNER JOIN gepr_tdivisa div
          ON div.oid_divisa = d.oid_divisa
       WHERE d.oid_divisa IN
             (SELECT OID oid_divisa
                FROM sapr_gtt_toids
               WHERE cod_calificador = 'DIVISA')
         AND d.oid_denominacion IN
             (SELECT OID oid_denominacion
                FROM sapr_gtt_toids
               WHERE cod_calificador = 'DENOMINAC');
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_denominaciones;

  END fcargar_rc_denominacion;

  FUNCTION fcargar_rc_mediopago(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_medios_pago SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_medios_pago FOR
      SELECT div.cod_iso_divisa,
             mp.oid_divisa,
             mp.oid_medio_pago,
             mp.cod_medio_pago,
             mp.des_medio_pago,
             mp.obs_medio_pago,
             mp.bol_vigente,
             mp.cod_usuario,
             mp.fyh_actualizacion,
             mp.cod_tipo_medio_pago,
             t.oid_termino                 AS t_oid_termino,
             t.cod_termino                 AS t_cod_termino,
             t.des_termino                 AS t_des_termino,
             t.obs_termino                 AS t_obs_termino,
             t.des_valor_inicial           AS t_des_valor_inicial,
             t.nec_longitud                AS t_nec_longitud,
             t.oid_formato                 AS t_oid_formato,
             t.oid_mascara                 AS t_oid_mascara,
             t.oid_algoritmo_validacion    AS t_oid_algoritmo_validacion,
             t.bol_mostrar_codigo          AS t_bol_mostrar_codigo,
             t.nec_orden                   AS t_nec_orden,
             t.bol_vigente                 AS t_bol_vigente,
             t.cod_usuario                 AS t_cod_usuario,
             t.fyh_actualizacion           AS t_fyh_actualizacion,
             form.oid_formato,
             form.cod_formato,
             form.des_formato,
             av.oid_algoritmo_validacion,
             av.cod_algoritmo_validacion,
             av.des_algoritmo_validacion,
             av.obs_algoritmo_validacion,
             av.bol_aplica_term_medio_pago AS av_bol_aplica_term_medio_pago,
             av.bol_aplica_term_iac        AS av_bol_aplica_term_iac,
             m.oid_mascara,
             m.cod_mascara,
             m.des_mascara,
             m.des_exp_regular,
             m.bol_aplica_term_medio_pago  AS m_bol_aplica_term_medio_pago,
             m.bol_aplica_term_iac         AS m_bol_aplica_term_iac,
             vt.oid_valor,
             vt.cod_valor,
             vt.des_valor,
             vt.bol_vigente                AS vt_bol_vigente
        FROM gepr_tmedio_pago mp
       INNER JOIN gepr_tdivisa div
          ON div.oid_divisa = mp.oid_divisa
        LEFT OUTER JOIN gepr_ttermino_medio_pago t
          ON mp.oid_medio_pago = t.oid_medio_pago
        LEFT OUTER JOIN gepr_tformato form
          ON form.oid_formato = t.oid_formato
        LEFT OUTER JOIN gepr_talgoritmo_validacion av
          ON av.oid_algoritmo_validacion = t.oid_algoritmo_validacion
        LEFT OUTER JOIN gepr_tmascara m
          ON m.oid_mascara = t.oid_mascara
        LEFT OUTER JOIN gepr_tvalor_termino_medio_pago vt
          ON vt.oid_termino = t.oid_termino
       WHERE mp.oid_divisa IN
             (SELECT OID oid_divisa
                FROM sapr_gtt_toids
               WHERE cod_calificador = 'DIVISA')
         AND mp.oid_medio_pago IN
             (SELECT OID oid_medio_pago
                FROM sapr_gtt_toids
               WHERE cod_calificador = 'MEDIO_PAGO');
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_medios_pago;

  END fcargar_rc_mediopago;

  FUNCTION fcargar_rc_unidadmedida(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_unidades_medida SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_unidades_medida FOR
      SELECT um.oid_unidad_medida,
             um.cod_unidad_medida,
             um.des_unidad_medida,
             um.cod_tipo_unidad_medida,
             um.num_valor_unidad,
             um.bol_defecto
        FROM sapr_gtt_toids tmp
       INNER JOIN gepr_tunidad_medida um
          ON tmp.oid = um.oid_unidad_medida
       WHERE tmp.cod_calificador = 'U_MEDIDA'
      UNION
      SELECT um.oid_unidad_medida,
             um.cod_unidad_medida,
             um.des_unidad_medida,
             um.cod_tipo_unidad_medida,
             um.num_valor_unidad,
             um.bol_defecto
        FROM gepr_tunidad_medida um
       WHERE bol_defecto = 1;
    par$selects := sql%ROWCOUNT;
    RETURN var$rc_unidades_medida;

  END fcargar_rc_unidadmedida;

  FUNCTION fcargar_rc_calidades(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_calidades SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_calidades FOR
      SELECT c.oid_calidad, c.cod_calidad, c.des_calidad
        FROM sapr_gtt_toids tmp
       INNER JOIN gepr_tcalidad c
          ON tmp.oid = c.oid_calidad
       WHERE tmp.cod_calificador = 'CALIDAD'
       ORDER BY des_calidad;
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_calidades;

  END fcargar_rc_calidades;

  FUNCTION fcargar_rc_terminos_mp(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_terminos_mediospago SYS_REFCURSOR;
  BEGIN

    OPEN var$rc_terminos_mediospago FOR
      SELECT pd.oid_documento,
             oid_medio_pago,
             tmp.des_valor,
             tmp.nec_indice_grupo
        FROM sapr_tvalor_termino_medio_pago tmp
       INNER JOIN sapr_tmedio_pagoxdocumento pd
          ON tmp.oid_medio_pagoxdocumento = pd.oid_medio_pagoxdocumento
       WHERE pd.oid_documento IN
             (SELECT OID oid_documento
                FROM sapr_gtt_toids
               WHERE cod_calificador = 'DOCUMENTO'
                  OR cod_calificador = 'DOC_PADRE')
       ORDER BY pd.oid_documento, tmp.nec_indice_grupo;
    par$selects := sql%ROWCOUNT;

    RETURN var$rc_terminos_mediospago;

  END fcargar_rc_terminos_mp;

END sapr_pdivisa_###VERSION###;
/
