﻿SELECT 
	MP.COD_MEDIO_PAGO, 
	MP.DES_MEDIO_PAGO, 
	MP.OBS_MEDIO_PAGO, 
	MP.COD_TIPO_MEDIO_PAGO, 
	NULL AS DES_TIPO_MEDIO_PAGO, 
	DIV.COD_ISO_DIVISA, 
	DIV.DES_DIVISA, 
	MP.BOL_VIGENTE 
FROM 
	GEPR_TMEDIO_PAGO_POR_AGRUPACIO MPAGR 
	INNER JOIN GEPR_TMEDIO_PAGO MP ON MPAGR.OID_MEDIO_PAGO = MP.OID_MEDIO_PAGO 
	INNER JOIN GEPR_TDIVISA DIV ON MP.OID_DIVISA = DIV.OID_DIVISA 
WHERE 
	MPAGR.OID_AGRUPACION = []OID_AGRUPACION