﻿SELECT 
        COAJ.COD_AJENO
FROM
        GEPR_TCODIGO_AJENO COAJ
WHERE
        COAJ.COD_TIPO_TABLA_GENESIS = []COD_TIPO_TABLA_GENESIS
        AND COAJ.OID_TABLA_GENESIS = []OID_TABLA_GENESIS
        AND COAJ.COD_IDENTIFICADOR = []COD_IDENTIFICADOR
        AND COAJ.BOL_ACTIVO = 1