DELETE FROM
	GEPR_TCARAC_POR_TIPO_PROCESADO CPTP
WHERE
	CPTP.OID_TIPO_PROCESADO = (SELECT TP.OID_TIPO_PROCESADO FROM GEPR_TTIPO_PROCESADO TP WHERE TP.COD_TIPO_PROCESADO = []COD_TIPO_PROCESADO)