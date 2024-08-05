CREATE OR REPLACE PACKAGE sapr_psector_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  FUNCTION fcargar_rc_caractpsectores(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  FUNCTION fcargar_rc_sectores(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  FUNCTION fcargar_rc_tipossectores(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  FUNCTION frecuperar_identificador(par$cod_sector     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

  PROCEDURE scargar_tt_oids_sector_cuen(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_tt_oids_sector_docs(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_tt_oids_sector_gpr(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_tt_oids_tipo_sec_elem(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_tt_oids_tipo_sec_gpr(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  PROCEDURE scargar_tt_sec_ancestros_gpr(par$oid_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_calificador IN gepr_pcomon_###VERSION###.tipo$cod_ DEFAULT 'SECTOR');

  PROCEDURE srecuperar_delegplantBySector(
        par$acod_sector in   gepr_pcomon_###VERSION###.tipo$cods_,
        par$cod_identificador_ajeno in   gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_reporte out SYS_REFCURSOR);

END sapr_psector_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_psector_###VERSION### AS

  FUNCTION fcargar_rc_caractpsectores(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_caract_tipossectores SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rc_caract_tipossectores FOR
      SELECT DISTINCT ctsts.oid_tipo_sector, cts.cod_caract_tiposector
        FROM sapr_gtt_toids tmp
       INNER JOIN gepr_tcaracttiposectorxtiposec ctsts
          ON tmp.oid = ctsts.oid_tipo_sector
       INNER JOIN gepr_tcaract_tiposector cts
          ON ctsts.oid_caract_tiposector = cts.oid_caract_tiposector
       WHERE tmp.cod_calificador = 'T_SECTOR';
    par$selects := sql%ROWCOUNT;
  
    RETURN var$rc_caract_tipossectores;
  
  END fcargar_rc_caractpsectores;

  FUNCTION fcargar_rc_sectores(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_sectores SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rc_sectores FOR
      SELECT DISTINCT oid_sector,
                      oid_sector_padre,
                      oid_tipo_sector,
                      oid_planta,
                      cod_sector,
                      des_sector,
                      cod_migracion,
                      bol_centro_proceso,
                      bol_permite_disponer_valor,
                      bol_tesoro,
                      bol_conteo,
                      bol_activo,
                      gmt_creacion,
                      des_usuario_creacion,
                      gmt_modificacion,
                      des_usuario_modificacion
        FROM sapr_gtt_toids tmp
       INNER JOIN gepr_tsector s
          ON tmp.oid = s.oid_sector
       WHERE tmp.cod_calificador = 'SECTOR';
    par$selects := sql%ROWCOUNT;
  
    RETURN var$rc_sectores;
  
  END fcargar_rc_sectores;

  FUNCTION fcargar_rc_tipossectores(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_tipos_sectores SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rc_tipos_sectores FOR
      SELECT DISTINCT ts.bol_activo,
                      ts.cod_migracion,
                      ts.cod_tipo_sector,
                      ts.des_tipo_sector,
                      ts.des_usuario_creacion,
                      ts.des_usuario_modificacion,
                      ts.gmt_creacion,
                      ts.gmt_modificacion,
                      ts.oid_tipo_sector
        FROM sapr_gtt_toids tmp
       INNER JOIN gepr_ttipo_sector ts
          ON tmp.oid = ts.oid_tipo_sector
       WHERE tmp.cod_calificador = 'T_SECTOR';
    par$selects := sql%ROWCOUNT;
  
    RETURN var$rc_tipos_sectores;
  
  END fcargar_rc_tipossectores;

  FUNCTION frecuperar_identificador(par$cod_sector     IN gepr_pcomon_###VERSION###.tipo$cod_,
                                    par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
    var$oid_sector gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
  
    BEGIN
      SELECT S.OID_SECTOR
        INTO var$oid_sector
        FROM GEPR_TSECTOR S
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = S.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION D
          ON D.OID_DELEGACION = P.OID_DELEGACION
       WHERE S.COD_SECTOR = par$cod_sector
         AND D.COD_DELEGACION = par$cod_delegacion;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_sector := NULL;
    END;
  
    RETURN var$oid_sector;
  END frecuperar_identificador;

  PROCEDURE scargar_tt_oids_sector_gpr(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$selects             OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                       par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    var$oid_sector_origen  gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_sector_destino gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
  
    SELECT oid_sector_origen, oid_sector_destino
      INTO var$oid_sector_origen, var$oid_sector_destino
      FROM sapr_tgrupo_documento
     WHERE oid_grupo_documento = par$oid_grupo_documento;
  
    par$selects := 0 + sql%ROWCOUNT;
  
    scargar_tt_sec_ancestros_gpr(var$oid_sector_origen);
    par$inserts := 0 + sql%ROWCOUNT;
  
    scargar_tt_sec_ancestros_gpr(var$oid_sector_destino);
    par$inserts := par$inserts + sql%ROWCOUNT;
  
  END scargar_tt_oids_sector_gpr;

  PROCEDURE scargar_tt_oids_sector_docs(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    CURSOR cur$serctores IS
      SELECT d.oid_sector_origen, d.oid_sector_destino
        FROM sapr_tdocumento d
       inner join sapr_gtt_toids tmp
          on tmp.oid = d.oid_documento
       WHERE tmp.COD_CALIFICADOR = 'DOCUMENTO'
          OR tmp.COD_CALIFICADOR = 'DOC_PADRE';
  
  BEGIN
  
    FOR rec$serctor IN cur$serctores() LOOP
      scargar_tt_sec_ancestros_gpr(rec$serctor.oid_sector_origen);
      par$inserts := 0 + sql%ROWCOUNT;
    
      scargar_tt_sec_ancestros_gpr(rec$serctor.oid_sector_destino);
      par$inserts := par$inserts + sql%ROWCOUNT;
    END LOOP rec$efectivo;
    par$selects := par$selects + 1;
  
  END scargar_tt_oids_sector_docs;

  PROCEDURE scargar_tt_sec_ancestros_gpr(par$oid_sector      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                         par$cod_calificador IN gepr_pcomon_###VERSION###.tipo$cod_ DEFAULT 'SECTOR') IS
  BEGIN
    IF par$oid_sector IS NOT NULL THEN
    
      INSERT INTO sapr_gtt_toids
        (oid, cod_calificador)
        SELECT r.oid, r.cod_calificador
          FROM (SELECT s.oid_sector oid, par$cod_calificador cod_calificador
                  FROM gepr_tsector s
                 START WITH s.oid_sector = par$oid_sector
                CONNECT BY PRIOR s.oid_sector_padre = s.oid_sector) r
          LEFT JOIN sapr_gtt_toids t
            ON r.oid = t.oid
           AND t.cod_calificador = par$cod_calificador
         WHERE t.oid IS NULL;
    
    END IF;
  END scargar_tt_sec_ancestros_gpr;

  PROCEDURE scargar_tt_oids_tipo_sec_elem(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO sapr_gtt_toids
      (oid, cod_calificador)
      SELECT DISTINCT oid_tipo_sector, 'T_SECTOR'
        FROM (SELECT oid_tipo_sector
                FROM gepr_ttipo_sectorxplanta
               WHERE oid_planta IN
                     (SELECT s.oid_planta
                        FROM sapr_gtt_toids t
                       INNER JOIN sapr_tcuenta c
                          ON c.oid_cuenta = t.oid
                       INNER JOIN gepr_tsector s
                          ON s.oid_sector = c.oid_sector
                       WHERE cod_calificador = 'CUENTA')
              UNION ALL
              SELECT oid_tipo_sector
                FROM gepr_tsector
               WHERE oid_sector IN
                     (SELECT c.oid_sector
                        FROM sapr_gtt_toids t
                       INNER JOIN sapr_tcuenta c
                          ON c.oid_cuenta = t.oid
                       WHERE t.cod_calificador = 'CUENTA'));
  
    par$inserts := 0 + sql%ROWCOUNT;
  
  END scargar_tt_oids_tipo_sec_elem;

  PROCEDURE scargar_tt_oids_tipo_sec_gpr(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO sapr_gtt_toids
      (oid, cod_calificador)
      SELECT DISTINCT oid_tipo_sector, 'T_SECTOR'
        FROM (SELECT oid_tipo_sector
                FROM gepr_ttipo_sectorxplanta
               WHERE oid_planta IN
                     (SELECT OID
                        FROM sapr_gtt_toids
                       WHERE cod_calificador = 'PLANTA')
              UNION ALL
              SELECT oid_tipo_sector
                FROM gepr_tsector
               WHERE oid_sector IN
                     (SELECT OID
                        FROM sapr_gtt_toids
                       WHERE cod_calificador = 'SECTOR'));
  
    par$inserts := 0 + sql%ROWCOUNT;
  
  END scargar_tt_oids_tipo_sec_gpr;

  PROCEDURE scargar_tt_oids_sector_cuen(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$oid_sector gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
  
    BEGIN
    
      INSERT INTO SAPR_GTT_TOIDS
        (OID, COD_CALIFICADOR)
        SELECT oid_sector, 'SECTOR'
          FROM sapr_tcuenta
         WHERE oid_cuenta IN
               (SELECT OID OID_CUENTA
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'CUENTA')
           AND oid_sector NOT IN
               (SELECT OID OID_CUENTA
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'SECTOR');
      par$inserts := 0 + sql%ROWCOUNT;
    
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_sector := NULL;
    END;
  END scargar_tt_oids_sector_cuen;

    PROCEDURE srecuperar_delegplantBySector(
        par$acod_sector in   gepr_pcomon_###VERSION###.tipo$cods_,
        par$cod_identificador_ajeno in   gepr_pcomon_###VERSION###.tipo$cod_,
        par$rc_reporte out SYS_REFCURSOR) 
    IS
    BEGIN
        /* #### Inicializar los cursores #### */
        OPEN par$rc_reporte FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
        /* Limpiar tabla temporaria */
        DELETE SAPR_GTT_TAUXILIAR;
        COMMIT;
        IF par$cod_identificador_ajeno is not null THEN
            IF par$acod_sector is not null THEN
                /* Recolecto los OID_SECTOR necesarios */
                FOR idx IN par$acod_sector.first .. par$acod_sector.last 
                LOOP
                    IF UPPER(par$cod_identificador_ajeno) = UPPER('MAE') THEN
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR, OID_CAMPO7)
                        SELECT OID_CODIGO_AJENO, 'OID_SECTOR', OID_TABLA_GENESIS
                        FROM    GEPR_TCODIGO_AJENO
                        WHERE
                            UPPER(COD_TIPO_TABLA_GENESIS) = 'GEPR_TSECTOR'
                            AND UPPER(COD_IDENTIFICADOR) = 'MAE'
                            AND COD_AJENO = par$acod_sector(idx);
                    ELSE
                        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                        SELECT OID_SECTOR, 'OID_SECTOR'
                        FROM    GEPR_TSECTOR
                        WHERE   
                            COD_SECTOR = par$acod_sector(idx);
                    END IF;              
                END LOOP;

                IF UPPER(par$cod_identificador_ajeno) = UPPER('MAE') THEN
                    OPEN par$rc_reporte FOR
                        SELECT 
                            CAS.COD_AJENO as "COD_SECTOR",
                            CAS.DES_AJENO as "DES_SECTOR",
                            CAP.COD_AJENO as "COD_PLANTA",
                            CAP.DES_AJENO as "DES_PLANTA",
                            CAD.COD_AJENO as "COD_DELEGACION",
                            CAD.DES_AJENO as "DES_DELEGACION"
                        FROM    SAPR_GTT_TAUXILIAR AUX
                        INNER JOIN GEPR_TCODIGO_AJENO CAS ON CAS.OID_CODIGO_AJENO = AUX.OID_CAMPO1
                        INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = AUX.OID_CAMPO7
                        INNER JOIN GEPR_TPLANTA PLA ON PLA.OID_PLANTA = SEC.OID_PLANTA
                        INNER JOIN GEPR_TDELEGACION  DELEG ON DELEG.OID_DELEGACION = PLA.OID_DELEGACION
                        INNER JOIN GEPR_TCODIGO_AJENO CAP ON CAP.OID_TABLA_GENESIS = PLA.OID_PLANTA AND UPPER(CAP.COD_TIPO_TABLA_GENESIS) = UPPER('GEPR_TPLANTA')
                        INNER JOIN GEPR_TCODIGO_AJENO CAD ON CAD.OID_TABLA_GENESIS = DELEG.OID_DELEGACION AND UPPER(CAD.COD_TIPO_TABLA_GENESIS) = UPPER('GEPR_TDELEGACION')
                        WHERE
                            AUX.COD_CALIFICADOR = 'OID_SECTOR';
                ELSE
                    OPEN par$rc_reporte FOR
                        SELECT 
                            SEC.COD_SECTOR,
                            SEC.DES_SECTOR,
                            PLA.COD_PLANTA,
                            PLA.DES_PLANTA,
                            DELEG.COD_DELEGACION,
                            DELEG.DES_DELEGACION
                        FROM
                            GEPR_TSECTOR SEC
                            INNER JOIN SAPR_GTT_TAUXILIAR AUX ON AUX.OID_CAMPO1 = SEC.OID_SECTOR
                            INNER JOIN GEPR_TCODIGO_AJENO COAJ ON COAJ.OID_TABLA_GENESIS = SEC.OID_SECTOR
                            INNER JOIN GEPR_TPLANTA PLA ON PLA.OID_PLANTA = SEC.OID_PLANTA
                            INNER JOIN GEPR_TDELEGACION  DELEG ON DELEG.OID_DELEGACION = PLA.OID_DELEGACION
                        WHERE
                            AUX.COD_CALIFICADOR = 'OID_SECTOR';
                            
                END IF;
            END IF;
        END IF;
    END srecuperar_delegplantBySector;

END sapr_psector_###VERSION###;
/
