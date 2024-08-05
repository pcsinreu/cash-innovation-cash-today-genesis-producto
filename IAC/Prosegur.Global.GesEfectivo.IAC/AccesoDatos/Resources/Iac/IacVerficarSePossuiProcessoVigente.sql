SELECT 
	IAC.COD_IAC 
FROM 
	GEPR_TINFORM_ADICIONAL_CLIENTE IAC 
	INNER JOIN GEPR_TPROCESO_POR_PSERVICIO PPS ON PPS.OID_IAC = IAC.OID_IAC 
WHERE 
	IAC.COD_IAC = []COD_IAC 
	AND PPS.BOL_VIGENTE = 1