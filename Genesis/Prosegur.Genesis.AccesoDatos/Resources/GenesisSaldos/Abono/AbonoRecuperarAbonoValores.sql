﻿SELECT  DISTINCT
   
    -- Remesas
  R.OID_REMESA,
  R.OID_CUENTA AS R_OID_CUENTA,
  R.COD_EXTERNO AS R_COD_EXTERNO,  
  R.BOL_GESTION_BULTO,
 -- Bultos
  B.OID_BULTO,
  B.OID_CUENTA AS B_OID_CUENTA,  
  B.COD_PRECINTO AS B_COD_PRECINTO, 
    --CLIENTE_ABONO_VALOR
  CLI.OID_CLIENTE,
  CLI.COD_CLIENTE,
  CLI.DES_CLIENTE,
  --SUB_CLIENTE_ABONO_VALOR
  SUBCLI.OID_SUBCLIENTE,
  SUBCLI.COD_SUBCLIENTE,
  SUBCLI.DES_SUBCLIENTE,
  --PTO_SERVICIO_ABONO_VALOR
  PTO.OID_PTO_SERVICIO,
  PTO.COD_PTO_SERVICIO,
  PTO.DES_PTO_SERVICIO,     
  --ABONO_VALOR
  AV.OID_ABONO_VALOR,
  AV.OID_BANCO,
  AV.OID_DIVISA,
  AV.COD_TIPO_CUENTA_BANCARIA,
  AV.COD_CUENTA_BANCARIA,
  AV.COD_DOCUMENTO,
  AV.DES_TITULARIDAD,
  AV.DES_OBSERVACIONES,
  AV.NUM_IMPORTE,
  AE.COD_ABONO_ELEMENTO
FROM SAPR_TREMESA R
LEFT JOIN SAPR_TBULTO B ON B.OID_REMESA = R.OID_REMESA
INNER JOIN SAPR_TABONO_ELEMENTO AE ON AE.OID_REMESA = R.OID_REMESA
INNER JOIN SAPR_TABONO_VALOR AV ON AV.OID_ABONO_VALOR = AE.OID_ABONO_VALOR
INNER JOIN GEPR_TCLIENTE CLI ON CLI.OID_CLIENTE = AV.OID_CLIENTE
LEFT JOIN GEPR_TSUBCLIENTE SUBCLI ON SUBCLI.OID_SUBCLIENTE = AV.OID_SUBCLIENTE
LEFT JOIN GEPR_TPUNTO_SERVICIO PTO ON PTO.OID_PTO_SERVICIO = AV.OID_PTO_SERVICIO
WHERE AV.OID_ABONO = []OID_ABONO