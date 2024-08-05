CREATE OR REPLACE PACKAGE gepr_putilidades_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  /* Realiza la conversión de formato de un texto a un número */
  FUNCTION ftexto_a_numero(par$texto gepr_pcomon_###VERSION###.tipo$desc_) RETURN gepr_pcomon_###VERSION###.tipo$nel_;

  /* devuelve la fecha y hora gmt +0 */
  FUNCTION fgmt_zero RETURN VARCHAR2;

  /* devuelve verdadero o falso si un numero es mayor que cero */
  FUNCTION fmayor_que_cero(par$cant gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_;

  /*devuelve la diferencia entre dos timestamps en segundos, con millonenismas de segundo como decimales
  si el primer parametro es posterior al segundo, el resultado es positivo,
  si es menor, el resultado es negativo*/
  FUNCTION fdif_timestamps_segundos(par$fin    gepr_pcomon_###VERSION###.tipo$gmt_,
                                    par$inicio gepr_pcomon_###VERSION###.tipo$gmt_)
    RETURN NUMBER;

  /* devuelve el stacklog del oracle */
  FUNCTION fformat_error RETURN VARCHAR2;

  /* verifica identificadores duplicados */
  FUNCTION farray_sin_duplicados_oids(par$array IN gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN gepr_pcomon_###VERSION###.tipo$oids_;

  /* verifica se el objecto esta vacio */
  FUNCTION farray_contiene_valor_vacio(par$array IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       valor     IN VARCHAR2) RETURN BOOLEAN;

  /* traducion */
  FUNCTION ftraduzir(par$codcultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                     par$codchave          IN gepr_pcomon_###VERSION###.tipo$desc_,
                     par$codfuncionalidad  IN gepr_pcomon_###VERSION###.tipo$desc_,
                     par$codaplicacion     IN gepr_pcomon_###VERSION###.tipo$cod_,
                     par$colparametros     IN gepr_pcomon_###VERSION###.tipo$obs_,
                     par$bolmarcarinitexto IN gepr_pcomon_###VERSION###.tipo$nbol_ DEFAULT 0)
    RETURN VARCHAR2;

  /* log de ejecuci?n de comandos internos */
  PROCEDURE sins_tlog_ejecucion_trn_in(par$des_comando             IN sapr_tlog_ejecucion.des_comando % TYPE,
                                       par$num_inserts             IN sapr_tlog_ejecucion.num_inserts % TYPE,
                                       par$num_updates             IN sapr_tlog_ejecucion.num_updates % TYPE,
                                       par$num_deletes             IN sapr_tlog_ejecucion.num_deletes % TYPE,
                                       par$num_merges              IN sapr_tlog_ejecucion.num_merges % TYPE,
                                       par$num_selects             IN sapr_tlog_ejecucion.num_selects % TYPE,
                                       par$num_total_reg_afectados IN sapr_tlog_ejecucion.num_total_registros_afectados % TYPE,
                                       par$fyh_inicio              IN gepr_pcomon_###VERSION###.tipo$gmt_,
                                       par$fyh_fin                 IN gepr_pcomon_###VERSION###.tipo$gmt_,
                                       par$cod_resultado           IN sapr_tlog_ejecucion.cod_resultado % TYPE,
                                       par$obs_info                IN sapr_tlog_ejecucion.obs_info % TYPE,
                                       par$obs_error               IN sapr_tlog_ejecucion.obs_error % TYPE DEFAULT NULL,
                                       par$des_usuario             IN sapr_tlog_ejecucion.des_usuario % TYPE,
                                       par$cod_ejecucion           OUT sapr_tlog_ejecucion.cod_ejecucion % TYPE);

  PROCEDURE supd_tlog_ejecucion_trn_ex(par$cod_ejecucion              IN sapr_tlog_ejecucion.cod_ejecucion % TYPE,
                                       par$cod_transaccion            IN sapr_tlog_ejecucion.cod_transaccion % TYPE,
                                       par$num_duracion_trans_ext_seg IN sapr_tlog_ejecucion.num_duracion_transac_ext_seg % TYPE,
                                       par$cod_resultado              IN sapr_tlog_ejecucion.cod_resultado % TYPE,
                                       par$error                      IN sapr_tlog_ejecucion.obs_error % TYPE DEFAULT NULL);

  FUNCTION farray_vacio_oid(par$array in gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN BOOLEAN;

  FUNCTION farray_vacio_cod(par$array in gepr_pcomon_###VERSION###.tipo$cods_)
    RETURN BOOLEAN;

  FUNCTION farray_vacio_nel(par$array in gepr_pcomon_###VERSION###.tipo$nels_)
    RETURN BOOLEAN;

  FUNCTION ffiltrar_array_por_oid_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$oids_;

  FUNCTION ffiltrar_array_por_cod_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$cods_;

  FUNCTION ffiltrar_array_por_imp_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$imps_;

  FUNCTION ffiltrar_array_por_nel_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$nels_;

  FUNCTION ffiltrar_array_por_des_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$descs_;

  FUNCTION ffiltrar_array_por_int_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$ints_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$ints_;

  FUNCTION frecuperar_parametro(par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_pais       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_parametro  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_aplicacion IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$desc_;

  FUNCTION fconverter_array_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN VARCHAR2_ARRAY;

  FUNCTION fconverter_array_cod_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$cods_)
    RETURN VARCHAR2_ARRAY;

  FUNCTION fconverter_array_nel_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$nels_)
    RETURN VARCHAR2_ARRAY;

  FUNCTION fconverter_array_nbol_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$nbols_)
    RETURN VARCHAR2_ARRAY;

  FUNCTION fconverter_array_desc_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$descs_)
    RETURN VARCHAR2_ARRAY;

  FUNCTION fdistinct_cods(par$array_Items IN gepr_pcomon_###VERSION###.tipo$cods_)
    RETURN gepr_pcomon_###VERSION###.tipo$cods_;

  FUNCTION fdistinct_oids(par$array_Items IN gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN gepr_pcomon_###VERSION###.tipo$oids_;

  FUNCTION fhay_item_coleccion_oids(par$array_Items IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$oid_item    IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  FUNCTION fhay_item_coleccion_cods(par$array_Items IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$cod_item    IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_;

  FUNCTION farray_vacio_desc(par$arr in gepr_pcomon_###VERSION###.tipo$descs_)
    RETURN BOOLEAN;

  FUNCTION farray_vacio_obs(par$arr in gepr_pcomon_###VERSION###.tipo$obss_) RETURN BOOLEAN;

  FUNCTION fsplit(par$origem IN VARCHAR2, par$separador IN VARCHAR2)
    RETURN SYS_REFCURSOR;

  FUNCTION fobtenerItemEntidadPadreCods(par$colItemsPadre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$colItemsHijo  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ItemCorrente  IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$cods_;

  FUNCTION fObtenerItemEntPadreDescs(par$colItemsPadre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$colItemsHijo  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ItemCorrente  IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$descs_;

  FUNCTION fobtener_primer_registro_oid(par$array_oids IN gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

  FUNCTION fobtener_primer_registro_cod(par$array_cods IN gepr_pcomon_###VERSION###.tipo$cods_)
    RETURN gepr_pcomon_###VERSION###.tipo$cod_;

  PROCEDURE sinicializar_sesion;

  /* srecuperar_fechahora_gmt */

  PROCEDURE srecuperar_fechahora_gmt(par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$fyh_fecha      OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                     par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* Calcular la fecha GMT */
  PROCEDURE scalcular_gmt(par$fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$oid_delegacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$nel_gmt_minuto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_);

  /* fsomar_fecha */
  FUNCTION fsomar_fecha(par$fyh_fecha      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$nel_gmt_minuto IN gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN gepr_pcomon_###VERSION###.tipo$fyh_;

  /* Calcular la fecha GMT */
  FUNCTION scalcular_fyh_gmt(par$fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                              par$oid_delegacion IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$fyh_;
                              
END gepr_putilidades_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY gepr_putilidades_###VERSION### AS

  FUNCTION ftexto_a_numero(par$texto gepr_pcomon_###VERSION###.tipo$desc_)
    RETURN gepr_pcomon_###VERSION###.tipo$nel_ IS
    var$res gepr_pcomon_###VERSION###.tipo$nel_;
  BEGIN
    /* Validamos que el parametro sea un valor numérico */
    IF par$texto IS NULL THEN
       var$res := 0;
    ELSE
      --Es númerico
      IF (LENGTH(TRIM(TRANSLATE(par$texto, '0123456789', ' '))) IS NULL) THEN
         var$res := TO_NUMBER(par$texto);
      ELSE
         var$res := 0;
      END IF;
    END IF;

    RETURN var$res;
  END ftexto_a_numero;
  
  FUNCTION fgmt_zero RETURN VARCHAR2 IS
  BEGIN
    RETURN(sys_extract_utc(current_timestamp) || ' +00:00');
  END fgmt_zero;

  FUNCTION fmayor_que_cero(par$cant gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN gepr_pcomon_###VERSION###.tipo$bol_ IS
    var$res gepr_pcomon_###VERSION###.tipo$bol_;
  BEGIN
    IF NVL(par$cant, 0) > 0 THEN
      var$res := true;
    ELSE
      var$res := false;
    END IF;
    RETURN var$res;
  END fmayor_que_cero;

  FUNCTION fdif_timestamps_segundos(par$fin    gepr_pcomon_###VERSION###.tipo$gmt_,
                                    par$inicio gepr_pcomon_###VERSION###.tipo$gmt_)
    RETURN NUMBER IS
    var$res NUMBER(38, 6);
  BEGIN
    SELECT EXTRACT(day from diff) * 24 * 60 * 60 +
           EXTRACT(hour from diff) * 60 * 60 +
           EXTRACT(minute from diff) * 60 + EXTRACT(second from diff)
      INTO var$res
      FROM (SELECT par$fin - par$inicio diff FROM dual);
    RETURN var$res;
  END fdif_timestamps_segundos;

  PROCEDURE sins_tlog_ejecucion_trn_in(par$des_comando             IN sapr_tlog_ejecucion.des_comando % TYPE,
                                       par$num_inserts             IN sapr_tlog_ejecucion.num_inserts % TYPE,
                                       par$num_updates             IN sapr_tlog_ejecucion.num_updates % TYPE,
                                       par$num_deletes             IN sapr_tlog_ejecucion.num_deletes % TYPE,
                                       par$num_merges              IN sapr_tlog_ejecucion.num_merges % TYPE,
                                       par$num_selects             IN sapr_tlog_ejecucion.num_selects % TYPE,
                                       par$num_total_reg_afectados IN sapr_tlog_ejecucion.num_total_registros_afectados % TYPE,
                                       par$fyh_inicio              IN gepr_pcomon_###VERSION###.tipo$gmt_,
                                       par$fyh_fin                 IN gepr_pcomon_###VERSION###.tipo$gmt_,
                                       par$cod_resultado           IN sapr_tlog_ejecucion.cod_resultado % TYPE,
                                       par$obs_info                IN sapr_tlog_ejecucion.obs_info % TYPE,
                                       par$obs_error               IN sapr_tlog_ejecucion.obs_error % TYPE DEFAULT NULL,
                                       par$des_usuario             IN sapr_tlog_ejecucion.des_usuario % TYPE,
                                       par$cod_ejecucion           OUT sapr_tlog_ejecucion.cod_ejecucion % TYPE) IS
    PRAGMA AUTONOMOUS_TRANSACTION;
    var$num_duracion_seg        sapr_tlog_ejecucion.num_duracion_seg % TYPE;
    var$num_total_reg_afectados sapr_tlog_ejecucion.num_total_registros_afectados % TYPE;
    var$cod_transaccion         sapr_tlog_ejecucion.cod_transaccion % TYPE;
    var$fyh_fin                 sapr_tlog_ejecucion.fyh_fin % TYPE;
  BEGIN
    par$cod_ejecucion           := sapr_tlog_ejecucion_seq.nextval;
    var$cod_transaccion         := 'I';
    var$fyh_fin                 := NVL(par$fyh_fin, systimestamp);
    var$num_duracion_seg        := fdif_timestamps_segundos(var$fyh_fin,
                                                            par$fyh_inicio);
    var$num_total_reg_afectados := par$num_total_reg_afectados;
    IF var$num_total_reg_afectados IS NULL THEN
      var$num_total_reg_afectados := NVL(par$num_inserts, 0) +
                                     NVL(par$num_updates, 0) +
                                     NVL(par$num_deletes, 0) +
                                     NVL(par$num_merges, 0) +
                                     NVL(par$num_selects, 0);
    END IF;
    INSERT INTO sapr_tlog_ejecucion
      (cod_ejecucion,
       des_comando,
       num_inserts,
       num_updates,
       num_deletes,
       num_merges,
       num_selects,
       num_total_registros_afectados,
       fyh_inicio,
       fyh_fin,
       num_duracion_seg,
       cod_transaccion,
       num_duracion_transac_ext_seg,
       cod_resultado,
       obs_info,
       obs_error,
       des_usuario)
    VALUES
      (par$cod_ejecucion,
       par$des_comando,
       NVL(par$num_inserts, 0),
       NVL(par$num_updates, 0),
       NVL(par$num_deletes, 0),
       NVL(par$num_merges, 0),
       NVL(par$num_selects, 0),
       var$num_total_reg_afectados,
       par$fyh_inicio,
       var$fyh_fin,
       var$num_duracion_seg,
       var$cod_transaccion,
       0,
       par$cod_resultado,
       par$obs_info,
       par$obs_error,
       par$des_usuario);

    COMMIT;
    /*
        exception
            when others then rollback;
    */
    if par$cod_resultado = 'R' then

      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              '***' || 'CODIGO_EJECUCION:' ||
                              par$cod_ejecucion || '***' || 'Error: ' ||
                              par$des_comando,
                              true);
    end if;

  END sins_tlog_ejecucion_trn_in;

  PROCEDURE supd_tlog_ejecucion_trn_ex(par$cod_ejecucion              IN sapr_tlog_ejecucion.cod_ejecucion % TYPE,
                                       par$cod_transaccion            IN sapr_tlog_ejecucion.cod_transaccion % TYPE,
                                       par$num_duracion_trans_ext_seg IN sapr_tlog_ejecucion.num_duracion_transac_ext_seg % TYPE,
                                       par$cod_resultado              IN sapr_tlog_ejecucion.cod_resultado % TYPE,
                                       par$error                      IN sapr_tlog_ejecucion.obs_error % TYPE DEFAULT NULL) IS
    PRAGMA AUTONOMOUS_TRANSACTION;
  BEGIN
    UPDATE sapr_tlog_ejecucion
       SET num_duracion_transac_ext_seg = par$num_duracion_trans_ext_seg,
           cod_transaccion              = par$cod_transaccion,
           cod_resultado                = par$cod_resultado,
           obs_error                    = par$error
     WHERE cod_ejecucion = par$cod_ejecucion;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
  END supd_tlog_ejecucion_trn_ex;

  FUNCTION fformat_error RETURN VARCHAR2 IS
    var$err VARCHAR2(4000);
  BEGIN
    var$err := dbms_utility.format_error_stack;
    var$err := var$err || dbms_utility.format_error_backtrace;
    RETURN var$err;
  END fformat_error;

  FUNCTION ftraduzir(par$codcultura        IN gepr_pcomon_###VERSION###.tipo$cod_,
                     par$codchave          IN gepr_pcomon_###VERSION###.tipo$desc_,
                     par$codfuncionalidad  IN gepr_pcomon_###VERSION###.tipo$desc_,
                     par$codaplicacion     IN gepr_pcomon_###VERSION###.tipo$cod_,
                     par$colparametros     IN gepr_pcomon_###VERSION###.tipo$obs_,
                     par$bolmarcarinitexto IN gepr_pcomon_###VERSION###.tipo$nbol_ DEFAULT 0)
    RETURN VARCHAR2 IS

    CURSOR cursor_parametros IS
      SELECT column_value cod_parametro
        FROM TABLE(string_to_array_###VERSION### (par$colparametros, '|'));

    v_valor_chave       VARCHAR2(4000);
    v_existe            NUMBER;
    v_index_parametro   NUMBER;
    v_simbolo_parametro VARCHAR2(100);

  BEGIN

    SELECT COUNT(*)
      INTO v_existe
      FROM (SELECT d.valor
              FROM gepr_tdiccionario d
             INNER JOIN gepr_taplicacion a
                ON a.oid_aplicacion = d.oid_aplicacion
             WHERE a.cod_aplicacion = par$codaplicacion
               AND d.cod_idioma = par$codcultura
               AND d.cod_funcionalidad = par$codfuncionalidad
               AND d.cod_expresion = par$codchave
            UNION
            SELECT d.valor
              FROM gepr_tdiccionario d
             INNER JOIN gepr_taplicacion a
                ON a.oid_aplicacion = d.oid_aplicacion
             WHERE a.cod_aplicacion = par$codaplicacion
               AND d.cod_idioma <> par$codcultura
               AND d.cod_funcionalidad = par$codfuncionalidad
               AND d.bol_defecto = 1
               AND d.cod_expresion = par$codchave
               AND d.cod_expresion NOT IN
                   (SELECT d.cod_expresion
                      FROM gepr_tdiccionario d
                     INNER JOIN gepr_taplicacion a
                        ON a.oid_aplicacion = d.oid_aplicacion
                     WHERE a.cod_aplicacion = par$codaplicacion
                       AND d.cod_idioma = par$codcultura
                       AND d.cod_expresion = par$codchave
                       AND d.cod_funcionalidad = par$codfuncionalidad));

    IF v_existe > 0 THEN

      SELECT valor
        INTO v_valor_chave
        FROM (SELECT d.valor
                FROM gepr_tdiccionario d
               INNER JOIN gepr_taplicacion a
                  ON a.oid_aplicacion = d.oid_aplicacion
               WHERE a.cod_aplicacion = par$codaplicacion
                 AND d.cod_idioma = par$codcultura
                 AND d.cod_funcionalidad = par$codfuncionalidad
                 AND d.cod_expresion = par$codchave
              UNION
              SELECT d.valor
                FROM gepr_tdiccionario d
               INNER JOIN gepr_taplicacion a
                  ON a.oid_aplicacion = d.oid_aplicacion
               WHERE a.cod_aplicacion = par$codaplicacion
                 AND d.cod_idioma <> par$codcultura
                 AND d.cod_funcionalidad = par$codfuncionalidad
                 AND d.bol_defecto = 1
                 AND d.cod_expresion = par$codchave
                 AND d.cod_expresion NOT IN
                     (SELECT d.cod_expresion
                        FROM gepr_tdiccionario d
                       INNER JOIN gepr_taplicacion a
                          ON a.oid_aplicacion = d.oid_aplicacion
                       WHERE a.cod_aplicacion = par$codaplicacion
                         AND d.cod_idioma = par$codcultura
                         AND d.cod_expresion = par$codchave
                         AND d.cod_funcionalidad = par$codfuncionalidad));

      IF par$colparametros IS NOT NULL THEN

        v_index_parametro   := 0;
        v_simbolo_parametro := '';

        FOR cp IN cursor_parametros LOOP

          v_simbolo_parametro := '{' || v_index_parametro || '}';
          v_valor_chave       := REPLACE(v_valor_chave,
                                         v_simbolo_parametro,
                                         cp.cod_parametro);
          v_index_parametro   := v_index_parametro + 1;
        END LOOP;

      END IF;

    END IF;

    if v_valor_chave is null then
      v_valor_chave := par$codchave;
    end if;

    IF par$bolmarcarinitexto = 1 AND v_valor_chave IS NOT NULL THEN

      v_valor_chave := '#' || v_valor_chave || '#';

    END IF;

    RETURN v_valor_chave;

  END ftraduzir;

  FUNCTION farray_contiene_valor_vacio(par$array IN gepr_pcomon_###VERSION###.tipo$oids_,
                                       valor     IN VARCHAR2) RETURN BOOLEAN IS
    var$contiene BOOLEAN := false;
  BEGIN
    FOR idx IN par$array.first .. par$array.last LOOP
      IF par$array(idx) = valor THEN
        var$contiene := true;
        EXIT;
      END IF;
    END LOOP;
    RETURN var$contiene;
  END farray_contiene_valor_vacio;

  FUNCTION farray_sin_duplicados_oids(par$array IN gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN gepr_pcomon_###VERSION###.tipo$oids_ IS
    var$array2 gepr_pcomon_###VERSION###.tipo$oids_;
    i          INTEGER := 1;
  BEGIN
    FOR idx IN par$array.first .. par$array.last LOOP
      IF NOT farray_contiene_valor_vacio(var$array2, par$array(idx)) THEN
        var$array2(i) := par$array(idx);
        i := i + 1;
      END IF;
    END LOOP;
    RETURN var$array2;
  END farray_sin_duplicados_oids;

  FUNCTION farray_vacio_oid(par$array in gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN BOOLEAN IS
    var$res         BOOLEAN;
    var$cant_existe INTEGER;
  BEGIN
    /*asume que si tiene un solo elemento, no debe ser nulo
    --      es por la imposibilidad de odac de pasar un array realmente vacio como paramentro
    --      que obliga a pasar un array con un elemento nulo */
    var$cant_existe := NVL(par$array.count, -1);
    IF var$cant_existe = 1 THEN
      IF par$array(par$array.first) IS NULL THEN
        var$cant_existe := 0;
      END IF;
    END IF;
    IF var$cant_existe < 1 THEN
      var$res := true;
    ELSE
      var$res := false;
    END IF;
    RETURN var$res;
  END farray_vacio_oid;

  FUNCTION farray_vacio_cod(par$array in gepr_pcomon_###VERSION###.tipo$cods_)
    RETURN BOOLEAN IS
    var$res         BOOLEAN;
    var$cant_existe INTEGER;
  BEGIN
    /*asume que si tiene un solo elemento, no debe ser nulo
    --      es por la imposibilidad de odac de pasar un array realmente vacio como paramentro
    --      que obliga a pasar un array con un elemento nulo */
    var$cant_existe := NVL(par$array.count, -1);
    IF var$cant_existe = 1 THEN
      IF par$array(par$array.first) IS NULL THEN
        var$cant_existe := 0;
      END IF;
    END IF;
    IF var$cant_existe < 1 THEN
      var$res := true;
    ELSE
      var$res := false;
    END IF;
    RETURN var$res;
  END farray_vacio_cod;

  FUNCTION farray_vacio_nel(par$array in gepr_pcomon_###VERSION###.tipo$nels_)
    RETURN BOOLEAN IS
    var$res         BOOLEAN;
    var$cant_existe INTEGER;
  BEGIN
    /*asume que si tiene un solo elemento, no debe ser nulo
    --      es por la imposibilidad de odac de pasar un array realmente vacio como paramentro
    --      que obliga a pasar un array con un elemento nulo */
    var$cant_existe := NVL(par$array.count, -1);
    IF var$cant_existe = 1 THEN
      IF par$array(par$array.first) IS NULL THEN
        var$cant_existe := 0;
      END IF;
    END IF;
    IF var$cant_existe < 1 THEN
      var$res := true;
    ELSE
      var$res := false;
    END IF;
    RETURN var$res;
  END farray_vacio_nel;

  FUNCTION ffiltrar_array_por_oid_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$oids_ IS
    var$result gepr_pcomon_###VERSION###.tipo$oids_;
    var$idx    gepr_pcomon_###VERSION###.tipo$nel_ := 1;
  BEGIN

    IF par$array_oids_padres IS NOT NULL AND par$array_itens IS NOT NULL AND
       par$oid_corrente IS NOT NULL THEN

      FOR idx IN par$array_oids_padres.FIRST .. par$array_oids_padres.LAST LOOP

        IF par$array_oids_padres(idx) = par$oid_corrente THEN
          var$result(var$idx) := par$array_itens(idx);
          var$idx := var$idx + 1;
        END IF;

      END LOOP;

    END IF;

    RETURN var$result;
  END ffiltrar_array_por_oid_padre;

  FUNCTION ffiltrar_array_por_cod_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$cods_ IS
    var$result gepr_pcomon_###VERSION###.tipo$cods_;
    var$idx    gepr_pcomon_###VERSION###.tipo$nel_ := 1;
  BEGIN

    IF par$array_oids_padres IS NOT NULL AND par$array_itens IS NOT NULL AND
       par$oid_corrente IS NOT NULL THEN

      FOR idx IN par$array_oids_padres.FIRST .. par$array_oids_padres.LAST LOOP

        IF par$array_oids_padres(idx) = par$oid_corrente THEN
          var$result(var$idx) := par$array_itens(idx);
          var$idx := var$idx + 1;
        END IF;

      END LOOP;

    END IF;

    RETURN var$result;
  END ffiltrar_array_por_cod_padre;

  FUNCTION ffiltrar_array_por_imp_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$imps_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$imps_ IS
    var$result gepr_pcomon_###VERSION###.tipo$imps_;
    var$idx    gepr_pcomon_###VERSION###.tipo$nel_ := 1;
  BEGIN

    IF par$array_oids_padres IS NOT NULL AND par$array_itens IS NOT NULL AND
       par$oid_corrente IS NOT NULL THEN

      FOR idx IN par$array_oids_padres.FIRST .. par$array_oids_padres.LAST LOOP

        IF par$array_oids_padres(idx) = par$oid_corrente THEN
          var$result(var$idx) := par$array_itens(idx);
          var$idx := var$idx + 1;
        END IF;

      END LOOP;

    END IF;

    RETURN var$result;
  END ffiltrar_array_por_imp_padre;

  FUNCTION ffiltrar_array_por_nel_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$nels_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$nels_ IS
    var$result gepr_pcomon_###VERSION###.tipo$nels_;
    var$idx    gepr_pcomon_###VERSION###.tipo$nel_ := 1;
  BEGIN

    IF par$array_oids_padres IS NOT NULL AND par$array_itens IS NOT NULL AND
       par$oid_corrente IS NOT NULL THEN

      FOR idx IN par$array_oids_padres.FIRST .. par$array_oids_padres.LAST LOOP

        IF par$array_oids_padres(idx) = par$oid_corrente THEN
          var$result(var$idx) := par$array_itens(idx);
          var$idx := var$idx + 1;
        END IF;

      END LOOP;

    END IF;

    RETURN var$result;
  END ffiltrar_array_por_nel_padre;

  FUNCTION ffiltrar_array_por_des_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$descs_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$descs_ IS
    var$result gepr_pcomon_###VERSION###.tipo$descs_;
    var$idx    gepr_pcomon_###VERSION###.tipo$nel_ := 1;
  BEGIN

    IF par$array_oids_padres IS NOT NULL AND par$array_itens IS NOT NULL AND
       par$oid_corrente IS NOT NULL THEN

      FOR idx IN par$array_oids_padres.FIRST .. par$array_oids_padres.LAST LOOP

        IF par$array_oids_padres(idx) = par$oid_corrente THEN
          var$result(var$idx) := par$array_itens(idx);
          var$idx := var$idx + 1;
        END IF;

      END LOOP;

    END IF;

    RETURN var$result;
  END ffiltrar_array_por_des_padre;

  FUNCTION ffiltrar_array_por_int_padre(par$array_oids_padres IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$array_itens       IN gepr_pcomon_###VERSION###.tipo$ints_,
                                        par$oid_corrente      IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$ints_ IS
    var$result gepr_pcomon_###VERSION###.tipo$ints_;
    var$idx    gepr_pcomon_###VERSION###.tipo$nel_ := 1;
  BEGIN

    IF par$array_oids_padres IS NOT NULL AND par$array_itens IS NOT NULL AND
       par$oid_corrente IS NOT NULL THEN

      FOR idx IN par$array_oids_padres.FIRST .. par$array_oids_padres.LAST LOOP

        IF par$array_oids_padres(idx) = par$oid_corrente THEN
          var$result(var$idx) := par$array_itens(idx);
          var$idx := var$idx + 1;
        END IF;

      END LOOP;

    END IF;

    RETURN var$result;
  END ffiltrar_array_por_int_padre;

  FUNCTION frecuperar_parametro(par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_pais       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_parametro  IN gepr_pcomon_###VERSION###.tipo$cod_,
                                par$cod_aplicacion IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$desc_ IS
    var$des_valor_parametro gepr_pcomon_###VERSION###.tipo$desc_;
  BEGIN

    BEGIN

      SELECT DES_VALOR_PARAMETRO
        INTO var$des_valor_parametro
        FROM (SELECT PV.DES_VALOR_PARAMETRO
                FROM GEPR_TPARAMETRO PA
               INNER JOIN GEPR_TPARAMETRO_VALOR PV
                  ON PV.OID_PARAMETRO = PA.OID_PARAMETRO
               INNER JOIN GEPR_TAPLICACION ap
                  ON ap.OID_APLICACION = PA.OID_APLICACION
               INNER JOIN GEPR_TNIVEL_PARAMETRO np
                  ON np.OID_NIVEL_PARAMETRO = PA.OID_NIVEL_PARAMETRO
               INNER JOIN GEPR_TDELEGACION d
                  ON d.OID_DELEGACION = PV.COD_IDENTIFICADOR_NIVEL
               WHERE PA.COD_PARAMETRO = par$cod_parametro
                 AND ap.COD_APLICACION = par$cod_aplicacion
                 AND np.COD_NIVEL_PARAMETRO = '2'
                 AND d.COD_DELEGACION = par$cod_delegacion
              UNION
              SELECT PV.DES_VALOR_PARAMETRO
                FROM GEPR_TPARAMETRO PA
               INNER JOIN GEPR_TPARAMETRO_VALOR PV
                  ON PV.OID_PARAMETRO = PA.OID_PARAMETRO
               INNER JOIN GEPR_TAPLICACION ap
                  ON ap.OID_APLICACION = PA.OID_APLICACION
               INNER JOIN GEPR_TNIVEL_PARAMETRO np
                  ON np.OID_NIVEL_PARAMETRO = PA.OID_NIVEL_PARAMETRO
               WHERE PA.COD_PARAMETRO = par$cod_parametro
                 AND ap.COD_APLICACION = par$cod_aplicacion
                 AND np.COD_NIVEL_PARAMETRO = '1'
                 AND PV.COD_IDENTIFICADOR_NIVEL = par$cod_pais);

    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$des_valor_parametro := '';
    END;

    RETURN var$des_valor_parametro;
  END frecuperar_parametro;

  FUNCTION fconverter_array_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN VARCHAR2_ARRAY IS
    var$tbl      VARCHAR2_ARRAY;
    var$strItems VARCHAR2(32767);
  BEGIN

    FOR idx IN par$table_varchar2.FIRST .. par$table_varchar2.LAST LOOP
      var$strItems := var$strItems || '|' || par$table_varchar2(idx);
    END LOOP;

    var$tbl := string_to_array_###VERSION### (p_string => var$strItems, p_seperator => '|');

    RETURN var$tbl;

  END fconverter_array_table;

  FUNCTION fconverter_array_cod_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$cods_)
    RETURN VARCHAR2_ARRAY IS
    var$tbl      VARCHAR2_ARRAY;
    var$strItems VARCHAR2(32767);
  BEGIN

    FOR idx IN par$table_varchar2.FIRST .. par$table_varchar2.LAST LOOP
      var$strItems := var$strItems || '|' || par$table_varchar2(idx);
    END LOOP;

    var$tbl := string_to_array_###VERSION### (p_string => var$strItems, p_seperator => '|');

    RETURN var$tbl;

  END fconverter_array_cod_table;

  FUNCTION fconverter_array_desc_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$descs_)
    RETURN VARCHAR2_ARRAY IS
    var$tbl      VARCHAR2_ARRAY;
    var$strItems VARCHAR2(32767);
  BEGIN

    FOR idx IN par$table_varchar2.FIRST .. par$table_varchar2.LAST LOOP
      var$strItems := var$strItems || '|' || par$table_varchar2(idx);
    END LOOP;

    var$tbl := string_to_array_###VERSION### (p_string => var$strItems, p_seperator => '|');

    RETURN var$tbl;

  END fconverter_array_desc_table;

  FUNCTION fconverter_array_nel_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$nels_)
    RETURN VARCHAR2_ARRAY IS
    var$tbl      VARCHAR2_ARRAY;
    var$strItems VARCHAR2(32767);
  BEGIN

    FOR idx IN par$table_varchar2.FIRST .. par$table_varchar2.LAST LOOP
      var$strItems := var$strItems || '|' || par$table_varchar2(idx);
    END LOOP;

    var$tbl := string_to_array_###VERSION### (p_string => var$strItems, p_seperator => '|');

    RETURN var$tbl;

  END fconverter_array_nel_table;

  FUNCTION fconverter_array_nbol_table(par$table_varchar2 IN gepr_pcomon_###VERSION###.tipo$nbols_)
    RETURN VARCHAR2_ARRAY IS
    var$tbl      VARCHAR2_ARRAY;
    var$strItems VARCHAR2(32767);
  BEGIN

    FOR idx IN par$table_varchar2.FIRST .. par$table_varchar2.LAST LOOP
      var$strItems := var$strItems || '|' || par$table_varchar2(idx);
    END LOOP;

    var$tbl := string_to_array_###VERSION### (p_string => var$strItems, p_seperator => '|');

    RETURN var$tbl;

  END fconverter_array_nbol_table;

  FUNCTION fdistinct_cods(par$array_Items IN gepr_pcomon_###VERSION###.tipo$cods_)
    RETURN gepr_pcomon_###VERSION###.tipo$cods_ IS
    var$colRetorno gepr_pcomon_###VERSION###.tipo$cods_;
    var$idx        gepr_pcomon_###VERSION###.tipo$nel_ := 1;
  BEGIN

    IF par$array_Items IS NOT NULL AND par$array_Items.COUNT > 0 THEN

      FOR idx IN par$array_Items.FIRST .. par$array_Items.LAST LOOP

        IF fhay_item_coleccion_cods(var$colRetorno, par$array_Items(idx)) = 0 THEN
          var$colRetorno(var$idx) := par$array_Items(idx);
          var$idx := var$idx + 1;
        END IF;
      END LOOP;

    END IF;

    RETURN var$colRetorno;
  END fdistinct_cods;

  FUNCTION fdistinct_oids(par$array_Items IN gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN gepr_pcomon_###VERSION###.tipo$oids_ IS
    var$colRetorno gepr_pcomon_###VERSION###.tipo$oids_;
    var$idx        gepr_pcomon_###VERSION###.tipo$nel_ := 1;
  BEGIN

    IF par$array_Items IS NOT NULL AND par$array_Items.COUNT > 0 THEN

      FOR idx IN par$array_Items.FIRST .. par$array_Items.LAST LOOP

        IF fhay_item_coleccion_oids(var$colRetorno, par$array_Items(idx)) = 0 THEN
          var$colRetorno(var$idx) := par$array_Items(idx);
          var$idx := var$idx + 1;
        END IF;
      END LOOP;

    END IF;

    RETURN var$colRetorno;
  END fdistinct_oids;

  FUNCTION fhay_item_coleccion_oids(par$array_Items IN gepr_pcomon_###VERSION###.tipo$oids_,
                                    par$oid_item    IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS
    var$HayRegistro gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
  BEGIN

    IF par$array_Items IS NOT NULL AND par$array_Items.COUNT > 0 THEN

      FOR idx IN par$array_Items.FIRST .. par$array_Items.LAST LOOP

        IF par$array_Items(idx) = par$oid_item THEN
          var$HayRegistro := 1;
        END IF;

      END LOOP;

    END IF;

    RETURN var$HayRegistro;
  END fhay_item_coleccion_oids;

  FUNCTION fhay_item_coleccion_cods(par$array_Items IN gepr_pcomon_###VERSION###.tipo$cods_,
                                    par$cod_item    IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$nbol_ IS
    var$HayRegistro gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
  BEGIN

    IF par$array_Items IS NOT NULL AND par$array_Items.COUNT > 0 THEN

      FOR idx IN par$array_Items.FIRST .. par$array_Items.LAST LOOP

        IF par$array_Items(idx) = par$cod_item THEN
          var$HayRegistro := 1;
        END IF;

      END LOOP;
    END IF;

    RETURN var$HayRegistro;
  END fhay_item_coleccion_cods;

  function farray_vacio_desc(par$arr in gepr_pcomon_###VERSION###.tipo$descs_)
    return boolean is
    var$res         boolean;
    var$cant_existe integer;
  BEGIN
    /*asume que si tiene un solo elemento, no debe ser nulo
    --      es por la imposibilidad de ODAC de pasar un array realmente vacio como paramentro
    --      que obliga a pasar un array con un elemento nulo */
    var$cant_existe := nvl(par$arr.count, -1);
    if var$cant_existe = 1 then
      if par$arr(par$arr.first) is null then
        var$cant_existe := 0;
      end if;
    end if;
    if var$cant_existe < 1 then
      var$res := true;
    else
      var$res := false;
    end if;
    RETURN var$res;
  END farray_vacio_desc;

  function farray_vacio_obs(par$arr in gepr_pcomon_###VERSION###.tipo$obss_) return boolean is
    var$res         boolean;
    var$cant_existe integer;
  BEGIN
    --asume que si tiene un solo elemento, no debe ser nulo
    --      es por la imposibilidad de ODAC de pasar un array realmente vacio como paramentro
    --      que obliga a pasar un array con un elemento nulo
    var$cant_existe := nvl(par$arr.count, -1);
    if var$cant_existe = 1 then
      if par$arr(par$arr.first) is null then
        var$cant_existe := 0;
      end if;
    end if;
    if var$cant_existe < 1 then
      var$res := true;
    else
      var$res := false;
    end if;
    return var$res;
  END farray_vacio_obs;

  FUNCTION fsplit(par$origem IN VARCHAR2, par$separador IN VARCHAR2)
    RETURN SYS_REFCURSOR IS
    var$rcRetorno SYS_REFCURSOR;
  BEGIN
    OPEN var$rcRetorno FOR
      select regexp_substr(par$origem,
                           '[^' || par$separador || ']+',
                           1,
                           level) AS fnSplit
        from dual
      connect by regexp_substr(par$origem,
                               '[^' || par$separador || ']+',
                               1,
                               level) is not null;

    RETURN var$rcRetorno;
  END fsplit;

  FUNCTION fObtenerItemEntidadPadreCods(par$colItemsPadre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                        par$colItemsHijo  IN gepr_pcomon_###VERSION###.tipo$cods_,
                                        par$ItemCorrente  IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$cods_ IS
    var$colRetorno gepr_pcomon_###VERSION###.tipo$cods_;
  BEGIN

    IF par$colItemsPadre IS NOT NULL AND par$colItemsPadre.COUNT > 0 AND
       par$ItemCorrente IS NOT NULL THEN

      FOR idx IN par$colItemsPadre.FIRST .. par$colItemsPadre.LAST LOOP

        IF par$colItemsPadre(idx) = par$ItemCorrente THEN
          var$colRetorno(var$colRetorno.COUNT) := par$colItemsHijo(idx);
        END IF;

      END LOOP;

    END IF;

    RETURN var$colRetorno;
  END fObtenerItemEntidadPadreCods;

  FUNCTION fObtenerItemEntPadreDescs(par$colItemsPadre IN gepr_pcomon_###VERSION###.tipo$oids_,
                                     par$colItemsHijo  IN gepr_pcomon_###VERSION###.tipo$descs_,
                                     par$ItemCorrente  IN gepr_pcomon_###VERSION###.tipo$oid_)
    RETURN gepr_pcomon_###VERSION###.tipo$descs_ IS
    var$colRetorno gepr_pcomon_###VERSION###.tipo$descs_;
  BEGIN

    IF par$colItemsPadre IS NOT NULL AND par$colItemsPadre.COUNT > 0 AND
       par$ItemCorrente IS NOT NULL THEN

      FOR idx IN par$colItemsPadre.FIRST .. par$colItemsPadre.LAST LOOP

        IF par$colItemsPadre(idx) = par$ItemCorrente THEN
          var$colRetorno(var$colRetorno.COUNT) := par$colItemsHijo(idx);
        END IF;

      END LOOP;

    END IF;

    RETURN var$colRetorno;
  END fObtenerItemEntPadreDescs;
  FUNCTION fobtener_primer_registro_oid(par$array_oids IN gepr_pcomon_###VERSION###.tipo$oids_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
    var$result gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN

    begin
      var$result := par$array_oids(0);
    exception
      WHEN OTHERS THEN
        var$result := null;
    end;

    if var$result is null then
      begin

        var$result := par$array_oids(1);
      exception
        WHEN OTHERS THEN
          var$result := null;
      end;
    end if;
    RETURN var$result;
  END fobtener_primer_registro_oid;

  FUNCTION fobtener_primer_registro_cod(par$array_cods IN gepr_pcomon_###VERSION###.tipo$cods_)
    RETURN gepr_pcomon_###VERSION###.tipo$cod_ IS
    var$result gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN

    begin
      var$result := par$array_cods(0);
    exception
      WHEN OTHERS THEN
        var$result := null;
    end;

    if var$result is null then
      begin

        var$result := par$array_cods(1);
      exception
        WHEN OTHERS THEN
          var$result := null;
      end;
    end if;
    RETURN var$result;
  END fobtener_primer_registro_cod;

  PROCEDURE sinicializar_sesion IS
  BEGIN

    EXECUTE IMMEDIATE 'TRUNCATE TABLE SAPR_GTT_TOIDS';
    COMMIT;

  END sinicializar_sesion;

  /* srecuperar_fechahora_gmt */
  PROCEDURE srecuperar_fechahora_gmt(par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$fyh_fecha      OUT gepr_pcomon_###VERSION###.tipo$fyh_,
                                     par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    var$HusoHorarioEnMinutos  GEPR_TDELEGACION.NEC_GMT_MINUTOS % TYPE := 0;
    var$FechaHoraVeranoInicio GEPR_TDELEGACION.FYH_VERANO_INICIO % TYPE := NULL;
    var$FechaHoraVeranoFin    GEPR_TDELEGACION.FYH_VERANO_FIN % TYPE := NULL;
    var$AjusteHorarioVerano   GEPR_TDELEGACION.NEC_VERANO_AJUSTE % TYPE := 0;
    var$fyh_actual_utc        timestamp := SYS_EXTRACT_UTC(CURRENT_TIMESTAMP);

  BEGIN

    /* fecha GMT */
    par$fyh_fecha := SYS_EXTRACT_UTC(CURRENT_TIMESTAMP);

    /* Verifica si fue informando el codigo de la delegacion */
    IF par$cod_delegacion is null THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              ftraduzir(par$cod_cultura,
                                        'MsgAtributoObrigatorio',
                                        gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                        gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                        'par$cod_delegacion',
                                        1),
                              true);
    END IF;

    /* Verifica si la delegacion existe y recupera las informaciones de GMT */
    BEGIN
      SELECT NVL(NEC_GMT_MINUTOS,0),
             FYH_VERANO_INICIO,
             FYH_VERANO_FIN,
             NVL(NEC_VERANO_AJUSTE,0)
        INTO var$HusoHorarioEnMinutos,
             var$FechaHoraVeranoInicio,
             var$FechaHoraVeranoFin,
             var$AjusteHorarioVerano
        FROM GEPR_TDELEGACION
       WHERE COD_DELEGACION = par$cod_delegacion;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                ftraduzir(par$cod_cultura,
                                          'codigo_delegacion_invalido',
                                          gepr_pcomon_###VERSION###.const$CodFuncDicComon,
                                          gepr_pcomon_###VERSION###.const$codaplicacionsaldos,
                                          par$cod_delegacion,
                                          1),
                                true);
    END;

    IF var$HusoHorarioEnMinutos <> 0 THEN
      /* añadir los minutos de GMT */
      par$fyh_fecha := par$fyh_fecha + var$HusoHorarioEnMinutos / 1440;
    END IF;
    IF ((var$AjusteHorarioVerano <> 0) AND
        (var$FechaHoraVeranoInicio IS NOT NULL) AND
        (var$FechaHoraVeranoFin IS NOT NULL)) THEN

      /* si necesario añadir los minutos de horario verano */
      IF (var$fyh_actual_utc >= var$FechaHoraVeranoInicio) AND
          (var$fyh_actual_utc < (var$FechaHoraVeranoFin + (var$HusoHorarioEnMinutos / 1440))) THEN
        par$fyh_fecha := par$fyh_fecha + var$AjusteHorarioVerano / 1440;

      END IF;
    END IF;

  END srecuperar_fechahora_gmt;

  /* Calcular la fecha GMT */
  PROCEDURE scalcular_gmt(par$fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                          par$oid_delegacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                          par$nel_gmt_minuto IN OUT gepr_pcomon_###VERSION###.tipo$nel_,
                          par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_) IS

    var$HusoHorarioEnMinutos   GEPR_TDELEGACION.NEC_GMT_MINUTOS % TYPE := 0;
    var$FechaHoraVeranoInicio  GEPR_TDELEGACION.FYH_VERANO_INICIO % TYPE := NULL;
    var$FechaHoraVeranoFin     GEPR_TDELEGACION.FYH_VERANO_FIN % TYPE := NULL;
    var$AjusteHorarioVerano    GEPR_TDELEGACION.NEC_VERANO_AJUSTE % TYPE := 0;
    var$nel_gmt_minuto        gepr_pcomon_###VERSION###.tipo$nel_;

   BEGIN
    IF (par$nel_gmt_minuto IS NULL) THEN
      IF par$oid_delegacion IS NULL THEN
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
            VALUES ('2040060004', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                          '2040060004',
                                          'GENERICO',
                                          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                          TO_CHAR(par$fyh_gestion, 'DD/MM/YYYY HH24:MI:SS'),
                                          0), 'VALIDACIONES');
      ELSE
        /* Verifica si la delegacion existe y recupera las informaciones de GMT */
        BEGIN
          SELECT NVL(NEC_GMT_MINUTOS,0),
                FYH_VERANO_INICIO,
                FYH_VERANO_FIN,
                NVL(NEC_VERANO_AJUSTE, 0)
            INTO var$HusoHorarioEnMinutos,
                var$FechaHoraVeranoInicio,
                var$FechaHoraVeranoFin,
                var$AjusteHorarioVerano
            FROM GEPR_TDELEGACION
          WHERE OID_DELEGACION = par$oid_delegacion;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
              var$HusoHorarioEnMinutos := NULL;

              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                  VALUES ('2040060004', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                '2040060004',
                                                'GENERICO',
                                                gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                TO_CHAR(par$fyh_gestion, 'DD/MM/YYYY HH24:MI:SS'),
                                                0), 'VALIDACIONES');
        END;
        IF var$HusoHorarioEnMinutos IS NOT NULL THEN
          IF var$HusoHorarioEnMinutos <> 0 THEN
            par$nel_gmt_minuto := var$HusoHorarioEnMinutos;
          END IF;
          IF ((var$AjusteHorarioVerano <> 0) AND (var$FechaHoraVeranoInicio IS NOT NULL) AND (var$FechaHoraVeranoFin IS NOT NULL)) THEN
            /* si necesario añadir los minutos de horario verano */
            IF (par$fyh_gestion >= var$FechaHoraVeranoInicio) AND
                (par$fyh_gestion < (var$FechaHoraVeranoFin + (var$HusoHorarioEnMinutos / 1440))) THEN
              IF par$nel_gmt_minuto IS NULL THEN
                par$nel_gmt_minuto := 0;
              END IF;
              
              par$nel_gmt_minuto := par$nel_gmt_minuto + var$AjusteHorarioVerano;            
            END IF;
          END IF;
        END IF;
      END IF;
    END IF;

   END scalcular_gmt;


  FUNCTION fsomar_fecha(par$fyh_fecha      IN gepr_pcomon_###VERSION###.tipo$fyh_,
                        par$nel_gmt_minuto IN gepr_pcomon_###VERSION###.tipo$nel_) RETURN gepr_pcomon_###VERSION###.tipo$fyh_ IS

    var$fyh_fecha gepr_pcomon_###VERSION###.tipo$fyh_ := NULL;
  BEGIN

     IF par$fyh_fecha IS NOT NULL THEN
       var$fyh_fecha := par$fyh_fecha;

       IF par$nel_gmt_minuto IS NOT NULL THEN
         var$fyh_fecha := var$fyh_fecha + par$nel_gmt_minuto / 1440;
       END IF;

     END IF;

     RETURN var$fyh_fecha;

  END fsomar_fecha;

  /* Calcular la fecha GMT */
  FUNCTION scalcular_fyh_gmt(par$fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyh_,
                              par$oid_delegacion IN gepr_pcomon_###VERSION###.tipo$oid_) RETURN gepr_pcomon_###VERSION###.tipo$fyh_  IS

    var$HusoHorarioEnMinutos  GEPR_TDELEGACION.NEC_GMT_MINUTOS % TYPE := 0;
    var$FechaHoraVeranoInicio GEPR_TDELEGACION.FYH_VERANO_INICIO % TYPE := NULL;
    var$FechaHoraVeranoFin    GEPR_TDELEGACION.FYH_VERANO_FIN % TYPE := NULL;
    var$AjusteHorarioVerano   GEPR_TDELEGACION.NEC_VERANO_AJUSTE % TYPE := 0;
    var$fyh_actual_utc        timestamp := SYS_EXTRACT_UTC(CURRENT_TIMESTAMP);
    var$nel_gmt_minuto        gepr_pcomon_###VERSION###.tipo$nel_;

    var$fyh_fecha gepr_pcomon_###VERSION###.tipo$fyh_ := NULL;
    
   BEGIN

      IF par$fyh_gestion IS NOT NULL THEN
          
        /* Verifica si la delegacion existe y recupera las informaciones de GMT */
        BEGIN
          SELECT NVL(NEC_GMT_MINUTOS,0),
                FYH_VERANO_INICIO,
                FYH_VERANO_FIN,
                NVL(NEC_VERANO_AJUSTE, 0)
            INTO var$HusoHorarioEnMinutos,
                 var$FechaHoraVeranoInicio,
                 var$FechaHoraVeranoFin,
                 var$AjusteHorarioVerano
            FROM GEPR_TDELEGACION
           WHERE OID_DELEGACION = par$oid_delegacion;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
              var$HusoHorarioEnMinutos := 0;
              var$FechaHoraVeranoInicio := NULL;
              var$FechaHoraVeranoFin := NULL;
              var$AjusteHorarioVerano := NULL;
        END;

        IF var$HusoHorarioEnMinutos IS NOT NULL THEN
          IF var$HusoHorarioEnMinutos <> 0 THEN
            var$nel_gmt_minuto := var$HusoHorarioEnMinutos;
          END IF;
          IF ((var$AjusteHorarioVerano <> 0) AND (var$FechaHoraVeranoInicio IS NOT NULL) AND (var$FechaHoraVeranoFin IS NOT NULL)) THEN
            /* si necesario añadir los minutos de horario verano */
            IF (var$fyh_actual_utc >= var$FechaHoraVeranoInicio) AND
                (var$fyh_actual_utc < (var$FechaHoraVeranoFin + (var$HusoHorarioEnMinutos / 1440))) THEN
              IF var$nel_gmt_minuto IS NULL THEN
                var$nel_gmt_minuto := 0;
              END IF;
              
              var$nel_gmt_minuto := var$nel_gmt_minuto + var$AjusteHorarioVerano;            
            END IF;
          END IF;
          var$fyh_fecha := fsomar_fecha(par$fyh_gestion, var$nel_gmt_minuto);
        END IF;
      END IF;
      
     RETURN var$fyh_fecha;
     
   END scalcular_fyh_gmt;
   
END gepr_putilidades_###VERSION###;
/
