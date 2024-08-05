CREATE OR REPLACE PACKAGE GEPR_PPROCESO_###VERSION### AS  
/*Version: ###VERSION_COMP###*/

  /* srecupera_iac_proceso */
  PROCEDURE srecupera_iac_proceso(par$oid_cuenta       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_iac_remesa  OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_iac_bulto   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_iac_parcial OUT gepr_pcomon_###VERSION###.tipo$oid_);

END GEPR_PPROCESO_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY GEPR_PPROCESO_###VERSION### AS

  /* srecupera_iac_proceso */
  PROCEDURE srecupera_iac_proceso(par$oid_cuenta       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_iac_remesa  OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_iac_bulto   OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$oid_iac_parcial OUT gepr_pcomon_###VERSION###.tipo$oid_) IS

     var$cod_delegacion gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN

    BEGIN
          SELECT DE.COD_DELEGACION
            INTO var$cod_delegacion
            FROM SAPR_TCUENTA CU
      INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = CU.OID_SECTOR
      INNER JOIN GEPR_TPLANTA PL ON PL.OID_PLANTA = SE.OID_PLANTA
      INNER JOIN GEPR_TDELEGACION DE ON DE.OID_DELEGACION = PL.OID_DELEGACION
           WHERE CU.OID_CUENTA = par$oid_cuenta;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        var$cod_delegacion  := NULL;
    END;
  
    IF var$cod_delegacion IS NOT NULL THEN
      BEGIN
        SELECT INFAD.OID_IAC, INFAD_B.OID_IAC, INFAD_R.OID_IAC
          INTO par$oid_iac_parcial, par$oid_iac_bulto, par$oid_iac_remesa
          FROM (SELECT OID_IAC, OID_IAC_BULTO, OID_IAC_REMESA
                  FROM (
                          SELECT CASE
                                   WHEN PSERV.OID_SUBCLIENTE IS NULL AND PSERV.OID_PTO_SERVICIO IS NULL THEN 3
                                   WHEN PSERV.OID_PTO_SERVICIO IS NULL THEN 2
                                   ELSE 1
                                 END AS NIVELA,
                                 CASE
                                   WHEN PSERV.COD_DELEGACION = '999' THEN '0'
                                   ELSE '1'
                                 END AS NIVELB,
                                 PSERV.OID_IAC,
                                 PSERV.OID_IAC_BULTO,
                                 PSERV.OID_IAC_REMESA
                            FROM GEPR_TPROCESO_POR_PSERVICIO PSERV
                      INNER JOIN GEPR_TPROCESO_SUBCANAL PSUB ON PSUB.OID_PROCESO_POR_PSERVICIO = PSERV.OID_PROCESO_POR_PSERVICIO
                      INNER JOIN SAPR_TCUENTA CU ON CU.OID_CLIENTE = PSERV.OID_CLIENTE
                      INNER JOIN GEPR_TSUBCANAL SUB ON SUB.OID_SUBCANAL = CU.OID_SUBCANAL AND PSUB.OID_SUBCANAL = SUB.OID_SUBCANAL
                           WHERE CU.OID_CUENTA = par$oid_cuenta
                             AND PSERV.BOL_VIGENTE = 1
                             AND PSUB.BOL_VIGENTE = 1
                             AND PSERV.COD_DELEGACION IN (var$cod_delegacion, '999')) PROCESO
                 WHERE ROWNUM = 1
                 ORDER BY NIVELA, NIVELB ASC) IAC
          LEFT JOIN GEPR_TINFORM_ADICIONAL_CLIENTE INFAD ON INFAD.OID_IAC = IAC.OID_IAC
          LEFT JOIN GEPR_TINFORM_ADICIONAL_CLIENTE INFAD_B ON INFAD_B.OID_IAC = IAC.OID_IAC_BULTO
          LEFT JOIN GEPR_TINFORM_ADICIONAL_CLIENTE INFAD_R ON INFAD_R.OID_IAC = IAC.OID_IAC_REMESA;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          par$oid_iac_remesa  := NULL;
          par$oid_iac_bulto   := NULL;
          par$oid_iac_parcial := NULL;
      END;      
    END IF;
  END srecupera_iac_proceso;

END GEPR_PPROCESO_###VERSION###;
/
