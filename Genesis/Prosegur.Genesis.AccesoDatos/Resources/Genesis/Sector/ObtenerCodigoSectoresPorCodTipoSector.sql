﻿SELECT DISTINCT COD_SECTOR FROM GEPR_TSECTOR S
INNER JOIN GEPR_TTIPO_SECTOR TS ON TS.OID_TIPO_SECTOR = S.OID_TIPO_SECTOR
WHERE S.BOL_ACTIVO =1
AND TS.COD_TIPO_SECTOR IN([]COD_TIPO_SECTOR)