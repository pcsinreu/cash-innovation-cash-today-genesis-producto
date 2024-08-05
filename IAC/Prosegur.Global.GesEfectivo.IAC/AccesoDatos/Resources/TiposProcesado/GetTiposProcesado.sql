SELECT 
	TP.COD_TIPO_PROCESADO, 
	TP.DES_TIPO_PROCESADO, 
	TP.OBS_TIPO_PROCESADO, 
	TP.BOL_VIGENTE,
	C.COD_CARACTERISTICA,
	C.DES_CARACTERISTICA
FROM 
	GEPR_TTIPO_PROCESADO TP
	LEFT OUTER JOIN GEPR_TCARAC_POR_TIPO_PROCESADO CPTP ON
		TP.OID_TIPO_PROCESADO = CPTP.OID_TIPO_PROCESADO
	LEFT OUTER JOIN GEPR_TCARACTERISTICA C ON
		CPTP.OID_CARACTERISTICA = C.OID_CARACTERISTICA
