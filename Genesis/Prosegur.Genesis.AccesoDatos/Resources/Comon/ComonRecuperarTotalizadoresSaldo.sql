﻿SELECT DISTINCT
NIVEL_MOV.OID_CONFIG_NIVEL_MOVIMIENTO,
NIVEL_SALDO.OID_CONFIG_NIVEL_SALDO,
NIVEL_MOV.BOL_DEFECTO,
CLI_SALDO.OID_CLIENTE,
CLI_SALDO.COD_CLIENTE,
CLI_SALDO.DES_CLIENTE,
SBCLI_SALDO.OID_SUBCLIENTE,
SBCLI_SALDO.COD_SUBCLIENTE,
SBCLI_SALDO.DES_SUBCLIENTE,
PTS_SALDO.OID_PTO_SERVICIO,
PTS_SALDO.COD_PTO_SERVICIO,
PTS_SALDO.DES_PTO_SERVICIO,
NIVEL_MOV.OID_SUBCANAL
FROM SAPR_TCONFIG_NIVEL_SALDO NIVEL_SALDO
INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO NIVEL_MOV
      ON NIVEL_MOV.OID_CONFIG_NIVEL_SALDO = NIVEL_SALDO.OID_CONFIG_NIVEL_SALDO
INNER JOIN GEPR_TCLIENTE CLI_SALDO
      ON CLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO 
     ON SBCLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
     AND SBCLI_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
     ON PTS_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
     AND PTS_SALDO.OID_PTO_SERVICIO = NIVEL_SALDO.OID_PTO_SERVICIO
INNER JOIN GEPR_TCLIENTE CLI_MOV
      ON CLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
LEFT JOIN GEPR_TSUBCLIENTE SBCLI_MOV 
     ON SBCLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
     AND SBCLI_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_MOV
     ON PTS_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
     AND PTS_MOV.OID_PTO_SERVICIO = NIVEL_MOV.OID_PTO_SERVICIO     
WHERE NIVEL_MOV.BOL_ACTIVO = 1