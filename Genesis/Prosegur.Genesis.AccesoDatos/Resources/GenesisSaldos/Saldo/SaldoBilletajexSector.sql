﻿SELECT DIV.DES_DIVISA, DENO.DES_DENOMINACION, 'EFECTIVO' TIPO_VALOR, SAEF.BOL_DISPONIBLE, SUM(SAEF.NUM_IMPORTE_SALDO) SALDO
 FROM SAPR_VSALDO_NO_CERT_EFECTIVO SAEF
 
	  INNER JOIN SAPR_VCUENTA CUEN ON SAEF.OID_CUENTA_SALDO = CUEN.OID_CUENTA
	  INNER JOIN GEPR_TDIVISA DIV  ON SAEF.OID_DIVISA = DIV.OID_DIVISA
    LEFT OUTER JOIN GEPR_TDENOMINACION DENO ON SAEF.OID_DENOMINACION = DENO.OID_DENOMINACION
    
WHERE SAEF.BOL_DISPONIBLE = 1
    {0}
    
GROUP BY 'DEFAULT', 'DEFAULT', CUEN.DES_PLANTA, CUEN.DES_SECTOR, DIV.DES_DIVISA, DENO.DES_DENOMINACION, 'EFECTIVO', SAEF.BOL_DISPONIBLE