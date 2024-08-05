﻿SELECT 
	SUM(QTDEVIGENTE) 
FROM 
(
	SELECT 
		COUNT(*) AS QTDEVIGENTE 
	FROM 
		GEPR_TDIVISA DIV 
		INNER JOIN GEPR_TDIVISA_POR_PROCESO DIVP ON DIV.OID_DIVISA = DIVP.OID_DIVISA 
		INNER JOIN GEPR_TPROCESO PRO ON DIVP.OID_PROCESO = PRO.OID_PROCESO 
		INNER JOIN GEPR_TPROCESO_POR_PSERVICIO PPS ON PRO.OID_PROCESO = PPS.OID_PROCESO
	WHERE 
		PPS.BOL_VIGENTE = 1 
		AND UPPER(DIV.COD_ISO_DIVISA) = UPPER([]COD_ISO_DIVISA) 
	UNION ALL 
	SELECT 
		COUNT(*) AS QTDEVIGENTE 
	FROM 
		GEPR_TDIVISA DIV 
		INNER JOIN GEPR_TDIVISA_POR_AGRUPACION DIVA ON (DIV.OID_DIVISA = DIVA.OID_DIVISA) 
		INNER JOIN GEPR_TAGRUPACION A ON (DIVA.OID_AGRUPACION = A.OID_AGRUPACION) 
	WHERE 
		A.BOL_VIGENTE = 1 
		AND UPPER(DIV.COD_ISO_DIVISA) = UPPER([]COD_ISO_DIVISA) 
	UNION ALL 
	SELECT 
		COUNT(*) AS QTDEVIGENTE 
	FROM 
		GEPR_TDIVISA DIV 
		INNER JOIN GEPR_TMEDIO_PAGO MP ON (DIV.OID_DIVISA = MP.OID_DIVISA) 
	WHERE 
		MP.BOL_VIGENTE = 1 
		AND UPPER(DIV.COD_ISO_DIVISA) = UPPER([]COD_ISO_DIVISA)
)
