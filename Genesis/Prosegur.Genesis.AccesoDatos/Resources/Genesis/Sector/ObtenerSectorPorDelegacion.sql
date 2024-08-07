﻿SELECT DISTINCT COD_SECTOR , DES_SECTOR, S.OID_SECTOR_PADRE, S.OID_SECTOR, P.OID_DELEGACION, D.COD_DELEGACION, P.DES_PLANTA
FROM GEPR_TSECTOR S
	INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA
	INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION
	WHERE S.BOL_ACTIVO = 1 {0}
ORDER BY P.OID_DELEGACION, P.DES_PLANTA, DES_SECTOR