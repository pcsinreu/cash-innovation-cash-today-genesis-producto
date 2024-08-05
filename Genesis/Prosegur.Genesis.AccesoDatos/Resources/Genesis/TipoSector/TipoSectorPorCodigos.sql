﻿SELECT TS.BOL_ACTIVO,
       TS.COD_MIGRACION,
       TS.COD_TIPO_SECTOR,
       TS.DES_TIPO_SECTOR,
       TS.DES_USUARIO_CREACION,
       TS.DES_USUARIO_MODIFICACION,
       TS.GMT_CREACION,
       TS.GMT_MODIFICACION,
       TS.OID_TIPO_SECTOR
FROM GEPR_TTIPO_SECTOR TS
WHERE TS.OID_TIPO_SECTOR = (SELECT OID_TIPO_SECTOR
      FROM GEPR_TSECTOR S
INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
     WHERE S.COD_SECTOR = []COD_SECTOR
       AND P.COD_PLANTA = []COD_PLANTA
       AND D.COD_DELEGACION = []COD_DELEGACION)