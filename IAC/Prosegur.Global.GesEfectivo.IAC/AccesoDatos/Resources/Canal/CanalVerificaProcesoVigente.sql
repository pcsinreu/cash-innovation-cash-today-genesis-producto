SELECT 
	CAN.COD_CANAL 
FROM 
	GEPR_TCANAL CAN 
	INNER JOIN GEPR_TSUBCANAL SUBC ON CAN.OID_CANAL = SUBC.OID_CANAL 
	INNER JOIN GEPR_TPROCESO_SUBCANAL PSC ON PSC.OID_SUBCANAL = SUBC.OID_SUBCANAL 
WHERE 
	CAN.COD_CANAL = []COD_CANAL 
	AND PSC.BOL_VIGENTE = 1