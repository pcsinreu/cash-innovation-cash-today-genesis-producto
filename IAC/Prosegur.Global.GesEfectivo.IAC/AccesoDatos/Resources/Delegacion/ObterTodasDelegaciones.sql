﻿SELECT DISTINCT D.COD_DELEGACION, D.DES_DELEGACION, D.OID_DELEGACION
FROM GEPR_TPROCESO P
INNER JOIN GEPR_TDELEGACION D ON D.COD_DELEGACION = P.COD_DELEGACION
ORDER BY D.COD_DELEGACION