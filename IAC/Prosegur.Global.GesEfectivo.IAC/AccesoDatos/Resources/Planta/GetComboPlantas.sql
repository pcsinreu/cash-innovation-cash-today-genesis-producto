﻿SELECT OID_PLANTA, DES_PLANTA
  FROM GEPR_TPLANTA INNER JOIN GEPR_TDELEGACION 
	ON GEPR_TPLANTA.oid_delegacion = GEPR_TDELEGACION.oid_delegacion
  WHERE GEPR_TDELEGACION.COD_DELEGACION = []COD_DELEGACION