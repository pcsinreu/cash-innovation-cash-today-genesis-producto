﻿SELECT v.OID_VALOR,
       v.COD_VALOR,
       v.DES_VALOR,
       v.BOL_VIGENTE,
       v.COD_USUARIO,
       v.FYH_ACTUALIZACION
FROM GEPR_TVALOR_TERMINO_MEDIO_PAGO v
WHERE v.OID_TERMINO = []OID_TERMINO
ORDER BY v.DES_VALOR