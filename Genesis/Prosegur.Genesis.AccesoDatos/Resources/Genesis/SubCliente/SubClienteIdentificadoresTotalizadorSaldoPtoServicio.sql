﻿SELECT S.OID_SUBCLIENTE FROM GEPR_TSUBCLIENTE S 
WHERE S.OID_SUBCLIENTE IN([]OID_SUBCLIENTE)
AND (S.BOL_TOTALIZADOR_SALDO=1 OR S.OID_SUBCLIENTE IN(SELECT PS.OID_SUBCLIENTE FROM GEPR_TPUNTO_SERVICIO PS
WHERE PS.BOL_TOTALIZADOR_SALDO = 1 AND PS.OID_SUBCLIENTE IN([]OID_SUBCLIENTE)))
