﻿SELECT 
    COUNT(0) as QUANTIDADE
FROM
    GEPR_TPUNTO_SERVICIO P
 INNER JOIN GEPR_TSUBCLIENTE SC ON SC.OID_SUBCLIENTE = P.OID_SUBCLIENTE
 INNER JOIN GEPR_TCLIENTE C ON C.OID_CLIENTE = SC.OID_CLIENTE
WHERE 1 = 1
   AND C.COD_CLIENTE = []COD_CLIENTE
   AND SC.COD_SUBCLIENTE = []COD_SUBCLIENTE
   AND P.COD_PTO_SERVICIO = []COD_PTO_SERVICIO