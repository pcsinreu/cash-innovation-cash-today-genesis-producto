﻿SELECT TM.COD_TIPO_MERCANCIA,
       TM.DES_TIPO_MERCANCIA,
       TM.OID_TIPO_MERCANCIA
FROM GEPR_TTIPO_MERCANCIA TM
INNER JOIN GEPR_TBULTO_TIPO_MERCANCIA BTM ON BTM.OID_TIPO_MERCANCIA = TM.OID_TIPO_MERCANCIA
WHERE BTM.OID_BULTO = []OID_BULTO