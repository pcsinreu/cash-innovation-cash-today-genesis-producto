﻿SELECT 
	COUNT(0) AS QUANTIDADE
FROM 
	GEPR_TMEDIO_PAGO MP
	INNER JOIN GEPR_TDIVISA D ON
		MP.OID_DIVISA = D.OID_DIVISA
WHERE 
	MP.COD_MEDIO_PAGO = []COD_MEDIO_PAGO
	AND D.COD_ISO_DIVISA = []COD_ISO_DIVISA
	AND ([]COD_TIPO_MEDIO_PAGO IS NULL OR MP.COD_TIPO_MEDIO_PAGO = []COD_TIPO_MEDIO_PAGO)