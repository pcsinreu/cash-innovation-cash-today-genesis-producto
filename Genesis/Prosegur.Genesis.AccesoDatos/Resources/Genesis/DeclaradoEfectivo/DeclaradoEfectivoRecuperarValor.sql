﻿SELECT SUM(DE.NUM_IMPORTE) AS IMPORTE,
       SUM(DE.NEL_CANTIDAD) AS CANTIDAD,
	   DE.OID_UNIDAD_MEDIDA
FROM  SAPR_TDECLARADO_EFECTIVO DE
WHERE
DE.OID_DENOMINACION = []OID_DENOMINACION {0}
GROUP BY DE.OID_UNIDAD_MEDIDA