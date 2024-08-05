﻿SELECT SUM(CE.NUM_IMPORTE) AS IMPORTE,
       SUM(CE.NEL_CANTIDAD) AS CANTIDAD,
       CE.OID_CALIDAD,
	   CE.OID_UNIDAD_MEDIDA
FROM  SAPR_TCONTADO_EFECTIVO CE
LEFT JOIN GEPR_TCALIDAD C ON C.OID_CALIDAD = CE.OID_CALIDAD
WHERE
CE.OID_PARCIAL = []OID_ELEMENTO AND CE.OID_DENOMINACION = []OID_DENOMINACION /*AND (CE.OID_DOCUMENTO IS NULL OR CE.OID_DOCUMENTO = []OID_DOCUMENTO)*/
GROUP BY CE.OID_CALIDAD, CE.OID_UNIDAD_MEDIDA