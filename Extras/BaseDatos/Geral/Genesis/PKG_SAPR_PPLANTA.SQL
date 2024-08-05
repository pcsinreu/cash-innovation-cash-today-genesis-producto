CREATE OR REPLACE PACKAGE sapr_pplanta_###VERSION###
    AS
  /*Version: ###VERSION_COMP###*/

  PROCEDURE scargar_tt_oids_planta_gpr(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  FUNCTION fcargar_rc_planta(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  FUNCTION fcargar_rc_tp_sector(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR;

  FUNCTION frecuperar_identificador(par$cod_planta IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

END sapr_pplanta_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pplanta_###VERSION### AS

  PROCEDURE scargar_tt_oids_planta_gpr(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO sapr_gtt_toids
      (oid, cod_calificador)
      SELECT DISTINCT oid_planta, 'PLANTA'
        FROM sapr_gtt_toids tmp
       INNER JOIN gepr_tsector s
          ON tmp.oid = s.oid_sector
       WHERE tmp.cod_calificador = 'SECTOR';
    par$inserts := 0 + sql%ROWCOUNT;
  
  END scargar_tt_oids_planta_gpr;

  FUNCTION fcargar_rc_planta(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rc_plantas SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rc_plantas FOR
      SELECT oid_planta,
             oid_delegacion,
             cod_planta,
             des_planta,
             cod_migracion,
             bol_activo,
             gmt_creacion,
             des_usuario_creacion,
             gmt_modificacion,
             des_usuario_modificacion
        FROM gepr_tplanta
       WHERE oid_planta IN
             (SELECT OID
                FROM sapr_gtt_toids
               WHERE cod_calificador = 'PLANTA');
    par$selects := sql%ROWCOUNT;
  
    RETURN var$rc_plantas;
  
  END fcargar_rc_planta;

  FUNCTION fcargar_rc_tp_sector(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$TiposSectorPorPlanta SYS_REFCURSOR;
  BEGIN
  
    OPEN var$TiposSectorPorPlanta FOR
      SELECT DISTINCT OID_PLANTA, OID_TIPO_SECTOR
        FROM SAPR_GTT_TOIDS tmp
       INNER JOIN GEPR_TTIPO_SECTORXPLANTA SXP
          ON tmp.OID = SXP.OID_PLANTA
       WHERE tmp.COD_CALIFICADOR = 'PLANTA';
    par$selects := sql%ROWCOUNT;
  
    RETURN var$TiposSectorPorPlanta;
  
  END fcargar_rc_tp_sector;

  FUNCTION frecuperar_identificador(par$cod_planta IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
    var$oid_planta gepr_pcomon_###VERSION###.tipo$oid_;
  BEGIN
  
    BEGIN
      SELECT P.OID_PLANTA
        INTO var$oid_planta
        FROM GEPR_TPLANTA P
       WHERE P.COD_PLANTA = par$cod_planta;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_planta := NULL;
    END;
  
    RETURN var$oid_planta;
  END frecuperar_identificador;

END sapr_pplanta_###VERSION###;
/
