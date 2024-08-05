CREATE OR REPLACE PACKAGE sapr_pdelegacion_###VERSION###
    AS
  /*Version: ###VERSION_COMP###*/
	
  FUNCTION fcargar_rc_delegacion
    RETURN SYS_REFCURSOR;

  FUNCTION frecuperar_identificador(par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_;

END sapr_pdelegacion_###VERSION###;
/

CREATE OR REPLACE PACKAGE BODY sapr_pdelegacion_###VERSION###
    AS

  FUNCTION fcargar_rc_delegacion RETURN SYS_REFCURSOR IS
    var$rc_delegaciones SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rc_delegaciones FOR
      SELECT DISTINCT d.oid_delegacion,
                      d.cod_pais,
                      d.cod_delegacion,
                      d.des_delegacion,
                      d.bol_vigente,
                      d.oid_pais,
                      d.nec_gmt_minutos,
                      d.fyh_verano_inicio,
                      d.fyh_verano_fin,
                      d.nec_verano_ajuste,
                      d.des_zona,
                      d.gmt_creacion,
                      d.des_usuario_creacion,
                      d.gmt_modificacion,
                      d.des_usuario_modificacion
        FROM gepr_tdelegacion d
       INNER JOIN gepr_tplanta p
          ON (p.oid_delegacion = d.oid_delegacion)
       WHERE p.oid_planta IN
             (SELECT OID
                FROM sapr_gtt_toids
               WHERE cod_calificador = 'PLANTA');
  
    RETURN var$rc_delegaciones;
  
  END fcargar_rc_delegacion;
  
  FUNCTION frecuperar_identificador(par$cod_delegacion IN gepr_pcomon_###VERSION###.tipo$cod_)
    RETURN gepr_pcomon_###VERSION###.tipo$oid_ IS
    var$oid_delegacion gepr_pcomon_###VERSION###.tipo$oid_;
        BEGIN

            BEGIN
      SELECT d.oid_delegacion
                    INTO var$oid_delegacion
        FROM gepr_tdelegacion d
       WHERE d.cod_delegacion = par$cod_delegacion;
            EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$oid_delegacion := NULL;
            END;

            RETURN var$oid_delegacion;
  
        END frecuperar_identificador;

END sapr_pdelegacion_###VERSION###;
/