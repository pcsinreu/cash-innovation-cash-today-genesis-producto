﻿SELECT S.COD_SECTOR
FROM GEPR_TSECTOR S
INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
WHERE S.OID_SECTOR_PADRE = 
(SELECT S.OID_SECTOR
FROM GEPR_TSECTOR S
INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
WHERE S.COD_SECTOR = []COD_SECTOR_PADRE AND D.COD_DELEGACION = []COD_DELEGACION AND P.COD_PLANTA = []COD_PLANTA)
AND D.COD_DELEGACION = []COD_DELEGACION AND P.COD_PLANTA = []COD_PLANTA