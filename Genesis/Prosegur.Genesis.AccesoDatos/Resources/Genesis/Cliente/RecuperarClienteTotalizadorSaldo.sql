﻿SELECT
  CLIE_S.OID_CLIENTE,
  CLIE_S.COD_CLIENTE,
  CLIE_S.DES_CLIENTE,
  SUBC_S.OID_SUBCLIENTE,
  SUBC_S.COD_SUBCLIENTE,
  SUBC_S.DES_SUBCLIENTE,
  PUSE_S.OID_PTO_SERVICIO,
  PUSE_S.COD_PTO_SERVICIO,
  PUSE_S.DES_PTO_SERVICIO,
  SC.COD_SUBCANAL
FROM
  SAPR_TCONFIG_NIVEL_MOVIMIENTO CONM 
  INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CONS ON CONM.OID_CONFIG_NIVEL_SALDO = CONS.OID_CONFIG_NIVEL_SALDO 
  INNER JOIN GEPR_TCLIENTE CLIE_M ON CONM.OID_CLIENTE = CLIE_M.OID_CLIENTE 
  INNER JOIN GEPR_TCLIENTE CLIE_S ON CONS.OID_CLIENTE = CLIE_S.OID_CLIENTE 
  LEFT OUTER JOIN GEPR_TSUBCLIENTE SUBC_M ON CONM.OID_SUBCLIENTE = SUBC_M.OID_SUBCLIENTE 
  LEFT OUTER JOIN GEPR_TSUBCLIENTE SUBC_S ON CONS.OID_SUBCLIENTE = SUBC_S.OID_SUBCLIENTE 
  LEFT OUTER JOIN GEPR_TPUNTO_SERVICIO PUSE_M ON CONM.OID_PTO_SERVICIO = PUSE_M.OID_PTO_SERVICIO 
  LEFT OUTER JOIN GEPR_TPUNTO_SERVICIO PUSE_S ON CONS.OID_PTO_SERVICIO = PUSE_S.OID_PTO_SERVICIO
  LEFT OUTER JOIN GEPR_TSUBCANAL SC ON CONM.OID_SUBCANAL = SC.OID_SUBCANAL
WHERE
  CONM.BOL_ACTIVO = 1 
  AND CLIE_M.COD_CLIENTE = []COD_CLIENTE 