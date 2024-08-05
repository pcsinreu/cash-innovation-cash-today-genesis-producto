﻿SELECT 
	DISTINCT DIV.COD_ISO_DIVISA, 
	DIV.DES_DIVISA,
	1 AS ES_EFECTIVO
FROM 
	GEPR_TAGRUPACION AG
	INNER JOIN GEPR_TDIVISA_POR_AGRUPACION DIVAG ON (AG.OID_AGRUPACION = DIVAG.OID_AGRUPACION)
	INNER JOIN GEPR_TDIVISA DIV ON (DIV.OID_DIVISA = DIVAG.OID_DIVISA)
WHERE 
	AG.COD_AGRUPACION = []COD_AGRUPACION
	
UNION

SELECT 
	DISTINCT DIV.COD_ISO_DIVISA, 
	DIV.DES_DIVISA,
	0 AS ES_EFECTIVO
FROM 
	GEPR_TAGRUPACION AG
	INNER JOIN GEPR_TMEDIO_PAGO_POR_AGRUPACIO MPAG ON (AG.OID_AGRUPACION = MPAG.OID_AGRUPACION)
	INNER JOIN GEPR_TMEDIO_PAGO MP ON (MPAG.OID_MEDIO_PAGO = MP.OID_MEDIO_PAGO)
	INNER JOIN GEPR_TDIVISA DIV ON (MP.OID_DIVISA = DIV.OID_DIVISA)
WHERE 
	AG.COD_AGRUPACION = []COD_AGRUPACION
ORDER BY ES_EFECTIVO DESC