CREATE OR REPLACE PACKAGE SAPR_PCAJERO_###VERSION###
  AS
  /*Version: ###VERSION_COMP###*/
    
  FUNCTION fvalidar_datos_atm(par$cod_cajero            IN gepr_pcomon_###VERSION###.tipo$cod_
                            ,par$oid_punto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_
                            ,par$cod_delegacion        IN gepr_pcomon_###VERSION###.tipo$cod_)
      RETURN gepr_pcomon_###VERSION###.tipo$bol_;

END SAPR_PCAJERO_###VERSION###;
/

CREATE OR REPLACE PACKAGE BODY SAPR_PCAJERO_###VERSION###
  AS

   FUNCTION fvalidar_datos_atm(par$cod_cajero            IN gepr_pcomon_###VERSION###.tipo$cod_
                            ,par$oid_punto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_
                            ,par$cod_delegacion        IN gepr_pcomon_###VERSION###.tipo$cod_)
        RETURN gepr_pcomon_###VERSION###.tipo$bol_
    IS
       var$cant INTEGER;
    BEGIN

      SELECT COUNT(1) INTO var$cant
      FROM GEPR_TCAJERO
      WHERE GEPR_TCAJERO.COD_CAJERO = par$cod_cajero
      AND GEPR_TCAJERO.OID_PTO_SERVICIO = par$oid_punto_servicio
      AND GEPR_TCAJERO.COD_DELEGACION = par$cod_delegacion;

      RETURN gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant);
    END fvalidar_datos_atm;

END SAPR_PCAJERO_###VERSION###;
/
