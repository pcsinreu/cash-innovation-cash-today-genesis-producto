﻿SELECT
       P.OID_PTO_SERVICIO
FROM
       GEPR_TPUNTO_SERVICIO P INNER JOIN GEPR_TSUBCLIENTE S ON P.OID_SUBCLIENTE = S.OID_SUBCLIENTE
                              INNER JOIN GEPR_TCLIENTE C ON S.OID_CLIENTE = C.OID_CLIENTE
WHERE
       C.OID_CLIENTE = []OID_CLIENTE AND
       S.OID_SUBCLIENTE = []OID_SUBCLIENTE AND
       P.COD_PTO_SERVICIO = []COD_PTO_SERVICIO