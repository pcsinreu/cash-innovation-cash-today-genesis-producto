﻿SELECT 1
FROM SAPR_TTRANSACCION_MIGRACION T {0}
WHERE T.FYH_TRANSACCION <= []FYH_TRANSACCION AND 
	  T.FYH_MIGRACION IS NULL AND 
	  ROWNUM = 1 {1}
